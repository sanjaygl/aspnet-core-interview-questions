# Implementing Clean Architecture / Onion Architecture in ASP. NET Core

Clean Architecture and Onion Architecture are design approaches that prioritize maintainability, testability, and independence from external frameworks. They help you build applications where business logic remains isolated from infrastructure concerns like databases, APIs, or UI frameworks. This guide walks you through implementing these patterns in ASP. NET Core with practical examples and interview-focused insights.

---

## What is Clean Architecture / Onion Architecture?

Clean Architecture (by Robert C. Martin) and Onion Architecture (by Jeffrey Palermo) both advocate structuring your application in layers where dependencies point inward toward the core business logic. The innermost layer contains your domain entities and business rules, while outer layers handle infrastructure, UI, and external concerns. This design ensures your core logic remains framework-agnostic, testable, and adaptable to change.

---

## Core Principles

### Dependency Rule

All dependencies must point inward. Outer layers can depend on inner layers, but inner layers must never depend on outer layers. The Domain layer knows nothing about the database, API, or UI.

### Separation of Concerns

Each layer has a distinct responsibility. Domain handles business logic, Application orchestrates use cases, Infrastructure manages external systems, and Presentation handles user interaction.

### Framework Independence

Your business logic should not depend on ASP. NET Core, Entity Framework, or any specific technology. This allows you to swap frameworks, databases, or UI technologies with minimal impact.

---

## High-Level Architecture Diagram

### Visual Representation

![Clean Architecture Diagram](https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg)

![Clean Architecture Diagram Asp Net](https://blog.ndepend.com/wp-content/uploads/Clean-Architecture-Diagram-Asp-Net.png)

![Data Flow](https://miro.medium.com/v2/1*jH0iI7-MSQYgLUrqTUm6mg.png)

*Image Credit: [Clean Architecture by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)*

### Simplified Layer View (ASCII)

```
┌─────────────────────────────────────────────────────────────┐
│                      Presentation Layer                      │
│              (API Controllers, Razor Pages, etc.)            │
└────────────────────────┬────────────────────────────────────┘
                         │ depends on
┌────────────────────────▼────────────────────────────────────┐
│                    Infrastructure Layer                      │
│        (EF Core, External APIs, File System, etc.)          │
└────────────────────────┬────────────────────────────────────┘
                         │ depends on
┌────────────────────────▼────────────────────────────────────┐
│                     Application Layer                        │
│         (Use Cases, DTOs, Service Interfaces, etc.)         │
└────────────────────────┬────────────────────────────────────┘
                         │ depends on
┌────────────────────────▼────────────────────────────────────┐
│                       Domain Layer                           │
│              (Entities, Value Objects, Rules)                │
└─────────────────────────────────────────────────────────────┘
```

**Key Insight:** Dependencies flow inward (from outer circles to inner circles). The Domain layer is the most stable and has zero dependencies.

---

## Typical ASP. NET Core Project Structure

```
Solution/
│
├── Domain/                          # Core business logic
│   ├── Entities/
│   ├── ValueObjects/
│   ├── Enums/
│   └── Exceptions/
│
├── Application/                     # Use cases and contracts
│   ├── Interfaces/
│   ├── DTOs/
│   ├── Services/
│   └── Validators/
│
├── Infrastructure/                  # External concerns
│   ├── Persistence/
│   │   ├── Configurations/
│   │   ├── Repositories/
│   │   └── ApplicationDbContext.cs
│   ├── Identity/
│   └── ExternalServices/
│
└── WebAPI/                          # Presentation layer
    ├── Controllers/
    ├── Middleware/
    ├── Program.cs
    └── appsettings.json
```

---

## Layer-by-Layer Explanation

### Domain Layer

Contains your business entities, value objects, and core business rules. This layer has **no dependencies** on any other layer or external library. It defines the "what" of your application.

**Key Components:**
* Entities (e.g.,  `Order`,  `Product`,  `Customer`)
* Value Objects (e.g.,  `Address`,  `Money`)
* Domain exceptions
* Business rule validations

### Application Layer

Orchestrates use cases and defines contracts (interfaces) for external services. It depends only on the Domain layer. This layer defines the "how" of your application logic.

**Key Components:**
* Service interfaces (`IOrderService`,  `IProductRepository`)
* DTOs for data transfer
* Use case implementations
* Validators (e.g., FluentValidation)

### Infrastructure Layer

Implements interfaces defined in the Application layer. Handles database access (EF Core), external APIs, file systems, and third-party integrations.

**Key Components:**
* EF Core DbContext and repositories
* External API clients
* Email, SMS, and notification services
* Data migrations and seeding

### Presentation Layer (API/WebAPI)

Handles HTTP requests, routing, authentication, and response formatting. Depends on Application and Infrastructure layers. This is where ASP. NET Core controllers and middleware live.

**Key Components:**
* API Controllers
* Middleware (logging, exception handling)
* Dependency injection configuration
* Authentication/Authorization setup

---

## Dependency Flow Explanation

In Clean Architecture, dependencies flow **inward**:

1. **Domain** has no dependencies (pure C# classes)
2. **Application** depends on **Domain** (interfaces and DTOs reference entities)
3. **Infrastructure** depends on **Application** and **Domain** (implements interfaces, uses entities)
4. **Presentation** depends on **Application** and **Infrastructure** (wires up DI, calls services)

**Critical Interview Point:** The Domain and Application layers define **contracts (interfaces)**, while Infrastructure provides **implementations**. This inversion of control makes the system testable and flexible.

```
WebAPI → Infrastructure → Application → Domain
  ↓           ↓              ↓
implements  implements    defines
interfaces  interfaces   contracts
```

---

## Real-World Example: Order Management System

### Entity (Domain Layer)

```csharp
// Domain/Entities/Order.cs
namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public string CustomerName { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(string customerName, decimal totalAmount)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name is required");
            
            if (totalAmount <= 0)
                throw new ArgumentException("Total amount must be positive");

            CustomerName = customerName;
            TotalAmount = totalAmount;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
        }

        public void MarkAsCompleted()
        {
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Cannot complete a cancelled order");
            
            Status = OrderStatus.Completed;
        }
    }

    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }
}
```

### Repository Interface (Application Layer)

```csharp
// Application/Interfaces/IOrderRepository.cs
namespace Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<int> CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
    }
}
```

### Service / Use Case (Application Layer)

```csharp
// Application/Services/OrderService.cs
namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<int> CreateOrderAsync(string customerName, decimal totalAmount)
        {
            var order = new Order(customerName, totalAmount);
            return await _orderRepository.CreateAsync(order);
        }

        public async Task CompleteOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} not found");

            order.MarkAsCompleted();
            await _orderRepository.UpdateAsync(order);
        }
    }

    public interface IOrderService
    {
        Task<int> CreateOrderAsync(string customerName, decimal totalAmount);
        Task CompleteOrderAsync(int orderId);
    }
}
```

### Controller (Presentation Layer)

```csharp
// WebAPI/Controllers/OrdersController.cs
namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var orderId = await _orderService.CreateOrderAsync(
                request.CustomerName, 
                request.TotalAmount
            );
            return CreatedAtAction(nameof(GetOrder), new { id = orderId }, orderId);
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            await _orderService.CompleteOrderAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            // Implementation omitted for brevity
            return Ok();
        }
    }

    public record CreateOrderRequest(string CustomerName, decimal TotalAmount);
}
```

---

## Dependency Injection Setup Example

```csharp
// WebAPI/Program.cs
var builder = WebApplication.CreateBuilder(args);

// Application layer services
builder.Services.AddScoped<IOrderService, OrderService>();

// Infrastructure layer services
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

```csharp
// Infrastructure/Persistence/OrderRepository.cs
namespace Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<int> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        // Other methods omitted for brevity
    }
}
```

---

## Testing Benefits

### Unit Testing

You can test business logic in isolation without any infrastructure dependencies. Mock repository interfaces to test service logic independently.

```csharp
[Fact]
public async Task CreateOrder_WithValidData_ReturnsOrderId()
{
    // Arrange
    var mockRepo = new Mock<IOrderRepository>();
    mockRepo.Setup(r => r.CreateAsync(It.IsAny<Order>())).ReturnsAsync(1);
    var service = new OrderService(mockRepo.Object);

    // Act
    var orderId = await service.CreateOrderAsync("John Doe", 100m);

    // Assert
    Assert.Equal(1, orderId);
}
```

### Integration Testing

Test Infrastructure layer implementations with real databases (in-memory or test containers) to verify data access logic works correctly.

**Key Benefit:** Clean separation means you can test 90% of your logic without touching the database, making tests fast and reliable.

---

## Common Mistakes Developers Make

* **Leaking domain logic into controllers:** Controllers should only handle HTTP concerns. Business rules belong in Domain or Application layers.

* **Infrastructure dependencies in Domain:** Never reference EF Core, ASP. NET Core, or any framework in your Domain layer.

* **Skipping interfaces for repositories:** Always define interfaces in Application layer and implement them in Infrastructure. This is essential for testing.

* **DTOs in Domain layer:** Domain entities should not be used as API response models. Use DTOs in Application layer to shape data for consumers.

* **Circular dependencies:** If you find Application depending on Infrastructure, you've violated the dependency rule. Infrastructure implements Application interfaces, not the other way around.

* **Overengineering small projects:** Clean Architecture adds complexity. For simple CRUD apps or MVPs, consider simpler approaches like vertical slice architecture.

* **Not using mediator pattern:** For complex applications, consider using MediatR to decouple controllers from services and implement CQRS patterns.

---

## Clean Architecture vs Onion Architecture

| Aspect | Clean Architecture | Onion Architecture |
|--------|-------------------|-------------------|
| **Origin** | Robert C. Martin (Uncle Bob) | Jeffrey Palermo |
| **Core Concept** | Dependency Rule (inward dependencies) | Concentric layers with domain at center |
| **Layer Names** | Entities, Use Cases, Interface Adapters, Frameworks | Domain, Application Services, Infrastructure |
| **Focus** | Separation of concerns and testability | Domain-centric design |
| **Practical Difference** | More prescriptive layer definitions | More flexible, emphasizes domain importance |
| **ASP. NET Core Usage** | Often uses Application + Domain + Infrastructure + WebAPI | Similar structure, sometimes merges Application into Domain Services |
| **Interview Answer** | "They're conceptually similar; both enforce dependency inversion and protect business logic from external concerns." |

**Real Answer for Interviews:** Both architectures achieve the same goal—isolating business logic from infrastructure. Clean Architecture is more explicit about layer names and boundaries, while Onion Architecture emphasizes the domain being at the center. In practice, ASP. NET Core implementations of both look nearly identical.

---

## Common Interview Questions

### Q1: What is the Dependency Rule in Clean Architecture?

**Answer:** Dependencies must point inward. Inner layers (Domain) cannot depend on outer layers (Infrastructure, UI). Outer layers depend on inner layers through interfaces defined in inner layers.

### Q2: Why shouldn't Domain layer reference Entity Framework Core?

**Answer:** Domain represents pure business logic and should remain framework-agnostic. If you reference EF Core, you couple your business rules to a specific ORM, making it harder to test and change.

### Q3: Where do you define repository interfaces?

**Answer:** In the Application layer. Domain defines entities, Application defines contracts (interfaces), and Infrastructure provides implementations.

### Q4: How do you handle cross-cutting concerns like logging?

**Answer:** Use middleware in the Presentation layer or decorators/pipelines in Application layer. Avoid coupling Domain logic to logging frameworks.

### Q5: When should you NOT use Clean Architecture?

**Answer:** For simple CRUD apps, prototypes, or MVPs where business logic is minimal. The overhead of multiple layers isn't justified for straightforward applications.

### Q6: What's the role of DTOs in Clean Architecture?

**Answer:** DTOs (Data Transfer Objects) decouple internal domain models from external representations (API responses, database models). They belong in the Application layer.

### Q7: How do you inject dependencies from Infrastructure into Application?

**Answer:** Use dependency injection in the Presentation layer (e.g., `Program.cs` ). Register Infrastructure implementations for Application interfaces.

### Q8: Can Application layer have database queries?

**Answer:** No. Application defines repository interfaces, but Infrastructure implements them. Application orchestrates use cases by calling those interfaces.

### Q9: What's the difference between Application and Domain services?

**Answer:** Domain services contain business logic that doesn't fit in a single entity. Application services orchestrate use cases, coordinate repositories, and handle transaction boundaries.

### Q10: How do you test Clean Architecture applications?

**Answer:** Unit test Domain and Application layers by mocking interfaces. Integration test Infrastructure with real databases. E2E test through the API layer.

---

## Conclusion

Clean Architecture and Onion Architecture provide a robust foundation for building maintainable, testable, and scalable ASP. NET Core applications. By enforcing the dependency rule and separating concerns across layers, you ensure your business logic remains independent of frameworks and external systems. While these patterns add initial complexity, they pay dividends in long-term maintainability, testability, and adaptability. Focus on the core principles—dependency inversion, separation of concerns, and framework independence—and you'll build systems that stand the test of time and changing requirements.
