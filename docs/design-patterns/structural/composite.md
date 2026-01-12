# Composite Pattern (Structural)

## Intent / Definition
Compose objects into tree structures to represent part-whole hierarchies. Composite lets clients treat individual objects and compositions uniformly.

## Problem Statement
- You need to work with tree-like structures (menus, file systems)
- You want to treat individual and composite objects the same way

## When to Use / When NOT to Use
✅ **Use Composite when:**
- You need to represent hierarchies
- You want uniform treatment of leaf and composite nodes
❌ **Don't use Composite when:**
- Structure is flat or simple
- Performance is critical (deep trees can be slow)

## UML-style Explanation (Text)
- Component interface defines operations
- Leaf implements base behavior
- Composite holds children and delegates operations

## C# Implementation Example
```csharp
public interface IMenuComponent
{
    void Display();
}

public class MenuItem : IMenuComponent
{
    public string Name { get; set; }
    public void Display() => Console.WriteLine(Name);
}

public class Menu : IMenuComponent
{
    private readonly List<IMenuComponent> _children = new();
    public string Name { get; set; }
    public void Add(IMenuComponent component) => _children.Add(component);
    public void Display()
    {
        Console.WriteLine($"Menu: {Name}");
        foreach (var child in _children) child.Display();
    }
}

// Client usage
var mainMenu = new Menu { Name = "Main" };
mainMenu.Add(new MenuItem { Name = "Home" });
mainMenu.Add(new MenuItem { Name = "Settings" });
mainMenu.Display();
```

## Real-world / Enterprise Use Case
- UI menu systems
- Organization charts
- File system representations

## Pros and Cons
**Pros:**
- Uniform treatment of objects
- Simplifies client code
**Cons:**
- Can be overkill for simple structures
- Can make debugging harder

## Common Mistakes & Anti-Patterns
- Not enforcing uniform interface
- Overcomplicating simple hierarchies

## Performance Considerations
- Tree traversal can be slow for deep structures
- Memory usage increases with large trees

## Relation to SOLID Principles
- Supports OCP (add new components easily)
- Can violate SRP if composite grows too large

## Interview Cross-Questions with Answers
- **Q:** How does Composite differ from Decorator?
  **A:** Composite manages hierarchies; Decorator adds behavior.
- **Q:** Where is Composite used in .NET?
  **A:** UI controls, file systems, menu trees.

## Quick Revision / Summary
- Tree structure for part-whole hierarchies
- Uniform treatment of objects and groups
