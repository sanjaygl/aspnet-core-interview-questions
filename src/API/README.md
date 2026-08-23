# .NET Core Web API Project Command Reference & Guide

A comprehensive production and development reference guide for managing the .NET Core Web API ecosystem, configuring database lifecycles via Entity Framework Core, and enforcing secure JWT authentication.

---

## 🛠️ 1. Essential Project Management Commands

These commands manage your basic project structure, build diagnostics, and package dependency integrations.

*   **Initialize a new Web API project:**
    ```bash
    dotnet new webapi -n API
    ```
*   **Compile and build the application:**
    ```bash
    dotnet build
    ```
*   **Execute the project in development watch mode (Auto-reload on save):**
    ```bash
    dotnet watch run
    ```
*   **Add required production Entity Framework Core packages for PostgreSQL:**
    ```bash
    dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
    dotnet add package Microsoft.EntityFrameworkCore.Design
    ```
*   **Add secure JWT authentication packages:**
    ```bash
    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
    ```

---

## 💾 2. Entity Framework Core & Database Management

These commands govern your database context lifecycles, migration generations, and structural sync operations with PostgreSQL.

*   **Install the EF Core global CLI tools (Run once on your machine):**
    ```bash
    dotnet tool install --global dotnet-ef
    ```
*   **Update the EF Core global CLI tools to the latest version:**
    ```bash
    dotnet tool update --global dotnet-ef
    ```
*   **Generate an initial schema migration script blueprint:**
    ```bash
    dotnet ef migrations add InitialSingularDatabaseSetup
    ```
*   **Remove the last generated migration blueprint file (If unapplied to database):**
    ```bash
    dotnet ef migrations remove
    ```
*   **Manually push migration scripts to execute table changes inside PostgreSQL:**
    ```bash
    dotnet ef database update
    ```
*   **Completely drop the physical database and clear out old tracking state:**
    ```bash
    dotnet ef database drop --force
    ```
*   **Generate a raw SQL script from your migrations for production deployment:**
    ```bash
    dotnet ef migrations script -o generated_script.sql
    ```

---

## 🔑 3. Local Secrets & Configuration Management

Manage sensitive cryptographic values, such as your JWT private key material, without committing hardcoded strings to code repositories.

*   **Initialize the User Secrets manager for your API project:**
    ```bash
    dotnet user-secrets init
    ```
*   **Store your development JWT SecretKey cryptographically on your local machine:**
    ```bash
    dotnet user-secrets set "JwtSettings:SecretKey" "bW9yZS10aGFuLTMyLWJ5dGVzLXNlY3JldC1rZXktZXhhbXBsZS1mb3ItcHJvZHVjdGlvbi0xMjM0NTY="
    ```
*   **List all active local user secrets configuration allocations:**
    ```bash
    dotnet user-secrets list
    ```

---

## 📝 4. Technical Architectural Interview Reference

### LINQ Selection Performance Matrix

| Method | 0 Matches | 1 Match | 2+ Matches | EF Core SQL Generation |
| :--- | :--- | :--- | :--- | :--- |
| **`FirstOrDefault()`** | Returns `null` | Returns item | Returns first item | Appends **`TOP(1)`** (Fires an early return) |
| **`SingleOrDefault()`** | Returns `null` | Returns item | Throws Exception | Appends **`TOP(2)`** (Scans entire scope) |

### API Idempotency across HTTP Verbs

*   **`GET` / `PUT` / `DELETE`:** **Idempotent**. Re-executing requests multiple times results in the identical infrastructure state effect.
*   **`POST`:** ❌ **Non-Idempotent**. Re-executing a generic collection endpoint inserts duplicate entity database tracking rows unless restricted via custom transaction headers (`Idempotency-Key`).

### The Options Pattern Lifecycles

*   **`IOptions<T>`:** Singleton lifetime. Initialized once at application boot. Ignores live file modifications.
*   **`IOptionsSnapshot<T>`:** Scoped lifetime. Recomputed once per HTTP request thread. Perfect for handling live runtime updates cleanly.
*   **`IOptionsMonitor<T>`:** Singleton lifetime. Utilizes filesystem change watchers to retrieve the absolute latest config via `.CurrentValue`. Safely injects into custom background tasks.