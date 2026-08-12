# ASP.NET Core — Middleware & Request Pipeline — Interview Q&A

---

### Q1. What is middleware, and how does the request pipeline actually work?

**Answer:**
"Middleware is a chain of components, each wrapping the next, that a request passes through on its way in and a response passes back through on its way out — often described as the 'Russian doll' model. Each middleware gets a reference to the next one (`next`) and decides whether to do work before calling it, after it, or both, or to short-circuit entirely and not call it at all. The order they're registered in `Program.cs` is exactly the order they execute on the way in, and the reverse order on the way back out."

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine("Before"); // runs on the way IN
    await next(context);
    Console.WriteLine("After");  // runs on the way OUT, after everything nested inside has finished
});
```

**Cross-question: Why does middleware order matter, and what's a concrete bug caused by registering two middlewares in the wrong order?**
"Because each middleware can only affect what happens to requests that reach it — anything registered before a short-circuiting middleware never gets a chance to run for requests that get stopped there, and anything registered after a middleware that depends on some context being set up won't find that context if it runs first. Classic real bug: registering `UseAuthorization()` before `UseAuthentication()` — authorization checks depend on `HttpContext.User` already being populated with the authenticated user's claims, which is exactly what the authentication middleware sets up; if authorization runs first, every request gets treated as unauthenticated, and protected endpoints incorrectly reject even valid, authenticated users."

```csharp
// WRONG ORDER - authorization runs before authentication ever populates HttpContext.User
app.UseAuthorization();
app.UseAuthentication();

// CORRECT ORDER
app.UseAuthentication();
app.UseAuthorization();
```

---

### Q2. What's the difference between `Use`, `Run`, and `Map`?

**Answer:**
"`Use` adds a middleware that can call `next()` to continue the pipeline — the normal building block for a chain. `Run` adds a terminal middleware that never calls `next()` at all — it's meant to be the last thing in a branch, since anything registered after a `Run` would never execute. `Map` (and `MapWhen`) branches the pipeline based on the request path (or an arbitrary condition) — requests matching the branch condition go down a completely separate sub-pipeline, then rejoin (or terminate) independently of the main pipeline."

```csharp
app.Use(async (context, next) => { /* can call next() */ await next(); });
app.Run(async context => { await context.Response.WriteAsync("Terminal - no next() call"); });
app.Map("/admin", adminApp => { adminApp.Run(async context => { /* separate branch, only for /admin */ }); });
```

---

### Q3. How would you write a custom middleware component?

**Answer:**
"Two common approaches: inline with `app.Use(...)` for something small and one-off, or a proper convention-based class (implementing the middleware pattern with a constructor taking `RequestDelegate next` and an `InvokeAsync(HttpContext)` method) for anything reusable, testable, or with its own dependencies injected. The class-based approach is preferred once the middleware has any real logic, since it's a proper, testable class rather than an anonymous lambda buried in `Program.cs`."

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
        var sw = Stopwatch.StartNew();
        await _next(context);
        _logger.LogInformation("{Path} took {Ms}ms", context.Request.Path, sw.ElapsedMilliseconds);
    }
}

// Registration - usually via an extension method for a clean Program.cs
app.UseMiddleware<RequestTimingMiddleware>();
```

---

### Q4. How does exception-handling middleware work, and how is it different from a try/catch in a controller action?

**Answer:**
"`UseExceptionHandler` wraps the *entire rest of the pipeline* in a try/catch — any unhandled exception thrown by anything downstream of it (routing, model binding, another middleware, a controller action) gets caught there, and it re-executes the request against a configured error-handling path/endpoint to produce a consistent error response. A try/catch inside one specific controller action only protects that one action — it's useful for handling a specific, expected failure mode locally (e.g., catching a known exception to return a specific error response), but it does nothing for exceptions thrown anywhere else in the app, which is exactly what the global exception-handling middleware is for."

```csharp
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
    });
});
```

**Cross-question: Why does exception-handling middleware need to be registered before other middleware in the pipeline to actually catch their exceptions?**
"Because middleware can only catch exceptions from things *nested inside* it — anything registered before the exception handler runs outside its try/catch entirely, so an exception thrown there propagates unhandled. `UseExceptionHandler` needs to be one of the very first things registered (right after things like `UseHsts`/`UseForwardedHeaders` that genuinely must run earlier) so that essentially everything else in the pipeline is wrapped inside its protection."

---

### Q5. What is Endpoint Routing, and how does it differ from the older, middleware-based routing model?

**Answer:**
"Endpoint Routing (the current model, since ASP.NET Core 3.0) separates *matching* a request to an endpoint (`UseRouting()`) from *executing* that endpoint (`UseEndpoints()`, or implicitly via `MapControllers()`/minimal API `Map...` calls). This separation is what lets middleware in between the two (like `UseAuthorization()`) inspect metadata about which endpoint was matched — e.g., its `[Authorize]` attributes — *before* actually executing it. The older model didn't have that clean separation; routing decisions and execution were more tangled together, making it harder for middleware to make decisions based on 'which endpoint is this request even going to.'"

---

### Q6. How does `UseRouting()`/`UseEndpoints()` relate to where `UseAuthentication()`/`UseAuthorization()` need to sit?

**Answer:**
"Authentication and Authorization middleware need to run *after* `UseRouting()` (so routing has already determined which endpoint matched, and its authorization metadata like `[Authorize]` is available to inspect) but *before* the endpoint actually executes (so unauthorized requests never reach the actual action code at all). The standard, correct order is: `UseRouting()` → `UseAuthentication()` → `UseAuthorization()` → (endpoint execution, either implicit or via `UseEndpoints()`)."

```csharp
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); // endpoint execution happens here, only for requests that passed auth
```

---

### Q7. What is short-circuiting in the middleware pipeline?

**Answer:**
"A middleware short-circuits by choosing not to call `next()` at all — it writes a response directly and returns, meaning nothing registered later in the pipeline ever runs for that request. A rate limiter is a clean real example: if a client has exceeded its allowed request rate, the rate-limiting middleware writes a `429 Too Many Requests` response immediately and returns, without ever calling `next()` — so routing, authentication, and the actual endpoint never execute for that throttled request at all, which is exactly the point: reject cheaply, before doing any real work."

```csharp
app.Use(async (context, next) =>
{
    if (IsRateLimited(context))
    {
        context.Response.StatusCode = 429;
        await context.Response.WriteAsync("Too Many Requests");
        return; // short-circuits - next() is never called, nothing downstream runs
    }
    await next(context);
});
```
