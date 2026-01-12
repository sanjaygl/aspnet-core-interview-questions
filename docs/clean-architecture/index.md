# Clean Architecture in C# and ASP.NET Core

## Introduction

**Clean Architecture** is a software design philosophy that emphasizes separation of concerns, testability, and independence from frameworks, databases, and UI. It was popularized by Robert C. Martin (Uncle Bob) to address the challenges of building maintainable, scalable, and adaptable enterprise applications.

### Why Clean Architecture?
- **Framework Independence:** Business logic is not coupled to ASP.NET Core, EF Core, or any external library.
- **Testability:** Core logic can be tested without UI, database, or web server.
- **Maintainability:** Changes in UI, database, or frameworks have minimal impact on business rules.
- **Scalability:** Supports large teams and complex domains.

### Problems It Solves
- Rigid, hard-to-change codebases
- Difficulties in testing business logic
- High coupling between layers
- Painful migrations between frameworks or databases

## Core Principles
- **Separation of Concerns:** Each layer has a distinct responsibility.
- **Independence of Frameworks:** Core logic does not depend on ASP.NET Core, EF Core, etc.
- **Testability:** Business rules are easily unit-testable.
- **UI/DB Independence:** UI and database can be swapped with minimal changes.

## Architecture Diagram (Text-Based)

```
+---------------------+
|     Entities        |
+---------------------+
          ^
          |
+---------------------+
|     Use Cases       |
+---------------------+
          ^
          |
+---------------------+
| Interface Adapters  |
+---------------------+
          ^
          |
+---------------------+
| Frameworks/Drivers  |
+---------------------+
```

## Linked Documentation Index

- [Overview](overview.md)
- [Entities Layer](layers/entities.md)
- [Use Cases Layer](layers/use-cases.md)
- [Interface Adapters Layer](layers/interface-adapters.md)
- [Frameworks & Drivers Layer](layers/frameworks-and-drivers.md)
- [Dependency Rule](dependency-rule.md)
- [.NET Folder Structure](folder-structure-dotnet.md)
- [Sample Project Walkthrough](sample-project-walkthrough.md)
- [Pitfalls & Best Practices](pitfalls-and-best-practices.md)

## Comparison Section

### Clean Architecture vs Layered Architecture
- **Clean Architecture:** Enforces dependency direction towards the core. UI, DB, and frameworks depend on business logic.
- **Layered Architecture:** Often allows dependencies in both directions, leading to tight coupling.

### Clean Architecture vs Hexagonal Architecture
- **Clean Architecture:** Focuses on concentric circles and dependency rules.
- **Hexagonal Architecture:** Emphasizes ports and adapters, but shares similar goals.

### Clean Architecture vs MVC
- **Clean Architecture:** Separates business logic from UI and infrastructure.
- **MVC:** Focuses on UI concerns; business logic often leaks into controllers or models.

## Cross-Architecture Interview Questions

**Q: When should Clean Architecture NOT be used?**
A: For simple CRUD apps or prototypes where speed is more important than maintainability, Clean Architecture may be overkill.

**Q: How do you enforce dependency rules?**
A: By using project references, dependency injection, and code reviews. Core projects should not reference infrastructure or UI projects.

**Q: How does Clean Architecture work with Microservices?**
A: Each microservice can implement Clean Architecture independently, ensuring clear boundaries and testability.

**Q: Common mistakes teams make while implementing Clean Architecture?**
A: Over-engineering, leaking infrastructure concerns into core, not enforcing dependency rules, and neglecting testing.

## Quick Revision / Cheat Sheet
- Core = Entities + Use Cases
- Adapters = Controllers, Presenters, Gateways
- Frameworks = ASP.NET Core, EF Core, external APIs
- Dependency always points inwards
- Test core logic in isolation
- Use DI for infrastructure
