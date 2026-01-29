# C# / . NET Core - Complete Interview Guide

> **Quick Reference for Interview Preparation** | Concise Answers | Real-World Examples | 10+ Years Experience

---

## 📑 Table of Contents

### [1. Collections & LINQ](#1-collections--linq)

1.1 [Remove duplicates from a list](#11-remove-duplicates-from-a-list)  
1.2 [Find second highest number](#12-find-second-highest-number)  
1.3 [Group objects using LINQ](#13-group-objects-using-linq)  
1.4 [Flatten nested collections](#14-flatten-nested-collections)  
1.5 [Convert List to Dictionary](#15-convert-list-to-dictionary)

### [2. String Manipulation](#2-string-manipulation)

2.1 [Reverse a string](#21-reverse-a-string)  
2.2 [Check palindrome](#22-check-palindrome)  
2.3 [Find first non-repeating character](#23-find-first-non-repeating-character)  
2.4 [Count character frequency](#24-count-character-frequency)

### [3. Date & Time](#3-date--time)

3.1 [Calculate age from DOB](#31-calculate-age-from-dob)  
3.2 [Find overlapping date ranges](#32-find-overlapping-date-ranges)  
3.3 [Convert UTC to local time](#33-convert-utc-to-local-time)

### [4. Async & Multithreading](#4-async--multithreading)

4.1 [Convert sync to async](#41-convert-sync-to-async)  
4.2 [Task vs Thread](#42-task-vs-thread)  
4.3 [What happens if you don't await?](#43-what-happens-if-you-dont-await)

### [5. Thread Safety](#5-thread-safety)

5.1 [Thread-safe Singleton](#51-thread-safe-singleton)  
5.2 [Lock vs SemaphoreSlim vs ConcurrentDictionary](#52-lock-vs-semaphoreslim-vs-concurrentdictionary)

### [6. Design Patterns](#6-design-patterns)

6.1 [Factory Pattern](#61-factory-pattern)  
6.2 [Strategy Pattern](#62-strategy-pattern)  
6.3 [SOLID Principles](#63-solid-principles)  
6.4 [Immutable Class](#64-immutable-class)

### [7. Data Structures & Algorithms](#7-data-structures--algorithms)

7.1 [Find missing number](#71-find-missing-number)  
7.2 [Two-Sum problem](#72-two-sum-problem)  
7.3 [Valid parentheses](#73-valid-parentheses)  
7.4 [LRU Cache implementation](#74-lru-cache-implementation)

### [8. ASP. NET Core Web API](#8-aspnet-core-web-api)

8.1 [REST API with proper status codes](#81-rest-api-with-proper-status-codes)  
8.2 [Global exception handling](#82-global-exception-handling)  
8.3 [Pagination & filtering](#83-pagination--filtering)  
8.4 [Custom middleware](#84-custom-middleware)

### [9. Dependency Injection](#9-dependency-injection)

9.1 [Scoped vs Singleton vs Transient](#91-scoped-vs-singleton-vs-transient)  
9.2 [Custom service registration](#92-custom-service-registration)

### [10. Entity Framework Core](#10-entity-framework-core)

10.1 [Optimized LINQ queries](#101-optimized-linq-queries)  
10.2 [N+1 problem](#102-n1-problem)  
10.3 [Eager vs Lazy loading](#103-eager-vs-lazy-loading)

### [11. SQL & Database](#11-sql--database)

11.1 [Find 2nd highest salary](#111-find-2nd-highest-salary)  
11.2 [Remove duplicate records](#112-remove-duplicate-records)  
11.3 [Pagination query](#113-pagination-query)

### [12. Microservices](#12-microservices)

12.1 [Retry with Polly](#121-retry-with-polly)  
12.2 [Circuit Breaker](#122-circuit-breaker)  
12.3 [Saga Pattern](#123-saga-pattern)

### [13. Advanced Topics](#13-advanced-topics)

13.1 [MediatR Pattern](#131-mediatr-pattern)  
13.2 [MediatR vs Traditional Handler Pattern](#132-mediatr-vs-traditional-handler-pattern)  
13.3 [CQRS Pattern](#133-cqrs-pattern)  
13.4 [Apache Kafka integration](#134-apache-kafka-integration)  
13.5 [Kafka Use Cases](#135-kafka-use-cases)  
13.6 [Kafka Consumer Project Types](#136-kafka-consumer-project-types)  
13.7 [Rate limiting](#137-rate-limiting)  
13.8 [Idempotent API](#138-idempotent-api)

### [14. SQL Advanced](#14-sql-advanced)

14.1 [Magic Tables (inserted/deleted)](#141-magic-tables-inserteddeleted)  
14.2 [Nested Stored Procedures with Temp Tables](#142-nested-stored-procedures-with-temp-tables)

### [15. Deployment & Build](#15-deployment--build)

15.1 [Production Build Commands](#151-production-build-commands)  
15.2 [Deployment Best Practices](#152-deployment-best-practices)

### [16. System Design](#16-system-design)

16.1 [URL Shortener](#161-url-shortener)  
16.2 [Order Management System](#162-order-management-system)  
16.3 [Payment Processing System](#163-payment-processing-system)

---

## 1. Collections & LINQ

### 1.1 Remove duplicates from a list

**Answer:**  
Use `Distinct()` for simple scenarios or `HashSet` for better performance with large datasets. Both preserve the order of first occurrence.

**Real-time Example:**

```csharp
// Method 1: Using LINQ Distinct()
List<string> emails = new List<string> { "a@test.com", "b@test.com", "a@test.com" };
var uniqueEmails = emails.Distinct().ToList();

// Method 2: Using HashSet (better performance, preserves order)
public List<int> RemoveDuplicatesWithHashSet(List<int> numbers) {
    HashSet<int> seen = new HashSet<int>();
    List<int> result = new List<int>();
    
    foreach (var num in numbers) {
        if (seen.Add(num)) { // Add returns false if already exists
            result.Add(num);
        }
    }
    return result;
}

// Method 3: Without LINQ or HashSet (Manual logic)
public List<int> RemoveDuplicatesManual(List<int> numbers) {
    List<int> result = new List<int>();
    
    foreach (var num in numbers) {
        bool isDuplicate = false;
        foreach (var existing in result) {
            if (existing == num) {
                isDuplicate = true;
                break;
            }
        }
        if (!isDuplicate) {
            result.Add(num);
        }
    }
    return result;
}
```

---

### 1.2 Find second highest number

**Answer:**  
Use LINQ `OrderByDescending().Skip(1).First()` for simplicity or a single-pass algorithm for O(n) performance. Handle edge cases like arrays with less than 2 unique elements.

**Real-time Example:**

```csharp
// Method 1: Using LINQ (Simple but O(n log n))
int[] salaries = { 50000, 80000, 60000, 80000, 90000 };
int secondHighest = salaries.Distinct()
                            .OrderByDescending(x => x)
                            .Skip(1)
                            .First(); // Returns 80000

// Method 2: Optimal O(n) - Single pass without LINQ
public int FindSecondHighestOptimal(int[] nums) {
    if (nums == null || nums.Length < 2)
        throw new ArgumentException("Array must have at least 2 elements");
    
    int highest = int.MinValue;
    int secondHighest = int.MinValue;
    
    foreach (var num in nums) {
        if (num > highest) {
            secondHighest = highest;
            highest = num;
        }
        else if (num > secondHighest && num != highest) {
            secondHighest = num;
        }
    }
    
    if (secondHighest == int.MinValue)
        throw new InvalidOperationException("No second highest found");
    
    return secondHighest;
}

// Example: [10, 5, 20, 20, 4, 1, 8] → Returns 10
```

---

### 1.3 Group objects using LINQ

**Answer:**  
Use `GroupBy()` to group collections by one or more properties. Combine with `Select()` for aggregations like count, sum, or average.

**Real-time Example:**

```csharp
// Method 1: Using LINQ GroupBy
var customerOrders = orders
    .GroupBy(o => o.CustomerId)
    .Select(g => new {
        CustomerId = g.Key,
        TotalOrders = g.Count(),
        TotalAmount = g.Sum(o => o.Amount)
    });

// Method 2: Without LINQ (Manual grouping logic)
public Dictionary<int, CustomerOrderSummary> GroupOrdersByCustomer(List<Order> orders) {
    var result = new Dictionary<int, CustomerOrderSummary>();
    
    foreach (var order in orders) {
        if (!result.ContainsKey(order.CustomerId)) {
            result[order.CustomerId] = new CustomerOrderSummary {
                CustomerId = order.CustomerId,
                TotalOrders = 0,
                TotalAmount = 0
            };
        }
        
        result[order.CustomerId].TotalOrders++;
        result[order.CustomerId].TotalAmount += order.Amount;
    }
    
    return result;
}

public class CustomerOrderSummary {
    public int CustomerId { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalAmount { get; set; }
}
```

---

### 1.4 Flatten nested collections

**Answer:**  
Use `SelectMany()` to flatten nested collections into a single collection. It projects each element to an `IEnumerable<T>` and flattens the results.

**Real-time Example:**

```csharp
// Method 1: Using LINQ SelectMany
var allItems = orders.SelectMany(o => o.Items).ToList();

// Method 2: Without LINQ (Manual flattening logic)
public List<OrderItem> FlattenOrderItems(List<Order> orders) {
    List<OrderItem> result = new List<OrderItem>();
    
    foreach (var order in orders) {
        foreach (var item in order.Items) {
            result.Add(item);
        }
    }
    
    return result;
}

// Method 3: Flatten with transformation using LINQ
var allProducts = orders
    .SelectMany(o => o.Items)
    .Select(item => item.ProductName)
    .Distinct()
    .ToList();

// Method 4: Manual flatten with transformation
public List<string> FlattenAndGetProductNames(List<Order> orders) {
    List<string> products = new List<string>();
    HashSet<string> seen = new HashSet<string>();
    
    foreach (var order in orders) {
        foreach (var item in order.Items) {
            if (seen.Add(item.ProductName)) {
                products.Add(item.ProductName);
            }
        }
    }
    
    return products;
}
```

---

### 1.5 Convert List to Dictionary

**Answer:**  
Use `ToDictionary()` specifying key and value selectors. Handle duplicates using `GroupBy()` first or `DistinctBy()` to take first/last occurrence.

**Real-time Example:**

```csharp
// Method 1: Using LINQ ToDictionary
var productDict = products.ToDictionary(p => p.Id, p => p.Name);

// Method 2: Without LINQ (Manual conversion with duplicate handling)
public Dictionary<int, Product> ConvertToDictionary(List<Product> products) {
    var dict = new Dictionary<int, Product>();
    
    foreach (var product in products) {
        if (!dict.ContainsKey(product.Id)) {
            dict.Add(product.Id, product);
        }
        // Or to take last: dict[product.Id] = product;
    }
    
    return dict;
}

// Method 3: Handle duplicates by taking last using LINQ
var dict = products
    .GroupBy(p => p.Id)
    .ToDictionary(g => g.Key, g => g.Last());

// Method 4: Manual grouping and taking last
public Dictionary<int, Product> ConvertWithDuplicates(List<Product> products) {
    var dict = new Dictionary<int, Product>();
    
    foreach (var product in products) {
        dict[product.Id] = product; // Overwrites if duplicate
    }
    
    return dict;
}
```

---

## 2. String Manipulation

### 2.1 Reverse a string

**Answer:**  
Convert string to char array, reverse it, and convert back. Or use two-pointer technique for in-place reversal with O(n) time complexity.

**Real-time Example:**

```csharp
// Reversing URL path for certain routing logic
public string ReverseString(string input) {
    char[] chars = input.ToCharArray();
    Array.Reverse(chars);
    return new string(chars);
}

// Two-pointer approach
public string ReverseTwoPointer(string s) {
    char[] arr = s.ToCharArray();
    int left = 0, right = arr.Length - 1;
    while (left < right) {
        (arr[left], arr[right]) = (arr[right], arr[left]);
        left++; right--;
    }
    return new string(arr);
}
```

---

### 2.2 Check palindrome

**Answer:**  
Use two-pointer technique comparing characters from both ends. For case-insensitive or alphanumeric-only checks, normalize the string first.

**Real-time Example:**

```csharp
// Validating palindrome for promo codes
public bool IsPalindrome(string s) {
    int left = 0, right = s.Length - 1;
    while (left < right) {
        if (s[left] != s[right]) return false;
        left++; right--;
    }
    return true;
}

// Alphanumeric only (like "A man, a plan, a canal: Panama")
public bool IsValidPalindrome(string s) {
    s = new string(s.Where(char.IsLetterOrDigit).ToArray()).ToLower();
    return IsPalindrome(s);
}
```

---

### 2.3 Find first non-repeating character

**Answer:**  
Use a Dictionary to count character frequencies in first pass, then iterate again to find the first character with count 1. Time complexity: O(n).

**Real-time Example:**

```csharp
// Finding first unique character in log messages
public char? FindFirstNonRepeating(string s) {
    var freq = new Dictionary<char, int>();
    foreach (char c in s)
        freq[c] = freq.GetValueOrDefault(c) + 1;
    
    foreach (char c in s)
        if (freq[c] == 1) return c;
    
    return null;
}

// Usage: "leetcode" returns 'l', "loveleetcode" returns 'v'
```

---

### 2.4 Count character frequency

**Answer:**  
Use Dictionary to store character counts. Iterate through string once, incrementing count for each character. O(n) time, O(k) space where k is unique characters.

**Real-time Example:**

```csharp
// Method 1: Using LINQ GroupBy
public Dictionary<char, int> CountFrequency(string s) {
    return s.GroupBy(c => c)
            .ToDictionary(g => g.Key, g => g.Count());
}

// Method 2: Without LINQ (Manual counting logic)
public Dictionary<char, int> CountFrequencyManual(string s) {
    var frequency = new Dictionary<char, int>();
    
    foreach (char c in s) {
        if (frequency.ContainsKey(c)) {
            frequency[c]++;
        }
        else {
            frequency[c] = 1;
        }
    }
    
    return frequency;
}

// Method 3: Check if two strings are anagrams without LINQ
public bool AreAnagrams(string s1, string s2) {
    if (s1.Length != s2.Length)
        return false;
    
    var freq1 = CountFrequencyManual(s1);
    var freq2 = CountFrequencyManual(s2);
    
    if (freq1.Count != freq2.Count)
        return false;
    
    foreach (var kvp in freq1) {
        if (!freq2.ContainsKey(kvp.Key) || freq2[kvp.Key] != kvp.Value)
            return false;
    }
    
    return true;
}
```

---

## 3. Date & Time

### 3.1 Calculate age from DOB

**Answer:**  
Subtract birth year from current year, then adjust if birthday hasn't occurred yet this year. Handle leap years and edge cases properly.

**Real-time Example:**

```csharp
// Calculating customer age for insurance premium
public int CalculateAge(DateTime birthDate) {
    var today = DateTime.Today;
    int age = today.Year - birthDate.Year;
    if (birthDate.Date > today.AddYears(-age)) age--;
    return age;
}

// Detailed age with months and days
public (int years, int months, int days) CalculateDetailedAge(DateTime dob) {
    var today = DateTime.Today;
    int years = today.Year - dob.Year;
    int months = today.Month - dob.Month;
    int days = today.Day - dob.Day;
    
    if (days < 0) { months--; days += DateTime.DaysInMonth(today.Year, today.Month); }
    if (months < 0) { years--; months += 12; }
    
    return (years, months, days);
}
```

---

### 3.2 Find overlapping date ranges

**Answer:**  
Two ranges overlap if start of one is before end of other AND vice versa. Formula: `range1.Start <= range2.End && range2.Start <= range1.End` .

**Real-time Example:**

```csharp
// Checking hotel room availability
public bool DoRangesOverlap(DateRange range1, DateRange range2) {
    return range1.StartDate <= range2.EndDate && 
           range2.StartDate <= range1.EndDate;
}

// Get overlapping period
public DateRange GetOverlap(DateRange r1, DateRange r2) {
    var start = r1.StartDate > r2.StartDate ? r1.StartDate : r2.StartDate;
    var end = r1.EndDate < r2.EndDate ? r1.EndDate : r2.EndDate;
    return start <= end ? new DateRange(start, end) : null;
}
```

---

### 3.3 Convert UTC to local time

**Answer:**  
Always store dates in UTC in database. Use `TimeZoneInfo.ConvertTimeFromUtc()` for conversion. Use `DateTimeOffset` for precision and to avoid ambiguity during DST transitions.

**Real-time Example:**

```csharp
// Converting server time to user's timezone for display
public DateTime ConvertUtcToTimeZone(DateTime utcTime, string timeZoneId) {
    TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    return TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
}

// Using DateTimeOffset (recommended)
public DateTimeOffset ConvertToUserTimeZone(DateTimeOffset utc, string tzId) {
    return TimeZoneInfo.ConvertTime(utc, TimeZoneInfo.FindSystemTimeZoneById(tzId));
}

// Common TimeZone IDs: "Eastern Standard Time", "India Standard Time", "UTC"
```

---

## 4. Async & Multithreading

### 4.1 Convert sync to async

**Answer:**  
Add `async` keyword to method, change return type to `Task<T>` , replace blocking calls with async versions using `await` , and add `Async` suffix to method name.

**Real-time Example:**

```csharp
// Before: Synchronous database call
public User GetUser(int id) {
    return _context.Users.Find(id);
}

// After: Asynchronous
public async Task<User> GetUserAsync(int id) {
    return await _context.Users.FindAsync(id);
}

// Multiple async operations in parallel
public async Task<Result> GetAggregatedDataAsync() {
    var userTask = _userService.GetUserAsync(userId);
    var ordersTask = _orderService.GetOrdersAsync(userId);
    var settingsTask = _settingsService.GetSettingsAsync(userId);
    
    await Task.WhenAll(userTask, ordersTask, settingsTask);
    
    return new Result {
        User = userTask.Result,
        Orders = ordersTask.Result,
        Settings = settingsTask.Result
    };
}
```

---

### 4.2 Task vs Thread

**Answer:**  
Task is high-level abstraction from TPL using thread pool, lightweight and composable. Thread is low-level OS thread with higher overhead but more control. Always prefer Task for async/await patterns.

**Real-time Example:**

```csharp
// Using Task (Recommended) - Thread pool managed
public async Task ProcessDataAsync() {
    await Task.Run(() => {
        // CPU-intensive work
    });
}

// Using Thread (Rare cases) - Dedicated OS thread
public void LongRunningOperation() {
    var thread = new Thread(() => {
        // Long-running background task
    });
    thread.IsBackground = true;
    thread.Start();
}

// Key differences:
// Task: Lightweight, returns values, supports cancellation, uses thread pool
// Thread: Heavy (~1MB stack), no return value support, manual management
```

---

### 4.3 What happens if you don't await?

**Answer:**  
Method continues executing without waiting, potential exceptions are unobserved, execution order becomes unpredictable, and you lose benefits of async/await like synchronization context.

**Real-time Example:**

```csharp
// ❌ BAD: Fire and forget - exceptions lost
public void ProcessOrder(int orderId) {
    ProcessOrderAsync(orderId); // Returns immediately, exceptions ignored
}

// ✅ GOOD: Proper await
public async Task ProcessOrder(int orderId) {
    await ProcessOrderAsync(orderId); // Waits for completion, catches exceptions
}

// Fire and forget with proper exception handling (rare cases)
public void SendNotification(string message) {
    Task.Run(async () => {
        try {
            await SendEmailAsync(message);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Email failed");
        }
    });
}
```

---

## 5. Thread Safety

### 5.1 Thread-safe Singleton

**Answer:**  
Use `Lazy<T>` for simple thread-safe lazy initialization. It's thread-safe by default, simple, and recommended for modern C#. Alternative: double-check locking or static constructor.

**Real-time Example:**

```csharp
// Recommended: Lazy<T> approach
public sealed class ConfigurationManager {
    private static readonly Lazy<ConfigurationManager> _instance =
        new Lazy<ConfigurationManager>(() => new ConfigurationManager());
    
    public static ConfigurationManager Instance => _instance.Value;
    
    private ConfigurationManager() {
        // Load configuration
    }
}

// With Dependency Injection (Modern approach)
public void ConfigureServices(IServiceCollection services) {
    services.AddSingleton<IConfigService, ConfigService>();
}
```

---

### 5.2 Lock vs SemaphoreSlim vs ConcurrentDictionary

**Answer:**  
`lock` for simple synchronization, `SemaphoreSlim` for limiting concurrent access with async support, `ConcurrentDictionary` for thread-safe dictionary operations without explicit locking.

**Real-time Example:**

```csharp
// 1. Lock - Simple synchronization
private readonly object _lock = new object();
public void UpdateCounter() {
    lock (_lock) {
        _counter++;
    }
}

// 2. SemaphoreSlim - Limit concurrent API calls
private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5); // Max 5 concurrent
public async Task ProcessAsync() {
    await _semaphore.WaitAsync();
    try {
        await CallExternalApiAsync();
    }
    finally {
        _semaphore.Release();
    }
}

// 3. ConcurrentDictionary - Thread-safe cache
private readonly ConcurrentDictionary<int, User> _cache = new();
public User GetOrAddUser(int id) {
    return _cache.GetOrAdd(id, key => _repository.GetUser(key));
}
```

---

## 6. Design Patterns

### 6.1 Factory Pattern

**Answer:**  
Factory pattern encapsulates object creation logic. Use it when object creation is complex or you want to decouple creation from usage. Common in creating database connections, loggers, or payment processors.

**Real-time Example:**

```csharp
// Payment processor factory
public interface IPaymentProcessor {
    Task<bool> ProcessPayment(decimal amount);
}

public class PaymentProcessorFactory {
    public IPaymentProcessor CreateProcessor(string type) {
        return type switch {
            "CreditCard" => new CreditCardProcessor(),
            "PayPal" => new PayPalProcessor(),
            "Stripe" => new StripeProcessor(),
            _ => throw new ArgumentException("Invalid payment type")
        };
    }
}

// Usage
var factory = new PaymentProcessorFactory();
var processor = factory.CreateProcessor("PayPal");
await processor.ProcessPayment(100.00m);
```

---

### 6.2 Strategy Pattern

**Answer:**  
Strategy pattern defines family of algorithms, encapsulates each one, and makes them interchangeable. Use for different behaviors that can be selected at runtime like compression, sorting, or validation strategies.

**Real-time Example:**

```csharp
// Shipping cost calculator with different strategies
public interface IShippingStrategy {
    decimal CalculateCost(Order order);
}

public class StandardShipping : IShippingStrategy {
    public decimal CalculateCost(Order order) => order.Weight * 5;
}

public class ExpressShipping : IShippingStrategy {
    public decimal CalculateCost(Order order) => order.Weight * 10;
}

public class OrderProcessor {
    private IShippingStrategy _shippingStrategy;
    
    public void SetStrategy(IShippingStrategy strategy) {
        _shippingStrategy = strategy;
    }
    
    public decimal GetShippingCost(Order order) {
        return _shippingStrategy.CalculateCost(order);
    }
}
```

---

### 6.3 SOLID Principles

**Answer:**  
**S**ingle Responsibility, **O**pen/Closed, **L**iskov Substitution, **I**nterface Segregation, **D**ependency Inversion. Each principle promotes maintainable, scalable, and testable code.

**Real-time Example:**

```csharp
// Single Responsibility - One class, one purpose
public class UserService {
    private readonly IUserRepository _repo;
    private readonly IEmailService _email;
    
    public async Task CreateUser(User user) {
        await _repo.SaveAsync(user);
        await _email.SendWelcomeAsync(user.Email);
    }
}

// Open/Closed - Extend without modifying
public interface IShape { double Area(); }
public class Circle : IShape { 
    public double Radius { get; set; }
    public double Area() => Math.PI * Radius * Radius;
}

// Dependency Inversion - Depend on abstractions
public class OrderService {
    private readonly INotificationSender _sender; // Interface, not concrete
    
    public OrderService(INotificationSender sender) {
        _sender = sender;
    }
}
```

---

### 6.4 Immutable Class

**Answer:**  
Immutable class has read-only properties set only through constructor. Once created, state cannot change. Benefits: thread-safe, safe for caching, predictable behavior. Use records in C# 9+ for concise syntax.

**Real-time Example:**

```csharp
// Traditional immutable class
public sealed class Address {
    public string Street { get; }
    public string City { get; }
    
    public Address(string street, string city) {
        Street = street;
        City = city;
    }
    
    // Return new instance for modifications
    public Address WithCity(string newCity) {
        return new Address(Street, newCity);
    }
}

// C# 9+ Record (immutable by default)
public record Customer(int Id, string Name, string Email);

// Usage - Thread-safe sharing
var config = new AppConfig("Production", "connectionString");
// config.Environment = "Test"; // Compile error - immutable
```

---

## 7. Data Structures & Algorithms

### 7.1 Find missing number

**Answer:**  
Given array of n-1 numbers from 1 to n, find missing number. Use sum formula: `n*(n+1)/2 - sum_of_array` or XOR approach for O(n) time, O(1) space.

**Real-time Example:**

```csharp
// Finding missing transaction ID in sequence
public int FindMissingNumber(int[] nums) {
    int n = nums.Length + 1;
    int expectedSum = n * (n + 1) / 2;
    int actualSum = nums.Sum();
    return expectedSum - actualSum;
}

// XOR approach (handles overflow)
public int FindMissingXOR(int[] nums) {
    int xor = 0;
    for (int i = 1; i <= nums.Length + 1; i++) xor ^= i;
    foreach (int num in nums) xor ^= num;
    return xor;
}
// Example: [1,2,4,5,6] missing 3
```

---

### 7.2 Two-Sum problem

**Answer:**  
Find two numbers in array that add up to target. Use HashMap to store complement (target - num) while iterating. O(n) time, O(n) space solution.

**Real-time Example:**

```csharp
// Method 1: Using Dictionary
public int[] TwoSum(int[] nums, int target) {
    var map = new Dictionary<int, int>();
    
    for (int i = 0; i < nums.Length; i++) {
        int complement = target - nums[i];
        if (map.ContainsKey(complement))
            return new[] { map[complement], i };
        map[nums[i]] = i;
    }
    
    return null;
}

// Method 2: Without Dictionary (Brute force O(n²))
public int[] TwoSumBruteForce(int[] nums, int target) {
    for (int i = 0; i < nums.Length; i++) {
        for (int j = i + 1; j < nums.Length; j++) {
            if (nums[i] + nums[j] == target) {
                return new[] { i, j };
            }
        }
    }
    
    return null;
}

// Example: nums=[2,7,11,15], target=9 → returns [0,1]
```

---

### 7.3 Valid parentheses

**Answer:**  
Check if string has valid bracket pairing using Stack. Push opening brackets, pop and match on closing brackets. Stack empty at end means valid.

**Real-time Example:**

```csharp
// Validating JSON structure or expression syntax
public bool IsValid(string s) {
    var stack = new Stack<char>();
    var pairs = new Dictionary<char, char> {
        {')', '('}, {']', '['}, {'}', '{'}
    };
    
    foreach (char c in s) {
        if (c == '(' || c == '[' || c == '{') {
            stack.Push(c);
        }
        else {
            if (stack.Count == 0 || stack.Pop() != pairs[c])
                return false;
        }
    }
    
    return stack.Count == 0;
}
// Example: "({[]})" → true, "({[})" → false
```

---

### 7.4 LRU Cache implementation

**Answer:**  
Least Recently Used Cache evicts least recently used item when full. Implement using Dictionary for O(1) lookup and LinkedList for O(1) reordering. Move accessed items to front.

**Real-time Example:**

```csharp
// API response caching with size limit
public class LRUCache<TKey, TValue> {
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<CacheItem>> _cache;
    private readonly LinkedList<CacheItem> _list;
    
    public LRUCache(int capacity) {
        _capacity = capacity;
        _cache = new Dictionary<TKey, LinkedListNode<CacheItem>>();
        _list = new LinkedList<CacheItem>();
    }
    
    public TValue Get(TKey key) {
        if (!_cache.TryGetValue(key, out var node))
            return default;
        
        _list.Remove(node);
        _list.AddFirst(node);
        return node.Value.Value;
    }
    
    public void Put(TKey key, TValue value) {
        if (_cache.TryGetValue(key, out var node)) {
            _list.Remove(node);
            _cache.Remove(key);
        }
        
        if (_cache.Count >= _capacity) {
            var last = _list.Last;
            _cache.Remove(last.Value.Key);
            _list.RemoveLast();
        }
        
        var newNode = new LinkedListNode<CacheItem>(new CacheItem(key, value));
        _list.AddFirst(newNode);
        _cache[key] = newNode;
    }
    
    private class CacheItem {
        public TKey Key { get; }
        public TValue Value { get; }
        public CacheItem(TKey k, TValue v) { Key = k; Value = v; }
    }
}
```

---

## 8. ASP. NET Core Web API

### 8.1 REST API with proper status codes

**Answer:**  
Return appropriate HTTP status codes: 200 (OK), 201 (Created), 204 (No Content), 400 (Bad Request), 404 (Not Found), 500 (Server Error). Use `ActionResult<T>` for flexible responses.

**Real-time Example:**

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase {
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id) {
        var product = await _service.GetByIdAsync(id);
        if (product == null)
            return NotFound(new { message = "Product not found" });
        
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var created = await _service.CreateAsync(product);
        return CreatedAtAction(nameof(GetProduct), 
            new { id = created.Id }, created);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id) {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
```

---

### 8.2 Global exception handling

**Answer:**  
Use middleware or exception filters to catch unhandled exceptions globally. Log errors, return consistent error responses, hide sensitive details in production.

**Real-time Example:**

```csharp
// Exception handling middleware
public class ExceptionMiddleware {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger) {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception ex) {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex switch {
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        
        return context.Response.WriteAsync(new ErrorResponse {
            StatusCode = context.Response.StatusCode,
            Message = ex.Message
        }.ToString());
    }
}

// Register in Program.cs
app.UseMiddleware<ExceptionMiddleware>();
```

---

### 8.3 Pagination & filtering

**Answer:**  
Use query parameters for pagination (page, pageSize) and filtering. Return metadata (totalCount, totalPages). Apply filters before pagination for efficiency.

**Real-time Example:**

```csharp
// Method 1: Using LINQ with EF Core
[HttpGet]
public async Task<ActionResult<PagedResult<Product>>> GetProducts(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string category = null,
    [FromQuery] decimal? minPrice = null) {
    
    var query = _context.Products.AsQueryable();
    
    // Apply filters
    if (!string.IsNullOrEmpty(category))
        query = query.Where(p => p.Category == category);
    if (minPrice.HasValue)
        query = query.Where(p => p.Price >= minPrice);
    
    // Get total count
    var totalItems = await query.CountAsync();
    
    // Apply pagination
    var items = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    
    return Ok(new PagedResult<Product> {
        Items = items,
        Page = page,
        PageSize = pageSize,
        TotalItems = totalItems,
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
    });
}

// Method 2: Without LINQ (In-memory pagination)
public PagedResult<Product> GetProductsPaginated(
    List<Product> allProducts, 
    int page, 
    int pageSize,
    string category = null) {
    
    // Filter manually
    List<Product> filtered = new List<Product>();
    foreach (var product in allProducts) {
        if (string.IsNullOrEmpty(category) || product.Category == category) {
            filtered.Add(product);
        }
    }
    
    // Calculate pagination
    int totalItems = filtered.Count;
    int skip = (page - 1) * pageSize;
    
    // Get page items
    List<Product> pageItems = new List<Product>();
    for (int i = skip; i < skip + pageSize && i < filtered.Count; i++) {
        pageItems.Add(filtered[i]);
    }
    
    return new PagedResult<Product> {
        Items = pageItems,
        Page = page,
        PageSize = pageSize,
        TotalItems = totalItems,
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
    };
}
```

---

### 8.4 Custom middleware

**Answer:**  
Middleware components handle HTTP requests/responses in pipeline. Implement `InvokeAsync` method, call `await _next(context)` to pass control, and register in `Program.cs` .

**Real-time Example:**

```csharp
// Request logging middleware
public class RequestLoggingMiddleware {
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    
    public RequestLoggingMiddleware(RequestDelegate next, 
        ILogger<RequestLoggingMiddleware> logger) {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context) {
        var stopwatch = Stopwatch.StartNew();
        
        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
        
        await _next(context);
        
        stopwatch.Stop();
        _logger.LogInformation(
            $"Response: {context.Response.StatusCode} " +
            $"in {stopwatch.ElapsedMilliseconds}ms");
    }
}

// Register in Program.cs
app.UseMiddleware<RequestLoggingMiddleware>();
```

---

## 9. Dependency Injection

### 9.1 Scoped vs Singleton vs Transient

**Answer:**  
**Transient**: New instance every time. **Scoped**: One instance per request. **Singleton**: Single instance for application lifetime. Choose based on state requirements and lifecycle needs.

**Real-time Example:**

```csharp
public void ConfigureServices(IServiceCollection services) {
    // Transient - New every time (stateless, lightweight services)
    services.AddTransient<IEmailService, EmailService>();
    
    // Scoped - One per HTTP request (DbContext, unit of work)
    services.AddScoped<IOrderService, OrderService>();
    services.AddDbContext<AppDbContext>(options => ...); // Scoped by default
    
    // Singleton - One for entire app (configurations, caches)
    services.AddSingleton<ICacheService, CacheService>();
    services.AddSingleton<IConfiguration>(configuration);
}

// Usage
public class OrderController : ControllerBase {
    private readonly IOrderService _orderService; // Scoped
    private readonly IEmailService _emailService; // Transient
    
    public OrderController(IOrderService orderService, IEmailService emailService) {
        _orderService = orderService;
        _emailService = emailService;
    }
}
```

---

### 9.2 Custom service registration

**Answer:**  
Create extension methods for clean, reusable service registration. Group related services together and use `IServiceCollection` extensions.

**Real-time Example:**

```csharp
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services) {
        
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IShippingService, ShippingService>();
        
        return services;
    }
    
    public static IServiceCollection AddRepositories(
        this IServiceCollection services) {
        
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        return services;
    }
}

// Usage in Program.cs
builder.Services
    .AddApplicationServices()
    .AddRepositories();
```

---

## 10. Entity Framework Core

### 10.1 Optimized LINQ queries

**Answer:**  
Filter at database level before loading to memory. Use `AsNoTracking()` for read-only queries. Avoid `ToList()` before filtering. Use projections with `Select()` to load only needed columns.

**Real-time Example:**

```csharp
// ❌ BAD: Loads all customers to memory first
public List<Customer> GetActiveCustomersBad() {
    var all = _context.Customers.ToList(); // Loads everything!
    return all.Where(c => c.IsActive).ToList();
}

// ✅ GOOD: Filters at database using LINQ
public async Task<List<Customer>> GetActiveCustomersGood() {
    return await _context.Customers
        .AsNoTracking() // Read-only, faster
        .Where(c => c.IsActive)
        .ToListAsync();
}

// ✅ BETTER: Project only needed columns using LINQ
public async Task<List<CustomerDto>> GetActiveCustomersOptimized() {
    return await _context.Customers
        .Where(c => c.IsActive)
        .Select(c => new CustomerDto {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email
        })
        .ToListAsync();
}

// Alternative: Manual filtering (if not using EF Core)
public List<Customer> GetActiveCustomersManual(List<Customer> allCustomers) {
    List<Customer> active = new List<Customer>();
    
    foreach (var customer in allCustomers) {
        if (customer.IsActive) {
            active.Add(customer);
        }
    }
    
    return active;
}
```

---

### 10.2 N+1 problem

**Answer:**  
N+1 occurs when loading related data in loop (1 query for parent + N queries for children). Fix with `Include()` for eager loading or projection with `Select()` .

**Real-time Example:**

```csharp
// ❌ BAD: N+1 Problem
public async Task<List<CustomerDto>> GetCustomersWithOrdersBad() {
    var customers = await _context.Customers.ToListAsync(); // Query 1
    
    foreach (var customer in customers) {
        customer.Orders = await _context.Orders
            .Where(o => o.CustomerId == customer.Id)
            .ToListAsync(); // N more queries!
    }
    return customers;
}

// ✅ GOOD: Eager loading with Include (LINQ)
public async Task<List<Customer>> GetCustomersWithOrdersGood() {
    return await _context.Customers
        .Include(c => c.Orders) // Single query with JOIN
        .ToListAsync();
}

// ✅ BETTER: Projection for specific data (LINQ)
public async Task<List<CustomerDto>> GetCustomersOptimized() {
    return await _context.Customers
        .Select(c => new CustomerDto {
            Id = c.Id,
            Name = c.Name,
            OrderCount = c.Orders.Count()
        })
        .ToListAsync();
}

// Alternative: Manual approach to avoid N+1 (without LINQ)
public List<CustomerWithOrders> GetCustomersWithOrdersManual() {
    // Load all customers
    List<Customer> customers = _context.Customers.ToList();
    
    // Load all orders in one query
    List<Order> allOrders = _context.Orders.ToList();
    
    // Group orders by customer manually
    var result = new List<CustomerWithOrders>();
    
    foreach (var customer in customers) {
        var customerOrders = new List<Order>();
        
        foreach (var order in allOrders) {
            if (order.CustomerId == customer.Id) {
                customerOrders.Add(order);
            }
        }
        
        result.Add(new CustomerWithOrders {
            Customer = customer,
            Orders = customerOrders
        });
    }
    
    return result; // Total: 2 queries instead of N+1
}
```

---

### 10.3 Eager vs Lazy loading

**Answer:**  
**Eager loading** ( `Include` ) loads related data immediately in single query. **Lazy loading** loads on-access but causes N+1. **Explicit loading** ( `Load()` ) loads on demand with control.

**Real-time Example:**

```csharp
// Eager loading - Load everything upfront
public async Task<Order> GetOrderWithDetails(int id) {
    return await _context.Orders
        .Include(o => o.Customer)
        .Include(o => o.OrderItems)
            .ThenInclude(i => i.Product)
        .FirstOrDefaultAsync(o => o.Id == id);
}

// Lazy loading (requires proxies) - Loads on access
public Order GetOrder(int id) {
    var order = _context.Orders.Find(id);
    var customerName = order.Customer.Name; // Triggers separate query
    return order;
}

// Explicit loading - Manual control
public async Task<Order> GetOrderExplicit(int id) {
    var order = await _context.Orders.FindAsync(id);
    
    await _context.Entry(order)
        .Collection(o => o.OrderItems)
        .LoadAsync();
    
    return order;
}
```

---

## 11. SQL & Database

### 11.1 Find 2nd highest salary

**Answer:**  
Use `DISTINCT` with `ORDER BY` and `OFFSET` / `FETCH` or subquery with `MAX()` . Handle case where no second salary exists.

**Real-time Example:**

```sql
-- Method 1: Using OFFSET FETCH (SQL Server, PostgreSQL)
SELECT DISTINCT Salary
FROM Employee
ORDER BY Salary DESC
OFFSET 1 ROW
FETCH NEXT 1 ROW ONLY;

-- Method 2: Using subquery
SELECT MAX(Salary) AS SecondHighestSalary
FROM Employee
WHERE Salary < (SELECT MAX(Salary) FROM Employee);

-- Method 3: Using ROW_NUMBER
SELECT Salary
FROM (
    SELECT Salary, ROW_NUMBER() OVER (ORDER BY Salary DESC) AS Rank
    FROM Employee
    GROUP BY Salary
) AS RankedSalaries
WHERE Rank = 2;
```

---

### 11.2 Remove duplicate records

**Answer:**  
Use CTE with `ROW_NUMBER()` to identify duplicates, then delete rows where RowNum > 1. Partition by columns that define uniqueness.

**Real-time Example:**

```sql
-- Remove duplicate emails, keep oldest record
WITH CTE AS (
    SELECT *,
        ROW_NUMBER() OVER (
            PARTITION BY Email 
            ORDER BY CreatedDate ASC
        ) AS RowNum
    FROM Users
)
DELETE FROM CTE WHERE RowNum > 1;

-- Or keep specific record based on ID
WITH CTE AS (
    SELECT *,
        ROW_NUMBER() OVER (
            PARTITION BY Email 
            ORDER BY Id DESC
        ) AS RowNum
    FROM Users
)
DELETE FROM CTE WHERE RowNum > 1;
```

---

### 11.3 Pagination query

**Answer:**  
Use `OFFSET` and `FETCH NEXT` for server-side pagination. Calculate offset as `(pageNumber - 1) * pageSize` . Always include `ORDER BY` for consistent results.

**Real-time Example:**

```sql
-- SQL Server / PostgreSQL
DECLARE @PageNumber INT = 2;
DECLARE @PageSize INT = 10;

SELECT ProductId, ProductName, Price
FROM Products
WHERE IsActive = 1
ORDER BY ProductName
OFFSET (@PageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;

-- With total count for UI
SELECT 
    ProductId, ProductName, Price,
    COUNT(*) OVER() AS TotalCount
FROM Products
WHERE IsActive = 1
ORDER BY ProductName
OFFSET (@PageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;

-- MySQL
SELECT * FROM Products
WHERE IsActive = 1
ORDER BY ProductName
LIMIT @PageSize OFFSET ((@PageNumber - 1) * @PageSize);
```

---

## 12. Microservices

### 12.1 Retry with Polly

**Answer:**  
Polly provides resilience patterns like retry, circuit breaker, timeout. Use exponential backoff for retries to avoid overwhelming failing services. Combine policies with `WrapAsync` .

**Real-time Example:**

```csharp
// Install: Install-Package Polly
// Install: Install-Package Microsoft.Extensions.Http.Polly

public class Startup {
    public void ConfigureServices(IServiceCollection services) {
        services.AddHttpClient("RetryClient")
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());
    }
    
    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() {
        return Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryCount, context) => {
                    Console.WriteLine($"Retry {retryCount} after {timespan.TotalSeconds}s");
                });
    }
}

// Usage
public class ApiService {
    private readonly IHttpClientFactory _httpClientFactory;
    
    public async Task<string> GetDataAsync() {
        var client = _httpClientFactory.CreateClient("RetryClient");
        var response = await client.GetAsync("https://api.example.com/data");
        return await response.Content.ReadAsStringAsync();
    }
}
```

---

### 12.2 Circuit Breaker

**Answer:**  
Circuit breaker prevents cascading failures by stopping calls to failing services. States: Closed (normal), Open (failing), Half-Open (testing). Configure failure threshold and duration.

**Real-time Example:**

```csharp
private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() {
    return Policy<HttpResponseMessage>
        .Handle<HttpRequestException>()
        .OrResult(r => !r.IsSuccessStatusCode)
        .CircuitBreakerAsync(
            handledEventsAllowedBeforeBreaking: 3,
            durationOfBreak: TimeSpan.FromSeconds(30),
            onBreak: (outcome, duration) => {
                Console.WriteLine($"Circuit opened for {duration.TotalSeconds}s");
            },
            onReset: () => {
                Console.WriteLine("Circuit closed, normal operation resumed");
            },
            onHalfOpen: () => {
                Console.WriteLine("Circuit half-open, testing service");
            });
}

// Combined with retry
services.AddHttpClient("ResilientClient")
    .AddPolicyHandler(GetRetryPolicy())
    .AddPolicyHandler(GetCircuitBreakerPolicy())
    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(10));
```

---

### 12.3 Saga Pattern

**Answer:**  
Saga manages distributed transactions across microservices using sequence of local transactions with compensating actions for rollback. Two types: Choreography (events) and Orchestration (coordinator).

**Real-time Example:**

```csharp
// Orchestration-based Saga for Order Processing
public class OrderSagaOrchestrator {
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;
    private readonly IInventoryService _inventoryService;
    private readonly IShippingService _shippingService;
    
    public async Task<OrderResult> ProcessOrderAsync(CreateOrderCommand cmd) {
        var context = new SagaContext { OrderId = Guid.NewGuid() };
        
        try {
            // Step 1: Create Order
            await _orderService.CreateOrderAsync(context);
            
            // Step 2: Reserve Inventory
            await _inventoryService.ReserveInventoryAsync(context);
            
            // Step 3: Process Payment
            await _paymentService.ProcessPaymentAsync(context);
            
            // Step 4: Arrange Shipping
            await _shippingService.ArrangeShippingAsync(context);
            
            // Confirm order
            await _orderService.ConfirmOrderAsync(context.OrderId);
            
            return OrderResult.Success(context.OrderId);
        }
        catch (Exception ex) {
            // Compensating transactions (rollback)
            await CompensateAsync(context);
            return OrderResult.Failure(ex.Message);
        }
    }
    
    private async Task CompensateAsync(SagaContext context) {
        // Rollback in reverse order
        await _shippingService.CancelShippingAsync(context.OrderId);
        await _paymentService.RefundPaymentAsync(context.OrderId);
        await _inventoryService.ReleaseInventoryAsync(context.OrderId);
        await _orderService.CancelOrderAsync(context.OrderId);
    }
}
```

---

## 13. Advanced Topics

### 13.1 MediatR Pattern

**Answer:**  
MediatR implements the **Mediator Pattern** for in-process messaging. It decouples requests from handlers, promotes single responsibility, and enables cross-cutting concerns (logging, validation, etc.) through pipeline behaviors.

**Real-time Example:**

```csharp
// Install: Install-Package MediatR
// Install: Install-Package MediatR.Extensions.Microsoft.DependencyInjection

// Request
public record CreateOrderCommand(int CustomerId, List<OrderItem> Items) 
    : IRequest<int>;

// Handler
public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int> {
    private readonly AppDbContext _context;
    private readonly ILogger<CreateOrderHandler> _logger;
    
    public CreateOrderHandler(AppDbContext context, ILogger<CreateOrderHandler> logger) {
        _context = context;
        _logger = logger;
    }
    
    public async Task<int> Handle(CreateOrderCommand request, 
        CancellationToken cancellationToken) {
        _logger.LogInformation("Creating order for customer {CustomerId}", request.CustomerId);
        
        var order = new Order {
            CustomerId = request.CustomerId,
            Items = request.Items,
            CreatedDate = DateTime.UtcNow
        };
        
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
        
        return order.Id;
    }
}

// Controller usage
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase {
    private readonly IMediator _mediator;
    
    public OrdersController(IMediator mediator) => _mediator = mediator;
    
    [HttpPost]
    public async Task<ActionResult<int>> CreateOrder(CreateOrderCommand command) {
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOrder), new { id = orderId }, orderId);
    }
}

// Pipeline Behavior for Logging
public class LoggingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {
    
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) {
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken) {
        
        _logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);
        var response = await next();
        _logger.LogInformation("Handled {RequestName}", typeof(TRequest).Name);
        
        return response;
    }
}

// Register in Program.cs
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
```

---

### 13.2 MediatR vs Traditional Handler Pattern

**Answer:**  
MediatR provides a standardized, loosely-coupled approach compared to traditional handler patterns. Here's the key difference:

**Comparison:**

| Aspect | Traditional Handler Pattern | MediatR Pattern |
|--------|---------------------------|-----------------|
| **Coupling** | Direct dependency on handler interface | Depends only on `IMediator` |
| **Registration** | Manual registration per handler | Auto-registration via assembly scan |
| **Pipeline** | Custom implementation needed | Built-in pipeline behaviors |
| **Testing** | Need to mock each handler | Mock single `IMediator` interface |
| **Cross-cutting concerns** | Decorator pattern or manual | Pipeline behaviors (logging, validation) |

**Real-time Example:**

```csharp
// ❌ TRADITIONAL HANDLER PATTERN
public interface IOrderHandler {
    Task<int> CreateOrder(CreateOrderRequest request);
}

public class OrderHandler : IOrderHandler {
    private readonly AppDbContext _context;
    
    public OrderHandler(AppDbContext context) {
        _context = context;
    }
    
    public async Task<int> CreateOrder(CreateOrderRequest request) {
        // Logic here
        return orderId;
    }
}

// Controller with traditional pattern
public class OrdersController : ControllerBase {
    private readonly IOrderHandler _orderHandler;
    private readonly IPaymentHandler _paymentHandler;
    private readonly IInventoryHandler _inventoryHandler;
    // ⚠️ Multiple handler dependencies
    
    public OrdersController(
        IOrderHandler orderHandler,
        IPaymentHandler paymentHandler,
        IInventoryHandler inventoryHandler) {
        _orderHandler = orderHandler;
        _paymentHandler = paymentHandler;
        _inventoryHandler = inventoryHandler;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateOrder(CreateOrderRequest request) {
        var orderId = await _orderHandler.CreateOrder(request);
        await _paymentHandler.ProcessPayment(orderId);
        await _inventoryHandler.ReserveStock(orderId);
        return Ok(orderId);
    }
}

// ✅ MEDIATR PATTERN
// Controller with MediatR
public class OrdersController : ControllerBase {
    private readonly IMediator _mediator;
    // ✅ Single dependency
    
    public OrdersController(IMediator mediator) {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateOrder(CreateOrderCommand command) {
        // MediatR routes to appropriate handler
        var orderId = await _mediator.Send(command);
        return Ok(orderId);
    }
}
```

**When to use MediatR:**
* ✅ Microservices/Clean Architecture
* ✅ CQRS implementation
* ✅ Need cross-cutting concerns (validation, logging)
* ✅ Complex applications with many handlers

**When to use Traditional Handlers:**
* ✅ Simple CRUD applications
* ✅ Small team unfamiliar with MediatR
* ✅ Performance-critical scenarios (MediatR adds minimal overhead)

---

### 13.3 CQRS Pattern

**Answer:**  
**CQRS (Command Query Responsibility Segregation)** separates read operations (Queries) from write operations (Commands). This allows independent scaling, optimization, and different data models for reads vs writes.

**Key Concepts:**
* **Command**: Changes state, returns void or ID (Write operation)
* **Query**: Returns data, never modifies state (Read operation)
* **Separate Models**: Read model optimized for queries, Write model for business logic
* **Eventual Consistency**: Read model may lag behind write model

**Real-time Example:**

```csharp
// ==================== WRITE SIDE (Commands) ====================

// Command - Modifies state
public record CreateOrderCommand(int CustomerId, List<OrderItem> Items) 
    : IRequest<int>;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int> {
    private readonly WriteDbContext _writeDb;
    private readonly IEventPublisher _eventPublisher;
    
    public async Task<int> Handle(CreateOrderCommand request, 
        CancellationToken cancellationToken) {
        
        // Write to normalized database
        var order = new Order {
            CustomerId = request.CustomerId,
            Items = request.Items,
            Status = OrderStatus.Pending,
            CreatedDate = DateTime.UtcNow
        };
        
        _writeDb.Orders.Add(order);
        await _writeDb.SaveChangesAsync(cancellationToken);
        
        // Publish event for read model update
        await _eventPublisher.PublishAsync(new OrderCreatedEvent {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            TotalAmount = order.TotalAmount
        });
        
        return order.Id;
    }
}

// ==================== READ SIDE (Queries) ====================

// Query - Returns data only
public record GetOrderQuery(int OrderId) : IRequest<OrderDto>;

public class GetOrderHandler : IRequestHandler<GetOrderQuery, OrderDto> {
    private readonly ReadDbContext _readDb; // Denormalized, optimized for reads
    
    public async Task<OrderDto> Handle(GetOrderQuery request, 
        CancellationToken cancellationToken) {
        
        // Read from denormalized view/table
        return await _readDb.OrderViews
            .Where(o => o.OrderId == request.OrderId)
            .Select(o => new OrderDto {
                OrderId = o.OrderId,
                CustomerName = o.CustomerName, // Already joined
                TotalAmount = o.TotalAmount,
                Items = o.Items, // Already aggregated
                Status = o.Status
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}

// Read Model Projector - Updates read database
public class OrderProjector {
    private readonly ReadDbContext _readDb;
    
    public async Task Handle(OrderCreatedEvent evt) {
        var orderView = new OrderView {
            OrderId = evt.OrderId,
            CustomerName = evt.CustomerName,
            TotalAmount = evt.TotalAmount,
            Status = "Pending",
            CreatedDate = DateTime.UtcNow
        };
        
        _readDb.OrderViews.Add(orderView);
        await _readDb.SaveChangesAsync();
    }
}

// ==================== Controller ====================

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase {
    private readonly IMediator _mediator;
    
    [HttpPost] // Command
    public async Task<ActionResult<int>> CreateOrder(CreateOrderCommand command) {
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOrder), new { id = orderId }, orderId);
    }
    
    [HttpGet("{id}")] // Query
    public async Task<ActionResult<OrderDto>> GetOrder(int id) {
        var order = await _mediator.Send(new GetOrderQuery(id));
        return Ok(order);
    }
}

// ==================== Database Context Setup ====================

// Write Database - Normalized
public class WriteDbContext : DbContext {
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
}

// Read Database - Denormalized
public class ReadDbContext : DbContext {
    public DbSet<OrderView> OrderViews { get; set; } // Flattened view
}

public class OrderView {
    public int OrderId { get; set; }
    public string CustomerName { get; set; } // Denormalized
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public List<OrderItemView> Items { get; set; } // Pre-joined
}
```

**Benefits:**
* ✅ **Scalability**: Scale reads and writes independently
* ✅ **Performance**: Optimize read models for specific queries
* ✅ **Flexibility**: Different databases for read/write (SQL + MongoDB)
* ✅ **Simplicity**: Simple queries without complex joins

**Trade-offs:**
* ⚠️ **Complexity**: More code, more infrastructure
* ⚠️ **Eventual Consistency**: Read model may be slightly behind
* ⚠️ **Data Synchronization**: Need to keep models in sync

---

### 13.4 Apache Kafka integration

**Answer:**  
**Apache Kafka** is a distributed event streaming platform for high-throughput, fault-tolerant messaging. It's used for building real-time data pipelines, event-driven architectures, and microservices communication. **Producer** sends messages to topics, **Consumer** reads and processes messages.

**NuGet Package:** `Confluent.Kafka`

**Key Concepts:**
* **Producer**: Publishes messages to Kafka topics
* **Consumer**: Subscribes to topics and processes messages
* **Topic**: Category/feed name to which records are published
* **Partition**: Topics are split into partitions for parallel processing
* **Consumer Group**: Multiple consumers working together to process a topic

**Real-time Example:**

```csharp
// ==================== INSTALLATION ====================
// Install-Package Confluent.Kafka
// dotnet add package Confluent.Kafka

// ==================== PRODUCER ====================
public interface IKafkaProducer {
    Task PublishAsync<T>(string topic, string key, T message);
}

public class KafkaProducer : IKafkaProducer {
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;
    
    public KafkaProducer(IConfiguration configuration, ILogger<KafkaProducer> logger) {
        var config = new ProducerConfig {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            ClientId = Environment.MachineName,
            Acks = Acks.All, // Wait for all replicas
            EnableIdempotence = true, // Exactly-once semantics
            MessageTimeoutMs = 10000
        };
        
        _producer = new ProducerBuilder<string, string>(config)
            .SetErrorHandler((_, error) => logger.LogError("Kafka error: {Error}", error.Reason))
            .Build();
        
        _logger = logger;
    }
    
    public async Task PublishAsync<T>(string topic, string key, T message) {
        try {
            var serializedMessage = JsonSerializer.Serialize(message);
            
            var result = await _producer.ProduceAsync(topic, 
                new Message<string, string> {
                    Key = key,
                    Value = serializedMessage,
                    Timestamp = Timestamp.Default
                });
            
            _logger.LogInformation(
                "Published message to {Topic}, Partition: {Partition}, Offset: {Offset}", 
                result.Topic, result.Partition.Value, result.Offset.Value);
        }
        catch (ProduceException<string, string> ex) {
            _logger.LogError(ex, "Failed to publish message to topic {Topic}", topic);
            throw;
        }
    }
    
    public void Dispose() => _producer?.Dispose();
}

// ==================== CONSUMER (Background Service) ====================
public class KafkaConsumerService : BackgroundService {
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _topic;
    
    public KafkaConsumerService(
        IConfiguration configuration,
        ILogger<KafkaConsumerService> logger,
        IServiceProvider serviceProvider) {
        
        var config = new ConsumerConfig {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = configuration["Kafka:ConsumerGroupId"],
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false, // Manual commit for reliability
            EnablePartitionEof = true
        };
        
        _consumer = new ConsumerBuilder<string, string>(config)
            .SetErrorHandler((_, error) => logger.LogError("Kafka error: {Error}", error.Reason))
            .Build();
        
        _logger = logger;
        _serviceProvider = serviceProvider;
        _topic = configuration["Kafka:Topic"];
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        _consumer.Subscribe(_topic);
        _logger.LogInformation("Subscribed to topic: {Topic}", _topic);
        
        try {
            while (!stoppingToken.IsCancellationRequested) {
                try {
                    var consumeResult = _consumer.Consume(stoppingToken);
                    
                    if (consumeResult.IsPartitionEOF) {
                        continue;
                    }
                    
                    _logger.LogInformation(
                        "Received message from {Topic}, Partition: {Partition}, Offset: {Offset}",
                        consumeResult.Topic, consumeResult.Partition.Value, consumeResult.Offset.Value);
                    
                    // Process message with scoped services
                    using (var scope = _serviceProvider.CreateScope()) {
                        await ProcessMessageAsync(consumeResult.Message.Value, scope);
                    }
                    
                    // Commit offset after successful processing
                    _consumer.Commit(consumeResult);
                }
                catch (ConsumeException ex) {
                    _logger.LogError(ex, "Error consuming message");
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "Error processing message");
                    // Optionally: Send to dead letter queue
                }
            }
        }
        finally {
            _consumer.Close();
            _consumer.Dispose();
        }
    }
    
    private async Task ProcessMessageAsync(string messageValue, IServiceScope scope) {
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
        
        var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(messageValue);
        await orderService.ProcessOrderAsync(orderEvent);
    }
}

// ==================== USAGE IN API ====================
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase {
    private readonly IKafkaProducer _kafkaProducer;
    private readonly ILogger<OrdersController> _logger;
    
    public OrdersController(IKafkaProducer kafkaProducer, ILogger<OrdersController> logger) {
        _kafkaProducer = kafkaProducer;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> CreateOrder(CreateOrderRequest request) {
        var order = new Order {
            CustomerId = request.CustomerId,
            Items = request.Items,
            CreatedDate = DateTime.UtcNow
        };
        
        // Save to database
        // ...
        
        // Publish event to Kafka
        var orderEvent = new OrderCreatedEvent {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            TotalAmount = order.TotalAmount,
            Items = order.Items
        };
        
        await _kafkaProducer.PublishAsync(
            topic: "order-events",
            key: order.Id.ToString(),
            message: orderEvent);
        
        _logger.LogInformation("Order {OrderId} created and published to Kafka", order.Id);
        
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order.Id);
    }
}

// ==================== REGISTRATION IN PROGRAM.CS ====================
// appsettings.json
/*
{
  "Kafka": {
    "BootstrapServers": "localhost:9092",
    "ConsumerGroupId": "order-service-group",
    "Topic": "order-events"
  }
}
*/

// Program.cs
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();
builder.Services.AddHostedService<KafkaConsumerService>();

// Domain services
builder.Services.AddScoped<IOrderService, OrderService>();
```

---

### 13.5 Kafka Use Cases

**Answer:**  
Apache Kafka is ideal for scenarios requiring high-throughput, fault-tolerant, scalable event streaming.

**Real-World Use Cases:**

| Use Case | Description | Example |
|----------|-------------|---------|
| **Event-Driven Microservices** | Decouple services via async events | Order service publishes `OrderCreated` , Payment service consumes |
| **Real-Time Analytics** | Stream data for real-time processing | User activity tracking, clickstream analysis |
| **Log Aggregation** | Centralized logging from multiple services | All microservices send logs to Kafka → Elasticsearch |
| **Change Data Capture (CDC)** | Capture database changes | Sync data between databases, update search indexes |
| **Message Queue Replacement** | Durable, scalable alternative to RabbitMQ | Job processing, task distribution |
| **Event Sourcing** | Store all state changes as events | Financial transactions, audit trails |
| **Stream Processing** | Real-time data transformation | Fraud detection, recommendation engines |
| **Notification System** | Send emails/SMS/push notifications | User activity triggers → Notification service consumes |

**Real-time Example:**

```csharp
// Use Case: Order Processing System with Kafka

// 1. Order Service (Producer)
public class OrderService {
    private readonly IKafkaProducer _kafka;
    
    public async Task<int> CreateOrderAsync(CreateOrderRequest request) {
        var order = await SaveOrderToDatabase(request);
        
        // Publish event
        await _kafka.PublishAsync("order-events", order.Id.ToString(), new {
            EventType = "OrderCreated",
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            TotalAmount = order.TotalAmount,
            Timestamp = DateTime.UtcNow
        });
        
        return order.Id;
    }
}

// 2. Payment Service (Consumer)
public class PaymentConsumerService : BackgroundService {
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        _consumer.Subscribe("order-events");
        
        while (!stoppingToken.IsCancellationRequested) {
            var message = _consumer.Consume(stoppingToken);
            var orderEvent = JsonSerializer.Deserialize<OrderEvent>(message.Value);
            
            if (orderEvent.EventType == "OrderCreated") {
                await _paymentService.ProcessPaymentAsync(orderEvent.OrderId);
            }
        }
    }
}

// 3. Inventory Service (Consumer)
public class InventoryConsumerService : BackgroundService {
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        _consumer.Subscribe("order-events");
        
        while (!stoppingToken.IsCancellationRequested) {
            var message = _consumer.Consume(stoppingToken);
            var orderEvent = JsonSerializer.Deserialize<OrderEvent>(message.Value);
            
            if (orderEvent.EventType == "OrderCreated") {
                await _inventoryService.ReserveStockAsync(orderEvent.OrderId);
            }
        }
    }
}

// 4. Notification Service (Consumer)
public class NotificationConsumerService : BackgroundService {
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        _consumer.Subscribe("order-events");
        
        while (!stoppingToken.IsCancellationRequested) {
            var message = _consumer.Consume(stoppingToken);
            var orderEvent = JsonSerializer.Deserialize<OrderEvent>(message.Value);
            
            if (orderEvent.EventType == "OrderCreated") {
                await _emailService.SendOrderConfirmationAsync(orderEvent.OrderId);
            }
        }
    }
}
```

**When to Use Kafka:**
* ✅ High throughput (millions of messages/sec)
* ✅ Need message replay capability
* ✅ Multiple consumers for same events
* ✅ Event-driven architecture
* ✅ Real-time stream processing

**When NOT to Use Kafka:**
* ❌ Simple request-response patterns (use REST/gRPC)
* ❌ Low-latency requirements (<5ms)
* ❌ Small-scale applications
* ❌ Complex routing logic (use RabbitMQ)

---

### 13.6 Kafka Consumer Project Types

**Answer:**  
Kafka consumers can be implemented in different . NET project types depending on your architecture and requirements.

**Project Types:**

| Project Type | Use Case | Pros | Cons |
|--------------|----------|------|------|
| **Worker Service** | Dedicated consumer app | Isolated, easy scaling | Separate deployment |
| **ASP. NET Core Web API** | Consumer + API in same app | Single deployment | Couples concerns |
| **Console Application** | Simple consumers, scripts | Lightweight, simple | Manual lifetime management |
| **Azure Functions** | Serverless event processing | Auto-scaling, serverless | Azure-specific |
| **Background Service (IHostedService)** | Part of existing app | Shared infrastructure | Same process as API |

**Real-time Examples:**

```csharp
// ==================== 1. WORKER SERVICE (RECOMMENDED) ====================
// Project Type: Worker Service (.NET 8)
// Create: dotnet new worker -n OrderConsumerWorker

// Program.cs
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<KafkaConsumerWorker>();
builder.Services.AddScoped<IOrderProcessor, OrderProcessor>();

var host = builder.Build();
host.Run();

// Worker.cs
public class KafkaConsumerWorker : BackgroundService {
    private readonly ILogger<KafkaConsumerWorker> _logger;
    private readonly IConsumer<string, string> _consumer;
    
    public KafkaConsumerWorker(ILogger<KafkaConsumerWorker> logger, IConfiguration config) {
        _logger = logger;
        var consumerConfig = new ConsumerConfig {
            BootstrapServers = config["Kafka:BootstrapServers"],
            GroupId = "order-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        _consumer.Subscribe("order-events");
        
        while (!stoppingToken.IsCancellationRequested) {
            var result = _consumer.Consume(stoppingToken);
            _logger.LogInformation("Processing: {Message}", result.Message.Value);
            // Process message
        }
    }
}

// ==================== 2. ASP.NET CORE WEB API ====================
// Project Type: ASP.NET Core Web API
// Use when: Consumer + API endpoints in same service

// Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHostedService<KafkaBackgroundConsumer>(); // Consumer
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();
app.MapControllers();
app.Run();

// KafkaBackgroundConsumer.cs
public class KafkaBackgroundConsumer : BackgroundService {
    private readonly IServiceProvider _serviceProvider;
    private readonly IConsumer<string, string> _consumer;
    
    public KafkaBackgroundConsumer(IServiceProvider serviceProvider, IConfiguration config) {
        _serviceProvider = serviceProvider;
        var consumerConfig = new ConsumerConfig {
            BootstrapServers = config["Kafka:BootstrapServers"],
            GroupId = "api-consumer-group"
        };
        _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        _consumer.Subscribe("order-events");
        
        while (!stoppingToken.IsCancellationRequested) {
            var result = _consumer.Consume(stoppingToken);
            
            // Use scoped services
            using (var scope = _serviceProvider.CreateScope()) {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                await orderService.ProcessAsync(result.Message.Value);
            }
        }
    }
}

// ==================== 3. CONSOLE APPLICATION ====================
// Project Type: Console App
// Use when: Simple consumer, one-off processing

// Program.cs
using Confluent.Kafka;

var config = new ConsumerConfig {
    BootstrapServers = "localhost:9092",
    GroupId = "console-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<string, string>(config).Build();
consumer.Subscribe("order-events");

Console.WriteLine("Kafka consumer started. Press Ctrl+C to exit.");

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => {
    e.Cancel = true;
    cts.Cancel();
};

try {
    while (!cts.Token.IsCancellationRequested) {
        var result = consumer.Consume(cts.Token);
        Console.WriteLine($"Received: {result.Message.Value}");
        
        // Process message
        ProcessOrder(result.Message.Value);
        
        consumer.Commit(result);
    }
}
catch (OperationCanceledException) {
    consumer.Close();
}

void ProcessOrder(string message) {
    // Business logic
    Console.WriteLine($"Processing order: {message}");
}

// ==================== 4. AZURE FUNCTION (Serverless) ====================
// NuGet: Microsoft.Azure.WebJobs.Extensions.Kafka

[FunctionName("KafkaOrderConsumer")]
public static async Task Run(
    [KafkaTrigger("localhost:9092", "order-events", ConsumerGroup = "azure-function-group")]
    string kafkaEvent,
    ILogger log) {
    
    log.LogInformation($"Kafka trigger function processed: {kafkaEvent}");
    
    var orderEvent = JsonSerializer.Deserialize<OrderEvent>(kafkaEvent);
    await ProcessOrderAsync(orderEvent);
}
```

**Recommendation:**
* **Production Microservices**: Use **Worker Service** (dedicated, scalable)
* **Monolithic App**: Use **ASP. NET Core with IHostedService**
* **Simple Scenarios**: Use **Console App**
* **Cloud/Serverless**: Use **Azure Functions**

---

### 13.7 Rate limiting

### 13.3 Rate limiting

**Answer:**  
Rate limiting controls API request rate per client. Prevents abuse and ensures fair usage. Implement using sliding window, token bucket, or fixed window algorithms.

**Real-time Example:**

```csharp
// Install: Install-Package AspNetCoreRateLimit

// Program.cs
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options => {
    options.GeneralRules = new List<RateLimitRule> {
        new RateLimitRule {
            Endpoint = "*",
            Period = "1m",
            Limit = 100
        },
        new RateLimitRule {
            Endpoint = "POST:/api/orders",
            Period = "1m",
            Limit = 10
        }
    };
});

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();

var app = builder.Build();
app.UseIpRateLimiting();

// Custom rate limiter using SemaphoreSlim
public class RateLimiter {
    private readonly SemaphoreSlim _semaphore;
    private readonly int _maxRequests;
    private readonly TimeSpan _timeWindow;
    private Queue<DateTime> _requestTimes = new();
    
    public RateLimiter(int maxRequests, TimeSpan timeWindow) {
        _maxRequests = maxRequests;
        _timeWindow = timeWindow;
        _semaphore = new SemaphoreSlim(1, 1);
    }
    
    public async Task<bool> IsAllowedAsync() {
        await _semaphore.WaitAsync();
        try {
            var now = DateTime.UtcNow;
            var windowStart = now - _timeWindow;
            
            // Remove old requests
            while (_requestTimes.Count > 0 && _requestTimes.Peek() < windowStart)
                _requestTimes.Dequeue();
            
            if (_requestTimes.Count < _maxRequests) {
                _requestTimes.Enqueue(now);
                return true;
            }
            
            return false;
        }
        finally {
            _semaphore.Release();
        }
    }
}
```

---

### 13.4 Idempotent API

**Answer:**  
Idempotent API produces same result when called multiple times with same input. Essential for retry scenarios. Use unique request IDs, check for duplicates before processing.

**Real-time Example:**

```csharp
// Idempotent payment processing
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase {
    private readonly IPaymentService _paymentService;
    private readonly IDistributedCache _cache;
    
    [HttpPost]
    public async Task<ActionResult<PaymentResult>> ProcessPayment(
        [FromBody] PaymentRequest request,
        [FromHeader(Name = "Idempotency-Key")] string idempotencyKey) {
        
        if (string.IsNullOrEmpty(idempotencyKey))
            return BadRequest("Idempotency-Key header is required");
        
        // Check if request already processed
        var cachedResult = await _cache.GetStringAsync(idempotencyKey);
        if (cachedResult != null) {
            return Ok(JsonSerializer.Deserialize<PaymentResult>(cachedResult));
        }
        
        // Process payment
        var result = await _paymentService.ProcessAsync(request);
        
        // Cache result for 24 hours
        await _cache.SetStringAsync(
            idempotencyKey,
            JsonSerializer.Serialize(result),
            new DistributedCacheEntryOptions {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
        
        return Ok(result);
    }
}

// Database-based idempotency
public class OrderService {
    public async Task<Order> CreateOrderAsync(CreateOrderRequest request, 
        string idempotencyKey) {
        
        // Check if order with this key already exists
        var existing = await _context.Orders
            .FirstOrDefaultAsync(o => o.IdempotencyKey == idempotencyKey);
        
        if (existing != null)
            return existing; // Return existing order
        
        // Create new order
        var order = new Order {
            IdempotencyKey = idempotencyKey,
            CustomerId = request.CustomerId,
            Total = request.Total
        };
        
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        
        return order;
    }
}
```

---

## 14. SQL Advanced

### 14.1 Magic Tables (inserted/deleted)

**Answer:**  
**Magic tables** ( `inserted` and `deleted` ) are special **temporary tables** automatically created by SQL Server during DML operations (INSERT, UPDATE, DELETE) within triggers. They store the **before** and **after** values of affected rows.

**Key Points:**
* **`inserted`**: Contains new values (used in INSERT and UPDATE triggers)
* **`deleted`**: Contains old values (used in DELETE and UPDATE triggers)
* **UPDATE trigger**: Has access to BOTH `inserted` (new values) and `deleted` (old values)
* **Scope**: Only accessible within the trigger context
* **Structure**: Same schema as the table that fired the trigger

**Real-time Example:**

```sql
-- ==================== CREATE TABLES ====================
CREATE TABLE Product (
    ProductId INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Price DECIMAL(10, 2),
    StockQuantity INT,
    LastModified DATETIME
);

CREATE TABLE ProductAudit (
    AuditId INT PRIMARY KEY IDENTITY(1,1),
    ProductId INT,
    OldPrice DECIMAL(10, 2),
    NewPrice DECIMAL(10, 2),
    OldStock INT,
    NewStock INT,
    ChangeType VARCHAR(20),
    ModifiedBy VARCHAR(100),
    ModifiedDate DATETIME
);

-- ==================== INSERT TRIGGER (using 'inserted') ====================
CREATE TRIGGER trg_Product_AfterInsert
ON Product
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 'inserted' table contains new rows
    INSERT INTO ProductAudit (ProductId, NewPrice, NewStock, ChangeType, ModifiedBy, ModifiedDate)
    SELECT 
        i.ProductId,
        i.Price AS NewPrice,
        i.StockQuantity AS NewStock,
        'INSERT' AS ChangeType,
        SYSTEM_USER AS ModifiedBy,
        GETDATE() AS ModifiedDate
    FROM inserted i;
    
    PRINT 'Audit record created for INSERT';
END;

-- Test INSERT trigger
INSERT INTO Product VALUES (1, 'Laptop', 999.99, 10, GETDATE());
SELECT * FROM ProductAudit; -- Shows audit record with NewPrice and NewStock

-- ==================== UPDATE TRIGGER (using both 'inserted' and 'deleted') ====================
CREATE TRIGGER trg_Product_AfterUpdate
ON Product
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 'deleted' table contains OLD values
    -- 'inserted' table contains NEW values
    INSERT INTO ProductAudit (
        ProductId, 
        OldPrice, NewPrice, 
        OldStock, NewStock, 
        ChangeType, ModifiedBy, ModifiedDate
    )
    SELECT 
        i.ProductId,
        d.Price AS OldPrice,        -- From 'deleted' (old value)
        i.Price AS NewPrice,         -- From 'inserted' (new value)
        d.StockQuantity AS OldStock, -- From 'deleted' (old value)
        i.StockQuantity AS NewStock, -- From 'inserted' (new value)
        'UPDATE' AS ChangeType,
        SYSTEM_USER AS ModifiedBy,
        GETDATE() AS ModifiedDate
    FROM inserted i
    INNER JOIN deleted d ON i.ProductId = d.ProductId
    WHERE i.Price <> d.Price OR i.StockQuantity <> d.StockQuantity;
END;

-- Test UPDATE trigger
UPDATE Product SET Price = 899.99, StockQuantity = 8 WHERE ProductId = 1;
SELECT * FROM ProductAudit; 
-- Shows: OldPrice = 999.99, NewPrice = 899.99, OldStock = 10, NewStock = 8

-- ==================== DELETE TRIGGER (using 'deleted') ====================
CREATE TRIGGER trg_Product_AfterDelete
ON Product
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 'deleted' table contains rows that were deleted
    INSERT INTO ProductAudit (ProductId, OldPrice, OldStock, ChangeType, ModifiedBy, ModifiedDate)
    SELECT 
        d.ProductId,
        d.Price AS OldPrice,
        d.StockQuantity AS OldStock,
        'DELETE' AS ChangeType,
        SYSTEM_USER AS ModifiedBy,
        GETDATE() AS ModifiedDate
    FROM deleted d;
END;

-- Test DELETE trigger
DELETE FROM Product WHERE ProductId = 1;
SELECT * FROM ProductAudit; -- Shows audit record with OldPrice and OldStock

-- ==================== INSTEAD OF TRIGGER ====================
-- Validate before allowing changes
CREATE TRIGGER trg_Product_InsteadOfUpdate
ON Product
INSTEAD OF UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if price increase is more than 20%
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        INNER JOIN deleted d ON i.ProductId = d.ProductId
        WHERE i.Price > d.Price * 1.2
    )
    BEGIN
        RAISERROR('Price increase cannot exceed 20%', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
    
    -- If valid, perform the update
    UPDATE p
    SET 
        p.ProductName = i.ProductName,
        p.Price = i.Price,
        p.StockQuantity = i.StockQuantity,
        p.LastModified = GETDATE()
    FROM Product p
    INNER JOIN inserted i ON p.ProductId = i.ProductId;
END;
```

**Magic Tables Summary:**

| Operation | `inserted` Contains | `deleted` Contains |
|-----------|-------------------|-------------------|
| **INSERT** | New rows | Empty |
| **UPDATE** | New (after) values | Old (before) values |
| **DELETE** | Empty | Deleted rows |

**Interview Tip:** Magic tables are crucial for audit trails, data validation, and maintaining history in enterprise applications.

---

### 14.2 Nested Stored Procedures with Temp Tables

**Answer:**  
**Nested stored procedures** are stored procedures that call other stored procedures. **Temp tables** ( `#TempTable` ) and **table variables** ( `@TableVariable` ) have different scopes:
* **Temp Tables (#)**: Visible to current session and all nested stored procedures
* **Table Variables (@)**: Local to the current stored procedure only (NOT visible to nested calls)

**Real-time Example:**

```sql
-- ==================== CREATE BASE TABLES ====================
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY,
    CustomerId INT,
    OrderDate DATETIME,
    TotalAmount DECIMAL(10, 2)
);

CREATE TABLE OrderItems (
    OrderItemId INT PRIMARY KEY,
    OrderId INT,
    ProductName VARCHAR(100),
    Quantity INT,
    Price DECIMAL(10, 2)
);

INSERT INTO Orders VALUES (1, 101, '2026-01-15', 500), (2, 102, '2026-01-20', 750);
INSERT INTO OrderItems VALUES 
    (1, 1, 'Laptop', 1, 500),
    (2, 2, 'Mouse', 2, 25),
    (3, 2, 'Keyboard', 1, 700);

-- ==================== SCENARIO: 3 NESTED STORED PROCEDURES ====================

-- ============ SP3: Innermost procedure (uses temp table from SP1) ============
CREATE PROCEDURE usp_CalculateOrderSummary
AS
BEGIN
    SET NOCOUNT ON;
    
    -- ✅ Can access #OrderSummary created in SP1
    -- ❌ Cannot access @CustomerOrders (table variable from SP1)
    
    SELECT 
        AVG(TotalAmount) AS AverageOrderValue,
        MAX(TotalAmount) AS MaxOrderValue,
        MIN(TotalAmount) AS MinOrderValue,
        COUNT(*) AS TotalOrders
    FROM #OrderSummary;  -- ✅ Temp table accessible here
    
    PRINT 'SP3: Calculated summary from temp table';
END;
GO

-- ============ SP2: Middle procedure (calls SP3) ============
CREATE PROCEDURE usp_ProcessOrderDetails
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- ✅ Can access #OrderSummary from SP1
    PRINT 'SP2: Processing order details';
    
    SELECT * FROM #OrderSummary 
    WHERE CustomerId = @CustomerId;
    
    -- Create local temp table visible to SP3
    CREATE TABLE #OrderItems (
        OrderId INT,
        ProductName VARCHAR(100),
        TotalValue DECIMAL(10, 2)
    );
    
    INSERT INTO #OrderItems
    SELECT 
        oi.OrderId,
        oi.ProductName,
        oi.Quantity * oi.Price AS TotalValue
    FROM OrderItems oi
    INNER JOIN #OrderSummary os ON oi.OrderId = os.OrderId
    WHERE os.CustomerId = @CustomerId;
    
    -- Call nested SP3
    EXEC usp_CalculateOrderSummary;
    
    -- Temp table created in SP2 is automatically dropped when SP2 ends
END;
GO

-- ============ SP1: Outermost procedure (calls SP2) ============
CREATE PROCEDURE usp_GetCustomerOrders
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    PRINT 'SP1: Starting customer order processing';
    
    -- ✅ TEMP TABLE: Visible to all nested SPs
    CREATE TABLE #OrderSummary (
        OrderId INT,
        CustomerId INT,
        OrderDate DATETIME,
        TotalAmount DECIMAL(10, 2)
    );
    
    -- ❌ TABLE VARIABLE: NOT visible to nested SPs
    DECLARE @CustomerOrders TABLE (
        OrderId INT,
        OrderDate DATETIME
    );
    
    -- Populate temp table
    INSERT INTO #OrderSummary
    SELECT OrderId, CustomerId, OrderDate, TotalAmount
    FROM Orders
    WHERE CustomerId = @CustomerId;
    
    -- Populate table variable
    INSERT INTO @CustomerOrders
    SELECT OrderId, OrderDate
    FROM Orders
    WHERE CustomerId = @CustomerId;
    
    PRINT 'SP1: Temp table and table variable populated';
    
    -- Call nested SP2 (which will call SP3)
    EXEC usp_ProcessOrderDetails @CustomerId;
    
    -- Display results
    SELECT * FROM #OrderSummary;
    SELECT * FROM @CustomerOrders; -- Only visible in SP1
    
    -- Temp table automatically dropped when session ends
END;
GO

-- ==================== EXECUTE THE NESTED CHAIN ====================
EXEC usp_GetCustomerOrders @CustomerId = 102;

-- OUTPUT:
-- SP1: Starting customer order processing
-- SP1: Temp table and table variable populated
-- SP2: Processing order details
-- SP3: Calculated summary from temp table

-- ==================== KEY DIFFERENCES ====================

-- TEMP TABLE (#TempTable)
CREATE TABLE #TempOrders (OrderId INT);  -- Created in tempdb
-- ✅ Visible to nested stored procedures
-- ✅ Can be indexed
-- ✅ Causes recompilation if schema changes
-- ⚠️ Persists until session ends or explicitly dropped

-- TABLE VARIABLE (@TableVariable)
DECLARE @TempOrders TABLE (OrderId INT);  -- In-memory
-- ❌ NOT visible to nested stored procedures
-- ✅ Automatically cleaned up
-- ✅ Less recompilation overhead
-- ⚠️ Cannot be indexed (except inline constraints)
-- ⚠️ No statistics, can cause poor query plans with large datasets
```

**Scope Accessibility Table:**

| Feature | Temp Table (#) | Table Variable (@) |
|---------|---------------|-------------------|
| **Visible to nested SPs** | ✅ Yes | ❌ No |
| **Lifetime** | Until dropped or session ends | Current batch/SP only |
| **Can be indexed** | ✅ Yes | ⚠️ Only inline indexes |
| **Statistics** | ✅ Yes | ❌ No |
| **Recompilation** | ⚠️ Can cause | ✅ Minimal |
| **Best for** | Large datasets, nested SPs | Small datasets (<100 rows) |

**Interview Tip:** Use **temp tables** when you need to share data across nested stored procedures or work with large datasets. Use **table variables** for small datasets within a single procedure.

---

## 15. Deployment & Build

### 15.1 Production Build Commands

**Answer:**  
The **`dotnet publish`** command creates a production-ready build by compiling the application and copying all dependencies to an output folder. It's optimized for deployment to servers.

**Key Commands:**

```bash
# ==================== BASIC PUBLISH ====================
dotnet publish -c Release

# ==================== PUBLISH WITH OUTPUT PATH ====================
dotnet publish -c Release -o ./publish

# ==================== SELF-CONTAINED DEPLOYMENT ====================
# Includes .NET runtime (no need to install .NET on server)
dotnet publish -c Release -r win-x64 --self-contained true -o ./publish

# For Linux
dotnet publish -c Release -r linux-x64 --self-contained true -o ./publish

# ==================== FRAMEWORK-DEPENDENT DEPLOYMENT ====================
# Requires .NET runtime installed on server (smaller package)
dotnet publish -c Release --self-contained false -o ./publish

# ==================== SINGLE FILE EXECUTABLE ====================
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true -o ./publish

# ==================== TRIMMED BUILD (Reduce size) ====================
dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishTrimmed=true -o ./publish

# ==================== READY TO RUN (Faster startup) ====================
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishReadyToRun=true -o ./publish
```

**Real-time Example with Full Project:**

```bash
# ==================== PROJECT STRUCTURE ====================
# MyWebApi/
#   ├── MyWebApi.csproj
#   ├── Program.cs
#   ├── Controllers/
#   ├── appsettings.json
#   └── appsettings.Production.json

# ==================== 1. CLEAN PREVIOUS BUILDS ====================
dotnet clean

# ==================== 2. RESTORE DEPENDENCIES ====================
dotnet restore

# ==================== 3. BUILD IN RELEASE MODE ====================
dotnet build -c Release

# ==================== 4. RUN TESTS (Optional but recommended) ====================
dotnet test -c Release --no-build

# ==================== 5. PUBLISH FOR PRODUCTION ====================
# For Windows Server (IIS)
dotnet publish -c Release -r win-x64 --self-contained false -o ./publish/windows

# For Linux Server (Docker/Ubuntu)
dotnet publish -c Release -r linux-x64 --self-contained false -o ./publish/linux

# For Azure App Service (Framework-dependent)
dotnet publish -c Release -o ./publish/azure

# ==================== 6. PUBLISH WITH ENVIRONMENT TRANSFORMATION ====================
dotnet publish -c Release -o ./publish /p:EnvironmentName=Production

# ==================== 7. DOCKER BUILD (Production) ====================
docker build -t mywebapi:latest -f Dockerfile .
docker run -d -p 8080:80 --name mywebapi-container mywebapi:latest
```

**Production-Ready Dockerfile:**

```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyWebApi.csproj", "./"]
RUN dotnet restore "MyWebApi.csproj"
COPY . .
RUN dotnet build "MyWebApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "MyWebApi.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "MyWebApi.dll"]
```

**Build Configuration in .csproj:**

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    
    <!-- Production optimizations -->
    <PublishTrimmed>true</PublishTrimmed>
    <PublishReadyToRun>true</PublishReadyToRun>
    <PublishSingleFile>false</PublishSingleFile>
    
    <!-- Remove debugging symbols in release -->
    <DebugType>None</DebugType>
    <DebugSymbols>false</DebugSymbols>
  </PropertyGroup>
</Project>
```

---

### 15.2 Deployment Best Practices

**Answer:**  
Production deployments require proper configuration management, security hardening, monitoring, and rollback strategies.

**Real-time Checklist:**

```csharp
// ==================== 1. ENVIRONMENT-SPECIFIC CONFIGURATION ====================
// appsettings.Production.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-sql;Database=ProdDb;User Id=sa;Password=${SQL_PASSWORD};"
  },
  "Kafka": {
    "BootstrapServers": "prod-kafka-1:9092,prod-kafka-2:9092",
    "ConsumerGroupId": "prod-consumer-group"
  },
  "Redis": {
    "Configuration": "prod-redis:6379,password=${REDIS_PASSWORD}"
  }
}

// ==================== 2. SECURE SECRETS MANAGEMENT ====================
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Use Azure Key Vault in production
if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri(builder.Configuration["KeyVault:Url"]),
        new DefaultAzureCredential());
}

// Use User Secrets in development
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// ==================== 3. HEALTH CHECKS ====================
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>()
    .AddRedis(builder.Configuration["Redis:Configuration"])
    .AddKafka(builder.Configuration["Kafka:BootstrapServers"]);

var app = builder.Build();

// Health check endpoints
app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready");
app.MapHealthChecks("/health/live");

// ==================== 4. PRODUCTION MIDDLEWARE CONFIGURATION ====================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Cache static files for 1 year in production
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=31536000");
    }
});

// ==================== 5. DATABASE MIGRATION ON STARTUP ====================
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    if (app.Environment.IsProduction())
    {
        // Only apply migrations if approved
        if (builder.Configuration.GetValue<bool>("ApplyMigrations"))
        {
            await dbContext.Database.MigrateAsync();
        }
    }
    else
    {
        await dbContext.Database.MigrateAsync();
    }
}
```

**Deployment Scripts:**

```powershell
# ==================== POWERSHELL: DEPLOY TO IIS ====================
# deploy-to-iis.ps1

param(
    [string]$SiteName = "MyWebApi",
    [string]$PublishPath = "./publish",
    [string]$IISPath = "C:\inetpub\wwwroot\MyWebApi"
)

Write-Host "Starting deployment to IIS..." -ForegroundColor Green

# Stop IIS site
Stop-WebSite -Name $SiteName

# Backup current version
$backupPath = "C:\Backups\$SiteName-$(Get-Date -Format 'yyyyMMdd-HHmmss')"
Copy-Item -Path $IISPath -Destination $backupPath -Recurse

# Deploy new version
Remove-Item -Path "$IISPath\*" -Recurse -Force
Copy-Item -Path "$PublishPath\*" -Destination $IISPath -Recurse

# Start IIS site
Start-WebSite -Name $SiteName

Write-Host "Deployment completed successfully!" -ForegroundColor Green
```

```bash
# ==================== BASH: DEPLOY TO LINUX ====================
#!/bin/bash
# deploy-to-linux.sh

APP_NAME="mywebapi"
PUBLISH_PATH="./publish"
DEPLOY_PATH="/var/www/$APP_NAME"
SERVICE_NAME="$APP_NAME.service"

echo "Starting deployment to Linux..."

# Stop service
sudo systemctl stop $SERVICE_NAME

# Backup current version
BACKUP_PATH="/var/backups/$APP_NAME-$(date +%Y%m%d-%H%M%S)"
sudo cp -r $DEPLOY_PATH $BACKUP_PATH

# Deploy new version
sudo rm -rf $DEPLOY_PATH/*
sudo cp -r $PUBLISH_PATH/* $DEPLOY_PATH/

# Set permissions
sudo chown -R www-data:www-data $DEPLOY_PATH
sudo chmod +x $DEPLOY_PATH/$APP_NAME

# Start service
sudo systemctl start $SERVICE_NAME
sudo systemctl status $SERVICE_NAME

echo "Deployment completed successfully!"
```

**Key Deployment Checklist:**
* ✅ Use `dotnet publish -c Release`
* ✅ Environment-specific `appsettings.{Environment}.json`
* ✅ Secure secrets with Azure Key Vault/AWS Secrets Manager
* ✅ Enable health checks
* ✅ Configure logging (Serilog, Application Insights)
* ✅ Set up monitoring and alerts
* ✅ Use Blue-Green or Canary deployment strategies
* ✅ Automated rollback plan
* ✅ Load testing before production
* ✅ Database migration strategy

---

## 16. System Design

### 16.1 URL Shortener

**Answer:**  
Design includes: unique short code generation (base62 encoding), URL storage with mapping, redirection service, analytics tracking. Consider collision handling, expiration, and scale with distributed caching.

**Real-time Example:**

```csharp
public class UrlShortenerService {
    private readonly AppDbContext _context;
    private readonly IDistributedCache _cache;
    private const string Alphabet = 
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    
    public async Task<string> ShortenUrl(string longUrl) {
        // Check if URL already shortened
        var existing = await _context.Urls
            .FirstOrDefaultAsync(u => u.LongUrl == longUrl);
        
        if (existing != null)
            return existing.ShortCode;
        
        // Generate unique short code
        var shortCode = GenerateShortCode();
        
        // Store mapping
        var urlMapping = new UrlMapping {
            ShortCode = shortCode,
            LongUrl = longUrl,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddYears(1)
        };
        
        _context.Urls.Add(urlMapping);
        await _context.SaveChangesAsync();
        
        // Cache for performance
        await _cache.SetStringAsync(
            shortCode, 
            longUrl, 
            new DistributedCacheEntryOptions {
                AbsoluteExpiration = urlMapping.ExpiresAt
            });
        
        return shortCode;
    }
    
    public async Task<string> GetLongUrl(string shortCode) {
        // Check cache first
        var cached = await _cache.GetStringAsync(shortCode);
        if (cached != null)
            return cached;
        
        // Query database
        var mapping = await _context.Urls
            .FirstOrDefaultAsync(u => u.ShortCode == shortCode);
        
        if (mapping == null || mapping.ExpiresAt < DateTime.UtcNow)
            return null;
        
        // Update analytics
        mapping.ClickCount++;
        await _context.SaveChangesAsync();
        
        return mapping.LongUrl;
    }
    
    private string GenerateShortCode() {
        var random = new Random();
        return new string(Enumerable.Range(0, 7)
            .Select(_ => Alphabet[random.Next(Alphabet.Length)])
            .ToArray());
    }
}

// Controller
[ApiController]
[Route("api/[controller]")]
public class UrlController : ControllerBase {
    [HttpPost("shorten")]
    public async Task<ActionResult<string>> Shorten([FromBody] string url) {
        var shortCode = await _service.ShortenUrl(url);
        return Ok($"https://short.ly/{shortCode}");
    }
    
    [HttpGet("{shortCode}")]
    public async Task<IActionResult> Redirect(string shortCode) {
        var longUrl = await _service.GetLongUrl(shortCode);
        if (longUrl == null)
            return NotFound();
        
        return Redirect(longUrl);
    }
}
```

---

### 16.2 Order Management System

**Answer:**  
Design includes: order creation, state machine for order status, inventory management, payment processing, notification system. Use events for communication between services, implement saga pattern for distributed transactions.

**Real-time Example:**

```csharp
// Order state machine
public enum OrderStatus {
    Pending, PaymentProcessing, PaymentConfirmed, 
    Shipped, Delivered, Cancelled
}

public class OrderStateMachine {
    public bool CanTransition(OrderStatus current, OrderStatus target) {
        return (current, target) switch {
            (OrderStatus.Pending, OrderStatus.PaymentProcessing) => true,
            (OrderStatus.PaymentProcessing, OrderStatus.PaymentConfirmed) => true,
            (OrderStatus.PaymentConfirmed, OrderStatus.Shipped) => true,
            (OrderStatus.Shipped, OrderStatus.Delivered) => true,
            (_, OrderStatus.Cancelled) => current != OrderStatus.Delivered,
            _ => false
        };
    }
}

public class OrderService {
    private readonly IEventBus _eventBus;
    private readonly OrderStateMachine _stateMachine;
    
    public async Task<Order> CreateOrderAsync(CreateOrderRequest request) {
        // Validate inventory
        foreach (var item in request.Items) {
            if (!await _inventoryService.IsAvailableAsync(item.ProductId, item.Quantity))
                throw new InsufficientInventoryException();
        }
        
        // Create order
        var order = new Order {
            CustomerId = request.CustomerId,
            Items = request.Items,
            Status = OrderStatus.Pending,
            Total = request.Items.Sum(i => i.Price * i.Quantity)
        };
        
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        
        // Publish event
        await _eventBus.PublishAsync(new OrderCreatedEvent {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            Total = order.Total
        });
        
        return order;
    }
    
    public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus) {
        var order = await _context.Orders.FindAsync(orderId);
        
        if (!_stateMachine.CanTransition(order.Status, newStatus))
            return false;
        
        order.Status = newStatus;
        order.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        
        // Publish status change event
        await _eventBus.PublishAsync(new OrderStatusChangedEvent {
            OrderId = orderId,
            OldStatus = order.Status,
            NewStatus = newStatus
        });
        
        return true;
    }
}
```

---

### 16.3 Payment Processing System

**Answer:**  
Design includes: multiple payment gateways (Strategy pattern), transaction logging, idempotency, retry mechanism, webhook handling for async updates, PCI compliance for card data, reconciliation for accounting.

**Real-time Example:**

```csharp
// Payment gateway abstraction
public interface IPaymentGateway {
    Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);
    Task<RefundResult> ProcessRefundAsync(string transactionId, decimal amount);
}

public class StripeGateway : IPaymentGateway {
    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request) {
        // Stripe API integration
        return new PaymentResult { Success = true, TransactionId = "stripe_123" };
    }
    
    public async Task<RefundResult> ProcessRefundAsync(string txnId, decimal amount) {
        // Refund logic
        return new RefundResult { Success = true };
    }
}

public class PaymentService {
    private readonly IPaymentGatewayFactory _gatewayFactory;
    private readonly AppDbContext _context;
    private readonly IDistributedCache _cache;
    
    public async Task<PaymentResult> ProcessPaymentAsync(
        PaymentRequest request, 
        string idempotencyKey) {
        
        // Check idempotency
        var cachedResult = await _cache.GetStringAsync(idempotencyKey);
        if (cachedResult != null)
            return JsonSerializer.Deserialize<PaymentResult>(cachedResult);
        
        // Log transaction attempt
        var transaction = new PaymentTransaction {
            OrderId = request.OrderId,
            Amount = request.Amount,
            Status = PaymentStatus.Pending,
            IdempotencyKey = idempotencyKey,
            AttemptedAt = DateTime.UtcNow
        };
        
        _context.PaymentTransactions.Add(transaction);
        await _context.SaveChangesAsync();
        
        try {
            // Select gateway
            var gateway = _gatewayFactory.CreateGateway(request.PaymentMethod);
            
            // Process with retry
            var result = await ProcessWithRetryAsync(gateway, request);
            
            // Update transaction
            transaction.Status = result.Success 
                ? PaymentStatus.Completed 
                : PaymentStatus.Failed;
            transaction.TransactionId = result.TransactionId;
            transaction.CompletedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            
            // Cache result
            await _cache.SetStringAsync(
                idempotencyKey,
                JsonSerializer.Serialize(result),
                new DistributedCacheEntryOptions {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
                });
            
            return result;
        }
        catch (Exception ex) {
            transaction.Status = PaymentStatus.Failed;
            transaction.ErrorMessage = ex.Message;
            await _context.SaveChangesAsync();
            throw;
        }
    }
    
    private async Task<PaymentResult> ProcessWithRetryAsync(
        IPaymentGateway gateway, 
        PaymentRequest request) {
        
        var policy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));
        
        return await policy.ExecuteAsync(() => gateway.ProcessPaymentAsync(request));
    }
}

// Webhook handling for async payment confirmation
[ApiController]
[Route("api/webhooks/payment")]
public class PaymentWebhookController : ControllerBase {
    [HttpPost("stripe")]
    public async Task<IActionResult> StripeWebhook([FromBody] StripeWebhookEvent evt) {
        // Verify webhook signature
        if (!VerifySignature(Request.Headers["Stripe-Signature"]))
            return Unauthorized();
        
        // Process event
        if (evt.Type == "payment_intent.succeeded") {
            await _paymentService.ConfirmPaymentAsync(evt.Data.TransactionId);
        }
        
        return Ok();
    }
}
```

---

## 🎯 Interview Preparation Checklist

### Must Know (Core Skills)

* ✅ LINQ operations (Distinct, GroupBy, SelectMany, Join)
* ✅ Async/await patterns and understanding
* ✅ Thread safety mechanisms
* ✅ SOLID principles with examples
* ✅ Common design patterns (Factory, Strategy, Singleton)
* ✅ Entity Framework optimization
* ✅ REST API best practices
* ✅ Dependency Injection lifetimes (Scoped, Singleton, Transient)

### Important (Mid-Level)

* ✅ N+1 problem and solutions
* ✅ Global exception handling
* ✅ Pagination and filtering
* ✅ Custom middleware
* ✅ SQL query optimization
* ✅ Retry and circuit breaker patterns (Polly)
* ✅ Rate limiting
* ✅ Idempotent APIs
* ✅ Magic tables in SQL (inserted/deleted)
* ✅ Stored procedures with temp tables

### Advanced (Senior Level)

* ✅ **MediatR Pattern** (in-process messaging)
* ✅ **MediatR vs Traditional Handler Pattern**
* ✅ **CQRS Pattern** (Command Query Responsibility Segregation)
* ✅ **Apache Kafka integration** (Producer/Consumer)
* ✅ **Kafka use cases** (Event-driven, real-time streaming)
* ✅ **Kafka consumer project types** (Worker Service, Background Service)
* ✅ Microservices patterns (Saga, Event Sourcing)
* ✅ Event-driven architecture
* ✅ System design problems
* ✅ Performance optimization
* ✅ Distributed caching
* ✅ **Production build commands** (dotnet publish)
* ✅ **Deployment best practices**
* ✅ Container orchestration basics

### SQL & Database

* ✅ Magic tables (inserted/deleted in triggers)
* ✅ Nested stored procedures with temp tables vs table variables
* ✅ 2nd highest salary query
* ✅ Remove duplicate records
* ✅ Pagination in SQL
* ✅ Query optimization techniques

---

## 💡 Interview Tips

1. **Start with simple solution, then optimize**
2. **Explain your thought process aloud**
3. **Consider edge cases and error handling**
4. **Discuss time/space complexity**
5. **Relate to real-world scenarios**
6. **Ask clarifying questions**
7. **Know when to use which pattern**
8. **Understand trade-offs of each approach**

---

## 📚 Quick Reference

### Time Complexities

* Array access: O(1)
* List. Add(): O(1) amortized
* Dictionary lookup: O(1) average
* List. Contains(): O(n)
* Sorting: O(n log n)
* HashSet operations: O(1) average

### Common Patterns

* **Repository**: Data access abstraction
* **Unit of Work**: Transaction management
* **Factory**: Object creation
* **Strategy**: Algorithm selection
* **Observer**: Event notification
* **Singleton**: Single instance
* **Decorator**: Add functionality

### HTTP Status Codes

* 200 OK, 201 Created, 204 No Content
* 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found
* 500 Internal Server Error, 503 Service Unavailable

---

## ❓ Frequently Asked Questions (Quick Answers)

### Q1: What NuGet package is used for Kafka?

**Answer:** `Confluent.Kafka`

```bash
dotnet add package Confluent.Kafka
```

### Q2: Which project type is used for Kafka consumer?

**Answer:** 
* **Worker Service** (recommended for dedicated consumers)
* **ASP. NET Core with IHostedService/BackgroundService** (when combined with APIs)
* **Console Application** (for simple scenarios)

```bash
# Create Worker Service
dotnet new worker -n KafkaConsumerWorker
```

### Q3: What is the MediatR pattern and how is it different from handler pattern?

**Answer:**
* **MediatR**: Implements mediator pattern for in-process messaging. Single dependency (`IMediator`) in controllers.
* **Traditional Handler**: Direct handler dependencies for each operation.

**Key Difference**: MediatR decouples controllers from handlers, supports pipeline behaviors (logging, validation), and enables cleaner architecture.

See [Section 13.1 (MediatR)](#131-mediatr-pattern) and [Section 13.2 (Comparison)](#132-mediatr-vs-traditional-handler-pattern)

### Q4: What is the CQRS pattern?

**Answer:** **Command Query Responsibility Segregation** - Separates read (Query) and write (Command) operations with different models. Enables independent scaling and optimization.

See [Section 13.3 (CQRS)](#133-cqrs-pattern)

### Q5: How to consume Kafka topic?

**Answer:**

```csharp
// Create Consumer with BackgroundService
public class KafkaConsumer : BackgroundService {
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        _consumer.Subscribe("topic-name");
        while (!stoppingToken.IsCancellationRequested) {
            var result = _consumer.Consume(stoppingToken);
            // Process message
        }
    }
}
```

See [Section 13.4 (Kafka Integration)](#134-apache-kafka-integration)

### Q6: What are Producer and Consumer in Kafka?

**Answer:**
* **Producer**: Publishes messages to Kafka topics
* **Consumer**: Subscribes to topics and processes messages
* Producers push data, Consumers pull data from topics

See [Section 13.4 (Kafka Integration)](#134-apache-kafka-integration)

### Q7: What types of Dependency Injection in . NET Core?

**Answer:**
* **Transient**: New instance every time (lightweight, stateless services)
* **Scoped**: One instance per HTTP request (DbContext, request-specific services)
* **Singleton**: Single instance for application lifetime (caching, logging)

See [Section 9.1 (DI Lifetimes)](#91-scoped-vs-singleton-vs-transient)

### Q8: What are use cases of Kafka?

**Answer:**
* Event-driven microservices communication
* Real-time analytics and stream processing
* Log aggregation
* Change Data Capture (CDC)
* Event sourcing
* Notification systems

See [Section 13.5 (Kafka Use Cases)](#135-kafka-use-cases)

### Q9: What are magic tables in SQL?

**Answer:** Special temporary tables in SQL Server triggers:
* **`inserted`**: Contains new/after values (INSERT, UPDATE)
* **`deleted`**: Contains old/before values (DELETE, UPDATE)

Used for audit trails and data validation in triggers.

See [Section 14.1 (Magic Tables)](#141-magic-tables-inserteddeleted)

### Q10: What is the production build command in . NET?

**Answer:**

```bash
# Basic production build
dotnet publish -c Release -o ./publish

# Self-contained (includes runtime)
dotnet publish -c Release -r win-x64 --self-contained true -o ./publish

# Framework-dependent (smaller size)
dotnet publish -c Release --self-contained false -o ./publish
```

See [Section 15.1 (Build Commands)](#151-production-build-commands)

### Q11: Nested stored procedures with temp tables - will temp table and variable be accessible in 3rd SP?

**Answer:**
* **Temp Table (#)**: ✅ YES - Accessible in all nested stored procedures
* **Table Variable (@)**: ❌ NO - Only accessible in the SP where it was declared

See [Section 14.2 (Nested Stored Procedures)](#142-nested-stored-procedures-with-temp-tables)

---

**Good luck with your interview! 🚀**

*Document Version: 2.0 | Last Updated: January 2026*
*Now includes: MediatR, CQRS, Kafka Deep Dive, SQL Magic Tables, Production Deployment*
