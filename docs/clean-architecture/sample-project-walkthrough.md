# Sample Project Walkthrough: Clean Architecture in ASP.NET Core

## Request Flow Example

### 1. Controller (Web Layer)
Receives HTTP request and calls the use case.
```csharp
[HttpPost]
public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest req)
{
    var result = await _handler.Handle(req);
    return Ok(result);
}
```

### 2. Use Case (Application Layer)
Handles business logic and coordinates entities.
```csharp
public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, Result>
{
    public async Task<Result> Handle(PlaceOrderCommand cmd, CancellationToken ct)
    {
        // Validate, create order, etc.
        return Result.Success();
    }
}
```

### 3. Entity (Domain Layer)
Represents core business logic.
```csharp
public class Order
{
    public void AddItem(OrderItem item) { /* ... */ }
}
```

### 4. Repository (Infrastructure Layer)
Persists data using EF Core.
```csharp
public class OrderRepository : IOrderRepository
{
    public async Task<Order> GetByIdAsync(int id) { /* ... */ }
}
```

## Sequence
1. Controller receives request
2. Calls use case handler
3. Use case manipulates entities
4. Use case calls repository interface
5. Repository implementation saves to DB
