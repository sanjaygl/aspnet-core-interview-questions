# Command Pattern (Behavioral)

## Intent / Definition
Encapsulate a request as an object, thereby letting you parameterize clients with different requests, queue or log requests, and support undoable operations.

## Problem Statement
- You need to parameterize objects with operations
- You want to support undo/redo, logging, or queuing of requests

## When to Use / When NOT to Use
✅ **Use Command when:**
- You need to queue, log, or undo operations
- You want to decouple sender and receiver
❌ **Don't use Command when:**
- Operations are simple and don't need undo/queue
- Overhead is not justified

## UML-style Explanation (Text)
- Command interface declares Execute
- ConcreteCommand implements Execute, calls receiver
- Invoker holds and executes command
- Receiver performs the action

## C# Implementation Example
```csharp
public interface ICommand
{
    void Execute();
}

public class SaveCommand : ICommand
{
    private readonly Document _doc;
    public SaveCommand(Document doc) => _doc = doc;
    public void Execute() => _doc.Save();
}

public class Document
{
    public void Save() => Console.WriteLine("Document saved");
}

public class CommandInvoker
{
    private readonly Queue<ICommand> _commands = new();
    public void AddCommand(ICommand command) => _commands.Enqueue(command);
    public void ExecuteAll()
    {
        while (_commands.Count > 0) _commands.Dequeue().Execute();
    }
}

// Client usage
var doc = new Document();
var save = new SaveCommand(doc);
var invoker = new CommandInvoker();
invoker.AddCommand(save);
invoker.ExecuteAll();
```

## Real-world / Enterprise Use Case
- Task scheduling
- Undo/redo in UI
- CQRS command handlers

## Pros and Cons
**Pros:**
- Decouples sender and receiver
- Supports undo/redo, logging
**Cons:**
- More classes/code
- Can be overkill for simple actions

## Common Mistakes & Anti-Patterns
- Using for trivial operations
- Not supporting undo/redo when needed

## Performance Considerations
- Minimal overhead
- Can improve maintainability

## Relation to SOLID Principles
- Supports OCP (add new commands)
- Supports SRP (separates request from execution)

## Interview Cross-Questions with Answers
- **Q:** How does Command differ from Strategy?
  **A:** Command encapsulates requests; Strategy encapsulates algorithms.
- **Q:** Where is Command used in .NET?
  **A:** Task scheduling, CQRS, UI actions.

## Quick Revision / Summary
- Encapsulate requests as objects
- Supports undo, logging, queuing
- Used in CQRS and UI actions
