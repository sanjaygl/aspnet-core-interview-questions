# Proxy Pattern (Structural)

## Intent / Definition
Provide a surrogate or placeholder for another object to control access, add security, caching, or lazy loading.

## Problem Statement
- You need to control access to an object
- You want to add cross-cutting concerns (logging, caching) without changing the real object
- You want to defer object creation (lazy loading)

## When to Use / When NOT to Use
✅ **Use Proxy when:**
- You need access control, logging, or caching
- You want to add behavior without modifying the real object
❌ **Don't use Proxy when:**
- No extra control or logic is needed
- Overhead of indirection is not justified

## UML-style Explanation (Text)
- Proxy implements the same interface as the real subject
- Holds a reference to the real subject
- Delegates calls, adding extra logic as needed

## C# Implementation Example
```csharp
public interface IFileLoader
{
    string Load(string path);
}

public class FileLoader : IFileLoader
{
    public string Load(string path) => File.ReadAllText(path);
}

public class CachingFileLoaderProxy : IFileLoader
{
    private readonly IFileLoader _realLoader;
    private readonly Dictionary<string, string> _cache = new();
    public CachingFileLoaderProxy(IFileLoader realLoader) => _realLoader = realLoader;
    public string Load(string path)
    {
        if (_cache.ContainsKey(path)) return _cache[path];
        var content = _realLoader.Load(path);
        _cache[path] = content;
        return content;
    }
}

// Client usage
IFileLoader loader = new CachingFileLoaderProxy(new FileLoader());
var data = loader.Load("data.txt");
```

## Real-world / Enterprise Use Case
- Caching proxies for data access
- Security proxies for authorization
- Remote proxies for distributed systems

## Pros and Cons
**Pros:**
- Adds control without changing real object
- Supports lazy loading, caching, security
**Cons:**
- Adds complexity
- Can hide real object behavior

## Common Mistakes & Anti-Patterns
- Overusing proxy for trivial logic
- Not delegating properly to the real subject

## Performance Considerations
- Can improve performance (caching)
- Adds indirection overhead

## Relation to SOLID Principles
- Supports OCP (add new proxies)
- Can violate SRP if proxy does too much

## Interview Cross-Questions with Answers
- **Q:** How does Proxy differ from Decorator?
  **A:** Proxy controls access; Decorator adds behavior.
- **Q:** Where is Proxy used in ASP.NET Core?
  **A:** Caching, authorization, remote service proxies.

## Quick Revision / Summary
- Control access, add caching or security
- Same interface as real object
- Used for cross-cutting concerns
