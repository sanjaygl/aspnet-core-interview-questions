# Interface Segregation Principle (ISP)

## Definition / Intent
**Interface Segregation Principle (ISP)** states that "no client should be forced to depend on methods it does not use". 

This means, instead of one fat interface, many small interfaces are preferred based on groups of methods with each one serving one sub-module.

The ISP was first used and formulated by Robert C. Martin while consulting for Xerox.

## Case Study: Xerox Printer Systems

### Problem
Xerox Corporation manufactures printer systems. In their development process of new systems, Xerox had created a new printer system that could perform a variety of tasks such as **stapling** and **faxing** along with the regular **printing** task.

The software for this system was created from the ground up. As the software grew for Xerox, making modifications became more and more difficult so that even the smallest change would take a **redeployment cycle of an hour**, which made development nearly impossible.

**The design problem was:**
- A single `Job` class was used by almost all of the tasks
- Whenever a print job or a stapling job needed to be performed, a call was made to the Job class
- This resulted in a **'fat' class** with multitudes of methods specific to a variety of different clients
- Because of this design, a staple job would know about all the methods of the print job, even though there was no use for them

### Solution
To overcome this problem, Robert C. Martin suggested a solution which is called the **Interface Segregation Principle**.

Instead of one fat interface, many small interfaces are preferred based on groups of methods with each one serving one sub-module.

## Problem Statement
Fat interfaces force implementers to provide unnecessary or irrelevant method implementations, leading to fragile and confusing code. Classes are forced to implement methods they don't need, resulting in:
- Empty implementations
- Throwing `NotImplementedException`
- Unnecessary coupling
- Difficult maintenance

## Code Example – Violation

**Before Interface Segregation Principle:**

```csharp
namespace ISPDemoConsole
{
    public interface IPrintTasks
    {
        bool PrintContent(string content);
        bool ScanContent(string content);
        bool FaxContent(string content);
        bool PhotoCopyContent(string content);
        bool PrintDuplexContent(string content);
    }
}
```

**Problem 1: High-End Printer (Forced to implement everything)**
```csharp
namespace ISPDemoConsole.Client
{
    class HPLaserJet : IPrintTasks
    {
        public bool FaxContent(string content)
        {
            Console.WriteLine("Fax Done"); 
            return true;
        }
        
        public bool PhotoCopyContent(string content)
        {
            Console.WriteLine("PhotoCopy Done"); 
            return true;
        }
        
        public bool PrintContent(string content)
        {
            Console.WriteLine("Print Done"); 
            return true;
        }
        
        public bool PrintDuplexContent(string content)
        {
            Console.WriteLine("Print Duplex Done"); 
            return true;
        }
        
        public bool ScanContent(string content)
        {
            Console.WriteLine("Scan Done"); 
            return true;
        }
    }
}
```

**Problem 2: Basic Printer (Forced to implement unsupported features)**
```csharp
namespace ISPDemoConsole.Client
{
    class CannonMG2470 : IPrintTasks
    {
        public bool PhotoCopyContent(string content)
        {
            Console.WriteLine("PhotoCopy Done"); 
            return true;
        }
        
        public bool PrintContent(string content)
        {
            Console.WriteLine("Print Done"); 
            return true;
        }
        
        public bool ScanContent(string content)
        {
            Console.WriteLine("Scan Done"); 
            return true;
        }
        
        // ❌ Forced to implement methods it doesn't support
        public bool PrintDuplexContent(string content)
        {
            return false; // Not supported
        }
        
        public bool FaxContent(string content)
        {
            return false; // Not supported
        }
    }
}
```

**Issues with this design:**
- `CannonMG2470` is forced to implement `FaxContent` and `PrintDuplexContent` even though it doesn't support these features
- The interface is too "fat" - it contains methods not relevant to all implementations
- Violates ISP because clients depend on methods they don't use
- Makes the code fragile and harder to maintain

## Code Example – Correct Implementation

**After Interface Segregation Principle:**

```csharp
namespace ISPDemoConsole
{
    // Core printing functionality
    interface IPrintScanContent
    {
        bool PrintContent(string content);
        bool ScanContent(string content);
        bool PhotoCopyContent(string content);
    }

    // Fax capability
    interface IFaxContent
    {
        bool FaxContent(string content);
    }

    // Duplex printing capability
    interface IPrintDuplex
    {
        bool PrintDuplexContent(string content);
    }
}
```

**Implementation 1: High-End Printer (Implements all features it supports)**
```csharp
namespace ISPDemoConsole.Client
{
    class HPLaserJet : IPrintScanContent, IFaxContent, IPrintDuplex
    {
        public bool FaxContent(string content)
        {
            Console.WriteLine("Fax Done"); 
            return true;
        }
        
        public bool PhotoCopyContent(string content)
        {
            Console.WriteLine("PhotoCopy Done"); 
            return true;
        }
        
        public bool PrintContent(string content)
        {
            Console.WriteLine("Print Done"); 
            return true;
        }
        
        public bool PrintDuplexContent(string content)
        {
            Console.WriteLine("Print Duplex Done"); 
            return true;
        }
        
        public bool ScanContent(string content)
        {
            Console.WriteLine("Scan Done"); 
            return true;
        }
    }
}
```

**Implementation 2: Basic Printer (Only implements what it supports)**
```csharp
namespace ISPDemoConsole.Client
{
    class CannonMG2470 : IPrintScanContent
    {
        public bool PhotoCopyContent(string content)
        {
            Console.WriteLine("PhotoCopy Done");
            return true;
        }
        
        public bool PrintContent(string content)
        {
            Console.WriteLine("Print Done");
            return true;
        }
        
        public bool ScanContent(string content)
        {
            Console.WriteLine("Scan Done");
            return true;
        }
        
        // ✅ No need to implement unsupported features!
    }
}
```

## Explanation of the Fix

The fat interface `IPrintTasks` is split into three focused interfaces:

1. **`IPrintScanContent`**: Core printing functionality (Print, Scan, PhotoCopy)
2. **`IFaxContent`**: Fax capability
3. **`IPrintDuplex`**: Duplex printing capability

**Benefits:**
- `HPLaserJet` implements all three interfaces because it supports all features
- `CannonMG2470` only implements `IPrintScanContent` because it only supports basic features
- No class is forced to implement methods it doesn't need
- Each interface is cohesive and focused on a specific capability
- Easy to add new printer models with different feature sets

## When to Use / When NOT to Overuse

**Use ISP when:**
- Interfaces are growing and contain many methods
- Different clients use different subsets of the interface
- You find classes with empty implementations or throwing `NotImplementedException`
- Unrelated classes are implementing the same interface

**Don't Overuse:**
- Avoid creating too many trivial one-method interfaces
- Don't split interfaces if all clients need all methods
- Balance between cohesion and over-fragmentation

## Real-world / Enterprise Use Case

### ASP.NET Core Example
Use separate interfaces for repositories, services, and background jobs to avoid bloated contracts.

```csharp
// ❌ Fat interface
public interface IRepository
{
    Task<T> GetByIdAsync<T>(int id);
    Task SaveAsync<T>(T entity);
    Task DeleteAsync<T>(int id);
    Task<List<T>> GetAllAsync<T>();
    Task BulkInsertAsync<T>(List<T> entities);
    Task ExecuteRawSqlAsync(string sql);
}

// ✅ Segregated interfaces
public interface IReadRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
}

public interface IWriteRepository<T>
{
    Task SaveAsync(T entity);
    Task DeleteAsync(int id);
}

public interface IBulkRepository<T>
{
    Task BulkInsertAsync(List<T> entities);
}
```

### Microservices Example
```csharp
// Each service only depends on what it needs
public class OrderService
{
    private readonly IReadRepository<Order> _orderReader;
    private readonly IWriteRepository<Order> _orderWriter;
    
    // Only uses read/write, not bulk operations
}

public class DataMigrationService
{
    private readonly IBulkRepository<Order> _bulkRepository;
    
    // Only uses bulk operations
}
```

## Common Mistakes & Anti-Patterns

1. **Fat Interfaces**: Single interface with too many unrelated methods
2. **God Interfaces**: Interfaces trying to do everything
3. **Implementing interfaces with irrelevant methods**: Returning `false`, `null`, or throwing `NotImplementedException`
4. **Interface pollution**: Adding methods to existing interfaces instead of creating new ones
5. **Not considering client needs**: Designing interfaces from the provider's perspective instead of the consumer's

## Performance & Maintainability Impact

**Maintainability:** ✅ Significantly increases
- Code is easier to understand
- Changes are isolated to specific interfaces
- Less risk of breaking implementations

**Testability:** ✅ Improves
- Easier to mock smaller interfaces
- Tests are more focused

**Flexibility:** ✅ Enhanced
- New implementations can pick and choose capabilities
- Easy to extend with new interfaces

**Performance:** ➡️ Neutral
- No runtime performance impact
- Slightly more interfaces to manage at compile time

## Relation to Design Patterns

ISP works well with many design patterns:

- **Decorator Pattern**: Decorators can implement specific interfaces
- **Adapter Pattern**: Adapters can convert between segregated interfaces
- **Proxy Pattern**: Proxies can implement only needed interfaces
- **Strategy Pattern**: Different strategies implement focused interfaces
- **Dependency Injection**: Clients depend on minimal interfaces

## Interview Cross-Questions with Answers

**Q: How do you identify when to apply ISP?**  
**A:** Look for:
- Classes implementing interfaces with empty or fake implementations
- Methods throwing `NotImplementedException`
- Interface methods that are only used by some implementers
- Different clients using different subsets of an interface

**Q: How do you refactor a fat interface?**  
**A:** 
1. Analyze which methods are used together
2. Group related methods into cohesive interfaces
3. Split the fat interface into multiple role-specific interfaces
4. Update implementations to implement only relevant interfaces

**Q: Why is ISP important for microservices?**  
**A:** Services expose only what clients need, reducing coupling. Each service can implement specific capabilities without being forced to support features it doesn't provide.

**Q: What's the difference between ISP and SRP?**  
**A:** 
- **SRP** focuses on classes having one responsibility (one reason to change)
- **ISP** focuses on interfaces not forcing clients to depend on unused methods
- SRP is about implementation; ISP is about contracts

**Q: Can ISP lead to too many interfaces?**  
**A:** Yes, if overused. Balance is key. Group cohesive methods together. Don't create a separate interface for every single method.

**Q: How does ISP relate to the Xerox case study?**  
**A:** Xerox had a single Job class with all printer operations. This made changes difficult and coupled unrelated functionality. ISP solved this by segregating interfaces based on printer capabilities (print, fax, staple, etc.).

## Quick Revision / Summary

✅ **No client should be forced to depend on methods it does not use**  
✅ **Prefer many small, role-specific interfaces over one fat interface**  
✅ **Split interfaces based on client needs, not provider capabilities**  
✅ **Avoid forcing classes to implement unused methods**  
✅ **Leads to cleaner, more maintainable, and flexible code**  
✅ **Historical origin: Robert C. Martin's work with Xerox printer systems**  
✅ **Prevents empty implementations and `NotImplementedException`**

### Key Takeaway
**"Clients should not be forced to depend on interfaces they do not use."** - Robert C. Martin
