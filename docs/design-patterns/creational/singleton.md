# Singleton Pattern

## Pattern Category
**Creational Pattern**

## Academic Foundation

### Historical Context
The Singleton pattern was first formally documented by the Gang of Four in 1994, though the concept of ensuring single instance creation has roots in earlier programming practices. It emerged from the need to control object instantiation in object-oriented systems where global state management was required.

### Theoretical Basis
The Singleton pattern is grounded in several computer science principles:

1. **Controlled Instantiation**: Based on the principle that some resources should have exactly one instance (e.g., database connections, thread pools)
2. **Global Access Point**: Provides a globally accessible instance without using global variables
3. **Lazy Initialization**: Supports deferred object creation until first use
4. **Resource Management**: Ensures efficient use of expensive resources

### Academic Classification
- **Pattern Type**: Object Creational
- **Scope**: Class-level (controls instantiation of a specific class)
- **Structure**: Involves a single class that is responsible for creating and managing its own unique instance

## Intent / Definition
The Singleton pattern ensures that a class has only one instance and provides a global point of access to that instance. It's useful when exactly one object is needed to coordinate actions across the system.

### Formal Definition
*"Ensure a class only has one instance, and provide a global point of access to it."* - Gang of Four

### Academic Problem Statement
In object-oriented systems, there are scenarios where:
- Multiple instances of a class would cause logical errors or resource conflicts
- A single point of control is needed for coordinating system-wide actions
- Global state must be managed in a controlled manner
- Expensive resources need to be shared efficiently across the application

## Problem Statement
- You need exactly one instance of a class (e.g., database connection pool, logger, configuration manager)
- Multiple instances would cause problems (resource conflicts, inconsistent state)
- You need global access to this instance
- You want to control instantiation and ensure thread safety

## Academic Analysis

### Forces and Constraints
The Singleton pattern addresses several competing forces:

1. **Uniqueness vs. Flexibility**: Need for single instance vs. potential future need for multiple instances
2. **Global Access vs. Encapsulation**: Providing global access while maintaining proper encapsulation
3. **Thread Safety vs. Performance**: Ensuring thread-safe access while minimizing synchronization overhead
4. **Lazy vs. Eager Initialization**: Balancing resource usage with initialization timing

### Theoretical Participants
1. **Singleton Class**: The class that controls its own instantiation
   - Maintains a static reference to its unique instance
   - Provides a static method to access the instance
   - Has a private constructor to prevent external instantiation

### Collaboration Patterns
- **Client → Singleton**: Clients access the singleton through the static getInstance() method
- **Singleton → Self**: The singleton manages its own lifecycle and instance creation

## When to Use

### Academic Criteria for Application
✅ **Use Singleton when:**
- **Mathematical Uniqueness**: The problem domain requires exactly one instance (e.g., system clock, random number generator seed)
- **Resource Coordination**: Need to coordinate access to a shared resource (database connection pool, file system)
- **Global State Management**: Managing application-wide configuration or state
- **Performance Optimization**: Expensive object creation that should be done once

❌ **Don't use Singleton when:**
- **Testability Requirements**: Unit testing requires multiple instances or mocking
- **Scalability Concerns**: Future requirements might need multiple instances
- **Dependency Injection Available**: Modern IoC containers can manage singleton lifecycle
- **Simple Utility Functions**: Static classes are more appropriate for stateless utilities

### Academic Decision Framework
Consider these factors when evaluating Singleton usage:

1. **Cardinality Analysis**: Is there a fundamental reason why only one instance should exist?
2. **Coupling Analysis**: Will the singleton create tight coupling throughout the system?
3. **Testing Impact**: How will the singleton affect unit testing and mocking?
4. **Concurrency Requirements**: What are the thread-safety requirements?
5. **Lifecycle Management**: Who should control the object's lifecycle?

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

### Academic UML Analysis
- **Static Association**: The class has a static association with itself
- **Creation Responsibility**: The class is responsible for its own creation
- **Access Control**: Private constructor prevents external instantiation
- **Global Access**: Public static method provides controlled access

## C# Implementation

### Thread-Safe Lazy Initialization (Recommended)
```csharp
/// <summary>
/// Academic Implementation: Thread-safe singleton using Lazy<T>
/// 
/// Theoretical Benefits:
/// - Thread-safe without explicit locking
/// - Lazy initialization (created only when first accessed)
/// - No performance penalty after initialization
/// - Exception-safe (if constructor throws, subsequent calls will retry)
/// </summary>
public sealed class ConfigurationManager
{
    // Lazy<T> provides thread-safe lazy initialization
    private static readonly Lazy<ConfigurationManager> _instance = 
        new Lazy<ConfigurationManager>(() => new ConfigurationManager());
    
    private readonly Dictionary<string, string> _settings;
    
    // Private constructor prevents external instantiation
    private ConfigurationManager()
    {
        _settings = new Dictionary<string, string>();
        LoadConfiguration();
    }
    
    // Public access point - thread-safe and lazy
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

### Academic Analysis of Lazy<T> Approach
**Theoretical Advantages**:
- **Thread Safety**: Uses internal locking mechanism that's optimized by the CLR
- **Performance**: No synchronization overhead after first initialization
- **Exception Safety**: If constructor fails, subsequent calls will retry initialization
- **Memory Model**: Proper memory barriers ensure visibility across threads

### Double-Checked Locking (Alternative - Academic Interest)
```csharp
/// <summary>
/// Academic Implementation: Double-checked locking pattern
/// 
/// Historical Note: This pattern was problematic in Java due to memory model issues,
/// but works correctly in .NET due to stronger memory guarantees.
/// 
/// Theoretical Analysis:
/// - First check avoids locking overhead in common case
/// - Second check ensures only one thread creates instance
/// - Volatile keyword ensures proper memory ordering
/// </summary>
public sealed class DatabaseConnectionPool
{
    private static volatile DatabaseConnectionPool _instance;
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
            // First check - no locking overhead in common case
            if (_instance == null)
            {
                lock (_lock)
                {
                    // Second check - ensure only one thread creates instance
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

### Academic Analysis of Double-Checked Locking
**Memory Model Considerations**:
- **Volatile Keyword**: Ensures proper memory ordering and prevents compiler optimizations
- **Lock Semantics**: Provides memory barriers that ensure visibility
- **Performance Trade-offs**: Optimizes for the common case (instance already created)

### Modern Approach with DI Container (Academic Best Practice)
```csharp
/// <summary>
/// Academic Implementation: Dependency Injection approach
/// 
/// Theoretical Advantages:
/// - Follows Dependency Inversion Principle
/// - Improves testability through interface abstraction
/// - Separates concerns (lifecycle management vs. business logic)
/// - Supports different lifetime scopes
/// </summary>

// Interface for better testability and abstraction
public interface IApplicationCache
{
    T Get<T>(string key);
    void Set<T>(string key, T value, TimeSpan expiration);
    void Remove(string key);
}

// Implementation - no longer responsible for its own instantiation
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
    // Container manages singleton lifecycle
    services.AddSingleton<IApplicationCache, ApplicationCache>();
}
```

### Academic Analysis of DI Approach
**Theoretical Benefits**:
- **Separation of Concerns**: Object creation separated from business logic
- **Testability**: Interface allows for easy mocking and testing
- **Flexibility**: Can change implementation without affecting clients
- **Lifecycle Management**: Container handles complex lifecycle scenarios

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

### Academic Analysis of Benefits ✅

#### **Controlled Instantiation**
- **Theoretical Basis**: Ensures mathematical uniqueness when required by problem domain
- **Practical Benefit**: Prevents resource conflicts and inconsistent state
- **Example**: Database connection pool prevents connection exhaustion

#### **Global Access Point**
- **Theoretical Basis**: Provides controlled global state without global variables
- **Practical Benefit**: Accessible from anywhere in the application
- **Academic Note**: Violates some OOP principles but solves practical problems

#### **Lazy Initialization**
- **Theoretical Basis**: Deferred computation principle - create only when needed
- **Practical Benefit**: Saves memory and initialization time
- **Performance Impact**: Reduces application startup time

#### **Memory Efficiency**
- **Theoretical Basis**: Single instance reduces memory footprint
- **Practical Benefit**: Reuses expensive objects
- **Scalability**: Important for resource-constrained environments

#### **Thread Safety (When Implemented Correctly)**
- **Theoretical Basis**: Proper synchronization ensures correctness in concurrent environments
- **Practical Benefit**: Safe to use in multi-threaded applications
- **Implementation Note**: Requires careful consideration of memory models

### Academic Analysis of Drawbacks ❌

#### **Testing Difficulties**
- **Theoretical Problem**: Global state makes unit testing harder
- **Practical Impact**: Difficult to mock and isolate for testing
- **Academic Solution**: Use dependency injection and interfaces

#### **Tight Coupling**
- **Theoretical Problem**: Creates hidden dependencies throughout codebase
- **Practical Impact**: Reduces modularity and flexibility
- **SOLID Violation**: Violates Dependency Inversion Principle

#### **Violates Single Responsibility Principle**
- **Theoretical Problem**: Class manages both its instantiation and business logic
- **Academic Analysis**: Mixing concerns reduces cohesion
- **Design Impact**: Makes the class harder to understand and maintain

#### **Hidden Dependencies**
- **Theoretical Problem**: Dependencies not visible in method signatures
- **Practical Impact**: Makes code harder to understand and test
- **Academic Note**: Violates explicit dependency principle

#### **Scalability Issues**
- **Theoretical Problem**: Can become bottleneck in multi-threaded scenarios
- **Practical Impact**: Synchronization overhead affects performance
- **Architectural Concern**: May not scale well in distributed systems

### Academic Trade-off Analysis

| Aspect | Benefit | Cost | Academic Recommendation |
|--------|---------|------|------------------------|
| **Global Access** | Convenience | Tight Coupling | Use DI for better design |
| **Single Instance** | Resource Control | Inflexibility | Consider if truly needed |
| **Thread Safety** | Correctness | Performance Overhead | Use appropriate synchronization |
| **Lazy Loading** | Performance | Complexity | Balance based on usage patterns |

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