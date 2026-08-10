# Static Class – Comprehensive Notes

## Part 1: Comprehensive Code Breakdown (With Advanced Static Concepts)

Here is the `StaticClass` architecture expanded to cover static constructors, memory behavior, inheritance constraints, extension methods, constants, and static members.

```csharp
using System;

namespace OOP_Concepts
{
    // ---------------------------------------------------------------------
    // PART A: ARCHITECTURAL RESTRICTIONS OF A STATIC CLASS
    // ---------------------------------------------------------------------

    /// <summary>
    /// A static class is a type used as a logical container for
    /// static functionality. It cannot be instantiated or inherited.
    /// </summary>
    internal static class StaticClass
        // : SomeBaseClass
        // ❌ Static classes cannot inherit from a custom class.
    {
        // 1. STATIC CONSTRUCTOR
        // The only constructor allowed in a static class.
        // It has no access modifier and no parameters.
        // The CLR runs it automatically before the type is first used.
        static StaticClass()
        {
            // Global/static initialization logic
        }

        // ❌ Static classes cannot contain instance constructors.
        // public StaticClass() { }

        // ❌ Static classes cannot contain instance members.
        // public int score = 0;

        // 2. STATIC FIELD
        // Belongs to the type rather than to an object.
        public static int highScore = 100;

        // 3. CONSTANT FIELD
        // A const is implicitly static and evaluated at compile time.
        public const string GameMode = "Ranked";

        // 4. STATIC READONLY FIELD
        // Initialized at runtime and cannot be reassigned afterward.
        public static readonly string BuildId =
            Guid.NewGuid().ToString();

        // 5. STATIC METHOD
        // Can be called directly using the class name.
        public static void DisplayScore()
        {
            Console.WriteLine($"Current High Score: {highScore}");
        }

        // 6. EXTENSION METHOD
        // Extension methods must be declared inside a static class.
        public static void PrintToConsole(this string text)
        {
            Console.WriteLine($"[Extended Output]: {text}");
        }
    }


    // ---------------------------------------------------------------------
    // PART B: INTERACTION WITH NORMAL CLASSES
    // ---------------------------------------------------------------------

    // ❌ Cannot inherit from a static class.
    // class DerivedStaticClass : StaticClass { }

    internal class StaticClassImplementationExample
    {
        // ❌ Cannot create an instance of a static class.
        // StaticClass staticClass = new StaticClass();

        // 7. INSTANCE FIELD
        // Every object has its own username.
        public string username;

        // 8. STATIC FIELD IN A NORMAL CLASS
        // Shared across all objects of this class.
        public static int totalUserCount = 0;

        public void Print()
        {
            // Access static members using the class name.
            Console.WriteLine(StaticClass.highScore);

            StaticClass.DisplayScore();

            // Use the extension method.
            string greeting = "Hello OOP Study Guide";
            greeting.PrintToConsole();
        }
    }
}
```

---

## Part 2: Critical Technical Points Added

- **Inheritance Blocked:** A static class cannot be inherited. Conceptually, it is treated as `abstract` and `sealed` by the CLR/type system.
- **No Custom Base Class:** A static class cannot inherit from another user-defined class.
- **No Object Creation:** A static class cannot be instantiated using `new`.
- **No Instance Members:** A static class can contain static members but cannot contain instance fields, instance methods, instance properties, or instance constructors.
- **No Interface Implementation:** A static class cannot implement an interface because interface implementation is based on an instance type.
- **No Object-Oriented Polymorphism:** A static class cannot participate in normal instance-based inheritance and polymorphism.
- **Extension Methods:** Extension methods must be declared as `static` methods inside a `static` class. The containing class must be non-generic and top-level.
- **Static Constructor:** A static class can have a static constructor. It runs automatically once for the type when static initialization is required.
- **Static State:** Static fields belong to the type and are shared rather than created separately for each object.
- **`const` vs `static readonly`:**
  - `const` is a compile-time constant.
  - `static readonly` is initialized at runtime and cannot be reassigned after initialization.
- **Thread Safety:** Static constructor initialization is handled safely by the CLR. However, this does not make later modifications to mutable static fields automatically thread-safe.
- **Memory Lifetime:** Static data is associated with the lifetime of its type/load context. Avoid treating static fields as objects that are collected like ordinary instance objects.

---

## Part 3: Deep-Dive Questions & Answers on Static Classes

### Question 1: If a static class is thread-unsafe by default when writing to fields, how does the CLR guarantee thread safety for its initialization?

**Answer:**

The CLR guarantees that a type's static initialization is performed safely. If multiple threads access a type for the first time, the runtime ensures that the static initialization is completed before the type is used by those threads.

For example:

```csharp
static StaticClass()
{
    highScore = 100;
}
```

The static constructor is executed once for the type.

However, this does **not** mean that modifying static fields afterward is automatically thread-safe.

For example:

```csharp
public static int totalUserCount;

totalUserCount++;
```

Multiple threads can execute this operation concurrently and cause race conditions.

For shared mutable state, use appropriate synchronization such as:

```csharp
lock
Monitor
Interlocked
```

---

### Question 2: What is the exact behavioral difference between `public const` and `public static readonly` variables inside a static class?

**Answer:**

#### `const`

```csharp
public const string GameMode = "Ranked";
```

- Compile-time constant.
- Value must be known at compile time.
- The compiler can embed the value into consuming code.
- Cannot be changed after compilation.

#### `static readonly`

```csharp
public static readonly string BuildId =
    Guid.NewGuid().ToString();
```

- Runtime-initialized value.
- Can be assigned during declaration or in a static constructor.
- Cannot be reassigned after initialization.
- Useful when the value is determined at runtime.

### Key Difference

```text
const
    ↓
Compile time

static readonly
    ↓
Runtime
```

A practical example:

```csharp
public const int MaxRetries = 3;

public static readonly string BuildId =
    Guid.NewGuid().ToString();
```

`MaxRetries` is known at compile time.

`BuildId` is generated when the application runs.

---

### Question 3: Why does using static classes excessively create tight coupling in software architecture, and how do Singleton or Dependency Injection solve this?

**Answer:**

Static classes create a direct dependency on a specific implementation.

For example:

```csharp
StaticClass.DisplayScore();
```

The calling code is directly coupled to `StaticClass`.

This can make unit testing difficult because you cannot normally replace the static class with a mock implementation.

With Dependency Injection, the dependency can be represented by an interface:

```csharp
public interface IScoreService
{
    void DisplayScore();
}
```

The application can then depend on the interface:

```csharp
public class GameService
{
    private readonly IScoreService scoreService;

    public GameService(IScoreService scoreService)
    {
        this.scoreService = scoreService;
    }
}
```

A test can provide a mock/fake implementation.

### Key Difference

```text
Static class
    ↓
Direct dependency
    ↓
Tighter coupling
    ↓
Harder to mock/test

Dependency Injection
    ↓
Interface/abstraction
    ↓
Loose coupling
    ↓
Easier to mock/test
```

A Singleton can provide a single shared instance while still being an object that can implement an interface. However, Singleton itself can introduce global-state and testing concerns if overused.

---

## Quick Revision

- Static class → **cannot be instantiated**
- Static class → **cannot be inherited**
- Static class → **cannot implement interfaces**
- Static class → **only static members**
- Static class → **can have a static constructor**
- Static constructor → **runs automatically once for the type**
- `const` → **compile-time**
- `static readonly` → **runtime initialization**
- Static field → **shared by the type**
- Extension methods → **must be inside a static class**
- Static classes are useful for **stateless utility functionality**
