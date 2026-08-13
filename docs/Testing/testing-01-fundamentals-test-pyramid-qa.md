# Testing — Fundamentals & the Test Pyramid — Interview Q&A

---

### Q1. What is a Unit Test — what actually makes something "a unit," and does that definition change across C#, Angular, and NestJS?

**Answer:**
"A unit test verifies a single piece of logic — typically one method or one class — in complete isolation from its real dependencies (a database, the filesystem, another service, the network), with those dependencies replaced by test doubles. That definition of 'unit' is the same concept in C#, Angular, and NestJS: a class with its collaborators faked out. What differs is just the tooling (xUnit/Moq in C#, Jasmine/Jest in Angular, Jest in NestJS) and, in Angular's case specifically, that 'a unit' sometimes gets stretched to mean 'a component rendered via `TestBed`,' which is a looser definition than a plain isolated class test — see the cross-question."

```csharp
// C# - OrderService in isolation, IPaymentGateway faked out
var mockGateway = new Mock<IPaymentGateway>();
var service = new OrderService(mockGateway.Object);
```

```typescript
// NestJS - OrdersService in isolation, repository faked out
const module = await Test.createTestingModule({
  providers: [OrdersService, { provide: OrdersRepository, useValue: mockRepository }]
}).compile();
```

**Cross-question: If a "unit" test for an Angular component renders the component via `TestBed` (real DOM, real change detection), is that still a unit test, or has it quietly become an integration test?**
"It's a genuinely blurry middle ground, and worth being honest about in an interview rather than papering over it. A `TestBed` component test still typically fakes out injected *services* (so it's not touching a real backend), but it exercises the real Angular compiler output, real template bindings, and real change detection — none of which a strict 'one class, fully isolated' definition would call a pure unit test. Most teams pragmatically call these 'component tests' or still bucket them as unit tests since they don't cross a process/network boundary, but the more precise answer is: it sits between a pure unit test and an integration test, and the label matters less than understanding what's actually being exercised."

---

### Q2. What is an Integration Test, and what's actually being "integrated"?

**Answer:**
"An integration test verifies that two or more real components work correctly together — and 'real' is the key word: instead of faking a dependency, the test exercises an actual database, an actual HTTP call to another service, or at minimum, several of your own classes wired together through real dependency injection instead of mocks. The specific boundary being tested varies: it could be 'does my repository class correctly talk to a real (test) database,' or 'does my whole API pipeline — routing, middleware, controller, DB — work end to end within the process,' as with `WebApplicationFactory`."

```csharp
// Integration test - real DbContext against a real (test) SQLite database, not mocked
var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options;
using var context = new AppDbContext(options);
var repository = new OrderRepository(context); // real repository, real (test) database underneath
```

---

### Q3. What is an End-to-End (E2E) Test, and why does the same E2E tooling work regardless of frontend framework?

**Answer:**
"An E2E test drives the *entire* running system from the outside, the same way a real user or a real API consumer would — clicking through a real browser against a fully deployed (or locally running) frontend and backend, with nothing faked at all. The reason Playwright/Cypress/Selenium work identically whether the app is Angular, React, or server-rendered HTML is that E2E tooling operates purely at the level of 'what's rendered in the DOM' and 'what HTTP calls go over the wire' — it has no awareness of, or dependency on, the framework that produced that DOM. That's actually a clean signal for what E2E testing is fundamentally about: verifying observable behavior from the outside, not internal implementation."

```typescript
// Playwright - this exact code works against an Angular app, a React app, or plain HTML - it doesn't know or care
await page.goto('https://myapp.com/login');
await page.fill('#username', 'testuser');
await page.click('button[type=submit]');
await expect(page).toHaveURL('/dashboard');
```

---

### Q4. What is the Test Pyramid, and why does it recommend many unit tests, fewer integration tests, and very few E2E tests?

**Answer:**
"The Test Pyramid is a shape recommendation for how a healthy test suite's composition should look: a large base of fast, cheap, isolated unit tests; a smaller middle layer of integration tests (slower, since they touch real infrastructure); and a thin top layer of E2E tests (slowest, most brittle, but highest confidence that the whole system actually works together). The reasoning is a straight cost/speed/reliability trade-off — unit tests run in milliseconds and pinpoint exactly what broke, while E2E tests take seconds-to-minutes each, are more prone to flakiness (network timing, environment issues), and when one fails, it tells you 'something in this whole flow is broken' without pinpointing where."

**Cross-question: What goes wrong for a team that inverts the pyramid — mostly E2E tests, few unit tests?**
"The classic 'ice cream cone' anti-pattern. CI runs become slow (E2E suites can take tens of minutes to hours), flaky (E2E tests are inherently more sensitive to timing/environment issues, so intermittent failures erode trust in the suite and people start ignoring red builds), and failures are expensive to diagnose (a failing E2E test tells you *that* checkout is broken, not *which* of the dozen components involved caused it). Bugs also get caught much later in the cycle — a unit test fails in seconds, locally, before a commit; an E2E failure might only surface after a full deploy to a test environment."

---

### Q5. What is a Smoke Test, and how is it different from a full regression suite?

**Answer:**
"A Smoke Test is a small, fast set of checks confirming the absolute basics work — the app starts, the login page loads, the health endpoint responds, a critical API returns 200 — run immediately after a deployment to catch a catastrophically broken build before doing anything else. A full regression suite is comprehensive — re-verifying that existing features still work correctly after a change, covering edge cases and less-critical paths too. Smoke tests answer 'is this build even worth testing further'; regression tests answer 'did this change break anything, anywhere, in what we already had working.'"

---

### Q6. What is a Sanity Test, and how is it different from a Smoke Test?

**Answer:**
"They're often used loosely/interchangeably in practice, but the more precise distinction: a Smoke Test is broad and shallow — a handful of checks across the whole system confirming basic stability, usually run on every build. A Sanity Test is narrow and deep — focused specifically on verifying that one particular area or a specific bug fix actually works correctly, typically run after a targeted change, without re-checking the entire application. If Smoke asks 'is the build stable enough to bother testing,' Sanity asks 'does this specific thing I just changed actually behave correctly now.'"

---

### Q7. What is Regression Testing, and how does an automated suite reduce manual regression testing?

**Answer:**
"Regression testing re-verifies that previously-working functionality still works after a code change — catching cases where a fix or new feature accidentally broke something unrelated. Before comprehensive automated suites existed, this meant manually re-clicking through the entire application before every release, which doesn't scale and gets skipped or rushed under deadline pressure. An automated regression suite (unit + integration + a thin layer of E2E, per the pyramid) runs the same checks consistently, in minutes, on every single change — turning an expensive, error-prone manual process into a fast, repeatable, always-on safety net."

---

### Q8. What is Acceptance Testing (UAT), and is it the same audience/purpose as a developer-written E2E test?

**Answer:**
"Acceptance Testing (User Acceptance Testing) verifies the system meets actual business/user requirements — often performed by product owners, QA, or actual end users against acceptance criteria defined *before* the feature was built, answering 'is this what we actually needed,' not just 'does the code work as the developer intended.' A developer-written E2E test is technically similar in mechanics (drives the real running app), but it's usually derived from the developer's own understanding of the feature and run automatically in CI — it verifies 'the implementation still matches what I, the developer, believe it should do,' which isn't automatically the same thing as 'this satisfies what the business actually asked for.' BDD-style acceptance criteria (Given/When/Then) are often the bridge between the two, letting the same scenarios inform both UAT and automated E2E coverage."

---

### Q9. Is "unit test" a language-agnostic concept, or does .NET/Angular/NestJS mean something different by it?

**Answer:**
"The *concept* is genuinely language-agnostic: isolate one piece of logic, fake its dependencies, verify its behavior fast and deterministically. What differs across .NET, Angular, and NestJS is: (1) the tooling — xUnit+Moq, Jasmine/Jest+TestBed, Jest+`Test.createTestingModule()` — which all serve the equivalent role of 'give me an isolated unit with fakeable dependencies,' and (2) how loosely the term gets applied in practice — a NestJS service unit test with a mocked repository is a clean, strict unit test; an Angular component test through `TestBed` is looser (real DOM, real change detection) even though teams still call it a unit test. So: same underlying idea, same fundamental trade-offs (fast/isolated vs slow/realistic), different ergonomics and different local conventions for exactly where the 'unit' boundary is drawn in practice."
