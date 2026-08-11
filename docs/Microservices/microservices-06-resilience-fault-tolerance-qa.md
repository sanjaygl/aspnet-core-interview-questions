# Microservices — 6. Resilience & Fault Tolerance — Interview Q&A

---

### Q1. How do you handle service failures?

**Answer:**
"Layer several resilience patterns together rather than relying on one: timeouts so a call never hangs indefinitely, retries with backoff for transient failures, a circuit breaker to stop hammering a service that's clearly down, a bulkhead to isolate resource pools per dependency, and a fallback (cached data, a default response, or a graceful degraded experience) so a failing dependency doesn't necessarily fail the whole request. Combined, these keep one failing service from cascading into a system-wide outage."

```csharp
// Polly - combine retry + circuit breaker + timeout
var policy = Policy.WrapAsync(
    Policy.Handle<Exception>().WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(i)),
    Policy.Handle<Exception>().CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)),
    Policy.TimeoutAsync(TimeSpan.FromSeconds(5))
);
```

---

### Q2. How do you implement Health Checks?

**Answer:**
"Expose a lightweight endpoint (commonly `/health`) that reports whether the service and its critical dependencies (database, downstream services) are actually working — not just that the process is running. Orchestrators like Kubernetes call this endpoint regularly: a failing liveness check gets the container restarted, a failing readiness check gets it removed from load-balancer rotation until it recovers, without killing it."

```csharp
// ASP.NET Core health checks
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString)
    .AddCheck("custom", () => HealthCheckResult.Healthy());

app.MapHealthChecks("/health");
```

**Liveness vs Readiness:** liveness = "is the process alive, or should it be restarted?"; readiness = "is it ready to receive traffic right now?" — a service can be alive but not ready (e.g., still warming up a cache).

---

### Q3. What is a Timeout strategy?

**Answer:**
"Setting a maximum time to wait for a call to a dependency before giving up, instead of waiting indefinitely. Without timeouts, a slow or hung downstream service can tie up the caller's threads/connections waiting forever, which can cascade into resource exhaustion on the caller too. Timeouts should be set thoughtfully — too short causes false failures under normal load spikes, too long delays failure detection and lets resource exhaustion build up."

```csharp
httpClient.Timeout = TimeSpan.FromSeconds(5); // don't wait forever on a slow dependency
```

**Where to use:** every outbound network call to another service — never rely on the OS/framework default, which is often far too long (or effectively infinite) for a responsive system.

---

### Q4. How do you ensure High Availability?

**Answer:**
"Run multiple instances of every service across multiple availability zones/nodes, so a single instance or node failure doesn't take the service down — combined with load balancing and health checks so traffic automatically routes only to healthy instances. At the data layer, use replicated/clustered databases instead of a single point of failure. And design for graceful degradation — if a non-critical dependency is down, the core functionality should still work, even if some feature is temporarily unavailable."

**Where to use:** anything customer-facing or business-critical — no single instance, no single node, no single database replica should be a single point of failure.
