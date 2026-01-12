# Dependency Inversion Principle (DIP)

## Definition / Intent
**Dependency Inversion Principle (DIP)** states that high-level modules should not depend on low-level modules; both should depend on abstractions. Abstractions should not depend on details; details should depend on abstractions.

## Problem Statement
Tightly coupled code makes it hard to test, maintain, and extend. High-level business logic depending directly on concrete implementations leads to fragile systems.

## Code Example – Violation
```csharp
public class EmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        // SMTP logic here
    }
}

public class NotificationManager
{
    private readonly EmailService _emailService = new EmailService();
    public void Notify(string message)
    {
        _emailService.SendEmail("admin@company.com", "Alert", message);
    }
}
```

## Code Example – Correct Implementation
```csharp
public interface INotificationService
{
    void Notify(string message);
}

public class EmailNotificationService : INotificationService
{
    public void Notify(string message)
    {
        // SMTP logic here
    }
}

public class NotificationManager
{
    private readonly INotificationService _notificationService;
    public NotificationManager(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    public void Notify(string message)
    {
        _notificationService.Notify(message);
    }
}
```

## Explanation of the Fix
`NotificationManager` now depends on the abstraction `INotificationService`, not a concrete class. This enables easy swapping and testing.

## When to Use / When NOT to Overuse
- **Use:** When high-level modules depend on concrete implementations
- **Don't Overuse:** Avoid unnecessary abstractions for simple, stable code

## Real-world / Enterprise Use Case
In ASP.NET Core, use constructor injection to inject services, repositories, and loggers via interfaces.

## Common Mistakes & Anti-Patterns
- Service Locator pattern (hides dependencies)
- Overusing static classes

## Performance & Maintainability Impact
- **Maintainability:** Greatly increases, as code is decoupled and testable
- **Performance:** Neutral, but DI containers add minimal overhead

## Relation to Design Patterns
- Dependency Injection, Inversion of Control, and Factory patterns

## Interview Cross-Questions with Answers
- **Q:** How does DIP improve testability?
  **A:** Allows mocking dependencies in unit tests.
- **Q:** What is the difference between DIP and Service Locator?
  **A:** DIP uses explicit injection; Service Locator hides dependencies and is considered an anti-pattern.

## Quick Revision / Summary
- Depend on abstractions, not concretions
- Enables decoupling and testability
- Use DI containers in ASP.NET Core
