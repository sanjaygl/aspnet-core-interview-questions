# Facade Pattern (Structural)

## Intent / Definition
Provide a simplified, unified interface to a complex subsystem. The Facade pattern hides subsystem complexity and makes the API easier to use.

## Problem Statement
- Subsystems have complex or verbose APIs
- Clients need a simple entry point
- You want to decouple client code from subsystem details

## When to Use / When NOT to Use
✅ **Use Facade when:**
- You want to simplify a complex subsystem
- You want to decouple client code from implementation details
❌ **Don't use Facade when:**
- Subsystem is already simple
- You need fine-grained control over subsystem

## UML-style Explanation (Text)
- Facade class provides simple methods
- Delegates calls to subsystem classes
- Client interacts only with Facade

## C# Implementation Example
```csharp
// Subsystem classes
public class AuthService { public void Authenticate() { } }
public class PaymentService { public void Process() { } }
public class NotificationService { public void Notify() { } }

// Facade
public class OrderFacade
{
    private readonly AuthService _auth;
    private readonly PaymentService _payment;
    private readonly NotificationService _notify;
    public OrderFacade()
    {
        _auth = new AuthService();
        _payment = new PaymentService();
        _notify = new NotificationService();
    }
    public void PlaceOrder()
    {
        _auth.Authenticate();
        _payment.Process();
        _notify.Notify();
    }
}

// Client usage
var facade = new OrderFacade();
facade.PlaceOrder();
```

## Real-world / Enterprise Use Case
- ASP.NET Core API Gateway
- Service aggregators
- Simplifying legacy system integration

## Pros and Cons
**Pros:**
- Simplifies usage
- Reduces coupling
- Hides complexity
**Cons:**
- Can become a god object
- May hide important subsystem features

## Common Mistakes & Anti-Patterns
- Putting too much logic in the facade
- Not updating facade when subsystem changes

## Performance Considerations
- Minimal overhead
- Can improve performance by batching calls

## Relation to SOLID Principles
- Supports SRP (single entry point)
- Can violate SRP if facade grows too large

## Interview Cross-Questions with Answers
- **Q:** How does Facade differ from Adapter?
  **A:** Facade simplifies a subsystem; Adapter changes interface to match client.
- **Q:** Where is Facade used in ASP.NET Core?
  **A:** API Gateway, service aggregators.

## Quick Revision / Summary
- Simplifies complex subsystems
- Provides a single entry point
- Used in gateways and service layers
