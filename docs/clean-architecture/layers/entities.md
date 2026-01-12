# Entities Layer

## Purpose
Defines the core business objects and rules that are independent of any application or framework.

## Responsibilities
- Encapsulate enterprise-wide business rules
- Represent domain concepts (e.g., `Order`, `Customer`)

## What SHOULD Be Inside
- Domain models
- Business rule methods
- Value objects

## What SHOULD NOT Be Inside
- Infrastructure concerns (e.g., database, logging)
- UI logic
- Framework dependencies

## C# Example
```csharp
public class Order
{
    public int Id { get; set; }
    public decimal Total { get; set; }
    public void AddItem(OrderItem item) { /* ... */ }
    public bool IsValid() { /* ... */ return true; }
}
```

## Dependency Direction
- No dependencies on other layers

## Testing Strategy
- Pure unit tests for business rules

## Common Mistakes
- Adding EF Core attributes or logic
- Mixing infrastructure or UI code
