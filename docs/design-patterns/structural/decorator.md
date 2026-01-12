# Decorator Pattern

## Pattern Category
**Structural Pattern**

## Intent / Definition
The Decorator pattern allows behavior to be added to objects dynamically without altering their structure. It provides a flexible alternative to subclassing for extending functionality by wrapping objects in decorator classes that contain the behavior.

## Problem Statement
- You need to add responsibilities to objects dynamically
- Inheritance leads to class explosion or is not possible
- You want to follow OCP by extending behavior without modifying code

## When to Use / When NOT to Use
✅ **Use Decorator when:**
- You need to add features at runtime
- You want to avoid subclassing for every feature combination
- You want to follow OCP for extending behavior
❌ **Don't use Decorator when:**
- All features are known at compile time
- Simpler inheritance or composition suffices

## UML-style Explanation (Text)
- Component interface defines operations
- ConcreteComponent implements base behavior
- Decorator implements component interface and wraps a component
- ConcreteDecorator adds new behavior

## C# Implementation Example
```csharp
// Component
public interface IMessageSender
{
    void Send(string message);
}

// Concrete Component
public class EmailSender : IMessageSender
{
    public void Send(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}

// Decorator Base
public abstract class MessageSenderDecorator : IMessageSender
{
    protected readonly IMessageSender _inner;
    protected MessageSenderDecorator(IMessageSender inner) => _inner = inner;
    public virtual void Send(string message) => _inner.Send(message);
}

// Concrete Decorator
public class LoggingMessageSender : MessageSenderDecorator
{
    public LoggingMessageSender(IMessageSender inner) : base(inner) { }
    public override void Send(string message)
    {
        Console.WriteLine($"Logging: {message}");
        base.Send(message);
    }
}

// Client Usage
IMessageSender sender = new LoggingMessageSender(new EmailSender());
sender.Send("Hello World");
```

## Real-world / Enterprise Use Case
- ASP.NET Core middleware pipeline (each middleware decorates the request/response)
- Logging, caching, validation wrappers

## Pros and Cons
**Pros:**
- Add behavior at runtime
- Avoids subclass explosion
- Follows OCP

**Cons:**
- Can make debugging harder
- Many small classes

## Common Mistakes & Anti-Patterns
- Overusing for simple cases
- Not delegating to the wrapped component

## Performance Considerations
- Minimal overhead per decorator
- Can impact performance if overused in deep chains

## Relation to SOLID Principles
- Supports OCP (add new behavior without modifying existing code)
- Supports SRP (each decorator has a single responsibility)

## Interview Cross-Questions with Answers
- **Q:** How does Decorator differ from Inheritance?
  **A:** Decorator adds behavior at runtime; inheritance at compile time.
- **Q:** Where is Decorator used in ASP.NET Core?
  **A:** Middleware pipeline, logging, caching wrappers.

## Quick Revision / Summary
- Add behavior to objects at runtime
- Avoid subclass explosion
- Used in middleware, logging, and cross-cutting concerns