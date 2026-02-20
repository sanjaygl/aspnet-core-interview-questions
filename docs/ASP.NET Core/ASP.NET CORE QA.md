# ASP. NET Core Interview Questions

## Table of Contents

01. [What is IHostedService?](#q-what-is-ihostedservice)
02. [Explain ASP.NET Core](#q-explain-asp-net-core)
03. [How do you design a highly scalable ASP.NET Core application?](#q-how-do-you-design-a-highly-scalable-asp-net-core-application)
04. [What is resilience?](#q-what-is-resilience)
05. [Monolith vs Microservices in ASP.NET Core — when would you choose each?](#q-monolith-vs-microservices-in-asp-net-core--when-would-you-choose-each)
06. [What is rate limiting?](#q-what-is-rate-limiting)
07. [Why do we need to configure rate limiting?](#q-why-do-we-need-to-configure-rate-limiting)
08. [How do we configure rate limiting in ASP.NET Core?](#q-how-do-we-configure-rate-limiting-in-asp-net-core)
09. [What is middleware in ASP.NET Core?](#q-what-is-middleware-in-asp-net-core)
10. [How do you create a custom middleware?](#q-how-do-you-create-a-custom-middleware)
11. [What is the difference between Use, Run, and Map in ASP.NET Core?](#q-what-is-the-difference-between-use-run-and-map-in-asp-net-core)
12. [What is the difference between Host.CreateApplicationBuilder and WebApplication.CreateBuilder?](#q-what-is-the-difference-between-host-createapplicationbuilder-and-webapplication-createbuilder)
13. [Explain the request processing pipeline in ASP.NET Core.](#q-explain-the-request-processing-pipeline-in-asp-net-core)
14. [What is a Request delegate and how is it used?](#q-what-is-a-request-delegate-and-how-is-it-used)
15. [What is the difference between AddScoped, AddTransient, and AddSingleton in ASP.NET Core?](#q-what-is-the-difference-between-addscoped-addtransient-and-addsingleton-in-asp-net-core)
16. [What is the difference between Middleware, Filters, and DelegatingHandler in ASP.NET Core?](#q-what-is-the-difference-between-middleware-filters-and-delegatinghandler-in-asp-net-core)
17. [What are Cross-Cutting Concerns and how do you handle them in ASP.NET Core?](#q-what-are-cross-cutting-concerns-and-how-do-you-handle-them-in-asp-net-core)
18. [How do you control Middleware execution order in ASP.NET Core?](#q-how-do-you-control-middleware-execution-order-in-asp-net-core)
19. [Can Middleware short-circuit the request? When would you do that?](#q-can-middleware-short-circuit-the-request-when-would-you-do-that)
20. [How do you reload configuration without restarting the app in ASP.NET Core?](#q-how-do-you-reload-configuration-without-restarting-the-app-in-asp-net-core)
21. [How do you implement Event-Driven Architecture in .NET using Kafka? (Short)](#q-how-do-you-implement-event-driven-architecture-in-net-using-kafka-short)
22. [Why do we use Event-Driven Architecture instead of direct API-to-API communication?](#q-why-do-we-use-event-driven-architecture-instead-of-direct-api-to-api-communication)
23. [What is Semaphore / Lock and why do we use it?](#q-what-is-semaphore--lock-and-why-do-we-use-it)
24. [What is the difference between throw and throw ex in C#?](#q-what-is-the-difference-between-throw-and-throw-ex-in-c)
25. [If a catch block throws an exception, will the finally block execute? Can we have multiple catch blocks?](#q-if-a-catch-block-throws-an-exception-will-the-finally-block-execute-can-we-have-multiple-catch-blocks)
26. [What is Microservices? Have you experienced Microservices architecture?](#q-what-is-microservices-have-you-experienced-in-microservices-architecture)
27. [What is Saga in Microservices? Explain Choreography and Orchestration.](#q-what-is-saga-in-microservices-explain-choreography-and-orchestration)
28. [How do we manage databases in distributed systems?](#q-how-do-we-manage-databases-in-distributed-systems)
29. [What is Dependency Injection? Explain all three lifetimes with a good example.](#q-what-is-dependency-injection-explain-all-three-lifetimes-with-a-good-example)
30. [What is Middleware? Explain in detail and describe custom Middleware.](#q-what-is-middleware-explain-in-detail-and-describe-custom-middleware)
31. [How can we set rate limiting in custom Middleware to restrict to 5 (or any number of) calls?](#q-how-can-we-set-rate-limiting-in-custom-middleware-to-restrict-to-5-or-any-number-of-calls)
32. [Design a workflow for a Microservices-based system.](#q-design-a-workflow-for-a-microservices-based-system)
33. [How do event-based services work (e.g., Kafka, RabbitMQ)?](#q-how-do-event-based-services-work-eg-kafka-rabbitmq)
34. [Given an E-commerce scenario, explain how Kafka messaging fits for Order, Inventory, and Payment services.](#q-given-an-e-commerce-scenario-explain-how-kafka-messaging-fits-for-order-inventory-and-payment-services)
35. [How can we optimize Kafka? Explain partitions, offsets, consumer types, etc.](#q-how-can-we-optimize-kafka-explain-partitions-offsets-consumer-types-etc)
36. [Write a program to create a custom Dictionary (do not use the built-in Dictionary).](#q-write-a-program-to-create-a-custom-dictionary-do-not-use-the-built-in-dictionary)
37. [Create a custom Order Controller.](#q-create-a-custom-order-controller)

---

## Q. What is IHostedService?

**A.**

IHostedService is an interface in . NET used to run background service or long-running tasks in ASP. NET Core application, outside the normal HTTP request-response lifecycle.

IHostedService is used to run background tasks in ASP. NET Core that start and stop with the application lifecycle

### Main Points

* Used for background processing
* Runs independently of HTTP requests
* Starts with application startup
* Stops gracefully on application shutdown
* Commonly used for queues, schedulers, and consumers
* Integrated with the ASP. NET Core hosting model

---

## Q. Explain ASP. NET Core

**A.**

ASP. NET Core is a modern, open-source web framework for building cross-platform web apps and APIs.

### Main Points

* Modern, open-source web framework
* Lightweight and modular architecture
* Cross-platform: runs on Windows, Linux, and macOS
* Unifies MVC and Web API into a single model
* Flexible for web apps, mobile backends, and IoT applications
* Backed by Microsoft and the . NET community
* Offers strong tooling and extensive documentation
* Vibrant ecosystem with active community support

---

## Q. How do you design a highly scalable ASP. NET Core application?

**A.**

Designing a highly scalable ASP. NET Core application means building the system in a way that it can handle increasing traffic and data load efficiently by using stateless architecture, asynchronous processing, optimized data access, and horizontal scaling without degrading performance.

Designing a scalable ASP. NET Core application focuses on efficient resource usage, performance optimization, and the ability to scale out easily as demand grows.

### Main Points

* Follows stateless architecture to enable horizontal scaling
* Uses asynchronous programming (async/await) to improve throughput
* Applies clean architecture and separation of concerns
* Optimizes database queries, indexing, and pagination
* Uses caching to reduce database load
* Supports horizontal scaling using load balancers and containers
* Offloads long-running tasks using background processing or messaging
* Includes monitoring, logging, and resilience mechanisms
* Uses health checks for readiness and liveness probes
* Implements rate limiting and throttling to protect APIs from overload
* Applies resilience patterns like retries, timeouts, and circuit breakers
* Secures the application using token-based authentication and authorization

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

* Handles temporary failures (API, network, database)
* Prevents cascading failures
* Improves system availability
* Very important in distributed systems

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

## Q. Monolith vs Microservices in ASP. NET Core — when would you choose each?

**A.**

A Monolithic architecture is a single, unified application where all features are deployed together, while Microservices architecture consists of multiple small, independent services that communicate over APIs. The choice depends on application size, team structure, scalability needs, and operational complexity.

### When would you choose a Monolith?

A monolith is a single ASP. NET Core application where all modules (UI, business logic, data access) are built, deployed, and scaled together.

#### Main Points

* Simple architecture and easy to develop
* Faster initial development and deployment
* Easier debugging and testing
* Lower operational and infrastructure cost
* Best for small to medium applications
* Suitable for small teams or early-stage products

#### Choose Monolith when:

* Application is small or medium-sized
* Team size is small
* Requirements are stable
* Scalability needs are moderate

### When would you choose Microservices?

Microservices architecture breaks the application into independent ASP. NET Core services, each responsible for a specific business capability and deployable separately.

#### Main Points

* Independent deployment and scaling
* Better fault isolation
* Each service can scale independently
* Suitable for large and complex systems
* Supports distributed and cloud-native systems
* Requires strong DevOps and monitoring

#### Choose Microservices when:

* Application is large and complex
* Different modules have different scaling needs
* Multiple teams work independently
* High availability and fault isolation are required

---

## Q. What is rate limiting?

**A.**

Rate limiting is a technique used to restrict the number of requests a client can make to an API within a specific time window (for example, 100 requests per minute). Rate limiting protects the application from being overused or abused and ensures fair usage for all clients.

---

## Q. Why do we need to configure rate limiting?

**A.**

Rate limiting is configured to protect system resources, maintain performance, and prevent misuse of APIs.

### Main Points

* Prevents API abuse and denial-of-service (DoS) attacks
* Protects backend resources like CPU, memory, and database
* Ensures fair usage among multiple clients
* Improves application stability and availability
* Avoids system overload during traffic spikes
* Helps control unexpected or malicious traffic
* Commonly used in public and high-traffic APIs

### Simple Real-World Example

**Without rate limiting:**
* One client can send thousands of requests and overload the system

**With rate limiting:**
* Each client is allowed only a fixed number of requests per time window

---

## Q. How do we configure rate limiting in ASP. NET Core?

**A.**

In ASP. NET Core, rate limiting can be configured using the built-in rate limiting middleware to control how many requests a client can make within a given time window. This helps protect APIs from abuse and ensures stable performance.

### Main Points

* Configured at the middleware level
* Can limit requests per IP, user, or route
* Prevents API abuse and traffic spikes
* Improves application stability
* Built-in support available in modern ASP. NET Core

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

* Allows 10 requests per minute per client
* Extra requests are rejected automatically
* Protects the API from overload

---

## Q. What is middleware in ASP. NET Core?

**A.**

Middleware is a component in . NET that handles HTTP requests and responses as they flow through the ASP. NET Core request pipeline. Each middleware can process the request, modify the response, or pass control to the next middleware.

### Main Points

* Executes on every HTTP request
* Forms a request–response pipeline
* Can read or modify request and response
* Can short-circuit the pipeline
* Order of middleware matters
* Used for logging, authentication, error handling, etc.

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

* Middleware runs for every request
* Logs request path before processing
* Calls the next middleware using `_next`
* Logs response status after execution

---

## Q. What is the difference between Use, Run, and Map in ASP. NET Core?

**A.**

`Use` , `Run` , and `Map` are methods used to configure middleware in the ASP. NET Core request pipeline, but they differ in how and when the request is passed to the next middleware.

### Main Points

* `Use` → Adds middleware and can call the next middleware
* `Run` → Adds terminal middleware (does not call next)
* `Map` → Branches the pipeline based on request path
* Order of middleware matters
* Used to control request flow

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
| `Use` | Yes       | No            | No          |
| `Run` | No        | Yes           | No          |
| `Map` | Depends   | Depends       | Yes (Path-based) |

---

## Q. What is the difference between Host. CreateApplicationBuilder and WebApplication. CreateBuilder?

**A.**

Both are used to configure and build . NET applications, but they are intended for different types of apps. `Host.CreateApplicationBuilder` is used for generic . NET applications (like background services or console apps), while `WebApplication.CreateBuilder` is specifically designed for ASP. NET Core web applications.

### Main Points

**Host. CreateApplicationBuilder**

* Creates a generic host
* Used for worker services, background jobs, console apps
* Does not include web features by default
* Requires manual configuration for web hosting
* More flexible for non-web workloads

**WebApplication. CreateBuilder**

* Creates a web host
* Used for ASP. NET Core APIs, MVC, Razor Pages
* Includes Kestrel, routing, middleware by default
* Simplifies startup configuration
* Most commonly used for web applications

---

## Q. Explain the request processing pipeline in ASP. NET Core.

**A.**

The request processing pipeline in ASP. NET Core is a sequence of middleware components that process HTTP requests and generate responses. Each middleware can inspect, modify, or short-circuit the request/response flow.

### Main Points

* Request flows through a chain of middleware components
* Each middleware can process the request before and after the next component
* Middleware order is critical and determines execution sequence
* Pipeline can be short-circuited by any middleware
* Response flows back through the same middleware in reverse order
* Supports cross-cutting concerns like logging, authentication, error handling

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
01. Request enters Middleware 1 → executes logic → calls next()
02. Request enters Middleware 2 → executes logic → calls next()
03. Request enters Middleware 3 → executes logic → reaches endpoint
04. Response flows back through Middleware 3 → executes remaining logic
05. Response flows back through Middleware 2 → executes remaining logic
06. Response flows back through Middleware 1 → executes remaining logic
07. Response is returned to client
```

### Key Pipeline Characteristics

* **Sequential Processing**: Middleware executes in the order they are registered
* **Bidirectional Flow**: Request flows down, response flows back up
* **Short-Circuiting**: Any middleware can stop the pipeline and return a response
* **Composable**: Middleware can be added, removed, or reordered easily
* **Cross-Cutting Concerns**: Handles logging, caching, security across all requests

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

In ASP. NET Core, a Request delegate is a function that processes and handles an incoming HTTP request. It's the core building block of the request processing pipeline, which is essentially a series of middleware components that handle the request one after the other.

### Main Points

* Represents a function that handles HTTP requests
* Core building block of the middleware pipeline
* Takes `HttpContext` as a parameter
* Returns a `Task` (asynchronous operation)
* Can be chained together to form the pipeline
* Used internally by `Use`,  `Run`, and `Map` methods
* Signature: `Func<HttpContext, Task>` or `Func<HttpContext, Func<Task>, Task>`

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

* **RequestDelegate** is the function signature that all middleware uses
* It enables the chaining of middleware components
* Each middleware receives the next `RequestDelegate` to call
* This creates the pipeline pattern for request processing

---

## Q. What is the difference between AddScoped, AddTransient, and AddSingleton in ASP. NET Core?

**A.**

`AddScoped` , `AddTransient` , and `AddSingleton` define the lifetime of dependency injection services in ASP. NET Core. They control how many instances are created and when they are reused.

### Main Points

* **AddScoped** → One instance per HTTP request (shared within the same request)
* **AddTransient** → New instance every time it is injected (even within the same request)
* **AddSingleton** → One instance for the entire application lifetime (shared across all requests)

### AddScoped Example

```csharp
// Registration
builder.Services.AddScoped<IUserService, UserService>();

// Usage in Controllers
public class EmployeeController : ControllerBase
{
    private readonly IUserService _userService;
    
    public EmployeeController(IUserService userService)
    {
        _userService = userService; // Instance A
    }
}

public class OrderController : ControllerBase
{
    private readonly IUserService _userService;
    
    public OrderController(IUserService userService)
    {
        _userService = userService; // Same Instance A (within same request)
    }
}
```

**Result:** One instance created per HTTP request and shared between both controllers in the same request.

---

### AddTransient Example

```csharp
// Registration
builder.Services.AddTransient<IUserService, UserService>();

// Usage in Controllers
public class EmployeeController : ControllerBase
{
    private readonly IUserService _userService;
    
    public EmployeeController(IUserService userService)
    {
        _userService = userService; // Instance A
    }
}

public class OrderController : ControllerBase
{
    private readonly IUserService _userService;
    
    public OrderController(IUserService userService)
    {
        _userService = userService; // Instance B (different instance)
    }
}
```

**Result:** Two separate instances created per HTTP request. `EmployeeController` gets Instance A, `OrderController` gets Instance B.

---

### AddSingleton Example

```csharp
// Registration
builder.Services.AddSingleton<IUserService, UserService>();

// Usage in Controllers
public class EmployeeController : ControllerBase
{
    private readonly IUserService _userService;
    
    public EmployeeController(IUserService userService)
    {
        _userService = userService; // Instance A
    }
}

public class OrderController : ControllerBase
{
    private readonly IUserService _userService;
    
    public OrderController(IUserService userService)
    {
        _userService = userService; // Same Instance A
    }
}
```

**Result:** One instance created for the entire application lifetime and shared across all requests and controllers.

---

### Quick Comparison

| Lifetime | Instances Per Request | Shared Across Requests | Use Case |
|----------|----------------------|------------------------|----------|
| **AddScoped** | 1 (shared within request) | No | Database contexts, request-specific services |
| **AddTransient** | Multiple (one per injection) | No | Lightweight, stateless services |
| **AddSingleton** | 1 (application-wide) | Yes | Configuration, caching, logging |

---

## Q. What is the difference between Middleware, Filters, and DelegatingHandler in ASP. NET Core?

**A.**

Middleware, Filters, and DelegatingHandler are three different components that handle request/response processing at different levels. Middleware works on the entire application pipeline, Filters work inside the MVC pipeline around controller actions, and DelegatingHandler handles outgoing HTTP requests to external APIs.

### Main Points

* **Middleware** → Handles incoming HTTP requests globally (entire application pipeline)
* **Filters** → Executes inside MVC pipeline around controller actions
* **DelegatingHandler** → Processes outgoing HTTP requests via HttpClient

### Middleware

Handles incoming HTTP requests and responses globally. Executes before and after the request reaches MVC or endpoints.

**Common uses:** Logging, authentication, exception handling, CORS

**Example:**

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
        Console.WriteLine($"Request: {context.Request.Path}");
        await _next(context);
        Console.WriteLine($"Response: {context.Response.StatusCode}");
    }
}

// Registration
app.UseMiddleware<RequestLoggingMiddleware>();
```

### Filters

Run inside the MVC pipeline around controller actions. Can be applied at global, controller, or action level.

**Common uses:** Validation, authorization, action-specific logging

**Example:**

```csharp
public class LogActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"Action executing: {context.ActionDescriptor.DisplayName}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"Action executed");
    }
}

// Apply at action level
[ServiceFilter(typeof(LogActionFilter))]
[HttpGet]
public IActionResult GetUsers() => Ok();
```

### DelegatingHandler

Used with HttpClient to process outgoing HTTP requests to external APIs.

**Common uses:** Adding headers (tokens), logging external calls, retry logic

**Example:**

```csharp
public class AuthTokenHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        // Add token to outgoing request
        request.Headers.Authorization = 
            new AuthenticationHeaderValue("Bearer", "your-token");

        return await base.SendAsync(request, cancellationToken);
    }
}

// Registration
builder.Services.AddTransient<AuthTokenHandler>();
builder.Services.AddHttpClient<IExternalApiClient, ExternalApiClient>()
    .AddHttpMessageHandler<AuthTokenHandler>();
```

### Comparison Table

| Feature | Middleware | Filters | DelegatingHandler |
|---------|-----------|---------|-------------------|
| **Works on** | Incoming HTTP requests | Controller/Action execution | Outgoing HTTP requests |
| **Scope** | Entire application pipeline | MVC pipeline only | HttpClient only |
| **Level** | Global only | Global / Controller / Action | Per HttpClient |
| **Runs when** | Before MVC/Endpoints | Inside MVC around actions | Before external API call |
| **Common use** | Logging, Auth, Exception handling | Validation, Authorization | Add headers, Retry logic |

---

## Q. What are Cross-Cutting Concerns and how do you handle them in ASP. NET Core?

**A.**

Cross-cutting concerns are common functionalities that are required across multiple parts of an application but are not part of the core business logic. These concerns affect many layers such as controllers, services, or the entire request pipeline.

### Examples

* Logging
* Authentication & Authorization
* Exception handling
* Caching
* Validation
* Performance monitoring

### How to handle them in ASP. NET Core?

| Concern | Recommended Approach |
|---------|---------------------|
| Logging | Middleware / DelegatingHandler (for external calls) |
| Global Exception Handling | Middleware |
| Authentication / Authorization | Middleware / Authorization Filters |
| Request/Response modification | Middleware |
| Action-specific validation or logic | Filters |
| External API logging / headers | DelegatingHandler |

---

## Q. How do you control Middleware execution order in ASP. NET Core?

**A.**

Middleware execution order is controlled by the order in which they are added in the request pipeline inside Program.cs (or Startup.cs).

* Middleware executes in the same order for the incoming request.
* Middleware executes in reverse order for the outgoing response.

### Example

```csharp
app.UseMiddleware<FirstMiddleware>();
app.UseMiddleware<SecondMiddleware>();
app.UseMiddleware<ThirdMiddleware>();
```

### Execution Flow

**Request:**
* First → Second → Third → Controller

**Response:**
* Controller → Third → Second → First

### Important Note

Order matters for built-in middleware too:

```csharp
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

Correct order ensures authentication runs before authorization.

---

## Q. Can Middleware short-circuit the request? When would you do that?

**A.**

Yes, middleware can short-circuit the request by not calling `next()` in the pipeline.

When `next()` is not invoked, the request stops there and does not proceed to the next middleware or controller.

### Example

```csharp
app.Use(async (context, next) =>
{
    if (!context.Request.Headers.ContainsKey("X-Api-Key"))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
        return; // Short-circuit: next() is NOT called
    }

    await next(); // Continue pipeline
});
```

### When do you short-circuit?

You short-circuit when the request should not continue further, such as:

| Scenario | Reason |
|----------|--------|
| Authentication/Authorization failure | Block unauthorized access |
| Invalid request | Return validation error early |
| Rate limiting | Stop excessive requests |
| Maintenance mode | Return service unavailable |
| Caching | Return cached response without hitting controller |

Yes, middleware can short-circuit a request by not calling `next()` , and it's used when you need to stop further processing, such as for authentication failure, validation errors, caching, or rate limiting.

---

## Q. How do you reload configuration without restarting the app in ASP. NET Core?

**A.**

ASP. NET Core supports automatic configuration reload when configuration files change by using the `reloadOnChange: true` option and accessing values through `IOptionsSnapshot` or `IOptionsMonitor` .

### 1. Enable reload on change

By default, appsettings.json is loaded with reload enabled:

```csharp
builder.Configuration.AddJsonFile(
    "appsettings.json",
    optional: false,
    reloadOnChange: true);
```

When the file changes, configuration is reloaded automatically.

### 2. Access updated values

**Option A: IOptionsSnapshot (Scoped – per request)**

```csharp
public class MyController : ControllerBase
{
    private readonly MySettings _settings;

    public MyController(IOptionsSnapshot<MySettings> options)
    {
        _settings = options.Value;
    }
}
```

Gets latest values on each request.

**Option B: IOptionsMonitor (Singleton-friendly)**

```csharp
public class MyService
{
    private readonly IOptionsMonitor<MySettings> _options;

    public MyService(IOptionsMonitor<MySettings> options)
    {
        _options = options;
    }

    public void Print()
    {
        var value = _options.CurrentValue;
    }
}
```

* Works with Singleton services.
* Always provides the latest configuration.

### 3. Bind configuration

```csharp
builder.Services.Configure<MySettings>(
    builder.Configuration.GetSection("MySettings"));
```

Configuration can be reloaded without restarting by enabling `reloadOnChange` and accessing values using `IOptionsSnapshot` (per request) or `IOptionsMonitor` (for real-time updates in singleton services).

---

## Q. How do you implement Event-Driven Architecture in . NET using Kafka? (Short)

**A.**

In Kafka-based event-driven architecture:

* A service (Producer) publishes events to a Kafka topic
* Other services (Consumers) subscribe to the topic and process events asynchronously
* In . NET, we use `Confluent.Kafka` package

### Producer (Publish Event)

```csharp
var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
using var producer = new ProducerBuilder<Null, string>(config).Build();

await producer.ProduceAsync("order-created",
    new Message<Null, string> { Value = JsonSerializer.Serialize(orderEvent) });
```

### Consumer (Receive Event)

```csharp
var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "order-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("order-created");

while (true)
{
    var result = consumer.Consume();
    var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(result.Message.Value);
}
```

### Flow

Producer → Kafka Topic → Consumer

In . NET, Kafka is implemented using `Confluent.Kafka` where producers publish events to a topic and consumers (often hosted services) process them asynchronously for loose coupling and scalability.

---

## Q. Why do we use Event-Driven Architecture instead of direct API-to-API communication?

**A.**

Event-driven architecture allows services to communicate asynchronously through events, making the system more flexible, scalable, and reliable compared to direct synchronous API calls.

### Main Points

* **Loose Coupling** – Services don't depend on each other directly
* **Asynchronous Processing** – No waiting for immediate responses
* **Better Performance** – Faster user response; background processing
* **Scalability** – Multiple consumers can handle increased load
* **Fault Tolerance** – Events stay in the queue if a service is down
* **Easy to Extend** – New consumers can be added without changing the publisher
* **Failure Isolation** – One service failure doesn't break the entire flow

### Comparison Table

| Aspect | Direct API Communication | Event-Driven Architecture |
|--------|-------------------------|---------------------------|
| **Coupling** | Tight – Service A must know Service B | Loose – Services communicate via events |
| **Communication** | Synchronous – Waits for response | Asynchronous – Fire and forget |
| **Availability** | If Service B is down, call fails | Events queued until consumer is ready |
| **Scalability** | Limited by single service capacity | Multiple consumers can process events |
| **Performance** | Slower – Waits for all services | Faster – Returns immediately |
| **Failure Handling** | One failure breaks entire flow | Isolated – Other services continue working |

### Example: Order Processing

**Direct API (Synchronous):**

```csharp
// User waits for all services to complete
var order = await CreateOrder(request);
await ProcessPayment(order);      // Waits
await UpdateInventory(order);     // Waits
await SendNotification(order);    // Waits
// If any service fails → entire request fails
```

**Event-Driven (Asynchronous):**

```csharp
// Create order and publish event
var order = await CreateOrder(request);
await PublishEvent("order-created", order);
// Returns immediately - consumers process independently
```

### When to Use

**Use Event-Driven When:**
* Services can work independently
* Long-running background tasks
* High scalability needed
* Fault tolerance is critical

**Use Direct API When:**
* Need immediate response/confirmation
* Simple, quick operations
* Strong consistency required

---

## Q. What is Semaphore / Lock and why do we use it?

**A.**

A lock mechanism is used to control access to shared resources so that multiple threads do not modify data at the same time, preventing race conditions and ensuring thread safety.

### Why we use it (Main Points)

- **Prevent race conditions** – Stop multiple threads from accessing data simultaneously
- **Ensure data consistency** – Protect shared data from corruption
- **Allow safe multi-threading** – Enable concurrent execution safely
- **Control how many threads can access a resource** – Limit concurrent access

### Common Types of Locks in C#

#### 1. lock (Monitor)

**Definition:** Allows only one thread to access a code block at a time.

**Points:**
- Simple and most commonly used
- Works only for synchronous code
- Best for protecting critical sections

**Example:**

```csharp
private static readonly object _lock = new object();
private static int _counter = 0;

public void IncrementCounter()
{
    lock (_lock)
    {
        // Only one thread can execute this at a time
        _counter++;
    }
}
```

---

#### 2. Mutex

**Definition:** Similar to lock but can work across multiple processes.

**Points:**
- Heavier than lock
- Used for cross-process synchronization
- Ensures only one process/thread can access

**Example:**

```csharp
using var mutex = new Mutex(false, "GlobalMutexName");

mutex.WaitOne(); // Wait for access
try
{
    // Critical section - only one process can enter
    Console.WriteLine("Working...");
}
finally
{
    mutex.ReleaseMutex();
}
```

---

#### 3. Semaphore

**Definition:** Allows a limited number of threads to access a resource simultaneously.

**Points:**
- Controls concurrent access (e.g., allow 3 threads at a time)
- Useful for resource pools (DB connections, API limits)
- Can work across processes

**Example:**

```csharp
// Allow max 3 threads to access at once
private static Semaphore _semaphore = new Semaphore(3, 3);

public void ProcessRequest()
{
    _semaphore.WaitOne(); // Wait if 3 threads already inside
    try
    {
        // Max 3 threads can execute this simultaneously
        Console.WriteLine("Processing...");
    }
    finally
    {
        _semaphore.Release(); // Allow next thread
    }
}
```

---

#### 4. SemaphoreSlim

**Definition:** Lightweight version of Semaphore for in-process and async scenarios.

**Points:**
- Faster than Semaphore
- Supports async/await
- Recommended for modern async code

**Example:**

```csharp
private static SemaphoreSlim _semaphore = new SemaphoreSlim(5, 5);

public async Task ProcessAsync()
{
    await _semaphore.WaitAsync(); // Async wait
    try
    {
        // Max 5 threads can execute this
        await Task.Delay(1000);
    }
    finally
    {
        _semaphore.Release();
    }
}
```

---

#### 5. ReaderWriterLockSlim

**Definition:** Allows multiple readers but only one writer.

**Points:**
- Improves performance when reads are frequent
- Ensures exclusive write access
- Best for read-heavy scenarios

**Example:**

```csharp
private static ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();
private static List<int> _data = new List<int>();

public int ReadData()
{
    _rwLock.EnterReadLock(); // Multiple readers allowed
    try
    {
        return _data.Count;
    }
    finally
    {
        _rwLock.ExitReadLock();
    }
}

public void WriteData(int value)
{
    _rwLock.EnterWriteLock(); // Only one writer allowed
    try
    {
        _data.Add(value);
    }
    finally
    {
        _rwLock.ExitWriteLock();
    }
}
```

---

### Quick Comparison

| Lock Type | Threads Allowed | Async Support | Cross Process | Performance |
|-----------|----------------|---------------|---------------|-------------|
| **lock** | 1 | No | No | Fast |
| **Mutex** | 1 | No | Yes | Slow |
| **Semaphore** | Limited | No | Yes | Medium |
| **SemaphoreSlim** | Limited | Yes | No | Fast |
| **ReaderWriterLockSlim** | Many readers / 1 writer | No | No | Fast for read-heavy |

### When to Use Which Lock

| Use Case | Recommended Lock |
|----------|-----------------|
| Simple critical section (sync) | `lock` |
| Async critical section | `SemaphoreSlim(1, 1)` |
| Limit concurrent API calls | `SemaphoreSlim` |
| Cross-process synchronization | `Mutex` or `Semaphore` |
| Read-heavy, write-light | `ReaderWriterLockSlim` |
| Database connection pool | `SemaphoreSlim` |

### Summary

Locks control thread access to shared resources to ensure thread safety. **Semaphore** allows limited concurrent access (e.g., 5 threads), while **lock** allows only one thread at a time. Use **SemaphoreSlim** for async scenarios and **lock** for simple synchronous cases.

---

## Q. What is the difference between throw and throw ex in C#?

**A.**

Both are used to rethrow exceptions, but they differ in how they preserve the original error details.

### Main Differences

#### 1. throw

- Rethrows the original exception
- Preserves the original stack trace
- Helps identify where the exception actually occurred
- **Recommended approach**

**Example:**

```csharp
catch (Exception ex)
{
    // logging
    throw;
}
```

#### 2. throw ex

- Throws the exception as a new exception
- Resets the stack trace
- Original error location is lost
- Makes debugging difficult

**Example:**

```csharp
catch (Exception ex)
{
    throw ex;
}
```

### Quick Comparison

| Feature | throw | throw ex |
|---------|-------|----------|
| Preserves original stack trace | Yes | No |
| Shows actual error location | Yes | No |
| Recommended | Yes | No |

### Summary

Use `throw` to rethrow exceptions because it preserves the original stack trace, while `throw ex` resets it and makes debugging harder.

---

## Q. If a catch block throws an exception, will the finally block execute? Can we have multiple catch blocks?

**A.**

In C#, `finally` always executes (except in rare cases like process crash). Multiple catch blocks are allowed, but only one matching catch runs.

### 1) If catch throws an exception, will finally execute?

**Answer: Yes.**

Even if the catch block throws a new exception, the finally block will still execute.

**Flow:**

```
try → catch → (new exception thrown) → finally → exception propagates
```

**Example:**

```csharp
try
{
    throw new Exception("Original error");
}
catch (Exception ex)
{
    Console.WriteLine("Catch block");
    throw new Exception("New error"); // Throw new exception
}
finally
{
    Console.WriteLine("Finally executes!"); // Still runs
}

// Output:
// Catch block
// Finally executes!
// (New exception propagates)
```

---

### 2) Can we have multiple catch blocks?

**Answer: Yes.**

You can define multiple catch blocks to handle different exception types.

**Example:**

```csharp
try
{
    // code
}
catch (NullReferenceException ex)
{
    // Handle null reference
}
catch (DivideByZeroException ex)
{
    // Handle divide by zero
}
catch (Exception ex)
{
    // Handle all other exceptions
}
finally
{
    // Always executes
}
```

---

### 3) If one catch executes and throws an exception, will other catch blocks execute?

**Answer: No.**

- Only the first matching catch block executes
- If that catch throws another exception:
  - Other catch blocks in the same try will **NOT** run
  - The new exception goes to the outer try-catch (if any)

**Example:**

```csharp
try
{
    throw new NullReferenceException();
}
catch (NullReferenceException ex)
{
    Console.WriteLine("First catch");
    throw new Exception("New error"); // Throws new exception
}
catch (Exception ex)
{
    Console.WriteLine("Second catch"); // Will NOT execute
}
finally
{
    Console.WriteLine("Finally"); // Still executes
}

// Output:
// First catch
// Finally
// (New exception propagates)
```

---

## Q. What is Microservices? Have you experienced Microservices architecture?

**A.**

Microservices is a software architecture style where an application is decomposed into small, autonomous services. Each service owns a single business capability, runs in its own process, and communicates with other services over lightweight protocols (HTTP/REST, gRPC, messaging).

Yes — experience with Microservices typically includes defining bounded contexts, implementing service-to-service communication, using API Gateway patterns, observability (metrics, tracing, logging), and CI/CD pipelines for independent deployment. In many Microsoft-centered projects, common building blocks include ASP.NET Core Web APIs, Kubernetes, Azure Service Bus or Kafka, and health checks/sidecars for observability.

### Main Points

* Service per business capability (Database per service)
* Independent deployability and scaling
* Lightweight communication (REST/gRPC/messaging)
* Observability, resilience, and automation are critical

---

## Q. What is Saga in Microservices? Explain Choreography and Orchestration.

**A.**

A Saga is a pattern for managing distributed transactions across multiple microservices. Instead of a single ACID transaction, a Saga sequences a set of local transactions across services and defines compensating actions to undo work if a later step fails.

### Types

1. **Choreography**
   - Each service publishes events and other services react to them.
   - No central coordinator; the flow emerges from event handlers.
   - Pros: Simple, decentralized. Cons: Harder to observe and reason about complex flows.

   Example flow (E-commerce):
   - Order Service publishes OrderCreated
   - Payment Service listens → processes payment → publishes PaymentSucceeded or PaymentFailed
   - Inventory Service listens to PaymentSucceeded → reserves stock

2. **Orchestration**
   - A central orchestrator (Saga orchestrator) directs the workflow by calling services or sending commands/events.
   - Pros: Easier to visualize and control. Cons: Single place of coordination (but can be made distributed/highly available).

   Example flow (Orchestrator):
   - Orchestrator receives CreateOrder
   - Orchestrator → Order Service (create order)
   - Orchestrator → Payment Service (process payment)
   - Orchestrator → Inventory Service (reserve stock)
   - If any step fails, orchestrator triggers compensating actions (refund, cancel order)

---

## Q. How do we manage databases in distributed systems?

**A.**

Managing data in distributed systems follows patterns that favor decoupling, scalability, and eventual consistency.

### Key Practices

1. **Database per Service (Data Ownership)**
   - Each service owns its schema and persistence. Other services interact via APIs/events rather than direct DB access.

2. **Avoid Distributed ACID Transactions**
   - Prefer Sagas (Choreography/Orchestration) and local transactions with compensating actions rather than 2PC.

3. **Eventual Consistency**
   - Accept that data across services may be eventually consistent; use events to propagate changes.

4. **Outbox Pattern (Reliable Event Publishing)**
   - Persist the state change and the corresponding event in the same local transaction (an outbox table), then reliably publish events from the outbox.

5. **Data Replication / Read Models (CQRS)**
   - Use read-models or materialized views built from events for complex queries and reports.

6. **Scaling and Sharding**
   - Partition data by tenant, user, or region. Use cloud-managed DBs or sharded architectures for scale.

### Example Summary

Use Database-per-Service, Sagas for cross-service consistency, Outbox to avoid lost events, and CQRS for efficient querying and reporting.

---

## Q. What is Dependency Injection? Explain all three lifetimes with a good example.

**A.**

Dependency Injection (DI) is a design pattern where dependencies are provided to a class rather than created inside it. ASP.NET Core has a built-in DI container supporting three primary lifetimes: Transient, Scoped, and Singleton.

### Lifetimes

1. **Transient**
   - A new instance is created every time the service is requested.
   - Use for lightweight, stateless services.

2. **Scoped**
   - One instance per scope (in web apps: per HTTP request).
   - Use for per-request operations like DbContext.

3. **Singleton**
   - A single instance for the application lifetime.
   - Use for thread-safe, shared services (caching, configuration).

### Example

```csharp
public interface ICounter { int Value { get; } }

public class TransientCounter : ICounter { public int Value { get; } = new Random().Next(); }
public class ScopedCounter : ICounter { public int Value { get; } = new Random().Next(); }
public class SingletonCounter : ICounter { public int Value { get; } = new Random().Next(); }

// Registration
builder.Services.AddTransient<ICounter, TransientCounter>();
builder.Services.AddScoped<ICounter, ScopedCounter>();
builder.Services.AddSingleton<ICounter, SingletonCounter>();

// Controller showing differences
[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    public TestController(
        ICounter transient1,
        ICounter transient2,
        ICounter scoped1,
        ICounter scoped2,
        ICounter singleton1,
        ICounter singleton2)
    {
        // Compare values: transient1 != transient2, scoped1 == scoped2 within same request, singleton1 == singleton2 across requests
    }
}
```

---

## Q. What is Middleware? Explain in detail and describe custom Middleware.

**A.**

Middleware are components assembled into a pipeline to handle HTTP requests and responses. Each middleware can inspect, modify, or short-circuit the pipeline.

### Responsibilities

* Authentication/Authorization
* Logging and diagnostics
* Exception handling
* Caching
* Compression
* Rate limiting

### Custom Middleware Example

```csharp
public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _limit;
    private static readonly ConcurrentDictionary<string, (int Count, DateTime Window)> _store = new();

    public RateLimitMiddleware(RequestDelegate next, int limit)
    {
        _next = next;
        _limit = limit;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var key = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var now = DateTime.UtcNow;
        var windowStart = now.Date.AddMinutes(now.Minute);

        var entry = _store.GetOrAdd(key, _ => (0, windowStart));
        if (entry.Window < windowStart) entry = (0, windowStart);

        if (entry.Count >= _limit)
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync("Rate limit exceeded");
            return;
        }

        _store[key] = (entry.Count + 1, entry.Window);
        await _next(context);
    }
}

// Registration
app.UseMiddleware<RateLimitMiddleware>(5); // limit to 5 calls per window (example)
```

---

## Q. How can we set rate limiting in custom Middleware to restrict to 5 (or any number of) calls?

**A.**

You can implement a simple in-memory sliding window or fixed-window counter per client IP or API key. Production systems should use distributed stores (Redis) to work across instances.

### Simple fixed-window example (per IP, 5 calls)

```csharp
// See RateLimitMiddleware above; register with limit=5
app.UseMiddleware<RateLimitMiddleware>(5);
```

### Production suggestions

* Use Redis for a distributed counter
* Use token-bucket or leaky-bucket algorithms for smoother limits
* Consider existing middleware or cloud API gateway features (Azure API Management, AWS API Gateway)

---

## Q. Design a workflow for a Microservices-based system.

**A.**

Example workflow for an Order processing system with API Gateway and event-driven communication:

Client → API Gateway → Order Service → Message Broker (Kafka/RabbitMQ) → Payment Service / Inventory Service

### Sequence

1. Client calls API Gateway
2. Gateway routes to Order Service
3. Order Service creates order (local DB) and publishes OrderCreated event
4. Payment Service subscribes to OrderCreated → processes payment → publishes PaymentSuccess/PaymentFailed
5. Inventory Service subscribes to PaymentSuccess → reserves stock → publishes InventoryReserved
6. If any step fails, Saga triggers compensating actions (refund, cancel order)

### Data stores

* Order Service DB
* Payment DB
* Inventory DB

---

## Q. How do event-based services work (e.g., Kafka, RabbitMQ)?

**A.**

Event-based services use a messaging broker to decouple producers from consumers using publish–subscribe semantics.

### Basic flow

1. Producer publishes a message/event to a topic/queue
2. Broker persists the message (durability depending on config)
3. Consumers subscribe and receive messages
4. Consumers process messages and optionally produce new events

### Differences

* **Kafka**: Distributed log, high-throughput, partitioned topics, consumer groups for horizontal scaling, retention-based storage
* **RabbitMQ**: Broker with flexible routing (exchanges/queues), lower-latency patterns, good for complex routing scenarios

---

## Q. Given an E-commerce scenario, explain how Kafka messaging fits for Order, Inventory, and Payment services.

**A.**

Kafka fits well for high-throughput, durable event streaming in e-commerce workflows.

### Flow

1. Order Service publishes OrderCreated to topic `orders`.
2. Payment Service subscribes to `orders`, processes payment, publishes `payment-succeeded` or `payment-failed`.
3. Inventory Service subscribes to `payment-succeeded` and reserves stock, then publishes `inventory-reserved`.
4. Order Service updates order status based on downstream events.

### Benefits

* Asynchronous processing and resilience
* Replayable events for recovery
* Scalability via partitions and consumer groups
* Decoupling of services for independent deployments

### Saga handling

* Implement compensating actions for failures (refunds, order cancellation)

---

## Q. How can we optimize Kafka? Explain partitions, offsets, consumer types, etc.

**A.**

Key Kafka concepts and optimizations:

* **Partitions**: Topics split into partitions for parallelism. More partitions → higher throughput but more resource usage.
* **Offsets**: Each message in a partition has an offset; consumers track offsets to maintain progress.
* **Consumer Groups**: Consumers in a group share partitions; each partition is consumed by one consumer within the group for load balancing.
* **Replication Factor**: Number of copies of partition data across brokers for fault tolerance.
* **Retention & Compaction**: Configure retention time and compaction for storage behavior.
* **Batching & Compression**: Use producer batching and compression (gzip/snappy/lz4) for throughput.
* **Idempotency**: Implement idempotent consumers or transactional producers to avoid duplicates.
* **Partition Keying**: Choose partition keys carefully to balance load (hot partitions cause imbalance).

---


## Q. Write a program to create a custom Dictionary (do not use the built-in Dictionary).

**A.**

Below is a straightforward, interview-friendly separate-chaining hash table implementation using an array of buckets (each bucket is a List). The example includes common members (Add, TryGetValue/Get, Remove, ContainsKey, Count). It is intentionally simple: no automatic resizing and not thread-safe — suitable for learning and interview/demo scenarios.

```csharp
public class CustomDictionary<TKey, TValue>
{
    // Array of buckets where each bucket is a list of key/value pairs (separate chaining)
    private readonly List<KeyValuePair<TKey, TValue>>[] _buckets; // bucket storage

    // Number of key/value pairs currently stored
    private int _count;

    // Total number of buckets (array length)
    private readonly int _size;

    // Constructor: allow caller to suggest initial bucket count (default 16)
    public CustomDictionary(int size = 16)
    {
        // Ensure size is at least 1
        _size = Math.Max(1, size);

        // Allocate the buckets array; individual lists are created lazily
        _buckets = new List<KeyValuePair<TKey, TValue>>[_size];
    }

    // Compute the bucket index for a key
    private int GetIndex(TKey key)
    {
        // GetHashCode() may throw for badly implemented keys; handle null keys safely
        var hash = key?.GetHashCode() ?? 0;              // null-coalescing: if key is null, use 0
        return Math.Abs(hash) % _size;                  // Normalize hash to [0, _size-1]
    }

    // Add a new key/value pair. Throws if key already exists (like Dictionary.Add)
    public void Add(TKey key, TValue value)
    {
        if (key is null) throw new ArgumentNullException(nameof(key)); // Guard clause for null keys

        int index = GetIndex(key); // Find appropriate bucket for this key

        // Get existing bucket or create it if missing (C# 8 null-coalescing assignment)
        var bucket = _buckets[index] ??= new List<KeyValuePair<TKey, TValue>>();

        // Check for duplicate key in the bucket
        for (int i = 0; i < bucket.Count; i++)
        {
            // Use EqualityComparer to support custom key equality implementations
            if (EqualityComparer<TKey>.Default.Equals(bucket[i].Key, key))
                throw new ArgumentException("An item with the same key already exists.", nameof(key));
        }

        // Add the key/value pair to the bucket and increment count
        bucket.Add(new KeyValuePair<TKey, TValue>(key, value));
        _count++;
    }

    // Try to get a value by key; returns true if found, false otherwise
    public bool TryGetValue(TKey key, out TValue value)
    {
        if (key is null) { value = default!; return false; } // Null guard: treat as not found

        int index = GetIndex(key);           // Compute bucket index
        var bucket = _buckets[index];        // Get bucket (may be null if never created)
        if (bucket != null)
        {
            // Iterate entries in bucket to find matching key
            foreach (var kvp in bucket)
            {
                if (EqualityComparer<TKey>.Default.Equals(kvp.Key, key))
                {
                    value = kvp.Value;        // On match set out value
                    return true;             // Found
                }
            }
        }

        value = default!;                    // Not found: set default and return false
        return false;
    }

    // Get value or throw if key not present (helper similar to Dictionary indexer get)
    public TValue Get(TKey key)
    {
        if (TryGetValue(key, out var value))
            return value;                    // Return found value
        throw new KeyNotFoundException($"Key '{key}' not found."); // Not found: throw
    }

    // Remove an entry by key; return true if removed
    public bool Remove(TKey key)
    {
        if (key is null) return false;      // Null guard

        int index = GetIndex(key);           // Find bucket index
        var bucket = _buckets[index];        // Get bucket
        if (bucket != null)
        {
            // Locate the item in the bucket and remove it
            for (int i = 0; i < bucket.Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(bucket[i].Key, key))
                {
                    bucket.RemoveAt(i);      // Remove item from bucket
                    _count--;               // Decrement total count
                    return true;            // Indicate successful removal
                }
            }
        }
        return false;                        // Not found
    }

    // Convenience: check whether a key exists
    public bool ContainsKey(TKey key) => TryGetValue(key, out _);

    // Expose number of elements
    public int Count => _count;
}

// Usage
var dict = new CustomDictionary<string, int>(size: 5); // Create with 5 buckets
dict.Add("one", 1);                                  // Add key/value
if (dict.TryGetValue("one", out var v)) Console.WriteLine(v); // Read and print value
```

**Limitations & Notes**

- Fixed-size buckets (no automatic resizing) — in production add resizing and rehashing.
- Not thread-safe — add synchronization for multi-threaded access or use concurrent collections.
- Uses `EqualityComparer<TKey>.Default` for robust key comparisons.
- `Add` throws on duplicate keys (consistent with `Dictionary<TKey,TValue>.Add`). You can change it to upsert if desired.

---

## Q. Create a custom Order Controller.

**A.**

Below is a minimal ASP.NET Core `OrderController` demonstrating common patterns: DTOs (records for request/response), validation, service abstraction, and publishing events (simplified).

```csharp
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponse>> CreateOrder(CreateOrderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var order = await _orderService.CreateOrderAsync(request);

        var response = new OrderResponse(order.Id, order.Status, order.TotalAmount);
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponse>> GetOrder(int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(new OrderResponse(order.Id, order.Status, order.TotalAmount));
    }
}

public record CreateOrderRequest(int CustomerId, List<OrderItemDto> Items, string ShippingAddress);
public record OrderItemDto(int ProductId, int Quantity);
public record OrderResponse(int Id, string Status, decimal TotalAmount);

public interface IOrderService
{
    Task<Order> CreateOrderAsync(CreateOrderRequest request);
    Task<Order?> GetByIdAsync(int id);
}

// Simplified domain/order model
public class Order
{
    public int Id { get; set; }
    public string Status { get; set; } = "Pending";
    public decimal TotalAmount { get; set; }
}

// Example service implementation sketch (pseudocode)
public class OrderService : IOrderService
{
    public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
    {
        // 1. Validate items and prices
        // 2. Save order to Order DB
        // 3. Publish OrderCreated to message broker
        // 4. Return created order
        return new Order { Id = 1, Status = "Created", TotalAmount = 100 };
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        // Read from DB
        return new Order { Id = id, Status = "Created", TotalAmount = 100 };
    }
}

---

### Quick Summary

- `finally` always executes, even if catch throws an exception
- Multiple catch blocks are allowed
- Only one matching catch executes