# is vs as — Interview Q&A

---

### Q1. What's the difference between `is` and `as`?

**Answer:**
"`is` checks whether an object is compatible with a given type and returns a `bool` — it doesn't give you back a converted reference by itself (though the modern pattern-matching form does, see Q3). `as` actually attempts the conversion and returns the object cast to that type, or `null` if the conversion isn't possible — it never throws."

```csharp
object obj = "hello";

bool isString = obj is string;         // true — just a check, returns bool

string s = obj as string;               // "hello" — successful conversion
object notAString = 5;
string s2 = notAString as string;       // null — failed conversion, no exception
```

---

### Q2. How is `as` different from a regular cast, like `(string)obj`?

**Answer:**
"A regular cast throws an `InvalidCastException` if the conversion fails. `as` returns `null` instead of throwing. That makes `as` better when a failed conversion is an *expected*, normal outcome you want to handle gracefully — a regular cast is better when a failed conversion means something is genuinely wrong and should blow up loudly."

```csharp
object obj = 5;

string s1 = (string)obj;  // throws InvalidCastException
string s2 = obj as string; // null, no exception — check for null afterward
```

**Where to use:** `as` when null is a legitimate, expected possibility you'll check for; a direct cast when you're certain of the type and want a loud failure if you're wrong (fail fast on a real bug).

---

### Q3. Important rule: can `as` be used with value types like `int`?

**Answer:**
"Only with nullable value types — `as` requires the target type to be a reference type or a `Nullable<T>`, because it needs to be able to return `null` on failure, and a non-nullable value type can't hold `null`."

```csharp
object obj = "hello";

int i = obj as int;       // COMPILE ERROR — int can't be null
int? i2 = obj as int?;    // fine — Nullable<int> CAN hold null; result is null here since obj isn't an int
```

---

### Q4. What's the modern, preferred way to check-and-cast in one step?

**Answer:**
"Pattern matching with `is` — `if (obj is string s)` checks the type AND assigns the converted value to a new variable `s`, all in one step, only if the check succeeds. This is generally cleaner than the old two-step pattern of checking with `is` and then casting again, or using `as` and then null-checking."

```csharp
object obj = "hello";

// Modern pattern-matching is — check + cast in one step
if (obj is string s)
{
    Console.WriteLine(s.Length); // s is already the converted string here
}

// Older equivalent, more verbose and does the check/cast work twice
if (obj is string)
{
    string s2 = (string)obj;
    Console.WriteLine(s2.Length);
}
```

**Where to use:** default to pattern-matching `is` (`if (obj is Type variable)`) for new code — it's shorter, safer, and avoids redundant casting.

---

### Q5. Why would `as` still be preferred over pattern-matching `is` in some cases?

**Answer:**
"When you need the converted value regardless of a simple true/false branch — for example, assigning it to a field, or handling `null` later in a more complex flow rather than immediately branching on it. `as` also reads naturally when you already expect the value might be null and want to handle that later, like `var handler = obj as IDisposable; handler?.Dispose();`."

```csharp
var handler = obj as IDisposable;
handler?.Dispose(); // null-conditional handles the "wasn't the right type" case gracefully
```

---

### Q6. Does `is`/`as` work with interfaces, or only classes?

**Answer:**
"Both — `is` and `as` work with any reference conversion, including checking whether an object implements a particular interface, not just class hierarchies."

```csharp
object obj = new List<int>();

if (obj is IEnumerable<int> enumerable)
{
    foreach (var item in enumerable) { /* ... */ }
}

IDisposable disposable = obj as IDisposable; // null — List<int> doesn't implement IDisposable
```

---

### Q7. Performance-wise, is `is` + cast slower than `as`?

**Answer:**
"A direct cast or `as` each do one type check internally. The old pattern of `if (obj is Type) { var x = (Type)obj; }` does the type check *twice* — once for `is`, once implicitly for the cast — so it's marginally slower and more verbose. Pattern-matching `is Type variable` fixes this by doing the check once and binding the variable in the same step, so it's both cleaner and slightly more efficient."

---

### Q8. Quick decision guide — which do you reach for?

**Answer:**
- Just need a true/false check, no need for the converted value → `is` (`if (obj is Customer)`).
- Need the converted value only if the check succeeds, branching immediately → pattern-matching `is` (`if (obj is Customer c)`).
- Need the converted value (possibly `null`) to use later, or want to chain with `?.` → `as`.
- Absolutely certain of the type, and a failure would mean a real bug → direct cast (`(Customer)obj`), so it throws loudly instead of silently returning null.

---

### Quick one-liner if asked to summarize

> "`is` checks if an object is compatible with a type and returns a bool (or, with pattern matching, checks and casts in one step). `as` attempts a conversion and returns null on failure instead of throwing — usable only with reference types or nullable value types. Use a direct cast instead of either when a failed conversion should be a loud exception, not a silent null."
 