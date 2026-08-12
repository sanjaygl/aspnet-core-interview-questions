# Angular — Signals (New Reactivity Model) — Interview Q&A

---

### Q1. What is `signal()`, and how is reading/writing one different from a plain class property?

**Answer:**
"`signal(initialValue)` creates a reactive, observable container for a value. You read it by *calling* it like a function — `mySignal()` — and write to it with `.set(newValue)` or `.update(current => newValue)`. The key difference from a plain property: Angular's reactivity system tracks exactly *where* a signal is read (inside a template, a `computed()`, or an `effect()`), so when it changes, only those specific consumers are notified and re-evaluated — not a broad, app-wide change detection sweep."

```typescript
count = signal(0);

increment() {
  this.count.update(current => current + 1); // or: this.count.set(this.count() + 1);
}
```
```html
<button (click)="increment()">{{ count() }}</button> <!-- reading it in the template auto-tracks this binding -->
```

**Cross-question: Why does a template automatically re-render when a signal it reads changes, without `ChangeDetectorRef` or `OnPush` boilerplate?**
"Because Angular's template compiler generates code that registers a dependency on any signal read directly inside a template binding. When that signal's value changes, Angular knows precisely which template expressions depend on it and schedules exactly those for re-check — it's a fundamentally more granular mechanism than Zone.js's 'something async happened somewhere, check broadly' approach, so there's no need to manually call `markForCheck()` the way you would updating plain component state under `OnPush`."

---

### Q2. What is `computed()`, and how does it know which signals to recompute from?

**Answer:**
"`computed(() => expression)` creates a derived, read-only signal whose value is automatically recalculated whenever any signal it reads *inside that function* changes. You never declare its dependencies explicitly — Angular's signal system automatically tracks every signal actually read during the computation's execution, the same automatic dependency-tracking mechanism used for template bindings. It's also lazy and memoized — it doesn't recompute on every read, only when one of its actual dependencies has changed since the last read."

```typescript
firstName = signal('John');
lastName = signal('Doe');
fullName = computed(() => `${this.firstName()} ${this.lastName()}`); // dependencies inferred automatically

this.firstName.set('Jane'); // fullName automatically recomputes to "Jane Doe" next time it's read
```

---

### Q3. What is `effect()`, and how is it different from `computed()`?

**Answer:**
"`computed()` produces a *value* — it's for deriving state from other state. `effect()` runs a *side effect* in response to signal changes — it doesn't produce a value at all, it's for things like logging, writing to `localStorage`, or manually syncing with a non-signal API. Use `computed()` whenever you need a derived value you'll read somewhere; use `effect()` only for the side effect itself, never as a way to compute and store a value (that's what `computed()` is for)."

```typescript
theme = signal<'light' | 'dark'>('light');

constructor() {
  effect(() => {
    localStorage.setItem('theme', this.theme()); // pure side effect, no value being produced/read elsewhere
  });
}
```

**Cross-question: What's the risk of writing to a signal from inside an `effect()` that also reads that same signal?**
"An infinite loop — the effect reads the signal (registering as a dependency), then writes to it, which triggers the effect to re-run again, which reads it again, writes again, and so on. Angular does have some built-in protections and will throw an error for the most direct self-referential case, but the broader lesson is: `effect()` is for one-directional side effects reacting to state, not for feeding back into the same state it depends on. If you need to derive a new value from a signal, that's `computed()`; if you need to update a *different* signal in response to a change, be careful about ordering and consider whether the relationship should be a `computed()` instead."

---

### Q4. What does it mean that `effect()` runs "outside" the normal template rendering cycle?

**Answer:**
"An `effect()` isn't tied to a specific template binding the way a `computed()` used in a template is — it runs as its own independent reactive scheduling unit, triggered by the Angular reactivity scheduler whenever its dependencies change, regardless of whether the component's view is even currently being checked. This is exactly why it's the correct place for side effects like `localStorage` writes or manually imperatively updating a non-Angular library — those aren't rendering the template, so they don't belong inside a `computed()` (which should stay pure and free of side effects) or directly in the template itself."

---

### Q5. How do `input()` and `model()` compare to the classic `@Input()`/`@Output()` decorator pattern?

**Answer:**
"`input()` creates a signal-based input — reading it (`this.myInput()`) participates in the same fine-grained reactivity as any other signal, and `computed()`/`effect()` can depend on it directly, unlike a plain `@Input()` property which is just a regular class field with no reactivity of its own. `model()` goes further, creating a signal-based input that also supports two-way binding (`[(value)]`) — replacing the classic `@Input() value` + `@Output() valueChange` pair with a single declaration."

```typescript
// Classic
@Input() count = 0;
@Output() countChange = new EventEmitter<number>();

// Signal-based equivalent
count = model(0); // single declaration replaces both the @Input and @Output pair

increment() {
  this.count.set(this.count() + 1); // updates the signal AND emits the change to any [(count)] binding
}
```
```html
<app-counter [(count)]="parentCount"></app-counter> <!-- two-way binds against the model() signal -->
```

---

### Q6. What are `toSignal()` and `toObservable()`, and why would you need to convert between RxJS and Signals?

**Answer:**
"`toSignal(observable$)` converts an RxJS Observable into a signal — useful when you have an existing Observable-based data source (an `HttpClient` call, a service exposing a `BehaviorSubject`) but want to consume it with signal-based ergonomics (calling it directly, letting `computed()`/`effect()` depend on it) rather than piping through the `async` pipe or manually subscribing. `toObservable(signal)` does the reverse — wraps a signal as an Observable, useful when you need to feed a signal's value into an existing RxJS operator chain (`debounceTime`, `switchMap`, etc.) that signals don't natively support."

```typescript
users = toSignal(this.userService.getUsers(), { initialValue: [] }); // Observable -> Signal

searchTerm = signal('');
searchTerm$ = toObservable(this.searchTerm).pipe(debounceTime(300), switchMap(term => this.api.search(term))); // Signal -> Observable, to use RxJS operators
```

**Cross-question: What happens to an Observable's multiple-values-over-time nature when it's converted to a signal — does anything get lost?**
"A signal only ever holds its *latest* value — `toSignal()` effectively behaves like a live snapshot, always reflecting the most recent emission, but it has no memory of the emission history, no concept of 'error' or 'complete' the way an Observable subscription does (errors need to be handled separately, e.g., via `catchError` before conversion), and nothing to unsubscribe from at the call site since the signal handles that internally. For a stream where every individual emission matters (not just the latest), or where you need to react to completion/errors as first-class events, staying with the Observable directly is still the right tool."

---

### Q7. What is `linkedSignal()`, and what problem does it solve?

**Answer:**
"`linkedSignal()` creates a writable signal whose default value is *derived* from another signal (like `computed()`), but — unlike `computed()` — it can also be manually overwritten afterward, and it automatically resets back to the derived value whenever its source signal changes. This solves the common 'resettable derived state' problem: e.g., a selected item that should default to the first item in a list, and reset to that default whenever the list itself changes, but which the user can also manually override in the meantime."

```typescript
items = signal<Item[]>([]);
selectedItem = linkedSignal(() => this.items()[0]); // defaults to first item, resets when items() changes

selectItem(item: Item) {
  this.selectedItem.set(item); // manual override — a plain computed() couldn't do this at all
}
```

**Where this comes up as a trick question:** "why not just use `computed()` for this?" — because `computed()` is strictly read-only and always recalculates purely from its dependencies; it has no way to be manually set to something else. `linkedSignal()` is specifically for state that's *usually* derived but sometimes needs a manual override.

---

### Q8. Are Signals meant to replace RxJS in Angular entirely?

**Answer:**
"No — they solve different problems. Signals model synchronous, glitch-free *state* — 'what is the current value of this thing right now,' with fine-grained change notification. RxJS models asynchronous *streams over time* — sequences of events, cancellation, complex temporal composition (`debounceTime`, `switchMap`, combining multiple async sources) — none of which signals attempt to replace. In practice: use Signals for component/UI state and derived values; keep RxJS for genuinely asynchronous operations (HTTP calls, WebSockets, complex event stream composition), and use `toSignal()`/`toObservable()` at the boundary where the two need to meet — e.g., converting an HTTP Observable to a signal once it's fetched, so the rest of the component's state model is signal-based."

**Where to use:** default new component-local state to Signals for simplicity; reach for RxJS specifically when you need its operator library or are dealing with genuinely asynchronous, multi-step, or cancellable streams.
