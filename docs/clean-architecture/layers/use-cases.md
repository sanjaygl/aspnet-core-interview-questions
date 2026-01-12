# Use Cases (Application Layer)

## Purpose
Coordinates application-specific business rules and orchestrates the flow of data to and from entities.

## Responsibilities
- Implement use case logic (e.g., `PlaceOrder`)
- Coordinate between entities and interfaces

## What SHOULD Be Inside
- Use case classes (e.g., `PlaceOrderHandler`)
- Application services
- Input/output models

## What SHOULD NOT Be Inside
- UI logic
- Infrastructure code (e.g., EF Core, HTTP)

## C# Example
```csharp
public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, Result>
{
    private readonly IOrderRepository _repo;
    public PlaceOrderHandler(IOrderRepository repo) { _repo = repo; }
    public async Task<Result> Handle(PlaceOrderCommand cmd, CancellationToken ct)
    {
        // Business logic here
        return Result.Success();
    }
}
```

## Dependency Direction
- Depends on Entities
- Interfaces for repositories, presenters, etc.

## Testing Strategy
- Unit tests with mocked interfaces

## Common Mistakes
- Directly using infrastructure classes
- Leaking framework dependencies
