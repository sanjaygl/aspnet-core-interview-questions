# C# Records - Complete Guide

> **Target Audience**: .NET Developers (Beginner to Senior)  
> **Framework**: C# 9.0+ (.NET 5+)  
> **Last Updated**: February 2026

---

## Table of Contents

1. [Introduction](#introduction)
2. [Key Features of Records](#key-features-of-records)
3. [Types of Records](#types-of-records)
4. [Examples](#examples)
5. [Record vs Class Comparison](#record-vs-class-comparison)
6. [When to Use a Record](#when-to-use-a-record)
7. [When NOT to Use Records](#when-not-to-use-records)
8. [Best Practices](#best-practices)
9. [Real-world Example](#real-world-example)
10. [Summary](#summary)

---

## Introduction

### What is a Record in C#?

A **record** is a reference type introduced in C# 9.0 that provides built-in functionality for encapsulating data. Records are designed primarily for **immutable data models** with **value-based equality** rather than reference-based equality.

```csharp
// Simple record declaration
public record Person(string FirstName, string LastName);
```

### Why Records Were Introduced

Records were introduced to address common pain points when working with data-centric objects:

- **Reduce boilerplate code** for data models
- **Built-in value equality** without manual implementation
- **Immutability by default** for safer concurrent programming
- **Concise syntax** for DTOs and value objects
- **Non-destructive mutation** using the `with` keyword

### Brief Comparison with Class and Struct

| Feature | Class | Record | Struct |
|---------|-------|--------|--------|
| **Type** | Reference type | Reference type (default) | Value type |
| **Equality** | Reference equality | Value equality | Value equality |
| **Mutability** | Mutable by default | Immutable by default | Mutable by default |
| **Inheritance** | Supports inheritance | Supports inheritance | No inheritance |
| **Default ToString** | Type name | All property values | Type name |
| **Memory** | Heap | Heap | Stack |

---

## Key Features of Records

### 1. Value-Based Equality

Records compare values, not references.

```csharp
public record Person(string Name, int Age);

var person1 = new Person("John", 30);
var person2 = new Person("John", 30);

Console.WriteLine(person1 == person2);  // True (value equality)
```

With classes, this would be `False` (reference equality) unless you override `Equals`.

### 2. Immutability (init-only properties)

Properties are immutable by default using `init` accessor.

```csharp
public record Product(string Name, decimal Price);

var product = new Product("Laptop", 999.99m);
// product.Name = "Desktop";  // Compilation error - init-only property
```

### 3. Concise Syntax (Positional Records)

Positional records provide a compact way to define immutable data.

```csharp
// Positional record - compiler generates constructor, properties, deconstruction
public record Person(string FirstName, string LastName, int Age);

// Equivalent to:
public record Person
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public int Age { get; init; }
    
    public Person(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }
}
```

### 4. Non-Destructive Mutation Using `with`

Create a copy with modified properties without changing the original.

```csharp
public record Person(string Name, int Age);

var john = new Person("John", 30);
var jane = john with { Name = "Jane" };  // Creates new instance

Console.WriteLine(john.Name);  // John (unchanged)
Console.WriteLine(jane.Name);  // Jane
```

### 5. Built-in Methods

Records automatically implement:
- `ToString()` - Returns all property values
- `Equals()` - Value-based comparison
- `GetHashCode()` - Based on property values

```csharp
public record Person(string Name, int Age);

var person = new Person("John", 30);
Console.WriteLine(person);  // Person { Name = John, Age = 30 }
```

---

## Types of Records

### 1. Positional Record (Primary Constructor Syntax)

Most concise syntax for simple data models.

```csharp
public record Person(string FirstName, string LastName, int Age);

// Usage
var person = new Person("John", "Doe", 30);
Console.WriteLine(person.FirstName);  // John
```

### 2. Standard Record with Properties

Traditional property syntax when you need more control.

```csharp
public record Employee
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Department { get; init; }
    public decimal Salary { get; init; }
    
    // Can add methods
    public decimal GetAnnualBonus() => Salary * 0.1m;
}

// Usage
var emp = new Employee
{
    Id = 1,
    Name = "Alice",
    Department = "Engineering",
    Salary = 100000
};
```

### 3. record class vs record struct

#### record class (default)

Reference type with value equality.

```csharp
public record class Person(string Name, int Age);  // Explicit
// or
public record Person(string Name, int Age);        // Implicit (same as above)
```

#### record struct

Value type with value equality (C# 10+).

```csharp
public record struct Point(int X, int Y);

var p1 = new Point(10, 20);
var p2 = p1;  // Copy (value type)

p2 = p2 with { X = 30 };
Console.WriteLine(p1.X);  // 10 (unchanged)
Console.WriteLine(p2.X);  // 30
```

**When to use record struct:**
- Small, simple data structures
- Performance-critical scenarios (stack allocation)
- Avoid heap allocations for small objects

---

## Examples

### Basic Record Example

```csharp
public record Customer(int Id, string Name, string Email);

var customer = new Customer(1, "John Doe", "john@example.com");
Console.WriteLine(customer);
// Output: Customer { Id = 1, Name = John Doe, Email = john@example.com }
```

### Equality Comparison Example

```csharp
public record Address(string Street, string City, string ZipCode);

var address1 = new Address("123 Main St", "New York", "10001");
var address2 = new Address("123 Main St", "New York", "10001");
var address3 = new Address("456 Oak Ave", "Boston", "02101");

Console.WriteLine(address1 == address2);  // True (same values)
Console.WriteLine(address1 == address3);  // False (different values)

// With classes (reference equality)
// address1 == address2 would be False (different references)
```

### Immutability Example

```csharp
public record BankAccount(string AccountNumber, decimal Balance);

var account = new BankAccount("12345", 1000m);

// Cannot modify
// account.Balance = 1500m;  // Compilation error

// Must create new instance
var updatedAccount = account with { Balance = 1500m };

Console.WriteLine(account.Balance);         // 1000 (original unchanged)
Console.WriteLine(updatedAccount.Balance);  // 1500 (new instance)
```

### Using `with` Keyword Example

```csharp
public record Employee(int Id, string Name, string Department, decimal Salary);

var employee = new Employee(1, "Alice", "Engineering", 80000);

// Promote with salary increase
var promotedEmployee = employee with 
{ 
    Department = "Senior Engineering",
    Salary = 100000 
};

Console.WriteLine(employee);
// Employee { Id = 1, Name = Alice, Department = Engineering, Salary = 80000 }

Console.WriteLine(promotedEmployee);
// Employee { Id = 1, Name = Alice, Department = Senior Engineering, Salary = 100000 }
```

### Record Used as DTO in ASP.NET Core Web API

```csharp
// Request DTO
public record CreateUserRequest(string Email, string Password, string FullName);

// Response DTO
public record UserResponse(int Id, string Email, string FullName, DateTime CreatedAt);

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUser(CreateUserRequest request)
    {
        // Validate and create user
        var user = await _userService.CreateUserAsync(request);
        
        var response = new UserResponse(
            user.Id,
            user.Email,
            user.FullName,
            user.CreatedAt
        );
        
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, response);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        
        if (user == null)
            return NotFound();
        
        var response = new UserResponse(
            user.Id,
            user.Email,
            user.FullName,
            user.CreatedAt
        );
        
        return Ok(response);
    }
}
```

---

## Record vs Class Comparison

| Aspect | Record | Class |
|--------|--------|-------|
| **Equality** | Value-based (compares property values) | Reference-based (compares memory addresses) |
| **Default Mutability** | Immutable (`init` properties) | Mutable (`set` properties) |
| **ToString()** | Auto-generated with all properties | Type name only (unless overridden) |
| **Boilerplate Code** | Minimal (compiler-generated) | More code (manual equals/hashcode) |
| **with Expression** | Supported natively | Not supported |
| **Inheritance** | Supports inheritance | Supports inheritance |
| **Primary Use** | Data representation | Behavior + data |
| **Identity** | Based on data | Based on reference |
| **Typical Use Cases** | DTOs, Value Objects, Config | Domain entities, Services, Business logic |

### Code Comparison

**Using Class:**

```csharp
public class PersonClass
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    
    public PersonClass(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }
    
    // Must override for value equality
    public override bool Equals(object obj)
    {
        if (obj is PersonClass other)
            return FirstName == other.FirstName 
                && LastName == other.LastName 
                && Age == other.Age;
        return false;
    }
    
    public override int GetHashCode()
        => HashCode.Combine(FirstName, LastName, Age);
    
    public override string ToString()
        => $"PersonClass {{ FirstName = {FirstName}, LastName = {LastName}, Age = {Age} }}";
}
```

**Using Record:**

```csharp
public record PersonRecord(string FirstName, string LastName, int Age);
```

**Result:** Same functionality with **95% less code**.

---

## When to Use a Record

### 1. When the Object Represents Data, Not Behavior

Records are ideal for data-centric models without complex business logic.

```csharp
// Good: Simple data representation
public record ProductInfo(string Name, decimal Price, string Category);

// Not ideal: Complex behavior
public class ShoppingCart
{
    public List<CartItem> Items { get; private set; }
    
    public void AddItem(CartItem item) { /* logic */ }
    public decimal CalculateTotal() { /* logic */ }
    public void ApplyDiscount(decimal discount) { /* logic */ }
}
```

### 2. DTOs (Data Transfer Objects)

Perfect for transferring data between layers or services.

```csharp
// API Request
public record CreateOrderRequest(
    int CustomerId,
    List<OrderItemDto> Items,
    string ShippingAddress
);

// API Response
public record OrderResponse(
    int OrderId,
    string OrderNumber,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt
);

// Database DTO
public record OrderSummaryDto(
    int OrderId,
    string CustomerName,
    decimal TotalAmount,
    DateTime OrderDate
);
```

### 3. API Request/Response Models

```csharp
// Authentication
public record LoginRequest(string Email, string Password);
public record LoginResponse(string Token, DateTime ExpiresAt, UserInfo User);

// User Info
public record UserInfo(int Id, string Name, string Email, string Role);

[HttpPost("login")]
public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
{
    var result = await _authService.AuthenticateAsync(request.Email, request.Password);
    
    if (!result.Success)
        return Unauthorized();
    
    var response = new LoginResponse(
        result.Token,
        result.ExpiresAt,
        new UserInfo(result.User.Id, result.User.Name, result.User.Email, result.User.Role)
    );
    
    return Ok(response);
}
```

### 4. Immutable Configuration or Settings Objects

```csharp
public record DatabaseSettings
{
    public string ConnectionString { get; init; }
    public int MaxRetryAttempts { get; init; }
    public int CommandTimeout { get; init; }
    public bool EnableLogging { get; init; }
}

public record JwtSettings(
    string SecretKey,
    string Issuer,
    string Audience,
    int ExpirationMinutes
);

// Usage in configuration
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
    }
}
```

### 5. Value Objects in Domain-Driven Design

```csharp
public record Money(decimal Amount, string Currency)
{
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Cannot add different currencies");
        
        return this with { Amount = Amount + other.Amount };
    }
}

public record Address(
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country
);

public record Email(string Value)
{
    public Email(string value) : this(ValidateEmail(value))
    {
    }
    
    private static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("Invalid email format");
        return email;
    }
}
```

### 6. When Value-Based Equality is Required

```csharp
public record Coordinate(double Latitude, double Longitude);

var location1 = new Coordinate(40.7128, -74.0060);  // New York
var location2 = new Coordinate(40.7128, -74.0060);  // New York (same coordinates)

if (location1 == location2)
{
    Console.WriteLine("Same location");  // This executes
}

// With classes, you'd need to override Equals and GetHashCode
// Records give you this for free
```

### 7. When You Want Safe Copying Using `with`

```csharp
public record AppSettings(
    string AppName,
    string Environment,
    int MaxConnections,
    bool EnableLogging
);

var devSettings = new AppSettings("MyApp", "Development", 10, true);
var prodSettings = devSettings with 
{ 
    Environment = "Production",
    MaxConnections = 100,
    EnableLogging = false
};

// devSettings remains unchanged
// prodSettings is a new instance with modified values
```

---

## When NOT to Use Records

### 1. Domain Entities with Identity

Entities have a unique identity (Id) and should use reference equality.

```csharp
// ❌ Don't use record for entities
public record User(int Id, string Name, string Email);

var user1 = new User(1, "John", "john@example.com");
var user2 = new User(1, "John Smith", "john.smith@example.com");

// Problem: user1 != user2 even though they have same Id
// In domain logic, they should be the same user (same Id)

// ✅ Use class instead
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj is User other)
            return Id == other.Id;  // Compare by Id only
        return false;
    }
    
    public override int GetHashCode() => Id.GetHashCode();
}
```

### 2. Objects That Require Frequent State Changes

```csharp
// ❌ Don't use record for mutable state
public record ShoppingCart(List<CartItem> Items, decimal Total);

// Every change creates a new instance - inefficient
cart = cart with { Items = newItems };
cart = cart with { Total = newTotal };

// ✅ Use class with mutable properties
public class ShoppingCart
{
    public List<CartItem> Items { get; private set; } = new();
    public decimal Total { get; private set; }
    
    public void AddItem(CartItem item)
    {
        Items.Add(item);
        Total += item.Price;
    }
    
    public void RemoveItem(CartItem item)
    {
        Items.Remove(item);
        Total -= item.Price;
    }
}
```

### 3. Business Logic-Heavy Models

```csharp
// ❌ Don't use record for complex business logic
public record Order(int Id, List<OrderItem> Items, decimal Total);

// ✅ Use class for rich domain models
public class Order
{
    public int Id { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    public decimal Total { get; private set; }
    public OrderStatus Status { get; private set; }
    
    public void AddItem(OrderItem item)
    {
        ValidateItem(item);
        Items.Add(item);
        RecalculateTotal();
    }
    
    public void ApplyDiscount(decimal percentage)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Cannot apply discount to processed order");
        
        Total *= (1 - percentage / 100);
    }
    
    public void Submit()
    {
        if (!Items.Any())
            throw new InvalidOperationException("Cannot submit empty order");
        
        Status = OrderStatus.Submitted;
    }
    
    private void ValidateItem(OrderItem item) { /* validation logic */ }
    private void RecalculateTotal() { Total = Items.Sum(i => i.Price * i.Quantity); }
}
```

### 4. EF Core Tracked Entities

EF Core tracks entities for change detection. Records' immutability conflicts with this.

```csharp
// ❌ Problematic with EF Core
public record Product
{
    public int Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
}

// Cannot update:
var product = await _context.Products.FindAsync(1);
// product.Price = 99.99m;  // Error: init-only property

// Must create new instance (doesn't work with EF Core tracking)
product = product with { Price = 99.99m };  // Different object reference

// ✅ Use class for EF Core entities
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// EF Core can track changes
var product = await _context.Products.FindAsync(1);
product.Price = 99.99m;  // Tracked by EF Core
await _context.SaveChangesAsync();
```

**Note:** You can use records with EF Core if you treat them as read-only or use them only for queries (not updates).

### 5. When Reference Equality is Important

```csharp
// ❌ Don't use record when you need reference equality
public record CacheEntry(string Key, object Value);

var cache = new Dictionary<CacheEntry, DateTime>();
var entry = new CacheEntry("user:1", userData);
cache[entry] = DateTime.Now;

// Problem: Creating "same" entry won't find it in cache
var sameEntry = new CacheEntry("user:1", userData);
var exists = cache.ContainsKey(sameEntry);  // May not work as expected

// ✅ Use class when reference identity matters
public class CacheEntry
{
    public string Key { get; set; }
    public object Value { get; set; }
}
```

---

## Best Practices

### 1. Prefer Immutability

Design records to be immutable using `init` properties.

```csharp
// ✅ Good: Immutable
public record Customer(int Id, string Name, string Email);

// ❌ Avoid: Mutable record (defeats the purpose)
public record Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

### 2. Use Positional Records for Simple Models

```csharp
// ✅ Good: Concise positional record
public record Point(int X, int Y);

// ❌ Verbose: Unnecessary when positional record suffices
public record Point
{
    public int X { get; init; }
    public int Y { get; init; }
    
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}
```

### 3. Use Standard Syntax When You Need Validation or Additional Members

```csharp
public record Email
{
    public string Value { get; init; }
    
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
            throw new ArgumentException("Invalid email format");
        
        Value = value;
    }
    
    public string Domain => Value.Split('@')[1];
}
```

### 4. Avoid Using Records for Large Mutable Domain Models

```csharp
// ❌ Don't: Complex mutable state
public record OrderAggregate(
    int Id,
    Customer Customer,
    List<OrderItem> Items,
    Payment Payment,
    Shipping Shipping,
    List<OrderEvent> Events
);

// ✅ Do: Use class for aggregates
public class OrderAggregate
{
    public int Id { get; private set; }
    public Customer Customer { get; private set; }
    private List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    
    // Rich domain behavior
    public void AddItem(OrderItem item) { /* logic */ }
    public void RemoveItem(int itemId) { /* logic */ }
    public void ApplyPayment(Payment payment) { /* logic */ }
}
```

### 5. Be Careful When Using Records with ORMs

```csharp
// For EF Core queries (read-only)
public record OrderSummaryDto(int OrderId, string CustomerName, decimal Total);

var summaries = await _context.Orders
    .Select(o => new OrderSummaryDto(o.Id, o.Customer.Name, o.Total))
    .ToListAsync();

// For EF Core entities (use classes)
public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public decimal Total { get; set; }
    // ... tracked entity
}
```

### 6. Use Record Inheritance When Appropriate

```csharp
public record Person(string Name, int Age);
public record Employee(string Name, int Age, string Department, decimal Salary) 
    : Person(Name, Age);

var emp = new Employee("Alice", 30, "Engineering", 100000);
Console.WriteLine(emp);
// Employee { Name = Alice, Age = 30, Department = Engineering, Salary = 100000 }
```

### 7. Consider record struct for Small Value Types

```csharp
// Small coordinate - use record struct
public record struct Point(int X, int Y);

// Avoids heap allocation
var point = new Point(10, 20);  // Stack-allocated
```

---

## Real-world Example

### ASP.NET Core API with Records

**Domain Layer:**

```csharp
// Value Objects (Records)
public record Email(string Value)
{
    public Email(string value) : this(ValidateEmail(value)) { }
    
    private static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("Invalid email");
        return email.ToLowerInvariant();
    }
}

public record Address(string Street, string City, string State, string ZipCode);

// Entity (Class - has identity and behavior)
public class Customer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Customer(string name, Email email, Address address)
    {
        Name = name;
        Email = email;
        Address = address;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void UpdateAddress(Address newAddress)
    {
        Address = newAddress;
    }
}
```

**API Layer:**

```csharp
// Request/Response DTOs (Records)
public record CreateCustomerRequest(
    string Name,
    string Email,
    string Street,
    string City,
    string State,
    string ZipCode
);

public record UpdateAddressRequest(
    string Street,
    string City,
    string State,
    string ZipCode
);

public record CustomerResponse(
    int Id,
    string Name,
    string Email,
    AddressResponse Address,
    DateTime CreatedAt
);

public record AddressResponse(string Street, string City, string State, string ZipCode);

// Controller
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    
    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    [HttpPost]
    public async Task<ActionResult<CustomerResponse>> CreateCustomer(
        CreateCustomerRequest request)
    {
        try
        {
            var email = new Email(request.Email);
            var address = new Address(
                request.Street,
                request.City,
                request.State,
                request.ZipCode
            );
            
            var customer = new Customer(request.Name, email, address);
            await _customerService.CreateAsync(customer);
            
            var response = new CustomerResponse(
                customer.Id,
                customer.Name,
                customer.Email.Value,
                new AddressResponse(
                    customer.Address.Street,
                    customer.Address.City,
                    customer.Address.State,
                    customer.Address.ZipCode
                ),
                customer.CreatedAt
            );
            
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponse>> GetCustomer(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        
        if (customer == null)
            return NotFound();
        
        var response = new CustomerResponse(
            customer.Id,
            customer.Name,
            customer.Email.Value,
            new AddressResponse(
                customer.Address.Street,
                customer.Address.City,
                customer.Address.State,
                customer.Address.ZipCode
            ),
            customer.CreatedAt
        );
        
        return Ok(response);
    }
    
    [HttpPut("{id}/address")]
    public async Task<IActionResult> UpdateAddress(int id, UpdateAddressRequest request)
    {
        var customer = await _customerService.GetByIdAsync(id);
        
        if (customer == null)
            return NotFound();
        
        var newAddress = new Address(
            request.Street,
            request.City,
            request.State,
            request.ZipCode
        );
        
        customer.UpdateAddress(newAddress);
        await _customerService.UpdateAsync(customer);
        
        return NoContent();
    }
}
```

**Key Observations:**
- **Records** for DTOs (Request/Response models)
- **Records** for Value Objects (Email, Address)
- **Class** for Entity (Customer - has identity and behavior)
- Clean separation of concerns
- Immutability where appropriate
- Type safety with value objects

---

## Summary

### Key Takeaways

1. **Records are for data, classes are for behavior**
2. **Value equality** is automatic with records
3. **Immutability by default** using `init` properties
4. **Concise syntax** reduces boilerplate code
5. **`with` expression** enables non-destructive mutation
6. **Perfect for DTOs**, value objects, and configuration
7. **Not suitable** for domain entities, mutable state, or EF Core tracked entities

### Quick Decision Guide: Record vs Class

```
┌─────────────────────────────────────────────────────────────┐
│                   Should I use a Record?                    │
└─────────────────────────────────────────────────────────────┘
                            │
                            ▼
                ┌───────────────────────┐
                │ Does it represent     │
                │ data without identity?│
                └───────────────────────┘
                        │        │
                   Yes  │        │  No
                        ▼        ▼
                    ┌──────┐  ┌──────┐
                    │ USE  │  │ USE  │
                    │RECORD│  │CLASS │
                    └──────┘  └──────┘
                    
Use RECORD for:                      Use CLASS for:
✅ DTOs                              ✅ Domain Entities
✅ API Request/Response              ✅ Business Logic
✅ Value Objects                     ✅ Mutable State
✅ Configuration                     ✅ EF Core Entities
✅ Immutable Data                    ✅ Identity-based Objects
✅ Value Equality                    ✅ Reference Equality
```

### Comparison at a Glance

| Scenario | Record | Class |
|----------|:------:|:-----:|
| DTO / API Model | ✅ | ❌ |
| Value Object | ✅ | ❌ |
| Configuration | ✅ | ❌ |
| Domain Entity | ❌ | ✅ |
| EF Core Entity | ❌ | ✅ |
| Business Logic | ❌ | ✅ |
| Mutable State | ❌ | ✅ |

---

## Further Reading

- [Microsoft Docs: Records (C# reference)](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record)
- [C# 9.0: Records](https://devblogs.microsoft.com/dotnet/c-9-0-on-the-record/)
- [Value Objects in Domain-Driven Design](https://martinfowler.com/bliki/ValueObject.html)

---

**Last Updated:** February 2026  
**C# Version:** 9.0+ (.NET 5+)  
**Framework:** .NET 8+
