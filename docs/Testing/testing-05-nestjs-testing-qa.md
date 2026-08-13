# Testing — NestJS Testing — Interview Q&A

---

### Q1. What's the difference between a NestJS unit test and an e2e test, in terms of what gets instantiated?

**Answer:**
"A unit test instantiates just the class under test (a service, a controller) via `Test.createTestingModule()`, with its dependencies replaced by mocks — nothing else in the app spins up. An e2e test bootstraps the *entire* Nest application (`app.init()`), with the real module graph, real middleware, real guards/pipes/interceptors wired up, and sends real HTTP requests into it (typically via Supertest) — the same 'boot the whole real app in-memory' idea as ASP.NET Core's `WebApplicationFactory` or Angular's full compiled app in an E2E test."

---

### Q2. What is `Test.createTestingModule()`, and how is it the same idea as Angular's `TestBed` or ASP.NET Core's `WebApplicationFactory`?

**Answer:**
"It builds a Nest module specifically for testing — you declare which providers/controllers to include, and can override any provider with a mock, then `.compile()` it to get a real, DI-resolved module instance to pull the class under test from. Conceptually it's the exact same pattern as the other two: give the framework's real dependency injection container a way to construct the thing you're testing with real DI wiring, but with specific dependencies swapped for fakes."

```typescript
const module: TestingModule = await Test.createTestingModule({
  providers: [OrdersService, { provide: OrdersRepository, useValue: mockRepository }]
}).compile();

const service = module.get<OrdersService>(OrdersService); // real DI resolution, fake repository underneath
```

**Cross-question: This is the same underlying pattern across all three frameworks — what does that tell you about how modern frameworks approach testability by design?**
"It shows that testability was a first-class design goal, not an afterthought — all three frameworks are built around dependency injection as a core architectural principle specifically *because* it makes swapping real dependencies for test doubles a natural, supported operation, rather than something requiring hacky workarounds (static mocking, monkey-patching). The convergence on 'spin up the framework's real DI container, but let the test override specific registrations' as the standard testing pattern across .NET, Angular, and Nest isn't a coincidence — it's what falls out naturally once a framework commits seriously to DI as its foundational pattern."

---

### Q3. How do you mock an injected provider in a NestJS unit test?

**Answer:**
"Register a fake value/class against the same DI token the real class expects, using `useValue` (a plain fake object, often built with Jest's `jest.fn()` for the methods) or `useClass` (a full fake class implementation)."

```typescript
const mockRepository = { find: jest.fn(), save: jest.fn() };

const module = await Test.createTestingModule({
  providers: [OrdersService, { provide: OrdersRepository, useValue: mockRepository }]
}).compile();

const service = module.get(OrdersService);
mockRepository.find.mockResolvedValue([{ id: 1, total: 100 }]);

const orders = await service.getAllOrders();
expect(mockRepository.find).toHaveBeenCalled();
```

---

### Q4. How do you write an e2e test for a NestJS controller using Supertest, and what does it exercise that a unit test wouldn't?

**Answer:**
"Bootstrap the full Nest app via `Test.createTestingModule({ imports: [AppModule] }).compile()` then `app.init()`, and use Supertest to fire real HTTP requests at it. Unlike a controller unit test (which calls the controller method directly, in-process, with mocked services), this exercises the real HTTP layer — routing, any global/route-level Guards, Pipes (including validation), Interceptors, and the real serialization of the response — none of which a plain unit test on the controller class touches at all."

```typescript
describe('OrdersController (e2e)', () => {
  let app: INestApplication;

  beforeAll(async () => {
    const moduleFixture = await Test.createTestingModule({ imports: [AppModule] }).compile();
    app = moduleFixture.createNestApplication();
    await app.init();
  });

  it('/orders (GET)', () => {
    return request(app.getHttpServer())
      .get('/orders')
      .expect(200)
      .expect(res => { expect(res.body).toBeInstanceOf(Array); });
  });

  afterAll(async () => await app.close());
});
```

---

### Q5. How do you test a NestJS Guard, Interceptor, or Pipe in isolation?

**Answer:**
"Instantiate it directly (most Guards/Pipes/Interceptors are plain classes with a well-defined method to implement) and call its method with a manually constructed `ExecutionContext` (for Guards/Interceptors) or raw input value (for Pipes) — no need to boot any module at all for a pure logic check, since these are typically simple, self-contained classes."

```typescript
describe('RolesGuard', () => {
  it('allows access when user has the required role', () => {
    const guard = new RolesGuard(new Reflector());
    const context = {
      switchToHttp: () => ({ getRequest: () => ({ user: { roles: ['admin'] } }) }),
      getHandler: () => ({}),
      getClass: () => ({})
    } as unknown as ExecutionContext;

    expect(guard.canActivate(context)).toBe(true);
  });
});
```

---

### Q6. How do you test a NestJS service that depends on TypeORM/Prisma without hitting a real database?

**Answer:**
"For pure unit tests, mock the repository/data-access layer entirely (as in Q3) — fastest, fully isolated, but doesn't verify real query behavior. For integration tests that need to verify actual database interaction (constraints, real query correctness), use a real, disposable test database — SQLite for a lightweight in-process option, or a containerized Postgres/MySQL via Testcontainers for something closer to production. The trade-off is identical in spirit to the EF Core InMemory-vs-SQLite-vs-Testcontainers decision covered in [[linq-05-efcore-advanced-scenarios-qa]]: mocking is fast but doesn't validate real data-layer behavior; a real (test) database is slower but actually proves the query/constraint logic works."

---

### Q7. Jest is the default test runner for both NestJS and Angular — does that mean tests "look the same" across both?

**Answer:**
"The *syntax* looks similar (`describe`/`it`/`expect`, `jest.fn()` for mocks) since it's literally the same test runner and assertion library — but what's actually being tested and how it's wired up differs meaningfully. A NestJS test typically deals with services/controllers/guards operating on plain data (no DOM, no rendering) — closer to a traditional backend unit test. An Angular test, even using Jest as the runner, still needs `TestBed` for anything component-related — DOM rendering, change detection, template bindings — none of which have any equivalent on the NestJS side at all. So: same tool, same basic assertion vocabulary, but genuinely different concerns being exercised, because the two frameworks solve fundamentally different problems (HTTP/business logic vs UI rendering)."
