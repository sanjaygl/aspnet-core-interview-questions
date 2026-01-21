# C# / .NET Core – Must-Know Coding Questions (10+ Years Experience)

## Table of Contents

### 1. Collections & LINQ
- [1.1 Remove Duplicates from a List While Preserving Order](#11-remove-duplicates-from-a-list-while-preserving-order)
- [1.2 Find Second Highest Number from an Array](#12-find-second-highest-number-from-an-array)
- [1.3 Group Objects Using LINQ](#13-group-objects-using-linq)
- [1.4 Flatten Nested Collections](#14-flatten-nested-collections)
- [1.5 Convert List to Dictionary with Duplicate Keys Handling](#15-convert-list-to-dictionary-with-duplicate-keys-handling)

### 2. String Manipulation
- [2.1 Reverse a String Without Built-in Reverse()](#21-reverse-a-string-without-built-in-reverse)
- [2.2 Check Palindrome](#22-check-palindrome)
- [2.3 Find First Non-Repeating Character](#23-find-first-non-repeating-character)
- [2.4 Count Character Frequency](#24-count-character-frequency)

### 3. Date & Time
- [3.1 Calculate Age from Date of Birth](#31-calculate-age-from-date-of-birth)
- [3.2 Find Overlapping Date Ranges](#32-find-overlapping-date-ranges)
- [3.3 Convert UTC to Local Time Safely](#33-convert-utc-to-local-time-safely)

### 4. Multithreading & Async Programming
- [4.1 Convert Sync Code to Async](#41-convert-sync-code-to-async)
- [4.2 Difference Between Task vs Thread](#42-difference-between-task-vs-thread)
- [4.3 What Happens If You Don't Await?](#43-what-happens-if-you-dont-await)

### 5. Thread Safety
- [5.1 Singleton Thread-Safe Implementation](#51-singleton-thread-safe-implementation)
- [5.2 Use of Lock, SemaphoreSlim, ConcurrentDictionary](#52-use-of-lock-semaphoreslim-concurrentdictionary)

### 6. Object-Oriented Design
- [6.1 Implement Factory Pattern](#61-implement-factory-pattern)
- [6.2 Strategy Pattern Use Case](#62-strategy-pattern-use-case)
- [6.3 SOLID Principle Coding Examples](#63-solid-principle-coding-examples)
- [6.4 Create Immutable Class](#64-create-immutable-class)
- [6.5 Benefits of Immutability in Multithreaded Apps](#65-benefits-of-immutability-in-multithreaded-apps)

### 7. Data Structures & Algorithms
- [7.1 Find Missing Number](#71-find-missing-number)
- [7.2 Rotate Array](#72-rotate-array)
- [7.3 Merge Two Sorted Arrays](#73-merge-two-sorted-arrays)
- [7.4 Find Duplicate Elements](#74-find-duplicate-elements)
- [7.5 Two-Sum Problem](#75-two-sum-problem)
- [7.6 Valid Parentheses](#76-valid-parentheses)
- [7.7 Implement LRU Cache](#77-implement-lru-cache)

### 8. .NET Core / ASP.NET Core Coding
- [8.1 Create REST API with Proper HTTP Status Codes](#81-create-rest-api-with-proper-http-status-codes)
- [8.2 Implement Global Exception Handling](#82-implement-global-exception-handling)
- [8.3 Pagination & Filtering](#83-pagination--filtering)
- [8.4 Custom Middleware Example](#84-custom-middleware-example)
- [8.5 Logging / Exception Middleware](#85-logging--exception-middleware)

### 9. Dependency Injection
- [9.1 Scoped vs Singleton vs Transient](#91-scoped-vs-singleton-vs-transient)
- [9.2 Coding Custom Service Registration](#92-coding-custom-service-registration)

### 10. Database & SQL
- [10.1 Find 2nd Highest Salary](#101-find-2nd-highest-salary)
- [10.2 Remove Duplicate Records](#102-remove-duplicate-records)
- [10.3 Join Multiple Tables](#103-join-multiple-tables)
- [10.4 Write Pagination Query](#104-write-pagination-query)
- [10.5 Lazy vs Eager Loading (EF Core)](#105-lazy-vs-eager-loading-ef-core)
- [10.6 Writing Optimized LINQ Queries](#106-writing-optimized-linq-queries)
- [10.7 Handling N+1 Problem](#107-handling-n1-problem)

### 11. Microservices & Distributed Systems
- [11.1 Implement Retry with Polly](#111-implement-retry-with-polly)
- [11.2 Circuit Breaker Example](#112-circuit-breaker-example)
- [11.3 Saga Pattern Flow](#113-saga-pattern-flow)

### 12. Real-World Coding Scenarios
- [12.1 Design Order Processing System](#121-design-order-processing-system)
- [12.2 Handle Concurrent API Requests](#122-handle-concurrent-api-requests)
- [12.3 Idempotent API Implementation](#123-idempotent-api-implementation)
- [12.4 Rate-Limiting Logic](#124-rate-limiting-logic)
- [12.5 Refactoring Task - Improve Poorly Written Code](#125-refactoring-task---improve-poorly-written-code)
- [12.6 Add Caching](#126-add-caching)
- [12.7 Improve Performance](#127-improve-performance)

### 13. System Design
- [13.1 Design URL Shortener](#131-design-url-shortener)
- [13.2 Design Order Management System](#132-design-order-management-system)
- [13.3 Design Payment Processing System](#133-design-payment-processing-system)

### 14. Advanced Database & Stored Procedures
- [14.1 Nested Stored Procedures with Temp Tables and Variables](#141-nested-stored-procedures-with-temp-tables-and-variables)

### 15. Apache Kafka Integration in .NET Core
- [15.1 Kafka NuGet Package](#151-kafka-nuget-package)
- [15.2 Kafka Consumer Project Type](#152-kafka-consumer-project-type)
- [15.3 Kafka Use Cases](#153-kafka-use-cases)
- [15.4 Kafka Producer and Consumer](#154-kafka-producer-and-consumer)

### 16. MediatR and CQRS Patterns
- [16.1 MediatR Pattern](#161-mediatr-pattern)
- [16.2 CQRS Pattern](#162-cqrs-pattern)

### 17. Dependency Injection in .NET Core
- [17.1 Types of Dependency Injection with Real-Time Examples](#171-types-of-dependency-injection-with-real-time-examples)

---

## 1. Collections & LINQ

### 1.1 Remove Duplicates from a List While Preserving Order

**Description:**
Removing duplicates while maintaining the original order is a common requirement. The `Distinct()` method in LINQ preserves the order of first occurrence.

**Solution 1: Using LINQ Distinct()**
```csharp
public class RemoveDuplicatesExample
{
    public static List<int> RemoveDuplicates(List<int> numbers)
    {
        return numbers.Distinct().ToList();
    }
    
    // For custom objects
    public static List<Person> RemoveDuplicatePersons(List<Person> persons)
    {
        return persons.Distinct(new PersonComparer()).ToList();
    }
}

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class PersonComparer : IEqualityComparer<Person>
{
    public bool Equals(Person x, Person y)
    {
        return x.Id == y.Id;
    }
    
    public int GetHashCode(Person obj)
    {
        return obj.Id.GetHashCode();
    }
}
```

**Solution 2: Using HashSet (Better Performance)**
```csharp
public static List<int> RemoveDuplicatesUsingHashSet(List<int> numbers)
{
    var seen = new HashSet<int>();
    var result = new List<int>();
    
    foreach (var num in numbers)
    {
        if (seen.Add(num)) // Add returns false if already exists
        {
            result.Add(num);
        }
    }
    
    return result;
}
```

**Example Usage:**
```csharp
var numbers = new List<int> { 1, 2, 3, 2, 4, 1, 5 };
var unique = RemoveDuplicates(numbers);
// Output: [1, 2, 3, 4, 5]
```

**Performance Considerations:**
- `Distinct()`: O(n) time complexity, cleaner code
- `HashSet`: O(n) time complexity, slightly better performance for large datasets
- Both preserve order of first occurrence

---

### 1.2 Find Second Highest Number from an Array

**Description:**
Finding the second highest number is a classic interview question that tests array manipulation and edge case handling.

**Solution 1: Using LINQ (Simple)**
```csharp
public class SecondHighestExample
{
    public static int? FindSecondHighest(int[] numbers)
    {
        if (numbers == null || numbers.Length < 2)
            return null;
            
        return numbers.Distinct()
                     .OrderByDescending(x => x)
                     .Skip(1)
                     .FirstOrDefault();
    }
}
```

**Solution 2: Single Pass (Optimal - O(n))**
```csharp
public static int? FindSecondHighestOptimal(int[] numbers)
{
    if (numbers == null || numbers.Length < 2)
        return null;
        
    int first = int.MinValue;
    int second = int.MinValue;
    
    foreach (int num in numbers)
    {
        if (num > first)
        {
            second = first;
            first = num;
        }
        else if (num > second && num != first)
        {
            second = num;
        }
    }
    
    return second == int.MinValue ? null : second;
}
```

**Solution 3: Using HashSet to Handle Duplicates**
```csharp
public static int? FindSecondHighestWithDuplicates(int[] numbers)
{
    if (numbers == null || numbers.Length < 2)
        return null;
        
    var uniqueNumbers = new HashSet<int>(numbers);
    
    if (uniqueNumbers.Count < 2)
        return null;
        
    int first = int.MinValue;
    int second = int.MinValue;
    
    foreach (int num in uniqueNumbers)
    {
        if (num > first)
        {
            second = first;
            first = num;
        }
        else if (num > second)
        {
            second = num;
        }
    }
    
    return second;
}
```

**Example Usage:**
```csharp
int[] numbers = { 10, 5, 20, 20, 4, 1, 8 };
var secondHighest = FindSecondHighest(numbers);
Console.WriteLine(secondHighest); // Output: 10

int[] numbersWithDuplicates = { 20, 20, 20 };
var result = FindSecondHighestWithDuplicates(numbersWithDuplicates);
Console.WriteLine(result ?? "No second highest"); // Output: No second highest
```

**Edge Cases to Handle:**
- Array with less than 2 elements
- All elements are the same
- Array with duplicates
- Null array

---

### 1.3 Group Objects Using LINQ

**Description:**
Grouping is essential for data aggregation and reporting. LINQ's `GroupBy` provides powerful grouping capabilities.

**Example Scenario:**
```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
    public int Age { get; set; }
}

public class GroupingExamples
{
    // Example 1: Simple Grouping by Department
    public static void GroupByDepartment(List<Employee> employees)
    {
        var groupedByDept = employees.GroupBy(e => e.Department);
        
        foreach (var group in groupedByDept)
        {
            Console.WriteLine($"Department: {group.Key}");
            foreach (var emp in group)
            {
                Console.WriteLine($"  - {emp.Name} ({emp.Salary:C})");
            }
        }
    }
    
    // Example 2: Grouping with Aggregation
    public static Dictionary<string, decimal> GetAverageSalaryByDepartment(List<Employee> employees)
    {
        return employees.GroupBy(e => e.Department)
                       .ToDictionary(g => g.Key, g => g.Average(e => e.Salary));
    }
    
    // Example 3: Multiple Aggregations
    public static List<DepartmentStats> GetDepartmentStatistics(List<Employee> employees)
    {
        return employees.GroupBy(e => e.Department)
                       .Select(g => new DepartmentStats
                       {
                           Department = g.Key,
                           EmployeeCount = g.Count(),
                           AverageSalary = g.Average(e => e.Salary),
                           TotalSalary = g.Sum(e => e.Salary),
                           MinSalary = g.Min(e => e.Salary),
                           MaxSalary = g.Max(e => e.Salary)
                       })
                       .ToList();
    }
    
    // Example 4: Grouping by Multiple Keys
    public static void GroupByDepartmentAndAgeRange(List<Employee> employees)
    {
        var grouped = employees.GroupBy(e => new 
        { 
            e.Department, 
            AgeGroup = e.Age < 30 ? "Young" : e.Age < 50 ? "Middle" : "Senior" 
        });
        
        foreach (var group in grouped)
        {
            Console.WriteLine($"{group.Key.Department} - {group.Key.AgeGroup}: {group.Count()} employees");
        }
    }
    
    // Example 5: Nested Grouping
    public static void NestedGrouping(List<Employee> employees)
    {
        var nested = employees.GroupBy(e => e.Department)
                             .Select(deptGroup => new
                             {
                                 Department = deptGroup.Key,
                                 AgeGroups = deptGroup.GroupBy(e => e.Age / 10 * 10) // Group by decade
                             });
                             
        foreach (var dept in nested)
        {
            Console.WriteLine($"Department: {dept.Department}");
            foreach (var ageGroup in dept.AgeGroups)
            {
                Console.WriteLine($"  Age {ageGroup.Key}s: {ageGroup.Count()} employees");
            }
        }
    }
}

public class DepartmentStats
{
    public string Department { get; set; }
    public int EmployeeCount { get; set; }
    public decimal AverageSalary { get; set; }
    public decimal TotalSalary { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
}
```

**Example Usage:**
```csharp
var employees = new List<Employee>
{
    new Employee { Id = 1, Name = "John", Department = "IT", Salary = 80000, Age = 30 },
    new Employee { Id = 2, Name = "Jane", Department = "IT", Salary = 90000, Age = 35 },
    new Employee { Id = 3, Name = "Bob", Department = "HR", Salary = 60000, Age = 28 },
    new Employee { Id = 4, Name = "Alice", Department = "HR", Salary = 65000, Age = 32 }
};

var avgSalaries = GetAverageSalaryByDepartment(employees);
// Output: IT: 85000, HR: 62500
```

---

### 1.4 Flatten Nested Collections

**Description:**
Flattening nested collections is common when dealing with hierarchical data structures.

**Example Scenarios:**
```csharp
public class Department
{
    public string Name { get; set; }
    public List<Team> Teams { get; set; }
}

public class Team
{
    public string Name { get; set; }
    public List<Employee> Employees { get; set; }
}

public class FlattenExamples
{
    // Example 1: Simple Flattening with SelectMany
    public static List<Employee> GetAllEmployees(List<Department> departments)
    {
        return departments.SelectMany(d => d.Teams)
                         .SelectMany(t => t.Employees)
                         .ToList();
    }
    
    // Example 2: Flatten with Additional Data
    public static List<EmployeeInfo> GetEmployeesWithContext(List<Department> departments)
    {
        return departments.SelectMany(d => d.Teams, (dept, team) => new { dept, team })
                         .SelectMany(x => x.team.Employees, (x, emp) => new EmployeeInfo
                         {
                             EmployeeName = emp.Name,
                             TeamName = x.team.Name,
                             DepartmentName = x.dept.Name
                         })
                         .ToList();
    }
    
    // Example 3: Flatten Nested Arrays
    public static List<int> FlattenNestedArray(List<List<int>> nestedList)
    {
        return nestedList.SelectMany(x => x).ToList();
    }
    
    // Example 4: Flatten with Filtering
    public static List<Employee> GetITEmployeesOnly(List<Department> departments)
    {
        return departments.Where(d => d.Name == "IT")
                         .SelectMany(d => d.Teams)
                         .SelectMany(t => t.Employees)
                         .ToList();
    }
    
    // Example 5: Flatten Jagged Arrays
    public static List<string> FlattenJaggedArray(string[][] jaggedArray)
    {
        return jaggedArray.SelectMany(arr => arr).ToList();
    }
    
    // Example 6: Recursive Flattening (Tree Structure)
    public static List<Node> FlattenTree(Node root)
    {
        if (root == null) return new List<Node>();
        
        var result = new List<Node> { root };
        
        if (root.Children != null && root.Children.Any())
        {
            result.AddRange(root.Children.SelectMany(child => FlattenTree(child)));
        }
        
        return result;
    }
}

public class EmployeeInfo
{
    public string EmployeeName { get; set; }
    public string TeamName { get; set; }
    public string DepartmentName { get; set; }
}

public class Node
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Node> Children { get; set; }
}
```

**Example Usage:**
```csharp
var nestedNumbers = new List<List<int>>
{
    new List<int> { 1, 2, 3 },
    new List<int> { 4, 5 },
    new List<int> { 6, 7, 8, 9 }
};

var flattened = FlattenNestedArray(nestedNumbers);
// Output: [1, 2, 3, 4, 5, 6, 7, 8, 9]
```

---

### 1.5 Convert List to Dictionary with Duplicate Keys Handling

**Description:**
Converting lists to dictionaries is common, but handling duplicate keys requires careful consideration.

**Solutions:**
```csharp
public class ListToDictionaryExamples
{
    // Example 1: Simple Conversion (No Duplicates Expected)
    public static Dictionary<int, Employee> SimpleToDictionary(List<Employee> employees)
    {
        return employees.ToDictionary(e => e.Id, e => e);
    }
    
    // Example 2: Handle Duplicates - Take First
    public static Dictionary<int, Employee> TakeFirst(List<Employee> employees)
    {
        return employees.GroupBy(e => e.Id)
                       .ToDictionary(g => g.Key, g => g.First());
    }
    
    // Example 3: Handle Duplicates - Take Last
    public static Dictionary<int, Employee> TakeLast(List<Employee> employees)
    {
        return employees.GroupBy(e => e.Id)
                       .ToDictionary(g => g.Key, g => g.Last());
    }
    
    // Example 4: Handle Duplicates - Store as List
    public static Dictionary<int, List<Employee>> GroupDuplicates(List<Employee> employees)
    {
        return employees.GroupBy(e => e.Id)
                       .ToDictionary(g => g.Key, g => g.ToList());
    }
    
    // Example 5: Handle Duplicates - Aggregate
    public static Dictionary<string, decimal> AggregateSalaryByDepartment(List<Employee> employees)
    {
        return employees.GroupBy(e => e.Department)
                       .ToDictionary(g => g.Key, g => g.Sum(e => e.Salary));
    }
    
    // Example 6: Using Distinct Before Conversion
    public static Dictionary<int, Employee> UseDistinct(List<Employee> employees)
    {
        return employees.Distinct(new EmployeeIdComparer())
                       .ToDictionary(e => e.Id, e => e);
    }
    
    // Example 7: Try-Catch Pattern for Manual Handling
    public static Dictionary<int, Employee> ManualHandling(List<Employee> employees)
    {
        var dictionary = new Dictionary<int, Employee>();
        
        foreach (var emp in employees)
        {
            if (!dictionary.ContainsKey(emp.Id))
            {
                dictionary.Add(emp.Id, emp);
            }
            else
            {
                // Custom logic: e.g., keep higher salary
                if (emp.Salary > dictionary[emp.Id].Salary)
                {
                    dictionary[emp.Id] = emp;
                }
            }
        }
        
        return dictionary;
    }
    
    // Example 8: Using TryAdd (C# 8.0+)
    public static Dictionary<int, Employee> UseTryAdd(List<Employee> employees)
    {
        var dictionary = new Dictionary<int, Employee>();
        
        foreach (var emp in employees)
        {
            dictionary.TryAdd(emp.Id, emp); // Ignores if key exists
        }
        
        return dictionary;
    }
}

public class EmployeeIdComparer : IEqualityComparer<Employee>
{
    public bool Equals(Employee x, Employee y)
    {
        return x.Id == y.Id;
    }
    
    public int GetHashCode(Employee obj)
    {
        return obj.Id.GetHashCode();
    }
}
```

**Example Usage:**
```csharp
var employees = new List<Employee>
{
    new Employee { Id = 1, Name = "John", Department = "IT", Salary = 80000 },
    new Employee { Id = 2, Name = "Jane", Department = "IT", Salary = 90000 },
    new Employee { Id = 1, Name = "John Updated", Department = "IT", Salary = 85000 } // Duplicate
};

// Take first occurrence
var dictFirst = TakeFirst(employees);

// Take last occurrence
var dictLast = TakeLast(employees);

// Group duplicates
var grouped = GroupDuplicates(employees);
```

---

## 2. String Manipulation

### 2.1 Reverse a String Without Built-in Reverse()

**Description:**
Reversing a string is a fundamental operation. Implementing it without built-in methods demonstrates understanding of string manipulation and algorithms.

**Solutions:**
```csharp
public class StringReverseExamples
{
    // Method 1: Using Array Reverse
    public static string ReverseUsingArray(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
            
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    
    // Method 2: Using Loop (Two Pointers)
    public static string ReverseUsingLoop(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
            
        char[] charArray = input.ToCharArray();
        int left = 0;
        int right = charArray.Length - 1;
        
        while (left < right)
        {
            // Swap characters
            char temp = charArray[left];
            charArray[left] = charArray[right];
            charArray[right] = temp;
            
            left++;
            right--;
        }
        
        return new string(charArray);
    }
    
    // Method 3: Using StringBuilder
    public static string ReverseUsingStringBuilder(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
            
        StringBuilder sb = new StringBuilder();
        
        for (int i = input.Length - 1; i >= 0; i--)
        {
            sb.Append(input[i]);
        }
        
        return sb.ToString();
    }
    
    // Method 4: Using Recursion
    public static string ReverseUsingRecursion(string input)
    {
        if (string.IsNullOrEmpty(input) || input.Length == 1)
            return input;
            
        return ReverseUsingRecursion(input.Substring(1)) + input[0];
    }
    
    // Method 5: Using Stack
    public static string ReverseUsingStack(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
            
        Stack<char> stack = new Stack<char>();
        
        foreach (char c in input)
        {
            stack.Push(c);
        }
        
        StringBuilder sb = new StringBuilder();
        while (stack.Count > 0)
        {
            sb.Append(stack.Pop());
        }
        
        return sb.ToString();
    }
    
    // Method 6: Using XOR (In-place swap without temp variable)
    public static string ReverseUsingXOR(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
            
        char[] charArray = input.ToCharArray();
        int left = 0;
        int right = charArray.Length - 1;
        
        while (left < right)
        {
            charArray[left] ^= charArray[right];
            charArray[right] ^= charArray[left];
            charArray[left] ^= charArray[right];
            
            left++;
            right--;
        }
        
        return new string(charArray);
    }
}
```

**Example Usage:**
```csharp
string input = "Hello World";
string reversed = ReverseUsingLoop(input);
Console.WriteLine(reversed); // Output: "dlroW olleH"
```

**Performance Comparison:**
- **Array.Reverse**: O(n) - Most efficient
- **Two Pointers**: O(n) - Good performance, clear logic
- **StringBuilder**: O(n) - Good for concatenation-heavy operations
- **Recursion**: O(n) but uses stack space - Not recommended for large strings
- **Stack**: O(n) - Extra space, but demonstrates data structure knowledge

---

### 2.2 Check Palindrome

**Description:**
A palindrome reads the same forwards and backwards. This tests string comparison and algorithm efficiency.

**Solutions:**
```csharp
public class PalindromeExamples
{
    // Method 1: Using Two Pointers (Most Efficient)
    public static bool IsPalindrome(string input)
    {
        if (string.IsNullOrEmpty(input))
            return true;
            
        int left = 0;
        int right = input.Length - 1;
        
        while (left < right)
        {
            if (input[left] != input[right])
                return false;
                
            left++;
            right--;
        }
        
        return true;
    }
    
    // Method 2: Using Reverse Comparison
    public static bool IsPalindromeReverse(string input)
    {
        if (string.IsNullOrEmpty(input))
            return true;
            
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        string reversed = new string(charArray);
        
        return input.Equals(reversed, StringComparison.Ordinal);
    }
    
    // Method 3: Using LINQ
    public static bool IsPalindromeLinq(string input)
    {
        if (string.IsNullOrEmpty(input))
            return true;
            
        return input.SequenceEqual(input.Reverse());
    }
    
    // Method 4: Ignoring Spaces and Case
    public static bool IsPalindromeIgnoreCaseAndSpaces(string input)
    {
        if (string.IsNullOrEmpty(input))
            return true;
            
        // Remove spaces and convert to lowercase
        string cleaned = new string(input.Where(char.IsLetterOrDigit)
                                         .Select(char.ToLower)
                                         .ToArray());
        
        int left = 0;
        int right = cleaned.Length - 1;
        
        while (left < right)
        {
            if (cleaned[left] != cleaned[right])
                return false;
                
            left++;
            right--;
        }
        
        return true;
    }
    
    // Method 5: Alphanumeric Palindrome (LeetCode style)
    public static bool IsValidPalindrome(string input)
    {
        if (string.IsNullOrEmpty(input))
            return true;
            
        int left = 0;
        int right = input.Length - 1;
        
        while (left < right)
        {
            // Skip non-alphanumeric characters
            while (left < right && !char.IsLetterOrDigit(input[left]))
                left++;
                
            while (left < right && !char.IsLetterOrDigit(input[right]))
                right--;
            
            if (char.ToLower(input[left]) != char.ToLower(input[right]))
                return false;
                
            left++;
            right--;
        }
        
        return true;
    }
    
    // Method 6: Check if Number is Palindrome
    public static bool IsNumberPalindrome(int number)
    {
        // Negative numbers are not palindromes
        if (number < 0)
            return false;
            
        int original = number;
        int reversed = 0;
        
        while (number > 0)
        {
            int digit = number % 10;
            reversed = reversed * 10 + digit;
            number /= 10;
        }
        
        return original == reversed;
    }
}
```

**Example Usage:**
```csharp
Console.WriteLine(IsPalindrome("racecar"));        // True
Console.WriteLine(IsPalindrome("hello"));          // False
Console.WriteLine(IsPalindromeIgnoreCaseAndSpaces("A man a plan a canal Panama")); // True
Console.WriteLine(IsValidPalindrome("A man, a plan, a canal: Panama")); // True
Console.WriteLine(IsNumberPalindrome(12321));      // True
```

**Edge Cases:**
- Empty string (typically considered palindrome)
- Single character
- Even vs odd length
- Special characters and spaces
- Case sensitivity

---

### 2.3 Find First Non-Repeating Character

**Description:**
Finding the first non-repeating character tests hash map usage and efficient string traversal.

**Solutions:**
```csharp
public class FirstNonRepeatingCharExamples
{
    // Method 1: Using Dictionary (Two Pass)
    public static char? FindFirstNonRepeating(string input)
    {
        if (string.IsNullOrEmpty(input))
            return null;
            
        // First pass: Count frequencies
        var charCount = new Dictionary<char, int>();
        
        foreach (char c in input)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }
        
        // Second pass: Find first non-repeating
        foreach (char c in input)
        {
            if (charCount[c] == 1)
                return c;
        }
        
        return null;
    }
    
    // Method 2: Using LINQ GroupBy
    public static char? FindFirstNonRepeatingLinq(string input)
    {
        if (string.IsNullOrEmpty(input))
            return null;
            
        return input.GroupBy(c => c)
                   .Where(g => g.Count() == 1)
                   .Select(g => g.Key)
                   .FirstOrDefault();
    }
    
    // Method 3: Using Array for ASCII Characters (Fastest)
    public static char? FindFirstNonRepeatingArray(string input)
    {
        if (string.IsNullOrEmpty(input))
            return null;
            
        int[] charCount = new int[256]; // Assuming ASCII
        
        // Count frequencies
        foreach (char c in input)
        {
            charCount[c]++;
        }
        
        // Find first non-repeating
        foreach (char c in input)
        {
            if (charCount[c] == 1)
                return c;
        }
        
        return null;
    }
    
    // Method 4: Return Index of First Non-Repeating Character
    public static int FindFirstNonRepeatingIndex(string input)
    {
        if (string.IsNullOrEmpty(input))
            return -1;
            
        var charCount = new Dictionary<char, int>();
        
        foreach (char c in input)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }
        
        for (int i = 0; i < input.Length; i++)
        {
            if (charCount[input[i]] == 1)
                return i;
        }
        
        return -1;
    }
    
    // Method 5: Case Insensitive
    public static char? FindFirstNonRepeatingCaseInsensitive(string input)
    {
        if (string.IsNullOrEmpty(input))
            return null;
            
        string lower = input.ToLower();
        var charCount = new Dictionary<char, int>();
        
        foreach (char c in lower)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }
        
        for (int i = 0; i < lower.Length; i++)
        {
            if (charCount[lower[i]] == 1)
                return input[i]; // Return original case
        }
        
        return null;
    }
}
```

**Example Usage:**
```csharp
string input1 = "leetcode";
Console.WriteLine(FindFirstNonRepeating(input1)); // Output: 'l'

string input2 = "loveleetcode";
Console.WriteLine(FindFirstNonRepeating(input2)); // Output: 'v'

string input3 = "aabb";
Console.WriteLine(FindFirstNonRepeating(input3) ?? "None"); // Output: "None"
```

**Time Complexity:**
- All methods: O(n) where n is the length of the string
- Dictionary method: Uses O(k) space where k is the number of unique characters
- Array method: Uses O(1) space for ASCII (fixed 256 size)

---

### 2.4 Count Character Frequency

**Description:**
Counting character frequency is fundamental for many string algorithms and text processing tasks.

**Solutions:**
```csharp
public class CharacterFrequencyExamples
{
    // Method 1: Using Dictionary
    public static Dictionary<char, int> CountFrequency(string input)
    {
        var frequency = new Dictionary<char, int>();
        
        foreach (char c in input)
        {
            if (frequency.ContainsKey(c))
                frequency[c]++;
            else
                frequency[c] = 1;
        }
        
        return frequency;
    }
    
    // Method 2: Using LINQ GroupBy
    public static Dictionary<char, int> CountFrequencyLinq(string input)
    {
        return input.GroupBy(c => c)
                   .ToDictionary(g => g.Key, g => g.Count());
    }
    
    // Method 3: Using TryGetValue (Efficient)
    public static Dictionary<char, int> CountFrequencyEfficient(string input)
    {
        var frequency = new Dictionary<char, int>();
        
        foreach (char c in input)
        {
            if (frequency.TryGetValue(c, out int count))
                frequency[c] = count + 1;
            else
                frequency[c] = 1;
        }
        
        return frequency;
    }
    
    // Method 4: Using Array for ASCII
    public static int[] CountFrequencyArray(string input)
    {
        int[] frequency = new int[256];
        
        foreach (char c in input)
        {
            frequency[c]++;
        }
        
        return frequency;
    }
    
    // Method 5: Case Insensitive
    public static Dictionary<char, int> CountFrequencyCaseInsensitive(string input)
    {
        var frequency = new Dictionary<char, int>();
        
        foreach (char c in input.ToLower())
        {
            if (frequency.ContainsKey(c))
                frequency[c]++;
            else
                frequency[c] = 1;
        }
        
        return frequency;
    }
    
    // Method 6: Count Only Letters
    public static Dictionary<char, int> CountLettersOnly(string input)
    {
        var frequency = new Dictionary<char, int>();
        
        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                char lower = char.ToLower(c);
                if (frequency.ContainsKey(lower))
                    frequency[lower]++;
                else
                    frequency[lower] = 1;
            }
        }
        
        return frequency;
    }
    
    // Method 7: Get Most Frequent Character
    public static char GetMostFrequentChar(string input)
    {
        var frequency = CountFrequency(input);
        return frequency.OrderByDescending(kvp => kvp.Value).First().Key;
    }
    
    // Method 8: Get Characters Sorted by Frequency
    public static List<KeyValuePair<char, int>> GetSortedByFrequency(string input)
    {
        var frequency = CountFrequency(input);
        return frequency.OrderByDescending(kvp => kvp.Value)
                       .ThenBy(kvp => kvp.Key)
                       .ToList();
    }
    
    // Method 9: Display Frequency
    public static void DisplayFrequency(string input)
    {
        var frequency = CountFrequency(input);
        
        Console.WriteLine("Character Frequencies:");
        foreach (var kvp in frequency.OrderBy(x => x.Key))
        {
            Console.WriteLine($"'{kvp.Key}': {kvp.Value}");
        }
    }
}
```

**Example Usage:**
```csharp
string input = "programming";
var freq = CountFrequency(input);

foreach (var kvp in freq)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}

// Output:
// p: 1
// r: 2
// o: 1
// g: 2
// a: 1
// m: 2
// i: 1
// n: 1

// Get most frequent character
char mostFrequent = GetMostFrequentChar(input);
Console.WriteLine($"Most frequent: {mostFrequent}"); // Output: r (or g or m - first one found)
```

**Advanced Applications:**
```csharp
public class AdvancedFrequencyExamples
{
    // Check if two strings are anagrams
    public static bool AreAnagrams(string str1, string str2)
    {
        if (str1.Length != str2.Length)
            return false;
            
        var freq1 = CountFrequency(str1.ToLower());
        var freq2 = CountFrequency(str2.ToLower());
        
        return freq1.Count == freq2.Count && 
               freq1.All(kvp => freq2.TryGetValue(kvp.Key, out int value) && value == kvp.Value);
    }
    
    // Find characters that appear more than once
    public static List<char> FindDuplicateCharacters(string input)
    {
        var frequency = CountFrequency(input);
        return frequency.Where(kvp => kvp.Value > 1)
                       .Select(kvp => kvp.Key)
                       .ToList();
    }
}
```

---

## 3. Date & Time

### 3.1 Calculate Age from Date of Birth

**Description:**
Calculating age correctly requires handling leap years, month boundaries, and edge cases.

**Solutions:**
```csharp
public class AgeCalculationExamples
{
    // Method 1: Accurate Age Calculation
    public static int CalculateAge(DateTime dateOfBirth)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - dateOfBirth.Year;
        
        // Adjust if birthday hasn't occurred this year
        if (dateOfBirth.Date > today.AddYears(-age))
            age--;
            
        return age;
    }
    
    // Method 2: Alternative Method
    public static int CalculateAgeAlternative(DateTime dateOfBirth)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - dateOfBirth.Year;
        
        // Check if birthday has occurred this year
        if (today.Month < dateOfBirth.Month || 
            (today.Month == dateOfBirth.Month && today.Day < dateOfBirth.Day))
        {
            age--;
        }
        
        return age;
    }
    
    // Method 3: Detailed Age (Years, Months, Days)
    public static AgeDetail CalculateDetailedAge(DateTime dateOfBirth)
    {
        DateTime today = DateTime.Today;
        
        int years = today.Year - dateOfBirth.Year;
        int months = today.Month - dateOfBirth.Month;
        int days = today.Day - dateOfBirth.Day;
        
        // Adjust for negative days
        if (days < 0)
        {
            months--;
            days += DateTime.DaysInMonth(today.Year, today.Month == 1 ? 12 : today.Month - 1);
        }
        
        // Adjust for negative months
        if (months < 0)
        {
            years--;
            months += 12;
        }
        
        return new AgeDetail
        {
            Years = years,
            Months = months,
            Days = days
        };
    }
    
    // Method 4: Calculate Age at Specific Date
    public static int CalculateAgeAt(DateTime dateOfBirth, DateTime targetDate)
    {
        int age = targetDate.Year - dateOfBirth.Year;
        
        if (dateOfBirth.Date > targetDate.AddYears(-age))
            age--;
            
        return age;
    }
    
    // Method 5: Validate Age Range
    public static bool IsAgeInRange(DateTime dateOfBirth, int minAge, int maxAge)
    {
        int age = CalculateAge(dateOfBirth);
        return age >= minAge && age <= maxAge;
    }
    
    // Method 6: Calculate Age in Months
    public static int CalculateAgeInMonths(DateTime dateOfBirth)
    {
        DateTime today = DateTime.Today;
        int months = (today.Year - dateOfBirth.Year) * 12 + today.Month - dateOfBirth.Month;
        
        if (today.Day < dateOfBirth.Day)
            months--;
            
        return months;
    }
    
    // Method 7: Calculate Age in Days
    public static int CalculateAgeInDays(DateTime dateOfBirth)
    {
        return (DateTime.Today - dateOfBirth.Date).Days;
    }
    
    // Method 8: Get Next Birthday
    public static DateTime GetNextBirthday(DateTime dateOfBirth)
    {
        DateTime today = DateTime.Today;
        DateTime nextBirthday = new DateTime(today.Year, dateOfBirth.Month, dateOfBirth.Day);
        
        if (nextBirthday < today)
            nextBirthday = nextBirthday.AddYears(1);
            
        return nextBirthday;
    }
    
    // Method 9: Days Until Next Birthday
    public static int DaysUntilNextBirthday(DateTime dateOfBirth)
    {
        DateTime nextBirthday = GetNextBirthday(dateOfBirth);
        return (nextBirthday - DateTime.Today).Days;
    }
}

public class AgeDetail
{
    public int Years { get; set; }
    public int Months { get; set; }
    public int Days { get; set; }
    
    public override string ToString()
    {
        return $"{Years} years, {Months} months, {Days} days";
    }
}
```

**Example Usage:**
```csharp
DateTime dob = new DateTime(1990, 5, 15);

int age = CalculateAge(dob);
Console.WriteLine($"Age: {age} years");

var detailedAge = CalculateDetailedAge(dob);
Console.WriteLine($"Detailed Age: {detailedAge}");

int months = CalculateAgeInMonths(dob);
Console.WriteLine($"Age in months: {months}");

int daysUntilBirthday = DaysUntilNextBirthday(dob);
Console.WriteLine($"Days until next birthday: {daysUntilBirthday}");
```

**Edge Cases to Consider:**
- Leap year birthdays (Feb 29)
- Today is the birthday
- Future dates (should handle gracefully)
- Different time zones

---

### 3.2 Find Overlapping Date Ranges

**Description:**
Finding overlapping date ranges is crucial for booking systems, scheduling, and resource management.

**Solutions:**
```csharp
public class DateRange
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public DateRange(DateTime start, DateTime end)
    {
        if (start > end)
            throw new ArgumentException("Start date must be before end date");
            
        StartDate = start;
        EndDate = end;
    }
}

public class DateRangeOverlapExamples
{
    // Method 1: Check if Two Ranges Overlap
    public static bool DoRangesOverlap(DateRange range1, DateRange range2)
    {
        return range1.StartDate <= range2.EndDate && range2.StartDate <= range1.EndDate;
    }
    
    // Method 2: Alternative Overlap Check
    public static bool DoRangesOverlapAlternative(DateRange range1, DateRange range2)
    {
        return !(range1.EndDate < range2.StartDate || range2.EndDate < range1.StartDate);
    }
    
    // Method 3: Get Overlapping Period
    public static DateRange? GetOverlappingPeriod(DateRange range1, DateRange range2)
    {
        if (!DoRangesOverlap(range1, range2))
            return null;
            
        DateTime overlapStart = range1.StartDate > range2.StartDate ? range1.StartDate : range2.StartDate;
        DateTime overlapEnd = range1.EndDate < range2.EndDate ? range1.EndDate : range2.EndDate;
        
        return new DateRange(overlapStart, overlapEnd);
    }
    
    // Method 4: Find All Overlapping Ranges
    public static List<DateRange> FindAllOverlappingRanges(List<DateRange> ranges)
    {
        var overlaps = new List<DateRange>();
        
        for (int i = 0; i < ranges.Count; i++)
        {
            for (int j = i + 1; j < ranges.Count; j++)
            {
                if (DoRangesOverlap(ranges[i], ranges[j]))
                {
                    var overlap = GetOverlappingPeriod(ranges[i], ranges[j]);
                    if (overlap != null)
                        overlaps.Add(overlap);
                }
            }
        }
        
        return overlaps;
    }
    
    // Method 5: Merge Overlapping Ranges
    public static List<DateRange> MergeOverlappingRanges(List<DateRange> ranges)
    {
        if (ranges == null || ranges.Count == 0)
            return new List<DateRange>();
            
        // Sort by start date
        var sorted = ranges.OrderBy(r => r.StartDate).ToList();
        var merged = new List<DateRange>();
        
        DateRange current = sorted[0];
        
        for (int i = 1; i < sorted.Count; i++)
        {
            if (DoRangesOverlap(current, sorted[i]))
            {
                // Merge ranges
                current = new DateRange(
                    current.StartDate,
                    current.EndDate > sorted[i].EndDate ? current.EndDate : sorted[i].EndDate
                );
            }
            else
            {
                merged.Add(current);
                current = sorted[i];
            }
        }
        
        merged.Add(current);
        return merged;
    }
    
    // Method 6: Check if Date Falls Within Range
    public static bool IsDateInRange(DateTime date, DateRange range)
    {
        return date >= range.StartDate && date <= range.EndDate;
    }
    
    // Method 7: Get Overlap Duration in Days
    public static int GetOverlapDurationInDays(DateRange range1, DateRange range2)
    {
        var overlap = GetOverlappingPeriod(range1, range2);
        
        if (overlap == null)
            return 0;
            
        return (overlap.EndDate - overlap.StartDate).Days + 1;
    }
    
    // Method 8: Find Gaps Between Ranges
    public static List<DateRange> FindGapsBetweenRanges(List<DateRange> ranges)
    {
        if (ranges == null || ranges.Count < 2)
            return new List<DateRange>();
            
        var sorted = ranges.OrderBy(r => r.StartDate).ToList();
        var gaps = new List<DateRange>();
        
        for (int i = 0; i < sorted.Count - 1; i++)
        {
            if (sorted[i].EndDate < sorted[i + 1].StartDate.AddDays(-1))
            {
                gaps.Add(new DateRange(
                    sorted[i].EndDate.AddDays(1),
                    sorted[i + 1].StartDate.AddDays(-1)
                ));
            }
        }
        
        return gaps;
    }
    
    // Method 9: Check if One Range Contains Another
    public static bool DoesRangeContain(DateRange outer, DateRange inner)
    {
        return outer.StartDate <= inner.StartDate && outer.EndDate >= inner.EndDate;
    }
}

// Practical Example: Hotel Booking System
public class HotelBooking
{
    public int RoomId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public string GuestName { get; set; }
}

public class HotelBookingSystem
{
    private List<HotelBooking> bookings = new List<HotelBooking>();
    
    public bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
    {
        var requestedRange = new DateRange(checkIn, checkOut);
        
        var roomBookings = bookings.Where(b => b.RoomId == roomId);
        
        foreach (var booking in roomBookings)
        {
            var bookingRange = new DateRange(booking.CheckIn, booking.CheckOut);
            
            if (DateRangeOverlapExamples.DoRangesOverlap(requestedRange, bookingRange))
                return false;
        }
        
        return true;
    }
    
    public List<HotelBooking> GetConflictingBookings(int roomId, DateTime checkIn, DateTime checkOut)
    {
        var requestedRange = new DateRange(checkIn, checkOut);
        var conflicts = new List<HotelBooking>();
        
        foreach (var booking in bookings.Where(b => b.RoomId == roomId))
        {
            var bookingRange = new DateRange(booking.CheckIn, booking.CheckOut);
            
            if (DateRangeOverlapExamples.DoRangesOverlap(requestedRange, bookingRange))
                conflicts.Add(booking);
        }
        
        return conflicts;
    }
}
```

**Example Usage:**
```csharp
var range1 = new DateRange(new DateTime(2026, 1, 1), new DateTime(2026, 1, 10));
var range2 = new DateRange(new DateTime(2026, 1, 5), new DateTime(2026, 1, 15));

bool overlaps = DoRangesOverlap(range1, range2);
Console.WriteLine($"Ranges overlap: {overlaps}"); // True

var overlapPeriod = GetOverlappingPeriod(range1, range2);
Console.WriteLine($"Overlap: {overlapPeriod.StartDate:d} to {overlapPeriod.EndDate:d}");
// Output: 1/5/2026 to 1/10/2026

int overlapDays = GetOverlapDurationInDays(range1, range2);
Console.WriteLine($"Overlap duration: {overlapDays} days"); // 6 days
```

---

### 3.3 Convert UTC to Local Time Safely

**Description:**
Time zone conversion is critical for global applications. Proper handling prevents data loss and user confusion.

**Solutions:**
```csharp
public class TimeZoneConversionExamples
{
    // Method 1: Convert UTC to Local Time
    public static DateTime ConvertUtcToLocal(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
            throw new ArgumentException("DateTime must be UTC");
            
        return utcDateTime.ToLocalTime();
    }
    
    // Method 2: Convert to Specific Time Zone
    public static DateTime ConvertUtcToTimeZone(DateTime utcDateTime, string timeZoneId)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
            utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
            
        TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, targetTimeZone);
    }
    
    // Method 3: Convert Between Any Two Time Zones
    public static DateTime ConvertBetweenTimeZones(
        DateTime dateTime, 
        string sourceTimeZoneId, 
        string targetTimeZoneId)
    {
        TimeZoneInfo sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId);
        TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(targetTimeZoneId);
        
        return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, targetTimeZone);
    }
    
    // Method 4: Safe UTC Conversion with DateTimeOffset
    public static DateTimeOffset ConvertToUtcOffset(DateTime localDateTime, string timeZoneId)
    {
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        DateTimeOffset dto = new DateTimeOffset(localDateTime, timeZone.GetUtcOffset(localDateTime));
        return dto.ToUniversalTime();
    }
    
    // Method 5: Handle Ambiguous Times (DST)
    public static DateTime ConvertAmbiguousTime(
        DateTime dateTime, 
        string timeZoneId, 
        bool isDaylightSaving)
    {
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        
        if (timeZone.IsAmbiguousTime(dateTime))
        {
            TimeSpan[] offsets = timeZone.GetAmbiguousTimeOffsets(dateTime);
            TimeSpan offset = isDaylightSaving ? offsets[0] : offsets[1];
            
            return new DateTimeOffset(dateTime, offset).UtcDateTime;
        }
        
        return TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone);
    }
    
    // Method 6: Get All Available Time Zones
    public static List<TimeZoneInfo> GetAllTimeZones()
    {
        return TimeZoneInfo.GetSystemTimeZones().ToList();
    }
    
    // Method 7: Display Time in Multiple Zones
    public static Dictionary<string, DateTime> GetTimeInMultipleZones(DateTime utcDateTime)
    {
        var times = new Dictionary<string, DateTime>();
        
        var importantZones = new[]
        {
            "UTC",
            "Eastern Standard Time",      // EST
            "Pacific Standard Time",       // PST
            "Central European Standard Time", // CET
            "India Standard Time",         // IST
            "AUS Eastern Standard Time"    // AEST
        };
        
        foreach (var zoneId in importantZones)
        {
            try
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
                var convertedTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone);
                times[timeZone.DisplayName] = convertedTime;
            }
            catch (TimeZoneNotFoundException)
            {
                // Handle missing time zone
            }
        }
        
        return times;
    }
    
    // Method 8: Check if Time Zone Observes DST
    public static bool ObservesDaylightSaving(string timeZoneId)
    {
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return timeZone.SupportsDaylightSavingTime;
    }
    
    // Method 9: Store and Retrieve Dates Safely
    public static class SafeDateStorage
    {
        // Always store in UTC
        public static DateTime StoreDate(DateTime localDateTime, string userTimeZoneId)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);
            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, userTimeZone);
        }
        
        // Convert back to user's time zone
        public static DateTime RetrieveDate(DateTime utcDateTime, string userTimeZoneId)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, userTimeZone);
        }
    }
}

// Best Practices Class
public class TimeZoneBestPractices
{
    // Example: API Response with Time Zone Info
    public class ApiResponse
    {
        public DateTime Timestamp { get; set; }
        public string TimeZone { get; set; }
        public string FormattedTime { get; set; }
    }
    
    public static ApiResponse CreateResponse(DateTime utcTime, string userTimeZoneId)
    {
        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);
        DateTime userTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, userTimeZone);
        
        return new ApiResponse
        {
            Timestamp = utcTime,
            TimeZone = userTimeZoneId,
            FormattedTime = userTime.ToString("yyyy-MM-dd HH:mm:ss")
        };
    }
    
    // Example: Scheduling System
    public class ScheduledEvent
    {
        public DateTime UtcStartTime { get; set; }
        public string TimeZoneId { get; set; }
        
        public DateTime GetLocalStartTime()
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(UtcStartTime, timeZone);
        }
    }
}
```

**Example Usage:**
```csharp
// Current UTC time
DateTime utcNow = DateTime.UtcNow;
Console.WriteLine($"UTC: {utcNow}");

// Convert to Eastern Time
DateTime eastTime = ConvertUtcToTimeZone(utcNow, "Eastern Standard Time");
Console.WriteLine($"Eastern: {eastTime}");

// Convert to India Time
DateTime indiaTime = ConvertUtcToTimeZone(utcNow, "India Standard Time");
Console.WriteLine($"India: {indiaTime}");

// Display in multiple zones
var times = GetTimeInMultipleZones(utcNow);
foreach (var kvp in times)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value:yyyy-MM-dd HH:mm:ss}");
}
```

**Common Time Zone IDs:**
- **UTC**: "UTC"
- **EST**: "Eastern Standard Time"
- **PST**: "Pacific Standard Time"
- **CST**: "Central Standard Time"
- **IST**: "India Standard Time"
- **GMT**: "GMT Standard Time"
- **CET**: "Central European Standard Time"

**Best Practices:**
1. Always store dates in UTC in database
2. Convert to user's time zone for display only
3. Use `DateTimeOffset` for precise time zone handling
4. Handle DST transitions carefully
5. Always specify `DateTimeKind` when creating DateTime objects

---

## 4. Multithreading & Async Programming

### 4.1 Convert Sync Code to Async

**Description:**
Converting synchronous code to asynchronous improves application responsiveness and scalability, especially for I/O-bound operations.

**Examples:**

**Scenario 1: Database Operations**
```csharp
// Before: Synchronous
public class UserRepository
{
    private readonly DbContext _context;
    
    public User GetUserById(int id)
    {
        return _context.Users.Find(id);
    }
    
    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }
    
    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}

// After: Asynchronous
public class UserRepositoryAsync
{
    private readonly DbContext _context;
    
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
    
    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
```

**Scenario 2: HTTP Calls**
```csharp
// Before: Synchronous
public class WeatherService
{
    private readonly HttpClient _httpClient;
    
    public string GetWeather(string city)
    {
        var response = _httpClient.GetAsync($"api/weather/{city}").Result;
        return response.Content.ReadAsStringAsync().Result;
    }
}

// After: Asynchronous
public class WeatherServiceAsync
{
    private readonly HttpClient _httpClient;
    
    public async Task<string> GetWeatherAsync(string city)
    {
        var response = await _httpClient.GetAsync($"api/weather/{city}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
```

**Scenario 3: File Operations**
```csharp
// Before: Synchronous
public class FileService
{
    public string ReadFile(string path)
    {
        return File.ReadAllText(path);
    }
    
    public void WriteFile(string path, string content)
    {
        File.WriteAllText(path, content);
    }
    
    public byte[] ReadBinaryFile(string path)
    {
        return File.ReadAllBytes(path);
    }
}

// After: Asynchronous
public class FileServiceAsync
{
    public async Task<string> ReadFileAsync(string path)
    {
        return await File.ReadAllTextAsync(path);
    }
    
    public async Task WriteFileAsync(string path, string content)
    {
        await File.WriteAllTextAsync(path, content);
    }
    
    public async Task<byte[]> ReadBinaryFileAsync(string path)
    {
        return await File.ReadAllBytesAsync(path);
    }
}
```

**Scenario 4: Multiple Async Operations**
```csharp
public class DataAggregationService
{
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    
    // Before: Sequential Synchronous (Slow)
    public DashboardData GetDashboardDataSync(int userId)
    {
        var user = _userService.GetUser(userId);           // 100ms
        var orders = _orderService.GetUserOrders(userId);  // 150ms
        var products = _productService.GetProducts();      // 200ms
        
        // Total: ~450ms
        return new DashboardData { User = user, Orders = orders, Products = products };
    }
    
    // After: Parallel Asynchronous (Fast)
    public async Task<DashboardData> GetDashboardDataAsync(int userId)
    {
        // All three calls run in parallel
        var userTask = _userService.GetUserAsync(userId);         // 100ms
        var ordersTask = _orderService.GetUserOrdersAsync(userId); // 150ms
        var productsTask = _productService.GetProductsAsync();    // 200ms
        
        await Task.WhenAll(userTask, ordersTask, productsTask);
        
        // Total: ~200ms (time of slowest operation)
        return new DashboardData 
        { 
            User = await userTask, 
            Orders = await ordersTask, 
            Products = await productsTask 
        };
    }
}
```

**Scenario 5: Error Handling in Async Code**
```csharp
public class AsyncErrorHandling
{
    // Synchronous
    public string ProcessDataSync(string input)
    {
        try
        {
            // Some processing
            return "Success";
        }
        catch (Exception ex)
        {
            // Handle error
            return "Error";
        }
    }
    
    // Asynchronous - Same pattern
    public async Task<string> ProcessDataAsync(string input)
    {
        try
        {
            await Task.Delay(100); // Simulate async work
            // Some processing
            return "Success";
        }
        catch (Exception ex)
        {
            // Handle error
            return "Error";
        }
    }
    
    // Multiple operations with individual error handling
    public async Task<Result> ProcessMultipleOperationsAsync()
    {
        var result = new Result();
        
        try
        {
            result.Data1 = await Operation1Async();
        }
        catch (Exception ex)
        {
            result.Errors.Add($"Operation1 failed: {ex.Message}");
        }
        
        try
        {
            result.Data2 = await Operation2Async();
        }
        catch (Exception ex)
        {
            result.Errors.Add($"Operation2 failed: {ex.Message}");
        }
        
        return result;
    }
    
    private async Task<string> Operation1Async() => await Task.FromResult("Data1");
    private async Task<string> Operation2Async() => await Task.FromResult("Data2");
}

public class Result
{
    public string Data1 { get; set; }
    public string Data2 { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}
```

**Key Conversion Rules:**
1. Add `async` keyword to method signature
2. Change return type: `T` → `Task<T>`, `void` → `Task`
3. Add `Async` suffix to method name (convention)
4. Use `await` for async operations
5. Replace `.Result` or `.Wait()` with `await`

---

### 4.2 Difference Between Task vs Thread

**Description:**
Understanding the difference between Task and Thread is crucial for effective concurrent programming in .NET.

**Comparison Table:**

| Aspect | Thread | Task |
|--------|--------|------|
| **Abstraction Level** | Low-level | High-level |
| **Resource Usage** | Each thread = 1 OS thread | Uses ThreadPool, more efficient |
| **Creation Cost** | Expensive (~1MB stack) | Lightweight |
| **Return Values** | No direct support | Built-in support with `Task<T>` |
| **Cancellation** | Manual implementation | Built-in `CancellationToken` |
| **Continuation** | Manual | Easy with `ContinueWith` and `await` |
| **Exception Handling** | Try-catch in thread | Propagates to caller |
| **Recommended Use** | Rare, low-level scenarios | Default choice for async operations |

**Code Examples:**

```csharp
public class TaskVsThreadExamples
{
    // Thread Example
    public void ThreadExample()
    {
        Thread thread = new Thread(() =>
        {
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine("Thread completed");
        });
        
        thread.Start();
        thread.Join(); // Wait for completion
    }
    
    // Task Example
    public async Task TaskExample()
    {
        await Task.Run(() =>
        {
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(1000).Wait();
            Console.WriteLine("Task completed");
        });
    }
    
    // Thread - Cannot return value directly
    public int ThreadWithReturnValue()
    {
        int result = 0;
        Thread thread = new Thread(() =>
        {
            Thread.Sleep(1000);
            result = 42; // Must use shared variable
        });
        
        thread.Start();
        thread.Join();
        return result;
    }
    
    // Task - Clean return value
    public async Task<int> TaskWithReturnValue()
    {
        return await Task.Run(() =>
        {
            Task.Delay(1000).Wait();
            return 42;
        });
    }
    
    // Thread - Manual cancellation
    public void ThreadWithCancellation()
    {
        bool shouldStop = false;
        
        Thread thread = new Thread(() =>
        {
            while (!shouldStop)
            {
                Console.WriteLine("Working...");
                Thread.Sleep(100);
            }
            Console.WriteLine("Thread stopped");
        });
        
        thread.Start();
        Thread.Sleep(500);
        shouldStop = true; // Signal to stop
        thread.Join();
    }
    
    // Task - Built-in cancellation
    public async Task TaskWithCancellationAsync(CancellationToken cancellationToken)
    {
        await Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Working...");
                await Task.Delay(100, cancellationToken);
            }
            Console.WriteLine("Task cancelled");
        }, cancellationToken);
    }
    
    // Thread - Manual exception handling
    public void ThreadWithException()
    {
        Exception threadException = null;
        
        Thread thread = new Thread(() =>
        {
            try
            {
                throw new InvalidOperationException("Thread error");
            }
            catch (Exception ex)
            {
                threadException = ex; // Must capture manually
            }
        });
        
        thread.Start();
        thread.Join();
        
        if (threadException != null)
        {
            Console.WriteLine($"Thread threw: {threadException.Message}");
        }
    }
    
    // Task - Automatic exception propagation
    public async Task TaskWithExceptionAsync()
    {
        try
        {
            await Task.Run(() =>
            {
                throw new InvalidOperationException("Task error");
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Task threw: {ex.Message}");
        }
    }
    
    // Thread - Multiple threads
    public void MultipleThreads()
    {
        var threads = new List<Thread>();
        
        for (int i = 0; i < 5; i++)
        {
            int taskNum = i;
            Thread thread = new Thread(() =>
            {
                Console.WriteLine($"Thread {taskNum} on {Thread.CurrentThread.ManagedThreadId}");
            });
            threads.Add(thread);
            thread.Start();
        }
        
        // Wait for all
        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
    
    // Task - Multiple tasks (more efficient)
    public async Task MultipleTasksAsync()
    {
        var tasks = new List<Task>();
        
        for (int i = 0; i < 5; i++)
        {
            int taskNum = i;
            tasks.Add(Task.Run(() =>
            {
                Console.WriteLine($"Task {taskNum} on {Thread.CurrentThread.ManagedThreadId}");
            }));
        }
        
        await Task.WhenAll(tasks);
    }
    
    // Performance Comparison
    public void PerformanceComparison()
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        
        // Create 1000 threads (very expensive!)
        // DON'T DO THIS IN PRODUCTION
        /*
        var threads = new List<Thread>();
        for (int i = 0; i < 1000; i++)
        {
            var thread = new Thread(() => Thread.Sleep(100));
            threads.Add(thread);
            thread.Start();
        }
        foreach (var t in threads) t.Join();
        */
        
        Console.WriteLine($"Threads: {sw.ElapsedMilliseconds}ms");
        sw.Restart();
        
        // Create 1000 tasks (efficient, uses ThreadPool)
        var tasks = new List<Task>();
        for (int i = 0; i < 1000; i++)
        {
            tasks.Add(Task.Run(() => Task.Delay(100).Wait()));
        }
        Task.WaitAll(tasks.ToArray());
        
        Console.WriteLine($"Tasks: {sw.ElapsedMilliseconds}ms");
    }
}
```

**When to Use Thread:**
- Need precise control over thread priority
- Long-running operations that shouldn't use ThreadPool
- Need to set apartment state (COM interop, old UI frameworks)
- Very rare scenarios

**When to Use Task (95% of cases):**
- Async I/O operations
- Parallel processing
- Web APIs and services
- Any modern asynchronous code

**Best Practices:**
```csharp
public class BestPractices
{
    // ✅ Good: Use Task for async operations
    public async Task<string> GoodExampleAsync()
    {
        return await Task.Run(() => "Result");
    }
    
    // ❌ Bad: Don't use Thread for simple async work
    public string BadExample()
    {
        string result = null;
        Thread thread = new Thread(() => result = "Result");
        thread.Start();
        thread.Join();
        return result;
    }
    
    // ✅ Good: Parallel operations with Task
    public async Task<int[]> ParallelProcessingAsync(int[] data)
    {
        var tasks = data.Select(item => Task.Run(() => Process(item)));
        return await Task.WhenAll(tasks);
    }
    
    private int Process(int item) => item * 2;
}
```

---

### 4.3 What Happens If You Don't Await?

**Description:**
Not awaiting async operations leads to "fire and forget" behavior, which can cause subtle bugs, exceptions being swallowed, and unexpected application behavior.

**Examples and Consequences:**

```csharp
public class AwaitExamples
{
    // Example 1: Fire and Forget (Dangerous!)
    public async Task BadExample()
    {
        DoSomethingAsync(); // ⚠️ Not awaited - fires and forgets!
        Console.WriteLine("This runs immediately");
    }
    
    // Example 2: Proper Await
    public async Task GoodExample()
    {
        await DoSomethingAsync(); // ✅ Waits for completion
        Console.WriteLine("This runs after DoSomethingAsync completes");
    }
    
    private async Task DoSomethingAsync()
    {
        await Task.Delay(1000);
        Console.WriteLine("Async operation completed");
    }
    
    // Example 3: Exception Swallowing
    public async Task ExceptionSwallowingExample()
    {
        // ❌ Exception is never observed!
        ThrowExceptionAsync();
        
        Console.WriteLine("Exception was thrown but we don't know about it!");
        await Task.Delay(2000); // Give time for exception to occur
    }
    
    public async Task ProperExceptionHandling()
    {
        try
        {
            // ✅ Exception is caught and handled
            await ThrowExceptionAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught exception: {ex.Message}");
        }
    }
    
    private async Task ThrowExceptionAsync()
    {
        await Task.Delay(100);
        throw new InvalidOperationException("Something went wrong!");
    }
    
    // Example 4: Return Value Lost
    public async Task LostReturnValueExample()
    {
        // ❌ Return value is lost!
        GetDataAsync();
        
        // Can't access the result
        Console.WriteLine("We can't get the data!");
    }
    
    public async Task ProperReturnValueExample()
    {
        // ✅ Can access return value
        var data = await GetDataAsync();
        Console.WriteLine($"Got data: {data}");
    }
    
    private async Task<string> GetDataAsync()
    {
        await Task.Delay(100);
        return "Important Data";
    }
    
    // Example 5: Race Conditions
    public class DataService
    {
        private List<string> _data = new List<string>();
        
        // ❌ Race condition!
        public async Task BadDataLoadingAsync()
        {
            LoadDataAsync(); // Not awaited
            
            // This might run before data is loaded!
            Console.WriteLine($"Data count: {_data.Count}");
        }
        
        // ✅ Proper synchronization
        public async Task GoodDataLoadingAsync()
        {
            await LoadDataAsync();
            
            // This runs after data is loaded
            Console.WriteLine($"Data count: {_data.Count}");
        }
        
        private async Task LoadDataAsync()
        {
            await Task.Delay(500);
            _data.AddRange(new[] { "Item1", "Item2", "Item3" });
        }
    }
    
    // Example 6: Memory Leaks with Unobserved Tasks
    public void MemoryLeakExample()
    {
        for (int i = 0; i < 1000; i++)
        {
            // ❌ These tasks keep running and holding resources
            LeakingTaskAsync(i);
        }
    }
    
    private async Task LeakingTaskAsync(int id)
    {
        await Task.Delay(10000); // Long running
        Console.WriteLine($"Task {id} completed");
    }
    
    // Example 7: Proper Fire-and-Forget (When Actually Needed)
    public async Task ProperFireAndForgetAsync()
    {
        // When you really need fire-and-forget, be explicit:
        _ = Task.Run(async () =>
        {
            try
            {
                await BackgroundOperationAsync();
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Background operation failed: {ex.Message}");
            }
        });
        
        Console.WriteLine("Continues immediately");
    }
    
    private async Task BackgroundOperationAsync()
    {
        await Task.Delay(5000);
        Console.WriteLine("Background operation completed");
    }
    
    // Example 8: Async void (Only for Event Handlers)
    public class AsyncVoidExample
    {
        // ❌ Never do this for regular methods
        public async void BadAsyncVoid()
        {
            await Task.Delay(100);
            throw new Exception("This exception can crash the app!");
        }
        
        // ✅ Return Task instead
        public async Task GoodAsyncTask()
        {
            await Task.Delay(100);
            // Exceptions can be caught by caller
        }
        
        // ✅ Only acceptable use: Event handlers
        public async void Button_Click(object sender, EventArgs e)
        {
            try
            {
                await ProcessClickAsync();
            }
            catch (Exception ex)
            {
                // Handle exception in event handler
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        
        private async Task ProcessClickAsync()
        {
            await Task.Delay(100);
        }
    }
    
    // Example 9: Comparing Behaviors
    public async Task CompareAwaitBehaviors()
    {
        Console.WriteLine("Starting comparisons...\n");
        
        // Without await
        var sw = System.Diagnostics.Stopwatch.StartNew();
        WithoutAwait();
        Console.WriteLine($"WithoutAwait returned in: {sw.ElapsedMilliseconds}ms\n");
        
        // With await
        sw.Restart();
        await WithAwait();
        Console.WriteLine($"WithAwait returned in: {sw.ElapsedMilliseconds}ms\n");
        
        await Task.Delay(2000); // Let fire-and-forget complete
    }
    
    private async Task WithoutAwait()
    {
        Task.Delay(1000); // Not awaited - method returns immediately
        Console.WriteLine("WithoutAwait method body complete");
    }
    
    private async Task WithAwait()
    {
        await Task.Delay(1000); // Awaited - method waits
        Console.WriteLine("WithAwait method body complete");
    }
}

// Example 10: Real-World Scenario - Web API
public class UserController
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    
    // ❌ Bad: Not awaiting in API controller
    [HttpPost]
    public async Task<IActionResult> RegisterUserBad(UserDto userDto)
    {
        var user = await _userService.CreateUserAsync(userDto);
        
        // ❌ Email might not be sent before response returns!
        _emailService.SendWelcomeEmailAsync(user.Email);
        
        return Ok(user);
    }
    
    // ✅ Good: Await everything
    [HttpPost]
    public async Task<IActionResult> RegisterUserGood(UserDto userDto)
    {
        var user = await _userService.CreateUserAsync(userDto);
        
        // ✅ Ensures email is sent
        await _emailService.SendWelcomeEmailAsync(user.Email);
        
        return Ok(user);
    }
    
    // ✅ Good: Intentional background task
    [HttpPost]
    public async Task<IActionResult> RegisterUserWithBackgroundEmail(UserDto userDto)
    {
        var user = await _userService.CreateUserAsync(userDto);
        
        // Explicitly fire-and-forget with error handling
        _ = SendEmailInBackground(user.Email);
        
        return Ok(user);
    }
    
    private async Task SendEmailInBackground(string email)
    {
        try
        {
            await _emailService.SendWelcomeEmailAsync(email);
        }
        catch (Exception ex)
        {
            // Log error but don't fail the request
            Console.WriteLine($"Background email failed: {ex.Message}");
        }
    }
}
```

**What Actually Happens Without Await:**

1. **Method Returns Immediately**: The async method returns a `Task` but doesn't wait for it
2. **Exceptions Are Swallowed**: Unobserved exceptions may not be seen
3. **No Return Value**: Can't access the result
4. **Race Conditions**: Code continues before async operation completes
5. **Resource Issues**: Tasks may keep running after method exits

**Key Rules:**
- ✅ **Always await** async methods unless you have a specific reason not to
- ✅ Use `Task` instead of `async void` (except event handlers)
- ✅ If you must fire-and-forget, use `_ = Task.Run()` with try-catch
- ✅ Never ignore exceptions in async code
- ✅ In ASP.NET Core, always await async operations

---

## 5. Thread Safety

### 5.1 Singleton Thread-Safe Implementation

**Description:**
Thread-safe Singleton ensures only one instance exists even under concurrent access. Critical for caching, configuration, and resource management.

**Implementation Patterns:**

```csharp
// Pattern 1: Lazy<T> (Recommended - Simplest)
public class Singleton1
{
    private static readonly Lazy<Singleton1> _instance = 
        new Lazy<Singleton1>(() => new Singleton1());
    
    private Singleton1()
    {
        // Private constructor
        Console.WriteLine("Singleton1 instance created");
    }
    
    public static Singleton1 Instance => _instance.Value;
    
    public void DoSomething()
    {
        Console.WriteLine("Doing something...");
    }
}

// Pattern 2: Double-Check Locking (Classic)
public class Singleton2
{
    private static Singleton2 _instance;
    private static readonly object _lock = new object();
    
    private Singleton2()
    {
        Console.WriteLine("Singleton2 instance created");
    }
    
    public static Singleton2 Instance
    {
        get
        {
            if (_instance == null) // First check (no lock)
            {
                lock (_lock)
                {
                    if (_instance == null) // Second check (with lock)
                    {
                        _instance = new Singleton2();
                    }
                }
            }
            return _instance;
        }
    }
}

// Pattern 3: Static Constructor (Thread-Safe by CLR)
public class Singleton3
{
    private static readonly Singleton3 _instance = new Singleton3();
    
    // Explicit static constructor tells C# not to mark type as beforefieldinit
    static Singleton3()
    {
    }
    
    private Singleton3()
    {
        Console.WriteLine("Singleton3 instance created");
    }
    
    public static Singleton3 Instance => _instance;
}

// Pattern 4: Nested Class (Lazy Initialization)
public class Singleton4
{
    private Singleton4()
    {
        Console.WriteLine("Singleton4 instance created");
    }
    
    private class Nested
    {
        // Explicit static constructor for lazy initialization
        static Nested()
        {
        }
        
        internal static readonly Singleton4 instance = new Singleton4();
    }
    
    public static Singleton4 Instance => Nested.instance;
}

// Pattern 5: Thread-Safe with Parameters
public class ConfigurationManager
{
    private static ConfigurationManager _instance;
    private static readonly object _lock = new object();
    
    public string Environment { get; private set; }
    public Dictionary<string, string> Settings { get; private set; }
    
    private ConfigurationManager(string environment)
    {
        Environment = environment;
        Settings = new Dictionary<string, string>();
        LoadConfiguration();
    }
    
    public static ConfigurationManager Initialize(string environment)
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationManager(environment);
                }
            }
        }
        return _instance;
    }
    
    public static ConfigurationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("ConfigurationManager not initialized");
            }
            return _instance;
        }
    }
    
    private void LoadConfiguration()
    {
        // Load configuration based on environment
        Settings["DatabaseConnection"] = $"Server=db-{Environment}.example.com";
        Settings["ApiKey"] = $"key-{Environment}";
    }
}

// Pattern 6: Generic Singleton Base Class
public abstract class SingletonBase<T> where T : class
{
    private static readonly Lazy<T> _instance = new Lazy<T>(() =>
    {
        // Use reflection to create instance of derived type
        var constructor = typeof(T).GetConstructor(
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
            null, Type.EmptyTypes, null);
            
        if (constructor == null)
        {
            throw new InvalidOperationException(
                $"Type {typeof(T).Name} must have a private parameterless constructor");
        }
        
        return (T)constructor.Invoke(null);
    });
    
    public static T Instance => _instance.Value;
}

// Usage of Generic Singleton
public class Logger : SingletonBase<Logger>
{
    private Logger()
    {
        Console.WriteLine("Logger instance created");
    }
    
    public void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
    }
}

// Pattern 7: Singleton with Dispose Support
public class DatabaseConnection : IDisposable
{
    private static readonly Lazy<DatabaseConnection> _instance = 
        new Lazy<DatabaseConnection>(() => new DatabaseConnection());
    
    private bool _disposed = false;
    
    private DatabaseConnection()
    {
        Console.WriteLine("Database connection established");
        // Initialize connection
    }
    
    public static DatabaseConnection Instance => _instance.Value;
    
    public void ExecuteQuery(string query)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(DatabaseConnection));
            
        Console.WriteLine($"Executing: {query}");
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources
                Console.WriteLine("Database connection closed");
            }
            _disposed = true;
        }
    }
}

// Pattern 8: Testing-Friendly Singleton
public interface ICache
{
    void Set(string key, object value);
    object Get(string key);
}

public class CacheManager : ICache
{
    private static readonly Lazy<CacheManager> _instance = 
        new Lazy<CacheManager>(() => new CacheManager());
    
    private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();
    private readonly object _lock = new object();
    
    private CacheManager() { }
    
    public static CacheManager Instance => _instance.Value;
    
    // Allow injection for testing
    public static ICache CreateTestInstance() => new CacheManager();
    
    public void Set(string key, object value)
    {
        lock (_lock)
        {
            _cache[key] = value;
        }
    }
    
    public object Get(string key)
    {
        lock (_lock)
        {
            return _cache.TryGetValue(key, out var value) ? value : null;
        }
    }
}
```

**Testing Thread Safety:**
```csharp
public class SingletonThreadSafetyTest
{
    public static void TestThreadSafety()
    {
        const int threadCount = 10;
        var instances = new System.Collections.Concurrent.ConcurrentBag<Singleton1>();
        var threads = new List<Thread>();
        
        for (int i = 0; i < threadCount; i++)
        {
            var thread = new Thread(() =>
            {
                var instance = Singleton1.Instance;
                instances.Add(instance);
            });
            threads.Add(thread);
        }
        
        // Start all threads simultaneously
        foreach (var thread in threads)
            thread.Start();
            
        // Wait for completion
        foreach (var thread in threads)
            thread.Join();
        
        // Verify all instances are the same
        var first = instances.First();
        bool allSame = instances.All(x => ReferenceEquals(x, first));
        
        Console.WriteLine($"Created {instances.Distinct().Count()} unique instance(s)");
        Console.WriteLine($"Thread-safe: {allSame}");
    }
}
```

**Example Usage:**
```csharp
// Simple usage
var logger = Logger.Instance;
logger.Log("Application started");

// Configuration with initialization
var config = ConfigurationManager.Initialize("Production");
Console.WriteLine(config.Settings["DatabaseConnection"]);

// Cache usage
CacheManager.Instance.Set("user:1", new { Name = "John", Age = 30 });
var user = CacheManager.Instance.Get("user:1");
```

**Comparison of Patterns:**

| Pattern | Lazy? | Thread-Safe? | Performance | Complexity |
|---------|-------|--------------|-------------|------------|
| Lazy<T> | Yes | Yes | Excellent | Low (Recommended) |
| Double-Check Lock | Yes | Yes | Excellent | Medium |
| Static Constructor | No | Yes | Excellent | Low |
| Nested Class | Yes | Yes | Excellent | Medium |

**Best Practices:**
- ✅ Use `Lazy<T>` for simplicity and performance
- ✅ Make constructor private
- ✅ Consider thread safety from the start
- ✅ Document why you need a Singleton
- ⚠️ Be cautious with Singletons in web applications (consider Dependency Injection instead)

---

### 5.2 Use of Lock, SemaphoreSlim, ConcurrentDictionary

**Description:**
Thread synchronization primitives protect shared resources from concurrent access. Each has specific use cases and performance characteristics.

**1. Lock (Monitor) - Basic Synchronization:**
```csharp
public class LockExamples
{
    private readonly object _lock = new object();
    private int _counter = 0;
    private List<string> _items = new List<string>();
    
    // Example 1: Protect shared counter
    public void IncrementCounter()
    {
        lock (_lock)
        {
            _counter++;
            Console.WriteLine($"Counter: {_counter}");
        }
    }
    
    // Example 2: Protect collection operations
    public void AddItem(string item)
    {
        lock (_lock)
        {
            _items.Add(item);
        }
    }
    
    public int GetCount()
    {
        lock (_lock)
        {
            return _items.Count;
        }
    }
    
    // Example 3: Multiple operations in critical section
    public void TransferFunds(Account from, Account to, decimal amount)
    {
        // Lock both accounts to prevent deadlock (always lock in same order)
        object lock1 = from.Id < to.Id ? from.Lock : to.Lock;
        object lock2 = from.Id < to.Id ? to.Lock : from.Lock;
        
        lock (lock1)
        {
            lock (lock2)
            {
                if (from.Balance >= amount)
                {
                    from.Balance -= amount;
                    to.Balance += amount;
                    Console.WriteLine($"Transferred {amount:C} from {from.Id} to {to.Id}");
                }
            }
        }
    }
    
    // Example 4: Lock with timeout (using Monitor)
    public bool TryAddItemWithTimeout(string item, int timeoutMs)
    {
        if (Monitor.TryEnter(_lock, timeoutMs))
        {
            try
            {
                _items.Add(item);
                return true;
            }
            finally
            {
                Monitor.Exit(_lock);
            }
        }
        return false;
    }
}

public class Account
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public object Lock { get; } = new object();
}
```

**2. SemaphoreSlim - Limited Concurrency:**
```csharp
public class SemaphoreSlimExamples
{
    // Example 1: Limit concurrent access (e.g., API rate limiting)
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(3, 3); // Max 3 concurrent
    
    public async Task ProcessRequestAsync(int requestId)
    {
        Console.WriteLine($"Request {requestId} waiting...");
        
        await _semaphore.WaitAsync();
        try
        {
            Console.WriteLine($"Request {requestId} processing...");
            await Task.Delay(2000); // Simulate work
            Console.WriteLine($"Request {requestId} completed");
        }
        finally
        {
            _semaphore.Release();
        }
    }
    
    // Example 2: Database connection pool
    public class DatabaseConnectionPool
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly List<DatabaseConnection> _connections;
        
        public DatabaseConnectionPool(int maxConnections)
        {
            _semaphore = new SemaphoreSlim(maxConnections, maxConnections);
            _connections = new List<DatabaseConnection>();
            
            for (int i = 0; i < maxConnections; i++)
            {
                _connections.Add(new DatabaseConnection { Id = i });
            }
        }
        
        public async Task<DatabaseConnection> AcquireConnectionAsync()
        {
            await _semaphore.WaitAsync();
            
            lock (_connections)
            {
                var connection = _connections.FirstOrDefault(c => !c.InUse);
                if (connection != null)
                {
                    connection.InUse = true;
                    return connection;
                }
            }
            
            throw new InvalidOperationException("No available connections");
        }
        
        public void ReleaseConnection(DatabaseConnection connection)
        {
            lock (_connections)
            {
                connection.InUse = false;
            }
            _semaphore.Release();
        }
    }
    
    public class DatabaseConnection
    {
        public int Id { get; set; }
        public bool InUse { get; set; }
    }
    
    // Example 3: Throttling parallel operations
    public async Task<List<string>> ProcessItemsWithThrottlingAsync(List<string> items)
    {
        var semaphore = new SemaphoreSlim(5); // Max 5 parallel operations
        var results = new System.Collections.Concurrent.ConcurrentBag<string>();
        
        var tasks = items.Select(async item =>
        {
            await semaphore.WaitAsync();
            try
            {
                var result = await ProcessItemAsync(item);
                results.Add(result);
            }
            finally
            {
                semaphore.Release();
            }
        });
        
        await Task.WhenAll(tasks);
        return results.ToList();
    }
    
    private async Task<string> ProcessItemAsync(string item)
    {
        await Task.Delay(100);
        return $"Processed: {item}";
    }
    
    // Example 4: Async lock alternative
    public class AsyncLock
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        
        public async Task<IDisposable> LockAsync()
        {
            await _semaphore.WaitAsync();
            return new ReleaseWrapper(_semaphore);
        }
        
        private class ReleaseWrapper : IDisposable
        {
            private readonly SemaphoreSlim _semaphore;
            
            public ReleaseWrapper(SemaphoreSlim semaphore)
            {
                _semaphore = semaphore;
            }
            
            public void Dispose()
            {
                _semaphore.Release();
            }
        }
    }
    
    // Usage of AsyncLock
    public async Task UseAsyncLockAsync()
    {
        var asyncLock = new AsyncLock();
        
        using (await asyncLock.LockAsync())
        {
            // Critical section
            await Task.Delay(100);
            Console.WriteLine("Protected operation completed");
        }
    }
}
```

**3. ConcurrentDictionary - Thread-Safe Collections:**
```csharp
public class ConcurrentDictionaryExamples
{
    // Example 1: Thread-safe cache
    private readonly ConcurrentDictionary<string, object> _cache = 
        new ConcurrentDictionary<string, object>();
    
    public void AddToCache(string key, object value)
    {
        _cache.TryAdd(key, value);
    }
    
    public object GetFromCache(string key)
    {
        _cache.TryGetValue(key, out var value);
        return value;
    }
    
    public object GetOrAddToCache(string key, Func<string, object> valueFactory)
    {
        return _cache.GetOrAdd(key, valueFactory);
    }
    
    // Example 2: AddOrUpdate with logic
    public void IncrementCounter(string key)
    {
        _cache.AddOrUpdate(
            key,
            1, // Add value if key doesn't exist
            (k, oldValue) => (int)oldValue + 1 // Update function
        );
    }
    
    // Example 3: Safe removal
    public bool RemoveFromCache(string key)
    {
        return _cache.TryRemove(key, out _);
    }
    
    // Example 4: User session management
    public class SessionManager
    {
        private readonly ConcurrentDictionary<string, UserSession> _sessions = 
            new ConcurrentDictionary<string, UserSession>();
        
        public UserSession CreateSession(string userId)
        {
            var session = new UserSession
            {
                SessionId = Guid.NewGuid().ToString(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastAccessedAt = DateTime.UtcNow
            };
            
            _sessions.TryAdd(session.SessionId, session);
            return session;
        }
        
        public UserSession GetSession(string sessionId)
        {
            if (_sessions.TryGetValue(sessionId, out var session))
            {
                session.LastAccessedAt = DateTime.UtcNow;
                return session;
            }
            return null;
        }
        
        public void RemoveExpiredSessions(TimeSpan timeout)
        {
            var expiredKeys = _sessions
                .Where(kvp => DateTime.UtcNow - kvp.Value.LastAccessedAt > timeout)
                .Select(kvp => kvp.Key)
                .ToList();
            
            foreach (var key in expiredKeys)
            {
                _sessions.TryRemove(key, out _);
            }
        }
    }
    
    public class UserSession
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastAccessedAt { get; set; }
    }
    
    // Example 5: Request counting
    public class RequestCounter
    {
        private readonly ConcurrentDictionary<string, int> _requestCounts = 
            new ConcurrentDictionary<string, int>();
        
        public void RecordRequest(string endpoint)
        {
            _requestCounts.AddOrUpdate(endpoint, 1, (key, count) => count + 1);
        }
        
        public Dictionary<string, int> GetStatistics()
        {
            return new Dictionary<string, int>(_requestCounts);
        }
        
        public void ResetCounter(string endpoint)
        {
            _requestCounts.TryRemove(endpoint, out _);
        }
    }
    
    // Example 6: TryUpdate with condition
    public bool UpdateIfMatches(string key, object newValue, object expectedValue)
    {
        return _cache.TryUpdate(key, newValue, expectedValue);
    }
    
    // Example 7: Batch operations
    public void AddMultiple(Dictionary<string, object> items)
    {
        foreach (var item in items)
        {
            _cache.TryAdd(item.Key, item.Value);
        }
    }
    
    // Example 8: Clear all (no direct Clear in some scenarios)
    public void ClearCache()
    {
        _cache.Clear();
    }
}

// Additional Concurrent Collections
public class OtherConcurrentCollections
{
    // ConcurrentBag - Unordered collection
    private readonly System.Collections.Concurrent.ConcurrentBag<string> _bag = 
        new System.Collections.Concurrent.ConcurrentBag<string>();
    
    public void AddToBag(string item)
    {
        _bag.Add(item);
    }
    
    public bool TryTakeFromBag(out string item)
    {
        return _bag.TryTake(out item);
    }
    
    // ConcurrentQueue - FIFO
    private readonly System.Collections.Concurrent.ConcurrentQueue<string> _queue = 
        new System.Collections.Concurrent.ConcurrentQueue<string>();
    
    public void Enqueue(string item)
    {
        _queue.Enqueue(item);
    }
    
    public bool TryDequeue(out string item)
    {
        return _queue.TryDequeue(out item);
    }
    
    // ConcurrentStack - LIFO
    private readonly System.Collections.Concurrent.ConcurrentStack<string> _stack = 
        new System.Collections.Concurrent.ConcurrentStack<string>();
    
    public void Push(string item)
    {
        _stack.Push(item);
    }
    
    public bool TryPop(out string item)
    {
        return _stack.TryPop(out item);
    }
}
```

**Comparison Table:**

| Mechanism | Use Case | Performance | Async Support | Complexity |
|-----------|----------|-------------|---------------|------------|
| **lock** | Simple mutual exclusion | Fast | No (blocks thread) | Low |
| **SemaphoreSlim** | Limited concurrency, async | Medium | Yes | Medium |
| **ConcurrentDictionary** | Thread-safe collections | Fast | N/A (lock-free) | Low |
| **Monitor** | lock with timeout/wait | Fast | No | Medium |
| **Mutex** | Cross-process sync | Slow | No | High |
| **ReaderWriterLockSlim** | Many readers, few writers | Medium | No | Medium |

**Best Practices:**
```csharp
public class BestPractices
{
    // ✅ Keep critical sections small
    private readonly object _lock = new object();
    private int _counter;
    
    public void GoodLock()
    {
        lock (_lock)
        {
            _counter++; // Fast operation only
        }
    }
    
    // ❌ Don't lock for long operations
    public void BadLock()
    {
        lock (_lock)
        {
            Thread.Sleep(5000); // Don't do this!
            _counter++;
        }
    }
    
    // ✅ Use SemaphoreSlim for async
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    
    public async Task GoodAsyncLock()
    {
        await _semaphore.WaitAsync();
        try
        {
            await Task.Delay(100); // Async operation
        }
        finally
        {
            _semaphore.Release();
        }
    }
    
    // ✅ Use ConcurrentDictionary instead of lock + Dictionary
    private readonly ConcurrentDictionary<string, int> _dict = 
        new ConcurrentDictionary<string, int>();
    
    public void GoodConcurrentAccess()
    {
        _dict.AddOrUpdate("key", 1, (k, v) => v + 1);
    }
}
```

---

## 6. Object-Oriented Design

### 6.1 Implement Factory Pattern

**Description:**
Factory Pattern provides an interface for creating objects without specifying their exact classes. Essential for loosely coupled, maintainable code.

**Implementation:**

```csharp
// Simple Factory Pattern
public interface IPayment
{
    void Pay(decimal amount);
    string GetPaymentMethod();
}

public class CreditCardPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Processing credit card payment of {amount:C}");
        // Credit card specific logic
    }
    
    public string GetPaymentMethod() => "Credit Card";
}

public class PayPalPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Processing PayPal payment of {amount:C}");
        // PayPal specific logic
    }
    
    public string GetPaymentMethod() => "PayPal";
}

public class CryptoPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Processing cryptocurrency payment of {amount:C}");
        // Crypto specific logic
    }
    
    public string GetPaymentMethod() => "Cryptocurrency";
}

// Simple Factory
public class PaymentFactory
{
    public static IPayment CreatePayment(string paymentType)
    {
        return paymentType.ToLower() switch
        {
            "creditcard" => new CreditCardPayment(),
            "paypal" => new PayPalPayment(),
            "crypto" => new CryptoPayment(),
            _ => throw new ArgumentException($"Unknown payment type: {paymentType}")
        };
    }
}

// Factory Method Pattern (More flexible)
public abstract class PaymentProcessor
{
    // Factory method
    public abstract IPayment CreatePayment();
    
    // Template method using factory method
    public void ProcessOrder(decimal amount)
    {
        var payment = CreatePayment();
        Console.WriteLine($"Using {payment.GetPaymentMethod()}");
        payment.Pay(amount);
        Console.WriteLine("Payment processed successfully");
    }
}

public class CreditCardProcessor : PaymentProcessor
{
    public override IPayment CreatePayment()
    {
        return new CreditCardPayment();
    }
}

public class PayPalProcessor : PaymentProcessor
{
    public override IPayment CreatePayment()
    {
        return new PayPalPayment();
    }
}

// Abstract Factory Pattern (Family of related objects)
public interface IUIFactory
{
    IButton CreateButton();
    ITextBox CreateTextBox();
    ICheckBox CreateCheckBox();
}

public interface IButton
{
    void Render();
}

public interface ITextBox
{
    void Render();
}

public interface ICheckBox
{
    void Render();
}

// Windows implementation
public class WindowsButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Windows-style button");
}

public class WindowsTextBox : ITextBox
{
    public void Render() => Console.WriteLine("Rendering Windows-style textbox");
}

public class WindowsCheckBox : ICheckBox
{
    public void Render() => Console.WriteLine("Rendering Windows-style checkbox");
}

// Mac implementation
public class MacButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Mac-style button");
}

public class MacTextBox : ITextBox
{
    public void Render() => Console.WriteLine("Rendering Mac-style textbox");
}

public class MacCheckBox : ICheckBox
{
    public void Render() => Console.WriteLine("Rendering Mac-style checkbox");
}

// Factories
public class WindowsUIFactory : IUIFactory
{
    public IButton CreateButton() => new WindowsButton();
    public ITextBox CreateTextBox() => new WindowsTextBox();
    public ICheckBox CreateCheckBox() => new WindowsCheckBox();
}

public class MacUIFactory : IUIFactory
{
    public IButton CreateButton() => new MacButton();
    public ITextBox CreateTextBox() => new MacTextBox();
    public ICheckBox CreateCheckBox() => new MacCheckBox();
}

// Client code
public class Application
{
    private readonly IUIFactory _uiFactory;
    
    public Application(IUIFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }
    
    public void CreateUI()
    {
        var button = _uiFactory.CreateButton();
        var textBox = _uiFactory.CreateTextBox();
        var checkBox = _uiFactory.CreateCheckBox();
        
        button.Render();
        textBox.Render();
        checkBox.Render();
    }
}

// Real-world example: Database Connection Factory
public interface IDatabase
{
    void Connect();
    void ExecuteQuery(string query);
    void Disconnect();
}

public class SqlServerDatabase : IDatabase
{
    public void Connect() => Console.WriteLine("Connected to SQL Server");
    public void ExecuteQuery(string query) => Console.WriteLine($"SQL Server executing: {query}");
    public void Disconnect() => Console.WriteLine("Disconnected from SQL Server");
}

public class PostgreSqlDatabase : IDatabase
{
    public void Connect() => Console.WriteLine("Connected to PostgreSQL");
    public void ExecuteQuery(string query) => Console.WriteLine($"PostgreSQL executing: {query}");
    public void Disconnect() => Console.WriteLine("Disconnected from PostgreSQL");
}

public class MongoDatabase : IDatabase
{
    public void Connect() => Console.WriteLine("Connected to MongoDB");
    public void ExecuteQuery(string query) => Console.WriteLine($"MongoDB executing: {query}");
    public void Disconnect() => Console.WriteLine("Disconnected from MongoDB");
}

public class DatabaseFactory
{
    public static IDatabase CreateDatabase(string connectionString)
    {
        if (connectionString.Contains("sqlserver", StringComparison.OrdinalIgnoreCase))
            return new SqlServerDatabase();
        if (connectionString.Contains("postgresql", StringComparison.OrdinalIgnoreCase))
            return new PostgreSqlDatabase();
        if (connectionString.Contains("mongodb", StringComparison.OrdinalIgnoreCase))
            return new MongoDatabase();
            
        throw new ArgumentException("Unknown database type");
    }
}
```

**Example Usage:**
```csharp
// Simple Factory
var payment = PaymentFactory.CreatePayment("creditcard");
payment.Pay(100.50m);

// Factory Method
PaymentProcessor processor = new CreditCardProcessor();
processor.ProcessOrder(250.00m);

// Abstract Factory
IUIFactory factory = new WindowsUIFactory();
var app = new Application(factory);
app.CreateUI();

// Database Factory
var db = DatabaseFactory.CreateDatabase("Server=localhost;Database=mydb;Provider=sqlserver");
db.Connect();
db.ExecuteQuery("SELECT * FROM Users");
db.Disconnect();
```

**Benefits:**
- Loose coupling between client and concrete classes
- Easy to extend with new types
- Single Responsibility Principle
- Open/Closed Principle

---

### 6.2 Strategy Pattern Use Case

**Description:**
Strategy Pattern defines a family of algorithms, encapsulates each one, and makes them interchangeable. Perfect for when behavior needs to change at runtime.

**Implementation:**

```csharp
// Strategy Interface
public interface IShippingStrategy
{
    decimal CalculateShippingCost(decimal orderTotal, decimal weight);
    string GetShippingMethod();
}

// Concrete Strategies
public class StandardShipping : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal orderTotal, decimal weight)
    {
        return weight * 5m; // $5 per kg
    }
    
    public string GetShippingMethod() => "Standard Shipping (5-7 days)";
}

public class ExpressShipping : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal orderTotal, decimal weight)
    {
        return weight * 10m + 15m; // $10 per kg + $15 handling
    }
    
    public string GetShippingMethod() => "Express Shipping (2-3 days)";
}

public class OvernightShipping : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal orderTotal, decimal weight)
    {
        return weight * 20m + 25m; // $20 per kg + $25 handling
    }
    
    public string GetShippingMethod() => "Overnight Shipping (Next day)";
}

public class FreeShipping : IShippingStrategy
{
    private readonly decimal _minimumOrder;
    
    public FreeShipping(decimal minimumOrder = 100m)
    {
        _minimumOrder = minimumOrder;
    }
    
    public decimal CalculateShippingCost(decimal orderTotal, decimal weight)
    {
        return orderTotal >= _minimumOrder ? 0 : weight * 5m;
    }
    
    public string GetShippingMethod() => $"Free Shipping (Orders over ${_minimumOrder})";
}

// Context
public class ShoppingCart
{
    private IShippingStrategy _shippingStrategy;
    private List<CartItem> _items = new List<CartItem>();
    
    public void AddItem(string name, decimal price, decimal weight)
    {
        _items.Add(new CartItem { Name = name, Price = price, Weight = weight });
    }
    
    public void SetShippingStrategy(IShippingStrategy strategy)
    {
        _shippingStrategy = strategy;
    }
    
    public decimal GetTotal()
    {
        return _items.Sum(i => i.Price);
    }
    
    public decimal GetTotalWeight()
    {
        return _items.Sum(i => i.Weight);
    }
    
    public void Checkout()
    {
        if (_shippingStrategy == null)
        {
            Console.WriteLine("Please select a shipping method");
            return;
        }
        
        decimal subtotal = GetTotal();
        decimal weight = GetTotalWeight();
        decimal shippingCost = _shippingStrategy.CalculateShippingCost(subtotal, weight);
        decimal total = subtotal + shippingCost;
        
        Console.WriteLine("===== Order Summary =====");
        Console.WriteLine($"Subtotal: {subtotal:C}");
        Console.WriteLine($"Shipping ({_shippingStrategy.GetShippingMethod()}): {shippingCost:C}");
        Console.WriteLine($"Total: {total:C}");
    }
}

public class CartItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
}

// Real-world example: Payment processing
public interface IPaymentStrategy
{
    bool ProcessPayment(decimal amount);
    string GetPaymentType();
}

public class CreditCardStrategy : IPaymentStrategy
{
    private readonly string _cardNumber;
    
    public CreditCardStrategy(string cardNumber)
    {
        _cardNumber = cardNumber;
    }
    
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing ${amount:C} via Credit Card ending in {_cardNumber.Substring(_cardNumber.Length - 4)}");
        // Actual payment processing logic
        return true;
    }
    
    public string GetPaymentType() => "Credit Card";
}

public class PayPalStrategy : IPaymentStrategy
{
    private readonly string _email;
    
    public PayPalStrategy(string email)
    {
        _email = email;
    }
    
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing ${amount:C} via PayPal account {_email}");
        // PayPal API integration
        return true;
    }
    
    public string GetPaymentType() => "PayPal";
}

// Compression Strategy Example
public interface ICompressionStrategy
{
    byte[] Compress(byte[] data);
    byte[] Decompress(byte[] data);
    string GetCompressionType();
}

public class ZipCompression : ICompressionStrategy
{
    public byte[] Compress(byte[] data)
    {
        Console.WriteLine("Compressing with ZIP algorithm");
        // Actual ZIP compression
        return data;
    }
    
    public byte[] Decompress(byte[] data)
    {
        Console.WriteLine("Decompressing ZIP data");
        return data;
    }
    
    public string GetCompressionType() => "ZIP";
}

public class GzipCompression : ICompressionStrategy
{
    public byte[] Compress(byte[] data)
    {
        Console.WriteLine("Compressing with GZIP algorithm");
        // Actual GZIP compression
        return data;
    }
    
    public byte[] Decompress(byte[] data)
    {
        Console.WriteLine("Decompressing GZIP data");
        return data;
    }
    
    public string GetCompressionType() => "GZIP";
}

public class FileCompressor
{
    private ICompressionStrategy _strategy;
    
    public void SetCompressionStrategy(ICompressionStrategy strategy)
    {
        _strategy = strategy;
    }
    
    public void CompressFile(string filePath)
    {
        if (_strategy == null)
            throw new InvalidOperationException("Compression strategy not set");
            
        // Read file
        byte[] fileData = new byte[] { }; // Simulated
        
        Console.WriteLine($"Compressing {filePath} using {_strategy.GetCompressionType()}");
        byte[] compressed = _strategy.Compress(fileData);
        Console.WriteLine("File compressed successfully");
    }
}

// Sorting Strategy Example
public interface ISortStrategy<T>
{
    void Sort(List<T> list);
    string GetSortAlgorithm();
}

public class QuickSort<T> : ISortStrategy<T> where T : IComparable<T>
{
    public void Sort(List<T> list)
    {
        Console.WriteLine("Sorting using QuickSort");
        list.Sort(); // Simplified
    }
    
    public string GetSortAlgorithm() => "QuickSort";
}

public class MergeSort<T> : ISortStrategy<T> where T : IComparable<T>
{
    public void Sort(List<T> list)
    {
        Console.WriteLine("Sorting using MergeSort");
        list.Sort(); // Simplified
    }
    
    public string GetSortAlgorithm() => "MergeSort";
}

public class DataSorter<T> where T : IComparable<T>
{
    private ISortStrategy<T> _strategy;
    
    public void SetStrategy(ISortStrategy<T> strategy)
    {
        _strategy = strategy;
    }
    
    public void SortData(List<T> data)
    {
        Console.WriteLine($"Using {_strategy.GetSortAlgorithm()} to sort {data.Count} items");
        _strategy.Sort(data);
    }
}
```

**Example Usage:**
```csharp
// Shipping example
var cart = new ShoppingCart();
cart.AddItem("Laptop", 1200m, 2.5m);
cart.AddItem("Mouse", 25m, 0.2m);

// Try different shipping strategies
cart.SetShippingStrategy(new StandardShipping());
cart.Checkout();

Console.WriteLine();

cart.SetShippingStrategy(new ExpressShipping());
cart.Checkout();

Console.WriteLine();

cart.SetShippingStrategy(new FreeShipping(100m));
cart.Checkout();

// Payment example
IPaymentStrategy payment = new CreditCardStrategy("1234567890123456");
payment.ProcessPayment(500m);

// Compression example
var compressor = new FileCompressor();
compressor.SetCompressionStrategy(new ZipCompression());
compressor.CompressFile("document.pdf");

compressor.SetCompressionStrategy(new GzipCompression());
compressor.CompressFile("archive.tar");
```

**When to Use Strategy Pattern:**
- Multiple algorithms for a specific task
- Need to switch algorithms at runtime
- Want to isolate algorithm implementation details
- Avoid conditional statements for selecting behavior

**Benefits:**
- Open/Closed Principle
- Single Responsibility Principle
- Eliminates conditional statements
- Easy to add new strategies

---

### 6.3 SOLID Principle Coding Examples

**Description:**
SOLID principles are fundamental to writing maintainable, scalable object-oriented code.

**1. Single Responsibility Principle (SRP)**
```csharp
// ❌ Bad: Multiple responsibilities
public class UserService
{
    public void CreateUser(string name, string email)
    {
        // Validate user
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name is required");
            
        // Save to database
        Console.WriteLine($"Saving user {name} to database");
        
        // Send email
        Console.WriteLine($"Sending welcome email to {email}");
        
        // Log activity
        Console.WriteLine($"User {name} created at {DateTime.Now}");
    }
}

// ✅ Good: Single responsibility per class
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UserValidator
{
    public void Validate(User user)
    {
        if (string.IsNullOrEmpty(user.Name))
            throw new ArgumentException("Name is required");
        if (string.IsNullOrEmpty(user.Email))
            throw new ArgumentException("Email is required");
    }
}

public class UserRepository
{
    public void Save(User user)
    {
        Console.WriteLine($"Saving user {user.Name} to database");
    }
}

public class EmailService
{
    public void SendWelcomeEmail(string email)
    {
        Console.WriteLine($"Sending welcome email to {email}");
    }
}

public class ActivityLogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now}] {message}");
    }
}

public class UserServiceGood
{
    private readonly UserValidator _validator;
    private readonly UserRepository _repository;
    private readonly EmailService _emailService;
    private readonly ActivityLogger _logger;
    
    public UserServiceGood(
        UserValidator validator,
        UserRepository repository,
        EmailService emailService,
        ActivityLogger logger)
    {
        _validator = validator;
        _repository = repository;
        _emailService = emailService;
        _logger = logger;
    }
    
    public void CreateUser(User user)
    {
        _validator.Validate(user);
        _repository.Save(user);
        _emailService.SendWelcomeEmail(user.Email);
        _logger.Log($"User {user.Name} created");
    }
}
```

**2. Open/Closed Principle (OCP)**
```csharp
// ❌ Bad: Modified for each new shape
public class AreaCalculator
{
    public double CalculateArea(object shape)
    {
        if (shape is Rectangle rectangle)
        {
            return rectangle.Width * rectangle.Height;
        }
        else if (shape is Circle circle)
        {
            return Math.PI * circle.Radius * circle.Radius;
        }
        // Need to modify class to add new shape
        else if (shape is Triangle triangle)
        {
            return 0.5 * triangle.Base * triangle.Height;
        }
        
        throw new ArgumentException("Unknown shape");
    }
}

// ✅ Good: Open for extension, closed for modification
public interface IShape
{
    double CalculateArea();
}

public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public double CalculateArea() => Width * Height;
}

public class Circle : IShape
{
    public double Radius { get; set; }
    
    public double CalculateArea() => Math.PI * Radius * Radius;
}

public class Triangle : IShape
{
    public double Base { get; set; }
    public double Height { get; set; }
    
    public double CalculateArea() => 0.5 * Base * Height;
}

public class AreaCalculatorGood
{
    public double CalculateTotalArea(List<IShape> shapes)
    {
        return shapes.Sum(shape => shape.CalculateArea());
    }
}
```

**3. Liskov Substitution Principle (LSP)**
```csharp
// ❌ Bad: Violates LSP
public class Bird
{
    public virtual void Fly()
    {
        Console.WriteLine("Flying...");
    }
}

public class Penguin : Bird
{
    public override void Fly()
    {
        throw new NotSupportedException("Penguins can't fly!"); // Breaks LSP
    }
}

// ✅ Good: Follows LSP
public abstract class Bird
{
    public abstract void Move();
}

public interface IFlyable
{
    void Fly();
}

public class Sparrow : Bird, IFlyable
{
    public override void Move()
    {
        Fly();
    }
    
    public void Fly()
    {
        Console.WriteLine("Sparrow flying...");
    }
}

public class Penguin : Bird
{
    public override void Move()
    {
        Swim();
    }
    
    public void Swim()
    {
        Console.WriteLine("Penguin swimming...");
    }
}

// Real-world example: Payment processing
public abstract class PaymentProcessor
{
    public abstract bool ProcessPayment(decimal amount);
    public abstract bool CanRefund();
}

public class CreditCardProcessor : PaymentProcessor
{
    public override bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing credit card payment: {amount:C}");
        return true;
    }
    
    public override bool CanRefund() => true;
}

public class GiftCardProcessor : PaymentProcessor
{
    public override bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing gift card payment: {amount:C}");
        return true;
    }
    
    public override bool CanRefund() => false; // Gift cards can't be refunded
}
```

**4. Interface Segregation Principle (ISP)**
```csharp
// ❌ Bad: Fat interface
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}

public class Human : IWorker
{
    public void Work() => Console.WriteLine("Working...");
    public void Eat() => Console.WriteLine("Eating...");
    public void Sleep() => Console.WriteLine("Sleeping...");
}

public class Robot : IWorker
{
    public void Work() => Console.WriteLine("Working...");
    public void Eat() => throw new NotImplementedException(); // Doesn't eat!
    public void Sleep() => throw new NotImplementedException(); // Doesn't sleep!
}

// ✅ Good: Segregated interfaces
public interface IWorkable
{
    void Work();
}

public interface IFeedable
{
    void Eat();
}

public interface ISleepable
{
    void Sleep();
}

public class Human : IWorkable, IFeedable, ISleepable
{
    public void Work() => Console.WriteLine("Human working...");
    public void Eat() => Console.WriteLine("Human eating...");
    public void Sleep() => Console.WriteLine("Human sleeping...");
}

public class Robot : IWorkable
{
    public void Work() => Console.WriteLine("Robot working...");
}

// Real-world example: Document management
public interface IReadable
{
    string Read();
}

public interface IWritable
{
    void Write(string content);
}

public interface IPrintable
{
    void Print();
}

public class Document : IReadable, IWritable, IPrintable
{
    public string Content { get; set; }
    
    public string Read() => Content;
    public void Write(string content) => Content = content;
    public void Print() => Console.WriteLine($"Printing: {Content}");
}

public class ReadOnlyDocument : IReadable
{
    public string Content { get; set; }
    
    public string Read() => Content;
}
```

**5. Dependency Inversion Principle (DIP)**
```csharp
// ❌ Bad: High-level depends on low-level
public class EmailSender
{
    public void SendEmail(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to}");
    }
}

public class NotificationService
{
    private EmailSender _emailSender = new EmailSender(); // Tight coupling
    
    public void Notify(string message)
    {
        _emailSender.SendEmail("user@example.com", "Notification", message);
    }
}

// ✅ Good: Both depend on abstraction
public interface IMessageSender
{
    void Send(string to, string subject, string body);
}

public class EmailSender : IMessageSender
{
    public void Send(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to}: {subject}");
    }
}

public class SmsSender : IMessageSender
{
    public void Send(string to, string subject, string body)
    {
        Console.WriteLine($"Sending SMS to {to}: {body}");
    }
}

public class PushNotificationSender : IMessageSender
{
    public void Send(string to, string subject, string body)
    {
        Console.WriteLine($"Sending push notification to {to}: {body}");
    }
}

public class NotificationServiceGood
{
    private readonly IMessageSender _messageSender;
    
    public NotificationServiceGood(IMessageSender messageSender)
    {
        _messageSender = messageSender; // Dependency injection
    }
    
    public void Notify(string to, string message)
    {
        _messageSender.Send(to, "Notification", message);
    }
}

// Real-world example: Data access
public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

public class SqlRepository<T> : IRepository<T>
{
    public async Task<T> GetByIdAsync(int id)
    {
        Console.WriteLine($"Getting {typeof(T).Name} from SQL database");
        await Task.Delay(10);
        return default(T);
    }
    
    public async Task<List<T>> GetAllAsync()
    {
        Console.WriteLine($"Getting all {typeof(T).Name} from SQL database");
        await Task.Delay(10);
        return new List<T>();
    }
    
    public async Task AddAsync(T entity)
    {
        Console.WriteLine($"Adding {typeof(T).Name} to SQL database");
        await Task.Delay(10);
    }
    
    public async Task UpdateAsync(T entity)
    {
        Console.WriteLine($"Updating {typeof(T).Name} in SQL database");
        await Task.Delay(10);
    }
    
    public async Task DeleteAsync(int id)
    {
        Console.WriteLine($"Deleting {typeof(T).Name} from SQL database");
        await Task.Delay(10);
    }
}

public class ProductService
{
    private readonly IRepository<Product> _repository;
    
    public ProductService(IRepository<Product> repository)
    {
        _repository = repository;
    }
    
    public async Task<Product> GetProductAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

**Example Usage:**
```csharp
// SRP
var validator = new UserValidator();
var repository = new UserRepository();
var emailService = new EmailService();
var logger = new ActivityLogger();
var userService = new UserServiceGood(validator, repository, emailService, logger);

var user = new User { Name = "John Doe", Email = "john@example.com" };
userService.CreateUser(user);

// OCP
var shapes = new List<IShape>
{
    new Rectangle { Width = 5, Height = 10 },
    new Circle { Radius = 7 },
    new Triangle { Base = 6, Height = 8 }
};

var calculator = new AreaCalculatorGood();
double totalArea = calculator.CalculateTotalArea(shapes);

// DIP
IMessageSender emailSender = new EmailSender();
var notificationService = new NotificationServiceGood(emailSender);
notificationService.Notify("user@example.com", "Hello from SOLID!");
```

---

### 6.4 Create Immutable Class

**Description:**
Immutable classes cannot be modified after creation. Essential for thread safety, caching, and functional programming.

**Implementation:**

```csharp
// Basic Immutable Class
public class ImmutablePerson
{
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime DateOfBirth { get; }
    
    public ImmutablePerson(string firstName, string lastName, DateTime dateOfBirth)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        DateOfBirth = dateOfBirth;
    }
    
    // Return new instance for modifications
    public ImmutablePerson WithFirstName(string firstName)
    {
        return new ImmutablePerson(firstName, LastName, DateOfBirth);
    }
    
    public ImmutablePerson WithLastName(string lastName)
    {
        return new ImmutablePerson(FirstName, lastName, DateOfBirth);
    }
}

// Immutable with Collection
public class ImmutableOrder
{
    public int OrderId { get; }
    public DateTime OrderDate { get; }
    public IReadOnlyList<OrderItem> Items { get; }
    public decimal TotalAmount { get; }
    
    public ImmutableOrder(int orderId, DateTime orderDate, IEnumerable<OrderItem> items)
    {
        OrderId = orderId;
        OrderDate = orderDate;
        // Create defensive copy
        Items = new List<OrderItem>(items).AsReadOnly();
        TotalAmount = Items.Sum(i => i.Price * i.Quantity);
    }
    
    public ImmutableOrder AddItem(OrderItem item)
    {
        var newItems = Items.ToList();
        newItems.Add(item);
        return new ImmutableOrder(OrderId, OrderDate, newItems);
    }
}

public class OrderItem
{
    public string ProductName { get; }
    public decimal Price { get; }
    public int Quantity { get; }
    
    public OrderItem(string productName, decimal price, int quantity)
    {
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }
}

// Using C# 9+ Records (Best approach for immutability)
public record PersonRecord(string FirstName, string LastName, DateTime DateOfBirth)
{
    public int Age => DateTime.Now.Year - DateOfBirth.Year;
}

// Record with additional logic
public record BankAccount(string AccountNumber, decimal Balance, string Owner)
{
    // Derived property
    public string AccountType => Balance >= 10000 ? "Premium" : "Standard";
    
    // With expressions for creating modified copies
    public BankAccount Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");
            
        return this with { Balance = Balance + amount };
    }
    
    public BankAccount Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");
        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds");
            
        return this with { Balance = Balance - amount };
    }
}

// Immutable Complex Object
public sealed class ImmutableAddress
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string Country { get; }
    
    public ImmutableAddress(
        string street,
        string city,
        string state,
        string zipCode,
        string country)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        City = city ?? throw new ArgumentNullException(nameof(city));
        State = state ?? throw new ArgumentNullException(nameof(state));
        ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
        Country = country ?? throw new ArgumentNullException(nameof(country));
    }
    
    public override string ToString()
    {
        return $"{Street}, {City}, {State} {ZipCode}, {Country}";
    }
    
    public override bool Equals(object obj)
    {
        if (obj is not ImmutableAddress other)
            return false;
            
        return Street == other.Street &&
               City == other.City &&
               State == other.State &&
               ZipCode == other.ZipCode &&
               Country == other.Country;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Street, City, State, ZipCode, Country);
    }
}

// Immutable with Builder Pattern
public class ImmutableUser
{
    public int Id { get; }
    public string Username { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime CreatedAt { get; }
    public IReadOnlyList<string> Roles { get; }
    
    private ImmutableUser(Builder builder)
    {
        Id = builder.Id;
        Username = builder.Username;
        Email = builder.Email;
        FirstName = builder.FirstName;
        LastName = builder.LastName;
        CreatedAt = builder.CreatedAt;
        Roles = new List<string>(builder.Roles).AsReadOnly();
    }
    
    public class Builder
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<string> Roles { get; set; } = new List<string>();
        
        public Builder WithId(int id)
        {
            Id = id;
            return this;
        }
        
        public Builder WithUsername(string username)
        {
            Username = username;
            return this;
        }
        
        public Builder WithEmail(string email)
        {
            Email = email;
            return this;
        }
        
        public Builder WithName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            return this;
        }
        
        public Builder WithRole(string role)
        {
            Roles.Add(role);
            return this;
        }
        
        public ImmutableUser Build()
        {
            // Validation
            if (string.IsNullOrEmpty(Username))
                throw new InvalidOperationException("Username is required");
            if (string.IsNullOrEmpty(Email))
                throw new InvalidOperationException("Email is required");
                
            return new ImmutableUser(this);
        }
    }
}

// Thread-safe Singleton with Immutable Configuration
public sealed class AppConfiguration
{
    private static readonly Lazy<AppConfiguration> _instance = 
        new Lazy<AppConfiguration>(() => new AppConfiguration());
    
    public string DatabaseConnection { get; }
    public string ApiKey { get; }
    public int MaxRetries { get; }
    public TimeSpan Timeout { get; }
    
    private AppConfiguration()
    {
        // Load from config file
        DatabaseConnection = "Server=localhost;Database=myapp";
        ApiKey = "api-key-12345";
        MaxRetries = 3;
        Timeout = TimeSpan.FromSeconds(30);
    }
    
    public static AppConfiguration Instance => _instance.Value;
}
```

**Example Usage:**
```csharp
// Basic immutable class
var person = new ImmutablePerson("John", "Doe", new DateTime(1990, 1, 1));
var updatedPerson = person.WithFirstName("Jane"); // Creates new instance

// Record (C# 9+)
var personRecord = new PersonRecord("John", "Doe", new DateTime(1990, 1, 1));
var updated = personRecord with { FirstName = "Jane" }; // Non-destructive mutation

// Bank account
var account = new BankAccount("123456", 1000m, "John Doe");
var afterDeposit = account.Deposit(500m);
Console.WriteLine($"Original: {account.Balance:C}"); // $1,000.00
Console.WriteLine($"After deposit: {afterDeposit.Balance:C}"); // $1,500.00

// Immutable collection
var items = new List<OrderItem>
{
    new OrderItem("Product A", 10m, 2),
    new OrderItem("Product B", 20m, 1)
};

var order = new ImmutableOrder(1, DateTime.Now, items);
var orderWithNewItem = order.AddItem(new OrderItem("Product C", 15m, 3));

// Builder pattern
var user = new ImmutableUser.Builder()
    .WithId(1)
    .WithUsername("johndoe")
    .WithEmail("john@example.com")
    .WithName("John", "Doe")
    .WithRole("Admin")
    .WithRole("User")
    .Build();
```

**Key Principles:**
1. All fields are `readonly` or have only getters
2. Initialize all fields in constructor
3. No setters or mutating methods
4. Return new instances for modifications
5. Make defensive copies of collections
6. Mark class as `sealed` if possible

---

### 6.5 Benefits of Immutability in Multithreaded Apps

**Description:**
Immutability eliminates race conditions, simplifies concurrent code, and improves application reliability.

**Benefits and Examples:**

```csharp
// 1. Thread Safety Without Locks
public class ThreadSafetyDemo
{
    // ❌ Mutable (requires locks)
    public class MutableCounter
    {
        private int _count;
        private readonly object _lock = new object();
        
        public void Increment()
        {
            lock (_lock) // Need lock!
            {
                _count++;
            }
        }
        
        public int GetCount()
        {
            lock (_lock) // Need lock!
            {
                return _count;
            }
        }
    }
    
    // ✅ Immutable (no locks needed)
    public record ImmutableCounter(int Count)
    {
        public ImmutableCounter Increment()
        {
            return this with { Count = Count + 1 };
        }
    }
}

// 2. Safe Sharing Across Threads
public class DataSharingDemo
{
    // ✅ Safe to share immutable data
    public record UserData(int Id, string Name, string Email);
    
    public void ProcessUserDataSafely(UserData userData)
    {
        // Can safely pass to multiple threads
        var tasks = new List<Task>();
        
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                // No race conditions - userData can't change
                Console.WriteLine($"Processing user: {userData.Name}");
                Task.Delay(100).Wait();
                Console.WriteLine($"Completed for: {userData.Name}");
            }));
        }
        
        Task.WaitAll(tasks.ToArray());
    }
}

// 3. Cache Safely
public class CachingDemo
{
    private readonly ConcurrentDictionary<int, UserProfile> _cache = 
        new ConcurrentDictionary<int, UserProfile>();
    
    // ✅ Immutable - safe to cache
    public record UserProfile(
        int Id,
        string Name,
        string Email,
        DateTime LastLoginDate,
        IReadOnlyList<string> Permissions);
    
    public UserProfile GetUserProfile(int userId)
    {
        return _cache.GetOrAdd(userId, id =>
        {
            // Load from database
            var permissions = new List<string> { "Read", "Write" }.AsReadOnly();
            return new UserProfile(
                id,
                $"User{id}",
                $"user{id}@example.com",
                DateTime.UtcNow,
                permissions);
        });
        
        // Safe to return cached instance - it can't be modified
    }
}

// 4. Easier Reasoning About Code
public class StateManagementDemo
{
    // ✅ Clear state transitions with immutable objects
    public record GameState(
        int Level,
        int Score,
        int Lives,
        IReadOnlyList<string> Inventory)
    {
        public GameState LevelUp()
        {
            return this with { Level = Level + 1, Score = Score + 1000 };
        }
        
        public GameState AddScore(int points)
        {
            return this with { Score = Score + points };
        }
        
        public GameState LoseLife()
        {
            return this with { Lives = Lives - 1 };
        }
        
        public GameState AddItem(string item)
        {
            var newInventory = Inventory.ToList();
            newInventory.Add(item);
            return this with { Inventory = newInventory.AsReadOnly() };
        }
    }
    
    public void PlayGame()
    {
        GameState state = new GameState(
            Level: 1,
            Score: 0,
            Lives: 3,
            Inventory: new List<string>().AsReadOnly());
        
        // Each operation creates new state - easy to track changes
        state = state.AddScore(100);
        state = state.AddItem("Sword");
        state = state.LevelUp();
        state = state.LoseLife();
        
        Console.WriteLine($"Level: {state.Level}, Score: {state.Score}, Lives: {state.Lives}");
    }
}

// 5. Undo/Redo Functionality
public class UndoRedoDemo
{
    private Stack<DocumentState> _undoStack = new Stack<DocumentState>();
    private Stack<DocumentState> _redoStack = new Stack<DocumentState>();
    private DocumentState _currentState;
    
    public record DocumentState(string Content, int CursorPosition);
    
    public UndoRedoDemo()
    {
        _currentState = new DocumentState("", 0);
    }
    
    public void EditDocument(string newContent, int cursorPosition)
    {
        _undoStack.Push(_currentState);
        _currentState = new DocumentState(newContent, cursorPosition);
        _redoStack.Clear(); // Clear redo stack on new edit
    }
    
    public void Undo()
    {
        if (_undoStack.Count > 0)
        {
            _redoStack.Push(_currentState);
            _currentState = _undoStack.Pop();
        }
    }
    
    public void Redo()
    {
        if (_redoStack.Count > 0)
        {
            _undoStack.Push(_currentState);
            _currentState = _redoStack.Pop();
        }
    }
    
    public string GetContent() => _currentState.Content;
}

// 6. Event Sourcing Pattern
public class EventSourcingDemo
{
    public record OrderCreatedEvent(int OrderId, DateTime CreatedAt);
    public record ItemAddedEvent(int OrderId, string ProductId, int Quantity);
    public record OrderShippedEvent(int OrderId, string TrackingNumber);
    
    public record OrderState(
        int OrderId,
        DateTime CreatedAt,
        IReadOnlyList<OrderLineItem> Items,
        OrderStatus Status,
        string TrackingNumber)
    {
        public static OrderState Apply(OrderState state, object @event)
        {
            return @event switch
            {
                OrderCreatedEvent e => new OrderState(
                    e.OrderId,
                    e.CreatedAt,
                    new List<OrderLineItem>().AsReadOnly(),
                    OrderStatus.Created,
                    null),
                    
                ItemAddedEvent e => state with
                {
                    Items = state.Items
                        .Append(new OrderLineItem(e.ProductId, e.Quantity))
                        .ToList()
                        .AsReadOnly()
                },
                
                OrderShippedEvent e => state with
                {
                    Status = OrderStatus.Shipped,
                    TrackingNumber = e.TrackingNumber
                },
                
                _ => state
            };
        }
    }
    
    public record OrderLineItem(string ProductId, int Quantity);
    
    public enum OrderStatus
    {
        Created,
        Processing,
        Shipped,
        Delivered
    }
}

// 7. Parallel Processing Safety
public class ParallelProcessingDemo
{
    public record Product(int Id, string Name, decimal Price, int Stock);
    
    public List<Product> ApplyDiscountParallel(List<Product> products, decimal discountPercent)
    {
        // ✅ Safe to process in parallel - no shared mutable state
        return products
            .AsParallel()
            .Select(p => p with { Price = p.Price * (1 - discountPercent / 100) })
            .ToList();
    }
    
    public record OrderSummary(int OrderId, decimal Total, int ItemCount);
    
    public List<OrderSummary> ProcessOrdersParallel(List<ImmutableOrder> orders)
    {
        // ✅ Safe parallel processing
        return orders
            .AsParallel()
            .Select(order => new OrderSummary(
                order.OrderId,
                order.TotalAmount,
                order.Items.Count))
            .ToList();
    }
}

// 8. Hash Code Stability
public class HashCodeStabilityDemo
{
    // ✅ Immutable - hash code never changes
    public record CacheKey(string UserId, string ResourceType, DateTime Date);
    
    private Dictionary<CacheKey, object> _cache = new Dictionary<CacheKey, object>();
    
    public void DemoHashCodeStability()
    {
        var key = new CacheKey("user123", "Profile", DateTime.Today);
        
        _cache[key] = "Profile Data";
        
        // ✅ Can always find the data because hash code is stable
        var data = _cache[key];
        Console.WriteLine($"Found: {data}");
    }
}

// 9. Functional Programming Style
public class FunctionalStyleDemo
{
    public record Money(decimal Amount, string Currency)
    {
        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Currency mismatch");
                
            return this with { Amount = Amount + other.Amount };
        }
        
        public Money Subtract(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Currency mismatch");
                
            return this with { Amount = Amount - other.Amount };
        }
        
        public Money Multiply(decimal factor)
        {
            return this with { Amount = Amount * factor };
        }
    }
    
    public Money CalculateTotalWithTax(Money subtotal, decimal taxRate)
    {
        var tax = subtotal.Multiply(taxRate);
        return subtotal.Add(tax);
    }
}
```

**Key Benefits Summary:**

| Benefit | Description | Impact |
|---------|-------------|---------|
| **Thread Safety** | No race conditions, no locks needed | High performance |
| **Simplified Debugging** | State doesn't change unexpectedly | Easier troubleshooting |
| **Safe Caching** | Objects can be cached without concerns | Better performance |
| **Predictable Behavior** | Functions are deterministic | Fewer bugs |
| **Easy Testing** | No side effects to worry about | Simpler tests |
| **Temporal Queries** | Can keep old states | History tracking |
| **Hash Code Stability** | Safe to use as dictionary keys | Reliable collections |
| **Parallel Processing** | No synchronization needed | Scales better |

**When to Use Immutability:**
- ✅ Configuration objects
- ✅ Domain entities
- ✅ DTOs (Data Transfer Objects)
- ✅ Cache keys and values
- ✅ Shared state across threads
- ✅ Event sourcing
- ✅ Value objects

**When Mutability Might Be Better:**
- ⚠️ Large collections with frequent updates (performance)
- ⚠️ Builder patterns (during construction)
- ⚠️ Game engines with frequent state updates
- ⚠️ Real-time systems with tight performance constraints

---

## 7. Data Structures & Algorithms

### 7.1 Find Missing Number

**Problem:**
Given an array containing n distinct numbers from 0 to n, find the one that is missing from the array.

**Example:**
```
Input: [3, 0, 1]
Output: 2

Input: [9,6,4,2,3,5,7,0,1]
Output: 8
```

**Solution 1: Using Math (Sum Formula)**
```csharp
public class MissingNumberFinder
{
    // Time: O(n), Space: O(1)
    public static int FindMissingNumber(int[] nums)
    {
        int n = nums.Length;
        // Sum of first n natural numbers: n * (n + 1) / 2
        int expectedSum = n * (n + 1) / 2;
        int actualSum = nums.Sum();
        
        return expectedSum - actualSum;
    }
    
    public static void Example()
    {
        int[] nums1 = { 3, 0, 1 };
        Console.WriteLine($"Missing number: {FindMissingNumber(nums1)}"); // 2
        
        int[] nums2 = { 9, 6, 4, 2, 3, 5, 7, 0, 1 };
        Console.WriteLine($"Missing number: {FindMissingNumber(nums2)}"); // 8
    }
}
```

**Solution 2: Using XOR (Bit Manipulation)**
```csharp
public class MissingNumberFinderXOR
{
    // Time: O(n), Space: O(1)
    // XOR properties: a ^ a = 0, a ^ 0 = a
    public static int FindMissingNumber(int[] nums)
    {
        int result = nums.Length;
        
        for (int i = 0; i < nums.Length; i++)
        {
            result ^= i ^ nums[i];
        }
        
        return result;
    }
}
```

**Solution 3: Using HashSet**
```csharp
public class MissingNumberFinderHashSet
{
    // Time: O(n), Space: O(n)
    public static int FindMissingNumber(int[] nums)
    {
        HashSet<int> numSet = new HashSet<int>(nums);
        
        for (int i = 0; i <= nums.Length; i++)
        {
            if (!numSet.Contains(i))
            {
                return i;
            }
        }
        
        return -1; // Should never reach here
    }
}
```

**Comparison:**

| Approach | Time Complexity | Space Complexity | Pros | Cons |
|----------|----------------|------------------|------|------|
| Math Formula | O(n) | O(1) | Simple, efficient | May overflow for large n |
| XOR | O(n) | O(1) | No overflow risk | Less intuitive |
| HashSet | O(n) | O(n) | Easy to understand | Extra space |

---

### 7.2 Rotate Array

**Problem:**
Rotate an array to the right by k steps.

**Example:**
```
Input: nums = [1,2,3,4,5,6,7], k = 3
Output: [5,6,7,1,2,3,4]

Input: nums = [-1,-100,3,99], k = 2
Output: [3,99,-1,-100]
```

**Solution 1: Using Reverse (In-Place)**
```csharp
public class ArrayRotator
{
    // Time: O(n), Space: O(1)
    public static void Rotate(int[] nums, int k)
    {
        if (nums == null || nums.Length == 0) return;
        
        k = k % nums.Length; // Handle k > nums.Length
        
        // Reverse entire array
        Reverse(nums, 0, nums.Length - 1);
        
        // Reverse first k elements
        Reverse(nums, 0, k - 1);
        
        // Reverse remaining elements
        Reverse(nums, k, nums.Length - 1);
    }
    
    private static void Reverse(int[] nums, int start, int end)
    {
        while (start < end)
        {
            int temp = nums[start];
            nums[start] = nums[end];
            nums[end] = temp;
            start++;
            end--;
        }
    }
    
    public static void Example()
    {
        int[] nums = { 1, 2, 3, 4, 5, 6, 7 };
        Rotate(nums, 3);
        Console.WriteLine(string.Join(", ", nums)); // 5, 6, 7, 1, 2, 3, 4
    }
}
```

**Solution 2: Using Extra Array**
```csharp
public class ArrayRotatorExtraSpace
{
    // Time: O(n), Space: O(n)
    public static int[] Rotate(int[] nums, int k)
    {
        if (nums == null || nums.Length == 0) return nums;
        
        int n = nums.Length;
        k = k % n;
        int[] result = new int[n];
        
        for (int i = 0; i < n; i++)
        {
            result[(i + k) % n] = nums[i];
        }
        
        return result;
    }
}
```

**Solution 3: Using LINQ**
```csharp
public class ArrayRotatorLINQ
{
    public static int[] Rotate(int[] nums, int k)
    {
        if (nums == null || nums.Length == 0) return nums;
        
        int n = nums.Length;
        k = k % n;
        
        return nums.Skip(n - k).Concat(nums.Take(n - k)).ToArray();
    }
}
```

**Step-by-Step Example (Reverse Method):**
```
Original: [1, 2, 3, 4, 5, 6, 7], k = 3

Step 1 - Reverse all: [7, 6, 5, 4, 3, 2, 1]
Step 2 - Reverse first k: [5, 6, 7, 4, 3, 2, 1]
Step 3 - Reverse rest: [5, 6, 7, 1, 2, 3, 4]
```

---

### 7.3 Merge Two Sorted Arrays

**Problem:**
Merge two sorted arrays into one sorted array.

**Example:**
```
Input: nums1 = [1,2,3,0,0,0], m = 3, nums2 = [2,5,6], n = 3
Output: [1,2,2,3,5,6]

Input: nums1 = [1], m = 1, nums2 = [], n = 0
Output: [1]
```

**Solution 1: Merge from End (In-Place)**
```csharp
public class ArrayMerger
{
    // Time: O(m + n), Space: O(1)
    // Merge nums2 into nums1 (nums1 has enough space)
    public static void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        int i = m - 1;      // Last element in nums1
        int j = n - 1;      // Last element in nums2
        int k = m + n - 1;  // Last position in nums1
        
        while (j >= 0)
        {
            if (i >= 0 && nums1[i] > nums2[j])
            {
                nums1[k--] = nums1[i--];
            }
            else
            {
                nums1[k--] = nums2[j--];
            }
        }
    }
    
    public static void Example()
    {
        int[] nums1 = { 1, 2, 3, 0, 0, 0 };
        int[] nums2 = { 2, 5, 6 };
        
        Merge(nums1, 3, nums2, 3);
        Console.WriteLine(string.Join(", ", nums1)); // 1, 2, 2, 3, 5, 6
    }
}
```

**Solution 2: Create New Array**
```csharp
public class ArrayMergerNewArray
{
    // Time: O(m + n), Space: O(m + n)
    public static int[] Merge(int[] nums1, int[] nums2)
    {
        int m = nums1.Length;
        int n = nums2.Length;
        int[] result = new int[m + n];
        
        int i = 0, j = 0, k = 0;
        
        while (i < m && j < n)
        {
            if (nums1[i] <= nums2[j])
            {
                result[k++] = nums1[i++];
            }
            else
            {
                result[k++] = nums2[j++];
            }
        }
        
        // Copy remaining elements
        while (i < m) result[k++] = nums1[i++];
        while (j < n) result[k++] = nums2[j++];
        
        return result;
    }
}
```

**Solution 3: Using LINQ**
```csharp
public class ArrayMergerLINQ
{
    public static int[] Merge(int[] nums1, int[] nums2)
    {
        return nums1.Concat(nums2).OrderBy(x => x).ToArray();
    }
    
    // More efficient for already sorted arrays
    public static int[] MergeSorted(int[] nums1, int[] nums2)
    {
        var merged = new List<int>();
        int i = 0, j = 0;
        
        while (i < nums1.Length && j < nums2.Length)
        {
            if (nums1[i] <= nums2[j])
                merged.Add(nums1[i++]);
            else
                merged.Add(nums2[j++]);
        }
        
        merged.AddRange(nums1.Skip(i));
        merged.AddRange(nums2.Skip(j));
        
        return merged.ToArray();
    }
}
```

---

### 7.4 Find Duplicate Elements

**Problem:**
Find all duplicate elements in an array.

**Example:**
```
Input: [4,3,2,7,8,2,3,1]
Output: [2,3]

Input: [1,1,2]
Output: [1]
```

**Solution 1: Using HashSet**
```csharp
public class DuplicateFinder
{
    // Time: O(n), Space: O(n)
    public static List<int> FindDuplicates(int[] nums)
    {
        HashSet<int> seen = new HashSet<int>();
        HashSet<int> duplicates = new HashSet<int>();
        
        foreach (int num in nums)
        {
            if (!seen.Add(num))
            {
                duplicates.Add(num);
            }
        }
        
        return duplicates.ToList();
    }
    
    public static void Example()
    {
        int[] nums = { 4, 3, 2, 7, 8, 2, 3, 1 };
        var duplicates = FindDuplicates(nums);
        Console.WriteLine(string.Join(", ", duplicates)); // 2, 3
    }
}
```

**Solution 2: Using LINQ**
```csharp
public class DuplicateFinderLINQ
{
    public static List<int> FindDuplicates(int[] nums)
    {
        return nums.GroupBy(x => x)
                   .Where(g => g.Count() > 1)
                   .Select(g => g.Key)
                   .ToList();
    }
    
    // With count of occurrences
    public static Dictionary<int, int> FindDuplicatesWithCount(int[] nums)
    {
        return nums.GroupBy(x => x)
                   .Where(g => g.Count() > 1)
                   .ToDictionary(g => g.Key, g => g.Count());
    }
}
```

**Solution 3: Using Dictionary**
```csharp
public class DuplicateFinderDictionary
{
    // Time: O(n), Space: O(n)
    // Returns duplicates with their frequencies
    public static Dictionary<int, int> FindDuplicatesWithFrequency(int[] nums)
    {
        Dictionary<int, int> frequency = new Dictionary<int, int>();
        
        foreach (int num in nums)
        {
            if (frequency.ContainsKey(num))
                frequency[num]++;
            else
                frequency[num] = 1;
        }
        
        return frequency.Where(kvp => kvp.Value > 1)
                       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
```

**Solution 4: In-Place (Array values in range [1, n])**
```csharp
public class DuplicateFinderInPlace
{
    // Time: O(n), Space: O(1)
    // Works only when array contains values in range [1, n]
    public static List<int> FindDuplicates(int[] nums)
    {
        List<int> result = new List<int>();
        
        foreach (int num in nums)
        {
            int index = Math.Abs(num) - 1;
            
            if (nums[index] < 0)
            {
                result.Add(Math.Abs(num));
            }
            else
            {
                nums[index] = -nums[index];
            }
        }
        
        // Restore original array
        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = Math.Abs(nums[i]);
        }
        
        return result;
    }
}
```

---

### 7.5 Two-Sum Problem

**Problem:**
Given an array of integers and a target sum, return indices of two numbers that add up to the target.

**Example:**
```
Input: nums = [2,7,11,15], target = 9
Output: [0,1]
Explanation: nums[0] + nums[1] = 2 + 7 = 9

Input: nums = [3,2,4], target = 6
Output: [1,2]
```

**Solution 1: Using Dictionary (Optimal)**
```csharp
public class TwoSumSolver
{
    // Time: O(n), Space: O(n)
    public static int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();
        
        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];
            
            if (map.ContainsKey(complement))
            {
                return new int[] { map[complement], i };
            }
            
            map[nums[i]] = i;
        }
        
        return new int[] { -1, -1 };
    }
    
    public static void Example()
    {
        int[] nums = { 2, 7, 11, 15 };
        int target = 9;
        
        int[] result = TwoSum(nums, target);
        Console.WriteLine($"Indices: [{result[0]}, {result[1]}]"); // [0, 1]
    }
}
```

**Solution 2: Brute Force**
```csharp
public class TwoSumBruteForce
{
    // Time: O(n²), Space: O(1)
    public static int[] TwoSum(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    return new int[] { i, j };
                }
            }
        }
        
        return new int[] { -1, -1 };
    }
}
```

**Solution 3: Two Pointers (For Sorted Array)**
```csharp
public class TwoSumTwoPointers
{
    // Time: O(n), Space: O(1)
    // Works only if array is sorted or you can sort it
    public static int[] TwoSum(int[] nums, int target)
    {
        // Create array of (value, originalIndex) pairs
        var indexed = nums.Select((value, index) => (value, index))
                         .OrderBy(x => x.value)
                         .ToArray();
        
        int left = 0;
        int right = indexed.Length - 1;
        
        while (left < right)
        {
            int sum = indexed[left].value + indexed[right].value;
            
            if (sum == target)
            {
                return new int[] 
                { 
                    Math.Min(indexed[left].index, indexed[right].index),
                    Math.Max(indexed[left].index, indexed[right].index)
                };
            }
            else if (sum < target)
            {
                left++;
            }
            else
            {
                right--;
            }
        }
        
        return new int[] { -1, -1 };
    }
}
```

**Variations:**

```csharp
public class TwoSumVariations
{
    // Return all pairs that sum to target
    public static List<(int, int)> FindAllPairs(int[] nums, int target)
    {
        var result = new List<(int, int)>();
        var map = new Dictionary<int, List<int>>();
        
        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];
            
            if (map.ContainsKey(complement))
            {
                foreach (int j in map[complement])
                {
                    result.Add((j, i));
                }
            }
            
            if (!map.ContainsKey(nums[i]))
                map[nums[i]] = new List<int>();
            
            map[nums[i]].Add(i);
        }
        
        return result;
    }
    
    // Return values instead of indices
    public static int[] TwoSumValues(int[] nums, int target)
    {
        HashSet<int> seen = new HashSet<int>();
        
        foreach (int num in nums)
        {
            int complement = target - num;
            
            if (seen.Contains(complement))
            {
                return new int[] { complement, num };
            }
            
            seen.Add(num);
        }
        
        return new int[] { -1, -1 };
    }
}
```

---

### 7.6 Valid Parentheses

**Problem:**
Given a string containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.

**Rules:**
- Open brackets must be closed by the same type of brackets
- Open brackets must be closed in the correct order

**Example:**
```
Input: "()"
Output: true

Input: "()[]{}"
Output: true

Input: "(]"
Output: false

Input: "([)]"
Output: false

Input: "{[]}"
Output: true
```

**Solution 1: Using Stack**
```csharp
public class ParenthesesValidator
{
    // Time: O(n), Space: O(n)
    public static bool IsValid(string s)
    {
        if (string.IsNullOrEmpty(s)) return true;
        if (s.Length % 2 != 0) return false;
        
        Stack<char> stack = new Stack<char>();
        Dictionary<char, char> pairs = new Dictionary<char, char>
        {
            { ')', '(' },
            { '}', '{' },
            { ']', '[' }
        };
        
        foreach (char c in s)
        {
            if (c == '(' || c == '{' || c == '[')
            {
                stack.Push(c);
            }
            else if (pairs.ContainsKey(c))
            {
                if (stack.Count == 0 || stack.Pop() != pairs[c])
                {
                    return false;
                }
            }
        }
        
        return stack.Count == 0;
    }
    
    public static void Example()
    {
        Console.WriteLine(IsValid("()"));       // True
        Console.WriteLine(IsValid("()[]{}"));   // True
        Console.WriteLine(IsValid("(]"));       // False
        Console.WriteLine(IsValid("([)]"));     // False
        Console.WriteLine(IsValid("{[]}"));     // True
    }
}
```

**Solution 2: Alternative Implementation**
```csharp
public class ParenthesesValidatorAlt
{
    public static bool IsValid(string s)
    {
        Stack<char> stack = new Stack<char>();
        
        foreach (char c in s)
        {
            switch (c)
            {
                case '(':
                    stack.Push(')');
                    break;
                case '{':
                    stack.Push('}');
                    break;
                case '[':
                    stack.Push(']');
                    break;
                default:
                    if (stack.Count == 0 || stack.Pop() != c)
                        return false;
                    break;
            }
        }
        
        return stack.Count == 0;
    }
}
```

**Extended Version: With Different Bracket Types**
```csharp
public class AdvancedParenthesesValidator
{
    public static bool IsValid(string s, char[]? allowedBrackets = null)
    {
        var openBrackets = new HashSet<char> { '(', '{', '[', '<' };
        var closeBrackets = new Dictionary<char, char>
        {
            { ')', '(' },
            { '}', '{' },
            { ']', '[' },
            { '>', '<' }
        };
        
        if (allowedBrackets != null)
        {
            openBrackets = new HashSet<char>(allowedBrackets.Where(c => openBrackets.Contains(c)));
        }
        
        Stack<char> stack = new Stack<char>();
        
        foreach (char c in s)
        {
            if (openBrackets.Contains(c))
            {
                stack.Push(c);
            }
            else if (closeBrackets.ContainsKey(c))
            {
                if (stack.Count == 0 || stack.Pop() != closeBrackets[c])
                {
                    return false;
                }
            }
        }
        
        return stack.Count == 0;
    }
    
    // Return detailed validation result
    public static (bool IsValid, string Error, int ErrorPosition) ValidateDetailed(string s)
    {
        Stack<(char bracket, int position)> stack = new Stack<(char, int)>();
        var closeBrackets = new Dictionary<char, char>
        {
            { ')', '(' },
            { '}', '{' },
            { ']', '[' }
        };
        
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            
            if (c == '(' || c == '{' || c == '[')
            {
                stack.Push((c, i));
            }
            else if (closeBrackets.ContainsKey(c))
            {
                if (stack.Count == 0)
                {
                    return (false, $"Unexpected closing bracket '{c}'", i);
                }
                
                var (openBracket, openPos) = stack.Pop();
                
                if (openBracket != closeBrackets[c])
                {
                    return (false, 
                        $"Mismatched brackets: '{openBracket}' at {openPos} and '{c}' at {i}", 
                        i);
                }
            }
        }
        
        if (stack.Count > 0)
        {
            var (bracket, position) = stack.Peek();
            return (false, $"Unclosed bracket '{bracket}' at position {position}", position);
        }
        
        return (true, string.Empty, -1);
    }
}
```

---

### 7.7 Implement LRU Cache

**Problem:**
Design a data structure that follows the Least Recently Used (LRU) cache constraints.

**Operations:**
- `Get(key)`: Get the value of the key if it exists, otherwise return -1
- `Put(key, value)`: Update or insert the value if the key exists. When cache reaches capacity, invalidate the least recently used item before inserting a new item.

**Example:**
```
LRUCache cache = new LRUCache(2);
cache.Put(1, 1);
cache.Put(2, 2);
cache.Get(1);       // returns 1
cache.Put(3, 3);    // evicts key 2
cache.Get(2);       // returns -1 (not found)
cache.Put(4, 4);    // evicts key 1
cache.Get(1);       // returns -1 (not found)
cache.Get(3);       // returns 3
cache.Get(4);       // returns 4
```

**Solution 1: Using Dictionary + LinkedList**
```csharp
public class LRUCache
{
    private readonly int capacity;
    private readonly Dictionary<int, LinkedListNode<CacheItem>> cache;
    private readonly LinkedList<CacheItem> lruList;
    
    public LRUCache(int capacity)
    {
        this.capacity = capacity;
        this.cache = new Dictionary<int, LinkedListNode<CacheItem>>();
        this.lruList = new LinkedList<CacheItem>();
    }
    
    // Time: O(1)
    public int Get(int key)
    {
        if (!cache.ContainsKey(key))
            return -1;
        
        var node = cache[key];
        
        // Move to front (most recently used)
        lruList.Remove(node);
        lruList.AddFirst(node);
        
        return node.Value.Value;
    }
    
    // Time: O(1)
    public void Put(int key, int value)
    {
        if (cache.ContainsKey(key))
        {
            // Update existing
            var node = cache[key];
            node.Value.Value = value;
            
            // Move to front
            lruList.Remove(node);
            lruList.AddFirst(node);
        }
        else
        {
            // Add new
            if (cache.Count >= capacity)
            {
                // Remove least recently used (last item)
                var lruNode = lruList.Last;
                lruList.RemoveLast();
                cache.Remove(lruNode.Value.Key);
            }
            
            var newItem = new CacheItem { Key = key, Value = value };
            var newNode = lruList.AddFirst(newItem);
            cache[key] = newNode;
        }
    }
    
    private class CacheItem
    {
        public int Key { get; set; }
        public int Value { get; set; }
    }
}
```

**Solution 2: Using .NET MemoryCache (Production Ready)**
```csharp
using Microsoft.Extensions.Caching.Memory;

public class LRUCacheMemory
{
    private readonly IMemoryCache cache;
    private readonly MemoryCacheOptions options;
    
    public LRUCacheMemory(int capacity)
    {
        options = new MemoryCacheOptions
        {
            SizeLimit = capacity
        };
        cache = new MemoryCache(options);
    }
    
    public int Get(int key)
    {
        if (cache.TryGetValue(key, out int value))
        {
            return value;
        }
        return -1;
    }
    
    public void Put(int key, int value)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSize(1)
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        
        cache.Set(key, value, cacheEntryOptions);
    }
}
```

**Solution 3: Generic LRU Cache**
```csharp
public class LRUCache<TKey, TValue> where TKey : notnull
{
    private readonly int capacity;
    private readonly Dictionary<TKey, LinkedListNode<CacheItem>> cache;
    private readonly LinkedList<CacheItem> lruList;
    private readonly object lockObject = new object();
    
    public LRUCache(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be positive", nameof(capacity));
        
        this.capacity = capacity;
        this.cache = new Dictionary<TKey, LinkedListNode<CacheItem>>();
        this.lruList = new LinkedList<CacheItem>();
    }
    
    public bool TryGet(TKey key, out TValue value)
    {
        lock (lockObject)
        {
            if (!cache.ContainsKey(key))
            {
                value = default!;
                return false;
            }
            
            var node = cache[key];
            value = node.Value.Value;
            
            // Move to front
            lruList.Remove(node);
            lruList.AddFirst(node);
            
            return true;
        }
    }
    
    public void Put(TKey key, TValue value)
    {
        lock (lockObject)
        {
            if (cache.ContainsKey(key))
            {
                var node = cache[key];
                node.Value.Value = value;
                
                lruList.Remove(node);
                lruList.AddFirst(node);
            }
            else
            {
                if (cache.Count >= capacity)
                {
                    var lruNode = lruList.Last;
                    lruList.RemoveLast();
                    cache.Remove(lruNode.Value.Key);
                }
                
                var newItem = new CacheItem { Key = key, Value = value };
                var newNode = lruList.AddFirst(newItem);
                cache[key] = newNode;
            }
        }
    }
    
    public void Clear()
    {
        lock (lockObject)
        {
            cache.Clear();
            lruList.Clear();
        }
    }
    
    public int Count
    {
        get
        {
            lock (lockObject)
            {
                return cache.Count;
            }
        }
    }
    
    private class CacheItem
    {
        public TKey Key { get; set; } = default!;
        public TValue Value { get; set; } = default!;
    }
}
```

**Usage Example:**
```csharp
public class LRUCacheExample
{
    public static void Example()
    {
        var cache = new LRUCache(2);
        
        cache.Put(1, 1);
        cache.Put(2, 2);
        Console.WriteLine(cache.Get(1));    // 1
        
        cache.Put(3, 3);                    // evicts key 2
        Console.WriteLine(cache.Get(2));    // -1
        
        cache.Put(4, 4);                    // evicts key 1
        Console.WriteLine(cache.Get(1));    // -1
        Console.WriteLine(cache.Get(3));    // 3
        Console.WriteLine(cache.Get(4));    // 4
    }
    
    public static void GenericExample()
    {
        var cache = new LRUCache<string, User>(3);
        
        cache.Put("user1", new User { Id = 1, Name = "Alice" });
        cache.Put("user2", new User { Id = 2, Name = "Bob" });
        cache.Put("user3", new User { Id = 3, Name = "Charlie" });
        
        if (cache.TryGet("user1", out var user))
        {
            Console.WriteLine($"Found: {user.Name}");
        }
        
        cache.Put("user4", new User { Id = 4, Name = "David" }); // evicts user2
        
        if (!cache.TryGet("user2", out _))
        {
            Console.WriteLine("user2 was evicted");
        }
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
```

**Time & Space Complexity:**

| Operation | Time | Space |
|-----------|------|-------|
| Get | O(1) | - |
| Put | O(1) | - |
| Overall | - | O(capacity) |

---

## 8. .NET Core / ASP.NET Core Coding

### 8.1 Create REST API with Proper HTTP Status Codes

**Problem:**
Create a RESTful API for a Product resource with proper HTTP status codes and responses.

**Requirements:**
- GET all products (200 OK)
- GET product by ID (200 OK or 404 Not Found)
- POST create product (201 Created)
- PUT update product (200 OK or 404 Not Found)
- DELETE product (204 No Content or 404 Not Found)
- Proper validation with 400 Bad Request

**Solution:**

```csharp
// Models
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateProductRequest
{
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [Range(0.01, 1000000, ErrorMessage = "Price must be between 0.01 and 1,000,000")]
    public decimal Price { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
    public int Stock { get; set; }
}

public class UpdateProductRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [Range(0.01, 1000000)]
    public decimal Price { get; set; }
    
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
}

// Controller
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;
    
    public ProductsController(
        IProductService productService,
        ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }
    
    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>List of products</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<Product>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<Product>>>> GetAll()
    {
        try
        {
            var products = await _productService.GetAllAsync();
            
            return Ok(new ApiResponse<List<Product>>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = products
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving products");
            return StatusCode(500, new ApiResponse<List<Product>>
            {
                Success = false,
                Message = "An error occurred while retrieving products",
                Errors = new List<string> { ex.Message }
            });
        }
    }
    
    /// <summary>
    /// Get product by ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Product details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<Product>>> GetById(int id)
    {
        try
        {
            var product = await _productService.GetByIdAsync(id);
            
            if (product == null)
            {
                return NotFound(new ApiResponse<Product>
                {
                    Success = false,
                    Message = $"Product with ID {id} not found"
                });
            }
            
            return Ok(new ApiResponse<Product>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = product
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product {ProductId}", id);
            return StatusCode(500, new ApiResponse<Product>
            {
                Success = false,
                Message = "An error occurred while retrieving the product",
                Errors = new List<string> { ex.Message }
            });
        }
    }
    
    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="request">Product creation request</param>
    /// <returns>Created product</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<Product>>> Create(
        [FromBody] CreateProductRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                
                return BadRequest(new ApiResponse<Product>
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = errors
                });
            }
            
            var product = await _productService.CreateAsync(request);
            
            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                new ApiResponse<Product>
                {
                    Success = true,
                    Message = "Product created successfully",
                    Data = product
                });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product");
            return StatusCode(500, new ApiResponse<Product>
            {
                Success = false,
                Message = "An error occurred while creating the product",
                Errors = new List<string> { ex.Message }
            });
        }
    }
    
    /// <summary>
    /// Update an existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="request">Product update request</param>
    /// <returns>Updated product</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<Product>>> Update(
        int id,
        [FromBody] UpdateProductRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                
                return BadRequest(new ApiResponse<Product>
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = errors
                });
            }
            
            var product = await _productService.UpdateAsync(id, request);
            
            if (product == null)
            {
                return NotFound(new ApiResponse<Product>
                {
                    Success = false,
                    Message = $"Product with ID {id} not found"
                });
            }
            
            return Ok(new ApiResponse<Product>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = product
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product {ProductId}", id);
            return StatusCode(500, new ApiResponse<Product>
            {
                Success = false,
                Message = "An error occurred while updating the product",
                Errors = new List<string> { ex.Message }
            });
        }
    }
    
    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var success = await _productService.DeleteAsync(id);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Product with ID {id} not found"
                });
            }
            
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product {ProductId}", id);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "An error occurred while deleting the product",
                Errors = new List<string> { ex.Message }
            });
        }
    }
}

// Service Interface
public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(CreateProductRequest request);
    Task<Product?> UpdateAsync(int id, UpdateProductRequest request);
    Task<bool> DeleteAsync(int id);
}

// Service Implementation
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    
    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }
    
    public async Task<Product> CreateAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        
        return product;
    }
    
    public async Task<Product?> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await _context.Products.FindAsync(id);
        
        if (product == null)
            return null;
        
        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        
        return product;
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        
        if (product == null)
            return false;
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
```

**HTTP Status Codes Summary:**

| Status Code | Meaning | Use Case |
|-------------|---------|----------|
| 200 OK | Success | GET, PUT successful |
| 201 Created | Resource created | POST successful |
| 204 No Content | Success with no body | DELETE successful |
| 400 Bad Request | Invalid input | Validation errors |
| 404 Not Found | Resource not found | GET, PUT, DELETE non-existent resource |
| 500 Internal Server Error | Server error | Unexpected errors |

---

### 8.2 Implement Global Exception Handling

**Problem:**
Implement centralized exception handling for an ASP.NET Core application to avoid repetitive try-catch blocks.

**Solution 1: Using Middleware**

```csharp
// Custom Exception Classes
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class ValidationException : Exception
{
    public List<string> Errors { get; }
    
    public ValidationException(List<string> errors) 
        : base("One or more validation errors occurred")
    {
        Errors = errors;
    }
}

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}

// Error Response Model
public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }
    public string? StackTrace { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

// Global Exception Handling Middleware
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly IHostEnvironment _environment;
    
    public GlobalExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlerMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred");
        
        context.Response.ContentType = "application/json";
        
        var response = new ErrorResponse
        {
            Timestamp = DateTime.UtcNow
        };
        
        switch (exception)
        {
            case NotFoundException notFoundEx:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = notFoundEx.Message;
                break;
                
            case ValidationException validationEx:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = validationEx.Message;
                response.Errors = validationEx.Errors;
                break;
                
            case BusinessException businessEx:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = businessEx.Message;
                break;
                
            case UnauthorizedAccessException:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Message = "Unauthorized access";
                break;
                
            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "An internal server error occurred";
                break;
        }
        
        // Include stack trace only in development
        if (_environment.IsDevelopment())
        {
            response.StackTrace = exception.StackTrace;
        }
        
        var jsonResponse = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(jsonResponse);
    }
}

// Extension Method
public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}

// Program.cs or Startup.cs
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services
        builder.Services.AddControllers();
        
        var app = builder.Build();
        
        // Use global exception handler (must be early in pipeline)
        app.UseGlobalExceptionHandler();
        
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        
        app.Run();
    }
}
```

**Solution 2: Using IExceptionHandler (.NET 8+)**

```csharp
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An error occurred: {Message}", exception.Message);
        
        var (statusCode, message) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            ValidationException => (StatusCodes.Status400BadRequest, exception.Message),
            BusinessException => (StatusCodes.Status400BadRequest, exception.Message),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error")
        };
        
        httpContext.Response.StatusCode = statusCode;
        
        var response = new ErrorResponse
        {
            StatusCode = statusCode,
            Message = message,
            Timestamp = DateTime.UtcNow
        };
        
        if (exception is ValidationException validationEx)
        {
            response.Errors = validationEx.Errors;
        }
        
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        
        return true;
    }
}

// Program.cs
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
```

**Usage in Controllers:**

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        // No try-catch needed - handled by global exception handler
        var product = await _productService.GetByIdAsync(id);
        
        if (product == null)
            throw new NotFoundException($"Product with ID {id} not found");
        
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<ActionResult<Product>> Create(CreateProductRequest request)
    {
        // Validation
        if (request.Price <= 0)
        {
            throw new ValidationException(new List<string> 
            { 
                "Price must be greater than 0" 
            });
        }
        
        var product = await _productService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
}
```

---

### 8.3 Pagination & Filtering

**Problem:**
Implement efficient pagination and filtering for large datasets.

**Solution:**

```csharp
// Pagination Models
public class PaginationParams
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;
    
    public int PageNumber { get; set; } = 1;
    
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
}

public class ProductFilterParams : PaginationParams
{
    public string? SearchTerm { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? SortBy { get; set; } // "name", "price", "created"
    public string? SortOrder { get; set; } = "asc"; // "asc" or "desc"
}

// Extension Methods for Pagination
public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedResult<T>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = count,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };
    }
}

// Controller
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<PagedResult<Product>>> GetProducts(
        [FromQuery] ProductFilterParams filterParams)
    {
        var query = _context.Products.AsQueryable();
        
        // Filtering
        if (!string.IsNullOrWhiteSpace(filterParams.SearchTerm))
        {
            query = query.Where(p =>
                p.Name.Contains(filterParams.SearchTerm) ||
                p.Description.Contains(filterParams.SearchTerm));
        }
        
        if (filterParams.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= filterParams.MinPrice.Value);
        }
        
        if (filterParams.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= filterParams.MaxPrice.Value);
        }
        
        // Sorting
        query = filterParams.SortBy?.ToLower() switch
        {
            "name" => filterParams.SortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name),
            "price" => filterParams.SortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(p => p.Price)
                : query.OrderBy(p => p.Price),
            "created" => filterParams.SortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderBy(p => p.CreatedAt),
            _ => query.OrderByDescending(p => p.CreatedAt) // Default
        };
        
        // Pagination
        var pagedResult = await query.ToPagedListAsync(
            filterParams.PageNumber,
            filterParams.PageSize);
        
        // Add pagination headers
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new
        {
            pagedResult.TotalCount,
            pagedResult.PageSize,
            pagedResult.PageNumber,
            pagedResult.TotalPages,
            pagedResult.HasPrevious,
            pagedResult.HasNext
        }));
        
        return Ok(pagedResult);
    }
}
```

**Advanced Filtering with Specification Pattern:**

```csharp
// Specification Pattern
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}

public class BaseSpecification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; private set; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }
    
    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    
    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    
    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }
    
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}

public class ProductsWithFiltersSpecification : BaseSpecification<Product>
{
    public ProductsWithFiltersSpecification(ProductFilterParams filterParams)
        : base(x =>
            (string.IsNullOrWhiteSpace(filterParams.SearchTerm) ||
             x.Name.Contains(filterParams.SearchTerm) ||
             x.Description.Contains(filterParams.SearchTerm)) &&
            (!filterParams.MinPrice.HasValue || x.Price >= filterParams.MinPrice.Value) &&
            (!filterParams.MaxPrice.HasValue || x.Price <= filterParams.MaxPrice.Value))
    {
        ApplyPaging(
            (filterParams.PageNumber - 1) * filterParams.PageSize,
            filterParams.PageSize);
        
        if (filterParams.SortBy?.ToLower() == "name")
        {
            if (filterParams.SortOrder?.ToLower() == "desc")
                ApplyOrderByDescending(p => p.Name);
            else
                ApplyOrderBy(p => p.Name);
        }
        else
        {
            ApplyOrderByDescending(p => p.CreatedAt);
        }
    }
}
```

---

### 8.4 Custom Middleware Example

**Problem:**
Create custom middleware for request logging, performance monitoring, and API key validation.

**Solution 1: Request Logging Middleware**

```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    
    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        // Log request
        var requestId = Guid.NewGuid().ToString();
        context.Items["RequestId"] = requestId;
        
        _logger.LogInformation(
            "Request {RequestId}: {Method} {Path} started at {Time}",
            requestId,
            context.Request.Method,
            context.Request.Path,
            DateTime.UtcNow);
        
        // Capture request body
        context.Request.EnableBuffering();
        var requestBody = await ReadRequestBodyAsync(context.Request);
        
        // Capture response
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;
        
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            
            // Log response
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            
            _logger.LogInformation(
                "Request {RequestId}: {Method} {Path} completed with {StatusCode} in {ElapsedMs}ms",
                requestId,
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
            
            // Copy response back to original stream
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
    
    private async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(request.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        request.Body.Seek(0, SeekOrigin.Begin);
        return body;
    }
}
```

**Solution 2: Performance Monitoring Middleware**

```csharp
public class PerformanceMonitoringMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMonitoringMiddleware> _logger;
    private readonly int _slowRequestThresholdMs;
    
    public PerformanceMonitoringMiddleware(
        RequestDelegate next,
        ILogger<PerformanceMonitoringMiddleware> logger,
        IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _slowRequestThresholdMs = configuration.GetValue<int>(
            "PerformanceMonitoring:SlowRequestThresholdMs", 1000);
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            
            var elapsed = stopwatch.ElapsedMilliseconds;
            
            if (elapsed > _slowRequestThresholdMs)
            {
                _logger.LogWarning(
                    "Slow request detected: {Method} {Path} took {ElapsedMs}ms",
                    context.Request.Method,
                    context.Request.Path,
                    elapsed);
            }
            
            // Add performance header
            context.Response.Headers.Add("X-Response-Time-Ms", elapsed.ToString());
        }
    }
}
```

**Solution 3: API Key Validation Middleware**

```csharp
public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private const string API_KEY_HEADER = "X-API-Key";
    
    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        // Skip authentication for certain paths
        if (ShouldSkipValidation(context.Request.Path))
        {
            await _next(context);
            return;
        }
        
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API Key is missing");
            return;
        }
        
        var apiKey = _configuration.GetValue<string>("ApiKey");
        
        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }
        
        await _next(context);
    }
    
    private bool ShouldSkipValidation(PathString path)
    {
        var publicPaths = new[] { "/health", "/swagger", "/api/auth" };
        return publicPaths.Any(p => path.StartsWithSegments(p));
    }
}
```

**Solution 4: Rate Limiting Middleware**

```csharp
public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMemoryCache _cache;
    private readonly int _requestLimit;
    private readonly TimeSpan _timeWindow;
    
    public RateLimitingMiddleware(
        RequestDelegate next,
        IMemoryCache cache,
        IConfiguration configuration)
    {
        _next = next;
        _cache = cache;
        _requestLimit = configuration.GetValue<int>("RateLimit:RequestLimit", 100);
        _timeWindow = TimeSpan.FromMinutes(
            configuration.GetValue<int>("RateLimit:TimeWindowMinutes", 1));
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var clientId = GetClientIdentifier(context);
        var cacheKey = $"RateLimit_{clientId}";
        
        if (!_cache.TryGetValue(cacheKey, out int requestCount))
        {
            requestCount = 0;
        }
        
        requestCount++;
        
        if (requestCount > _requestLimit)
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            context.Response.Headers.Add("Retry-After", _timeWindow.TotalSeconds.ToString());
            await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
            return;
        }
        
        _cache.Set(cacheKey, requestCount, _timeWindow);
        
        context.Response.Headers.Add("X-Rate-Limit-Limit", _requestLimit.ToString());
        context.Response.Headers.Add("X-Rate-Limit-Remaining", (_requestLimit - requestCount).ToString());
        
        await _next(context);
    }
    
    private string GetClientIdentifier(HttpContext context)
    {
        // Use IP address or authenticated user ID
        return context.Connection.RemoteIpAddress?.ToString() 
               ?? context.User?.Identity?.Name 
               ?? "anonymous";
    }
}
```

**Registration in Program.cs:**

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddControllers();

var app = builder.Build();

// Order matters!
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<PerformanceMonitoringMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<RateLimitingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

---

### 8.5 Logging / Exception Middleware

**Problem:**
Implement comprehensive logging with structured logging and correlation IDs.

**Solution:**

```csharp
// Correlation ID Middleware
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string CorrelationIdHeader = "X-Correlation-ID";
    
    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = GetOrCreateCorrelationId(context);
        
        // Add to response headers
        context.Response.Headers.Add(CorrelationIdHeader, correlationId);
        
        // Add to logging scope
        using var _ = context.RequestServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger<CorrelationIdMiddleware>()
            .BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            });
        
        await _next(context);
    }
    
    private string GetOrCreateCorrelationId(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
        {
            return correlationId.ToString();
        }
        
        return Guid.NewGuid().ToString();
    }
}

// Structured Logging Middleware
public class StructuredLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<StructuredLoggingMiddleware> _logger;
    
    public StructuredLoggingMiddleware(
        RequestDelegate next,
        ILogger<StructuredLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            await _next(context);
            
            stopwatch.Stop();
            
            LogRequestCompleted(context, stopwatch.ElapsedMilliseconds, null);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            
            LogRequestCompleted(context, stopwatch.ElapsedMilliseconds, ex);
            
            throw;
        }
    }
    
    private void LogRequestCompleted(
        HttpContext context,
        long elapsedMs,
        Exception? exception)
    {
        var logLevel = exception != null ? LogLevel.Error : LogLevel.Information;
        
        _logger.Log(
            logLevel,
            exception,
            "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs}ms",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            elapsedMs);
    }
}

// Application Logging Service
public interface IAppLogger
{
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(Exception exception, string message, params object[] args);
    void LogDebug(string message, params object[] args);
}

public class AppLogger : IAppLogger
{
    private readonly ILogger<AppLogger> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AppLogger(
        ILogger<AppLogger> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public void LogInformation(string message, params object[] args)
    {
        using var scope = CreateLogScope();
        _logger.LogInformation(message, args);
    }
    
    public void LogWarning(string message, params object[] args)
    {
        using var scope = CreateLogScope();
        _logger.LogWarning(message, args);
    }
    
    public void LogError(Exception exception, string message, params object[] args)
    {
        using var scope = CreateLogScope();
        _logger.LogError(exception, message, args);
    }
    
    public void LogDebug(string message, params object[] args)
    {
        using var scope = CreateLogScope();
        _logger.LogDebug(message, args);
    }
    
    private IDisposable? CreateLogScope()
    {
        var context = _httpContextAccessor.HttpContext;
        
        if (context == null)
            return null;
        
        var scopeData = new Dictionary<string, object>
        {
            ["RequestPath"] = context.Request.Path,
            ["RequestMethod"] = context.Request.Method,
            ["UserAgent"] = context.Request.Headers["User-Agent"].ToString(),
            ["RemoteIpAddress"] = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown"
        };
        
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            scopeData["UserId"] = context.User.Identity.Name ?? "Unknown";
        }
        
        return _logger.BeginScope(scopeData);
    }
}

// appsettings.json configuration
/*
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "Console": {
      "IncludeScopes": true,
      "TimestampFormat": "yyyy-MM-dd HH:mm:ss "
    }
  }
}
*/

// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAppLogger, AppLogger>();
builder.Services.AddControllers();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Add middleware
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<StructuredLoggingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

**Usage Example:**

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IAppLogger _logger;
    private readonly IProductService _productService;
    
    public ProductsController(
        IAppLogger logger,
        IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        _logger.LogInformation("Retrieving product with ID: {ProductId}", id);
        
        try
        {
            var product = await _productService.GetByIdAsync(id);
            
            if (product == null)
            {
                _logger.LogWarning("Product not found: {ProductId}", id);
                return NotFound();
            }
            
            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product: {ProductId}", id);
            throw;
        }
    }
}
```

---

## 9. Dependency Injection

### 9.1 Scoped vs Singleton vs Transient

**Problem:**
Explain and demonstrate the three service lifetimes in .NET Core DI with practical examples.

**Service Lifetimes:**

| Lifetime | Description | Use Case | Created | Disposed |
|----------|-------------|----------|---------|----------|
| **Transient** | New instance every time | Lightweight, stateless services | Each request | Immediately after use |
| **Scoped** | One instance per request | Per-request state, DbContext | Once per HTTP request | End of request |
| **Singleton** | One instance for app lifetime | Configuration, caching | App startup | App shutdown |

**Solution:**

```csharp
// Service Interfaces
public interface IOperationTransient
{
    Guid OperationId { get; }
    DateTime CreatedAt { get; }
}

public interface IOperationScoped
{
    Guid OperationId { get; }
    DateTime CreatedAt { get; }
}

public interface IOperationSingleton
{
    Guid OperationId { get; }
    DateTime CreatedAt { get; }
}

// Service Implementation
public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
{
    public Guid OperationId { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}

// Demo Service to Show Lifetimes
public class OperationService
{
    public IOperationTransient TransientOperation { get; }
    public IOperationScoped ScopedOperation { get; }
    public IOperationSingleton SingletonOperation { get; }
    
    public OperationService(
        IOperationTransient transientOperation,
        IOperationScoped scopedOperation,
        IOperationSingleton singletonOperation)
    {
        TransientOperation = transientOperation;
        ScopedOperation = scopedOperation;
        SingletonOperation = singletonOperation;
    }
}

// Program.cs Registration
var builder = WebApplication.CreateBuilder(args);

// Register services with different lifetimes
builder.Services.AddTransient<IOperationTransient, Operation>();
builder.Services.AddScoped<IOperationScoped, Operation>();
builder.Services.AddSingleton<IOperationSingleton, Operation>();
builder.Services.AddTransient<OperationService>();

var app = builder.Build();

// Test endpoint
app.MapGet("/lifetime-test", (
    OperationService operationService1,
    OperationService operationService2,
    IOperationTransient transient,
    IOperationScoped scoped,
    IOperationSingleton singleton) =>
{
    return new
    {
        Service1 = new
        {
            Transient = operationService1.TransientOperation.OperationId,
            Scoped = operationService1.ScopedOperation.OperationId,
            Singleton = operationService1.SingletonOperation.OperationId
        },
        Service2 = new
        {
            Transient = operationService2.TransientOperation.OperationId,
            Scoped = operationService2.ScopedOperation.OperationId,
            Singleton = operationService2.SingletonOperation.OperationId
        },
        DirectInjection = new
        {
            Transient = transient.OperationId,
            Scoped = scoped.OperationId,
            Singleton = singleton.OperationId
        }
    };
});

app.Run();
```

**Real-World Examples:**

```csharp
// 1. TRANSIENT - Email Service (stateless, lightweight)
public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    
    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }
    
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        _logger.LogInformation("Sending email to {To}", to);
        // Send email logic
        await Task.CompletedTask;
    }
}

// Register as Transient
builder.Services.AddTransient<IEmailService, EmailService>();

// 2. SCOPED - Database Context (per-request state)
public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

// Register as Scoped (default for DbContext)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. SINGLETON - Configuration Service (shared state)
public interface IAppConfiguration
{
    string GetSetting(string key);
    T GetSetting<T>(string key);
}

public class AppConfiguration : IAppConfiguration
{
    private readonly IConfiguration _configuration;
    private readonly ConcurrentDictionary<string, object> _cache;
    
    public AppConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
        _cache = new ConcurrentDictionary<string, object>();
    }
    
    public string GetSetting(string key)
    {
        return _cache.GetOrAdd(key, k => _configuration[k] ?? string.Empty).ToString()!;
    }
    
    public T GetSetting<T>(string key)
    {
        return (T)_cache.GetOrAdd(key, k => _configuration.GetValue<T>(k)!);
    }
}

// Register as Singleton
builder.Services.AddSingleton<IAppConfiguration, AppConfiguration>();

// 4. SCOPED - Unit of Work Pattern
public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    Task<int> SaveChangesAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    
    public UnitOfWork(
        ApplicationDbContext context,
        IProductRepository products,
        IOrderRepository orders)
    {
        _context = context;
        Products = products;
        Orders = orders;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context?.Dispose();
    }
}

// Register as Scoped
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

**Common Pitfalls:**

```csharp
// ❌ WRONG: Injecting Scoped service into Singleton
public class SingletonService // Registered as Singleton
{
    private readonly ApplicationDbContext _context; // Scoped!
    
    // This will cause issues - DbContext will live for entire app lifetime
    public SingletonService(ApplicationDbContext context)
    {
        _context = context;
    }
}

// ✅ CORRECT: Use IServiceScopeFactory
public class SingletonService
{
    private readonly IServiceScopeFactory _scopeFactory;
    
    public SingletonService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task DoWorkAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // Use context
    }
}
```

---

### 9.2 Coding Custom Service Registration

**Problem:**
Create custom extension methods for service registration with advanced patterns.

**Solution 1: Extension Method for Bulk Registration**

```csharp
public static class ServiceCollectionExtensions
{
    // Register all services from an assembly by convention
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        Assembly assembly)
    {
        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => t.Name.EndsWith("Service"))
            .ToList();
        
        foreach (var implementationType in serviceTypes)
        {
            var interfaceType = implementationType.GetInterfaces()
                .FirstOrDefault(i => i.Name == $"I{implementationType.Name}");
            
            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, implementationType);
            }
        }
        
        return services;
    }
    
    // Register services by interface marker
    public static IServiceCollection AddServicesByInterface<TInterface>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        var interfaceType = typeof(TInterface);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
        
        foreach (var type in types)
        {
            var serviceInterface = type.GetInterfaces()
                .FirstOrDefault(i => i != interfaceType && interfaceType.IsAssignableFrom(i));
            
            if (serviceInterface != null)
            {
                services.Add(new ServiceDescriptor(serviceInterface, type, lifetime));
            }
        }
        
        return services;
    }
    
    // Decorator pattern registration
    public static IServiceCollection Decorate<TInterface, TDecorator>(
        this IServiceCollection services)
        where TInterface : class
        where TDecorator : class, TInterface
    {
        var wrappedDescriptor = services.FirstOrDefault(
            s => s.ServiceType == typeof(TInterface));
        
        if (wrappedDescriptor == null)
        {
            throw new InvalidOperationException(
                $"{typeof(TInterface).Name} is not registered");
        }
        
        var objectFactory = ActivatorUtilities.CreateFactory(
            typeof(TDecorator),
            new[] { typeof(TInterface) });
        
        services.Replace(ServiceDescriptor.Describe(
            typeof(TInterface),
            provider =>
            {
                var wrappedInstance = ActivatorUtilities.CreateInstance(
                    provider,
                    wrappedDescriptor.ImplementationType!);
                
                return objectFactory(provider, new[] { wrappedInstance });
            },
            wrappedDescriptor.Lifetime));
        
        return services;
    }
    
    // Conditional registration
    public static IServiceCollection AddConditional<TInterface, TImplementation>(
        this IServiceCollection services,
        Func<IServiceProvider, bool> condition,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.Add(new ServiceDescriptor(
            typeof(TInterface),
            provider => condition(provider)
                ? ActivatorUtilities.CreateInstance<TImplementation>(provider)
                : throw new InvalidOperationException("Condition not met"),
            lifetime));
        
        return services;
    }
}

// Usage Examples
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // 1. Register all services from assembly
        builder.Services.AddApplicationServices(typeof(Program).Assembly);
        
        // 2. Register by marker interface
        builder.Services.AddServicesByInterface<ITransientService>(
            ServiceLifetime.Transient);
        
        // 3. Decorator pattern
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.Decorate<IProductService, CachedProductService>();
        
        // 4. Conditional registration
        builder.Services.AddConditional<IPaymentGateway, StripePaymentGateway>(
            provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                return config["PaymentProvider"] == "Stripe";
            });
        
        var app = builder.Build();
        app.Run();
    }
}
```

**Solution 2: Factory Pattern Registration**

```csharp
// Factory Interface
public interface IServiceFactory<T>
{
    T Create(string key);
}

// Factory Implementation
public class PaymentGatewayFactory : IServiceFactory<IPaymentGateway>
{
    private readonly IServiceProvider _serviceProvider;
    
    public PaymentGatewayFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public IPaymentGateway Create(string key)
    {
        return key.ToLower() switch
        {
            "stripe" => _serviceProvider.GetRequiredService<StripePaymentGateway>(),
            "paypal" => _serviceProvider.GetRequiredService<PayPalPaymentGateway>(),
            "square" => _serviceProvider.GetRequiredService<SquarePaymentGateway>(),
            _ => throw new ArgumentException($"Unknown payment gateway: {key}")
        };
    }
}

// Service Interface
public interface IPaymentGateway
{
    Task<bool> ProcessPaymentAsync(decimal amount);
}

// Implementations
public class StripePaymentGateway : IPaymentGateway
{
    public async Task<bool> ProcessPaymentAsync(decimal amount)
    {
        // Stripe logic
        await Task.CompletedTask;
        return true;
    }
}

public class PayPalPaymentGateway : IPaymentGateway
{
    public async Task<bool> ProcessPaymentAsync(decimal amount)
    {
        // PayPal logic
        await Task.CompletedTask;
        return true;
    }
}

// Registration
builder.Services.AddScoped<StripePaymentGateway>();
builder.Services.AddScoped<PayPalPaymentGateway>();
builder.Services.AddScoped<SquarePaymentGateway>();
builder.Services.AddScoped<IServiceFactory<IPaymentGateway>, PaymentGatewayFactory>();

// Usage
public class PaymentController : ControllerBase
{
    private readonly IServiceFactory<IPaymentGateway> _gatewayFactory;
    
    public PaymentController(IServiceFactory<IPaymentGateway> gatewayFactory)
    {
        _gatewayFactory = gatewayFactory;
    }
    
    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment(
        [FromBody] PaymentRequest request)
    {
        var gateway = _gatewayFactory.Create(request.Provider);
        var result = await gateway.ProcessPaymentAsync(request.Amount);
        return Ok(result);
    }
}
```

**Solution 3: Options Pattern with Validation**

```csharp
// Options Class
public class EmailSettings
{
    public string SmtpServer { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool EnableSsl { get; set; }
}

// Validator
public class EmailSettingsValidator : IValidateOptions<EmailSettings>
{
    public ValidateOptionsResult Validate(string? name, EmailSettings options)
    {
        if (string.IsNullOrWhiteSpace(options.SmtpServer))
        {
            return ValidateOptionsResult.Fail("SMTP Server is required");
        }
        
        if (options.Port <= 0 || options.Port > 65535)
        {
            return ValidateOptionsResult.Fail("Port must be between 1 and 65535");
        }
        
        return ValidateOptionsResult.Success;
    }
}

// Extension Method
public static class OptionsExtensions
{
    public static IServiceCollection AddValidatedOptions<TOptions>(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName)
        where TOptions : class
    {
        services.AddOptions<TOptions>()
            .Bind(configuration.GetSection(sectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return services;
    }
}

// Registration
builder.Services.AddValidatedOptions<EmailSettings>(
    builder.Configuration,
    "EmailSettings");

builder.Services.AddSingleton<IValidateOptions<EmailSettings>, EmailSettingsValidator>();

// Usage
public class EmailService
{
    private readonly EmailSettings _settings;
    
    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }
}
```

**Solution 4: Named Services**

```csharp
public static class NamedServiceExtensions
{
    public static IServiceCollection AddNamedService<TInterface, TImplementation>(
        this IServiceCollection services,
        string name)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddSingleton<TImplementation>();
        
        services.AddSingleton<Func<string, TInterface>>(provider =>
        {
            return serviceName =>
            {
                if (serviceName == name)
                {
                    return provider.GetRequiredService<TImplementation>();
                }
                throw new ArgumentException($"Unknown service: {serviceName}");
            };
        });
        
        return services;
    }
}

// Usage
builder.Services.AddNamedService<ICache, RedisCache>("redis");
builder.Services.AddNamedService<ICache, MemoryCache>("memory");

public class CacheService
{
    private readonly ICache _cache;
    
    public CacheService(Func<string, ICache> cacheFactory)
    {
        _cache = cacheFactory("redis");
    }
}
```

---

## 10. Database & SQL

### 10.1 Find 2nd Highest Salary

**Problem:**
Write SQL queries to find the second highest salary from an Employee table.

**Table Structure:**
```sql
CREATE TABLE Employee (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100),
    Salary DECIMAL(18,2),
    DepartmentId INT
);
```

**Solution 1: Using OFFSET FETCH (SQL Server 2012+)**
```sql
SELECT DISTINCT Salary AS SecondHighestSalary
FROM Employee
ORDER BY Salary DESC
OFFSET 1 ROWS
FETCH NEXT 1 ROWS ONLY;
```

**Solution 2: Using Subquery**
```sql
SELECT MAX(Salary) AS SecondHighestSalary
FROM Employee
WHERE Salary < (SELECT MAX(Salary) FROM Employee);
```

**Solution 3: Using ROW_NUMBER()**
```sql
WITH RankedSalaries AS (
    SELECT 
        Salary,
        ROW_NUMBER() OVER (ORDER BY Salary DESC) AS RowNum
    FROM Employee
    WHERE Salary IS NOT NULL
    GROUP BY Salary
)
SELECT Salary AS SecondHighestSalary
FROM RankedSalaries
WHERE RowNum = 2;
```

**Solution 4: Using DENSE_RANK() (Handles Duplicates)**
```sql
WITH RankedSalaries AS (
    SELECT 
        Salary,
        DENSE_RANK() OVER (ORDER BY Salary DESC) AS Rank
    FROM Employee
)
SELECT DISTINCT Salary AS SecondHighestSalary
FROM RankedSalaries
WHERE Rank = 2;
```

**Solution 5: Finding Nth Highest Salary**
```sql
CREATE FUNCTION GetNthHighestSalary(@N INT)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @Result DECIMAL(18,2);
    
    WITH RankedSalaries AS (
        SELECT 
            Salary,
            DENSE_RANK() OVER (ORDER BY Salary DESC) AS Rank
        FROM Employee
        WHERE Salary IS NOT NULL
    )
    SELECT @Result = Salary
    FROM RankedSalaries
    WHERE Rank = @N;
    
    RETURN @Result;
END;

-- Usage
SELECT dbo.GetNthHighestSalary(2) AS SecondHighestSalary;
```

**Solution 6: Second Highest Per Department**
```sql
WITH DepartmentRanks AS (
    SELECT 
        DepartmentId,
        Name,
        Salary,
        DENSE_RANK() OVER (
            PARTITION BY DepartmentId 
            ORDER BY Salary DESC
        ) AS SalaryRank
    FROM Employee
)
SELECT 
    DepartmentId,
    Name,
    Salary AS SecondHighestSalary
FROM DepartmentRanks
WHERE SalaryRank = 2;
```

**Comparison:**

| Method | Pros | Cons | Duplicates |
|--------|------|------|------------|
| OFFSET FETCH | Simple, readable | Requires DISTINCT | Skips |
| Subquery | Works in older SQL | Slower for large data | Handles |
| ROW_NUMBER() | Precise control | Treats duplicates as separate | Skips |
| DENSE_RANK() | Best for duplicates | More complex | Handles |

---

### 10.2 Remove Duplicate Records

**Problem:**
Remove duplicate records from a table, keeping only one instance.

**Table Structure:**
```sql
CREATE TABLE Customer (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(100),
    Name NVARCHAR(100),
    Phone NVARCHAR(20)
);
```

**Solution 1: Using CTE and ROW_NUMBER() (Recommended)**
```sql
WITH CTE AS (
    SELECT 
        *,
        ROW_NUMBER() OVER (
            PARTITION BY Email 
            ORDER BY Id
        ) AS RowNum
    FROM Customer
)
DELETE FROM CTE
WHERE RowNum > 1;
```

**Solution 2: Keep Latest Record**
```sql
WITH CTE AS (
    SELECT 
        *,
        ROW_NUMBER() OVER (
            PARTITION BY Email 
            ORDER BY Id DESC
        ) AS RowNum
    FROM Customer
)
DELETE FROM CTE
WHERE RowNum > 1;
```

**Solution 3: Using NOT IN (Less Efficient)**
```sql
DELETE FROM Customer
WHERE Id NOT IN (
    SELECT MIN(Id)
    FROM Customer
    GROUP BY Email
);
```

**Solution 4: Using Self Join**
```sql
DELETE c1
FROM Customer c1
INNER JOIN Customer c2
    ON c1.Email = c2.Email
    AND c1.Id > c2.Id;
```

**Solution 5: Create New Table (Safest)**
```sql
-- Create new table with unique records
SELECT DISTINCT Email, Name, Phone
INTO Customer_Clean
FROM Customer;

-- Drop old table and rename
DROP TABLE Customer;
EXEC sp_rename 'Customer_Clean', 'Customer';
```

**Solution 6: With Multiple Columns**
```sql
WITH CTE AS (
    SELECT 
        *,
        ROW_NUMBER() OVER (
            PARTITION BY Email, Phone 
            ORDER BY Id
        ) AS RowNum
    FROM Customer
)
DELETE FROM CTE
WHERE RowNum > 1;
```

**Solution 7: Find Duplicates First (Analysis)**
```sql
-- Find duplicate emails
SELECT 
    Email,
    COUNT(*) AS DuplicateCount
FROM Customer
GROUP BY Email
HAVING COUNT(*) > 1;

-- Show all duplicate records
SELECT c.*
FROM Customer c
INNER JOIN (
    SELECT Email
    FROM Customer
    GROUP BY Email
    HAVING COUNT(*) > 1
) duplicates ON c.Email = duplicates.Email
ORDER BY c.Email, c.Id;
```

---

### 10.3 Join Multiple Tables

**Problem:**
Demonstrate different types of joins with multiple tables.

**Tables:**
```sql
CREATE TABLE Department (
    DepartmentId INT PRIMARY KEY,
    DepartmentName NVARCHAR(100)
);

CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY,
    Name NVARCHAR(100),
    DepartmentId INT,
    ManagerId INT,
    Salary DECIMAL(18,2)
);

CREATE TABLE Project (
    ProjectId INT PRIMARY KEY,
    ProjectName NVARCHAR(100),
    DepartmentId INT
);

CREATE TABLE EmployeeProject (
    EmployeeId INT,
    ProjectId INT,
    Role NVARCHAR(50),
    PRIMARY KEY (EmployeeId, ProjectId)
);
```

**Solution 1: INNER JOIN (Employees with Departments)**
```sql
SELECT 
    e.EmployeeId,
    e.Name AS EmployeeName,
    d.DepartmentName,
    e.Salary
FROM Employee e
INNER JOIN Department d ON e.DepartmentId = d.DepartmentId;
```

**Solution 2: LEFT JOIN (All Employees, Even Without Department)**
```sql
SELECT 
    e.EmployeeId,
    e.Name AS EmployeeName,
    ISNULL(d.DepartmentName, 'No Department') AS DepartmentName,
    e.Salary
FROM Employee e
LEFT JOIN Department d ON e.DepartmentId = d.DepartmentId;
```

**Solution 3: Self Join (Employees with Managers)**
```sql
SELECT 
    e.EmployeeId,
    e.Name AS EmployeeName,
    m.Name AS ManagerName,
    d.DepartmentName
FROM Employee e
LEFT JOIN Employee m ON e.ManagerId = m.EmployeeId
LEFT JOIN Department d ON e.DepartmentId = d.DepartmentId;
```

**Solution 4: Multiple Joins (Employees, Departments, Projects)**
```sql
SELECT 
    e.Name AS EmployeeName,
    d.DepartmentName,
    p.ProjectName,
    ep.Role
FROM Employee e
INNER JOIN Department d ON e.DepartmentId = d.DepartmentId
INNER JOIN EmployeeProject ep ON e.EmployeeId = ep.EmployeeId
INNER JOIN Project p ON ep.ProjectId = p.ProjectId
ORDER BY e.Name, p.ProjectName;
```

**Solution 5: Complex Query with Aggregations**
```sql
SELECT 
    d.DepartmentName,
    COUNT(DISTINCT e.EmployeeId) AS EmployeeCount,
    COUNT(DISTINCT p.ProjectId) AS ProjectCount,
    AVG(e.Salary) AS AverageSalary,
    MAX(e.Salary) AS MaxSalary
FROM Department d
LEFT JOIN Employee e ON d.DepartmentId = e.DepartmentId
LEFT JOIN EmployeeProject ep ON e.EmployeeId = ep.EmployeeId
LEFT JOIN Project p ON ep.ProjectId = p.ProjectId
GROUP BY d.DepartmentId, d.DepartmentName
ORDER BY EmployeeCount DESC;
```

**Solution 6: CROSS APPLY (Get Top N per Group)**
```sql
-- Get top 3 highest paid employees per department
SELECT 
    d.DepartmentName,
    TopEmployees.Name,
    TopEmployees.Salary
FROM Department d
CROSS APPLY (
    SELECT TOP 3 
        e.Name,
        e.Salary
    FROM Employee e
    WHERE e.DepartmentId = d.DepartmentId
    ORDER BY e.Salary DESC
) AS TopEmployees;
```

**Solution 7: Employees Working on Multiple Projects**
```sql
SELECT 
    e.Name AS EmployeeName,
    STRING_AGG(p.ProjectName, ', ') AS Projects,
    COUNT(p.ProjectId) AS ProjectCount
FROM Employee e
INNER JOIN EmployeeProject ep ON e.EmployeeId = ep.EmployeeId
INNER JOIN Project p ON ep.ProjectId = p.ProjectId
GROUP BY e.EmployeeId, e.Name
HAVING COUNT(p.ProjectId) > 1
ORDER BY ProjectCount DESC;
```

---

### 10.4 Write Pagination Query

**Problem:**
Implement efficient pagination for large result sets.

**Solution 1: OFFSET FETCH (SQL Server 2012+)**
```sql
DECLARE @PageNumber INT = 1;
DECLARE @PageSize INT = 10;

SELECT 
    EmployeeId,
    Name,
    DepartmentId,
    Salary
FROM Employee
ORDER BY EmployeeId
OFFSET (@PageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;
```

**Solution 2: Pagination with Total Count**
```sql
DECLARE @PageNumber INT = 1;
DECLARE @PageSize INT = 10;

-- Get total count
DECLARE @TotalCount INT;
SELECT @TotalCount = COUNT(*) FROM Employee;

-- Calculate total pages
DECLARE @TotalPages INT = CEILING(@TotalCount * 1.0 / @PageSize);

-- Get page data
SELECT 
    EmployeeId,
    Name,
    DepartmentId,
    Salary,
    @TotalCount AS TotalRecords,
    @TotalPages AS TotalPages,
    @PageNumber AS CurrentPage
FROM Employee
ORDER BY EmployeeId
OFFSET (@PageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;
```

**Solution 3: Stored Procedure for Pagination**
```sql
CREATE PROCEDURE GetEmployeesPaginated
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @SortColumn NVARCHAR(50) = 'EmployeeId',
    @SortOrder NVARCHAR(4) = 'ASC',
    @SearchTerm NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate inputs
    IF @PageNumber < 1 SET @PageNumber = 1;
    IF @PageSize < 1 SET @PageSize = 10;
    IF @PageSize > 100 SET @PageSize = 100; -- Max page size
    
    -- Get total count
    DECLARE @TotalCount INT;
    SELECT @TotalCount = COUNT(*)
    FROM Employee
    WHERE @SearchTerm IS NULL 
       OR Name LIKE '%' + @SearchTerm + '%';
    
    -- Build dynamic SQL for sorting
    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = N'
    SELECT 
        EmployeeId,
        Name,
        DepartmentId,
        Salary,
        ' + CAST(@TotalCount AS NVARCHAR) + ' AS TotalRecords,
        ' + CAST(CEILING(@TotalCount * 1.0 / @PageSize) AS NVARCHAR) + ' AS TotalPages,
        ' + CAST(@PageNumber AS NVARCHAR) + ' AS CurrentPage
    FROM Employee
    WHERE @SearchTerm IS NULL 
       OR Name LIKE ''%'' + @SearchTerm + ''%''
    ORDER BY ' + QUOTENAME(@SortColumn) + ' ' + @SortOrder + '
    OFFSET ' + CAST((@PageNumber - 1) * @PageSize AS NVARCHAR) + ' ROWS
    FETCH NEXT ' + CAST(@PageSize AS NVARCHAR) + ' ROWS ONLY';
    
    EXEC sp_executesql @SQL, N'@SearchTerm NVARCHAR(100)', @SearchTerm;
END;

-- Usage
EXEC GetEmployeesPaginated 
    @PageNumber = 2, 
    @PageSize = 20,
    @SortColumn = 'Name',
    @SortOrder = 'DESC',
    @SearchTerm = 'John';
```

**Solution 4: EF Core LINQ Pagination**
```csharp
public async Task<PagedResult<Employee>> GetEmployeesPaginatedAsync(
    int pageNumber,
    int pageSize,
    string? sortColumn = "Id",
    string? sortOrder = "asc",
    string? searchTerm = null)
{
    var query = _context.Employees.AsQueryable();
    
    // Filtering
    if (!string.IsNullOrWhiteSpace(searchTerm))
    {
        query = query.Where(e => e.Name.Contains(searchTerm));
    }
    
    // Get total count
    var totalCount = await query.CountAsync();
    
    // Sorting
    query = sortColumn?.ToLower() switch
    {
        "name" => sortOrder == "desc" 
            ? query.OrderByDescending(e => e.Name)
            : query.OrderBy(e => e.Name),
        "salary" => sortOrder == "desc"
            ? query.OrderByDescending(e => e.Salary)
            : query.OrderBy(e => e.Salary),
        _ => query.OrderBy(e => e.EmployeeId)
    };
    
    // Pagination
    var items = await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    
    return new PagedResult<Employee>
    {
        Items = items,
        PageNumber = pageNumber,
        PageSize = pageSize,
        TotalCount = totalCount,
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
    };
}
```

**Solution 5: Keyset Pagination (Better Performance)**
```sql
-- Initial page
SELECT TOP 10
    EmployeeId,
    Name,
    Salary
FROM Employee
ORDER BY EmployeeId;

-- Next page (using last EmployeeId from previous page)
DECLARE @LastEmployeeId INT = 10;

SELECT TOP 10
    EmployeeId,
    Name,
    Salary
FROM Employee
WHERE EmployeeId > @LastEmployeeId
ORDER BY EmployeeId;
```

---

### 10.5 Lazy vs Eager Loading (EF Core)

**Problem:**
Demonstrate the difference between lazy loading and eager loading in Entity Framework Core.

**Solution:**

```csharp
// Models
public class Department
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; } = null!;
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}

public class Project
{
    public int ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

// 1. Lazy Loading (Load on Access)
public class LazyLoadingExample
{
    private readonly ApplicationDbContext _context;
    
    public LazyLoadingExample(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Employee> GetEmployeeWithLazyLoading(int id)
    {
        // Only loads Employee
        var employee = await _context.Employees.FindAsync(id);
        
        // Department loaded on access (separate query)
        var departmentName = employee.Department.Name;
        
        // Projects loaded on access (separate query)
        var projectCount = employee.Projects.Count;
        
        return employee;
        // Result: 3 database queries (1 + 1 + 1)
    }
}

// Enable Lazy Loading in DbContext
public class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Enable lazy loading
        optionsBuilder.UseLazyLoadingProxies();
    }
}

// 2. Eager Loading (Load with Include)
public class EagerLoadingExample
{
    private readonly ApplicationDbContext _context;
    
    public EagerLoadingExample(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // Single Query with Include
    public async Task<Employee> GetEmployeeWithEagerLoading(int id)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Projects)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);
        // Result: 1 database query with JOINs
    }
    
    // Multiple Levels
    public async Task<List<Department>> GetDepartmentsWithEmployeesAndProjects()
    {
        return await _context.Departments
            .Include(d => d.Employees)
                .ThenInclude(e => e.Projects)
            .ToListAsync();
    }
    
    // Filtered Include (EF Core 5+)
    public async Task<List<Department>> GetDepartmentsWithFilteredEmployees()
    {
        return await _context.Departments
            .Include(d => d.Employees.Where(e => e.Salary > 50000))
            .ToListAsync();
    }
}

// 3. Explicit Loading (Load on Demand)
public class ExplicitLoadingExample
{
    private readonly ApplicationDbContext _context;
    
    public ExplicitLoadingExample(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Employee> GetEmployeeWithExplicitLoading(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        
        if (employee != null)
        {
            // Explicitly load Department
            await _context.Entry(employee)
                .Reference(e => e.Department)
                .LoadAsync();
            
            // Explicitly load Projects
            await _context.Entry(employee)
                .Collection(e => e.Projects)
                .LoadAsync();
        }
        
        return employee;
    }
    
    // Load with Query
    public async Task<Employee> GetEmployeeWithFilteredProjects(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        
        if (employee != null)
        {
            // Load only active projects
            await _context.Entry(employee)
                .Collection(e => e.Projects)
                .Query()
                .Where(p => p.IsActive)
                .LoadAsync();
        }
        
        return employee;
    }
}

// 4. Projection (Select Only What You Need)
public class ProjectionExample
{
    private readonly ApplicationDbContext _context;
    
    public ProjectionExample(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<EmployeeDto>> GetEmployeesProjection()
    {
        return await _context.Employees
            .Select(e => new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                DepartmentName = e.Department.Name,
                ProjectCount = e.Projects.Count
            })
            .ToListAsync();
        // Most efficient - only selected columns
    }
}

public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public int ProjectCount { get; set; }
}
```

**Comparison:**

| Method | Queries | When to Use | Pros | Cons |
|--------|---------|-------------|------|------|
| **Lazy** | Multiple (N+1) | Unknown needs | Flexible | Performance issues |
| **Eager** | Single (JOINs) | Known needs | Fast, efficient | May load too much |
| **Explicit** | On-demand | Conditional | Control | More code |
| **Projection** | Single (SELECT) | DTOs | Most efficient | Read-only |

---

### 10.6 Writing Optimized LINQ Queries

**Problem:**
Write efficient LINQ queries that translate to optimized SQL.

**Solution:**

```csharp
// BAD: Loads all data into memory, then filters
public async Task<List<Employee>> GetHighSalaryEmployeesBad()
{
    var allEmployees = await _context.Employees.ToListAsync();
    return allEmployees.Where(e => e.Salary > 100000).ToList();
    // SQL: SELECT * FROM Employees (all rows)
    // Filtering done in memory
}

// GOOD: Filters in database
public async Task<List<Employee>> GetHighSalaryEmployeesGood()
{
    return await _context.Employees
        .Where(e => e.Salary > 100000)
        .ToListAsync();
    // SQL: SELECT * FROM Employees WHERE Salary > 100000
}

// BAD: Multiple database calls
public async Task<decimal> GetAverageSalaryBad()
{
    var employees = await _context.Employees.ToListAsync();
    return employees.Average(e => e.Salary);
}

// GOOD: Single aggregation query
public async Task<decimal> GetAverageSalaryGood()
{
    return await _context.Employees.AverageAsync(e => e.Salary);
    // SQL: SELECT AVG(Salary) FROM Employees
}

// BAD: Checking existence with Count
public async Task<bool> EmployeeExistsBad(string email)
{
    var count = await _context.Employees
        .Where(e => e.Email == email)
        .CountAsync();
    return count > 0;
    // SQL: SELECT COUNT(*) FROM Employees WHERE Email = @email
}

// GOOD: Using AnyAsync
public async Task<bool> EmployeeExistsGood(string email)
{
    return await _context.Employees
        .AnyAsync(e => e.Email == email);
    // SQL: SELECT CASE WHEN EXISTS(...) THEN 1 ELSE 0 END
}

// BAD: Loading navigation property in loop (N+1 problem)
public async Task<List<string>> GetDepartmentNamesBad(List<int> employeeIds)
{
    var names = new List<string>();
    
    foreach (var id in employeeIds)
    {
        var employee = await _context.Employees.FindAsync(id);
        await _context.Entry(employee).Reference(e => e.Department).LoadAsync();
        names.Add(employee.Department.Name);
    }
    
    return names;
    // N+1 queries: 1 + N (one query per employee)
}

// GOOD: Single query with Include
public async Task<List<string>> GetDepartmentNamesGood(List<int> employeeIds)
{
    return await _context.Employees
        .Where(e => employeeIds.Contains(e.EmployeeId))
        .Include(e => e.Department)
        .Select(e => e.Department.Name)
        .ToListAsync();
    // Single query with JOIN
}

// BAD: Using FirstOrDefault unnecessarily
public async Task<int> GetTotalEmployeesBad()
{
    var firstEmployee = await _context.Employees.FirstOrDefaultAsync();
    return await _context.Employees.CountAsync();
}

// GOOD: Direct count
public async Task<int> GetTotalEmployeesGood()
{
    return await _context.Employees.CountAsync();
}

// Optimized Complex Query
public async Task<List<DepartmentSummaryDto>> GetDepartmentSummaries()
{
    return await _context.Departments
        .Select(d => new DepartmentSummaryDto
        {
            DepartmentId = d.DepartmentId,
            Name = d.Name,
            EmployeeCount = d.Employees.Count,
            AverageSalary = d.Employees.Average(e => e.Salary),
            TotalSalary = d.Employees.Sum(e => e.Salary),
            HighestPaid = d.Employees
                .OrderByDescending(e => e.Salary)
                .Select(e => e.Name)
                .FirstOrDefault()
        })
        .ToListAsync();
    // Single efficient query with GROUP BY
}

// Using AsNoTracking for Read-Only Queries
public async Task<List<Employee>> GetEmployeesReadOnly()
{
    return await _context.Employees
        .AsNoTracking() // Faster, no change tracking overhead
        .Where(e => e.IsActive)
        .ToListAsync();
}

// Compiled Queries for Repeated Use
private static readonly Func<ApplicationDbContext, int, Task<Employee?>> 
    _getEmployeeById = EF.CompileAsyncQuery(
        (ApplicationDbContext context, int id) =>
            context.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.EmployeeId == id));

public async Task<Employee?> GetEmployeeByIdCompiled(int id)
{
    return await _getEmployeeById(_context, id);
    // Faster subsequent executions
}
```

---

### 10.7 Handling N+1 Problem

**Problem:**
Identify and fix the N+1 query problem in Entity Framework.

**N+1 Problem Example:**

```csharp
// PROBLEM: N+1 Queries
public async Task<List<string>> GetDepartmentEmployeeNames()
{
    var departments = await _context.Departments.ToListAsync(); // 1 query
    
    var result = new List<string>();
    foreach (var dept in departments) // N queries (one per department)
    {
        var employeeNames = await _context.Employees
            .Where(e => e.DepartmentId == dept.DepartmentId)
            .Select(e => e.Name)
            .ToListAsync();
        
        result.AddRange(employeeNames);
    }
    
    return result;
}
// Total: 1 + N queries (1 for departments + 1 for each department's employees)
```

**Solution 1: Eager Loading with Include**

```csharp
public async Task<List<string>> GetDepartmentEmployeeNamesFixed1()
{
    var departments = await _context.Departments
        .Include(d => d.Employees) // Load employees with departments
        .ToListAsync();
    
    return departments
        .SelectMany(d => d.Employees)
        .Select(e => e.Name)
        .ToList();
    // Total: 1 query with JOIN
}
```

**Solution 2: Projection (Best Performance)**

```csharp
public async Task<List<string>> GetDepartmentEmployeeNamesFixed2()
{
    return await _context.Departments
        .SelectMany(d => d.Employees)
        .Select(e => e.Name)
        .ToListAsync();
    // Total: 1 query, only selects needed columns
}
```

**Solution 3: Split Query (EF Core 5+)**

```csharp
public async Task<List<Department>> GetDepartmentsWithEmployeesSplitQuery()
{
    return await _context.Departments
        .Include(d => d.Employees)
        .AsSplitQuery() // Uses 2 efficient queries instead of large JOIN
        .ToListAsync();
    // 2 queries: one for departments, one for all employees
    // Better for large result sets
}
```

**Real-World Example: Blog Posts with Comments**

```csharp
public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int BlogPostId { get; set; }
    public virtual BlogPost BlogPost { get; set; } = null!;
}

// N+1 PROBLEM
public async Task<List<BlogPostDto>> GetBlogPostsBad()
{
    var posts = await _context.BlogPosts.ToListAsync(); // 1 query
    
    var result = new List<BlogPostDto>();
    foreach (var post in posts) // N queries
    {
        var commentCount = await _context.Comments
            .CountAsync(c => c.BlogPostId == post.Id);
        
        result.Add(new BlogPostDto
        {
            Id = post.Id,
            Title = post.Title,
            CommentCount = commentCount
        });
    }
    
    return result;
}

// SOLUTION 1: Eager Loading
public async Task<List<BlogPostDto>> GetBlogPostsGood1()
{
    return await _context.BlogPosts
        .Include(p => p.Comments)
        .Select(p => new BlogPostDto
        {
            Id = p.Id,
            Title = p.Title,
            CommentCount = p.Comments.Count
        })
        .ToListAsync();
    // 1 query with JOIN
}

// SOLUTION 2: GroupJoin in Single Query
public async Task<List<BlogPostDto>> GetBlogPostsGood2()
{
    return await _context.BlogPosts
        .GroupJoin(
            _context.Comments,
            post => post.Id,
            comment => comment.BlogPostId,
            (post, comments) => new BlogPostDto
            {
                Id = post.Id,
                Title = post.Title,
                CommentCount = comments.Count()
            })
        .ToListAsync();
}

// SOLUTION 3: Projection (Most Efficient)
public async Task<List<BlogPostDto>> GetBlogPostsGood3()
{
    return await _context.BlogPosts
        .Select(p => new BlogPostDto
        {
            Id = p.Id,
            Title = p.Title,
            CommentCount = p.Comments.Count
        })
        .ToListAsync();
    // SQL: SELECT Id, Title, (SELECT COUNT(*) FROM Comments WHERE...) FROM BlogPosts
}
```

**Detection Tools:**

```csharp
// Enable query logging to detect N+1
public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
}

// MiniProfiler for ASP.NET Core
public void ConfigureServices(IServiceCollection services)
{
    services.AddMiniProfiler(options =>
    {
        options.RouteBasePath = "/profiler";
    }).AddEntityFramework();
}
```

---

## 11. Microservices & Distributed Systems

### 11.1 Implement Retry with Polly

**Problem:**
Implement retry logic with exponential backoff for resilient microservice communication.

**Solution:**

```csharp
// Install NuGet Packages:
// Polly
// Microsoft.Extensions.Http.Polly

// 1. Basic Retry Policy
public class RetryPolicyExample
{
    public async Task<string> CallApiWithRetry(string url)
    {
        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry {retryCount} after {timeSpan.TotalSeconds}s due to: {exception.Message}");
                });
        
        using var httpClient = new HttpClient();
        
        return await retryPolicy.ExecuteAsync(async () =>
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        });
    }
}

// 2. Retry with Exponential Backoff
public class ExponentialBackoffRetry
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ExponentialBackoffRetry> _logger;
    
    public ExponentialBackoffRetry(
        IHttpClientFactory httpClientFactory,
        ILogger<ExponentialBackoffRetry> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    public async Task<T?> ExecuteWithRetryAsync<T>(
        Func<HttpClient, Task<T>> action,
        string clientName = "default")
    {
        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .Or<TaskCanceledException>()
            .WaitAndRetryAsync(
                retryCount: 5,
                sleepDurationProvider: retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + 
                    TimeSpan.FromMilliseconds(Random.Shared.Next(0, 1000)), // Jitter
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogWarning(
                        "Retry {RetryCount} of {ClientName} after {Delay}ms due to: {Error}",
                        retryCount,
                        clientName,
                        timeSpan.TotalMilliseconds,
                        exception.Message);
                });
        
        var client = _httpClientFactory.CreateClient(clientName);
        
        return await retryPolicy.ExecuteAsync(() => action(client));
    }
}

// 3. Configuring HttpClient with Polly in Program.cs
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add Polly policies to HttpClient
        builder.Services.AddHttpClient("OrderService", client =>
        {
            client.BaseAddress = new Uri("https://order-service.example.com");
            client.Timeout = TimeSpan.FromSeconds(30);
        })
        .AddTransientHttpErrorPolicy(policyBuilder =>
            policyBuilder.WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryCount, context) =>
                {
                    Console.WriteLine($"Retrying... Attempt {retryCount}");
                }))
        .AddTransientHttpErrorPolicy(policyBuilder =>
            policyBuilder.CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30)));
        
        var app = builder.Build();
        app.Run();
    }
}

// 4. Advanced Retry with Different Strategies
public class AdvancedRetryStrategies
{
    // Retry only on specific status codes
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicyForSpecificErrors()
    {
        return Policy
            .HandleResult<HttpResponseMessage>(r => 
                r.StatusCode == System.Net.HttpStatusCode.RequestTimeout ||
                r.StatusCode == System.Net.HttpStatusCode.TooManyRequests ||
                (int)r.StatusCode >= 500)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
    
    // Retry with timeout
    public static IAsyncPolicy GetRetryWithTimeout()
    {
        var timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromSeconds(10));
        
        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(3, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        
        return Policy.WrapAsync(retryPolicy, timeoutPolicy);
    }
    
    // Conditional retry based on result
    public static IAsyncPolicy<HttpResponseMessage> GetConditionalRetryPolicy()
    {
        return Policy<HttpResponseMessage>
            .HandleResult(r => !r.IsSuccessStatusCode)
            .OrResult(r => r.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: (retryAttempt, response, context) =>
                {
                    // Use Retry-After header if present
                    if (response.Result?.Headers.RetryAfter?.Delta.HasValue == true)
                    {
                        return response.Result.Headers.RetryAfter.Delta.Value;
                    }
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                },
                onRetryAsync: async (response, timespan, retryCount, context) =>
                {
                    await Task.CompletedTask;
                    Console.WriteLine($"Retry {retryCount} after {timespan.TotalSeconds}s");
                });
    }
}

// 5. Service Class Using Retry
public interface IOrderService
{
    Task<Order> GetOrderAsync(int orderId);
    Task<bool> CreateOrderAsync(Order order);
}

public class OrderService : IOrderService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<OrderService> _logger;
    private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;
    
    public OrderService(
        IHttpClientFactory httpClientFactory,
        ILogger<OrderService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        
        _retryPolicy = Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryCount, context) =>
                {
                    _logger.LogWarning(
                        "Retry {RetryCount} for {Url} after {Delay}ms",
                        retryCount,
                        outcome.Result?.RequestMessage?.RequestUri,
                        timespan.TotalMilliseconds);
                });
    }
    
    public async Task<Order> GetOrderAsync(int orderId)
    {
        var client = _httpClientFactory.CreateClient("OrderService");
        
        var response = await _retryPolicy.ExecuteAsync(() =>
            client.GetAsync($"/api/orders/{orderId}"));
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<Order>() 
            ?? throw new Exception("Failed to deserialize order");
    }
    
    public async Task<bool> CreateOrderAsync(Order order)
    {
        var client = _httpClientFactory.CreateClient("OrderService");
        
        var response = await _retryPolicy.ExecuteAsync(() =>
            client.PostAsJsonAsync("/api/orders", order));
        
        return response.IsSuccessStatusCode;
    }
}

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
}
```

---

### 11.2 Circuit Breaker Example

**Problem:**
Implement circuit breaker pattern to prevent cascading failures in microservices.

**Solution:**

```csharp
// 1. Basic Circuit Breaker
public class CircuitBreakerExample
{
    private readonly IAsyncPolicy<HttpResponseMessage> _circuitBreakerPolicy;
    
    public CircuitBreakerExample()
    {
        _circuitBreakerPolicy = Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 3, // Open after 3 failures
                durationOfBreak: TimeSpan.FromSeconds(30), // Stay open for 30s
                onBreak: (result, timespan) =>
                {
                    Console.WriteLine($"Circuit breaker opened for {timespan.TotalSeconds}s");
                },
                onReset: () =>
                {
                    Console.WriteLine("Circuit breaker reset");
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine("Circuit breaker is half-open");
                });
    }
    
    public async Task<string> CallServiceAsync(string url)
    {
        using var client = new HttpClient();
        
        var response = await _circuitBreakerPolicy.ExecuteAsync(() =>
            client.GetAsync(url));
        
        return await response.Content.ReadAsStringAsync();
    }
}

// 2. Combined Retry + Circuit Breaker
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddHttpClient("PaymentService", client =>
        {
            client.BaseAddress = new Uri("https://payment-service.example.com");
        })
        // Retry policy (inner)
        .AddTransientHttpErrorPolicy(policyBuilder =>
            policyBuilder.WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
        // Circuit breaker policy (outer)
        .AddTransientHttpErrorPolicy(policyBuilder =>
            policyBuilder.CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30)));
        
        var app = builder.Build();
        app.Run();
    }
}

// 3. Advanced Circuit Breaker with Fallback
public class PaymentService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<PaymentService> _logger;
    private readonly IAsyncPolicy<PaymentResult> _policy;
    
    public PaymentService(
        IHttpClientFactory httpClientFactory,
        ILogger<PaymentService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        
        // Fallback policy
        var fallbackPolicy = Policy<PaymentResult>
            .Handle<BrokenCircuitException>()
            .Or<HttpRequestException>()
            .FallbackAsync(
                fallbackValue: new PaymentResult 
                { 
                    Success = false, 
                    Message = "Service temporarily unavailable" 
                },
                onFallbackAsync: async (result, context) =>
                {
                    _logger.LogWarning("Fallback triggered for payment processing");
                    await Task.CompletedTask;
                });
        
        // Circuit breaker policy
        var circuitBreakerPolicy = Policy<PaymentResult>
            .Handle<HttpRequestException>()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromMinutes(1),
                onBreak: (result, duration) =>
                {
                    _logger.LogError("Circuit breaker opened for {Duration}s", 
                        duration.TotalSeconds);
                },
                onReset: () =>
                {
                    _logger.LogInformation("Circuit breaker reset");
                });
        
        // Retry policy
        var retryPolicy = Policy<PaymentResult>
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        
        // Combine policies: Fallback -> CircuitBreaker -> Retry
        _policy = Policy.WrapAsync(fallbackPolicy, circuitBreakerPolicy, retryPolicy);
    }
    
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string cardNumber)
    {
        return await _policy.ExecuteAsync(async () =>
        {
            var client = _httpClientFactory.CreateClient("PaymentService");
            
            var request = new PaymentRequest
            {
                Amount = amount,
                CardNumber = cardNumber
            };
            
            var response = await client.PostAsJsonAsync("/api/payments/process", request);
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<PaymentResult>()
                ?? throw new Exception("Failed to deserialize payment result");
        });
    }
}

public class PaymentRequest
{
    public decimal Amount { get; set; }
    public string CardNumber { get; set; } = string.Empty;
}

public class PaymentResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
}

// 4. Circuit Breaker State Monitoring
public interface ICircuitBreakerStateStore
{
    CircuitState GetState(string serviceName);
    void SetState(string serviceName, CircuitState state);
}

public enum CircuitState
{
    Closed,
    Open,
    HalfOpen
}

public class CircuitBreakerMonitoringService
{
    private readonly ICircuitBreakerStateStore _stateStore;
    private readonly ILogger<CircuitBreakerMonitoringService> _logger;
    
    public CircuitBreakerMonitoringService(
        ICircuitBreakerStateStore stateStore,
        ILogger<CircuitBreakerMonitoringService> logger)
    {
        _stateStore = stateStore;
        _logger = logger;
    }
    
    public IAsyncPolicy<HttpResponseMessage> CreateMonitoredCircuitBreaker(string serviceName)
    {
        return Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromMinutes(1),
                onBreak: (result, duration) =>
                {
                    _stateStore.SetState(serviceName, CircuitState.Open);
                    _logger.LogError(
                        "Circuit breaker OPENED for {Service} for {Duration}s",
                        serviceName,
                        duration.TotalSeconds);
                },
                onReset: () =>
                {
                    _stateStore.SetState(serviceName, CircuitState.Closed);
                    _logger.LogInformation(
                        "Circuit breaker CLOSED for {Service}",
                        serviceName);
                },
                onHalfOpen: () =>
                {
                    _stateStore.SetState(serviceName, CircuitState.HalfOpen);
                    _logger.LogInformation(
                        "Circuit breaker HALF-OPEN for {Service}",
                        serviceName);
                });
    }
}
```

---

### 11.3 Saga Pattern Flow

**Problem:**
Implement the Saga pattern for distributed transactions across microservices.

**Solution:**

```csharp
// Saga Pattern: Orchestration-Based

// 1. Saga State and Events
public enum SagaState
{
    NotStarted,
    OrderCreated,
    PaymentProcessed,
    InventoryReserved,
    ShipmentCreated,
    Completed,
    Failed,
    Compensating,
    Compensated
}

public abstract class SagaEvent
{
    public Guid SagaId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public class OrderCreatedEvent : SagaEvent
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
}

public class PaymentProcessedEvent : SagaEvent
{
    public string TransactionId { get; set; } = string.Empty;
}

public class PaymentFailedEvent : SagaEvent
{
    public string Reason { get; set; } = string.Empty;
}

// 2. Saga Orchestrator
public class OrderSaga
{
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;
    private readonly IInventoryService _inventoryService;
    private readonly IShippingService _shippingService;
    private readonly ILogger<OrderSaga> _logger;
    
    public Guid SagaId { get; private set; }
    public SagaState State { get; private set; }
    public OrderData Data { get; private set; }
    
    public OrderSaga(
        IOrderService orderService,
        IPaymentService paymentService,
        IInventoryService inventoryService,
        IShippingService shippingService,
        ILogger<OrderSaga> logger)
    {
        _orderService = orderService;
        _paymentService = paymentService;
        _inventoryService = inventoryService;
        _shippingService = shippingService;
        _logger = logger;
        
        SagaId = Guid.NewGuid();
        State = SagaState.NotStarted;
        Data = new OrderData();
    }
    
    public async Task<bool> ExecuteAsync(CreateOrderRequest request)
    {
        _logger.LogInformation("Starting saga {SagaId}", SagaId);
        
        try
        {
            // Step 1: Create Order
            await CreateOrderAsync(request);
            
            // Step 2: Process Payment
            await ProcessPaymentAsync();
            
            // Step 3: Reserve Inventory
            await ReserveInventoryAsync();
            
            // Step 4: Create Shipment
            await CreateShipmentAsync();
            
            State = SagaState.Completed;
            _logger.LogInformation("Saga {SagaId} completed successfully", SagaId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Saga {SagaId} failed, starting compensation", SagaId);
            await CompensateAsync();
            return false;
        }
    }
    
    private async Task CreateOrderAsync(CreateOrderRequest request)
    {
        Data.OrderId = await _orderService.CreateOrderAsync(request);
        State = SagaState.OrderCreated;
        _logger.LogInformation("Order {OrderId} created", Data.OrderId);
    }
    
    private async Task ProcessPaymentAsync()
    {
        var result = await _paymentService.ProcessPaymentAsync(
            Data.OrderId, 
            Data.Amount);
        
        if (!result.Success)
        {
            throw new PaymentException("Payment failed: " + result.Message);
        }
        
        Data.TransactionId = result.TransactionId;
        State = SagaState.PaymentProcessed;
        _logger.LogInformation("Payment processed: {TransactionId}", Data.TransactionId);
    }
    
    private async Task ReserveInventoryAsync()
    {
        var result = await _inventoryService.ReserveItemsAsync(
            Data.OrderId,
            Data.Items);
        
        if (!result.Success)
        {
            throw new InventoryException("Inventory reservation failed: " + result.Message);
        }
        
        Data.ReservationId = result.ReservationId;
        State = SagaState.InventoryReserved;
        _logger.LogInformation("Inventory reserved: {ReservationId}", Data.ReservationId);
    }
    
    private async Task CreateShipmentAsync()
    {
        var result = await _shippingService.CreateShipmentAsync(Data.OrderId);
        
        if (!result.Success)
        {
            throw new ShippingException("Shipment creation failed: " + result.Message);
        }
        
        Data.ShipmentId = result.ShipmentId;
        State = SagaState.ShipmentCreated;
        _logger.LogInformation("Shipment created: {ShipmentId}", Data.ShipmentId);
    }
    
    private async Task CompensateAsync()
    {
        State = SagaState.Compensating;
        
        // Compensate in reverse order
        if (State >= SagaState.ShipmentCreated)
        {
            await _shippingService.CancelShipmentAsync(Data.ShipmentId);
            _logger.LogInformation("Shipment cancelled: {ShipmentId}", Data.ShipmentId);
        }
        
        if (State >= SagaState.InventoryReserved)
        {
            await _inventoryService.ReleaseReservationAsync(Data.ReservationId);
            _logger.LogInformation("Inventory released: {ReservationId}", Data.ReservationId);
        }
        
        if (State >= SagaState.PaymentProcessed)
        {
            await _paymentService.RefundPaymentAsync(Data.TransactionId);
            _logger.LogInformation("Payment refunded: {TransactionId}", Data.TransactionId);
        }
        
        if (State >= SagaState.OrderCreated)
        {
            await _orderService.CancelOrderAsync(Data.OrderId);
            _logger.LogInformation("Order cancelled: {OrderId}", Data.OrderId);
        }
        
        State = SagaState.Compensated;
        _logger.LogInformation("Saga {SagaId} compensated", SagaId);
    }
}

public class OrderData
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public string TransactionId { get; set; } = string.Empty;
    public string ReservationId { get; set; } = string.Empty;
    public string ShipmentId { get; set; } = string.Empty;
}

public class CreateOrderRequest
{
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItem> Items { get; set; } = new();
    public decimal Amount { get; set; }
}

public class OrderItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

// Service Interfaces
public interface IOrderService
{
    Task<int> CreateOrderAsync(CreateOrderRequest request);
    Task CancelOrderAsync(int orderId);
}

public interface IPaymentService
{
    Task<PaymentResult> ProcessPaymentAsync(int orderId, decimal amount);
    Task RefundPaymentAsync(string transactionId);
}

public interface IInventoryService
{
    Task<InventoryResult> ReserveItemsAsync(int orderId, List<OrderItem> items);
    Task ReleaseReservationAsync(string reservationId);
}

public interface IShippingService
{
    Task<ShippingResult> CreateShipmentAsync(int orderId);
    Task CancelShipmentAsync(string shipmentId);
}

public class InventoryResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string ReservationId { get; set; } = string.Empty;
}

public class ShippingResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string ShipmentId { get; set; } = string.Empty;
}

// Exceptions
public class PaymentException : Exception
{
    public PaymentException(string message) : base(message) { }
}

public class InventoryException : Exception
{
    public InventoryException(string message) : base(message) { }
}

public class ShippingException : Exception
{
    public ShippingException(string message) : base(message) { }
}

// Usage
public class OrderController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    
    public OrderController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        using var scope = _serviceProvider.CreateScope();
        
        var saga = scope.ServiceProvider.GetRequiredService<OrderSaga>();
        var success = await saga.ExecuteAsync(request);
        
        if (success)
        {
            return Ok(new { orderId = saga.Data.OrderId, sagaId = saga.SagaId });
        }
        
        return BadRequest(new { message = "Order creation failed", sagaId = saga.SagaId });
    }
}
```

---

## 12. Real-World Coding Scenarios

### 12.1 Design Order Processing System

**Problem:**
Design a complete order processing system with validation, state management, and event handling.

**Solution:**

```csharp
// Order Entity with State Pattern
public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public enum OrderStatus
{
    Draft,
    Pending,
    Confirmed,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
}

// Order Service with Complete Business Logic
public interface IOrderProcessingService
{
    Task<Result<Order>> CreateOrderAsync(CreateOrderCommand command);
    Task<Result> ConfirmOrderAsync(int orderId);
    Task<Result> CancelOrderAsync(int orderId, string reason);
    Task<Result<Order>> GetOrderAsync(int orderId);
}

public class OrderProcessingService : IOrderProcessingService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IInventoryService _inventoryService;
    private readonly IPaymentService _paymentService;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<OrderProcessingService> _logger;
    
    public OrderProcessingService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IInventoryService inventoryService,
        IPaymentService paymentService,
        IEventPublisher eventPublisher,
        ILogger<OrderProcessingService> logger)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _inventoryService = inventoryService;
        _paymentService = paymentService;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }
    
    public async Task<Result<Order>> CreateOrderAsync(CreateOrderCommand command)
    {
        try
        {
            // Validate customer
            if (command.CustomerId <= 0)
                return Result<Order>.Failure("Invalid customer");
            
            // Validate items
            if (!command.Items.Any())
                return Result<Order>.Failure("Order must have at least one item");
            
            // Check product availability
            var productIds = command.Items.Select(i => i.ProductId).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds);
            
            if (products.Count != productIds.Count)
                return Result<Order>.Failure("Some products not found");
            
            // Check inventory
            foreach (var item in command.Items)
            {
                var available = await _inventoryService.CheckAvailabilityAsync(
                    item.ProductId, 
                    item.Quantity);
                
                if (!available)
                    return Result<Order>.Failure(
                        $"Insufficient inventory for product {item.ProductId}");
            }
            
            // Create order
            var order = new Order
            {
                OrderNumber = GenerateOrderNumber(),
                CustomerId = command.CustomerId,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                Items = command.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    ProductName = products.First(p => p.Id == i.ProductId).Name,
                    Quantity = i.Quantity,
                    UnitPrice = products.First(p => p.Id == i.ProductId).Price
                }).ToList()
            };
            
            order.TotalAmount = order.Items.Sum(i => i.TotalPrice);
            
            // Save order
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();
            
            // Publish event
            await _eventPublisher.PublishAsync(new OrderCreatedEvent
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                TotalAmount = order.TotalAmount,
                Timestamp = DateTime.UtcNow
            });
            
            _logger.LogInformation("Order {OrderId} created successfully", order.Id);
            
            return Result<Order>.Success(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order");
            return Result<Order>.Failure("Failed to create order");
        }
    }
    
    public async Task<Result> ConfirmOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        
        if (order == null)
            return Result.Failure("Order not found");
        
        if (order.Status != OrderStatus.Pending)
            return Result.Failure($"Cannot confirm order in {order.Status} status");
        
        // Process payment
        var paymentResult = await _paymentService.ProcessPaymentAsync(
            orderId, 
            order.TotalAmount);
        
        if (!paymentResult.Success)
            return Result.Failure("Payment failed: " + paymentResult.Message);
        
        // Reserve inventory
        foreach (var item in order.Items)
        {
            await _inventoryService.ReserveAsync(item.ProductId, item.Quantity);
        }
        
        // Update order status
        order.Status = OrderStatus.Confirmed;
        order.UpdatedAt = DateTime.UtcNow;
        
        await _orderRepository.UpdateAsync(order);
        await _orderRepository.SaveChangesAsync();
        
        // Publish event
        await _eventPublisher.PublishAsync(new OrderConfirmedEvent
        {
            OrderId = order.Id,
            Timestamp = DateTime.UtcNow
        });
        
        return Result.Success();
    }
    
    public async Task<Result> CancelOrderAsync(int orderId, string reason)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        
        if (order == null)
            return Result.Failure("Order not found");
        
        if (order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Cancelled)
            return Result.Failure($"Cannot cancel order in {order.Status} status");
        
        // Refund if payment was processed
        if (order.Status >= OrderStatus.Confirmed)
        {
            await _paymentService.RefundAsync(orderId);
        }
        
        // Release inventory
        if (order.Status >= OrderStatus.Confirmed)
        {
            foreach (var item in order.Items)
            {
                await _inventoryService.ReleaseAsync(item.ProductId, item.Quantity);
            }
        }
        
        order.Status = OrderStatus.Cancelled;
        order.UpdatedAt = DateTime.UtcNow;
        
        await _orderRepository.UpdateAsync(order);
        await _orderRepository.SaveChangesAsync();
        
        await _eventPublisher.PublishAsync(new OrderCancelledEvent
        {
            OrderId = order.Id,
            Reason = reason,
            Timestamp = DateTime.UtcNow
        });
        
        return Result.Success();
    }
    
    public async Task<Result<Order>> GetOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        
        if (order == null)
            return Result<Order>.Failure("Order not found");
        
        return Result<Order>.Success(order);
    }
    
    private string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
    }
}

// Commands and Events
public class CreateOrderCommand
{
    public int CustomerId { get; set; }
    public List<OrderItemCommand> Items { get; set; } = new();
}

public class OrderItemCommand
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderCreatedEvent
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime Timestamp { get; set; }
}

public class OrderConfirmedEvent
{
    public int OrderId { get; set; }
    public DateTime Timestamp { get; set; }
}

public class OrderCancelledEvent
{
    public int OrderId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}

// Result Pattern
public class Result
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    
    public static Result Success() => new Result { IsSuccess = true };
    public static Result Failure(string message) => new Result { IsSuccess = false, Message = message };
}

public class Result<T> : Result
{
    public T? Data { get; set; }
    
    public static Result<T> Success(T data) => new Result<T> { IsSuccess = true, Data = data };
    public new static Result<T> Failure(string message) => new Result<T> { IsSuccess = false, Message = message };
}
```

---

### 12.2 Handle Concurrent API Requests

**Problem:**
Handle concurrent requests safely with proper locking and race condition prevention.

**Solution:**

```csharp
// 1. Using Semaphore for Concurrency Control
public class ConcurrentRequestHandler
{
    private readonly SemaphoreSlim _semaphore;
    private readonly ILogger<ConcurrentRequestHandler> _logger;
    
    public ConcurrentRequestHandler(ILogger<ConcurrentRequestHandler> logger, int maxConcurrency = 10)
    {
        _semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
        _logger = logger;
    }
    
    public async Task<T> ExecuteWithConcurrencyLimitAsync<T>(Func<Task<T>> action)
    {
        await _semaphore.WaitAsync();
        
        try
        {
            return await action();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}

// 2. Preventing Double-Booking (e.g., Seat Reservation)
public class SeatReservationService
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly ILogger<SeatReservationService> _logger;
    
    public SeatReservationService(
        ApplicationDbContext context,
        IDistributedCache cache,
        ILogger<SeatReservationService> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }
    
    public async Task<Result<Reservation>> ReserveSeatAsync(int seatId, int userId)
    {
        var lockKey = $"seat:lock:{seatId}";
        var lockValue = Guid.NewGuid().ToString();
        
        try
        {
            // Distributed lock using Redis
            var lockAcquired = await TryAcquireLockAsync(lockKey, lockValue, TimeSpan.FromSeconds(10));
            
            if (!lockAcquired)
            {
                return Result<Reservation>.Failure("Seat is being reserved by another user");
            }
            
            // Check if seat is available
            var seat = await _context.Seats
                .Include(s => s.Reservations)
                .FirstOrDefaultAsync(s => s.Id == seatId);
            
            if (seat == null)
            {
                return Result<Reservation>.Failure("Seat not found");
            }
            
            if (seat.Reservations.Any(r => r.Status == ReservationStatus.Active))
            {
                return Result<Reservation>.Failure("Seat already reserved");
            }
            
            // Create reservation
            var reservation = new Reservation
            {
                SeatId = seatId,
                UserId = userId,
                Status = ReservationStatus.Active,
                ReservedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            };
            
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            
            return Result<Reservation>.Success(reservation);
        }
        finally
        {
            await ReleaseLockAsync(lockKey, lockValue);
        }
    }
    
    private async Task<bool> TryAcquireLockAsync(string key, string value, TimeSpan expiry)
    {
        try
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry
            };
            
            var existing = await _cache.GetStringAsync(key);
            
            if (existing != null)
                return false;
            
            await _cache.SetStringAsync(key, value, options);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    private async Task ReleaseLockAsync(string key, string value)
    {
        var existing = await _cache.GetStringAsync(key);
        
        if (existing == value)
        {
            await _cache.RemoveAsync(key);
        }
    }
}

// 3. Handling Concurrent Updates with Optimistic Locking
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; }
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}

public class ProductService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductService> _logger;
    private const int MaxRetries = 3;
    
    public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<Result> UpdateStockAsync(int productId, int quantity)
    {
        int retryCount = 0;
        
        while (retryCount < MaxRetries)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                
                if (product == null)
                    return Result.Failure("Product not found");
                
                if (product.Stock < quantity)
                    return Result.Failure("Insufficient stock");
                
                product.Stock -= quantity;
                
                await _context.SaveChangesAsync();
                
                return Result.Success();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                retryCount++;
                _logger.LogWarning(
                    "Concurrency conflict updating product {ProductId}, retry {RetryCount}",
                    productId, retryCount);
                
                if (retryCount >= MaxRetries)
                {
                    _logger.LogError(ex, "Max retries reached for product {ProductId}", productId);
                    return Result.Failure("Failed to update stock due to concurrent updates");
                }
                
                // Reload entity with current database values
                var entry = ex.Entries.Single();
                await entry.ReloadAsync();
            }
        }
        
        return Result.Failure("Unexpected error");
    }
}

// DbContext configuration for optimistic locking
public class ApplicationDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.RowVersion)
            .IsRowVersion();
    }
}

// 4. Batch Processing Concurrent Requests
public class BatchProcessor<T>
{
    private readonly SemaphoreSlim _semaphore;
    private readonly int _batchSize;
    
    public BatchProcessor(int maxConcurrency = 10, int batchSize = 100)
    {
        _semaphore = new SemaphoreSlim(maxConcurrency);
        _batchSize = batchSize;
    }
    
    public async Task<List<TResult>> ProcessBatchAsync<TResult>(
        List<T> items,
        Func<T, Task<TResult>> processor)
    {
        var results = new ConcurrentBag<TResult>();
        var batches = items.Chunk(_batchSize);
        
        foreach (var batch in batches)
        {
            var tasks = batch.Select(async item =>
            {
                await _semaphore.WaitAsync();
                try
                {
                    var result = await processor(item);
                    results.Add(result);
                }
                finally
                {
                    _semaphore.Release();
                }
            });
            
            await Task.WhenAll(tasks);
        }
        
        return results.ToList();
    }
}
```

---

### 12.3 Idempotent API Implementation

**Problem:**
Implement idempotent APIs to handle duplicate requests safely.

**Solution:**

```csharp
// 1. Idempotency Key Middleware
public class IdempotencyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDistributedCache _cache;
    private readonly ILogger<IdempotencyMiddleware> _logger;
    
    public IdempotencyMiddleware(
        RequestDelegate next,
        IDistributedCache cache,
        ILogger<IdempotencyMiddleware> logger)
    {
        _next = next;
        _cache = cache;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        // Only apply to POST, PUT, DELETE
        if (context.Request.Method == "GET" || context.Request.Method == "HEAD")
        {
            await _next(context);
            return;
        }
        
        // Get idempotency key from header
        if (!context.Request.Headers.TryGetValue("Idempotency-Key", out var idempotencyKey))
        {
            await _next(context);
            return;
        }
        
        var cacheKey = $"idempotency:{idempotencyKey}";
        
        // Check if request was already processed
        var cachedResponse = await _cache.GetStringAsync(cacheKey);
        
        if (cachedResponse != null)
        {
            _logger.LogInformation("Returning cached response for idempotency key: {Key}", idempotencyKey);
            
            var cached = JsonSerializer.Deserialize<CachedResponse>(cachedResponse);
            
            context.Response.StatusCode = cached!.StatusCode;
            context.Response.ContentType = "application/json";
            
            await context.Response.WriteAsync(cached.Body);
            return;
        }
        
        // Capture response
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;
        
        await _next(context);
        
        // Cache successful responses
        if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            
            var cached = new CachedResponse
            {
                StatusCode = context.Response.StatusCode,
                Body = responseText
            };
            
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            };
            
            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(cached),
                cacheOptions);
        }
        
        await responseBody.CopyToAsync(originalBodyStream);
    }
}

public class CachedResponse
{
    public int StatusCode { get; set; }
    public string Body { get; set; } = string.Empty;
}

// 2. Idempotent Payment Processing
public class PaymentService
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly ILogger<PaymentService> _logger;
    
    public PaymentService(
        ApplicationDbContext context,
        IDistributedCache cache,
        ILogger<PaymentService> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }
    
    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request, string idempotencyKey)
    {
        var lockKey = $"payment:lock:{idempotencyKey}";
        var resultKey = $"payment:result:{idempotencyKey}";
        
        // Check if payment already processed
        var cachedResult = await _cache.GetStringAsync(resultKey);
        
        if (cachedResult != null)
        {
            _logger.LogInformation("Payment already processed: {IdempotencyKey}", idempotencyKey);
            return JsonSerializer.Deserialize<PaymentResult>(cachedResult)!;
        }
        
        // Acquire lock
        var lockValue = Guid.NewGuid().ToString();
        var lockAcquired = await TryAcquireLockAsync(lockKey, lockValue, TimeSpan.FromMinutes(1));
        
        if (!lockAcquired)
        {
            // Another request is processing, wait and retry
            await Task.Delay(100);
            cachedResult = await _cache.GetStringAsync(resultKey);
            
            if (cachedResult != null)
            {
                return JsonSerializer.Deserialize<PaymentResult>(cachedResult)!;
            }
            
            return new PaymentResult
            {
                Success = false,
                Message = "Payment is being processed"
            };
        }
        
        try
        {
            // Check for existing payment
            var existingPayment = await _context.Payments
                .FirstOrDefaultAsync(p => p.IdempotencyKey == idempotencyKey);
            
            if (existingPayment != null)
            {
                var result = new PaymentResult
                {
                    Success = true,
                    TransactionId = existingPayment.TransactionId,
                    Message = "Payment already processed"
                };
                
                await CacheResultAsync(resultKey, result);
                return result;
            }
            
            // Process payment
            var payment = new Payment
            {
                IdempotencyKey = idempotencyKey,
                Amount = request.Amount,
                Currency = request.Currency,
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
            
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            
            // Call payment gateway
            var transactionId = await ProcessWithGatewayAsync(request);
            
            payment.TransactionId = transactionId;
            payment.Status = PaymentStatus.Completed;
            payment.CompletedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            
            var successResult = new PaymentResult
            {
                Success = true,
                TransactionId = transactionId,
                Message = "Payment processed successfully"
            };
            
            // Cache result
            await CacheResultAsync(resultKey, successResult);
            
            return successResult;
        }
        finally
        {
            await ReleaseLockAsync(lockKey, lockValue);
        }
    }
    
    private async Task<bool> TryAcquireLockAsync(string key, string value, TimeSpan expiry)
    {
        try
        {
            var existing = await _cache.GetStringAsync(key);
            if (existing != null) return false;
            
            await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry
            });
            
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    private async Task ReleaseLockAsync(string key, string value)
    {
        var existing = await _cache.GetStringAsync(key);
        if (existing == value)
        {
            await _cache.RemoveAsync(key);
        }
    }
    
    private async Task CacheResultAsync(string key, PaymentResult result)
    {
        await _cache.SetStringAsync(
            key,
            JsonSerializer.Serialize(result),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
            });
    }
    
    private async Task<string> ProcessWithGatewayAsync(PaymentRequest request)
    {
        // Simulate payment gateway call
        await Task.Delay(100);
        return Guid.NewGuid().ToString();
    }
}

public class Payment
{
    public int Id { get; set; }
    public string IdempotencyKey { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed
}

public class PaymentRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string CardNumber { get; set; } = string.Empty;
}

public class PaymentResult
{
    public bool Success { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

// Controller Usage
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly PaymentService _paymentService;
    
    public PaymentsController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpPost]
    public async Task<ActionResult<PaymentResult>> ProcessPayment(
        [FromBody] PaymentRequest request,
        [FromHeader(Name = "Idempotency-Key")] string idempotencyKey)
    {
        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            return BadRequest(new { error = "Idempotency-Key header is required" });
        }
        
        var result = await _paymentService.ProcessPaymentAsync(request, idempotencyKey);
        
        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }
}
```

---

## 13. System Design

### 13.1 Design URL Shortener

**Problem:**
Design a URL shortening service like bit.ly.

**Solution:**

```csharp
// URL Shortener Service
public class UrlShortenerService
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly ILogger<UrlShortenerService> _logger;
    private const string BaseUrl = "https://short.url/";
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int ShortCodeLength = 7;
    
    public UrlShortenerService(
        ApplicationDbContext context,
        IDistributedCache cache,
        ILogger<UrlShortenerService> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }
    
    public async Task<ShortenedUrl> CreateShortUrlAsync(string longUrl, string? customAlias = null, int? expiryDays = null)
    {
        // Validate URL
        if (!Uri.TryCreate(longUrl, UriKind.Absolute, out _))
        {
            throw new ArgumentException("Invalid URL");
        }
        
        // Check if URL already shortened
        var existing = await _context.ShortenedUrls
            .FirstOrDefaultAsync(u => u.LongUrl == longUrl && !u.IsExpired);
        
        if (existing != null)
        {
            return existing;
        }
        
        string shortCode;
        
        if (!string.IsNullOrWhiteSpace(customAlias))
        {
            // Validate custom alias
            if (customAlias.Length < 3 || customAlias.Length > 20)
            {
                throw new ArgumentException("Custom alias must be between 3 and 20 characters");
            }
            
            // Check if alias available
            var aliasExists = await _context.ShortenedUrls
                .AnyAsync(u => u.ShortCode == customAlias);
            
            if (aliasExists)
            {
                throw new ArgumentException("Custom alias already taken");
            }
            
            shortCode = customAlias;
        }
        else
        {
            // Generate unique short code
            shortCode = await GenerateUniqueShortCodeAsync();
        }
        
        var shortenedUrl = new ShortenedUrl
        {
            LongUrl = longUrl,
            ShortCode = shortCode,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = expiryDays.HasValue 
                ? DateTime.UtcNow.AddDays(expiryDays.Value) 
                : null,
            Clicks = 0
        };
        
        _context.ShortenedUrls.Add(shortenedUrl);
        await _context.SaveChangesAsync();
        
        // Cache the mapping
        await CacheShortUrlAsync(shortCode, longUrl);
        
        _logger.LogInformation("Created short URL: {ShortCode} -> {LongUrl}", shortCode, longUrl);
        
        return shortenedUrl;
    }
    
    public async Task<string?> GetLongUrlAsync(string shortCode)
    {
        // Try cache first
        var cacheKey = $"url:{shortCode}";
        var cached = await _cache.GetStringAsync(cacheKey);
        
        if (cached != null)
        {
            await IncrementClickCountAsync(shortCode);
            return cached;
        }
        
        // Query database
        var shortenedUrl = await _context.ShortenedUrls
            .FirstOrDefaultAsync(u => u.ShortCode == shortCode);
        
        if (shortenedUrl == null || shortenedUrl.IsExpired)
        {
            return null;
        }
        
        // Cache for future requests
        await CacheShortUrlAsync(shortCode, shortenedUrl.LongUrl);
        
        // Increment click count asynchronously
        _ = IncrementClickCountAsync(shortCode);
        
        return shortenedUrl.LongUrl;
    }
    
    public async Task<UrlStatistics> GetStatisticsAsync(string shortCode)
    {
        var shortenedUrl = await _context.ShortenedUrls
            .Include(u => u.ClickEvents)
            .FirstOrDefaultAsync(u => u.ShortCode == shortCode);
        
        if (shortenedUrl == null)
        {
            throw new NotFoundException("Short URL not found");
        }
        
        return new UrlStatistics
        {
            ShortCode = shortCode,
            LongUrl = shortenedUrl.LongUrl,
            TotalClicks = shortenedUrl.Clicks,
            CreatedAt = shortenedUrl.CreatedAt,
            ExpiresAt = shortenedUrl.ExpiresAt,
            ClicksByDate = shortenedUrl.ClickEvents
                .GroupBy(c => c.ClickedAt.Date)
                .Select(g => new ClickStat
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(c => c.Date)
                .ToList(),
            ClicksByCountry = shortenedUrl.ClickEvents
                .GroupBy(c => c.Country)
                .Select(g => new CountryStat
                {
                    Country = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(c => c.Count)
                .Take(10)
                .ToList()
        };
    }
    
    private async Task<string> GenerateUniqueShortCodeAsync()
    {
        for (int attempts = 0; attempts < 5; attempts++)
        {
            var shortCode = GenerateRandomCode();
            
            var exists = await _context.ShortenedUrls
                .AnyAsync(u => u.ShortCode == shortCode);
            
            if (!exists)
            {
                return shortCode;
            }
        }
        
        throw new Exception("Failed to generate unique short code");
    }
    
    private string GenerateRandomCode()
    {
        var random = new Random();
        var code = new char[ShortCodeLength];
        
        for (int i = 0; i < ShortCodeLength; i++)
        {
            code[i] = Alphabet[random.Next(Alphabet.Length)];
        }
        
        return new string(code);
    }
    
    private async Task CacheShortUrlAsync(string shortCode, string longUrl)
    {
        var cacheKey = $"url:{shortCode}";
        await _cache.SetStringAsync(
            cacheKey,
            longUrl,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });
    }
    
    private async Task IncrementClickCountAsync(string shortCode)
    {
        try
        {
            var url = await _context.ShortenedUrls
                .FirstOrDefaultAsync(u => u.ShortCode == shortCode);
            
            if (url != null)
            {
                url.Clicks++;
                url.LastAccessedAt = DateTime.UtcNow;
                
                // Log click event
                _context.ClickEvents.Add(new ClickEvent
                {
                    ShortenedUrlId = url.Id,
                    ClickedAt = DateTime.UtcNow,
                    Country = "US" // Would come from IP geolocation
                });
                
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to increment click count for {ShortCode}", shortCode);
        }
    }
}

// Models
public class ShortenedUrl
{
    public int Id { get; set; }
    public string LongUrl { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime? LastAccessedAt { get; set; }
    public long Clicks { get; set; }
    public bool IsExpired => ExpiresAt.HasValue && ExpiresAt.Value < DateTime.UtcNow;
    public ICollection<ClickEvent> ClickEvents { get; set; } = new List<ClickEvent>();
}

public class ClickEvent
{
    public int Id { get; set; }
    public int ShortenedUrlId { get; set; }
    public DateTime ClickedAt { get; set; }
    public string Country { get; set; } = string.Empty;
    public ShortenedUrl ShortenedUrl { get; set; } = null!;
}

public class UrlStatistics
{
    public string ShortCode { get; set; } = string.Empty;
    public string LongUrl { get; set; } = string.Empty;
    public long TotalClicks { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public List<ClickStat> ClicksByDate { get; set; } = new();
    public List<CountryStat> ClicksByCountry { get; set; } = new();
}

public class ClickStat
{
    public DateTime Date { get; set; }
    public int Count { get; set; }
}

public class CountryStat
{
    public string Country { get; set; } = string.Empty;
    public int Count { get; set; }
}

// Controller
[ApiController]
[Route("api/[controller]")]
public class UrlShortenerController : ControllerBase
{
    private readonly UrlShortenerService _urlShortener;
    
    public UrlShortenerController(UrlShortenerService urlShortener)
    {
        _urlShortener = urlShortener;
    }
    
    [HttpPost("shorten")]
    public async Task<ActionResult<ShortenedUrl>> CreateShortUrl([FromBody] CreateShortUrlRequest request)
    {
        var result = await _urlShortener.CreateShortUrlAsync(
            request.LongUrl,
            request.CustomAlias,
            request.ExpiryDays);
        
        return Ok(result);
    }
    
    [HttpGet("{shortCode}")]
    public async Task<IActionResult> RedirectToLongUrl(string shortCode)
    {
        var longUrl = await _urlShortener.GetLongUrlAsync(shortCode);
        
        if (longUrl == null)
        {
            return NotFound();
        }
        
        return Redirect(longUrl);
    }
    
    [HttpGet("{shortCode}/stats")]
    public async Task<ActionResult<UrlStatistics>> GetStatistics(string shortCode)
    {
        var stats = await _urlShortener.GetStatisticsAsync(shortCode);
        return Ok(stats);
    }
}

public class CreateShortUrlRequest
{
    public string LongUrl { get; set; } = string.Empty;
    public string? CustomAlias { get; set; }
    public int? ExpiryDays { get; set; }
}
```

---

## 14. Advanced Database & Stored Procedures

### 14.1 Nested Stored Procedures with Temp Tables and Variables

**Question:** I have 3 stored procedures called nested one inside one with temp table and temp variable. Will the temp table and temp variable be accessible in the 3rd stored procedure?

**Answer:**

**Temp Tables (#Table):**
- ✅ **YES** - Temp tables created in an outer stored procedure are accessible in nested stored procedures
- Temp tables have session scope and are visible to all nested levels
- They exist for the duration of the session or until explicitly dropped

**Temp Variables (@Table):**
- ❌ **NO** - Table variables have limited scope and are NOT accessible in nested stored procedures
- Table variables are local to the batch, function, or stored procedure where they are defined
- They cannot be passed to nested procedures unless passed as parameters (not common)

**Example:**

```sql
-- Stored Procedure 1 (Outer)
CREATE PROCEDURE SP1
AS
BEGIN
    -- Create temp table (accessible in nested SPs)
    CREATE TABLE #TempTable
    (
        Id INT,
        Name NVARCHAR(100)
    );
    
    INSERT INTO #TempTable VALUES (1, 'John'), (2, 'Jane');
    
    -- Declare table variable (NOT accessible in nested SPs)
    DECLARE @TableVariable TABLE
    (
        Id INT,
        Status NVARCHAR(50)
    );
    
    INSERT INTO @TableVariable VALUES (1, 'Active'), (2, 'Inactive');
    
    -- Call second stored procedure
    EXEC SP2;
    
    -- Clean up
    DROP TABLE #TempTable;
END
GO

-- Stored Procedure 2 (Middle)
CREATE PROCEDURE SP2
AS
BEGIN
    -- Can access #TempTable from SP1
    SELECT * FROM #TempTable;
    
    -- Add more data to temp table
    INSERT INTO #TempTable VALUES (3, 'Bob');
    
    -- Call third stored procedure
    EXEC SP3;
    
    -- Cannot access @TableVariable from SP1
    -- This would cause an error:
    -- SELECT * FROM @TableVariable;
END
GO

-- Stored Procedure 3 (Inner/Nested)
CREATE PROCEDURE SP3
AS
BEGIN
    -- ✅ Can still access #TempTable from SP1
    SELECT * FROM #TempTable;
    
    -- Can see all data including what was added in SP2
    SELECT COUNT(*) as TotalRecords FROM #TempTable; -- Returns 3
    
    -- ❌ Cannot access @TableVariable from SP1
    -- This would cause an error:
    -- SELECT * FROM @TableVariable;
END
GO
```

**Key Differences:**

| Feature | Temp Table (#Table) | Table Variable (@Table) |
|---------|---------------------|-------------------------|
| **Scope** | Session-wide | Batch/Procedure-level |
| **Accessible in Nested SPs** | ✅ Yes | ❌ No |
| **Statistics** | Yes (better for large data) | No |
| **Indexes** | Can create explicitly | Only at declaration |
| **Transactions** | Participates | Limited participation |
| **Performance** | Better for large datasets | Better for small datasets |

**Real-World Example with 3 Nested SPs:**

```sql
-- SP1: Order Processing Entry Point
CREATE PROCEDURE ProcessOrder @OrderId INT
AS
BEGIN
    -- Create temp table for order items (accessible in all nested SPs)
    CREATE TABLE #OrderItems
    (
        ItemId INT,
        ProductName NVARCHAR(100),
        Quantity INT,
        Price DECIMAL(18,2),
        TotalAmount DECIMAL(18,2)
    );
    
    -- Populate order items
    INSERT INTO #OrderItems
    SELECT ItemId, ProductName, Quantity, Price, Quantity * Price
    FROM OrderItems WHERE OrderId = @OrderId;
    
    -- Call validation SP
    EXEC ValidateOrderItems @OrderId;
END
GO

-- SP2: Validate Order Items
CREATE PROCEDURE ValidateOrderItems @OrderId INT
AS
BEGIN
    -- ✅ Can access #OrderItems from ProcessOrder
    DECLARE @TotalAmount DECIMAL(18,2);
    
    SELECT @TotalAmount = SUM(TotalAmount) FROM #OrderItems;
    
    -- Add validation results to temp table
    ALTER TABLE #OrderItems ADD ValidationStatus NVARCHAR(50);
    
    UPDATE #OrderItems 
    SET ValidationStatus = 'Valid' 
    WHERE Quantity > 0 AND Price > 0;
    
    -- Call calculation SP
    EXEC CalculateOrderTotals @OrderId;
END
GO

-- SP3: Calculate Totals
CREATE PROCEDURE CalculateOrderTotals @OrderId INT
AS
BEGIN
    -- ✅ Can still access #OrderItems from ProcessOrder
    -- Can see all modifications made in ValidateOrderItems
    
    DECLARE @SubTotal DECIMAL(18,2);
    DECLARE @Tax DECIMAL(18,2);
    DECLARE @GrandTotal DECIMAL(18,2);
    
    SELECT @SubTotal = SUM(TotalAmount) 
    FROM #OrderItems 
    WHERE ValidationStatus = 'Valid';
    
    SET @Tax = @SubTotal * 0.10;
    SET @GrandTotal = @SubTotal + @Tax;
    
    -- Return final results
    SELECT 
        @OrderId as OrderId,
        @SubTotal as SubTotal,
        @Tax as Tax,
        @GrandTotal as GrandTotal;
END
GO
```

**C# Code to Call Nested SPs:**

```csharp
public class OrderService
{
    private readonly string _connectionString;
    
    public async Task<OrderTotals> ProcessOrderAsync(int orderId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        using var command = new SqlCommand("ProcessOrder", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        
        command.Parameters.AddWithValue("@OrderId", orderId);
        
        using var reader = await command.ExecuteReaderAsync();
        
        if (await reader.ReadAsync())
        {
            return new OrderTotals
            {
                OrderId = reader.GetInt32(0),
                SubTotal = reader.GetDecimal(1),
                Tax = reader.GetDecimal(2),
                GrandTotal = reader.GetDecimal(3)
            };
        }
        
        return null;
    }
}

public class OrderTotals
{
    public int OrderId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Tax { get; set; }
    public decimal GrandTotal { get; set; }
}
```

---

## 15. Apache Kafka Integration in .NET Core

### 15.1 Kafka NuGet Package

**Question:** What is the NuGet package for Kafka topic?

**Answer:**

The primary NuGet package for Apache Kafka in .NET is:

```xml
<PackageReference Include="Confluent.Kafka" Version="2.3.0" />
```

**Additional Related Packages:**

```xml
<!-- Core Kafka client -->
<PackageReference Include="Confluent.Kafka" Version="2.3.0" />

<!-- For Avro serialization -->
<PackageReference Include="Confluent.SchemaRegistry.Serdes.Avro" Version="2.3.0" />

<!-- For JSON serialization -->
<PackageReference Include="Confluent.SchemaRegistry.Serdes.Json" Version="2.3.0" />

<!-- For Protobuf serialization -->
<PackageReference Include="Confluent.SchemaRegistry.Serdes.Protobuf" Version="2.3.0" />
```

**Installation:**

```powershell
dotnet add package Confluent.Kafka
```

---

### 15.2 Kafka Consumer Project Type

**Question:** Which project type is used for Kafka consumer?

**Answer:**

For Kafka consumers in .NET, you typically use:

1. **Worker Service (.NET)** - **Most Common for Background Processing**
   ```powershell
   dotnet new worker -n KafkaConsumerService
   ```

2. **Console Application** - For simple consumers or testing
   ```powershell
   dotnet new console -n KafkaConsumer
   ```

3. **ASP.NET Core Web API with Background Service** - When you need both API and consumer
   ```powershell
   dotnet new webapi -n KafkaConsumerApi
   ```

**Recommended: Worker Service Example**

```xml
<!-- KafkaConsumerService.csproj -->
<Project Sdk="Microsoft.NET.Sdk.Worker">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Confluent.Kafka" Version="2.3.0" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.0" />
  </ItemGroup>
</Project>
```

```csharp
// Program.cs
using KafkaConsumerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<KafkaConsumerWorker>();

var host = builder.Build();
host.Run();
```

```csharp
// KafkaConsumerWorker.cs
public class KafkaConsumerWorker : BackgroundService
{
    private readonly ILogger<KafkaConsumerWorker> _logger;
    private readonly IConsumer<string, string> _consumer;

    public KafkaConsumerWorker(ILogger<KafkaConsumerWorker> logger)
    {
        _logger = logger;
        
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "my-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        
        _consumer = new ConsumerBuilder<string, string>(config).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("my-topic");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);
                _logger.LogInformation($"Received: {result.Message.Value}");
                
                // Process message here
                await ProcessMessageAsync(result.Message.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consuming message");
            }
        }
        
        _consumer.Close();
    }
    
    private async Task ProcessMessageAsync(string message)
    {
        // Your business logic here
        await Task.Delay(100); // Simulate processing
    }
}
```

---

### 15.3 Kafka Use Cases

**Question:** What is the use case of Kafka?

**Answer:**

Apache Kafka is used for:

**1. Event-Driven Architecture**
```csharp
// Example: Order processing system
public class OrderCreatedEvent
{
    public int OrderId { get; set; }
    public string CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}

// Producer (Order Service)
await _producer.ProduceAsync("order-created", new Message<string, string>
{
    Key = order.OrderId.ToString(),
    Value = JsonSerializer.Serialize(new OrderCreatedEvent
    {
        OrderId = order.OrderId,
        CustomerId = order.CustomerId,
        TotalAmount = order.TotalAmount,
        CreatedAt = DateTime.UtcNow
    })
});

// Consumers:
// - Inventory Service (reduce stock)
// - Notification Service (send confirmation email)
// - Analytics Service (update reports)
// - Billing Service (process payment)
```

**2. Microservices Communication**
- Decouples services
- Async communication
- High scalability

**3. Real-Time Data Streaming**
- Log aggregation
- Metrics collection
- User activity tracking
- IoT sensor data

**4. Data Integration & ETL**
```csharp
// Source System -> Kafka -> Target System
// Example: Sync data from SQL Server to ElasticSearch
```

**5. CQRS & Event Sourcing**
```csharp
public class AccountEventStore
{
    // Write side: Publish events to Kafka
    public async Task DepositAsync(string accountId, decimal amount)
    {
        var evt = new AccountDepositedEvent(accountId, amount, DateTime.UtcNow);
        await _producer.ProduceAsync("account-events", evt);
    }
    
    // Read side: Consume events and build read models
}
```

**Common Real-World Use Cases:**

| Use Case | Description | Example |
|----------|-------------|---------|
| **Order Processing** | Track order lifecycle events | E-commerce platforms |
| **Activity Tracking** | User behavior analytics | Netflix, LinkedIn |
| **Log Aggregation** | Centralized logging | Distributed systems |
| **Notification System** | Send emails, SMS, push | Banking alerts |
| **Fraud Detection** | Real-time transaction analysis | Payment systems |
| **Stock Trading** | Market data streaming | Financial applications |
| **IoT Data** | Sensor data collection | Smart devices |
| **CDC (Change Data Capture)** | Database replication | Data synchronization |

---

### 15.4 Kafka Producer and Consumer

**Question:** Explain producer and consumer with example.

**Answer:**

**Producer:** Sends messages to Kafka topics
**Consumer:** Reads messages from Kafka topics

**Complete Example:**

**1. Producer Implementation:**

```csharp
// KafkaProducer.cs
public interface IKafkaProducer
{
    Task ProduceAsync<T>(string topic, string key, T message);
}

public class KafkaProducer : IKafkaProducer, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;

    public KafkaProducer(IConfiguration configuration, ILogger<KafkaProducer> logger)
    {
        _logger = logger;
        
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            Acks = Acks.All, // Wait for all replicas
            EnableIdempotence = true, // Exactly-once semantics
            MaxInFlight = 5,
            MessageSendMaxRetries = 3
        };
        
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task ProduceAsync<T>(string topic, string key, T message)
    {
        try
        {
            var value = JsonSerializer.Serialize(message);
            
            var result = await _producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = key,
                Value = value,
                Timestamp = new Timestamp(DateTime.UtcNow)
            });
            
            _logger.LogInformation(
                $"Message delivered to {result.TopicPartitionOffset}");
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError(ex, $"Failed to deliver message: {ex.Error.Reason}");
            throw;
        }
    }

    public void Dispose()
    {
        _producer?.Flush(TimeSpan.FromSeconds(10));
        _producer?.Dispose();
    }
}
```

**2. Consumer Implementation:**

```csharp
// KafkaConsumerWorker.cs
public class KafkaConsumerWorker : BackgroundService
{
    private readonly ILogger<KafkaConsumerWorker> _logger;
    private readonly IConsumer<string, string> _consumer;
    private readonly IServiceProvider _serviceProvider;

    public KafkaConsumerWorker(
        IConfiguration configuration,
        ILogger<KafkaConsumerWorker> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        
        var config = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = configuration["Kafka:ConsumerGroup"],
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false, // Manual commit for reliability
            EnableAutoOffsetStore = false
        };
        
        _consumer = new ConsumerBuilder<string, string>(config)
            .SetErrorHandler((_, error) => 
                _logger.LogError($"Error: {error.Reason}"))
            .Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(new[] { "order-events", "payment-events" });
        
        _logger.LogInformation("Kafka consumer started");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                
                _logger.LogInformation(
                    $"Received message from {consumeResult.Topic}: " +
                    $"Key={consumeResult.Message.Key}, " +
                    $"Value={consumeResult.Message.Value}");
                
                // Process message
                await ProcessMessageAsync(consumeResult.Message, stoppingToken);
                
                // Commit offset after successful processing
                _consumer.Commit(consumeResult);
                _consumer.StoreOffset(consumeResult);
            }
            catch (ConsumeException ex)
            {
                _logger.LogError(ex, "Consume error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Processing error");
                // Optionally: Dead letter queue handling
            }
        }
        
        _consumer.Close();
    }
    
    private async Task ProcessMessageAsync(Message<string, string> message, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        
        // Deserialize and process
        var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message.Value);
        
        // Business logic
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
        await orderService.ProcessOrderAsync(orderEvent, cancellationToken);
    }

    public override void Dispose()
    {
        _consumer?.Close();
        _consumer?.Dispose();
        base.Dispose();
    }
}
```

**3. Real-World Example - E-Commerce Order Flow:**

```csharp
// Domain Events
public record OrderCreatedEvent(int OrderId, string CustomerId, decimal Amount);
public record PaymentProcessedEvent(int OrderId, string TransactionId, bool Success);
public record InventoryReservedEvent(int OrderId, List<int> ProductIds);

// Producer Service (Order API)
public class OrderService
{
    private readonly IKafkaProducer _producer;
    
    public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
    {
        // 1. Save order to database
        var order = await _repository.CreateOrderAsync(request);
        
        // 2. Publish event to Kafka
        await _producer.ProduceAsync(
            topic: "order-events",
            key: order.OrderId.ToString(),
            message: new OrderCreatedEvent(
                order.OrderId,
                order.CustomerId,
                order.TotalAmount));
        
        return order;
    }
}

// Consumer Service 1: Inventory Service
public class InventoryConsumerWorker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("order-events");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var result = _consumer.Consume(stoppingToken);
            var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(result.Message.Value);
            
            // Reserve inventory
            var reserved = await _inventoryService.ReserveItemsAsync(orderEvent.OrderId);
            
            // Publish inventory event
            await _producer.ProduceAsync("inventory-events", 
                orderEvent.OrderId.ToString(),
                new InventoryReservedEvent(orderEvent.OrderId, reserved.ProductIds));
            
            _consumer.Commit(result);
        }
    }
}

// Consumer Service 2: Notification Service
public class NotificationConsumerWorker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("order-events");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var result = _consumer.Consume(stoppingToken);
            var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(result.Message.Value);
            
            // Send confirmation email
            await _emailService.SendOrderConfirmationAsync(
                orderEvent.CustomerId,
                orderEvent.OrderId);
            
            _consumer.Commit(result);
        }
    }
}

// Consumer Service 3: Analytics Service
public class AnalyticsConsumerWorker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(new[] { "order-events", "payment-events", "inventory-events" });
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var result = _consumer.Consume(stoppingToken);
            
            // Update analytics dashboard
            await _analyticsService.UpdateDashboardAsync(
                result.Topic,
                result.Message.Value);
            
            _consumer.Commit(result);
        }
    }
}
```

**4. Configuration:**

```json
// appsettings.json
{
  "Kafka": {
    "BootstrapServers": "localhost:9092",
    "ConsumerGroup": "order-processing-group",
    "Topics": {
      "OrderEvents": "order-events",
      "PaymentEvents": "payment-events",
      "InventoryEvents": "inventory-events"
    }
  }
}
```

**5. Dependency Injection Setup:**

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Register Kafka Producer
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();

// Register Kafka Consumers as Hosted Services
builder.Services.AddHostedService<InventoryConsumerWorker>();
builder.Services.AddHostedService<NotificationConsumerWorker>();
builder.Services.AddHostedService<AnalyticsConsumerWorker>();

var app = builder.Build();
app.Run();
```

**Key Concepts:**

| Concept | Producer | Consumer |
|---------|----------|----------|
| **Purpose** | Send messages | Receive messages |
| **Idempotence** | Prevents duplicates | Handle duplicates |
| **Acknowledgment** | Acks.All for reliability | Manual commit recommended |
| **Partitioning** | Key-based routing | Consumer group balancing |
| **Error Handling** | Retry logic | Dead letter queue |
| **Scalability** | Multiple producers | Consumer groups |

---

## 16. MediatR and CQRS Patterns

### 16.1 MediatR Pattern

**Question:** What is MediatR pattern and how is it different from handler pattern? Explain with example.

**Answer:**

**MediatR** is an in-process mediator implementation that supports:
- Request/response messages
- Commands and queries
- Notifications (pub/sub)
- Decoupling of business logic

**Key Differences from Traditional Handler Pattern:**

| Aspect | Traditional Handler | MediatR Pattern |
|--------|-------------------|-----------------|
| **Coupling** | Direct dependency | Loose coupling via mediator |
| **Registration** | Manual wiring | Convention-based |
| **Pipeline** | No built-in pipeline | Supports behaviors/middleware |
| **Pub/Sub** | Not supported | Built-in notifications |
| **Testing** | Harder to mock | Easy to test |

**Installation:**

```powershell
dotnet add package MediatR
dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
```

**Traditional Handler Pattern Example:**

```csharp
// ❌ Traditional approach - tight coupling
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _repository;
    private readonly IEmailService _emailService;
    private readonly IInventoryService _inventoryService;
    private readonly ILogger<OrderController> _logger;
    
    // Constructor becomes bloated
    public OrderController(
        IOrderRepository repository,
        IEmailService emailService,
        IInventoryService inventoryService,
        ILogger<OrderController> logger)
    {
        _repository = repository;
        _emailService = emailService;
        _inventoryService = inventoryService;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        // Business logic mixed with controller
        var order = new Order
        {
            CustomerId = request.CustomerId,
            TotalAmount = request.TotalAmount
        };
        
        await _repository.AddAsync(order);
        await _inventoryService.ReserveItemsAsync(order.Id);
        await _emailService.SendConfirmationAsync(order.CustomerId);
        _logger.LogInformation($"Order {order.Id} created");
        
        return Ok(order);
    }
}
```

**MediatR Pattern Example:**

```csharp
// ✅ MediatR approach - loose coupling

// 1. Define Request
public record CreateOrderCommand(string CustomerId, decimal TotalAmount) 
    : IRequest<OrderResponse>;

public record OrderResponse(int OrderId, string Status);

// 2. Define Handler
public class CreateOrderCommandHandler 
    : IRequestHandler<CreateOrderCommand, OrderResponse>
{
    private readonly IOrderRepository _repository;
    private readonly IEmailService _emailService;
    private readonly IInventoryService _inventoryService;
    private readonly ILogger<CreateOrderCommandHandler> _logger;
    
    public CreateOrderCommandHandler(
        IOrderRepository repository,
        IEmailService emailService,
        IInventoryService inventoryService,
        ILogger<CreateOrderCommandHandler> logger)
    {
        _repository = repository;
        _emailService = emailService;
        _inventoryService = inventoryService;
        _logger = logger;
    }
    
    public async Task<OrderResponse> Handle(
        CreateOrderCommand request, 
        CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CustomerId = request.CustomerId,
            TotalAmount = request.TotalAmount
        };
        
        await _repository.AddAsync(order);
        await _inventoryService.ReserveItemsAsync(order.Id);
        await _emailService.SendConfirmationAsync(order.CustomerId);
        
        _logger.LogInformation($"Order {order.Id} created");
        
        return new OrderResponse(order.Id, "Created");
    }
}

// 3. Clean Controller
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator; // Only one dependency!
    
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var command = new CreateOrderCommand(request.CustomerId, request.TotalAmount);
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}
```

**MediatR Pipeline Behaviors (Middleware):**

```csharp
// Logging Behavior
public class LoggingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling {typeof(TRequest).Name}");
        var response = await next();
        _logger.LogInformation($"Handled {typeof(TRequest).Name}");
        return response;
    }
}

// Validation Behavior
public class ValidationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();
        
        if (failures.Any())
            throw new ValidationException(failures);
        
        return await next();
    }
}

// Transaction Behavior
public class TransactionBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly AppDbContext _context;
    
    public TransactionBehavior(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var response = await next();
            await transaction.CommitAsync(cancellationToken);
            return response;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
```

**MediatR Notifications (Pub/Sub):**

```csharp
// 1. Define Notification
public record OrderCreatedNotification(int OrderId, string CustomerId) 
    : INotification;

// 2. Multiple Handlers
public class SendEmailNotificationHandler 
    : INotificationHandler<OrderCreatedNotification>
{
    private readonly IEmailService _emailService;
    
    public async Task Handle(
        OrderCreatedNotification notification, 
        CancellationToken cancellationToken)
    {
        await _emailService.SendOrderConfirmationAsync(
            notification.CustomerId,
            notification.OrderId);
    }
}

public class UpdateAnalyticsNotificationHandler 
    : INotificationHandler<OrderCreatedNotification>
{
    private readonly IAnalyticsService _analyticsService;
    
    public async Task Handle(
        OrderCreatedNotification notification, 
        CancellationToken cancellationToken)
    {
        await _analyticsService.TrackOrderCreatedAsync(notification.OrderId);
    }
}

public class LogNotificationHandler 
    : INotificationHandler<OrderCreatedNotification>
{
    private readonly ILogger<LogNotificationHandler> _logger;
    
    public async Task Handle(
        OrderCreatedNotification notification, 
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Order {notification.OrderId} created");
        await Task.CompletedTask;
    }
}

// 3. Publish Notification
public class CreateOrderCommandHandler 
    : IRequestHandler<CreateOrderCommand, OrderResponse>
{
    private readonly IMediator _mediator;
    
    public async Task<OrderResponse> Handle(
        CreateOrderCommand request, 
        CancellationToken cancellationToken)
    {
        // ... create order logic ...
        
        // Publish notification - all handlers will execute
        await _mediator.Publish(
            new OrderCreatedNotification(order.Id, order.CustomerId),
            cancellationToken);
        
        return new OrderResponse(order.Id, "Created");
    }
}
```

**Dependency Injection Setup:**

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Register MediatR
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Register Pipeline Behaviors (order matters!)
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

var app = builder.Build();
app.Run();
```

**Complete Real-World Example:**

```csharp
// Commands
public record CreateOrderCommand(string CustomerId, List<OrderItemDto> Items) 
    : IRequest<Result<OrderDto>>;

public record UpdateOrderCommand(int OrderId, string Status) 
    : IRequest<Result<OrderDto>>;

public record CancelOrderCommand(int OrderId, string Reason) 
    : IRequest<Result>;

// Queries
public record GetOrderByIdQuery(int OrderId) 
    : IRequest<Result<OrderDto>>;

public record GetOrdersByCustomerQuery(string CustomerId, int Page, int PageSize) 
    : IRequest<Result<PagedList<OrderDto>>>;

// Handler Example
public class GetOrderByIdQueryHandler 
    : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    
    public async Task<Result<OrderDto>> Handle(
        GetOrderByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.OrderId);
        
        if (order == null)
            return Result<OrderDto>.Failure("Order not found");
        
        var dto = _mapper.Map<OrderDto>(order);
        return Result<OrderDto>.Success(dto);
    }
}

// Controller
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id));
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateOrderCommand command)
    {
        if (id != command.OrderId)
            return BadRequest();
        
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(int id, [FromBody] string reason)
    {
        var result = await _mediator.Send(new CancelOrderCommand(id, reason));
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
}
```

**Benefits of MediatR:**

1. ✅ **Single Responsibility** - Each handler has one job
2. ✅ **Testability** - Easy to unit test handlers
3. ✅ **Pipeline Behaviors** - Cross-cutting concerns
4. ✅ **Loose Coupling** - Controllers don't know about handlers
5. ✅ **Clean Code** - Organized and maintainable
6. ✅ **Pub/Sub** - Built-in notification support

---

### 16.2 CQRS Pattern

**Question:** What is CQRS pattern? Explain with example.

**Answer:**

**CQRS (Command Query Responsibility Segregation)** is a pattern that separates read and write operations into different models.

**Key Concepts:**
- **Commands**: Modify state (Create, Update, Delete)
- **Queries**: Read state (Get, List, Search)
- Separate data models for reads and writes
- Can use different databases for reads/writes

**Why CQRS?**

| Problem | CQRS Solution |
|---------|---------------|
| Complex queries slow down writes | Separate read/write databases |
| Read and write models conflict | Different models optimized for each |
| Scaling reads vs writes | Scale independently |
| Performance bottlenecks | Optimize each side separately |

**CQRS Levels:**

**Level 1: Simple CQRS (Same Database)**

```csharp
// Commands (Write Model)
public record CreateProductCommand(string Name, decimal Price, int Stock) 
    : IRequest<Result<int>>;

public class CreateProductCommandHandler 
    : IRequestHandler<CreateProductCommand, Result<int>>
{
    private readonly AppDbContext _context;
    
    public async Task<Result<int>> Handle(
        CreateProductCommand request, 
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result<int>.Success(product.Id);
    }
}

// Queries (Read Model)
public record GetProductByIdQuery(int Id) 
    : IRequest<Result<ProductDto>>;

public class GetProductByIdQueryHandler 
    : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IProductReadRepository _repository;
    
    public async Task<Result<ProductDto>> Handle(
        GetProductByIdQuery request, 
        CancellationToken cancellationToken)
    {
        // Optimized read-only query
        var product = await _repository.GetByIdAsync(request.Id);
        
        if (product == null)
            return Result<ProductDto>.Failure("Product not found");
        
        return Result<ProductDto>.Success(product);
    }
}

// Different repositories
public interface IProductWriteRepository
{
    Task<int> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}

public interface IProductReadRepository
{
    Task<ProductDto> GetByIdAsync(int id);
    Task<List<ProductDto>> GetAllAsync();
    Task<List<ProductDto>> SearchAsync(string searchTerm);
}
```

**Level 2: CQRS with Separate Databases**

```csharp
// Write Model (SQL Server - normalized)
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

// Read Model (MongoDB - denormalized for fast reads)
public class ProductReadModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public List<string> Tags { get; set; }
    public int TotalOrders { get; set; }
    public double AverageRating { get; set; }
    public DateTime CreatedAt { get; set; }
}

// Command Handler (Write to SQL Server)
public class CreateProductCommandHandler 
    : IRequestHandler<CreateProductCommand, Result<int>>
{
    private readonly AppDbContext _writeDb; // SQL Server
    private readonly IMediator _mediator;
    
    public async Task<Result<int>> Handle(
        CreateProductCommand request, 
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow
        };
        
        _writeDb.Products.Add(product);
        await _writeDb.SaveChangesAsync(cancellationToken);
        
        // Publish event to sync read model
        await _mediator.Publish(
            new ProductCreatedEvent(product.Id, product.Name, product.Price),
            cancellationToken);
        
        return Result<int>.Success(product.Id);
    }
}

// Event Handler (Update MongoDB)
public class ProductCreatedEventHandler 
    : INotificationHandler<ProductCreatedEvent>
{
    private readonly IMongoCollection<ProductReadModel> _readDb; // MongoDB
    
    public async Task Handle(
        ProductCreatedEvent notification, 
        CancellationToken cancellationToken)
    {
        var readModel = new ProductReadModel
        {
            Id = notification.ProductId,
            Name = notification.Name,
            Price = notification.Price,
            CreatedAt = DateTime.UtcNow,
            Tags = new List<string>(),
            TotalOrders = 0,
            AverageRating = 0
        };
        
        await _readDb.InsertOneAsync(readModel, cancellationToken: cancellationToken);
    }
}

// Query Handler (Read from MongoDB)
public class SearchProductsQueryHandler 
    : IRequestHandler<SearchProductsQuery, Result<List<ProductDto>>>
{
    private readonly IMongoCollection<ProductReadModel> _readDb;
    
    public async Task<Result<List<ProductDto>>> Handle(
        SearchProductsQuery request, 
        CancellationToken cancellationToken)
    {
        var filter = Builders<ProductReadModel>.Filter.Or(
            Builders<ProductReadModel>.Filter.Regex(p => p.Name, request.SearchTerm),
            Builders<ProductReadModel>.Filter.AnyIn(p => p.Tags, new[] { request.SearchTerm })
        );
        
        var products = await _readDb
            .Find(filter)
            .Skip((request.Page - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync(cancellationToken);
        
        var dtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Stock = p.Stock,
            AverageRating = p.AverageRating
        }).ToList();
        
        return Result<List<ProductDto>>.Success(dtos);
    }
}
```

**Level 3: Event Sourcing + CQRS**

```csharp
// Events (Event Store)
public abstract record ProductEvent(int ProductId, DateTime OccurredAt);

public record ProductCreatedEvent(
    int ProductId, 
    string Name, 
    decimal Price, 
    DateTime OccurredAt) 
    : ProductEvent(ProductId, OccurredAt);

public record ProductPriceChangedEvent(
    int ProductId, 
    decimal OldPrice, 
    decimal NewPrice, 
    DateTime OccurredAt) 
    : ProductEvent(ProductId, OccurredAt);

public record ProductStockUpdatedEvent(
    int ProductId, 
    int OldStock, 
    int NewStock, 
    DateTime OccurredAt) 
    : ProductEvent(ProductId, OccurredAt);

// Command Handler (Append events)
public class ChangeProductPriceCommandHandler 
    : IRequestHandler<ChangeProductPriceCommand, Result>
{
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;
    
    public async Task<Result> Handle(
        ChangeProductPriceCommand request, 
        CancellationToken cancellationToken)
    {
        // Load aggregate from events
        var events = await _eventStore.GetEventsAsync(request.ProductId);
        var product = Product.LoadFromHistory(events);
        
        // Validate
        if (request.NewPrice <= 0)
            return Result.Failure("Price must be positive");
        
        // Create and append event
        var @event = new ProductPriceChangedEvent(
            request.ProductId,
            product.Price,
            request.NewPrice,
            DateTime.UtcNow);
        
        await _eventStore.AppendEventAsync(@event);
        
        // Publish for read model update
        await _mediator.Publish(@event, cancellationToken);
        
        return Result.Success();
    }
}

// Projection (Build read model from events)
public class ProductProjection : INotificationHandler<ProductEvent>
{
    private readonly IMongoCollection<ProductReadModel> _readDb;
    
    public async Task Handle(ProductEvent notification, CancellationToken cancellationToken)
    {
        switch (notification)
        {
            case ProductCreatedEvent created:
                await HandleCreatedAsync(created, cancellationToken);
                break;
            
            case ProductPriceChangedEvent priceChanged:
                await HandlePriceChangedAsync(priceChanged, cancellationToken);
                break;
            
            case ProductStockUpdatedEvent stockUpdated:
                await HandleStockUpdatedAsync(stockUpdated, cancellationToken);
                break;
        }
    }
    
    private async Task HandleCreatedAsync(
        ProductCreatedEvent @event, 
        CancellationToken cancellationToken)
    {
        var readModel = new ProductReadModel
        {
            Id = @event.ProductId,
            Name = @event.Name,
            Price = @event.Price,
            CreatedAt = @event.OccurredAt
        };
        
        await _readDb.InsertOneAsync(readModel, cancellationToken: cancellationToken);
    }
    
    private async Task HandlePriceChangedAsync(
        ProductPriceChangedEvent @event, 
        CancellationToken cancellationToken)
    {
        var filter = Builders<ProductReadModel>.Filter.Eq(p => p.Id, @event.ProductId);
        var update = Builders<ProductReadModel>.Update.Set(p => p.Price, @event.NewPrice);
        
        await _readDb.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }
    
    private async Task HandleStockUpdatedAsync(
        ProductStockUpdatedEvent @event, 
        CancellationToken cancellationToken)
    {
        var filter = Builders<ProductReadModel>.Filter.Eq(p => p.Id, @event.ProductId);
        var update = Builders<ProductReadModel>.Update.Set(p => p.Stock, @event.NewStock);
        
        await _readDb.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }
}
```

**Complete E-Commerce Example:**

```csharp
// Project Structure:
// - Application
//   - Commands
//     - CreateOrderCommand.cs
//     - UpdateOrderStatusCommand.cs
//   - Queries
//     - GetOrderByIdQuery.cs
//     - GetOrdersByCustomerQuery.cs
//   - Handlers
//     - CreateOrderCommandHandler.cs
//     - GetOrderByIdQueryHandler.cs
// - Domain
//   - Entities
//     - Order.cs
//   - Events
//     - OrderCreatedEvent.cs
// - Infrastructure
//   - Persistence
//     - WriteDbContext.cs (SQL Server)
//     - ReadDbContext.cs (MongoDB)

// Commands
public record CreateOrderCommand(
    string CustomerId, 
    List<OrderItemDto> Items) 
    : IRequest<Result<int>>;

public record UpdateOrderStatusCommand(
    int OrderId, 
    OrderStatus NewStatus) 
    : IRequest<Result>;

// Queries
public record GetOrderByIdQuery(int OrderId) 
    : IRequest<Result<OrderDetailDto>>;

public record GetOrdersByCustomerQuery(
    string CustomerId, 
    int Page, 
    int PageSize) 
    : IRequest<Result<PagedList<OrderSummaryDto>>>;

// Write Model (Domain Entity)
public class Order
{
    public int Id { get; private set; }
    public string CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public List<OrderItem> Items { get; private set; }
    
    public static Order Create(string customerId, List<OrderItem> items)
    {
        var order = new Order
        {
            CustomerId = customerId,
            Status = OrderStatus.Pending,
            Items = items,
            TotalAmount = items.Sum(i => i.Price * i.Quantity),
            CreatedAt = DateTime.UtcNow
        };
        
        return order;
    }
    
    public void UpdateStatus(OrderStatus newStatus)
    {
        if (Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Cannot update cancelled order");
        
        Status = newStatus;
    }
}

// Read Model (Optimized for queries)
public class OrderReadModel
{
    public int OrderId { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public int ItemCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemReadModel> Items { get; set; }
}

// Command Handler
public class CreateOrderCommandHandler 
    : IRequestHandler<CreateOrderCommand, Result<int>>
{
    private readonly WriteDbContext _writeDb;
    private readonly IMediator _mediator;
    
    public async Task<Result<int>> Handle(
        CreateOrderCommand request, 
        CancellationToken cancellationToken)
    {
        var items = request.Items.Select(i => new OrderItem
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            Price = i.Price
        }).ToList();
        
        var order = Order.Create(request.CustomerId, items);
        
        _writeDb.Orders.Add(order);
        await _writeDb.SaveChangesAsync(cancellationToken);
        
        // Publish event to update read model
        await _mediator.Publish(
            new OrderCreatedEvent(
                order.Id,
                order.CustomerId,
                order.TotalAmount,
                order.Items),
            cancellationToken);
        
        return Result<int>.Success(order.Id);
    }
}

// Query Handler
public class GetOrderByIdQueryHandler 
    : IRequestHandler<GetOrderByIdQuery, Result<OrderDetailDto>>
{
    private readonly IMongoCollection<OrderReadModel> _readDb;
    
    public async Task<Result<OrderDetailDto>> Handle(
        GetOrderByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var order = await _readDb
            .Find(o => o.OrderId == request.OrderId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (order == null)
            return Result<OrderDetailDto>.Failure("Order not found");
        
        var dto = new OrderDetailDto
        {
            OrderId = order.OrderId,
            CustomerName = order.CustomerName,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            Items = order.Items
        };
        
        return Result<OrderDetailDto>.Success(dto);
    }
}

// Dependency Injection
builder.Services.AddDbContext<WriteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WriteDb")));

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("ReadDb")));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
```

**CQRS Benefits:**

1. ✅ **Separation of Concerns** - Read/write logic isolated
2. ✅ **Scalability** - Scale reads and writes independently
3. ✅ **Performance** - Optimize each side separately
4. ✅ **Flexibility** - Use different databases for different needs
5. ✅ **Complex Queries** - Denormalized read models
6. ✅ **Security** - Different permissions for reads/writes

**When to Use CQRS:**

| Use Case | Why CQRS? |
|----------|-----------|
| High read/write ratio | Scale reads independently |
| Complex domain logic | Separate write complexity from reads |
| Different read/write requirements | Optimize separately |
| Event-driven systems | Natural fit with event sourcing |
| Microservices | Service-level separation |

---

## 17. Dependency Injection in .NET Core

### 17.1 Types of Dependency Injection with Real-Time Examples

**Question:** What are the types of dependency injection classes in .NET Core with real-time examples?

**Answer:**

.NET Core provides three built-in service lifetimes:

**1. Transient**
**2. Scoped**
**3. Singleton**

**1. Transient Lifetime (`AddTransient`)**

**Characteristics:**
- Created every time they are requested
- New instance for each injection
- Short-lived
- No state sharing

**When to Use:**
- Lightweight, stateless services
- Services that don't hold state
- Services used briefly

**Real-Time Examples:**

```csharp
// Registration
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IValidator<CreateOrderRequest>, OrderValidator>();

// Example 1: Email Service
public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    
    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
        _logger.LogInformation($"EmailService instance created: {GetHashCode()}");
    }
    
    public async Task SendAsync(string to, string subject, string body)
    {
        // Send email logic
        await Task.Delay(100);
        _logger.LogInformation($"Email sent to {to}");
    }
}

// Example 2: Password Hasher
public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        // In real app, use BCrypt or similar
        return Convert.ToBase64String(
            System.Security.Cryptography.SHA256.HashData(
                System.Text.Encoding.UTF8.GetBytes(password)));
    }
    
    public bool Verify(string password, string hash)
    {
        return Hash(password) == hash;
    }
}

// Usage in Controller
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IPasswordHasher _passwordHasher;
    
    // New instances injected every time
    public AccountController(
        IEmailService emailService,
        IPasswordHasher passwordHasher)
    {
        _emailService = emailService;
        _passwordHasher = passwordHasher;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        // Hash password
        var hashedPassword = _passwordHasher.Hash(request.Password);
        
        // Save user...
        
        // Send welcome email
        await _emailService.SendAsync(
            request.Email,
            "Welcome!",
            "Thank you for registering");
        
        return Ok();
    }
}
```

**2. Scoped Lifetime (`AddScoped`)**

**Characteristics:**
- Created once per HTTP request (or scope)
- Same instance within the request
- Disposed at end of request
- Most common for database contexts

**When to Use:**
- Database contexts (DbContext)
- Unit of Work pattern
- Request-specific caching
- Services that maintain state during a request

**Real-Time Examples:**

```csharp
// Registration
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)); // Scoped by default

// Example 1: Order Service with DbContext
public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly ILogger<OrderService> _logger;
    
    public OrderService(AppDbContext context, ILogger<OrderService> logger)
    {
        _context = context;
        _logger = logger;
        _logger.LogInformation($"OrderService instance created: {GetHashCode()}");
    }
    
    public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
    {
        var order = new Order
        {
            CustomerId = request.CustomerId,
            TotalAmount = request.TotalAmount,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        
        return order;
    }
    
    public async Task<Order> GetOrderAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}

// Example 2: Unit of Work Pattern
public interface IUnitOfWork : IDisposable
{
    IOrderRepository Orders { get; }
    IProductRepository Products { get; }
    Task<int> SaveChangesAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Orders = new OrderRepository(context);
        Products = new ProductRepository(context);
    }
    
    public IOrderRepository Orders { get; }
    public IProductRepository Products { get; }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context?.Dispose();
    }
}

// Usage
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IUnitOfWork _unitOfWork;
    
    // Same instances throughout the request
    public OrdersController(
        IOrderService orderService,
        IUnitOfWork unitOfWork)
    {
        _orderService = orderService;
        _unitOfWork = unitOfWork;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        // Create order
        var order = await _orderService.CreateOrderAsync(request);
        
        // Update inventory (uses same DbContext)
        var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
        product.Stock -= request.Quantity;
        
        // Save all changes in one transaction
        await _unitOfWork.SaveChangesAsync();
        
        return Ok(order);
    }
}

// Example 3: Request-Specific Cache
public interface IRequestCache
{
    void Set<T>(string key, T value);
    T Get<T>(string key);
    bool TryGet<T>(string key, out T value);
}

public class RequestCache : IRequestCache
{
    private readonly Dictionary<string, object> _cache = new();
    
    public void Set<T>(string key, T value)
    {
        _cache[key] = value;
    }
    
    public T Get<T>(string key)
    {
        return _cache.TryGetValue(key, out var value) ? (T)value : default;
    }
    
    public bool TryGet<T>(string key, out T value)
    {
        if (_cache.TryGetValue(key, out var obj))
        {
            value = (T)obj;
            return true;
        }
        
        value = default;
        return false;
    }
}

// Registration
builder.Services.AddScoped<IRequestCache, RequestCache>();

// Usage
public class ProductService
{
    private readonly IRequestCache _cache;
    private readonly IProductRepository _repository;
    
    public async Task<Product> GetProductAsync(int id)
    {
        // Check request cache first
        if (_cache.TryGet($"product-{id}", out Product cachedProduct))
            return cachedProduct;
        
        // Fetch from database
        var product = await _repository.GetByIdAsync(id);
        
        // Cache for this request
        _cache.Set($"product-{id}", product);
        
        return product;
    }
}
```

**3. Singleton Lifetime (`AddSingleton`)**

**Characteristics:**
- Created once for the application lifetime
- Same instance for all requests
- Lives until application shutdown
- Must be thread-safe

**When to Use:**
- Configuration services
- Caching services
- Logging services
- Stateless services
- Connection pools

**Real-Time Examples:**

```csharp
// Registration
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<ICacheService, RedisCacheService>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect("localhost:6379"));

// Example 1: Cache Service
public interface ICacheService
{
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan expiration);
    Task RemoveAsync(string key);
}

public class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _database;
    private readonly ILogger<RedisCacheService> _logger;
    
    // Only created once
    public RedisCacheService(
        IConnectionMultiplexer redis,
        ILogger<RedisCacheService> logger)
    {
        _redis = redis;
        _database = redis.GetDatabase();
        _logger = logger;
        _logger.LogInformation($"RedisCacheService instance created: {GetHashCode()}");
    }
    
    public async Task<T> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);
        
        if (value.IsNullOrEmpty)
            return default;
        
        return JsonSerializer.Deserialize<T>(value);
    }
    
    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        var serialized = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, serialized, expiration);
    }
    
    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}

// Example 2: Configuration Service
public interface IAppSettings
{
    string ApiKey { get; }
    int MaxRetries { get; }
    TimeSpan Timeout { get; }
}

public class AppSettings : IAppSettings
{
    public AppSettings(IConfiguration configuration)
    {
        ApiKey = configuration["ApiSettings:ApiKey"];
        MaxRetries = int.Parse(configuration["ApiSettings:MaxRetries"]);
        Timeout = TimeSpan.FromSeconds(
            int.Parse(configuration["ApiSettings:TimeoutSeconds"]));
    }
    
    public string ApiKey { get; }
    public int MaxRetries { get; }
    public TimeSpan Timeout { get; }
}

// Registration
builder.Services.AddSingleton<IAppSettings, AppSettings>();

// Example 3: Connection Pool Manager
public interface IConnectionPool
{
    Task<DbConnection> GetConnectionAsync();
    void ReturnConnection(DbConnection connection);
}

public class ConnectionPool : IConnectionPool, IDisposable
{
    private readonly ConcurrentBag<DbConnection> _connections;
    private readonly string _connectionString;
    private readonly SemaphoreSlim _semaphore;
    
    public ConnectionPool(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        _connections = new ConcurrentBag<DbConnection>();
        _semaphore = new SemaphoreSlim(10, 10); // Max 10 connections
    }
    
    public async Task<DbConnection> GetConnectionAsync()
    {
        await _semaphore.WaitAsync();
        
        if (_connections.TryTake(out var connection))
        {
            if (connection.State == ConnectionState.Open)
                return connection;
            
            connection.Dispose();
        }
        
        connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
    
    public void ReturnConnection(DbConnection connection)
    {
        _connections.Add(connection);
        _semaphore.Release();
    }
    
    public void Dispose()
    {
        while (_connections.TryTake(out var connection))
        {
            connection.Dispose();
        }
        
        _semaphore?.Dispose();
    }
}
```

**Comparison Table:**

| Lifetime | Created | Shared | Disposed | Use Case |
|----------|---------|--------|----------|----------|
| **Transient** | Every time | ❌ No | After use | Lightweight, stateless |
| **Scoped** | Per request | Within request | End of request | DbContext, Unit of Work |
| **Singleton** | Once | ✅ Application-wide | App shutdown | Cache, Configuration |

**Real-World Complete Example:**

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Singleton - Application-wide services
builder.Services.AddSingleton<ICacheService, RedisCacheService>();
builder.Services.AddSingleton<IAppSettings, AppSettings>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect("localhost:6379"));

// Scoped - Per-request services
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Transient - Lightweight, stateless services
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IValidator<CreateOrderRequest>, OrderValidator>();

// MediatR (Transient by default)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();
app.Run();

// Controller demonstrating all three
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService; // Scoped
    private readonly ICacheService _cacheService; // Singleton
    private readonly IEmailService _emailService; // Transient
    private readonly ILogger<OrdersController> _logger;
    
    public OrdersController(
        IOrderService orderService,
        ICacheService cacheService,
        IEmailService emailService,
        ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _cacheService = cacheService;
        _emailService = emailService;
        _logger = logger;
        
        _logger.LogInformation(
            $"OrderService: {orderService.GetHashCode()}, " +
            $"CacheService: {cacheService.GetHashCode()}, " +
            $"EmailService: {emailService.GetHashCode()}");
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        // Check singleton cache
        var cached = await _cacheService.GetAsync<Order>($"order-{id}");
        if (cached != null)
            return Ok(cached);
        
        // Fetch using scoped service
        var order = await _orderService.GetOrderAsync(id);
        
        // Cache for future requests
        await _cacheService.SetAsync($"order-{id}", order, TimeSpan.FromMinutes(5));
        
        return Ok(order);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        // Create using scoped service
        var order = await _orderService.CreateOrderAsync(request);
        
        // Send email using transient service
        await _emailService.SendAsync(
            request.CustomerEmail,
            "Order Confirmation",
            $"Your order #{order.Id} has been created");
        
        // Invalidate cache
        await _cacheService.RemoveAsync($"orders-customer-{request.CustomerId}");
        
        return Ok(order);
    }
}
```

**Key Takeaways:**

1. **Transient** = New instance every time (Email, Validators, Hashers)
2. **Scoped** = One instance per request (DbContext, Unit of Work, Request Cache)
3. **Singleton** = One instance for entire app (Cache, Configuration, Connection Pools)
4. **Thread Safety** = Singleton must be thread-safe; Scoped/Transient don't need to be
5. **Memory** = Singleton lives longest; Transient shortest

---

This completes the additions to your interview questions document! The questions cover:
1. ✅ Nested stored procedures with temp tables/variables
2. ✅ Kafka NuGet package
3. ✅ Kafka consumer project types
4. ✅ MediatR pattern vs handler pattern
5. ✅ CQRS pattern with examples
6. ✅ Kafka producer and consumer implementation
7. ✅ Types of dependency injection with real-time examples
8. ✅ Kafka use cases

All questions include detailed explanations, code examples, and real-world scenarios.

