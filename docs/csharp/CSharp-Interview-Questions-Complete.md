# C# Interview Questions – Complete Guide (154 Questions: Basic to Advanced)

## C# Basics

**Q1. What is the default access modifier of a class in C#?**  
A. `internal` by default, meaning the class is accessible only within the same assembly.

**Q2. What is the default access modifier of class members (fields, methods)?**  
A. `private` by default, meaning they are accessible only within the containing class.

**Q3. What are the built-in value types in C#?**  
A. `int`, `float`, `double`, `decimal`, `char`, `bool`, `byte`, `short`, `long`, `struct`, `enum`. They are stored on the stack and hold actual values.

**Q4. What are reference types in C#?**  
A. `class`, `interface`, `delegate`, `object`, `string`, `array`. They are stored on the heap and hold references (memory addresses) to actual data.

**Q5. What is the difference between `string` and `StringBuilder`?**  
A. `string` is immutable (every modification creates a new object). `StringBuilder` is mutable (modifies the same object), making it efficient for multiple concatenations.

**Q6. What is the difference between `==` and `Equals()` for strings?**  
A. For strings, both == and Equals() compare values (content) because string overrides Equals() and overloads the == operator to perform value comparison instead of reference comparison.

== is null-safe, while calling Equals() on a null object throws a NullReferenceException.

For reference types (custom objects), both == and Equals() compare references by default, unless Equals() and/or == are explicitly overridden.

```csharp
public class Program
{
	public static void Main(string[] args)
	{
      var p1 = new Person { Name = "Sanjay" };
      var p2 = new Person { Name = "Sanjay" };
      
      Console.WriteLine(p1 == p2);        // false (different references)
      Console.WriteLine(p1.Equals(p2));   // false (same as == by default)
      
	  string a = "Hello";
      string b = "Hello";
      string c = null;
      
      Console.WriteLine(a == b);          // true
      Console.WriteLine(a.Equals(b));     // true
      
      Console.WriteLine(a == c);          // false
      Console.WriteLine(c == null);       // true
      
      Console.WriteLine(c.Equals(a));     // ❌ NullReferenceException
	}
}

class Person
{
      public string Name { get; set; }
}
```
Key Clarification (One-liner)
Objects → reference comparison by default
Strings → value comparison (special case)

**Q7. What is boxing and unboxing?**  
A. Boxing converts a value type to reference type (object). Unboxing converts reference type back to value type. Both have performance overhead due to heap allocation and type checking.

**Q8. What is the `var` keyword?**  
A. `var` is implicitly typed, meaning the compiler infers the type at compile-time based on the assigned value. Once assigned, the type cannot change (it's strongly typed).

**Q9. What is the difference between `var` and `dynamic`?**  
A. `var` is resolved at compile-time (statically typed). `dynamic` is resolved at runtime (dynamically typed), bypassing compile-time type checking. Use `dynamic` for interop or reflection scenarios.

**Q10. What is the `object` type?**  
A. `object` is the base class for all types in C#. It can hold any value but requires explicit casting to retrieve the original type. Boxing occurs when assigning value types to `object`.

---

## Access Modifiers

**Q11. What are the access modifiers in C#?**  
A. `public`, `private`, `protected`, `internal`, `protected internal`, `private protected`. They control the visibility and accessibility of classes, methods, and members.

**Q12. What is the difference between `internal` and `public`?**  
A. `public` is accessible from any assembly. `internal` is accessible only within the same assembly (project). Use `internal` to hide implementation details from external consumers.

**Q13. What is `protected internal`?**  
A. Accessible within the same assembly OR by derived classes in other assemblies. It's the union of `protected` and `internal`.

**Q14. What is `private protected`?**  
A. Accessible only by derived classes within the same assembly. It's the intersection of `protected` and `internal` (more restrictive than `protected internal`).

**Q15. Can you have a `private` class?**  
A. Yes, but only as a nested class within another class. Top-level classes can only be `public` or `internal`.

---

## OOP Concepts

**Q16. What is Encapsulation?**  
A. Bundling data (fields) and methods that operate on the data into a single unit (class), and restricting direct access to some components using access modifiers (private fields, public properties).

**Q17. What is Inheritance?**  
A. A mechanism where a class (child/derived) inherits members from another class (parent/base), promoting code reuse. C# supports single inheritance (one base class) but multiple interface implementation.

**Q18. What is Polymorphism?**  
A. The ability of objects to take multiple forms. Compile-time (method overloading) and runtime (method overriding with `virtual`/`override`) polymorphism allow the same method name to behave differently.

**Q19. What is Abstraction?**  
A. Hiding complex implementation details and exposing only essential features. Achieved using abstract classes (partial implementation) or interfaces (contracts with no implementation).

**Q20. What is the difference between method overloading and overriding?**  
A. Overloading: Same method name, different parameters (compile-time polymorphism). Overriding: Child class redefines a `virtual` or `abstract` method from the parent (runtime polymorphism).

**Q21. Can you override a non-virtual method?**  
A. No. Only methods marked `virtual`, `abstract`, or `override` can be overridden. Use the `new` keyword to hide (not override) a non-virtual method in the derived class.

**Q22. What is the `base` keyword?**  
A. Used to access members of the base class from the derived class, such as calling the base class constructor or accessing overridden methods.

**Q23. What is the `sealed` keyword?**  
A. Prevents a class from being inherited or a method from being overridden. Use `sealed` on classes to prevent further derivation or on overridden methods to stop the override chain.

**Q24. Why and when should you use the `sealed` keyword?**  
A. Use `sealed` for security (prevent malicious inheritance), performance (enables compiler optimizations like devirtualization), or design intent (class is complete and not meant for extension). Example: `string` class is sealed in .NET.

---

## Classes vs Structs

**Q25. What is the difference between class and struct?**  
A. Class is a reference type (heap), supports inheritance, has default constructor, can be `null`. Struct is a value type (stack), no inheritance (except interfaces), cannot be `null` (unless nullable), more lightweight.

**Q26. When should you use a struct instead of a class?**  
A. Use struct for small, immutable, value-semantic types (e.g., `Point`, `Color`) that are frequently created and destroyed. Classes are better for larger, mutable objects with complex behavior.

**Q27. Can a struct have a default constructor?**  
A. In C# 10+, yes (you can define parameterless constructors). Before C# 10, structs always had an implicit parameterless constructor that initializes fields to default values.

**Q50. Can a struct inherit from another struct or class?**  
A. No. Structs cannot inherit from other structs or classes, but they can implement interfaces. All structs implicitly inherit from `System.ValueType`.

---

## Interfaces vs Abstract Classes

**Q46. What is an interface?**  
A. A contract that defines method signatures, properties, events, or indexers without implementation. Classes or structs that implement an interface must provide implementations for all members.

**Q46. What is an abstract class?**  
A. A class that cannot be instantiated and may contain both abstract (no implementation) and concrete (with implementation) members. Derived classes must implement all abstract members.

**Q46. What is the difference between interface and abstract class?**  
A. Interface: No implementation (until C# 8 default methods), multiple inheritance supported, no fields. Abstract class: Can have implementation, single inheritance, can have fields/constructors/access modifiers.

**Q46. When should you use an interface vs abstract class?**  
A. Use interface for contracts that unrelated classes can implement (e.g., `IDisposable`, `IComparable`). Use abstract class for shared base functionality among related classes with common behavior.

**Q46. Can an interface have fields?**  
A. No. Interfaces cannot have fields, but they can have properties (which are method contracts for getters/setters).

**Q46. Can an interface have constructors?**  
A. No. Interfaces cannot have constructors because they cannot be instantiated directly.

**Q46. What are default interface methods (C# 8)?**  
A. Interfaces can now have default implementations for methods, allowing you to add new methods to existing interfaces without breaking implementations. Use sparingly to maintain interface simplicity.

---

## Constructors & Destructors

**Q46. What is a constructor?**  
A. A special method called when an object is created, used to initialize fields. It has the same name as the class and no return type. Constructors can be overloaded.

**Q46. What is a parameterless (default) constructor?**  
A. A constructor with no parameters. If you don't define any constructor, the compiler automatically provides a parameterless constructor. If you define any constructor, the default one is not auto-generated.

**Q46. What is constructor chaining?**  
A. Calling one constructor from another within the same class (using `this`) or from the base class (using `base`). Helps reduce code duplication.

**Q46. What is a static constructor?**  
A. A constructor that initializes static members of a class. It's called automatically before the first instance is created or any static member is accessed. It has no access modifier, no parameters, and runs only once.

**Q44. What is a destructor (finalizer)?**  
A. A method (prefixed with `~`) called by the garbage collector before an object is destroyed. Used to release unmanaged resources. Avoid using destructors if possible; prefer `IDisposable` and `Dispose()`.

**Q45. What is the difference between `Dispose()` and destructor?**  
A. `Dispose()` is deterministic (you control when it's called) and used to release unmanaged resources immediately. Destructor is non-deterministic (called by GC at an unpredictable time) and adds overhead.

**Q46. What is a private constructor and when do you use it?**  
A. A constructor with `private` access modifier. Used to prevent instantiation from outside the class. Common use cases: Singleton pattern, static-only utility classes, or when using factory methods for object creation.

**Q47. What is a protected constructor and when do you use it?**  
A. A constructor with `protected` access modifier. Used in abstract classes or base classes to allow instantiation only by derived classes, preventing direct instantiation of the base class.

**Q48. Why use a static constructor?**  
A. To initialize static fields/properties before any instance is created or static members are accessed. Common uses: loading configuration, initializing static caches, setting up static connections, or complex static field initialization that requires logic.

---

## Static vs Instance Members

**Q46. What is a static member?**  
A. A member (field, method, property) that belongs to the class itself, not to any instance. Accessed using the class name. Shared across all instances.

**Q46. What is the difference between static and instance methods?**  
A. Static methods belong to the class and cannot access instance members (because there's no `this` context). Instance methods belong to an object and can access both static and instance members.

**Q46. Can a static class have instance members?**  
A. No. Static classes can only contain static members and cannot be instantiated. Used for utility/helper classes (e.g., `Math`, `Console`).

**Q47. Can you have a static constructor in a non-static class?**  
A. Yes. Static constructors initialize static members of any class (static or non-static). They run once before the class is used.

**Q48. What is the `this` keyword?**  
A. Refers to the current instance of the class. Used to access instance members, differentiate between parameters and fields, or pass the current object as a parameter.

---

## Value Types vs Reference Types

**Q49. What is the difference between value types and reference types?**  
A. Value types store data directly (stack allocation), passed by value (copy). Reference types store references to data (heap allocation), passed by reference (same object). Examples: `int` vs `class`.

**Q50. Where are value types and reference types stored?**  
A. Value types are typically stored on the stack (unless part of a reference type). Reference types are stored on the heap, with the reference (pointer) on the stack.

**Q51. What happens when you assign one value type to another?**  
A. A copy of the value is created. Changes to one variable do not affect the other.

**Q52. What happens when you assign one reference type to another?**  
A. Both variables point to the same object in memory. Changes through one variable affect the other because they reference the same object.

**Q53. Can value types be `null`?**  
A. No, by default. But nullable value types (`int?`, `bool?`) can represent `null` using `Nullable<T>`.

---

## ref, out, in Keywords

**Q54. What is the `ref` keyword?**  
A. Passes a variable by reference (not a copy). Changes inside the method affect the original variable. The variable must be initialized before passing.

**Q59. What is the `out` keyword?**  
A. Similar to `ref`, but the variable does not need to be initialized before passing. The method must assign a value to the `out` parameter before returning.

**Q60. What is the `in` keyword?**  
A. Passes a variable by reference (readonly). The method cannot modify the value. Used for performance (avoids copying large structs) while ensuring immutability.

**Q61. What is the difference between `ref` and `out`?**  
A. `ref` requires the variable to be initialized before passing; `out` does not. `out` parameters must be assigned inside the method before returning; `ref` parameters are optional.

**Q62. When should you use `in` instead of `ref`?**  
A. Use `in` when you want to pass large structs by reference (for performance) but don't want the method to modify the value. It's a read-only reference.

---

## readonly vs const

**Q63. What is the difference between `readonly` and `const`?**  
A. `const` is compile-time constant (value must be known at compile-time), implicitly static, cannot be modified. `readonly` is runtime constant (can be assigned in constructor), can be instance or static, value can differ per instance.

**Q64. Can you change a `readonly` field after it's assigned?**  
A. No, once assigned (either at declaration or in the constructor), it cannot be changed. However, if it's a reference type, the object it points to can be mutated.

**Q65. Can a `const` field be an instance member?**  
A. No. `const` fields are implicitly static and belong to the type, not to instances.

**Q66. What types can be `const`?**  
A. Only primitive types (`int`, `float`, `bool`, `char`) and `string`. Reference types (except `string`) cannot be `const`.

---

## var, dynamic, object

**Q67. What is the difference between `var`, `dynamic`, and `object`?**  
A. `var`: Implicitly typed at compile-time (strongly typed). `dynamic`: Resolved at runtime (bypasses compile-time checking). `object`: Base type requiring explicit casting; boxing occurs for value types.

**Q68. When should you use `dynamic`?**  
A. Use `dynamic` for COM interop, dynamic languages (IronPython), reflection scenarios, or when the type is truly unknown at compile-time. Avoid overuse due to lack of compile-time safety.

**Q69. Can you reassign a different type to a `var` variable?**  
A. No. Once the compiler infers the type for `var`, it becomes strongly typed and cannot be reassigned to a different type.

**Q70. Can you use `var` for method parameters?**  
A. No. `var` is only for local variables. Method parameters, return types, and fields must have explicit types (or use generics).

---

## Exception Handling

**Q71. What is exception handling?**  
A. A mechanism to handle runtime errors gracefully using `try`, `catch`, `finally`, and `throw`. Prevents application crashes and provides error recovery.

**Q72. What is the difference between `throw` and `throw ex`?**  
A. `throw` re-throws the original exception preserving the stack trace. `throw ex` resets the stack trace to the current method, losing original error context. Always use `throw`.

**Q73. What is the `finally` block?**  
A. A block that always executes, whether an exception is thrown or not. Used for cleanup (closing files, database connections). Executes even if there's a `return` in `try` or `catch`.

**Q74. What is the difference between `Exception` and `ApplicationException`?**  
A. `Exception` is the base class for all exceptions. `ApplicationException` was intended for custom exceptions but is now obsolete. Derive custom exceptions directly from `Exception` or specific exception types.

**Q75. What are checked vs unchecked exceptions in C#?**  
A. C# does not have checked exceptions (like Java). All exceptions are unchecked, meaning methods don't declare which exceptions they throw. Developers must document or handle exceptions appropriately.

**Q76. What is the `using` statement?**  
A. A syntactic sugar for `try-finally` that automatically calls `Dispose()` on objects implementing `IDisposable`. Ensures resources are released even if exceptions occur.

**Q77. What is exception filtering (C# 6)?**  
A. Using `when` keyword to catch exceptions conditionally: `catch (Exception ex) when (ex.Message.Contains("404"))`. Only catches exceptions that meet the condition.

---

## Collections & Generics

**Q78. What are generics?**  
A. Type-safe parameterized types that allow you to define classes, methods, and interfaces with placeholder types (e.g., `List<T>`, `Dictionary<TKey, TValue>`). Provides compile-time type safety and avoids boxing.

**Q79. What is the difference between `List<T>` and `ArrayList`?**  
A. `List<T>` is generic (type-safe, no boxing). `ArrayList` is non-generic (stores `object`, requires casting, boxing for value types). Always prefer `List<T>`.

**Q80. What is the difference between `Array` and `List<T>`?**  
A. `Array` has fixed size (cannot grow/shrink). `List<T>` is dynamic (automatically resizes). Arrays are faster for fixed-size collections; `List<T>` is more flexible.

**Q81. What is `Dictionary<TKey, TValue>`?**  
A. A collection of key-value pairs with fast lookups (O(1) average). Keys must be unique. Internally uses a hash table.

**Q82. What is the difference between `IEnumerable<T>` and `ICollection<T>`?**  
A. `IEnumerable<T>` provides read-only iteration (forward-only). `ICollection<T>` extends `IEnumerable<T>` and adds `Count`, `Add()`, `Remove()`, and modification capabilities.

**Q83. What is the difference between `ICollection<T>` and `IList<T>`?**  
A. `IList<T>` extends `ICollection<T>` and adds indexer access (`list[i]`), `Insert()`, `IndexOf()`, allowing random access and positional operations.

**Q84. What are generic constraints?**  
A. Restrictions on type parameters (e.g., `where T : class`, `where T : struct`, `where T : new()`, `where T : IComparable<T>`). Ensures the generic type meets certain requirements.

**Q85. What is covariance and contravariance?**  
A. Covariance (`out`): Allows a more derived type (e.g., `IEnumerable<Derived>` → `IEnumerable<Base>`). Contravariance (`in`): Allows a less derived type (e.g., `Action<Base>` → `Action<Derived>`). Used in generics and delegates.

---

## LINQ

**Q86. What is LINQ?**  
A. Language Integrated Query allows querying collections (arrays, lists, databases) using SQL-like syntax. Supports method syntax and query syntax. Operates on `IEnumerable<T>` or `IQueryable<T>`.

**Q87. What is the difference between `IEnumerable<T>` and `IQueryable<T>`?**  
A. `IEnumerable<T>`: In-memory queries (LINQ to Objects), executes in application. `IQueryable<T>`: Queryable data sources (LINQ to SQL/EF), translates expressions to SQL, executes on database.

**Q88. What is deferred execution in LINQ?**  
A. LINQ queries are not executed when defined but when iterated (e.g., `foreach`, `.ToList()`, `.Count()`). Allows query composition and optimization.

**Q89. What is the difference between `Where()` and `Select()`?**  
A. `Where()` filters elements (returns subset). `Select()` projects/transforms elements (returns new shape). Example: `Where(x => x > 5)` filters; `Select(x => x * 2)` transforms.

**Q90. What is the difference between `First()` and `FirstOrDefault()`?**  
A. `First()` throws an exception if no element is found. `FirstOrDefault()` returns the default value (`null` for reference types, `0` for `int`, etc.) if no element exists.

**Q91. What is the difference between `Any()` and `All()`?**  
A. `Any()` checks if at least one element satisfies the condition. `All()` checks if all elements satisfy the condition. Both return `bool`.

**Q92. What is method syntax vs query syntax in LINQ?**  
A. Method syntax uses lambda expressions: `list.Where(x => x > 5).Select(x => x * 2)`. Query syntax uses SQL-like keywords: `from x in list where x > 5 select x * 2`. Both compile to the same code.

---

## Delegates, Events, Func, Action, Predicate

**Q93. What is a delegate?**  
A. A type-safe function pointer that references methods with matching signatures. Enables passing methods as parameters, callbacks, and event handling. Supports multicasting (multiple methods).

**Q94. What is a multicast delegate?**  
A. A delegate that holds references to multiple methods. When invoked, all methods are called in order. Use `+=` to add methods and `-=` to remove.

**Q95. What is the difference between delegates and events?**  
A. Delegates are type-safe function pointers. Events are a special kind of delegate with restrictions: can only be invoked by the declaring class (encapsulation), subscribers can only `+=` or `-=` (not reassign).

**Q96. What are `Func`, `Action`, and `Predicate`?**  
A. Built-in generic delegates. `Func<T, TResult>`: Takes parameters, returns a value. `Action<T>`: Takes parameters, returns void. `Predicate<T>`: Takes one parameter, returns `bool`.

**Q97. What is the difference between `Func<T>` and `Action<T>`?**  
A. `Func<T>` returns a value (last type parameter is the return type). `Action<T>` returns void. Example: `Func<int, int>` takes int, returns int; `Action<int>` takes int, returns void.

**Q98. What is an anonymous method?**  
A. A method without a name, defined inline using the `delegate` keyword. Superseded by lambda expressions for cleaner syntax.

**Q99. What is a lambda expression?**  
A. A concise syntax for anonymous methods using `=>` operator. Example: `x => x * 2` is equivalent to `delegate(int x) { return x * 2; }`.

**Q100. What is the difference between `Func<int, int>` and `Expression<Func<int, int>>`?**  
A. `Func<int, int>` is a compiled delegate (executable code). `Expression<Func<int, int>>` is an expression tree (data structure representing code), used by LINQ providers (EF) to translate to SQL.

---

## Async / Await & Task

**Q101. What is the difference between synchronous and asynchronous code?**  
A. Synchronous: Code executes sequentially, blocking until each operation completes. Asynchronous: Code starts long-running operations without blocking, allowing other work to proceed. Use `async`/`await` for async code.

**Q102. What is `async` and `await`?**  
A. `async` marks a method as asynchronous, allowing the use of `await`. `await` pauses execution until the awaited `Task` completes, without blocking the thread. Improves responsiveness and scalability.

**Q103. What is a `Task`?**  
A. Represents an asynchronous operation. `Task` returns no value (like `void`). `Task<T>` returns a value of type `T`. Created using `async` methods or `Task.Run()`.

**Q104. What is the difference between `Task.Run()` and `Task.Factory.StartNew()`?**  
A. `Task.Run()` is simpler, uses default scheduler, and queues work on the thread pool. `Task.Factory.StartNew()` offers more control (custom scheduler, options) but is more complex. Prefer `Task.Run()`.

**Q105. What is the difference between `Task` and `Thread`?**  
A. `Thread` represents an OS thread (heavyweight, expensive to create). `Task` is a higher-level abstraction representing work, managed by the thread pool (lightweight, efficient). Use `Task` for async I/O; use `Thread` only for long-running CPU work.

**Q106. What happens if you don't `await` a `Task`?**  
A. The method continues without waiting for the task to complete (fire-and-forget). Exceptions are not observed, potentially causing silent failures. Always `await` tasks or handle exceptions explicitly.

**Q107. What is `ConfigureAwait(false)`?**  
A. Tells `await` not to capture the synchronization context (e.g., UI thread context). Improves performance in library code by avoiding unnecessary context switching. Don't use in UI apps where you need to return to the UI thread.

**Q108. What is the difference between `Task.WaitAll()` and `Task.WhenAll()`?**  
A. `Task.WaitAll()` blocks the calling thread until all tasks complete (synchronous). `Task.WhenAll()` returns a `Task` that completes when all tasks finish (asynchronous, awaitable).

**Q109. Can you use `async void`?**  
A. Only for event handlers (e.g., button click). For all other methods, use `async Task` because `async void` cannot be awaited, and exceptions cannot be caught outside the method.

---

## Memory Management & Garbage Collection

**Q110. What is the Garbage Collector (GC)?**  
A. An automatic memory management system that reclaims memory occupied by unreachable objects. Eliminates manual memory management (no `free()`/`delete`) but adds non-deterministic performance overhead.

**Q111. What are GC generations?**  
A. Gen 0 (short-lived objects, collected frequently), Gen 1 (medium-lived, buffer between Gen 0 and Gen 2), Gen 2 (long-lived objects, collected rarely). Objects surviving collections are promoted to higher generations.

**Q112. What is the difference between `Dispose()` and `Finalize()`?**  
A. `Dispose()` is deterministic (you call it explicitly via `IDisposable`), releases managed and unmanaged resources. `Finalize()` (destructor) is non-deterministic (GC calls it), only releases unmanaged resources. Use `Dispose()` pattern.

**Q113. What is the `IDisposable` interface?**  
A. An interface with a single `Dispose()` method for releasing unmanaged resources (file handles, database connections). Use with `using` statement for automatic cleanup.

**Q114. What is the Large Object Heap (LOH)?**  
A. A separate heap for objects ≥85,000 bytes. Not compacted by default (to avoid expensive copying). Can cause fragmentation. LOH objects are Gen 2.

**Q115. What is a memory leak in .NET?**  
A. When objects are unintentionally kept alive (referenced) and cannot be garbage collected, causing memory to grow unbounded. Common causes: event handlers not unsubscribed, static collections holding references.

**Q116. What is the difference between stack and heap?**  
A. Stack: Fast, LIFO, stores value types and method call information, limited size. Heap: Slower, stores reference types, managed by GC, larger size. Stack allocation/deallocation is automatic; heap requires GC.

**Q117. What is a weak reference?**  
A. A reference that doesn't prevent the GC from collecting an object. Used for caches where objects can be recreated if collected. Created using `WeakReference<T>`.

---

## Dependency Injection Basics

**Q118. What is Dependency Injection (DI)?**  
A. A design pattern where dependencies (services) are provided to a class from the outside (via constructor, property, or method) rather than created inside the class. Promotes loose coupling and testability.

**Q119. What are the benefits of Dependency Injection?**  
A. Loose coupling (classes depend on abstractions, not concrete implementations), testability (easy to mock dependencies), maintainability (change implementations without modifying dependent classes), and better separation of concerns.

**Q120. What are the three types of Dependency Injection?**  
A. Constructor Injection (dependencies passed via constructor, most common), Property Injection (dependencies set via public properties), Method Injection (dependencies passed to methods when needed).

**Q121. What are service lifetimes in ASP.NET Core DI?**  
A. Transient (new instance per request), Scoped (one instance per HTTP request/scope), Singleton (one instance for application lifetime). Choose based on statefulness and resource usage.

**Q122. What is the difference between Transient and Scoped?**  
A. Transient: New instance every time requested (even within the same HTTP request). Scoped: Same instance within a single scope (HTTP request in web apps), but new instance for each scope.

**Q123. What is a DI container?**  
A. A framework component (e.g., ASP.NET Core's built-in DI, Autofac, Ninject) that manages object creation, lifetime, and dependency resolution. Registers services and resolves them when needed.

**Q124. What is constructor injection?**  
A. Passing dependencies through the constructor. The class declares dependencies as constructor parameters, and the DI container provides them. Most common and recommended approach.

---

## .NET Runtime (CLR, CTS, CLS)

**Q125. What is the Common Language Runtime (CLR)?**  
A. The execution engine for .NET applications. Provides services like memory management (GC), exception handling, security, JIT compilation, and thread management. Manages code execution from IL to machine code.

**Q126. What is Just-In-Time (JIT) compilation?**  
A. The process of converting Intermediate Language (IL) to native machine code at runtime. Improves portability (IL is platform-independent) but adds startup delay. Types: Tiered JIT, R2R (ReadyToRun).

**Q127. What is the Common Type System (CTS)?**  
A. A specification defining how types are declared, used, and managed in .NET. Ensures type compatibility across .NET languages (C#, F#, VB.NET). Defines value types, reference types, and type members.

**Q128. What is the Common Language Specification (CLS)?**  
A. A subset of CTS defining rules for language interoperability. CLS-compliant code can be used by all .NET languages. Example: Avoid unsigned types (`uint`) in public APIs for CLS compliance.

**Q129. What is Intermediate Language (IL/MSIL)?**  
A. Platform-independent bytecode generated by .NET compilers. CLR's JIT compiler converts IL to native machine code at runtime. IL is stored in assemblies (.dll/.exe).

**Q130. What is an assembly?**  
A. A compiled code library (.dll) or executable (.exe) containing IL code, metadata, and resources. Assemblies are the deployment unit in .NET and can be private or shared (GAC).

**Q131. What is the Global Assembly Cache (GAC)?**  
A. A machine-wide cache for shared assemblies used by multiple applications. Assemblies in GAC must be strongly named (signed with a key). Modern .NET (Core/5+) doesn't use GAC.

**Q132. What is the difference between .NET Framework and .NET Core/.NET 5+?**  
A. .NET Framework: Windows-only, full-featured, legacy. .NET Core/.NET 5+: Cross-platform (Windows, Linux, macOS), modular, modern, high-performance, open-source. .NET 5+ is the unified future of .NET.

---

## Advanced Topics

**Q133. What is reflection?**  
A. The ability to inspect and manipulate types, methods, properties, and assemblies at runtime. Used for dynamic loading, serialization, dependency injection, and frameworks. Slower than compile-time code.

**Q134. What is an attribute?**  
A. Metadata attached to code elements (classes, methods, properties) using `[AttributeName]` syntax. Examples: `[Obsolete]`, `[Serializable]`, `[Required]`. Custom attributes can be created by inheriting from `Attribute`.

**Q135. What is serialization?**  
A. Converting an object to a format (JSON, XML, binary) for storage or transmission. Deserialization reconstructs the object. Used for APIs, caching, and persistence.

**Q136. What is the difference between `is` and `as` keywords?**  
A. `is` checks if an object is of a specific type (returns `bool`). `as` attempts to cast to a type, returning `null` if the cast fails (no exception). Use `as` when you expect the cast might fail.

**Q137. What is pattern matching (C# 7+)?**  
A. Enhanced `switch` and `is` expressions that match objects against patterns (type, property, constant). Example: `if (obj is string s && s.Length > 5)`. Simplifies type checking and data extraction.

**Q138. What is a nullable reference type (C# 8)?**  
A. A feature that makes reference types non-nullable by default. Use `?` to declare nullable reference types (e.g., `string?`). Helps prevent `NullReferenceException` at compile-time with warnings.

**Q139. What is the `nameof` operator?**  
A. Returns the name of a variable, type, or member as a string. Example: `nameof(Customer.Name)` returns `"Name"`. Provides compile-time safety for strings (refactoring-safe).

**Q140. What is tuple?**  
A. A lightweight data structure holding multiple values of different types. Value tuple (C# 7): `(int, string) person = (1, "John");` with named elements: `(int Id, string Name)`. Use for returning multiple values.

**Q141. What is deconstruction?**  
A. Breaking down tuples or objects into individual variables. Example: `var (id, name) = GetPerson();`. Custom types can support deconstruction by defining `Deconstruct` methods.

**Q142. What is the `yield` keyword?**  
A. Used in iterators to return elements one at a time (`yield return`) or end iteration (`yield break`). The compiler generates a state machine. Enables lazy evaluation (values produced on-demand).

**Q143. What is the difference between `IEnumerable` and `IEnumerator`?**  
A. `IEnumerable` represents a collection that can be iterated (has `GetEnumerator()` method). `IEnumerator` is the iterator itself with `MoveNext()` and `Current` for traversing the collection.

**Q144. What is covariance in delegates?**  
A. Allows a method returning a derived type to be assigned to a delegate expecting a base type. Example: `Func<object> func = () => "string";` (string derives from object).

**Q145. What is the `lock` statement?**  
A. Provides thread synchronization by ensuring only one thread can execute a code block at a time. Uses `Monitor.Enter()` and `Monitor.Exit()` internally. Prevents race conditions on shared resources.

**Q146. What is the `volatile` keyword?**  
A. Prevents compiler and CPU optimizations that reorder reads/writes to a field. Ensures visibility of changes across threads. Use for simple flags; prefer `lock` or `Interlocked` for complex synchronization.

**Q147. What is the `params` keyword?**  
A. Allows a method to accept a variable number of arguments as an array. Example: `void Print(params string[] items)` can be called with `Print("a", "b", "c")` without creating an array explicitly.

**Q148. What is operator overloading?**  
A. Defining custom behavior for operators (`+`, `-`, `*`, `==`, etc.) on user-defined types. Example: `public static Point operator +(Point a, Point b)`. Use sparingly for intuitive operations.

**Q149. What is indexer?**  
A. Allows objects to be indexed like arrays using `[]` syntax. Defined using `this[int index]`. Example: `var item = myCollection[5];`. Commonly used in custom collections.

**Q150. What is extension method?**  
A. Static method that appears as an instance method on a type. Defined in a static class with `this` modifier on the first parameter. Example: `public static bool IsEmpty(this string str)`. Cannot access private members.

**Q151. What is partial class?**  
A. A class split across multiple files using the `partial` keyword. All parts are combined at compile-time. Useful for separating auto-generated code from manual code (e.g., EF models, WinForms).

**Q152. What is an anonymous type?**  
A. A compiler-generated type with read-only properties, created using object initializer without explicitly defining a class. Example: `var person = new { Name = "John", Age = 30 };`. Used in LINQ projections.

**Q153. What is implicit vs explicit interface implementation?**  
A. Implicit: Interface members are public and accessible as class members. Explicit: Interface members are only accessible through the interface reference, hidden from the class. Use explicit to avoid naming conflicts.

**Q154. What is the `default` keyword?**  
A. Returns the default value for a type (`null` for reference types, `0` for numeric types, `false` for bool). Example: `int x = default;` is `0`. Useful in generics: `return default(T);`.

**Q155. What is short-circuiting in logical operators?**  
A. `&&` and `||` operators stop evaluating as soon as the result is determined. Example: `false && ExpensiveMethod()` doesn't call `ExpensiveMethod()`. Use for performance and null checks.

**Q156. What is the conditional (ternary) operator?**  
A. A shorthand for `if-else`: `condition ? trueValue : falseValue`. Example: `string result = (age >= 18) ? "Adult" : "Minor";`. Use for simple conditions; avoid nesting.

**Q157. What is the null-coalescing operator (`??`)?**  
A. Returns the left operand if not `null`, otherwise returns the right operand. Example: `string name = input ?? "Default";`. C# 8 adds `??=` for conditional assignment: `name ??= "Default";`.

---

**Total Questions: 150** covering C# from basics to advanced topics, designed for interview preparation.
