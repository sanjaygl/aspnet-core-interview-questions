# Inheritance – Comprehensive Notes

## Part A: BASIC INHERITANCE CONCEPTS

```csharp
namespace OOP_Concepts.Inheritance
{
    // =========================================================================
    // PART A: BASE CLASS
    // =========================================================================
    // Inheritance allows a child class to acquire accessible members of a
    // parent class and extend or customize the existing behavior.
    //
    // Base class -> Parent / Super class
    // Derived class -> Child / Sub class
    // =========================================================================

    public class Animal
    {
        // ---------------------------------------------------------------------
        // BASE CLASS MEMBERS
        // ---------------------------------------------------------------------
        // These members can be inherited by the derived class.
        // ---------------------------------------------------------------------

        public string Name { get; set; }

        public void Eat()
        {
            Console.WriteLine($"{Name} is eating.");
        }

        public virtual void MakeSound()
        {
            Console.WriteLine("Animal makes a sound.");
        }
    }


    // =========================================================================
    // PART B: DERIVED CLASS
    // =========================================================================
    // Dog inherits from Animal using ':'.
    // Dog gets access to the accessible members of Animal and can also add
    // its own members.
    // =========================================================================

    public class Dog : Animal
    {
        // ---------------------------------------------------------------------
        // DERIVED CLASS MEMBER
        // ---------------------------------------------------------------------

        public string Breed { get; set; }

        // ---------------------------------------------------------------------
        // METHOD OVERRIDING
        // ---------------------------------------------------------------------
        // 'override' provides a specialized implementation of the virtual
        // method defined in the base class.
        // ---------------------------------------------------------------------

        public override void MakeSound()
        {
            Console.WriteLine("Dog barks.");
        }

        // ---------------------------------------------------------------------
        // DERIVED CLASS METHOD
        // ---------------------------------------------------------------------

        public void Run()
        {
            Console.WriteLine($"{Name} is running.");
        }
    }


    // =========================================================================
    // PART C: MULTILEVEL INHERITANCE
    // =========================================================================
    // A derived class can itself become a base class for another class.
    //
    // Animal
    //    ↓
    // Dog
    //    ↓
    // Puppy
    // =========================================================================

    public class Puppy : Dog
    {
        public void Play()
        {
            Console.WriteLine($"{Name} is playing.");
        }
    }


    // =========================================================================
    // PART D: CONSTRUCTOR INHERITANCE & BASE CONSTRUCTOR
    // =========================================================================
    // Constructors are NOT inherited.
    // A derived class constructor can call the base class constructor using
    // the 'base(...)' keyword.
    // =========================================================================

    public class Vehicle
    {
        public string Brand { get; }

        protected Vehicle(string brand)
        {
            Brand = brand;
            Console.WriteLine("Vehicle constructor executed.");
        }
    }

    public class Car : Vehicle
    {
        public int NumberOfDoors { get; }

        public Car(string brand, int numberOfDoors)
            : base(brand)
        {
            NumberOfDoors = numberOfDoors;
            Console.WriteLine("Car constructor executed.");
        }
    }


    // =========================================================================
    // PART E: ACCESS MODIFIERS IN INHERITANCE
    // =========================================================================
    // public    -> accessible from anywhere.
    // protected -> accessible inside the declaring class and derived classes.
    // private   -> accessible only inside the declaring class.
    // =========================================================================

    public class Parent
    {
        public int PublicValue;

        protected int ProtectedValue;

        private int PrivateValue;

        public void ShowValues()
        {
            Console.WriteLine(PublicValue);
            Console.WriteLine(ProtectedValue);
            Console.WriteLine(PrivateValue);
        }
    }

    public class Child : Parent
    {
        public void ShowInheritedValues()
        {
            Console.WriteLine(PublicValue);
            Console.WriteLine(ProtectedValue);

            // PrivateValue cannot be accessed directly here.
            // ❌ Compile error because it belongs only to Parent.
        }
    }


    // =========================================================================
    // PART F: SINGLE INHERITANCE
    // =========================================================================
    // A class can directly inherit from only ONE class.
    // =========================================================================

    public class A
    {
    }

    public class B : A
    {
    }

    // ❌ Not allowed:
    // public class C : A, B
    //
    // C# does not support multiple class inheritance.


    // =========================================================================
    // PART G: INTERFACE + CLASS INHERITANCE
    // =========================================================================
    // A class can inherit from one class and implement multiple interfaces.
    // =========================================================================

    public interface IPrintable
    {
        void Print();
    }

    public interface ILoggable
    {
        void Log();
    }

    public class Report : Document, IPrintable, ILoggable
    {
        public void Print()
        {
            Console.WriteLine("Printing report.");
        }

        public void Log()
        {
            Console.WriteLine("Logging report.");
        }
    }

    public class Document
    {
        public string Title { get; set; }
    }


    // =========================================================================
    // PART H: SEALED CLASS
    // =========================================================================
    // A sealed class cannot be inherited.
    // =========================================================================

    public sealed class FinalReport
    {
        public void Generate()
        {
            Console.WriteLine("Report generated.");
        }
    }

    // ❌ Not allowed:
    // public class SpecialReport : FinalReport
    //
    // A sealed class cannot be used as a base class.


    // =========================================================================
    // PART I: EXECUTION PLATFORM
    // =========================================================================

    public class InheritanceRunner
    {
        public static void Run()
        {
            // =================================================================
            // 1. BASIC INHERITANCE
            // =================================================================

            Dog dog = new Dog
            {
                Name = "Bruno",
                Breed = "Labrador"
            };

            dog.Eat();
            dog.Run();

            // =================================================================
            // 2. METHOD OVERRIDING
            // =================================================================

            dog.MakeSound();

            // =================================================================
            // 3. MULTILEVEL INHERITANCE
            // =================================================================

            Puppy puppy = new Puppy
            {
                Name = "Max",
                Breed = "Golden Retriever"
            };

            puppy.Eat();
            puppy.Run();
            puppy.Play();

            // =================================================================
            // 4. UPCASTING
            // =================================================================
            // A derived class object can be assigned to a base class reference.
            // =================================================================

            Animal animal = new Dog();

            animal.Eat();
            animal.MakeSound();

            // =================================================================
            // 5. BASE CONSTRUCTOR
            // =================================================================

            Car car = new Car("Toyota", 4);

            Console.WriteLine(car.Brand);
            Console.WriteLine(car.NumberOfDoors);
        }
    }
}
```

---

## Part J: QUICK INTERVIEW QUESTIONS

```csharp
// =========================================================================
// PART J: QUICK INTERVIEW QUESTIONS
// =========================================================================
//
// 1. What is inheritance, and why do we use it?
// 2. What is the difference between a base class and a derived class?
// 3. How do we implement inheritance in C#?
// 4. What members are inherited by a derived class?
// 5. Are constructors inherited in C#?
// 6. What is the purpose of the 'base' keyword?
// 7. What is single inheritance?
// 8. Does C# support multiple class inheritance?
// 9. Can a class inherit from one class and implement multiple interfaces?
// 10. What is multilevel inheritance?
// 11. What is hierarchical inheritance?
// 12. What is the difference between inheritance and composition?
// 13. What is method overriding in inheritance?
// 14. What is the difference between 'virtual', 'override', and 'new'?
// 15. Can a private member be accessed directly from a derived class?
// 16. What is the difference between private and protected members?
// 17. Can an abstract class be used as a base class?
// 18. Can a sealed class be inherited?
// 19. Can a derived class access the base class implementation?
// 20. What is the difference between inheritance and interface implementation?
//
// =========================================================================
```

---

# Explanation

### 1. What is Inheritance?

Inheritance is an OOP mechanism where a derived class acquires accessible members and behavior from a base class.

```csharp
class Dog : Animal
{
}
```

Here, `Dog` inherits from `Animal`.

---

### 2. What is the difference between a base class and a derived class?

```text
Base class
    ↓
Provides common behavior/state

Derived class
    ↓
Inherits and extends the base behavior
```

Example:

```csharp
Animal
   ↓
Dog
```

`Animal` is the base class and `Dog` is the derived class.

---

### 3. How do we implement inheritance in C#?

Use the `:` symbol:

```csharp
public class Dog : Animal
{
}
```

---

### 4. What members are inherited by a derived class?

Accessible members of the base class can be used by the derived class.

Private members still belong to the base class and cannot be accessed directly from the derived class.

---

### 5. Are constructors inherited in C#?

No.

Constructors are not inherited.

A derived constructor can explicitly call a base constructor using:

```csharp
: base(value)
```

---

### 6. What is the purpose of the `base` keyword?

`base` can be used to:

- Call a base-class constructor.
- Access a base-class member.
- Call a base implementation of an overridden method.

Example:

```csharp
public Car(string brand)
    : base(brand)
{
}
```

---

### 7. What is single inheritance?

A class can directly inherit from only one class.

```csharp
class Dog : Animal
{
}
```

C# does not allow:

```csharp
// Not allowed
class Dog : Animal, Mammal
{
}
```

---

### 8. Does C# support multiple class inheritance?

No.

A class cannot inherit from multiple classes.

However, a class can implement multiple interfaces.

---

### 9. Can a class inherit from one class and implement multiple interfaces?

Yes.

```csharp
class Report : Document, IPrintable, ILoggable
{
}
```

The class inherits from one base class and implements multiple interfaces.

---

### 10. What is multilevel inheritance?

Multilevel inheritance occurs when a class derives from another derived class.

```text
Animal
   ↓
Dog
   ↓
Puppy
```

`Puppy` can access the accessible members inherited through `Dog` and `Animal`.

---

### 11. What is hierarchical inheritance?

Hierarchical inheritance occurs when multiple classes inherit from the same base class.

```text
        Animal
        /    \
      Dog    Cat
```

Both `Dog` and `Cat` inherit from `Animal`.

---

### 12. What is the difference between inheritance and composition?

Inheritance represents an **"is-a"** relationship.

```text
Dog is an Animal
```

Composition represents a **"has-a"** relationship.

```text
Car has an Engine
```

---

### 13. What is method overriding in inheritance?

A derived class can provide a specialized implementation of a base class method marked `virtual` or `abstract`.

```csharp
public virtual void MakeSound()
{
}

public override void MakeSound()
{
}
```

---

### 14. What is the difference between `virtual`, `override`, and `new`?

```text
virtual
    ↓
Base class allows overriding

override
    ↓
Derived class replaces the virtual implementation

new
    ↓
Derived class hides the inherited member
```

`override` participates in runtime polymorphism, while `new` performs method hiding.

---

### 15. Can a private member be accessed directly from a derived class?

No.

```csharp
class Parent
{
    private int value;
}

class Child : Parent
{
    // value cannot be accessed directly.
}
```

The private member is accessible only within `Parent`.

---

### 16. What is the difference between private and protected members?

```text
private
    ↓
Only declaring class

protected
    ↓
Declaring class + derived classes
```

---

### 17. Can an abstract class be used as a base class?

Yes.

In fact, an abstract class is specifically designed to be used as a base class.

```csharp
abstract class Animal
{
    public abstract void MakeSound();
}
```

A concrete derived class must implement its abstract members.

---

### 18. Can a sealed class be inherited?

No.

```csharp
public sealed class FinalReport
{
}
```

This prevents another class from deriving from `FinalReport`.

---

### 19. Can a derived class access the base class implementation?

Yes.

The `base` keyword can access the base implementation.

```csharp
public override void MakeSound()
{
    base.MakeSound();

    Console.WriteLine("Dog barks.");
}
```

---

### 20. What is the difference between inheritance and interface implementation?

Inheritance from a class represents an implementation relationship where the derived class receives accessible class behavior and state.

Interface implementation represents a contract that the implementing class agrees to fulfill.

```text
Class inheritance
    ↓
Reuse / extend base class behavior

Interface implementation
    ↓
Fulfill a contract
```

---

# Important Rules

- A derived class can inherit from only one class.
- C# does not support multiple class inheritance.
- A class can implement multiple interfaces.
- Constructors are not inherited.
- `base(...)` calls a base-class constructor.
- `base.Member` can access a base-class member.
- `private` members cannot be accessed directly by derived classes.
- `protected` members can be accessed by derived classes.
- `virtual` allows overriding.
- `override` provides a derived implementation.
- `new` hides an inherited member.
- An abstract class can be used as a base class.
- A sealed class cannot be inherited.
- Multilevel inheritance is supported.
- Hierarchical inheritance is supported.
- Inheritance represents an **"is-a"** relationship.

---

# Important Missing Points

- C# supports **single class inheritance**, but multiple interfaces can be implemented.
- Constructors are not inherited; they are invoked through constructor chaining.
- Inheritance should represent a genuine **"is-a"** relationship. If the relationship is **"has-a"**, composition is generally more appropriate.
- A derived class can extend base behavior, override virtual/abstract behavior, or hide members using `new`.
