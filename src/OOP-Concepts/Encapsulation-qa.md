# Encapsulation — Interview Questions (C#)

55 questions: 30 non-coding, 25 coding. All code examples are C#.

---

## Non-Coding Questions

**Question:** What is encapsulation, in one sentence?

**Answer:**
Encapsulation is bundling an object's data with the methods that operate on it, while restricting direct access to that data from outside — the object controls how and when its own state can change.

---

**Question:** Why does encapsulation matter?

**Answer:**
It protects an object's **invariants** (rules that must always hold, e.g. "balance can never go negative"). If external code can freely mutate fields, nothing can guarantee those rules stay true. Encapsulation forces all state changes through code that can validate them.

---

**Question:** What are the C# access modifiers, from most to least restrictive?

**Answer:**
`private` (this class only) → `private protected` (this class + derived classes in the same assembly) → `protected` (this class + any derived class) → `internal` (anywhere in the same assembly) → `protected internal` (derived classes anywhere, or anywhere in the same assembly) → `public` (everywhere).

---

**Question:** Why prefer properties over public fields in C#?

**Answer:**
A field exposes raw storage — any assignment bypasses validation entirely. A property is a pair of methods (`get`/`set`) that *look* like field access but let you validate, compute, log, or restrict on every read/write — and you can change the internal representation later without breaking callers' syntax.

---

**Question:** What is an auto-implemented property, and when do you need a full property instead?

**Answer:**
`public string Name { get; set; }` — the compiler generates a hidden backing field for you. You need a full property (explicit backing field) as soon as you need logic in the getter/setter — validation, computed values, or side effects.

---

**Question:** What's the difference between `{ get; private set; }` and `{ get; init; }`?

**Answer:**
`private set` allows mutation from **any** method inside the class, at any time. `init` (C# 9+) allows the value to be set **only** during object construction (constructor or object initializer) — after that, it's permanently read-only, even from inside the class itself. `init` expresses immutability more strongly.

---

**Question:** How does encapsulation relate to immutability?

**Answer:**
Immutability is encapsulation taken to its logical extreme: once state can only be set at construction and never changed again, there's no mutation path to protect — the class doesn't need to defend its invariants after the fact, because they're guaranteed permanently the moment construction succeeds.

---

**Question:** Are encapsulation and information hiding the same thing?

**Answer:**
They're closely related but not identical. **Information hiding** is the broader design principle: hide implementation details behind a stable interface so callers don't depend on internals. **Encapsulation** is the language mechanism (access modifiers, properties) that C#/Java/etc. use to *achieve* information hiding.

---

**Question:** What's the classic confusion between encapsulation and abstraction?

**Answer:**
Abstraction is about **what** an object does (its interface/contract) — hiding complexity of *design*. Encapsulation is about **how** its data is protected — hiding complexity of *implementation/state*. A `private` field with validated setters is encapsulation; an `interface IShape { double Area(); }` hiding "how area is computed" is abstraction. They're often used together but answer different questions.

---

**Question:** Can encapsulation be bypassed via reflection?

**Answer:**
Yes — .NET reflection can read/write `private` fields and invoke `private` methods, bypassing access modifiers entirely. Encapsulation is a compile-time discipline enforced by the compiler for *normal* code paths, not a runtime security boundary.

---

**Question:** What is "Tell, Don't Ask," and how does encapsulation support it?

**Answer:**
Instead of *asking* an object for its internal data and then deciding what to do with it externally, you *tell* the object what you want done and let it use its own (possibly private) state to do it. This keeps behavior next to the data it operates on — exactly what encapsulation is meant to enable.

---

**Question:** What's the risk of exposing a mutable collection (e.g. `public List<T> Items`) directly from a class?

**Answer:**
Any caller can add, remove, or clear the list without the owning class ever knowing — completely bypassing any invariant the class might want to enforce (e.g. "list can never be empty," or "adding an item should also update a total"). The property looks encapsulated but isn't, because the *referenced object* is still fully mutable from outside.

---

**Question:** What is a defensive copy, and why does encapsulation sometimes require one?

**Answer:**
Returning a **copy** of internal mutable state (or an `IReadOnlyList<T>` wrapper) instead of the original reference, so callers can look at the data but can't mutate the class's actual internal collection. Without it, a getter that "just returns the list" silently leaks full write access.

---

**Question:** How does encapsulation support unit testing?

**Answer:**
By exposing behavior through a small, well-defined public surface (often an interface), tests can exercise observable behavior without needing to know or manipulate internal state directly — and implementation details can be refactored freely as long as the public contract's behavior stays the same.

---

**Question:** What is an anemic domain model, and how does it relate to weak encapsulation?

**Answer:**
An anemic domain model is a class that's mostly public getters/setters with no real behavior — all the logic lives in external "service" classes that manipulate the object's data from outside. This is a symptom of weak encapsulation: the object never protects its own invariants because nothing is actually private or validated.

---

**Question:** What's a downside of *over*-encapsulation?

**Answer:**
Wrapping every trivial field behind a getter/setter pair with no real validation or logic adds ceremony without benefit — it can make a class harder to read for no protective gain. Encapsulation should guard something real (an invariant, a computed value, a side effect), not exist reflexively.

---

**Question:** What's the difference between validating in a constructor versus validating in a property setter?

**Answer:**
Constructor validation guarantees the object is **never** in an invalid state, even for a moment — useful for immutable objects. Setter validation guards **every subsequent mutation** too, which matters for mutable objects whose state can legitimately change after construction.

---

**Question:** How does encapsulation relate to thread safety?

**Answer:**
If all mutation of an object's state happens through a small set of methods/properties, that's exactly where you'd add locking or other synchronization — a class with no encapsulation (raw public fields mutated from everywhere) has no single place to make thread-safe.

---

**Question:** How does exposing only an interface (rather than the concrete class) support encapsulation at a larger scale?

**Answer:**
Callers depending on `IRepository` instead of `SqlRepository` never see (and can't depend on) implementation-specific public members that leaked onto the concrete class for internal reasons — the interface is a second, tighter encapsulation boundary layered on top of the class's own access modifiers.

---

**Question:** What is the Law of Demeter, and how does it connect to encapsulation?

**Answer:**
Roughly: "talk to your immediate friends, not their friends" — avoid chains like `a.GetB().GetC().DoSomething()`. Long chains mean you depend on the internal structure of objects several hops away, defeating their encapsulation. Well-encapsulated objects expose behavior that avoids forcing callers into these chains.

---

**Question:** Does a `record`'s default immutability strengthen encapsulation compared to a class?

**Answer:**
Yes, by default — `record` properties are commonly declared with `init` accessors (via positional records), so once constructed, external code has no way to mutate them at all, even through otherwise-legitimate property syntax. It doesn't replace encapsulation for behavior, but it removes an entire category of accidental-mutation bugs.

---

**Question:** What is a backing field, and why might you need one explicitly instead of an auto-property?

**Answer:**
The private field that actually stores a property's value. You need one explicitly when the getter/setter needs logic — e.g. lazy initialization, validation, or transforming the stored value — that an auto-property's compiler-generated field can't support.

---

**Question:** What's the encapsulation benefit of a private nested class?

**Answer:**
It lets you model an implementation detail (e.g. a linked-list node, an internal cache entry) as a real class with its own behavior, while guaranteeing no code outside the containing class can ever reference that type directly — the detail is fully hidden, not just "discouraged from use."

---

**Question:** What does `internal` buy you that `public` doesn't, for encapsulation across a whole assembly?

**Answer:**
It lets types/members be shared freely between classes within the *same* assembly (e.g. implementation helpers used by several internal services) while remaining completely invisible to consumers of the compiled library — a coarser encapsulation boundary than per-class access modifiers.

---

**Question:** Why is a static factory method with a private constructor a form of encapsulation?

**Answer:**
It hides *how* an object gets constructed (validation, choosing a concrete subtype, caching/pooling instances) behind a method name, while preventing callers from bypassing that logic via `new` directly — construction itself becomes an encapsulated operation, not just the object's post-construction state.

---

**Question:** What's a practical example of encapsulation making a class easier to refactor later?

**Answer:**
If a `Temperature` class stores Celsius internally as a `private double _celsius` behind a `public double Fahrenheit` property, you can later change the internal storage to Fahrenheit (or add caching, or add validation) without any caller code changing at all — callers only ever depended on the property, never the field.

---

**Question:** What is a guard clause, and how does it support encapsulation?

**Answer:**
An early validation check (often at the top of a constructor or setter) that rejects invalid input immediately, e.g. `if (amount < 0) throw new ArgumentException(...)`. It's the actual enforcement mechanism behind an encapsulated invariant — without it, "private + property" alone doesn't guarantee correctness, just controlled access.

---

**Question:** How does encapsulation interact with dependency injection?

**Answer:**
A class exposes its dependencies only through its constructor (or a narrow set of setters), not by letting callers reach in and swap arbitrary internal collaborators — DI containers rely on this encapsulated "construction contract" to wire dependencies correctly and consistently.

---

**Question:** What's a real-world consequence of NOT encapsulating a `Money`-like value (e.g. exposing raw `decimal` everywhere instead of a `Money` type)?

**Answer:**
Nothing stops mismatched-currency arithmetic, inconsistent rounding, or accidental unit confusion (cents vs dollars) anywhere in the codebase — every call site has to remember and re-implement the same rules, instead of one encapsulated type enforcing them once.

---

## Coding Questions

**Question:** Show a private field exposed via a validated public property.

**Answer:**
```csharp
class BankAccount
{
    private decimal _balance;

    public decimal Balance
    {
        get => _balance;
        private set // only this class can assign — callers can only read
        {
            if (value < 0) throw new InvalidOperationException("Balance cannot go negative");
            _balance = value;
        }
    }

    public void Deposit(decimal amount) => Balance += amount; // the only sanctioned mutation path
}
```

---

**Question:** Contrast an auto-property with a full property that needs real logic.

**Answer:**
```csharp
class Product
{
    // Auto-property: fine when there's truly no logic needed.
    public string Name { get; set; }

    private decimal _price;
    public decimal Price
    {
        get => _price;
        set
        {
            // Needs a real backing field the moment validation is required.
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _price = value;
        }
    }
}
```

---

**Question:** Show a read-only (get-only) property computed from other private state.

**Answer:**
```csharp
class Rectangle
{
    public double Width { get; }
    public double Height { get; }

    public Rectangle(double width, double height) => (Width, Height) = (width, height);

    // No setter at all — always derived, never independently settable
    // (so it can never disagree with Width/Height).
    public double Area => Width * Height;
}
```

---

**Question:** Show `init`-only properties (C# 9+) for construction-time-only mutability.

**Answer:**
```csharp
class Order
{
    public string CustomerName { get; init; } = string.Empty;
    public decimal Total { get; init; }
}

var order = new Order { CustomerName = "Alice", Total = 49.99m }; // allowed — object initializer
// order.Total = 10m; // ERROR — init-only, cannot mutate after construction
```

---

**Question:** Show why returning a raw mutable list breaks encapsulation, then fix it.

**Answer:**
```csharp
class ShoppingCartBroken
{
    private readonly List<string> _items = new();
    public List<string> Items => _items; // BUG: exposes the real internal list
}

var cart = new ShoppingCartBroken();
cart.Items.Clear(); // caller just wiped the cart's internal state directly

class ShoppingCartFixed
{
    private readonly List<string> _items = new();

    // Read-only view — caller can enumerate but not Add/Remove/Clear.
    public IReadOnlyList<string> Items => _items;

    public void AddItem(string item) => _items.Add(item); // the only sanctioned mutation path
}
```

---

**Question:** Show an immutable class whose entire state is set once via the constructor.

**Answer:**
```csharp
class Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y) => (X, Y) = (x, y);

    // Any "change" produces a NEW Point rather than mutating this one.
    public Point Translate(double dx, double dy) => new Point(X + dx, Y + dy);
}
```

---

**Question:** Show an indexer, and explain how it's still encapsulation even though it looks like array access.

**Answer:**
```csharp
class SparseArray
{
    private readonly Dictionary<int, double> _values = new();

    public double this[int index]
    {
        get => _values.TryGetValue(index, out var v) ? v : 0.0; // default for unset indices
        set
        {
            if (value == 0.0) _values.Remove(index); // don't store zeros — saves memory
            else _values[index] = value;
        }
    }
}

var arr = new SparseArray();
arr[1000] = 5.0;   // looks like array access, but real logic runs underneath
Console.WriteLine(arr[999]); // 0.0 — never explicitly set
```

---

**Question:** Show constructor validation enforcing an invariant that can never be violated.

**Answer:**
```csharp
class DateRange
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public DateRange(DateTime start, DateTime end)
    {
        // Once construction succeeds, Start <= End is guaranteed forever —
        // there's no setter later that could break it.
        if (end < start) throw new ArgumentException("End must not be before Start");
        Start = start;
        End = end;
    }
}
```

---

**Question:** Show a static factory method with a private constructor, encapsulating construction logic.

**Answer:**
```csharp
class Connection
{
    public string Endpoint { get; }
    private Connection(string endpoint) => Endpoint = endpoint; // callers can't `new Connection(...)`

    public static Connection CreateSecure(string host)
    {
        // Encapsulates the "how" — callers never see the endpoint-formatting rule.
        return new Connection($"https://{host}:443");
    }
}

var conn = Connection.CreateSecure("example.com"); // only entry point
```

---

**Question:** Bug hunt — what does this class fail to protect, and how do you fix it?

**Answer:**
```csharp
class Inventory
{
    public int Quantity; // BUG: public mutable field, no validation at all

    public void Remove(int amount) => Quantity -= amount; // can go negative unnoticed
}

var inv = new Inventory { Quantity = 5 };
inv.Quantity = -100; // nothing stops this
```
**Fix:**
```csharp
class Inventory
{
    private int _quantity;
    public int Quantity
    {
        get => _quantity;
        private set
        {
            if (value < 0) throw new InvalidOperationException("Quantity cannot go negative");
            _quantity = value;
        }
    }

    public void Remove(int amount) => Quantity -= amount; // now goes through validation
}
```

---

**Question:** Show a private nested class encapsulating an implementation detail.

**Answer:**
```csharp
class LinkedStack<T>
{
    // Nobody outside LinkedStack can ever reference Node — it's a pure implementation detail.
    private class Node
    {
        public T Value = default!;
        public Node? Next;
    }

    private Node? _top;

    public void Push(T value) => _top = new Node { Value = value, Next = _top };

    public T Pop()
    {
        if (_top is null) throw new InvalidOperationException("Stack is empty");
        var value = _top.Value;
        _top = _top.Next;
        return value;
    }
}
```

---

**Question:** Show `internal` restricting a helper class to the containing assembly only.

**Answer:**
```csharp
// Visible to every class inside THIS assembly, invisible to any consumer
// referencing the compiled library — a coarser encapsulation boundary.
internal class ConnectionStringParser
{
    public static (string Host, int Port) Parse(string connectionString)
    {
        var parts = connectionString.Split(':');
        return (parts[0], int.Parse(parts[1]));
    }
}
```

---

**Question:** Show a computed property with no backing field at all.

**Answer:**
```csharp
class Person
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;

    // Never stored, never out of sync — always recomputed from the source of truth.
    public string FullName => $"{FirstName} {LastName}";
}
```

---

**Question:** Show a Law of Demeter violation, then refactor it to respect encapsulation.

**Answer:**
```csharp
// Violation: reaches through Customer -> Wallet -> Balance from the outside.
class OrderProcessorBroken
{
    public bool CanAfford(Customer customer, decimal amount) =>
        customer.Wallet.Balance >= amount; // depends on Wallet's internal shape
}

// Fixed: Customer exposes behavior, hiding how affordability is actually checked.
class Customer
{
    private readonly Wallet _wallet;
    public Customer(Wallet wallet) => _wallet = wallet;

    public bool CanAfford(decimal amount) => _wallet.Balance >= amount; // Wallet detail stays inside
}

class OrderProcessorFixed
{
    public bool CanAfford(Customer customer, decimal amount) => customer.CanAfford(amount);
}
```

---

**Question:** Show a private field backing a public event, restricting who can raise it.

**Answer:**
```csharp
class Thermostat
{
    // 'event' keyword itself already restricts +=/-= to subscribe only —
    // ONLY this class can call Invoke to actually raise it.
    public event Action<double>? TemperatureChanged;

    private double _temperature;
    public double Temperature
    {
        get => _temperature;
        set
        {
            _temperature = value;
            TemperatureChanged?.Invoke(value); // only this class can trigger notifications
        }
    }
}
```

---

**Question:** Show the Builder pattern encapsulating complex, multi-step construction.

**Answer:**
```csharp
class Pizza
{
    public string Size { get; }
    public IReadOnlyList<string> Toppings { get; }

    private Pizza(string size, List<string> toppings) => (Size, Toppings) = (size, toppings);

    public class Builder
    {
        private string _size = "Medium";
        private readonly List<string> _toppings = new();

        public Builder WithSize(string size) { _size = size; return this; }
        public Builder AddTopping(string topping) { _toppings.Add(topping); return this; }

        // The ONLY way to get a Pizza — construction rules live entirely here.
        public Pizza Build() => new Pizza(_size, _toppings);
    }
}

var pizza = new Pizza.Builder().WithSize("Large").AddTopping("Cheese").Build();
```

---

**Question:** Show a `record` demonstrating encapsulation through immutability.

**Answer:**
```csharp
record Temperature(double Celsius)
{
    // Computed, not stored — no way to set an inconsistent Fahrenheit value.
    public double Fahrenheit => Celsius * 9 / 5 + 32;
}

var t = new Temperature(20);
// t.Celsius = 25; // ERROR — positional record properties are init-only by default
var warmer = t with { Celsius = 25 }; // produces a NEW instance instead
```

---

**Question:** Show a thread-safe encapsulated counter.

**Answer:**
```csharp
class Counter
{
    private int _count;
    private readonly object _lock = new();

    public int Value
    {
        get { lock (_lock) return _count; } // synchronized read
    }

    public void Increment()
    {
        lock (_lock) { _count++; } // synchronized write — the ONLY mutation path, easy to guard
    }
}
```

---

**Question:** Show an anemic domain model, then refactor it to be properly encapsulated.

**Answer:**
```csharp
// Anemic: all data, no behavior — logic lives OUTSIDE the object entirely.
class OrderAnemic
{
    public decimal Total { get; set; }
    public bool IsPaid { get; set; }
}
class OrderServiceAnemic
{
    public void MarkPaid(OrderAnemic order)
    {
        if (order.Total <= 0) throw new InvalidOperationException("Nothing to pay");
        order.IsPaid = true; // anyone could have set this directly, bypassing the rule
    }
}

// Encapsulated: the rule lives WITH the data it protects.
class Order
{
    public decimal Total { get; private set; }
    public bool IsPaid { get; private set; }

    public Order(decimal total) => Total = total;

    public void MarkPaid()
    {
        if (Total <= 0) throw new InvalidOperationException("Nothing to pay");
        IsPaid = true; // this is the ONLY way IsPaid can ever become true
    }
}
```

---

**Question:** Show exposing a narrow interface instead of a full concrete class, as a larger-scale encapsulation boundary.

**Answer:**
```csharp
interface IReadOnlyAccount
{
    decimal Balance { get; }
}

class Account : IReadOnlyAccount
{
    public decimal Balance { get; private set; }
    public void Deposit(decimal amount) => Balance += amount; // mutation stays hidden from IReadOnlyAccount consumers
}

void PrintBalance(IReadOnlyAccount account) => Console.WriteLine(account.Balance);
// PrintBalance only ever sees the read-only surface, regardless of the real object's capabilities.
```

---

**Question:** Show guard clauses enforcing an invariant in a setter.

**Answer:**
```csharp
class Percentage
{
    private double _value;
    public double Value
    {
        get => _value;
        set
        {
            // Guard clauses — the actual enforcement, not just "private + property" alone.
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException(nameof(value), "Must be between 0 and 100");
            _value = value;
        }
    }
}
```

---

**Question:** Show a `Money`-like value type encapsulating currency and amount together.

**Answer:**
```csharp
readonly struct Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency) => (Amount, Currency) = (amount, currency);

    public static Money operator +(Money a, Money b)
    {
        // Encapsulates the rule that mismatched currencies can NEVER silently add —
        // every call site gets this check for free instead of re-implementing it.
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Cannot add different currencies");
        return new Money(a.Amount + b.Amount, a.Currency);
    }
}
```

---

**Question:** Show dependency injection relying on a constructor as the sole encapsulated entry point for dependencies.

**Answer:**
```csharp
interface IEmailSender { void Send(string to, string body); }

class OrderConfirmationService
{
    private readonly IEmailSender _emailSender; // never reassignable after construction

    public OrderConfirmationService(IEmailSender emailSender) => _emailSender = emailSender;

    public void Confirm(string customerEmail) =>
        _emailSender.Send(customerEmail, "Your order is confirmed!");
}
// Callers can never swap _emailSender mid-lifecycle — the dependency contract is fixed at construction.
```

---

**Question:** Show a "before/after" comparing a public settable enum-like state field versus an encapsulated state machine.

**Answer:**
```csharp
// Before: any code can jump to any state, including invalid transitions.
class OrderBroken
{
    public string Status = "Pending"; // e.g. someone sets "Shipped" before "Paid"
}

// After: transitions are the only mutation path, and illegal ones are rejected.
class Order
{
    public string Status { get; private set; } = "Pending";

    public void Pay()
    {
        if (Status != "Pending") throw new InvalidOperationException($"Cannot pay from {Status}");
        Status = "Paid";
    }

    public void Ship()
    {
        if (Status != "Paid") throw new InvalidOperationException($"Cannot ship from {Status}");
        Status = "Shipped";
    }
}
```
 