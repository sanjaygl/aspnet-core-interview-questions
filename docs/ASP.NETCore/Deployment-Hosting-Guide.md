# Deployment & Hosting in ASP.NET Core – From Local to Production

## Introduction

Deployment and hosting knowledge is critical for moving ASP.NET Core applications from development to production. While your application may work perfectly on your local machine, production environments introduce challenges like reverse proxy configuration, HTTPS certificates, environment-specific settings, and performance considerations. Understanding hosting models (in-process vs out-of-process), web servers (Kestrel, IIS, Nginx), containerization (Docker), and scaling strategies is essential for reliable production deployments. This guide explains practical deployment approaches, common configurations, and real-world scenarios for hosting your ASP.NET Core applications.

---

## Hosting Models in ASP.NET Core

### In-Process Hosting

The application runs **inside the IIS worker process (w3wp.exe)**, eliminating the need for Kestrel as a separate process.

**Characteristics:**
- Application runs in the same process as IIS
- Uses IIS HTTP Server (IISHttpServer) instead of Kestrel
- Better performance (no reverse proxy overhead)
- Only available on Windows with IIS
- Default for IIS deployments

**When to Use:**
- Windows Server with IIS
- Maximum performance on Windows
- Simplified process management
- Corporate environments standardized on IIS

**Configuration:**
```xml
<!-- web.config (generated during publish) -->
<aspNetCore processPath="dotnet" 
            arguments=".\MyApp.dll" 
            stdoutLogEnabled="false" 
            hostingModel="inprocess">
</aspNetCore>
```

### Out-of-Process Hosting

The application runs as a **separate Kestrel process**, with IIS acting as a reverse proxy.

**Characteristics:**
- Application runs in separate Kestrel process
- IIS forwards requests to Kestrel
- Slight performance overhead (reverse proxy)
- Cross-platform compatibility
- Better process isolation

**When to Use:**
- Need cross-platform compatibility
- Development/testing environments
- When you want process isolation
- Running multiple apps on same IIS server

**Configuration:**
```xml
<!-- web.config -->
<aspNetCore processPath="dotnet" 
            arguments=".\MyApp.dll" 
            stdoutLogEnabled="false" 
            hostingModel="outofprocess">
</aspNetCore>
```

### Comparison Table

| Aspect | In-Process | Out-of-Process |
|--------|-----------|---------------|
| **Performance** | Faster (no proxy overhead) | Slightly slower (proxy overhead) |
| **Process** | Runs in IIS worker process | Separate Kestrel process |
| **Platform** | Windows/IIS only | Cross-platform |
| **Isolation** | Shares IIS process | Independent process |
| **Debugging** | More complex | Easier (separate process) |
| **Default** | Yes (for IIS) | No |

---

## Web Servers Used in ASP.NET Core

### 1. Kestrel

**Kestrel** is the default, cross-platform web server built into ASP.NET Core.

**Characteristics:**
- Cross-platform (Windows, Linux, macOS)
- High-performance, asynchronous I/O
- Supports HTTP/1.1, HTTP/2, and HTTP/3
- Lightweight and fast
- Can run standalone or behind reverse proxy

**When to Use Kestrel Directly:**
```csharp
// Program.cs - Kestrel standalone (development/testing)
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000); // HTTP
    options.ListenLocalhost(5001, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS
    });
});

var app = builder.Build();
app.MapGet("/", () => "Hello from Kestrel!");
app.Run();
```

**Production Recommendation:**
- Always use Kestrel behind a reverse proxy (IIS, Nginx, Apache)
- Reverse proxy handles SSL termination, load balancing, security

### 2. IIS (Internet Information Services)

**IIS** is Microsoft's web server for Windows Server.

**Responsibilities:**
- Reverse proxy to Kestrel (out-of-process) or host directly (in-process)
- SSL/TLS termination
- Load balancing
- URL rewriting
- Static file serving
- Request filtering and security
- Integration with Windows Authentication

**When to Use:**
- Windows Server environments
- Corporate/enterprise deployments
- Windows Authentication required
- Standardized on Microsoft stack

### 3. Nginx / Apache

**Nginx** and **Apache** are popular reverse proxy servers on Linux.

**Nginx Responsibilities:**
- Reverse proxy to Kestrel
- SSL/TLS termination
- Load balancing across multiple Kestrel instances
- Static file serving
- Caching
- Rate limiting
- Request buffering

**When to Use:**
- Linux production deployments
- High-performance requirements
- Cloud environments (Azure, AWS, GCP)
- Kubernetes/container orchestration

---

## Kestrel with Reverse Proxy

### Why Use a Reverse Proxy?

Running Kestrel directly in production is **not recommended** because:

1. **Security**: Reverse proxies provide additional security layers (request filtering, DDoS protection)
2. **SSL Termination**: Handle HTTPS certificates and encryption/decryption
3. **Load Balancing**: Distribute traffic across multiple Kestrel instances
4. **Static Files**: Efficiently serve static content
5. **Port Management**: Kestrel runs on high ports (5000+), proxy handles standard ports (80, 443)
6. **Multiple Apps**: Host multiple applications on same server
7. **Request Buffering**: Handle slow clients without blocking Kestrel

### Request Flow with Reverse Proxy

**Architecture:**
```
┌─────────────┐
│   Client    │
└──────┬──────┘
       │ HTTPS (443)
       ▼
┌─────────────────────────┐
│   Reverse Proxy         │
│   (IIS/Nginx/Apache)    │
│                         │
│  - SSL Termination      │
│  - Load Balancing       │
│  - Static Files         │
│  - Security Filtering   │
└──────┬──────────────────┘
       │ HTTP (5000)
       ▼
┌─────────────────────────┐
│   Kestrel               │
│   ASP.NET Core App      │
│                         │
│  - Business Logic       │
│  - API Endpoints        │
│  - Dynamic Content      │
└─────────────────────────┘
```

**Request Flow:**

```
1. Client → HTTPS Request (port 443)
              ↓
2. Reverse Proxy → SSL Termination (decrypt HTTPS)
              ↓
3. Reverse Proxy → Forwards HTTP Request (port 5000)
              ↓
4. Kestrel → Processes Request
              ↓
5. Kestrel → Returns Response
              ↓
6. Reverse Proxy → Encrypts Response (SSL)
              ↓
7. Client ← HTTPS Response
```

### Forwarded Headers Configuration

When behind a reverse proxy, configure forwarded headers to preserve client information:

```csharp
// Program.cs
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Configure forwarded headers for reverse proxy
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = 
        ForwardedHeaders.XForwardedFor | 
        ForwardedHeaders.XForwardedProto;
    
    // Trust proxy servers (configure for your network)
    options.KnownProxies.Clear();
    options.KnownNetworks.Clear();
});

var app = builder.Build();

// Use forwarded headers (MUST be before other middleware)
app.UseForwardedHeaders();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

**Headers Forwarded:**
- `X-Forwarded-For`: Original client IP address
- `X-Forwarded-Proto`: Original protocol (http/https)
- `X-Forwarded-Host`: Original host header

---

## Deploying to IIS (Windows)

### Step 1: Install .NET Hosting Bundle

```powershell
# Download and install .NET Hosting Bundle on Windows Server
# https://dotnet.microsoft.com/download/dotnet/8.0
# Install: dotnet-hosting-8.0.x-win.exe

# Verify installation
dotnet --info
```

### Step 2: Publish the Application

```bash
# Publish for production (Framework-Dependent Deployment)
dotnet publish -c Release -o ./publish

# Or Self-Contained Deployment (includes runtime)
dotnet publish -c Release -r win-x64 --self-contained -o ./publish
```

**Published Files Structure:**
```
publish/
├── MyApp.dll
├── MyApp.deps.json
├── MyApp.runtimeconfig.json
├── appsettings.json
├── appsettings.Production.json
├── web.config (auto-generated)
└── wwwroot/
```

### Step 3: Configure IIS

```powershell
# Enable IIS and ASP.NET Core Module
Install-WindowsFeature -name Web-Server -IncludeManagementTools
Install-WindowsFeature -name Web-Asp-Net45

# Create Application Pool (No Managed Code)
New-WebAppPool -Name "MyAppPool"
Set-ItemProperty IIS:\AppPools\MyAppPool -name "managedRuntimeVersion" -value ""

# Create Website
New-Website -Name "MyApp" -Port 80 -PhysicalPath "C:\inetpub\wwwroot\MyApp" -ApplicationPool "MyAppPool"

# Set Permissions
icacls "C:\inetpub\wwwroot\MyApp" /grant "IIS_IUSRS:(OI)(CI)F" /T
```

**web.config (auto-generated during publish):**
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet"
                  arguments=".\MyApp.dll"
                  stdoutLogEnabled="true"
                  stdoutLogFile=".\logs\stdout"
                  hostingModel="inprocess" />
    </system.webServer>
  </location>
</configuration>
```

### Common IIS Issues

| Issue | Cause | Solution |
|-------|-------|----------|
| **502.5 Error** | .NET Hosting Bundle not installed | Install .NET Hosting Bundle |
| **500.19 Error** | web.config misconfiguration | Verify XML syntax, check handlers |
| **403 Forbidden** | Incorrect permissions | Grant IIS_IUSRS permissions |
| **Application won't start** | Missing dependencies | Check Event Viewer, enable stdout logging |
| **Module not found** | ASP.NET Core Module not registered | Restart IIS after installing hosting bundle |

**Enable Logging:**
```xml
<!-- web.config -->
<aspNetCore stdoutLogEnabled="true" stdoutLogFile=".\logs\stdout">
</aspNetCore>
```

Check logs at: `C:\inetpub\wwwroot\MyApp\logs\`

---

## Deploying to Linux with Nginx

### Why Linux for Production?

**Benefits:**
- **Cost**: No Windows Server licensing fees
- **Performance**: Lower resource overhead
- **Containerization**: Docker/Kubernetes ecosystem
- **Cloud**: Native fit for AWS, Azure, GCP Linux VMs
- **Scalability**: Easier horizontal scaling
- **Open Source**: Community support and flexibility

### Step 1: Install .NET Runtime on Linux

```bash
# Ubuntu/Debian
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y aspnetcore-runtime-8.0

# Verify installation
dotnet --info
```

### Step 2: Deploy Application

```bash
# Create application directory
sudo mkdir -p /var/www/myapp
sudo chown -R $USER:$USER /var/www/myapp

# Copy published files (from local machine)
scp -r ./publish/* user@server:/var/www/myapp/

# Set permissions
sudo chmod +x /var/www/myapp/MyApp.dll
```

### Step 3: Create Systemd Service

```bash
# Create service file
sudo nano /etc/systemd/system/myapp.service
```

```ini
[Unit]
Description=My ASP.NET Core App
After=network.target

[Service]
WorkingDirectory=/var/www/myapp
ExecStart=/usr/bin/dotnet /var/www/myapp/MyApp.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=myapp
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

```bash
# Enable and start service
sudo systemctl enable myapp.service
sudo systemctl start myapp.service

# Check status
sudo systemctl status myapp.service

# View logs
sudo journalctl -u myapp.service -f
```

### Step 4: Configure Nginx as Reverse Proxy

```bash
# Install Nginx
sudo apt-get install nginx

# Create Nginx configuration
sudo nano /etc/nginx/sites-available/myapp
```

```nginx
server {
    listen 80;
    server_name example.com www.example.com;
    
    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

```bash
# Enable site
sudo ln -s /etc/nginx/sites-available/myapp /etc/nginx/sites-enabled/

# Test configuration
sudo nginx -t

# Reload Nginx
sudo systemctl reload nginx
```

### Step 5: Configure HTTPS with Let's Encrypt

```bash
# Install Certbot
sudo apt-get install certbot python3-certbot-nginx

# Obtain SSL certificate
sudo certbot --nginx -d example.com -d www.example.com

# Auto-renewal is configured automatically
sudo certbot renew --dry-run
```

**Updated Nginx config (with HTTPS):**
```nginx
server {
    listen 443 ssl http2;
    server_name example.com www.example.com;
    
    ssl_certificate /etc/letsencrypt/live/example.com/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/example.com/privkey.pem;
    
    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}

# Redirect HTTP to HTTPS
server {
    listen 80;
    server_name example.com www.example.com;
    return 301 https://$server_name$request_uri;
}
```

---

## Dockerizing ASP.NET Core Applications

### Benefits of Docker Containers

1. **Consistency**: Same environment from dev to production
2. **Isolation**: Each container is independent
3. **Portability**: Run anywhere (local, cloud, on-premises)
4. **Scalability**: Easy horizontal scaling with orchestrators
5. **Version Control**: Docker images are versioned
6. **Resource Efficiency**: Lightweight compared to VMs

### Simple Dockerfile Example

```dockerfile
# Multi-stage build for optimized image size

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["MyApp/MyApp.csproj", "MyApp/"]
RUN dotnet restore "MyApp/MyApp.csproj"

# Copy source code and build
COPY . .
WORKDIR "/src/MyApp"
RUN dotnet build "MyApp.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "MyApp.csproj" -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy published app from publish stage
COPY --from=publish /app/publish .

# Set entry point
ENTRYPOINT ["dotnet", "MyApp.dll"]
```

### Building and Running the Container

```bash
# Build Docker image
docker build -t myapp:latest .

# Run container
docker run -d -p 8080:80 --name myapp-container myapp:latest

# Check running containers
docker ps

# View logs
docker logs myapp-container

# Stop container
docker stop myapp-container
```

### Docker Compose for Multi-Container Setup

```yaml
# docker-compose.yml
version: '3.8'

services:
  web:
    build: .
    ports:
      - "8080:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Server=db;Database=MyAppDb;User=sa;Password=YourPassword123!
    depends_on:
      - db
    restart: unless-stopped

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourPassword123!
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql
    restart: unless-stopped

volumes:
  sqldata:
```

```bash
# Start all services
docker-compose up -d

# Stop all services
docker-compose down
```

### When to Use Docker

**Use Docker When:**
- Deploying to Kubernetes or container orchestrators
- Need consistent environments across dev/staging/prod
- Microservices architecture
- Cloud-native applications
- CI/CD pipelines with containerized builds

**Skip Docker When:**
- Simple single-server deployments
- Corporate environments with strict container policies
- Legacy infrastructure without container support
- Learning/prototyping phase

---

## Environment Configuration in Production

### Environment Variables

```csharp
// Program.cs - Reading environment variables
var builder = WebApplication.CreateBuilder(args);

// Environment variables override appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var apiKey = builder.Configuration["ExternalApi:ApiKey"];

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
```

**Setting Environment Variables:**

**Linux/Systemd:**
```ini
# /etc/systemd/system/myapp.service
[Service]
Environment="ASPNETCORE_ENVIRONMENT=Production"
Environment="ConnectionStrings__DefaultConnection=Server=..."
```

**Docker:**
```bash
docker run -e ASPNETCORE_ENVIRONMENT=Production \
           -e ConnectionStrings__DefaultConnection="Server=..." \
           myapp:latest
```

**Azure App Service:**
```bash
az webapp config appsettings set --name myapp --resource-group mygroup \
  --settings ASPNETCORE_ENVIRONMENT=Production \
             ConnectionStrings__DefaultConnection="Server=..."
```

### Secrets Management

**Never hardcode secrets in code or commit them to source control.**

**Development (User Secrets):**
```bash
# Initialize user secrets
dotnet user-secrets init

# Set secrets
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;..."
dotnet user-secrets set "ExternalApi:ApiKey" "secret-key-123"
```

**Production Options:**

**1. Azure Key Vault:**
```bash
# Install package
dotnet add package Azure.Extensions.AspNetCore.Configuration.Secrets

# Program.cs
var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    var keyVaultUrl = new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(keyVaultUrl, new DefaultAzureCredential());
}
```

**2. AWS Secrets Manager:**
```bash
dotnet add package AWSSDK.SecretsManager
```

**3. Environment Variables (for containers):**
```bash
# Kubernetes Secret
kubectl create secret generic myapp-secrets \
  --from-literal=ConnectionStrings__DefaultConnection="Server=..." \
  --from-literal=ApiKey="secret-key"
```

### Avoiding Hardcoded Settings

```csharp
// ❌ Bad: Hardcoded
public class EmailService
{
    private const string SmtpServer = "smtp.gmail.com"; // Hardcoded!
    private const string ApiKey = "abc123"; // Never do this!
}

// ✅ Good: Configuration-based
public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var smtpServer = _configuration["Email:SmtpServer"];
        var apiKey = _configuration["Email:ApiKey"];
        // Use configuration values
    }
}
```

**appsettings.Production.json:**
```json
{
  "Email": {
    "SmtpServer": "smtp.company.com",
    "ApiKey": "" // Loaded from environment variable or Key Vault
  }
}
```

---

## HTTPS & Security Considerations

### SSL Termination

**SSL termination** is the process of decrypting HTTPS traffic at the reverse proxy level.

**Recommended Approach: Terminate SSL at Reverse Proxy**

```
Client → HTTPS (443) → Reverse Proxy (SSL Termination) → HTTP (5000) → Kestrel
```

**Benefits:**
- Offloads SSL processing from application
- Centralized certificate management
- Better performance (reverse proxy optimized for SSL)
- Easier certificate renewal

### Configuring HTTPS in Kestrel (If Not Using Reverse Proxy)

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps("certificate.pfx", "password");
    });
});
```

**appsettings.json:**
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5000"
      },
      "Https": {
        "Url": "https://localhost:5001",
        "Certificate": {
          "Path": "certificate.pfx",
          "Password": "your-password"
        }
      }
    }
  }
}
```

### HSTS (HTTP Strict Transport Security)

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Enable HSTS in production
if (!app.Environment.IsDevelopment())
{
    app.UseHsts(); // Adds Strict-Transport-Security header
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

**HSTS Configuration:**
```csharp
builder.Services.AddHsts(options =>
{
    options.MaxAge = TimeSpan.FromDays(365);
    options.IncludeSubDomains = true;
    options.Preload = true;
});
```

**Response Header:**
```
Strict-Transport-Security: max-age=31536000; includeSubDomains; preload
```

### Security Best Practices

1. **Always use HTTPS in production**
2. **Keep secrets out of source control** (use Key Vault, Secrets Manager)
3. **Enable HSTS** to prevent protocol downgrade attacks
4. **Update certificates before expiration** (use Let's Encrypt for auto-renewal)
5. **Restrict exposed ports** (only 80/443 open)
6. **Use strong cipher suites** (configured at reverse proxy)
7. **Implement rate limiting** (prevent brute force attacks)

---

## Health Checks & Monitoring

### Why Health Checks Matter

Health checks allow orchestrators (Kubernetes, Docker Swarm) and load balancers to determine if your application is ready to receive traffic.

**Benefits:**
- Automated recovery (restart unhealthy containers)
- Zero-downtime deployments
- Load balancer routing decisions
- Monitoring and alerting

### Configuring Health Checks

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add health checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>("database")
    .AddRedis("localhost:6379", "redis")
    .AddUrlGroup(new Uri("https://api.example.com/status"), "external-api");

builder.Services.AddControllers();

var app = builder.Build();

// Map health check endpoints
app.MapHealthChecks("/health"); // Basic health check
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.MapControllers();
app.Run();
```

### Liveness vs Readiness Probes

| Probe Type | Purpose | When to Fail |
|-----------|---------|--------------|
| **Liveness** | Is the app running? | Application deadlocked or crashed |
| **Readiness** | Can the app serve traffic? | Dependencies unavailable (DB, Redis down) |

**Kubernetes Example:**
```yaml
apiVersion: v1
kind: Pod
metadata:
  name: myapp
spec:
  containers:
  - name: myapp
    image: myapp:latest
    livenessProbe:
      httpGet:
        path: /health/live
        port: 80
      initialDelaySeconds: 30
      periodSeconds: 10
    readinessProbe:
      httpGet:
        path: /health/ready
        port: 80
      initialDelaySeconds: 5
      periodSeconds: 5
```

### Custom Health Check

```csharp
public class CustomHealthCheck : IHealthCheck
{
    private readonly ILogger<CustomHealthCheck> _logger;

    public CustomHealthCheck(ILogger<CustomHealthCheck> logger)
    {
        _logger = logger;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Check critical dependency
            var isHealthy = await CheckDependencyAsync();

            if (isHealthy)
                return HealthCheckResult.Healthy("All systems operational");
            else
                return HealthCheckResult.Degraded("Dependency is slow");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Health check failed");
            return HealthCheckResult.Unhealthy("Dependency is unavailable", ex);
        }
    }

    private async Task<bool> CheckDependencyAsync()
    {
        await Task.Delay(10); // Simulate check
        return true;
    }
}

// Register custom health check
builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>("custom-check", tags: new[] { "ready" });
```

---

## Scaling ASP.NET Core Applications

### Vertical Scaling (Scale Up)

**Increase resources of a single server** (more CPU, RAM).

**Pros:**
- Simple to implement
- No code changes required
- Lower complexity

**Cons:**
- Limited by hardware constraints
- Single point of failure
- Expensive at scale

**Example:**
```
Before: 2 CPU cores, 4 GB RAM
After:  8 CPU cores, 16 GB RAM
```

### Horizontal Scaling (Scale Out)

**Add more server instances** and distribute traffic with a load balancer.

**Pros:**
- Virtually unlimited scalability
- High availability (no single point of failure)
- Cost-effective (use commodity hardware)
- Fault tolerance (one server fails, others continue)

**Cons:**
- Requires stateless design
- Session management complexity
- More complex infrastructure

**Architecture:**
```
                    ┌─────────────────┐
                    │  Load Balancer  │
                    └────────┬─────────┘
                             │
        ┌────────────────────┼────────────────────┐
        ▼                    ▼                    ▼
┌───────────────┐    ┌───────────────┐    ┌───────────────┐
│   Server 1    │    │   Server 2    │    │   Server 3    │
│  Kestrel:5000 │    │  Kestrel:5000 │    │  Kestrel:5000 │
└───────────────┘    └───────────────┘    └───────────────┘
        │                    │                    │
        └────────────────────┼────────────────────┘
                             ▼
                    ┌─────────────────┐
                    │  Shared Cache   │
                    │     (Redis)     │
                    └─────────────────┘
                             │
                             ▼
                    ┌─────────────────┐
                    │    Database     │
                    └─────────────────┘
```

### Making Applications Scale-Ready

**1. Use Distributed Cache (Not In-Memory):**
```csharp
// ❌ Bad: In-memory cache doesn't scale
builder.Services.AddMemoryCache();

// ✅ Good: Distributed cache shared across servers
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379";
});
```

**2. Use Distributed Session State:**
```csharp
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379";
});

builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});
```

**3. Avoid Server Affinity (Sticky Sessions):**
- Design stateless APIs
- Store session data in Redis/database
- Use JWT tokens instead of server-side sessions

**4. External Configuration:**
```csharp
// Use Azure App Configuration or similar
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(builder.Configuration["AppConfig:ConnectionString"]);
});
```

### Load Balancing Basics

**Load Balancer Types:**

| Type | Description | Use Case |
|------|-------------|----------|
| **Round Robin** | Distributes requests evenly across servers | Default, simple |
| **Least Connections** | Routes to server with fewest active connections | Varying request duration |
| **IP Hash** | Routes based on client IP (sticky sessions) | When session affinity needed |
| **Weighted** | Routes based on server capacity | Mixed server sizes |

**Common Load Balancers:**
- **Azure Load Balancer** / **Application Gateway**
- **AWS Elastic Load Balancing (ELB)**
- **Nginx** (software load balancer)
- **HAProxy**
- **Kubernetes Ingress**

---

## Common Deployment Mistakes

### ❌ Mistake 1: Incorrect Environment Settings

```bash
# Bad: Running production app in Development mode
ASPNETCORE_ENVIRONMENT=Development
dotnet MyApp.dll

# Problems:
# - Detailed error pages expose sensitive info
# - Developer exception page shows stack traces
# - HTTPS redirection may be disabled
```

**✅ Better Approach:**
```bash
# Always set to Production
ASPNETCORE_ENVIRONMENT=Production
dotnet MyApp.dll

# Or in systemd service
Environment="ASPNETCORE_ENVIRONMENT=Production"
```

### ❌ Mistake 2: Missing Reverse Proxy Configuration

```bash
# Bad: Exposing Kestrel directly to internet
# Kestrel listening on public IP without reverse proxy
dotnet MyApp.dll --urls "http://0.0.0.0:80"
```

**Why It's Wrong:**
- No SSL termination
- No request buffering
- No DDoS protection
- No static file optimization
- Security vulnerabilities

**✅ Better Approach:**
```bash
# Kestrel on localhost, Nginx/IIS handles public traffic
dotnet MyApp.dll --urls "http://localhost:5000"

# Nginx forwards from public port 80/443 to localhost:5000
```

### ❌ Mistake 3: Exposing Sensitive Endpoints

```csharp
// Bad: Swagger/health checks accessible in production
app.UseSwagger();
app.UseSwaggerUI();
app.MapHealthChecks("/health"); // No authorization

// Result: Attackers can see API structure and health info
```

**✅ Better Approach:**
```csharp
// Only enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Protect health checks with authorization or IP filtering
app.MapHealthChecks("/health").RequireAuthorization("AdminOnly");

// Or use Nginx to restrict access
# nginx.conf
location /health {
    allow 10.0.0.0/8;  # Internal network only
    deny all;
    proxy_pass http://localhost:5000;
}
```

### ❌ Mistake 4: Not Handling Forwarded Headers

```csharp
// Bad: Not configuring forwarded headers
// Result: HttpContext.Connection.RemoteIpAddress shows proxy IP, not client IP
// Result: Request.IsHttps returns false even though client used HTTPS
```

**✅ Better Approach:**
```csharp
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

app.UseForwardedHeaders(); // MUST be before other middleware
```

### ❌ Mistake 5: Hardcoded URLs and Ports

```csharp
// Bad: Hardcoded URLs
var apiUrl = "http://localhost:5000/api";
var databaseConnection = "Server=localhost;Database=MyDb;";
```

**✅ Better Approach:**
```csharp
// Use configuration
var apiUrl = builder.Configuration["ApiUrl"];
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
```

### ❌ Mistake 6: Not Testing in Production-Like Environment

```bash
# Bad: Only testing in Development mode
dotnet run

# Problems found only in production:
# - Missing dependencies
# - Configuration errors
# - Performance issues
```

**✅ Better Approach:**
```bash
# Test with production configuration locally
dotnet publish -c Release
cd ./publish
ASPNETCORE_ENVIRONMENT=Production dotnet MyApp.dll

# Use Docker to match production environment
docker build -t myapp:test .
docker run -e ASPNETCORE_ENVIRONMENT=Production myapp:test
```

### ❌ Mistake 7: Ignoring Logging and Monitoring

```csharp
// Bad: No logging configuration
// Result: Can't diagnose production issues
```

**✅ Better Approach:**
```csharp
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddEventLog(); // Windows
builder.Logging.AddAzureWebAppDiagnostics(); // Azure

// Use structured logging
builder.Services.AddApplicationInsightsTelemetry();
```

---

## Interview Questions

### 1. What's the difference between IIS and Kestrel?

**Kestrel** is a cross-platform, high-performance web server built into ASP.NET Core, running on Windows, Linux, and macOS. **IIS** is a Windows-only web server that can host ASP.NET Core apps in-process (directly in IIS) or out-of-process (as reverse proxy to Kestrel). Kestrel is recommended behind a reverse proxy (IIS, Nginx, Apache) for production because reverse proxies handle SSL termination, load balancing, and security filtering.

### 2. What is a reverse proxy and why use one?

A **reverse proxy** sits between clients and your ASP.NET Core application, forwarding requests to Kestrel. Benefits: (1) SSL/TLS termination (handles HTTPS encryption), (2) load balancing across multiple servers, (3) static file serving, (4) security filtering and DDoS protection, (5) request buffering for slow clients, (6) allows multiple apps on same server. Common reverse proxies: IIS, Nginx, Apache, Azure Application Gateway.

### 3. How do you deploy ASP.NET Core to Linux?

Steps: (1) Install .NET runtime on Linux server (`apt-get install aspnetcore-runtime-8.0`), (2) Publish app (`dotnet publish -c Release`), (3) Copy published files to server (`/var/www/myapp`), (4) Create systemd service to run app automatically, (5) Install and configure Nginx as reverse proxy, (6) Set up HTTPS with Let's Encrypt (`certbot --nginx`). Systemd service ensures app runs on boot and restarts on crashes. Nginx handles SSL and forwards to Kestrel on localhost:5000.

### 4. Why use Docker for ASP.NET Core deployment?

Docker provides: (1) **Consistency** - same environment from dev to production, (2) **Isolation** - each container is independent, (3) **Portability** - run anywhere (local, cloud, Kubernetes), (4) **Scalability** - easy horizontal scaling with orchestrators, (5) **Version control** - Docker images are versioned and immutable, (6) **Resource efficiency** - lightweight compared to VMs. Use Docker for cloud-native apps, microservices, Kubernetes deployments, and CI/CD pipelines.

### 5. What's the difference between in-process and out-of-process hosting in IIS?

**In-process**: App runs inside IIS worker process (w3wp.exe) using IISHttpServer instead of Kestrel. Better performance, Windows-only, default for IIS. **Out-of-process**: App runs as separate Kestrel process with IIS as reverse proxy. Slight performance overhead but cross-platform compatible and better process isolation. Choose in-process for maximum IIS performance, out-of-process for compatibility and isolation.

### 6. How do you handle environment-specific configuration in production?

Use environment variables to override `appsettings.json` values. Set `ASPNETCORE_ENVIRONMENT=Production` and configure connection strings, API keys via environment variables or secrets management (Azure Key Vault, AWS Secrets Manager). Never hardcode secrets or commit them to source control. Use `dotnet user-secrets` for development, Key Vault for production. Configuration precedence: Environment Variables > appsettings.Production.json > appsettings.json.

### 7. What is SSL termination and where should it happen?

**SSL termination** is decrypting HTTPS traffic (converting HTTPS to HTTP). Best practice: terminate SSL at the reverse proxy (Nginx, IIS, load balancer) rather than in Kestrel. Benefits: offloads SSL processing from application, centralized certificate management, better performance (reverse proxies optimized for SSL), easier certificate renewal. Traffic flow: Client → HTTPS → Reverse Proxy (SSL termination) → HTTP → Kestrel.

### 8. What's the difference between liveness and readiness probes?

**Liveness probe** checks if the app is running (answers "Is the process alive?"). Fails when app is deadlocked or crashed—triggers container restart. **Readiness probe** checks if the app can serve traffic (answers "Are dependencies available?"). Fails when database or Redis is down—removes container from load balancer but doesn't restart. Use `/health/live` for liveness, `/health/ready` for readiness in Kubernetes.

### 9. How do you scale an ASP.NET Core application horizontally?

Horizontal scaling requires: (1) **Stateless design** - no server-side session state, (2) **Distributed cache** - use Redis instead of in-memory cache, (3) **External configuration** - centralized config store, (4) **Database connection pooling** - handle concurrent connections, (5) **Load balancer** - distributes traffic across instances. Use JWT tokens (not sessions), store shared data in Redis/database, deploy to Kubernetes or cloud services with auto-scaling (Azure App Service, AWS ECS).

### 10. What are common mistakes when deploying to production?

Common mistakes: (1) Running in Development mode (exposes stack traces), (2) Not using reverse proxy (Kestrel exposed directly), (3) Exposing Swagger/health checks publicly, (4) Not configuring forwarded headers (wrong client IPs), (5) Hardcoding URLs and secrets, (6) Using in-memory cache in multi-server environments, (7) Not testing with production configuration, (8) Missing SSL/HTTPS configuration, (9) No logging or monitoring, (10) Not handling graceful shutdowns.

---

## Summary

- **ASP.NET Core uses Kestrel web server** (cross-platform, high-performance) which should run behind a reverse proxy (IIS, Nginx, Apache) in production for SSL termination, load balancing, and security
- **Two hosting models**: in-process (app runs in IIS worker process, Windows-only, faster) and out-of-process (app runs as separate Kestrel process with reverse proxy, cross-platform compatible)
- **Deploy to Windows/IIS** by installing .NET Hosting Bundle, publishing with `dotnet publish`, configuring IIS application pool with "No Managed Code", and setting proper permissions—deploy to Linux by installing runtime, creating systemd service, and configuring Nginx reverse proxy with Let's Encrypt SSL
- **Docker provides consistency** across environments, easy scalability, and portability—use multi-stage Dockerfile to minimize image size, with build/publish/runtime stages, and deploy to Kubernetes or container orchestrators
- **Manage secrets properly**: never hardcode or commit secrets, use environment variables for production, Azure Key Vault or AWS Secrets Manager for sensitive data, and `dotnet user-secrets` for development
- **Configure forwarded headers** when behind reverse proxy to preserve client IP and protocol information—without this, `RemoteIpAddress` shows proxy IP and `IsHttps` returns false
- **Horizontal scaling requires stateless design**: use distributed cache (Redis) instead of in-memory, store sessions externally, use JWT tokens instead of server sessions, and deploy behind load balancer with health checks for automated recovery and zero-downtime deployments
