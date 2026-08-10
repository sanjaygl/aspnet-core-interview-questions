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