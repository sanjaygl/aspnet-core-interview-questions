# Angular — RxJS & Reactive Patterns — Interview Q&A

---

### Q1. Observable vs Promise — what are the real, practical differences?

**Answer:**
"A Promise represents a single future value and starts executing immediately when created — it can't be cancelled, and once it resolves/rejects, that's final. An Observable represents a stream of zero, one, or many values over time, is lazy (nothing happens until something subscribes), and is cancellable via unsubscribing. Observables also come with the entire RxJS operator library for transforming/combining streams, which Promises don't have natively."

```typescript
// Promise - one value, eager, no cancellation
const promise = fetch('/api/data'); // starts immediately, even if nothing awaits it

// Observable - lazy, cancellable, can emit many values over time
const obs$ = this.http.get('/api/data'); // does NOTHING until subscribed
const sub = obs$.subscribe(data => console.log(data));
sub.unsubscribe(); // can cancel an in-flight request
```

---

### Q2. What's the difference between an Observable, an Observer, and a Subscription?

**Answer:**
"An Observable is the producer — a blueprint describing how and when to emit values, but it does nothing on its own until subscribed to. An Observer is the consumer — an object with (up to) three callbacks: `next` (a new value arrived), `error` (something went wrong), and `complete` (no more values will come). A Subscription is the handle returned when you actually connect an Observer to an Observable via `.subscribe()` — it represents that specific, ongoing execution, and calling `.unsubscribe()` on it tears down that connection and stops further emissions/cleans up resources."

```typescript
const observable$ = new Observable<number>(subscriber => {
  subscriber.next(1);
  subscriber.next(2);
  subscriber.complete();
});

const observer = {
  next: (value: number) => console.log('Got:', value),
  error: (err: any) => console.error(err),
  complete: () => console.log('Done')
};

const subscription: Subscription = observable$.subscribe(observer); // connects Observer to Observable
subscription.unsubscribe(); // tears down that connection
```

**Cross-question: Is a `Subject` an Observable, an Observer, or both — and what does that dual nature actually let it do?**
"Both — a `Subject` implements the `Observable` interface (you can `.subscribe()` to it) AND the `Observer` interface (you can call `.next()`, `.error()`, `.complete()` on it directly). That's exactly what lets it act as a bridge: something imperative (like a button click handler, or a plain callback-based API) can push values into it via `.next()`, while multiple other parts of the app subscribe to it like any other Observable to receive those values."

---

### Q3. What does "multicast" vs "unicast" mean for an Observable?

**Answer:**
"A unicast Observable runs its own independent producer logic *separately* for every single subscriber — two subscribers to the same unicast Observable each get their own independent execution (e.g., two separate HTTP requests, if the Observable wraps an HTTP call). A multicast Observable shares one single underlying execution among all its subscribers — they all receive the same values from the same execution, rather than each triggering their own. A plain `new Observable(...)` (and `HttpClient` methods) is unicast by default; `Subject` (and its variants) is inherently multicast."

```typescript
const unicast$ = this.http.get('/api/data'); // each .subscribe() triggers a SEPARATE HTTP request
unicast$.subscribe(); // request #1
unicast$.subscribe(); // request #2 - a totally separate call

const subject = new Subject<number>(); // multicast - all subscribers share the SAME emissions
subject.subscribe(v => console.log('A:', v));
subject.subscribe(v => console.log('B:', v));
subject.next(1); // BOTH A and B log this same single emission
```

**Where to use:** `share()`/`shareReplay()` operators convert a unicast Observable into a multicast one — useful for an HTTP call that multiple parts of the UI subscribe to, so you don't accidentally fire the same request multiple times.

---

### Q4. `Subject` vs `BehaviorSubject` vs `ReplaySubject` vs `AsyncSubject` — what's actually different?

**Answer:**
"`Subject` has no memory — a new subscriber only gets values emitted *after* it subscribes, nothing from before. `BehaviorSubject` always holds a current value (requires an initial value at construction) and immediately replays that current value to any new subscriber — great for representing 'current state.' `ReplaySubject` replays some configurable number (or all) of the previous emissions to a new subscriber, not just the latest one. `AsyncSubject` only emits the *final* value, and only once the source completes — new subscribers get nothing until then, and get exactly that one final value."

```typescript
const subject = new Subject<number>();
const behaviorSubject = new BehaviorSubject<number>(0);   // must have an initial value
const replaySubject = new ReplaySubject<number>(2);       // replays the last 2 values to new subscribers
const asyncSubject = new AsyncSubject<number>();          // only emits the final value, on complete
```

**Where to use:** `BehaviorSubject` for shared "current state" services (the most common of the four in real Angular apps); `ReplaySubject` when a late subscriber genuinely needs some history, not just the latest; plain `Subject` for pure event streams with no "current value" concept (like a click event bus); `AsyncSubject` is rarely reached for directly in application code.

---

### Q5. `switchMap` vs `mergeMap` vs `concatMap` vs `exhaustMap` — the classic RxJS question.

**Answer:**
"All four flatten an Observable of Observables (or Promises) into a single stream, but differ in how they handle a *new* source value arriving while a previous inner Observable is still in flight. `switchMap` cancels the previous inner Observable and switches to the new one — only the latest matters. `mergeMap` runs all inner Observables concurrently, in parallel, merging their emissions as they arrive — order isn't preserved, nothing is cancelled. `concatMap` queues inner Observables and runs them strictly one at a time, in order — the next doesn't start until the current one completes. `exhaustMap` ignores new source values entirely while an inner Observable is still active — only starts a new one once the current one finishes."

```typescript
// switchMap - type-ahead search: cancel the stale request, only care about the latest keystroke
searchInput$.pipe(switchMap(term => this.api.search(term)));

// mergeMap - fire off several independent uploads concurrently, order doesn't matter
fileUploads$.pipe(mergeMap(file => this.api.upload(file)));

// concatMap - sequential operations that must happen in order, one after another
saveSteps$.pipe(concatMap(step => this.api.saveStep(step)));

// exhaustMap - ignore repeat submit clicks while the first request is still in flight
submitClick$.pipe(exhaustMap(() => this.api.submitForm()));
```

**Cross-question: Which of these four would you use for a type-ahead search box, and why specifically that one?**
"`switchMap` — every new keystroke should cancel whatever search request is still in flight for the *previous* keystroke, since only the latest search term's result actually matters. Using `mergeMap` there would risk an older, slower response arriving after a newer one and incorrectly overwriting the more recent, more relevant results on screen."

**Cross-question: Which one would you use for a submit button, to guarantee a second click can't fire a duplicate request while the first is still in flight?**
"`exhaustMap` — it ignores any new emissions entirely while the current inner Observable is still active, which is exactly 'ignore extra clicks until the current submit finishes' — as opposed to `switchMap`, which would instead cancel the first submit and start a second one, potentially submitting the form twice if the user's second click reflects a different, half-typed state."

---

### Q6. How do you prevent memory leaks from subscriptions in Angular components?

**Answer:**
"Any subscription to a long-lived/global Observable (a service's `BehaviorSubject`, a `setInterval`-based Observable, a WebSocket stream) needs to be explicitly torn down when the component is destroyed — otherwise it keeps running and holding a reference to the destroyed component's callback, which both wastes resources and can cause errors trying to update a UI that no longer exists. The standard pattern is unsubscribing in `ngOnDestroy`, commonly via the `takeUntil(this.destroy$)` pattern, or (preferred where possible) just using the `async` pipe in the template, which handles subscription/unsubscription automatically."

```typescript
private destroy$ = new Subject<void>();

ngOnInit() {
  this.someService.data$.pipe(takeUntil(this.destroy$)).subscribe(data => this.data = data);
}
ngOnDestroy() {
  this.destroy$.next();
  this.destroy$.complete();
}
```

**Cross-question: Why does the `async` pipe in a template avoid this problem automatically?**
"Because the `async` pipe itself subscribes when the template is rendered and automatically unsubscribes when the component/template is destroyed — it's built to manage that subscription's lifecycle for you, tied to Angular's own change detection lifecycle, so there's no `ngOnDestroy` boilerplate needed at all for that specific binding."

```html
<div>{{ data$ | async }}</div> <!-- subscribes on render, unsubscribes automatically on destroy -->
```

---

### Q7. What is the difference between a "cold" and a "hot" Observable?

**Answer:**
"A cold Observable doesn't start producing values until something subscribes — each subscriber triggers its own independent execution (this is the same idea as 'unicast' from Q3). A hot Observable is already producing values regardless of whether anyone's subscribed yet — subscribers just tap into whatever's currently happening, potentially missing earlier emissions. An `HttpClient` call is cold (nothing happens until subscribed); a `Subject` wrapping DOM click events is hot (clicks happen whether or not anyone's currently subscribed)."

---

### Q8. What does `takeUntil` do, and why is it a common pattern paired with a `Subject` for unsubscribing?

**Answer:**
"`takeUntil(notifier$)` keeps forwarding emissions from the source Observable until the `notifier$` Observable itself emits — at that point, it automatically completes (and unsubscribes from) the source. Pairing it with a `destroy$` Subject that's `.next()`'d in `ngOnDestroy` gives a single, reusable 'kill switch' — every subscription in the component pipes through `takeUntil(this.destroy$)`, and one `this.destroy$.next()` in `ngOnDestroy` cleanly tears down all of them at once, instead of manually tracking and unsubscribing from each `Subscription` object individually."

---

### Q9. What's the difference between `combineLatest`, `forkJoin`, and `zip`?

**Answer:**
"`combineLatest` emits whenever *any* of the source Observables emits, combining the latest value from each — keeps re-emitting over time as any one of them changes. `forkJoin` waits for *all* source Observables to complete, then emits once with the last value from each — the RxJS equivalent of `Promise.all`, good for 'wait for a fixed set of requests, then proceed.' `zip` pairs up emissions by *index* across sources — its Nth emission combines the Nth value from each source, waiting for all of them to have produced that Nth value."

```typescript
// combineLatest - re-emits every time ANY source changes
combineLatest([user$, settings$]).subscribe(([user, settings]) => { /* runs on every change to either */ });

// forkJoin - waits for ALL to complete, emits once - good for "wait for these 3 API calls, then continue"
forkJoin([this.api.getUser(), this.api.getOrders(), this.api.getSettings()])
  .subscribe(([user, orders, settings]) => { /* all three finished */ });

// zip - pairs by index, waits for the Nth value from every source
zip([source1$, source2$]).subscribe(([a, b]) => { /* Nth emission from each, paired together */ });
```

**Where to use:** `forkJoin` for "load these independent things once, then render the page"; `combineLatest` for reactive UI state built from several ongoing streams (e.g., filters + search term + sort order all recombining live); `zip` is the least commonly needed of the three in typical Angular apps.
