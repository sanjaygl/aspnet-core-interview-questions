# C# Extension Methods - Complete Guide

## Table of Contents
1. [Introduction](#introduction)
2. [What Are Extension Methods?](#what-are-extension-methods)
3. [How Extension Methods Work](#how-extension-methods-work)
4. [Syntax and Rules](#syntax-and-rules)
5. [Real-World Examples](#real-world-examples)
6. [Best Practices](#best-practices)
7. [Advanced Scenarios](#advanced-scenarios)
8. [Common Use Cases](#common-use-cases)
9. [Limitations and Gotchas](#limitations-and-gotchas)
10. [Performance Considerations](#performance-considerations)
11. [Interview Questions](#interview-questions)

---

## Introduction

Extension methods are one of the most powerful features introduced in C# 3.0 (.NET Framework 3.5) that enable developers to add new methods to existing types without modifying the original type, creating a new derived type, or recompiling the original code. They provide a clean and intuitive way to extend functionality while maintaining code readability and following the Open/Closed Principle from SOLID design principles.

**Key Benefits:**
- Extend types you don't own (sealed classes, third-party libraries)
- Add methods to interfaces without breaking existing implementations
- Improve code readability through fluent APIs
- Enable LINQ functionality
- Avoid unnecessary inheritance or wrapper classes

---

## What Are Extension Methods?

Extension methods are static methods that can be called as if they were instance methods on the extended type. They provide syntactic sugar that makes code more readable and intuitive.

### Basic Concept

```csharp
// Traditional static method call
string result = StringHelper.CapitalizeFirstLetter(myString);

// Extension method call (looks like an instance method)
string result = myString.CapitalizeFirstLetter();
```

### Historical Context

Extension methods were introduced to support LINQ (Language Integrated Query) in C# 3.0. LINQ needed a way to add query capabilities to collections without modifying existing collection types.

```csharp
// LINQ uses extension methods extensively
var numbers = new List<int> { 1, 2, 3, 4, 5 };
var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
```

---

## How Extension Methods Work

### Under the Hood

Extension methods are purely compile-time syntactic sugar. At runtime, they are called as regular static methods.

```csharp
// What you write
string result = myString.ToTitleCase();

// What the compiler generates
string result = StringExtensions.ToTitleCase(myString);
```

### Compiler Resolution

When the compiler encounters a method call, it follows this order:
1. **Instance methods** on the type
2. **Extension methods** in the current namespace
3. **Extension methods** in imported namespaces (using directives)

```csharp
public class MyClass
{
    public void DoSomething() 
    { 
        Console.WriteLine("Instance method"); 
    }
}

public static class MyClassExtensions
{
    public static void DoSomething(this MyClass obj) 
    { 
        Console.WriteLine("Extension method"); 
    }
}

// Usage
var obj = new MyClass();
obj.DoSomething(); // Outputs: "Instance method" (instance method wins)
```

---

## Syntax and Rules

### Basic Syntax

```csharp
// Extension method must be in a static class
public static class StringExtensions
{
    // Method must be static with 'this' modifier on first parameter
    public static string CapitalizeFirstLetter(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        
        return char.ToUpper(str[0]) + str.Substring(1);
    }
}
```

### Mandatory Rules

1. **Static Class**: Extension methods must be defined in a static class
2. **Static Method**: The method itself must be static
3. **`this` Modifier**: The first parameter must have the `this` modifier
4. **Non-Generic Static Class**: The containing class cannot be generic (but can contain generic methods)

```csharp
// ? WRONG - Not a static class
public class StringExtensions 
{
    public static string Reverse(this string str) { }
}

// ? WRONG - Not a static method
public static class StringExtensions 
{
    public string Reverse(this string str) { }
}

// ? WRONG - Missing 'this' modifier
public static class StringExtensions 
{
    public static string Reverse(string str) { }
}

// ? CORRECT
public static class StringExtensions 
{
    public static string Reverse(this string str) 
    {
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
```

### Generic Extension Methods

```csharp
public static class GenericExtensions
{
    // Generic extension method
    public static bool IsIn<T>(this T item, params T[] items)
    {
        return items.Contains(item);
    }
    
    // Extension method with multiple type parameters
    public static TResult ConvertTo<TSource, TResult>(this TSource source, 
        Func<TSource, TResult> converter)
    {
        return converter(source);
    }
}

// Usage
int number = 5;
bool exists = number.IsIn(1, 2, 3, 4, 5); // true

string result = 42.ConvertTo<int, string>(x => $"The answer is {x}");
```

---

## Real-World Examples

### Example 1: String Extensions

```csharp
public static class StringExtensions
{
    /// <summary>
    /// Capitalizes the first letter of the string
    /// </summary>
    public static string CapitalizeFirstLetter(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        
        return char.ToUpper(str[0]) + str.Substring(1);
    }
    
    /// <summary>
    /// Converts string to title case (capitalizes first letter of each word)
    /// </summary>
    public static string ToTitleCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
    }
    
    /// <summary>
    /// Truncates string to specified length and adds ellipsis
    /// </summary>
    public static string Truncate(this string str, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(str) || str.Length <= maxLength)
            return str;
        
        return str.Substring(0, maxLength - suffix.Length) + suffix;
    }
    
    /// <summary>
    /// Checks if string is a valid email address
    /// </summary>
    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Reverses the string
    /// </summary>
    public static string Reverse(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}

// Usage Examples
string greeting = "hello world";
string capitalized = greeting.CapitalizeFirstLetter(); // "Hello world"
string title = greeting.ToTitleCase(); // "Hello World"
string truncated = "This is a long string".Truncate(10); // "This is..."
bool isEmail = "test@example.com".IsValidEmail(); // true
string reversed = "hello".Reverse(); // "olleh"
```

### Example 2: Collection Extensions

```csharp
public static class CollectionExtensions
{
    /// <summary>
    /// Checks if collection is null or empty
    /// </summary>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    {
        return source == null || !source.Any();
    }
    
    /// <summary>
    /// Performs an action on each element in the collection
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (action == null) throw new ArgumentNullException(nameof(action));
        
        foreach (var item in source)
        {
            action(item);
        }
    }
    
    /// <summary>
    /// Returns distinct elements by a key selector
    /// </summary>
    public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, 
        Func<T, TKey> keySelector)
    {
        var seenKeys = new HashSet<TKey>();
        foreach (var element in source)
        {
            if (seenKeys.Add(keySelector(element)))
            {
                yield return element;
            }
        }
    }
    
    /// <summary>
    /// Splits collection into batches of specified size
    /// </summary>
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (batchSize <= 0) throw new ArgumentException("Batch size must be positive", nameof(batchSize));
        
        var batch = new List<T>(batchSize);
        foreach (var item in source)
        {
            batch.Add(item);
            if (batch.Count == batchSize)
            {
                yield return batch;
                batch = new List<T>(batchSize);
            }
        }
        
        if (batch.Count > 0)
            yield return batch;
    }
    
    /// <summary>
    /// Shuffles the collection randomly
    /// </summary>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        var random = new Random();
        return source.OrderBy(x => random.Next());
    }
}

// Usage Examples
var numbers = new List<int> { 1, 2, 3, 4, 5 };

if (!numbers.IsNullOrEmpty())
{
    numbers.ForEach(n => Console.WriteLine(n));
}

var people = new List<Person>
{
    new Person { Id = 1, Name = "John" },
    new Person { Id = 2, Name = "Jane" },
    new Person { Id = 1, Name = "John Duplicate" }
};
var distinctPeople = people.DistinctBy(p => p.Id);

var batches = numbers.Batch(2); // [[1,2], [3,4], [5]]
var shuffled = numbers.Shuffle();
```

### Example 3: DateTime Extensions

```csharp
public static class DateTimeExtensions
{
    /// <summary>
    /// Checks if date is a weekend
    /// </summary>
    public static bool IsWeekend(this DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || 
               date.DayOfWeek == DayOfWeek.Sunday;
    }
    
    /// <summary>
    /// Gets the start of the day (00:00:00)
    /// </summary>
    public static DateTime StartOfDay(this DateTime date)
    {
        return date.Date;
    }
    
    /// <summary>
    /// Gets the end of the day (23:59:59)
    /// </summary>
    public static DateTime EndOfDay(this DateTime date)
    {
        return date.Date.AddDays(1).AddTicks(-1);
    }
    
    /// <summary>
    /// Gets the start of the month
    /// </summary>
    public static DateTime StartOfMonth(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, 1);
    }
    
    /// <summary>
    /// Gets the end of the month
    /// </summary>
    public static DateTime EndOfMonth(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, 1).AddMonths(1).AddTicks(-1);
    }
    
    /// <summary>
    /// Calculates age from birthdate
    /// </summary>
    public static int CalculateAge(this DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }
    
    /// <summary>
    /// Gets a friendly relative time string (e.g., "2 hours ago")
    /// </summary>
    public static string ToRelativeTime(this DateTime dateTime)
    {
        var timeSpan = DateTime.Now - dateTime;
        
        if (timeSpan.TotalSeconds < 60)
            return "just now";
        if (timeSpan.TotalMinutes < 60)
            return $"{(int)timeSpan.TotalMinutes} minute(s) ago";
        if (timeSpan.TotalHours < 24)
            return $"{(int)timeSpan.TotalHours} hour(s) ago";
        if (timeSpan.TotalDays < 7)
            return $"{(int)timeSpan.TotalDays} day(s) ago";
        if (timeSpan.TotalDays < 30)
            return $"{(int)(timeSpan.TotalDays / 7)} week(s) ago";
        if (timeSpan.TotalDays < 365)
            return $"{(int)(timeSpan.TotalDays / 30)} month(s) ago";
        
        return $"{(int)(timeSpan.TotalDays / 365)} year(s) ago";
    }
}

// Usage Examples
DateTime today = DateTime.Now;
bool isWeekend = today.IsWeekend();
DateTime startOfDay = today.StartOfDay();
DateTime endOfMonth = today.EndOfMonth();

DateTime birthDate = new DateTime(1990, 5, 15);
int age = birthDate.CalculateAge(); // Current age

DateTime postTime = DateTime.Now.AddHours(-2);
string relativeTime = postTime.ToRelativeTime(); // "2 hour(s) ago"
```

### Example 4: Nullable Extensions

```csharp
public static class NullableExtensions
{
    /// <summary>
    /// Gets value or default if null
    /// </summary>
    public static T GetValueOrDefault<T>(this T? value, T defaultValue) where T : struct
    {
        return value.HasValue ? value.Value : defaultValue;
    }
    
    /// <summary>
    /// Executes action if value is not null
    /// </summary>
    public static void IfNotNull<T>(this T? value, Action<T> action) where T : struct
    {
        if (value.HasValue)
            action(value.Value);
    }
    
    /// <summary>
    /// Converts nullable to Option type pattern
    /// </summary>
    public static TResult Match<T, TResult>(this T? value, 
        Func<T, TResult> some, 
        Func<TResult> none) where T : struct
    {
        return value.HasValue ? some(value.Value) : none();
    }
}

// Usage Examples
int? nullableInt = null;
int result = nullableInt.GetValueOrDefault(42); // 42

int? age = 25;
age.IfNotNull(a => Console.WriteLine($"Age is {a}"));

string message = nullableInt.Match(
    some: x => $"Value is {x}",
    none: () => "No value"
);
```

### Example 5: Object Extensions

```csharp
public static class ObjectExtensions
{
    /// <summary>
    /// Deep clone using serialization
    /// </summary>
    public static T DeepClone<T>(this T source)
    {
        if (source == null) return default(T);
        
        var json = System.Text.Json.JsonSerializer.Serialize(source);
        return System.Text.Json.JsonSerializer.Deserialize<T>(json);
    }
    
    /// <summary>
    /// Converts object to dictionary
    /// </summary>
    public static Dictionary<string, object> ToDictionary(this object obj)
    {
        if (obj == null) return new Dictionary<string, object>();
        
        return obj.GetType()
            .GetProperties()
            .ToDictionary(
                prop => prop.Name,
                prop => prop.GetValue(obj)
            );
    }
    
    /// <summary>
    /// Checks if object is in a collection
    /// </summary>
    public static bool IsIn<T>(this T obj, params T[] args)
    {
        return args.Contains(obj);
    }
    
    /// <summary>
    /// Throws exception if object is null
    /// </summary>
    public static T ThrowIfNull<T>(this T obj, string paramName = null) where T : class
    {
        if (obj == null)
            throw new ArgumentNullException(paramName ?? nameof(obj));
        return obj;
    }
    
    /// <summary>
    /// Fluent null check with action
    /// </summary>
    public static T With<T>(this T obj, Action<T> action) where T : class
    {
        if (obj != null)
            action(obj);
        return obj;
    }
}

// Usage Examples
var person = new Person { Name = "John", Age = 30 };
var clone = person.DeepClone();
var dict = person.ToDictionary();

int dayOfWeek = 3;
if (dayOfWeek.IsIn(1, 2, 3, 4, 5))
    Console.WriteLine("Weekday");

var customer = GetCustomer().ThrowIfNull("customer");

person.With(p => p.Name = "Jane")
      .With(p => Console.WriteLine(p.Name));
```

---

## Best Practices

### 1. Naming Conventions

```csharp
// ? GOOD - Descriptive extension class names
public static class StringExtensions { }
public static class DateTimeExtensions { }
public static class CollectionExtensions { }

// ? BAD - Generic names
public static class Extensions { }
public static class Helpers { }
public static class Utils { }
```

### 2. Namespace Organization

```csharp
// ? GOOD - Organized by functionality
namespace MyCompany.Extensions.String
{
    public static class StringExtensions { }
}

namespace MyCompany.Extensions.Collections
{
    public static class IEnumerableExtensions { }
}

// ? GOOD - Common extensions in shared namespace
namespace MyCompany.Common.Extensions
{
    public static class CommonExtensions { }
}
```

### 3. Null Checking

```csharp
// ? GOOD - Check for null on reference types
public static string Reverse(this string str)
{
    if (str == null)
        throw new ArgumentNullException(nameof(str));
    
    // Implementation
}

// ? GOOD - Graceful null handling when appropriate
public static bool IsNullOrEmpty(this string str)
{
    return string.IsNullOrEmpty(str);
}
```

### 4. Keep Extensions Simple and Focused

```csharp
// ? GOOD - Simple, single responsibility
public static string ToTitleCase(this string str)
{
    return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
}

// ? BAD - Too complex, multiple responsibilities
public static string FormatAndValidateAndSave(this string str)
{
    // Format
    str = str.Trim().ToLower();
    
    // Validate
    if (string.IsNullOrEmpty(str))
        throw new Exception("Invalid");
    
    // Save to database
    SaveToDatabase(str);
    
    return str;
}
```

### 5. Don't Override Existing Behavior

```csharp
// ? BAD - Conflicts with existing methods
public static string ToString(this object obj)
{
    // This will never be called due to existing Object.ToString()
}

// ? GOOD - Provide complementary functionality
public static string ToJsonString(this object obj)
{
    return JsonSerializer.Serialize(obj);
}
```

### 6. Use Generic Constraints When Appropriate

```csharp
// ? GOOD - Constrains to reference types
public static T ThrowIfNull<T>(this T obj, string paramName) where T : class
{
    if (obj == null)
        throw new ArgumentNullException(paramName);
    return obj;
}

// ? GOOD - Constrains to value types
public static T GetValueOrDefault<T>(this T? value, T defaultValue) where T : struct
{
    return value ?? defaultValue;
}
```

### 7. Document Extension Methods

```csharp
/// <summary>
/// Truncates the string to the specified maximum length and appends a suffix.
/// </summary>
/// <param name="str">The string to truncate.</param>
/// <param name="maxLength">The maximum length of the resulting string.</param>
/// <param name="suffix">The suffix to append (default is "...").</param>
/// <returns>The truncated string with suffix, or the original string if shorter than maxLength.</returns>
/// <example>
/// <code>
/// string text = "This is a long string";
/// string result = text.Truncate(10); // "This is..."
/// </code>
/// </example>
public static string Truncate(this string str, int maxLength, string suffix = "...")
{
    if (string.IsNullOrEmpty(str) || str.Length <= maxLength)
        return str;
    
    return str.Substring(0, maxLength - suffix.Length) + suffix;
}
```

---

## Advanced Scenarios

### 1. Extending Interfaces

```csharp
// Extension method on interface
public static class IEnumerableExtensions
{
    public static void Print<T>(this IEnumerable<T> source)
    {
        foreach (var item in source)
        {
            Console.WriteLine(item);
        }
    }
}

// Works with any type implementing IEnumerable<T>
var list = new List<int> { 1, 2, 3 };
list.Print();

var array = new[] { "a", "b", "c" };
array.Print();

var hashSet = new HashSet<double> { 1.1, 2.2, 3.3 };
hashSet.Print();
```

### 2. Extension Methods with Multiple Parameters

```csharp
public static class NumberExtensions
{
    public static bool IsBetween(this int value, int min, int max, bool inclusive = true)
    {
        return inclusive 
            ? value >= min && value <= max
            : value > min && value < max;
    }
    
    public static string Format(this decimal value, string format, IFormatProvider provider = null)
    {
        return value.ToString(format, provider ?? CultureInfo.CurrentCulture);
    }
}

// Usage
int age = 25;
bool isAdult = age.IsBetween(18, 65); // true

decimal price = 1234.56m;
string formatted = price.Format("C2"); // "$1,234.56"
```

### 3. Chaining Extension Methods (Fluent API)

```csharp
public static class FluentStringExtensions
{
    public static string Trim(this string str)
    {
        return str?.Trim();
    }
    
    public static string ToLower(this string str)
    {
        return str?.ToLower();
    }
    
    public static string RemoveSpaces(this string str)
    {
        return str?.Replace(" ", "");
    }
    
    public static string EnsurePrefix(this string str, string prefix)
    {
        if (str == null) return prefix;
        return str.StartsWith(prefix) ? str : prefix + str;
    }
}

// Usage - Method chaining
string input = "  Hello World  ";
string result = input
    .Trim()
    .ToLower()
    .RemoveSpaces()
    .EnsurePrefix(">>"); // ">>helloworld"
```

### 4. Extension Methods with Lambda Expressions

```csharp
public static class FunctionalExtensions
{
    public static TResult Pipe<T, TResult>(this T value, Func<T, TResult> func)
    {
        return func(value);
    }
    
    public static T Tap<T>(this T value, Action<T> action)
    {
        action(value);
        return value;
    }
    
    public static TResult Let<T, TResult>(this T value, Func<T, TResult> func)
    {
        return func(value);
    }
    
    public static T Also<T>(this T value, Action<T> action)
    {
        action(value);
        return value;
    }
}

// Usage - Functional programming style
int result = 5
    .Pipe(x => x * 2)              // 10
    .Tap(x => Console.WriteLine(x)) // Output: 10
    .Pipe(x => x + 3)              // 13
    .Let(x => x.ToString());        // "13"

var person = new Person { Name = "John" }
    .Also(p => p.Age = 30)
    .Also(p => Console.WriteLine($"Created: {p.Name}"));
```

### 5. Extension Methods on Sealed Classes

```csharp
// String is a sealed class, but we can extend it
public static class SealedStringExtensions
{
    public static string ToBase64(this string str)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);
        return Convert.ToBase64String(bytes);
    }
    
    public static string FromBase64(this string base64)
    {
        var bytes = Convert.FromBase64String(base64);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}

// Usage
string original = "Hello World";
string encoded = original.ToBase64(); // "SGVsbG8gV29ybGQ="
string decoded = encoded.FromBase64(); // "Hello World"
```

### 6. Extension Methods with Out Parameters

```csharp
public static class ParsingExtensions
{
    public static bool TryParseInt(this string str, out int result)
    {
        return int.TryParse(str, out result);
    }
    
    public static bool TryParseDate(this string str, out DateTime result)
    {
        return DateTime.TryParse(str, out result);
    }
}

// Usage
string numberStr = "123";
if (numberStr.TryParseInt(out int number))
{
    Console.WriteLine($"Parsed: {number}");
}
```

---

## Common Use Cases

### 1. LINQ-Like Operations

```csharp
public static class LinqStyleExtensions
{
    public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        return source.Where(item => !predicate(item));
    }
    
    public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item))
                yield break;
            yield return item;
        }
    }
    
    public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) 
        where TKey : IComparable<TKey>
    {
        return source.OrderByDescending(selector).FirstOrDefault();
    }
}

// Usage
var numbers = new[] { 1, 2, 3, 4, 5, 6 };
var notEven = numbers.WhereNot(n => n % 2 == 0); // 1, 3, 5

var untilFive = numbers.TakeUntil(n => n > 5); // 1, 2, 3, 4, 5

var people = new[] 
{ 
    new Person { Name = "John", Age = 30 },
    new Person { Name = "Jane", Age = 35 }
};
var oldest = people.MaxBy(p => p.Age); // Jane
```

### 2. Validation Extensions

```csharp
public static class ValidationExtensions
{
    public static T ValidateNotNull<T>(this T obj, string paramName) where T : class
    {
        if (obj == null)
            throw new ArgumentNullException(paramName);
        return obj;
    }
    
    public static string ValidateNotEmpty(this string str, string paramName)
    {
        if (string.IsNullOrWhiteSpace(str))
            throw new ArgumentException("String cannot be empty", paramName);
        return str;
    }
    
    public static int ValidatePositive(this int value, string paramName)
    {
        if (value <= 0)
            throw new ArgumentException("Value must be positive", paramName);
        return value;
    }
    
    public static T ValidateRange<T>(this T value, T min, T max, string paramName) 
        where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new ArgumentOutOfRangeException(paramName);
        return value;
    }
}

// Usage
public void ProcessOrder(Order order, int quantity, string customerEmail)
{
    order.ValidateNotNull(nameof(order));
    customerEmail.ValidateNotEmpty(nameof(customerEmail));
    quantity.ValidatePositive(nameof(quantity));
    quantity.ValidateRange(1, 1000, nameof(quantity));
    
    // Process order...
}
```

### 3. Async Extension Methods

```csharp
public static class AsyncExtensions
{
    public static async Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout)
    {
        if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
        {
            return await task;
        }
        throw new TimeoutException("The operation has timed out.");
    }
    
    public static async Task<T> WithRetry<T>(this Func<Task<T>> taskFactory, 
        int maxAttempts, TimeSpan delay)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            try
            {
                return await taskFactory();
            }
            catch when (i < maxAttempts - 1)
            {
                await Task.Delay(delay);
            }
        }
        throw new Exception($"Failed after {maxAttempts} attempts");
    }
    
    public static async Task ForEachAsync<T>(this IEnumerable<T> source, 
        Func<T, Task> action, int maxParallel = 10)
    {
        var semaphore = new SemaphoreSlim(maxParallel);
        var tasks = source.Select(async item =>
        {
            await semaphore.WaitAsync();
            try
            {
                await action(item);
            }
            finally
            {
                semaphore.Release();
            }
        });
        
        await Task.WhenAll(tasks);
    }
}

// Usage
try
{
    var result = await GetDataAsync().WithTimeout(TimeSpan.FromSeconds(5));
}
catch (TimeoutException)
{
    Console.WriteLine("Operation timed out");
}

var data = await (() => FetchDataAsync()).WithRetry(3, TimeSpan.FromSeconds(2));

var urls = new[] { "url1", "url2", "url3" };
await urls.ForEachAsync(async url => await DownloadAsync(url), maxParallel: 5);
```

### 4. Conversion Extensions

```csharp
public static class ConversionExtensions
{
    public static int ToInt(this string str, int defaultValue = 0)
    {
        return int.TryParse(str, out int result) ? result : defaultValue;
    }
    
    public static decimal ToDecimal(this string str, decimal defaultValue = 0m)
    {
        return decimal.TryParse(str, out decimal result) ? result : defaultValue;
    }
    
    public static bool ToBool(this string str)
    {
        return str?.ToLower() == "true" || str == "1";
    }
    
    public static byte[] ToByteArray(this string str)
    {
        return System.Text.Encoding.UTF8.GetBytes(str);
    }
    
    public static string ToHexString(this byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", "");
    }
}

// Usage
string numberStr = "123";
int number = numberStr.ToInt(); // 123

string invalidStr = "abc";
int defaulted = invalidStr.ToInt(-1); // -1

string boolStr = "true";
bool flag = boolStr.ToBool(); // true

byte[] bytes = "Hello".ToByteArray();
string hex = bytes.ToHexString();
```

### 5. Builder Pattern Extensions

```csharp
public class EmailMessage
{
    public string From { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<string> Attachments { get; set; } = new List<string>();
}

public static class EmailBuilderExtensions
{
    public static EmailMessage WithFrom(this EmailMessage email, string from)
    {
        email.From = from;
        return email;
    }
    
    public static EmailMessage WithTo(this EmailMessage email, string to)
    {
        email.To = to;
        return email;
    }
    
    public static EmailMessage WithSubject(this EmailMessage email, string subject)
    {
        email.Subject = subject;
        return email;
    }
    
    public static EmailMessage WithBody(this EmailMessage email, string body)
    {
        email.Body = body;
        return email;
    }
    
    public static EmailMessage AddAttachment(this EmailMessage email, string attachment)
    {
        email.Attachments.Add(attachment);
        return email;
    }
}

// Usage - Fluent API
var email = new EmailMessage()
    .WithFrom("sender@example.com")
    .WithTo("recipient@example.com")
    .WithSubject("Hello")
    .WithBody("This is the body")
    .AddAttachment("file1.pdf")
    .AddAttachment("file2.pdf");
```

---

## Limitations and Gotchas

### 1. Cannot Access Private Members

```csharp
public class MyClass
{
    private int privateField = 10;
    private void PrivateMethod() { }
}

public static class MyClassExtensions
{
    public static void TryAccess(this MyClass obj)
    {
        // ? Cannot access private members
        // var value = obj.privateField; // Compile error
        // obj.PrivateMethod(); // Compile error
        
        // ? Can only access public members
        // var value = obj.PublicProperty;
    }
}
```

### 2. Instance Methods Have Priority

```csharp
public class MyClass
{
    public void DoSomething()
    {
        Console.WriteLine("Instance method");
    }
}

public static class MyClassExtensions
{
    public static void DoSomething(this MyClass obj)
    {
        Console.WriteLine("Extension method");
    }
}

// Usage
var obj = new MyClass();
obj.DoSomething(); // Always calls instance method: "Instance method"
```

### 3. Extension Method Hiding

```csharp
namespace Namespace1
{
    public static class Extensions
    {
        public static void Print(this string str)
        {
            Console.WriteLine("Namespace1: " + str);
        }
    }
}

namespace Namespace2
{
    public static class Extensions
    {
        public static void Print(this string str)
        {
            Console.WriteLine("Namespace2: " + str);
        }
    }
}

// In your code
using Namespace1;
using Namespace2; // ? Ambiguous call

class Program
{
    static void Main()
    {
        "Hello".Print(); // ? Compile error - ambiguous
        
        // ? Must use fully qualified name
        Namespace1.Extensions.Print("Hello");
    }
}
```

### 4. Cannot Override Virtual Methods

```csharp
public class BaseClass
{
    public virtual void Method()
    {
        Console.WriteLine("Base");
    }
}

public static class Extensions
{
    // ? This does NOT override the virtual method
    public static void Method(this BaseClass obj)
    {
        Console.WriteLine("Extension");
    }
}

public class DerivedClass : BaseClass
{
    public override void Method()
    {
        Console.WriteLine("Derived");
    }
}

// Usage
BaseClass obj = new DerivedClass();
obj.Method(); // Outputs "Derived" (not "Extension")
```

### 5. Null Reference Issues

```csharp
public static class NullExtensions
{
    public static void Print(this string str)
    {
        // ? Extension methods CAN be called on null references
        Console.WriteLine(str ?? "NULL");
    }
}

// Usage
string nullStr = null;
nullStr.Print(); // Works! Outputs "NULL"

// This is because extension methods are static methods
// The above is equivalent to:
NullExtensions.Print(nullStr);
```

### 6. Generic Type Inference Limitations

```csharp
public static class GenericExtensions
{
    public static void Print<T>(this T value)
    {
        Console.WriteLine(value);
    }
    
    // ? Ambiguous when used with nullable types
    public static void Print<T>(this T? value) where T : struct
    {
        Console.WriteLine(value.HasValue ? value.Value.ToString() : "NULL");
    }
}

// Usage
int? nullable = 5;
nullable.Print(); // May be ambiguous depending on context
```

### 7. Namespace Import Required

```csharp
namespace MyLibrary.Extensions
{
    public static class StringExtensions
    {
        public static string Reverse(this string str)
        {
            // Implementation
        }
    }
}

// In another file
namespace MyApp
{
    // ? Without using directive, extension method is not visible
    class Program
    {
        void Test()
        {
            string s = "hello";
            s.Reverse(); // ? Compile error
        }
    }
}

// ? Must import namespace
namespace MyApp
{
    using MyLibrary.Extensions; // Required
    
    class Program
    {
        void Test()
        {
            string s = "hello";
            s.Reverse(); // ? Works now
        }
    }
}
```

---

## Performance Considerations

### 1. No Performance Overhead

Extension methods are syntactic sugar with zero performance overhead at runtime. They compile to regular static method calls.

```csharp
// These are identical in IL code:
string result1 = str.Reverse();
string result2 = StringExtensions.Reverse(str);
```

### 2. Avoid Boxing with Value Types

```csharp
// ? BAD - Causes boxing
public static void Print(this object obj)
{
    Console.WriteLine(obj);
}

int number = 42;
number.Print(); // Boxes int to object

// ? GOOD - Generic, no boxing
public static void Print<T>(this T value)
{
    Console.WriteLine(value);
}

int number = 42;
number.Print(); // No boxing
```

### 3. IEnumerable vs IQueryable

```csharp
public static class QueryExtensions
{
    // ? BAD - Forces immediate execution
    public static IEnumerable<T> FilterActive<T>(this IEnumerable<T> source) 
        where T : IActive
    {
        return source.Where(x => x.IsActive);
    }
    
    // ? GOOD - Maintains deferred execution for IQueryable
    public static IQueryable<T> FilterActive<T>(this IQueryable<T> source) 
        where T : IActive
    {
        return source.Where(x => x.IsActive);
    }
}

// Usage with Entity Framework
var activeUsers = dbContext.Users
    .FilterActive() // Translates to SQL with IQueryable version
    .ToList();
```

### 4. Lazy Evaluation with yield

```csharp
// ? GOOD - Lazy evaluation
public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
{
    foreach (var item in source)
    {
        if (predicate(item))
            yield return item;
    }
}

// ? BAD - Immediate evaluation
public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
{
    var result = new List<T>();
    foreach (var item in source)
    {
        if (predicate(item))
            result.Add(item);
    }
    return result;
}
```

### 5. Memory Allocation Considerations

```csharp
// ? BAD - Creates new string on every call
public static string AddPrefix(this string str, string prefix)
{
    return prefix + str; // String concatenation creates new string
}

// ? BETTER - Uses StringBuilder for multiple operations
public static string Transform(this string str, params Func<string, string>[] transformations)
{
    var sb = new StringBuilder(str);
    foreach (var transform in transformations)
    {
        str = transform(str);
        sb.Clear();
        sb.Append(str);
    }
    return sb.ToString();
}

// ? BEST - Use Span<T> for performance-critical scenarios
public static ReadOnlySpan<char> TrimPrefix(this ReadOnlySpan<char> span, ReadOnlySpan<char> prefix)
{
    return span.StartsWith(prefix) ? span.Slice(prefix.Length) : span;
}
```

---

## Interview Questions

### Basic Questions

**Q1: What is an extension method in C#?**

**A:** An extension method is a static method that can be called as if it were an instance method on the extended type. It allows developers to add new methods to existing types without modifying the original type, creating a derived type, or recompiling. Extension methods must be defined in a static class, and the first parameter must use the `this` modifier.

**Q2: What are the basic requirements for creating an extension method?**

**A:** The requirements are:
1. Must be defined in a static class
2. Must be a static method
3. First parameter must have the `this` modifier
4. First parameter specifies the type being extended
5. The namespace containing the extension method must be imported with a `using` directive

**Q3: Can extension methods access private members of the extended type?**

**A:** No. Extension methods can only access public members of the extended type because they are external static methods, not actual instance methods of the type.

**Q4: What happens if an extension method has the same name as an instance method?**

**A:** The instance method always takes priority. The extension method will never be called if there's an instance method with the same signature. The compiler resolves method calls in this order:
1. Instance methods
2. Extension methods in current namespace
3. Extension methods in imported namespaces

**Q5: Can extension methods be called on null references?**

**A:** Yes. Since extension methods are actually static methods, they can be called on null references. However, you need to handle null checks inside the extension method:

```csharp
public static string SafePrint(this string str)
{
    return str ?? "NULL";
}

string nullStr = null;
nullStr.SafePrint(); // Works, returns "NULL"
```

### Intermediate Questions

**Q6: What is the difference between instance methods and extension methods at runtime?**

**A:** At runtime, there is no difference in how they are called. Extension methods are compiled as regular static method calls. The extension method syntax is purely syntactic sugar provided by the compiler. For example:
- What you write: `str.Reverse()`
- What the compiler generates: `StringExtensions.Reverse(str)`

**Q7: Can you create extension methods for sealed classes?**

**A:** Yes. Extension methods can extend any type, including sealed classes. This is one of their major advantages. For example, you can extend the `string` class (which is sealed) with custom methods.

**Q8: How do you create generic extension methods?**

**A:** Generic extension methods use type parameters and can include constraints:

```csharp
public static class GenericExtensions
{
    public static bool IsIn<T>(this T item, params T[] items)
    {
        return items.Contains(item);
    }
    
    public static T ThrowIfNull<T>(this T obj, string paramName) where T : class
    {
        if (obj == null)
            throw new ArgumentNullException(paramName);
        return obj;
    }
}
```

**Q9: Can extension methods be used with interfaces?**

**A:** Yes, and this is one of the most powerful features of extension methods. When you extend an interface, the extension method becomes available to all types that implement that interface:

```csharp
public static class IEnumerableExtensions
{
    public static void Print<T>(this IEnumerable<T> source)
    {
        foreach (var item in source)
            Console.WriteLine(item);
    }
}

// Works with all IEnumerable<T> implementations
var list = new List<int> { 1, 2, 3 };
list.Print();

var array = new[] { "a", "b", "c" };
array.Print();
```

**Q10: What is the relationship between extension methods and LINQ?**

**A:** LINQ is built entirely on extension methods. All LINQ operators (Where, Select, OrderBy, etc.) are extension methods defined on IEnumerable<T> and IQueryable<T>. This allows LINQ to add query capabilities to existing collection types without modifying them.

### Advanced Questions

**Q11: What happens when two extension methods with the same name are available from different namespaces?**

**A:** The compiler generates an ambiguity error. You must either:
1. Remove one of the using directives
2. Use the fully qualified static method name
3. Create an alias for one of the namespaces

```csharp
using Namespace1;
using Namespace2;

// ? Ambiguous if both have Print extension
"Hello".Print(); // Compile error

// ? Use fully qualified name
Namespace1.Extensions.Print("Hello");
```

**Q12: Can extension methods override virtual methods?**

**A:** No. Extension methods cannot override virtual methods because they are not actual instance methods. Polymorphism does not apply to extension methods:

```csharp
public class Base
{
    public virtual void Method() => Console.WriteLine("Base");
}

public static class Extensions
{
    public static void Method(this Base obj) => Console.WriteLine("Extension");
}

public class Derived : Base
{
    public override void Method() => Console.WriteLine("Derived");
}

Base obj = new Derived();
obj.Method(); // Outputs "Derived", not "Extension"
```

**Q13: How do extension methods affect performance?**

**A:** Extension methods have zero performance overhead compared to regular static methods. They are syntactic sugar that is resolved at compile time. The IL code generated is identical to calling a static method directly. However, the implementation inside the extension method can have performance implications (boxing, memory allocation, etc.).

**Q14: What are the best practices for naming extension method classes?**

**A:** Best practices include:
1. Use descriptive names: `StringExtensions`, `DateTimeExtensions`
2. Group related extensions in the same class
3. Use the `Extensions` suffix consistently
4. Organize in namespaces by functionality
5. Avoid generic names like `Helpers` or `Utils`

**Q15: Can you create extension methods with ref or out parameters?**

**A:** Yes, but the `this` parameter cannot be ref or out. Additional parameters can be ref or out:

```csharp
// ? WRONG - this parameter cannot be ref/out
public static void Invalid(ref this string str) { }

// ? CORRECT - additional parameters can be ref/out
public static bool TryParseInt(this string str, out int result)
{
    return int.TryParse(str, out result);
}
```

**Q16: What is the difference between extending IEnumerable<T> and IQueryable<T>?**

**A:**
- **IEnumerable<T>**: Executes in-memory, uses LINQ to Objects, operations run on client side
- **IQueryable<T>**: Builds expression trees, translates to provider-specific queries (SQL, etc.), operations run on server side

```csharp
// IEnumerable - runs in memory
public static IEnumerable<T> FilterActive<T>(this IEnumerable<T> source) where T : IActive
{
    return source.Where(x => x.IsActive); // Filters in C#
}

// IQueryable - translates to SQL
public static IQueryable<T> FilterActive<T>(this IQueryable<T> source) where T : IActive
{
    return source.Where(x => x.IsActive); // Translates to SQL WHERE clause
}
```

**Q17: How do you create extension methods that support method chaining (fluent API)?**

**A:** Return the extended object (or a modified version) from the extension method:

```csharp
public static class FluentExtensions
{
    public static StringBuilder AppendLine(this StringBuilder sb, string value, bool condition)
    {
        if (condition)
            sb.AppendLine(value);
        return sb;
    }
}

// Method chaining
var result = new StringBuilder()
    .AppendLine("First", true)
    .AppendLine("Second", false)  // Skipped
    .AppendLine("Third", true)
    .ToString();
```

**Q18: Can extension methods be used in reflection?**

**A:** Extension methods are regular static methods at the IL level, so they can be found using reflection. However, they won't appear as instance methods when you reflect on the extended type. You need to search for static methods with the ExtensionAttribute:

```csharp
var extensionMethods = typeof(StringExtensions)
    .GetMethods(BindingFlags.Static | BindingFlags.Public)
    .Where(m => m.IsDefined(typeof(ExtensionAttribute), false));
```

**Q19: What are the security implications of extension methods?**

**A:** Extension methods:
1. Cannot access private members, so they don't break encapsulation
2. Can be defined by any assembly, potentially causing conflicts
3. Require namespace imports, giving you control over what's available
4. Should validate parameters (especially for public APIs)
5. Should be documented to avoid confusion with real instance methods

**Q20: How do extension methods relate to SOLID principles?**

**A:** Extension methods support several SOLID principles:
- **Open/Closed Principle**: Extend types without modifying them
- **Interface Segregation**: Add specific functionality to interfaces without forcing all implementations to have it
- **Single Responsibility**: Separate concerns by adding functionality externally
- **Dependency Inversion**: Extend abstractions (interfaces) rather than concrete types

### Expert Questions

**Q21: How can you create extension methods that work with both synchronous and asynchronous code?**

**A:** Create separate extension methods for Task<T> and T, or use async/await:

```csharp
public static class AsyncExtensions
{
    // Synchronous version
    public static T Process<T>(this T value)
    {
        // Process
        return value;
    }
    
    // Asynchronous version
    public static async Task<T> ProcessAsync<T>(this Task<T> task)
    {
        var value = await task;
        // Process
        return value;
    }
    
    // Fluent async chaining
    public static async Task<TResult> Then<T, TResult>(
        this Task<T> task, 
        Func<T, TResult> func)
    {
        var result = await task;
        return func(result);
    }
}
```

**Q22: How do extension methods interact with expression trees in Entity Framework?**

**A:** Extension methods on IQueryable<T> work with expression trees, but they must:
1. Use expressions that can be translated to the target query language (SQL, etc.)
2. Avoid method calls that can't be translated
3. Return IQueryable<T> to maintain query composition

```csharp
// ? Translatable - uses expression
public static IQueryable<T> WhereActive<T>(this IQueryable<T> query) where T : IActive
{
    return query.Where(x => x.IsActive);
}

// ? Not translatable - calls C# method
public static IQueryable<User> WhereActiveUsers(this IQueryable<User> query)
{
    return query.Where(u => IsUserActive(u)); // Can't translate IsUserActive to SQL
}
```

**Q23: Can you explain the internals of how the compiler resolves extension methods?**

**A:** The compiler:
1. Checks if the method exists as an instance method (highest priority)
2. Searches for extension methods in the current namespace
3. Searches for extension methods in imported namespaces
4. Performs type inference for generic extension methods
5. Applies overload resolution rules
6. Transforms the call into a static method call in the generated IL

The compiler adds the ExtensionAttribute to extension methods in the IL code to mark them.

**Q24: How can you create extension methods that work with ref structs (Span<T>)?**

**A:** Extension methods can extend ref structs like Span<T> and ReadOnlySpan<T>:

```csharp
public static class SpanExtensions
{
    public static int IndexOf<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>
    {
        for (int i = 0; i < span.Length; i++)
        {
            if (span[i].Equals(value))
                return i;
        }
        return -1;
    }
    
    public static void Fill<T>(this Span<T> span, T value)
    {
        for (int i = 0; i < span.Length; i++)
        {
            span[i] = value;
        }
    }
}
```

**Q25: What are the implications of using extension methods in library code vs application code?**

**A:**

**Library Code:**
- Use conservative naming to avoid conflicts
- Document extensively
- Consider InternalsVisibleTo for internal extensions
- Be careful with breaking changes
- Version carefully (extensions can be removed or changed)

**Application Code:**
- More freedom with naming
- Can be more specific to domain
- Less concern about conflicts
- Can refactor more easily

```csharp
// Library - conservative naming
namespace MyLibrary.Extensions.String
{
    public static class StringExtensions { }
}

// Application - can be more specific
namespace MyApp.Extensions
{
    public static class OrderExtensions { }
}
```

---

## Summary

Extension methods are a powerful C# feature that enables:
- **Code Extension**: Add methods to types you don't own
- **Clean Syntax**: Write fluent, readable code
- **LINQ Support**: Foundation of Language Integrated Query
- **Interface Enhancement**: Add functionality to interfaces
- **Open/Closed Principle**: Extend without modification

### Key Takeaways

1. Extension methods are static methods with `this` modifier on first parameter
2. They are syntactic sugar compiled to static method calls
3. Instance methods always have priority over extension methods
4. They can extend any type including sealed classes and interfaces
5. They cannot access private members of extended types
6. They are the foundation of LINQ
7. They have zero performance overhead
8. They require namespace imports to be visible
9. They support generic types and constraints
10. They enable fluent API design and method chaining

### When to Use Extension Methods

**Use extension methods when:**
- Extending third-party or framework types
- Adding utility methods to common types
- Creating fluent APIs
- Extending interfaces with default behavior
- Building LINQ-like query operations

**Avoid extension methods when:**
- You own the type and can modify it directly
- The method needs access to private members
- The method is core functionality of the type
- It would conflict with existing or potential future methods
- The functionality is complex and better suited to a separate class

---

## Additional Resources

- [Microsoft Documentation: Extension Methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)
- [C# Language Specification: Extension Methods](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#extension-methods)
- [LINQ Architecture and Design](https://docs.microsoft.com/en-us/dotnet/standard/linq/)

---

**Document Version:** 1.0  
**Last Updated:** 2024  
**Target Framework:** .NET 6.0 and later  
**C# Version:** C# 10.0 and later
