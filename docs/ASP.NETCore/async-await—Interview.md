# async/await — Interview Q&A

---

### Q1. What's the difference between synchronous and asynchronous code?

**Answer:**
"Synchronous code blocks the calling thread until an operation finishes — the thread sits idle, doing nothing else. Asynchronous code, using `await`, releases the thread back to do other work and registers a continuation to resume once the operation completes. The thread is never blocked waiting."

```csharp
string result = CallDatabaseSync();         // thread blocked until this returns
string result2 = await CallDatabaseAsync(); // thread is free during the wait
```

**Where to use:** any operation that spends time waiting on something external — a database call, an HTTP request, a file read — should be async so the calling thread isn't wasted sitting idle.

### 🍽️ The Restaurant Analogy

#### Synchronous Restaurant (The Blocker)
* **The Process:** A customer sits down and orders food.
* **The Waiter's Action:** The waiter takes the order to the kitchen. The waiter then stands completely still at the kitchen window waiting for the chef to cook the food.
* **The Problem:** While the food is cooking, the waiter cannot take orders from new tables, refill water, or bring checks. The entire restaurant freezes because the waiter is blocked waiting for the kitchen.

#### Asynchronous Restaurant (The Non-Blocker)
* **The Process:** A customer sits down and orders food.
* **The Waiter's Action:** The waiter takes the order to the kitchen. Instead of waiting there, the waiter hands the order ticket to the chef and immediately returns to the dining floor.
* **The Result:** While the chef cooks the first order, the waiter greets new tables, serves drinks, and clears plates. When the chef yells, *"Order up!"*, the waiter pauses what they are doing, grabs the hot plate, and serves it. No one is left waiting around.

---

### Q2. What does `await` actually do to the thread?

**Answer:**
"It doesn't block. The compiler transforms the `async` method into a state machine — at each `await`, if the awaited task isn't already complete, the method returns control to its caller immediately, and the rest of the method becomes a continuation that runs later, on whatever thread is free when the awaited operation completes."

```csharp
public async Task<string> GetDataAsync()
{
    Console.WriteLine("before await");
    var result = await CallDatabaseAsync(); // control returns to caller here if not yet complete
    Console.WriteLine("after await");        // runs later, as a continuation
    return result;
}
```

---

### Q3. What's the difference between `Task` and `Task<T>`?

**Answer:**
"`Task` represents an operation that completes without producing a value — the awaitable equivalent of `void`. `Task<T>` represents an operation that completes and produces a result of type `T`, which you get by awaiting it."

```csharp
async Task LogAsync() { /* no return value */ }
async Task<int> GetCountAsync() { return 5; }

await LogAsync();
int count = await GetCountAsync();
```

---

### Q4. What's the difference between I/O-bound and CPU-bound async work?

**Answer:**
"I/O-bound work — a database call, an HTTP request, a file read — genuinely frees the calling thread while waiting, because the operation is handled by the OS/network/disk, not by a thread sitting and spinning. CPU-bound work wrapped in `await Task.Run(...)` doesn't free up any thread capacity overall — it just moves the computation onto a different thread-pool thread. Async doesn't create more processing power; it only avoids blocking a *specific* thread (like a UI thread) while the work happens elsewhere."

```csharp
await httpClient.GetAsync(url);               // I/O-bound - no thread spinning while waiting
await Task.Run(() => ComputeHash(largeFile));  // CPU-bound - moved to another thread, still real work
```

**Where this comes up as a trick question:** "does async make CPU-bound code faster?" — no. It doesn't add compute power; it just avoids blocking a specific thread. Wrapping CPU-bound work in `Task.Run` on a server already running on thread-pool threads usually adds overhead with no benefit.

---

### Q5. What's `Task.WhenAll`, and why does it matter?

**Answer:**
"It lets you run multiple independent async operations concurrently and wait for all of them to finish, instead of awaiting each one in sequence. If the operations don't depend on each other's results, awaiting them one at a time wastes time waiting for each individually when they could all be in flight simultaneously."

```csharp
// BAD - sequential, waits for each before starting the next
var a = await GetAAsync();
var b = await GetBAsync();
var c = await GetCAsync();

// GOOD - all start immediately, wait together for the slowest one
var taskA = GetAAsync();
var taskB = GetBAsync();
var taskC = GetCAsync();
await Task.WhenAll(taskA, taskB, taskC);
```

**Where to use:** whenever you have multiple independent async calls — fetching from several APIs, running several independent queries — start them all first, then `Task.WhenAll` them.

---

### Q6. Why is `async void` discouraged?

**Answer:**
"An `async void` method gives the caller nothing to await, and no way to observe whether it succeeded or failed. Exceptions thrown inside it can't be caught by a normal try/catch around the call — they get raised directly on the `SynchronizationContext` and can crash the process. `async Task` gives the caller a `Task` to await, inspect, and catch exceptions from normally."

```csharp
async void ProcessAsync() { await DoWorkAsync(); } // caller has nothing to await or catch from

async Task ProcessAsync() { await DoWorkAsync(); } // caller can await and catch exceptions normally
```

**Where `async void` is acceptable:** UI event handlers, where the delegate signature requires `void` — but wrap the body in try/catch since exceptions won't propagate normally.

---

### Q7. How does the classic async deadlock happen?

**Answer:**
"It happens when synchronous code blocks on an async call with `.Result` or `.Wait()`, from a context with a captured `SynchronizationContext` (WPF, WinForms, classic ASP.NET). The awaited method's continuation tries to resume on that same captured context — but that context's only thread is the one currently blocked waiting on `.Result`. Both sides wait on each other forever."

```csharp
// Deadlocks in a context with a SynchronizationContext (WPF/WinForms/classic ASP.NET)
var result = GetDataAsync().Result;

async Task<string> GetDataAsync()
{
    await Task.Delay(1000); // tries to resume on the blocked context
    return "data";
}
```

**Fix:** "async all the way" — `await` instead of blocking — or use `ConfigureAwait(false)` inside the async method if it must be called from sync code. Full detail in [[deadlock-qa]].

---

### Q8. What does `ConfigureAwait(false)` do?

**Answer:**
"It tells the awaited task's continuation that it doesn't need to resume on the originally captured `SynchronizationContext` — any thread-pool thread can run it. This avoids the deadlock scenario above and is slightly cheaper, since there's no need to marshal the continuation back to one specific thread."

```csharp
await CallDatabaseAsync().ConfigureAwait(false);
```

**Where to use:** library code that doesn't touch UI elements or anything thread-affinity-sensitive. Not needed in ASP.NET Core, which has no `SynchronizationContext` by default.

---

### Q9. Does the same thread run the code before and after an `await`?

**Answer:**
"Not necessarily. Once an `await` yields control, the continuation is queued to run on whatever thread is available when the awaited operation completes — could be the same thread, could be a different one from the thread pool. Code should never assume thread identity is preserved across an `await`, unless a `SynchronizationContext` specifically enforces it (e.g., a UI thread)."

```csharp
Console.WriteLine(Thread.CurrentThread.ManagedThreadId); // e.g., 4
await Task.Delay(1000);
Console.WriteLine(Thread.CurrentThread.ManagedThreadId); // could be 4, could be different
```

---

### Quick one-liner if asked to summarize

> "`await` doesn't block a thread — it registers a continuation and frees the thread to do other work until the awaited operation completes, then resumes on any available thread. Use `Task.WhenAll` for independent operations instead of awaiting sequentially, avoid `async void` except for event handlers, and never block on async code with `.Result`/`.Wait()` — that's the classic deadlock."
 