# SQL — Indexes & Query Optimization + Transactions & Concurrency — Interview Q&A

---

### Q1. What's the difference between a Clustered Index and a Non-Clustered Index?

**Answer:**
"A Clustered Index determines the actual physical order rows are stored on disk — the table's data *is* the index, sorted by the clustered key. A Non-Clustered Index is a separate structure that stores the indexed column(s) plus a pointer back to the actual row (in SQL Server, that pointer is the clustered index key, if one exists, otherwise a row locator). Because there's only one physical order a table can be stored in, there can only be one clustered index per table, but many non-clustered indexes."

```sql
CREATE CLUSTERED INDEX IX_Orders_Date ON Orders(OrderDate);        -- only one of these per table
CREATE NONCLUSTERED INDEX IX_Orders_CustomerId ON Orders(CustomerId); -- can have many of these
```

**Cross-question: How many clustered vs non-clustered indexes can one table have?**
"Exactly one clustered index (or zero, for a 'heap' table with no clustered index at all), and up to 999 non-clustered indexes in SQL Server — though in practice you'd never want anywhere near that many, since every index adds write overhead."

---

### Q2. What is a Covering Index?

**Answer:**
"A non-clustered index that includes every column a particular query needs — either as part of the key or via the `INCLUDE` clause — so the query can be satisfied entirely from the index itself, without a further lookup back to the actual table (a 'key lookup'). This avoids extra I/O and is one of the most effective, targeted performance fixes for a specific slow query."

```sql
-- Query: SELECT CustomerId, OrderDate FROM Orders WHERE Status = 'Shipped'
CREATE NONCLUSTERED INDEX IX_Orders_Status_Covering
ON Orders(Status) INCLUDE (CustomerId, OrderDate);
-- Now this exact query never needs to touch the base table at all
```

---

### Q3. Index Seek vs Index Scan — which is better, and why?

**Answer:**
"A Seek navigates directly to the matching row(s) using the index's tree structure — efficient, and its cost barely grows as the table grows. A Scan reads the entire index (or table) from start to end, checking every row — its cost grows linearly with table size. A Seek is generally what you want for selective queries (returning a small subset of rows); a Scan isn't automatically bad, though — for a query that legitimately needs most of the table's rows (e.g., aggregating a whole table), a Scan can actually be the more efficient choice, since a Seek's per-row navigation overhead adds up if you're going to touch almost every row anyway."

```
Execution plan showing "Index Seek" on IX_Orders_CustomerId  -> fast, targeted lookup
Execution plan showing "Index Scan" or "Table Scan"          -> reading everything, check if that's expected
```

**Where to use:** if a query filtering to a small subset of rows shows a Scan instead of a Seek, that's usually a sign of a missing or unusable index — investigate.

---

### Q4. When can adding an index make a query *slower*?

**Answer:**
"Indexes aren't free — every `INSERT`/`UPDATE`/`DELETE` has to also update every index on that table, so too many indexes (or the wrong ones) slow down writes. An index can also be actively unhelpful for reads if it's not selective enough (e.g., indexing a boolean column that's 90% one value) — the optimizer may ignore it and scan anyway, or worse, use it inefficiently. And an index that doesn't match how queries actually filter/sort (wrong column order in a composite index, wrong leading column) provides no benefit while still costing write overhead."

**Where to use:** index based on actual query patterns (what's in `WHERE`, `JOIN`, `ORDER BY`), not preemptively on every column — measure with the execution plan, don't guess.

---

### Q5. Composite (multi-column) Index — why does column order matter?

**Answer:**
"A composite index is sorted first by its first column, then by the second column within each value of the first, and so on — like a phone book sorted by last name, then first name. This means the index is only efficiently searchable via a Seek when your query filters on a *leading, contiguous* prefix of those columns. Filtering only on the second column, skipping the first, generally can't use a Seek on that index at all — it degrades to a Scan."

```sql
CREATE NONCLUSTERED INDEX IX_Orders_Customer_Date ON Orders(CustomerId, OrderDate);

-- Uses the index efficiently (Seek) - filters on the leading column
SELECT * FROM Orders WHERE CustomerId = 5;
SELECT * FROM Orders WHERE CustomerId = 5 AND OrderDate > '2026-01-01';

-- Can't Seek efficiently - skips the leading column (CustomerId)
SELECT * FROM Orders WHERE OrderDate > '2026-01-01';
```

**Where to use:** put the column most commonly filtered with an equality check first, and the most selective columns earlier in the key when queries commonly filter on both.

---

### Q6. How do you read and interpret an Execution Plan?

**Answer:**
"Read it right-to-left, bottom-to-top for the general data flow — the rightmost/bottommost operators run first (table/index access), feeding upward/leftward into joins, filters, and finally the output. Look at the relative cost percentage on each operator to spot the expensive step. Key things to check: Seek vs Scan on the operators touching your biggest tables, whether estimated vs actual row counts diverge wildly (a sign statistics are stale), and whether there's an expensive Sort or a Key Lookup that a covering index could eliminate."

**Where to use:** any time a query is 'slow' and you need to know *why* — guessing at the fix without looking at the plan usually wastes time on the wrong optimization.

---

### Q7. What is SARGability, and why does wrapping a column in a function break it?

**Answer:**
"SARGable ('Search ARGument-able') means a predicate can use an index Seek directly. Wrapping the *indexed column* in a function (`WHERE YEAR(OrderDate) = 2026`) forces the database to compute that function for every single row before it can compare — which means it can't use the index's sort order to jump directly to matching rows, and falls back to a full Scan. The fix is to rewrite the predicate so the raw column stays untouched, moving any transformation to the other side of the comparison instead."

```sql
-- NOT SARGable - function wraps the indexed column, forces a scan
SELECT * FROM Orders WHERE YEAR(OrderDate) = 2026;

-- SARGable - OrderDate itself stays untouched, index Seek possible
SELECT * FROM Orders WHERE OrderDate >= '2026-01-01' AND OrderDate < '2027-01-01';
```

**Cross-question: Give another example of a non-SARGable predicate and rewrite it to be SARGable.**
"`WHERE '%' + Name LIKE '%smith'` (leading wildcard) can't use an index Seek at all, because the index can't determine where in its sorted order a value ending in a given string would fall — it would have to check every row. There's no SARGable rewrite for a genuine leading-wildcard search on a standard B-tree index; that scenario is what full-text search indexes are actually designed for."

---

### Q8. What is `NOLOCK`, and what are the risks of using it?

**Answer:**
"`NOLOCK` (equivalent to the `READ UNCOMMITTED` isolation level, applied per-table via a query hint) tells SQL Server to read data without taking or respecting shared locks — so reads don't block writers, and writers don't block these reads. The cost: it can return dirty reads (uncommitted data that later gets rolled back), duplicate rows, or even skip rows entirely during certain concurrent modifications — because it's reading data that could be mid-change. It's tempting to slap on every query to 'fix' blocking, but it's really only appropriate for approximate reporting/dashboards where perfect accuracy isn't required."

```sql
SELECT * FROM Orders WITH (NOLOCK) WHERE Status = 'Pending';
-- fast, non-blocking, but could show data that's about to be rolled back, or miss/duplicate rows
```

**Where to use:** rough dashboards/reports where a slightly-stale or approximate result is acceptable; never for anything involving money, inventory counts, or any decision that depends on the data actually being correct.

---

### Q9. What is a Transaction, and what does ACID mean?

**Answer:**
"A transaction is a group of operations that succeed or fail together, as a single unit. ACID describes the guarantees a transaction should provide: Atomicity — all-or-nothing, no partial effect left behind. Consistency — the database moves from one valid state to another, never violating its constraints. Isolation — concurrent transactions don't see each other's uncommitted, in-progress changes (to a degree controlled by the isolation level). Durability — once committed, the change survives even a crash right afterward."

```sql
BEGIN TRAN;
UPDATE Accounts SET Balance = Balance - 100 WHERE AccountId = 1;
UPDATE Accounts SET Balance = Balance + 100 WHERE AccountId = 2;
COMMIT; -- both updates succeed together, or (on error) neither does
```

---

### Q10. What are the Transaction Isolation Levels?

**Answer:**
"From least to most strict: Read Uncommitted allows dirty reads — you can see another transaction's uncommitted changes. Read Committed (the SQL Server default) only sees committed data, but a value can still change if you read it twice within the same transaction (non-repeatable read). Repeatable Read guarantees that if you read a row twice in the same transaction, you'll see the same value — achieved by holding locks on read rows until the transaction ends — but new rows matching your filter can still appear (a phantom read). Serializable is the strictest — it also locks the range being queried, preventing phantom reads too, at the cost of the most blocking/concurrency."

**Cross-question: What's the default isolation level in SQL Server?**
"Read Committed."

---

### Q11. What's the difference between a Dirty Read, a Non-Repeatable Read, and a Phantom Read?

**Answer:**
"Dirty Read — reading another transaction's uncommitted change, which might get rolled back, leaving you having acted on data that never really existed. Non-Repeatable Read — reading the same row twice within one transaction and getting different values, because another transaction committed a change to it in between. Phantom Read — running the same query twice within one transaction and getting a different *set of rows*, because another transaction inserted or deleted rows matching your filter in between."

```
Dirty read:            T1 updates a row (not yet committed) -> T2 reads the new value -> T1 rolls back -> T2 acted on a value that never existed
Non-repeatable read:    T1 reads Salary=100 -> T2 commits Salary=200 -> T1 reads again, now gets 200
Phantom read:           T1 counts rows WHERE Age > 30 (gets 5) -> T2 inserts a matching row -> T1 counts again, gets 6
```

---

### Q12. What's the difference between Optimistic and Pessimistic Locking?

**Answer:**
"Pessimistic locking assumes conflicts are likely, so it locks the row as soon as it's read for update, blocking any other transaction from touching it until released — safe, but reduces concurrency. Optimistic locking assumes conflicts are rare — it doesn't lock anything upfront, but checks at update time whether the row changed since it was read (usually via a version/timestamp column), and rejects the update if it did, letting the caller retry. Optimistic is generally preferred for high-concurrency, low-conflict workloads (most web apps); pessimistic makes sense when conflicts are frequent or the cost of a lost update is high."

**Cross-question: How would you actually implement optimistic concurrency in application code?**
"Add a `rowversion` (or `timestamp`) column to the table — SQL Server auto-updates it on every row change. When updating, include the original `rowversion` value in the `WHERE` clause; if no rows are affected, that means someone else changed the row in between, so the application knows to reject/retry rather than blindly overwrite."

```sql
UPDATE Orders SET Status = 'Shipped'
WHERE OrderId = 42 AND RowVersion = @OriginalRowVersion;
-- if @@ROWCOUNT = 0, someone else modified this row since it was read - handle the conflict
```

---

### Q13. What is a Deadlock, and how does SQL Server resolve one automatically?

**Answer:**
"Two transactions each hold a lock the other needs, so both wait on each other forever. SQL Server runs a background deadlock monitor that detects the cycle and automatically kills one transaction (the 'victim,' usually the cheaper one to roll back), raising error 1205 back to that caller and letting the other proceed. The full mechanics, victim selection (`DEADLOCK_PRIORITY`), and how to diagnose/prevent deadlocks are covered in [[database-deadlock-qa]]."
