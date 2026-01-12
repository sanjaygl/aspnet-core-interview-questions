# Pitfalls and Best Practices in Clean Architecture

## Common Pitfalls

### Over-Engineering
- Adding unnecessary abstractions for simple scenarios
- Prematurely splitting projects

### Anemic Domain Model
- Entities with only properties, no behavior
- Business logic leaking into use cases or infrastructure

### Leaky Abstractions
- Exposing infrastructure details in core
- Passing EF Core entities through layers

### Performance Concerns
- Excessive mapping between layers
- Overuse of DI containers

## Best Practices
- Keep business logic in entities and use cases
- Use interfaces for infrastructure
- Test core logic in isolation
- Use MediatR for use case orchestration
- Register dependencies in DI container

## Before/After Example
**Before:**
```csharp
public class OrderController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PlaceOrder(Order order)
    {
        _db.Orders.Add(order); // Direct DB access
        await _db.SaveChangesAsync();
        return Ok();
    }
}
```
**After:**
```csharp
public class OrderController : ControllerBase
{
    private readonly IPlaceOrderHandler _handler;
    [HttpPost]
    public async Task<IActionResult> PlaceOrder(PlaceOrderRequest req)
    {
        var result = await _handler.Handle(req);
        return Ok(result);
    }
}
```
