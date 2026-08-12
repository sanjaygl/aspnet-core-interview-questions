# EF Core — Advanced / Scenario-Based — Senior Interview Q&A

---

### Q1. What is a Global Query Filter, and what's a realistic use case?

**Answer:**
"A Global Query Filter is a predicate configured once on an entity type in `OnModelCreating`, automatically applied to *every* query against that entity, without needing to repeat `.Where(...)` everywhere in the codebase. Classic use cases: soft delete (only ever show rows where `IsDeleted == false`, unless explicitly asked otherwise) and multi-tenancy (only show rows belonging to the current tenant)."

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Order>().HasQueryFilter(o => !o.IsDeleted);
}

var orders = await dbContext.Orders.ToListAsync(); // automatically excludes IsDeleted == true rows, everywhere
```

**Cross-question: How do you bypass a global query filter for one specific query when you legitimately need to?**
"`.IgnoreQueryFilters()` — useful for an admin 'show deleted items' view, or a background job that needs to process soft-deleted records."

```csharp
var allIncludingDeleted = await dbContext.Orders.IgnoreQueryFilters().ToListAsync();
```

---

### Q2. What is an Owned Type / value object in EF Core, and how does it map to the database?

**Answer:**
"An Owned Type models a value object that doesn't have its own identity or table — it's a set of properties conceptually grouped together (like an `Address` with Street/City/Zip) that belongs entirely to its owner entity. By default, EF Core maps an owned type's properties into columns on the *same table* as the owner, rather than creating a separate table with a foreign key — matching the idea that the value object has no independent existence or identity of its own."

```csharp
public class Customer
{
    public int Id { get; set; }
    public Address ShippingAddress { get; set; } // owned type, not its own entity
}

public class Address // no Id, no independent identity - just grouped data
{
    public string Street { get; set; }
    public string City { get; set; }
}

modelBuilder.Entity<Customer>().OwnsOne(c => c.ShippingAddress);
// Customer table gets columns: ShippingAddress_Street, ShippingAddress_City
```

**Where to use:** any grouped set of properties that's conceptually a single value (address, money amount with currency, a date range) rather than an independent entity with its own lifecycle/identity.

---

### Q3. How would you implement multi-tenancy in EF Core?

**Answer:**
"Three common strategies. Database-per-tenant — full isolation, each tenant gets a separate database/connection string, resolved at runtime; strongest isolation, most operational overhead (migrations must run per tenant database). Schema-per-tenant — one database, separate schema per tenant; less overhead than separate databases, still decent isolation. Shared table with a `TenantId` column — simplest to operate, but requires a Global Query Filter on `TenantId` on every entity to prevent one tenant's queries from ever seeing another's data, and that filter has to be applied absolutely consistently or it's a real data leak risk."

```csharp
// Shared-table approach - global filter tied to the current tenant, resolved per request
modelBuilder.Entity<Order>().HasQueryFilter(o => o.TenantId == _currentTenantService.TenantId);
```

**Where to use:** shared-table with `TenantId` + query filters for most SaaS products (cheapest to operate, scales fine); database-per-tenant when a tenant needs strict isolation/compliance guarantees or wildly different scaling needs.

---

### Q4. How do you run raw SQL safely in EF Core, and how do you avoid SQL injection while doing it?

**Answer:**
"`FromSqlInterpolated` (with a C# interpolated string) or `FromSqlRaw` with explicit `SqlParameter` objects both parameterize values automatically/safely — the interpolated values never get concatenated directly into the SQL text, they're passed as real ADO.NET parameters under the hood. The unsafe version is manually string-concatenating user input into `FromSqlRaw`'s SQL text — that reintroduces classic SQL injection, defeating the entire point of using an ORM's parameterization."

```csharp
// Safe - interpolated values become real SQL parameters, not concatenated text
var status = userInput;
var orders = await dbContext.Orders.FromSqlInterpolated($"SELECT * FROM Orders WHERE Status = {status}").ToListAsync();

// DANGEROUS - do not do this
var orders2 = await dbContext.Orders.FromSqlRaw($"SELECT * FROM Orders WHERE Status = '{status}'").ToListAsync();
```

**Where to use:** raw SQL for queries EF Core's LINQ provider can't express efficiently (complex window functions, hints, a stored procedure call) — always via the parameterized APIs, never raw string concatenation.

---

### Q5. How do transactions work across multiple `SaveChanges()` calls?

**Answer:**
"Each individual `SaveChanges()` call is implicitly wrapped in its own transaction. To make several EF Core operations — potentially across multiple `SaveChanges()` calls — atomic together, wrap them in an explicit transaction with `dbContext.Database.BeginTransactionAsync()`, do all the work, then `CommitAsync()` (or let it roll back on an exception)."

```csharp
using var transaction = await dbContext.Database.BeginTransactionAsync();
try
{
    dbContext.Orders.Add(newOrder);
    await dbContext.SaveChangesAsync();       // part of the transaction, not yet committed

    dbContext.Inventory.Update(stockRecord);
    await dbContext.SaveChangesAsync();       // still part of the same transaction

    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync();
    throw;
}
```

**Cross-question: What happens if you mix EF Core's own transaction with a manually-started ADO.NET transaction?**
"You generally shouldn't — EF Core has its own mechanism for wrapping/enlisting in transactions (`Database.BeginTransaction()`, or `Database.UseTransaction(existingAdoTransaction)` if you genuinely need to share one with raw ADO.NET code). Starting a separate, unrelated ADO.NET transaction alongside EF Core's own connection/transaction can lead to confusing behavior or errors, since there'd be two different transaction scopes that aren't coordinated with each other. If you must share a connection/transaction with non-EF-Core code, use `UseTransaction()` to explicitly enlist EF Core in the existing one instead of creating a second, independent transaction."

---

### Q6. How would you unit/integration test code that uses EF Core?

**Answer:**
"For pure business-logic unit tests that don't need real query translation, the EF Core InMemory provider is fast and simple, but it doesn't enforce real relational constraints and — critically — doesn't validate that your LINQ actually translates to valid SQL; a query that passes against InMemory can still throw against a real SQL Server. For genuine integration tests that need to verify actual SQL behavior (constraints, real translation, concurrency), use SQLite in-memory mode, or better, a real disposable SQL Server instance (e.g., via Testcontainers) spun up per test run — slower, but it's testing against the real thing, not an approximation."

```csharp
// Fast, but not a real translation check
options.UseInMemoryDatabase("TestDb");

// Closer to real SQL Server behavior, still fast, runs in-process
options.UseSqlite("DataSource=:memory:");

// Most accurate - real SQL Server behavior, via a disposable container
// (Testcontainers.MsSql, spun up fresh per test run)
```

**Where to use:** InMemory for fast, purely logical unit tests where SQL translation isn't the concern; SQLite or a real containerized SQL Server for anything where "does this actually work against a real database" matters — which, for most EF Core-specific bugs, is most of the time.

---

### Q7. How would you handle a schema migration that needs to run against a live production database with zero downtime?

**Answer:**
"Same underlying principle as any zero-downtime schema change (see [[sql-06-db-objects-scenarios-qa]] for the general SQL-side pattern): avoid a single migration that both changes the schema and requires the application to switch over atomically. Add new columns as nullable first, deploy application code that can handle both the old and new shape simultaneously, backfill data in batches, then only enforce constraints (`NOT NULL`, drop old columns) in a later migration once everything's confirmed migrated and the old code path is fully retired. EF Core's migrations can express each of these as separate, sequential steps — the discipline is deploying them in the right order relative to application code changes, not doing it all in one migration."

```csharp
// Migration 1: add nullable column, deploy app code that writes to BOTH old and new column
migrationBuilder.AddColumn<string>("NewStatus", "Orders", nullable: true);

// (separate deploy) Backfill NewStatus from the old column, in batches, via a script or background job

// Migration 2 (later, after backfill confirmed complete): enforce NOT NULL, drop the old column
migrationBuilder.AlterColumn<string>("NewStatus", "Orders", nullable: false);
migrationBuilder.DropColumn("OldStatus", "Orders");
```
