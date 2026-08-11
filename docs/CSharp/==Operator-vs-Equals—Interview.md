# == Operator vs .Equals() — Interview Q&A

---

### Q1. What's the difference between `==` and `.Equals()`?

**Answer:**
"`==` is an operator — for value types it compares the actual values, but for reference types, by default, it compares whether two references point to the *same object* (reference equality), unless the type overloads `==` to do something else. `.Equals()` is a virtual method inherited from `object` — it's meant to be overridden by a type to define what 'equal' actually means for it, like comparing all its fields. So the real difference is: `==` is a compile-time-resolved operator that can be overloaded per-type; `.Equals()` is a runtime virtual method call that can be overridden per-type."

```csharp
int a = 5;
int b = 5;
Console.WriteLine(a == b);        // true — value types compare by value
Console.WriteLine(a.Equals(b));   // true — same result for value types

class Point { public int X; }
var p1 = new Point { X = 1 };
var p2 = new Point { X = 1 };
Console.WriteLine(p1 == p2);       // false — different objects, default reference equality
Console.WriteLine(p1.Equals(p2));  // false — Equals not overridden, falls back to reference equality too
```

**Where this comes up:** for a plain `class` with no overrides, `==` and `.Equals()` give the *same* answer (reference equality) — the difference only becomes visible once a type overrides one, the other, or both.

---

### Q2. Why does `==` work differently for `string`, even though it's a reference type?

**Answer:**
"Because `string` overloads the `==` operator to do value equality — comparing the actual characters — instead of the default reference equality every other reference type has. That's a special design decision baked into `string`, not something reference types get for free."

```csharp
string s1 = "hello";
string s2 = "hel" + "lo";       // different object at runtime (usually), same content
Console.WriteLine(s1 == s2);       // true — string overloads == for value equality
Console.WriteLine(s1.Equals(s2));  // true — string also overrides Equals consistently

Console.WriteLine(ReferenceEquals(s1, s2)); // could be true or false depending on interning — don't rely on it
```

**Where this comes up as a trick question:** "reference types always use reference equality with `==`" is false — `string` is the classic counter-example everyone should know.

---

### Q3. How do you check true reference equality, ignoring any operator overloads?

**Answer:**
"With the static method `object.ReferenceEquals(a, b)`. It always checks 'are these literally the same object in memory', regardless of whether the type overloaded `==` or `Equals`. Useful when you specifically need to bypass custom equality logic."

```csharp
string s1 = "hello";
string s2 = new string("hello".ToCharArray()); // forces a genuinely different object

Console.WriteLine(s1 == s2);                    // true — value equality via overloaded ==
Console.WriteLine(ReferenceEquals(s1, s2));      // false — different objects in memory
```

---

### Q4. How do you make `==` and `.Equals()` do value comparison for your own class?

**Answer:**
"Override `Equals(object obj)`, and — critically — override `GetHashCode()` at the same time, since any type used as a dictionary key or in a hash set relies on both being consistent. Optionally, also overload the `==` and `!=` operators so both syntaxes behave the same way; if you don't, `==` still does reference equality even though `.Equals()` now does value equality, which is a common source of bugs."

```csharp
public class Point
{
    public int X, Y;

    public override bool Equals(object obj)
    {
        return obj is Point other && X == other.X && Y == other.Y;
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public static bool operator ==(Point a, Point b) =>
        a is null ? b is null : a.Equals(b);

    public static bool operator !=(Point a, Point b) => !(a == b);
}

var p1 = new Point { X = 1, Y = 2 };
var p2 = new Point { X = 1, Y = 2 };
Console.WriteLine(p1 == p2);       // true — now uses value equality
Console.WriteLine(p1.Equals(p2));  // true
```

**Where to use:** any domain/value object where two separate instances with the same data should be considered equal — DTOs used as dictionary keys, value objects like `Money`, `DateRange`, coordinates.

---

### Q5. Why do you have to override `GetHashCode()` whenever you override `Equals()`?

**Answer:**
"Because collections like `Dictionary` and `HashSet` use `GetHashCode()` first to find the right 'bucket', then use `Equals()` to confirm the match within that bucket. If two objects are `Equals()`-equal but have different hash codes, a dictionary can put them in different buckets and never find one when looking up by the other — breaking lookups silently. The contract is: equal objects MUST have equal hash codes (the reverse isn't required — different objects can share a hash code)."

```csharp
// BAD - Equals overridden but GetHashCode left as default (reference-based)
public override bool Equals(object obj) => obj is Point p && X == p.X && Y == p.Y;
// GetHashCode() not overridden -> two "equal" points can get different hash codes

var dict = new Dictionary<Point, string>();
dict[new Point { X = 1, Y = 1 }] = "origin";
dict.ContainsKey(new Point { X = 1, Y = 1 }); // could return false! broken lookup
```

---

### Q6. What's `IEquatable<T>`, and why bother implementing it?

**Answer:**
"`IEquatable<T>` adds a strongly-typed `Equals(T other)` method, avoiding the boxing and type-check/cast that `Equals(object obj)` needs. Generic collections like `List<T>.Contains()` and `Dictionary<TKey,TValue>` check for `IEquatable<T>` first and use it when available, which is faster for value types especially, since it skips boxing."

```csharp
public struct Point : IEquatable<Point>
{
    public int X, Y;

    public bool Equals(Point other) => X == other.X && Y == other.Y; // no boxing, no cast
    public override bool Equals(object obj) => obj is Point p && Equals(p);
    public override int GetHashCode() => HashCode.Combine(X, Y);
}
```

**Where to use:** any `struct` you define, or a `class` used heavily in generic collections — implementing `IEquatable<T>` is a meaningful, cheap performance win.

---

### Q7. Does `==` behave differently for boxed value types?

**Answer:**
"Yes, and it's a classic gotcha. Once a value type is boxed into `object`, `==` compares the object references, not the underlying values — because you're now using `object`'s `==`, not the value type's. `.Equals()` still works correctly, because `object.Equals` is virtual and dispatches to the actual overridden `Equals` for that value type."

```csharp
int a = 5;
int b = 5;
object boxedA = a;
object boxedB = b;

Console.WriteLine(boxedA == boxedB);       // false — reference comparison once boxed!
Console.WriteLine(boxedA.Equals(boxedB));  // true — Equals still does value comparison
```

**Where this comes up:** a frequently-asked trick question — "why does `==` return false for two boxed ints with the same value?"

---

### Q8. How do `record` types in C# handle equality?

**Answer:**
"Records get value-based equality generated automatically by the compiler — `Equals`, `GetHashCode`, and `==`/`!=` are all overridden to compare every property, without writing any of that boilerplate yourself. This is one of the main reasons to use a `record` instead of a `class` for simple data-carrying types."

```csharp
public record Point(int X, int Y);

var p1 = new Point(1, 2);
var p2 = new Point(1, 2);
Console.WriteLine(p1 == p2);       // true — compiler-generated value equality
Console.WriteLine(p1.Equals(p2));  // true
```

**Where to use:** DTOs, value objects, anything where "two instances with the same data are the same thing" — use a `record` and get correct `Equals`/`GetHashCode`/`==` for free instead of hand-writing them on a `class`.

---

### Quick one-liner if asked to summarize

> "`==` is an operator that, by default, does reference equality for classes and value equality for structs/primitives — but it can be overloaded per-type (like `string` does). `.Equals()` is a virtual method meant to be overridden to define what equality actually means for a type. For a class with no overrides, both do the same thing (reference equality); once you override one for value semantics, you should override `GetHashCode()` too, and usually `==`/`!=` as well, to keep everything consistent — or just use a `record`, which generates all of that automatically."
 