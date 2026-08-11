# ref vs out — Interview Q&A

---

### Q1. What is `ref`, and what does it do?

**Answer:**
"`ref` passes a parameter by reference instead of by value — so the method works directly on the caller's variable, not a copy. The variable has to already be initialized before the call, and any change the method makes to it is visible back in the caller."

```csharp
void Double(ref int number)
{
    number = number * 2;
}

int x = 5;
Double(ref x);
Console.WriteLine(x); // 10 — the caller's variable was changed
```

**Where to use:** when a method needs to modify a caller's existing variable directly — e.g., a swap function, or a performance-sensitive case where you want to avoid copying a large struct.

---

### Q2. What is `out`, and how is it different from `ref`?

**Answer:**
"`out` also passes by reference, but it's meant for a method to *produce* a value for the caller, not read one. The variable doesn't need to be initialized before the call — in fact the method is required to assign it before returning. That's the core difference: `ref` requires the variable to already have a value going in; `out` doesn't care what's in it going in, but guarantees it'll have a value coming out."

```csharp
bool TryParseAge(string input, out int age)
{
    if (int.TryParse(input, out age))
        return true;

    age = 0; // must assign before returning, even on the failure path
    return false;
}

int result;                       // no need to initialize
bool success = TryParseAge("25", out result);
Console.WriteLine(result); // 25
```

**Where to use:** the classic case is `TryParse`/`TryGetValue`-style methods — returning a `bool` for success/failure, and using `out` to hand back the actual result only when it succeeded.

---

### Q3. Side-by-side comparison

| | `ref` | `out` |
|---|---|---|
| Caller must initialize before calling? | Yes | No |
| Callee must assign before returning? | No (optional) | Yes (mandatory) |
| Typical use case | Modify an existing value in place (swap, in-place update) | Return an additional value from a method (`TryParse`, `TryGetValue`) |
| Direction of intent | "Read and possibly write" | "Write only — this is an output" |

---

### Q4. What happens if you don't assign an `out` parameter before the method returns?

**Answer:**
"It won't compile. The compiler enforces that every `out` parameter is definitely assigned on every code path before the method returns — including exception paths. That's the whole contract of `out`: the caller is guaranteed to get a value back."

```csharp
bool TryDivide(int a, int b, out int result)
{
    if (b == 0)
    {
        result = 0;   // still required, even on the failure branch
        return false;
    }

    result = a / b;
    return true;
}
```

---

### Q5. Can you call a method with `ref` without initializing the variable first?

**Answer:**
"No — that's the opposite rule from `out`. With `ref`, the compiler requires the variable to already be assigned before you pass it in, because the method might read the current value before (or instead of) writing a new one."

```csharp
int x;
Double(ref x); // COMPILE ERROR — use of unassigned local variable 'x'

int y = 0;
Double(ref y); // fine — y was initialized first
```

---

### Q6. Why does `int.TryParse` use `out` instead of just returning the parsed number?

**Answer:**
"Because the method already needs its return value for something else — success/failure as a `bool` — so it can't also use the return value for the parsed number. `out` lets it return two things: whether parsing succeeded, and the actual parsed value, without needing a wrapper object or throwing an exception for the common 'invalid input' case."

```csharp
// Without out, you'd need a wrapper object or exceptions for a common, expected case:
if (int.TryParse(userInput, out int parsedValue))
{
    Console.WriteLine($"Parsed: {parsedValue}");
}
else
{
    Console.WriteLine("Invalid input");
}
```

**Where to use:** any "try this operation, tell me if it worked, and give me the result if it did" method — avoids using exceptions for control flow on expected failure cases (parsing bad input, dictionary lookups, etc.).

---

### Q7. Do `ref` and `out` affect overload resolution?

**Answer:**
"Yes — a method can be overloaded by `ref`/`out`/plain differently, because they're part of the method signature. But you can't have two overloads that differ *only* by `ref` vs `out` on the same parameter — the compiler considers those ambiguous at the call site, since the caller's syntax (`ref x` vs `out x`) is the only way to tell them apart, but the underlying calling convention is too similar."

```csharp
void Method(int x) { }
void Method(ref int x) { } // valid overload — different signature

// void Method(out int x) { } // COMPILE ERROR if this pair already has a ref overload
```

---

### Q8. What's `in`, and how does it relate to `ref`/`out`?

**Answer:**
"`in` also passes by reference, but read-only — the method can read the caller's variable directly (avoiding a copy, useful for large structs) but can't modify it. Think of it as the 'read-only' counterpart to `ref`/`out`: `ref` = read-write, `out` = write-only, `in` = read-only."

```csharp
void PrintTotal(in decimal total)
{
    Console.WriteLine(total);
    // total = 0; // COMPILE ERROR — can't modify an 'in' parameter
}
```

**Where to use:** performance-sensitive code passing large `struct` values (like a big `readonly struct`) where you want to avoid the copy cost of pass-by-value, but the method has no business modifying it.

---

### Quick one-liner if asked to summarize

> "Both `ref` and `out` pass a parameter by reference instead of by value. `ref` requires the variable to already have a value going in, and the method may or may not change it. `out` doesn't require an initial value, but the method must assign one before returning — it's meant purely for returning an extra value out of the method, like the classic `TryParse(string s, out int result)` pattern."
 