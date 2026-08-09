# Polymorphism – Compact Interview Notes

## What is Polymorphism?

Polymorphism means **"many forms."**

It allows the same method, object, or interface reference to behave differently depending on the actual object or implementation.

In C#, polymorphism is mainly achieved using:

- **Compile-time polymorphism** — Method Overloading
- **Runtime polymorphism** — Method Overriding

---

# 1. Compile-Time Polymorphism

Compile-time polymorphism is achieved using **method overloading**.

The method name is the same, but the parameter list is different.

```csharp
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }
}
```

Usage:

```csharp
Calculator calculator = new Calculator();

calculator.Add(10, 20);
calculator.Add(10, 20, 30);
calculator.Add(10.5, 20.5);
```

The compiler decides which method to call based on the arguments.

### Important

Method overloading can differ by:

- Number of parameters
- Parameter types
- Parameter order

Return type alone cannot be used for overloading.

```csharp
// Not allowed
int Add(int a, int b)
{
}

double Add(int a, int b)
{
}
```

---

# 2. Runtime Polymorphism

Runtime polymorphism is achieved using **inheritance + virtual/override methods**.

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

public class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Cat says Meow");
    }
}
```

Usage:

```csharp
Animal animal;

animal = new Dog();
animal.MakeSound();

animal = new Cat();
animal.MakeSound();
```

Output:

```text
Dog says Woof
Cat says Meow
```

The reference type is `Animal`, but the actual object determines which overridden method executes.

---

# Virtual and Override

### Virtual

A base class uses `virtual` to allow a derived class to change the implementation.

```csharp
public virtual void MakeSound()
{
    Console.WriteLine("Animal sound");
}
```

### Override

The derived class uses `override` to provide its own implementation.

```csharp
public override void MakeSound()
{
    Console.WriteLine("Dog says Woof");
}
```

---

# Polymorphism Using Abstract Class

An abstract method can also provide runtime polymorphism.

```csharp
public abstract class Animal
{
    public abstract void MakeSound();
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Woof");
    }
}

public class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Meow");
    }
}
```

Usage:

```csharp
Animal animal = new Dog();
animal.MakeSound();
```

The actual object is `Dog`, so `Dog.MakeSound()` executes.

---

# Polymorphism Using Interface

Interfaces are also commonly used for runtime polymorphism.

```csharp
public interface IPayment
{
    void Pay();
}

public class CreditCardPayment : IPayment
{
    public void Pay()
    {
        Console.WriteLine("Paid using Credit Card");
    }
}

public class UpiPayment : IPayment
{
    public void Pay()
    {
        Console.WriteLine("Paid using UPI");
    }
}
```

Usage:

```csharp
IPayment payment;

payment = new CreditCardPayment();
payment.Pay();

payment = new UpiPayment();
payment.Pay();
```

Output:

```text
Paid using Credit Card
Paid using UPI
```

The same `IPayment` reference can represent different implementations.

---

# Method Hiding vs Method Overriding

### Method Overriding

Uses `virtual` and `override`.

```csharp
public class Parent
{
    public virtual void Print()
    {
        Console.WriteLine("Parent");
    }
}

public class Child : Parent
{
    public override void Print()
    {
        Console.WriteLine("Child");
    }
}
```

```csharp
Parent obj = new Child();
obj.Print();
```

Output:

```text
Child
```

### Method Hiding

Uses `new`.

```csharp
public class Parent
{
    public void Print()
    {
        Console.WriteLine("Parent");
    }
}

public class Child : Parent
{
    public new void Print()
    {
        Console.WriteLine("Child");
    }
}
```

```csharp
Parent obj = new Child();
obj.Print();
```

Output:

```text
Parent
```

With method hiding, the method is selected based on the **reference type**, not runtime polymorphism.

---

# Overloading vs Overriding

| Overloading | Overriding |
|---|---|
| Compile-time polymorphism | Runtime polymorphism |
| Same method name | Same method signature |
| Different parameters | Same parameters |
| Usually within the same class | Requires inheritance |
| Does not require `virtual` | Uses `virtual` + `override` |
| Compiler selects the method | Runtime selects the implementation |

---

# Important Rules

- Polymorphism means **one interface/reference with multiple forms/implementations**.
- Method overloading provides compile-time polymorphism.
- Method overriding provides runtime polymorphism.
- Runtime polymorphism requires inheritance or interface-based dispatch.
- A base method must be `virtual`, `abstract`, or otherwise overridable for normal overriding.
- `override` provides a new implementation of an inherited virtual/abstract member.
- `new` hides a base member; it does not provide overriding polymorphism.
- Static methods cannot be overridden.
- Constructors cannot be overridden.
- Return type alone cannot create method overloading.

---

# Interview Questions

### 1. What is polymorphism?

Polymorphism means **many forms**. It allows the same method or reference to behave differently depending on the parameters or actual object.

---

### 2. What are the types of polymorphism in C#?

Mainly:

- **Compile-time polymorphism** — Method overloading
- **Runtime polymorphism** — Method overriding

---

### 3. What is method overloading?

Having multiple methods with the same name but different parameter lists.

```csharp
void Print(int value)
{
}

void Print(string value)
{
}
```

---

### 4. What is method overriding?

Providing a new implementation in a derived class for an inherited `virtual` or `abstract` member.

```csharp
public override void Print()
{
}
```

---

### 5. Can we overload a method by changing only the return type?

**No.**

The parameter list must be different.

---

### 6. Can a static method be overridden?

**No.**

Static methods belong to the type and are not polymorphic instance members.

They can be hidden using `new`, but not overridden.

---

### 7. What is the difference between `virtual` and `override`?

`virtual` allows a derived class to override a method.

`override` provides the new implementation in the derived class.

---

### 8. What is the difference between `override` and `new`?

`override` provides runtime polymorphism.

`new` hides the base member.

```csharp
Parent obj = new Child();
obj.Print();
```

With `override`, the child implementation executes.

With `new`, the parent implementation executes.

---

### 9. Can an abstract method be overridden?

**Yes.**

A derived class must override an abstract method unless the derived class is also abstract.

---

### 10. Can an interface be used for runtime polymorphism?

**Yes.**

```csharp
IPayment payment = new CreditCardPayment();
payment.Pay();

payment = new UpiPayment();
payment.Pay();
```

The same interface reference can point to different implementations.

---

### 11. What is dynamic polymorphism?

Runtime polymorphism is sometimes called **dynamic polymorphism** because the implementation to execute is determined at runtime.

```csharp
Animal animal = new Dog();
animal.MakeSound();
```

---

### 12. Can constructors be overridden?

**No.**

Constructors are not inherited and therefore cannot be overridden.

---

# Important Missing Points

- Polymorphism is one of the four main OOP principles.
- **Overloading = compile time.**
- **Overriding = runtime.**
- `virtual` allows overriding.
- `abstract` requires overriding in a concrete derived class.
- `new` hides a member; it is not the same as `override`.
- Interfaces are commonly used to achieve runtime polymorphism and loose coupling.
- Polymorphism is especially useful when multiple implementations need to be handled through a common base type or interface.
