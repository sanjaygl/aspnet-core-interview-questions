# Design Patterns in C# - Complete Guide

## Introduction

### What are Design Patterns?

Design patterns are reusable solutions to commonly occurring problems in software design. They represent best practices evolved over time by experienced developers and provide a shared vocabulary for discussing design solutions.

In enterprise .NET applications, design patterns help:
- **Reduce complexity** through proven architectural solutions
- **Improve maintainability** with well-structured, loosely coupled code
- **Enhance team communication** through common design vocabulary
- **Accelerate development** by leveraging established patterns
- **Support SOLID principles** and clean architecture practices

### Why Design Patterns Matter in Enterprise .NET

Modern enterprise applications built with ASP.NET Core, microservices, and cloud-native architectures benefit significantly from design patterns:

- **Dependency Injection** (built into .NET Core) implements multiple patterns
- **Microservices** architecture relies heavily on patterns like Facade, Proxy, and Observer
- **Domain-Driven Design** leverages patterns like Strategy, Factory, and Repository
- **Clean Architecture** uses patterns to maintain separation of concerns
- **Testability** is enhanced through patterns that promote loose coupling

---

## Design Pattern Classification

### Creational Patterns
Focus on object creation mechanisms, trying to create objects in a manner suitable to the situation.

### Structural Patterns
Deal with object composition, creating relationships between objects to form larger structures.

### Behavioral Patterns
Concerned with communication between objects and the assignment of responsibilities.

---

## Pattern Index

### Creational Patterns
- [Singleton](creational/singleton.md) - Ensure a class has only one instance
- [Factory Method](creational/factory-method.md) - Create objects without specifying exact classes
- [Abstract Factory](creational/abstract-factory.md) - Create families of related objects
- [Builder](creational/builder.md) - Construct complex objects step by step
- [Prototype](creational/prototype.md) - Create objects by cloning existing instances

### Structural Patterns
- [Adapter](structural/adapter.md) - Allow incompatible interfaces to work together
- [Decorator](structural/decorator.md) - Add behavior to objects dynamically
- [Facade](structural/facade.md) - Provide simplified interface to complex subsystem
- [Composite](structural/composite.md) - Compose objects into tree structures
- [Proxy](structural/proxy.md) - Provide placeholder/surrogate for another object
- [Bridge](structural/bridge.md) - Separate abstraction from implementation

### Behavioral Patterns
- [Strategy](behavioral/strategy.md) - Define family of algorithms and make them interchangeable
- [Observer](behavioral/observer.md) - Define one-to-many dependency between objects
- [Command](behavioral/command.md) - Encapsulate requests as objects
- [Template Method](behavioral/template-method.md) - Define skeleton of algorithm in base class
- [Chain of Responsibility](behavioral/chain-of-responsibility.md) - Pass requests along chain of handlers
- [Mediator](behavioral/mediator.md) - Define how objects interact with each other

---

## Cross-Pattern Comparison

| Comparison | Pattern A | Pattern B | Key Difference | When to Use A | When to Use B |
|------------|-----------|-----------|----------------|---------------|---------------|
| **Object Creation** | Factory Method | Abstract Factory | Single vs Family | One product type | Related product families |
| **Algorithm Selection** | Strategy | Template Method | Composition vs Inheritance | Runtime algorithm change | Fixed algorithm structure |
| **Interface Adaptation** | Adapter | Facade | Single vs Multiple | Incompatible interface | Complex subsystem |
| **Behavior Addition** | Decorator | Inheritance | Runtime vs Compile-time | Dynamic behavior | Static behavior extension |
| **Object Access** | Proxy | Decorator | Control vs Enhancement | Access control/lazy loading | Feature enhancement |
| **Request Handling** | Chain of Responsibility | Command | Sequential vs Encapsulation | Multiple handlers | Request as object |

---

## Cross-Pattern Interview Questions

### 1. **Scenario: You're building a payment processing system that needs to support multiple payment methods (Credit Card, PayPal, Bank Transfer) and multiple currencies. Which patterns would you combine and why?**

**Answer:**
```csharp
// Combine Abstract Factory + Strategy + Adapter

// Abstract Factory for payment method families
public interface IPaymentFactory
{
    IPaymentProcessor CreateProcessor();
    ICurrencyConverter CreateConverter();
}

// Strategy for different payment algorithms
public interface IPaymentProcessor
{
    Task<PaymentResult> ProcessAsync(PaymentRequest request);
}

// Adapter for third-party payment services
public class PayPalAdapter : IPaymentProcessor
{
    private readonly PayPalSDK _paypalSdk;
    
    public async Task<PaymentResult> ProcessAsync(PaymentRequest request)
    {
        // Adapt our interface to PayPal's interface
        var paypalRequest = AdaptToPayPalFormat(request);
        var result = await _paypalSdk.ProcessPaymentAsync(paypalRequest);
        return AdaptFromPayPalFormat(result);
    }
}
```

**Why this combination:**
- **Abstract Factory**: Creates families of related payment objects (processor + converter)
- **Strategy**: Allows runtime selection of payment algorithms
- **Adapter**: Integrates third-party payment services with different interfaces

### 2. **In a microservices architecture, you need to implement cross-cutting concerns like logging, authentication, and caching. Which patterns would you use and how?**

**Answer:**
```csharp
// Decorator for cross-cutting concerns
public class LoggingDecorator<T> : IService<T>
{
    private readonly IService<T> _inner;
    private readonly ILogger _logger;
    
    public async Task<T> ExecuteAsync(Request request)
    {
        _logger.LogInformation("Executing {Service}", typeof(T).Name);
        try
        {
            var result = await _inner.ExecuteAsync(request);
            _logger.LogInformation("Completed {Service}", typeof(T).Name);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in {Service}", typeof(T).Name);
            throw;
        }
    }
}

// Chain of Responsibility for middleware pipeline
public abstract class MiddlewareBase
{
    protected MiddlewareBase _next;
    
    public MiddlewareBase SetNext(MiddlewareBase next)
    {
        _next = next;
        return next;
    }
    
    public abstract Task<Response> HandleAsync(Request request);
}

// Proxy for service discovery and load balancing
public class ServiceProxy : IRemoteService
{
    private readonly IServiceDiscovery _discovery;
    
    public async Task<T> CallAsync<T>(string serviceName, Request request)
    {
        var serviceUrl = await _discovery.DiscoverAsync(serviceName);
        // Implement circuit breaker, retry logic, etc.
        return await CallRemoteServiceAsync<T>(serviceUrl, request);
    }
}
```

**Pattern Usage:**
- **Decorator**: Cross-cutting concerns (logging, caching, authentication)
- **Chain of Responsibility**: Middleware pipeline processing
- **Proxy**: Service discovery, circuit breaker, load balancing

### 3. **You're refactoring a monolithic application to microservices. The current code has tight coupling and violates SOLID principles. Which patterns help with this transition?**

**Answer:**

**Before (Monolithic, Tight Coupling):**
```csharp
public class OrderService
{
    public void ProcessOrder(Order order)
    {
        // Violates SRP - doing too many things
        ValidateOrder(order);
        CalculatePrice(order);
        SendEmail(order.CustomerEmail, "Order confirmed");
        UpdateInventory(order.Items);
        LogOrder(order);
    }
}
```

**After (Microservices, Loose Coupling):**
```csharp
// Strategy Pattern - Different pricing strategies
public interface IPricingStrategy
{
    decimal Calculate(Order order);
}

// Observer Pattern - Event-driven communication
public interface IOrderEventPublisher
{
    Task PublishOrderCreatedAsync(OrderCreatedEvent orderEvent);
}

// Factory Pattern - Create appropriate services
public interface IServiceFactory
{
    INotificationService CreateNotificationService(NotificationType type);
    IPricingStrategy CreatePricingStrategy(CustomerType customerType);
}

// Facade Pattern - Simplify complex operations
public class OrderFacade
{
    private readonly IOrderValidator _validator;
    private readonly IPricingStrategy _pricingStrategy;
    private readonly IOrderEventPublisher _eventPublisher;
    
    public async Task<OrderResult> ProcessOrderAsync(Order order)
    {
        // Single responsibility - orchestrate the process
        await _validator.ValidateAsync(order);
        order.TotalPrice = _pricingStrategy.Calculate(order);
        
        var result = await SaveOrderAsync(order);
        await _eventPublisher.PublishOrderCreatedAsync(new OrderCreatedEvent(order));
        
        return result;
    }
}
```

**Patterns for Microservices Transition:**
- **Strategy**: Replace conditional logic with pluggable algorithms
- **Observer**: Event-driven communication between services
- **Factory**: Create service instances based on configuration
- **Facade**: Simplify complex service interactions
- **Adapter**: Integrate legacy systems during transition

### 4. **How do you handle the "God Object" anti-pattern using design patterns?**

**Answer:**

**God Object Anti-Pattern:**
```csharp
// BAD: God object doing everything
public class UserManager
{
    public void CreateUser(User user) { }
    public void ValidateUser(User user) { }
    public void SendWelcomeEmail(User user) { }
    public void LogUserActivity(User user) { }
    public void UpdateUserPreferences(User user) { }
    public void GenerateUserReport(User user) { }
    public void BackupUserData(User user) { }
    // ... 50+ more methods
}
```

**Solution using Multiple Patterns:**
```csharp
// Command Pattern - Encapsulate operations
public interface IUserCommand
{
    Task ExecuteAsync(User user);
}

public class CreateUserCommand : IUserCommand
{
    private readonly IUserRepository _repository;
    private readonly IUserValidator _validator;
    
    public async Task ExecuteAsync(User user)
    {
        await _validator.ValidateAsync(user);
        await _repository.SaveAsync(user);
    }
}

// Strategy Pattern - Different validation strategies
public interface IUserValidator
{
    Task<ValidationResult> ValidateAsync(User user);
}

// Observer Pattern - Decouple side effects
public class UserService
{
    private readonly IUserEventPublisher _eventPublisher;
    
    public async Task CreateUserAsync(User user)
    {
        // Core responsibility only
        await _repository.SaveAsync(user);
        
        // Publish event for side effects
        await _eventPublisher.PublishAsync(new UserCreatedEvent(user));
    }
}

// Facade Pattern - Coordinate operations
public class UserFacade
{
    private readonly Dictionary<Type, IUserCommand> _commands;
    
    public async Task ExecuteAsync<T>(User user) where T : IUserCommand
    {
        var command = _commands[typeof(T)];
        await command.ExecuteAsync(user);
    }
}
```

### 5. **In ASP.NET Core, which design patterns are built-in and how do you extend them?**

**Answer:**

**Built-in Patterns in ASP.NET Core:**

1. **Dependency Injection (Service Locator + Factory)**
```csharp
// Built-in DI container uses Factory pattern
services.AddScoped<IUserService, UserService>();
services.AddSingleton<ICacheService, RedisCacheService>();

// Extending with Factory Pattern
services.AddScoped<Func<CacheType, ICacheService>>(provider => cacheType =>
{
    return cacheType switch
    {
        CacheType.Memory => provider.GetService<IMemoryCacheService>(),
        CacheType.Redis => provider.GetService<IRedisCacheService>(),
        _ => throw new ArgumentException("Invalid cache type")
    };
});
```

2. **Middleware Pipeline (Chain of Responsibility)**
```csharp
// Built-in middleware chain
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

// Custom middleware extending the pattern
public class CustomMiddleware
{
    private readonly RequestDelegate _next;
    
    public async Task InvokeAsync(HttpContext context)
    {
        // Pre-processing
        await _next(context); // Chain to next middleware
        // Post-processing
    }
}
```

3. **Options Pattern (Strategy + Builder)**
```csharp
// Built-in options configuration
services.Configure<DatabaseOptions>(configuration.GetSection("Database"));

// Extending with Strategy pattern
services.AddScoped<IDatabaseStrategy>(provider =>
{
    var options = provider.GetService<IOptions<DatabaseOptions>>();
    return options.Value.Provider switch
    {
        "SqlServer" => new SqlServerStrategy(options.Value.ConnectionString),
        "PostgreSQL" => new PostgreSqlStrategy(options.Value.ConnectionString),
        _ => throw new NotSupportedException()
    };
});
```

4. **Model Binding (Adapter Pattern)**
```csharp
// Custom model binder extending built-in adapter pattern
public class CustomModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        // Adapt request data to model
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        // Custom binding logic
        return Task.CompletedTask;
    }
}
```

### 6. **Which patterns commonly violate SOLID principles and how do you fix them?**

**Answer:**

| Pattern | SOLID Violation | Problem | Solution |
|---------|----------------|---------|----------|
| **Singleton** | DIP, SRP | Hard dependencies, multiple responsibilities | Use DI container, split responsibilities |
| **Factory Method** | OCP | Adding new types requires modification | Use Abstract Factory or Registry |
| **Observer** | SRP | Observers doing too much | Use Mediator pattern |
| **Template Method** | LSP | Subclasses changing behavior unexpectedly | Use Strategy pattern |

**Example - Fixing Singleton Violations:**
```csharp
// BAD: Violates DIP and SRP
public class DatabaseManager
{
    private static DatabaseManager _instance;
    private string _connectionString = "hardcoded"; // DIP violation
    
    public void SaveUser(User user) { } // SRP violation
    public void SaveOrder(Order order) { } // SRP violation
    public void BackupDatabase() { } // SRP violation
}

// GOOD: Following SOLID principles
public interface IRepository<T>
{
    Task SaveAsync(T entity);
}

public interface IBackupService
{
    Task BackupAsync();
}

// Register as singleton in DI container
services.AddSingleton<IRepository<User>, UserRepository>();
services.AddSingleton<IBackupService, DatabaseBackupService>();
```

---

## Quick Revision Cheat Sheet

### Creational Patterns
- **Singleton**: One instance only - `services.AddSingleton<T>()`
- **Factory Method**: Create without specifying class - `CreateLogger(type)`
- **Abstract Factory**: Create families - `CreateDatabaseFactory(provider)`
- **Builder**: Step-by-step construction - `QueryBuilder.Select().Where().Build()`
- **Prototype**: Clone existing - `user.Clone()`

### Structural Patterns
- **Adapter**: Interface compatibility - `LegacySystemAdapter`
- **Decorator**: Add behavior dynamically - `LoggingDecorator<T>`
- **Facade**: Simplify complex system - `PaymentFacade`
- **Composite**: Tree structures - `MenuComponent`
- **Proxy**: Control access - `LazyLoadingProxy<T>`
- **Bridge**: Separate abstraction/implementation - `DatabaseBridge`

### Behavioral Patterns
- **Strategy**: Interchangeable algorithms - `IPricingStrategy`
- **Observer**: One-to-many notifications - `IEventPublisher`
- **Command**: Encapsulate requests - `ICommand<T>`
- **Template Method**: Algorithm skeleton - `abstract ProcessData()`
- **Chain of Responsibility**: Pass along chain - `MiddlewarePipeline`
- **Mediator**: Centralized communication - `IMediator`

### Common Use Cases
- **API Gateway**: Facade + Proxy
- **Microservices Communication**: Observer + Mediator
- **Cross-cutting Concerns**: Decorator + Chain of Responsibility
- **Plugin Architecture**: Factory + Strategy
- **Legacy Integration**: Adapter + Facade
- **Configuration Management**: Builder + Strategy