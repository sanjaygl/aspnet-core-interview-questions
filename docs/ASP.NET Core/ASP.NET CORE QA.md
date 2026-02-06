## Q. What is IHostedService?

**A.**

IHostedService is an interface in .NET used to run background service or long-running tasks in ASP.NET Core application, outside the normal HTTP request-response lifecycle.

IHostedService is used to run background tasks in ASP.NET Core that start and stop with the application lifecycle

### Main Points

- Used for background processing
- Runs independently of HTTP requests
- Starts with application startup
- Stops gracefully on application shutdown
- Commonly used for queues, schedulers, and consumers
- Integrated with the ASP.NET Core hosting model

---

## Q. Explain ASP.NET Core

**A.**

ASP.NET Core is a modern, open-source web framework for building cross-platform web apps and APIs.

### Main Points

- Modern, open-source web framework
- Lightweight and modular architecture
- Cross-platform: runs on Windows, Linux, and macOS
- Unifies MVC and Web API into a single model
- Flexible for web apps, mobile backends, and IoT applications
- Backed by Microsoft and the .NET community
- Offers strong tooling and extensive documentation
- Vibrant ecosystem with active community support

---

## Q. How do you design a highly scalable ASP.NET Core application?

**A.**

Designing a highly scalable ASP.NET Core application means building the system in a way that it can handle increasing traffic and data load efficiently by using stateless architecture, asynchronous processing, optimized data access, and horizontal scaling without degrading performance.

Designing a scalable ASP.NET Core application focuses on efficient resource usage, performance optimization, and the ability to scale out easily as demand grows.

### Main Points

- Follows stateless architecture to enable horizontal scaling
- Uses asynchronous programming (async/await) to improve throughput
- Applies clean architecture and separation of concerns
- Optimizes database queries, indexing, and pagination
- Uses caching to reduce database load
- Supports horizontal scaling using load balancers and containers
- Offloads long-running tasks using background processing or messaging
- Includes monitoring, logging, and resilience mechanisms
- Uses health checks for readiness and liveness probes
- Implements rate limiting and throttling to protect APIs from overload
- Applies resilience patterns like retries, timeouts, and circuit breakers
- Secures the application using token-based authentication and authorization

### Example

```csharp
public class VehicleService
{
    private readonly IMemoryCache _cache;

    public VehicleService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<List<Vehicle>> GetVehiclesAsync()
    {
        // Try to get data from cache first
        if (_cache.TryGetValue("vehicles", out List<Vehicle> vehicles))
            return vehicles;

        // Simulate database call
        vehicles = await GetVehiclesFromDatabaseAsync();

        // Store result in cache to reduce DB load
        _cache.Set("vehicles", vehicles, TimeSpan.FromMinutes(5));

        return vehicles;
    }
}
```

---

## Q. What is resilience?

**A.**

Resilience is the ability of an application to handle temporary failures gracefully and continue working without crashing when a dependent service fails. Resilience ensures the system recovers quickly and avoids cascading failures.

### Main Points

- Handles temporary failures (API, network, database)
- Prevents cascading failures
- Improves system availability
- Very important in distributed systems

### Example

```csharp
builder.Services.AddHttpClient<ExternalApiClient>()
    .AddTransientHttpErrorPolicy(policy =>
        policy.WaitAndRetryAsync(3)); // Retry failed requests 3 times

public class ExternalApiClient
{
    private readonly HttpClient _httpClient;

    public ExternalApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDataAsync()
    {
        // Call external API
        var response = await _httpClient.GetAsync("/data");

        // Return response safely
        return response.IsSuccessStatusCode
            ? await response.Content.ReadAsStringAsync()
            : "Fallback response";
    }
}
```

---

## Q. Monolith vs Microservices in ASP.NET Core — when would you choose each?

**A.**

A Monolithic architecture is a single, unified application where all features are deployed together, while Microservices architecture consists of multiple small, independent services that communicate over APIs. The choice depends on application size, team structure, scalability needs, and operational complexity.

### When would you choose a Monolith?

A monolith is a single ASP.NET Core application where all modules (UI, business logic, data access) are built, deployed, and scaled together.

#### Main Points

- Simple architecture and easy to develop
- Faster initial development and deployment
- Easier debugging and testing
- Lower operational and infrastructure cost
- Best for small to medium applications
- Suitable for small teams or early-stage products

#### Choose Monolith when:

- Application is small or medium-sized
- Team size is small
- Requirements are stable
- Scalability needs are moderate

### When would you choose Microservices?

Microservices architecture breaks the application into independent ASP.NET Core services, each responsible for a specific business capability and deployable separately.

#### Main Points

- Independent deployment and scaling
- Better fault isolation
- Each service can scale independently
- Suitable for large and complex systems
- Supports distributed and cloud-native systems
- Requires strong DevOps and monitoring

#### Choose Microservices when:

- Application is large and complex
- Different modules have different scaling needs
- Multiple teams work independently
- High availability and fault isolation are required

---

## Q. What is rate limiting?

**A.**

Rate limiting is a technique used to restrict the number of requests a client can make to an API within a specific time window (for example, 100 requests per minute). Rate limiting protects the application from being overused or abused and ensures fair usage for all clients.

---

## Q. Why do we need to configure rate limiting?

**A.**

Rate limiting is configured to protect system resources, maintain performance, and prevent misuse of APIs.

### Main Points

- Prevents API abuse and denial-of-service (DoS) attacks
- Protects backend resources like CPU, memory, and database
- Ensures fair usage among multiple clients
- Improves application stability and availability
- Avoids system overload during traffic spikes
- Helps control unexpected or malicious traffic
- Commonly used in public and high-traffic APIs

### Simple Real-World Example

**Without rate limiting:**
- One client can send thousands of requests and overload the system

**With rate limiting:**
- Each client is allowed only a fixed number of requests per time window

---

## Q. How do we configure rate limiting in ASP.NET Core?

**A.**

In ASP.NET Core, rate limiting can be configured using the built-in rate limiting middleware to control how many requests a client can make within a given time window. This helps protect APIs from abuse and ensures stable performance.

### Main Points

- Configured at the middleware level
- Can limit requests per IP, user, or route
- Prevents API abuse and traffic spikes
- Improves application stability
- Built-in support available in modern ASP.NET Core

### Example

```csharp
using System.Threading.RateLimiting;

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.PermitLimit = 10;              // Allow 10 requests
        limiterOptions.Window = TimeSpan.FromMinutes(1); // Per 1 minute
        limiterOptions.QueueLimit = 0;                // No queuing
    });
});

var app = builder.Build();

app.UseRateLimiter(); // Enable rate limiting middleware

app.MapGet("/api/data", () => "Hello World")
   .RequireRateLimiting("fixed"); // Apply rate limit to this endpoint

app.Run();
```

### Code Explanation

- Allows 10 requests per minute per client
- Extra requests are rejected automatically
- Protects the API from overload

---

## Q. What is middleware in ASP.NET Core?

**A.**

Middleware is a component in .NET that handles HTTP requests and responses as they flow through the ASP.NET Core request pipeline. Each middleware can process the request, modify the response, or pass control to the next middleware.

### Main Points

- Executes on every HTTP request
- Forms a request–response pipeline
- Can read or modify request and response
- Can short-circuit the pipeline
- Order of middleware matters
- Used for logging, authentication, error handling, etc.

---

## Q. How do you create a custom middleware?

**A.**

A custom middleware is created by defining a class with an `Invoke` or `InvokeAsync` method and registering it in the application pipeline.

### Example

```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Before request is processed
        Console.WriteLine($"Request Path: {context.Request.Path}");

        await _next(context); // Call next middleware

        // After response is generated
        Console.WriteLine($"Response Status: {context.Response.StatusCode}");
    }
}
```

### Register Middleware in Pipeline

```csharp
var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>(); // Register custom middleware

app.MapControllers();
app.Run();
```

### Code Explanation

- Middleware runs for every request
- Logs request path before processing
- Calls the next middleware using `_next`
- Logs response status after execution

---

## Q. What is the difference between Use, Run, and Map in ASP.NET Core?

**A.**

`Use`, `Run`, and `Map` are methods used to configure middleware in the ASP.NET Core request pipeline, but they differ in how and when the request is passed to the next middleware.

### Main Points

- `Use` → Adds middleware and can call the next middleware
- `Run` → Adds terminal middleware (does not call next)
- `Map` → Branches the pipeline based on request path
- Order of middleware matters
- Used to control request flow

### Use

**Definition:**
`Use` adds middleware that can process the request and then pass control to the next middleware.

**Example:**

```csharp
app.Use(async (context, next) =>
{
    // Before next middleware
    Console.WriteLine("Before");

    await next(); // Call next middleware

    // After next middleware
    Console.WriteLine("After");
});
```

**Explanation:**
Runs before and after the next middleware.

### Run

**Definition:**
`Run` adds terminal middleware that ends the pipeline and does not call the next middleware.

**Example:**

```csharp
app.Run(async context =>
{
    // Pipeline ends here
    await context.Response.WriteAsync("Hello from Run");
});
```

**Explanation:**
No middleware after this will run.

### Map

**Definition:**
`Map` creates a branch in the pipeline based on the request path.

**Example:**

```csharp
app.Map("/admin", adminApp =>
{
    adminApp.Run(async context =>
    {
        await context.Response.WriteAsync("Admin area");
    });
});
```

**Explanation:**
This middleware runs only for `/admin` requests.

### Quick Comparison Table

| Method | Calls Next | Pipeline Ends | Conditional |
|--------|-----------|---------------|-------------|
| `Use`  | Yes       | No            | No          |
| `Run`  | No        | Yes           | No          |
| `Map`  | Depends   | Depends       | Yes (Path-based) |

---

## Q. What is the difference between Host.CreateApplicationBuilder and WebApplication.CreateBuilder?

**A.**

Both are used to configure and build .NET applications, but they are intended for different types of apps. `Host.CreateApplicationBuilder` is used for generic .NET applications (like background services or console apps), while `WebApplication.CreateBuilder` is specifically designed for ASP.NET Core web applications.

### Main Points

**Host.CreateApplicationBuilder**

- Creates a generic host
- Used for worker services, background jobs, console apps
- Does not include web features by default
- Requires manual configuration for web hosting
- More flexible for non-web workloads

**WebApplication.CreateBuilder**

- Creates a web host
- Used for ASP.NET Core APIs, MVC, Razor Pages
- Includes Kestrel, routing, middleware by default
- Simplifies startup configuration
- Most commonly used for web applications

---

## Q. Explain the request processing pipeline in ASP.NET Core.

**A.**

The request processing pipeline in ASP.NET Core is a sequence of middleware components that process HTTP requests and generate responses. Each middleware can inspect, modify, or short-circuit the request/response flow.

### Main Points

- Request flows through a chain of middleware components
- Each middleware can process the request before and after the next component
- Middleware order is critical and determines execution sequence
- Pipeline can be short-circuited by any middleware
- Response flows back through the same middleware in reverse order
- Supports cross-cutting concerns like logging, authentication, error handling

### High-Level Pipeline Diagram

```
┌──────────────────────────────────────────────────────────────────────────────────────┐
│                                                                                      │
│  Request  ──────►┌──────────────────┐      ┌──────────────────┐      ┌─────────────┐│
│                  │  Middleware 1    │      │  Middleware 2    │      │ Middleware 3││
│                  │                  │      │                  │      │             ││
│                  │  // Logic        │      │                  │      │             ││
│                  │  next(); ────────┼─────►│  // Logic        │      │             ││
│                  │                  │      │  next(); ────────┼─────►│  // Logic   ││
│                  │                  │      │                  │      │             ││
│                  │                  │      │                  │      │  // Endpoint││
│                  │                  │      │                  │      │             ││
│                  │                  │      │  // More logic   │◄─────┤             ││
│                  │  // More logic   │◄─────┤                  │      │             ││
│  Response ◄──────┤                  │      │                  │      │             ││
│                  └──────────────────┘      └──────────────────┘      └─────────────┘│
│                                                                                      │
└──────────────────────────────────────────────────────────────────────────────────────┘

Flow Explanation:
1. Request enters Middleware 1 → executes logic → calls next()
2. Request enters Middleware 2 → executes logic → calls next()
3. Request enters Middleware 3 → executes logic → reaches endpoint
4. Response flows back through Middleware 3 → executes remaining logic
5. Response flows back through Middleware 2 → executes remaining logic
6. Response flows back through Middleware 1 → executes remaining logic
7. Response is returned to client
```

### Key Pipeline Characteristics

- **Sequential Processing**: Middleware executes in the order they are registered
- **Bidirectional Flow**: Request flows down, response flows back up
- **Short-Circuiting**: Any middleware can stop the pipeline and return a response
- **Composable**: Middleware can be added, removed, or reordered easily
- **Cross-Cutting Concerns**: Handles logging, caching, security across all requests

### Example

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Configure the HTTP request pipeline
app.UseExceptionHandler("/error");  // 1. Handle exceptions
app.UseHttpsRedirection();           // 2. Redirect HTTP to HTTPS
app.UseStaticFiles();                // 3. Serve static files
app.UseRouting();                    // 4. Match endpoints
app.UseAuthentication();             // 5. Verify identity
app.UseAuthorization();              // 6. Check permissions
app.UseRateLimiter();                // 7. Apply rate limiting
app.MapControllers();                // 8. Map endpoints

app.Run();
```

---

## Q. What is a Request delegate and how is it used?

**A.**

In ASP.NET Core, a Request delegate is a function that processes and handles an incoming HTTP request. It's the core building block of the request processing pipeline, which is essentially a series of middleware components that handle the request one after the other.

### Main Points

- Represents a function that handles HTTP requests
- Core building block of the middleware pipeline
- Takes `HttpContext` as a parameter
- Returns a `Task` (asynchronous operation)
- Can be chained together to form the pipeline
- Used internally by `Use`, `Run`, and `Map` methods
- Signature: `Func<HttpContext, Task>` or `Func<HttpContext, Func<Task>, Task>`

### Example

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Request delegate using Run (terminal middleware)
app.Run(async context =>
{
    await context.Response.WriteAsync("Hello from request delegate!");
});

app.Run();
```

### Request Delegate with Next

```csharp
// Request delegate with next delegate
app.Use(async (context, next) =>
{
    // Before processing
    await context.Response.WriteAsync("Before next middleware\n");
    
    await next(); // Call next delegate in pipeline
    
    // After processing
    await context.Response.WriteAsync("After next middleware\n");
});
```

### Custom Middleware Using Request Delegate

```csharp
public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next; // Store the next delegate
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Process request
        Console.WriteLine("Processing request...");
        
        // Call the next delegate/middleware
        await _next(context);
        
        // Process response
        Console.WriteLine("Processing response...");
    }
}
```

### Key Points

- **RequestDelegate** is the function signature that all middleware uses
- It enables the chaining of middleware components
- Each middleware receives the next `RequestDelegate` to call
- This creates the pipeline pattern for request processing