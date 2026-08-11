# Microservices — 2. Architecture & Design — Interview Q&A

---

### Q1. What is the difference between Synchronous and Asynchronous communication?

**Answer:**
"Synchronous is a direct call (REST/HTTP, gRPC) where the caller blocks/waits for a response — used when you need an immediate answer. Asynchronous is message/event-based, through a broker — the sender publishes and moves on, and one or more subscribers react whenever they get to it. Sync couples the caller to the callee's availability right now; async decouples them in time."

```
Sync:  Order Service --(HTTP GET)--> Party Service   (waits for response)
Async: Order Service --(publish "OrderCreated")--> Broker --> Inventory, Billing (react independently, later)
```

**Where to use:** sync for "I need the answer now" (checking stock before confirming an order); async for "notify others something happened" (an order was placed) without blocking on their processing.

---

### Q2. What is Event-Driven Architecture?

**Answer:**
"Services communicate by publishing and reacting to events instead of calling each other directly. A service doesn't know or care who's listening — it just announces 'this happened.' Any number of other services can subscribe and react. This decouples services in both time and knowledge of each other, at the cost of losing the immediate request/response guarantee."

```
OrderService publishes: OrderPlaced { OrderId, CustomerId, Items }
  -> InventoryService reserves stock
  -> BillingService charges the customer
  -> NotificationService emails a confirmation
(OrderService knows nothing about any of these three subscribers)
```

---

### Q3. What is Circuit Breaker and how does it work?

**Answer:**
"It tracks failures for calls to a dependency. After enough failures within a window, it 'trips' to an Open state and starts failing fast (or returning a fallback) instead of calling the failing service — protecting the caller from piling up requests/threads against something that's down. After a cooldown, it moves to Half-Open and lets a few requests through to test recovery; if they succeed, it closes again."

```csharp
// Polly circuit breaker in .NET
var breaker = Policy
    .Handle<HttpRequestException>()
    .CircuitBreakerAsync(exceptionsAllowedBeforeBreaking: 3, durationOfBreak: TimeSpan.FromSeconds(30));

await breaker.ExecuteAsync(() => httpClient.GetAsync(url));
```

**States:** Closed (normal) → Open (failing fast) → Half-Open (testing recovery) → back to Closed or Open.

---

### Q4. What is the Bulkhead pattern?

**Answer:**
"Named after ship bulkheads that stop one flooded compartment from sinking the whole ship. In software, it means isolating resources (thread pools, connection pools) per dependency, so if one downstream service is slow or overwhelmed, it only exhausts its own dedicated pool — not the shared pool every other call depends on, which would otherwise cascade the failure to unrelated calls."

```
Without bulkhead: one shared thread pool. Slow Payments calls exhaust ALL threads, starving Inventory calls too.
With bulkhead: Payments gets its own limited pool. Inventory calls keep working even if Payments is stuck.
```

**Where to use:** any service calling multiple downstream dependencies with different reliability/latency profiles — isolate the risky ones.

---

### Q5. What is the Retry pattern?

**Answer:**
"Automatically retrying a failed operation, usually with a backoff delay between attempts, because many failures in distributed systems are transient (a brief network blip, a momentarily overloaded service) and simply succeed on a second try. It should always be combined with a capped retry count and ideally exponential backoff, so it doesn't hammer an already-struggling service."

```csharp
var retryPolicy = Policy
    .Handle<HttpRequestException>()
    .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))); // 2s, 4s, 8s

await retryPolicy.ExecuteAsync(() => httpClient.GetAsync(url));
```

**Where this comes up as a trick question:** retrying a non-idempotent operation (like "charge the card") without idempotency protection can cause duplicate side effects — see Q6.

---

### Q6. What is Idempotency in APIs?

**Answer:**
"An operation is idempotent if calling it multiple times with the same input has the same effect as calling it once. This matters enormously with retries — if a request times out but actually succeeded server-side, and the client retries, an idempotent endpoint won't double-charge a card or create a duplicate order. Typically implemented with an idempotency key the client generates and sends, which the server checks against previously-processed requests."

```csharp
[HttpPost("orders")]
public async Task<IActionResult> CreateOrder([FromHeader] string idempotencyKey, OrderRequest request)
{
    if (await _store.HasProcessed(idempotencyKey))
        return Ok(await _store.GetResult(idempotencyKey)); // return the same result, don't redo the work

    var result = await _orderService.CreateOrder(request);
    await _store.SaveResult(idempotencyKey, result);
    return Ok(result);
}
```

**Where to use:** any endpoint that creates or charges something and might be retried — payment processing, order creation.

---

### Q7. What is the Strangler Pattern?

**Answer:**
"A strategy for migrating a monolith to microservices incrementally instead of a risky big-bang rewrite. New functionality (or a piece being extracted) is built as a new microservice, and a routing layer gradually redirects traffic for that capability from the old monolith to the new service — piece by piece, until the monolith is 'strangled' down to nothing, or just the parts not worth migrating."

```
Router: /api/orders/*   -> new Order microservice
        /api/*  (everything else) -> old monolith (unchanged, for now)
Over time, more routes move to new services until the monolith shrinks or disappears.
```

**Where to use:** migrating a legacy monolith safely — lets you validate each extracted piece in production with real traffic before removing the old code path.

---

### Q8. How do you handle configuration management in Microservices?

**Answer:**
"Externalize configuration from code entirely — don't bake environment-specific values into the deployed artifact. Use a centralized config source (Azure App Configuration, Consul, Kubernetes ConfigMaps/Secrets, environment variables injected at deploy time) so the same container image can run in dev/staging/prod with different config, and config changes don't require a rebuild."

```
Kubernetes: values come from ConfigMaps (non-secret) and Secrets (sensitive), injected as env vars or mounted files
Azure: App Configuration + Key Vault for secrets
```

**Where to use:** anything environment-specific (connection strings, feature flags, API keys) — never hardcoded, never baked into the image.

---

### Q9. What is a Service Registry? (Eureka, Consul, etc.)

**Answer:**
"A directory that keeps track of which service instances are currently running and where (host/port), so other services (or a load balancer) can look them up dynamically instead of relying on hardcoded addresses. Services register themselves on startup and send heartbeats; the registry removes instances that stop responding. Kubernetes provides this built-in via its internal DNS and Service objects — you often don't need a separate tool like Eureka/Consul if you're already on Kubernetes."

```
Order Service registers: "I'm at 10.0.1.5:8080, healthy"
Payment Service asks registry: "where's a healthy Order Service instance?" -> gets 10.0.1.5:8080
```
