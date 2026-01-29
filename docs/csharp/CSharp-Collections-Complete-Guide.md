# C# Collections - Complete Technical Guide

> **Comprehensive Interview Guide** | . NET 6, 7, 8+ | Beginner to Expert Level

---

## 📑 Table of Contents

### [Core Concepts](#core-concepts)

01. [What are Collections in C#?](#1-what-are-collections-in-c)
02. [Difference between Array and Collection?](#2-difference-between-array-and-collection)
03. [What are Generic and Non-Generic Collections?](#3-what-are-generic-and-non-generic-collections)
04. [Why prefer Generic Collections over Non-Generic?](#4-why-prefer-generic-collections-over-non-generic)

### [List and ArrayList](#list-and-arraylist)

05. [What is List<T>?](#5-what-is-listt)
06. [What is ArrayList?](#6-what-is-arraylist)
07. [Difference between List<T> and ArrayList?](#7-difference-between-listt-and-arraylist)
08. [When to use List<T>?](#8-when-to-use-listt)
09. [How does List<T> resize internally?](#9-how-does-listt-resize-internally)
10. [What is the capacity of List<T>?](#10-what-is-the-capacity-of-listt)

### [Dictionary and Hashtable](#dictionary-and-hashtable)

11. [What is Dictionary<TKey, TValue>?](#11-what-is-dictionarytkey-tvalue)
12. [What is Hashtable?](#12-what-is-hashtable)
13. [Difference between Dictionary and Hashtable?](#13-difference-between-dictionary-and-hashtable)
14. [How does Dictionary handle collisions?](#14-how-does-dictionary-handle-collisions)
15. [What happens if key doesn't exist in Dictionary?](#15-what-happens-if-key-doesnt-exist-in-dictionary)
16. [What is TryGetValue in Dictionary?](#16-what-is-trygetvalue-in-dictionary)
17. [Can Dictionary have duplicate keys?](#17-can-dictionary-have-duplicate-keys)

### [HashSet and SortedSet](#hashset-and-sortedset)

18. [What is HashSet<T>?](#18-what-is-hashsett)
19. [What is SortedSet<T>?](#19-what-is-sortedsett)
20. [Difference between HashSet and List?](#20-difference-between-hashset-and-list)
21. [When to use HashSet?](#21-when-to-use-hashset)
22. [What are set operations in HashSet?](#22-what-are-set-operations-in-hashset)

### [Queue and Stack](#queue-and-stack)

23. [What is Queue<T>?](#23-what-is-queuet)
24. [What is Stack<T>?](#24-what-is-stackt)
25. [Difference between Queue and Stack?](#25-difference-between-queue-and-stack)
26. [Real-time use case of Queue?](#26-real-time-use-case-of-queue)
27. [Real-time use case of Stack?](#27-real-time-use-case-of-stack)

### [LinkedList](#linkedlist)

28. [What is LinkedList<T>?](#28-what-is-linkedlistt)
29. [Difference between List and LinkedList?](#29-difference-between-list-and-linkedlist)
30. [When to use LinkedList?](#30-when-to-use-linkedlist)

### [Concurrent Collections](#concurrent-collections)

31. [What are Concurrent Collections?](#31-what-are-concurrent-collections)
32. [What is ConcurrentDictionary?](#32-what-is-concurrentdictionary)
33. [What is ConcurrentQueue?](#33-what-is-concurrentqueue)
34. [What is ConcurrentBag?](#34-what-is-concurrentbag)
35. [When to use Concurrent Collections?](#35-when-to-use-concurrent-collections)

### [Immutable Collections](#immutable-collections)

36. [What are Immutable Collections?](#36-what-are-immutable-collections)
37. [Benefits of Immutable Collections?](#37-benefits-of-immutable-collections)
38. [What is ImmutableList?](#38-what-is-immutablelist)
39. [What is ImmutableDictionary?](#39-what-is-immutabledictionary)

### [Specialized Collections](#specialized-collections)

40. [What is SortedList?](#40-what-is-sortedlist)
41. [What is SortedDictionary?](#41-what-is-sorteddictionary)
42. [Difference between SortedList and SortedDictionary?](#42-difference-between-sortedlist-and-sorteddictionary)
43. [What is ObservableCollection?](#43-what-is-observablecollection)
44. [What is BitArray?](#44-what-is-bitarray)

### [Interfaces](#interfaces)

45. [What is IEnumerable<T>?](#45-what-is-ienumerablet)
46. [What is ICollection<T>?](#46-what-is-icollectiont)
47. [What is IList<T>?](#47-what-is-ilistt)
48. [Difference between IEnumerable and ICollection?](#48-difference-between-ienumerable-and-icollection)
49. [What is IReadOnlyCollection<T>?](#49-what-is-ireadonlycollectiont)
50. [What is IReadOnlyList<T>?](#50-what-is-ireadonlylistt)

### [Performance and Best Practices](#performance-and-best-practices)

51. [Time complexity of List operations?](#51-time-complexity-of-list-operations)
52. [Time complexity of Dictionary operations?](#52-time-complexity-of-dictionary-operations)
53. [Time complexity of HashSet operations?](#53-time-complexity-of-hashset-operations)
54. [When to use List vs LinkedList?](#54-when-to-use-list-vs-linkedlist)
55. [When to use Dictionary vs List?](#55-when-to-use-dictionary-vs-list)
56. [How to avoid boxing/unboxing in collections?](#56-how-to-avoid-boxingunboxing-in-collections)
57. [What is Collection initialization syntax?](#57-what-is-collection-initialization-syntax)
58. [What is the benefit of AsReadOnly()?](#58-what-is-the-benefit-of-asreadonly)

### [Advanced Topics](#advanced-topics)

59. [What is yield return in collections?](#59-what-is-yield-return-in-collections)
60. [What is lazy evaluation with IEnumerable?](#60-what-is-lazy-evaluation-with-ienumerable)
61. [Difference between ToList() and AsEnumerable()?](#61-difference-between-tolist-and-asenumerable)
62. [What is the difference between Add and AddRange?](#62-what-is-the-difference-between-add-and-addrange)
63. [How to sort collections in C#?](#63-how-to-sort-collections-in-c)
64. [What is IComparer and IComparable?](#64-what-is-icomparer-and-icomparable)
65. [What is the purpose of EqualityComparer?](#65-what-is-the-purpose-of-equalitycomparer)

### [Practical Scenarios](#practical-scenarios)

66. [How to remove duplicates from List?](#66-how-to-remove-duplicates-from-list)
67. [How to find common elements between two lists?](#67-how-to-find-common-elements-between-two-lists)
68. [How to merge two dictionaries?](#68-how-to-merge-two-dictionaries)
69. [How to convert List to Dictionary?](#69-how-to-convert-list-to-dictionary)
70. [How to check if collection is null or empty?](#70-how-to-check-if-collection-is-null-or-empty)

---

## Core Concepts

### 1. What are Collections in C#?

**Answer:**  
Collections are data structures that store and manage groups of related objects. They provide methods for adding, removing, searching, and sorting elements. Collections are part of `System.Collections` and `System.Collections.Generic` namespaces.

**Example:**

```csharp
List<string> names = new List<string> { "Alice", "Bob", "Charlie" };
Dictionary<int, string> employees = new Dictionary<int, string>();
```

---

### 2. Difference between Array and Collection?

**Answer:**  
Arrays have fixed size and are type-safe but cannot grow dynamically. Collections can grow/shrink dynamically and provide rich functionality like sorting, searching, and filtering. Arrays are faster for indexed access, collections offer more flexibility.

**Example:**

```csharp
// Array - Fixed size
string[] arr = new string[3];

// Collection - Dynamic size
List<string> list = new List<string>();
list.Add("Item1"); // Can grow
```

---

### 3. What are Generic and Non-Generic Collections?

**Answer:**  
Generic collections ( `List<T>` , `Dictionary<K,V>` ) are type-safe and avoid boxing/unboxing. Non-generic collections ( `ArrayList` , `Hashtable` ) store objects and require type casting. Generics provide better performance and compile-time type checking.

**Example:**

```csharp
// Generic - Type-safe
List<int> numbers = new List<int> { 1, 2, 3 };

// Non-Generic - Not type-safe
ArrayList list = new ArrayList();
list.Add(1); // Boxing occurs
```

---

### 4. Why prefer Generic Collections over Non-Generic?

**Answer:**  
Generic collections provide type safety, eliminate boxing/unboxing overhead, catch type errors at compile-time, and offer better performance. They reduce runtime errors and improve code readability. Non-generic collections are legacy and should be avoided in modern C#.

**Real-time Example:**

```csharp
// Bad - Non-Generic (Boxing)
ArrayList items = new ArrayList();
items.Add(100); // Boxing int to object
int val = (int)items[0]; // Unboxing

// Good - Generic (No Boxing)
List<int> items = new List<int>();
items.Add(100); // No boxing
int val = items[0]; // No casting
```

---

## List and ArrayList

### 5. What is List<T>?

**Answer:**  
`List<T>` is a generic collection that stores elements in a dynamic array. It provides indexed access, automatic resizing, and rich methods like Add, Remove, Sort, and Find. It's the most commonly used collection for ordered data.

**Example:**

```csharp
List<int> numbers = new List<int> { 1, 2, 3 };
numbers.Add(4);
numbers.Remove(2);
int first = numbers[0]; // Indexed access
```

**Real-time:** Storing shopping cart items in an e-commerce application.

---

### 6. What is ArrayList?

**Answer:**  
`ArrayList` is a non-generic collection that stores objects dynamically. It requires type casting when retrieving elements and suffers from boxing/unboxing overhead. It's obsolete and replaced by `List<T>` in modern C#.

**Example:**

```csharp
ArrayList list = new ArrayList();
list.Add(10); // Boxing
list.Add("text"); // Different types allowed
int num = (int)list[0]; // Unboxing and casting required
```

---

### 7. Difference between List<T> and ArrayList?

**Answer:**  
`List<T>` is generic, type-safe, and avoids boxing. `ArrayList` is non-generic, stores objects, requires casting, and has performance overhead. `List<T>` catches type errors at compile-time, while `ArrayList` throws runtime errors.

**Example:**

```csharp
// List<T> - Compile-time error
List<int> list = new List<int>();
// list.Add("text"); // Compile error

// ArrayList - Runtime error
ArrayList arrayList = new ArrayList();
arrayList.Add(10);
arrayList.Add("text"); // Allowed
// int x = (int)arrayList[1]; // Runtime exception
```

---

### 8. When to use List<T>?

**Answer:**  
Use `List<T>` when you need indexed access, dynamic sizing, and frequent additions at the end. It's ideal for ordered collections where you know the approximate size or need to iterate frequently.

**Real-time Example:**

```csharp
// Storing user search history
List<string> searchHistory = new List<string>();
searchHistory.Add("C# collections");
searchHistory.Add("ASP.NET Core");
// Display recent 10 searches
var recent = searchHistory.TakeLast(10);
```

---

### 9. How does List<T> resize internally?

**Answer:**  
`List<T>` starts with default capacity (0 or 4) and doubles its capacity when full. It allocates a new array, copies existing elements, and discards the old array. This amortized O(1) operation minimizes resizing overhead.

**Example:**

```csharp
List<int> list = new List<int>(2); // Capacity: 2
list.Add(1); // Capacity: 2
list.Add(2); // Capacity: 2
list.Add(3); // Resizes to Capacity: 4
list.Add(4); // Capacity: 4
list.Add(5); // Resizes to Capacity: 8
```

---

### 10. What is the capacity of List<T>?

**Answer:**  
Capacity is the size of the internal array that List<T> allocates. Count is the actual number of elements. When Count exceeds Capacity, List<T> automatically resizes. You can set initial capacity to avoid frequent resizing.

**Example:**

```csharp
List<int> list = new List<int>(100); // Initial capacity: 100
Console.WriteLine($"Capacity: {list.Capacity}, Count: {list.Count}");
// Output: Capacity: 100, Count: 0

list.Add(1);
Console.WriteLine($"Capacity: {list.Capacity}, Count: {list.Count}");
// Output: Capacity: 100, Count: 1
```

---

## Dictionary and Hashtable

### 11. What is Dictionary<TKey, TValue>?

**Answer:**  
`Dictionary<TKey, TValue>` is a generic collection of key-value pairs with fast lookups (O(1) average). Keys must be unique and immutable. It uses hash table internally for efficient retrieval, insertion, and deletion.

**Example:**

```csharp
Dictionary<int, string> employees = new Dictionary<int, string>
{
    { 1, "John" },
    { 2, "Jane" }
};
string name = employees[1]; // O(1) lookup
```

**Real-time:** Caching user sessions by session ID.

---

### 12. What is Hashtable?

**Answer:**  
`Hashtable` is a non-generic collection of key-value pairs that stores objects. It requires boxing/unboxing and type casting. Keys and values can be of different types, making it error-prone and slower than `Dictionary<TKey, TValue>` .

**Example:**

```csharp
Hashtable table = new Hashtable();
table.Add(1, "One");
table.Add("Key", 100); // Different types allowed
string value = (string)table[1]; // Casting required
```

---

### 13. Difference between Dictionary and Hashtable?

**Answer:**  
`Dictionary` is generic, type-safe, and faster with no boxing. `Hashtable` is non-generic, requires casting, and has boxing overhead. `Dictionary` is thread-unsafe but has better performance, while `Hashtable` offers thread-safe operations with synchronization.

**Example:**

```csharp
// Dictionary - Type-safe
Dictionary<int, string> dict = new Dictionary<int, string>();
dict.Add(1, "Value");
// dict.Add("Key", "Value"); // Compile error

// Hashtable - Not type-safe
Hashtable hash = new Hashtable();
hash.Add(1, "Value");
hash.Add("Key", 100); // Allowed, runtime issues possible
```

---

### 14. How does Dictionary handle collisions?

**Answer:**  
Dictionary uses chaining to handle hash collisions. When multiple keys hash to the same bucket, it stores them in a linked list or array within that bucket. It uses `GetHashCode()` and `Equals()` to resolve conflicts.

**Example:**

```csharp
// Custom key with hash collision handling
public class CustomKey
{
    public int Id { get; set; }
    
    public override int GetHashCode() => Id % 10; // May cause collisions
    
    public override bool Equals(object obj) => 
        obj is CustomKey key && key.Id == Id;
}
```

---

### 15. What happens if key doesn't exist in Dictionary?

**Answer:**  
Accessing a non-existent key with indexer throws `KeyNotFoundException` . Use `ContainsKey()` to check existence or `TryGetValue()` for safe retrieval. This prevents runtime exceptions and improves code robustness.

**Example:**

```csharp
Dictionary<int, string> dict = new Dictionary<int, string>();
dict.Add(1, "One");

// Throws KeyNotFoundException
// string value = dict[2]; 

// Safe approach
if (dict.ContainsKey(2))
    string value = dict[2];
```

---

### 16. What is TryGetValue in Dictionary?

**Answer:**  
`TryGetValue()` attempts to retrieve a value without throwing exceptions. It returns true if key exists and outputs the value, otherwise returns false. It's more efficient than checking with `ContainsKey()` then accessing.

**Example:**

```csharp
Dictionary<int, string> dict = new Dictionary<int, string>
{
    { 1, "One" },
    { 2, "Two" }
};

if (dict.TryGetValue(1, out string value))
    Console.WriteLine(value); // Output: One
else
    Console.WriteLine("Key not found");
```

**Real-time:** Safely retrieving configuration settings without crashes.

---

### 17. Can Dictionary have duplicate keys?

**Answer:**  
No, Dictionary doesn't allow duplicate keys. Adding a duplicate key throws `ArgumentException` . Keys must be unique and are checked using `Equals()` and `GetHashCode()` . Use indexer to update existing key's value.

**Example:**

```csharp
Dictionary<int, string> dict = new Dictionary<int, string>();
dict.Add(1, "One");
// dict.Add(1, "Another"); // Throws ArgumentException

// Update existing key
dict[1] = "Updated One"; // Allowed
```

---

## HashSet and SortedSet

### 18. What is HashSet<T>?

**Answer:**  
`HashSet<T>` is an unordered collection of unique elements with O(1) add, remove, and contains operations. It doesn't allow duplicates and uses hash table internally. Ideal for membership testing and removing duplicates.

**Example:**

```csharp
HashSet<int> numbers = new HashSet<int> { 1, 2, 3 };
numbers.Add(2); // Duplicate ignored
bool exists = numbers.Contains(2); // O(1)
```

**Real-time:** Storing unique user IDs in an active session manager.

---

### 19. What is SortedSet<T>?

**Answer:**  
`SortedSet<T>` maintains elements in sorted order with O(log n) operations. It uses red-black tree internally and doesn't allow duplicates. Useful when you need both uniqueness and sorted order.

**Example:**

```csharp
SortedSet<int> numbers = new SortedSet<int> { 5, 1, 3, 2 };
// Stored as: 1, 2, 3, 5
numbers.Add(4); // Inserted in sorted position
```

**Real-time:** Maintaining sorted list of high scores in a game.

---

### 20. Difference between HashSet and List?

**Answer:**  
`HashSet` stores unique elements with O(1) lookups but no ordering or indexing. `List` allows duplicates, maintains insertion order, and supports indexed access but has O(n) contains operation. Choose based on uniqueness and access pattern needs.

**Example:**

```csharp
// HashSet - No duplicates, fast lookup
HashSet<int> set = new HashSet<int> { 1, 2, 2, 3 };
// Contains: 1, 2, 3

// List - Allows duplicates, indexed
List<int> list = new List<int> { 1, 2, 2, 3 };
// Contains: 1, 2, 2, 3
int first = list[0]; // Indexed access
```

---

### 21. When to use HashSet?

**Answer:**  
Use `HashSet` when you need unique elements, fast membership testing, or performing set operations (union, intersection, difference). It's perfect for removing duplicates, checking existence, or implementing mathematical set operations.

**Real-time Example:**

```csharp
// Remove duplicates from list
List<int> numbers = new List<int> { 1, 2, 2, 3, 3, 4 };
HashSet<int> unique = new HashSet<int>(numbers);
// unique contains: 1, 2, 3, 4

// Check if email already registered
HashSet<string> registeredEmails = new HashSet<string>();
if (!registeredEmails.Add(email))
    Console.WriteLine("Email already registered");
```

---

### 22. What are set operations in HashSet?

**Answer:**  
HashSet provides mathematical set operations: `UnionWith` (combines sets), `IntersectWith` (common elements), `ExceptWith` (difference), and `SymmetricExceptWith` (elements in either but not both). These operations modify the original set.

**Example:**

```csharp
HashSet<int> set1 = new HashSet<int> { 1, 2, 3 };
HashSet<int> set2 = new HashSet<int> { 3, 4, 5 };

set1.UnionWith(set2);        // set1: 1,2,3,4,5
set1.IntersectWith(set2);    // Common: 3,4,5
set1.ExceptWith(set2);       // Difference: empty
set1.SymmetricExceptWith(set2); // 1,2,4,5
```

**Real-time:** Finding common tags between two blog posts.

---

## Queue and Stack

### 23. What is Queue<T>?

**Answer:**  
`Queue<T>` is a FIFO (First-In-First-Out) collection. Elements are added at the rear using `Enqueue()` and removed from the front using `Dequeue()` . `Peek()` views the front element without removing it.

**Example:**

```csharp
Queue<string> queue = new Queue<string>();
queue.Enqueue("First");
queue.Enqueue("Second");
string first = queue.Dequeue(); // "First"
string peek = queue.Peek();     // "Second"
```

**Real-time:** Processing print jobs in order received.

---

### 24. What is Stack<T>?

**Answer:**  
`Stack<T>` is a LIFO (Last-In-First-Out) collection. Elements are added using `Push()` and removed using `Pop()` . `Peek()` views the top element without removing it. Useful for undo operations and backtracking.

**Example:**

```csharp
Stack<string> stack = new Stack<string>();
stack.Push("First");
stack.Push("Second");
string last = stack.Pop();  // "Second"
string top = stack.Peek();  // "First"
```

**Real-time:** Browser back button navigation history.

---

### 25. Difference between Queue and Stack?

**Answer:**  
Queue follows FIFO (First-In-First-Out) for sequential processing. Stack follows LIFO (Last-In-First-Out) for reverse-order processing. Queue uses Enqueue/Dequeue, Stack uses Push/Pop. Choose based on processing order requirements.

**Example:**

```csharp
// Queue - FIFO
Queue<int> queue = new Queue<int>();
queue.Enqueue(1); queue.Enqueue(2);
queue.Dequeue(); // Returns 1

// Stack - LIFO
Stack<int> stack = new Stack<int>();
stack.Push(1); stack.Push(2);
stack.Pop(); // Returns 2
```

---

### 26. Real-time use case of Queue?

**Answer:**  
Queues are used in message processing, task scheduling, BFS algorithms, and handling asynchronous operations. Perfect for order-sensitive processing where first request should be handled first.

**Real-time Example:**

```csharp
// Customer support ticket system
Queue<SupportTicket> ticketQueue = new Queue<SupportTicket>();

// Add new tickets
ticketQueue.Enqueue(new SupportTicket { Id = 1, Issue = "Login problem" });
ticketQueue.Enqueue(new SupportTicket { Id = 2, Issue = "Payment failed" });

// Process in order received
while (ticketQueue.Count > 0)
{
    var ticket = ticketQueue.Dequeue();
    ProcessTicket(ticket);
}
```

---

### 27. Real-time use case of Stack?

**Answer:**  
Stacks are used for undo/redo operations, expression evaluation, DFS algorithms, and recursion simulation. Perfect for scenarios requiring reverse-order access or backtracking.

**Real-time Example:**

```csharp
// Browser navigation history
Stack<string> history = new Stack<string>();

void Navigate(string url)
{
    history.Push(currentUrl);
    currentUrl = url;
}

void GoBack()
{
    if (history.Count > 0)
        currentUrl = history.Pop(); // Go to previous page
}
```

---

## LinkedList

### 28. What is LinkedList<T>?

**Answer:**  
`LinkedList<T>` is a doubly-linked list where each node contains data and references to next and previous nodes. It provides O(1) insertion/deletion at both ends and middle but O(n) indexed access.

**Example:**

```csharp
LinkedList<int> list = new LinkedList<int>();
list.AddFirst(1);
list.AddLast(3);
var node = list.Find(1);
list.AddAfter(node, 2); // Insert after node
// Result: 1, 2, 3
```

---

### 29. Difference between List and LinkedList?

**Answer:**  
`List<T>` uses dynamic array with O(1) indexed access but O(n) insertions. `LinkedList<T>` uses nodes with O(1) insertions/deletions but O(n) indexed access. List is better for random access, LinkedList for frequent insertions/deletions.

**Example:**

```csharp
// List - Fast indexed access
List<int> list = new List<int> { 1, 2, 3 };
int value = list[1]; // O(1)
list.Insert(1, 5);   // O(n) - shifts elements

// LinkedList - Fast insertion
LinkedList<int> linked = new LinkedList<int>();
linked.AddFirst(1); // O(1)
linked.AddLast(2);  // O(1)
// No indexer: linked[0] not supported
```

---

### 30. When to use LinkedList?

**Answer:**  
Use `LinkedList<T>` when you frequently insert/remove from middle or both ends, implement custom data structures like LRU cache, or need efficient node manipulation. Avoid for indexed access or searching.

**Real-time Example:**

```csharp
// LRU Cache implementation
LinkedList<CacheItem> cache = new LinkedList<CacheItem>();

void AccessItem(CacheItem item)
{
    cache.Remove(item);       // Remove from current position
    cache.AddFirst(item);     // Move to front (most recent)
    
    if (cache.Count > maxSize)
        cache.RemoveLast();   // Evict least recently used
}
```

---

## Concurrent Collections

### 31. What are Concurrent Collections?

**Answer:**  
Concurrent collections are thread-safe collections in `System.Collections.Concurrent` namespace designed for multi-threaded scenarios. They use lock-free mechanisms for better performance than manually synchronized collections. Include `ConcurrentDictionary` , `ConcurrentQueue` , `ConcurrentBag` , etc.

**Example:**

```csharp
ConcurrentDictionary<int, string> dict = new ConcurrentDictionary<int, string>();
Parallel.For(0, 1000, i => dict.TryAdd(i, $"Value{i}"));
```

---

### 32. What is ConcurrentDictionary?

**Answer:**  
`ConcurrentDictionary` is a thread-safe dictionary with atomic operations like `TryAdd` , `TryUpdate` , `GetOrAdd` , and `AddOrUpdate` . It uses fine-grained locking for better concurrency than lock-based Dictionary access.

**Example:**

```csharp
ConcurrentDictionary<int, int> dict = new ConcurrentDictionary<int, int>();

// Thread-safe operations
dict.TryAdd(1, 100);
dict.AddOrUpdate(1, 100, (key, oldValue) => oldValue + 1);
int value = dict.GetOrAdd(2, 200);
```

**Real-time:** Caching data in multi-threaded web applications.

---

### 33. What is ConcurrentQueue?

**Answer:**  
`ConcurrentQueue` is a thread-safe FIFO queue with lock-free enqueue/dequeue operations. Multiple threads can safely add and remove items concurrently. Uses `TryDequeue` instead of `Dequeue` to avoid exceptions.

**Example:**

```csharp
ConcurrentQueue<string> queue = new ConcurrentQueue<string>();

// Multiple threads can safely enqueue
Parallel.For(0, 100, i => queue.Enqueue($"Item{i}"));

// Safe dequeue
if (queue.TryDequeue(out string item))
    Console.WriteLine(item);
```

---

### 34. What is ConcurrentBag?

**Answer:**  
`ConcurrentBag` is an unordered, thread-safe collection optimized for scenarios where same thread adds and removes items. It provides better performance than `ConcurrentQueue` for producer-consumer patterns on same thread.

**Example:**

```csharp
ConcurrentBag<int> bag = new ConcurrentBag<int>();

Parallel.For(0, 1000, i => 
{
    bag.Add(i);
    if (bag.TryTake(out int item))
        Console.WriteLine(item);
});
```

**Real-time:** Thread-local work item storage in parallel processing.

---

### 35. When to use Concurrent Collections?

**Answer:**  
Use concurrent collections when multiple threads access shared data concurrently. They eliminate need for manual locking, reduce contention, and prevent race conditions. Essential for multi-threaded applications, parallel processing, and async operations.

**Real-time Example:**

```csharp
// Processing orders from multiple threads
ConcurrentQueue<Order> orderQueue = new ConcurrentQueue<Order>();

// Multiple threads adding orders
Task.Run(() => orderQueue.Enqueue(new Order()));

// Multiple threads processing orders
Parallel.For(0, 5, i => 
{
    while (orderQueue.TryDequeue(out Order order))
        ProcessOrder(order);
});
```

---

## Immutable Collections

### 36. What are Immutable Collections?

**Answer:**  
Immutable collections cannot be modified after creation. Any modification creates a new collection, preserving the original. They're thread-safe, prevent unintended changes, and are useful in functional programming. Available in `System.Collections.Immutable` package.

**Example:**

```csharp
ImmutableList<int> list = ImmutableList.Create(1, 2, 3);
ImmutableList<int> newList = list.Add(4); // Creates new list
// Original list unchanged
```

---

### 37. Benefits of Immutable Collections?

**Answer:**  
Immutable collections provide thread safety without locking, prevent accidental modifications, enable safe sharing across threads, and support undo/redo operations. They simplify reasoning about state and are ideal for concurrent and functional programming.

**Example:**

```csharp
// Safe sharing across threads
ImmutableList<string> sharedList = ImmutableList.Create("A", "B");

Task.Run(() => 
{
    var modified = sharedList.Add("C"); // Creates new list
    // Original sharedList unchanged
});
```

---

### 38. What is ImmutableList?

**Answer:**  
`ImmutableList<T>` is an immutable list that creates new instances on modifications. It uses efficient structural sharing to minimize memory overhead. Operations return new lists while preserving originals.

**Example:**

```csharp
ImmutableList<int> list1 = ImmutableList.Create(1, 2, 3);
ImmutableList<int> list2 = list1.Add(4);    // New list: 1,2,3,4
ImmutableList<int> list3 = list1.Remove(2); // New list: 1,3

// list1 remains: 1, 2, 3
```

**Real-time:** Maintaining version history in document editing.

---

### 39. What is ImmutableDictionary?

**Answer:**  
`ImmutableDictionary<TKey, TValue>` is an immutable key-value collection. Modifications return new dictionaries using structural sharing for efficiency. Ideal for configuration data and thread-safe state management.

**Example:**

```csharp
ImmutableDictionary<int, string> dict1 = 
    ImmutableDictionary.Create<int, string>();
    
ImmutableDictionary<int, string> dict2 = dict1.Add(1, "One");
ImmutableDictionary<int, string> dict3 = dict2.SetItem(1, "Updated");

// dict1 and dict2 unchanged
```

---

## Specialized Collections

### 40. What is SortedList?

**Answer:**  
`SortedList<TKey, TValue>` maintains key-value pairs sorted by key. It uses less memory than `SortedDictionary` and provides O(log n) lookups and O(n) insertions. Best for small collections with infrequent updates.

**Example:**

```csharp
SortedList<int, string> list = new SortedList<int, string>
{
    { 3, "Three" },
    { 1, "One" },
    { 2, "Two" }
};
// Stored as: (1, "One"), (2, "Two"), (3, "Three")
```

---

### 41. What is SortedDictionary?

**Answer:**  
`SortedDictionary<TKey, TValue>` maintains sorted key-value pairs using red-black tree. It provides O(log n) operations for both lookups and insertions. Better than `SortedList` for frequent insertions/deletions.

**Example:**

```csharp
SortedDictionary<string, int> dict = new SortedDictionary<string, int>
{
    { "Charlie", 30 },
    { "Alice", 25 },
    { "Bob", 28 }
};
// Sorted by key: Alice, Bob, Charlie
```

**Real-time:** Maintaining sorted leaderboard in gaming applications.

---

### 42. Difference between SortedList and SortedDictionary?

**Answer:**  
`SortedList` uses less memory and faster indexed access but slower insertions (O(n)). `SortedDictionary` has faster insertions/deletions (O(log n)) but more memory overhead. Choose SortedList for small static data, SortedDictionary for dynamic data.

**Example:**

```csharp
// SortedList - Memory efficient, slower insert
SortedList<int, string> list = new SortedList<int, string>();
list.Add(1, "One"); // O(n) insertion

// SortedDictionary - Faster insert, more memory
SortedDictionary<int, string> dict = new SortedDictionary<int, string>();
dict.Add(1, "One"); // O(log n) insertion
```

---

### 43. What is ObservableCollection?

**Answer:**  
`ObservableCollection<T>` notifies listeners when items are added, removed, or list is refreshed via `CollectionChanged` event. Primarily used in WPF/XAML for data binding to automatically update UI.

**Example:**

```csharp
ObservableCollection<string> items = new ObservableCollection<string>();
items.CollectionChanged += (s, e) => 
{
    Console.WriteLine($"Action: {e.Action}");
};

items.Add("Item1"); // Triggers CollectionChanged event
```

**Real-time:** Binding to ListBox/DataGrid in WPF applications.

---

### 44. What is BitArray?

**Answer:**  
`BitArray` manages a compact array of boolean values represented as bits. It's memory-efficient for large boolean arrays, using 1 bit per value instead of 1 byte. Provides bitwise operations like AND, OR, XOR.

**Example:**

```csharp
BitArray bits = new BitArray(8);
bits[0] = true;
bits[1] = true;
bits.And(new BitArray(new bool[] { true, false, true }));
```

**Real-time:** Implementing permission flags or bloom filters.

---

## Interfaces

### 45. What is IEnumerable<T>?

**Answer:**  
`IEnumerable<T>` is the base interface for all collections, providing forward-only iteration using `foreach` . It has single method `GetEnumerator()` that returns an enumerator. It's read-only and supports LINQ queries.

**Example:**

```csharp
IEnumerable<int> numbers = new List<int> { 1, 2, 3 };
foreach (int num in numbers)
    Console.WriteLine(num);

var even = numbers.Where(n => n % 2 == 0); // LINQ
```

---

### 46. What is ICollection<T>?

**Answer:**  
`ICollection<T>` extends `IEnumerable<T>` adding Count, Add, Remove, Clear, and Contains methods. It represents modifiable collections and provides basic collection operations beyond just iteration.

**Example:**

```csharp
ICollection<string> collection = new List<string>();
collection.Add("Item1");
collection.Remove("Item1");
int count = collection.Count;
bool exists = collection.Contains("Item1");
```

---

### 47. What is IList<T>?

**Answer:**  
`IList<T>` extends `ICollection<T>` adding indexed access, Insert, and RemoveAt methods. It represents ordered collections with positional access. Implemented by `List<T>` , arrays, and other indexed collections.

**Example:**

```csharp
IList<int> list = new List<int> { 1, 2, 3 };
int first = list[0];      // Indexed access
list.Insert(1, 5);        // Insert at position
list.RemoveAt(0);         // Remove at index
```

---

### 48. Difference between IEnumerable and ICollection?

**Answer:**  
`IEnumerable<T>` provides only iteration capability (read-only). `ICollection<T>` extends it with Count, Add, Remove operations (read-write). Use IEnumerable for query results, ICollection when you need modification or count.

**Example:**

```csharp
// IEnumerable - Read-only
IEnumerable<int> query = list.Where(x => x > 5);
// Cannot: query.Add(10), query.Count directly

// ICollection - Modifiable
ICollection<int> collection = new List<int>();
collection.Add(10);
int count = collection.Count;
```

---

### 49. What is IReadOnlyCollection<T>?

**Answer:**  
`IReadOnlyCollection<T>` provides read-only access with Count property but no modification methods. It prevents external code from modifying collection while allowing iteration and count queries. Better than IEnumerable when count is needed.

**Example:**

```csharp
IReadOnlyCollection<int> readOnly = new List<int> { 1, 2, 3 };
int count = readOnly.Count;
// Cannot: readOnly.Add(4), readOnly.Remove(1)
```

---

### 50. What is IReadOnlyList<T>?

**Answer:**  
`IReadOnlyList<T>` extends `IReadOnlyCollection<T>` with indexed read-only access. It allows retrieving elements by index but prevents modifications. Useful for exposing internal lists safely.

**Example:**

```csharp
IReadOnlyList<string> list = new List<string> { "A", "B", "C" };
string first = list[0];  // Read by index
int count = list.Count;
// Cannot: list[0] = "X", list.Add("D")
```

**Real-time:** Returning collection from API without allowing modifications.

---

## Performance and Best Practices

### 51. Time complexity of List operations?

**Answer:**  
List<T> operations: Add (amortized O(1)), Insert/RemoveAt (O(n)), Access by index (O(1)), Contains/Remove (O(n)), Sort (O(n log n)). Fast for end additions and indexed access, slower for middle insertions.

**Example:**

```csharp
List<int> list = new List<int>();
list.Add(1);           // O(1) amortized
int val = list[0];     // O(1)
list.Insert(0, 5);     // O(n) - shifts elements
list.Contains(5);      // O(n) - linear search
list.Sort();           // O(n log n)
```

---

### 52. Time complexity of Dictionary operations?

**Answer:**  
Dictionary operations: Add/Remove/ContainsKey/Lookup (O(1) average, O(n) worst case with collisions). GetHashCode quality affects performance. Best for fast lookups by key.

**Example:**

```csharp
Dictionary<int, string> dict = new Dictionary<int, string>();
dict.Add(1, "One");        // O(1) average
string val = dict[1];      // O(1) average
bool exists = dict.ContainsKey(1); // O(1) average
dict.Remove(1);            // O(1) average
```

---

### 53. Time complexity of HashSet operations?

**Answer:**  
HashSet operations: Add/Remove/Contains (O(1) average, O(n) worst case). Set operations like UnionWith, IntersectWith (O(n+m)). Ideal for membership testing and duplicate removal.

**Example:**

```csharp
HashSet<int> set = new HashSet<int>();
set.Add(1);              // O(1) average
bool exists = set.Contains(1); // O(1) average
set.Remove(1);           // O(1) average
set.UnionWith(otherSet); // O(n+m)
```

---

### 54. When to use List vs LinkedList?

**Answer:**  
Use List<T> for random access, frequent searching, and end additions. Use LinkedList<T> for frequent insertions/deletions in middle, no need for indexing, or implementing queue/deque structures.

**Example:**

```csharp
// Use List for random access
List<int> list = new List<int>();
int value = list[index]; // O(1)

// Use LinkedList for frequent insertions
LinkedList<int> linked = new LinkedList<int>();
var node = linked.Find(value);
linked.AddAfter(node, newValue); // O(1) if node is known
```

---

### 55. When to use Dictionary vs List?

**Answer:**  
Use Dictionary for fast lookups by key (O(1)), when data has unique identifiers. Use List for ordered data, indexed access, or when keys aren't natural. Dictionary trades memory for speed.

**Example:**

```csharp
// Use Dictionary for lookup by ID
Dictionary<int, Employee> employees = new Dictionary<int, Employee>();
Employee emp = employees[123]; // O(1) by ID

// Use List for ordered data
List<Employee> employees = new List<Employee>();
Employee emp = employees.Find(e => e.Id == 123); // O(n)
```

---

### 56. How to avoid boxing/unboxing in collections?

**Answer:**  
Use generic collections instead of non-generic ones. Generics provide type safety without boxing value types to objects. This improves performance and reduces garbage collection pressure.

**Example:**

```csharp
// Bad - Boxing occurs
ArrayList list = new ArrayList();
list.Add(100); // int boxed to object

// Good - No boxing
List<int> list = new List<int>();
list.Add(100); // No boxing
```

---

### 57. What is Collection initialization syntax?

**Answer:**  
Collection initializer syntax allows initializing collections inline using curly braces. It's syntactic sugar that calls Add method multiple times. Makes code more readable and concise.

**Example:**

```csharp
// Collection initializer
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Dictionary initializer
Dictionary<int, string> dict = new Dictionary<int, string>
{
    { 1, "One" },
    { 2, "Two" },
    [3] = "Three" // C# 6+ syntax
};
```

---

### 58. What is the benefit of AsReadOnly()?

**Answer:**  
`AsReadOnly()` returns read-only wrapper around existing collection, preventing modifications while sharing the same underlying data. It's memory-efficient and safer than copying entire collection.

**Example:**

```csharp
List<int> list = new List<int> { 1, 2, 3 };
IReadOnlyList<int> readOnly = list.AsReadOnly();

// Can read
int value = readOnly[0];

// Cannot modify
// readOnly.Add(4); // Compile error
// But original list can still be modified
list.Add(4); // Affects readOnly view
```

**Real-time:** Returning internal collection from class without allowing external modifications.

---

## Advanced Topics

### 59. What is yield return in collections?

**Answer:**  
`yield return` creates iterator methods that return `IEnumerable<T>` lazily, generating values on-demand without creating entire collection in memory. Execution pauses at each yield and resumes on next iteration.

**Example:**

```csharp
IEnumerable<int> GetNumbers()
{
    for (int i = 1; i <= 5; i++)
    {
        Console.WriteLine($"Generating {i}");
        yield return i;
    }
}

foreach (int num in GetNumbers())
{
    Console.WriteLine($"Consuming {num}");
    if (num == 3) break; // Only generates 1, 2, 3
}
```

**Real-time:** Reading large files line by line without loading entire file.

---

### 60. What is lazy evaluation with IEnumerable?

**Answer:**  
Lazy evaluation (deferred execution) means LINQ queries on IEnumerable execute only when iterated, not when declared. This saves memory and CPU by processing only needed elements.

**Example:**

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Query not executed yet
IEnumerable<int> query = numbers.Where(n => n > 2);

numbers.Add(6); // Modifies source

// Query executes now, includes 6
foreach (int num in query) // Output: 3, 4, 5, 6
    Console.WriteLine(num);
```

---

### 61. Difference between ToList() and AsEnumerable()?

**Answer:**  
`ToList()` executes query immediately and creates new list in memory (eager evaluation). `AsEnumerable()` returns IEnumerable without execution, enabling lazy evaluation. Use ToList() to cache results, AsEnumerable() for deferred execution.

**Example:**

```csharp
List<int> source = new List<int> { 1, 2, 3 };

// ToList - Executes immediately, creates copy
List<int> list = source.Where(n => n > 1).ToList();
source.Add(4); // Doesn't affect list

// AsEnumerable - Deferred execution
IEnumerable<int> query = source.Where(n => n > 1);
source.Add(5); // Affects query results
```

---

### 62. What is the difference between Add and AddRange?

**Answer:**  
`Add()` adds single element with one capacity check. `AddRange()` adds multiple elements with one capacity check for entire range, making it more efficient for bulk additions.

**Example:**

```csharp
List<int> list = new List<int>();

// Less efficient - multiple capacity checks
list.Add(1);
list.Add(2);
list.Add(3);

// More efficient - single capacity check
list.AddRange(new[] { 1, 2, 3 });
list.AddRange(otherList);
```

---

### 63. How to sort collections in C#?

**Answer:**  
Use `Sort()` method for in-place sorting, `OrderBy()` / `OrderByDescending()` LINQ for new sorted sequence. Can provide custom comparison using `IComparer<T>` , lambda, or `Comparison<T>` delegate.

**Example:**

```csharp
List<int> numbers = new List<int> { 3, 1, 4, 1, 5 };

// In-place sort
numbers.Sort(); // 1, 1, 3, 4, 5

// LINQ - returns new sequence
var sorted = numbers.OrderByDescending(n => n);

// Custom sorting
numbers.Sort((a, b) => b.CompareTo(a)); // Descending
```

---

### 64. What is IComparer and IComparable?

**Answer:**  
`IComparable<T>` defines default comparison within type using `CompareTo()` . `IComparer<T>` defines external comparison logic using `Compare()` . IComparable for natural ordering, IComparer for custom/multiple orderings.

**Example:**

```csharp
// IComparable - Natural ordering
public class Person : IComparable<Person>
{
    public string Name { get; set; }
    public int CompareTo(Person other) => Name.CompareTo(other.Name);
}

// IComparer - Custom ordering
public class PersonAgeComparer : IComparer<Person>
{
    public int Compare(Person x, Person y) => x.Age.CompareTo(y.Age);
}

list.Sort(); // Uses IComparable
list.Sort(new PersonAgeComparer()); // Uses IComparer
```

---

### 65. What is the purpose of EqualityComparer?

**Answer:**  
`EqualityComparer<T>` defines custom equality logic for collections like Dictionary, HashSet using `Equals()` and `GetHashCode()` . Allows using custom keys or case-insensitive comparisons.

**Example:**

```csharp
// Case-insensitive dictionary
var dict = new Dictionary<string, int>(
    StringComparer.OrdinalIgnoreCase);
dict["Key"] = 1;
dict["key"] = 2; // Updates same entry

// Custom comparer
public class PersonComparer : IEqualityComparer<Person>
{
    public bool Equals(Person x, Person y) => x.Id == y.Id;
    public int GetHashCode(Person obj) => obj.Id.GetHashCode();
}

var set = new HashSet<Person>(new PersonComparer());
```

---

## Practical Scenarios

### 66. How to remove duplicates from List?

**Answer:**  
Use `Distinct()` LINQ method or convert to HashSet which automatically removes duplicates. HashSet is more efficient for large lists.

**Example:**

```csharp
List<int> numbers = new List<int> { 1, 2, 2, 3, 3, 4 };

// Using LINQ
List<int> unique1 = numbers.Distinct().ToList();

// Using HashSet - More efficient
HashSet<int> uniqueSet = new HashSet<int>(numbers);
List<int> unique2 = uniqueSet.ToList();
```

**Real-time:** Removing duplicate emails from mailing list.

---

### 67. How to find common elements between two lists?

**Answer:**  
Use `Intersect()` LINQ method or HashSet's `IntersectWith()` . Intersect returns common elements without modifying original collections.

**Example:**

```csharp
List<int> list1 = new List<int> { 1, 2, 3, 4 };
List<int> list2 = new List<int> { 3, 4, 5, 6 };

// Using LINQ
var common = list1.Intersect(list2).ToList(); // 3, 4

// Using HashSet
HashSet<int> set1 = new HashSet<int>(list1);
set1.IntersectWith(list2); // set1 now: 3, 4
```

**Real-time:** Finding common skills between job posting and candidate profile.

---

### 68. How to merge two dictionaries?

**Answer:**  
Use LINQ `Union()` , loop through and add, or C# 9+ collection expressions. Handle duplicate keys appropriately based on requirements.

**Example:**

```csharp
var dict1 = new Dictionary<int, string> { { 1, "One" }, { 2, "Two" } };
var dict2 = new Dictionary<int, string> { { 2, "Deux" }, { 3, "Three" } };

// Method 1: Loop (overwrites duplicates)
foreach (var kvp in dict2)
    dict1[kvp.Key] = kvp.Value;

// Method 2: LINQ (skip duplicates)
var merged = dict1.Concat(dict2.Where(k => !dict1.ContainsKey(k.Key)))
    .ToDictionary(k => k.Key, v => v.Value);
```

---

### 69. How to convert List to Dictionary?

**Answer:**  
Use `ToDictionary()` LINQ method specifying key and value selectors. Ensure keys are unique to avoid `ArgumentException` .

**Example:**

```csharp
List<Employee> employees = new List<Employee>
{
    new Employee { Id = 1, Name = "John" },
    new Employee { Id = 2, Name = "Jane" }
};

// Convert to Dictionary
Dictionary<int, Employee> dict = employees.ToDictionary(e => e.Id);

// With custom value
Dictionary<int, string> nameDict = employees.ToDictionary(
    e => e.Id, 
    e => e.Name);
```

**Real-time:** Converting list of products to dictionary for fast lookup by ID.

---

### 70. How to check if collection is null or empty?

**Answer:**  
Check for null first, then use `Count == 0` or `Any()` for IEnumerable. `Any()` is more efficient for IEnumerable as it stops at first element.

**Example:**

```csharp
List<int> numbers = new List<int>();

// For List/Collection - use Count
if (numbers == null || numbers.Count == 0)
    Console.WriteLine("Empty or null");

// For IEnumerable - use Any() (more efficient)
IEnumerable<int> query = numbers.Where(n => n > 5);
if (query == null || !query.Any())
    Console.WriteLine("Empty or null");

// Extension method approach
public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    => source == null || !source.Any();
```

**Real-time:** Validating user input collections before processing.

---

## 🎯 Key Takeaways

### Performance Summary

* **Fast Lookup**: Dictionary, HashSet - O(1)
* **Fast Indexed Access**: List, Array - O(1)
* **Fast Insert/Delete**: LinkedList, Queue, Stack - O(1)
* **Sorted Operations**: SortedSet, SortedDictionary - O(log n)

### When to Use What

* **List<T>**: General purpose, indexed access, dynamic sizing
* **Dictionary<K, V>**: Key-value pairs, fast lookup
* **HashSet<T>**: Unique elements, membership testing
* **Queue<T>**: FIFO processing
* **Stack<T>**: LIFO processing, undo operations
* **LinkedList<T>**: Frequent middle insertions/deletions
* **Concurrent***: Multi-threaded scenarios
* **Immutable***: Thread-safety without locks, functional programming

### Best Practices

01. ✅ Always prefer generic collections over non-generic
02. ✅ Set initial capacity if size is known
03. ✅ Use appropriate collection for access pattern
04. ✅ Use concurrent collections in multi-threaded code
05. ✅ Consider read-only interfaces for API contracts
06. ✅ Use LINQ for queries, not manual loops
07. ✅ Dispose enumerators when breaking early
08. ✅ Avoid modifying collection during enumeration

---

## 📚 Additional Resources

* Microsoft Docs: [Collections and Data Structures](https://docs.microsoft.com/en-us/dotnet/standard/collections/)
* Performance: [Big-O Complexity Chart](https://www.bigocheatsheet.com/)
* Concurrent Collections: [Thread-Safe Collections](https://docs.microsoft.com/en-us/dotnet/standard/collections/thread-safe/)

---

**Last Updated**: January 2026  
**Target Audience**: . NET Developers preparing for technical interviews  
**Difficulty Level**: Beginner to Expert
