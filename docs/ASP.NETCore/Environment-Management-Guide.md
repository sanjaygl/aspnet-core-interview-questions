# Environment Management in ASP.NET Core – Development, Staging & Production

## Introduction

Environment management is crucial for building robust, secure applications that behave differently based on where they're running. Running development settings in production can expose sensitive data, enable verbose logging that degrades performance, or leak stack traces to end users. ASP.NET Core provides a flexible environment management system that allows you to configure behavior, logging, middleware, and features differently across Development, Staging, and Production environments. Understanding environment management is essential for production deployments and is frequently discussed in technical interviews.

---

## What is an Environment in ASP.NET Core?

An **environment** in ASP.NET Core represents the runtime context where your application is deployed. It determines application behavior, configuration settings, logging levels, error handling, and feature availability.

### Common Environments

#### 1. Development

**Purpose**: Local development and debugging  
**Characteristics**:
- Detailed error pages with stack traces
- Verbose logging (Debug/Trace levels)
- No caching, hot reload enabled
- Swagger/OpenAPI enabled
- Less secure settings for convenience

**Use When**: Running on developer machines, Visual Studio, VS Code

---

#### 2. Staging

**Purpose**: Pre-production testing environment that mimics production  
**Characteristics**:
- Production-like configuration
- Moderate logging (Information/Warning)
- Test data, not real user data
- QA testing, integration testing
- Performance testing

**Use When**: Testing before production deployment, UAT (User Acceptance Testing)

---

#### 3. Production

**Purpose**: Live application serving real users  
**Characteristics**:
- Minimal error details (user-friendly messages only)
- Minimal logging (Warning/Error/Critical)
- Aggressive caching, performance optimizations
- Security hardened (HSTS, HTTPS, CORS restrictions)
- No Swagger/debugging tools exposed

**Use When**: Deployed to live servers, cloud services, containers

---

### Custom Environments

You can define custom environments like `QA`, `UAT`, `PreProduction`, or `Demo`:

```bash
# Set custom environment
set ASPNETCORE_ENVIRONMENT=QA
```

ASP.NET Core treats any environment name as valid, allowing flexible deployment strategies.

---

## ASPNETCORE_ENVIRONMENT Variable

The `ASPNETCORE_ENVIRONMENT` environment variable tells ASP.NET Core which environment the application is running in.

### What It Is

- **System environment variable** read at application startup
- **Case-insensitive** (Development = development = DEVELOPMENT)
- **Controls behavior** throughout the application lifecycle
- **Overrideable** via command-line, Docker, Kubernetes, Azure App Service, etc.

---

### How ASP.NET Core Uses It

1. **Configuration Loading**: Determines which `appsettings.{Environment}.json` to load
2. **IWebHostEnvironment**: Populates `EnvironmentName` property
3. **Conditional Logic**: Enables environment-specific middleware, logging, features

```csharp
var builder = WebApplication.CreateBuilder(args);

// ASP.NET Core automatically reads ASPNETCORE_ENVIRONMENT
Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");

var app = builder.Build();
```

---

### Default Value Behavior

- **Default**: If `ASPNETCORE_ENVIRONMENT` is not set, defaults to **Production**
- **Visual Studio**: Automatically sets to `Development` via `launchSettings.json`
- **Production Servers**: Typically not set (defaults to Production) or explicitly set to `Production`

**⚠️ Important**: Always verify environment settings in production deployments!

---

### Setting ASPNETCORE_ENVIRONMENT

#### Windows (PowerShell)
```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
```

#### Windows (Command Prompt)
```cmd
set ASPNETCORE_ENVIRONMENT=Development
```

#### Linux/macOS (Bash)
```bash
export ASPNETCORE_ENVIRONMENT=Development
```

#### Docker
```dockerfile
ENV ASPNETCORE_ENVIRONMENT=Production
```

#### Kubernetes
```yaml
env:
  - name: ASPNETCORE_ENVIRONMENT
    value: "Production"
```

#### Azure App Service
Set in Configuration → Application Settings → `ASPNETCORE_ENVIRONMENT`

#### launchSettings.json (Visual Studio)
```json
{
  "profiles": {
    "MyApp": {
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

---

## Environment-Specific Configuration

ASP.NET Core uses a hierarchical configuration system where environment-specific files override base settings.

### Configuration Files

```
appsettings.json                      ← Base configuration (all environments)
appsettings.Development.json          ← Development overrides
appsettings.Staging.json              ← Staging overrides
appsettings.Production.json           ← Production overrides
```

### Configuration Override Order (Later Wins)

```
1. appsettings.json
2. appsettings.{Environment}.json
3. User Secrets (Development only)
4. Environment Variables
5. Command-line Arguments
```

---

### Example Configuration

#### appsettings.json (Base)
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MyApp;..."
  },
  "AppSettings": {
    "EnableCaching": true,
    "CacheExpirationMinutes": 60,
    "MaxUploadSizeMB": 10
  }
}
```

---

#### appsettings.Development.json (Development Overrides)
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MyApp_Dev;..."
  },
  "AppSettings": {
    "EnableCaching": false,
    "MaxUploadSizeMB": 100
  }
}
```

**Key Differences**:
- Logging: Debug level (verbose)
- Database: Local development database
- Caching: Disabled for easier debugging
- Upload limit: Higher for testing

---

#### appsettings.Production.json (Production Overrides)
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft": "Error",
      "MyApp": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-db.example.com;Database=MyApp_Prod;..."
  },
  "AppSettings": {
    "EnableCaching": true,
    "CacheExpirationMinutes": 120,
    "MaxUploadSizeMB": 5
  }
}
```

**Key Differences**:
- Logging: Warning level (minimal logs)
- Database: Production database server
- Caching: Enabled with longer expiration
- Upload limit: Restricted for security

---

### How Configuration Merging Works

```csharp
// If ASPNETCORE_ENVIRONMENT = Development

// appsettings.json:                  "EnableCaching": true
// appsettings.Development.json:      "EnableCaching": false
// Result:                            "EnableCaching": false ✅ (Development wins)

// appsettings.json:                  "CacheExpirationMinutes": 60
// appsettings.Development.json:      (not specified)
// Result:                            "CacheExpirationMinutes": 60 ✅ (Base used)
```

---

## IWebHostEnvironment

`IWebHostEnvironment` is the service that provides information about the current hosting environment.

### Common Properties

```csharp
public interface IWebHostEnvironment
{
    string EnvironmentName { get; set; }        // "Development", "Production", etc.
    string ApplicationName { get; set; }        // Assembly name
    string ContentRootPath { get; set; }        // Application root directory
    string WebRootPath { get; set; }            // wwwroot directory
    IFileProvider ContentRootFileProvider { get; set; }
    IFileProvider WebRootFileProvider { get; set; }
}
```

---

### Extension Methods

```csharp
// Check environment
bool IsDevelopment()
bool IsStaging()
bool IsProduction()
bool IsEnvironment(string environmentName)
```

---

### Example Usage

```csharp
var builder = WebApplication.CreateBuilder(args);

// Access environment during startup
if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("Running in Development mode");
    builder.Services.AddSwaggerGen();
}
else if (builder.Environment.IsProduction())
{
    Console.WriteLine("Running in Production mode");
    // Production-specific services
}

var app = builder.Build();

// Use in middleware configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.Run();
```

---

### Using IWebHostEnvironment in Services

```csharp
public class FileStorageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<FileStorageService> _logger;
    
    public FileStorageService(IWebHostEnvironment environment, ILogger<FileStorageService> logger)
    {
        _environment = environment;
        _logger = logger;
    }
    
    public string GetStoragePath()
    {
        if (_environment.IsDevelopment())
        {
            // Local file system in development
            return Path.Combine(_environment.ContentRootPath, "uploads");
        }
        else
        {
            // Azure Blob Storage or S3 in production
            return "https://myapp-storage.blob.core.windows.net/uploads";
        }
    }
    
    public async Task<string> SaveFileAsync(IFormFile file)
    {
        var storagePath = GetStoragePath();
        
        _logger.LogInformation(
            "Saving file to {StoragePath} in {Environment} environment",
            storagePath,
            _environment.EnvironmentName);
        
        // Save file logic...
        return storagePath;
    }
}
```

---

## Environment-Based Middleware

Middleware can be registered conditionally based on the environment.

### Complete Example

```csharp
var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger only in Development
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// ===== DEVELOPMENT MIDDLEWARE =====
if (app.Environment.IsDevelopment())
{
    // Detailed error pages with stack traces
    app.UseDeveloperExceptionPage();
    
    // Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
    
    // CORS - Allow all origins in development
    app.UseCors(policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
}
// ===== PRODUCTION MIDDLEWARE =====
else
{
    // User-friendly error page (no stack traces)
    app.UseExceptionHandler("/error");
    
    // HTTP Strict Transport Security (HTTPS enforcement)
    app.UseHsts();
    
    // CORS - Restricted origins
    app.UseCors(policy => policy
        .WithOrigins("https://myapp.com", "https://www.myapp.com")
        .AllowAnyMethod()
        .AllowAnyHeader());
}

// Common middleware (all environments)
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

---

### Developer Exception Page (Development Only)

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
```

**Development Output**: Detailed stack trace, request details, exception type  
**Why Not Production?**: Exposes internal implementation details, security risk

---

### HSTS (Production Only)

```csharp
if (!app.Environment.IsDevelopment())
{
    app.UseHsts(); // HTTP Strict Transport Security
}
```

**What It Does**: Forces browsers to use HTTPS for future requests  
**Why Not Development?**: HTTPS not always configured locally, can cause issues

---

### Custom Middleware by Environment

```csharp
public class PerformanceLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceLoggingMiddleware> _logger;
    
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();
        
        if (stopwatch.ElapsedMilliseconds > 1000)
        {
            _logger.LogWarning(
                "Slow request: {Method} {Path} took {ElapsedMs}ms",
                context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds);
        }
    }
}

// Register only in non-production environments
if (!app.Environment.IsProduction())
{
    app.UseMiddleware<PerformanceLoggingMiddleware>();
}
```

---

## Environment-Based Logging

Different environments require different logging strategies.

### Development Logging

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

**Characteristics**:
- **Debug/Trace** logs enabled
- SQL queries logged
- Request/response details
- Console logging

---

### Production Logging

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft": "Error",
      "MyApp": "Information"
    }
  }
}
```

**Characteristics**:
- **Warning/Error/Critical** only
- No SQL query logging
- Minimal request details
- Centralized logging (Application Insights, Serilog, ELK)

---

### Programmatic Logging Configuration

```csharp
var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
}
else if (builder.Environment.IsProduction())
{
    builder.Logging.SetMinimumLevel(LogLevel.Warning);
    // Add Application Insights, Serilog, etc.
    builder.Logging.AddApplicationInsights();
}

var app = builder.Build();
```

---

### Avoiding Verbose Logs in Production

```csharp
// ❌ Bad: Verbose logging in production
_logger.LogDebug("Processing item {ItemId} with details {Details}", itemId, details);

// ✅ Good: Check environment or use appropriate log level
if (_environment.IsDevelopment())
{
    _logger.LogDebug("Processing item {ItemId} with details {Details}", itemId, details);
}

// ✅ Better: Use correct log level
_logger.LogInformation("Processing item {ItemId}", itemId); // Information in production
```

---

## Environment-Based Feature Control

### Feature Toggles

```csharp
public class FeatureSettings
{
    public bool EnableNewDashboard { get; set; }
    public bool EnableBetaFeatures { get; set; }
    public bool EnableDetailedLogging { get; set; }
}

// appsettings.Development.json
{
  "Features": {
    "EnableNewDashboard": true,
    "EnableBetaFeatures": true,
    "EnableDetailedLogging": true
  }
}

// appsettings.Production.json
{
  "Features": {
    "EnableNewDashboard": false,
    "EnableBetaFeatures": false,
    "EnableDetailedLogging": false
  }
}

// Usage
builder.Services.Configure<FeatureSettings>(
    builder.Configuration.GetSection("Features"));

public class DashboardController : ControllerBase
{
    private readonly FeatureSettings _features;
    
    public DashboardController(IOptions<FeatureSettings> features)
    {
        _features = features.Value;
    }
    
    [HttpGet]
    public IActionResult GetDashboard()
    {
        if (_features.EnableNewDashboard)
        {
            return Ok(new { version = "v2", features = "new" });
        }
        
        return Ok(new { version = "v1", features = "stable" });
    }
}
```

---

### Enabling/Disabling Swagger

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// ✅ Good: Swagger only in Development
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
```

**Why?**  
- **Development**: Swagger helps with API testing and documentation
- **Production**: Swagger exposes API structure, security risk if publicly accessible

**Alternative for Production**: Deploy Swagger to internal/admin subdomain with authentication

---

### Database Seeding (Development Only)

```csharp
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        // Seed database with test data
        if (!dbContext.Users.Any())
        {
            dbContext.Users.AddRange(
                new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
                new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" }
            );
            
            await dbContext.SaveChangesAsync();
        }
    }
}
```

---

## Real-World Deployment Scenarios

### 1. Local Development

**Environment**: Development  
**Configuration**:
```json
// launchSettings.json
{
  "profiles": {
    "MyApp": {
      "commandName": "Project",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "https://localhost:5001;http://localhost:5000"
    }
  }
}
```

**Characteristics**:
- Hot reload enabled
- Detailed error pages
- Swagger UI at `/swagger`
- Local database (SQL Server LocalDB, SQLite)
- No HTTPS enforcement

---

### 2. CI/CD Pipelines (Azure DevOps, GitHub Actions)

#### Build Stage
```yaml
# azure-pipelines.yml
- task: DotNetCoreCLI@2
  inputs:
    command: 'publish'
    publishWebProjects: true
    arguments: '--configuration Release --output $(Build.ArtifactStagingDirectory)'
```

#### Release Stages

**Staging Deployment**:
```yaml
- task: AzureWebApp@1
  inputs:
    appName: 'myapp-staging'
    appSettings: |
      -ASPNETCORE_ENVIRONMENT Staging
```

**Production Deployment**:
```yaml
- task: AzureWebApp@1
  inputs:
    appName: 'myapp-production'
    appSettings: |
      -ASPNETCORE_ENVIRONMENT Production
```

---

### 3. Docker Deployment

```dockerfile
# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyApp/MyApp.csproj", "MyApp/"]
RUN dotnet restore "MyApp/MyApp.csproj"
COPY . .
WORKDIR "/src/MyApp"
RUN dotnet build "MyApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "MyApp.dll"]
```

**Run with Different Environment**:
```bash
# Development
docker run -e ASPNETCORE_ENVIRONMENT=Development myapp:latest

# Production (default)
docker run myapp:latest
```

---

### 4. Kubernetes Deployment

```yaml
# deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: myapp-production
spec:
  replicas: 3
  selector:
    matchLabels:
      app: myapp
  template:
    metadata:
      labels:
        app: myapp
    spec:
      containers:
      - name: myapp
        image: myregistry.azurecr.io/myapp:latest
        ports:
        - containerPort: 80
        env:
        - name: ASPNETCORE_ENVIRONMENT
          value: "Production"
        - name: ConnectionStrings__DefaultConnection
          valueFrom:
            secretKeyRef:
              name: db-connection
              key: connectionString
```

---

### 5. Azure App Service

**Configuration via Portal**:
1. Navigate to App Service → Configuration → Application Settings
2. Add new setting: `ASPNETCORE_ENVIRONMENT` = `Production`
3. Save and restart

**Configuration via Azure CLI**:
```bash
az webapp config appsettings set \
  --resource-group myResourceGroup \
  --name myapp \
  --settings ASPNETCORE_ENVIRONMENT=Production
```

---

## Common Mistakes

### ❌ Mistake 1: Running Production with Development Settings

```bash
# BAD: Production server with Development environment
ASPNETCORE_ENVIRONMENT=Development dotnet MyApp.dll
```

**Problems**:
- Stack traces exposed to users
- Verbose logging degrades performance
- Swagger/debugging endpoints publicly accessible
- Security vulnerabilities

**Solution**: Always set `ASPNETCORE_ENVIRONMENT=Production` in production

---

### ❌ Mistake 2: Hardcoding Environment Checks

```csharp
// BAD: Hardcoded strings
if (app.Environment.EnvironmentName == "Development") // ❌ Typo risk
{
    app.UseSwagger();
}

// GOOD: Use extension methods
if (app.Environment.IsDevelopment()) // ✅ Type-safe
{
    app.UseSwagger();
}
```

**Why?** Extension methods are type-safe, prevent typos, and handle case sensitivity.

---

### ❌ Mistake 3: Exposing Swagger Publicly in Production

```csharp
// BAD: Swagger always enabled
builder.Services.AddSwaggerGen();
app.UseSwagger();
app.UseSwaggerUI();

// Accessible at https://api.myapp.com/swagger ❌ Security risk!
```

**Solution**: Enable Swagger only in Development or secure it with authentication

```csharp
// GOOD: Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ALTERNATIVE: Secure Swagger in Production
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "My API (Admin Only)";
    });
    
    app.MapSwagger().RequireAuthorization(); // Require authentication
}
```

---

### ❌ Mistake 4: Not Validating Environment Settings

```csharp
// BAD: No validation
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
```

**Problem**: If connection string missing in Production, application crashes at runtime

**Solution**: Validate critical settings at startup

```csharp
// GOOD: Validate at startup
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException(
        "Connection string 'DefaultConnection' is not configured. " +
        $"Environment: {builder.Environment.EnvironmentName}");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
```

---

### ❌ Mistake 5: Committing Environment-Specific Secrets

```json
// BAD: Production secrets in appsettings.Production.json (committed to Git!)
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-db;User=sa;Password=SuperSecret123;"
  }
}
```

**Solution**: Use User Secrets (dev) and Environment Variables (prod)

```bash
# Production: Environment variables
export ConnectionStrings__DefaultConnection="Server=prod-db;User=sa;Password=FromEnvVar;"

# Or Azure Key Vault, AWS Secrets Manager, etc.
```

---

## Common Interview Questions

### Q1: What is ASPNETCORE_ENVIRONMENT and how does it work?

**Answer**: `ASPNETCORE_ENVIRONMENT` is a system environment variable that tells ASP.NET Core which runtime environment the application is running in (Development, Staging, Production, or custom). ASP.NET Core reads this variable at startup to:
1. Load environment-specific configuration files (`appsettings.{Environment}.json`)
2. Populate the `IWebHostEnvironment.EnvironmentName` property
3. Enable environment-specific middleware, logging, and features

If not set, it defaults to **Production**. Visual Studio automatically sets it to `Development` via `launchSettings.json`. This variable is critical for controlling application behavior across different deployment stages.

---

### Q2: How do you manage environment-specific settings in ASP.NET Core?

**Answer**: Use environment-specific `appsettings.json` files:
- `appsettings.json` - Base configuration for all environments
- `appsettings.Development.json` - Development overrides
- `appsettings.Production.json` - Production overrides

Configuration loading follows this order (later wins):
1. appsettings.json
2. appsettings.{Environment}.json
3. User Secrets (Development only)
4. Environment Variables
5. Command-line Arguments

This allows you to have verbose logging and local databases in Development, while using minimal logging and production databases in Production without code changes.

---

### Q3: What are the key differences between Development and Production modes?

**Answer**:

| Aspect | Development | Production |
|--------|-------------|------------|
| **Error Pages** | Detailed stack traces | User-friendly messages |
| **Logging** | Debug/Trace (verbose) | Warning/Error (minimal) |
| **Swagger** | Enabled | Disabled (or secured) |
| **HSTS** | Disabled | Enabled (HTTPS enforcement) |
| **Caching** | Often disabled | Enabled for performance |
| **Database** | Local (LocalDB, SQLite) | Production server |
| **Secrets** | User Secrets | Environment Variables/Key Vault |
| **Performance** | Hot reload, no optimization | Optimized, no debugging |

Development prioritizes developer productivity and detailed diagnostics, while Production prioritizes security, performance, and user experience.

---

### Q4: How do you conditionally register middleware based on environment?

**Answer**: Use `IWebHostEnvironment` or `app.Environment` to check the current environment:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Detailed errors
    app.UseSwagger();                // API documentation
}
else
{
    app.UseExceptionHandler("/error"); // User-friendly errors
    app.UseHsts();                     // HTTPS enforcement
}
```

Common pattern: Enable debugging tools (Swagger, detailed errors) in Development, but use secure, user-friendly alternatives in Production.

---

### Q5: Why shouldn't you expose Swagger in Production?

**Answer**: Swagger exposes your API structure, endpoints, request/response schemas, and sometimes example data. In production, this creates security risks:
- **Information disclosure**: Attackers learn API endpoints and parameters
- **Attack surface**: Easier to craft malicious requests
- **Sensitive data**: Example data might contain real information

**Solutions**:
1. **Disable in Production**: `if (app.Environment.IsDevelopment())` wrapper
2. **Secure with Authentication**: `app.MapSwagger().RequireAuthorization()`
3. **Internal/Admin Subdomain**: Deploy to `admin.myapp.com` with IP restrictions

---

### Q6: How do you set ASPNETCORE_ENVIRONMENT in different deployment scenarios?

**Answer**:

**Local (launchSettings.json)**:
```json
"environmentVariables": { "ASPNETCORE_ENVIRONMENT": "Development" }
```

**Windows**:
```powershell
$env:ASPNETCORE_ENVIRONMENT = "Production"
```

**Linux/macOS**:
```bash
export ASPNETCORE_ENVIRONMENT=Production
```

**Docker**:
```dockerfile
ENV ASPNETCORE_ENVIRONMENT=Production
```

**Kubernetes**:
```yaml
env:
  - name: ASPNETCORE_ENVIRONMENT
    value: "Production"
```

**Azure App Service**: Configuration → Application Settings

---

### Q7: What is the default environment if ASPNETCORE_ENVIRONMENT is not set?

**Answer**: If `ASPNETCORE_ENVIRONMENT` is not set, ASP.NET Core defaults to **Production**. This is a safety feature - it's better to default to the more secure, restrictive Production mode rather than the permissive Development mode. Visual Studio sets it to `Development` automatically via `launchSettings.json`, but on production servers it's often left unset (defaulting to Production) or explicitly set to `Production`.

---

### Q8: How do you access environment information in a service or controller?

**Answer**: Inject `IWebHostEnvironment` via constructor:

```csharp
public class MyService
{
    private readonly IWebHostEnvironment _environment;
    
    public MyService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    
    public void DoWork()
    {
        if (_environment.IsDevelopment())
        {
            // Development-specific logic
        }
        else if (_environment.IsProduction())
        {
            // Production-specific logic
        }
        
        var envName = _environment.EnvironmentName;
        var contentRoot = _environment.ContentRootPath;
    }
}
```

Use extension methods (`IsDevelopment()`, `IsProduction()`, `IsStaging()`) for type-safe checks.

---

### Q9: Can you have custom environment names besides Development, Staging, Production?

**Answer**: Yes! ASP.NET Core supports any custom environment name (e.g., `QA`, `UAT`, `PreProduction`, `Demo`). Simply set `ASPNETCORE_ENVIRONMENT` to your custom value:

```bash
export ASPNETCORE_ENVIRONMENT=QA
```

Check custom environments using `IsEnvironment()`:

```csharp
if (app.Environment.IsEnvironment("QA"))
{
    // QA-specific configuration
}
```

Create corresponding configuration files like `appsettings.QA.json` for environment-specific settings.

---

### Q10: What's the configuration precedence order in ASP.NET Core?

**Answer**: Configuration sources are loaded in this order, with **later sources overriding earlier ones**:

1. `appsettings.json` (base)
2. `appsettings.{Environment}.json` (environment-specific)
3. User Secrets (Development only, via `dotnet user-secrets`)
4. Environment Variables
5. Command-line Arguments (highest priority)

Example: If `ConnectionString` is defined in `appsettings.json`, `appsettings.Production.json`, and as an environment variable, the **environment variable value** is used.

This allows flexibility: base defaults in JSON, environment overrides in deployment configs.

---

## Summary

✅ **Key Takeaways:**

- **ASPNETCORE_ENVIRONMENT** is the system variable that controls application behavior across Development, Staging, and Production environments, defaulting to Production if not set

- **Environment-specific configuration files** (`appsettings.{Environment}.json`) override base settings, following a clear precedence order where environment variables and command-line arguments have the highest priority

- **IWebHostEnvironment** service provides environment information throughout the application, with extension methods (`IsDevelopment()`, `IsProduction()`) enabling type-safe conditional logic

- **Middleware and features** should be registered conditionally: enable Developer Exception Page and Swagger in Development, but use HSTS and user-friendly error handlers in Production

- **Avoid common mistakes**: Never run Production with Development settings, don't hardcode environment checks, don't expose Swagger publicly, and never commit secrets to source control

- **Real-world deployments** use different strategies: `launchSettings.json` for local dev, environment variables in Docker/Kubernetes, and configuration settings in cloud platforms (Azure, AWS)

- **In interviews**, demonstrate understanding of environment management importance, configuration precedence, security implications (Development vs Production), and deployment strategies across different hosting scenarios

---

**Master environment management to build secure, flexible applications that behave correctly across development, testing, and production!** 🚀
