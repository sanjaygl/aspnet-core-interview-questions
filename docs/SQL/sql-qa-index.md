# SQL Interview Questions — Index

Revised pass: trimmed a couple of weaker items, added a few higher-value questions that come up often (ON vs WHERE in outer joins, index seek vs scan, NOLOCK, optimistic concurrency in practice), and added **cross-questions** — the follow-up/gotcha questions an interviewer typically asks right after your first answer. Still 6 files.

---

## File 1 — `sql-01-fundamentals-keys-qa.md`
**SQL Fundamentals + Keys & Constraints**
1. `DELETE` vs `TRUNCATE` vs `DROP`.
   - *Cross-question:* Can you `ROLLBACK` a `TRUNCATE` inside a transaction?
2. `WHERE` vs `HAVING`.
3. `UNION` vs `UNION ALL`.
4. Logical order of execution of a SQL query (FROM → WHERE → GROUP BY → HAVING → SELECT → ORDER BY).
5. Primary Key vs Unique Key.
   - *Cross-question:* In SQL Server, can a Unique constraint column hold more than one `NULL`?
6. Foreign Key and referential integrity.
7. Primary Key vs Clustered Index — are they the same thing?
   - *Cross-question:* Can a table have more than one clustered index?
8. `ON DELETE CASCADE` / `RESTRICT` / `SET NULL`.
9. `COALESCE` vs `ISNULL` — what's actually different? *(new)*

## File 2 — `sql-02-joins-database-design-qa.md`
**Joins + Normalization & Database Design**
1. Types of JOINs (INNER, LEFT, RIGHT, FULL, CROSS, SELF) and INNER vs OUTER.
2. SELF JOIN — when would you use one? *(coding example)*
3. Filtering in the `ON` clause vs the `WHERE` clause in a LEFT/OUTER JOIN — do they mean the same thing? *(new, commonly missed)*
   - *Cross-question:* Can a LEFT JOIN return more rows than the left table has?
4. JOIN vs Subquery — when would you pick one over the other?
5. JOIN on a column with NULL values (gotcha).
6. What is Normalization, and why is it done?
7. 1NF, 2NF, 3NF, BCNF — with examples.
8. Denormalization — when would you deliberately break normal form?
   - *Cross-question:* What update anomalies does denormalization risk introducing?
9. Surrogate Key vs Natural Key.

## File 3 — `sql-03-aggregates-subqueries-window-functions-qa.md`
**Aggregate Functions & Grouping + Subqueries & CTEs + Window Functions**
1. `COUNT(*)` vs `COUNT(1)` vs `COUNT(column)`.
   - *Cross-question:* Does `COUNT(column)` count `NULL` values?
2. `GROUP BY` rules for columns in `SELECT`.
3. `GROUP BY` vs `DISTINCT`.
4. Why can't you use an aggregate function directly in a `WHERE` clause? *(new — ties to File 1's WHERE/HAVING)*
5. Subquery types (scalar, correlated, single-row, multi-row).
6. Correlated Subquery vs a regular subquery.
   - *Cross-question:* Does a correlated subquery execute once, or once per outer row — and what's the performance cost vs a JOIN?
7. CTE vs Subquery — why use one over the other?
8. Recursive CTE. *(coding example — org chart / hierarchy)*
9. CTE vs Temp Table / Table Variable.
10. Window Functions vs Aggregate Functions — how are they different?
    - *Cross-question:* Can you use a window function result directly in a `WHERE` clause?
11. `ROW_NUMBER()` vs `RANK()` vs `DENSE_RANK()` — behavior on ties. *(coding example)*
12. `PARTITION BY` inside a window function.
13. `LAG()` and `LEAD()`. *(coding example)*

## File 4 — `sql-04-coding-practice-qa.md`
**SQL Coding / Query-Writing Questions — classic problems + extra challenge set**
1. Find the 2nd (or Nth) highest salary without using `TOP`/`LIMIT`.
2. Second highest salary *per department*.
3. Top 3 earners in each department.
4. Find duplicate records in a table.
5. Delete duplicate rows, keeping only one copy.
6. Find employees who earn more than their manager (self join).
7. Find the department with the highest number of employees.
8. Find consecutive days/values (e.g., users who logged in 3 days in a row).
9. Find gaps in a sequence of numbers/dates.
10. Running total / cumulative sum per group.
11. Find the most recent record per group ("latest record per user").
12. Pivot rows into columns (and the reverse — unpivot).
13. Convert comma-separated values in a column into rows (string split).
14. Detect overlapping date ranges (e.g., overlapping bookings).
15. Customers who placed orders in every month of the year.
16. Products never ordered.
17. Median salary in a table (no built-in `MEDIAN` function in SQL Server).
18. `EXISTS` vs `IN` — and the performance difference.

## File 5 — `sql-05-indexes-optimization-transactions-qa.md`
**Indexes & Query Optimization + Transactions & Concurrency**
1. Clustered Index vs Non-Clustered Index.
   - *Cross-question:* How many clustered vs non-clustered indexes can one table have?
2. What is a Covering Index?
3. Index Seek vs Index Scan — which is better, and why? *(new — very commonly asked alongside execution plans)*
4. When can adding an index make a query *slower*?
5. Composite (multi-column) Index — why does column order matter?
6. How do you read and interpret an Execution Plan?
7. What is SARGability, and why does wrapping a column in a function break it?
   - *Cross-question:* Give an example of a non-SARGable predicate and rewrite it to be SARGable.
8. What is `NOLOCK`, and what are the risks of using it? *(new — frequently asked, practical)*
9. What is a Transaction, and what does ACID mean?
10. Transaction Isolation Levels (Read Uncommitted, Read Committed, Repeatable Read, Serializable).
    - *Cross-question:* What's the default isolation level in SQL Server?
11. Dirty Read vs Non-Repeatable Read vs Phantom Read.
12. Optimistic vs Pessimistic Locking.
    - *Cross-question:* How would you actually implement optimistic concurrency in application code (e.g., a `rowversion`/`timestamp` column)?
13. Deadlock, and how SQL Server resolves one automatically. *(cross-reference: [[database-deadlock-qa]])*

## File 6 — `sql-06-db-objects-scenarios-qa.md`
**Views, Stored Procedures, Functions & Triggers + Advanced/Scenario-Based**
1. View — pros/cons vs querying tables directly.
   - *Cross-question:* Can you update data through a View, and what restrictions apply?
2. Materialized/Indexed View vs a regular View.
3. Stored Procedure vs inline application SQL.
4. Stored Procedure vs Function.
   - *Cross-question:* Can a scalar or table-valued function perform `INSERT`/`UPDATE`/`DELETE`?
5. Trigger — and the risks of overusing them.
6. Scalar Function vs Table-Valued Function.
7. How would you design a schema for an e-commerce Orders/Products/Customers system?
8. How would you find and fix a slow-running query in production?
9. How would you detect and resolve a blocking/long-running query in production? *(new — pairs naturally with the deadlock/locking topics in File 5)*
10. How would you paginate a large result set efficiently?
11. How would you handle a table with hundreds of millions of rows that's slowing down?
12. Horizontal vs Vertical Partitioning of a table.
13. How would you migrate a schema change on a large production table with zero downtime?
14. How do you prevent SQL Injection, and why do parameterized queries fix it?
