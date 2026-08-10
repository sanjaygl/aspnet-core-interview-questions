# CORS in ASP.NET Core – Securing Cross-Origin Requests

## Introduction

Modern web applications often involve a frontend (React, Angular, Vue) hosted on one domain calling a backend API hosted on another domain. Browsers, by default, block such **cross-origin requests** for security reasons. This is where CORS (Cross-Origin Resource Sharing) comes in. CORS is a browser security mechanism that allows servers to specify which origins (domains) are permitted to access their resources. Understanding how to configure CORS correctly in ASP.NET Core is critical—misconfiguration leads to frustrating "blocked by CORS policy" errors or, worse, security vulnerabilities.

---

## What is CORS?

**CORS (Cross-Origin Resource Sharing)** is a W3C standard that allows servers to relax the **Same-Origin Policy** enforced by browsers. The Same-Origin Policy prevents JavaScript running on one origin (domain, protocol, port) from accessing resources on a different origin.

### Same-Origin Policy (Quick Explanation)

Browsers consider two URLs to have the **same origin** if they share:
1. **Protocol** (http vs https)
2. **Domain** (example.com vs api.example.com)
3. **Port** (80 vs 8080)

**Examples:**

| Frontend URL | API URL | Same Origin? |
|--------------|---------|--------------|
| `https://example.com` | `https://example.com/api` | ✅ Yes |
| `https://example.com` | `https://api.example.com` | ❌ No (different subdomain) |
| `http://localhost:3000` | `http://localhost:5000` | ❌ No (different port) |
| `https://example.com` | `http://example.com` | ❌ No (different protocol) |

**Without CORS:**
- Browser blocks the request
- API never receives the request
- Frontend gets a CORS error

**With CORS:**
- Server sends `Access-Control-Allow-Origin` header
- Browser allows the request
- Frontend can access API data

---

## How CORS Works (High-Level)

CORS works through HTTP headers. The browser checks if the server allows cross-origin requests by examining response headers.

### Request Types

#### 1. Simple Requests

Simple requests are sent **directly** to the server without a preflight check.

**Criteria for Simple Requests:**
- HTTP methods: `GET`, `HEAD`, or `POST`
- Only simple headers: `Accept`, `Accept-Language`, `Content-Language`, `Content-Type`
- `Content-Type` values: `application/x-www-form-urlencoded`, `multipart/form-data`, `text/plain`

**Flow:**
```
Browser → GET /api/products (Origin: https://frontend.com) → Server
Browser ← Response + Access-Control-Allow-Origin: https://frontend.com ← Server
```

If the response includes `Access-Control-Allow-Origin` matching the request origin, the browser allows JavaScript to access the response.

---

#### 2. Preflight Requests

Preflight requests are sent **before** the actual request to check if the server allows the operation.

**When Preflight Happens:**
- HTTP methods: `PUT`, `DELETE`, `PATCH`
- Custom headers (e.g., `Authorization`, `X-Custom-Header`)
- `Content-Type`: `application/json`

**Flow:**
```
Step 1: Preflight (OPTIONS request)
Browser → OPTIONS /api/products (Origin: https://frontend.com) → Server
Browser ← 204 No Content + CORS headers ← Server

Step 2: Actual Request (if preflight succeeds)
Browser → DELETE /api/products/123 → Server
Browser ← Response + CORS headers ← Server
```

**OPTIONS Request:**
- Sent automatically by the browser
- Checks which methods/headers are allowed
- Returns `204 No Content` (no body)

---

## Common CORS Headers

### 1. `Access-Control-Allow-Origin`

Specifies which origins can access the resource.

**Examples:**
```http
Access-Control-Allow-Origin: https://frontend.com
Access-Control-Allow-Origin: *
```

**Values:**
- Specific origin: `https://frontend.com` (recommended)
- Wildcard: `*` (allows all origins, but cannot be used with credentials)

---

### 2. `Access-Control-Allow-Methods`

Specifies which HTTP methods are allowed.

```http
Access-Control-Allow-Methods: GET, POST, PUT, DELETE
```

---

### 3. `Access-Control-Allow-Headers`

Specifies which request headers are allowed.

```http
Access-Control-Allow-Headers: Content-Type, Authorization
```

---

### 4. `Access-Control-Allow-Credentials`

Indicates whether credentials (cookies, HTTP authentication) can be included.

```http
Access-Control-Allow-Credentials: true
```

**Important:** When `true`, `Access-Control-Allow-Origin` **cannot** be `*` (must be a specific origin).

---

## Enabling CORS in ASP.NET Core

### Step 1: Add CORS Services

In `Program.cs`, register CORS services:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://frontend.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Use CORS middleware (MUST come before UseAuthorization)
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

---

### Step 2: Define Named Policies

Define multiple policies for different scenarios:

```csharp
builder.Services.AddCors(options =>
{
    // Policy 1: Production Frontend
    options.AddPolicy("AllowProduction", policy =>
    {
        policy.WithOrigins("https://myapp.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

    // Policy 2: Development (localhost)
    options.AddPolicy("AllowDevelopment", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });

    // Policy 3: Allow All (Testing Only - NOT recommended for production)
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

---

### Step 3: Use Environment-Specific Policies

```csharp
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowDevelopment");
}
else
{
    app.UseCors("AllowProduction");
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

---

## Applying CORS Policies

### 1. Global CORS (Apply to All Endpoints)

```csharp
app.UseCors("AllowFrontend"); // Applies to all controllers/endpoints
```

---

### 2. Controller-Level CORS

```csharp
[EnableCors("AllowFrontend")]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts() => Ok("Products");
}
```

---

### 3. Action-Level CORS

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [EnableCors("AllowFrontend")]
    [HttpGet]
    public IActionResult GetProducts() => Ok("Products");

    [DisableCors] // Disable CORS for this action
    [HttpPost]
    public IActionResult CreateProduct() => Ok("Created");
}
```

---

## CORS Middleware Order (Very Important)

**CRITICAL:** `UseCors()` must be placed **after** `UseRouting()` and **before** `UseAuthentication()` / `UseAuthorization()` / `UseEndpoints()`.

### ✅ Correct Order

```csharp
var app = builder.Build();

app.UseRouting();         // 1. Routing
app.UseCors("MyPolicy");  // 2. CORS (MUST be here)
app.UseAuthentication();  // 3. Authentication
app.UseAuthorization();   // 4. Authorization
app.MapControllers();     // 5. Endpoints

app.Run();
```

### ❌ Incorrect Order

```csharp
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MyPolicy");  // Bug: CORS after auth — preflight fails!
app.MapControllers();

app.Run();
```

**Result:** Preflight requests (OPTIONS) fail because authentication middleware runs before CORS, blocking the request.

---

## Handling Preflight Requests

ASP.NET Core automatically handles **OPTIONS** requests when CORS is configured correctly.

### How Preflight Works

**Frontend Code:**
```javascript
fetch('https://api.example.com/products/123', {
  method: 'DELETE',
  headers: {
    'Authorization': 'Bearer token123'
  }
});
```

**Step 1: Browser Sends Preflight (OPTIONS)**
```http
OPTIONS /products/123 HTTP/1.1
Origin: https://frontend.com
Access-Control-Request-Method: DELETE
Access-Control-Request-Headers: authorization
```

**Step 2: Server Responds (204 No Content)**
```http
HTTP/1.1 204 No Content
Access-Control-Allow-Origin: https://frontend.com
Access-Control-Allow-Methods: GET, POST, PUT, DELETE
Access-Control-Allow-Headers: authorization
```

**Step 3: Browser Sends Actual Request (DELETE)**
```http
DELETE /products/123 HTTP/1.1
Authorization: Bearer token123
```

**Step 4: Server Responds**
```http
HTTP/1.1 200 OK
Access-Control-Allow-Origin: https://frontend.com
```

---

### Common Mistakes Leading to CORS Errors

#### ❌ Mistake 1: CORS Middleware After Authentication

```csharp
app.UseAuthentication();
app.UseCors("MyPolicy"); // Bug: Too late — preflight already blocked
```

**Fix:** Move `UseCors()` **before** authentication.

---

#### ❌ Mistake 2: Not Allowing OPTIONS Method

```csharp
options.AddPolicy("MyPolicy", policy =>
{
    policy.WithOrigins("https://frontend.com")
          .WithMethods("GET", "POST"); // Bug: Missing OPTIONS
});
```

**Fix:** Use `.AllowAnyMethod()` or explicitly include OPTIONS.

---

#### ❌ Mistake 3: Not Including Required Headers

```csharp
options.AddPolicy("MyPolicy", policy =>
{
    policy.WithOrigins("https://frontend.com")
          .WithHeaders("Content-Type"); // Bug: Missing Authorization
});
```

**Fix:** Use `.AllowAnyHeader()` or explicitly include all needed headers.

---

## CORS with Authentication

### JWT + CORS

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowJWT", policy =>
    {
        policy.WithOrigins("https://frontend.com")
              .AllowAnyMethod()
              .AllowAnyHeader(); // Includes Authorization header
    });
});
```

**Frontend Code:**
```javascript
fetch('https://api.example.com/products', {
  headers: {
    'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'
  }
});
```

---

### Cookies + Credentials

When using cookies for authentication, you must enable credentials:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCredentials", policy =>
    {
        policy.WithOrigins("https://frontend.com")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // CRITICAL for cookies
    });
});
```

**Frontend Code:**
```javascript
fetch('https://api.example.com/products', {
  credentials: 'include' // Send cookies
});
```

**Important:**
- `AllowCredentials()` requires a **specific origin** (cannot use `AllowAnyOrigin()`)
- Frontend must set `credentials: 'include'`

---

## Real-World Use Cases

### 1. SPA (Angular/React/Vue) Calling Web API

**Scenario:**
- Frontend: `http://localhost:4200` (Angular dev server)
- Backend: `https://localhost:5001` (ASP.NET Core API)

**Solution:**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

app.UseCors("AllowAngularApp");
```

---

### 2. Multiple Frontend Environments

**Scenario:**
- Development: `http://localhost:3000`
- Staging: `https://staging.myapp.com`
- Production: `https://myapp.com`

**Solution:**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMultipleOrigins", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",
                "https://staging.myapp.com",
                "https://myapp.com"
              )
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
```

---

### 3. Dynamic Origin Validation

**Scenario:** Allow origins from a database or configuration.

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("DynamicOrigins", policy =>
    {
        policy.SetIsOriginAllowed(origin =>
        {
            // Custom logic: Check database or config
            var allowedOrigins = new[] { "https://app1.com", "https://app2.com" };
            return allowedOrigins.Contains(origin);
        })
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});
```

---

### 4. Public API (Allow All Origins)

**Scenario:** Public API with no authentication (weather data, news feeds).

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("PublicAPI", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
        // Note: Cannot use AllowCredentials() with AllowAnyOrigin()
    });
});
```

---

## Common Mistakes

### ❌ Mistake 1: Using `AllowAnyOrigin()` with `AllowCredentials()`

```csharp
options.AddPolicy("MyPolicy", policy =>
{
    policy.AllowAnyOrigin()
          .AllowCredentials(); // ERROR: Cannot use both!
});
```

**Error:**
```
System.InvalidOperationException: The CORS protocol does not allow specifying a wildcard (any) origin and credentials at the same time.
```

**Fix:** Use specific origins:
```csharp
policy.WithOrigins("https://frontend.com")
      .AllowCredentials();
```

---

### ❌ Mistake 2: Incorrect Middleware Order

```csharp
app.UseAuthorization();
app.UseCors("MyPolicy"); // Bug: CORS too late
```

**Symptom:** Preflight requests return `401 Unauthorized` instead of `204 No Content`.

**Fix:** Move `UseCors()` before authorization.

---

### ❌ Mistake 3: Confusing CORS Errors with API Errors

**CORS Error (Browser Console):**
```
Access to fetch at 'https://api.example.com/products' from origin 'https://frontend.com'
has been blocked by CORS policy: No 'Access-Control-Allow-Origin' header is present.
```

**Common Misunderstanding:** Developers think the API is broken.

**Reality:**
- The API works fine (you can test with Postman)
- CORS is a **browser security feature**
- Server must send correct CORS headers

---

### ❌ Mistake 4: Not Testing Preflight Requests

Developers test `GET` (simple request) and forget about `DELETE` (preflight request).

**Symptom:** `GET` works, but `DELETE` fails.

**Fix:** Test with methods that trigger preflight (`PUT`, `DELETE`, `PATCH`).

---

### ❌ Mistake 5: Overly Permissive Policies in Production

```csharp
// Bug: Allows ANY origin in production!
options.AddPolicy("AllowAll", policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});
```

**Risk:** Anyone can call your API from any website.

**Fix:** Use specific origins in production.

---

## Common Interview Questions

### Q1: What is CORS?

**Answer:** CORS (Cross-Origin Resource Sharing) is a browser security mechanism that allows servers to specify which origins (domains) are permitted to access their resources. It relaxes the Same-Origin Policy by using HTTP headers like `Access-Control-Allow-Origin`.

---

### Q2: What is a preflight request?

**Answer:** A preflight request is an automatic **OPTIONS** request sent by the browser before the actual request to check if the server allows the operation. It happens for non-simple requests (e.g., `DELETE`, `PUT`, custom headers, `application/json`).

---

### Q3: Why does a CORS preflight request return `204 No Content`?

**Answer:** `204 No Content` indicates that the preflight check succeeded, and the browser can proceed with the actual request. The preflight response has no body—only CORS headers.

---

### Q4: Where should `UseCors()` be placed in the middleware pipeline?

**Answer:** `UseCors()` must be placed **after** `UseRouting()` and **before** `UseAuthentication()` / `UseAuthorization()`. If placed after authentication, preflight requests will fail.

---

### Q5: Can you use `AllowAnyOrigin()` with `AllowCredentials()`?

**Answer:** No. The CORS protocol does not allow using a wildcard origin (`*`) with credentials. You must specify explicit origins when using `AllowCredentials()`.

---

### Q6: Why does CORS work in Postman but fail in the browser?

**Answer:** Postman is not a browser—it doesn't enforce CORS. CORS is a browser security feature. The API must send correct CORS headers for browsers to allow the request.

---

### Q7: What happens if `Access-Control-Allow-Origin` doesn't match the request origin?

**Answer:** The browser blocks the response, and JavaScript cannot access the data. The frontend gets a CORS error, even though the API successfully processed the request.

---

### Q8: How do you allow multiple origins in CORS?

**Answer:**
```csharp
policy.WithOrigins("https://app1.com", "https://app2.com")
```
Or use `SetIsOriginAllowed()` for dynamic validation.

---

### Q9: What is the difference between `EnableCors` and `DisableCors` attributes?

**Answer:**
- `[EnableCors("PolicyName")]` applies a CORS policy to a controller or action
- `[DisableCors]` disables CORS for a specific action (useful when a controller has CORS enabled but one action should not)

---

### Q10: Why does `DELETE` fail but `GET` works?

**Answer:** `GET` is a simple request (no preflight). `DELETE` triggers a preflight request. If CORS is misconfigured (e.g., middleware order, missing OPTIONS), the preflight fails, so the browser never sends the actual `DELETE` request.

---

## Summary

- **CORS** is a browser security mechanism that uses HTTP headers to allow servers to specify which origins can access their resources, overcoming the Same-Origin Policy
- **Preflight requests** (OPTIONS) are sent automatically by browsers for non-simple requests (PUT, DELETE, custom headers, application/json) to check if the server allows the operation
- **Middleware order is critical** — `UseCors()` must be placed **after** `UseRouting()` and **before** `UseAuthentication()` / `UseAuthorization()`, or preflight requests will fail
- **Common CORS headers** include `Access-Control-Allow-Origin` (which origins), `Access-Control-Allow-Methods` (which HTTP methods), `Access-Control-Allow-Headers` (which headers), and `Access-Control-Allow-Credentials` (cookies allowed)
- **Cannot use `AllowAnyOrigin()` with `AllowCredentials()`** — when allowing credentials (cookies, auth), you must specify explicit origins, not a wildcard
- **CORS errors appear in the browser, not in Postman** — CORS is a browser security feature, so testing with Postman doesn't validate CORS configuration
- **Common mistakes** include incorrect middleware order (CORS after authentication), using overly permissive policies in production (`AllowAnyOrigin`), and confusing CORS errors with API failures

