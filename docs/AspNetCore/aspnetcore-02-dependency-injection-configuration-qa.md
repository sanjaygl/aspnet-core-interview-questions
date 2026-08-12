# ASP.NET Core — Dependency Injection & Configuration — Interview Q&A

---

### Q1. What are the three built-in service lifetimes, and what does "scoped" actually mean for a web request?

**Answer:**
"Transient creates a brand new instance every single time it's requested/injected — even twice within the same class construction. Scoped creates one instance per *request* (technically per `IServiceScope`, and ASP.NET Core creates one scope per incoming HTTP request automatically) — every service resolved within that same request shares the same scoped instance. Singleton creates exactly one instance for the entire lifetime of the application, shared across every request and every user."

```csharp
builder.Services.AddTransient<IEmailSender, EmailSender>();   // new instance every injection
builder.Services.AddScoped<AppDbContext>();                     // one per HTTP request
builder.Services.AddSingleton<ICacheService, MemoryCacheService>(); // one for the whole app's lifetime
```

---

### Q2. What is a "captive dependency," and why is injecting a Scoped service into a Singleton a real production bug?

**Answer:**
"A captive dependency happens when a longer-lived service holds a reference to a shorter-lived one — specifically, a Singleton capturing a Scoped service in its constructor. Since the Singleton is only ever constructed once, whatever Scoped instance gets injected at that moment gets 'captured' and reused for the Singleton's entire lifetime — effectively making that Scoped service behave like a Singleton too, but silently, for just this one consumer. If that captured service is something like a `DbContext` (designed to be short-lived, one per request), you end up with a single, long-lived `DbContext` instance shared across every request forever — which is a serious bug: `DbContext` isn't thread-safe, its change tracker accumulates entities from every request indefinitely, and it never gets disposed/recreated the way it's supposed to."

**Cross-question: Does ASP.NET Core detect and prevent this at startup, or does it fail silently/subtly at runtime?**
"By default in earlier versions it failed silently at runtime, which made it a genuinely dangerous, hard-to-spot bug. Since .NET Core 3.0, the built-in DI container performs *scope validation* automatically in the Development environment (via `ValidateScopes = true`, on by default when using `CreateDefaultBuilder`/`WebApplication.CreateBuilder`) — it throws an `InvalidOperationException` at the moment of the problematic resolution, catching the mistake early, in dev, before it ever reaches production. It's still worth knowing this validation is Development-environment-specific by default, so don't assume production will catch it the same way unless explicitly configured to validate scopes there too."

```csharp
// Throws in Development (scope validation) - Singleton capturing a Scoped dependency
public class BadSingleton
{
    public BadSingleton(AppDbContext dbContext) { } // AppDbContext is Scoped - this is the bug
}
builder.Services.AddSingleton<BadSingleton>();
```

---

### Q3. How would you resolve a Scoped service from within a Singleton or a background service when you genuinely need to?

**Answer:**
"Inject `IServiceScopeFactory` (or `IServiceProvider` and call `.CreateScope()`) into the Singleton, and create a *new* scope explicitly whenever you need to do work — resolving the Scoped service from that fresh scope, using it, and disposing the scope afterward (via `using`). This is exactly the pattern `BackgroundService` implementations need for anything touching a `DbContext`, since a `BackgroundService` itself runs in a singleton-like context for the app's lifetime."

```csharp
public class MyBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    public MyBackgroundService(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope(); // fresh scope, fresh Scoped instances
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // ... use dbContext, then the scope (and dbContext) is disposed at the end of this iteration
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
```

---

### Q4. What is the `IOptions<T>` pattern, and what's the difference between `IOptions<T>`, `IOptionsSnapshot<T>`, and `IOptionsMonitor<T>`?

**Answer:**
"The Options pattern binds a section of configuration to a strongly-typed POCO class, injected wherever needed instead of reading raw configuration strings/keys everywhere. `IOptions<T>` is a Singleton — it reads the configuration once and never reflects changes afterward, even if the underlying config file changes at runtime. `IOptionsSnapshot<T>` is Scoped — it re-reads current configuration once per request/scope, so it picks up changes between requests, but stays consistent within a single request. `IOptionsMonitor<T>` is a Singleton but supports live-reloading and a change-notification callback (`OnChange`), letting you react immediately when configuration changes, even mid-request."

```csharp
public class SmtpSettings { public string Host { get; set; } = ""; public int Port { get; set; } }
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

// IOptions<T> - read once, Singleton lifetime, never reflects later changes
public class EmailSender(IOptions<SmtpSettings> options) { /* options.Value.Host */ }

// IOptionsMonitor<T> - live-reloading, can react to changes
public class EmailSender2(IOptionsMonitor<SmtpSettings> monitor)
{
    public EmailSender2(IOptionsMonitor<SmtpSettings> monitor)
    {
        monitor.OnChange(newSettings => Console.WriteLine("Smtp settings changed!"));
    }
}
```

**Where to use:** `IOptions<T>` for config that genuinely never needs to change without an app restart; `IOptionsSnapshot<T>` for per-request consistency with occasional config file changes; `IOptionsMonitor<T>` when you need to actively react to a config change while the app keeps running.

---

### Q5. How does the configuration provider hierarchy work, and which wins on conflict?

**Answer:**
"Configuration providers are applied in the order they're added, and later providers override earlier ones for the same key. The conventional default order is: `appsettings.json` (base) → `appsettings.{Environment}.json` (environment-specific overrides) → User Secrets (Development only) → environment variables → command-line arguments. So an environment variable always wins over a value in `appsettings.json` for the same key, and command-line arguments win over everything — which is exactly what makes it possible to override a setting for a specific deployment without ever touching the checked-in config files."

```csharp
var builder = WebApplication.CreateBuilder(args);
// Behind the scenes, roughly: appsettings.json -> appsettings.{env}.json -> env vars -> args
// A value set via an environment variable overrides the same key from appsettings.json
```

---

### Q6. How would you validate configuration/options at startup so the app fails fast?

**Answer:**
"Use `.ValidateDataAnnotations()` (with attributes like `[Required]` on the options class) or `.Validate(...)` with a custom predicate, combined with `.ValidateOnStart()` — this forces the options to actually be built and validated during application startup, rather than lazily the first time something injects and reads them. Without `ValidateOnStart()`, a misconfigured options class might not throw until the first request that happens to touch it, potentially well after deployment — failing at startup instead makes a bad config immediately, loudly obvious in the deployment logs."

```csharp
public class SmtpSettings
{
    [Required] public string Host { get; set; } = "";
    [Range(1, 65535)] public int Port { get; set; }
}

builder.Services.AddOptions<SmtpSettings>()
    .Bind(builder.Configuration.GetSection("Smtp"))
    .ValidateDataAnnotations()
    .ValidateOnStart(); // fails immediately at app startup if Smtp:Host is missing, not on first use
```

---

### Q7. What's the difference between registering a service with `AddScoped<IFoo, Foo>()` vs `AddScoped<Foo>()`?

**Answer:**
"`AddScoped<IFoo, Foo>()` registers `Foo` as the implementation for the `IFoo` abstraction — consumers inject `IFoo` and get a `Foo` instance, without depending on the concrete type at all (the standard pattern for testability and following the dependency inversion principle). `AddScoped<Foo>()` (no interface) registers the concrete class directly against itself — consumers have to inject `Foo` specifically, coupling them to the concrete implementation. It matters because injecting the concrete type makes it harder to swap implementations later (e.g., for testing with a mock) — prefer registering against an interface unless the service genuinely has no reason to ever have more than one implementation (e.g., a simple internal helper with no need for abstraction)."

```csharp
builder.Services.AddScoped<IOrderService, OrderService>(); // consumers depend on IOrderService - swappable, mockable
builder.Services.AddScoped<OrderService>();                 // consumers depend on the concrete OrderService directly
```
