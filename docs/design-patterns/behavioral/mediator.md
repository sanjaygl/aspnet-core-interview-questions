# Mediator Pattern (Behavioral)

## Intent / Definition
Define an object that encapsulates how a set of objects interact. Mediator promotes loose coupling by keeping objects from referring to each other explicitly.

## Problem Statement
- You want to reduce direct dependencies between objects
- Complex communication logic is scattered across classes

## When to Use / When NOT to Use
✅ **Use Mediator when:**
- Many objects interact in complex ways
- You want to centralize communication
❌ **Don't use Mediator when:**
- Communication is simple
- Overhead is not justified

## UML-style Explanation (Text)
- Mediator interface defines communication
- ConcreteMediator coordinates colleagues
- Colleagues communicate via Mediator

## C# Implementation Example
```csharp
public interface IMediator
{
    void Send(string message, Colleague colleague);
}

public abstract class Colleague
{
    protected IMediator _mediator;
    public Colleague(IMediator mediator) => _mediator = mediator;
}

public class ConcreteMediator : IMediator
{
    private List<Colleague> _colleagues = new();
    public void Register(Colleague colleague) => _colleagues.Add(colleague);
    public void Send(string message, Colleague sender)
    {
        foreach (var c in _colleagues)
            if (c != sender) Console.WriteLine($"{sender.GetType().Name} to {c.GetType().Name}: {message}");
    }
}

public class UserColleague : Colleague
{
    public UserColleague(IMediator mediator) : base(mediator) { }
    public void Send(string message) => _mediator.Send(message, this);
}

// Client usage
var mediator = new ConcreteMediator();
var user1 = new UserColleague(mediator);
var user2 = new UserColleague(mediator);
mediator.Register(user1);
mediator.Register(user2);
user1.Send("Hello!");
```

## Real-world / Enterprise Use Case
- Event bus in microservices
- UI dialog coordination
- ASP.NET Core MediatR library

## Pros and Cons
**Pros:**
- Reduces coupling
- Centralizes communication
**Cons:**
- Can become a bottleneck
- More code/complexity

## Common Mistakes & Anti-Patterns
- Overusing mediator for simple cases
- Making mediator a god object

## Performance Considerations
- Minimal overhead
- Can impact performance if mediator is overloaded

## Relation to SOLID Principles
- Supports SRP (centralizes communication)
- Supports OCP (add new colleagues)

## Interview Cross-Questions with Answers
- **Q:** How does Mediator differ from Observer?
  **A:** Mediator centralizes; Observer notifies all dependents.
- **Q:** Where is Mediator used in ASP.NET Core?
  **A:** MediatR library, event bus.

## Quick Revision / Summary
- Centralize communication between objects
- Used in event bus, MediatR, UI dialogs
