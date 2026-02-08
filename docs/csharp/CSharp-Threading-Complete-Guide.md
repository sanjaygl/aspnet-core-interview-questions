# Threading in C# - Complete Guide

> **Target Audience**: Mid to Senior .NET Developers  
> **Focus**: Threading, Synchronization, and Parallel Programming  
> **Last Updated**: February 2026 (.NET 8+)

---

## Table of Contents

1. [Introduction to Threading](#introduction-to-threading)
2. [Types of Threading in C#](#types-of-threading-in-c)
3. [Thread Synchronization Techniques](#thread-synchronization-techniques)
4. [Common Threading Issues](#common-threading-issues)
5. [Best Practices](#best-practices)
6. [Real-world Use Cases](#real-world-use-cases)
7. [Interview Quick Summary](#interview-quick-summary)

---

## Introduction to Threading

### What is a Thread?

A **thread** is the smallest unit of execution within a process. It represents a single path of execution through the program code.

**Key Points:**
- Executes code independently
- Has its own call stack
- Shares memory with other threads in the same process
- Managed by the operating system

### Process vs Thread

| Aspect | Process | Thread |
|--------|---------|--------|
| **Definition** | Independent program in execution | Lightweight unit within a process |
| **Memory** | Separate memory space | Shared memory within process |
| **Communication** | IPC (Inter-Process Communication) | Direct (shared memory) |
| **Creation Cost** | Expensive | Lightweight |
| **Isolation** | Fully isolated | Shares process resources |
| **Example** | Running application (Chrome, VS Code) | Single operation within app |

### Why Threading is Used

**1. Performance**
- Utilize multiple CPU cores
- Process data in parallel
- Reduce overall execution time

**2. Responsiveness**
- Keep UI responsive during long operations
- Handle multiple requests simultaneously
- Improve user experience

**3. Background Processing**
- Execute tasks without blocking main thread
- Process data asynchronously
- Handle scheduled operations

**Example:**

```csharp
// Without threading - blocks for 5 seconds
public void ProcessData()
{
    Thread.Sleep(5000); // Blocks everything
    Console.WriteLine("Done");
}

// With threading - runs in background
public void ProcessDataAsync()
{
    Task.Run(() =>
    {
        Thread.Sleep(5000); // Doesn't block main thread
        Console.WriteLine("Done");
    });
}
```

---

## Types of Threading in C#

### 1. Foreground vs Background Threads

#### Foreground Thread
- Keeps application alive
- Application waits for completion
- Created by default

#### Background Thread
- Doesn't keep application alive
- Terminates when main thread ends
- Set using `IsBackground = true`

**Example:**

```csharp
// Foreground thread (default)
Thread foregroundThread = new Thread(() =>
{
    Thread.Sleep(10000);
    Console.WriteLine("Foreground done");
});
foregroundThread.Start();
// App waits for this thread to complete

// Background thread
Thread backgroundThread = new Thread(() =>
{
    Thread.Sleep(10000);
    Console.WriteLine("Background done");
});
backgroundThread.IsBackground = true;
backgroundThread.Start();
// App can exit before this completes
```

---

### 2. Single-threading vs Multi-threading

#### Single-threading
- One task at a time
- Sequential execution
- Simple but slower

#### Multi-threading
- Multiple tasks simultaneously
- Parallel execution
- Complex but faster

**Example:**

```csharp
// Single-threaded (sequential)
for (int i = 0; i < 5; i++)
{
    ProcessItem(i); // One at a time
}

// Multi-threaded (parallel)
Parallel.For(0, 5, i =>
{
    ProcessItem(i); // All simultaneously
});
```

---

### 3. Manual Threads (Thread class)

Direct control over thread creation and lifecycle.

**When to use:** Rarely. Use Task instead in modern code.

**Example:**

```csharp
Thread thread = new Thread(() =>
{
    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} running");
});

thread.Start();
thread.Join(); // Wait for completion
```

**Drawbacks:**
- Manual management required
- No built-in exception handling
- Cannot return values easily

---

### 4. Thread Pool

Pre-created pool of worker threads for reusing.

**Advantages:**
- Reduces thread creation overhead
- Automatically manages thread lifecycle
- Limits concurrent threads

**Example:**

```csharp
ThreadPool.QueueUserWorkItem(_ =>
{
    Console.WriteLine("Executing on thread pool");
});

// Or using Task (uses thread pool internally)
Task.Run(() =>
{
    Console.WriteLine("Task on thread pool");
});
```

---

### 5. Task Parallel Library (TPL)

Modern approach for parallel and asynchronous operations.

**Key Features:**
- Built on top of Thread Pool
- Supports cancellation and continuation
- Exception handling built-in
- Can return values

**Example:**

```csharp
// Simple task
Task task = Task.Run(() =>
{
    Console.WriteLine("Task running");
});

// Task with return value
Task<int> taskWithResult = Task.Run(() =>
{
    return 42;
});

int result = await taskWithResult;
Console.WriteLine($"Result: {result}");
```

---

### 6. Asynchronous Programming (async / await)

Non-blocking operations for I/O-bound work.

**When to use:**
- File I/O
- Network calls
- Database operations
- Any I/O-bound work

**Example:**

```csharp
public async Task<string> FetchDataAsync()
{
    using HttpClient client = new HttpClient();
    
    // Non-blocking call
    string result = await client.GetStringAsync("https://api.example.com/data");
    
    return result;
}

// Usage
var data = await FetchDataAsync(); // Doesn't block thread
```

**Key Points:**
- Doesn't create new threads for I/O
- Frees thread while waiting
- More efficient than blocking

---

### 7. Parallel Programming

CPU-bound parallel processing.

#### Parallel.For

```csharp
Parallel.For(0, 100, i =>
{
    // Process items in parallel
    ProcessItem(i);
});
```

#### Parallel.ForEach

```csharp
var items = new List<int> { 1, 2, 3, 4, 5 };

Parallel.ForEach(items, item =>
{
    ProcessItem(item);
});
```

#### Parallel LINQ (PLINQ)

```csharp
var numbers = Enumerable.Range(1, 1000000);

var result = numbers
    .AsParallel()
    .Where(n => n % 2 == 0)
    .Select(n => n * n)
    .ToList();
```

**When to use:**
- CPU-intensive operations
- Large data processing
- Independent computations

---

## Thread Synchronization Techniques

### 1. lock (Monitor)

Simplest synchronization mechanism. Allows only one thread at a time.

**Example:**

```csharp
private static readonly object _lock = new object();
private static int _counter = 0;

public void IncrementCounter()
{
    lock (_lock)
    {
        _counter++; // Thread-safe
    }
}
```

**Use when:**
- Simple critical section
- Synchronous code
- Single resource protection

---

### 2. Mutex

Cross-process synchronization.

**Example:**

```csharp
using var mutex = new Mutex(false, "GlobalMutexName");

mutex.WaitOne(); // Wait for access
try
{
    // Critical section
    Console.WriteLine("Only one process can execute this");
}
finally
{
    mutex.ReleaseMutex();
}
```

**Use when:**
- Need cross-process synchronization
- System-wide resource locking

---

### 3. Semaphore / SemaphoreSlim

Limits number of concurrent threads.

**Example:**

```csharp
// Allow max 3 threads simultaneously
private static SemaphoreSlim _semaphore = new SemaphoreSlim(3, 3);

public async Task ProcessAsync()
{
    await _semaphore.WaitAsync();
    try
    {
        // Max 3 threads can be here
        await Task.Delay(1000);
    }
    finally
    {
        _semaphore.Release();
    }
}
```

**Use when:**
- Limiting concurrent operations
- API rate limiting
- Resource pool management (DB connections)

---

### 4. ReaderWriterLockSlim

Optimizes read-heavy scenarios.

**Example:**

```csharp
private static ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();
private static List<int> _data = new List<int>();

public int ReadData()
{
    _rwLock.EnterReadLock(); // Multiple readers allowed
    try
    {
        return _data.Count;
    }
    finally
    {
        _rwLock.ExitReadLock();
    }
}

public void WriteData(int value)
{
    _rwLock.EnterWriteLock(); // Only one writer
    try
    {
        _data.Add(value);
    }
    finally
    {
        _rwLock.ExitWriteLock();
    }
}
```

**Use when:**
- Frequent reads, infrequent writes
- Shared data access optimization

---

### When to Use Each Synchronization Technique

| Scenario | Recommended Technique |
|----------|----------------------|
| Simple critical section (sync) | `lock` |
| Async critical section | `SemaphoreSlim(1, 1)` |
| Limit concurrent operations | `SemaphoreSlim` |
| Cross-process sync | `Mutex` |
| Read-heavy, write-light | `ReaderWriterLockSlim` |
| Database connection pool | `SemaphoreSlim` |
| API rate limiting | `SemaphoreSlim` |

---

## Common Threading Issues

### 1. Race Condition

Multiple threads access shared data simultaneously, causing unpredictable results.

**Problem:**

```csharp
private int _counter = 0;

public void Increment()
{
    _counter++; // Not thread-safe!
    // Steps: Read → Modify → Write (can be interrupted)
}

// Two threads calling Increment() simultaneously
// Expected: counter = 2
// Actual: counter = 1 (race condition)
```

**Solution:**

```csharp
private readonly object _lock = new object();
private int _counter = 0;

public void Increment()
{
    lock (_lock)
    {
        _counter++; // Thread-safe
    }
}
```

---

### 2. Deadlock

Two or more threads wait for each other indefinitely.

**Problem:**

```csharp
object lock1 = new object();
object lock2 = new object();

// Thread 1
lock (lock1)
{
    Thread.Sleep(100);
    lock (lock2) // Waits for Thread 2 to release lock2
    {
        // Work
    }
}

// Thread 2
lock (lock2)
{
    Thread.Sleep(100);
    lock (lock1) // Waits for Thread 1 to release lock1
    {
        // Work
    }
}
// DEADLOCK: Both threads wait forever
```

**Solution: Lock in consistent order**

```csharp
// Both threads lock in same order
lock (lock1)
{
    lock (lock2)
    {
        // Work
    }
}
```

---

### 3. Thread Starvation

A thread cannot get access to resources because other threads monopolize them.

**Problem:**

```csharp
// High-priority threads always get lock first
// Low-priority thread never gets chance
lock (_lock)
{
    // High-priority work keeps running
}
```

**Solution:**
- Use fair scheduling mechanisms
- Limit execution time
- Use `SemaphoreSlim` with timeouts

---

### 4. Context Switching Overhead

Too many threads cause performance degradation.

**Problem:**

```csharp
// Creating 10,000 threads
for (int i = 0; i < 10000; i++)
{
    new Thread(() => DoWork()).Start();
}
// CPU spends more time switching than working
```

**Solution:**

```csharp
// Use Thread Pool or limit concurrency
var options = new ParallelOptions { MaxDegreeOfParallelism = 10 };
Parallel.For(0, 10000, options, i => DoWork());
```

---

## Best Practices

### 1. Prefer Task over Thread

**❌ Don't:**

```csharp
Thread thread = new Thread(() => DoWork());
thread.Start();
```

**✅ Do:**

```csharp
Task.Run(() => DoWork());
```

**Why:** Task uses thread pool, has better exception handling, and supports async patterns.

---

### 2. Use async/await for I/O Operations

**❌ Don't:**

```csharp
public string GetData()
{
    var client = new HttpClient();
    var result = client.GetStringAsync("url").Result; // Blocks thread
    return result;
}
```

**✅ Do:**

```csharp
public async Task<string> GetDataAsync()
{
    var client = new HttpClient();
    var result = await client.GetStringAsync("url"); // Non-blocking
    return result;
}
```

**Why:** Doesn't block threads, more scalable.

---

### 3. Avoid Blocking Calls

**❌ Don't:**

```csharp
task.Wait();        // Blocks
task.Result;        // Blocks
Task.WaitAll();     // Blocks
```

**✅ Do:**

```csharp
await task;                    // Non-blocking
await Task.WhenAll(tasks);     // Non-blocking
```

---

### 4. Minimize Shared State

**❌ Don't:**

```csharp
private int _sharedCounter = 0; // Multiple threads modify

public void Process()
{
    _sharedCounter++; // Race condition
}
```

**✅ Do:**

```csharp
public void Process()
{
    int localCounter = 0; // Thread-local
    localCounter++;
}

// Or use proper synchronization if sharing is necessary
```

---

### 5. Use Proper Synchronization

**❌ Don't:**

```csharp
private List<int> _items = new List<int>();

public void Add(int item)
{
    _items.Add(item); // Not thread-safe
}
```

**✅ Do:**

```csharp
private readonly object _lock = new object();
private List<int> _items = new List<int>();

public void Add(int item)
{
    lock (_lock)
    {
        _items.Add(item);
    }
}

// Or use thread-safe collections
private ConcurrentBag<int> _items = new ConcurrentBag<int>();

public void Add(int item)
{
    _items.Add(item); // Thread-safe
}
```

---

### 6. Always Release Locks

**❌ Don't:**

```csharp
_semaphore.WaitAsync();
DoWork(); // If exception, lock never released
_semaphore.Release();
```

**✅ Do:**

```csharp
await _semaphore.WaitAsync();
try
{
    DoWork();
}
finally
{
    _semaphore.Release(); // Always released
}
```

---

### 7. Use CancellationToken

**✅ Do:**

```csharp
public async Task ProcessAsync(CancellationToken cancellationToken)
{
    while (!cancellationToken.IsCancellationRequested)
    {
        await DoWorkAsync();
    }
}

// Usage
var cts = new CancellationTokenSource();
var task = ProcessAsync(cts.Token);

// Cancel when needed
cts.Cancel();
```

---

## Real-world Use Cases

### 1. Background Processing

**Scenario:** Send emails without blocking user request.

```csharp
[HttpPost("register")]
public async Task<IActionResult> RegisterUser(UserDto user)
{
    // Save user (fast)
    await _userService.CreateUserAsync(user);
    
    // Send welcome email in background (slow)
    _ = Task.Run(async () =>
    {
        await _emailService.SendWelcomeEmailAsync(user.Email);
    });
    
    return Ok("User registered");
}
```

---

### 2. Parallel API Calls

**Scenario:** Call multiple APIs simultaneously.

```csharp
public async Task<DashboardData> GetDashboardDataAsync()
{
    // Call 3 APIs in parallel
    var userTask = _userService.GetUserAsync();
    var ordersTask = _orderService.GetOrdersAsync();
    var statsTask = _statsService.GetStatsAsync();
    
    await Task.WhenAll(userTask, ordersTask, statsTask);
    
    return new DashboardData
    {
        User = userTask.Result,
        Orders = ordersTask.Result,
        Stats = statsTask.Result
    };
}
```

---

### 3. File Processing

**Scenario:** Process large files line by line.

```csharp
public async Task ProcessLargeFileAsync(string filePath)
{
    using var reader = new StreamReader(filePath);
    
    string line;
    while ((line = await reader.ReadLineAsync()) != null)
    {
        await ProcessLineAsync(line);
    }
}
```

---

### 4. Parallel Data Processing

**Scenario:** Process large dataset in parallel.

```csharp
public async Task<List<ProcessedData>> ProcessDataAsync(List<RawData> data)
{
    var results = new ConcurrentBag<ProcessedData>();
    
    await Parallel.ForEachAsync(data, 
        new ParallelOptions { MaxDegreeOfParallelism = 10 },
        async (item, ct) =>
        {
            var processed = await ProcessItemAsync(item);
            results.Add(processed);
        });
    
    return results.ToList();
}
```

---

### 5. API Rate Limiting

**Scenario:** Limit concurrent API calls to 5.

```csharp
private static SemaphoreSlim _apiSemaphore = new SemaphoreSlim(5, 5);

public async Task<string> CallApiAsync(string url)
{
    await _apiSemaphore.WaitAsync();
    try
    {
        using var client = new HttpClient();
        return await client.GetStringAsync(url);
    }
    finally
    {
        _apiSemaphore.Release();
    }
}
```

---

### 6. Database Connection Pooling

**Scenario:** Limit concurrent database operations.

```csharp
private static SemaphoreSlim _dbSemaphore = new SemaphoreSlim(20, 20);

public async Task<User> GetUserAsync(int id)
{
    await _dbSemaphore.WaitAsync();
    try
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        // Execute query
        return await GetUserFromDbAsync(connection, id);
    }
    finally
    {
        _dbSemaphore.Release();
    }
}
```

---

## Interview Quick Summary

### Key Concepts

**Threading Basics:**
- Thread = smallest unit of execution
- Process = isolated program, Thread = execution path within process
- Used for performance, responsiveness, background processing

**Types of Threading:**
- **Foreground Thread** – Keeps app alive
- **Background Thread** – Terminates with main thread
- **Thread Pool** – Pre-created reusable threads
- **Task (TPL)** – Modern, preferred approach
- **async/await** – Non-blocking I/O operations
- **Parallel** – CPU-bound parallel processing

**Synchronization:**
- **lock** – Simple, one thread, synchronous
- **Mutex** – Cross-process synchronization
- **Semaphore/SemaphoreSlim** – Limit concurrent access
- **ReaderWriterLockSlim** – Multiple readers, one writer

**Common Issues:**
- **Race Condition** – Simultaneous access to shared data
- **Deadlock** – Threads wait for each other
- **Thread Starvation** – Thread can't get resources
- **Context Switching** – Too many threads hurt performance

**Best Practices:**
- ✅ Use Task over Thread
- ✅ Use async/await for I/O
- ✅ Avoid blocking (.Wait(), .Result)
- ✅ Minimize shared state
- ✅ Use proper synchronization
- ✅ Always release locks (try/finally)
- ✅ Use CancellationToken

**Real-world Uses:**
- Background email sending
- Parallel API calls
- File processing
- Data processing
- Rate limiting
- Connection pooling

---

## Quick Reference Table

| Scenario | Solution |
|----------|----------|
| CPU-intensive work | `Parallel.For`, `Task.Run` |
| I/O operations | `async/await` |
| Background task | `Task.Run` |
| Limit concurrency | `SemaphoreSlim` |
| Protect shared data | `lock` or `SemaphoreSlim` |
| Read-heavy operations | `ReaderWriterLockSlim` |
| Cross-process sync | `Mutex` |
| Cancel operation | `CancellationToken` |
| Multiple API calls | `Task.WhenAll` |
| Rate limiting | `SemaphoreSlim` |

---

## Further Reading

- [Microsoft Docs: Threading](https://docs.microsoft.com/en-us/dotnet/standard/threading/)
- [Task Parallel Library](https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/)
- [Async/Await Best Practices](https://docs.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming)

---

**Last Updated:** February 2026  
**Target Framework:** .NET 8+
