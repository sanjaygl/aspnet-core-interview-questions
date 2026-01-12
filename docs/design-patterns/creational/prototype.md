# Prototype Pattern (Creational)

## Intent / Definition
Create new objects by copying (cloning) an existing object, known as the prototype. Useful when object creation is costly or complex.

## Problem Statement
- Creating new instances is expensive (deep object graphs, resource-intensive setup)
- You need to avoid subclassing for every variation
- You want to decouple code from concrete classes

## When to Use / When NOT to Use
✅ **Use Prototype when:**
- Object creation is expensive or complex
- You need many similar objects
- You want to avoid subclassing for each configuration
❌ **Don't use Prototype when:**
- Objects are simple or cheap to create
- Cloning is error-prone (deep/shallow copy issues)

## UML-style Explanation (Text)
- Prototype interface defines a Clone method
- ConcretePrototype implements Clone
- Client clones prototype to create new objects

## C# Implementation Example
```csharp
public interface IPrototype<T>
{
    T Clone();
}

public class Document : IPrototype<Document>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Document Clone() => (Document)this.MemberwiseClone();
}

// Client usage
var original = new Document { Title = "Spec", Content = "Details" };
var copy = original.Clone();
```

## Real-world / Enterprise Use Case
- Cloning configuration objects
- Prototyping UI elements
- Caching and restoring object state

## Pros and Cons
**Pros:**
- Avoids costly creation
- Reduces subclassing
- Flexible object creation
**Cons:**
- Cloning can be tricky (deep vs shallow)
- Can hide dependencies

## Common Mistakes & Anti-Patterns
- Not implementing deep copy when needed
- Cloning objects with unmanaged resources

## Performance Considerations
- Faster than re-creating complex objects
- Beware of memory leaks with deep object graphs

## Relation to SOLID Principles
- Supports OCP (new types via cloning)
- Can violate SRP if clone logic is complex

## Interview Cross-Questions with Answers
- **Q:** How does Prototype differ from Factory?
  **A:** Prototype clones existing objects; Factory creates new from scratch.
- **Q:** Where is Prototype used in .NET?
  **A:** `ICloneable`, serialization, Entity Framework tracking.

## Quick Revision / Summary
- Clone objects to avoid costly creation
- Use for complex, resource-intensive objects
