# Angular — Routing & Forms — Interview Q&A

---

### Q1. What are Route Guards, and what's a realistic use case for each?

**Answer:**
"`CanActivate` runs before a route is entered — the classic use case is blocking navigation to a page unless the user is authenticated. `CanDeactivate` runs before *leaving* a route — used to warn 'you have unsaved changes, are you sure you want to leave?' on a form page. `Resolve` pre-fetches data *before* the route activates, so the component never renders in a half-loaded state waiting on an API call. `CanMatch` (newer, replacing part of what `CanLoad` did) controls whether a route configuration is even considered a match at all — useful for conditionally offering entirely different routes/lazy chunks based on a feature flag."

```typescript
export const authGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  return auth.isLoggedIn() ? true : inject(Router).createUrlTree(['/login']);
};

{ path: 'admin', component: AdminComponent, canActivate: [authGuard] }
```

**Cross-question: What's the difference between blocking navigation with a guard vs just checking auth inside the component itself?**
"A guard runs *before* the component is even created — an unauthenticated user never sees the component mount, flash, or fire any of its lifecycle hooks/API calls at all. Checking inside the component (e.g., in `ngOnInit`) means the component still gets created and initialized first, potentially triggering unwanted side effects (API calls, brief UI flicker) before the check redirects away. Guards are the correct layer for 'should this navigation even be allowed to happen.'"

---

### Q2. How does lazy loading work for feature modules/routes, and what's the actual performance benefit?

**Answer:**
"Instead of bundling every feature into the app's single initial JavaScript bundle, a lazy-loaded route's code is split into a separate chunk that's only downloaded when the user actually navigates to that route. The performance benefit is entirely about initial load time — users pay the download/parse cost only for the features they actually visit, not the whole application upfront, which matters a lot for large apps with many rarely-visited sections (admin panels, settings pages, etc.)."

```typescript
{ path: 'admin', loadChildren: () => import('./admin/admin.routes').then(m => m.ADMIN_ROUTES) }
// or for a single standalone component:
{ path: 'reports', loadComponent: () => import('./reports/reports.component').then(m => m.ReportsComponent) }
```

---

### Q3. Reactive Forms vs Template-Driven Forms — what are the real trade-offs?

**Answer:**
"Template-Driven Forms build the form model implicitly from directives in the template (`ngModel`, `required`) — quick for simple forms, but the form's structure and validation logic live scattered across the template, harder to unit test in isolation, and async-by-nature (the form model isn't fully available synchronously). Reactive Forms build the form model explicitly in the component class (`FormGroup`, `FormControl`), giving synchronous access to the whole form's value/state, straightforward unit testing without rendering any template at all, and much easier support for dynamic forms (fields added/removed based on logic) and complex custom/cross-field validation. For anything beyond a trivial form, senior developers default to Reactive Forms — the upfront verbosity pays for itself in testability and control as the form grows."

```typescript
// Reactive - explicit, testable, synchronous
this.form = this.fb.group({
  email: ['', [Validators.required, Validators.email]],
  password: ['', Validators.required]
});
```

---

### Q4. How do you write a custom validator, and what's the difference between a sync and an async validator?

**Answer:**
"A sync validator is a function taking an `AbstractControl` and returning either `null` (valid) or a `ValidationErrors` object immediately. An async validator does the same, but returns an `Observable<ValidationErrors | null>` (or a Promise) — used when the validation itself requires an API call (e.g., checking if a username is already taken), and it can't be answered synchronously."

```typescript
// Sync validator
export function forbiddenNameValidator(forbidden: string): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null =>
    control.value === forbidden ? { forbiddenName: true } : null;
}

// Async validator
export function uniqueUsernameValidator(api: UserService): AsyncValidatorFn {
  return (control: AbstractControl) =>
    api.checkUsernameTaken(control.value).pipe(
      map(taken => (taken ? { usernameTaken: true } : null))
    );
}

this.form = this.fb.group({
  username: ['', [Validators.required], [uniqueUsernameValidator(this.userService)]] // 3rd array = async validators
});
```

---

### Q5. What is `ControlValueAccessor`, and when do you need to implement it?

**Answer:**
"It's the interface that lets Angular's forms system (`ngModel`/`formControlName`) communicate with a custom component the same way it communicates with native inputs — implementing `writeValue` (form → component), `registerOnChange`/`registerOnTouched` (component → form), and optionally `setDisabledState`. Without it, Angular has no idea how to get/set a value on your custom component, since it's not a native `<input>`."

```typescript
@Component({ selector: 'app-star-rating', /* ... */ })
export class StarRatingComponent implements ControlValueAccessor {
  value = 0;
  onChange: (value: number) => void = () => {};
  onTouched: () => void = () => {};

  writeValue(value: number): void { this.value = value; }
  registerOnChange(fn: (value: number) => void): void { this.onChange = fn; }
  registerOnTouched(fn: () => void): void { this.onTouched = fn; }

  selectStar(star: number) {
    this.value = star;
    this.onChange(star); // tells the form "the value changed"
    this.onTouched();
  }
}
```

**Cross-question: Why doesn't `[(ngModel)]` or `formControlName` work on a custom component out of the box without it?**
"Because Angular's forms module has no built-in knowledge of your component's internal API — it only knows how to talk to the standard DOM form elements (via built-in `ControlValueAccessor` implementations Angular ships for `<input>`, `<select>`, etc.) or to a component that explicitly declares itself as a `ControlValueAccessor` via the `NG_VALUE_ACCESSOR` provider token. Without that contract, there's no mechanism for the form to push a value in or be notified when your component's internal value changes."

---

### Q6. How do you handle cross-field validation in Reactive Forms?

**Answer:**
"Put the validator on the parent `FormGroup` itself, not on either individual control — that way it has access to both controls' current values at once and can compare them, then attach any resulting error to whichever control makes sense (or to the group itself)."

```typescript
export function passwordMatchValidator(group: AbstractControl): ValidationErrors | null {
  const password = group.get('password')?.value;
  const confirm = group.get('confirmPassword')?.value;
  return password === confirm ? null : { passwordMismatch: true };
}

this.form = this.fb.group({
  password: ['', Validators.required],
  confirmPassword: ['', Validators.required]
}, { validators: passwordMatchValidator }); // validator on the GROUP, not either individual control
```

```html
<div *ngIf="form.errors?.['passwordMismatch']">Passwords do not match</div>
```
