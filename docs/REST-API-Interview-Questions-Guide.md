# REST API Interview Questions - Complete Guide

A comprehensive collection of 50 REST API interview questions with real-world HTTP examples, best practices, and production-oriented explanations for mid-to-senior level backend developers.

---

## 📋 Table of Contents

### Batch 1: REST Fundamentals & HTTP Methods (Questions 1-10)

01. [What is REST and what are its core constraints?](#1-what-is-rest-and-what-are-its-core-constraints)
02. [What are the main HTTP methods and their purposes?](#2-what-are-the-main-http-methods-and-their-purposes)
03. [What is idempotency in REST APIs?](#3-what-is-idempotency-in-rest-apis)
04. [What does it mean for REST APIs to be stateless?](#4-what-does-it-mean-for-rest-apis-to-be-stateless)
05. [What is the difference between REST and SOAP?](#5-what-is-the-difference-between-rest-and-soap)
06. [What are HTTP status codes and when should you use them?](#6-what-are-http-status-codes-and-when-should-you-use-them)
07. [What is the difference between safe and unsafe HTTP methods?](#7-what-is-the-difference-between-safe-and-unsafe-http-methods)
08. [What is the difference between idempotent and non-idempotent methods?](#8-what-is-the-difference-between-idempotent-and-non-idempotent-methods)
09. [What is HATEOAS in REST?](#9-what-is-hateoas-in-rest)
10. [What are REST resource naming best practices?](#10-what-are-rest-resource-naming-best-practices)

### Batch 2: Authentication, Authorization & Security (Questions 11-20)

11. [What is the difference between Authentication and Authorization?](#11-what-is-the-difference-between-authentication-and-authorization)
12. [What is JWT and how does it work?](#12-what-is-jwt-and-how-does-it-work)
13. [What is OAuth 2.0 and when should you use it?](#13-what-is-oauth-20-and-when-should-you-use-it)
14. [What is rate limiting and why is it important?](#14-what-is-rate-limiting-and-why-is-it-important)
15. [What are the different API versioning strategies?](#15-what-are-the-different-api-versioning-strategies)
16. [What is CORS and why is it needed?](#16-what-is-cors-and-why-is-it-needed)
17. [What is the difference between API Keys and Bearer Tokens?](#17-what-is-the-difference-between-api-keys-and-bearer-tokens)
18. [How do you implement pagination in REST APIs?](#18-how-do-you-implement-pagination-in-rest-apis)
19. [What are HTTP caching strategies?](#19-what-are-http-caching-strategies)
20. [What is the difference between webhooks and polling?](#20-what-is-the-difference-between-webhooks-and-polling)

### Batch 3: API Design, Best Practices & Error Handling (Questions 21-30)

21. [What are REST API URI design best practices?](#21-what-are-rest-api-uri-design-best-practices)
22. [How should you handle errors in REST APIs?](#22-how-should-you-handle-errors-in-rest-apis)
23. [What is the difference between PUT and POST?](#23-what-is-the-difference-between-put-and-post)
24. [What is the difference between synchronous and asynchronous APIs?](#24-what-is-the-difference-between-synchronous-and-asynchronous-apis)
25. [What is an API Gateway and what are its benefits?](#25-what-is-an-api-gateway-and-what-are-its-benefits)
26. [What is the difference between throttling and rate limiting?](#26-what-is-the-difference-between-throttling-and-rate-limiting)
27. [How do you document REST APIs?](#27-how-do-you-document-rest-apis)
28. [What are REST API security best practices?](#28-what-are-rest-api-security-best-practices)
29. [How do you implement API monitoring and observability?](#29-how-do-you-implement-api-monitoring-and-observability)
30. [What is API deprecation and how do you handle it?](#30-what-is-api-deprecation-and-how-do-you-handle-it)

### Batch 4: Advanced Topics & Performance (Questions 31-40)

31. [What is the difference between Monolithic and Microservices API architecture?](#31-what-is-the-difference-between-monolithic-and-microservices-api-architecture)
32. [What is the difference between GraphQL and REST?](#32-what-is-the-difference-between-graphql-and-rest)
33. [What is the API Gateway pattern and when should you use it?](#33-what-is-the-api-gateway-pattern-and-when-should-you-use-it)
34. [How do you implement idempotency in distributed systems?](#34-how-do-you-implement-idempotency-in-distributed-systems)
35. [What are the different HTTP status code categories and when to use them?](#35-what-are-the-different-http-status-code-categories-and-when-to-use-them)
36. [How do you handle file uploads in REST APIs?](#36-how-do-you-handle-file-uploads-in-rest-apis)
37. [How do you handle API testing and what types of tests should you implement?](#37-how-do-you-handle-api-testing-and-what-types-of-tests-should-you-implement)
38. [How do you design APIs for mobile vs web clients?](#38-how-do-you-design-apis-for-mobile-vs-web-clients)
39. [What are common API design mistakes and how to avoid them?](#39-what-are-common-api-design-mistakes-and-how-to-avoid-them)
40. [How do you handle backwards compatibility when evolving REST APIs?](#40-how-do-you-handle-backwards-compatibility-when-evolving-rest-apis)

### Batch 5: Real-World Scenarios & Best Practices (Questions 41-50)

41. [How do you implement request/response compression in REST APIs?](#41-how-do-you-implement-requestresponse-compression-in-rest-apis)
42. [What is Content Negotiation and how does it work?](#42-what-is-content-negotiation-and-how-does-it-work)
43. [How do you implement bulk operations in REST APIs?](#43-how-do-you-implement-bulk-operations-in-rest-apis)
44. [What are Long Polling, Server-Sent Events (SSE), and WebSockets?](#44-what-are-long-polling-server-sent-events-sse-and-websockets)
45. [How do you handle API timeouts and retries?](#45-how-do-you-handle-api-timeouts-and-retries)
46. [What is the Circuit Breaker pattern and when should you use it?](#46-what-is-the-circuit-breaker-pattern-and-when-should-you-use-it)
47. [How do you implement API request validation?](#47-how-do-you-implement-api-request-validation)
48. [What are conditional requests (If-Match, If-None-Match, ETag)?](#48-what-are-conditional-requests-if-match-if-none-match-etag)
49. [How do you handle API performance optimization?](#49-how-do-you-handle-api-performance-optimization)
50. [What are the best practices for REST API logging and debugging?](#50-what-are-the-best-practices-for-rest-api-logging-and-debugging)

---

## Batch 1: REST Fundamentals & HTTP Methods (Questions 1-10)

---

### 1. What is REST and what are its core constraints?

**REST (Representational State Transfer)** is an architectural style for designing networked applications. It defines a set of constraints that make web services scalable, maintainable, and loosely coupled.

**Why it's needed:** REST provides a standardized way to build APIs that are predictable, cacheable, and stateless, making them easier to scale horizontally and integrate with various clients.

**When to use:** Use REST for building public APIs, microservices, mobile backends, and any distributed system requiring stateless communication over HTTP.

**The 6 REST Constraints:**
01. **Client-Server Architecture** - Separation of concerns between UI and data storage
02. **Stateless** - Each request contains all information needed; no session stored on server
03. **Cacheable** - Responses must define themselves as cacheable or non-cacheable
04. **Uniform Interface** - Standardized way of communicating (URIs, HTTP methods, representations)
05. **Layered System** - Client cannot tell if connected directly to server or through intermediary
06. **Code on Demand (Optional)** - Server can extend client functionality by transferring executable code

**Real-time Example:**

```http
GET /api/customers/12345 HTTP/1.1
Host: api.company.com
Authorization: Bearer eyJhbGc...

HTTP/1.1 200 OK
Content-Type: application/json
Cache-Control: max-age=3600

{
  "customerId": 12345,
  "name": "John Doe",
  "email": "john.doe@example.com",
  "status": "active"
}
```

---

### 2. Explain the difference between PUT and PATCH methods

**PUT** is used to completely replace a resource, while **PATCH** is used to partially update a resource. Understanding this distinction is critical for proper API design and avoiding unintended data loss.

**Why it's needed:** Different use cases require different update strategies. Full replacement (PUT) vs partial updates (PATCH) affects bandwidth, client logic, and data integrity.

**When to use:** Use PUT when clients send complete resource representations. Use PATCH when updating specific fields without affecting others (mobile apps with limited bandwidth, partial form updates).

| Aspect | PUT | PATCH |
|--------|-----|-------|
| **Purpose** | Complete replacement of resource | Partial modification of resource |
| **Idempotency** | Idempotent (multiple identical requests have same effect) | Can be idempotent but not guaranteed |
| **Request Body** | Must contain complete resource representation | Contains only fields to be updated |
| **Missing Fields** | Missing fields set to null/default | Missing fields remain unchanged |
| **Bandwidth** | Higher (sends entire object) | Lower (sends only changed fields) |

**Real-time Example:**

**PUT Request (Complete Replacement):**

```http
PUT /api/employees/789 HTTP/1.1
Content-Type: application/json

{
  "employeeId": 789,
  "firstName": "Jane",
  "lastName": "Smith",
  "department": "Engineering",
  "salary": 95000,
  "email": "jane.smith@company.com"
}
```

**PATCH Request (Partial Update):**

```http
PATCH /api/employees/789 HTTP/1.1
Content-Type: application/json

{
  "department": "Management",
  "salary": 105000
}
```

---

### 3. What is idempotency in REST APIs and which HTTP methods are idempotent?

**Idempotency** means that making multiple identical requests produces the same result as making a single request. The server state will be the same regardless of how many times the request is repeated.

**Why it's needed:** Network failures, timeouts, and retries are common in distributed systems. Idempotent operations ensure that retrying failed requests doesn't cause unintended side effects or duplicate actions.

**When to use:** Design all safe operations to be idempotent. Critical for payment systems, order processing, and any operation where duplicate execution could cause issues.

**Idempotent HTTP Methods:**
* **GET** - Reading data multiple times doesn't change it
* **PUT** - Replacing a resource with same data produces same result
* **DELETE** - Deleting already deleted resource has no additional effect
* **HEAD** - Same as GET, only retrieves headers
* **OPTIONS** - Querying supported methods doesn't change state

**Non-Idempotent HTTP Methods:**
* **POST** - Creating resources multiple times creates multiple instances
* **PATCH** - Depending on implementation, may or may not be idempotent

**Real-time Example:**

```http
# Idempotent DELETE - First call deletes, subsequent calls return 404
DELETE /api/orders/5001 HTTP/1.1

# First Response:
HTTP/1.1 204 No Content

# Second Response (idempotent - same result):
HTTP/1.1 404 Not Found

---

# Non-Idempotent POST - Each call creates new resource
POST /api/orders HTTP/1.1
Content-Type: application/json

{
  "customerId": 123,
  "items": [{"productId": 456, "quantity": 2}],
  "total": 199.99
}

# Each call creates a new order with different ID
# Response 1: {"orderId": 5001, ...}
# Response 2: {"orderId": 5002, ...}
# Response 3: {"orderId": 5003, ...}
```

---

### 4. Explain statelessness in REST APIs

**Statelessness** means the server doesn't store any client context between requests. Each request must contain all information necessary to process it, including authentication credentials, parameters, and data.

**Why it's needed:** Stateless design enables horizontal scalability, simplifies server architecture, improves reliability (no session state to lose), and makes load balancing straightforward since any server can handle any request.

**When to use:** Always design REST APIs to be stateless. Store session data on the client (JWT tokens) or in external stores (Redis, databases) rather than in server memory.

**Stateless vs Stateful Comparison:**
* **Stateless:** Token in each request → Server validates → Processes request
* **Stateful:** Session ID in request → Server looks up session → Processes request

**Real-time Example:**

**Stateless Approach (Correct REST):**

```http
# Request 1 - Get user profile
GET /api/users/profile HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Host: api.company.com

# Request 2 - Update preferences (to different server)
PUT /api/users/preferences HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "theme": "dark",
  "notifications": true
}

# Each request contains all auth info (JWT)
# Any server can handle any request
# JWT contains: userId, roles, expiration
```

**Stateful Approach (Violates REST):**

```http
# Login creates server-side session
POST /api/login
Response: Set-Cookie: SESSIONID=abc123

# Subsequent requests depend on server remembering session
GET /api/users/profile
Cookie: SESSIONID=abc123

# Problem: Load balancer must route to same server (sticky sessions)
# Problem: Session data lost if server restarts
```

---

### 5. What are the main differences between REST and SOAP?

**REST** (Representational State Transfer) is an architectural style using HTTP protocols, while **SOAP** (Simple Object Access Protocol) is a protocol with strict standards. REST is lightweight and flexible; SOAP is heavyweight but offers built-in security and transaction support.

**Why it's needed:** Understanding when to use REST vs SOAP impacts performance, security requirements, and development complexity. REST suits most modern web services; SOAP fits enterprise scenarios requiring ACID transactions and WS-Security.

**When to use REST:** Public APIs, mobile backends, microservices, high-performance scenarios. **When to use SOAP:** Banking transactions, payment gateways, enterprise integration requiring ACID compliance and WS-Security standards.

| Aspect | REST | SOAP |
|--------|------|------|
| **Architecture** | Architectural style with guidelines | Strict protocol with specifications |
| **Format** | JSON, XML, HTML, plain text | XML only |
| **Transport** | HTTP/HTTPS primarily | HTTP, SMTP, TCP, JMS, and more |
| **Performance** | Lightweight, faster, less bandwidth | Heavyweight, slower, more bandwidth |
| **Security** | HTTPS, OAuth, JWT (custom implementation) | Built-in WS-Security standard |
| **State** | Stateless | Can be stateful or stateless |
| **Caching** | Supports HTTP caching | No built-in caching |
| **Error Handling** | HTTP status codes | Fault element in SOAP message |

**Real-time Example:**

**REST API Request:**

```http
POST /api/payments HTTP/1.1
Host: api.payment.com
Authorization: Bearer token123
Content-Type: application/json

{
  "orderId": "ORD-789",
  "amount": 250.00,
  "currency": "USD"
}

HTTP/1.1 201 Created
{
  "transactionId": "TXN-456",
  "status": "completed"
}
```

**SOAP API Request:**

```xml
POST /PaymentService HTTP/1.1
Host: api.payment.com
Content-Type: text/xml

<?xml version="1.0"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <ProcessPayment>
      <OrderId>ORD-789</OrderId>
      <Amount>250.00</Amount>
      <Currency>USD</Currency>
    </ProcessPayment>
  </soap:Body>
</soap:Envelope>
```

---

### 6. Explain HTTP status codes and their categories

**HTTP status codes** are three-digit numbers returned by servers to indicate the result of a client's request. They provide standardized communication about success, failure, or required actions.

**Why it's needed:** Status codes enable clients to programmatically handle different scenarios (success, errors, redirects) without parsing response bodies. They're essential for proper error handling and API contracts.

**When to use:** Always return appropriate status codes. Helps client applications implement retry logic, error handling, caching, and user feedback mechanisms.

**Status Code Categories:**
* **1xx (Informational)** - Request received, continuing process
* **2xx (Success)** - Request successfully received, understood, and accepted
* **3xx (Redirection)** - Further action needed to complete the request
* **4xx (Client Error)** - Request contains bad syntax or cannot be fulfilled
* **5xx (Server Error)** - Server failed to fulfill valid request

**Common Status Codes:**

| Code | Name | Meaning | Use Case |
|------|------|---------|----------|
| **200** | OK | Request succeeded | GET, PUT, PATCH success |
| **201** | Created | Resource created successfully | POST creating new resource |
| **204** | No Content | Success but no content to return | DELETE success |
| **400** | Bad Request | Invalid request syntax/data | Validation errors |
| **401** | Unauthorized | Authentication required/failed | Missing or invalid token |
| **403** | Forbidden | Authenticated but no permission | Access denied |
| **404** | Not Found | Resource doesn't exist | Invalid resource ID |
| **409** | Conflict | Request conflicts with current state | Duplicate creation |
| **429** | Too Many Requests | Rate limit exceeded | Throttling |
| **500** | Internal Server Error | Server encountered error | Unhandled exception |
| **503** | Service Unavailable | Server temporarily unavailable | Maintenance mode |

**Real-time Example:**

```http
# 201 Created - New customer registration
POST /api/customers HTTP/1.1
Content-Type: application/json

{"name": "Alice Brown", "email": "alice@example.com"}

HTTP/1.1 201 Created
Location: /api/customers/9876
{"customerId": 9876, "name": "Alice Brown"}

---

# 400 Bad Request - Invalid email format
POST /api/customers HTTP/1.1
{"name": "Bob", "email": "invalid-email"}

HTTP/1.1 400 Bad Request
{
  "error": "ValidationError",
  "message": "Invalid email format",
  "field": "email"
}

---

# 404 Not Found - Resource doesn't exist
GET /api/customers/99999 HTTP/1.1

HTTP/1.1 404 Not Found
{
  "error": "NotFound",
  "message": "Customer with ID 99999 not found"
}

---

# 409 Conflict - Duplicate resource
POST /api/customers HTTP/1.1
{"email": "alice@example.com"}

HTTP/1.1 409 Conflict
{
  "error": "DuplicateResource",
  "message": "Customer with email already exists"
}
```

---

### 7. What is the difference between 401 Unauthorized and 403 Forbidden?

Both **401** and **403** are client error codes, but they represent different security scenarios. **401** means authentication is missing or invalid (who are you?), while **403** means authentication succeeded but authorization failed (you can't do that).

**Why it's needed:** Proper distinction helps clients implement correct error handling - 401 triggers re-authentication, 403 indicates a permissions issue that re-authentication won't fix.

**When to use:** Return 401 when JWT is missing, expired, or invalid. Return 403 when user is authenticated but lacks required roles/permissions for the requested resource or action.

| Aspect | 401 Unauthorized | 403 Forbidden |
|--------|------------------|---------------|
| **Meaning** | Authentication required or failed | Authenticated but not authorized |
| **Problem** | Identity not verified (Who are you?) | Permission denied (You can't access this) |
| **Client Action** | Prompt for login/authentication | Show "access denied" message |
| **Can Retry?** | Yes, with valid credentials | No, same credentials won't help |
| **WWW-Authenticate Header** | Must include this header | Not required |
| **Common Causes** | Missing token, expired token, invalid token | Insufficient role, resource ownership, feature access |

**Real-time Example:**

**401 Unauthorized Scenarios:**

```http
# No authentication token provided
GET /api/orders/5001 HTTP/1.1
Host: api.company.com

HTTP/1.1 401 Unauthorized
WWW-Authenticate: Bearer realm="api"
{
  "error": "Unauthorized",
  "message": "Authentication token is required"
}

---

# Expired token
GET /api/orders/5001 HTTP/1.1
Authorization: Bearer expired_token_xyz

HTTP/1.1 401 Unauthorized
WWW-Authenticate: Bearer error="invalid_token"
{
  "error": "TokenExpired",
  "message": "Authentication token has expired"
}
```

**403 Forbidden Scenarios:**

```http
# User authenticated but trying to access another user's order
GET /api/orders/5001 HTTP/1.1
Authorization: Bearer valid_token_user123

HTTP/1.1 403 Forbidden
{
  "error": "Forbidden",
  "message": "You don't have permission to access this order"
}

---

# User lacks required role
DELETE /api/users/456 HTTP/1.1
Authorization: Bearer valid_token_regular_user

HTTP/1.1 403 Forbidden
{
  "error": "InsufficientPermissions",
  "message": "Admin role required for this operation"
}
```

---

### 8. What is Content Negotiation in REST APIs?

**Content Negotiation** is the mechanism where client and server agree on the format of data exchange (JSON, XML, HTML) and other characteristics like language and encoding. The client specifies preferences using HTTP headers, and the server responds accordingly.

**Why it's needed:** Different clients have different capabilities and preferences. Mobile apps prefer JSON, legacy systems might need XML, browsers need HTML. Content negotiation enables one API to serve multiple client types efficiently.

**When to use:** Implement when API serves multiple client types, supports internationalization, or needs to provide data in different formats (public APIs, B2B integrations, multilingual applications).

**Key Headers:**
* **Accept**: Client specifies desired response format
* **Accept-Language**: Client specifies preferred language
* **Accept-Encoding**: Client specifies compression (gzip, deflate)
* **Content-Type**: Server/client specifies actual format sent

**Real-time Example:**

```http
# Client requests JSON format
GET /api/vehicles/VH-789 HTTP/1.1
Host: api.fleet.com
Accept: application/json
Accept-Language: en-US
Authorization: Bearer token123

HTTP/1.1 200 OK
Content-Type: application/json
Content-Language: en-US

{
  "vehicleId": "VH-789",
  "make": "Toyota",
  "model": "Camry",
  "year": 2024,
  "status": "available"
}

---

# Client requests XML format
GET /api/vehicles/VH-789 HTTP/1.1
Accept: application/xml
Accept-Language: es

HTTP/1.1 200 OK
Content-Type: application/xml
Content-Language: es

<?xml version="1.0"?>
<Vehicle>
  <VehicleId>VH-789</VehicleId>
  <Make>Toyota</Make>
  <Model>Camry</Model>
  <Year>2024</Year>
  <Status>disponible</Status>
</Vehicle>

---

# Server cannot provide requested format
GET /api/vehicles/VH-789 HTTP/1.1
Accept: application/pdf

HTTP/1.1 406 Not Acceptable
{
  "error": "NotAcceptable",
  "message": "Supported formats: application/json, application/xml",
  "supported": ["application/json", "application/xml"]
}
```

---

### 9. Explain the concept of HATEOAS (Hypermedia as the Engine of Application State)

**HATEOAS** is a REST constraint where API responses include hyperlinks to related resources and available actions, making the API self-descriptive and discoverable. Clients navigate the API by following these links rather than constructing URLs.

**Why it's needed:** HATEOAS decouples clients from hardcoded URLs, enables API evolution without breaking clients, and makes APIs self-documenting. Clients discover available operations dynamically based on current resource state.

**When to use:** Implement in mature APIs where versioning flexibility is crucial, public APIs requiring discoverability, or complex workflows where available actions depend on resource state (order processing, approval workflows).

**Real-time Example:**

```http
# Get order details with HATEOAS links
GET /api/orders/ORD-5001 HTTP/1.1
Authorization: Bearer token123

HTTP/1.1 200 OK
Content-Type: application/json

{
  "orderId": "ORD-5001",
  "customerId": 123,
  "status": "pending",
  "total": 299.99,
  "items": [
    {
      "productId": 456,
      "quantity": 2,
      "price": 149.99
    }
  ],
  "_links": {
    "self": {
      "href": "/api/orders/ORD-5001"
    },
    "customer": {
      "href": "/api/customers/123"
    },
    "cancel": {
      "href": "/api/orders/ORD-5001/cancel",
      "method": "POST"
    },
    "payment": {
      "href": "/api/orders/ORD-5001/payment",
      "method": "POST"
    }
  }
}

---

# After payment, available actions change
GET /api/orders/ORD-5001 HTTP/1.1

HTTP/1.1 200 OK
{
  "orderId": "ORD-5001",
  "status": "paid",
  "total": 299.99,
  "_links": {
    "self": {
      "href": "/api/orders/ORD-5001"
    },
    "customer": {
      "href": "/api/customers/123"
    },
    "ship": {
      "href": "/api/orders/ORD-5001/ship",
      "method": "POST"
    },
    "invoice": {
      "href": "/api/orders/ORD-5001/invoice",
      "method": "GET"
    }
  }
}

# Notice: 'cancel' and 'payment' links removed, 'ship' and 'invoice' added
# Client doesn't hardcode URLs or business rules
```

---

### 10. What are safe and unsafe HTTP methods?

**Safe methods** are HTTP operations that don't modify server state (read-only), while **unsafe methods** change server state (create, update, delete). This distinction is crucial for caching, retry logic, and understanding side effects.

**Why it's needed:** Browsers and proxies cache safe methods aggressively and allow prefetching. Safe methods can be retried without concern. Unsafe methods require extra caution (CSRF protection, idempotency tokens, confirmation dialogs).

**When to use:** Use safe methods (GET, HEAD, OPTIONS) for retrievals and metadata queries. Use unsafe methods (POST, PUT, PATCH, DELETE) for state-changing operations. Never use GET for operations that modify data.

**Method Classification:**

| Method | Safe? | Idempotent? | Purpose | Cacheable? |
|--------|-------|-------------|---------|------------|
| **GET** | ✓ Yes | ✓ Yes | Retrieve resource | ✓ Yes |
| **HEAD** | ✓ Yes | ✓ Yes | Retrieve headers only | ✓ Yes |
| **OPTIONS** | ✓ Yes | ✓ Yes | Get supported methods | ✓ Yes |
| **POST** | ✗ No | ✗ No | Create resource | Sometimes |
| **PUT** | ✗ No | ✓ Yes | Replace resource | ✗ No |
| **PATCH** | ✗ No | Maybe | Update resource partially | ✗ No |
| **DELETE** | ✗ No | ✓ Yes | Delete resource | ✗ No |

**Real-time Example:**

```http
# SAFE METHODS - No side effects, can retry freely

# GET - Retrieve employee data (safe, cacheable)
GET /api/employees/EMP-123 HTTP/1.1
Cache-Control: max-age=3600

HTTP/1.1 200 OK
{
  "employeeId": "EMP-123",
  "name": "Sarah Johnson",
  "department": "Engineering"
}

# HEAD - Check if resource exists without downloading body
HEAD /api/employees/EMP-123 HTTP/1.1

HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 245
Last-Modified: Thu, 23 Jan 2026 10:00:00 GMT

# OPTIONS - Discover allowed methods
OPTIONS /api/employees/EMP-123 HTTP/1.1

HTTP/1.1 200 OK
Allow: GET, HEAD, PUT, PATCH, DELETE, OPTIONS

---

# UNSAFE METHODS - Modify state, require caution

# POST - Create new employee (not idempotent)
POST /api/employees HTTP/1.1
Content-Type: application/json

{
  "name": "Mike Wilson",
  "department": "Sales",
  "email": "mike.w@company.com"
}

HTTP/1.1 201 Created
Location: /api/employees/EMP-789

# DELETE - Remove employee (idempotent but unsafe)
DELETE /api/employees/EMP-789 HTTP/1.1

HTTP/1.1 204 No Content
```

**Common Mistake - Using GET for Unsafe Operations:**

```http
# WRONG - Never use GET to modify state
GET /api/employees/EMP-123/delete HTTP/1.1

# Problems:
# - Browser may prefetch/cache this URL
# - Can be triggered by web crawlers
# - No CSRF protection
# - Violates REST principles
```

---

## Batch 2: Authentication, Authorization & Security (Questions 11-20)

---

### 11. What is the difference between Authentication and Authorization?

**Authentication** is the process of verifying identity (who you are), while **Authorization** determines what actions an authenticated user can perform (what you can do). Authentication always comes before authorization.

**Why it's needed:** Security requires both layers - knowing who the user is (authentication) and controlling what they can access (authorization). Separating these concerns allows flexible permission models and better security architecture.

**When to use:** Implement authentication for all protected endpoints using JWT, OAuth, or API keys. Implement authorization using role-based (RBAC) or attribute-based (ABAC) access control for fine-grained permissions.

| Aspect | Authentication | Authorization |
|--------|---------------|----------------|
| **Question** | Who are you? | What can you do? |
| **Purpose** | Verify user identity | Control access to resources |
| **Process** | Username/password, token, biometric, MFA | Check roles, permissions, policies |
| **HTTP Status** | 401 Unauthorized (if fails) | 403 Forbidden (if fails) |
| **Occurs** | First, at entry point | After authentication, per resource |
| **Examples** | Login, JWT validation, OAuth | Admin-only routes, resource ownership |
| **Technologies** | JWT, OAuth2, Basic Auth, API Keys | RBAC, ABAC, ACLs, Policy-based |

**Real-time Example:**

```http
# AUTHENTICATION - Proving identity
POST /api/auth/login HTTP/1.1
Content-Type: application/json

{
  "email": "john.doe@company.com",
  "password": "SecurePass123!"
}

HTTP/1.1 200 OK
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "RT-abc123...",
  "user": {
    "userId": 456,
    "name": "John Doe",
    "roles": ["Employee", "Manager"]
  }
}

---

# AUTHORIZATION - Checking permissions

# Scenario 1: User tries to view their own profile (ALLOWED)
GET /api/employees/456 HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
# JWT contains: userId=456, roles=["Employee", "Manager"]

HTTP/1.1 200 OK
{
  "employeeId": 456,
  "name": "John Doe",
  "department": "Sales"
}

---

# Scenario 2: User tries to delete another employee (DENIED - lacks permission)
DELETE /api/employees/789 HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
# JWT valid (authenticated) but role "Manager" can't delete employees

HTTP/1.1 403 Forbidden
{
  "error": "Forbidden",
  "message": "Admin role required to delete employees"
}

---

# Scenario 3: Invalid token (AUTHENTICATION FAILED)
GET /api/employees/456 HTTP/1.1
Authorization: Bearer invalid_or_expired_token

HTTP/1.1 401 Unauthorized
WWW-Authenticate: Bearer error="invalid_token"
{
  "error": "Unauthorized",
  "message": "Invalid or expired authentication token"
}
```

---

### 12. Explain JWT (JSON Web Token) and its structure

**JWT (JSON Web Token)** is a compact, self-contained token format for securely transmitting information between parties as a JSON object. It's digitally signed, ensuring integrity and authenticity without requiring server-side session storage.

**Why it's needed:** JWTs enable stateless authentication in distributed systems. They eliminate session storage, enable horizontal scaling, work across domains, and can carry claims (user info, roles) reducing database lookups.

**When to use:** Use JWTs for API authentication, microservices communication, single sign-on (SSO), mobile app authentication, and distributed systems where statelessness and scalability are priorities.

**JWT Structure (3 parts separated by dots):**

01. **Header** - Algorithm and token type
02. **Payload** - Claims (user data, metadata)
03. **Signature** - Cryptographic signature for verification

**Format:** `header.payload.signature`

**Real-time Example:**

```javascript
// JWT Structure Breakdown

// 1. HEADER (Base64URL encoded)
{
    "alg": "HS256", // Algorithm: HMAC SHA-256
    "typ": "JWT" // Type: JSON Web Token
}

// 2. PAYLOAD (Base64URL encoded) - Claims
{
    "sub": "user123", // Subject (user ID)
    "name": "Alice Johnson", // User name
    "email": "alice@company.com", // Email
    "roles": ["Employee", "Manager"], // Authorization
    "iat": 1706011200, // Issued At (timestamp)
    "exp": 1706014800 // Expiration (timestamp)
}

// 3. SIGNATURE (prevents tampering)
HMACSHA256(
    base64UrlEncode(header) + "." + base64UrlEncode(payload),
    secret_key
)
```

**Complete JWT Token:**

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyMTIzIiwibmFtZSI6IkFsaWNlIEpvaG5zb24iLCJlbWFpbCI6ImFsaWNlQGNvbXBhbnkuY29tIiwicm9sZXMiOlsiRW1wbG95ZWUiLCJNYW5hZ2VyIl0sImlhdCI6MTcwNjAxMTIwMCwiZXhwIjoxNzA2MDE0ODAwfQ.4vC2Hf8x_9mN5yKzP1qW8rL3jT6nU2oS7bM9dR4eE1k
```

**API Usage:**

```http
# Using JWT for authenticated request
GET /api/orders HTTP/1.1
Host: api.company.com
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

# Server validates JWT:
# 1. Verify signature (ensure not tampered)
# 2. Check expiration (exp claim)
# 3. Extract user info from payload (no DB lookup needed!)
# 4. Check roles/permissions for authorization

HTTP/1.1 200 OK
[
  {"orderId": "ORD-101", "total": 299.99},
  {"orderId": "ORD-102", "total": 149.50}
]
```

**JWT Best Practices:**
* Store sensitive key server-side, never in JWT
* Keep payload small (JWT sent with every request)
* Set appropriate expiration time (short-lived: 15-60 min)
* Use refresh tokens for long sessions
* Use HTTPS to prevent token interception
* Validate signature and expiration on every request

---

### 13. What is OAuth 2.0 and when should you use it?

**OAuth 2.0** is an authorization framework that enables applications to obtain limited access to user accounts on third-party services without exposing passwords. It uses access tokens instead of credentials for authentication and authorization.

**Why it's needed:** OAuth allows secure third-party access without password sharing, provides fine-grained scopes (read-only, write access), enables revocable access, and standardizes authorization flows across different platforms.

**When to use:** Use OAuth 2.0 for social login (Login with Google/Facebook), third-party API integrations, mobile apps, single sign-on (SSO), and scenarios where users grant limited access to their data.

**OAuth 2.0 Flow Types:**

| Flow Type | Use Case | Client Type |
|-----------|----------|-------------|
| **Authorization Code** | Web applications with backend | Confidential client |
| **Implicit** | Single-page apps (deprecated, use PKCE) | Public client |
| **Client Credentials** | Machine-to-machine, microservices | Confidential client |
| **Resource Owner Password** | Legacy apps, trusted clients (avoid) | Confidential client |
| **PKCE Extension** | Mobile apps, SPAs (modern approach) | Public client |

**Real-time Example - Authorization Code Flow:**

```http
# Step 1: Client redirects user to authorization server
GET /oauth/authorize?
    response_type=code&
    client_id=app123&
    redirect_uri=https://myapp.com/callback&
    scope=read_orders+write_orders&
    state=xyz123
Host: auth.provider.com

# User logs in and grants permission

# Step 2: Authorization server redirects back with code
HTTP/1.1 302 Found
Location: https://myapp.com/callback?code=AUTH_CODE_abc&state=xyz123

---

# Step 3: Client exchanges code for access token
POST /oauth/token HTTP/1.1
Host: auth.provider.com
Content-Type: application/x-www-form-urlencoded

grant_type=authorization_code&
code=AUTH_CODE_abc&
redirect_uri=https://myapp.com/callback&
client_id=app123&
client_secret=SECRET_xyz

HTTP/1.1 200 OK
{
  "access_token": "ACCESS_TOKEN_def789",
  "token_type": "Bearer",
  "expires_in": 3600,
  "refresh_token": "REFRESH_TOKEN_ghi456",
  "scope": "read_orders write_orders"
}

---

# Step 4: Use access token to call protected API
GET /api/orders HTTP/1.1
Host: api.provider.com
Authorization: Bearer ACCESS_TOKEN_def789

HTTP/1.1 200 OK
{
  "orders": [
    {"orderId": "ORD-501", "status": "shipped"},
    {"orderId": "ORD-502", "status": "pending"}
  ]
}

---

# Step 5: Refresh token when access token expires
POST /oauth/token HTTP/1.1
Host: auth.provider.com
Content-Type: application/x-www-form-urlencoded

grant_type=refresh_token&
refresh_token=REFRESH_TOKEN_ghi456&
client_id=app123&
client_secret=SECRET_xyz

HTTP/1.1 200 OK
{
  "access_token": "NEW_ACCESS_TOKEN_jkl012",
  "token_type": "Bearer",
  "expires_in": 3600
}
```

**OAuth 2.0 Scopes Example:**

```
read_profile       - View user profile
write_profile      - Modify user profile
read_orders        - View orders
write_orders       - Create/modify orders
admin              - Full access
```

---

### 14. What is API Rate Limiting and how do you implement it?

**API Rate Limiting** is a technique to control the number of requests a client can make to an API within a specific time window. It protects against abuse, ensures fair resource distribution, and maintains service quality for all users.

**Why it's needed:** Rate limiting prevents DDoS attacks, protects against brute-force attempts, manages server load, ensures fair usage among clients, and enables tiered pricing models (free vs premium tiers with different limits).

**When to use:** Implement rate limiting on all public APIs, authentication endpoints (prevent brute force), resource-intensive operations, and APIs with multiple pricing tiers requiring different quotas.

**Common Rate Limiting Algorithms:**

| Algorithm | Description | Use Case |
|-----------|-------------|----------|
| **Fixed Window** | Fixed request count per time window | Simple rate limiting |
| **Sliding Window** | Smooth rate limiting across time | Better distribution |
| **Token Bucket** | Tokens refill at constant rate | Allow bursts |
| **Leaky Bucket** | Requests processed at constant rate | Smooth traffic |
| **Concurrent Requests** | Limit simultaneous connections | Protect server resources |

**Real-time Example:**

```http
# Client makes request (within limit)
GET /api/products HTTP/1.1
Host: api.shop.com
Authorization: Bearer token123

HTTP/1.1 200 OK
X-RateLimit-Limit: 1000           # Max requests per hour
X-RateLimit-Remaining: 847        # Requests left in window
X-RateLimit-Reset: 1706015400     # When limit resets (Unix timestamp)
X-RateLimit-Window: 3600          # Window size in seconds

{
  "products": [...]
}

---

# Client exceeds rate limit
GET /api/products HTTP/1.1
Authorization: Bearer token123

HTTP/1.1 429 Too Many Requests
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 0
X-RateLimit-Reset: 1706015400
Retry-After: 600                   # Retry after 600 seconds

{
  "error": "RateLimitExceeded",
  "message": "API rate limit exceeded. 1000 requests per hour allowed.",
  "retryAfter": 600,
  "upgradeUrl": "https://api.shop.com/pricing"
}

---

# Different tiers with different limits

# FREE TIER
X-RateLimit-Limit: 1000           # 1000 req/hour
X-RateLimit-Tier: free

# PREMIUM TIER
X-RateLimit-Limit: 10000          # 10000 req/hour
X-RateLimit-Tier: premium

# ENTERPRISE TIER
X-RateLimit-Limit: unlimited
X-RateLimit-Tier: enterprise
```

**Implementation Strategies:**

```http
# Per-User Rate Limiting (by authentication token)
Authorization: Bearer user_token_123
# Tracks: user_123 → 847/1000 requests used

# Per-IP Rate Limiting (for unauthenticated endpoints)
X-Forwarded-For: 192.168.1.100
# Tracks: ip_192.168.1.100 → 45/100 requests used

# Per-Endpoint Rate Limiting (critical operations)
POST /api/orders              # 100 req/hour
GET /api/products             # 1000 req/hour
POST /api/auth/login          # 5 req/minute (prevent brute force)
```

**Best Practices:**
* Return `429 Too Many Requests` with `Retry-After` header
* Include rate limit info in response headers
* Implement different limits for different endpoints
* Use distributed rate limiting (Redis) for multiple servers
* Provide upgrade path for users hitting limits

---

### 15. Explain API Versioning strategies

**API Versioning** is the practice of managing changes to APIs over time while maintaining backward compatibility for existing clients. It allows introducing new features or breaking changes without disrupting current users.

**Why it's needed:** APIs evolve - new features, bug fixes, performance improvements, security updates. Versioning prevents breaking existing integrations while enabling progress. Without versioning, every change risks breaking production systems.

**When to use:** Version APIs when making breaking changes (removing fields, changing response structure, modifying behavior). Maintain old versions during deprecation periods. Plan versioning strategy before launching public APIs.

**Versioning Strategies:**

| Strategy | Example | Pros | Cons |
|----------|---------|------|------|
| **URI Path** | `/api/v1/users` , `/api/v2/users` | Clear, simple, cacheable | URL changes, multiple endpoints |
| **Query Parameter** | `/api/users?version=1` | Single endpoint | Not RESTful, caching issues |
| **Header** | `Accept: application/vnd.api.v1+json` | Clean URLs | Less visible, tooling issues |
| **Content Negotiation** | `Accept: application/vnd.api+json;version=2` | RESTful, flexible | Complex, harder to test |
| **Subdomain** | `v1.api.com/users` | Clear separation | Infrastructure overhead |

**Real-time Example:**

```http
# VERSION 1 - Original API (URI Path Versioning)
GET /api/v1/customers/C-123 HTTP/1.1
Host: api.company.com
Authorization: Bearer token123

HTTP/1.1 200 OK
{
  "id": "C-123",
  "name": "John Smith",
  "contact": "john.smith@email.com"
}

---

# VERSION 2 - Enhanced API with breaking changes
# - Split name into firstName/lastName
# - Added email and phone separately
# - Changed id to customerId

GET /api/v2/customers/C-123 HTTP/1.1
Authorization: Bearer token123

HTTP/1.1 200 OK
{
  "customerId": "C-123",
  "firstName": "John",
  "lastName": "Smith",
  "email": "john.smith@email.com",
  "phone": "+1-555-0123",
  "createdAt": "2024-01-15T10:30:00Z"
}

---

# HEADER-BASED VERSIONING
GET /api/customers/C-123 HTTP/1.1
Host: api.company.com
Authorization: Bearer token123
API-Version: 2.0

HTTP/1.1 200 OK
API-Version: 2.0
{
  "customerId": "C-123",
  "firstName": "John",
  "lastName": "Smith"
}

---

# CONTENT NEGOTIATION VERSIONING
GET /api/customers/C-123 HTTP/1.1
Accept: application/vnd.company.customer.v2+json
Authorization: Bearer token123

HTTP/1.1 200 OK
Content-Type: application/vnd.company.customer.v2+json
{
  "customerId": "C-123",
  "firstName": "John",
  "lastName": "Smith"
}

---

# DEPRECATION NOTICE
GET /api/v1/customers/C-123 HTTP/1.1

HTTP/1.1 200 OK
Deprecation: true
Sunset: Wed, 31 Dec 2026 23:59:59 GMT
Link: </api/v2/customers/C-123>; rel="successor-version"
Warning: 299 - "API v1 is deprecated. Migrate to v2 by Dec 2026"

{
  "id": "C-123",
  "name": "John Smith"
}
```

**Version Lifecycle Management:**

```
Version 1.0 → Released (Jan 2024)
Version 2.0 → Released (Jan 2025)
             → v1 marked deprecated (Jan 2025)
             → v1 sunset announced (Dec 2026)
Version 3.0 → Released (Jan 2026)
             → v2 marked deprecated (Jan 2026)
             → v1 shut down (Dec 2026)
```

---

### 16. What is CORS (Cross-Origin Resource Sharing)?

**CORS (Cross-Origin Resource Sharing)** is a security mechanism that allows or restricts web applications running on one domain to request resources from a different domain. It's implemented through HTTP headers that tell browsers whether cross-origin requests are permitted.

**Why it's needed:** Browsers enforce Same-Origin Policy for security, blocking requests between different origins (domain, protocol, or port). CORS selectively relaxes this restriction, enabling legitimate cross-origin requests while maintaining security against malicious websites.

**When to use:** Configure CORS when your API serves web applications on different domains, single-page applications (SPAs), mobile web apps, or any scenario where frontend and backend are on different origins.

**CORS Headers:**

| Header | Purpose | Example |
|--------|---------|---------|
| **Access-Control-Allow-Origin** | Specifies allowed origins | `https://app.example.com` or `*` |
| **Access-Control-Allow-Methods** | Allowed HTTP methods | `GET, POST, PUT, DELETE` |
| **Access-Control-Allow-Headers** | Allowed request headers | `Authorization, Content-Type` |
| **Access-Control-Max-Age** | Preflight cache duration | `3600` (1 hour) |
| **Access-Control-Allow-Credentials** | Allow cookies/auth | `true` |

**Real-time Example:**

```http
# SCENARIO: Frontend at https://app.mycompany.com 
# calls API at https://api.mycompany.com

# Simple Request (GET, no custom headers)
GET /api/products HTTP/1.1
Host: api.mycompany.com
Origin: https://app.mycompany.com

HTTP/1.1 200 OK
Access-Control-Allow-Origin: https://app.mycompany.com
Access-Control-Allow-Credentials: true
Content-Type: application/json

{
  "products": [{"id": 1, "name": "Laptop"}]
}

---

# Preflight Request (for POST, PUT, DELETE, custom headers)
# Browser sends OPTIONS first to check if allowed

OPTIONS /api/orders HTTP/1.1
Host: api.mycompany.com
Origin: https://app.mycompany.com
Access-Control-Request-Method: POST
Access-Control-Request-Headers: Authorization, Content-Type

HTTP/1.1 204 No Content
Access-Control-Allow-Origin: https://app.mycompany.com
Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS
Access-Control-Allow-Headers: Authorization, Content-Type
Access-Control-Max-Age: 3600
Access-Control-Allow-Credentials: true

# After successful preflight, actual request sent
POST /api/orders HTTP/1.1
Host: api.mycompany.com
Origin: https://app.mycompany.com
Authorization: Bearer token123
Content-Type: application/json

{
  "customerId": 456,
  "items": [{"productId": 1, "quantity": 2}]
}

HTTP/1.1 201 Created
Access-Control-Allow-Origin: https://app.mycompany.com
{
  "orderId": "ORD-789",
  "status": "created"
}

---

# CORS Error (Origin not allowed)
GET /api/products HTTP/1.1
Host: api.mycompany.com
Origin: https://malicious-site.com

HTTP/1.1 403 Forbidden
# No CORS headers returned → Browser blocks response

# Browser Console Error:
# "Access to fetch at 'https://api.mycompany.com/api/products' 
#  from origin 'https://malicious-site.com' has been blocked by CORS policy"
```

**CORS Configuration Examples:**

```http
# Development (Allow all origins - NOT for production)
Access-Control-Allow-Origin: *
Access-Control-Allow-Methods: GET, POST, PUT, DELETE
Access-Control-Allow-Headers: *

# Production (Specific origins only)
Access-Control-Allow-Origin: https://app.mycompany.com
Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS
Access-Control-Allow-Headers: Authorization, Content-Type, X-Request-ID
Access-Control-Allow-Credentials: true
Access-Control-Max-Age: 86400

# Multiple Origins (implemented server-side)
# Check request Origin header, respond with matching allowed origin
Request Origin: https://app.mycompany.com
Response: Access-Control-Allow-Origin: https://app.mycompany.com

Request Origin: https://mobile.mycompany.com
Response: Access-Control-Allow-Origin: https://mobile.mycompany.com
```

---

### 17. What is the difference between API Key, Bearer Token, and Basic Authentication?

These are three different **authentication mechanisms** for APIs. **API Keys** identify applications, **Bearer Tokens** (JWT/OAuth) identify authenticated users with claims, and **Basic Auth** sends credentials (base64-encoded) with each request.

**Why it's needed:** Different authentication methods suit different scenarios - API keys for service-to-service, bearer tokens for user sessions, basic auth for simple internal tools. Understanding trade-offs (security, complexity, scalability) is crucial.

**When to use:** Use **API Keys** for public APIs with simple auth. Use **Bearer Tokens (JWT)** for user authentication in modern apps. Use **Basic Auth** only for internal tools or initial MVP (avoid in production due to security concerns).

| Aspect | API Key | Bearer Token (JWT) | Basic Authentication |
|--------|---------|-------------------|---------------------|
| **Format** | Single string/key | JWT with header.payload.signature | Base64(username:password) |
| **Sent In** | Header, query param | Authorization header | Authorization header |
| **Identifies** | Application/client | User with claims | User credentials |
| **Stateless** | Yes | Yes | No (credentials verified each time) |
| **Expires** | Usually long-lived | Short-lived (minutes/hours) | No expiration (credentials) |
| **Revocation** | Manually revoke/regenerate | Token expiration or blacklist | Change password |
| **Security** | Moderate (shared secret) | High (signed, encrypted) | Low (base64, not encrypted) |
| **Use Case** | Third-party integrations | User sessions, mobile apps | Internal tools, legacy systems |

**Real-time Example:**

```http
# 1. API KEY AUTHENTICATION
# Used for application-level identification

GET /api/weather?city=Seattle HTTP/1.1
Host: api.weather.com
X-API-Key: sk_live_1234567890abcdef

# OR as query parameter (less secure)
GET /api/weather?city=Seattle&apiKey=sk_live_1234567890abcdef HTTP/1.1

HTTP/1.1 200 OK
{
  "city": "Seattle",
  "temperature": 55,
  "condition": "Cloudy"
}

# API key identifies: company/application, not specific user
# Typically used for: Rate limiting, billing, access control

---

# 2. BEARER TOKEN (JWT) AUTHENTICATION
# Used for user-level authentication with claims

POST /api/auth/login HTTP/1.1
Content-Type: application/json

{
  "email": "user@company.com",
  "password": "SecurePass123"
}

HTTP/1.1 200 OK
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": 3600
}

# Use bearer token for subsequent requests
GET /api/orders HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

HTTP/1.1 200 OK
[
  {"orderId": "ORD-101", "userId": 456, "total": 299.99}
]

# Token contains: userId, roles, expiration
# Server validates signature and extracts claims
# No database lookup needed for authentication

---

# 3. BASIC AUTHENTICATION
# Sends credentials with every request (base64 encoded)

GET /api/admin/users HTTP/1.1
Authorization: Basic dXNlcm5hbWU6cGFzc3dvcmQ=
# Base64 encoded "username:password"

HTTP/1.1 200 OK
[
  {"userId": 1, "name": "Admin User"},
  {"userId": 2, "name": "Regular User"}
]

# Security concerns:
# - Credentials sent with EVERY request
# - Base64 is encoding, not encryption
# - Must use HTTPS (credentials easily decoded)
# - Server validates credentials every request (DB lookup)
# - No token expiration mechanism
```

**Security Comparison:**

```http
# API Key - Exposed if hardcoded in client
X-API-Key: sk_live_abc123def456
# Risk: Key leakage, no user context

# Bearer Token - Short-lived, contains claims
Authorization: Bearer eyJhbGc...
# JWT payload (decoded):
{
  "userId": 789,
  "email": "user@company.com",
  "roles": ["Employee"],
  "exp": 1706015400  # Expires in 1 hour
}
# Risk: Token theft (use HTTPS, short expiry, refresh tokens)

# Basic Auth - Credentials on every request
Authorization: Basic YWRtaW46c2VjcmV0  # "admin:secret"
# Risk: Credential exposure, replay attacks, no expiration
```

**Best Practices:**
* **API Keys:** Rotate regularly, use separate keys per environment, don't commit to source control
* **Bearer Tokens:** Short expiration (15-60 min), use refresh tokens, store securely (httpOnly cookies)
* **Basic Auth:** Only with HTTPS, avoid in production, use for internal/dev environments only

---

### 18. What is API Pagination and what are the different pagination strategies?

**API Pagination** is the technique of dividing large datasets into smaller chunks (pages) to improve performance, reduce bandwidth, and enhance user experience. Instead of returning thousands of records, the API returns manageable subsets.

**Why it's needed:** Returning large datasets causes performance issues (slow queries, memory exhaustion, timeouts), wastes bandwidth, and provides poor UX. Pagination enables efficient data retrieval, faster response times, and better resource utilization.

**When to use:** Implement pagination for any endpoint returning collections - user lists, product catalogs, order history, search results, logs, notifications. Essential for scalable APIs handling large datasets.

**Pagination Strategies:**

| Strategy | Method | Pros | Cons | Best For |
|----------|--------|------|------|----------|
| **Offset/Limit** | `?page=2&size=20` or `?offset=20&limit=20` | Simple, jump to any page | Performance degrades with large offsets | Small datasets, random access |
| **Cursor-Based** | `?cursor=abc123&limit=20` | Consistent, efficient for large datasets | Can't jump to specific page | Real-time feeds, infinite scroll |
| **Keyset** | `?after_id=500&limit=20` | Efficient, no offset issues | Requires sortable unique field | Large datasets, sequential access |
| **Time-Based** | `?since=2024-01-01&until=2024-01-31` | Natural for time-series data | Complex filtering | Logs, events, time-series |

**Real-time Example:**

```http
# 1. OFFSET/LIMIT PAGINATION (Page-Based)
# Most common, easiest to implement

GET /api/products?page=1&pageSize=20 HTTP/1.1
# OR
GET /api/products?offset=0&limit=20 HTTP/1.1

HTTP/1.1 200 OK
{
  "data": [
    {"productId": 1, "name": "Laptop", "price": 999.99},
    {"productId": 2, "name": "Mouse", "price": 29.99},
    // ... 18 more items
  ],
  "pagination": {
    "currentPage": 1,
    "pageSize": 20,
    "totalPages": 50,
    "totalCount": 1000,
    "hasNextPage": true,
    "hasPreviousPage": false
  },
  "links": {
    "self": "/api/products?page=1&pageSize=20",
    "first": "/api/products?page=1&pageSize=20",
    "next": "/api/products?page=2&pageSize=20",
    "last": "/api/products?page=50&pageSize=20"
  }
}

# Page 2 request
GET /api/products?page=2&pageSize=20 HTTP/1.1
# Returns items 21-40

---

# 2. CURSOR-BASED PAGINATION
# Efficient for large datasets, prevents missing items

GET /api/orders?limit=20 HTTP/1.1
# First request, no cursor

HTTP/1.1 200 OK
{
  "data": [
    {"orderId": "ORD-1001", "total": 299.99, "createdAt": "2026-01-20T10:00:00Z"},
    {"orderId": "ORD-1002", "total": 149.50, "createdAt": "2026-01-20T11:30:00Z"},
    // ... 18 more orders
  ],
  "pagination": {
    "nextCursor": "eyJpZCI6MTAyMCwiY3JlYXRlZEF0IjoiMjAyNi0wMS0yMFQxNTowMDowMFoifQ==",
    "hasMore": true,
    "limit": 20
  }
}

# Next page request using cursor
GET /api/orders?cursor=eyJpZCI6MTAyMCwiY3JlYXRlZEF0IjoiMjAyNi0wMS0yMFQxNTowMDowMFoifQ==&limit=20 HTTP/1.1

# Cursor encodes: last item ID and timestamp
# Server decodes and fetches next 20 items after that point

---

# 3. KEYSET PAGINATION (After ID)
# Efficient, uses indexed columns

GET /api/users?limit=20 HTTP/1.1
# First page

HTTP/1.1 200 OK
{
  "data": [
    {"userId": 1, "name": "Alice"},
    {"userId": 2, "name": "Bob"},
    // ... up to userId: 20
  ],
  "nextId": 20,
  "hasMore": true
}

# Next page
GET /api/users?afterId=20&limit=20 HTTP/1.1
# Query: SELECT * FROM users WHERE userId > 20 ORDER BY userId LIMIT 20

HTTP/1.1 200 OK
{
  "data": [
    {"userId": 21, "name": "Charlie"},
    {"userId": 22, "name": "Diana"},
    // ... up to userId: 40
  ],
  "nextId": 40,
  "hasMore": true
}

---

# 4. TIME-BASED PAGINATION
# Perfect for logs, events, activity feeds

GET /api/logs?startDate=2026-01-01&endDate=2026-01-31&limit=100 HTTP/1.1

HTTP/1.1 200 OK
{
  "data": [
    {"timestamp": "2026-01-01T00:05:23Z", "level": "INFO", "message": "..."},
    {"timestamp": "2026-01-01T00:12:45Z", "level": "ERROR", "message": "..."}
  ],
  "pagination": {
    "startDate": "2026-01-01T00:00:00Z",
    "endDate": "2026-01-31T23:59:59Z",
    "count": 100,
    "nextToken": "2026-01-01T01:00:00Z"
  }
}
```

**Filtering + Sorting + Pagination Combined:**

```http
GET /api/employees?
    department=Engineering&
    status=active&
    sortBy=salary&
    sortOrder=desc&
    page=1&
    pageSize=25 HTTP/1.1

HTTP/1.1 200 OK
{
  "data": [
    {"employeeId": 123, "name": "Sarah", "salary": 150000},
    {"employeeId": 456, "name": "Mike", "salary": 145000}
  ],
  "filters": {
    "department": "Engineering",
    "status": "active"
  },
  "sort": {
    "field": "salary",
    "order": "desc"
  },
  "pagination": {
    "page": 1,
    "pageSize": 25,
    "totalCount": 127
  }
}
```

---

### 19. What is API Caching and what are the caching strategies?

**API Caching** stores copies of responses to serve future identical requests faster, without executing expensive operations (database queries, computations, external API calls). It dramatically improves performance and reduces server load.

**Why it's needed:** Caching reduces response times from seconds to milliseconds, decreases database load, lowers infrastructure costs, improves scalability, and enhances user experience. Critical for high-traffic APIs and expensive operations.

**When to use:** Cache relatively static data (product catalogs, reference data), expensive computations, frequently accessed resources, and responses with predictable expiration. Avoid caching user-specific sensitive data without proper isolation.

**Caching Strategies:**

| Strategy | Location | Speed | Scope | Use Case |
|----------|----------|-------|-------|----------|
| **Client-Side** | Browser/App | Fastest | Per user | Public resources, static data |
| **CDN** | Edge servers | Very Fast | Global | Static assets, public APIs |
| **API Gateway** | Gateway layer | Fast | All users | Common requests, rate limiting |
| **Application** | In-memory (Redis) | Fast | All users | Session data, frequently accessed |
| **Database** | Query cache | Moderate | All users | Complex queries |

**HTTP Caching Headers:**

| Header | Purpose | Example |
|--------|---------|---------|
| **Cache-Control** | Caching directives | `max-age=3600, public` |
| **ETag** | Response version identifier | `"abc123xyz"` |
| **Last-Modified** | Last modification time | `Mon, 22 Jan 2026 10:00:00 GMT` |
| **Expires** | Absolute expiration time | `Wed, 23 Jan 2026 10:00:00 GMT` |

**Real-time Example:**

```http
# 1. CLIENT-SIDE CACHING (Browser/HTTP Cache)

# First request - Cache MISS
GET /api/products/PROD-123 HTTP/1.1
Host: api.shop.com

HTTP/1.1 200 OK
Cache-Control: max-age=3600, public
ETag: "v1.abc123"
Last-Modified: Mon, 22 Jan 2026 10:00:00 GMT
Content-Type: application/json

{
  "productId": "PROD-123",
  "name": "Laptop",
  "price": 999.99,
  "stock": 45
}

# Second request (within 1 hour) - Cache HIT
# Browser serves from cache, no network request!

---

# 2. CONDITIONAL REQUESTS (If-None-Modified)

GET /api/products/PROD-123 HTTP/1.1
If-None-Match: "v1.abc123"
If-Modified-Since: Mon, 22 Jan 2026 10:00:00 GMT

# If resource unchanged:
HTTP/1.1 304 Not Modified
ETag: "v1.abc123"
# No body sent - saves bandwidth

# If resource changed:
HTTP/1.1 200 OK
ETag: "v2.def456"
Last-Modified: Tue, 23 Jan 2026 14:30:00 GMT
{
  "productId": "PROD-123",
  "name": "Laptop",
  "price": 899.99,  # Price changed
  "stock": 30        # Stock changed
}

---

# 3. SERVER-SIDE CACHING (Redis/In-Memory)

# Request for product catalog
GET /api/products?category=electronics&page=1 HTTP/1.1

# Server-side logic (pseudocode):
# cacheKey = "products:electronics:page1"
# 
# if (cache.exists(cacheKey)) {
#     return cache.get(cacheKey)  // Cache HIT - 5ms response
# } else {
#     data = database.query(...)   // Cache MISS - 200ms query
#     cache.set(cacheKey, data, ttl=600)  // Cache for 10 min
#     return data
# }

HTTP/1.1 200 OK
Cache-Control: private, max-age=600
X-Cache-Status: MISS  # or HIT
{
  "products": [...]
}

---

# 4. CACHE INVALIDATION

# Update product price
PUT /api/products/PROD-123 HTTP/1.1
Content-Type: application/json

{
  "price": 799.99
}

HTTP/1.1 200 OK
# Server invalidates cache:
# - cache.delete("product:PROD-123")
# - cache.delete("products:electronics:*")
# - Increment ETag version

{
  "productId": "PROD-123",
  "price": 799.99
}

# Next GET returns fresh data with new ETag

---

# 5. NO CACHE FOR SENSITIVE DATA

GET /api/users/profile HTTP/1.1
Authorization: Bearer token123

HTTP/1.1 200 OK
Cache-Control: no-store, no-cache, must-revalidate, private
Pragma: no-cache
Expires: 0

{
  "userId": 456,
  "email": "user@company.com",
  "balance": 5000.00  # Sensitive, always fresh
}
```

**Cache-Control Directives:**

```http
# Public, cacheable for 1 hour
Cache-Control: public, max-age=3600

# Private (user-specific), cacheable for 5 minutes
Cache-Control: private, max-age=300

# Never cache
Cache-Control: no-store, no-cache, must-revalidate

# Cache but revalidate
Cache-Control: max-age=0, must-revalidate

# Stale cache allowed if server unavailable
Cache-Control: max-age=3600, stale-if-error=86400
```

**Caching Best Practices:**
* Use appropriate Cache-Control directives
* Implement ETags for efficient revalidation
* Never cache sensitive/personal data without `private`
* Use cache invalidation on data updates
* Consider using Redis for distributed caching
* Monitor cache hit rates and adjust TTLs

---

### 20. What are WebHooks and how do they differ from polling?

**WebHooks** are HTTP callbacks that automatically send data to a specified URL when specific events occur. Unlike polling (client repeatedly asking "any updates?"), webhooks use push notifications (server says "here's an update!").

**Why it's needed:** Webhooks eliminate constant polling, reduce unnecessary API calls, provide real-time notifications, decrease latency, and save resources. They enable event-driven architectures where systems react immediately to changes.

**When to use:** Use webhooks for payment confirmations, order status updates, CI/CD pipelines, third-party integrations (Stripe, GitHub, Slack), real-time notifications, and any scenario requiring immediate event notification instead of periodic checking.

| Aspect | Polling | WebHooks |
|--------|---------|----------|
| **Communication** | Pull (client requests) | Push (server sends) |
| **Frequency** | Repeated requests at intervals | Only when event occurs |
| **Latency** | High (depends on poll interval) | Low (immediate notification) |
| **Resource Usage** | High (many unnecessary requests) | Low (only when needed) |
| **Implementation** | Simple (client-side only) | Complex (requires endpoint setup) |
| **Reliability** | Reliable (client controls) | Can fail (retry needed) |
| **Real-time** | No (delayed by interval) | Yes (instant notification) |
| **Use Case** | Simple apps, few users | Event-driven, high volume |

**Real-time Example:**

```http
# POLLING APPROACH (Inefficient)

# Client checks order status every 30 seconds
GET /api/orders/ORD-5001/status HTTP/1.1
Authorization: Bearer token123

# Response 1 (T=0s):
HTTP/1.1 200 OK
{"orderId": "ORD-5001", "status": "processing"}

# Response 2 (T=30s):
HTTP/1.1 200 OK
{"orderId": "ORD-5001", "status": "processing"}  # No change

# Response 3 (T=60s):
HTTP/1.1 200 OK
{"orderId": "ORD-5001", "status": "processing"}  # No change

# Response 4 (T=90s):
HTTP/1.1 200 OK
{"orderId": "ORD-5001", "status": "shipped"}  # Finally changed!

# Problem: 3 unnecessary requests, 90-second delay

---

# WEBHOOK APPROACH (Efficient)

# Step 1: Client registers webhook when creating order
POST /api/orders HTTP/1.1
Authorization: Bearer token123
Content-Type: application/json

{
  "customerId": 456,
  "items": [{"productId": 789, "quantity": 2}],
  "webhookUrl": "https://myapp.com/webhooks/order-status",
  "webhookEvents": ["order.shipped", "order.delivered", "order.cancelled"]
}

HTTP/1.1 201 Created
{
  "orderId": "ORD-5001",
  "status": "processing",
  "webhookId": "WH-abc123"
}

---

# Step 2: Server sends webhook when status changes
# (When order status changes to "shipped")

POST /webhooks/order-status HTTP/1.1
Host: myapp.com
Content-Type: application/json
X-Webhook-Signature: sha256=abc123def456...
X-Webhook-Event: order.shipped
X-Webhook-ID: WH-abc123
X-Webhook-Timestamp: 1706012400

{
  "eventType": "order.shipped",
  "timestamp": "2026-01-23T12:30:00Z",
  "data": {
    "orderId": "ORD-5001",
    "status": "shipped",
    "trackingNumber": "TRACK-789XYZ",
    "carrier": "FedEx",
    "estimatedDelivery": "2026-01-25"
  }
}

# Client responds to acknowledge receipt
HTTP/1.1 200 OK

# Benefits: Immediate notification, no polling, no wasted requests

---

# WEBHOOK SECURITY - Signature Verification

# Server calculates signature
secret = "webhook_secret_key_xyz"
payload = '{"eventType":"order.shipped",...}'
signature = HMAC-SHA256(payload, secret)

# Client verifies signature
receivedSignature = request.headers['X-Webhook-Signature']
calculatedSignature = HMAC-SHA256(request.body, secret)

if (receivedSignature === calculatedSignature) {
    // Webhook is authentic, process event
} else {
    // Potential attack, reject request
    return 401 Unauthorized
}

---

# WEBHOOK RETRY MECHANISM

# Attempt 1: Client endpoint down
POST /webhooks/order-status HTTP/1.1
(Connection timeout)

# Attempt 2: Retry after 1 minute
POST /webhooks/order-status HTTP/1.1
HTTP/1.1 500 Internal Server Error

# Attempt 3: Retry after 5 minutes
POST /webhooks/order-status HTTP/1.1
HTTP/1.1 200 OK  # Success

# Retry strategy: Exponential backoff (1min, 5min, 15min, 1hr)
# After max attempts, mark webhook delivery as failed
```

**Webhook Use Cases:**

```http
# Payment Processing (Stripe, PayPal)
Event: payment.succeeded
Webhook: https://myapp.com/webhooks/payment

# CI/CD (GitHub, GitLab)
Event: push, pull_request, release
Webhook: https://ci.myapp.com/webhooks/github

# Messaging (Slack, Teams)
Event: message.sent, user.joined
Webhook: https://bot.myapp.com/webhooks/slack

# E-commerce (Shopify, WooCommerce)
Event: order.created, order.fulfilled
Webhook: https://inventory.myapp.com/webhooks/orders
```

**Webhook Best Practices:**
* Always verify webhook signatures (prevent spoofing)
* Respond quickly (200 OK), process asynchronously
* Implement retry logic with exponential backoff
* Use HTTPS for webhook endpoints
* Log all webhook events for debugging
* Provide webhook management UI (view, test, disable)
* Include idempotency keys to handle duplicate deliveries

---

## Batch 3: API Design, Best Practices & Error Handling (Questions 21-30)

---

### 21. What are the best practices for REST API URI design?

**REST API URI design** defines how resources are identified and accessed through URLs. Well-designed URIs are intuitive, predictable, hierarchical, and self-documenting, making APIs easier to understand and use.

**Why it's needed:** Consistent URI patterns improve developer experience, reduce learning curve, enable predictable behavior, and make APIs self-explanatory. Poor URI design leads to confusion, maintenance issues, and integration difficulties.

**When to use:** Apply these principles when designing any REST API, especially public APIs, microservices, or APIs consumed by multiple teams. Good URI design is foundational and difficult to change later without breaking compatibility.

**URI Design Best Practices:**

| Rule | Good Example | Bad Example |
|------|--------------|-------------|
| **Use nouns, not verbs** | `/api/customers` | `/api/getCustomers` |
| **Use plural nouns** | `/api/orders` | `/api/order` |
| **Use lowercase** | `/api/products` | `/api/Products` |
| **Use hyphens, not underscores** | `/api/order-items` | `/api/order_items` |
| **Don't use file extensions** | `/api/customers/123` | `/api/customers/123.json` |
| **Use hierarchical relationships** | `/api/customers/123/orders` | `/api/customer-orders?customerId=123` |
| **Keep URIs short and meaningful** | `/api/customers/123/addresses` | `/api/getAllCustomerShippingAddresses?id=123` |
| **Use query params for filtering** | `/api/products?category=electronics` | `/api/products/electronics` |

**Real-time Example:**

```http
# RESOURCE COLLECTION
GET /api/customers HTTP/1.1
# Returns list of all customers

HTTP/1.1 200 OK
{
  "customers": [
    {"customerId": 1, "name": "John Doe"},
    {"customerId": 2, "name": "Jane Smith"}
  ]
}

---

# SINGLE RESOURCE
GET /api/customers/123 HTTP/1.1
# Returns specific customer

HTTP/1.1 200 OK
{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com"
}

---

# NESTED/HIERARCHICAL RESOURCES
GET /api/customers/123/orders HTTP/1.1
# Returns orders for customer 123

HTTP/1.1 200 OK
{
  "customerId": 123,
  "orders": [
    {"orderId": "ORD-501", "total": 299.99},
    {"orderId": "ORD-502", "total": 149.50}
  ]
}

# Specific order for specific customer
GET /api/customers/123/orders/ORD-501 HTTP/1.1

HTTP/1.1 200 OK
{
  "orderId": "ORD-501",
  "customerId": 123,
  "total": 299.99,
  "items": [...]
}

---

# FILTERING, SORTING, PAGINATION (Query Parameters)
GET /api/products?category=electronics&
                  minPrice=100&
                  maxPrice=1000&
                  sortBy=price&
                  sortOrder=asc&
                  page=1&
                  pageSize=20 HTTP/1.1

# Query params for:
# - Filtering: category, minPrice, maxPrice
# - Sorting: sortBy, sortOrder
# - Pagination: page, pageSize

---

# RESOURCE ACTIONS (Non-CRUD Operations)
# Use POST with descriptive sub-resource

# Good approach - Clear action
POST /api/orders/ORD-501/cancel HTTP/1.1
POST /api/orders/ORD-501/ship HTTP/1.1
POST /api/invoices/INV-789/send HTTP/1.1
POST /api/users/123/activate HTTP/1.1

# Alternative - Status update
PATCH /api/orders/ORD-501 HTTP/1.1
{"status": "cancelled"}

# Avoid
GET /api/cancelOrder?id=ORD-501  # WRONG: GET should be safe
POST /api/orders/cancel HTTP/1.1  # WRONG: Missing resource ID
Body: {"orderId": "ORD-501"}

---

# VERSIONING IN URI
GET /api/v1/customers HTTP/1.1
GET /api/v2/customers HTTP/1.1

---

# SEARCH ENDPOINTS
# Option 1: Query parameters
GET /api/products?q=laptop&searchIn=name,description HTTP/1.1

# Option 2: Dedicated search endpoint (complex search)
POST /api/products/search HTTP/1.1
Content-Type: application/json

{
  "query": "laptop",
  "filters": {
    "category": "electronics",
    "priceRange": {"min": 500, "max": 1500}
  },
  "sort": {"field": "price", "order": "asc"}
}
```

**Complete Example - E-commerce API:**

```http
# Customers
GET    /api/customers                    # List all customers
POST   /api/customers                    # Create customer
GET    /api/customers/123                # Get customer details
PUT    /api/customers/123                # Update customer (full)
PATCH  /api/customers/123                # Update customer (partial)
DELETE /api/customers/123                # Delete customer

# Customer's Orders (nested resource)
GET    /api/customers/123/orders         # Get all orders for customer
POST   /api/customers/123/orders         # Create order for customer
GET    /api/customers/123/orders/ORD-501 # Get specific order

# Orders (top-level resource)
GET    /api/orders                       # List all orders
GET    /api/orders/ORD-501               # Get order details
PATCH  /api/orders/ORD-501               # Update order
POST   /api/orders/ORD-501/cancel        # Cancel order
POST   /api/orders/ORD-501/ship          # Ship order

# Products
GET    /api/products                     # List products
GET    /api/products?category=electronics&inStock=true
GET    /api/products/PROD-789            # Get product details
GET    /api/products/PROD-789/reviews    # Get product reviews

# Search
GET    /api/products/search?q=laptop
POST   /api/search                       # Advanced search
```

---

### 22. How do you handle errors in REST APIs?

**Error handling** in REST APIs involves returning appropriate HTTP status codes, providing detailed error messages, maintaining consistent error response formats, and helping clients understand and resolve issues.

**Why it's needed:** Proper error handling enables clients to programmatically detect and handle failures, provides meaningful feedback for debugging, improves developer experience, and reduces support burden by making problems self-explanatory.

**When to use:** Implement comprehensive error handling for all endpoints - validation errors (400), authentication failures (401), authorization issues (403), not found errors (404), server errors (500), and custom business logic errors.

**Error Response Structure:**

```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Request validation failed",
    "details": [
      {
        "field": "email",
        "message": "Invalid email format",
        "value": "invalid-email"
      }
    ],
    "timestamp": "2026-01-23T10:30:00Z",
    "path": "/api/customers",
    "requestId": "req-abc123"
  }
}
```

**Real-time Example:**

```http
# 1. VALIDATION ERROR (400 Bad Request)
POST /api/customers HTTP/1.1
Content-Type: application/json

{
  "name": "",
  "email": "invalid-email",
  "age": -5,
  "phone": "123"
}

HTTP/1.1 400 Bad Request
Content-Type: application/json

{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Request validation failed",
    "details": [
      {
        "field": "name",
        "message": "Name is required and cannot be empty",
        "rejectedValue": ""
      },
      {
        "field": "email",
        "message": "Email must be a valid email address",
        "rejectedValue": "invalid-email"
      },
      {
        "field": "age",
        "message": "Age must be between 0 and 150",
        "rejectedValue": -5
      },
      {
        "field": "phone",
        "message": "Phone must be at least 10 digits",
        "rejectedValue": "123"
      }
    ],
    "timestamp": "2026-01-23T10:30:00Z",
    "path": "/api/customers",
    "requestId": "req-xyz789"
  }
}

---

# 2. AUTHENTICATION ERROR (401 Unauthorized)
GET /api/orders HTTP/1.1
Authorization: Bearer expired_token_abc123

HTTP/1.1 401 Unauthorized
WWW-Authenticate: Bearer error="invalid_token", error_description="Token has expired"
Content-Type: application/json

{
  "error": {
    "code": "TOKEN_EXPIRED",
    "message": "Authentication token has expired",
    "details": "Please login again to obtain a new token",
    "timestamp": "2026-01-23T10:30:00Z",
    "path": "/api/orders"
  }
}

---

# 3. AUTHORIZATION ERROR (403 Forbidden)
DELETE /api/users/456 HTTP/1.1
Authorization: Bearer valid_token_regular_user

HTTP/1.1 403 Forbidden
Content-Type: application/json

{
  "error": {
    "code": "INSUFFICIENT_PERMISSIONS",
    "message": "You don't have permission to perform this action",
    "details": "Admin role required to delete user accounts",
    "requiredRole": "Admin",
    "userRole": "Employee",
    "timestamp": "2026-01-23T10:30:00Z",
    "path": "/api/users/456"
  }
}

---

# 4. RESOURCE NOT FOUND (404 Not Found)
GET /api/customers/99999 HTTP/1.1
Authorization: Bearer valid_token

HTTP/1.1 404 Not Found
Content-Type: application/json

{
  "error": {
    "code": "RESOURCE_NOT_FOUND",
    "message": "Customer not found",
    "details": "No customer exists with ID: 99999",
    "resourceType": "Customer",
    "resourceId": "99999",
    "timestamp": "2026-01-23T10:30:00Z",
    "path": "/api/customers/99999"
  }
}

---

# 5. CONFLICT ERROR (409 Conflict)
POST /api/customers HTTP/1.1
Content-Type: application/json

{
  "email": "existing@example.com",
  "name": "New User"
}

HTTP/1.1 409 Conflict
Content-Type: application/json

{
  "error": {
    "code": "DUPLICATE_RESOURCE",
    "message": "Customer with this email already exists",
    "details": "A customer with email 'existing@example.com' is already registered",
    "conflictingField": "email",
    "conflictingValue": "existing@example.com",
    "existingResourceId": "CUST-123",
    "timestamp": "2026-01-23T10:30:00Z",
    "path": "/api/customers"
  }
}

---

# 6. RATE LIMIT ERROR (429 Too Many Requests)
GET /api/products HTTP/1.1
Authorization: Bearer valid_token

HTTP/1.1 429 Too Many Requests
Retry-After: 300
Content-Type: application/json

{
  "error": {
    "code": "RATE_LIMIT_EXCEEDED",
    "message": "API rate limit exceeded",
    "details": "You have exceeded the maximum of 1000 requests per hour",
    "limit": 1000,
    "remaining": 0,
    "resetAt": "2026-01-23T11:00:00Z",
    "retryAfter": 300,
    "timestamp": "2026-01-23T10:30:00Z",
    "path": "/api/products"
  }
}

---

# 7. BUSINESS LOGIC ERROR (422 Unprocessable Entity)
POST /api/orders/ORD-501/ship HTTP/1.1

HTTP/1.1 422 Unprocessable Entity
Content-Type: application/json

{
  "error": {
    "code": "INVALID_ORDER_STATE",
    "message": "Cannot ship order in current state",
    "details": "Order must be in 'paid' status to be shipped. Current status: 'pending'",
    "currentState": "pending",
    "requiredState": "paid",
    "orderId": "ORD-501",
    "timestamp": "2026-01-23T10:30:00Z",
    "path": "/api/orders/ORD-501/ship"
  }
}

---

# 8. INTERNAL SERVER ERROR (500)
GET /api/customers HTTP/1.1

HTTP/1.1 500 Internal Server Error
Content-Type: application/json

{
  "error": {
    "code": "INTERNAL_SERVER_ERROR",
    "message": "An unexpected error occurred",
    "details": "We're working to fix this issue. Please try again later.",
    "timestamp": "2026-01-23T10:30:00Z",
    "path": "/api/customers",
    "requestId": "req-error-abc123",
    "supportContact": "support@company.com"
  }
}
# Note: Don't expose stack traces or internal details in production

---

# 9. SERVICE UNAVAILABLE (503)
GET /api/orders HTTP/1.1

HTTP/1.1 503 Service Unavailable
Retry-After: 120
Content-Type: application/json

{
  "error": {
    "code": "SERVICE_UNAVAILABLE",
    "message": "Service temporarily unavailable",
    "details": "The service is undergoing maintenance. Please try again in 2 minutes.",
    "retryAfter": 120,
    "estimatedRestoreTime": "2026-01-23T10:32:00Z",
    "timestamp": "2026-01-23T10:30:00Z"
  }
}
```

**Error Handling Best Practices:**
* Use appropriate HTTP status codes
* Provide consistent error response structure
* Include helpful error messages and details
* Don't expose sensitive information or stack traces
* Include request IDs for traceability
* Log errors server-side with full context
* Use specific error codes for programmatic handling
* Provide timestamp and path information
* Include suggestions for resolution when possible

---

### 23. What is the difference between PUT vs POST for creating resources?

**POST** is typically used to create new resources where the server assigns the ID, while **PUT** can create resources when the client specifies the ID. POST is non-idempotent (multiple calls create multiple resources), PUT is idempotent (multiple identical calls have the same effect).

**Why it's needed:** Understanding when to use POST vs PUT affects API semantics, client behavior, and system reliability. The distinction impacts idempotency guarantees, URL structure, and how clients interact with resources.

**When to use POST:** Use when creating resources where server generates unique IDs, submitting forms, triggering actions, or any operation where multiple identical requests should have different outcomes.

**When to use PUT:** Use when client knows the resource identifier, creating or replacing resources at specific URLs, or implementing idempotent creation operations.

| Aspect | POST | PUT |
|--------|------|-----|
| **Primary Use** | Create new resource | Update/replace existing resource |
| **Can Create?** | Yes (server assigns ID) | Yes (client provides ID) |
| **Idempotent** | No | Yes |
| **URI** | Collection URI ( `/api/orders` ) | Specific resource URI ( `/api/orders/123` ) |
| **Response Status** | 201 Created | 200 OK or 204 No Content |
| **Location Header** | Yes (new resource URL) | Optional |
| **Multiple Calls** | Creates multiple resources | Same result |

**Real-time Example:**

```http
# POST - Server assigns ID (typical creation)
POST /api/customers HTTP/1.1
Content-Type: application/json

{
  "name": "Alice Johnson",
  "email": "alice@example.com",
  "phone": "+1-555-0123"
}

HTTP/1.1 201 Created
Location: /api/customers/CUST-789
Content-Type: application/json

{
  "customerId": "CUST-789",  # Server generated ID
  "name": "Alice Johnson",
  "email": "alice@example.com",
  "phone": "+1-555-0123",
  "createdAt": "2026-01-23T10:30:00Z"
}

# Calling POST again creates ANOTHER customer with different ID
POST /api/customers HTTP/1.1
(same body)

HTTP/1.1 201 Created
Location: /api/customers/CUST-790  # New ID!
{
  "customerId": "CUST-790",  # Different resource created
  ...
}

---

# PUT - Client specifies ID (idempotent creation/update)
# First call - Resource doesn't exist, creates it
PUT /api/customers/CUST-999 HTTP/1.1
Content-Type: application/json

{
  "customerId": "CUST-999",
  "name": "Bob Smith",
  "email": "bob@example.com",
  "phone": "+1-555-0456"
}

HTTP/1.1 201 Created  # Resource created
Location: /api/customers/CUST-999
{
  "customerId": "CUST-999",
  "name": "Bob Smith",
  "email": "bob@example.com",
  "phone": "+1-555-0456"
}

# Second call - Resource exists, replaces it (IDEMPOTENT)
PUT /api/customers/CUST-999 HTTP/1.1
(same body)

HTTP/1.1 200 OK  # Or 204 No Content
{
  "customerId": "CUST-999",
  "name": "Bob Smith",
  "email": "bob@example.com",
  "phone": "+1-555-0456"
}
# Same resource, same state - no duplicate created

---

# Real-world scenario: Order Creation

# POST for orders (server generates order ID)
POST /api/orders HTTP/1.1
Content-Type: application/json
Authorization: Bearer token123

{
  "customerId": "CUST-789",
  "items": [
    {"productId": "PROD-001", "quantity": 2, "price": 29.99}
  ],
  "shippingAddress": {
    "street": "123 Main St",
    "city": "Seattle",
    "state": "WA",
    "zip": "98101"
  }
}

HTTP/1.1 201 Created
Location: /api/orders/ORD-20260123-001
{
  "orderId": "ORD-20260123-001",  # Server-generated
  "status": "pending",
  "total": 59.98,
  "createdAt": "2026-01-23T10:30:00Z"
}

---

# PUT for configuration/settings (client controls ID)
PUT /api/users/USR-456/preferences HTTP/1.1
Content-Type: application/json

{
  "theme": "dark",
  "language": "en-US",
  "notifications": {
    "email": true,
    "sms": false,
    "push": true
  }
}

HTTP/1.1 200 OK
# Idempotent - calling multiple times with same data = same result

---

# Special case: Upsert pattern with PUT
# Create if doesn't exist, update if exists

PUT /api/cache/user-session-123 HTTP/1.1
Content-Type: application/json

{
  "userId": 123,
  "sessionData": {...},
  "expiresAt": "2026-01-23T12:00:00Z"
}

# If session doesn't exist: 201 Created
# If session exists: 200 OK (updated)
# Multiple identical calls: Same result (idempotent)
```

**Decision Matrix:**

```
Need to create resource?
├─ Server should assign ID?
│  └─ Use POST /api/resources
│     Response: 201 Created with Location header
│
└─ Client knows/controls ID?
   └─ Use PUT /api/resources/{id}
      Response: 201 Created (new) or 200 OK (updated)

Need to update resource?
├─ Full replacement?
│  └─ Use PUT /api/resources/{id}
│
└─ Partial update?
   └─ Use PATCH /api/resources/{id}
```

---

### 24. What is the difference between Synchronous and Asynchronous API patterns?

**Synchronous APIs** block and wait for operations to complete before returning a response, while **Asynchronous APIs** immediately return and process requests in the background, notifying clients when complete (via callbacks, polling, or webhooks).

**Why it's needed:** Long-running operations (file processing, report generation, batch jobs) would cause timeouts in synchronous mode. Asynchronous patterns prevent blocking, improve responsiveness, enable better resource utilization, and handle time-intensive tasks gracefully.

**When to use Synchronous:** Quick operations (< 30 seconds), simple CRUD, real-time data requirements, when immediate response contains all needed data.

**When to use Asynchronous:** Long-running tasks (> 30 seconds), batch processing, file uploads/conversions, external service dependencies, report generation, data imports/exports.

| Aspect | Synchronous | Asynchronous |
|--------|-------------|--------------|
| **Response Time** | Waits for completion | Immediate acknowledgment |
| **Client Blocks** | Yes | No |
| **Use Case** | Quick operations | Long-running tasks |
| **Status Checking** | Not needed | Required (polling/webhooks) |
| **Timeout Risk** | High for long tasks | Low |
| **Complexity** | Simple | More complex |
| **Resource Usage** | Holds connection open | Releases connection |

**Real-time Example:**

```http
# SYNCHRONOUS API PATTERN

# Simple CRUD - Quick operation
POST /api/customers HTTP/1.1
Content-Type: application/json

{
  "name": "John Doe",
  "email": "john@example.com"
}

# Response immediately with result
HTTP/1.1 201 Created
{
  "customerId": "CUST-123",
  "name": "John Doe",
  "status": "active"
}
# Client waits ~100-500ms, acceptable

---

# ASYNCHRONOUS API PATTERN

# Scenario: Generate complex sales report (takes 5 minutes)

# Step 1: Client initiates long-running task
POST /api/reports/sales HTTP/1.1
Content-Type: application/json
Authorization: Bearer token123

{
  "reportType": "annual-sales",
  "year": 2025,
  "format": "pdf",
  "includeCharts": true,
  "emailWhenReady": "user@company.com"
}

# Immediate response with job ID (202 Accepted)
HTTP/1.1 202 Accepted
Location: /api/reports/jobs/JOB-789
Content-Type: application/json

{
  "jobId": "JOB-789",
  "status": "pending",
  "message": "Report generation started",
  "estimatedCompletionTime": "2026-01-23T10:35:00Z",
  "statusUrl": "/api/reports/jobs/JOB-789",
  "createdAt": "2026-01-23T10:30:00Z"
}

---

# Step 2: Client polls for status (Polling Pattern)

GET /api/reports/jobs/JOB-789 HTTP/1.1
Authorization: Bearer token123

# While processing:
HTTP/1.1 200 OK
{
  "jobId": "JOB-789",
  "status": "processing",
  "progress": 45,  # 45% complete
  "message": "Generating charts...",
  "createdAt": "2026-01-23T10:30:00Z"
}

# Poll again after 30 seconds...

---

# Step 3: Job completed

GET /api/reports/jobs/JOB-789 HTTP/1.1

HTTP/1.1 200 OK
{
  "jobId": "JOB-789",
  "status": "completed",
  "progress": 100,
  "result": {
    "reportId": "RPT-456",
    "downloadUrl": "/api/reports/RPT-456/download",
    "expiresAt": "2026-01-30T10:35:00Z",
    "fileSize": "2.5 MB",
    "format": "pdf"
  },
  "completedAt": "2026-01-23T10:35:00Z"
}

# Client downloads report
GET /api/reports/RPT-456/download HTTP/1.1

HTTP/1.1 200 OK
Content-Type: application/pdf
Content-Disposition: attachment; filename="sales-report-2025.pdf"
(PDF binary data)

---

# ALTERNATIVE: Webhook Pattern (no polling needed)

POST /api/reports/sales HTTP/1.1
Content-Type: application/json

{
  "reportType": "annual-sales",
  "year": 2025,
  "webhookUrl": "https://myapp.com/webhooks/report-complete"
}

HTTP/1.1 202 Accepted
{
  "jobId": "JOB-890",
  "status": "pending"
}

# When complete, server calls webhook:
POST /webhooks/report-complete HTTP/1.1
Host: myapp.com
Content-Type: application/json
X-Webhook-Event: report.completed

{
  "jobId": "JOB-890",
  "status": "completed",
  "reportId": "RPT-457",
  "downloadUrl": "https://api.company.com/api/reports/RPT-457/download"
}

---

# ERROR HANDLING IN ASYNC APIs

GET /api/reports/jobs/JOB-789 HTTP/1.1

# Job failed:
HTTP/1.1 200 OK
{
  "jobId": "JOB-789",
  "status": "failed",
  "error": {
    "code": "DATA_UNAVAILABLE",
    "message": "Unable to generate report: No sales data for 2025",
    "details": "Please ensure data exists for the requested period"
  },
  "failedAt": "2026-01-23T10:32:00Z"
}

---

# FILE UPLOAD WITH ASYNC PROCESSING

# Step 1: Upload file
POST /api/documents/upload HTTP/1.1
Content-Type: multipart/form-data
Authorization: Bearer token123

(file data: large-document.pdf - 50MB)

HTTP/1.1 202 Accepted
{
  "uploadId": "UPL-555",
  "status": "processing",
  "message": "File uploaded, processing virus scan and OCR...",
  "statusUrl": "/api/documents/uploads/UPL-555"
}

# Step 2: Check processing status
GET /api/documents/uploads/UPL-555 HTTP/1.1

HTTP/1.1 200 OK
{
  "uploadId": "UPL-555",
  "status": "completed",
  "result": {
    "documentId": "DOC-999",
    "fileName": "large-document.pdf",
    "virusScanResult": "clean",
    "ocrCompleted": true,
    "url": "/api/documents/DOC-999"
  }
}
```

**Common Async Patterns:**

01. **202 Accepted + Polling**: Return job ID, client polls status
02. **Webhooks**: Server notifies client when complete
03. **Server-Sent Events (SSE)**: Real-time updates pushed to client
04. **WebSockets**: Bidirectional real-time communication
05. **Message Queue**: Decouple request from processing

**Status Codes for Async:**
* `202 Accepted` - Request accepted, processing async
* `200 OK` - Status check successful
* `303 See Other` - Resource ready, redirect to result
* `410 Gone` - Job completed and cleaned up

---

### 25. What is API Gateway and what are its benefits?

**API Gateway** is a server that acts as a single entry point for all client requests to microservices. It handles routing, authentication, rate limiting, request/response transformation, load balancing, and cross-cutting concerns, abstracting backend complexity from clients.

**Why it's needed:** In microservices architecture, clients would otherwise need to know multiple service endpoints, handle different auth mechanisms, manage rate limits per service, and deal with protocol differences. API Gateway centralizes these concerns.

**When to use:** Use in microservices architectures, when exposing multiple backends through unified API, requiring centralized security/monitoring, managing multiple API versions, or needing to aggregate responses from multiple services.

**API Gateway Functions:**

| Function | Description | Benefit |
|----------|-------------|---------|
| **Request Routing** | Routes to appropriate microservice | Single endpoint for clients |
| **Authentication** | Validates credentials/tokens | Centralized security |
| **Authorization** | Checks permissions | Consistent access control |
| **Rate Limiting** | Throttles requests | Protects backend services |
| **Load Balancing** | Distributes traffic | High availability |
| **Caching** | Caches responses | Reduced latency |
| **Request/Response Transformation** | Modifies data format | Protocol translation |
| **Aggregation** | Combines multiple service calls | Reduces client requests |
| **Monitoring & Logging** | Tracks all API traffic | Centralized observability |

**Real-time Example:**

```http
# WITHOUT API GATEWAY - Client calls multiple services directly

# Client needs to make 3 separate calls:

# 1. Get customer info from Customer Service
GET https://customer-service.internal:8001/api/customers/123 HTTP/1.1
Authorization: Bearer token_for_customer_service

# 2. Get orders from Order Service
GET https://order-service.internal:8002/api/customers/123/orders HTTP/1.1
Authorization: Bearer token_for_order_service

# 3. Get payment methods from Payment Service
GET https://payment-service.internal:8003/api/customers/123/payment-methods HTTP/1.1
Authorization: Bearer token_for_payment_service

# Problems:
# - Client knows internal URLs and ports
# - Multiple authentication mechanisms
# - 3 network round trips
# - Different error formats
# - No centralized rate limiting

---

# WITH API GATEWAY - Single unified entry point

# Single call to API Gateway
GET https://api.company.com/api/customers/123/complete-profile HTTP/1.1
Authorization: Bearer user_token_abc123

# API Gateway:
# 1. Validates JWT token (authentication)
# 2. Checks rate limits for this user
# 3. Routes to appropriate microservices
# 4. Calls all 3 services in parallel:
#    - Customer Service: /customers/123
#    - Order Service: /customers/123/orders
#    - Payment Service: /customers/123/payment-methods
# 5. Aggregates responses
# 6. Transforms to unified format
# 7. Caches result (if applicable)
# 8. Returns combined response

HTTP/1.1 200 OK
Content-Type: application/json
X-RateLimit-Remaining: 995
X-Response-Time: 245ms
X-Cache-Status: MISS

{
  "customer": {
    "customerId": 123,
    "name": "John Doe",
    "email": "john@example.com",
    "status": "active"
  },
  "orders": [
    {
      "orderId": "ORD-501",
      "total": 299.99,
      "status": "shipped",
      "createdAt": "2026-01-20T10:00:00Z"
    },
    {
      "orderId": "ORD-502",
      "total": 149.50,
      "status": "delivered",
      "createdAt": "2026-01-15T14:30:00Z"
    }
  ],
  "paymentMethods": [
    {
      "id": "PM-789",
      "type": "credit_card",
      "last4": "4242",
      "isDefault": true
    }
  ]
}

---

# API GATEWAY ROUTING CONFIGURATION (conceptual)

# Route 1: Customer operations
GET /api/customers/*         → customer-service:8001/api/customers/*
POST /api/customers           → customer-service:8001/api/customers

# Route 2: Order operations  
GET /api/orders/*             → order-service:8002/api/orders/*
POST /api/orders              → order-service:8002/api/orders

# Route 3: Product catalog
GET /api/products/*           → product-service:8003/api/products/*

# Route 4: Aggregated endpoint
GET /api/customers/*/profile  → Aggregate from multiple services

---

# API GATEWAY FEATURES IN ACTION

# 1. AUTHENTICATION & AUTHORIZATION
Client Request:
GET /api/orders HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

API Gateway:
- Validates JWT signature
- Checks token expiration
- Extracts userId and roles
- Passes to backend with headers:
  X-User-ID: 123
  X-User-Roles: Employee,Customer
  
---

# 2. RATE LIMITING
Client Request:
GET /api/products HTTP/1.1

API Gateway Response:
HTTP/1.1 200 OK
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 847
X-RateLimit-Reset: 1706015400

# If exceeded:
HTTP/1.1 429 Too Many Requests
{
  "error": "Rate limit exceeded",
  "retryAfter": 60
}

---

# 3. REQUEST/RESPONSE TRANSFORMATION

# Client sends:
POST /api/v1/orders HTTP/1.1
{
  "items": [{"id": 1, "qty": 2}]
}

# Gateway transforms to backend format:
POST /orders-service/v2/create HTTP/1.1
{
  "orderItems": [{"productId": 1, "quantity": 2}],
  "timestamp": "2026-01-23T10:30:00Z"
}

# Backend responds:
{"order_id": 123, "status": "pending"}

# Gateway transforms to client format:
{
  "orderId": 123,
  "orderStatus": "pending"
}

---

# 4. CIRCUIT BREAKER (Fault Tolerance)

GET /api/products HTTP/1.1

# If product-service is down:
# Gateway detects failures, opens circuit breaker
# Returns cached response or fallback:

HTTP/1.1 200 OK
X-Circuit-Status: OPEN
X-Data-Source: CACHE

{
  "products": [...],  # From cache
  "notice": "Live data temporarily unavailable"
}

---

# 5. LOAD BALANCING

Client Request:
GET /api/customers/123 HTTP/1.1

# Gateway routes to one of multiple instances:
# Round-robin or least-connections algorithm
→ customer-service-instance-1:8001  (40% load)
→ customer-service-instance-2:8001  (35% load)  ← Selected
→ customer-service-instance-3:8001  (45% load)

---

# 6. API VERSIONING

# Gateway routes based on version:
GET /api/v1/customers/123  → customer-service-v1
GET /api/v2/customers/123  → customer-service-v2

# Or header-based:
GET /api/customers/123 HTTP/1.1
API-Version: 2.0
→ Routes to customer-service-v2
```

**Popular API Gateway Solutions:**
* **AWS API Gateway** - Fully managed, serverless
* **Kong** - Open-source, plugin-based
* **NGINX** - High-performance, widely used
* **Apigee** - Enterprise-grade, Google Cloud
* **Azure API Management** - Microsoft Azure
* **Tyk** - Open-source, Docker-native
* **Ocelot** - . NET Core, lightweight

**API Gateway Benefits:**
* Simplified client code (single endpoint)
* Centralized security and authentication
* Reduced number of client requests (aggregation)
* Backend service independence (routing abstraction)
* Consistent error handling and logging
* Easy A/B testing and canary deployments
* Protocol translation (REST to gRPC, etc.)

---

### 26. What is API throttling and how does it differ from rate limiting?

**API Throttling** is the broader concept of controlling API consumption, while **Rate Limiting** is a specific throttling technique that limits requests per time window. Throttling includes rate limiting, but also encompasses quota management, request queuing, and dynamic adjustment based on system load.

**Why it's needed:** Prevents resource exhaustion, ensures fair usage across clients, protects against DDoS attacks, enables tiered pricing models, and maintains service quality during peak loads.

**When to use:** Implement on all public APIs, authentication endpoints, resource-intensive operations, and services with multiple pricing tiers. Essential for protecting infrastructure and ensuring equitable access.

| Aspect | Rate Limiting | Throttling (Broader) |
|--------|---------------|---------------------|
| **Scope** | Time-based request limits | All consumption control mechanisms |
| **Techniques** | Fixed/sliding window, token bucket | Rate limits, quotas, queuing, prioritization |
| **Measurement** | Requests per time unit | Requests, data volume, compute time |
| **Response** | Reject (429) when exceeded | Reject, queue, slow down, prioritize |
| **Examples** | 1000 req/hour, 10 req/second | Daily quotas, bandwidth limits, CPU throttling |

**Real-time Example:**

```http
# 1. RATE LIMITING - Requests per time window

# Client makes requests
GET /api/products HTTP/1.1
Authorization: Bearer user_token_123

HTTP/1.1 200 OK
X-RateLimit-Limit: 100          # Max 100 requests per minute
X-RateLimit-Remaining: 87       # 87 requests left
X-RateLimit-Reset: 1706012460   # Resets at this timestamp
X-RateLimit-Window: 60          # 60-second window

# After 100 requests in 1 minute:
GET /api/products HTTP/1.1

HTTP/1.1 429 Too Many Requests
Retry-After: 30                  # Try again in 30 seconds
X-RateLimit-Limit: 100
X-RateLimit-Remaining: 0
X-RateLimit-Reset: 1706012460

{
  "error": "RateLimitExceeded",
  "message": "API rate limit of 100 requests per minute exceeded"
}

---

# 2. QUOTA-BASED THROTTLING - Total usage limits

# Monthly quota system
GET /api/data/export HTTP/1.1
Authorization: Bearer token_free_tier

HTTP/1.1 200 OK
X-Quota-Limit: 10000            # 10,000 requests per month
X-Quota-Used: 8547              # 8,547 used so far
X-Quota-Remaining: 1453         # 1,453 remaining
X-Quota-Reset: 2026-02-01T00:00:00Z

# When quota exhausted:
GET /api/data/export HTTP/1.1

HTTP/1.1 402 Payment Required  # or 429
X-Quota-Limit: 10000
X-Quota-Used: 10000
X-Quota-Remaining: 0

{
  "error": "QuotaExceeded",
  "message": "Monthly quota of 10,000 requests exceeded",
  "quotaResetDate": "2026-02-01",
  "upgradeUrl": "https://api.company.com/upgrade"
}

---

# 3. TIERED THROTTLING - Different limits per plan

# FREE TIER
GET /api/search HTTP/1.1
Authorization: Bearer free_user_token

HTTP/1.1 200 OK
X-RateLimit-Limit: 100          # 100 req/hour
X-RateLimit-Tier: free
X-Quota-Limit: 10000            # 10K req/month

---

# PREMIUM TIER
GET /api/search HTTP/1.1
Authorization: Bearer premium_user_token

HTTP/1.1 200 OK
X-RateLimit-Limit: 10000        # 10,000 req/hour
X-RateLimit-Tier: premium
X-Quota-Limit: 1000000          # 1M req/month

---

# ENTERPRISE TIER
GET /api/search HTTP/1.1
Authorization: Bearer enterprise_user_token

HTTP/1.1 200 OK
X-RateLimit-Limit: unlimited
X-RateLimit-Tier: enterprise
X-Quota-Limit: unlimited

---

# 4. BANDWIDTH THROTTLING - Data volume limits

# Downloading large file
GET /api/files/large-report.pdf HTTP/1.1

HTTP/1.1 200 OK
Content-Length: 52428800        # 50 MB file
X-Bandwidth-Limit: 10485760     # 10 MB/minute limit
X-Bandwidth-Used: 8388608       # 8 MB used this minute

# If bandwidth exceeded:
HTTP/1.1 429 Too Many Requests
{
  "error": "BandwidthLimitExceeded",
  "message": "Data transfer limit of 10 MB/minute exceeded",
  "limitMB": 10,
  "usedMB": 12.5
}

---

# 5. CONCURRENT REQUEST THROTTLING

# User has 5 concurrent requests (limit: 5)
GET /api/reports/generate HTTP/1.1  # Request 1 - processing
GET /api/reports/generate HTTP/1.1  # Request 2 - processing
GET /api/reports/generate HTTP/1.1  # Request 3 - processing
GET /api/reports/generate HTTP/1.1  # Request 4 - processing
GET /api/reports/generate HTTP/1.1  # Request 5 - processing

# 6th concurrent request rejected:
GET /api/reports/generate HTTP/1.1

HTTP/1.1 429 Too Many Requests
X-Concurrent-Limit: 5
X-Concurrent-Active: 5

{
  "error": "ConcurrentLimitExceeded",
  "message": "Maximum 5 concurrent requests allowed",
  "activeRequests": 5,
  "suggestion": "Wait for existing requests to complete"
}

---

# 6. ADAPTIVE THROTTLING - Based on system load

# Normal load - Full speed
GET /api/data HTTP/1.1

HTTP/1.1 200 OK
X-System-Load: normal
X-RateLimit-Limit: 1000

# High system load - Reduced limits
GET /api/data HTTP/1.1

HTTP/1.1 200 OK
X-System-Load: high
X-RateLimit-Limit: 500          # Dynamically reduced
X-Throttle-Reason: high-system-load

# Critical load - Severe throttling
GET /api/data HTTP/1.1

HTTP/1.1 503 Service Unavailable
Retry-After: 60
X-System-Load: critical

{
  "error": "ServiceOverloaded",
  "message": "System under heavy load, please retry later"
}

---

# 7. REQUEST QUEUING - Instead of rejecting

POST /api/batch/process HTTP/1.1
Content-Type: application/json

{
  "data": [...]  # Large batch job
}

# Instead of rejecting, queue the request:
HTTP/1.1 202 Accepted
X-Queue-Position: 15
X-Estimated-Wait: 120           # seconds

{
  "jobId": "JOB-789",
  "status": "queued",
  "queuePosition": 15,
  "estimatedWaitTime": 120,
  "statusUrl": "/api/jobs/JOB-789"
}

---

# 8. COMBINED THROTTLING EXAMPLE

GET /api/analytics/dashboard HTTP/1.1
Authorization: Bearer token_123

HTTP/1.1 200 OK
# Multiple throttling metrics:
X-RateLimit-Limit: 1000         # Per hour rate limit
X-RateLimit-Remaining: 847
X-Quota-Daily: 50000            # Daily quota
X-Quota-Daily-Used: 12543
X-Bandwidth-Limit-MB: 100       # Bandwidth limit
X-Bandwidth-Used-MB: 45.8
X-Concurrent-Limit: 10          # Concurrent requests
X-Concurrent-Active: 3
X-Tier: professional

{
  "dashboardData": {...}
}
```

**Throttling Strategies:**
01. **Token Bucket** - Tokens refill at constant rate, allow bursts
02. **Leaky Bucket** - Requests processed at constant rate, smooth traffic
03. **Fixed Window** - Reset limits at fixed intervals
04. **Sliding Window** - Smooth distribution across time
05. **Dynamic Throttling** - Adjust based on system load
06. **Priority Throttling** - Different limits for different user tiers

**Implementation Best Practices:**
* Return clear headers (limit, remaining, reset)
* Use 429 status code with Retry-After header
* Implement per-user and per-IP throttling
* Provide upgrade paths for higher limits
* Log throttling events for analysis
* Use distributed throttling (Redis) for multiple servers
* Consider geographic distribution
* Implement graceful degradation during overload

---

### 27. What is API documentation and why is it important?

**API Documentation** is comprehensive information about API endpoints, request/response formats, authentication, error codes, examples, and usage guidelines. It serves as the contract between API providers and consumers, enabling effective integration and maintenance.

**Why it's needed:** Good documentation reduces integration time, decreases support requests, improves developer experience, increases API adoption, and serves as reference for current and future developers. Poor documentation leads to frustration, errors, and abandoned integrations.

**When to use:** Document all APIs - internal microservices, public APIs, partner integrations. Generate documentation early in development, maintain it alongside code changes, and version it with API versions.

**Essential Documentation Components:**

| Component | Description | Example |
|-----------|-------------|---------|
| **Overview** | API purpose, capabilities, getting started | "REST API for customer management" |
| **Authentication** | Auth methods, obtaining credentials | OAuth 2.0, API keys, JWT |
| **Endpoints** | Available URIs, methods, purposes | `GET /api/customers` |
| **Request Format** | Parameters, headers, body schema | Query params, JSON body |
| **Response Format** | Success responses, data structure | Status codes, response schema |
| **Error Handling** | Error codes, messages, troubleshooting | 400, 401, 404, 500 examples |
| **Examples** | Real requests/responses, use cases | cURL, code samples |
| **Rate Limits** | Throttling policies, quotas | 1000 req/hour |
| **Versioning** | Version strategy, migration guides | v1 vs v2 differences |
| **SDKs & Libraries** | Client libraries, code generators | Python, JavaScript, C# SDKs |

**Real-time Example - Customer API Documentation:**

```markdown
# Customer Management API Documentation

## Overview

The Customer API enables management of customer records, including creation, 
retrieval, updates, and deletion. Base URL: `https://api.company.com`

## Authentication

All requests require Bearer token authentication:

Authorization: Bearer YOUR_ACCESS_TOKEN

To obtain access token:
POST /api/auth/login
{
  "email": "user@company.com",
  "password": "your_password"
}

---

## Endpoints

### 1. Create Customer

Creates a new customer record.

**Endpoint:** `POST /api/customers`

**Headers:**
- `Authorization: Bearer {token}` (required)
- `Content-Type: application/json` (required)

**Request Body:**
{
  "name": "string (required, 2-100 characters)",
  "email": "string (required, valid email format)",
  "phone": "string (optional, 10-15 digits)",
  "address": {
    "street": "string",
    "city": "string",
    "state": "string",
    "zip": "string"
  }
}

**Success Response:** `201 Created`
{
  "customerId": "CUST-789",
  "name": "John Doe",
  "email": "john@example.com",
  "phone": "+1-555-0123",
  "status": "active",
  "createdAt": "2026-01-23T10:30:00Z"
}

**Error Responses:**

400 Bad Request - Validation error
{
  "error": "VALIDATION_ERROR",
  "message": "Invalid email format",
  "field": "email"
}

401 Unauthorized - Invalid or missing token
{
  "error": "UNAUTHORIZED",
  "message": "Authentication token required"
}

409 Conflict - Duplicate email
{
  "error": "DUPLICATE_RESOURCE",
  "message": "Customer with this email already exists"
}

**Example cURL:**
curl -X POST https://api.company.com/api/customers \
  -H "Authorization: Bearer eyJhbGc..." \
  -H "Content-Type: application/json" \
  -d '{
    "name": "John Doe",
    "email": "john@example.com",
    "phone": "+1-555-0123"
  }'

**Example JavaScript:**
const response = await fetch('https://api.company.com/api/customers', {
  method: 'POST',
  headers: {
    'Authorization': 'Bearer YOUR_TOKEN',
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    name: 'John Doe',
    email: 'john@example.com',
    phone: '+1-555-0123'
  })
});
const customer = await response.json();

---

### 2. Get Customer

Retrieves a specific customer by ID.

**Endpoint:** `GET /api/customers/{customerId}`

**Path Parameters:**
- `customerId` (string, required) - Unique customer identifier

**Headers:**
- `Authorization: Bearer {token}` (required)

**Success Response:** `200 OK`
{
  "customerId": "CUST-789",
  "name": "John Doe",
  "email": "john@example.com",
  "phone": "+1-555-0123",
  "status": "active",
  "address": {
    "street": "123 Main St",
    "city": "Seattle",
    "state": "WA",
    "zip": "98101"
  },
  "createdAt": "2026-01-20T10:00:00Z",
  "updatedAt": "2026-01-23T10:30:00Z"
}

**Error Responses:**

404 Not Found - Customer doesn't exist
{
  "error": "RESOURCE_NOT_FOUND",
  "message": "Customer with ID CUST-999 not found"
}

**Example cURL:**
curl -X GET https://api.company.com/api/customers/CUST-789 \
  -H "Authorization: Bearer eyJhbGc..."

---

### 3. List Customers

Retrieves paginated list of customers with optional filtering.

**Endpoint:** `GET /api/customers`

**Query Parameters:**
- `page` (integer, optional, default: 1) - Page number
- `pageSize` (integer, optional, default: 20, max: 100) - Items per page
- `status` (string, optional) - Filter by status: active, inactive
- `search` (string, optional) - Search by name or email
- `sortBy` (string, optional) - Sort field: name, email, createdAt
- `sortOrder` (string, optional) - asc or desc

**Success Response:** `200 OK`
{
  "data": [
    {
      "customerId": "CUST-789",
      "name": "John Doe",
      "email": "john@example.com",
      "status": "active"
    }
  ],
  "pagination": {
    "currentPage": 1,
    "pageSize": 20,
    "totalPages": 5,
    "totalCount": 87
  }
}

**Example:**
GET /api/customers?page=1&pageSize=20&status=active&sortBy=name&sortOrder=asc

---

### 4. Update Customer

Updates customer information (partial update).

**Endpoint:** `PATCH /api/customers/{customerId}`

**Request Body:** (all fields optional)
{
  "name": "string",
  "phone": "string",
  "address": { ... }
}

**Success Response:** `200 OK`

---

### 5. Delete Customer

Deletes a customer record.

**Endpoint:** `DELETE /api/customers/{customerId}`

**Success Response:** `204 No Content`

---

## Rate Limiting

- Free tier: 1,000 requests/hour
- Premium tier: 10,000 requests/hour
- Enterprise: Unlimited

Headers returned:
- `X-RateLimit-Limit`: Maximum requests per hour
- `X-RateLimit-Remaining`: Requests remaining
- `X-RateLimit-Reset`: Timestamp when limit resets

When exceeded:
429 Too Many Requests
{
  "error": "RATE_LIMIT_EXCEEDED",
  "retryAfter": 300
}

---

## Versioning

Current version: v1
Base URL includes version: https://api.company.com/api/v1/

Migration guide for v2: /docs/migration-v1-to-v2

---

## SDKs and Libraries

- Python: pip install company-api-client
- JavaScript: npm install @company/api-client
- C#: Install-Package Company.Api.Client

---

## Support

- Documentation: https://docs.api.company.com
- Support email: api-support@company.com
- Status page: https://status.api.company.com
```

**Documentation Tools:**
* **Swagger/OpenAPI** - Industry standard, interactive docs
* **Postman** - Collection sharing, examples
* **ReadMe** - User-friendly, interactive
* **API Blueprint** - Markdown-based
* **Redoc** - Clean, responsive OpenAPI renderer
* **Stoplight** - Design-first approach
* **Slate** - Beautiful static docs

**OpenAPI (Swagger) Example:**

```yaml
openapi: 3.0.0
info:
  title: Customer API
  version: 1.0.0
  description: Customer management API
servers:
  - url: https://api.company.com/api/v1

paths:
  /customers:
    post:
      summary: Create customer
      security:
        - bearerAuth: []
      requestBody:
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/CustomerCreate'
      responses:
        '201':
          description: Customer created
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/Customer'
        '400':
          description: Validation error
        '401':
          description: Unauthorized

components:
  schemas:
    Customer:
      type: object
      properties:
        customerId:
          type: string
        name:
          type: string
        email:
          type: string
          format: email
  securitySchemes:
    bearerAuth:
      type: http
      scheme: bearer
      bearerFormat: JWT
```

---

### 28. What are REST API Security Best Practices?

**REST API Security** involves protecting APIs from unauthorized access, data breaches, injection attacks, and abuse through authentication, authorization, encryption, input validation, and security headers.

**Why it's needed:** APIs are prime targets for attacks due to direct data access. Security breaches lead to data theft, financial loss, regulatory penalties, and reputation damage. Comprehensive security measures protect both API and users.

**When to use:** Implement security measures from day one - authentication, HTTPS, input validation, rate limiting. Security is not optional and should be built into the architecture, not added later.

**Security Best Practices:**

| Practice | Implementation | Risk Mitigated |
|----------|----------------|----------------|
| **HTTPS Only** | TLS 1.2+, no HTTP | Man-in-the-middle attacks |
| **Authentication** | JWT, OAuth 2.0, API keys | Unauthorized access |
| **Authorization** | RBAC, ABAC, resource ownership | Privilege escalation |
| **Input Validation** | Whitelist validation, sanitization | Injection attacks |
| **Rate Limiting** | Token bucket, sliding window | DDoS, brute force |
| **CORS Configuration** | Whitelist specific origins | Cross-origin attacks |
| **Security Headers** | CSP, HSTS, X-Frame-Options | Various attacks |
| **Sensitive Data** | Encryption, masking, no logs | Data exposure |
| **Error Messages** | Generic errors, no stack traces | Information disclosure |
| **API Keys** | Rotation, environment-specific | Key compromise |

**Real-time Example:**

```http
# 1. HTTPS ENFORCEMENT
# WRONG - Unencrypted HTTP
http://api.company.com/api/users
# Risk: Credentials and data transmitted in plain text

# CORRECT - Encrypted HTTPS
https://api.company.com/api/users
# All traffic encrypted with TLS 1.2+

# Server configuration: Redirect HTTP to HTTPS
GET http://api.company.com/api/users HTTP/1.1

HTTP/1.1 301 Moved Permanently
Location: https://api.company.com/api/users
Strict-Transport-Security: max-age=31536000; includeSubDomains

---

# 2. STRONG AUTHENTICATION
# Use JWT with short expiration

POST /api/auth/login HTTP/1.1
Content-Type: application/json
{
  "email": "user@company.com",
  "password": "SecureP@ssw0rd!"
}

HTTP/1.1 200 OK
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "tokenType": "Bearer",
  "expiresIn": 900,  # 15 minutes (short-lived)
  "refreshToken": "RT-abc123..."  # Longer-lived
}

# Use token in subsequent requests
GET /api/users/profile HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

---

# 3. AUTHORIZATION - Resource Ownership Check
# User A tries to access User B's data

GET /api/users/456/orders HTTP/1.1
Authorization: Bearer user_a_token
# Token contains: userId=123

# Server checks: token userId (123) != resource userId (456)
HTTP/1.1 403 Forbidden
{
  "error": "FORBIDDEN",
  "message": "Access denied"
}
# Notice: Generic message, doesn't reveal if resource exists

---

# 4. INPUT VALIDATION & SANITIZATION
# Prevent SQL injection, XSS

# VULNERABLE REQUEST:
POST /api/customers HTTP/1.1
{
  "name": "Robert'; DROP TABLE customers;--",
  "email": "<script>alert('XSS')</script>@test.com"
}

# SECURE RESPONSE: Server validates and sanitizes
HTTP/1.1 400 Bad Request
{
  "error": "VALIDATION_ERROR",
  "details": [
    {
      "field": "name",
      "message": "Name contains invalid characters"
    },
    {
      "field": "email",
      "message": "Invalid email format"
    }
  ]
}

# Server-side validation:
# - Whitelist allowed characters
# - Escape special characters  
# - Use parameterized queries (prevents SQL injection)
# - Validate data types, lengths, formats

---

# 5. RATE LIMITING & BRUTE FORCE PROTECTION
# Prevent brute force on login endpoint

POST /api/auth/login HTTP/1.1
{
  "email": "victim@company.com",
  "password": "wrong_password"
}

# After 5 failed attempts:
HTTP/1.1 429 Too Many Requests
Retry-After: 300
{
  "error": "TOO_MANY_ATTEMPTS",
  "message": "Too many login attempts. Account locked for 5 minutes.",
  "unlockAt": "2026-01-23T10:35:00Z"
}

---

# 6. SECURE HEADERS
# Add security-focused HTTP headers

HTTP/1.1 200 OK
# Prevent clickjacking
X-Frame-Options: DENY
X-Content-Type-Options: nosniff
# Content Security Policy
Content-Security-Policy: default-src 'self'
# Force HTTPS
Strict-Transport-Security: max-age=31536000; includeSubDomains; preload
# Control referrer information
Referrer-Policy: no-referrer
# Feature policy
Permissions-Policy: geolocation=(), microphone=(), camera=()
# XSS protection
X-XSS-Protection: 1; mode=block

---

# 7. SENSITIVE DATA HANDLING
# Never expose sensitive data in responses

# BAD RESPONSE:
{
  "userId": 123,
  "email": "user@company.com",
  "password": "hashed_password_here",  # NEVER return passwords!
  "ssn": "123-45-6789",  # Don't expose SSN
  "creditCard": "4532-1234-5678-9010"  # Don't expose full card
}

# GOOD RESPONSE:
{
  "userId": 123,
  "email": "user@company.com",
  "ssnLastFour": "6789",  # Masked
  "creditCardLast4": "9010",  # Masked
  "creditCardType": "Visa"
}

# Secure logging: Never log sensitive data
# BAD LOG:
# [INFO] User login: email=user@company.com, password=P@ssw0rd123

# GOOD LOG:
# [INFO] User login successful: userId=123, timestamp=2026-01-23T10:30:00Z

---

# 8. CORS CONFIGURATION
# Restrict cross-origin access

# INSECURE (allows all origins):
Access-Control-Allow-Origin: *
Access-Control-Allow-Credentials: true
# Problem: Any website can call your API

# SECURE (whitelist specific origins):
Access-Control-Allow-Origin: https://app.company.com
Access-Control-Allow-Methods: GET, POST, PUT, DELETE
Access-Control-Allow-Headers: Authorization, Content-Type
Access-Control-Allow-Credentials: true
Access-Control-Max-Age: 3600

---

# 9. API KEY SECURITY
# Use separate keys per environment

# Development:
X-API-Key: sk_dev_1234567890abcdef

# Production:
X-API-Key: sk_live_abcdef1234567890

# Best practices:
# - Rotate keys regularly
# - Use different keys per client/environment
# - Store keys securely (environment variables, secrets manager)
# - Never commit keys to source control
# - Implement key expiration
# - Monitor key usage for anomalies

---

# 10. ERROR HANDLING - No Information Disclosure
# Don't expose internal details

# BAD RESPONSE (reveals internal structure):
HTTP/1.1 500 Internal Server Error
{
  "error": "SQLException: Table 'users' doesn't exist at line 45 in UserRepository.cs",
  "stackTrace": "at UserRepository.GetUser()...",
  "serverName": "prod-db-01.internal",
  "connectionString": "Server=10.0.1.5;Database=ProductionDB"
}

# GOOD RESPONSE (generic, secure):
HTTP/1.1 500 Internal Server Error
{
  "error": "INTERNAL_SERVER_ERROR",
  "message": "An unexpected error occurred. Please try again later.",
  "requestId": "req-abc123",  # For support/debugging
  "timestamp": "2026-01-23T10:30:00Z"
}
# Log full details server-side securely

---

# 11. SQL INJECTION PREVENTION
# Use parameterized queries, ORM

# VULNERABLE CODE:
# query = "SELECT * FROM users WHERE email = '" + userEmail + "'"

# Attacker sends:
# email = "' OR '1'='1'; DROP TABLE users;--"

# SECURE CODE:
# Use parameterized query or ORM:
# query = "SELECT * FROM users WHERE email = @email"
# parameters = { "@email": userEmail }

---

# 12. REQUEST SIZE LIMITS
# Prevent resource exhaustion

POST /api/documents HTTP/1.1
Content-Length: 1073741824  # 1 GB file

HTTP/1.1 413 Payload Too Large
{
  "error": "PAYLOAD_TOO_LARGE",
  "message": "Request body exceeds maximum size of 10 MB",
  "maxSizeBytes": 10485760,
  "receivedBytes": 1073741824
}

---

# 13. DEPENDENCY SCANNING
# Regular security audits of dependencies

npm audit      # Node.js
pip check      # Python  
dotnet list package --vulnerable  # .NET

# Update vulnerable packages regularly
# Use tools like Snyk, Dependabot, WhiteSource
```

**Security Checklist:**
* [ ] Enforce HTTPS (TLS 1.2+)
* [ ] Implement strong authentication (JWT/OAuth)
* [ ] Apply authorization checks on all endpoints
* [ ] Validate and sanitize all inputs
* [ ] Implement rate limiting
* [ ] Configure CORS properly
* [ ] Add security headers
* [ ] Never expose sensitive data
* [ ] Use generic error messages
* [ ] Implement logging and monitoring
* [ ] Regular security audits and pen testing
* [ ] Keep dependencies updated
* [ ] Use secrets management (not hardcoded)
* [ ] Implement request size limits
* [ ] Enable audit logging for sensitive operations

---

### 29. What is API Monitoring and Observability?

**API Monitoring and Observability** involves tracking API health, performance, usage patterns, and errors through metrics, logs, and traces. Monitoring answers "what's broken?" while observability answers "why is it broken?"

**Why it's needed:** Enables proactive issue detection, performance optimization, capacity planning, security threat detection, and understanding user behavior. Without monitoring, issues go unnoticed until users complain.

**When to use:** Implement monitoring from day one for all APIs - production, staging, and development. Critical for SLA compliance, incident response, and continuous improvement.

**Key Observability Pillars:**

| Pillar | Description | Tools | Use Case |
|--------|-------------|-------|----------|
| **Metrics** | Quantitative measurements over time | Prometheus, Datadog, CloudWatch | Request rate, latency, error rate |
| **Logs** | Discrete event records | ELK, Splunk, Loki | Error investigation, audit trail |
| **Traces** | Request flow across services | Jaeger, Zipkin, APM | Distributed system debugging |
| **Alerts** | Automated notifications | PagerDuty, Opsgenie | Incident response |

**Real-time Example:**

```http
# 1. METRICS COLLECTION
# Each API request generates metrics

GET /api/orders/ORD-123 HTTP/1.1
Authorization: Bearer token123

# Metrics collected:
# - http_requests_total{method="GET", endpoint="/api/orders", status="200"} +1
# - http_request_duration_seconds{endpoint="/api/orders"} 0.145
# - http_request_size_bytes 256
# - http_response_size_bytes 1024

HTTP/1.1 200 OK
X-Response-Time: 145ms
X-Request-ID: req-abc123

---

# 2. KEY METRICS TO TRACK

# Availability Metrics:
- Uptime percentage (Target: 99.9% = 43 minutes downtime/month)
- Health check status

# Performance Metrics:
- Response time (p50, p95, p99 percentiles)
  p50: 50ms   # 50% requests faster than this
  p95: 250ms  # 95% requests faster than this
  p99: 500ms  # 99% requests faster than this
- Throughput (requests per second)

# Error Metrics:
- Error rate (percentage of failed requests)
- Error types (4xx vs 5xx)
- Specific error codes (401, 403, 404, 500, 503)

# Business Metrics:
- Active users
- API calls per user
- Feature usage
- Revenue per API call

---

# 3. STRUCTURED LOGGING
# Every request logged with context

{
  "timestamp": "2026-01-23T10:30:00.145Z",
  "level": "INFO",
  "requestId": "req-abc123",
  "method": "GET",
  "path": "/api/orders/ORD-123",
  "statusCode": 200,
  "responseTime": 145,
  "userId": 456,
  "userAgent": "Mozilla/5.0...",
  "ip": "203.0.113.42",
  "apiVersion": "v1"
}

# Error logging with full context:
{
  "timestamp": "2026-01-23T10:31:00.234Z",
  "level": "ERROR",
  "requestId": "req-xyz789",
  "method": "POST",
  "path": "/api/orders",
  "statusCode": 500,
  "error": {
    "type": "DatabaseConnectionError",
    "message": "Unable to connect to database",
    "code": "DB_CONN_FAILED"
  },
  "userId": 789,
  "stackTrace": "...",
  "environment": "production",
  "serverInstance": "api-server-03"
}

---

# 4. DISTRIBUTED TRACING
# Track request across multiple services

# Client → API Gateway → Customer Service → Order Service → Payment Service

# Trace ID: trace-abc123 (spans entire request flow)

# Span 1 (API Gateway):
{
  "traceId": "trace-abc123",
  "spanId": "span-001",
  "service": "api-gateway",
  "operation": "process_request",
  "duration": 245ms,
  "tags": {
    "http.method": "POST",
    "http.url": "/api/orders",
    "http.status_code": 201
  }
}

# Span 2 (Customer Service):
{
  "traceId": "trace-abc123",
  "spanId": "span-002",
  "parentSpanId": "span-001",
  "service": "customer-service",
  "operation": "get_customer",
  "duration": 45ms,
  "tags": {
    "customer.id": "CUST-789",
    "cache.hit": true
  }
}

# Span 3 (Order Service):
{
  "traceId": "trace-abc123",
  "spanId": "span-003",
  "parentSpanId": "span-001",
  "service": "order-service",
  "operation": "create_order",
  "duration": 125ms,
  "tags": {
    "order.id": "ORD-5001",
    "db.query_time": 95ms
  }
}

# Span 4 (Payment Service):
{
  "traceId": "trace-abc123",
  "spanId": "span-004",
  "parentSpanId": "span-003",
  "service": "payment-service",
  "operation": "process_payment",
  "duration": 180ms,
  "tags": {
    "payment.status": "success",
    "external_api.latency": 150ms
  }
}

# Total request time: 245ms
# Breakdown visible: Gateway→Customer→Order→Payment

---

# 5. HEALTH CHECK ENDPOINT

GET /health HTTP/1.1

HTTP/1.1 200 OK
Content-Type: application/json

{
  "status": "healthy",
  "version": "1.2.5",
  "uptime": 864235,  # seconds
  "timestamp": "2026-01-23T10:30:00Z",
  "checks": {
    "database": {
      "status": "healthy",
      "responseTime": 12
    },
    "redis": {
      "status": "healthy",
      "responseTime": 5
    },
    "externalApi": {
      "status": "degraded",
      "responseTime": 2500,
      "message": "High latency detected"
    }
  }
}

# Degraded state:
HTTP/1.1 200 OK
{
  "status": "degraded",
  "checks": {
    "database": {"status": "healthy"},
    "redis": {"status": "unhealthy", "error": "Connection timeout"}
  }
}

# Unhealthy state:
HTTP/1.1 503 Service Unavailable
{
  "status": "unhealthy",
  "checks": {
    "database": {"status": "unhealthy", "error": "Cannot connect"}
  }
}

---

# 6. ALERTING RULES

# Alert 1: High error rate
ALERT HighErrorRate
IF (http_requests_total{status=~"5.."} / http_requests_total) > 0.05
FOR 5m
MESSAGE "Error rate above 5% for 5 minutes"
SEVERITY critical

# Alert 2: Slow response time
ALERT SlowResponseTime
IF http_request_duration_p95 > 1.0
FOR 10m
MESSAGE "95th percentile response time above 1s"
SEVERITY warning

# Alert 3: Low availability
ALERT ServiceDown
IF up{job="api-server"} == 0
FOR 2m
MESSAGE "API server instance is down"
SEVERITY critical

# Alert 4: Rate limit breaches
ALERT HighRateLimitHits
IF rate_limit_exceeded_total > 1000
FOR 15m
MESSAGE "High number of rate limit breaches"
SEVERITY warning

---

# 7. DASHBOARD METRICS

# Real-time API Dashboard displays:

## Availability

✓ Uptime: 99.95% (current month)
✓ Status: All systems operational

## Performance (Last hour)

- Requests: 1,245,678
- RPS (req/sec): 346
- Avg Response Time: 125ms
- p50: 85ms | p95: 250ms | p99: 450ms

## Errors (Last hour)

- Total Errors: 1,234 (0.99%)
- 4xx Errors: 1,100 (89%)
- 5xx Errors: 134 (11%)

## Top Endpoints (by traffic)

01. GET /api/products - 45% (156 RPS)
02. GET /api/customers - 25% (87 RPS)
03. POST /api/orders - 15% (52 RPS)

## Slowest Endpoints (p95)

01. POST /api/reports/generate - 3.2s
02. GET /api/analytics/dashboard - 1.8s
03. GET /api/orders?includeItems=true - 850ms

## Error Breakdown

- 401 Unauthorized: 600 (48.6%)
- 404 Not Found: 350 (28.4%)
- 500 Internal Server Error: 134 (10.9%)
- 403 Forbidden: 150 (12.2%)

---

# 8. CUSTOM INSTRUMENTATION
# Add business-specific metrics

POST /api/orders HTTP/1.1
{
  "customerId": "CUST-789",
  "items": [...],
  "total": 299.99
}

# Custom metrics emitted:
# - orders_created_total +1
# - order_value_total +299.99
# - order_items_count +3
# - customer_lifetime_orders{customer_id="CUST-789"} +1

HTTP/1.1 201 Created

---

# 9. LOG AGGREGATION QUERY EXAMPLES

# ELK/Splunk query: Find all 500 errors in last hour
statusCode:500 AND timestamp:[now-1h TO now]

# Find slow requests (>1s)
responseTime:>1000 AND timestamp:[now-24h TO now]

# Find authentication failures for specific user
statusCode:401 AND userId:456

# Find all errors for specific request chain
requestId:"req-abc123"

---

# 10. SLA MONITORING

# Service Level Agreement (SLA) targets:
- Availability: 99.9% uptime (Monthly)
- Latency: p95 < 500ms
- Error Rate: < 1%

# SLA Dashboard:
Current Month (January 2026)
✓ Availability: 99.95% (Target: 99.9%) ✓
✓ p95 Latency: 245ms (Target: <500ms) ✓
✗ Error Rate: 1.2% (Target: <1%) ✗

# Alert when SLA at risk:
Warning: Error rate trending toward SLA breach
Current: 0.95% | Target: <1% | Trend: ↗ increasing
```

**Monitoring Tools Stack:**
* **Metrics**: Prometheus, Datadog, New Relic, CloudWatch
* **Logging**: ELK Stack, Splunk, Loki
* **Tracing**: Jaeger, Zipkin, AWS X-Ray
* **APM**: New Relic, Datadog APM, Dynatrace
* **Alerting**: PagerDuty, Opsgenie, Slack
* **Dashboards**: Grafana, Kibana

---

### 30. What is API Deprecation and how do you manage it?

**API Deprecation** is the process of phasing out old API versions or endpoints while providing migration paths for clients. It involves announcing retirement, providing transition periods, and eventually removing deprecated functionality.

**Why it's needed:** APIs evolve - better designs emerge, security vulnerabilities are discovered, performance improvements require breaking changes. Proper deprecation maintains backward compatibility during transitions and respects client integration timelines.

**When to use:** Deprecate when introducing breaking changes, removing outdated features, consolidating redundant endpoints, or addressing security concerns. Plan deprecation before launching new versions.

**Deprecation Lifecycle:**

| Phase | Duration | Actions | Client Impact |
|-------|----------|---------|---------------|
| **Announcement** | T-6 months | Communicate plans, document changes | Awareness |
| **Deprecation** | 6 months | Mark as deprecated, warn in responses | Plan migration |
| **Sunset Warning** | T-3 months | Increase warning frequency | Active migration |
| **Sunset** | T-0 | Stop accepting requests | Must migrate |
| **Removal** | T+1 month | Remove code, endpoints | N/A |

**Real-time Example:**

```http
# PHASE 1: ANNOUNCEMENT (T-6 months)
# January 2026: Announce v1 deprecation

# Blog post, email, documentation update:
"API v1 will be deprecated on July 1, 2026 and 
 sunset on January 1, 2027. Please migrate to v2."

---

# PHASE 2: DEPRECATION NOTICE (T-6 to T-0)
# Add deprecation headers to responses

GET /api/v1/customers/123 HTTP/1.1
Authorization: Bearer token123

HTTP/1.1 200 OK
Deprecation: true
Sunset: Wed, 01 Jan 2027 00:00:00 GMT
Link: </api/v2/customers/123>; rel="successor-version"
Link: </docs/migration-guide>; rel="deprecation"
Warning: 299 - "API v1 is deprecated. Please migrate to v2 by Jan 1, 2027"
X-API-Version: 1.0
X-API-Deprecation-Date: 2026-07-01
X-API-Sunset-Date: 2027-01-01

{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com"
}

---

# PHASE 3: INCREASED WARNINGS (T-3 months)
# October 2026: More prominent warnings

GET /api/v1/customers/123 HTTP/1.1

HTTP/1.1 200 OK
Deprecation: true
Sunset: Wed, 01 Jan 2027 00:00:00 GMT
Warning: 299 - "URGENT: API v1 sunset in 3 months. Migrate to v2 immediately"
X-Days-Until-Sunset: 90

{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com",
  "_deprecation": {
    "message": "This API version will be sunset on Jan 1, 2027",
    "migrationGuide": "https://docs.api.company.com/migration-v1-to-v2",
    "newEndpoint": "/api/v2/customers/123",
    "supportContact": "api-support@company.com"
  }
}

---

# PHASE 4: SUNSET DATE (T-0)
# January 1, 2027: Stop accepting v1 requests

GET /api/v1/customers/123 HTTP/1.1

HTTP/1.1 410 Gone
Content-Type: application/json

{
  "error": "API_VERSION_SUNSET",
  "message": "API v1 has been sunset and is no longer available",
  "sunsetDate": "2027-01-01",
  "migrationGuide": "https://docs.api.company.com/migration-v1-to-v2",
  "newVersion": {
    "version": "v2",
    "endpoint": "/api/v2/customers/123",
    "documentation": "https://docs.api.company.com/v2"
  },
  "supportContact": "api-support@company.com"
}

---

# MIGRATION GUIDE DOCUMENT

## API v1 to v2 Migration Guide

### Overview

API v2 introduces improved response structures, better error handling,
and enhanced performance. All v1 clients must migrate by Jan 1, 2027.

### Breaking Changes

#### 1. Customer Resource Structure

**v1:**
{
  "id": "123",
  "name": "John Doe",
  "contact": "john@example.com"
}

**v2:**
{
  "customerId": "123",  # Changed: id → customerId
  "firstName": "John",  # Changed: name split into firstName/lastName
  "lastName": "Doe",
  "email": "john@example.com",  # Changed: contact → email
  "createdAt": "2026-01-15T10:00:00Z"  # Added
}

#### 2. Endpoint Changes

| v1 Endpoint | v2 Endpoint | Notes |
|-------------|-------------|-------|
| GET /api/v1/customers | GET /api/v2/customers | Pagination required |
| GET /api/v1/customers/{id} | GET /api/v2/customers/{customerId} | Parameter renamed |
| POST /api/v1/orders | POST /api/v2/customers/{customerId}/orders | Nested under customer |

#### 3. Error Response Format

**v1:**
{
  "error": "Customer not found"
}

**v2:**
{
  "error": {
    "code": "RESOURCE_NOT_FOUND",
    "message": "Customer not found",
    "details": "No customer exists with ID: 123",
    "timestamp": "2026-01-23T10:30:00Z",
    "requestId": "req-abc123"
  }
}

### Code Examples

**Python - Before (v1):**
import requests

response = requests.get(
    'https://api.company.com/api/v1/customers/123',
    headers={'Authorization': f'Bearer {token}'}
)
customer = response.json()
print(customer['name'])  # "John Doe"

**Python - After (v2):**
import requests

response = requests.get(
    'https://api.company.com/api/v2/customers/123',
    headers={'Authorization': f'Bearer {token}'}
)
customer = response.json()
print(f"{customer['firstName']} {customer['lastName']}")  # "John Doe"

### Migration Timeline

- Now - Jan 1, 2027: Dual support (v1 and v2 available)
- Jan 1, 2027: v1 sunset (returns 410 Gone)
- Feb 1, 2027: v1 code removed

### Support

- Migration questions: api-support@company.com
- Office hours: Wednesdays 2-4 PM PT
- Slack channel: #api-v2-migration

---

# FEATURE DEPRECATION (within same version)

# Deprecating specific endpoint feature

GET /api/v2/customers?includeAddresses=true HTTP/1.1
# The 'includeAddresses' parameter is deprecated

HTTP/1.1 200 OK
Deprecation: param="includeAddresses"; date="2026-07-01"
Warning: 299 - "Parameter 'includeAddresses' is deprecated. Use /api/v2/customers/{id}/addresses instead"

{
  "customers": [...],
  "_warnings": [
    {
      "type": "DeprecatedParameter",
      "parameter": "includeAddresses",
      "message": "Use dedicated endpoint: GET /api/v2/customers/{id}/addresses",
      "deprecationDate": "2026-07-01",
      "sunsetDate": "2027-01-01"
    }
  ]
}

---

# MONITORING DEPRECATED API USAGE

# Track deprecated endpoint usage
GET /api/v1/customers/123

# Server logs:
{
  "timestamp": "2026-12-15T10:30:00Z",
  "level": "WARNING",
  "message": "Deprecated API version accessed",
  "apiVersion": "v1",
  "endpoint": "/api/v1/customers/123",
  "clientId": "app-client-789",
  "userId": 456,
  "daysUntilSunset": 17,
  "clientUserAgent": "MyApp/1.0"
}

# Dashboard shows:
# - % of traffic still on v1: 5.2%
# - Top v1 clients: app-client-789 (45%), app-client-012 (30%)
# - Trend: Decreasing ↘ (from 15% last month)

# Email notifications to clients still using v1:
"Your application (Client ID: app-client-789) is still using 
 deprecated API v1. It will stop working in 17 days. 
 Please migrate to v2 immediately."

---

# BACKWARD COMPATIBILITY LAYER (temporary)

# v2 endpoint with compatibility mode
GET /api/v2/customers/123?compatibilityMode=v1 HTTP/1.1

HTTP/1.1 200 OK
X-Compatibility-Mode: v1
Warning: 299 - "Compatibility mode will be removed on Jan 1, 2027"

{
  "id": "123",  # v1 format
  "name": "John Doe",
  "contact": "john@example.com"
}

# This buys time for clients but should be temporary

---

# GRADUAL ROLLOUT

# Use feature flags to control rollout
GET /api/customers/123 HTTP/1.1
X-Feature-Flag: use-v2-format

# Server decides which format to return based on flag
# Allows A/B testing, gradual migration, quick rollback if issues
```

**Deprecation Best Practices:**
* Provide 6-12 month deprecation period
* Use standard HTTP headers (Deprecation, Sunset)
* Send email notifications to affected clients
* Maintain comprehensive migration documentation
* Offer backward compatibility layer temporarily
* Monitor deprecated API usage
* Provide sandbox environment for testing
* Have dedicated support during transition
* Use semantic versioning
* Log deprecated feature usage

**Communication Channels:**
* API documentation (prominent banner)
* Response headers (every request)
* Email to registered developers
* Blog posts and announcements
* Slack/Discord community channels
* Status page updates
* In-product notifications
* Changelog and release notes

---

## Batch 4: Advanced Topics & Performance (Questions 31-40)

---

### 31. What is the difference between Monolithic and Microservices API architecture?

**Monolithic architecture** builds the entire application as a single, unified codebase deployed as one unit, while **Microservices architecture** breaks the application into small, independent services that communicate via APIs. Each approach has distinct trade-offs for scalability, complexity, and development.

**Why it's needed:** As applications grow, monoliths become difficult to scale and maintain. Microservices enable independent scaling, technology diversity, and team autonomy, but introduce complexity in distributed systems, networking, and data consistency.

**When to use Monolithic:** Small to medium applications, MVPs, limited team size, simple domains, when operational simplicity is priority.

**When to use Microservices:** Large-scale applications, multiple teams, need for independent scaling, polyglot technology requirements, domain complexity.

| Aspect | Monolithic | Microservices |
|--------|------------|---------------|
| **Architecture** | Single codebase, one deployment unit | Multiple services, independent deployments |
| **Scaling** | Scale entire application | Scale individual services |
| **Technology** | Single tech stack | Polyglot (multiple languages/frameworks) |
| **Deployment** | Deploy all or nothing | Independent service deployment |
| **Development** | Simple initially, complex as grows | Complex initially, manageable at scale |
| **Data** | Single shared database | Database per service |
| **Communication** | In-process (method calls) | Network calls (REST, gRPC, messaging) |
| **Failure** | Entire app fails | Service-level failure isolation |
| **Team Structure** | Single team or small teams | Team per service |
| **Testing** | Easier (single unit) | Complex (integration testing required) |

**Real-time Example:**

```http
# MONOLITHIC ARCHITECTURE

# Single application handling everything:
# - Customer management
# - Order processing
# - Inventory management
# - Payment processing
# - Notifications

# All requests go to one application:
https://api.company.com

GET /api/customers/123
GET /api/orders/ORD-501
POST /api/payments
GET /api/inventory/products

# Pros:
# - Simple deployment (single artifact)
# - Easy local development
# - No network latency between components
# - Straightforward transactions (ACID)
# - Easier debugging

# Cons:
# - Must scale entire app even if only orders need scaling
# - Technology lock-in (entire app in one language)
# - Large codebase becomes hard to navigate
# - Longer deployment times
# - Higher risk deployments (everything deployed together)
# - Tight coupling between modules

---

# MICROSERVICES ARCHITECTURE

# Separate services:

# 1. Customer Service (Node.js + MongoDB)
https://customer-service.internal:8001
GET /api/customers/123
POST /api/customers

# 2. Order Service (Java Spring + PostgreSQL)
https://order-service.internal:8002
GET /api/orders/ORD-501
POST /api/orders

# 3. Inventory Service (Python + Redis + PostgreSQL)
https://inventory-service.internal:8003
GET /api/products/PROD-789
PATCH /api/products/PROD-789/stock

# 4. Payment Service (C# .NET + SQL Server)
https://payment-service.internal:8004
POST /api/payments
GET /api/payments/PAY-456

# 5. Notification Service (Go + RabbitMQ)
https://notification-service.internal:8005
POST /api/notifications/email
POST /api/notifications/sms

# API Gateway provides unified entry point:
https://api.company.com

---

# REQUEST FLOW COMPARISON

# MONOLITHIC: Create Order
POST https://api.company.com/api/orders
{
  "customerId": 123,
  "items": [{"productId": 789, "quantity": 2}]
}

# Internal flow (in-process method calls):
OrderController.CreateOrder()
  → CustomerService.ValidateCustomer(123)  # Method call
  → InventoryService.CheckStock(789, 2)    # Method call
  → InventoryService.ReserveStock(789, 2)  # Method call
  → PaymentService.ChargeCustomer(123)     # Method call
  → NotificationService.SendEmail(123)     # Method call
  → Database.CommitTransaction()           # Single ACID transaction

Response Time: ~150ms (no network overhead)

---

# MICROSERVICES: Create Order
POST https://api.company.com/api/orders
(API Gateway routes to Order Service)

# Order Service orchestrates via HTTP calls:
OrderService.CreateOrder()
  → HTTP GET https://customer-service/api/customers/123  # Network call - 20ms
  → HTTP GET https://inventory-service/api/products/789/stock  # Network call - 25ms
  → HTTP POST https://inventory-service/api/products/789/reserve  # Network call - 30ms
  → HTTP POST https://payment-service/api/payments  # Network call - 150ms
  → Message Queue → NotificationService  # Async - doesn't block
  → Order Service saves to own database

Response Time: ~250ms (network overhead + multiple calls)

# Challenges:
# - Distributed transactions (no ACID across services)
# - Network latency and failures
# - Data consistency (eventual consistency)
# - More complex error handling

---

# SCALING COMPARISON

# MONOLITHIC SCALING
# Black Friday: Orders spike 10x, but customers/products same
# Must scale ENTIRE application:
┌─────────────────────┐
│   Monolith x 10     │  Scale all features
│ - Customers         │  even though only
│ - Orders ← spike!   │  orders are busy
│ - Inventory         │
│ - Payments          │  Inefficient resource use
│ - Notifications     │
└─────────────────────┘

---

# MICROSERVICES SCALING
# Black Friday: Scale only what's needed
┌──────────────┐
│ Customer     │ x1 instance (no spike)
└──────────────┘
┌──────────────┐
│ Order        │ x10 instances (spike!)
│ Order        │
│ Order...     │
└──────────────┘
┌──────────────┐
│ Inventory    │ x3 instances (moderate spike)
│ Inventory    │
└──────────────┘
┌──────────────┐
│ Payment      │ x8 instances (spike!)
│ Payment...   │
└──────────────┘
┌──────────────┐
│ Notification │ x2 instances (async, buffered)
└──────────────┘

# Benefits:
# - Cost-effective scaling
# - Faster scaling (smaller services)
# - Independent scaling per service needs

---

# DEPLOYMENT COMPARISON

# MONOLITHIC DEPLOYMENT
# Small bug fix in notification module:
01. Fix code in Notification module
02. Run ALL tests (entire application)
03. Build entire application
04. Deploy entire application (downtime/rolling restart)
05. Risk: Any bug anywhere affects entire app

Deployment time: 30-60 minutes
Risk level: High (everything deployed)

---

# MICROSERVICES DEPLOYMENT
# Small bug fix in notification service:
01. Fix code in Notification Service only
02. Run tests for Notification Service
03. Build Notification Service only
04. Deploy Notification Service independently
05. Other services unaffected, keep running

Deployment time: 5-10 minutes
Risk level: Low (isolated to one service)

---

# TECHNOLOGY DIVERSITY

# MONOLITHIC
# Entire app must use one tech stack:
- Language: Java
- Framework: Spring Boot
- Database: PostgreSQL
# Stuck with these choices for entire application

---

# MICROSERVICES
# Each service can use optimal technology:
- Customer Service: Node.js + MongoDB (flexible schema)
- Order Service: Java Spring + PostgreSQL (transactions)
- Inventory Service: Python + Redis (caching) + PostgreSQL
- Payment Service: C# .NET + SQL Server (enterprise features)
- Notification Service: Go (high concurrency) + RabbitMQ
- Search Service: Elasticsearch (full-text search)

---

# DATA MANAGEMENT

# MONOLITHIC
# Single shared database:
┌────────────────────────────┐
│      Shared Database       │
│  - Customers Table         │
│  - Orders Table            │
│  - Products Table          │
│  - Payments Table          │
└────────────────────────────┘
        ↑
        │
┌────────────────┐
│   Monolith     │
└────────────────┘

# Pros: ACID transactions, referential integrity
# Cons: Tight coupling, scaling bottleneck

---

# MICROSERVICES
# Database per service:
┌──────────────┐     ┌──────────────┐
│ Customer     │────→│ Customer DB  │
│ Service      │     │ (MongoDB)    │
└──────────────┘     └──────────────┘

┌──────────────┐     ┌──────────────┐
│ Order        │────→│ Order DB     │
│ Service      │     │ (PostgreSQL) │
└──────────────┘     └──────────────┘

┌──────────────┐     ┌──────────────┐
│ Inventory    │────→│ Inventory DB │
│ Service      │     │ (PostgreSQL) │
└──────────────┘     └──────────────┘

# Pros: Independent scaling, technology choice, loose coupling
# Cons: No ACID transactions across services, data duplication, eventual consistency

---

# FAILURE HANDLING

# MONOLITHIC
# If any component fails, entire app fails:
NotificationService crashes → Entire app down

---

# MICROSERVICES
# Service-level failure isolation:
NotificationService crashes → Only notifications fail
# Orders, Payments, Inventory still working

# Circuit breaker pattern:
OrderService → calls PaymentService (timeout)
              ↓
        Circuit opens
              ↓
        Return fallback response
# Order Service continues operating with degraded functionality
```

**Migration Strategy (Monolith → Microservices):**
01. **Strangler Fig Pattern**: Gradually extract services
02. **Start with boundaries**: Identify bounded contexts
03. **Extract non-critical services first**: Lower risk
04. **API Gateway**: Facade over both monolith and microservices
05. **Data migration**: Gradual separation
06. **Don't over-decompose**: Find right service size

**Decision Matrix:**
* **Small team (< 10)**: Monolithic
* **Large team (> 20)**: Consider microservices
* **Simple domain**: Monolithic
* **Complex domain with clear boundaries**: Microservices
* **Rapid prototyping/MVP**: Monolithic
* **Scale requirements vary by feature**: Microservices

---

### 32. What is GraphQL and how does it compare to REST?

**GraphQL** is a query language and runtime for APIs that allows clients to request exactly the data they need in a single request. Unlike REST's fixed endpoint responses, GraphQL enables flexible, hierarchical queries and returns only requested fields.

**Why it's needed:** REST APIs often suffer from over-fetching (returning too much data) or under-fetching (requiring multiple requests). GraphQL solves this by letting clients specify exact requirements, reducing bandwidth and improving performance, especially for mobile apps.

**When to use REST:** Simple CRUD operations, caching-heavy scenarios, public APIs requiring HTTP caching, when clients have predictable data needs.

**When to use GraphQL:** Complex data relationships, mobile apps (minimize requests), rapid UI iteration, when clients have varying data needs, aggregating multiple data sources.

| Aspect | REST | GraphQL |
|--------|------|----------|
| **Data Fetching** | Fixed endpoints return predefined data | Client specifies exactly what data needed |
| **Requests** | Multiple endpoints for related data | Single endpoint for all queries |
| **Over-fetching** | Common (returns more than needed) | No over-fetching (only requested fields) |
| **Under-fetching** | Common (multiple requests needed) | No under-fetching (get all in one request) |
| **Versioning** | URL versioning (v1, v2) | Schema evolution, no versions |
| **Caching** | HTTP caching (standard) | Complex (requires custom implementation) |
| **Learning Curve** | Simple, widely understood | Steeper learning curve |
| **Tooling** | Mature, widespread | Growing, excellent dev tools |
| **Error Handling** | HTTP status codes | Always 200, errors in response body |

**Real-time Example:**

```http
# REST API - Multiple Requests Problem

# Scenario: Display customer profile with orders and products

# Request 1: Get customer
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
{
  "customerId": 123,
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "phone": "+1-555-0123",
  "address": {...},  # Don't need this
  "preferences": {...},  # Don't need this
  "createdAt": "2025-01-15T10:00:00Z",  # Don't need this
  "updatedAt": "2026-01-20T14:30:00Z"   # Don't need this
}
# Problem: Over-fetching (got unnecessary data)

---

# Request 2: Get customer's orders
GET /api/customers/123/orders HTTP/1.1

HTTP/1.1 200 OK
{
  "orders": [
    {
      "orderId": "ORD-501",
      "total": 299.99,
      "status": "shipped",
      "items": [
        {"productId": "PROD-789", "quantity": 2}
      ]
    }
  ]
}
# Problem: Under-fetching (need product details, another request needed)

---

# Request 3: Get product details
GET /api/products/PROD-789 HTTP/1.1

HTTP/1.1 200 OK
{
  "productId": "PROD-789",
  "name": "Wireless Mouse",
  "price": 29.99,
  "description": "...",  # Don't need this
  "specifications": {...},  # Don't need this
  "reviews": [...]  # Don't need this
}

# Summary: 3 separate HTTP requests
# Total time: ~300ms (3 x 100ms)
# Bandwidth: Over-fetched unnecessary data

---

# GRAPHQL - Single Request Solution

POST /graphql HTTP/1.1
Content-Type: application/json

{
  "query": "{ 
    customer(id: 123) {
      firstName
      lastName
      email
      orders {
        orderId
        total
        status
        items {
          quantity
          product {
            productId
            name
            price
          }
        }
      }
    }
  }"
}

HTTP/1.1 200 OK
{
  "data": {
    "customer": {
      "firstName": "John",
      "lastName": "Doe",
      "email": "john@example.com",
      "orders": [
        {
          "orderId": "ORD-501",
          "total": 299.99,
          "status": "shipped",
          "items": [
            {
              "quantity": 2,
              "product": {
                "productId": "PROD-789",
                "name": "Wireless Mouse",
                "price": 29.99
              }
            }
          ]
        }
      ]
    }
  }
}

# Benefits:
# ✓ Single request
# ✓ Exactly the fields needed (no over-fetching)
# ✓ All related data in one response (no under-fetching)
# ✓ Faster: ~150ms (one request)
# ✓ Less bandwidth

---

# GRAPHQL QUERY EXAMPLES

# 1. Simple Query - Get specific fields
POST /graphql HTTP/1.1

{
  "query": "{
    products(category: \"electronics\") {
      productId
      name
      price
    }
  }"
}

Response:
{
  "data": {
    "products": [
      {"productId": "PROD-789", "name": "Wireless Mouse", "price": 29.99},
      {"productId": "PROD-790", "name": "Keyboard", "price": 79.99}
    ]
  }
}

---

# 2. Query with Variables
POST /graphql HTTP/1.1

{
  "query": "query GetCustomer($customerId: ID!) {
    customer(id: $customerId) {
      firstName
      email
      orders {
        orderId
        total
      }
    }
  }",
  "variables": {
    "customerId": "123"
  }
}

---

# 3. Mutation - Modify Data (like POST/PUT in REST)
POST /graphql HTTP/1.1

{
  "query": "mutation CreateOrder($input: OrderInput!) {
    createOrder(input: $input) {
      orderId
      total
      status
    }
  }",
  "variables": {
    "input": {
      "customerId": "123",
      "items": [
        {"productId": "PROD-789", "quantity": 2}
      ]
    }
  }
}

Response:
{
  "data": {
    "createOrder": {
      "orderId": "ORD-502",
      "total": 59.98,
      "status": "pending"
    }
  }
}

---

# 4. Fragments - Reusable Query Parts
{
  "query": "{
    customer(id: 123) {
      ...CustomerDetails
      orders {
        orderId
        items {
          product {
            ...ProductDetails
          }
        }
      }
    }
  }
  
  fragment CustomerDetails on Customer {
    customerId
    firstName
    lastName
    email
  }
  
  fragment ProductDetails on Product {
    productId
    name
    price
  }"
}

---

# 5. Aliases - Multiple Queries of Same Type
{
  "query": "{
    electronics: products(category: \"electronics\") {
      name
      price
    }
    books: products(category: \"books\") {
      name
      price
    }
  }"
}

Response:
{
  "data": {
    "electronics": [
      {"name": "Mouse", "price": 29.99}
    ],
    "books": [
      {"name": "GraphQL Guide", "price": 39.99}
    ]
  }
}

---

# ERROR HANDLING COMPARISON

# REST - HTTP Status Codes
GET /api/customers/99999 HTTP/1.1

HTTP/1.1 404 Not Found
{
  "error": "Customer not found"
}

---

# GRAPHQL - Always 200, Errors in Response
POST /graphql HTTP/1.1

{
  "query": "{
    customer(id: 99999) {
      customerId
      name
    }
  }"
}

HTTP/1.1 200 OK
{
  "data": {
    "customer": null
  },
  "errors": [
    {
      "message": "Customer not found",
      "locations": [{"line": 2, "column": 3}],
      "path": ["customer"],
      "extensions": {
        "code": "RESOURCE_NOT_FOUND",
        "customerId": "99999"
      }
    }
  ]
}

# Note: Always returns 200 OK, errors in 'errors' array

---

# GRAPHQL SCHEMA DEFINITION

type Customer {
  customerId: ID!
  firstName: String!
  lastName: String!
  email: String!
  phone: String
  orders: [Order!]!
  createdAt: DateTime!
}

type Order {
  orderId: ID!
  customerId: ID!
  total: Float!
  status: OrderStatus!
  items: [OrderItem!]!
  createdAt: DateTime!
}

type OrderItem {
  product: Product!
  quantity: Int!
  price: Float!
}

type Product {
  productId: ID!
  name: String!
  description: String
  price: Float!
  category: String!
  stock: Int!
}

enum OrderStatus {
  PENDING
  PAID
  SHIPPED
  DELIVERED
  CANCELLED
}

# Queries (Read operations)
type Query {
  customer(id: ID!): Customer
  customers(limit: Int, offset: Int): [Customer!]!
  product(id: ID!): Product
  products(category: String, limit: Int): [Product!]!
  order(id: ID!): Order
}

# Mutations (Write operations)
type Mutation {
  createCustomer(input: CustomerInput!): Customer!
  updateCustomer(id: ID!, input: CustomerInput!): Customer!
  deleteCustomer(id: ID!): Boolean!
  
  createOrder(input: OrderInput!): Order!
  cancelOrder(id: ID!): Order!
}

# Subscriptions (Real-time updates)
type Subscription {
  orderStatusChanged(orderId: ID!): Order!
}

---

# GRAPHQL ADVANTAGES

# 1. Flexible Queries - Client controls data shape
# Mobile app (limited bandwidth):
{
  products { productId, name, price }  # Minimal data
}

# Web app (rich UI):
{
  products { 
    productId, name, price, description, 
    images, reviews, specifications 
  }  # Full data
}
# Same endpoint, different data - no new API version needed

---

# 2. Strong Typing - Schema as contract
# IDE autocomplete, validation, documentation generation
# Type errors caught at development time

---

# 3. Introspection - Self-documenting
POST /graphql HTTP/1.1

{
  "query": "{
    __schema {
      types {
        name
        fields {
          name
          type {
            name
          }
        }
      }
    }
  }"
}
# Returns entire API schema - enables tools like GraphiQL, Apollo Studio

---

# REST ADVANTAGES

# 1. HTTP Caching - Built-in
GET /api/products/PROD-789
Cache-Control: max-age=3600
ETag: "abc123"
# Browser/CDN caching works automatically

# GraphQL: POST requests, harder to cache
# Requires custom caching (Apollo Client, Relay)

---

# 2. Simpler Architecture
# REST: Standard HTTP methods, well-understood patterns
# GraphQL: Requires GraphQL server, resolvers, schema

---

# 3. Better for Simple CRUD
# REST: Straightforward endpoints
# GraphQL: Overkill for simple operations

---

# HYBRID APPROACH - REST + GraphQL

# Use REST for:
- Simple CRUD operations
- Public APIs requiring HTTP caching
- File uploads/downloads
- Health checks, webhooks

# Use GraphQL for:
- Complex data aggregation
- Mobile apps
- Dashboard/reporting UIs
- When clients have varying needs
```

**Popular GraphQL Tools:**
* **Apollo Server/Client**: Most popular GraphQL implementation
* **GraphiQL**: Interactive GraphQL IDE
* **Relay**: Facebook's GraphQL client
* **Hasura**: Instant GraphQL APIs over PostgreSQL
* **AWS AppSync**: Managed GraphQL service
* **GraphQL Code Generator**: Generate types from schema

**When to Choose:**
* **Choose REST**: Public APIs, simple CRUD, RESTful conventions match domain, caching critical
* **Choose GraphQL**: Complex data relationships, mobile apps, rapid iteration, varying client needs, real-time subscriptions

---

### 33. What is API Gateway pattern and what problems does it solve?

**API Gateway pattern** is a single entry point for all client requests that sits between clients and backend microservices. It handles routing, composition, protocol translation, authentication, rate limiting, and other cross-cutting concerns, abstracting backend complexity.

**Why it's needed:** In microservices, clients would otherwise need to know multiple service endpoints, handle different protocols, manage multiple authentication mechanisms, and deal with chattiness (multiple network calls). API Gateway centralizes these concerns.

**When to use:** Essential in microservices architectures, mobile backends (reduce round-trips), when backend services use different protocols, requiring centralized security/monitoring, or managing multiple API versions.

**Problems API Gateway Solves:**

| Problem | Without Gateway | With Gateway |
|---------|----------------|--------------|
| **Multiple Endpoints** | Client knows all service URLs | Single unified endpoint |
| **Protocol Translation** | Client handles REST/gRPC/SOAP | Gateway translates protocols |
| **Authentication** | Implement in each service | Centralized auth validation |
| **Rate Limiting** | Per-service limits | Unified rate limiting |
| **Request Aggregation** | Multiple client requests | Single request, gateway aggregates |
| **Monitoring** | Monitor each service separately | Centralized logging/monitoring |
| **Versioning** | Version each service | Unified versioning strategy |

**Real-time Example:**

```http
# PROBLEM 1: MULTIPLE SERVICE CALLS (Chatty Communication)

# WITHOUT API GATEWAY
# Mobile app needs customer dashboard data

# Call 1: Get customer info
GET https://customer-svc.internal:8001/customers/123
Time: 50ms

# Call 2: Get recent orders
GET https://order-svc.internal:8002/customers/123/orders?recent=true
Time: 80ms

# Call 3: Get payment methods
GET https://payment-svc.internal:8003/customers/123/payment-methods
Time: 40ms

# Call 4: Get loyalty points
GET https://loyalty-svc.internal:8004/customers/123/points
Time: 30ms

# Total: 4 HTTP requests, ~200ms + 4x network overhead
# Mobile data usage: 4 separate connections
# Complexity: Client must know all service endpoints

---

# WITH API GATEWAY - Request Aggregation

GET https://api.company.com/api/customers/123/dashboard HTTP/1.1
Authorization: Bearer token123

# API Gateway:
# 1. Validates JWT token
# 2. Calls all 4 services in parallel
# 3. Aggregates responses
# 4. Returns unified response

HTTP/1.1 200 OK
{
  "customer": {
    "customerId": 123,
    "name": "John Doe",
    "email": "john@example.com"
  },
  "recentOrders": [
    {"orderId": "ORD-501", "total": 299.99, "status": "shipped"}
  ],
  "paymentMethods": [
    {"id": "PM-789", "type": "credit_card", "last4": "4242"}
  ],
  "loyaltyPoints": {
    "balance": 2500,
    "tier": "gold"
  }
}

# Total: 1 HTTP request, ~100ms (parallel execution)
# Benefits: Fewer round-trips, better mobile UX, simpler client code

---

# PROBLEM 2: PROTOCOL TRANSLATION

# Backend services use different protocols:
# - Customer Service: REST JSON
# - Inventory Service: gRPC
# - Legacy System: SOAP XML
# - Messaging: WebSocket

# WITHOUT GATEWAY
# Client must implement support for all protocols

# REST call
POST https://customer-svc/api/customers
Content-Type: application/json

# gRPC call (binary protocol)
inventory_service_stub.CheckStock(product_id=789)

# SOAP call
POST https://legacy-svc/soap/orders
Content-Type: text/xml
<?xml version="1.0"?>
<soap:Envelope>...</soap:Envelope>

# Client complexity: HIGH

---

# WITH API GATEWAY
# Client uses only REST, Gateway translates

POST https://api.company.com/api/orders HTTP/1.1
Content-Type: application/json
{
  "customerId": 123,
  "items": [{"productId": 789, "quantity": 2}]
}

# API Gateway translates:
# 1. REST → REST (Customer Service)
# 2. REST → gRPC (Inventory Service)
# 3. REST → SOAP (Legacy Order System)

HTTP/1.1 201 Created
{
  "orderId": "ORD-502",
  "status": "created"
}

# Client complexity: LOW (only knows REST)

---

# PROBLEM 3: AUTHENTICATION & AUTHORIZATION

# WITHOUT GATEWAY
# Each service validates auth independently

# Service 1: Validates JWT
GET https://customer-svc/api/customers/123
Authorization: Bearer eyJhbGc...
# Customer Service: Parse JWT, validate signature, check expiration

# Service 2: Validates JWT
GET https://order-svc/api/orders
Authorization: Bearer eyJhbGc...
# Order Service: Parse JWT, validate signature, check expiration

# Service 3: Validates JWT
GET https://payment-svc/api/payments
Authorization: Bearer eyJhbGc...
# Payment Service: Parse JWT, validate signature, check expiration

# Problems:
# - Duplicate auth logic in every service
# - Inconsistent auth implementation
# - Higher latency (each service validates)
# - Security risk (one service misconfigured = breach)

---

# WITH API GATEWAY
# Centralized authentication

GET https://api.company.com/api/orders HTTP/1.1
Authorization: Bearer eyJhbGc...

# API Gateway:
# 1. Validates JWT once (signature, expiration, issuer)
# 2. Extracts user info (userId, roles, permissions)
# 3. Adds headers for downstream services:
#    X-User-ID: 123
#    X-User-Roles: customer,premium
# 4. Forwards to backend services

# Backend services trust Gateway headers (internal network)
# No need for JWT validation in each service

---

# PROBLEM 4: RATE LIMITING

# WITHOUT GATEWAY
# Implement rate limiting in each service

GET https://customer-svc/api/customers/123
# Customer Service: Check rate limit for user 123

GET https://order-svc/api/orders
# Order Service: Check rate limit for user 123 (separate counter)

GET https://product-svc/api/products
# Product Service: Check rate limit for user 123 (separate counter)

# Problems:
# - Inconsistent limits across services
# - Complex to manage (change limit → update all services)
# - User can exceed total limit across services

---

# WITH API GATEWAY
# Unified rate limiting

GET https://api.company.com/api/customers/123 HTTP/1.1
Authorization: Bearer token_user123

# API Gateway tracks: user_123 → 847/1000 requests this hour
HTTP/1.1 200 OK
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 847

# All requests (regardless of backend service) count toward same limit
# Consistent enforcement across entire API

---

# PROBLEM 5: API VERSIONING

# WITHOUT GATEWAY
# Version each service independently

GET https://customer-svc-v1/api/customers/123  # v1
GET https://customer-svc-v2/api/customers/123  # v2
GET https://order-svc-v1/api/orders            # v1
GET https://payment-svc-v2/api/payments        # v2

# Problems:
# - Client tracks multiple service versions
# - Complex compatibility matrix
# - Difficult to deprecate old versions

---

# WITH API GATEWAY
# Unified API versioning

GET https://api.company.com/v1/customers/123
GET https://api.company.com/v2/customers/123

# Gateway routes based on version:
# /v1/* → Routes to v1 services
# /v2/* → Routes to v2 services or translates requests

# Simplified client experience
# Gateway can even translate v1 requests to v2 backend

---

# PROBLEM 6: LOAD BALANCING & SERVICE DISCOVERY

# Service instances:
# - customer-service: 3 instances (ports 8001, 8002, 8003)
# - order-service: 5 instances (ports 9001-9005)

# WITHOUT GATEWAY
# Client must implement:
# - Service discovery (find available instances)
# - Load balancing logic
# - Health checking
# - Retry logic

# WITH API GATEWAY
GET https://api.company.com/api/customers/123

# Gateway:
# 1. Discovers healthy customer-service instances
# 2. Load balances (round-robin, least connections)
# 3. Routes to: customer-service-instance-2:8002
# 4. If fails, retries with customer-service-instance-3:8003

---

# PROBLEM 7: MONITORING & LOGGING

# WITHOUT GATEWAY
# Monitor each service separately

# Customer Service logs:
[INFO] GET /customers/123 - 200 OK - 45ms

# Order Service logs:
[INFO] GET /orders - 200 OK - 80ms

# Payment Service logs:
[INFO] POST /payments - 201 Created - 120ms

# Problems:
# - Distributed logs across services
# - Difficult to trace requests across services
# - No unified dashboard

---

# WITH API GATEWAY
# Centralized monitoring

GET https://api.company.com/api/customers/123/dashboard

# Gateway logs with request ID:
{
  "requestId": "req-abc123",
  "timestamp": "2026-01-23T10:30:00Z",
  "method": "GET",
  "path": "/api/customers/123/dashboard",
  "statusCode": 200,
  "responseTime": 145ms,
  "userId": 456,
  "serviceCalls": [
    {"service": "customer-service", "time": 45ms, "status": 200},
    {"service": "order-service", "time": 80ms, "status": 200},
    {"service": "payment-service", "time": 40ms, "status": 200},
    {"service": "loyalty-service", "time": 30ms, "status": 200}
  ]
}

# Unified dashboard shows:
# - Total requests: 1.2M/day
# - Avg response time: 125ms
# - Error rate: 0.8%
# - Top endpoints by traffic
# - Slowest backend services

---

# API GATEWAY ROUTING RULES

# Route configuration (conceptual):

# Public customer endpoints
GET /api/customers/*           → customer-service
POST /api/customers            → customer-service

# Order management
GET /api/orders/*              → order-service
POST /api/orders               → order-service

# Product catalog
GET /api/products/*            → product-service

# Search (different protocol)
GET /api/search                → search-service (Elasticsearch)

# Admin endpoints (requires admin role)
/api/admin/*                   → admin-service (+ role check)

# Legacy system (SOAP)
/api/legacy/*                  → legacy-soap-service (+ protocol translation)

# Aggregated endpoints
GET /api/customers/*/dashboard → Aggregate multiple services

---

# CIRCUIT BREAKER PATTERN IN GATEWAY

# Payment service is slow/failing

GET https://api.company.com/api/orders HTTP/1.1

# Gateway detects payment-service failures:
# - 5 failures in 10 seconds
# - Circuit OPENS

# Instead of calling failing service:
HTTP/1.1 200 OK
{
  "orderId": "ORD-503",
  "status": "pending",
  "paymentStatus": "unavailable",
  "_notice": "Payment service temporarily unavailable"
}

# Benefits:
# - Prevents cascading failures
# - Fast-fail instead of timeouts
# - Graceful degradation
# - After cooldown, circuit tries again (half-open state)

---

# API GATEWAY IMPLEMENTATION EXAMPLES

# Configuration (Kong Gateway):
routes:
  - name: customer-route
    paths:
      - /api/customers
    methods:
      - GET
      - POST
    service: customer-service
    plugins:
      - name: jwt
      - name: rate-limiting
        config:
          minute: 1000
      - name: correlation-id
      - name: prometheus

# Configuration (AWS API Gateway):
{
  "paths": {
    "/api/customers": {
      "get": {
        "x-amazon-apigateway-integration": {
          "type": "http",
          "uri": "http://customer-service:8001/api/customers",
          "httpMethod": "GET"
        }
      }
    }
  }
}

# Configuration (Ocelot .NET):
{
  "Routes": [
    {
      "UpstreamPathTemplate": "/api/customers/{id}",
      "UpstreamHttpMethod": [ "Get" ],
      "DownstreamPathTemplate": "/customers/{id}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        { "Host": "customer-service", "Port": 8001 }
      ],
      "AuthenticationOptions": {
        "AuthenticationProviderKey": "Bearer"
      },
      "RateLimitOptions": {
        "EnableRateLimiting": true,
        "Period": "1h",
        "Limit": 1000
      }
    }
  ]
}
```

**API Gateway Responsibilities:**
* ✓ Request Routing & Composition
* ✓ Protocol Translation (REST ↔ gRPC ↔ SOAP)
* ✓ Authentication & Authorization
* ✓ Rate Limiting & Throttling
* ✓ Load Balancing
* ✓ Caching
* ✓ Request/Response Transformation
* ✓ Circuit Breaking
* ✓ Monitoring & Logging
* ✓ API Versioning
* ✓ CORS handling
* ✓ SSL/TLS Termination

**Popular API Gateway Solutions:**
* Kong, AWS API Gateway, Azure API Management
* NGINX, Apigee, Tyk, Ocelot (. NET), Spring Cloud Gateway

**API Gateway Considerations:**
* **Single Point of Failure**: Make gateway highly available
* **Performance Bottleneck**: Optimize, scale horizontally
* **Added Latency**: Keep gateway logic lightweight
* **Complexity**: Don't add too much business logic

---

### 34. What is idempotency and why is it critical in distributed systems?

**Idempotency** means an operation produces the same result whether executed once or multiple times. In distributed systems, network failures, timeouts, and retries are common - idempotency ensures repeated requests don't cause unintended side effects like duplicate charges or double orders.

**Why it's needed:** Networks are unreliable. When a request times out, clients don't know if it was processed. Retrying non-idempotent operations causes duplicates. Idempotency makes systems resilient to retries, enabling reliable distributed communication.

**When to use:** Critical for payment processing, order creation, any state-changing operation in distributed systems, webhook deliveries, message queue consumers, and any operation that might be retried automatically.

**HTTP Methods Idempotency:**

| Method | Idempotent? | Reason |
|--------|-------------|---------|
| **GET** | Yes | Reading doesn't change state |
| **PUT** | Yes | Replacing with same data = same result |
| **DELETE** | Yes | Deleting deleted resource = still deleted |
| **HEAD** | Yes | Like GET, only headers |
| **OPTIONS** | Yes | Querying capabilities doesn't change state |
| **POST** | No | Creating multiple times = multiple resources |
| **PATCH** | Depends | Implementation-specific |

**Real-time Example:**

```http
# PROBLEM: NON-IDEMPOTENT OPERATION

# Client creates order
POST /api/orders HTTP/1.1
Content-Type: application/json
Authorization: Bearer token123

{
  "customerId": 456,
  "items": [{"productId": 789, "quantity": 1, "price": 99.99}],
  "total": 99.99
}

# Network timeout after 30 seconds
# Client doesn't know if order was created
# Retries the request...

POST /api/orders HTTP/1.1
(same request body)

# RESULT: TWO ORDERS CREATED!
# Order 1: ORD-5001, total: $99.99
# Order 2: ORD-5002, total: $99.99
# Customer charged twice!

---

# SOLUTION 1: IDEMPOTENCY KEYS

# Client generates unique idempotency key (UUID)
POST /api/orders HTTP/1.1
Content-Type: application/json
Authorization: Bearer token123
Idempotency-Key: 550e8400-e29b-41d4-a716-446655440000

{
  "customerId": 456,
  "items": [{"productId": 789, "quantity": 1, "price": 99.99}],
  "total": 99.99
}

# Server stores: idempotency_key → order_id mapping
# {
#   "550e8400-e29b-41d4-a716-446655440000": "ORD-5001"
# }

HTTP/1.1 201 Created
{
  "orderId": "ORD-5001",
  "status": "created",
  "total": 99.99
}

---

# Network timeout, client retries with SAME idempotency key
POST /api/orders HTTP/1.1
Idempotency-Key: 550e8400-e29b-41d4-a716-446655440000
(same body)

# Server checks: idempotency key exists?
# YES → Return original response (from cache/database)

HTTP/1.1 200 OK
X-Idempotency-Replay: true

{
  "orderId": "ORD-5001",  # Same order ID
  "status": "created",
  "total": 99.99
}

# RESULT: Only ONE order created
# Safe to retry multiple times

---

# IDEMPOTENCY KEY IMPLEMENTATION (Server-side)

# Pseudocode:
function createOrder(request, idempotencyKey):
    # Check if request with this key already processed
    existingResult = cache.get(idempotencyKey)
    
    if existingResult:
        # Return cached result
        return response(200, existingResult, headers={
            "X-Idempotency-Replay": "true"
        })
    
    # First time seeing this key, process request
    try:
        order = createOrderInDatabase(request)
        result = {
            "orderId": order.id,
            "status": "created",
            "total": order.total
        }
        
        # Cache result for 24 hours
        cache.set(idempotencyKey, result, ttl=86400)
        
        return response(201, result)
        
    except Exception as e:
        # Don't cache failures
        raise e

---

# SOLUTION 2: NATURAL IDEMPOTENCY (PUT)

# PUT is naturally idempotent - use client-generated IDs

# Client generates order ID
PUT /api/orders/ORD-CLIENT-123 HTTP/1.1
Content-Type: application/json

{
  "orderId": "ORD-CLIENT-123",
  "customerId": 456,
  "items": [{"productId": 789, "quantity": 1}],
  "total": 99.99
}

# First request:
HTTP/1.1 201 Created
{
  "orderId": "ORD-CLIENT-123",
  "status": "created"
}

---

# Retry with same order ID
PUT /api/orders/ORD-CLIENT-123 HTTP/1.1
(same body)

# Server logic:
# if (order with this ID exists and same data):
#     return 200 OK (idempotent)
# else:
#     return error (different data for same ID)

HTTP/1.1 200 OK
{
  "orderId": "ORD-CLIENT-123",
  "status": "created"  # Same result
}

# Only ONE order created, safe to retry

---

# PAYMENT PROCESSING EXAMPLE

# PROBLEM: Duplicate payment charges

# Payment attempt 1
POST /api/payments HTTP/1.1
{
  "orderId": "ORD-5001",
  "amount": 299.99,
  "cardToken": "tok_abc123"
}

# Timeout after payment processed but before response received
# Client retries...

POST /api/payments HTTP/1.1
(same body)

# RESULT: Customer charged TWICE ($299.99 x 2 = $599.98)

---

# SOLUTION: Idempotency keys for payments

POST /api/payments HTTP/1.1
Idempotency-Key: pay-550e8400-e29b-41d4-a716-446655440000
{
  "orderId": "ORD-5001",
  "amount": 299.99,
  "cardToken": "tok_abc123"
}

# First request: Processes payment
HTTP/1.1 201 Created
{
  "paymentId": "PAY-789",
  "status": "succeeded",
  "amount": 299.99
}

---

# Retry with same idempotency key
POST /api/payments HTTP/1.1
Idempotency-Key: pay-550e8400-e29b-41d4-a716-446655440000
(same body)

# Server: Idempotency key seen before
# Return original payment result, no new charge

HTTP/1.1 200 OK
X-Idempotency-Replay: true
{
  "paymentId": "PAY-789",  # Same payment
  "status": "succeeded",
  "amount": 299.99
}

# RESULT: Only ONE charge ($299.99)

---

# DELETE IDEMPOTENCY

# DELETE is naturally idempotent

DELETE /api/orders/ORD-5001 HTTP/1.1

# First request:
HTTP/1.1 204 No Content
# Order deleted

---

# Retry (order already deleted)
DELETE /api/orders/ORD-5001 HTTP/1.1

# Options:
# Option 1: Return 204 (idempotent - same result)
HTTP/1.1 204 No Content

# Option 2: Return 404 (more accurate but still idempotent)
HTTP/1.1 404 Not Found
{
  "message": "Order already deleted or never existed"
}

# Both responses are acceptable
# Key point: Multiple DELETEs don't cause different side effects

---

# PATCH IDEMPOTENCY - DEPENDS ON IMPLEMENTATION

# NON-IDEMPOTENT PATCH (Increment operation)
PATCH /api/products/PROD-789 HTTP/1.1
{
  "operation": "increment",
  "field": "stock",
  "value": 10
}

# First request: stock = 100, after: stock = 110
# Retry: stock = 110, after: stock = 120  ← DIFFERENT RESULT!
# Not idempotent!

---

# IDEMPOTENT PATCH (Set operation)
PATCH /api/products/PROD-789 HTTP/1.1
{
  "stock": 110  # Set to absolute value
}

# First request: stock = 100, after: stock = 110
# Retry: stock = 110, after: stock = 110  ← SAME RESULT
# Idempotent!

---

# WEBHOOK IDEMPOTENCY

# Webhook delivery with retries

# Delivery attempt 1
POST /client/webhooks/order-status HTTP/1.1
X-Webhook-ID: wh-abc123
X-Webhook-Event: order.shipped
X-Webhook-Delivery-Attempt: 1

{
  "eventType": "order.shipped",
  "orderId": "ORD-5001",
  "timestamp": "2026-01-23T10:30:00Z"
}

# Client receives but response times out
# Server retries...

---

# Delivery attempt 2 (retry)
POST /client/webhooks/order-status HTTP/1.1
X-Webhook-ID: wh-abc123  # Same webhook ID
X-Webhook-Event: order.shipped
X-Webhook-Delivery-Attempt: 2

(same body)

# Client implementation (idempotent):
function handleWebhook(webhookId, data):
    # Check if already processed
    if (processedWebhooks.contains(webhookId)):
        return 200 OK  # Already processed, acknowledge
    
    # Process webhook
    updateOrderStatus(data.orderId, "shipped")
    sendShippingEmail(data.orderId)
    
    # Mark as processed
    processedWebhooks.add(webhookId)
    
    return 200 OK

# Result: Email sent once, status updated once (idempotent)

---

# MESSAGE QUEUE IDEMPOTENCY

# Consumer processes messages from queue

# Message 1 (processing order)
{
  "messageId": "msg-550e8400",
  "orderId": "ORD-5001",
  "action": "charge_customer",
  "amount": 299.99
}

# Consumer processes, charges customer
# Consumer crashes before acknowledging message
# Message redelivered...

---

# Message 1 (redelivered)
{
  "messageId": "msg-550e8400",  # Same message ID
  "orderId": "ORD-5001",
  "action": "charge_customer",
  "amount": 299.99
}

# Idempotent consumer implementation:
function processMessage(messageId, data):
    # Check if message already processed
    if (processedMessages.contains(messageId)):
        return # Skip, already processed
    
    # Process message
    chargeCustomer(data.orderId, data.amount)
    
    # Mark as processed
    processedMessages.add(messageId)
    
    # Acknowledge message
    queue.ack(messageId)

# Result: Customer charged once (idempotent)

---

# IDEMPOTENCY KEY BEST PRACTICES

# 1. Client-generated UUIDs
Idempotency-Key: 550e8400-e29b-41d4-a716-446655440000

# 2. Store keys with expiration (24 hours typical)
cache.set(key, result, ttl=86400)

# 3. Return same status code on replay
# First: 201 Created
# Replay: 200 OK (with X-Idempotency-Replay header)

# 4. Handle concurrent requests with same key
# Use database unique constraint or distributed lock

# 5. Different keys for different operations
# Order creation: order-{uuid}
# Payment: payment-{uuid}
# Refund: refund-{uuid}

# 6. Validate key format
if (!isValidUUID(idempotencyKey)):
    return 400 Bad Request

# 7. Don't cache failures
# Only cache successful responses

# 8. Include key in logs
log.info("Processing order with idempotency key: " + key)
```

**Idempotency Benefits:**
* ✓ Safe retries (no duplicate operations)
* ✓ Resilient to network failures
* ✓ Enables automatic retry logic
* ✓ Prevents duplicate charges/orders
* ✓ Simplifies client error handling
* ✓ Essential for webhooks and message queues

**Implementation Strategies:**
01. **Idempotency Keys** (POST operations)
02. **Natural Idempotency** (PUT, DELETE)
03. **Client-Generated IDs** (PUT with resource ID)
04. **Deduplication** (track processed message IDs)
05. **Database Constraints** (unique constraints)

---

### 35. What are HTTP response codes 2xx, 3xx, 4xx, and 5xx, and when to use each?

**HTTP status codes** are three-digit numbers indicating the result of an HTTP request. They're grouped into five classes (1xx-5xx), each representing a different response category. Proper status codes enable clients to programmatically handle different scenarios.

**Why it's needed:** Status codes provide standardized communication between client and server. They enable proper error handling, caching behavior, retry logic, and user feedback without parsing response bodies.

**When to use:** Always return appropriate status codes. They're fundamental to REST API contracts and enable HTTP-compliant caching, client libraries, and middleware to function correctly.

**Status Code Categories:**

| Category | Range | Meaning | Client Action |
|----------|-------|---------|---------------|
| **Informational** | 100-199 | Request received, continuing | Continue sending |
| **Success** | 200-299 | Request successful | Process response |
| **Redirection** | 300-399 | Further action needed | Follow redirect |
| **Client Error** | 400-499 | Client-side error | Fix request |
| **Server Error** | 500-599 | Server-side error | Retry or report |

**Real-time Example:**

```http
# ========================================
# 2xx SUCCESS CODES
# ========================================

# 200 OK - Standard success response
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
Content-Type: application/json

{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com"
}

# Use: GET requests, successful PUT/PATCH updates

---

# 201 Created - Resource successfully created
POST /api/customers HTTP/1.1
Content-Type: application/json

{
  "name": "Jane Smith",
  "email": "jane@example.com"
}

HTTP/1.1 201 Created
Location: /api/customers/456
Content-Type: application/json

{
  "customerId": 456,
  "name": "Jane Smith",
  "email": "jane@example.com",
  "createdAt": "2026-01-23T10:30:00Z"
}

# Use: POST requests creating new resources
# Must include Location header with new resource URL

---

# 202 Accepted - Request accepted, processing async
POST /api/reports/generate HTTP/1.1

{
  "reportType": "annual-sales",
  "year": 2025
}

HTTP/1.1 202 Accepted
Content-Type: application/json

{
  "jobId": "JOB-789",
  "status": "pending",
  "statusUrl": "/api/reports/jobs/JOB-789",
  "estimatedCompletionTime": "2026-01-23T10:35:00Z"
}

# Use: Long-running operations, async processing, batch jobs

---

# 204 No Content - Success but no response body
DELETE /api/customers/123 HTTP/1.1

HTTP/1.1 204 No Content
# No response body

# Use: DELETE requests, operations where no data needs returning

---

# 206 Partial Content - Range request fulfilled
GET /api/files/large-video.mp4 HTTP/1.1
Range: bytes=0-1023

HTTP/1.1 206 Partial Content
Content-Range: bytes 0-1023/5242880
Content-Length: 1024

(first 1024 bytes of video)

# Use: Range requests, resumable downloads, video streaming

---

# ========================================
# 3xx REDIRECTION CODES
# ========================================

# 301 Moved Permanently - Resource permanently moved
GET /api/v1/customers HTTP/1.1

HTTP/1.1 301 Moved Permanently
Location: /api/v2/customers

# Browser/client should update bookmarks
# Future requests go to new location

# Use: API endpoint permanently relocated, URL restructuring

---

# 302 Found - Temporary redirect
GET /api/reports/latest HTTP/1.1

HTTP/1.1 302 Found
Location: /api/reports/2026-01

# Temporary redirect, may change
# Client should continue using original URL

# Use: Temporary redirects, dynamic resource locations

---

# 303 See Other - Result available at different URI
POST /api/orders HTTP/1.1

HTTP/1.1 303 See Other
Location: /api/orders/ORD-5001

# Response to POST, redirect to GET the created resource

# Use: After POST/PUT/DELETE, redirect to resource location

---

# 304 Not Modified - Cached version still valid
GET /api/products/PROD-789 HTTP/1.1
If-None-Match: "etag-abc123"

HTTP/1.1 304 Not Modified
ETag: "etag-abc123"
# No body sent, use cached version

# Use: Conditional requests, bandwidth optimization

---

# 307 Temporary Redirect - Method must not change
POST /api/orders HTTP/1.1
(request body)

HTTP/1.1 307 Temporary Redirect
Location: https://new-server.com/api/orders

# Client must repeat POST to new location with same body

# Use: Load balancing, maintenance mode redirects

---

# ========================================
# 4xx CLIENT ERROR CODES
# ========================================

# 400 Bad Request - Malformed or invalid request
POST /api/customers HTTP/1.1
Content-Type: application/json

{
  "name": "",  # Empty name
  "email": "invalid-email",  # Invalid format
  "age": -5  # Invalid value
}

HTTP/1.1 400 Bad Request
Content-Type: application/json

{
  "error": "VALIDATION_ERROR",
  "message": "Request validation failed",
  "details": [
    {"field": "name", "message": "Name cannot be empty"},
    {"field": "email", "message": "Invalid email format"},
    {"field": "age", "message": "Age must be positive"}
  ]
}

# Use: Validation errors, malformed JSON, missing required fields

---

# 401 Unauthorized - Authentication required or failed
GET /api/orders HTTP/1.1
# No Authorization header

HTTP/1.1 401 Unauthorized
WWW-Authenticate: Bearer realm="api"

{
  "error": "UNAUTHORIZED",
  "message": "Authentication token required"
}

---

# Token expired
GET /api/orders HTTP/1.1
Authorization: Bearer expired_token

HTTP/1.1 401 Unauthorized
WWW-Authenticate: Bearer error="invalid_token"

{
  "error": "TOKEN_EXPIRED",
  "message": "Authentication token has expired. Please login again."
}

# Use: Missing/invalid/expired credentials, failed authentication

---

# 403 Forbidden - Authenticated but not authorized
DELETE /api/users/456 HTTP/1.1
Authorization: Bearer valid_employee_token

HTTP/1.1 403 Forbidden

{
  "error": "FORBIDDEN",
  "message": "Insufficient permissions. Admin role required.",
  "requiredRole": "Admin",
  "userRole": "Employee"
}

# Use: Permission denied, insufficient role/privileges

---

# 404 Not Found - Resource doesn't exist
GET /api/customers/99999 HTTP/1.1

HTTP/1.1 404 Not Found

{
  "error": "RESOURCE_NOT_FOUND",
  "message": "Customer with ID 99999 not found",
  "resourceType": "Customer",
  "resourceId": "99999"
}

# Use: Resource doesn't exist, invalid ID, wrong URL

---

# 405 Method Not Allowed - HTTP method not supported
DELETE /api/products/PROD-789 HTTP/1.1

HTTP/1.1 405 Method Not Allowed
Allow: GET, PUT, PATCH

{
  "error": "METHOD_NOT_ALLOWED",
  "message": "DELETE method not allowed for this resource",
  "allowedMethods": ["GET", "PUT", "PATCH"]
}

# Use: Unsupported HTTP method for endpoint

---

# 406 Not Acceptable - Cannot produce requested format
GET /api/customers/123 HTTP/1.1
Accept: application/pdf

HTTP/1.1 406 Not Acceptable

{
  "error": "NOT_ACCEPTABLE",
  "message": "Cannot produce response in requested format",
  "requestedFormat": "application/pdf",
  "supportedFormats": ["application/json", "application/xml"]
}

# Use: Content negotiation failure, unsupported format

---

# 408 Request Timeout - Client took too long to send request
POST /api/large-upload HTTP/1.1
# Client sends headers but body slowly...

HTTP/1.1 408 Request Timeout

{
  "error": "REQUEST_TIMEOUT",
  "message": "Client did not send complete request within time limit"
}

# Use: Client connection timeout, slow uploads

---

# 409 Conflict - Request conflicts with current state
POST /api/customers HTTP/1.1

{
  "email": "existing@example.com"
}

HTTP/1.1 409 Conflict

{
  "error": "DUPLICATE_RESOURCE",
  "message": "Customer with this email already exists",
  "conflictingField": "email",
  "existingResourceId": "CUST-789"
}

# Use: Duplicate creation, concurrent modification conflicts

---

# 410 Gone - Resource permanently deleted
GET /api/products/OLD-PROD HTTP/1.1

HTTP/1.1 410 Gone

{
  "error": "RESOURCE_GONE",
  "message": "This product has been permanently discontinued",
  "discontinuedDate": "2025-12-01"
}

# Use: Permanently deleted/discontinued resources

---

# 413 Payload Too Large - Request body too big
POST /api/documents HTTP/1.1
Content-Length: 1073741824  # 1 GB

HTTP/1.1 413 Payload Too Large

{
  "error": "PAYLOAD_TOO_LARGE",
  "message": "Request body exceeds maximum size",
  "maxSizeBytes": 10485760,  # 10 MB
  "receivedBytes": 1073741824
}

# Use: File upload too large, request body size limit exceeded

---

# 415 Unsupported Media Type - Wrong Content-Type
POST /api/customers HTTP/1.1
Content-Type: text/xml

<customer>...</customer>

HTTP/1.1 415 Unsupported Media Type

{
  "error": "UNSUPPORTED_MEDIA_TYPE",
  "message": "Content-Type not supported",
  "receivedType": "text/xml",
  "supportedTypes": ["application/json"]
}

# Use: Wrong Content-Type header, unsupported format

---

# 422 Unprocessable Entity - Semantic error (valid syntax, invalid logic)
POST /api/orders/ORD-501/ship HTTP/1.1

HTTP/1.1 422 Unprocessable Entity

{
  "error": "INVALID_STATE_TRANSITION",
  "message": "Cannot ship order in current state",
  "currentState": "pending",
  "requiredState": "paid",
  "orderId": "ORD-501"
}

# Use: Business rule violation, invalid state transition

---

# 429 Too Many Requests - Rate limit exceeded
GET /api/products HTTP/1.1

HTTP/1.1 429 Too Many Requests
Retry-After: 300
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 0
X-RateLimit-Reset: 1706015400

{
  "error": "RATE_LIMIT_EXCEEDED",
  "message": "API rate limit exceeded",
  "limit": 1000,
  "retryAfter": 300
}

# Use: Rate limiting, throttling, quota exceeded

---

# ========================================
# 5xx SERVER ERROR CODES
# ========================================

# 500 Internal Server Error - Unexpected server error
GET /api/customers HTTP/1.1

HTTP/1.1 500 Internal Server Error

{
  "error": "INTERNAL_SERVER_ERROR",
  "message": "An unexpected error occurred. Please try again later.",
  "requestId": "req-abc123",
  "timestamp": "2026-01-23T10:30:00Z"
}

# Use: Unhandled exceptions, database errors, unexpected failures
# Note: Don't expose stack traces or internal details in production

---

# 501 Not Implemented - Feature not implemented
PATCH /api/customers/123 HTTP/1.1

HTTP/1.1 501 Not Implemented

{
  "error": "NOT_IMPLEMENTED",
  "message": "PATCH method not yet implemented for this resource"
}

# Use: Planned but unimplemented features

---

# 502 Bad Gateway - Invalid response from upstream server
GET /api/orders HTTP/1.1

# API Gateway calls Order Service, which returns invalid response

HTTP/1.1 502 Bad Gateway

{
  "error": "BAD_GATEWAY",
  "message": "Received invalid response from upstream service",
  "upstreamService": "order-service"
}

# Use: Gateway/proxy received invalid response from backend

---

# 503 Service Unavailable - Service temporarily unavailable
GET /api/products HTTP/1.1

HTTP/1.1 503 Service Unavailable
Retry-After: 120

{
  "error": "SERVICE_UNAVAILABLE",
  "message": "Service temporarily unavailable due to maintenance",
  "retryAfter": 120,
  "estimatedRestoreTime": "2026-01-23T11:00:00Z"
}

# Use: Maintenance mode, overload, temporary downtime

---

# 504 Gateway Timeout - Upstream server didn't respond in time
GET /api/reports/complex HTTP/1.1

# API Gateway waits for Report Service, times out after 30s

HTTP/1.1 504 Gateway Timeout

{
  "error": "GATEWAY_TIMEOUT",
  "message": "Upstream service did not respond within timeout period",
  "timeout": 30000,
  "upstreamService": "report-service"
}

# Use: Upstream service timeout, slow backend response

---

# STATUS CODE DECISION TREE

# Success?
#   └─ Yes → 2xx
#       ├─ Created resource? → 201 Created
#       ├─ Accepted for processing? → 202 Accepted
#       ├─ No content to return? → 204 No Content
#       └─ Standard success → 200 OK
#
#   └─ No → Error
#       ├─ Client mistake? → 4xx
#       │   ├─ Bad request format? → 400 Bad Request
#       │   ├─ Not authenticated? → 401 Unauthorized
#       │   ├─ Not authorized? → 403 Forbidden
#       │   ├─ Resource not found? → 404 Not Found
#       │   ├─ Conflict? → 409 Conflict
#       │   ├─ Validation error? → 422 Unprocessable Entity
#       │   └─ Rate limited? → 429 Too Many Requests
#       │
#       └─ Server problem? → 5xx
#           ├─ Unexpected error? → 500 Internal Server Error
#           ├─ Service down? → 503 Service Unavailable
#           └─ Upstream timeout? → 504 Gateway Timeout
```

**Common Mistakes to Avoid:**
* ❌ Using 200 for errors with error message in body
* ❌ Using 500 for validation errors (use 400)
* ❌ Using 401 for permission denied (use 403)
* ❌ Not including Location header with 201
* ❌ Not including Retry-After with 429/503
* ❌ Exposing stack traces in 500 responses
* ❌ Using 404 when resource is forbidden (leaks information)

**Best Practices:**
* ✓ Use appropriate status codes consistently
* ✓ Include helpful error messages
* ✓ Add relevant headers (Location, Retry-After, WWW-Authenticate)
* ✓ Document expected status codes per endpoint
* ✓ Log detailed errors server-side (don't expose to client)
* ✓ Use 4xx for client errors, 5xx for server errors
* ✓ Implement proper error response structure

---

## Batch 5: Real-World Scenarios & Best Practices (Questions 36-50)

---

### 36. How do you handle file uploads in REST APIs?

**File upload handling** in REST APIs involves accepting binary data (images, documents, videos) through HTTP requests, validating files, storing them securely, and returning references. It requires special considerations for size limits, validation, security, and progress tracking.

**Why it's needed:** Modern applications frequently need file uploads - profile pictures, document uploads, data imports, media sharing. Proper implementation prevents security vulnerabilities, manages resources efficiently, and provides good user experience.

**When to use:** Implement file uploads for user avatars, document management systems, data import/export, media platforms, backup systems, and any feature requiring file submission.

**File Upload Approaches:**

| Approach | Method | Use Case | Pros | Cons |
|----------|--------|----------|------|------|
| **Multipart/Form-Data** | POST with multipart | Single/multiple files with metadata | Standard, widely supported | Larger request size |
| **Base64 Encoded** | POST with JSON | Small files, simple APIs | Simple JSON structure | 33% size increase |
| **Binary Upload** | PUT/POST binary | Large files, streaming | Efficient, resumable | Less metadata |
| **Presigned URLs** | Upload direct to storage | Cloud storage (S3) | Offloads server, scalable | More complex flow |
| **Chunked Upload** | Multiple requests | Large files (>100MB) | Resumable, progress tracking | Complex implementation |

**Real-time Example:**

```http
# ========================================
# METHOD 1: MULTIPART/FORM-DATA (Most Common)
# ========================================

# Upload profile picture with metadata
POST /api/users/123/profile-picture HTTP/1.1
Content-Type: multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW
Authorization: Bearer token123

------WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Disposition: form-data; name="file"; filename="profile.jpg"
Content-Type: image/jpeg

(binary image data)
------WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Disposition: form-data; name="description"

My new profile picture
------WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Disposition: form-data; name="makePublic"

true
------WebKitFormBoundary7MA4YWxkTrZu0gW--

HTTP/1.1 201 Created
Content-Type: application/json

{
  "fileId": "FILE-789",
  "url": "https://cdn.company.com/uploads/users/123/profile.jpg",
  "fileName": "profile.jpg",
  "fileSize": 245760,
  "mimeType": "image/jpeg",
  "uploadedAt": "2026-01-23T10:30:00Z"
}

---

# Multiple files upload
POST /api/documents/upload HTTP/1.1
Content-Type: multipart/form-data; boundary=----Boundary123

------Boundary123
Content-Disposition: form-data; name="files"; filename="doc1.pdf"
Content-Type: application/pdf

(binary PDF data)
------Boundary123
Content-Disposition: form-data; name="files"; filename="doc2.pdf"
Content-Type: application/pdf

(binary PDF data)
------Boundary123--

HTTP/1.1 201 Created
{
  "uploadedFiles": [
    {
      "fileId": "DOC-101",
      "fileName": "doc1.pdf",
      "url": "https://cdn.company.com/documents/DOC-101.pdf"
    },
    {
      "fileId": "DOC-102",
      "fileName": "doc2.pdf",
      "url": "https://cdn.company.com/documents/DOC-102.pdf"
    }
  ]
}

---

# ========================================
# METHOD 2: BASE64 ENCODED (Small Files)
# ========================================

POST /api/users/123/avatar HTTP/1.1
Content-Type: application/json
Authorization: Bearer token123

{
  "fileName": "avatar.png",
  "mimeType": "image/png",
  "fileData": "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+M9QDwADhgGAWjR9awAAAABJRU5ErkJggg==",
  "description": "User avatar"
}

HTTP/1.1 201 Created
{
  "avatarUrl": "https://cdn.company.com/avatars/123.png",
  "fileSize": 182
}

# Note: Base64 increases size by ~33%
# Best for small files (<1MB) where JSON simplicity is preferred

---

# ========================================
# METHOD 3: DIRECT BINARY UPLOAD
# ========================================

# Upload file as raw binary
PUT /api/files/FILE-789/content HTTP/1.1
Content-Type: application/pdf
Content-Length: 2457600
Authorization: Bearer token123

(raw PDF binary data)

HTTP/1.1 200 OK
{
  "fileId": "FILE-789",
  "uploaded": true,
  "size": 2457600
}

# Useful for: Simple file storage, streaming, resumable uploads

---

# ========================================
# METHOD 4: PRESIGNED URL (S3/Cloud Storage)
# ========================================

# Step 1: Request presigned URL from API
POST /api/uploads/initiate HTTP/1.1
Content-Type: application/json
Authorization: Bearer token123

{
  "fileName": "report.pdf",
  "fileSize": 5242880,
  "mimeType": "application/pdf"
}

HTTP/1.1 200 OK
{
  "uploadId": "UPL-abc123",
  "presignedUrl": "https://s3.amazonaws.com/bucket/uploads/report.pdf?AWSAccessKeyId=...&Expires=1706013000&Signature=...",
  "expiresAt": "2026-01-23T11:00:00Z",
  "maxFileSize": 10485760
}

---

# Step 2: Client uploads directly to S3
PUT https://s3.amazonaws.com/bucket/uploads/report.pdf?AWSAccessKeyId=...
Content-Type: application/pdf
Content-Length: 5242880

(file binary data)

HTTP/1.1 200 OK
# Upload complete to S3

---

# Step 3: Notify API that upload complete
POST /api/uploads/UPL-abc123/complete HTTP/1.1
Authorization: Bearer token123

HTTP/1.1 200 OK
{
  "fileId": "FILE-456",
  "url": "https://cdn.company.com/files/FILE-456.pdf",
  "status": "completed"
}

# Benefits:
# ✓ Server doesn't handle file data (reduced load)
# ✓ Direct upload to CDN/storage
# ✓ Scalable for large files
# ✓ Faster uploads (direct to storage)

---

# ========================================
# METHOD 5: CHUNKED UPLOAD (Large Files)
# ========================================

# For files >100MB, split into chunks

# Step 1: Initiate chunked upload
POST /api/uploads/chunked/initiate HTTP/1.1
Content-Type: application/json

{
  "fileName": "large-video.mp4",
  "fileSize": 524288000,  # 500 MB
  "chunkSize": 5242880,   # 5 MB per chunk
  "mimeType": "video/mp4"
}

HTTP/1.1 200 OK
{
  "uploadId": "CHUNK-xyz789",
  "totalChunks": 100,
  "chunkSize": 5242880,
  "uploadUrls": [
    "https://api.company.com/api/uploads/CHUNK-xyz789/chunks/1",
    "https://api.company.com/api/uploads/CHUNK-xyz789/chunks/2",
    // ... up to 100
  ]
}

---

# Step 2: Upload chunks sequentially or in parallel
PUT /api/uploads/CHUNK-xyz789/chunks/1 HTTP/1.1
Content-Type: application/octet-stream
Content-Length: 5242880

(chunk 1 binary data - bytes 0 to 5242879)

HTTP/1.1 200 OK
{
  "chunkNumber": 1,
  "status": "uploaded"
}

---

PUT /api/uploads/CHUNK-xyz789/chunks/2 HTTP/1.1
(chunk 2 binary data - bytes 5242880 to 10485759)

HTTP/1.1 200 OK
{
  "chunkNumber": 2,
  "status": "uploaded"
}

# ... continue for all 100 chunks

---

# Step 3: Finalize upload (server assembles chunks)
POST /api/uploads/CHUNK-xyz789/finalize HTTP/1.1

HTTP/1.1 202 Accepted
{
  "uploadId": "CHUNK-xyz789",
  "status": "processing",
  "message": "Assembling chunks...",
  "statusUrl": "/api/uploads/CHUNK-xyz789/status"
}

---

# Check status
GET /api/uploads/CHUNK-xyz789/status HTTP/1.1

HTTP/1.1 200 OK
{
  "uploadId": "CHUNK-xyz789",
  "status": "completed",
  "fileId": "FILE-999",
  "url": "https://cdn.company.com/videos/FILE-999.mp4",
  "totalSize": 524288000
}

# Benefits:
# ✓ Resumable (if chunk fails, retry only that chunk)
# ✓ Progress tracking
# ✓ Works with large files
# ✓ Can upload chunks in parallel

---

# Resume after failure
GET /api/uploads/CHUNK-xyz789/status HTTP/1.1

HTTP/1.1 200 OK
{
  "uploadId": "CHUNK-xyz789",
  "status": "partial",
  "completedChunks": [1, 2, 3, 5, 6],
  "missingChunks": [4, 7, 8, ...],  # Resume from chunk 4
  "progress": 6
}

# Client resumes uploading missing chunks

---

# ========================================
# FILE VALIDATION
# ========================================

# Server-side validation checks

POST /api/uploads HTTP/1.1
Content-Type: multipart/form-data
(file: executable.exe)

# Validation failures:

# 1. File size exceeded
HTTP/1.1 413 Payload Too Large
{
  "error": "FILE_TOO_LARGE",
  "message": "File size exceeds maximum allowed",
  "maxSize": 10485760,  # 10 MB
  "receivedSize": 52428800  # 50 MB
}

---

# 2. Invalid file type
POST /api/users/123/avatar HTTP/1.1
(file: document.pdf)

HTTP/1.1 400 Bad Request
{
  "error": "INVALID_FILE_TYPE",
  "message": "Invalid file type for avatar",
  "receivedType": "application/pdf",
  "allowedTypes": ["image/jpeg", "image/png", "image/gif"]
}

---

# 3. Malicious file detected
POST /api/documents HTTP/1.1
(file: virus.exe)

HTTP/1.1 400 Bad Request
{
  "error": "SECURITY_THREAT_DETECTED",
  "message": "File contains potential security threat",
  "reason": "Executable file detected"
}

---

# 4. Invalid file content (magic number check)
POST /api/images HTTP/1.1
(file: fake-image.jpg - actually a .exe renamed)

HTTP/1.1 400 Bad Request
{
  "error": "FILE_CONTENT_MISMATCH",
  "message": "File content doesn't match declared type",
  "declaredType": "image/jpeg",
  "actualType": "application/x-msdownload"
}

# Server checks file magic numbers/signatures:
# JPEG: FF D8 FF
# PNG: 89 50 4E 47
# PDF: 25 50 44 46

---

# ========================================
# PROGRESS TRACKING
# ========================================

# Client-side upload with progress
POST /api/uploads HTTP/1.1
Content-Type: multipart/form-data
Content-Length: 52428800

# Client JavaScript example:
const formData = new FormData();
formData.append('file', fileInput.files[0]);

const xhr = new XMLHttpRequest();
xhr.upload.addEventListener('progress', (e) => {
  if (e.lengthComputable) {
    const percentComplete = (e.loaded / e.total) * 100;
    console.log(`Upload progress: ${percentComplete}%`);
    // Update UI progress bar
  }
});

xhr.open('POST', '/api/uploads');
xhr.send(formData);

---

# Server-side progress tracking (chunked uploads)
GET /api/uploads/CHUNK-xyz789/progress HTTP/1.1

HTTP/1.1 200 OK
{
  "uploadId": "CHUNK-xyz789",
  "totalChunks": 100,
  "uploadedChunks": 45,
  "progress": 45.0,  # percentage
  "uploadedBytes": 236029440,
  "totalBytes": 524288000,
  "estimatedTimeRemaining": 180  # seconds
}

---

# ========================================
# SECURITY BEST PRACTICES
# ========================================

# 1. Authentication required
POST /api/uploads HTTP/1.1
# No Authorization header

HTTP/1.1 401 Unauthorized
{
  "error": "UNAUTHORIZED",
  "message": "Authentication required for file uploads"
}

---

# 2. File size limits (prevent DoS)
# Enforce at multiple levels:
# - Web server (nginx/Apache): client_max_body_size 10M;
# - Application middleware
# - Storage service limits

---

# 3. Virus/malware scanning
POST /api/documents HTTP/1.1
(file upload)

# Server workflow:
# 1. Receive file
# 2. Store in quarantine
# 3. Run virus scan (ClamAV, VirusTotal API)
# 4. If clean: Move to permanent storage
# 5. If infected: Delete and notify user

HTTP/1.1 201 Created
{
  "fileId": "DOC-123",
  "status": "scanning",
  "message": "File uploaded. Virus scan in progress.",
  "statusUrl": "/api/documents/DOC-123/status"
}

---

# 4. Sanitize filenames
# User uploads: ../../../../etc/passwd
# Server sanitizes to: etc_passwd

# Remove: ../, ..\, absolute paths
# Allow: alphanumeric, dash, underscore, period

---

# 5. Content-Type validation
# Don't trust client-provided Content-Type
# Verify using file magic numbers

---

# 6. Store files outside web root
# ✓ Store: /var/uploads/files/
# ✗ Store: /var/www/html/uploads/
# Prevents direct execution of uploaded scripts

---

# 7. Generate unique filenames
# Original: profile.jpg
# Stored as: 550e8400-e29b-41d4-a716-446655440000.jpg
# Prevents overwrites, adds obfuscation

---

# 8. Serve files through API (not direct links)
GET /api/files/FILE-789/download HTTP/1.1
Authorization: Bearer token123

# Check permissions before serving
# Serve with proper headers:
HTTP/1.1 200 OK
Content-Type: application/pdf
Content-Disposition: attachment; filename="document.pdf"
X-Content-Type-Options: nosniff

(file data)
```

**File Upload Best Practices:**
* ✓ Implement file size limits
* ✓ Validate file types (whitelist, not blacklist)
* ✓ Check file content (magic numbers), don't trust extensions
* ✓ Run virus/malware scans
* ✓ Sanitize filenames
* ✓ Use unique filenames (UUIDs)
* ✓ Store files outside web root
* ✓ Implement authentication and authorization
* ✓ Use chunked uploads for large files
* ✓ Provide progress tracking
* ✓ Set request timeouts
* ✓ Use presigned URLs for direct-to-storage uploads
* ✓ Implement rate limiting on upload endpoints

**Storage Options:**
* **Local Filesystem**: Simple, but not scalable
* **AWS S3**: Scalable, CDN integration, versioning
* **Azure Blob Storage**: Microsoft ecosystem
* **Google Cloud Storage**: Google Cloud integration
* **CDN**: CloudFront, CloudFlare for fast delivery

---

### 37. How do you handle API testing and what types of tests should you implement?

**API Testing** validates that APIs function correctly, handle errors properly, perform well under load, and maintain security. It includes unit tests, integration tests, contract tests, performance tests, and security tests to ensure reliability and quality.

**Why it's needed:** APIs are contracts between services. Bugs in APIs affect all consumers. Comprehensive testing prevents regressions, ensures reliability, validates business logic, catches security vulnerabilities, and maintains performance standards.

**When to use:** Implement testing from day one. Write tests for all endpoints, error scenarios, edge cases, and integrations. Automate tests in CI/CD pipelines. Test before every deployment.

**API Testing Types:**

| Test Type | Purpose | Tools | When to Run |
|-----------|---------|-------|-------------|
| **Unit Tests** | Test individual functions/methods | xUnit, JUnit, pytest | On every commit |
| **Integration Tests** | Test API endpoints end-to-end | Postman, RestAssured | Pre-deployment |
| **Contract Tests** | Validate API contracts | Pact, Spring Cloud Contract | On API changes |
| **Performance Tests** | Test load, stress, scalability | JMeter, k6, Gatling | Before release |
| **Security Tests** | Find vulnerabilities | OWASP ZAP, Burp Suite | Regular scans |
| **Smoke Tests** | Quick health check | Custom scripts | Post-deployment |

**Real-time Example:**

```http
# ========================================
# 1. UNIT TESTS (Test Business Logic)
# ========================================

# Test customer service functions in isolation

// C# Unit Test Example (xUnit)
[Fact]
public async Task CreateCustomer_WithValidData_ReturnsCreatedCustomer()
{
    // Arrange
    var customerDto = new CreateCustomerDto
    {
        Name = "John Doe",
        Email = "john@example.com",
        Phone = "+1-555-0123"
    };
    var mockRepository = new Mock<ICustomerRepository>();
    var service = new CustomerService(mockRepository.Object);
    
    // Act
    var result = await service.CreateCustomerAsync(customerDto);
    
    // Assert
    Assert.NotNull(result);
    Assert.Equal("John Doe", result.Name);
    Assert.Equal("john@example.com", result.Email);
    mockRepository.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
}

[Fact]
public async Task CreateCustomer_WithDuplicateEmail_ThrowsConflictException()
{
    // Arrange
    var customerDto = new CreateCustomerDto
    {
        Email = "existing@example.com"
    };
    var mockRepository = new Mock<ICustomerRepository>();
    mockRepository.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>()))
                  .ReturnsAsync(true);
    var service = new CustomerService(mockRepository.Object);
    
    // Act & Assert
    await Assert.ThrowsAsync<ConflictException>(
        () => service.CreateCustomerAsync(customerDto)
    );
}

---

# ========================================
# 2. INTEGRATION TESTS (Test Full API)
# ========================================

# Test actual HTTP endpoints with real database (test DB)

// C# Integration Test (WebApplicationFactory)
[Fact]
public async Task POST_CreateCustomer_Returns201Created()
{
    // Arrange
    var client = _factory.CreateClient();
    var customerData = new
    {
        name = "Jane Smith",
        email = "jane@example.com",
        phone = "+1-555-0456"
    };
    
    // Act
    var response = await client.PostAsJsonAsync("/api/customers", customerData);
    
    // Assert
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    Assert.NotNull(response.Headers.Location);
    
    var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();
    Assert.NotNull(customer);
    Assert.Equal("Jane Smith", customer.Name);
    Assert.Equal("jane@example.com", customer.Email);
}

[Fact]
public async Task GET_NonExistentCustomer_Returns404()
{
    // Arrange
    var client = _factory.CreateClient();
    
    // Act
    var response = await client.GetAsync("/api/customers/99999");
    
    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}

[Fact]
public async Task POST_CreateCustomer_WithoutAuth_Returns401()
{
    // Arrange
    var client = _factory.CreateClient();
    // No Authorization header
    
    var customerData = new { name = "Test", email = "test@example.com" };
    
    // Act
    var response = await client.PostAsJsonAsync("/api/customers", customerData);
    
    // Assert
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
}

---

# ========================================
# 3. POSTMAN/NEWMAN TESTS
# ========================================

# Postman Collection for API Testing

// Test 1: Create Customer
POST https://api.company.com/api/customers
Content-Type: application/json
Authorization: Bearer {{authToken}}

{
  "name": "Test Customer",
  "email": "test@example.com"
}

// Postman Test Script:
pm.test("Status code is 201", function () {
    pm.response.to.have.status(201);
});

pm.test("Response has customerId", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData).to.have.property('customerId');
    pm.environment.set("customerId", jsonData.customerId);
});

pm.test("Response time < 500ms", function () {
    pm.expect(pm.response.responseTime).to.be.below(500);
});

---

// Test 2: Get Customer
GET https://api.company.com/api/customers/{{customerId}}
Authorization: Bearer {{authToken}}

// Postman Test Script:
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});

pm.test("Customer data matches", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData.name).to.eql("Test Customer");
    pm.expect(jsonData.email).to.eql("test@example.com");
});

---

// Test 3: Validation Error
POST https://api.company.com/api/customers
Content-Type: application/json
Authorization: Bearer {{authToken}}

{
  "name": "",
  "email": "invalid-email"
}

// Postman Test Script:
pm.test("Status code is 400", function () {
    pm.response.to.have.status(400);
});

pm.test("Validation errors present", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData.error).to.eql("VALIDATION_ERROR");
    pm.expect(jsonData.details).to.be.an('array');
});

---

# Run Postman tests in CI/CD (Newman):
newman run api-tests.postman_collection.json \
  --environment production.postman_environment.json \
  --reporters cli,junit \
  --reporter-junit-export results.xml

---

# ========================================
# 4. CONTRACT TESTS (API Contract Validation)
# ========================================

# Ensure API matches documented contract (OpenAPI/Swagger)

// Using Pact (Consumer-Driven Contracts)

// Consumer side (client test):
[Fact]
public async Task GetCustomer_ReturnsExpectedContract()
{
    // Define expected contract
    var pact = Pact.V3("CustomerApp", "CustomerAPI")
        .UponReceiving("A request for customer 123")
        .WithRequest(HttpMethod.Get, "/api/customers/123")
        .WithHeader("Authorization", Match.Type("Bearer token"))
        .WillRespond()
        .WithStatus(200)
        .WithJsonBody(new
        {
            customerId = Match.Type(123),
            name = Match.Type("John Doe"),
            email = Match.Regex("john@example.com", @"^[\w\.-]+@[\w\.-]+\.\w+$")
        });
    
    await pact.VerifyAsync(async ctx =>
    {
        var client = new HttpClient { BaseAddress = ctx.MockServerUri };
        var response = await client.GetAsync("/api/customers/123");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    });
}

// Provider side (API verification):
// Verify API implementation matches contracts

---

# ========================================
# 5. PERFORMANCE TESTS (Load Testing)
# ========================================

# Test API under load using k6

// k6 Load Test Script (JavaScript)
import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
  stages: [
    { duration: '1m', target: 50 },   // Ramp up to 50 users
    { duration: '3m', target: 50 },   // Stay at 50 users
    { duration: '1m', target: 100 },  // Ramp to 100 users
    { duration: '3m', target: 100 },  // Stay at 100 users
    { duration: '1m', target: 0 },    // Ramp down
  ],
  thresholds: {
    http_req_duration: ['p(95)<500'],  // 95% requests < 500ms
    http_req_failed: ['rate<0.01'],    // < 1% errors
  },
};

export default function () {
  // GET request
  let response = http.get('https://api.company.com/api/customers');
  
  check(response, {
    'status is 200': (r) => r.status === 200,
    'response time < 500ms': (r) => r.timings.duration < 500,
  });
  
  sleep(1);
  
  // POST request
  const payload = JSON.stringify({
    name: 'Load Test User',
    email: `user${__VU}_${__ITER}@example.com`,  // Unique email
  });
  
  response = http.post(
    'https://api.company.com/api/customers',
    payload,
    {
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + __ENV.AUTH_TOKEN,
      },
    }
  );
  
  check(response, {
    'create status is 201': (r) => r.status === 201,
  });
  
  sleep(1);
}

// Run test:
k6 run --vus 100 --duration 30s load-test.js

// Results:
/*
     ✓ status is 200
     ✓ response time < 500ms
     ✓ create status is 201

     checks.........................: 100.00% ✓ 15000  ✗ 0
     data_received..................: 45 MB   1.5 MB/s
     data_sent......................: 3.0 MB  100 kB/s
     http_req_duration..............: avg=245ms p(95)=450ms
     http_reqs......................: 15000   500/s
     vus............................: 100     min=50  max=100
*/

---

# ========================================
# 6. SECURITY TESTS (Vulnerability Scanning)
# ========================================

# Test for common vulnerabilities

# A. SQL Injection Test
POST /api/customers HTTP/1.1
Content-Type: application/json

{
  "name": "' OR '1'='1",
  "email": "test@example.com"
}

# Expected: Server rejects (validation error or sanitized)
# Vulnerable: SQL error or unauthorized data access

---

# B. XSS (Cross-Site Scripting) Test
POST /api/customers HTTP/1.1

{
  "name": "<script>alert('XSS')</script>",
  "email": "test@example.com"
}

# Expected: Input sanitized, script tags escaped
# Vulnerable: Script stored and executed

---

# C. Authentication Bypass Test
GET /api/customers/123 HTTP/1.1
# No Authorization header

# Expected: 401 Unauthorized
# Vulnerable: Returns data without auth

---

# D. Authorization Test (Privilege Escalation)
DELETE /api/users/456 HTTP/1.1
Authorization: Bearer regular_user_token

# Expected: 403 Forbidden (non-admin user)
# Vulnerable: Allows deletion

---

# E. Rate Limiting Test
# Send 1000 requests in 1 second

for i in {1..1000}; do
  curl -X GET https://api.company.com/api/products &
done

# Expected: 429 Too Many Requests after limit
# Vulnerable: All requests succeed (DoS risk)

---

# F. IDOR (Insecure Direct Object Reference)
# User A tries to access User B's data

GET /api/users/999/orders HTTP/1.1
Authorization: Bearer user_a_token  # User A (ID=123)

# Expected: 403 Forbidden or 404 Not Found
# Vulnerable: Returns User B's orders

---

# Automated Security Scanning with OWASP ZAP:
zap-cli quick-scan --self-contained \
  --start-options '-config api.disablekey=true' \
  https://api.company.com

---

# ========================================
# 7. SMOKE TESTS (Health Check)
# ========================================

# Quick tests after deployment to verify basic functionality

#!/bin/bash
# smoke-test.sh

API_URL="https://api.company.com"

# Test 1: Health endpoint
echo "Testing health endpoint..."
response=$(curl -s -o /dev/null -w "%{http_code}" $API_URL/health)
if [ $response -eq 200 ]; then
  echo "✓ Health check passed"
else
  echo "✗ Health check failed (HTTP $response)"
  exit 1
fi

# Test 2: Authentication
echo "Testing authentication..."
response=$(curl -s -o /dev/null -w "%{http_code}" \
  -X POST $API_URL/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"test@example.com","password":"test123"}')
if [ $response -eq 200 ]; then
  echo "✓ Authentication passed"
else
  echo "✗ Authentication failed (HTTP $response)"
  exit 1
fi

# Test 3: Basic CRUD
echo "Testing GET endpoint..."
response=$(curl -s -o /dev/null -w "%{http_code}" \
  -H "Authorization: Bearer $AUTH_TOKEN" \
  $API_URL/api/customers)
if [ $response -eq 200 ]; then
  echo "✓ GET endpoint passed"
else
  echo "✗ GET endpoint failed (HTTP $response)"
  exit 1
fi

echo "All smoke tests passed!"

---

# ========================================
# 8. CI/CD PIPELINE TESTS
# ========================================

# GitHub Actions workflow example

name: API Tests

on: [push, pull_request]

jobs:
  test:
    runs-on: ubuntu-latest
    
    steps:
      - uses: actions/checkout@v2
      
      # Unit Tests
      - name: Run Unit Tests
        run: dotnet test --filter Category=Unit
      
      # Integration Tests
      - name: Start Test Database
        run: docker-compose up -d test-db
      
      - name: Run Integration Tests
        run: dotnet test --filter Category=Integration
      
      # Contract Tests
      - name: Run Contract Tests
        run: dotnet test --filter Category=Contract
      
      # API Tests with Newman
      - name: Install Newman
        run: npm install -g newman
      
      - name: Run Postman Tests
        run: newman run tests/api-tests.postman_collection.json \
          --environment tests/test.postman_environment.json
      
      # Security Scan
      - name: OWASP Dependency Check
        run: |
          dotnet tool install --global dotnet-security-scan
          dotnet security-scan
      
      # Performance Smoke Test
      - name: Quick Load Test
        run: |
          npm install -g k6
          k6 run --vus 10 --duration 30s tests/load-test.js
      
      # Report Results
      - name: Publish Test Results
        uses: actions/upload-artifact@v2
        with:
          name: test-results
          path: test-results/
```

**Testing Best Practices:**
* ✓ Write tests for all endpoints (happy path + error cases)
* ✓ Test authentication and authorization
* ✓ Test input validation (valid, invalid, edge cases)
* ✓ Test error responses (status codes, error messages)
* ✓ Test performance under load
* ✓ Automate tests in CI/CD pipeline
* ✓ Use test databases (not production!)
* ✓ Mock external dependencies in unit tests
* ✓ Test idempotency (especially for POST/DELETE)
* ✓ Verify rate limiting works
* ✓ Test CORS configuration
* ✓ Test file uploads (size limits, types, malicious files)
* ✓ Security testing for common vulnerabilities
* ✓ Monitor test coverage (aim for >80%)

**Test Pyramid:**

```
         /\
        /  \  E2E Tests (Few, Slow, Expensive)
       /____\
      /      \
     / Integration Tests (Some, Medium)
    /________\
   /          \
  /  Unit Tests  \  (Many, Fast, Cheap)
 /______________\
```

**Testing Tools:**
* **Unit**: xUnit, JUnit, pytest, Jest
* **Integration**: RestAssured, Supertest, WebApplicationFactory
* **API**: Postman, Newman, Insomnia
* **Contract**: Pact, Spring Cloud Contract
* **Performance**: k6, JMeter, Gatling, Apache Bench
* **Security**: OWASP ZAP, Burp Suite, SonarQube
* **Mocking**: WireMock, MockServer, Mountebank

---

### 38. How do you design APIs for mobile vs web clients?

**Mobile vs Web API Design** requires different considerations due to constraints in network connectivity, battery life, screen size, and data usage. Mobile APIs prioritize efficiency, offline support, and reduced payloads, while web APIs can be more flexible with data transfer.

**Why it's needed:** Mobile devices face unique challenges - limited bandwidth, intermittent connectivity, battery constraints, and varied network conditions (3G/4G/5G/WiFi). Optimizing APIs for mobile improves user experience, reduces costs, and extends battery life.

**When to use:** Design mobile-specific optimizations for native mobile apps, progressive web apps (PWAs), and any application targeting mobile devices. Consider implementing API gateways with mobile-specific endpoints or response transformations.

| Aspect | Web API Design | Mobile API Design |
|--------|----------------|-------------------|
| **Payload Size** | Can be larger | Minimize (gzip, smaller responses) |
| **Requests** | Multiple requests acceptable | Batch/aggregate to reduce round-trips |
| **Caching** | Standard HTTP caching | Aggressive caching, offline support |
| **Fields** | Return all fields | Selective fields, pagination |
| **Images** | Full resolution | Multiple sizes, progressive loading |
| **Real-time** | WebSockets, polling | Push notifications, efficient polling |
| **Authentication** | Session/JWT | Long-lived tokens, refresh tokens |
| **Versioning** | Can force updates | Graceful degradation, backward compat |

**Real-time Example:**

```http
# ========================================
# 1. PAYLOAD OPTIMIZATION
# ========================================

# WEB API - Full response (acceptable)
GET /api/products/PROD-789 HTTP/1.1

HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 3456

{
  "productId": "PROD-789",
  "name": "Wireless Keyboard",
  "description": "Full-size wireless keyboard with mechanical keys, RGB backlighting, and customizable macros. Features include...",
  "longDescription": "Detailed product information spanning multiple paragraphs...",
  "price": 79.99,
  "currency": "USD",
  "stock": 150,
  "category": "Electronics",
  "subcategory": "Computer Accessories",
  "brand": "TechBrand",
  "sku": "KB-WL-789-BLK",
  "weight": 850,
  "dimensions": {
    "length": 440,
    "width": 135,
    "height": 35,
    "unit": "mm"
  },
  "images": [
    {
      "url": "https://cdn.company.com/products/PROD-789/image1-4k.jpg",
      "resolution": "3840x2160",
      "size": 2457600
    },
    {
      "url": "https://cdn.company.com/products/PROD-789/image2-4k.jpg",
      "resolution": "3840x2160",
      "size": 2621440
    }
  ],
  "specifications": {...},  # 50+ detailed specs
  "reviews": [...],  # 100 reviews
  "relatedProducts": [...],  # 20 related items
  "metadata": {...}
}

---

# MOBILE API - Optimized response
GET /api/mobile/v1/products/PROD-789 HTTP/1.1
Accept-Encoding: gzip

HTTP/1.1 200 OK
Content-Type: application/json
Content-Encoding: gzip
Content-Length: 856  # Compressed

{
  "id": "PROD-789",
  "name": "Wireless Keyboard",
  "shortDesc": "Full-size wireless keyboard with RGB lighting",  # Truncated
  "price": 79.99,
  "currency": "USD",
  "inStock": true,  # Boolean instead of count
  "image": "https://cdn.company.com/products/PROD-789/thumb-400.jpg",  # Smaller image
  "rating": 4.5,
  "reviewCount": 100
  # No specifications, full reviews, or related products in initial load
}

# Size reduction: 3456 → 856 bytes (75% smaller!)
# Mobile benefits:
# ✓ Faster load time
# ✓ Less data usage
# ✓ Less battery drain

---

# ========================================
# 2. FIELD SELECTION (GraphQL-style)
# ========================================

# Mobile requests only needed fields

GET /api/products/PROD-789?fields=id,name,price,image HTTP/1.1

HTTP/1.1 200 OK
{
  "id": "PROD-789",
  "name": "Wireless Keyboard",
  "price": 79.99,
  "image": "https://cdn.company.com/products/PROD-789/thumb-400.jpg"
}

# Lazy loading - Fetch details on demand
# Initial list view: id, name, price, thumbnail
# Detail view: GET /api/products/PROD-789?fields=all

---

# ========================================
# 3. REQUEST BATCHING/AGGREGATION
# ========================================

# WEB - Multiple requests acceptable
GET /api/customers/123 HTTP/1.1
GET /api/customers/123/orders HTTP/1.1
GET /api/customers/123/payment-methods HTTP/1.1

# 3 HTTP requests, ~300ms total

---

# MOBILE - Single aggregated request
GET /api/mobile/v1/customers/123/dashboard HTTP/1.1

HTTP/1.1 200 OK
{
  "customer": {
    "id": 123,
    "name": "John Doe",
    "email": "john@example.com"
  },
  "recentOrders": [
    {"id": "ORD-501", "total": 299.99, "status": "shipped"}
  ],
  "paymentMethods": [
    {"id": "PM-789", "type": "card", "last4": "4242"}
  ]
}

# 1 HTTP request, ~150ms
# Reduced:
# ✓ Network round-trips (important on 3G/4G)
# ✓ Battery consumption
# ✓ Data usage

---

# ========================================
# 4. IMAGE OPTIMIZATION
# ========================================

# WEB - High-resolution images
GET /api/products/PROD-789/images HTTP/1.1

HTTP/1.1 200 OK
{
  "images": [
    {
      "url": "https://cdn.company.com/products/PROD-789/full-4k.jpg",
      "width": 3840,
      "height": 2160,
      "size": 2457600  # 2.4 MB
    }
  ]
}

---

# MOBILE - Multiple sizes, progressive loading
GET /api/mobile/v1/products/PROD-789/images HTTP/1.1

HTTP/1.1 200 OK
{
  "images": [
    {
      "thumbnail": "https://cdn.company.com/products/PROD-789/thumb-200.jpg",
      "width": 200,
      "height": 112,
      "size": 12288  # 12 KB - Load first
    },
    {
      "medium": "https://cdn.company.com/products/PROD-789/medium-800.jpg",
      "width": 800,
      "height": 450,
      "size": 81920  # 80 KB - Load on demand
    },
    {
      "large": "https://cdn.company.com/products/PROD-789/large-1920.jpg",
      "width": 1920,
      "height": 1080,
      "size": 409600  # 400 KB - Load on zoom
    }
  ]
}

# Mobile app strategy:
# 1. Load thumbnail immediately (12 KB)
# 2. Load medium when user scrolls into view
# 3. Load large only when user zooms/taps

# Also use modern formats: WebP (30% smaller than JPEG)

---

# ========================================
# 5. PAGINATION FOR MOBILE
# ========================================

# WEB - Page-based pagination
GET /api/products?page=1&pageSize=50 HTTP/1.1

# Returns 50 products at once

---

# MOBILE - Cursor-based, smaller pages
GET /api/mobile/v1/products?limit=20 HTTP/1.1

HTTP/1.1 200 OK
{
  "products": [... 20 items ...],
  "nextCursor": "eyJpZCI6MjB9",
  "hasMore": true
}

# Infinite scroll friendly
# Smaller pages = faster initial load
# User scrolls → Load next page

GET /api/mobile/v1/products?cursor=eyJpZCI6MjB9&limit=20 HTTP/1.1

---

# ========================================
# 6. CACHING STRATEGIES
# ========================================

# WEB - Standard HTTP caching
GET /api/products HTTP/1.1

HTTP/1.1 200 OK
Cache-Control: max-age=3600
ETag: "abc123"

---

# MOBILE - Aggressive caching + offline support
GET /api/mobile/v1/products HTTP/1.1
If-None-Match: "abc123"

HTTP/1.1 304 Not Modified
Cache-Control: max-age=86400, stale-while-revalidate=604800
# Cache for 24 hours
# Allow stale content for 7 days if offline

# Mobile app stores:
# - Products list: 24 hours
# - Product details: 1 hour
# - User profile: Until app restart
# - Images: Forever (until cache cleared)

---

# Offline-first approach
# App checks local cache first:
# 1. Request data
# 2. Return cached data immediately
# 3. Fetch from API in background
# 4. Update cache and UI when response arrives

---

# ========================================
# 7. PUSH NOTIFICATIONS vs POLLING
# ========================================

# WEB - Polling acceptable
# Check for new notifications every 30 seconds
GET /api/notifications?since=2026-01-23T10:30:00Z HTTP/1.1

# Repeat every 30 seconds

---

# MOBILE - Push notifications
# Server sends push notification when event occurs
# No polling needed → Saves battery and data

# Backend sends push:
POST https://fcm.googleapis.com/fcm/send
Authorization: Bearer server_key
{
  "to": "device_fcm_token",
  "notification": {
    "title": "Order Shipped",
    "body": "Your order #ORD-501 has been shipped"
  },
  "data": {
    "orderId": "ORD-501",
    "type": "order_shipped"
  }
}

# Mobile app receives push → Updates UI
# No battery drain from polling

---

# If polling required (e.g., for app that's open):
# Use exponential backoff
# App in foreground: Poll every 15s
# App in background: Poll every 5 minutes or use push

---

# ========================================
# 8. AUTHENTICATION FOR MOBILE
# ========================================

# WEB - Short-lived JWT (15-60 min)
POST /api/auth/login HTTP/1.1

HTTP/1.1 200 OK
{
  "accessToken": "eyJhbGc...",
  "expiresIn": 900,  # 15 minutes
  "refreshToken": "RT-abc123..."
}

---

# MOBILE - Long-lived tokens + refresh tokens
POST /api/mobile/v1/auth/login HTTP/1.1

HTTP/1.1 200 OK
{
  "accessToken": "eyJhbGc...",
  "expiresIn": 3600,  # 1 hour (longer than web)
  "refreshToken": "RT-abc123...",
  "refreshTokenExpiresIn": 2592000  # 30 days
}

# Store tokens securely (iOS Keychain, Android Keystore)
# Auto-refresh before expiration
# No need to re-login frequently

---

# Silent token refresh (background)
POST /api/mobile/v1/auth/refresh HTTP/1.1
Content-Type: application/json

{
  "refreshToken": "RT-abc123..."
}

HTTP/1.1 200 OK
{
  "accessToken": "eyJhbGc...",  # New access token
  "expiresIn": 3600
}

---

# ========================================
# 9. ERROR HANDLING FOR POOR CONNECTIVITY
# ========================================

# Mobile: Graceful degradation

# Scenario: Request times out on slow 3G
GET /api/mobile/v1/products HTTP/1.1

# Mobile app strategy:
# 1. Set timeout: 30 seconds (longer than web's 10s)
# 2. If timeout: Show cached data
# 3. Display banner: "Showing cached data. Retrying..."
# 4. Retry with exponential backoff

# Response when online:
HTTP/1.1 200 OK
X-Data-Source: network
{
  "products": [...]
}

# Response when offline (from cache):
X-Data-Source: cache
X-Cache-Age: 3600  # 1 hour old
{
  "products": [...],
  "_cached": true,
  "_cacheAge": 3600
}

---

# ========================================
# 10. BANDWIDTH-AWARE APIS
# ========================================

# Mobile app detects connection type

# On WiFi - Full quality
GET /api/mobile/v1/feed?quality=high HTTP/1.1

HTTP/1.1 200 OK
{
  "posts": [
    {
      "id": 1,
      "text": "Post content...",
      "image": "https://cdn.company.com/images/full-hd.jpg",  # 1080p
      "video": "https://cdn.company.com/videos/1080p.mp4"
    }
  ]
}

---

# On 3G/4G - Optimized quality
GET /api/mobile/v1/feed?quality=low HTTP/1.1

HTTP/1.1 200 OK
{
  "posts": [
    {
      "id": 1,
      "text": "Post content...",
      "image": "https://cdn.company.com/images/compressed-sd.jpg",  # 480p
      "video": null  # No auto-play video on cellular
    }
  ]
}

---

# ========================================
# 11. DELTA SYNC (Incremental Updates)
# ========================================

# WEB - Full data fetch
GET /api/products HTTP/1.1

HTTP/1.1 200 OK
# Returns all 1000 products (large payload)

---

# MOBILE - Delta sync
# First sync:
GET /api/mobile/v1/products/sync HTTP/1.1

HTTP/1.1 200 OK
{
  "products": [... 1000 products ...],
  "syncToken": "sync-token-abc123",
  "timestamp": "2026-01-23T10:00:00Z"
}

# Store locally

---

# Subsequent syncs (only changes):
GET /api/mobile/v1/products/sync?since=sync-token-abc123 HTTP/1.1

HTTP/1.1 200 OK
{
  "added": [
    {"id": 1001, "name": "New Product"}
  ],
  "updated": [
    {"id": 789, "name": "Updated Name", "price": 89.99}
  ],
  "deleted": [456],  # Product IDs removed
  "syncToken": "sync-token-def456",
  "timestamp": "2026-01-23T11:00:00Z"
}

# Only 3 changes sent instead of 1000 products
# Massive bandwidth savings

---

# ========================================
# 12. BINARY PROTOCOLS FOR MOBILE
# ========================================

# Consider Protocol Buffers (protobuf) or MessagePack
# Instead of JSON (text), use binary format

# JSON response: 1,234 bytes
{
  "id": 123,
  "name": "John Doe",
  "email": "john@example.com",
  ...
}

# Protobuf response: 487 bytes (60% smaller!)
# Faster parsing, less bandwidth, less battery

# gRPC (uses protobuf) for mobile-backend communication
```

**Mobile API Best Practices:**
* ✓ Minimize payload size (compress, remove unnecessary fields)
* ✓ Reduce number of requests (batch, aggregate)
* ✓ Use appropriate image sizes
* ✓ Implement aggressive caching
* ✓ Support offline mode
* ✓ Use push notifications instead of polling
* ✓ Implement delta sync for large datasets
* ✓ Use long-lived tokens (reduce re-authentication)
* ✓ Provide bandwidth-aware options
* ✓ Handle poor connectivity gracefully
* ✓ Consider binary protocols (protobuf, gRPC)
* ✓ Implement request timeouts (longer than web)
* ✓ Use CDN for static assets
* ✓ Version APIs carefully (support old app versions)

**Mobile-Specific Headers:**

```http
X-App-Version: 2.1.0          # Track app version
X-Device-Type: iPhone14        # Device info
X-OS-Version: iOS-17.2         # OS version
X-Connection-Type: 4G          # Network type
X-Battery-Level: 45            # Battery percentage
```

**Mobile API Gateway Pattern:**
* Web API Gateway → Serves web clients
* Mobile API Gateway → Optimized for mobile
* Backend Microservices → Shared by both gateways

---

### 39. What are common API design mistakes and how to avoid them?

**API design mistakes** are architectural and implementation errors that make APIs difficult to use, maintain, or scale. These mistakes lead to poor developer experience, integration issues, performance problems, and technical debt.

**Why it's needed:** APIs are contracts that are difficult to change once deployed and consumed. Design mistakes early on compound over time, affecting all consumers. Understanding and avoiding common pitfalls ensures APIs are intuitive, maintainable, and scalable.

**When to use:** Review these principles during API design phase, code reviews, and when refactoring existing APIs. Apply consistently across all endpoints.

**Common API Design Mistakes:**

| Mistake | Problem | Solution |
|---------|---------|----------|
| **Using GET for state changes** | Violates REST, caching issues | Use POST/PUT/DELETE |
| **Inconsistent naming** | Confusing, hard to learn | Establish naming conventions |
| **Not versioning** | Breaking changes affect all clients | Version from day one |
| **Exposing internal structure** | Tight coupling, hard to change | Use DTOs, abstraction |
| **Poor error messages** | Hard to debug | Provide detailed, actionable errors |
| **No pagination** | Performance issues | Always paginate collections |
| **Ignoring HTTP status codes** | Client confusion | Use appropriate codes |
| **No rate limiting** | Abuse, DoS vulnerability | Implement rate limiting |

**Real-time Example:**

```http
# ========================================
# MISTAKE 1: Using GET for State Changes
# ========================================

# WRONG - State-changing operation with GET
GET /api/orders/ORD-501/cancel HTTP/1.1

# Problems:
# - GET should be safe (no side effects)
# - Browser/crawlers may prefetch
# - Can't use CSRF protection
# - Violates REST principles
# - Cached responses cause issues

# CORRECT - Use POST/DELETE for state changes
POST /api/orders/ORD-501/cancel HTTP/1.1

# Or
DELETE /api/orders/ORD-501 HTTP/1.1

---

# ========================================
# MISTAKE 2: Inconsistent Naming
# ========================================

# WRONG - Inconsistent endpoint naming
GET /api/getCustomers           # Verb in URL
POST /api/customer              # Singular
GET /api/Orders                 # Capitalized
DELETE /api/delete_product/123  # Mixed styles

# CORRECT - Consistent RESTful naming
GET    /api/customers           # Plural noun
POST   /api/customers           # Plural noun
GET    /api/orders              # Lowercase, plural
DELETE /api/products/123        # Consistent pattern

# Conventions:
# - Use nouns, not verbs
# - Use plurals consistently
# - Use lowercase with hyphens (kebab-case)
# - HTTP method indicates action

---

# ========================================
# MISTAKE 3: Exposing Database Structure
# ========================================

# WRONG - Exposing internal DB structure
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
{
  "customer_id": 123,                    # DB column names
  "first_name": "John",
  "last_name": "Doe",
  "created_timestamp": "2026-01-15 10:30:45.123",  # DB format
  "is_deleted": false,                   # Internal flag
  "internal_notes": "VIP customer",      # Internal data
  "db_version": 5,                       # DB versioning
  "_legacy_field": "old_value"           # Legacy DB field
}

# Problems:
# - Exposes internal implementation
# - Can't change DB without breaking API
# - Leaks sensitive/internal data
# - Non-standard naming (snake_case vs camelCase)

---

# CORRECT - Use DTOs (Data Transfer Objects)
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
{
  "customerId": 123,                     # API naming convention
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "status": "active",                    # Business concept
  "createdAt": "2026-01-15T10:30:45Z"   # ISO 8601 format
}

# Benefits:
# ✓ Stable API contract
# ✓ Can change DB without breaking API
# ✓ No internal data exposure
# ✓ Consistent naming

---

# ========================================
# MISTAKE 4: Poor Error Messages
# ========================================

# WRONG - Generic, unhelpful errors
POST /api/customers HTTP/1.1
{
  "name": "",
  "email": "invalid-email"
}

HTTP/1.1 400 Bad Request
{
  "error": "Bad Request"
}

# OR worse:
HTTP/1.1 200 OK
{
  "success": false,
  "message": "Error"
}

# Problems:
# - No details about what's wrong
# - Client can't fix the issue
# - Hard to debug

---

# CORRECT - Detailed, actionable errors
POST /api/customers HTTP/1.1
{
  "name": "",
  "email": "invalid-email"
}

HTTP/1.1 400 Bad Request
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Request validation failed",
    "details": [
      {
        "field": "name",
        "message": "Name is required and cannot be empty",
        "rejectedValue": ""
      },
      {
        "field": "email",
        "message": "Email must be in valid format (e.g., user@example.com)",
        "rejectedValue": "invalid-email"
      }
    ],
    "timestamp": "2026-01-23T10:30:00Z",
    "path": "/api/customers",
    "requestId": "req-abc123"
  }
}

# Benefits:
# ✓ Client knows exactly what's wrong
# ✓ Can provide user-friendly error messages
# ✓ Easy to debug with requestId
# ✓ Actionable (knows how to fix)

---

# ========================================
# MISTAKE 5: Not Implementing Pagination
# ========================================

# WRONG - Returns all records
GET /api/customers HTTP/1.1

HTTP/1.1 200 OK
[
  ... 50,000 customers ...
]

# Problems:
# - Slow response time
# - High bandwidth usage
# - Memory issues
# - Timeout risk
# - Poor UX

---

# CORRECT - Always paginate collections
GET /api/customers?page=1&pageSize=20 HTTP/1.1

HTTP/1.1 200 OK
{
  "data": [
    ... 20 customers ...
  ],
  "pagination": {
    "currentPage": 1,
    "pageSize": 20,
    "totalPages": 2500,
    "totalCount": 50000,
    "hasNextPage": true,
    "hasPreviousPage": false
  },
  "links": {
    "self": "/api/customers?page=1&pageSize=20",
    "next": "/api/customers?page=2&pageSize=20",
    "last": "/api/customers?page=2500&pageSize=20"
  }
}

---

# ========================================
# MISTAKE 6: Returning 200 OK for Errors
# ========================================

# WRONG - Always 200, error in body
GET /api/customers/99999 HTTP/1.1

HTTP/1.1 200 OK
{
  "success": false,
  "error": "Customer not found"
}

# Problems:
# - Client libraries can't detect errors
# - Middleware/proxies treat as success
# - Violates HTTP semantics
# - Breaks error monitoring tools

---

# CORRECT - Use appropriate HTTP status codes
GET /api/customers/99999 HTTP/1.1

HTTP/1.1 404 Not Found
{
  "error": {
    "code": "RESOURCE_NOT_FOUND",
    "message": "Customer not found",
    "resourceType": "Customer",
    "resourceId": "99999"
  }
}

# Use proper codes:
# 200 OK - Success
# 201 Created - Resource created
# 400 Bad Request - Validation error
# 401 Unauthorized - Auth required
# 403 Forbidden - No permission
# 404 Not Found - Resource doesn't exist
# 500 Internal Server Error - Server error

---

# ========================================
# MISTAKE 7: Not Versioning APIs
# ========================================

# WRONG - No versioning, breaking changes
# Version 1 (Initial):
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
{
  "id": 123,
  "name": "John Doe"
}

# Later, breaking change:
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
{
  "customerId": 123,            # Changed: id → customerId
  "firstName": "John",          # Changed: name split
  "lastName": "Doe"
}

# Problem: All existing clients break!

---

# CORRECT - Version from day one
# Version 1:
GET /api/v1/customers/123 HTTP/1.1

HTTP/1.1 200 OK
{
  "id": 123,
  "name": "John Doe"
}

# Version 2 (new version with changes):
GET /api/v2/customers/123 HTTP/1.1

HTTP/1.1 200 OK
{
  "customerId": 123,
  "firstName": "John",
  "lastName": "Doe"
}

# V1 clients continue working
# V2 clients use new structure
# Deprecate V1 with grace period

---

# ========================================
# MISTAKE 8: Ignoring Security Headers
# ========================================

# WRONG - No security headers
HTTP/1.1 200 OK
Content-Type: application/json

---

# CORRECT - Include security headers
HTTP/1.1 200 OK
Content-Type: application/json
Strict-Transport-Security: max-age=31536000; includeSubDomains
X-Content-Type-Options: nosniff
X-Frame-Options: DENY
X-XSS-Protection: 1; mode=block
Content-Security-Policy: default-src 'self'
Referrer-Policy: no-referrer

---

# ========================================
# MISTAKE 9: Not Implementing Rate Limiting
# ========================================

# WRONG - No rate limiting
# Attacker sends 100,000 requests in 1 minute
# All requests succeed → DoS, server overload

---

# CORRECT - Implement rate limiting
GET /api/products HTTP/1.1

# Within limits:
HTTP/1.1 200 OK
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 847
X-RateLimit-Reset: 1706015400

# Exceeded limits:
HTTP/1.1 429 Too Many Requests
Retry-After: 300
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 0

{
  "error": "RATE_LIMIT_EXCEEDED",
  "message": "Too many requests. Please try again later.",
  "retryAfter": 300
}

---

# ========================================
# MISTAKE 10: Overly Chatty APIs
# ========================================

# WRONG - Requires multiple requests
# To display order details:

GET /api/orders/ORD-501          # Get order
GET /api/customers/123           # Get customer
GET /api/products/789            # Get product 1
GET /api/products/790            # Get product 2

# 4 HTTP requests → Slow, especially on mobile

---

# CORRECT - Provide aggregated endpoints
GET /api/orders/ORD-501?include=customer,products HTTP/1.1

HTTP/1.1 200 OK
{
  "orderId": "ORD-501",
  "total": 299.99,
  "customer": {
    "customerId": 123,
    "name": "John Doe"
  },
  "items": [
    {
      "product": {
        "productId": 789,
        "name": "Wireless Mouse"
      },
      "quantity": 2
    }
  ]
}

# 1 HTTP request → Fast, efficient

---

# ========================================
# MISTAKE 11: Not Handling Concurrent Updates
# ========================================

# WRONG - No optimistic locking
# User A and User B both update customer 123

# User A:
GET /api/customers/123           # balance: $100
PUT /api/customers/123           # Set balance: $150

# User B (simultaneous):
GET /api/customers/123           # balance: $100 (stale!)
PUT /api/customers/123           # Set balance: $200

# Final balance: $200 (User A's update lost!)

---

# CORRECT - Use ETags for optimistic locking
# User A:
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
ETag: "version-1"
{
  "customerId": 123,
  "balance": 100
}

PUT /api/customers/123 HTTP/1.1
If-Match: "version-1"
{
  "balance": 150
}

HTTP/1.1 200 OK
ETag: "version-2"

---

# User B (simultaneous):
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
ETag: "version-1"

PUT /api/customers/123 HTTP/1.1
If-Match: "version-1"           # Stale version!
{
  "balance": 200
}

HTTP/1.1 412 Precondition Failed
ETag: "version-2"
{
  "error": "CONFLICT",
  "message": "Resource has been modified. Please refresh and try again."
}

# User B must re-fetch and apply changes to latest version

---

# ========================================
# MISTAKE 12: Lack of Documentation
# ========================================

# WRONG - No documentation
# Developers guess how to use API
# Trial and error, frustration

---

# CORRECT - Comprehensive documentation
# - OpenAPI/Swagger spec
# - Interactive API explorer (Swagger UI)
# - Code examples (cURL, JavaScript, Python, etc.)
# - Error code reference
# - Authentication guide
# - Rate limiting info
# - SDKs/client libraries

# Example: Stripe API documentation
# https://stripe.com/docs/api
# - Clear, searchable
# - Live testing in docs
# - Multiple language examples
# - Comprehensive error reference

---

# ========================================
# MISTAKE 13: Not Using Idempotency Keys
# ========================================

# WRONG - No idempotency for POST
POST /api/orders HTTP/1.1
{
  "customerId": 123,
  "total": 299.99
}

# Network timeout, client retries
POST /api/orders HTTP/1.1
(same body)

# Result: 2 orders created! (duplicate charge)

---

# CORRECT - Use idempotency keys
POST /api/orders HTTP/1.1
Idempotency-Key: 550e8400-e29b-41d4-a716-446655440000
{
  "customerId": 123,
  "total": 299.99
}

# Retry with same key:
POST /api/orders HTTP/1.1
Idempotency-Key: 550e8400-e29b-41d4-a716-446655440000

# Returns original response
# Only 1 order created (safe retry)
```

**API Design Checklist:**
* [ ] Use nouns in URLs, not verbs
* [ ] Use plural names for collections
* [ ] Use proper HTTP methods (GET, POST, PUT, PATCH, DELETE)
* [ ] Never use GET for state-changing operations
* [ ] Return appropriate HTTP status codes
* [ ] Version API from day one (/api/v1/)
* [ ] Implement pagination for collections
* [ ] Provide filtering, sorting, searching
* [ ] Use consistent naming conventions
* [ ] Return detailed, actionable error messages
* [ ] Include request IDs for tracing
* [ ] Implement authentication and authorization
* [ ] Add rate limiting
* [ ] Use HTTPS only
* [ ] Implement CORS properly
* [ ] Add security headers
* [ ] Use DTOs (don't expose DB structure)
* [ ] Document API thoroughly (OpenAPI/Swagger)
* [ ] Implement idempotency for unsafe operations
* [ ] Use ETags for caching and concurrency control
* [ ] Provide health check endpoints
* [ ] Support content negotiation (JSON, XML)
* [ ] Use compression (gzip)
* [ ] Implement monitoring and logging
* [ ] Handle concurrent updates properly
* [ ] Provide aggregated endpoints (reduce chattiness)
* [ ] Cache responses where appropriate
* [ ] Test extensively (unit, integration, load, security)

**Resources for API Design:**
* REST API Design Rulebook (O'Reilly)
* Microsoft REST API Guidelines
* Google API Design Guide
* API Design Patterns (Manning)
* RESTful Web APIs (O'Reilly)

---

### 40. How do you handle backwards compatibility when evolving REST APIs?

**Backwards compatibility** ensures that existing API clients continue to work when you make changes to your API. It's about evolving APIs without breaking integrations, requiring coordinated migrations, or version proliferation.

**Why it's needed:** Breaking existing clients causes production outages, frustrated users, emergency fixes, and erodes trust. Coordinating simultaneous updates across all consumers is impractical. Backwards compatibility enables independent deployment and gradual migration.

**When to use:** Apply backwards compatibility principles for every API change - adding features, fixing bugs, improving performance. Essential for public APIs, multi-tenant systems, and microservices with multiple consumers.

**Types of Changes:**

| Change Type | Backwards Compatible? | Example |
|-------------|----------------------|---------|
| **Add new endpoint** | ✓ Yes | New `/api/reports` endpoint |
| **Add new optional field to request** | ✓ Yes | New `phone` field (optional) |
| **Add new field to response** | ✓ Yes (mostly) | Add `createdAt` to customer |
| **Add new enum value** | ✓ Yes (if clients handle unknown) | Add "pending" status |
| **Remove field from response** | ✗ No | Remove `email` field |
| **Rename field** | ✗ No | `id` → `customerId` |
| **Change field type** | ✗ No | `price` string → number |
| **Make required field optional** | ✓ Yes | `phone` required → optional |
| **Make optional field required** | ✗ No | `email` optional → required |
| **Change URL structure** | ✗ No | `/customers` → `/clients` |

**Real-time Example:**

```http
# ========================================
# COMPATIBLE: Add New Optional Field
# ========================================

# Version 1 (existing)
POST /api/customers HTTP/1.1
{
  "name": "John Doe",
  "email": "john@example.com"
}

HTTP/1.1 201 Created
{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com"
}

---

# Version 1.1 (add optional phone field)
POST /api/customers HTTP/1.1
{
  "name": "Jane Smith",
  "email": "jane@example.com",
  "phone": "+1-555-0123"  # New optional field
}

HTTP/1.1 201 Created
{
  "customerId": 124,
  "name": "Jane Smith",
  "email": "jane@example.com",
  "phone": "+1-555-0123"  # Included if provided
}

# Old clients (no phone) still work:
POST /api/customers HTTP/1.1
{
  "name": "Bob Wilson",
  "email": "bob@example.com"
  # No phone field
}

HTTP/1.1 201 Created
{
  "customerId": 125,
  "name": "Bob Wilson",
  "email": "bob@example.com"
  # No phone in response
}

# ✓ Backwards compatible
# Old clients continue working
# New clients can use new field

---

# ========================================
# COMPATIBLE: Add New Field to Response
# ========================================

# Version 1 (existing)
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com"
}

---

# Version 1.1 (add createdAt field)
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com",
  "createdAt": "2026-01-15T10:00:00Z"  # New field
}

# Old clients ignore unknown fields (JSON parsers handle this)
# New clients use the new field

# ✓ Backwards compatible (with caveat: clients must ignore unknown fields)
# Best practice: Design clients to be forward-compatible

---

# ========================================
# INCOMPATIBLE: Remove Field from Response
# ========================================

# Version 1 (existing)
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com",
  "phoneNumber": "+1-555-0123"
}

---

# Version 1.1 (remove phoneNumber) - BREAKS CLIENTS!
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com"
  # phoneNumber removed
}

# Old clients expecting phoneNumber:
# customer.phoneNumber → undefined/null
# May cause errors if not null-checked

# ✗ NOT backwards compatible
# Requires new API version

---

# SOLUTION: Deprecation Period
# Version 1 (mark field as deprecated, still return it)
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
Warning: 299 - "Field 'phoneNumber' is deprecated. Use 'phone' instead. Will be removed in v2."
{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com",
  "phoneNumber": "+1-555-0123",  # Still included
  "phone": "+1-555-0123"         # New field
}

# Give clients 6-12 months to migrate
# Then release v2 without phoneNumber

---

# ========================================
# INCOMPATIBLE: Rename Field
# ========================================

# Version 1 (existing)
{
  "id": 123,
  "name": "John Doe"
}

---

# Renamed field - BREAKS CLIENTS!
{
  "customerId": 123,  # Was "id"
  "name": "John Doe"
}

# Old clients: customer.id → undefined

# ✗ NOT backwards compatible

---

# SOLUTION: Support both fields temporarily
{
  "id": 123,              # Deprecated, still included
  "customerId": 123,      # New field
  "name": "John Doe"
}

# Documentation:
# "id field is deprecated. Use customerId instead. id will be removed in v2."

# After migration period, release v2 with only customerId

---

# ========================================
# INCOMPATIBLE: Change Field Type
# ========================================

# Version 1 (existing)
{
  "orderId": "ORD-123",
  "total": "299.99",     # String
  "quantity": "2"        # String
}

---

# Changed to numbers - BREAKS CLIENTS!
{
  "orderId": "ORD-123",
  "total": 299.99,       # Number
  "quantity": 2          # Number
}

# JavaScript clients: parseInt(customer.quantity) now fails
# Type-safe languages: Parsing errors

# ✗ NOT backwards compatible

---

# SOLUTION: New API version
# v1 - Keep strings
GET /api/v1/orders/ORD-123

{
  "total": "299.99"
}

# v2 - Use numbers
GET /api/v2/orders/ORD-123

{
  "total": 299.99
}

---

# ========================================
# COMPATIBLE: Make Required Field Optional
# ========================================

# Version 1 (existing - phone required)
POST /api/customers HTTP/1.1
{
  "name": "John Doe",
  "email": "john@example.com",
  "phone": "+1-555-0123"  # Required
}

---

# Version 1.1 (phone now optional)
POST /api/customers HTTP/1.1
{
  "name": "Jane Smith",
  "email": "jane@example.com"
  # No phone - now acceptable
}

HTTP/1.1 201 Created

# Old clients (still sending phone) work fine
# New clients (not sending phone) also work

# ✓ Backwards compatible

---

# ========================================
# INCOMPATIBLE: Make Optional Field Required
# ========================================

# Version 1 (existing - phone optional)
POST /api/customers HTTP/1.1
{
  "name": "John Doe",
  "email": "john@example.com"
  # No phone - was acceptable
}

HTTP/1.1 201 Created

---

# Version 1.1 (phone now required) - BREAKS CLIENTS!
POST /api/customers HTTP/1.1
{
  "name": "Jane Smith",
  "email": "jane@example.com"
  # No phone
}

HTTP/1.1 400 Bad Request
{
  "error": "VALIDATION_ERROR",
  "message": "Phone is required"
}

# Old clients not sending phone now fail

# ✗ NOT backwards compatible

---

# SOLUTION: Add new required field to v2 only
# v1 - Phone optional
POST /api/v1/customers HTTP/1.1
{
  "name": "John Doe",
  "email": "john@example.com"
}

# v2 - Phone required
POST /api/v2/customers HTTP/1.1
{
  "name": "Jane Smith",
  "email": "jane@example.com",
  "phone": "+1-555-0123"  # Required in v2
}

---

# ========================================
# COMPATIBLE: Add New Enum Value
# ========================================

# Version 1 (existing order statuses)
{
  "orderId": "ORD-123",
  "status": "shipped"  # Possible: pending, paid, shipped, delivered
}

---

# Version 1.1 (add "cancelled" status)
{
  "orderId": "ORD-124",
  "status": "cancelled"  # New status
}

# Old clients must handle unknown enum values gracefully
# Good practice: Default/fallback for unknown values

# Client code:
switch (order.status) {
  case 'pending': // ...
  case 'paid': // ...
  case 'shipped': // ...
  case 'delivered': // ...
  default:
    // Handle unknown status (future-proof)
    console.log('Unknown status:', order.status);
}

# ✓ Backwards compatible (if clients handle unknowns)
# ✗ Breaks clients with exhaustive enum checks

---

# ========================================
# EXPANSION FIELDS PATTERN
# ========================================

# Allow clients to opt-in to new data

# Version 1 (minimal response)
GET /api/orders/ORD-123 HTTP/1.1

HTTP/1.1 200 OK
{
  "orderId": "ORD-123",
  "total": 299.99,
  "customerId": 456  # Just ID
}

---

# Version 1.1 (expand customer data on demand)
GET /api/orders/ORD-123?expand=customer HTTP/1.1

HTTP/1.1 200 OK
{
  "orderId": "ORD-123",
  "total": 299.99,
  "customerId": 456,
  "customer": {        # Expanded customer object
    "customerId": 456,
    "name": "John Doe",
    "email": "john@example.com"
  }
}

# Old clients (no expand) get minimal response
# New clients (with expand) get rich response

# ✓ Backwards compatible
# Stripe API uses this pattern extensively

---

# ========================================
# SUNSET HEADER PATTERN
# ========================================

# Communicate deprecation to clients

# Deprecated endpoint (still works)
GET /api/v1/customers HTTP/1.1

HTTP/1.1 200 OK
Deprecation: true
Sunset: Wed, 31 Dec 2026 23:59:59 GMT
Link: </api/v2/customers>; rel="successor-version"
Warning: 299 - "API v1 is deprecated and will be removed on Dec 31, 2026. Migrate to v2."

{
  "customers": [...]
}

# Clients can detect deprecation and plan migration
# Server can track usage of deprecated endpoints

---

# ========================================
# GRACEFUL DEGRADATION
# ========================================

# Handle missing/invalid data gracefully

# Server receives request from old client
POST /api/v2/orders HTTP/1.1
{
  "customerId": 456,
  "items": [...]
  # Missing new required field: "deliveryPreference"
}

# Instead of rejecting (breaking old clients):
HTTP/1.1 201 Created
{
  "orderId": "ORD-789",
  "deliveryPreference": "standard"  # Applied default
}

# ✓ Backwards compatible
# Server supplies sensible defaults for missing fields

---

# ========================================
# CONTENT NEGOTIATION
# ========================================

# Different response formats based on client preference

# Old client (expects v1 format)
GET /api/customers/123 HTTP/1.1
Accept: application/vnd.company.customer.v1+json

HTTP/1.1 200 OK
Content-Type: application/vnd.company.customer.v1+json
{
  "id": 123,
  "name": "John Doe"
}

---

# New client (requests v2 format)
GET /api/customers/123 HTTP/1.1
Accept: application/vnd.company.customer.v2+json

HTTP/1.1 200 OK
Content-Type: application/vnd.company.customer.v2+json
{
  "customerId": 123,
  "firstName": "John",
  "lastName": "Doe"
}

# Same endpoint, different representations
# ✓ Backwards compatible

---

# ========================================
# FEATURE FLAGS FOR GRADUAL ROLLOUT
# ========================================

# Gradually enable new behavior

GET /api/orders HTTP/1.1
X-Feature-Flag: use-new-pricing-logic

# Server checks flag:
if (request.hasFeatureFlag("use-new-pricing-logic")) {
    // Use new pricing calculation
} else {
    // Use legacy pricing calculation
}

# Enables:
# - A/B testing
# - Canary releases
# - Quick rollback if issues
# - Gradual migration

---

# ========================================
# DUAL-WRITE PATTERN (Data Migration)
# ========================================

# Migrating from old to new data structure

# Write to both old and new systems
POST /api/customers HTTP/1.1

# Server:
# 1. Write to legacy database (old schema)
# 2. Write to new database (new schema)
# 3. Return response

# Read from old or new based on client version
# Gradually switch readers to new system
# Once all readers migrated, stop dual-writes
```

**Backwards Compatibility Strategies:**

01. **Additive Changes Only** (safest)
   - Add new fields (optional)
   - Add new endpoints
   - Add new enum values
   - Never remove or rename

02. **Deprecation Period**
   - Mark fields/endpoints as deprecated
   - Support both old and new (6-12 months)
   - Communicate sunset date
   - Monitor usage of deprecated features
   - Remove in next major version

03. **API Versioning**
   - Major version for breaking changes
   - Maintain multiple versions simultaneously
   - Clear migration path
   - Eventually sunset old versions

04. **Feature Flags**
   - Toggle new behavior
   - Gradual rollout
   - Quick rollback
   - Per-client enablement

05. **Sensible Defaults**
   - Provide defaults for new required fields
   - Handle missing data gracefully
   - Don't break old clients

**Best Practices:**
* ✓ Design for extension from day one
* ✓ Accept unknown fields (forward compatibility)
* ✓ Make clients ignore unknown response fields
* ✓ Use optional fields for new additions
* ✓ Communicate changes clearly (changelogs, deprecation warnings)
* ✓ Monitor usage of deprecated features
* ✓ Test with real client versions
* ✓ Provide migration guides
* ✓ Version breaking changes
* ✓ Support multiple versions temporarily
* ✓ Use semantic versioning (major.minor.patch)

**When to Break Compatibility:**
* Security vulnerabilities
* Fundamental design flaws
* Performance issues (if benefits outweigh cost)
* When usage of old version is minimal
* With proper deprecation period and communication

---

**Batch 5 completed. Continuing with questions 41-50...**

---

### 41. How do you implement request/response compression in REST APIs?

**Request/response compression** reduces the size of data transmitted over the network by encoding it using algorithms like gzip or Brotli. This improves API performance, reduces bandwidth costs, and provides faster response times, especially for large payloads.

**Why it's needed:** Network bandwidth is often the bottleneck in API performance. Compression can reduce payload size by 70-90%, significantly improving response times for mobile clients, reducing data transfer costs, and allowing more concurrent requests on the same infrastructure.

**When to use:** Always enable compression for text-based responses (JSON, XML, HTML). Essential for mobile APIs, large response payloads, APIs with high traffic volume, and when optimizing for slow network conditions.

**Compression Comparison:**

| Feature | gzip | Brotli | Deflate | No Compression |
|---------|------|--------|---------|----------------|
| **Compression Ratio** | 70-80% | 75-85% (better) | 70-75% | 0% |
| **Speed** | Fast | Slower (better compression) | Fastest | N/A |
| **Browser Support** | Universal | Modern browsers | Universal | Universal |
| **Best For** | General use | Static assets | Legacy support | Small payloads |
| **CPU Usage** | Moderate | Higher | Low | None |

**Real-time Example:**

```http
# ========================================
# WITHOUT COMPRESSION
# ========================================

GET /api/products HTTP/1.1
Host: api.company.com

HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 524288  # 512 KB uncompressed

{
  "products": [
    {
      "productId": "PROD-001",
      "name": "Wireless Mouse with RGB Lighting and Ergonomic Design",
      "description": "Premium wireless mouse featuring...",
      "longDescription": "Detailed description with 500+ characters...",
      "specifications": {...},
      "reviews": [...]
    },
    // ... 100 more products
  ]
}

# Transfer time on 4G (10 Mbps): ~4 seconds

---

# ========================================
# WITH GZIP COMPRESSION
# ========================================

GET /api/products HTTP/1.1
Host: api.company.com
Accept-Encoding: gzip, deflate, br  # Client accepts compression

HTTP/1.1 200 OK
Content-Type: application/json
Content-Encoding: gzip              # Server compressed with gzip
Content-Length: 52428               # 51 KB (90% reduction!)
Vary: Accept-Encoding               # Cache separately per encoding

(gzip-compressed binary data)

# After decompression by client: Same 512 KB JSON
# Transfer time on 4G: ~0.4 seconds (10x faster!)

---

# ========================================
# COMPRESSION NEGOTIATION
# ========================================

# Client specifies preferred compression algorithms
GET /api/customers HTTP/1.1
Accept-Encoding: br, gzip, deflate, *;q=0.5

# Preference order:
# 1. Brotli (br) - best compression
# 2. gzip - good compression, universal
# 3. deflate - fallback
# 4. * (any) with quality 0.5

---

# Server chooses best supported algorithm
HTTP/1.1 200 OK
Content-Encoding: br               # Server chose Brotli
Content-Length: 45678
Vary: Accept-Encoding

(Brotli-compressed data)

---

# ========================================
# NO COMPRESSION FOR SMALL PAYLOADS
# ========================================

# Client requests compression
GET /api/health HTTP/1.1
Accept-Encoding: gzip

HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 42
# No Content-Encoding header = uncompressed

{"status":"healthy","version":"1.0"}

# Small response (42 bytes)
# Compression overhead > savings
# Server skips compression

---

# ========================================
# REQUEST BODY COMPRESSION
# ========================================

# Client compresses large POST request
POST /api/customers/bulk HTTP/1.1
Content-Type: application/json
Content-Encoding: gzip
Content-Length: 12345  # Compressed size

(gzip-compressed JSON with 1000 customers)

# Server decompresses and processes
HTTP/1.1 201 Created
{
  "imported": 1000,
  "status": "success"
}

---

# ========================================
# COMPRESSION CONFIGURATION (ASP.NET Core)
# ========================================

// Startup.cs or Program.cs
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;  # Enable for HTTPS
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    
    // MIME types to compress
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/json", "application/xml" }
    );
});

// Configure compression levels
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;  // Balance speed vs size
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

app.UseResponseCompression();  // Add middleware

---

# ========================================
# WHEN TO SKIP COMPRESSION
# ========================================

# 1. Already compressed content
GET /api/files/image.jpg HTTP/1.1
Accept-Encoding: gzip

HTTP/1.1 200 OK
Content-Type: image/jpeg
Content-Length: 245760
# No Content-Encoding (JPEG already compressed)

(binary JPEG data)

# Don't compress: images, videos, PDFs (already compressed)

---

# 2. Very small responses
GET /api/ping HTTP/1.1

HTTP/1.1 200 OK
Content-Length: 15
{"status":"ok"}

# Compression overhead > benefit for tiny payloads

---

# 3. Streaming responses
GET /api/logs/stream HTTP/1.1

HTTP/1.1 200 OK
Content-Type: text/event-stream
Transfer-Encoding: chunked

# Don't compress real-time streams

---

# ========================================
# COMPRESSION BEST PRACTICES
# ========================================

# ✓ Always include Vary header
HTTP/1.1 200 OK
Content-Encoding: gzip
Vary: Accept-Encoding  # Important for caching!

# Without Vary: Cached gzipped response served to non-gzip clients
# With Vary: Cache maintains separate entries per encoding
```

**Compression Best Practices:**
* ✓ Always enable for text-based content (JSON, XML, HTML, CSS, JavaScript)
* ✓ Use Brotli for better compression (if client supports)
* ✓ Fall back to gzip for universal support
* ✓ Include `Vary: Accept-Encoding` header for proper caching
* ✓ Skip compression for already-compressed formats (images, videos)
* ✓ Skip compression for small payloads (<1 KB)
* ✓ Use `CompressionLevel.Optimal` for balanced performance
* ✓ Enable HTTPS compression carefully (BREACH/CRIME vulnerability mitigation)
* ✓ Monitor CPU usage (compression has computational cost)
* ✓ Consider pre-compressing static assets at build time

---

### 42. What is Content Negotiation and how does it work?

**Content Negotiation** is the mechanism where a REST API serves different representations of the same resource based on client preferences. The client specifies what formats it can handle (JSON, XML, CSV, etc.), and the server responds with the most appropriate format.

**Why it's needed:** Different clients have different needs - web browsers prefer HTML, mobile apps prefer JSON, legacy systems may require XML. Content negotiation allows a single API endpoint to serve multiple client types without duplicating endpoints.

**When to use:** Implement content negotiation when supporting multiple client types, providing data in multiple formats, supporting different languages/locales, or building flexible APIs that must accommodate various consumers.

**Content Negotiation Types:**

| Type | Header | Purpose | Example |
|------|--------|---------|---------|
| **Media Type** | `Accept` | Response format | `application/json` , `application/xml` |
| **Language** | `Accept-Language` | Localization | `en-US` , `es-ES` , `fr-FR` |
| **Encoding** | `Accept-Encoding` | Compression | `gzip` , `br` , `deflate` |
| **Charset** | `Accept-Charset` | Character encoding | `utf-8` , `iso-8859-1` |
| **Custom** | `X-Format` , `?format=` | Query param override | `?format=csv` |

**Real-time Example:**

```http
# ========================================
# MEDIA TYPE NEGOTIATION (JSON vs XML)
# ========================================

# Client 1: Prefers JSON
GET /api/customers/123 HTTP/1.1
Accept: application/json

HTTP/1.1 200 OK
Content-Type: application/json

{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com"
}

---

# Client 2: Prefers XML
GET /api/customers/123 HTTP/1.1
Accept: application/xml

HTTP/1.1 200 OK
Content-Type: application/xml

<?xml version="1.0" encoding="UTF-8"?>
<customer>
  <customerId>123</customerId>
  <name>John Doe</name>
  <email>john@example.com</email>
</customer>

---

# Client 3: Accepts multiple formats (priority order)
GET /api/customers/123 HTTP/1.1
Accept: application/json, application/xml;q=0.8, */*;q=0.5

# Priority:
# 1. application/json (q=1.0 implicit)
# 2. application/xml (q=0.8)
# 3. Anything else (q=0.5)

HTTP/1.1 200 OK
Content-Type: application/json
# Server chose JSON (highest priority)

---

# ========================================
# LANGUAGE NEGOTIATION
# ========================================

# Client in Spain
GET /api/products/PROD-789 HTTP/1.1
Accept-Language: es-ES, es;q=0.9, en;q=0.5

HTTP/1.1 200 OK
Content-Type: application/json
Content-Language: es-ES

{
  "productId": "PROD-789",
  "name": "Ratón inalámbrico",
  "description": "Ratón inalámbrico con iluminación RGB",
  "price": 79.99,
  "currency": "EUR"
}

---

# Client in USA
GET /api/products/PROD-789 HTTP/1.1
Accept-Language: en-US

HTTP/1.1 200 OK
Content-Type: application/json
Content-Language: en-US

{
  "productId": "PROD-789",
  "name": "Wireless Mouse",
  "description": "Wireless mouse with RGB lighting",
  "price": 89.99,
  "currency": "USD"
}

---

# ========================================
# UNSUPPORTED MEDIA TYPE
# ========================================

# Client requests unsupported format
GET /api/customers/123 HTTP/1.1
Accept: application/pdf

HTTP/1.1 406 Not Acceptable
Content-Type: application/json

{
  "error": "NOT_ACCEPTABLE",
  "message": "Requested media type not supported",
  "supportedTypes": [
    "application/json",
    "application/xml",
    "text/csv"
  ]
}

---

# ========================================
# QUERY PARAMETER OVERRIDE
# ========================================

# Some APIs allow format override via query param
GET /api/customers/123?format=csv HTTP/1.1

HTTP/1.1 200 OK
Content-Type: text/csv
Content-Disposition: attachment; filename="customer-123.csv"

customerId,name,email
123,John Doe,john@example.com

---

# Also common:
GET /api/customers/123.xml HTTP/1.1  # Extension-based
GET /api/customers/123 HTTP/1.1
X-Response-Format: json              # Custom header

---

# ========================================
# CSV EXPORT EXAMPLE
# ========================================

GET /api/orders?format=csv HTTP/1.1
Accept: text/csv

HTTP/1.1 200 OK
Content-Type: text/csv
Content-Disposition: attachment; filename="orders-2026-01-23.csv"

orderId,customerId,total,status,createdAt
ORD-501,123,299.99,shipped,2026-01-20T10:00:00Z
ORD-502,456,149.99,delivered,2026-01-21T14:30:00Z
ORD-503,789,599.99,pending,2026-01-23T09:15:00Z

---

# ========================================
# API VERSIONING VIA CONTENT TYPE
# ========================================

# Version 1 request
GET /api/customers/123 HTTP/1.1
Accept: application/vnd.company.v1+json

HTTP/1.1 200 OK
Content-Type: application/vnd.company.v1+json

{
  "id": 123,
  "name": "John Doe"
}

---

# Version 2 request (different structure)
GET /api/customers/123 HTTP/1.1
Accept: application/vnd.company.v2+json

HTTP/1.1 200 OK
Content-Type: application/vnd.company.v2+json

{
  "customerId": 123,
  "firstName": "John",
  "lastName": "Doe",
  "fullName": "John Doe"
}

---

# ========================================
# ASP.NET CORE IMPLEMENTATION
# ========================================

// Configure content negotiation
builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;  // Honor Accept header
    options.ReturnHttpNotAcceptable = true;     // Return 406 if unsupported
})
.AddXmlSerializerFormatters()  // Add XML support
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
});

// Custom CSV formatter
public class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add("text/csv");
        SupportedEncodings.Add(Encoding.UTF8);
    }

    public override async Task WriteResponseBodyAsync(
        OutputFormatterWriteContext context, 
        Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var csv = ConvertToCsv(context.Object);
        await response.WriteAsync(csv, selectedEncoding);
    }
}

// Register custom formatter
builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Add(new CsvOutputFormatter());
});

---

# Controller with content negotiation
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    [HttpGet("{id}")]
    [Produces("application/json", "application/xml", "text/csv")]
    public ActionResult<Customer> GetCustomer(int id)
    {
        var customer = _service.GetCustomer(id);
        if (customer == null)
            return NotFound();
        
        // Framework automatically serializes based on Accept header
        return Ok(customer);
    }
}
```

**Content Negotiation Best Practices:**
* ✓ Support JSON as primary format (most common)
* ✓ Use standard MIME types (`application/json`,  `application/xml`)
* ✓ Return `406 Not Acceptable` for unsupported formats
* ✓ Include supported formats in error response
* ✓ Use quality values (q) for preference ordering
* ✓ Consider query parameter override for convenience
* ✓ Document all supported formats
* ✓ Set proper `Content-Type` header in response
* ✓ Use `Vary: Accept` header for caching
* ✓ Support language negotiation for international APIs

---

### 43. How do you implement bulk operations in REST APIs?

**Bulk operations** allow clients to perform multiple create, update, or delete actions in a single API request, reducing network overhead and improving performance. Instead of making 100 requests to create 100 resources, make 1 request with all data.

**Why it's needed:** Making individual requests for each operation is inefficient - high latency, network overhead, increased server load, and poor user experience. Bulk operations batch multiple actions, reducing round-trips from hundreds to one.

**When to use:** Implement bulk operations for data imports, batch processing, bulk updates/deletes, syncing operations, and any scenario where clients need to process multiple items efficiently.

**Bulk Operation Patterns:**

| Pattern | Method | Endpoint | Use Case |
|---------|--------|----------|----------|
| **Bulk Create** | POST | `/api/customers/bulk` | Import 1000 customers |
| **Bulk Update** | PATCH/PUT | `/api/customers/bulk` | Update prices for 500 products |
| **Bulk Delete** | DELETE | `/api/customers/bulk` | Delete 50 inactive accounts |
| **Batch Mixed** | POST | `/api/batch` | Mix of create/update/delete |
| **Transaction** | POST | `/api/transactions` | All-or-nothing atomic batch |

**Real-time Example:**

```http
# ========================================
# BULK CREATE (Batch Insert)
# ========================================

# INEFFICIENT: Individual requests
POST /api/customers HTTP/1.1
{"name": "Customer 1", "email": "c1@example.com"}

POST /api/customers HTTP/1.1
{"name": "Customer 2", "email": "c2@example.com"}

# ... 998 more requests
# Total: 1000 HTTP requests, ~30 seconds

---

# EFFICIENT: Bulk create
POST /api/customers/bulk HTTP/1.1
Content-Type: application/json

{
  "customers": [
    {"name": "Customer 1", "email": "c1@example.com"},
    {"name": "Customer 2", "email": "c2@example.com"},
    {"name": "Customer 3", "email": "c3@example.com"},
    // ... 997 more
  ]
}

HTTP/1.1 207 Multi-Status
Content-Type: application/json

{
  "totalRequested": 1000,
  "successful": 998,
  "failed": 2,
  "results": [
    {
      "index": 0,
      "status": "created",
      "customerId": "CUST-1001",
      "httpStatus": 201
    },
    {
      "index": 1,
      "status": "created",
      "customerId": "CUST-1002",
      "httpStatus": 201
    },
    {
      "index": 50,
      "status": "failed",
      "error": "DUPLICATE_EMAIL",
      "message": "Email already exists",
      "httpStatus": 409
    },
    {
      "index": 127,
      "status": "failed",
      "error": "VALIDATION_ERROR",
      "message": "Invalid email format",
      "httpStatus": 400
    }
    // ... remaining results
  ],
  "processingTimeMs": 2500  # 12x faster than individual requests
}

# Status code 207 Multi-Status: Some succeeded, some failed

---

# ========================================
# BULK UPDATE (Partial Updates)
# ========================================

PATCH /api/products/bulk HTTP/1.1
Content-Type: application/json

{
  "updates": [
    {
      "productId": "PROD-101",
      "changes": {
        "price": 99.99,
        "stock": 50
      }
    },
    {
      "productId": "PROD-102",
      "changes": {
        "price": 149.99
      }
    },
    {
      "productId": "PROD-103",
      "changes": {
        "discontinued": true,
        "stock": 0
      }
    }
  ]
}

HTTP/1.1 200 OK

{
  "updated": 3,
  "results": [
    {
      "productId": "PROD-101",
      "status": "success"
    },
    {
      "productId": "PROD-102",
      "status": "success"
    },
    {
      "productId": "PROD-103",
      "status": "success"
    }
  ]
}

---

# ========================================
# BULK DELETE
# ========================================

# Method 1: Request body with DELETE
DELETE /api/customers/bulk HTTP/1.1
Content-Type: application/json

{
  "customerIds": ["CUST-101", "CUST-102", "CUST-103", "CUST-104", "CUST-105"]
}

HTTP/1.1 200 OK

{
  "deleted": 5,
  "failed": 0,
  "results": [
    {"customerId": "CUST-101", "status": "deleted"},
    {"customerId": "CUST-102", "status": "deleted"},
    {"customerId": "CUST-103", "status": "deleted"},
    {"customerId": "CUST-104", "status": "deleted"},
    {"customerId": "CUST-105", "status": "deleted"}
  ]
}

---

# Method 2: Query parameters (for smaller batches)
DELETE /api/customers?ids=CUST-101,CUST-102,CUST-103 HTTP/1.1

---

# ========================================
# BATCH MIXED OPERATIONS
# ========================================

# Mix create, update, delete in single request
POST /api/batch HTTP/1.1
Content-Type: application/json

{
  "operations": [
    {
      "method": "POST",
      "path": "/api/customers",
      "body": {
        "name": "New Customer",
        "email": "new@example.com"
      }
    },
    {
      "method": "PATCH",
      "path": "/api/customers/123",
      "body": {
        "email": "updated@example.com"
      }
    },
    {
      "method": "DELETE",
      "path": "/api/customers/456"
    },
    {
      "method": "GET",
      "path": "/api/customers/789"
    }
  ]
}

HTTP/1.1 207 Multi-Status

{
  "responses": [
    {
      "status": 201,
      "body": {
        "customerId": "CUST-1001",
        "name": "New Customer"
      }
    },
    {
      "status": 200,
      "body": {
        "customerId": 123,
        "email": "updated@example.com"
      }
    },
    {
      "status": 204
    },
    {
      "status": 200,
      "body": {
        "customerId": 789,
        "name": "John Doe"
      }
    }
  ]
}

---

# ========================================
# TRANSACTIONAL BULK OPERATION
# ========================================

# All-or-nothing: Either all succeed or all rollback

POST /api/orders/bulk?atomic=true HTTP/1.1
Content-Type: application/json

{
  "orders": [
    {
      "customerId": 123,
      "items": [{"productId": "PROD-101", "quantity": 2}],
      "total": 199.98
    },
    {
      "customerId": 456,
      "items": [{"productId": "PROD-102", "quantity": 1}],
      "total": 149.99
    },
    {
      "customerId": 789,
      "items": [{"productId": "PROD-INVALID", "quantity": 1}],  # Invalid!
      "total": 99.99
    }
  ]
}

HTTP/1.1 400 Bad Request

{
  "error": "TRANSACTION_FAILED",
  "message": "All operations rolled back due to validation error in item 3",
  "failedOperation": {
    "index": 2,
    "reason": "Product PROD-INVALID not found"
  },
  "rollbackStatus": "completed"
}

# All 3 orders rolled back (none created)

---

# ========================================
# ASYNC BULK PROCESSING
# ========================================

# For very large batches, process asynchronously

POST /api/customers/bulk/async HTTP/1.1
Content-Type: application/json

{
  "customers": [
    ... 10,000 customers ...
  ]
}

HTTP/1.1 202 Accepted
Location: /api/jobs/JOB-abc123

{
  "jobId": "JOB-abc123",
  "status": "processing",
  "statusUrl": "/api/jobs/JOB-abc123",
  "estimatedCompletionTime": "2026-01-23T10:35:00Z"
}

---

# Check job status
GET /api/jobs/JOB-abc123 HTTP/1.1

HTTP/1.1 200 OK

{
  "jobId": "JOB-abc123",
  "status": "completed",
  "totalRequested": 10000,
  "successful": 9987,
  "failed": 13,
  "startedAt": "2026-01-23T10:30:00Z",
  "completedAt": "2026-01-23T10:34:22Z",
  "resultsUrl": "/api/jobs/JOB-abc123/results",
  "errorReportUrl": "/api/jobs/JOB-abc123/errors"
}

---

# Download results
GET /api/jobs/JOB-abc123/results HTTP/1.1

HTTP/1.1 200 OK
Content-Type: application/json

{
  "successful": [
    {"index": 0, "customerId": "CUST-1001"},
    {"index": 1, "customerId": "CUST-1002"},
    // ...
  ],
  "failed": [
    {
      "index": 50,
      "error": "DUPLICATE_EMAIL",
      "details": "Email already exists"
    }
  ]
}

---

# ========================================
# BULK OPERATION LIMITS
# ========================================

# Request exceeds limit
POST /api/customers/bulk HTTP/1.1

{
  "customers": [
    ... 5001 customers ...
  ]
}

HTTP/1.1 413 Payload Too Large

{
  "error": "BATCH_SIZE_EXCEEDED",
  "message": "Bulk operations limited to 5000 items per request",
  "requested": 5001,
  "maximum": 5000,
  "suggestion": "Split into multiple requests or use async endpoint"
}

---

# ========================================
# PARTIAL SUCCESS HANDLING
# ========================================

POST /api/orders/bulk HTTP/1.1

{
  "orders": [
    {"customerId": 123, "total": 99.99},   # Valid
    {"customerId": 999, "total": 49.99},   # Customer doesn't exist
    {"customerId": 456, "total": 149.99},  # Valid
    {"customerId": 789, "total": -10.00}   # Invalid total
  ]
}

HTTP/1.1 207 Multi-Status

{
  "successful": 2,
  "failed": 2,
  "results": [
    {
      "index": 0,
      "status": "created",
      "orderId": "ORD-501",
      "httpStatus": 201
    },
    {
      "index": 1,
      "status": "failed",
      "error": "CUSTOMER_NOT_FOUND",
      "httpStatus": 404
    },
    {
      "index": 2,
      "status": "created",
      "orderId": "ORD-502",
      "httpStatus": 201
    },
    {
      "index": 3,
      "status": "failed",
      "error": "INVALID_TOTAL",
      "message": "Total must be positive",
      "httpStatus": 400
    }
  ]
}

# Client can:
# - Retry only failed items (index 1 and 3)
# - Display success/failure breakdown to user
```

**Bulk Operation Best Practices:**
* ✓ Use `207 Multi-Status` for partial success
* ✓ Return detailed results for each item (index, status, error)
* ✓ Set reasonable batch size limits (e.g., 1000-5000 items)
* ✓ Use async processing for very large batches
* ✓ Provide progress tracking for long-running operations
* ✓ Support atomic transactions when needed (all-or-nothing)
* ✓ Validate entire batch before processing (fail-fast)
* ✓ Include index in error responses for client correlation
* ✓ Implement proper database transactions
* ✓ Consider chunking/streaming for huge datasets
* ✓ Return `413 Payload Too Large` if limits exceeded
* ✓ Document batch size limits clearly
* ✓ Provide downloadable error reports for large failures

---

### 44. What are Long Polling, Server-Sent Events (SSE), and WebSockets?

**Long Polling, SSE, and WebSockets** are techniques for real-time communication between client and server. Unlike traditional request/response where the client always initiates, these allow the server to push data to clients, enabling live updates, notifications, and bidirectional communication.

**Why it's needed:** Traditional HTTP polling is inefficient for real-time data - wastes bandwidth, increases latency, and creates unnecessary server load. These techniques provide efficient real-time communication for chat apps, live dashboards, notifications, and collaborative tools.

**When to use:** Long Polling for basic real-time needs with broad compatibility, SSE for server-to-client streams (stock tickers, notifications), and WebSockets for bidirectional real-time communication (chat, gaming, collaborative editing).

**Comparison:**

| Feature | Regular Polling | Long Polling | Server-Sent Events (SSE) | WebSockets |
|---------|----------------|--------------|--------------------------|------------|
| **Direction** | Client → Server | Client → Server | Server → Client | Bidirectional |
| **Protocol** | HTTP | HTTP | HTTP | WebSocket (ws://) |
| **Connection** | Short-lived | Long-lived | Long-lived | Long-lived |
| **Efficiency** | Low (wasteful) | Medium | High | Very High |
| **Browser Support** | Universal | Universal | Modern browsers | Modern browsers |
| **Use Case** | Simple updates | Basic real-time | Live feeds, notifications | Chat, gaming, collaboration |
| **Complexity** | Low | Medium | Medium | High |

**Real-time Example:**

```http
# ========================================
# 1. REGULAR POLLING (Inefficient)
# ========================================

# Client polls every 5 seconds
GET /api/notifications HTTP/1.1

HTTP/1.1 200 OK
{
  "notifications": []  # No new notifications
}

# Wait 5 seconds...

GET /api/notifications HTTP/1.1

HTTP/1.1 200 OK
{
  "notifications": []  # Still none
}

# Wait 5 seconds...

GET /api/notifications HTTP/1.1

HTTP/1.1 200 OK
{
  "notifications": [
    {"id": 1, "message": "New order received"}
  ]
}

# Problems:
# - Wasted requests when no data
# - Delay (up to 5 seconds) before receiving update
# - High server load (many clients × requests/second)

---

# ========================================
# 2. LONG POLLING (Better)
# ========================================

# Client makes request
GET /api/notifications/long-poll HTTP/1.1

# Server holds connection open until data available
# If no data for 30 seconds, server responds:

HTTP/1.1 204 No Content
# Connection closes, client immediately reconnects

---

# If data becomes available:
GET /api/notifications/long-poll HTTP/1.1

# Server waits... (connection open)
# New notification arrives!

HTTP/1.1 200 OK
{
  "notifications": [
    {"id": 1, "message": "New order received", "timestamp": "2026-01-23T10:30:00Z"}
  ]
}

# Connection closes
# Client processes data and immediately reconnects

# Benefits:
# ✓ Near-instant updates (no polling delay)
# ✓ Less wasted requests
# ✓ Still uses HTTP (works through proxies/firewalls)

---

# Server-side pseudocode (Long Polling):
app.get('/api/notifications/long-poll', async (req, res) => {
    const timeout = 30000;  // 30 seconds
    const startTime = Date.now();
    
    // Check for new notifications every 1 second
    const interval = setInterval(async () => {
        const notifications = await getNewNotifications(req.userId);
        
        if (notifications.length > 0) {
            clearInterval(interval);
            res.json({ notifications });
        } else if (Date.now() - startTime > timeout) {
            clearInterval(interval);
            res.status(204).send();  // Timeout, no data
        }
    }, 1000);
});

---

# ========================================
# 3. SERVER-SENT EVENTS (SSE)
# ========================================

# Client establishes SSE connection
GET /api/notifications/stream HTTP/1.1
Accept: text/event-stream

HTTP/1.1 200 OK
Content-Type: text/event-stream
Cache-Control: no-cache
Connection: keep-alive

# Server sends events as they occur:

# Event 1:
id: 1
event: notification
data: {"message": "New order received", "orderId": "ORD-501"}

# Event 2:
id: 2
event: notification
data: {"message": "Payment confirmed", "orderId": "ORD-501"}

# Event 3:
id: 3
event: status
data: {"status": "connected", "clients": 1523}

# Event 4:
id: 4
event: notification
data: {"message": "Order shipped", "orderId": "ORD-501"}

# Connection stays open indefinitely
# Server pushes data as it becomes available

---

# Client-side (JavaScript):
const eventSource = new EventSource('/api/notifications/stream');

// Listen for "notification" events
eventSource.addEventListener('notification', (event) => {
    const data = JSON.parse(event.data);
    console.log('New notification:', data.message);
    showNotification(data);
});

// Listen for "status" events
eventSource.addEventListener('status', (event) => {
    const data = JSON.parse(event.data);
    console.log('Status update:', data.status);
});

// Handle errors
eventSource.onerror = (error) => {
    console.error('SSE error:', error);
    eventSource.close();
};

---

# Server-side (Node.js/Express):
app.get('/api/notifications/stream', (req, res) => {
    res.setHeader('Content-Type', 'text/event-stream');
    res.setHeader('Cache-Control', 'no-cache');
    res.setHeader('Connection', 'keep-alive');
    
    // Send initial connection event
    res.write('event: status\n');
    res.write('data: {"status":"connected"}\n\n');
    
    // Subscribe to notification events
    const listener = (notification) => {
        res.write(`id: ${notification.id}\n`);
        res.write('event: notification\n');
        res.write(`data: ${JSON.stringify(notification)}\n\n`);
    };
    
    notificationEmitter.on('new', listener);
    
    // Cleanup on client disconnect
    req.on('close', () => {
        notificationEmitter.off('new', listener);
    });
});

---

# SSE with automatic reconnection:
GET /api/notifications/stream HTTP/1.1
Last-Event-ID: 42  # Resume from event 42

# Server sends events starting from ID > 42

# Benefits:
# ✓ Server-to-client streaming
# ✓ Automatic reconnection
# ✓ Event IDs for resuming
# ✓ Multiple event types
# ✓ Built-in browser support

---

# ========================================
# 4. WEBSOCKETS (Full Duplex)
# ========================================

# Client initiates WebSocket handshake
GET /api/chat HTTP/1.1
Host: chat.company.com
Upgrade: websocket
Connection: Upgrade
Sec-WebSocket-Key: dGhlIHNhbXBsZSBub25jZQ==
Sec-WebSocket-Version: 13

# Server accepts upgrade
HTTP/1.1 101 Switching Protocols
Upgrade: websocket
Connection: Upgrade
Sec-WebSocket-Accept: s3pPLMBiTxaQ9kYGzzhZRbK+xOo=

# Connection upgraded to WebSocket protocol
# Now bidirectional communication:

---

# Client → Server: Send message
{
  "type": "message",
  "content": "Hello, everyone!",
  "chatId": "CHAT-123"
}

# Server → Client: Message delivered confirmation
{
  "type": "ack",
  "messageId": "MSG-789",
  "status": "delivered"
}

# Server → All Clients: Broadcast message
{
  "type": "message",
  "messageId": "MSG-789",
  "from": "John Doe",
  "content": "Hello, everyone!",
  "timestamp": "2026-01-23T10:30:00Z"
}

# Client → Server: Typing indicator
{
  "type": "typing",
  "chatId": "CHAT-123",
  "userId": 123
}

# Server → Other Clients: User typing
{
  "type": "typing",
  "user": "John Doe"
}

---

# Client-side (JavaScript):
const ws = new WebSocket('wss://chat.company.com/api/chat');

// Connection opened
ws.onopen = () => {
    console.log('WebSocket connected');
    
    // Send authentication
    ws.send(JSON.stringify({
        type: 'auth',
        token: 'Bearer token123'
    }));
};

// Receive messages
ws.onmessage = (event) => {
    const data = JSON.parse(event.data);
    
    if (data.type === 'message') {
        displayMessage(data);
    } else if (data.type === 'typing') {
        showTypingIndicator(data.user);
    }
};

// Send message
function sendMessage(content) {
    ws.send(JSON.stringify({
        type: 'message',
        content: content
    }));
}

// Handle close
ws.onclose = () => {
    console.log('WebSocket disconnected');
    // Attempt reconnection
};

---

# Server-side (Node.js with ws library):
const WebSocket = require('ws');
const wss = new WebSocket.Server({ port: 8080 });

const clients = new Map();

wss.on('connection', (ws, req) => {
    console.log('New client connected');
    
    ws.on('message', (message) => {
        const data = JSON.parse(message);
        
        if (data.type === 'auth') {
            // Authenticate and store client
            const userId = authenticateToken(data.token);
            clients.set(userId, ws);
        } else if (data.type === 'message') {
            // Broadcast to all clients
            const messageData = {
                type: 'message',
                from: data.from,
                content: data.content,
                timestamp: new Date().toISOString()
            };
            
            wss.clients.forEach(client => {
                if (client.readyState === WebSocket.OPEN) {
                    client.send(JSON.stringify(messageData));
                }
            });
        }
    });
    
    ws.on('close', () => {
        console.log('Client disconnected');
        // Remove from clients map
    });
});

---

# ========================================
# COMPARISON USE CASES
# ========================================

# Long Polling: Basic notifications
# - Order status updates
# - Simple chat (low message frequency)
# - Fallback for WebSocket

# SSE: Server-to-client streams
# - Stock price ticker
# - Live sports scores
# - Server logs streaming
# - News feed updates
# - Social media notifications

# WebSockets: Real-time bidirectional
# - Chat applications
# - Online gaming
# - Collaborative editing (Google Docs-style)
# - Video conferencing signaling
# - Live trading platforms
# - IoT device control

---

# ========================================
# WEBSOCKET HEARTBEAT (Keep-Alive)
# ========================================

# Prevent connection timeout with periodic ping/pong

# Client → Server: Ping
{
  "type": "ping",
  "timestamp": "2026-01-23T10:30:00Z"
}

# Server → Client: Pong
{
  "type": "pong",
  "timestamp": "2026-01-23T10:30:00Z"
}

# If no pong received within timeout → reconnect

// Client-side heartbeat:
setInterval(() => {
    if (ws.readyState === WebSocket.OPEN) {
        ws.send(JSON.stringify({ type: 'ping' }));
    }
}, 30000);  // Every 30 seconds
```

**When to Use Which:**

| Scenario | Best Choice | Why |
|----------|-------------|-----|
| Live stock prices | SSE | Server pushes updates, client doesn't send |
| Chat application | WebSocket | Bidirectional, high frequency |
| Order status notifications | Long Polling or SSE | Infrequent updates, server-to-client |
| Collaborative editing | WebSocket | Real-time bidirectional, low latency |
| Social media feed | SSE | Server pushes new posts |
| Online gaming | WebSocket | Low latency, bidirectional |
| Admin dashboard metrics | SSE | Server streams metrics |

**Best Practices:**
* ✓ Use HTTPS/WSS (secure connections)
* ✓ Implement authentication before streaming
* ✓ Add heartbeat/ping-pong for connection health
* ✓ Handle reconnection gracefully (exponential backoff)
* ✓ Implement message queuing for offline clients
* ✓ Limit message size and rate
* ✓ Clean up resources on disconnect
* ✓ Provide fallback (WebSocket → SSE → Long Polling)
* ✓ Monitor active connections
* ✓ Use load balancers with sticky sessions (or Redis for pub/sub)

---

### 45. How do you handle API timeouts and retries?

**API timeouts and retries** are mechanisms to handle transient failures, network issues, and temporary service unavailability. Timeouts prevent indefinite waiting, while retries automatically reattempt failed requests, improving reliability and user experience.

**Why it's needed:** Networks are unreliable - packets get lost, servers temporarily fail, services restart. Without timeouts, clients hang indefinitely. Without retries, transient failures cause permanent errors. Proper timeout and retry strategies make APIs resilient.

**When to use:** Implement timeouts for all API calls. Use retries for idempotent operations (GET, PUT, DELETE) and transient errors (network timeouts, 503 Service Unavailable, 429 Too Many Requests). Avoid retrying non-idempotent operations (POST) without idempotency keys.

**Timeout Types:**

| Timeout Type | Description | Typical Value | Example |
|--------------|-------------|---------------|---------|
| **Connection Timeout** | Time to establish connection | 5-10 seconds | Can't reach server |
| **Request Timeout** | Time for complete request/response | 30-60 seconds | Slow query |
| **Read Timeout** | Time waiting for response data | 30 seconds | Server processing |
| **Idle Timeout** | Inactivity before closing connection | 60 seconds | Long-lived connections |

**Real-time Example:**

```http
# ========================================
# TIMEOUT SCENARIOS
# ========================================

# 1. Connection Timeout
GET /api/products HTTP/1.1
Host: api.company.com

# Client can't establish TCP connection within 10 seconds
# Error: Connection timeout

Client Error: {
  "error": "CONNECTION_TIMEOUT",
  "message": "Unable to connect to server",
  "timeout": 10000,
  "retry": true
}

---

# 2. Request Timeout
GET /api/reports/generate?year=2025 HTTP/1.1

# Server receives request but processing takes too long
# Client timeout after 30 seconds

HTTP/1.1 504 Gateway Timeout
{
  "error": "REQUEST_TIMEOUT",
  "message": "Request exceeded maximum processing time",
  "timeout": 30000,
  "suggestion": "Use async endpoint for long-running operations"
}

---

# 3. Read Timeout
GET /api/customers HTTP/1.1

# Connection established
# Server started sending response
# Network interruption - no data received for 30 seconds

Client Error: {
  "error": "READ_TIMEOUT",
  "message": "Server stopped sending data",
  "timeout": 30000,
  "bytesReceived": 45678,
  "retry": true
}

---

# ========================================
# RETRY STRATEGIES
# ========================================

# 1. IMMEDIATE RETRY (Not Recommended)
GET /api/orders HTTP/1.1
# Fails immediately

GET /api/orders HTTP/1.1
# Retry immediately
# Fails again (server still down)

# Problem: Hammers failing server, wastes resources

---

# 2. FIXED DELAY RETRY
GET /api/orders HTTP/1.1
# Fails

# Wait 2 seconds...

GET /api/orders HTTP/1.1
# Retry after fixed delay
# Still fails

# Wait 2 seconds...

GET /api/orders HTTP/1.1
# Retry again

# Problem: Predictable load pattern, thundering herd

---

# 3. EXPONENTIAL BACKOFF (Recommended)
GET /api/orders HTTP/1.1
# Attempt 1: Fails

# Wait 1 second...

GET /api/orders HTTP/1.1
# Attempt 2: Fails

# Wait 2 seconds (2^1)...

GET /api/orders HTTP/1.1
# Attempt 3: Fails

# Wait 4 seconds (2^2)...

GET /api/orders HTTP/1.1
# Attempt 4: Fails

# Wait 8 seconds (2^3)...

GET /api/orders HTTP/1.1
# Attempt 5: Success!

# Benefits:
# ✓ Gives server time to recover
# ✓ Reduces load on failing service
# ✓ Increases success probability

---

# 4. EXPONENTIAL BACKOFF WITH JITTER (Best)
# Add randomness to prevent thundering herd

Attempt 1: Fail → Wait 1s
Attempt 2: Fail → Wait 2s + random(0-1s) = 2.7s
Attempt 3: Fail → Wait 4s + random(0-2s) = 5.3s
Attempt 4: Fail → Wait 8s + random(0-4s) = 10.2s
Attempt 5: Success

# Jitter prevents multiple clients retrying simultaneously

---

# ========================================
# RETRY DECISION LOGIC
# ========================================

# RETRY: Transient errors (likely to succeed on retry)
GET /api/products HTTP/1.1

HTTP/1.1 503 Service Unavailable
Retry-After: 60
# Server temporarily unavailable → RETRY

---

HTTP/1.1 429 Too Many Requests
Retry-After: 30
# Rate limit exceeded → RETRY after delay

---

HTTP/1.1 504 Gateway Timeout
# Timeout → RETRY (idempotent request)

---

HTTP/1.1 500 Internal Server Error
# Server error (might be transient) → RETRY (limited attempts)

---

# DON'T RETRY: Permanent errors
HTTP/1.1 400 Bad Request
# Client error (bad data) → DON'T RETRY

---

HTTP/1.1 401 Unauthorized
# Authentication failed → DON'T RETRY (fix auth first)

---

HTTP/1.1 404 Not Found
# Resource doesn't exist → DON'T RETRY

---

HTTP/1.1 409 Conflict
# Business logic error → DON'T RETRY

---

# ========================================
# CLIENT-SIDE IMPLEMENTATION (JavaScript)
# ========================================

// Retry with exponential backoff
async function fetchWithRetry(url, options = {}, maxRetries = 3) {
    let attempt = 0;
    
    while (attempt < maxRetries) {
        try {
            const controller = new AbortController();
            const timeoutId = setTimeout(() => controller.abort(), 30000);  // 30s timeout
            
            const response = await fetch(url, {
                ...options,
                signal: controller.signal
            });
            
            clearTimeout(timeoutId);
            
            // Check if should retry
            if (response.ok) {
                return response;
            }
            
            // Retry on specific status codes
            if ([408, 429, 500, 502, 503, 504].includes(response.status)) {
                attempt++;
                
                if (attempt < maxRetries) {
                    // Exponential backoff with jitter
                    const baseDelay = Math.pow(2, attempt) * 1000;  // 2s, 4s, 8s
                    const jitter = Math.random() * 1000;
                    const delay = baseDelay + jitter;
                    
                    console.log(`Retry attempt ${attempt} after ${delay}ms`);
                    await new Promise(resolve => setTimeout(resolve, delay));
                    continue;
                }
            }
            
            // Don't retry permanent errors
            throw new Error(`HTTP ${response.status}: ${response.statusText}`);
            
        } catch (error) {
            if (error.name === 'AbortError') {
                console.error('Request timeout');
                attempt++;
                
                if (attempt < maxRetries) {
                    const delay = Math.pow(2, attempt) * 1000;
                    await new Promise(resolve => setTimeout(resolve, delay));
                    continue;
                }
            }
            
            throw error;
        }
    }
    
    throw new Error(`Max retries (${maxRetries}) exceeded`);
}

// Usage:
try {
    const response = await fetchWithRetry('/api/orders', {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer token123'
        }
    });
    
    const data = await response.json();
    console.log('Success:', data);
    
} catch (error) {
    console.error('Failed after retries:', error);
}

---

# ========================================
# SERVER-SIDE: Polly (C#/.NET)
# ========================================

using Polly;
using Polly.Timeout;

// Define retry policy with exponential backoff
var retryPolicy = Policy
    .Handle<HttpRequestException>()
    .Or<TimeoutRejectedException>()
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt => 
            TimeSpan.FromSeconds(Math.Pow(2, attempt)) + 
            TimeSpan.FromMilliseconds(Random.Shared.Next(0, 1000)),  // Jitter
        onRetry: (exception, timeSpan, attempt, context) =>
        {
            _logger.LogWarning($"Retry {attempt} after {timeSpan.TotalSeconds}s due to {exception.Message}");
        }
    );

// Define timeout policy
var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(
    TimeSpan.FromSeconds(30),
    TimeoutStrategy.Pessimistic
);

// Combine policies
var combinedPolicy = Policy.WrapAsync(retryPolicy, timeoutPolicy);

// Execute with retry and timeout
var response = await combinedPolicy.ExecuteAsync(async () =>
{
    return await _httpClient.GetAsync("https://api.external.com/data");
});

---

# ========================================
# CIRCUIT BREAKER + RETRY
# ========================================

# Prevent retry storm when service is down

var circuitBreakerPolicy = Policy
    .Handle<HttpRequestException>()
    .CircuitBreakerAsync(
        handledEventsAllowedBeforeBreaking: 3,  // Open after 3 failures
        durationOfBreak: TimeSpan.FromMinutes(1)  // Stay open for 1 minute
    );

var retryPolicy = Policy
    .Handle<HttpRequestException>()
    .WaitAndRetryAsync(2, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

// Retry first, then circuit breaker
var policy = Policy.WrapAsync(circuitBreakerPolicy, retryPolicy);

try
{
    await policy.ExecuteAsync(async () =>
    {
        return await _httpClient.GetAsync("/api/orders");
    });
}
catch (BrokenCircuitException)
{
    // Circuit is open - don't even try
    return StatusCode(503, "Service temporarily unavailable");
}

---

# ========================================
# RETRY-AFTER HEADER
# ========================================

# Server tells client when to retry

GET /api/orders HTTP/1.1

HTTP/1.1 429 Too Many Requests
Retry-After: 60  # Retry after 60 seconds

{
  "error": "RATE_LIMIT_EXCEEDED",
  "retryAfter": 60
}

---

# Alternative: Absolute time
HTTP/1.1 503 Service Unavailable
Retry-After: Wed, 23 Jan 2026 10:35:00 GMT

---

# Client respects Retry-After:
async function fetchWithRetryAfter(url) {
    const response = await fetch(url);
    
    if (response.status === 429 || response.status === 503) {
        const retryAfter = response.headers.get('Retry-After');
        
        if (retryAfter) {
            const delay = isNaN(retryAfter) 
                ? new Date(retryAfter) - Date.now()
                : parseInt(retryAfter) * 1000;
            
            console.log(`Waiting ${delay}ms before retry`);
            await new Promise(resolve => setTimeout(resolve, delay));
            
            return fetchWithRetryAfter(url);  // Retry
        }
    }
    
    return response;
}

---

# ========================================
# IDEMPOTENCY FOR SAFE RETRIES
# ========================================

# POST is NOT idempotent - retry can create duplicates

# WITHOUT idempotency key:
POST /api/orders HTTP/1.1
{
  "customerId": 123,
  "total": 299.99
}

# Timeout! Was it created? Retry...

POST /api/orders HTTP/1.1
{
  "customerId": 123,
  "total": 299.99
}

# Result: 2 orders created (duplicate!)

---

# WITH idempotency key:
POST /api/orders HTTP/1.1
Idempotency-Key: 550e8400-e29b-41d4-a716-446655440000

{
  "customerId": 123,
  "total": 299.99
}

# Timeout! Retry with SAME key...

POST /api/orders HTTP/1.1
Idempotency-Key: 550e8400-e29b-41d4-a716-446655440000

{
  "customerId": 123,
  "total": 299.99
}

# Server detects duplicate key → Returns original response
# Only 1 order created (safe to retry)
```

**Retry Best Practices:**
* ✓ Use exponential backoff with jitter
* ✓ Set maximum retry attempts (3-5)
* ✓ Respect `Retry-After` header
* ✓ Only retry idempotent operations (or use idempotency keys)
* ✓ Only retry transient errors (503, 504, 429, network timeouts)
* ✓ Don't retry client errors (4xx except 408, 429)
* ✓ Implement circuit breaker to prevent retry storms
* ✓ Log retry attempts for debugging
* ✓ Set reasonable timeouts (connection, request, read)
* ✓ Use timeout strategy appropriate for operation type
* ✓ Provide fallback behavior when all retries fail
* ✓ Consider bulkhead pattern to isolate failures

**Timeout Configuration Examples:**
* **Fast APIs**: 5-10 seconds
* **Standard APIs**: 30 seconds
* **Long-running operations**: 60+ seconds (or use async)
* **Connection timeout**: 5-10 seconds
* **Mobile apps**: Longer timeouts (poor connectivity)

* **Long-running operations**: 60+ seconds (or use async)
* **Connection timeout**: 5-10 seconds
* **Mobile apps**: Longer timeouts (poor connectivity)

---

### 46. What is the Circuit Breaker pattern and when should you use it?

**Circuit Breaker** is a design pattern that prevents an application from repeatedly trying to execute an operation that's likely to fail. Like an electrical circuit breaker, it "trips" after detecting failures, allowing the system to recover instead of cascading failures.

**Why it's needed:** When a dependent service fails, continuously retrying wastes resources, increases latency, and can cause cascading failures across the system. Circuit breakers detect failures quickly, fail fast, and give failing services time to recover.

**When to use:** Implement circuit breakers when calling external APIs, microservices, databases, or any remote resource that can fail. Essential for resilient distributed systems, preventing cascading failures, and improving overall system stability.

**Circuit Breaker States:**

| State | Behavior | Transitions To | Description |
|-------|----------|----------------|-------------|
| **Closed** | Normal operation, requests pass through | Open (after threshold failures) | Everything working |
| **Open** | Fail fast, reject requests immediately | Half-Open (after timeout) | Service is down |
| **Half-Open** | Test if service recovered | Closed (success) or Open (failure) | Testing recovery |

**Real-time Example:**

```http
# ========================================
# CIRCUIT BREAKER STATES
# ========================================

# STATE 1: CLOSED (Normal Operation)
# All requests pass through to service

GET /api/orders HTTP/1.1

# Circuit Breaker: CLOSED
# Forward request to Order Service

→ Order Service: GET /orders
← Order Service: 200 OK

HTTP/1.1 200 OK
{
  "orders": [...]
}

# Success counter: 1000 (all successful)
# Circuit remains CLOSED

---

# STATE 2: FAILURES START
GET /api/orders HTTP/1.1

# Circuit Breaker: CLOSED
→ Order Service: GET /orders
← Order Service: 500 Internal Server Error

HTTP/1.1 500 Internal Server Error

# Failure counter: 1/5

---

GET /api/orders HTTP/1.1
→ Order Service: Timeout
HTTP/1.1 504 Gateway Timeout

# Failure counter: 2/5

---

GET /api/orders HTTP/1.1
→ Order Service: Connection refused
HTTP/1.1 503 Service Unavailable

# Failure counter: 3/5

---

# STATE 3: CIRCUIT OPENS
GET /api/orders HTTP/1.1
→ Order Service: Timeout
# Failure counter: 5/5 → THRESHOLD EXCEEDED

# Circuit Breaker: CLOSED → OPEN

HTTP/1.1 503 Service Unavailable
{
  "error": "CIRCUIT_BREAKER_OPEN",
  "message": "Order service is temporarily unavailable",
  "retryAfter": 60
}

---

# All subsequent requests fail fast (no call to Order Service)
GET /api/orders HTTP/1.1

# Circuit Breaker: OPEN
# Don't even try calling Order Service
# Fail immediately

HTTP/1.1 503 Service Unavailable
{
  "error": "CIRCUIT_BREAKER_OPEN",
  "message": "Order service is temporarily unavailable",
  "retryAfter": 45
}

# Benefits:
# ✓ No wasted resources calling failing service
# ✓ Fast failure response
# ✓ Gives Order Service time to recover

---

# STATE 4: HALF-OPEN (Testing Recovery)
# After 60 seconds timeout, circuit transitions to HALF-OPEN

GET /api/orders HTTP/1.1

# Circuit Breaker: OPEN → HALF-OPEN
# Allow one test request through

→ Order Service: GET /orders
← Order Service: 200 OK

HTTP/1.1 200 OK
{
  "orders": [...]
}

# SUCCESS! Circuit Breaker: HALF-OPEN → CLOSED
# Resume normal operation

---

# If test request fails:
GET /api/orders HTTP/1.1

# Circuit Breaker: HALF-OPEN
→ Order Service: GET /orders
← Order Service: 500 Internal Server Error

# FAILURE! Circuit Breaker: HALF-OPEN → OPEN
# Reset timeout, try again in 60 seconds

---

# ========================================
# C# IMPLEMENTATION WITH POLLY
# ========================================

using Polly;
using Polly.CircuitBreaker;

// Configure Circuit Breaker
var circuitBreakerPolicy = Policy
    .Handle<HttpRequestException>()
    .Or<TimeoutException>()
    .CircuitBreakerAsync(
        handledEventsAllowedBeforeBreaking: 5,  // Open after 5 failures
        durationOfBreak: TimeSpan.FromSeconds(60),  // Stay open for 60 seconds
        onBreak: (exception, duration) =>
        {
            _logger.LogWarning($"Circuit breaker opened due to {exception.Message}. Breaking for {duration.TotalSeconds}s");
        },
        onReset: () =>
        {
            _logger.LogInformation("Circuit breaker closed. Resuming normal operation.");
        },
        onHalfOpen: () =>
        {
            _logger.LogInformation("Circuit breaker half-open. Testing service recovery.");
        }
    );

// Use circuit breaker
try
{
    var response = await circuitBreakerPolicy.ExecuteAsync(async () =>
    {
        return await _httpClient.GetAsync("https://api.orders.com/orders");
    });
    
    return Ok(await response.Content.ReadAsStringAsync());
}
catch (BrokenCircuitException)
{
    // Circuit is open
    return StatusCode(503, new
    {
        error = "CIRCUIT_BREAKER_OPEN",
        message = "Order service is temporarily unavailable",
        retryAfter = 60
    });
}
catch (HttpRequestException ex)
{
    return StatusCode(503, new { error = ex.Message });
}

---

# ========================================
# ADVANCED CIRCUIT BREAKER
# ========================================

// Advanced circuit breaker with percentage-based threshold
var advancedCircuitBreaker = Policy
    .Handle<HttpRequestException>()
    .AdvancedCircuitBreakerAsync(
        failureThreshold: 0.5,  // Open if 50% of requests fail
        samplingDuration: TimeSpan.FromSeconds(30),  // In 30-second window
        minimumThroughput: 10,  // At least 10 requests in window
        durationOfBreak: TimeSpan.FromSeconds(60)
    );

// Example:
// Window: 30 seconds
// Requests: 20 total
// Failures: 11 (55%)
// Result: Circuit opens (55% > 50% threshold)

---

# ========================================
# CIRCUIT BREAKER + FALLBACK
# ========================================

// Provide fallback when circuit is open
var fallbackPolicy = Policy<HttpResponseMessage>
    .Handle<BrokenCircuitException>()
    .FallbackAsync(
        fallbackAction: (cancellationToken) =>
        {
            // Return cached data or default response
            var fallbackResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(new
                {
                    orders = GetCachedOrders(),  // Cached data
                    message = "Data from cache (service unavailable)"
                }))
            };
            return Task.FromResult(fallbackResponse);
        },
        onFallbackAsync: (response, context) =>
        {
            _logger.LogWarning("Circuit breaker open, returning fallback response");
            return Task.CompletedTask;
        }
    );

var circuitBreakerPolicy = /* ... */;

// Combine policies: Circuit Breaker → Fallback
var policy = Policy.WrapAsync(fallbackPolicy, circuitBreakerPolicy);

var response = await policy.ExecuteAsync(async () =>
{
    return await _httpClient.GetAsync("https://api.orders.com/orders");
});

---

# ========================================
# CIRCUIT BREAKER MONITORING
# ========================================

# Expose circuit breaker status
GET /api/health/circuit-breakers HTTP/1.1

HTTP/1.1 200 OK
{
  "circuitBreakers": [
    {
      "name": "OrderService",
      "state": "Closed",
      "failureCount": 0,
      "successCount": 1234,
      "lastFailure": null
    },
    {
      "name": "PaymentService",
      "state": "Open",
      "failureCount": 5,
      "successCount": 890,
      "lastFailure": "2026-01-23T10:30:00Z",
      "reopenAt": "2026-01-23T10:31:00Z"
    },
    {
      "name": "InventoryService",
      "state": "HalfOpen",
      "failureCount": 3,
      "successCount": 567,
      "lastFailure": "2026-01-23T10:29:30Z"
    }
  ]
}

---

# ========================================
# MULTIPLE CIRCUIT BREAKERS
# ========================================

// Different circuit breakers for different services
public class ServiceClients
{
    private readonly AsyncCircuitBreakerPolicy _orderServiceBreaker;
    private readonly AsyncCircuitBreakerPolicy _paymentServiceBreaker;
    private readonly AsyncCircuitBreakerPolicy _inventoryServiceBreaker;
    
    public ServiceClients()
    {
        // Order Service: More tolerant (critical service)
        _orderServiceBreaker = Policy
            .Handle<HttpRequestException>()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 10,
                durationOfBreak: TimeSpan.FromMinutes(2)
            );
        
        // Payment Service: Less tolerant (must be reliable)
        _paymentServiceBreaker = Policy
            .Handle<HttpRequestException>()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 3,
                durationOfBreak: TimeSpan.FromMinutes(5)
            );
        
        // Inventory Service: Moderate tolerance
        _inventoryServiceBreaker = Policy
            .Handle<HttpRequestException>()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromMinutes(1)
            );
    }
}
```

**Circuit Breaker Best Practices:**
* ✓ Set appropriate failure threshold (3-10 failures)
* ✓ Configure reasonable timeout before testing recovery (30-120 seconds)
* ✓ Use different thresholds for different services (critical vs non-critical)
* ✓ Combine with retry policy (retry first, then circuit breaker)
* ✓ Provide fallback responses (cached data, default values)
* ✓ Log state transitions for monitoring
* ✓ Expose circuit breaker status via health checks
* ✓ Use percentage-based thresholds for high-traffic services
* ✓ Consider bulkhead pattern to isolate failures
* ✓ Test circuit breaker behavior in staging

**When to Use Circuit Breaker:**
* ✓ Calling external APIs (third-party services)
* ✓ Microservice communication
* ✓ Database connections
* ✓ Any remote resource that can fail
* ✓ Preventing cascading failures
* ✓ Improving system resilience

---

### 47. How do you implement API request validation?

**API request validation** ensures that incoming requests contain valid, well-formed data before processing. It prevents invalid data from reaching business logic, protects against security vulnerabilities, and provides clear error messages to clients.

**Why it's needed:** Invalid data causes application errors, security vulnerabilities (SQL injection, XSS), data corruption, and poor user experience. Validation at the API layer catches issues early, provides immediate feedback, and maintains data integrity.

**When to use:** Validate ALL incoming requests. Check data types, required fields, format (email, phone), length constraints, value ranges, business rules, and prevent malicious input.

**Validation Types:**

| Type | Examples | Purpose |
|------|----------|---------|
| **Required Fields** | Name, email must be present | Ensure mandatory data |
| **Data Type** | Age must be number | Correct data types |
| **Format** | Email, phone, URL, date format | Valid structure |
| **Length** | Min 8 chars, max 255 chars | Constraint enforcement |
| **Range** | Age 18-100, quantity > 0 | Value boundaries |
| **Pattern** | Regex for postal code, credit card | Complex formats |
| **Business Rules** | End date > start date | Domain logic |
| **Sanitization** | Remove HTML, SQL chars | Security |

**Real-time Example:**

```http
# ========================================
# VALIDATION EXAMPLES
# ========================================

# 1. MISSING REQUIRED FIELDS
POST /api/customers HTTP/1.1
Content-Type: application/json

{
  "email": "john@example.com"
  # Missing required field: name
}

HTTP/1.1 400 Bad Request
{
  "error": "VALIDATION_ERROR",
  "message": "Request validation failed",
  "details": [
    {
      "field": "name",
      "message": "Name is required",
      "code": "REQUIRED_FIELD"
    }
  ]
}

---

# 2. INVALID DATA TYPE
POST /api/products HTTP/1.1

{
  "name": "Wireless Mouse",
  "price": "ninety-nine dollars",  # Should be number
  "stock": 50
}

HTTP/1.1 400 Bad Request
{
  "error": "VALIDATION_ERROR",
  "details": [
    {
      "field": "price",
      "message": "Price must be a number",
      "rejectedValue": "ninety-nine dollars",
      "expectedType": "number"
    }
  ]
}

---

# 3. INVALID FORMAT
POST /api/customers HTTP/1.1

{
  "name": "John Doe",
  "email": "not-an-email",  # Invalid email format
  "phone": "12345"  # Invalid phone format
}

HTTP/1.1 400 Bad Request
{
  "error": "VALIDATION_ERROR",
  "details": [
    {
      "field": "email",
      "message": "Email must be in valid format (e.g., user@example.com)",
      "rejectedValue": "not-an-email",
      "pattern": "^[\\w\\.-]+@[\\w\\.-]+\\.\\w+$"
    },
    {
      "field": "phone",
      "message": "Phone must be in format: +1-555-0123",
      "rejectedValue": "12345",
      "pattern": "^\\+\\d{1,3}-\\d{3}-\\d{4}$"
    }
  ]
}

---

# 4. LENGTH CONSTRAINTS
POST /api/customers HTTP/1.1

{
  "name": "Jo",  # Too short
  "email": "john@example.com",
  "password": "12345"  # Too short
}

HTTP/1.1 400 Bad Request
{
  "error": "VALIDATION_ERROR",
  "details": [
    {
      "field": "name",
      "message": "Name must be between 3 and 100 characters",
      "rejectedValue": "Jo",
      "minLength": 3,
      "maxLength": 100,
      "actualLength": 2
    },
    {
      "field": "password",
      "message": "Password must be at least 8 characters",
      "minLength": 8,
      "actualLength": 5
    }
  ]
}

---

# 5. RANGE VALIDATION
POST /api/orders HTTP/1.1

{
  "customerId": 123,
  "quantity": -5,  # Negative quantity
  "discount": 150  # Discount > 100%
}

HTTP/1.1 400 Bad Request
{
  "error": "VALIDATION_ERROR",
  "details": [
    {
      "field": "quantity",
      "message": "Quantity must be greater than 0",
      "rejectedValue": -5,
      "minimum": 1
    },
    {
      "field": "discount",
      "message": "Discount must be between 0 and 100",
      "rejectedValue": 150,
      "minimum": 0,
      "maximum": 100
    }
  ]
}

---

# 6. BUSINESS RULE VALIDATION
POST /api/bookings HTTP/1.1

{
  "eventId": "EVT-123",
  "startDate": "2026-02-01",
  "endDate": "2026-01-15"  # End before start!
}

HTTP/1.1 400 Bad Request
{
  "error": "VALIDATION_ERROR",
  "details": [
    {
      "field": "endDate",
      "message": "End date must be after start date",
      "rejectedValue": "2026-01-15",
      "comparison": "startDate",
      "comparisonValue": "2026-02-01"
    }
  ]
}

---

# 7. NESTED OBJECT VALIDATION
POST /api/orders HTTP/1.1

{
  "customerId": 123,
  "items": [
    {
      "productId": "PROD-101",
      "quantity": 2,
      "price": 99.99
    },
    {
      "productId": "",  # Invalid
      "quantity": -1,  # Invalid
      "price": 0  # Invalid
    }
  ]
}

HTTP/1.1 400 Bad Request
{
  "error": "VALIDATION_ERROR",
  "details": [
    {
      "field": "items[1].productId",
      "message": "Product ID is required",
      "rejectedValue": ""
    },
    {
      "field": "items[1].quantity",
      "message": "Quantity must be at least 1",
      "rejectedValue": -1
    },
    {
      "field": "items[1].price",
      "message": "Price must be greater than 0",
      "rejectedValue": 0
    }
  ]
}

---

# ========================================
# ASP.NET CORE VALIDATION
# ========================================

// Model with Data Annotations
public class CreateCustomerRequest
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 3, 
        ErrorMessage = "Name must be between 3 and 100 characters")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }
    
    [Phone(ErrorMessage = "Invalid phone format")]
    public string Phone { get; set; }
    
    [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
    public int Age { get; set; }
    
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$",
        ErrorMessage = "Password must be at least 8 characters with uppercase, lowercase, and digit")]
    public string Password { get; set; }
}

// Controller automatically validates
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        // ModelState.IsValid automatically checked by [ApiController]
        // If invalid, returns 400 with validation errors
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        // Proceed with valid data
        var customer = _service.CreateCustomer(request);
        return Created($"/api/customers/{customer.Id}", customer);
    }
}

---

# Custom validation attribute
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime date)
        {
            if (date <= DateTime.UtcNow)
            {
                return new ValidationResult("Date must be in the future");
            }
        }
        
        return ValidationResult.Success;
    }
}

// Usage:
public class CreateBookingRequest
{
    [Required]
    [FutureDate]
    public DateTime StartDate { get; set; }
}

---

# FluentValidation (more powerful)
using FluentValidation;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MustAsync(async (email, cancellation) =>
            {
                return !await _repository.EmailExistsAsync(email);
            }).WithMessage("Email already exists");
        
        RuleFor(x => x.Age)
            .InclusiveBetween(18, 100).WithMessage("Age must be between 18 and 100");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(@"[A-Z]").WithMessage("Password must contain uppercase")
            .Matches(@"[a-z]").WithMessage("Password must contain lowercase")
            .Matches(@"\d").WithMessage("Password must contain digit");
    }
}

// Register in Program.cs:
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();
builder.Services.AddFluentValidationAutoValidation();

---

# ========================================
# SANITIZATION (Prevent XSS, SQL Injection)
# ========================================

POST /api/comments HTTP/1.1

{
  "content": "<script>alert('XSS')</script>Hello world"
}

# Before saving, sanitize:
public string SanitizeInput(string input)
{
    // Remove HTML tags
    var sanitized = Regex.Replace(input, "<.*?>", string.Empty);
    
    // Encode special characters
    sanitized = HttpUtility.HtmlEncode(sanitized);
    
    return sanitized;
}

# Stored as: "Hello world" (script removed)

---

# SQL Injection prevention (use parameterized queries):
// ✗ VULNERABLE:
var query = $"SELECT * FROM Users WHERE Email = '{email}'";
// Attacker sends: ' OR '1'='1

// ✓ SAFE:
var query = "SELECT * FROM Users WHERE Email = @email";
command.Parameters.AddWithValue("@email", email);

---

# ========================================
# VALIDATION MIDDLEWARE
# ========================================

// Global validation error handler
public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            
            var response = new
            {
                error = "VALIDATION_ERROR",
                message = "Request validation failed",
                details = ex.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                    rejectedValue = e.AttemptedValue
                })
            };
            
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

// Register middleware:
app.UseMiddleware<ValidationExceptionMiddleware>();
```

**Validation Best Practices:**
* ✓ Validate all incoming requests
* ✓ Return detailed error messages (field, reason, rejected value)
* ✓ Use HTTP 400 Bad Request for validation errors
* ✓ Validate at API boundary (don't trust clients)
* ✓ Use declarative validation (Data Annotations, FluentValidation)
* ✓ Validate data types, formats, lengths, ranges
* ✓ Implement business rule validation
* ✓ Sanitize input to prevent XSS and SQL injection
* ✓ Use parameterized queries for database operations
* ✓ Provide consistent error response format
* ✓ Include field names in error messages
* ✓ Validate nested objects and arrays
* ✓ Use async validation for database checks (email uniqueness)
* ✓ Document validation rules in API documentation
* ✓ Test validation with invalid data

---

### 48. What are conditional requests (If-Match, If-None-Match, ETag)?

**Conditional requests** use HTTP headers to make requests conditional on the current state of a resource. They enable efficient caching, prevent lost updates in concurrent scenarios, and reduce bandwidth by avoiding unnecessary data transfer.

**Why it's needed:** Without conditional requests, clients waste bandwidth downloading unchanged resources, and concurrent updates cause data loss (last-write-wins). ETags and conditional headers solve these problems with optimistic locking and efficient caching.

**When to use:** Implement ETags for all GET requests (caching), use If-Match for PUT/PATCH (prevent lost updates), use If-None-Match for conditional creates, and leverage If-Modified-Since for time-based caching.

**Conditional Headers:**

| Header | Used With | Purpose | Response |
|--------|-----------|---------|----------|
| **ETag** | Response | Resource version identifier | `ETag: "abc123"` |
| **If-Match** | PUT/PATCH/DELETE | Proceed only if ETag matches | 412 if mismatch |
| **If-None-Match** | GET/POST | Proceed only if ETag doesn't match | 304 if matches |
| **If-Modified-Since** | GET | Return only if modified after date | 304 if not modified |
| **If-Unmodified-Since** | PUT/PATCH | Proceed only if not modified | 412 if modified |

**Real-time Example:**

```http
# ========================================
# ETAG FOR CACHING (GET Requests)
# ========================================

# First request - Get customer
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
ETag: "version-1"
Last-Modified: Wed, 22 Jan 2026 10:00:00 GMT
Cache-Control: max-age=3600

{
  "customerId": 123,
  "name": "John Doe",
  "email": "john@example.com"
}

# Client stores ETag: "version-1"

---

# Subsequent request - Check if changed
GET /api/customers/123 HTTP/1.1
If-None-Match: "version-1"

# Server checks: Current ETag == "version-1"?
# Yes → Resource unchanged

HTTP/1.1 304 Not Modified
ETag: "version-1"
# No body sent (saves bandwidth!)

# Client uses cached version

---

# Resource was modified
GET /api/customers/123 HTTP/1.1
If-None-Match: "version-1"

# Server checks: Current ETag == "version-1"?
# No → Resource changed (now "version-2")

HTTP/1.1 200 OK
ETag: "version-2"
Last-Modified: Wed, 23 Jan 2026 09:00:00 GMT

{
  "customerId": 123,
  "name": "John Doe",
  "email": "john.doe@example.com",  # Email changed
  "phone": "+1-555-0123"  # Phone added
}

# Client updates cache with new ETag

---

# ========================================
# IF-MATCH FOR OPTIMISTIC LOCKING (PUT/PATCH)
# ========================================

# User A gets customer
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
ETag: "version-1"
{
  "customerId": 123,
  "name": "John Doe",
  "balance": 100.00
}

---

# User B gets customer (same version)
GET /api/customers/123 HTTP/1.1

HTTP/1.1 200 OK
ETag: "version-1"
{
  "customerId": 123,
  "name": "John Doe",
  "balance": 100.00
}

---

# User A updates balance (with If-Match)
PUT /api/customers/123 HTTP/1.1
If-Match: "version-1"
Content-Type: application/json

{
  "customerId": 123,
  "name": "John Doe",
  "balance": 150.00  # Add $50
}

# Server checks: Current ETag == "version-1"?
# Yes → Update succeeds

HTTP/1.1 200 OK
ETag: "version-2"  # New version
{
  "customerId": 123,
  "name": "John Doe",
  "balance": 150.00
}

---

# User B tries to update (with stale ETag)
PUT /api/customers/123 HTTP/1.1
If-Match: "version-1"  # Stale!
Content-Type: application/json

{
  "customerId": 123,
  "name": "John Doe",
  "balance": 200.00  # Add $100
}

# Server checks: Current ETag == "version-1"?
# No! Current is "version-2"

HTTP/1.1 412 Precondition Failed
ETag: "version-2"
{
  "error": "PRECONDITION_FAILED",
  "message": "Resource has been modified by another user",
  "currentVersion": "version-2",
  "requestedVersion": "version-1",
  "action": "Please refresh and try again"
}

# User B must:
# 1. Re-fetch customer (get version-2)
# 2. Apply changes to latest data
# 3. Retry with new ETag

---

# ========================================
# IF-NONE-MATCH FOR CONDITIONAL CREATE
# ========================================

# Prevent duplicate creation

POST /api/customers HTTP/1.1
If-None-Match: *  # Only create if doesn't exist
Content-Type: application/json

{
  "email": "new@example.com",
  "name": "New Customer"
}

# Server checks: Does customer with this email exist?
# No → Create

HTTP/1.1 201 Created
ETag: "version-1"
Location: /api/customers/456

{
  "customerId": 456,
  "email": "new@example.com",
  "name": "New Customer"
}

---

# Retry (network timeout scenario)
POST /api/customers HTTP/1.1
If-None-Match: *
Content-Type: application/json

{
  "email": "new@example.com",
  "name": "New Customer"
}

# Customer already exists!

HTTP/1.1 412 Precondition Failed
{
  "error": "ALREADY_EXISTS",
  "message": "Customer with this email already exists",
  "existingResource": "/api/customers/456"
}

---

# ========================================
# IF-MODIFIED-SINCE (Time-based Caching)
# ========================================

# First request
GET /api/products HTTP/1.1

HTTP/1.1 200 OK
Last-Modified: Wed, 22 Jan 2026 10:00:00 GMT
{
  "products": [...]
}

---

# Subsequent request
GET /api/products HTTP/1.1
If-Modified-Since: Wed, 22 Jan 2026 10:00:00 GMT

# Server checks: Modified after Wed, 22 Jan 2026 10:00:00 GMT?
# No → Not modified

HTTP/1.1 304 Not Modified
Last-Modified: Wed, 22 Jan 2026 10:00:00 GMT

---

# Products updated
GET /api/products HTTP/1.1
If-Modified-Since: Wed, 22 Jan 2026 10:00:00 GMT

# Server checks: Modified after Wed, 22 Jan 2026 10:00:00 GMT?
# Yes → Return new data

HTTP/1.1 200 OK
Last-Modified: Wed, 23 Jan 2026 11:30:00 GMT
{
  "products": [...]  # Updated list
}

---

# ========================================
# ETAG GENERATION STRATEGIES
# ========================================

// 1. Hash-based ETag (content hash)
public string GenerateETag(object data)
{
    var json = JsonSerializer.Serialize(data);
    var hash = SHA256.HashData(Encoding.UTF8.GetBytes(json));
    return $"\"{Convert.ToHexString(hash)}\"";
}

// Example: ETag: "A3F5B8C2D1E4..."

---

// 2. Version-based ETag (database version)
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [Timestamp]  // Concurrency token
    public byte[] RowVersion { get; set; }
}

public string GenerateETag(Customer customer)
{
    return $"\"{Convert.ToBase64String(customer.RowVersion)}\"";
}

// Example: ETag: "AAAAAAAAB9E="

---

// 3. Last-Modified timestamp
public string GenerateETag(DateTime lastModified)
{
    return $"\"{lastModified.Ticks}\"";
}

---

// 4. Strong vs Weak ETags
// Strong ETag: "abc123" (byte-for-byte identical)
// Weak ETag: W/"abc123" (semantically equivalent)

HTTP/1.1 200 OK
ETag: "abc123"  # Strong
# vs
ETag: W/"abc123"  # Weak

---

# ========================================
# ASP.NET CORE IMPLEMENTATION
# ========================================

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetCustomer(int id)
    {
        var customer = _repository.GetById(id);
        if (customer == null)
            return NotFound();
        
        // Generate ETag
        var etag = GenerateETag(customer);
        
        // Check If-None-Match
        if (Request.Headers.IfNoneMatch.Contains(etag))
        {
            return StatusCode(304);  // Not Modified
        }
        
        // Return with ETag
        Response.Headers.ETag = etag;
        return Ok(customer);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(int id, [FromBody] UpdateCustomerRequest request)
    {
        var customer = _repository.GetById(id);
        if (customer == null)
            return NotFound();
        
        var currentETag = GenerateETag(customer);
        
        // Check If-Match (optimistic locking)
        if (!Request.Headers.IfMatch.Contains(currentETag))
        {
            return StatusCode(412, new
            {
                error = "PRECONDITION_FAILED",
                message = "Resource has been modified",
                currentVersion = currentETag
            });
        }
        
        // Update customer
        customer.Name = request.Name;
        customer.Email = request.Email;
        _repository.Update(customer);
        
        // Return new ETag
        var newETag = GenerateETag(customer);
        Response.Headers.ETag = newETag;
        
        return Ok(customer);
    }
}
```

**Conditional Request Best Practices:**
* ✓ Always include ETag in GET responses
* ✓ Support If-None-Match for efficient caching
* ✓ Use If-Match for PUT/PATCH to prevent lost updates
* ✓ Return 304 Not Modified when resource unchanged
* ✓ Return 412 Precondition Failed when ETag mismatch
* ✓ Include current ETag in 412 response
* ✓ Use strong ETags for precise matching
* ✓ Consider weak ETags for gzip-compressed responses
* ✓ Support both ETag and Last-Modified
* ✓ Generate ETags efficiently (hash, version number)
* ✓ Use database row version for concurrency control
* ✓ Document ETag behavior in API documentation
* ✓ Test concurrent update scenarios

**Benefits:**
* ✓ Prevents lost updates (optimistic locking)
* ✓ Reduces bandwidth (304 Not Modified)
* ✓ Improves cache efficiency
* ✓ No need for distributed locks
* ✓ Better user experience (fast responses)
* ✓ Scales horizontally (stateless)

---

### 49. How do you handle API performance optimization?

**API performance optimization** involves techniques to reduce response times, increase throughput, and efficiently handle load. It includes caching, database optimization, asynchronous processing, connection pooling, compression, and infrastructure scaling.

**Why it's needed:** Slow APIs frustrate users, waste resources, increase costs, and can cause cascading failures. Performance optimization improves user experience, reduces infrastructure costs, increases scalability, and maintains reliability under load.

**When to use:** Optimize from design phase (proper architecture), during development (efficient code), and continuously monitor/improve in production. Focus on bottlenecks identified through profiling and monitoring.

**Performance Optimization Techniques:**

| Technique | Impact | Complexity | When to Use |
|-----------|--------|------------|-------------|
| **Caching** | High | Low | Frequently accessed data |
| **Database Indexing** | High | Medium | Slow queries |
| **Connection Pooling** | Medium | Low | Database/HTTP connections |
| **Async Processing** | High | Medium | Long-running operations |
| **Compression** | Medium | Low | Large responses |
| **Pagination** | High | Low | Large datasets |
| **Query Optimization** | High | Medium | N+1 queries, joins |
| **CDN** | High | Low | Static assets, global users |
| **Load Balancing** | High | Medium | High traffic |
| **Response Streaming** | Medium | High | Large files |

**Real-time Example:**

```http
# ========================================
# 1. CACHING (Biggest Impact)
# ========================================

# WITHOUT CACHING (Slow)
GET /api/products HTTP/1.1

# Every request hits database
# Query time: 250ms
# Response time: 300ms

HTTP/1.1 200 OK
{
  "products": [... 100 products ...]
}

---

# WITH CACHING (Fast)
GET /api/products HTTP/1.1

# First request: Cache miss
# Hit database, cache result
# Response time: 300ms

HTTP/1.1 200 OK
Cache-Control: public, max-age=3600
ETag: "abc123"
{
  "products": [... 100 products ...]
}

---

# Subsequent requests: Cache hit
GET /api/products HTTP/1.1
If-None-Match: "abc123"

# Check cache: HIT
# No database query
# Response time: 5ms (60x faster!)

HTTP/1.1 304 Not Modified
ETag: "abc123"

---

# Cache implementation (Redis)
public class ProductService
{
    private readonly IDistributedCache _cache;
    private readonly IProductRepository _repository;
    
    public async Task<List<Product>> GetProductsAsync()
    {
        const string cacheKey = "products:all";
        
        // Try cache first
        var cached = await _cache.GetStringAsync(cacheKey);
        if (cached != null)
        {
            return JsonSerializer.Deserialize<List<Product>>(cached);
        }
        
        // Cache miss - query database
        var products = await _repository.GetAllAsync();
        
        // Store in cache (1 hour)
        await _cache.SetStringAsync(cacheKey, 
            JsonSerializer.Serialize(products),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });
        
        return products;
    }
}

---

# ========================================
# 2. DATABASE OPTIMIZATION
# ========================================

# PROBLEM: N+1 Query
GET /api/orders HTTP/1.1

# Naive implementation:
// 1 query for orders
var orders = await _context.Orders.ToListAsync();  # Query 1

foreach (var order in orders)
{
    // N queries for customers
    order.Customer = await _context.Customers
        .FindAsync(order.CustomerId);  # Query 2, 3, 4, ...
}

# Result: 1 + 100 = 101 queries!
# Response time: 5000ms (very slow)

---

# SOLUTION: Eager Loading
GET /api/orders HTTP/1.1

// 1 query with JOIN
var orders = await _context.Orders
    .Include(o => o.Customer)      # LEFT JOIN Customers
    .Include(o => o.Items)          # LEFT JOIN OrderItems
        .ThenInclude(i => i.Product) # LEFT JOIN Products
    .ToListAsync();

# Result: 1 query with JOINs
# Response time: 250ms (20x faster!)

---

# SOLUTION: Indexing
# Slow query:
SELECT * FROM Orders WHERE CustomerId = 123;
# No index on CustomerId → Full table scan
# Time: 2000ms

# Create index:
CREATE INDEX IX_Orders_CustomerId ON Orders(CustomerId);

# Fast query:
SELECT * FROM Orders WHERE CustomerId = 123;
# Uses index → Direct lookup
# Time: 5ms (400x faster!)

---

# ========================================
# 3. PAGINATION (Essential)
# ========================================

# WITHOUT PAGINATION (Bad)
GET /api/products HTTP/1.1

SELECT * FROM Products;  # 50,000 products!
# Response size: 15 MB
# Response time: 8000ms
# Memory: 250 MB
# User sees: Loading...

---

# WITH PAGINATION (Good)
GET /api/products?page=1&pageSize=20 HTTP/1.1

SELECT * FROM Products 
ORDER BY ProductId 
OFFSET 0 ROWS FETCH NEXT 20 ROWS ONLY;

HTTP/1.1 200 OK
{
  "data": [... 20 products ...],
  "pagination": {
    "page": 1,
    "pageSize": 20,
    "totalPages": 2500,
    "totalCount": 50000
  }
}

# Response size: 15 KB (1000x smaller!)
# Response time: 50ms (160x faster!)
# Memory: 1 MB

---

# ========================================
# 4. ASYNC PROCESSING
# ========================================

# SYNCHRONOUS (Blocking)
POST /api/reports/generate HTTP/1.1

{
  "reportType": "annual",
  "year": 2025
}

# Server blocks thread for 60 seconds
# Thread pool exhausted under load
# Other requests queue/timeout

HTTP/1.1 200 OK (after 60 seconds)
{
  "reportUrl": "https://cdn.company.com/reports/annual-2025.pdf"
}

---

# ASYNCHRONOUS (Non-blocking)
POST /api/reports/generate HTTP/1.1

{
  "reportType": "annual",
  "year": 2025
}

# Server queues job, returns immediately
# Background worker processes report

HTTP/1.1 202 Accepted (immediate)
Location: /api/reports/jobs/JOB-123

{
  "jobId": "JOB-123",
  "status": "processing",
  "statusUrl": "/api/reports/jobs/JOB-123"
}

# Client polls status:
GET /api/reports/jobs/JOB-123 HTTP/1.1

HTTP/1.1 200 OK
{
  "jobId": "JOB-123",
  "status": "completed",
  "reportUrl": "https://cdn.company.com/reports/annual-2025.pdf"
}

---

# ========================================
# 5. CONNECTION POOLING
# ========================================

# WITHOUT POOLING (Slow)
# Open connection: 100ms
# Execute query: 10ms
# Close connection: 50ms
# Total: 160ms per request

---

# WITH POOLING (Fast)
# Get connection from pool: 1ms
# Execute query: 10ms
# Return to pool: 1ms
# Total: 12ms per request (13x faster!)

# Configuration (ASP.NET Core):
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.MaxBatchSize(100);
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);
    }));

// HttpClient pooling:
services.AddHttpClient<IExternalService, ExternalService>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));

---

# ========================================
# 6. COMPRESSION (Response Size)
# ========================================

# WITHOUT COMPRESSION
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 524288  # 512 KB

{
  "products": [...]
}

# Transfer time on 4G: 4 seconds

---

# WITH COMPRESSION
HTTP/1.1 200 OK
Content-Type: application/json
Content-Encoding: gzip
Content-Length: 52428  # 51 KB (90% reduction!)

(gzip-compressed data)

# Transfer time on 4G: 0.4 seconds (10x faster!)

---

# ========================================
# 7. SELECT SPECIFIC FIELDS
# ========================================

# BAD: Select all fields
SELECT * FROM Products;
# Returns 50 columns including BLOB data
# Response: 10 MB

---

# GOOD: Select only needed fields
SELECT ProductId, Name, Price, Stock FROM Products;
# Returns 4 columns
# Response: 500 KB (20x smaller!)

// EF Core projection:
var products = await _context.Products
    .Select(p => new ProductListDto
    {
        Id = p.Id,
        Name = p.Name,
        Price = p.Price,
        Stock = p.Stock
    })
    .ToListAsync();

---

# ========================================
# 8. PARALLEL PROCESSING
# ========================================

# SEQUENTIAL (Slow)
var customer = await GetCustomerAsync(customerId);     # 100ms
var orders = await GetOrdersAsync(customerId);         # 150ms
var payments = await GetPaymentsAsync(customerId);     # 120ms

# Total: 370ms

---

# PARALLEL (Fast)
var tasks = new[]
{
    GetCustomerAsync(customerId),
    GetOrdersAsync(customerId),
    GetPaymentsAsync(customerId)
};

await Task.WhenAll(tasks);

var customer = tasks[0].Result;
var orders = tasks[1].Result;
var payments = tasks[2].Result;

# Total: 150ms (bottleneck is slowest task)
# 2.5x faster!

---

# ========================================
# 9. IMPLEMENT CQRS (Read/Write Separation)
# ========================================

# Write API (normalized, transactional)
POST /api/orders HTTP/1.1

# Writes to transactional database
# Updates: Orders, OrderItems, Inventory

---

# Read API (denormalized, optimized for reads)
GET /api/orders/ORD-501 HTTP/1.1

# Reads from read replica or cache
# Pre-joined, denormalized data
# No complex JOINs at read time

HTTP/1.1 200 OK
{
  "orderId": "ORD-501",
  "customerName": "John Doe",  # Denormalized
  "items": [...],  # Pre-joined
  "total": 299.99
}

---

# ========================================
# 10. LOAD BALANCING & SCALING
# ========================================

# Horizontal scaling (multiple instances)
#
#                 Load Balancer
#                      |
#         +------------+------------+
#         |            |            |
#     API Server   API Server   API Server
#     Instance 1   Instance 2   Instance 3
#         |            |            |
#         +------------+------------+
#                      |
#                  Database
#
# Benefits:
# ✓ Handle more concurrent requests
# ✓ No single point of failure
# ✓ Better response times

---

# ========================================
# PERFORMANCE METRICS
# ========================================

# BEFORE OPTIMIZATION:
GET /api/orders HTTP/1.1

# Response time: 2500ms
# Database queries: 101 (N+1 problem)
# Database time: 2200ms
# Memory: 150 MB
# Throughput: 20 req/sec

---

# AFTER OPTIMIZATION:
GET /api/orders HTTP/1.1

# Response time: 85ms (29x faster!)
# Database queries: 1 (eager loading)
# Database time: 50ms
# Memory: 5 MB (30x less)
# Throughput: 500 req/sec (25x more)

# Optimizations applied:
# ✓ Caching (Redis)
# ✓ Database indexing
# ✓ Eager loading (no N+1)
# ✓ Pagination
# ✓ Compression (gzip)
# ✓ Connection pooling
# ✓ Async processing
# ✓ CDN for static assets
```

**Performance Optimization Checklist:**
* [ ] Implement caching (Redis, in-memory)
* [ ] Add database indexes on frequently queried columns
* [ ] Use eager loading to prevent N+1 queries
* [ ] Implement pagination for large datasets
* [ ] Enable response compression (gzip/Brotli)
* [ ] Use connection pooling (database, HTTP)
* [ ] Process long operations asynchronously
* [ ] Select only required fields (no SELECT *)
* [ ] Implement CDN for static assets
* [ ] Use load balancing for horizontal scaling
* [ ] Optimize database queries (execution plans)
* [ ] Implement read replicas for read-heavy workloads
* [ ] Use denormalized read models (CQRS)
* [ ] Batch database operations
* [ ] Use parallel processing where appropriate
* [ ] Monitor performance with APM tools
* [ ] Set up alerting for slow queries
* [ ] Regularly review and optimize bottlenecks

**Monitoring Tools:**
* **APM**: Application Insights, New Relic, Datadog
* **Database**: SQL Server Profiler, pgAdmin, MongoDB Compass
* **Caching**: Redis CLI, Redis Insight
* **Load Testing**: k6, JMeter, Apache Bench, Gatling

---

### 50. What are the best practices for REST API logging and debugging?

**API logging and debugging** involves capturing request/response data, errors, performance metrics, and trace information to troubleshoot issues, monitor behavior, and maintain system health. Good logging is essential for production support and incident response.

**Why it's needed:** Without proper logging, debugging production issues is impossible. Logs provide visibility into what happened, when, why requests failed, who made them, and performance characteristics. Essential for troubleshooting, auditing, security, and compliance.

**When to use:** Log all requests (with sanitization), errors, warnings, and significant events. Implement structured logging from day one. Use different log levels appropriately. Enable correlation IDs for distributed tracing.

**Log Levels:**

| Level | When to Use | Example |
|-------|-------------|---------|
| **TRACE** | Very detailed (development only) | Method entry/exit, variable values |
| **DEBUG** | Diagnostic information | Query parameters, business logic flow |
| **INFO** | Normal operation events | Request started, user logged in |
| **WARN** | Potentially harmful situations | Deprecated API used, slow query |
| **ERROR** | Error events | Exceptions, failed operations |
| **FATAL** | Severe errors causing shutdown | Database connection lost |

**Real-time Example:**

```http
# ========================================
# REQUEST/RESPONSE LOGGING
# ========================================

# Incoming request
GET /api/orders/ORD-501 HTTP/1.1
Host: api.company.com
Authorization: Bearer eyJhbGc...
User-Agent: Mozilla/5.0...
X-Request-ID: 550e8400-e29b-41d4-a716-446655440000

# Log entry (structured JSON):
{
  "timestamp": "2026-01-23T10:30:15.123Z",
  "level": "INFO",
  "requestId": "550e8400-e29b-41d4-a716-446655440000",
  "method": "GET",
  "path": "/api/orders/ORD-501",
  "queryParams": {},
  "headers": {
    "User-Agent": "Mozilla/5.0...",
    "Authorization": "Bearer ***"  # Sanitized!
  },
  "userId": 123,
  "ipAddress": "203.0.113.42",
  "userAgent": "Mozilla/5.0..."
}

---

# Response logged
HTTP/1.1 200 OK
X-Request-ID: 550e8400-e29b-41d4-a716-446655440000

{
  "orderId": "ORD-501",
  "total": 299.99
}

# Log entry:
{
  "timestamp": "2026-01-23T10:30:15.456Z",
  "level": "INFO",
  "requestId": "550e8400-e29b-41d4-a716-446655440000",
  "method": "GET",
  "path": "/api/orders/ORD-501",
  "statusCode": 200,
  "responseTimeMs": 333,
  "responseSize": 245,
  "userId": 123
}

---

# ========================================
# ERROR LOGGING
# ========================================

GET /api/orders/ORD-999 HTTP/1.1
X-Request-ID: abc-123

# Order not found

HTTP/1.1 404 Not Found
X-Request-ID: abc-123

{
  "error": "ORDER_NOT_FOUND",
  "message": "Order not found"
}

# Log entry:
{
  "timestamp": "2026-01-23T10:30:20.789Z",
  "level": "WARN",
  "requestId": "abc-123",
  "method": "GET",
  "path": "/api/orders/ORD-999",
  "statusCode": 404,
  "error": "ORDER_NOT_FOUND",
  "message": "Order ORD-999 not found",
  "userId": 123
}

---

# Exception error
POST /api/orders HTTP/1.1
X-Request-ID: def-456

{
  "customerId": 999,  # Customer doesn't exist
  "items": [...]
}

# Throws exception

HTTP/1.1 400 Bad Request
X-Request-ID: def-456

# Log entry:
{
  "timestamp": "2026-01-23T10:31:00.123Z",
  "level": "ERROR",
  "requestId": "def-456",
  "method": "POST",
  "path": "/api/orders",
  "statusCode": 400,
  "error": "ValidationException",
  "message": "Customer 999 not found",
  "stackTrace": "at OrderService.CreateOrder...",
  "userId": 123,
  "requestBody": {
    "customerId": 999,
    "items": [...]
  }
}

---

# ========================================
# CORRELATION ID (Distributed Tracing)
# ========================================

# Client request
GET /api/orders/ORD-501 HTTP/1.1
X-Correlation-ID: trace-abc-123

# API Gateway logs:
{
  "service": "APIGateway",
  "correlationId": "trace-abc-123",
  "message": "Request forwarded to OrderService"
}

---

# Order Service logs:
{
  "service": "OrderService",
  "correlationId": "trace-abc-123",
  "message": "Fetching order ORD-501 from database",
  "duration": 45
}

---

# Order Service calls Payment Service:
GET /internal/api/payments?orderId=ORD-501 HTTP/1.1
X-Correlation-ID: trace-abc-123

# Payment Service logs:
{
  "service": "PaymentService",
  "correlationId": "trace-abc-123",
  "message": "Retrieving payment for order ORD-501",
  "duration": 23
}

---

# All logs have same correlationId!
# Query logs: correlationId = "trace-abc-123"
# See entire request flow across services

---

# ========================================
# STRUCTURED LOGGING (Serilog)
# ========================================

using Serilog;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentName()
    .WriteTo.Console(new JsonFormatter())
    .WriteTo.File(
        path: "logs/api-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "api-logs-{0:yyyy.MM.dd}"
    })
    .CreateLogger();

// Use in controller:
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(string id)
    {
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["OrderId"] = id,
            ["UserId"] = User.FindFirst("sub")?.Value
        }))
        {
            _logger.LogInformation("Fetching order {OrderId}", id);
            
            try
            {
                var order = await _orderService.GetOrderAsync(id);
                
                if (order == null)
                {
                    _logger.LogWarning("Order {OrderId} not found", id);
                    return NotFound();
                }
                
                _logger.LogInformation("Order {OrderId} retrieved successfully", id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving order {OrderId}", id);
                return StatusCode(500);
            }
        }
    }
}

---

# ========================================
# LOGGING MIDDLEWARE
# ========================================

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    
    public async Task InvokeAsync(HttpContext context)
    {
        var requestId = Guid.NewGuid().ToString();
        context.Items["RequestId"] = requestId;
        context.Response.Headers.Add("X-Request-ID", requestId);
        
        var stopwatch = Stopwatch.StartNew();
        
        // Log request
        _logger.LogInformation(
            "HTTP {Method} {Path} started. RequestId: {RequestId}, User: {UserId}",
            context.Request.Method,
            context.Request.Path,
            requestId,
            context.User.FindFirst("sub")?.Value ?? "anonymous");
        
        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            
            // Log response
            _logger.LogInformation(
                "HTTP {Method} {Path} completed. StatusCode: {StatusCode}, Duration: {Duration}ms, RequestId: {RequestId}",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds,
                requestId);
        }
    }
}

// Register:
app.UseMiddleware<RequestLoggingMiddleware>();

---

# ========================================
# SENSITIVE DATA SANITIZATION
# ========================================

// ❌ BAD: Logging sensitive data
_logger.LogInformation("User login: {Email}, Password: {Password}", 
    email, password);

// Logs:
// User login: john@example.com, Password: Secret123!
// SECURITY RISK!

---

// ✅ GOOD: Sanitize sensitive data
public class SensitiveDataSanitizer
{
    private static readonly string[] SensitiveFields = 
    {
        "password", "creditCard", "ssn", "token", "authorization"
    };
    
    public static object Sanitize(object data)
    {
        if (data is string json)
        {
            var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            
            foreach (var field in SensitiveFields)
            {
                if (root.TryGetProperty(field, out _))
                {
                    // Replace with ***
                    json = json.Replace(
                        root.GetProperty(field).GetString(),
                        "***");
                }
            }
            
            return json;
        }
        
        return data;
    }
}

_logger.LogInformation("User login: {Email}", email);
// Logs: User login: john@example.com (no password!)

---

# ========================================
# PERFORMANCE LOGGING
# ========================================

public class PerformanceLoggingFilter : IActionFilter
{
    private readonly ILogger<PerformanceLoggingFilter> _logger;
    private Stopwatch _stopwatch;
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();
    }
    
    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        var duration = _stopwatch.ElapsedMilliseconds;
        
        var logLevel = duration switch
        {
            < 100 => LogLevel.Debug,
            < 500 => LogLevel.Information,
            < 1000 => LogLevel.Warning,
            _ => LogLevel.Error  # Very slow!
        };
        
        _logger.Log(logLevel,
            "Action {Action} completed in {Duration}ms",
            context.ActionDescriptor.DisplayName,
            duration);
        
        if (duration > 1000)
        {
            _logger.LogWarning(
                "SLOW REQUEST: {Method} {Path} took {Duration}ms",
                context.HttpContext.Request.Method,
                context.HttpContext.Request.Path,
                duration);
        }
    }
}

---

# ========================================
# DEBUGGING WITH REQUEST ID
# ========================================

# User reports error
# User provides: "Request ID: 550e8400-e29b-41d4-a716-446655440000"

# Query logs:
{
  "query": {
    "match": {
      "requestId": "550e8400-e29b-41d4-a716-446655440000"
    }
  }
}

# Results: All logs for this request
[
  {
    "timestamp": "2026-01-23T10:30:15.123Z",
    "level": "INFO",
    "message": "Request started",
    ...
  },
  {
    "timestamp": "2026-01-23T10:30:15.234Z",
    "level": "DEBUG",
    "message": "Querying database for order",
    ...
  },
  {
    "timestamp": "2026-01-23T10:30:15.567Z",
    "level": "ERROR",
    "message": "Database timeout",
    "exception": "TimeoutException..."
  }
]

# Found root cause: Database timeout

---

# ========================================
# LOG AGGREGATION (ELK Stack)
# ========================================

# Elasticsearch, Logstash, Kibana

# Query Kibana:
# - Show all errors in last hour
# - Average response time by endpoint
# - Top 10 slowest requests
# - Error rate per service
# - User activity timeline

# Example Kibana query:
{
  "query": {
    "bool": {
      "must": [
        {"match": {"level": "ERROR"}},
        {"range": {"timestamp": {"gte": "now-1h"}}}
      ]
    }
  },
  "aggs": {
    "top_errors": {
      "terms": {"field": "error.keyword", "size": 10}
    }
  }
}
```

**Logging Best Practices:**
* ✓ Use structured logging (JSON format)
* ✓ Include correlation ID in all logs
* ✓ Log request ID for tracing
* ✓ Sanitize sensitive data (passwords, tokens, PII)
* ✓ Use appropriate log levels (DEBUG, INFO, WARN, ERROR)
* ✓ Log request start and completion
* ✓ Log performance metrics (response time)
* ✓ Include user ID for audit trail
* ✓ Log all errors with stack traces
* ✓ Use log aggregation (ELK, Splunk, Datadog)
* ✓ Set up alerts for error rate thresholds
* ✓ Rotate logs regularly (daily)
* ✓ Retain logs per compliance requirements
* ✓ Index logs for fast searching
* ✓ Monitor log volume (prevent disk full)

**What to Log:**
* ✓ Request method, path, query params
* ✓ Response status code, time
* ✓ User ID, IP address, user agent
* ✓ Request/Correlation ID
* ✓ Errors and exceptions
* ✓ Performance metrics
* ✓ External API calls (start, end, duration)
* ✓ Database queries (slow queries)
* ✓ Authentication events (login, logout, failures)
* ✓ Authorization failures

**What NOT to Log:**
* ✗ Passwords
* ✗ Credit card numbers
* ✗ Social security numbers
* ✗ API keys, tokens (full values)
* ✗ Personal identifiable information (PII) unless required
* ✗ Full request/response bodies (unless debugging)

---

**Batch 5 completed. All 50 questions completed!**

---

## Summary

This comprehensive guide covered 50 REST API interview questions across five key areas:

01. **REST Fundamentals & HTTP Methods (1-10)**: REST principles, HTTP methods, idempotency, statelessness, REST vs SOAP, status codes, safe/unsafe methods
   
02. **Authentication, Security & Authorization (11-20)**: JWT, OAuth 2.0, API keys, rate limiting, versioning, CORS, pagination, caching, webhooks

03. **API Design & Best Practices (21-30)**: URI design, error handling, PUT vs POST, sync vs async, API Gateway, throttling, documentation, monitoring, deprecation

04. **Advanced Topics & Performance (31-40)**: Monolithic vs microservices, GraphQL vs REST, API Gateway pattern, idempotency in distributed systems, HTTP status codes, file uploads, API testing, mobile vs web design, common mistakes, backwards compatibility

Each question includes:
* Clear explanation of concepts
* Real-world use cases
* Practical HTTP examples
* Code samples
* Best practices
* Common pitfalls to avoid

This guide prepares you for mid-to-senior level REST API interviews with production-ready knowledge.
