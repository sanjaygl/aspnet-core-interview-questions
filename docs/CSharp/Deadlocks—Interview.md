# Deadlocks — Interview Q&A

---

### Q1. What is a deadlock?

**Answer:**
"A deadlock happens when two or more threads are each waiting on a resource the other one holds, so none of them can ever proceed. Classic example: Thread A locks Resource 1 and then waits for Resource 2; Thread B locks Resource 2 and waits for Resource 1. Neither releases what it's holding, so both wait forever."

```csharp
object lockA = new object();
object lockB = new object();

// Thread 1
lock (lockA)
{
    Thread.Sleep(100);
    lock (lockB) { /* ... */ }   // waits forever if Thread 2 already holds lockB
}

// Thread 2 (running concurrently)
lock (lockB)
{
    Thread.Sleep(100);
    lock (lockA) { /* ... */ }   // waits forever if Thread 1 already holds lockA
}
```

---

### Q2. What are the four conditions that must all be true for a deadlock to occur?

**Answer:**
"Mutual exclusion — a resource can only be held by one thread at a time. Hold and wait — a thread holds one resource while waiting for another. No preemption — a resource can't be forcibly taken away from a thread; it has to release it voluntarily. And circular wait — there's a cycle of threads, each waiting on a resource the next one holds. Break any one of these and a deadlock becomes impossible — that's the basis for most prevention strategies."

---

### Q3. How do you prevent the classic circular-wait deadlock?

**Answer:**
"The most reliable fix is enforcing a consistent lock ordering — always acquire locks in the same global order across every thread, no matter which one you 'need first' logically. If every thread agrees to lock Resource 1 before Resource 2, a circular wait becomes structurally impossible."

```csharp
// Fix: BOTH threads always lock in the same order — lockA, then lockB
void ThreadWork()
{
    lock (lockA)
    {
        lock (lockB)
        {
            // safe — no other thread will ever hold lockB while waiting for lockA
        }
    }
}
```

**Where to use:** any code path that needs to hold multiple locks at once — document and enforce one fixed acquisition order across the whole codebase.

---

### Q4. What's `Monitor.TryEnter`, and how does it help avoid deadlocks?

**Answer:**
"`Monitor.TryEnter` attempts to acquire a lock with a timeout, instead of waiting indefinitely like `lock` does. If it can't get the lock within the timeout, it gives up and returns `false`, letting you back off, release what you're already holding, and retry — instead of getting stuck forever."

```csharp
if (Monitor.TryEnter(lockA, TimeSpan.FromSeconds(2)))
{
    try
    {
        if (Monitor.TryEnter(lockB, TimeSpan.FromSeconds(2)))
        {
            try { /* do work */ }
            finally { Monitor.Exit(lockB); }
        }
        else
        {
            // couldn't get lockB in time — back off and retry later instead of deadlocking
        }
    }
    finally { Monitor.Exit(lockA); }
}
```

**Where to use:** situations where strict lock ordering isn't practical (e.g., locking objects chosen dynamically at runtime) — a timeout-based approach at least prevents an infinite hang, converting a deadlock into a retryable failure.

---

### Q5. What's the "async deadlock" that trips people up in .NET, separate from classic lock deadlocks?

**Answer:**
"It happens when you block synchronously on an async call — calling `.Result` or `.Wait()` on a `Task` — from a context that has a captured `SynchronizationContext` (like an older ASP.NET or a WPF/WinForms UI thread). The async method, once it hits an `await`, tries to resume back on that same captured context. But that context's single thread is the same one that's blocked waiting on `.Result` — so the two are stuck waiting on each other, on a single thread."

```csharp
// Classic ASP.NET (non-Core) or WPF/WinForms deadlock
public ActionResult Index()
{
    var result = GetDataAsync().Result; // BLOCKS the UI/request thread
    return View(result);
}

async Task<string> GetDataAsync()
{
    await Task.Delay(1000); // tries to resume on the captured context...
    return "data";           // ...which is still blocked waiting on .Result above
}
```

**Where this comes up:** less common in ASP.NET Core (no `SynchronizationContext` by default), but still very real in WPF, WinForms, and classic ASP.NET (.NET Framework) — a frequently-asked "gotcha" interview question.

---

### Q6. How do you avoid the async deadlock?

**Answer:**
"Two main fixes: 'async all the way' — never block on async code with `.Result`/`.Wait()`, use `await` all the way up the call stack instead. If you genuinely can't await (e.g., inside a library that must stay synchronous), use `.ConfigureAwait(false)` inside the async method, which tells it not to try to resume on the original captured context, avoiding the circular wait."

```csharp
// Fix 1 (preferred) - async all the way, no blocking
public async Task<ActionResult> Index()
{
    var result = await GetDataAsync();
    return View(result);
}

// Fix 2 - if you truly must call from sync code, avoid capturing the context inside the async method
async Task<string> GetDataAsync()
{
    await Task.Delay(1000).ConfigureAwait(false); // doesn't need to resume on the original context
    return "data";
}
```

**Where to use:** `ConfigureAwait(false)` is standard practice in library code (that doesn't know or care what context called it); "async all the way" is the standard practice in application/UI code.

---

### Q7. How would you detect or diagnose a deadlock in a running application?

**Answer:**
"For classic lock deadlocks, attach a debugger and check the Threads/Parallel Stacks window in Visual Studio — you'll see threads blocked in `Monitor.Enter`/`lock`, and can trace which thread holds which lock and what it's waiting on. In production, a memory dump analyzed with WinDbg/`!syncblk` (for lock objects) shows the same picture. For async deadlocks, the tell-tale sign is a hung request/UI with a thread stuck inside `.Result`/`.Wait()`, combined with an async continuation that never got scheduled — visible in a dump as a `Task` stuck in `WaitingForActivation`."

---

### Q8. What's the difference between a deadlock and a livelock?

**Answer:**
"In a deadlock, threads are stuck completely — blocked, doing nothing, waiting forever. In a livelock, threads are still actively running and responding to each other, but they keep changing state in a way that never actually makes progress — like two people repeatedly stepping aside for each other in a hallway and never getting past one another. Livelocks are actually harder to spot because CPU usage looks 'busy', not stuck."

---

### Quick one-liner if asked to summarize

> "A deadlock is two or more threads each waiting on a resource the other holds, so neither can proceed. Prevent it with a consistent lock-acquisition order, or use `Monitor.TryEnter` with a timeout so a stuck attempt can back off instead of hanging forever. A related but distinct .NET-specific version is the async deadlock — blocking on `.Result`/`.Wait()` from a context that the async continuation needs to resume on — fixed by going 'async all the way' or using `ConfigureAwait(false)` in library code."
 