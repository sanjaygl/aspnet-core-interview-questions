# Microservices — 9. Observability — Interview Q&A

---

### Q1. How do you monitor Microservices?

**Answer:**
"Through three complementary pillars: metrics (numeric time-series data like request rate, error rate, latency, CPU/memory — good for dashboards and alerting on thresholds), logs (detailed, timestamped events from each service, centralized so you can search across all services at once), and distributed tracing (following a single request as it flows through multiple services, to see where time is spent or where it failed). No single one is enough on its own — metrics tell you *that* something's wrong, logs and traces help you find *why*."

---

### Q2. What is Distributed Tracing?

**Answer:**
"A way of following a single logical request end-to-end as it passes through multiple microservices, by attaching a shared trace ID (and per-hop span IDs) that gets propagated across every service call. Tools like OpenTelemetry, Jaeger, or Zipkin collect these spans and let you visualize the whole request as a timeline — which service was called, how long each step took, and where in the chain a failure or slowdown happened."

```
Trace: abc-123
  Span 1: API Gateway (2ms)
    Span 2: Order Service (50ms)
      Span 3: Inventory Service call (30ms)
      Span 4: Payment Service call (15ms)
```

**Where to use:** essential once a single user request can touch 3+ services — without tracing, diagnosing "why was this request slow" means manually correlating logs across every service by hand.

---

### Q3. What is Centralized Logging?

**Answer:**
"Instead of each service's logs staying only on its own instance/container (which disappears when the container restarts or scales down), every service ships its logs to one central store where they can all be searched and correlated together. This is essential in microservices, where a single user action might generate log entries across five different services — you need to see them all in one place, in order, to understand what happened."

**Where to use:** every service, from day one — retrofitting centralized logging after an incident, when you actually need it, is too late.

---

### Q4. What tools have you used for monitoring and observability? (ELK, Prometheus, Grafana, etc.)

**Answer:**
"ELK stack (Elasticsearch, Logstash, Kibana) — centralized log aggregation, search, and visualization; commonly paired with Filebeat/Fluentd shipping logs from each container. Prometheus — a metrics collection and time-series database, typically scraping `/metrics` endpoints from each service at intervals. Grafana — dashboards and alerting, usually visualizing Prometheus (or other) data sources. Together: Prometheus + Grafana for metrics/dashboards/alerts, ELK for log search, and something like Jaeger/OpenTelemetry for tracing."

---

### Q5. What are Correlation IDs?

**Answer:**
"A unique identifier generated at the start of a request (often at the API Gateway or the first service that receives it) and passed along with every subsequent call to other services and every log line written along the way. It lets you search logs across every service involved in that one request and reconstruct the full picture, even though the services themselves have no other direct relationship to each other."

```csharp
// Middleware: generate or forward a correlation ID
var correlationId = context.Request.Headers["X-Correlation-Id"].FirstOrDefault() ?? Guid.NewGuid().ToString();
context.Response.Headers["X-Correlation-Id"] = correlationId;
// ... propagate it to any downstream HTTP calls, and include it in every log line
```

**Where to use:** every incoming request at the edge — generate one if the caller didn't supply one, propagate it through every downstream call and log statement.
