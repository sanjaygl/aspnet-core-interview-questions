# EF Core — Core Concepts — Senior Interview Q&A

---

### Q1. What is Change Tracking in EF Core, and how does `SaveChanges()` know what to update?

**Answer:**
"When you query entities normally, EF Core's `ChangeTracker` keeps a snapshot of each entity's original property values alongside the entity itself. When you call `SaveChanges()`, EF Core compares the current values against that snapshot for every tracked entity, figures out which properties actually changed, and generates the minimal `UPDATE` statements needed — you never write the `UPDATE` SQL yourself, it's inferred from the diff."

```csharp
var order = await dbContext.Orders.FindAsync(1); // tracked, snapshot taken here
order.Status = "Shipped";                          // change tracker notices this on SaveChanges
await dbContext.SaveChangesAsync();                 // generates: UPDATE Orders SET Status = 'Shipped' WHERE OrderId = 1
```

**Cross-question: What does `AsNoTracking()` actually save you, and when could using it break something?**
"It skips creating and maintaining that snapshot entirely, which reduces memory usage and CPU overhead — worthwhile for read-only queries (reporting, API `GET` endpoints returning data you'll never modify). It breaks things if you then try to modify the entity and call `SaveChanges()` expecting it to persist — since it was never tracked, EF Core has no idea anything changed, and the update silently does nothing unless you explicitly re-attach and mark it modified."

```csharp
var order = await dbContext.Orders.AsNoTracking().FirstAsync(o => o.Id == 1);
order.Status = "Shipped";
await dbContext.SaveChangesAsync(); // does NOTHING — order was never tracked, EF Core sees no change
```

---

### Q2. Why is `DbContext` registered as Scoped in DI, and what goes wrong if you make it a Singleton?

**Answer:**
"`DbContext` is deliberately lightweight and not thread-safe — it's designed to be created, used for one unit of work (typically one HTTP request), and disposed. Scoped means one instance per request, which matches that lifecycle naturally. A Singleton `DbContext` would be shared across every concurrent request on every thread — since it's not thread-safe, concurrent access from multiple requests corrupts its internal change-tracking state and throws unpredictable exceptions, plus its change tracker would accumulate every entity ever loaded across the app's entire lifetime, leaking memory."

```csharp
// Correct
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString)); // Scoped by default

// Wrong - do not do this
builder.Services.AddSingleton<AppDbContext>(...); // shared across all concurrent requests — not thread-safe
```

**Cross-question: What actually happens if two threads share the same `DbContext` instance concurrently?**
"You'll typically get an `InvalidOperationException` along the lines of 'A second operation was started on this context instance before a previous operation completed' — EF Core actively detects concurrent use on the same instance and throws, rather than silently corrupting data. It's a hard failure, not a subtle bug, but it can be intermittent and confusing if concurrency only happens under load."

---

### Q3. Lazy Loading vs Eager Loading (`Include`) vs Explicit Loading — trade-offs of each?

**Answer:**
"Eager Loading (`.Include()`) loads related data as part of the initial query, in one round trip (or a couple, with split queries) — predictable, but can load more than you need if overused. Lazy Loading automatically fetches a related entity the *first time* a navigation property is accessed, transparently issuing a new query behind the scenes — convenient, but this is exactly the mechanism behind the N+1 problem if you access a lazy-loaded navigation property inside a loop. Explicit Loading is a manual, deliberate call (`context.Entry(order).Collection(o => o.Lines).Load()`) triggered only when you decide to — same underlying extra-query cost as lazy loading, but visible and intentional in the code instead of hidden."

```csharp
// Eager
var orders = await dbContext.Orders.Include(o => o.Customer).ToListAsync();

// Lazy (requires virtual navigation properties + UseLazyLoadingProxies())
foreach (var order in orders)
    Console.WriteLine(order.Customer.Name); // each access triggers its OWN query if not already loaded — N+1 risk

// Explicit
var order = await dbContext.Orders.FindAsync(1);
await dbContext.Entry(order).Reference(o => o.Customer).LoadAsync(); // deliberate, visible extra query
```

**Cross-question: What has to be true about your entity classes for lazy loading to even work in EF Core?**
"Navigation properties must be declared `virtual`, and the project must install `Microsoft.EntityFrameworkCore.Proxies` and call `.UseLazyLoadingProxies()` when configuring the context. EF Core generates a runtime proxy subclass that overrides those virtual properties to trigger loading on first access — without both pieces in place, lazy loading silently doesn't happen (the navigation property just stays `null`/empty)."

---

### Q4. What is the N+1 query problem in EF Core, and how do you actually spot it happened, after the fact, in production?

**Answer:**
"It's issuing 1 query for a list, then N additional queries — one per item — to fetch related data, instead of 1 query total. In EF Core it usually happens via lazy loading inside a loop, or forgetting `.Include()` and accessing a navigation property per item. In production, you spot it by looking at database query logs/APM traces for a request and seeing an unexpectedly large, repetitive burst of nearly-identical queries differing only by an ID parameter — a strong signature of N+1 that a single slow-query alert on its own wouldn't necessarily flag, since each individual query might look 'fast.'"

```csharp
// N+1 - one query for orders, then one MORE query per order for Customer
var orders = await dbContext.Orders.ToListAsync();
foreach (var order in orders)
    Console.WriteLine(order.Customer.Name); // lazy-loads, triggers a query PER order

// Fixed - one query total
var orders2 = await dbContext.Orders.Include(o => o.Customer).ToListAsync();
```

**Where to use:** enable EF Core's logging (`.LogTo(Console.WriteLine, LogLevel.Information)` in dev, or a proper structured logger in production) during development to actually see the SQL being generated — this catches N+1 far earlier than waiting to notice it from production metrics.

---

### Q5. What's the difference between `Include`/`ThenInclude` producing a single query vs `AsSplitQuery()`?

**Answer:**
"By default, multiple `Include`s are combined into one SQL query with multiple `JOIN`s. That's fine for one-to-one/many-to-one relations, but including *multiple* one-to-many collections in a single query causes a Cartesian explosion — the row count multiplies across each joined collection, so EF Core ends up transferring far more data than the actual entity count, then has to de-duplicate it client-side. `AsSplitQuery()` instead issues a separate SQL query per included collection, avoiding the Cartesian multiplication, at the cost of multiple round trips instead of one."

```csharp
// Single query - if Orders has many Lines AND many Payments, row count multiplies (Cartesian)
var customers = await dbContext.Customers
    .Include(c => c.Orders).ThenInclude(o => o.Lines)
    .Include(c => c.Payments)
    .ToListAsync();

// Split query - separate queries per collection, avoids the multiplication
var customers2 = await dbContext.Customers
    .Include(c => c.Orders).ThenInclude(o => o.Lines)
    .Include(c => c.Payments)
    .AsSplitQuery()
    .ToListAsync();
```

**Cross-question: Why would splitting one query into several ever be *faster* than a single query with several joins?**
"Because the single-query Cartesian-product version can transfer dramatically more row data over the network than actually needed — e.g., a customer with 10 orders and 10 payments produces 100 joined rows in a single query (10×10), even though there are really only 20 related records total. Multiple smaller, targeted queries avoid that multiplication entirely, so despite the extra round trips, total data transferred and processing time can be far lower for collections that are both large and combined in one `Include` chain."

---

### Q6. How do EF Core Migrations work, and what's the danger of running `Database.EnsureCreated()` in production instead?

**Answer:**
"Migrations are versioned, incremental C# files (generated via `dotnet ef migrations add`) that describe schema changes step by step, each with an `Up()` and `Down()` method — applied in order via `dotnet ef database update` or `context.Database.Migrate()`. This gives you a reviewable, source-controlled history of every schema change, and a way to roll back. `Database.EnsureCreated()` is a shortcut meant for quick prototyping/tests — it creates the schema directly from the current model snapshot if the database doesn't exist yet, but it has no concept of incremental changes at all: it can't apply schema *updates* to an existing database, and it's incompatible with the Migrations history table, so mixing the two approaches breaks things."

```csharp
// Migrations - production-appropriate, incremental, reviewable
dotnet ef migrations add AddOrderStatusColumn
dotnet ef database update

// EnsureCreated - dev/test only, all-or-nothing, no incremental updates
context.Database.EnsureCreated();
```

**Where to use:** Migrations for any real application with a schema that evolves over time; `EnsureCreated()` only for throwaway test databases (e.g., integration tests spinning up a fresh SQLite/InMemory database per test run).

---

### Q7. What's a Shadow Property, and why would you use one instead of a real property on the entity class?

**Answer:**
"A Shadow Property is a column that exists in the database and in EF Core's model, but has no corresponding CLR property on the entity class itself — EF Core tracks its value internally, and you read/write it through `context.Entry(entity).Property("PropName").CurrentValue` instead of a normal property access. Useful for metadata you don't want cluttering the domain model itself — like `CreatedAt`/`ModifiedAt` audit columns, or a foreign key you're deliberately keeping out of the entity's public API and managing only through the navigation property."

```csharp
modelBuilder.Entity<Order>().Property<DateTime>("LastModified");

// Setting it (e.g., in a SaveChanges override for auditing)
dbContext.Entry(order).Property("LastModified").CurrentValue = DateTime.UtcNow;
```

**Where to use:** audit columns (`CreatedAt`, `ModifiedAt`, `CreatedBy`) that are infrastructure concerns, not part of the domain model's actual behavior/identity — keeps the entity class focused on business logic instead of persistence plumbing.
