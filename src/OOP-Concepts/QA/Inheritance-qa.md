# Inheritance — Interview Questions (C#)

55 questions: 30 non-coding, 25 coding. All code examples are C#.

---

## Non-Coding Questions

**Question:** What is inheritance, in one sentence?

**Answer:**
Inheritance lets one class (the derived/subclass) acquire the members and behavior of another class (the base/superclass), modeling an "is-a" relationship — a `Dog` **is an** `Animal`.

---

**Question:** What is the "is-a" relationship, and why does it matter for deciding whether to use inheritance?

**Answer:**
If you can honestly say "a `Derived` **is a** `Base`" and every `Base` capability genuinely makes sense for `Derived` too, inheritance is a reasonable fit. If the relationship is really "has a" or "uses a" (e.g. "a `Car` has an `Engine`," not "a `Car` is an `Engine`"), composition is the correct tool instead.

---

**Question:** Does C# support multiple inheritance of classes?

**Answer:**
No — a class can inherit from exactly one base class. C# avoids this deliberately to sidestep the diamond problem (ambiguity when two base classes define conflicting members). Multiple *interface* implementation is allowed instead.

---

**Question:** What is the diamond problem, and how does C# avoid it for classes?

**Answer:**
If class `D` inherited from both `B` and `C`, and both `B` and `C` inherited from `A` and each overrode the same method differently, it would be ambiguous which version `D` should get. C# sidesteps this entirely by disallowing multiple class inheritance — there's only ever one base class chain.

---

**Question:** How do interfaces give C# something like "multiple inheritance," and how is it different from multiple class inheritance?

**Answer:**
A class can implement many interfaces, inheriting many separate *contracts*. Before C# 8, interfaces carried no implementation at all, so there was no state/logic to conflict over. Default interface methods (C# 8+) reintroduce a milder version of the diamond problem, but C# forces you to resolve any collision explicitly rather than guessing.

---

**Question:** What does constructor chaining via `base(...)` do, and why is it necessary?

**Answer:**
It explicitly calls a specific base class constructor before the derived class's own constructor body runs. It's necessary because a derived object's base portion must be fully constructed first — you can't have a valid `Dog` without first having a valid `Animal` underneath it.

---

**Question:** Are private members inherited?

**Answer:**
Not accessibly — a derived class *has* the base class's private fields as part of its object layout (they still exist and take up memory), but the derived class's own code cannot see or reference them directly. Only the base class's own methods can touch them.

---

**Question:** Are constructors themselves inherited?

**Answer:**
No — a derived class does not automatically get the base class's constructors as its own. It must define its own constructors (even a default one, implicitly), which then chain to a base constructor via `base(...)` (or implicitly to the base's parameterless constructor if one exists).

---

**Question:** What's the difference between inheritance and composition?

**Answer:**
Inheritance reuses behavior via an "is-a" relationship, baked in at compile time, exposing (or at least assuming) the base's entire contract. Composition reuses behavior via a "has-a" relationship — one object holds a reference to another and delegates to it selectively, which is more flexible and can be changed at runtime.

---

**Question:** Why is "favor composition over inheritance" a common guideline?

**Answer:**
Inheritance creates a tight, compile-time-fixed coupling to the base class's implementation (the Fragile Base Class problem) and forces the entire base contract onto every subclass, even parts that don't make sense for it. Composition lets you pick exactly which behaviors to reuse and swap collaborators without rearranging a class hierarchy.

---

**Question:** What is the Fragile Base Class problem?

**Answer:**
A seemingly safe change to a base class (e.g. changing how one method calls another internally) can silently break derived classes that overrode one of those methods and relied on the old calling behavior — the derived class is "fragile" to changes in a base class it doesn't control and often can't even see the source of.

---

**Question:** What's the downside of very deep inheritance hierarchies?

**Answer:**
Understanding any one class means mentally tracing behavior across every level above it — a bug or a design decision made 4 levels up can be hard to find. Deep hierarchies also make the Fragile Base Class problem worse, since more subclasses potentially depend on more base-class assumptions.

---

**Question:** Can you inherit from a `sealed` class?

**Answer:**
No — `sealed` on a class explicitly forbids any further inheritance from it. It's often applied to a class the author considers "finished" and doesn't want extended, sometimes also for a minor JIT performance benefit (no virtual dispatch needed for calls known to be non-overridable).

---

**Question:** What is the `protected` access modifier for, specifically in the context of inheritance?

**Answer:**
It exposes a member to the class itself *and* any derived class, but nobody else — it exists precisely to let a base class share implementation details with subclasses (for extension/customization) without making those details fully `public` to unrelated code.

---

**Question:** What happens to static members when a class is inherited?

**Answer:**
Static members belong to the *type*, not to instances, and are shared across the whole hierarchy — `Base.Counter` and `Derived.Counter` (if not explicitly hidden) refer to the exact same underlying storage. Static members also cannot be truly polymorphic (no virtual dispatch applies to them).

---

**Question:** Can you override a `static` method?

**Answer:**
No — `override` only applies to instance members participating in virtual dispatch. You can declare a same-named `static` method in a derived class, but that's method **hiding**, resolved entirely at compile time based on which type name you call it through.

---

**Question:** What's the difference between an abstract base class and a "concrete" (fully instantiable) base class, from an inheritance-design standpoint?

**Answer:**
An abstract base class explicitly says "I'm incomplete — you must provide the missing pieces," and can't be instantiated on its own. A concrete base class is a fully valid, usable object by itself; subclassing it is *optional* customization, not a requirement to make it usable.

---

**Question:** Can a class both inherit from a base class AND implement one or more interfaces?

**Answer:**
Yes — `class Dog : Animal, IFlyer, ISwimmer` is legal. A class has exactly one base class (or none, implicitly `object`) but can implement any number of interfaces alongside it.

---

**Question:** What is `private protected` (C# 7.2+), and how does it differ from `protected internal`?

**Answer:**
`private protected` = accessible to derived classes, but **only within the same assembly** (the intersection of `private` and `protected`). `protected internal` = accessible to derived classes **anywhere**, OR anywhere within the same assembly (the union of `protected` and `internal`). They're opposite combinations of the same two modifiers.

---

**Question:** How does inheritance relate to the Liskov Substitution Principle?

**Answer:**
Inheritance gives you the *mechanism* to substitute a subclass wherever the base class is expected (compile-time-checked). LSP is the *design rule* that this substitution should also be *behaviorally* safe — a subclass shouldn't violate expectations the base class established (e.g. throwing where the base wouldn't, or returning something outside the base's documented range).

---

**Question:** What is the classic Square/Rectangle example of an LSP violation via inheritance?

**Answer:**
If `Square` inherits from `Rectangle` and overrides `Width`/`Height` setters to always keep both equal (since a square's sides must match), then code that sets a `Rectangle`'s width and height independently — perfectly valid for any *real* `Rectangle` — breaks in surprising ways when the actual object is a `Square`. The "is-a" relationship is geometrically true but behaviorally unsafe.

---

**Question:** Does C# support mixins? If not natively, what's the closest workaround?

**Answer:**
Not natively as a first-class language feature. The closest workarounds are **default interface methods** (C# 8+, giving multiple interfaces shared default behavior) and **extension methods** (adding methods to a type from outside, without inheritance at all) — neither is a true mixin, but both let you compose reusable behavior without a rigid single-base-class hierarchy.

---

**Question:** Why would an abstract class have a constructor if it can never be instantiated directly?

**Answer:**
So it can enforce setup/validation shared by *every* concrete subclass — the constructor still runs (via constructor chaining) whenever any derived class is instantiated, it's just that you can never write `new AbstractClass()` yourself.

---

**Question:** What's the semantic difference between a class "extends" relationship and an interface "implements" relationship?

**Answer:**
"Extends" implies shared identity and (often) shared implementation/state — a genuine specialization of what the base already is. "Implements" implies only a shared *capability contract* — no assumption of shared ancestry, state, or behavior beyond "can do X."

---

**Question:** What is a real-world risk of inheritance-based code reuse, beyond the Fragile Base Class problem?

**Answer:**
It couples the subclass to the base class's *internal* implementation choices, not just its public contract — subclasses that override protected members or call `base.Method()` are effectively depending on implementation details that the base class author may consider free to change.

---

**Question:** What is the Composite Reuse Principle?

**Answer:**
Another name for "favor composition over inheritance for code reuse" — reuse should be achieved by *composing* objects (delegating to them) rather than by *inheriting* from a class purely to reuse its implementation, especially when there's no genuine "is-a" relationship.

---

**Question:** What's a practical rule of thumb for choosing inheritance versus composition?

**Answer:**
Ask: "does every capability of the base class genuinely make sense for this subclass, unconditionally?" If yes, and the relationship is a real "is-a," inheritance is reasonable. If you find yourself overriding methods to throw `NotSupportedException` or to do nothing, that's a strong signal the relationship isn't really "is-a," and composition (or an interface) is the better fit.

---

**Question:** How does multi-level inheritance (A → B → C) affect constructor execution order?

**Answer:**
Base constructors always run **before** derived constructors, all the way up the chain — so for `C : B : A`, the order is `A`'s constructor, then `B`'s, then `C`'s, each completing before the next begins its own body (after its `base(...)` call resolves).

---

**Question:** Why might you choose an abstract class over an interface specifically when you *do* want to share implementation across subclasses?

**Answer:**
An abstract class can hold shared fields, provide default method implementations, and enforce that certain construction/setup logic always runs — an interface (pre-C#-8) offered none of that. Even with default interface methods now available, an abstract class remains the more natural fit when the relationship is genuinely "is-a" *and* there's meaningful shared state.

---

## Coding Questions

**Question:** Show a basic single inheritance example.

**Answer:**
```csharp
class Animal
{
    public string Name { get; }
    public Animal(string name) => Name = name;

    public virtual string Describe() => $"{Name} is an animal";
}

class Dog : Animal
{
    public Dog(string name) : base(name) { } // reuses Animal's constructor logic

    public override string Describe() => $"{Name} is a dog"; // specializes the shared contract
}
```

---

**Question:** Show constructor chaining across a multi-level hierarchy (A → B → C).

**Answer:**
```csharp
class A
{
    public A() => Console.WriteLine("A constructed");
}
class B : A
{
    public B() : base() => Console.WriteLine("B constructed");
}
class C : B
{
    public C() : base() => Console.WriteLine("C constructed");
}

new C();
// Output, in order: "A constructed", "B constructed", "C constructed"
```

---

**Question:** Show a `protected` member accessed by a derived class but not from outside.

**Answer:**
```csharp
class Vehicle
{
    protected int _fuelLiters = 50; // visible to subclasses, not to external callers

    public virtual string Status() => $"{_fuelLiters}L remaining";
}

class Car : Vehicle
{
    public void Refuel(int liters) => _fuelLiters += liters; // legal — Car IS a Vehicle
}

// var v = new Vehicle();
// v._fuelLiters = 10; // ERROR from outside the hierarchy — _fuelLiters is protected, not public
```

---

**Question:** Show why multiple class inheritance doesn't compile, and the interface-based fix.

**Answer:**
```csharp
class Bird { }
class Fish { }

// class Penguin : Bird, Fish { } // ERROR — a class can have only ONE base class

interface ICanSwim { void Swim(); }
interface ICanWalk { void Walk(); }

class Penguin : Bird, ICanSwim, ICanWalk // one base class + any number of interfaces
{
    public void Swim() => Console.WriteLine("Swimming");
    public void Walk() => Console.WriteLine("Walking");
}
```

---

**Question:** Show `sealed` preventing further inheritance from a class.

**Answer:**
```csharp
sealed class FinalPolicy
{
    public virtual string Describe() => "Final policy"; // still virtual, just can't be subclassed further
}

// class CustomPolicy : FinalPolicy { } // ERROR — FinalPolicy is sealed
```

---

**Question:** Show that static members are shared, not overridden, across a hierarchy.

**Answer:**
```csharp
class Counter
{
    public static int Instances = 0;
    public Counter() => Instances++;
}

class SpecialCounter : Counter { }

_ = new Counter();
_ = new SpecialCounter();
_ = new SpecialCounter();

Console.WriteLine(Counter.Instances); // 3 — ONE shared counter, not per-subtype
```

---

**Question:** Bug hunt — spot the confusing (but legal) static member hiding.

**Answer:**
```csharp
class Base
{
    public static string Label = "Base label";
}

class Derived : Base
{
    // Legal — hides Base.Label with a SEPARATE static field, doesn't override it
    // (static members can't be virtual/overridden at all).
    public new static string Label = "Derived label";
}

Console.WriteLine(Base.Label);    // "Base label"
Console.WriteLine(Derived.Label); // "Derived label" — a totally different field, same name
```

---

**Question:** Show refactoring "is-a" inheritance abuse into composition.

**Answer:**
```csharp
// Before: awkward — a Car "is an" Engine doesn't make real-world sense.
class EngineBroken { public void Start() => Console.WriteLine("Engine started"); }
class CarBroken : EngineBroken { } // Car inherits Start() only because Engine happened to have it

// After: Car HAS an Engine — composition models reality correctly.
class Engine { public void Start() => Console.WriteLine("Engine started"); }
class Car
{
    private readonly Engine _engine = new();
    public void Start() => _engine.Start(); // delegates, doesn't inherit
}
```

---

**Question:** Show an abstract base class whose constructor performs shared setup for every subclass.

**Answer:**
```csharp
abstract class Report
{
    protected DateTime GeneratedAt { get; }

    protected Report()
    {
        // Runs for EVERY concrete subclass automatically, via constructor chaining.
        GeneratedAt = DateTime.UtcNow;
    }

    public abstract string Render();
}

class SalesReport : Report
{
    public override string Render() => $"Sales report generated at {GeneratedAt}";
}
```

---

**Question:** Show a class that both inherits from a base class and implements an interface.

**Answer:**
```csharp
abstract class Employee
{
    public string Name { get; }
    protected Employee(string name) => Name = name;
    public abstract decimal MonthlySalary();
}

interface ITaxable
{
    decimal TaxOwed();
}

class Contractor : Employee, ITaxable
{
    public Contractor(string name) : base(name) { }
    public override decimal MonthlySalary() => 5000m;
    public decimal TaxOwed() => MonthlySalary() * 0.15m; // combines both contracts freely
}
```

---

**Question:** Show `private protected` versus `protected internal` in practice.

**Answer:**
```csharp
// Imagine both classes live in the SAME assembly for this example.
class Base
{
    private protected int OnlySameAssemblyDerived = 1;   // derived class must be in this assembly too
    protected internal int AnyAssemblyOrDerived = 2;      // derived anywhere, OR same-assembly non-derived
}

class Derived : Base
{
    void Access()
    {
        var a = OnlySameAssemblyDerived; // OK — Derived is in the same assembly
        var b = AnyAssemblyOrDerived;    // OK
    }
}
```

---

**Question:** Show the Fragile Base Class problem concretely.

**Answer:**
```csharp
class Base
{
    public virtual void Save()
    {
        Validate();
        Console.WriteLine("Saved");
    }
    protected virtual void Validate() => Console.WriteLine("Base validation");
}

class Derived : Base
{
    private bool _initialized = true;
    protected override void Validate()
    {
        // Assumes Save() ALWAYS calls Validate() — if Base.Save() is later refactored
        // to skip Validate() under some condition, this silently stops running.
        if (_initialized) Console.WriteLine("Derived validation");
    }
}
```

---

**Question:** Show a default interface method acting as a lightweight mixin-style workaround.

**Answer:**
```csharp
interface ILoggable
{
    string LogTag { get; }
    void Log(string message) => Console.WriteLine($"[{LogTag}] {message}"); // shared default behavior
}

class OrderService : ILoggable
{
    public string LogTag => "OrderService"; // supplies just the customization point
}

((ILoggable)new OrderService()).Log("Order placed"); // "[OrderService] Order placed"
```

---

**Question:** Show downcasting a base-typed reference back to a derived type safely.

**Answer:**
```csharp
class Animal { }
class Dog : Animal { public void Fetch() => Console.WriteLine("Fetching!"); }

void Handle(Animal animal)
{
    if (animal is Dog dog) // safe pattern-matching downcast, no exception risk
    {
        dog.Fetch();
    }
}

Handle(new Dog());   // "Fetching!"
Handle(new Animal()); // does nothing — safely skipped, no crash
```

---

**Question:** Demonstrate that private members are not accessible from a derived class.

**Answer:**
```csharp
class Base
{
    private int _secret = 42; // exists in memory for Derived instances too, but invisible to Derived's code
    protected int Exposed = 7;
}

class Derived : Base
{
    void TryAccess()
    {
        // var s = _secret; // ERROR — _secret is private to Base, not visible here
        var e = Exposed; // OK — protected members ARE visible
    }
}
```

---

**Question:** Reproduce the classic Square/Rectangle LSP violation via inheritance.

**Answer:**
```csharp
class Rectangle
{
    public virtual double Width { get; set; }
    public virtual double Height { get; set; }
    public double Area() => Width * Height;
}

class Square : Rectangle
{
    public override double Width
    {
        get => base.Width;
        set { base.Width = value; base.Height = value; } // forces both to match
    }
    public override double Height
    {
        get => base.Height;
        set { base.Width = value; base.Height = value; }
    }
}

void Resize(Rectangle r)
{
    r.Width = 4;
    r.Height = 5;
    Console.WriteLine(r.Area()); // expected 20 for ANY Rectangle...
}

Resize(new Rectangle()); // 20, as expected
Resize(new Square());    // 25 — surprising! Height=5 silently overwrote Width too
```

---

**Question:** Fix the Square/Rectangle violation using composition/interface segregation instead of inheritance.

**Answer:**
```csharp
interface IShape { double Area(); }

class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Area() => Width * Height;
}

class Square : IShape // no longer "is-a" Rectangle — sibling types, not a hierarchy
{
    public double Side { get; set; }
    public double Area() => Side * Side;
}
// No shared setter logic to secretly interact with each other — each type owns its own invariant.
```

---

**Question:** Show inheriting from a generic base class.

**Answer:**
```csharp
abstract class Repository<T>
{
    protected readonly List<T> Items = new();
    public virtual void Add(T item) => Items.Add(item);
    public abstract T? FindFirst();
}

class UserRepository : Repository<string> // "closes" the generic with a concrete type
{
    public override string? FindFirst() => Items.FirstOrDefault();
}
```

---

**Question:** Show a derived class forced to supply required constructor parameters via `base(...)`.

**Answer:**
```csharp
class Shape
{
    public string Color { get; }
    public Shape(string color) => Color = color; // no parameterless constructor exists
}

class Circle : Shape
{
    public double Radius { get; }

    // MUST pass a color to base(...) — there's no other way to satisfy Shape's constructor.
    public Circle(string color, double radius) : base(color) => Radius = radius;
}
```

---

**Question:** Show a real-world-style base repository class extended per entity type.

**Answer:**
```csharp
abstract class BaseRepository<TEntity, TId>
{
    protected readonly Dictionary<TId, TEntity> Store = new();

    public virtual TEntity? FindById(TId id) => Store.TryGetValue(id, out var e) ? e : default;
    public virtual void Save(TId id, TEntity entity) => Store[id] = entity;
}

class ProductRepository : BaseRepository<Product, int>
{
    // Adds Product-specific behavior on top of the shared CRUD-ish base.
    public IEnumerable<Product> FindLowStock() => Store.Values.Where(p => p.Stock < 5);
}

class Product { public int Stock; }
```

---

**Question:** Show overriding a virtual method AND extending (not replacing) it via `base`.

**Answer:**
```csharp
class Vehicle
{
    public virtual string Describe() => "A vehicle";
}

class ElectricCar : Vehicle
{
    public override string Describe() => $"{base.Describe()} that runs on electricity";
    // Extends the base's description instead of throwing it away entirely.
}

new ElectricCar().Describe(); // "A vehicle that runs on electricity"
```

---

**Question:** Show a hierarchy where a subclass legitimately overrides a method to add a precondition, without violating LSP.

**Answer:**
```csharp
class Account
{
    public virtual void Withdraw(decimal amount)
    {
        Console.WriteLine($"Withdrew {amount}");
    }
}

class OverdraftProtectedAccount : Account
{
    private decimal _balance = 100;

    public override void Withdraw(decimal amount)
    {
        // Adds a STRICTER precondition, but never does LESS than the base promises
        // when the precondition passes — this stays LSP-safe.
        if (amount > _balance) throw new InvalidOperationException("Insufficient funds");
        base.Withdraw(amount);
        _balance -= amount;
    }
}
```

---

**Question:** Show why a derived class overriding a method to do nothing (or throw `NotSupportedException`) is a smell.

**Answer:**
```csharp
class Bird
{
    public virtual void Fly() => Console.WriteLine("Flying");
}

class Penguin : Bird
{
    // SMELL: Penguin "is a" Bird by name, but breaks the base contract entirely —
    // a strong sign the hierarchy itself is wrong, not just this override.
    public override void Fly() => throw new NotSupportedException("Penguins can't fly");
}
```
**Better design:** split flight into its own interface so only flying birds implement it:
```csharp
interface IFlyingBird { void Fly(); }
class Sparrow : Bird, IFlyingBird { public void Fly() => Console.WriteLine("Flying"); }
class Penguin : Bird { } // simply doesn't implement IFlyingBird — no broken promise
```

---

**Question:** Show inheritance combined with an interface to implement `IDisposable`-style resource cleanup consistently across a hierarchy.

**Answer:**
```csharp
abstract class Resource : IDisposable
{
    private bool _disposed;

    public void Dispose()
    {
        if (_disposed) return;
        ReleaseResources(); // each subclass defines its own cleanup
        _disposed = true;
    }

    protected abstract void ReleaseResources();
}

class FileResource : Resource
{
    protected override void ReleaseResources() => Console.WriteLine("File handle closed");
}
```
 