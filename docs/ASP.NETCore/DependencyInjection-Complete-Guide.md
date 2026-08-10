# Dependency Injection in ASP.NET Core – Lifetimes, Best Practices & Real Examples

## Introduction

Dependency Injection (DI) is one of the core design principles in ASP.NET Core, baked directly into the framework. It solves critical problems like tight coupling between classes, difficulty in unit testing, and managing object lifetimes. Understanding DI is essential for building maintainable, testable applications and is a frequent topic in technical interviews. This guide covers service lifetimes, best practices, common pitfalls, and real-world scenarios you'll encounter in production.

---

## What is Dependency Injection?

**Dependency Injection** is a design pattern where a class receives its dependencies from external sources rather than creating them itself. Instead of a class using `new` to instantiate dependencies, those dependencies are "injected" through the constructor, making the code more modular, testable, and maintainable.

### Real-World Analogy

Think of ordering food at a restaurant. You don't go into the kitchen, gather ingredients, and cook the meal yourself (creating dependencies). Instead, you tell the waiter what you want (declare dependencies), and the kitchen (DI container) prepares and delivers it to you (injects dependencies). This separation allows the restaurant to change recipes, suppliers, or chefs without affecting your experience.

---

## Built-in DI Container in ASP.NET Core

ASP.NET Core includes a **built-in DI container** (also called the IoC container or Service Provider) that manages object creation and lifetime automatically.

### Why ASP.NET Core Has DI Built-In

- **Framework Philosophy**: ASP.NET Core was designed from the ground up to be modular, testable, and cloud-ready
- **Consistency**: All framework components (controllers, middleware, Razor Pages, etc.) use the same DI mechanism
- **Simplified Testing**: Easily swap real implementations with mocks/fakes in unit tests
- **Automatic Disposal**: The container manages object lifetimes and disposes of `IDisposable` objects automatically

### How Services Are Registered and Resolved

1. **Registration**: Services are registered in `Program.cs` using the `IServiceCollection`
2. **Resolution**: When a class (like a controller) needs a dependency, the DI container automatically creates and injects it
3. **Disposal**: When the scope ends (e.g., request completes), the container disposes of services implementing `IDisposable`

```csharp
// Registration
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Resolution (automatic in constructors)
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    
    // DI container injects IProductRepository automatically
    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }
}
```

---

## Service Lifetimes (Very Important)

Understanding service lifetimes is **critical** for avoiding memory leaks, concurrency issues, and unexpected behavior. ASP.NET Core supports three lifetimes:

### 1. Transient

**Lifetime**: Created **every time** they are requested

**Use When**: The service is lightweight and stateless

**Characteristics**:
- New instance per injection
- Multiple instances in the same request if injected multiple times
- Disposed at the end of the scope

**Example Scenario**: Lightweight utility classes, data converters, validators

```csharp
builder.Services.AddTransient<IEmailSender, EmailSender>();
```

**Memory Impact**: ⚠️ Can create many objects if injected frequently in the same request

---

### 2. Scoped

**Lifetime**: Created **once per HTTP request** (or scope)

**Use When**: You need the same instance throughout a single request

**Characteristics**:
- Same instance within a request
- Different instance for each new request
- Disposed when the request completes

**Example Scenario**: Database contexts (EF Core DbContext), repository pattern, unit of work

```csharp
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

**Why Scoped for DbContext?** Ensures all repositories in a request share the same database context, enabling proper transaction management and change tracking.

---

### 3. Singleton

**Lifetime**: Created **once** for the entire application lifetime

**Use When**: The service is stateless and thread-safe

**Characteristics**:
- Same instance for all requests
- Created on first request (or at startup if needed)
- Disposed when the application shuts down

**Example Scenario**: Configuration, caching services, logging, shared state managers

```csharp
builder.Services.AddSingleton<IAppSettings, AppSettings>();
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
```

**⚠️ Warning**: Singleton services must be **thread-safe** since they're shared across all requests!

---

### Lifetime Comparison Table

| Lifetime | Created | Shared Across | Disposed | Best For |
|----------|---------|---------------|----------|----------|
| **Transient** | Every injection | Nothing (always new) | End of scope | Lightweight, stateless utilities |
| **Scoped** | Once per request | Single request | End of request | DbContext, repositories, request-specific data |
| **Singleton** | Once per app | All requests | App shutdown | Configuration, caching, logging |

---

### Common Lifetime Mistakes

#### ❌ Mistake 1: Injecting Scoped Service into Singleton

```csharp
// BAD: Singleton depends on Scoped service
public class MySingleton
{
    private readonly MyDbContext _dbContext; // DbContext is scoped!
    
    public MySingleton(MyDbContext dbContext) // ❌ WRONG!
    {
        _dbContext = dbContext; // Captive dependency!
    }
}
```

**Problem**: The scoped `DbContext` becomes "captive" in the singleton, living for the entire application lifetime instead of per request. This causes stale data, concurrency issues, and memory leaks.

**Solution**: Use `IServiceProvider` to resolve scoped services on-demand or refactor dependencies.

---

#### ❌ Mistake 2: Using Transient for Expensive Objects

```csharp
// BAD: Creating expensive objects every time
builder.Services.AddTransient<IHttpClient, HttpClient>(); // ❌ Creates new HttpClient each time
```

**Problem**: `HttpClient` is expensive to create and can exhaust socket connections.

**Solution**: Use `IHttpClientFactory` or register as Singleton if properly designed.

```csharp
// GOOD: Use IHttpClientFactory
builder.Services.AddHttpClient<IExternalApiClient, ExternalApiClient>();
```

---

#### ❌ Mistake 3: Singleton with Mutable State

```csharp
// BAD: Singleton with mutable state
public class UserContext // Registered as Singleton
{
    public string CurrentUserId { get; set; } // ❌ Shared across all requests!
}
```

**Problem**: All users would share the same `CurrentUserId`, causing data leaks.

**Solution**: Use Scoped lifetime for request-specific data.

```csharp
builder.Services.AddScoped<IUserContext, UserContext>(); // ✅ Correct
```

---

## Service Registration Examples

### Basic Registration

```csharp
var builder = WebApplication.CreateBuilder(args);

// Transient: New instance every time
builder.Services.AddTransient<IGuidGenerator, GuidGenerator>();

// Scoped: Same instance per HTTP request
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Singleton: Single instance for app lifetime
builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>();
builder.Services.AddSingleton<ICacheManager, MemoryCacheManager>();

var app = builder.Build();
```

---

### Multiple Implementations

```csharp
public interface INotificationService
{
    Task SendAsync(string message);
}

public class EmailNotificationService : INotificationService
{
    public Task SendAsync(string message) 
        => Task.CompletedTask; // Send email
}

public class SmsNotificationService : INotificationService
{
    public Task SendAsync(string message) 
        => Task.CompletedTask; // Send SMS
}

// Register multiple implementations
builder.Services.AddScoped<INotificationService, EmailNotificationService>();
builder.Services.AddScoped<INotificationService, SmsNotificationService>();

// Inject all implementations
public class NotificationManager
{
    private readonly IEnumerable<INotificationService> _notificationServices;
    
    public NotificationManager(IEnumerable<INotificationService> notificationServices)
    {
        _notificationServices = notificationServices; // Gets both implementations
    }
    
    public async Task NotifyAllAsync(string message)
    {
        foreach (var service in _notificationServices)
        {
            await service.SendAsync(message);
        }
    }
}
```

---

### Registering Concrete Classes (Without Interface)

```csharp
// Register concrete class directly
builder.Services.AddScoped<ProductService>(); // No interface

// Usage
public class OrderController : ControllerBase
{
    private readonly ProductService _productService;
    
    public OrderController(ProductService productService)
    {
        _productService = productService;
    }
}
```

> **Note**: Prefer interface-based registration for better testability and loose coupling.

---

### Factory-Based Registration

```csharp
// Register with factory method for complex initialization
builder.Services.AddSingleton<IPaymentProcessor>(serviceProvider =>
{
    var config = serviceProvider.GetRequiredService<IConfiguration>();
    var apiKey = config["Payment:ApiKey"];
    var timeout = TimeSpan.FromSeconds(30);
    
    return new StripePaymentProcessor(apiKey, timeout);
});
```

---

## Constructor Injection

**Constructor Injection** is the **preferred** method of DI in ASP.NET Core because it:
- Makes dependencies explicit and required
- Enables immutability (dependencies as `readonly` fields)
- Simplifies unit testing
- Fails fast if dependencies are missing

### Controller Example

```csharp
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
}

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _context.Products.ToListAsync();
    
    public async Task<Product?> GetByIdAsync(int id)
        => await _context.Products.FindAsync(id);
    
    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }
}
```

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductsController> _logger;
    
    // Constructor injection - dependencies are injected automatically
    public ProductsController(
        IProductRepository repository,
        ILogger<ProductsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        _logger.LogInformation("Fetching all products");
        var products = await _repository.GetAllAsync();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        
        if (product == null)
        {
            _logger.LogWarning("Product {ProductId} not found", id);
            return NotFound();
        }
        
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        await _repository.AddAsync(product);
        _logger.LogInformation("Product {ProductId} created", product.Id);
        
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
}
```

### Why Constructor Injection Is Preferred

✅ **Explicit Dependencies**: Clear what a class needs to function  
✅ **Immutability**: Dependencies can be `readonly`  
✅ **Fail Fast**: Application won't start if dependencies are missing  
✅ **Testability**: Easy to pass mocks/fakes in unit tests  
✅ **No Service Locator**: Avoids anti-pattern of requesting dependencies inside methods

---

## DI Resolution Flow

Understanding how ASP.NET Core resolves dependencies helps debug issues and optimize performance.

### High-Level Resolution Process

```
1. HTTP Request Arrives
        ↓
2. ASP.NET Core Creates Request Scope
        ↓
3. Routing Determines Endpoint (e.g., ProductsController)
        ↓
4. DI Container Analyzes Constructor
   - Needs: IProductRepository, ILogger<ProductsController>
        ↓
5. Container Resolves Dependencies
   - IProductRepository → ProductRepository (Scoped)
   - ILogger<ProductsController> → Logger instance (Singleton)
        ↓
6. Container Creates ProductRepository
   - Needs: ApplicationDbContext
   - Resolves DbContext (Scoped, same instance for this request)
        ↓
7. Container Instantiates ProductsController
   - Passes resolved dependencies to constructor
        ↓
8. Action Method Executes
        ↓
9. Response Sent to Client
        ↓
10. Request Scope Disposed
    - Scoped services disposed (DbContext, Repository)
    - Transient services disposed
    - Singleton services remain alive
```

### Example: Dependency Graph

```csharp
// Registration
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSingleton<ILogger<OrderService>, Logger<OrderService>>();

// OrderService depends on multiple services
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly IProductRepository _productRepo;
    private readonly ILogger<OrderService> _logger;
    
    public OrderService(
        IOrderRepository orderRepo,
        IProductRepository productRepo,
        ILogger<OrderService> logger)
    {
        _orderRepo = orderRepo;      // Scoped (created once per request)
        _productRepo = productRepo;  // Scoped (created once per request)
        _logger = logger;            // Singleton (reused from app start)
    }
}

// Dependency Resolution:
// OrderService → IOrderRepository → OrderRepository → ApplicationDbContext
//             → IProductRepository → ProductRepository → ApplicationDbContext (same instance!)
//             → ILogger (Singleton, reused)
```

**Key Point**: Both `OrderRepository` and `ProductRepository` receive the **same** `ApplicationDbContext` instance because they're resolved within the same request scope.

---

## DI in Middleware

Middleware components run for every request, so DI works slightly differently than in controllers.

### Middleware Constructor Injection (Singleton Services Only)

```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger; // Singleton OK
    
    // Only singleton services can be injected in constructor
    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Request: {Method} {Path}", 
            context.Request.Method, 
            context.Request.Path);
        
        await _next(context);
        
        _logger.LogInformation("Response: {StatusCode}", 
            context.Response.StatusCode);
    }
}
```

---

### Injecting Scoped Services in Middleware (InvokeAsync Method)

```csharp
public class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;
    
    public TenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    // Scoped services injected in InvokeAsync method
    public async Task InvokeAsync(
        HttpContext context,
        ITenantService tenantService,      // Scoped ✅
        ApplicationDbContext dbContext)    // Scoped ✅
    {
        var tenantId = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        
        if (!string.IsNullOrEmpty(tenantId))
        {
            await tenantService.SetCurrentTenantAsync(tenantId);
        }
        
        await _next(context);
    }
}
```

**Why InvokeAsync?** 
- Middleware instances are created once (singleton-like lifetime)
- Constructor-injected services would be captive
- `InvokeAsync` is called per request, allowing scoped service injection

---

### Middleware Registration

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ApplicationDbContext>();

var app = builder.Build();

// Register middleware
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<TenantResolutionMiddleware>();

app.MapControllers();
app.Run();
```

---

## Real-World Use Cases

### 1. Logging

```csharp
// Registration (built-in)
builder.Services.AddLogging();

// Usage
public class OrderService
{
    private readonly ILogger<OrderService> _logger;
    
    public OrderService(ILogger<OrderService> logger)
    {
        _logger = logger;
    }
    
    public async Task ProcessOrderAsync(Order order)
    {
        _logger.LogInformation("Processing order {OrderId}", order.Id);
        
        try
        {
            // Process order logic
            _logger.LogInformation("Order {OrderId} processed successfully", order.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process order {OrderId}", order.Id);
            throw;
        }
    }
}
```

---

### 2. Repository Pattern

```csharp
// Repository interface
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

// Generic repository implementation
public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;
    
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.ToListAsync();
    
    public async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);
    
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

// Registration
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Usage
public class ProductService
{
    private readonly IRepository<Product> _productRepository;
    
    public ProductService(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
}
```

---

### 3. External API Clients

```csharp
public interface IWeatherApiClient
{
    Task<WeatherData> GetWeatherAsync(string city);
}

public class WeatherApiClient : IWeatherApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherApiClient> _logger;
    
    public WeatherApiClient(HttpClient httpClient, ILogger<WeatherApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        _logger.LogInformation("Fetching weather for {City}", city);
        
        var response = await _httpClient.GetAsync($"/weather?city={city}");
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<WeatherData>()
            ?? throw new InvalidOperationException("Failed to deserialize weather data");
    }
}

// Registration with HttpClientFactory
builder.Services.AddHttpClient<IWeatherApiClient, WeatherApiClient>(client =>
{
    client.BaseAddress = new Uri("https://api.weather.com");
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
```

---

### 4. Configuration Settings

```csharp
// appsettings.json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "FromEmail": "noreply@example.com"
  }
}

// Configuration class
public class EmailSettings
{
    public string SmtpServer { get; set; } = string.Empty;
    public int Port { get; set; }
    public string FromEmail { get; set; } = string.Empty;
}

// Registration
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

// Usage with IOptions
public class EmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailService> _logger;
    
    public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger)
    {
        _settings = options.Value;
        _logger = logger;
    }
    
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        _logger.LogInformation("Sending email to {To} via {SmtpServer}", 
            to, _settings.SmtpServer);
        
        // Send email logic using _settings
    }
}
```

---

## Common Anti-Patterns

### 1. Service Locator Pattern (❌ Avoid)

```csharp
// BAD: Using IServiceProvider directly in business logic
public class OrderService
{
    private readonly IServiceProvider _serviceProvider; // ❌ Anti-pattern
    
    public OrderService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task ProcessOrderAsync(Order order)
    {
        // Resolving dependencies inside methods
        var repository = _serviceProvider.GetRequiredService<IOrderRepository>();
        var logger = _serviceProvider.GetRequiredService<ILogger<OrderService>>();
        
        // Process order...
    }
}
```

**Problems**:
- Hidden dependencies (not visible in constructor)
- Harder to test
- Runtime errors if service not registered
- Defeats the purpose of DI

**Solution**: Use constructor injection

```csharp
// GOOD: Explicit constructor injection
public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<OrderService> _logger;
    
    public OrderService(IOrderRepository repository, ILogger<OrderService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
}
```

---

### 2. Captive Dependency (❌ Avoid)

```csharp
// BAD: Singleton capturing Scoped dependency
builder.Services.AddSingleton<INotificationManager, NotificationManager>();
builder.Services.AddScoped<IEmailService, EmailService>();

public class NotificationManager : INotificationManager
{
    private readonly IEmailService _emailService; // Scoped service captured!
    
    public NotificationManager(IEmailService emailService) // ❌ Wrong!
    {
        _emailService = emailService; // Lives for entire app lifetime
    }
}
```

**Problem**: The scoped `IEmailService` becomes captive in the singleton, living far longer than intended.

**Solution**: Use factory pattern or `IServiceScopeFactory`

```csharp
// GOOD: Use IServiceScopeFactory to create scopes on-demand
public class NotificationManager : INotificationManager
{
    private readonly IServiceScopeFactory _scopeFactory;
    
    public NotificationManager(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task SendNotificationAsync(string email, string message)
    {
        using var scope = _scopeFactory.CreateScope();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        await emailService.SendAsync(email, message);
    }
}
```

---

### 3. Injecting Too Many Services (Constructor Over-Injection)

```csharp
// BAD: Too many dependencies (code smell)
public class OrderService
{
    public OrderService(
        IOrderRepository orderRepo,
        IProductRepository productRepo,
        ICustomerRepository customerRepo,
        IPaymentService paymentService,
        IShippingService shippingService,
        IEmailService emailService,
        ILogger<OrderService> logger,
        IMapper mapper,
        IValidator<Order> validator,
        ICacheService cacheService) // 10 dependencies! ❌
    {
        // Too many responsibilities
    }
}
```

**Problem**: Indicates the class is doing too much (violates Single Responsibility Principle)

**Solution**: Refactor into smaller, focused services

```csharp
// GOOD: Split responsibilities
public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly IOrderValidator _validator;
    private readonly IOrderProcessor _processor;
    private readonly ILogger<OrderService> _logger;
    
    public OrderService(
        IOrderRepository repository,
        IOrderValidator validator,
        IOrderProcessor processor,
        ILogger<OrderService> logger)
    {
        _repository = repository;
        _validator = validator;
        _processor = processor;
        _logger = logger;
    }
}

// OrderProcessor handles payment, shipping, email
public class OrderProcessor : IOrderProcessor
{
    private readonly IPaymentService _paymentService;
    private readonly IShippingService _shippingService;
    private readonly IEmailService _emailService;
    
    // Focused on processing logic
}
```

---

## DI vs Manual Object Creation

| Aspect | Dependency Injection | Manual Object Creation |
|--------|---------------------|------------------------|
| **Object Creation** | Managed by DI container | Created with `new` keyword |
| **Lifetime Management** | Automatic (Transient/Scoped/Singleton) | Manual (developer responsibility) |
| **Disposal** | Automatic for `IDisposable` | Manual using `using` or `try-finally` |
| **Testability** | Easy (inject mocks/fakes) | Difficult (hard-coded dependencies) |
| **Coupling** | Loose (depend on interfaces) | Tight (depend on concrete classes) |
| **Flexibility** | High (swap implementations easily) | Low (requires code changes) |
| **Complexity** | Initial setup required | Simple for small apps |
| **Use Case** | Production applications | Simple scripts, prototypes |

### Example Comparison

```csharp
// Manual Object Creation (❌ Not recommended for production)
public class OrderController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateOrder(Order order)
    {
        // Manually creating dependencies
        var connectionString = "Server=..."; // Hard-coded!
        var dbContext = new ApplicationDbContext(connectionString);
        var repository = new OrderRepository(dbContext);
        var service = new OrderService(repository);
        
        service.CreateOrder(order);
        
        dbContext.Dispose(); // Manual disposal
        
        return Ok();
    }
}

// Dependency Injection (✅ Recommended)
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService; // Injected by DI container
    }
    
    [HttpPost]
    public IActionResult CreateOrder(Order order)
    {
        _orderService.CreateOrder(order);
        return Ok();
        // No manual disposal needed - handled by DI container
    }
}
```

---

## Common Interview Questions

### Q1: What is Dependency Injection?

**Answer**: Dependency Injection is a design pattern where a class receives its dependencies from external sources rather than creating them itself. Instead of using `new` to instantiate dependencies, they are "injected" through the constructor (or method/property). This promotes loose coupling, testability, and maintainability. ASP.NET Core has a built-in DI container that automatically resolves and injects dependencies.

---

### Q2: What's the difference between Transient, Scoped, and Singleton lifetimes?

**Answer**:
- **Transient**: New instance created every time the service is requested. Use for lightweight, stateless services.
- **Scoped**: One instance per HTTP request (or scope). Use for services like DbContext that should be shared within a request but not across requests.
- **Singleton**: Single instance for the entire application lifetime. Use for stateless, thread-safe services like configuration or caching.

**Example**: `DbContext` is Scoped (one per request), `ILogger` is Singleton (shared across app), `IEmailSender` might be Transient (new each time).

---

### Q3: Why is constructor injection preferred over property injection?

**Answer**: Constructor injection is preferred because:
1. **Explicit Dependencies**: Makes dependencies visible and required
2. **Immutability**: Dependencies can be `readonly`, preventing changes after construction
3. **Fail Fast**: Application won't start if dependencies are missing
4. **Testability**: Easy to pass mocks in unit tests
5. **No Null Checks**: Dependencies are guaranteed to be non-null

Property injection can lead to `NullReferenceException` and hides dependencies from the caller.

---

### Q4: Can middleware use Dependency Injection? How?

**Answer**: Yes, but with limitations:
- **Singleton services**: Can be injected in the middleware constructor (e.g., `ILogger`)
- **Scoped/Transient services**: Must be injected in the `InvokeAsync` method, not the constructor

This is because middleware instances are created once (singleton lifetime), so injecting scoped services in the constructor would make them captive.

```csharp
public class MyMiddleware
{
    private readonly ILogger _logger; // Singleton OK in constructor
    
    public async Task InvokeAsync(HttpContext context, IMyService service)
    {
        // service is scoped, injected per request ✅
    }
}
```

---

### Q5: What is a "captive dependency" and why is it a problem?

**Answer**: A captive dependency occurs when a service with a longer lifetime (e.g., Singleton) holds a reference to a service with a shorter lifetime (e.g., Scoped). This causes the shorter-lived service to be "captured" and live longer than intended.

**Example**: Singleton injecting a Scoped DbContext would keep the DbContext alive for the entire application lifetime, causing memory leaks, stale data, and concurrency issues.

**Solution**: Use `IServiceScopeFactory` to create scopes on-demand or refactor to match lifetimes.

---

### Q6: How does ASP.NET Core resolve dependencies?

**Answer**: When a class (like a controller) is needed, the DI container:
1. Analyzes the constructor to find required dependencies
2. Recursively resolves each dependency (and their dependencies)
3. Creates instances based on registered lifetimes
4. Injects them into the constructor
5. Returns the fully constructed object
6. Disposes of scoped/transient services when the scope ends

All dependencies must be registered in `Program.cs` using `builder.Services.Add*` methods.

---

### Q7: What happens if you inject a service that's not registered?

**Answer**: At runtime, when the DI container tries to resolve the unregistered service, it throws an `InvalidOperationException` with a message like "Unable to resolve service for type 'IMyService'".

**To prevent this**: Ensure all dependencies are registered in `Program.cs` before calling `builder.Build()`. You can also use `GetService()` instead of `GetRequiredService()` to get `null` if the service isn't registered.

---

### Q8: Can you register the same interface with multiple implementations?

**Answer**: Yes! When you register multiple implementations of the same interface, you can:
1. **Inject `IEnumerable<T>`**: Gets all registered implementations
2. **Inject single instance**: Gets the last registered implementation

```csharp
builder.Services.AddScoped<INotifier, EmailNotifier>();
builder.Services.AddScoped<INotifier, SmsNotifier>();

// Inject all
public MyClass(IEnumerable<INotifier> notifiers) // Gets both

// Inject last registered
public MyClass(INotifier notifier) // Gets SmsNotifier only
```

---

### Q9: What is the Service Locator anti-pattern?

**Answer**: Service Locator is an anti-pattern where you inject `IServiceProvider` and manually resolve dependencies inside methods using `GetService()` or `GetRequiredService()`. This hides dependencies, makes testing harder, and can cause runtime errors.

**Bad Example**:
```csharp
public class MyService
{
    private readonly IServiceProvider _provider;
    
    public void DoWork()
    {
        var repo = _provider.GetService<IRepository>(); // ❌ Hidden dependency
    }
}
```

**Good Example**: Use constructor injection to make dependencies explicit.

---

### Q10: How do you unit test classes that use DI?

**Answer**: Unit testing with DI is straightforward because you can pass mock or fake implementations:

```csharp
public class OrderServiceTests
{
    [Fact]
    public async Task CreateOrder_ShouldCallRepository()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var mockLogger = new Mock<ILogger<OrderService>>();
        var service = new OrderService(mockRepo.Object, mockLogger.Object);
        
        var order = new Order { Id = 1, Total = 100 };
        
        // Act
        await service.CreateOrderAsync(order);
        
        // Assert
        mockRepo.Verify(r => r.AddAsync(order), Times.Once);
    }
}
```

No need for complex setup - just pass mock objects to the constructor!

---

## Summary

✅ **Key Takeaways:**

- **Dependency Injection** is a core design pattern in ASP.NET Core that promotes loose coupling, testability, and maintainability by injecting dependencies rather than creating them with `new`

- **Three service lifetimes**: **Transient** (new every time), **Scoped** (once per request), **Singleton** (once per app). Choose based on state, thread-safety, and resource usage

- **Constructor injection is preferred** because it makes dependencies explicit, enables immutability, fails fast, and simplifies testing compared to property or method injection

- **Avoid anti-patterns**: Service Locator (injecting `IServiceProvider`), captive dependencies (longer-lived service holding shorter-lived service), and constructor over-injection (too many dependencies)

- **Middleware DI rules**: Singleton services in constructor, Scoped/Transient services in `InvokeAsync` method to avoid captive dependencies

- **Real-world usage**: DI is essential for repositories, logging, API clients, configuration, and any service that needs to be mocked in tests or swapped in different environments

- **In interviews**, be ready to explain lifetimes, captive dependencies, why constructor injection is best, and how to use DI in middleware and controllers with real code examples

---

**Master Dependency Injection, and you'll write cleaner, more testable, and maintainable ASP.NET Core applications!** 🚀

