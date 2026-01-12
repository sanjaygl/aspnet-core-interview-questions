# Liskov Substitution Principle (LSP)

## Definition / Intent
**Liskov Substitution Principle (LSP)** states that objects of a superclass should be replaceable with objects of a subclass without affecting the correctness of the program.

## Problem Statement
Violating LSP leads to unexpected behavior when subclasses override or restrict base class functionality, breaking polymorphism.

## Code Example – Violation
```csharp
public class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
}

public class Square : Rectangle
{
    public override int Width { set { base.Width = base.Height = value; } }
    public override int Height { set { base.Width = base.Height = value; } }
}

// Client code
Rectangle rect = new Square();
rect.Width = 5;
rect.Height = 10; // Unexpected: both width and height become 10
```

## Code Example – Correct Implementation
```csharp
public abstract class Shape
{
    public abstract int Area();
}

public class Rectangle : Shape
{
    public int Width { get; set; }
    public int Height { get; set; }
    public override int Area() => Width * Height;
}

public class Square : Shape
{
    public int Side { get; set; }
    public override int Area() => Side * Side;
}
```

## Explanation of the Fix
`Rectangle` and `Square` are now separate types, preventing misuse and preserving substitutability.

## When to Use / When NOT to Overuse
- **Use:** When designing class hierarchies for polymorphic use
- **Don't Overuse:** Don’t force inheritance where composition is better

## Real-world / Enterprise Use Case
In ASP.NET Core, custom middleware and filters should not break the contract of the base types they extend.

## Common Mistakes & Anti-Patterns
- Overriding methods to throw exceptions
- Subclasses that restrict base class behavior

## Performance & Maintainability Impact
- **Maintainability:** Increases, as contracts are preserved
- **Performance:** Neutral

## Relation to Design Patterns
- Factory Method, Template Method, and Adapter patterns rely on LSP

## Interview Cross-Questions with Answers
- **Q:** How do you detect LSP violations?
  **A:** When a subclass cannot be used in place of a base class without errors or unexpected results.
- **Q:** Why is LSP important for unit testing?
  **A:** It ensures mocks and stubs behave like real objects.

## Quick Revision / Summary
- Subtypes must be substitutable for base types
- Avoid breaking contracts in subclasses
- Enables reliable polymorphism
