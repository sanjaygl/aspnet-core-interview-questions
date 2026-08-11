# SQL — Joins + Normalization & Database Design — Interview Q&A

---

### Q1. What are the different types of JOINs, and what's the difference between INNER and OUTER?

**Answer:**
"INNER JOIN returns only rows that have a match in both tables. OUTER JOINs (LEFT, RIGHT, FULL) return matched rows *plus* unmatched rows from one or both sides, filling in `NULL` for the columns that have no match. LEFT keeps all rows from the left table; RIGHT keeps all rows from the right table; FULL keeps all rows from both. CROSS JOIN produces the Cartesian product — every row from the left paired with every row from the right, no matching condition. SELF JOIN is just a table joined to itself, using aliases to distinguish the two 'copies'."

```sql
SELECT c.Name, o.OrderId
FROM Customers c
LEFT JOIN Orders o ON o.CustomerId = c.CustomerId;
-- every customer appears, even ones with zero orders (o.OrderId is NULL for them)
```

---

### Q2. What is a SELF JOIN, and when would you use one?

**Answer:**
"Joining a table to itself, using two different aliases, to compare rows within the same table to each other — most commonly for hierarchical data, like an employee referencing their manager by a `ManagerId` column that points back to the same `Employees` table."

```sql
SELECT e.Name AS Employee, m.Name AS Manager
FROM Employees e
LEFT JOIN Employees m ON e.ManagerId = m.EmployeeId;
-- e and m are the SAME table, joined to itself via the ManagerId -> EmployeeId relationship
```

**Where to use:** any table that references itself — org charts, category trees, "referred by" relationships.

---

### Q3. Does filtering in the `ON` clause mean the same thing as filtering in the `WHERE` clause, for an OUTER JOIN?

**Answer:**
"No, and this is a genuinely common mistake. For an INNER JOIN they behave the same. For a LEFT (or other OUTER) JOIN, a condition in `ON` is applied *while matching* — rows from the left table that don't satisfy it still appear, just with `NULL`s on the right side. The same condition in `WHERE` is applied *after* the join has already produced its result — which can silently strip out the unmatched left-side rows that the LEFT JOIN was supposed to preserve, effectively turning it into an INNER JOIN."

```sql
-- ON clause: keeps ALL customers, but only "Shipped" orders are matched (NULL otherwise)
SELECT c.Name, o.OrderId
FROM Customers c
LEFT JOIN Orders o ON o.CustomerId = c.CustomerId AND o.Status = 'Shipped';

-- WHERE clause: silently drops customers with NO shipped order at all (including customers with zero orders)
SELECT c.Name, o.OrderId
FROM Customers c
LEFT JOIN Orders o ON o.CustomerId = c.CustomerId
WHERE o.Status = 'Shipped';
```

**Cross-question: Can a LEFT JOIN return more rows than the left table has?**
"Yes — if a single left-table row matches multiple rows on the right side, it gets duplicated once per match, same as an INNER JOIN would. A LEFT JOIN only guarantees every left row appears *at least* once, not exactly once."

---

### Q4. JOIN vs Subquery — when would you pick one over the other?

**Answer:**
"A JOIN combines columns from both tables into one result set, and is usually what you want when you need actual columns from both tables. A subquery is useful when you only need to filter based on another table's data (existence, aggregation) without needing its columns in the final output — often expressed with `EXISTS`/`IN`, or as a scalar value. Modern query optimizers frequently rewrite one into the other internally anyway, so for simple cases performance is often equivalent — pick whichever expresses the intent more clearly."

```sql
-- JOIN - need columns from both tables
SELECT o.OrderId, c.Name FROM Orders o JOIN Customers c ON c.CustomerId = o.CustomerId;

-- Subquery - only need to filter, don't need Customers' columns in the output
SELECT OrderId FROM Orders WHERE CustomerId IN (SELECT CustomerId FROM Customers WHERE Country = 'US');
```

---

### Q5. What happens if you JOIN on a column that contains NULL values?

**Answer:**
"Rows with `NULL` in the join column never match, on either side — because SQL's equality comparison (`=`) never evaluates to true when comparing against `NULL`, even `NULL = NULL` is `NULL` (unknown), not true. So if you `JOIN Table A ON a.Col = b.Col` and some rows in either table have `NULL` in `Col`, those rows simply won't be included in an INNER JOIN's results, and will show as unmatched (`NULL` on the other side) in an OUTER JOIN."

```sql
-- If Orders.CustomerId is NULL for some rows, those orders NEVER match, even with a matching NULL on the other side
SELECT * FROM Orders o JOIN Customers c ON o.CustomerId = c.CustomerId;
```

**Where this comes up:** a frequent "why is this row missing from my join result" debugging scenario — check for `NULL`s in the join columns first.

---

### Q6. What is Normalization, and why is it done?

**Answer:**
"Organizing tables and columns to minimize data duplication and avoid update anomalies — where the same fact is stored in multiple places and can get out of sync if only one copy is updated. Normalization splits data into related tables connected by keys, following a series of increasingly strict rules (normal forms)."

---

### Q7. What are 1NF, 2NF, 3NF, and BCNF?

**Answer:**
"1NF — each column holds a single, atomic value (no comma-separated lists in a cell), and rows are uniquely identifiable. 2NF — 1NF, plus every non-key column depends on the *entire* primary key, not just part of it (relevant with composite keys). 3NF — 2NF, plus no non-key column depends on another non-key column (no transitive dependency) — every column should depend only on the key, the whole key, and nothing but the key. BCNF is a stricter version of 3NF that closes a few edge cases 3NF misses, mainly around overlapping candidate keys."

```sql
-- Violates 1NF - PhoneNumbers is not atomic
CREATE TABLE Customers (CustomerId INT, PhoneNumbers NVARCHAR(200)); -- "555-1111,555-2222"

-- Violates 3NF - CustomerCity depends on CustomerId, a non-key column, not directly on OrderId (the key)
CREATE TABLE Orders (OrderId INT PRIMARY KEY, CustomerId INT, CustomerCity NVARCHAR(50));
-- Fix: move CustomerCity to the Customers table, referenced via CustomerId
```

---

### Q8. What is Denormalization, and when would you deliberately break normal form?

**Answer:**
"Deliberately introducing some redundancy — duplicating data or pre-computing joins/aggregates — to optimize for read performance, usually at the cost of write complexity and potential inconsistency. Common in reporting/analytics databases (OLAP-style), or hot read paths where joining several normalized tables on every request is too expensive and the data changes infrequently enough that keeping a denormalized copy in sync is manageable."

```sql
-- Normalized: OrderTotal computed via JOIN + SUM every time it's needed
-- Denormalized: store a precomputed OrderTotal column on Orders, updated whenever line items change
ALTER TABLE Orders ADD OrderTotal DECIMAL(10,2);
```

**Cross-question: What update anomalies does denormalization risk introducing?**
"Since the same fact now lives in more than one place, an update to one copy can leave the other stale if it's not kept in sync consistently — e.g., updating an OrderLine's price but forgetting to recompute the denormalized `OrderTotal` on the Orders row. It trades write-side correctness risk for read-side performance."

---

### Q9. What's the difference between a Surrogate Key and a Natural Key?

**Answer:**
"A Natural Key is a column (or set of columns) that has real-world business meaning and would naturally be unique — like an email address or a national ID number. A Surrogate Key is an artificial identifier with no business meaning, generated purely to identify the row — an auto-incrementing `IDENTITY` column or a `GUID`. Surrogate keys are usually preferred as the Primary Key because natural keys can change (a person can change their email) or turn out not to be as unique as assumed, which is disastrous for a Primary Key that other tables reference via Foreign Keys."

```sql
CREATE TABLE Customers (
    CustomerId INT IDENTITY PRIMARY KEY,      -- surrogate key - meaningless, stable
    Email NVARCHAR(200) UNIQUE NOT NULL       -- natural key - meaningful, but could theoretically need to change
);
```

**Where to use:** surrogate keys as the Primary Key in almost all cases; keep the natural key as a `UNIQUE` constraint alongside it if it also needs to be enforced as unique.
