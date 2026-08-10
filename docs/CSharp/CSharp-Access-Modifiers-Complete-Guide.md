# C# Access Modifiers – Complete Guide with Practical Examples

## 1. Access Modifiers Overview

### What are Access Modifiers?

Access modifiers are keywords in C# that define the accessibility or visibility of classes, methods, properties, fields, and other members. They control which parts of your code can access specific members, forming the foundation of **encapsulation** in object-oriented programming.

### Why Are They Important?

* **Encapsulation**: Hide implementation details and expose only what's necessary
* **Security**: Prevent unauthorized access to sensitive data or critical methods
* **API Design**: Create clean, maintainable public APIs while keeping internal details hidden
* **Maintainability**: Changes to private/internal members don't break external code
* **Principle of Least Privilege**: Expose minimum necessary access to reduce coupling

---

## 2. List of Access Modifiers

C# provides **six access modifiers** with varying levels of accessibility:

---

### 2.1 `public` – Accessible Everywhere

**Definition:**  
The most permissive access level. Members are accessible from any code in the same assembly or any other assembly that references it. No restrictions.

**Where it can be applied:**  
Classes, structs, interfaces, methods, properties, fields, constructors, delegates, events.

**Example:**

```csharp
namespace ClassLibraryA
{
    // Public class - accessible from any assembly
    public class PublicClass
    {
        // Public field - accessible from anywhere
        public string Name = "Public Field";
        
        // Public property
        public int Count { get; set; }
        
        // Public method
        public void DisplayMessage()
        {
            Console.WriteLine("Hello from PublicClass");
        }
    }
}
```

**Usage:**

```csharp
// From another assembly/project
var obj = new ClassLibraryA.PublicClass(); // ✅ Accessible
obj.DisplayMessage();                       // ✅ Accessible
Console.WriteLine(obj.Name);                // ✅ Accessible
```

---

### 2.2 `private` – Accessible Only Within the Same Class

**Definition:**  
The most restrictive access level. Members are accessible only within the same class or struct. Not visible to derived classes or external code.

**Where it can be applied:**  
Methods, properties, fields, nested classes, constructors (for Singleton pattern).

**Example:**

```csharp
namespace ClassLibraryA
{
    public class PrivateExample
    {
        // Private field - only accessible within this class
        private string secretKey = "MySecret";
        
        // Private method
        private void InternalProcess()
        {
            Console.WriteLine($"Processing with key: {secretKey}");
        }
        
        // Public method that uses private members
        public void Execute()
        {
            InternalProcess(); // ✅ Can access private method within same class
        }
        
        // Private nested class
        private class InternalHelper
        {
            public void Help() { }
        }
    }
}
```

**Usage:**

```csharp
var obj = new PrivateExample();
obj.Execute();                // ✅ Accessible (public method)
// obj.InternalProcess();     // ❌ Error: Cannot access private method
// obj.secretKey;             // ❌ Error: Cannot access private field
```

---

### 2.3 `protected` – Accessible Within Class and Derived Classes

**Definition:**  
Members are accessible within the same class and by derived classes (in any assembly). Not accessible by unrelated classes.

**Where it can be applied:**  
Methods, properties, fields, nested classes, constructors (in abstract classes).

**Example:**

```csharp
namespace ClassLibraryA
{
    public class ProtectedClass
    {
        // Protected field - accessible in derived classes
        protected string baseData = "Base Data";
        
        // Protected method
        protected void ProcessData()
        {
            Console.WriteLine($"Processing: {baseData}");
        }
        
        // Protected property
        protected int InternalCounter { get; set; }
    }
    
    // Derived class in same assembly
    public class DerivedClass : ProtectedClass
    {
        public void DisplayData()
        {
            // ✅ Can access protected members from base class
            Console.WriteLine(baseData);
            ProcessData();
            InternalCounter = 10;
        }
    }
}
```

**Usage:**

```csharp
var derived = new DerivedClass();
derived.DisplayData();        // ✅ Accessible (public method)
// derived.baseData;          // ❌ Error: Cannot access protected member from outside
// derived.ProcessData();     // ❌ Error: Cannot access protected method from outside
```

---

### 2.4 `internal` – Accessible Within the Same Assembly Only

**Definition:**  
Members are accessible from any code within the same assembly (project/DLL) but not from other assemblies. Default access level for top-level classes.

**Where it can be applied:**  
Classes, structs, interfaces, methods, properties, fields, constructors.

**Example:**

```csharp
namespace ClassLibraryA
{
    // Internal class - only accessible within ClassLibraryA assembly
    internal class InternalClass
    {
        // Internal method
        internal void InternalOperation()
        {
            Console.WriteLine("Internal operation in ClassLibraryA");
        }
        
        // Internal property
        internal string InternalData { get; set; }
    }
    
    // Public class with internal members
    public class PublicClassWithInternals
    {
        // Internal method - accessible only within same assembly
        internal void InternalHelper()
        {
            Console.WriteLine("Internal helper method");
        }
        
        // Public method
        public void PublicMethod()
        {
            InternalHelper(); // ✅ Can call internal method from same assembly
        }
    }
}
```

**Usage from Same Assembly:**

```csharp
// Within ClassLibraryA project
var obj = new InternalClass();           // ✅ Accessible within same assembly
obj.InternalOperation();                 // ✅ Accessible
```

**Usage from Different Assembly:**

```csharp
// From another project/assembly
// var obj = new ClassLibraryA.InternalClass(); // ❌ Error: Not accessible
// InternalClass is not visible outside ClassLibraryA
```

---

### 2.5 `protected internal` – Union of Protected + Internal

**Definition:**  
Members are accessible from the same assembly OR from derived classes in any assembly. It's the **union** (logical OR) of `protected` and `internal` .

**Formula:** `protected internal = protected OR internal`

**Where it can be applied:**  
Methods, properties, fields, nested classes, constructors.

**Example:**

```csharp
namespace ClassLibraryA
{
    public class ProtectedInternalExample
    {
        // Protected internal field
        // Accessible: same assembly OR derived classes (any assembly)
        protected internal string SharedData = "Shared";
        
        // Protected internal method
        protected internal void SharedMethod()
        {
            Console.WriteLine("Protected internal method");
        }
    }
}
```

**Accessibility:**

| Scenario | Accessible? |
|----------|-------------|
| Same assembly, same class | ✅ Yes |
| Same assembly, different class | ✅ Yes (internal part) |
| Different assembly, derived class | ✅ Yes (protected part) |
| Different assembly, non-derived class | ❌ No |

**Usage:**

```csharp
// Same assembly (any class)
var obj = new ProtectedInternalExample();
obj.SharedMethod();                      // ✅ Accessible (internal part)

// Different assembly (derived class only)
public class ExternalDerived : ClassLibraryA.ProtectedInternalExample
{
    public void Test()
    {
        SharedMethod();                  // ✅ Accessible (protected part)
        Console.WriteLine(SharedData);   // ✅ Accessible (protected part)
    }
}
```

---

### 2.6 `private protected` – Intersection of Private + Protected

**Definition:**  
Members are accessible only within the same class AND derived classes in the **same assembly**. It's the **intersection** (logical AND) of `private` and `protected` . Introduced in C# 7.2.

**Formula:** `private protected = protected AND internal`

**Where it can be applied:**  
Methods, properties, fields, nested classes, constructors.

**Example:**

```csharp
namespace ClassLibraryA
{
    public class PrivateProtectedExample
    {
        // Private protected field
        // Accessible: derived classes in same assembly ONLY
        private protected string restrictedData = "Restricted";
        
        // Private protected method
        private protected void RestrictedMethod()
        {
            Console.WriteLine("Private protected method");
        }
    }
    
    // Derived class in SAME assembly
    public class SameAssemblyDerived : PrivateProtectedExample
    {
        public void Test()
        {
            // ✅ Can access private protected members (same assembly + derived)
            Console.WriteLine(restrictedData);
            RestrictedMethod();
        }
    }
}
```

**Accessibility:**

| Scenario | Accessible? |
|----------|-------------|
| Same assembly, same class | ✅ Yes |
| Same assembly, derived class | ✅ Yes |
| Same assembly, non-derived class | ❌ No |
| Different assembly, derived class | ❌ No (key difference from protected) |
| Different assembly, non-derived class | ❌ No |

**Usage from Different Assembly:**

```csharp
// Different assembly (derived class)
public class ExternalDerived : ClassLibraryA.PrivateProtectedExample
{
    public void Test()
    {
        // ❌ Error: Cannot access private protected members from different assembly
        // Console.WriteLine(restrictedData);    // Not accessible
        // RestrictedMethod();                   // Not accessible
    }
}
```

---

## 3. Comparison Table

### Access Modifiers Comparison Matrix

| Access Modifier | Same Class | Derived Class<br>(Same Assembly) | Derived Class<br>(Different Assembly) | Non-Derived Class<br>(Same Assembly) | Non-Derived Class<br>(Different Assembly) |
|-----------------|------------|----------------------------------|---------------------------------------|--------------------------------------|------------------------------------------|
| **`public`** | ✅ Yes | ✅ Yes | ✅ Yes | ✅ Yes | ✅ Yes |
| **`private`** | ✅ Yes | ❌ No | ❌ No | ❌ No | ❌ No |
| **`protected`** | ✅ Yes | ✅ Yes | ✅ Yes | ❌ No | ❌ No |
| **`internal`** | ✅ Yes | ✅ Yes | ❌ No | ✅ Yes | ❌ No |
| **`protected internal`** | ✅ Yes | ✅ Yes | ✅ Yes | ✅ Yes | ❌ No |
| **`private protected`** | ✅ Yes | ✅ Yes | ❌ No | ❌ No | ❌ No |

### Visual Accessibility Summary

```
🌍 public             ─── Most Permissive (everywhere)
  ├─ protected internal ─── Same assembly OR derived classes (any assembly)
  ├─ internal          ─── Same assembly only
  ├─ protected         ─── Derived classes (any assembly)
  ├─ private protected ─── Derived classes (same assembly only)
  └─ private           ─── Same class only (Most Restrictive)
```

### Logical Formula Summary

* **`protected internal`** = `protected` **OR** `internal` (union - more permissive)
* **`private protected`** = `protected` **AND** `internal` (intersection - more restrictive)

---

## 4. Interview Cross Questions (Q&A Style)

**Q1. What are the access modifiers in C#?**  
A. C# has six access modifiers: `public` , `private` , `protected` , `internal` , `protected internal` , and `private protected` . They control the visibility and accessibility of types and members, forming the foundation of encapsulation.

**Q2. What is the difference between `public` and `internal` ?**  
A. `public` is accessible from anywhere (all assemblies). `internal` is accessible only within the same assembly (project/DLL). Use `internal` for implementation details that should be hidden from external consumers but shared within your library.

**Q3. What is `protected internal` and when would you use it?**  
A. `protected internal` is the union (OR) of `protected` and `internal` . A member is accessible if code is in the same assembly OR in a derived class (any assembly). It's useful for base classes in libraries that need internal helpers but also want to allow external inheritance.

**Q4. What is `private protected` and how is it different from `protected` ?**  
A. `private protected` (C# 7.2) is the intersection (AND) of `protected` and `internal` . Members are accessible only by derived classes in the **same assembly**. Unlike `protected` , external assemblies cannot access these members even in derived classes. Use it to expose extensibility points internally without allowing external overrides.

**Q5. Can a top-level class be `private` ?**  
A. No. Top-level classes (not nested) can only be `public` or `internal` (default). Only nested classes can be `private` . A `private` nested class is accessible only within its containing class.

**Q6. What is the default access modifier for class members?**  
A. `private` is the default for all class members (fields, methods, properties, nested classes, events). Top-level types default to `internal` .

**Q7. Can you have a `protected` constructor?**  
A. Yes. A `protected` constructor is commonly used in abstract classes to prevent direct instantiation while allowing derived classes to call the base constructor. Also used in the Singleton pattern variant.

**Q8. Why were `protected internal` and `private protected` introduced?**  
A. `protected internal` (existed since C# 1.0) allows library authors to provide internal helpers that are also accessible to external derived classes. `private protected` (C# 7.2) was added to restrict protected members to the same assembly only, preventing external assemblies from overriding sensitive methods while still allowing internal extensibility.

**Q9. What is the difference between `protected internal` and `private protected` ?**  
A. `protected internal` = `protected` OR `internal` (more permissive: same assembly + external derived classes). `private protected` = `protected` AND `internal` (more restrictive: derived classes in same assembly only). They are opposites in terms of external assembly access.

**Q10. Can interfaces have access modifiers on their members?**  
A. Since C# 8.0, yes. Interface members can have access modifiers ( `public` , `private` , `protected` , `internal` , `protected internal` , `private protected` ) with default implementations. Before C# 8.0, all interface members were implicitly `public` .

**Q11. What is `internal` typically used for?**  
A. Hiding implementation details within a library/assembly. Classes, helper methods, or utilities that shouldn't be exposed to external consumers but need to be shared across types within the same project. Common in well-designed NuGet packages.

**Q12. Can properties have different access modifiers for getter and setter?**  
A. Yes. You can restrict one accessor: `public int Count { get; private set; }` (public getter, private setter). The more restrictive accessor cannot be more accessible than the property itself.

**Q13. What happens if you don't specify an access modifier?**  
A. Defaults apply: `private` for class members, `internal` for top-level types (classes, structs, interfaces, enums, delegates).

**Q14. Can you override a `protected` method with `public` in a derived class?**  
A. No. You cannot change the access modifier when overriding. The overridden method must have the same access level as the base method. However, you can hide it using `new` keyword with different access.

**Q15. Why is `private` the most commonly used access modifier?**  
A. Following the principle of least privilege and encapsulation: expose only what's necessary. Private members can be changed without affecting external code, making the codebase more maintainable and less coupled.

---

## 5. Common Mistakes

### ❌ Mistake 1: Using `public` Everywhere

**Problem:**  
Beginners often make everything `public` because it "just works" and avoids access errors.

```csharp
public class OrderProcessor
{
    public string connectionString;  // ❌ Should be private
    public decimal taxRate;          // ❌ Should be private or property
    
    public void ProcessOrder() { }   // ✅ OK - public API
}
```

**Solution:**  
Use `private` by default, expose only necessary members:

```csharp
public class OrderProcessor
{
    private string connectionString;  // ✅ Implementation detail
    private decimal taxRate;          // ✅ Internal state
    
    public void ProcessOrder() { }    // ✅ Public API
}
```

**Impact:** Over-exposing members creates tight coupling, makes refactoring difficult, and increases the API surface.

---

### ❌ Mistake 2: Misunderstanding `protected internal`

**Problem:**  
Thinking `protected internal` is more restrictive than `protected` , when it's actually **more permissive**.

```csharp
// Incorrect assumption: "protected internal is for internal derived classes only"
public class BaseClass
{
    protected internal void Method() { } // Actually accessible from same assembly too!
}
```

**Truth:**  
`protected internal` = `protected` **OR** `internal` (union)
* Accessible in same assembly (even non-derived classes)
* Accessible from external derived classes

**When you probably wanted `private protected` :**

```csharp
public class BaseClass
{
    private protected void Method() { } // ✅ Derived classes in same assembly only
}
```

---

### ❌ Mistake 3: Confusing `private protected` with `protected private`

**Problem:**  
The order matters! There's no `protected private` keyword.

```csharp
// ❌ Compilation error
protected private void Method() { }

// ✅ Correct
private protected void Method() { }
```

**Remember:** It's `private protected` (not `protected private` ).

---

### ❌ Mistake 4: Not Using `internal` for Library Implementation

**Problem:**  
Exposing helper classes as `public` when they're only needed internally.

```csharp
// ❌ Bad: Helper class exposed publicly
public class StringHelper { }
public class ValidationHelper { }
```

**Solution:**

```csharp
// ✅ Good: Hide implementation details
internal class StringHelper { }
internal class ValidationHelper { }

// Only expose what consumers need
public class ProductService { }
```

---

### ❌ Mistake 5: Ignoring `protected` Constructor in Abstract Classes

**Problem:**  
Using `public` constructor in abstract classes (which can't be instantiated directly anyway).

```csharp
// ❌ Public constructor in abstract class
public abstract class BaseRepository
{
    public BaseRepository() { } // Unnecessary
}
```

**Solution:**

```csharp
// ✅ Protected constructor - clearer intent
public abstract class BaseRepository
{
    protected BaseRepository() { } // Can only be called by derived classes
}
```

---

### ❌ Mistake 6: Not Restricting Property Setters

**Problem:**  
Making setters `public` when only internal modification is needed.

```csharp
// ❌ Allows external modification
public class Order
{
    public decimal TotalAmount { get; set; } // Anyone can change this!
}
```

**Solution:**

```csharp
// ✅ Public getter, private/protected setter
public class Order
{
    public decimal TotalAmount { get; private set; }
    
    public void CalculateTotal(IEnumerable<OrderItem> items)
    {
        TotalAmount = items.Sum(x => x.Price); // Only modified internally
    }
}
```

---

## 6. Summary

### Key Takeaways

✅ **Use `private` by default** – Expose only what's necessary. Follow the principle of least privilege for better encapsulation and maintainability.

✅ ** `internal` for library internals** – Hide implementation details within your assembly while sharing code across types in the same project.

✅ ** `protected` for inheritance** – Allow derived classes (any assembly) to access base class members. Useful for template method pattern and extensibility.

✅ ** `protected internal` = OR logic** – More permissive (same assembly OR external derived classes). Use for base classes that need internal helpers and external inheritance.

✅ ** `private protected` = AND logic** – More restrictive (derived classes in same assembly only). Use to prevent external overrides while allowing internal extensibility.

### Access Modifier Selection Guide

```
Choose access modifier based on scope:

Need access from other assemblies?
  ├─ Yes → public
  └─ No  → Is it for derived classes?
      ├─ Yes → Need external inheritance?
      │   ├─ Yes → protected or protected internal
      │   └─ No  → private protected
      └─ No  → internal or private
```

### Best Practices

1. **Default to `private`** for fields and helper methods
2. **Use `internal`** for shared utilities within the same assembly
3. **Use `protected`** for virtual/abstract methods meant to be overridden
4. **Avoid `public` fields** – use properties instead
5. **Use `private protected`** when you want inheritance only within your library
6. **Document complex access patterns** – especially `protected internal` and `private protected`

---

## 7. Practical Example: Complete Class Library

### Assembly: ClassLibraryA

```csharp
namespace ClassLibraryA
{
    // Public API - accessible everywhere
    public class ProductService
    {
        // Private field - internal state
        private readonly string _connectionString;
        
        // Internal helper - accessible within assembly
        internal ILogger Logger { get; set; }
        
        // Protected method - for inheritance
        protected virtual void ValidateProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException();
        }
        
        // Private protected - internal extensibility only
        private protected void InternalOptimization()
        {
            // Can be overridden by derived classes in same assembly only
        }
        
        // Protected internal - flexible access
        protected internal void LogOperation(string message)
        {
            // Accessible: same assembly OR external derived classes
            Logger?.Log(message);
        }
        
        // Public method - main API
        public void SaveProduct(Product product)
        {
            ValidateProduct(product);
            InternalOptimization();
            LogOperation("Product saved");
        }
    }
    
    // Internal class - implementation detail
    internal class DatabaseHelper
    {
        internal void ExecuteQuery(string sql) { }
    }
    
    // Private nested class - encapsulated logic
    public class Order
    {
        private class OrderValidator
        {
            public bool Validate(Order order) { return true; }
        }
    }
}
```

---

## 8. When to Use Each Modifier - Decision Tree

```
╔════════════════════════════════════════════════════════════════╗
║  Should this be accessible from external assemblies?           ║
╚════════════════════════════════════════════════════════════════╝
         │
         ├─── YES → Should ALL external code access it?
         │          ├─── YES → public
         │          └─── NO  → protected (derived classes only)
         │
         └─── NO  → Is it for inheritance within this assembly?
                    ├─── YES → private protected
                    │
                    └─── NO  → Should other types in this assembly access it?
                               ├─── YES → internal
                               └─── NO  → private
```

---

**Total Guide Length:** ~2, 500 words  
**Complexity Level:** Junior to Senior Interview Preparation  
**Last Updated:** January 2026 (. NET 10)
