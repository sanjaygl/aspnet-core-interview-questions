# Clean Architecture Overview

Clean Architecture is a set of organizing principles for enterprise applications. It separates the system into concentric layers, each with clear responsibilities and dependency rules. The innermost layers contain business logic, while the outer layers handle infrastructure and frameworks.

## Key Benefits
- Decouples business logic from frameworks
- Improves testability and maintainability
- Enables easier refactoring and scaling

## Layers
- **Entities:** Core business objects and rules
- **Use Cases:** Application-specific business logic
- **Interface Adapters:** Controllers, presenters, gateways
- **Frameworks & Drivers:** UI, database, external services

## Real-World Example
In an ASP.NET Core application, controllers (Interface Adapters) receive HTTP requests, invoke use cases (Application Layer), which manipulate entities (Domain Layer), and interact with repositories (Frameworks & Drivers).

## When to Use
- Large, complex, or long-lived applications
- Teams requiring high test coverage
- Systems needing flexibility in UI or database

## When Not to Use
- Simple, short-lived projects
- Prototypes or MVPs
