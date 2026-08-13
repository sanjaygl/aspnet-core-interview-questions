# Testing — Angular Testing — Interview Q&A

---

### Q1. What's the difference between testing a component with `TestBed` vs testing a plain service/pipe directly with `new`?

**Answer:**
"A pure service, pipe, or standalone function with no Angular-specific dependencies can just be instantiated directly with `new` and tested like any plain class — no Angular test infrastructure needed at all, fastest possible test. A component needs `TestBed` because its template requires compilation, and its constructor typically needs real dependency injection wiring — `TestBed.createComponent()` gives you a `ComponentFixture` with the actual rendered DOM and the ability to trigger change detection, which a plain `new MyComponent(...)` can't provide (no template gets compiled/rendered at all that way)."

```typescript
// Plain class - no TestBed needed at all
it('should format currency', () => {
  const pipe = new CurrencyFormatPipe();
  expect(pipe.transform(100)).toBe('$100.00');
});

// Component - needs TestBed for template compilation + DI + change detection
TestBed.configureTestingModule({ imports: [UserCardComponent] });
const fixture = TestBed.createComponent(UserCardComponent);
fixture.detectChanges();
```

**Cross-question: Since `TestBed`-based component tests render real DOM and trigger real change detection, where do these sit on the unit-vs-integration spectrum?**
"They're a hybrid, and worth naming as such rather than forcing them into one bucket. The component's *injected services* are typically still faked out (so it's not reaching a real backend), which keeps it closer to a unit test in spirit — but the actual template compilation, DOM rendering, and change detection are entirely real, which a strict unit test definition wouldn't allow. Most teams pragmatically still call these unit tests since no network/process boundary is crossed, but the more precise framing is 'component test' — a distinct category that's more integrated than testing a plain class, but far more isolated than a full E2E test."

---

### Q2. How do you mock `HttpClient` in an Angular test?

**Answer:**
"`HttpClientTestingModule` (paired with `HttpTestingController`) intercepts outgoing HTTP calls and lets the test assert exactly what request was made, then manually supply the response — giving fine-grained control and verification over the HTTP interaction itself. Alternatively, providing a fully fake service in place of the real HTTP-calling service sidesteps HTTP entirely, which is simpler when the test doesn't actually care about HTTP-specific details (headers, exact URL)."

```typescript
TestBed.configureTestingModule({ imports: [HttpClientTestingModule] });
const httpMock = TestBed.inject(HttpTestingController);

service.getUsers().subscribe(users => expect(users.length).toBe(2));

const req = httpMock.expectOne('/api/users'); // asserts the exact request was made
req.flush([{ id: 1 }, { id: 2 }]); // manually supplies the response
httpMock.verify(); // fails the test if any unexpected requests were made
```

---

### Q3. How do you test a component that depends on an Observable/async data source?

**Answer:**
"Provide a mock service returning a controllable Observable, trigger `fixture.detectChanges()`, and use `fakeAsync`/`tick()` (or `fixture.whenStable()`) to flush pending async work before asserting on the rendered DOM. The full mechanics — including why a second `detectChanges()` call after `tick()` is usually needed — are covered in [[angular-06-testing-advanced-scenarios-qa]]."

---

### Q4. Jasmine/Karma vs Jest for Angular testing — why have teams migrated?

**Answer:**
"Jasmine/Karma was the historical Angular CLI default — Karma runs tests in a real browser (or headless Chrome), which is arguably more realistic, but slower to start up and less convenient in CI/containerized environments. Jest runs in Node, with no real browser needed (a jsdom-simulated DOM instead) — noticeably faster test runs and startup, built-in code coverage and snapshot testing, and it's the same tool many teams already use for their NestJS/Node backend, letting a monorepo standardize tooling across frontend and backend. The main trade-off going to Jest is that jsdom doesn't perfectly replicate real browser behavior in every edge case — but for the vast majority of component/service tests, that gap doesn't matter in practice, and the speed/tooling benefits are usually judged worth it."

---

### Q5. What is an Angular E2E test actually driving?

**Answer:**
"A real, fully running instance of the compiled application, served in a real browser — not `TestBed`, not a simulated DOM, the actual built output making actual HTTP calls to an actual backend (or a backend faithfully stubbed at the network level). This means an E2E test exercises the entire real stack: real routing, real HTTP, real backend responses (or realistic stand-ins), real rendering — which is exactly why it gives the highest confidence of any test type, and also why it's the slowest and most expensive to run and maintain, per the Test Pyramid reasoning in [[testing-01-fundamentals-test-pyramid-qa]]."

---

### Q6. What is Snapshot Testing, and is it commonly used in Angular the way it is in React?

**Answer:**
"Snapshot testing captures a serialized representation of a rendered component (or some data structure) the first time a test runs, saves it as a reference file, and on subsequent runs compares the current output against that saved snapshot — flagging any difference for review. It's genuinely central to a lot of React testing culture (rendered component trees are a very natural fit for it), but far less commonly used in Angular. Angular's template-driven, more structurally rigid component model doesn't lend itself to snapshotting the same way, and Angular's testing culture leans more toward explicit `TestBed`-based assertions about specific behavior/DOM content rather than broad 'did anything change' snapshot diffs — which also avoids the common snapshot-testing pitfall of tests that pass by blindly re-approving a snapshot without anyone actually checking whether the change was correct."
