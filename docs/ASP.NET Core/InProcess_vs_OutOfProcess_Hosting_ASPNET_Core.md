# In-Process vs Out-of-Process Hosting in ASP.NET Core (Interview Guide)

ASP.NET Core supports two hosting models when running behind IIS:
**In-Process** and **Out-of-Process** hosting.
These models define how requests flow between IIS, Kestrel, and the application.

---

## What is Hosting Model?

A hosting model describes **where the ASP.NET Core application runs** and **how IIS forwards requests** to it.

---

## In-Process Hosting

### Definition
In-Process hosting runs the ASP.NET Core application **inside the IIS worker process (w3wp.exe)**.
There is no separate Kestrel process for request forwarding.

### How It Works (Diagram)

```
Client
  |
  v
+------+
| IIS  |
| (w3wp.exe)
+------+
  |
  v
ASP.NET Core App
(In same process)
```

### Key Characteristics
- Runs inside IIS process
- Faster request handling
- No reverse proxy overhead
- Tightly coupled with IIS

### Advantages
- Best performance
- Lower latency
- Simpler architecture

### Disadvantages
- Windows + IIS only
- Less isolation (app crash can affect IIS)

---

## Out-of-Process Hosting

### Definition
Out-of-Process hosting runs the ASP.NET Core application in a **separate process** using Kestrel.
IIS acts as a **reverse proxy**.

### How It Works (Diagram)

```
Client
  |
  v
+------+
| IIS  |
+------+
  |
  v
+----------+
| Kestrel  |
| (dotnet.exe)
+----------+
  |
  v
ASP.NET Core App
(Separate process)
```

### Key Characteristics
- Separate process for app
- IIS forwards requests to Kestrel
- More flexible hosting

### Advantages
- Better isolation
- Cross-platform support
- Works with IIS, Nginx, Apache

### Disadvantages
- Slight performance overhead
- Extra network hop

---

## In-Process vs Out-of-Process (Comparison)

| Feature | In-Process | Out-of-Process |
|------|-----------|---------------|
| Process | IIS (w3wp.exe) | Kestrel (dotnet.exe) |
| Performance | Faster | Slightly slower |
| Isolation | Low | High |
| Platform | Windows + IIS | Cross-platform |
| Reverse Proxy | Not required | Required |
| Default | Yes (.NET Core 3+) | Optional |

---

## Which One Should You Use?

### Use In-Process When:
- Hosting on IIS (Windows)
- Performance is critical
- Simple enterprise applications

### Use Out-of-Process When:
- Using Linux or containers
- Need high isolation
- Using Nginx or Apache
- Microservices architecture

---

## Common Interview Questions

### Q1. What is the default hosting model?
In-Process hosting is the default from .NET Core 3.0 onwards.

### Q2. Which hosting model is faster?
In-Process hosting is faster because there is no reverse proxy overhead.

### Q3. Does Out-of-Process use Kestrel?
Yes, Kestrel always runs in Out-of-Process hosting.

### Q4. Can In-Process run without IIS?
No. In-Process hosting requires IIS.

---

## One-Line Interview Summary

In-Process hosting runs ASP.NET Core inside IIS for better performance, while Out-of-Process hosting runs the app separately using Kestrel for better isolation and flexibility.

---

## Memory Trick

- In-Process → Same process → Faster
- Out-of-Process → Separate process → Safer

---
