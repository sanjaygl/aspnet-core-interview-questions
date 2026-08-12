# Angular — Testing & Advanced/Scenario-Based — Interview Q&A

---

### Q1. What is `TestBed`, and how does it differ from just instantiating a component class directly?

**Answer:**
"`TestBed` builds a real Angular testing module — it compiles the component's template, wires up dependency injection, and gives you a `ComponentFixture` with access to the actual rendered DOM, change detection triggering, and the component instance together. Just instantiating the class directly (`new MyComponent(mockService)`) tests the class's logic in isolation, but tells you nothing about whether the template actually binds correctly, whether directives/pipes work, or whether DI is wired up as configured in the real app — `TestBed` is what makes it a true Angular *component* test rather than a plain class unit test."

```typescript
TestBed.configureTestingModule({
  imports: [UserCardComponent], // standalone component
  providers: [{ provide: UserService, useValue: mockUserService }]
});
const fixture = TestBed.createComponent(UserCardComponent);
fixture.detectChanges(); // triggers initial render + change detection
```

---

### Q2. How would you mock an injected service dependency in a component test?

**Answer:**
"Override the provider inside `TestBed.configureTestingModule` with a fake implementation — either a hand-written stub object with the methods the component actually calls, or a Jasmine/Jest spy object (`jasmine.createSpyObj`) so you can assert on calls and control return values without touching a real service (and definitely without hitting a real API)."

```typescript
const mockUserService = jasmine.createSpyObj('UserService', ['getUsers']);
mockUserService.getUsers.and.returnValue(of([{ id: 1, name: 'Test User' }]));

TestBed.configureTestingModule({
  imports: [UserListComponent],
  providers: [{ provide: UserService, useValue: mockUserService }]
});
```

---

### Q3. How do you test a component that depends on an Observable/async data source?

**Answer:**
"Provide a mock service returning a controllable Observable (`of(...)` for a simple synchronous emission), call `fixture.detectChanges()` to trigger rendering, and use `fixture.whenStable()` (or `fakeAsync`/`tick()` for time-based Observables like `debounceTime`) before asserting on the rendered DOM, since the async pipe/subscription needs a change detection cycle to actually update the template after the Observable emits."

```typescript
it('should display users', fakeAsync(() => {
  mockUserService.getUsers.and.returnValue(of([{ id: 1, name: 'Test User' }]));
  fixture.detectChanges();
  tick(); // flush any pending async work (timers, promises)
  fixture.detectChanges(); // re-render after the data arrived
  expect(fixture.nativeElement.textContent).toContain('Test User');
}));
```

**Where to use:** for genuinely time-sensitive RxJS operators (`debounceTime`, `delay`), RxJS's own `TestScheduler`/marble testing lets you assert on exact timing/emission sequences without real `tick()`-based clock juggling — reach for it when timing itself is what's being tested, not just the eventual result.

---

### Q4. How would you diagnose a performance problem in a production Angular app?

**Answer:**
"Start with the Angular DevTools browser extension's Profiler — it records a change detection timeline and shows exactly which components were checked, how often, and how long each took, which usually points straight at the offender (often a component without `OnPush` re-rendering constantly, or an expensive pure computation running on every cycle instead of being memoized). Beyond change detection specifically, browser DevTools' Performance tab and Lighthouse cover bundle size, load time, and general rendering bottlenecks. Common root causes once found: missing `OnPush`, missing `trackBy` on large lists, an impure pipe doing heavy work, or a subscription re-running expensive logic more often than necessary."

---

### Q5. What's involved in upgrading a large Angular application across major versions, and what's usually the riskiest part?

**Answer:**
"Angular's `ng update` handles a lot of the mechanical migration automatically (schematics rewrite deprecated APIs, update dependencies) for most one-major-version-at-a-time upgrades, which is the officially supported path — skipping several majors at once compounds risk and isn't well supported. The riskiest part in practice is usually third-party library compatibility — a major Angular version bump can leave some dependencies not yet updated to support it, which sometimes forces workarounds or delays. Deprecated API removals (things flagged deprecated a few versions earlier finally being deleted) are the other common breakage point — running `ng update` one version at a time and fixing deprecation warnings as they appear, rather than ignoring them until forced, is the standard way to keep upgrades low-risk."

---

### Q6. What is Angular Universal (SSR), and what problem does it solve?

**Answer:**
"Angular Universal renders the initial page on the server, sending fully-formed HTML to the browser instead of an empty shell that only fills in after the JavaScript bundle downloads and bootstraps. It solves two things a pure client-side SPA struggles with: SEO (search engine crawlers see real content immediately, not an empty `<app-root>`), and perceived performance — users see meaningful content sooner (better First Contentful Paint), even though the app still needs to 'hydrate' (attach event listeners, become interactive) once the JS bundle loads."

---

### Q7. How would you approach breaking up a large Angular monolith into micro-frontends?

**Answer:**
"Identify genuinely independent feature boundaries — ones that could reasonably be owned, built, and deployed by separate teams without constant coordination. Common technical approaches: Module Federation (Webpack, and Angular's build tooling now supports it directly) lets separate Angular applications be built and deployed independently, then composed together at runtime, loading each other's code on demand. Each micro-frontend needs its own build/deploy pipeline, and you need a clear contract for shared concerns — a shared design system/component library, a consistent auth/session mechanism across the split apps, and careful version alignment for shared dependencies (Angular itself, RxJS) to avoid loading multiple incompatible copies at runtime. This is real architectural complexity — worth it mainly when team/organizational boundaries genuinely need independent deployability, not as a default for a single team's app."
