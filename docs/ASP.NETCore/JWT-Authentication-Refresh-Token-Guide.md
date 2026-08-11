# JWT Authentication with Refresh Token - ASP.NET Core + Angular

## Overview

JWT (JSON Web Token) authentication with refresh tokens provides secure, stateless authentication where:
- **Access Token**: Short-lived (5-15 min), used for API requests
- **Refresh Token**: Long-lived (days/weeks), used to get new access tokens without re-login

## High-Level Flow Diagram

### 1. Login Flow
```
┌──────┐                                           
│ User │                                           
└───┬──┘                                           
    │                                              
    │  Login Request (username, password)         
    │                                              
    ▼                                              
┌────────────────────────────────────┐            
│   API - Validate Credentials       │            
└────────────┬───────────────────────┘            
             │                                     
             │  Credentials Valid?                
             │                                     
             ▼                                     
┌────────────────────────────────────┐            
│   Generate JWT Tokens:             │            
│   • Access Token (15 min)          │            
│   • Refresh Token (7 days)         │            
└────────────┬───────────────────────┘            
             │                                     
             │  Return Tokens                     
             │                                     
             ▼                                     
┌────────────────────────────────────┐            
│   Angular Client                   │            
│   • Store Access Token             │            
│   • Store Refresh Token            │            
│   • Navigate to Dashboard          │            
└────────────────────────────────────┘            
```

### 2. API Request with Valid Token Flow
```
┌──────────┐                 ┌─────────────┐
│  Client  │                 │     API     │
└────┬─────┘                 └──────┬──────┘
     │                              │
     │  GET /api/profile            │
     │  Authorization: Bearer <AT>  │
     ├─────────────────────────────►│
     │                              │
     │                        ┌─────▼─────┐
     │                        │  Validate │
     │                        │   Token   │
     │                        └─────┬─────┘
     │                              │
     │                         Token Valid
     │                              │
     │  200 OK + Profile Data       │
     │◄─────────────────────────────┤
     │                              │
```

### 3. Token Refresh Flow (Expired Access Token)
```
┌──────────┐                 ┌─────────────┐              ┌──────────┐
│  Client  │                 │     API     │              │    DB    │
└────┬─────┘                 └──────┬──────┘              └────┬─────┘
     │                              │                          │
     │  1. GET /api/profile         │                          │
     │     Authorization: Bearer    │                          │
     │     <Expired Access Token>   │                          │
     ├─────────────────────────────►│                          │
     │                              │                          │
     │                        ┌─────▼─────┐                    │
     │                        │  Validate │                    │
     │                        │   Token   │                    │
     │                        └─────┬─────┘                    │
     │                              │                          │
     │                        Token Expired!                   │
     │                              │                          │
     │  2. 401 Unauthorized         │                          │
     │◄─────────────────────────────┤                          │
     │                              │                          │
     │                              │                          │
┌────▼─────────────────┐            │                          │
│  HTTP Interceptor    │            │                          │
│  • Catch 401         │            │                          │
│  • Prevent Multiple  │            │                          │
│    Refresh Calls     │            │                          │
└────┬─────────────────┘            │                          │
     │                              │                          │
     │  3. POST /api/auth/refresh   │                          │
     │     Body: { refreshToken }   │                          │
     │     Authorization: Bearer    │                          │
     │     <Expired Access Token>   │                          │
     ├─────────────────────────────►│                          │
     │                              │                          │
     │                              │  4. Validate Refresh     │
     │                              │     Token in DB          │
     │                              ├─────────────────────────►│
     │                              │                          │
     │                              │  • Token exists?         │
     │                              │  • Not expired?          │
     │                              │  • Not revoked?          │
     │                              │  • UserId matches?       │
     │                              │                          │
     │                              │  5. ✓ Valid              │
     │                              │◄─────────────────────────┤
     │                              │                          │
     │                        ┌─────▼─────┐                    │
     │                        │  Generate │                    │
     │                        │    New    │                    │
     │                        │   Tokens  │                    │
     │                        └─────┬─────┘                    │
     │                              │                          │
     │                              │  6. Revoke Old Refresh   │
     │                              │     Save New Refresh     │
     │                              ├─────────────────────────►│
     │                              │                          │
     │                              │  7. ✓ Saved              │
     │                              │◄─────────────────────────┤
     │                              │                          │
     │  8. 200 OK                   │                          │
     │     { accessToken,           │                          │
     │       refreshToken }         │                          │
     │◄─────────────────────────────┤                          │
     │                              │                          │
┌────▼─────────────────┐            │                          │
│  Store New Tokens    │            │                          │
│  in localStorage     │            │                          │
└────┬─────────────────┘            │                          │
     │                              │                          │
     │  9. Retry: GET /api/profile  │                          │
     │     Authorization: Bearer    │                          │
     │     <New Access Token>       │                          │
     ├─────────────────────────────►│                          │
     │                              │                          │
     │                        ┌─────▼─────┐                    │
     │                        │  Validate │                    │
     │                        │   Token   │                    │
     │                        └─────┬─────┘                    │
     │                              │                          │
     │                        Token Valid!                     │
     │                              │                          │
     │  10. 200 OK + Profile Data   │                          │
     │◄─────────────────────────────┤                          │
     │                              │                          │
```

### 4. Simplified Refresh Flow (Quick Reference)
```
Access Token Expired → 401 Error
         ↓
HTTP Interceptor Catches 401
         ↓
Send Refresh Token to /api/auth/refresh
         ↓
Backend Validates Refresh Token
         ↓
Generate New Access + Refresh Tokens
         ↓
Return New Tokens to Client
         ↓
Store New Tokens in localStorage
         ↓
Retry Original Request with New Access Token
         ↓
Success! ✓
```

---

## ASP.NET Core Web API Implementation

### 1. Install NuGet Package
```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

### 2. Token Models

**RefreshToken.cs**
```csharp
public class RefreshToken
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
}
```

**LoginRequest.cs / LoginResponse.cs**
```csharp
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; }
}
```

### 3. JWT Configuration (Program.cs)

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// In-memory refresh token store (use DB in production)
builder.Services.AddSingleton<IRefreshTokenStore, InMemoryRefreshTokenStore>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

**appsettings.json**
```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "YourAppName",
    "Audience": "YourAppName",
    "AccessTokenExpiryMinutes": 15,
    "RefreshTokenExpiryDays": 7
  }
}
```

### 4. Token Service

**ITokenService.cs**
```csharp
public interface ITokenService
{
    string GenerateAccessToken(string userId, string username);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateToken(string token);
}
```

**TokenService.cs**
```csharp
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(string userId, string username)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.UtcNow.AddMinutes(
            Convert.ToDouble(_configuration["Jwt:AccessTokenExpiryMinutes"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiry,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = false // Don't validate expiry for refresh
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}
```

### 5. Refresh Token Store (In-Memory Example)

**IRefreshTokenStore.cs**
```csharp
public interface IRefreshTokenStore
{
    Task SaveRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    Task RevokeRefreshTokenAsync(string token);
}
```

**InMemoryRefreshTokenStore.cs**
```csharp
public class InMemoryRefreshTokenStore : IRefreshTokenStore
{
    private readonly Dictionary<string, RefreshToken> _tokens = new();

    public Task SaveRefreshTokenAsync(RefreshToken refreshToken)
    {
        _tokens[refreshToken.Token] = refreshToken;
        return Task.CompletedTask;
    }

    public Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        _tokens.TryGetValue(token, out var refreshToken);
        return Task.FromResult(refreshToken);
    }

    public Task RevokeRefreshTokenAsync(string token)
    {
        if (_tokens.TryGetValue(token, out var refreshToken))
        {
            refreshToken.IsRevoked = true;
        }
        return Task.CompletedTask;
    }
}
```

### 6. Auth Controller

**AuthController.cs**
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly IConfiguration _configuration;

    public AuthController(
        ITokenService tokenService,
        IRefreshTokenStore refreshTokenStore,
        IConfiguration configuration)
    {
        _tokenService = tokenService;
        _refreshTokenStore = refreshTokenStore;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Validate user credentials (simplified - use Identity in production)
        if (request.Username != "admin" || request.Password != "password")
        {
            return Unauthorized("Invalid credentials");
        }

        var userId = "123"; // Get from database
        var accessToken = _tokenService.GenerateAccessToken(userId, request.Username);
        var refreshToken = _tokenService.GenerateRefreshToken();

        // Store refresh token
        var refreshTokenEntity = new RefreshToken
        {
            UserId = userId,
            Token = refreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(
                Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiryDays"])),
            IsRevoked = false
        };

        await _refreshTokenStore.SaveRefreshTokenAsync(refreshTokenEntity);

        return Ok(new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        // Validate refresh token exists and not expired/revoked
        var storedToken = await _refreshTokenStore.GetRefreshTokenAsync(request.RefreshToken);

        if (storedToken == null || 
            storedToken.IsRevoked || 
            storedToken.ExpiryDate < DateTime.UtcNow)
        {
            return Unauthorized("Invalid refresh token");
        }

        // Get user from old access token (sent in header - optional)
        var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var principal = _tokenService.ValidateToken(accessToken);

        if (principal == null)
        {
            return Unauthorized("Invalid access token");
        }

        var userId = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var username = principal.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

        // Verify userId matches
        if (userId != storedToken.UserId)
        {
            return Unauthorized("Token mismatch");
        }

        // Generate new tokens
        var newAccessToken = _tokenService.GenerateAccessToken(userId, username);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        // Revoke old refresh token
        await _refreshTokenStore.RevokeRefreshTokenAsync(request.RefreshToken);

        // Store new refresh token
        var newRefreshTokenEntity = new RefreshToken
        {
            UserId = userId,
            Token = newRefreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(
                Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiryDays"])),
            IsRevoked = false
        };

        await _refreshTokenStore.SaveRefreshTokenAsync(newRefreshTokenEntity);

        return Ok(new LoginResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
    {
        await _refreshTokenStore.RevokeRefreshTokenAsync(request.RefreshToken);
        return Ok("Logged out successfully");
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var username = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

        return Ok(new { UserId = userId, Username = username });
    }
}
```

### 7. Register Services in Program.cs

```csharp
// Add these lines after AddAuthorization()
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddSingleton<IRefreshTokenStore, InMemoryRefreshTokenStore>();
```

---

## Angular Implementation

### 1. Auth Models

**auth.models.ts**
```typescript
export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
}

export interface RefreshTokenRequest {
  refreshToken: string;
}
```

### 2. Auth Service

**auth.service.ts**
```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LoginRequest, LoginResponse } from './auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:5001/api/auth';
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasToken());

  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  constructor(private http: HttpClient) {}

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, credentials)
      .pipe(
        tap(response => {
          this.setTokens(response.accessToken, response.refreshToken);
          this.isAuthenticatedSubject.next(true);
        })
      );
  }

  refreshToken(): Observable<LoginResponse> {
    const refreshToken = this.getRefreshToken();
    return this.http.post<LoginResponse>(
      `${this.apiUrl}/refresh`,
      { refreshToken }
    ).pipe(
      tap(response => {
        this.setTokens(response.accessToken, response.refreshToken);
      })
    );
  }

  logout(): void {
    const refreshToken = this.getRefreshToken();
    if (refreshToken) {
      this.http.post(`${this.apiUrl}/logout`, { refreshToken }).subscribe();
    }
    this.clearTokens();
    this.isAuthenticatedSubject.next(false);
  }

  getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }

  private setTokens(accessToken: string, refreshToken: string): void {
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('refreshToken', refreshToken);
  }

  private clearTokens(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }

  private hasToken(): boolean {
    return !!this.getAccessToken();
  }
}
```

### 3. HTTP Interceptor (Token & Refresh Logic)

**auth.interceptor.ts**
```typescript
import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, switchMap, filter, take } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Add access token to request
    const accessToken = this.authService.getAccessToken();
    if (accessToken) {
      req = this.addTokenToRequest(req, accessToken);
    }

    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        // If 401 and not already refreshing, try to refresh token
        if (error.status === 401 && !req.url.includes('/refresh') && !req.url.includes('/login')) {
          return this.handle401Error(req, next);
        }
        return throwError(() => error);
      })
    );
  }

  private addTokenToRequest(request: HttpRequest<any>, token: string): HttpRequest<any> {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      return this.authService.refreshToken().pipe(
        switchMap((response) => {
          this.isRefreshing = false;
          this.refreshTokenSubject.next(response.accessToken);
          return next.handle(this.addTokenToRequest(request, response.accessToken));
        }),
        catchError((error) => {
          this.isRefreshing = false;
          this.authService.logout();
          return throwError(() => error);
        })
      );
    } else {
      // Wait for refresh to complete, then retry with new token
      return this.refreshTokenSubject.pipe(
        filter(token => token !== null),
        take(1),
        switchMap(token => next.handle(this.addTokenToRequest(request, token!)))
      );
    }
  }
}
```

### 4. Register Interceptor

**app.config.ts** (Standalone Component)
```typescript
import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideHttpClient(withInterceptors([authInterceptor]))
  ]
};
```

**OR app.module.ts** (NgModule)
```typescript
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';

@NgModule({
  imports: [HttpClientModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ]
})
export class AppModule {}
```

### 5. Login Component

**login.component.ts**
```typescript
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-login',
  template: `
    <div class="login-container">
      <h2>Login</h2>
      <form (ngSubmit)="onLogin()">
        <input 
          type="text" 
          [(ngModel)]="username" 
          name="username" 
          placeholder="Username"
          required>
        <input 
          type="password" 
          [(ngModel)]="password" 
          name="password" 
          placeholder="Password"
          required>
        <button type="submit">Login</button>
        <div *ngIf="errorMessage" class="error">{{ errorMessage }}</div>
      </form>
    </div>
  `
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onLogin(): void {
    this.authService.login({ username: this.username, password: this.password })
      .subscribe({
        next: () => {
          this.router.navigate(['/dashboard']);
        },
        error: (error) => {
          this.errorMessage = 'Invalid credentials';
          console.error('Login error:', error);
        }
      });
  }
}
```

### 6. Auth Guard

**auth.guard.ts**
```typescript
import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AuthService } from './auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.getAccessToken()) {
    return true;
  }

  router.navigate(['/login']);
  return false;
};
```

**Usage in routes:**
```typescript
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { 
    path: 'dashboard', 
    component: DashboardComponent,
    canActivate: [authGuard]
  }
];
```

---

## How Token Refresh Works (Step-by-Step)

### When User Makes an API Request:

1. **Access Token Attached**: Angular interceptor adds `Authorization: Bearer <access-token>` header
2. **Token Valid**: API processes request normally
3. **Token Expired**: API returns `401 Unauthorized`

### When 401 is Detected:

4. **Interceptor Catches 401**: HTTP interceptor detects the error
5. **Check if Refreshing**: Prevents multiple simultaneous refresh calls
6. **Call Refresh API**: Sends refresh token to `/api/auth/refresh`
7. **Backend Validates**:
   - Checks refresh token exists in store
   - Verifies not expired or revoked
   - Validates user identity
8. **Generate New Tokens**: Backend creates new access + refresh tokens
9. **Store New Tokens**: Angular saves new tokens in localStorage
10. **Retry Original Request**: Interceptor retries failed request with new access token
11. **Success**: Original API call succeeds

### Token Rotation (Security Enhancement):

- Each refresh generates **both** new access and refresh tokens
- Old refresh token is **revoked** immediately
- Prevents token reuse attacks

---

## Key Differences: Access Token vs Refresh Token

| Aspect | Access Token | Refresh Token |
|--------|-------------|---------------|
| **Lifespan** | Short (5-15 minutes) | Long (7-30 days) |
| **Purpose** | Authorize API requests | Get new access tokens |
| **Sent With** | Every API request | Only to refresh endpoint |
| **Stored** | localStorage/memory | localStorage (HttpOnly cookie better) |
| **Compromise Risk** | Low (expires quickly) | High (needs secure storage) |
| **Revocation** | Not stored (stateless) | Stored & can be revoked |

---

## Security Best Practices

### 1. Token Storage
```typescript
// ❌ Bad: XSS vulnerable
localStorage.setItem('refreshToken', token);

// ✅ Better: HttpOnly Cookie (set by backend)
// Cookie: refreshToken=xxx; HttpOnly; Secure; SameSite=Strict
```

### 2. Token Expiry
```csharp
// Access Token: 15 minutes
AccessTokenExpiryMinutes: 15

// Refresh Token: 7 days
RefreshTokenExpiryDays: 7
```

### 3. Refresh Token Rotation
```csharp
// Always generate new refresh token on refresh
var newRefreshToken = _tokenService.GenerateRefreshToken();

// Revoke old token immediately
await _refreshTokenStore.RevokeRefreshTokenAsync(oldRefreshToken);
```

### 4. Secure Communication
- Always use **HTTPS** in production
- Set `Secure` flag on cookies
- Use `SameSite=Strict` for CSRF protection

### 5. Token Revocation
```csharp
// Store refresh tokens to enable revocation
// Revoke on:
// - Logout
// - Password change
// - Suspicious activity
// - Token refresh (rotate)
```

### 6. Additional Security Measures
```typescript
// Angular: Clear tokens on tab close (session storage)
sessionStorage.setItem('accessToken', token); // Instead of localStorage

// Backend: Implement rate limiting on refresh endpoint
[RateLimit(PermitLimit = 10, Window = 1, TimeUnit = TimeUnit.Minute)]
```

### 7. Error Handling
```typescript
// Angular: Handle refresh failure gracefully
catchError((error) => {
  this.isRefreshing = false;
  this.authService.logout(); // Redirect to login
  this.router.navigate(['/login']);
  return throwError(() => error);
})
```

---

## Complete Flow Example

### Scenario: User accesses protected resource after access token expires

```
1. User clicks "View Profile" (Access Token expired)
   ↓
2. Angular sends GET /api/profile with expired token
   ↓
3. API validates token → Returns 401
   ↓
4. HTTP Interceptor catches 401
   ↓
5. Interceptor checks: Not already refreshing?
   ↓
6. Interceptor calls POST /api/auth/refresh
   Body: { refreshToken: "abc123..." }
   Header: Authorization: Bearer <expired-access-token>
   ↓
7. Backend validates refresh token:
   - Exists in database? ✓
   - Not expired? ✓
   - Not revoked? ✓
   - UserId matches? ✓
   ↓
8. Backend generates new tokens
   - New Access Token (expires in 15 min)
   - New Refresh Token (expires in 7 days)
   ↓
9. Backend revokes old refresh token
   ↓
10. Backend returns { accessToken: "new...", refreshToken: "new..." }
    ↓
11. Angular saves new tokens to localStorage
    ↓
12. Interceptor retries GET /api/profile with new access token
    ↓
13. API validates new token → Returns 200 OK
    ↓
14. User sees profile data
```

---

## Testing the Implementation

### 1. Test Login
```bash
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"password"}'

# Response:
# {
#   "accessToken": "eyJhbGc...",
#   "refreshToken": "xYz123..."
# }
```

### 2. Test Protected Endpoint
```bash
curl -X GET https://localhost:5001/api/auth/profile \
  -H "Authorization: Bearer eyJhbGc..."

# Response:
# { "userId": "123", "username": "admin" }
```

### 3. Test Refresh (after access token expires)
```bash
curl -X POST https://localhost:5001/api/auth/refresh \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <expired-access-token>" \
  -d '{"refreshToken":"xYz123..."}'

# Response:
# {
#   "accessToken": "eyJnew...",
#   "refreshToken": "aBc456..."
# }
```

---

## Common Pitfalls & Solutions

| Problem | Solution |
|---------|----------|
| Multiple refresh calls | Use `BehaviorSubject` to queue requests |
| Infinite refresh loop | Check URL in interceptor (exclude `/refresh`) |
| Lost refresh token | Store in HttpOnly cookie instead of localStorage |
| Token not attached | Ensure interceptor runs before request |
| 401 after refresh | Check refresh token expiry and revocation |

---

## Summary

This implementation provides:
- ✅ Secure JWT authentication
- ✅ Automatic token refresh
- ✅ Minimal boilerplate
- ✅ Production-ready patterns
- ✅ Token rotation for security
- ✅ Graceful error handling

**Remember**: This is a minimal example. For production:
- Use **Entity Framework** instead of in-memory store
- Store refresh tokens in **HttpOnly cookies**
- Implement **rate limiting**
- Add **logging and monitoring**
- Use **ASP.NET Core Identity** for user management
- Implement **two-factor authentication** (optional)
