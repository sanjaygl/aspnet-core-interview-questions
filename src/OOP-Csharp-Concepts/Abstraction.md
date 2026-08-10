# Abstraction – Comprehensive Notes

## Part A: ABSTRACT CLASS

```csharp
namespace OOP_Concepts.Abstraction
{
    // =========================================================================
    // PART A: ABSTRACT CLASS
    // =========================================================================
    // An abstract class is a partially implemented blueprint.
    //
    // It can contain:
    // - Instance fields
    // - Instance constructors
    // - Abstract members
    // - Concrete members
    // - Virtual members
    // - Static members
    // - Constants
    //
    // An abstract class cannot be instantiated directly.
    // =========================================================================

    internal abstract class ConceptBase
    {
        // ---------------------------------------------------------------------
        // 1. INSTANCE FIELD
        // ---------------------------------------------------------------------
        // Abstract classes can contain instance state.
        // ---------------------------------------------------------------------

        private string internalState = "Sanjay";

        // ---------------------------------------------------------------------
        // 2. INSTANCE READONLY FIELD
        // ---------------------------------------------------------------------
        // A readonly instance field can be initialized when the object is
        // created and cannot be reassigned afterward.
        // ---------------------------------------------------------------------

        protected readonly string instanceId =
            Guid.NewGuid().ToString();

        // ---------------------------------------------------------------------
        // 3. INSTANCE CONSTRUCTOR
        // ---------------------------------------------------------------------
        // An abstract class can have an instance constructor.
        // It is executed when a derived class object is created.
        // ---------------------------------------------------------------------

        public ConceptBase()
        {
            // Constructor logic goes here.
        }

        // ---------------------------------------------------------------------
        // PART B: ABSTRACT MEMBERS
        // ---------------------------------------------------------------------

        // 4. ABSTRACT PROPERTY
        // ---------------------------------------------------------------------
        // No implementation is provided here.
        // A concrete derived class must override it.
        // ---------------------------------------------------------------------

        public abstract string Name { get; set; }

        // ---------------------------------------------------------------------
        // 5. ABSTRACT GET-ONLY PROPERTY
        // ---------------------------------------------------------------------

        public abstract string RuntimeId { get; }

        // ---------------------------------------------------------------------
        // 6. ABSTRACT METHODS
        // ---------------------------------------------------------------------
        // Abstract methods have no body and must be implemented by a concrete
        // derived class.
        // ---------------------------------------------------------------------

        public abstract string PrintMessage(string message);

        public abstract string Print(string message);

        // ---------------------------------------------------------------------
        // 7. ABSTRACT EVENT
        // ---------------------------------------------------------------------

        public abstract event EventHandler OnDataChanged;

        // ---------------------------------------------------------------------
        // 8. ABSTRACT INDEXER
        // ---------------------------------------------------------------------

        public abstract string this[int index] { get; set; };


        // ---------------------------------------------------------------------
        // PART C: CONCRETE / REGULAR MEMBERS
        // ---------------------------------------------------------------------

        // 9. PRIVATE HELPER METHOD
        // ---------------------------------------------------------------------
        // An abstract class can contain normal implemented methods.
        // ---------------------------------------------------------------------

        private int InternalHelper(int a, int b)
        {
            return a + b;
        }

        // ---------------------------------------------------------------------
        // 10. VIRTUAL / CONCRETE METHOD
        // ---------------------------------------------------------------------
        // A virtual method already has an implementation.
        // A derived class may optionally override it.
        // ---------------------------------------------------------------------

        public virtual decimal Discount(decimal discount)
        {
            int temporaryCalculation =
                InternalHelper(5, 5);

            return discount;
        }

        // ---------------------------------------------------------------------
        // 11. STATIC FIELD AND METHOD
        // ---------------------------------------------------------------------
        // Static members belong to the type rather than an object.
        // ---------------------------------------------------------------------

        public static string ClassVersion = "v2.0";

        public static void ShowVersion()
        {
            Console.WriteLine($"Current version: {ClassVersion}");
        }

        // ---------------------------------------------------------------------
        // 12. CONSTANT
        // ---------------------------------------------------------------------
        // A const value is evaluated at compile time.
        // ---------------------------------------------------------------------

        public const int MaxRetries = 3;

        // ---------------------------------------------------------------------
        // 13. STATIC READONLY FIELD
        // ---------------------------------------------------------------------
        // Initialized at runtime and cannot be reassigned afterward.
        // ---------------------------------------------------------------------

        public static readonly string BuildDate =
            DateTime.Now.ToString("yyyy-MM-dd");
    }


    // =========================================================================
    // PART D: DERIVED CLASS
    // =========================================================================
    // A concrete derived class must implement all abstract members.
    // =========================================================================

    internal class DerivedImplementationExample : ConceptBase, IDisposable
    {
        // ---------------------------------------------------------------------
        // 14. IMPLEMENTING ABSTRACT PROPERTY
        // ---------------------------------------------------------------------

        public override string Name { get; set; }

        // ---------------------------------------------------------------------
        // 15. IMPLEMENTING ABSTRACT GET-ONLY PROPERTY
        // ---------------------------------------------------------------------

        public override string RuntimeId => instanceId;

        // ---------------------------------------------------------------------
        // 16. IMPLEMENTING ABSTRACT METHODS
        // ---------------------------------------------------------------------

        public override string Print(string message)
        {
            return message + " " +
                   PrintMessage(ConceptBase.ClassVersion);
        }

        public override string PrintMessage(string message) =>
            $"ClassVersion: {message}, " +
            $"MaxRetries: {ConceptBase.MaxRetries}, " +
            $"BuildDate: {ConceptBase.BuildDate}";

        // ---------------------------------------------------------------------
        // 17. IMPLEMENTING ABSTRACT EVENT
        // ---------------------------------------------------------------------

        public override event EventHandler OnDataChanged;

        // ---------------------------------------------------------------------
        // 18. IMPLEMENTING ABSTRACT INDEXER
        // ---------------------------------------------------------------------

        public override string this[int index]
        {
            get => "Value";
            set { }
        }

        // ---------------------------------------------------------------------
        // 19. IMPLEMENTING INTERFACE
        // ---------------------------------------------------------------------
        // A class can inherit from one class and implement multiple interfaces.
        // ---------------------------------------------------------------------

        public void Dispose()
        {
            // Cleanup code.
        }
    }


    // =========================================================================
    // PART E: ABSTRACT CLASS INHERITANCE RULES
    // =========================================================================

    // A class can inherit from only ONE class.
    //
    // ❌ A class cannot inherit from multiple abstract classes.
    //
    // A struct cannot inherit from an abstract class.
    // Only classes can inherit from classes.


    // =========================================================================
    // PART F: EXECUTION PLATFORM
    // =========================================================================

    internal class AbstractionRunner
    {
        public static void Run()
        {
            // =================================================================
            // 1. ABSTRACT CLASS CANNOT BE INITIALIZED DIRECTLY
            // =================================================================

            // ConceptBase baseObject = new ConceptBase();
            // ❌ Compile error:
            // Cannot create an instance of the abstract class.

            // =================================================================
            // 2. ABSTRACT CLASS REFERENCE CAN POINT TO DERIVED OBJECT
            // =================================================================

            ConceptBase concept =
                new DerivedImplementationExample();

            concept.Name = "Sanjay";

            Console.WriteLine(concept.Name);
            Console.WriteLine(concept.RuntimeId);

            // =================================================================
            // 3. ABSTRACT METHOD
            // =================================================================

            Console.WriteLine(
                concept.PrintMessage("Hello"));

            // =================================================================
            // 4. CONCRETE / VIRTUAL METHOD
            // =================================================================

            Console.WriteLine(
                concept.Discount(100));

            // =================================================================
            // 5. STATIC MEMBERS
            // =================================================================

            Console.WriteLine(ConceptBase.ClassVersion);
            Console.WriteLine(ConceptBase.MaxRetries);
            Console.WriteLine(ConceptBase.BuildDate);
        }
    }
}
```

---

## Part G: QUICK INTERVIEW QUESTIONS

```csharp
// =========================================================================
// PART G: QUICK INTERVIEW QUESTIONS
// =========================================================================
//
// 1. What is abstraction, and why do we use it?
// 2. What is an abstract class?
// 3. Can an abstract class be initialized?
// 4. How is an abstract class initialized?
// 5. Can an abstract class have a constructor?
// 6. Can an abstract class have a private constructor?
// 7. Can an abstract class have a static constructor?
// 8. Can an abstract class have concrete methods?
// 9. Can an abstract class have virtual methods?
// 10. Can an abstract class have static members?
// 11. Can an abstract class have fields?
// 12. Can an abstract class have abstract properties?
// 13. Can an abstract class have abstract events and indexers?
// 14. Can a non-abstract class have abstract members?
// 15. Can an abstract class be sealed?
// 16. Can an abstract method have a body?
// 17. Can an abstract class inherit from another abstract class?
// 18. Can a class inherit from multiple abstract classes?
// 19. Can a struct inherit from an abstract class?
// 20. What is the difference between an abstract class and an interface?
//
// =========================================================================
```

---

# Explanation

### 1. What is Abstraction?

Abstraction focuses on exposing the required behavior while hiding unnecessary implementation details.

An abstract class can define what a derived class must implement while also providing common implementation.

---

### 2. What is an Abstract Class?

An abstract class is a class that cannot be instantiated directly.

```csharp
abstract class ConceptBase
{
}
```

It can contain both abstract and concrete members.

---

### 3. Can an Abstract Class Be Initialized?

An abstract class cannot be instantiated directly:

```csharp
// Not allowed
ConceptBase obj = new ConceptBase();
```

However, an abstract-class reference can point to an object of a concrete derived class:

```csharp
ConceptBase obj =
    new DerivedImplementationExample();
```

---

### 4. How Is an Abstract Class Initialized?

The abstract class itself is not directly instantiated.

When a derived class object is created, the base-class constructor executes as part of the derived object's construction.

```text
Derived object creation
        ↓
Base constructor
        ↓
Derived constructor
```

---

### 5. Can an Abstract Class Have a Constructor?

Yes.

```csharp
public ConceptBase()
{
}
```

The constructor is executed when a derived class object is created.

---

### 6. Can an Abstract Class Have a Private Constructor?

Yes.

A private constructor can be used when only the class itself or its allowed construction mechanisms should invoke it.

However, a private constructor prevents derived classes from directly calling that constructor, so it affects whether the class can be inherited in the normal way.

---

### 7. Can an Abstract Class Have a Static Constructor?

Yes.

```csharp
static ConceptBase()
{
}
```

A static constructor is used to initialize static members and runs automatically for the type.

---

### 8. Can an Abstract Class Have Concrete Methods?

Yes.

```csharp
public void Print()
{
    Console.WriteLine("Implementation");
}
```

An abstract class can contain both abstract and concrete methods.

---

### 9. Can an Abstract Class Have Virtual Methods?

Yes.

```csharp
public virtual void Print()
{
}
```

A derived class can optionally override a virtual method.

---

### 10. Can an Abstract Class Have Static Members?

Yes.

```csharp
public static string ClassVersion = "v2.0";
```

Static members belong to the type rather than an instance.

---

### 11. Can an Abstract Class Have Fields?

Yes.

It can have:

```csharp
private string internalState;
protected readonly string instanceId;
```

---

### 12. Can an Abstract Class Have Abstract Properties?

Yes.

```csharp
public abstract string Name { get; set; }
```

A concrete derived class must implement the property.

---

### 13. Can an Abstract Class Have Abstract Events and Indexers?

Yes.

```csharp
public abstract event EventHandler OnDataChanged;

public abstract string this[int index] { get; set; }
```

The concrete derived class must implement them.

---

### 14. Can a Non-Abstract Class Have Abstract Members?

No.

If a class contains an abstract member, the class itself must be abstract.

```csharp
// Not allowed
class Test
{
    public abstract void Print();
}
```

Correct:

```csharp
abstract class Test
{
    public abstract void Print();
}
```

---

### 15. Can an Abstract Class Be Sealed?

No.

`abstract` means the class is designed to be inherited.

`sealed` means the class cannot be inherited.

Therefore:

```csharp
abstract sealed class Test
{
}
```

is not a valid combination for a class declaration.

---

### 16. Can an Abstract Method Have a Body?

No.

An abstract method does not provide an implementation.

```csharp
public abstract void Print();
```

A method with a body must be concrete or virtual:

```csharp
public virtual void Print()
{
}
```

---

### 17. Can an Abstract Class Inherit from Another Abstract Class?

Yes.

```csharp
abstract class Animal
{
    public abstract void MakeSound();
}

abstract class Dog : Animal
{
}
```

`Dog` can remain abstract and defer implementation to a further derived class.

---

### 18. Can a Class Inherit from Multiple Abstract Classes?

No.

C# supports single class inheritance.

```csharp
// Not allowed
class Child : AbstractA, AbstractB
{
}
```

A class can inherit from one class and implement multiple interfaces.

---

### 19. Can a Struct Inherit from an Abstract Class?

No.

Structs cannot inherit from user-defined classes.

They can implement interfaces.

---

### 20. What Is the Difference Between an Abstract Class and an Interface?

```text
Abstract Class
    ↓
Can contain state
Can have constructors
Can contain concrete methods
Can contain abstract methods
Can contain static members
Can be inherited by a class

Interface
    ↓
Defines a contract
Cannot have instance constructors
Can be implemented by classes and structs
A class can implement multiple interfaces
```

---

# Important Rules

- An abstract class cannot be instantiated directly.
- An abstract class can contain abstract and concrete members.
- An abstract class can have instance fields.
- An abstract class can have instance constructors.
- An abstract class can have static members.
- An abstract class can have a static constructor.
- Abstract methods have no implementation.
- Concrete derived classes must implement inherited abstract members.
- A non-abstract class cannot contain abstract members.
- An abstract class cannot be sealed.
- A class can inherit from only one class.
- A class can inherit from an abstract class.
- An abstract class can inherit from another abstract class.
- Structs cannot inherit from abstract classes.
- Abstract classes can implement interfaces.
- `virtual` and `abstract` are different: a virtual member has an implementation, while an abstract member does not.

---

# Important Missing Points

- Abstraction can be achieved using both **abstract classes and interfaces**.
- An abstract class can provide shared implementation while forcing derived classes to implement required behavior.
- The base constructor of an abstract class still executes when a derived object is created.
- An abstract class reference can point to a concrete derived object.
- An abstract method cannot be `private`, because a concrete derived class must be able to implement it.
