# Singleton Pattern

## Pattern Category
**Creational Pattern**

## Intent / Definition
The Singleton pattern ensures that a class has only one instance and provides a global point of access to that instance. It's useful when exactly one object is needed to coordinate actions across the system.

## Problem Statement
- You need exactly one instance of a class (e.g., database connection pool, logger, configuration manager)
- Multiple instances would cause problems (resource conflicts, inconsistent state)
- You need global access to this instance
- You want to control instantiation and ensure thread safety

## When to Use
✅ **Use Singleton when:**
- You need exactly one instance (database connection manager, cache)
- The instance needs to be accessible globally
- You want to control resource usage (expensive objects)
- You need to coordinate actions across the system

❌ **Don't use Singleton when:**
- You might need multiple instances in the future
- It makes unit testing difficult
- It creates tight coupling
- You're using a DI container (use container's singleton scope instead)

## UML Structure
```
┌─────────────────┐
│    Singleton    │
├─────────────────┤
│ - instance      │
│ - Singleton()   │
├─────────────────┤
│ + GetInstance() │
│ + Operation()   │
└─────────────────┘
```

## C# Implementation

### Thread-Safe Lazy Initialization (Recommended)
```csharp
public sealed class ConfigurationManager
{
    private static readonly Lazy<ConfigurationManager> _instance = 
        new Lazy<ConfigurationManager>(() => new ConfigurationManager());
    
    private readonly Dictionary<string, string> _settings;
    
    private ConfigurationManager()
    {
        _settings = new Dictionary<string, string>();
        LoadConfiguration();
    }
    
    public static ConfigurationManager Instance => _instance.Value;
    
    public string GetSetting(string key)
    {
        return _settings.TryGetValue(key, out string value) ? value : null;
    }
    
    public void SetSetting(string key, string value)
    {
        _settings[key] = value;
    }
    
    private void LoadConfiguration()
    {
        // Load from file, database, etc.
        _settings["DatabaseConnection"] = "Server=localhost;Database=MyApp;";
        _settings["ApiKey"] = "your-api-key-here";
    }
}
```

### Double-Checked Locking (Alternative)
```csharp
public sealed class DatabaseConnectionPool
{
    private static DatabaseConnectionPool _instance;
    private static readonly object _lock = new object();
    
    private readonly List<IDbConnection> _connections;
    private readonly string _connectionString;
    
    private DatabaseConnectionPool()
    {
        _connectionString = ConfigurationManager.Instance.GetSetting("DatabaseConnection");
        _connections = new List<IDbConnection>();
        InitializePool();
    }
    
    public static DatabaseConnectionPool Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new DatabaseConnectionPool();
                }
            }
            return _instance;
        }
    }
    
    public IDbConnection GetConnection()
    {
        lock (_lock)
        {
            if (_connections.Count > 0)
            {
                var connection = _connections[0];
                _connections.RemoveAt(0);
                return connection;
            }
            
            return CreateNewConnection();
        }
    }
    
    public void ReturnConnection(IDbConnection connection)
    {
        lock (_lock)
        {
            if (connection.State == ConnectionState.Open)
            {
                _connections.Add(connection);
            }
        }
    }
    
    private void InitializePool()
    {
        for (int i = 0; i < 10; i++)
        {
            _connections.Add(CreateNewConnection());
        }
    }
    
    private IDbConnection CreateNewConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
```

### Modern Approach with DI Container
```csharp
// Interface for better testability
public interface IApplicationCache
{
    T Get<T>(string key);
    void Set<T>(string key, T value, TimeSpan expiration);
    void Remove(string key);
}

// Implementation
public class ApplicationCache : IApplicationCache
{
    private readonly MemoryCache _cache;
    
    public ApplicationCache()
    {
        _cache = new MemoryCache(new MemoryCacheOptions
        {
            SizeLimit = 1000
        });
    }
    
    public T Get<T>(string key)
    {
        return _cache.TryGetValue(key, out T value) ? value : default(T);
    }
    
    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        _cache.Set(key, value, expiration);
    }
    
    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}

// Registration in DI container (Startup.cs or Program.cs)
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IApplicationCache, ApplicationCache>();
}
```

## Client Usage

### Traditional Singleton Usage
```csharp
public class UserService
{
    public async Task<User> GetUserAsync(int userId)
    {
        // Access singleton instance
        var config = ConfigurationManager.Instance;
        var connectionString = config.GetSetting("DatabaseConnection");
        
        var connectionPool = DatabaseConnectionPool.Instance;
        using var connection = connectionPool.GetConnection();
        
        // Use connection to fetch user
        var user = await FetchUserFromDatabase(connection, userId);
        
        connectionPool.ReturnConnection(connection);
        return user;
    }
}
```

### DI Container Approach (Recommended)
```csharp
public class UserService
{
    private readonly IApplicationCache _cache;
    private readonly IConfiguration _configuration;
    
    public UserService(IApplicationCache cache, IConfiguration configuration)
    {
        _cache = cache;
        _configuration = configuration;
    }
    
    public async Task<User> GetUserAsync(int userId)
    {
        string cacheKey = $"user_{userId}";
        
        // Try cache first
        var cachedUser = _cache.Get<User>(cacheKey);
        if (cachedUser != null)
            return cachedUser;
        
        // Fetch from database
        var user = await FetchUserFromDatabase(userId);
        
        // Cache for future requests
        _cache.Set(cacheKey, user, TimeSpan.FromMinutes(30));
        
        return user;
    }
}
```

## Real-World / Enterprise Use Cases

### 1. Application Logger
```csharp
public sealed class ApplicationLogger
{
    private static readonly Lazy<ApplicationLogger> _instance = 
        new Lazy<ApplicationLogger>(() => new ApplicationLogger());
    
    private readonly ILogger _logger;
    
    private ApplicationLogger()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
            builder.AddConsole().AddFile("logs/app.log"));
        _logger = loggerFactory.CreateLogger<ApplicationLogger>();
    }
    
    public static ApplicationLogger Instance => _instance.Value;
    
    public void LogInfo(string message) => _logger.LogInformation(message);
    public void LogError(string message, Exception ex) => _logger.LogError(ex, message);
    public void LogWarning(string message) => _logger.LogWarning(message);
}
```

### 2. Feature Flag Manager
```csharp
public sealed class FeatureFlagManager
{
    private static readonly Lazy<FeatureFlagManager> _instance = 
        new Lazy<FeatureFlagManager>(() => new FeatureFlagManager());
    
    private readonly Dictionary<string, bool> _flags;
    private readonly Timer _refreshTimer;
    
    private FeatureFlagManager()
    {
        _flags = new Dictionary<string, bool>();
        LoadFeatureFlags();
        
        // Refresh flags every 5 minutes
        _refreshTimer = new Timer(RefreshFlags, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
    }
    
    public static FeatureFlagManager Instance => _instance.Value;
    
    public bool IsEnabled(string flagName)
    {
        return _flags.TryGetValue(flagName, out bool isEnabled) && isEnabled;
    }
    
    private void LoadFeatureFlags()
    {
        // Load from configuration service, database, etc.
        _flags["NewPaymentFlow"] = true;
        _flags["BetaFeatures"] = false;
        _flags["MaintenanceMode"] = false;
    }
    
    private void RefreshFlags(object state)
    {
        LoadFeatureFlags();
    }
}
```

### 3. API Rate Limiter
```csharp
public sealed class RateLimiter
{
    private static readonly Lazy<RateLimiter> _instance = 
        new Lazy<RateLimiter>(() => new RateLimiter());
    
    private readonly Dictionary<string, List<DateTime>> _requests;
    private readonly object _lock = new object();
    
    private RateLimiter()
    {
        _requests = new Dictionary<string, List<DateTime>>();
    }
    
    public static RateLimiter Instance => _instance.Value;
    
    public bool IsAllowed(string clientId, int maxRequests, TimeSpan timeWindow)
    {
        lock (_lock)
        {
            var now = DateTime.UtcNow;
            var windowStart = now.Subtract(timeWindow);
            
            if (!_requests.ContainsKey(clientId))
            {
                _requests[clientId] = new List<DateTime>();
            }
            
            var clientRequests = _requests[clientId];
            
            // Remove old requests outside the time window
            clientRequests.RemoveAll(time => time < windowStart);
            
            if (clientRequests.Count >= maxRequests)
            {
                return false; // Rate limit exceeded
            }
            
            clientRequests.Add(now);
            return true;
        }
    }
}
```

## Pros and Cons

### Pros ✅
- **Controlled instantiation**: Ensures only one instance exists
- **Global access**: Available throughout the application
- **Lazy initialization**: Created only when needed
- **Memory efficient**: Reuses the same instance
- **Thread-safe**: When implemented correctly

### Cons ❌
- **Testing difficulties**: Hard to mock and unit test
- **Tight coupling**: Creates dependencies throughout codebase
- **Violates Single Responsibility**: Controls instantiation + business logic
- **Hidden dependencies**: Not visible in constructor/method signatures
- **Scalability issues**: Can become bottleneck in multi-threaded scenarios

## Common Mistakes & Anti-Patterns

### 1. Non-Thread-Safe Implementation
```csharp
// BAD: Not thread-safe
public class BadSingleton
{
    private static BadSingleton _instance;
    
    public static BadSingleton Instance
    {
        get
        {
            if (_instance == null) // Race condition here!
                _instance = new BadSingleton();
            return _instance;
        }
    }
}
```

### 2. Overusing Singleton
```csharp
// BAD: Everything as singleton
public class UserSingleton { } // Should be regular class
public class OrderSingleton { } // Should be regular class
public class ProductSingleton { } // Should be regular class
```

### 3. Singleton with Mutable State
```csharp
// BAD: Mutable state without proper synchronization
public class BadStateSingleton
{
    public int Counter { get; set; } // Not thread-safe!
    
    public void Increment()
    {
        Counter++; // Race condition!
    }
}
```

## Performance Considerations

### Memory Usage
- **Low memory footprint**: Only one instance in memory
- **Potential memory leaks**: Instance lives for application lifetime
- **GC impact**: Singleton instances are never collected

### Thread Safety Performance
```csharp
// Performance comparison
public class PerformanceComparison
{
    // Fastest: Lazy<T> (recommended)
    private static readonly Lazy<MySingleton> _lazy = 
        new Lazy<MySingleton>(() => new MySingleton());
    
    // Slower: Double-checked locking
    private static MySingleton _instance;
    private static readonly object _lock = new object();
    
    public static MySingleton LazyInstance => _lazy.Value;
    
    public static MySingleton LockedInstance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new MySingleton();
                }
            }
            return _instance;
        }
    }
}
```

## Relation to SOLID Principles

### Violations
- **SRP**: Often handles both instantiation control and business logic
- **DIP**: Creates hard dependencies (high-level modules depend on concrete class)

### Solutions
```csharp
// Better: Use DI container for singleton behavior
public interface IConfigurationService
{
    string GetSetting(string key);
}

public class ConfigurationService : IConfigurationService
{
    public string GetSetting(string key)
    {
        // Implementation
        return "value";
    }
}

// Register as singleton in DI container
services.AddSingleton<IConfigurationService, ConfigurationService>();
```

## Interview Questions & Answers

### Q1: How do you make Singleton thread-safe in C#?
**Answer:** Use `Lazy<T>` (recommended), double-checked locking, or static constructor:
```csharp
// Best approach - Lazy<T>
private static readonly Lazy<Singleton> _instance = new Lazy<Singleton>(() => new Singleton());
public static Singleton Instance => _instance.Value;
```

### Q2: What are the problems with Singleton pattern?
**Answer:** 
- Makes unit testing difficult (hard to mock)
- Creates tight coupling throughout application
- Violates Single Responsibility Principle
- Can become bottleneck in multi-threaded applications
- Hidden dependencies not visible in method signatures

### Q3: How do you test code that uses Singleton?
**Answer:** 
- Extract interface and use DI container
- Use wrapper/adapter pattern
- Make Singleton implement interface for mocking
```csharp
public interface ILogger { void Log(string message); }
public class Logger : ILogger { /* implementation */ }

// Test with mock
var mockLogger = new Mock<ILogger>();
var service = new UserService(mockLogger.Object);
```

### Q4: When should you use Singleton vs Static class?
**Answer:**
- **Singleton**: When you need interface implementation, inheritance, lazy initialization, or instance state
- **Static class**: When you have pure utility functions with no state

### Q5: How does DI container handle Singleton lifetime?
**Answer:** DI containers manage singleton lifetime automatically:
```csharp
services.AddSingleton<IService, Service>(); // Container ensures single instance
services.AddScoped<IService, Service>();    // One per request/scope
services.AddTransient<IService, Service>(); // New instance each time
```

## Quick Revision / Summary

**Intent**: Ensure single instance with global access
**Structure**: Private constructor + static instance + public accessor
**Thread Safety**: Use `Lazy<T>` or double-checked locking
**Modern Approach**: Use DI container singleton registration
**Common Use Cases**: Configuration, logging, caching, connection pooling
**Main Problems**: Testing difficulty, tight coupling, SOLID violations
**Best Practice**: Prefer DI container over traditional singleton implementation