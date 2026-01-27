# Delegates in C# – A Practical Guide with Real Examples

## Introduction

Delegates in C# are type-safe function pointers that allow you to pass methods as parameters, store them in variables, and invoke them dynamically. They are fundamental to event-driven programming, callbacks, and implementing flexible architectures. Understanding delegates is essential for any .NET developer, especially when working with events, LINQ, and asynchronous programming.

## What is a Delegate?

A delegate is a reference type that defines a method signature. It can hold references to one or more methods that match its signature and invoke them safely. Think of it as a type-safe function pointer that knows what parameters a method should accept and what it should return.

Delegates enable decoupling in your code by allowing you to pass behavior (methods) as parameters without knowing the implementation details at compile time.

## Basic Delegate Example

Here's the simplest form of a delegate:

```csharp
// Step 1: Declare a delegate type
public delegate void MessageDelegate(string message);

public class DelegateBasics
{
    // Step 2: Create a method that matches the delegate signature
    public static void PrintMessage(string message)
    {
        Console.WriteLine($"Message: {message}");
    }

    public static void Demo()
    {
        // Step 3: Create a delegate instance and assign the method
        MessageDelegate del = PrintMessage;

        // Step 4: Invoke the delegate
        del("Hello, Delegates!");
    }
}
```

**Syntax Breakdown:**
- `delegate void MessageDelegate(string message)` – Declares a delegate type that can reference methods returning `void` and accepting a `string` parameter.
- `MessageDelegate del = PrintMessage` – Creates a delegate instance pointing to the `PrintMessage` method.
- `del("Hello, Delegates!")` – Invokes the method through the delegate.

## Real Example: Calculator Using Delegates

Let's build a practical calculator that uses delegates to perform different operations:

```csharp
// Declare a delegate for arithmetic operations
public delegate int CalculatorDelegate(int x, int y);

public class Calculator
{
    // Method 1: Add
    public static int Add(int x, int y)
    {
        return x + y;
    }

    // Method 2: Subtract
    public static int Subtract(int x, int y)
    {
        return x - y;
    }

    // Method 3: Multiply
    public static int Multiply(int x, int y)
    {
        return x * y;
    }

    // Method 4: Divide
    public static int Divide(int x, int y)
    {
        if (y == 0)
        {
            Console.WriteLine("Cannot divide by zero");
            return 0;
        }
        return x / y;
    }

    public static void Demo()
    {
        int a = 20, b = 10;

        // Assign Add method to delegate
        CalculatorDelegate operation = Add;
        Console.WriteLine($"Add: {operation(a, b)}"); // Output: 30

        // Reassign to Subtract
        operation = Subtract;
        Console.WriteLine($"Subtract: {operation(a, b)}"); // Output: 10

        // Reassign to Multiply
        operation = Multiply;
        Console.WriteLine($"Multiply: {operation(a, b)}"); // Output: 200

        // Reassign to Divide
        operation = Divide;
        Console.WriteLine($"Divide: {operation(a, b)}"); // Output: 2
    }
}
```

**Key Points:**
- The same delegate variable `operation` can reference different methods.
- All methods must match the delegate signature: `int MethodName(int, int)`.
- This allows dynamic method selection at runtime.

## What are Multicast Delegates?

A **multicast delegate** is a delegate that holds references to multiple methods. When invoked, it calls all the methods in its invocation list, in the order they were added.

**Key Operators:**
- `+=` – Adds a method to the invocation list
- `-=` – Removes a method from the invocation list

**Important Notes:**
- All methods in the invocation list are executed sequentially.
- If the delegate has a return value, only the return value of the **last method** in the list is returned.
- If any method throws an exception, subsequent methods in the list are not executed.

## Example: Multicast Delegate

Let's see multicast delegates in action with a notification system:

```csharp
// Declare a delegate for notifications
public delegate void NotificationDelegate(string message);

public class NotificationSystem
{
    public static void SendEmail(string message)
    {
        Console.WriteLine($"📧 Email sent: {message}");
    }

    public static void SendSMS(string message)
    {
        Console.WriteLine($"📱 SMS sent: {message}");
    }

    public static void LogToConsole(string message)
    {
        Console.WriteLine($"📝 Logged: {message}");
    }

    public static void Demo()
    {
        // Create multicast delegate
        NotificationDelegate notify = SendEmail;
        notify += SendSMS;
        notify += LogToConsole;

        // Invoke - all three methods will be called
        Console.WriteLine("Sending notification...\n");
        notify("System maintenance scheduled at 2 AM");

        Console.WriteLine("\n--- Removing SMS notification ---\n");

        // Remove SMS from the chain
        notify -= SendSMS;

        notify("Maintenance completed successfully");
    }
}
```

**Output:**
```
Sending notification...

📧 Email sent: System maintenance scheduled at 2 AM
📱 SMS sent: System maintenance scheduled at 2 AM
📝 Logged: System maintenance scheduled at 2 AM

--- Removing SMS notification ---

📧 Email sent: Maintenance completed successfully
📝 Logged: Maintenance completed successfully
```

### Multicast Delegate with Return Values

```csharp
public delegate int ReturnValueDelegate(int x);

public class MulticastReturn
{
    public static int Double(int x)
    {
        Console.WriteLine($"Double called: {x * 2}");
        return x * 2;
    }

    public static int Triple(int x)
    {
        Console.WriteLine($"Triple called: {x * 3}");
        return x * 3;
    }

    public static void Demo()
    {
        ReturnValueDelegate del = Double;
        del += Triple;

        // Only the last method's return value is captured
        int result = del(5);
        Console.WriteLine($"Final result: {result}"); // Output: 15 (from Triple)
    }
}
```

**Important:** When using multicast delegates with return values, only the return value of the last method in the invocation list is returned. All methods execute, but previous return values are discarded.

## Real-World Use Cases

### 1. Event Handling

Delegates are the foundation of events in C#. Events use multicast delegates to allow multiple subscribers.

```csharp
public class Button
{
    // Event based on delegate
    public delegate void ClickHandler(string message);
    public event ClickHandler OnClick;

    public void Click()
    {
        OnClick?.Invoke("Button was clicked!");
    }
}

public class EventDemo
{
    public static void Demo()
    {
        Button btn = new Button();
        
        // Subscribe to the event
        btn.OnClick += LogClick;
        btn.OnClick += NotifyUser;

        btn.Click();
    }

    static void LogClick(string msg) => Console.WriteLine($"[LOG] {msg}");
    static void NotifyUser(string msg) => Console.WriteLine($"[NOTIFICATION] {msg}");
}
```

### 2. Callbacks

Delegates are perfect for callback mechanisms where you want to notify the caller when an operation completes.

```csharp
public class DataProcessor
{
    public delegate void ProcessCompleteCallback(string result);

    public void ProcessData(int[] data, ProcessCompleteCallback callback)
    {
        // Simulate processing
        int sum = data.Sum();
        
        // Invoke callback when done
        callback($"Processing complete. Sum: {sum}");
    }
}

public class CallbackDemo
{
    public static void Demo()
    {
        DataProcessor processor = new DataProcessor();
        int[] numbers = { 1, 2, 3, 4, 5 };

        processor.ProcessData(numbers, OnProcessComplete);
    }

    static void OnProcessComplete(string result)
    {
        Console.WriteLine($"Callback received: {result}");
    }
}
```

### 3. Logging / Notification Pipelines

Build flexible logging systems where different handlers can be added or removed dynamically.

```csharp
public class Logger
{
    public delegate void LogHandler(string message);
    private LogHandler _logHandlers;

    public void AddHandler(LogHandler handler)
    {
        _logHandlers += handler;
    }

    public void RemoveHandler(LogHandler handler)
    {
        _logHandlers -= handler;
    }

    public void Log(string message)
    {
        _logHandlers?.Invoke($"[{DateTime.Now}] {message}");
    }
}

public class LoggingDemo
{
    public static void Demo()
    {
        Logger logger = new Logger();
        
        logger.AddHandler(ConsoleLog);
        logger.AddHandler(FileLog);
        
        logger.Log("Application started");
    }

    static void ConsoleLog(string msg) => Console.WriteLine($"Console: {msg}");
    static void FileLog(string msg) => Console.WriteLine($"File: {msg}");
}
```

## Delegates vs Interfaces

| **Aspect** | **Delegates** | **Interfaces** |
|------------|---------------|----------------|
| **Purpose** | Pass single method as parameter | Define contract with multiple members |
| **Use Case** | Callbacks, events, single operations | Polymorphism, multiple related methods |
| **Multicast** | Yes (can chain multiple methods) | No |
| **Syntax** | Simple, concise | Requires class/struct implementation |
| **Best For** | Event handling, short-lived operations | Long-lived objects with behavior contracts |
| **Example** | `Action<string>`, event handlers | `IComparable`, `IEnumerable` |

**When to Use Delegates:**
- You need to pass a single method as a parameter
- Building event-driven systems
- Creating callback mechanisms
- Chaining multiple method calls (multicast)

**When to Use Interfaces:**
- You need to define multiple related methods
- Creating contracts for classes to implement
- Building complex polymorphic hierarchies
- When you need properties, events, and methods together

## Built-in Delegates (Quick Reference)

C# provides built-in generic delegates to avoid declaring custom delegates:

```csharp
// Action - no return value
Action<string> action = message => Console.WriteLine(message);
action("Hello Action");

// Func - has return value
Func<int, int, int> add = (x, y) => x + y;
Console.WriteLine(add(5, 3)); // Output: 8

// Predicate - returns bool
Predicate<int> isEven = num => num % 2 == 0;
Console.WriteLine(isEven(4)); // Output: True
```

## Common Interview Questions

### Q1: What is a delegate?
**Answer:** A delegate is a type-safe function pointer in C# that references methods with a specific signature. It allows methods to be passed as parameters, stored in variables, and invoked dynamically. Delegates are the foundation for events and callback mechanisms.

### Q2: What is a multicast delegate?
**Answer:** A multicast delegate is a delegate that can reference multiple methods. You can add methods using the `+=` operator and remove them using `-=`. When invoked, all methods in the invocation list are called sequentially. If the delegate returns a value, only the last method's return value is captured.

### Q3: Can delegates have return values?
**Answer:** Yes, delegates can have return values. However, in multicast delegates, only the return value of the last method in the invocation list is returned. All methods execute, but previous return values are discarded. For scenarios requiring all return values, use `GetInvocationList()` to manually invoke each delegate.

### Q4: How are delegates used in events?
**Answer:** Events in C# are built on top of delegates. An event is essentially a multicast delegate with restricted access—only the declaring class can invoke the event, while external classes can only subscribe (`+=`) or unsubscribe (`-=`). This encapsulation makes events safer than raw delegates for implementing the observer pattern.

### Q5: What's the difference between `Func`, `Action`, and custom delegates?
**Answer:** `Func<T>` and `Action<T>` are built-in generic delegates provided by .NET. `Action` represents methods that return `void`, while `Func` represents methods that return a value. Custom delegates are needed when you want more descriptive names or when working with legacy code, but for most scenarios, `Func` and `Action` are preferred for their simplicity.

### Q6: Can a delegate reference instance methods and static methods?
**Answer:** Yes, a delegate can reference both static and instance methods, as long as they match the delegate's signature. For instance methods, the delegate maintains a reference to the object instance, while static methods don't require an instance.

### Q7: What happens if a method in a multicast delegate throws an exception?
**Answer:** If any method in a multicast delegate throws an exception, the remaining methods in the invocation list are not executed. To handle this, you can use `GetInvocationList()` to invoke each delegate individually within a try-catch block.

## Summary

**Key Takeaways:**

- **Delegates are type-safe function pointers** that enable passing methods as parameters, providing flexibility and decoupling in your code.

- **Multicast delegates** allow chaining multiple methods using `+=` and `-=` operators, making them perfect for event systems and notification pipelines.

- **Return values in multicast delegates** only capture the last method's result—use `GetInvocationList()` if you need all return values.

- **Real-world applications** include event handling, callbacks, logging systems, and implementing flexible architectures where behavior needs to be passed dynamically.

- **Use delegates for single-method scenarios** and callbacks; use interfaces when you need to define complete contracts with multiple members and properties.

---

**📚 Related Topics:**
- Events in C#
- Action, Func, and Predicate
- Anonymous Methods and Lambda Expressions
- Asynchronous Programming with Delegates

---

*Written for intermediate .NET developers preparing for technical interviews.*
