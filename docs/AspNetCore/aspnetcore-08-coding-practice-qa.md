# ASP.NET Core — Coding Practice — Interview Q&A

---

### Q1. Write a custom middleware component that logs request duration.

**Answer:**
```csharp
public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();
        _logger.LogInformation("{Method} {Path} responded {StatusCode} in {ElapsedMs}ms",
            context.Request.Method, context.Request.Path, context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
    }
}

// Program.cs
app.UseMiddleware<RequestTimingMiddleware>();
```

---

### Q2. Write a custom Action Filter that validates an API key header before the action runs.

**Answer:**
```csharp
public class RequireApiKeyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var providedKey))
        {
            context.Result = new UnauthorizedObjectResult(new { error = "Missing API key" });
            return;
        }

        var configuredKey = context.HttpContext.RequestServices
            .GetRequiredService<IConfiguration>()["ApiKey"];

        if (providedKey != configuredKey)
        {
            context.Result = new UnauthorizedObjectResult(new { error = "Invalid API key" });
        }
    }
}

[RequireApiKey]
[HttpGet("reports")]
public IActionResult GetReports() => Ok();
```

---

### Q3. Write a custom `IAuthorizationHandler` + requirement for a "resource owner only" check.

**Answer:**
```csharp
public class ResourceOwnerRequirement : IAuthorizationRequirement { }

public class ResourceOwnerHandler : AuthorizationHandler<ResourceOwnerRequirement, Order>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, ResourceOwnerRequirement requirement, Order resource)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null && resource.CustomerId == userId)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

// Registration
builder.Services.AddScoped<IAuthorizationHandler, ResourceOwnerHandler>();

// Usage in an endpoint
[HttpGet("orders/{id}")]
public async Task<IActionResult> GetOrder(int id, [FromServices] IAuthorizationService authService)
{
    var order = await _db.Orders.FindAsync(id);
    if (order is null) return NotFound();

    var result = await authService.AuthorizeAsync(User, order, new ResourceOwnerRequirement());
    return result.Succeeded ? Ok(order) : Forbid();
}
```

---

### Q4. Write a minimal API endpoint with route parameters, model binding, and validation.

**Answer:**
```csharp
public record CreateOrderRequest(
    [Required] string CustomerName,
    [Range(1, int.MaxValue)] int Quantity);

app.MapPost("/customers/{customerId}/orders", async (
    int customerId,
    CreateOrderRequest request,
    AppDbContext db) =>
{
    var validationContext = new ValidationContext(request);
    var errors = new List<ValidationResult>();
    if (!Validator.TryValidateObject(request, validationContext, errors, true))
        return Results.ValidationProblem(errors.ToDictionary(e => e.MemberNames.First(), e => new[] { e.ErrorMessage! }));

    var order = new Order { CustomerId = customerId, CustomerName = request.CustomerName, Quantity = request.Quantity };
    db.Orders.Add(order);
    await db.SaveChangesAsync();

    return Results.Created($"/orders/{order.Id}", order);
});
```

---

### Q5. Write a custom model binder for a type the default binder can't handle automatically.

**Answer:**
"A custom `IModelBinder` for something like a comma-separated list of integers passed as a single query string value, which the default binder wouldn't automatically split and convert."

```csharp
public class CommaSeparatedIntsBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        if (string.IsNullOrEmpty(value))
        {
            bindingContext.Result = ModelBindingResult.Success(new List<int>());
            return Task.CompletedTask;
        }

        var ids = value.Split(',', StringSplitOptions.TrimEntries)
            .Select(s => int.TryParse(s, out var n) ? n : (int?)null)
            .Where(n => n.HasValue)
            .Select(n => n!.Value)
            .ToList();

        bindingContext.Result = ModelBindingResult.Success(ids);
        return Task.CompletedTask;
    }
}

// Usage
[HttpGet("orders")]
public IActionResult GetOrders([ModelBinder(typeof(CommaSeparatedIntsBinder))] List<int> ids) => Ok(ids);
// GET /orders?ids=1,2,3 -> binds to List<int> { 1, 2, 3 }
```

---

### Q6. Write an `IHostedService`/`BackgroundService` that processes items from a queue.

**Answer:**
```csharp
public interface IBackgroundTaskQueue
{
    void Enqueue(Func<CancellationToken, Task> workItem);
    Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
}

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly Channel<Func<CancellationToken, Task>> _channel = Channel.CreateUnbounded<Func<CancellationToken, Task>>();
    public void Enqueue(Func<CancellationToken, Task> workItem) => _channel.Writer.TryWrite(workItem);
    public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken) =>
        await _channel.Reader.ReadAsync(cancellationToken);
}

public class QueuedHostedService : BackgroundService
{
    private readonly IBackgroundTaskQueue _queue;
    private readonly ILogger<QueuedHostedService> _logger;
    public QueuedHostedService(IBackgroundTaskQueue queue, ILogger<QueuedHostedService> logger)
    {
        _queue = queue; _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await _queue.DequeueAsync(stoppingToken);
            try { await workItem(stoppingToken); }
            catch (Exception ex) { _logger.LogError(ex, "Error executing background work item"); }
        }
    }
}

// Registration
builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
builder.Services.AddHostedService<QueuedHostedService>();

// Enqueueing work from a controller
app.MapPost("/orders/{id}/notify", (int id, IBackgroundTaskQueue queue) =>
{
    queue.Enqueue(async ct => await SendNotificationAsync(id, ct)); // fire-and-forget, processed in the background
    return Results.Accepted();
});
```

---

### Q7. Configure a rate limiter policy and apply it to a specific endpoint.

**Answer:**
```csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddSlidingWindowLimiter("search-policy", opt =>
    {
        opt.Window = TimeSpan.FromMinutes(1);
        opt.SegmentsPerWindow = 4;
        opt.PermitLimit = 30;
        opt.QueueLimit = 0; // reject immediately once over the limit, no queueing
    });
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsync("Rate limit exceeded. Try again shortly.", token);
    };
});

app.UseRateLimiter();
app.MapGet("/search", SearchHandler).RequireRateLimiting("search-policy");
```

---

### Q8. Configure `IHttpClientFactory` with a named client and a Polly retry policy attached.

**Answer:**
```csharp
builder.Services.AddHttpClient("PaymentGateway", client =>
{
    client.BaseAddress = new Uri("https://payments.example.com");
    client.Timeout = TimeSpan.FromSeconds(10);
})
.AddPolicyHandler(GetRetryPolicy())
.AddPolicyHandler(GetCircuitBreakerPolicy());

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
    Policy<HttpResponseMessage>
        .Handle<HttpRequestException>()
        .OrResult(r => (int)r.StatusCode >= 500)
        .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
    Policy<HttpResponseMessage>
        .Handle<HttpRequestException>()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

// Usage - retry/circuit-breaker apply automatically to every call through this named client
public class PaymentService(IHttpClientFactory factory)
{
    public async Task<HttpResponseMessage> ChargeAsync(PaymentRequest request)
    {
        var client = factory.CreateClient("PaymentGateway");
        return await client.PostAsJsonAsync("/charge", request);
    }
}
```
