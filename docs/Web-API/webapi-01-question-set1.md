# .NET Core & Web API Interview Preparation Guide

## 1. LINQ Selection Methods: First vs Single

The differences between these four LINQ methods boil down to two core criteria: **how many items match your condition**, and **what happens if zero items are found**.

### Method Decision Matrix

| Method | What if **0 items** match? | What if **1 item** matches? | What if **2+ items** match? |
| :--- | :--- | :--- | :--- |
| **`First()`** | 💥 **Throws Exception** | Returns the item | Returns the **first** item |
| **`FirstOrDefault()`** | ↩️ Returns `null` / default | Returns the item | Returns the **first** item |
| **`Single()`** | 💥 **Throws Exception** | Returns the item | 💥 **Throws Exception** |
| **`SingleOrDefault()`** | ↩️ Returns `null` / default | Returns the item | 💥 **Throws Exception** |

---

### Detailed Method Breakdown

#### `First()` vs `FirstOrDefault()`
Use these when you only care about getting a matching item and do not care if duplicates exist later in the collection.
* **`First()`**: Evaluates the collection and extracts the very first match. If the sequence is empty, it crashes.
  ```csharp
  // 💥 Throws InvalidOperationException: "Sequence contains no matching element"
  var user = users.First(u => u.Username == "non_existent"); 
  ```
* **`FirstOrDefault()`**: Finds the first match. If no match exists, it safely returns `null` (or the type's default value).
  ```csharp
  // ↩️ Returns null safely (No Exception)
  var user = users.FirstOrDefault(u => u.Username == "non_existent"); 
  ```

#### `Single()` vs `SingleOrDefault()`
Use these when you want to enforce strict **uniqueness**. They scan the *entire* collection to guarantee that exactly one item matches.
* **`Single()`**: Expects **exactly one** element. It throws an exception if there are zero matches, or if there are two or more matches.
  ```csharp
  // 💥 Throws InvalidOperationException: "Sequence contains more than one matching element"
  var user = users.Single(u => u.Id == 2); // (Assuming multiple users have Id = 2)
  ```
* **`SingleOrDefault()`**: Expects **zero or one** item. It returns `null` safely if none are found, but crashes if duplicates are detected.
  ```csharp
  // 💥 Throws InvalidOperationException: "Sequence contains more than one matching element"
  var user = users.SingleOrDefault(u => u.Id == 2); 
  ```

### 💡 Interviewer's Performance Tip
When translated to SQL via Entity Framework Core:
* `FirstOrDefault()` appends **`TOP(1)`** to the SQL query. The database engine stops searching the moment it hits the first matching row.
* `SingleOrDefault()` appends **`TOP(2)`** to the SQL query. The database engine *must* continue scanning the database even after finding a match to guarantee that a second matching row does not exist. Therefore, `FirstOrDefault()` is generally more performant for large tables.

---

## 2. API Architectural Concepts: Idempotency

**Idempotency** is an API design property where an operation can be executed multiple times, but the final side effects on the system infrastructure remain exactly the same as the **very first execution**.

> **Analogy:** No matter how many times you press the elevator call button, the elevator is only called once. The side effect does not multiply.

### Idempotency across HTTP Verbs

| HTTP Method | Idempotent? | Architectural Reason |
| :--- | :--- | :--- |
| **`GET`** | **Yes** | Read-only. Fetching a resource changes nothing in the database. |
| **`PUT`** | **Yes** | Replaces/updates a resource. Setting `Status = "Active"` multiple times leaves it as `"Active"`. |
| **`DELETE`** | **Yes** | Removes a resource. Once an item is removed, subsequent deletes change nothing (it stays gone). |
| **`POST`** | ❌ **No** | Factory action. Sending `POST /api/orders` 5 times will create 5 distinct records. |

### 🛠️ Production Pattern: The Idempotency-Key
Because `POST` is not naturally idempotent, downstream payment gateways and processing APIs utilize the **Idempotency Key Pattern**:
1. The client generates a unique transaction string identifier (usually a `Guid`) and attaches it as a custom header: `Idempotency-Key: 9b1deb4d-3b7d-4bad-9bdd-2b0d7b3dcb6d`.
2. The API intercepts the header and checks a high-speed distributed cache (like **Redis**).
3. If the key is new, the request processes normally, and the final response payload is saved into Redis against that key with an expiration window (e.g., 24 hours).
4. If a duplicate network request arrives with the identical key due to a client timeout retry, the middleware short-circuits the application code, extracts the saved response from Redis, and returns it directly.

### ⚠️ Critical Interview Trick Question
**Question:** *"If a `DELETE` endpoint returns a `200 OK` on the first call, and a `404 Not Found` on the second call because the resource is missing, is the endpoint still idempotent?"*

**Answer:** **Yes.** Idempotency describes the final **state of the system resource**, not the HTTP status response code. After the first call, the item is deleted. After the second call, the item remains deleted. The database state did not alter further after the subsequent execution, confirming it is functionally idempotent.

---

## 3. The Options Pattern: IOptions vs IOptionsSnapshot vs IOptionsMonitor

When reading configurations via the Options Pattern in .NET, you can inject three distinct interfaces. They differ fundamentally based on **service lifetime support**, **performance overhead**, and whether they support **re-reading file changes at runtime** (`reloadOnChange: true`).

### Options Pattern Matrix

| Interface | Service Lifetime | Reads Live File Changes? | Value Evaluation Timing | Performance Cost |
| :--- | :--- | :--- | :--- | :--- |
| **`IOptions<T>`** | Singleton | ❌ No | Computed once at app startup | 🟢 Lowest (Cached) |
| **`IOptionsSnapshot<T>`** | Scoped |  Yes | Recomputed **once per HTTP request** | 🟡 Medium (Per-request isolation) |
| **`IOptionsMonitor<T>`** | Singleton |  Yes | Always retrieves the **absolute latest** value | 🟡 Medium (Uses file watchers) |

---

### Detailed Interface Breakdown

#### `IOptions<T>`
* **How it works:** It acts as a permanent snapshot taken when the web server initializes. It is registered as a **Singleton**.
* **Limitation:** If you modify `appsettings.json` while the app is running, `IOptions<T>` will **never** see the new value until you completely restart the host process.
* **Best used for:** Formats or keys that absolutely never change during runtime (e.g., App Name, base structural setup values).

#### `IOptionsSnapshot<T>`
* **How it works:** It is registered as a **Scoped** service. It reads the configuration file at the start of an HTTP request and locks those values in place for the remainder of that specific request.
* **Benefit:** If a user makes a request, and you update a configuration file mid-request, the current executing code is safe from changing values, ensuring data consistency within that thread.
* **Best used for:** Scoped business services (like `IdentityService` or `OrderService`) where settings might be changed by an administrator live.
* **Warning:** Because it is Scoped, you **cannot** inject `IOptionsSnapshot<T>` into a Singleton service (it will cause a *Captive Dependency* exception).

#### `IOptionsMonitor<T>`
* **How it works:** It is registered as a **Singleton** but utilizes an internal file-system watcher to monitor updates dynamically. You access its properties using `.CurrentValue` instead of `.Value`.
* **Benefit:** It can be safely injected into Singleton services (like custom caching layers, background workers, or Kestrel level hooks) while still retaining the ability to read modified data immediately.
* **Bonus Feature:** It exposes an `.OnChange()` event handler method allowing you to trigger custom C# code actions immediately when a file change occurs (e.g., logging a notice or clearing an memory cache).

```csharp
// Example using IOptionsMonitor's change tracker in a constructor:
public MyCacheService(IOptionsMonitor<JwtOptions> monitor)
{
    _options = monitor.CurrentValue;
    monitor.OnChange(updatedOptions => {
        // Triggered automatically the second appsettings.json is saved!
        Console.WriteLine("JWT Secrets were updated live in production!");
    });
}
```
## 4. Anatomy of a JWT: Header, Payload, and Signature

A JSON Web Token (JWT) is an open standard (RFC 7519) that defines a compact and self-contained way for securely transmitting information between parties as a JSON object. Structurally, it consists of three distinct parts separated by dots (`.`): `Header.Payload.Signature`.

---

### JWT Structure Matrix

| Component | Content Type | Primary Responsibility | Data Visibility | Encrypted? |
| :--- | :--- | :--- | :--- | :--- |
| **1. Header** | JSON Metadata | Identifies the token type and cryptographic algorithm used. | 👁️ Publicly Readable (Base64Url) | ❌ No |
| **2. Payload** | JSON Identity Claims | Contains the user identity details, permissions, and metadata. | 👁️ Publicly Readable (Base64Url) | ❌ No |
| **3. Signature** | Cryptographic Hash | Verifies token integrity and proves the token has not been altered. | 🔒 Secure Hash Value | ❌ No (It is a hash signature) |

---

### Detailed Breakdown of Components

#### 1. The Header
The header typically consists of two parts: the type of the token, which is JWT, and the signing algorithm being used, such as HMAC SHA256 (HS256) or RSA.
```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```
* **Role:** It acts as the processing instruction manual for the incoming security middleware. When your API's `UseAuthentication` middleware catches a token, it reads the header first to know exactly which mathematical algorithm to initialize to evaluate the upcoming signature.

#### 2. The Payload
The payload contains the **Claims**, which are statements about an entity (typically, the user) and additional data. 
```json
{
  "unique_name": "admin",
  "email": "admin@company.com",
  "role": "Admin",
  "jti": "d3b07384-d113-4c92-a743-162e245a4a58",
  "iss": "https://localhost:44351/",
  "aud": "https://localhost:44351/",
  "exp": 1718912345
}
```
* **Role:** It serves as the primary stateless identity payload of the request. Once verified, this data maps straight into the `HttpContext.User` object. The `role` claim dictates whether `[Authorize(Roles = "Admin")]` blocks or passes the user, and the `exp` claim dictates if the token is dead or alive.

---

### Key JWT Configurations & Core Concepts

#### What is Encoding (Base64Url)?
Encoding is the process of translating raw data structures (like a JSON text object) into a standardized string format that is **safe to transmit across networks and URL paths**. 
* **The Rule:** **Encoding is NOT Encryption.** The Header and Payload are simply Base64Url encoded to turn standard JSON symbols (like `{}` and `"`) into safe alphanumeric characters (`A-Z`, `a-z`, `0-9`). Anyone can paste an encoded JWT string into a decoder tool like [jwt.io](https://jwt.io) and read your raw payload data instantly. 

#### What is the JTI (`jti`) Claim?
`jti` stands for **JWT ID**. It provides a unique, random string identifier (usually a `Guid`) for that specific token instance.
* **Role:** **Replay Attack Prevention**. By tracking used `jti` values in a high-speed data store like Redis, your API can intercept and reject an incoming token if an attacker intercepts it and tries to maliciously execute it multiple times within its validity window.

#### What is the SecretKey?
The **SecretKey** is the master cryptographic password known **only to your backend**. It must be at least 256 bits long (32 plain text characters) for HMAC SHA256. 
* **Role:** It is mixed with the token's data to generate the final Signature. This key ensures that your API can cryptographically verify that the token was generated by your trusted source and has not been fabricated or tampered with by the client.

#### What is the Issuer (`iss`)?
The **Issuer** identifies the specific **security server that created the token** (e.g., `https://localhost:44351/`).
* **Role:** It prevents your API from accepting tokens created by an untrusted or external authentication system.

#### What is the Audience (`aud`)?
The **Audience** identifies the **intended recipient or destination system of the token** (e.g., your specific backend API service URL).
* **Role:** It prevents token misuse by ensuring a token generated for a low-security public application cannot be sneakily used to access a highly sensitive financial API endpoint.

---

#### 3. The Signature
The signature is the security seal of the entire token. To create it, the framework takes the encoded header, the encoded payload, and your backend's private **SecretKey**, and passes them all through the algorithm specified in the header.
```csharp
// Conceptually, the signature is generated like this:
HMACSHA256(
    Base64UrlEncode(header) + "." +
    Base64UrlEncode(payload),
    SecretKey
)
```
* **Role:** **Integrity Verification and Anti-Tampering**. If an attacker tries to change the payload text from `"role": "User"` to `"role": "Admin"`, they can easily re-encode that payload back into a Base64 string. However, because they do not know the backend's `SecretKey`, they cannot generate a matching signature hash. Your API recalculates the hash on every request, catches the discrepancy, and drops the request instantly.
