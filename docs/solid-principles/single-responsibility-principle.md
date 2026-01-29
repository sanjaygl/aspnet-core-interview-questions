# Single Responsibility Principle (SRP)

## Definition / Intent

**Single Responsibility Principle (SRP)** states that a class should have only one reason to change, meaning it should have only one job or responsibility.

Each class and module should focus on a single task at a time. Everything in the class should be related to that single purpose. There can be many members in the class as long as they are related to the single responsibility.

## Benefits of SRP

* Classes become smaller and cleaner
* Code is less fragile
* Improves maintainability and testability
* Changes are isolated to specific responsibilities

## Problem Statement

When a class handles multiple responsibilities (e.g., user management, logging, and email notifications), changes in one responsibility can break or affect others, making the code hard to maintain and test.

## Code Example – Violation

```csharp
using System;

namespace SRPDemo
{
    interface IUser
    {
        bool Login(string username, string password);
        bool Register(string username, string password, string email);
        void LogError(string error);
        bool SendEmail(string emailContent);
    }
}
```

**Problems with this approach:**
* The `IUser` interface has **three different responsibilities**:
  1. **User Management** (Login, Register)
  2. **Logging** (LogError)
  3. **Email Communication** (SendEmail)
* Any change to logging or email logic forces changes to the user interface
* Classes implementing this interface must handle unrelated concerns
* Violates the "one reason to change" principle

## Code Example – Correct Implementation

```csharp
using System;

namespace SRPDemo
{
    interface IUser
    {
        bool Login(string username, string password);
        bool Register(string username, string password, string email);
    }

    interface ILogger
    {
        void LogError(string error);
    }

    interface IEmail
    {
        bool SendEmail(string emailContent);
    }
}
```

**Example Implementation:**

```csharp
public class UserService : IUser
{
    private readonly ILogger _logger;
    private readonly IEmail _emailService;

    public UserService(ILogger logger, IEmail emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public bool Login(string username, string password)
    {
        // User authentication logic
        try
        {
            // Validate credentials
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Login failed: {ex.Message}");
            return false;
        }
    }

    public bool Register(string username, string password, string email)
    {
        // User registration logic
        try
        {
            // Create user account
            _emailService.SendEmail($"Welcome {username}!");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Registration failed: {ex.Message}");
            return false;
        }
    }
}

public class FileLogger : ILogger
{
    public void LogError(string error)
    {
        // Log to file
        Console.WriteLine($"[ERROR] {error}");
    }
}

public class EmailService : IEmail
{
    public bool SendEmail(string emailContent)
    {
        // Send email logic
        Console.WriteLine($"Sending email: {emailContent}");
        return true;
    }
}
```

## Explanation of the Fix

Responsibilities are separated into three distinct interfaces:
* **`IUser`**: Handles only user-related operations (Login, Register)
* **`ILogger`**: Handles only logging operations (LogError)
* **`IEmail`**: Handles only email operations (SendEmail)

Each interface now has a **single reason to change**:
* User logic changes only affect `IUser`
* Logging implementation changes only affect `ILogger`
* Email logic changes only affect `IEmail`

## When to Use / When NOT to Overuse

* **Use:** When a class is growing and taking on unrelated responsibilities
* **Use:** When you find yourself saying "and" when describing what a class does (e.g., "UserService handles login AND logging AND email")
* **Don't Overuse:** Don't create trivial classes for every tiny responsibility; group cohesive logic
* **Don't Overuse:** Avoid creating too many micro-classes that make the codebase harder to navigate

## Real-world / Enterprise Use Case

In ASP. NET Core applications:
* **Controllers** should only handle HTTP requests/responses
* **Services** should handle business logic
* **Repositories** should handle data access
* **Loggers** should handle logging
* **Email Services** should handle email notifications

Example:

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUser _userService;

    public UsersController(IUser userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var result = _userService.Login(request.Username, request.Password);
        return result ? Ok() : Unauthorized();
    }
}
```

## Common Mistakes & Anti-Patterns

* **God classes** (do everything) - One class handling user management, logging, email, database access, validation, etc.
* **Mixing UI, business, and data logic** in a single class
* **Utility classes** with unrelated static methods
* **Fat interfaces** with too many unrelated methods

## Performance & Maintainability Impact

* **Maintainability:** ✅ Increases significantly, as changes are isolated to specific classes
* **Testability:** ✅ Each responsibility can be tested independently with mocks
* **Performance:** ➡️ Neutral, but dependency injection overhead is negligible
* **Refactoring:** ✅ Easier to refactor individual responsibilities without affecting others

## Relation to Design Patterns

SRP facilitates the use of many design patterns:
* **Repository Pattern**: Separates data access logic
* **Service Pattern**: Separates business logic
* **Command Pattern**: Each command has a single responsibility
* **Strategy Pattern**: Each strategy implements one algorithm
* **Dependency Injection**: Enables separation of concerns through interfaces

## Interview Cross-Questions with Answers

* **Q:** How do you identify SRP violations?
  **A:** Look for classes with multiple unrelated methods, classes that have more than one reason to change, or interfaces with methods that serve different purposes (like IUser having Login, LogError, and SendEmail).

* **Q:** How does SRP help in microservices?
  **A:** Each microservice focuses on a single business capability (e.g., UserService, EmailService, LoggingService), making scaling, deployment, and maintenance easier.

* **Q:** Can a class have multiple methods and still follow SRP?
  **A:** Yes! A class can have many methods as long as they all relate to the same responsibility. For example, `IUser` has both Login and Register because both are user management operations.

* **Q:** What's the difference between SRP and separation of concerns?
  **A:** SRP is a specific principle focusing on classes having one reason to change. Separation of Concerns is a broader design principle about organizing code into distinct sections handling different concerns.

## Quick Revision / Summary

✅ **One reason to change per class**  
✅ **Focus on a single task/responsibility**  
✅ **Multiple members allowed if related to same purpose**  
✅ **Promotes maintainability and testability**  
✅ **Avoids god classes and fat interfaces**  
✅ **Makes code smaller, cleaner, and less fragile**
