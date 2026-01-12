# Frameworks & Drivers (Infrastructure Layer)

## Purpose
Implements technical details such as database access, external APIs, and frameworks. This is the outermost layer.

## Responsibilities
- Database context (EF Core)
- External service integrations
- File system, messaging, etc.

## What SHOULD Be Inside
- EF Core DbContext
- API clients
- Infrastructure implementations

## What SHOULD NOT Be Inside
- Business rules
- Application logic

## C# Example
```csharp
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _db;
    public OrderRepository(AppDbContext db) { _db = db; }
    public async Task<Order> GetByIdAsync(int id)
    {
        return await _db.Orders.FindAsync(id);
    }
}
```

## Dependency Direction
- Depends on abstractions in core

## Testing Strategy
- Integration tests with real or in-memory DB

## Common Mistakes
- Referencing core projects from infrastructure
- Leaking infrastructure code into core
