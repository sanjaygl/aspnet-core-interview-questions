# Microservices — 3. Data Management — Interview Q&A

---

### Q1. How do you maintain data consistency across Microservices?

**Answer:**
"Since each service has its own database, you can't use a single ACID transaction across services. Instead, consistency is maintained through patterns like the Saga pattern (a sequence of local transactions coordinated via events, with compensating actions on failure), or by designing operations to be eventually consistent — accepting a short window where different services see slightly different states, converging as events propagate."

---

### Q2. What is Eventual Consistency?

**Answer:**
"Instead of every service reflecting a change instantly (like a single-database transaction would), changes propagate asynchronously through events, and for a short period different services can have a temporarily inconsistent view of the same underlying fact — until the event is processed everywhere. The system 'eventually' converges to a consistent state, just not immediately."

```
1. Order Service creates an order, publishes OrderCreated
2. A moment later, Inventory Service (subscribed) decrements stock
3. Briefly: Order shows "created" while Inventory hasn't yet reflected the change
```

**Where to use:** design for it explicitly — idempotent event handlers, UI/UX that tolerates a short delay, and don't assume synchronous read-after-write consistency across service boundaries.

---

### Q3. What is the Saga Pattern?

**Answer:**
"A way to manage a business transaction that spans multiple services, where each service performs its own local transaction and publishes an event/result, triggering the next step. If a step fails partway through, previously completed steps are undone with compensating transactions — since there's no single database transaction to roll back automatically, each step's 'undo' has to be explicitly defined."

```
1. Order Service: create order (local transaction) -> OrderCreated
2. Payment Service: charge card (local transaction) -> PaymentCompleted
3. Inventory Service: reserve stock (local transaction) -> StockReserved
   If step 3 fails: compensate step 2 (refund the charge), compensate step 1 (cancel the order)
```

**Where to use:** any multi-service business process that needs "all steps succeed or effectively undo" behavior — order processing, booking/reservation systems, multi-step onboarding.

---

### Q4. What's the difference between Saga Choreography and Saga Orchestration?

**Answer:**
"Choreography — each service reacts to events from the previous step and publishes its own event next, with no central coordinator; it's fully decentralized. Orchestration — a central orchestrator service explicitly tells each participant what to do next and tracks the overall state of the saga. Choreography is simpler for a few steps but gets hard to reason about as the chain grows (no single place to see the whole flow); orchestration adds a coordinator but gives you one place to see and control the whole process."

```
Choreography:
  Order Service -> OrderCreated event -> Payment Service reacts, charges, publishes PaymentCompleted
  -> Inventory Service reacts, reserves stock, publishes StockReserved
  (no one component "runs" the whole saga)

Orchestration:
  Saga Orchestrator calls Order Service, then calls Payment Service, then calls Inventory Service,
  tracking each result and deciding the next step / triggering compensations on failure.
```

**Where to use:** choreography for a small number of steps with simple dependencies; orchestration once the flow gets complex enough that you need visibility and centralized control over compensation logic.

---

### Q5. How do you handle distributed transactions?

**Answer:**
"Avoid true distributed (two-phase-commit style) transactions across microservices — they're slow, fragile, and reintroduce tight coupling between services that defeats the point of splitting them up. Instead, use the Saga pattern with compensating transactions, design operations to be idempotent so retries are safe, and accept eventual consistency where strict atomicity isn't achievable across service boundaries."

**Where this comes up as a trick question:** "why not just use a distributed transaction (2PC) across services?" — technically possible with some technologies, but considered impractical at scale: it requires all participants to be available and responsive simultaneously, which defeats independent availability, one of the main reasons to use microservices in the first place.
