​# Polymorphism — Interview Questions (C#)

55 questions: 30 non-coding, 25 coding. All code examples are C#.

---

## Non-Coding Questions

**Question:** What is polymorphism, in one sentence?

**Answer:**
Polymorphism ("many forms") means the same method call, interface, or operator can behave differently depending on the actual runtime type of the object it's invoked on — the caller doesn't need to know the concrete type.

---

**Question:** What are the main types of polymorphism?

**Answer:**
- **Compile-time (static)** — resolved by the compiler: method overloading, operator overloading.
- **Runtime (dynamic)** — resolved while the program runs, via `virtual`/`override` or interfaces.
- **Parametric** — generics (`List<T>`) behave uniformly across types.
- **Ad-hoc** — a function/operator behaves differently per argument type (overloading).

---

**Question:** What's the difference between method overloading and method overriding?

**Answer:**
- **Overloading**: same method name, different parameter list, same class, resolved at compile time.
- **Overriding**: a derived class replaces a `virtual`/`abstract` method's implementation, same signature, resolved at runtime based on the object's actual type.

---

**Question:** How does runtime polymorphism work under the hood in .NET?

**Answer:**
Every object carries a reference to a **method table** for its actual runtime type. Calling a `virtual` method looks up the method in the object's method table (not the static/declared type's), so the correct override runs — this lookup is "virtual dispatch." Non-virtual methods are resolved at compile time directly, with no lookup.

---

**Question:** What is the `virtual` keyword for?

**Answer:**
It marks a method in a base class as overridable — without it, a derived class's same-named method either doesn't compile as an override, or silently hides the base method instead (see method hiding below) rather than participating in dynamic dispatch.

---

**Question:** What is the `override` keyword for, and why is it required (unlike Java)?

**Answer:**
It explicitly marks a method as overriding a `virtual`/`abstract` base member. C# requires it (unlike Java's optional `@Override`) so a typo'd signature is a **compile error** instead of silently creating an unrelated new method — this is a deliberate safety feature.

---

**Question:** What is method hiding, and how does the `new` keyword relate to it?

**Answer:**
If a derived class declares a method with the same name as a base method that is **not** `virtual`, using `new` explicitly says "I know this hides the base member rather than overriding it." Calls through a base-typed reference still invoke the base version; only calls through the derived-typed reference (or reflection) see the hidden one. This is compile-time resolution, not polymorphism.

---

**Question:** What's the practical difference between method hiding and overriding when calling through a base reference?

**Answer:**
With `override`, a base-typed reference still invokes the derived class's implementation (true polymorphism). With `new` (hiding), a base-typed reference invokes the **base's** implementation, because hiding is resolved by the *static* type of the reference, not the runtime type.

---

**Question:** What's the difference between an abstract class and an interface, with respect to polymorphism?

**Answer:**
Both allow polymorphic behavior through a common contract. An abstract class can provide shared state and partial implementation, and a type can inherit from only **one**. An interface has (traditionally) no state and a type can implement **many** — polymorphism via interfaces is more about "can do X" than "is-a Y."

---

**Question:** Can a method be both `virtual` and `private`? Why or why not?

**Answer:**
No — `private` members aren't visible to derived classes at all, so there's nothing to override. Virtual dispatch requires the member to be at least `protected` so a subclass can participate in the override chain.

---

**Question:** What is the `sealed` keyword, and how does it affect polymorphism?

**Answer:**
`sealed` on a class prevents further inheritance; `sealed override` on a method prevents further overriding in classes derived from the one that sealed it. It caps the polymorphic chain at that point deliberately — often used when a base author wants a specific override to be the final word.

---

**Question:** What is the Liskov Substitution Principle, and how does it relate to polymorphism?

**Answer:**
LSP says a subclass instance must be usable anywhere its base type is expected without breaking the caller's expectations (e.g. not throwing where the base wouldn't, not weakening postconditions). Polymorphism gives you the *mechanism* to substitute subtypes; LSP is the *design discipline* that keeps that substitution safe and predictable.

---

**Question:** What is covariance and contravariance in C# generic interfaces?

**Answer:**
**Covariance** (`out T`) lets you use a more derived type than originally specified — `IEnumerable<Derived>` can be treated as `IEnumerable<Base>`. **Contravariance** (`in T`) lets you use a more generic type — `Action<Base>` can be treated as `Action<Derived>`. Both are compile-time-checked forms of polymorphism over generic type parameters, safe only for read-only (`out`) or write-only (`in`) usage respectively.

---

**Question:** Is operator overloading a form of polymorphism?

**Answer:**
Yes — it's compile-time/ad-hoc polymorphism. The same operator (`+`, `==`, etc.) behaves differently depending on the operand types, resolved by the compiler based on the declared operator overloads.

---

**Question:** How does polymorphism apply to boxed value types and `object`?

**Answer:**
Every type in C# derives from `object`, so a value type like `int` can be boxed into an `object` reference. Calling a virtual member like `ToString()` on that boxed value still dispatches to the actual type's override (e.g. a custom struct's `ToString()`), even though the reference is statically typed as `object`.

---

**Question:** How do delegates provide a form of polymorphism?

**Answer:**
A delegate variable can be assigned any method matching its signature, regardless of which class defines it — calling the delegate invokes whichever method it currently points to. This is behavioral polymorphism without any shared class hierarchy at all, closer to first-class functions than classic OOP polymorphism.

---

**Question:** What is the difference between a variable's compile-time type and its runtime type?

**Answer:**
The compile-time (static) type is what the variable is *declared* as and determines which overloads/members are visible to the compiler. The runtime type is the actual object it refers to. Polymorphism exploits this gap: code written against the compile-time type (e.g. `Shape`) executes behavior from the runtime type (e.g. `Circle`).

---

**Question:** Why can a class implement multiple interfaces but inherit from only one base class?

**Answer:**
Multiple base classes would create ambiguity if two bases defined conflicting state or implementation (the "diamond problem"). Interfaces (until default interface methods) carried no state and, even with default methods, C# forces explicit disambiguation when two interfaces' default implementations collide — so multiple interface implementation is safe in a way multiple class inheritance isn't.

---

**Question:** What are default interface methods (C# 8+), and how do they interact with polymorphism?

**Answer:**
An interface can now provide a method body, so implementing types get default behavior for free and can still override it. This blurs the historical abstract-class-vs-interface line — you get multiple-contract flexibility *and* shared default implementation, with implementers still free to polymorphically override.

---

**Question:** Does C# support duck typing?

**Answer:**
Not natively through the static type system (unlike TypeScript's structural typing) — C# is nominally typed, so a type must explicitly declare it implements an interface. The `dynamic` keyword gets close to duck typing by deferring member resolution to runtime, but it sacrifices compile-time safety to do it.

---

**Question:** What's the difference between an abstract method and a virtual method?

**Answer:**
An `abstract` method has no implementation at all and *must* be overridden by any concrete derived class; the declaring class must also be `abstract`. A `virtual` method has a default implementation that derived classes *may* override — it's optional.

---

**Question:** What happens if a base method isn't marked `virtual` — can a derived class still "override" it?

**Answer:**
No — the compiler won't let you use `override`. You can still declare a same-named method with `new` (or without any keyword, which emits a warning), but that's method **hiding**, not overriding — it doesn't participate in dynamic dispatch.

---

**Question:** What's a real-world risk of calling a virtual member from a base class constructor?

**Answer:**
The derived class's override can run *before* the derived class's own field initializers have executed, since base constructors run first. This can silently operate on uninitialized derived-class state — a classic, subtle polymorphism pitfall.

---

**Question:** How does polymorphism help when iterating a collection of a base type?

**Answer:**
A single loop over `List<Shape>` can call `shape.Area()` once per element and get the *correct* per-type calculation for every concrete shape in the list, with zero type-checking or branching in the loop itself — the dispatch happens automatically per element.

---

**Question:** What are static abstract interface members (C# 11), and what new kind of polymorphism do they enable?

**Answer:**
They let an interface declare a `static abstract` member that implementing types must provide at the type level (not the instance level) — e.g. a common `Zero` property across numeric types. This enables **generic/static polymorphism**: a generic method constrained to such an interface can call the static member polymorphically per type parameter, without ever having an instance.

---

**Question:** What is the difference between explicit and implicit interface implementation, and how does it affect polymorphic calls?

**Answer:**
Implicit implementation exposes the method on the class's public surface directly. Explicit implementation (`IShape.Area()`) hides the member from the class's own type — it's only callable through a reference typed as the interface. This lets a class satisfy two interfaces that happen to share a member name/signature without collision, at the cost of that member being invisible except polymorphically through the interface.

---

**Question:** Is a `switch` expression on type patterns a substitute for polymorphism? What's the trade-off?

**Answer:**
It achieves similar per-type behavior without a virtual method, but every call site needs its own switch, and adding a new type means updating every switch — whereas true polymorphism means adding a new subclass with its own override, with zero changes to existing call sites. Pattern matching is fine for one-off logic; polymorphism scales better across many call sites.

---

**Question:** Why is overriding `Equals`, `GetHashCode`, and `ToString` a practical everyday example of polymorphism?

**Answer:**
Every type inherits default (often unhelpful) implementations from `object`. Overriding them means generic code — collections' hash lookups, debugger displays, string interpolation — automatically gets the *correct*, type-specific behavior without that generic code knowing anything about your specific type.

---

**Question:** How does the Strategy design pattern rely on polymorphism?

**Answer:**
A context class holds a reference to a strategy interface and calls its method without knowing which concrete strategy is plugged in. Swapping behavior means swapping which concrete implementation is injected — the context's code never changes. This is polymorphism used deliberately as an architectural seam for extensibility.

---

**Question:** Do C# records support polymorphism the same way classes do?

**Answer:**
Yes for `record class` (reference-type records) — they support inheritance, `virtual`/`override`, exactly like ordinary classes, while also getting value-based equality and `with`-expressions for free. `record struct` (value-type records) can't be inherited from (structs don't support inheritance), so polymorphism there is limited to interface implementation only.

---

**Question:** What's a real downside of deep polymorphic inheritance chains?

**Answer:**
Behavior for a given call can be scattered across many levels of `override`, making it hard to know which implementation actually runs without tracing the whole chain — this is a common reason many codebases now favor composition and interfaces over deep multi-level class hierarchies.

---

## Coding Questions

**Question:** Write a classic runtime polymorphism example using `virtual`/`override`.

**Answer:**
```csharp
abstract class Animal
{
    // No body — every concrete Animal must supply its own.
    public abstract string Speak();
}

class Dog : Animal
{
    public override string Speak() => "Woof"; // Dog's own contract fulfillment
}

class Cat : Animal
{
    public override string Speak() => "Meow"; // Cat's own contract fulfillment
}

void Announce(Animal animal)
{
    // Caller only knows "Animal" — the actual Speak() invoked depends
    // on the object's real runtime type, decided via virtual dispatch.
    Console.WriteLine(animal.Speak());
}

Announce(new Dog()); // "Woof"
Announce(new Cat()); // "Meow"
```

---

**Question:** Show method overloading (compile-time polymorphism).

**Answer:**
```csharp
class Calculator
{
    // Same method name, different parameter lists — the compiler picks
    // the right one based on the argument types at the call site.
    public int Add(int a, int b) => a + b;
    public double Add(double a, double b) => a + b;
    public string Add(string a, string b) => a + b; // concatenation, not math
}

var calc = new Calculator();
calc.Add(1, 2);       // 3 (int overload)
calc.Add(1.5, 2.5);   // 4.0 (double overload)
calc.Add("a", "b");   // "ab" (string overload)
```

---

**Question:** Show interface-based polymorphism with no shared base class.

**Answer:**
```csharp
interface IShape
{
    double Area();
}

class Circle : IShape
{
    private readonly double _radius;
    public Circle(double radius) => _radius = radius;
    public double Area() => Math.PI * _radius * _radius; // Circle's own formula
}

class Rectangle : IShape
{
    private readonly double _width, _height;
    public Rectangle(double width, double height) => (_width, _height) = (width, height);
    public double Area() => _width * _height; // Rectangle's own formula
}

double TotalArea(IEnumerable<IShape> shapes) =>
    // Each Area() call dispatches to the concrete type's implementation.
    shapes.Sum(shape => shape.Area());

TotalArea(new IShape[] { new Circle(2), new Rectangle(3, 4) });
```

---

**Question:** Demonstrate method hiding (`new`) versus true overriding (`override`), and show how a base-typed reference behaves differently for each.

**Answer:**
```csharp
class Base
{
    public virtual string VirtualGreet() => "Hello from Base (virtual)";
    public string HiddenGreet() => "Hello from Base (non-virtual)";
}

class Derived : Base
{
    public override string VirtualGreet() => "Hello from Derived (override)";
    public new string HiddenGreet() => "Hello from Derived (new/hiding)";
}

Base b = new Derived();
Console.WriteLine(b.VirtualGreet()); // "Hello from Derived (override)" — true polymorphism
Console.WriteLine(b.HiddenGreet());  // "Hello from Base (non-virtual)" — hiding is NOT polymorphic
```

---

**Question:** Write an abstract class mixing an `abstract` method (must override) and a `virtual` method (may override).

**Answer:**
```csharp
abstract class PaymentMethod
{
    public abstract decimal ProcessingFee(decimal amount); // every subtype must define this

    public virtual string DisplayName() => "Payment"; // subtypes may customize, but don't have to
}

class CreditCard : PaymentMethod
{
    public override decimal ProcessingFee(decimal amount) => amount * 0.029m; // required override
    public override string DisplayName() => "Credit Card"; // optional override, chosen here
}

class Cash : PaymentMethod
{
    public override decimal ProcessingFee(decimal amount) => 0m; // required override
    // DisplayName() not overridden — inherits "Payment" from the base.
}
```

---

**Question:** Show a `sealed override` and explain what it prevents.

**Answer:**
```csharp
class Base
{
    public virtual string Describe() => "Base";
}

class Middle : Base
{
    public sealed override string Describe() => "Middle"; // final word on this override chain
}

class Bottom : Middle
{
    // ERROR if uncommented — Describe() was sealed by Middle:
    // public override string Describe() => "Bottom";
}
```

---

**Question:** Show operator overloading as compile-time polymorphism.

**Answer:**
```csharp
struct Money
{
    public decimal Amount { get; }
    public Money(decimal amount) => Amount = amount;

    // '+' behaves differently for Money than it does for int/double —
    // resolved by the compiler based on operand types, not at runtime.
    public static Money operator +(Money a, Money b) => new Money(a.Amount + b.Amount);
}

var total = new Money(10) + new Money(5); // Money { Amount = 15 }
```

---

**Question:** Demonstrate covariance with `IEnumerable<out T>`.

**Answer:**
```csharp
class Animal { }
class Dog : Animal { }

IEnumerable<Dog> dogs = new List<Dog> { new Dog(), new Dog() };

// Legal because IEnumerable<T> declares T as covariant (`out T`) —
// a read-only sequence of Dog can safely stand in for a sequence of Animal.
IEnumerable<Animal> animals = dogs;

foreach (var a in animals) Console.WriteLine(a.GetType().Name); // "Dog", "Dog"
```

---

**Question:** Demonstrate contravariance with `Action<in T>`.

**Answer:**
```csharp
class Animal { }
class Dog : Animal { }

Action<Animal> feedAnyAnimal = animal => Console.WriteLine("Feeding an animal");

// Legal because Action<T> declares T as contravariant (`in T`) — an action
// that can handle ANY Animal can safely be used wherever a Dog-specific action is expected.
Action<Dog> feedDog = feedAnyAnimal;

feedDog(new Dog()); // "Feeding an animal"
```

---

**Question:** Show polymorphic iteration over a heterogeneous collection.

**Answer:**
```csharp
List<Animal> zoo = new() { new Dog(), new Cat(), new Dog() };

foreach (var animal in zoo)
{
    // No type-checking here at all — Speak() dispatches correctly per element.
    Console.WriteLine(animal.Speak());
}
// Output: Woof, Meow, Woof
```

---

**Question:** Show calling the base implementation from within an override using `base`.

**Answer:**
```csharp
class Logger
{
    public virtual string Format(string message) => $"[LOG] {message}";
}

class TimestampedLogger : Logger
{
    public override string Format(string message) =>
        // Extends the base behavior instead of fully replacing it.
        $"{DateTime.UtcNow:O} {base.Format(message)}";
}

new TimestampedLogger().Format("started"); // "2026-... [LOG] started"
```

---

**Question:** Bug hunt — spot the mistake that breaks polymorphism here.

**Answer:**
```csharp
class Base
{
    public virtual string Greet() => "Hello from Base";
}

class Derived : Base
{
    // BUG: missing 'override' — this compiles as method HIDING via an
    // implicit 'new', with a compiler warning, not a true override.
    public string Greet() => "Hello from Derived";
}

Base b = new Derived();
Console.WriteLine(b.Greet()); // "Hello from Base" — NOT what most people expect
```
**Fix:** add `override` explicitly:
```csharp
class Derived : Base
{
    public override string Greet() => "Hello from Derived"; // now true polymorphism
}
```

---

**Question:** Show a default interface method (C# 8+) and a type that overrides it.

**Answer:**
```csharp
interface IGreeter
{
    string Greet() => "Hello from the interface default"; // default body
}

class DefaultGreeter : IGreeter { } // gets the default for free

class CustomGreeter : IGreeter
{
    public string Greet() => "Hello from CustomGreeter"; // overrides the default
}

IGreeter g1 = new DefaultGreeter();
IGreeter g2 = new CustomGreeter();
Console.WriteLine(g1.Greet()); // "Hello from the interface default"
Console.WriteLine(g2.Greet()); // "Hello from CustomGreeter"
```

---

**Question:** Show pattern-matching-based type dispatch as a contrast to virtual-method polymorphism.

**Answer:**
```csharp
double AreaOf(object shape) => shape switch
{
    // Every new shape type requires editing THIS switch — unlike a
    // virtual Area() method, which needs no changes here at all.
    Circle c => Math.PI * c.Radius * c.Radius,
    Rectangle r => r.Width * r.Height,
    _ => throw new ArgumentException("Unknown shape")
};
```

---

**Question:** Show boxing polymorphism — a value type's override running through an `object` reference.

**Answer:**
```csharp
struct Point
{
    public int X, Y;
    public override string ToString() => $"({X}, {Y})"; // struct's own override
}

object boxed = new Point { X = 1, Y = 2 };

// Even though 'boxed' is statically typed as object, ToString() dispatches
// to Point's override, not object's default "Namespace.Point".
Console.WriteLine(boxed.ToString()); // "(1, 2)"
```

---

**Question:** Show delegate-based polymorphism — varying behavior without any class hierarchy.

**Answer:**
```csharp
void Process(int[] numbers, Func<int, int> transform)
{
    // Process() has no idea what transform actually does — it just calls it.
    foreach (var n in numbers) Console.WriteLine(transform(n));
}

Process(new[] { 1, 2, 3 }, x => x * 2);  // 2, 4, 6
Process(new[] { 1, 2, 3 }, x => x * x);  // 1, 4, 9
```

---

**Question:** Show a static abstract interface member (C# 11) and a generic method that uses it polymorphically.

**Answer:**
```csharp
interface IHasZero<T>
{
    static abstract T Zero { get; } // required at the TYPE level, no instance needed
}

struct Meters : IHasZero<Meters>
{
    public double Value;
    public static Meters Zero => new Meters { Value = 0 }; // this type's own "zero"
}

T Sum<T>(T[] items) where T : IHasZero<T>
{
    // T.Zero calls the static member polymorphically per type parameter —
    // no instance of T exists yet at this point.
    T result = T.Zero;
    // ... accumulate items into result ...
    return result;
}
```

---

**Question:** Show explicit interface implementation and how it changes what's visible polymorphically.

**Answer:**
```csharp
interface IEnglishGreeter { string Greet(); }
interface IFrenchGreeter { string Greet(); }

class Bilingual : IEnglishGreeter, IFrenchGreeter
{
    // Both interfaces declare Greet() — explicit implementation avoids collision.
    string IEnglishGreeter.Greet() => "Hello";
    string IFrenchGreeter.Greet() => "Bonjour";
}

var b = new Bilingual();
// b.Greet(); // ERROR — not visible on Bilingual's own public surface
Console.WriteLine(((IEnglishGreeter)b).Greet()); // "Hello" — only reachable via the interface
Console.WriteLine(((IFrenchGreeter)b).Greet());  // "Bonjour"
```

---

**Question:** Show the constructor-calls-virtual-member pitfall.

**Answer:**
```csharp
class Base
{
    public Base()
    {
        PrintName(); // BUG-PRONE: calls a virtual member during construction
    }
    protected virtual void PrintName() => Console.WriteLine("Base");
}

class Derived : Base
{
    private readonly string _name = "Derived"; // initialized AFTER Base's constructor runs
    protected override void PrintName() => Console.WriteLine(_name);
}

new Derived();
// Prints "" (empty), not "Derived" — Base's constructor called the
// override before Derived's own field initializer had run.
```

---

**Question:** Show a generic method constrained to an interface, used polymorphically across implementations.

**Answer:**
```csharp
interface IValidator<T> { bool IsValid(T item); }

class PositiveNumberValidator : IValidator<int>
{
    public bool IsValid(int item) => item > 0;
}

class NonEmptyStringValidator : IValidator<string>
{
    public bool IsValid(string item) => !string.IsNullOrEmpty(item);
}

// Works for ANY T and ANY IValidator<T> — the actual validation logic
// is dispatched polymorphically to whichever validator is passed in.
IEnumerable<T> FilterValid<T>(IEnumerable<T> items, IValidator<T> validator) =>
    items.Where(validator.IsValid);

FilterValid(new[] { -1, 2, -3, 4 }, new PositiveNumberValidator()); // [2, 4]
```

---

**Question:** Show overriding `Equals`, `GetHashCode`, and `ToString` — the everyday polymorphism example.

**Answer:**
```csharp
class Point
{
    public int X, Y;

    public override bool Equals(object? obj) =>
        obj is Point other && X == other.X && Y == other.Y; // value equality, not reference equality

    public override int GetHashCode() => HashCode.Combine(X, Y); // must agree with Equals

    public override string ToString() => $"({X}, {Y})"; // used by Console.WriteLine, debuggers, etc.
}

var set = new HashSet<Point> { new Point { X = 1, Y = 2 } };
set.Contains(new Point { X = 1, Y = 2 }); // true — HashSet relies on the overridden Equals/GetHashCode
```

---

**Question:** Show the Strategy pattern implemented via polymorphism.

**Answer:**
```csharp
interface IDiscountStrategy
{
    decimal Apply(decimal price);
}

class NoDiscount : IDiscountStrategy
{
    public decimal Apply(decimal price) => price;
}

class TenPercentOff : IDiscountStrategy
{
    public decimal Apply(decimal price) => price * 0.9m;
}

class Checkout
{
    private readonly IDiscountStrategy _strategy;
    public Checkout(IDiscountStrategy strategy) => _strategy = strategy; // injected, swappable

    public decimal FinalPrice(decimal price) => _strategy.Apply(price); // never changes
}

new Checkout(new TenPercentOff()).FinalPrice(100); // 90 — behavior swapped via constructor, not code changes
```

---

**Question:** Show inheritance and polymorphism with `record class`, including a `with`-expression.

**Answer:**
```csharp
abstract record Shape
{
    public abstract double Area();
}

record Circle(double Radius) : Shape
{
    public override double Area() => Math.PI * Radius * Radius; // record still supports override
}

Shape s = new Circle(2);
Console.WriteLine(s.Area()); // dispatches to Circle's override, same as a class would

var bigger = ((Circle)s) with { Radius = 4 }; // value-based copy — a class can't do this for free
```

---

**Question:** Show why `record struct` can't participate in inheritance-based polymorphism, and what alternative it has.

**Answer:**
```csharp
// record struct Shape { }          // struct — CANNOT be a base for inheritance
// record struct Circle : Shape { } // ERROR — structs don't support inheritance at all

interface IShape { double Area(); } // the only polymorphism route left for a struct

record struct Circle(double Radius) : IShape
{
    public double Area() => Math.PI * Radius * Radius; // interface implementation, not inheritance
}
```

---

**Question:** Write a polymorphic factory method that returns different concrete types behind a common interface.

**Answer:**
```csharp
interface INotifier
{
    void Send(string message);
}

class EmailNotifier : INotifier
{
    public void Send(string message) => Console.WriteLine($"Email: {message}");
}

class SmsNotifier : INotifier
{
    public void Send(string message) => Console.WriteLine($"SMS: {message}");
}

INotifier CreateNotifier(string channel) => channel switch
{
    "email" => new EmailNotifier(),
    "sms" => new SmsNotifier(),
    _ => throw new ArgumentException("Unknown channel")
};

// Caller only ever interacts with INotifier — never knows the concrete type.
INotifier notifier = CreateNotifier("sms");
notifier.Send("Your order shipped"); // "SMS: Your order shipped"
```

---

**Question:** Show `is` pattern matching used to safely downcast within a polymorphic hierarchy.

**Answer:**
```csharp
void Describe(Animal animal)
{
    // Handle a subtype-specific case without losing polymorphism for the general case.
    if (animal is Dog dog)
    {
        Console.WriteLine($"A dog that says {dog.Speak()}");
    }
    else
    {
        Console.WriteLine($"Some animal that says {animal.Speak()}");
    }
}
```

---

**Question:** Show multiple interface implementation resolving without a diamond-problem conflict.

**Answer:**
```csharp
interface IFlyer { string Move() => "Flying"; }   // default via C# 8+ default interface methods
interface ISwimmer { string Move() => "Swimming"; }

class Duck : IFlyer, ISwimmer
{
    // Must resolve the Move() collision explicitly — C# won't guess for you.
    public string Move() => $"{((IFlyer)this).Move()} and {((ISwimmer)this).Move()}";
}

new Duck().Move(); // "Flying and Swimming"
```

---

**Question:** Show a generic repository interface implemented polymorphically for two different entity types.

**Answer:**
```csharp
interface IRepository<T>
{
    T? FindById(int id);
}

class InMemoryUserRepository : IRepository<string>
{
    private readonly Dictionary<int, string> _users = new() { [1] = "Alice" };
    public string? FindById(int id) => _users.GetValueOrDefault(id);
}

class InMemoryOrderRepository : IRepository<decimal>
{
    private readonly Dictionary<int, decimal> _orders = new() { [1] = 99.99m };
    public decimal FindById(int id) => _orders.GetValueOrDefault(id);
}

// Same interface shape, completely different underlying data and type —
// calling code depending only on IRepository<T> doesn't care which.
void PrintFound<T>(IRepository<T> repo, int id) => Console.WriteLine(repo.FindById(id));
```
