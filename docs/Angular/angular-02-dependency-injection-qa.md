# Angular — Dependency Injection — Interview Q&A

---

### Q1. How does Angular's hierarchical DI work?

**Answer:**
"Angular builds a tree of injectors that mirrors the component tree — each component (and each module) can have its own injector. When a component asks for a dependency, Angular walks *up* the injector tree, starting from the component's own injector, checking each ancestor in turn, until it finds a provider for that token — the root injector is checked last. That means a service provided lower in the tree (e.g., in a specific component) is only visible to that component and its descendants, while a service provided at the root is visible app-wide."

```
RootInjector (providedIn: 'root')
  └── AppComponent's injector
        └── FeatureComponent's injector (its own `providers: [FeatureService]`)
              └── ChildComponent's injector — resolves FeatureService from its parent, not root
```

---

### Q2. What's the difference between `providedIn: 'root'`, providing in an `NgModule`, and providing in a component's `providers` array?

**Answer:**
"`providedIn: 'root'` (set directly on the `@Injectable()` decorator) registers the service with the app's root injector and makes it tree-shakable — if nothing ever injects it, it's not included in the final bundle at all. Providing a service in an `NgModule`'s `providers` array registers it with that module's injector — with eagerly-loaded modules this is effectively also app-wide (and NOT tree-shakable, since the module itself references it directly), but with a lazy-loaded module it creates a separate instance scoped to that lazy module. Providing in a component's own `providers` array creates a *new instance* of that service for every instance of that component, and that instance is only visible to the component and its children — never siblings or ancestors."

```typescript
@Injectable({ providedIn: 'root' }) // one shared instance, app-wide, tree-shakable
export class AuthService {}

@Component({ providers: [CartService] }) // a NEW CartService instance, scoped to this component + its children
export class CheckoutComponent {}
```

**Cross-question: If a service is provided both at the root level and again in a component's `providers` array, which instance does that component's children get?**
"The component-level one — Angular resolves from the nearest injector upward, so the component's own `providers` array shadows the root registration for that component and everything below it. Anything *outside* that component's subtree still gets the root instance. This is a deliberate mechanism for scoping — e.g., giving each instance of a reusable `<app-wizard>` component its own isolated `WizardStateService`, while the rest of the app shares one root instance of something else with the same token."

---

### Q3. What are the different provider types (`useClass`, `useValue`, `useFactory`, `useExisting`)?

**Answer:**
"`useClass` tells Angular which class to instantiate when a token is requested — useful for swapping an implementation (e.g., a mock in tests) without changing every place that injects the interface/token. `useValue` provides a pre-built, static value directly — no instantiation, just hands back exactly what you gave it (good for config objects, constants). `useFactory` calls a function to produce the value, letting you build it dynamically, possibly using other injected dependencies. `useExisting` aliases one token to resolve to whatever another token already resolves to — the same instance, just accessible under two names."

```typescript
// useClass - swap implementation
{ provide: LoggerService, useClass: ConsoleLoggerService }

// useValue - static config
{ provide: API_URL, useValue: 'https://api.example.com' }

// useFactory - built dynamically, can depend on other injected services
{ provide: FeatureFlagService, useFactory: (http: HttpClient) => new FeatureFlagService(http), deps: [HttpClient] }

// useExisting - alias, same instance under a second token
{ provide: OldLoggerToken, useExisting: LoggerService }
```

---

### Q4. What is an `InjectionToken`, and why can't you just use a TypeScript interface as a DI token?

**Answer:**
"TypeScript interfaces are a compile-time-only construct — they're completely erased from the compiled JavaScript, so there's nothing left at runtime for Angular's DI system to actually key a lookup on. `InjectionToken` creates a real, unique runtime object (with an optional type parameter for compile-time type safety) that can be used to identify a dependency — most commonly for configuration values, or plain objects/interfaces that don't have a class to key off of."

```typescript
export interface AppConfig { apiUrl: string; }
export const APP_CONFIG = new InjectionToken<AppConfig>('APP_CONFIG');

{ provide: APP_CONFIG, useValue: { apiUrl: 'https://api.example.com' } }

constructor(@Inject(APP_CONFIG) private config: AppConfig) {} // works — APP_CONFIG exists at runtime
```

---

### Q5. What's the difference between `providers` and `viewProviders` on a component?

**Answer:**
"`providers` makes a service available to the component itself, its content-projected children (`<ng-content>`), and its view children. `viewProviders` restricts it to only the component's own *view* children — explicitly excluding anything projected into it via `<ng-content>`. This distinction matters for component libraries that want to isolate a service from whatever arbitrary content a consumer projects into the component, keeping that internal service private to the component's own template."

---

### Q6. What is a multi-provider (`multi: true`), and what's a real built-in Angular feature that uses one?

**Answer:**
"A multi-provider lets multiple values be registered against the *same* token, and Angular resolves an array of all of them instead of overwriting the previous registration. `HTTP_INTERCEPTORS` is the canonical built-in example — every app can register several interceptors (auth, logging, error handling), each added via `{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }`, and Angular runs them all in the order they were provided, chained together, instead of the last one overwriting the rest."

```typescript
{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
{ provide: HTTP_INTERCEPTORS, useClass: LoggingInterceptor, multi: true },
// both run, chained, for every HTTP request — neither overwrites the other
```

---

### Q7. How would you resolve a circular dependency between two injectable services?

**Answer:**
"First, seriously consider whether the circular dependency is a design smell — often it means two services should be merged, or a piece of shared logic should be extracted into a third service both can depend on one-directionally. If it's genuinely unavoidable (rare, but happens with some cross-cutting concerns), Angular's `forwardRef()` lets you reference a class before it's been defined, breaking the immediate declaration-order problem — though the runtime circular dependency between the two service instances still needs careful handling (e.g., one side using a setter-based injection or lazily resolving the other via `Injector.get()` instead of constructor injection)."

```typescript
constructor(@Inject(forwardRef(() => ServiceB)) private serviceB: ServiceB) {}
```

**Where to use:** as a last resort — `forwardRef` solves the TypeScript/decorator declaration-order problem, but a true circular *runtime* dependency between two services is usually a sign to refactor the responsibility boundary between them.
