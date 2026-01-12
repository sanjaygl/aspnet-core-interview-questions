# Interface Segregation Principle (ISP)

## Definition / Intent
**Interface Segregation Principle (ISP)** states that no client should be forced to depend on methods it does not use. Prefer many small, role-specific interfaces over large, general-purpose ones.

## Problem Statement
Fat interfaces force implementers to provide unnecessary or irrelevant method implementations, leading to fragile and confusing code.

## Code Example – Violation
```csharp
public interface IWorker
{
    void Work();
    void Eat();
}

public class Robot : IWorker
{
    public void Work() { /* ... */ }
    public void Eat() { throw new NotImplementedException(); }
}
```

## Code Example – Correct Implementation
```csharp
public interface IWorkable
{
    void Work();
}

public interface IFeedable
{
    void Eat();
}

public class Robot : IWorkable
{
    public void Work() { /* ... */ }
}
```

## Explanation of the Fix
Interfaces are split by responsibility. `Robot` only implements what it needs.

## When to Use / When NOT to Overuse
- **Use:** When interfaces are growing or being implemented by unrelated classes
- **Don't Overuse:** Avoid creating too many trivial interfaces

## Real-world / Enterprise Use Case
In ASP.NET Core, use separate interfaces for repositories, services, and background jobs to avoid bloated contracts.

## Common Mistakes & Anti-Patterns
- Fat interfaces
- Implementing interfaces with irrelevant methods

## Performance & Maintainability Impact
- **Maintainability:** Increases, as code is easier to understand and change
- **Performance:** Neutral

## Relation to Design Patterns
- Decorator, Adapter, and Proxy patterns benefit from ISP

## Interview Cross-Questions with Answers
- **Q:** How do you refactor a fat interface?
  **A:** Split it into smaller, role-specific interfaces.
- **Q:** Why is ISP important for microservices?
  **A:** Services expose only what clients need, reducing coupling.

## Quick Revision / Summary
- Prefer many small interfaces
- Avoid forcing classes to implement unused methods
- Leads to cleaner, more maintainable code
