# Logging in ASP.NET Core – Best Practices, Providers & Real Examples

## Introduction

Logging is one of the most critical aspects of production applications, enabling developers to trace issues, monitor performance, and understand application behavior in real-time. Good logging practices can mean the difference between quickly resolving a production incident and hours of blind debugging. ASP.NET Core provides a powerful, flexible, built-in logging framework that supports structured logging, multiple providers, and environment-specific configuration. This guide covers logging fundamentals, best practices, and real-world scenarios essential for interviews and production systems.

---

## What is Logging in ASP.NET Core?

**Logging** is the process of recording application events, errors, and diagnostic information during runtime. ASP.NET Core provides a **built-in logging abstraction** (`ILogger<T>`) that decouples your application code from specific logging implementations. This abstraction allows you to:

- Write logs once using `ILogger<T>`
- Route logs to multiple destinations (console, files, cloud services)
- Change logging providers without modifying application code
- Configure log levels per environment (verbose in dev, minimal in production)

### Key Benefit

The logging abstraction means you can switch from Console logging in development to Application Insights in Azure production without changing a single line of logging code.

---

## Built-in Logging Architecture

ASP.NET Core's logging system consists of three main components:

### 1. ILogger Interface

The core abstraction for writing logs. All log methods (LogInformation, LogWarning, LogError, etc.) are defined here.

```csharp
public interface ILogger
{
    void Log<TState>(LogLevel logLevel, EventId eventId, TState state, 
        Exception? exception, Func<TState, Exception?, string> formatter);
    
    bool IsEnabled(LogLevel logLevel);
    
    IDisposable BeginScope<TState>(TState state);
}
```

**In Practice**: You rarely use this directly; instead, use extension methods like `LogInformation()`, `LogError()`, etc.

---

### 2. ILogger&lt;T&gt; (Generic Logger)

The **recommended** way to use logging. The generic type parameter `T` is typically the class where logging occurs, providing automatic category names.

```csharp
public class ProductService
{
    private readonly ILogger<ProductService> _logger;
    
    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger; // Category: "ProductService"
    }
    
    public async Task<Product> GetProductAsync(int id)
    {
        _logger.LogInformation("Fetching product {ProductId}", id);
        // ...
    }
}
```

**Category Name**: The type `T` becomes the log category, helping filter and organize logs. In this example, all logs have category `"ProductService"`.

---

### 3. Logging Providers

Providers determine **where** logs are written. ASP.NET Core includes several built-in providers:

| Provider | Output Destination | Use Case |
|----------|-------------------|----------|
| **Console** | Console window | Development, Docker containers |
| **Debug** | Debug output window | Visual Studio debugging |
| **EventSource** | Event Tracing for Windows (ETW) | Windows performance monitoring |
| **EventLog** | Windows Event Log | Windows services, production |

### Third-Party Providers (Popular)

- **Serilog** - Structured logging with sinks for files, databases, Elasticsearch
- **NLog** - Highly configurable, supports targets like files, databases, email
- **Application Insights** - Azure cloud monitoring and diagnostics
- **Seq** - Centralized structured log server
- **Elasticsearch/ELK Stack** - Distributed search and analytics

### Registering Providers

```csharp
var builder = WebApplication.CreateBuilder(args);

// Built-in providers are added automatically
// You can add custom providers:
builder.Logging.ClearProviders(); // Remove defaults
builder.Logging.AddConsole();     // Add Console
builder.Logging.AddDebug();       // Add Debug
builder.Logging.AddEventLog();    // Add Event Log

var app = builder.Build();
```

---

## Log Levels (Very Important)

Log levels indicate the **severity** and **importance** of log messages. Understanding when to use each level is critical for effective logging.

### 1. Trace (LogLevel.Trace) - Most Verbose

**Value**: 0  
**Use When**: Tracking detailed execution flow for deep debugging  
**Production**: ❌ Disabled (too verbose, performance impact)

```csharp
_logger.LogTrace("Entering method ProcessOrder with OrderId: {OrderId}", orderId);
_logger.LogTrace("Validating order items...");
_logger.LogTrace("Order validation completed");
```

**Real-World Example**: Debugging a complex algorithm step-by-step in development.

---

### 2. Debug (LogLevel.Debug)

**Value**: 1  
**Use When**: Diagnostic information useful during development  
**Production**: ❌ Usually disabled (verbose, sensitive data risk)

```csharp
_logger.LogDebug("Database query executed: {Query}", sqlQuery);
_logger.LogDebug("Cache hit for key: {CacheKey}", key);
_logger.LogDebug("HTTP request to {Url} completed in {ElapsedMs}ms", url, elapsed);
```

**Real-World Example**: Understanding database query performance or cache behavior during development.

---

### 3. Information (LogLevel.Information) - Default

**Value**: 2  
**Use When**: General informational messages about application flow  
**Production**: ✅ Enabled (key business events, state changes)

```csharp
_logger.LogInformation("User {UserId} logged in successfully", userId);
_logger.LogInformation("Order {OrderId} created with total {Total:C}", orderId, total);
_logger.LogInformation("Payment processed for Order {OrderId}", orderId);
_logger.LogInformation("Email sent to {Email}", email);
```

**Real-World Example**: Tracking significant business events like user actions, order processing, payments.

---

### 4. Warning (LogLevel.Warning)

**Value**: 3  
**Use When**: Unexpected situations that don't prevent operation but need attention  
**Production**: ✅ Enabled (potential issues, degraded performance)

```csharp
_logger.LogWarning("API rate limit approaching: {Current}/{Limit} requests", current, limit);
_logger.LogWarning("Product {ProductId} inventory low: {Quantity} remaining", id, qty);
_logger.LogWarning("External API call failed, retrying... Attempt {Attempt}", attempt);
_logger.LogWarning("Cache miss for key {CacheKey}, fetching from database", key);
```

**Real-World Example**: Retry logic triggering, low inventory alerts, approaching rate limits.

---

### 5. Error (LogLevel.Error)

**Value**: 4  
**Use When**: Operation failures that prevent completing a request but don't crash the app  
**Production**: ✅ Always enabled (requires immediate investigation)

```csharp
_logger.LogError(ex, "Failed to process payment for Order {OrderId}", orderId);
_logger.LogError(ex, "Database connection failed: {ConnectionString}", connString);
_logger.LogError("Validation failed for User {UserId}: {Errors}", userId, errors);
_logger.LogError(ex, "External API call failed after {RetryCount} retries", retryCount);
```

**Real-World Example**: Payment gateway failures, database connection errors, third-party API failures.

**⚠️ Important**: Always include the exception as the first parameter!

---

### 6. Critical (LogLevel.Critical) - Most Severe

**Value**: 5  
**Use When**: Catastrophic failures requiring immediate action (application might crash)  
**Production**: ✅ Always enabled (triggers alerts, pages on-call engineers)

```csharp
_logger.LogCritical(ex, "Application unable to start: missing configuration");
_logger.LogCritical(ex, "Database server unreachable, application shutting down");
_logger.LogCritical("Out of memory exception, application unstable");
_logger.LogCritical(ex, "Failed to connect to critical dependency: {Service}", serviceName);
```

**Real-World Example**: Database server down, critical dependency unreachable, application startup failure.

**🚨 Difference from Error**: Critical means the application or major feature is completely broken and needs immediate attention (wake up the on-call engineer!).

---

### Log Level Comparison Table

| Level | Value | Verbosity | Production | Use Case | Example |
|-------|-------|-----------|------------|----------|---------|
| **Trace** | 0 | Highest | ❌ No | Step-by-step execution | Method entry/exit |
| **Debug** | 1 | High | ❌ Rarely | Development diagnostics | SQL queries, cache hits |
| **Information** | 2 | Normal | ✅ Yes | Business events | User login, order created |
| **Warning** | 3 | Important | ✅ Yes | Potential issues | Retry attempts, low inventory |
| **Error** | 4 | Critical | ✅ Yes | Operation failures | Payment failed, DB error |
| **Critical** | 5 | Most Critical | ✅ Yes | System failures | App crash, dependency down |

---

## Basic Logging Example

### Controller Example

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;
    
    public ProductsController(
        IProductService productService,
        ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        _logger.LogInformation("Fetching product {ProductId}", id);
        
        try
        {
            var product = await _productService.GetByIdAsync(id);
            
            if (product == null)
            {
                _logger.LogWarning("Product {ProductId} not found", id);
                return NotFound();
            }
            
            _logger.LogInformation("Product {ProductId} retrieved successfully", id);
            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product {ProductId}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        _logger.LogInformation("Creating product: {ProductName}", product.Name);
        
        try
        {
            var created = await _productService.CreateAsync(product);
            
            _logger.LogInformation(
                "Product {ProductId} created successfully with name {ProductName}", 
                created.Id, 
                created.Name);
            
            return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create product {ProductName}", product.Name);
            return StatusCode(500, "Failed to create product");
        }
    }
}
```

---

### Service Example

```csharp
public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IPaymentService _paymentService;
    private readonly ILogger<OrderService> _logger;
    
    public OrderService(
        IOrderRepository repository,
        IPaymentService paymentService,
        ILogger<OrderService> logger)
    {
        _repository = repository;
        _paymentService = paymentService;
        _logger = logger;
    }
    
    public async Task<Order> ProcessOrderAsync(OrderRequest request)
    {
        _logger.LogInformation(
            "Processing order for User {UserId} with {ItemCount} items, Total: {Total:C}", 
            request.UserId, 
            request.Items.Count, 
            request.TotalAmount);
        
        try
        {
            // Create order
            var order = await _repository.CreateOrderAsync(request);
            _logger.LogInformation("Order {OrderId} created", order.Id);
            
            // Process payment
            _logger.LogInformation("Processing payment for Order {OrderId}", order.Id);
            var paymentResult = await _paymentService.ProcessPaymentAsync(order);
            
            if (!paymentResult.Success)
            {
                _logger.LogWarning(
                    "Payment failed for Order {OrderId}: {Reason}", 
                    order.Id, 
                    paymentResult.FailureReason);
                
                order.Status = OrderStatus.PaymentFailed;
                await _repository.UpdateOrderAsync(order);
                return order;
            }
            
            order.Status = OrderStatus.Completed;
            await _repository.UpdateOrderAsync(order);
            
            _logger.LogInformation(
                "Order {OrderId} completed successfully, Payment: {PaymentId}", 
                order.Id, 
                paymentResult.PaymentId);
            
            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process order for User {UserId}", request.UserId);
            throw;
        }
    }
}
```

---

### Structured Logging with Placeholders

**❌ Bad (String Interpolation)**:
```csharp
_logger.LogInformation($"User {userId} logged in at {DateTime.Now}");
```

**✅ Good (Message Templates)**:
```csharp
_logger.LogInformation("User {UserId} logged in at {LoginTime}", userId, DateTime.Now);
```

**Why?** Structured logging treats placeholders as **properties**, making logs searchable, filterable, and analyzable. Interpolation creates plain text strings that can't be queried efficiently.

---

## Logging Configuration

### appsettings.json Configuration

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information",
      "System": "Warning",
      "MyApp.Services": "Debug",
      "MyApp.Controllers.ProductsController": "Trace"
    }
  }
}
```

**Explanation**:
- `"Default": "Information"` - All logs at Information level or higher
- `"Microsoft": "Warning"` - Only warnings/errors from Microsoft namespaces
- `"Microsoft.EntityFrameworkCore": "Information"` - Show EF Core queries
- `"MyApp.Services": "Debug"` - Debug logs for your services namespace
- `"MyApp.Controllers.ProductsController": "Trace"` - All logs for specific controller

---

### Environment-Based Logging

```json
// appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}

// appsettings.Production.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft": "Error",
      "MyApp": "Information"
    }
  }
}
```

**Strategy**:
- **Development**: Verbose logging (Debug/Trace) for detailed diagnostics
- **Production**: Minimal logging (Warning/Error/Critical) to reduce noise and improve performance

---

### Programmatic Configuration

```csharp
var builder = WebApplication.CreateBuilder(args);

// Configure logging levels programmatically
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
builder.Logging.AddFilter("System", LogLevel.Error);
builder.Logging.AddFilter("MyApp.Services", LogLevel.Debug);

var app = builder.Build();
```

---

## Logging in Middleware

### Custom Request/Response Logging Middleware

```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    
    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        
        // Log request
        _logger.LogInformation(
            "HTTP {Method} {Path} started", 
            context.Request.Method, 
            context.Request.Path);
        
        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            
            // Log response
            var logLevel = context.Response.StatusCode >= 500 
                ? LogLevel.Error 
                : context.Response.StatusCode >= 400 
                    ? LogLevel.Warning 
                    : LogLevel.Information;
            
            _logger.Log(
                logLevel,
                "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}

// Extension method for registration
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}

// Registration in Program.cs
var app = builder.Build();
app.UseRequestLogging(); // Add before routing
app.MapControllers();
```

**Output Example**:
```
info: RequestLoggingMiddleware[0]
      HTTP GET /api/products/123 started
info: RequestLoggingMiddleware[0]
      HTTP GET /api/products/123 responded 200 in 45ms
```

---

### Enhanced Logging with Correlation ID

```csharp
public class CorrelatedRequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelatedRequestLoggingMiddleware> _logger;
    
    public CorrelatedRequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<CorrelatedRequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault() 
            ?? Guid.NewGuid().ToString();
        
        context.Response.Headers.Add("X-Correlation-ID", correlationId);
        
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId,
            ["UserAgent"] = context.Request.Headers["User-Agent"].ToString(),
            ["RemoteIP"] = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown"
        }))
        {
            var stopwatch = Stopwatch.StartNew();
            
            _logger.LogInformation(
                "Request started: {Method} {Path}",
                context.Request.Method,
                context.Request.Path);
            
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception during request processing");
                throw;
            }
            finally
            {
                stopwatch.Stop();
                
                _logger.LogInformation(
                    "Request completed: {Method} {Path} {StatusCode} in {ElapsedMs}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
```

**Benefits**:
- **Correlation ID**: Track requests across distributed systems
- **Scope**: Automatically adds correlation ID to all logs in the request
- **Response Header**: Clients can reference correlation ID when reporting issues

---

## Structured Logging (Very Important)

Structured logging treats log messages as **data with properties** rather than plain text strings. This enables powerful querying, filtering, and analysis.

### Plain Text Logging (❌ Avoid)

```csharp
// Bad: String interpolation or concatenation
_logger.LogInformation($"User {userId} placed order #{orderId} for ${totalAmount}");
_logger.LogInformation("User " + userId + " placed order #" + orderId + " for $" + totalAmount);
```

**Problems**:
- Can't filter by userId, orderId, or totalAmount
- Can't aggregate orders by user
- Can't analyze spending patterns
- Plain text search only

---

### Structured Logging (✅ Recommended)

```csharp
// Good: Message templates with placeholders
_logger.LogInformation(
    "User {UserId} placed order {OrderId} for {TotalAmount:C}",
    userId,
    orderId,
    totalAmount);
```

**JSON Output** (with structured provider like Serilog):
```json
{
  "Timestamp": "2026-01-27T10:30:45.123Z",
  "Level": "Information",
  "MessageTemplate": "User {UserId} placed order {OrderId} for {TotalAmount:C}",
  "Properties": {
    "UserId": 12345,
    "OrderId": 67890,
    "TotalAmount": 149.99
  }
}
```

**Now you can query**:
- All orders by specific user: `UserId = 12345`
- High-value orders: `TotalAmount > 100`
- Orders in time range: `Timestamp between X and Y`

---

### Message Template Guidelines

```csharp
// ✅ Good: Descriptive property names
_logger.LogInformation("Order {OrderId} shipped to {City}, {State}", orderId, city, state);

// ❌ Bad: Generic property names
_logger.LogInformation("Order {Id} shipped to {Param1}, {Param2}", orderId, city, state);

// ✅ Good: Format specifiers
_logger.LogInformation("Total revenue: {Revenue:C}", revenue);           // Currency: $1,234.56
_logger.LogInformation("Processing time: {ElapsedMs:N2}ms", elapsed);   // Number: 123.45ms
_logger.LogInformation("Order date: {OrderDate:yyyy-MM-dd}", date);     // Date: 2026-01-27

// ✅ Good: Structured data
_logger.LogInformation(
    "Payment processed: {PaymentId}, Status: {Status}, Amount: {Amount:C}",
    paymentId,
    status,
    amount);
```

---

### Using Log Scopes

Scopes add context to all logs within a block:

```csharp
public async Task<Order> ProcessOrderAsync(int orderId, int userId)
{
    using (_logger.BeginScope(new Dictionary<string, object>
    {
        ["OrderId"] = orderId,
        ["UserId"] = userId
    }))
    {
        _logger.LogInformation("Starting order processing");
        
        // All logs inside this scope automatically include OrderId and UserId
        
        _logger.LogInformation("Validating order items");
        await ValidateItemsAsync();
        
        _logger.LogInformation("Processing payment");
        await ProcessPaymentAsync();
        
        _logger.LogInformation("Order processing completed");
    }
}
```

**Output** (all logs include OrderId and UserId):
```
[OrderId: 123, UserId: 456] Starting order processing
[OrderId: 123, UserId: 456] Validating order items
[OrderId: 123, UserId: 456] Processing payment
[OrderId: 123, UserId: 456] Order processing completed
```

---

## Exception Logging

### Best Practices

#### ✅ Always Log Exceptions with Context

```csharp
try
{
    var product = await _repository.GetByIdAsync(productId);
}
catch (Exception ex)
{
    // Good: Log exception with context
    _logger.LogError(ex, "Failed to retrieve product {ProductId}", productId);
    throw;
}
```

---

#### ❌ Avoid Logging Exceptions Multiple Times

```csharp
// BAD: Logging at every layer
public async Task<Product> GetProductAsync(int id)
{
    try
    {
        return await _repository.GetByIdAsync(id);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Repository error"); // ❌ Logged here
        throw; // Exception bubbles up...
    }
}

public async Task<ActionResult> GetProduct(int id)
{
    try
    {
        var product = await _service.GetProductAsync(id);
        return Ok(product);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Controller error"); // ❌ Logged again!
        return StatusCode(500);
    }
}
```

**Problem**: Same exception logged twice, polluting logs

**Solution**: Log once at the boundary (controller or global exception handler)

---

#### ✅ Use Global Exception Handler

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Global exception handler
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionFeature?.Error;
        
        var logger = context.RequestServices
            .GetRequiredService<ILogger<Program>>();
        
        logger.LogError(exception,
            "Unhandled exception: {Method} {Path}",
            context.Request.Method,
            context.Request.Path);
        
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        
        await context.Response.WriteAsJsonAsync(new
        {
            error = "An error occurred while processing your request",
            traceId = Activity.Current?.Id ?? context.TraceIdentifier
        });
    });
});

app.MapControllers();
app.Run();
```

---

#### ✅ Include Relevant Data

```csharp
try
{
    await _paymentService.ProcessPaymentAsync(order);
}
catch (PaymentException ex)
{
    _logger.LogError(ex,
        "Payment failed: OrderId={OrderId}, UserId={UserId}, Amount={Amount:C}, Gateway={Gateway}",
        order.Id,
        order.UserId,
        order.TotalAmount,
        ex.PaymentGateway);
    throw;
}
```

---

## What NOT to Log

### 1. Sensitive Data (Security Risk)

```csharp
// ❌ BAD: Logging passwords, tokens, API keys
_logger.LogInformation("User logged in with password: {Password}", password);
_logger.LogDebug("API Key: {ApiKey}", apiKey);
_logger.LogInformation("Credit card: {CardNumber}", cardNumber);
_logger.LogDebug("JWT Token: {Token}", jwtToken);

// ✅ GOOD: Mask or omit sensitive data
_logger.LogInformation("User {UserId} logged in successfully", userId);
_logger.LogDebug("API Key: {MaskedKey}", apiKey.Substring(0, 4) + "****");
_logger.LogInformation("Payment processed with card ending {Last4}", cardLast4Digits);
```

---

### 2. Personally Identifiable Information (PII)

```csharp
// ❌ BAD: Logging PII (GDPR/privacy violation)
_logger.LogInformation("User email: {Email}, Phone: {Phone}, SSN: {SSN}", 
    email, phone, ssn);

// ✅ GOOD: Log user ID only
_logger.LogInformation("User {UserId} profile updated", userId);
```

---

### 3. Large Payloads

```csharp
// ❌ BAD: Logging entire request/response bodies
_logger.LogDebug("Request body: {Body}", JsonSerializer.Serialize(largeObject));

// ✅ GOOD: Log summary or size
_logger.LogDebug("Processing request with {ItemCount} items, Size: {SizeKB}KB",
    items.Count, sizeInKB);
```

---

### 4. High-Frequency Logs

```csharp
// ❌ BAD: Logging in tight loops
foreach (var item in millionsOfItems)
{
    _logger.LogDebug("Processing item {ItemId}", item.Id); // Performance killer!
}

// ✅ GOOD: Log batch summary
_logger.LogInformation("Processing {ItemCount} items", millionsOfItems.Count);
// Process items...
_logger.LogInformation("Processed {ItemCount} items in {ElapsedMs}ms", 
    millionsOfItems.Count, elapsed);
```

---

## Real-World Production Scenarios

### 1. API Request Tracing

```csharp
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;
    
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var correlationId = Guid.NewGuid().ToString();
        
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId,
            ["UserId"] = request.UserId,
            ["ItemCount"] = request.Items.Count
        }))
        {
            _logger.LogInformation("Order creation started");
            
            try
            {
                var order = await _orderService.CreateOrderAsync(request);
                
                _logger.LogInformation(
                    "Order {OrderId} created successfully, Total: {Total:C}",
                    order.Id,
                    order.TotalAmount);
                
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Order creation failed");
                return StatusCode(500, new { error = "Failed to create order", correlationId });
            }
        }
    }
}
```

---

### 2. Debugging Failures in Distributed Systems

```csharp
public class DistributedOrderService
{
    private readonly ILogger<DistributedOrderService> _logger;
    private readonly IInventoryService _inventoryService;
    private readonly IPaymentService _paymentService;
    private readonly IShippingService _shippingService;
    
    public async Task<Order> ProcessOrderAsync(Order order)
    {
        var correlationId = Guid.NewGuid().ToString();
        
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId,
            ["OrderId"] = order.Id
        }))
        {
            _logger.LogInformation("Starting distributed order processing");
            
            // Step 1: Reserve inventory
            _logger.LogInformation("Calling inventory service");
            var inventoryReserved = await _inventoryService.ReserveAsync(order.Items, correlationId);
            
            if (!inventoryReserved)
            {
                _logger.LogWarning("Inventory reservation failed");
                return order;
            }
            
            _logger.LogInformation("Inventory reserved successfully");
            
            // Step 2: Process payment
            _logger.LogInformation("Calling payment service");
            var paymentResult = await _paymentService.ProcessAsync(order, correlationId);
            
            if (!paymentResult.Success)
            {
                _logger.LogError("Payment failed: {Reason}, rolling back inventory", 
                    paymentResult.FailureReason);
                
                await _inventoryService.ReleaseAsync(order.Items, correlationId);
                return order;
            }
            
            _logger.LogInformation("Payment processed: {PaymentId}", paymentResult.PaymentId);
            
            // Step 3: Initiate shipping
            _logger.LogInformation("Calling shipping service");
            var shippingResult = await _shippingService.CreateShipmentAsync(order, correlationId);
            
            _logger.LogInformation(
                "Order processing completed: Tracking={TrackingNumber}",
                shippingResult.TrackingNumber);
            
            return order;
        }
    }
}
```

**Benefits**:
- **Correlation ID** traces the order across all services
- Each service call logged with timing and result
- Failures include context for debugging
- Can reconstruct entire order flow from logs

---

### 3. Correlation IDs Across Microservices

```csharp
// API Gateway or first service
public class ApiGatewayMiddleware
{
    private readonly RequestDelegate _next;
    
    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
            ?? Guid.NewGuid().ToString();
        
        context.Items["CorrelationId"] = correlationId;
        context.Response.Headers.Add("X-Correlation-ID", correlationId);
        
        await _next(context);
    }
}

// HTTP Client Factory configuration
builder.Services.AddHttpClient("InventoryService", client =>
{
    client.BaseAddress = new Uri("https://inventory-service.example.com");
})
.AddHttpMessageHandler<CorrelationIdDelegatingHandler>();

// Delegating handler to propagate correlation ID
public class CorrelationIdDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CorrelationIdDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"] as string;
        
        if (!string.IsNullOrEmpty(correlationId))
        {
            request.Headers.Add("X-Correlation-ID", correlationId);
        }
        
        return await base.SendAsync(request, cancellationToken);
    }
}
```

**Result**: Correlation ID flows through all microservices, enabling end-to-end request tracing.

---

## Common Interview Questions

### Q1: What is ILogger&lt;T&gt; and why use the generic version?

**Answer**: `ILogger<T>` is the generic logging interface in ASP.NET Core where `T` is typically the class using the logger. The generic type parameter automatically sets the **log category** to the full name of `T`, making it easy to filter logs by class or namespace. For example, `ILogger<ProductService>` creates logs with category `"MyApp.Services.ProductService"`, allowing you to configure log levels per class or namespace without hardcoding category names.

---

### Q2: What's the difference between LogError and LogCritical?

**Answer**:
- **LogError**: Used for operation failures that prevent completing a request but don't crash the application (e.g., payment gateway failure, database query error, external API timeout). The application continues running and can handle other requests.
  
- **LogCritical**: Used for catastrophic failures that might cause the application to crash or become completely unusable (e.g., database server unreachable, out of memory, critical dependency unavailable). These require immediate attention and typically trigger alerts to wake up on-call engineers.

**Rule of Thumb**: If you need to wake someone up at 3 AM, it's Critical. Otherwise, it's Error.

---

### Q3: What is structured logging and why is it important?

**Answer**: Structured logging treats log messages as **data with properties** rather than plain text strings. Instead of string interpolation (`$"User {userId} logged in"`), you use message templates with placeholders (`"User {UserId} logged in", userId`). This enables:

- **Queryable logs**: Filter by specific property values (e.g., all logs for `UserId = 123`)
- **Aggregation**: Analyze patterns (e.g., average order value, error rates by service)
- **Searchability**: Find logs efficiently in centralized systems like Elasticsearch or Application Insights
- **Performance**: More efficient than string concatenation

Structured logging is essential for observability in production systems and microservices architectures.

---

### Q4: How do you log requests globally in ASP.NET Core?

**Answer**: Use custom middleware to log all HTTP requests and responses:

```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    
    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Request: {Method} {Path}", 
            context.Request.Method, context.Request.Path);
        
        await _next(context);
        
        _logger.LogInformation("Response: {StatusCode}", 
            context.Response.StatusCode);
    }
}

// Register early in pipeline
app.UseMiddleware<RequestLoggingMiddleware>();
```

Alternatively, enable built-in HTTP logging:
```csharp
app.UseHttpLogging();
```

For production, include correlation IDs, execution time, and conditional logging based on status codes.

---

### Q5: What should you never log in production?

**Answer**: Never log:
1. **Passwords, API keys, tokens**: Security risk, enables unauthorized access
2. **Personally Identifiable Information (PII)**: Email, phone, SSN, credit card numbers (GDPR/privacy violations)
3. **Entire request/response bodies**: Contains sensitive data, performance impact, log storage issues
4. **High-frequency logs in loops**: Performance degradation, log storage costs

Always use user IDs instead of personal data, mask sensitive information, and log summaries instead of full payloads.

---

### Q6: How do you configure different log levels for different environments?

**Answer**: Use environment-specific `appsettings.json` files:

```json
// appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information"
    }
  }
}

// appsettings.Production.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft": "Error",
      "MyApp": "Information"
    }
  }
}
```

ASP.NET Core automatically loads the correct file based on `ASPNETCORE_ENVIRONMENT`. Development gets verbose logging (Debug/Trace), while Production gets minimal logging (Warning/Error/Critical) for performance and security.

---

### Q7: What is a correlation ID and why is it useful?

**Answer**: A correlation ID is a unique identifier (typically a GUID) that tracks a single request across multiple services in a distributed system. It's passed via HTTP headers (`X-Correlation-ID`) and included in all logs.

**Benefits**:
- **End-to-end tracing**: Track a request through API Gateway → Service A → Service B → Database
- **Debugging**: Reconstruct the entire flow of a failed request
- **Troubleshooting**: Users can provide correlation ID when reporting issues
- **Observability**: Analyze request paths and identify bottlenecks

Essential for microservices, distributed systems, and production debugging.

---

### Q8: How do you log exceptions without logging them multiple times?

**Answer**: Log exceptions at the **boundary** (controller or global exception handler) rather than at every layer:

```csharp
// ❌ Bad: Logging at every layer
Repository → catch { Log(ex); throw; }
Service    → catch { Log(ex); throw; } // Logged twice!
Controller → catch { Log(ex); }        // Logged three times!

// ✅ Good: Log once at boundary
Repository → throw
Service    → throw
Controller → catch { Log(ex); } // Logged once

// ✅ Better: Global exception handler
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        _logger.LogError(exception, "Unhandled exception");
    });
});
```

This prevents log pollution and makes it clear where errors originate.

---

### Q9: What logging providers would you use in production?

**Answer**: In production, use centralized, structured logging providers:

- **Application Insights** (Azure): Cloud-native monitoring, automatic correlation, powerful querying
- **Serilog + Seq**: Structured logging with centralized log server for on-premises
- **Serilog + Elasticsearch (ELK Stack)**: Distributed search and analytics, ideal for microservices
- **AWS CloudWatch** (AWS): Cloud-native logging for AWS environments
- **Datadog/Splunk**: Enterprise-grade observability and monitoring

Avoid Console and Debug providers in production (performance impact, no centralization).

---

### Q10: How do you test logging in unit tests?

**Answer**: Use a mock logger to verify log calls:

```csharp
public class ProductServiceTests
{
    [Fact]
    public async Task GetProduct_LogsInformation()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ProductService>>();
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(new Product { Id = 1, Name = "Test" });
        
        var service = new ProductService(mockRepo.Object, mockLogger.Object);
        
        // Act
        await service.GetProductAsync(1);
        
        // Assert
        mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("Fetching product")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }
}
```

Alternatively, use `NullLogger<T>` for tests where logging isn't important.

---

## Summary

✅ **Key Takeaways:**

- **ASP.NET Core logging** provides a built-in abstraction (`ILogger<T>`) that decouples application code from logging providers, enabling easy provider switching and environment-specific configuration

- **Six log levels** (Trace, Debug, Information, Warning, Error, Critical) indicate severity: use Information for business events in production, Error for operation failures, and Critical for catastrophic failures requiring immediate action

- **Structured logging** with message templates (`"User {UserId} logged in", userId`) treats logs as queryable data, enabling powerful filtering, aggregation, and analysis in production systems

- **Custom middleware** enables global request/response logging with correlation IDs, execution times, and status codes, essential for observability and debugging in distributed systems

- **Never log sensitive data**: Avoid passwords, tokens, PII, and large payloads; use user IDs, mask sensitive info, and log summaries to maintain security and comply with privacy regulations

- **Environment-specific configuration**: Use verbose logging (Debug/Trace) in development for diagnostics and minimal logging (Warning/Error/Critical) in production for performance and security

- **In interviews**, demonstrate knowledge of log levels, structured logging benefits, correlation IDs for distributed tracing, global exception handling, and production logging strategies with real-world examples

---

**Master logging, and you'll build observable, debuggable, production-ready ASP.NET Core applications!** 🚀
