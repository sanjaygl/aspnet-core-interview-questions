# C# Collections - Complete Technical & Academic Guide

## Table of Contents
1. [Introduction to Collections](#introduction-to-collections)
2. [Academic Foundation](#academic-foundation)
3. [Classification of Collections](#classification-of-collections)
4. [Arrays](#arrays)
5. [Non-Generic Collections](#non-generic-collections)
6. [Generic Collections](#generic-collections)
7. [Concurrent Collections](#concurrent-collections)
8. [Collection Interfaces](#collection-interfaces)
9. [Performance Comparison](#performance-comparison)
10. [Real-World Use Cases](#real-world-use-cases)
11. [Interview Questions](#interview-questions)
12. [Quick Reference Summary](#quick-reference-summary)

---

## Introduction to Collections

### Academic Foundation of Collections

Collections in computer science represent abstract data types (ADTs) that store and organize multiple elements. They are fundamental building blocks in software engineering, providing standardized ways to manage data with well-defined operations and complexity guarantees.

#### **Theoretical Background**
Collections evolved from mathematical concepts of sets, sequences, and mappings. They embody several key computer science principles:

1. **Abstract Data Types (ADTs)**: Collections define operations independent of implementation
2. **Data Structure Theory**: Each collection type optimizes for specific access patterns
3. **Algorithmic Complexity**: Collections provide predictable performance characteristics
4. **Memory Management**: Efficient allocation and deallocation strategies
5. **Type Theory**: Generic collections ensure type safety at compile time

#### **Academic Classification Framework**
Collections can be academically classified along multiple dimensions:

**By Access Pattern:**
- **Sequential Access**: Elements accessed in order (Lists, Queues, Stacks)
- **Random Access**: Elements accessed by index/key (Arrays, Dictionaries)
- **Associative Access**: Elements accessed by key-value relationships

**By Mutability:**
- **Immutable**: Cannot be modified after creation
- **Mutable**: Can be modified after creation
- **Append-Only**: Can only add elements, not remove or modify

**By Ordering:**
- **Ordered**: Maintains insertion or sort order
- **Unordered**: No guaranteed order
- **Sorted**: Maintains elements in sorted order

### Why Collections Are Needed

Collections in C# provide dynamic data structures that can grow and shrink at runtime, unlike arrays which have fixed size. They offer:

#### **Dynamic Sizing**
- **Academic Principle**: Amortized analysis ensures efficient growth
- **Implementation**: Exponential growth strategy (typically doubling capacity)
- **Benefit**: O(1) amortized insertion time despite occasional O(n) resize operations

#### **Type Safety**
- **Academic Principle**: Type theory and generic programming
- **Implementation**: Compile-time type checking through generics
- **Benefit**: Eliminates runtime type errors and boxing/unboxing overhead

#### **Rich Functionality**
- **Academic Principle**: Algorithm design and complexity analysis
- **Implementation**: Optimized algorithms for common operations
- **Benefit**: Built-in methods for searching, sorting, filtering with known complexity

#### **Memory Management**
- **Academic Principle**: Memory allocation strategies and garbage collection
- **Implementation**: Efficient memory allocation and deallocation
- **Benefit**: Automatic memory management with minimal overhead

#### **Thread Safety**
- **Academic Principle**: Concurrent programming and synchronization
- **Implementation**: Lock-free algorithms and atomic operations
- **Benefit**: Safe concurrent access without explicit synchronization

```csharp
/// <summary>
/// Academic Example: Demonstrating Collection Benefits
/// 
/// This example illustrates the theoretical advantages of collections
/// over fixed-size arrays in terms of flexibility and functionality.
/// </summary>
public class CollectionBenefitsDemo
{
    public void DemonstrateArrayLimitations()
    {
        // Problem with arrays - fixed size constraint
        // Academic Explanation: Arrays in C# are statically sized, meaning their length is set at creation and cannot be changed. This is efficient for memory and performance, but inflexible for dynamic data scenarios.
        int[] numbers = new int[5]; // Compile-time size constraint

        // Cannot grow beyond initial capacity
        // numbers[5] = 6; // Runtime exception: IndexOutOfRangeException
        // Academic Explanation: Attempting to access or assign beyond the array's bounds results in a runtime exception, demonstrating the limitations of static sizing.

        // No built-in search functionality
        // Academic Explanation: Arrays do not provide built-in search or query methods. Searching requires manual iteration, which is O(n) time complexity.
        bool contains = false;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] == 3)
            {
                contains = true;
                break;
            }
        }
        // Academic Note: This manual search loop is less readable and maintainable than using collection methods.
    }

    public void DemonstrateCollectionAdvantages()
    {
        // Solution with collections - dynamic sizing
        // Academic Explanation: Collections like List<T> are dynamically sized, growing as needed. This is achieved through internal resizing strategies (amortized analysis), making them suitable for scenarios with unpredictable data volume.
        List<int> numbersList = new List<int>(); // Dynamic capacity

        // Can grow indefinitely (within memory constraints)
        for (int i = 0; i < 1000000; i++)
        {
            numbersList.Add(i); // O(1) amortized time complexity
        }
        // Academic Note: List<T> uses an internal array that doubles in size when capacity is exceeded, ensuring efficient insertions on average.

        // Rich built-in functionality with optimal algorithms
        bool contains = numbersList.Contains(500000); // O(n) linear search
        int index = numbersList.BinarySearch(500000); // O(log n) if sorted
        var evenNumbers = numbersList.Where(x => x % 2 == 0).ToList(); // LINQ support
        // Academic Explanation: Collections provide built-in methods for searching, sorting, and filtering, improving code clarity and leveraging optimized algorithms.

        // Type safety with generics
        // numbersList.Add("string"); // Compile-time error
        // Academic Note: Generics enforce type safety at compile time, preventing runtime errors and eliminating boxing/unboxing overhead.
    }
}
```

### Academic Analysis: When to Use Collections vs Arrays

#### **Use Arrays When:**

**Mathematical Operations and Performance-Critical Code:**
```csharp
/// <summary>
/// Academic Example: Array Usage for Mathematical Operations
/// 
/// Arrays provide optimal performance for mathematical computations
/// due to contiguous memory layout and minimal overhead.
/// </summary>
public class MathematicalOperations
{
    // Matrix multiplication - arrays provide optimal cache performance
    public double[,] MultiplyMatrices(double[,] a, double[,] b)
    {
        int rows = a.GetLength(0);
        int cols = b.GetLength(1);
        int common = a.GetLength(1);
        
        double[,] result = new double[rows, cols];
        
        // Contiguous memory access pattern optimizes cache usage
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                for (int k = 0; k < common; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        
        return result;
    }
    
    // Signal processing - fixed-size buffers
    public void ProcessSignal(double[] samples)
    {
        // FFT algorithms require power-of-2 sized arrays
        int n = samples.Length;
        if ((n & (n - 1)) != 0) // Check if power of 2
        {
            throw new ArgumentException("Array size must be power of 2");
        }
        
        // In-place FFT processing
        FastFourierTransform(samples);
    }
    
    private void FastFourierTransform(double[] samples)
    {
        // Implementation would go here
        // Arrays provide predictable memory layout for optimization
    }
}
```

**Interop with Unmanaged Code:**
```csharp
/// <summary>
/// Academic Example: Array Usage for Interoperability
/// 
/// Arrays provide direct memory mapping for P/Invoke scenarios
/// </summary>
public class InteropExample
{
    [DllImport("kernel32.dll")]
    private static extern bool ReadFile(IntPtr handle, byte[] buffer, 
        uint bytesToRead, out uint bytesRead, IntPtr overlapped);
    
    public byte[] ReadFileData(IntPtr fileHandle, int bufferSize)
    {
        // Arrays provide direct memory mapping for P/Invoke
        byte[] buffer = new byte[bufferSize];
        uint bytesRead;
        
        if (ReadFile(fileHandle, buffer, (uint)bufferSize, out bytesRead, IntPtr.Zero))
        {
            // Resize array to actual data size
            Array.Resize(ref buffer, (int)bytesRead);
        }
        
        return buffer;
    }
}
```

#### **Use Collections When:**

**Dynamic Requirements and Rich Functionality:**
```csharp
/// <summary>
/// Academic Example: Collection Usage for Dynamic Scenarios
/// 
/// Collections provide flexibility and rich functionality
/// for scenarios with changing requirements.
/// </summary>
public class DynamicDataProcessing
{
    // Event processing with unknown number of events
    public void ProcessEvents(IEnumerable<Event> eventStream)
    {
        var eventsByType = new Dictionary<EventType, List<Event>>();
        var recentEvents = new Queue<Event>();
        var uniqueUsers = new HashSet<string>();
        
        foreach (var evt in eventStream)
        {
            // Dynamic grouping by type
            if (!eventsByType.ContainsKey(evt.Type))
            {
                eventsByType[evt.Type] = new List<Event>();
            }
            eventsByType[evt.Type].Add(evt);
            
            // Sliding window of recent events
            recentEvents.Enqueue(evt);
            if (recentEvents.Count > 1000)
            {
                recentEvents.Dequeue();
            }
            
            // Unique user tracking
            uniqueUsers.Add(evt.UserId);
        }
        
        // Rich querying capabilities with LINQ
        var topEventTypes = eventsByType
            .OrderByDescending(kvp => kvp.Value.Count)
            .Take(10)
            .Select(kvp => new { Type = kvp.Key, Count = kvp.Value.Count })
            .ToList();
    }
    
    // Configuration management with type safety
    public class ConfigurationManager
    {
        private readonly Dictionary<string, object> _settings = new();
        
        public T GetSetting<T>(string key, T defaultValue = default(T))
        {
            if (_settings.TryGetValue(key, out var value) && value is T typedValue)
            {
                return typedValue;
            }
            return defaultValue;
        }
        
        public void SetSetting<T>(string key, T value)
        {
            _settings[key] = value;
        }
    }
}
```

### Academic Performance Characteristics

#### **Theoretical Complexity Analysis**

| Operation | Array | List<T> | Dictionary<T> | HashSet<T> |
|-----------|-------|---------|---------------|------------|
| **Access by Index** | O(1) | O(1) | N/A | N/A |
| **Access by Key** | N/A | N/A | O(1) avg | N/A |
| **Search** | O(n) | O(n) | O(1) avg | O(1) avg |
| **Insert at End** | N/A | O(1) amortized | O(1) avg | O(1) avg |
| **Insert at Beginning** | N/A | O(n) | N/A | N/A |
| **Delete** | N/A | O(n) | O(1) avg | O(1) avg |
| **Memory Overhead** | Minimal | Low | Medium | Medium |

#### **Amortized Analysis Example**
```csharp
/// <summary>
/// Academic Example: Amortized Analysis of Dynamic Array Growth
/// 
/// This demonstrates why List<T> provides O(1) amortized insertion
/// despite occasional O(n) resize operations.
/// </summary>
public class AmortizedAnalysisDemo
{
    public void DemonstrateAmortizedComplexity()
    {
        var list = new List<int>();
        var operationCosts = new List<int>();
        
        // Track the cost of each insertion
        for (int i = 0; i < 1000; i++)
        {
            int capacityBefore = list.Capacity;
            list.Add(i);
            int capacityAfter = list.Capacity;
            
            // Cost is 1 for normal insertion, n for resize operation
            int cost = (capacityAfter > capacityBefore) ? capacityAfter : 1;
            operationCosts.Add(cost);
        }
        
        // Calculate amortized cost
        double totalCost = operationCosts.Sum();
        double amortizedCost = totalCost / operationCosts.Count;
        
        Console.WriteLine($"Total operations: {operationCosts.Count}");
        Console.WriteLine($"Total cost: {totalCost}");
        Console.WriteLine($"Amortized cost per operation: {amortizedCost:F2}");
        // Result: Amortized cost approaches 2 (constant time)
    }
}

---

## Academic Foundation

### Theoretical Computer Science Principles

#### **Abstract Data Types (ADTs)**
Collections implement Abstract Data Types, which define:
- **Interface**: Set of operations that can be performed
- **Semantics**: What each operation does (not how)
- **Complexity**: Performance guarantees for each operation

#### **Data Structure Theory**
Each collection type is optimized for specific access patterns:
- **Arrays**: Contiguous memory, O(1) random access
- **Linked Lists**: Node-based, O(1) insertion/deletion at known positions
- **Hash Tables**: Hash-based, O(1) average case key-value operations
- **Trees**: Hierarchical, O(log n) operations for balanced trees

#### **Algorithmic Complexity Theory**
Collections provide predictable performance through:
- **Worst-case Analysis**: Maximum time for any input
- **Average-case Analysis**: Expected time for random input
- **Amortized Analysis**: Average time over sequence of operations

#### **Memory Management Theory**
Collections implement various memory strategies:
- **Contiguous Allocation**: Arrays, List<T> (better cache locality)
- **Linked Allocation**: LinkedList<T> (flexible but poor cache performance)
- **Hash-based Allocation**: Dictionary<T>, HashSet<T> (trade memory for speed)

#### **Concurrency Theory**
Concurrent collections implement:
- **Lock-free Algorithms**: Using atomic operations and memory barriers
- **Compare-and-Swap (CAS)**: Atomic read-modify-write operations
- **Memory Models**: Ensuring visibility and ordering of operations across threads

---

## Classification of Collections

### Academic Classification Framework

Collections can be classified along multiple theoretical dimensions:

#### **By Mathematical Structure**
```
Mathematical Foundations
├── Sequences (Ordered Collections)
│   ├── Arrays - Fixed-size sequences
│   ├── Lists - Dynamic sequences
│   └── Queues/Stacks - Restricted sequences
├── Sets (Unordered, Unique Elements)
│   ├── HashSet - Hash-based sets
│   └── SortedSet - Ordered sets
├── Mappings (Key-Value Associations)
│   ├── Dictionary - Hash-based mappings
│   └── SortedDictionary - Ordered mappings
└── Multisets (Allow Duplicates)
    └── List - Sequence allowing duplicates
```

#### **By Access Pattern Complexity**
```
Access Pattern Analysis
├── O(1) Random Access
│   ├── Arrays
│   └── List<T> (by index)
├── O(1) Key-based Access (Average Case)
│   ├── Dictionary<TKey, TValue>
│   └── HashSet<T>
├── O(log n) Ordered Access
│   ├── SortedDictionary<TKey, TValue>
│   └── SortedSet<T>
└── O(n) Sequential Access
    ├── LinkedList<T>
    └── Linear search operations
```

#### **By Memory Layout**
```
Memory Organization
├── Contiguous Memory (Cache-Friendly)
│   ├── Arrays
│   ├── List<T>
│   └── Stack<T>
├── Linked Memory (Flexible)
│   └── LinkedList<T>
└── Hash-based Memory (Distributed)
    ├── Dictionary<TKey, TValue>
    └── HashSet<T>
```

### Implementation Hierarchy in C#

```
C# Collections Hierarchy
├── Arrays (System.Array)
│   ├── Single-dimensional: T[]
│   ├── Multi-dimensional: T[,]
│   └── Jagged Arrays: T[][]
├── Non-Generic Collections (System.Collections)
│   ├── ArrayList (Dynamic array of objects)
│   ├── Hashtable (Hash table of objects)
│   ├── Queue (FIFO queue of objects)
│   ├── Stack (LIFO stack of objects)
│   └── SortedList (Sorted key-value pairs)
├── Generic Collections (System.Collections.Generic)
│   ├── List<T> (Dynamic array with type safety)
│   ├── Dictionary<TKey, TValue> (Hash table with type safety)
│   ├── HashSet<T> (Hash-based set)
│   ├── Queue<T> (FIFO queue with type safety)
│   ├── Stack<T> (LIFO stack with type safety)
│   ├── LinkedList<T> (Doubly-linked list)
│   ├── SortedDictionary<TKey, TValue> (Red-black tree)
│   └── SortedSet<T> (Red-black tree set)
└── Concurrent Collections (System.Collections.Concurrent)
    ├── ConcurrentDictionary<TKey, TValue> (Lock-free hash table)
    ├── ConcurrentQueue<T> (Lock-free FIFO queue)
    ├── ConcurrentStack<T> (Lock-free LIFO stack)
    ├── ConcurrentBag<T> (Lock-free unordered collection)
    └── BlockingCollection<T> (Producer-consumer collection)
```

### Academic Analysis of Design Decisions

#### **Generic vs Non-Generic Collections**
```csharp
/// <summary>
/// Academic Analysis: Type Safety and Performance Impact
/// 
/// This comparison demonstrates the theoretical and practical
/// benefits of generic collections over non-generic ones.
/// </summary>
public class GenericVsNonGenericAnalysis
{
    public void DemonstrateBoxingOverhead()
    {
        // Non-generic collection - boxing overhead
        ArrayList arrayList = new ArrayList();
        
        // Boxing: value type -> reference type (heap allocation)
        arrayList.Add(42);        // int -> object (boxing)
        arrayList.Add(3.14);      // double -> object (boxing)
        
        // Unboxing: reference type -> value type (type checking + cast)
        int value1 = (int)arrayList[0];     // object -> int (unboxing)
        double value2 = (double)arrayList[1]; // object -> double (unboxing)
        
        // Generic collection - no boxing
        List<int> intList = new List<int>();
        List<double> doubleList = new List<double>();
        
        intList.Add(42);        // Direct storage, no boxing
        doubleList.Add(3.14);   // Direct storage, no boxing
        
        int directValue1 = intList[0];     // Direct access, no unboxing
        double directValue2 = doubleList[0]; // Direct access, no unboxing
    }
    
    public void DemonstrateTypesSafety()
    {
        // Non-generic - runtime type errors possible
        ArrayList mixedList = new ArrayList();
        mixedList.Add(42);
        mixedList.Add("Hello");
        
        // Runtime error - no compile-time checking
        try
        {
            int sum = 0;
            foreach (object item in mixedList)
            {
                sum += (int)item; // InvalidCastException for "Hello"
            }
        }
        catch (InvalidCastException ex)
        {
            Console.WriteLine($"Runtime error: {ex.Message}");
        }
        
        // Generic - compile-time type safety
        List<int> typedList = new List<int>();
        typedList.Add(42);
        // typedList.Add("Hello"); // Compile-time error
        
        int safeSum = 0;
        foreach (int item in typedList)
        {
            safeSum += item; // Type-safe, no casting needed
        }
    }
}
```

#### **Concurrent vs Non-Concurrent Collections**
```csharp
/// <summary>
/// Academic Analysis: Concurrency and Thread Safety
/// 
/// This demonstrates the theoretical foundations of concurrent
/// collections and their performance trade-offs.
/// </summary>
public class ConcurrencyAnalysis
{
    public void DemonstrateThreadSafetyIssues()
    {
        // Non-thread-safe collection
        var unsafeDict = new Dictionary<int, string>();
        
        // Race condition - can cause corruption or exceptions
        Parallel.For(0, 1000, i =>
        {
            // Multiple threads modifying simultaneously
            // Can cause: KeyNotFoundException, ArgumentException, or corruption
            try
            {
                unsafeDict[i] = $"Value {i}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Race condition error: {ex.GetType().Name}");
            }
        });
        
        // Thread-safe concurrent collection
        var safeDict = new ConcurrentDictionary<int, string>();
        
        // Lock-free operations - safe for concurrent access
        Parallel.For(0, 1000, i =>
        {
            safeDict[i] = $"Value {i}"; // Thread-safe operation
        });
        
        Console.WriteLine($"Unsafe dictionary count: {unsafeDict.Count}");
        Console.WriteLine($"Safe dictionary count: {safeDict.Count}");
    }
    
    public void DemonstratePerformanceTradeoffs()
    {
        const int iterations = 1000000;
        
        // Single-threaded performance
        var regularDict = new Dictionary<int, int>();
        var stopwatch = Stopwatch.StartNew();
        
        for (int i = 0; i < iterations; i++)
        {
            regularDict[i] = i * 2;
        }
        
        stopwatch.Stop();
        Console.WriteLine($"Regular Dictionary: {stopwatch.ElapsedMilliseconds}ms");
        
        // Concurrent collection performance
        var concurrentDict = new ConcurrentDictionary<int, int>();
        stopwatch.Restart();
        
        for (int i = 0; i < iterations; i++)
        {
            concurrentDict[i] = i * 2;
        }
        
        stopwatch.Stop();
        Console.WriteLine($"Concurrent Dictionary: {stopwatch.ElapsedMilliseconds}ms");
        
        // Concurrent collections have overhead but provide thread safety
    }
}
```

---

## Arrays

### Academic Foundation of Arrays

Arrays are the most fundamental data structure in computer science, representing a contiguous block of memory containing elements of the same type. They provide the foundation for understanding more complex data structures and are essential for performance-critical applications.

#### **Theoretical Properties**
- **Homogeneous**: All elements must be of the same type
- **Fixed Size**: Size determined at creation time
- **Contiguous Memory**: Elements stored in adjacent memory locations
- **Random Access**: O(1) access time to any element by index
- **Cache Friendly**: Sequential access patterns optimize CPU cache usage

#### **Mathematical Model**
An array A of size n can be modeled as a function:
```
A: {0, 1, 2, ..., n-1} → T
```
Where T is the element type, and the function maps indices to values.

### Single-Dimensional Arrays

#### **Academic Analysis**
Single-dimensional arrays represent the simplest form of indexed collections, providing direct mapping from integer indices to memory addresses.

**Memory Layout:**
```
Address Calculation: address(A[i]) = base_address + (i × element_size)
```

**Time Complexity:**
- Access: O(1)
- Search: O(n) for unsorted, O(log n) for sorted
- Insertion: Not supported (fixed size)
- Deletion: Not supported (fixed size)

```csharp
/// <summary>
/// Academic Example: Single-Dimensional Array Operations
/// 
/// This demonstrates the theoretical properties and practical
/// usage of single-dimensional arrays in C#.
/// </summary>
public class SingleDimensionalArrayAnalysis
{
    public void DemonstrateArrayProperties()
    {
        // Declaration and initialization methods
        int[] numbers = new int[5];                    // Zero-initialized
        string[] names = {"Alice", "Bob", "Charlie"};  // Literal initialization
        int[] values = new int[] {1, 2, 3, 4, 5};     // Explicit initialization
        
        // Array properties - all O(1) operations
        Console.WriteLine($"Length: {numbers.Length}");        // Number of elements
        Console.WriteLine($"Rank: {numbers.Rank}");           // Number of dimensions (1)
        Console.WriteLine($"Lower bound: {numbers.GetLowerBound(0)}"); // Always 0 in C#
        Console.WriteLine($"Upper bound: {numbers.GetUpperBound(0)}"); // Length - 1
        
        // Random access - O(1) time complexity
        numbers[0] = 10;  // Direct memory access
        int first = numbers[0]; // Direct memory read
        
        // Bounds checking in C# (unlike C/C++)
        try
        {
            numbers[10] = 100; // Throws IndexOutOfRangeException
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Bounds checking prevented: {ex.Message}");
        }
    }
    
    public void DemonstrateIterationPatterns()
    {
        int[] array = {1, 2, 3, 4, 5};
        
        // Index-based iteration - optimal for random access
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine($"Index {i}: {array[i]}");
        }
        
        // Enhanced for loop - optimal for sequential access
        foreach (int value in array)
        {
            Console.WriteLine($"Value: {value}");
        }
        
        // LINQ operations - functional programming approach
        var evenNumbers = array.Where(x => x % 2 == 0).ToArray();
        var doubled = array.Select(x => x * 2).ToArray();
        int sum = array.Sum(); // O(n) reduction operation
    }
    
    public void DemonstrateArrayAlgorithms()
    {
        int[] unsorted = {64, 34, 25, 12, 22, 11, 90};
        int[] sorted = (int[])unsorted.Clone();
        
        // Built-in sorting - Introsort (hybrid algorithm)
        // Average case: O(n log n), Worst case: O(n log n)
        Array.Sort(sorted);
        
        // Binary search - requires sorted array
        // Time complexity: O(log n)
        int index = Array.BinarySearch(sorted, 25);
        Console.WriteLine($"Element 25 found at index: {index}");
        
        // Linear search - works on unsorted arrays
        // Time complexity: O(n)
        int linearIndex = Array.IndexOf(unsorted, 25);
        Console.WriteLine($"Linear search found 25 at index: {linearIndex}");
        
        // Array reversal - O(n) time, O(1) space
        Array.Reverse(unsorted);
        
        // Array copying - O(n) time
        int[] copy = new int[unsorted.Length];
        Array.Copy(unsorted, copy, unsorted.Length);
    }
}
```

### Multi-Dimensional Arrays

#### **Academic Analysis**
Multi-dimensional arrays extend the array concept to multiple dimensions, representing matrices, tensors, and other mathematical structures.

**Memory Layout (Row-Major Order):**
```
For 2D array A[m,n]:
address(A[i,j]) = base_address + (i × n + j) × element_size
```

**Mathematical Applications:**
- Matrix operations (linear algebra)
- Image processing (2D pixel arrays)
- Scientific computing (multi-dimensional data)
- Game development (2D/3D grids)

```csharp
/// <summary>
/// Academic Example: Multi-Dimensional Array Applications
/// 
/// This demonstrates the mathematical and practical applications
/// of multi-dimensional arrays in computational scenarios.
/// </summary>
public class MultiDimensionalArrayAnalysis
{
    public void DemonstrateRectangularArrays()
    {
        // Rectangular (multi-dimensional) array - contiguous memory
        int[,] matrix = new int[3, 4]; // 3 rows, 4 columns
        int[,] initialized = {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};
        
        // Access elements - O(1) time complexity
        matrix[0, 0] = 10;
        int value = matrix[1, 2];
        
        // Dimension information
        int rows = matrix.GetLength(0);    // First dimension size
        int cols = matrix.GetLength(1);    // Second dimension size
        int totalElements = matrix.Length; // Total number of elements
        
        Console.WriteLine($"Matrix dimensions: {rows} × {cols} = {totalElements} elements");
    }
    
    public void DemonstrateMatrixOperations()
    {
        // Matrix multiplication example - O(n³) algorithm
        double[,] matrixA = {{1, 2}, {3, 4}};
        double[,] matrixB = {{5, 6}, {7, 8}};
        
        double[,] result = MultiplyMatrices(matrixA, matrixB);
        
        // Display result matrix
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                Console.Write($"{result[i, j]:F2} ");
            }
            Console.WriteLine();
        }
    }
    
    private double[,] MultiplyMatrices(double[,] a, double[,] b)
    {
        int aRows = a.GetLength(0);
        int aCols = a.GetLength(1);
        int bRows = b.GetLength(0);
        int bCols = b.GetLength(1);
        
        if (aCols != bRows)
        {
            throw new ArgumentException("Matrix dimensions incompatible for multiplication");
        }
        
        double[,] result = new double[aRows, bCols];
        
        // Triple nested loop - O(n³) time complexity
        for (int i = 0; i < aRows; i++)
        {
            for (int j = 0; j < bCols; j++)
            {
                for (int k = 0; k < aCols; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        
        return result;
    }
    
    public void DemonstrateImageProcessing()
    {
        // 2D array representing grayscale image
        byte[,] image = new byte[100, 100]; // 100x100 pixel image
        
        // Initialize with gradient pattern
        for (int i = 0; i < image.GetLength(0); i++)
        {
            for (int j = 0; j < image.GetLength(1); j++)
            {
                image[i, j] = (byte)((i + j) % 256);
            }
        }
        
        // Apply blur filter - convolution operation
        byte[,] blurred = ApplyBlurFilter(image);
    }
    
    private byte[,] ApplyBlurFilter(byte[,] image)
    {
        int rows = image.GetLength(0);
        int cols = image.GetLength(1);
        byte[,] result = new byte[rows, cols];
        
        // 3x3 blur kernel
        double[,] kernel = {
            {1.0/9, 1.0/9, 1.0/9},
            {1.0/9, 1.0/9, 1.0/9},
            {1.0/9, 1.0/9, 1.0/9}
        };
        
        // Convolution operation - O(n²) for each pixel
        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 1; j < cols - 1; j++)
            {
                double sum = 0;
                for (int ki = -1; ki <= 1; ki++)
                {
                    for (int kj = -1; kj <= 1; kj++)
                    {
                        sum += image[i + ki, j + kj] * kernel[ki + 1, kj + 1];
                    }
                }
                result[i, j] = (byte)Math.Min(255, Math.Max(0, sum));
            }
        }
        
        return result;
    }
}
```

### Jagged Arrays

#### **Academic Analysis**
Jagged arrays (arrays of arrays) provide flexibility by allowing each row to have different lengths. They represent a compromise between the efficiency of rectangular arrays and the flexibility of dynamic structures.

**Memory Layout:**
- Array of references to sub-arrays
- Each sub-array is independently allocated
- Less cache-friendly than rectangular arrays
- More memory overhead due to reference indirection

**Use Cases:**
- Triangular matrices (upper/lower triangular)
- Sparse data structures
- Variable-length records
- Hierarchical data representation

```csharp
/// <summary>
/// Academic Example: Jagged Array Applications
/// 
/// This demonstrates scenarios where jagged arrays provide
/// optimal memory usage and flexibility.
/// </summary>
public class JaggedArrayAnalysis
{
    public void DemonstrateJaggedArrays()
    {
        // Array of arrays - each sub-array can have different length
        int[][] jaggedArray = new int[3][];
        jaggedArray[0] = new int[4];     // First row: 4 elements
        jaggedArray[1] = new int[2];     // Second row: 2 elements
        jaggedArray[2] = new int[3];     // Third row: 3 elements
        
        // Initialize with values
        int[][] scores = 
        {
            new int[] {92, 93, 78, 87},    // Student 1: 4 subjects
            new int[] {68, 84},            // Student 2: 2 subjects
            new int[] {90, 91, 88}         // Student 3: 3 subjects
        };
        
        // Access elements - requires two index operations
        int firstScore = scores[0][0]; // Student 1, Subject 1
        
        // Iterate through jagged array
        for (int i = 0; i < scores.Length; i++)
        {
            Console.Write($"Student {i + 1} scores: ");
            for (int j = 0; j < scores[i].Length; j++)
            {
                Console.Write($"{scores[i][j]} ");
            }
            Console.WriteLine();
        }
    }
    
    public void DemonstrateTriangularMatrix()
    {
        // Upper triangular matrix using jagged array
        // Saves memory by not storing zero elements
        int n = 5;
        double[][] upperTriangular = new double[n][];
        
        for (int i = 0; i < n; i++)
        {
            upperTriangular[i] = new double[n - i]; // Decreasing row length
        }
        
        // Fill upper triangular matrix
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < upperTriangular[i].Length; j++)
            {
                upperTriangular[i][j] = (i + 1) * 10 + (j + i + 1);
            }
        }
        
        // Display matrix (with zeros for lower triangle)
        Console.WriteLine("Upper Triangular Matrix:");
        for (int i = 0; i < n; i++)
        {
            // Print leading zeros
            for (int k = 0; k < i; k++)
            {
                Console.Write("0.00 ");
            }
            
            // Print actual values
            for (int j = 0; j < upperTriangular[i].Length; j++)
            {
                Console.Write($"{upperTriangular[i][j]:F2} ");
            }
            Console.WriteLine();
        }
    }
    
    public void DemonstrateSparseDataStructure()
    {
        // Sparse matrix representation using jagged arrays
        // Only store non-zero elements to save memory
        
        // Example: 1000x1000 matrix with only 100 non-zero elements
        // Rectangular array: 1,000,000 elements
        // Sparse representation: ~200 elements (row indices + values)
        
        var sparseMatrix = new SparseMatrix(1000, 1000);
        
        // Add some non-zero elements
        sparseMatrix.SetValue(0, 5, 3.14);
        sparseMatrix.SetValue(100, 200, 2.71);
        sparseMatrix.SetValue(500, 750, 1.41);
        
        Console.WriteLine($"Matrix size: {sparseMatrix.Rows} × {sparseMatrix.Cols}");
        Console.WriteLine($"Non-zero elements: {sparseMatrix.NonZeroCount}");
        Console.WriteLine($"Memory efficiency: {sparseMatrix.MemoryEfficiency:P2}");
    }
}

/// <summary>
/// Academic Example: Sparse Matrix Implementation
/// 
/// This demonstrates how jagged arrays can be used to implement
/// memory-efficient sparse data structures.
/// </summary>
public class SparseMatrix
{
    private readonly Dictionary<int, Dictionary<int, double>> _data;
    
    public int Rows { get; }
    public int Cols { get; }
    public int NonZeroCount => _data.Values.Sum(row => row.Count);
    public double MemoryEfficiency => (double)NonZeroCount / (Rows * Cols);
    
    public SparseMatrix(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        _data = new Dictionary<int, Dictionary<int, double>>();
    }
    
    public void SetValue(int row, int col, double value)
    {
        if (row < 0 || row >= Rows || col < 0 || col >= Cols)
        {
            throw new ArgumentOutOfRangeException("Index out of bounds");
        }
        
        if (Math.Abs(value) < double.Epsilon)
        {
            // Remove zero values to maintain sparsity
            if (_data.ContainsKey(row))
            {
                _data[row].Remove(col);
                if (_data[row].Count == 0)
                {
                    _data.Remove(row);
                }
            }
        }
        else
        {
            if (!_data.ContainsKey(row))
            {
                _data[row] = new Dictionary<int, double>();
            }
            _data[row][col] = value;
        }
    }
    
    public double GetValue(int row, int col)
    {
        if (row < 0 || row >= Rows || col < 0 || col >= Cols)
        {
            throw new ArgumentOutOfRangeException("Index out of bounds");
        }
        
        return _data.ContainsKey(row) && _data[row].ContainsKey(col) 
            ? _data[row][col] 
            : 0.0;
    }
}
```

### Academic Performance Analysis of Arrays

#### **Cache Performance**
```csharp
/// <summary>
/// Academic Example: Cache Performance Analysis
/// 
/// This demonstrates how array access patterns affect
/// CPU cache performance and overall execution speed.
/// </summary>
public class ArrayCachePerformanceAnalysis
{
    public void DemonstrateCacheEfficiency()
    {
        const int size = 1000;
        int[,] matrix = new int[size, size];
        
        // Initialize matrix
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = i * size + j;
            }
        }
        
        // Row-major access (cache-friendly)
        var stopwatch = Stopwatch.StartNew();
        long sum1 = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                sum1 += matrix[i, j]; // Sequential memory access
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Row-major access: {stopwatch.ElapsedMilliseconds}ms");
        
        // Column-major access (cache-unfriendly)
        stopwatch.Restart();
        long sum2 = 0;
        for (int j = 0; j < size; j++)
        {
            for (int i = 0; i < size; i++)
            {
                sum2 += matrix[i, j]; // Strided memory access
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Column-major access: {stopwatch.ElapsedMilliseconds}ms");
        
        Console.WriteLine($"Performance ratio: {(double)stopwatch.ElapsedMilliseconds / stopwatch.ElapsedMilliseconds:F2}x");
    }
}
Console.WriteLine($"Rank: {numbers.Rank}"); // Always 1 for single-dimensional

// Iteration
foreach (int number in numbers)
{
    Console.WriteLine(number);
}

// Array methods
Array.Sort(numbers);
int index = Array.IndexOf(numbers, 3);
Array.Reverse(numbers);
```

### Multi-Dimensional Arrays

```csharp
// Rectangular array
int[,] matrix = new int[3, 4]; // 3 rows, 4 columns
int[,] initialized = {{1, 2}, {3, 4}, {5, 6}};

// Access elements
matrix[0, 0] = 10;
int value = matrix[1, 2];

// Get dimensions
int rows = matrix.GetLength(0);    // 3
int cols = matrix.GetLength(1);    // 4
```

### Jagged Arrays

```csharp
// Array of arrays
int[][] jaggedArray = new int[3][];
jaggedArray[0] = new int[4];
jaggedArray[1] = new int[2];
jaggedArray[2] = new int[3];

// Initialize with values
int[][] scores = 
{
    new int[] {92, 93, 78, 87},
    new int[] {68, 84},
    new int[] {90, 91, 88}
};

// Access elements
int firstScore = scores[0][0]; // 92
```

---

## Non-Generic Collections

### ArrayList

**Characteristics:**
- Stores objects (boxing/unboxing overhead)
- Dynamic sizing
- Not type-safe
- Legacy collection (use List<T> instead)

```csharp
using System.Collections;

ArrayList arrayList = new ArrayList();

// Adding elements (boxing occurs)
arrayList.Add(1);        // int boxed to object
arrayList.Add("Hello");  // string reference
arrayList.Add(3.14);     // double boxed to object

// Accessing elements (unboxing required)
int number = (int)arrayList[0];  // Unboxing
string text = (string)arrayList[1];

// Common operations
arrayList.Insert(1, "Inserted");
arrayList.Remove("Hello");
arrayList.RemoveAt(0);
bool contains = arrayList.Contains(3.14);

// Iteration
foreach (object item in arrayList)
{
    Console.WriteLine(item);
}
```

### Hashtable

**Characteristics:**
- Key-value pairs
- Keys and values are objects
- Not type-safe
- Not thread-safe
- Legacy collection (use Dictionary<TKey, TValue> instead)

```csharp
Hashtable hashtable = new Hashtable();

// Adding key-value pairs
hashtable.Add("name", "John");
hashtable.Add("age", 30);
hashtable["city"] = "New York"; // Alternative syntax

// Accessing values
string name = (string)hashtable["name"];
int age = (int)hashtable["age"];

// Check if key exists
if (hashtable.ContainsKey("name"))
{
    Console.WriteLine($"Name: {hashtable["name"]}");
}

// Iteration
foreach (DictionaryEntry entry in hashtable)
{
    Console.WriteLine($"{entry.Key}: {entry.Value}");
}

// Keys and Values collections
foreach (object key in hashtable.Keys)
{
    Console.WriteLine($"Key: {key}");
}
```

### Queue (Non-Generic)

```csharp
Queue queue = new Queue();

// Enqueue (add to rear)
queue.Enqueue("First");
queue.Enqueue("Second");
queue.Enqueue("Third");

// Dequeue (remove from front)
object first = queue.Dequeue(); // "First"

// Peek (look at front without removing)
object next = queue.Peek(); // "Second"

// Properties
int count = queue.Count;
```

### Stack (Non-Generic)

```csharp
Stack stack = new Stack();

// Push (add to top)
stack.Push("Bottom");
stack.Push("Middle");
stack.Push("Top");

// Pop (remove from top)
object top = stack.Pop(); // "Top"

// Peek (look at top without removing)
object current = stack.Peek(); // "Middle"

// Properties
int count = stack.Count;
```

---

## Generic Collections

### List<T>

**Characteristics:**
- Type-safe
- Dynamic sizing
- Indexed access
- Implements IList<T>, ICollection<T>, IEnumerable<T>

```csharp
// Declaration and initialization
List<int> numbers = new List<int>();
List<string> names = new List<string> {"Alice", "Bob", "Charlie"};
List<int> range = new List<int>(Enumerable.Range(1, 10));

// Adding elements
numbers.Add(1);
numbers.AddRange(new[] {2, 3, 4, 5});
numbers.Insert(0, 0); // Insert at index 0

// Accessing elements
int first = numbers[0];
int last = numbers[numbers.Count - 1];

// Searching
int index = numbers.IndexOf(3);
bool contains = numbers.Contains(4);
List<int> found = numbers.FindAll(x => x > 3);
int firstGreaterThan2 = numbers.Find(x => x > 2);

// Removing elements
numbers.Remove(3);        // Remove first occurrence
numbers.RemoveAt(0);      // Remove at index
numbers.RemoveAll(x => x > 4); // Remove all matching

// Sorting and manipulation
numbers.Sort();
numbers.Reverse();
numbers.Clear();

// LINQ operations
var evenNumbers = numbers.Where(x => x % 2 == 0).ToList();
var doubled = numbers.Select(x => x * 2).ToList();
```

### Dictionary<TKey, TValue>

**Characteristics:**
- Key-value pairs
- Type-safe
- Fast lookups O(1) average case
- Keys must be unique
- Implements IDictionary<TKey, TValue>

```csharp
// Declaration and initialization
Dictionary<string, int> ages = new Dictionary<string, int>();
Dictionary<int, string> names = new Dictionary<int, string>
{
    {1, "Alice"},
    {2, "Bob"},
    {3, "Charlie"}
};

// Adding elements
ages.Add("Alice", 25);
ages["Bob"] = 30;  // Alternative syntax, overwrites if exists

// Accessing elements
int aliceAge = ages["Alice"];

// Safe access
if (ages.TryGetValue("Charlie", out int charlieAge))
{
    Console.WriteLine($"Charlie is {charlieAge} years old");
}

// Check existence
bool hasAlice = ages.ContainsKey("Alice");
bool hasAge30 = ages.ContainsValue(30);

// Removing elements
ages.Remove("Alice");
ages.Clear();

// Iteration
foreach (KeyValuePair<string, int> kvp in ages)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}

// Keys and Values
foreach (string name in ages.Keys)
{
    Console.WriteLine($"Name: {name}");
}

foreach (int age in ages.Values)
{
    Console.WriteLine($"Age: {age}");
}

// LINQ operations
var adults = ages.Where(kvp => kvp.Value >= 18).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
```

### HashSet<T>

**Characteristics:**
- Unique elements only
- Fast lookups, additions, removals O(1) average case
- No indexed access
- Implements ISet<T>

```csharp
// Declaration and initialization
HashSet<int> numbers = new HashSet<int>();
HashSet<string> uniqueNames = new HashSet<string> {"Alice", "Bob", "Alice"}; // Only one "Alice"

// Adding elements
numbers.Add(1);
numbers.Add(2);
numbers.Add(1); // Duplicate, won't be added

// Set operations
HashSet<int> set1 = new HashSet<int> {1, 2, 3, 4};
HashSet<int> set2 = new HashSet<int> {3, 4, 5, 6};

// Union
set1.UnionWith(set2); // {1, 2, 3, 4, 5, 6}

// Intersection
HashSet<int> intersection = new HashSet<int>(set1);
intersection.IntersectWith(set2); // {3, 4}

// Difference
HashSet<int> difference = new HashSet<int>(set1);
difference.ExceptWith(set2); // {1, 2}

// Symmetric difference
HashSet<int> symmetricDiff = new HashSet<int>(set1);
symmetricDiff.SymmetricExceptWith(set2); // {1, 2, 5, 6}

// Subset/Superset checks
bool isSubset = set1.IsSubsetOf(set2);
bool isSuperset = set1.IsSupersetOf(set2);

// Removing duplicates from List
List<int> listWithDuplicates = new List<int> {1, 2, 2, 3, 3, 4};
HashSet<int> uniqueNumbers = new HashSet<int>(listWithDuplicates);
```

### Queue<T>

**Characteristics:**
- FIFO (First In, First Out)
- Type-safe
- No indexed access

```csharp
Queue<string> queue = new Queue<string>();

// Enqueue (add to rear)
queue.Enqueue("First");
queue.Enqueue("Second");
queue.Enqueue("Third");

// Dequeue (remove from front)
string first = queue.Dequeue(); // "First"

// Peek (look at front without removing)
string next = queue.Peek(); // "Second"

// Properties and methods
int count = queue.Count;
bool contains = queue.Contains("Second");
string[] array = queue.ToArray();

// Iteration (doesn't modify queue)
foreach (string item in queue)
{
    Console.WriteLine(item);
}

// Process all items
while (queue.Count > 0)
{
    string item = queue.Dequeue();
    Console.WriteLine($"Processing: {item}");
}
```

### Stack<T>

**Characteristics:**
- LIFO (Last In, First Out)
- Type-safe
- No indexed access

```csharp
Stack<int> stack = new Stack<int>();

// Push (add to top)
stack.Push(1);
stack.Push(2);
stack.Push(3);

// Pop (remove from top)
int top = stack.Pop(); // 3

// Peek (look at top without removing)
int current = stack.Peek(); // 2

// Properties and methods
int count = stack.Count;
bool contains = stack.Contains(2);
int[] array = stack.ToArray();

// Iteration (doesn't modify stack)
foreach (int item in stack)
{
    Console.WriteLine(item); // Prints in LIFO order
}

// Process all items
while (stack.Count > 0)
{
    int item = stack.Pop();
    Console.WriteLine($"Processing: {item}");
}
```

### LinkedList<T>

**Characteristics:**
- Doubly-linked list
- Efficient insertion/deletion at any position
- No indexed access
- Each element is a LinkedListNode<T>

```csharp
LinkedList<string> linkedList = new LinkedList<string>();

// Adding elements
LinkedListNode<string> first = linkedList.AddFirst("First");
LinkedListNode<string> last = linkedList.AddLast("Last");
LinkedListNode<string> middle = linkedList.AddAfter(first, "Middle");

// Navigation
LinkedListNode<string> current = linkedList.First;
while (current != null)
{
    Console.WriteLine(current.Value);
    current = current.Next;
}

// Removing elements
linkedList.Remove("Middle");
linkedList.RemoveFirst();
linkedList.RemoveLast();

// Finding elements
LinkedListNode<string> found = linkedList.Find("Last");
if (found != null)
{
    Console.WriteLine($"Found: {found.Value}");
}
```

---

## Concurrent Collections

### ConcurrentDictionary<TKey, TValue>

**Characteristics:**
- Thread-safe dictionary
- Lock-free for reads
- Atomic operations for updates

```csharp
using System.Collections.Concurrent;

ConcurrentDictionary<string, int> concurrentDict = new ConcurrentDictionary<string, int>();

// Thread-safe operations
concurrentDict.TryAdd("key1", 1);
concurrentDict.TryUpdate("key1", 2, 1); // Update if current value is 1

// Atomic operations
int newValue = concurrentDict.AddOrUpdate("key2", 1, (key, oldValue) => oldValue + 1);

// Get or add
int value = concurrentDict.GetOrAdd("key3", 3);

// Safe removal
if (concurrentDict.TryRemove("key1", out int removedValue))
{
    Console.WriteLine($"Removed: {removedValue}");
}

// Parallel operations example
Parallel.For(0, 1000, i =>
{
    concurrentDict.TryAdd($"key{i}", i);
});
```

### ConcurrentQueue<T>

**Characteristics:**
- Thread-safe FIFO queue
- Lock-free implementation

```csharp
ConcurrentQueue<int> concurrentQueue = new ConcurrentQueue<int>();

// Thread-safe enqueue
concurrentQueue.Enqueue(1);
concurrentQueue.Enqueue(2);

// Thread-safe dequeue
if (concurrentQueue.TryDequeue(out int result))
{
    Console.WriteLine($"Dequeued: {result}");
}

// Peek
if (concurrentQueue.TryPeek(out int peeked))
{
    Console.WriteLine($"Peeked: {peeked}");
}

// Producer-Consumer example
Task producer = Task.Run(() =>
{
    for (int i = 0; i < 100; i++)
    {
        concurrentQueue.Enqueue(i);
        Thread.Sleep(10);
    }
});

Task consumer = Task.Run(() =>
{
    while (!producer.IsCompleted || !concurrentQueue.IsEmpty)
    {
        if (concurrentQueue.TryDequeue(out int item))
        {
            Console.WriteLine($"Consumed: {item}");
        }
        Thread.Sleep(15);
    }
});

await Task.WhenAll(producer, consumer);
```

### BlockingCollection<T>

**Characteristics:**
- Thread-safe collection with blocking semantics
- Wrapper around IProducerConsumerCollection<T>
- Supports bounded capacity

```csharp
using (BlockingCollection<int> blockingCollection = new BlockingCollection<int>(boundedCapacity: 10))
{
    // Producer task
    Task producer = Task.Run(() =>
    {
        for (int i = 0; i < 20; i++)
        {
            blockingCollection.Add(i);
            Console.WriteLine($"Produced: {i}");
            Thread.Sleep(100);
        }
        blockingCollection.CompleteAdding();
    });

    // Consumer task
    Task consumer = Task.Run(() =>
    {
        foreach (int item in blockingCollection.GetConsumingEnumerable())
        {
            Console.WriteLine($"Consumed: {item}");
            Thread.Sleep(150);
        }
    });

    await Task.WhenAll(producer, consumer);
}

// Multiple producers, single consumer
BlockingCollection<string> messages = new BlockingCollection<string>();

// Multiple producer tasks
Task[] producers = Enumerable.Range(0, 3).Select(i =>
    Task.Run(() =>
    {
        for (int j = 0; j < 5; j++)
        {
            messages.Add($"Producer {i}, Message {j}");
            Thread.Sleep(50);
        }
    })
).ToArray();

// Consumer task
Task consumer2 = Task.Run(() =>
{
    while (!messages.IsCompleted)
    {
        if (messages.TryTake(out string message, 100))
        {
            Console.WriteLine($"Received: {message}");
        }
    }
});

await Task.WhenAll(producers);
messages.CompleteAdding();
await consumer2;
```
---


## Collection Interfaces

### IEnumerable<T> vs ICollection<T> vs IList<T>

| Interface | Purpose | Key Members | Use Case |
|-----------|---------|-------------|----------|
| `IEnumerable<T>` | Forward-only iteration | `GetEnumerator()` | Read-only iteration, LINQ |
| `ICollection<T>` | Basic collection operations | `Add()`, `Remove()`, `Count`, `Contains()` | Basic collection manipulation |
| `IList<T>` | Indexed access | `this[int]`, `IndexOf()`, `Insert()`, `RemoveAt()` | Random access collections |

```csharp
// IEnumerable<T> - Read-only iteration
public void ProcessItems(IEnumerable<string> items)
{
    foreach (string item in items)
    {
        Console.WriteLine(item);
    }
    
    // LINQ operations
    var filtered = items.Where(x => x.Length > 5);
}

// ICollection<T> - Basic collection operations
public void ManageCollection(ICollection<string> collection)
{
    collection.Add("New Item");
    collection.Remove("Old Item");
    
    if (collection.Contains("Specific Item"))
    {
        Console.WriteLine($"Collection has {collection.Count} items");
    }
}

// IList<T> - Indexed access
public void ManageList(IList<string> list)
{
    list[0] = "Updated First Item";
    list.Insert(1, "Inserted Item");
    
    int index = list.IndexOf("Search Item");
    if (index >= 0)
    {
        list.RemoveAt(index);
    }
}
```

### IDictionary<TKey, TValue>

```csharp
public void ProcessDictionary(IDictionary<string, int> dictionary)
{
    // Add or update
    dictionary["key1"] = 100;
    
    // Safe access
    if (dictionary.TryGetValue("key2", out int value))
    {
        Console.WriteLine($"Value: {value}");
    }
    
    // Iterate over keys and values
    foreach (var kvp in dictionary)
    {
        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }
}
```

---

## Performance Comparison

### Time Complexity Comparison

| Collection | Access | Search | Insertion | Deletion | Memory |
|------------|--------|--------|-----------|----------|---------|
| `Array` | O(1) | O(n) | N/A | N/A | Minimal |
| `List<T>` | O(1) | O(n) | O(1) amortized | O(n) | Low |
| `LinkedList<T>` | O(n) | O(n) | O(1) | O(1) | Medium |
| `Dictionary<T>` | O(1) avg | O(1) avg | O(1) avg | O(1) avg | Medium |
| `HashSet<T>` | N/A | O(1) avg | O(1) avg | O(1) avg | Medium |
| `Queue<T>` | N/A | O(n) | O(1) | O(1) | Low |
| `Stack<T>` | N/A | O(n) | O(1) | O(1) | Low |

### Benchmark Example

```csharp
using System.Diagnostics;

public class CollectionBenchmark
{
    private const int ItemCount = 1_000_000;
    
    public void BenchmarkCollections()
    {
        // List vs LinkedList insertion
        BenchmarkListInsertion();
        BenchmarkLinkedListInsertion();
        
        // Dictionary vs Hashtable lookup
        BenchmarkDictionaryLookup();
        BenchmarkHashtableLookup();
    }
    
    private void BenchmarkListInsertion()
    {
        var stopwatch = Stopwatch.StartNew();
        var list = new List<int>();
        
        for (int i = 0; i < ItemCount; i++)
        {
            list.Add(i);
        }
        
        stopwatch.Stop();
        Console.WriteLine($"List<int> insertion: {stopwatch.ElapsedMilliseconds}ms");
    }
    
    private void BenchmarkLinkedListInsertion()
    {
        var stopwatch = Stopwatch.StartNew();
        var linkedList = new LinkedList<int>();
        
        for (int i = 0; i < ItemCount; i++)
        {
            linkedList.AddLast(i);
        }
        
        stopwatch.Stop();
        Console.WriteLine($"LinkedList<int> insertion: {stopwatch.ElapsedMilliseconds}ms");
    }
    
    private void BenchmarkDictionaryLookup()
    {
        var dictionary = new Dictionary<int, string>();
        for (int i = 0; i < ItemCount; i++)
        {
            dictionary[i] = $"Value{i}";
        }
        
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < ItemCount; i++)
        {
            var value = dictionary[i];
        }
        stopwatch.Stop();
        
        Console.WriteLine($"Dictionary<int, string> lookup: {stopwatch.ElapsedMilliseconds}ms");
    }
    
    private void BenchmarkHashtableLookup()
    {
        var hashtable = new Hashtable();
        for (int i = 0; i < ItemCount; i++)
        {
            hashtable[i] = $"Value{i}";
        }
        
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < ItemCount; i++)
        {
            var value = hashtable[i];
        }
        stopwatch.Stop();
        
        Console.WriteLine($"Hashtable lookup: {stopwatch.ElapsedMilliseconds}ms");
    }
}
```

### Memory Usage Considerations

```csharp
public class MemoryUsageExample
{
    public void DemonstrateMemoryUsage()
    {
        // List<T> - Contiguous memory, good cache locality
        List<int> list = new List<int>(1000); // Pre-allocate capacity
        
        // LinkedList<T> - Non-contiguous memory, poor cache locality
        LinkedList<int> linkedList = new LinkedList<int>();
        
        // Dictionary<T> - Hash table with buckets
        Dictionary<string, int> dictionary = new Dictionary<string, int>(1000); // Pre-allocate
        
        // HashSet<T> - Similar to Dictionary but only keys
        HashSet<int> hashSet = new HashSet<int>();
        
        // Boxing/Unboxing overhead in non-generic collections
        ArrayList arrayList = new ArrayList(); // Avoid - uses object references
        arrayList.Add(1); // Boxing: int -> object
        int value = (int)arrayList[0]; // Unboxing: object -> int
    }
}
```

---

## Real-World Use Cases

### 1. Caching System with Dictionary

```csharp
public class CacheService<TKey, TValue>
{
    private readonly Dictionary<TKey, CacheItem<TValue>> _cache;
    private readonly TimeSpan _defaultExpiry;
    
    public CacheService(TimeSpan defaultExpiry)
    {
        _cache = new Dictionary<TKey, CacheItem<TValue>>();
        _defaultExpiry = defaultExpiry;
    }
    
    public void Set(TKey key, TValue value, TimeSpan? expiry = null)
    {
        var expiryTime = DateTime.UtcNow.Add(expiry ?? _defaultExpiry);
        _cache[key] = new CacheItem<TValue>(value, expiryTime);
    }
    
    public bool TryGet(TKey key, out TValue value)
    {
        if (_cache.TryGetValue(key, out var cacheItem))
        {
            if (cacheItem.ExpiryTime > DateTime.UtcNow)
            {
                value = cacheItem.Value;
                return true;
            }
            else
            {
                _cache.Remove(key); // Remove expired item
            }
        }
        
        value = default(TValue);
        return false;
    }
    
    private class CacheItem<T>
    {
        public T Value { get; }
        public DateTime ExpiryTime { get; }
        
        public CacheItem(T value, DateTime expiryTime)
        {
            Value = value;
            ExpiryTime = expiryTime;
        }
    }
}

// Usage
var cache = new CacheService<string, UserProfile>(TimeSpan.FromMinutes(30));
cache.Set("user123", new UserProfile { Name = "John", Email = "john@example.com" });

if (cache.TryGet("user123", out UserProfile profile))
{
    Console.WriteLine($"Cached user: {profile.Name}");
}
```

### 2. Task Queue with ConcurrentQueue

```csharp
public class TaskProcessor
{
    private readonly ConcurrentQueue<WorkItem> _taskQueue;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly Task[] _workers;
    
    public TaskProcessor(int workerCount = Environment.ProcessorCount)
    {
        _taskQueue = new ConcurrentQueue<WorkItem>();
        _cancellationTokenSource = new CancellationTokenSource();
        _workers = new Task[workerCount];
        
        // Start worker tasks
        for (int i = 0; i < workerCount; i++)
        {
            _workers[i] = Task.Run(() => ProcessTasks(_cancellationTokenSource.Token));
        }
    }
    
    public void EnqueueTask(Func<Task> taskFunc, string description = null)
    {
        _taskQueue.Enqueue(new WorkItem(taskFunc, description));
    }
    
    private async Task ProcessTasks(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (_taskQueue.TryDequeue(out WorkItem workItem))
            {
                try
                {
                    await workItem.TaskFunc();
                    Console.WriteLine($"Completed: {workItem.Description}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {workItem.Description}: {ex.Message}");
                }
            }
            else
            {
                await Task.Delay(100, cancellationToken); // Wait for new tasks
            }
        }
    }
    
    public async Task StopAsync()
    {
        _cancellationTokenSource.Cancel();
        await Task.WhenAll(_workers);
    }
    
    private class WorkItem
    {
        public Func<Task> TaskFunc { get; }
        public string Description { get; }
        
        public WorkItem(Func<Task> taskFunc, string description)
        {
            TaskFunc = taskFunc;
            Description = description ?? "Unknown Task";
        }
    }
}

// Usage
var processor = new TaskProcessor(4);

for (int i = 0; i < 100; i++)
{
    int taskId = i;
    processor.EnqueueTask(async () =>
    {
        await Task.Delay(1000); // Simulate work
        Console.WriteLine($"Task {taskId} completed");
    }, $"Task {taskId}");
}

await Task.Delay(10000); // Let tasks process
await processor.StopAsync();
```

### 3. Unique Visitor Tracking with HashSet

```csharp
public class VisitorTracker
{
    private readonly HashSet<string> _uniqueVisitors;
    private readonly Dictionary<string, List<DateTime>> _visitorHistory;
    
    public VisitorTracker()
    {
        _uniqueVisitors = new HashSet<string>();
        _visitorHistory = new Dictionary<string, List<DateTime>>();
    }
    
    public void RecordVisit(string visitorId)
    {
        _uniqueVisitors.Add(visitorId); // Automatically handles duplicates
        
        if (!_visitorHistory.ContainsKey(visitorId))
        {
            _visitorHistory[visitorId] = new List<DateTime>();
        }
        
        _visitorHistory[visitorId].Add(DateTime.UtcNow);
    }
    
    public int GetUniqueVisitorCount() => _uniqueVisitors.Count;
    
    public int GetTotalVisits() => _visitorHistory.Values.Sum(visits => visits.Count);
    
    public List<string> GetFrequentVisitors(int minVisits)
    {
        return _visitorHistory
            .Where(kvp => kvp.Value.Count >= minVisits)
            .Select(kvp => kvp.Key)
            .ToList();
    }
    
    public HashSet<string> GetVisitorsInTimeRange(DateTime start, DateTime end)
    {
        var visitorsInRange = new HashSet<string>();
        
        foreach (var kvp in _visitorHistory)
        {
            if (kvp.Value.Any(visit => visit >= start && visit <= end))
            {
                visitorsInRange.Add(kvp.Key);
            }
        }
        
        return visitorsInRange;
    }
}

// Usage
var tracker = new VisitorTracker();

// Simulate visits
string[] visitors = {"user1", "user2", "user3", "user1", "user2", "user1"};
foreach (string visitor in visitors)
{
    tracker.RecordVisit(visitor);
}

Console.WriteLine($"Unique visitors: {tracker.GetUniqueVisitorCount()}"); // 3
Console.WriteLine($"Total visits: {tracker.GetTotalVisits()}"); // 6

var frequentVisitors = tracker.GetFrequentVisitors(2);
Console.WriteLine($"Frequent visitors: {string.Join(", ", frequentVisitors)}");
```

### 4. LRU Cache with LinkedList and Dictionary

```csharp
public class LRUCache<TKey, TValue>
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<CacheItem>> _cache;
    private readonly LinkedList<CacheItem> _lruList;
    
    public LRUCache(int capacity)
    {
        _capacity = capacity;
        _cache = new Dictionary<TKey, LinkedListNode<CacheItem>>();
        _lruList = new LinkedList<CacheItem>();
    }
    
    public bool TryGet(TKey key, out TValue value)
    {
        if (_cache.TryGetValue(key, out var node))
        {
            // Move to front (most recently used)
            _lruList.Remove(node);
            _lruList.AddFirst(node);
            
            value = node.Value.Value;
            return true;
        }
        
        value = default(TValue);
        return false;
    }
    
    public void Put(TKey key, TValue value)
    {
        if (_cache.TryGetValue(key, out var existingNode))
        {
            // Update existing item
            existingNode.Value.Value = value;
            _lruList.Remove(existingNode);
            _lruList.AddFirst(existingNode);
        }
        else
        {
            // Add new item
            if (_cache.Count >= _capacity)
            {
                // Remove least recently used item
                var lru = _lruList.Last;
                _lruList.RemoveLast();
                _cache.Remove(lru.Value.Key);
            }
            
            var newItem = new CacheItem(key, value);
            var newNode = _lruList.AddFirst(newItem);
            _cache[key] = newNode;
        }
    }
    
    private class CacheItem
    {
        public TKey Key { get; }
        public TValue Value { get; set; }
        
        public CacheItem(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}

// Usage
var lruCache = new LRUCache<string, string>(3);

lruCache.Put("1", "One");
lruCache.Put("2", "Two");
lruCache.Put("3", "Three");

if (lruCache.TryGet("1", out string value))
{
    Console.WriteLine($"Found: {value}"); // "One"
}

lruCache.Put("4", "Four"); // This will evict "2" (least recently used)

if (!lruCache.TryGet("2", out value))
{
    Console.WriteLine("Key '2' was evicted");
}
```

---

## Interview Questions

### Fundamental Questions

#### 1. **What is the difference between Array and List<T>?**

**Answer:**

| Aspect | Array | List<T> |
|--------|-------|---------|
| **Size** | Fixed at creation | Dynamic, grows automatically |
| **Performance** | Fastest access O(1) | Slightly slower due to bounds checking |
| **Memory** | Contiguous, minimal overhead | Contiguous with some overhead for capacity management |
| **Functionality** | Basic operations only | Rich set of methods (Add, Remove, Find, etc.) |
| **Type Safety** | Type-safe | Type-safe |
| **LINQ Support** | Yes | Yes |

```csharp
// Array - fixed size
int[] array = new int[5]; // Cannot grow beyond 5
// array[5] = 10; // Runtime exception

// List<T> - dynamic size
List<int> list = new List<int>(); // Starts empty, can grow
list.Add(1);
list.Add(2); // No size limit (within memory constraints)

// Performance comparison
int[] largeArray = new int[1000000];
List<int> largeList = new List<int>(1000000); // Pre-allocate for better performance

// Array access is slightly faster
int value1 = largeArray[500000]; // Direct memory access

// List access has bounds checking overhead
int value2 = largeList[500000]; // Bounds checking + memory access
```

#### 2. **Explain the difference between List<T> and LinkedList<T>. When would you use each?**

**Answer:**

| Aspect | List<T> | LinkedList<T> |
|--------|---------|---------------|
| **Storage** | Contiguous array | Doubly-linked nodes |
| **Random Access** | O(1) | O(n) |
| **Insertion/Deletion at ends** | O(1) amortized | O(1) |
| **Insertion/Deletion in middle** | O(n) | O(1) if you have the node |
| **Memory Usage** | Lower | Higher (node overhead) |
| **Cache Performance** | Better | Worse |

```csharp
// Use List<T> when:
// - You need random access by index
// - Memory usage is a concern
// - You do more reading than inserting/deleting

List<string> userList = new List<string>();
userList.Add("User1");
string firstUser = userList[0]; // O(1) access

// Use LinkedList<T> when:
// - Frequent insertions/deletions in the middle
// - You don't need random access
// - You work with nodes directly

LinkedList<Task> taskQueue = new LinkedList<Task>();
var taskNode = taskQueue.AddLast(new Task(() => Console.WriteLine("Task")));
taskQueue.AddBefore(taskNode, new Task(() => Console.WriteLine("Priority Task"))); // O(1) insertion
```

#### 3. **What is the difference between Dictionary<T> and Hashtable?**

**Answer:**

| Aspect | Dictionary<TKey, TValue> | Hashtable |
|--------|--------------------------|-----------|
| **Type Safety** | Generic, type-safe | Non-generic, stores objects |
| **Performance** | No boxing/unboxing | Boxing/unboxing overhead |
| **Null Keys** | Depends on key type | Not allowed |
| **Thread Safety** | Not thread-safe | Thread-safe for reads |
| **Introduced** | .NET 2.0 | .NET 1.0 |

```csharp
// Dictionary<T> - Modern, type-safe approach
Dictionary<string, int> ages = new Dictionary<string, int>();
ages["John"] = 30; // No boxing
int johnAge = ages["John"]; // No unboxing, compile-time type safety

// Hashtable - Legacy, avoid in new code
Hashtable hashtable = new Hashtable();
hashtable["John"] = 30; // Boxing: int -> object
int age = (int)hashtable["John"]; // Unboxing: object -> int, runtime cast required

// Performance impact
for (int i = 0; i < 1000000; i++)
{
    hashtable[i.ToString()] = i; // Boxing overhead
    ages[i.ToString()] = i; // No boxing
}
```

#### 4. **Explain IEnumerable<T>, ICollection<T>, and IList<T>. What's the hierarchy?**

**Answer:**

```
IEnumerable<T>
    ↑
ICollection<T>
    ↑
IList<T>
```

```csharp
// IEnumerable<T> - Basic iteration
public void ProcessItems(IEnumerable<string> items)
{
    foreach (string item in items) // Only supports iteration
    {
        Console.WriteLine(item);
    }
    
    // LINQ operations available
    var filtered = items.Where(x => x.Length > 5);
}

// ICollection<T> - Basic collection operations
public void ManageCollection(ICollection<string> collection)
{
    collection.Add("New Item");     // Can add
    collection.Remove("Old Item");  // Can remove
    int count = collection.Count;   // Has count
    bool contains = collection.Contains("Item"); // Can check existence
    
    // Still supports iteration (inherits from IEnumerable<T>)
    foreach (string item in collection) { }
}

// IList<T> - Indexed access
public void ManageList(IList<string> list)
{
    string first = list[0];         // Indexed access
    list[1] = "Updated";           // Indexed assignment
    list.Insert(2, "Inserted");   // Insert at specific position
    list.RemoveAt(3);              // Remove at specific position
    int index = list.IndexOf("Find"); // Find index of item
    
    // Supports all ICollection<T> and IEnumerable<T> operations
}

// Real-world usage
public class DataService
{
    // Accept most flexible interface for input
    public void ProcessData(IEnumerable<DataItem> items) { }
    
    // Return specific type when you need specific functionality
    public IList<DataItem> GetEditableData() => new List<DataItem>();
}
```

### Advanced Questions

#### 5. **How does Dictionary<T> handle hash collisions? What happens when two keys have the same hash code?**

**Answer:**

Dictionary<T> uses **separate chaining** with **buckets** to handle hash collisions:

```csharp
public class HashCollisionDemo
{
    public void DemonstrateCollisions()
    {
        // Custom class with intentional hash collision
        var dict = new Dictionary<CollisionKey, string>();
        
        var key1 = new CollisionKey("A");
        var key2 = new CollisionKey("B"); // Same hash code as key1
        
        dict[key1] = "Value A";
        dict[key2] = "Value B"; // Both stored despite same hash code
        
        Console.WriteLine(dict[key1]); // "Value A"
        Console.WriteLine(dict[key2]); // "Value B"
    }
}

public class CollisionKey
{
    public string Value { get; }
    
    public CollisionKey(string value) => Value = value;
    
    // Intentionally return same hash code for demonstration
    public override int GetHashCode() => 42;
    
    // Equals is crucial for collision resolution
    public override bool Equals(object obj)
    {
        return obj is CollisionKey other && Value == other.Value;
    }
}

// Dictionary internal structure (simplified):
// Bucket 0: null
// Bucket 1: null
// Bucket 42: [CollisionKey("A"), "Value A"] -> [CollisionKey("B"), "Value B"]
// Bucket 43: null
```

**Process:**
1. Calculate hash code of key
2. Map hash code to bucket index: `bucket = hashCode % bucketCount`
3. If bucket is empty, store key-value pair
4. If bucket has items, use `Equals()` to find exact match
5. If no match found, add to the chain in that bucket

#### 6. **What are the thread-safety characteristics of different collections?**

**Answer:**

| Collection | Thread Safety | Details |
|------------|---------------|---------|
| `List<T>` | Not thread-safe | Multiple readers OK, any writer requires synchronization |
| `Dictionary<T>` | Not thread-safe | Same as List<T> |
| `HashSet<T>` | Not thread-safe | Same as List<T> |
| `Queue<T>` | Not thread-safe | Same as List<T> |
| `Stack<T>` | Not thread-safe | Same as List<T> |
| `ConcurrentDictionary<T>` | Thread-safe | Lock-free reads, atomic updates |
| `ConcurrentQueue<T>` | Thread-safe | Lock-free implementation |
| `ConcurrentStack<T>` | Thread-safe | Lock-free implementation |
| `BlockingCollection<T>` | Thread-safe | Blocking semantics for producer-consumer |

```csharp
// Thread-unsafe example
List<int> unsafeList = new List<int>();

// This can cause race conditions
Parallel.For(0, 1000, i =>
{
    unsafeList.Add(i); // NOT SAFE - can corrupt internal state
});

// Thread-safe alternatives
ConcurrentBag<int> safeBag = new ConcurrentBag<int>();
Parallel.For(0, 1000, i =>
{
    safeBag.Add(i); // SAFE - thread-safe operations
});

// Manual synchronization for non-concurrent collections
List<int> synchronizedList = new List<int>();
object lockObject = new object();

Parallel.For(0, 1000, i =>
{
    lock (lockObject)
    {
        synchronizedList.Add(i); // SAFE - but slower due to locking
    }
});

// Reader-writer scenarios
Dictionary<string, int> dict = new Dictionary<string, int>();
ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

// Multiple readers can access simultaneously
Task.Run(() =>
{
    rwLock.EnterReadLock();
    try
    {
        var value = dict.ContainsKey("key1"); // Safe concurrent read
    }
    finally
    {
        rwLock.ExitReadLock();
    }
});

// Writers need exclusive access
Task.Run(() =>
{
    rwLock.EnterWriteLock();
    try
    {
        dict["key1"] = 100; // Exclusive write access
    }
    finally
    {
        rwLock.ExitWriteLock();
    }
});
```

#### 7. **Explain the internal working of List<T>. How does it grow? What is the capacity vs count?**

**Answer:**

```csharp
public class ListInternalsDemo
{
    public void DemonstrateListGrowth()
    {
        List<int> list = new List<int>();
        
        Console.WriteLine($"Initial - Count: {list.Count}, Capacity: {list.Capacity}");
        // Output: Count: 0, Capacity: 0
        
        // Add first element
        list.Add(1);
        Console.WriteLine($"After 1 add - Count: {list.Count}, Capacity: {list.Capacity}");
        // Output: Count: 1, Capacity: 4 (default initial capacity)
        
        // Add more elements
        for (int i = 2; i <= 5; i++)
        {
            list.Add(i);
            Console.WriteLine($"After {i} adds - Count: {list.Count}, Capacity: {list.Capacity}");
        }
        // Capacity doubles when exceeded: 4 -> 8
        
        // Pre-allocate for better performance
        List<int> optimizedList = new List<int>(1000); // Initial capacity 1000
        Console.WriteLine($"Pre-allocated - Count: {optimizedList.Count}, Capacity: {optimizedList.Capacity}");
        // Output: Count: 0, Capacity: 1000
    }
}

// Internal structure (simplified):
public class MyList<T>
{
    private T[] _items;
    private int _size;
    
    public MyList()
    {
        _items = new T[0];
        _size = 0;
    }
    
    public void Add(T item)
    {
        if (_size == _items.Length)
        {
            Grow();
        }
        
        _items[_size] = item;
        _size++;
    }
    
    private void Grow()
    {
        int newCapacity = _items.Length == 0 ? 4 : _items.Length * 2;
        T[] newArray = new T[newCapacity];
        Array.Copy(_items, newArray, _size);
        _items = newArray;
    }
    
    public int Count => _size;
    public int Capacity => _items.Length;
}
```

**Growth Strategy:**
- Initial capacity: 0
- First add: Capacity becomes 4
- When full: Capacity doubles (4 → 8 → 16 → 32...)
- **Amortized O(1)** insertion time
- **Memory overhead**: Up to 50% unused capacity

#### 8. **What is the difference between ConcurrentDictionary and Dictionary with locks?**

**Answer:**

```csharp
// ConcurrentDictionary - Lock-free for reads
ConcurrentDictionary<string, int> concurrentDict = new ConcurrentDictionary<string, int>();

// Multiple threads can read simultaneously without blocking
Parallel.For(0, 1000, i =>
{
    concurrentDict.TryGetValue($"key{i}", out int value); // Lock-free read
});

// Atomic operations
concurrentDict.AddOrUpdate("counter", 1, (key, oldValue) => oldValue + 1);

// Dictionary with manual locking
Dictionary<string, int> lockedDict = new Dictionary<string, int>();
ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

// Reads require lock acquisition
Parallel.For(0, 1000, i =>
{
    rwLock.EnterReadLock();
    try
    {
        lockedDict.TryGetValue($"key{i}", out int value); // Requires lock
    }
    finally
    {
        rwLock.ExitReadLock();
    }
});

// Performance comparison
public class ConcurrencyBenchmark
{
    private readonly ConcurrentDictionary<int, string> _concurrentDict = new();
    private readonly Dictionary<int, string> _lockedDict = new();
    private readonly ReaderWriterLockSlim _lock = new();
    
    public void BenchmarkReads()
    {
        // Setup data
        for (int i = 0; i < 10000; i++)
        {
            _concurrentDict[i] = $"Value{i}";
            _lockedDict[i] = $"Value{i}";
        }
        
        // ConcurrentDictionary reads - faster, no contention
        var sw1 = Stopwatch.StartNew();
        Parallel.For(0, 100000, i =>
        {
            _concurrentDict.TryGetValue(i % 10000, out _);
        });
        sw1.Stop();
        
        // Locked Dictionary reads - slower due to lock contention
        var sw2 = Stopwatch.StartNew();
        Parallel.For(0, 100000, i =>
        {
            _lock.EnterReadLock();
            try
            {
                _lockedDict.TryGetValue(i % 10000, out _);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        });
        sw2.Stop();
        
        Console.WriteLine($"ConcurrentDictionary: {sw1.ElapsedMilliseconds}ms");
        Console.WriteLine($"Locked Dictionary: {sw2.ElapsedMilliseconds}ms");
    }
}
```

**Key Differences:**

| Aspect | ConcurrentDictionary | Dictionary + Locks |
|--------|---------------------|-------------------|
| **Read Performance** | Lock-free, very fast | Lock acquisition overhead |
| **Write Performance** | Optimized atomic operations | Full synchronization required |
| **Memory Usage** | Higher (lock-free structures) | Lower |
| **Complexity** | Built-in thread safety | Manual lock management |
| **Deadlock Risk** | None | Possible with complex locking |

#### 9. **How would you implement a thread-safe cache with expiration using collections?**

**Answer:**

```csharp
public class ThreadSafeCacheWithExpiration<TKey, TValue>
{
    private readonly ConcurrentDictionary<TKey, CacheEntry<TValue>> _cache;
    private readonly Timer _cleanupTimer;
    private readonly TimeSpan _defaultExpiration;
    
    public ThreadSafeCacheWithExpiration(TimeSpan defaultExpiration, TimeSpan cleanupInterval)
    {
        _cache = new ConcurrentDictionary<TKey, CacheEntry<TValue>>();
        _defaultExpiration = defaultExpiration;
        
        // Periodic cleanup of expired items
        _cleanupTimer = new Timer(CleanupExpiredItems, null, cleanupInterval, cleanupInterval);
    }
    
    public void Set(TKey key, TValue value, TimeSpan? expiration = null)
    {
        var expiryTime = DateTime.UtcNow.Add(expiration ?? _defaultExpiration);
        var entry = new CacheEntry<TValue>(value, expiryTime);
        
        _cache.AddOrUpdate(key, entry, (k, oldEntry) => entry);
    }
    
    public bool TryGet(TKey key, out TValue value)
    {
        if (_cache.TryGetValue(key, out var entry))
        {
            if (entry.ExpiryTime > DateTime.UtcNow)
            {
                value = entry.Value;
                return true;
            }
            else
            {
                // Remove expired item
                _cache.TryRemove(key, out _);
            }
        }
        
        value = default(TValue);
        return false;
    }
    
    public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory, TimeSpan? expiration = null)
    {
        var expiryTime = DateTime.UtcNow.Add(expiration ?? _defaultExpiration);
        
        var entry = _cache.AddOrUpdate(
            key,
            k => new CacheEntry<TValue>(valueFactory(k), expiryTime),
            (k, existingEntry) =>
            {
                if (existingEntry.ExpiryTime > DateTime.UtcNow)
                {
                    return existingEntry; // Return existing if not expired
                }
                else
                {
                    return new CacheEntry<TValue>(valueFactory(k), expiryTime); // Create new if expired
                }
            });
        
        return entry.Value;
    }
    
    private void CleanupExpiredItems(object state)
    {
        var now = DateTime.UtcNow;
        var expiredKeys = new List<TKey>();
        
        foreach (var kvp in _cache)
        {
            if (kvp.Value.ExpiryTime <= now)
            {
                expiredKeys.Add(kvp.Key);
            }
        }
        
        foreach (var key in expiredKeys)
        {
            _cache.TryRemove(key, out _);
        }
        
        Console.WriteLine($"Cleaned up {expiredKeys.Count} expired items");
    }
    
    public void Dispose()
    {
        _cleanupTimer?.Dispose();
    }
    
    private class CacheEntry<T>
    {
        public T Value { get; }
        public DateTime ExpiryTime { get; }
        
        public CacheEntry(T value, DateTime expiryTime)
        {
            Value = value;
            ExpiryTime = expiryTime;
        }
    }
}

// Usage example
public class CacheUsageExample
{
    private readonly ThreadSafeCacheWithExpiration<string, UserProfile> _userCache;
    
    public CacheUsageExample()
    {
        _userCache = new ThreadSafeCacheWithExpiration<string, UserProfile>(
            TimeSpan.FromMinutes(30), // Default expiration
            TimeSpan.FromMinutes(5)   // Cleanup interval
        );
    }
    
    public async Task<UserProfile> GetUserProfileAsync(string userId)
    {
        return _userCache.GetOrAdd(userId, async id =>
        {
            // Expensive database call
            return await LoadUserFromDatabaseAsync(id);
        }, TimeSpan.FromHours(1)); // Custom expiration for this item
    }
    
    private async Task<UserProfile> LoadUserFromDatabaseAsync(string userId)
    {
        // Simulate database call
        await Task.Delay(100);
        return new UserProfile { Id = userId, Name = $"User {userId}" };
    }
}
```

#### 10. **Explain memory allocation and garbage collection impact of different collections.**

**Answer:**

```csharp
public class MemoryAllocationDemo
{
    public void DemonstrateMemoryPatterns()
    {
        // 1. Value types in collections - no additional allocations
        List<int> integers = new List<int> { 1, 2, 3, 4, 5 };
        // Memory: One allocation for the internal array, values stored inline
        
        // 2. Reference types - each object is a separate allocation
        List<string> strings = new List<string> { "A", "B", "C" };
        // Memory: One allocation for List array + separate allocations for each string
        
        // 3. Boxing in non-generic collections
        ArrayList arrayList = new ArrayList();
        arrayList.Add(1); // Boxing: int -> object (heap allocation)
        arrayList.Add(2); // Another boxing allocation
        // Memory: ArrayList array + boxed int objects on heap
        
        // 4. LinkedList - many small allocations
        LinkedList<int> linkedList = new LinkedList<int>();
        linkedList.AddLast(1); // Allocates LinkedListNode<int>
        linkedList.AddLast(2); // Another node allocation
        // Memory: Each node is a separate allocation (poor cache locality)
        
        // 5. Dictionary - hash table with buckets
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        dictionary["key1"] = 1;
        // Memory: Bucket array + Entry structs + string keys
        
        DemonstrateGCPressure();
    }
    
    private void DemonstrateGCPressure()
    {
        // High GC pressure - many temporary allocations
        for (int i = 0; i < 100000; i++)
        {
            var list = new List<string>(); // New allocation each iteration
            list.Add($"Item {i}");         // String allocation
            // List goes out of scope - eligible for GC
        }
        
        // Low GC pressure - reuse collections
        var reusableList = new List<string>(1000); // Pre-allocate capacity
        for (int i = 0; i < 100000; i++)
        {
            reusableList.Clear();          // Reuse existing capacity
            reusableList.Add($"Item {i}"); // Only string allocation
        }
        
        // Object pooling for high-frequency scenarios
        var listPool = new ObjectPool<List<string>>(() => new List<string>());
        
        for (int i = 0; i < 100000; i++)
        {
            var list = listPool.Get();     // Reuse from pool
            try
            {
                list.Add($"Item {i}");
                // Process list
            }
            finally
            {
                list.Clear();
                listPool.Return(list);     // Return to pool
            }
        }
    }
}

// Simple object pool implementation
public class ObjectPool<T> where T : class
{
    private readonly ConcurrentQueue<T> _objects = new ConcurrentQueue<T>();
    private readonly Func<T> _objectGenerator;
    
    public ObjectPool(Func<T> objectGenerator)
    {
        _objectGenerator = objectGenerator;
    }
    
    public T Get()
    {
        return _objects.TryDequeue(out T item) ? item : _objectGenerator();
    }
    
    public void Return(T item)
    {
        _objects.Enqueue(item);
    }
}

// Memory-efficient patterns
public class MemoryEfficientPatterns
{
    // Use ArrayPool for temporary arrays
    public void ProcessLargeData()
    {
        var pool = ArrayPool<byte>.Shared;
        byte[] buffer = pool.Rent(1024); // Rent from pool
        
        try
        {
            // Use buffer for processing
        }
        finally
        {
            pool.Return(buffer); // Return to pool
        }
    }
    
    // Use Span<T> for stack-allocated collections
    public void ProcessSmallData()
    {
        Span<int> numbers = stackalloc int[10]; // Stack allocation
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i * i;
        }
        // No heap allocation, no GC pressure
    }
    
    // Pre-size collections when size is known
    public List<string> ProcessKnownSize(int expectedSize)
    {
        var result = new List<string>(expectedSize); // Avoid resizing
        
        for (int i = 0; i < expectedSize; i++)
        {
            result.Add($"Item {i}");
        }
        
        return result;
    }
}
```

**GC Impact Summary:**

| Collection Pattern | GC Pressure | Recommendation |
|-------------------|-------------|----------------|
| **Frequent small collections** | High | Use object pooling |
| **Large collections with known size** | Medium | Pre-allocate capacity |
| **Boxing in non-generic collections** | High | Use generic collections |
| **LinkedList for large datasets** | High | Consider List<T> instead |
| **Dictionary with string keys** | Medium | Consider string interning |
| **Temporary arrays** | High | Use ArrayPool<T> |

---

## Quick Reference Summary

### Collection Selection Guide

```csharp
// Choose your collection based on usage pattern:

// Random access by index, dynamic sizing
List<T> list = new List<T>();

// Key-value lookups, unique keys
Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

// Unique items, set operations
HashSet<T> set = new HashSet<T>();

// FIFO processing
Queue<T> queue = new Queue<T>();

// LIFO processing
Stack<T> stack = new Stack<T>();

// Frequent insertion/deletion in middle
LinkedList<T> linkedList = new LinkedList<T>();

// Thread-safe key-value operations
ConcurrentDictionary<TKey, TValue> concurrentDict = new ConcurrentDictionary<TKey, TValue>();

// Producer-consumer scenarios
BlockingCollection<T> blockingCollection = new BlockingCollection<T>();
```

### Performance Quick Reference

| Operation | List<T> | LinkedList<T> | Dictionary<T> | HashSet<T> | Queue<T> | Stack<T> |
|-----------|---------|---------------|---------------|------------|----------|----------|
| **Add** | O(1)* | O(1) | O(1)* | O(1)* | O(1) | O(1) |
| **Remove** | O(n) | O(1)** | O(1)* | O(1)* | O(1) | O(1) |
| **Search** | O(n) | O(n) | O(1)* | O(1)* | O(n) | O(n) |
| **Access by Index** | O(1) | O(n) | N/A | N/A | N/A | N/A |

*Amortized time complexity  
**If you have reference to the node

### Memory Usage Guidelines

1. **Use generic collections** - avoid boxing/unboxing
2. **Pre-allocate capacity** when size is known
3. **Consider object pooling** for frequently created collections
4. **Use concurrent collections** only when needed
5. **Prefer List<T> over LinkedList<T>** for most scenarios
6. **Use HashSet<T>** for uniqueness and set operations
7. **Choose Dictionary<T>** over Hashtable always

### Thread Safety Summary

- **Thread-Safe**: ConcurrentDictionary, ConcurrentQueue, ConcurrentStack, BlockingCollection
- **Not Thread-Safe**: List, Dictionary, HashSet, Queue, Stack, LinkedList
- **Legacy Thread-Safe**: Hashtable, ArrayList (avoid in new code)

This completes the comprehensive C# Collections guide with detailed explanations, code examples, performance considerations, and interview-focused content suitable for experienced developers.