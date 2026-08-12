# ASP.NET Core — Testing, Observability & Advanced/Scenario-Based — Interview Q&A

---

### Q1. What is `WebApplicationFactory<T>`, and how does it enable true integration tests?

**Answer:**
"`WebApplicationFactory<TEntryPoint>` boots up your actual ASP.NET Core app in-memory, running through the real `Program.cs` startup (including the real middleware pipeline, DI configuration, and routing), and hands back an `HttpClient` that sends real HTTP requests directly against that in-memory server — no need to actually deploy anywhere or bind to a real network port. This lets you test the whole app, end-to-end through the actual pipeline (middleware, model binding, filters, everything), rather than testing a controller class in isolation with mocked dependencies."

```csharp
public class OrdersApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public OrdersApiTests(WebApplicationFactory<Program> factory) => _client = factory.CreateClient();

    [Fact]
    public async Task GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        var response = await _client.GetAsync("/orders/99999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
```

---

### Q2. How would you replace a real dependency with a test double inside a `WebApplicationFactory`-based test?

**Answer:**
"Override `ConfigureWebHost` and, within `ConfigureTestServices`, remove the real service registration and add a fake/mock one — this runs *after* the app's own `Program.cs` configuration, so it can cleanly swap out just the specific dependency you want to fake (a database, an external HTTP API client) while leaving everything else (real middleware, real routing, real filters) exactly as it runs in production."

```csharp
public class CustomWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IPaymentGateway>();
            services.AddSingleton<IPaymentGateway, FakePaymentGateway>(); // real gateway replaced for tests
        });
    }
}
```

---

### Q3. How does structured logging with `ILogger` work, and why is it better than string interpolation?

**Answer:**
"`_logger.LogInformation("User {UserId} logged in", userId)` doesn't just format a string immediately — it passes the message template and the raw parameter values separately to the logging provider, which can store them as structured, queryable fields (not just flattened text) if the sink supports it (Seq, Elasticsearch, Application Insights). That means you can later query 'show me every log entry where UserId = 42' directly as a structured filter, across every log statement that used that same named parameter — something a plain interpolated string (`$"User {userId} logged in"`) can't support, since by the time it's logged, it's just an opaque block of text with no separately queryable fields."

```csharp
// Structured - UserId is a queryable field in the log sink
_logger.LogInformation("User {UserId} logged in", userId);

// Just a flat string - UserId isn't separately queryable afterward
_logger.LogInformation($"User {userId} logged in");
```

---

### Q4. What is OpenTelemetry, and what's its role compared to just using `ILogger`?

**Answer:**
"OpenTelemetry is a vendor-neutral standard (and .NET library) for collecting the three observability pillars — traces (following a request across multiple services/components), metrics (numeric time-series data like request rate/latency), and logs — in one consistent way, exportable to whatever backend you choose (Jaeger, Prometheus, Application Insights, etc.) without changing instrumentation code if you switch backends later. `ILogger` alone only covers the logs pillar — OpenTelemetry's distributed tracing specifically is what lets you follow one logical request as it flows through an ASP.NET Core API, into a downstream HTTP call, into a database query, and see the whole timeline as one connected trace, which plain logging (even structured logging) doesn't give you without manually correlating log lines yourself."

```csharp
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddAspNetCoreInstrumentation().AddHttpClientInstrumentation().AddSource("MyApp"))
    .WithMetrics(metrics => metrics.AddAspNetCoreInstrumentation());
```

---

### Q5. What's the difference between handling exceptions with `UseExceptionHandler` globally vs a `try/catch` locally?

**Answer:**
"Global exception handling middleware is the right place for a consistent, catch-all safety net — turning *any* unhandled exception, anywhere in the app, into a well-formed error response, and is the appropriate place for cross-cutting concerns like logging every unhandled exception. A local `try/catch` inside a specific action is still worth keeping when you need to handle one *specific, expected* exception type differently right at the point it occurs — e.g., catching a known `InsufficientStockException` to return a specific, meaningful `409 Conflict` with business-relevant details, rather than letting it fall through to the generic global handler's generic `500` response. The two aren't mutually exclusive — use local catches for expected, specific business failure modes, and rely on the global handler as the safety net for everything else, genuinely unexpected."

---

### Q6. How does CORS actually work at the HTTP level?

**Answer:**
"For 'simple' requests (basic GET/POST with standard headers), the browser just sends the request and checks the response's `Access-Control-Allow-Origin` header before letting JavaScript read the response. For anything else — custom headers, non-simple content types like `application/json`, or non-GET/POST verbs — the browser first sends a 'preflight' `OPTIONS` request asking the server, 'if I were to send this real request, would you allow it, from this origin, with these headers/methods?' The server responds with `Access-Control-Allow-Origin`/`-Methods`/`-Headers`, and only if the browser is satisfied with that preflight response does it then send the actual request. Note this is entirely a browser-enforced security mechanism — CORS headers don't prevent server-to-server calls or tools like `curl`/Postman from working, only browser-based JavaScript is subject to it."

```csharp
builder.Services.AddCors(options =>
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("https://myapp.com").AllowAnyMethod().AllowAnyHeader()));
app.UseCors("AllowFrontend");
```

**Where this comes up as a trick question:** "why does my API 'work in Postman but fail from the browser'?" — CORS is exactly why: Postman isn't a browser, so it never enforces or triggers CORS at all, which is why a misconfigured CORS policy can go completely unnoticed until a real frontend hits it.

---

### Q7. How would you secure an ASP.NET Core API called by both a first-party frontend and third-party partners with different trust levels?

**Answer:**
"Use separate authentication schemes for each trust tier, and apply them per-endpoint rather than one blanket policy for everything — e.g., cookie or short-lived JWT auth (tightly scoped, tied to a logged-in user session) for the first-party frontend, and a separate API-key or client-credentials OAuth2 flow (scoped to specific permissions/rate limits per partner) for third-party integrations. Apply stricter rate limiting and narrower authorization scopes to the third-party-facing endpoints by default, and never assume 'authenticated' automatically means 'equally trusted' — a compromised or overly-permissive partner API key shouldn't be able to reach the same breadth of functionality a logged-in first-party user session can."

```csharp
builder.Services.AddAuthentication()
    .AddCookie("Frontend")
    .AddApiKeySupport("PartnerApiKey", options => { /* validate against a partner-specific store, narrower scope */ });

[Authorize(AuthenticationSchemes = "Frontend")]
public IActionResult InternalOnlyEndpoint() { /* ... */ }

[Authorize(AuthenticationSchemes = "PartnerApiKey", Policy = "PartnerReadOnly")]
public IActionResult PartnerFacingEndpoint() { /* ... */ }
```
