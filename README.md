# CodingProblem

## About This Project

This is a C# coding practice repository containing various programming problems and ## Repository Statistics

- **📚 Total Documents**: 56+ comprehensive guides
- **❓ Interview Questions**: 687+ questions with detailed answers
- **💻 Code Examples**: 400+ real-world code snippets
- **🎯 Coverage**: Beginner to Senior/Lead level (11+ years experience)
- **🔥 Latest Additions**: Kestrel Web Server Guide, Middleware Complete Guide, CLR Complete Guide
- **📝 Last Updated**: January 2026 (.NET 10)s. The project includes implementations of common algorithms and data structure problems, along with comprehensive documentation about C# concepts.

## Project Structure

The project contains:
- **Algorithm Implementations**: Factorial, Fibonacci, Palindrome checking, duplicate finding, and number swapping
- **C# Concept Examples**: Interface vs Abstract Class implementations
- **Documentation**: Detailed explanations and interview questions

## Documentation

### Core C# Concepts

- **[⚙️ CLR (Common Language Runtime) - Complete Guide](docs/csharp/CLR-Complete-Guide.md)** - **NEW!** Comprehensive CLR reference covering architecture, execution flow, JIT compilation (Tiered JIT with PGO), Garbage Collection (generational model), CTS/CLS, BCL, Thread Pool, Assembly Loader, Exception Manager, Type Checker, real-world scenarios (Web API, high-performance data processing), performance tuning (.NET 10 strategies: JIT, R2R, Native AOT), 12 interview Q&A, and CLR vs JVM vs V8 comparison with visual diagrams, code examples, and best practices for senior-level interviews
- **[🎯 C# Delegates - Complete Guide](docs/csharp/CSharp-Delegates-Complete-Guide.md)** - **NEW!** Professional interview-oriented guide covering delegates as type-safe function pointers, basic delegate syntax, real-world calculator example (Add, Subtract, Multiply, Divide), multicast delegates with += and -= operators, invocation list concept, return value handling, practical use cases (event handling, callbacks, logging pipelines), Delegates vs Interfaces comparison, built-in delegates (Action, Func, Predicate), 7 common interview questions with answers, and real-world code examples for intermediate .NET developers
- [C# Interface vs Abstract Class Guide](docs/csharp/CSharp-Interface-vs-AbstractClass-Guide.md) - Comprehensive guide with 40+ interview questions and answers about interfaces and abstract classes in C#
- [C# Collections Complete Guide](docs/csharp/CSharp-Collections-Complete-Guide.md) - 70 interview questions covering all C# collection types with concise 3-4 line answers, real-time examples, and performance analysis
- [C# Boxing, Unboxing, Stack & Heap Guide](docs/csharp/CSharp-Boxing-Unboxing-Stack-Heap-Guide.md) - Complete guide to value types, reference types, boxing/unboxing, and memory management with performance considerations
- [C# ref and out Keywords Guide](docs/csharp/CSharp-Ref-Out-Keywords-Guide.md) - Comprehensive guide to parameter passing in C# including ref, out, in modifiers with real-world examples and interview questions
- [C# Extension Methods Guide](docs/csharp/CSharp-Extension-Methods-Guide.md) - Complete guide to extension methods including syntax, best practices, real-world examples, advanced scenarios, and 25+ interview questions

### Complete Interview Guides (10+ Years Experience)

- **[🎯 C# 100 Senior Interview Questions](docs/CSharp-100-Senior-Interview-Questions.md)** - **NEW!** Comprehensive collection of 100 high-level C# interview questions for Senior/Principal Software Engineers (11+ years experience) covering CLR & Memory Management, Advanced Language Features, Design Patterns, ASP.NET Core, EF Core, Microservices, Distributed Systems, Performance Optimization, Testing, Cloud Architecture, and Modern .NET with concise explanations, real-world examples, and production-level considerations

- **[🔥 C# / .NET Core - Complete Interview Guide](docs/CSharp-DotNetCore-Complete-Interview-Guide.md)** - Consolidated guide with 143+ questions covering Collections & LINQ, String Manipulation, Date & Time, Async/Multithreading, Thread Safety, Design Patterns, Data Structures & Algorithms, ASP.NET Core Web API, Dependency Injection, Entity Framework Core, SQL & Database, Microservices, Advanced Topics (MediatR, Kafka, Rate Limiting), and System Design with concise answers and real-world examples

### REST API Development

- **[🌐 REST API Interview Questions - Complete Guide](docs/REST-API-Interview-Questions-Guide.md)** - **NEW!** 50 comprehensive REST API interview questions covering REST fundamentals, HTTP methods, idempotency, statelessness, authentication (JWT, OAuth 2.0, API keys), authorization, security best practices, rate limiting, API versioning, CORS, pagination, caching strategies, webhooks vs polling, error handling, PUT vs POST, synchronous vs asynchronous APIs, API Gateway pattern, throttling, API documentation, monitoring & observability, monolithic vs microservices, GraphQL vs REST, idempotency in distributed systems, HTTP status codes (2xx, 3xx, 4xx, 5xx), file uploads, API testing (unit, integration, contract, performance, security), mobile vs web API design, common API design mistakes, and backwards compatibility strategies with real-world HTTP examples and best practices for production environments

### Database & SQL

- **[📊 SQL Interview Questions - Complete Guide](docs/SQL-Interview-Questions-Guide.md)** - **NEW!** 50 comprehensive SQL interview questions (25 core concepts + 25 real-world scenarios) covering DDL/DML/DCL/TCL, JOINs, indexes, normalization, stored procedures vs functions, transactions & ACID, locks & deadlocks, performance optimization, execution plans, and practical troubleshooting scenarios including deadlocks, race conditions, query timeouts, data migration issues, SQL injection prevention, and bulk insert optimization with detailed explanations
- **[🗃️ Entity Framework Core - Complete Interview Guide](docs/EF-Core-Interview-Questions-Guide.md)** - **NEW!** 25 comprehensive EF Core interview questions covering DbContext & DbSet, Code First vs Database First, Migrations, Change Tracking, Lazy/Eager/Explicit Loading, Tracking vs No-Tracking queries, LINQ, N+1 problem, AsNoTracking, Raw SQL, Transactions, Concurrency handling, EF Core vs Dapper, Performance optimization, Navigation Properties, Shadow Properties, Connection Resiliency, Soft Delete, Value Conversions, Split Query, Owned Types, Global Query Filters, and Find() vs FirstOrDefault() with real-world examples

### ASP.NET Core

- **[📝 Logging in ASP.NET Core - Best Practices, Providers & Real Examples](docs/ASP.NET%20Core/Logging-Complete-Guide.md)** - **NEW!** Professional interview-oriented guide covering ASP.NET Core logging fundamentals (built-in logging abstraction with ILogger<T>, logging providers: Console/Debug/EventSource, third-party: Serilog/NLog/Application Insights), six log levels (Trace, Debug, Information, Warning, Error, Critical) with detailed explanations and real-world examples of when to use each, basic logging in controllers and services with structured logging using message templates, logging configuration in appsettings.json (environment-specific, namespace-based filtering), custom request/response logging middleware (HTTP method, path, status code, execution time, correlation IDs), structured logging importance (queryable data vs plain text, message templates with placeholders, log scopes for context), exception logging best practices (avoiding duplicate logs, global exception handlers, including relevant context), what NOT to log (sensitive data, PII, large payloads, high-frequency logs), real-world production scenarios (API request tracing, debugging distributed systems, correlation IDs across microservices), 10 interview questions covering ILogger<T>, log levels differences, structured logging benefits, global request logging, sensitive data handling, environment configuration, and unit testing logs

- **[⚙️ Configuration & Options Pattern in ASP.NET Core - Clean and Maintainable Settings](docs/ASP.NET%20Core/Configuration-Options-Pattern-Guide.md)** - **NEW!** Professional interview-oriented guide covering ASP.NET Core configuration system (multiple providers: appsettings.json, environment variables, command-line args, configuration precedence), IConfiguration basics (accessing values, why direct access isn't ideal), Options Pattern fundamentals (strongly-typed POCOs for configuration sections), creating and registering options classes (Configure<T>, Bind, validation), using options in services (constructor injection with IOptions), three critical option interfaces (IOptions<T> - singleton/static config, IOptionsSnapshot<T> - scoped/per-request reload, IOptionsMonitor<T> - singleton with real-time reload and OnChange callbacks) with detailed comparison table and when to use each, options validation (Data Annotations, ValidateDataAnnotations, ValidateOnStart, custom IValidateOptions, PostConfigure), real-world use cases (external API settings, feature flags, multiple database connections, JWT authentication settings), common mistakes (using IConfiguration everywhere, not validating options, captive dependency with IOptionsSnapshot in singletons, storing secrets in appsettings.json), 10 interview questions covering option interfaces, validation, environment-specific config, captive dependencies, and complex type binding

- **[� Dependency Injection in ASP.NET Core - Lifetimes, Best Practices & Real Examples](docs/ASP.NET%20Core/DependencyInjection-Complete-Guide.md)** - **NEW!** Professional interview-oriented guide covering DI fundamentals (what is DI, real-world restaurant analogy), built-in DI container in ASP.NET Core (why it's built-in, registration & resolution), three critical service lifetimes (Transient - new every time, Scoped - once per request, Singleton - once per app) with when to use each, common lifetime mistakes (captive dependency, expensive transients, singleton with mutable state), service registration examples (basic, multiple implementations, concrete classes, factory-based), constructor injection (why preferred, controller examples with IRepository and ILogger), DI resolution flow (10-step process from request to disposal with dependency graph), DI in middleware (singleton in constructor, scoped in InvokeAsync), real-world use cases (logging, repository pattern, external API clients, configuration with IOptions), common anti-patterns (Service Locator, captive dependency, constructor over-injection) with solutions, DI vs manual object creation comparison table, 10 common interview questions with detailed answers covering service lifetimes, captive dependencies, middleware DI rules, multiple implementations, and unit testing with mocks

- **[�🔄 Request Lifecycle in ASP.NET Core - From Client to Response](docs/ASP.NET%20Core/Request-Lifecycle-Guide.md)** - **NEW!** Professional interview-oriented guide covering complete request lifecycle flow (Client → Kestrel → Middleware Pipeline → Routing → Authentication/Authorization → Controller/Minimal API → Action Result → Response), step-by-step execution with detailed ASCII diagram, middleware's role in the lifecycle (interception, forwarding with next(), short-circuiting), filters vs middleware comparison (scope, execution level, types: authorization/action/exception/result filters), model binding & validation (when it happens, automatic validation, ModelState), response creation (IActionResult/IResult execution, status codes, serialization), complete Program.cs example with proper middleware ordering, real-world debugging scenarios (middleware order issues, CORS errors, static files, exception handling, model binding problems), 10 common interview questions with detailed answers covering lifecycle execution, middleware placement, authentication timing, filter differences, and short-circuiting, and key takeaways for understanding ASP.NET Core request processing

- **[🌐 Kestrel Web Server in ASP.NET Core - Architecture, Hosting & Real Use Cases](docs/ASP.NET%20Core/Kestrel-Web-Server-Guide.md)** - **NEW!** Comprehensive guide covering Kestrel fundamentals (cross-platform, high-performance web server), why Kestrel was introduced (IIS limitations, cross-platform benefits), how Kestrel works with ASCII request flow diagram (Client → Reverse Proxy → Kestrel → Middleware → Controllers), three hosting models (Kestrel with IIS, Kestrel with Nginx, Kestrel as edge server), In-Process vs Out-of-Process hosting comparison (performance, stability, use cases), Kestrel configuration (ports, HTTP/HTTPS, limits, timeouts) with code examples, SSL termination strategies (reverse proxy vs Kestrel), performance characteristics (async I/O, non-blocking, HTTP/2 support), common production scenarios (Windows/IIS, Linux/Nginx, Kubernetes microservices), Kestrel vs IIS comparison table (platform, performance, security), and 12 interview questions covering production deployment, reverse proxy usage, and real-world hosting decisions

- **[⚙️ Middleware in ASP.NET Core - Complete Guide with Custom Middleware](docs/ASP.NET%20Core/Middleware-Complete-Guide.md)** - **NEW!** Professional interview-oriented guide covering middleware fundamentals (request-response pipeline), how middleware pipeline works with ASCII flow diagram, built-in middleware (UseRouting, UseAuthentication, UseAuthorization, UseEndpoints), critical middleware ordering rules (wrong vs correct order examples), step-by-step custom middleware creation (RequestDelegate, InvokeAsync implementation), real-world Request Logging Middleware example with HTTP method/path/status code/duration tracking, middleware registration in Program.cs with extension methods, short-circuiting the pipeline (API Key Authentication example), real-world use cases (logging, exception handling, authentication, request/response modification, rate limiting), Middleware vs Filters comparison table (scope, execution level, use cases), and 10 common interview questions with detailed answers for practical ASP.NET Core development

### Frontend Development

- **[Angular Interview Questions 2026 (v16–21+)](docs/Angular-Interview-Questions-Guide.md)** - 143 comprehensive Angular interview questions and answers (Beginner → Senior → Lead) with complete indexing, covering Signals, Standalone Components, SSR, Control Flow, performance optimization, security, and practical coding challenges with code examples

### AI & Modern Development Tools

- [GitHub Copilot Interview Questions Guide](docs/Copilot-Interview-Questions-Guide.md) - Interview questions and insights about GitHub Copilot, AI-assisted development, and best practices for using AI coding assistants

### SOLID Design Principles (Object-Oriented Design Foundation)

#### **SOLID Overview**
- [SOLID Design Principles Introduction](docs/solid-principles/SOLID-Design-Principles-Introduction.md) - Complete guide to SOLID principles introduced by Robert C. Martin, including history, cross-principle comparisons, and real-world ASP.NET Core applications

#### **The Five SOLID Principles**
- [Single Responsibility Principle (SRP)](docs/solid-principles/single-responsibility-principle.md) - One class, one responsibility - ensure each class has only one reason to change
- [Open/Closed Principle (OCP)](docs/solid-principles/open-closed-principle.md) - Open for extension, closed for modification - add features without changing existing code
- [Liskov Substitution Principle (LSP)](docs/solid-principles/liskov-substitution-principle.md) - Subtypes must be substitutable for base types - maintain contracts in inheritance
- [Interface Segregation Principle (ISP)](docs/solid-principles/interface-segregation-principle.md) - Many small interfaces over one fat interface - clients shouldn't depend on unused methods
- [Dependency Inversion Principle (DIP)](docs/solid-principles/dependency-inversion-principle.md) - Depend on abstractions, not concretions - high-level and low-level modules depend on interfaces

**What You'll Learn:**
- Historical context and evolution of each principle
- Detailed before/after code examples with violations and corrections
- Real-world enterprise scenarios in ASP.NET Core applications
- Interview questions with comprehensive answers
- Cross-principle relationships and how they work together
- Common anti-patterns and how to avoid them
- Performance and maintainability impact analysis

### Design Patterns (Academic & Practical)

#### **Pattern Overview**
- [Design Patterns Index](docs/design-patterns/index.md) - Academic foundation, theoretical analysis, and comprehensive pattern catalog

#### **Creational Patterns** (Object Creation)
- [Singleton](docs/design-patterns/creational/singleton.md) - Ensure single instance with global access
- [Factory Method](docs/design-patterns/creational/factory-method.md) - Create objects without specifying exact classes
- [Abstract Factory](docs/design-patterns/creational/abstract-factory.md) - Create families of related objects
- [Builder](docs/design-patterns/creational/builder.md) - Construct complex objects step by step
- [Prototype](docs/design-patterns/creational/prototype.md) - Create objects by cloning existing instances

#### **Structural Patterns** (Object Composition)
- [Adapter](docs/design-patterns/structural/adapter.md) - Make incompatible interfaces work together
- [Bridge](docs/design-patterns/structural/bridge.md) - Separate abstraction from implementation
- [Composite](docs/design-patterns/structural/composite.md) - Compose objects into tree structures
- [Decorator](docs/design-patterns/structural/decorator.md) - Add behavior to objects dynamically
- [Facade](docs/design-patterns/structural/facade.md) - Provide simplified interface to complex subsystem
- [Proxy](docs/design-patterns/structural/proxy.md) - Provide placeholder or surrogate for another object

#### **Behavioral Patterns** (Object Interaction)
- [Chain of Responsibility](docs/design-patterns/behavioral/chain-of-responsibility.md) - Pass requests along chain of handlers
- [Command](docs/design-patterns/behavioral/command.md) - Encapsulate requests as objects
- [Mediator](docs/design-patterns/behavioral/mediator.md) - Define how objects interact
- [Observer](docs/design-patterns/behavioral/observer.md) - Notify multiple objects about state changes
- [Strategy](docs/design-patterns/behavioral/strategy.md) - Encapsulate algorithms and make them interchangeable
- [Template Method](docs/design-patterns/behavioral/template-method.md) - Define algorithm skeleton in base class

### Clean Architecture

- **[🏛️ Implementing Clean Architecture / Onion Architecture in ASP.NET Core](docs/clean-architecture/Implementing-Clean-Architecture-ASPNET-Core.md)** - **NEW!** Complete professional guide covering Clean Architecture and Onion Architecture implementation in ASP.NET Core with core principles (Dependency Rule, Separation of Concerns, Framework Independence), ASCII architecture diagrams, typical project structure, layer-by-layer explanation (Domain, Application, Infrastructure, API layers), dependency flow, real-world Order Management example with Entity/Repository/Service/Controller implementations, Dependency Injection setup, testing benefits (unit vs integration), common mistakes developers make, Clean vs Onion Architecture comparison table, and 10 interview Q&As with concise answers for mid-to-senior level .NET developers

## Getting Started

1. Clone the repository
2. Open the solution file in Visual Studio: `src/CodingProblem.slnx`
3. Build and run the project to see the implementations in action
4. Explore the documentation for detailed explanations of C# concepts

## Topics Covered

- **Algorithms**: Factorial calculation, Fibonacci sequence, palindrome detection
- **Data Structures**: Array manipulation, duplicate detection, LRU Cache, Tree traversals
- **C# Core Concepts**: Interface vs Abstract Class, Collections, Boxing/Unboxing, Memory Management, Extension Methods, ref/out/in keywords, Delegates (type-safe function pointers, multicast delegates, events)
- **CLR & Runtime**: CLR architecture, JIT compilation (Tiered JIT, PGO), Garbage Collection (Gen 0/1/2, LOH), CTS/CLS, BCL, Thread Pool, Assembly Loader, Exception Manager, Type Checker, IL/MSIL, Managed vs Unmanaged code, .NET 10 compilation strategies (JIT, R2R, Native AOT)
- **Advanced C#**: Extension Methods, ref/out/in keywords, LINQ operations, Delegates, Action/Func/Predicate
- **REST API Development**: HTTP methods, idempotency, authentication (JWT, OAuth 2.0), authorization, security, rate limiting, versioning, CORS, pagination, caching, webhooks, error handling, API Gateway, GraphQL vs REST, file uploads, API testing, mobile optimization, backwards compatibility
- **ASP.NET Core**: Logging (ILogger<T>, six log levels: Trace/Debug/Information/Warning/Error/Critical, structured logging with message templates, request/response middleware, correlation IDs, exception handling, production scenarios), Configuration & Options Pattern (IConfiguration, IOptions/IOptionsSnapshot/IOptionsMonitor, strongly-typed settings, validation, environment-specific config, secrets management), Dependency Injection (three lifetimes: Transient/Scoped/Singleton, constructor injection, DI container, service registration, captive dependencies, anti-patterns, DI in middleware), Request Lifecycle (Client → Kestrel → Middleware → Routing → Auth → Controller → Response flow, middleware interception, filters, model binding, response creation, debugging scenarios), Kestrel web server (cross-platform hosting, reverse proxy setup, In-Process vs Out-of-Process, SSL termination, production scenarios), Middleware (request-response pipeline, built-in middleware, custom middleware creation, middleware ordering, short-circuiting, Middleware vs Filters), dependency injection, Web API development, global exception handling
- **Async & Multithreading**: Task vs Thread, async/await patterns, thread safety
- **Memory Management**: Value types, reference types, boxing/unboxing, stack/heap allocation, GC performance tuning
- **Parameter Passing**: ref, out, in modifiers and their use cases
- **Object-Oriented Design**: SOLID principles, design patterns (23 GoF patterns), clean architecture
- **ASP.NET Core**: Web API development, middleware, dependency injection, global exception handling
- **Entity Framework Core**: DbContext & DbSet, Code First vs Database First, Migrations, Change Tracking, Lazy/Eager/Explicit Loading, Tracking vs No-Tracking queries, LINQ optimization, N+1 problem, AsNoTracking, Raw SQL, Transactions, Concurrency handling, EF Core vs Dapper, Performance optimization, Navigation Properties, Include/ThenInclude, Shadow Properties, Connection Resiliency, Soft Delete, Value Conversions, Split Query, Owned Types, Global Query Filters, Find() vs FirstOrDefault()
- **Database & SQL**: 50 questions covering DDL/DML/DCL/TCL commands, JOINs, indexes (clustered vs non-clustered), stored procedures vs functions, transactions & ACID, locks & deadlocks, query optimization, execution plans, pagination, NULL handling, plus 25 real-world scenarios (deadlocks, race conditions, query timeouts, data migration, SQL injection, bulk inserts, trigger optimization, CTE performance, recursive queries, and production troubleshooting)
- **Microservices**: Retry patterns with Polly, Circuit Breaker, Saga Pattern, event-driven architecture
- **Advanced Topics**: MediatR, CQRS, Apache Kafka, rate limiting, idempotent APIs
- **System Design**: URL Shortener, Order Management, Payment Processing systems
- **Frontend**: Angular (v16-21+) with Signals, Standalone Components, SSR
- **Modern Development**: GitHub Copilot, AI-assisted development practices

## Repository Statistics

- **📚 Total Documents**: 60+ comprehensive guides
- **❓ Interview Questions**: 727+ questions with detailed answers
- **💻 Code Examples**: 470+ real-world code snippets
- **🎯 Coverage**: Beginner to Senior/Lead level (11+ years experience)
- **🔥 Latest Additions**: Logging Guide, Configuration & Options Pattern Guide, Dependency Injection Guide
- **📝 Last Updated**: January 2026 (.NET 10)