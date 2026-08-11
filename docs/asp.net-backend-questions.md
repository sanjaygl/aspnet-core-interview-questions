## What is `yield`?

**A:**  The `yield` keyword is used to create custom iterators with lazy evaluation. Instead of generating and returning an entire collection at once, it returns elements one at a time on demand, pausing execution until the next element is requested.

**Ref:** [C# Yield Keyword – Complete Guide](CSharp\CSharp-Yield-Keyword-Complete-Guide.md)

## What is CLR and how it works?
**A.**  The Common Language Runtime (CLR) is the execution engine for .NET applications. It converts Intermediate Language (IL) code to native machine code via a JIT compiler. It also provides the very important feature of a garbage collector, which cleans up unused objects.

**Ref:** [Free dot net video tutorials for beginners
](https://csharp-video-tutorials.blogspot.com/p/free-dot-net-video-tutorials-for.html)

## What is .Net Core and what's new in latest version?
**A.** .NET core is Free, Open Source Framework to build applications which compiled on multiple Platform. .Net 10 is the latest version as LTS. 

## What is the difference between Value type and Reference Type?
**A.** **value type** holds the **actual value** directly in its allocated memory. 

**reference type** stores a **memory address (pointer)** that directs to the actual object. The actual object data is always allocated on the managed **Heap**.

**Ref:** [Value-and-Reference-Types](CSharp\Value-and-Reference-Types.md)

## What is Garbage Collection?
**A.** Garbage Collection (GC) is an automatic memory management process in .NET that automatically reclaims memory by destroying objects on the managed heap that are no longer reachable or used by the application.

**Ref** [CLR Working Process — .NET Execution Model](CSharp\CLR-Complete-Guide.md)

## What is middleware and how we can create custom middleware?
**A.** **Middleware** is software assembled into an application pipeline to handle incoming HTTP requests and outgoing HTTP responses. Each component can choose whether to pass the request to the next component in the pipeline or short-circuit (stop) the execution.

### 🛠️ How to Create Custom Middleware

To create standard, strongly-typed custom middleware, you must follow these steps:
1. **Receive the Request Delegate:** Accept a `RequestDelegate` in the constructor to hold the reference to the next middleware component in the pipeline.
2. **Implement `InvokeAsync`:** Define a public method named `InvokeAsync` (or `Invoke`) that accepts the current `HttpContext`.
3. **Execute Custom Logic:** Add your custom logic before passing the request forward (e.g., logging, authentication, or headers).
4. **Pass to the Next Component:** Invoke the request delegate, passing the `HttpContext` forward.
5. **Register the Middleware:** Register it in the pipeline using `app.UseMiddleware<MyCustomMiddleware>()` inside your `Program.cs` file.

**Ref** [Middleware-Complete-Guide](ASP.NETCore\Middleware-Complete-Guide.md)

## What is Dependency Injection and explain when to use AddSingleton, AddScoped and AddTransient?
**A.** Dependency injection is a design pattern where class receives its dependency from external sources rather then creating them itself. Instead of class using `new` to instantiate dependencies, those dependencies are "injected" through the constructor, making the code more modular, testable, and maintainable.

**AddSingleton** : its object created once and hold the state of whole application life time, AddSingleton we can use in logger classes.

**AddScoped** : AddScoped object created at every http request, and we can use AddScoped injection for db related operation and transaction related operation.

**AddTransient** : AddTransient object is created at every injection, we can use AddTransient for logical operation where we don't need to keep the state of object.
 
**Ref** [DependencyInjection-Complete-Guide](ASP.NETCore\DependencyInjection-Complete-Guide.md)

## What is delegate?
**A.** A delegate is a type safe function pointer, it can point to multiple methods which have same signature.

**Ref** [Delegates](../src/OOP-Csharp-Concepts/Delegate.cs)

## What are the differences between Func, Action, and Predicate?
**A.** Func, Action, and Predicate are built-in generic delegates in C#.

A **Func** is a built-in delegate that points to a method that must return a value. The final generic type parameter always specifies the return type.
  ```csharp
  Func<int, int, int> add = (a, b) => a + b;
  int result = add(5, 3); // Returns 8
  ```
An **Action** is a built-in delegate that points to a method that returns `void` (no value).
  ```csharp
  Action<string> log = message => Console.WriteLine(message);
  log("Hello World"); // Prints "Hello World" to the console
  ```
A **Predicate** is a specialized, semantic wrapper around `Func<T, bool>`. It always takes exactly one input parameter and always returns a boolean value (`true` or `false`).
  ```csharp
  Predicate<int> isPositive = num => num > 0;
  bool check = isPositive(-5); // Returns false
  ```
## What is reflection?
**A.** Reflection is a C# feature that allows a program to inspect, metadata-analyze and dynamically interact with its own code at runtime.

**Ref** [Reflection—Interview Q&A](CSharp\Reflection—Interview.md)

## What is the difference between IEnumerable and IQueryable?
**A.** 
**IEnumerable**: Loads the entire dataset into your app memory first, then applies filters like Where or OrderBy. Best for local collections like List or arrays.
**IQueryable**: Translates your query commands into native database code (like SQL). It fetches only the matching data back, making it much faster for large databases.

**Ref** [IEnumerable-vs-IQueryable—Interview Q&A](CSharp\IEnumerable-vs-IQueryable—Interview.md)

## What is the default access modifier of Class, Interface and Variable etc?
**A.** Class-internal
Interface-public
variable-private

## What is the difference between Const and Readonly?
Const is only set value when we initialize the value;
Readonly, we can value set value in constructor.

**Ref** [const vs readonly vs static — Interview Q&A](CSharp\const-vs-readonly-vs-static—Interview.md)

## What is the difference of Array vs ArrayList vs Dictionary?
**Ref** [Array vs ArrayList vs Dictionary — Interview Q&A](CSharp\Array-vs-ArrayList-vs-Dictionary—Interview.md)

## What is SOLID Principle? What happens if we not follow or miss any SOLID principle?
**Ref** [SOLID-Design-Principles-Introduction](solid-principles\SOLID-Design-Principles-Introduction.md)