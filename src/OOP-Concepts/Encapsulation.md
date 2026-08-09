# Encapsulation – Compact Interview Notes

## What is Encapsulation?

Encapsulation is an OOP concept of **bundling data and the methods that operate on that data together and controlling how that data can be accessed or modified**.

It helps:
- Protect object state.
- Control access to data.
- Prevent invalid or unwanted changes.
- Keep implementation details inside the class.

---

# Basic Example

```csharp
public class BankAccount
{
    private decimal balance;

    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            balance += amount;
        }
    }

    public decimal GetBalance()
    {
        return balance;
    }
}
```

Usage:

```csharp
BankAccount account = new BankAccount();

account.Deposit(1000);

Console.WriteLine(account.GetBalance());
```

The `balance` field cannot be directly modified from outside the class:

```csharp
// Not allowed
account.balance = -1000;
```

The class controls how the balance can be changed.

That is **encapsulation**.

---

# Encapsulation Using Properties

Properties are commonly used to control access to fields.

```csharp
public class Employee
{
    private string name;

    public string Name
    {
        get => name;
        set => name = value;
    }
}
```

The property provides controlled access to the private field.

---

# Read-Only Property

We can expose a value for reading while preventing outside code from changing it.

```csharp
public class Employee
{
    public string Name { get; private set; }

    public Employee(string name)
    {
        Name = name;
    }
}
```

Outside code can read:

```csharp
Console.WriteLine(employee.Name);
```

But cannot directly change it:

```csharp
// Not allowed
employee.Name = "John";
```

---

# Validation Using Encapsulation

Encapsulation allows validation before changing internal state.

```csharp
public class Employee
{
    private int age;

    public int Age
    {
        get => age;
        set
        {
            if (value >= 18)
            {
                age = value;
            }
        }
    }
}
```

The class controls what values are allowed.

---

# Access Modifiers

Access modifiers are important for implementing encapsulation.

### Private

```csharp
private string name;
```

Accessible only within the same class.

### Protected

```csharp
protected string name;
```

Accessible within the class and derived classes.

### Internal

```csharp
internal string name;
```

Accessible within the same assembly.

### Public

```csharp
public string Name;
```

Accessible from anywhere allowed by the type's accessibility.

---

# Encapsulation vs Abstraction

### Encapsulation

Focuses on:

> **How do we protect and control access to data and implementation?**

Example:

```csharp
private decimal balance;
```

### Abstraction

Focuses on:

> **What functionality should be exposed while hiding unnecessary implementation details?**

Example:

```csharp
account.Withdraw(1000);
```

The caller knows what operation is available but does not need to know how it works internally.

### Quick Difference

| Encapsulation | Abstraction |
|---|---|
| Protects data and controls access | Hides unnecessary implementation details |
| Focuses on access/control | Focuses on what functionality to expose |
| Uses access modifiers, properties, methods, etc. | Commonly achieved using interfaces and abstract classes |
| Helps maintain object state | Helps reduce complexity |

---

# Real-World Example

Consider a bank account.

The account has:

```text
Balance
PIN
Account Number
```

These should not be freely changed by external code.

Instead:

```csharp
public class BankAccount
{
    private decimal balance;

    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            balance += amount;
        }
    }

    public void Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= balance)
        {
            balance -= amount;
        }
    }

    public decimal GetBalance()
    {
        return balance;
    }
}
```

The class controls how `balance` changes.

This protects the object's state and is an example of **encapsulation**.

---

# Important Rules

- Keep internal state private when possible.
- Expose data through properties or methods when controlled access is required.
- Use validation before modifying internal state.
- `private` is commonly used to hide internal fields.
- `protected` allows access from derived classes.
- `public` exposes members to external code.
- Encapsulation helps maintain valid object state.
- Encapsulation reduces unwanted dependencies on internal implementation.

---

# Interview Questions

### 1. What is encapsulation?

Encapsulation is the process of bundling data and related behavior together and controlling access to the internal state of an object.

---

### 2. Why do we use encapsulation?

Mainly to:

- Protect data.
- Control how data is modified.
- Validate input.
- Maintain a valid object state.
- Reduce dependency on internal implementation.

---

### 3. How do we achieve encapsulation in C#?

Commonly using:

- Classes
- Access modifiers
- Private fields
- Properties
- Methods

---

### 4. Why should fields usually be private?

Private fields prevent external code from directly changing the object's internal state.

Instead, controlled access can be provided through properties or methods.

```csharp
private decimal balance;

public void Deposit(decimal amount)
{
    if (amount > 0)
        balance += amount;
}
```

---

### 5. What is the difference between a field and a property?

A **field** stores data directly.

```csharp
private string name;
```

A **property** provides controlled access to data.

```csharp
public string Name
{
    get => name;
    set => name = value;
}
```

A property can also contain validation or other logic.

---

### 6. Can encapsulation be achieved without private fields?

**Yes.**

Encapsulation is about controlling access to state and behavior. We can use properties, methods, access modifiers, and other class design techniques.

---

### 7. What is `private set`?

`private set` allows a property to be read from outside the class but changed only inside the class.

```csharp
public string Name { get; private set; }
```

---

### 8. Is encapsulation the same as data hiding?

They are closely related but not exactly the same.

- **Data hiding** focuses on restricting direct access to internal data.
- **Encapsulation** is the broader concept of bundling data and behavior and controlling access to them.

---

### 9. Can a class be fully encapsulated?

A class can be designed with strong encapsulation, but whether it is "fully encapsulated" depends on what state and behavior it exposes.

The general principle is:

> Expose only what consumers need and keep implementation details controlled.

---

# Important Missing Points

- Encapsulation is not simply making every field `private`; it is about **controlled access to state and behavior**.
- Properties can provide validation and controlled read/write access.
- `private set` is useful when a value should be changed only by the class.
- Methods are often better than public setters when changing a value requires business rules.
- Good encapsulation helps prevent invalid object states.
- Encapsulation and abstraction are complementary OOP concepts, not alternatives.
