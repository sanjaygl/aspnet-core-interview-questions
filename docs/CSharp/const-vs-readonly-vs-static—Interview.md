# const vs readonly vs static — Interview Q&A

---

### Q1. What is `const`?

**Answer:**
"`const` is a compile-time constant. Its value has to be known at compile time, it's implicitly static, and it can never change. Because the value gets baked directly into the IL everywhere it's used, it's not even a real field at runtime — the compiler substitutes the literal value."

```csharp
public class Config
{
    public const int MaxRetries = 3;
    public const string AppName = "UWB";
}

Config.MaxRetries; // used like a static member, no instance needed
```

**Where to use:** true constants that will never change across the lifetime of the app — math constants, fixed limits, magic strings/numbers that are part of the design, not configuration.

---

### Q2. What is `readonly`?

**Answer:**
"`readonly` means the field can only be assigned once — either inline at declaration or inside the constructor. Unlike `const`, the value doesn't have to be known at compile time — it can be computed at runtime, e.g., from a config file or a method call."

```csharp
public class Order
{
    public readonly DateTime CreatedAt;
    public readonly string Id;

    public Order()
    {
        CreatedAt = DateTime.UtcNow; // fine — set once, in the constructor
        Id = Guid.NewGuid().ToString();
    }
}
```

**Where to use:** values that are fixed per-instance but only known at runtime — an ID generated in the constructor, a value read from configuration/DI at construction time.

---

### Q3. What is `static`?

**Answer:**
"`static` means the member belongs to the type itself, not to any individual instance. There's only ever one copy of it, shared across every instance of that class."

```csharp
public class Counter
{
    public static int TotalCount = 0;

    public Counter()
    {
        TotalCount++; // shared across ALL instances
    }
}

new Counter();
new Counter();
Console.WriteLine(Counter.TotalCount); // 2
```

**Where to use:** shared state or shared behavior that doesn't depend on any specific instance — utility/helper methods, counters, caches, singletons.

---

### Q4. What's the actual difference between `const` and `readonly`?

**Answer:**
"The big ones: `const` must be assigned at compile time with a literal value and can only be a primitive/string/enum. `readonly` can be assigned at runtime, in the constructor, and can be any type — including reference types or values computed from other code. Also, `const` is implicitly static; `readonly` is per-instance unless you also mark it `static`."

| | `const` | `readonly` |
|---|---|---|
| When assigned | Compile time only | Compile time OR in the constructor (runtime) |
| Value source | Must be a literal/constant expression | Can be computed at runtime |
| Implicitly static? | Yes | No — need to add `static` explicitly if you want that |
| Allowed types | Primitives, `string`, `enum` | Any type |

---

### Q5. Why is mixing versions of a DLL with `const` fields dangerous?

**Answer:**
"Because `const` values are baked directly into the *caller's* compiled IL at compile time — not looked up at runtime. If library A defines `public const int Version = 1`, and I compile my app against it, my app's IL literally contains `1`. If library A is later updated to `Version = 2` and I only replace the DLL without recompiling my app, my app still uses the old baked-in value `1`. `readonly` doesn't have this problem, because it's read from the field at runtime, not inlined."

**Where this matters:** shared/public library code exposing "constants" that might change between versions — prefer `static readonly` over `const` if the value could ever change in a future release, even if it's technically fixed today.

```csharp
// Risky in a shared library if the value might change later:
public const int Version = 1;

// Safer — always re-read at runtime, no stale inlining across binary versions:
public static readonly int Version = 1;
```

---

### Q6. Can `readonly` be used with reference types? Does that make the object immutable?

**Answer:**
"Yes, but it only locks the *reference*, not the object's contents. Once assigned, I can't point the field at a different object — but I can still mutate the object it already points to, if that object is itself mutable."

```csharp
public class Holder
{
    public readonly List<int> Items = new List<int>();
}

var h = new Holder();
h.Items.Add(1);      // fine — mutating the object the reference points to
h.Items = new List<int>(); // COMPILE ERROR — can't reassign the reference itself
```

**Where to use:** `readonly` collections/objects when you only need to prevent reassignment of the field, not the immutability of its contents. For true immutability, use an immutable type (e.g., `ImmutableList<T>`) or expose a read-only view (`IReadOnlyList<T>`).

---

### Q7. Can `static` be combined with `readonly`? Why would you do that?

**Answer:**
"Yes — `static readonly` gives you one shared value across all instances, but assigned at runtime instead of compile time. This is the go-to pattern when you want something constant-like, but the value has to be computed or can't be a compile-time literal — like a value coming from configuration, or `DateTime`/`Guid`/objects that `const` doesn't support at all."

```csharp
public class AppSettings
{
    public static readonly string ConnectionString = Environment.GetEnvironmentVariable("DB_CONN");
    public static readonly DateTime StartupTime = DateTime.UtcNow;
}
```

**Where to use:** anytime you want a single shared, effectively-constant value that either isn't a compile-time literal, or is a type `const` doesn't support.

---

### Q8. What is a static constructor, and how does it relate to `static readonly`?

**Answer:**
"A static constructor runs once, automatically, the first time the class is used — before any static members are accessed or any instance is created. It's the place to do more complex initialization of `static readonly` fields that can't be done in a single expression."

```csharp
public class Settings
{
    public static readonly Dictionary<string, string> Values;

    static Settings()
    {
        Values = new Dictionary<string, string>();
        Values["Env"] = "Production";
    }
}
```

**Where to use:** initializing static state that needs more than one line of setup logic, or that needs error handling before the class is first used.

---

### Q9. Is `static` state thread-safe? What's the risk?

**Answer:**
"No, not automatically. Since a `static` field is shared across the whole application, if multiple threads read and write it at the same time without synchronization, you can get race conditions. `readonly` static fields are safe to *read* concurrently, since they're never reassigned after construction — the risk is specifically with *mutable* static state, like a static `List<T>` or `Dictionary<T>` that multiple threads modify."

```csharp
// Risky under concurrent access — not thread-safe
public static class Cache
{
    public static Dictionary<string, string> Items = new();
}

// Safer — use a thread-safe collection, or lock around access
public static class Cache
{
    public static readonly ConcurrentDictionary<string, string> Items = new();
}
```

**Where to use:** if you need shared mutable state across requests/threads (e.g., in a web app), use thread-safe collections (`ConcurrentDictionary`, etc.) or explicit locking — never a plain mutable static collection.

---

### Q10. Quick decision guide — which one do I reach for?

**Answer:**
- Value never changes, ever, and is known at compile time (a literal) → **`const`**.
- Value is fixed per object but computed at runtime (e.g., in the constructor) → **`readonly`**.
- Value/behavior is shared across all instances, not tied to any one object → **`static`**.
- Value is shared across all instances AND fixed after being computed once at startup → **`static readonly`**.

---

### Quick one-liner if asked to summarize

> "`const` is a compile-time literal baked into the IL. `readonly` is assigned once, but at runtime — usually in the constructor. `static` means the member belongs to the type, shared across all instances, not to any single object. They're independent concepts — you can combine `static` and `readonly`, but `const` is always implicitly static and can't be combined with `readonly`."
 