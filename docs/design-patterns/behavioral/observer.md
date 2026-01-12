# Observer Pattern (Behavioral)

## Intent / Definition
Define a one-to-many dependency so that when one object changes state, all its dependents are notified and updated automatically.

## Problem Statement
- You need to notify multiple objects about state changes
- You want to decouple subject and observers

## When to Use / When NOT to Use
✅ **Use Observer when:**
- Many objects depend on another object's state
- You want to implement event-driven systems
❌ **Don't use Observer when:**
- Only one object needs notification
- Tight coupling is acceptable

## UML-style Explanation (Text)
- Subject maintains list of observers
- Observers register/unregister with subject
- Subject notifies observers on state change

## C# Implementation Example
```csharp
public interface IObserver
{
    void Update(string message);
}

public class EmailObserver : IObserver
{
    public void Update(string message) => Console.WriteLine($"Email: {message}");
}

public class SmsObserver : IObserver
{
    public void Update(string message) => Console.WriteLine($"SMS: {message}");
}

public class NotificationSubject
{
    private readonly List<IObserver> _observers = new();
    public void Attach(IObserver observer) => _observers.Add(observer);
    public void Detach(IObserver observer) => _observers.Remove(observer);
    public void Notify(string message)
    {
        foreach (var observer in _observers) observer.Update(message);
    }
}

// Client usage
var subject = new NotificationSubject();
subject.Attach(new EmailObserver());
subject.Attach(new SmsObserver());
subject.Notify("Order shipped");
```

## Real-world / Enterprise Use Case
- Event bus in microservices
- UI event handling
- ASP.NET Core IHostedService notifications

## Pros and Cons
**Pros:**
- Decouples subject and observers
- Supports event-driven design
**Cons:**
- Can be hard to debug
- Risk of memory leaks if not managed

## Common Mistakes & Anti-Patterns
- Not detaching observers
- Creating circular dependencies

## Performance Considerations
- Minimal overhead for small lists
- Can be slow with many observers

## Relation to SOLID Principles
- Supports OCP (add new observers)
- Can violate SRP if subject manages too much

## Interview Cross-Questions with Answers
- **Q:** How does Observer differ from Mediator?
  **A:** Observer notifies all dependents; Mediator centralizes communication.
- **Q:** Where is Observer used in .NET?
  **A:** Events, delegates, IObservable<T>.

## Quick Revision / Summary
- One-to-many notifications
- Decouples subject and observers
- Used in events and messaging
