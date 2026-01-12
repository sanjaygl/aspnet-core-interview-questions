# Adapter Pattern

## Pattern Category
**Structural Pattern**

## Intent / Definition
The Adapter pattern allows incompatible interfaces to work together. It acts as a bridge between two incompatible interfaces by wrapping an existing class with a new interface, making it compatible with the client's expectations.

## Problem Statement
- You have an existing class with a useful interface that doesn't match what you need
- You want to use a third-party library with an incompatible interface
- You need to integrate legacy code with new systems
- Two classes have similar functionality but different interfaces
- You can't modify existing classes due to constraints

## When to Use
✅ **Use Adapter when:**
- You need to use an existing class with an incompatible interface
- Integrating third-party libraries or legacy systems
- You want to create a reusable class that cooperates with unrelated classes
- You need to use several existing subclasses but can't adapt their interface by subclassing

❌ **Don't use Adapter when:**
- You can modify the original class interface
- The interface differences are minimal
- You're creating new code from scratch
- Simple inheritance can solve the problem

## UML Structure
```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│     Client      │    │     Target      │    │    Adaptee      │
├─────────────────┤    ├─────────────────┤    ├─────────────────┤
│                 │───▷│ + Request()     │    │ + SpecificReq() │
└─────────────────┘    └─────────────────┘    └─────────────────┘
                               △                       △
                               │                       │
                       ┌─────────────────┐            │
                       │     Adapter     │            │
                       ├─────────────────┤            │
                       │ - adaptee       │───────────┘
                       │ + Request()     │
                       └─────────────────┘
```

## C# Implementation

### Payment Gateway Adapter Example
```csharp
// Target interface that client expects
public interface IPaymentProcessor
{
    Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string cardNumber);
    Task<bool> RefundAsync(string transactionId, decimal amount);
}

// Adaptee - Third-party payment library with different interface
public class LegacyPayPalGateway
{
    public string MakePayment(double amountInDollars, string creditCardNum)
    {
        Console.WriteLine($"Processing ${amountInDollars} via PayPal Legacy API");
        return $"PP_{Guid.NewGuid().ToString("N")[..8]}";
    }
    
    public bool ProcessRefund(string transactionRef, double refundAmount)
    {
        Console.WriteLine($"Refunding ${refundAmount} for transaction {transactionRef}");
        return true;
    }
}

// Another Adaptee - Different third-party service
public class StripeApiClient
{
    public StripeResponse CreateCharge(int amountInCents, string source, string currency)
    {
        Console.WriteLine($"Stripe: Charging {amountInCents} cents in {currency}");
        return new StripeResponse 
        { 
            Id = $"ch_{Guid.NewGuid().ToString("N")[..12]}", 
            Success = true 
        };
    }
    
    public StripeResponse CreateRefund(string chargeId, int amountInCents)
    {
        Console.WriteLine($"Stripe: Refunding {amountInCents} cents for {chargeId}");
        return new StripeResponse { Id = $"re_{Guid.NewGuid().ToString("N")[..12]}", Success = true };
    }
}

public class StripeResponse
{
    public string Id { get; set; }
    public bool Success { get; set; }
}

// Adapter for PayPal
public class PayPalAdapter : IPaymentProcessor
{
    private readonly LegacyPayPalGateway _payPalGateway;
    
    public PayPalAdapter(LegacyPayPalGateway payPalGateway)
    {
        _payPalGateway = payPalGateway;
    }
    
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string cardNumber)
    {
        // Adapt the interface - convert decimal to double, handle currency
        if (currency != "USD")
        {
            throw new NotSupportedException("PayPal adapter only supports USD");
        }
        
        var transactionId = _payPalGateway.MakePayment((double)amount, cardNumber);
        
        return new PaymentResult
        {
            TransactionId = transactionId,
            Success = !string.IsNullOrEmpty(transactionId),
            Message = "Payment processed via PayPal"
        };
    }
    
    public async Task<bool> RefundAsync(string transactionId, decimal amount)
    {
        return _payPalGateway.ProcessRefund(transactionId, (double)amount);
    }
}

// Adapter for Stripe
public class StripeAdapter : IPaymentProcessor
{
    private readonly StripeApiClient _stripeClient;
    
    public StripeAdapter(StripeApiClient stripeClient)
    {
        _stripeClient = stripeClient;
    }
    
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string cardNumber)
    {
        // Adapt the interface - convert decimal to cents, pass currency
        int amountInCents = (int)(amount * 100);
        
        var response = _stripeClient.CreateCharge(amountInCents, cardNumber, currency.ToLower());
        
        return new PaymentResult
        {
            TransactionId = response.Id,
            Success = response.Success,
            Message = "Payment processed via Stripe"
        };
    }
    
    public async Task<bool> RefundAsync(string transactionId, decimal amount)
    {
        int amountInCents = (int)(amount * 100);
        var response = _stripeClient.CreateRefund(transactionId, amountInCents);
        return response.Success;
    }
}

// Common result type
public class PaymentResult
{
    public string TransactionId { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}

// Client code
public class PaymentService
{
    private readonly IPaymentProcessor _paymentProcessor;
    
    public PaymentService(IPaymentProcessor paymentProcessor)
    {
        _paymentProcessor = paymentProcessor;
    }
    
    public async Task<PaymentResult> ProcessOrderPaymentAsync(decimal amount, string currency, string cardNumber)
    {
        return await _paymentProcessor.ProcessPaymentAsync(amount, currency, cardNumber);
    }
}
```

### Usage Example
```csharp
// Using different payment gateways through the same interface
public class PaymentExample
{
    public async Task DemonstrateAdapters()
    {
        // PayPal adapter
        var payPalGateway = new LegacyPayPalGateway();
        var payPalAdapter = new PayPalAdapter(payPalGateway);
        var payPalService = new PaymentService(payPalAdapter);
        
        var payPalResult = await payPalService.ProcessOrderPaymentAsync(100.50m, "USD", "4111111111111111");
        Console.WriteLine($"PayPal: {payPalResult.Message} - {payPalResult.TransactionId}");
        
        // Stripe adapter
        var stripeClient = new StripeApiClient();
        var stripeAdapter = new StripeAdapter(stripeClient);
        var stripeService = new PaymentService(stripeAdapter);
        
        var stripeResult = await stripeService.ProcessOrderPaymentAsync(100.50m, "USD", "4242424242424242");
        Console.WriteLine($"Stripe: {stripeResult.Message} - {stripeResult.TransactionId}");
    }
}
```

## Real-world / Enterprise Use Case

### ASP.NET Core Configuration Adapter
```csharp
// Legacy configuration system
public class LegacyConfigurationManager
{
    private readonly Dictionary<string, string> _settings = new();
    
    public void LoadFromFile(string filePath)
    {
        // Load configuration from legacy format
        _settings["DatabaseConnection"] = "Server=localhost;Database=LegacyDB";
        _settings["ApiTimeout"] = "30000";
    }
    
    public string GetSetting(string key) => _settings.TryGetValue(key, out var value) ? value : null;
}

// Modern .NET Core configuration interface
public interface IModernConfiguration
{
    T GetValue<T>(string key);
    string GetConnectionString(string name);
}

// Adapter to bridge legacy and modern systems
public class ConfigurationAdapter : IModernConfiguration
{
    private readonly LegacyConfigurationManager _legacyConfig;
    
    public ConfigurationAdapter(LegacyConfigurationManager legacyConfig)
    {
        _legacyConfig = legacyConfig;
    }
    
    public T GetValue<T>(string key)
    {
        var value = _legacyConfig.GetSetting(key);
        if (value == null) return default(T);
        
        return (T)Convert.ChangeType(value, typeof(T));
    }
    
    public string GetConnectionString(string name)
    {
        return _legacyConfig.GetSetting($"{name}Connection");
    }
}

// Usage in DI container
public void ConfigureServices(IServiceCollection services)
{
    var legacyConfig = new LegacyConfigurationManager();
    legacyConfig.LoadFromFile("legacy-config.ini");
    
    var adapter = new ConfigurationAdapter(legacyConfig);
    services.AddSingleton<IModernConfiguration>(adapter);
}
```

## Pros and Cons

### ✅ Pros
- **Reusability**: Allows reuse of existing functionality without modification
- **Separation of Concerns**: Keeps business logic separate from interface conversion
- **Open/Closed Principle**: Open for extension, closed for modification
- **Legacy Integration**: Enables integration with legacy systems
- **Third-party Integration**: Simplifies working with external libraries

### ❌ Cons
- **Increased Complexity**: Adds another layer of abstraction
- **Performance Overhead**: Additional method calls and object creation
- **Maintenance**: More classes to maintain
- **Debugging Difficulty**: Can make debugging more complex
- **Over-engineering**: May be overkill for simple interface differences

## Common Mistakes & Anti-Patterns

### ❌ **Mistake 1: Adapter Doing Too Much**
```csharp
// Wrong - Adapter contains business logic
public class BadPaymentAdapter : IPaymentProcessor
{
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string cardNumber)
    {
        // Business logic doesn't belong in adapter
        if (amount > 10000)
        {
            await SendFraudAlert(amount, cardNumber);
        }
        
        // Validation logic doesn't belong here
        if (!IsValidCardNumber(cardNumber))
        {
            throw new ArgumentException("Invalid card number");
        }
        
        // Adapter should only adapt interfaces
        return await _gateway.ProcessPayment(amount, currency, cardNumber);
    }
}

// Correct - Adapter only adapts interfaces
public class GoodPaymentAdapter : IPaymentProcessor
{
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string cardNumber)
    {
        // Only interface adaptation
        var result = _legacyGateway.MakePayment((double)amount, cardNumber);
        return new PaymentResult { TransactionId = result, Success = !string.IsNullOrEmpty(result) };
    }
}
```

### ❌ **Mistake 2: Not Handling Exceptions Properly**
```csharp
// Wrong - Not adapting exceptions
public class BadAdapter : IPaymentProcessor
{
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string cardNumber)
    {
        // Legacy system throws different exceptions
        var result = _legacySystem.Process(amount); // Might throw LegacyException
        return new PaymentResult { Success = true };
    }
}

// Correct - Adapt exceptions too
public class GoodAdapter : IPaymentProcessor
{
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string cardNumber)
    {
        try
        {
            var result = _legacySystem.Process(amount);
            return new PaymentResult { Success = true, TransactionId = result };
        }
        catch (LegacySystemException ex)
        {
            throw new PaymentProcessingException("Payment failed", ex);
        }
    }
}
```

## Performance Considerations

- **Minimal Overhead**: Adapter should add minimal performance overhead
- **Object Creation**: Consider object pooling for frequently used adapters
- **Caching**: Cache adapted results when appropriate
- **Lazy Loading**: Initialize expensive adaptees only when needed

```csharp
public class PerformantAdapter : IPaymentProcessor
{
    private readonly Lazy<ExpensivePaymentGateway> _gateway;
    private readonly IMemoryCache _cache;
    
    public PerformantAdapter(IMemoryCache cache)
    {
        _cache = cache;
        _gateway = new Lazy<ExpensivePaymentGateway>(() => new ExpensivePaymentGateway());
    }
    
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string cardNumber)
    {
        var cacheKey = $"payment_{cardNumber}_{amount}";
        
        if (_cache.TryGetValue(cacheKey, out PaymentResult cachedResult))
        {
            return cachedResult;
        }
        
        var result = await _gateway.Value.ProcessAsync(amount, currency, cardNumber);
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
        
        return result;
    }
}
```

## Relation to SOLID Principles

- **Single Responsibility**: Adapter has one job - interface conversion
- **Open/Closed**: Open for extension (new adapters), closed for modification
- **Liskov Substitution**: Adapters should be substitutable for the target interface
- **Interface Segregation**: Adapters help integrate systems with different interface granularity
- **Dependency Inversion**: Clients depend on abstractions, not concrete adapters

## Interview Cross-Questions with Answers

### Q1: **What's the difference between Adapter and Facade patterns?**
**Answer:**
- **Adapter**: Makes incompatible interfaces compatible, typically wraps one class
- **Facade**: Provides a simplified interface to a complex subsystem, may wrap multiple classes
- **Intent**: Adapter focuses on compatibility, Facade focuses on simplification

### Q2: **How would you implement an Adapter for a REST API client?**
**Answer:**
```csharp
public interface IUserService
{
    Task<User> GetUserAsync(int id);
}

public class RestApiAdapter : IUserService
{
    private readonly HttpClient _httpClient;
    
    public async Task<User> GetUserAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/users/{id}");
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<User>(json);
    }
}
```

### Q3: **When would you choose Adapter over inheritance?**
**Answer:**
- Use Adapter when you can't modify the existing class
- When you need to adapt multiple unrelated classes
- When the adapted class is from a third-party library
- When you want to avoid tight coupling through inheritance

### Q4: **How does Adapter pattern support the Open/Closed Principle?**
**Answer:**
- Classes are closed for modification (don't change existing code)
- Open for extension (add new adapters for new interfaces)
- New requirements are handled by creating new adapters, not modifying existing code

## Quick Revision / Summary

**Purpose**: Make incompatible interfaces work together  
**Structure**: Wrapper that translates one interface to another  
**Use Case**: Third-party integration, legacy system integration  
**Key Benefit**: Reuse existing code without modification  
**Common Example**: Payment gateway integration, API client wrappers  
**SOLID**: Supports Open/Closed and Dependency Inversion principles