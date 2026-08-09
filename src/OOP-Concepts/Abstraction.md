# Abstraction – Compact Interview Notes

## What is Abstraction?

Abstraction is an OOP concept of **hiding implementation details and exposing only the required functionality**.

It focuses on:
- **What an object does**, rather than how it does it.
- Hiding unnecessary implementation details.
- Providing a simple interface to the user.

Abstraction can be achieved mainly using:
- **Abstract classes**
- **Interfaces**

---

# Example

```csharp
public abstract class Payment
{
    public abstract void Pay(decimal amount);
}

public class CreditCardPayment : Payment
{
    public override void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using Credit Card");
    }
}

public class UpiPayment : Payment
{
    public override void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using UPI");
    }
}
```

The caller only needs to know:

```csharp
Payment payment = new CreditCardPayment();
payment.Pay(1000);
```

The caller does not need to know how the payment is processed internally.

That is **abstraction**.

---

# Abstraction Using Interface

```csharp
public interface IPayment
{
    void Pay(decimal amount);
}

public class CreditCardPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using Credit Card");
    }
}
```

The interface exposes the required operation:

```csharp
payment.Pay(1000);
```

The implementation details remain inside `CreditCardPayment`.

---

# Abstraction vs Abstract Class

These two terms are related but are not the same.

### Abstraction

**Abstraction is an OOP principle.**

> Hide unnecessary implementation details and expose only what is required.

### Abstract Class

**An abstract class is a C# language feature that can be used to achieve abstraction.**

```csharp
public abstract class Vehicle
{
    public abstract void Start();
}
```

So:

```text
Abstraction
    ↓
OOP Principle

Abstract Class / Interface
    ↓
Ways to implement abstraction
```

---

# Abstraction vs Encapsulation

### Abstraction

Focuses on:

> **What should be exposed?**

It hides unnecessary implementation details.

Example:

```csharp
car.Start();
```

The user does not need to know how the engine starts.

### Encapsulation

Focuses on:

> **How should data and implementation be protected?**

It bundles data and methods together and controls access using access modifiers.

Example:

```csharp
private decimal balance;

public void Deposit(decimal amount)
{
    balance += amount;
}
```

The `balance` field cannot be directly modified from outside the class.

### Quick Difference

| Abstraction | Encapsulation |
|---|---|
| Hides implementation details | Protects data and implementation |
| Focuses on what to expose | Focuses on controlling access |
| Achieved using abstract classes/interfaces | Achieved using classes, access modifiers, properties, methods |
| Design-level concept | Implementation-level concept |

---

# Real-World Example

Consider an ATM.

The user sees:
- Withdraw
- Deposit
- Balance
- Transfer

The user does not see:
- Database queries
- Account validation
- PIN verification logic
- Bank communication
- Transaction processing

The ATM provides the required functionality while hiding the internal implementation.

This is **abstraction**.

---

# Important Points

- Abstraction hides unnecessary implementation details.
- It exposes only the required functionality.
- Abstraction focuses on **what**, not **how**.
- Abstract classes and interfaces are commonly used to achieve abstraction.
- Abstraction improves loose coupling and makes code easier to maintain.
- An abstract class can provide both abstraction and shared implementation.
- An interface primarily defines a contract, although modern C# also supports default implementations.

---

# Interview Questions

### 1. What is abstraction?

Abstraction is the process of hiding implementation details and exposing only the necessary functionality to the user.

### 2. How can we achieve abstraction in C#?

Mainly using:
- Abstract classes
- Interfaces

### 3. What is the difference between abstraction and an abstract class?

Abstraction is an **OOP principle**.

An abstract class is a **C# language feature** that can be used to implement abstraction.

### 4. Can we achieve abstraction without an abstract class?

**Yes.**

We can use an interface:

```csharp
public interface IPayment
{
    void Pay(decimal amount);
}
```

### 5. Is abstraction the same as encapsulation?

**No.**

- Abstraction hides unnecessary implementation details.
- Encapsulation protects data and controls access to it.

### 6. Can an abstract class contain concrete methods?

**Yes.**

```csharp
public abstract class Vehicle
{
    public abstract void Start();

    public void Stop()
    {
        Console.WriteLine("Vehicle stopped");
    }
}
```

### 7. Can an interface provide abstraction?

**Yes.**

An interface exposes a contract while hiding the implementation provided by the implementing class.

### 8. What is the main benefit of abstraction?

It reduces complexity by exposing only the functionality that the consumer needs.

---

# Important Missing Points

- Abstraction does not mean that all implementation must be hidden.
- An abstract class can contain both abstract and concrete members.
- An interface can also contain default implementations in modern C#.
- Abstraction is mainly about **hiding complexity**, while encapsulation is mainly about **controlling access to data and implementation**.
