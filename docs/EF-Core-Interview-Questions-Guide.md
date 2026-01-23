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
26. [How to configure One-to-One relationships in EF Core?](#26-how-to-configure-one-to-one-relationships-in-ef-core)
27. [How to configure One-to-Many relationships in EF Core?](#27-how-to-configure-one-to-many-relationships-in-ef-core)
28. [How to configure Many-to-Many relationships in EF Core?](#28-how-to-configure-many-to-many-relationships-in-ef-core)
29. [What is the difference between Fluent API and Data Annotations?](#29-what-is-the-difference-between-fluent-api-and-data-annotations)
30. [What is IEntityTypeConfiguration and why use it?](#30-what-is-ientitytypeconfiguration-and-why-use-it)
31. [How to implement Data Seeding in EF Core?](#31-how-to-implement-data-seeding-in-ef-core)
32. [How to implement Composite Keys in EF Core?](#32-how-to-implement-composite-keys-in-ef-core)
33. [How to implement Audit Fields (CreatedDate, UpdatedDate) in EF Core?](#33-how-to-implement-audit-fields-createddate-updateddate-in-ef-core)
34. [What is Deferred Execution in LINQ and EF Core?](#34-what-is-deferred-execution-in-linq-and-ef-core)
35. [What is the difference between IQueryable and IEnumerable in EF Core?](#35-what-is-the-difference-between-iqueryable-and-ienumerable-in-ef-core)
36. [How to handle Migrations in large teams?](#36-how-to-handle-migrations-in-large-teams)
37. [How to implement Multi-Tenant architecture in EF Core?](#37-how-to-implement-multi-tenant-architecture-in-ef-core)
38. [What are the key Performance Tuning strategies in EF Core?](#38-what-are-the-key-performance-tuning-strategies-in-ef-core)
39. [How to handle large datasets efficiently in EF Core?](#39-how-to-handle-large-datasets-efficiently-in-ef-core)
40. [What are EF Core limitations and when to use alternatives?](#40-what-are-ef-core-limitations-and-when-to-use-alternatives)

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

## 26. How to configure One-to-One relationships in EF Core?

### What is it?

A One-to-One relationship means each entity instance relates to exactly one instance of another entity. In EF Core, one side must be the principal (with the primary key) and the other is the dependent (with the foreign key). The dependent entity's foreign key also serves as its primary key, ensuring the one-to-one constraint.

### Why do we use it?

One-to-One relationships separate concerns by splitting entity data across tables while maintaining referential integrity. This is useful for optional data, sensitive information segregation, or performance optimization by keeping frequently accessed columns separate from rarely used ones.

### When to use it?

Use One-to-One when you have optional entity extensions (User and UserProfile), security concerns requiring data segregation (Employee and EmployeeSalary), or performance needs to split large tables. Also useful when modeling IS-A relationships without inheritance.

### Example

```csharp
// Principal entity
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    
    // Reference navigation to dependent
    public UserProfile Profile { get; set; }
}

// Dependent entity
public class UserProfile
{
    public int UserId { get; set; } // FK and PK
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    // Reference navigation to principal
    public User User { get; set; }
}

// Configuration in OnModelCreating
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
        .HasOne(u => u.Profile)
        .WithOne(p => p.User)
        .HasForeignKey<UserProfile>(p => p.UserId) // Specify dependent
        .OnDelete(DeleteBehavior.Cascade);
}

// Usage
using (var context = new AppDbContext())
{
    var user = new User
    {
        Username = "john_doe",
        Email = "john@example.com",
        Profile = new UserProfile
        {
            FirstName = "John",
            LastName = "Doe",
            Bio = "Software Engineer"
        }
    };
    
    context.Users.Add(user);
    context.SaveChanges();
    
    // Query with profile
    var userWithProfile = context.Users
        .Include(u => u.Profile)
        .FirstOrDefault(u => u.UserId == 1);
}
```

---

## 27. How to configure One-to-Many relationships in EF Core?

### What is it?

A One-to-Many relationship means one principal entity relates to multiple dependent entities. The dependent side contains a foreign key pointing to the principal's primary key. EF Core can infer this relationship through navigation properties or you can configure it explicitly using Fluent API.

### Why do we use it?

One-to-Many is the most common relationship pattern in databases, representing hierarchical or grouped data. It maintains referential integrity, enables efficient querying of related data, and accurately models real-world relationships like customers-orders, departments-employees, or categories-products.

### When to use it?

Use One-to-Many for parent-child relationships, master-detail scenarios, hierarchical data, or any case where multiple items belong to a single entity. Examples include blog posts with comments, orders with order items, or departments with employees.

### Example

```csharp
// Principal entity (One side)
public class Department
{
    public int DepartmentId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    
    // Collection navigation property
    public List<Employee> Employees { get; set; }
}

// Dependent entity (Many side)
public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public decimal Salary { get; set; }
    
    // Foreign key
    public int DepartmentId { get; set; }
    
    // Reference navigation property
    public Department Department { get; set; }
}

// Explicit configuration
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Department>()
        .HasMany(d => d.Employees)
        .WithOne(e => e.Department)
        .HasForeignKey(e => e.DepartmentId)
        .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
}

// Usage
using (var context = new AppDbContext())
{
    // Add department with employees
    var dept = new Department
    {
        Name = "Engineering",
        Location = "Building A",
        Employees = new List<Employee>
        {
            new Employee { Name = "Alice", Salary = 80000 },
            new Employee { Name = "Bob", Salary = 75000 }
        }
    };
    
    context.Departments.Add(dept);
    context.SaveChanges();
    
    // Query department with employees
    var department = context.Departments
        .Include(d => d.Employees)
        .FirstOrDefault(d => d.DepartmentId == 1);
}
```

---

## 28. How to configure Many-to-Many relationships in EF Core?

### What is it?

A Many-to-Many relationship means entities on both sides can relate to multiple entities on the other side. EF Core 5+ supports automatic join tables (skip navigation), while earlier versions or complex scenarios require explicit join entities. The join table contains foreign keys from both principal tables.

### Why do we use it?

Many-to-Many relationships model real-world scenarios where associations exist between multiple entities on both sides. They eliminate data redundancy, maintain normalization, and accurately represent relationships like students-courses, products-categories, or users-roles without duplicate data.

### When to use it?

Use Many-to-Many for bidirectional multi-entity relationships: students enrolling in multiple courses (and courses having multiple students), products in multiple categories, users with multiple roles, or tags associated with multiple posts.

### Example

```csharp
// EF Core 5+ Automatic Join Table (No explicit join entity)
public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    
    // Collection navigation - skip navigation
    public List<Course> Courses { get; set; }
}

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    
    // Collection navigation - skip navigation
    public List<Student> Students { get; set; }
}

// Configuration (optional - EF Core auto-configures)
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Student>()
        .HasMany(s => s.Courses)
        .WithMany(c => c.Students)
        .UsingEntity(j => j.ToTable("StudentCourses")); // Custom join table name
}

// Explicit Join Entity (for additional properties)
public class StudentCourse
{
    public int StudentId { get; set; }
    public Student Student { get; set; }
    
    public int CourseId { get; set; }
    public Course Course { get; set; }
    
    // Additional properties
    public DateTime EnrollmentDate { get; set; }
    public string Grade { get; set; }
}

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public List<StudentCourse> StudentCourses { get; set; }
}

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public List<StudentCourse> StudentCourses { get; set; }
}

// Configuration for explicit join entity
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<StudentCourse>()
        .HasKey(sc => new { sc.StudentId, sc.CourseId }); // Composite key
    
    modelBuilder.Entity<StudentCourse>()
        .HasOne(sc => sc.Student)
        .WithMany(s => s.StudentCourses)
        .HasForeignKey(sc => sc.StudentId);
    
    modelBuilder.Entity<StudentCourse>()
        .HasOne(sc => sc.Course)
        .WithMany(c => c.StudentCourses)
        .HasForeignKey(sc => sc.CourseId);
}

// Usage
using (var context = new AppDbContext())
{
    // Add student with courses
    var student = new Student
    {
        Name = "John Doe",
        Courses = new List<Course>
        {
            new Course { Title = "Mathematics" },
            new Course { Title = "Physics" }
        }
    };
    
    context.Students.Add(student);
    context.SaveChanges();
    
    // Query with related data
    var studentWithCourses = context.Students
        .Include(s => s.Courses)
        .FirstOrDefault(s => s.StudentId == 1);
}
```

---

## 29. What is the difference between Fluent API and Data Annotations?

### What is it?

Data Annotations are attributes applied directly to entity classes for configuration, while Fluent API is a code-based configuration approach using method chaining in `OnModelCreating()`. Fluent API is more powerful and takes precedence over Data Annotations. Both configure entity mappings, relationships, constraints, and database schema.

### Why do we use them?

Data Annotations keep configuration close to entity definitions and are simpler for basic scenarios. Fluent API separates configuration from entity classes (cleaner domain models), supports advanced scenarios that attributes can't handle, and provides centralized configuration management. Fluent API is preferred for complex configurations and when keeping entities clean.

### When to use each?

Use **Data Annotations** for simple configurations, validation rules, or when you prefer configuration co-located with entities. Use **Fluent API** for complex relationships, composite keys, shadow properties, value conversions, or when you want clean POCOs without infrastructure concerns. Many projects use both strategically.

### Example

```csharp
// Data Annotations Approach
[Table("Products")]
[Index(nameof(SKU), IsUnique = true)]
public class Product
{
    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }
    
    [StringLength(50)]
    public string SKU { get; set; }
    
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    
    public int CategoryId { get; set; }
}

// Fluent API Approach - Clean POCO
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string SKU { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

// Configuration in OnModelCreating
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Product>(entity =>
    {
        entity.ToTable("Products");
        
        entity.HasKey(p => p.ProductId);
        entity.Property(p => p.ProductId)
            .HasColumnName("product_id");
        
        entity.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        entity.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        entity.Property(p => p.SKU)
            .HasMaxLength(50)
            .IsRequired();
        
        entity.HasIndex(p => p.SKU)
            .IsUnique();
        
        entity.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    });
}

// Advanced Fluent API features not available in Data Annotations
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Shadow property
    modelBuilder.Entity<Product>()
        .Property<DateTime>("LastModified");
    
    // Value conversion
    modelBuilder.Entity<Product>()
        .Property(p => p.Status)
        .HasConversion<string>();
    
    // Global query filter
    modelBuilder.Entity<Product>()
        .HasQueryFilter(p => !p.IsDeleted);
    
    // Composite key
    modelBuilder.Entity<OrderItem>()
        .HasKey(oi => new { oi.OrderId, oi.ProductId });
}
```

---

## 30. What is IEntityTypeConfiguration and why use it?

### What is it?

`IEntityTypeConfiguration<TEntity>` is an interface that allows you to extract entity configuration from `OnModelCreating()` into separate, dedicated configuration classes. Each entity gets its own configuration class implementing this interface, promoting separation of concerns and better organization.

### Why do we use it?

As applications grow, `OnModelCreating()` becomes bloated with hundreds of lines of configuration code. `IEntityTypeConfiguration` separates each entity's configuration into its own file, improving maintainability, testability, and code organization. It follows Single Responsibility Principle and makes configurations easier to locate and modify.

### When to use it?

Use `IEntityTypeConfiguration` in medium to large projects with many entities, when you want clean and organized configuration, in team environments where different developers work on different entities, or when following Clean Architecture/DDD principles with separate configuration assemblies.

### Example

```csharp
// Entity
public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}

// Separate configuration class
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Table name
        builder.ToTable("Orders");
        
        // Primary key
        builder.HasKey(o => o.OrderId);
        
        // Properties
        builder.Property(o => o.OrderDate)
            .IsRequired();
        
        builder.Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        // Relationships
        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Indexes
        builder.HasIndex(o => o.OrderDate);
        builder.HasIndex(o => o.CustomerId);
    }
}

// Another entity configuration
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(p => p.ProductId);
        
        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
        
        builder.HasIndex(p => p.SKU)
            .IsUnique();
    }
}

// DbContext - Apply all configurations
public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply individual configurations
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        
        // OR apply all configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
```

---

## 31. How to implement Data Seeding in EF Core?

### What is it?

Data seeding populates database tables with initial or default data during migrations. EF Core provides `HasData()` method in `OnModelCreating()` to seed data that gets included in migrations. This ensures consistent initial data across all environments (development, staging, production).

### Why do we use it?

Data seeding provides initial lookup data, default configurations, test data for development, or reference data required for application functionality. It ensures databases start with necessary data, supports automated testing, and maintains consistency across deployments without manual data entry scripts.

### When to use it?

Use data seeding for lookup tables (countries, states, categories), default admin users, configuration settings, role definitions, or any reference data required for application startup. Seed data should be static or rarely changing - don't seed transactional data.

### Example

```csharp
// Entities
public class Department
{
    public int DepartmentId { get; set; }
    public string Name { get; set; }
    public List<Employee> Employees { get; set; }
}

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public decimal Salary { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}

// Seeding in OnModelCreating
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Seed Departments
    modelBuilder.Entity<Department>().HasData(
        new Department { DepartmentId = 1, Name = "Engineering" },
        new Department { DepartmentId = 2, Name = "Sales" },
        new Department { DepartmentId = 3, Name = "HR" }
    );
    
    // Seed Employees
    modelBuilder.Entity<Employee>().HasData(
        new Employee 
        { 
            EmployeeId = 1, 
            Name = "John Doe", 
            Salary = 80000, 
            DepartmentId = 1 
        },
        new Employee 
        { 
            EmployeeId = 2, 
            Name = "Jane Smith", 
            Salary = 75000, 
            DepartmentId = 1 
        },
        new Employee 
        { 
            EmployeeId = 3, 
            Name = "Bob Johnson", 
            Salary = 65000, 
            DepartmentId = 2 
        }
    );
}

// Complex seeding with relationships
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Seed roles
    var adminRoleId = Guid.NewGuid();
    var userRoleId = Guid.NewGuid();
    
    modelBuilder.Entity<Role>().HasData(
        new Role { RoleId = adminRoleId, Name = "Admin" },
        new Role { RoleId = userRoleId, Name = "User" }
    );
    
    // Seed users
    var adminUserId = Guid.NewGuid();
    
    modelBuilder.Entity<User>().HasData(
        new User 
        { 
            UserId = adminUserId, 
            Username = "admin", 
            Email = "admin@example.com" 
        }
    );
    
    // Seed many-to-many relationship
    modelBuilder.Entity<UserRole>().HasData(
        new UserRole 
        { 
            UserId = adminUserId, 
            RoleId = adminRoleId 
        }
    );
}

// Alternative: Seeding via migration or custom method
public static class DataSeeder
{
    public static void SeedData(AppDbContext context)
    {
        // Check if data already exists
        if (!context.Departments.Any())
        {
            var departments = new[]
            {
                new Department { Name = "Engineering" },
                new Department { Name = "Sales" },
                new Department { Name = "HR" }
            };
            
            context.Departments.AddRange(departments);
            context.SaveChanges();
        }
    }
}

// Usage in Program.cs or Startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DataSeeder.SeedData(context);
}
```

---

## 32. How to implement Composite Keys in EF Core?

### What is it?

A composite key is a primary key consisting of two or more columns that together uniquely identify a record. In EF Core, composite keys cannot be configured using Data Annotations - you must use Fluent API with `HasKey()` specifying multiple properties.

### Why do we use it?

Composite keys represent natural business keys where multiple properties combine to form uniqueness, commonly in join tables for many-to-many relationships or when modeling real-world constraints. They enforce business rules at the database level and eliminate the need for artificial surrogate keys in certain scenarios.

### When to use it?

Use composite keys for many-to-many join tables, when natural business keys span multiple columns (e.g., OrderId + ProductId for OrderItems), temporal tables with entity + date keys, or when modeling domain requirements that naturally have multi-column uniqueness constraints.

### Example

```csharp
// Join table for many-to-many with composite key
public class StudentCourse
{
    public int StudentId { get; set; }
    public Student Student { get; set; }
    
    public int CourseId { get; set; }
    public Course Course { get; set; }
    
    public DateTime EnrollmentDate { get; set; }
    public string Grade { get; set; }
}

// Configuration
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<StudentCourse>()
        .HasKey(sc => new { sc.StudentId, sc.CourseId });
    
    modelBuilder.Entity<StudentCourse>()
        .HasOne(sc => sc.Student)
        .WithMany(s => s.StudentCourses)
        .HasForeignKey(sc => sc.StudentId);
    
    modelBuilder.Entity<StudentCourse>()
        .HasOne(sc => sc.Course)
        .WithMany(c => c.StudentCourses)
        .HasForeignKey(sc => sc.CourseId);
}

// Natural business composite key example
public class ProductInventory
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Composite key on ProductId and WarehouseId
    modelBuilder.Entity<ProductInventory>()
        .HasKey(pi => new { pi.ProductId, pi.WarehouseId });
    
    modelBuilder.Entity<ProductInventory>()
        .Property(pi => pi.Quantity)
        .IsRequired();
}

// Usage
using (var context = new AppDbContext())
{
    // Add with composite key
    var inventory = new ProductInventory
    {
        ProductId = 1,
        WarehouseId = 5,
        Quantity = 100,
        LastUpdated = DateTime.Now
    };
    
    context.ProductInventories.Add(inventory);
    context.SaveChanges();
    
    // Find by composite key
    var item = context.ProductInventories
        .Find(1, 5); // ProductId = 1, WarehouseId = 5
    
    // Update
    if (item != null)
    {
        item.Quantity += 50;
        context.SaveChanges();
    }
    
    // Query
    var warehouseInventory = context.ProductInventories
        .Where(pi => pi.WarehouseId == 5)
        .Include(pi => pi.Product)
        .ToList();
}
```

---

## 33. How to implement Audit Fields (CreatedDate, UpdatedDate) in EF Core?

### What is it?

Audit fields automatically track when records are created, modified, and by whom. Common audit fields include CreatedDate, UpdatedDate, CreatedBy, and UpdatedBy. These are implemented by overriding `SaveChanges()` or `SaveChangesAsync()` to automatically populate audit properties based on entity state before saving.

### Why do we use it?

Audit fields provide automatic tracking for compliance, debugging, data history, and accountability without manually setting these values in every operation. They support regulatory requirements, help troubleshoot issues, enable temporal queries, and provide transparency in data modifications across the application.

### When to use it?

Implement audit fields in applications requiring compliance tracking, multi-user systems needing accountability, when debugging data issues requires modification history, or when business requirements mandate knowing who changed what and when. Essential for financial, healthcare, or regulated industries.

### Example

```csharp
// Base entity with audit fields
public abstract class AuditableEntity
{
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
}

// Domain entity inheriting audit fields
public class Product : AuditableEntity
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class Order : AuditableEntity
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}

// DbContext with automatic audit tracking
public class AppDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    public override int SaveChanges()
    {
        ApplyAuditInformation();
        return base.SaveChanges();
    }
    
    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        ApplyAuditInformation();
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    private void ApplyAuditInformation()
    {
        var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name 
            ?? "System";
        var currentTime = DateTime.UtcNow;
        
        var entries = ChangeTracker.Entries<AuditableEntity>()
            .Where(e => e.State == EntityState.Added || 
                       e.State == EntityState.Modified);
        
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = currentTime;
                entry.Entity.CreatedBy = currentUser;
            }
            
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedDate = currentTime;
                entry.Entity.UpdatedBy = currentUser;
                
                // Prevent modification of creation audit fields
                entry.Property(x => x.CreatedDate).IsModified = false;
                entry.Property(x => x.CreatedBy).IsModified = false;
            }
        }
    }
}

// Usage - audit fields populated automatically
using (var context = new AppDbContext(options, httpContextAccessor))
{
    // Add new product - CreatedDate and CreatedBy set automatically
    var product = new Product
    {
        Name = "Laptop",
        Price = 999.99m
    };
    
    context.Products.Add(product);
    context.SaveChanges();
    // product.CreatedDate = 2026-01-23 10:30:00
    // product.CreatedBy = "john.doe@example.com"
    
    // Update product - UpdatedDate and UpdatedBy set automatically
    product.Price = 899.99m;
    context.SaveChanges();
    // product.UpdatedDate = 2026-01-23 11:45:00
    // product.UpdatedBy = "john.doe@example.com"
    
    // Query by audit fields
    var recentProducts = context.Products
        .Where(p => p.CreatedDate >= DateTime.UtcNow.AddDays(-7))
        .ToList();
}
```

---

## 34. What is Deferred Execution in LINQ and EF Core?

### What is it?

Deferred execution means LINQ queries are not executed when defined - they execute only when enumerated (ToList(), FirstOrDefault(), Count(), foreach, etc.). The query definition creates an expression tree that EF Core translates to SQL only at enumeration time, allowing query composition and optimization before database execution.

### Why is it important?

Deferred execution enables query composition (building queries incrementally), allows EF Core to optimize the final SQL, and prevents unnecessary database calls. However, it can cause issues like multiple enumerations executing the query repeatedly, queries executing outside of context scope, or unintended lazy evaluation in loops.

### When to be careful?

Be careful when returning IQueryable from methods (queries execute after method returns, potentially outside context scope), enumerating queries multiple times (each enumeration executes a new query), or using queries in loops. Always materialize queries with ToList() when appropriate to control execution timing.

### Example

```csharp
using (var context = new AppDbContext())
{
    // Query definition - NOT executed yet
    var query = context.Employees
        .Where(e => e.Salary > 50000);
    // No SQL executed at this point
    
    // Add more filters - still not executed
    query = query.Where(e => e.Department == "IT");
    // Still no SQL executed
    
    // Execution happens here at ToList()
    var employees = query.ToList();
    // SQL executed: SELECT * FROM Employees 
    //               WHERE Salary > 50000 AND Department = 'IT'
}

// PROBLEM: Multiple enumerations
using (var context = new AppDbContext())
{
    var query = context.Orders.Where(o => o.TotalAmount > 1000);
    
    // First enumeration - executes query
    var count = query.Count();
    
    // Second enumeration - executes query AGAIN!
    var orders = query.ToList();
    
    // SOLUTION: Materialize once
    var materializedQuery = query.ToList();
    var count2 = materializedQuery.Count; // No database hit
    var orders2 = materializedQuery; // No database hit
}

// PROBLEM: Query execution outside context scope
public IQueryable<Product> GetExpensiveProducts()
{
    var context = new AppDbContext();
    return context.Products.Where(p => p.Price > 1000);
    // Query NOT executed yet, context will be disposed
} // Context disposed here

// Usage
var products = GetExpensiveProducts().ToList();
// ERROR: Context already disposed when query executes

// SOLUTION 1: Return materialized list
public List<Product> GetExpensiveProducts()
{
    using (var context = new AppDbContext())
    {
        return context.Products
            .Where(p => p.Price > 1000)
            .ToList(); // Execute within context scope
    }
}

// SOLUTION 2: Accept context as parameter
public IQueryable<Product> GetExpensiveProducts(AppDbContext context)
{
    return context.Products.Where(p => p.Price > 1000);
    // Caller controls execution and context lifetime
}

// PROBLEM: Query in loop (N+1 problem variant)
using (var context = new AppDbContext())
{
    var departments = context.Departments.ToList();
    
    foreach (var dept in departments)
    {
        // Each iteration executes a new query!
        var empCount = context.Employees
            .Count(e => e.DepartmentId == dept.DepartmentId);
    }
    
    // SOLUTION: Single query with grouping
    var employeeCounts = context.Employees
        .GroupBy(e => e.DepartmentId)
        .Select(g => new { DepartmentId = g.Key, Count = g.Count() })
        .ToDictionary(x => x.DepartmentId, x => x.Count);
}

// Practical example showing deferred execution benefits
using (var context = new AppDbContext())
{
    IQueryable<Order> query = context.Orders;
    
    // Build query conditionally
    if (startDate.HasValue)
        query = query.Where(o => o.OrderDate >= startDate.Value);
    
    if (endDate.HasValue)
        query = query.Where(o => o.OrderDate <= endDate.Value);
    
    if (!string.IsNullOrEmpty(customerName))
        query = query.Where(o => o.Customer.Name.Contains(customerName));
    
    // Execute once with all conditions combined
    var results = query.ToList();
    // Single optimized SQL query with all WHERE clauses
}
```

---

## 35. What is the difference between IQueryable and IEnumerable in EF Core?

### What is it?

`IQueryable<T>` represents a query that hasn't been executed against the database yet - operations are translated to SQL and executed on the database server. `IEnumerable<T>` represents data already loaded into memory - further operations execute in memory using LINQ-to-Objects. IQueryable derives from IEnumerable but has different execution semantics.

### Why does it matter?

Using IQueryable allows EF Core to translate all operations to SQL, executing filtering, sorting, and projections on the database server. Using IEnumerable after loading data means subsequent operations run in memory, potentially loading large datasets and performing inefficient processing. The choice significantly impacts performance and database load.

### When to use each?

Use **IQueryable** when building queries that should execute on the database, when you want EF Core to optimize the SQL, or when working with large datasets. Use **IEnumerable** when working with already-loaded data, when operations can't be translated to SQL, or when processing small in-memory collections.

### Example

```csharp
using (var context = new AppDbContext())
{
    // IQueryable - operations translated to SQL
    IQueryable<Employee> queryable = context.Employees;
    
    queryable = queryable.Where(e => e.Salary > 50000); // Not executed
    queryable = queryable.OrderBy(e => e.Name); // Not executed
    
    var employees = queryable.Take(10).ToList(); // Single SQL query
    // SQL: SELECT TOP 10 * FROM Employees 
    //      WHERE Salary > 50000 ORDER BY Name
}

// PROBLEM: Converting to IEnumerable too early
using (var context = new AppDbContext())
{
    // AsEnumerable() forces subsequent operations to memory
    IEnumerable<Employee> enumerable = context.Employees
        .AsEnumerable(); // ALL employees loaded into memory!
    
    // This filter runs IN MEMORY, not in database
    var highEarners = enumerable
        .Where(e => e.Salary > 50000)
        .ToList();
    
    // BAD: Loaded all employees just to filter in memory
}

// CORRECT: Keep as IQueryable
using (var context = new AppDbContext())
{
    IQueryable<Employee> queryable = context.Employees;
    
    // All operations run on database
    var highEarners = queryable
        .Where(e => e.Salary > 50000)
        .ToList();
    
    // SQL: SELECT * FROM Employees WHERE Salary > 50000
    // Only filtered results loaded into memory
}

// When you NEED IEnumerable (operations not translatable to SQL)
using (var context = new AppDbContext())
{
    var employees = context.Employees
        .Where(e => e.IsActive) // IQueryable - runs on DB
        .ToList(); // Materialize to memory
    
    // Custom logic not translatable to SQL
    var processed = employees
        .Where(e => ComplexBusinessLogic(e)) // Runs in memory
        .OrderBy(e => CustomSorting(e)) // Runs in memory
        .ToList();
}

// Real-world example showing performance difference
// SLOW - loads 1 million employees into memory first
using (var context = new AppDbContext())
{
    var allEmployees = context.Employees.ToList(); // IEnumerable
    var itDepartment = allEmployees
        .Where(e => e.Department == "IT")
        .Take(10)
        .ToList();
    // Loaded 1,000,000 rows to get 10!
}

// FAST - filters in database
using (var context = new AppDbContext())
{
    var itDepartment = context.Employees // IQueryable
        .Where(e => e.Department == "IT")
        .Take(10)
        .ToList();
    // SQL: SELECT TOP 10 * FROM Employees WHERE Department = 'IT'
    // Only 10 rows loaded
}

// Method return types impact
public IQueryable<Product> GetProductsQueryable(AppDbContext context)
{
    return context.Products.Where(p => p.IsActive);
    // Caller can add more filters that run on database
}

public IEnumerable<Product> GetProductsEnumerable(AppDbContext context)
{
    return context.Products.Where(p => p.IsActive).ToList();
    // All data already loaded, caller operations run in memory
}

// Usage
using (var context = new AppDbContext())
{
    // Can add database filters
    var expensive = GetProductsQueryable(context)
        .Where(p => p.Price > 1000)
        .ToList();
    // SQL: WHERE IsActive = 1 AND Price > 1000
    
    // Filters run in memory
    var expensive2 = GetProductsEnumerable(context)
        .Where(p => p.Price > 1000)
        .ToList();
    // All active products loaded, then filtered in memory
}
```

---

## 36. How to handle Migrations in large teams?

### What is it?

Managing migrations in large teams requires strategies to avoid merge conflicts, maintain migration order, and ensure consistency across branches. Challenges include multiple developers creating migrations simultaneously, maintaining migration history, and coordinating database updates across environments. Teams use branching strategies, migration naming conventions, and coordination practices.

### Why is it challenging?

Concurrent migrations from different developers can conflict when merged, causing broken migration history or database schema inconsistencies. Migration timestamps create merge conflicts, and applying migrations out of order can fail. Large teams need processes to prevent duplicate migrations, resolve conflicts, and maintain a single source of truth for schema.

### When to implement strategies?

Implement migration strategies when you have 3+ developers working on database changes, when using feature branches that modify the database, when deployments happen frequently, or when experiencing migration conflicts. Critical for CI/CD pipelines and distributed teams working on different features simultaneously.

### Example

```csharp
// PROBLEM: Two developers create migrations simultaneously
// Developer A (feature/add-products)
// Timestamp: 20260123_120000
public partial class AddProductTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                ProductId = table.Column<int>(),
                Name = table.Column<string>()
            });
    }
}

// Developer B (feature/add-customers)  
// Timestamp: 20260123_120500 (5 minutes later)
public partial class AddCustomerTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Customers",
            columns: table => new
            {
                CustomerId = table.Column<int>(),
                Name = table.Column<string>()
            });
    }
}

// When merged to main: CONFLICT! Both think they're the latest migration

// SOLUTION 1: Rebase and recreate migration
// Developer A merges first to main
// Developer B:
// 1. Pull latest main (includes AddProductTable migration)
// 2. Delete their migration file and remove from ModelSnapshot
// 3. Run: dotnet ef migrations add AddCustomerTable
//    (Gets new timestamp AFTER AddProductTable)

// SOLUTION 2: Use migration coordination (team process)
// Before creating migration:
// 1. Pull latest changes
// 2. Check for pending migrations from others
// 3. Create migration with descriptive name
// 4. Merge quickly to avoid conflicts

// SOLUTION 3: Feature-based migration naming
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer(
        connectionString,
        x => x.MigrationsHistoryTable("__EFMigrationsHistory", "migrations"));
}

// SOLUTION 4: Idempotent migrations script for CI/CD
// Generate script that can run multiple times safely
// dotnet ef migrations script --idempotent --output migration.sql

// SOLUTION 5: Environment-specific migration tracking
public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            connectionString,
            options => options.MigrationsAssembly("Infrastructure"));
    }
}

// SOLUTION 6: Pre-merge checks in CI pipeline
// .github/workflows/check-migrations.yml
// - name: Check for migration conflicts
//   run: |
//     dotnet ef migrations list
//     # Fail if multiple migrations with same timestamp exist

// Best Practice: Migration Coordination Workflow
// 1. Create feature branch
// 2. Make entity changes
// 3. Before creating migration:
//    - Pull latest from main
//    - Apply any new migrations
//    - Check ModelSnapshot for conflicts
// 4. Create migration: dotnet ef migrations add [DescriptiveName]
// 5. Review generated SQL
// 6. Commit migration files
// 7. Create PR and merge quickly
// 8. Team members pull and apply migration immediately

// Handling merge conflicts in ModelSnapshot
// If conflict occurs:
// 1. Accept incoming changes (main branch version)
// 2. Delete your migration file
// 3. Revert changes to DbContext/entities
// 4. Pull latest
// 5. Reapply your entity changes
// 6. Create new migration with new timestamp

// Production deployment strategy
public class DatabaseMigrator
{
    public static void MigrateDatabase(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var context = scope.ServiceProvider
                .GetRequiredService<AppDbContext>();
            
            try
            {
                // Get pending migrations
                var pendingMigrations = context.Database
                    .GetPendingMigrations();
                
                if (pendingMigrations.Any())
                {
                    Console.WriteLine($"Applying {pendingMigrations.Count()} migrations...");
                    
                    // Apply migrations
                    context.Database.Migrate();
                    
                    Console.WriteLine("Migrations applied successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Migration failed: {ex.Message}");
                throw;
            }
        }
    }
}
```

---

## 37. How to implement Multi-Tenant architecture in EF Core?

### What is it?

Multi-tenancy allows a single application instance to serve multiple tenants (customers/organizations) with data isolation. EF Core supports different strategies: separate databases per tenant, separate schemas per tenant, or shared database with discriminator columns. Global query filters automatically isolate tenant data in queries.

### Why do we use it?

Multi-tenancy reduces infrastructure costs, simplifies maintenance (single codebase/deployment), enables easier scaling, and provides logical data isolation between tenants. It's essential for SaaS applications where each customer needs their own isolated data while sharing the same application infrastructure.

### When to use each strategy?

Use **separate databases** for strong isolation, compliance requirements, or per-tenant scaling. Use **separate schemas** for moderate isolation with shared infrastructure. Use **discriminator column** for many small tenants with simple isolation needs. Choice depends on isolation requirements, scale, compliance, and performance needs.

### Example

```csharp
// STRATEGY 1: Discriminator Column (Shared Database)
// Entity with TenantId
public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string TenantId { get; set; } // Tenant discriminator
}

// Tenant service
public interface ITenantService
{
    string GetCurrentTenantId();
}

public class TenantService : ITenantService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public TenantService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string GetCurrentTenantId()
    {
        // Get from claims, header, subdomain, etc.
        return _httpContextAccessor.HttpContext?.User
            ?.FindFirst("TenantId")?.Value ?? throw new Exception("No tenant");
    }
}

// DbContext with global query filter
public class AppDbContext : DbContext
{
    private readonly ITenantService _tenantService;
    
    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ITenantService tenantService) : base(options)
    {
        _tenantService = tenantService;
    }
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Global query filter for tenant isolation
        modelBuilder.Entity<Order>()
            .HasQueryFilter(o => o.TenantId == _tenantService.GetCurrentTenantId());
        
        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => p.TenantId == _tenantService.GetCurrentTenantId());
        
        // Index for performance
        modelBuilder.Entity<Order>()
            .HasIndex(o => o.TenantId);
    }
    
    public override int SaveChanges()
    {
        // Automatically set TenantId on new entities
        var tenantId = _tenantService.GetCurrentTenantId();
        
        foreach (var entry in ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added))
        {
            var tenantProperty = entry.Entity.GetType()
                .GetProperty("TenantId");
            
            if (tenantProperty != null)
            {
                tenantProperty.SetValue(entry.Entity, tenantId);
            }
        }
        
        return base.SaveChanges();
    }
}

// STRATEGY 2: Separate Database Per Tenant
public class TenantDbContext : DbContext
{
    private readonly string _tenantConnectionString;
    
    public TenantDbContext(string tenantConnectionString)
    {
        _tenantConnectionString = tenantConnectionString;
    }
    
    public DbSet<Order> Orders { get; set; }
    
    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_tenantConnectionString);
    }
}

// Tenant connection resolver
public interface ITenantConnectionResolver
{
    string GetConnectionString(string tenantId);
}

public class TenantConnectionResolver : ITenantConnectionResolver
{
    private readonly IConfiguration _configuration;
    
    public TenantConnectionResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GetConnectionString(string tenantId)
    {
        // Get from configuration, database, cache, etc.
        return _configuration
            .GetConnectionString($"Tenant_{tenantId}");
    }
}

// DbContext factory for multi-tenant
public class TenantDbContextFactory
{
    private readonly ITenantService _tenantService;
    private readonly ITenantConnectionResolver _connectionResolver;
    
    public TenantDbContextFactory(
        ITenantService tenantService,
        ITenantConnectionResolver connectionResolver)
    {
        _tenantService = tenantService;
        _connectionResolver = connectionResolver;
    }
    
    public AppDbContext CreateDbContext()
    {
        var tenantId = _tenantService.GetCurrentTenantId();
        var connectionString = _connectionResolver
            .GetConnectionString(tenantId);
        
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        
        return new AppDbContext(optionsBuilder.Options);
    }
}

// Registration in Program.cs
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ITenantConnectionResolver, TenantConnectionResolver>();
builder.Services.AddScoped<TenantDbContextFactory>();

// STRATEGY 3: Separate Schema Per Tenant
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    var tenantId = _tenantService.GetCurrentTenantId();
    var schema = $"tenant_{tenantId}";
    
    modelBuilder.Entity<Order>().ToTable("Orders", schema);
    modelBuilder.Entity<Product>().ToTable("Products", schema);
}

// Usage in controller
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public OrdersController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        // Automatically filtered by tenant
        var orders = await _context.Orders.ToListAsync();
        return Ok(orders);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder(Order order)
    {
        // TenantId set automatically in SaveChanges
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return Ok(order);
    }
}
```

---

## 38. What are the key Performance Tuning strategies in EF Core?

### What is it?

Performance tuning in EF Core involves optimizing query execution, reducing database round trips, minimizing data transfer, and improving memory usage. Key strategies include query optimization, proper indexing, connection pooling, compiled queries, batch operations, and profiling. Performance problems often stem from N+1 queries, over-fetching data, or inefficient query patterns.

### Why is it critical?

Poor EF Core performance leads to slow application response times, high database load, increased costs, and poor user experience. A single inefficient query can impact entire application performance. Performance optimization can improve response times by 10-100x, reduce server costs, and improve scalability from hundreds to thousands of concurrent users.

### When to optimize?

Optimize during development for known hot paths, after profiling reveals bottlenecks, when performance tests show degradation, or when monitoring indicates high database load. Focus on frequently executed queries, report generation, data export, and high-traffic endpoints. Always measure before and after optimization.

### Example

```csharp
// PROBLEM 1: N+1 Query Problem
// BAD - executes N+1 queries
public async Task<List<OrderDto>> GetOrdersSlow()
{
    var orders = await _context.Orders.ToListAsync(); // 1 query
    
    foreach (var order in orders) // N queries
    {
        order.Customer = await _context.Customers
            .FindAsync(order.CustomerId);
    }
    
    return orders;
}

// GOOD - single query with Include
public async Task<List<OrderDto>> GetOrdersFast()
{
    return await _context.Orders
        .Include(o => o.Customer)
        .ToListAsync(); // 1 query with JOIN
}

// PROBLEM 2: Loading entire entities when only few columns needed
// BAD - loads all columns
public async Task<List<ProductSummary>> GetProductsSlow()
{
    var products = await _context.Products.ToListAsync();
    
    return products.Select(p => new ProductSummary
    {
        Name = p.Name,
        Price = p.Price
    }).ToList();
}

// GOOD - projection with Select (only needed columns)
public async Task<List<ProductSummary>> GetProductsFast()
{
    return await _context.Products
        .Select(p => new ProductSummary
        {
            Name = p.Name,
            Price = p.Price
        })
        .ToListAsync();
    // SQL: SELECT Name, Price FROM Products (not SELECT *)
}

// PROBLEM 3: Tracking entities for read-only operations
// BAD - unnecessary tracking overhead
public async Task<List<Order>> GetOrdersForReportSlow()
{
    return await _context.Orders
        .Include(o => o.Customer)
        .ToListAsync();
}

// GOOD - no tracking for read-only
public async Task<List<Order>> GetOrdersForReportFast()
{
    return await _context.Orders
        .AsNoTracking()
        .Include(o => o.Customer)
        .ToListAsync();
    // 30-50% faster, less memory
}

// PROBLEM 4: Cartesian explosion with multiple collections
// BAD - single query causes cartesian explosion
public async Task<List<Customer>> GetCustomersSlow()
{
    return await _context.Customers
        .Include(c => c.Orders) // 10 orders
        .Include(c => c.Addresses) // 3 addresses
        .ToListAsync();
    // Returns 30 rows for 1 customer (10 * 3)!
}

// GOOD - split query
public async Task<List<Customer>> GetCustomersFast()
{
    return await _context.Customers
        .Include(c => c.Orders)
        .Include(c => c.Addresses)
        .AsSplitQuery() // Separate queries for each collection
        .ToListAsync();
    // Query 1: Customers
    // Query 2: Orders for these customers
    // Query 3: Addresses for these customers
}

// PROBLEM 5: Repeated identical queries
// BAD - same query executed multiple times
public async Task<decimal> CalculateTotalsSlow()
{
    var total1 = await _context.Orders.SumAsync(o => o.TotalAmount);
    var total2 = await _context.Orders.SumAsync(o => o.TotalAmount);
    var total3 = await _context.Orders.SumAsync(o => o.TotalAmount);
    
    return total1 + total2 + total3;
}

// GOOD - compiled query for repeated execution
private static readonly Func<AppDbContext, Task<decimal>> _getOrderTotal =
    EF.CompileAsyncQuery((AppDbContext context) =>
        context.Orders.Sum(o => o.TotalAmount));

public async Task<decimal> CalculateTotalsFast()
{
    var total1 = await _getOrderTotal(_context);
    var total2 = await _getOrderTotal(_context);
    var total3 = await _getOrderTotal(_context);
    
    return total1 + total2 + total3;
}

// PROBLEM 6: Multiple SaveChanges in loop
// BAD - N database round trips
public async Task AddProductsSlow(List<Product> products)
{
    foreach (var product in products)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync(); // Separate DB call each time
    }
}

// GOOD - batch operation
public async Task AddProductsFast(List<Product> products)
{
    await _context.Products.AddRangeAsync(products);
    await _context.SaveChangesAsync(); // Single DB call
}

// PROBLEM 7: Loading entire table to count
// BAD - loads all records into memory
public async Task<int> GetProductCountSlow()
{
    var products = await _context.Products.ToListAsync();
    return products.Count;
}

// GOOD - COUNT query on database
public async Task<int> GetProductCountFast()
{
    return await _context.Products.CountAsync();
    // SQL: SELECT COUNT(*) FROM Products
}

// Comprehensive performance optimization example
public class OptimizedOrderService
{
    private readonly AppDbContext _context;
    
    public OptimizedOrderService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<OrderDto>> GetOrdersOptimized(
        DateTime startDate, DateTime endDate)
    {
        return await _context.Orders
            .AsNoTracking() // No tracking needed for read-only
            .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
            .Include(o => o.Customer) // Eager load to avoid N+1
            .AsSplitQuery() // Prevent cartesian explosion
            .Select(o => new OrderDto // Project only needed data
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                CustomerName = o.Customer.Name,
                ItemCount = o.OrderItems.Count
            })
            .ToListAsync(); // Execute asynchronously
    }
}

// Enable query logging to identify slow queries
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder
        .UseSqlServer(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging() // Development only
        .EnableDetailedErrors();
}

// Use execution strategy for connection resiliency
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer(
        connectionString,
        options => options.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null));
}
```

---

## 39. How to handle large datasets efficiently in EF Core?

### What is it?

Handling large datasets requires techniques to avoid loading everything into memory. Strategies include pagination, streaming with `AsAsyncEnumerable()`, batching operations, using projections to reduce data size, and executing operations directly on the database with `ExecuteUpdate()`/`ExecuteDelete()` (EF Core 7+).

### Why is it important?

Loading large datasets into memory causes OutOfMemoryException, high memory consumption, slow query execution, and application crashes. Processing millions of records requires streaming, batching, or server-side processing to maintain acceptable performance and memory usage. Improper handling can crash production applications.

### When to use these techniques?

Use pagination for user-facing lists, streaming for background processing or data export, batching for bulk operations, and ExecuteUpdate/ExecuteDelete for mass updates. Apply these when dealing with thousands+ records, generating reports, migrating data, or processing queues.

### Example

```csharp
// PROBLEM: Loading millions of records into memory
// BAD - OutOfMemoryException!
public async Task ProcessAllOrdersSlow()
{
    var orders = await _context.Orders.ToListAsync(); // Loads millions!
    
    foreach (var order in orders)
    {
        ProcessOrder(order);
    }
}

// SOLUTION 1: Pagination
public async Task<PagedResult<Order>> GetOrdersPaged(int page, int pageSize)
{
    var totalCount = await _context.Orders.CountAsync();
    
    var orders = await _context.Orders
        .OrderBy(o => o.OrderId)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    
    return new PagedResult<Order>
    {
        Items = orders,
        TotalCount = totalCount,
        Page = page,
        PageSize = pageSize
    };
}

// SOLUTION 2: Streaming with AsAsyncEnumerable
public async Task ProcessAllOrdersStreaming()
{
    await foreach (var order in _context.Orders.AsAsyncEnumerable())
    {
        ProcessOrder(order);
        // Each order processed immediately, not loaded all at once
    }
}

// SOLUTION 3: Batched processing
public async Task ProcessOrdersInBatches(int batchSize = 1000)
{
    var totalOrders = await _context.Orders.CountAsync();
    var batches = (int)Math.Ceiling((double)totalOrders / batchSize);
    
    for (int i = 0; i < batches; i++)
    {
        var batch = await _context.Orders
            .OrderBy(o => o.OrderId)
            .Skip(i * batchSize)
            .Take(batchSize)
            .ToListAsync();
        
        foreach (var order in batch)
        {
            ProcessOrder(order);
        }
        
        // Optional: Clear tracked entities to free memory
        _context.ChangeTracker.Clear();
    }
}

// SOLUTION 4: ExecuteUpdate for bulk updates (EF Core 7+)
// BAD - loads all records, updates in memory, saves back
public async Task IncreaseAllPricesSlow(decimal percentage)
{
    var products = await _context.Products.ToListAsync();
    
    foreach (var product in products)
    {
        product.Price *= (1 + percentage);
    }
    
    await _context.SaveChangesAsync();
}

// GOOD - single UPDATE statement on database
public async Task IncreaseAllPricesFast(decimal percentage)
{
    await _context.Products
        .ExecuteUpdateAsync(setters => setters
            .SetProperty(p => p.Price, p => p.Price * (1 + percentage)));
    
    // SQL: UPDATE Products SET Price = Price * 1.1
}

// SOLUTION 5: ExecuteDelete for bulk deletes (EF Core 7+)
// BAD - loads all records, deletes each
public async Task DeleteOldOrdersSlow(DateTime cutoffDate)
{
    var oldOrders = await _context.Orders
        .Where(o => o.OrderDate < cutoffDate)
        .ToListAsync();
    
    _context.Orders.RemoveRange(oldOrders);
    await _context.SaveChangesAsync();
}

// GOOD - single DELETE statement
public async Task DeleteOldOrdersFast(DateTime cutoffDate)
{
    await _context.Orders
        .Where(o => o.OrderDate < cutoffDate)
        .ExecuteDeleteAsync();
    
    // SQL: DELETE FROM Orders WHERE OrderDate < @cutoffDate
}

// Real-world example: Export large dataset to CSV
public async Task ExportOrdersToCsv(string filePath)
{
    using (var writer = new StreamWriter(filePath))
    {
        await writer.WriteLineAsync("OrderId,OrderDate,TotalAmount,Customer");
        
        // Stream orders instead of loading all
        await foreach (var order in _context.Orders
            .AsNoTracking()
            .Include(o => o.Customer)
            .AsAsyncEnumerable())
        {
            await writer.WriteLineAsync(
                $"{order.OrderId},{order.OrderDate:yyyy-MM-dd}," +
                $"{order.TotalAmount},{order.Customer.Name}");
        }
    }
}

// Processing with progress reporting
public async Task ProcessLargeDatasetWithProgress(
    IProgress<int> progress)
{
    var totalCount = await _context.Orders.CountAsync();
    var processed = 0;
    var batchSize = 1000;
    
    for (int skip = 0; skip < totalCount; skip += batchSize)
    {
        var batch = await _context.Orders
            .AsNoTracking()
            .OrderBy(o => o.OrderId)
            .Skip(skip)
            .Take(batchSize)
            .ToListAsync();
        
        foreach (var order in batch)
        {
            ProcessOrder(order);
            processed++;
            
            if (processed % 100 == 0)
            {
                progress.Report((processed * 100) / totalCount);
            }
        }
    }
}

// Cursor-based pagination (better for real-time data)
public async Task<List<Order>> GetOrdersAfterCursor(
    int lastOrderId, int pageSize)
{
    return await _context.Orders
        .Where(o => o.OrderId > lastOrderId)
        .OrderBy(o => o.OrderId)
        .Take(pageSize)
        .ToListAsync();
}
```

---

## 40. What are EF Core limitations and when to use alternatives?

### What is it?

EF Core has limitations in complex queries, bulk operations, performance-critical scenarios, and database-specific features. Understanding these limitations helps you make informed decisions about when to use raw SQL, stored procedures, Dapper, or other tools alongside EF Core.

### Why acknowledge limitations?

No tool is perfect for every scenario. EF Core prioritizes developer productivity and maintainability over raw performance. Recognizing its limitations prevents forcing EF Core into inappropriate use cases, allows hybrid approaches, and helps set realistic performance expectations. Better to use the right tool for each job.

### When to consider alternatives?

Consider alternatives for complex reporting with multiple aggregations, bulk operations on millions of records, database-specific features (window functions, full-text search), extreme performance requirements, or when generated SQL is inefficient. Many successful projects use EF Core for CRUD and Dapper/raw SQL for complex queries.

### Example

```csharp
// LIMITATION 1: Complex aggregations and window functions
// EF Core struggles with complex SQL features
// BAD - not well supported in EF Core
public async Task<List<SalesRanking>> GetSalesRankingSlow()
{
    // EF Core doesn't natively support ROW_NUMBER(), RANK(), etc.
    // Would require loading all data and ranking in memory
    var sales = await _context.Sales
        .Include(s => s.Employee)
        .ToListAsync();
    
    // Ranking in memory - inefficient
    return sales
        .OrderByDescending(s => s.Amount)
        .Select((s, index) => new SalesRanking
        {
            EmployeeName = s.Employee.Name,
            Amount = s.Amount,
            Rank = index + 1
        })
        .ToList();
}

// GOOD - use raw SQL or Dapper
public async Task<List<SalesRanking>> GetSalesRankingFast()
{
    var sql = @"
        SELECT 
            e.Name AS EmployeeName,
            s.Amount,
            ROW_NUMBER() OVER (ORDER BY s.Amount DESC) AS Rank
        FROM Sales s
        INNER JOIN Employees e ON s.EmployeeId = e.EmployeeId";
    
    return await _context.Database
        .SqlQueryRaw<SalesRanking>(sql)
        .ToListAsync();
}

// LIMITATION 2: Bulk operations on large datasets
// EF Core loads entities, tracks changes, generates individual statements
// BAD - slow for large datasets
public async Task UpdateProductPricesSlow(
    Dictionary<int, decimal> priceUpdates)
{
    foreach (var kvp in priceUpdates)
    {
        var product = await _context.Products.FindAsync(kvp.Key);
        if (product != null)
        {
            product.Price = kvp.Value;
        }
    }
    
    await _context.SaveChangesAsync();
    // Generates separate UPDATE for each product
}

// GOOD - use bulk update library or raw SQL
public async Task UpdateProductPricesFast(
    Dictionary<int, decimal> priceUpdates)
{
    // EF Core 7+ ExecuteUpdate
    foreach (var kvp in priceUpdates)
    {
        await _context.Products
            .Where(p => p.ProductId == kvp.Key)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Price, kvp.Value));
    }
    
    // OR use bulk update library like EFCore.BulkExtensions
    // await _context.BulkUpdateAsync(products);
}

// LIMITATION 3: Complex joins that EF Core doesn't translate well
// BAD - EF Core generates inefficient SQL
public async Task<List<OrderSummary>> GetComplexReportSlow()
{
    return await _context.Orders
        .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.Product)
        .Include(o => o.Customer)
        .Where(o => o.OrderDate.Year == 2026)
        .Select(o => new OrderSummary
        {
            OrderId = o.OrderId,
            CustomerName = o.Customer.Name,
            TotalItems = o.OrderItems.Count,
            TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice)
        })
        .ToListAsync();
    // May generate suboptimal SQL with multiple subqueries
}

// GOOD - hand-crafted SQL for complex reports
public async Task<List<OrderSummary>> GetComplexReportFast()
{
    var sql = @"
        SELECT 
            o.OrderId,
            c.Name AS CustomerName,
            COUNT(oi.OrderItemId) AS TotalItems,
            SUM(oi.Quantity * oi.UnitPrice) AS TotalAmount
        FROM Orders o
        INNER JOIN Customers c ON o.CustomerId = c.CustomerId
        INNER JOIN OrderItems oi ON o.OrderId = oi.OrderId
        WHERE YEAR(o.OrderDate) = 2026
        GROUP BY o.OrderId, c.Name";
    
    return await _context.Database
        .SqlQueryRaw<OrderSummary>(sql)
        .ToListAsync();
}

// LIMITATION 4: Database-specific features
// EF Core abstracts database differences, limiting access to specific features
// Example: SQL Server Full-Text Search
public async Task<List<Product>> SearchProductsFullText(string searchTerm)
{
    // Can't use CONTAINS() or FREETEXT() through LINQ
    var sql = @"
        SELECT * FROM Products
        WHERE CONTAINS((Name, Description), @searchTerm)";
    
    return await _context.Products
        .FromSqlRaw(sql, searchTerm)
        .ToListAsync();
}

// LIMITATION 5: Stored procedures with complex output
// EF Core can call stored procedures but has limitations with complex outputs
public async Task<List<MonthlyReport>> GetMonthlyReportFromStoredProc()
{
    // Stored procedure: EXEC sp_GetMonthlyReport @Year, @Month
    return await _context.Set<MonthlyReport>()
        .FromSqlRaw("EXEC sp_GetMonthlyReport @Year = {0}, @Month = {1}", 
            2026, 1)
        .ToListAsync();
}

// LIMITATION 6: Transactions spanning multiple contexts or databases
// EF Core transactions limited to single context
// Use distributed transactions or application-level coordination

// LIMITATION 7: Dynamic queries with many optional parameters
// BAD - many optional parameters make EF Core queries complex
public async Task<List<Product>> SearchProducts(
    string name, decimal? minPrice, decimal? maxPrice,
    int? categoryId, bool? inStock, string sortBy)
{
    IQueryable<Product> query = _context.Products;
    
    if (!string.IsNullOrEmpty(name))
        query = query.Where(p => p.Name.Contains(name));
    
    if (minPrice.HasValue)
        query = query.Where(p => p.Price >= minPrice);
    
    if (maxPrice.HasValue)
        query = query.Where(p => p.Price <= maxPrice);
    
    // ... many more conditions
    
    return await query.ToListAsync();
}

// Consider using specification pattern or dynamic query builder

// Hybrid approach: Use both EF Core and Dapper
public class ProductService
{
    private readonly AppDbContext _context;
    private readonly IDbConnection _connection;
    
    // Use EF Core for writes and simple queries
    public async Task<Product> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    
    // Use Dapper for complex read queries
    public async Task<List<ProductSalesReport>> GetSalesReport()
    {
        var sql = @"
            SELECT 
                p.ProductId,
                p.Name,
                SUM(oi.Quantity) AS TotalSold,
                SUM(oi.Quantity * oi.UnitPrice) AS Revenue
            FROM Products p
            LEFT JOIN OrderItems oi ON p.ProductId = oi.ProductId
            GROUP BY p.ProductId, p.Name
            ORDER BY Revenue DESC";
        
        return (await _connection.QueryAsync<ProductSalesReport>(sql))
            .ToList();
    }
}

// When to use what?
// EF Core: CRUD operations, simple to moderate queries, need change tracking
// Raw SQL: Complex queries, database-specific features, optimal performance
// Dapper: Read-heavy scenarios, simple mapping, better performance than EF Core
// Stored Procedures: Complex business logic, security, reusability across apps
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
