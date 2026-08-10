# Middleware in ASP.NET Core – Complete Guide with Custom Middleware

## Introduction

Middleware is a fundamental concept in ASP.NET Core that forms the backbone of request processing. Every HTTP request passes through a series of middleware components that can inspect, modify, or short-circuit the request-response pipeline. Understanding middleware is crucial for building robust ASP.NET Core applications and is a common topic in technical interviews. This guide covers everything from basic concepts to creating custom middleware with real-world examples.

## What is Middleware?

Middleware is software that's assembled into an application pipeline to handle requests and responses. Each component in the middleware pipeline can:
- Process incoming HTTP requests
- Pass the request to the next middleware component
- Process the response before it returns to the client
- Short-circuit the pipeline by not calling the next middleware

Think of middleware as a chain of components where each component decides whether to pass the request to the next component in the pipeline.

## How Middleware Pipeline Works

The middleware pipeline in ASP.NET Core follows a request-response flow where each middleware component is executed in sequence for the request and in reverse order for the response.

**Request-Response Flow:**

```
Client Request
     ↓
[Middleware 1] ──→ (processes request)
     ↓
[Middleware 2] ──→ (processes request)
     ↓
[Middleware 3] ──→ (processes request)
     ↓
[Controller/Endpoint] ──→ (generates response)
     ↓
[Middleware 3] ──→ (processes response)
     ↓
[Middleware 2] ──→ (processes response)
     ↓
[Middleware 1] ──→ (processes response)
     ↓
Client Response
```

Each middleware can perform operations before and after calling `next()`, which invokes the next middleware in the pipeline.

## Built-in Middleware Examples

ASP.NET Core provides several built-in middleware components that handle common functionality:

### UseRouting
Adds route matching to the middleware pipeline. It determines which endpoint should handle the request based on the URL and HTTP method. Must be called before `UseAuthorization` and `UseEndpoints`.

### UseAuthentication
Enables authentication capabilities. It attempts to authenticate the user before the request reaches the endpoint. Must be called before `UseAuthorization`.

### UseAuthorization
Enables authorization capabilities. It verifies whether the authenticated user has permission to access the requested resource. Must be called after `UseAuthentication` but before `UseEndpoints`.

### UseEndpoints
Adds endpoint execution to the middleware pipeline. It maps routes to controllers, Razor Pages, or other endpoints. Typically the last middleware in the pipeline.

## Order of Middleware (Very Important)

The order in which middleware is registered is **critical** because it determines the execution sequence. Incorrect ordering can lead to security vulnerabilities, runtime errors, or unexpected behavior.

### ❌ Wrong Order (Security Issue)
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// WRONG: Authorization before Authentication
app.UseAuthorization();    // Tries to authorize unauthenticated users
app.UseAuthentication();   // Too late!
app.UseRouting();

app.Run();
```

### ✅ Correct Order
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Exception handling first
app.UseExceptionHandler("/Error");
app.UseHsts();

// HTTPS redirection
app.UseHttpsRedirection();

// Static files (short-circuits for static content)
app.UseStaticFiles();

// Routing
app.UseRouting();

// Authentication before Authorization
app.UseAuthentication();
app.UseAuthorization();

// Endpoints last
app.MapControllers();

app.Run();
```

**Key Rules:**
- Exception handling middleware should be first
- `UseAuthentication` must come before `UseAuthorization`
- `UseRouting` must come before `UseAuthorization` and `UseEndpoints`
- Static files middleware should be early to avoid unnecessary processing

## Creating Custom Middleware

Custom middleware allows you to add application-specific logic to the request pipeline. Follow these steps to create custom middleware:

### Step 1: Create Middleware Class
Create a class with a constructor that accepts `RequestDelegate` as a parameter. This delegate represents the next middleware in the pipeline.

### Step 2: Inject RequestDelegate
Store the `RequestDelegate` in a private field. You can also inject other services through the constructor.

### Step 3: Implement InvokeAsync Method
Create a public `InvokeAsync` or `Invoke` method that:
- Accepts `HttpContext` as the first parameter
- Can inject additional services as parameters (via dependency injection)
- Contains your custom logic
- Calls `await next(context)` to invoke the next middleware

### Step 4: Register Middleware
Use `app.UseMiddleware<T>()` or create an extension method for cleaner registration.

## Custom Middleware Example

Let's create a **Request Logging Middleware** that logs details about every HTTP request and response.

### RequestLoggingMiddleware.cs
```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log request details
        var startTime = DateTime.UtcNow;
        var requestPath = context.Request.Path;
        var requestMethod = context.Request.Method;

        _logger.LogInformation(
            "Incoming Request: {Method} {Path} at {Time}",
            requestMethod,
            requestPath,
            startTime);

        // Call the next middleware in the pipeline
        await _next(context);

        // Log response details
        var duration = DateTime.UtcNow - startTime;
        var statusCode = context.Response.StatusCode;

        _logger.LogInformation(
            "Completed Request: {Method} {Path} - Status: {StatusCode} - Duration: {Duration}ms",
            requestMethod,
            requestPath,
            statusCode,
            duration.TotalMilliseconds);
    }
}
```

### Extension Method (Optional but Recommended)
```csharp
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
```

## Registering Custom Middleware

### Using UseMiddleware<T>()
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Register custom middleware
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseRouting();
app.MapControllers();

app.Run();
```

### Using Extension Method (Cleaner)
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Register using extension method
app.UseRequestLogging();

app.UseRouting();
app.MapControllers();

app.Run();
```

**Best Practice:** Create extension methods for custom middleware to improve code readability and follow ASP.NET Core conventions.

## Short-Circuiting the Pipeline

Short-circuiting means terminating the pipeline execution without calling the next middleware. This is useful when you want to return a response immediately without further processing.

### Example: API Key Authentication Middleware
```csharp
public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate _next;
    private const string API_KEY_HEADER = "X-API-Key";

    public ApiKeyAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if API key exists
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var apiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key is missing");
            return; // Short-circuit: do NOT call _next()
        }

        // Validate API key
        if (!IsValidApiKey(apiKey))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Invalid API Key");
            return; // Short-circuit: do NOT call _next()
        }

        // API key is valid, continue to next middleware
        await _next(context);
    }

    private bool IsValidApiKey(string apiKey)
    {
        // Validate against stored keys (simplified)
        return apiKey == "your-secret-api-key";
    }
}
```

**When to Short-Circuit:**
- Authentication/Authorization failures
- Rate limiting exceeded
- Request validation failures
- Maintenance mode
- Cached responses

## Real-World Use Cases

### 1. Logging
Track request/response details, performance metrics, and debugging information.
```csharp
// Log execution time, request details, user information
```

### 2. Exception Handling
Centralized error handling and logging for all unhandled exceptions.
```csharp
app.UseExceptionHandler("/error");
// Or create custom exception middleware
```

### 3. Authentication & Authorization
Validate user identity and permissions before allowing access to resources.
```csharp
app.UseAuthentication();
app.UseAuthorization();
// Or custom JWT/API Key middleware
```

### 4. Request/Response Modification
Transform request headers, body, or response content.
```csharp
// Add security headers, compress responses, modify content
```

### 5. Rate Limiting
Protect APIs from abuse by limiting request frequency.
```csharp
// Track requests per IP/user and block excessive requests
```

### 6. Request Validation
Validate incoming requests before they reach controllers.
```csharp
// Check content types, validate headers, sanitize input
```

### 7. Caching
Cache responses to improve performance for frequently requested data.
```csharp
app.UseResponseCaching();
// Or implement custom caching logic
```

### 8. CORS
Handle cross-origin requests for APIs consumed by web applications.
```csharp
app.UseCors(policy => policy.AllowAnyOrigin());
```

## Middleware vs Filters (Comparison Table)

| Aspect | Middleware | Filters |
|--------|-----------|---------|
| **Scope** | Application-wide, affects all requests | Controller/Action specific |
| **Execution Level** | HTTP pipeline level | MVC/Razor Pages level |
| **Access to** | `HttpContext` only | `HttpContext` + MVC context (ModelState, ActionDescriptor) |
| **Order Control** | Order defined by registration sequence | Controlled by filter attributes and IOrderedFilter |
| **Use Cases** | Logging, authentication, exception handling, CORS | Action-specific validation, authorization, result transformation |
| **Dependency Injection** | Constructor and InvokeAsync parameters | Constructor injection and method parameters |
| **Short-Circuiting** | Don't call `next()` | Set `Result` property to short-circuit |
| **When to Use** | Cross-cutting concerns affecting all requests | MVC-specific logic for specific controllers/actions |

**General Rule:** Use middleware for application-wide concerns and filters for MVC-specific logic.

## Common Interview Questions

### 1. What is middleware in ASP.NET Core?
Middleware is a component assembled into the application pipeline to handle HTTP requests and responses. Each middleware can process the request, pass it to the next component, or short-circuit the pipeline. Middleware forms a chain where each component can perform operations before and after the next component.

### 2. Why is middleware order important?
Middleware order is critical because it determines the execution sequence. For example, authentication must occur before authorization, and exception handling should be first to catch all errors. Incorrect ordering can lead to security vulnerabilities (e.g., authorizing before authenticating) or runtime errors (e.g., routing before static files when needed).

### 3. What is the difference between Use, Run, and Map?
- **Use**: Adds middleware to the pipeline and can call the next middleware
- **Run**: Terminal middleware that doesn't call next; ends the pipeline
- **Map**: Branches the pipeline based on request path; creates conditional middleware execution

```csharp
app.Use(async (context, next) => {
    // Do work before next
    await next();
    // Do work after next
});

app.Run(async context => {
    await context.Response.WriteAsync("Terminal middleware");
    // No next() call
});

app.Map("/api", apiApp => {
    // Middleware only for /api paths
});
```

### 4. What is the difference between Middleware and Filters?
Middleware operates at the HTTP pipeline level and affects all requests application-wide. Filters work at the MVC level and are scoped to specific controllers or actions. Use middleware for cross-cutting concerns (logging, authentication) and filters for MVC-specific logic (action validation, result formatting).

### 5. How do you create custom middleware?
Create a class with:
1. Constructor accepting `RequestDelegate` (next middleware)
2. Public `InvokeAsync(HttpContext context)` method
3. Your custom logic before/after calling `await _next(context)`
4. Register using `app.UseMiddleware<T>()` in Program.cs

### 6. Can middleware have dependencies injected?
Yes. Constructor parameters (except `RequestDelegate`) are resolved from DI. You can also inject services into the `InvokeAsync` method as additional parameters. These are resolved per request using scoped or transient services.

```csharp
public async Task InvokeAsync(HttpContext context, IMyService service)
{
    // Use service here
    await _next(context);
}
```

### 7. How do you short-circuit the middleware pipeline?
Don't call `await _next(context)`. Instead, write directly to the response and return. This is useful for authentication failures, rate limiting, or returning cached responses.

### 8. What is the difference between app.Use() and app.UseMiddleware()?
- `app.Use()`: Inline middleware using lambda expressions; good for simple logic
- `app.UseMiddleware<T>()`: Registers a middleware class; better for complex, reusable middleware with constructor dependencies

### 9. Can you modify the response in middleware after calling next()?
Yes, but with caution. You can modify headers and status code only if the response hasn't started. To modify the response body, you need to replace the response stream before calling `next()`.

```csharp
var originalBody = context.Response.Body;
using var newBody = new MemoryStream();
context.Response.Body = newBody;

await _next(context); // Response written to newBody

newBody.Seek(0, SeekOrigin.Begin);
// Read and modify response
await newBody.CopyToAsync(originalBody);
```

### 10. How does dependency injection work in middleware?
Constructor parameters (except `RequestDelegate`) are resolved when the middleware is registered (singleton lifetime). Parameters in `InvokeAsync` are resolved per request, allowing scoped and transient services. Never inject scoped services in the constructor.

## Summary

- **Middleware forms the request-response pipeline** in ASP.NET Core, where each component can process requests and responses
- **Order matters critically** – wrong order can cause security issues or runtime errors (authentication before authorization, routing before endpoints)
- **Custom middleware is created** using a class with `RequestDelegate` constructor parameter and `InvokeAsync` method
- **Short-circuiting stops pipeline execution** by not calling `next()`, useful for authentication, validation, or caching
- **Middleware vs Filters**: Use middleware for application-wide concerns and filters for MVC-specific controller/action logic
- **Real-world use cases** include logging, exception handling, authentication, rate limiting, and request/response modification
- **Extension methods improve code readability** and follow ASP.NET Core conventions for registering custom middleware
- **Understanding middleware is essential** for building robust ASP.NET Core applications and succeeding in technical interviews
