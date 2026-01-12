# Strategy Pattern (Behavioral)

## Intent / Definition
Define a family of algorithms, encapsulate each one, and make them interchangeable. Strategy lets the algorithm vary independently from clients that use it.

## Problem Statement
- You need to switch between different algorithms at runtime
- You want to avoid large switch/case or if/else blocks
- You want to follow OCP for algorithm changes

## When to Use / When NOT to Use
✅ **Use Strategy when:**
- You have multiple ways to perform an operation
- You want to select algorithm at runtime
❌ **Don't use Strategy when:**
- Only one algorithm is needed
- Overhead of abstraction is not justified

## UML-style Explanation (Text)
- Strategy interface defines algorithm
- ConcreteStrategy implements algorithm
- Context uses a Strategy

## C# Implementation Example
```csharp
public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount) => Console.WriteLine($"Paid {amount} by credit card");
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay(decimal amount) => Console.WriteLine($"Paid {amount} by PayPal");
}

public class PaymentContext
{
    private IPaymentStrategy _strategy;
    public void SetStrategy(IPaymentStrategy strategy) => _strategy = strategy;
    public void Pay(decimal amount) => _strategy.Pay(amount);
}

// Client usage
var context = new PaymentContext();
context.SetStrategy(new CreditCardPayment());
context.Pay(100);
context.SetStrategy(new PayPalPayment());
context.Pay(200);
```

## Real-world / Enterprise Use Case
- Payment processing (multiple payment methods)
- Sorting algorithms
- ASP.NET Core authentication schemes

## Pros and Cons
**Pros:**
- Swap algorithms at runtime
- Avoids conditional logic
- Follows OCP
**Cons:**
- More classes/code
- Can be overkill for simple cases

## Common Mistakes & Anti-Patterns
- Using for trivial logic
- Not making strategies truly interchangeable

## Performance Considerations
- Minimal overhead
- Can improve maintainability

## Relation to SOLID Principles
- Supports OCP (add new strategies)
- Supports SRP (separates algorithm from context)

## Interview Cross-Questions with Answers
- **Q:** How does Strategy differ from Template Method?
  **A:** Strategy uses composition; Template Method uses inheritance.
- **Q:** Where is Strategy used in ASP.NET Core?
  **A:** Authentication, sorting, payment processing.

## Quick Revision / Summary
- Encapsulate interchangeable algorithms
- Select at runtime
- Avoids conditional logic
