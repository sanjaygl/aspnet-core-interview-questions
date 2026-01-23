# ASP.NET Core Servers & Hosting – Interview Questions and Answers (Professional Guide)

This document explains ASP.NET Core servers and hosting concepts in a professional **question–answer blog style**, suitable for interviews and technical articles.

---

## Q1. What are the different web servers available in ASP.NET Core?

ASP.NET Core uses **Kestrel** as its core web server and can be hosted behind **IIS**, **Nginx**, or **Apache** acting as reverse proxies.
For development, **IIS Express** is commonly used.

Servers:
- Kestrel
- IIS
- IIS Express
- Nginx
- Apache

---

## Q2. What is Kestrel in ASP.NET Core?

Kestrel is a **cross-platform, high-performance web server** built into ASP.NET Core.
It listens for HTTP requests and passes them to the ASP.NET Core middleware pipeline.

---

## Q3. What is IIS and how does it work with ASP.NET Core?

IIS is a **Windows web server** that often works as a **reverse proxy** for ASP.NET Core.
It handles security, process management, and forwards requests to Kestrel or the in-process app.

---

## Q4. What is IIS Express?

IIS Express is a **lightweight development version of IIS**.
It is mainly used for **local development and debugging** and is not intended for production use.

---

## Q5. What is the difference between IIS and IIS Express?

| Feature | IIS | IIS Express |
|------|-----|------------|
| Usage | Production | Development |
| Installation | Windows Feature | Visual Studio |
| Performance | High | Lightweight |
| Scope | System-wide | Per user |

---

## Q6. What are the hosting models in ASP.NET Core?

ASP.NET Core supports **In-Process** and **Out-of-Process** hosting.

### In-Process Hosting
The app runs inside the IIS worker process.

```
Client → IIS (w3wp.exe) → ASP.NET Core App
```

### Out-of-Process Hosting
The app runs in a separate Kestrel process.

```
Client → IIS/Nginx → Kestrel → ASP.NET Core App
```

---

## Q7. Which hosting model is faster?

In-Process hosting is faster because it avoids reverse proxy overhead and inter-process communication.

---

## Q8. What is a reverse proxy?

A reverse proxy sits in front of the application and forwards requests.
It is used for SSL termination, load balancing, and security.

---

## Q9. What is Nginx and how is it used with ASP.NET Core?

Nginx is an open-source web server used mainly on Linux.
It acts as a reverse proxy in front of Kestrel for high-performance systems.

---

## Q10. What is Apache?

Apache is an open-source web server commonly used in Linux environments.
It can be used as an alternative reverse proxy to Nginx.

---

## Q11. What is HTTP vs HTTPS?

HTTP sends data in plain text, while HTTPS encrypts data using SSL/TLS.

| Feature | HTTP | HTTPS |
|------|------|------|
| Security | No | Yes |
| Use Case | Local testing | Production |

---

## Q12. What is WSL and how does ASP.NET Core run on Linux?

WSL allows running Linux inside Windows.
ASP.NET Core runs on Linux using Kestrel and optional Nginx inside WSL.

---

## Q13. Can ASP.NET Core run without IIS?

Yes. ASP.NET Core can run directly on Kestrel or behind Nginx or Apache.

---

## Q14. Is Kestrel production ready?

Yes. Kestrel is production-ready and widely used in cloud and microservice architectures.

---

## One-Line Interview Summary

ASP.NET Core uses Kestrel as its core server and supports multiple hosting and reverse proxy options across Windows and Linux.

---
