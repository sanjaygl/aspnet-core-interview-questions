# SQL — Fundamentals + Keys & Constraints — Interview Q&A

---

### Q1. What's the difference between `DELETE`, `TRUNCATE`, and `DROP`?

**Answer:**
"`DELETE` removes rows one at a time, is fully logged, can be filtered with a `WHERE` clause, and can be rolled back inside a transaction. `TRUNCATE` removes all rows at once by deallocating data pages — much faster, minimally logged, can't be filtered, resets any `IDENTITY` counter back to its seed, but can still be rolled back if it's inside an explicit transaction. `DROP` removes the entire table object — structure, data, indexes, constraints — permanently."

```sql
DELETE FROM Orders WHERE Status = 'Cancelled'; -- filtered, fully logged, slower
TRUNCATE TABLE Orders;                           -- all rows gone, resets IDENTITY, faster
DROP TABLE Orders;                               -- table itself no longer exists
```

**Cross-question: Can you `ROLLBACK` a `TRUNCATE` inside a transaction?**
"Yes — a common misconception is that `TRUNCATE` can't be rolled back because it's 'not logged.' It actually is logged enough to be rolled back, just far less than `DELETE` (it logs page deallocations, not individual row deletions). Wrap it in `BEGIN TRAN` / `ROLLBACK` and it undoes cleanly, same as `DELETE`."

```sql
BEGIN TRAN;
TRUNCATE TABLE Orders;
ROLLBACK; -- Orders is back, unchanged
```

**Where to use:** `DELETE` when you need to remove a subset of rows or need triggers to fire; `TRUNCATE` when clearing an entire table fast and don't need row-level `DELETE` triggers to run; `DROP` when the table itself is no longer needed.

---

### Q2. What's the difference between `WHERE` and `HAVING`?

**Answer:**
"`WHERE` filters individual rows *before* grouping/aggregation happens. `HAVING` filters *groups*, after `GROUP BY` has produced them — which is why `HAVING` can reference aggregate functions like `SUM()`/`COUNT()` and `WHERE` can't."

```sql
SELECT DepartmentId, COUNT(*) AS EmployeeCount
FROM Employees
WHERE IsActive = 1              -- filters rows first
GROUP BY DepartmentId
HAVING COUNT(*) > 5;            -- then filters the resulting groups
```

**Where to use:** `WHERE` to cut down rows early (cheaper, filters before grouping); `HAVING` only when the filter condition itself depends on an aggregate result.

---

### Q3. What's the difference between `UNION` and `UNION ALL`?

**Answer:**
"`UNION` combines the result sets of two queries and removes duplicate rows — which means it has to sort/compare the combined output, adding overhead. `UNION ALL` just concatenates both result sets without checking for duplicates, so it's faster. Both require the same number of columns with compatible types in each query."

```sql
SELECT CustomerId FROM OnlineOrders
UNION
SELECT CustomerId FROM StoreOrders;   -- de-duplicated, slower

SELECT CustomerId FROM OnlineOrders
UNION ALL
SELECT CustomerId FROM StoreOrders;   -- all rows kept, faster
```

**Where to use:** `UNION ALL` by default unless you specifically need duplicates removed — many people reach for `UNION` out of habit and pay an unnecessary sort/dedup cost.

---

### Q4. What is the logical order of execution of a SQL query?

**Answer:**
"SQL is written as `SELECT ... FROM ... WHERE ... GROUP BY ... HAVING ... ORDER BY`, but that's not the order it actually executes in. The real logical order is: `FROM` (and `JOIN`s) → `WHERE` → `GROUP BY` → `HAVING` → `SELECT` → `ORDER BY`. That's why you can't reference a column alias defined in `SELECT` inside the `WHERE` clause — `WHERE` runs before `SELECT` has even been evaluated."

```sql
SELECT DepartmentId, COUNT(*) AS EmpCount
FROM Employees
WHERE IsActive = 1
GROUP BY DepartmentId
HAVING COUNT(*) > 5
ORDER BY EmpCount DESC;

-- Logical order: FROM -> WHERE -> GROUP BY -> HAVING -> SELECT -> ORDER BY
-- This is also why "WHERE EmpCount > 5" would fail — EmpCount doesn't exist yet at that stage
```

**Where this comes up:** explains a lot of "why doesn't this compile" questions — aliases from `SELECT` are only usable in `ORDER BY` (which runs last), never in `WHERE`/`GROUP BY`/`HAVING`.

---

### Q5. What's the difference between Primary Key and Unique Key?

**Answer:**
"Both enforce uniqueness, but a Primary Key also implies `NOT NULL` and there can only be one per table — it's meant to be *the* identifier for a row. A Unique Key allows one `NULL` (in SQL Server) and a table can have multiple Unique constraints, for other columns that also need to be unique but aren't the row's primary identifier."

```sql
CREATE TABLE Employees (
    EmployeeId INT PRIMARY KEY,       -- exactly one PK, never null
    Email NVARCHAR(200) UNIQUE,       -- can have several of these
    SSN NVARCHAR(11) UNIQUE
);
```

**Cross-question: In SQL Server, can a Unique constraint column hold more than one `NULL`?**
"No — this trips people up. ANSI SQL technically treats each `NULL` as distinct, which would allow multiple `NULL`s in a unique column, but SQL Server's implementation treats all `NULL`s as duplicates of each other for uniqueness purposes. So a `UNIQUE` column in SQL Server allows exactly one row with `NULL`, not many."

```sql
CREATE TABLE T (Code NVARCHAR(10) UNIQUE);
INSERT INTO T VALUES (NULL); -- OK
INSERT INTO T VALUES (NULL); -- FAILS - violates unique constraint
```

---

### Q6. What is a Foreign Key, and what does referential integrity mean?

**Answer:**
"A Foreign Key is a column (or set of columns) in one table that references the Primary Key of another table, enforcing that any value in that column must actually exist in the referenced table — you can't have an order pointing at a customer that doesn't exist. Referential integrity is the general guarantee that these relationships stay valid — the database itself rejects any insert/update that would create a dangling reference, rather than relying on application code to remember to check."

```sql
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY,
    CustomerId INT NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

INSERT INTO Orders (OrderId, CustomerId) VALUES (1, 9999); -- FAILS if Customer 9999 doesn't exist
```

---

### Q7. Is a Primary Key the same thing as a Clustered Index?

**Answer:**
"No, even though SQL Server creates a clustered index on the Primary Key by default unless told otherwise. A Primary Key is a logical constraint — uniqueness and not-null. A Clustered Index is a physical storage structure — it determines the actual order rows are stored on disk. You can have a Primary Key backed by a non-clustered index instead, and put the clustered index on a different column entirely."

```sql
-- Primary key that is explicitly NOT clustered; clustered index lives elsewhere
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY NONCLUSTERED,
    OrderDate DATETIME
);
CREATE CLUSTERED INDEX IX_Orders_Date ON Orders(OrderDate);
```

**Cross-question: Can a table have more than one clustered index?**
"No — only one, ever, because a clustered index defines the physical row order, and a table's rows can only be physically sorted one way at a time. You can have many non-clustered indexes on the same table, though."

---

### Q8. What happens when you delete a row that's referenced by a Foreign Key? (`CASCADE` / `RESTRICT` / `SET NULL`)

**Answer:**
"By default (`NO ACTION`/`RESTRICT`), the delete is blocked if any child rows still reference it — you'd get a foreign key violation error. `ON DELETE CASCADE` automatically deletes the matching child rows too. `ON DELETE SET NULL` sets the foreign key column in child rows to `NULL` instead of deleting them (only valid if that FK column is nullable)."

```sql
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY,
    CustomerId INT,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId) ON DELETE CASCADE
);

DELETE FROM Customers WHERE CustomerId = 5; -- also deletes all that customer's Orders rows
```

**Where to use:** `CASCADE` when child rows are meaningless without the parent (e.g., OrderLines belonging to an Order); `SET NULL` when the relationship is optional and the child record should survive independently; default `RESTRICT` when deleting the parent while children exist should be a hard stop requiring explicit cleanup first.

---

### Q9. What's the difference between `COALESCE` and `ISNULL`?

**Answer:**
"`ISNULL(a, b)` is SQL-Server-specific, takes exactly two arguments, and its return type is based on the first argument's type. `COALESCE(a, b, c, ...)` is ANSI-standard SQL (portable across databases), takes any number of arguments, and returns the first non-null one — its return type follows standard data type precedence rules across all its arguments. For simple two-value null-coalescing they behave the same, but `COALESCE` is more flexible and more portable."

```sql
SELECT ISNULL(MiddleName, '') FROM Employees;              -- 2 args only
SELECT COALESCE(NickName, MiddleName, FirstName) FROM Employees; -- first non-null of any number of columns
```

**Where to use:** `COALESCE` by default (portable, flexible with more than 2 values); `ISNULL` mainly seen in older SQL Server-only codebases or when its specific type-inference behavior is actually wanted.
