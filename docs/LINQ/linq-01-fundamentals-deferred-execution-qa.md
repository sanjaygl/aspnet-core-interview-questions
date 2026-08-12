# LINQ — Fundamentals & Deferred Execution — Senior Interview Q&A

For the IEnumerable/IQueryable execution model itself (expression trees vs delegates, SQL translation), see [[ienumerable-iqueryable-qa]] — this file assumes that background and focuses on operator-level behavior.

---

### Q1. What's the difference between deferred and immediate execution in LINQ, and which operators fall into each category?

**Answer:**
"Deferred operators — `Where`, `Select`, `OrderBy`, `GroupBy`, `Join`, etc. — don't run anything when you call them; they just build up a query definition. The actual work happens only when the result is enumerated (`foreach`, or a call to an immediate operator). Immediate operators — `ToList()`, `ToArray()`, `Count()`, `First()`, `Sum()`, etc. — force execution right then and return a concrete result."

```csharp
var query = numbers.Where(n => n > 5);   // nothing executes yet — just builds the query
var list = query.ToList();                // NOW it actually runs
```

**Cross-question: What happens if the underlying collection is modified after a query is defined but before it's enumerated?**
"Because the query hasn't run yet, it sees whatever the collection looks like *at enumeration time*, not at definition time. This can produce surprising results if you don't expect it."

```csharp
var numbers = new List<int> { 1, 2, 3 };
var query = numbers.Where(n => n > 1); // not executed yet

numbers.Add(10);

foreach (var n in query) Console.WriteLine(n); // prints 2, 3, 10 — the Add() IS reflected
```

**Where this comes up:** a classic bug is capturing a query, mutating the source, then being surprised the "already defined" query reflects the mutation — because it was never actually run until the `foreach`.

---

### Q2. What is the "multiple enumeration" problem, and why is it a real production bug, not just a style nit?

**Answer:**
"If a deferred query is enumerated more than once, it re-runs its entire pipeline each time — for `IEnumerable`, that means re-executing whatever logic built the source (potentially an expensive computation); for `IQueryable` backed by EF Core, it means a *separate database round trip* each time. It's not just wasted CPU — it can silently produce different results across enumerations if the underlying data changed in between, which is a correctness bug, not just a performance one."

```csharp
IQueryable<Order> query = dbContext.Orders.Where(o => o.Total > 100);

var count = query.Count();   // DB round trip #1
var list = query.ToList();    // DB round trip #2 — same logical query, executed twice

// Fix: materialize once, reuse the in-memory result
var orders = query.ToList();
var count2 = orders.Count;
```

**Cross-question: Does calling `.Count()` then `.ToList()` on the same `IQueryable` re-run the query twice?**
"Yes — each is a separate terminal/immediate operation, so each triggers its own full execution against the database. There's no caching between them unless you materialize the result yourself first."

---

### Q3. Method syntax vs query syntax — is there a real difference, or just style?

**Answer:**
"Functionally, query syntax (`from x in y select x`) is compiled by the C# compiler into the exact same method calls as method syntax (`y.Select(x => x)`) — there's no runtime difference. The practical difference is expressiveness: query syntax reads more naturally for multi-step joins/grouping with `let` clauses, but doesn't have syntax for every LINQ operator (no query-syntax equivalent for `Aggregate`, `Any`, etc.) — you often end up mixing both, or falling back to method syntax entirely once the query needs something query syntax doesn't support."

```csharp
// Query syntax
var result1 = from o in orders where o.Total > 100 select o.CustomerId;

// Method syntax — compiles to the same thing
var result2 = orders.Where(o => o.Total > 100).Select(o => o.CustomerId);
```

---

### Q4. What's the difference between `Select` and `SelectMany`?

**Answer:**
"`Select` transforms each element into something else, one-to-one — if the source has 5 items, the result has 5 items. `SelectMany` is for when each element itself projects to a *sequence*, and you want all those inner sequences flattened into one single, flat sequence — the total count depends on how many items each inner sequence produced, not the original count."

```csharp
var customers = new List<Customer> {
    new Customer { Orders = new List<Order> { order1, order2 } },
    new Customer { Orders = new List<Order> { order3 } }
};

var perCustomerOrderLists = customers.Select(c => c.Orders);        // IEnumerable<List<Order>> — nested
var allOrdersFlat = customers.SelectMany(c => c.Orders);             // IEnumerable<Order> — flattened, 3 total
```

**Cross-question: How would you flatten a `List<List<T>>` into a single `List<T>` using LINQ?**
"`SelectMany(x => x)` — the selector just returns each inner list as-is, and `SelectMany` flattens them together."

```csharp
List<List<int>> nested = new() { new() { 1, 2 }, new() { 3, 4, 5 } };
List<int> flat = nested.SelectMany(x => x).ToList(); // [1, 2, 3, 4, 5]
```

---

### Q5. `First()`/`Single()` vs their `OrDefault` counterparts — what's the real-world failure mode of picking the wrong one?

**Answer:**
"`First()` returns the first matching element or throws if there are none. `FirstOrDefault()` returns `default(T)` (usually `null` for reference types) instead of throwing if there's no match. `Single()` requires *exactly one* match — it throws if there are zero **or** more than one. `SingleOrDefault()` allows zero (returns default) but still throws if there's more than one. Picking `First()` when you actually expect exactly one match hides a data-integrity bug (duplicate rows) by silently taking the first one; picking `Single()` when zero results is a normal, expected case causes unnecessary crashes on a legitimately empty result."

```csharp
var user = users.Single(u => u.Email == email);
// throws InvalidOperationException if there are ZERO matches — is that really what you want
// for "look up a user that might not exist"? Probably should be SingleOrDefault.

var firstAdmin = users.First(u => u.Role == "Admin");
// if there are accidentally 2 admins, this silently picks one and hides the duplicate — a Single() would have caught it
```

**Cross-question: What exception does `Single()` throw if more than one match exists, and why is that different from `First()`?**
"`InvalidOperationException` — 'Sequence contains more than one matching element.' `First()` never throws for that reason at all; it just takes the first one and ignores the rest. That's exactly the risk: `Single()` is the right choice specifically because it *fails loudly* on an unexpected duplicate, where `First()` would silently mask it."

---

### Q6. What are `Any()`/`All()`, and why are they almost always better than `.Where(...).Count() > 0`?

**Answer:**
"`Any()` returns true if at least one element matches (or, with no predicate, if the sequence has any elements at all); `All()` returns true only if every element matches. Both short-circuit — `Any()` stops as soon as it finds one match, `All()` stops as soon as it finds one non-match. `.Where(...).Count() > 0` has to enumerate the *entire* sequence to produce an exact count, even though all you actually needed to know was 'is there at least one' — wasteful, especially against a large in-memory collection or an `IQueryable`, where `Any()` translates to a cheap `EXISTS` in SQL instead of a full `COUNT`."

```csharp
// Wasteful — counts everything just to check > 0
bool hasOverdueOrders = orders.Where(o => o.IsOverdue).Count() > 0;

// Efficient — stops at the first match; against EF Core, translates to SQL EXISTS
bool hasOverdueOrders2 = orders.Any(o => o.IsOverdue);
```

---

### Q7. What is a custom iterator using `yield return`, and how does it relate to deferred execution under the hood?

**Answer:**
"`yield return` lets you write a method that returns `IEnumerable<T>` without building the whole collection upfront — the compiler transforms the method into a state machine that produces one value at a time, resuming from where it left off each time `MoveNext()` is called. This is exactly the same mechanism deferred LINQ operators are built on internally — `Where`/`Select` are themselves implemented as iterator methods using `yield return`."

```csharp
IEnumerable<int> GetEvenNumbers(IEnumerable<int> source)
{
    foreach (var n in source)
    {
        if (n % 2 == 0)
            yield return n; // execution pauses here, resumes on the next MoveNext() call
    }
}
```

**Cross-question: If an iterator method throws partway through, when does the exception actually surface to the caller?**
"Not when the method is called — the method body doesn't run at all until the first `MoveNext()` (i.e., the first iteration of a `foreach`). The exception surfaces at whichever `MoveNext()` call reaches the throwing line, which could be several iterations into consuming the sequence, not at the point where the method was originally invoked. This is the same deferred-execution surprise as Q1, just at the custom-iterator level."

```csharp
IEnumerable<int> RiskyIterator()
{
    yield return 1;
    yield return 2;
    throw new InvalidOperationException("boom");
}

var it = RiskyIterator(); // does NOT throw here — nothing has run yet
foreach (var x in it) Console.WriteLine(x); // throws only after printing 1 and 2
```
