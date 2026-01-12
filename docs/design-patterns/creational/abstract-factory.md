# Abstract Factory Pattern

## Pattern Category
**Creational Pattern**

## Intent / Definition
The Abstract Factory pattern provides an interface for creating families of related or dependent objects without specifying their concrete classes. It's a factory of factories that creates objects that belong together.

## Problem Statement
- You need to create families of related products
- Products must be used together (compatibility requirement)
- You want to enforce constraints between related objects
- System should be independent of how products are created
- You need to support multiple product families

## When to Use
✅ **Use Abstract Factory when:**
- You need to create families of related objects
- Products must work together (UI themes, database providers)
- You want to enforce product compatibility
- You need to support multiple platforms/environments

❌ **Don't use Abstract Factory when:**
- You only have one product family
- Products don't need to work together
- Simple Factory Method is sufficient
- You're overengineering simple creation

## UML Structure
```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│ AbstractFactory │    │  AbstractProductA│    │  AbstractProductB│
├─────────────────┤    ├─────────────────┤    ├─────────────────┤
│+CreateProductA()│    │                 │    │                 │
│+CreateProductB()│    └─────────────────┘    └─────────────────┘
└─────────────────┘            △                       △
        △                      │                       │
        │              ┌───────────────┐       ┌───────────────┐
┌─────────────────┐    │ ConcreteProductA1│    │ ConcreteProductB1│
│ ConcreteFactory1│    └───────────────┘       └───────────────┘
├─────────────────┤    ┌───────────────┐       ┌───────────────┐
│+CreateProductA()│────│ ConcreteProductA2│    │ ConcreteProductB2│
│+CreateProductB()│    └───────────────┘       └───────────────┘
└─────────────────┘
```

## C# Implementation

### Database Provider Family Example
```csharp
// Abstract Products
public interface IConnection
{
    Task OpenAsync();
    Task CloseAsync();
    string ConnectionString { get; }
}

public interface ICommand
{
    Task<int> ExecuteNonQueryAsync();
    Task<T> ExecuteScalarAsync<T>();
    Task<IDataReader> ExecuteReaderAsync();
}

public interface ITransaction
{
    Task CommitAsync();
    Task RollbackAsync();
}

// SQL Server Family
public class SqlConnection : IConnection
{
    public string ConnectionString { get; private set; }
    
    public SqlConnection(string connectionString)
    {
        ConnectionString = connectionString;
    }
    
    public async Task OpenAsync()
    {
        // SQL Server connection logic
        await Task.CompletedTask;
    }
    
    public async Task CloseAsync()
    {
        // SQL Server close logic
        await Task.CompletedTask;
    }
}

public class SqlCommand : ICommand
{
    public async Task<int> ExecuteNonQueryAsync()
    {
        // SQL Server command execution
        return await Task.FromResult(1);
    }
    
    public async Task<T> ExecuteScalarAsync<T>()
    {
        // SQL Server scalar execution
        return await Task.FromResult(default(T));
    }
    
    public async Task<IDataReader> ExecuteReaderAsync()
    {
        // SQL Server reader execution
        return await Task.FromResult<IDataReader>(null);
    }
}

public class SqlTransaction : ITransaction
{
    public async Task CommitAsync()
    {
        // SQL Server commit
        await Task.CompletedTask;
    }
    
    public async Task RollbackAsync()
    {
        // SQL Server rollback
        await Task.CompletedTask;
    }
}

// PostgreSQL Family
public class PostgreSqlConnection : IConnection
{
    public string ConnectionString { get; private set; }
    
    public PostgreSqlConnection(string connectionString)
    {
        ConnectionString = connectionString;
    }
    
    public async Task OpenAsync()
    {
        // PostgreSQL connection logic
        await Task.CompletedTask;
    }
    
    public async Task CloseAsync()
    {
        // PostgreSQL close logic
        await Task.CompletedTask;
    }
}

public class PostgreSqlCommand : ICommand
{
    public async Task<int> ExecuteNonQueryAsync()
    {
        // PostgreSQL command execution
        return await Task.FromResult(1);
    }
    
    public async Task<T> ExecuteScalarAsync<T>()
    {
        // PostgreSQL scalar execution
        return await Task.FromResult(default(T));
    }
    
    public async Task<IDataReader> ExecuteReaderAsync()
    {
        // PostgreSQL reader execution
        return await Task.FromResult<IDataReader>(null);
    }
}

public class PostgreSqlTransaction : ITransaction
{
    public async Task CommitAsync()
    {
        // PostgreSQL commit
        await Task.CompletedTask;
    }
    
    public async Task RollbackAsync()
    {
        // PostgreSQL rollback
        await Task.CompletedTask;
    }
}
```// 
Abstract Factory
public interface IDatabaseFactory
{
    IConnection CreateConnection();
    ICommand CreateCommand();
    ITransaction CreateTransaction();
}

// Concrete Factories
public class SqlServerFactory : IDatabaseFactory
{
    private readonly string _connectionString;
    
    public SqlServerFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
    
    public ICommand CreateCommand()
    {
        return new SqlCommand();
    }
    
    public ITransaction CreateTransaction()
    {
        return new SqlTransaction();
    }
}

public class PostgreSqlFactory : IDatabaseFactory
{
    private readonly string _connectionString;
    
    public PostgreSqlFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IConnection CreateConnection()
    {
        return new PostgreSqlConnection(_connectionString);
    }
    
    public ICommand CreateCommand()
    {
        return new PostgreSqlCommand();
    }
    
    public ITransaction CreateTransaction()
    {
        return new PostgreSqlTransaction();
    }
}

### UI Theme Family Example
```csharp
// Abstract UI Products
public interface IButton
{
    void Render();
    void Click();
}

public interface ITextBox
{
    void Render();
    string GetText();
    void SetText(string text);
}

public interface ICheckBox
{
    void Render();
    bool IsChecked { get; set; }
}

// Windows Theme Family
public class WindowsButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Rendering Windows-style button");
    }
    
    public void Click()
    {
        Console.WriteLine("Windows button clicked");
    }
}

public class WindowsTextBox : ITextBox
{
    private string _text = "";
    
    public void Render()
    {
        Console.WriteLine("Rendering Windows-style textbox");
    }
    
    public string GetText() => _text;
    public void SetText(string text) => _text = text;
}

public class WindowsCheckBox : ICheckBox
{
    public bool IsChecked { get; set; }
    
    public void Render()
    {
        Console.WriteLine($"Rendering Windows-style checkbox: {(IsChecked ? "☑" : "☐")}");
    }
}

// Mac Theme Family
public class MacButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Rendering Mac-style button");
    }
    
    public void Click()
    {
        Console.WriteLine("Mac button clicked");
    }
}

public class MacTextBox : ITextBox
{
    private string _text = "";
    
    public void Render()
    {
        Console.WriteLine("Rendering Mac-style textbox");
    }
    
    public string GetText() => _text;
    public void SetText(string text) => _text = text;
}

public class MacCheckBox : ICheckBox
{
    public bool IsChecked { get; set; }
    
    public void Render()
    {
        Console.WriteLine($"Rendering Mac-style checkbox: {(IsChecked ? "✓" : "○")}");
    }
}

// UI Factory
public interface IUIFactory
{
    IButton CreateButton();
    ITextBox CreateTextBox();
    ICheckBox CreateCheckBox();
}

public class WindowsUIFactory : IUIFactory
{
    public IButton CreateButton() => new WindowsButton();
    public ITextBox CreateTextBox() => new WindowsTextBox();
    public ICheckBox CreateCheckBox() => new WindowsCheckBox();
}

public class MacUIFactory : IUIFactory
{
    public IButton CreateButton() => new MacButton();
    public ITextBox CreateTextBox() => new MacTextBox();
    public ICheckBox CreateCheckBox() => new MacCheckBox();
}

## Client Usage

### Database Usage
```csharp
public class DataService
{
    private readonly IDatabaseFactory _databaseFactory;
    
    public DataService(IDatabaseFactory databaseFactory)
    {
        _databaseFactory = databaseFactory;
    }
    
    public async Task<User> GetUserAsync(int userId)
    {
        using var connection = _databaseFactory.CreateConnection();
        await connection.OpenAsync();
        
        using var transaction = _databaseFactory.CreateTransaction();
        try
        {
            var command = _databaseFactory.CreateCommand();
            // Use command to fetch user
            
            await transaction.CommitAsync();
            return new User(); // Placeholder
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}

// DI Registration
public void ConfigureServices(IServiceCollection services)
{
    var databaseProvider = Configuration.GetValue<string>("DatabaseProvider");
    var connectionString = Configuration.GetConnectionString("DefaultConnection");
    
    services.AddScoped<IDatabaseFactory>(provider =>
    {
        return databaseProvider.ToLower() switch
        {
            "sqlserver" => new SqlServerFactory(connectionString),
            "postgresql" => new PostgreSqlFactory(connectionString),
            _ => throw new NotSupportedException($"Database provider '{databaseProvider}' is not supported")
        };
    });
    
    services.AddScoped<DataService>();
}
```

### UI Usage
```csharp
public class Application
{
    private readonly IUIFactory _uiFactory;
    
    public Application(IUIFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }
    
    public void CreateLoginForm()
    {
        // All UI elements will be from the same family (theme)
        var usernameTextBox = _uiFactory.CreateTextBox();
        var passwordTextBox = _uiFactory.CreateTextBox();
        var rememberMeCheckBox = _uiFactory.CreateCheckBox();
        var loginButton = _uiFactory.CreateButton();
        
        // Render the form
        usernameTextBox.Render();
        passwordTextBox.Render();
        rememberMeCheckBox.Render();
        loginButton.Render();
    }
}

// Factory selection based on platform
public class UIFactoryProvider
{
    public static IUIFactory GetFactory()
    {
        var platform = Environment.OSVersion.Platform;
        
        return platform switch
        {
            PlatformID.Win32NT => new WindowsUIFactory(),
            PlatformID.MacOSX => new MacUIFactory(),
            _ => new WindowsUIFactory() // Default
        };
    }
}
```

## Real-World / Enterprise Use Cases

### 1. Cloud Provider Abstraction
```csharp
public interface ICloudStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName);
    Task<Stream> DownloadFileAsync(string fileId);
    Task DeleteFileAsync(string fileId);
}

public interface ICloudMessageQueue
{
    Task SendMessageAsync(string message);
    Task<string> ReceiveMessageAsync();
}

public interface ICloudDatabase
{
    Task<T> GetAsync<T>(string id);
    Task SaveAsync<T>(string id, T item);
}

public interface ICloudFactory
{
    ICloudStorageService CreateStorageService();
    ICloudMessageQueue CreateMessageQueue();
    ICloudDatabase CreateDatabase();
}

public class AzureCloudFactory : ICloudFactory
{
    public ICloudStorageService CreateStorageService() => new AzureBlobStorage();
    public ICloudMessageQueue CreateMessageQueue() => new AzureServiceBus();
    public ICloudDatabase CreateDatabase() => new AzureCosmosDb();
}

public class AwsCloudFactory : ICloudFactory
{
    public ICloudStorageService CreateStorageService() => new AwsS3Storage();
    public ICloudMessageQueue CreateMessageQueue() => new AwsSqs();
    public ICloudDatabase CreateDatabase() => new AwsDynamoDb();
}
```

### 2. Payment Gateway Family
```csharp
public interface IPaymentProcessor
{
    Task<PaymentResult> ProcessAsync(PaymentRequest request);
}

public interface IPaymentValidator
{
    ValidationResult Validate(PaymentRequest request);
}

public interface IPaymentNotifier
{
    Task NotifyAsync(PaymentResult result);
}

public interface IPaymentGatewayFactory
{
    IPaymentProcessor CreateProcessor();
    IPaymentValidator CreateValidator();
    IPaymentNotifier CreateNotifier();
}

public class StripeGatewayFactory : IPaymentGatewayFactory
{
    public IPaymentProcessor CreateProcessor() => new StripeProcessor();
    public IPaymentValidator CreateValidator() => new StripeValidator();
    public IPaymentNotifier CreateNotifier() => new StripeNotifier();
}

public class PayPalGatewayFactory : IPaymentGatewayFactory
{
    public IPaymentProcessor CreateProcessor() => new PayPalProcessor();
    public IPaymentValidator CreateValidator() => new PayPalValidator();
    public IPaymentNotifier CreateNotifier() => new PayPalNotifier();
}
```

## Pros and Cons

### Pros ✅
- **Product compatibility**: Ensures related objects work together
- **Consistency**: All products from same family have consistent interface
- **Isolation**: Separates client code from concrete product classes
- **Easy switching**: Can change entire product family easily
- **Single point of control**: Factory controls object creation

### Cons ❌
- **Complexity**: More interfaces and classes to maintain
- **Rigid structure**: Adding new products requires changing all factories
- **Overhead**: May be overkill for simple scenarios
- **Learning curve**: More complex than simple factory patterns

## Common Mistakes & Anti-Patterns

### 1. Breaking Product Family Consistency
```csharp
// BAD: Mixing products from different families
public class BadUIFactory : IUIFactory
{
    public IButton CreateButton() => new WindowsButton(); // Windows
    public ITextBox CreateTextBox() => new MacTextBox();  // Mac - Inconsistent!
    public ICheckBox CreateCheckBox() => new WindowsCheckBox(); // Windows
}
```

### 2. Too Many Product Types
```csharp
// BAD: Factory with too many products
public interface IBloatedFactory
{
    IProductA CreateProductA();
    IProductB CreateProductB();
    IProductC CreateProductC();
    // ... 20+ more products
}

// GOOD: Split into smaller, focused factories
public interface IUIControlFactory
{
    IButton CreateButton();
    ITextBox CreateTextBox();
}

public interface IUILayoutFactory
{
    IPanel CreatePanel();
    IGrid CreateGrid();
}
```

## Performance Considerations

### Factory Caching
```csharp
public class CachedAbstractFactory : IDatabaseFactory
{
    private readonly IDatabaseFactory _innerFactory;
    private readonly ConcurrentDictionary<Type, object> _cache = new();
    
    public CachedAbstractFactory(IDatabaseFactory innerFactory)
    {
        _innerFactory = innerFactory;
    }
    
    public IConnection CreateConnection()
    {
        return (IConnection)_cache.GetOrAdd(typeof(IConnection), 
            _ => _innerFactory.CreateConnection());
    }
    
    // Similar for other products...
}
```

## Relation to SOLID Principles

### Single Responsibility Principle (SRP) ✅
Each factory is responsible for creating one product family.

### Open/Closed Principle (OCP) ⚠️
Adding new products requires modifying all factories (violation).

### Liskov Substitution Principle (LSP) ✅
All factories can be substituted for the abstract factory.

### Interface Segregation Principle (ISP) ✅
Clients depend only on the factory methods they use.

### Dependency Inversion Principle (DIP) ✅
Clients depend on abstract factory, not concrete implementations.

## Interview Questions & Answers

### Q1: What's the difference between Factory Method and Abstract Factory?
**Answer:**
- **Factory Method**: Creates one product type, uses inheritance
- **Abstract Factory**: Creates families of related products, uses composition

### Q2: How do you handle adding new products to Abstract Factory?
**Answer:**
Adding new products requires modifying all existing factories, which violates OCP. Solutions:
- Use Builder pattern for complex product creation
- Implement factory registration system
- Consider if Abstract Factory is the right pattern

### Q3: When would you choose Abstract Factory over Factory Method?
**Answer:**
- When you need to create families of related objects
- When products must work together (compatibility requirement)
- When you need to enforce constraints between objects
- When supporting multiple platforms/themes

### Q4: How do you test Abstract Factory implementations?
**Answer:**
```csharp
// Create test factory for unit testing
public class TestDatabaseFactory : IDatabaseFactory
{
    public IConnection CreateConnection() => new Mock<IConnection>().Object;
    public ICommand CreateCommand() => new Mock<ICommand>().Object;
    public ITransaction CreateTransaction() => new Mock<ITransaction>().Object;
}
```

### Q5: What are the alternatives to Abstract Factory?
**Answer:**
- **Dependency Injection**: Let DI container manage object creation
- **Builder Pattern**: For complex object construction
- **Factory Method**: When you only need one product type
- **Service Locator**: For runtime service resolution

## Quick Revision / Summary

**Intent**: Create families of related objects without specifying concrete classes
**Structure**: Abstract factory interface, concrete factories, product families
**Key Benefit**: Ensures product compatibility within families
**Main Challenge**: Adding new products requires changing all factories
**Best Use**: UI themes, database providers, cloud services, payment gateways
**Modern Alternative**: Often replaced by DI containers in .NET applications