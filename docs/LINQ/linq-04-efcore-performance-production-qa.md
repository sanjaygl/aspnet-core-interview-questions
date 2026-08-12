# EF Core — Performance & Production Concerns — Senior Interview Q&A

---

### Q1. How do you diagnose a slow EF Core query in production — what's your actual process?

**Answer:**
"First, get the actual generated SQL and its execution plan — either from EF Core's own logging, or from SQL Server's query store/execution plan cache for that statement. Compare what EF Core generated against what you expected — often the slowness comes from EF Core producing a less efficient query shape than you'd hand-write (unnecessary joins from over-eager `Include`s, a query that isn't SARGable, missing an index the query actually needs). Then fix the root cause — usually an index, a projection to avoid loading unnecessary columns, or restructuring the LINQ to produce simpler SQL — and re-measure."

```csharp
// Enable SQL logging in development
optionsBuilder.UseSqlServer(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging(); // shows parameter values too - dev only, never in production
```

**Cross-question: How do you get EF Core to log the generated SQL, and why might the generated SQL differ from what you expected?**
"`.LogTo(...)` with `LogLevel.Information` (or a proper `ILogger` in production) prints every generated SQL statement. It can differ from expectations because EF Core sometimes generates extra joins for navigation properties, translates seemingly-simple LINQ into more complex SQL than you'd write by hand (especially with multiple `Include`s or `GroupBy`), or falls back to evaluating part of a query client-side if a piece isn't translatable — always verify by actually looking at the SQL rather than assuming."

---

### Q2. What is a Compiled Query in EF Core, and when does it actually move the needle?

**Answer:**
"EF Core already caches the *compiled query plan* for LINQ query shapes automatically behind the scenes — a genuine `EF.CompiledQuery.Compile` is a more explicit, lower-level version of that caching, skipping some of the per-call overhead of resolving parameters/query building for a query that's executed extremely frequently with only its parameter values changing. In practice it matters for very hot, simple, frequently-repeated queries (thousands of calls per second) — for typical CRUD/API workloads, EF Core's automatic caching already covers most of the benefit, so a hand-written compiled query is a micro-optimization, not a first resort."

```csharp
private static readonly Func<AppDbContext, int, Task<Order>> GetOrderById =
    EF.CompileAsyncQuery((AppDbContext ctx, int id) => ctx.Orders.FirstOrDefault(o => o.Id == id));

var order = await GetOrderById(dbContext, 42);
```

**Where to use:** only after profiling shows query-plan-building overhead is a genuine bottleneck for a specific, extremely hot query path — not a default habit for every query.

---

### Q3. What's the difference between `ExecuteUpdate`/`ExecuteDelete` (EF Core 7+) and loading entities then calling `SaveChanges()`?

**Answer:**
"The traditional pattern loads entities into memory, tracks them, mutates properties, and `SaveChanges()` generates per-row `UPDATE`/`DELETE` statements. `ExecuteUpdate`/`ExecuteDelete` generate a single bulk `UPDATE`/`DELETE` statement directly against the database, without ever loading the affected rows into memory or through change tracking at all — much faster for bulk operations, since you skip pulling potentially thousands of rows into the app just to immediately write them back."

```csharp
// Traditional - loads every matching row into memory first
var oldOrders = await dbContext.Orders.Where(o => o.Status == "Cancelled").ToListAsync();
foreach (var o in oldOrders) o.IsArchived = true;
await dbContext.SaveChangesAsync();

// Bulk - one SQL statement, nothing loaded into memory
await dbContext.Orders.Where(o => o.Status == "Cancelled").ExecuteUpdateAsync(s => s.SetProperty(o => o.IsArchived, true));
```

**Cross-question: Why does `ExecuteUpdate` bypass change tracking and concurrency tokens entirely?**
"Because it never materializes entities in the first place — it compiles your LINQ predicate/setters directly into a SQL `UPDATE ... WHERE ...` statement executed server-side. Since there's no tracked entity in memory to compare a concurrency token against, optimistic concurrency checks (like a `RowVersion` column) simply don't apply — if you need concurrency protection, you'd have to add that condition explicitly into the `Where()` clause yourself."

---

### Q4. How does optimistic concurrency work in EF Core, and what exception do you catch when a conflict happens?

**Answer:**
"Mark a property (commonly a `byte[] RowVersion` with `[Timestamp]`, or any property with `.IsConcurrencyToken()`) as a concurrency token. EF Core automatically includes that column's original value in the `WHERE` clause of the generated `UPDATE`/`DELETE`. If another process changed the row in between, zero rows match that `WHERE` condition, and EF Core throws `DbUpdateConcurrencyException` — the application then decides how to resolve it (reload and retry, merge, or surface a conflict to the user)."

```csharp
public class Order
{
    public int Id { get; set; }
    [Timestamp] public byte[] RowVersion { get; set; }
}

try
{
    await dbContext.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException ex)
{
    var entry = ex.Entries.Single();
    await entry.ReloadAsync(); // or apply custom conflict-resolution logic
}
```

---

### Q5. What is an EF Core Interceptor, and what's a real use case for one?

**Answer:**
"An Interceptor is a hook into EF Core's internal pipeline — you can intercept command execution (`DbCommandInterceptor`), save operations (`SaveChangesInterceptor`), and more — to add cross-cutting behavior without scattering that logic through every place `SaveChanges()`/queries get called. Common real uses: automatically stamping `CreatedAt`/`ModifiedAt` audit fields on every save, implementing soft-delete by rewriting a `DELETE` into an `UPDATE IsDeleted = 1`, or logging every executed SQL command with timing for centralized diagnostics."

```csharp
public class AuditingInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        foreach (var entry in eventData.Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
            entry.Property("ModifiedAt").CurrentValue = DateTime.UtcNow;
        return result;
    }
}

optionsBuilder.AddInterceptors(new AuditingInterceptor());
```

---

### Q6. What is DbContext Pooling, and what's the catch that makes it unsafe for certain designs?

**Answer:**
"`AddDbContextPool` reuses `DbContext` instances from a pool instead of constructing a brand-new one per request — the instance is 'reset' (change tracker cleared) and handed back for reuse, saving on allocation/construction overhead for high-throughput scenarios. The catch: it only resets EF Core's own internal state — any *custom* state you added to your `DbContext` subclass (extra fields, injected per-request context you cached on the context) does NOT get reset automatically, so it leaks across requests unless you override `IResettableService` behavior or otherwise avoid keeping such state on the context at all."

```csharp
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(connectionString));
```

**Cross-question: What happens to any state stored on the DbContext itself (not just tracked entities) when it's returned to the pool?**
"It's preserved as-is, unless your `DbContext` subclass explicitly implements the reset logic (or avoids holding extra mutable per-request state at all). This is a real footgun: a custom field like `public string CurrentUserId` set once per request will silently carry over to whichever unrelated request reuses that pooled instance next, unless it's reset. Safest practice: keep `DbContext` subclasses free of any custom mutable state when pooling is enabled."

---

### Q7. How does EF Core's connection resiliency / retry-on-failure work, and why can naive retry logic break transactions?

**Answer:**
"`EnableRetryOnFailure()` wraps database operations with automatic retry logic for transient failures (temporary network blips, cloud database throttling) using an execution strategy. The reason this can break transactions if you're not careful: if you manually start a transaction (`BeginTransaction()`) and a transient failure occurs partway through, naively retrying just the failed statement while assuming the transaction is still valid is wrong — the whole logical unit of work needs to be retried together, from the start, inside the retry strategy's execution scope, not just the one statement that happened to fail."

```csharp
optionsBuilder.UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure());

// Manual transactions need to be wrapped in the execution strategy explicitly
var strategy = dbContext.Database.CreateExecutionStrategy();
await strategy.ExecuteAsync(async () =>
{
    using var transaction = await dbContext.Database.BeginTransactionAsync();
    // ... do work ...
    await dbContext.SaveChangesAsync();
    await transaction.CommitAsync();
});
```

---

### Q8. How do you implement pagination in EF Core with `Skip`/`Take`, and what's the pitfall with large offsets on big tables?

**Answer:**
"`Skip(pageIndex * pageSize).Take(pageSize)` translates to SQL's `OFFSET`/`FETCH NEXT`, which is simple and works fine for shallow pages. The pitfall: the database still has to walk through and discard every row up to the offset before it can return the requested page — so performance degrades as the page number grows, even though each page returns the same small number of rows. On a table with millions of rows, requesting page 10,000 can be dramatically slower than page 1, even though both return the same page size."

```csharp
var page = await dbContext.Orders
    .OrderBy(o => o.OrderId)
    .Skip(pageIndex * pageSize)
    .Take(pageSize)
    .ToListAsync();
```

**Cross-question: How do you get the total record count alongside a paged page of results without running the underlying query twice?**
"You generally can't avoid a second query for an exact total count with plain LINQ — `Skip`/`Take` and `Count()` are separate operations, and EF Core doesn't have a single round-trip way to get both from one query shape. The best practice is to run them concurrently against separate `DbContext` instances if truly needed, or more commonly, just accept two queries but keep the count query cheap (an index-only `COUNT(*)` with the same filter, no joins/projections) — and for very large tables, consider whether the UI genuinely needs an *exact* total at all, versus an approximate count or 'has more' flag, which avoids the expensive full-table count entirely."

```csharp
var query = dbContext.Orders.Where(o => o.Status == "Shipped");

var totalCount = await query.CountAsync(); // query #1 - cheap COUNT, same filter, no Include/projection
var page = await query.OrderBy(o => o.OrderId).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(); // query #2
```

---

### Q9. What's the best practice for combining a paged data query and a total-count query efficiently in the same endpoint?

**Answer:**
"Build the filter once as an `IQueryable` and reuse it for both the count and the page — never duplicate the `Where()` logic, since that risks the two queries drifting out of sync. Keep the count query lean: no `Include()`, no projection to a DTO, just `CountAsync()` against the filtered `IQueryable` — those extras add cost to the count query for no benefit, since you're only asking for a number. Where the frontend allows it, prefer returning a 'has next page' boolean (`Take(pageSize + 1)` and check if you got one extra row) instead of an exact total count — it avoids a full count scan entirely for very large, frequently-paged tables."

```csharp
IQueryable<Order> baseQuery = dbContext.Orders.Where(o => o.Status == status); // single source of truth for the filter

var totalCount = await baseQuery.CountAsync();
var page = await baseQuery
    .OrderBy(o => o.OrderId)
    .Skip(pageIndex * pageSize)
    .Take(pageSize)
    .Select(o => new OrderDto(o.Id, o.Total, o.Status)) // project only what the page actually needs
    .ToListAsync();

return new PagedResult<OrderDto>(page, totalCount);
```

---

### Q10. What are the key techniques for optimizing EF Core queries against tables with millions of rows?

**Answer:**
"Project to a DTO with `.Select()` instead of pulling full entities — fetch only the columns actually needed, reducing both data transfer and change-tracking overhead. Use `AsNoTracking()` for anything read-only. Make sure the columns driving `WHERE`/`ORDER BY`/`JOIN` are actually indexed — check the execution plan, don't assume. Avoid `Include()`-ing large collections you don't need for the current view. For deep pagination specifically, switch from offset-based `Skip`/`Take` to keyset/seek pagination (see Q11). And batch bulk writes instead of calling `SaveChanges()` once per row in a loop."

```csharp
// Lean, read-only, projected - the combination that matters most for large-table read performance
var summaries = await dbContext.Orders
    .AsNoTracking()
    .Where(o => o.CustomerId == customerId)
    .OrderByDescending(o => o.OrderDate)
    .Select(o => new OrderSummaryDto(o.Id, o.OrderDate, o.Total)) // never loads full Order entities
    .Take(20)
    .ToListAsync();
```

---

### Q11. When would you choose keyset/seek pagination over offset-based `Skip`/`Take` in EF Core?

**Answer:**
"Whenever pages can go deep and performance needs to stay flat regardless of page number — infinite-scroll feeds, large exports, any large table where users/jobs might page far beyond the first few pages. Instead of skipping N rows, you remember the last row's sort key from the previous page and filter for rows strictly beyond it — since that filter can use an index Seek directly, performance stays roughly constant no matter how deep you page, unlike `Skip`, which gets progressively more expensive."

```csharp
// Keyset pagination - remembers the last seen OrderId, seeks directly to the next page
var nextPage = await dbContext.Orders
    .Where(o => o.OrderId > lastSeenOrderId)
    .OrderBy(o => o.OrderId)
    .Take(pageSize)
    .ToListAsync();

// lastSeenOrderId comes from the last row of the PREVIOUS page's result
```

**Where to use:** keyset pagination for large, deep-paging datasets and infinite scroll; `Skip`/`Take` remains fine for small tables or UIs that only ever page a few pages deep (e.g., typical admin grids with filters that narrow the result set anyway).
