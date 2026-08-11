# Microservices — 5. Scalability & Performance — Interview Q&A

---

### Q1. How do you scale Microservices?

**Answer:**
"Mainly horizontally — run more instances of the service under load, behind a load balancer, rather than making a single instance bigger. In Kubernetes this is as simple as increasing the replica count for a Deployment, and can be automated with a Horizontal Pod Autoscaler reacting to CPU/memory/custom metrics. Each service can also be scaled independently based on its own load — the whole point of splitting into microservices."

```yaml
# Kubernetes HPA - scale between 2 and 10 pods based on CPU
minReplicas: 2
maxReplicas: 10
targetCPUUtilizationPercentage: 70
```

---

### Q2. What is the difference between Horizontal and Vertical Scaling?

**Answer:**
"Horizontal scaling adds more instances of the service, spreading load across them — this is the standard approach for microservices, since it's elastic and works well with load balancing. Vertical scaling adds more resources (CPU/RAM) to a single existing instance — there's a hard ceiling (the biggest machine available), and it usually requires downtime to resize. Microservices are designed to favor horizontal scaling, which is part of why they need to be stateless (see Q3)."

---

### Q3. What is a Stateless Service and why is it important?

**Answer:**
"A stateless service doesn't store any client-specific session state in its own memory between requests — any instance can handle any request, because nothing about a particular user's session is 'stuck' on one particular instance. This is what makes horizontal scaling and load balancing actually work cleanly: if state were kept in-process, a load balancer sending a user's next request to a different instance would lose that state. Session state, if needed, gets externalized to something shared — a distributed cache like Redis, or a database — instead of living in the service's memory."

```
Stateless: any of Order Service's 5 instances can handle any request equally
Stateful (bad for scaling): User's shopping cart stored in Instance 2's memory only —
  if the load balancer routes their next request to Instance 4, the cart "disappears"
```

---

### Q4. How do you handle caching in Microservices?

**Answer:**
"With a distributed cache (Redis is the common choice) shared across all instances of a service, rather than an in-memory cache local to one instance — otherwise different instances could serve stale or inconsistent cached data. Cache frequently-read, rarely-changed data (reference data, computed results) to reduce load on the database and downstream service calls, and set sensible expiration/invalidation so stale data doesn't linger indefinitely."

```csharp
var cached = await _redisCache.GetStringAsync($"customer:{id}");
if (cached == null)
{
    var customer = await _repository.GetByIdAsync(id);
    await _redisCache.SetStringAsync($"customer:{id}", JsonSerializer.Serialize(customer),
        new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });
}
```

**Where to use:** reference/lookup data, expensive computed results, responses from slow downstream calls — anywhere read volume is much higher than write/change frequency.

---

### Q5. What is Rate Limiting?

**Answer:**
"Restricting how many requests a client (or overall) can make in a given time window, to protect a service from being overwhelmed — whether by legitimate traffic spikes, a buggy client retrying too aggressively, or abuse. Usually implemented at the API Gateway so it's enforced once, centrally, rather than duplicated in every downstream service."

```csharp
// ASP.NET Core built-in rate limiting (Microsoft.AspNetCore.RateLimiting)
builder.Services.AddRateLimiter(options =>
    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromMinutes(1);
    }));
```

**Where to use:** public-facing APIs, and internal APIs shared across many callers where one misbehaving consumer shouldn't be able to degrade service for everyone else.
