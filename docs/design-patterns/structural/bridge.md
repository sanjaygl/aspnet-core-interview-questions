# Bridge Pattern (Structural)

## Intent / Definition
Separate abstraction from implementation so they can vary independently. Useful for cross-platform or multi-variant systems.

## Problem Statement
- You need to decouple abstraction from implementation
- You want to avoid a combinatorial explosion of subclasses

## When to Use / When NOT to Use
✅ **Use Bridge when:**
- You have multiple dimensions of variation (e.g., shape & color)
- You want to change implementation at runtime
❌ **Don't use Bridge when:**
- Only one dimension of variation
- Simpler inheritance suffices

## UML-style Explanation (Text)
- Abstraction holds a reference to Implementor
- Implementor defines interface for implementation
- ConcreteImplementor provides implementation

## C# Implementation Example
```csharp
// Implementor
public interface IRenderer
{
    void Render(string shape);
}

public class VectorRenderer : IRenderer
{
    public void Render(string shape) => Console.WriteLine($"Drawing {shape} as vectors");
}

public class RasterRenderer : IRenderer
{
    public void Render(string shape) => Console.WriteLine($"Drawing {shape} as pixels");
}

// Abstraction
public abstract class Shape
{
    protected readonly IRenderer _renderer;
    protected Shape(IRenderer renderer) => _renderer = renderer;
    public abstract void Draw();
}

public class Circle : Shape
{
    public Circle(IRenderer renderer) : base(renderer) { }
    public override void Draw() => _renderer.Render("Circle");
}

// Client usage
Shape shape = new Circle(new VectorRenderer());
shape.Draw();
```

## Real-world / Enterprise Use Case
- Cross-platform UI toolkits
- Database drivers (abstraction over providers)
- ASP.NET Core logging providers

## Pros and Cons
**Pros:**
- Decouples abstraction from implementation
- Supports runtime changes
**Cons:**
- More classes/code
- Can be overkill for simple cases

## Common Mistakes & Anti-Patterns
- Using bridge for single-dimension variation
- Not keeping abstraction and implementation separate

## Performance Considerations
- Minimal overhead
- Can improve maintainability

## Relation to SOLID Principles
- Supports OCP (add new abstractions/implementations)
- Supports SRP (separate responsibilities)

## Interview Cross-Questions with Answers
- **Q:** How does Bridge differ from Adapter?
  **A:** Bridge separates abstraction/implementation; Adapter changes interface.
- **Q:** Where is Bridge used in .NET?
  **A:** Logging providers, database providers.

## Quick Revision / Summary
- Decouple abstraction from implementation
- Use for multi-variant or cross-platform systems
