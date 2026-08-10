# Caching in ASP.NET Core – Improving Performance and Scalability

## Introduction

Caching is one of the most effective techniques to improve application performance and scalability by storing frequently accessed data in fast-access storage. Without caching, applications repeatedly query databases, call external APIs, or perform expensive computations for the same data, causing unnecessary load and slower response times. ASP.NET Core provides three types of caching: in-memory caching for single-server scenarios, distributed caching for multi-server deployments, and response caching for HTTP responses. This guide explains practical caching strategies, implementation patterns, and real-world scenarios for optimizing your ASP.NET Core applications.

---

## What is Caching?

**Caching** is the practice of storing copies of frequently accessed data in a fast-access storage layer (cache) to reduce retrieval time, database load, and external API calls. Instead of querying the database every time, the application checks the cache first—if the data exists (cache hit), it's returned immediately; if not (cache miss), the data is fetched, stored in cache, and returned.

**Cache Hit vs Cache Miss:**
```
Request → Check Cache
   ├─ Cache Hit: Return data immediately (fast)
   └─ Cache Miss: Fetch from database → Store in cache → Return data (slow, first time)
```

### In-Memory vs Distributed Caching

| Aspect | In-Memory Cache | Distributed Cache |
|--------|----------------|-------------------|
| **Storage** | Local server memory (RAM) | External cache server (Redis, SQL Server) |
| **Scope** | Single server instance | Shared across multiple servers |
| **Speed** | Fastest (local memory access) | Fast (network call required) |
| **Scalability** | Not suitable for multi-server | Required for load-balanced apps |
| **Data Loss Risk** | Lost on app restart | Persists across app restarts |
| **Use Case** | Single-server apps, development | Production multi-server deployments |

---

## Types of Caching in ASP.NET Core

### 1. In-Memory Caching

Stores data in the local server's memory using `IMemoryCache`. Fast but limited to a single server instance.

**Characteristics:**
- Fastest caching option (no network calls)
- Data lost on application restart
- Not shared across multiple servers
- Best for single-server scenarios or development

**When to Use:**
- Frequently accessed reference data (countries, categories)
- Single-server deployments
- Development/testing environments
- Data that's acceptable to lose on restart

### 2. Distributed Caching

Stores data in an external cache server (Redis, SQL Server, NCache) shared across multiple application servers.

**Characteristics:**
- Shared cache across all servers
- Survives application restarts
- Requires external cache server
- Network latency (slower than in-memory, faster than database)

**When to Use:**
- Multi-server (load-balanced) environments
- Session state in web farms
- Data that must persist across restarts
- High-availability scenarios

**Common Implementations:**
- **Redis** (most popular, high-performance)
- **SQL Server** (when Redis infrastructure isn't available)
- **NCache** (commercial, feature-rich)

### 3. Response Caching

Caches entire HTTP responses based on cache-related headers, reducing server processing for repeated requests.

**Characteristics:**
- Caches full HTTP responses (headers + body)
- Works with HTTP cache headers (Cache-Control, ETag)
- Can leverage browser cache and CDN
- Reduces server load for repeated identical requests

**When to Use:**
- Public APIs with mostly GET requests
- Static or semi-static content
- Read-heavy endpoints
- Content that doesn't change frequently

---

## In-Memory Caching

### Configuring In-Memory Cache

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Register in-memory caching services
builder.Services.AddMemoryCache();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
```

### Basic Usage with IMemoryCache

```csharp
using Microsoft.Extensions.Caching.Memory;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMemoryCache cache, ILogger<ProductsController> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        const string cacheKey = "products_list";

        // Try to get data from cache
        if (_cache.TryGetValue(cacheKey, out List<Product>? cachedProducts))
        {
            _logger.LogInformation("Cache hit: Returning products from cache");
            return Ok(cachedProducts);
        }

        // Cache miss: Fetch from database
        _logger.LogInformation("Cache miss: Fetching products from database");
        var products = FetchProductsFromDatabase();

        // Store in cache with 5-minute expiration
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };
        _cache.Set(cacheKey, products, cacheOptions);

        return Ok(products);
    }

    private List<Product> FetchProductsFromDatabase()
    {
        // Simulate database call
        return new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99m },
            new Product { Id = 2, Name = "Mouse", Price = 29.99m }
        };
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
```

### Cache with GetOrCreate Pattern

```csharp
[HttpGet("{id}")]
public IActionResult GetProduct(int id)
{
    var cacheKey = $"product_{id}";

    // GetOrCreate: Automatically handles cache hit/miss
    var product = _cache.GetOrCreate(cacheKey, entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
        entry.SetPriority(CacheItemPriority.Normal);
        
        _logger.LogInformation("Cache miss: Fetching product {ProductId} from database", id);
        return FetchProductFromDatabase(id);
    });

    return Ok(product);
}

private Product FetchProductFromDatabase(int id)
{
    // Simulate database call
    return new Product { Id = id, Name = "Laptop", Price = 999.99m };
}
```

### Async Cache Operations

```csharp
[HttpGet("categories")]
public async Task<IActionResult> GetCategories()
{
    const string cacheKey = "categories";

    // GetOrCreateAsync for async operations
    var categories = await _cache.GetOrCreateAsync(cacheKey, async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
        
        _logger.LogInformation("Fetching categories from database");
        return await FetchCategoriesFromDatabaseAsync();
    });

    return Ok(categories);
}

private async Task<List<string>> FetchCategoriesFromDatabaseAsync()
{
    // Simulate async database call
    await Task.Delay(100);
    return new List<string> { "Electronics", "Clothing", "Books" };
}
```

---

## Distributed Caching

### Configuring Distributed Cache (Redis)

```bash
# Install Redis package
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis
```

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Register distributed cache with Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "MyApp_";
});

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
```

```json
// appsettings.json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379"
  }
}
```

### Using IDistributedCache

```csharp
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IDistributedCache cache, ILogger<OrdersController> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var cacheKey = $"order_{id}";

        // Try to get from cache
        var cachedData = await _cache.GetStringAsync(cacheKey);
        if (cachedData != null)
        {
            _logger.LogInformation("Cache hit: Returning order {OrderId} from Redis", id);
            var cachedOrder = JsonSerializer.Deserialize<Order>(cachedData);
            return Ok(cachedOrder);
        }

        // Cache miss: Fetch from database
        _logger.LogInformation("Cache miss: Fetching order {OrderId} from database", id);
        var order = await FetchOrderFromDatabaseAsync(id);

        // Store in cache with 30-minute expiration
        var serializedOrder = JsonSerializer.Serialize(order);
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };
        await _cache.SetStringAsync(cacheKey, serializedOrder, cacheOptions);

        return Ok(order);
    }

    private async Task<Order> FetchOrderFromDatabaseAsync(int id)
    {
        // Simulate database call
        await Task.Delay(100);
        return new Order { Id = id, CustomerName = "John Doe", Total = 199.99m };
    }
}

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal Total { get; set; }
}
```

### Distributed Cache with Binary Data

```csharp
[HttpGet("report/{id}")]
public async Task<IActionResult> GetReport(int id)
{
    var cacheKey = $"report_{id}";

    // Try to get binary data from cache
    var cachedReport = await _cache.GetAsync(cacheKey);
    if (cachedReport != null)
    {
        _logger.LogInformation("Cache hit: Returning report from cache");
        var report = JsonSerializer.Deserialize<Report>(cachedReport);
        return Ok(report);
    }

    // Cache miss: Generate report
    _logger.LogInformation("Cache miss: Generating report {ReportId}", id);
    var newReport = await GenerateReportAsync(id);

    // Store as bytes
    var serialized = JsonSerializer.SerializeToUtf8Bytes(newReport);
    var cacheOptions = new DistributedCacheEntryOptions
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2)
    };
    await _cache.SetAsync(cacheKey, serialized, cacheOptions);

    return Ok(newReport);
}

private async Task<Report> GenerateReportAsync(int id)
{
    // Simulate expensive report generation
    await Task.Delay(2000);
    return new Report { Id = id, Title = "Sales Report", GeneratedAt = DateTime.UtcNow };
}

public class Report
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; }
}
```

### When to Use Redis vs SQL Server

| Factor | Redis | SQL Server |
|--------|-------|------------|
| **Performance** | Fastest (in-memory) | Slower (disk-based) |
| **Complexity** | Requires Redis infrastructure | Uses existing SQL Server |
| **Cost** | Additional infrastructure cost | No additional cost if SQL exists |
| **Scalability** | Excellent horizontal scaling | Limited by SQL Server capacity |
| **Persistence** | Optional persistence to disk | Persistent by default |
| **Best For** | High-performance production apps | Simple deployments, existing SQL infrastructure |

---

## Response Caching

### Configuring Response Caching Middleware

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add response caching services
builder.Services.AddResponseCaching();

builder.Services.AddControllers();

var app = builder.Build();

// Add response caching middleware (BEFORE MapControllers)
app.UseResponseCaching();

app.MapControllers();
app.Run();
```

### Using Response Caching with Attributes

```csharp
[ApiController]
[Route("api/[controller]")]
public class PublicDataController : ControllerBase
{
    // Cache response for 60 seconds
    [HttpGet("countries")]
    [ResponseCache(Duration = 60)]
    public IActionResult GetCountries()
    {
        var countries = new[] { "USA", "Canada", "UK", "Germany", "France" };
        return Ok(countries);
    }

    // Cache with VaryByQueryKeys
    [HttpGet("search")]
    [ResponseCache(Duration = 120, VaryByQueryKeys = new[] { "q" })]
    public IActionResult Search([FromQuery] string q)
    {
        // Different cache entry for each query string value
        var results = new[] { $"Result 1 for {q}", $"Result 2 for {q}" };
        return Ok(results);
    }

    // No caching for this endpoint
    [HttpGet("live-data")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public IActionResult GetLiveData()
    {
        return Ok(new { Timestamp = DateTime.UtcNow, Value = Random.Shared.Next(1, 100) });
    }
}
```

### Response Caching with Cache Profiles

```csharp
// Program.cs
builder.Services.AddControllers(options =>
{
    // Define reusable cache profiles
    options.CacheProfiles.Add("Default", new CacheProfile
    {
        Duration = 60
    });
    
    options.CacheProfiles.Add("Long", new CacheProfile
    {
        Duration = 3600 // 1 hour
    });
    
    options.CacheProfiles.Add("NoCache", new CacheProfile
    {
        NoStore = true,
        Location = ResponseCacheLocation.None
    });
});

// Use cache profiles in controllers
[HttpGet("static-data")]
[ResponseCache(CacheProfileName = "Long")]
public IActionResult GetStaticData()
{
    return Ok(new { Message = "This data rarely changes" });
}
```

### Manual Response Cache Headers

```csharp
[HttpGet("custom-cache")]
public IActionResult GetWithCustomCache()
{
    // Set cache headers manually
    Response.Headers.CacheControl = "public, max-age=300"; // 5 minutes
    Response.Headers.ETag = "\"version-123\"";
    Response.Headers.Vary = "Accept-Encoding";

    return Ok(new { Data = "Cached data" });
}
```

---

## Cache Expiration Strategies

### 1. Absolute Expiration

Cache entry expires at a specific date/time, regardless of access patterns.

```csharp
// Expires after 10 minutes from now
var options = new MemoryCacheEntryOptions
{
    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
};
_cache.Set("key", value, options);

// Or expires at specific date/time
var options2 = new MemoryCacheEntryOptions
{
    AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(2)
};
_cache.Set("key", value, options2);
```

**Use Case:**
- Time-sensitive data (stock prices, weather)
- Daily reports
- Data that becomes stale after fixed time

### 2. Sliding Expiration

Cache entry expires if not accessed for a specified duration. Resets on each access.

```csharp
// Expires if not accessed for 5 minutes
var options = new MemoryCacheEntryOptions
{
    SlidingExpiration = TimeSpan.FromMinutes(5)
};
_cache.Set("key", value, options);

// Combining sliding and absolute expiration
var options2 = new MemoryCacheEntryOptions
{
    SlidingExpiration = TimeSpan.FromMinutes(5),  // Reset on access
    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) // Maximum lifetime
};
_cache.Set("key", value, options2);
```

**Use Case:**
- User session data
- Frequently accessed reference data
- Data that should stay cached while actively used

**Behavior Example:**
```
Time 0:00 → Cache entry created (expires at 0:05)
Time 0:03 → Accessed → Expiration reset to 0:08
Time 0:06 → Accessed → Expiration reset to 0:11
Time 0:11 → Not accessed → Entry expires
```

### 3. Cache Priority and Eviction

Control which entries are removed first when memory is limited.

```csharp
var options = new MemoryCacheEntryOptions
{
    Priority = CacheItemPriority.High, // Less likely to be evicted
    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
};
_cache.Set("important_data", value, options);

var options2 = new MemoryCacheEntryOptions
{
    Priority = CacheItemPriority.Low, // First to be evicted under memory pressure
    SlidingExpiration = TimeSpan.FromMinutes(10)
};
_cache.Set("less_important", value2, options2);
```

**Priority Levels:**
- `CacheItemPriority.Low` - Evicted first
- `CacheItemPriority.Normal` - Default
- `CacheItemPriority.High` - Evicted last
- `CacheItemPriority.NeverRemove` - Never evicted (use carefully!)

### 4. Cache Removal Callbacks

Execute code when cache entry is evicted.

```csharp
var options = new MemoryCacheEntryOptions
{
    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
    
    // Callback when entry is removed
    RegisterPostEvictionCallback((key, value, reason, state) =>
    {
        _logger.LogInformation(
            "Cache entry {Key} was removed. Reason: {Reason}", 
            key, 
            reason);
    })
};
_cache.Set("monitored_key", value, options);
```

**Eviction Reasons:**
- `Expired` - Time-based expiration
- `Capacity` - Memory pressure
- `Removed` - Manually removed via Remove()
- `Replaced` - Overwritten with new value
- `TokenExpired` - Change token triggered

---

## Caching with Dependency Injection

### Service Lifetime Considerations

```csharp
// IMemoryCache and IDistributedCache are registered as Singletons
builder.Services.AddMemoryCache();        // Singleton
builder.Services.AddDistributedCache();   // Singleton

// Safe to inject into any service
public class ProductService
{
    private readonly IMemoryCache _cache;
    
    public ProductService(IMemoryCache cache)
    {
        _cache = cache; // Safe: Cache is singleton
    }
}
```

### Creating a Caching Service

```csharp
// ICacheService.cs
public interface ICacheService
{
    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
    Task RemoveByPrefixAsync(string prefix);
}

// CacheService.cs
public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheService> _logger;

    public CacheService(IDistributedCache cache, ILogger<CacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        // Try to get from cache
        var cachedData = await _cache.GetStringAsync(key);
        if (cachedData != null)
        {
            _logger.LogInformation("Cache hit: {Key}", key);
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        // Cache miss: Execute factory
        _logger.LogInformation("Cache miss: {Key}", key);
        var data = await factory();

        // Store in cache
        var serialized = JsonSerializer.Serialize(data);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(10)
        };
        await _cache.SetStringAsync(key, serialized, options);

        return data;
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
        _logger.LogInformation("Cache removed: {Key}", key);
    }

    public async Task RemoveByPrefixAsync(string prefix)
    {
        // Note: Requires Redis-specific implementation or tracking cache keys
        _logger.LogInformation("Removing cache keys with prefix: {Prefix}", prefix);
        // Implementation depends on cache provider
    }
}

// Register service
builder.Services.AddScoped<ICacheService, CacheService>();
```

### Using the Caching Service

```csharp
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICacheService _cacheService;

    public CustomersController(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        var customer = await _cacheService.GetOrCreateAsync(
            key: $"customer_{id}",
            factory: async () => await FetchCustomerFromDatabaseAsync(id),
            expiration: TimeSpan.FromMinutes(15)
        );

        return Ok(customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
    {
        // Update in database
        await UpdateCustomerInDatabaseAsync(id, customer);

        // Invalidate cache
        await _cacheService.RemoveAsync($"customer_{id}");

        return NoContent();
    }

    private async Task<Customer> FetchCustomerFromDatabaseAsync(int id)
    {
        await Task.Delay(50);
        return new Customer { Id = id, Name = "John Doe", Email = "john@example.com" };
    }

    private async Task UpdateCustomerInDatabaseAsync(int id, Customer customer)
    {
        await Task.Delay(50);
    }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
```

---

## Real-World Use Cases

### 1. Frequently Accessed Reference Data

```csharp
// Scenario: Country list rarely changes, accessed on every request
[HttpGet("countries")]
public async Task<IActionResult> GetCountries()
{
    const string cacheKey = "countries";

    var countries = await _cache.GetOrCreateAsync(cacheKey, async entry =>
    {
        // Cache for 24 hours (reference data changes rarely)
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
        entry.SetPriority(CacheItemPriority.High); // Important data

        return await FetchCountriesFromDatabaseAsync();
    });

    return Ok(countries);
}
```

### 2. Expensive Database Queries

```csharp
// Scenario: Complex report with joins, aggregations
[HttpGet("sales-report")]
public async Task<IActionResult> GetSalesReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
{
    var cacheKey = $"sales_report_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}";

    var report = await _cacheService.GetOrCreateAsync(
        key: cacheKey,
        factory: async () =>
        {
            // Expensive query: joins, aggregations, calculations
            await Task.Delay(3000); // Simulates 3-second query
            return new SalesReport
            {
                TotalSales = 150000m,
                OrderCount = 1234,
                AverageOrderValue = 121.55m
            };
        },
        expiration: TimeSpan.FromHours(1)
    );

    return Ok(report);
}

public class SalesReport
{
    public decimal TotalSales { get; set; }
    public int OrderCount { get; set; }
    public decimal AverageOrderValue { get; set; }
}
```

### 3. External API Call Caching

```csharp
// Scenario: Weather API costs money per call
[HttpGet("weather/{city}")]
public async Task<IActionResult> GetWeather(string city)
{
    var cacheKey = $"weather_{city.ToLowerInvariant()}";

    var weather = await _cacheService.GetOrCreateAsync(
        key: cacheKey,
        factory: async () =>
        {
            // Expensive external API call
            return await CallExternalWeatherApiAsync(city);
        },
        expiration: TimeSpan.FromMinutes(30) // Weather data valid for 30 min
    );

    return Ok(weather);
}

private async Task<WeatherData> CallExternalWeatherApiAsync(string city)
{
    // Call external API (costs money!)
    await Task.Delay(500);
    return new WeatherData { City = city, Temperature = 22, Condition = "Sunny" };
}

public class WeatherData
{
    public string City { get; set; } = string.Empty;
    public int Temperature { get; set; }
    public string Condition { get; set; } = string.Empty;
}
```

### 4. User Session Data (Distributed Cache)

```csharp
// Scenario: Shopping cart in multi-server environment
[HttpGet("cart")]
public async Task<IActionResult> GetCart()
{
    var userId = User.FindFirst("sub")?.Value;
    var cacheKey = $"cart_{userId}";

    var cartData = await _distributedCache.GetStringAsync(cacheKey);
    if (cartData != null)
    {
        var cart = JsonSerializer.Deserialize<ShoppingCart>(cartData);
        return Ok(cart);
    }

    // New empty cart
    var newCart = new ShoppingCart { UserId = userId, Items = new List<CartItem>() };
    return Ok(newCart);
}

[HttpPost("cart/add")]
public async Task<IActionResult> AddToCart([FromBody] CartItem item)
{
    var userId = User.FindFirst("sub")?.Value;
    var cacheKey = $"cart_{userId}";

    // Get existing cart
    var cartData = await _distributedCache.GetStringAsync(cacheKey);
    var cart = cartData != null
        ? JsonSerializer.Deserialize<ShoppingCart>(cartData)
        : new ShoppingCart { UserId = userId, Items = new List<CartItem>() };

    // Add item
    cart!.Items.Add(item);

    // Save back to cache (30-minute sliding expiration)
    var options = new DistributedCacheEntryOptions
    {
        SlidingExpiration = TimeSpan.FromMinutes(30)
    };
    await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(cart), options);

    return Ok(cart);
}

public class ShoppingCart
{
    public string? UserId { get; set; }
    public List<CartItem> Items { get; set; } = new();
}

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
```

### 5. API Response Caching for Public Endpoints

```csharp
// Scenario: Public blog API accessed by thousands of websites
[HttpGet("posts")]
[ResponseCache(Duration = 300, VaryByQueryKeys = new[] { "page", "pageSize" })]
public IActionResult GetPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
{
    // Response cached for 5 minutes per unique page/pageSize combination
    var posts = FetchPostsFromDatabase(page, pageSize);
    return Ok(posts);
}

[HttpGet("posts/{id}")]
[ResponseCache(Duration = 600)] // 10 minutes
public IActionResult GetPost(int id)
{
    var post = FetchPostFromDatabase(id);
    return Ok(post);
}

private List<BlogPost> FetchPostsFromDatabase(int page, int pageSize)
{
    return new List<BlogPost>
    {
        new BlogPost { Id = 1, Title = "First Post", Content = "Content..." }
    };
}

private BlogPost FetchPostFromDatabase(int id)
{
    return new BlogPost { Id = id, Title = "Post Title", Content = "Content..." };
}

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
```

---

## Common Mistakes

### ❌ Mistake 1: Caching Sensitive Data

```csharp
// Bad: Caching user passwords or sensitive info
var cacheKey = $"user_{userId}";
_cache.Set(cacheKey, new User
{
    Id = userId,
    Username = "john",
    Password = "hashed_password", // Never cache passwords!
    CreditCard = "1234-5678-9012-3456" // Never cache payment info!
});
```

**Why It's Wrong:**
- Security risk if cache is compromised
- Compliance violations (PCI-DSS, GDPR)
- Cached data may persist longer than intended

**✅ Better Approach:**
```csharp
// Only cache non-sensitive data
var cacheKey = $"user_profile_{userId}";
_cache.Set(cacheKey, new UserProfile
{
    Id = userId,
    Username = "john",
    DisplayName = "John Doe",
    Email = "john@example.com" // Public profile info only
});
```

### ❌ Mistake 2: Using In-Memory Cache in Multi-Server Deployments

```csharp
// Bad: In-memory cache in load-balanced environment
// Server 1: Cache user session
_memoryCache.Set("session_user123", userData);

// Load balancer routes next request to Server 2
// Server 2: Cache miss (session not found)
// Result: User appears logged out!
```

**Why It's Wrong:**
- Each server has its own cache
- Data not shared across servers
- Inconsistent user experience

**✅ Better Approach:**
```csharp
// Use distributed cache (Redis) for multi-server
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis-server:6379";
});

// All servers share the same cache
await _distributedCache.SetStringAsync("session_user123", userData);
```

### ❌ Mistake 3: Not Handling Cache Invalidation

```csharp
// Bad: Update data without invalidating cache
[HttpPut("products/{id}")]
public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
{
    await UpdateProductInDatabaseAsync(id, product);
    
    // Cache still has old data!
    return NoContent();
}

// Later: GET /api/products/123 returns stale cached data
```

**Why It's Wrong:**
- Stale data returned to users
- Inconsistent state between cache and database
- Data integrity issues

**✅ Better Approach:**
```csharp
[HttpPut("products/{id}")]
public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
{
    await UpdateProductInDatabaseAsync(id, product);
    
    // Invalidate cache after update
    _cache.Remove($"product_{id}");
    _cache.Remove("products_list"); // Also invalidate list cache
    
    return NoContent();
}
```

### ❌ Mistake 4: Caching Everything Blindly

```csharp
// Bad: Caching data that changes frequently
[HttpGet("stock-price/{symbol}")]
public IActionResult GetStockPrice(string symbol)
{
    var cacheKey = $"stock_{symbol}";
    
    // Stock prices change every second!
    var price = _cache.GetOrCreate(cacheKey, entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60); // Too long!
        return FetchRealTimeStockPrice(symbol);
    });
    
    return Ok(price);
}
```

**Why It's Wrong:**
- Real-time data becomes stale immediately
- Defeats the purpose of real-time updates
- Misleads users with outdated information

**✅ Better Approach:**
```csharp
// Don't cache real-time data, or use very short expiration
[HttpGet("stock-price/{symbol}")]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
public IActionResult GetStockPrice(string symbol)
{
    // No caching for real-time data
    var price = FetchRealTimeStockPrice(symbol);
    return Ok(price);
}

// Or if you must cache, use extremely short expiration
var price = _cache.GetOrCreate(cacheKey, entry =>
{
    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5); // 5 seconds max
    return FetchRealTimeStockPrice(symbol);
});
```

### ❌ Mistake 5: Not Monitoring Cache Performance

```csharp
// Bad: No visibility into cache effectiveness
_cache.GetOrCreate("key", entry => ExpensiveOperation());

// Questions you can't answer:
// - What's the cache hit ratio?
// - Which cache keys are most accessed?
// - Is caching actually improving performance?
```

**Why It's Wrong:**
- Can't measure cache effectiveness
- No data for optimization decisions
- Miss performance issues

**✅ Better Approach:**
```csharp
[HttpGet("{id}")]
public IActionResult GetProduct(int id)
{
    var cacheKey = $"product_{id}";
    var stopwatch = Stopwatch.StartNew();

    if (_cache.TryGetValue(cacheKey, out Product? product))
    {
        stopwatch.Stop();
        _logger.LogInformation(
            "Cache HIT for {Key} - Retrieved in {Ms}ms", 
            cacheKey, 
            stopwatch.ElapsedMilliseconds);
        
        return Ok(product);
    }

    // Cache miss
    product = FetchProductFromDatabase(id);
    _cache.Set(cacheKey, product, TimeSpan.FromMinutes(10));
    
    stopwatch.Stop();
    _logger.LogInformation(
        "Cache MISS for {Key} - Retrieved in {Ms}ms", 
        cacheKey, 
        stopwatch.ElapsedMilliseconds);

    return Ok(product);
}
```

### ❌ Mistake 6: Large Objects in Cache

```csharp
// Bad: Caching massive objects
var hugeLogo = File.ReadAllBytes("company-logo-50mb.png"); // 50 MB!
_cache.Set("logo", hugeLogo, TimeSpan.FromDays(1));

// Result: Memory pressure, potential OutOfMemoryException
```

**Why It's Wrong:**
- Consumes excessive memory
- Can trigger aggressive cache eviction
- May cause OutOfMemoryException

**✅ Better Approach:**
```csharp
// Store large files in blob storage (Azure Blob, S3)
// Cache only the URL or metadata
_cache.Set("logo_url", "https://cdn.example.com/logo.png", TimeSpan.FromDays(1));

// Or use distributed cache with compression for large objects
```

---

## Caching vs Database Queries

| Aspect | Caching | Database Query |
|--------|---------|----------------|
| **Speed** | Very fast (microseconds) | Slower (milliseconds to seconds) |
| **Latency** | In-memory: <1ms<br>Distributed: 1-5ms | Simple query: 10-50ms<br>Complex query: 100ms-10s |
| **Consistency** | May be stale | Always current |
| **Scalability** | Reduces database load | Can overload database |
| **Cost** | Memory/cache infrastructure | Database compute/storage |
| **Best For** | Frequently read, rarely changed data | Real-time, frequently changing data |
| **Data Freshness** | Depends on expiration | Always fresh |
| **Complexity** | Requires invalidation strategy | Simpler, no cache management |

**When to Use Caching:**
- Read-heavy workloads (90%+ reads)
- Reference data (countries, categories)
- Expensive queries (joins, aggregations)
- External API calls
- High traffic endpoints

**When to Skip Caching:**
- Real-time data requirements
- Data changes frequently
- One-time queries
- User-specific sensitive data
- Simple, fast queries (<10ms)

---

## Interview Questions

### 1. What's the difference between IMemoryCache and IDistributedCache?

**IMemoryCache** stores data in the local server's memory (fastest, but not shared across servers and lost on restart). **IDistributedCache** stores data in an external cache server like Redis (shared across all servers, persists through restarts, but slightly slower due to network calls). Use IMemoryCache for single-server apps or development; use IDistributedCache for production multi-server deployments.

### 2. When should you use Redis for caching?

Use Redis when: (1) running multiple servers (load-balanced/scaled-out), (2) need to share cache across all instances, (3) data must survive application restarts, (4) high-performance distributed cache is required, (5) session state in web farms. Redis is the most popular distributed cache due to its speed (in-memory), rich data structures, persistence options, and horizontal scalability.

### 3. What is response caching?

Response caching stores entire HTTP responses (headers + body) based on cache-related headers like Cache-Control and ETag. It reduces server processing by returning cached responses for repeated identical requests. Configured using `[ResponseCache]` attribute or manual cache headers. Works with browser cache and CDNs. Best for public GET endpoints with static or semi-static content.

### 4. What's the difference between absolute and sliding expiration?

**Absolute expiration** removes the cache entry after a fixed duration regardless of access (e.g., expires 10 minutes after creation). **Sliding expiration** removes the entry if not accessed for a specified duration, but resets the timer on each access (e.g., expires 5 minutes after last access). Use absolute for time-sensitive data; use sliding for frequently accessed data that should stay cached while active.

### 5. How do you handle cache invalidation after data updates?

Explicitly remove the cache entry after updating the database using `_cache.Remove(key)` or `_distributedCache.RemoveAsync(key)`. For related data (e.g., updating a product should invalidate both product detail and product list caches), remove all affected cache keys. Alternatively, use change tokens or event-based cache invalidation patterns for more complex scenarios.

### 6. Can you cache sensitive data like passwords or credit cards?

**No, never cache sensitive data** like passwords, credit card numbers, SSNs, or PII without proper encryption and compliance controls. Caching sensitive data creates security risks (cache compromise), compliance violations (PCI-DSS, GDPR), and data leakage. Only cache non-sensitive profile information. Store sensitive data securely in databases with proper access controls.

### 7. Why is in-memory caching problematic in multi-server deployments?

Each server has its own in-memory cache instance, so cached data is not shared. If Server 1 caches user session and the load balancer routes the next request to Server 2, the session won't be found (cache miss), causing inconsistent user experience (appearing logged out). Solution: Use distributed cache (Redis) shared across all servers.

### 8. What are cache profiles in response caching?

Cache profiles are reusable caching configurations defined once and applied to multiple endpoints using `[ResponseCache(CacheProfileName = "ProfileName")]`. Defined in `AddControllers()` with options like Duration, Location, VaryByHeader. Promotes consistency, reduces duplication, and centralizes cache policy management across the application.

### 9. How do you measure cache effectiveness?

Track cache hit ratio (hits / total requests), measure response times for cache hits vs misses, monitor memory usage, log cache operations (hits/misses), and use Application Performance Monitoring (APM) tools. Calculate metrics like cache hit percentage (should be >70% for effective caching), average retrieval time, and eviction rate. Adjust expiration times and cache keys based on data.

### 10. When should you NOT use caching?

Don't cache when: (1) data changes frequently (real-time stock prices, live scores), (2) data is user-specific and sensitive (passwords, payment info), (3) queries are already very fast (<10ms), (4) one-time queries rarely repeated, (5) memory is extremely limited, (6) caching complexity outweighs benefits. Caching adds complexity (invalidation, consistency) so only use when clear performance benefits exist.

---

## Summary

- **Caching dramatically improves performance** by storing frequently accessed data in fast-access storage (memory or Redis), reducing database load, external API calls, and response times from milliseconds/seconds to microseconds
- **ASP.NET Core provides three caching types**: IMemoryCache (fastest, single-server, lost on restart), IDistributedCache (shared across servers, requires Redis/SQL Server, persists through restarts), and Response Caching (caches entire HTTP responses with Cache-Control headers)
- **Use distributed caching (Redis) in production** when running multiple servers (load-balanced) because in-memory cache is per-server and creates inconsistent user experiences—Redis shares cache across all instances and survives restarts
- **Implement proper expiration strategies**: absolute expiration for time-sensitive data (expires after fixed time), sliding expiration for actively-used data (resets timer on access), and cache priorities to control eviction under memory pressure
- **Always invalidate cache after updates** by explicitly removing affected cache keys using `Remove()` or `RemoveAsync()` after database updates—forgetting invalidation causes stale data and data integrity issues
- **Never cache sensitive data** like passwords, credit cards, or PII without encryption/compliance controls—cache only public profile information and reference data to avoid security risks and regulatory violations
- **Monitor cache effectiveness** by tracking hit ratios (should be >70%), response time improvements, memory usage, and eviction rates—cache only when clear performance benefits exist and avoid caching real-time data, fast queries, or rarely-accessed data
