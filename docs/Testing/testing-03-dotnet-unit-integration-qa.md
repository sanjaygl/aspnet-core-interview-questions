# Testing — C# / .NET Unit & Integration Testing — Interview Q&A

---

### Q1. xUnit vs NUnit vs MSTest — what are the practical differences?

**Answer:**
"All three are capable, mature test frameworks with largely overlapping features at this point. xUnit is the modern default for new .NET projects (and what the .NET team itself uses) — it creates a new test class instance per test method by default (better test isolation out of the box), and has a cleaner attribute model (`[Fact]`/`[Theory]` instead of `[Test]`/`[TestMethod]`). NUnit has a longer history and a richer built-in assertion/constraint model. MSTest is Microsoft's original framework, tightly integrated with Visual Studio, and has closed much of its historical feature gap with the other two in recent versions. In practice, the choice mostly comes down to team convention and ecosystem/tooling familiarity — xUnit is the safest default if starting fresh."

---

### Q2. How do you mock a dependency in a C# unit test, and what's the difference between `.Setup()` and `.Verify()`?

**Answer:**
"With Moq (or NSubstitute), `.Setup()` configures what a mocked method should *return* when called with certain arguments — controlling the input/state fed into the code under test. `.Verify()` asserts, after the test has run, that a particular method *was* called (and optionally, how many times, and with what arguments) — checking an interaction actually happened, not controlling behavior."

```csharp
var mockGateway = new Mock<IPaymentGateway>();
mockGateway.Setup(g => g.Charge(It.IsAny<decimal>())).Returns(true); // controls what Charge() returns

var service = new OrderService(mockGateway.Object);
service.CompleteOrder(order);

mockGateway.Verify(g => g.Charge(150m), Times.Once); // verifies Charge was actually called with 150m
```

---

### Q3. How do you unit test a method that throws an exception under a specific condition?

**Answer:**
"Use the test framework's exception-assertion helper (`Assert.Throws<T>` in xUnit) around the call, and optionally assert on the exception's message/properties for more specific verification."

```csharp
[Fact]
public void Withdraw_ThrowsException_WhenInsufficientFunds()
{
    var account = new BankAccount(balance: 50m);

    var exception = Assert.Throws<InsufficientFundsException>(() => account.Withdraw(100m));

    Assert.Equal("Insufficient funds for this withdrawal.", exception.Message);
}
```

---

### Q4. How do you unit test `async`/`await` code correctly, and what's the classic mistake?

**Answer:**
"Make the test method itself `async Task` (never `async void` — a test framework can't await a void method, so any exception/assertion failure inside it can be silently lost instead of failing the test) and `await` the call under test properly. The classic mistake is writing a synchronous test that calls an async method without awaiting it — the test method returns (and reports as passed) before the async operation has actually completed, meaning the test doesn't really verify anything about what the async code did."

```csharp
// WRONG - test doesn't wait for the async work, can report success even if the async code fails
[Fact]
public void ProcessOrder_Wrong() { _service.ProcessOrderAsync(order); } // not awaited!

// CORRECT
[Fact]
public async Task ProcessOrder_Correct()
{
    var result = await _service.ProcessOrderAsync(order);
    Assert.True(result.Success);
}
```

---

### Q5. What's the difference between `[Fact]` and `[Theory]` in xUnit?

**Answer:**
"`[Fact]` is a single test case with no parameters — it either passes or fails, once. `[Theory]` (paired with `[InlineData]`, `[MemberData]`, or `[ClassData]`) runs the *same* test logic multiple times with different input values — useful for verifying a rule holds across a range of inputs without duplicating the test method body for each case."

```csharp
[Theory]
[InlineData(0, 0, 0)]
[InlineData(2, 3, 5)]
[InlineData(-1, 1, 0)]
public void Add_ReturnsExpectedSum(int a, int b, int expected)
{
    Assert.Equal(expected, new Calculator().Add(a, b));
}
```

---

### Q6. How would you write an integration test for an ASP.NET Core API using `WebApplicationFactory`?

**Answer:**
"`WebApplicationFactory<TEntryPoint>` boots the real app in-memory — real middleware pipeline, real DI, real routing — and hands back an `HttpClient` for sending genuine HTTP requests against it, without needing to deploy anywhere. This is the standard way to verify the whole request pipeline works end to end within the test process. Full mechanics, including swapping out real dependencies for fakes via `ConfigureTestServices`, are covered in [[aspnetcore-07-testing-observability-scenarios-qa]]."

```csharp
public class OrdersApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public OrdersApiTests(WebApplicationFactory<Program> factory) => _client = factory.CreateClient();

    [Fact]
    public async Task GetOrders_ReturnsOk() =>
        Assert.Equal(HttpStatusCode.OK, (await _client.GetAsync("/orders")).StatusCode);
}
```

---

### Q7. How would you integration-test code that uses EF Core without hitting a real production database?

**Answer:**
"Options range from the EF Core InMemory provider (fast, but doesn't validate real SQL translation) to SQLite in-memory mode (closer to real relational behavior, still fast) to a real, disposable SQL Server instance via Testcontainers (slowest, but the only option that genuinely validates real SQL Server-specific behavior). The trade-offs between these three, and when each is the right call, are covered in full in [[linq-05-efcore-advanced-scenarios-qa]]."

---

### Q8. What is FluentAssertions, and why do teams prefer it over the built-in `Assert` methods?

**Answer:**
"FluentAssertions is a library providing a more readable, chainable assertion syntax (`result.Should().Be(expected)`) instead of the built-in `Assert.Equal(expected, result)` style. Beyond readability, its failure messages are typically far more descriptive out of the box — showing exactly what was expected vs actual, and for collections/objects, often a detailed diff of what specifically didn't match — which makes diagnosing a failing test faster without needing to add custom failure messages manually."

```csharp
// Built-in
Assert.Equal(expectedOrder.Total, actualOrder.Total);

// FluentAssertions - reads closer to natural language, richer failure output
actualOrder.Total.Should().Be(expectedOrder.Total);
actualOrder.Should().BeEquivalentTo(expectedOrder); // deep, property-by-property comparison with a clear diff on failure
```
