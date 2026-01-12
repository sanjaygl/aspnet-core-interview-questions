# C# Design Patterns - Complete Guide

## Table of Contents
1. [Introduction to Design Patterns](#introduction-to-design-patterns)
2. [Creational Patterns](#creational-patterns)
3. [Structural Patterns](#structural-patterns)
4. [Behavioral Patterns](#behavioral-patterns)
5. [Modern C# Patterns](#modern-c-patterns)
6. [Anti-Patterns to Avoid](#anti-patterns-to-avoid)
7. [Interview Questions](#interview-questions)
8. [Quick Reference](#quick-reference)

---

## Introduction to Design Patterns

### What are Design Patterns?

Design patterns are reusable solutions to commonly occurring problems in software design. They represent best practices and provide a common vocabulary for developers.

**Benefits:**
- **Reusability**: Proven solutions that can be applied across projects
- **Communication**: Common vocabulary among developers
- **Best Practices**: Encapsulate expert knowledge and experience
- **Maintainability**: Well-structured, predictable code organization

### Gang of Four (GoF) Classification

```
Design Patterns (23 GoF Patterns)
├── Creational (5) - Object creation mechanisms
│   ├── Singleton
│   ├── Factory Method
│   ├── Abstract Factory
│   ├── Builder
│   └── Prototype
├── Structural (7) - Object composition
│   ├── Adapter
│   ├── Bridge
│   ├── Composite
│   ├── Decorator
│   ├── Facade
│   ├── Flyweight
│   └── Proxy
└── Behavioral (11) - Object interaction and responsibilities
    ├── Chain of Responsibility
    ├── Command
    ├── Iterator
    ├── Mediator
    ├── Memento
    ├── Observer
    ├── State
    ├── Strategy
    ├── Template Method
    ├── Visitor
    └── Interpreter
```

---

## Creational Patterns

### 1. Singleton Pattern

**Intent:** Ensure a class has only one instance and provide global access to it.

**When to Use:**
- Database connections
- Logging services
- Configuration settings
- Cache managers

```csharp
// Thread-safe Singleton implementation
public sealed class DatabaseConnection
{
    private static readonly Lazy<DatabaseConnection> _instance = 
        new Lazy<DatabaseConnection>(() => new DatabaseConnection());
    
    private readonly string _connectionString;
    
    private DatabaseConnection()
    {
        _connectionString = "Server=localhost;Database=MyApp;";
        Console.WriteLine("Database connection initialized");
    }
    
    public static DatabaseConnection Instance => _instance.Value;
    
    public void ExecuteQuery(string query)
    {
        Console.WriteLine($"Executing: {query} on {_connectionString}");
    }
}

// Usage
var db1 = DatabaseConnection.Instance;
var db2 = DatabaseConnection.Instance;
Console.WriteLine(ReferenceEquals(db1, db2)); // True - same instance

// Modern C# approach with Dependency Injection
public interface IDatabaseConnection
{
    void ExecuteQuery(string query);
}

public class DatabaseConnectionService : IDatabaseConnection
{
    private readonly string _connectionString;
    
    public DatabaseConnectionService(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default");
    }
    
    public void ExecuteQuery(string query)
    {
        Console.WriteLine($"Executing: {query}");
    }
}

// Register as singleton in DI container
services.AddSingleton<IDatabaseConnection, DatabaseConnectionService>();
```

### 2. Factory Method Pattern

**Intent:** Create objects without specifying their exact classes.

```csharp
// Product hierarchy
public abstract class Document
{
    public abstract void Open();
    public abstract void Save();
}

public class WordDocument : Document
{
    public override void Open() => Console.WriteLine("Opening Word document");
    public override void Save() => Console.WriteLine("Saving Word document");
}

public class PdfDocument : Document
{
    public override void Open() => Console.WriteLine("Opening PDF document");
    public override void Save() => Console.WriteLine("Saving PDF document");
}

public class ExcelDocument : Document
{
    public override void Open() => Console.WriteLine("Opening Excel document");
    public override void Save() => Console.WriteLine("Saving Excel document");
}

// Creator hierarchy
public abstract class DocumentCreator
{
    // Factory method
    public abstract Document CreateDocument();
    
    // Template method using factory method
    public void ProcessDocument()
    {
        var document = CreateDocument();
        document.Open();
        // Process document
        document.Save();
    }
}

public class WordDocumentCreator : DocumentCreator
{
    public override Document CreateDocument() => new WordDocument();
}

public class PdfDocumentCreator : DocumentCreator
{
    public override Document CreateDocument() => new PdfDocument();
}

// Modern approach with generics and delegates
public class DocumentFactory
{
    private readonly Dictionary<string, Func<Document>> _creators;
    
    public DocumentFactory()
    {
        _creators = new Dictionary<string, Func<Document>>
        {
            ["word"] = () => new WordDocument(),
            ["pdf"] = () => new PdfDocument(),
            ["excel"] = () => new ExcelDocument()
        };
    }
    
    public Document CreateDocument(string type)
    {
        if (_creators.TryGetValue(type.ToLower(), out var creator))
        {
            return creator();
        }
        throw new ArgumentException($"Unknown document type: {type}");
    }
    
    public void RegisterCreator<T>(string type) where T : Document, new()
    {
        _creators[type.ToLower()] = () => new T();
    }
}

// Usage
var factory = new DocumentFactory();
var wordDoc = factory.CreateDocument("word");
wordDoc.Open();

// Register new type
factory.RegisterCreator<ExcelDocument>("spreadsheet");
var excelDoc = factory.CreateDocument("spreadsheet");
```

### 3. Abstract Factory Pattern

**Intent:** Provide an interface for creating families of related objects.

```csharp
// Abstract products
public interface IButton
{
    void Render();
    void Click();
}

public interface ITextBox
{
    void Render();
    string GetText();
}

// Windows implementations
public class WindowsButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Windows button");
    public void Click() => Console.WriteLine("Windows button clicked");
}

public class WindowsTextBox : ITextBox
{
    public void Render() => Console.WriteLine("Rendering Windows textbox");
    public string GetText() => "Windows text";
}

// Mac implementations
public class MacButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Mac button");
    public void Click() => Console.WriteLine("Mac button clicked");
}

public class MacTextBox : ITextBox
{
    public void Render() => Console.WriteLine("Rendering Mac textbox");
    public string GetText() => "Mac text";
}

// Abstract factory
public interface IUIFactory
{
    IButton CreateButton();
    ITextBox CreateTextBox();
}

// Concrete factories
public class WindowsUIFactory : IUIFactory
{
    public IButton CreateButton() => new WindowsButton();
    public ITextBox CreateTextBox() => new WindowsTextBox();
}

public class MacUIFactory : IUIFactory
{
    public IButton CreateButton() => new MacButton();
    public ITextBox CreateTextBox() => new MacTextBox();
}

// Client code
public class Application
{
    private readonly IUIFactory _uiFactory;
    
    public Application(IUIFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }
    
    public void CreateUI()
    {
        var button = _uiFactory.CreateButton();
        var textBox = _uiFactory.CreateTextBox();
        
        button.Render();
        textBox.Render();
    }
}

// Usage
IUIFactory factory = Environment.OSVersion.Platform == PlatformID.Win32NT 
    ? new WindowsUIFactory() 
    : new MacUIFactory();

var app = new Application(factory);
app.CreateUI();

// Modern approach with dependency injection
public class UIFactoryProvider
{
    public static IUIFactory GetFactory(string platform)
    {
        return platform.ToLower() switch
        {
            "windows" => new WindowsUIFactory(),
            "mac" => new MacUIFactory(),
            _ => throw new NotSupportedException($"Platform {platform} not supported")
        };
    }
}
```

### 4. Builder Pattern

**Intent:** Construct complex objects step by step.

```csharp
// Product
public class Computer
{
    public string CPU { get; set; }
    public string RAM { get; set; }
    public string Storage { get; set; }
    public string GPU { get; set; }
    public bool HasWiFi { get; set; }
    public bool HasBluetooth { get; set; }
    
    public override string ToString()
    {
        return $"Computer: CPU={CPU}, RAM={RAM}, Storage={Storage}, GPU={GPU}, WiFi={HasWiFi}, Bluetooth={HasBluetooth}";
    }
}

// Traditional Builder
public class ComputerBuilder
{
    private readonly Computer _computer = new Computer();
    
    public ComputerBuilder SetCPU(string cpu)
    {
        _computer.CPU = cpu;
        return this;
    }
    
    public ComputerBuilder SetRAM(string ram)
    {
        _computer.RAM = ram;
        return this;
    }
    
    public ComputerBuilder SetStorage(string storage)
    {
        _computer.Storage = storage;
        return this;
    }
    
    public ComputerBuilder SetGPU(string gpu)
    {
        _computer.GPU = gpu;
        return this;
    }
    
    public ComputerBuilder EnableWiFi()
    {
        _computer.HasWiFi = true;
        return this;
    }
    
    public ComputerBuilder EnableBluetooth()
    {
        _computer.HasBluetooth = true;
        return this;
    }
    
    public Computer Build() => _computer;
}

// Usage
var gamingPC = new ComputerBuilder()
    .SetCPU("Intel i9-12900K")
    .SetRAM("32GB DDR4")
    .SetStorage("1TB NVMe SSD")
    .SetGPU("RTX 4080")
    .EnableWiFi()
    .EnableBluetooth()
    .Build();

// Modern C# approach with records and init-only properties
public record ComputerSpec
{
    public string CPU { get; init; }
    public string RAM { get; init; }
    public string Storage { get; init; }
    public string GPU { get; init; }
    public bool HasWiFi { get; init; }
    public bool HasBluetooth { get; init; }
}

// Functional builder approach
public static class ComputerSpecBuilder
{
    public static ComputerSpec Create() => new ComputerSpec();
    
    public static ComputerSpec WithCPU(this ComputerSpec spec, string cpu) 
        => spec with { CPU = cpu };
    
    public static ComputerSpec WithRAM(this ComputerSpec spec, string ram) 
        => spec with { RAM = ram };
    
    public static ComputerSpec WithStorage(this ComputerSpec spec, string storage) 
        => spec with { Storage = storage };
    
    public static ComputerSpec WithGPU(this ComputerSpec spec, string gpu) 
        => spec with { GPU = gpu };
    
    public static ComputerSpec WithWiFi(this ComputerSpec spec) 
        => spec with { HasWiFi = true };
    
    public static ComputerSpec WithBluetooth(this ComputerSpec spec) 
        => spec with { HasBluetooth = true };
}

// Usage with modern approach
var modernPC = ComputerSpecBuilder.Create()
    .WithCPU("AMD Ryzen 9 7950X")
    .WithRAM("64GB DDR5")
    .WithStorage("2TB NVMe SSD")
    .WithGPU("RTX 4090")
    .WithWiFi()
    .WithBluetooth();

// Director pattern for predefined configurations
public class ComputerDirector
{
    public Computer BuildGamingComputer(ComputerBuilder builder)
    {
        return builder
            .SetCPU("Intel i7-12700K")
            .SetRAM("16GB DDR4")
            .SetStorage("1TB NVMe SSD")
            .SetGPU("RTX 4070")
            .EnableWiFi()
            .Build();
    }
    
    public Computer BuildOfficeComputer(ComputerBuilder builder)
    {
        return builder
            .SetCPU("Intel i5-12400")
            .SetRAM("8GB DDR4")
            .SetStorage("512GB SSD")
            .EnableWiFi()
            .EnableBluetooth()
            .Build();
    }
}
```

### 5. Prototype Pattern

**Intent:** Create objects by cloning existing instances.

```csharp
// ICloneable approach (not recommended for deep cloning)
public abstract class Shape : ICloneable
{
    public string Color { get; set; }
    public abstract void Draw();
    public abstract object Clone();
}

public class Circle : Shape
{
    public int Radius { get; set; }
    
    public override void Draw()
    {
        Console.WriteLine($"Drawing {Color} circle with radius {Radius}");
    }
    
    public override object Clone()
    {
        return new Circle 
        { 
            Color = this.Color, 
            Radius = this.Radius 
        };
    }
}

public class Rectangle : Shape
{
    public int Width { get; set; }
    public int Height { get; set; }
    
    public override void Draw()
    {
        Console.WriteLine($"Drawing {Color} rectangle {Width}x{Height}");
    }
    
    public override object Clone()
    {
        return new Rectangle 
        { 
            Color = this.Color, 
            Width = this.Width, 
            Height = this.Height 
        };
    }
}

// Modern approach with generic cloning
public interface IPrototype<T>
{
    T Clone();
}

public class Document : IPrototype<Document>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public DateTime CreatedDate { get; set; }
    
    public Document Clone()
    {
        return new Document
        {
            Title = this.Title,
            Content = this.Content,
            Tags = new List<string>(this.Tags), // Deep copy of list
            CreatedDate = this.CreatedDate
        };
    }
}

// Prototype registry
public class DocumentRegistry
{
    private readonly Dictionary<string, Document> _prototypes = new();
    
    public void RegisterPrototype(string key, Document prototype)
    {
        _prototypes[key] = prototype;
    }
    
    public Document CreateDocument(string type)
    {
        if (_prototypes.TryGetValue(type, out var prototype))
        {
            return prototype.Clone();
        }
        throw new ArgumentException($"Unknown document type: {type}");
    }
}

// Usage
var registry = new DocumentRegistry();

// Register prototypes
registry.RegisterPrototype("report", new Document 
{ 
    Title = "Monthly Report Template",
    Content = "Report content template...",
    Tags = new List<string> { "report", "monthly" }
});

registry.RegisterPrototype("memo", new Document 
{ 
    Title = "Memo Template",
    Content = "Memo content template...",
    Tags = new List<string> { "memo", "internal" }
});

// Create documents from prototypes
var report1 = registry.CreateDocument("report");
report1.Title = "January Report";
report1.CreatedDate = DateTime.Now;

var report2 = registry.CreateDocument("report");
report2.Title = "February Report";
report2.CreatedDate = DateTime.Now;

// Deep cloning with serialization (for complex objects)
public static class DeepCloner
{
    public static T DeepClone<T>(T obj) where T : class
    {
        if (obj == null) return null;
        
        var json = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(json);
    }
}

// Usage
var originalDoc = new Document 
{ 
    Title = "Original", 
    Tags = new List<string> { "tag1", "tag2" } 
};

var clonedDoc = DeepCloner.DeepClone(originalDoc);
clonedDoc.Tags.Add("tag3"); // Won't affect original
```

---

## Structural Patterns

### 1. Adapter Pattern

**Intent:** Allow incompatible interfaces to work together.

```csharp
// Target interface (what client expects)
public interface IMediaPlayer
{
    void Play(string audioType, string fileName);
}

// Adaptee (existing class with incompatible interface)
public class AdvancedMediaPlayer
{
    public void PlayVlc(string fileName)
    {
        Console.WriteLine($"Playing vlc file: {fileName}");
    }
    
    public void PlayMp4(string fileName)
    {
        Console.WriteLine($"Playing mp4 file: {fileName}");
    }
}

// Adapter
public class MediaAdapter : IMediaPlayer
{
    private readonly AdvancedMediaPlayer _advancedPlayer;
    
    public MediaAdapter()
    {
        _advancedPlayer = new AdvancedMediaPlayer();
    }
    
    public void Play(string audioType, string fileName)
    {
        switch (audioType.ToLower())
        {
            case "vlc":
                _advancedPlayer.PlayVlc(fileName);
                break;
            case "mp4":
                _advancedPlayer.PlayMp4(fileName);
                break;
            default:
                throw new NotSupportedException($"Audio type {audioType} not supported");
        }
    }
}

// Client
public class AudioPlayer : IMediaPlayer
{
    private readonly MediaAdapter _adapter;
    
    public AudioPlayer()
    {
        _adapter = new MediaAdapter();
    }
    
    public void Play(string audioType, string fileName)
    {
        if (audioType.ToLower() == "mp3")
        {
            Console.WriteLine($"Playing mp3 file: {fileName}");
        }
        else
        {
            _adapter.Play(audioType, fileName);
        }
    }
}

// Real-world example: Database adapter
public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task SaveAsync(T entity);
}

// Legacy database service
public class LegacyDatabaseService
{
    public DataTable GetUserById(int id)
    {
        // Legacy database call returning DataTable
        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Rows.Add(id, $"User {id}");
        return table;
    }
    
    public DataTable GetAllUsers()
    {
        // Legacy implementation
        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Name", typeof(string));
        for (int i = 1; i <= 5; i++)
        {
            table.Rows.Add(i, $"User {i}");
        }
        return table;
    }
}

// Modern entity
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}

// Adapter for legacy service
public class LegacyUserRepositoryAdapter : IRepository<User>
{
    private readonly LegacyDatabaseService _legacyService;
    
    public LegacyUserRepositoryAdapter(LegacyDatabaseService legacyService)
    {
        _legacyService = legacyService;
    }
    
    public async Task<User> GetByIdAsync(int id)
    {
        var table = _legacyService.GetUserById(id);
        if (table.Rows.Count > 0)
        {
            var row = table.Rows[0];
            return new User 
            { 
                Id = (int)row["Id"], 
                Name = (string)row["Name"] 
            };
        }
        return null;
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var table = _legacyService.GetAllUsers();
        var users = new List<User>();
        
        foreach (DataRow row in table.Rows)
        {
            users.Add(new User 
            { 
                Id = (int)row["Id"], 
                Name = (string)row["Name"] 
            });
        }
        
        return users;
    }
    
    public async Task SaveAsync(User entity)
    {
        // Adapt to legacy save method
        throw new NotImplementedException("Legacy service doesn't support async save");
    }
}

// Usage
var legacyService = new LegacyDatabaseService();
var adapter = new LegacyUserRepositoryAdapter(legacyService);

var user = await adapter.GetByIdAsync(1);
var allUsers = await adapter.GetAllAsync();
```

### 2. Decorator Pattern

**Intent:** Add new functionality to objects dynamically without altering their structure.

```csharp
// Component interface
public interface ICoffee
{
    string GetDescription();
    decimal GetCost();
}

// Concrete component
public class SimpleCoffee : ICoffee
{
    public string GetDescription() => "Simple coffee";
    public decimal GetCost() => 2.00m;
}

// Base decorator
public abstract class CoffeeDecorator : ICoffee
{
    protected readonly ICoffee _coffee;
    
    protected CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }
    
    public virtual string GetDescription() => _coffee.GetDescription();
    public virtual decimal GetCost() => _coffee.GetCost();
}

// Concrete decorators
public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee) { }
    
    public override string GetDescription() => $"{_coffee.GetDescription()}, Milk";
    public override decimal GetCost() => _coffee.GetCost() + 0.50m;
}

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee) { }
    
    public override string GetDescription() => $"{_coffee.GetDescription()}, Sugar";
    public override decimal GetCost() => _coffee.GetCost() + 0.25m;
}

public class WhipDecorator : CoffeeDecorator
{
    public WhipDecorator(ICoffee coffee) : base(coffee) { }
    
    public override string GetDescription() => $"{_coffee.GetDescription()}, Whip";
    public override decimal GetCost() => _coffee.GetCost() + 0.75m;
}

// Usage
ICoffee coffee = new SimpleCoffee();
Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost()}");

coffee = new MilkDecorator(coffee);
Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost()}");

coffee = new SugarDecorator(coffee);
coffee = new WhipDecorator(coffee);
Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost()}");
// Output: Simple coffee, Milk, Sugar, Whip - $3.50

// Real-world example: Logging decorator
public interface IDataService
{
    Task<string> GetDataAsync(int id);
    Task SaveDataAsync(int id, string data);
}

public class DatabaseService : IDataService
{
    public async Task<string> GetDataAsync(int id)
    {
        await Task.Delay(100); // Simulate database call
        return $"Data for ID {id}";
    }
    
    public async Task SaveDataAsync(int id, string data)
    {
        await Task.Delay(50); // Simulate database save
        Console.WriteLine($"Saved data for ID {id}: {data}");
    }
}

public class LoggingDecorator : IDataService
{
    private readonly IDataService _dataService;
    private readonly ILogger _logger;
    
    public LoggingDecorator(IDataService dataService, ILogger logger)
    {
        _dataService = dataService;
        _logger = logger;
    }
    
    public async Task<string> GetDataAsync(int id)
    {
        _logger.LogInformation($"Getting data for ID {id}");
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            var result = await _dataService.GetDataAsync(id);
            _logger.LogInformation($"Successfully retrieved data for ID {id} in {stopwatch.ElapsedMilliseconds}ms");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting data for ID {id}");
            throw;
        }
    }
    
    public async Task SaveDataAsync(int id, string data)
    {
        _logger.LogInformation($"Saving data for ID {id}");
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            await _dataService.SaveDataAsync(id, data);
            _logger.LogInformation($"Successfully saved data for ID {id} in {stopwatch.ElapsedMilliseconds}ms");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error saving data for ID {id}");
            throw;
        }
    }
}

public class CachingDecorator : IDataService
{
    private readonly IDataService _dataService;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheExpiry;
    
    public CachingDecorator(IDataService dataService, IMemoryCache cache, TimeSpan cacheExpiry)
    {
        _dataService = dataService;
        _cache = cache;
        _cacheExpiry = cacheExpiry;
    }
    
    public async Task<string> GetDataAsync(int id)
    {
        var cacheKey = $"data_{id}";
        
        if (_cache.TryGetValue(cacheKey, out string cachedData))
        {
            return cachedData;
        }
        
        var data = await _dataService.GetDataAsync(id);
        _cache.Set(cacheKey, data, _cacheExpiry);
        return data;
    }
    
    public async Task SaveDataAsync(int id, string data)
    {
        await _dataService.SaveDataAsync(id, data);
        
        // Invalidate cache
        var cacheKey = $"data_{id}";
        _cache.Remove(cacheKey);
    }
}

// Chaining decorators
IDataService service = new DatabaseService();
service = new LoggingDecorator(service, logger);
service = new CachingDecorator(service, cache, TimeSpan.FromMinutes(5));

var data = await service.GetDataAsync(123);
```

### 3. Facade Pattern

**Intent:** Provide a simplified interface to a complex subsystem.

```csharp
// Complex subsystem classes
public class CPU
{
    public void Freeze() => Console.WriteLine("CPU: Freezing processor");
    public void Jump(long position) => Console.WriteLine($"CPU: Jumping to position {position}");
    public void Execute() => Console.WriteLine("CPU: Executing instructions");
}

public class Memory
{
    public void Load(long position, byte[] data) 
        => Console.WriteLine($"Memory: Loading {data.Length} bytes at position {position}");
}

public class HardDrive
{
    public byte[] Read(long lba, int size)
    {
        Console.WriteLine($"HardDrive: Reading {size} bytes from LBA {lba}");
        return new byte[size];
    }
}

// Facade
public class ComputerFacade
{
    private readonly CPU _cpu;
    private readonly Memory _memory;
    private readonly HardDrive _hardDrive;
    
    public ComputerFacade()
    {
        _cpu = new CPU();
        _memory = new Memory();
        _hardDrive = new HardDrive();
    }
    
    public void StartComputer()
    {
        Console.WriteLine("Starting computer...");
        _cpu.Freeze();
        
        var bootData = _hardDrive.Read(0, 1024);
        _memory.Load(0, bootData);
        
        _cpu.Jump(0);
        _cpu.Execute();
        
        Console.WriteLine("Computer started successfully!");
    }
}

// Usage
var computer = new ComputerFacade();
computer.StartComputer(); // Simple interface hides complexity

// Real-world example: Payment processing facade
public class PaymentGateway
{
    public bool ProcessPayment(decimal amount, string cardNumber)
    {
        Console.WriteLine($"Processing payment of ${amount} with card {cardNumber}");
        return true;
    }
}

public class InventoryService
{
    public bool CheckStock(int productId, int quantity)
    {
        Console.WriteLine($"Checking stock for product {productId}, quantity {quantity}");
        return true;
    }
    
    public void ReserveItems(int productId, int quantity)
    {
        Console.WriteLine($"Reserving {quantity} items of product {productId}");
    }
}

public class ShippingService
{
    public string CreateShipment(string address, List<int> productIds)
    {
        Console.WriteLine($"Creating shipment to {address} for products: {string.Join(", ", productIds)}");
        return "SHIP123456";
    }
}

public class NotificationService
{
    public void SendOrderConfirmation(string email, string orderId)
    {
        Console.WriteLine($"Sending order confirmation for {orderId} to {email}");
    }
}

// Facade for e-commerce order processing
public class OrderProcessingFacade
{
    private readonly PaymentGateway _paymentGateway;
    private readonly InventoryService _inventoryService;
    private readonly ShippingService _shippingService;
    private readonly NotificationService _notificationService;
    
    public OrderProcessingFacade()
    {
        _paymentGateway = new PaymentGateway();
        _inventoryService = new InventoryService();
        _shippingService = new ShippingService();
        _notificationService = new NotificationService();
    }
    
    public async Task<OrderResult> ProcessOrderAsync(OrderRequest request)
    {
        try
        {
            // Check inventory
            foreach (var item in request.Items)
            {
                if (!_inventoryService.CheckStock(item.ProductId, item.Quantity))
                {
                    return new OrderResult { Success = false, Message = "Insufficient stock" };
                }
            }
            
            // Process payment
            if (!_paymentGateway.ProcessPayment(request.TotalAmount, request.CardNumber))
            {
                return new OrderResult { Success = false, Message = "Payment failed" };
            }
            
            // Reserve inventory
            foreach (var item in request.Items)
            {
                _inventoryService.ReserveItems(item.ProductId, item.Quantity);
            }
            
            // Create shipment
            var productIds = request.Items.Select(i => i.ProductId).ToList();
            var shipmentId = _shippingService.CreateShipment(request.ShippingAddress, productIds);
            
            // Send confirmation
            _notificationService.SendOrderConfirmation(request.CustomerEmail, request.OrderId);
            
            return new OrderResult 
            { 
                Success = true, 
                OrderId = request.OrderId,
                ShipmentId = shipmentId,
                Message = "Order processed successfully" 
            };
        }
        catch (Exception ex)
        {
            return new OrderResult { Success = false, Message = ex.Message };
        }
    }
}

public class OrderRequest
{
    public string OrderId { get; set; }
    public string CustomerEmail { get; set; }
    public string ShippingAddress { get; set; }
    public string CardNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}

public class OrderItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderResult
{
    public bool Success { get; set; }
    public string OrderId { get; set; }
    public string ShipmentId { get; set; }
    public string Message { get; set; }
}

// Usage
var orderFacade = new OrderProcessingFacade();
var orderRequest = new OrderRequest
{
    OrderId = "ORD123",
    CustomerEmail = "customer@example.com",
    ShippingAddress = "123 Main St",
    CardNumber = "1234-5678-9012-3456",
    TotalAmount = 99.99m,
    Items = new List<OrderItem>
    {
        new OrderItem { ProductId = 1, Quantity = 2 },
        new OrderItem { ProductId = 2, Quantity = 1 }
    }
};

var result = await orderFacade.ProcessOrderAsync(orderRequest);
Console.WriteLine($"Order result: {result.Success} - {result.Message}");
```---

#
# Behavioral Patterns

### 1. Observer Pattern

**Intent:** Define a one-to-many dependency between objects so that when one object changes state, all dependents are notified.

```csharp
// Traditional Observer Pattern
public interface IObserver<T>
{
    void Update(T data);
}

public interface ISubject<T>
{
    void Attach(IObserver<T> observer);
    void Detach(IObserver<T> observer);
    void Notify(T data);
}

public class WeatherStation : ISubject<WeatherData>
{
    private readonly List<IObserver<WeatherData>> _observers = new();
    private WeatherData _currentWeather;
    
    public void Attach(IObserver<WeatherData> observer)
    {
        _observers.Add(observer);
    }
    
    public void Detach(IObserver<WeatherData> observer)
    {
        _observers.Remove(observer);
    }
    
    public void Notify(WeatherData data)
    {
        foreach (var observer in _observers)
        {
            observer.Update(data);
        }
    }
    
    public void SetWeatherData(float temperature, float humidity, float pressure)
    {
        _currentWeather = new WeatherData(temperature, humidity, pressure);
        Notify(_currentWeather);
    }
}

public record WeatherData(float Temperature, float Humidity, float Pressure);

public class CurrentConditionsDisplay : IObserver<WeatherData>
{
    public void Update(WeatherData data)
    {
        Console.WriteLine($"Current conditions: {data.Temperature}°F, {data.Humidity}% humidity, {data.Pressure} pressure");
    }
}

public class StatisticsDisplay : IObserver<WeatherData>
{
    private readonly List<float> _temperatures = new();
    
    public void Update(WeatherData data)
    {
        _temperatures.Add(data.Temperature);
        var avg = _temperatures.Average();
        var min = _temperatures.Min();
        var max = _temperatures.Max();
        
        Console.WriteLine($"Statistics: Avg {avg:F1}°F, Min {min}°F, Max {max}°F");
    }
}

// Modern C# approach with events
public class ModernWeatherStation
{
    public event Action<WeatherData> WeatherChanged;
    
    public void SetWeatherData(float temperature, float humidity, float pressure)
    {
        var weatherData = new WeatherData(temperature, humidity, pressure);
        WeatherChanged?.Invoke(weatherData);
    }
}

// Usage with events
var weatherStation = new ModernWeatherStation();

weatherStation.WeatherChanged += data => 
    Console.WriteLine($"Display 1: {data.Temperature}°F");

weatherStation.WeatherChanged += data => 
    Console.WriteLine($"Display 2: Humidity {data.Humidity}%");

weatherStation.SetWeatherData(75.5f, 65.0f, 30.2f);

// Real-world example: Stock price monitoring
public class Stock
{
    private decimal _price;
    public string Symbol { get; }
    public decimal Price 
    { 
        get => _price;
        set
        {
            if (_price != value)
            {
                var oldPrice = _price;
                _price = value;
                PriceChanged?.Invoke(new StockPriceChangedEventArgs(Symbol, oldPrice, value));
            }
        }
    }
    
    public event Action<StockPriceChangedEventArgs> PriceChanged;
    
    public Stock(string symbol, decimal initialPrice)
    {
        Symbol = symbol;
        _price = initialPrice;
    }
}

public class StockPriceChangedEventArgs
{
    public string Symbol { get; }
    public decimal OldPrice { get; }
    public decimal NewPrice { get; }
    public decimal Change => NewPrice - OldPrice;
    public decimal ChangePercent => OldPrice != 0 ? (Change / OldPrice) * 100 : 0;
    
    public StockPriceChangedEventArgs(string symbol, decimal oldPrice, decimal newPrice)
    {
        Symbol = symbol;
        OldPrice = oldPrice;
        NewPrice = newPrice;
    }
}

public class StockPortfolio
{
    private readonly Dictionary<string, Stock> _stocks = new();
    
    public void AddStock(Stock stock)
    {
        _stocks[stock.Symbol] = stock;
        stock.PriceChanged += OnStockPriceChanged;
    }
    
    public void RemoveStock(string symbol)
    {
        if (_stocks.TryGetValue(symbol, out var stock))
        {
            stock.PriceChanged -= OnStockPriceChanged;
            _stocks.Remove(symbol);
        }
    }
    
    private void OnStockPriceChanged(StockPriceChangedEventArgs e)
    {
        Console.WriteLine($"Portfolio Alert: {e.Symbol} changed from ${e.OldPrice} to ${e.NewPrice} ({e.ChangePercent:F2}%)");
    }
}

// Usage
var portfolio = new StockPortfolio();
var appleStock = new Stock("AAPL", 150.00m);
var microsoftStock = new Stock("MSFT", 300.00m);

portfolio.AddStock(appleStock);
portfolio.AddStock(microsoftStock);

appleStock.Price = 155.50m; // Triggers notification
microsoftStock.Price = 295.75m; // Triggers notification
```

### 2. Strategy Pattern

**Intent:** Define a family of algorithms, encapsulate each one, and make them interchangeable.

```csharp
// Strategy interface
public interface IPaymentStrategy
{
    PaymentResult ProcessPayment(decimal amount);
}

// Concrete strategies
public class CreditCardPayment : IPaymentStrategy
{
    private readonly string _cardNumber;
    private readonly string _cvv;
    
    public CreditCardPayment(string cardNumber, string cvv)
    {
        _cardNumber = cardNumber;
        _cvv = cvv;
    }
    
    public PaymentResult ProcessPayment(decimal amount)
    {
        // Credit card processing logic
        Console.WriteLine($"Processing ${amount} via Credit Card ending in {_cardNumber.Substring(_cardNumber.Length - 4)}");
        
        // Simulate processing
        if (amount <= 10000)
        {
            return new PaymentResult { Success = true, TransactionId = Guid.NewGuid().ToString() };
        }
        
        return new PaymentResult { Success = false, ErrorMessage = "Amount exceeds credit limit" };
    }
}

public class PayPalPayment : IPaymentStrategy
{
    private readonly string _email;
    
    public PayPalPayment(string email)
    {
        _email = email;
    }
    
    public PaymentResult ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing ${amount} via PayPal account {_email}");
        
        // PayPal processing logic
        return new PaymentResult { Success = true, TransactionId = $"PP_{Guid.NewGuid()}" };
    }
}

public class BankTransferPayment : IPaymentStrategy
{
    private readonly string _accountNumber;
    
    public BankTransferPayment(string accountNumber)
    {
        _accountNumber = accountNumber;
    }
    
    public PaymentResult ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing ${amount} via Bank Transfer from account {_accountNumber}");
        
        // Bank transfer processing logic
        return new PaymentResult { Success = true, TransactionId = $"BT_{Guid.NewGuid()}" };
    }
}

public class PaymentResult
{
    public bool Success { get; set; }
    public string TransactionId { get; set; }
    public string ErrorMessage { get; set; }
}

// Context
public class PaymentProcessor
{
    private IPaymentStrategy _paymentStrategy;
    
    public void SetPaymentStrategy(IPaymentStrategy strategy)
    {
        _paymentStrategy = strategy;
    }
    
    public PaymentResult ProcessPayment(decimal amount)
    {
        if (_paymentStrategy == null)
        {
            throw new InvalidOperationException("Payment strategy not set");
        }
        
        return _paymentStrategy.ProcessPayment(amount);
    }
}

// Usage
var processor = new PaymentProcessor();

// Use credit card
processor.SetPaymentStrategy(new CreditCardPayment("1234-5678-9012-3456", "123"));
var result1 = processor.ProcessPayment(100.00m);

// Switch to PayPal
processor.SetPaymentStrategy(new PayPalPayment("user@example.com"));
var result2 = processor.ProcessPayment(250.00m);

// Modern approach with dependency injection
public class OrderService
{
    private readonly Dictionary<PaymentMethod, IPaymentStrategy> _paymentStrategies;
    
    public OrderService(IEnumerable<IPaymentStrategy> strategies)
    {
        _paymentStrategies = strategies.ToDictionary(s => GetPaymentMethod(s), s => s);
    }
    
    public async Task<PaymentResult> ProcessOrderPaymentAsync(decimal amount, PaymentMethod method)
    {
        if (_paymentStrategies.TryGetValue(method, out var strategy))
        {
            return strategy.ProcessPayment(amount);
        }
        
        throw new NotSupportedException($"Payment method {method} not supported");
    }
    
    private PaymentMethod GetPaymentMethod(IPaymentStrategy strategy)
    {
        return strategy switch
        {
            CreditCardPayment => PaymentMethod.CreditCard,
            PayPalPayment => PaymentMethod.PayPal,
            BankTransferPayment => PaymentMethod.BankTransfer,
            _ => throw new ArgumentException("Unknown payment strategy")
        };
    }
}

public enum PaymentMethod
{
    CreditCard,
    PayPal,
    BankTransfer
}

// Real-world example: Sorting strategies
public interface ISortStrategy<T>
{
    void Sort(T[] array) where T : IComparable<T>;
}

public class BubbleSortStrategy<T> : ISortStrategy<T>
{
    public void Sort(T[] array) where T : IComparable<T>
    {
        Console.WriteLine("Using Bubble Sort");
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j].CompareTo(array[j + 1]) > 0)
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                }
            }
        }
    }
}

public class QuickSortStrategy<T> : ISortStrategy<T>
{
    public void Sort(T[] array) where T : IComparable<T>
    {
        Console.WriteLine("Using Quick Sort");
        QuickSort(array, 0, array.Length - 1);
    }
    
    private void QuickSort(T[] array, int low, int high) where T : IComparable<T>
    {
        if (low < high)
        {
            int pi = Partition(array, low, high);
            QuickSort(array, low, pi - 1);
            QuickSort(array, pi + 1, high);
        }
    }
    
    private int Partition(T[] array, int low, int high) where T : IComparable<T>
    {
        T pivot = array[high];
        int i = low - 1;
        
        for (int j = low; j < high; j++)
        {
            if (array[j].CompareTo(pivot) <= 0)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
        
        (array[i + 1], array[high]) = (array[high], array[i + 1]);
        return i + 1;
    }
}

public class SortContext<T>
{
    private ISortStrategy<T> _sortStrategy;
    
    public void SetSortStrategy(ISortStrategy<T> strategy)
    {
        _sortStrategy = strategy;
    }
    
    public void Sort(T[] array) where T : IComparable<T>
    {
        // Choose strategy based on array size
        if (_sortStrategy == null)
        {
            _sortStrategy = array.Length < 10 
                ? new BubbleSortStrategy<T>() 
                : new QuickSortStrategy<T>();
        }
        
        _sortStrategy.Sort(array);
    }
}

// Usage
var numbers = new[] { 64, 34, 25, 12, 22, 11, 90 };
var sortContext = new SortContext<int>();

sortContext.SetSortStrategy(new BubbleSortStrategy<int>());
sortContext.Sort(numbers);
```

### 3. Command Pattern

**Intent:** Encapsulate a request as an object, allowing you to parameterize clients with different requests, queue operations, and support undo operations.

```csharp
// Command interface
public interface ICommand
{
    void Execute();
    void Undo();
}

// Receiver
public class TextEditor
{
    private readonly StringBuilder _content = new();
    
    public void InsertText(string text, int position)
    {
        _content.Insert(position, text);
        Console.WriteLine($"Inserted '{text}' at position {position}");
    }
    
    public void DeleteText(int position, int length)
    {
        _content.Remove(position, length);
        Console.WriteLine($"Deleted {length} characters at position {position}");
    }
    
    public string GetContent() => _content.ToString();
    
    public void Clear()
    {
        _content.Clear();
        Console.WriteLine("Content cleared");
    }
}

// Concrete commands
public class InsertTextCommand : ICommand
{
    private readonly TextEditor _editor;
    private readonly string _text;
    private readonly int _position;
    
    public InsertTextCommand(TextEditor editor, string text, int position)
    {
        _editor = editor;
        _text = text;
        _position = position;
    }
    
    public void Execute()
    {
        _editor.InsertText(_text, _position);
    }
    
    public void Undo()
    {
        _editor.DeleteText(_position, _text.Length);
    }
}

public class DeleteTextCommand : ICommand
{
    private readonly TextEditor _editor;
    private readonly int _position;
    private readonly int _length;
    private string _deletedText;
    
    public DeleteTextCommand(TextEditor editor, int position, int length)
    {
        _editor = editor;
        _position = position;
        _length = length;
    }
    
    public void Execute()
    {
        var content = _editor.GetContent();
        _deletedText = content.Substring(_position, Math.Min(_length, content.Length - _position));
        _editor.DeleteText(_position, _length);
    }
    
    public void Undo()
    {
        if (_deletedText != null)
        {
            _editor.InsertText(_deletedText, _position);
        }
    }
}

// Invoker
public class EditorInvoker
{
    private readonly Stack<ICommand> _commandHistory = new();
    private readonly Stack<ICommand> _undoHistory = new();
    
    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commandHistory.Push(command);
        _undoHistory.Clear(); // Clear redo history when new command is executed
    }
    
    public void Undo()
    {
        if (_commandHistory.Count > 0)
        {
            var command = _commandHistory.Pop();
            command.Undo();
            _undoHistory.Push(command);
        }
        else
        {
            Console.WriteLine("Nothing to undo");
        }
    }
    
    public void Redo()
    {
        if (_undoHistory.Count > 0)
        {
            var command = _undoHistory.Pop();
            command.Execute();
            _commandHistory.Push(command);
        }
        else
        {
            Console.WriteLine("Nothing to redo");
        }
    }
}

// Usage
var editor = new TextEditor();
var invoker = new EditorInvoker();

// Execute commands
invoker.ExecuteCommand(new InsertTextCommand(editor, "Hello", 0));
invoker.ExecuteCommand(new InsertTextCommand(editor, " World", 5));
invoker.ExecuteCommand(new DeleteTextCommand(editor, 5, 6));

Console.WriteLine($"Content: '{editor.GetContent()}'");

// Undo operations
invoker.Undo(); // Undo delete
Console.WriteLine($"After undo: '{editor.GetContent()}'");

invoker.Undo(); // Undo second insert
Console.WriteLine($"After undo: '{editor.GetContent()}'");

invoker.Redo(); // Redo second insert
Console.WriteLine($"After redo: '{editor.GetContent()}'");

// Real-world example: Database operations
public interface IDatabaseCommand
{
    Task ExecuteAsync();
    Task UndoAsync();
}

public class CreateUserCommand : IDatabaseCommand
{
    private readonly IUserRepository _repository;
    private readonly User _user;
    private bool _wasExecuted;
    
    public CreateUserCommand(IUserRepository repository, User user)
    {
        _repository = repository;
        _user = user;
    }
    
    public async Task ExecuteAsync()
    {
        await _repository.CreateAsync(_user);
        _wasExecuted = true;
        Console.WriteLine($"Created user: {_user.Name}");
    }
    
    public async Task UndoAsync()
    {
        if (_wasExecuted)
        {
            await _repository.DeleteAsync(_user.Id);
            Console.WriteLine($"Deleted user: {_user.Name}");
        }
    }
}

public class UpdateUserCommand : IDatabaseCommand
{
    private readonly IUserRepository _repository;
    private readonly User _newUser;
    private User _originalUser;
    
    public UpdateUserCommand(IUserRepository repository, User newUser)
    {
        _repository = repository;
        _newUser = newUser;
    }
    
    public async Task ExecuteAsync()
    {
        _originalUser = await _repository.GetByIdAsync(_newUser.Id);
        await _repository.UpdateAsync(_newUser);
        Console.WriteLine($"Updated user: {_newUser.Name}");
    }
    
    public async Task UndoAsync()
    {
        if (_originalUser != null)
        {
            await _repository.UpdateAsync(_originalUser);
            Console.WriteLine($"Reverted user: {_originalUser.Name}");
        }
    }
}

// Transaction manager using command pattern
public class DatabaseTransaction
{
    private readonly List<IDatabaseCommand> _commands = new();
    private readonly List<IDatabaseCommand> _executedCommands = new();
    
    public void AddCommand(IDatabaseCommand command)
    {
        _commands.Add(command);
    }
    
    public async Task<bool> ExecuteAsync()
    {
        try
        {
            foreach (var command in _commands)
            {
                await command.ExecuteAsync();
                _executedCommands.Add(command);
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Transaction failed: {ex.Message}");
            await RollbackAsync();
            return false;
        }
    }
    
    public async Task RollbackAsync()
    {
        // Undo in reverse order
        for (int i = _executedCommands.Count - 1; i >= 0; i--)
        {
            try
            {
                await _executedCommands[i].UndoAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Rollback failed for command {i}: {ex.Message}");
            }
        }
        _executedCommands.Clear();
    }
}
```

### 4. State Pattern

**Intent:** Allow an object to alter its behavior when its internal state changes.

```csharp
// State interface
public interface IVendingMachineState
{
    void InsertCoin(VendingMachine machine);
    void SelectProduct(VendingMachine machine, string product);
    void DispenseProduct(VendingMachine machine);
    void ReturnCoins(VendingMachine machine);
}

// Context
public class VendingMachine
{
    private IVendingMachineState _currentState;
    private decimal _insertedAmount;
    private readonly Dictionary<string, (decimal Price, int Stock)> _products;
    
    public VendingMachine()
    {
        _products = new Dictionary<string, (decimal, int)>
        {
            ["Coke"] = (1.50m, 10),
            ["Pepsi"] = (1.50m, 8),
            ["Water"] = (1.00m, 15),
            ["Chips"] = (2.00m, 5)
        };
        
        _currentState = new NoCoinState();
    }
    
    public void SetState(IVendingMachineState state)
    {
        _currentState = state;
        Console.WriteLine($"State changed to: {state.GetType().Name}");
    }
    
    public void InsertCoin(decimal amount)
    {
        _insertedAmount += amount;
        Console.WriteLine($"Inserted ${amount}. Total: ${_insertedAmount}");
        _currentState.InsertCoin(this);
    }
    
    public void SelectProduct(string product)
    {
        _currentState.SelectProduct(this, product);
    }
    
    public void DispenseProduct()
    {
        _currentState.DispenseProduct(this);
    }
    
    public void ReturnCoins()
    {
        _currentState.ReturnCoins(this);
    }
    
    public decimal GetInsertedAmount() => _insertedAmount;
    
    public bool HasProduct(string product) => _products.ContainsKey(product);
    
    public decimal GetProductPrice(string product) => _products.TryGetValue(product, out var info) ? info.Price : 0;
    
    public bool HasStock(string product) => _products.TryGetValue(product, out var info) && info.Stock > 0;
    
    public void DeductStock(string product)
    {
        if (_products.TryGetValue(product, out var info))
        {
            _products[product] = (info.Price, info.Stock - 1);
        }
    }
    
    public void ClearInsertedAmount()
    {
        _insertedAmount = 0;
    }
    
    public void DeductAmount(decimal amount)
    {
        _insertedAmount -= amount;
    }
}

// Concrete states
public class NoCoinState : IVendingMachineState
{
    public void InsertCoin(VendingMachine machine)
    {
        machine.SetState(new HasCoinState());
    }
    
    public void SelectProduct(VendingMachine machine, string product)
    {
        Console.WriteLine("Please insert coins first");
    }
    
    public void DispenseProduct(VendingMachine machine)
    {
        Console.WriteLine("Please insert coins first");
    }
    
    public void ReturnCoins(VendingMachine machine)
    {
        Console.WriteLine("No coins to return");
    }
}

public class HasCoinState : IVendingMachineState
{
    public void InsertCoin(VendingMachine machine)
    {
        // Stay in same state, just accumulate coins
        Console.WriteLine("Coin added to existing amount");
    }
    
    public void SelectProduct(VendingMachine machine, string product)
    {
        if (!machine.HasProduct(product))
        {
            Console.WriteLine("Product not available");
            return;
        }
        
        if (!machine.HasStock(product))
        {
            Console.WriteLine("Product out of stock");
            return;
        }
        
        var price = machine.GetProductPrice(product);
        if (machine.GetInsertedAmount() >= price)
        {
            machine.SetState(new ProductSelectedState(product));
        }
        else
        {
            Console.WriteLine($"Insufficient funds. Need ${price - machine.GetInsertedAmount()} more");
        }
    }
    
    public void DispenseProduct(VendingMachine machine)
    {
        Console.WriteLine("Please select a product first");
    }
    
    public void ReturnCoins(VendingMachine machine)
    {
        Console.WriteLine($"Returning ${machine.GetInsertedAmount()}");
        machine.ClearInsertedAmount();
        machine.SetState(new NoCoinState());
    }
}

public class ProductSelectedState : IVendingMachineState
{
    private readonly string _selectedProduct;
    
    public ProductSelectedState(string product)
    {
        _selectedProduct = product;
    }
    
    public void InsertCoin(VendingMachine machine)
    {
        Console.WriteLine("Product already selected. Dispensing...");
        DispenseProduct(machine);
    }
    
    public void SelectProduct(VendingMachine machine, string product)
    {
        Console.WriteLine("Product already selected. Dispensing...");
        DispenseProduct(machine);
    }
    
    public void DispenseProduct(VendingMachine machine)
    {
        var price = machine.GetProductPrice(_selectedProduct);
        machine.DeductAmount(price);
        machine.DeductStock(_selectedProduct);
        
        Console.WriteLine($"Dispensing {_selectedProduct}");
        
        // Return change if any
        if (machine.GetInsertedAmount() > 0)
        {
            Console.WriteLine($"Returning change: ${machine.GetInsertedAmount()}");
            machine.ClearInsertedAmount();
        }
        
        machine.SetState(new NoCoinState());
    }
    
    public void ReturnCoins(VendingMachine machine)
    {
        Console.WriteLine($"Returning ${machine.GetInsertedAmount()}");
        machine.ClearInsertedAmount();
        machine.SetState(new NoCoinState());
    }
}

// Usage
var vendingMachine = new VendingMachine();

vendingMachine.SelectProduct("Coke"); // Should prompt to insert coins
vendingMachine.InsertCoin(1.00m);
vendingMachine.SelectProduct("Coke"); // Should ask for more money
vendingMachine.InsertCoin(0.75m);
vendingMachine.SelectProduct("Coke"); // Should dispense and return change

// Real-world example: Order processing state machine
public enum OrderStatus
{
    Pending,
    Confirmed,
    Shipped,
    Delivered,
    Cancelled
}

public interface IOrderState
{
    void Confirm(Order order);
    void Ship(Order order);
    void Deliver(Order order);
    void Cancel(Order order);
}

public class Order
{
    public string OrderId { get; }
    public OrderStatus Status { get; private set; }
    private IOrderState _currentState;
    
    public Order(string orderId)
    {
        OrderId = orderId;
        Status = OrderStatus.Pending;
        _currentState = new PendingState();
    }
    
    public void SetState(IOrderState state, OrderStatus status)
    {
        _currentState = state;
        Status = status;
        Console.WriteLine($"Order {OrderId} status changed to: {status}");
    }
    
    public void Confirm() => _currentState.Confirm(this);
    public void Ship() => _currentState.Ship(this);
    public void Deliver() => _currentState.Deliver(this);
    public void Cancel() => _currentState.Cancel(this);
}

public class PendingState : IOrderState
{
    public void Confirm(Order order)
    {
        order.SetState(new ConfirmedState(), OrderStatus.Confirmed);
    }
    
    public void Ship(Order order)
    {
        Console.WriteLine("Cannot ship pending order. Confirm first.");
    }
    
    public void Deliver(Order order)
    {
        Console.WriteLine("Cannot deliver pending order.");
    }
    
    public void Cancel(Order order)
    {
        order.SetState(new CancelledState(), OrderStatus.Cancelled);
    }
}

public class ConfirmedState : IOrderState
{
    public void Confirm(Order order)
    {
        Console.WriteLine("Order already confirmed.");
    }
    
    public void Ship(Order order)
    {
        order.SetState(new ShippedState(), OrderStatus.Shipped);
    }
    
    public void Deliver(Order order)
    {
        Console.WriteLine("Cannot deliver unshipped order.");
    }
    
    public void Cancel(Order order)
    {
        order.SetState(new CancelledState(), OrderStatus.Cancelled);
    }
}

public class ShippedState : IOrderState
{
    public void Confirm(Order order)
    {
        Console.WriteLine("Order already confirmed and shipped.");
    }
    
    public void Ship(Order order)
    {
        Console.WriteLine("Order already shipped.");
    }
    
    public void Deliver(Order order)
    {
        order.SetState(new DeliveredState(), OrderStatus.Delivered);
    }
    
    public void Cancel(Order order)
    {
        Console.WriteLine("Cannot cancel shipped order.");
    }
}

public class DeliveredState : IOrderState
{
    public void Confirm(Order order)
    {
        Console.WriteLine("Order already delivered.");
    }
    
    public void Ship(Order order)
    {
        Console.WriteLine("Order already delivered.");
    }
    
    public void Deliver(Order order)
    {
        Console.WriteLine("Order already delivered.");
    }
    
    public void Cancel(Order order)
    {
        Console.WriteLine("Cannot cancel delivered order.");
    }
}

public class CancelledState : IOrderState
{
    public void Confirm(Order order)
    {
        Console.WriteLine("Cannot confirm cancelled order.");
    }
    
    public void Ship(Order order)
    {
        Console.WriteLine("Cannot ship cancelled order.");
    }
    
    public void Deliver(Order order)
    {
        Console.WriteLine("Cannot deliver cancelled order.");
    }
    
    public void Cancel(Order order)
    {
        Console.WriteLine("Order already cancelled.");
    }
}

// Usage
var order = new Order("ORD123");
order.Confirm();
order.Ship();
order.Deliver();
order.Cancel(); // Should not be allowed
```

---

## Modern C# Patterns

### 1. Repository Pattern with Unit of Work

```csharp
// Generic repository interface
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

// Generic repository implementation
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;
    
    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    
    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
    
    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }
    
    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }
    
    public virtual async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
    }
}

// Specific repository interfaces
public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetActiveUsersAsync();
}

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
    Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
}

// Specific repository implementations
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context) { }
    
    public async Task<User> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await _dbSet.Where(u => u.IsActive).ToListAsync();
    }
}

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(DbContext context) : base(context) { }
    
    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _dbSet.Where(o => o.UserId == userId).ToListAsync();
    }
    
    public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToListAsync();
    }
}

// Unit of Work interface
public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IOrderRepository Orders { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}

// Unit of Work implementation
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private IDbContextTransaction _transaction;
    
    public UnitOfWork(DbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Orders = new OrderRepository(_context);
    }
    
    public IUserRepository Users { get; }
    public IOrderRepository Orders { get; }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }
    
    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    
    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    
    public void Dispose()
    {
        _transaction?.Dispose();
        _context?.Dispose();
    }
}

// Service layer using Unit of Work
public class OrderService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Order> CreateOrderAsync(int userId, List<OrderItem> items)
    {
        await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            // Verify user exists
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            
            // Create order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Items = items,
                TotalAmount = items.Sum(i => i.Price * i.Quantity)
            };
            
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            
            await _unitOfWork.CommitTransactionAsync();
            return order;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
```

### 2. CQRS (Command Query Responsibility Segregation) Pattern

```csharp
// Command and Query interfaces
public interface ICommand
{
}

public interface IQuery<TResult>
{
}

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}

public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query);
}

// Commands
public class CreateUserCommand : ICommand
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UpdateUserCommand : ICommand
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

// Queries
public class GetUserByIdQuery : IQuery<UserDto>
{
    public int UserId { get; set; }
}

public class GetAllUsersQuery : IQuery<IEnumerable<UserDto>>
{
}

// DTOs
public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

// Command Handlers
public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    
    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task HandleAsync(CreateUserCommand command)
    {
        var user = new User
        {
            Name = command.Name,
            Email = command.Email
        };
        
        await _userRepository.AddAsync(user);
    }
}

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    
    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task HandleAsync(UpdateUserCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user != null)
        {
            user.Name = command.Name;
            user.Email = command.Email;
            await _userRepository.UpdateAsync(user);
        }
    }
}

// Query Handlers
public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<UserDto> HandleAsync(GetUserByIdQuery query)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);
        return user != null ? new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        } : null;
    }
}

// Mediator for CQRS
public interface IMediator
{
    Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    Task<TResult> SendAsync<TResult>(IQuery<TResult> query);
}

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;
    
    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command);
    }
    
    public async Task<TResult> SendAsync<TResult>(IQuery<TResult> query)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = _serviceProvider.GetRequiredService(handlerType);
        
        var method = handlerType.GetMethod("HandleAsync");
        var task = (Task<TResult>)method.Invoke(handler, new object[] { query });
        
        return await task;
    }
}

// Controller using CQRS
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        await _mediator.SendAsync(command);
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
    {
        command.UserId = id;
        await _mediator.SendAsync(command);
        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _mediator.SendAsync(new GetUserByIdQuery { UserId = id });
        return user != null ? Ok(user) : NotFound();
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _mediator.SendAsync(new GetAllUsersQuery());
        return Ok(users);
    }
}
```

---

## Anti-Patterns to Avoid

### 1. God Object Anti-Pattern

```csharp
// BAD: God Object - does everything
public class UserManager
{
    public void CreateUser(User user) { }
    public void UpdateUser(User user) { }
    public void DeleteUser(int id) { }
    public User GetUser(int id) { return null; }
    public void SendEmail(string email, string message) { }
    public void LogActivity(string activity) { }
    public void ValidateUser(User user) { }
    public void EncryptPassword(string password) { }
    public void GenerateReport() { }
    public void BackupDatabase() { }
    public void ProcessPayment(decimal amount) { }
    // ... 50 more methods
}

// GOOD: Separated responsibilities
public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IPasswordService _passwordService;
    
    public UserService(IUserRepository userRepository, IEmailService emailService, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _passwordService = passwordService;
    }
    
    public async Task<User> CreateUserAsync(CreateUserRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = _passwordService.HashPassword(request.Password)
        };
        
        await _userRepository.AddAsync(user);
        await _emailService.SendWelcomeEmailAsync(user.Email);
        
        return user;
    }
}

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string email);
}

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}
```

### 2. Singleton Abuse Anti-Pattern

```csharp
// BAD: Overusing Singleton
public class DatabaseSingleton
{
    private static DatabaseSingleton _instance;
    private static readonly object _lock = new object();
    
    public static DatabaseSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new DatabaseSingleton();
                }
            }
            return _instance;
        }
    }
    
    public void SaveUser(User user) { }
    public void SaveOrder(Order order) { }
    public void SaveProduct(Product product) { }
    // Tight coupling, hard to test, global state
}

// GOOD: Dependency Injection
public interface IUserRepository
{
    Task SaveAsync(User user);
}

public class UserRepository : IUserRepository
{
    private readonly IDbContext _context;
    
    public UserRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}

// Register in DI container
services.AddScoped<IUserRepository, UserRepository>();
```

### 3. Anemic Domain Model Anti-Pattern

```csharp
// BAD: Anemic Domain Model - just data containers
public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; }
    public OrderStatus Status { get; set; }
}

public class OrderService
{
    public void ProcessOrder(Order order)
    {
        // All business logic in service
        decimal total = 0;
        foreach (var item in order.Items)
        {
            total += item.Price * item.Quantity;
        }
        order.TotalAmount = total;
        
        if (total > 1000)
        {
            order.Status = OrderStatus.RequiresApproval;
        }
        else
        {
            order.Status = OrderStatus.Confirmed;
        }
    }
}

// GOOD: Rich Domain Model - behavior with data
public class Order
{
    private readonly List<OrderItem> _items = new();
    
    public int Id { get; private set; }
    public DateTime OrderDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    public OrderStatus Status { get; private set; }
    
    public Order(int customerId)
    {
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Draft;
    }
    
    public void AddItem(Product product, int quantity)
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException("Cannot modify confirmed order");
        
        var existingItem = _items.FirstOrDefault(i => i.ProductId == product.Id);
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            _items.Add(new OrderItem(product.Id, product.Price, quantity));
        }
        
        RecalculateTotal();
    }
    
    public void Confirm()
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException("Order already confirmed");
        
        if (!_items.Any())
            throw new InvalidOperationException("Cannot confirm empty order");
        
        Status = TotalAmount > 1000 ? OrderStatus.RequiresApproval : OrderStatus.Confirmed;
    }
    
    private void RecalculateTotal()
    {
        TotalAmount = _items.Sum(i => i.Price * i.Quantity);
    }
}
```

---

## Interview Questions

### 1. **What is the difference between Factory Method and Abstract Factory patterns?**

**Answer:**

| Aspect | Factory Method | Abstract Factory |
|--------|----------------|------------------|
| **Purpose** | Create one product | Create families of related products |
| **Structure** | One factory method | Multiple factory methods |
| **Inheritance** | Uses inheritance | Uses composition |
| **Products** | Single product hierarchy | Multiple product hierarchies |

```csharp
// Factory Method - creates one type of product
public abstract class DocumentCreator
{
    public abstract Document CreateDocument(); // One factory method
}

// Abstract Factory - creates families of products
public interface IUIFactory
{
    IButton CreateButton();     // Multiple factory methods
    ITextBox CreateTextBox();   // for related products
    ICheckBox CreateCheckBox();
}
```

### 2. **How does the Strategy pattern differ from the State pattern?**

**Answer:**

| Aspect | Strategy | State |
|--------|----------|-------|
| **Intent** | Choose algorithm at runtime | Change behavior based on internal state |
| **Client Knowledge** | Client chooses strategy | Client doesn't know about states |
| **State Changes** | Strategy doesn't change itself | States can transition to other states |
| **Context** | Context uses strategy | Context delegates to current state |

```csharp
// Strategy - client chooses algorithm
var processor = new PaymentProcessor();
processor.SetStrategy(new CreditCardPayment()); // Client decides
processor.ProcessPayment(100);

// State - internal state drives behavior
var order = new Order();
order.Confirm(); // Internal state machine handles transitions
order.Ship();    // Behavior changes based on current state
```

### 3. **When would you use the Decorator pattern vs Inheritance?**

**Answer:**

**Use Decorator when:**
- Need to add responsibilities dynamically
- Want to avoid class explosion
- Composition is preferred over inheritance
- Need multiple combinations of features

**Use Inheritance when:**
- IS-A relationship exists
- Behavior is fixed at compile time
- Simple hierarchy with few variations

```csharp
// Decorator - dynamic composition
ICoffee coffee = new SimpleCoffee();
coffee = new MilkDecorator(coffee);      // Add milk
coffee = new SugarDecorator(coffee);     // Add sugar
coffee = new WhipDecorator(coffee);      // Add whip

// Inheritance - would need many classes
// SimpleCoffee, MilkCoffee, SugarCoffee, MilkSugarCoffee, 
// MilkSugarWhipCoffee, etc. (combinatorial explosion)
```

### 4. **Explain the SOLID principles with examples.**

**Answer:**

**S - Single Responsibility Principle:**
```csharp
// BAD - multiple responsibilities
public class User
{
    public void Save() { } // Database responsibility
    public void SendEmail() { } // Email responsibility
    public void ValidateData() { } // Validation responsibility
}

// GOOD - single responsibility
public class User { /* Just user data */ }
public class UserRepository { public void Save(User user) { } }
public class EmailService { public void SendEmail(string email) { } }
public class UserValidator { public bool Validate(User user) { return true; } }
```

**O - Open/Closed Principle:**
```csharp
// Open for extension, closed for modification
public abstract class Shape
{
    public abstract double CalculateArea();
}

public class Circle : Shape
{
    public double Radius { get; set; }
    public override double CalculateArea() => Math.PI * Radius * Radius;
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public override double CalculateArea() => Width * Height;
}
```

### 5. **How do you implement thread-safe Singleton in C#?**

**Answer:**

```csharp
// Method 1: Lazy<T> (Recommended)
public sealed class Singleton
{
    private static readonly Lazy<Singleton> _instance = 
        new Lazy<Singleton>(() => new Singleton());
    
    private Singleton() { }
    
    public static Singleton Instance => _instance.Value;
}

// Method 2: Static constructor
public sealed class Singleton
{
    private static readonly Singleton _instance = new Singleton();
    
    static Singleton() { }
    private Singleton() { }
    
    public static Singleton Instance => _instance;
}

// Method 3: Double-checked locking (not recommended in C#)
public sealed class Singleton
{
    private static Singleton _instance;
    private static readonly object _lock = new object();
    
    public static Singleton Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new Singleton();
                }
            }
            return _instance;
        }
    }
}
```

---

## Quick Reference

### Pattern Categories Quick Guide

**Creational Patterns - Object Creation:**
- **Singleton**: One instance globally
- **Factory Method**: Create objects without specifying exact class
- **Abstract Factory**: Create families of related objects
- **Builder**: Construct complex objects step by step
- **Prototype**: Create objects by cloning existing instances

**Structural Patterns - Object Composition:**
- **Adapter**: Make incompatible interfaces work together
- **Decorator**: Add behavior dynamically
- **Facade**: Provide simplified interface to complex subsystem
- **Composite**: Treat individual and composite objects uniformly
- **Proxy**: Provide placeholder/surrogate for another object

**Behavioral Patterns - Object Interaction:**
- **Observer**: Notify multiple objects about state changes
- **Strategy**: Encapsulate algorithms and make them interchangeable
- **Command**: Encapsulate requests as objects
- **State**: Change object behavior based on internal state
- **Template Method**: Define algorithm skeleton, let subclasses override steps

### Modern C# Pattern Recommendations

1. **Prefer Dependency Injection over Singleton**
2. **Use Events for Observer pattern**
3. **Leverage generics for type-safe patterns**
4. **Use async/await in pattern implementations**
5. **Consider records for immutable data objects**
6. **Use pattern matching for cleaner code**
7. **Implement IDisposable for resource management**

### When to Use Each Pattern

| Pattern | Use When | Avoid When |
|---------|----------|------------|
| **Singleton** | Truly need one instance (rare) | Can use DI instead |
| **Factory** | Object creation is complex | Simple object creation |
| **Observer** | One-to-many notifications | Tight coupling is acceptable |
| **Strategy** | Multiple algorithms for same task | Only one algorithm |
| **Decorator** | Need to add behavior dynamically | Simple inheritance works |
| **Command** | Need undo/redo, queuing | Simple method calls suffice |

This completes the comprehensive C# Design Patterns guide with practical examples, modern C# approaches, anti-patterns to avoid, and interview-ready content for experienced developers.