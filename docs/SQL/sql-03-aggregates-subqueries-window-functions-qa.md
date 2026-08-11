# SQL — Aggregates & Grouping + Subqueries & CTEs + Window Functions — Interview Q&A

---

### Q1. What's the difference between `COUNT(*)`, `COUNT(1)`, and `COUNT(column)`?

**Answer:**
"`COUNT(*)` counts every row, regardless of any column's contents. `COUNT(1)` counts every row too — the `1` is just a constant expression evaluated per row, so it behaves identically to `COUNT(*)` in modern SQL Server (the optimizer treats them the same; there's no real performance difference despite old folklore claiming otherwise). `COUNT(column)` only counts rows where that specific column is *not* `NULL` — this is the one with actually different behavior."

```sql
SELECT COUNT(*) FROM Employees;              -- total row count
SELECT COUNT(1) FROM Employees;               -- same as COUNT(*) in SQL Server
SELECT COUNT(MiddleName) FROM Employees;      -- only counts rows where MiddleName IS NOT NULL
```

**Cross-question: Does `COUNT(column)` count NULL values?**
"No — that's the entire point of the distinction. If 100 employees exist but 30 have a `NULL` `MiddleName`, `COUNT(MiddleName)` returns 70, while `COUNT(*)` returns 100."

---

### Q2. How does `GROUP BY` work, and what are the rules for columns in `SELECT`?

**Answer:**
"`GROUP BY` collapses rows sharing the same value(s) in the grouped column(s) into a single output row per group. The rule: every column in `SELECT` must either be part of the `GROUP BY` list, or wrapped in an aggregate function (`SUM`, `COUNT`, `MAX`, etc.) — SQL Server enforces this and throws a compile error otherwise, because for a non-aggregated, non-grouped column, there's no single well-defined value to return per group (which of the many rows' values should it show?)."

```sql
SELECT DepartmentId, COUNT(*) AS EmpCount, AVG(Salary) AS AvgSalary
FROM Employees
GROUP BY DepartmentId; -- DepartmentId is grouped, the rest are aggregated - valid

-- SELECT DepartmentId, EmployeeName, COUNT(*) FROM Employees GROUP BY DepartmentId;
-- COMPILE ERROR - EmployeeName isn't grouped or aggregated, ambiguous which employee's name to show
```

---

### Q3. What's the difference between `GROUP BY` and `DISTINCT`?

**Answer:**
"`DISTINCT` removes duplicate rows from the result set based on all selected columns — it's about de-duplicating output. `GROUP BY` is about aggregating rows into groups, typically used *with* aggregate functions to compute something per group. If you use `GROUP BY` with no aggregate functions at all, it behaves the same as `DISTINCT` on those columns — but `GROUP BY` is the wrong tool if all you want is simple de-duplication; `DISTINCT` says that intent more clearly."

```sql
SELECT DISTINCT DepartmentId FROM Employees;         -- unique department IDs, nothing else
SELECT DepartmentId FROM Employees GROUP BY DepartmentId; -- same result, but implies aggregation intent
SELECT DepartmentId, COUNT(*) FROM Employees GROUP BY DepartmentId; -- GROUP BY actually earning its keep
```

---

### Q4. Why can't you use an aggregate function directly in a `WHERE` clause?

**Answer:**
"Because of the logical execution order — `WHERE` runs *before* `GROUP BY`/aggregation happens, so at the point `WHERE` is evaluated, there's no aggregated value yet to compare against. `HAVING` exists specifically to filter on aggregate results, since it runs *after* grouping. This is the same underlying reason column aliases from `SELECT` can't be used in `WHERE` — both are about `WHERE` executing too early in the pipeline for that value to exist yet."

```sql
-- SELECT DepartmentId FROM Employees WHERE COUNT(*) > 5 GROUP BY DepartmentId;
-- COMPILE ERROR - COUNT(*) doesn't exist yet when WHERE runs

SELECT DepartmentId FROM Employees GROUP BY DepartmentId HAVING COUNT(*) > 5; -- correct
```

---

### Q5. What are the different types of Subqueries?

**Answer:**
"Scalar subquery returns a single value (one row, one column) and can be used anywhere a single value is expected. Single-row/multi-row subqueries return one or more rows, typically used with `IN`, `ANY`, `ALL`. Correlated subqueries reference a column from the outer query, so they logically run once per outer row (see Q6). Non-correlated subqueries are self-contained and can run independently of the outer query."

```sql
-- Scalar
SELECT Name, Salary, (SELECT AVG(Salary) FROM Employees) AS CompanyAvg FROM Employees;

-- Multi-row, non-correlated
SELECT Name FROM Employees WHERE DepartmentId IN (SELECT DepartmentId FROM Departments WHERE Region = 'West');
```

---

### Q6. What's the difference between a Correlated Subquery and a regular subquery?

**Answer:**
"A correlated subquery references a column from the outer query inside its own `WHERE` clause, which means it can't be evaluated independently — its result depends on which outer row is currently being processed. A regular (non-correlated) subquery is fully self-contained and could be run on its own, producing the same result regardless of the outer query."

```sql
-- Correlated - "e" (outer row) is referenced inside the subquery
SELECT e.Name FROM Employees e
WHERE e.Salary > (SELECT AVG(e2.Salary) FROM Employees e2 WHERE e2.DepartmentId = e.DepartmentId);

-- Non-correlated - self-contained, no reference to the outer query
SELECT Name FROM Employees WHERE Salary > (SELECT AVG(Salary) FROM Employees);
```

**Cross-question: Does a correlated subquery execute once, or once per outer row — and what's the performance cost vs a JOIN?**
"Conceptually, once per outer row — the database re-evaluates the subquery for each row of the outer query, using that row's value. In practice, a good optimizer often rewrites it into a JOIN-like plan internally for better performance, but you can't rely on that always happening — a correlated subquery *can* genuinely execute row-by-row and be much slower than an equivalent JOIN or window function, especially on large tables. It's worth checking the execution plan rather than assuming the optimizer will always save you."

---

### Q7. CTE vs Subquery — why use one over the other?

**Answer:**
"Functionally, a CTE (`WITH name AS (...)`) and a subquery can often express the same logic. The advantage of a CTE is readability and reusability within the same query — you name it once at the top and can reference it multiple times below, instead of repeating (or deeply nesting) the same subquery. CTEs also make recursive queries possible, which a plain subquery can't do."

```sql
WITH DeptAverages AS (
    SELECT DepartmentId, AVG(Salary) AS AvgSalary FROM Employees GROUP BY DepartmentId
)
SELECT e.Name, d.AvgSalary
FROM Employees e
JOIN DeptAverages d ON d.DepartmentId = e.DepartmentId
WHERE e.Salary > d.AvgSalary;
```

---

### Q8. What is a Recursive CTE?

**Answer:**
"A CTE that references itself, used for hierarchical or graph-like data — like an org chart, a category tree, or a bill of materials. It has two parts: an 'anchor' query (the starting point, e.g., the top-level manager with no manager above them) and a 'recursive' member that joins back to the CTE itself, repeating until no more rows are produced."

```sql
WITH OrgChart AS (
    -- Anchor: top-level employees (no manager)
    SELECT EmployeeId, Name, ManagerId, 0 AS Level
    FROM Employees WHERE ManagerId IS NULL

    UNION ALL

    -- Recursive member: join back to OrgChart itself, one level down each time
    SELECT e.EmployeeId, e.Name, e.ManagerId, oc.Level + 1
    FROM Employees e
    JOIN OrgChart oc ON e.ManagerId = oc.EmployeeId
)
SELECT * FROM OrgChart ORDER BY Level;
```

**Where to use:** any self-referencing hierarchy — org charts, folder/category trees, part-of/component breakdowns.

---

### Q9. What's the difference between a CTE and a Temp Table / Table Variable?

**Answer:**
"A CTE exists only for the duration of the single statement it's defined in — it's not materialized as a separate physical object, and its definition is (usually) inlined into the query plan each time it's referenced. A temp table (`#Temp`) is a real, physical table in `tempdb` — it persists for the session/connection, can have its own indexes added, and can be reused across multiple statements. A table variable (`@Table`) is similar to a temp table but scoped to the batch/procedure, with somewhat different optimizer statistics behavior (historically, fewer/rougher statistics than a temp table, though this has improved in recent SQL Server versions)."

```sql
-- CTE - single statement only
WITH RecentOrders AS (SELECT * FROM Orders WHERE OrderDate > '2026-01-01')
SELECT * FROM RecentOrders WHERE Total > 100;

-- Temp table - persists across multiple statements in the session, can be indexed
SELECT * INTO #RecentOrders FROM Orders WHERE OrderDate > '2026-01-01';
CREATE INDEX IX_Temp ON #RecentOrders(Total);
SELECT * FROM #RecentOrders WHERE Total > 100;
```

**Where to use:** CTE for readability within one query; temp table when you need to reuse the intermediate result across several statements, or need to index it for a large/complex intermediate set.

---

### Q10. How are Window Functions different from Aggregate Functions?

**Answer:**
"An aggregate function (`SUM`, `AVG`, `COUNT` with `GROUP BY`) collapses multiple rows into one output row per group. A window function computes a value across a set of related rows (the 'window', defined by `OVER (...)`) but still returns one output row *per original row* — nothing gets collapsed. That's what makes window functions perfect for things like 'this row's value, plus a running total up to this row' — impossible to express with a plain `GROUP BY` aggregate, since that would collapse away the individual rows."

```sql
-- Aggregate - collapses to one row per department
SELECT DepartmentId, AVG(Salary) FROM Employees GROUP BY DepartmentId;

-- Window function - keeps every employee row, adds the department average alongside it
SELECT Name, DepartmentId, Salary, AVG(Salary) OVER (PARTITION BY DepartmentId) AS DeptAvg
FROM Employees;
```

**Cross-question: Can you use a window function result directly in a `WHERE` clause?**
"No — `WHERE` runs before window functions are evaluated in the logical execution order (window functions are computed roughly alongside `SELECT`). To filter on a window function's result, wrap the query in a subquery or CTE and filter in the outer query instead."

```sql
-- Fails: WHERE RowNum = 1  -- RowNum doesn't exist yet at WHERE time
WITH Ranked AS (
    SELECT *, ROW_NUMBER() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS RowNum
    FROM Employees
)
SELECT * FROM Ranked WHERE RowNum = 1; -- correct - filter in the outer query
```

---

### Q11. What's the difference between `ROW_NUMBER()`, `RANK()`, and `DENSE_RANK()`?

**Answer:**
"All three assign a sequential number to rows within a window, ordered by some column, but they handle ties differently. `ROW_NUMBER()` always assigns a unique, sequential number, even to tied rows — so ties get arbitrarily broken. `RANK()` gives tied rows the same rank, but then skips numbers afterward (1, 2, 2, 4). `DENSE_RANK()` also gives tied rows the same rank, but doesn't skip numbers afterward (1, 2, 2, 3)."

```sql
SELECT Name, Salary,
    ROW_NUMBER() OVER (ORDER BY Salary DESC) AS RowNum,
    RANK()       OVER (ORDER BY Salary DESC) AS Rnk,
    DENSE_RANK() OVER (ORDER BY Salary DESC) AS DenseRnk
FROM Employees;

-- Salary: 100, 90, 90, 80
-- RowNum:   1,  2,  3,  4   (ties broken arbitrarily)
-- Rnk:      1,  2,  2,  4   (skips 3 after the tie)
-- DenseRnk: 1,  2,  2,  3   (no gap after the tie)
```

**Where to use:** `ROW_NUMBER()` for deduplication or strict pagination where every row needs a distinct number; `RANK()`/`DENSE_RANK()` for actual leaderboard-style ranking where ties should share a position.

---

### Q12. What does `PARTITION BY` do inside a window function?

**Answer:**
"It divides the rows into independent groups (partitions), and the window function is computed separately within each partition — resetting for each new partition — while still returning one row per original row, unlike `GROUP BY` which would collapse them. It's essentially 'restart the calculation for each group,' but without losing row-level detail."

```sql
SELECT Name, DepartmentId, Salary,
    ROW_NUMBER() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS RankInDept
FROM Employees;
-- ranking restarts at 1 for each new DepartmentId
```

---

### Q13. What are `LAG()` and `LEAD()`, and when would you use them?

**Answer:**
"`LAG()` looks back at a previous row's value within the same window (e.g., 'the previous month's total'); `LEAD()` looks ahead at a following row's value. Both let you compare a row to a neighboring row without a self-join, which is what you'd otherwise need to do this without window functions."

```sql
SELECT OrderMonth, Revenue,
    LAG(Revenue) OVER (ORDER BY OrderMonth) AS PreviousMonthRevenue,
    Revenue - LAG(Revenue) OVER (ORDER BY OrderMonth) AS MonthOverMonthChange
FROM MonthlyRevenue;
```

**Where to use:** month-over-month or day-over-day comparisons, detecting a change from the prior row, finding gaps between consecutive events (see [[sql-04-coding-practice-qa]] for the "find gaps" coding problem).
