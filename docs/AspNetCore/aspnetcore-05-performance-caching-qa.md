# ASP.NET Core — Performance & Caching — Interview Q&A

---

### Q1. What's the difference between In-Memory Caching and Distributed Caching, and when does an app outgrow in-memory?

**Answer:**
"`IMemoryCache` stores cached data in the web server process's own memory — fast (no network hop), but private to that one instance. `IDistributedCache` (backed by Redis, SQL Server, etc.) stores cached data in a shared, external store that every instance of the app can read/write — slightly slower per access (network round trip), but consistent across instances. An app outgrows in-memory caching the moment it runs on more than one instance (which is the normal case for anything scaled horizontally, e.g., in Kubernetes) — otherwise each instance has its own separate, inconsistent cache, and a value cached by one instance is invisible to requests handled by another."

```csharp
builder.Services.AddMemoryCache(); // single-instance, in-process
builder.Services.AddStackExchangeRedisCache(options => options.Configuration = "redis-connection-string"); // shared across instances
```

**Where to use:** in-memory for a single-instance app or for data that's cheap to recompute if inconsistent across instances; distributed cache the moment you're running multiple instances and need cache consistency between them.

---

### Q2. What's the difference between Response Caching and Output Caching?

**Answer:**
"Response Caching (`UseResponseCaching()` + `[ResponseCache]`) works by setting standard HTTP caching headers (`Cache-Control`) and relying on the *client* or an intermediate proxy/CDN to actually do the caching — the ASP.NET Core middleware itself has fairly limited server-side caching behavior for it. Output Caching (introduced in .NET 7) is a proper server-side caching mechanism — the app itself stores and serves the full cached response for matching requests, with much more flexible policies (varying by query string, custom cache keys, programmatic invalidation) than Response Caching offered. Output Caching was introduced because Response Caching's server-side story was genuinely limited — most real server-side output caching needs (varying cache by specific parameters, tag-based invalidation) needed a more capable feature."

```csharp
builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("Orders", policy => policy.Expire(TimeSpan.FromMinutes(1)).Tag("orders"));
});
app.UseOutputCache();

app.MapGet("/orders", GetOrders).CacheOutput("Orders");

// Later, invalidate everything tagged "orders" (e.g., after a write) without waiting for expiry
await outputCacheStore.EvictByTagAsync("orders", default);
```

---

### Q3. Why should `IHttpClientFactory` always be used instead of `new HttpClient()` directly?

**Answer:**
"`HttpClient` implements `IDisposable`, which tempts people into a `using (var client = new HttpClient())` pattern per call — but each `HttpClient` owns its own underlying socket/connection pool, and disposing it doesn't release the underlying TCP connection immediately (it lingers in a `TIME_WAIT` state); do this repeatedly under load and you exhaust available sockets, causing `SocketException`s under real traffic. `IHttpClientFactory` manages a pool of `HttpMessageHandler` instances behind the scenes, reusing underlying connections properly, while still giving you a fresh logical `HttpClient` per use — solving socket exhaustion without you needing to manage connection pooling manually."

```csharp
// BAD - new HttpClient() per call, socket exhaustion risk under load
using var client = new HttpClient();
await client.GetAsync(url);

// GOOD - factory-managed, safe under sustained load
builder.Services.AddHttpClient("OrdersApi", client => client.BaseAddress = new Uri("https://api.example.com"));
// injected as IHttpClientFactory, then: var client = factory.CreateClient("OrdersApi");
```

---

### Q4. How would you add resilience (retry, circuit breaker, timeout) to outbound HTTP calls?

**Answer:**
"Attach Polly policies (or .NET 8's built-in `Microsoft.Extensions.Http.Resilience` package, which wraps Polly with sensible defaults) directly to a named/typed `HttpClient` registration — every call made through that client automatically goes through the configured retry/circuit-breaker/timeout policy, without repeating that logic at every call site. This is the same underlying Polly usage covered for EF Core-adjacent scenarios in [[linq-04-efcore-performance-production-qa]], applied here specifically to outbound HTTP rather than database calls."

```csharp
builder.Services.AddHttpClient("OrdersApi")
    .AddResilienceHandler("default", builder =>
    {
        builder.AddRetry(new HttpRetryStrategyOptions { MaxRetryAttempts = 3 });
        builder.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions());
        builder.AddTimeout(TimeSpan.FromSeconds(5));
    });
```

---

### Q5. Why does "async all the way" matter specifically in ASP.NET Core request handling?

**Answer:**
"Each incoming request in ASP.NET Core is handled by a thread-pool thread. If a request handler blocks that thread synchronously waiting on I/O (`.Result`, `.Wait()` on a `Task`), that thread is stuck doing nothing productive until the I/O completes — under load, with many concurrent requests all doing this, the thread pool can run out of available threads, causing requests to queue up and latency to spike, even though the server's CPU isn't actually busy. `await`ing properly releases the thread back to the pool while waiting on I/O, so it can serve other requests in the meantime — dramatically increasing the number of concurrent requests a given number of threads can actually sustain. The full mechanics of why blocking on async code is costly (and the classic deadlock risk) are covered in [[async-await-qa]]."

---

### Q6. What is Rate Limiting middleware, and what algorithms does it support?

**Answer:**
"Built into ASP.NET Core since .NET 7 (`Microsoft.AspNetCore.RateLimiting`), it throttles incoming requests according to a configured policy, returning `429 Too Many Requests` once a limit is exceeded. Four built-in algorithms: Fixed Window (a hard limit per fixed time interval, e.g., 100 requests per minute, resetting sharply at each interval boundary), Sliding Window (similar, but smooths the boundary effect by tracking sub-intervals instead of a hard reset), Token Bucket (tokens refill at a steady rate, requests consume tokens, allowing bursts up to the bucket's capacity), and Concurrency (limits how many requests can be *in flight* at once, rather than a rate over time — good for protecting a genuinely limited downstream resource)."

```csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", opt => { opt.Window = TimeSpan.FromMinutes(1); opt.PermitLimit = 100; });
});
app.UseRateLimiter();
app.MapGet("/orders", GetOrders).RequireRateLimiting("fixed");
```

---

### Q7. How would you diagnose a memory leak or high memory usage in a production ASP.NET Core app?

**Answer:**
"Capture a memory dump from the live process (`dotnet-dump collect`, or a cloud provider's built-in diagnostics) and analyze it with `dotnet-dump analyze`/`dotnet-gcdump`/WinDbg or a tool like PerfView — look for unexpectedly large object counts of a specific type, and trace their retention paths (what's still holding a reference preventing garbage collection). Common real causes in ASP.NET Core specifically: a captive-dependency-style bug where a Scoped service (holding growing state, like a `DbContext`'s change tracker) got captured by a Singleton and never gets recreated (see [[aspnetcore-02-dependency-injection-configuration-qa]]); event handlers subscribed but never unsubscribed, keeping otherwise-dead objects alive; or a static/singleton-scoped collection that keeps growing (a cache with no eviction policy)."
