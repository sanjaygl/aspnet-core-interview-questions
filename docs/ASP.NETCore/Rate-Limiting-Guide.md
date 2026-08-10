# Rate Limiting in ASP.NET Core – Protecting APIs from Abuse

## Introduction

Rate limiting is a critical technique to protect APIs from abuse, prevent resource exhaustion, and ensure fair usage across all clients. Without rate limiting, a single user or malicious actor can overwhelm your API with excessive requests, causing performance degradation or complete service outages. Starting with .NET 7, ASP.NET Core includes built-in rate limiting middleware that provides multiple algorithms to control request flow. This guide explains practical rate limiting strategies, implementation approaches, and real-world scenarios for protecting your APIs.

---

## What is Rate Limiting?

**Rate Limiting** is the practice of controlling the number of requests a client can make to an API within a specific time window. It prevents any single client from consuming too many resources, ensuring the API remains available and responsive for all users.

**Example:**
- Allow maximum 100 requests per minute per user
- Block additional requests until the time window resets
- Return HTTP 429 (Too Many Requests) when limit is exceeded

### Rate Limiting vs Throttling

While often used interchangeably, there's a subtle difference:

| Aspect | Rate Limiting | Throttling |
|--------|--------------|------------|
| **Action** | Rejects requests that exceed the limit | Slows down or queues requests |
| **Response** | Returns 429 immediately | Delays processing |
| **Use Case** | Hard limits (e.g., API quotas) | Managing traffic spikes |
| **User Experience** | Client must retry later | Request still processed, but slower |

**In Practice:**
- **Rate Limiting**: "You've made 1000 requests this hour. Wait 15 minutes before trying again."
- **Throttling**: "Too many requests detected. Your request is queued and will process in 5 seconds."

Most implementations use the term "rate limiting" to refer to both concepts.

---

## Why Rate Limiting is Important

### 1. Prevent Abuse and Attacks

**DDoS Protection:**
```csharp
// Without rate limiting: Attacker sends 10,000 requests/second
// Result: Server crashes, legitimate users can't access API

// With rate limiting: After 100 requests/minute, attacker is blocked
// Result: API remains available for legitimate users
```

**Brute Force Prevention:**
```csharp
// Login endpoint without rate limiting
POST /api/auth/login
{ "username": "admin", "password": "attempt1" }
{ "username": "admin", "password": "attempt2" }
// ... 10,000 password guesses in seconds

// With rate limiting: Only 5 login attempts per minute
// Result: Brute force attacks become impractical
```

### 2. Improve Stability and Performance

Rate limiting prevents resource exhaustion:
- CPU overload from processing too many requests
- Database connection pool exhaustion
- Memory pressure from concurrent operations
- Network bandwidth saturation

### 3. Ensure Fair Usage

Prevents one client from monopolizing resources:
```csharp
// Scenario: Public API with 1000 users
// Without rate limiting: One user makes 90% of all requests
// With rate limiting: Each user gets fair access (e.g., 1000 requests/hour)
```

### 4. Monetization and Tiered Access

Different rate limits for different subscription tiers:
```csharp
// Free tier: 100 requests/hour
// Pro tier: 1,000 requests/hour
// Enterprise tier: 10,000 requests/hour
```

---

## Built-in Rate Limiting in ASP.NET Core

### Why Built-in Support Was Added

Before .NET 7, developers relied on third-party libraries like AspNetCoreRateLimit. Microsoft added native support because:

1. **Common Requirement**: Almost all production APIs need rate limiting
2. **Security First**: Built-in security features reduce vulnerabilities
3. **Performance**: Native middleware is optimized for .NET runtime
4. **Consistency**: Standardized approach across ASP.NET Core apps
5. **Multiple Algorithms**: Supports different rate limiting strategies

**Package:**
```bash
# Built into ASP.NET Core 7.0+
# No additional package needed for basic rate limiting
# Available in: Microsoft.AspNetCore.RateLimiting namespace
```

---

## Common Rate Limiting Algorithms

### 1. Fixed Window

Allows a fixed number of requests within a time window.

**How It Works:**
```
Window: 1 minute
Limit: 10 requests

00:00 - 00:59 → 10 requests allowed
01:00 - 01:59 → Counter resets, 10 new requests allowed
```

**Visualization:**
```
Window 1 (00:00-01:00): ████████░░ (8/10 requests used)
Window 2 (01:00-02:00): ██░░░░░░░░ (2/10 requests used)
```

**Use Case:**
- Simple to understand and implement
- Good for predictable traffic patterns
- API quotas (e.g., "1000 requests per hour")

**Limitation:**
- Burst at window boundaries (20 requests in 2 seconds if timed right)

### 2. Sliding Window

More accurate than fixed window, prevents boundary bursts.

**How It Works:**
```
Window: 1 minute (rolling)
Limit: 10 requests

At 00:30: Counts requests from 23:30 to 00:30
At 00:45: Counts requests from 23:45 to 00:45
```

**Visualization:**
```
Time:     00:00  00:15  00:30  00:45  01:00
Requests:   3      2      4      1      2
Window:  [--------10 requests rolling-------]
```

**Use Case:**
- More accurate rate limiting
- Prevents burst exploitation at window boundaries
- Better for APIs requiring strict fairness

**Advantage over Fixed Window:**
- Smooths out request distribution
- No sudden reset allowing bursts

### 3. Token Bucket

Allows bursts while maintaining average rate over time.

**How It Works:**
```
Bucket capacity: 10 tokens
Refill rate: 2 tokens/second

- Client makes request → consumes 1 token
- Bucket refills at constant rate
- If bucket is empty, request is rejected
- Allows burst if tokens are available
```

**Visualization:**
```
Time:     0s    1s    2s    3s    4s    5s
Tokens:   10 →  8  → 10  →  8  →  6  →  8
Requests:  2     0     2     4     0
Status:   ✅    ✅    ✅    ✅    ✅
```

**Use Case:**
- APIs that tolerate occasional bursts
- Background job processing
- Balancing throughput with burst capacity

**Example Scenario:**
```csharp
// Image processing API
// Normal: 5 requests/second
// Burst: Up to 20 requests at once (if tokens available)
// Use case: Batch upload of 20 images, then idle for a while
```

### 4. Concurrency Limiter

Limits the number of concurrent requests, not request rate.

**How It Works:**
```
Limit: 5 concurrent requests

Request 1 arrives → Processing (count: 1)
Request 2 arrives → Processing (count: 2)
...
Request 5 arrives → Processing (count: 5)
Request 6 arrives → Rejected (429) until one completes
Request 1 completes → count: 4
Request 6 can now be processed
```

**Visualization:**
```
Time:      0s    1s    2s    3s    4s
Active:    ||||| ||||| ||||| |||   ||
Count:     5     5     5     3     2
New req:   ❌    ❌    ❌    ✅    ✅
```

**Use Case:**
- Expensive operations (video transcoding, PDF generation)
- Database connection limits
- External API calls with concurrency restrictions
- Resource-intensive endpoints

**Example:**
```csharp
// PDF Generation API
// Limit: 3 concurrent PDF generations
// Each generation takes 5-10 seconds
// Prevents server from being overwhelmed
```

---

## Configuring Rate Limiting

### Step 1: Add Rate Limiting Services

```csharp
// Program.cs
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add rate limiting services
builder.Services.AddRateLimiter(options =>
{
    // Configure global rejection response
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    
    // Add a fixed window policy
    options.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 10;               // 10 requests
        options.Window = TimeSpan.FromMinutes(1); // per minute
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;                 // Queue up to 2 requests
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Enable rate limiting middleware (MUST be before MapControllers)
app.UseRateLimiter();

app.MapControllers();
app.Run();
```

### Step 2: Configure Multiple Policies

```csharp
builder.Services.AddRateLimiter(options =>
{
    // Policy 1: Fixed Window for general API endpoints
    options.AddFixedWindowLimiter("api", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromMinutes(1);
    });
    
    // Policy 2: Strict limits for authentication
    options.AddFixedWindowLimiter("auth", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromMinutes(1);
    });
    
    // Policy 3: Sliding window for premium users
    options.AddSlidingWindowLimiter("premium", opt =>
    {
        opt.PermitLimit = 1000;
        opt.Window = TimeSpan.FromHours(1);
        opt.SegmentsPerWindow = 6; // Divides window into 6 segments (10 min each)
    });
    
    // Policy 4: Token bucket for burst-tolerant endpoints
    options.AddTokenBucketLimiter("burst", opt =>
    {
        opt.TokenLimit = 20;            // Bucket holds 20 tokens
        opt.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
        opt.TokensPerPeriod = 5;        // Add 5 tokens every 10 seconds
        opt.AutoReplenishment = true;
    });
    
    // Policy 5: Concurrency limiter for expensive operations
    options.AddConcurrencyLimiter("expensive", opt =>
    {
        opt.PermitLimit = 3;            // Max 3 concurrent requests
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 10;            // Queue up to 10 waiting requests
    });
});
```

---

## Applying Rate Limiting Policies

### 1. Endpoint-Specific Rate Limiting

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // Apply "api" policy to this endpoint
    [HttpGet]
    [EnableRateLimiting("api")]
    public IActionResult GetProducts()
    {
        return Ok(new[] { "Product1", "Product2" });
    }
    
    // Apply strict "auth" policy to login
    [HttpPost("login")]
    [EnableRateLimiting("auth")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Login logic
        return Ok(new { Token = "jwt-token" });
    }
    
    // Apply concurrency limiter to expensive operation
    [HttpPost("export")]
    [EnableRateLimiting("expensive")]
    public async Task<IActionResult> ExportData()
    {
        // Expensive PDF generation
        await Task.Delay(5000); // Simulates long operation
        return Ok(new { FileUrl = "https://..." });
    }
}
```

### 2. Controller-Level Rate Limiting

```csharp
// Apply rate limiting to entire controller
[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("api")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders() => Ok("Orders");
    
    [HttpGet("{id}")]
    public IActionResult GetOrder(int id) => Ok($"Order {id}");
    
    // Disable rate limiting for specific endpoint
    [HttpGet("status")]
    [DisableRateLimiting]
    public IActionResult GetStatus() => Ok("Healthy");
}
```

### 3. Global Rate Limiting

```csharp
builder.Services.AddRateLimiter(options =>
{
    // Global policy applied to all endpoints
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        // Partition by IP address
        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        
        return RateLimitPartition.GetFixedWindowLimiter(ipAddress, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 100,
            Window = TimeSpan.FromMinutes(1)
        });
    });
});
```

### 4. Per-User Rate Limiting

```csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("per-user", context =>
    {
        // Get user ID from claims
        var userId = context.User.FindFirst("sub")?.Value ?? "anonymous";
        
        return RateLimitPartition.GetSlidingWindowLimiter(userId, _ => new SlidingWindowRateLimiterOptions
        {
            PermitLimit = 1000,
            Window = TimeSpan.FromHours(1),
            SegmentsPerWindow = 6
        });
    });
});

// Apply to authenticated endpoints
[HttpGet]
[Authorize]
[EnableRateLimiting("per-user")]
public IActionResult GetUserData()
{
    return Ok("User-specific data");
}
```

### 5. Per-API-Key Rate Limiting

```csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("per-api-key", context =>
    {
        // Get API key from header
        var apiKey = context.Request.Headers["X-API-Key"].ToString();
        
        if (string.IsNullOrEmpty(apiKey))
            apiKey = "anonymous";
        
        // Look up tier from database (simplified)
        var tier = GetApiKeyTier(apiKey); // Returns "free", "pro", "enterprise"
        
        return tier switch
        {
            "free" => RateLimitPartition.GetFixedWindowLimiter(apiKey, _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromHours(1)
            }),
            "pro" => RateLimitPartition.GetFixedWindowLimiter(apiKey, _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 1000,
                Window = TimeSpan.FromHours(1)
            }),
            "enterprise" => RateLimitPartition.GetFixedWindowLimiter(apiKey, _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10000,
                Window = TimeSpan.FromHours(1)
            }),
            _ => RateLimitPartition.GetFixedWindowLimiter("default", _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromHours(1)
            })
        };
    });
});

static string GetApiKeyTier(string apiKey)
{
    // Simplified lookup - in reality, query database
    return apiKey switch
    {
        "free-key-123" => "free",
        "pro-key-456" => "pro",
        "enterprise-key-789" => "enterprise",
        _ => "free"
    };
}
```

---

## Handling Rate Limit Responses

### Understanding HTTP 429 (Too Many Requests)

When rate limit is exceeded, the API returns:

```http
HTTP/1.1 429 Too Many Requests
Retry-After: 60
X-RateLimit-Limit: 100
X-RateLimit-Remaining: 0
X-RateLimit-Reset: 1706356800

{
  "error": "Rate limit exceeded",
  "message": "You have exceeded the maximum number of requests. Please try again in 60 seconds."
}
```

### Customizing Rate Limit Response

```csharp
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    
    // Customize rejection response
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        
        // Add Retry-After header
        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            context.HttpContext.Response.Headers.RetryAfter = retryAfter.TotalSeconds.ToString();
        }
        
        // Custom error response
        await context.HttpContext.Response.WriteAsJsonAsync(new
        {
            error = "Rate limit exceeded",
            message = "Too many requests. Please slow down.",
            retryAfter = retryAfter?.TotalSeconds ?? 60
        }, cancellationToken);
    };
});
```

### Adding Rate Limit Headers to All Responses

```csharp
// Custom middleware to add rate limit headers
app.Use(async (context, next) =>
{
    await next();
    
    // Add rate limit info headers (simplified example)
    context.Response.Headers.Add("X-RateLimit-Limit", "100");
    context.Response.Headers.Add("X-RateLimit-Remaining", "95");
    context.Response.Headers.Add("X-RateLimit-Reset", DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeSeconds().ToString());
});
```

---

## Rate Limiting with Authentication

### Scenario 1: Different Limits for Authenticated vs Anonymous Users

```csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("auth-aware", context =>
    {
        var isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;
        
        if (isAuthenticated)
        {
            // Authenticated users: 1000 requests/hour
            var userId = context.User.FindFirst("sub")?.Value ?? "unknown";
            return RateLimitPartition.GetFixedWindowLimiter(userId, _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 1000,
                Window = TimeSpan.FromHours(1)
            });
        }
        else
        {
            // Anonymous users: 100 requests/hour per IP
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromHours(1)
            });
        }
    });
});

[HttpGet]
[EnableRateLimiting("auth-aware")]
public IActionResult GetData()
{
    return Ok("Data with auth-aware rate limiting");
}
```

### Scenario 2: Role-Based Rate Limiting

```csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("role-based", context =>
    {
        var userId = context.User.FindFirst("sub")?.Value ?? "anonymous";
        var isAdmin = context.User.IsInRole("Admin");
        var isPremium = context.User.IsInRole("Premium");
        
        if (isAdmin)
        {
            // Admins: Unlimited (very high limit)
            return RateLimitPartition.GetFixedWindowLimiter($"admin-{userId}", _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100000,
                Window = TimeSpan.FromHours(1)
            });
        }
        else if (isPremium)
        {
            // Premium users: 5000 requests/hour
            return RateLimitPartition.GetFixedWindowLimiter($"premium-{userId}", _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5000,
                Window = TimeSpan.FromHours(1)
            });
        }
        else
        {
            // Regular users: 1000 requests/hour
            return RateLimitPartition.GetFixedWindowLimiter($"user-{userId}", _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 1000,
                Window = TimeSpan.FromHours(1)
            });
        }
    });
});
```

---

## Real-World Use Cases

### 1. Public API Protection

```csharp
// Scenario: Weather API consumed by thousands of websites
builder.Services.AddRateLimiter(options =>
{
    // Free tier: 100 requests/day per API key
    options.AddPolicy("public-api", context =>
    {
        var apiKey = context.Request.Headers["X-API-Key"].ToString();
        
        return RateLimitPartition.GetFixedWindowLimiter(apiKey, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 100,
            Window = TimeSpan.FromDays(1)
        });
    });
});

[HttpGet("weather/{city}")]
[EnableRateLimiting("public-api")]
public IActionResult GetWeather(string city)
{
    return Ok(new { City = city, Temperature = 22, Condition = "Sunny" });
}
```

### 2. Login Endpoint Brute Force Protection

```csharp
// Scenario: Prevent password guessing attacks
builder.Services.AddRateLimiter(options =>
{
    // Only 5 login attempts per 15 minutes per IP
    options.AddPolicy("login-protection", context =>
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        
        return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromMinutes(15),
            QueueLimit = 0 // No queuing for login attempts
        });
    });
});

[HttpPost("login")]
[EnableRateLimiting("login-protection")]
public IActionResult Login([FromBody] LoginRequest request)
{
    // Validate credentials
    if (IsValidCredentials(request.Username, request.Password))
        return Ok(new { Token = "jwt-token" });
    
    return Unauthorized(new { Error = "Invalid credentials" });
}
```

### 3. Expensive Operations Protection

```csharp
// Scenario: Video transcoding API
builder.Services.AddRateLimiter(options =>
{
    // Only 2 concurrent video transcoding operations per user
    options.AddPolicy("video-processing", context =>
    {
        var userId = context.User.FindFirst("sub")?.Value ?? "anonymous";
        
        return RateLimitPartition.GetConcurrencyLimiter(userId, _ => new ConcurrencyLimiterOptions
        {
            PermitLimit = 2,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 5 // Queue up to 5 jobs
        });
    });
});

[HttpPost("transcode")]
[Authorize]
[EnableRateLimiting("video-processing")]
public async Task<IActionResult> TranscodeVideo([FromBody] TranscodeRequest request)
{
    // Simulate expensive operation
    await Task.Delay(10000); // 10 seconds
    
    return Ok(new { JobId = Guid.NewGuid(), Status = "Processing" });
}
```

### 4. Webhook Delivery Rate Limiting

```csharp
// Scenario: Prevent overwhelming webhook consumers
builder.Services.AddRateLimiter(options =>
{
    // Token bucket allows bursts but maintains average rate
    options.AddPolicy("webhook", context =>
    {
        var webhookUrl = context.Request.Query["url"].ToString();
        
        return RateLimitPartition.GetTokenBucketLimiter(webhookUrl, _ => new TokenBucketRateLimiterOptions
        {
            TokenLimit = 50,         // Allow burst of 50 webhooks
            ReplenishmentPeriod = TimeSpan.FromMinutes(1),
            TokensPerPeriod = 10,    // Average 10 webhooks/minute
            AutoReplenishment = true
        });
    });
});
```

### 5. Multi-Tenant SaaS API

```csharp
// Scenario: Different rate limits per tenant/organization
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("tenant-based", context =>
    {
        var tenantId = context.User.FindFirst("tenant_id")?.Value ?? "default";
        
        // Fetch tenant plan from cache/database
        var plan = GetTenantPlan(tenantId); // Returns "starter", "business", "enterprise"
        
        return plan switch
        {
            "starter" => RateLimitPartition.GetFixedWindowLimiter($"tenant-{tenantId}", _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 1000,
                Window = TimeSpan.FromHours(1)
            }),
            "business" => RateLimitPartition.GetFixedWindowLimiter($"tenant-{tenantId}", _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10000,
                Window = TimeSpan.FromHours(1)
            }),
            "enterprise" => RateLimitPartition.GetFixedWindowLimiter($"tenant-{tenantId}", _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100000,
                Window = TimeSpan.FromHours(1)
            }),
            _ => RateLimitPartition.GetFixedWindowLimiter("default", _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromHours(1)
            })
        };
    });
});

static string GetTenantPlan(string tenantId)
{
    // Simplified - in reality, query from database or cache
    return "business";
}
```

---

## Common Mistakes

### ❌ Mistake 1: Over-Restrictive Limits

```csharp
// Bad: Too strict for normal usage
options.AddFixedWindowLimiter("api", opt =>
{
    opt.PermitLimit = 10;        // Only 10 requests
    opt.Window = TimeSpan.FromHours(1); // per hour!
});
```

**Why It's Wrong:**
- Legitimate users hit limits during normal usage
- Frustrates users and reduces adoption
- Forces users to cache aggressively or find workarounds

**✅ Better Approach:**
```csharp
// Start generous, monitor usage, adjust based on data
options.AddFixedWindowLimiter("api", opt =>
{
    opt.PermitLimit = 1000;      // Reasonable limit
    opt.Window = TimeSpan.FromHours(1);
});
```

### ❌ Mistake 2: Same Limits for All Endpoints

```csharp
// Bad: Same 100 req/min limit for everything
[EnableRateLimiting("api")]  // Applied to all endpoints
public class ApiController : ControllerBase
{
    [HttpGet("status")]          // Simple, cheap
    public IActionResult Status() => Ok("Healthy");
    
    [HttpPost("generate-report")] // Expensive, slow
    public IActionResult Report() => Ok("Processing...");
}
```

**Why It's Wrong:**
- Cheap endpoints don't need strict limits
- Expensive endpoints need stricter limits
- Different security requirements (login vs health check)

**✅ Better Approach:**
```csharp
public class ApiController : ControllerBase
{
    [HttpGet("status")]
    [DisableRateLimiting]  // Health checks shouldn't be limited
    public IActionResult Status() => Ok("Healthy");
    
    [HttpPost("generate-report")]
    [EnableRateLimiting("expensive")]  // Strict concurrency limit
    public IActionResult Report() => Ok("Processing...");
}
```

### ❌ Mistake 3: Not Returning Proper Response Headers

```csharp
// Bad: Generic 429 response without guidance
options.OnRejected = async (context, cancellationToken) =>
{
    context.HttpContext.Response.StatusCode = 429;
    await context.HttpContext.Response.WriteAsync("Too many requests");
};
```

**Why It's Wrong:**
- Client doesn't know when to retry
- No visibility into remaining quota
- Poor developer experience

**✅ Better Approach:**
```csharp
options.OnRejected = async (context, cancellationToken) =>
{
    context.HttpContext.Response.StatusCode = 429;
    
    // Add Retry-After header
    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
    {
        context.HttpContext.Response.Headers.RetryAfter = retryAfter.TotalSeconds.ToString();
    }
    
    // Informative response
    await context.HttpContext.Response.WriteAsJsonAsync(new
    {
        error = "Rate limit exceeded",
        message = "You have exceeded your request quota",
        retryAfter = retryAfter?.TotalSeconds ?? 60,
        documentation = "https://api.example.com/docs/rate-limits"
    }, cancellationToken);
};
```

### ❌ Mistake 4: Not Considering Distributed Scenarios

```csharp
// Bad: In-memory rate limiting in multi-server deployment
// Server 1: User makes 100 requests (allowed)
// Server 2: Same user makes 100 requests (allowed)
// Result: User gets 200 requests instead of 100
```

**Why It's Wrong:**
- Rate limits are per-server, not per-user
- Load balancer can route to different servers
- Ineffective in cloud/containerized environments

**✅ Better Approach:**
```csharp
// Use distributed cache (Redis) for rate limiting across servers
// Libraries: AspNetCoreRateLimit with Redis store
// Or implement custom distributed limiter with IDistributedCache
```

### ❌ Mistake 5: Forgetting to Place Middleware in Correct Order

```csharp
// Bad: Rate limiter after authentication
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();  // Too late!
app.MapControllers();
```

**Why It's Wrong:**
- Unauthenticated requests waste authentication processing
- Attackers can overwhelm auth middleware

**✅ Better Approach:**
```csharp
// Rate limiter early in pipeline (but after routing for policy selection)
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

### ❌ Mistake 6: No Monitoring or Alerting

```csharp
// Bad: Set rate limits and forget
// No logging of rate limit violations
// No alerts when limits are frequently hit
```

**Why It's Wrong:**
- Can't detect attacks or abuse patterns
- Don't know if limits are too strict or too lenient
- Miss opportunities to optimize API usage

**✅ Better Approach:**
```csharp
options.OnRejected = async (context, cancellationToken) =>
{
    // Log rate limit violations
    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogWarning("Rate limit exceeded for {IP} on {Path}", 
        context.HttpContext.Connection.RemoteIpAddress,
        context.HttpContext.Request.Path);
    
    // Send metrics to monitoring system (e.g., Application Insights, Prometheus)
    // _metrics.IncrementRateLimitViolations();
    
    context.HttpContext.Response.StatusCode = 429;
};
```

---

## Interview Questions

### 1. What is rate limiting and why is it needed?

Rate limiting controls the number of requests a client can make to an API within a specific time window. It's needed to prevent abuse (DDoS attacks, brute force), protect backend resources from exhaustion, ensure fair usage across all clients, and enable monetization through tiered access plans. Without rate limiting, a single malicious or misbehaving client can overwhelm the API and degrade service for everyone.

### 2. What's the difference between rate limiting and throttling?

**Rate limiting** rejects requests that exceed the limit by returning HTTP 429 immediately. **Throttling** slows down or queues requests instead of rejecting them. Rate limiting is used for hard quotas (e.g., "100 requests per hour"), while throttling manages traffic spikes by delaying processing. In practice, most APIs use "rate limiting" to refer to both concepts, though technically throttling is a softer approach.

### 3. Which HTTP status code is returned when rate limit is exceeded?

**HTTP 429 (Too Many Requests)** is the standard status code. The response should include a `Retry-After` header indicating when the client can retry, and optionally `X-RateLimit-*` headers showing limit, remaining quota, and reset time. Example: `429 Too Many Requests` with `Retry-After: 60` means "wait 60 seconds before retrying."

### 4. How do you implement rate limiting in ASP.NET Core?

Use the built-in `Microsoft.AspNetCore.RateLimiting` middleware (available in .NET 7+). Steps: (1) Add services with `AddRateLimiter()` and configure policies, (2) Add `UseRateLimiter()` middleware to pipeline, (3) Apply policies using `[EnableRateLimiting("policyName")]` attribute on controllers/actions. Configure algorithms like FixedWindow, SlidingWindow, TokenBucket, or Concurrency limiter based on requirements.

### 5. What are the common rate limiting algorithms?

Four main algorithms: (1) **Fixed Window** - fixed number of requests per time window (simple but allows bursts at boundaries), (2) **Sliding Window** - rolling window that prevents boundary bursts (more accurate), (3) **Token Bucket** - allows bursts while maintaining average rate (good for variable traffic), (4) **Concurrency Limiter** - limits concurrent requests, not rate (good for expensive operations). Choose based on use case: Fixed for simplicity, Sliding for fairness, Token Bucket for bursts, Concurrency for resource protection.

### 6. How do you implement per-user rate limiting?

Use policy-based rate limiting that partitions by user ID from claims. Configure with `AddPolicy()` and extract user ID using `context.User.FindFirst("sub")?.Value`. Return `RateLimitPartition.GetFixedWindowLimiter(userId, ...)` to create separate limits per user. Apply to endpoints with `[EnableRateLimiting("per-user")]`. For authenticated APIs, this ensures each user gets their own quota independent of others.

### 7. What's the difference between FixedWindow and SlidingWindow?

**FixedWindow** resets the counter at fixed intervals (e.g., every 1 minute), which can allow bursts at window boundaries (100 requests at 00:59, then 100 more at 01:00 = 200 in 2 seconds). **SlidingWindow** uses a rolling time window that moves continuously, counting requests in the last N seconds/minutes from the current time, preventing boundary burst exploitation. Sliding is more accurate but slightly more complex.

### 8. How do you handle rate limit responses for clients?

Return HTTP 429 with informative headers and body. Include: (1) `Retry-After` header indicating when to retry, (2) `X-RateLimit-Limit` showing the limit, (3) `X-RateLimit-Remaining` showing remaining quota, (4) `X-RateLimit-Reset` showing when limit resets. Provide a JSON body with error details and documentation URL. Configure using `options.OnRejected` callback to customize the response.

### 9. When should you use Concurrency Limiter instead of Fixed Window?

Use **Concurrency Limiter** when protecting resource-intensive operations based on simultaneous execution (e.g., video transcoding, PDF generation, database-heavy queries, external API calls with connection limits). Use **Fixed Window** for general API quotas based on request count over time. Concurrency limits "how many at once," while Fixed Window limits "how many per time period." Choose concurrency when operation duration matters more than request frequency.

### 10. How do you implement different rate limits for different subscription tiers?

Use policy-based partitioning with API keys or user roles. Extract tier information from API key header or user claims, then return different `RateLimitPartition` configurations based on tier (e.g., Free: 100/hour, Pro: 1000/hour, Enterprise: 10000/hour). Use switch expression to map tier to appropriate limiter options. Store tier information in database/cache and query during partition creation. Apply with `[EnableRateLimiting("tier-based")]` on endpoints.

---

## Summary

- **Rate limiting protects APIs from abuse** by controlling request volume per client, preventing DDoS attacks, brute force attempts, and resource exhaustion while ensuring fair usage
- **ASP.NET Core 7+ includes built-in rate limiting** with `Microsoft.AspNetCore.RateLimiting` middleware, supporting four algorithms: Fixed Window (simple quotas), Sliding Window (accurate limits), Token Bucket (burst tolerance), and Concurrency Limiter (simultaneous requests)
- **Apply rate limiting flexibly** at global, controller, or endpoint level using `[EnableRateLimiting("policyName")]`, with different policies for different security requirements (strict for login, relaxed for health checks, concurrency for expensive operations)
- **Implement per-user or per-API-key limits** by partitioning based on user ID from claims or API key from headers, enabling tiered access (free/pro/enterprise) with different quotas
- **Return proper HTTP 429 responses** with `Retry-After`, `X-RateLimit-*` headers, and informative JSON body so clients know when to retry and understand their quota status
- **Avoid common mistakes** like over-restrictive limits that frustrate legitimate users, applying same limits to all endpoints regardless of cost, forgetting response headers, and not considering distributed deployments where in-memory limits don't work across multiple servers
- **Monitor and adjust rate limits** based on real usage patterns, log violations for security analysis, and alert on frequent limit hits to detect attacks or adjust policies—rate limiting is not set-and-forget
