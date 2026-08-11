# Database Deadlocks (SQL Server) — Interview Q&A

---

### Q1. What is a database deadlock?

**Answer:**
"Same core idea as a threading deadlock, but at the database level — two transactions each hold a lock the other one needs, and each is waiting for the other to release it. Neither can proceed. Classic example: Transaction A updates Row 1 then tries to update Row 2; Transaction B updates Row 2 then tries to update Row 1 — at the same time. Each is now waiting on a lock the other holds."

```sql
-- Transaction A
BEGIN TRAN;
UPDATE Orders SET Status = 'Shipped' WHERE Id = 1;  -- locks Row 1
-- ... at the same moment, Transaction B does the same in reverse order ...
UPDATE Orders SET Status = 'Shipped' WHERE Id = 2;  -- waits for Transaction B to release Row 2

-- Transaction B
BEGIN TRAN;
UPDATE Orders SET Status = 'Cancelled' WHERE Id = 2; -- locks Row 2
UPDATE Orders SET Status = 'Cancelled' WHERE Id = 1; -- waits for Transaction A to release Row 1
```

---

### Q2. How does SQL Server actually resolve a deadlock once it happens?

**Answer:**
"SQL Server runs a background 'deadlock monitor' thread that periodically checks for a cycle in the lock-wait graph. Once it detects one, it doesn't just let both transactions hang forever — it picks one as the 'deadlock victim', kills that transaction, rolls it back, and raises error 1205 back to the caller. The other transaction proceeds normally. It's automatic — the app doesn't need to detect the deadlock itself, only handle the resulting exception."

**Where this comes up:** interviewers often ask "does SQL Server just hang on a deadlock?" — the answer is no, it self-resolves by killing one side within seconds.

---

### Q3. How does SQL Server pick which transaction becomes the victim?

**Answer:**
"By default, it kills whichever transaction is cheaper to roll back — the one that's done less work / would cost less to undo. You can influence this with `SET DEADLOCK_PRIORITY`, which lets you mark a particular session as more or less likely to be chosen as the victim — useful if some process is more important and you'd rather have a lower-priority background job get killed instead."

```sql
SET DEADLOCK_PRIORITY LOW;  -- this session will be preferred as the victim if a deadlock occurs
-- ... do the transaction ...

SET DEADLOCK_PRIORITY HIGH; -- this session will be protected, other side gets killed instead
```

**Where to use:** background/batch jobs that can safely be retried should run at `LOW` priority, so a deadlock against a user-facing, latency-sensitive transaction kills the batch job instead.

---

### Q4. How does the application find out a deadlock happened, and what should it do?

**Answer:**
"The killed transaction gets a SQL exception back — error number 1205 ('Transaction was deadlocked... rerun the transaction'). The correct response is to retry the whole transaction from the start, usually with a short backoff, since deadlocks are often transient and a retry frequently succeeds. I wouldn't retry blindly forever — cap the retry count and let it bubble up as a real failure if it keeps happening, since that usually means an underlying design problem, not bad luck."

```csharp
// EF Core / ADO.NET style retry on SQL error 1205
int attempts = 0;
while (true)
{
    try
    {
        using var transaction = dbContext.Database.BeginTransaction();
        // ... do the work ...
        transaction.Commit();
        break;
    }
    catch (SqlException ex) when (ex.Number == 1205 && ++attempts < 3)
    {
        Thread.Sleep(100 * attempts); // short backoff before retry
    }
}
```

**Where to use:** wrap this kind of retry logic around any transaction known to touch hot, frequently-contended rows — order processing, inventory decrement, counters. In .NET, `Polly` is the standard library for this instead of hand-rolled retry loops.

---

### Q5. What are the common real-world causes of database deadlocks?

**Answer:**
"Most often: two transactions updating the same rows in a different order (like the classic example above), long-running transactions holding locks longer than necessary, missing indexes causing full table/range scans that take broader locks than needed, and mixing read and write operations without a consistent access order across the app. Basically — anything that makes SQL Server hold a lock for longer, on more rows, than it strictly needs to."

**Where to use as diagnostic instinct:** if deadlocks show up on a specific table, first check for missing indexes on the columns in the `WHERE`/`JOIN` clauses of the queries involved — a scan taking a wider range/table lock is a very common root cause.

---

### Q6. How do you prevent deadlocks at the database level?

**Answer:**
"Access tables and rows in a consistent order across all transactions — same idea as lock ordering with threads. Keep transactions as short as possible — don't hold a transaction open while doing unrelated work (like an external API call) in the middle of it. Make sure the right indexes exist so queries take narrow, targeted locks instead of scanning and locking broad ranges. And consider a less restrictive isolation level (like `READ COMMITTED SNAPSHOT`) if the workload allows it, since it reduces the amount of blocking/locking overall."

```sql
-- Enable snapshot-based reads (readers don't block writers, less lock contention)
ALTER DATABASE MyDb SET READ_COMMITTED_SNAPSHOT ON;
```

**Where to use:** `READ_COMMITTED_SNAPSHOT` is a common, low-effort win for OLTP workloads with heavy read/write contention — readers use a row versioning snapshot instead of taking shared locks that could conflict with writers.

---

### Q7. How would you actually diagnose a deadlock that already happened in production?

**Answer:**
"Capture the deadlock graph — SQL Server can log this via Extended Events (the `xml_deadlock_report` event) or, in older setups, trace flag 1222 to the SQL Server error log. The graph shows exactly which two sessions/queries were involved, what resources (which rows/indexes) they held and were waiting on, and which one got picked as the victim. That's the starting point for figuring out which queries need reordering, indexing, or shorter transactions."

**Where to use:** set up an Extended Events session capturing `xml_deadlock_report` on any production database that sees deadlocks with any regularity — it's the only reliable way to see exactly what happened, since the error message alone doesn't show the other side of the deadlock.

---

### Q8. Are database deadlocks and application/thread deadlocks the same thing?

**Answer:**
"Conceptually yes — the same fundamental pattern: circular waiting on held resources. The key practical difference is that SQL Server actively detects and resolves database deadlocks automatically by killing a victim transaction within seconds, so the app just needs to catch the error and retry. A classic in-process thread deadlock has no such automatic detection — the threads just hang forever unless the app itself uses timeouts (like `Monitor.TryEnter`) to break out."

---

### Quick one-liner if asked to summarize

> "A database deadlock is two transactions each waiting on a lock the other holds. SQL Server detects this automatically and kills one transaction (the 'victim', chosen by cost or `DEADLOCK_PRIORITY`) with error 1205, rolling it back so the other can proceed. The app's job is to catch that error and retry with backoff. Prevention is about consistent access order, short transactions, proper indexing, and possibly `READ_COMMITTED_SNAPSHOT` to reduce lock contention overall."
 