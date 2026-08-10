# Encapsulation – Comprehensive Notes

## Part A: ENCAPSULATION

```csharp
namespace OOP_Concepts.Encapsulation
{
    // =========================================================================
    // PART A: ENCAPSULATION
    // =========================================================================
    // Encapsulation means bundling data and the methods that operate on that
    // data inside a class and controlling how the internal state is accessed.
    //
    // The main purpose is to protect the object's state and prevent invalid
    // or unwanted changes from outside the class.
    // =========================================================================

    public class BankAccount
    {
        // ---------------------------------------------------------------------
        // PRIVATE FIELD
        // ---------------------------------------------------------------------
        // 'private' prevents outside code from directly accessing the field.
        // The field represents the internal state of the BankAccount object.
        // ---------------------------------------------------------------------

        private decimal balance;

        // ---------------------------------------------------------------------
        // PUBLIC PROPERTY WITH PRIVATE SET
        // ---------------------------------------------------------------------
        // Anyone can read the balance, but only this class can change it.
        // This provides controlled access to the internal state.
        // ---------------------------------------------------------------------

        public decimal Balance
        {
            get => balance;
            private set => balance = value;
        }

        // ---------------------------------------------------------------------
        // CONSTRUCTOR
        // ---------------------------------------------------------------------
        // The constructor controls how the object is initially created.
        // Invalid initial values can be rejected before the object is created.
        // ---------------------------------------------------------------------

        public BankAccount(decimal initialBalance)
        {
            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial balance cannot be negative.");
            }

            balance = initialBalance;
        }

        // ---------------------------------------------------------------------
        // DEPOSIT METHOD
        // ---------------------------------------------------------------------
        // External code cannot directly change 'balance'.
        // It must use Deposit(), where validation can be performed.
        // ---------------------------------------------------------------------

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be greater than zero.");
            }

            balance += amount;
        }

        // ---------------------------------------------------------------------
        // WITHDRAW METHOD
        // ---------------------------------------------------------------------
        // The class controls how the balance can be reduced.
        // This prevents the object from entering an invalid state.
        // ---------------------------------------------------------------------

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || amount > balance)
            {
                return false;
            }

            balance -= amount;
            return true;
        }
    }


    // =========================================================================
    // PART B: ENCAPSULATION USING VALIDATION
    // =========================================================================

    public class Employee
    {
        // ---------------------------------------------------------------------
        // PRIVATE FIELD
        // ---------------------------------------------------------------------
        // External code cannot directly change the employee's age.
        // ---------------------------------------------------------------------

        private int age;

        // ---------------------------------------------------------------------
        // PROPERTY WITH VALIDATION
        // ---------------------------------------------------------------------

        public int Age
        {
            get => age;
            private set => age = value;
        }

        // ---------------------------------------------------------------------
        // CONTROLLED STATE CHANGE
        // ---------------------------------------------------------------------
        // Business rules are applied before changing the internal state.
        // ---------------------------------------------------------------------

        public void SetAge(int newAge)
        {
            if (newAge < 18)
            {
                throw new ArgumentException("Employee must be at least 18 years old.");
            }

            Age = newAge;
        }
    }


    // =========================================================================
    // PART C: READ-ONLY DATA
    // =========================================================================

    public class EmployeeProfile
    {
        // ---------------------------------------------------------------------
        // GET-ONLY PROPERTY
        // ---------------------------------------------------------------------
        // The value can be assigned during construction but cannot be changed
        // through the public interface afterward.
        // ---------------------------------------------------------------------

        public string EmployeeId { get; }

        public string Name { get; private set; }

        public EmployeeProfile(string employeeId, string name)
        {
            EmployeeId = employeeId;
            Name = name;
        }

        // ---------------------------------------------------------------------
        // CONTROLLED UPDATE
        // ---------------------------------------------------------------------

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            Name = name;
        }
    }


    // =========================================================================
    // PART D: EXECUTION PLATFORM
    // =========================================================================

    public class EncapsulationRunner
    {
        public static void Run()
        {
            // =================================================================
            // 1. OBJECT CREATION
            // =================================================================

            BankAccount account = new BankAccount(1000);

            // =================================================================
            // 2. READING CONTROLLED DATA
            // =================================================================

            Console.WriteLine($"Initial Balance: {account.Balance}");

            // =================================================================
            // 3. CONTROLLED STATE CHANGE
            // =================================================================

            account.Deposit(500);

            Console.WriteLine($"After Deposit: {account.Balance}");

            // =================================================================
            // 4. CONTROLLED WITHDRAWAL
            // =================================================================

            bool success = account.Withdraw(300);

            Console.WriteLine($"Withdrawal Successful: {success}");
            Console.WriteLine($"Final Balance: {account.Balance}");

            // =================================================================
            // 5. DIRECT FIELD ACCESS IS NOT ALLOWED
            // =================================================================

            // account.balance = -500;
            // ❌ Compile error because 'balance' is private.

            // =================================================================
            // 6. PRIVATE SETTER
            // =================================================================

            // account.Balance = 5000;
            // ❌ Compile error because Balance has a private setter.
        }
    }
}
```

---

## Part E: QUICK INTERVIEW QUESTIONS

```csharp
// =========================================================================
// PART E: QUICK INTERVIEW QUESTIONS
// =========================================================================
//
// 1. What is encapsulation, and why do we use it?
// 2. Why should fields usually be private?
// 3. What is the difference between a field and a property?
// 4. What is the purpose of a private setter?
// 5. Can we achieve encapsulation without using properties?
// 6. How does encapsulation help prevent invalid object state?
// 7. What is the difference between data hiding and encapsulation?
// 8. What is the difference between abstraction and encapsulation?
// 9. Why should we prefer methods over public setters when business validation
//    is required?
// 10. Can a private field be accessed directly from a derived class?
// 11. What is the difference between private and protected members?
// 12. Can a property contain validation logic?
// 13. Why is exposing public fields generally considered poor encapsulation?
// 14. Can encapsulation be achieved using only access modifiers?
// 15. How does encapsulation help with maintainability and loose coupling?
//
// =========================================================================
```

---

# Explanation

### 1. What is Encapsulation?

Encapsulation is the process of **bundling data and the methods that operate on that data together and controlling access to the internal state**.

### 2. Why should fields usually be private?

A private field prevents external code from directly changing the object's internal state.

### 3. What is the difference between a field and a property?

A field directly stores data. A property provides controlled access to that data and can contain validation or other logic.

### 4. What is the purpose of a private setter?

A private setter allows the value to be read from outside the class but changed only inside the class.

```csharp
public string Name { get; private set; }
```

### 5. Can we achieve encapsulation without using properties?

Yes. Methods can provide controlled access to private state.

### 6. How does encapsulation help prevent invalid object state?

The class controls how its internal state changes and can validate values before applying them.

### 7. What is the difference between data hiding and encapsulation?

Data hiding focuses on restricting direct access to internal data.

Encapsulation is broader: it combines data and behavior and provides controlled access to the object's state.

### 8. What is the difference between abstraction and encapsulation?

**Abstraction** focuses on hiding unnecessary implementation details.

**Encapsulation** focuses on protecting and controlling access to data and behavior.

### 9. Why should we prefer methods over public setters when business validation is required?

A method can represent a business operation and enforce rules before changing state.

```csharp
account.Withdraw(500);
```

is more controlled than exposing:

```csharp
account.Balance = 500;
```

### 10. Can a private field be accessed directly from a derived class?

No. Private members are accessible only within the class that declares them.

### 11. What is the difference between private and protected members?

```text
private
    ↓
Accessible only inside the declaring class

protected
    ↓
Accessible inside the declaring class
and derived classes
```

### 12. Can a property contain validation logic?

Yes.

```csharp
public int Age
{
    get => age;
    set
    {
        if (value >= 18)
            age = value;
    }
}
```

### 13. Why is exposing public fields generally considered poor encapsulation?

A public field allows external code to modify internal state directly, making validation and business rules difficult to enforce.

### 14. Can encapsulation be achieved using only access modifiers?

Access modifiers are an important part of encapsulation, but good encapsulation also involves designing controlled properties and methods.

### 15. How does encapsulation help with maintainability and loose coupling?

External code depends on the public contract rather than the internal implementation. The internal implementation can therefore change without unnecessarily affecting callers.

---

# Important Rules

- Keep internal state private whenever possible.
- Expose only the functionality consumers need.
- Use properties for controlled read/write access.
- Use `private set` when outside code should read but not directly modify a value.
- Use methods when changing state requires business rules or validation.
- `private` members are accessible only within the declaring class.
- `protected` members are accessible within the declaring class and derived classes.
- Public fields provide little protection over object state.
- Encapsulation helps maintain valid object state.
- Encapsulation is different from abstraction.

---

# Important Missing Points

- Encapsulation is not simply making every field private; it is about **controlled access to state and behavior**.
- A public property with an unrestricted setter may still expose too much control.
- Methods can be preferable to setters when an operation represents a business action.
- Good encapsulation allows internal implementation details to change without unnecessarily affecting calling code.
