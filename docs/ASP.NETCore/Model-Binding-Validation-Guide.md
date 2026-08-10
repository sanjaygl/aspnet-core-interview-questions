# Model Binding & Model Validation in ASP.NET Core – How Data Enters Your API

## Introduction

Model binding and model validation are two critical features in ASP.NET Core that determine how incoming HTTP request data is converted into C# objects and validated before reaching your business logic. Understanding these concepts is essential for building secure, robust APIs. Many production bugs—such as null reference exceptions, invalid data reaching the database, or unexpected 400 errors—stem from misunderstanding how model binding and validation work. This guide explains both concepts with practical examples and real-world use cases to help you write better APIs and answer interview questions confidently.

---

## What is Model Binding?

**Model binding** is the process by which ASP.NET Core automatically extracts data from an HTTP request (URL, query string, request body, headers, form data) and maps it to action method parameters or model properties. Instead of manually parsing the request, ASP.NET Core does this automatically, allowing you to work directly with strongly-typed C# objects.

**Key Points:**
- Converts HTTP request data into C# types (primitives, objects, collections)
- Happens automatically before your action method executes
- Supports multiple binding sources (route, query, body, header, form)
- Reduces boilerplate code and manual parsing

---

## Model Binding Sources

ASP.NET Core provides explicit attributes to specify where data should come from. These are called **binding source attributes**.

### 1. `[FromRoute]` – Data from URL Path

Binds data from route parameters in the URL path.

```csharp
[HttpGet("users/{id}")]
public IActionResult GetUser([FromRoute] int id)
{
    return Ok($"User ID: {id}");
}
```

**Request:**
```
GET /users/42
```

**Result:** `id = 42`

---

### 2. `[FromQuery]` – Data from Query String

Binds data from the URL query string (`?key=value`).

```csharp
[HttpGet("search")]
public IActionResult Search([FromQuery] string query, [FromQuery] int page = 1)
{
    return Ok($"Searching for '{query}' on page {page}");
}
```

**Request:**
```
GET /search?query=laptop&page=2
```

**Result:** `query = "laptop"`, `page = 2`

---

### 3. `[FromBody]` – Data from Request Body (JSON/XML)

Binds data from the HTTP request body, typically JSON in Web APIs.

```csharp
public class CreateUserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
}

[HttpPost("users")]
public IActionResult CreateUser([FromBody] CreateUserRequest request)
{
    return Ok($"User {request.Name} created with email {request.Email}");
}
```

**Request:**
```http
POST /users
Content-Type: application/json

{
  "name": "John Doe",
  "email": "john@example.com"
}
```

**Result:** `request.Name = "John Doe"`, `request.Email = "john@example.com"`

---

### 4. `[FromHeader]` – Data from HTTP Headers

Binds data from HTTP request headers.

```csharp
[HttpGet("profile")]
public IActionResult GetProfile([FromHeader(Name = "X-User-Id")] string userId)
{
    return Ok($"Profile for user: {userId}");
}
```

**Request:**
```http
GET /profile
X-User-Id: 12345
```

**Result:** `userId = "12345"`

---

### 5. `[FromForm]` – Data from HTML Forms

Binds data from form submissions (`application/x-www-form-urlencoded` or `multipart/form-data`).

```csharp
[HttpPost("upload")]
public IActionResult Upload([FromForm] string title, [FromForm] IFormFile file)
{
    return Ok($"Uploaded file: {file.FileName}, Title: {title}");
}
```

**Request:**
```http
POST /upload
Content-Type: multipart/form-data

title=MyDocument&file=<binary-data>
```

**Result:** `title = "MyDocument"`, `file` contains the uploaded file.

---

## Default Model Binding Behavior

When you **don't specify** a binding source attribute, ASP.NET Core follows these rules:

| Parameter Type | Default Binding Source |
|----------------|------------------------|
| Simple types (`int`, `string`, `bool`, etc.) | `[FromRoute]` or `[FromQuery]` |
| Complex types (classes, DTOs) | `[FromBody]` (in Web APIs with `[ApiController]`) |
| `IFormFile` | `[FromForm]` |

### Example: Implicit Binding

```csharp
[HttpGet("products/{id}")]
public IActionResult GetProduct(int id, string category)
{
    // id comes from [FromRoute] (URL path)
    // category comes from [FromQuery] (query string)
    return Ok($"Product ID: {id}, Category: {category}");
}
```

**Request:**
```
GET /products/10?category=electronics
```

**Result:** `id = 10`, `category = "electronics"`

---

## Complex vs Simple Types

### Simple Types (Primitives)

Bound from route or query string by default.

```csharp
[HttpGet("calculate")]
public IActionResult Calculate(int a, int b)
{
    return Ok(a + b);
}
```

**Request:**
```
GET /calculate?a=5&b=10
```

**Result:** `15`

---

### Complex Types (DTOs / Objects)

Bound from request body by default (in Web APIs).

```csharp
public class ProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}

[HttpPost("products")]
public IActionResult CreateProduct(ProductDto product)
{
    // product is automatically bound from JSON body
    return Ok($"Product '{product.Name}' created at ${product.Price}");
}
```

**Request:**
```http
POST /products
Content-Type: application/json

{
  "name": "Laptop",
  "price": 999.99,
  "category": "Electronics"
}
```

**Result:** `product.Name = "Laptop"`, `product.Price = 999.99`, `product.Category = "Electronics"`

---

## What is Model Validation?

**Model validation** is the process of checking whether the data bound to a model meets certain rules (e.g., required fields, length constraints, valid email format) before the action method executes. ASP.NET Core uses **Data Annotations** to define validation rules declaratively on model properties. If validation fails, the framework automatically returns a `400 Bad Request` response (when using `[ApiController]`).

**Key Points:**
- Validates data **after** model binding, **before** action execution
- Uses attributes like `[Required]`, `[Range]`, `[EmailAddress]`
- Prevents invalid data from reaching business logic
- Automatic error responses in Web APIs with `[ApiController]`

---

## Data Annotations Validation

Data annotations are attributes applied to model properties to define validation rules.

### Commonly Used Attributes

| Attribute | Description | Example |
|-----------|-------------|---------|
| `[Required]` | Field must have a value | `[Required] public string Name { get; set; }` |
| `[StringLength]` | String must be within a length range | `[StringLength(100, MinimumLength = 3)]` |
| `[Range]` | Number must be within a range | `[Range(1, 100)]` |
| `[EmailAddress]` | String must be a valid email | `[EmailAddress]` |
| `[RegularExpression]` | String must match a regex pattern | `[RegularExpression(@"^\d{5}$")]` |
| `[Compare]` | Must match another property | `[Compare("Password")]` |
| `[Url]` | String must be a valid URL | `[Url]` |
| `[Phone]` | String must be a valid phone number | `[Phone]` |

### Example: Using Data Annotations

```csharp
public class RegisterUserRequest
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password { get; set; }

    [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
    public int Age { get; set; }
}

[HttpPost("register")]
public IActionResult Register(RegisterUserRequest request)
{
    // If validation fails, ASP.NET Core automatically returns 400 Bad Request
    return Ok("User registered successfully");
}
```

**Valid Request:**
```json
{
  "name": "John Doe",
  "email": "john@example.com",
  "password": "SecurePass123",
  "age": 25
}
```

**Response:** `200 OK` with message "User registered successfully"

**Invalid Request:**
```json
{
  "name": "J",
  "email": "invalid-email",
  "password": "123",
  "age": 15
}
```

**Response:** `400 Bad Request` with validation errors:
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Name": ["Name must be between 2 and 100 characters"],
    "Email": ["Invalid email format"],
    "Password": ["Password must be at least 8 characters"],
    "Age": ["Age must be between 18 and 100"]
  }
}
```

---

## Automatic Model Validation in Web APIs

When you add the `[ApiController]` attribute to a controller, ASP.NET Core enables **automatic model validation**:

- If `ModelState.IsValid` is `false`, it automatically returns `400 Bad Request`
- You don't need to manually check `ModelState`
- Validation errors are returned in a standardized JSON format

### Example: Automatic Validation with `[ApiController]`

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser(CreateUserRequest request)
    {
        // No need to check ModelState.IsValid — it's automatic
        // If validation fails, 400 is returned before this code runs
        return Ok("User created");
    }
}

public class CreateUserRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
```

**Invalid Request:**
```json
{
  "username": "",
  "email": "not-an-email"
}
```

**Response:** `400 Bad Request` (automatic, no manual check needed)

---

## Custom Validation

### 1. Using `IValidatableObject` Interface

For complex validation logic that involves multiple properties:

```csharp
public class BookingRequest : IValidatableObject
{
    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndDate <= StartDate)
        {
            yield return new ValidationResult(
                "End date must be after start date",
                new[] { nameof(EndDate) }
            );
        }

        if ((EndDate - StartDate).TotalDays > 30)
        {
            yield return new ValidationResult(
                "Booking cannot exceed 30 days",
                new[] { nameof(EndDate) }
            );
        }
    }
}
```

---

### 2. Custom Validation Attributes

Create reusable validation logic:

```csharp
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime date)
        {
            if (date <= DateTime.Now)
            {
                return new ValidationResult("Date must be in the future");
            }
        }
        return ValidationResult.Success;
    }
}

public class EventRequest
{
    [Required]
    public string Name { get; set; }

    [FutureDate]
    public DateTime EventDate { get; set; }
}
```

---

## Handling Validation Errors

### Manually Checking `ModelState` (Without `[ApiController]`)

If you're not using `[ApiController]`, you must manually check validation:

```csharp
[HttpPost("users")]
public IActionResult CreateUser(CreateUserRequest request)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Business logic
    return Ok("User created");
}
```

---

### Customizing Validation Responses

Override the default `400` response format:

```csharp
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .Select(e => new
                {
                    Field = e.Key,
                    Errors = e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                });

            return new BadRequestObjectResult(new
            {
                Message = "Validation failed",
                Errors = errors
            });
        };
    });
```

**Custom Response:**
```json
{
  "message": "Validation failed",
  "errors": [
    {
      "field": "Email",
      "errors": ["Invalid email format"]
    }
  ]
}
```

---

## Real-World Use Cases

### 1. Validating Request DTOs

```csharp
public class CreateOrderRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int ProductId { get; set; }

    [Required]
    [Range(1, 100)]
    public int Quantity { get; set; }

    [Required]
    [StringLength(200)]
    public string ShippingAddress { get; set; }
}

[HttpPost("orders")]
public IActionResult CreateOrder(CreateOrderRequest request)
{
    // Validation happens automatically
    // Only valid data reaches this point
    var order = _orderService.Create(request);
    return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
}
```

---

### 2. Preventing Bad Data from Entering Business Logic

```csharp
public class TransferMoneyRequest
{
    [Required]
    public string FromAccount { get; set; }

    [Required]
    public string ToAccount { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }
}

[HttpPost("transfer")]
public IActionResult Transfer(TransferMoneyRequest request)
{
    // Validation ensures:
    // - Accounts are not null
    // - Amount is positive
    // Invalid data never reaches the banking service
    _bankingService.Transfer(request.FromAccount, request.ToAccount, request.Amount);
    return Ok("Transfer successful");
}
```

---

### 3. Combining Multiple Binding Sources

```csharp
public class UpdateProductRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}

[HttpPut("products/{id}")]
public IActionResult UpdateProduct(
    [FromRoute] int id,                          // From URL
    [FromBody] UpdateProductRequest request,     // From JSON body
    [FromHeader(Name = "X-User-Id")] string userId) // From header
{
    // id, request, and userId are all bound and validated
    return Ok($"Product {id} updated by user {userId}");
}
```

---

## Common Mistakes

### ❌ Mistake 1: Missing `[ApiController]` Attribute

Without `[ApiController]`, automatic validation doesn't work:

```csharp
// Bug: No automatic validation
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser(CreateUserRequest request)
    {
        // ModelState.IsValid must be checked manually
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok("User created");
    }
}
```

**Fix:** Add `[ApiController]`:
```csharp
[ApiController] // Enables automatic validation
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // Validation happens automatically
}
```

---

### ❌ Mistake 2: Overusing `[FromBody]`

You can only have **one** `[FromBody]` parameter per action:

```csharp
// Bug: Can't have two [FromBody] parameters
[HttpPost]
public IActionResult Create([FromBody] User user, [FromBody] Address address)
{
    return Ok();
}
```

**Fix:** Combine into a single DTO:
```csharp
public class CreateUserRequest
{
    public User User { get; set; }
    public Address Address { get; set; }
}

[HttpPost]
public IActionResult Create([FromBody] CreateUserRequest request)
{
    return Ok();
}
```

---

### ❌ Mistake 3: Not Validating Input Properly

Relying on client-side validation only:

```csharp
// Bug: No validation attributes
public class CreateUserRequest
{
    public string Email { get; set; } // No [Required], no [EmailAddress]
}

[HttpPost]
public IActionResult CreateUser(CreateUserRequest request)
{
    // request.Email could be null or invalid
    _userService.Create(request.Email); // Potential bug
    return Ok();
}
```

**Fix:** Add proper validation:
```csharp
public class CreateUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
```

---

### ❌ Mistake 4: Ignoring `ModelState` Without `[ApiController]`

```csharp
// Bug: Not using [ApiController], not checking ModelState
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreateProductRequest request)
    {
        // Validation is ignored, invalid data can pass through
        return Ok();
    }
}
```

**Fix:** Either add `[ApiController]` or manually check `ModelState`:
```csharp
if (!ModelState.IsValid)
{
    return BadRequest(ModelState);
}
```

---

### ❌ Mistake 5: Using Primitive Types for Complex Data

```csharp
// Bug: Too many parameters, hard to validate
[HttpPost("users")]
public IActionResult CreateUser(string name, string email, int age, string address)
{
    // No validation, hard to maintain
    return Ok();
}
```

**Fix:** Use a DTO:
```csharp
public class CreateUserRequest
{
    [Required]
    public string Name { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Range(18, 100)]
    public int Age { get; set; }

    public string Address { get; set; }
}

[HttpPost("users")]
public IActionResult CreateUser(CreateUserRequest request)
{
    return Ok();
}
```

---

## Common Interview Questions

### Q1: What is model binding in ASP.NET Core?

**Answer:** Model binding is the process that automatically maps HTTP request data (from route, query string, body, headers, or form) to action method parameters or model properties, converting them into strongly-typed C# objects.

---

### Q2: What's the difference between `[FromQuery]` and `[FromBody]`?

**Answer:**
- `[FromQuery]` binds data from the URL query string (`?key=value`)
- `[FromBody]` binds data from the HTTP request body (typically JSON)
- You can only have one `[FromBody]` parameter per action

---

### Q3: How does automatic model validation work in ASP.NET Core?

**Answer:** When you add the `[ApiController]` attribute to a controller, ASP.NET Core automatically validates the model using data annotations. If validation fails (`ModelState.IsValid` is false), it returns a `400 Bad Request` response with error details before the action method executes.

---

### Q4: What happens when model validation fails?

**Answer:** With `[ApiController]`, a `400 Bad Request` response is automatically returned with a JSON object containing validation errors. Without `[ApiController]`, you must manually check `ModelState.IsValid` and return errors yourself.

---

### Q5: Can you have multiple `[FromBody]` parameters in a single action?

**Answer:** No. ASP.NET Core only allows one `[FromBody]` parameter per action because the request body can only be read once. To accept multiple objects, combine them into a single DTO.

---

### Q6: What are data annotations?

**Answer:** Data annotations are attributes like `[Required]`, `[StringLength]`, `[Range]`, and `[EmailAddress]` that define validation rules on model properties. They're used for automatic validation in ASP.NET Core.

---

### Q7: How do you implement custom validation logic?

**Answer:** You can implement custom validation by:
1. Using the `IValidatableObject` interface for multi-property validation
2. Creating custom validation attributes by inheriting from `ValidationAttribute`

---

### Q8: What is `ModelState.IsValid`?

**Answer:** `ModelState.IsValid` is a boolean property that indicates whether all model validation rules passed. It's automatically checked by `[ApiController]`, but must be manually checked in controllers without that attribute.

---

### Q9: What's the default binding source for complex types in Web APIs?

**Answer:** In Web APIs with `[ApiController]`, complex types (classes, DTOs) are bound from the request body (`[FromBody]`) by default. Simple types (int, string, etc.) are bound from route or query string.

---

### Q10: How do you customize validation error responses?

**Answer:** You can customize error responses by configuring `InvalidModelStateResponseFactory` in `ConfigureApiBehaviorOptions` during service registration:
```csharp
services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            // Custom error response logic
        };
    });
```

---

## Summary

- **Model binding** automatically maps HTTP request data (route, query, body, headers, form) to action parameters or model properties, eliminating manual parsing
- **Binding source attributes** (`[FromRoute]`, `[FromQuery]`, `[FromBody]`, `[FromHeader]`, `[FromForm]`) explicitly specify where data comes from
- **Model validation** uses data annotations (`[Required]`, `[Range]`, `[EmailAddress]`, etc.) to validate data after binding and before action execution
- **`[ApiController]` attribute** enables automatic validation — if validation fails, a `400 Bad Request` is returned automatically without manual `ModelState` checks
- **Common mistakes** include missing `[ApiController]`, having multiple `[FromBody]` parameters, not validating input properly, and using primitive types instead of DTOs
- **Custom validation** can be implemented using `IValidatableObject` for multi-property logic or custom `ValidationAttribute` classes for reusable rules
- **Real-world use cases** include validating request DTOs, preventing invalid data from reaching business logic, and combining multiple binding sources for complex API endpoints

