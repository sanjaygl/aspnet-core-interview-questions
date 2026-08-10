# ASP.NET MVC Interview Questions – Complete Guide (100 Questions: Basic to Advanced)

## MVC Architecture & Fundamentals

**Q1. What is MVC in ASP.NET?**  
A. MVC (Model-View-Controller) is an architectural pattern that separates an application into three components: Model (data/business logic), View (UI/presentation), and Controller (handles requests, coordinates between Model and View). It improves maintainability, testability, and separation of concerns.

**Q2. What are the benefits of using MVC over Web Forms?**  
A. Better separation of concerns, full control over HTML/CSS, no ViewState (lighter pages), testability (controllers can be unit tested), SEO-friendly URLs, and support for modern web standards. MVC is more suitable for large-scale applications and RESTful APIs.

**Q3. What is the role of Model in MVC?**  
A. Model represents the application's data and business logic. It encapsulates data validation, business rules, and data access logic. Models are independent of the UI and can be reused across different views or even different applications.

**Q4. What is the role of View in MVC?**  
A. View is responsible for presenting data to the user (UI/presentation layer). It receives data from the Controller through ViewModels and renders HTML. Views should contain minimal logic and focus on displaying data using Razor syntax.

**Q5. What is the role of Controller in MVC?**  
A. Controller handles incoming HTTP requests, processes user input, interacts with Models, and selects the appropriate View to render. It acts as an intermediator between Model and View, coordinating application flow and business logic execution.

**Q6. What is the difference between ASP.NET MVC and ASP.NET Core MVC?**  
A. ASP.NET MVC runs only on Windows/.NET Framework. ASP.NET Core MVC is cross-platform (Windows/Linux/macOS), open-source, faster, modular, supports dependency injection natively, unified MVC and Web API, and uses modern middleware pipeline instead of HTTP modules.

**Q7. What is a ViewModel?**  
A. A ViewModel is a model specifically designed for a View. It contains only the data needed by the View and can combine data from multiple domain models. It helps keep Views simple and prevents exposing domain models directly to the UI.

**Q8. What is the difference between Model and ViewModel?**  
A. Model represents domain/business entities and database structure. ViewModel is tailored for a specific View, containing only the data and properties needed for that View. ViewModels often aggregate multiple Models and include presentation-specific properties (e.g., display labels, validation).

---

## Request Life Cycle in MVC

**Q9. Explain the MVC request life cycle.**  
A. 1) User sends HTTP request → 2) Routing determines Controller/Action → 3) Controller Instantiation → 4) Authorization Filter → 5) Model Binding → 6) Action Filter (before) → 7) Action Method Execution → 8) Action Filter (after) → 9) Result Filter → 10) Result Execution (View rendering) → 11) HTTP Response sent to client.

**Q10. What happens when a request is received by an MVC application?**  
A. The request hits the routing middleware which matches the URL pattern to a route, identifies the Controller and Action method. The Controller is instantiated (via DI), filters are executed, model binding occurs, the Action method executes, and the ActionResult is returned and rendered.

**Q11. What are the different stages in the MVC pipeline?**  
A. Route Matching → Controller Instantiation → Authorization → Model Binding → Action Filters (OnActionExecuting) → Action Execution → Action Filters (OnActionExecuted) → Result Filters (OnResultExecuting) → View Rendering → Result Filters (OnResultExecuted) → Response.

**Q12. What is the order of filter execution in MVC?**  
A. Authorization Filters → Resource Filters → Action Filters (OnActionExecuting) → Action Method → Action Filters (OnActionExecuted) → Exception Filters (if exception) → Result Filters (OnResultExecuting) → Result Execution → Result Filters (OnResultExecuted).

---

## Routing

**Q13. What is Routing in MVC?**  
A. Routing is the process of mapping incoming HTTP requests to Controller actions based on URL patterns. It extracts values from URLs (like Controller, Action, parameters) and routes requests to the appropriate handler without physical file paths.

**Q14. What is the difference between Conventional Routing and Attribute Routing?**  
A. Conventional Routing: Defined globally in Startup/Program.cs using patterns like `{controller}/{action}/{id?}`, applies to all controllers. Attribute Routing: Defined using attributes on Controllers/Actions (`[Route("api/products")]`), provides more control and flexibility per action.

**Q15. How do you define a default route in MVC?**  
A. In ASP.NET Core MVC: `app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");`. The `=` syntax defines defaults (Home controller, Index action), and `?` makes the parameter optional.

**Q16. What is Attribute Routing?**  
A. Routing defined using attributes directly on Controllers or Actions: `[Route("products/{id}")]`, `[HttpGet("api/users")]`. Provides fine-grained control over URL patterns, supports RESTful conventions, and allows different URLs for different actions on the same controller.

**Q17. Can you have multiple routes for the same action method?**  
A. Yes, using multiple `[Route]` or HTTP verb attributes: `[HttpGet("products/{id}")]` and `[HttpGet("items/{id}")]` can both map to the same action method, allowing multiple URL patterns.

**Q18. What are route constraints?**  
A. Constraints restrict route parameter values: `{id:int}` (integer only), `{name:alpha}` (alphabetic only), `{date:datetime}`, `{id:min(1)}`. They ensure URLs match specific patterns before routing to an action, improving routing precision.

**Q19. What is the difference between MapRoute and MapControllerRoute?**  
A. `MapRoute` is legacy ASP.NET MVC. `MapControllerRoute` is ASP.NET Core MVC's routing method. Both define conventional routes, but Core uses endpoint routing with `MapControllerRoute` inside `app.UseEndpoints()` or simplified `app.MapControllerRoute()`.

---

## Controllers & Action Methods

**Q20. What is a Controller in MVC?**  
A. A class that inherits from `Controller` or `ControllerBase`, handles HTTP requests, contains Action methods (one per request type), processes input, calls business logic/models, and returns ActionResults (views, JSON, redirects, etc.).

**Q21. What is the difference between Controller and ControllerBase?**  
A. `ControllerBase`: Base class for API controllers, provides core functionality (model binding, validation, HTTP responses), no View support. `Controller`: Inherits from `ControllerBase`, adds View-related features (ViewBag, ViewData, View(), PartialView()). Use `ControllerBase` for APIs.

**Q22. What is an Action Method?**  
A. A public method in a Controller that handles HTTP requests. It can accept parameters (via routing, query string, or body), perform business logic, and return an ActionResult (View, JSON, Redirect, File, etc.).

**Q23. Can an Action Method be private?**  
A. No. Action methods must be public to be accessible by the MVC framework. Private methods in controllers are helper methods, not actions.

**Q24. What is ActionResult?**  
A. The base return type for action methods representing the result of an action (View, JSON, Redirect, File, Content, etc.). ASP.NET Core uses `IActionResult` interface for polymorphic return types.

**Q25. What is the difference between ActionResult and IActionResult?**  
A. `ActionResult` is a concrete base class. `IActionResult` is an interface (ASP.NET Core) allowing returning different result types (ViewResult, JsonResult, RedirectResult, etc.) without specifying the exact type, enabling flexibility.

**Q26. What are the different types of ActionResult?**  
A. `ViewResult` (View()), `PartialViewResult` (PartialView()), `JsonResult` (Json()), `RedirectResult` (Redirect()), `RedirectToActionResult` (RedirectToAction()), `ContentResult` (Content()), `FileResult` (File()), `StatusCodeResult` (StatusCode()), `EmptyResult`, `ObjectResult`.

**Q27. What is the difference between View() and PartialView()?**  
A. `View()` renders a complete view with layout (master page). `PartialView()` renders a partial view without layout, used for reusable UI components (loaded via AJAX or rendered inside other views).

**Q28. What is RedirectToAction()?**  
A. Returns `RedirectToActionResult`, redirects the browser to a different action method (new HTTP GET request). Syntax: `RedirectToAction("ActionName", "ControllerName", new { id = 1 })`. URL changes in the browser.

**Q29. What is the difference between Redirect() and RedirectToAction()?**  
A. `Redirect(url)`: Redirects to any URL (external or internal) using a string URL. `RedirectToAction()`: Redirects to a specific MVC action using Controller/Action names, type-safe, URL is generated by routing.

---

## Model Binding

**Q30. What is Model Binding in MVC?**  
A. The process of automatically mapping HTTP request data (query string, form data, route values, headers, body) to action method parameters or model properties. Simplifies data extraction and type conversion.

**Q31. Where does Model Binding get data from?**  
A. Route values, query strings, form data (POST), request body (JSON/XML for APIs), headers, cookies. Binding sources can be specified using attributes: `[FromRoute]`, `[FromQuery]`, `[FromBody]`, `[FromHeader]`, `[FromForm]`.

**Q32. What is the order of Model Binding sources?**  
A. 1) Form values (POST data), 2) Route values, 3) Query strings. If a parameter is found in form data, it's used; otherwise, routing checks route values, then query strings.

**Q33. What are binding source attributes?**  
A. Attributes that specify where to bind data from: `[FromRoute]` (URL route), `[FromQuery]` (query string), `[FromBody]` (request body), `[FromHeader]` (HTTP header), `[FromForm]` (form data), `[FromServices]` (DI container).

**Q34. What is the difference between [FromBody] and [FromForm]?**  
A. `[FromBody]`: Binds data from the request body (JSON/XML), typically for APIs. `[FromForm]`: Binds data from HTML form fields (application/x-www-form-urlencoded or multipart/form-data), typically for traditional MVC forms.

**Q35. How do you bind a model from JSON in the request body?**  
A. Use `[FromBody]` attribute on the parameter: `public IActionResult Create([FromBody] Product product)`. The JSON in the request body is automatically deserialized into the `Product` object.

---

## Model Validation & Data Annotations

**Q36. What is Model Validation in MVC?**  
A. The process of validating user input against defined rules (required fields, string length, range, regex, etc.) before processing. MVC automatically validates models using Data Annotations and sets `ModelState.IsValid` to true/false.

**Q37. What are Data Annotations?**  
A. Attributes applied to model properties to define validation rules and metadata: `[Required]`, `[StringLength(100)]`, `[Range(1, 100)]`, `[EmailAddress]`, `[RegularExpression]`, `[Compare]`, `[Display(Name="")]`, `[DataType]`.

**Q38. How do you check if a model is valid?**  
A. Check `ModelState.IsValid` property in the action method: `if (!ModelState.IsValid) { return View(model); }`. If validation fails, return the view with validation errors displayed to the user.

**Q39. What is ModelState?**  
A. A dictionary that contains the state of model binding and validation. It stores attempted values, validation errors, and whether validation succeeded. Access errors via `ModelState.Values` or `ModelState["PropertyName"].Errors`.

**Q40. What is the difference between client-side and server-side validation?**  
A. Client-side validation: JavaScript validation in the browser (immediate feedback, reduces server load), can be bypassed. Server-side validation: Validates on the server (secure, cannot be bypassed), ensures data integrity before processing. Always use both.

**Q41. How do you implement custom validation?**  
A. Create a custom attribute by inheriting from `ValidationAttribute` and overriding `IsValid()` method: `public class CustomAttribute : ValidationAttribute { protected override ValidationResult IsValid(object value, ValidationContext context) { /* logic */ } }`. Apply to properties.

**Q42. What is Remote Validation?**  
A. Client-side validation that calls a server action method via AJAX to validate input (e.g., checking if username already exists). Use `[Remote("ActionName", "ControllerName")]` attribute. Returns JSON: `Json(true)` for valid, `Json("Error message")` for invalid.

---

## Filters

**Q43. What are Filters in MVC?**  
A. Attributes or classes that execute code before or after specific stages in the request pipeline (authorization, action execution, result execution, exception handling). They enable cross-cutting concerns like logging, caching, authentication.

**Q44. What are the types of Filters in MVC?**  
A. 1) Authorization Filters (IAuthorizationFilter) - runs first, checks permissions. 2) Resource Filters (IResourceFilter) - runs after authorization. 3) Action Filters (IActionFilter) - before/after action execution. 4) Exception Filters (IExceptionFilter) - handles exceptions. 5) Result Filters (IResultFilter) - before/after result execution.

**Q45. What is the execution order of filters?**  
A. Authorization → Resource → Action (OnActionExecuting) → Action Method → Action (OnActionExecuted) → Exception (if error) → Result (OnResultExecuting) → Result Execution → Result (OnResultExecuted).

**Q46. What is an Action Filter?**  
A. A filter that runs before and after action method execution. Implement `IActionFilter` with `OnActionExecuting()` (before) and `OnActionExecuted()` (after). Used for logging, input modification, caching, performance tracking.

**Q47. What is an Authorization Filter?**  
A. A filter that runs first in the pipeline to check if a user is authorized to access a resource. Implement `IAuthorizationFilter` or use built-in `[Authorize]` attribute. Returns `ChallengeResult` or `ForbidResult` if unauthorized.

**Q48. What is an Exception Filter?**  
A. A filter that handles exceptions thrown during action execution. Implement `IExceptionFilter` with `OnException()` method. Used for global error handling, logging exceptions, returning custom error views or JSON error responses.

**Q49. What is a Result Filter?**  
A. A filter that runs before and after result execution (view rendering, JSON serialization). Implement `IResultFilter` with `OnResultExecuting()` and `OnResultExecuted()`. Used for modifying response headers, compression, output caching.

**Q50. What is the difference between ActionFilter and ResultFilter?**  
A. Action Filter: Runs around action method execution (before/after business logic). Result Filter: Runs around result execution (before/after view rendering or JSON serialization). Use Action Filters for input/output manipulation, Result Filters for response modification.

**Q51. How do you create a custom filter?**  
A. Inherit from appropriate interface (`IActionFilter`, `IAuthorizationFilter`, etc.) or base class (`ActionFilterAttribute`, `Attribute`), implement required methods, and apply as an attribute: `[CustomFilter]` on Controller or Action.

**Q52. What is the difference between Filter and Middleware?**  
A. Middleware: Runs for every request in the pipeline (even static files), configured in Startup, operates on HttpContext. Filters: Only run for MVC actions, access to MVC context (ModelState, ActionContext, ViewData), more specialized for MVC-specific logic.

---

## Dependency Injection in MVC

**Q53. What is Dependency Injection in MVC?**  
A. A design pattern where dependencies (services) are injected into Controllers via constructor, enabling loose coupling, testability, and better maintainability. ASP.NET Core MVC has built-in DI container.

**Q54. How do you inject services into a Controller?**  
A. Register services in `Program.cs`: `builder.Services.AddScoped<IProductService, ProductService>();`. Then inject via constructor: `public ProductController(IProductService productService) { _service = productService; }`.

**Q55. What are the service lifetimes in MVC?**  
A. 1) Transient (`AddTransient`) - new instance per request. 2) Scoped (`AddScoped`) - one instance per HTTP request. 3) Singleton (`AddSingleton`) - one instance for application lifetime. Choose based on statefulness and resource usage.

**Q56. Can you inject services directly into Views?**  
A. Yes, using `@inject` directive: `@inject IProductService ProductService`. Then use `ProductService` in the view. Useful for accessing services without passing through Controllers, but avoid complex logic in views.

---

## ViewBag, ViewData, TempData

**Q57. What is ViewBag?**  
A. A dynamic property for passing data from Controller to View. Syntax: `ViewBag.Message = "Hello"` (Controller), `@ViewBag.Message` (View). Data is not type-safe, lost after the current request, and only available in the current view.

**Q58. What is ViewData?**  
A. A dictionary (`ViewDataDictionary`) for passing data from Controller to View. Syntax: `ViewData["Message"] = "Hello"` (Controller), `@ViewData["Message"]` (View). Requires casting for complex types, lost after the current request.

**Q59. What is TempData?**  
A. A dictionary for passing data between requests (typically redirect scenarios). Data survives one additional request, then is deleted. Internally uses session state or cookies. Syntax: `TempData["Message"] = "Saved"`, `@TempData["Message"]` (View).

**Q60. What is the difference between ViewBag, ViewData, and TempData?**  
A. ViewBag: Dynamic, current request only, no type-safety. ViewData: Dictionary, current request only, requires casting. TempData: Dictionary, persists across one redirect, useful for redirect messages (PRG pattern - Post-Redirect-Get).

**Q61. When should you use TempData over ViewBag?**  
A. Use TempData when redirecting after POST (Post-Redirect-Get pattern) to display success/error messages on the redirected page. Use ViewBag/ViewData for passing data within the same request (Controller → View).

**Q62. How does TempData work internally?**  
A. TempData uses Session state by default (requires session middleware: `builder.Services.AddSession()`). Alternatively, can use cookies via `AddCookieTempDataProvider()`. Data is marked for deletion after being read once.

---

## Razor View Engine

**Q63. What is Razor View Engine?**  
A. The default view engine in ASP.NET MVC that uses `@` syntax to embed C# code in HTML. Provides clean, readable syntax for mixing server-side code with HTML markup: `@Model.Name`, `@if()`, `@foreach()`, `@{ }` code blocks.

**Q64. What is the syntax for Razor expressions?**  
A. `@` for single expressions: `@Model.Name`, `@DateTime.Now`. `@{ }` for multi-line code blocks. `@:` for plain text inside code block. `@@` to escape `@` symbol. `@* comment *@` for Razor comments.

**Q65. What is @model directive?**  
A. Declares the model type for the view: `@model MyApp.Models.Product`. Enables strong typing, IntelliSense, and compile-time checking. Access model via `@Model` property: `@Model.Name`, `@Model.Price`.

**Q66. What is the difference between @model and @Model?**  
A. `@model` (lowercase): Directive at the top of the view declaring the model type. `@Model` (uppercase): Property accessing the actual model instance passed from the Controller.

**Q67. What are Razor code blocks?**  
A. Multi-line C# code enclosed in `@{ }`: `@{ var x = 10; var y = 20; var sum = x + y; }`. Used for declaring variables, loops, conditions. Code doesn't output to HTML unless explicitly written: `@sum`.

---

## Layouts, Partial Views, View Components

**Q68. What is a Layout in MVC?**  
A. A master template that defines the common structure (header, footer, navigation) for multiple views. Views inherit the layout using `Layout = "_Layout";` or `_ViewStart.cshtml`. Uses `@RenderBody()` to insert view content.

**Q69. What is @RenderBody()?**  
A. A method in Layout files that renders the content of the child view. It's the placeholder where the specific view's HTML is inserted within the layout's structure.

**Q70. What is @RenderSection()?**  
A. Defines optional or required sections in layouts: `@RenderSection("Scripts", required: false)`. Views can provide content for sections: `@section Scripts { <script>...</script> }`. Used for page-specific scripts, styles.

**Q71. What is _ViewStart.cshtml?**  
A. A special file that runs before each view in the same folder (and subfolders). Typically sets the default layout: `@{ Layout = "_Layout"; }`. Eliminates the need to specify layout in every view.

**Q72. What is _ViewImports.cshtml?**  
A. A special file that imports common namespaces, tag helpers, and directives for all views in the folder: `@using MyApp.Models`, `@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`. Reduces repetition in views.

**Q73. What is a Partial View?**  
A. A reusable view component (without layout) rendered inside other views or returned from actions. Created as `.cshtml` file, rendered using `@Html.Partial()`, `@await Html.PartialAsync()`, or `<partial name="_Partial" />` tag helper.

**Q74. How do you render a Partial View?**  
A. In Razor: `@Html.Partial("_PartialName", model)` (synchronous), `@await Html.PartialAsync("_PartialName", model)` (async), or `<partial name="_PartialName" model="@model" />` (tag helper). From controller: `return PartialView("_Partial", model)`.

**Q75. What is a View Component?**  
A. A reusable component with its own logic (like a mini-controller), used for complex UI elements (e.g., shopping cart widget, login panel). Has its own class (inherits `ViewComponent`) and view. Invoked using `@await Component.InvokeAsync("ComponentName")` or tag helper.

**Q76. What is the difference between Partial View and View Component?**  
A. Partial View: Simple UI reuse, no logic (just rendering). View Component: Includes logic (own class with `Invoke`/`InvokeAsync` method), can have dependencies injected, suitable for complex UI widgets with business logic.

---

## Tag Helpers vs HTML Helpers

**Q77. What are Tag Helpers?**  
A. Server-side code that extends HTML tags with additional attributes for generating HTML (e.g., `<a asp-controller="Home" asp-action="Index">`). More readable, HTML-like syntax compared to HTML Helpers. Introduced in ASP.NET Core.

**Q78. What are HTML Helpers?**  
A. C# methods that generate HTML markup: `@Html.ActionLink()`, `@Html.TextBoxFor()`, `@Html.BeginForm()`. Legacy approach (ASP.NET MVC), less readable than Tag Helpers but still supported in Core for backward compatibility.

**Q79. What is the difference between Tag Helpers and HTML Helpers?**  
A. Tag Helpers: HTML-like syntax, attributes on HTML tags (`<input asp-for="Name" />`), cleaner, better IntelliSense, designer-friendly. HTML Helpers: C# methods (`@Html.TextBoxFor(m => m.Name)`), more verbose, harder for designers to read.

**Q80. What are some common Tag Helpers?**  
A. `asp-controller`, `asp-action`, `asp-route-*` (anchor/form), `asp-for` (input/label binding), `asp-validation-for` (validation messages), `asp-validation-summary`, `asp-append-version` (cache busting), `asp-area` (areas), `asp-items` (select lists).

**Q81. How do you create a custom Tag Helper?**  
A. Create a class inheriting from `TagHelper`, override `Process()` or `ProcessAsync()`, manipulate `output` parameter to generate HTML. Register in `_ViewImports.cshtml`: `@addTagHelper *, AssemblyName`. Apply using tag name or attribute.

---

## Forms & Anti-Forgery Token

**Q82. How do you create a form in MVC?**  
A. Using Tag Helper: `<form asp-controller="Product" asp-action="Create" method="post">`. Or HTML Helper: `@using (Html.BeginForm("Create", "Product", FormMethod.Post)) { }`. Forms submit data to action methods via POST.

**Q83. What is CSRF (Cross-Site Request Forgery)?**  
A. An attack where a malicious website tricks a user's browser into sending unauthorized requests to another site where the user is authenticated. Example: Attacker submits a form to transfer money while the user is logged into their bank.

**Q84. What is Anti-Forgery Token?**  
A. A security token generated by the server and embedded in forms to prevent CSRF attacks. The token is validated on form submission. If tokens don't match, the request is rejected. Implemented using `[ValidateAntiForgeryToken]` attribute.

**Q85. How do you implement Anti-Forgery Token?**  
A. In View: Add `<form>` tag helper (automatically includes token) or `@Html.AntiForgeryToken()`. In Controller: Apply `[ValidateAntiForgeryToken]` attribute to POST action. Token is validated automatically; mismatched tokens result in HTTP 400 error.

**Q86. What is the difference between [ValidateAntiForgeryToken] and [AutoValidateAntiforgeryToken]?**  
A. `[ValidateAntiForgeryToken]`: Validates only on the specific action. `[AutoValidateAntiforgeryToken]`: Automatically validates on all unsafe HTTP methods (POST, PUT, PATCH, DELETE) across the controller/globally. Prefer `[AutoValidateAntiforgeryToken]` for better coverage.

---

## Authentication & Authorization

**Q87. What is Authentication in MVC?**  
A. The process of verifying a user's identity (who are you?). Typically involves checking credentials (username/password, JWT token, external providers like Google/Facebook). Configured using authentication middleware and schemes (Cookies, JWT, OAuth).

**Q88. What is Authorization in MVC?**  
A. The process of determining if an authenticated user has permission to access a resource (what can you do?). Implemented using `[Authorize]` attribute with roles or policies: `[Authorize(Roles = "Admin")]`, `[Authorize(Policy = "MinimumAge")]`.

**Q89. What is the difference between Authentication and Authorization?**  
A. Authentication: Verifies identity (login). Authorization: Verifies permissions (access control). Authentication happens first, then authorization checks if the authenticated user can access the requested resource.

**Q90. How do you implement authentication in MVC?**  
A. Configure authentication in `Program.cs`: `builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie()`. Use `app.UseAuthentication()` middleware. Sign in users: `await HttpContext.SignInAsync(claimsPrincipal)`. Secure actions with `[Authorize]`.

**Q91. What is the [Authorize] attribute?**  
A. An authorization filter that restricts access to Controllers or Actions to authenticated users. Syntax: `[Authorize]` (any authenticated user), `[Authorize(Roles = "Admin,Manager")]` (role-based), `[Authorize(Policy = "PolicyName")]` (policy-based).

**Q92. What is the [AllowAnonymous] attribute?**  
A. An attribute that allows unauthenticated users to access a specific action/controller even if `[Authorize]` is applied globally or at the controller level. Used for public pages like login, registration.

**Q93. What is Role-Based Authorization?**  
A. Restricting access based on user roles: `[Authorize(Roles = "Admin")]`. Roles are assigned to users during authentication (claims with type `ClaimTypes.Role`). Simple but less flexible than policy-based authorization.

**Q94. What is Policy-Based Authorization?**  
A. Flexible authorization using custom policies defined in `Program.cs`: `builder.Services.AddAuthorization(options => { options.AddPolicy("MinimumAge", policy => policy.Requirements.Add(new MinimumAgeRequirement(18))); })`. More powerful than role-based, supports complex requirements.

---

## Sessions, Cookies, TempData

**Q95. What is Session in MVC?**  
A. Server-side storage for user-specific data across multiple requests. Each user gets a unique session ID (stored in a cookie). Session data is stored in memory, distributed cache (Redis), or database. Requires `app.UseSession()` middleware.

**Q96. How do you use Session in MVC?**  
A. Enable session: `builder.Services.AddSession()` and `app.UseSession()`. Store data: `HttpContext.Session.SetString("key", "value")` or `SetInt32()`. Retrieve: `HttpContext.Session.GetString("key")`. Session expires after inactivity or browser close.

**Q97. What are Cookies in MVC?**  
A. Small pieces of data stored on the client's browser (key-value pairs). Used for authentication tokens, user preferences, tracking. Created using `Response.Cookies.Append("key", "value", options)`. Read using `Request.Cookies["key"]`.

**Q98. What is the difference between Session and Cookies?**  
A. Session: Stored on server, more secure, larger storage, expires after inactivity. Cookies: Stored on client browser, less secure (can be tampered), limited size (4KB), can have expiration date. Use sessions for sensitive data, cookies for non-sensitive preferences.

**Q99. What is the difference between TempData and Session?**  
A. TempData: Short-lived, persists only for the next request (redirect), automatically deleted after being read. Session: Persists across multiple requests until timeout or manual deletion. Use TempData for redirect messages, Session for user-specific state.

---

## Exception Handling

**Q100. How do you handle exceptions in MVC?**  
A. 1) Try-catch in action methods. 2) Exception Filters (`[ExceptionFilter]` or `IExceptionFilter`). 3) Global exception handling middleware (`app.UseExceptionHandler("/Error")`). 4) Developer Exception Page (development): `app.UseDeveloperExceptionPage()`. 5) Custom error pages for production.

---

**Total Questions: 100** covering ASP.NET MVC from fundamentals to advanced topics, designed for interview preparation from Junior to Senior level positions.
