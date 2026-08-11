# Value Types

A **value type** holds the **actual value** directly in its allocated memory.

```csharp
int a = 10;
int b = a;   // A brand new copy of the value is created
b = 20;      // Modifying 'b' has zero impact on 'a'

Console.WriteLine(a); // Output: 10
Console.WriteLine(b); // Output: 20
```

**Result:** `a` and `b` maintain completely independent data blocks. Changing `b` does not affect `a`.

```text
Variable Memory Layout (Assuming Local Variables):
┌─────────┐
│ a = 10  │ 💻 (Stack Allocation)
├─────────┤
│ b = 20  │ 💻 (Stack Allocation)
└─────────┘
```

---

# Reference Types

A **reference type** stores a **memory address (pointer)** that directs to the actual object. The actual object data is always allocated on the managed **Heap**.

```csharp
class Test
{
    public int Value;
}

Test t1 = new Test { Value = 10 };
Test t2 = t1;  // Copies the memory address reference, NOT the object
t2.Value = 20; // Modifies the object shared by both variables

Console.WriteLine(t1.Value); // Output: 20
Console.WriteLine(t2.Value); // Output: 20
```

**Result:** Both `t1` and `t2` point to the exact same object in memory. Any mutations made through one reference variable will be immediately visible to the other.

```text
Variable Storage (Pointer):           Actual Object Data:
┌─────────────┐                      ┌──────────────┐
│ t1 ───────┐ │                      │ Test Object  │
├───────────┼─┼─────────────────────>│ Value = 20   │ 🧠 (Heap Allocation)
│ t2 ───────┘ │                      └──────────────┘
└─────────────┘
💻 (Stack Allocation)
```


# Value Types vs Reference Types — Interview Q&A

---

### Q1. What's the difference between a value type and a reference type?

**Answer:**
"A value type holds its actual data directly, and when you assign it or pass it to a method, you get a full copy — the original and the copy are independent. A reference type holds a reference (a pointer) to data that lives elsewhere; when you assign it or pass it around, you're copying the reference, not the data — so both variables end up pointing at the same object."

```csharp
// Value type - struct
struct Point { public int X; public int Y; }

Point p1 = new Point { X = 1, Y = 2 };
Point p2 = p1;      // COPIES the values
p2.X = 99;
Console.WriteLine(p1.X); // 1 — p1 is untouched

// Reference type - class
class Box { public int Value; }

Box b1 = new Box { Value = 1 };
Box b2 = b1;         // COPIES the reference — both point at the SAME object
b2.Value = 99;
Console.WriteLine(b1.Value); // 99 — b1 changed too, because they're the same object
```

**Where this comes up:** this is the single most common C# fundamentals question — get this example exactly right, it comes up constantly.

---

### Q2. What are examples of each?

**Answer:**
"Value types: all the built-in numeric types (`int`, `double`, `decimal`, `bool`), `struct`, and `enum`. Reference types: `class`, `string`, `interface`, `array`, `delegate`. Note that `string` behaves a bit specially — see Q6."

| Value types | Reference types |
|---|---|
| `int`, `float`, `double`, `decimal`, `bool`, `char` | `class` |
| `struct` (including built-in ones like `DateTime`, `Point`) | `string` |
| `enum` | `interface` |
| | `array` (even `int[]`) |
| | `delegate` |

---

### Q3. Where is each stored — stack or heap?

**Answer:**
"As a rule of thumb: value types live on the stack when they're local variables, and reference types always live on the heap, with just the reference itself on the stack. But this isn't a hard rule — a value type that's a field inside a class instance lives on the heap too, as part of that object. The real distinguishing rule isn't stack vs heap, it's copy-by-value vs copy-by-reference."

```csharp
class Container
{
    public int Number; // value type, but stored on the HEAP because it's part of a heap object
}

void Method()
{
    int local = 5;              // value type, local variable — lives on the stack
    Container c = new Container(); // reference — 'c' (the pointer) is on the stack,
                                    // the actual Container object is on the heap
}
```

**Where this comes up as a trick question:** "value types are always on the stack" is a common oversimplification — the accurate answer is about copy semantics, not memory location.

---

### Q4. What happens when you pass a value type vs a reference type into a method?

**Answer:**
"A value type is copied — changes made inside the method don't affect the caller's variable, unless you explicitly pass it with `ref`. A reference type passes the reference by value — so the method gets its own copy of the *reference*, pointing at the same object; changes to the object's fields are visible to the caller, but reassigning the parameter to a new object inside the method is not visible to the caller."

```csharp
void ModifyValue(Point p) { p.X = 100; }        // no effect on caller — p is a copy
void ModifyReference(Box b) { b.Value = 100; }  // caller's object IS changed — same object
void ReassignReference(Box b) { b = new Box(); } // no effect on caller — only the local copy of the reference changed

var point = new Point { X = 1 };
ModifyValue(point);
Console.WriteLine(point.X); // 1 — unchanged

var box = new Box { Value = 1 };
ModifyReference(box);
Console.WriteLine(box.Value); // 100 — changed, same object

ReassignReference(box);
Console.WriteLine(box.Value); // still 100 — reassignment inside the method didn't affect caller's variable
```

---

### Q5. What does "boxing" and "unboxing" mean?

**Answer:**
"Boxing is converting a value type into a reference type — wrapping it in an `object` so it can live on the heap, usually happening implicitly when you assign a value type to an `object` or a non-generic interface. Unboxing is the reverse — extracting the value back out, which requires an explicit cast. Both cost extra allocation/copying, so boxing in a hot loop is a common performance mistake."

```csharp
int number = 5;
object boxed = number;        // boxing - int wrapped into an object on the heap
int unboxed = (int)boxed;     // unboxing - explicit cast required

// Classic accidental-boxing trap: ArrayList stores everything as object
ArrayList list = new ArrayList();
list.Add(5); // boxed automatically
```

**Where to use / avoid:** avoid boxing in performance-sensitive code (e.g., tight loops, high-throughput code) — use generics (`List<int>` instead of `ArrayList`) so value types stay unboxed.

---

### Q6. Why does `string` behave differently from other reference types?

**Answer:**
"`string` is a reference type, but it's immutable — once created, its contents can never change. Any operation that looks like it 'modifies' a string (`.ToUpper()`, `+`, `.Replace()`) actually creates a brand new string object and returns it; the original is untouched. That's why strings can safely *feel* like value types in day-to-day code even though they're allocated on the heap like any other reference type."

```csharp
string s1 = "hello";
string s2 = s1;
s2 = s2.ToUpper(); // creates a NEW string, doesn't mutate the original

Console.WriteLine(s1); // "hello" — unchanged
Console.WriteLine(s2); // "HELLO" — s2 now points at a different object
```

---

### Q7. Can a struct contain reference types? Does that change anything?

**Answer:**
"Yes — a struct's fields can be reference types. Copying the struct still copies the struct itself value-by-value, but any reference-type field inside it copies the *reference*, not the object it points to — so two copies of the struct can end up pointing at the same underlying object for that field."

```csharp
struct Wrapper
{
    public List<int> Items; // reference type field inside a value type
}

Wrapper w1 = new Wrapper { Items = new List<int> { 1, 2, 3 } };
Wrapper w2 = w1;              // struct itself is copied...
w2.Items.Add(4);              // ...but Items is a reference — both share the SAME list

Console.WriteLine(w1.Items.Count); // 4 — w1 sees the change too, because Items is a shared reference
```

**Where this comes up as a trick question:** "structs are always fully independent copies" is only true if every field inside the struct is also a value type — a reference-type field breaks that isolation.

---

### Q8. When would you choose a `struct` over a `class`?

**Answer:**
"Structs make sense for small, simple, immutable-ish data where value semantics are actually what you want — a coordinate, a money amount, a date/time — and where you don't want the overhead of heap allocation and garbage collection for lots of short-lived instances. If the type is large, needs to be mutated and shared, needs inheritance, or is passed around a lot by reference, a class fits better. Microsoft's general guidance is: keep structs small (roughly under 16 bytes) and immutable."

**Where to use:**
- `struct`: small value-like data, e.g. `Point`, `Money`, `DateRange` — especially in performance-sensitive code creating lots of short-lived instances.
- `class`: anything with identity, larger state, needs to be shared/mutated across references, or participates in inheritance.

---

### Quick one-liner if asked to summarize

> "Value types hold their data directly and get copied on assignment/passing — changes to the copy don't affect the original. Reference types hold a pointer to data on the heap, so copying the variable just copies the pointer — both variables end up pointing at the same object, so changes through either one are visible to both. `string` is the odd one out: a reference type, but immutable, so it behaves like a value type in everyday use."
 