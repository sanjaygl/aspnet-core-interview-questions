# SQL — Coding Practice — Classic Interview Problems

Assumed schema for most examples:
```sql
Employees(EmployeeId, Name, Salary, DepartmentId, ManagerId, HireDate)
Departments(DepartmentId, Name)
Orders(OrderId, CustomerId, OrderDate, Total)
Customers(CustomerId, Name)
```

---

### Q1. Find the 2nd (or Nth) highest salary without using `TOP`/`LIMIT`.

**Answer:**
"The classic way is `DENSE_RANK()` (handles ties correctly — same salary means same rank) or a correlated subquery counting distinct higher salaries."

```sql
-- Window function approach (preferred)
SELECT Salary FROM (
    SELECT Salary, DENSE_RANK() OVER (ORDER BY Salary DESC) AS Rnk FROM Employees
) t WHERE Rnk = 2; -- change 2 to N for the Nth highest

-- Subquery approach (no window functions)
SELECT MAX(Salary) FROM Employees
WHERE Salary < (SELECT MAX(Salary) FROM Employees);
```

**Cross-question: What if there are duplicate salaries at the top — does your answer handle it correctly?**
"The `DENSE_RANK()` version does — two people tied for highest salary both get rank 1, and rank 2 correctly refers to the next distinct salary value down. A naive `ORDER BY Salary DESC OFFSET 1 ROW FETCH NEXT 1 ROW` would instead return the second *row*, which could just be a duplicate of the highest salary, not the second-highest distinct value."

---

### Q2. Second highest salary *per department*.

**Answer:**
"Same idea as Q1, but partition the ranking by department so it resets for each one."

```sql
SELECT DepartmentId, Salary FROM (
    SELECT DepartmentId, Salary,
        DENSE_RANK() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS Rnk
    FROM Employees
) t WHERE Rnk = 2;
```

---

### Q3. Top 3 earners in each department.

**Answer:**
"Same `DENSE_RANK()`/`PARTITION BY` pattern, filtering for rank <= 3 instead of = 2."

```sql
SELECT DepartmentId, Name, Salary FROM (
    SELECT DepartmentId, Name, Salary,
        DENSE_RANK() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS Rnk
    FROM Employees
) t WHERE Rnk <= 3;
```

---

### Q4. Find duplicate records in a table.

**Answer:**
"Group by the columns that define a 'duplicate' and filter for groups with more than one row using `HAVING`."

```sql
SELECT Email, COUNT(*) AS Occurrences
FROM Customers
GROUP BY Email
HAVING COUNT(*) > 1;
```

---

### Q5. Delete duplicate rows, keeping only one copy.

**Answer:**
"Use `ROW_NUMBER()` partitioned by the columns that define a duplicate, then delete every row where the row number is greater than 1 — keeping exactly one (the first) per group."

```sql
WITH Duplicates AS (
    SELECT *, ROW_NUMBER() OVER (PARTITION BY Email ORDER BY CustomerId) AS RowNum
    FROM Customers
)
DELETE FROM Duplicates WHERE RowNum > 1;
```

**Where this comes up:** this exact pattern — CTE + `ROW_NUMBER()` + `DELETE`/`SELECT` on the CTE — is one of the most reused idioms in practical SQL work, not just interviews.

---

### Q6. Find employees who earn more than their manager (self join).

**Answer:**
"Self join `Employees` to itself, matching each employee's `ManagerId` to the manager's `EmployeeId`, then filter where the employee's salary exceeds the manager's."

```sql
SELECT e.Name AS Employee, e.Salary, m.Name AS Manager, m.Salary AS ManagerSalary
FROM Employees e
JOIN Employees m ON e.ManagerId = m.EmployeeId
WHERE e.Salary > m.Salary;
```

---

### Q7. Find the department with the highest number of employees.

**Answer:**
"Group by department, count, and take the top result — using `ORDER BY ... DESC` with `TOP 1`, or `DENSE_RANK()` if ties should all be returned."

```sql
SELECT TOP 1 DepartmentId, COUNT(*) AS EmpCount
FROM Employees
GROUP BY DepartmentId
ORDER BY EmpCount DESC;
```

---

### Q8. Find consecutive days/values (e.g., users who logged in 3 days in a row).

**Answer:**
"A classic 'gaps and islands' problem. Subtract a running row number from the date — for genuinely consecutive dates, that difference stays constant, which lets you group consecutive runs together with a single `GROUP BY`."

```sql
WITH Ranked AS (
    SELECT UserId, LoginDate,
        DATEADD(DAY, -ROW_NUMBER() OVER (PARTITION BY UserId ORDER BY LoginDate), LoginDate) AS GroupKey
    FROM Logins
)
SELECT UserId, MIN(LoginDate) AS StreakStart, MAX(LoginDate) AS StreakEnd, COUNT(*) AS StreakLength
FROM Ranked
GROUP BY UserId, GroupKey
HAVING COUNT(*) >= 3;
```

**Where to use:** this "subtract row number from date/value" trick is the standard way to solve almost any consecutive-sequence problem in SQL.

---

### Q9. Find gaps in a sequence of numbers/dates.

**Answer:**
"Use `LEAD()` to look at the next row's value, and check whether it's more than one step away from the current row — that's a gap."

```sql
WITH Ordered AS (
    SELECT Id, LEAD(Id) OVER (ORDER BY Id) AS NextId
    FROM Sequence
)
SELECT Id AS GapStart, NextId AS GapEnd
FROM Ordered
WHERE NextId - Id > 1;
```

---

### Q10. Running total / cumulative sum per group.

**Answer:**
"A `SUM()` window function with `ORDER BY` inside the `OVER()` clause naturally computes a running total up through the current row, without needing a self-join or a cursor."

```sql
SELECT OrderId, CustomerId, OrderDate, Total,
    SUM(Total) OVER (PARTITION BY CustomerId ORDER BY OrderDate) AS RunningTotal
FROM Orders;
```

---

### Q11. Find the most recent record per group ("latest record per user").

**Answer:**
"`ROW_NUMBER()` partitioned by the group column, ordered descending by the date column, then filter for row number 1."

```sql
WITH Ranked AS (
    SELECT *, ROW_NUMBER() OVER (PARTITION BY UserId ORDER BY EventDate DESC) AS RowNum
    FROM UserEvents
)
SELECT * FROM Ranked WHERE RowNum = 1;
```

---

### Q12. Pivot rows into columns (and the reverse — unpivot).

**Answer:**
"`PIVOT` turns distinct row values into columns, typically combined with an aggregate. `UNPIVOT` does the reverse — turning columns back into rows."

```sql
-- Pivot: one row per employee, one column per quarter, showing total sales
SELECT EmployeeId, [Q1], [Q2], [Q3], [Q4]
FROM (SELECT EmployeeId, Quarter, SalesAmount FROM Sales) AS SourceTable
PIVOT (SUM(SalesAmount) FOR Quarter IN ([Q1], [Q2], [Q3], [Q4])) AS PivotTable;

-- Unpivot: back to one row per EmployeeId/Quarter
SELECT EmployeeId, Quarter, SalesAmount
FROM PivotedSales
UNPIVOT (SalesAmount FOR Quarter IN ([Q1], [Q2], [Q3], [Q4])) AS UnpivotTable;
```

---

### Q13. Convert comma-separated values in a column into rows (string split).

**Answer:**
"`STRING_SPLIT()` (built into SQL Server 2016+) turns a delimited string into a table of individual values, one row per value — commonly cross-applied per row when the comma-separated column belongs to a larger table."

```sql
SELECT c.CustomerId, s.value AS Tag
FROM Customers c
CROSS APPLY STRING_SPLIT(c.Tags, ',') s;
-- Tags = "vip,wholesale,repeat" -> 3 rows, one per tag
```

---

### Q14. Detect overlapping date ranges (e.g., overlapping bookings).

**Answer:**
"Two ranges overlap if one starts before the other ends, AND ends after the other starts — the classic interval-overlap condition. Self-join the bookings table on that condition (excluding comparing a row to itself)."

```sql
SELECT a.BookingId AS Booking1, b.BookingId AS Booking2
FROM Bookings a
JOIN Bookings b
    ON a.RoomId = b.RoomId
    AND a.BookingId < b.BookingId              -- avoid comparing a row to itself or double-counting pairs
    AND a.StartDate < b.EndDate
    AND a.EndDate > b.StartDate;               -- the actual overlap condition
```

---

### Q15. Customers who placed orders in every month of the year.

**Answer:**
"Group orders by customer, count the distinct months they ordered in, and filter for exactly 12."

```sql
SELECT CustomerId
FROM Orders
WHERE YEAR(OrderDate) = 2026
GROUP BY CustomerId
HAVING COUNT(DISTINCT MONTH(OrderDate)) = 12;
```

---

### Q16. Products never ordered.

**Answer:**
"A classic anti-join — find products that have no matching row in Orders, using `LEFT JOIN ... WHERE ... IS NULL`, or equivalently `NOT EXISTS`."

```sql
-- LEFT JOIN / anti-join style
SELECT p.ProductId, p.Name
FROM Products p
LEFT JOIN OrderLines ol ON ol.ProductId = p.ProductId
WHERE ol.ProductId IS NULL;

-- NOT EXISTS style (often performs better, and doesn't risk NULL-related surprises)
SELECT p.ProductId, p.Name
FROM Products p
WHERE NOT EXISTS (SELECT 1 FROM OrderLines ol WHERE ol.ProductId = p.ProductId);
```

---

### Q17. Median salary in a table (no built-in `MEDIAN` function in SQL Server).

**Answer:**
"`PERCENTILE_CONT(0.5)` computes the median directly (it's the standard way in modern SQL Server) — for the pre-2012 or manual approach, use `ROW_NUMBER()` twice (ascending and descending) and average the middle row(s)."

```sql
-- Modern, preferred
SELECT DISTINCT PERCENTILE_CONT(0.5) OVER (ORDER BY Salary) AS MedianSalary
FROM Employees;

-- Manual fallback
WITH Ordered AS (
    SELECT Salary,
        ROW_NUMBER() OVER (ORDER BY Salary) AS RowAsc,
        ROW_NUMBER() OVER (ORDER BY Salary DESC) AS RowDesc
    FROM Employees
)
SELECT AVG(1.0 * Salary) AS MedianSalary FROM Ordered WHERE RowAsc IN (RowDesc, RowDesc - 1, RowDesc + 1);
```

---

### Q18. `EXISTS` vs `IN` — and the performance difference.

**Answer:**
"Both check for membership/existence, but `EXISTS` stops as soon as it finds one matching row (short-circuits), while `IN` conceptually builds the full list of values from the subquery first. For large subqueries, `EXISTS` is often faster and handles `NULL`s more predictably — `IN` can behave surprisingly if the subquery's result set contains `NULL`s (a `NOT IN` against a list containing `NULL` can silently return zero rows, a well-known gotcha)."

```sql
-- EXISTS - short-circuits, no NULL surprises
SELECT * FROM Customers c WHERE EXISTS (SELECT 1 FROM Orders o WHERE o.CustomerId = c.CustomerId);

-- IN - equivalent here, but "NOT IN" is the risky one:
SELECT * FROM Customers WHERE CustomerId NOT IN (SELECT CustomerId FROM Orders WHERE CustomerId IS NOT NULL);
-- if that inner CustomerId column can contain NULL and you forget the IS NOT NULL filter,
-- NOT IN silently returns ZERO rows instead of the expected result
```

**Where to use:** default to `EXISTS`/`NOT EXISTS` over `IN`/`NOT IN` for subquery-based existence checks — safer with `NULL`s and often faster on large data.
