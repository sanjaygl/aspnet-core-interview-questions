# .NET Coding Interview Questions – Detailed Explanations and Examples

---

## 1. Implement pagination in a .NET Web API endpoint using query parameters

**Explanation:**
Pagination is essential for APIs that return large datasets. It allows clients to request data in chunks (pages) using query parameters like `pageNumber` and `pageSize`.

**Example:**
```csharp
[HttpGet("/api/products")]
public IActionResult GetProducts(int pageNumber = 1, int pageSize = 10)
{
    var paged = _db.Products
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    return Ok(paged);
}
```

---

## 2. Write a method to call multiple external APIs in parallel and return a combined response

**Explanation:**
Use `Task.WhenAll` to call APIs concurrently, improving performance.

**Example:**
```csharp
public async Task<IActionResult> GetCombinedData()
{
    var task1 = _http.GetAsync("https://api1.com/data");
    var task2 = _http.GetAsync("https://api2.com/data");
    await Task.WhenAll(task1, task2);
    var result1 = await task1.Result.Content.ReadAsStringAsync();
    var result2 = await task2.Result.Content.ReadAsStringAsync();
    return Ok(new { result1, result2 });
}
```

---

## 3. Given a list of orders, calculate total order value per customer using LINQ

**Explanation:**
Group orders by customer and sum their values.

**Example:**
```csharp
var totals = orders
    .GroupBy(o => o.CustomerId)
    .Select(g => new { CustomerId = g.Key, Total = g.Sum(o => o.Value) })
    .ToList();
```

---

## 4. Implement a custom middleware to log request execution time

**Explanation:**
Middleware can measure and log how long each request takes.

**Example:**
```csharp
public class TimingMiddleware
{
    private readonly RequestDelegate _next;
    public TimingMiddleware(RequestDelegate next) => _next = next;
    public async Task Invoke(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        await _next(context);
        sw.Stop();
        Console.WriteLine($"Request took {sw.ElapsedMilliseconds} ms");
    }
}
// In Startup.cs: app.UseMiddleware<TimingMiddleware>();
```

---

## 5. Create a global exception handling mechanism in ASP.NET Core

**Explanation:**
Use `UseExceptionHandler` middleware to catch and handle unhandled exceptions globally.

**Example:**
```csharp
// In Program.cs or Startup.cs
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = error.Error.Message }));
        }
    });
});
```

---

## 6. Implement JWT-based authentication in a Web API

**Explanation:**
JWT (JSON Web Token) is a stateless authentication mechanism. Use `Microsoft.AspNetCore.Authentication.JwtBearer`.

**Example:**
```csharp
// In Program.cs
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your-issuer",
            ValidAudience = "your-audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret"))
        };
    });
// [Authorize] on controllers/actions
```

---

## 7. Write an API endpoint to filter records based on optional query parameters

**Explanation:**
Use nullable query parameters and conditionally apply filters.

**Example:**
```csharp
[HttpGet("/api/orders")]
public IActionResult GetOrders(string? status, DateTime? from, DateTime? to)
{
    var query = _db.Orders.AsQueryable();
    if (!string.IsNullOrEmpty(status))
        query = query.Where(o => o.Status == status);
    if (from.HasValue)
        query = query.Where(o => o.Date >= from);
    if (to.HasValue)
        query = query.Where(o => o.Date <= to);
    return Ok(query.ToList());
}
```

---

## 8. Optimize a slow database query in an API that handles large datasets

**Explanation:**
Use indexes, projections, and `AsNoTracking` for read-only queries.

**Example:**
```csharp
var data = _db.Orders
    .AsNoTracking()
    .Where(o => o.Status == "Active")
    .Select(o => new { o.Id, o.CustomerId, o.Total })
    .ToList();
```

---

## 9. Implement caching in a .NET API for frequently accessed data

**Explanation:**
Use `IMemoryCache` or `IDistributedCache` for caching.

**Example:**
```csharp
public class ProductService
{
    private readonly IMemoryCache _cache;
    public ProductService(IMemoryCache cache) => _cache = cache;
    public Product GetProduct(int id)
    {
        return _cache.GetOrCreate($"product_{id}", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return _db.Products.Find(id);
        });
    }
}
```

---

## 10. Create a background service to process messages asynchronously

**Explanation:**
Use `BackgroundService` for long-running background tasks.

**Example:**
```csharp
public class MessageProcessor : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Process messages from queue
            await Task.Delay(1000, stoppingToken);
        }
    }
}
// Register with: services.AddHostedService<MessageProcessor>();
```

---

## 11. Implement soft delete functionality using Entity Framework

**Explanation:**
Soft delete marks records as deleted without physically removing them from the database. Add an `IsDeleted` property and filter queries accordingly.

**Example:**
```csharp
public class Entity {
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
}
// Mark as deleted:
entity.IsDeleted = true;
// Filter in queries:
var active = _db.Entities.Where(e => !e.IsDeleted);
```

---

## 12. Design an API endpoint to handle bulk insert requests efficiently

**Explanation:**
Bulk inserts can be optimized using `AddRange` and `SaveChanges`, or with third-party libraries for very large datasets.

**Example:**
```csharp
[HttpPost("/api/orders/bulk")]
public IActionResult BulkInsert([FromBody] List<Order> orders)
{
    _db.Orders.AddRange(orders);
    _db.SaveChanges();
    return Ok();
}
// For very large sets, consider EFCore.BulkExtensions
```

---

## 13. Write a thread-safe singleton service

**Explanation:**
Use `Lazy<T>` or double-check locking for thread-safe singletons.

**Example:**
```csharp
public class SingletonService
{
    private static readonly Lazy<SingletonService> _instance = new(() => new SingletonService());
    public static SingletonService Instance => _instance.Value;
    private SingletonService() { }
}
```

---

## 14. Implement retry and timeout logic for outbound HTTP calls

**Explanation:**
Use `Polly` for robust retry and timeout policies.

**Example:**
```csharp
var policy = Policy
    .Handle<HttpRequestException>()
    .WaitAndRetry(3, attempt => TimeSpan.FromSeconds(2))
    .Wrap(Policy.TimeoutAsync<HttpResponseMessage>(5));
await policy.ExecuteAsync(() => httpClient.GetAsync(url));
```

---

## 15. Create a versioned API and explain how clients can migrate

**Explanation:**
API versioning allows you to introduce breaking changes without affecting existing clients. Use the `Microsoft.AspNetCore.Mvc.Versioning` package.

**Example:**
```csharp
// Startup.cs
services.AddApiVersioning(options => {
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});
// Controller
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase { }
```
**Client Migration:**
Clients specify the version in the URL or header. They can migrate by updating the version in their requests and adapting to new contract changes.

---

## 16. Implement cursor-based pagination instead of offset-based pagination

**Explanation:**
Cursor-based pagination uses a unique value (like an ID or timestamp) as a pointer to the next page, which is more efficient for large datasets.

**Example:**
```csharp
[HttpGet("/api/items")]
public IActionResult GetItems(int? lastId, int pageSize = 10)
{
    var query = _db.Items.OrderBy(i => i.Id);
    if (lastId.HasValue)
        query = query.Where(i => i.Id > lastId);
    var items = query.Take(pageSize).ToList();
    return Ok(items);
}
```

---

## 17. Design an API to support dynamic sorting and filtering

**Explanation:**
Accept sort and filter parameters in the query string and apply them dynamically using LINQ.

**Example:**
```csharp
[HttpGet("/api/products")]
public IActionResult GetProducts(string? sortBy, string? filter)
{
    var query = _db.Products.AsQueryable();
    if (!string.IsNullOrEmpty(filter))
        query = query.Where(p => p.Name.Contains(filter));
    if (sortBy == "price")
        query = query.OrderBy(p => p.Price);
    else if (sortBy == "name")
        query = query.OrderBy(p => p.Name);
    return Ok(query.ToList());
}
```

---

## 18. Implement rate limiting in an ASP.NET Core API

**Explanation:**
Rate limiting restricts the number of requests a client can make in a given time window. Use middleware or libraries like `AspNetCoreRateLimit`.

**Example:**
```csharp
// Install AspNetCoreRateLimit and configure in Startup.cs
services.AddMemoryCache();
services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
services.AddInMemoryRateLimiting();
app.UseIpRateLimiting();
```

---

## 19. Create a custom action filter and explain when to use it over middleware

**Explanation:**
Action filters run before/after controller actions. Use them for logic that needs access to action context, model state, or result (e.g., validation, logging, modifying results).

**Example:**
```csharp
public class LogActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Before action
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // After action
    }
}
// Register globally or with [ServiceFilter(typeof(LogActionFilter))]
```

---

## 20. Implement request validation using FluentValidation

**Explanation:**
FluentValidation is a popular library for model validation in .NET.

**Example:**
```csharp
public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}
// Register: services.AddValidatorsFromAssemblyContaining<OrderValidator>();
```

---

## 21. Handle partial updates using HTTP PATCH

**Explanation:**
Use `JsonPatchDocument` to apply partial updates to resources.

**Example:**
```csharp
[HttpPatch("/api/products/{id}")]
public IActionResult PatchProduct(int id, [FromBody] JsonPatchDocument<Product> patch)
{
    var product = _db.Products.Find(id);
    if (product == null) return NotFound();
    patch.ApplyTo(product);
    _db.SaveChanges();
    return Ok(product);
}
```

---

## 22. Design a clean API response wrapper for success and error responses

**Explanation:**
A consistent response format improves client experience and error handling.

**Example:**
```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Error { get; set; }
}
// Usage:
return Ok(new ApiResponse<Product> { Success = true, Data = product });
```

---

## 23. Implement idempotent APIs for POST requests

**Explanation:**
Idempotency ensures that repeating the same request produces the same result. Use idempotency keys or check for existing records.

**Example:**
```csharp
[HttpPost("/api/payments")]
public IActionResult CreatePayment([FromBody] Payment payment, [FromHeader(Name = "Idempotency-Key")] string key)
{
    if (_db.Payments.Any(p => p.IdempotencyKey == key))
        return Conflict("Duplicate request");
    payment.IdempotencyKey = key;
    _db.Payments.Add(payment);
    _db.SaveChanges();
    return Ok(payment);
}
```

---

## 24. Secure sensitive configuration using Azure Key Vault or AWS Secrets Manager

**Explanation:**
Store secrets outside of code and configuration files. Use managed identity and SDKs to retrieve secrets at runtime.

**Example (Azure):**
```csharp
// In Program.cs
builder.Configuration.AddAzureKeyVault(new Uri("https://<vault-name>.vault.azure.net/"), new DefaultAzureCredential());
```

---

## 25. Implement distributed caching using Redis

**Explanation:**
Distributed caching allows multiple app instances to share cached data. Use `StackExchange.Redis` and `AddStackExchangeRedisCache`.

**Example:**
```csharp
// In Program.cs
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});
// Usage:
await _cache.SetStringAsync("key", "value");
var value = await _cache.GetStringAsync("key");
```

---

## 26. Design a health check endpoint for dependent services

**Explanation:**
Health checks monitor the status of your app and its dependencies. Use `AspNetCore.HealthChecks`.

**Example:**
```csharp
// In Program.cs
builder.Services.AddHealthChecks().AddSqlServer("<conn-string>");
app.MapHealthChecks("/health");
```

---

## 27. Implement graceful shutdown handling in ASP.NET Core

**Explanation:**
Graceful shutdown allows in-flight requests to complete before the app stops. Use `IHostApplicationLifetime` events.

**Example:**
```csharp
public class ShutdownService : IHostedService
{
    private readonly IHostApplicationLifetime _lifetime;
    public ShutdownService(IHostApplicationLifetime lifetime) => _lifetime = lifetime;
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _lifetime.ApplicationStopping.Register(() =>
        {
            // Cleanup logic
        });
        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
```

---

## 28. Handle concurrency conflicts using optimistic locking

**Explanation:**
Use a concurrency token (e.g., a `RowVersion` byte array) and handle `DbUpdateConcurrencyException`.

**Example:**
```csharp
public class Product {
    public int Id { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
}
// In update logic:
try {
    _db.SaveChanges();
} catch (DbUpdateConcurrencyException) {
    // Handle conflict
}
```

---

## 29. Implement API-level authorization using policy-based authorization

**Explanation:**
Policies allow you to define complex authorization requirements.

**Example:**
```csharp
// In Program.cs
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});
// [Authorize(Policy = "AdminOnly")] on controllers/actions
```

---

## 30. Design an API that supports file upload and streaming download

**Explanation:**
Use `IFormFile` for uploads and `FileStreamResult` for downloads.

**Example:**
```csharp
[HttpPost("/api/upload")]
public async Task<IActionResult> Upload(IFormFile file)
{
    using var stream = new FileStream($"uploads/{file.FileName}", FileMode.Create);
    await file.CopyToAsync(stream);
    return Ok();
}

[HttpGet("/api/download/{filename}")]
public IActionResult Download(string filename)
{
    var path = $"uploads/{filename}";
    var stream = new FileStream(path, FileMode.Open);
    return File(stream, "application/octet-stream", filename);
}
```

---

## 31. Implement server-side logging with correlation IDs

**Explanation:**
Correlation IDs help trace requests across distributed systems. Use middleware to generate and propagate IDs.

**Example:**
```csharp
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    public CorrelationIdMiddleware(RequestDelegate next) => _next = next;
    public async Task Invoke(HttpContext context)
    {
        var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault() ?? Guid.NewGuid().ToString();
        context.Items["CorrelationId"] = correlationId;
        context.Response.Headers["X-Correlation-ID"] = correlationId;
        await _next(context);
    }
}
// app.UseMiddleware<CorrelationIdMiddleware>();
```

---

## 32. Handle long-running background jobs with retries and failure handling

**Explanation:**
Use libraries like Hangfire or Quartz.NET for background jobs with retry/failure logic.

**Example (Hangfire):**
```csharp
// In Program.cs
builder.Services.AddHangfire(x => x.UseSqlServerStorage("<conn-string>"));
// Enqueue job:
BackgroundJob.Enqueue(() => DoWork());
```

---

## 33. Implement an outbox pattern for reliable message publishing

**Explanation:**
The outbox pattern ensures messages are only published if the database transaction succeeds.

**Example:**
```csharp
// 1. Write message to Outbox table in same transaction as business data
// 2. Background service reads Outbox and publishes messages
```

---

## 34. Design a clean repository pattern with unit-of-work

**Explanation:**
Repository abstracts data access; unit-of-work coordinates changes across repositories.

**Example:**
```csharp
public interface IRepository<T> { ... }
public interface IUnitOfWork { void Commit(); }
public class UnitOfWork : IUnitOfWork {
    private readonly DbContext _db;
    public IRepository<Order> Orders { get; }
    public void Commit() { _db.SaveChanges(); }
}
```

---

## 35. Implement CQRS for a simple read/write scenario

**Explanation:**
CQRS separates read and write models for scalability and clarity.

**Example:**
```csharp
// Command handler for writes
public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int> { ... }
// Query handler for reads
public class GetOrderHandler : IRequestHandler<GetOrderQuery, Order> { ... }
```

---

## 36. Implement transactional operations across multiple repositories

**Explanation:**
Use a single `DbContext` transaction to coordinate changes.

**Example:**
```csharp
public class UnitOfWork : IUnitOfWork {
    private readonly DbContext _db;
    public IRepository<Order> Orders { get; }
    public IRepository<Customer> Customers { get; }
    public void Commit() {
        using var tx = _db.Database.BeginTransaction();
        try {
            _db.SaveChanges();
            tx.Commit();
        } catch {
            tx.Rollback();
            throw;
        }
    }
}
```

---

## 37. Handle database migrations safely in production

**Explanation:**
Use EF Core migrations, apply them during deployment, and always back up the database first.

**Example:**
```csharp
// In deployment pipeline:
dotnet ef database update
// Always back up the database before applying migrations.
```

---

## 38. Implement bulk update and delete operations efficiently

**Explanation:**
Use `ExecuteSqlRaw` or a library like EFCore.BulkExtensions for efficient bulk operations.

**Example:**
```csharp
// Using raw SQL
_db.Database.ExecuteSqlRaw("UPDATE Orders SET Status = 'Closed' WHERE Expired = 1");
// Using EFCore.BulkExtensions
await _db.BulkUpdateAsync(orders);
```

---

## 39. Optimize Entity Framework tracking vs no-tracking queries

**Explanation:**
Use `.AsNoTracking()` for read-only queries to improve performance.

**Example:**
```csharp
var orders = _db.Orders.AsNoTracking().Where(o => o.Status == "Open").ToList();
```

---

## 40. Implement custom model binders

**Explanation:**
Custom model binders allow you to control how parameters are bound from requests.

**Example:**
```csharp
public class CustomBinder : IModelBinder {
    public Task BindModelAsync(ModelBindingContext context) {
        // Custom binding logic
        return Task.CompletedTask;
    }
}
// Register in Startup.cs
services.AddControllers(options => {
    options.ModelBinderProviders.Insert(0, new CustomBinderProvider());
});
```

---

## 41. Design APIs for backward compatibility

**Explanation:**
Version your APIs, avoid breaking changes, and use optional parameters or new endpoints for new features.

**Example:**
```csharp
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase { }
```

---

## 42. Implement request/response compression

**Explanation:**
Enable response compression middleware to reduce payload size.

**Example:**
```csharp
// Startup.cs
services.AddResponseCompression();
app.UseResponseCompression();
```

---

## 43. Implement async streaming using IAsyncEnumerable

**Explanation:**
Return `IAsyncEnumerable<T>` from controller actions for efficient streaming of large datasets.

**Example:**
```csharp
[HttpGet("/api/stream")]
public async IAsyncEnumerable<Item> Stream() {
    foreach (var item in _db.Items) {
        yield return item;
        await Task.Delay(10); // Simulate work
    }
}
```

---

## 44. Handle deadlocks and performance bottlenecks in SQL queries

**Explanation:**
Analyze query plans, add indexes, and avoid long-running transactions.

**Example:**
```csharp
// Use SQL Server Management Studio to analyze query plans
// Add indexes:
CREATE INDEX IX_Orders_Status ON Orders(Status);
// Keep transactions short and efficient.
```

---

## 45. Implement multi-tenant API support

**Explanation:**
Use a tenant identifier (e.g., from headers or claims) to filter data per tenant.

**Example:**
```csharp
[HttpGet("/api/orders")]
public IActionResult Get() {
    var tenantId = User.FindFirst("tenant_id")?.Value;
    var orders = _db.Orders.Where(o => o.TenantId == tenantId).ToList();
    return Ok(orders);
}
```

---

## 46. Design a fault-tolerant API using Polly

**Explanation:**
Use Polly to add retry, circuit breaker, and fallback policies for resilience.

**Example:**
```csharp
var policy = Policy.Handle<HttpRequestException>()
    .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
    .Wrap(Policy.Handle<HttpRequestException>().CircuitBreaker(2, TimeSpan.FromMinutes(1)));
await policy.ExecuteAsync(() => httpClient.GetAsync(url));
```

---

## 47. Implement circuit breaker pattern

**Explanation:**
A circuit breaker prevents repeated calls to a failing service. Use Polly's `CircuitBreaker` policy.

**Example:**
```csharp
var breaker = Policy.Handle<HttpRequestException>()
    .CircuitBreaker(2, TimeSpan.FromMinutes(1));
await breaker.ExecuteAsync(() => httpClient.GetAsync(url));
```

---

## 48. Handle secure file storage and retrieval

**Explanation:**
Encrypt files at rest and restrict access. Use Azure Blob Storage or AWS S3 with encryption.

**Example:**
```csharp
// Use Azure.Storage.Blobs or Amazon.S3 SDKs
// Set encryption and access policies in cloud provider
```

---

## 49. Implement audit logging for create/update/delete operations

**Explanation:**
Log changes to important entities for compliance and traceability.

**Example:**
```csharp
public class AuditLog {
    public int Id { get; set; }
    public string Action { get; set; }
    public string Entity { get; set; }
    public DateTime Timestamp { get; set; }
}
// In service:
_auditDb.AuditLogs.Add(new AuditLog { Action = "Create", Entity = "Order", Timestamp = DateTime.UtcNow });
_auditDb.SaveChanges();
```

---

## 50. Design APIs for high-throughput scenarios

**Explanation:**
Use batching, connection pooling, and asynchronous processing to maximize throughput.

**Example:**
```csharp
// Use async endpoints, minimize blocking calls, and batch writes where possible
```

---

## 51. Implement background job scheduling

**Explanation:**
Use libraries like Hangfire or Quartz.NET for scheduled jobs.

**Example:**
```csharp
// Hangfire: RecurringJob.AddOrUpdate(() => DoWork(), Cron.Daily);
```

---

## 52. Handle time-zone–aware date processing

**Explanation:**
Store dates in UTC and convert to local time as needed.

**Example:**
```csharp
var utcNow = DateTime.UtcNow;
var local = TimeZoneInfo.ConvertTimeFromUtc(utcNow, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
```

---

## 53. Implement database sharding or partitioning strategies

**Explanation:**
Split data across multiple databases or tables for scalability.

**Example:**
```csharp
// Use a sharding key to route queries to the correct database
```

---

## 54. Design a secure authentication flow with refresh tokens

**Explanation:**
Issue short-lived access tokens and long-lived refresh tokens. Validate refresh tokens on renewal.

**Example:**
```csharp
// On login: issue access and refresh tokens
// On refresh: validate refresh token, issue new access token
```

---

## 55. Implement custom authorization handlers

**Explanation:**
Custom handlers allow for complex authorization logic.

**Example:**
```csharp
public class CustomRequirement : IAuthorizationRequirement { }
public class CustomHandler : AuthorizationHandler<CustomRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
    {
        // Custom logic
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
// Register in Program.cs
```

---

## 56. Handle graceful API deprecation

**Explanation:**
Mark deprecated endpoints and provide migration guidance.

**Example:**
```csharp
[Obsolete("This endpoint will be removed in v2.0. Use /api/v2/... instead.")]
[HttpGet("/api/old-endpoint")]
public IActionResult OldEndpoint() => Ok();
```

---

## 57. Implement minimal APIs and compare them with MVC controllers

**Explanation:**
Minimal APIs are lightweight and ideal for microservices. MVC controllers offer more features and structure.

**Example:**
```csharp
// Minimal API
app.MapGet("/api/hello", () => "Hello World");
// MVC Controller
[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase { ... }
```

---

## 58. Optimize memory usage in high-load APIs

**Explanation:**
Use object pooling, avoid large object allocations, and release resources promptly.

**Example:**
```csharp
// Use ArrayPool<T>.Shared for large arrays
```

---

## 59. Implement API gateway integration

**Explanation:**
API gateways provide routing, security, and aggregation. Use Ocelot or Azure API Management.

**Example:**
```csharp
// Configure Ocelot with ocelot.json
```

---

## 60. Design APIs for eventual consistency

**Explanation:**
Use asynchronous messaging and background processing to achieve eventual consistency.

**Example:**
```csharp
// Use message queues and background workers
```

---

## 61. Implement database connection pooling best practices

**Explanation:**
Use default pooling, set max pool size, and avoid opening/closing connections repeatedly.

**Example:**
```csharp
// In connection string: "Max Pool Size=100;"
```

---

## 62. Handle API throttling and abuse prevention

**Explanation:**
Use rate limiting, quotas, and monitoring to prevent abuse.

**Example:**
```csharp
// Use AspNetCoreRateLimit or API gateway features
```

---

## 63. Implement saga pattern for distributed transactions

**Explanation:**
Saga pattern coordinates long-running transactions across services using compensating actions.

**Example:**
```csharp
// Use MassTransit or NServiceBus for saga orchestration
```

---

## 64. Design APIs for cloud-native deployments

**Explanation:**
Use stateless design, health checks, and configuration via environment variables.

**Example:**
```csharp
// Use appsettings.json and environment variables
```

---

## 65. Implement structured logging with Serilog

**Explanation:**
Structured logging enables querying and analysis. Use Serilog for rich, structured logs.

**Example:**
```csharp
// In Program.cs
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();
```

---

## 66. Implement application metrics and monitoring

**Explanation:**
Expose metrics endpoints and use tools like Prometheus and Grafana.

**Example:**
```csharp
// Use prometheus-net to expose /metrics endpoint
```

---

## 67. Debug a production issue caused by thread starvation

**Explanation:**
Thread starvation occurs when all threads are blocked. Use async/await, avoid blocking calls, and monitor thread pool usage.

**Example:**
```csharp
// Use async APIs, avoid Task.Wait() and Task.Result
// Monitor with dotnet-counters or Application Insights
```

---
