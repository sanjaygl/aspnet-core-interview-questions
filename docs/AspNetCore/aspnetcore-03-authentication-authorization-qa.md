# ASP.NET Core — Authentication & Authorization — Interview Q&A

---

### Q1. What's the actual difference between Authentication and Authorization?

**Answer:**
"Authentication answers 'who are you' — verifying identity, handled by `UseAuthentication()`, which populates `HttpContext.User` with a `ClaimsPrincipal` if the request carries valid credentials (a cookie, a JWT, etc.). Authorization answers 'are you allowed to do this' — handled by `UseAuthorization()` and `[Authorize]` attributes/policies, which check the already-established identity's claims/roles against what a specific endpoint requires. Authentication has to succeed (or at least run) before authorization can make any meaningful decision, which is exactly why the middleware order matters (see [[aspnetcore-01-middleware-pipeline-qa]])."

---

### Q2. How does JWT Bearer authentication work end-to-end in ASP.NET Core?

**Answer:**
"The client sends a request with `Authorization: Bearer <token>`. The JWT Bearer middleware extracts the token, validates its signature against the configured signing key (proving it wasn't tampered with and was actually issued by the trusted authority), checks standard claims like `exp` (expiration) and `iss`/`aud` (issuer/audience match what's configured), and if everything checks out, builds a `ClaimsPrincipal` from the token's claims and assigns it to `HttpContext.User`. If validation fails for any reason, the request is rejected with `401 Unauthorized` before it ever reaches the controller action."

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://my-auth-server",
            ValidateAudience = true,
            ValidAudience = "my-api",
            ValidateLifetime = true,     // this is what checks the 'exp' claim
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
        };
    });
```

**Cross-question: What happens if the JWT's signature is valid but its `exp` claim has passed — where does that get checked?**
"`ValidateLifetime = true` in the `TokenValidationParameters` is what checks `exp` (and `nbf`, 'not before') against the current server time — a validly-signed but expired token still fails validation and the request is rejected with `401`. This happens entirely inside the JWT Bearer middleware, before the request reaches any controller code — the application never sees an expired token as 'authenticated.'"

---

### Q3. What's the difference between Cookie authentication and JWT Bearer authentication, and when would you pick one?

**Answer:**
"Cookie authentication stores an encrypted authentication ticket in a browser cookie, automatically sent by the browser on every request to the same origin — the server (or a distributed cache backing the auth ticket) holds the actual session-related state. It's the natural fit for traditional server-rendered or same-origin SPA scenarios, and benefits from built-in CSRF protection patterns and easy server-side revocation (invalidate the session, done). JWT Bearer is stateless — the token itself carries all the claims, verified purely by signature, no server-side session store needed — which fits APIs consumed by multiple different clients (mobile apps, SPAs on a different domain, other services), especially across multiple API instances where you don't want a shared session store. The trade-off: a JWT can't be easily revoked before its expiration without extra infrastructure (a token blocklist/very short expiry + refresh tokens), where a cookie-backed session can just be invalidated server-side instantly."

---

### Q4. What is Claims-based identity, and how does a `ClaimsPrincipal` relate to `[Authorize(Roles = "Admin")]`?

**Answer:**
"A `ClaimsPrincipal` represents the authenticated user as a collection of claims — key/value pairs like `role: Admin`, `sub: user123`, `email: ...` — rather than a single fixed 'role' field. `[Authorize(Roles = "Admin")]` is really just a convenience check translating to 'does this `ClaimsPrincipal` have a claim of type `role` with value `Admin`' — roles are simply one conventional, well-known claim type among many, not a separate concept from claims themselves."

```csharp
[Authorize(Roles = "Admin")]
public IActionResult DeleteUser(int id) { /* only reached if the ClaimsPrincipal has a role claim = "Admin" */ }

// Equivalent manual check, showing what's actually happening under the hood
if (User.HasClaim(ClaimTypes.Role, "Admin")) { /* ... */ }
```

---

### Q5. What is Policy-based Authorization, and why is it more flexible than role checks alone?

**Answer:**
"A policy is a named, reusable authorization rule that can combine multiple requirements — not just a single role check, but arbitrary logic (multiple claims, custom conditions, even resource-specific checks). Policies decouple 'what the rule is' from 'where it's applied' — you define the policy once, then apply it by name anywhere with `[Authorize(Policy = "...")]`, instead of scattering ad-hoc role/claim checks across many actions."

```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MinimumAge18", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "DateOfBirth") &&
            DateTime.Parse(context.User.FindFirst("DateOfBirth")!.Value) <= DateTime.Now.AddYears(-18)));
});

[Authorize(Policy = "MinimumAge18")]
public IActionResult BuyAlcohol() { /* ... */ }
```

---

### Q6. How would you write a custom `IAuthorizationHandler` for a requirement that can't be expressed as a simple role/claim check?

**Answer:**
"For genuinely resource-specific authorization (e.g., 'a user can only edit *their own* order,' which requires knowing about the specific `Order` being acted on, not just the user's claims in isolation), implement a custom `IAuthorizationRequirement` and a matching `AuthorizationHandler<TRequirement, TResource>` that receives both the user's `ClaimsPrincipal` and the actual resource instance, and decides `Succeed()`/fails based on comparing them."

```csharp
public class SameOwnerRequirement : IAuthorizationRequirement { }

public class SameOwnerHandler : AuthorizationHandler<SameOwnerRequirement, Order>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, SameOwnerRequirement requirement, Order order)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (order.CustomerId == userId) context.Succeed(requirement);
        return Task.CompletedTask;
    }
}

// Usage in an action - resource-based, not just a static [Authorize] attribute
var result = await _authorizationService.AuthorizeAsync(User, order, new SameOwnerRequirement());
if (!result.Succeeded) return Forbid();
```

---

### Q7. How does ASP.NET Core integrate with an external OAuth2/OpenID Connect provider, and what's the token flow at a high level?

**Answer:**
"For a browser-based app, the OpenID Connect middleware redirects the user to the identity provider (e.g., Entra ID) to authenticate, the provider redirects back with an authorization code, the middleware exchanges that code for tokens (ID token + access token, and optionally a refresh token) behind the scenes, and builds the app's local authentication cookie/session from the resulting claims. For an API being called directly (no browser redirect involved), the API just validates an already-issued JWT access token via the JWT Bearer middleware (as in Q2) — the API itself typically doesn't participate in the login redirect flow at all, it just trusts tokens issued by the configured authority."

```csharp
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.Authority = "https://login.microsoftonline.com/{tenant}";
    options.ClientId = "...";
    options.ResponseType = "code"; // authorization code flow
});
```
