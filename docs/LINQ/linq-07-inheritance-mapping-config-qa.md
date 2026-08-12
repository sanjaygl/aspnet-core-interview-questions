# EF Core — Inheritance Mapping & Configuration — Senior Interview Q&A

---

### Q1. What are the three inheritance mapping strategies in EF Core — TPH, TPT, and TPC?

**Answer:**
"Table-Per-Hierarchy (TPH) maps an entire class hierarchy into a *single* table, with a discriminator column telling EF Core which derived type each row represents — it's the EF Core default. Table-Per-Type (TPT) gives each class in the hierarchy its own table, with the derived tables sharing a primary key with the base table and joined together to reconstruct a full object. Table-Per-Concrete-Type (TPC) gives each *concrete* (non-abstract) class its own complete, independent table, duplicating the base class's columns in every derived table instead of sharing them via a join."

```csharp
public abstract class Employee { public int Id; public string Name; }
public class Manager : Employee { public decimal Bonus; }
public class Engineer : Employee { public string ProgrammingLanguage; }

// TPH (default) - ONE table: Employees(Id, Name, Discriminator, Bonus, ProgrammingLanguage)
//   Bonus is NULL for Engineer rows, ProgrammingLanguage is NULL for Manager rows

// TPT - THREE tables: Employees(Id, Name), Managers(Id, Bonus), Engineers(Id, ProgrammingLanguage)
//   reconstructing a Manager requires a JOIN between Employees and Managers
modelBuilder.Entity<Manager>().ToTable("Managers");
modelBuilder.Entity<Engineer>().ToTable("Engineers");

// TPC - TWO tables: Managers(Id, Name, Bonus), Engineers(Id, Name, ProgrammingLanguage)
//   Name is duplicated in both tables, no shared base table at all
modelBuilder.Entity<Employee>().UseTpcMappingStrategy();
```

**Cross-question: Why is TPH (the EF Core default) usually the fastest to query, but the "ugliest" schema?**
"Fastest because querying any subset of the hierarchy (or the whole thing) never needs a `JOIN` — it's all one table, filtered by the discriminator column. The trade-off is schema cleanliness: every derived type's columns live in the same table, so the table accumulates a growing number of nullable columns (only relevant to some rows), and adding a very different subclass can make the table sprawl uncomfortably wide. TPT is cleaner relationally (proper normalization, no nullable columns for irrelevant subclasses) but pays a `JOIN` on every query that needs a derived type's data. TPC avoids the join too, but duplicates shared columns across every concrete table and complicates any query that needs to search across the *whole* hierarchy at once (typically requires a `UNION` under the hood)."

**Where to use:** TPH by default (simplest, fastest) unless the hierarchy has wildly different derived types with many type-specific columns, or normalization/schema cleanliness genuinely matters more than raw query performance — then consider TPT; TPC is a narrower fit, mainly when concrete types rarely need to be queried together as a whole hierarchy.

---

### Q2. What is a Value Converter in EF Core, and when would you write one?

**Answer:**
"A Value Converter tells EF Core how to translate a property's CLR type into a different type for storage, and back again when reading — without changing the entity class's public shape. Common cases: storing an `enum` as its string name instead of an integer (more readable in the database, more resilient to enum reordering), storing a complex type as JSON in a single column, or applying encryption/decryption transparently on read/write."

```csharp
modelBuilder.Entity<Order>()
    .Property(o => o.Status)
    .HasConversion<string>(); // stores the OrderStatus enum as its string name, not its underlying int

// A fully custom converter
modelBuilder.Entity<Customer>()
    .Property(c => c.Email)
    .HasConversion(
        v => v.ToLowerInvariant(),   // C# value -> stored value
        v => v);                       // stored value -> C# value
```

**Where to use:** any time the natural C# representation of a property doesn't match how you want it persisted — enum-as-string, a value object serialized to a single JSON column, normalization (lowercasing) applied consistently on write.

---

### Q3. Fluent API vs Data Annotations — which wins if both are used, and which should you prefer?

**Answer:**
"Fluent API configuration (in `OnModelCreating`) always wins over Data Annotations (attributes on the entity class) if both configure the same thing differently — Fluent API is applied after and overrides conflicting annotation-based configuration. Data Annotations are simpler and colocate the rule with the property, which is nice for basic things (`[Required]`, `[MaxLength(100)]`), but Fluent API is strictly more powerful — it can express things Data Annotations simply can't (composite keys, TPH/TPT/TPC mapping, value converters, precise index configuration) — so for anything beyond the simplest constraints, Fluent API is generally preferred, and keeps persistence concerns out of the domain model's attributes."

```csharp
// Data Annotation
public class Customer { [MaxLength(100)] public string Name { get; set; } }

// Fluent API - wins if both configure the same property differently, and can express far more
modelBuilder.Entity<Customer>().Property(c => c.Name).HasMaxLength(150);
// Actual effective max length here is 150 — Fluent API overrode the annotation's 100
```

**Where to use:** Data Annotations for quick, simple, self-evident constraints if the team prefers that style; Fluent API as the primary source of truth for anything more complex, or when the team wants to keep the entity classes free of persistence-specific attributes entirely.

---

### Q4. What's the difference between DbContext Pooling and ADO.NET connection pooling — are they the same thing?

**Answer:**
"No, they're two separate layers. ADO.NET connection pooling (handled by the underlying database driver, e.g., `Microsoft.Data.SqlClient`) reuses actual physical database *connections* under the hood — this happens automatically regardless of whether you use DbContext pooling at all, and is what makes opening/closing connections cheap in typical EF Core usage. DbContext Pooling (`AddDbContextPool`) is a higher-level EF Core feature that reuses *`DbContext` instances themselves* — the C# objects wrapping change tracking, the model cache, and so on — to avoid the overhead of constructing a new `DbContext` (and its internal services) per request. You can have ADO.NET connection pooling without DbContext pooling (the normal default), but DbContext pooling always relies on connection pooling still happening underneath it."

**Where to use:** ADO.NET connection pooling is essentially always on and not something you typically configure directly; consider DbContext pooling specifically for high-throughput APIs where profiling shows meaningful overhead from `DbContext` construction itself — and remember the earlier caveat about custom mutable state not resetting between pooled uses (see [[linq-04-efcore-performance-production-qa]]).

---

### Q5. How would you generate a reviewable SQL script from EF Core migrations instead of applying them directly against production?

**Answer:**
"`dotnet ef migrations script` generates the raw SQL for one or more pending migrations without executing anything — that script can be reviewed by a DBA, checked into source control, or run through a proper deployment/change-management pipeline, instead of the application calling `context.Database.Migrate()` directly against a live production database (which offers no review step and runs with whatever permissions the application's connection string has, often broader than you'd want to grant just for routine app operation)."

```
dotnet ef migrations script FromMigration ToMigration --idempotent --output migration.sql
```

"The `--idempotent` flag wraps each migration in a check against the migrations history table, so the script is safe to run even if some of those migrations were already applied — useful when the same script might be run across multiple environments that aren't perfectly in sync."

**Where to use:** any production deployment process with a change-review requirement, or where the application's own database credentials shouldn't have DDL permissions — generate the script, hand it off for review/execution through the proper channel, rather than auto-migrating on app startup.
