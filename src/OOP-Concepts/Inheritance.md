# Inheritance – Compact Interview Notes

## What is Inheritance?

Inheritance is an OOP concept where a **derived class inherits members and behavior from a base class**.

It helps with:
- Code reuse
- Extending existing functionality
- Creating a parent-child relationship between classes
- Supporting runtime polymorphism through virtual/abstract members

Example:

```csharp
public class Vehicle
{
    public void Start()
    {
        Console.WriteLine("Vehicle started");
    }
}

public class Car : Vehicle
{
    public void Drive()
    {
        Console.WriteLine("Car is driving");
    }
}
```

Usage:

```csharp
Car car = new Car();

car.Start(); // Inherited from Vehicle
car.Drive(); // Car's own method
```

---

# Base Class and Derived Class

```csharp
public class Vehicle       // Base class
{
}

public class Car : Vehicle // Derived class
{
}
```

- `Vehicle` is the **base/parent class**.
- `Car` is the **derived/child class**.
- `Car` gets accessible members of `Vehicle`.

Inheritance syntax:

```csharp
class DerivedClass : BaseClass
{
}
```

---

# Example with Constructor

```csharp
public class Vehicle
{
    public Vehicle()
    {
        Console.WriteLine("Vehicle constructor");
    }
}

public class Car : Vehicle
{
    public Car()
    {
        Console.WriteLine("Car constructor");
    }
}
```

```csharp
Car car = new Car();
```

Output:

```text
Vehicle constructor
Car constructor
```

The base class constructor executes before the derived class constructor.

---

# Access Modifiers and Inheritance

### Public

```csharp
public string Name;
```

Accessible from the derived class and from outside the class.

### Protected

```csharp
protected string Name;
```

Accessible inside the base class and derived classes, but not directly from outside.

### Private

```csharp
private string Name;
```

Not directly accessible from the derived class.

### Internal

```csharp
internal string Name;
```

Accessible within the same assembly, subject to the normal C# access rules.

---

# Method Overriding

A base class can provide a `virtual` method that a derived class can override.

```csharp
public class Animal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("Animal sound");
    }
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Dog says Woof");
    }
}
```

Usage:

```csharp
Animal animal = new Dog();
animal.MakeSound();
```

Output:

```text
Dog says Woof
```

This is runtime polymorphism.

---

# Abstract Members and Inheritance

An abstract base class can define rules that derived classes must implement.

```csharp
public abstract class Animal
{
    public abstract void MakeSound();
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Dog says Woof");
    }
}
```

The derived class must implement the abstract member unless it is also abstract.

---

# `base` Keyword

The `base` keyword is used to access members of the base class.

### Calling Base Constructor

```csharp
public class Vehicle
{
    public Vehicle(string name)
    {
        Console.WriteLine(name);
    }
}

public class Car : Vehicle
{
    public Car() : base("Car")
    {
    }
}
```

### Calling Base Method

```csharp
public class Vehicle
{
    public virtual void Start()
    {
        Console.WriteLine("Vehicle started");
    }
}

public class Car : Vehicle
{
    public override void Start()
    {
        base.Start();
        Console.WriteLine("Car started");
    }
}
```

---

# Types of Inheritance

## 1. Single Inheritance

One derived class inherits from one base class.

```text
Vehicle
   ↓
Car
```

```csharp
class Car : Vehicle
{
}
```

---

## 2. Multilevel Inheritance

A class inherits from another derived class.

```text
Vehicle
   ↓
Car
   ↓
SportsCar
```

```csharp
class Vehicle
{
}

class Car : Vehicle
{
}

class SportsCar : Car
{
}
```

---

## 3. Hierarchical Inheritance

Multiple classes inherit from the same base class.

```text
        Vehicle
       /            Car       Bike
```

```csharp
class Car : Vehicle
{
}

class Bike : Vehicle
{
}
```

---

## 4. Multiple Inheritance

C# does **not support multiple inheritance using classes**.

This is not allowed:

```csharp
class Car : Vehicle, Machine
{
}
```

However, C# supports implementing multiple interfaces:

```csharp
class Car : Vehicle, IDisposable, IComparable
{
}
```

So:

> C# supports single class inheritance and multiple interface implementation.

---

## 5. Hybrid Inheritance

Hybrid inheritance is a combination of multiple inheritance types.

C# does not support hybrid inheritance through multiple classes because C# does not support multiple class inheritance.

It can be represented using classes together with interfaces.

---

# Inheritance vs Composition

### Inheritance

Represents an **"is-a"** relationship.

```text
Car is a Vehicle
Dog is an Animal
```

```csharp
class Car : Vehicle
{
}
```

### Composition

Represents a **"has-a"** relationship.

```text
Car has an Engine
```

```csharp
class Car
{
    private Engine engine = new Engine();
}
```

Use inheritance when there is a strong parent-child relationship. Composition is often preferred when you want to combine independent behaviors.

---

# Important Rules

- C# supports **single class inheritance**.
- A class can implement **multiple interfaces**.
- A class cannot inherit from a `sealed` class.
- Constructors are not inherited.
- Private members are not directly accessible in a derived class.
- Protected members are accessible in derived classes.
- `virtual` members can be overridden.
- `abstract` members must be implemented by a concrete derived class.
- `base` is used to access base-class constructors and members.
- A derived class can itself be used as a base class for another class.
- Every class ultimately derives from `System.Object`.

---

# Interview Questions

### 1. What is inheritance?

Inheritance is an OOP mechanism where a derived class acquires accessible members and behavior from a base class.

---

### 2. Why do we use inheritance?

Mainly for:

- Code reuse
- Extending existing functionality
- Creating an "is-a" relationship
- Supporting polymorphism

---

### 3. Does C# support multiple inheritance?

**No, not with classes.**

A class can inherit from only one class.

However, a class can implement multiple interfaces.

---

### 4. Can a derived class access private members of the base class?

**No, not directly.**

Private members belong to the base class and are accessible only within that class.

A protected member can be accessed by the derived class.

---

### 5. Are constructors inherited?

**No.**

Constructors are not inherited.

However, the base constructor executes when a derived-class object is created.

---

### 6. What is the difference between `virtual` and `override`?

`virtual` allows a derived class to change the implementation.

```csharp
public virtual void Print()
{
}
```

`override` provides the new implementation in the derived class.

```csharp
public override void Print()
{
}
```

---

### 7. What is the purpose of the `base` keyword?

It is used to access base-class members and call a base-class constructor.

```csharp
base.Start();
```

```csharp
public Car() : base()
{
}
```

---

### 8. Can a sealed class be inherited?

**No.**

A `sealed` class prevents further inheritance.

```csharp
public sealed class Vehicle
{
}
```

This is not allowed:

```csharp
class Car : Vehicle
{
}
```

---

### 9. Can an abstract class be used as a base class?

**Yes.**

That is one of its main purposes.

```csharp
public abstract class Vehicle
{
}

public class Car : Vehicle
{
}
```

---

### 10. What is multilevel inheritance?

When a class inherits from another derived class.

```text
Vehicle
   ↓
Car
   ↓
SportsCar
```

---

### 11. What is hierarchical inheritance?

When multiple derived classes inherit from the same base class.

```text
       Vehicle
       /          Car     Bike
```

---

### 12. What is the difference between inheritance and composition?

Inheritance represents an **"is-a"** relationship.

Composition represents a **"has-a"** relationship.

```text
Car is a Vehicle       → Inheritance
Car has an Engine      → Composition
```

---

# Important Missing Points

- Inheritance enables reuse, but excessive inheritance can create tight coupling.
- Prefer `protected` members carefully; exposing behavior through methods/properties is often better than exposing fields.
- `private` members still exist as part of the base-class object state, but derived classes cannot access them directly.
- A derived class can override inherited virtual/abstract members, but cannot override a non-virtual member.
- `new` can hide a base member, but it is different from `override`.
- Static members are associated with the type and are not overridden polymorphically.
- `System.Object` is the ultimate base class for all C# classes.
