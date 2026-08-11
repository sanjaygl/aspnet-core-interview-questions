# SQL — Views, Stored Procedures, Functions & Triggers + Advanced/Scenario-Based — Interview Q&A

---

### Q1. What is a View, and what are its pros/cons vs querying tables directly?

**Answer:**
"A View is a saved, named query that behaves like a virtual table — you query it like a table, but it re-runs its underlying `SELECT` each time (unless it's materialized/indexed, see Q2). Pros: hides complex joins behind a simple name, centralizes a query definition so it's defined once and reused, and can restrict which columns/rows a user is allowed to see. Cons: it's still just a query — a view over several joined tables gets the same performance as writing that join directly, and stacking views on top of views can obscure what's actually being executed and hurt the optimizer's ability to produce a good plan."

```sql
CREATE VIEW ActiveCustomerOrders AS
SELECT c.Name, o.OrderId, o.Total
FROM Customers c
JOIN Orders o ON o.CustomerId = c.CustomerId
WHERE c.IsActive = 1;

SELECT * FROM ActiveCustomerOrders WHERE Total > 100; -- queried just like a table
```

**Cross-question: Can you update data through a View, and what restrictions apply?**
"Yes, but only if the view is simple enough — a single base table, no aggregates, no `DISTINCT`, no `GROUP BY`, and all `NOT NULL` columns without defaults must be included. A view joining multiple tables generally can't be directly updated without an `INSTEAD OF` trigger to tell SQL Server how to translate the update back onto the underlying tables."

---

### Q2. What's the difference between a Materialized/Indexed View and a regular View?

**Answer:**
"A regular View is just a stored query — it re-executes every time you select from it. An Indexed View (SQL Server's version of a materialized view) actually persists its result set physically on disk, with a unique clustered index, kept automatically in sync as the underlying data changes. That means reading from it is fast (no re-computation), but it adds write overhead, since every change to the underlying tables must also update the materialized result."

```sql
CREATE VIEW dbo.OrderTotals WITH SCHEMABINDING AS
SELECT CustomerId, SUM(Total) AS TotalSpent FROM dbo.Orders GROUP BY CustomerId;

CREATE UNIQUE CLUSTERED INDEX IX_OrderTotals ON dbo.OrderTotals(CustomerId); -- materializes it
```

**Where to use:** indexed views for expensive, frequently-read aggregations over data that doesn't change too often — trades write cost for much faster reads.

---

### Q3. Stored Procedure vs inline application SQL — why use one?

**Answer:**
"A Stored Procedure is precompiled and cached on the database server, reducing repeated compilation overhead, and centralizes SQL logic in one place instead of scattering raw SQL strings across application code. It also naturally parameterizes inputs, which protects against SQL injection the same way parameterized queries in application code do. Downsides: business logic split between the database and the application layer can get harder to test/version/deploy consistently compared to keeping it all in application code with an ORM."

---

### Q4. What's the difference between a Stored Procedure and a Function?

**Answer:**
"A Stored Procedure can perform data modification (`INSERT`/`UPDATE`/`DELETE`), doesn't have to return a value at all (or can return multiple result sets), and can't be used directly inside a `SELECT` statement. A Function must return a value (scalar or table), can be used inline inside a `SELECT`/`WHERE` like a regular expression or table source, but in SQL Server can't modify data — it's meant to be a read-only, composable expression."

```sql
CREATE FUNCTION GetAge(@BirthDate DATE) RETURNS INT AS
BEGIN RETURN DATEDIFF(YEAR, @BirthDate, GETDATE()) END;

SELECT Name, dbo.GetAge(BirthDate) FROM Employees; -- used inline, like an expression
```

**Cross-question: Can a scalar or table-valued function perform `INSERT`/`UPDATE`/`DELETE`?**
"No — in SQL Server, functions are restricted to being side-effect-free; they can't modify database state. If you need to both compute something and modify data, that has to be a Stored Procedure."

---

### Q5. What is a Trigger, and what are the risks of overusing them?

**Answer:**
"A Trigger is code that automatically runs in response to an event on a table — typically `INSERT`/`UPDATE`/`DELETE` (a DML trigger). Useful for enforcing complex rules the built-in constraints can't express, or auditing changes. The risk is that triggers are invisible at the call site — a simple `UPDATE` statement can silently cascade into a chain of trigger logic nobody looking at that statement would expect, making the system harder to reason about and debug, and they can add real performance overhead to every affected write."

```sql
CREATE TRIGGER trg_Orders_Audit ON Orders AFTER UPDATE AS
BEGIN
    INSERT INTO OrdersAudit (OrderId, ChangedAt) SELECT OrderId, GETDATE() FROM inserted;
END;
```

**Where to use:** sparingly — auditing and enforcing cross-table invariants that truly can't be done with constraints. Avoid using triggers for core business logic that would be clearer as explicit application code.

---

### Q6. Scalar Function vs Table-Valued Function?

**Answer:**
"A Scalar Function returns a single value and is called like an expression. A Table-Valued Function returns a table (a full result set) and is used in the `FROM` clause like a table. Table-Valued Functions come in two flavors: inline (a single `RETURN (SELECT ...)`, which the optimizer can often inline into the calling query's plan efficiently) and multi-statement (a function body building up a table variable, generally less optimizer-friendly, since SQL Server can't see inside it as easily)."

```sql
-- Inline table-valued function - preferred, optimizer-friendly
CREATE FUNCTION GetOrdersByCustomer(@CustomerId INT) RETURNS TABLE AS
RETURN (SELECT * FROM Orders WHERE CustomerId = @CustomerId);

SELECT * FROM GetOrdersByCustomer(5); -- used like a table
```

---

### Q7. How would you design a schema for an e-commerce Orders/Products/Customers system?

**Answer:**
"Core entities: `Customers` (identity/contact info), `Products` (catalog data, price), `Orders` (header — customer, date, status, total), `OrderLines` (one row per product in an order — quantity, unit price at time of purchase). Note `OrderLines` stores its own `UnitPrice` snapshot rather than joining live to `Products.Price` — product prices change over time, and a historical order must reflect what was actually charged, not today's price. Add supporting tables as needed: `Addresses` (shipping/billing, possibly many per customer), `Payments`, `Inventory`/`StockLevels`. Keys: surrogate integer/GUID primary keys, foreign keys enforcing the relationships, indexes on `Orders.CustomerId` and `OrderLines.OrderId`/`ProductId` for the common lookup patterns."

```sql
Customers(CustomerId PK, Name, Email)
Products(ProductId PK, Name, Price)
Orders(OrderId PK, CustomerId FK, OrderDate, Status, Total)
OrderLines(OrderLineId PK, OrderId FK, ProductId FK, Quantity, UnitPrice) -- UnitPrice is a snapshot, not live
```

---

### Q8. How would you find and fix a slow-running query in production?

**Answer:**
"First, capture the actual execution plan (not just guess) — either via `SET STATISTICS IO, TIME ON` plus the plan, or a monitoring tool that already captured it when the query ran slow. Look for Scans where a Seek was expected, large gaps between estimated and actual row counts (stale statistics), expensive Sorts, or Key Lookups that a covering index could eliminate. Check for blocking from other sessions holding locks on the same rows. Fix the most impactful single issue first — usually a missing/wrong index — rather than rewriting the whole query speculatively, then re-measure."

---

### Q9. How would you detect and resolve a blocking/long-running query in production?

**Answer:**
"Query `sys.dm_exec_requests`/`sys.dm_exec_sessions` (or `sp_who2` / Activity Monitor) to find sessions with a non-null `blocking_session_id` — that tells you who's blocking whom. Look at what the blocking session is doing and how long it's held its transaction open; often it's a long-running or forgotten-open transaction holding locks far longer than necessary. The right fix is almost always addressing the root cause — shortening the transaction, adding a missing index so the query doesn't need such a broad lock, or fixing application code that opened a transaction and never closed it promptly — rather than reflexively killing sessions or slapping `NOLOCK` everywhere."

```sql
SELECT blocking_session_id, session_id, wait_type, wait_time, text
FROM sys.dm_exec_requests r
CROSS APPLY sys.dm_exec_sql_text(r.sql_handle)
WHERE blocking_session_id <> 0;
```

---

### Q10. How would you paginate a large result set efficiently?

**Answer:**
"`OFFSET`/`FETCH NEXT` is the standard SQL Server approach, but note that `OFFSET` on a large page number still has to skip past all the preceding rows internally, so very deep pagination gets progressively slower. For better performance on large datasets, 'keyset pagination' (a.k.a. seek-based pagination) is preferred — remember the last row's sort key from the previous page and filter `WHERE SortKey > @LastSeenKey` instead of counting offsets, which lets the query Seek directly to the right spot regardless of how deep you page."

```sql
-- OFFSET/FETCH - simple, degrades on deep pages
SELECT * FROM Orders ORDER BY OrderId OFFSET 10000 ROWS FETCH NEXT 20 ROWS ONLY;

-- Keyset pagination - stays fast regardless of page depth
SELECT * FROM Orders WHERE OrderId > @LastSeenOrderId ORDER BY OrderId OFFSET 0 ROWS FETCH NEXT 20 ROWS ONLY;
```

---

### Q11. How would you handle a table with hundreds of millions of rows that's slowing down?

**Answer:**
"Start with the basics — confirm indexes actually match query patterns, statistics are up to date, and queries are SARGable. Beyond that: table partitioning (splitting the table's storage by a key like date, so operations can target/scan a single partition instead of the whole table), archiving old data out of the hot table into a history table, and considering whether some reporting/aggregate queries should run against a denormalized or pre-aggregated copy instead of the live transactional table."

---

### Q12. Horizontal vs Vertical Partitioning of a table?

**Answer:**
"Horizontal partitioning splits a table by *rows* — e.g., orders from 2024 in one partition, 2025 in another, often by date range, so queries and maintenance can target just the relevant partition instead of the whole table. Vertical partitioning splits by *columns* — e.g., splitting frequently-accessed columns from rarely-accessed large columns (like a big `TEXT`/`BLOB` field) into a separate table, so the common queries don't have to read past that heavy data."

---

### Q13. How would you migrate a schema change on a large production table with zero downtime?

**Answer:**
"Avoid a single blocking `ALTER TABLE` that locks the whole table for its duration. For adding a nullable column, SQL Server handles this pretty efficiently already (metadata-only in most cases). For anything heavier — adding a `NOT NULL` column with a default on a huge table, or changing a column's type — the safe pattern is: add the new column as nullable, backfill it in small batches (to avoid one giant blocking transaction and huge log growth), then switch application code to use it, and only enforce `NOT NULL`/drop the old column once everything's confirmed migrated. Online index rebuilds (`WITH (ONLINE = ON)`) avoid blocking reads/writes during index changes, where the edition supports it."

---

### Q14. How do you prevent SQL Injection, and why do parameterized queries fix it?

**Answer:**
"Never build SQL by concatenating raw user input into a query string — always use parameterized queries (or an ORM that does this for you), where the input is passed separately from the SQL text as a typed parameter. The database then treats it strictly as a *value*, never as executable SQL syntax, no matter what characters it contains — so a malicious input like `'; DROP TABLE Users; --` just becomes a literal string value being searched for, not a second statement being executed."

```csharp
// BAD - string concatenation, vulnerable
var query = $"SELECT * FROM Users WHERE Username = '{username}'";

// GOOD - parameterized, the input can never break out of being "just a value"
var command = new SqlCommand("SELECT * FROM Users WHERE Username = @username", connection);
command.Parameters.AddWithValue("@username", username);
```
