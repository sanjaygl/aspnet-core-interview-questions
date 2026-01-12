# SOLID Principles in C# for Enterprise .NET / ASP.NET Core

## Introduction to SOLID

SOLID is an acronym for five design principles that help developers build robust, maintainable, and scalable software. These principles are especially critical in large-scale .NET and ASP.NET Core applications, where code quality, testability, and long-term maintainability are paramount.

## Why SOLID Matters in Large-Scale Systems

- Reduces technical debt and code rot
- Improves testability and flexibility
- Enables easier onboarding and collaboration
- Supports clean architecture and microservices

## SOLID Overview Table

| Principle | Purpose | Key Benefit |
|-----------|---------|-------------|
| Single Responsibility Principle (SRP) | Each class should have one, and only one, reason to change | Simpler, more maintainable code |
| Open/Closed Principle (OCP) | Software entities should be open for extension, but closed for modification | Easier to add features, fewer bugs |
| Liskov Substitution Principle (LSP) | Subtypes must be substitutable for their base types | Reliable polymorphism |
| Interface Segregation Principle (ISP) | No client should be forced to depend on methods it does not use | Leaner, more focused interfaces |
| Dependency Inversion Principle (DIP) | Depend on abstractions, not concretions | Decoupled, testable code |

## SOLID Principles Index

- [Single Responsibility Principle](single-responsibility-principle.md)
- [Open/Closed Principle](open-closed-principle.md)
- [Liskov Substitution Principle](liskov-substitution-principle.md)
- [Interface Segregation Principle](interface-segregation-principle.md)
- [Dependency Inversion Principle](dependency-inversion-principle.md)

## Cross-Principle Comparison & Scenarios

### SRP vs Separation of Concerns
- SRP focuses on a single reason to change per class; Separation of Concerns is broader, about dividing a system into distinct features.

### OCP vs Strategy Pattern
- OCP is a principle; Strategy is a pattern that helps achieve OCP by allowing behavior to be selected at runtime.

### ISP vs Fat Interfaces
- ISP avoids fat interfaces by splitting them into smaller, role-specific ones.

### DIP vs Service Locator
- DIP encourages constructor injection and explicit dependencies; Service Locator is an anti-pattern that hides dependencies.

## Cross-Principle Interview Questions

1. **Which SOLID principle is violated and why?**
   - Analyze code for multiple responsibilities, tight coupling, or fat interfaces.
2. **How to refactor legacy code to follow SOLID?**
   - Identify violations, extract classes/interfaces, use DI, and apply patterns.
3. **How does SOLID apply to Microservices & Clean Architecture?**
   - Promotes loose coupling, clear boundaries, and testability.
4. **Which principles are most commonly violated?**
   - SRP and DIP are often overlooked in legacy codebases.

## Quick Revision / Cheat Sheet

- **SRP:** One reason to change per class (e.g., separate logging from business logic)
- **OCP:** Extend via new classes, not by modifying existing ones (e.g., new payment methods)
- **LSP:** Subclasses must honor base class contracts (e.g., derived controllers in ASP.NET Core)
- **ISP:** Prefer many small interfaces (e.g., `IRepository<T>`, `ILogger`)
- **DIP:** Inject abstractions, not concrete classes (e.g., use DI container)

---

**Real-world mapping:**
- Controllers, Services, and Repositories in ASP.NET Core should each follow SRP
- Middleware and Filters often use OCP and DIP
- Interface-based programming is key for testability and maintainability
