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