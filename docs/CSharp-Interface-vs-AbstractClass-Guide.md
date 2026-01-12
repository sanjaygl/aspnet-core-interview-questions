# Interface vs Abstract Class - Cross Questions

## Fundamental Concepts

### 1. **What is the difference between an interface and an abstract class in C#?**

**Answer:** Here are the key differences with detailed explanations and examples:

#### **Multiple Inheritance vs Single Inheritance**
- **Interfaces**: A class can implement multiple interfaces
- **Abstract Classes**: A class can inherit from only one abstract class

```csharp
// Multiple interface implementation - ALLOWED
public interface IFlyable { void Fly(); }
public interface ISwimmable { void Swim(); }

public class Duck : IFlyable, ISwimmable
{
    public void Fly() => Console.WriteLine("Duck is flying");
    public void Swim() => Console.WriteLine("Duck is swimming");
}

// Multiple abstract class inheritance - NOT ALLOWED
public abstract class Animal { }
public abstract class Vehicle { }
// public class FlyingCar : Animal, Vehicle { } // COMPILATION ERROR
```

#### **Implementation Requirements**
- **Interfaces**: All members must be implemented (unless default implementation in C# 8.0+)
- **Abstract Classes**: Can have both abstract (must implement) and concrete (optional to override) methods

```csharp
// Interface - all methods must be implemented
public interface IShape
{
    double GetArea();        // Must implement
    double GetPerimeter();   // Must implement
}

// Abstract class - mixed implementation requirements
public abstract class Shape
{
    public abstract double GetArea();           // Must implement
    public virtual void Display()               // Optional to override
    {
        Console.WriteLine($"Area: {GetArea()}");
    }
    public void PrintInfo()                     // Cannot override
    {
        Console.WriteLine("This is a shape");
    }
}
```

#### **Fields and Constructors**
- **Interfaces**: Cannot have instance fields or constructors
- **Abstract Classes**: Can have fields, constructors, and destructors

```csharp
// Interface - no fields or constructors
public interface IRepository
{
    // string connectionString; // NOT ALLOWED
    // IRepository() { }         // NOT ALLOWED
    void Save(object data);
}

// Abstract class - can have fields and constructors
public abstract class BaseRepository
{
    protected string connectionString;    // Field allowed
    protected DateTime createdAt;
    
    protected BaseRepository(string connStr)  // Constructor allowed
    {
        connectionString = connStr;
        createdAt = DateTime.Now;
    }
    
    public abstract void Save(object data);
}
```

#### **Access Modifiers**
- **Interfaces**: Members are public by default (C# 8.0+ allows other modifiers with default implementations)
- **Abstract Classes**: Can use any access modifier (private, protected, internal, public)

```csharp
// Interface access modifiers
public interface IExample
{
    void PublicMethod();                    // Implicitly public
    // private void PrivateMethod();        // NOT ALLOWED (before C# 8.0)
}

// Abstract class access modifiers
public abstract class ExampleBase
{
    private int privateField;               // Private field
    protected abstract void ProtectedMethod(); // Protected abstract method
    internal void InternalMethod() { }      // Internal concrete method
    public abstract void PublicMethod();    // Public abstract method
}
```

#### **Static Members**
- **Interfaces**: Can have static members (C# 8.0+)
- **Abstract Classes**: Can have static members (always supported)

```csharp
// Interface with static members (C# 8.0+)
public interface IMathOperations
{
    static double Pi = 3.14159;
    static double GetCircleArea(double radius) => Pi * radius * radius;
    double Calculate(double value); // Instance method
}

// Abstract class with static members
public abstract class MathBase
{
    public static double Pi = 3.14159;
    public static double GetCircleArea(double radius) => Pi * radius * radius;
    public abstract double Calculate(double value);
}
```

#### **When to Use Each**

**Use Interface When:**
```csharp
// Contract definition - multiple unrelated classes can implement
public interface INotificationService
{
    Task SendAsync(string message, string recipient);
}

// Different implementations
public class EmailService : INotificationService { /* implementation */ }
public class SmsService : INotificationService { /* implementation */ }
public class PushNotificationService : INotificationService { /* implementation */ }
```

**Use Abstract Class When:**
```csharp
// Shared functionality among related classes
public abstract class Employee
{
    protected string name;
    protected decimal baseSalary;
    
    protected Employee(string name, decimal baseSalary)
    {
        this.name = name;
        this.baseSalary = baseSalary;
    }
    
    // Shared implementation
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Employee: {name}, Base Salary: {baseSalary}");
    }
    
    // Must be implemented by derived classes
    public abstract decimal CalculateSalary();
}

public class FullTimeEmployee : Employee
{
    public FullTimeEmployee(string name, decimal baseSalary) : base(name, baseSalary) { }
    public override decimal CalculateSalary() => baseSalary;
}

public class ContractEmployee : Employee
{
    private int hoursWorked;
    public ContractEmployee(string name, decimal hourlyRate, int hours) : base(name, hourlyRate)
    {
        hoursWorked = hours;
    }
    public override decimal CalculateSalary() => baseSalary * hoursWorked;
}
```

### 2. **Can an abstract class implement an interface?**
**Answer:** Yes, an abstract class can implement one or more interfaces. The abstract class can either provide implementation for interface members or leave them for derived classes to implement.

```csharp
public interface IExample
{
    void DoWork();
}

public abstract class BaseClass : IExample
{
    public void DoWork() // Can be implemented
    {
        // Implementation
    }
    
    // Or leave it abstract
    public abstract void DoWork();
}
```

### 3. **Can an interface inherit from another interface?**
**Answer:** Yes, an interface can inherit from one or multiple interfaces. This is called interface inheritance.

```csharp
public interface IBase
{
    void Method1();
}

public interface IDerived : IBase
{
    void Method2();
}
```

### 4. **Can we have a constructor in an interface?**
**Answer:** No, interfaces cannot have constructors because they cannot be instantiated directly. Only classes (including abstract classes) can have constructors.

### 5. **Can abstract classes have private methods?**
**Answer:** Yes, abstract classes can have private, protected, internal, and public methods. This allows for encapsulation and helper methods.

## Advanced Concepts

### 6. **What are default interface methods (C# 8.0+)?**
**Answer:** Starting from C# 8.0, interfaces can have default implementations for methods. This allows adding new methods to interfaces without breaking existing implementations.

```csharp
public interface ILogger
{
    void Log(string message);
    
    // Default implementation
    void LogError(string error)
    {
        Log($"ERROR: {error}");
    }
}
```

### 7. **Can an abstract class be sealed?**
**Answer:** No, an abstract class cannot be sealed. The `abstract` keyword means the class must be inherited, while `sealed` means it cannot be inherited. These are mutually exclusive.

### 8. **What is the difference between abstract methods and virtual methods?**
**Answer:**
- **Abstract methods:** Have no implementation in the base class and MUST be overridden in derived classes
- **Virtual methods:** Have an implementation in the base class and MAY be overridden in derived classes (optional)

```csharp
public abstract class Base
{
    public abstract void MustOverride();    // Must be implemented
    public virtual void CanOverride() { }   // Optional to override
}
```

### 9. **Can interfaces have static members?**
**Answer:** Yes (C# 8.0+), interfaces can have static members including static methods, fields, and properties.

```csharp
public interface IConstants
{
    static int MaxValue = 100;
    static void StaticMethod() { }
}
```

### 10. **When should you use an abstract class vs an interface?**
**Answer:**

**Use Abstract Class when:**

1. **Shared Implementation**: You have common code that multiple related classes should share
```csharp
public abstract class Vehicle
{
    protected string brand;
    protected int year;
    
    protected Vehicle(string brand, int year)
    {
        this.brand = brand;
        this.year = year;
    }
    
    // Shared implementation
    public virtual string GetInfo() => $"{year} {brand}";
    
    // Common behavior with default implementation
    public virtual void StartEngine()
    {
        Console.WriteLine($"{brand} engine starting...");
    }
    
    // Force derived classes to implement
    public abstract void Move();
    public abstract double GetFuelEfficiency();
}

public class Car : Vehicle
{
    public Car(string brand, int year) : base(brand, year) { }
    
    public override void Move() => Console.WriteLine("Driving on road");
    public override double GetFuelEfficiency() => 25.5; // MPG
}

public class Boat : Vehicle
{
    public Boat(string brand, int year) : base(brand, year) { }
    
    public override void Move() => Console.WriteLine("Sailing on water");
    public override double GetFuelEfficiency() => 8.2; // MPG
}
```

2. **IS-A Relationship**: When there's a clear hierarchical relationship
```csharp
public abstract class Employee
{
    public string Name { get; protected set; }
    public int EmployeeId { get; protected set; }
    
    protected Employee(string name, int id)
    {
        Name = name;
        EmployeeId = id;
    }
    
    public abstract decimal CalculateSalary();
    
    // Common behavior for all employees
    public virtual void ClockIn()
    {
        Console.WriteLine($"{Name} clocked in at {DateTime.Now}");
    }
}
```

**Use Interface when:**

1. **Contract Definition**: You want to define what a class can do, not what it is
```csharp
public interface IPaymentProcessor
{
    Task<PaymentResult> ProcessPaymentAsync(decimal amount, string paymentMethod);
    Task<bool> RefundAsync(string transactionId);
}

// Completely different classes can implement the same interface
public class CreditCardProcessor : IPaymentProcessor
{
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string paymentMethod)
    {
        // Credit card specific logic
        return new PaymentResult { Success = true, TransactionId = Guid.NewGuid().ToString() };
    }
    
    public async Task<bool> RefundAsync(string transactionId)
    {
        // Credit card refund logic
        return true;
    }
}

public class PayPalProcessor : IPaymentProcessor
{
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string paymentMethod)
    {
        // PayPal specific logic
        return new PaymentResult { Success = true, TransactionId = $"PP_{Guid.NewGuid()}" };
    }
    
    public async Task<bool> RefundAsync(string transactionId)
    {
        // PayPal refund logic
        return true;
    }
}
```

2. **Multiple Capabilities**: When a class needs multiple unrelated capabilities
```csharp
public interface IFlyable { void Fly(); }
public interface ISwimmable { void Swim(); }
public interface IWalkable { void Walk(); }

// A duck can do all three
public class Duck : IFlyable, ISwimmable, IWalkable
{
    public void Fly() => Console.WriteLine("Duck flying");
    public void Swim() => Console.WriteLine("Duck swimming");
    public void Walk() => Console.WriteLine("Duck walking");
}

// A fish can only swim
public class Fish : ISwimmable
{
    public void Swim() => Console.WriteLine("Fish swimming");
}
```

3. **Dependency Injection and Testing**: Interfaces make code more testable
```csharp
public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

public class OrderService
{
    private readonly IEmailService emailService;
    
    public OrderService(IEmailService emailService)
    {
        this.emailService = emailService;
    }
    
    public async Task ProcessOrderAsync(Order order)
    {
        // Process order logic
        await emailService.SendEmailAsync(order.CustomerEmail, "Order Confirmation", "Your order has been processed");
    }
}

// Easy to mock for testing
public class MockEmailService : IEmailService
{
    public List<(string to, string subject, string body)> SentEmails { get; } = new();
    
    public Task SendEmailAsync(string to, string subject, string body)
    {
        SentEmails.Add((to, subject, body));
        return Task.CompletedTask;
    }
}
```

**Decision Matrix:**

| Scenario | Use Abstract Class | Use Interface |
|----------|-------------------|---------------|
| Shared code among related classes | ✅ | ❌ |
| Multiple inheritance needed | ❌ | ✅ |
| Need constructors/fields | ✅ | ❌ |
| Defining a contract | ❌ | ✅ |
| IS-A relationship | ✅ | ❌ |
| CAN-DO relationship | ❌ | ✅ |
| Versioning flexibility | ✅ | ❌ (unless C# 8.0+ default methods) |
| Testing/Mocking | ❌ | ✅ |

## Practical Scenarios

### 11. **Can a class implement two interfaces with the same method signature?**
**Answer:** Yes, a class can implement multiple interfaces with the same method signature. A single implementation satisfies both interfaces.

```csharp
public interface IA { void DoWork(); }
public interface IB { void DoWork(); }

public class MyClass : IA, IB
{
    public void DoWork() // Satisfies both interfaces
    {
        // Implementation
    }
}
```

### 12. **What is explicit interface implementation?**
**Answer:** Explicit interface implementation is when you implement an interface member by fully qualifying it with the interface name. This is useful when implementing multiple interfaces with conflicting member names or when you want to hide interface members from the public API.

**Basic Explicit Implementation:**
```csharp
public interface IDrawable
{
    void Draw();
}

public interface IPrintable
{
    void Print();
}

public class Document : IDrawable, IPrintable
{
    // Explicit implementation - only accessible through interface reference
    void IDrawable.Draw()
    {
        Console.WriteLine("Drawing document on screen");
    }
    
    void IPrintable.Print()
    {
        Console.WriteLine("Printing document on paper");
    }
    
    // Public method - accessible directly
    public void Save()
    {
        Console.WriteLine("Saving document");
    }
}

// Usage
Document doc = new Document();
doc.Save(); // Direct access - OK
// doc.Draw(); // Compilation error - not accessible

IDrawable drawable = doc;
drawable.Draw(); // OK - accessed through interface

IPrintable printable = doc;
printable.Print(); // OK - accessed through interface
```

**Handling Name Conflicts:**
```csharp
public interface IFileManager
{
    void Delete();
}

public interface IUserInterface
{
    void Delete();
}

public class FileEditor : IFileManager, IUserInterface
{
    // Explicit implementations to resolve conflict
    void IFileManager.Delete()
    {
        Console.WriteLine("Deleting file from disk");
    }
    
    void IUserInterface.Delete()
    {
        Console.WriteLine("Deleting UI element");
    }
    
    // Public method with clear intent
    public void DeleteFile()
    {
        ((IFileManager)this).Delete();
    }
    
    public void DeleteUIElement()
    {
        ((IUserInterface)this).Delete();
    }
}
```

**Mixed Implicit and Explicit Implementation:**
```csharp
public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task SaveAsync(T entity);
    Task DeleteAsync(int id);
}

public interface IAuditableRepository
{
    Task<AuditLog[]> GetAuditLogsAsync(int entityId);
}

public class UserRepository : IRepository<User>, IAuditableRepository
{
    // Implicit implementation - part of public API
    public async Task<User> GetByIdAsync(int id)
    {
        Console.WriteLine($"Getting user {id}");
        return new User { Id = id };
    }
    
    public async Task SaveAsync(User entity)
    {
        Console.WriteLine($"Saving user {entity.Id}");
    }
    
    // Explicit implementation - hidden from public API
    async Task IRepository<User>.DeleteAsync(int id)
    {
        Console.WriteLine($"Soft deleting user {id}");
        // Soft delete implementation
    }
    
    // Explicit implementation for auditing
    async Task<AuditLog[]> IAuditableRepository.GetAuditLogsAsync(int entityId)
    {
        Console.WriteLine($"Getting audit logs for entity {entityId}");
        return new AuditLog[0];
    }
    
    // Public method for hard delete
    public async Task HardDeleteAsync(int id)
    {
        Console.WriteLine($"Hard deleting user {id}");
        // Hard delete implementation
    }
}

// Usage demonstrates the difference
UserRepository repo = new UserRepository();
repo.SaveAsync(new User()); // Direct access - implicit implementation
// repo.DeleteAsync(1); // Compilation error - explicit implementation not accessible

IRepository<User> repoInterface = repo;
repoInterface.DeleteAsync(1); // OK - soft delete through interface

repo.HardDeleteAsync(1); // Direct access to public method
```

**When to Use Explicit Implementation:**

1. **Interface Segregation**: Hide complex interface members from everyday usage
```csharp
public interface IAdvancedConfiguration
{
    void SetInternalBuffer(byte[] buffer);
    void EnableDebugMode(bool enable);
}

public class SimpleService : IAdvancedConfiguration
{
    // Hide advanced configuration from normal usage
    void IAdvancedConfiguration.SetInternalBuffer(byte[] buffer)
    {
        // Advanced configuration
    }
    
    void IAdvancedConfiguration.EnableDebugMode(bool enable)
    {
        // Debug configuration
    }
    
    // Simple public API
    public void DoWork()
    {
        Console.WriteLine("Doing work");
    }
}
```

2. **Version Compatibility**: Maintain backward compatibility
```csharp
public interface ILegacyService
{
    string ProcessData(string data);
}

public interface IModernService
{
    Task<string> ProcessDataAsync(string data);
}

public class DataService : ILegacyService, IModernService
{
    // Modern async implementation
    public async Task<string> ProcessDataAsync(string data)
    {
        await Task.Delay(100); // Simulate async work
        return $"Processed: {data}";
    }
    
    // Legacy sync implementation (explicit to discourage use)
    string ILegacyService.ProcessData(string data)
    {
        // Synchronous wrapper for backward compatibility
        return ProcessDataAsync(data).GetAwaiter().GetResult();
    }
}
```

### 13. **Can abstract classes have fields while interfaces cannot?**
**Answer:** Yes, abstract classes can have instance fields (variables), while interfaces cannot have instance fields. However, interfaces can have static fields (C# 8.0+).

```csharp
public abstract class BaseClass
{
    protected int value; // Instance field allowed
}

public interface IExample
{
    // int value; // Not allowed
    static int StaticValue = 10; // Allowed in C# 8.0+
}
```

### 14. **Can you instantiate an abstract class or interface?**
**Answer:** No, you cannot directly instantiate either an abstract class or an interface. However, you can create instances of concrete classes that inherit from abstract classes or implement interfaces.

```csharp
// Shape shape = new Shape(); // ERROR - cannot instantiate
Shape shape = new Circle(); // OK - Circle is concrete
```

### 15. **What happens if a derived class doesn't implement all abstract methods?**
**Answer:** The derived class must also be declared as abstract. Only concrete (non-abstract) classes must implement all abstract methods.

```csharp
public abstract class Base
{
    public abstract void Method1();
    public abstract void Method2();
}

public abstract class Derived : Base
{
    public override void Method1() { } // Implements only one
    // Method2 remains abstract
}

public class Concrete : Derived
{
    public override void Method2() { } // Must implement remaining
}
```

## Design Patterns

### 16. **How are interfaces used in Dependency Injection?**
**Answer:** Interfaces are commonly used in Dependency Injection to define contracts, allowing loose coupling between components. This makes code more testable and maintainable.

```csharp
public interface IRepository
{
    void Save(object data);
}

public class Service
{
    private readonly IRepository _repository;
    
    public Service(IRepository repository) // Inject interface
    {
        _repository = repository;
    }
}
```

### 17. **What is the Template Method pattern and how does it relate to abstract classes?**
**Answer:** The Template Method pattern uses abstract classes to define the skeleton of an algorithm, deferring some steps to subclasses. Abstract methods in the base class are overridden to provide specific behavior.

```csharp
public abstract class DataProcessor
{
    public void Process() // Template method
    {
        ReadData();
        ProcessData();
        WriteData();
    }
    
    protected abstract void ReadData();
    protected abstract void ProcessData();
    protected abstract void WriteData();
}
```

### 18. **Can interfaces have properties?**
**Answer:** Yes, interfaces can define properties. Implementing classes must provide the property implementation.

```csharp
public interface IEntity
{
    int Id { get; set; }
    string Name { get; }
}

public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; private set; }
}
```

## Performance and Best Practices

### 19. **Is there a performance difference between interfaces and abstract classes?**
**Answer:** The performance difference is negligible. Both use virtual method dispatch. The choice should be based on design requirements rather than performance.

### 20. **What are marker interfaces?**
**Answer:** Marker interfaces (or tag interfaces) are empty interfaces used to mark classes as having certain characteristics. Example: `ISerializable`, `ICloneable`.

```csharp
public interface ISpecialProcessing { }

public class MyClass : ISpecialProcessing
{
    // Class is marked for special processing
}

// Usage
if (obj is ISpecialProcessing)
{
    // Perform special processing
}
```

### 21. **Can abstract classes have static methods?**
**Answer:** Yes, abstract classes can have static methods just like regular classes.

```csharp
public abstract class MathOperations
{
    public static int Add(int a, int b)
    {
        return a + b;
    }
    
    public abstract int Multiply(int a, int b);
}
```

### 22. **What is the purpose of abstract properties?**
**Answer:** Abstract properties force derived classes to provide their own implementation for getting/setting values, allowing each derived class to handle the property differently.

```csharp
public abstract class Entity
{
    public abstract int Id { get; set; }
    public abstract string GetDisplayName();
}
```

### 23. **Can you have an abstract class with no abstract methods?**
**Answer:** Yes, you can have an abstract class with no abstract methods. This is useful when you want to prevent direct instantiation of a class while providing common functionality.

```csharp
public abstract class Logger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}
```

### 24. **What is interface segregation principle (ISP)?**
**Answer:** ISP states that clients should not be forced to depend on interfaces they don't use. It's better to have many specific interfaces than one general-purpose interface.

```csharp
// Bad - Fat interface
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}

// Good - Segregated interfaces
public interface IWorkable { void Work(); }
public interface IFeedable { void Eat(); }
public interface ISleepable { void Sleep(); }
```

### 25. **Can abstract classes implement the Singleton pattern?**
**Answer:** While technically possible, it's not common because abstract classes cannot be instantiated directly. However, you can implement Singleton in a concrete class derived from an abstract class.

```csharp
public abstract class Configuration
{
    protected abstract void Initialize();
}

public sealed class AppConfiguration : Configuration
{
    private static readonly AppConfiguration _instance = new();
    private AppConfiguration() { }
    public static AppConfiguration Instance => _instance;
    
    protected override void Initialize() { }
}
```

## Bonus Questions

### 26. **What are covariant and contravariant interfaces?**
**Answer:** Covariant (`out`) and contravariant (`in`) allow for more flexible type conversions with generic interfaces.

```csharp
public interface ICovariant<out T>
{
    T GetItem();
}

public interface IContravariant<in T>
{
    void SetItem(T item);
}
```

### 27. **Can you use access modifiers with interface members in C# 8.0+?**
**Answer:** Yes, starting from C# 8.0, interface members can have access modifiers when they have default implementations.

```csharp
public interface IExample
{
    void PublicMethod();
    
    protected void ProtectedMethod() { }
    private void PrivateHelper() { }
}
```

### 28. **What is the liskov substitution principle and how does it relate to abstract classes?**
**Answer:** The Liskov Substitution Principle states that objects of a derived class should be able to replace objects of the base class without affecting program correctness. Abstract classes should be designed with this principle in mind.

### 29. **Can you have async methods in interfaces?**
**Answer:** Yes, interfaces can declare async method signatures using `Task` or `Task<T>` return types.

```csharp
public interface IAsyncService
{
    Task<string> GetDataAsync();
    Task ProcessAsync(string data);
}
```

### 30. **What is the difference between IS-A and CAN-DO relationships?**
**Answer:**
- **IS-A (Abstract Class):** Represents inheritance - "A Dog IS-A Animal"
- **CAN-DO (Interface):** Represents capability - "A Dog CAN-DO Run, Bark, Eat"

```csharp
// IS-A relationship
public abstract class Animal { }
public class Dog : Animal { } // Dog IS-A Animal

// CAN-DO relationship
public interface IRunnable { void Run(); }
public class Dog : IRunnable { } // Dog CAN-DO Run
```

---

## Summary Tips for Interviews

1. **Remember the key differences:** Multiple inheritance, constructors, fields, default implementations
2. **Know when to use each:** Abstract classes for shared code, interfaces for contracts
3. **Understand C# evolution:** Be aware of features added in C# 8.0+ (default implementations, static members)
4. **Think about design principles:** SOLID principles, especially ISP and LSP
5. **Be practical:** Provide real-world examples from your experience
6. **Know common patterns:** Dependency Injection, Template Method, Strategy Pattern

---

## Additional Advanced Questions

### 31. **What are generic interfaces and abstract classes? How do they differ?**
**Answer:** Both can be generic, but they handle type constraints differently.

```csharp
// Generic interface
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task SaveAsync(T entity);
}

// Generic abstract class
public abstract class BaseRepository<T> where T : class, new()
{
    protected readonly DbContext context;
    
    protected BaseRepository(DbContext context)
    {
        this.context = context;
    }
    
    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }
    
    public abstract Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate);
}
```

### 32. **Can interfaces have nested types? What about abstract classes?**
**Answer:** Yes, both can have nested types, but with different accessibility rules.

```csharp
public interface IContainer
{
    // Nested interface
    interface IItem
    {
        string Name { get; }
    }
    
    // Nested enum
    enum ContainerType
    {
        List,
        Dictionary,
        Set
    }
    
    void Add(IItem item);
}

public abstract class Container
{
    // Nested class with different access levels
    protected class Item
    {
        public string Name { get; set; }
    }
    
    private enum InternalType { A, B, C }
    
    public abstract void ProcessItems();
}
```

### 33. **How do you handle versioning with interfaces vs abstract classes?**
**Answer:** Interfaces are more fragile to changes, while abstract classes provide better versioning support.

```csharp
// Interface versioning - breaking change
public interface IPaymentProcessor_V1
{
    bool ProcessPayment(decimal amount);
}

// New version with additional method - breaking change for existing implementations
public interface IPaymentProcessor_V2 : IPaymentProcessor_V1
{
    bool ProcessRefund(decimal amount);
    // Solution: Use default implementation (C# 8.0+)
    bool ValidatePayment(decimal amount) => amount > 0;
}

// Abstract class versioning - non-breaking
public abstract class PaymentProcessorBase
{
    public abstract bool ProcessPayment(decimal amount);
    
    // Adding new virtual method - non-breaking
    public virtual bool ProcessRefund(decimal amount)
    {
        // Default implementation
        return true;
    }
}
```

### 34. **What is the difference between implicit and explicit interface implementation performance?**
**Answer:** Explicit interface implementation has slight performance overhead due to boxing/casting.

```csharp
public interface IExample
{
    void Method();
}

public class Implementation : IExample
{
    // Implicit - direct call, better performance
    public void Method()
    {
        Console.WriteLine("Implicit implementation");
    }
    
    // Explicit - requires casting, slight overhead
    void IExample.Method()
    {
        Console.WriteLine("Explicit implementation");
    }
}

// Usage performance difference
Implementation obj = new Implementation();
obj.Method(); // Direct call - faster

IExample iface = obj;
iface.Method(); // Virtual dispatch - slightly slower
```

### 35. **How do abstract classes work with dependency injection containers?**
**Answer:** Abstract classes cannot be directly registered but can be used as base classes for concrete implementations.

```csharp
public abstract class BaseService
{
    protected readonly ILogger logger;
    
    protected BaseService(ILogger logger)
    {
        this.logger = logger;
    }
    
    public abstract Task<string> ProcessAsync(string input);
    
    protected void LogInfo(string message)
    {
        logger.LogInformation(message);
    }
}

public class EmailService : BaseService
{
    public EmailService(ILogger logger) : base(logger) { }
    
    public override async Task<string> ProcessAsync(string input)
    {
        LogInfo("Processing email");
        return await Task.FromResult($"Email: {input}");
    }
}

// DI Registration
services.AddScoped<EmailService>(); // Register concrete class
// services.AddScoped<BaseService>(); // Cannot register abstract class directly
```

### 36. **What are the memory implications of interfaces vs abstract classes?**
**Answer:** Interfaces have no memory footprint themselves, while abstract classes can have instance data.

```csharp
public interface ILightweight
{
    void DoWork();
}

public abstract class HeavyBase
{
    private readonly Dictionary<string, object> cache = new(); // Memory overhead
    private readonly Timer timer; // More memory
    protected readonly string[] largeArray = new string[1000]; // Even more memory
    
    protected HeavyBase()
    {
        timer = new Timer(OnTimer, null, 1000, 1000);
    }
    
    private void OnTimer(object state) { /* cleanup logic */ }
    public abstract void DoWork();
}

// Memory usage comparison
public class LightImplementation : ILightweight
{
    public void DoWork() { } // Minimal memory footprint
}

public class HeavyImplementation : HeavyBase
{
    public override void DoWork() { } // Inherits all base class memory overhead
}
```

### 37. **How do you test classes that depend on abstract classes vs interfaces?**
**Answer:** Interfaces are easier to mock, while abstract classes may require partial mocking or test-specific implementations.

```csharp
// Easy to mock interface
public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);
}

// Testing with interface mock
[Test]
public async Task TestWithInterface()
{
    var mockEmailService = new Mock<IEmailService>();
    mockEmailService.Setup(x => x.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                   .Returns(Task.CompletedTask);
    
    var service = new NotificationService(mockEmailService.Object);
    await service.NotifyUserAsync("test@example.com", "Hello");
    
    mockEmailService.Verify(x => x.SendAsync("test@example.com", "Hello", It.IsAny<string>()), Times.Once);
}

// Abstract class - harder to mock, need test implementation
public abstract class EmailServiceBase
{
    protected abstract Task SendInternalAsync(string to, string subject, string body);
    
    public async Task SendAsync(string to, string subject, string body)
    {
        // Some common logic
        if (string.IsNullOrEmpty(to)) throw new ArgumentException("Email required");
        await SendInternalAsync(to, subject, body);
    }
}

// Test implementation for abstract class
public class TestEmailService : EmailServiceBase
{
    public List<(string to, string subject, string body)> SentEmails { get; } = new();
    
    protected override Task SendInternalAsync(string to, string subject, string body)
    {
        SentEmails.Add((to, subject, body));
        return Task.CompletedTask;
    }
}
```

### 38. **What is the role of interfaces and abstract classes in microservices architecture?**
**Answer:** Interfaces define service contracts, while abstract classes provide shared infrastructure code.

```csharp
// Service contract definition
public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(CreateOrderRequest request);
    Task<OrderDto> GetOrderAsync(int orderId);
    Task<bool> CancelOrderAsync(int orderId);
}

// Shared infrastructure for all services
public abstract class BaseService
{
    protected readonly ILogger logger;
    protected readonly IMetrics metrics;
    protected readonly IConfiguration config;
    
    protected BaseService(ILogger logger, IMetrics metrics, IConfiguration config)
    {
        this.logger = logger;
        this.metrics = metrics;
        this.config = config;
    }
    
    protected async Task<T> ExecuteWithMetricsAsync<T>(string operationName, Func<Task<T>> operation)
    {
        using var timer = metrics.StartTimer(operationName);
        try
        {
            logger.LogInformation($"Starting {operationName}");
            var result = await operation();
            metrics.IncrementCounter($"{operationName}.success");
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error in {operationName}");
            metrics.IncrementCounter($"{operationName}.error");
            throw;
        }
    }
}

// Service implementation
public class OrderService : BaseService, IOrderService
{
    private readonly IOrderRepository repository;
    
    public OrderService(IOrderRepository repository, ILogger logger, IMetrics metrics, IConfiguration config)
        : base(logger, metrics, config)
    {
        this.repository = repository;
    }
    
    public async Task<OrderDto> CreateOrderAsync(CreateOrderRequest request)
    {
        return await ExecuteWithMetricsAsync("CreateOrder", async () =>
        {
            // Business logic here
            var order = new Order(request.CustomerId, request.Items);
            await repository.SaveAsync(order);
            return order.ToDto();
        });
    }
    
    // Other interface implementations...
    public Task<OrderDto> GetOrderAsync(int orderId) => throw new NotImplementedException();
    public Task<bool> CancelOrderAsync(int orderId) => throw new NotImplementedException();
}
```

### 39. **How do you handle circular dependencies with interfaces and abstract classes?**
**Answer:** Interfaces help break circular dependencies, while abstract classes can sometimes create them.

```csharp
// Circular dependency problem
public class OrderService
{
    private readonly CustomerService customerService;
    public OrderService(CustomerService customerService) => this.customerService = customerService;
}

public class CustomerService
{
    private readonly OrderService orderService; // Circular dependency!
    public CustomerService(OrderService orderService) => this.orderService = orderService;
}

// Solution using interfaces
public interface IOrderService
{
    Task<Order> GetOrderAsync(int orderId);
}

public interface ICustomerService
{
    Task<Customer> GetCustomerAsync(int customerId);
}

public class OrderService : IOrderService
{
    private readonly ICustomerService customerService; // Depends on interface
    public OrderService(ICustomerService customerService) => this.customerService = customerService;
    
    public async Task<Order> GetOrderAsync(int orderId)
    {
        // Implementation
        return new Order();
    }
}

public class CustomerService : ICustomerService
{
    private readonly IOrderService orderService; // Depends on interface
    public CustomerService(IOrderService orderService) => this.orderService = orderService;
    
    public async Task<Customer> GetCustomerAsync(int customerId)
    {
        // Implementation
        return new Customer();
    }
}
```

### 40. **What are the compilation and runtime differences between interfaces and abstract classes?**
**Answer:** Different IL generation and runtime behavior.

```csharp
// Interface method call
public interface IExample
{
    void DoWork();
}

public class Implementation : IExample
{
    public void DoWork() => Console.WriteLine("Working");
}

// Generated IL for interface call (simplified):
// callvirt instance void IExample::DoWork()

// Abstract class method call
public abstract class ExampleBase
{
    public abstract void DoWork();
}

public class Implementation : ExampleBase
{
    public override void DoWork() => Console.WriteLine("Working");
}

// Generated IL for abstract class call (simplified):
// callvirt instance void ExampleBase::DoWork()

// Performance implications:
// - Interface calls: Virtual dispatch through interface method table
// - Abstract class calls: Virtual dispatch through class method table
// - Both have similar performance characteristics
// - Concrete class calls are fastest (no virtual dispatch)
```

