# Interface – Compact Interview Notes

## Original Code

```csharp
namespace OOP_Concepts.Abstraction
{
    /// <summary>
    /// An interface is a contract that defines members an implementing
    /// class or struct must provide.
    /// </summary>
    internal interface IConcept
    {
        // ---------------------------------------------------------------------
        // PART A: RESTRICTIONS
        // ---------------------------------------------------------------------

        // 1. No instance fields
        // Interfaces cannot hold normal instance state.
        // string name = "Sanjay";

        // 2. No instance readonly fields
        // readonly string instanceId;

        // 3. No instance constructors
        // Interfaces cannot be instantiated directly.
        // public IConcept() { }


        // ---------------------------------------------------------------------
        // PART B: STANDARD INTERFACE MEMBERS
        // ---------------------------------------------------------------------

        // 4. Property
        string Name { get; set; }

        // 5. Get-only property
        string RuntimeId { get; }

        // 6. Methods
        string PrintMessage(string message);
        string Print(string message);

        // 7. Event
        event EventHandler OnDataChanged;

        // 8. Indexer
        string this[int index] { get; set; }


        // ---------------------------------------------------------------------
        // PART C: MODERN INTERFACE FEATURES (C# 8+)
        // ---------------------------------------------------------------------

        // 9. Private method
        // Must have an implementation and can be used by default interface methods.
        private int InternalHelper(int a, int b)
        {
            return a + b;
        }

        // 10. Default interface implementation
        // Implementing classes can use this implementation without overriding it.
        public decimal Discount(decimal discount)
        {
            int temporaryCalculation = InternalHelper(5, 5);
            return discount;
        }

        // 11. Static field and method
        // Belong to the interface itself, not to the implementing object.
        public static string InterfaceVersion = "v2.0";

        public static void ShowVersion()
        {
            Console.WriteLine($"Current version: {InterfaceVersion}");
        }

        // 12. Constant
        // A const is implicitly static and is evaluated at compile time.
        public const int MaxRetries = 3;

        // 13. Static readonly field
        // Initialized at runtime and cannot be changed afterward.
        public static readonly string BuildDate =
            DateTime.Now.ToString("yyyy-MM-dd");
    }


    // ---------------------------------------------------------------------
    // PART D: USAGE & IMPLEMENTATION RULES
    // ---------------------------------------------------------------------

    // A class or struct can implement multiple interfaces.

    internal class ImplementationExample : IConcept, IDisposable
    {
        // 4. Implementing property
        public string Name { get; set; }

        // 5. Implementing get-only property
        public string RuntimeId { get; } = Guid.NewGuid().ToString();

        // 6. Implementing methods
        public string Print(string message)
        {
            return message + " " + PrintMessage(IConcept.InterfaceVersion);
        }

        public string PrintMessage(string message) =>
            $"InterfaceVersion: {message}, " +
            $"MaxRetries: {IConcept.MaxRetries}, " +
            $"BuildDate: {IConcept.BuildDate}";

        // 7. Implementing event
        public event EventHandler OnDataChanged;

        // 8. Implementing indexer
        public string this[int index]
        {
            get => "Value";
            set { }
        }

        // Implementing IDisposable
        public void Dispose()
        {
            // Cleanup code
        }
    }
}
```

---

# Explanation

### 1. Instance Fields

```csharp
// string name = "Sanjay";
```

An interface cannot have normal instance fields because it does not maintain instance state.

### 2. Instance Readonly Fields

```csharp
// readonly string instanceId;
```

An interface cannot have instance `readonly` fields.

If a read-only value is required, a get-only property can be used:

```csharp
string RuntimeId { get; }
```

### 3. Instance Constructor

```csharp
// public IConcept() { }
```

An interface cannot have an instance constructor because it cannot be instantiated directly.

---

### 4. Properties

```csharp
string Name { get; set; }
```

Defines a property contract. The implementing class or struct must provide the implementation.

### 5. Get-Only Properties

```csharp
string RuntimeId { get; }
```

Defines a read-only property contract. The implementation can provide the value in any appropriate way.

### 6. Methods

```csharp
string PrintMessage(string message);
```

A normal interface method declaration defines a contract that the implementing type must satisfy.

### 7. Events

```csharp
event EventHandler OnDataChanged;
```

Defines an event contract that the implementing type must implement.

### 8. Indexers

```csharp
string this[int index] { get; set; }
```

Defines an indexer contract so an implementing object can support syntax such as:

```csharp
object[0]
```

---

### 9. Private Methods

```csharp
private int InternalHelper(int a, int b)
{
    return a + b;
}
```

Modern C# interfaces can contain private methods, but they must have an implementation.

They are mainly useful as helper methods for default interface implementations.

### 10. Default Interface Implementation

```csharp
public decimal Discount(decimal discount)
{
    return discount;
}
```

An interface can provide a default implementation.

The implementing class does not have to override this method.

### 11. Static Members

```csharp
public static string InterfaceVersion = "v2.0";
```

Static members belong to the interface itself.

They are accessed through the interface:

```csharp
IConcept.InterfaceVersion;
IConcept.ShowVersion();
```

They do not become instance members of the implementing class.

### 12. Constants

```csharp
public const int MaxRetries = 3;
```

A constant is implicitly static and is evaluated at compile time.

It is accessed through the interface:

```csharp
IConcept.MaxRetries;
```

### 13. Static Readonly

```csharp
public static readonly string BuildDate = ...;
```

A `static readonly` member belongs to the interface and is initialized at runtime.

It cannot be reassigned after initialization.

---

# Implementation Rules

- A class can implement multiple interfaces.
- A struct can implement multiple interfaces.
- A class cannot inherit from multiple classes.
- An interface cannot be instantiated directly.
- An implementing class must implement required interface members unless the class is abstract.
- Default interface members do not have to be reimplemented.
- Interface static members belong to the interface, not the implementing class.

---

# Questions

### 1. Can an interface be initialized?

An interface cannot be instantiated directly.

```csharp
// Not allowed
IConcept obj = new IConcept();
```

But an interface can be used as a reference to an implementing object:

```csharp
IConcept obj = new ImplementationExample();
```

Here, `obj` is an `IConcept` reference, while the actual object is `ImplementationExample`.

---

### 2. Can an interface have methods with implementations?

**Yes.**

Since C# 8, interfaces can have default implementations:

```csharp
public decimal Discount(decimal discount)
{
    return discount;
}
```

The implementing class can use the default implementation or provide its own implementation.

Interfaces can also contain private methods with implementations, mainly to support default interface methods.

---

### 3. What happens if two interfaces have the same method? How can we implement them?

There are two common cases.

#### Case 1: Same method signature and same behavior

One implementation can satisfy both interfaces:

```csharp
interface IFirst
{
    void Print();
}

interface ISecond
{
    void Print();
}

class Test : IFirst, ISecond
{
    public void Print()
    {
        Console.WriteLine("Print");
    }
}
```

The single `Print()` implementation satisfies both interfaces.

#### Case 2: Different implementations are required

Use **explicit interface implementation**:

```csharp
interface IFirst
{
    void Print();
}

interface ISecond
{
    void Print();
}

class Test : IFirst, ISecond
{
    void IFirst.Print()
    {
        Console.WriteLine("First implementation");
    }

    void ISecond.Print()
    {
        Console.WriteLine("Second implementation");
    }
}
```

Usage:

```csharp
IFirst first = new Test();
first.Print();

ISecond second = new Test();
second.Print();
```

Output:

```text
First implementation
Second implementation
```

---

# Important Missing Points

- An interface can be implemented by both **classes and structs**.
- A class can implement **multiple interfaces**.
- An interface can inherit from **multiple interfaces**.
- A class implementing an interface does not have to implement a default interface method.
- Explicit interface implementation is useful when two interfaces contain members with the same name but require different behavior.
- Interface members are contracts; modern C# also allows default and static implementations.
- Interface static members are accessed through the interface type and are not instance members of the implementing class.
