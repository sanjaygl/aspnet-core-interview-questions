# Testing — Cross-Stack E2E/Smoke/Other Types + Coding Practice — Interview Q&A

---

### Q1. Playwright vs Cypress vs Selenium — what are the actual differences?

**Answer:**
"Selenium is the oldest, drives real browsers via the WebDriver protocol, supports the widest range of browsers/languages, but is generally slower and historically more prone to flaky timing issues, needing more manual wait-handling. Cypress runs inside the browser itself (rather than remote-controlling it), giving fast, reliable execution with great debugging (time-travel snapshots of each step), but historically was limited to one origin/tab at a time and JavaScript/TypeScript only. Playwright (Microsoft) is the newer common default — runs outside the browser like Selenium (so it supports multi-tab/multi-origin scenarios Cypress historically struggled with) but with modern auto-waiting built in (reduces flakiness without manual waits), fast parallel execution, and strong multi-language support (JS/TS, Python, .NET, Java)."

---

### Q2. Why does the same E2E tool work identically regardless of frontend framework?

**Answer:**
"E2E tools operate purely at the browser/DOM and network level — clicking elements, reading rendered text, intercepting HTTP calls — with zero awareness of whatever framework produced that DOM or made those calls. This directly confirms what E2E testing is fundamentally about: verifying observable, black-box behavior from a real user's perspective, not testing any framework-specific internal mechanism. It's the same reasoning already covered for Angular specifically in [[testing-04-angular-testing-qa]], generalized: if your E2E suite somehow needed framework-specific knowledge to work, it would no longer be testing 'does this behave correctly to an outside observer,' which is the entire point of that test layer."

---

### Q3. What is Contract Testing, and what problem does it solve that neither unit tests nor full E2E tests solve well?

**Answer:**
"Contract testing (Pact is the standard tool) verifies that a service consumer's expectations of a provider's API (request shape, response shape) match what the provider actually delivers — *without* either side needing the other actually running during the test. The consumer records its expectations as a 'contract'; the provider replays that contract against its real implementation to verify compliance. This fills a real gap: a unit test can't catch a mismatch between two independently-deployed services at all (it only tests one side in isolation), and a full E2E test *can* catch it, but only by standing up both real services together, which is exactly the kind of slow, heavyweight, hard-to-maintain-across-many-services test the Test Pyramid recommends minimizing — especially relevant across the many independently-deployed services described in [[microservices-03-data-management-qa]]. Contract testing catches the same class of integration mismatch far more cheaply and independently."

---

### Q4. What is Visual Regression Testing, and how is it different from a functional E2E assertion?

**Answer:**
"A functional E2E assertion checks *behavior* — did clicking this button navigate to the right page, does this text appear. Visual Regression Testing (Percy, Chromatic, Playwright's built-in screenshot comparison) captures a screenshot of a rendered page/component and compares it pixel-by-pixel (or with perceptual diffing) against a previously-approved baseline, flagging any visual difference — even ones that don't break any functional behavior at all, like a CSS change accidentally shifting a layout or changing a color. It catches an entire category of bug functional tests are blind to: things that still 'work' correctly but look wrong."

---

### Q5. What is Load/Performance Testing, and why is it a separate discipline from functional testing?

**Answer:**
"Tools like k6, JMeter, or Gatling simulate many concurrent users/requests against a system to measure how it behaves under realistic or peak load — response times, error rates, and resource usage as traffic scales up. This is a genuinely separate concern from functional correctness: a passing functional test suite proves the system produces correct results for a *single* request in isolation, but tells you absolutely nothing about whether it stays correct and responsive under 1,000 concurrent requests, where entirely different failure modes emerge — connection pool exhaustion, database lock contention, memory pressure, thread starvation — none of which a functional test, running one request at a time, could ever surface."

---

### Q6. What test types would you actually add during day-to-day development on a feature?

**Answer:**
"Realistically, as a workflow: while writing the business logic itself, unit tests for each class/method as it's written (often TDD-style, or immediately after) — fast feedback, run constantly locally. Once a feature touches a real dependency (database, another service), an integration test verifying that specific interaction actually works, not just the isolated logic. Once the feature is wired into the actual API/UI, a small number of E2E tests covering the critical happy path and maybe one or two critical failure paths — not exhaustive, just enough to prove the whole thing actually works together end to end. In CI specifically: the full unit + integration suite runs on every commit/PR (fast enough to not block iteration); a smoke test suite runs immediately after any deployment, before anything else, to catch a catastrophically broken build fast; the full E2E/regression suite runs on a schedule or before a release, since it's the slowest layer and doesn't need to gate every single commit. Each layer earns its place by catching a class of bug the cheaper layers below it can't."

---

### Q7. Write a C# xUnit test using Moq for a service with one dependency.

**Answer:**
```csharp
public class OrderServiceTests
{
    [Fact]
    public void CompleteOrder_ChargesCorrectAmount()
    {
        // Arrange
        var mockGateway = new Mock<IPaymentGateway>();
        mockGateway.Setup(g => g.Charge(It.IsAny<decimal>())).Returns(true);
        var service = new OrderService(mockGateway.Object);
        var order = new Order { Total = 150m };

        // Act
        var result = service.CompleteOrder(order);

        // Assert
        Assert.True(result.Success);
        mockGateway.Verify(g => g.Charge(150m), Times.Once);
    }
}
```

---

### Q8. Write an Angular component test using `TestBed`, mocking an injected service.

**Answer:**
```typescript
describe('UserListComponent', () => {
  let mockUserService: jasmine.SpyObj<UserService>;

  beforeEach(() => {
    mockUserService = jasmine.createSpyObj('UserService', ['getUsers']);
    mockUserService.getUsers.and.returnValue(of([{ id: 1, name: 'Test User' }]));

    TestBed.configureTestingModule({
      imports: [UserListComponent],
      providers: [{ provide: UserService, useValue: mockUserService }]
    });
  });

  it('should display users', () => {
    const fixture = TestBed.createComponent(UserListComponent);
    fixture.detectChanges();
    expect(fixture.nativeElement.textContent).toContain('Test User');
  });
});
```

---

### Q9. Write a NestJS unit test for a service using `Test.createTestingModule()` and a mocked repository.

**Answer:**
```typescript
describe('OrdersService', () => {
  let service: OrdersService;
  let mockRepository: { find: jest.Mock; save: jest.Mock };

  beforeEach(async () => {
    mockRepository = { find: jest.fn(), save: jest.fn() };
    const module = await Test.createTestingModule({
      providers: [OrdersService, { provide: OrdersRepository, useValue: mockRepository }]
    }).compile();
    service = module.get(OrdersService);
  });

  it('should return all orders', async () => {
    mockRepository.find.mockResolvedValue([{ id: 1, total: 100 }]);
    const orders = await service.getAllOrders();
    expect(orders).toHaveLength(1);
    expect(mockRepository.find).toHaveBeenCalled();
  });
});
```

---

### Q10. Write a NestJS e2e test for a controller endpoint using Supertest.

**Answer:**
```typescript
describe('OrdersController (e2e)', () => {
  let app: INestApplication;

  beforeAll(async () => {
    const moduleFixture = await Test.createTestingModule({ imports: [AppModule] }).compile();
    app = moduleFixture.createNestApplication();
    await app.init();
  });

  it('POST /orders creates an order', () => {
    return request(app.getHttpServer())
      .post('/orders')
      .send({ customerName: 'John Doe', quantity: 2 })
      .expect(201)
      .expect(res => { expect(res.body.customerName).toBe('John Doe'); });
  });

  afterAll(async () => await app.close());
});
```

---

### Q11. Write a Playwright E2E test for a login flow.

**Answer:**
```typescript
import { test, expect } from '@playwright/test';

test('user can log in and see the dashboard', async ({ page }) => {
  await page.goto('https://myapp.com/login');
  await page.fill('#username', 'testuser@example.com');
  await page.fill('#password', 'correct-password');
  await page.click('button[type=submit]');

  await expect(page).toHaveURL('https://myapp.com/dashboard');
  await expect(page.locator('h1')).toHaveText('Welcome, testuser');
});
```

---

### Q12. Write a minimal smoke test suite for an API — what would you deliberately leave out?

**Answer:**
"Keep it to the handful of checks that answer 'is this build catastrophically broken' — nothing exhaustive."

```typescript
import { test, expect } from '@playwright/test';

test.describe('Smoke Tests', () => {
  test('health endpoint responds', async ({ request }) => {
    const res = await request.get('/health');
    expect(res.status()).toBe(200);
  });

  test('login page loads', async ({ page }) => {
    await page.goto('/login');
    await expect(page.locator('form')).toBeVisible();
  });

  test('critical API returns data', async ({ request }) => {
    const res = await request.get('/api/products?limit=1');
    expect(res.status()).toBe(200);
  });
});
```

"Deliberately left out: edge cases, error-path validation, permission/authorization variations, and anything covering a non-critical feature — those belong in the full regression/E2E suite, not smoke tests. A smoke suite should run in well under a minute; the moment it starts covering business-logic correctness rather than 'is anything on fire,' it's stopped being a smoke test and become a slow, redundant subset of the regression suite."
