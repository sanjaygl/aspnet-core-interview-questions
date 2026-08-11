# Microservices — 10. Scenario-Based Questions — Interview Q&A

---

### Q1. How do you handle one service failure without affecting other services?

**Answer:**
"Isolate the failure with resilience patterns: a circuit breaker so callers stop hammering the failing service, bulkheads so its resource usage (thread/connection pools) can't starve unrelated calls, timeouts so callers don't hang waiting on it, and fallbacks (cached data, a degraded response, or skipping a non-critical feature) so the overall request can still complete in a reduced form instead of failing outright. Combined with async/event-driven communication where possible, so a slow or down service doesn't block others that don't strictly need an immediate response from it."

---

### Q2. How do you manage 100+ Microservice URLs?

**Answer:**
"Don't hardcode addresses anywhere — use service discovery (Kubernetes DNS, Consul, Eureka) so services resolve each other by logical name, not a fixed IP/port. Put an API Gateway in front for anything external-facing, so clients only need to know one address, not a hundred. Internally, a service mesh (like Istio/Linkerd) can also handle routing, retries, and load balancing between services transparently, without each service needing its own discovery/routing logic hardcoded in."

---

### Q3. How do you ensure data consistency when multiple services update data?

**Answer:**
"Accept that strict, instantaneous consistency across services isn't realistic without reintroducing tight coupling — instead use the Saga pattern to coordinate a multi-service business transaction with compensating actions on failure, make each step idempotent so retries are safe, and design for eventual consistency with events, rather than trying to force a distributed ACID transaction across service boundaries."

---

### Q4. How do you migrate a Monolith to Microservices?

**Answer:**
"Incrementally, using the Strangler pattern rather than a risky rewrite-everything-at-once approach. Identify a clear, well-bounded capability in the monolith, extract it into a new service with its own data store, put a routing layer in front that redirects traffic for that capability to the new service, and validate it in production with real traffic before moving to the next piece. Repeat, piece by piece, until the monolith is reduced to whatever core doesn't need to be split out."

---

### Q5. How do you handle large traffic spikes?

**Answer:**
"Horizontal autoscaling (Kubernetes HPA scaling out more pods based on load), a load balancer distributing traffic across all healthy instances, rate limiting at the edge to protect against abusive or runaway traffic, caching to reduce repeated load on the same expensive operations, and asynchronous processing (queues) for work that doesn't need to complete synchronously — smoothing out a spike into a queue that gets drained at a sustainable rate instead of trying to process it all instantly."

---

### Q6. How do you debug issues in production across multiple services?

**Answer:**
"Start with distributed tracing — find the trace for the affected request(s) and see exactly which service in the chain failed or was slow. Cross-reference with centralized logs filtered by the same correlation ID, to see the detailed error context from each service involved. Check metrics/dashboards around that time window for anomalies (error rate spikes, latency spikes, resource exhaustion) that might point to root cause. Without tracing and correlation IDs in place beforehand, this becomes a much slower, manual process of guessing which service logs to even look at."

---

### Q7. How would you design an Order-Payment-Inventory system using Microservices?

**Answer:**
"Three services, each owning its own data: Order Service (order records, order state machine), Payment Service (payment processing, integrates with a payment gateway), Inventory Service (stock levels, reservations). Flow as a Saga: Order Service creates the order in a 'Pending' state and publishes `OrderCreated`. Inventory Service reserves stock and publishes `StockReserved` (or `StockUnavailable` if it fails). Payment Service, once stock is confirmed, charges the customer and publishes `PaymentCompleted` (or `PaymentFailed`). Order Service listens for these events and moves the order to 'Confirmed' or 'Cancelled' accordingly. If Payment fails after stock was reserved, a compensating event releases the reserved stock. Communication is event-driven (via a broker) between the services so none of them block waiting on the others synchronously, and each step is idempotent so a redelivered event doesn't double-charge or double-reserve."

```
OrderCreated -> InventoryService reserves stock -> StockReserved
             -> PaymentService charges card -> PaymentCompleted
             -> OrderService marks order Confirmed

Failure path: PaymentFailed -> InventoryService releases reserved stock (compensating action)
                             -> OrderService marks order Cancelled
```
