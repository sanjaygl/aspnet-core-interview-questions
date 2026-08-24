# Angular Application

Angular frontend application for the Demo .NET Web API.

The application communicates with the .NET API using HTTP-only cookie-based authentication with Access Token and Refresh Token support.

---

# 1. Tech Stack

- Angular
- TypeScript
- RxJS
- Angular Router
- HttpClient
- HTTP Interceptors
- SCSS
- .NET Web API backend
- JWT Authentication
- HttpOnly Cookies

---

# 2. Project Setup

Install dependencies:

```bash id="9m3z0n"
npm install
```

Start the development server:

```bash id="l2k6g7"
ng serve
```

Or:

```bash id="2v9p8a"
npm start
```

Open:

```text id="j6xq7w"
http://localhost:4200
```

---

# 3. Useful Angular Commands

Check Angular CLI version:

```bash id="y9d2x4"
ng version
```

Create a component:

```bash id="m4h6k8"
ng generate component components/component-name
```

Short form:

```bash id="q7s3n1"
ng g c components/component-name
```

Create a service:

```bash id="r8k2p5"
ng generate service services/service-name
```

Short form:

```bash id="a6f1c9"
ng g s services/service-name
```

Create an interceptor:

```bash id="u5d8e2"
ng generate interceptor interceptors/auth
```

Build the application:

```bash id="p3x7m1"
ng build
```

Build for production:

```bash id="v6n2b8"
ng build --configuration production
```

Run tests:

```bash id="h8c4z5"
ng test
```

Run lint:

```bash id="k1m7q9"
ng lint
```

---

# 4. API Configuration

The Angular application communicates with the .NET API.

Example API URL:

```text id="d2f8r4"
https://localhost:44351
```

API endpoints are called using the configured API base URL.

Example:

```typescript id="s7h3k1"
this.http.get(`${apiUrl}/api/Order/my-orders`);
```

---

# 5. Authentication Architecture

The application uses:

```text id="w4n6p2"
Access Token
     +
Refresh Token
     +
HttpOnly Cookies
     +
HTTP Interceptor
```

Angular does **not** read or store the JWT tokens directly.

The browser manages the authentication cookies.

```text id="j3r8v5"
Angular
   │
   │ Login
   ▼
.NET API
   │
   ├── X-Access-Token
   └── X-Refresh-Token
           │
           ▼
        Browser
           │
           ▼
     HttpOnly Cookies
```

---

# 6. Login Flow

Angular sends:

```http id="c5m8x2"
POST /api/Auth/login
```

Example request:

```json id="q9r4t7"
{
  "username": "subpoch",
  "password": "password"
}
```

The API validates the credentials and sets:

```text id="e6y2p9"
X-Access-Token
X-Refresh-Token
```

as HttpOnly cookies.

Angular does not receive the raw token values in the response body.

---

# 7. `withCredentials`

Because authentication uses cookies, Angular requests must include:

```typescript id="b4k7m1"
withCredentials: true
```

Example:

```typescript id="n8v2c5"
this.http.get(
  `${apiUrl}/api/Order/my-orders`,
  {
    withCredentials: true
  }
);
```

This allows the browser to send the authentication cookies to the API.

---

# 8. Authentication Interceptor

The HTTP interceptor is responsible for:

1. Adding `withCredentials: true`.
2. Sending normal API requests.
3. Detecting `401 Unauthorized`.
4. Calling the refresh endpoint.
5. Retrying the original request.
6. Logging out when refresh fails.

Flow:

```text id="x6k2m8"
Angular Request
      │
      ▼
HTTP Interceptor
      │
      ├── withCredentials: true
      │
      ▼
.NET API
      │
      ├── 200 → return response
      │
      └── 401
            │
            ▼
      POST /api/Auth/refresh
            │
       ┌────┴────┐
       │         │
      200       401
       │         │
       ▼         ▼
    Retry      Logout
    Request
```

---

# 9. Access Token Flow

The Access Token is stored by the browser in:

```text id="e3c8h6"
X-Access-Token
```

Lifetime:

```text id="v2m7q1"
15 minutes
```

Angular does not access this cookie because it is:

```text id="z6p4r8"
HttpOnly
```

Normal API requests simply use:

```typescript id="a9x5k3"
withCredentials: true
```

The browser automatically sends the cookie.

---

# 10. Refresh Token Flow

The Refresh Token is stored by the browser in:

```text id="p8w3d6"
X-Refresh-Token
```

Lifetime:

```text id="f4y7n2"
7 days
```

When the Access Token expires:

```text id="h5q9c2"
API Request
    │
    ▼
401 Unauthorized
    │
    ▼
Angular Interceptor
    │
    ▼
POST /api/Auth/refresh
    │
    ▼
Browser sends:
    X-Access-Token
    X-Refresh-Token
    │
    ▼
.NET API
    │
    ├── Validate refresh token
    ├── Generate new Access Token
    ├── Generate new Refresh Token
    └── Update cookies
    │
    ▼
Angular retries original request
```

---

# 11. Refresh Token Rotation

After successful refresh, the API generates a new Refresh Token.

```text id="n3x7b1"
Old Refresh Token
       │
       ▼
   API validates
       │
       ▼
New Refresh Token
       │
       ├── Browser Cookie
       └── Database UserSession
```

Angular does not need to manually update the token.

The browser automatically receives the new cookies from the API response.

---

# 12. HTTP Interceptor Example

The interceptor should ensure credentials are included:

```typescript id="k4p8m2"
intercept(
  req: HttpRequest<any>,
  next: HttpHandler
): Observable<HttpEvent<any>> {

  const request = req.clone({
    withCredentials: true
  });

  return next.handle(request);
}
```

The interceptor can also handle `401` responses by calling the refresh endpoint and retrying the original request.

---

# 13. Important: Do Not Store JWT in Local Storage

Do not do this:

```typescript id="r7m2q4"
localStorage.setItem('accessToken', token);
```

Do not do this either:

```typescript id="x5n8c1"
sessionStorage.setItem('accessToken', token);
```

The application uses:

```text id="d9k3w6"
HttpOnly Cookie
```

instead.

This prevents JavaScript from directly accessing the authentication tokens.

---

# 14. API Request Flow

Normal authenticated request:

```text id="q3v7m9"
Angular
   │
   │ withCredentials: true
   ▼
Browser
   │
   └── X-Access-Token
   │
   ▼
.NET API
   │
   ▼
JWT Authentication
   │
   ▼
[Authorize]
   │
   ▼
Controller
```

---

# 15. Authentication Endpoints

## Login

```http id="f8k2n5"
POST /api/Auth/login
```

## Register

```http id="m7p3x9"
POST /api/Auth/register
```

## Refresh

```http id="c4w8d2"
POST /api/Auth/refresh
```

## Logout

If the application provides a logout endpoint, it should clear the authentication cookies on the API side.

---

# 16. Example Auth Service

Login:

```typescript id="b6q1r8"
login(credentials: LoginRequest) {
  return this.http.post<AuthResponse>(
    `${this.apiUrl}/api/Auth/login`,
    credentials,
    {
      withCredentials: true
    }
  );
}
```

Refresh:

```typescript id="n9v4k2"
refreshToken() {
  return this.http.post<AuthResponse>(
    `${this.apiUrl}/api/Auth/refresh`,
    {},
    {
      withCredentials: true
    }
  );
}
```

Logout:

```typescript id="p5c8x3"
logout() {
  return this.http.post(
    `${this.apiUrl}/api/Auth/logout`,
    {},
    {
      withCredentials: true
    }
  );
}
```

---

# 17. Protected API Example

Example service:

```typescript id="r2m6v8"
getMyOrders() {
  return this.http.get<Order[]>(
    `${this.apiUrl}/api/Order/my-orders`,
    {
      withCredentials: true
    }
  );
}
```

If the request receives `401`, the interceptor handles the refresh flow.

---

# 18. Authentication State

Angular should not treat the JWT itself as the authentication state.

The backend is the source of truth.

Example:

```text id="w8f3k6"
Angular
   │
   ▼
GET /api/Auth/me
   │
   ▼
.NET API
   │
   ▼
ClaimsPrincipal
   │
   ├── Username
   ├── Email
   └── Role
```

For example:

```typescript id="x4p7n2"
getCurrentUser() {
  return this.http.get<User>(
    `${this.apiUrl}/api/Auth/me`,
    {
      withCredentials: true
    }
  );
}
```

---

# 19. Error Handling

Common authentication responses:

```text id="k6v2m9"
200 OK
    → Request successful

401 Unauthorized
    → Access Token invalid/expired

403 Forbidden
    → User authenticated but not authorized

429 Too Many Requests
    → Rate limit exceeded
```

The interceptor should only attempt token refresh for `401` responses.

---

# 20. Development Commands

Install dependencies:

```bash id="c8r5m1"
npm install
```

Start development server:

```bash id="j2v7n4"
ng serve
```

Start on a specific port:

```bash id="q6x9p3"
ng serve --port 4200
```

Build:

```bash id="f3k8w2"
ng build
```

Production build:

```bash id="m5n1c7"
ng build --configuration production
```

Run unit tests:

```bash id="v9r4d6"
ng test
```

Run tests without watch mode:

```bash id="p2k7x5"
ng test --watch=false
```

Run lint:

```bash id="h4c8n3"
ng lint
```

---

# 21. Useful Angular CLI Commands

Create component:

```bash id="a7m3q9"
ng g c components/component-name
```

Create service:

```bash id="b5x8r2"
ng g s services/service-name
```

Create interceptor:

```bash id="n6k2p4"
ng g interceptor interceptors/auth
```

Create guard:

```bash id="w3c7v9"
ng g guard guards/auth
```

Create interface:

```bash id="r8m4x1"
ng g interface models/user
```

---

# 22. Browser Debugging

Authentication cookies can be inspected from:

```text id="f7n2k5"
Chrome DevTools
→ Application
→ Storage
→ Cookies
```

Expected cookies:

```text id="c3v8m6"
X-Access-Token
X-Refresh-Token
```

Because they are HttpOnly, JavaScript cannot access their values.

---

# 23. Network Debugging

Open:

```text id="q9x4b2"
Chrome DevTools
→ Network
```

For a protected API request:

```text id="d6m1r8"
Request Headers
    ↓
Cookie
    ↓
X-Access-Token=...
X-Refresh-Token=...
```

When the Access Token expires, you should see:

```text id="n5k7p3"
my-orders       → 401
refresh         → 200
my-orders       → 200
```

This confirms that the interceptor successfully refreshed the token and retried the request.

---

# 24. Complete Angular Authentication Flow

```text id="v8q3m7"
                     LOGIN
                       │
                       ▼
                Angular AuthService
                       │
                       ▼
              POST /api/Auth/login
                       │
                       ▼
                  .NET API
                       │
             ┌─────────┴─────────┐
             ▼                   ▼
      Access Token        Refresh Token
        15 minutes           7 days
             │                   │
             └─────────┬─────────┘
                       ▼
                 HttpOnly Cookies
                       │
                       ▼
              Normal API Request
                       │
                       ▼
                withCredentials
                       │
                       ▼
                 .NET API
                       │
                  ┌────┴────┐
                  │         │
                 200       401
                  │         │
                  │         ▼
                  │    HTTP Interceptor
                  │         │
                  │         ▼
                  │   POST /refresh
                  │         │
                  │         ▼
                  │   New Cookies
                  │         │
                  │         ▼
                  │   Retry Request
                  │         │
                  └────┬────┘
                       ▼
                    200 OK
```

---

# 25. Security Rules

- Do not store JWT tokens in `localStorage`.
- Do not store JWT tokens in `sessionStorage`.
- Do not manually read HttpOnly authentication cookies.
- Always use `withCredentials: true` for authenticated API calls.
- Let the browser manage authentication cookies.
- Let the API validate JWTs.
- Let the API rotate Refresh Tokens.
- Handle `401` through the HTTP interceptor.
- Do not expose Access Tokens or Refresh Tokens in UI logs.
- Do not log token values in the browser console.
- Use HTTPS when running the API with secure cookies.

---

# 26. Authentication Responsibilities

```text id="j4n8p2"
Angular
│
├── Login request
├── Register request
├── withCredentials
├── HTTP interceptor
├── Detect 401
├── Call refresh endpoint
├── Retry original request
└── Handle logout/session state

.NET API
│
├── Validate credentials
├── Generate Access Token
├── Generate Refresh Token
├── Set HttpOnly cookies
├── Validate JWT
├── Validate Refresh Token
├── Rotate tokens
├── Store Refresh Token
└── Authorize API requests

Browser
│
├── Store HttpOnly cookies
└── Automatically send cookies
```

---

# 27. Quick Reference

| Item | Value |
|---|---|
| Angular URL | `http://localhost:4200` |
| Access Token Cookie | `X-Access-Token` |
| Access Token Lifetime | 15 minutes |
| Refresh Token Cookie | `X-Refresh-Token` |
| Refresh Token Lifetime | 7 days |
| Cookie Type | HttpOnly |
| Cookie Credentials | `withCredentials: true` |
| Login | `POST /api/Auth/login` |
| Refresh | `POST /api/Auth/refresh` |
| Authentication Error | `401 Unauthorized` |
| Authorization Error | `403 Forbidden` |
| Rate Limit Error | `429 Too Many Requests` |

---

# 28. End-to-End Authentication Test

1. Start PostgreSQL.
2. Start the .NET API.
3. Start Angular.
4. Open `http://localhost:4200`.
5. Login.
6. Verify `X-Access-Token` and `X-Refresh-Token` cookies.
7. Call a protected API.
8. Verify the API returns `200 OK`.
9. Allow the Access Token to expire.
10. Call the protected API again.
11. Verify the first request returns `401`.
12. Verify Angular calls `/api/Auth/refresh`.
13. Verify refresh returns `200`.
14. Verify the original request is retried.
15. Verify the retried request returns `200`.