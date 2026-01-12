# Recommended .NET Folder Structure for Clean Architecture

## Solution Structure
```
MyApp.sln
src/
  MyApp.Core/           # Entities, interfaces
  MyApp.Application/    # Use cases, application logic
  MyApp.Infrastructure/ # EF Core, external APIs
  MyApp.Web/            # ASP.NET Core UI
```

## Project References
- `MyApp.Application` references `MyApp.Core`
- `MyApp.Infrastructure` references `MyApp.Application` and `MyApp.Core`
- `MyApp.Web` references `MyApp.Application` and `MyApp.Infrastructure`

## Naming Conventions
- Suffix projects with `.Core`, `.Application`, `.Infrastructure`, `.Web`
- Use `I` prefix for interfaces (e.g., `IOrderRepository`)
- Use `DbContext` for EF Core contexts

## Example
```
MyApp.sln
src/
  MyApp.Core/
    Entities/
    Interfaces/
  MyApp.Application/
    UseCases/
    DTOs/
  MyApp.Infrastructure/
    Data/
    Services/
  MyApp.Web/
    Controllers/
    Views/
```
