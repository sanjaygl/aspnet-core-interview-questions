# Abstract Class – Compact Interview Notes

## Original Code

```csharp
namespace OOP_Concepts.Abstraction
{
    /// <summary>
    /// An abstract class is a partially built blueprint. It can contain
    /// abstract members (rules with no implementation) and concrete members
    /// (fully implemented methods/fields). Derived classes must implement
    /// the abstract members.
    /// </summary>
    internal abstract class ConceptBase
    {
        // ---------------------------------------------------------------------
        // PART A: INSTANCE STATE & INITIALIZATION
        // ---------------------------------------------------------------------

        // 1. Instance field
        // Abstract classes can directly hold instance state.
        private string internalState = "Sanjay";

        // 2. Instance readonly field
        // Can be initialized at declaration or in the constructor.
        protected readonly string instanceId = Guid.NewGuid().ToString();

        // 3. Instance constructor
        // Executes when a derived class object is created.
        public ConceptBase()
        {
            // Constructor logic
        }


        // ---------------------------------------------------------------------
        // PART B: ABSTRACT MEMBERS
        // ---------------------------------------------------------------------

        // 4. Abstract property
        // Derived class must implement it using 'override'.
        public abstract string Name { get; set; }

        // 5. Abstract get-only property
        public abstract string RuntimeId { get; }

        // 6. Abstract methods
        // They have no implementation.
        public abstract string PrintMessage(string message);
        public abstract string Print(string message);

        // 7. Abstract event
        public abstract event EventHandler OnDataChanged;

        // 8. Abstract indexer
        public abstract string this[int index] { get; set; }


        // ---------------------------------------------------------------------
        // PART C: CONCRETE / REGULAR MEMBERS
        // ---------------------------------------------------------------------

        // 9. Private helper method
        // Can contain implementation and is accessible only inside this class.
        private int InternalHelper(int a, int b)
        {
            return a + b;
        }

        // 10. Virtual / concrete method
        // Provides default implementation. Derived class can override it.
        public virtual decimal Discount(decimal discount)
        {
            int temporaryCalculation = InternalHelper(5, 5);
            return discount;
        }

        // 11. Static field and method
        // Belong to the type, not to an object.
        public static string ClassVersion = "v2.0";

        public static void ShowVersion()
        {
            Console.WriteLine($"Current version: {ClassVersion}");
        }

        // 12. Constant
        // Compile-time constant.
        public const int MaxRetries = 3;

        // 13. Static readonly field
        // Initialized at runtime and cannot be changed afterward.
        public static readonly string BuildDate =
            DateTime.Now.ToString("yyyy-MM-dd");
    }


    // ---------------------------------------------------------------------
    // PART D: USAGE & INHERITANCE RULES
    // ---------------------------------------------------------------------

    // A class can inherit from ONLY ONE class.
    // A class can implement MULTIPLE interfaces.
    //
    // A struct cannot inherit from an abstract class.
    // A struct can implement interfaces.

    internal class DerivedImplementationExample : ConceptBase, IDisposable
    {
        // 4. Implementing abstract property
        public override string Name { get; set; }

        // 5. Implementing abstract get-only property
        public override string RuntimeId => instanceId;

        // 6. Implementing abstract methods
        public override string Print(string message)
        {
            return message + " " + PrintMessage(ConceptBase.ClassVersion);
        }

        public override string PrintMessage(string message) =>
            $"ClassVersion: {message}, " +
            $"MaxRetries: {ConceptBase.MaxRetries}, " +
            $"BuildDate: {ConceptBase.BuildDate}";

        // 7. Implementing abstract event
        public override event EventHandler OnDataChanged;

        // 8. Implementing abstract indexer
        public override string this[int index]
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
private string internalState = "Sanjay";
```

An abstract class can have instance fields and maintain object state.

### 2. Readonly Instance Fields

```csharp
protected readonly string instanceId = Guid.NewGuid().ToString();
```

An abstract class can have `readonly` instance fields. They can be initialized at declaration or in an instance constructor.

### 3. Instance Constructor

```csharp
public ConceptBase()
{
}
```

An abstract class can have an instance constructor.

It cannot be called directly by creating an abstract-class object, but it executes when a derived-class object is created.

```text
new DerivedImplementationExample()
        ↓
ConceptBase constructor
        ↓
Derived constructor
```

### 4. Abstract Properties

```csharp
public abstract string Name { get; set; }
```

An abstract property has no implementation. A non-abstract derived class must implement it using `override`.

### 5. Abstract Get-Only Property

```csharp
public abstract string RuntimeId { get; }
```

The derived class must provide the implementation.

### 6. Abstract Methods

```csharp
public abstract string PrintMessage(string message);
public abstract string Print(string message);
```

Abstract methods have no body. A concrete derived class must implement them.

### 7. Abstract Events

```csharp
public abstract event EventHandler OnDataChanged;
```

The derived class must implement the event.

### 8. Abstract Indexers

```csharp
public abstract string this[int index] { get; set; }
```

The derived class must implement the indexer.

### 9. Private Methods

```csharp
private int InternalHelper(int a, int b)
{
    return a + b;
}
```

An abstract class can have private methods with implementations. They are accessible only inside the abstract class.

### 10. Virtual / Concrete Methods

```csharp
public virtual decimal Discount(decimal discount)
{
    return discount;
}
```

A virtual method has a default implementation.

The derived class can either use it as-is or override it.

### 11. Static Fields and Methods

```csharp
public static string ClassVersion = "v2.0";
```

Static members belong to the type rather than an individual object.

```csharp
ConceptBase.ClassVersion;
ConceptBase.ShowVersion();
```

### 12. Constants

```csharp
public const int MaxRetries = 3;
```

A `const` value is a compile-time constant.

### 13. Static Readonly

```csharp
public static readonly string BuildDate = DateTime.Now.ToString("yyyy-MM-dd");
```

`static readonly` belongs to the type and can be initialized at runtime.

---

# Inheritance Rules

- A class can inherit from only **one class**.
- A class can implement **multiple interfaces**.
- A struct cannot inherit from an abstract class.
- A struct can implement interfaces.
- A derived class must implement all abstract members unless the derived class is also abstract.
- An abstract class can implement interfaces.

---

# Questions

### 1. Can a non-abstract class have abstract members?

**No.**

If a class contains an abstract member, the class must also be abstract.

```csharp
abstract class Test
{
    public abstract void Print();
}
```

---

### 2. Can an abstract class be a sealed class?

**No.**

`abstract` requires inheritance, while `sealed` prevents inheritance.

---

### 3. Can an abstract class have non-abstract methods without a body?

**No.**

A normal method must have a body.

```csharp
public void Print()
{
    Console.WriteLine("Hello");
}
```

If it should not have a body, it must be abstract:

```csharp
public abstract void Print();
```

---

### 4. Can an abstract class be initialized?

An abstract class **cannot be instantiated directly**.

```csharp
// Not allowed
ConceptBase obj = new ConceptBase();
```

But it can be used as a reference type:

```csharp
ConceptBase obj = new DerivedImplementationExample();
```

---

### 5. How is an abstract class initialized?

When a concrete derived class is instantiated, the abstract base-class constructor executes first.

```text
Base constructor
      ↓
Derived constructor
```

---

### 6. Can an abstract class have a private constructor?

**Yes.**

```csharp
private ConceptBase()
{
}
```

However, a derived class cannot access a private constructor.

If the class is intended to be inherited, `protected` is normally used:

```csharp
protected ConceptBase()
{
}
```

---

### 7. Can an abstract class have a static constructor?

**Yes.**

```csharp
static ConceptBase()
{
    // Static initialization
}
```

A static constructor:

- Has no parameters.
- Has no access modifier.
- Runs automatically.
- Runs once for the type.

---

# Important Missing Points

- An abstract class can contain **abstract, virtual, and normal methods**.
- An abstract class can contain **instance and static constructors**.
- A derived class can also be **abstract**.
- If the derived class is abstract, it does not have to implement all abstract members.
- An abstract class can implement an interface without implementing all its members if the abstract class remains abstract.
- `static` members belong to the declaring type and are not polymorphic instance members.
