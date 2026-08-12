# ASP.NET Core — Hosting, Background Processing & Real-Time — Interview Q&A

---

### Q1. What is Kestrel, and why does production still usually put a reverse proxy in front of it?

**Answer:**
"Kestrel is ASP.NET Core's built-in, cross-platform web server — fast, and capable of handling internet traffic directly on its own. Production deployments still commonly put a reverse proxy (IIS, Nginx, Apache, or Azure App Service's/YARP's own front end) in front of it for things Kestrel doesn't focus on being the primary tool for: TLS termination management at scale, serving static files extremely efficiently, advanced request filtering/WAF-style protections, load balancing across multiple Kestrel instances/processes, and process management (auto-restarting a crashed process). Kestrel is fully capable as an edge server too, but the reverse-proxy pattern remains the common, battle-tested default, especially in Windows/IIS-hosted environments."

---

### Q2. What is `IHostedService`/`BackgroundService`, and what's a realistic use case?

**Answer:**
"`IHostedService` is the interface for a long-running background task that starts when the app starts and stops when the app shuts down — `BackgroundService` is an abstract base class implementing it, with a simpler `ExecuteAsync(CancellationToken)` method to override instead of separately implementing `StartAsync`/`StopAsync`. Realistic use case: a lightweight, always-running task baked directly into the same process as the API — e.g., periodically polling a queue for new work, running scheduled cleanup, or processing an in-memory `Channel<T>` that other parts of the app write work items into. It's a good fit when the background work is simple enough not to need a separate dedicated worker process/scheduler library."

```csharp
public class QueueProcessorService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    public QueueProcessorService(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var queue = scope.ServiceProvider.GetRequiredService<IQueueService>();
            await queue.ProcessNextBatchAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}

builder.Services.AddHostedService<QueueProcessorService>();
```

**Cross-question: Since a `BackgroundService` runs for the lifetime of the app, how does it safely access a Scoped service like a `DbContext`?**
"It can't hold a `DbContext` injected directly into its own constructor (that would be the exact captive-dependency bug from [[aspnetcore-02-dependency-injection-configuration-qa]]). Instead, it injects `IServiceScopeFactory`, and creates a fresh `IServiceScope` inside each iteration of its work loop, resolving the `DbContext` (and anything else Scoped) from that new scope — then disposes the scope at the end of that unit of work, exactly as shown in the code above."

---

### Q3. What is `IHostApplicationLifetime`, and how would you use it for graceful shutdown?

**Answer:**
"`IHostApplicationLifetime` exposes cancellation tokens/events tied to the application's startup and shutdown sequence (`ApplicationStarted`, `ApplicationStopping`, `ApplicationStopped`), and a `StopApplication()` method to trigger shutdown programmatically. For graceful shutdown, you register a callback on `ApplicationStopping` to do cleanup — stop accepting new work, wait for in-flight requests/background work to finish (within a bounded timeout), before the process actually exits. This matters especially in containerized/Kubernetes environments, where the platform sends a termination signal and gives the app a limited grace period to shut down cleanly before force-killing it."

```csharp
public class GracefulWorker : BackgroundService
{
    public GracefulWorker(IHostApplicationLifetime lifetime)
    {
        lifetime.ApplicationStopping.Register(() =>
        {
            // signal in-flight work to wrap up; e.g., stop pulling new items from a queue
        });
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;
}
```

---

### Q4. What are Health Checks, and what's the difference between liveness and readiness?

**Answer:**
"Health checks (`AddHealthChecks()`) expose an endpoint reporting whether the app and its critical dependencies (database, downstream services) are actually functioning. Liveness answers 'is this process alive, or has it hung/deadlocked and needs to be restarted' — a failing liveness probe in Kubernetes causes a pod restart. Readiness answers 'is this instance currently ready to receive traffic' — a failing readiness probe removes the pod from load-balancer rotation *without* restarting it, useful for a pod that's alive but still warming up (e.g., hasn't finished loading a cache) or temporarily can't reach a dependency. The full mechanics and Kubernetes-side behavior are covered in [[microservices-06-resilience-fault-tolerance-qa]]; the ASP.NET Core-specific piece is tagging checks and mapping separate endpoints for each."

```csharp
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString, tags: new[] { "ready" })
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" });

app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = c => c.Tags.Contains("live") });
app.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = c => c.Tags.Contains("ready") });
```

---

### Q5. What is SignalR, and how does it choose its transport?

**Answer:**
"SignalR is ASP.NET Core's real-time communication library, abstracting away the underlying transport mechanism so application code just deals with 'send a message to this client/group' without worrying about the wire protocol. It negotiates the best available transport automatically: WebSockets first (full duplex, most efficient, if both client and server/network support it), falling back to Server-Sent Events (one-directional server-to-client, still efficient) if WebSockets aren't available, and finally Long Polling (repeated HTTP requests simulating a persistent connection, least efficient) as the last resort for environments that block the other two (some corporate proxies/older infrastructure)."

```csharp
public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message) =>
        await Clients.All.SendAsync("ReceiveMessage", user, message);
}

builder.Services.AddSignalR();
app.MapHub<ChatHub>("/chatHub");
```

---

### Q6. How would you scale SignalR across multiple server instances?

**Answer:**
"Without extra configuration, each server instance only knows about the clients directly connected to *it* — a message sent from a client connected to Instance A has no way to reach a client connected to Instance B. A backplane (Redis is the common choice, `Microsoft.AspNetCore.SignalR.StackExchangeRedis`) solves this by having every instance publish outgoing messages to a shared Redis pub/sub channel, and every instance subscribes to that channel — so a message originating on any instance gets relayed to every instance, which then delivers it to whichever of its own locally-connected clients need it. This is essential the moment SignalR runs behind a load balancer with more than one instance."

```csharp
builder.Services.AddSignalR().AddStackExchangeRedis("redis-connection-string");
```

---

### Q7. What's the difference between a `BackgroundService` for a recurring job vs a dedicated scheduler library (Hangfire/Quartz.NET)?

**Answer:**
"A raw `BackgroundService` with a `while` loop and `Task.Delay` is simple and has zero extra dependencies, but it has no persistence (a scheduled job is forgotten if the app restarts mid-cycle), no built-in retry/failure handling, no dashboard/visibility into job history, and no support for complex scheduling (cron expressions, dependent job chains) beyond a fixed delay. Hangfire/Quartz.NET add persistence (jobs survive app restarts, stored in a database), proper retry policies, a monitoring dashboard, and cron-based scheduling. A raw `BackgroundService` stops being enough once you need any of: guaranteed job execution even across restarts, visibility into what ran and when/why it failed, or non-trivial scheduling — at that point, reach for a real scheduler library instead of hand-rolling that infrastructure."
