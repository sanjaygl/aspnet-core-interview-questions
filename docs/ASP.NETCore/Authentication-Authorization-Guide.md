# Authentication & Authorization in ASP.NET Core – Securing Web APIs

## Introduction

Security is a critical aspect of modern web applications, yet many developers confuse **authentication** (verifying who you are) with **authorization** (verifying what you can do). Understanding the distinction between these two concepts—and how ASP.NET Core implements them—is essential for building secure APIs. This guide explains both authentication and authorization in practical terms, shows how they work in the request pipeline, and provides real-world examples to help you secure your APIs confidently and answer interview questions with clarity.

---

## Authentication vs Authorization

### Definitions

| Term | Definition | Question It Answers |
|------|------------|---------------------|
| **Authentication** | The process of verifying **who the user is** (identity verification) | "Who are you?" |
| **Authorization** | The process of verifying **what the user can do** (permission checking) | "What are you allowed to do?" |

### Real-World Analogy

**Airport Security:**

1. **Authentication:** You show your passport at check-in to prove your identity ("Who are you?")
2. **Authorization:** Security checks your boarding pass to see if you're allowed to board a specific flight ("Can you access this gate?")

**Key Points:**
- Authentication happens **first** (establish identity)
- Authorization happens **after** (check permissions)
- You can be authenticated (logged in) but not authorized (access denied to a specific resource)

### Comparison Table

| Aspect | Authentication | Authorization |
|--------|----------------|---------------|
| **Purpose** | Verify identity | Verify permissions |
| **Question** | "Who are you?" | "What can you do?" |
| **Mechanism** | Login credentials, tokens, certificates | Roles, policies, claims |
| **Result** | User identity established (or rejected) | Access granted (or denied) |
| **Example** | JWT token, cookies, OAuth | `[Authorize(Roles = "Admin")]` |
| **Middleware** | `UseAuthentication()` | `UseAuthorization()` |
| **Fails When** | Invalid credentials, expired token | Insufficient permissions, wrong role |

---

## Authentication in ASP.NET Core

**Authentication** is the process of determining **who the user is**. In ASP.NET Core, authentication middleware reads credentials from the HTTP request (e.g., cookies, JWT tokens, OAuth tokens), validates them, and establishes the user's identity (`ClaimsPrincipal`).

### Common Authentication Mechanisms

| Mechanism | Description | Use Case |
|-----------|-------------|----------|
| **Cookies** | Session-based authentication, server stores session | Traditional web apps (MVC, Razor Pages) |
| **JWT Bearer Tokens** | Stateless tokens containing user claims | Web APIs, mobile apps, SPAs |
| **OAuth 2.0 / OpenID Connect** | Delegated authentication (e.g., login with Google, Microsoft) | Third-party authentication |

---

## Authentication Flow (JWT Example)

JWT (JSON Web Token) is the most common authentication mechanism for Web APIs.

### Step-by-Step Flow

```
1. User Logs In
   ↓
   Client → POST /auth/login { username, password } → Server
   
2. Server Validates Credentials
   ↓
   Database Check: Username/Password Valid?
   
3. Server Generates JWT Token
   ↓
   Token = Header.Payload.Signature
   Payload = { userId: 123, role: "Admin", exp: 1738000000 }
   
4. Token Sent to Client
   ↓
   Server → { token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." } → Client
   
5. Client Stores Token
   ↓
   LocalStorage / SessionStorage / Memory
   
6. Client Sends Token in Requests
   ↓
   Client → GET /api/products (Header: Authorization: Bearer <token>) → Server
   
7. Server Validates Token
   ↓
   Middleware: Verify signature, check expiration, extract claims
   
8. User Identity Established
   ↓
   HttpContext.User = ClaimsPrincipal (userId: 123, role: Admin)
   
9. Request Proceeds to Controller
   ↓
   [Authorize] checks if user is authenticated/authorized
```

---

## Configuring Authentication

### Step 1: Install NuGet Package

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

---

### Step 2: Configure Authentication in `Program.cs`

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://yourapi.com",
        ValidAudience = "https://yourapi.com",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("YourSuperSecretKey123!@#"))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

// CRITICAL: Correct middleware order
app.UseAuthentication(); // Must come BEFORE UseAuthorization
app.UseAuthorization();

app.MapControllers();
app.Run();
```

---

### Step 3: Generate JWT Token (Login Endpoint)

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Validate credentials (simplified)
        if (request.Username != "admin" || request.Password != "password123")
        {
            return Unauthorized("Invalid credentials");
        }

        // Create claims
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "123"),
            new Claim(ClaimTypes.Name, "admin"),
            new Claim(ClaimTypes.Role, "Admin")
        };

        // Generate JWT token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey123!@#"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://yourapi.com",
            audience: "https://yourapi.com",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = tokenString });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEyMyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJhZG1pbiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzM4MDA3NjAwLCJpc3MiOiJodHRwczovL3lvdXJhcGkuY29tIiwiYXVkIjoiaHR0cHM6Ly95b3VyYXBpLmNvbSJ9.abc123xyz"
}
```

---

### Step 4: Protect Endpoints with `[Authorize]`

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    [Authorize] // Requires authentication
    public IActionResult GetProducts()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userName = User.Identity?.Name;
        
        return Ok($"Authenticated user: {userName} (ID: {userId})");
    }
}
```

**Request:**
```http
GET /api/products
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response:** `200 OK` with user data

**Without Token:** `401 Unauthorized`

---

## Authorization in ASP.NET Core

**Authorization** is the process of determining **what the user can do**. After authentication establishes identity, authorization checks if the user has permission to access a specific resource or perform an action.

### Types of Authorization

1. **Role-Based Authorization** – Check if user belongs to a specific role
2. **Policy-Based Authorization** – Check if user meets custom requirements
3. **Claims-Based Authorization** – Check if user has specific claims

---

## Using `[Authorize]` and `[AllowAnonymous]`

### `[Authorize]` Attribute

Restricts access to authenticated users only.

```csharp
[Authorize] // Requires authentication
[HttpGet("profile")]
public IActionResult GetProfile()
{
    return Ok("User profile");
}
```

---

### `[AllowAnonymous]` Attribute

Allows public access (overrides `[Authorize]` at controller level).

```csharp
[Authorize] // All actions require authentication
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        // Requires authentication
        return Ok($"User {id}");
    }

    [AllowAnonymous] // Public access
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        // No authentication required
        return Ok("User registered");
    }
}
```

---

## Role-Based Authorization

Restrict access based on user roles.

### Example: Admin-Only Endpoint

```csharp
[Authorize(Roles = "Admin")]
[HttpDelete("products/{id}")]
public IActionResult DeleteProduct(int id)
{
    return Ok($"Product {id} deleted");
}
```

**Flow:**
- User must be authenticated (`[Authorize]`)
- User must have role "Admin"
- If role missing → `403 Forbidden`

---

### Example: Multiple Roles

```csharp
[Authorize(Roles = "Admin,Manager")]
[HttpPut("products/{id}")]
public IActionResult UpdateProduct(int id)
{
    return Ok($"Product {id} updated");
}
```

**Allows:** Users with role "Admin" **OR** "Manager"

---

## Policy-Based Authorization

Define custom authorization requirements using policies.

### Step 1: Define Policy in `Program.cs`

```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("MinimumAge", policy =>
        policy.RequireClaim("Age", "18", "19", "20")); // Age >= 18

    options.AddPolicy("EmployeeOnly", policy =>
        policy.RequireClaim("EmployeeNumber"));
});
```

---

### Step 2: Apply Policy to Endpoints

```csharp
[Authorize(Policy = "RequireAdminRole")]
[HttpGet("admin/reports")]
public IActionResult GetReports()
{
    return Ok("Admin reports");
}

[Authorize(Policy = "EmployeeOnly")]
[HttpGet("employee/dashboard")]
public IActionResult GetDashboard()
{
    return Ok("Employee dashboard");
}
```

---

## Claims-Based Authorization

**Claims** are key-value pairs that describe the user (e.g., `UserId`, `Email`, `Role`, `EmployeeNumber`).

### Example: Adding Claims During Login

```csharp
var claims = new[]
{
    new Claim(ClaimTypes.NameIdentifier, "123"),
    new Claim(ClaimTypes.Name, "john.doe"),
    new Claim(ClaimTypes.Email, "john@example.com"),
    new Claim(ClaimTypes.Role, "Manager"),
    new Claim("EmployeeNumber", "EMP-456"),
    new Claim("Department", "Sales")
};
```

---

### Example: Accessing Claims in Controllers

```csharp
[Authorize]
[HttpGet("me")]
public IActionResult GetCurrentUser()
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var email = User.FindFirst(ClaimTypes.Email)?.Value;
    var role = User.FindFirst(ClaimTypes.Role)?.Value;
    var department = User.FindFirst("Department")?.Value;

    return Ok(new
    {
        UserId = userId,
        Email = email,
        Role = role,
        Department = department
    });
}
```

---

### Example: Policy Based on Multiple Claims

```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SalesManagerOnly", policy =>
        policy.RequireRole("Manager")
              .RequireClaim("Department", "Sales"));
});
```

```csharp
[Authorize(Policy = "SalesManagerOnly")]
[HttpGet("sales/reports")]
public IActionResult GetSalesReports()
{
    return Ok("Sales reports");
}
```

**Allowed:** Users with role "Manager" **AND** department "Sales"

---

## Authorization Flow in Request Pipeline

### Middleware Order (CRITICAL)

```csharp
var app = builder.Build();

app.UseRouting();          // 1. Routing determines which endpoint to call
app.UseAuthentication();   // 2. Authentication establishes user identity
app.UseAuthorization();    // 3. Authorization checks if user has permission
app.MapControllers();      // 4. Endpoint executes (if authorized)

app.Run();
```

### Request Flow

```
Client Request
    ↓
UseRouting → Determine endpoint
    ↓
UseAuthentication → Read token, validate, set HttpContext.User
    ↓
UseAuthorization → Check [Authorize] attribute, roles, policies
    ↓
[Authorize] Check:
    - Is user authenticated? (Yes/No)
    - Does user have required role? (Yes/No)
    - Does user satisfy policy? (Yes/No)
    ↓
If Authorized → Execute Controller Action
If Not Authorized → Return 403 Forbidden
If Not Authenticated → Return 401 Unauthorized
```

---

## Common Security Mistakes

### ❌ Mistake 1: Missing `UseAuthentication()`

```csharp
var app = builder.Build();

app.UseAuthorization(); // Bug: Authentication not called
app.MapControllers();

app.Run();
```

**Result:** `HttpContext.User` is always null, `[Authorize]` always fails → `401 Unauthorized`

**Fix:** Always call `UseAuthentication()` **before** `UseAuthorization()`

---

### ❌ Mistake 2: Incorrect Middleware Order

```csharp
var app = builder.Build();

app.UseAuthorization();   // Bug: Authorization BEFORE authentication
app.UseAuthentication();  // Too late — User already null
app.MapControllers();

app.Run();
```

**Result:** Authorization checks fail because user identity isn't established yet.

**Fix:** `UseAuthentication()` must come **before** `UseAuthorization()`

---

### ❌ Mistake 3: Over-Trusting Client Data

```csharp
[HttpGet("admin/reports")]
public IActionResult GetReports([FromQuery] string role)
{
    // Bug: Trusting client-provided role
    if (role == "Admin")
    {
        return Ok("Admin reports");
    }
    return Forbid();
}
```

**Result:** Any client can send `?role=Admin` and bypass security.

**Fix:** Always use `User.IsInRole()` or `[Authorize(Roles = "Admin")]`

```csharp
[Authorize(Roles = "Admin")]
[HttpGet("admin/reports")]
public IActionResult GetReports()
{
    return Ok("Admin reports");
}
```

---

### ❌ Mistake 4: Not Validating Token Properly

```csharp
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,        // Bug: Not validating issuer
        ValidateAudience = false,      // Bug: Not validating audience
        ValidateLifetime = false,      // Bug: Accepting expired tokens
        ValidateIssuerSigningKey = true
    };
});
```

**Result:** Insecure — accepts tokens from any source, even expired ones.

**Fix:** Validate everything:
```csharp
ValidateIssuer = true,
ValidateAudience = true,
ValidateLifetime = true,
ValidateIssuerSigningKey = true
```

---

### ❌ Mistake 5: Storing JWT in LocalStorage (XSS Vulnerability)

```javascript
// Bug: Vulnerable to XSS attacks
localStorage.setItem('token', jwtToken);
```

**Result:** Malicious scripts can steal tokens.

**Fix:** Use `HttpOnly` cookies for web apps, or secure in-memory storage for SPAs.

---

## Real-World Use Cases

### 1. Securing APIs

```csharp
[Authorize] // All actions require authentication
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var orders = _orderService.GetOrdersByUserId(userId);
        return Ok(orders);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public IActionResult GetAllOrders()
    {
        var orders = _orderService.GetAllOrders();
        return Ok(orders);
    }
}
```

---

### 2. Role-Based Access

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous] // Public: Anyone can view products
    public IActionResult GetProducts() => Ok(_productService.GetAll());

    [Authorize(Roles = "Manager,Admin")]
    [HttpPost] // Managers and Admins can add products
    public IActionResult CreateProduct(ProductDto product) => Ok();

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")] // Only Admins can delete products
    public IActionResult DeleteProduct(int id) => Ok();
}
```

---

### 3. Company/Tenant-Based Access

```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SameCompany", policy =>
        policy.RequireAssertion(context =>
        {
            var userCompanyId = context.User.FindFirst("CompanyId")?.Value;
            var requestedCompanyId = context.Resource as string; // From route/query
            return userCompanyId == requestedCompanyId;
        }));
});
```

```csharp
[Authorize(Policy = "SameCompany")]
[HttpGet("companies/{companyId}/reports")]
public IActionResult GetCompanyReports(string companyId)
{
    // User can only access reports for their own company
    return Ok("Company reports");
}
```

---

## Common Interview Questions

### Q1: What's the difference between authentication and authorization?

**Answer:**
- **Authentication** verifies **who the user is** (identity verification) — "Who are you?"
- **Authorization** verifies **what the user can do** (permission checking) — "What are you allowed to do?"
- Authentication happens first, authorization happens after.

---

### Q2: What is JWT (JSON Web Token)?

**Answer:** JWT is a stateless, self-contained token used for authentication in Web APIs. It contains three parts (Header.Payload.Signature) and includes user claims (userId, role, etc.). The server validates the signature to ensure the token hasn't been tampered with.

---

### Q3: What's the difference between role-based and policy-based authorization?

**Answer:**
- **Role-based:** Checks if user belongs to a specific role — `[Authorize(Roles = "Admin")]`
- **Policy-based:** Checks if user meets custom requirements (roles + claims) — `[Authorize(Policy = "SalesManagerOnly")]`
- Policies are more flexible and can combine multiple conditions.

---

### Q4: Where does authentication middleware run in the pipeline?

**Answer:** Authentication middleware (`UseAuthentication()`) runs **after** `UseRouting()` and **before** `UseAuthorization()`. It reads credentials from the request, validates them, and establishes the user's identity (`HttpContext.User`).

---

### Q5: What happens if you forget to call `UseAuthentication()`?

**Answer:** The `HttpContext.User` will always be null or unauthenticated. All `[Authorize]` checks will fail, and requests will return `401 Unauthorized` even with a valid token.

---

### Q6: Can you have `[Authorize]` at both controller and action levels?

**Answer:** Yes. Controller-level `[Authorize]` applies to all actions. Action-level `[Authorize]` can add additional restrictions (like roles or policies) or use `[AllowAnonymous]` to allow public access.

---

### Q7: What is `HttpContext.User`?

**Answer:** `HttpContext.User` is a `ClaimsPrincipal` object that represents the authenticated user. It contains claims (userId, email, roles) extracted from the authentication token. You access it in controllers with `User.FindFirst(ClaimTypes.NameIdentifier)`.

---

### Q8: What's the difference between `401 Unauthorized` and `403 Forbidden`?

**Answer:**
- **401 Unauthorized:** User is **not authenticated** (no valid token, invalid credentials)
- **403 Forbidden:** User is authenticated but **not authorized** (lacks required role or permission)

---

### Q9: How do you add custom claims to a JWT token?

**Answer:**
```csharp
var claims = new[]
{
    new Claim(ClaimTypes.NameIdentifier, "123"),
    new Claim("EmployeeNumber", "EMP-456"),
    new Claim("Department", "Sales")
};
```
These claims are included in the JWT payload and can be accessed via `User.FindFirst("ClaimName")`.

---

### Q10: Why should `UseAuthentication()` come before `UseAuthorization()`?

**Answer:** Authentication establishes the user's identity (`HttpContext.User`). Authorization checks if that user has permission. If authorization runs first, there's no user identity to check, so all authorization checks fail.

---

## Summary

- **Authentication** verifies **who the user is** (identity), while **authorization** verifies **what the user can do** (permissions) — they are distinct concepts that work together
- **JWT tokens** are the most common authentication mechanism for Web APIs — they're stateless, self-contained, and include user claims (userId, role, etc.)
- **Middleware order is critical** — `UseAuthentication()` must come **before** `UseAuthorization()` in the pipeline, or authentication won't work
- **`[Authorize]` attribute** restricts access to authenticated users, while **role-based** (`[Authorize(Roles = "Admin")]`) and **policy-based** authorization add permission checks
- **Common mistakes** include missing `UseAuthentication()`, incorrect middleware order, over-trusting client data, and not validating tokens properly
- **Claims-based authorization** uses key-value pairs (claims) to make fine-grained permission decisions, enabling flexible policies beyond simple role checks
- **Real-world use cases** include securing API endpoints, implementing role-based access (Admin/Manager/User), and multi-tenant access control based on company/organization claims

