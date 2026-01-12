# Chain of Responsibility Pattern (Behavioral)

## Intent / Definition
Pass a request along a chain of handlers. Each handler decides either to process the request or to pass it to the next handler in the chain.

## Problem Statement
- You want to decouple sender and receiver
- Multiple objects may handle a request
- You want to build flexible processing pipelines

## When to Use / When NOT to Use
✅ **Use Chain of Responsibility when:**
- You need multiple, optional handlers for a request
- You want to build middleware or processing pipelines
❌ **Don't use Chain of Responsibility when:**
- Only one handler is needed
- Pipeline is fixed and simple

## UML-style Explanation (Text)
- Handler interface defines request handling
- ConcreteHandler processes or passes to next
- Client sends request to first handler

## C# Implementation Example
```csharp
public abstract class Handler
{
    protected Handler _next;
    public void SetNext(Handler next) => _next = next;
    public abstract void Handle(string request);
}

public class AuthHandler : Handler
{
    public override void Handle(string request)
    {
        if (request == "auth") Console.WriteLine("Auth handled");
        else _next?.Handle(request);
    }
}

public class LogHandler : Handler
{
    public override void Handle(string request)
    {
        Console.WriteLine("Logging request");
        _next?.Handle(request);
    }
}

// Client usage
var auth = new AuthHandler();
var log = new LogHandler();
auth.SetNext(log);
auth.Handle("auth");
auth.Handle("other");
```

## Real-world / Enterprise Use Case
- ASP.NET Core middleware pipeline
- Logging, validation, authorization chains

## Pros and Cons
**Pros:**
- Decouples sender and receiver
- Flexible, extensible pipelines
**Cons:**
- Can be hard to debug
- Order of handlers matters

## Common Mistakes & Anti-Patterns
- Not terminating the chain
- Overcomplicating simple flows

## Performance Considerations
- Minimal overhead per handler
- Can impact performance with long chains

## Relation to SOLID Principles
- Supports OCP (add new handlers)
- Supports SRP (separates concerns)

## Interview Cross-Questions with Answers
- **Q:** How does Chain of Responsibility differ from Command?
  **A:** Chain passes request along handlers; Command encapsulates requests.
- **Q:** Where is Chain of Responsibility used in ASP.NET Core?
  **A:** Middleware pipeline, logging, validation.

## Quick Revision / Summary
- Pass request along chain of handlers
- Used in middleware, logging, validation
