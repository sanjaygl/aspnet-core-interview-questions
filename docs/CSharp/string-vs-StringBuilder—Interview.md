# string vs StringBuilder — Interview Q&A

---

### Q1. What's the difference between `string` and `StringBuilder`?

**Answer:**
"`string` is immutable — once created, it can never change. Every operation that looks like it modifies a string (`+`, `.Replace()`, `.ToUpper()`) actually creates a brand new string object in memory and leaves the original untouched. `StringBuilder` is mutable — it maintains an internal, resizable character buffer, so appending or modifying text updates that buffer in place instead of allocating a new object every time."

```csharp
string s = "Hello";
s = s + " World"; // creates a NEW string object, "Hello" is discarded/left for GC

StringBuilder sb = new StringBuilder("Hello");
sb.Append(" World"); // modifies the SAME internal buffer, no new object each time
```

---

### Q2. Why is string immutability a problem in a loop?

**Answer:**
"Because every concatenation allocates a brand-new string and copies over the old content plus the new content. In a loop with N iterations, that's roughly N string allocations, most of them immediately garbage — real memory churn and CPU cost, especially for a large N. `StringBuilder` avoids that by growing an internal buffer instead of reallocating a whole new string every time."

```csharp
// BAD - allocates a new string on every iteration (O(n^2) total work for n appends)
string result = "";
for (int i = 0; i < 10000; i++)
{
    result += i.ToString(); // new string object each time
}

// GOOD - one growing buffer, appends are cheap
StringBuilder sb = new StringBuilder();
for (int i = 0; i < 10000; i++)
{
    sb.Append(i);
}
string result2 = sb.ToString(); // convert to a string once, at the end
```

**Where to use:** any loop or repeated operation building up a string incrementally — logging, building large SQL/HTML/JSON text by hand, report generation.

---

### Q3. If strings are immutable, why does this compile and "work"?
```csharp
string s = "Hello";
s = s.ToUpper();
```

**Answer:**
"Because `s` is just a variable — a reference. `.ToUpper()` doesn't change the original string object; it creates a new string and returns it, and the assignment `s = ...` just makes the variable `s` point at that new object instead. The original `"Hello"` string object still exists unchanged (as long as anything else references it), it's just that `s` no longer points to it."

---

### Q4. When would you NOT use StringBuilder, even in a loop?

**Answer:**
"If the number of concatenations is small and known — a handful of appends — the difference is negligible, and plain string concatenation or interpolation is more readable. Also, if you're just combining a small, fixed number of parts once (not in a loop), `string.Format`, `$"..."` interpolation, or `string.Concat`/`string.Join` are clearer and the compiler can sometimes optimize simple chained `+` operations directly into a single `string.Concat` call anyway."

```csharp
// Fine as-is - only a few concatenations, not in a loop
string message = $"Hello, {firstName} {lastName}! You have {count} new messages.";

// Also fine - joining a known collection
string csv = string.Join(",", names);
```

**Where to use:** reach for `StringBuilder` specifically when you're appending in a loop or an unknown/large number of times — not for a one-off combination of a few values.

---

### Q5. What are the useful `StringBuilder` methods beyond `Append`?

**Answer:**
"`AppendLine` (adds a newline), `Insert` (add at a specific position), `Remove` (delete a range), `Replace` (find-and-replace within the buffer), and `.ToString()` to finally materialize the built-up text as an actual `string` when you're done."

```csharp
var sb = new StringBuilder();
sb.AppendLine("Header");
sb.Append("Row 1");
sb.Insert(0, "*** ");         // insert at position 0
sb.Replace("Row 1", "Row A"); // in-place replace within the buffer
string final = sb.ToString();
```

---

### Q6. Does `StringBuilder` have a fixed size, or does it grow automatically?

**Answer:**
"It grows automatically — internally it doubles its capacity when it runs out of room, similar to how `List<T>` grows. You can optionally pass an initial capacity to the constructor if you have a rough idea of the final size, which avoids some of the resizing/copying overhead as it grows."

```csharp
var sb = new StringBuilder(1024); // pre-size the buffer if you know it'll be roughly this big
```

**Where to use:** when building a large, predictable-size string (e.g., a report or large JSON payload) — pre-sizing avoids repeated internal buffer reallocations.

---

### Q7. Is `StringBuilder` thread-safe?

**Answer:**
"No — it's not synchronized, so if multiple threads append to the same `StringBuilder` instance concurrently, you can get corrupted output or exceptions. If multiple threads need to build text concurrently, each thread should use its own `StringBuilder`, or access needs to be externally synchronized (e.g., with a `lock`)."

---

### Q8. Practical rule of thumb — string vs StringBuilder?

**Answer:**
- A few known concatenations, done once → plain `string` with interpolation (`$"..."`) or `string.Format`/`string.Join`.
- Many concatenations, especially in a loop, or the number of appends is unknown ahead of time → `StringBuilder`, then call `.ToString()` once at the end.
- Need the result as an actual `string` to pass around, compare, or store → convert with `.ToString()` — `StringBuilder` itself isn't a `string` and doesn't support `==` value comparison the way `string` does.

---

### Quick one-liner if asked to summarize

> "`string` is immutable — every 'modification' creates a new object, which gets expensive in a loop. `StringBuilder` is a mutable, resizable character buffer designed exactly for that case — build up text incrementally with `.Append()`, then call `.ToString()` once at the end to get the final immutable `string`."
 