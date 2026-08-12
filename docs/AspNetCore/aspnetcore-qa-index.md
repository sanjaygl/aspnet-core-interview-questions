# ASP.NET Core — Senior-Level Interview Questions — Index

Same approach as the LINQ/EF Core and Angular series — skips "what is middleware" trivia, focuses on what actually gets probed at senior level (pipeline ordering bugs, DI lifetime traps, real production concerns) plus hands-on coding asks. Cross-references [[async-await-qa]], [[linq-04-efcore-performance-production-qa]], and the microservices series where relevant instead of repeating that content. Grouped into 8 files.

---

## File 1 — `aspnetcore-01-middleware-pipeline-qa.md`
**Middleware & Request Pipeline**
1. What is middleware, and how does the request pipeline actually work (the "Russian doll" / nested delegate model)?
   - *Cross-question:* Why does middleware order matter, and what's a concrete bug caused by registering two specific middlewares in the wrong order (e.g., `UseAuthentication` after `UseAuthorization`, or exception handling registered too late)?
2. What's the difference between `Use`, `Run`, and `Map` when building the pipeline?
3. How would you write a custom middleware component, and what are the two common ways to write one (convention-based class vs inline `app.Use(...)`)?
4. How does exception-handling middleware (`UseExceptionHandler`) work, and how is it different from a try/catch inside a controller action?
   - *Cross-question:* Why does exception-handling middleware need to be registered *before* other middleware in the pipeline to actually catch their exceptions?
5. What is Endpoint Routing, and how does it differ from the older, middleware-based routing model?
6. How does `UseRouting()`/`UseEndpoints()` (or their minimal-API equivalent) relate to where `UseAuthentication()`/`UseAuthorization()` need to sit in the pipeline?
7. What is short-circuiting in the middleware pipeline, and what's a real example of a middleware that does it (e.g., a rate limiter returning 429 without calling `next()`)?

## File 2 — `aspnetcore-02-dependency-injection-configuration-qa.md`
**Dependency Injection & Configuration**
1. What are the three built-in service lifetimes (Transient, Scoped, Singleton), and what does "scoped" actually mean in the context of a web request?
2. What is a "captive dependency," and why is injecting a Scoped service into a Singleton a real production bug, not just a style issue?
   - *Cross-question:* Does ASP.NET Core detect and prevent this at startup, or does it fail silently/subtly at runtime?
3. How would you resolve a Scoped service from within a Singleton or a background service, when you genuinely need to (hint: `IServiceScopeFactory`)?
4. What is the `IOptions<T>` pattern, and what's the difference between `IOptions<T>`, `IOptionsSnapshot<T>`, and `IOptionsMonitor<T>`?
5. How does the configuration provider hierarchy work (appsettings.json → appsettings.{Environment}.json → environment variables → command line), and which one wins on conflict?
6. How would you validate configuration/options at startup so the app fails fast instead of throwing a `NullReferenceException` deep in some request handler later?
7. What's the difference between registering a service with `AddScoped<IFoo, Foo>()` vs `AddScoped<Foo>()` — does it matter which you inject?

## File 3 — `aspnetcore-03-authentication-authorization-qa.md`
**Authentication & Authorization**
1. What's the actual difference between Authentication and Authorization, in terms of what middleware/code does each?
2. How does JWT Bearer authentication work end-to-end in ASP.NET Core — what does the middleware actually validate?
   - *Cross-question:* What happens if the JWT's signature is valid but its `exp` claim has passed — where does that get checked?
3. What's the difference between Cookie authentication and JWT Bearer authentication, and when would you pick one over the other?
4. What is Claims-based identity, and how does a `ClaimsPrincipal` relate to `[Authorize(Roles = "Admin")]`?
5. What is Policy-based Authorization, and why is it more flexible than role checks alone?
6. How would you write a custom `IAuthorizationHandler` for a requirement that can't be expressed as a simple role/claim check (e.g., "user can only edit their own order")?
7. How does ASP.NET Core integrate with an external OAuth2/OpenID Connect provider (e.g., Azure AD/Entra ID), and what's the actual token flow at a high level?

## File 4 — `aspnetcore-04-web-api-mvc-qa.md`
**Web API & MVC**
1. Minimal APIs vs Controller-based APIs — what are the real trade-offs, and which would you pick for a large, long-lived API?
2. How does Model Binding work, and what's a case where it silently fails to bind what you expected?
3. How does Model Validation (`[Required]`, `ModelState.IsValid`, or minimal API validation) tie into automatic `400 Bad Request` responses?
4. What's the difference between an Action Filter and Middleware — if both can run code "around" a request, when do you pick one over the other?
   - *Cross-question:* Can an Action Filter access route data and model-bound parameters the way middleware can't — why does that matter?
5. What is `ProblemDetails`, and why is it the standard shape for error responses instead of a custom error object?
6. How would you implement API versioning in ASP.NET Core, and what are the trade-offs of URL-based vs header-based versioning?
7. What is Content Negotiation, and how does ASP.NET Core decide whether to return JSON vs XML (or another format) for a given request?

## File 5 — `aspnetcore-05-performance-caching-qa.md`
**Performance & Caching**
1. What's the difference between In-Memory Caching (`IMemoryCache`) and Distributed Caching (`IDistributedCache`, e.g., Redis), and when does an app outgrow in-memory caching?
2. What's the difference between Response Caching and Output Caching (the newer ASP.NET Core feature) — why did Output Caching get introduced when Response Caching already existed?
3. Why should `IHttpClientFactory` always be used instead of `new HttpClient()` directly — what specific problem does it solve (socket exhaustion)?
4. How would you add resilience (retry, circuit breaker, timeout) to outbound HTTP calls in ASP.NET Core? *(cross-reference: [[linq-04-efcore-performance-production-qa]] for the EF Core side of this)*
5. Why does "async all the way" matter specifically in ASP.NET Core request handling, and what's the actual throughput cost of blocking a request thread with `.Result`? *(cross-reference: [[async-await-qa]])*
6. What is Rate Limiting middleware (built into ASP.NET Core 7+), and what are the different rate limiting algorithms it supports (fixed window, sliding window, token bucket, concurrency)?
7. How would you diagnose a memory leak or high memory usage in a production ASP.NET Core app?

## File 6 — `aspnetcore-06-hosting-background-realtime-qa.md`
**Hosting, Background Processing & Real-Time**
1. What is Kestrel, and why does a production deployment usually still put a reverse proxy (IIS, Nginx, YARP) in front of it?
2. What is `IHostedService`/`BackgroundService`, and what's a realistic use case for one in a web API (that isn't "just use Hangfire for everything")?
   - *Cross-question:* Since a `BackgroundService` runs for the lifetime of the app (a singleton-scoped context), how does it safely access a Scoped service like a `DbContext`?
3. What is `IHostApplicationLifetime`, and how would you use it to implement graceful shutdown (finish in-flight requests/jobs before the process exits)?
4. What are Health Checks (`AddHealthChecks()`), and what's the difference between a liveness check and a readiness check in a Kubernetes-hosted ASP.NET Core app? *(cross-reference: [[microservices-06-resilience-fault-tolerance-qa]])*
5. What is SignalR, and how does it choose between WebSockets, Server-Sent Events, and long polling as its actual transport?
6. How would you scale SignalR across multiple server instances so a message from one instance reaches clients connected to a different instance (hint: backplane)?
7. What's the difference between using a `BackgroundService` for a recurring job vs a dedicated scheduler library (Hangfire/Quartz.NET) — when does a raw `BackgroundService` stop being enough?

## File 7 — `aspnetcore-07-testing-observability-scenarios-qa.md`
**Testing, Observability & Advanced/Scenario-Based**
1. What is `WebApplicationFactory<T>`, and how does it let you write true integration tests against your API without deploying it anywhere?
2. How would you replace a real dependency (a database, an external HTTP API) with a test double inside a `WebApplicationFactory`-based integration test?
3. How does structured logging with `ILogger` work, and why is `_logger.LogInformation("User {UserId} logged in", userId)` meaningfully better than string interpolation into the log message?
4. What is OpenTelemetry, and what's its role in an ASP.NET Core app compared to just using `ILogger`?
5. What's the difference between handling exceptions with `UseExceptionHandler` globally vs a `try/catch` in a specific controller action — when would you still want the latter?
6. How does CORS actually work at the HTTP level (preflight `OPTIONS` requests), and what's a common CORS misconfiguration that "works" in dev but fails in production?
7. How would you secure an ASP.NET Core API that's called by both a first-party frontend and third-party partners, with different trust levels?

## File 8 — `aspnetcore-08-coding-practice-qa.md`
**Coding Practice (interviewers frequently ask you to actually write these)**
1. Write a custom middleware component (e.g., one that logs request duration).
2. Write a custom Action Filter (e.g., one that validates an API key header before the action runs).
3. Write a custom `IAuthorizationHandler` + requirement (e.g., "resource owner only" check).
4. Write a minimal API endpoint with route parameters, model binding, and validation.
5. Write a custom model binder for a type the default binder can't handle automatically.
6. Write an `IHostedService`/`BackgroundService` that processes items from a queue.
7. Configure a rate limiter policy and apply it to a specific endpoint.
8. Configure `IHttpClientFactory` with a named client and a Polly retry policy attached.
