# Dependency Rule in Clean Architecture

## What is the Dependency Rule?
All dependencies must point inwards, towards the core business logic. Outer layers (UI, infrastructure) depend on inner layers, never the reverse.

## Dependency Inversion Principle
- High-level modules (business rules) should not depend on low-level modules (infrastructure).
- Both should depend on abstractions (interfaces).

## How DI Enforces Clean Architecture
- Use Dependency Injection (DI) to provide infrastructure implementations to core logic at runtime.
- Register implementations in ASP.NET Core's DI container.

## Correct Example
```csharp
// In Core
public interface IOrderRepository { /* ... */ }

// In Infrastructure
public class OrderRepository : IOrderRepository { /* ... */ }

// In Startup
services.AddScoped<IOrderRepository, OrderRepository>();
```

## Incorrect Example
```csharp
// Core references EF Core directly
public class OrderRepository : DbContext { /* ... */ } // ❌
```

## Enforcing Dependency Rules
- Use project references: Core projects should not reference infrastructure projects.
- Code reviews and static analysis.
