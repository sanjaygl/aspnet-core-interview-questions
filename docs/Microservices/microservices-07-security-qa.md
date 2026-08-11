# Microservices — 7. Security — Interview Q&A

---

### Q1. How do you secure Microservices?

**Answer:**
"Multiple layers: authenticate and authorize every request (usually via OAuth2/JWT), enforce it at the API Gateway so it's not duplicated inconsistently across every service, encrypt traffic between services (TLS/mTLS), validate and sanitize all input at each service boundary (never trust that 'internal' traffic is automatically safe), and apply least-privilege access — each service and its database credentials should only be able to do what that service actually needs, nothing more."

---

### Q2. What is OAuth 2.0?

**Answer:**
"An authorization framework/protocol — it defines how a client can obtain a token proving it's allowed to access a resource, without handing over the actual user credentials to every service that needs to check access. A common flow: a user authenticates with an identity provider (Azure AD, Auth0, etc.), gets back an access token, and that token is then sent with subsequent requests to prove who they are and what they're allowed to do — services validate the token instead of re-authenticating the user themselves."

```
1. User logs in via Identity Provider -> gets an access token
2. Client sends: Authorization: Bearer <token> with every API request
3. API Gateway / service validates the token (signature, expiry, claims) before processing the request
```

---

### Q3. What is JWT?

**Answer:**
"JSON Web Token — a compact, digitally signed token format commonly used to carry the result of authentication (who the user is, what roles/claims they have, when it expires) between client and services. It's self-contained — a service can verify the signature and read the claims directly, without a round trip back to the identity provider for every single request, which is a big part of why it scales well across many microservices."

```
Header.Payload.Signature
eyJhbGc...  .  eyJzdWIiOiIxMjM...  .  SflKxwRJ...

Decoded payload (example):
{ "sub": "user123", "role": "Admin", "exp": 1735689600 }
```

**Where this comes up as a trick question:** JWTs are signed, not encrypted by default — anyone can decode and read the payload (it's just base64), they just can't forge a valid signature without the signing key. Don't put secrets in a JWT payload.

---

### Q4. How does an API Gateway help with security?

**Answer:**
"It centralizes authentication/authorization enforcement in one place, so individual services don't each need to reimplement token validation correctly (and risk getting it wrong or inconsistent). It can also terminate TLS, apply rate limiting to blunt abuse/DoS attempts, and hide the internal service topology from external clients, reducing the attack surface exposed to the outside world."

---

### Q5. How do services authenticate with each other?

**Answer:**
"Common approaches: mutual TLS (mTLS), where each service presents a certificate proving its identity to the other, common in a service mesh setup (like Istio) that handles this transparently. Or service-to-service tokens — a service obtains its own access token (via client-credentials OAuth flow) representing 'this is the Order Service calling,' distinct from a user's token, so downstream services can authorize based on which service is calling, not just which user is behind the request."

```
Service-to-service (client credentials flow):
Order Service -> Identity Provider: "give me a token as 'order-service'"
Order Service -> Inventory Service: Authorization: Bearer <service-token>
Inventory Service validates: "this is really order-service calling" before processing
```
