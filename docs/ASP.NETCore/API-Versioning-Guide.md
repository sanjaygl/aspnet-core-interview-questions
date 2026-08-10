# API Versioning in ASP.NET Core – Designing Evolvable APIs

## Introduction

As APIs evolve, introducing breaking changes can disrupt existing clients like mobile apps, web frontends, or third-party integrations. API versioning allows you to maintain backward compatibility while releasing new features or improvements. Without versioning, changing a response structure or removing a field can break production applications. This guide explains practical API versioning strategies, implementation approaches, and real-world considerations for building evolvable ASP.NET Core Web APIs.

---

## What is API Versioning?

**API Versioning** is the practice of managing multiple versions of an API simultaneously, allowing clients to choose which version they consume. It ensures that existing clients continue working when you introduce breaking changes in newer API versions.

**Why Backward Compatibility Matters:**
- Mobile apps may not update immediately
- Third-party integrations depend on contract stability
- Breaking changes without versioning cause production failures
- Gradual migration reduces risk for consumers

---

## When Should You Version an API?

### Breaking Changes (Require New Version)

Breaking changes alter the API contract in ways that break existing clients:

```csharp
// V1: Original response
public class ProductV1
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// V2: Breaking change - renamed property
public class ProductV2
{
    public int Id { get; set; }
    public string ProductName { get; set; }  // Breaking: "Name" → "ProductName"
    public decimal Price { get; set; }
    public string Category { get; set; }     // Breaking: Required field added
}
```

**Common Breaking Changes:**
- Renaming or removing properties
- Changing response structure
- Adding required request parameters
- Changing HTTP status codes
- Modifying authentication schemes

### Non-Breaking Changes (No Version Needed)

Non-breaking changes enhance the API without affecting existing clients:

```csharp
// V1: Original response
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// V1.1: Non-breaking additions
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }  // Non-breaking: Optional field
    public DateTime? CreatedAt { get; set; }  // Non-breaking: Optional field
}
```

**Common Non-Breaking Changes:**
- Adding optional properties to responses
- Adding new endpoints
- Adding optional query parameters
- Expanding enum values (with proper client handling)

---

## Common API Versioning Strategies

### 1. URL-Based Versioning (Most Common)

Version is embedded in the URL path.

```csharp
// URL-based versioning
GET /api/v1/products
GET /api/v2/products
```

**Pros:**
- Clear and visible
- Easy to cache
- Simple to test with tools like Postman

**Cons:**
- URL changes require client code updates
- Can lead to URL bloat

### 2. Query String Versioning

Version is specified as a query parameter.

```csharp
// Query string versioning
GET /api/products?api-version=1.0
GET /api/products?api-version=2.0
```

**Pros:**
- Doesn't pollute URL structure
- Easy to add to existing APIs

**Cons:**
- Less visible
- Query parameters can be accidentally omitted
- Harder to cache effectively

### 3. Header-Based Versioning

Version is passed in a custom HTTP header.

```csharp
// Header-based versioning
GET /api/products
Headers: X-Api-Version: 1.0

GET /api/products
Headers: X-Api-Version: 2.0
```

**Pros:**
- Clean URLs
- Follows RESTful principles
- Good separation of concerns

**Cons:**
- Not visible in browser
- Harder to test manually
- Requires documentation

### 4. Media Type Versioning (Content Negotiation)

Version is specified in the `Accept` header using custom media types.

```csharp
// Media type versioning
GET /api/products
Headers: Accept: application/vnd.myapi.v1+json

GET /api/products
Headers: Accept: application/vnd.myapi.v2+json
```

**Pros:**
- True RESTful approach
- Aligns with HTTP standards
- Supports multiple formats (JSON/XML)

**Cons:**
- Most complex to implement
- Hardest for developers to understand
- Requires custom media type registration

---

## Recommended Versioning Approach

### URL-Based Versioning (Recommended for Most Scenarios)

For most ASP.NET Core Web APIs, **URL-based versioning** is recommended because:

1. **Clarity**: Version is immediately visible in the URL
2. **Simplicity**: Easy to understand and implement
3. **Tooling**: Works seamlessly with Swagger, Postman, and curl
4. **Caching**: Simple cache invalidation per version
5. **Industry Standard**: Used by major APIs (Stripe, GitHub, Twitter)

**Trade-offs Summary:**

| Strategy | Visibility | Simplicity | RESTful | Caching | Tooling Support |
|----------|-----------|-----------|---------|---------|----------------|
| URL-based | ✅ High | ✅ Easy | ⚠️ Moderate | ✅ Easy | ✅ Excellent |
| Query String | ⚠️ Moderate | ✅ Easy | ⚠️ Moderate | ⚠️ Complex | ✅ Good |
| Header-based | ❌ Low | ⚠️ Moderate | ✅ Good | ⚠️ Complex | ⚠️ Moderate |
| Media Type | ❌ Low | ❌ Complex | ✅ Excellent | ❌ Complex | ❌ Poor |

---

## Implementing API Versioning in ASP.NET Core

### Step 1: Install the Package

```bash
dotnet add package Asp.Versioning.Mvc
```

### Step 2: Configure API Versioning Services

```csharp
// Program.cs
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    // Report API versions in response headers
    options.ReportApiVersions = true;
    
    // Use default version when client doesn't specify
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    
    // Support multiple versioning strategies
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),           // /api/v1/products
        new QueryStringApiVersionReader("api-version"), // ?api-version=1.0
        new HeaderApiVersionReader("X-Api-Version")     // X-Api-Version: 1.0
    );
}).AddApiExplorer(options =>
{
    // Format version as 'v'major[.minor] (e.g., v1, v2)
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();
```

**Key Configuration Options:**
- `ReportApiVersions`: Adds `api-supported-versions` and `api-deprecated-versions` headers to responses
- `AssumeDefaultVersionWhenUnspecified`: Uses default version when client doesn't specify one
- `ApiVersionReader`: Defines how versions are read (URL, query string, header)

---

## Versioned Controllers

### Using [ApiVersion] Attribute

```csharp
// V1 Controller
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = new[]
        {
            new { Id = 1, Name = "Laptop", Price = 999.99 }
        };
        
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        return Ok(new { Id = id, Name = "Laptop", Price = 999.99 });
    }
}
```

```csharp
// V2 Controller - Breaking changes
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public class ProductsV2Controller : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = new[]
        {
            new { 
                Id = 1, 
                ProductName = "Laptop",  // Breaking: Renamed from "Name"
                Price = 999.99,
                Category = "Electronics" // Breaking: New required field
            }
        };
        
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        return Ok(new { 
            Id = id, 
            ProductName = "Laptop", 
            Price = 999.99,
            Category = "Electronics"
        });
    }
}
```

### Mapping Multiple Versions to Same Controller

```csharp
// Support both V1 and V2 in one controller
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class OrdersController : ControllerBase
{
    // Available in V1 only
    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult GetOrdersV1()
    {
        return Ok(new { Message = "Orders V1" });
    }
    
    // Available in V2 only
    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult GetOrdersV2()
    {
        return Ok(new { Message = "Orders V2 with enhanced data" });
    }
    
    // Available in both versions
    [HttpGet("{id}")]
    public IActionResult GetOrder(int id, ApiVersion apiVersion)
    {
        if (apiVersion.MajorVersion == 1)
            return Ok(new { Id = id, Status = "Completed" });
        
        // V2 includes more details
        return Ok(new { Id = id, Status = "Completed", TrackingNumber = "ABC123" });
    }
}
```

---

## Versioning with Routing

### Route Templates for Versioned APIs

```csharp
// URL segment versioning
[Route("api/v{version:apiVersion}/products")]

// Example URLs:
// GET /api/v1/products
// GET /api/v2/products
// GET /api/v1/products/123
```

### Namespace-Based Versioning (Alternative)

```csharp
// V1/ProductsController.cs
namespace MyApi.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("V1");
    }
}

// V2/ProductsController.cs
namespace MyApi.Controllers.V2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("V2");
    }
}
```

**Benefits:**
- Clean separation of versions
- Easy to maintain version-specific logic
- Clear folder structure

---

## Deprecating API Versions

### Marking Versions as Deprecated

```csharp
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0", Deprecated = true)]  // Mark as deprecated
[ApiVersion("2.0")]                      // Current version
public class ProductsController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult GetV1()
    {
        return Ok(new { Message = "V1 - Deprecated, please migrate to V2" });
    }
    
    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult GetV2()
    {
        return Ok(new { Message = "V2 - Current version" });
    }
}
```

**Deprecation Response Headers:**

When `ReportApiVersions = true`, deprecated versions are reported:

```http
HTTP/1.1 200 OK
api-supported-versions: 1.0, 2.0
api-deprecated-versions: 1.0
```

### Communicating Deprecation to Clients

```csharp
[HttpGet]
[MapToApiVersion("1.0")]
public IActionResult GetV1()
{
    // Add deprecation warning in response
    Response.Headers.Add("X-API-Warn", "Version 1.0 is deprecated. Migrate to v2 by March 2026.");
    Response.Headers.Add("X-API-Deprecation-Info", "https://api.example.com/docs/migration-v2");
    
    return Ok(new { Message = "V1 data" });
}
```

**Best Practices for Deprecation:**
1. Announce deprecation at least 6 months in advance
2. Provide migration guides and documentation
3. Set a sunset date and communicate it clearly
4. Monitor usage of deprecated versions
5. Send notifications to API consumers
6. Gradually reduce rate limits for deprecated versions

---

## API Versioning with Swagger

### Configuring Swagger for Multiple Versions

```bash
dotnet add package Asp.Versioning.Mvc.ApiExplorer
dotnet add package Swashbuckle.AspNetCore
```

```csharp
// Program.cs
using Asp.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();

// Configure Swagger for multiple versions
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
        
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.MapControllers();
app.Run();
```

### Swagger Configuration Class

```csharp
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo
                {
                    Title = $"My API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = description.IsDeprecated 
                        ? "⚠️ This API version is deprecated" 
                        : "Current API version"
                });
        }
    }
}
```

**Result:**
- Swagger UI displays dropdown with V1, V2, V3, etc.
- Each version shows only its endpoints
- Deprecated versions clearly marked

---

## Real-World Use Cases

### 1. Mobile App Evolution

```csharp
// V1: Initial mobile app release (2024)
[ApiVersion("1.0")]
public class UserProfileController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetProfile(int id)
    {
        return Ok(new 
        { 
            UserId = id, 
            Name = "John Doe",
            Email = "john@example.com"
        });
    }
}

// V2: Mobile app update with enhanced features (2025)
[ApiVersion("2.0")]
public class UserProfileV2Controller : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetProfile(int id)
    {
        // V2 adds profile picture, bio, preferences
        return Ok(new 
        { 
            UserId = id, 
            FullName = "John Doe",      // Breaking: "Name" → "FullName"
            Email = "john@example.com",
            ProfilePictureUrl = "https://...",
            Bio = "Software developer",
            Preferences = new { Theme = "dark", Language = "en" }
        });
    }
}
```

**Why Versioning Matters:**
- Old mobile apps (v1.0) still work while users gradually update
- New mobile app (v2.0) gets enhanced features
- No forced updates required

### 2. Public API with Multiple Clients

```csharp
// V1: Used by 100+ third-party integrations
[ApiVersion("1.0")]
public class PaymentsController : ControllerBase
{
    [HttpPost]
    public IActionResult ProcessPayment([FromBody] PaymentRequest request)
    {
        // V1: Simple payment processing
        return Ok(new { TransactionId = "TXN123", Status = "Success" });
    }
}

// V2: Enhanced payment with fraud detection
[ApiVersion("2.0")]
public class PaymentsV2Controller : ControllerBase
{
    [HttpPost]
    public IActionResult ProcessPayment([FromBody] PaymentRequestV2 request)
    {
        // V2: Requires additional fraud detection fields
        return Ok(new 
        { 
            TransactionId = "TXN123", 
            Status = "Success",
            FraudScore = 0.02,
            RiskLevel = "Low"
        });
    }
}
```

### 3. Gradual Feature Rollout

```csharp
[ApiController]
[Route("api/v{version:apiVersion}/products")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts(ApiVersion version)
    {
        var products = _productService.GetProducts();
        
        if (version.MajorVersion >= 2)
        {
            // V2: Include AI-powered recommendations
            var recommendations = _aiService.GetRecommendations();
            return Ok(new { Products = products, Recommendations = recommendations });
        }
        
        // V1: Simple product list
        return Ok(products);
    }
}
```

---

## Common Mistakes

### ❌ Mistake 1: Versioning Too Early

```csharp
// Bad: Versioning a brand new API
[ApiVersion("1.0")]
public class BrandNewFeatureController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("This API has no consumers yet!");
}
```

**Why It's Wrong:**
- No clients exist to maintain backward compatibility for
- Adds unnecessary complexity
- Wait until you have real consumers

**✅ Better Approach:**
Start without versioning. Add versioning only when you need to introduce breaking changes.

### ❌ Mistake 2: Versioning Everything Unnecessarily

```csharp
// Bad: Creating V2 for non-breaking changes
[ApiVersion("1.0")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { Id = 1, Name = "Laptop" });
}

[ApiVersion("2.0")]  // Unnecessary version bump
public class ProductsV2Controller : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { Id = 1, Name = "Laptop", Description = "Optional field" });
}
```

**Why It's Wrong:**
- Adding optional fields is non-breaking
- Creates maintenance burden
- Confuses API consumers

**✅ Better Approach:**
Only create new versions for breaking changes. Add optional fields to existing versions.

### ❌ Mistake 3: Breaking APIs Without Version Bump

```csharp
// Bad: Breaking change without new version
[ApiVersion("1.0")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        // Changed "OrderId" to "Id" - BREAKING CHANGE!
        return Ok(new { Id = 123, Total = 99.99 });
    }
}
```

**Why It's Wrong:**
- Existing clients break immediately
- No migration path
- Damages API reputation

**✅ Better Approach:**
Create V2 with breaking changes. Keep V1 unchanged.

### ❌ Mistake 4: No Deprecation Strategy

```csharp
// Bad: Supporting old versions forever
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[ApiVersion("3.0")]
[ApiVersion("4.0")]  // Too many versions!
public class UsersController : ControllerBase { }
```

**Why It's Wrong:**
- Maintenance nightmare
- Technical debt accumulates
- Performance and security issues

**✅ Better Approach:**
Deprecate old versions with clear sunset dates. Support max 2-3 versions simultaneously.

### ❌ Mistake 5: Inconsistent Versioning Strategies

```csharp
// Bad: Mixing versioning approaches
[Route("api/v1/products")]    // URL versioning
public class ProductsController : ControllerBase { }

[Route("api/orders")]          // Header versioning
public class OrdersController : ControllerBase { }
```

**Why It's Wrong:**
- Confuses API consumers
- Hard to document
- Inconsistent experience

**✅ Better Approach:**
Choose one versioning strategy and apply it consistently across all endpoints.

---

## Interview Questions

### 1. Why is API versioning needed?

API versioning is needed to maintain backward compatibility while evolving APIs. Without versioning, introducing breaking changes (like renaming fields, changing response structures, or modifying authentication) would break existing clients such as mobile apps, web frontends, or third-party integrations. Versioning allows old and new clients to coexist during migration periods.

### 2. What is the best API versioning strategy?

**URL-based versioning** (`/api/v1/products`) is the most common and recommended approach because it's visible, simple to implement, works well with caching and tooling (Swagger, Postman), and is widely adopted by major APIs. However, the "best" strategy depends on your requirements: header-based versioning is more RESTful, query string versioning is easier to add to existing APIs, and media type versioning follows HTTP standards most closely.

### 3. What's the difference between breaking and non-breaking changes?

**Breaking changes** alter the API contract in ways that break existing clients (e.g., renaming properties, removing endpoints, adding required parameters, changing status codes). These require a new API version. **Non-breaking changes** enhance the API without affecting existing clients (e.g., adding optional properties, new endpoints, optional query parameters). These don't require versioning.

### 4. How do you deprecate an API version?

Mark the version as deprecated using `[ApiVersion("1.0", Deprecated = true)]`. Enable `ReportApiVersions = true` to include deprecation headers in responses. Communicate the deprecation through documentation, response headers (`X-API-Warn`), email notifications, and provide a sunset date (typically 6+ months). Monitor usage and gradually reduce rate limits before final removal.

### 5. Can you support multiple versioning strategies simultaneously?

Yes, ASP.NET Core allows combining multiple versioning strategies using `ApiVersionReader.Combine()`. You can support URL, query string, and header-based versioning simultaneously, giving clients flexibility. However, this increases complexity, so it's generally better to choose one primary strategy.

### 6. When should you NOT version an API?

Don't version a brand new API with no consumers—wait until you need to introduce breaking changes. Don't version for non-breaking changes like adding optional fields. Don't version internal APIs consumed by your own applications if you can coordinate deployments. Only version when backward compatibility is necessary.

### 7. How does [MapToApiVersion] work?

`[MapToApiVersion]` maps a specific action method to one or more API versions within a controller that supports multiple versions. This allows you to have different implementations of the same endpoint for different versions while sharing the same controller class.

```csharp
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult GetV1() => Ok("V1");
    
    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult GetV2() => Ok("V2");
}
```

### 8. What happens when a client doesn't specify an API version?

When `AssumeDefaultVersionWhenUnspecified = true` is configured, the API uses the `DefaultApiVersion` (e.g., v1.0). If this setting is `false` and no version is specified, the API returns a `400 Bad Request` with an error message indicating that an API version is required.

### 9. How do you version Swagger documentation?

Install `Asp.Versioning.Mvc.ApiExplorer` and configure Swagger to generate separate documents for each API version. Use `IApiVersionDescriptionProvider` to create Swagger documents dynamically. The Swagger UI displays a dropdown allowing users to switch between versions (v1, v2, etc.), with each version showing only its endpoints.

### 10. How many API versions should you support simultaneously?

Best practice is to support **2-3 versions maximum** at any time: the current version, one previous version for migration, and optionally one deprecated version approaching sunset. Supporting too many versions creates maintenance overhead, security risks, and technical debt. Plan deprecation timelines (6-12 months) and actively encourage migration to newer versions.

---

## Summary

- **API versioning maintains backward compatibility** when introducing breaking changes, allowing old and new clients to coexist during migration periods
- **URL-based versioning (`/api/v1/products`)** is the most common and recommended approach for its simplicity, visibility, and excellent tooling support
- **Only version for breaking changes** like renaming properties, removing endpoints, or adding required parameters; non-breaking changes (optional fields) don't need new versions
- **Deprecate old versions systematically** by marking them with `Deprecated = true`, communicating sunset dates (6-12 months), and monitoring usage before removal
- **Use Asp.Versioning.Mvc package** to implement versioning with `[ApiVersion]` attributes, configure default versions, and integrate with Swagger for multi-version documentation
- **Support 2-3 versions maximum** to balance backward compatibility with maintenance burden; supporting too many versions creates technical debt
- **Common mistakes** include versioning too early (before you have consumers), versioning non-breaking changes, and breaking APIs without version bumps—plan versioning strategically based on real needs
