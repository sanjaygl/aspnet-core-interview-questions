# .NET Core Web API

A .NET Core Web API project demonstrating REST APIs, Entity Framework Core, PostgreSQL, JWT authentication, HttpOnly cookie-based Access/Refresh Token authentication, repository/service patterns, middleware, rate limiting, and database migrations.

---

## 1. Tech Stack

- .NET
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- HttpOnly Cookies
- Angular Client
- Repository Pattern
- Service Layer
- Middleware
- Swagger
- Rate Limiting

---

# 2. Project Setup

Clone the repository and move into the API directory:

```bash
git clone <repository-url>
cd Demo_NET_API
```

Restore dependencies:

```bash
dotnet restore
```

Build the project:

```bash
dotnet build
```

Run the API:

```bash
dotnet run
```

Run with automatic reload:

```bash
dotnet watch run
```

Clean the project:

```bash
dotnet clean
```

---

# 3. Useful .NET Commands

Check installed .NET SDK:

```bash
dotnet --version
```

List installed SDKs:

```bash
dotnet --list-sdks
```

List installed runtimes:

```bash
dotnet --list-runtimes
```

List project dependencies:

```bash
dotnet list package
```

Add a NuGet package:

```bash
dotnet add package <PackageName>
```

Remove a NuGet package:

```bash
dotnet remove package <PackageName>
```

---

# 4. Entity Framework Core Commands

Install EF Core CLI:

```bash
dotnet tool install --global dotnet-ef
```

Update EF Core CLI:

```bash
dotnet tool update --global dotnet-ef
```

Check EF Core version:

```bash
dotnet ef --version
```

Create migration:

```bash
dotnet ef migrations add MigrationName
```

Example:

```bash
dotnet ef migrations add AddUserSession
```

List migrations:

```bash
dotnet ef migrations list
```

Remove the last migration:

```bash
dotnet ef migrations remove
```

Apply migrations:

```bash
dotnet ef database update
```

Update database to a specific migration:

```bash
dotnet ef database update MigrationName
```

Drop database:

```bash
dotnet ef database drop --force
```

Generate SQL migration script:

```bash
dotnet ef migrations script
```

Generate SQL script to a file:

```bash
dotnet ef migrations script -o migration.sql
```

---

# 5. PostgreSQL

The API uses PostgreSQL through Entity Framework Core.

Database configuration is stored in application configuration/User Secrets.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=demo_db;Username=postgres;Password=<password>"
  }
}
```

Do not commit real database passwords to source control.

---

# 6. JWT Configuration

JWT settings are configured using `JwtSettings`.

Example:

```json
{
  "JwtSettings": {
    "Issuer": "https://localhost:44351/",
    "Audience": "https://localhost:44351/"
  }
}
```

The JWT SecretKey should not be committed to Git.

Use User Secrets for local development:

```bash
dotnet user-secrets init
```

Set the JWT secret:

```bash
dotnet user-secrets set "JwtSettings:SecretKey" "your-development-secret"
```

Set the database connection:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
```

List configured secrets:

```bash
dotnet user-secrets list
```

Remove a secret:

```bash
dotnet user-secrets remove "JwtSettings:SecretKey"
```

---

# 7. Authentication Flow

The application uses an Access Token + Refresh Token architecture.

```text
Angular
   │
   │ POST /api/Auth/login
   ▼
.NET API
   │
   ├── Validate username/password
   ├── Generate Access Token
   ├── Generate Refresh Token
   ├── Store Refresh Token in UserSession
   └── Set HttpOnly Cookies
          │
          ├── X-Access-Token
          └── X-Refresh-Token
```

The tokens are not returned in the response body.

---

# 8. Access Token

The Access Token is a JWT used for normal API authentication.

```text
Cookie: X-Access-Token
Lifetime: 15 minutes
Storage: HttpOnly Cookie
```

Cookie configuration:

```text
HttpOnly = true
Secure = true
SameSite = None
Path = /
```

The API reads the token from:

```text
X-Access-Token
```

JWT validation checks:

```text
Issuer
Audience
Lifetime
Signing Key
```

---

# 9. Refresh Token

The Refresh Token is generated using a cryptographically secure random number generator.

```text
Cookie: X-Refresh-Token
Lifetime: 7 days
Storage: HttpOnly Cookie + UserSession
```

The database stores the Refresh Token against the user's session.

```text
Browser
   │
   └── X-Refresh-Token
            │
            ▼
        .NET API
            │
            ▼
       UserSession
            │
            └── RefreshToken
```

---

# 10. JWT Claims

The API uses standard ASP.NET Core claims:

```text
ClaimTypes.Name
ClaimTypes.Email
ClaimTypes.Role
JwtRegisteredClaimNames.Jti
```

Example:

```text
Name  → username
Email → user email
Role  → user role
Jti   → unique token ID
```

JWT configuration:

```csharp
NameClaimType = ClaimTypes.Name;
RoleClaimType = ClaimTypes.Role;
```

Username can be retrieved using:

```csharp
var username = User.GetUsername();
```

---

# 11. Authentication Endpoints

## Login

```http
POST /api/Auth/login
```

Example:

```json
{
  "username": "subpoch",
  "password": "password"
}
```

Successful response:

```json
{
  "message": "Login successful!"
}
```

The API sets:

```text
X-Access-Token
X-Refresh-Token
```

as HttpOnly cookies.

---

## Register

```http
POST /api/Auth/register
```

Example:

```json
{
  "username": "subpoch",
  "email": "admin@company.com",
  "password": "password"
}
```

---

## Refresh Token

```http
POST /api/Auth/refresh
```

The browser automatically sends:

```text
X-Access-Token
X-Refresh-Token
```

The API validates the Refresh Token and generates new tokens.

---

# 12. Access Token Refresh Flow

When the Access Token expires:

```text
Angular
   │
   ▼
Protected API
   │
   ▼
401 Unauthorized
   │
   ▼
Angular HTTP Interceptor
   │
   ▼
POST /api/Auth/refresh
   │
   ▼
Validate Refresh Token
   │
   ▼
Generate New Access Token
   │
   ▼
Generate New Refresh Token
   │
   ▼
Update UserSession
   │
   ▼
Update HttpOnly Cookies
   │
   ▼
Retry Original Request
   │
   ▼
200 OK
```

---

# 13. Refresh Token Rotation

Every successful refresh generates a new Refresh Token.

```text
Old Refresh Token
       │
       ▼
    Validate
       │
       ▼
New Refresh Token
       │
       ├── Update Database
       └── Update Cookie
```

---

# 14. Protected APIs

Use `[Authorize]` for authenticated endpoints:

```csharp
[Authorize]
[HttpGet("my-orders")]
public async Task<IActionResult> GetMyOrders()
{
    ...
}
```

Role-based authorization:

```csharp
[Authorize(Roles = "Admin")]
```

Get the current username:

```csharp
var username = User.GetUsername();
```

Check the current role:

```csharp
User.IsInRole("Admin");
```

---

# 15. Angular → API Authentication

Angular does not directly access the authentication tokens.

Requests must use:

```typescript
withCredentials: true
```

The browser automatically sends the HttpOnly cookies.

```text
Angular
   │
   │ withCredentials: true
   ▼
Browser
   │
   ├── X-Access-Token
   └── X-Refresh-Token
   │
   ▼
.NET API
```

---

# 16. CORS

The API allows the Angular development application:

```text
http://localhost:4200
```

Credentials are enabled because authentication uses cookies.

```csharp
policy.WithOrigins("http://localhost:4200")
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials();
```

---

# 17. Middleware Pipeline

```text
GlobalExceptionMiddleware
        ↓
HTTPS Redirection
        ↓
Routing
        ↓
CORS
        ↓
Authentication
        ↓
Authorization
        ↓
Rate Limiter
        ↓
Controllers
```

Authentication must execute before authorization:

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

---

# 18. Rate Limiting

Current configuration:

```text
100 requests
per
1 minute
```

When the limit is exceeded:

```text
429 Too Many Requests
```

---

# 19. Repository and Service Pattern

The API follows:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
Entity Framework Core
    ↓
PostgreSQL
```

Authentication:

```text
AuthController
      ↓
UserService
      ↓
UserRepository
      ↓
DemoDbContext
      ↓
PostgreSQL
```

---

# 20. Common Development Commands

### Start API

```bash
dotnet run
```

### Start with hot reload

```bash
dotnet watch run
```

### Build

```bash
dotnet build
```

### Clean

```bash
dotnet clean
```

### Restore packages

```bash
dotnet restore
```

### Run tests

```bash
dotnet test
```

### Run with a specific environment

Windows PowerShell:

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet run
```

---

# 21. Authentication Testing

Recommended testing sequence:

### 1. Register

```http
POST /api/Auth/register
```

### 2. Login

```http
POST /api/Auth/login
```

Verify that the response sets:

```text
X-Access-Token
X-Refresh-Token
```

### 3. Call protected API

```http
GET /api/Order/my-orders
```

The browser automatically sends the Access Token cookie.

### 4. Wait for Access Token expiry

Access Token lifetime:

```text
15 minutes
```

### 5. Call protected API again

The API returns:

```text
401 Unauthorized
```

### 6. Angular interceptor calls

```http
POST /api/Auth/refresh
```

### 7. API validates Refresh Token

Refresh Token lifetime:

```text
7 days
```

### 8. API rotates tokens

New:

```text
X-Access-Token
X-Refresh-Token
```

### 9. Angular retries the original request

Expected result:

```text
200 OK
```

---

# 22. Security Rules

- Never return JWT tokens in response bodies.
- Keep Access Tokens short-lived.
- Use HttpOnly cookies for authentication tokens.
- Never commit JWT SecretKey.
- Never commit database passwords.
- Validate issuer, audience, lifetime, and signing key.
- Validate Refresh Tokens server-side.
- Rotate Refresh Tokens after successful refresh.
- Use HTTPS with `Secure=true`.
- Use `withCredentials: true` from Angular.
- Never log Access Tokens or Refresh Tokens.

---

# 23. Useful Debugging Locations

When authentication fails, check these in order:

```text
1. Browser Cookies
      ↓
2. X-Access-Token
      ↓
3. X-Refresh-Token
      ↓
4. API JWT validation
      ↓
5. ClaimsPrincipal
      ↓
6. User.GetUsername()
      ↓
7. UserSession
      ↓
8. RefreshToken comparison
```

For browser debugging:

```text
Chrome DevTools
→ Application
→ Cookies
```

For API debugging:

```text
Chrome DevTools
→ Network
→ Request Headers
→ Cookie
```

Verify:

```text
X-Access-Token=...
X-Refresh-Token=...
```

---

# 24. Complete Authentication Lifecycle

```text
                     LOGIN
                       │
                       ▼
                Validate User
                       │
                       ▼
             Generate Access Token
                       │
                       ▼
            Generate Refresh Token
                       │
                       ▼
          Store Refresh Token in DB
                       │
                       ▼
             Set HttpOnly Cookies
                       │
              ┌────────┴────────┐
              ▼                 ▼
       Access Token       Refresh Token
        15 minutes           7 days
              │                 │
              ▼                 │
       Protected APIs            │
              │                 │
              ▼                 │
        Access Token             │
          expires                │
              │                 │
              ▼                 │
             401                │
              │                 │
              ▼                 │
     Angular Interceptor        │
              │                 │
              ▼                 │
       /api/Auth/refresh ───────┘
              │
              ▼
     Validate Refresh Token
              │
              ▼
     Generate New Access Token
              │
              ▼
    Generate New Refresh Token
              │
              ▼
       Update UserSession
              │
              ▼
       Update HttpOnly Cookies
              │
              ▼
      Retry Original Request
              │
              ▼
            200 OK
```

---

# 25. Interview Concepts Covered

This project demonstrates:

- REST API
- HTTP status codes
- Authentication vs Authorization
- JWT
- Access Token
- Refresh Token
- HttpOnly Cookies
- CORS
- Claims
- ClaimsPrincipal
- Role-based authorization
- Middleware
- Dependency Injection
- Repository Pattern
- Service Layer
- Entity Framework Core
- PostgreSQL
- EF Core Migrations
- Rate Limiting
- Options Pattern
- Global Exception Handling
- Refresh Token Rotation
- Token Expiration
- API Security