# Angular — Fundamentals + Directives + Compiler + Change Detection & Performance — Interview Q&A

---

### Q1. What's the actual difference between an NgModule-based app and a standalone-components app?

**Answer:**
"NgModules group components/directives/pipes together and declare what they need via `imports`/`declarations`/`providers` — every component has to belong to exactly one module. Standalone components (default since Angular 17+, available since 14) skip that entirely — a component declares its own dependencies directly via an `imports` array on the `@Component` decorator itself, no `NgModule` wrapper required at all. Angular moved this direction to reduce boilerplate (no more `SharedModule`/`FeatureModule` ceremony for simple cases) and make the dependency graph more explicit and local to each component, which also improves tree-shaking and lazy-loading granularity."

```typescript
// NgModule-based
@NgModule({ declarations: [UserCardComponent], imports: [CommonModule] })
export class UserModule {}

// Standalone
@Component({
  selector: 'app-user-card',
  standalone: true,
  imports: [CommonModule, RouterLink], // dependencies declared right here, no module needed
  template: `...`
})
export class UserCardComponent {}
```

---

### Q2. What are the three types of Angular directives, and how is a Component "just" a directive with a template?

**Answer:**
"Component directives have a template and are the most commonly used — most of what you write day to day. Attribute directives change the appearance/behavior of an existing element (`ngClass`, `ngStyle`, a custom `appHighlight`). Structural directives add/remove elements from the DOM entirely (`*ngIf`, `*ngFor`). Under the hood, a `@Component` is implemented as a directive that also happens to carry template/view metadata — the `@Directive` decorator is the base building block, and `@Component` extends that concept with a `template`/`templateUrl`."

---

### Q3. What's the difference between the classic structural directives (`*ngIf`/`*ngFor`/`*ngSwitch`) and the newer built-in control flow syntax (`@if`/`@for`/`@switch`)?

**Answer:**
"The classic ones are actual structural directives, desugared by the compiler from the `*` shorthand into `<ng-template>` bindings. The new `@if`/`@for`/`@switch` control flow (stable since Angular 17) is built directly into the template compiler as native syntax — not a directive at all — which lets Angular generate more efficient instructions, ship less runtime code (no need to import `CommonModule` just for `*ngIf`), and enforce good practices at compile time. For example, `@for` requires a `track` expression by syntax, where `*ngFor`'s `trackBy` was just an easily-forgotten optional input."

```html
<!-- Classic -->
<div *ngIf="isVisible">Content</div>
<div *ngFor="let item of items; trackBy: trackByFn">{{ item.name }}</div>

<!-- New control flow -->
@if (isVisible) {
  <div>Content</div>
}
@for (item of items; track item.id) {
  <div>{{ item.name }}</div>
}
```

**Cross-question: Why is the new `@for` block able to require a `track` expression outright, where `*ngFor`'s `trackBy` was easy to forget?**
"Because it's compiler-enforced syntax, not an optional directive input — `@for` simply won't compile without a `track` expression. `*ngFor`'s `trackBy` was just another optional binding on a directive, easy to skip entirely, which is exactly how large lists ended up silently re-rendering every item on every change detection cycle in a lot of real codebases."

---

### Q4. What is a Host Directive, and what problem does it solve?

**Answer:**
"A Host Directive lets a component or directive apply another directive's behavior to itself, composing behavior without inheritance or a wrapper component. Before this existed, sharing behavior (like a common set of ARIA attributes, or a `CdkMenuTrigger`-style behavior) across multiple unrelated components meant either duplicating logic, using inheritance (fragile, single-parent-only), or wrapping every consumer in an extra host element."

```typescript
@Directive({ selector: '[appTooltip]', standalone: true })
export class TooltipDirective { @Input() tooltipText = ''; }

@Component({
  selector: 'app-button',
  standalone: true,
  hostDirectives: [{ directive: TooltipDirective, inputs: ['tooltipText'] }]
})
export class ButtonComponent {} // ButtonComponent now has tooltip behavior, without inheriting from it
```

---

### Q5. What is the Ivy compiler, and what changed compared to the older View Engine?

**Answer:**
"Ivy is Angular's rendering/compilation pipeline (default since Angular 9), replacing the older View Engine. The big practical differences: much smaller bundle sizes via better tree-shaking (unused component/directive code is more aggressively eliminated), locality — each component compiles somewhat independently instead of needing whole-module context, which speeds up incremental builds, and improved debugging (Ivy's generated code and error messages are considerably more readable/traceable than View Engine's)."

---

### Q6. AOT vs JIT compilation — what's the practical difference?

**Answer:**
"JIT (Just-in-Time) compiles templates in the browser, at runtime, on every page load — used mainly during local development for fast rebuild cycles. AOT (Ahead-of-Time) compiles templates during the build step, before shipping to the browser — used for production builds. AOT means a smaller bundle (no compiler shipped to the browser at all), faster initial rendering (no compile step at runtime), and template errors caught at build time instead of surfacing as runtime errors for users."

```
ng build                    # AOT by default for production builds
ng serve                    # JIT-style fast rebuilds for local dev
```

---

### Q7. What is Zone.js, and what problem does it solve for Angular's change detection?

**Answer:**
"Zone.js monkey-patches async browser APIs (`setTimeout`, `Promise`, DOM events, `XMLHttpRequest`, etc.) so Angular gets notified whenever any of them complete — that's the actual trigger for Angular running change detection. Without Zone.js, Angular would have no automatic way to know 'something async just finished, maybe the UI needs updating' — you'd have to manually tell Angular to check for changes after every async operation."

**Cross-question: What breaks if you run code with `NgZone.runOutsideAngular()` and then try to update a bound template property inside it?**
"The property does get updated in memory, but Angular's change detection never runs for it automatically, because Zone.js isn't tracking that code as being 'inside the Angular zone' — the template silently doesn't reflect the new value until something else happens to trigger change detection anyway. This is intentional — `runOutsideAngular()` exists specifically to skip triggering change detection for noisy async work (e.g., frequent mouse-move handling, polling) that doesn't need to update the UI on every tick — you'd manually call `ngZone.run(() => ...)` around just the part that actually needs to update the view."

```typescript
this.ngZone.runOutsideAngular(() => {
  setInterval(() => {
    this.counter++; // updates in memory, but the template WON'T reflect it automatically
  }, 100);
});
```

---

### Q8. `ChangeDetectionStrategy.Default` vs `OnPush` — what actually triggers change detection under each?

**Answer:**
"`Default` means Angular checks this component (and its whole subtree) on every change detection cycle, triggered by essentially any async event Zone.js notices anywhere in the app — safe, but potentially wasteful for components that rarely actually change. `OnPush` restricts checking to specific triggers only: an `@Input()` reference changes, an event originates from within the component's own template, an `Observable` bound via the `async` pipe emits, or `ChangeDetectorRef.markForCheck()`/`detectChanges()` is called manually. It does NOT re-check just because *some other, unrelated* part of the app changed."

```typescript
@Component({ selector: 'app-user-card', changeDetection: ChangeDetectionStrategy.OnPush, template: `...` })
export class UserCardComponent { @Input() user!: User; }
```

**Cross-question: If a component uses `OnPush` and you mutate an array property in place (`.push()`) instead of reassigning it, will the view update?**
"No — and this is the single most common `OnPush` bug. `OnPush` checks whether the `@Input()` *reference* changed, not whether its contents changed. `array.push(x)` mutates the same array object in place — the reference passed into the child component's `@Input()` never changes, so `OnPush` sees nothing to react to, and the view silently doesn't update. The fix is to always replace the reference: `this.items = [...this.items, newItem]` instead of `this.items.push(newItem)`."

```typescript
// BAD under OnPush - same array reference, view won't update
this.items.push(newItem);

// GOOD - new reference, OnPush detects the change
this.items = [...this.items, newItem];
```

---

### Q9. What is Zoneless Change Detection, and how does it relate to Signals?

**Answer:**
"Zoneless change detection (stable as of recent Angular versions) removes the dependency on Zone.js entirely — instead of reacting to 'some async browser API fired,' Angular relies on explicit, granular signals of what actually changed: Signals themselves (a signal write notifies exactly the views that read it), plus `markForCheck()` calls from things like the `async` pipe. This is more precise (only the parts that could have actually changed get checked) and removes a real chunk of runtime overhead and bundle size that Zone.js patching added. It's the direction Angular's whole reactivity model is heading — Signals aren't just a nicer API, they're what makes zoneless apps practical."

---

### Q10. What are `ChangeDetectorRef.markForCheck()`, `detectChanges()`, and `detach()`?

**Answer:**
"`markForCheck()` flags this component (and its ancestors, up to the root) as needing to be checked on the *next* change detection cycle — used constantly with `OnPush` when something outside Angular's normal triggers changed state (e.g., a WebSocket callback). `detectChanges()` runs change detection immediately, synchronously, for this component and its children — right now, not waiting for the next cycle — useful in tests or rare manual-control scenarios. `detach()` completely removes a component from the change detection tree until you call `reattach()` — an escape hatch for components that manage their own rendering entirely and should never be automatically checked."

```typescript
constructor(private cdr: ChangeDetectorRef, private ws: WebSocketService) {
  this.ws.messages$.subscribe(msg => {
    this.latestMessage = msg;
    this.cdr.markForCheck(); // tell OnPush "check me on the next cycle" — this came from outside Angular's normal triggers
  });
}
```

---

### Q11. What is `trackBy` in `*ngFor`, and what breaks if you omit it on a large list?

**Answer:**
"Without `trackBy`, Angular's default identity check for `*ngFor` items is by object reference — if the array is replaced with a new array containing conceptually-the-same-but-newly-created objects (common after a fresh API response), Angular can't tell they're 'the same' item and destroys/recreates every single DOM node for the whole list, even if nothing visually changed. `trackBy` gives Angular a stable identity function (usually an ID) so it can match old and new items correctly and only touch the DOM nodes that actually changed."

```typescript
trackByFn(index: number, item: User) { return item.id; }
```
```html
<div *ngFor="let user of users; trackBy: trackByFn">{{ user.name }}</div>
```

**Where to use:** any `*ngFor`/`@for` over a list that gets refreshed from an API repeatedly — without it, large lists suffer real, visible rendering jank on every refresh.

---

### Q12. Pure vs Impure pipes — what's the performance implication?

**Answer:**
"A pure pipe (the default) only re-executes when its input *reference* changes — same optimization philosophy as `OnPush`. An impure pipe (`pure: false`) re-executes on *every* change detection cycle, regardless of whether its input actually changed — which can be a real performance problem if the pipe does expensive work, since it now runs far more often than necessary."

```typescript
@Pipe({ name: 'expensiveFilter', pure: false }) // runs on EVERY change detection cycle, not just on input change
export class ExpensiveFilterPipe implements PipeTransform {
  transform(items: Item[]): Item[] { /* ... */ }
}
```

**Where to use:** impure pipes only when you genuinely need to react to mutations of an object/array in place (rare, and usually better solved by fixing the mutation pattern instead) — default to pure pipes.

---

### Q13. What is the full Angular component lifecycle hook order, and which two are most commonly misused?

**Answer:**
"`ngOnChanges` (before `ngOnInit`, and again on every subsequent `@Input()` change) → `ngOnInit` (once, after the first `ngOnChanges`) → `ngDoCheck` (every change detection cycle) → `ngAfterContentInit` → `ngAfterContentChecked` → `ngAfterViewInit` (once, after the component's own view and child views are fully initialized) → `ngAfterViewChecked` → `ngOnDestroy`. The two most commonly misused: reading `@Input()` values inside the constructor (they're not set yet — see the cross-question), and trying to access a `@ViewChild` inside `ngOnInit` instead of `ngAfterViewInit` (the child view isn't guaranteed to exist yet in `ngOnInit`)."

```typescript
@ViewChild('myDiv') myDiv!: ElementRef;

ngOnInit() {
  console.log(this.myDiv); // undefined — view isn't initialized yet
}
ngAfterViewInit() {
  console.log(this.myDiv); // correctly available now
}
```

**Cross-question: Why is `@Input()` not yet guaranteed to be set inside a component's constructor?**
"Because Angular constructs the component instance first, and only *afterward* sets its `@Input()`-bound properties and calls `ngOnChanges`/`ngOnInit`. The constructor is purely for dependency injection — using it to read `@Input()` values reads `undefined`, since those bindings haven't been applied yet at that point in the component's creation."

**Cross-question: What is the `SimpleChanges` object passed into `ngOnChanges`, and what would you use `ngDoCheck` for that `ngOnChanges` can't tell you?**
"`SimpleChanges` is a dictionary keyed by input property name, each value containing `previousValue`, `currentValue`, and `firstChange` — it tells you exactly which `@Input()`s changed and what they changed from/to, but only for reference changes on `@Input()`-bound properties. `ngDoCheck` runs on every change detection cycle regardless, and is the escape hatch for detecting changes `ngOnChanges` structurally *can't* see — like a mutation inside an object/array whose reference didn't change, or state that isn't an `@Input()` at all. It's rarely needed and easy to make expensive if you're not careful, since it runs constantly."

```typescript
ngOnChanges(changes: SimpleChanges) {
  if (changes['userId'] && !changes['userId'].firstChange) {
    console.log(`userId changed from ${changes['userId'].previousValue} to ${changes['userId'].currentValue}`);
  }
}
```
