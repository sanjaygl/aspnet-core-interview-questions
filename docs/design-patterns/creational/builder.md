# Builder Pattern

## Pattern Category
**Creational Pattern**

## Intent / Definition
The Builder pattern constructs complex objects step by step. It allows you to produce different types and representations of an object using the same construction code, separating the construction process from the representation.

## Problem Statement
- You need to create complex objects with many optional parameters
- Constructor has too many parameters (telescoping constructor problem)
- Object creation requires multiple steps in specific order
- You want to create different representations of the same object
- Immutable objects need to be constructed step by step

## When to Use
✅ **Use Builder when:**
- Object has many optional parameters
- Construction process is complex and multi-step
- You need different representations of the same object
- You want to create immutable objects

## When NOT to Use
❌ **Don't use Builder when:**
- Object construction is simple
- Only a few parameters are required
- Construction logic does not vary

## UML-style Explanation (Text)
- Builder interface defines construction steps
- ConcreteBuilder implements steps for specific product
- Director orchestrates construction (optional)
- Client uses builder to get the product

## C# Implementation Example
```csharp
// Product
public class Report
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }
}

// Builder Interface
public interface IReportBuilder
{
    void SetTitle(string title);
    void SetContent(string content);
    void SetFooter(string footer);
    Report Build();
}

// Concrete Builder
public class PdfReportBuilder : IReportBuilder
{
    private Report _report = new Report();
    public void SetTitle(string title) => _report.Title = title;
    public void SetContent(string content) => _report.Content = content;
    public void SetFooter(string footer) => _report.Footer = footer;
    public Report Build() => _report;
}

// Director (optional)
public class ReportDirector
{
    private readonly IReportBuilder _builder;
    public ReportDirector(IReportBuilder builder) => _builder = builder;
    public Report BuildStandardReport()
    {
        _builder.SetTitle("Monthly Report");
        _builder.SetContent("Report Content");
        _builder.SetFooter("Confidential");
        return _builder.Build();
    }
}

// Client Usage
var builder = new PdfReportBuilder();
var director = new ReportDirector(builder);
Report report = director.BuildStandardReport();
```

## Real-world / Enterprise Use Case
- Building complex DTOs or ViewModels in ASP.NET Core
- Fluent configuration APIs (e.g., `IServiceCollection`)
- Generating documents, queries, or configuration objects

## Pros and Cons
**Pros:**
- Step-by-step construction
- Supports different representations
- Improves code readability
- Avoids telescoping constructors

**Cons:**
- More classes/code
- Can be overkill for simple objects

## Common Mistakes & Anti-Patterns
- Using builder for trivial objects
- Not making builder fluent (chaining)
- Forgetting to reset builder state

## Performance Considerations
- Minimal overhead
- Can improve maintainability for complex objects

## Relation to SOLID Principles
- Supports SRP (separates construction from representation)
- Supports OCP (add new builders for new representations)

## Interview Cross-Questions with Answers
- **Q:** How does Builder differ from Factory?
  **A:** Builder constructs complex objects step by step; Factory creates objects in one step.
- **Q:** Where is Builder used in ASP.NET Core?
  **A:** Fluent configuration APIs, e.g., `IApplicationBuilder`, `IServiceCollection`.

## Quick Revision / Summary
- Step-by-step construction of complex objects
- Avoids telescoping constructors
- Use for objects with many optional parameters