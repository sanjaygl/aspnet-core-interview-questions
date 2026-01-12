# Interface Adapters Layer

## Purpose
Converts data between the format most convenient for use cases/entities and the format required by external agents (UI, DB, APIs).

## Responsibilities
- Controllers, Presenters, ViewModels
- Repository implementations
- Mapping between domain and DTOs

## What SHOULD Be Inside
- ASP.NET Core controllers
- Presenters/ViewModels
- Repository implementations (infrastructure interfaces)

## What SHOULD NOT Be Inside
- Business rules
- Direct database or framework code (should use abstractions)

## C# Example
```csharp
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IPlaceOrderHandler _handler;
    public OrdersController(IPlaceOrderHandler handler) { _handler = handler; }
    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest req)
    {
        var result = await _handler.Handle(req);
        return Ok(result);
    }
}
```

## Dependency Direction
- Depends on Use Cases
- Implements interfaces defined in core

## Testing Strategy
- Integration tests
- Controller unit tests with mocks

## Common Mistakes
- Placing business logic in controllers
- Tight coupling to frameworks
