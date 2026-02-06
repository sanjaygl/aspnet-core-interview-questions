# HTTP Status Codes - Complete Guide for REST APIs

## Table of Contents
- [Introduction](#introduction)
- [Status Code Series Overview](#status-code-series-overview)
- [1xx - Informational Responses](#1xx---informational-responses)
- [2xx - Successful Responses](#2xx---successful-responses)
- [3xx - Redirection Messages](#3xx---redirection-messages)
- [4xx - Client Error Responses](#4xx---client-error-responses)
- [5xx - Server Error Responses](#5xx---server-error-responses)
- [Common Status Codes Reference Table](#common-status-codes-reference-table)
- [REST API Best Practices](#rest-api-best-practices)
- [ASP.NET Core Implementation](#aspnet-core-implementation)
- [Do's and Don'ts](#dos-and-donts)
- [Quick Reference Summary](#quick-reference-summary)

---

## Introduction

### What are HTTP Status Codes?

HTTP status codes are **three-digit numeric codes** returned by a server in response to a client's HTTP request. They indicate whether the request was successful, encountered an error, or requires further action.

**Structure**: `XXX` where:
- First digit: Response class (1-5)
- Last two digits: Specific status within that class

**Example**: `404 Not Found`
- `4` = Client Error
- `04` = Specific error (Resource Not Found)

### Why Are They Important in REST APIs?

1. **Clear Communication**: Instantly convey request outcome without parsing response body
2. **Standardization**: Universal language understood by all HTTP clients
3. **Error Handling**: Enable proper client-side error handling and retry logic
4. **Debugging**: Simplify troubleshooting and logging
5. **API Contract**: Form part of the API contract and documentation
6. **Best Practices**: Following standards improves API quality and developer experience

---

## Status Code Series Overview

HTTP status codes are divided into **five classes**, each serving a distinct purpose:

| Series | Category | Purpose |
|--------|----------|---------|
| **1xx** | Informational | Request received, continuing process |
| **2xx** | Success | Request successfully received, understood, and accepted |
| **3xx** | Redirection | Further action needed to complete the request |
| **4xx** | Client Error | Request contains bad syntax or cannot be fulfilled |
| **5xx** | Server Error | Server failed to fulfill a valid request |

### Quick Mental Model

- **1xx**: "Hold on, I'm processing..."
- **2xx**: "Success! Here's what you asked for."
- **3xx**: "Go somewhere else for this."
- **4xx**: "You made a mistake."
- **5xx**: "I made a mistake."

---

## 1xx - Informational Responses

**Purpose**: Indicate that the request was received and the server is continuing to process it.

### Common 1xx Status Codes

| Code | Name | Description | REST API Use Case |
|------|------|-------------|-------------------|
| **100** | Continue | Initial part of request received; client should continue | Large file uploads with `Expect: 100-continue` header |
| **101** | Switching Protocols | Server switching protocols per client request | Upgrading HTTP to WebSocket |
| **102** | Processing (WebDAV) | Server received request but no response yet | Long-running operations |

### Key Points

- **Rarely used** in typical REST APIs
- More common in streaming, WebSocket upgrades, or large file transfers
- Most modern APIs skip directly to 2xx or 4xx/5xx responses

### Example Scenario

```http
POST /api/upload HTTP/1.1
Expect: 100-continue
Content-Length: 1048576

# Server responds:
HTTP/1.1 100 Continue

# Client then sends the full request body
```

---

## 2xx - Successful Responses

**Purpose**: Indicate that the client's request was successfully received, understood, and accepted.

### Common 2xx Status Codes

| Code | Name | Description | REST API Use Case |
|------|------|-------------|-------------------|
| **200** | OK | Request succeeded | GET, PUT, PATCH requests with response body |
| **201** | Created | New resource created successfully | POST requests creating new resources |
| **202** | Accepted | Request accepted but not yet processed | Async operations, background jobs |
| **204** | No Content | Success but no content to return | DELETE, PUT requests with no response body |
| **206** | Partial Content | Partial resource returned | Range requests, pagination, streaming |

### Detailed Breakdown

#### 200 OK
**When to use**: General success response with data

```csharp
// GET request - Retrieve user
[HttpGet("{id}")]
public IActionResult GetUser(int id)
{
    var user = _userService.GetById(id);
    return Ok(user); // 200 OK with user data
}

// PUT request - Update existing resource
[HttpPut("{id}")]
public IActionResult UpdateUser(int id, UserDto user)
{
    var updated = _userService.Update(id, user);
    return Ok(updated); // 200 OK with updated data
}
```

#### 201 Created
**When to use**: New resource successfully created

```csharp
[HttpPost]
public IActionResult CreateUser(UserDto user)
{
    var created = _userService.Create(user);
    return CreatedAtAction(
        nameof(GetUser), 
        new { id = created.Id }, 
        created
    ); // 201 Created with Location header
}
```

**Best Practice**: Include `Location` header pointing to new resource

```http
HTTP/1.1 201 Created
Location: /api/users/123
Content-Type: application/json

{
  "id": 123,
  "name": "John Doe"
}
```

#### 202 Accepted
**When to use**: Request accepted but processing not complete

```csharp
[HttpPost("process-report")]
public IActionResult ProcessReport(ReportRequest request)
{
    var jobId = _reportService.QueueReport(request);
    return Accepted(new { jobId, status = "processing" }); 
    // 202 Accepted
}
```

#### 204 No Content
**When to use**: Success but no content to return

```csharp
[HttpDelete("{id}")]
public IActionResult DeleteUser(int id)
{
    _userService.Delete(id);
    return NoContent(); // 204 No Content
}

[HttpPut("{id}")]
public IActionResult UpdateUserNoReturn(int id, UserDto user)
{
    _userService.Update(id, user);
    return NoContent(); // 204 when client doesn't need updated data
}
```

#### 206 Partial Content
**When to use**: Returning partial data (pagination, range requests)

```csharp
[HttpGet]
public IActionResult GetUsers([FromQuery] int page, [FromQuery] int size)
{
    var users = _userService.GetPaged(page, size);
    Response.Headers.Add("Content-Range", $"users {page*size}-{(page+1)*size}/{users.Total}");
    return Ok(users.Items); // Could use 206 for range requests
}
```

---

## 3xx - Redirection Messages

**Purpose**: Indicate that the client must take additional action to complete the request.

### Common 3xx Status Codes

| Code | Name | Description | REST API Use Case |
|------|------|-------------|-------------------|
| **301** | Moved Permanently | Resource permanently moved to new URL | API versioning, resource relocation |
| **302** | Found | Temporary redirect | Temporary endpoint changes |
| **303** | See Other | Redirect to GET after POST | POST-Redirect-GET pattern |
| **304** | Not Modified | Resource not modified since last request | Caching with ETags |
| **307** | Temporary Redirect | Temporary redirect preserving method | Maintenance mode |
| **308** | Permanent Redirect | Permanent redirect preserving method | API endpoint migration |

### Detailed Breakdown

#### 301 Moved Permanently
**When to use**: Resource permanently moved

```csharp
[HttpGet("old-endpoint")]
public IActionResult OldEndpoint()
{
    return RedirectPermanent("/api/v2/new-endpoint"); // 301
}
```

```http
HTTP/1.1 301 Moved Permanently
Location: /api/v2/users
```

#### 302 Found / 307 Temporary Redirect
**When to use**: Temporary redirect

```csharp
[HttpGet("maintenance")]
public IActionResult CheckMaintenance()
{
    if (_maintenanceMode)
        return RedirectPreserveMethod("/api/status"); // 307
    return Ok();
}
```

#### 304 Not Modified
**When to use**: Conditional requests with caching

```csharp
[HttpGet("{id}")]
public IActionResult GetUser(int id, [FromHeader(Name = "If-None-Match")] string etag)
{
    var user = _userService.GetById(id);
    var currentEtag = GenerateEtag(user);
    
    if (etag == currentEtag)
        return StatusCode(304); // 304 Not Modified
        
    Response.Headers.Add("ETag", currentEtag);
    return Ok(user);
}
```

### Key Points

- **Less common** in pure REST APIs
- More prevalent in web applications
- Important for **caching strategies** (304)
- Useful for **API versioning** (301, 308)

---

## 4xx - Client Error Responses

**Purpose**: Indicate that the client sent an invalid request.

### Common 4xx Status Codes

| Code | Name | Description | REST API Use Case |
|------|------|-------------|-------------------|
| **400** | Bad Request | Malformed request syntax | Invalid JSON, missing required fields |
| **401** | Unauthorized | Authentication required or failed | Missing/invalid authentication token |
| **403** | Forbidden | Authenticated but not authorized | Insufficient permissions |
| **404** | Not Found | Resource doesn't exist | Invalid resource ID |
| **405** | Method Not Allowed | HTTP method not supported | POST on read-only endpoint |
| **406** | Not Acceptable | Cannot produce acceptable response | Unsupported Accept header format |
| **408** | Request Timeout | Request took too long | Client timeout |
| **409** | Conflict | Request conflicts with current state | Duplicate resource, version conflict |
| **410** | Gone | Resource permanently deleted | Soft-deleted resources |
| **415** | Unsupported Media Type | Request content type not supported | Wrong Content-Type header |
| **422** | Unprocessable Entity | Syntax correct but semantically invalid | Validation errors |
| **429** | Too Many Requests | Rate limit exceeded | API throttling |

### Detailed Breakdown

#### 400 Bad Request
**When to use**: General client error, malformed request

```csharp
[HttpPost]
public IActionResult CreateUser([FromBody] UserDto user)
{
    if (user == null)
        return BadRequest("User data is required"); // 400
        
    if (string.IsNullOrEmpty(user.Email))
        return BadRequest(new { error = "Email is required" });
        
    return Ok();
}
```

**Use cases**:
- Invalid JSON syntax
- Missing required parameters
- General validation failures

#### 401 Unauthorized
**When to use**: Authentication missing or invalid

```csharp
[Authorize]
[HttpGet("profile")]
public IActionResult GetProfile()
{
    // If no valid token, ASP.NET Core returns 401 automatically
    var user = User.Identity.Name;
    return Ok(new { user });
}
```

```http
HTTP/1.1 401 Unauthorized
WWW-Authenticate: Bearer realm="API"

{
  "error": "Invalid or missing authentication token"
}
```

**Key distinction**: "You need to log in" or "Your credentials are wrong"

#### 403 Forbidden
**When to use**: Authenticated but lacks permission

```csharp
[Authorize(Roles = "Admin")]
[HttpDelete("{id}")]
public IActionResult DeleteUser(int id)
{
    // If user is authenticated but not Admin, returns 403
    _userService.Delete(id);
    return NoContent();
}

// Custom authorization
[HttpPut("{id}")]
public IActionResult UpdateUser(int id, UserDto user)
{
    var currentUserId = GetCurrentUserId();
    if (id != currentUserId && !User.IsInRole("Admin"))
        return Forbid(); // 403 Forbidden
        
    _userService.Update(id, user);
    return Ok();
}
```

**Key distinction**: "I know who you are, but you can't do this"

#### 404 Not Found
**When to use**: Resource doesn't exist

```csharp
[HttpGet("{id}")]
public IActionResult GetUser(int id)
{
    var user = _userService.GetById(id);
    if (user == null)
        return NotFound($"User with ID {id} not found"); // 404
        
    return Ok(user);
}
```

#### 405 Method Not Allowed
**When to use**: HTTP method not supported for endpoint

```http
POST /api/users/123/readonly

HTTP/1.1 405 Method Not Allowed
Allow: GET, HEAD
```

ASP.NET Core handles this automatically based on route configuration.

#### 409 Conflict
**When to use**: Request conflicts with current resource state

```csharp
[HttpPost]
public IActionResult CreateUser(UserDto user)
{
    if (_userService.EmailExists(user.Email))
        return Conflict(new { 
            error = "User with this email already exists" 
        }); // 409
        
    var created = _userService.Create(user);
    return Created($"/api/users/{created.Id}", created);
}

[HttpPut("{id}")]
public IActionResult UpdateUser(int id, UserDto user, [FromHeader(Name = "If-Match")] string etag)
{
    if (!_userService.VersionMatches(id, etag))
        return Conflict(new { 
            error = "Resource has been modified by another user" 
        }); // 409 - Optimistic concurrency conflict
        
    _userService.Update(id, user);
    return Ok();
}
```

#### 415 Unsupported Media Type
**When to use**: Content-Type not supported

```csharp
[HttpPost]
[Consumes("application/json")]
public IActionResult CreateUser([FromBody] UserDto user)
{
    // If client sends XML with Content-Type: application/xml
    // ASP.NET Core returns 415 automatically
    return Created($"/api/users/{user.Id}", user);
}
```

#### 422 Unprocessable Entity
**When to use**: Request is well-formed but has semantic errors

```csharp
[HttpPost]
public IActionResult CreateUser([FromBody] UserDto user)
{
    if (!ModelState.IsValid)
        return UnprocessableEntity(ModelState); // 422
        
    // Business rule validation
    if (user.Age < 18)
        return UnprocessableEntity(new { 
            error = "User must be at least 18 years old" 
        });
        
    var created = _userService.Create(user);
    return Created($"/api/users/{created.Id}", created);
}
```

#### 429 Too Many Requests
**When to use**: Rate limit exceeded

```csharp
// Using ASP.NET Core Rate Limiting
[EnableRateLimiting("fixed")]
[HttpGet]
public IActionResult GetUsers()
{
    // If rate limit exceeded, middleware returns 429 automatically
    return Ok(_userService.GetAll());
}
```

```http
HTTP/1.1 429 Too Many Requests
Retry-After: 3600

{
  "error": "Rate limit exceeded. Try again in 1 hour."
}
```

---

## 5xx - Server Error Responses

**Purpose**: Indicate that the server encountered an error or is incapable of performing the request.

### Common 5xx Status Codes

| Code | Name | Description | REST API Use Case |
|------|------|-------------|-------------------|
| **500** | Internal Server Error | Generic server error | Unhandled exceptions, unexpected errors |
| **501** | Not Implemented | Server doesn't support functionality | Feature not yet implemented |
| **502** | Bad Gateway | Invalid response from upstream server | Gateway/proxy errors |
| **503** | Service Unavailable | Server temporarily unavailable | Maintenance, overload |
| **504** | Gateway Timeout | Upstream server timeout | Proxy timeout waiting for response |

### Detailed Breakdown

#### 500 Internal Server Error
**When to use**: Unexpected server error

```csharp
[HttpGet("{id}")]
public IActionResult GetUser(int id)
{
    try
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error retrieving user {UserId}", id);
        return StatusCode(500, new { 
            error = "An unexpected error occurred" 
        });
    }
}

// Better: Use global exception handler
public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception occurred");
        
        httpContext.Response.StatusCode = 500;
        await httpContext.Response.WriteAsJsonAsync(new
        {
            error = "An unexpected error occurred",
            // Include details only in development
            details = _env.IsDevelopment() ? exception.Message : null
        });
        
        return true;
    }
}
```

**Best practice**: Never expose internal error details in production

```http
# Production
HTTP/1.1 500 Internal Server Error
{
  "error": "An unexpected error occurred",
  "requestId": "abc123"
}

# Development (optional)
HTTP/1.1 500 Internal Server Error
{
  "error": "NullReferenceException in UserService.GetById",
  "stackTrace": "..."
}
```

#### 501 Not Implemented
**When to use**: Functionality not yet implemented

```csharp
[HttpPost("advanced-search")]
public IActionResult AdvancedSearch(SearchCriteria criteria)
{
    return StatusCode(501, new { 
        error = "Advanced search not implemented yet" 
    }); // 501
}
```

#### 503 Service Unavailable
**When to use**: Server temporarily unavailable

```csharp
public class MaintenanceMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (_maintenanceMode)
        {
            context.Response.StatusCode = 503;
            context.Response.Headers.Add("Retry-After", "3600");
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Service temporarily unavailable for maintenance",
                retryAfter = "1 hour"
            });
            return;
        }
        
        await _next(context);
    }
}
```

```http
HTTP/1.1 503 Service Unavailable
Retry-After: 3600

{
  "error": "Service temporarily unavailable for maintenance"
}
```

#### 502 Bad Gateway / 504 Gateway Timeout
**When to use**: Issues with upstream services

```csharp
[HttpGet("external-data")]
public async Task<IActionResult> GetExternalData()
{
    try
    {
        var response = await _httpClient.GetAsync("https://api.external.com/data");
        
        if (!response.IsSuccessStatusCode)
            return StatusCode(502, new { 
                error = "Error communicating with external service" 
            }); // 502
            
        var data = await response.Content.ReadAsStringAsync();
        return Ok(data);
    }
    catch (TaskCanceledException)
    {
        return StatusCode(504, new { 
            error = "External service timeout" 
        }); // 504
    }
}
```

### Key Points

- **Always log** server errors for debugging
- **Never expose** sensitive error details to clients in production
- **Include request ID** for error tracking
- **Use monitoring** to track 5xx error rates
- **Not the client's fault** - they can retry the same request

---

## Common Status Codes Reference Table

| Code | Name | When to Use | Example Scenario |
|------|------|-------------|------------------|
| **200** | OK | Successful GET, PUT, PATCH | Retrieve or update user profile |
| **201** | Created | Successful POST creating resource | Create new user account |
| **202** | Accepted | Async operation started | Submit batch processing job |
| **204** | No Content | Successful DELETE or PUT with no body | Delete user, update without response |
| **400** | Bad Request | Malformed request, general validation error | Invalid JSON, missing required field |
| **401** | Unauthorized | Authentication required or failed | Missing/invalid JWT token |
| **403** | Forbidden | Authenticated but no permission | Non-admin trying admin action |
| **404** | Not Found | Resource doesn't exist | User ID not found in database |
| **409** | Conflict | Duplicate or version conflict | Email already exists |
| **422** | Unprocessable Entity | Valid syntax but semantic error | Age validation, business rule violation |
| **429** | Too Many Requests | Rate limit exceeded | API call limit reached |
| **500** | Internal Server Error | Unexpected server error | Database connection failure |
| **503** | Service Unavailable | Temporary server unavailability | Maintenance mode, server overload |

---

## REST API Best Practices

### 200 OK vs 201 Created

| Aspect | 200 OK | 201 Created |
|--------|--------|-------------|
| **Use case** | Resource retrieved or updated | New resource created |
| **HTTP Methods** | GET, PUT, PATCH | POST |
| **Location Header** | Not required | Should include URL of new resource |
| **Response Body** | Typically includes resource | May include created resource |

```csharp
// 200 OK - Update existing
[HttpPut("{id}")]
public IActionResult UpdateUser(int id, UserDto user)
{
    var updated = _userService.Update(id, user);
    return Ok(updated); // 200 with updated resource
}

// 201 Created - Create new
[HttpPost]
public IActionResult CreateUser(UserDto user)
{
    var created = _userService.Create(user);
    return CreatedAtAction(
        nameof(GetUser), 
        new { id = created.Id }, 
        created
    ); // 201 with Location header
}
```

### 400 Bad Request vs 422 Unprocessable Entity

| Aspect | 400 Bad Request | 422 Unprocessable Entity |
|--------|-----------------|--------------------------|
| **When** | Syntax errors, malformed request | Well-formed but semantically invalid |
| **Examples** | Invalid JSON, wrong data type | Business rule violations, validation errors |
| **Recovery** | Fix request format | Fix data values |

```csharp
[HttpPost]
public IActionResult CreateUser([FromBody] UserDto user)
{
    // 400 - Malformed request
    if (user == null)
        return BadRequest("Request body is required");
    
    // 422 - Semantic validation
    if (!ModelState.IsValid)
        return UnprocessableEntity(ModelState);
    
    // 422 - Business rule violation
    if (user.Age < 18)
        return UnprocessableEntity(new { 
            errors = new { Age = "Must be at least 18" } 
        });
    
    return Created($"/api/users/{user.Id}", user);
}
```

**Rule of thumb**:
- **400**: "I can't parse your request"
- **422**: "I parsed it, but the data is invalid"

### 401 Unauthorized vs 403 Forbidden

| Aspect | 401 Unauthorized | 403 Forbidden |
|--------|------------------|---------------|
| **Meaning** | Authentication missing/invalid | Authenticated but insufficient permissions |
| **Message** | "Who are you?" | "I know who you are, but you can't do this" |
| **Action** | Login or provide valid credentials | Request permission or use different account |
| **WWW-Authenticate** | Should include header | Not required |

```csharp
// 401 - No authentication
[Authorize] // Returns 401 if no valid token
[HttpGet("profile")]
public IActionResult GetProfile()
{
    return Ok(new { user = User.Identity.Name });
}

// 403 - Insufficient permissions
[HttpDelete("{id}")]
public IActionResult DeleteUser(int id)
{
    if (!User.IsInRole("Admin"))
        return Forbid(); // 403 - authenticated but not admin
    
    _userService.Delete(id);
    return NoContent();
}
```

### 204 No Content vs 200 OK

| Aspect | 204 No Content | 200 OK |
|--------|----------------|--------|
| **Response Body** | Empty (no content) | Contains data |
| **Use case** | DELETE, PUT when client doesn't need data back | GET, PUT when returning data |
| **Performance** | Slightly better (no serialization) | Standard |

```csharp
// 204 - Client doesn't need response
[HttpDelete("{id}")]
public IActionResult DeleteUser(int id)
{
    _userService.Delete(id);
    return NoContent(); // 204
}

// 200 - Client needs updated data
[HttpPut("{id}")]
public IActionResult UpdateUser(int id, UserDto user)
{
    var updated = _userService.Update(id, user);
    return Ok(updated); // 200 with updated resource
}

// 204 - Client just needs confirmation
[HttpPut("{id}/confirm")]
public IActionResult ConfirmUser(int id)
{
    _userService.Confirm(id);
    return NoContent(); // 204 - confirmation only
}
```

**Rule of thumb**: Use 204 when the client only needs to know the operation succeeded.

---

## ASP.NET Core Implementation

### IActionResult Return Types

ASP.NET Core provides built-in helper methods for common status codes:

```csharp
public class UsersController : ControllerBase
{
    // 2xx Success
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(data);                              // 200 OK
        return Created(uri, data);                    // 201 Created
        return CreatedAtAction(action, routeValues, data); // 201 with Location
        return Accepted();                            // 202 Accepted
        return NoContent();                           // 204 No Content
    }
    
    // 3xx Redirection
    [HttpGet("redirect")]
    public IActionResult Redirect()
    {
        return Redirect(url);                         // 302 Found
        return RedirectPermanent(url);                // 301 Moved Permanently
        return RedirectToAction(action);              // 302 to action
        return RedirectToActionPermanent(action);     // 301 to action
    }
    
    // 4xx Client Errors
    [HttpPost]
    public IActionResult Create([FromBody] UserDto user)
    {
        return BadRequest();                          // 400 Bad Request
        return BadRequest(error);                     // 400 with error message
        return BadRequest(ModelState);                // 400 with validation errors
        return Unauthorized();                        // 401 Unauthorized
        return Forbid();                              // 403 Forbidden
        return NotFound();                            // 404 Not Found
        return NotFound(message);                     // 404 with message
        return Conflict();                            // 409 Conflict
        return UnprocessableEntity(ModelState);       // 422 Unprocessable Entity
    }
    
    // 5xx Server Errors
    [HttpGet("error")]
    public IActionResult Error()
    {
        return StatusCode(500);                       // 500 Internal Server Error
        return StatusCode(500, error);                // 500 with error object
        return StatusCode(503);                       // 503 Service Unavailable
    }
    
    // Generic status code
    public IActionResult Custom()
    {
        return StatusCode(429, new { error = "Too many requests" });
    }
}
```

### Complete REST API Example

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>200 OK with user list</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users); // 200 OK
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>200 OK with user data or 404 Not Found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        
        if (user == null)
            return NotFound(new { error = $"User with ID {id} not found" }); // 404
        
        return Ok(user); // 200 OK
    }

    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="user">User data</param>
    /// <returns>201 Created with new user data</returns>
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Create([FromBody] CreateUserDto user)
    {
        // 400 - Bad Request (malformed)
        if (user == null)
            return BadRequest(new { error = "User data is required" });
        
        // 422 - Validation errors
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        // 409 - Conflict (duplicate)
        if (_userService.EmailExists(user.Email))
            return Conflict(new { error = "User with this email already exists" });
        
        // Business validation
        if (user.Age < 18)
            return UnprocessableEntity(new { 
                errors = new { Age = "User must be at least 18 years old" } 
            });
        
        var created = _userService.Create(user);
        
        // 201 Created with Location header
        return CreatedAtAction(
            nameof(GetById), 
            new { id = created.Id }, 
            created
        );
    }

    /// <summary>
    /// Update existing user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="user">Updated user data</param>
    /// <returns>200 OK with updated data or 404 Not Found</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public IActionResult Update(int id, [FromBody] UpdateUserDto user)
    {
        // Check if exists
        if (!_userService.Exists(id))
            return NotFound(new { error = $"User with ID {id} not found" }); // 404
        
        // Validation
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState); // 422
        
        var updated = _userService.Update(id, user);
        return Ok(updated); // 200 OK
    }

    /// <summary>
    /// Partially update user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="patchDoc">JSON Patch document</param>
    /// <returns>200 OK or 404 Not Found</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Patch(int id, [FromBody] JsonPatchDocument<UserDto> patchDoc)
    {
        var user = _userService.GetById(id);
        
        if (user == null)
            return NotFound(); // 404
        
        patchDoc.ApplyTo(user, ModelState);
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState); // 422
        
        var updated = _userService.Update(id, user);
        return Ok(updated); // 200 OK
    }

    /// <summary>
    /// Delete user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>204 No Content or 404 Not Found</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        if (!_userService.Exists(id))
            return NotFound(new { error = $"User with ID {id} not found" }); // 404
        
        _userService.Delete(id);
        return NoContent(); // 204 No Content
    }

    /// <summary>
    /// Search users
    /// </summary>
    /// <param name="query">Search query</param>
    /// <returns>200 OK with search results</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public IActionResult Search([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest(new { error = "Search query is required" }); // 400
        
        var results = _userService.Search(query);
        return Ok(results); // 200 OK
    }

    /// <summary>
    /// Activate user account
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>204 No Content</returns>
    [HttpPost("{id}/activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Activate(int id)
    {
        var user = _userService.GetById(id);
        
        if (user == null)
            return NotFound(); // 404
        
        if (user.IsActive)
            return Conflict(new { error = "User is already active" }); // 409
        
        _userService.Activate(id);
        return NoContent(); // 204
    }
}
```

### Using ProducesResponseType Attribute

Document expected responses for API documentation (Swagger):

```csharp
[HttpGet("{id}")]
[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public IActionResult GetById(int id)
{
    // Implementation
}
```

### Global Error Handling

```csharp
// Program.cs
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

// GlobalExceptionHandler.cs
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger, 
        IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception occurred");

        var statusCode = exception switch
        {
            ArgumentNullException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            InvalidOperationException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(new
        {
            error = exception.Message,
            type = exception.GetType().Name,
            statusCode = statusCode,
            // Only include stack trace in development
            stackTrace = _env.IsDevelopment() ? exception.StackTrace : null
        }, cancellationToken);

        return true;
    }
}
```

### Custom Error Response Model

```csharp
public class ErrorResponse
{
    public string Error { get; set; }
    public int StatusCode { get; set; }
    public string RequestId { get; set; }
    public DateTime Timestamp { get; set; }
    public Dictionary<string, string[]> Errors { get; set; } // For validation errors

    public ErrorResponse(string error, int statusCode)
    {
        Error = error;
        StatusCode = statusCode;
        Timestamp = DateTime.UtcNow;
    }
}

// Usage in controller
[HttpPost]
public IActionResult Create([FromBody] UserDto user)
{
    if (!ModelState.IsValid)
    {
        var errors = ModelState
            .Where(x => x.Value.Errors.Any())
            .ToDictionary(
                x => x.Key,
                x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var errorResponse = new ErrorResponse(
            "Validation failed", 
            StatusCodes.Status422UnprocessableEntity
        )
        {
            Errors = errors,
            RequestId = HttpContext.TraceIdentifier
        };

        return UnprocessableEntity(errorResponse);
    }

    // Create user
    return Created($"/api/users/{user.Id}", user);
}
```

---

## Do's and Don'ts

### ✅ DO's

#### 1. **Use Appropriate Status Codes**
```csharp
// ✅ Correct
[HttpPost]
public IActionResult Create(UserDto user)
{
    var created = _userService.Create(user);
    return CreatedAtAction(nameof(Get), new { id = created.Id }, created); // 201
}

// ❌ Incorrect
[HttpPost]
public IActionResult Create(UserDto user)
{
    var created = _userService.Create(user);
    return Ok(created); // 200 - Should be 201
}
```

#### 2. **Include Location Header for 201 Created**
```csharp
// ✅ Correct
return CreatedAtAction(
    nameof(GetUser), 
    new { id = user.Id }, 
    user
); // Automatically adds Location header

// ❌ Incorrect
return StatusCode(201, user); // Missing Location header
```

#### 3. **Return 204 for DELETE Operations**
```csharp
// ✅ Correct
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    _userService.Delete(id);
    return NoContent(); // 204
}

// ❌ Incorrect
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    _userService.Delete(id);
    return Ok(new { message = "Deleted" }); // 200 - Unnecessary
}
```

#### 4. **Differentiate Between 401 and 403**
```csharp
// ✅ Correct
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    if (!User.Identity.IsAuthenticated)
        return Unauthorized(); // 401 - Not logged in
    
    if (!User.IsInRole("Admin"))
        return Forbid(); // 403 - Logged in but insufficient permissions
    
    _userService.Delete(id);
    return NoContent();
}
```

#### 5. **Use 422 for Validation Errors**
```csharp
// ✅ Correct
if (!ModelState.IsValid)
    return UnprocessableEntity(ModelState); // 422

// ❌ Less specific
if (!ModelState.IsValid)
    return BadRequest(ModelState); // 400 - Works but less precise
```

#### 6. **Include Meaningful Error Messages**
```csharp
// ✅ Correct
return NotFound(new { 
    error = $"User with ID {id} not found",
    requestId = HttpContext.TraceIdentifier
});

// ❌ Incorrect
return NotFound(); // No context
```

#### 7. **Use Problem Details for Consistent Error Format**
```csharp
// ✅ Correct - RFC 7807 Problem Details
return Problem(
    statusCode: 404,
    title: "Resource Not Found",
    detail: $"User with ID {id} does not exist",
    instance: HttpContext.Request.Path
);

// Returns:
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Resource Not Found",
  "status": 404,
  "detail": "User with ID 123 does not exist",
  "instance": "/api/users/123"
}
```

#### 8. **Document Expected Status Codes**
```csharp
// ✅ Correct
/// <summary>
/// Creates a new user
/// </summary>
/// <returns>201 Created with user data</returns>
/// <response code="201">User created successfully</response>
/// <response code="400">Invalid request</response>
/// <response code="409">Email already exists</response>
[HttpPost]
[ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
public IActionResult Create(UserDto user) { }
```

#### 9. **Handle Async Operations with 202**
```csharp
// ✅ Correct
[HttpPost("bulk-import")]
public IActionResult BulkImport(List<UserDto> users)
{
    var jobId = _importService.QueueImport(users);
    return Accepted(new { 
        jobId, 
        status = "processing",
        statusUrl = $"/api/jobs/{jobId}"
    }); // 202
}
```

#### 10. **Use Specific 4xx Codes**
```csharp
// ✅ Correct - Specific codes
if (rateLimitExceeded)
    return StatusCode(429, new { error = "Too many requests" });

if (mediaTypeNotSupported)
    return StatusCode(415, new { error = "Content-Type must be application/json" });

if (emailExists)
    return Conflict(new { error = "Email already exists" });
```

---

### ❌ DON'Ts

#### 1. **Don't Return 200 for Everything**
```csharp
// ❌ Incorrect
[HttpPost]
public IActionResult Create(UserDto user)
{
    if (user == null)
        return Ok(new { error = "User is null" }); // Should be 400
    
    if (_userService.EmailExists(user.Email))
        return Ok(new { error = "Email exists" }); // Should be 409
    
    var created = _userService.Create(user);
    return Ok(created); // Should be 201
}

// ✅ Correct
[HttpPost]
public IActionResult Create(UserDto user)
{
    if (user == null)
        return BadRequest(new { error = "User is null" }); // 400
    
    if (_userService.EmailExists(user.Email))
        return Conflict(new { error = "Email exists" }); // 409
    
    var created = _userService.Create(user);
    return CreatedAtAction(nameof(Get), new { id = created.Id }, created); // 201
}
```

#### 2. **Don't Return Success Status with Error Messages**
```csharp
// ❌ Incorrect
return Ok(new { success = false, error = "User not found" });

// ✅ Correct
return NotFound(new { error = "User not found" }); // 404
```

#### 3. **Don't Expose Internal Error Details in Production**
```csharp
// ❌ Incorrect
catch (Exception ex)
{
    return StatusCode(500, new { 
        error = ex.Message,
        stackTrace = ex.StackTrace,
        source = ex.Source
    });
}

// ✅ Correct
catch (Exception ex)
{
    _logger.LogError(ex, "Error processing request");
    return StatusCode(500, new { 
        error = "An unexpected error occurred",
        requestId = HttpContext.TraceIdentifier
    });
}
```

#### 4. **Don't Use 404 for Business Logic Failures**
```csharp
// ❌ Incorrect
if (user.Age < 18)
    return NotFound(new { error = "User must be 18+" }); // Not a "not found" issue

// ✅ Correct
if (user.Age < 18)
    return UnprocessableEntity(new { error = "User must be 18+" }); // 422
```

#### 5. **Don't Mix Status Codes and Response Bodies Inconsistently**
```csharp
// ❌ Incorrect - Inconsistent format
[HttpGet("{id}")]
public IActionResult Get(int id)
{
    var user = _userService.GetById(id);
    if (user == null)
        return NotFound("Not found"); // String
    return Ok(user); // Object
}

[HttpPost]
public IActionResult Create(UserDto user)
{
    if (user == null)
        return BadRequest(new { message = "Invalid" }); // Different property
    return Ok(user);
}

// ✅ Correct - Consistent format
[HttpGet("{id}")]
public IActionResult Get(int id)
{
    var user = _userService.GetById(id);
    if (user == null)
        return NotFound(new { error = "User not found" });
    return Ok(user);
}

[HttpPost]
public IActionResult Create(UserDto user)
{
    if (user == null)
        return BadRequest(new { error = "User data required" });
    return Created($"/api/users/{user.Id}", user);
}
```

#### 6. **Don't Overuse 500 Internal Server Error**
```csharp
// ❌ Incorrect
[HttpGet("{id}")]
public IActionResult Get(int id)
{
    try
    {
        var user = _userService.GetById(id);
        if (user == null)
            throw new Exception("User not found");
        return Ok(user);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message); // 500 for expected scenario
    }
}

// ✅ Correct
[HttpGet("{id}")]
public IActionResult Get(int id)
{
    var user = _userService.GetById(id);
    if (user == null)
        return NotFound(new { error = $"User {id} not found" }); // 404
    return Ok(user);
}
```

#### 7. **Don't Ignore Content-Type Validation**
```csharp
// ❌ Incorrect - Accept any content type
[HttpPost]
public IActionResult Create([FromBody] UserDto user)
{
    return Created($"/api/users/{user.Id}", user);
}

// ✅ Correct - Specify accepted content types
[HttpPost]
[Consumes("application/json")]
[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
public IActionResult Create([FromBody] UserDto user)
{
    return Created($"/api/users/{user.Id}", user);
}
```

#### 8. **Don't Return Data with 204 No Content**
```csharp
// ❌ Incorrect
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    _userService.Delete(id);
    return NoContent(); // But also sets Response body
}

// 204 means NO content - don't include body
```

#### 9. **Don't Use Custom Status Codes**
```csharp
// ❌ Incorrect
return StatusCode(499, new { error = "Custom error" }); // Non-standard

// ✅ Correct - Use standard codes
return StatusCode(400, new { error = "Invalid request" });
```

#### 10. **Don't Forget to Set Appropriate Headers**
```csharp
// ❌ Incorrect
[HttpPost]
public IActionResult Create(UserDto user)
{
    var created = _userService.Create(user);
    return StatusCode(201, created); // Missing Location header
}

// ✅ Correct
[HttpPost]
public IActionResult Create(UserDto user)
{
    var created = _userService.Create(user);
    return CreatedAtAction(
        nameof(Get), 
        new { id = created.Id }, 
        created
    ); // Includes Location header
}
```

---

## Quick Reference Summary

### One-Line Series Explanations

| Series | One-Line Summary | Interview Note |
|--------|------------------|----------------|
| **1xx** | "Request received, processing continues" | Rarely used in REST APIs |
| **2xx** | "Success - request completed successfully" | Most common for successful operations |
| **3xx** | "Redirect - client must take additional action" | Used for caching and URL changes |
| **4xx** | "Client error - invalid request from client" | Client's fault, client should fix |
| **5xx** | "Server error - server failed to process valid request" | Server's fault, client can retry |

### Most Important Status Codes (Interview Ready)

**Success (2xx)**
- `200 OK` - Standard success response
- `201 Created` - Resource created (POST)
- `204 No Content` - Success with no body (DELETE)

**Client Errors (4xx)**
- `400 Bad Request` - Malformed request
- `401 Unauthorized` - Authentication required
- `403 Forbidden` - Insufficient permissions
- `404 Not Found` - Resource doesn't exist
- `422 Unprocessable Entity` - Validation error

**Server Errors (5xx)**
- `500 Internal Server Error` - Generic server error
- `503 Service Unavailable` - Server overloaded/maintenance

### Common Interview Questions

#### Q: What's the difference between 401 and 403?
**A**: 
- **401 Unauthorized**: "Who are you?" - Authentication missing or invalid
- **403 Forbidden**: "I know who you are, but you can't do this" - Authenticated but lacks permission

#### Q: When should you use 200 vs 201?
**A**:
- **200 OK**: For successful GET, PUT, or PATCH operations
- **201 Created**: Only for POST operations that create new resources (should include Location header)

#### Q: What's the difference between 400 and 422?
**A**:
- **400 Bad Request**: Malformed request syntax (invalid JSON, missing required parameters)
- **422 Unprocessable Entity**: Well-formed request but semantically invalid (validation errors, business rules)

#### Q: When to use 204 No Content?
**A**: When operation succeeded but client doesn't need response data (DELETE, PUT updates where client doesn't need the result)

#### Q: Should you return error details in 500 responses?
**A**: No in production - log details server-side but return generic error to client. In development, you can include details for debugging.

### Status Code Decision Tree

```
Is the request successful?
├─ Yes → 2xx
│  ├─ Creating new resource? → 201 Created (with Location header)
│  ├─ No content to return? → 204 No Content
│  ├─ Async processing? → 202 Accepted
│  └─ Otherwise → 200 OK
│
└─ No → Is it client's fault or server's fault?
   ├─ Client's fault → 4xx
   │  ├─ Not authenticated? → 401 Unauthorized
   │  ├─ Not authorized? → 403 Forbidden
   │  ├─ Resource not found? → 404 Not Found
   │  ├─ Malformed request? → 400 Bad Request
   │  ├─ Validation error? → 422 Unprocessable Entity
   │  ├─ Duplicate/conflict? → 409 Conflict
   │  └─ Rate limited? → 429 Too Many Requests
   │
   └─ Server's fault → 5xx
      ├─ Unexpected error? → 500 Internal Server Error
      ├─ Temporarily unavailable? → 503 Service Unavailable
      └─ Not implemented? → 501 Not Implemented
```

### REST API CRUD Mapping

| Operation | HTTP Method | Success Status | Error Status |
|-----------|-------------|----------------|--------------|
| **Create** | POST | 201 Created | 400, 409, 422 |
| **Read (one)** | GET | 200 OK | 404 |
| **Read (all)** | GET | 200 OK | - |
| **Update (full)** | PUT | 200 OK or 204 | 404, 422 |
| **Update (partial)** | PATCH | 200 OK | 404, 422 |
| **Delete** | DELETE | 204 No Content | 404 |

### Key Takeaways for Interviews

1. **Status codes communicate intent** - Use the right code for the right situation
2. **2xx = Success, 4xx = Client error, 5xx = Server error**
3. **Be consistent** across your API
4. **Document expected responses** in API specifications
5. **Use specific codes** rather than generic ones (422 vs 400, 403 vs 401)
6. **Include helpful error messages** but don't expose sensitive details
7. **Follow REST conventions** for better API usability
8. **201 requires Location header** pointing to the new resource
9. **Don't return 200 for errors** or include error objects with success codes
10. **Log server errors (5xx)** but return generic messages to clients

---

## Conclusion

Understanding and correctly implementing HTTP status codes is crucial for building robust, developer-friendly REST APIs. They provide a universal language for communicating the outcome of API requests, enabling proper error handling, debugging, and client-side logic.

**Key Principles**:
- **Clarity**: Use specific status codes that accurately describe the response
- **Consistency**: Apply status codes uniformly across your API
- **Standards**: Follow HTTP specifications and REST best practices
- **Communication**: Include meaningful error messages when appropriate
- **Security**: Don't expose sensitive information in error responses

By following the guidelines and examples in this guide, you'll create APIs that are:
- Easy to understand and use
- Consistent and predictable
- Properly documented
- Interview-ready for technical discussions

---

**Related Topics**:
- [REST API Best Practices](REST-API-Interview-Questions-Guide.md)
- [ASP.NET Core Web API](ASP.NET%20CORE%20QA.md)
- [Authentication & Authorization](Authentication-Authorization-Guide.md)
- [API Versioning](API-Versioning-Guide.md)

---

*Document Version: 1.0*  
*Last Updated: February 2026*  
*Target Audience: Backend Engineers, API Developers, Interview Preparation*
