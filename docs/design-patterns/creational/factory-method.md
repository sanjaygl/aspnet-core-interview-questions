# Factory Method Pattern

## Pattern Category
**Creational Pattern**

## Intent / Definition
The Factory Method pattern defines an interface for creating objects, but lets subclasses decide which class to instantiate. It delegates the object creation to subclasses, promoting loose coupling by eliminating the need to bind application-specific classes into the code.

## Problem Statement
- You need to create objects without specifying their exact classes
- The exact type of object to create is determined at runtime
- You want to delegate object creation to subclasses
- You need to eliminate tight coupling between creator and concrete products
- Different contexts require different implementations of the same interface

## When to Use
✅ **Use Factory Method when:**
- You don't know beforehand the exact types of objects your code should work with
- You want to provide users with a way to extend internal components
- You want to save system resources by reusing existing objects
- You need to delegate object creation to subclasses

❌ **Don't use Factory Method when:**
- Object creation is simple and doesn't vary
- You only have one concrete implementation
- The creation logic is unlikely to change
- You're overengineering simple object creation

## UML Structure
```
┌─────────────────┐         ┌─────────────────┐
│    Creator      │         │    Product      │
├─────────────────┤         ├─────────────────┤
│ + Operation()   │◇────────│ + Operation()   │
│ + CreateProduct()│         └─────────────────┘
└─────────────────┘                 △
        △                           │
        │                           │
┌─────────────────┐         ┌─────────────────┐
│ ConcreteCreator │         │ ConcreteProduct │
├─────────────────┤         ├─────────────────┤
│ + CreateProduct()│────────▷│ + Operation()   │
└─────────────────┘         └─────────────────┘
```

## C# Implementation

### Basic Factory Method Implementation
```csharp
// Product interface
public interface ILogger
{
    void Log(string message);
    void LogError(string message, Exception exception);
}

// Concrete Products
public class FileLogger : ILogger
{
    private readonly string _filePath;
    
    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }
    
    public void Log(string message)
    {
        File.AppendAllText(_filePath, $"{DateTime.Now}: {message}\n");
    }
    
    public void LogError(string message, Exception exception)
    {
        File.AppendAllText(_filePath, $"{DateTime.Now}: ERROR - {message}\n{exception}\n");
    }
}

public class DatabaseLogger : ILogger
{
    private readonly string _connectionString;
    
    public DatabaseLogger(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public void Log(string message)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = new SqlCommand("INSERT INTO Logs (Message, Timestamp) VALUES (@message, @timestamp)", connection);
        command.Parameters.AddWithValue("@message", message);
        command.Parameters.AddWithValue("@timestamp", DateTime.Now);
        command.ExecuteNonQuery();
    }
    
    public void LogError(string message, Exception exception)
    {
        Log($"ERROR: {message} - {exception}");
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"{DateTime.Now}: {message}");
    }
    
    public void LogError(string message, Exception exception)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{DateTime.Now}: ERROR - {message}");
        Console.WriteLine(exception.ToString());
        Console.ResetColor();
    }
}

// Creator (Factory)
public abstract class LoggerFactory
{
    // Factory method - to be implemented by subclasses
    public abstract ILogger CreateLogger();
    
    // Template method using the factory method
    public void LogMessage(string message)
    {
        var logger = CreateLogger();
        logger.Log($"[{GetType().Name}] {message}");
    }
}

// Concrete Creators
public class FileLoggerFactory : LoggerFactory
{
    private readonly string _filePath;
    
    public FileLoggerFactory(string filePath)
    {
        _filePath = filePath;
    }
    
    public override ILogger CreateLogger()
    {
        return new FileLogger(_filePath);
    }
}

public class DatabaseLoggerFactory : LoggerFactory
{
    private readonly string _connectionString;
    
    public DatabaseLoggerFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public override ILogger CreateLogger()
    {
        return new DatabaseLogger(_connectionString);
    }
}

public class ConsoleLoggerFactory : LoggerFactory
{
    public override ILogger CreateLogger()
    {
        return new ConsoleLogger();
    }
}
```

### Modern C# Implementation with Generics
```csharp
// Generic factory interface
public interface IFactory<T>
{
    T Create();
}

// Repository pattern with factory method
public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task SaveAsync(T entity);
    Task DeleteAsync(int id);
}

public class SqlServerRepository<T> : IRepository<T> where T : class
{
    private readonly string _connectionString;
    
    public SqlServerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<T> GetByIdAsync(int id)
    {
        // SQL Server implementation
        return default(T);
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        // SQL Server implementation
        return new List<T>();
    }
    
    public async Task SaveAsync(T entity)
    {
        // SQL Server implementation
    }
    
    public async Task DeleteAsync(int id)
    {
        // SQL Server implementation
    }
}

public class MongoRepository<T> : IRepository<T> where T : class
{
    private readonly string _connectionString;
    
    public MongoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<T> GetByIdAsync(int id)
    {
        // MongoDB implementation
        return default(T);
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        // MongoDB implementation
        return new List<T>();
    }
    
    public async Task SaveAsync(T entity)
    {
        // MongoDB implementation
    }
    
    public async Task DeleteAsync(int id)
    {
        // MongoDB implementation
    }
}

// Repository factory
public interface IRepositoryFactory
{
    IRepository<T> CreateRepository<T>() where T : class;
}

public class SqlServerRepositoryFactory : IRepositoryFactory
{
    private readonly string _connectionString;
    
    public SqlServerRepositoryFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IRepository<T> CreateRepository<T>() where T : class
    {
        return new SqlServerRepository<T>(_connectionString);
    }
}

public class MongoRepositoryFactory : IRepositoryFactory
{
    private readonly string _connectionString;
    
    public MongoRepositoryFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IRepository<T> CreateRepository<T>() where T : class
    {
        return new MongoRepository<T>(_connectionString);
    }
}
```

### Factory Method with Configuration
```csharp
public enum DatabaseProvider
{
    SqlServer,
    PostgreSQL,
    MongoDB,
    InMemory
}

public class RepositoryFactoryProvider
{
    private readonly IConfiguration _configuration;
    
    public RepositoryFactoryProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IRepositoryFactory CreateFactory()
    {
        var provider = _configuration.GetValue<DatabaseProvider>("DatabaseProvider");
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        
        return provider switch
        {
            DatabaseProvider.SqlServer => new SqlServerRepositoryFactory(connectionString),
            DatabaseProvider.PostgreSQL => new PostgreSqlRepositoryFactory(connectionString),
            DatabaseProvider.MongoDB => new MongoRepositoryFactory(connectionString),
            DatabaseProvider.InMemory => new InMemoryRepositoryFactory(),
            _ => throw new NotSupportedException($"Database provider {provider} is not supported")
        };
    }
}
```

## Client Usage

### Basic Usage
```csharp
public class Application
{
    public void Run()
    {
        // Create different logger factories based on configuration
        LoggerFactory factory;
        
        var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
        
        factory = environment switch
        {
            "Development" => new ConsoleLoggerFactory(),
            "Production" => new FileLoggerFactory("logs/app.log"),
            "Enterprise" => new DatabaseLoggerFactory("Server=localhost;Database=Logs;"),
            _ => new ConsoleLoggerFactory()
        };
        
        // Use the factory
        factory.LogMessage("Application started");
        
        var logger = factory.CreateLogger();
        logger.Log("Processing user request");
        logger.LogError("An error occurred", new Exception("Sample exception"));
    }
}
```

### Dependency Injection Usage
```csharp
public class UserService
{
    private readonly IRepositoryFactory _repositoryFactory;
    
    public UserService(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }
    
    public async Task<User> GetUserAsync(int userId)
    {
        var userRepository = _repositoryFactory.CreateRepository<User>();
        return await userRepository.GetByIdAsync(userId);
    }
    
    public async Task SaveUserAsync(User user)
    {
        var userRepository = _repositoryFactory.CreateRepository<User>();
        await userRepository.SaveAsync(user);
    }
}

// DI Registration
public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IRepositoryFactory>(provider =>
    {
        var configuration = provider.GetService<IConfiguration>();
        var factoryProvider = new RepositoryFactoryProvider(configuration);
        return factoryProvider.CreateFactory();
    });
    
    services.AddScoped<UserService>();
}
```

## Real-World / Enterprise Use Cases

### 1. Payment Processing Factory
```csharp
public interface IPaymentProcessor
{
    Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);
    Task<RefundResult> RefundPaymentAsync(RefundRequest request);
}

public class CreditCardProcessor : IPaymentProcessor
{
    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        // Credit card processing logic
        return new PaymentResult { Success = true, TransactionId = Guid.NewGuid().ToString() };
    }
    
    public async Task<RefundResult> RefundPaymentAsync(RefundRequest request)
    {
        // Credit card refund logic
        return new RefundResult { Success = true };
    }
}

public class PayPalProcessor : IPaymentProcessor
{
    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        // PayPal processing logic
        return new PaymentResult { Success = true, TransactionId = $"PP_{Guid.NewGuid()}" };
    }
    
    public async Task<RefundResult> RefundPaymentAsync(RefundRequest request)
    {
        // PayPal refund logic
        return new RefundResult { Success = true };
    }
}

public abstract class PaymentProcessorFactory
{
    public abstract IPaymentProcessor CreateProcessor();
    
    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        var processor = CreateProcessor();
        return await processor.ProcessPaymentAsync(request);
    }
}

public class CreditCardProcessorFactory : PaymentProcessorFactory
{
    public override IPaymentProcessor CreateProcessor()
    {
        return new CreditCardProcessor();
    }
}

public class PayPalProcessorFactory : PaymentProcessorFactory
{
    public override IPaymentProcessor CreateProcessor()
    {
        return new PayPalProcessor();
    }
}
```

### 2. Document Generator Factory
```csharp
public interface IDocumentGenerator
{
    byte[] GenerateDocument(DocumentData data);
    string GetMimeType();
}

public class PdfDocumentGenerator : IDocumentGenerator
{
    public byte[] GenerateDocument(DocumentData data)
    {
        // Generate PDF using iTextSharp or similar
        return new byte[0]; // Placeholder
    }
    
    public string GetMimeType() => "application/pdf";
}

public class ExcelDocumentGenerator : IDocumentGenerator
{
    public byte[] GenerateDocument(DocumentData data)
    {
        // Generate Excel using EPPlus or similar
        return new byte[0]; // Placeholder
    }
    
    public string GetMimeType() => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
}

public class WordDocumentGenerator : IDocumentGenerator
{
    public byte[] GenerateDocument(DocumentData data)
    {
        // Generate Word document
        return new byte[0]; // Placeholder
    }
    
    public string GetMimeType() => "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
}

public class DocumentGeneratorFactory
{
    public static IDocumentGenerator CreateGenerator(DocumentType type)
    {
        return type switch
        {
            DocumentType.Pdf => new PdfDocumentGenerator(),
            DocumentType.Excel => new ExcelDocumentGenerator(),
            DocumentType.Word => new WordDocumentGenerator(),
            _ => throw new ArgumentException($"Unsupported document type: {type}")
        };
    }
}
```

### 3. Notification Service Factory
```csharp
public interface INotificationService
{
    Task SendAsync(string recipient, string subject, string message);
}

public class EmailNotificationService : INotificationService
{
    private readonly IEmailService _emailService;
    
    public EmailNotificationService(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    public async Task SendAsync(string recipient, string subject, string message)
    {
        await _emailService.SendEmailAsync(recipient, subject, message);
    }
}

public class SmsNotificationService : INotificationService
{
    private readonly ISmsService _smsService;
    
    public SmsNotificationService(ISmsService smsService)
    {
        _smsService = smsService;
    }
    
    public async Task SendAsync(string recipient, string subject, string message)
    {
        await _smsService.SendSmsAsync(recipient, $"{subject}: {message}");
    }
}

public class PushNotificationService : INotificationService
{
    private readonly IPushService _pushService;
    
    public PushNotificationService(IPushService pushService)
    {
        _pushService = pushService;
    }
    
    public async Task SendAsync(string recipient, string subject, string message)
    {
        await _pushService.SendPushAsync(recipient, subject, message);
    }
}

public class NotificationServiceFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    public NotificationServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public INotificationService CreateService(NotificationType type)
    {
        return type switch
        {
            NotificationType.Email => new EmailNotificationService(_serviceProvider.GetService<IEmailService>()),
            NotificationType.Sms => new SmsNotificationService(_serviceProvider.GetService<ISmsService>()),
            NotificationType.Push => new PushNotificationService(_serviceProvider.GetService<IPushService>()),
            _ => throw new ArgumentException($"Unsupported notification type: {type}")
        };
    }
}
```

## Pros and Cons

### Pros ✅
- **Loose coupling**: Eliminates dependency on concrete classes
- **Extensibility**: Easy to add new product types
- **Single Responsibility**: Each factory creates one type of product
- **Open/Closed Principle**: Open for extension, closed for modification
- **Testability**: Easy to mock and test different implementations

### Cons ❌
- **Complexity**: Adds extra classes and interfaces
- **Indirection**: Makes code harder to follow
- **Overhead**: May be overkill for simple object creation
- **Maintenance**: More classes to maintain

## Common Mistakes & Anti-Patterns

### 1. Overusing Factory Method
```csharp
// BAD: Using factory for simple object creation
public class StringFactory
{
    public static string CreateString(string value)
    {
        return new string(value); // Unnecessary factory
    }
}

// GOOD: Direct instantiation for simple objects
string value = "Hello World";
```

### 2. Factory with Too Many Parameters
```csharp
// BAD: Factory method with many parameters
public IService CreateService(string param1, int param2, bool param3, 
    DateTime param4, List<string> param5)
{
    // Too many parameters make it hard to use
}

// GOOD: Use configuration object or builder pattern
public IService CreateService(ServiceConfiguration config)
{
    // Cleaner interface
}
```

### 3. Not Following Open/Closed Principle
```csharp
// BAD: Modifying factory for new types
public class BadFactory
{
    public IProduct CreateProduct(ProductType type)
    {
        switch (type) // Need to modify this for new types
        {
            case ProductType.A: return new ProductA();
            case ProductType.B: return new ProductB();
            // Adding ProductC requires modifying this method
            default: throw new ArgumentException();
        }
    }
}

// GOOD: Use registry or separate factories
public class GoodFactory
{
    private readonly Dictionary<ProductType, Func<IProduct>> _factories;
    
    public GoodFactory()
    {
        _factories = new Dictionary<ProductType, Func<IProduct>>
        {
            { ProductType.A, () => new ProductA() },
            { ProductType.B, () => new ProductB() }
        };
    }
    
    public void RegisterFactory(ProductType type, Func<IProduct> factory)
    {
        _factories[type] = factory; // Can add new types without modification
    }
    
    public IProduct CreateProduct(ProductType type)
    {
        return _factories[type]();
    }
}
```

## Performance Considerations

### Object Creation Overhead
```csharp
// Consider object pooling for expensive objects
public class ExpensiveObjectFactory
{
    private readonly ConcurrentQueue<IExpensiveObject> _pool = new();
    
    public IExpensiveObject CreateObject()
    {
        if (_pool.TryDequeue(out var obj))
        {
            obj.Reset(); // Reuse existing object
            return obj;
        }
        
        return new ExpensiveObject(); // Create new if pool is empty
    }
    
    public void ReturnObject(IExpensiveObject obj)
    {
        _pool.Enqueue(obj); // Return to pool for reuse
    }
}
```

### Caching Factory Results
```csharp
public class CachedFactory
{
    private readonly ConcurrentDictionary<string, IProduct> _cache = new();
    
    public IProduct CreateProduct(string key)
    {
        return _cache.GetOrAdd(key, k => CreateNewProduct(k));
    }
    
    private IProduct CreateNewProduct(string key)
    {
        // Expensive creation logic
        return new Product(key);
    }
}
```

## Relation to SOLID Principles

### Single Responsibility Principle (SRP) ✅
Each factory is responsible for creating one type of product.

### Open/Closed Principle (OCP) ✅
Can add new products without modifying existing factory code.

### Liskov Substitution Principle (LSP) ✅
All products implement the same interface and can be substituted.

### Interface Segregation Principle (ISP) ✅
Clients depend only on the factory methods they use.

### Dependency Inversion Principle (DIP) ✅
High-level modules depend on abstractions, not concrete factories.

## Interview Questions & Answers

### Q1: What's the difference between Factory Method and Simple Factory?
**Answer:**
- **Simple Factory**: Static method that creates objects based on parameters (not a GoF pattern)
- **Factory Method**: Abstract method in base class, implemented by subclasses (GoF pattern)

```csharp
// Simple Factory
public static class SimpleFactory
{
    public static IProduct CreateProduct(ProductType type) { }
}

// Factory Method
public abstract class FactoryMethod
{
    public abstract IProduct CreateProduct(); // Subclasses implement this
}
```

### Q2: When would you use Factory Method over direct instantiation?
**Answer:**
- When object creation is complex or varies based on context
- When you need to decouple client code from concrete classes
- When you want to support different implementations at runtime
- When following dependency inversion principle

### Q3: How does Factory Method relate to Dependency Injection?
**Answer:**
Factory Method and DI solve similar problems but at different levels:
- **Factory Method**: Creates objects within application logic
- **DI**: Manages object creation and lifetime at container level
- Often used together: DI creates factories, factories create business objects

### Q4: What's the difference between Factory Method and Abstract Factory?
**Answer:**
- **Factory Method**: Creates one product type, uses inheritance
- **Abstract Factory**: Creates families of related products, uses composition

### Q5: How do you test code that uses Factory Method?
**Answer:**
```csharp
// Create mock factory for testing
public class MockLoggerFactory : LoggerFactory
{
    public override ILogger CreateLogger()
    {
        return new Mock<ILogger>().Object;
    }
}

// Or inject factory interface
public class TestableService
{
    private readonly ILoggerFactory _factory;
    
    public TestableService(ILoggerFactory factory) // Inject for testability
    {
        _factory = factory;
    }
}
```

## Quick Revision / Summary

**Intent**: Create objects without specifying exact classes
**Structure**: Abstract creator with factory method, concrete creators implement it
**Key Benefit**: Loose coupling between creator and products
**Modern Usage**: Often combined with DI containers and configuration
**Common Use Cases**: Database providers, payment processors, document generators
**SOLID**: Supports all SOLID principles when implemented correctly
**Testing**: Use mock factories or inject factory interfaces