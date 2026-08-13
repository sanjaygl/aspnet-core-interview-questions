# Testing — TDD, BDD & Testing Methodology — Interview Q&A

---

### Q1. What is TDD, and what does the Red-Green-Refactor cycle mean?

**Answer:**
"Red: write a test for behavior that doesn't exist yet — it fails, because there's no implementation. Green: write the minimum code needed to make that test pass — not the most elegant solution, just enough to pass. Refactor: clean up the implementation (and/or the test) now that you have a passing test as a safety net, without changing behavior. The cycle repeats for the next small piece of behavior."

```csharp
// RED - write this first, it fails (Calculator.Add doesn't exist yet)
[Fact]
public void Add_ReturnsSum() => Assert.Equal(5, new Calculator().Add(2, 3));

// GREEN - just enough code to pass
public class Calculator { public int Add(int a, int b) => a + b; }

// REFACTOR - now safely improve the implementation, the test still passes throughout
```

**Cross-question: What's the actual argument for writing the test before the implementation, rather than right after?**
"Writing the test first forces you to think about the *interface* and expected behavior from the caller's perspective before you're anchored to a specific implementation — which tends to produce more testable, better-designed code, since you're designing 'how would I want to call this' rather than retrofitting a test around code that already exists. It also guarantees the test actually can fail (and thus can meaningfully pass later) — a test written after the implementation risks unconsciously being written to match whatever the code already does, including any bugs, rather than what it *should* do."

---

### Q2. What is BDD, and how do Given/When/Then-style tests differ in intent from a plain unit test?

**Answer:**
"BDD frames tests as executable specifications of *behavior*, written in a structured, near-natural-language style (Given a starting context, When an action occurs, Then an expected outcome follows) — intended to be readable and reviewable by non-developers (product owners, QA), bridging the gap between business requirements and automated tests. A plain unit test is usually written purely from a developer's perspective, verifying an implementation detail or method's output — BDD-style tests aim to read like a description of a business scenario, regardless of the underlying implementation."

```gherkin
Feature: Order Discount
  Scenario: Customer gets a discount on orders over $100
    Given a customer has items totaling $150 in their cart
    When they proceed to checkout
    Then a 10% discount should be applied
```

---

### Q3. What are the different kinds of Test Doubles?

**Answer:**
"Dummy — a placeholder object passed around to satisfy a parameter/constructor signature, never actually used or checked in the test. Stub — provides pre-programmed, canned answers to calls made during the test (e.g., always returns a fixed value), used to control the *state* fed into the code under test. Fake — a working, simplified implementation of a real dependency (an in-memory database standing in for a real one) — has real behavior, just a lighter-weight version. Spy — records information about how it was called (call count, arguments) so the test can inspect that afterward, without necessarily controlling behavior. Mock — pre-programmed with expectations about *how* it should be called, and the test explicitly verifies those expectations were met (it fails the test itself if the expected interaction didn't happen, rather than just letting you inspect it after)."

```csharp
// Stub - just returns a canned value, doesn't verify anything about how it's called
var stub = new Mock<IPriceCalculator>();
stub.Setup(p => p.CalculateTotal(It.IsAny<Order>())).Returns(100m);

// Mock - explicitly verifies an expected interaction happened
var mock = new Mock<IEmailService>();
// ... run the code under test ...
mock.Verify(m => m.SendConfirmation(It.IsAny<string>()), Times.Once); // test FAILS if this didn't happen
```

**Cross-question: If you assert on how many times a method was called and with what arguments, are you using a stub or a mock — and why does that distinction matter?**
"That's mock usage specifically — you're verifying *behavior/interaction* (did this get called, how many times, with what), not just controlling what data flows into the system under test. The distinction matters because over-using interaction verification (mocking everything and asserting call counts everywhere) tends to produce brittle tests tightly coupled to implementation details — refactoring the internal way a class calls its dependencies (even without changing observable behavior) can break a pile of mock-verification tests that were never actually about the observable *result*. A good rule of thumb: prefer asserting on the *outcome* (a stub feeding in data, then checking the returned/resulting state) over asserting on *how* it got there, unless the interaction itself (e.g., 'did we actually send the email') is the behavior genuinely being tested."

---

### Q4. What is Code Coverage, and why isn't "100% coverage" the same as "well-tested"?

**Answer:**
"Code coverage measures what percentage of lines/branches were *executed* by the test suite — it says nothing about whether the test actually *asserted* anything meaningful about the result. A test that calls a method and checks nothing (or checks something trivially true) still counts as covering every line that method executed, while verifying absolutely nothing about correctness."

```csharp
[Fact]
public void ProcessOrder_DoesNotThrow() // executes every line, asserts NOTHING useful, still "100% coverage"
{
    var service = new OrderService();
    service.ProcessOrder(new Order()); // no assertion on the actual result at all
}
```

**Where this comes up:** coverage is a useful signal for finding *untested* code (0% coverage on a file is a real red flag), but a high coverage number alone tells you nothing about test quality — it can be trivially gamed, intentionally or not.

---

### Q5. What is Mutation Testing, and what problem does it solve that coverage numbers can't?

**Answer:**
"Mutation testing (tools like Stryker for .NET/JS) automatically introduces small, deliberate bugs ('mutants') into your code — flipping a `>` to `>=`, changing a `+` to `-`, negating a condition — then re-runs your test suite against each mutant. If a test suite still passes despite the introduced bug, that mutant 'survived,' revealing a gap: your tests execute that code (so coverage looks fine) but don't actually *verify* it correctly enough to catch a real defect there. It directly answers the question code coverage can't: not just 'was this line executed,' but 'would my tests actually catch a bug here.'"

---

### Q6. What is the AAA pattern, and why does it matter?

**Answer:**
"Arrange — set up the inputs, dependencies, and initial state needed for the test. Act — execute the one thing actually being tested. Assert — verify the outcome matches expectations. Structuring every test this way, consistently, makes tests fast to read and review — anyone skimming a test file immediately knows where setup ends and the actual behavior-under-test begins, without needing to trace through intermingled setup/execution/verification code."

```csharp
[Fact]
public void ApplyDiscount_ReducesTotalByTenPercent()
{
    // Arrange
    var order = new Order { Total = 100m };
    var service = new DiscountService();

    // Act
    var result = service.ApplyDiscount(order, 0.10m);

    // Assert
    Assert.Equal(90m, result.Total);
}
```

---

### Q7. What's the difference between a "flaky" test and a genuinely failing test, and what causes flakiness?

**Answer:**
"A genuinely failing test fails consistently, every time, because the code under test actually has a bug or the test's expectation is correct and unmet. A flaky test fails *intermittently* — sometimes passing, sometimes failing, with no change to the code — which is far more corrosive to a team's trust in the suite, since people start assuming red failures are 'probably just flaky' and re-run the build instead of investigating, eventually missing real failures hidden among the noise. Common root causes: timing/async issues (asserting before an async operation has actually completed, or a fixed `sleep()` that's sometimes too short), shared mutable state leaking between tests (a static field, a shared database row, tests not cleaning up after themselves), and tests that make real network/external calls (subject to real-world latency/availability, rather than being fully isolated)."

**Where to use:** treat any flaky test as a priority to fix or quarantine (mark it explicitly skipped/tracked) rather than ignoring it — a suite people don't trust stops providing the safety net it exists for.
