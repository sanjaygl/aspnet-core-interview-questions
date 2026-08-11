# Microservices — 11. .NET-Specific Microservices — Interview Q&A

---

### Q1. How do you build Microservices using ASP.NET Core?

**Answer:**
"Each microservice is its own ASP.NET Core Web API project — its own solution/project, its own `Program.cs` bootstrapping its dependencies, its own Dockerfile, its own database context. Cross-cutting concerns (health checks, logging, auth) get configured per service via the standard ASP.NET Core middleware pipeline. Shared code that's genuinely common (e.g., a shared contracts/DTO library) goes in a small shared NuGet package — kept minimal, since over-sharing code between microservices reintroduces coupling."

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<OrderDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddHealthChecks().AddSqlServer(connectionString);

var app = builder.Build();
app.MapControllers();
app.MapHealthChecks("/health");
app.Run();
```

---

### Q2. How do you implement an API Gateway in .NET? (Ocelot, YARP)

**Answer:**
"Ocelot is a .NET-native API Gateway configured mostly through JSON route definitions — maps incoming request paths to downstream services, and supports auth, rate limiting, and aggregation out of the box. YARP (Yet Another Reverse Proxy) is Microsoft's own reverse-proxy library, more code-first/flexible, and is what Microsoft itself now recommends for building a gateway in .NET, since it's actively maintained and integrates cleanly with ASP.NET Core's pipeline."

```json
// Ocelot route example
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/orders/{id}",
      "DownstreamScheme": "https",
      "DownstreamHostAndPorts": [{ "Host": "order-service", "Port": 443 }],
      "UpstreamPathTemplate": "/gateway/orders/{id}"
    }
  ]
}
```

---

### Q3. How do you implement communication between services? (HttpClient, gRPC, MassTransit)

**Answer:**
"For synchronous REST calls, `HttpClient` via `IHttpClientFactory` (avoids socket exhaustion issues from manually managing `HttpClient` instances), often wrapped with Polly policies for retry/circuit breaker. For high-performance internal calls, gRPC with `Grpc.AspNetCore` and generated client/server code from `.proto` contracts. For async/event-driven messaging, MassTransit is the standard .NET abstraction over brokers (RabbitMQ, Azure Service Bus, Kafka) — it handles serialization, retries, and routing without hand-rolling broker-specific code."

```csharp
// IHttpClientFactory + Polly
builder.Services.AddHttpClient<InventoryClient>()
    .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(i)));

// MassTransit publish
await _bus.Publish(new OrderCreated { OrderId = order.Id });
```

---

### Q4. How do you implement Health Checks in ASP.NET Core?

**Answer:**
"Using the built-in `Microsoft.Extensions.Diagnostics.HealthChecks` package — register checks for critical dependencies (database, downstream services) via `AddHealthChecks()`, and expose them at an endpoint (commonly `/health`) that Kubernetes probes for liveness/readiness. You can also separate liveness (is the process alive) from readiness (is it ready for traffic) using tags."

```csharp
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString, tags: new[] { "ready" })
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" });

app.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = c => c.Tags.Contains("ready") });
app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = c => c.Tags.Contains("live") });
```

---

### Q5. How do you implement distributed logging?

**Answer:**
"Use a structured logging library like Serilog, configured to enrich every log entry with a correlation ID and ship logs to a centralized sink (Elasticsearch, Seq, Application Insights) instead of just writing to local files/console. Structured logging (logging objects/properties, not just formatted strings) is what makes centralized log querying actually useful — you can filter/search by specific fields (OrderId, CorrelationId) instead of grepping through text."

```csharp
Log.Logger = new LoggerConfiguration()
    .Enrich.WithProperty("Service", "OrderService")
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl)))
    .CreateLogger();

_logger.LogInformation("Order {OrderId} created for customer {CustomerId}", order.Id, order.CustomerId);
```

---

### Q6. How do you implement Retry and Circuit Breaker in .NET? (Polly)

**Answer:**
"Polly is the standard .NET resilience library — define policies (retry, circuit breaker, timeout, bulkhead) declaratively and apply them to any operation, either directly or wired into `IHttpClientFactory` so every call through that client automatically gets the policy applied without repeating the logic at every call site."

```csharp
// Retry with exponential backoff + circuit breaker, combined
var retryPolicy = Policy
    .Handle<HttpRequestException>()
    .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

var circuitBreakerPolicy = Policy
    .Handle<HttpRequestException>()
    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

builder.Services.AddHttpClient<InventoryClient>()
    .AddPolicyHandler(retryPolicy.WrapAsync(circuitBreakerPolicy));
```

**Where to use:** wrap every outbound `HttpClient` call to another microservice with at least a retry + timeout policy by default; add a circuit breaker for dependencies known to have occasional extended outages.
