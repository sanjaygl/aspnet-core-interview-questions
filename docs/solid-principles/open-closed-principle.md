# Open/Closed Principle (OCP)

## Definition / Intent
**Open/Closed Principle (OCP)** states that software entities (classes, modules, functions) should be open for extension but closed for modification.

## Problem Statement
If you need to change existing code to add new features, you risk introducing bugs and breaking existing functionality. This is common when using large switch/case or if/else blocks.

## Code Example – Violation
```csharp
public class DiscountService
{
    public decimal ApplyDiscount(Order order, string customerType)
    {
        if (customerType == "Regular")
            return order.Total * 0.95m;
        else if (customerType == "Premium")
            return order.Total * 0.90m;
        else
            return order.Total;
    }
}
```

## Code Example – Correct Implementation
```csharp
public interface IDiscountStrategy
{
    decimal ApplyDiscount(Order order);
}

public class RegularDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(Order order) => order.Total * 0.95m;
}

public class PremiumDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(Order order) => order.Total * 0.90m;
}

public class DiscountService
{
    private readonly IDiscountStrategy _discountStrategy;
    public DiscountService(IDiscountStrategy discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }
    public decimal ApplyDiscount(Order order) => _discountStrategy.ApplyDiscount(order);
}
```

## Explanation of the Fix
New discount types can be added by implementing `IDiscountStrategy` without modifying `DiscountService`.

## When to Use / When NOT to Overuse
- **Use:** When requirements change frequently or new types are expected.
- **Don't Overuse:** Avoid excessive abstraction for simple, stable logic.

## Real-world / Enterprise Use Case
In ASP.NET Core, middleware and filters are open for extension via DI, not by modifying framework code.

## Common Mistakes & Anti-Patterns
- Relying on switch/case for extensible logic
- Modifying core classes for every new requirement

## Performance & Maintainability Impact
- **Maintainability:** Increases, as new features require less risk
- **Performance:** Slight overhead from abstraction, but negligible in most cases

## Relation to Design Patterns
- Strategy, Decorator, and Template Method patterns help achieve OCP

## Interview Cross-Questions with Answers
- **Q:** How does OCP relate to plugin architectures?
  **A:** Plugins extend functionality without modifying the host application.
- **Q:** How do you refactor a switch statement to follow OCP?
  **A:** Use polymorphism and interfaces to encapsulate behavior.

## Quick Revision / Summary
- Open for extension, closed for modification
- Use interfaces and inheritance to add features
- Avoid modifying existing code for new requirements
