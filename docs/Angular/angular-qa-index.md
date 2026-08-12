# Angular — Senior-Level Interview Questions — Index

Same approach as the LINQ/EF Core series — skips "what is a component" trivia, focuses on what actually gets probed at senior level (change detection internals, RxJS operator differences, DI edge cases, and hands-on coding asks). Grouped into 8 files.

---

## File 1 — `angular-01-fundamentals-change-detection-qa.md`
**Fundamentals (senior angle) + Directives + Compiler + Change Detection & Performance**
1. What's the actual difference between an NgModule-based app and a standalone-components app, and why did Angular move that direction?
2. What are the three types of Angular directives (Component, Attribute, Structural), and how is a Component "just" a directive with a template under the hood?
3. What's the difference between the classic `*ngIf`/`*ngFor`/`*ngSwitch` structural directives and the newer built-in control flow syntax (`@if`/`@for`/`@switch`)?
   - *Cross-question:* Why is the new `@for` block able to require a `track` expression outright, where `*ngFor`'s `trackBy` was easy to forget?
4. What is a Host Directive, and what problem does it solve that used to require inheritance or a wrapper component?
5. What is the Ivy compiler, and what changed compared to the older View Engine?
6. AOT (Ahead-of-Time) vs JIT (Just-in-Time) compilation — what's the practical difference in a production build?
7. What is Zone.js, and what problem does it solve for Angular's change detection?
   - *Cross-question:* What breaks if you run code with `NgZone.runOutsideAngular()` and then try to update a bound template property inside it?
8. `ChangeDetectionStrategy.Default` vs `OnPush` — what actually triggers change detection under each?
   - *Cross-question:* If a component uses `OnPush` and you mutate an array property in place (`.push()`) instead of reassigning it, will the view update?
9. What is Zoneless Change Detection (Angular 18+), and how does it relate to Signals replacing the need for Zone.js? *(deep dive on Signals themselves is File 8)*
10. What are `ChangeDetectorRef.markForCheck()`, `detectChanges()`, and `detach()`, and when would you reach for each?
11. What is `trackBy` in `*ngFor`, and what breaks (performance-wise) if you omit it on a large list?
12. Pure vs Impure pipes — what's the performance implication of marking a pipe impure?
13. What is the full Angular component lifecycle hook order (`ngOnChanges` → `ngOnInit` → `ngDoCheck` → `ngAfterContentInit` → `ngAfterContentChecked` → `ngAfterViewInit` → `ngAfterViewChecked` → `ngOnDestroy`), and which two are the most commonly misused?
    - *Cross-question:* Why is `@Input()` not yet guaranteed to be set inside a component's constructor?
    - *Cross-question:* What is the `SimpleChanges` object passed into `ngOnChanges`, and what would you actually use `ngDoCheck` for that `ngOnChanges` can't tell you?

## File 2 — `angular-02-dependency-injection-qa.md`
**Dependency Injection**
1. How does Angular's hierarchical DI work — what does "hierarchical" actually mean here?
2. What's the difference between `providedIn: 'root'`, providing a service in a specific `NgModule`, and providing it in a component's `providers` array?
   - *Cross-question:* If a service is provided both at the root level and again in a component's `providers` array, which instance does that component's children get?
3. What are the different provider types (`useClass`, `useValue`, `useFactory`, `useExisting`), and when would you reach for each?
4. What is an `InjectionToken`, and why can't you just use a TypeScript interface as a DI token?
5. What's the difference between `providers` and `viewProviders` on a component?
6. What is a multi-provider (`multi: true`), and what's a real built-in Angular feature that uses one (hint: `HTTP_INTERCEPTORS`)?
7. How would you resolve a circular dependency between two injectable services?

## File 3 — `angular-03-rxjs-reactive-patterns-qa.md`
**RxJS & Reactive Patterns**
1. Observable vs Promise — what are the real, practical differences (cancellation, multiple values, laziness)?
2. What's the difference between an Observable, an Observer, and a Subscription — three terms people often blur together?
   - *Cross-question:* Is a `Subject` an Observable, an Observer, or both — and what does that dual nature actually let it do?
3. What does "multicast" vs "unicast" mean for an Observable, and which category does a plain `Observable.create`/`new Observable()` fall into by default?
4. `Subject` vs `BehaviorSubject` vs `ReplaySubject` vs `AsyncSubject` — what's actually different about each?
5. `switchMap` vs `mergeMap` vs `concatMap` vs `exhaustMap` — the classic RxJS interview question. What's a realistic use case for each?
   - *Cross-question:* Which of these four would you use for a type-ahead search box, and why specifically that one?
   - *Cross-question:* Which one would you use for a "submit" button click, to guarantee a second click can't fire a duplicate request while the first is still in flight?
6. How do you prevent memory leaks from subscriptions in Angular components?
   - *Cross-question:* Why does the `async` pipe in a template avoid this problem automatically?
7. What is the difference between a "cold" and a "hot" Observable?
8. What does `takeUntil` do, and why is it a common pattern paired with a `Subject` for unsubscribing?
9. What's the difference between `combineLatest`, `forkJoin`, and `zip`?

## File 4 — `angular-04-routing-forms-qa.md`
**Routing & Forms**
1. What are Route Guards (`CanActivate`, `CanDeactivate`, `Resolve`, `CanMatch`), and what's a realistic use case for each?
   - *Cross-question:* What's the difference between blocking navigation with a guard vs just checking auth inside the component itself?
2. How does lazy loading work for feature modules/routes, and what's the actual performance benefit?
3. Reactive Forms vs Template-Driven Forms — what are the real trade-offs, and which does a senior dev default to for a complex form, and why?
4. How do you write a custom validator, and what's the difference between a sync and an async validator?
5. What is `ControlValueAccessor`, and when do you need to implement it?
   - *Cross-question:* Why doesn't `[(ngModel)]` or `formControlName` work on a custom component out of the box without it?
6. How do you handle cross-field validation (e.g., "confirm password must match password") in Reactive Forms?

## File 5 — `angular-05-state-management-architecture-qa.md`
**State Management & Architecture**
1. Smart (container) vs Dumb (presentational) components — what's the actual architectural benefit of this split?
2. When would you reach for a full state management library (NgRx) instead of a shared service with a `BehaviorSubject`?
   - *Cross-question:* What specific problems does NgRx's strict unidirectional data flow solve that a service-based approach can start to struggle with as an app grows?
3. What are Angular Signals as a state primitive, and how do they compare to using a service + `BehaviorSubject` for shared state?
4. What is the NgRx store/actions/reducers/effects/selectors model, in brief — what's each piece responsible for?
5. How would you structure a large Angular application (feature modules, shared module, core module) to keep it maintainable at scale?

## File 6 — `angular-06-testing-advanced-scenarios-qa.md`
**Testing & Advanced/Scenario-Based**
1. What is `TestBed`, and how does it differ from just instantiating a component class directly in a unit test?
2. How would you mock an injected service dependency in a component test?
3. How do you test a component that depends on an Observable/async data source?
4. How would you diagnose a performance problem (e.g., a laggy UI) in a production Angular app?
5. What's involved in upgrading a large Angular application across major versions, and what's usually the riskiest part?
6. What is Angular Universal (SSR), and what problem does it solve that a pure client-side SPA can't?
7. How would you approach breaking up a large Angular monolith into micro-frontends?

## File 7 — `angular-07-coding-practice-qa.md`
**Coding Practice (interviewers frequently ask you to actually write these)**
1. Write a custom pipe (e.g., a `truncate` pipe).
2. Write a custom attribute directive (e.g., a `highlight` directive reacting to hover).
3. Write a custom structural directive (like a simplified version of `*ngIf`).
4. Implement a debounced type-ahead search using RxJS operators (the classic `switchMap` + `debounceTime` + `distinctUntilChanged` combo).
5. Implement `ControlValueAccessor` for a custom form control (e.g., a star-rating component usable with `formControlName`).
6. Write a custom Reactive Forms validator (sync) and a custom async validator (e.g., checking username availability against an API).
7. Write an `HttpInterceptor` (e.g., attaching an auth token to every outgoing request, and handling a 401 globally).
8. Write a custom RxJS operator using `pipe()` composition.

## File 8 — `angular-08-signals-qa.md`
**Signals — Angular's New Reactivity Model (new)**
1. What is `signal()`, and how is reading/writing one different from a plain class property?
   - *Cross-question:* Why does a template automatically re-render when a signal it reads changes, without `ChangeDetectorRef` or `OnPush` boilerplate?
2. What is `computed()`, and how does it know which signals to recompute from — do you have to declare its dependencies explicitly?
3. What is `effect()`, and how is it different from a `computed()` — specifically, when would you use one over the other?
   - *Cross-question:* What's the risk of writing to a signal from inside an `effect()` that also reads that same signal?
4. What does it mean that `effect()` runs "outside" the normal template rendering cycle, and why does that matter for side effects like logging or `localStorage` writes?
5. How do `input()` and `model()` (signal-based inputs and two-way-bindable inputs) compare to the classic `@Input()`/`@Output()` decorator pattern?
6. What are `toSignal()` and `toObservable()`, and why would you need to convert between RxJS and Signals at all in a real app?
   - *Cross-question:* What happens to an Observable's multiple-values-over-time nature when it's converted to a signal — does anything get lost?
7. What is `linkedSignal()`, and what problem does it solve that a plain `computed()` can't (hint: resettable derived state)?
8. Are Signals meant to replace RxJS in Angular entirely, or do they solve a different problem — how would you decide which one to reach for in a new feature?
