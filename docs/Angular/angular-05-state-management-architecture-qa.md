# Angular — State Management & Architecture — Interview Q&A

---

### Q1. Smart (container) vs Dumb (presentational) components — what's the actual architectural benefit?

**Answer:**
"A Smart component knows about services, fetches data, holds state, and contains business logic. A Dumb component only receives data via `@Input()` and communicates outward via `@Output()` — it has no idea where its data came from or what happens with its events, it just renders and emits. The benefit: dumb components become trivially reusable and easy to unit test (pure input-in, output-out, no mocking services required), while all the complexity/state gets concentrated in a small number of smart components at the top of a feature, rather than scattered through every component in the tree."

```typescript
// Smart - knows about the service, holds state
@Component({ selector: 'app-user-list-container' })
export class UserListContainerComponent {
  users$ = this.userService.getUsers();
  constructor(private userService: UserService) {}
}

// Dumb - just renders what it's given, no service knowledge at all
@Component({ selector: 'app-user-list', template: `<div *ngFor="let u of users">{{ u.name }}</div>` })
export class UserListComponent { @Input() users: User[] = []; }
```

---

### Q2. When would you reach for a full state management library (NgRx) instead of a shared service with a `BehaviorSubject`?

**Answer:**
"A service with a `BehaviorSubject` works fine for a small-to-medium app, or for state that's naturally scoped to one feature. NgRx earns its considerable boilerplate once state genuinely needs to be shared and mutated from many, many places across a large app, and you're finding it hard to answer 'what changed this value, and when' — NgRx's strict unidirectional flow (dispatch an action → reducer computes new state → selectors read it) makes every state change traceable and debuggable (especially with Redux DevTools), at the cost of real ceremony for even simple changes."

**Cross-question: What specific problems does NgRx's strict unidirectional data flow solve that a service-based approach can start to struggle with as an app grows?**
"With a plain service, any component with access to it can call any method and mutate shared state directly, from anywhere — as an app grows, it becomes hard to trace *why* a particular piece of state changed, since there's no single, structured record of 'what happened.' NgRx forces every state change to go through an explicit, named `Action`, processed by a pure `Reducer` function — so every state transition is inspectable, replayable, and testable in isolation, and time-travel debugging becomes possible because every past state is just the result of replaying a known sequence of actions."

---

### Q3. What are Angular Signals as a state primitive, compared to a service + `BehaviorSubject`?

**Answer:**
"A signal (`signal(initialValue)`) is a simpler, synchronous, more ergonomic primitive for reactive state than a `BehaviorSubject` — reading it (`mySignal()`) is a plain function call rather than needing to subscribe or use the `async` pipe, and Angular's change detection can react to signal reads with fine-grained precision (only the parts of the template that actually read that signal get checked), rather than the more coarse-grained `OnPush`/Zone.js triggers. For simple shared state in a service, a `signal` is often now a lighter-weight, easier-to-reason-about replacement for a `BehaviorSubject` — though RxJS Observables remain necessary for genuinely asynchronous streams (HTTP, WebSocket, timers) which signals alone don't model."

```typescript
@Injectable({ providedIn: 'root' })
export class CartService {
  private itemsSignal = signal<CartItem[]>([]);
  readonly items = this.itemsSignal.asReadonly(); // exposed read-only to consumers

  addItem(item: CartItem) {
    this.itemsSignal.update(current => [...current, item]);
  }
}
```

```html
<div>{{ cartService.items().length }} items</div> <!-- no async pipe needed, just call it -->
```

---

### Q4. What is the NgRx store/actions/reducers/effects/selectors model, in brief?

**Answer:**
"Actions are plain objects describing 'something happened' (e.g., `loadUsers`, `userLoaded`) — they carry a type and optional payload, but no logic. Reducers are pure functions that take the current state and an action, and return a *new* state object — this is the only place state actually changes, and it must be synchronous and side-effect-free. Effects handle side effects (API calls, routing, anything async) triggered by actions — an effect listens for an action, performs the async work, and dispatches a new action with the result. Selectors are pure functions for reading specific slices of state out of the store efficiently, with built-in memoization so components only re-render when the specific slice they selected actually changes."

```typescript
// Action
export const loadUsers = createAction('[Users] Load');
export const usersLoaded = createAction('[Users] Loaded', props<{ users: User[] }>());

// Reducer - pure, synchronous
export const userReducer = createReducer(initialState,
  on(usersLoaded, (state, { users }) => ({ ...state, users }))
);

// Effect - handles the actual async API call
loadUsers$ = createEffect(() => this.actions$.pipe(
  ofType(loadUsers),
  switchMap(() => this.api.getUsers().pipe(map(users => usersLoaded({ users }))))
));

// Selector - memoized read
export const selectAllUsers = createSelector(selectUserState, state => state.users);
```

---

### Q5. How would you structure a large Angular application to keep it maintainable at scale?

**Answer:**
"Separate by responsibility: a `Core` area for singleton, app-wide services and things loaded exactly once (auth, root-level interceptors); a `Shared` area for genuinely reusable, presentational components/pipes/directives with no feature-specific business logic, imported by many features; and one folder/module per `Feature`, each as self-contained as possible (its own routes, services, components), ideally lazy-loaded, so features don't bleed into each other's concerns. Within each feature, apply the smart/dumb component split from Q1. Keep cross-feature shared *state* (if any) in a small number of well-defined services or store slices, rather than components reaching into unrelated features directly."

```
src/app/
  core/       — singleton services, guards, interceptors (loaded once, app-wide)
  shared/     — reusable presentational components, pipes, directives (no business logic)
  features/
    orders/   — self-contained, lazy-loaded feature: its own routes, services, components
    users/    — same pattern, independent of orders/
```
