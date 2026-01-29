# C# Interview Questions - 100 Senior/Principal Level Questions

> **Target Audience**: Senior Software Engineers, Architects, Technical Leads (11+ years experience)  
> **Focus**: Real-world scenarios, decision-making, trade-offs, and production-level considerations

---

## 📑 Table of Contents - All 100 Questions

### Batch 1: CLR, Memory Management & Fundamentals (Questions 1-10)

001. [CLR execution process and JIT compilation](#question-1-explain-the-clr-execution-process-from-compilation-to-runtime-how-does-jit-compilation-work)
002. [Stack vs Heap memory allocation](#question-2-explain-stack-vs-heap-memory-allocation-when-does-a-reference-type-get-allocated-on-the-stack)
003. [Garbage Collection - Generations, LOH, and tuning](#question-3-explain-garbage-collection-in-net-what-are-generations-and-loh)
004. [Value types vs Reference types - Boxing/Unboxing](#question-4-explain-value-types-vs-reference-types-what-is-boxingunboxing-and-performance-impact)
005. [async/await internals and state machine](#question-5-explain-asyncawait-how-does-the-compiler-transform-async-methods)
006. [Task vs Thread - When to use each](#question-6-explain-task-vs-thread-when-should-you-use-each)
007. [LINQ deferred execution and query optimization](#question-7-explain-linq-deferred-execution-how-to-optimize-linq-queries)
008. [Delegates, Events, and multicast delegates](#question-8-explain-delegates-events-and-multicast-delegates-when-to-use-each)
009. [ref, out, in parameters - Use cases](#question-9-explain-ref-out-and-in-parameters-when-to-use-each)
010. [Expression Trees - How LINQ to SQL/EF works](#question-10-explain-expression-trees-how-does-linq-to-sqlef-work-internally)

### Batch 2: Advanced Language Features (Questions 11-20)

011. [IEnumerable vs IQueryable - Database query differences](#question-11-explain-ienumerable-vs-iqueryable-when-to-use-each-for-database-queries)
012. [Extension methods - Design and limitations](#question-12-explain-extension-methods-how-to-design-fluent-apis)
013. [Covariance and Contravariance in generics](#question-13-explain-covariance-and-contravariance-in-c-generics)
014. [Reflection and Attributes - Use cases and performance](#question-14-explain-reflection-and-attributes-use-cases-and-performance-concerns)
015. [Dynamic keyword vs Reflection - Trade-offs](#question-15-explain-dynamic-keyword-vs-reflection-when-to-use-each)
016. [yield return and IEnumerable - State machine](#question-16-explain-yield-return-and-ienumerable-how-does-state-machine-work)
017. [Tuples, Deconstruction, Pattern Matching](#question-17-explain-tuples-deconstruction-and-pattern-matching-in-modern-c)
018. [Records vs Classes - Immutability and value equality](#question-18-explain-records-vs-classes-when-to-use-records)
019. [Span<T> and Memory<T> - Zero-copy operations](#question-19-explain-spant-and-memoryt-when-to-use-for-performance)
020. [Nullable reference types - Null safety](#question-20-explain-nullable-reference-types-how-to-enable-null-safety)

### Batch 3: Design Patterns & SOLID (Questions 21-30)

021. [SOLID principles with real-world examples](#question-21-explain-solid-principles-with-real-world-examples)
022. [Dependency Injection - Scopes and lifetimes](#question-22-explain-dependency-injection-in-aspnet-core-scopes-and-lifetimes)
023. [Singleton pattern - Thread-safe implementation](#question-23-explain-singleton-pattern-how-to-implement-thread-safe-singleton)
024. [Factory pattern vs Abstract Factory](#question-24-explain-factory-pattern-vs-abstract-factory-when-to-use-each)
025. [Repository pattern with EF Core](#question-25-explain-repository-pattern-with-ef-core-pros-and-cons)
026. [Strategy pattern - Behavior composition](#question-26-explain-strategy-pattern-how-to-implement-in-c)
027. [Decorator pattern - Adding behavior dynamically](#question-27-explain-decorator-pattern-how-to-use-with-aspnet-core)
028. [Observer pattern - Event-driven systems](#question-28-explain-observer-pattern-how-does-it-relate-to-c-events)
029. [Chain of Responsibility - Request processing](#question-29-explain-chain-of-responsibility-pattern-with-aspnet-core-middleware)
030. [Command pattern - CQRS foundation](#question-30-explain-command-pattern-how-does-it-relate-to-cqrs)

### Batch 4: ASP. NET Core & Web APIs (Questions 31-40)

031. [ASP.NET Core middleware pipeline](#question-31-explain-aspnet-core-middleware-pipeline-how-to-create-custom-middleware)
032. [Dependency Injection - AddScoped vs AddSingleton vs AddTransient](#question-32-explain-di-container-in-aspnet-core-addscoped-vs-addsingleton-vs-addtransient)
033. [Model binding and validation](#question-33-explain-model-binding-and-validation-in-aspnet-core)
034. [Filters - Authorization, Action, Exception filters](#question-34-explain-filters-in-aspnet-core-types-and-execution-order)
035. [JWT authentication and authorization](#question-35-explain-jwt-authentication-in-aspnet-core-how-to-implement)
036. [Advanced LINQ operators and performance](#question-36-explain-advanced-linq-operators-selectmany-groupby-join)
037. [EF Core change tracking and performance](#question-37-explain-ef-core-change-tracking-how-to-optimize-for-performance)
038. [Unit testing with xUnit, Moq, and FluentAssertions](#question-38-explain-unit-testing-best-practices-with-xunit-moq-fluentassertions)
039. [TDD approach and benefits](#question-39-explain-test-driven-development-tdd-how-to-practice-in-real-projects)
040. [ConfigureAwait(false) - When and why](#question-40-explain-configureavaitfalse-when-and-why-to-use-it)

### Batch 5: Microservices & Distributed Systems (Questions 41-50)

041. [gRPC vs REST - When to use each](#question-41-explain-grpc-vs-rest-when-to-use-each-for-microservices)
042. [Message queues - RabbitMQ patterns](#question-42-explain-message-queues-rabbitmq-patterns-and-use-cases)
043. [CQRS pattern - Command/Query separation](#question-43-explain-cqrs-pattern-when-to-use-command-query-separation)
044. [Event Sourcing - Event store implementation](#question-44-explain-event-sourcing-how-to-implement-event-store)
045. [Distributed caching with Redis](#question-45-explain-distributed-caching-with-redis-use-cases-and-patterns)
046. [Circuit Breaker pattern with Polly](#question-46-explain-circuit-breaker-pattern-how-to-implement-with-polly)
047. [API Gateway pattern - Routing and aggregation](#question-47-explain-api-gateway-pattern-benefits-and-implementation)
048. [Service discovery and load balancing](#question-48-explain-service-discovery-and-load-balancing-in-microservices)
049. [Distributed transactions - Saga pattern](#question-49-explain-distributed-transactions-saga-pattern-implementation)
050. [Message idempotency in distributed systems](#question-50-explain-message-idempotency-how-to-handle-duplicate-messages)

### Batch 6: Performance & Optimization (Questions 51-60)

051. [Async best practices - Avoiding deadlocks](#question-51-explain-async-best-practices-how-to-avoid-deadlocks)
052. [Memory leaks - Detection and prevention](#question-52-explain-memory-leaks-in-c-how-to-detect-and-prevent)
053. [String optimization - StringBuilder vs String](#question-53-explain-string-optimization-stringbuilder-vs-string-interpolation)
054. [Collection performance - List vs HashSet vs Dictionary](#question-54-explain-collection-performance-list-vs-hashset-vs-dictionary)
055. [Lazy loading vs Eager loading in EF Core](#question-55-explain-lazy-loading-vs-eager-loading-in-ef-core)
056. [Caching strategies - Cache-aside, Write-through](#question-56-explain-caching-strategies-cache-aside-write-through-write-behind)
057. [Profiling and benchmarking with BenchmarkDotNet](#question-57-explain-profiling-and-benchmarking-with-benchmarkdotnet)
058. [ThreadPool and Task Parallel Library](#question-58-explain-threadpool-and-task-parallel-library-tpl)
059. [Lock-free programming with Interlocked](#question-59-explain-lock-free-programming-with-interlocked-operations)
060. [Source Generators - Compile-time code generation](#question-60-explain-source-generators-how-to-generate-code-at-compile-time)

### Batch 7: Testing & Quality (Questions 61-65)

061. [Integration testing with WebApplicationFactory](#question-61-explain-integration-testing-with-webapplicationfactory-in-aspnet-core)
062. [Mocking external dependencies](#question-62-explain-mocking-external-dependencies-best-practices)
063. [Test data builders pattern](#question-63-explain-test-data-builders-pattern-for-readable-tests)
064. [Mutation testing for test quality](#question-64-explain-mutation-testing-how-to-verify-test-quality)
065. [Contract testing for microservices](#question-65-explain-contract-testing-for-microservices-with-pact)

### Batch 8: Modern . NET Features (Questions 66-70)

066. [GraphQL vs REST - Trade-offs](#question-66-explain-graphql-vs-rest-when-to-use-graphql)
067. [SignalR for real-time communication](#question-67-explain-signalr-how-to-implement-real-time-communication)
068. [Background services with IHostedService](#question-68-explain-background-services-with-ihostedservice-in-aspnet-core)
069. [Docker containerization for .NET apps](#question-69-explain-docker-containerization-for-net-applications)
070. [Kubernetes deployment for .NET microservices](#question-70-explain-kubernetes-deployment-for-net-microservices)

### Batch 9: Cloud & DevOps (Questions 71-75)

071. [Minimal APIs in .NET 6+](#question-71-explain-minimal-apis-in-net-6-when-to-use-vs-controllers)
072. [Azure Service Bus vs Azure Event Grid](#question-72-explain-azure-service-bus-vs-azure-event-grid-when-to-use-each)
073. [Azure Functions - Serverless patterns](#question-73-explain-azure-functions-serverless-patterns-and-use-cases)
074. [Blazor - Server vs WebAssembly](#question-74-explain-blazor-server-vs-webassembly-when-to-use-each)
075. [Performance optimization with BenchmarkDotNet](#question-75-explain-performance-optimization-with-benchmarkdotnet-example)

### Batch 10: Advanced Patterns (Questions 76-80)

076. [Strategy pattern for algorithm selection](#question-76-explain-strategy-pattern-for-algorithm-selection)
077. [Logging with Serilog and structured logging](#question-77-explain-logging-with-serilog-structured-logging-best-practices)
078. [Application Insights for monitoring](#question-78-explain-application-insights-for-monitoring-net-applications)
079. [Saga pattern for distributed transactions](#question-79-explain-saga-pattern-for-distributed-transactions)
080. [Observer pattern and reactive programming](#question-80-explain-observer-pattern-and-reactive-programming-with-rx)

### Batch 11: Architectural Patterns (Questions 81-85)

081. [ValueTask vs Task - Performance optimization](#question-81-explain-valuetask-vs-task-when-to-use-valuetask)
082. [System.Threading.Channels - Producer/consumer](#question-82-explain-systemthreadingchannels-producer-consumer-pattern)
083. [Outbox pattern - Reliable messaging](#question-83-explain-outbox-pattern-for-reliable-messaging)
084. [Proxy pattern - Virtual, protection, smart proxies](#question-84-explain-proxy-pattern-virtual-protection-and-smart-proxies)
085. [Global error handling in ASP.NET Core](#question-85-explain-global-error-handling-in-aspnet-core)

### Batch 12: Data Access & Architecture (Questions 86-90)

086. [Code-First vs Database-First in EF Core](#question-86-explain-code-first-vs-database-first-in-ef-core-when-to-use-each)
087. [Template Method pattern - Algorithm skeleton](#question-87-explain-the-template-method-pattern-how-is-it-used-in-aspnet-core)
088. [Rate limiting and throttling in APIs](#question-88-explain-rate-limiting-and-throttling-in-apis-how-to-implement-with-aspnetcoreratelimit)
089. [Builder pattern with Fluent APIs](#question-89-explain-the-builder-pattern-how-is-it-used-with-fluent-apis)
090. [Database connection pooling](#question-90-explain-database-connection-pooling-how-does-it-work-in-adonet-and-ef-core)

### Batch 13: Caching & Performance (Questions 91-95)

091. [IMemoryCache vs IDistributedCache](#question-91-explain-imemorycache-vs-idistributedcache-when-to-use-each)
092. [Mediator pattern and MediatR library](#question-92-explain-the-mediator-pattern-and-mediatr-library-when-to-use-it)
093. [API versioning strategies](#question-93-explain-versioning-strategies-for-apis-how-to-implement-in-aspnet-core)
094. [Specification pattern with EF Core](#question-94-explain-the-specification-pattern-how-to-use-it-with-ef-core)
095. [Health checks in ASP.NET Core](#question-95-explain-health-checks-in-aspnet-core-how-to-implement-custom-health-checks)

### Batch 14: Final Advanced Topics (Questions 96-100)

096. [Unit of Work pattern - EF Core implementation](#question-96-explain-the-unit-of-work-pattern-how-does-ef-core-implement-it)
097. [Feature flags/toggles - Runtime feature control](#question-97-explain-feature-flagstoggles-how-to-implement-in-net)
098. [Background tasks with IHostedService and BackgroundService](#question-98-explain-background-tasks-with-ihostedservice-and-backgroundservice-when-to-use-each)
099. [Message idempotency in distributed systems](#question-99-explain-message-idempotency-how-to-implement-in-distributed-systems)
100. [Eventual consistency in distributed systems](#question-100-explain-eventual-consistency-in-distributed-systems-how-to-handle-in-net)

---

## Question 1: Explain the CLR execution process from compilation to runtime. How does JIT compilation work?

**Answer:**
* C# code compiles to CIL (Common Intermediate Language) stored in assemblies with metadata, enabling language interoperability and platform independence
* When you execute an assembly, the CLR loads it, verifies the CIL for type safety, and the JIT compiler converts CIL to native machine code just before method execution
* JIT compilation occurs per-method on first call—subsequent calls use the cached native code, eliminating re-compilation overhead
* Two JIT modes exist: Normal JIT (default) compiles methods on demand, while Pre-JIT (NGen/ReadyToRun) compiles ahead-of-time to reduce startup latency
* RyuJIT is the current 64-bit compiler optimizing for throughput with techniques like inlining, loop unrolling, and dead code elimination
* Tiered compilation in modern . NET initially generates quick, unoptimized code for fast startup, then recompiles hot paths with aggressive optimizations after profiling runtime behavior

**Interview Notes:**
* Expect questions about startup performance trade-offs—explain ReadyToRun for containerized apps with fast cold-start requirements
* Be ready to discuss why reflection-heavy code or large method bodies can increase JIT overhead
* Tiered compilation questions probe understanding of how . NET balances startup time versus steady-state performance

---

## Question 2: Explain stack vs heap memory allocation. When does a reference type get allocated on the stack?

**Answer:**
* Stack allocates value types and method parameters/local variables in a LIFO structure—allocation is pointer increment, deallocation is automatic on scope exit
* Heap stores reference type instances managed by the garbage collector, requiring allocation overhead and eventual GC pressure
* Reference types allocated on stack occur via escape analysis optimization when the JIT proves the object doesn't escape the method scope—this is rare and not guaranteed
* `Span<T>` and `stackalloc` explicitly enable stack allocation of array-like data, providing zero-allocation performance for short-lived buffers
* Closures capturing variables force heap allocation of display classes, even for value types—this affects lambda and LINQ performance in hot paths

**Interview Notes:**
* Interviewers often ask about `ref struct` types like `Span<T>` that cannot be boxed or placed on the heap
* Clarify that "reference types on stack" typically refers to the reference itself (pointer), not the object
* Discuss trade-offs: stack overflow risk vs heap GC pressure in high-throughput scenarios

---

## Question 3: Explain garbage collection in . NET. What are generations and LOH?

**Answer:**
* GC uses generational collection based on the weak generational hypothesis: most objects die young, so Gen0 collections are frequent and fast
* Gen0 collects newly allocated objects, survivors promote to Gen1, and long-lived objects reach Gen2—Gen2 collections are expensive full heap scans
* Large Object Heap (LOH) stores objects ≥85KB, isn't compacted by default (causes fragmentation), and collected only during Gen2 GC
* Workstation GC pauses threads during collection; Server GC uses dedicated threads per core for higher throughput at the cost of increased memory
* . NET 5+ supports `GCSettings.LatencyMode` to tune for low-latency (interactive) vs high-throughput (batch) scenarios
* LOH compaction can be enabled via `GCSettings.LargeObjectHeapCompactionMode` when fragmentation becomes problematic

**Interview Notes:**
* Expect deep dives into GC tuning for specific scenarios—microservices need different settings than desktop apps
* Discuss memory leaks from event handlers, static references, or finalizers preventing collection
* Know when to use `ArrayPool<T>` or object pooling to reduce Gen2/LOH pressure in high-allocation code

---

## Question 4: Explain value types vs reference types. What is boxing/unboxing and performance impact?

**Answer:**
* Value types store data directly, are stack-allocated for locals, and copied by value—reference types store heap pointers and are copied by reference
* Boxing wraps a value type in a heap-allocated object to satisfy reference type constraints (interfaces, object parameters)—unboxing extracts the value with type checking
* Each box allocates 12-16 bytes overhead plus the value size, creating GC pressure in loops or collections
* Generic collections (`List<T>`) avoid boxing by maintaining type information, whereas non-generic collections (`ArrayList`) box every value type insertion
* Implementing interfaces on structs causes boxing when called via interface reference, but direct struct method calls remain unboxed

**Interview Notes:**
* Highlight that boxing often occurs silently—string concatenation with value types, non-generic collections, and LINQ over non-generic sources
* Discuss `Nullable<T>` as a special case where boxing a null value produces a null reference, not a boxed object
* Performance-sensitive code should measure boxing with profilers, especially in serialization or reflection scenarios

---

## Question 5: Explain async/await. How does the compiler transform async methods?

**Answer:**
* Compiler rewrites async methods into state machines (generated struct implementing `IAsyncStateMachine`) that track execution state across await points
* Each await creates a suspension point—the method returns a Task immediately, and execution resumes on the captured context when the awaited operation completes
* State machines avoid stack frame retention by storing locals as fields, enabling long-running operations without thread blocking
* SynchronizationContext or TaskScheduler determines resume context—ASP. NET Core has no context, so continuations run on arbitrary thread pool threads
* Avoid `async void` except for event handlers—unhandled exceptions crash the process since there's no Task to observe

**Interview Notes:**
* Expect questions on ConfigureAwait(false) and why it's unnecessary in ASP. NET Core but critical in UI frameworks
* Clarify that async doesn't mean parallel—it's about non-blocking I/O, not CPU-bound work
* Discuss state machine overhead: small async methods might allocate more than synchronous equivalents, measurable via heap profiling

---

## Question 6: Explain Task vs Thread. When should you use each?

**Answer:**
* Threads are OS-level execution contexts with dedicated stacks (1MB default), expensive to create and destroy—use for long-running CPU-bound work that justifies overhead
* Tasks are lightweight abstractions over thread pool work items, supporting composition (ContinueWith, WhenAll), cancellation, and exception propagation
* Thread pool manages a fixed set of worker threads, reusing them for queued Tasks to avoid creation overhead—ideal for short-lived async operations
* Use `Task.Run` for CPU-bound work needing background execution, but never for I/O-bound work (defeats async/await efficiency)
* Direct Thread instantiation is rare in modern . NET—relevant only for thread affinity requirements like STAThread for COM interop

**Interview Notes:**
* Interviewers probe understanding of thread pool starvation when blocking threads or using Task. Run excessively in hot paths
* Clarify that Tasks don't guarantee thread allocation—completed Tasks or cached results return synchronously
* Discuss `TaskCreationOptions.LongRunning` to force dedicated thread creation outside the pool for truly long-running operations

---

## Question 7: Explain LINQ deferred execution. How to optimize LINQ queries?

**Answer:**
* Deferred execution means query definition doesn't execute immediately—enumeration (foreach, ToList, Count) triggers actual iteration over the source
* Each enumeration re-executes the query—calling Count() then ToList() iterates twice, potentially hitting the database twice for IQueryable
* IEnumerable executes in-memory with LINQ-to-Objects, while IQueryable translates expressions to provider-specific queries (SQL for EF Core)
* Materializing with ToList() at query boundaries prevents redundant execution and makes iteration costs predictable
* Avoid chaining where clauses in loops—each adds a deferred filter layer, creating nested enumerators with allocation overhead
* Use query syntax for complex joins/grouping (flattens to method chains) but stick to method syntax for simple operations and consistency

**Interview Notes:**
* Expect questions about N+1 queries from deferred execution inside loops—explain eager loading with Include()
* Discuss AsNoTracking() to disable EF change tracking for read-only queries, improving performance by 20-30%
* Clarify that Select projections after filtering reduce memory by materializing only needed columns, especially for large result sets

---

## Question 8: Explain delegates, events, and multicast delegates. When to use each?

**Answer:**
* Delegates are type-safe function pointers enabling callback patterns and decoupling—they're reference types storing method references and optional target objects
* Multicast delegates maintain an invocation list, calling subscribers sequentially—return values are lost except for the last invocation
* Events are delegates with restricted access (add/remove only outside the declaring class), preventing external invocation or reassignment that could break encapsulation
* Use delegates for strategy patterns or callbacks where a single subscriber is expected; use events for publish-subscribe with multiple listeners
* Event memory leaks occur when subscribers outlive publishers and aren't unsubscribed—short-lived subscribers on long-lived publishers are dangerous
* WeakEventManager or weak references prevent leaks but add complexity—design for explicit unsubscription in critical paths

**Interview Notes:**
* Interviewers often ask about event handler leaks in UI frameworks or ASP. NET Core middleware
* Clarify that events can't be invoked externally (`obj.Event()` is invalid), but delegates can—this enforces proper encapsulation
* Discuss performance: multicast delegates iterate synchronously, blocking the publisher until all handlers complete

---

## Question 9: Explain ref, out, and in parameters. When to use each?

**Answer:**
* `ref` passes references to existing variables, allowing methods to read and modify the original—caller must initialize before passing
* `out` is write-only from the method's perspective, requiring assignment before return—useful for multiple return values without tuples
* `in` passes read-only references for large structs, preventing defensive copies while disallowing modification—reduces stack copying for value types
* Use `ref` for bidirectional communication,  `out` for multiple outputs (though tuples are often clearer), and `in` for passing large read-only structs efficiently
* `ref` returns enable returning references to array elements or fields without copying, but they require careful lifetime management to avoid dangling references

**Interview Notes:**
* Expect questions about defensive copies: calling instance methods on `in` parameters can trigger copies if the method isn't readonly
* Discuss `ref struct` types like Span<T> that can only live on the stack, preventing heap allocation and boxing
* Clarify that `out` variables can be declared inline (`method(out var result)`) as of C# 7, improving readability

---

## Question 10: Explain expression trees. How does LINQ to SQL/EF work internally?

**Answer:**
* Expression trees represent code as data structures (AST) rather than executable IL—`Expression<Func<T, bool>>` describes a lambda without compiling it
* EF Core translates expression trees to SQL by visiting nodes and mapping C# operations to SQL equivalents—filters become WHERE clauses, projections become SELECT columns
* Expressions are immutable—visitors create new trees rather than modifying existing ones, enabling safe transformation pipelines
* Expressions support compile-time type checking while remaining inspectable at runtime, unlike compiled delegates
* Unsupported operations (custom methods, complex logic) can't translate and throw runtime exceptions—use client evaluation or AsEnumerable() to force in-memory processing
* Dynamic LINQ builds expression trees at runtime for scenarios like user-defined filters, but with reflection overhead and type safety loss

**Interview Notes:**
* Interviewers probe understanding of query translation limits—explain why arbitrary C# code can't always map to SQL
* Discuss performance: expression compilation via `.Compile()` has overhead, so cache compiled expressions for repeated use
* Clarify that IQueryable<T> enables composable queries that translate as a whole, versus IEnumerable<T> which executes incrementally in memory

---

## Question 11: Explain IEnumerable vs IQueryable. When to use each for database queries?

**Answer:**
* IEnumerable executes LINQ-to-Objects in-memory—filtering happens client-side after pulling data from the database
* IQueryable builds expression trees that translate to provider-specific queries (SQL for EF Core)—filtering happens database-side before data retrieval
* Switching from IQueryable to IEnumerable mid-query (via AsEnumerable()) forces remaining operations to execute in-memory, often causing full table loads
* Use IQueryable for composable database queries where filters/projections should translate to SQL; use IEnumerable for in-memory collections or after materialization
* EF Core's AsNoTracking() returns IQueryable but disables change tracking, improving read performance without switching to in-memory execution
* Deferred execution applies to both, but IQueryable can optimize across multiple chained operations before database execution

**Interview Notes:**
* Expect questions about performance pitfalls: calling ToList() too early or using AsEnumerable() prematurely causes N+1 or full table scans
* Discuss query translation failures—complex C# logic in Where() clauses may not translate, forcing client evaluation or errors in EF Core 3+
* Clarify that IQueryable extends IEnumerable, so a query can degrade to in-memory without obvious syntax changes

---

## Question 12: Explain extension methods. How to design fluent APIs?

**Answer:**
* Extension methods are static methods in static classes with `this` modifier on the first parameter, appearing as instance methods at call sites
* They enable adding methods to types you don't own without inheritance or modifying source code—LINQ's entire API is built on extensions
* Name collisions resolve in favor of instance methods, then extensions in the current namespace, then imported namespaces—this makes extensions non-breaking additions
* Fluent APIs chain extension methods returning the same or related types (`builder.AddService().Configure()`)—method return types determine composability
* Avoid over-extension: adding extensions to broad types like `string` or `object` pollutes IntelliSense and creates maintenance burden
* Generic constraints on extension methods enable type-specific behavior while maintaining fluent call chains

**Interview Notes:**
* Interviewers often ask about extension method resolution and namespace impacts—explain why organizing extensions by functionality namespace is critical
* Discuss limitations: can't access private members, can't override virtual methods, and can't be used for property-like accessors
* Fluent API design questions probe understanding of builder patterns and method chaining for configuration scenarios

---

## Question 13: Explain covariance and contravariance in C# generics.

**Answer:**
* Covariance (`out T`) allows assigning a more derived generic type to a less derived one—`IEnumerable<string>` to `IEnumerable<object>` is valid
* Contravariance (`in T`) allows the reverse: assigning less derived to more derived—`Action<object>` to `Action<string>` works because Action accepts parameters
* Covariance applies to output positions (return types), contravariance to input positions (parameters)—enforcing type safety at compile time
* Interfaces and delegates support variance; classes and structs do not—variance requires reference type constraints (`where T : class`)
* Arrays have broken covariance for historical reasons—`string[]` converts to `object[]`, but inserting non-strings throws runtime exceptions

**Interview Notes:**
* Expect questions about why variance exists: enabling polymorphic collection APIs without sacrificing type safety
* Discuss practical examples: `IEnumerable<T>` is covariant (read-only),  `IComparer<T>` is contravariant (write-only from consumer perspective)
* Clarify that variance only applies to interfaces and delegates, and only with reference types—value types don't support variance

---

## Question 14: Explain reflection and attributes. Use cases and performance concerns?

**Answer:**
* Reflection inspects and invokes types/members at runtime via `Type`,  `MethodInfo`, etc.—enabling dynamic behavior like serialization, dependency injection, and plugins
* Attributes are metadata attached to types/members, queryable via reflection—used for declarative configuration (validation, routing, ORM mappings)
* Reflection has significant overhead: type lookups, method invocations via `MethodInfo.Invoke()` box parameters and use dynamic dispatch
* Cache reflected metadata aggressively—repeated `typeof()` or `GetMethod()` calls in hot paths kill performance
* Expression trees compiled via `Expression.Compile()` eliminate reflection overhead for repeated invocations at the cost of initial compilation time
* Source generators in modern . NET replace runtime reflection with compile-time code generation, achieving zero reflection overhead

**Interview Notes:**
* Interviewers probe understanding of reflection alternatives: dynamic, expression trees, or source generators for performance-critical scenarios
* Discuss security: reflection can access private members, requiring trust and potentially breaking encapsulation
* Expect questions about serializers (JSON. NET uses reflection, System. Text. Json uses source generators) and their performance differences

---

## Question 15: Explain dynamic keyword vs Reflection. When to use each?

**Answer:**
* `dynamic` defers type checking to runtime via the DLR (Dynamic Language Runtime)—method/property resolution happens at invocation, not compile time
* Reflection explicitly queries and invokes members via metadata APIs—requires manual type handling and method lookup
* `dynamic` generates DLR call sites that cache member lookups after first invocation, making repeated calls faster than raw reflection
* Use `dynamic` for COM interop, JSON deserialization with unknown schemas, or interfacing with dynamic languages (IronPython)
* Use reflection for scenarios needing metadata inspection, attribute queries, or invocation of members discovered programmatically
* Both have significant performance costs compared to static typing—profile and consider alternatives like polymorphism or source generators

**Interview Notes:**
* Expect questions about ExpandoObject and DynamicObject—these enable creating dynamic types with custom member resolution logic
* Discuss trade-offs: `dynamic` is cleaner syntax but loses IntelliSense and compile-time safety; reflection is verbose but explicit
* Clarify that `dynamic` uses reflection internally but with call site caching for better repeated invocation performance

---

## Question 16: Explain yield return and IEnumerable. How does state machine work?

**Answer:**
* `yield return` creates compiler-generated state machines implementing IEnumerable/IEnumerator, enabling lazy evaluation without manual enumerator implementation
* Execution pauses at each `yield return`, returning control to the caller—state (locals, position) persists across MoveNext() calls
* State machines transform method locals into fields of a generated class, preserving values between yields
* Deferred execution means the method body doesn't run until enumeration starts—no work happens at method call time
* `yield break` terminates enumeration early; exceptions in iterator blocks propagate during MoveNext(), not at method call
* Avoid expensive operations before the first yield—they execute immediately when getting the enumerator, not during enumeration

**Interview Notes:**
* Interviewers ask about multiple enumeration: each foreach/ToList() restarts the iterator from the beginning, re-executing logic
* Discuss performance: state machine allocation overhead is negligible, but accidental re-enumeration can cause duplicate expensive operations
* Clarify that `yield return` in async methods requires `IAsyncEnumerable<T>` (C# 8+), enabling await inside iteration

---

## Question 17: Explain tuples, deconstruction, and pattern matching in modern C#.

**Answer:**
* Value tuples (`(int, string)`) are lightweight stack-allocated structs for returning multiple values without defining custom types
* Tuple elements support named fields (`(int Count, string Name)`) improving readability over Item1/Item2 positional access
* Deconstruction unpacks tuples or custom types implementing Deconstruct methods into individual variables: `var (id, name) = GetUser();`
* Pattern matching in `switch` expressions enables concise type checks, property comparisons, and tuple patterns without cascading if-else chains
* Record types combine tuples' lightweight syntax with nominal typing, value equality, and immutability—ideal for DTOs and domain models
* Positional records enable both tuple-like deconstruction and class-like member access simultaneously

**Interview Notes:**
* Expect questions about when to use tuples vs classes—tuples for temporary internal returns, classes for public APIs requiring stability
* Discuss pattern matching evolution: switch expressions (C# 8), relational patterns (C# 9), list patterns (C# 11) progressively reduce boilerplate
* Clarify that value tuples are value types, so passing them across methods copies data—unlike reference tuples (Tuple<T>)

---

## Question 18: Explain records vs classes. When to use records?

**Answer:**
* Records provide value-based equality (comparing all properties) instead of reference equality, eliminating custom Equals/GetHashCode implementations
* `with` expressions enable non-destructive mutation, creating modified copies while preserving immutability of the original
* Records are reference types by default but support `record struct` for value type records with similar equality semantics
* Primary constructors on records generate public init-only properties automatically, reducing boilerplate for immutable data carriers
* Use records for DTOs, API models, domain events, or any data-centric types where value equality and immutability are desired
* Classes remain appropriate for entities with identity, mutable state, or behavior-rich objects in OOP designs

**Interview Notes:**
* Interviewers probe understanding of when value equality matters—domain models, cache keys, or comparison scenarios benefit from records
* Discuss performance: record equality is structural (compares all fields), potentially slower than reference equality for large objects
* Clarify that records support inheritance (other records only), unlike structs, enabling polymorphic immutable hierarchies

---

## Question 19: Explain Span<T> and Memory<T>. When to use for performance?

**Answer:**
* `Span<T>` is a stack-only (`ref struct`) type representing contiguous memory slices—enables zero-copy views over arrays, stack memory, or native memory
* `Memory<T>` is a heap-compatible alternative supporting async methods and storage in fields, with slightly more overhead than Span
* Slicing operations create new views without copying data—`span.Slice(start, length)` returns a new Span pointing to the same memory
* Use Span for performance-critical parsing, encoding, or buffer manipulation where allocation elimination is critical
* `stackalloc` with Span enables small buffer allocation without heap pressure, ideal for temporary buffers under ~1KB
* Methods accepting `ReadOnlySpan<T>` can consume strings, arrays, or memory-mapped files through a unified API without defensive copying

**Interview Notes:**
* Expect questions about Span limitations: can't be used in async methods, stored in fields, or boxed—Memory<T> addresses these at performance cost
* Discuss practical use: ASP. NET Core Kestrel uses Span heavily for HTTP parsing, achieving microsecond request processing
* Clarify that Span enables working with unmanaged memory safely without pinning or unsafe code in most scenarios

---

## Question 20: Explain nullable reference types. How to enable null safety?

**Answer:**
* Nullable reference types introduce compile-time nullability annotations, treating reference types as non-nullable by default unless marked with `?`
* Enable via `<Nullable>enable</Nullable>` in .csproj or `#nullable enable` directives—warnings (not errors) flag potential null dereferences
* The compiler performs flow analysis, tracking null states through branches to minimize false positives—null checks eliminate warnings downstream
* Dereference operations on nullable types require null-forgiving operator `!` or explicit null checks, documenting intent
* Legacy code and external libraries without annotations generate warnings at boundaries—use null-forgiving operator judiciously or suppress diagnostics
* Nullable value types (`int?`) remain distinct from nullable reference types—former is `Nullable<T>` struct, latter is annotation-only

**Interview Notes:**
* Interviewers ask about migration strategies: enable incrementally per file/project, fix warnings in new code first, suppress legacy warnings temporarily
* Discuss limitations: annotations don't prevent nulls at runtime, only guide developers—reflection, serialization, and interop can still introduce nulls
* Expect questions about `[NotNull]`,  `[MaybeNull]` attributes for annotating pre-existing APIs to improve nullability context

---

## Question 21: Explain SOLID principles with real-world examples.

**Answer:**
* **Single Responsibility**: Each class has one reason to change—`UserRepository` handles data access,  `UserValidator` handles validation, not both
* **Open/Closed**: Classes open for extension, closed for modification—strategy pattern lets you add payment methods without changing `PaymentProcessor`
* **Liskov Substitution**: Derived types must be substitutable for base types—`Square` inheriting from `Rectangle` violates this if SetWidth/SetHeight break expectations
* **Interface Segregation**: Clients shouldn't depend on unused methods—split `IRepository` into `IReadRepository` and `IWriteRepository` for read-only consumers
* **Dependency Inversion**: Depend on abstractions, not concretions—controllers depend on `IOrderService`, not `OrderService` implementation
* SOLID reduces coupling, improves testability, and makes systems easier to extend—but over-abstraction creates complexity without value

**Interview Notes:**
* Expect questions about trade-offs: SOLID adds indirection; in small projects or well-understood domains, simpler designs may be preferable
* Discuss practical violations: static methods, singletons, and concrete dependencies in constructors all violate DIP
* Clarify that SOLID guides design but doesn't prescribe architecture—microservices, clean architecture, and DDD all apply SOLID differently

---

## Question 22: Explain Dependency Injection in ASP. NET Core. Scopes and lifetimes?

**Answer:**
* **Transient**: New instance per request—use for lightweight, stateless services; overhead minimal but creates GC pressure
* **Scoped**: One instance per HTTP request—ideal for DbContext, unit of work, or services needing request-scoped state
* **Singleton**: Single instance for application lifetime—use for stateless services, caches, or configuration; must be thread-safe
* Scoped services captured in singletons cause memory leaks and stale data—the singleton outlives requests, holding disposed scoped dependencies
* Constructor injection is preferred over property/method injection—dependencies are explicit, required, and immutable after construction
* ASP. NET Core's container validates dependency graphs at startup with `ValidateScopes` in development, catching lifetime mismatches early

**Interview Notes:**
* Interviewers probe captive dependency problems: injecting scoped DbContext into singleton service causes disposed object exceptions
* Discuss `IServiceScopeFactory` for manually creating scopes in background services or singleton-hosted operations
* Expect questions about when to use third-party containers (Autofac)—ASP. NET Core's built-in container suffices for 95% of scenarios

---

## Question 23: Explain Singleton pattern. How to implement thread-safe singleton?

**Answer:**
* Singleton ensures a single instance exists globally, providing a controlled access point—common for loggers, configuration, or connection pools
* **Lazy initialization** with `Lazy<T>` is thread-safe by default and defers construction until first access
* **Static constructor** initialization is thread-safe and eager—CLR guarantees single execution before any static member access
* Double-checked locking pattern is obsolete in . NET—memory model issues and complexity outweigh benefits of lazy initialization
* Singletons create hidden dependencies and tight coupling—dependency injection with singleton lifetime is preferable for testability
* Avoid mutable state in singletons without synchronization—concurrent access requires locks,  `ConcurrentDictionary`, or immutable data structures

**Interview Notes:**
* Expect questions about singleton downsides: difficult to test, violates SRP, and creates global mutable state
* Discuss alternatives: IoC containers manage singleton lifetime without static access; static classes for stateless utilities
* Clarify that `static readonly` instance with static constructor is the canonical . NET singleton implementation

---

## Question 24: Explain Factory pattern vs Abstract Factory. When to use each?

**Answer:**
* **Factory Method** defines an interface for creating objects but lets subclasses decide which class to instantiate—`LoggerFactory.Create()` returns different loggers
* **Abstract Factory** provides an interface for creating families of related objects without specifying concrete classes—`IServiceProvider` creates related dependency groups
* Factory Method uses inheritance for object creation, Abstract Factory uses composition with factory interfaces
* Use Factory Method when subclasses need to customize object creation; use Abstract Factory when systems need to work with multiple product families
* Factories centralize object construction logic, enabling dependency injection, configuration-based selection, and substitution during testing
* Over-use creates indirection without benefit—simple constructors suffice when object creation is straightforward

**Interview Notes:**
* Interviewers ask about real examples: ASP. NET Core's `ILoggerFactory`, EF Core's `DbContextFactory`, or `HttpClientFactory` all demonstrate factory patterns
* Discuss trade-offs: factories add abstraction layers; needed when construction is complex or runtime decisions determine types
* Expect questions about factory vs builder pattern—factories create objects in one call, builders construct step-by-step with fluent APIs

---

## Question 25: Explain Repository pattern with EF Core. Pros and cons?

**Answer:**
* Repository abstracts data access logic behind interfaces, decoupling business logic from ORM specifics—`IUserRepository` hides EF Core details
* Centralizes query logic, enabling consistent filtering, sorting, and transaction management across the application
* **Cons**: Adds abstraction over EF Core's already abstract DbContext; questionable value when EF Core provides unit of work and change tracking
* Generic repositories (`IRepository<T>`) often leak persistence concerns through IQueryable returns or expose insufficient query flexibility
* Use repositories when multiple data sources exist (SQL + NoSQL), complex domain logic needs encapsulation, or testing requires mocking data access
* Modern approach: thin repositories for specific aggregates, or skip repositories entirely and inject DbContext directly for CRUD operations

**Interview Notes:**
* Interviewers probe whether repositories add value—EF Core DbSet<T> already implements repository pattern
* Discuss testing: repositories enable mocking, but in-memory providers or test containers often provide better integration test coverage
* Expect questions about specification pattern as an alternative—composable query objects without full repository abstraction

---

## Question 26: Explain Strategy pattern. How to implement in C#?

**Answer:**
* Strategy defines a family of interchangeable algorithms, encapsulating each in a class implementing a common interface
* Enables runtime algorithm selection without conditionals—`IPaymentStrategy` with implementations like `CreditCardPayment`,  `PayPalPayment`
* Composition over inheritance: context holds a strategy reference, delegating behavior instead of inheriting it
* Use when multiple algorithms exist for a task, behavior varies by runtime conditions, or you want to eliminate switch statements on types
* Dependency injection naturally implements strategy—inject `INotificationService` and swap `EmailNotification`/`SmsNotification` via configuration
* Combines well with factory pattern for strategy selection based on configuration or user input

**Interview Notes:**
* Expect questions about strategy vs state pattern—strategy changes algorithm, state changes behavior based on internal state transitions
* Discuss when to avoid: if only two strategies exist and one is clearly default, simple if-else may be clearer
* Clarify that LINQ's `OrderBy` with `IComparer<T>` is a built-in strategy pattern example

---

## Question 27: Explain Decorator pattern. How to use with ASP. NET Core?

**Answer:**
* Decorator dynamically adds behavior to objects by wrapping them in decorator classes implementing the same interface
* Enables composing features without subclassing—`CachedRepository` wraps `SqlRepository`, adding caching transparently
* Each decorator forwards calls to the wrapped instance, adding behavior before/after delegation
* ASP. NET Core middleware is decorator pattern—each middleware wraps the next, processing requests/responses in a pipeline
* Use Scrutor library for automatic decorator registration in DI: `services.Decorate<IService, CachingDecorator>()`
* Decorator provides more flexibility than inheritance—combine multiple decorators at runtime (logging + caching + retry)

**Interview Notes:**
* Interviewers ask about decorator vs proxy pattern—decorator adds behavior, proxy controls access (lazy loading, security)
* Discuss practical uses: logging decorators, caching decorators, transaction decorators around service methods
* Expect questions about order: decorator registration order matters; logging should wrap everything, caching should be inner layers

---

## Question 28: Explain Observer pattern. How does it relate to C# events?

**Answer:**
* Observer defines one-to-many dependency where state changes in subject automatically notify all observers
* C# events are language-level observer pattern—publishers raise events, subscribers register handlers with `+=`, unregister with `-=`
* Decouples publishers from subscribers—`OrderService` raises `OrderPlaced` event without knowing which listeners exist
* Use for loosely coupled communication between components, event-driven architectures, or UI updates based on model changes
* Events can cause memory leaks when short-lived subscribers register on long-lived publishers without unsubscribing
* Alternatives: `IObservable<T>`/`IObserver<T>` from Reactive Extensions for composable event streams with LINQ-like operators

**Interview Notes:**
* Expect questions about event handler leaks—ASP. NET Core request-scoped services subscribing to singleton events never get garbage collected
* Discuss `WeakEventManager` for preventing leaks or ensuring explicit unsubscription in Dispose()
* Clarify that mediator pattern (MediatR) often replaces observer in modern applications for better testability and explicit dependencies

---

## Question 29: Explain Chain of Responsibility pattern with ASP. NET Core middleware.

**Answer:**
* Chain of Responsibility passes requests along a chain of handlers until one handles it or the chain ends
* Each handler decides to process the request, pass to the next handler, or both
* ASP. NET Core middleware pipeline is textbook Chain of Responsibility—`app.Use()` creates handlers that call `next()`
* Enables dynamic pipeline composition: authentication → authorization → routing → endpoint execution
* Short-circuiting occurs when middleware doesn't call `next()`—authorization denies access, error handlers catch exceptions
* Order matters critically: authentication before authorization, exception handling wraps everything, static files before MVC

**Interview Notes:**
* Interviewers probe understanding of middleware order and branching with `Map()` or `MapWhen()`
* Discuss practical chain implementations: logging, validation, caching layers each deciding whether to proceed
* Expect questions about when to use vs alternatives—complex decision trees may be clearer with strategy or rules engine

---

## Question 30: Explain Command pattern. How does it relate to CQRS?

**Answer:**
* Command encapsulates requests as objects, containing all information needed to perform an action
* Decouples request sender from receiver—`PlaceOrderCommand` contains order data,  `OrderHandler` processes it
* Enables queuing, logging, undo/redo, and transaction management by treating operations as first-class objects
* CQRS (Command Query Responsibility Segregation) applies command pattern to separate write operations (commands) from reads (queries)
* Commands change state and return void or success indicators; queries return data without side effects
* MediatR library implements command pattern with `IRequest`/`IRequestHandler`, centralizing command dispatch and enabling cross-cutting concerns via pipeline behaviors

**Interview Notes:**
* Expect questions about CQRS benefits: independent scaling of read/write models, optimized data structures per use case, eventual consistency patterns
* Discuss when CQRS is overkill—simple CRUD apps gain little; complex domains with distinct read/write patterns benefit significantly
* Clarify that commands in CQRS don't require event sourcing, but they pair naturally for audit trails and temporal queries

---

## Question 31: Explain ASP. NET Core middleware pipeline. How to create custom middleware?

**Answer:**
* Middleware are components assembled into a pipeline to handle requests and responses—each can process, short-circuit, or pass to the next component
* Pipeline executes in registration order for requests, reverse order for responses—exception handling wraps everything, static files before routing
* Create inline with `app.Use()` or convention-based classes with `InvokeAsync(HttpContext, RequestDelegate)` method
* Short-circuiting happens when middleware doesn't call `await next(context)`—useful for caching, authorization, or early returns
* Terminal middleware like `app.Run()` never calls next, ending the pipeline—typically used for fallback handlers
* Middleware constructors support DI for singleton services;  `InvokeAsync` resolves scoped services per request

**Interview Notes:**
* Interviewers probe middleware order understanding—authentication before authorization, CORS before routing, exception handling first
* Discuss branching with `Map()` and `MapWhen()` for conditional pipeline execution based on path or request properties
* Expect questions about performance: middleware allocation overhead is minimal; avoid heavy computation in middleware constructors

---

## Question 32: Explain DI container in ASP. NET Core. AddScoped vs AddSingleton vs AddTransient?

**Answer:**
* **Singleton**: One instance for application lifetime—shared across all requests; must be thread-safe; use for caches, configuration, stateless services
* **Scoped**: One instance per request scope—ideal for DbContext, unit of work, or request-specific state; disposed after request completes
* **Transient**: New instance every time it's requested—use for lightweight stateless services; creates GC pressure if used excessively
* Captive dependencies occur when longer-lived services capture shorter-lived ones—singleton holding scoped DbContext causes disposed object exceptions
* `IServiceScopeFactory` creates manual scopes in background services or singleton contexts where scoped dependencies are needed
* ASP. NET Core validates dependency graphs with `ValidateScopes` in development, catching lifetime mismatches at startup

**Interview Notes:**
* Expect questions about common mistakes: injecting scoped services into singletons, not disposing manual scopes, or registering DbContext as singleton
* Discuss when to use third-party containers—ASP. NET Core's built-in container handles 95% of scenarios; Autofac useful for property injection or advanced scenarios
* Clarify that scoped lifetime is per HTTP request in web apps, but manual scopes in background services or console apps

---

## Question 33: Explain model binding and validation in ASP. NET Core.

**Answer:**
* Model binding converts HTTP request data (route, query, form, header) into . NET method parameters using naming conventions and attributes
* Binding sources: `[FromRoute]`,  `[FromQuery]`,  `[FromBody]`,  `[FromHeader]`,  `[FromForm]`—inferred by default based on parameter type and location
* Complex types (classes) bind from body by default; simple types bind from route/query—override with explicit attributes
* Validation uses data annotations (`[Required]`,  `[MaxLength]`) or `IValidatableObject` for complex logic—automatically executed before action invocation
* `ModelState.IsValid` checks validation results; API controllers with `[ApiController]` attribute auto-return 400 responses on validation failures
* Custom model binders implement `IModelBinder` for non-standard binding logic—parsing custom formats, decrypting values, or complex transformations

**Interview Notes:**
* Interviewers probe binding source conflicts: multiple `[FromBody]` parameters fail; use wrapper objects or read body manually via `Request.Body`
* Discuss validation approaches: FluentValidation for complex scenarios, data annotations for simple rules, custom validators for business logic
* Expect questions about binding security: over-posting attacks preventable with DTOs instead of binding directly to domain entities

---

## Question 34: Explain filters in ASP. NET Core. Types and execution order?

**Answer:**
* Filters run code before/after specific pipeline stages: authorization, resource, action, exception, and result execution
* Execution order: Authorization → Resource → Action → Result → Exception (if thrown)—each has before/after hooks except authorization and exception
* **Authorization filters** run first, short-circuit unauthorized requests before resource allocation
* **Action filters** surround action method execution—useful for logging, validation, or modifying arguments/results
* **Exception filters** handle exceptions from actions/filters—alternative to middleware exception handling with access to MVC context
* Apply via attributes (`[TypeFilter]`), globally in `AddControllers()`, or as middleware for broader request coverage

**Interview Notes:**
* Expect questions about filter vs middleware choice—filters have MVC context (action descriptors, model state), middleware runs for all requests
* Discuss async filters (`IAsyncActionFilter`) for async operations in filter logic without blocking
* Clarify that multiple filters of the same type execute in order: global → controller → action, but can override with `Order` property

---

## Question 35: Explain JWT authentication in ASP. NET Core. How to implement?

**Answer:**
* JWT (JSON Web Token) contains claims encoded in three parts: header (algorithm), payload (claims), signature (verification)—stateless authentication without server-side sessions
* Configure with `AddAuthentication().AddJwtBearer()`, specifying issuer, audience, and signing key for token validation
* Token validation checks signature, expiration, issuer, and audience automatically—invalid tokens return 401 before reaching controllers
* Use `[Authorize]` attribute to protect endpoints; access user claims via `HttpContext.User` or inject `ClaimsPrincipal`
* Refresh tokens (stored server-side or in secure cookies) enable re-issuing JWTs without re-authentication—implement rotation to mitigate theft
* Store tokens in HttpOnly cookies (not localStorage) for web clients to prevent XSS attacks; use short expiration times and refresh token rotation

**Interview Notes:**
* Interviewers probe security: symmetric vs asymmetric signing (RS256 for multiple services, HS256 for single service), token storage, and refresh strategies
* Discuss common mistakes: long-lived tokens without refresh, storing sensitive data in claims (tokens are readable), or exposing signing keys
* Expect questions about token revocation—JWTs can't be revoked until expiration; maintain blacklist or use short-lived tokens with refresh mechanism

---

## Question 36: Explain advanced LINQ operators: SelectMany, GroupBy, Join.

**Answer:**
* **SelectMany** flattens nested collections—`users.SelectMany(u => u.Orders)` produces flat order list; useful for many-to-many relationships
* **GroupBy** creates groups with shared keys—returns `IEnumerable<IGrouping<TKey, TElement>>` where each grouping is a collection with a Key property
* **Join** performs inner joins between sequences based on key selectors—equivalent to SQL INNER JOIN; use `GroupJoin` for left joins
* SelectMany with index and projection enables complex transformations: `SelectMany((user, index) => user.Orders.Select(o => new { user, order, index }))`
* GroupBy deferred execution means grouping occurs on enumeration—materializing with ToList() before grouping when source is IQueryable avoids multiple DB hits
* Join operator is rarely used in LINQ-to-Entities—query syntax with multiple `from` clauses or navigation properties translates to SQL joins more naturally

**Interview Notes:**
* Expect questions about SelectMany vs Select: SelectMany for flattening collections, Select for one-to-one transformation
* Discuss performance: GroupBy in LINQ-to-Objects loads all data into memory for grouping; SQL-side grouping via IQueryable is far more efficient
* Clarify that GroupJoin returns groups even with no matches (left join behavior), unlike Join (inner join)

---

## Question 37: Explain EF Core change tracking. How to optimize for performance?

**Answer:**
* Change tracking monitors entity state (Added, Modified, Deleted, Unchanged) to generate appropriate SQL on SaveChanges()
* DbContext's ChangeTracker maintains snapshots of original property values, comparing them to current values to detect modifications
* **AsNoTracking()** disables tracking for queries, improving read performance by 20-30%—entities become disconnected, changes won't persist
* Attach entities to context with explicit state setting (`context.Entry(entity).State = EntityState.Modified`) for update scenarios without prior query
* Tracking overhead grows with entity count—bulk operations should use `ExecuteUpdate()`/`ExecuteDelete()` to bypass change tracking entirely
* DetectChanges() scans all tracked entities for modifications—called automatically by SaveChanges(), but manual calls in loops kill performance

**Interview Notes:**
* Interviewers probe when to disable tracking: read-only queries, API endpoints returning DTOs, or scenarios where updates aren't needed
* Discuss identity resolution: tracked entities return same instance for repeated queries within a context, preventing duplicates
* Expect questions about bulk updates in EF Core 7+: `ExecuteUpdate()` generates UPDATE without loading entities, bypassing change tracking for massive performance gains

---

## Question 38: Explain unit testing best practices with xUnit, Moq, FluentAssertions.

**Answer:**
* **xUnit** conventions: test classes don't need attributes,  `[Fact]` for single tests,  `[Theory]` with `[InlineData]` for parameterized tests
* **Moq** creates test doubles: `Mock<IService>` with `Setup()` for behavior specification and `Verify()` for interaction assertions
* **FluentAssertions** provides readable assertions: `result.Should().NotBeNull()` and `result.Should().BeEquivalentTo(expected)` with clear failure messages
* Follow AAA pattern: Arrange (setup), Act (execute), Assert (verify)—single logical assertion per test for clear failure diagnosis
* Mock external dependencies only (I/O, databases, external APIs)—don't mock domain logic or value objects; use real implementations
* Use constructor injection for dependencies to enable mocking—avoid service locator or static access that complicates testing

**Interview Notes:**
* Expect questions about mocking frameworks: Moq vs NSubstitute (syntax preference), when to use fakes vs mocks vs stubs
* Discuss test naming: `MethodName_Scenario_ExpectedBehavior` or natural sentences—consistency matters more than convention
* Clarify that over-mocking makes tests brittle—test behavior, not implementation details; refactoring shouldn't break tests

---

## Question 39: Explain Test-Driven Development (TDD). How to practice in real projects?

**Answer:**
* TDD cycle: Red (write failing test), Green (implement minimum code to pass), Refactor (improve design without breaking tests)
* Tests drive design by forcing consideration of API surface, dependencies, and behavior contracts before implementation
* Start with acceptance criteria as test cases, implement incrementally, ensuring each test passes before adding the next
* TDD works best for algorithms, business logic, and well-defined requirements—less effective for exploratory UI work or infrastructure
* Benefits: high test coverage by default, prevents over-engineering, provides living documentation through test names
* Requires discipline: resist implementing beyond test requirements, keep tests fast, and refactor aggressively after tests pass

**Interview Notes:**
* Interviewers ask about practical challenges: TDD in legacy codebases (requires refactoring for testability first), learning curve, time investment
* Discuss when to skip TDD: throwaway prototypes, trivial CRUD with framework guarantees, or exploratory spikes
* Expect questions about TDD vs test-after: TDD influences design toward loosely coupled, testable code; retroactive testing often reveals tight coupling

---

## Question 40: Explain ConfigureAwait(false). When and why to use it?

**Answer:**
* `ConfigureAwait(false)` prevents capturing the SynchronizationContext, allowing continuations to run on any thread pool thread
* Improves performance by avoiding context switches—UI threads or ASP. NET Framework contexts have overhead marshaling continuations back
* ASP. NET Core has no SynchronizationContext, making `ConfigureAwait(false)` unnecessary—all continuations run on thread pool threads by default
* Use in library code to avoid imposing context requirements on consumers—ensures library doesn't deadlock in synchronous-over-async scenarios
* Omit in application code for simplicity—no performance benefit in ASP. NET Core, and local variables/thread-local storage work consistently without it
* ConfigureAwait applies only to the specific await, not the entire method—must be called on every await for consistent behavior

**Interview Notes:**
* Expect questions about deadlock scenarios: synchronous waits (`.Result`,  `.Wait()`) on async code capture context, blocking the continuation
* Discuss ASP. NET Framework vs Core differences—Framework had AspNetSynchronizationContext for request context, Core eliminated it
* Clarify that ConfigureAwait is about context capture, not parallelism—doesn't make code run faster, just avoids unnecessary thread switches

---

## Question 41: Explain gRPC vs REST. When to use each for microservices?

**Answer:**
* gRPC uses HTTP/2 binary protocol with Protocol Buffers for serialization—significantly smaller payloads and faster than JSON over HTTP/1.1
* REST uses HTTP/1.1 with JSON—human-readable, browser-compatible, and universally supported without special tooling
* gRPC supports bidirectional streaming, enabling real-time communication patterns (server push, client streaming, duplex) natively
* Use gRPC for internal service-to-service communication requiring high throughput, low latency, or streaming; use REST for public APIs, browser clients, or ecosystem integration
* gRPC requires code generation from .proto files—strong typing and versioning but adds build complexity
* REST has better debugging tools, caching infrastructure (CDNs, HTTP caches), and wider ecosystem support

**Interview Notes:**
* Interviewers probe practical trade-offs: gRPC's performance benefits vs REST's simplicity and ubiquity
* Discuss load balancer support: gRPC requires L7 load balancers aware of HTTP/2 streams; REST works with simple L4 balancers
* Expect questions about browser support: gRPC-Web enables browser clients but requires proxy translation to standard gRPC

---

## Question 42: Explain message queues. RabbitMQ patterns and use cases?

**Answer:**
* Message queues decouple producers and consumers via asynchronous messaging—enables temporal decoupling, load leveling, and fault tolerance
* **Work Queue**: Multiple consumers compete for messages—load distribution and parallel processing of tasks
* **Pub/Sub**: Publishers send to exchanges, subscribers receive copies via bound queues—event broadcasting to multiple interested parties
* **Routing**: Direct/topic exchanges route messages based on routing keys—selective message delivery based on criteria
* Use queues for background jobs, event-driven architectures, microservice communication, or handling traffic spikes without losing requests
* Idempotency is critical—network failures or retries can deliver messages multiple times; consumers must handle duplicates gracefully
* Dead letter queues capture failed messages after max retry attempts—essential for monitoring and manual intervention

**Interview Notes:**
* Expect questions about delivery guarantees: at-most-once (fire and forget), at-least-once (retries, requires idempotency), exactly-once (complex, requires transactional outbox)
* Discuss queue vs topic: queues for point-to-point, topics/exchanges for pub-sub; RabbitMQ uses exchanges to implement both patterns
* Clarify performance considerations: queue depth monitoring, prefetch limits to prevent consumer overload, and message persistence trade-offs

---

## Question 43: Explain CQRS pattern. When to use Command/Query separation?

**Answer:**
* CQRS separates write operations (commands) from read operations (queries)—different models optimize for each concern
* Commands modify state and return void or success indicators; queries return data without side effects—enables independent scaling
* Write model enforces business rules and consistency; read model denormalizes for query performance—eventual consistency bridges the gap
* Use CQRS when read/write patterns differ significantly, scalability requires independent optimization, or audit/event sourcing provides value
* Implementation ranges from simple separation (same DB, different models) to complete physical separation (separate databases, event-driven sync)
* Adds complexity: data synchronization, eventual consistency handling, and increased codebase surface area

**Interview Notes:**
* Interviewers probe when CQRS is overkill—simple CRUD apps gain little; complex domains with vastly different read/write needs benefit
* Discuss event sourcing relationship: CQRS doesn't require events, but event-sourced write models naturally project to optimized read models
* Expect questions about eventual consistency: how to handle stale reads, user expectations, and UI feedback patterns

---

## Question 44: Explain Event Sourcing. How to implement event store?

**Answer:**
* Event sourcing persists state changes as immutable event sequences rather than current state snapshots—every mutation becomes an append-only event
* Rebuild entity state by replaying events from the beginning—provides complete audit trail and temporal queries ("what was the state on date X?")
* Event store is append-only log with indexes on aggregate ID—PostgreSQL, EventStoreDB, or custom implementations over any persistent storage
* Snapshots optimize replay performance by caching state at intervals—rebuild from latest snapshot plus subsequent events instead of full history
* Schema evolution challenges: events are immutable, so versioning requires upcasting old formats to new schemas during replay
* Use when audit requirements are strict, temporal analysis is valuable, or complex domains benefit from behavior modeling as events

**Interview Notes:**
* Expect questions about practical challenges: event schema versioning, replay performance for long-lived aggregates, and eventual consistency complexities
* Discuss when to avoid: simple CRUD, domains where event history has no value, or teams lacking distributed systems expertise
* Clarify that projections (read models) are built by subscribing to event stream—materialized views optimized for queries

---

## Question 45: Explain distributed caching with Redis. Use cases and patterns?

**Answer:**
* Redis provides in-memory key-value storage with millisecond latency—reduces database load and improves response times for frequently accessed data
* **Cache-Aside**: Application checks cache, fetches from DB on miss, populates cache—most common pattern, application controls caching logic
* **Write-Through**: Application writes to cache and DB simultaneously—ensures cache is always fresh but increases write latency
* **Write-Behind**: Application writes to cache immediately, async process persists to DB—improves write performance but risks data loss on failures
* Use Redis for session state, API response caching, rate limiting counters, or distributed locks across service instances
* Set appropriate TTLs (time-to-live) to balance freshness and performance; use cache invalidation on updates to maintain consistency

**Interview Notes:**
* Interviewers probe cache invalidation strategies: time-based expiration, explicit invalidation on updates, or event-driven invalidation
* Discuss cache stampede: thundering herd on cache miss; mitigate with locks, early recomputation, or probabilistic early expiration
* Expect questions about data structures: Redis supports strings, hashes, lists, sets, sorted sets—choose based on access patterns

---

## Question 46: Explain Circuit Breaker pattern. How to implement with Polly?

**Answer:**
* Circuit breaker prevents cascading failures by stopping requests to failing dependencies—fast-fail instead of waiting for timeouts
* Three states: **Closed** (normal), **Open** (failing, reject requests immediately), **Half-Open** (testing recovery with limited requests)
* Transitions: Closed → Open after N failures; Open → Half-Open after timeout; Half-Open → Closed on success or → Open on failure
* Polly implements circuit breakers with configurable thresholds: `Policy.Handle<Exception>().CircuitBreakerAsync(failureThreshold, durationOfBreak)`
* Combine with retry and timeout policies via `PolicyWrap` for comprehensive resilience—order matters: timeout innermost, retry middle, circuit breaker outermost
* Monitor circuit state changes—emit metrics when opening/closing to track dependency health and alert on degradation

**Interview Notes:**
* Expect questions about failure thresholds: too sensitive causes unnecessary circuit opens; too lenient delays failure detection
* Discuss bulkhead pattern alongside circuit breakers—isolate thread pools per dependency to prevent one failure from exhausting resources
* Clarify that circuit breakers protect callers, not callees—prevents wasting resources on failing services

---

## Question 47: Explain API Gateway pattern. Benefits and implementation?

**Answer:**
* API Gateway is a single entry point for clients, routing requests to appropriate microservices—abstracts internal architecture from consumers
* Provides cross-cutting concerns: authentication, rate limiting, logging, request/response transformation, and API composition
* **Aggregation**: Gateway calls multiple services and combines responses—reduces client round trips and network chattiness
* **Backend for Frontend (BFF)**: Separate gateways per client type (web, mobile)—optimizes responses for each platform's needs
* Use for microservices architectures with multiple internal services, complex routing, or when cross-cutting concerns need centralization
* Trade-offs: single point of failure (mitigate with redundancy), potential bottleneck (scale horizontally), and added latency

**Interview Notes:**
* Interviewers probe implementation choices: Kong, Ocelot, YARP, or cloud-managed (AWS API Gateway, Azure APIM)
* Discuss anti-patterns: business logic in gateway (belongs in services), overly complex transformation logic, or becoming a monolithic gateway
* Expect questions about service mesh vs gateway: gateway for north-south (external) traffic, service mesh for east-west (internal) traffic

---

## Question 48: Explain service discovery and load balancing in microservices.

**Answer:**
* Service discovery maintains a registry of available service instances—clients query registry to find healthy endpoints dynamically
* **Client-side discovery**: Client queries registry and selects instance—more control but couples clients to discovery logic (Consul, Eureka)
* **Server-side discovery**: Load balancer queries registry and routes—clients unaware of discovery mechanism (Kubernetes services, ECS)
* Health checks ensure registry contains only healthy instances—remove failing services automatically to prevent routing failures
* Load balancing strategies: round-robin (simple), least connections (uneven workloads), consistent hashing (sticky sessions/caching)
* DNS-based discovery is simple but has TTL caching issues; API-based discovery (Consul HTTP API) provides real-time updates

**Interview Notes:**
* Expect questions about Kubernetes service discovery: DNS names for services, kube-proxy for load balancing, and automatic endpoint updates
* Discuss health check design: liveness (restart unhealthy containers) vs readiness (remove from load balancer); distinguish shallow vs deep checks
* Clarify that service mesh (Istio, Linkerd) provides advanced discovery with circuit breaking, retries, and observability built-in

---

## Question 49: Explain distributed transactions. Saga pattern implementation?

**Answer:**
* Distributed transactions across microservices are problematic—two-phase commit (2PC) doesn't scale and creates availability issues
* Saga pattern coordinates transactions as sequences of local transactions with compensating actions for rollback
* **Choreography**: Services publish events, others react—decentralized, no orchestrator, but difficult to reason about flow
* **Orchestration**: Central coordinator directs saga steps—explicit flow, easier debugging, but coordinator is potential bottleneck
* Each saga step must be idempotent—retries from failures can cause duplicate operations; use deduplication or natural idempotency
* Compensating transactions undo committed work on failure—order reversal, inventory restoration, or payment refunds

**Interview Notes:**
* Interviewers probe practical implementation: MassTransit state machines for orchestration, event sourcing for choreography
* Discuss saga failures: partial completion leaves system in intermediate state; compensations may fail, requiring manual intervention
* Expect questions about isolation: sagas expose intermediate states; design for eventual consistency and handle "dirty reads"

---

## Question 50: Explain message idempotency. How to handle duplicate messages?

**Answer:**
* Idempotency ensures processing the same message multiple times produces the same result as processing once—critical for at-least-once delivery
* Network failures, retries, and redeliveries can cause duplicate messages—consumers must deduplicate or handle naturally idempotent operations
* **Natural idempotency**: SET operations, absolute updates ("set balance to 100"), or existence checks ("create if not exists")
* **Deduplication**: Store message IDs in database/cache, skip processing if ID exists—requires message ID and atomic check-and-insert
* Use distributed locks (Redis) or database constraints (unique indexes on message ID) to prevent concurrent duplicate processing
* Deduplication storage must persist longer than max message retention to catch late duplicates

**Interview Notes:**
* Expect questions about idempotency keys: clients generate unique IDs (UUIDs) per operation; servers deduplicate using these keys
* Discuss trade-offs: deduplication adds storage and lookup overhead; natural idempotency is preferred when achievable
* Clarify that exactly-once delivery is a myth in distributed systems—at-least-once with idempotent consumers is the practical approach

---
