# Single Responsibility Principle (SRP)

## Definition / Intent
**Single Responsibility Principle (SRP)** states that a class should have only one reason to change, meaning it should have only one job or responsibility.

## Problem Statement
When a class handles multiple responsibilities (e.g., business logic, data access, and logging), changes in one responsibility can break or affect others, making the code hard to maintain and test.

## Code Example – Violation
```csharp
public class InvoiceService
{
    public void CreateInvoice(Invoice invoice)
    {
        // Business logic
        // ...
        SaveToDatabase(invoice);
        LogInvoiceCreation(invoice);
    }
    private void SaveToDatabase(Invoice invoice) { /* ... */ }
    private void LogInvoiceCreation(Invoice invoice) { /* ... */ }
}
```

## Code Example – Correct Implementation
```csharp
public class InvoiceService
{
    private readonly IInvoiceRepository _repository;
    private readonly ILogger _logger;
    public InvoiceService(IInvoiceRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public void CreateInvoice(Invoice invoice)
    {
        // Business logic
        _repository.Save(invoice);
        _logger.Log($"Invoice created: {invoice.Id}");
    }
}
```

## Explanation of the Fix
Responsibilities are separated into `IInvoiceRepository` and `ILogger` abstractions. `InvoiceService` now only coordinates the process.

## When to Use / When NOT to Overuse
- **Use:** When a class is growing and taking on unrelated responsibilities.
- **Don't Overuse:** Don't create trivial classes for every tiny responsibility; group cohesive logic.

## Real-world / Enterprise Use Case
In ASP.NET Core, Controllers should delegate business logic to Services, not mix HTTP, business, and data access logic.

## Common Mistakes & Anti-Patterns
- God classes (do everything)
- Mixing UI, business, and data logic

## Performance & Maintainability Impact
- **Maintainability:** Increases, as changes are isolated
- **Performance:** Neutral, but testability and refactoring improve

## Relation to Design Patterns
- Facilitates use of patterns like Repository, Service, and Command

## Interview Cross-Questions with Answers
- **Q:** How do you identify SRP violations?
  **A:** Look for classes with multiple unrelated methods or reasons to change.
- **Q:** How does SRP help in microservices?
  **A:** Each service focuses on a single business capability, making scaling and deployment easier.

## Quick Revision / Summary
- One reason to change per class
- Promotes maintainability and testability
- Avoids god classes
