# Abstraction — Interview Questions (C#)

55 questions: 30 non-coding, 25 coding. All code examples are C#.

---

## Non-Coding Questions

**Question:** What is abstraction, in one sentence?

**Answer:**
Abstraction means exposing only what something *does* (its essential behavior/contract) while hiding *how* it does it — callers depend on a simplified interface, not the full complexity behind it.

---

**Question:** What's the difference between abstraction and encapsulation?

**Answer:**
Abstraction hides **design complexity** — "what can I do with this?" (the contract). Encapsulation hides **implementation state** — "how is the data protected?" An `interface IShape { double Area(); }` is abstraction; a `private` field with a validated setter is encapsulation. They're complementary, not the same thing.

---

**Question:** How do abstract classes serve as an abstraction mechanism?

**Answer:**
An abstract class defines a contract (abstract members) plus optionally some shared implementation, while explicitly forbidding direct instantiation — you're meant to interact with it (or its subtypes) through the contract it defines, not by thinking of it as a concrete, complete thing on its own.

---

**Question:** How do interfaces serve as an abstraction mechanism, and how do they differ from abstract classes in that role?

**Answer:**
An interface is a *pure* contract — historically no implementation, no state at all. It abstracts over "can do X" with zero assumptions about shared ancestry or implementation. Abstract classes can share implementation too, which is a form of code reuse abstraction, not just behavioral contract.

---

**Question:** Why does abstraction reduce coupling between parts of a system?

**Answer:**
If code depends on `IPaymentGateway` instead of `StripeGateway`, it has zero knowledge of Stripe's SDK, HTTP calls, or auth details — you can swap the concrete implementation entirely (Stripe → PayPal) without touching the dependent code, because it never depended on those details in the first place.

---

**Question:** What is a "leaky abstraction"?

**Answer:**
An abstraction that's supposed to hide complexity but lets some of that complexity show through anyway — e.g. an ORM's `IQueryable` that behaves differently (or throws) depending on what SQL the underlying provider can actually translate, forcing callers to understand the database after all.

---

**Question:** How does abstraction relate to the Dependency Inversion Principle (the "D" in SOLID)?

**Answer:**
DIP says high-level code should depend on abstractions, not low-level concrete details — and low-level details should depend on those same abstractions too. Abstraction (interfaces/abstract classes) is the literal mechanism that makes this inversion possible; without it, there's nothing to depend on *except* concrete implementations.

---

**Question:** What is an Abstract Data Type (ADT)?

**Answer:**
A type defined purely by the operations you can perform on it and the rules those operations obey — not by how it's actually stored in memory. A `Stack` is an ADT: push/pop/peek and their behavior are the whole contract, regardless of whether it's backed by an array or a linked list.

---

**Question:** Why do we generally prefer depending on abstractions (interfaces) over concrete classes in method signatures?

**Answer:**
A method accepting `IEnumerable<T>` works with arrays, lists, LINQ query results, or a database cursor — anything satisfying that contract. A method accepting `List<T>` specifically only works with that one concrete type, needlessly narrowing what callers can pass and what implementations you can swap in later (e.g. for testing).

---

**Question:** What is premature/over-abstraction, and why is it considered a smell?

**Answer:**
Introducing an interface, abstract base class, or configurable strategy for something that has (and likely will only ever have) one concrete implementation. It adds indirection and cognitive overhead with no actual flexibility payoff — YAGNI ("You Aren't Gonna Need It") is the usual counter-principle invoked here.

---

**Question:** How does abstraction support the Open/Closed Principle (the "O" in SOLID)?

**Answer:**
If code depends on an abstraction (`IDiscountStrategy`), you can add a brand-new implementation to extend behavior without modifying any existing code that already depends on the interface — the system is "open" to new behavior but "closed" to changes in already-working code.

---

**Question:** What is the Template Method pattern, and how does it combine abstraction with polymorphism?

**Answer:**
A base class defines the overall *algorithm's shape* as a sequence of steps (often in a non-overridable method), while individual steps are abstract or virtual and filled in by subclasses. The abstraction is "here's the fixed shape of the process"; polymorphism supplies "here's how each variant customizes specific steps."

---

**Question:** What is a Facade, and how is it a form of abstraction?

**Answer:**
A Facade is a single, simplified interface that hides a complex subsystem with many moving parts (multiple classes, ordering requirements, configuration) behind one or a few easy method calls — the caller interacts with the facade's abstraction and never needs to learn the subsystem's real shape.

---

**Question:** What does "program to an interface, not an implementation" mean in practice?

**Answer:**
Declare variables, parameters, and return types using the abstraction (`IRepository<T>`) rather than the concrete class (`SqlRepository`), even in code that currently only has one implementation — this keeps the door open for substitution (testing, swapping backends) without a later, invasive refactor.

---

**Question:** How does dependency injection rely on abstraction?

**Answer:**
A DI container wires a concrete implementation into a constructor parameter typed as an abstraction — the consuming class is written entirely against the interface and has no idea (and doesn't need to know) which concrete type it actually received at runtime.

---

**Question:** How does abstraction support testability specifically?

**Answer:**
If a class depends on `IEmailSender` instead of a concrete `SmtpEmailSender`, a test can inject a fake/mock implementation that records calls instead of actually sending email — the abstraction is the seam that makes the class testable in isolation.

---

**Question:** Give a real-world analogy for abstraction.

**Answer:**
A car's dashboard: you interact with a steering wheel, pedals, and a few dials — that's the abstraction. You don't need to understand fuel injection timing, engine combustion, or transmission gear ratios to drive; all of that complexity is hidden behind a small, stable interface.

---

**Question:** What is YAGNI, and how does it caution against over-abstraction?

**Answer:**
"You Aren't Gonna Need It" — don't build a flexible abstraction for a hypothetical future requirement that doesn't exist yet. Abstraction has a real cost (indirection, more types to understand); it should be introduced when a second real variant/consumer actually shows up, not speculatively "just in case."

---

**Question:** What is an abstraction "seam," and why is it especially useful in legacy code?

**Answer:**
A seam is a place where you can substitute behavior without editing the surrounding code — usually by introducing an interface around a hard dependency (a static call, a `new SomeConcreteClass()`) that previously had none. It's the standard technique for making untested legacy code testable without a full rewrite.

---

**Question:** How do generics act as an abstraction mechanism, distinct from interfaces/abstract classes?

**Answer:**
Interfaces/abstract classes abstract over **behavior** ("what operations exist"). Generics abstract over **type** ("what data flows through, without caring what it specifically is") — `List<T>` is abstracted over element type; it doesn't need to know or care whether `T` is `int` or `Customer`.

---

**Question:** What's an example of "abstraction leakage" specific to ORMs like Entity Framework?

**Answer:**
`IQueryable<T>` is meant to abstract away the database, but LINQ expressions that can't be translated to SQL throw at runtime (or silently pull the whole table into memory to filter in .NET) — the abstraction only holds as long as you stay within what the provider can actually translate; step outside that, and SQL-specific limitations leak through.

---

**Question:** What's the difference between abstraction and generalization?

**Answer:**
Generalization is about finding **commonality across related types** (e.g. `Dog` and `Cat` generalize to `Animal`) — it's often a *design step* that leads to introducing an abstraction. Abstraction itself is the resulting mechanism (the shared interface/base class) that hides implementation differences behind that commonality.

---

**Question:** How does layered architecture (Controller → Service → Repository) use abstraction?

**Answer:**
Each layer exposes an interface to the layer above it and depends on an interface from the layer below it — the Controller doesn't know how the Service computes results, and the Service doesn't know if the Repository is backed by SQL, an in-memory store, or a remote API. Each boundary is an abstraction seam.

---

**Question:** What is the Command pattern, and what does it abstract over?

**Answer:**
It abstracts over "an action to be performed later" — wrapping a request (and its parameters) as an object with a single `Execute()` method, so the invoker doesn't need to know what the action actually does, just that it can be executed, queued, undone, or logged uniformly.

---

**Question:** What's the difference between abstracting *data* versus abstracting *behavior*?

**Answer:**
Abstracting data means hiding how something is represented/stored (an ADT, a DTO's internal shape). Abstracting behavior means hiding how an operation is actually carried out (an interface method's implementation). Both are "abstraction," but they answer different questions about what's hidden.

---

**Question:** How does the Observer pattern rely on abstraction?

**Answer:**
The subject only knows it has a list of `IObserver` instances and calls `Notify()` on each — it has zero knowledge of what any specific observer actually does with that notification (update a UI, log it, trigger another process). The abstraction decouples "something happened" from "here's what to do about it."

---

**Question:** Why is a repository interface (`IRepository<T>`) a common example of abstraction in real applications?

**Answer:**
Business logic can be written entirely against "get/save/delete an entity" without knowing whether that entity currently lives in SQL Server, an in-memory test double, or eventually a completely different data store — swapping the backing technology becomes a matter of writing one new implementation, not rewriting business logic.

---

**Question:** What's a caution around abstracting "just in case," specifically for interfaces with exactly one implementation forever?

**Answer:**
If a type will realistically only ever have one implementation and there's no plan (or need) to test against a fake, the interface adds a level of indirection (jump-to-definition goes to the interface, not the logic) with no real substitutability benefit — sometimes a concrete class is simply the right level of abstraction.

---

**Question:** How does an Abstract Factory differ from a single Factory Method, in terms of what it abstracts?

**Answer:**
A Factory Method abstracts creation of **one** product type. An Abstract Factory abstracts creation of a **family of related products** that must be consistent with each other (e.g. a UI theme's button + checkbox + scrollbar) — the caller gets one factory and everything it produces is guaranteed to match.

---

## Coding Questions

**Question:** Show an abstract class defining a pure contract with no shared implementation yet.

**Answer:**
```csharp
abstract class Shape
{
    // Pure contract — "what" (compute an area), not "how".
    public abstract double Area();
}

class Circle : Shape
{
    private readonly double _radius;
    public Circle(double radius) => _radius = radius;
    public override double Area() => Math.PI * _radius * _radius;
}
```

---

**Question:** Show an interface used as a pure abstraction with zero implementation detail leaking through.

**Answer:**
```csharp
interface IPaymentGateway
{
    // Caller knows NOTHING about HTTP, auth tokens, retries, or the provider's SDK.
    bool Charge(decimal amount, string cardToken);
}

class OrderProcessor
{
    private readonly IPaymentGateway _gateway;
    public OrderProcessor(IPaymentGateway gateway) => _gateway = gateway;

    public bool Checkout(decimal total, string cardToken) => _gateway.Charge(total, cardToken);
}
```

---

**Question:** Show a Facade hiding a multi-step subsystem behind one simple call.

**Answer:**
```csharp
class VideoConverter // subsystem
{
    public byte[] Decode(byte[] input) => input; // simplified for the example
}
class AudioNormalizer { public byte[] Normalize(byte[] input) => input; }
class Compressor { public byte[] Compress(byte[] input) => input; }

class VideoProcessingFacade
{
    private readonly VideoConverter _converter = new();
    private readonly AudioNormalizer _normalizer = new();
    private readonly Compressor _compressor = new();

    // Caller doesn't need to know the correct ORDER or even that 3 classes exist.
    public byte[] ProcessVideo(byte[] rawVideo)
    {
        var decoded = _converter.Decode(rawVideo);
        var normalized = _normalizer.Normalize(decoded);
        return _compressor.Compress(normalized);
    }
}
```

---

**Question:** Show the Template Method pattern.

**Answer:**
```csharp
abstract class DataExporter
{
    // The fixed "shape" of the algorithm — not overridable, so the sequence can't be broken.
    public string Export(IEnumerable<string> rows)
    {
        var header = GetHeader();
        var body = string.Join(Separator, rows);
        return $"{header}{Separator}{body}";
    }

    protected abstract string GetHeader();   // customizable step
    protected virtual string Separator => "\n"; // customizable step, with a sensible default
}

class CsvExporter : DataExporter
{
    protected override string GetHeader() => "Id,Name";
    protected override string Separator => ",";
}
```

---

**Question:** Show Dependency Inversion — a high-level class depending on an abstraction instead of a concrete detail.

**Answer:**
```csharp
interface INotifier { void Notify(string message); }

class EmailNotifier : INotifier
{
    public void Notify(string message) => Console.WriteLine($"Email: {message}");
}

class OrderShippedHandler // high-level policy
{
    private readonly INotifier _notifier; // depends on the abstraction, not EmailNotifier directly
    public OrderShippedHandler(INotifier notifier) => _notifier = notifier;

    public void Handle(string orderId) => _notifier.Notify($"Order {orderId} shipped");
}
```

---

**Question:** Show a Repository abstraction letting you swap SQL for an in-memory store with zero business-logic changes.

**Answer:**
```csharp
interface IUserRepository
{
    User? FindByEmail(string email);
}

class SqlUserRepository : IUserRepository
{
    public User? FindByEmail(string email) => /* real ADO.NET/EF query */ null;
}

class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<string, User> _users = new();
    public User? FindByEmail(string email) => _users.GetValueOrDefault(email);
}

class LoginService
{
    private readonly IUserRepository _repo; // works identically with either implementation
    public LoginService(IUserRepository repo) => _repo = repo;
}
```

---

**Question:** Show abstracting time via an `IClock` interface, for testability.

**Answer:**
```csharp
interface IClock
{
    DateTime UtcNow { get; }
}

class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow; // the "real" implementation
}

class FakeClock : IClock
{
    public DateTime UtcNow { get; set; } // fully controllable in tests
}

class SubscriptionService
{
    private readonly IClock _clock;
    public SubscriptionService(IClock clock) => _clock = clock;

    public bool IsExpired(DateTime expiresAt) => _clock.UtcNow > expiresAt; // testable without waiting for real time
}
```

---

**Question:** Bug hunt — spot the leaky abstraction, then fix it.

**Answer:**
```csharp
// BUG: the interface exposes a raw SqlConnection — a caller now knows this is SQL,
// defeating the whole point of abstracting the data source.
interface IUserRepositoryLeaky
{
    SqlConnection GetConnection();
}

// Fixed: the interface only exposes the OPERATION, never the underlying technology.
interface IUserRepository
{
    User? FindByEmail(string email);
}
```

---

**Question:** Show `IComparer<T>` abstracting sort order away from the sorting algorithm itself.

**Answer:**
```csharp
class Person { public string Name = ""; public int Age; }

class ByAgeComparer : IComparer<Person>
{
    public int Compare(Person? a, Person? b) => a!.Age.CompareTo(b!.Age);
}

var people = new List<Person> { new() { Name = "Bo", Age = 40 }, new() { Name = "Al", Age = 25 } };
people.Sort(new ByAgeComparer());
// List.Sort() has zero knowledge of "Age" — it only knows the IComparer<T> contract.
```

---

**Question:** Show abstracting logging behind an interface instead of calling `Console.WriteLine` directly.

**Answer:**
```csharp
interface ILogger
{
    void Info(string message);
}

class ConsoleLogger : ILogger
{
    public void Info(string message) => Console.WriteLine($"[INFO] {message}");
}

class OrderService
{
    private readonly ILogger _logger; // could be console, file, cloud logging — service doesn't care
    public OrderService(ILogger logger) => _logger = logger;

    public void PlaceOrder(string id)
    {
        _logger.Info($"Placing order {id}");
    }
}
```

---

**Question:** Show the Command pattern abstracting "an action" as an object.

**Answer:**
```csharp
interface ICommand
{
    void Execute();
}

class TurnOnLightCommand : ICommand
{
    public void Execute() => Console.WriteLine("Light on");
}

class RemoteControl
{
    private readonly List<ICommand> _history = new();

    // Doesn't know or care WHAT the command does — just that it's executable.
    public void Run(ICommand command)
    {
        command.Execute();
        _history.Add(command);
    }
}
```

---

**Question:** Show an Abstract Factory producing a family of related, consistent objects.

**Answer:**
```csharp
interface IButton { void Render(); }
interface ICheckbox { void Render(); }

interface IUiFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}

class DarkButton : IButton { public void Render() => Console.WriteLine("Dark button"); }
class DarkCheckbox : ICheckbox { public void Render() => Console.WriteLine("Dark checkbox"); }

class DarkThemeFactory : IUiFactory
{
    // Guarantees every control this factory produces matches the same theme.
    public IButton CreateButton() => new DarkButton();
    public ICheckbox CreateCheckbox() => new DarkCheckbox();
}
```

---

**Question:** Show abstracting a message queue so producer code doesn't know if it's in-memory or a real broker.

**Answer:**
```csharp
interface IMessageQueue<T>
{
    void Publish(T message);
}

class InMemoryQueue<T> : IMessageQueue<T>
{
    public Queue<T> Items = new();
    public void Publish(T message) => Items.Enqueue(message);
}

// A real implementation could wrap Azure Service Bus, RabbitMQ, Kafka, etc.,
// with IDENTICAL calling code on the producer side.
class OrderCreatedPublisher
{
    private readonly IMessageQueue<string> _queue;
    public OrderCreatedPublisher(IMessageQueue<string> queue) => _queue = queue;

    public void Publish(string orderId) => _queue.Publish($"OrderCreated:{orderId}");
}
```

---

**Question:** Show the Observer pattern abstracting "something happened" from "what to do about it."

**Answer:**
```csharp
interface IObserver<T> { void OnNext(T value); }

class Subject<T>
{
    private readonly List<IObserver<T>> _observers = new();
    public void Subscribe(IObserver<T> observer) => _observers.Add(observer);

    public void Publish(T value)
    {
        // Subject has NO idea what any observer actually does with 'value'.
        foreach (var observer in _observers) observer.OnNext(value);
    }
}

class LoggingObserver : IObserver<string>
{
    public void OnNext(string value) => Console.WriteLine($"Logged: {value}");
}
```

---

**Question:** Show abstracting configuration access instead of reading environment variables directly everywhere.

**Answer:**
```csharp
interface IAppConfig
{
    string GetConnectionString();
}

class EnvironmentAppConfig : IAppConfig
{
    public string GetConnectionString() =>
        Environment.GetEnvironmentVariable("DB_CONNECTION") ?? throw new InvalidOperationException("Missing config");
}

class Startup
{
    private readonly IAppConfig _config; // could later come from a file, Key Vault, etc. with no changes here
    public Startup(IAppConfig config) => _config = config;
}
```

---

**Question:** Show abstracting caching, swapping in-memory for distributed with no consumer changes.

**Answer:**
```csharp
interface ICache
{
    T? Get<T>(string key);
    void Set<T>(string key, T value);
}

class InMemoryCache : ICache
{
    private readonly Dictionary<string, object> _store = new();
    public T? Get<T>(string key) => _store.TryGetValue(key, out var v) ? (T)v : default;
    public void Set<T>(string key, T value) => _store[key] = value!;
}

class ProductService
{
    private readonly ICache _cache; // a Redis-backed ICache would need ZERO changes here
    public ProductService(ICache cache) => _cache = cache;
}
```

---

**Question:** Show a "minimal necessary abstraction" versus premature over-abstraction, for a single-implementation scenario.

**Answer:**
```csharp
// Over-abstracted: IGreeter has exactly one implementation, ever, with no test double planned.
interface IGreeter { string Greet(string name); }
class EnglishGreeter : IGreeter { public string Greet(string name) => $"Hello, {name}"; }

// Simpler and just as correct, if there's truly no second implementation coming:
static class Greeter
{
    public static string Greet(string name) => $"Hello, {name}";
}
```

---

**Question:** Show abstracting an email/SMS notification behind a single interface with multiple concrete channels.

**Answer:**
```csharp
interface INotificationChannel
{
    void Send(string recipient, string message);
}

class EmailChannel : INotificationChannel
{
    public void Send(string recipient, string message) => Console.WriteLine($"Email to {recipient}: {message}");
}

class SmsChannel : INotificationChannel
{
    public void Send(string recipient, string message) => Console.WriteLine($"SMS to {recipient}: {message}");
}

class AlertService
{
    private readonly IEnumerable<INotificationChannel> _channels; // fan-out over however many channels exist
    public AlertService(IEnumerable<INotificationChannel> channels) => _channels = channels;

    public void Alert(string recipient, string message)
    {
        foreach (var channel in _channels) channel.Send(recipient, message);
    }
}
```

---

**Question:** Show validation rules abstracted behind a common interface so new rules can be added without touching existing code.

**Answer:**
```csharp
interface IValidationRule<T>
{
    bool IsSatisfiedBy(T item);
    string ErrorMessage { get; }
}

class MinAgeRule : IValidationRule<int>
{
    public bool IsSatisfiedBy(int age) => age >= 18;
    public string ErrorMessage => "Must be at least 18";
}

IEnumerable<string> Validate<T>(T item, IEnumerable<IValidationRule<T>> rules) =>
    // Adding a new rule never requires touching this method.
    rules.Where(rule => !rule.IsSatisfiedBy(item)).Select(rule => rule.ErrorMessage);
```

---

**Question:** Show layered architecture abstraction — Controller depends only on a Service interface.

**Answer:**
```csharp
interface IOrderService
{
    void PlaceOrder(string customerId, decimal amount);
}

class OrderService : IOrderService
{
    // The Controller has no idea whether this hits a database, a queue, or a mock.
    public void PlaceOrder(string customerId, decimal amount) =>
        Console.WriteLine($"Order placed for {customerId}: {amount}");
}

class OrdersController
{
    private readonly IOrderService _service;
    public OrdersController(IOrderService service) => _service = service;

    public void Post(string customerId, decimal amount) => _service.PlaceOrder(customerId, amount);
}
```

---

**Question:** Show adapting a third-party API behind your own abstraction (Adapter-flavored abstraction).

**Answer:**
```csharp
// Third-party SDK shape you don't control:
class ThirdPartyWeatherClient
{
    public string FetchRawJson(string city) => "{\"temp_f\": 72}";
}

// Your own abstraction, decoupled from the SDK's exact method names/shapes.
interface IWeatherProvider
{
    double GetTemperatureCelsius(string city);
}

class ThirdPartyWeatherAdapter : IWeatherProvider
{
    private readonly ThirdPartyWeatherClient _client = new();

    public double GetTemperatureCelsius(string city)
    {
        // All the "how" — parsing JSON, converting units — is hidden here.
        var fahrenheit = 72; // parsed from _client.FetchRawJson(city) in reality
        return (fahrenheit - 32) * 5.0 / 9.0;
    }
}
```

---

**Question:** Show abstracting a payment gateway so switching providers touches only one class.

**Answer:**
```csharp
interface IPaymentGateway
{
    bool Charge(string cardToken, decimal amount);
}

class StripeGateway : IPaymentGateway
{
    public bool Charge(string cardToken, decimal amount)
    {
        Console.WriteLine($"Charging {amount} via Stripe");
        return true;
    }
}

class PayPalGateway : IPaymentGateway
{
    public bool Charge(string cardToken, decimal amount)
    {
        Console.WriteLine($"Charging {amount} via PayPal");
        return true;
    }
}

// Swapping StripeGateway for PayPalGateway is a ONE-LINE change at the composition root —
// CheckoutService itself never changes.
class CheckoutService
{
    private readonly IPaymentGateway _gateway;
    public CheckoutService(IPaymentGateway gateway) => _gateway = gateway;
}
```

---

**Question:** Show an abstract base class providing shared, non-abstract helper logic alongside an abstract contract.

**Answer:**
```csharp
abstract class ReportGenerator
{
    // Shared, concrete helper — every subclass gets this for free.
    protected string FormatDate(DateTime date) => date.ToString("yyyy-MM-dd");

    // Contract each subclass must fulfill its own way.
    public abstract string Generate(DateTime asOf);
}

class SalesReport : ReportGenerator
{
    public override string Generate(DateTime asOf) => $"Sales report as of {FormatDate(asOf)}";
}
```
 