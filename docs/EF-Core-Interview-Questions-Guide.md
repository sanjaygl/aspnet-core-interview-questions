# Entity Framework Core - Complete Interview Guide

## Table of Contents

1. [What is Entity Framework Core?](#1-what-is-entity-framework-core)
2. [What is DbContext and DbSet in EF Core?](#2-what-is-dbcontext-and-dbset-in-ef-core)
3. [Code First vs Database First Approach](#3-code-first-vs-database-first-approach)
4. [What are Migrations in EF Core?](#4-what-are-migrations-in-ef-core)
5. [What is Change Tracking in EF Core?](#5-what-is-change-tracking-in-ef-core)
6. [Lazy Loading vs Eager Loading vs Explicit Loading](#6-lazy-loading-vs-eager-loading-vs-explicit-loading)
7. [What are Tracking vs No-Tracking Queries?](#7-what-are-tracking-vs-no-tracking-queries)
8. [How does LINQ work with EF Core?](#8-how-does-linq-work-with-ef-core)
9. [What is the N+1 Problem in EF Core?](#9-what-is-the-n1-problem-in-ef-core)
10. [What is AsNoTracking and when to use it?](#10-what-is-asnotracking-and-when-to-use-it)
11. [How to execute Raw SQL in EF Core?](#11-how-to-execute-raw-sql-in-ef-core)
12. [How to handle Transactions in EF Core?](#12-how-to-handle-transactions-in-ef-core)
13. [What is Concurrency Handling in EF Core?](#13-what-is-concurrency-handling-in-ef-core)
14. [EF Core vs Dapper - Key Differences](#14-ef-core-vs-dapper---key-differences)
15. [Performance Optimization Techniques in EF Core](#15-performance-optimization-techniques-in-ef-core)
16. [What are Navigation Properties?](#16-what-are-navigation-properties)
17. [What is Include() and ThenInclude()?](#17-what-is-include-and-theninclude)
18. [What are Shadow Properties in EF Core?](#18-what-are-shadow-properties-in-ef-core)
19. [What is Connection Resiliency in EF Core?](#19-what-is-connection-resiliency-in-ef-core)
20. [How to implement Soft Delete in EF Core?](#20-how-to-implement-soft-delete-in-ef-core)
21. [What are Value Conversions in EF Core?](#21-what-are-value-conversions-in-ef-core)
22. [What is Split Query in EF Core?](#22-what-is-split-query-in-ef-core)
23. [How to handle Owned Types in EF Core?](#23-how-to-handle-owned-types-in-ef-core)
24. [What are Global Query Filters?](#24-what-are-global-query-filters)
25. [What is the difference between Find() and FirstOrDefault()?](#25-what-is-the-difference-between-find-and-firstordefault)

---

## 1. What is Entity Framework Core?

### What is it?

Entity Framework Core (EF Core) is a lightweight, open-source, cross-platform Object-Relational Mapper (ORM) for .NET. It allows developers to work with databases using strongly-typed .NET objects instead of writing raw SQL queries.

### Why do we use it?

EF Core eliminates repetitive ADO.NET code, reduces development time, and provides type safety with compile-time checking. It automatically handles database connections, SQL generation, and object mapping, allowing developers to focus on business logic rather than data access plumbing. It also provides built-in features like migrations, change tracking, and LINQ support.

### When to use it?

Use EF Core in applications where you want rapid development with strong typing, automatic migrations, and complex object relationships. It's ideal for enterprise applications, web APIs, and projects where database schema evolves over time. Avoid it for high-performance scenarios where raw SQL or micro-ORMs like Dapper are better suited.

### Example

```csharp
// Without EF Core - Raw ADO.NET
using (SqlConnection conn = new SqlConnection(connectionString))
{
    SqlCommand cmd = new SqlCommand("SELECT * FROM Employees WHERE Id = @Id", conn);
    cmd.Parameters.AddWithValue("@Id", employeeId);
    conn.Open();
    SqlDataReader reader = cmd.ExecuteReader();
    // Manual mapping...
}

// With EF Core - Clean and Simple
using (var context = new AppDbContext())
{
    var employee = context.Employees.FirstOrDefault(e => e.Id == employeeId);
}
```

---

## 2. What is DbContext and DbSet in EF Core?

### What is it?

**DbContext** is the primary class that represents a session with the database in EF Core. It manages database connections, tracks entity changes, and provides APIs for querying and saving data. **DbSet** represents a collection of entities (like a table) that can be queried and modified.

### Why do we use it?

DbContext provides a centralized place to configure database connections, entity mappings, and behavior. DbSet abstracts table operations, allowing you to query and modify data using LINQ without writing SQL. Together, they simplify database operations and provide automatic change tracking and transaction management.

### When to use it?

Use DbContext as a unit of work for related operations - create one instance per request in web applications. DbSet is used whenever you need to query or modify a specific entity type. Always dispose DbContext properly using `using` statements to release database connections.

### Example

```csharp
public class AppDbContext : DbContext
{
    // DbSet represents the Employees table
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=CompanyDB;Trusted_Connection=True;");
    }
}

// Usage
using (var context = new AppDbContext())
{
    // Query all employees
    var employees = context.Employees.ToList();
    
    // Add new employee
    context.Employees.Add(new Employee { Name = "John Doe", Salary = 50000 });
    context.SaveChanges();
}
```

---

## 3. Code First vs Database First Approach

### What is it?

**Code First** starts with C# entity classes, and EF Core generates the database schema from code. **Database First** starts with an existing database, and EF Core scaffolds entity classes from the database schema.

### Why do we use it?

Code First gives developers full control over the domain model with version-controlled migrations, ideal for agile development and domain-driven design. Database First is practical when working with legacy databases or when DBAs control the database design, allowing quick integration with existing systems.

### When to use it?

Use Code First for new projects, greenfield development, or when your team prefers code-over-configuration and wants migration history in source control. Use Database First when integrating with existing databases, working with legacy systems, or when database design is managed separately by DBAs.

### Comparison Table

| **Aspect** | **Code First** | **Database First** |
|------------|----------------|-------------------|
| **Starting Point** | Start by writing C# entity classes, database schema is generated from code | Start with existing database, generate entity classes from database schema |
| **Control** | Full control over entity classes and relationships in code | Database structure controls the entity model |
| **Migrations** | Use migrations to evolve database schema over time | Regenerate models when database schema changes |
| **Use Case** | New projects, greenfield development, domain-driven design | Legacy systems, existing databases, database-centric teams |
| **Flexibility** | Easy to refactor code and propagate changes to database | Changes to database require model regeneration |
| **Version Control** | Easy to track schema changes through migration files | Database changes harder to track in version control |

### Example - Code First

```csharp
// Define entity classes first
public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Order> Orders { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}

// DbContext
public class ShopDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
}

// Run migrations to create database
// dotnet ef migrations add InitialCreate
// dotnet ef database update
```

### Example - Database First

```bash
# Scaffold models from existing database
dotnet ef dbcontext scaffold "Server=.;Database=ShopDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
```

---

## 4. What are Migrations in EF Core?

### What is it?

Migrations are version control for your database schema. They are code-based scripts that incrementally update the database structure to match your entity model changes while preserving existing data.

### Why do we use it?

Migrations enable safe, trackable database evolution without losing data. They maintain a history of schema changes in source control, making it easy to apply updates across development, staging, and production environments. This ensures all team members and environments stay synchronized with the latest database structure.

### When to use it?

Use migrations whenever you modify entity classes (add/remove properties, change relationships) in a Code First approach. Apply migrations during development, CI/CD pipelines, and production deployments. Avoid migrations when using Database First or when DBAs manage schema changes manually.

### Example

```csharp
// Initial Model
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
}

// Add initial migration
// dotnet ef migrations add InitialCreate
// dotnet ef database update

// Later, add new property
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }  // New property
    public decimal Salary { get; set; }  // New property
}

// Create migration for changes
// dotnet ef migrations add AddEmailAndSalaryToEmployee
// dotnet ef database update

// Roll back if needed
// dotnet ef database update InitialCreate
```

---

## 5. What is Change Tracking in EF Core?

### What is it?

Change Tracking is EF Core's mechanism to monitor changes made to entity instances after they're loaded from the database. It creates snapshots of original values and compares them with current values to detect modifications, additions, and deletions.

### Why do we use it?

Change Tracking enables EF Core to automatically generate the correct INSERT, UPDATE, or DELETE SQL statements when you call `SaveChanges()`. You don't need to explicitly tell EF Core what changed - it figures it out automatically, significantly reducing boilerplate code and potential errors.

### When to use it?

Change tracking is enabled by default for all queries and is essential for update operations. Disable it with `AsNoTracking()` for read-only scenarios where you don't plan to modify data, as tracking consumes memory and CPU resources.

### Example

```csharp
using (var context = new AppDbContext())
{
    // Entity is tracked when retrieved from database
    var employee = context.Employees.FirstOrDefault(e => e.Id == 1);
    Console.WriteLine(context.Entry(employee).State); // Output: Unchanged
    
    // Modify the entity
    employee.Salary = 60000;
    Console.WriteLine(context.Entry(employee).State); // Output: Modified
    
    // Add new entity
    var newEmployee = new Employee { Name = "Jane Doe", Salary = 55000 };
    context.Employees.Add(newEmployee);
    Console.WriteLine(context.Entry(newEmployee).State); // Output: Added
    
    // Delete entity
    context.Employees.Remove(employee);
    Console.WriteLine(context.Entry(employee).State); // Output: Deleted
    
    // Save all changes - EF Core generates appropriate SQL
    context.SaveChanges();
}
```

---

## 6. Lazy Loading vs Eager Loading vs Explicit Loading

### What is it?

These are three strategies for loading related entities in EF Core:
- **Lazy Loading**: Related data loads automatically when accessed (requires `virtual` navigation properties)
- **Eager Loading**: Related data loads immediately with the main query using `Include()`
- **Explicit Loading**: Related data loads on-demand when explicitly requested using `Load()`

### Why do we use it?

Each strategy optimizes different scenarios. Eager loading prevents N+1 problems when you always need related data. Lazy loading simplifies code when related data is rarely needed. Explicit loading provides fine-grained control for conditional loading based on business logic.

### When to use it?

Use **Eager Loading** when you know you'll need the related data (most common). Use **Lazy Loading** for convenience when related data is optional and accessed unpredictably. Use **Explicit Loading** when you need conditional logic to decide whether to load related data.

### Comparison Table

| **Aspect** | **Lazy Loading** | **Eager Loading** | **Explicit Loading** |
|------------|------------------|-------------------|---------------------|
| **Loading Time** | Related data loaded only when accessed (on-demand) | Related data loaded immediately with main query | Related data loaded explicitly when requested |
| **Query Count** | Multiple queries (can cause N+1 problem) | Single query with JOINs | Separate queries when explicitly called |
| **Performance** | Can be slower due to multiple database trips | Faster for scenarios needing all related data | Control over what and when to load |
| **Syntax** | Automatic (requires proxies and virtual properties) | Uses `.Include()` method | Uses `.Entry().Collection().Load()` or `.Reference().Load()` |
| **Use Case** | When related data rarely needed | When related data always needed | When conditionally loading related data |

### Example

```csharp
// Lazy Loading (requires Microsoft.EntityFrameworkCore.Proxies)
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<Employee> Employees { get; set; }  // virtual keyword required
}

using (var context = new AppDbContext())
{
    var dept = context.Departments.FirstOrDefault(d => d.Id == 1);
    // Employees loaded only when accessed
    var employeeCount = dept.Employees.Count;  // Triggers separate query
}

// Eager Loading
using (var context = new AppDbContext())
{
    // Load department with employees in single query
    var dept = context.Departments
        .Include(d => d.Employees)
        .FirstOrDefault(d => d.Id == 1);
    var employeeCount = dept.Employees.Count;  // No additional query
}

// Explicit Loading
using (var context = new AppDbContext())
{
    var dept = context.Departments.FirstOrDefault(d => d.Id == 1);
    
    // Explicitly load employees when needed
    context.Entry(dept)
        .Collection(d => d.Employees)
        .Load();
    
    var employeeCount = dept.Employees.Count;
}
```

---

## 7. What are Tracking vs No-Tracking Queries?

### What is it?

**Tracking queries** (default) make EF Core monitor entity changes by creating snapshots for change detection. **No-Tracking queries** retrieve data without change tracking, using `AsNoTracking()` for better performance.

### Why do we use it?

Tracking enables automatic update detection but consumes memory and CPU. No-tracking queries are 30-50% faster for read-only operations since EF Core skips snapshot creation and identity resolution. Use the right approach based on whether you need to update the data.

### When to use it?

Use **Tracking** when you plan to modify and save entities. Use **No-Tracking** for read-only scenarios like displaying lists, reports, dashboards, or API GET endpoints. No-tracking is critical for performance in high-traffic read operations.

### Example

```csharp
// Tracking Query (default)
using (var context = new AppDbContext())
{
    var employee = context.Employees.FirstOrDefault(e => e.Id == 1);
    Console.WriteLine(context.Entry(employee).State); // Unchanged
    
    employee.Salary = 65000;
    context.SaveChanges(); // Works - entity is tracked
}

// No-Tracking Query (better performance for read-only)
using (var context = new AppDbContext())
{
    var employees = context.Employees
        .AsNoTracking()
        .Where(e => e.Department == "IT")
        .ToList();
    
    // Good for displaying data in reports or grids
    // Cannot track changes or save updates
}

// Configure no-tracking globally
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder
        .UseSqlServer(connectionString)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
```

---

## 8. How does LINQ work with EF Core?

### What is it?

LINQ (Language Integrated Query) in EF Core allows you to write database queries using C# syntax. EF Core translates LINQ expressions into SQL and executes them against the database when the query materializes (using `ToList()`, `FirstOrDefault()`, etc.).

### Why do we use it?

LINQ provides IntelliSense, compile-time type safety, and refactoring support that raw SQL can't offer. It prevents SQL injection vulnerabilities, reduces syntax errors, and allows you to write database queries using familiar C# constructs. Query translation happens automatically, optimizing developer productivity.

### When to use it?

Use LINQ for all standard database queries in EF Core. It's ideal for filtering, sorting, joining, grouping, and projections. For complex database-specific operations or stored procedures, fall back to raw SQL using `FromSqlRaw()`.

### Example

```csharp
using (var context = new AppDbContext())
{
    // Method Syntax - Most Common
    var highEarners = context.Employees
        .Where(e => e.Salary > 50000)
        .OrderByDescending(e => e.Salary)
        .Select(e => new 
        { 
            e.Name, 
            e.Salary, 
            e.Department 
        })
        .Take(10)
        .ToList();
    
    // Query Syntax
    var itEmployees = (from e in context.Employees
                       where e.Department == "IT"
                       orderby e.Name
                       select e).ToList();
    
    // Complex query with joins
    var employeesWithDept = context.Employees
        .Join(context.Departments,
            emp => emp.DepartmentId,
            dept => dept.Id,
            (emp, dept) => new
            {
                EmployeeName = emp.Name,
                DepartmentName = dept.Name,
                emp.Salary
            })
        .Where(x => x.Salary > 40000)
        .ToList();
    
    // Aggregation
    var avgSalary = context.Employees
        .Where(e => e.Department == "Sales")
        .Average(e => e.Salary);
}
```

---

## 9. What is the N+1 Problem in EF Core?

### What is it?

The N+1 problem occurs when loading a parent entity executes 1 query, then accessing related child entities in a loop executes N additional queries (one per parent). For 100 parent records, this results in 101 database queries instead of 1 or 2.

### Why does it happen?

It happens when using lazy loading or not using eager loading (`Include()`) for related data. Each navigation property access triggers a separate database query, causing severe performance degradation in loops or iterations.

### How to solve it?

Use **Eager Loading** with `.Include()` to load related data in a single query with JOINs, or use **Projections** with `.Select()` to fetch only needed data. Both approaches reduce database round trips from N+1 to 1.

### Example

```csharp
// BAD - N+1 Problem
using (var context = new AppDbContext())
{
    var customers = context.Customers.ToList(); // 1 query
    
    foreach (var customer in customers)
    {
        // Each iteration executes a separate query - N queries
        Console.WriteLine($"{customer.Name}: {customer.Orders.Count} orders");
    }
    // Total: 1 + N queries (if 100 customers = 101 queries!)
}

// GOOD - Solution with Eager Loading
using (var context = new AppDbContext())
{
    var customers = context.Customers
        .Include(c => c.Orders) // Load orders with customers in single query
        .ToList(); // Only 1 query with JOIN
    
    foreach (var customer in customers)
    {
        Console.WriteLine($"{customer.Name}: {customer.Orders.Count} orders");
    }
    // Total: Only 1 query
}

// GOOD - Solution with Projection
using (var context = new AppDbContext())
{
    var customerOrders = context.Customers
        .Select(c => new
        {
            c.Name,
            OrderCount = c.Orders.Count
        })
        .ToList(); // Only 1 query
    
    foreach (var item in customerOrders)
    {
        Console.WriteLine($"{item.Name}: {item.OrderCount} orders");
    }
}
```

---

## 10. What is AsNoTracking and when to use it?

### What is it?

`AsNoTracking()` is a LINQ method that tells EF Core to retrieve entities without enabling change tracking. EF Core won't create snapshots or monitor changes, resulting in faster queries with lower memory consumption.

### Why do we use it?

No-tracking queries are 30-50% faster than tracked queries because EF Core skips snapshot creation and change detection overhead. For read-only operations like reports or displaying data, tracking is wasteful since you never call `SaveChanges()`. This significantly improves performance for high-volume read scenarios.

### When to use it?

Use `AsNoTracking()` for read-only queries: displaying lists, generating reports, exporting data, or API GET endpoints. Don't use it when you plan to update entities. It's especially important for queries returning large result sets or executed frequently.

### Example

```csharp
// Scenario: Generating monthly sales report
using (var context = new AppDbContext())
{
    // BAD - Tracking entities unnecessarily
    var salesData = context.Orders
        .Where(o => o.OrderDate.Month == DateTime.Now.Month)
        .Include(o => o.Customer)
        .Include(o => o.OrderItems)
        .ToList(); // Tracks all entities - wastes memory
    
    // GOOD - No tracking for read-only report
    var salesReport = context.Orders
        .AsNoTracking()
        .Where(o => o.OrderDate.Month == DateTime.Now.Month)
        .Include(o => o.Customer)
        .Include(o => o.OrderItems)
        .Select(o => new
        {
            o.OrderId,
            CustomerName = o.Customer.Name,
            o.TotalAmount,
            o.OrderDate
        })
        .ToList(); // Faster, uses less memory
    
    return salesReport; // For display only
}

// Scenario: API endpoint for listing products
[HttpGet]
public async Task<IActionResult> GetProducts()
{
    var products = await _context.Products
        .AsNoTracking() // No need to track for GET operations
        .Where(p => p.IsActive)
        .OrderBy(p => p.Name)
        .ToListAsync();
    
    return Ok(products);
}
```

---

## 11. How to execute Raw SQL in EF Core?

### What is it?

Raw SQL execution allows you to run SQL queries and commands directly against the database when LINQ is insufficient or for database-specific features. EF Core provides `FromSqlRaw()` for queries returning entities and `ExecuteSqlRaw()` for INSERT/UPDATE/DELETE commands.

### Why do we use it?

Raw SQL is necessary for complex queries, stored procedures, database-specific functions, or performance optimization that LINQ can't handle. It provides full control over SQL generation while still mapping results to strongly-typed entities. Always use parameterized queries to prevent SQL injection.

### When to use it?

Use raw SQL for stored procedures, complex joins, window functions, database-specific operations, or when LINQ generates inefficient queries. For routine CRUD operations, stick with LINQ. Always use `FromSqlInterpolated()` or `ExecuteSqlInterpolated()` for automatic parameterization.

### Example

```csharp
using (var context = new AppDbContext())
{
    // Query returning entities - FromSqlRaw
    var employees = context.Employees
        .FromSqlRaw("SELECT * FROM Employees WHERE Department = {0}", "IT")
        .ToList();
    
    // Query returning entities - FromSqlInterpolated (safer)
    string dept = "Sales";
    var salesEmployees = context.Employees
        .FromSqlInterpolated($"SELECT * FROM Employees WHERE Department = {dept}")
        .Where(e => e.Salary > 50000) // Can combine with LINQ
        .ToList();
    
    // Execute stored procedure returning entities
    var topEarners = context.Employees
        .FromSqlRaw("EXEC GetTopEarners @Count = {0}", 10)
        .ToList();
    
    // Execute command (INSERT, UPDATE, DELETE) - ExecuteSqlRaw
    var rowsAffected = context.Database
        .ExecuteSqlRaw("UPDATE Employees SET Salary = Salary * 1.1 WHERE Department = {0}", "IT");
    
    // Execute command - ExecuteSqlInterpolated (safer)
    string department = "HR";
    decimal increment = 1.05m;
    var updated = context.Database
        .ExecuteSqlInterpolated($"UPDATE Employees SET Salary = Salary * {increment} WHERE Department = {department}");
    
    // Execute stored procedure without return
    context.Database
        .ExecuteSqlRaw("EXEC ProcessMonthEndSalaries @Month = {0}, @Year = {1}", 12, 2023);
}
```

---

## 12. How to handle Transactions in EF Core?

### What is it?

Transactions ensure that a group of database operations execute as a single atomic unit - either all succeed or all fail together. By default, `SaveChanges()` wraps all changes in a transaction. For explicit control, use `Database.BeginTransaction()` to span multiple SaveChanges calls.

### Why do we use it?

Transactions maintain data integrity and consistency when performing multiple related operations. If any operation fails, all changes are rolled back, preventing partial updates that could corrupt data. This is critical for financial operations, order processing, or any multi-step business process.

### When to use it?

Use explicit transactions when you need multiple `SaveChanges()` calls in a single atomic operation, when combining EF Core operations with raw SQL, or when implementing complex business workflows that must succeed or fail as a unit (e.g., money transfers, order processing).

### Example

```csharp
// Scenario: Transfer money between bank accounts
using (var context = new BankDbContext())
{
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            // Debit from source account
            var sourceAccount = context.Accounts.FirstOrDefault(a => a.AccountNumber == "ACC001");
            if (sourceAccount.Balance < 1000)
                throw new InvalidOperationException("Insufficient balance");
            
            sourceAccount.Balance -= 1000;
            context.SaveChanges();
            
            // Credit to destination account
            var destAccount = context.Accounts.FirstOrDefault(a => a.AccountNumber == "ACC002");
            destAccount.Balance += 1000;
            context.SaveChanges();
            
            // Log transaction
            context.Transactions.Add(new Transaction
            {
                FromAccount = "ACC001",
                ToAccount = "ACC002",
                Amount = 1000,
                TransactionDate = DateTime.Now
            });
            context.SaveChanges();
            
            // Commit if all operations succeed
            transaction.Commit();
            Console.WriteLine("Transfer completed successfully");
        }
        catch (Exception ex)
        {
            // Rollback on any error
            transaction.Rollback();
            Console.WriteLine($"Transfer failed: {ex.Message}");
            throw;
        }
    }
}

// Scenario: Order processing with inventory update
public async Task<bool> ProcessOrderAsync(Order order)
{
    using (var transaction = await _context.Database.BeginTransactionAsync())
    {
        try
        {
            // Add order
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            
            // Update inventory
            foreach (var item in order.OrderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                product.StockQuantity -= item.Quantity;
            }
            await _context.SaveChangesAsync();
            
            // Create invoice
            var invoice = new Invoice { OrderId = order.Id, Amount = order.TotalAmount };
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }
}
```

---

## 13. What is Concurrency Handling in EF Core?

### What is it?

Concurrency handling manages conflicts when multiple users try to update the same data simultaneously. EF Core uses **optimistic concurrency** with concurrency tokens (like `[Timestamp]` or `[ConcurrencyCheck]`) to detect if data changed since it was read.

### Why do we use it?

Without concurrency control, the last update wins, potentially overwriting other users' changes silently (lost updates). Concurrency handling prevents data loss by detecting conflicts and allowing you to decide how to resolve them - merge changes, override, or prompt the user.

### When to use it?

Use concurrency handling in multi-user applications where simultaneous updates are possible, especially for critical data like inventory, financial records, or user profiles. It's essential in web applications where multiple users might edit the same record.

### Example

```csharp
// Entity with concurrency token
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int StockQuantity { get; set; }
    
    [Timestamp]  // SQL Server RowVersion
    public byte[] RowVersion { get; set; }
}

// Scenario: Two users updating same product stock
// User 1
using (var context = new AppDbContext())
{
    var product = context.Products.FirstOrDefault(p => p.ProductId == 1);
    product.StockQuantity = 100; // Current: 50, Update to 100
    
    // User 2 updates the same product here (in parallel)
    
    try
    {
        context.SaveChanges(); // May throw DbUpdateConcurrencyException
    }
    catch (DbUpdateConcurrencyException ex)
    {
        var entry = ex.Entries.Single();
        var databaseValues = entry.GetDatabaseValues();
        var clientValues = entry.CurrentValues;
        
        // Handle conflict
        Console.WriteLine("Concurrency conflict detected!");
        Console.WriteLine($"Your value: {clientValues["StockQuantity"]}");
        Console.WriteLine($"Database value: {databaseValues["StockQuantity"]}");
        
        // Option 1: Override with client values
        entry.OriginalValues.SetValues(databaseValues);
        context.SaveChanges();
        
        // Option 2: Refresh with database values
        // entry.Reload();
        
        // Option 3: Show user and let them decide
        // return conflict to UI for resolution
    }
}

// Configure concurrency token in fluent API
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Product>()
        .Property(p => p.StockQuantity)
        .IsConcurrencyToken(); // Any change to this property will trigger concurrency check
}
```

---

## 14. EF Core vs Dapper - Key Differences

### What is it?

**EF Core** is a full-featured ORM with change tracking, LINQ, and migrations. **Dapper** is a lightweight micro-ORM that maps SQL query results to objects with minimal overhead, close to raw ADO.NET performance.

### Why do we use them?

EF Core simplifies complex applications with automatic SQL generation, migrations, and relationship management, reducing development time for CRUD operations. Dapper offers superior performance for read-heavy scenarios and gives full control over SQL, making it ideal for complex queries and performance-critical operations.

### When to use what?

Use **EF Core** for complex domain models, rapid development, teams unfamiliar with SQL, and applications requiring migrations. Use **Dapper** for high-performance scenarios, microservices, simple CRUD with existing SQL, or reporting queries. Many projects use both - EF Core for writes, Dapper for reads.

### Comparison Table

| **Aspect** | **EF Core** | **Dapper** |
|------------|-------------|------------|
| **Type** | Full-featured ORM with change tracking and migrations | Micro-ORM, thin wrapper over ADO.NET |
| **Performance** | Slower due to change tracking and feature overhead | Faster, minimal overhead, close to raw ADO.NET |
| **Learning Curve** | Steeper, requires understanding of ORM concepts | Easier, straightforward SQL mapping |
| **Features** | LINQ queries, migrations, change tracking, lazy loading | Manual SQL queries, simple object mapping |
| **Code Generation** | Generates SQL from LINQ expressions | Write SQL manually |
| **Use Case** | Complex applications with relationships and business logic | High-performance scenarios, simple CRUD, legacy SQL |
| **Query Control** | Less control over generated SQL | Full control over SQL queries |
| **Maintenance** | Easier for complex domains with auto-migrations | Requires manual SQL maintenance |

### Example

```csharp
// EF Core Approach
using (var context = new AppDbContext())
{
    // LINQ query - EF Core generates SQL
    var employees = context.Employees
        .Include(e => e.Department)
        .Where(e => e.Salary > 50000)
        .OrderBy(e => e.Name)
        .ToList();
    
    // Change tracking and update
    var employee = context.Employees.Find(1);
    employee.Salary = 60000;
    context.SaveChanges(); // Auto-generates UPDATE statement
}

// Dapper Approach
using (var connection = new SqlConnection(connectionString))
{
    // Manual SQL query
    var sql = @"SELECT e.*, d.* 
                FROM Employees e 
                INNER JOIN Departments d ON e.DepartmentId = d.Id 
                WHERE e.Salary > @MinSalary 
                ORDER BY e.Name";
    
    var employees = connection.Query<Employee, Department, Employee>(
        sql,
        (employee, department) => 
        {
            employee.Department = department;
            return employee;
        },
        new { MinSalary = 50000 },
        splitOn: "Id"
    ).ToList();
    
    // Manual update
    var updateSql = "UPDATE Employees SET Salary = @Salary WHERE Id = @Id";
    connection.Execute(updateSql, new { Id = 1, Salary = 60000 });
}

// When to use what?
// Use EF Core: Complex domains, rapid development, team unfamiliar with SQL, need migrations
// Use Dapper: Performance-critical, existing complex SQL, simple CRUD, microservices
// Use Both: EF Core for writes/complex operations, Dapper for read-heavy/reporting queries
```

---

## 15. Performance Optimization Techniques in EF Core

### What is it?

Performance optimization involves strategies to minimize database round trips, reduce data transfer, and improve query efficiency. Key techniques include no-tracking queries, eager loading, projections, compiled queries, and split queries.

### Why do we use it?

Poor EF Core performance often stems from N+1 problems, unnecessary change tracking, and loading entire entities when only a few columns are needed. Optimization techniques can improve query performance by 50-90%, reduce memory consumption, and improve application scalability.

### When to use it?

Apply optimizations when profiling reveals performance bottlenecks, when working with large datasets, or in high-traffic scenarios. Always measure before and after optimization. Common hot spots include frequently executed queries, report generation, and data export operations.

### Example

```csharp
// BAD - Multiple Performance Issues
public List<EmployeeDto> GetEmployees()
{
    using (var context = new AppDbContext())
    {
        var employees = context.Employees.ToList(); // Loads all columns with tracking
        var result = new List<EmployeeDto>();
        
        foreach (var emp in employees) // N+1 problem
        {
            result.Add(new EmployeeDto
            {
                Name = emp.Name,
                DepartmentName = emp.Department.Name, // Separate query for each
                OrderCount = emp.Orders.Count // Separate query for each
            });
        }
        return result;
    }
}

// GOOD - Optimized Version
public async Task<List<EmployeeDto>> GetEmployeesOptimizedAsync()
{
    using (var context = new AppDbContext())
    {
        return await context.Employees
            .AsNoTracking() // No change tracking needed
            .Include(e => e.Department) // Eager loading
            .Include(e => e.Orders) // Eager loading
            .Select(e => new EmployeeDto // Projection - only needed data
            {
                Name = e.Name,
                DepartmentName = e.Department.Name,
                OrderCount = e.Orders.Count
            })
            .ToListAsync(); // Async operation
    }
    // Single query, minimal data transfer, no tracking
}

// Compiled Query for frequently executed queries
private static readonly Func<AppDbContext, int, Task<Employee>> _getEmployeeById =
    EF.CompileAsyncQuery((AppDbContext context, int id) =>
        context.Employees
            .Include(e => e.Department)
            .FirstOrDefault(e => e.Id == id));

public async Task<Employee> GetEmployeeByIdOptimized(int id)
{
    using (var context = new AppDbContext())
    {
        return await _getEmployeeById(context, id); // Uses compiled query
    }
}

// Split Query for multiple collections (EF Core 5+)
var customers = await context.Customers
    .Include(c => c.Orders)
    .Include(c => c.Addresses)
    .AsSplitQuery() // Separate queries for each collection to avoid cartesian explosion
    .ToListAsync();

// Batch operations
public async Task BulkInsertEmployees(List<Employee> employees)
{
    using (var context = new AppDbContext())
    {
        await context.Employees.AddRangeAsync(employees); // Batch insert
        await context.SaveChangesAsync(); // Single database round trip
    }
}
```

---

## 16. What are Navigation Properties?

### What is it?

Navigation properties are properties on entity classes that reference related entities, representing foreign key relationships. They can be **reference navigation properties** (single related entity) or **collection navigation properties** (multiple related entities).

### Why do we use it?

Navigation properties allow you to navigate from one entity to related entities using C# object references instead of writing JOIN queries. EF Core uses them to automatically generate foreign keys, understand relationships, and create efficient JOIN queries when needed.

### When to use it?

Define navigation properties whenever you have relationships between entities (one-to-many, many-to-one, one-to-one, many-to-many). They're essential for loading related data with `Include()` and for maintaining referential integrity through EF Core's relationship management.

### Example

```csharp
// One-to-Many Relationship
public class Department
{
    public int DepartmentId { get; set; }
    public string Name { get; set; }
    
    // Collection navigation property - one department has many employees
    public List<Employee> Employees { get; set; }
}

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public int DepartmentId { get; set; } // Foreign key
    
    // Reference navigation property - employee belongs to one department
    public Department Department { get; set; }
}

// Many-to-Many Relationship (EF Core 5+)
public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    
    // Collection navigation property
    public List<Course> Courses { get; set; }
}

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    
    // Collection navigation property
    public List<Student> Students { get; set; }
}

// Usage - Navigate relationships
using (var context = new SchoolDbContext())
{
    // Load department with all employees
    var dept = context.Departments
        .Include(d => d.Employees) // Use navigation property
        .FirstOrDefault(d => d.DepartmentId == 1);
    
    Console.WriteLine($"Department: {dept.Name}");
    foreach (var emp in dept.Employees)
    {
        Console.WriteLine($"  - {emp.Name}");
    }
    
    // Navigate from employee to department
    var employee = context.Employees
        .Include(e => e.Department)
        .FirstOrDefault(e => e.EmployeeId == 1);
    
    Console.WriteLine($"{employee.Name} works in {employee.Department.Name}");
}
```

---

## 17. What is Include() and ThenInclude()?

### What is it?

`Include()` is used for eager loading related entities through navigation properties, loading them in the same query with SQL JOINs. `ThenInclude()` loads nested relationships - entities related to the entities loaded by `Include()`.

### Why do we use it?

Include() prevents the N+1 problem by loading related data in a single database query instead of making separate queries for each related entity. ThenInclude() extends this to multi-level relationships, efficiently loading complex object graphs without multiple database round trips.

### When to use it?

Use Include() whenever you need related data and want to avoid N+1 problems. Use ThenInclude() for nested relationships like Customer -> Orders -> OrderItems. For read-only scenarios, combine with AsNoTracking() for better performance.

### Example

```csharp
// Single level Include
using (var context = new AppDbContext())
{
    // Load customers with their orders
    var customers = context.Customers
        .Include(c => c.Orders) // Single level
        .ToList();
    
    // Generates SQL with JOIN:
    // SELECT * FROM Customers c
    // LEFT JOIN Orders o ON c.CustomerId = o.CustomerId
}

// Multi-level Include with ThenInclude
using (var context = new AppDbContext())
{
    // Load customers -> orders -> order items -> product
    var customers = context.Customers
        .Include(c => c.Orders)              // Level 1: Orders of Customer
            .ThenInclude(o => o.OrderItems)  // Level 2: Items of each Order
                .ThenInclude(oi => oi.Product) // Level 3: Product of each Item
        .Include(c => c.Address)             // Also include customer address
        .ToList();
    
    // Access nested data without additional queries
    foreach (var customer in customers)
    {
        Console.WriteLine($"Customer: {customer.Name}");
        Console.WriteLine($"Address: {customer.Address.Street}");
        
        foreach (var order in customer.Orders)
        {
            Console.WriteLine($"  Order #{order.OrderId} - {order.OrderDate}");
            
            foreach (var item in order.OrderItems)
            {
                Console.WriteLine($"    - {item.Product.Name}: ${item.Price}");
            }
        }
    }
}

// Multiple ThenInclude branches
var employees = context.Employees
    .Include(e => e.Department)
        .ThenInclude(d => d.Manager)
    .Include(e => e.Projects)
        .ThenInclude(p => p.Client)
    .ToList();

// Filtering with Include (EF Core 5+)
var departments = context.Departments
    .Include(d => d.Employees.Where(e => e.IsActive))
    .ToList();
```

---

## 18. What are Shadow Properties in EF Core?

### What is it?

Shadow properties exist in the EF Core model and database but don't exist as CLR properties on entity classes. They're defined in model configuration and can only be accessed through the ChangeTracker API.

### Why do we use it?

Shadow properties keep domain models clean by hiding infrastructure concerns like audit timestamps, foreign keys, or tenant IDs that don't belong in the domain model. They're stored in the database but don't clutter entity classes with non-business properties.

### When to use it?

Use shadow properties for audit fields (CreatedDate, ModifiedDate), foreign keys you want to hide, multi-tenancy tenant IDs, or any database column that shouldn't be exposed in the domain model. Access them via `Entry(entity).Property("PropertyName")`.

### Example

```csharp
// Entity without audit properties in class definition
public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public decimal Salary { get; set; }
    // No CreatedDate, ModifiedDate, CreatedBy properties defined
}

// Configure shadow properties
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Add shadow properties for audit
    modelBuilder.Entity<Employee>()
        .Property<DateTime>("CreatedDate");
    
    modelBuilder.Entity<Employee>()
        .Property<DateTime>("ModifiedDate");
    
    modelBuilder.Entity<Employee>()
        .Property<string>("CreatedBy");
    
    // Foreign key as shadow property
    modelBuilder.Entity<Order>()
        .Property<int>("CustomerId");
}

// Set shadow property values
public override int SaveChanges()
{
    var entries = ChangeTracker.Entries()
        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
    
    foreach (var entry in entries)
    {
        if (entry.State == EntityState.Added)
        {
            entry.Property("CreatedDate").CurrentValue = DateTime.Now;
            entry.Property("CreatedBy").CurrentValue = _currentUser;
        }
        
        if (entry.State == EntityState.Modified)
        {
            entry.Property("ModifiedDate").CurrentValue = DateTime.Now;
        }
    }
    
    return base.SaveChanges();
}

// Query using shadow properties
using (var context = new AppDbContext())
{
    var recentEmployees = context.Employees
        .Where(e => EF.Property<DateTime>(e, "CreatedDate") > DateTime.Now.AddDays(-30))
        .ToList();
    
    // Read shadow property value
    var employee = context.Employees.First();
    var createdDate = context.Entry(employee).Property("CreatedDate").CurrentValue;
    Console.WriteLine($"Created: {createdDate}");
}
```

---

## 19. What is Connection Resiliency in EF Core?

### What is it?

Connection resiliency (retry logic) is EF Core's ability to automatically retry failed database operations due to transient errors like network issues, timeouts, or temporary database unavailability.

### Why do we use it?

Cloud databases and network connections can experience temporary failures. Without retry logic, your application would fail on these transient errors, requiring manual intervention or application restarts. Connection resiliency automatically handles these temporary issues, improving application reliability and user experience.

### When to use it?

Enable connection resiliency when using cloud databases (Azure SQL, AWS RDS), distributed systems, or unreliable network connections. Configure it with `EnableRetryOnFailure()` specifying max retry count and delay. Essential for production applications where high availability is required.

### Example

```csharp
// Configure connection resiliency in DbContext
public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            connectionString,
            options => options.EnableRetryOnFailure(
                maxRetryCount: 5,              // Retry up to 5 times
                maxRetryDelay: TimeSpan.FromSeconds(30), // Max 30 seconds between retries
                errorNumbersToAdd: null        // Additional SQL error numbers to retry
            ));
    }
}

// Configure in ASP.NET Core Startup/Program.cs
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions
            .EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null)));

// Using execution strategy manually
using (var context = new AppDbContext())
{
    var strategy = context.Database.CreateExecutionStrategy();
    
    strategy.Execute(() =>
    {
        using (var transaction = context.Database.BeginTransaction())
        {
            try
            {
                // Perform operations
                context.Employees.Add(new Employee { Name = "John" });
                context.SaveChanges();
                
                context.Orders.Add(new Order { OrderDate = DateTime.Now });
                context.SaveChanges();
                
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    });
}

// Async version
await strategy.ExecuteAsync(async () =>
{
    using (var transaction = await context.Database.BeginTransactionAsync())
    {
        try
        {
            await context.Employees.AddAsync(new Employee { Name = "Jane" });
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
});
```

---

## 20. How to implement Soft Delete in EF Core?

### What is it?

Soft delete is a pattern where records are marked as deleted (using an `IsDeleted` flag or `DeletedDate` timestamp) rather than physically removed from the database. Data remains in the database but is hidden from normal queries.

### Why do we use it?

Soft delete preserves historical data, enables data recovery, maintains referential integrity, supports audit requirements, and prevents accidental data loss. It's essential for compliance, reporting, and scenarios where "deleted" data may need restoration.

### When to use it?

Use soft delete for business-critical data, user-generated content, financial records, or when regulatory compliance requires data retention. Implement it with global query filters so deleted records are automatically excluded from queries unless explicitly requested with `IgnoreQueryFilters()`.

### Example

```csharp
// Add soft delete property to entities
public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public decimal Salary { get; set; }
    public bool IsDeleted { get; set; } // Soft delete flag
    public DateTime? DeletedDate { get; set; }
}

// Configure global query filter
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Apply to all entities with IsDeleted property
    modelBuilder.Entity<Employee>()
        .HasQueryFilter(e => !e.IsDeleted); // Automatically filter out deleted records
    
    modelBuilder.Entity<Department>()
        .HasQueryFilter(d => !d.IsDeleted);
}

// Override SaveChanges to implement soft delete
public override int SaveChanges()
{
    foreach (var entry in ChangeTracker.Entries()
        .Where(e => e.State == EntityState.Deleted))
    {
        // Check if entity has IsDeleted property
        var isDeletedProperty = entry.Entity.GetType().GetProperty("IsDeleted");
        if (isDeletedProperty != null)
        {
            // Soft delete: change state to Modified instead of Deleted
            entry.State = EntityState.Modified;
            entry.CurrentValues["IsDeleted"] = true;
            entry.CurrentValues["DeletedDate"] = DateTime.Now;
        }
    }
    
    return base.SaveChanges();
}

// Usage - appears as normal delete to application code
using (var context = new AppDbContext())
{
    var employee = context.Employees.Find(1);
    context.Employees.Remove(employee); // Soft delete
    context.SaveChanges();
    
    // Employee not returned in normal queries
    var employees = context.Employees.ToList(); // Excludes soft-deleted
    
    // Explicitly query deleted records
    var deletedEmployees = context.Employees
        .IgnoreQueryFilters() // Bypass global filter
        .Where(e => e.IsDeleted)
        .ToList();
    
    // Restore deleted record
    var deletedEmployee = context.Employees
        .IgnoreQueryFilters()
        .FirstOrDefault(e => e.EmployeeId == 1);
    deletedEmployee.IsDeleted = false;
    deletedEmployee.DeletedDate = null;
    context.SaveChanges();
}

// Create base entity for all soft-deletable entities
public abstract class SoftDeletableEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
    public string DeletedBy { get; set; }
}

public class Employee : SoftDeletableEntity
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
}
```

---

## 21. What are Value Conversions in EF Core?

### What is it?

Value conversions transform property values when reading from or writing to the database. They enable storing enums as strings, encrypting data, converting complex types to JSON, or mapping value objects to database columns.

### Why do we use it?

Value conversions handle type mismatches between your domain model and database schema without cluttering entity classes with database-specific logic. They provide clean separation between how data is represented in code versus how it's stored in the database.

### When to use it?

Use value conversions for storing enums as strings for readability, encrypting sensitive data, persisting value objects, converting DateTimeOffset to UTC, or storing complex objects as JSON. Configure them with `HasConversion()` in OnModelCreating.

### Example

```csharp
// Enum stored as string
public enum EmployeeStatus
{
    Active,
    OnLeave,
    Terminated
}

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public EmployeeStatus Status { get; set; } // Stored as string in DB
    public DateTime DateOfBirth { get; set; }
}

// Configure value conversions
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Convert enum to string
    modelBuilder.Entity<Employee>()
        .Property(e => e.Status)
        .HasConversion<string>(); // Store as "Active", "OnLeave", "Terminated"
    
    // Convert DateTime to DateOnly (store only date, not time)
    modelBuilder.Entity<Employee>()
        .Property(e => e.DateOfBirth)
        .HasConversion(
            v => v.Date, // To database: remove time component
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // From database
    
    // Custom conversion - encrypt/decrypt
    modelBuilder.Entity<Employee>()
        .Property(e => e.SocialSecurityNumber)
        .HasConversion(
            v => Encrypt(v),   // To database: encrypt
            v => Decrypt(v));  // From database: decrypt
    
    // Convert complex object to JSON
    modelBuilder.Entity<Employee>()
        .Property(e => e.Address)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<Address>(v, (JsonSerializerOptions)null));
}

// Value Object conversion
public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }
    
    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }
}

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public Money Price { get; set; } // Value object
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Convert Money value object to decimal in database
    modelBuilder.Entity<Product>()
        .Property(p => p.Price)
        .HasConversion(
            v => v.Amount,                    // To database: store only amount
            v => new Money(v, "USD"));        // From database: reconstruct with default currency
}

// Usage - works transparently
using (var context = new AppDbContext())
{
    var employee = new Employee
    {
        Name = "John Doe",
        Status = EmployeeStatus.Active, // Stored as "Active" in DB
        DateOfBirth = new DateTime(1990, 5, 15, 10, 30, 0) // Time removed in DB
    };
    
    context.Employees.Add(employee);
    context.SaveChanges();
    
    // Query by enum value
    var activeEmployees = context.Employees
        .Where(e => e.Status == EmployeeStatus.Active)
        .ToList();
}
```

---

## 22. What is Split Query in EF Core?

### What is it?

Split Query (EF Core 5+) executes separate SQL queries for each `Include()` instead of a single query with multiple JOINs. It splits one LINQ query into multiple database queries to avoid cartesian explosion.

### Why do we use it?

When including multiple collections, a single query with JOINs creates cartesian explosion - dramatically increasing result set size due to row multiplication. If a customer has 10 orders and 3 addresses, a single query returns 30 rows instead of 14. Split queries eliminate this data duplication.

### When to use it?

Use split queries (`.AsSplitQuery()`) when including multiple collections that cause cartesian explosion, significantly reducing data transfer. Use single queries (default) when including only reference navigation properties or when you need a consistent snapshot across all data.

### Example

```csharp
// Problem: Cartesian Explosion with Single Query
public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public List<Order> Orders { get; set; }       // Collection 1
    public List<Address> Addresses { get; set; }  // Collection 2
}

using (var context = new AppDbContext())
{
    // Single Query (default) - Cartesian Explosion
    var customers = context.Customers
        .Include(c => c.Orders)
        .Include(c => c.Addresses)
        .ToList();
    
    // SQL: SELECT * FROM Customers c
    //      LEFT JOIN Orders o ON c.Id = o.CustomerId
    //      LEFT JOIN Addresses a ON c.Id = a.CustomerId
    // If customer has 10 orders and 3 addresses, returns 30 rows (10 * 3)!
}

// Solution: Split Query
using (var context = new AppDbContext())
{
    var customers = context.Customers
        .Include(c => c.Orders)
        .Include(c => c.Addresses)
        .AsSplitQuery() // Use split query
        .ToList();
    
    // SQL 1: SELECT * FROM Customers
    // SQL 2: SELECT * FROM Orders WHERE CustomerId IN (...)
    // SQL 3: SELECT * FROM Addresses WHERE CustomerId IN (...)
    // Total rows: 1 + 10 + 3 = 14 rows instead of 30!
}

// Configure globally for all queries
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder
        .UseSqlServer(connectionString)
        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
}

// Complex scenario with nested includes
var departments = context.Departments
    .Include(d => d.Employees)
        .ThenInclude(e => e.Projects)
    .Include(d => d.Employees)
        .ThenInclude(e => e.Skills)
    .Include(d => d.Manager)
    .AsSplitQuery() // Prevents massive cartesian explosion
    .ToList();

// Force single query when needed
var customers = context.Customers
    .Include(c => c.Orders)
    .AsSingleQuery() // Override global split query setting
    .ToList();

// Trade-offs:
// Single Query: One database round-trip, potential cartesian explosion, consistent snapshot
// Split Query: Multiple round-trips, less data duplication, potential inconsistency between queries
```

---

## 23. How to handle Owned Types in EF Core?

### What is it?

Owned types (value objects) are types without their own identity that are owned by another entity. They're always accessed through their owner and share the owner's identity. Examples include Address, Money, or DateRange.

### Why do we use it?

Owned types allow grouping related properties in your domain model while storing them in the same table or separate table. They improve code organization, enable reusability, and better represent domain concepts that don't need separate identities or primary keys.

### When to use it?

Use owned types for value objects like Address, Money, PhoneNumber, or DateRange that don't need separate database tables or independent lifecycle management. Configure with `OwnsOne()` for single owned types or `OwnsMany()` for collections.

### Example

```csharp
// Owned Type - Address
public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
}

// Entity that owns Address
public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public Address ShippingAddress { get; set; } // Owned type
    public Address BillingAddress { get; set; }  // Owned type
}

// Configure owned types
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // OwnsOne - stores in same table with prefix
    modelBuilder.Entity<Customer>()
        .OwnsOne(c => c.ShippingAddress, sa =>
        {
            sa.Property(a => a.Street).HasColumnName("ShippingStreet");
            sa.Property(a => a.City).HasColumnName("ShippingCity");
            sa.Property(a => a.State).HasColumnName("ShippingState");
            sa.Property(a => a.ZipCode).HasColumnName("ShippingZipCode");
            sa.Property(a => a.Country).HasColumnName("ShippingCountry");
        });
    
    modelBuilder.Entity<Customer>()
        .OwnsOne(c => c.BillingAddress, ba =>
        {
            ba.Property(a => a.Street).HasColumnName("BillingStreet");
            ba.Property(a => a.City).HasColumnName("BillingCity");
            // ... similar configuration
        });
    
    // Store owned type in separate table
    modelBuilder.Entity<Order>()
        .OwnsOne(o => o.DeliveryAddress, da =>
        {
            da.ToTable("OrderDeliveryAddresses");
        });
}

// OwnsMany - collection of owned types
public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public List<PhoneNumber> PhoneNumbers { get; set; } // Collection of owned types
}

public class PhoneNumber
{
    public string Number { get; set; }
    public string Type { get; set; } // Mobile, Home, Work
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Employee>()
        .OwnsMany(e => e.PhoneNumbers, pn =>
        {
            pn.Property(p => p.Number).IsRequired();
            pn.Property(p => p.Type).HasMaxLength(20);
        });
}

// Usage
using (var context = new AppDbContext())
{
    var customer = new Customer
    {
        Name = "John Doe",
        ShippingAddress = new Address
        {
            Street = "123 Main St",
            City = "Seattle",
            State = "WA",
            ZipCode = "98101",
            Country = "USA"
        },
        BillingAddress = new Address
        {
            Street = "456 Oak Ave",
            City = "Portland",
            State = "OR",
            ZipCode = "97201",
            Country = "USA"
        }
    };
    
    context.Customers.Add(customer);
    context.SaveChanges();
    
    // Query
    var customers = context.Customers
        .Where(c => c.ShippingAddress.City == "Seattle")
        .ToList();
}
```

---

## 24. What are Global Query Filters?

### What is it?

Global Query Filters are model-level filters automatically applied to all queries for a specific entity type. They're defined once in `OnModelCreating()` using `HasQueryFilter()` and automatically added to every LINQ query.

### Why do we use it?

Global filters eliminate repetitive WHERE clauses across your codebase. Instead of manually filtering by tenant ID or IsDeleted in every query, the filter applies automatically. This prevents bugs from forgetting filters and implements cross-cutting concerns like soft delete or multi-tenancy consistently.

### When to use it?

Use global filters for multi-tenancy (filter by tenant ID), soft deletes (exclude deleted records), security (filter by user permissions), or any filter that should apply to all queries. Bypass filters when needed using `IgnoreQueryFilters()`.

### Example

```csharp
// Multi-tenant application scenario
public class Order
{
    public int OrderId { get; set; }
    public string Description { get; set; }
    public int TenantId { get; set; }  // Every record belongs to a tenant
    public bool IsDeleted { get; set; } // Soft delete flag
}

// Current tenant service
public interface ITenantService
{
    int GetCurrentTenantId();
}

public class AppDbContext : DbContext
{
    private readonly ITenantService _tenantService;
    
    public AppDbContext(DbContextOptions options, ITenantService tenantService)
        : base(options)
    {
        _tenantService = tenantService;
    }
    
    public DbSet<Order> Orders { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Global filter for multi-tenancy AND soft delete
        modelBuilder.Entity<Order>()
            .HasQueryFilter(o => 
                o.TenantId == _tenantService.GetCurrentTenantId() && 
                !o.IsDeleted);
        
        // Filter for user-specific data
        modelBuilder.Entity<Document>()
            .HasQueryFilter(d => d.IsPublic || d.OwnerId == _currentUserId);
        
        // Filter for active records
        modelBuilder.Entity<Employee>()
            .HasQueryFilter(e => e.Status == EmployeeStatus.Active);
    }
}

// Usage - filters applied automatically
using (var context = new AppDbContext(options, tenantService))
{
    // Automatically filters by current tenant and non-deleted
    var orders = context.Orders.ToList();
    // SQL: SELECT * FROM Orders WHERE TenantId = @p0 AND IsDeleted = 0
    
    var order = context.Orders.Find(1);
    // Only returns if order belongs to current tenant and not deleted
    
    var specificOrder = context.Orders
        .Where(o => o.Description.Contains("urgent"))
        .FirstOrDefault();
    // Filter combined with your query:
    // WHERE TenantId = @p0 AND IsDeleted = 0 AND Description LIKE '%urgent%'
}

// Temporarily disable filters when needed
using (var context = new AppDbContext(options, tenantService))
{
    // Admin view: see all orders from all tenants including deleted
    var allOrders = context.Orders
        .IgnoreQueryFilters()
        .ToList();
    
    // See deleted orders only
    var deletedOrders = context.Orders
        .IgnoreQueryFilters()
        .Where(o => o.IsDeleted)
        .ToList();
}

// Complex scenario with navigation properties
public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public int TenantId { get; set; }
    public List<Order> Orders { get; set; }
}

// When you query customers, related orders are also filtered
var customers = context.Customers
    .Include(c => c.Orders) // Orders automatically filtered by tenant and IsDeleted
    .ToList();
```

---

## 25. What is the difference between Find() and FirstOrDefault()?

### What is it?

`Find()` retrieves an entity by its primary key value, checking the local context cache first before querying the database. `FirstOrDefault()` retrieves an entity based on any LINQ expression, always querying the database.

### Why use each?

Use `Find()` when searching by primary key - it's faster because it checks the context cache first, avoiding unnecessary database queries for already-loaded entities. Use `FirstOrDefault()` for flexible queries with any condition, complex filters, ordering, or when including related data.

### When to use which?

Use **Find()** for lookups by primary key, especially in update scenarios where the entity might already be tracked. Use **FirstOrDefault()** for searches by non-key properties, complex conditions, or when combining with `Include()`, `Where()`, or `OrderBy()`.

### Comparison Table

| **Aspect** | **Find()** | **FirstOrDefault()** |
|------------|-----------|---------------------|
| **Primary Use** | Retrieve entity by primary key | Retrieve entity by any condition |
| **Local Cache** | Checks local context cache first before database query | Always queries the database (unless explicit tracking check) |
| **Query Flexibility** | Only works with primary key values | Supports any LINQ expression/condition |
| **Performance** | Faster if entity already loaded in context | Requires database round-trip |
| **Syntax** | `Find(keyValue)` or `Find(key1, key2)` for composite keys | `FirstOrDefault(e => e.Property == value)` |
| **Tracking** | Always returns tracked entity | Returns tracked entity by default, can use AsNoTracking() |
| **Null Return** | Returns null if not found | Returns null if not found |

### Example

```csharp
// Find() - by primary key
using (var context = new AppDbContext())
{
    // First call - queries database
    var employee1 = context.Employees.Find(1);
    // SQL: SELECT * FROM Employees WHERE Id = 1
    
    // Second call - returns from context cache (no database query!)
    var employee2 = context.Employees.Find(1);
    // No SQL executed - returns cached instance
    
    Console.WriteLine(Object.ReferenceEquals(employee1, employee2)); // True - same instance
    
    // Composite key
    var orderItem = context.OrderItems.Find(orderId: 100, productId: 5);
}

// FirstOrDefault() - by any condition
using (var context = new AppDbContext())
{
    // Query by non-primary key property
    var employee = context.Employees
        .FirstOrDefault(e => e.Email == "john@example.com");
    // SQL: SELECT TOP 1 * FROM Employees WHERE Email = 'john@example.com'
    
    // Complex condition
    var highEarner = context.Employees
        .FirstOrDefault(e => e.Salary > 100000 && e.Department == "IT");
    
    // With Include
    var employeeWithDept = context.Employees
        .Include(e => e.Department)
        .FirstOrDefault(e => e.Id == 1);
    
    // With ordering
    var topEmployee = context.Employees
        .OrderByDescending(e => e.Salary)
        .FirstOrDefault();
}

// Performance comparison
using (var context = new AppDbContext())
{
    // Scenario 1: Load entity by ID for update
    var emp1 = context.Employees.Find(1); // Best - checks cache first
    emp1.Salary = 60000;
    
    var emp2 = context.Employees.Find(1); // No DB query - from cache
    Console.WriteLine(emp2.Salary); // 60000 (sees pending changes)
    
    context.SaveChanges();
}

using (var context = new AppDbContext())
{
    // Scenario 2: Load entity by ID for update
    var emp = context.Employees.FirstOrDefault(e => e.Id == 1); // DB query
    emp.Salary = 60000;
    
    var emp2 = context.Employees.FirstOrDefault(e => e.Id == 1); // Another DB query!
    Console.WriteLine(emp2.Salary); // 60000 (same tracked instance, but queried DB)
}

// When to use what?
// Use Find():
// - When searching by primary key
// - When entity might already be in context
// - For better performance in update scenarios

// Use FirstOrDefault():
// - When searching by non-primary key properties
// - When you need complex conditions
// - When you need to include related data
// - When you need ordering
// - For read-only scenarios with AsNoTracking()
```

---

## Additional Resources

### Common EF Core Attributes

```csharp
// Table and column mapping
[Table("tbl_Employees")]
public class Employee
{
    [Key]
    [Column("employee_id")]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; }
    
    [NotMapped] // Exclude from database
    public int Age { get; set; }
    
    [Timestamp] // Concurrency token
    public byte[] RowVersion { get; set; }
    
    [ConcurrencyCheck]
    public string LastModifiedBy { get; set; }
    
    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }
}
```

### Performance Best Practices Summary

1. **Use AsNoTracking() for read-only queries**
2. **Use Include() to avoid N+1 problems**
3. **Use projections (Select) to load only needed data**
4. **Use async methods for I/O operations**
5. **Batch operations with AddRange/UpdateRange**
6. **Use compiled queries for repeated operations**
7. **Use AsSplitQuery() for multiple collections**
8. **Disable change tracking globally for read-only contexts**
9. **Use proper database indexes**
10. **Profile queries with logging and analysis tools**

### EF Core Logging Configuration

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder
        .UseSqlServer(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging() // Show parameter values (dev only)
        .EnableDetailedErrors(); // Show detailed error info
}
```

---

**End of Document**

*This guide covers the most important Entity Framework Core interview questions and concepts. Practice these examples and understand the underlying principles to ace your .NET interviews.*
