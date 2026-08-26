# 🎯 Senior .NET Core & Angular Architectural Interview Questions

This study guide contains senior-level architectural and technical questions covering authentication security, database optimization, system design patterns, and request pipelines.

---

## 🔒 Section 1: Authentication & Authorization (JWT, Cookies, Session Protection)

### Q1: Storing Tokens in Single-Page Applications
When building RESTful Web APIs connected to Single Page Applications (like Angular), many developers default to storing JWT Access and Refresh tokens inside browser `LocalStorage`. 
* What are the critical security vulnerabilities of this approach?
* How should you architect your authentication and storage layers to completely eliminate these attack vectors?

### Q2: Silent Token Rotation and Interceptor Safeguards
* Walk me through the end-to-end implementation mechanics of a silent token refresh loop using an Angular HTTP Interceptor when dealing with cross-origin cookie-based authentication.
* What critical structural bug can occur in the interceptor code if a user's session completely dies, and how do you write a safeguard to prevent it?

### Q3: Inbound Claim Mapping and Framework Synchronization
* Why is invoking `JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear()` often essential when building modern, cross-platform RESTful services?
* What specific runtime symptoms occur inside your controllers or authorization policies if this map is left at its default configuration while using clean JSON JWT payloads?

---

## 🗄️ Section 2: Entity Framework Core & Advanced Database Mechanics

### Q4: Relationship Mapping and EF Core Shadow States
* What are EF Core "Shadow Properties," and how do they function within the state tracking engine?
* Explain the precise mechanism of how configuring relationships via Fluent API while omitting a high-level `DbSet<T>` property declaration triggers a `Conflicting property... shadow state` compilation warning.

### Q5: Tracking Contexts vs. Untracked Performance Optimizations
* Compare the operational mechanics of Tracking Queries versus No-Tracking Queries (`AsNoTracking()`) within the Entity Framework Core engine.
* What are the primary production use cases where appending `.AsNoTracking()` is mandatory, and what are the architectural consequences of omitting it in read-heavy workflows?

### Q6: Eradicating the N+1 Query Problem Natively
* What is the N+1 Query Problem, and how does the combination of the `virtual` keyword and Lazy Loading introduce severe network latency bottlenecks in stateless Web APIs?
* How do you completely eliminate this problem inside a custom repository pattern layer while maintaining an entirely stateless execution context?

---

## 🏛️ Section 3: High-Level Architecture, Dependency Injection & Clean Code

### Q7: The Necessity of Wrapping DbContext
Entity Framework Core's `DbContext` already inherently implements the Repository Pattern (via `DbSet<T>`) and the Unit of Work Pattern (via `SaveChangesAsync()`). 
* Why should a senior architect still advocate for explicitly wrapping `DbContext` inside a custom Repository and Business Service layer?
* Provide three concrete architectural benefits this pattern introduces to an enterprise application.

### Q8: Captive Dependencies and Lifecycle Scopes
* What is a "Captive Dependency" in dependency injection, and why is resolving a Scoped service (like `DbContext`) directly from the Root Service Provider (`app.Services`) incredibly dangerous in a multi-threaded web environment?
* How must you explicitly handle service resolution if you need to run database migrations asynchronously during application bootstrapping?

### Q9: Centralizing Cross-Cutting Concerns via Extensions
* Imagine you have 15 different protected controllers that all need to read the authenticated user's unique identity string from the active HTTP request context. Why is writing claim parsing logic directly inside each controller action an architectural anti-pattern?
* How do you leverage C# Extension Methods to centralize this cross-cutting concern into a single, type-safe, reusable location?

---

## 🚀 Section 4: Middleware Pipelines & Resilient Systems Design

### Q10: Middleware Sequencing and Preflight CORS Dropouts
* Why is the registration sequence of middleware components inside `Program.cs` considered strictly binding in ASP.NET Core?
* Walk me through the exact pipeline failure cascade that occurs if an infrastructural middleware (like `UseRateLimiter()` or `UseAuthentication()`) is mistakenly placed *above* `UseCors()` when processing cross-origin requests.

### Q11: Centralized Exception Interception Contracts
* Why is wrapping code execution inside localized `try-catch` blocks within every single individual controller action considered a failure in enterprise systems design?
* How do you construct a custom `GlobalExceptionMiddleware` wrapper to intercept unexpected database infrastructure crashes natively, and how do you ensure it safely respects environment logging boundaries?
