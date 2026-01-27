# Dependency Injection Lifetimes in .NET (Interview Guide)

Dependency Injection (DI) lifetimes define how long an object lives in memory and how it is shared within an application.
Choosing the correct lifetime is critical for performance, memory management, and data consistency.

---

## 1. Singleton

### Definition
Singleton creates **one single instance** of a service for the **entire application lifetime**.
The same instance is shared across **all requests and users**.

### When to Use
- Application configuration
- Logging services
- Read-only or shared caching
- Stateless shared utilities

### Why Singleton Works Here
- Configuration and logging do not depend on request data
- Thread-safe and efficient
- No repeated object creation

### Example
```csharp
services.AddSingleton<IConfigurationService, ConfigurationService>();
services.AddSingleton<ILoggerService, LoggerService>();
```

### Risks of Wrong Usage
- Storing user/session data causes data corruption
- Injecting Scoped services inside Singleton causes runtime errors
- Long-lived memory usage if misused

---

## 2. Scoped

### Definition
Scoped services are created **once per request** and shared across that request.
A new instance is created for each new HTTP request.

### When to Use
- Database transactions
- Add / Update / Delete operations
- Entity Framework DbContext
- Unit of Work pattern
- Request-level business logic

### Why Scoped Is Ideal
- Ensures transaction consistency
- Prevents data mixing between users
- Automatically disposed after request ends

### Example
```csharp
services.AddScoped<AppDbContext>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
```

### Interview Tip
> Transactions should always be scoped to avoid concurrency issues.

---

## 3. Transient

### Definition
Transient services create a **new instance every time** they are requested.
They are short-lived and garbage collected quickly.

### When to Use
- Stateless services
- Validators
- Formatters
- Mappers
- Calculation helpers

### Why Transient Is Useful
- No shared state
- Safe for multi-threaded environments
- Ideal for lightweight logic

### Example
```csharp
services.AddTransient<IPriceCalculator, PriceCalculator>();
```

### Risks of Wrong Usage
- Performance issues if object creation is expensive
- Cannot maintain request-level state
- Not suitable for DB or transaction logic

---

## Lifetime Comparison Table

| Lifetime   | Instance Scope        | Best For                     | Avoid Using For            |
|-----------|----------------------|------------------------------|----------------------------|
| Singleton | Entire application   | Config, Logging, Cache       | User data, Transactions   |
| Scoped    | Per HTTP request     | DB, Transactions             | App-wide shared services  |
| Transient | Per usage            | Stateless helpers            | Heavy objects             |

---

## Common Interview Rules
- Never inject **Scoped** into **Singleton**
- DbContext should always be **Scoped**
- Static objects behave like **Singleton**
- Transient increases GC pressure if overused

---

## One-Line Interview Summary
> Singleton is for application-wide services, Scoped is for request-level transactional work, and Transient is for lightweight stateless operations.

---

## Final Memory Trick
- Singleton → Application lifetime  
- Scoped → Request lifetime  
- Transient → Method lifetime  
