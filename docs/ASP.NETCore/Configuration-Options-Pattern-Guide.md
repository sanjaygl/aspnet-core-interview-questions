# Configuration & Options Pattern in ASP.NET Core – Clean and Maintainable Settings

## Introduction

Configuration management is a critical aspect of building production-ready applications. Hard-coding values like connection strings, API keys, or feature flags directly in code makes applications inflexible, difficult to deploy across environments, and a security risk. ASP.NET Core provides a powerful, extensible configuration system with the Options Pattern that enables strongly-typed, validated, and environment-specific settings. Understanding these concepts is essential for building maintainable applications and is frequently discussed in technical interviews.

---

## Configuration in ASP.NET Core (Overview)

ASP.NET Core uses a flexible configuration system that reads settings from multiple sources. These sources are called **configuration providers**.

### Common Configuration Providers

1. **appsettings.json** - Default configuration file for all environments
2. **appsettings.{Environment}.json** - Environment-specific overrides (Development, Staging, Production)
3. **Environment Variables** - System or container-level settings
4. **Command-line Arguments** - Runtime parameters
5. **User Secrets** - Development-only secrets (not committed to source control)
6. **Azure Key Vault** - Production secrets management
7. **In-Memory Collections** - Testing or dynamic configuration

### Configuration Precedence (Later Sources Override Earlier Ones)

```
appsettings.json
    ↓
appsettings.{Environment}.json
    ↓
Environment Variables
    ↓
Command-line Arguments
    ↓
(Highest Priority)
```

**Example**: If `ConnectionString` is defined in `appsettings.json` and as an environment variable, the environment variable value wins.

### Built-in Configuration Setup

```csharp
var builder = WebApplication.CreateBuilder(args);

// ASP.NET Core automatically configures:
// 1. appsettings.json
// 2. appsettings.{Environment}.json
// 3. User Secrets (in Development)
// 4. Environment Variables
// 5. Command-line arguments

var app = builder.Build();
app.Run();
```

No manual configuration needed - it's built-in! 🎉

---

## IConfiguration Basics

`IConfiguration` is the service used to access configuration values directly.

### appsettings.json Example

```json
{
  "AppName": "MyWebApp",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MyDb;..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "FromEmail": "noreply@example.com",
    "ApiKey": "secret-key"
  }
}
```

### Accessing Configuration Values

```csharp
[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    
    public SettingsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet]
    public IActionResult GetSettings()
    {
        // Simple value
        var appName = _configuration["AppName"];
        
        // Nested value using colon separator
        var smtpServer = _configuration["EmailSettings:SmtpServer"];
        
        // Connection string (special section)
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        
        // With default value
        var timeout = _configuration.GetValue<int>("Timeout", 30);
        
        return Ok(new 
        { 
            appName, 
            smtpServer, 
            connectionString,
            timeout 
        });
    }
}
```

### Why Direct IConfiguration Access Is Not Always Ideal

❌ **Problems with Direct Access:**
- **No Type Safety**: Everything is a string, no compile-time checking
- **Magic Strings**: Configuration keys are error-prone (`"EmailSettings:SmtpServer"`)
- **No IntelliSense**: Easy to make typos
- **No Validation**: Invalid values discovered at runtime
- **Scattered Logic**: Configuration access spread throughout the codebase
- **Hard to Test**: Difficult to mock or replace

✅ **Solution**: Use the **Options Pattern** for strongly-typed configuration!

---

## What is the Options Pattern?

The **Options Pattern** uses classes to represent groups of related settings, providing strongly-typed access to configuration values. Instead of accessing configuration with string keys, you define a POCO (Plain Old CLR Object) class that maps to a configuration section, then inject it into services via dependency injection.

### Benefits

✅ **Type Safety**: Compile-time checking, no magic strings  
✅ **IntelliSense**: IDE support for configuration properties  
✅ **Validation**: Automatic validation using Data Annotations  
✅ **Testability**: Easy to mock and test  
✅ **Separation of Concerns**: Configuration logic in one place  
✅ **Refactoring Support**: Rename properties safely  

---

## Creating a Strongly-Typed Options Class

### Step 1: Define a POCO Class

```csharp
public class EmailSettings
{
    public string SmtpServer { get; set; } = string.Empty;
    public int Port { get; set; }
    public string FromEmail { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public bool EnableSsl { get; set; } = true;
    public int TimeoutSeconds { get; set; } = 30;
}
```

### Step 2: Map to Configuration Section in appsettings.json

```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "FromEmail": "noreply@myapp.com",
    "ApiKey": "secret-key-12345",
    "EnableSsl": true,
    "TimeoutSeconds": 30
  }
}
```

**Important**: The section name (`"EmailSettings"`) must match the class name (by convention) or be explicitly specified.

---

## Registering Options

Options are registered in `Program.cs` using the `Configure<T>()` method.

### Basic Registration

```csharp
var builder = WebApplication.CreateBuilder(args);

// Register EmailSettings as options
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

var app = builder.Build();
```

### Alternative: Using Bind()

```csharp
var emailSettings = new EmailSettings();
builder.Configuration.GetSection("EmailSettings").Bind(emailSettings);

// Now emailSettings is populated with values from appsettings.json
Console.WriteLine($"SMTP Server: {emailSettings.SmtpServer}");
```

**Note**: `Bind()` is useful for one-time setup, but `Configure<T>()` is preferred for dependency injection.

---

## Using Options in Services

Once registered, options can be injected into any service using `IOptions<T>`, `IOptionsSnapshot<T>`, or `IOptionsMonitor<T>`.

### Example: Email Service

```csharp
public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailService> _logger;
    
    // Inject IOptions<EmailSettings>
    public EmailService(
        IOptions<EmailSettings> options,
        ILogger<EmailService> logger)
    {
        _settings = options.Value; // Extract the strongly-typed settings
        _logger = logger;
    }
    
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        _logger.LogInformation(
            "Sending email to {To} via {SmtpServer}:{Port}", 
            to, 
            _settings.SmtpServer, 
            _settings.Port);
        
        // Use _settings.SmtpServer, _settings.Port, etc.
        // Send email logic here...
        
        await Task.Delay(100); // Simulate async operation
        
        _logger.LogInformation("Email sent successfully");
    }
}
```

### Registration in Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);

// Register options
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

// Register service
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();
```

### Using in Controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IEmailService _emailService;
    
    public NotificationController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
    {
        await _emailService.SendEmailAsync(
            request.To, 
            request.Subject, 
            request.Body);
        
        return Ok(new { message = "Email sent successfully" });
    }
}

public record EmailRequest(string To, string Subject, string Body);
```

---

## IOptions vs IOptionsSnapshot vs IOptionsMonitor (Very Important)

ASP.NET Core provides three interfaces for accessing options, each with different lifetime and reload behaviors.

### 1. IOptions&lt;T&gt;

**Lifetime**: **Singleton**

**Characteristics**:
- Registered as a singleton
- Configuration read **once** at startup
- **Does NOT** reload when configuration changes
- Same instance for the entire application lifetime

**Use When**: Configuration is static and won't change during application runtime

```csharp
public class MyService
{
    private readonly EmailSettings _settings;
    
    public MyService(IOptions<EmailSettings> options)
    {
        _settings = options.Value; // Read once
    }
}
```

**Registration**: Automatically registered as Singleton

---

### 2. IOptionsSnapshot&lt;T&gt;

**Lifetime**: **Scoped**

**Characteristics**:
- Registered as scoped (one per HTTP request)
- Configuration read **once per request**
- Reloads when configuration file changes **between requests**
- Different instances for different requests

**Use When**: You need configuration to reload without restarting the app (e.g., feature flags, dynamic settings)

```csharp
public class FeatureService
{
    private readonly IOptionsSnapshot<FeatureFlags> _featureFlags;
    
    public FeatureService(IOptionsSnapshot<FeatureFlags> featureFlags)
    {
        _featureFlags = featureFlags;
    }
    
    public bool IsFeatureEnabled(string featureName)
    {
        // Gets latest config for this request
        return _featureFlags.Value.IsEnabled(featureName);
    }
}
```

**Important**: Cannot be injected into singleton services (causes captive dependency)!

---

### 3. IOptionsMonitor&lt;T&gt;

**Lifetime**: **Singleton**

**Characteristics**:
- Registered as singleton
- Configuration reloads **immediately** when file changes
- Can register change callbacks
- Thread-safe for concurrent access

**Use When**: You need real-time configuration updates without restarting or waiting for next request

```csharp
public class CacheService
{
    private readonly IOptionsMonitor<CacheSettings> _cacheSettings;
    
    public CacheService(IOptionsMonitor<CacheSettings> cacheSettings)
    {
        _cacheSettings = cacheSettings;
        
        // Register callback for configuration changes
        _cacheSettings.OnChange(newSettings =>
        {
            Console.WriteLine($"Cache settings changed! New TTL: {newSettings.TimeToLive}");
            // React to configuration changes
        });
    }
    
    public void CacheItem(string key, object value)
    {
        var ttl = _cacheSettings.CurrentValue.TimeToLive; // Always latest
        // Cache logic...
    }
}
```

**Key Property**: Use `.CurrentValue` instead of `.Value` to always get the latest configuration

---

### Comparison Table

| Feature | IOptions&lt;T&gt; | IOptionsSnapshot&lt;T&gt; | IOptionsMonitor&lt;T&gt; |
|---------|-------------------|---------------------------|--------------------------|
| **Lifetime** | Singleton | Scoped (per request) | Singleton |
| **Reload Config** | ❌ No | ✅ Yes (between requests) | ✅ Yes (immediately) |
| **Use in Singleton** | ✅ Yes | ❌ No (captive dependency) | ✅ Yes |
| **Use in Scoped** | ✅ Yes | ✅ Yes | ✅ Yes |
| **Change Notifications** | ❌ No | ❌ No | ✅ Yes (OnChange) |
| **Access Property** | `.Value` | `.Value` | `.CurrentValue` |
| **Best For** | Static config | Per-request config | Real-time config updates |
| **Performance** | ⚡ Fastest | 🔄 Moderate | 🔄 Moderate |

---

### When to Use Which?

```csharp
// ✅ IOptions<T> - Static configuration (best performance)
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database"));

// ✅ IOptionsSnapshot<T> - Feature flags that can change between requests
builder.Services.Configure<FeatureFlags>(
    builder.Configuration.GetSection("Features"));

// ✅ IOptionsMonitor<T> - Real-time updates (e.g., logging levels, cache TTL)
builder.Services.Configure<LoggingSettings>(
    builder.Configuration.GetSection("Logging"));
```

---

## Options Validation

Validation ensures that configuration values are correct at startup or when reloaded, preventing runtime errors.

### Using Data Annotations

```csharp
using System.ComponentModel.DataAnnotations;

public class EmailSettings
{
    [Required]
    [EmailAddress]
    public string FromEmail { get; set; } = string.Empty;
    
    [Required]
    [MinLength(3)]
    public string SmtpServer { get; set; } = string.Empty;
    
    [Range(1, 65535)]
    public int Port { get; set; }
    
    [Required]
    [MinLength(10)]
    public string ApiKey { get; set; } = string.Empty;
    
    [Range(5, 300)]
    public int TimeoutSeconds { get; set; } = 30;
}
```

### Registering with Validation

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<EmailSettings>()
    .Bind(builder.Configuration.GetSection("EmailSettings"))
    .ValidateDataAnnotations()  // Enable validation
    .ValidateOnStart();          // Validate at startup (fail fast)

var app = builder.Build();
```

**What Happens?**
- If validation fails at startup → Application throws `OptionsValidationException`
- If validation fails during reload → Exception logged, old config retained

---

### Custom Validation

For complex validation logic, implement `IValidateOptions<T>`:

```csharp
public class EmailSettingsValidator : IValidateOptions<EmailSettings>
{
    public ValidateOptionsResult Validate(string? name, EmailSettings options)
    {
        var errors = new List<string>();
        
        if (options.Port <= 0 || options.Port > 65535)
        {
            errors.Add("Port must be between 1 and 65535");
        }
        
        if (string.IsNullOrEmpty(options.SmtpServer))
        {
            errors.Add("SMTP Server is required");
        }
        
        if (!options.FromEmail.Contains("@"))
        {
            errors.Add("FromEmail must be a valid email address");
        }
        
        if (options.TimeoutSeconds < 5)
        {
            errors.Add("TimeoutSeconds must be at least 5 seconds");
        }
        
        return errors.Any()
            ? ValidateOptionsResult.Fail(errors)
            : ValidateOptionsResult.Success;
    }
}

// Registration
builder.Services.AddSingleton<IValidateOptions<EmailSettings>, EmailSettingsValidator>();

builder.Services.AddOptions<EmailSettings>()
    .Bind(builder.Configuration.GetSection("EmailSettings"))
    .ValidateOnStart(); // Custom validator runs automatically
```

---

### Post-Configuration (Modify After Binding)

```csharp
builder.Services.PostConfigure<EmailSettings>(settings =>
{
    // Set defaults or transform values after binding
    if (string.IsNullOrEmpty(settings.FromEmail))
    {
        settings.FromEmail = "default@example.com";
    }
    
    // Convert relative to absolute path
    if (!string.IsNullOrEmpty(settings.LogPath) && !Path.IsPathRooted(settings.LogPath))
    {
        settings.LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, settings.LogPath);
    }
});
```

---

## Real-World Use Cases

### 1. External API Settings

```csharp
// appsettings.json
{
  "PaymentGateway": {
    "BaseUrl": "https://api.stripe.com",
    "ApiKey": "sk_test_12345",
    "WebhookSecret": "whsec_abcdef",
    "TimeoutSeconds": 30,
    "RetryCount": 3
  }
}

// Options class
public class PaymentGatewaySettings
{
    [Required, Url]
    public string BaseUrl { get; set; } = string.Empty;
    
    [Required, MinLength(20)]
    public string ApiKey { get; set; } = string.Empty;
    
    [Required]
    public string WebhookSecret { get; set; } = string.Empty;
    
    [Range(5, 60)]
    public int TimeoutSeconds { get; set; } = 30;
    
    [Range(1, 5)]
    public int RetryCount { get; set; } = 3;
}

// Service
public class PaymentService
{
    private readonly PaymentGatewaySettings _settings;
    private readonly HttpClient _httpClient;
    
    public PaymentService(
        IOptions<PaymentGatewaySettings> options,
        HttpClient httpClient)
    {
        _settings = options.Value;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_settings.TimeoutSeconds);
    }
}
```

---

### 2. Feature Flags

```csharp
// appsettings.json
{
  "Features": {
    "EnableNewDashboard": true,
    "EnableBetaFeatures": false,
    "MaxUploadSizeMB": 50,
    "AllowedFileTypes": ["jpg", "png", "pdf"]
  }
}

// Options class
public class FeatureFlags
{
    public bool EnableNewDashboard { get; set; }
    public bool EnableBetaFeatures { get; set; }
    
    [Range(1, 1000)]
    public int MaxUploadSizeMB { get; set; } = 50;
    
    public List<string> AllowedFileTypes { get; set; } = new();
    
    public bool IsFileTypeAllowed(string extension)
        => AllowedFileTypes.Contains(extension, StringComparer.OrdinalIgnoreCase);
}

// Service (use IOptionsSnapshot for runtime changes)
public class FeatureService
{
    private readonly IOptionsSnapshot<FeatureFlags> _features;
    
    public FeatureService(IOptionsSnapshot<FeatureFlags> features)
    {
        _features = features;
    }
    
    public bool CanAccessNewDashboard()
        => _features.Value.EnableNewDashboard;
    
    public bool CanUploadFile(string extension, long sizeInBytes)
    {
        var flags = _features.Value;
        var sizeMB = sizeInBytes / (1024.0 * 1024.0);
        
        return flags.IsFileTypeAllowed(extension) 
            && sizeMB <= flags.MaxUploadSizeMB;
    }
}
```

---

### 3. Connection Settings (Multiple Databases)

```csharp
// appsettings.json
{
  "ConnectionStrings": {
    "Primary": "Server=db1.example.com;Database=MainDb;...",
    "ReadReplica": "Server=db2.example.com;Database=MainDb;...",
    "Cache": "redis.example.com:6379"
  },
  "DatabaseSettings": {
    "CommandTimeout": 30,
    "EnableRetry": true,
    "MaxRetryCount": 3,
    "EnableLogging": true
  }
}

// Options class
public class DatabaseSettings
{
    public string Primary { get; set; } = string.Empty;
    public string ReadReplica { get; set; } = string.Empty;
    public string Cache { get; set; } = string.Empty;
    
    [Range(5, 300)]
    public int CommandTimeout { get; set; } = 30;
    
    public bool EnableRetry { get; set; } = true;
    
    [Range(1, 10)]
    public int MaxRetryCount { get; set; } = 3;
    
    public bool EnableLogging { get; set; }
}

// Registration
builder.Services.AddOptions<DatabaseSettings>()
    .Bind(builder.Configuration.GetSection("DatabaseSettings"))
    .Configure(settings =>
    {
        // Inject connection strings
        settings.Primary = builder.Configuration.GetConnectionString("Primary") ?? "";
        settings.ReadReplica = builder.Configuration.GetConnectionString("ReadReplica") ?? "";
        settings.Cache = builder.Configuration.GetConnectionString("Cache") ?? "";
    })
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

---

### 4. JWT Authentication Settings

```csharp
// appsettings.json
{
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-min-32-chars",
    "Issuer": "https://myapi.com",
    "Audience": "https://myapp.com",
    "ExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  }
}

// Options class
public class JwtSettings
{
    [Required, MinLength(32)]
    public string SecretKey { get; set; } = string.Empty;
    
    [Required, Url]
    public string Issuer { get; set; } = string.Empty;
    
    [Required, Url]
    public string Audience { get; set; } = string.Empty;
    
    [Range(5, 1440)]
    public int ExpirationMinutes { get; set; } = 60;
    
    [Range(1, 30)]
    public int RefreshTokenExpirationDays { get; set; } = 7;
}

// Registration in Program.cs
builder.Services.AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetSection("JwtSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

// JWT configuration using options
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration
            .GetSection("JwtSettings")
            .Get<JwtSettings>()!;
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });
```

---

## Common Mistakes

### ❌ Mistake 1: Using IConfiguration Everywhere

```csharp
// BAD: Direct IConfiguration access scattered throughout code
public class EmailService
{
    private readonly IConfiguration _config;
    
    public EmailService(IConfiguration config)
    {
        _config = config;
    }
    
    public void SendEmail()
    {
        var smtpServer = _config["EmailSettings:SmtpServer"]; // Magic strings!
        var port = _config.GetValue<int>("EmailSettings:Port");
        // ...
    }
}
```

**Problem**: No type safety, magic strings, hard to test

**Solution**: Use Options Pattern

```csharp
// GOOD: Strongly-typed options
public class EmailService
{
    private readonly EmailSettings _settings;
    
    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value; // Type-safe!
    }
    
    public void SendEmail()
    {
        var smtpServer = _settings.SmtpServer; // IntelliSense, refactoring support
        var port = _settings.Port;
        // ...
    }
}
```

---

### ❌ Mistake 2: Not Validating Options

```csharp
// BAD: No validation
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

// If Port is invalid (e.g., -1 or 999999), runtime error occurs later!
```

**Solution**: Always validate

```csharp
// GOOD: Validation at startup
builder.Services.AddOptions<EmailSettings>()
    .Bind(builder.Configuration.GetSection("EmailSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart(); // Fail fast!
```

---

### ❌ Mistake 3: Mixing Environments Incorrectly

```csharp
// BAD: Hard-coded environment-specific values
public class ApiService
{
    private const string ApiUrl = "https://api.production.com"; // Wrong!
}
```

**Problem**: Can't change per environment, requires code changes

**Solution**: Use environment-specific appsettings

```json
// appsettings.Development.json
{
  "ApiSettings": {
    "BaseUrl": "https://api.dev.com"
  }
}

// appsettings.Production.json
{
  "ApiSettings": {
    "BaseUrl": "https://api.production.com"
  }
}
```

---

### ❌ Mistake 4: Injecting IOptionsSnapshot into Singleton

```csharp
// BAD: Scoped service injected into Singleton
builder.Services.AddSingleton<ICacheManager, CacheManager>();

public class CacheManager : ICacheManager
{
    public CacheManager(IOptionsSnapshot<CacheSettings> settings) // ❌ Captive!
    {
        // IOptionsSnapshot is scoped, but CacheManager is singleton
    }
}
```

**Problem**: Captive dependency - IOptionsSnapshot captured at startup

**Solution**: Use IOptionsMonitor for singletons

```csharp
// GOOD: Use IOptionsMonitor in singleton
public class CacheManager : ICacheManager
{
    private readonly IOptionsMonitor<CacheSettings> _settings;
    
    public CacheManager(IOptionsMonitor<CacheSettings> settings)
    {
        _settings = settings; // ✅ Correct!
    }
    
    public void Cache(string key, object value)
    {
        var ttl = _settings.CurrentValue.TimeToLive; // Always current
    }
}
```

---

### ❌ Mistake 5: Storing Secrets in appsettings.json

```csharp
// BAD: Secrets in appsettings.json (committed to source control!)
{
  "EmailSettings": {
    "ApiKey": "secret-key-12345" // ❌ Security risk!
  }
}
```

**Solution**: Use User Secrets (dev) and Environment Variables (prod)

```bash
# Development: User Secrets
dotnet user-secrets init
dotnet user-secrets set "EmailSettings:ApiKey" "dev-secret-key"

# Production: Environment Variables
export EmailSettings__ApiKey="prod-secret-key"
```

**Note**: Use double underscore `__` for nested keys in environment variables!

---

## Common Interview Questions

### Q1: What is the Options Pattern in ASP.NET Core?

**Answer**: The Options Pattern is a design pattern that uses strongly-typed classes (POCOs) to represent groups of related configuration settings. Instead of accessing configuration values using string keys (`_config["EmailSettings:SmtpServer"]`), you define a class that maps to a configuration section and inject it via dependency injection using `IOptions<T>`, `IOptionsSnapshot<T>`, or `IOptionsMonitor<T>`. This provides type safety, IntelliSense support, validation, and better testability.

---

### Q2: What's the difference between IOptions, IOptionsSnapshot, and IOptionsMonitor?

**Answer**:
- **IOptions&lt;T&gt;**: Singleton lifetime, configuration read once at startup, does not reload. Best for static configuration.
- **IOptionsSnapshot&lt;T&gt;**: Scoped lifetime (per request), reloads between requests when configuration changes. Cannot be injected into singletons (causes captive dependency).
- **IOptionsMonitor&lt;T&gt;**: Singleton lifetime, reloads immediately when configuration changes, supports change notifications via `OnChange()`. Use `.CurrentValue` to get latest config.

**Quick Rule**: Use IOptions for static config, IOptionsSnapshot for per-request config that reloads, IOptionsMonitor for real-time updates in singletons.

---

### Q3: When would you use IOptionsMonitor instead of IOptions?

**Answer**: Use `IOptionsMonitor<T>` when:
1. Configuration needs to reload without restarting the application (e.g., logging levels, feature flags, cache TTL)
2. The service is registered as a Singleton (IOptionsSnapshot can't be used in singletons)
3. You need to react to configuration changes using the `OnChange()` callback

Example: A singleton cache service that needs to adjust TTL when appsettings.json is updated without restarting the app.

---

### Q4: How do you validate options in ASP.NET Core?

**Answer**: You can validate options using:
1. **Data Annotations**: Add attributes like `[Required]`, `[Range]`, `[EmailAddress]` to the options class
2. **Call ValidateDataAnnotations()**: Enable validation during registration
3. **Call ValidateOnStart()**: Validate at startup to fail fast

```csharp
builder.Services.AddOptions<EmailSettings>()
    .Bind(builder.Configuration.GetSection("EmailSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

For complex validation, implement `IValidateOptions<T>` with custom logic.

---

### Q5: What happens if you inject IOptionsSnapshot into a Singleton service?

**Answer**: This creates a **captive dependency** problem. Since the singleton is created once and held for the application lifetime, the `IOptionsSnapshot<T>` instance becomes "captive" and won't reload properly. ASP.NET Core may throw an exception or log a warning about this incorrect lifetime configuration.

**Solution**: Use `IOptionsMonitor<T>` instead, which is safe for singletons and supports real-time reloading.

---

### Q6: How do you handle environment-specific configuration?

**Answer**: ASP.NET Core automatically loads environment-specific configuration files based on the `ASPNETCORE_ENVIRONMENT` variable:
- `appsettings.json` (base configuration)
- `appsettings.{Environment}.json` (overrides base)

Example:
- Development: `appsettings.Development.json` loaded automatically
- Production: `appsettings.Production.json` loaded automatically

Later sources override earlier ones, so production settings in `appsettings.Production.json` will override base settings in `appsettings.json`. You can also use environment variables and command-line arguments for deployment-specific overrides.

---

### Q7: Why is the Options Pattern better than using IConfiguration directly?

**Answer**:
- **Type Safety**: Compile-time checking vs runtime string parsing
- **IntelliSense**: IDE support for properties
- **No Magic Strings**: Avoid typos like `"EmailSetings:SmtpServer"`
- **Validation**: Automatic validation with Data Annotations
- **Testability**: Easy to mock `IOptions<T>` vs mocking IConfiguration with nested keys
- **Refactoring**: Rename properties safely, refactoring tools work correctly
- **Separation of Concerns**: Configuration logic centralized in one place

---

### Q8: Can you bind configuration to a List or Dictionary?

**Answer**: Yes! ASP.NET Core configuration system supports complex types:

```json
{
  "AllowedHosts": ["example.com", "*.myapp.com"],
  "ConnectionStrings": {
    "Primary": "Server=...",
    "Secondary": "Server=..."
  }
}
```

```csharp
public class AppSettings
{
    public List<string> AllowedHosts { get; set; } = new();
    public Dictionary<string, string> ConnectionStrings { get; set; } = new();
}

builder.Services.Configure<AppSettings>(
    builder.Configuration);
```

Arrays use indexed keys: `AllowedHosts:0`, `AllowedHosts:1`, etc.

---

### Q9: How do you access configuration values at startup (before DI is available)?

**Answer**: Before the DI container is built, you can use `builder.Configuration` directly:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Access configuration before building DI
var enableSwagger = builder.Configuration.GetValue<bool>("EnableSwagger", true);

if (enableSwagger)
{
    builder.Services.AddSwaggerGen();
}

// Or bind to a class
var dbSettings = new DatabaseSettings();
builder.Configuration.GetSection("DatabaseSettings").Bind(dbSettings);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(dbSettings.ConnectionString));

var app = builder.Build();
```

After `builder.Build()`, use DI to inject options instead.

---

### Q10: What is the difference between Configure and PostConfigure?

**Answer**:
- **Configure&lt;T&gt;**: Binds configuration section to options class during registration. Runs first.
- **PostConfigure&lt;T&gt;**: Runs after all `Configure<T>` calls, allowing you to modify or set default values.

```csharp
// Configure - bind from appsettings.json
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

// PostConfigure - set defaults or transform values
builder.Services.PostConfigure<EmailSettings>(settings =>
{
    if (string.IsNullOrEmpty(settings.FromEmail))
    {
        settings.FromEmail = "default@example.com";
    }
});
```

Use `PostConfigure` for computed values, defaults, or transformations that apply after binding.

---

## Summary

✅ **Key Takeaways:**

- **ASP.NET Core configuration system** supports multiple providers (appsettings.json, environment variables, command-line args) with a clear precedence order where later sources override earlier ones

- **Options Pattern** provides strongly-typed, validated, testable configuration access through POCO classes, eliminating magic strings and enabling compile-time safety with IntelliSense support

- **Three option interfaces**: **IOptions&lt;T&gt;** (singleton, static config), **IOptionsSnapshot&lt;T&gt;** (scoped, reloads per request), **IOptionsMonitor&lt;T&gt;** (singleton with real-time reload and change notifications)

- **Always validate options** using Data Annotations (`ValidateDataAnnotations()`) and `ValidateOnStart()` to fail fast and catch configuration errors at startup rather than runtime

- **Avoid common mistakes**: Don't use IConfiguration everywhere (use Options Pattern), never inject IOptionsSnapshot into singletons (use IOptionsMonitor), validate all options, use environment-specific files, and store secrets securely (User Secrets for dev, Environment Variables for prod)

- **Real-world usage**: Options Pattern is essential for external API settings, feature flags, connection strings, JWT authentication, caching configuration, and any setting that varies by environment

- **In interviews**, be ready to explain the three option interfaces, their lifetimes and use cases, why Options Pattern is preferred over IConfiguration, how to validate options, and how to handle environment-specific configuration

---

**Master Configuration and the Options Pattern to build flexible, maintainable, and production-ready ASP.NET Core applications!** 🚀
