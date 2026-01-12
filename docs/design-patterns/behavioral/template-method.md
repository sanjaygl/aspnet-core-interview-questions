# Template Method Pattern (Behavioral)

## Intent / Definition
Define the skeleton of an algorithm in a base class, letting subclasses override specific steps without changing the algorithm's structure.

## Problem Statement
- You want to define the outline of an operation, but allow steps to vary
- You want to avoid code duplication in subclasses

## When to Use / When NOT to Use
✅ **Use Template Method when:**
- You have invariant algorithm structure with variable steps
- You want to enforce a sequence of operations
❌ **Don't use Template Method when:**
- All steps are the same for all subclasses
- Overhead of inheritance is not justified

## UML-style Explanation (Text)
- Abstract class defines template method
- Concrete subclasses override specific steps

## C# Implementation Example
```csharp
public abstract class DataProcessor
{
    public void Process()
    {
        ReadData();
        ProcessData();
        SaveData();
    }
    protected abstract void ReadData();
    protected abstract void ProcessData();
    protected abstract void SaveData();
}

public class CsvDataProcessor : DataProcessor
{
    protected override void ReadData() => Console.WriteLine("Reading CSV");
    protected override void ProcessData() => Console.WriteLine("Processing CSV");
    protected override void SaveData() => Console.WriteLine("Saving CSV");
}

// Client usage
DataProcessor processor = new CsvDataProcessor();
processor.Process();
```

## Real-world / Enterprise Use Case
- Data import/export pipelines
- Workflow engines
- ASP.NET Core middleware base classes

## Pros and Cons
**Pros:**
- Enforces algorithm structure
- Reduces code duplication
**Cons:**
- Inflexible if steps need to change at runtime
- Can lead to fragile hierarchies

## Common Mistakes & Anti-Patterns
- Overusing inheritance
- Not making steps truly overridable

## Performance Considerations
- Minimal overhead
- Can improve maintainability

## Relation to SOLID Principles
- Supports SRP (separates algorithm from steps)
- Can violate LSP if subclasses break contract

## Interview Cross-Questions with Answers
- **Q:** How does Template Method differ from Strategy?
  **A:** Template Method uses inheritance; Strategy uses composition.
- **Q:** Where is Template Method used in ASP.NET Core?
  **A:** Middleware base classes, workflow engines.

## Quick Revision / Summary
- Skeleton of algorithm in base class
- Steps overridden by subclasses
- Used in workflows and data processing
