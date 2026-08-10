# Test-Driven Development (TDD) in ASP.NET Core - Practical Guide

## Why This Matters

Test-Driven Development (TDD) helps teams build reliable ASP.NET Core APIs with fewer regressions, clearer behavior contracts, and safer refactoring.

## What You Will Learn

- How to apply the Red-Green-Refactor cycle in ASP.NET Core
- How to structure unit and integration tests effectively
- How to avoid common Test-Driven Development mistakes

## What Is Test-Driven Development (TDD)?

**Test-Driven Development (TDD)** is a software development approach where you write a failing test first, then write the minimum production code to pass it, and finally improve the code while keeping tests green.

### Red-Green-Refactor

1. **Red**: Write a failing test for a behavior.
2. **Green**: Implement minimal code to pass.
3. **Refactor**: Improve code design without breaking behavior.

---

## Test Setup in ASP.NET Core

| Layer | Recommended Tool |
|---|---|
| Test framework | xUnit |
| Assertions | FluentAssertions (optional) |
| Mocking | Moq / NSubstitute |
| Integration testing | WebApplicationFactory |

```bash
dotnet new xunit -n Api.Tests
dotnet add Api.Tests reference src/API/API.csproj
dotnet add Api.Tests package Microsoft.AspNetCore.Mvc.Testing
```

---

## Example: Test-Driven Development for a Service

### Failing Test First (Red)

```csharp
public class DiscountServiceTests
{
    [Fact]
    public void CalculateDiscount_Returns10Percent_WhenAmountIs1000OrMore()
    {
        var service = new DiscountService();

        var result = service.CalculateDiscount(1200m);

        result.Should().Be(120m);
    }
}
```

### Minimal Code (Green)

```csharp
public class DiscountService
{
    public decimal CalculateDiscount(decimal amount)
    {
        return amount >= 1000m ? amount * 0.10m : 0m;
    }
}
```

### Improve Design (Refactor)

```csharp
[Theory]
[InlineData(999, 0)]
[InlineData(1000, 100)]
[InlineData(1500, 150)]
public void CalculateDiscount_BoundaryCases(decimal amount, decimal expected)
{
    var service = new DiscountService();
    service.CalculateDiscount(amount).Should().Be(expected);
}
```

---

## Example: Integration Test for API Behavior

```csharp
public class OrdersApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public OrdersApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetOrderById_Returns404_WhenOrderDoesNotExist()
    {
        var response = await _client.GetAsync("/api/order/99999");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
```

This verifies real request pipeline behavior and is useful with Test-Driven Development when driving API contracts.

---

## Test Pyramid for ASP.NET Core

| Test Type | Scope | Speed | Main Goal |
|---|---|---|---|
| Unit tests | Single class | Fast | Verify business rules |
| Integration tests | App slice | Medium | Validate middleware/routing/data contracts |
| End-to-end tests | Full system | Slow | Validate user flows |

---

## Common Mistakes in Test-Driven Development

- Writing tests after implementation and calling it Test-Driven Development
- Testing implementation details instead of behavior
- Skipping refactor step
- Overusing mocks for simple logic
- Allowing flaky tests in CI

---

## Production Best Practices

- Run tests in CI for every pull request
- Block merge when tests fail
- Keep tests deterministic and fast
- Use realistic test data builders
- Track and fix flaky tests immediately

## Quick Recap

- Test-Driven Development = Red-Green-Refactor.
- Start from behavior, then implement minimal code.
- Combine unit and integration tests for confidence.
- Test-Driven Development improves long-term maintainability.
