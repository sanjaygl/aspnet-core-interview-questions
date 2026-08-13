# Software Testing — Interview Questions — Index

Same format as the other series — spoken answer + code example + cross-questions once built. This one is explicitly cross-stack: the core testing *concepts* (unit/integration/e2e/TDD/etc.) are the same regardless of language, but the tooling and what counts as "a unit" differs by stack — that distinction gets its own question rather than being assumed. Grouped into 6 files.

---

## File 1 — `testing-01-fundamentals-test-pyramid-qa.md`
**Testing Fundamentals & the Test Pyramid (cross-stack concepts)**
1. What is a Unit Test — what actually makes something "a unit," and does that definition change across C#, Angular, and NestJS?
   - *Cross-question:* If a "unit" test for an Angular component renders the component via `TestBed` (real DOM, real change detection), is that still a unit test, or has it quietly become an integration test?
2. What is an Integration Test, and what's actually being "integrated" — two classes in the same process, or two separate real systems (a real database, a real HTTP call)?
3. What is an End-to-End (E2E) Test, and why is E2E tooling (Playwright/Cypress/Selenium) largely the same regardless of whether the app underneath is Angular, React, or server-rendered — what does that tell you about what E2E is actually testing?
4. What is the Test Pyramid, and why does it recommend many unit tests, fewer integration tests, and very few E2E tests?
   - *Cross-question:* What goes wrong (cost- and speed-wise) for a team that inverts the pyramid — mostly E2E tests, few unit tests?
5. What is a Smoke Test, and how is it different from a full regression suite?
6. What is a Sanity Test, and how is it different from a Smoke Test — people often use these two terms interchangeably; what's the actual distinction?
7. What is Regression Testing, and how does an automated test suite reduce the need for manual regression testing before every release?
8. What is Acceptance Testing (UAT), and who is it really for — is it the same audience/purpose as a developer-written E2E test?
9. Is "unit test" a language-agnostic concept, or does .NET/Angular/NestJS mean something different by it? *(the direct answer to your core question — summarized from Q1 plus the stack-specific files below)*

## File 2 — `testing-02-tdd-methodologies-test-doubles-qa.md`
**TDD, BDD & Testing Methodology**
1. What is TDD (Test-Driven Development), and what does the Red-Green-Refactor cycle actually mean step by step?
   - *Cross-question:* What's the actual argument for writing the test *before* the implementation, rather than right after — what does writing it first supposedly catch that writing it after doesn't?
2. What is BDD (Behavior-Driven Development), and how do Given/When/Then-style tests (e.g., SpecFlow in .NET, Cucumber, Jest's `describe`/`it` read that way too) differ in intent from a plain unit test?
3. What are the different kinds of Test Doubles — Dummy, Stub, Fake, Spy, and Mock — and what's actually different about each (people use "mock" as a catch-all, which blurs a real distinction)?
   - *Cross-question:* If you assert on *how many times* a method was called and *with what arguments*, are you using a stub or a mock — and why does that distinction matter for what the test is actually verifying?
4. What is Code Coverage, and why is "100% coverage" not the same thing as "well-tested" — what's a concrete example of a test that inflates coverage without actually verifying behavior?
5. What is Mutation Testing, and what problem does it solve that code coverage numbers alone can't catch?
6. What is the AAA pattern (Arrange-Act-Assert), and why does consistently structuring tests this way matter for readability at scale?
7. What's the difference between a "flaky" test and a genuinely failing test, and what are the most common root causes of flakiness (timing/async issues, shared state between tests, real network calls)?

## File 3 — `testing-03-dotnet-unit-integration-qa.md`
**C# / .NET Unit & Integration Testing**
1. xUnit vs NUnit vs MSTest — what are the practical differences, and which is the modern default for new .NET projects?
2. How do you mock a dependency in a C# unit test (Moq/NSubstitute), and what's the difference between `.Setup()` and `.Verify()`?
3. How do you unit test a method that throws an exception under a specific condition?
4. How do you unit test `async`/`await` code correctly — what's the classic mistake that makes an async test pass even when it shouldn't?
5. What's the difference between `[Fact]` and `[Theory]` (xUnit), and when would you use `[Theory]` with `[InlineData]`?
6. How would you write an integration test for an ASP.NET Core API using `WebApplicationFactory`? *(cross-reference: [[aspnetcore-07-testing-observability-scenarios-qa]] for the full mechanics)*
7. How would you integration-test code that uses EF Core without hitting a real production database? *(cross-reference: [[linq-05-efcore-advanced-scenarios-qa]] for InMemory vs SQLite vs Testcontainers trade-offs)*
8. What is FluentAssertions (or similar), and why do teams prefer `result.Should().Be(expected)` over `Assert.Equal(expected, result)`?

## File 4 — `testing-04-angular-testing-qa.md`
**Angular Testing**
1. What's the difference between testing an Angular component with `TestBed` vs testing a plain service/pipe/pure function directly with `new`?
   - *Cross-question:* Since `TestBed`-based component tests render real DOM and trigger real change detection, where do these actually sit on the unit-vs-integration spectrum from File 1?
2. How do you mock `HttpClient` in an Angular test — what's the difference between `HttpClientTestingModule` and manually providing a fake service?
3. How do you test a component that depends on an Observable/async data source? *(cross-reference: [[angular-06-testing-advanced-scenarios-qa]] for the full `fakeAsync`/`tick()` mechanics)*
4. What's the difference between Jasmine/Karma (the historical default) and Jest for Angular testing, and why have many teams migrated to Jest?
5. What is a Angular E2E test actually driving — the compiled, running application in a real browser, or something more like `TestBed`? What does that imply about how much of the real stack (routing, HTTP, backend) is actually exercised?
6. What is Snapshot Testing, and is it commonly used in Angular the way it is in React — why or why not?

## File 5 — `testing-05-nestjs-testing-qa.md`
**NestJS Testing**
1. What's the difference between a NestJS unit test and an e2e test, in terms of what actually gets instantiated — a single provider vs the whole Nest application?
2. What is `Test.createTestingModule()`, and how is it conceptually the same idea as Angular's `TestBed` or ASP.NET Core's `WebApplicationFactory`?
   - *Cross-question:* This is the same underlying pattern across all three frameworks — what does that tell you about how modern frameworks approach testability by design?
3. How do you mock an injected provider (e.g., a repository or another service) in a NestJS unit test?
4. How do you write an e2e test for a NestJS controller using Supertest, and what does it actually exercise that a unit test on the controller class wouldn't?
5. How do you test a NestJS Guard, Interceptor, or Pipe in isolation?
6. How do you test a NestJS service that depends on TypeORM/Prisma without hitting a real database — what are the trade-offs of mocking the repository vs using a real test database (SQLite/Testcontainers)?
7. Jest is the default test runner for both NestJS and (commonly) Angular — does that mean tests "look the same" across both, or does what's actually being tested still differ significantly?

## File 6 — `testing-06-e2e-smoke-crossstack-coding-qa.md`
**Cross-Stack E2E/Smoke/Other Test Types + Coding Practice**
1. Playwright vs Cypress vs Selenium — what are the actual differences, and why has Playwright become the common modern default?
2. Why does the *same* E2E tool (Playwright, say) work identically whether the frontend is Angular, React, or plain server-rendered HTML — what does that confirm about what E2E testing actually cares about?
3. What is Contract Testing (e.g., Pact), and what specific problem does it solve in a microservices setup that neither unit tests nor full E2E tests solve well? *(cross-reference: [[microservices-03-data-management-qa]] for the broader distributed-systems context)*
4. What is Visual Regression Testing, and how is it different from a functional E2E assertion?
5. What is Load/Performance Testing (e.g., k6, JMeter), and why is it a genuinely separate discipline from functional testing — what does a passing functional test suite tell you nothing about?
6. What test types would you actually add during day-to-day development on a feature (not just "at the end") — walk through unit → integration → smoke → E2E as a real workflow, and where each one earns its keep in CI.
7. Write a C# xUnit test using Moq for a service with one dependency (Arrange-Act-Assert).
8. Write an Angular component test using `TestBed`, mocking an injected service.
9. Write a NestJS unit test for a service using `Test.createTestingModule()` and a mocked repository.
10. Write a NestJS e2e test for a controller endpoint using Supertest.
11. Write a Playwright E2E test for a login flow.
12. Write a minimal smoke test suite (a handful of critical-path checks) for an API — what would you deliberately leave out of it, and why?
