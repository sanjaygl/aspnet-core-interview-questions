# Microservices — 8. Deployment & DevOps — Interview Q&A

---

### Q1. What is Containerization?

**Answer:**
"Packaging an application together with everything it needs to run — runtime, libraries, config — into a single portable image, so it runs identically regardless of what host machine it's deployed to. This solves the classic 'works on my machine' problem, and is what makes it practical to run and manage dozens of independently-deployed microservices consistently."

---

### Q2. What is Docker?

**Answer:**
"The most widely used tool for building, packaging, and running containers. You define an image with a Dockerfile (base runtime, app files, dependencies, startup command), build it into an image, and run it as a container — an isolated, lightweight process with its own filesystem view, sharing the host's kernel (unlike a full VM, which is heavier)."

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "OrderService.dll"]
```

```
docker build -t order-service:latest .
docker run -p 8080:8080 order-service:latest
```

---

### Q3. What is Kubernetes and why is it used?

**Answer:**
"An orchestration platform for running many containers across a cluster of machines. It handles things you'd otherwise have to do manually across dozens of services: scheduling containers onto available nodes, scaling (more/fewer pods based on load), self-healing (restarting crashed containers automatically), service discovery and load balancing between pods, and rolling deployments (updating a service gradually with no downtime). Without something like Kubernetes, operating many independently-deployed microservices at scale becomes extremely labor-intensive."

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: order-service
spec:
  replicas: 3
  template:
    spec:
      containers:
      - name: order-service
        image: order-service:latest
        ports:
        - containerPort: 8080
```

---

### Q4. What is CI/CD in Microservices?

**Answer:**
"Continuous Integration — every code change is automatically built and tested as soon as it's pushed, catching integration problems early. Continuous Deployment/Delivery — a change that passes CI is automatically (or with one click) deployed through environments up to production. With many independently deployable microservices, each service typically has its own CI/CD pipeline, so a change to one service can flow to production without needing to coordinate a release with every other service's team."

---

### Q5. How do you manage versioning of services?

**Answer:**
"API versioning (e.g., `/api/v1/orders`, `/api/v2/orders`, or a version header) so consumers of an older contract keep working while a new version is introduced — you don't force every consumer to upgrade in lockstep with the provider. Internally, services should follow semantic versioning for their own releases, and breaking API changes should ship as a new version running alongside the old one for a deprecation period, not an in-place breaking change."

```
GET /api/v1/orders/42   (old clients keep working)
GET /api/v2/orders/42   (new clients get the new contract)
```

---

### Q6. What is Blue-Green Deployment?

**Answer:**
"Two identical production environments — 'blue' (currently live) and 'green' (the new version). You deploy the new version fully to green, verify it, then switch the router/load balancer to send all traffic to green in one atomic cutover. If something's wrong, you switch back to blue instantly. The main benefit is a near-instant rollback since the old version is still fully running and ready."

```
Before: Router -> Blue (v1, live)         Green (v2, idle, being tested)
After:  Router -> Green (v2, now live)    Blue (v1, idle, kept as instant rollback)
```

---

### Q7. What is Canary Deployment?

**Answer:**
"Instead of switching all traffic at once, you roll out the new version to a small percentage of traffic/instances first (the 'canary'), monitor it for errors/performance issues, and gradually increase the percentage if it looks healthy — or roll back quickly if it doesn't, having only affected a small slice of users. This limits the blast radius of a bad deployment compared to an all-at-once switch."

```
Step 1: 5% of traffic -> v2, 95% -> v1 (watch metrics/errors)
Step 2: if healthy, 25% -> v2, 75% -> v1
Step 3: eventually 100% -> v2
```

**Where to use:** blue-green when you want instant, all-or-nothing cutover with a simple rollback; canary when you want to limit risk by gradually validating the new version against real production traffic before fully committing.
