# LINQ + Entity Framework Core — Senior-Level Interview Questions — Index

Focused on what actually gets asked at senior level — less "what is LINQ", more "what breaks in production and why." Cross-references [[ienumerable-iqueryable-qa]], [[equality-operator-vs-equals-qa]], and [[deadlock-qa]] / [[database-deadlock-qa]] where relevant instead of repeating that content. Grouped into 7 files.

---

## File 1 — `linq-01-fundamentals-deferred-execution-qa.md`
**LINQ Fundamentals & Deferred Execution (senior angle, not "what is LINQ")**
1. What's the difference between deferred and immediate execution in LINQ, and which operators fall into each category?
   - *Cross-question:* What happens if the underlying collection is modified after a query is defined but before it's enumerated?
2. What is the "multiple enumeration" problem, and why is it a real production bug, not just a style nit?
   - *Cross-question:* Does calling `.Count()` then `.ToList()` on the same `IQueryable` re-run the query twice?
3. Method syntax vs query syntax — is there a real difference, or just style?
4. What's the difference between `Select` and `SelectMany`?
   - *Cross-question:* How would you flatten a `List<List<T>>` into a single `List<T>` using LINQ?
5. `First()`/`Single()` vs their `OrDefault` counterparts — what's the real-world failure mode of picking the wrong one?
   - *Cross-question:* What exception does `Single()` throw if more than one match exists, and why is that different from `First()`?
6. What are `Any()`/`All()`, and why are they almost always better than `.Where(...).Count() > 0`?
7. What is a custom iterator using `yield return`, and how does it relate to deferred execution under the hood?
   - *Cross-question:* If an iterator method throws partway through, when does the exception actually surface to the caller?

## File 2 — `linq-02-advanced-operators-qa.md`
**LINQ Advanced Operators & Techniques**
1. What's the difference between `Join` and `GroupJoin` in LINQ?
2. How does `GroupBy` work internally, and what's a common performance mistake with it against `IQueryable`?
   - *Cross-question:* Does `GroupBy` translate efficiently to SQL in EF Core, or does it sometimes force client-side evaluation?
3. What is `Aggregate()`, and when would you reach for it over `Sum`/`Count`/a loop?
4. What does `Zip()` do, and what's a realistic use case?
5. What's the difference between `Expression<Func<T,bool>>` and `Func<T,bool>` in a LINQ method signature, and why does it matter which one a method accepts?
   - *Cross-question:* Why can't you pass an arbitrary C# method call inside a lambda that needs to become an `Expression<Func<T,bool>>` for EF Core?
6. What is PLINQ (`AsParallel()`), and when is it actually appropriate to use?
   - *Cross-question:* Why is `AsParallel()` almost never the right call directly on an EF Core `IQueryable`?
7. How would you write a custom LINQ extension method, and what's a case where you've actually needed one?
8. `ToLookup()` vs `GroupBy()` — what's actually different? *(new)*
9. `OfType<T>()` vs `Cast<T>()` — filtering vs casting a mixed/polymorphic collection. *(new)*
10. `Union`/`Intersect`/`Except`/`Distinct` — how do they determine whether two elements are "equal"?
    - *Cross-question:* What happens if you call `.Distinct()` on a list of a custom class that hasn't overridden `Equals`/`GetHashCode`? *(cross-reference: [[equality-operator-vs-equals-qa]])*

## File 3 — `linq-03-efcore-core-concepts-qa.md`
**EF Core Core Concepts (senior-level depth)**
1. What is Change Tracking in EF Core, and how does `SaveChanges()` know what to update?
   - *Cross-question:* What does `AsNoTracking()` actually save you, and when could using it break something?
2. Why is `DbContext` registered as Scoped in DI, and what goes wrong if you make it a Singleton?
   - *Cross-question:* What actually happens if two threads share the same `DbContext` instance concurrently?
3. Lazy Loading vs Eager Loading (`Include`) vs Explicit Loading — trade-offs of each?
   - *Cross-question:* What has to be true about your entity classes for lazy loading to even work in EF Core?
4. What is the N+1 query problem in EF Core, and how do you actually spot it happened, after the fact, in production?
5. What's the difference between `Include`/`ThenInclude` producing a single query vs `AsSplitQuery()`?
   - *Cross-question:* Why would splitting one query into several ever be *faster* than a single query with several joins?
6. How do EF Core Migrations work, and what's the danger of running `Database.EnsureCreated()` in production instead?
7. What's a Shadow Property, and why would you use one instead of a real property on the entity class?

## File 4 — `linq-04-efcore-performance-production-qa.md`
**EF Core Performance & Production Concerns**
1. How do you diagnose a slow EF Core query in production — what's your actual process?
   - *Cross-question:* How do you get EF Core to log the generated SQL, and why might the generated SQL differ from what you expected?
2. What is a Compiled Query in EF Core, and when does it actually move the needle?
3. What's the difference between `ExecuteUpdate`/`ExecuteDelete` (EF Core 7+) and loading entities then calling `SaveChanges()`?
   - *Cross-question:* Why does `ExecuteUpdate` bypass change tracking and concurrency tokens entirely?
4. How does optimistic concurrency work in EF Core, and what exception do you catch when a conflict happens?
5. What is an EF Core Interceptor, and what's a real use case for one (e.g., auditing, soft delete, query logging)?
6. What is DbContext Pooling, and what's the catch that makes it unsafe for certain designs?
   - *Cross-question:* What happens to any state stored on the DbContext itself (not just tracked entities) when it's returned to the pool?
7. How does EF Core's connection resiliency / retry-on-failure work, and why can naive retry logic break transactions?
8. How do you implement pagination in EF Core with `Skip`/`Take`, and what's the pitfall with large offsets on big tables?
   - *Cross-question:* How do you get the total record count alongside a paged page of results without running the underlying query twice?
9. What's the best practice for combining a paged data query and a total-count query efficiently in the same endpoint?
10. What are the key techniques for optimizing EF Core queries against tables with millions of rows (projection, `AsNoTracking`, indexing, split queries, keyset pagination)?
    - *Cross-question:* When would you choose keyset/seek pagination over offset-based `Skip`/`Take` in EF Core, and what does that query actually look like?

## File 6 — `linq-06-coding-practice-joins-groupby-qa.md`
**LINQ Coding Practice — Writing Joins, GroupBy & Aggregation Queries**
(Interviewers frequently ask you to actually write these, not just describe them — each question below is answered with a runnable LINQ query.)
1. Write a LINQ query to perform an inner join between two collections (e.g., Employees and Departments).
2. Write a LINQ query for a left outer join — since LINQ has no `LEFT JOIN` keyword, how do you simulate one?
   - *Cross-question:* Why does `GroupJoin` + `SelectMany` + `DefaultIfEmpty()` produce a left join, step by step?
3. Write a LINQ query to join three collections together (e.g., Employees, Departments, Locations).
4. Write a LINQ query for a self-join (employees joined to their own managers).
5. Write a LINQ query to group employees by department and return the count per department.
   - *Cross-question:* What exactly is `g.Key` and `g` itself inside a `GroupBy` result — what are their types?
6. Write a LINQ query to group by department and compute average/max/min salary per group.
7. Write a LINQ query to group by *multiple* columns (e.g., Department + JobTitle).
8. Write a LINQ query that's the equivalent of SQL's `HAVING` — group, then filter the groups themselves.
9. Write a LINQ query to find the top N employees per group, ordered by salary (the LINQ equivalent of the SQL `ROW_NUMBER()`/`DENSE_RANK()` per-group pattern).
10. Write a LINQ query to find duplicate items in a list.
11. Write a LINQ query for a cross join (every combination of two collections).
12. Write one realistic composite LINQ query combining `Where`, `GroupBy`, `OrderBy`, and `Select` together.
    - *Cross-question:* If this were an `IQueryable` against EF Core instead of an in-memory `List<T>`, which parts (if any) would risk not translating to SQL?
13. What's the bug in calling `.Skip(n).Take(m)` without an `OrderBy` first? *(new — classic pagination gotcha)*

## File 5 — `linq-05-efcore-advanced-scenarios-qa.md`
**EF Core Advanced / Scenario-Based**
1. What is a Global Query Filter, and what's a realistic use case (soft delete, multi-tenancy)?
   - *Cross-question:* How do you bypass a global query filter for one specific query when you legitimately need to?
2. What is an Owned Type / value object in EF Core, and how does it map to the database?
3. How would you implement multi-tenancy in EF Core — what are the main strategies and their trade-offs?
4. How do you run raw SQL safely in EF Core (`FromSqlRaw`/`FromSqlInterpolated`), and how do you avoid SQL injection while doing it?
5. How do transactions work across multiple `SaveChanges()` calls, and how would you wrap several EF Core operations in one atomic unit?
   - *Cross-question:* What happens if you mix EF Core's own transaction with a manually-started `ADO.NET` transaction?
6. How would you unit/integration test code that uses EF Core — what are the trade-offs of the InMemory provider vs SQLite vs a real test database?
7. How would you handle a schema migration that needs to run against a live production database with zero downtime? *(cross-reference: [[sql-06-db-objects-scenarios-qa]])*

## File 7 — `linq-07-inheritance-mapping-config-qa.md`
**EF Core Inheritance Mapping & Configuration (new)**
1. What are the three inheritance mapping strategies in EF Core — TPH, TPT, and TPC — and what are the trade-offs of each?
   - *Cross-question:* Why is TPH (the EF Core default) usually the fastest to query, but the "ugliest" schema?
2. What is a Value Converter in EF Core, and when would you write one?
3. Fluent API vs Data Annotations for configuring the model — which wins if both are used on the same property, and which should you actually prefer?
4. What's the difference between DbContext Pooling and ADO.NET connection pooling — are they the same thing?
5. How would you generate a reviewable SQL script from EF Core migrations instead of applying them directly against production?
