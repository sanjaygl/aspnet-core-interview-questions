# ASP.NET Core — Web API & MVC — Interview Q&A

---

### Q1. Minimal APIs vs Controller-based APIs — what are the real trade-offs?

**Answer:**
"Minimal APIs define endpoints directly with lambda expressions (`app.MapGet(...)`) — less boilerplate, faster to write for small services/simple endpoints, and a lower startup/memory footprint (relevant for things like AWS Lambda/Azure Functions cold starts). Controller-based APIs give you a more structured, convention-heavy framework — action filters, model binding conventions, `[ApiController]`'s automatic model validation, and a more familiar structure for larger teams used to MVC patterns. For a large, long-lived API with many endpoints, many cross-cutting filters, and a big team, Controllers still tend to scale better organizationally — the structure and conventions pay for themselves as the surface area grows; Minimal APIs shine for smaller, focused services or where startup performance genuinely matters."

```csharp
// Minimal API
app.MapGet("/orders/{id}", async (int id, AppDbContext db) =>
    await db.Orders.FindAsync(id) is Order order ? Results.Ok(order) : Results.NotFound());

// Controller-based
[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) =>
        await _db.Orders.FindAsync(id) is Order order ? Ok(order) : NotFound();
}
```

---

### Q2. How does Model Binding work, and what's a case where it silently fails to bind what you expected?

**Answer:**
"Model binding takes values from the request (route parameters, query string, form data, headers, JSON body) and maps them onto the parameters/model of the action being invoked, based on naming conventions and explicit attributes (`[FromRoute]`, `[FromQuery]`, `[FromBody]`, `[FromHeader]`). A common silent-failure case: binding a complex object from the query string when property names don't match exactly (case aside, nested objects need a specific `prefix.property` naming convention) — the property just ends up with its default value, with no error at all, since model binding treats an unmatched property as 'just not provided' rather than a hard failure."

```csharp
public class OrderFilter { public string Status { get; set; } = ""; public int? MinTotal { get; set; } }

[HttpGet]
public IActionResult Get([FromQuery] OrderFilter filter) { /* ... */ }
// GET /orders?Status=Shipped&MinTotal=100 - binds correctly
// GET /orders?status=Shipped&minimumTotal=100 - MinTotal silently stays null, name mismatch, no error
```

---

### Q3. How does Model Validation tie into automatic `400 Bad Request` responses?

**Answer:**
"With `[ApiController]` applied (automatic on controllers by convention in API projects), ASP.NET Core automatically checks `ModelState.IsValid` *before* the action method body even runs — if any `[Required]`/`[Range]`/etc. validation attribute fails, it short-circuits and returns a `400 Bad Request` with a `ProblemDetails`-shaped body describing the validation errors, without the action code needing to check `ModelState` manually at all. Without `[ApiController]`, that automatic behavior doesn't happen — you'd need to check `ModelState.IsValid` yourself at the top of every action."

```csharp
public class CreateOrderRequest
{
    [Required] public string CustomerName { get; set; } = "";
    [Range(1, int.MaxValue)] public int Quantity { get; set; }
}

[ApiController] // this is what enables automatic 400 responses on invalid ModelState
[Route("orders")]
public class OrdersController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreateOrderRequest request)
    {
        // if request is invalid, this line is NEVER reached - a 400 was already returned automatically
        return Ok();
    }
}
```

---

### Q4. What's the difference between an Action Filter and Middleware?

**Answer:**
"Both can run code before/after a request in a wrapping fashion, but they operate at different layers. Middleware runs for *every* request, regardless of whether it maps to any specific MVC action at all (it doesn't know or care about controllers/actions), and executes before routing has even resolved model-bound parameters. An Action Filter runs specifically around MVC action execution, *after* model binding has already happened — so it has access to the bound action arguments, `ModelState`, and the specific action/controller metadata being invoked, which generic middleware simply doesn't have visibility into."

```csharp
public class ValidateApiKeyFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // has access to context.ActionArguments - the actual bound parameters for this specific action
        if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var key) || key != "secret")
            context.Result = new UnauthorizedResult();
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}
```

**Cross-question: Can an Action Filter access route data and model-bound parameters the way middleware can't — why does that matter?**
"Yes — `ActionExecutingContext.ActionArguments` gives a filter the actual, already-bound parameter values for the specific action about to run, and `ActionDescriptor` gives it metadata about the action/controller itself. Middleware, running earlier and more generically, only sees the raw `HttpContext` — the request path, headers, raw body stream — with no concept of 'which controller action, with which bound parameters, is this going to.' That's precisely why per-action logic that needs bound values (like validating one specific parameter's business rules) belongs in a filter, not middleware."

---

### Q5. What is `ProblemDetails`, and why is it the standard shape for error responses?

**Answer:**
"`ProblemDetails` is a standardized (RFC 7807) JSON shape for HTTP API error responses — fields like `type`, `title`, `status`, `detail`, `instance`, plus room for extension members. ASP.NET Core uses it automatically for its built-in error responses (validation failures, `UseExceptionHandler`'s default output). The value of standardizing on it: any client consuming the API can parse error responses with one consistent shape, regardless of which specific error occurred, instead of every endpoint (or every different error type) inventing its own bespoke error JSON structure."

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": { "CustomerName": ["The CustomerName field is required."] }
}
```

---

### Q6. How would you implement API versioning, and what are the trade-offs of URL-based vs header-based versioning?

**Answer:**
"URL-based versioning (`/v1/orders`, `/v2/orders`) is the most explicit and discoverable — easy to see in logs, easy to test manually, easy for API consumers to understand at a glance, but it does mean the version is baked into every URL a client hardcodes. Header-based versioning (a custom header like `X-Api-Version: 2`, or via the `Accept` header's media type) keeps URLs clean and stable, but is less visible/discoverable — you can't just look at a URL to know which version you're hitting, and it's slightly more awkward to test via a browser address bar. Most public APIs lean toward URL-based versioning for its simplicity and discoverability; header-based is more common for APIs consumed exclusively by well-coordinated internal clients."

```csharp
// Using Asp.Versioning package
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/orders")]
public class OrdersV1Controller : ControllerBase { }
```

---

### Q7. What is Content Negotiation, and how does ASP.NET Core decide the response format?

**Answer:**
"Content Negotiation is the process of the server choosing a response representation format based on what the client says it can accept, via the `Accept` request header (e.g., `application/json`, `application/xml`). ASP.NET Core's MVC formatters inspect the `Accept` header and pick the first configured formatter that matches; if no formatter matches what the client requested (and no formatter is configured as a fallback), it can return `406 Not Acceptable`. By default, most ASP.NET Core Web API templates only configure a JSON formatter — XML or other formats need to be explicitly added if actually needed."

```csharp
builder.Services.AddControllers()
    .AddXmlSerializerFormatters(); // now the API can also negotiate application/xml if the client asks for it
```
