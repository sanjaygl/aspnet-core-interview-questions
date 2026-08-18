# Angular State Management — Interview Questions & Answers

## 1. What is State Management in Angular?

State management is the process of storing, updating, sharing, and reacting to application data across components and services.

Examples include logged-in user, authentication status, shopping cart, notifications, application settings, and loading/error state.

---

## 2. What is a Signal in Angular?

A Signal is an Angular reactive primitive used to hold a value and notify consumers when that value changes.

```typescript
import { signal } from '@angular/core';

count = signal(0);

this.count.set(10);

this.count.update(value => value + 1);
```

Read it with:

```typescript
console.log(this.count());
```

> **Signal = reactive value/state**

---

## 3. What is an Observable?

An Observable is an RxJS type representing a stream of values that can arrive over time.

```typescript
users$ = this.http.get<User[]>('/api/users');
```

Common Angular examples include HTTP responses, WebSocket messages, route parameters, form value changes, and user events.

> **Observable = stream of values over time**

---

## 4. What is an Observer?

An Observer consumes values emitted by an Observable.

```typescript
observable.subscribe({
    next: value => console.log(value),
    error: error => console.error(error),
    complete: () => console.log('Completed')
});
```

An Observer handles `next`, `error`, and `complete`.

> **Observable produces → Observer consumes**

---

## 5. What is a Subject?

A Subject is both an Observable and an Observer. It can receive values using `next()` and broadcast them to multiple subscribers.

```typescript
const subject = new Subject<number>();

subject.subscribe(value => console.log('Subscriber 1:', value));
subject.subscribe(value => console.log('Subscriber 2:', value));

subject.next(10);
```

> **Subject = Observable + Observer**

---

## 6. What is a BehaviorSubject?

A BehaviorSubject is a Subject that requires an initial value, stores the latest value, and immediately gives that latest value to a new subscriber.

```typescript
private userSubject =
    new BehaviorSubject<User | null>(null);

user$ = this.userSubject.asObservable();

this.userSubject.next(currentUser);
```

> **BehaviorSubject = Subject + current/latest value**

---

## 7. What is a Promise?

A Promise represents the eventual result of a single asynchronous operation.

```typescript
fetch('/api/users')
    .then(response => response.json())
    .then(users => console.log(users));
```

> **Promise = one future result**

An Observable can emit multiple values over time.

---

## 8. Observable vs Promise

| Observable | Promise |
|---|---|
| Can emit multiple values | Normally produces one result |
| Lazy by default | Starts when created |
| Supports RxJS operators | Uses `then`, `catch`, `finally` |
| Represents streams | Represents one eventual result |
| Common for HTTP, events, WebSockets | Common for one-time async results |

> Use an Observable for reactive streams and RxJS pipelines; use a Promise for a single eventual result.

---

## 9. Observable vs Signal

| Signal | Observable |
|---|---|
| Angular reactive primitive | RxJS reactive primitive |
| Excellent for reactive state | Excellent for async streams/events |
| Read with `signal()` | Consume with `subscribe()` or `async` pipe |
| Uses `computed()` and `effect()` | Uses RxJS operators |
| Synchronous value access | Values are emitted over time |

> Signals and Observables solve related but different problems. Signals are especially useful for reactive state, while Observables are powerful for asynchronous streams and event-based workflows.

---

## 10. BehaviorSubject vs Signal

### BehaviorSubject

```typescript
private countSubject =
    new BehaviorSubject<number>(0);

count$ = this.countSubject.asObservable();

this.countSubject.next(10);
```

### Signal

```typescript
count = signal(0);

this.count.set(10);
```

| BehaviorSubject | Signal |
|---|---|
| RxJS | Angular |
| Stream-based | Value/state-based |
| Subscribe to receive values | Read using `count()` |
| Useful with RxJS pipelines | Excellent for reactive state |

> Use a Signal when you primarily need reactive state. Use a BehaviorSubject when the state naturally needs to participate in an RxJS stream/pipeline.

---

## 11. Subject vs BehaviorSubject

A Subject does not store a current value.

```typescript
const subject = new Subject<number>();

subject.next(10);

subject.subscribe(value => console.log(value));
```

The new subscriber does not receive the previous `10`.

A BehaviorSubject stores the latest value:

```typescript
const subject = new BehaviorSubject<number>(0);

subject.next(10);

subject.subscribe(value => console.log(value));
```

The new subscriber immediately receives `10`.

> **Subject → future emissions. BehaviorSubject → latest/current value.**

---

## 12. What is ReplaySubject?

ReplaySubject stores previous emitted values and replays them to new subscribers.

```typescript
const subject = new ReplaySubject<number>(2);

subject.next(10);
subject.next(20);
subject.next(30);

subject.subscribe(value => console.log(value));
```

The subscriber receives:

```text
20
30
```

Comparison:

```text
Subject
→ No previous values

BehaviorSubject
→ Latest value

ReplaySubject
→ Configurable number/time of previous values
```

---

## 13. What is `computed()` in Angular Signals?

`computed()` creates derived read-only state from other Signals.

```typescript
firstName = signal('Sanjay');
lastName = signal('Bopche');

fullName = computed(() =>
    `${this.firstName()} ${this.lastName()}`
);
```

> **`computed()` → derived state**

---

## 14. What is `effect()` in Angular Signals?

`effect()` runs side-effect logic when Signals read inside it change.

```typescript
effect(() => {
    console.log('Count:', this.count());
});
```

Good use cases include logging, synchronizing with browser APIs, and integrating with non-reactive APIs.

Do not use `effect()` simply to derive state. Use `computed()` for derived state.

> **`computed()` → derived state. `effect()` → side effects.**

---

## 15. When should you use Signals vs RxJS?

### Use Signals for:

- Component/local state
- Simple reactive application state
- Derived state
- Reactive UI state

```typescript
isMenuOpen = signal(false);

toggleMenu() {
    this.isMenuOpen.update(value => !value);
}
```

### Use RxJS for:

- HTTP streams
- WebSockets
- User event streams
- Complex asynchronous workflows
- Combining asynchronous sources
- Operators such as `switchMap`, `mergeMap`, `debounceTime`

```typescript
searchResults$ = this.searchControl.valueChanges.pipe(
    debounceTime(300),
    distinctUntilChanged(),
    switchMap(term => this.api.search(term))
);
```

> Signals are excellent for reactive state, while RxJS is excellent for asynchronous streams and complex event/data flows. They can also be used together.

---

## 16. What is NgRx?

NgRx is an Angular state-management library built around RxJS and the Redux pattern.

The basic flow is:

```text
Component
    ↓
 Action
    ↓
 Reducer
    ↓
  Store
    ↓
  State
    ↓
 Selector
    ↓
Component
```

NgRx also provides Effects for side effects such as API calls.

Official documentation: https://ngrx.io/

---

## 17. What is the NgRx Store?

The Store is the centralized state container used by NgRx.

```typescript
store.dispatch(
    loginSuccess({ user })
);
```

Read state using a selector:

```typescript
user$ = this.store.select(selectUser);
```

> **Store → centralized application state**

---

## 18. What is an Action in NgRx?

An Action describes something that happened.

```typescript
export const loginSuccess = createAction(
    '[Auth] Login Success',
    props<{ user: User }>()
);
```

Dispatch:

```typescript
this.store.dispatch(
    loginSuccess({ user })
);
```

> **Action = describes what happened**

Examples include `Login Success`, `Load Users`, `Add Product`, and `Logout`.

---

## 19. What is a Reducer in NgRx?

A Reducer determines how application state changes in response to Actions.

```typescript
export const authReducer = createReducer(
    initialState,

    on(loginSuccess, (state, { user }) => ({
        ...state,
        user,
        isAuthenticated: true
    }))
);
```

Conceptually:

```text
Action + Current State
        ↓
      Reducer
        ↓
    New State
```

Reducers should be predictable and free of side effects.

---

## 20. What is a Selector in NgRx?

A Selector reads or derives data from the Store.

```typescript
export const selectUser =
    createSelector(
        selectAuthState,
        state => state.user
    );
```

Component:

```typescript
user$ = this.store.select(selectUser);
```

> **Store → Selector → Component**

---

## 21. What is an Effect in NgRx?

An Effect handles side effects triggered by Actions.

Typical examples:

- API calls
- HTTP requests
- Navigation
- External service calls

```typescript
loadUsers$ = createEffect(() =>
    this.actions$.pipe(
        ofType(loadUsers),
        switchMap(() =>
            this.api.getUsers().pipe(
                map(users => loadUsersSuccess({ users })),
                catchError(error =>
                    of(loadUsersFailure({ error }))
                )
            )
        )
    )
);
```

Flow:

```text
Action
  ↓
Effect
  ↓
API
  ↓
Success/Failure Action
  ↓
Reducer
  ↓
Store
```

> **Effect = handles side effects**

---

## 22. Signals vs BehaviorSubject vs NgRx

| | Signal | BehaviorSubject | NgRx |
|---|---|---|---|
| Primary purpose | Reactive state | RxJS state/stream | Complex application state |
| Technology | Angular | RxJS | NgRx + RxJS |
| Complexity | Low | Low/Medium | Medium/High |
| Current value | Yes | Yes | Store state |
| Derived state | `computed()` | RxJS operators | Selectors |
| Side effects | `effect()` | RxJS/services | Effects |
| Best suited for | Local/shared reactive state | RxJS-based state | Large/complex shared state |

Practical guideline:

```text
Simple local state
        ↓
     Signal

Shared state with RxJS requirements
        ↓
 BehaviorSubject / Observable

Large and complex application-wide state
        ↓
      NgRx
```

This is a guideline, not a strict rule. Signals can also manage shared application state, and NgRx provides modern signal-based APIs.

---

## 23. What is the difference between local state and global/shared state?

### Local State

State used by a single component or small part of the UI.

```typescript
isMenuOpen = signal(false);
```

There is usually no reason to put simple UI state into NgRx.

### Shared State

State required by multiple unrelated components.

Examples:

```text
Authentication
Shopping Cart
Current User
Application Preferences
```

Depending on complexity, shared state can use:

```text
Signal
BehaviorSubject
NgRx
```

---

## 24. Should every Angular application use NgRx?

No.

NgRx introduces structure and complexity, so it should be used when the application benefits from centralized state management.

For simple applications:

```text
Component
   ↓
Signal
```

or:

```text
Component
   ↓
Service
   ↓
BehaviorSubject / Signal
```

may be sufficient.

For large applications with complex shared state, many state transitions, side effects, and multiple consumers, NgRx can provide better predictability and maintainability.

> **Do not introduce NgRx just because an application has state. Choose it when centralized, predictable state management provides enough value to justify its complexity.**

---

## 25. How would you choose between Signal, BehaviorSubject, and NgRx?

A practical decision:

```text
Is this simple component/local state?
        │
       Yes
        ↓
     Signal

        No
        │
        ↓
Does the state need RxJS streams/operators?
        │
       Yes
        ↓
 BehaviorSubject / Observable

        No
        │
        ↓
Is the application state complex and shared
across many parts of the application?
        │
       Yes
        ↓
      NgRx
```

### Interview-ready answer

> "For simple reactive UI or component state, I would prefer Signals. If the state is naturally part of an RxJS stream or requires RxJS operators, I may use an Observable or BehaviorSubject. For large applications with complex shared state, predictable state transitions, and significant side effects, I would consider NgRx."

---

# Quick Interview Cheat Sheet

```text
Signal
→ Angular reactive state primitive

Observable
→ Stream of values over time

Observer
→ Consumes Observable emissions

Subject
→ Observable + Observer

BehaviorSubject
→ Subject + latest/current value

ReplaySubject
→ Replays previous values

Promise
→ Single eventual asynchronous result

computed()
→ Derived Signal state

effect()
→ Side effects based on Signals

NgRx Store
→ Centralized application state

Action
→ Describes what happened

Reducer
→ Calculates new state

Selector
→ Reads/derives state

Effect
→ Handles side effects
```

# Most Important Interview Comparisons

```text
Signal vs Observable
Signal vs BehaviorSubject
BehaviorSubject vs Subject
Subject vs ReplaySubject
Observable vs Promise
Signals vs RxJS
Signals vs NgRx
BehaviorSubject vs NgRx
Local State vs Global State
When should you use NgRx?
computed() vs effect()
```
