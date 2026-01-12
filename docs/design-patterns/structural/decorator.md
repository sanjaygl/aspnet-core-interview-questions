# Decorator Pattern

## Pattern Category
**Structural Pattern**

## Academic Foundation

### Historical Context
The Decorator pattern was formalized by the Gang of Four (GoF) in 1994, but its roots trace back to the mathematical concept of function composition and layered architectural design. It was developed to address the limitations of inheritance for extending object behavior, especially in large systems where subclass proliferation becomes unmanageable.

### Theoretical Basis
The Decorator pattern is grounded in several computer science principles:

1. **Composition over Inheritance**: Prefer object composition to extend functionality at runtime rather than static inheritance.
2. **Open/Closed Principle (OCP)**: Classes should be open for extension but closed for modification.
3. **Single Responsibility Principle (SRP)**: Each decorator should have a single, well-defined responsibility.
4. **Recursive Composition**: Decorators can be nested, allowing complex behaviors to be built from simple components.

### Academic Classification
- **Pattern Type**: Object Structural
- **Scope**: Object-level (composition at runtime)
- **Intent**: Attach additional responsibilities to objects dynamically

## Intent / Definition
The Decorator pattern allows behavior to be added to objects dynamically, without altering their structure. It provides a flexible alternative to subclassing for extending functionality by wrapping objects in decorator classes.

### Formal Definition
*"Attach additional responsibilities to an object dynamically. Decorators provide a flexible alternative to subclassing for extending functionality."* — Gang of Four

### Academic Problem Statement
In object-oriented design, there are scenarios where:
- Objects need additional functionality that varies independently
- Inheritance leads to a combinatorial explosion of classes
- Functionality needs to be added or removed at runtime
- Multiple orthogonal features need to be combined flexibly

## Problem Statement
- You need to add responsibilities to objects dynamically
- Inheritance leads to class explosion or is not possible
- You want to follow OCP by extending behavior without modifying code
- You need to combine multiple features in various ways

## Academic Analysis

### Forces and Constraints
The Decorator pattern addresses several competing forces:

1. **Flexibility vs. Complexity**: Need for runtime flexibility vs. increased system complexity
2. **Composition vs. Inheritance**: Dynamic composition vs. static inheritance hierarchies
3. **Performance vs. Functionality**: Method call overhead vs. feature richness
4. **Transparency vs. Control**: Transparent decoration vs. explicit control over decoration

### Theoretical Participants

#### Component (Abstract Interface)
- **Role**: Defines the interface for objects that can have responsibilities added dynamically
- **Purpose**: Establishes the contract for both concrete components and decorators

#### ConcreteComponent
- **Role**: Defines an object to which additional responsibilities can be attached
- **Purpose**: Provides the base functionality that can be extended

#### Decorator (Abstract)
- **Role**: Maintains a reference to a Component object and defines an interface that conforms to Component's interface
- **Purpose**: Establishes the decoration protocol and provides default forwarding behavior

#### ConcreteDecorator
- **Role**: Adds responsibilities to the component
- **Purpose**: Implements specific behavioral extensions

### Collaboration Patterns
1. **Client → Decorator**: Client interacts with decorated object through Component interface
2. **Decorator → Component**: Decorator forwards requests to wrapped component
3. **Decorator → Self**: Decorator adds its own behavior before/after forwarding
4. **Recursive Decoration**: Decorators can wrap other decorators, creating chains

## When to Use / When NOT to Use

### Academic Criteria for Application
✅ **Use Decorator when:**
- Inheritance would create too many classes for feature combinations
- Need to add/remove responsibilities at runtime
- Features are independent and can be combined in various ways
- Want to extend functionality without modifying existing code
- Clients should be unaware of decoration

❌ **Don't use Decorator when:**
- Basic inheritance or composition suffices
- Method call overhead is unacceptable
- All features are known at compile time
- Decorators have complex interdependencies

### Academic Decision Framework
Consider these aspects:
1. **Behavioral Analysis**: Are the behaviors orthogonal and independently useful?
2. **Complexity Analysis**: Does the flexibility justify the added complexity?
3. **Performance Analysis**: Is the method call overhead acceptable?
4. **Maintenance Analysis**: Will the decorator chain be manageable?

## UML-style Explanation (Text)

### Academic UML Structure
```
Component Interface
├── ConcreteComponent (implements Component)
└── Decorator (implements Component, contains Component)
    ├── ConcreteDecoratorA (extends Decorator)
    └── ConcreteDecoratorB (extends Decorator)
```

### Structural Relationships
- **Aggregation**: Decorator aggregates Component (has-a relationship)
- **Inheritance**: Decorator inherits from Component interface (is-a relationship)
- **Composition**: Decorators can be composed recursively

### Academic Sequence Analysis
1. **Client Request**: Client calls method on outermost decorator
2. **Decorator Processing**: Each decorator adds its behavior
3. **Forwarding**: Request is forwarded to wrapped component
4. **Response Chain**: Response travels back through decorator chain
5. **Final Result**: Client receives enhanced result

## C# Implementation Example

### Academic Implementation with Comprehensive Analysis
```csharp
/// <summary>
/// Academic Implementation: Message Processing System
/// 
/// Theoretical Design:
/// - Component interface defines the contract
/// - Decorators add orthogonal concerns (logging, encryption, compression)
/// - Recursive composition allows flexible combinations
/// - Each decorator follows Single Responsibility Principle
/// </summary>

// Component Interface - defines the contract
public interface IMessageProcessor
{
    Task<string> ProcessMessageAsync(string message);
}

// ConcreteComponent - core functionality
public class BasicMessageProcessor : IMessageProcessor
{
    public async Task<string> ProcessMessageAsync(string message)
    {
        // Core message processing logic
        await Task.Delay(10); // Simulate processing
        return $"Processed: {message}";
    }
}

// Abstract Decorator - establishes decoration protocol
public abstract class MessageProcessorDecorator : IMessageProcessor
{
    protected readonly IMessageProcessor _processor;
    
    protected MessageProcessorDecorator(IMessageProcessor processor)
    {
        _processor = processor ?? throw new ArgumentNullException(nameof(processor));
    }
    
    // Default behavior: forward to wrapped component
    public virtual async Task<string> ProcessMessageAsync(string message)
    {
        return await _processor.ProcessMessageAsync(message);
    }
}

// ConcreteDecorator - adds logging concern
public class LoggingMessageProcessor : MessageProcessorDecorator
{
    private readonly ILogger _logger;
    
    public LoggingMessageProcessor(IMessageProcessor processor, ILogger logger) 
        : base(processor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    ## C# Implementation Example

    ### Academic Implementation: Message Processing System
    ```csharp
    // ...existing code...
    ```

    ### Academic Usage Analysis
    ```csharp
    // ...existing code...
    ```

    ### Academic Analysis of Implementation
    **Theoretical Benefits Demonstrated**:
    1. **Composition Flexibility**: Different combinations create different behaviors
    2. **Runtime Configuration**: Decorators can be added based on runtime conditions
    3. **Separation of Concerns**: Each decorator handles one specific concern
    4. **Interface Consistency**: All combinations implement the same interface
    5. **Testability**: Each decorator can be tested independently

    ## Real-world / Enterprise Use Case
    - ASP.NET Core middleware pipeline (each middleware decorates the request/response)
    - Logging, caching, validation wrappers

    ## Pros and Cons
    **Pros:**
    - Add behavior at runtime
    - Avoids subclass explosion
    - Follows OCP

    **Cons:**
    - Can make debugging harder
    - Many small classes

    ## Common Mistakes & Anti-Patterns
    - Overusing for simple cases
    - Not delegating to the wrapped component

    ## Performance Considerations
    - Minimal overhead per decorator
    - Can impact performance if overused in deep chains

    ## Relation to SOLID Principles
    - Supports OCP (add new behavior without modifying existing code)
    - Supports SRP (each decorator has a single responsibility)

    ## Interview Cross-Questions with Answers
    - **Q:** How does Decorator differ from Inheritance?
      **A:** Decorator adds behavior at runtime; inheritance at compile time.
    - **Q:** Where is Decorator used in ASP.NET Core?
      **A:** Middleware pipeline, logging, caching wrappers.

    ## Quick Revision / Summary
    - Add behavior to objects at runtime
    - Avoid subclass explosion
    - Used in middleware, logging, and cross-cutting concerns