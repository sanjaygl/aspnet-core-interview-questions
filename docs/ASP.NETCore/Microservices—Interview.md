# Microservices — Interview Q&A

---

### Q1. What is a microservice architecture?

**Answer:**
"It's an approach where an application is built as a collection of small, independent services, each responsible for one specific business capability, each with its own codebase, its own database, and its own deployment lifecycle. They communicate with each other over the network — usually REST/HTTP, gRPC, or async messaging — instead of being compiled and deployed together as one big application."

**Where to use:** when different parts of a system have very different scaling needs, release cadences, or ownership boundaries — e.g., a large platform where separate teams each own a distinct capability.

---

### Q2. How is that different from a monolith?

**Answer:**
"A monolith is a single deployable application — one codebase, usually one database, all features compiled and deployed together. Changing one small feature means rebuilding and redeploying the whole thing. Microservices split that into independently deployable pieces — you can deploy the Orders service without touching the Payments service at all."

| | Monolith | Microservices |
|---|---|---|
| Deployment | One unit, deployed together | Independent, per-service |
| Database | Usually one shared database | Each service owns its own database |
| Scaling | Scale the whole app together | Scale each service independently |
| Team ownership | Often one team, whole app | Different teams can own different services |
| Failure blast radius | A bug can affect the whole app | Contained more to the failing service (if designed well) |
| Complexity | Simpler to build/debug/deploy early on | More operational complexity — networking, deployment, monitoring |

---

### Q3. Can you give a real example of a microservice from your own experience?

**Answer:**
"The Party service — it's described as 'Contacts as a Service' for the Duck Creek platform. Instead of every other service maintaining its own copy of contact data (people, businesses, addresses, phone numbers), Party owns that single responsibility and exposes it through REST and WCF APIs. It's multi-tenant, containerized with Docker, and runs in Kubernetes, with its own SQL Server database — a textbook example of a microservice: one clear business capability, its own data, its own deployment, consumed by other services over the network instead of being baked into them."

**Where this comes up:** interviewers like a concrete example over a textbook definition — this one demonstrates single-responsibility, independent deployability, and multi-tenancy in one answer.

---

### Q4. How do microservices typically communicate with each other?

**Answer:**
"Two broad patterns: synchronous and asynchronous. Synchronous is a direct call — REST/HTTP or gRPC — where the caller waits for a response, used when you need an immediate answer (e.g., 'give me this contact's address right now'). Asynchronous is message/event-based — through a message broker like RabbitMQ, Kafka, or Azure Service Bus — where a service publishes an event and doesn't wait for a response, used for things like 'notify other services that an order was placed' without coupling them together in real time."

```
Synchronous:  Order Service --(HTTP GET /parties/42)--> Party Service
Asynchronous: Order Service --(publishes "OrderCreated" event)--> Message Broker --> Inventory Service, Billing Service (subscribe, react independently)
```

**Where to use:** synchronous for request/response needs where you need the answer now; asynchronous for decoupling services that just need to react to something happening, without blocking the sender or creating a tight dependency.

---

### Q5. Why does each microservice need its own database instead of sharing one?

**Answer:**
"Because a shared database creates hidden coupling — if two services both read/write the same tables directly, you can't change one service's schema without risking breaking the other, which defeats the whole point of independent deployability. Each service owning its own database means the only way another service can get that data is through the owning service's API — enforcing a real boundary, not just an organizational one."

**Where this comes up as a trick question:** "can two microservices share a database?" — technically possible, but it's considered an anti-pattern ('distributed monolith') because it reintroduces the tight coupling microservices are meant to remove.

---

### Q6. What's the API Gateway pattern, and why is it used?

**Answer:**
"Instead of a client (like a web/mobile frontend) calling a dozen different microservices directly, an API Gateway sits in front of all of them as a single entry point. It handles cross-cutting concerns — authentication, rate limiting, routing requests to the right backend service, sometimes aggregating responses from multiple services into one — so clients don't need to know about the internal service topology at all."

**Where to use:** any system with more than a handful of microservices exposed to external clients — avoids clients needing to know every service's address and duplicating auth/logging logic per client.

---

### Q7. What's "eventual consistency," and why does it matter in microservices?

**Answer:**
"In a monolith with one database, a transaction can update multiple tables atomically — all-or-nothing. Across microservices, each with its own database, there's no single transaction spanning all of them. So instead, changes propagate through events, and for a short window, different services can have slightly different, temporarily inconsistent views of the data — until the event has been processed everywhere. That's eventual consistency: the system converges to a consistent state, just not instantly."

```
1. Order Service creates an order, publishes "OrderCreated"
2. Inventory Service (subscribed) decrements stock — a moment later, not instantly
3. Briefly, Order shows "created" while Inventory hasn't yet reflected the stock change
```

**Where to use:** design for it explicitly — idempotent event handlers, retries, and UI/UX that tolerates a short delay — rather than trying to force strict consistency across service boundaries.

---

### Q8. What's the Circuit Breaker pattern, and why is it needed in microservices?

**Answer:**
"When Service A calls Service B and B is slow or down, naively retrying or waiting can pile up requests and threads on A, potentially cascading the failure outward. A circuit breaker tracks failures — after enough of them, it 'trips' and starts failing fast (or returning a fallback) instead of calling the failing service at all, giving it time to recover, and protecting the caller from being dragged down too. After a cooldown, it lets a few requests through to test if the dependency has recovered."

**Where to use:** any synchronous call from one microservice to another — implemented in .NET with libraries like Polly, wrapping outbound HTTP calls with retry + circuit breaker policies.

---

### Q9. What are the main downsides / costs of microservices?

**Answer:**
"Distributed systems complexity — network calls can fail in ways in-process calls can't, so you need retries, timeouts, circuit breakers. Harder to debug — a single user request might touch five services, so you need distributed tracing (correlation IDs, tools like OpenTelemetry) to follow it end-to-end. More operational overhead — each service needs its own CI/CD pipeline, monitoring, and deployment, which is real infrastructure investment. And data consistency gets harder, as covered above. Microservices trade simplicity for independent scalability/deployability — worth it at a certain scale/team size, but real overhead if adopted too early."

**Where to use judgment:** a small team or early-stage product is usually better served by a well-structured monolith first — microservices pay off once team size, deployment frequency, or scaling needs genuinely require independent services.

---

### Q10. How does containerization (Docker) and orchestration (Kubernetes) relate to microservices?

**Answer:**
"They're not required for microservices, but they're the standard way to run them in practice. Docker packages each service and its dependencies into a portable, consistent container image. Kubernetes then manages running many of those containers across a cluster — handling scaling (more pods under load), self-healing (restarting crashed containers), service discovery (so services can find each other by name), and rolling deployments (updating one service without downtime). Without something like Kubernetes, manually managing dozens of independently-deployed services would be extremely painful."

---

### Quick one-liner if asked to summarize

> "Microservices split an application into small, independently deployable services, each owning one business capability and its own data, communicating over the network instead of in-process. It trades the simplicity of a monolith for independent scalability and deployability — at the cost of distributed-systems complexity: network failures, eventual consistency, and the operational overhead of running many services, usually managed with Docker and Kubernetes."
 