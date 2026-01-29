# Liskov Substitution Principle (LSP)

## Definition / Intent

**Liskov Substitution Principle (LSP)** states that, in a computer program, if S is a Subtype of T, then objects of type T may be replaced with objects of type S.

Which means, **Derived types must be completely substitutable for their base types**.

More formally, the Liskov substitution principle (LSP) is a particular definition of a subtyping relation, called **(strong) behavioral subtyping**.

This principle was introduced by **Barbara Liskov in 1987** during her conference address on Data abstraction and hierarchy.

**This principle is just an extension of the Open/Closed Principle.**

## Implementation Guidelines

In the process of development we should ensure that:

1. **No new exceptions** can be thrown by the subtype unless they are part of the existing exception hierarchy
2. **Clients should not know which specific subtype** they are calling, nor should they need to know that. The client should behave the same regardless of the subtype instance that it is given
3. **New derived classes just extend** without replacing the functionality of old classes

## Connection to Open/Closed Principle

The examples used in this session are related to the Open/Closed Principle. LSP builds upon OCP to ensure that extensions don't break existing functionality.

In the previous session as part of the **Open/Closed Principle** implementation, we created different employee classes to calculate bonus of the employee. From the employee perspective we implemented the Open/Closed Principle.

If you take a look at the main program, we created Employee objects which consist of both permanent and temporary employees. The Derived types (PermanentEmployee and TemporaryEmployee) have completely substituted the base type Employee class.

Without using the subtypes directly, we are calculating the bonus of the employee from the base class type itself. This **partially** satisfies the Liskov Substitution Principle along with the Open/Closed Principle.

## Problem Statement

Violating LSP leads to unexpected behavior when subclasses override or restrict base class functionality, breaking polymorphism.

**Why the OCP implementation violates LSP:**

Let's assume that we need to have a **ContractEmployee** as one of the employee categories. A point to note here is that a **contract employee is not eligible for any bonus calculation**. After implementing the Employee class, we end up throwing an exception at runtime in the `CalculateBonus()` method. **This violates the Liskov Substitution Principle.**

## Code Example – Violation

**Before Liskov Substitution Principle:**

```csharp
using System;

namespace OpenClosedDemo
{
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

        public abstract decimal CalculateBonus(decimal salary);
       
        public override string ToString()
        {
            return string.Format("ID : {0} Name : {1}", this.ID, this.Name);
        }
    }

    public class PermanentEmployee : Employee
    {
        public PermanentEmployee()
        { }

        public PermanentEmployee(int id, string name) : base(id, name)
        { }
        
        public override decimal CalculateBonus(decimal salary)
        {
            return salary * .1M;
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
            return salary * .05M;
        }
    }

    // ❌ LSP Violation: Contract employees don't get bonuses
    public class ContractEmployee : Employee
    {
        public ContractEmployee()
        { }

        public ContractEmployee(int id, string name) : base(id, name)
        { }
        
        // ❌ Throws exception - breaks substitutability!
        public override decimal CalculateBonus(decimal salary)
        {
            throw new NotImplementedException();
        }
    }
}
```

**Usage that breaks:**

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
                
            // ❌ Runtime error! NotImplementedException thrown
            Console.WriteLine(string.Format("Employee {0} Bonus: {1}",
                empMike.ToString(),
                empMike.CalculateBonus(150000).ToString()));

            Console.ReadLine();
        }
    }
}
```

**Problems with this approach:**
* `ContractEmployee` cannot be substituted for `Employee` without causing runtime errors
* Client code (Main method) cannot safely treat all employees polymorphically
* Violates LSP guideline: "No new exceptions can be thrown by the subtype"
* Forces client code to check employee type before calling `CalculateBonus`
* Breaks the promise of the `Employee` contract

## Code Example – Correct Implementation

**After Implementing Liskov Substitution Principle:**

**Step 1: Define segregated interfaces**

```csharp
using System;

namespace LSPDemoConsole
{
    // Base employee interface - common to all employees
    interface IEmployee
    {
        int ID { get; set; }
        string Name { get; set; }
        decimal GetMinimumSalary();
    }
}
```

```csharp
using System;

namespace LSPDemoConsole
{
    // Bonus capability - only for employees eligible for bonuses
    interface IEmployeeBonus
    {
        decimal CalculateBonus(decimal salary);
    }
}
```

**Step 2: Define Employee base class (for bonus-eligible employees)**

```csharp
using System;

namespace LSPDemoConsole
{
    public abstract class Employee : IEmployee, IEmployeeBonus
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Employee()
        { }

        public Employee(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public abstract decimal CalculateBonus(decimal salary);

        public override string ToString()
        {
            return string.Format("ID : {0} Name : {1}", this.ID, this.Name);
        }

        public abstract decimal GetMinimumSalary();
    }
}
```

**Step 3: Implement bonus-eligible employees**

```csharp
using System;

namespace LSPDemoConsole.Implementation
{
    public class PermanentEmployee : Employee
    {
        public PermanentEmployee()
        { }

        public PermanentEmployee(int id, string name) : base(id, name)
        { }

        public override decimal CalculateBonus(decimal salary)
        {
            return (salary * .1M);
        }

        public override decimal GetMinimumSalary()
        {
            return 15000;
        }
    }
}
```

```csharp
using System;

namespace LSPDemoConsole.Implementation
{
    public class TemporaryEmployee : Employee
    {
        public TemporaryEmployee()
        { }

        public TemporaryEmployee(int id, string name) : base(id, name)
        { }

        public override decimal CalculateBonus(decimal salary)
        {
            return (salary * .05M);
        }

        public override decimal GetMinimumSalary()
        {
            return 5000;
        }
    }
}
```

**Step 4: Implement contract employee (only IEmployee, no bonus)**

```csharp
using System;

namespace LSPDemoConsole.Implementation
{
    // ✅ Only implements IEmployee, not IEmployeeBonus
    public class ContractEmployee : IEmployee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public ContractEmployee()
        { }

        public ContractEmployee(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public decimal GetMinimumSalary()
        {
            return 5000;
        }

        public override string ToString()
        {
            return string.Format("ID : {0} Name : {1}", this.ID, this.Name);
        }
    }
}
```

**Step 5: Client code using LSP correctly**

```csharp
using LSPDemoConsole.Implementation;
using System;
using System.Collections.Generic;

namespace LSPDemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // ✅ List of bonus-eligible employees
            List<Employee> employees = new List<Employee>();
            employees.Add(new PermanentEmployee(1, "John"));
            employees.Add(new TemporaryEmployee(2, "Jason"));
            
            // ✅ Cannot add ContractEmployee here - compile-time safety!
            // employees.Add(new ContractEmployee(3, "Mike")); // Won't compile
            
            foreach (var employee in employees)
            {
                Console.WriteLine(string.Format("Employee {0} Bonus: {1} MinSalary: {2}",
                    employee.ToString(),
                    employee.CalculateBonus(100000).ToString(),
                    employee.GetMinimumSalary().ToString()));
            }

            Console.WriteLine();

            // ✅ List of all employees (no bonus calculation)
            List<IEmployee> employeesOnly = new List<IEmployee>();
            employeesOnly.Add(new PermanentEmployee(1, "John"));
            employeesOnly.Add(new TemporaryEmployee(2, "Jason"));
            employeesOnly.Add(new ContractEmployee(3, "Mike")); // ✅ Works fine!

            foreach (var employee in employeesOnly)
            {
                Console.WriteLine(string.Format("Employee {0}  MinSalary: {1}",
                    employee.ToString(),
                    employee.GetMinimumSalary().ToString()));
            }
            
            Console.ReadLine();
        }
    }
}
```

## Explanation of the Fix

**How LSP is Achieved:**

1. **Interface Segregation**: Split functionality into `IEmployee` (common) and `IEmployeeBonus` (bonus-specific)
2. **Proper Hierarchy**: 
   - `Employee` abstract class implements both `IEmployee` and `IEmployeeBonus`

   - `ContractEmployee` only implements `IEmployee` (no bonus capability)
3. **Type Safety**: Compiler prevents adding `ContractEmployee` to `List<Employee>`
4. **Substitutability**: 
   - All `Employee` objects can calculate bonuses safely
   - All `IEmployee` objects can get minimum salary safely
5. **No Runtime Exceptions**: No `NotImplementedException` or type checking needed

**Benefits:**
* ✅ `PermanentEmployee` and `TemporaryEmployee` are fully substitutable for `Employee`
* ✅ `ContractEmployee` doesn't violate any contract it implements
* ✅ Compile-time safety prevents invalid operations
* ✅ Client code knows exactly what operations are available
* ✅ No runtime surprises or exceptions

## Classic LSP Violation: Rectangle-Square Problem

```csharp
// ❌ Classic LSP violation
public class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
}

public class Square : Rectangle
{
    public override int Width { set { base.Width = base.Height = value; } }
    public override int Height { set { base.Width = base.Height = value; } }
}

// Client code
Rectangle rect = new Square();
rect.Width = 5;
rect.Height = 10; // ❌ Unexpected: both width and height become 10
```

**Correct Implementation:**

```csharp
// ✅ Proper abstraction
public abstract class Shape
{
    public abstract int Area();
}

public class Rectangle : Shape
{
    public int Width { get; set; }
    public int Height { get; set; }
    public override int Area() => Width * Height;
}

public class Square : Shape
{
    public int Side { get; set; }
    public override int Area() => Side * Side;
}
```

## When to Use / When NOT to Overuse

**Use LSP when:**
* Designing class hierarchies for polymorphic use
* Creating base classes or interfaces
* Subclasses have different capabilities than the base class
* You need to ensure runtime substitutability

**Don't Overuse:**
* Don't force inheritance where composition is better
* Avoid deep inheritance hierarchies
* Don't create abstract methods that not all subclasses can implement

## Real-world / Enterprise Use Case

### ASP. NET Core Middleware

In ASP. NET Core, custom middleware and filters should not break the contract of the base types they extend.

```csharp
// ✅ All middleware must follow the same contract
public class CustomAuthMiddleware
{
    private readonly RequestDelegate _next;
    
    public CustomAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    // Must always call next or terminate properly
    public async Task InvokeAsync(HttpContext context)
    {
        // Custom logic
        await _next(context); // ✅ Maintains contract
    }
}
```

### Repository Pattern

```csharp
public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task SaveAsync(T entity);
}

// ✅ All implementations must support these operations
public class SqlRepository<T> : IRepository<T>
{
    public async Task<T> GetByIdAsync(int id)
    {
        // SQL implementation
    }
    
    public async Task SaveAsync(T entity)
    {
        // SQL implementation
    }
}

// ❌ Violation: Read-only repository can't implement SaveAsync properly
public class ReadOnlyRepository<T> : IRepository<T>
{
    public async Task<T> GetByIdAsync(int id)
    {
        // Implementation
    }
    
    public async Task SaveAsync(T entity)
    {
        throw new NotSupportedException(); // ❌ Violates LSP!
    }
}
```

## Common Mistakes & Anti-Patterns

1. **Throwing NotImplementedException**: Overriding methods to throw exceptions
2. **Strengthening Preconditions**: Subclass requires more than base class
3. **Weakening Postconditions**: Subclass delivers less than base class promises
4. **Returning null**: Instead of proper implementation
5. **Type Checking**: Client code checking types before calling methods
6. **Empty Implementations**: Methods that do nothing when they should do something

**Example of Type Checking (Anti-pattern):**

```csharp
// ❌ If you need to check types, LSP is violated
if (employee is ContractEmployee)
{
    // Don't calculate bonus
}
else
{
    employee.CalculateBonus(salary);
}
```

## Performance & Maintainability Impact

**Maintainability:** ✅ Significantly increases
* Contracts are preserved and reliable
* No unexpected runtime behavior
* Easier to reason about code

**Testability:** ✅ Greatly improves
* Ensures mocks and stubs behave like real objects
* Polymorphic code can be tested with any subtype
* No special cases needed

**Reliability:** ✅ Enhanced
* Compile-time safety prevents many errors
* No runtime surprises
* Predictable behavior

**Performance:** ➡️ Neutral
* No performance overhead from following LSP
* Actually may improve performance by avoiding type checks

## Relation to Design Patterns

LSP is fundamental to these patterns:

* **Factory Method**: Factory creates substitutable objects
* **Template Method**: Subclasses extend without breaking template
* **Adapter**: Adapts interfaces while maintaining contracts
* **Strategy**: Different strategies are substitutable
* **Decorator**: Decorators preserve the interface contract

## Interview Cross-Questions with Answers

**Q: How do you detect LSP violations?**  
**A:** Look for:
* Subclasses throwing `NotImplementedException` or new exceptions
* Type checking (`is`,  `as`,  `typeof`) before method calls
* Empty or no-op implementations of base methods
* Subclasses that can't be used in place of base class without errors

**Q: Why is LSP important for unit testing?**  
**A:** It ensures mocks and stubs behave like real objects. If LSP is violated, mocks may not accurately represent real implementations, leading to false test results.

**Q: What's the difference between LSP and OCP?**  
**A:** 
* **OCP**: Software should be open for extension but closed for modification
* **LSP**: Subtypes must be substitutable for base types
* LSP ensures that extensions (OCP) don't break existing functionality

**Q: How does LSP relate to Design by Contract?**  
**A:** LSP is based on Design by Contract principles:
* Preconditions cannot be strengthened in subclass
* Postconditions cannot be weakened in subclass
* Invariants must be preserved in subclass

**Q: Can you violate LSP without using inheritance?**  
**A:** No, LSP specifically deals with subtyping relationships. However, similar issues can occur with interface implementations.

**Q: How does Barbara Liskov define LSP formally?**  
**A:** If for each object o1 of type S there is an object o2 of type T such that for all programs P defined in terms of T, the behavior of P is unchanged when o1 is substituted for o2, then S is a subtype of T.

**Q: What's the relationship between LSP and ISP?**  
**A:** ISP helps achieve LSP by ensuring interfaces are focused. Smaller, focused interfaces make it easier to create proper substitutable implementations.

## Quick Revision / Summary

✅ **Subtypes must be completely substitutable for base types**  
✅ **Introduced by Barbara Liskov in 1987**  
✅ **Extension of the Open/Closed Principle**  
✅ **No new exceptions in subtypes unless part of existing hierarchy**  
✅ **Clients should not know which specific subtype they're using**  
✅ **Derived classes extend without replacing functionality**  
✅ **Avoid throwing NotImplementedException in overrides**  
✅ **Use interface segregation to separate capabilities**  
✅ **Enables reliable polymorphism and testing**

### Key Takeaway

**"Derived types must be completely substitutable for their base types without altering the correctness of the program."** - Barbara Liskov

If you find yourself checking types or catching exceptions from subtypes, you're likely violating LSP!
