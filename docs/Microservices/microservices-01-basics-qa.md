# Microservices — 1. Basic Microservices — Interview Q&A

---

### Q1. What are Microservices?

**Answer:**
"An architectural style where an application is built as a set of small, independently deployable services, each owning one specific business capability, its own data, and its own release lifecycle — communicating over the network instead of being compiled into one deployable unit."

**Where to use:** platforms with multiple teams, different scaling needs per capability, and a need to deploy parts of the system independently.

---

### Q2. How is Microservices architecture different from Monolithic architecture?

**Answer:**
"A monolith is one codebase, usually one database, deployed as a single unit — any change means rebuilding and redeploying everything. Microservices split that into independently deployable services, each with its own database, so you can change and deploy one part without touching the rest."

| | Monolith | Microservices |
|---|---|---|
| Deployment unit | Whole app | Per service |
| Database | Usually shared | Own DB per service |
| Scaling | Whole app together | Per service, independently |
| Failure blast radius | Whole app | Contained to the failing service (if designed well) |
| Initial complexity | Lower | Higher (networking, orchestration) |

---

### Q3. What are the advantages of Microservices?

**Answer:**
"Independent deployability — ship one service without a full-app release. Independent scaling — scale only the service under load, not the whole app. Technology flexibility — each service can use the stack that fits it best. Fault isolation — a crash in one service doesn't necessarily take down the rest, if boundaries and resilience patterns are done right. And clearer ownership — a team can fully own a service end-to-end."

---

### Q4. What are the disadvantages or challenges of Microservices?

**Answer:**
"Distributed systems complexity — network calls fail in ways in-process calls don't, so you need retries, timeouts, circuit breakers. Harder debugging — one user request can span five services, requiring distributed tracing to follow. More operational overhead — each service needs its own CI/CD, monitoring, deployment pipeline. And data consistency is harder — no single database transaction spans multiple services, so you deal with eventual consistency."

---

### Q5. When should you choose Microservices over a Monolith?

**Answer:**
"When the team/organization is large enough that independent deployment and ownership boundaries actually pay off, when different parts of the system have very different scaling requirements, or when parts of the system need independent release cadences. For a small team or early-stage product, a well-structured monolith is usually the better starting point — microservices add real operational cost that isn't worth paying before you actually need the independence."

**Where to use judgment:** don't adopt microservices just because it's trendy — adopt them when the coordination/deployment cost of a monolith has become a genuine bottleneck.

---

### Q6. What is Service Independence?

**Answer:**
"Each service can be developed, tested, deployed, scaled, and even fail, without requiring changes to or coordination with other services. That's the core promise of microservices — without it, you just have a 'distributed monolith': many deployables that are still tightly coupled and have to move together."

```
Deploying the Payments service should never require redeploying Orders or Inventory.
```

---

### Q7. What is the Database-per-Service pattern?

**Answer:**
"Each microservice owns and exclusively accesses its own database — no other service is allowed to read or write it directly. If another service needs that data, it goes through the owning service's API. This enforces a real boundary between services instead of just an organizational one, and lets each service evolve its schema independently."

```
Order Service  --> Orders DB     (only Order Service touches this)
Party Service  --> Contacts DB   (only Party Service touches this)
```

**Where this comes up as a trick question:** sharing a database across services is called a "distributed monolith" anti-pattern — it looks like microservices but keeps the same tight coupling.

---

### Q8. What is an API Gateway and why is it used?

**Answer:**
"A single entry point in front of all the backend microservices, handling cross-cutting concerns — authentication, rate limiting, request routing to the right service, sometimes aggregating multiple service calls into one response. It means external clients don't need to know the internal service topology."

**Where to use:** any system exposing more than a handful of services to external clients — avoids clients needing every service's address and duplicating auth logic per client. Common .NET options: Ocelot, YARP.

---

### Q9. What is Service Discovery?

**Answer:**
"A mechanism for services to find each other's network location dynamically, instead of hardcoding IP addresses/ports. In a containerized environment, service instances come and go constantly (scaling, restarts, deployments) — service discovery keeps track of who's currently available and where, so callers always resolve to a live instance."

```
Order Service asks: "where is Inventory Service right now?"
Service Registry (or Kubernetes DNS) answers with a current, healthy instance address.
```

**Where to use:** Kubernetes provides this out of the box via DNS-based service names; standalone tools like Consul or Eureka do the same job outside Kubernetes.

---

### Q10. What is Load Balancing in Microservices?

**Answer:**
"Distributing incoming requests across multiple instances of the same service, so no single instance gets overwhelmed and the system can handle more traffic by adding more instances. In Kubernetes, a Service object load-balances across all healthy Pods behind it automatically; a reverse proxy (NGINX, an API Gateway) can also do this at the edge."

```
Client --> Load Balancer --> [Order Service Pod 1, Pod 2, Pod 3] (requests spread across all three)
```

**Where to use:** any service expected to run more than one instance for scale or availability — load balancing is what makes running multiple instances actually useful.
