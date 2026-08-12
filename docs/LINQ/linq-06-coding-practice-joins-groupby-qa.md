# LINQ — Coding Practice: Joins, GroupBy & Aggregation — Interview Q&A

Assumed model for these examples:
```csharp
record Employee(int Id, string Name, int DepartmentId, int? ManagerId, decimal Salary, string JobTitle);
record Department(int Id, string Name);
record Location(int Id, string City);
```

---

### Q1. Write a LINQ query to perform an inner join between two collections.

**Answer:**
"`join` in query syntax, or `.Join()` in method syntax — matches employees to their department by `DepartmentId`."

```csharp
// Query syntax
var result = from e in employees
             join d in departments on e.DepartmentId equals d.Id
             select new { e.Name, DepartmentName = d.Name };

// Method syntax
var result2 = employees.Join(departments,
    e => e.DepartmentId, d => d.Id,
    (e, d) => new { e.Name, DepartmentName = d.Name });
```

---

### Q2. Write a LINQ query for a left outer join.

**Answer:**
"LINQ has no `LEFT JOIN` keyword — you build it from `GroupJoin` (which keeps every left-side item, matched or not) followed by `SelectMany` with `DefaultIfEmpty()` to flatten it back out, substituting a default value for employees with no matching department."

```csharp
var result = from e in employees
             join d in departments on e.DepartmentId equals d.Id into deptGroup
             from d in deptGroup.DefaultIfEmpty()
             select new { e.Name, DepartmentName = d?.Name ?? "No Department" };

// Method syntax equivalent
var result2 = employees
    .GroupJoin(departments, e => e.DepartmentId, d => d.Id, (e, deptGroup) => new { e, deptGroup })
    .SelectMany(x => x.deptGroup.DefaultIfEmpty(), (x, d) => new { x.e.Name, DepartmentName = d?.Name ?? "No Department" });
```

**Cross-question: Why does `GroupJoin` + `SelectMany` + `DefaultIfEmpty()` produce a left join, step by step?**
"`GroupJoin` first produces one result per left item, each paired with its *entire* group of matches (which can be empty for an unmatched employee — nothing is dropped at this stage, unlike a plain `Join`). `SelectMany` then flattens each of those groups back into individual rows. `DefaultIfEmpty()` is the key piece — called on each group before flattening, it ensures that if a group is empty, it's treated as a sequence containing one `default(T)` (`null` for a reference type) instead of a truly empty sequence — so `SelectMany` still produces exactly one output row for that employee, with `null` standing in for the missing department, instead of the employee disappearing entirely."

---

### Q3. Write a LINQ query to join three collections together.

**Answer:**
"Chain additional `join` clauses — each one joins against the result so far, same as chaining multiple `JOIN`s in SQL."

```csharp
var result = from e in employees
             join d in departments on e.DepartmentId equals d.Id
             join l in locations on d.Id equals l.Id // assuming Department has a matching LocationId shape for this example
             select new { e.Name, d.Name, l.City };
```

---

### Q4. Write a LINQ query for a self-join (employees joined to their own managers).

**Answer:**
"Join the `employees` collection to itself, matching `ManagerId` on one side to `Id` on the other — exactly the same idea as a SQL self join, just against an in-memory/queryable sequence instead of a table."

```csharp
var result = from e in employees
             join m in employees on e.ManagerId equals m.Id into managerGroup
             from m in managerGroup.DefaultIfEmpty() // left join, since not everyone has a manager
             select new { Employee = e.Name, Manager = m?.Name ?? "No Manager" };
```

---

### Q5. Write a LINQ query to group employees by department and return the count per department.

**Answer:**
"`GroupBy` produces one `IGrouping<TKey, TElement>` per distinct key; `g.Key` is the department ID, and `g.Count()` counts the elements within that group."

```csharp
var result = employees
    .GroupBy(e => e.DepartmentId)
    .Select(g => new { DepartmentId = g.Key, EmployeeCount = g.Count() });
```

**Cross-question: What exactly is `g.Key` and `g` itself inside a `GroupBy` result — what are their types?**
"`g` is an `IGrouping<TKey, TElement>` — which itself implements `IEnumerable<TElement>`, so you can enumerate all the elements in that group directly (`foreach (var e in g)`), or call any LINQ operator on it (`g.Count()`, `g.Average(x => x.Salary)`). `g.Key` is a property on that grouping holding the key value the group was formed by — here, `int` (the `DepartmentId`)."

---

### Q6. Write a LINQ query to group by department and compute average/max/min salary per group.

**Answer:**
"Once you have each department's group (`g`), call the aggregate LINQ methods directly on it."

```csharp
var result = employees
    .GroupBy(e => e.DepartmentId)
    .Select(g => new
    {
        DepartmentId = g.Key,
        AverageSalary = g.Average(e => e.Salary),
        MaxSalary = g.Max(e => e.Salary),
        MinSalary = g.Min(e => e.Salary)
    });
```

---

### Q7. Write a LINQ query to group by multiple columns.

**Answer:**
"Group by an anonymous type (or a tuple) combining however many columns you need — the grouping key becomes that composite value, and two rows are only in the same group if every part of the key matches."

```csharp
var result = employees
    .GroupBy(e => new { e.DepartmentId, e.JobTitle })
    .Select(g => new { g.Key.DepartmentId, g.Key.JobTitle, Count = g.Count() });
```

---

### Q8. Write a LINQ query that's the equivalent of SQL's `HAVING` — group, then filter the groups themselves.

**Answer:**
"Add a `.Where()` *after* the `GroupBy`, filtering on the group's aggregate result — same logical position as `HAVING` runs after `GROUP BY` in SQL."

```csharp
var result = employees
    .GroupBy(e => e.DepartmentId)
    .Where(g => g.Count() > 5) // this is the "HAVING" — filtering the GROUPS, not the individual employees
    .Select(g => new { DepartmentId = g.Key, EmployeeCount = g.Count() });
```

---

### Q9. Write a LINQ query to find the top N employees per group, ordered by salary.

**Answer:**
"For each group, order its members and take the top N with `.Take()` — the direct LINQ equivalent of the SQL `ROW_NUMBER()`/`DENSE_RANK() ... PARTITION BY` pattern for 'top N per group.'"

```csharp
var result = employees
    .GroupBy(e => e.DepartmentId)
    .SelectMany(g => g.OrderByDescending(e => e.Salary).Take(3)); // top 3 earners per department

// With explicit rank, if the rank value itself is needed downstream:
var withRank = employees
    .GroupBy(e => e.DepartmentId)
    .SelectMany(g => g.OrderByDescending(e => e.Salary)
        .Select((e, index) => new { e.Name, e.DepartmentId, Rank = index + 1 }))
    .Where(x => x.Rank <= 3);
```

**Where this comes up as a trick question:** against EF Core/`IQueryable`, this exact "top N per group" pattern is one of the places LINQ historically struggled to translate directly — `GroupBy().SelectMany(g => g.Take(N))` may not translate to efficient SQL depending on the EF Core version; check the generated SQL, and if needed, fall back to a raw SQL query using `ROW_NUMBER()`/`DENSE_RANK()` instead.

---

### Q10. Write a LINQ query to find duplicate items in a list.

**Answer:**
"Group by whatever defines a duplicate, then filter for groups with more than one member — the LINQ mirror of the SQL `GROUP BY ... HAVING COUNT(*) > 1` pattern."

```csharp
var duplicateEmails = employees
    .GroupBy(e => e.Email)
    .Where(g => g.Count() > 1)
    .Select(g => new { Email = g.Key, Occurrences = g.Count() });
```

---

### Q11. Write a LINQ query for a cross join.

**Answer:**
"A cross join pairs every element of one sequence with every element of another, with no matching condition at all — in query syntax, this is simply two `from` clauses with no `join`/`where` connecting them. `SelectMany` with a selector ignoring the outer item's relationship to the inner does the same thing in method syntax."

```csharp
var result = from e in employees
             from d in departments // no join condition at all - every combination
             select new { e.Name, d.Name };

// Method syntax
var result2 = employees.SelectMany(e => departments, (e, d) => new { e.Name, d.Name });
```

---

### Q12. Write one realistic composite LINQ query combining `Where`, `GroupBy`, `OrderBy`, and `Select` together.

**Answer:**
"A realistic report-style query: active employees, grouped by department, with average salary, only departments with more than 3 active employees, sorted by average salary descending."

```csharp
var report = employees
    .Where(e => e.IsActive)
    .GroupBy(e => e.DepartmentId)
    .Where(g => g.Count() > 3)
    .Select(g => new { DepartmentId = g.Key, AvgSalary = g.Average(e => e.Salary), Count = g.Count() })
    .OrderByDescending(x => x.AvgSalary);
```

**Cross-question: If this were an `IQueryable` against EF Core instead of an in-memory `List<T>`, which parts (if any) would risk not translating to SQL?**
"This particular shape — `Where`, `GroupBy` on a simple column, `Where` on the group (HAVING-equivalent), `Select` with standard aggregates (`Average`, `Count`), and `OrderBy` — all translate cleanly to standard SQL (`WHERE`, `GROUP BY`, `HAVING`, `ORDER BY`) in modern EF Core. The risk zones would be if the `Select` inside the group used something non-standard (string concatenation across group members, a custom C# method call, or nested `SelectMany`/`Take` per group as in Q9) — those are the patterns worth double-checking against the actual generated SQL rather than assuming they translate."

---

### Q13. What's the bug in calling `.Skip(n).Take(m)` without an `OrderBy` first?

**Answer:**
"Without an explicit `OrderBy`, the sequence's order is undefined — a relational database has no inherent 'natural order' for a table, so `Skip`/`Take` without a defined sort can return rows in a different order (or even different rows landing in different pages) on separate executions, especially as the underlying data changes between calls. This shows up as flaky, hard-to-reproduce bugs: 'page 2 sometimes shows a row that was already on page 1,' or `Skip`/`Take` results that look consistent in a small dev database but misbehave once the table is large enough that the database doesn't happen to return rows in insertion order anymore."

```csharp
// BAD - no defined order, Skip/Take results aren't guaranteed stable across calls
var page = dbContext.Orders.Skip(20).Take(10).ToList();

// GOOD - explicit, deterministic order makes Skip/Take actually meaningful and repeatable
var page2 = dbContext.Orders.OrderBy(o => o.OrderId).Skip(20).Take(10).ToList();
```

**Where to use:** always pair `Skip`/`Take` with an `OrderBy` on a column (ideally unique, or a unique tie-breaker added) — this applies to LINQ over in-memory collections too, not just EF Core, though the practical consequences are usually more visible against a database.
