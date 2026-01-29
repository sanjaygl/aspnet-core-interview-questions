# Open/Closed Principle (OCP)

## Definition / Intent

**Open/Closed Principle (OCP)** states that "software entities such as classes, modules, functions, etc. should be open for extension, but closed for modification".

Which means, any new functionality should be implemented by adding new classes, attributes and methods, instead of changing the current ones or existing ones.

**Bertrand Meyer** is generally credited for having originated the term open/closed principle and this Principle is considered by **Bob Martin** as **"the most important principle of object-oriented design"**.

## Implementation Guidelines

The simplest way to apply OCP is to implement the new functionality on new derived (sub) classes that inherit the original class implementation.

Another way is to allow client to access the original class with an abstract interface.

So, at any given point of time when there is a requirement change instead of touching the existing functionality, it's always suggested to create new classes and leave the original implementation untouched.

## Problem Statement

If you need to change existing code to add new features, you risk introducing bugs and breaking existing functionality. This is common when using large switch/case or if/else blocks.

### Pitfalls of NOT Following OCP

What happens if we do not follow Open Closed Principle during a requirement enhancement in the development process?

**Disadvantages:**
1. **Extensive Testing Required**: If a class or function always allows the addition of new logic, as a developer we end up testing the entire functionality along with the new requirement
2. **Increased QA Burden**: As a developer we need to ensure to communicate the scope of the changes to the Quality Assurance team in advance so that they can gear up for enhanced regression testing along with the feature testing
3. **Costly Process**: Step 2 above is a costly process to adapt for any organization
4. **Breaks SRP**: Not following the Open Closed Principle breaks the Single Responsibility Principle since the class or function might end up doing multiple tasks
5. **Maintenance Nightmare**: If the changes are implemented on the same class, maintenance of the class becomes difficult since the code of the class increases by thousands of unorganized lines

## Code Example – Violation

**Before Open Closed Principle:**

```csharp
using System;

namespace OpenClosedDemo
{
    public class Employee
    {
        Employee() { }
        
        public Employee(int id, string name, string type)
        {
            this.ID = id;
            this.Name = name;
            this.EmployeeType = type;
        }

        public int ID { get; set; }
        public string EmployeeType { get; set; }
        public string Name { get; set; }

        // ❌ Violation: Must modify this method for every new employee type
        public decimal CalculateBonus(decimal salary)
        {
            if (this.EmployeeType == "Permanent")
                return salary * .1M;
            else
                return salary * .05M;
        }
    }
}
```

**Usage:**

```csharp
using System;

namespace OpenClosedDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee empJohn = new Employee(1, "John", "Permanent");
            Employee empJason = new Employee(2, "Jason", "Temp");
           
            Console.WriteLine(string.Format("Employee {0} Bonus: {1}",
                empJohn.Name,
                empJohn.CalculateBonus(100000).ToString()));
            
            Console.ReadLine();
        }
    }
}
```

**Problems with this approach:**
* Every time a new employee type is added (Contract, Intern, Executive), the `CalculateBonus` method must be modified
* Existing code is at risk of breaking with each change
* Violates OCP because the class is NOT closed for modification
* Testing burden increases with each new employee type
* The `EmployeeType` string comparison is error-prone

## Code Example – Correct Implementation

**After Open Closed Principle Implementation:**

```csharp
using System;

namespace OpenClosedDemo
{
    // Abstract base class - defines the contract
    public abstract class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
       
        public Employee()
        {
        }
        
        public Employee(int id, string name)
        {
            this.ID = id; 
            this.Name = name; 
        }
        
        // ✅ Abstract method - forces derived classes to implement
        public abstract decimal CalculateBonus(decimal salary);
       
        public override string ToString()
        {
            return string.Format("ID : {0} Name : {1}", this.ID, this.Name);
        }
    }

    // ✅ Extension through inheritance - no modification needed
    public class PermanentEmployee : Employee
    {
        public PermanentEmployee()
        { }

        public PermanentEmployee(int id, string name) : base(id, name)
        { }
        
        public override decimal CalculateBonus(decimal salary)
        {
            return salary * .1M; // 10% bonus
        }
    }

    public class TemporaryEmployee : Employee
    {
        public TemporaryEmployee()
        { }

        public TemporaryEmployee(int id, string name) : base(id, name)
        { }
        
        public override decimal CalculateBonus(decimal salary)
        {
            return salary * .05M; // 5% bonus
        }
    }
    
    // ✅ New employee type - just add a new class, no existing code modified!
    public class ContractEmployee : Employee
    {
        public ContractEmployee()
        { }

        public ContractEmployee(int id, string name) : base(id, name)
        { }
        
        public override decimal CalculateBonus(decimal salary)
        {
            return salary * .03M; // 3% bonus
        }
    }
}
```

**Usage:**

```csharp
using System;

namespace OpenClosedDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee empJohn = new PermanentEmployee(1, "John");
            Employee empJason = new TemporaryEmployee(2, "Jason");
            Employee empMike = new ContractEmployee(3, "Mike");
           
            Console.WriteLine(string.Format("Employee {0} Bonus: {1}",
                empJohn.ToString(),
                empJohn.CalculateBonus(100000).ToString()));
                
            Console.WriteLine(string.Format("Employee {0} Bonus: {1}",
                empJason.ToString(),
                empJason.CalculateBonus(150000).ToString()));
                
            Console.WriteLine(string.Format("Employee {0} Bonus: {1}",
                empMike.ToString(),
                empMike.CalculateBonus(120000).ToString()));
                
            Console.ReadLine();
        }
    }
}
```

## Explanation of the Fix

**How OCP is Achieved:**

1. **Open for Extension**: New employee types can be added by creating new derived classes (`PermanentEmployee`, `TemporaryEmployee`, `ContractEmployee`)
2. **Closed for Modification**: The base `Employee` class remains unchanged when new types are added
3. **Polymorphism**: Each employee type implements its own bonus calculation logic
4. **No If/Else or Switch**: Type-specific logic is encapsulated in derived classes

**Benefits:**
* Adding a new employee type (e.g.,  `InternEmployee`) requires only creating a new class
* Existing classes are never touched, reducing risk of bugs
* Each class has a single responsibility
* Easy to test each employee type independently
* Follows both OCP and SRP

## Alternative Implementation Using Interface

You can also use interfaces to achieve OCP:

```csharp
public interface IBonusCalculator
{
    decimal CalculateBonus(decimal salary);
}

public class PermanentEmployeeBonusCalculator : IBonusCalculator
{
    public decimal CalculateBonus(decimal salary) => salary * 0.1M;
}

public class TemporaryEmployeeBonusCalculator : IBonusCalculator
{
    public decimal CalculateBonus(decimal salary) => salary * 0.05M;
}

public class Employee
{
    private readonly IBonusCalculator _bonusCalculator;
    
    public Employee(int id, string name, IBonusCalculator bonusCalculator)
    {
        ID = id;
        Name = name;
        _bonusCalculator = bonusCalculator;
    }
    
    public int ID { get; set; }
    public string Name { get; set; }
    
    public decimal CalculateBonus(decimal salary)
    {
        return _bonusCalculator.CalculateBonus(salary);
    }
}
```

## When to Use / When NOT to Overuse

**Use OCP when:**
* Requirements change frequently or new types are expected
* You have if/else or switch statements based on type
* Adding new functionality requires modifying existing classes
* You need to support plugin architectures

**Don't Overuse:**
* Avoid excessive abstraction for simple, stable logic
* Don't create complex hierarchies for one-time requirements
* Balance between flexibility and simplicity

## Real-world / Enterprise Use Case

### ASP. NET Core Middleware

In ASP. NET Core, middleware and filters are open for extension via DI, not by modifying framework code.

```csharp
// ✅ Extend functionality without modifying existing code
public class CustomAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    
    public CustomAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        // Custom authentication logic
        await _next(context);
    }
}
```

### Payment Gateway Integration

```csharp
public interface IPaymentProcessor
{
    Task<PaymentResult> ProcessPayment(decimal amount);
}

public class StripePaymentProcessor : IPaymentProcessor
{
    public async Task<PaymentResult> ProcessPayment(decimal amount)
    {
        // Stripe-specific implementation
    }
}

public class PayPalPaymentProcessor : IPaymentProcessor
{
    public async Task<PaymentResult> ProcessPayment(decimal amount)
    {
        // PayPal-specific implementation
    }
}

// ✅ Add new payment gateway without modifying existing code
public class RazorpayPaymentProcessor : IPaymentProcessor
{
    public async Task<PaymentResult> ProcessPayment(decimal amount)
    {
        // Razorpay-specific implementation
    }
}
```

## Common Mistakes & Anti-Patterns

1. **Switch/Case Proliferation**: Relying on switch/case for extensible logic
2. **God Classes**: Modifying core classes for every new requirement
3. **Type Checking**: Using `typeof`,  `is`, or `GetType()` to determine behavior
4. **String-Based Type Discrimination**: Using string properties to determine type
5. **Premature Abstraction**: Creating complex hierarchies before they're needed

**Example of Anti-Pattern:**

```csharp
// ❌ Bad: Adding new types requires modifying this method
public decimal CalculateDiscount(string customerType)
{
    switch (customerType)
    {
        case "Regular": return 0.05M;
        case "Premium": return 0.10M;
        case "VIP": return 0.15M;
        default: return 0M;
    }
}
```

## Performance & Maintainability Impact

**Maintainability:** ✅ Significantly increases
* New features require less risk
* Changes are isolated to new classes
* Easier code reviews

**Testability:** ✅ Improves dramatically
* Each class can be tested independently
* No need to test entire functionality for new additions

**Performance:** ➡️ Slight overhead
* Virtual method calls have minimal overhead
* Benefits far outweigh any performance cost

**Development Speed:** ✅ Faster over time
* Initial setup takes longer
* Adding new features becomes trivial
* Less regression testing needed

## Relation to Design Patterns

OCP is fundamental to many design patterns:

* **Strategy Pattern**: Define a family of algorithms, encapsulate each one
* **Decorator Pattern**: Add responsibilities without modifying objects
* **Template Method Pattern**: Define skeleton, let subclasses override
* **Factory Pattern**: Create objects without specifying exact class
* **Plugin Architecture**: Add functionality through plugins

## Interview Cross-Questions with Answers

**Q: How does OCP relate to plugin architectures?**  
**A:** Plugins extend functionality without modifying the host application. The host defines interfaces, and plugins implement them. This allows adding features by dropping in new assemblies without recompiling the main application.

**Q: How do you refactor a switch statement to follow OCP?**  
**A:** Use polymorphism and interfaces to encapsulate behavior:
1. Create an interface or abstract class
2. Create a class for each switch case
3. Replace switch with polymorphic call
4. Use dependency injection or factory pattern

**Q: What's the difference between OCP and SRP?**  
**A:** 
* **SRP**: A class should have one reason to change
* **OCP**: A class should be open for extension but closed for modification
* OCP helps maintain SRP by preventing classes from accumulating responsibilities

**Q: Can you have OCP without interfaces or abstract classes?**  
**A:** Technically yes, through composition and delegation, but interfaces/abstract classes are the most common and idiomatic approach in C#.

**Q: How does OCP relate to the "Don't Repeat Yourself" (DRY) principle?**  
**A:** OCP reduces duplication by allowing behavior to be extended through inheritance or composition rather than copying and modifying code.

**Q: What are the signs that OCP is being violated?**  
**A:** 
* Frequent modifications to existing classes for new features
* Long if/else or switch statements based on type
* Many methods with similar structure but different behavior
* Commenting out or modifying existing code for new requirements

**Q: Why is OCP considered the most important principle by Bob Martin?**  
**A:** Because it directly addresses the core problem of software maintenance: change. Code that follows OCP is resilient to change, reducing bugs and making the system more maintainable over time.

## Quick Revision / Summary

✅ **Open for extension, closed for modification**  
✅ **Add new functionality by creating new classes, not modifying existing ones**  
✅ **Use interfaces, abstract classes, and inheritance**  
✅ **Originated by Bertrand Meyer, emphasized by Bob Martin**  
✅ **Avoids if/else and switch statements for type-based behavior**  
✅ **Reduces testing burden - only test new classes**  
✅ **Prevents maintenance nightmares and regression bugs**  
✅ **Foundation for many design patterns (Strategy, Decorator, etc.)**

### Key Takeaway

**"Software entities should be open for extension but closed for modification."** - Bertrand Meyer

When you need to add new functionality, you should be able to do so by adding new code, not by changing existing code that already works.
