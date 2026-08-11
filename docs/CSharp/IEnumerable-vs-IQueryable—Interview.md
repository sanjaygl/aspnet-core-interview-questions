# IEnumerable vs IQueryable — Interview Q&A

---

### Q1. What is the difference between IEnumerable and IQueryable?

**Answer:**
"IEnumerable is used to work with in-memory data — like a `List` or `Array`. When you filter or query it, the filtering happens in the application's memory.

IQueryable is used to work with out-of-process data — like a database. When you filter or query it, the query gets translated into SQL and runs on the database itself, so only the matching data comes back."

**Where to use:**
- `IEnumerable` → LINQ over collections already in memory (`List<T>`, `T[]`, results you already fetched).
- `IQueryable` → LINQ over `DbSet<T>` / EF Core queries, where you want the database to do the filtering.

```csharp
// IEnumerable - filters IN MEMORY
IEnumerable<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
var evens = numbers.Where(n => n % 2 == 0); // runs in C#, on the list already in RAM

// IQueryable - filters ON THE DATABASE
IQueryable<Customer> customers = dbContext.Customers;
var adults = customers.Where(c => c.Age >= 18); // becomes SQL: WHERE Age >= 18
```

---

### Q2. If I write the same `.Where()` on both, what's actually different under the hood?

**Answer:**
"IEnumerable's LINQ methods take a compiled `Func<T,bool>` delegate — normal C# code that just runs. IQueryable's LINQ methods take an `Expression<Func<T,bool>>` — that's not compiled code, it's a tree describing what the lambda *means*. A provider like EF Core reads that tree and converts it into SQL."

```csharp
Func<Customer, bool> f = c => c.Age >= 18;                 // compiled code
Expression<Func<Customer, bool>> e = c => c.Age >= 18;      // data describing the logic
```

**Where this comes up:** whenever someone asks "why can't I call my own C# helper method inside an EF Core `.Where()`?" — see Q4.

---

### Q3. Is the query executed immediately when you write `.Where()`?

**Answer:**
"No — both are lazy / deferred. Writing `.Where()`, `.Select()`, `.OrderBy()` just builds up the query. Nothing actually runs until you enumerate it — with a `foreach`, or by calling `.ToList()`, `.First()`, `.Count()`, etc."

```csharp
var query = dbContext.Orders.Where(o => o.Total > 100); // no DB call yet
query = query.OrderBy(o => o.Date);                       // still no DB call

var results = query.ToList();  // <-- DB call happens HERE
```

**Where to use:** keep chaining filters/sorts as `IQueryable` for as long as possible, and only call `.ToList()` at the very end, right before you need the actual data (e.g., to return from an API, bind to a UI).

---

### Q4. Why did my EF Core query throw an error saying it "could not be translated"?

**Answer:**
"Because IQueryable has to convert my lambda into SQL. If I call a custom C# method or something the SQL translator doesn't recognize, EF Core can't turn it into SQL and throws at runtime."

```csharp
// Throws InvalidOperationException — EF Core doesn't know how to translate MyHelper.IsVip
context.Customers.Where(c => MyHelper.IsVip(c));

// Fix: pull the query to memory first with AsEnumerable(), then apply custom C# logic
context.Customers
    .Where(c => c.Age >= 18)     // still SQL, still efficient
    .AsEnumerable()               // switch to LINQ to Objects from here
    .Where(c => MyHelper.IsVip(c)); // now plain C#, runs in memory
```

**Where to use `AsEnumerable()`:** when you need complex C# logic in a filter that the DB provider can't translate — but only switch after you've already filtered down as much as possible on the DB side.

---

### Q5. What's the classic mistake developers make with IQueryable?

**Answer:**
"Declaring or passing a query as `IEnumerable<T>` too early. Once that happens, any `.Where()` you write afterward binds to `Enumerable.Where` (LINQ to Objects) instead of `Queryable.Where`, so EF Core is forced to pull the entire table into memory before filtering."

```csharp
// BAD - parameter type IEnumerable forces full table load
public IEnumerable<Customer> GetAdults(IEnumerable<Customer> customers) =>
    customers.Where(c => c.Age >= 18);

GetAdults(dbContext.Customers); // pulls ALL customers into memory first, then filters

// GOOD - keep it IQueryable so the filter is translated to SQL
public IQueryable<Customer> GetAdults(IQueryable<Customer> customers) =>
    customers.Where(c => c.Age >= 18);
```

**Where to use:** keep method signatures as `IQueryable<T>` throughout your data-access layer; only convert to `IEnumerable`/`List` at the boundary, right before returning to the caller.

---

### Q6. Does calling `.ToList()` early in a chain matter?

**Answer:**
"Yes. Everything before `.ToList()` runs as SQL. Everything after `.ToList()` runs in memory on a plain `List<T>`. If you call `.ToList()` too soon, you lose paging, filtering, and sorting on the database side."

```csharp
// BAD - loads the whole Orders table, THEN filters/pages in memory
context.Orders.ToList().Where(o => o.Total > 100).Take(10);

// GOOD - filtering and paging happen in SQL, only 10 rows come back
context.Orders.Where(o => o.Total > 100).OrderBy(o => o.Date).Take(10).ToList();
```

**Where to use:** always call `.ToList()`/`.ToArray()` as the very last step, after all filtering/sorting/paging is applied.

---

### Q7. If I call `.Count()` and then `.ToList()` on the same query, does it hit the database twice?

**Answer:**
"Yes — each one is a separate enumeration, so it's a separate database round trip. If I need both, I should materialize the results once with `.ToList()` and then get the count from that list."

```csharp
var query = dbContext.Orders.Where(o => o.Total > 100);

// BAD - 2 round trips
var count = query.Count();
var list = query.ToList();

// GOOD - 1 round trip
var list = query.ToList();
var count = list.Count;
```

**Where to use:** anytime you need multiple pieces of information (count + data, exists + data) from the same query — materialize once, reuse the in-memory result.

---

### Q8. Why does IQueryable inherit from IEnumerable?

**Answer:**
"So that anywhere code just needs to iterate with `foreach`, it doesn't matter whether the source is a database query or an in-memory list — an `IQueryable<T>` can be used wherever `IEnumerable<T>` is expected. The tradeoff is that once you treat it as `IEnumerable`, you lose the ability to keep composing SQL-translatable queries on it."

---

### Q9. Is IQueryable always faster than IEnumerable?

**Answer:**
"No. IQueryable is only better when the data source is remote — like a database — because it lets the filtering happen there instead of pulling everything across the network first. For an in-memory collection that's already small, wrapping it in IQueryable just adds overhead for no benefit — I'd just use IEnumerable / LINQ to Objects."

**Where to use:**
- Small in-memory list → `IEnumerable`.
- Large remote data source (DB, API with an IQueryable provider) → `IQueryable`.

---

### Q10. What's the N+1 problem, and how does it relate to IQueryable?

**Answer:**
"It happens when you enumerate a query and then, for each row, run another query — for example, lazy-loading a related entity inside a `foreach` loop. Instead of 1 query, you end up with 1 + N queries. The fix is to eager load with `.Include()`, or project exactly what you need with `.Select()`, so it all becomes one SQL query."

```csharp
// BAD - N+1: one query for orders, then one more query per order for Customer
foreach (var order in dbContext.Orders.ToList())
{
    Console.WriteLine(order.Customer.Name); // lazy-loads Customer per order
}

// GOOD - one query total
var orders = dbContext.Orders.Include(o => o.Customer).ToList();
```

---

### Q11. How would you unit test code that uses IQueryable without hitting a real database?

**Answer:**
"I'd use `List<T>.AsQueryable()` to fake an `IQueryable` source in memory for testing the query composition logic. I'd keep in mind that it doesn't validate whether the query is actually translatable to SQL — for that, I'd use EF Core's InMemory or SQLite in-memory provider instead, since those still go through real query translation."

```csharp
var fakeCustomers = new List<Customer> { new Customer { Age = 20 } }.AsQueryable();
var repo = new CustomerRepository(fakeCustomers);
```

---

### Quick one-liner if asked to summarize

> "IEnumerable runs queries in memory using compiled code. IQueryable builds an expression tree that gets translated into SQL and runs on the database — so IQueryable pushes the work to the data source, IEnumerable always does the work locally."

 