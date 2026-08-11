# Microservices — 4. Communication — Interview Q&A

---

### Q1. REST vs gRPC vs Messaging — when would you use each?

**Answer:**
"REST/HTTP with JSON is the default for external-facing or general-purpose service-to-service calls — human-readable, widely supported, easy to debug. gRPC is a binary, contract-first RPC protocol over HTTP/2 — much faster and more compact than REST/JSON, with strongly-typed contracts (protobuf), and supports streaming — good for high-throughput internal service-to-service calls where performance matters and you control both ends. Messaging (a broker) is for asynchronous, decoupled, event-driven communication where the sender doesn't need or want an immediate response, and possibly multiple consumers need to react independently."

| | REST | gRPC | Messaging |
|---|---|---|---|
| Style | Request/response | Request/response (+ streaming) | Publish/subscribe, async |
| Format | JSON (text) | Protobuf (binary) | Varies (JSON, Avro, protobuf) |
| Speed | Slower (text, HTTP/1.1 typical) | Faster (binary, HTTP/2) | N/A — not request/response |
| Coupling | Caller waits, tightly coupled in time | Caller waits, tightly coupled in time | Decoupled — sender doesn't wait |
| Best for | Public APIs, general service calls | High-performance internal calls | Event-driven workflows, decoupling |

---

### Q2. What message brokers have you used? (Kafka, RabbitMQ, Azure Service Bus)

**Answer:**
"Depends on the workload. RabbitMQ is a traditional message broker — great for task queues and point-to-point messaging with flexible routing (exchanges, topics). Kafka is built for high-throughput event streaming — it retains events on disk for a configurable period, so consumers can replay history, and it scales to very high message volumes, commonly used for event sourcing and analytics pipelines. Azure Service Bus is a managed cloud broker with queues and topics, well-integrated into the Azure ecosystem, good default choice when already on Azure and not needing Kafka-scale throughput."

---

### Q3. What is the difference between a Message Queue and Event Streaming?

**Answer:**
"A message queue (like RabbitMQ, Azure Service Bus Queues) typically delivers each message to exactly one consumer and removes it once processed — it's a work-distribution model, good for 'do this task exactly once.' Event streaming (like Kafka) retains a durable, ordered log of events that multiple independent consumers can read at their own pace, and can replay from any point — it's a 'here's everything that happened' model, good for multiple services needing to react to the same events independently, or reprocessing history."

```
Queue: OrderTasks queue -> ONE worker picks up and processes each task, then it's gone
Stream: OrderEvents topic -> Inventory AND Billing AND Analytics each independently read the same events,
        at their own pace, and could replay from the beginning if needed
```

---

### Q4. What is a Dead Letter Queue (DLQ)?

**Answer:**
"A separate queue where messages get routed after they fail processing repeatedly (exceeding a retry limit) instead of being lost or retried forever. It lets you inspect, fix, and potentially reprocess failed messages later without blocking the main queue on a poison message that will never succeed."

```
Main queue: OrderTasks -> consumer tries to process, fails 5 times ->
Dead Letter Queue: OrderTasks-dlq -> message parked here for manual inspection/reprocessing
```

**Where to use:** any queue-based processing pipeline — without a DLQ, a single malformed/poison message can either be dropped silently or retried forever, blocking everything behind it.
