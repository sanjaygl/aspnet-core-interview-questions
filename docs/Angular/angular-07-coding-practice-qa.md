# Angular — Coding Practice — Interview Q&A

---

### Q1. Write a custom pipe (e.g., a `truncate` pipe).

**Answer:**
```typescript
@Pipe({ name: 'truncate', standalone: true })
export class TruncatePipe implements PipeTransform {
  transform(value: string, limit = 50, suffix = '...'): string {
    if (!value || value.length <= limit) return value;
    return value.substring(0, limit) + suffix;
  }
}
```
```html
<p>{{ longText | truncate:100 }}</p>
```

---

### Q2. Write a custom attribute directive (e.g., a `highlight` directive reacting to hover).

**Answer:**
```typescript
@Directive({ selector: '[appHighlight]', standalone: true })
export class HighlightDirective {
  @Input() highlightColor = 'yellow';

  constructor(private el: ElementRef) {}

  @HostListener('mouseenter') onMouseEnter() {
    this.el.nativeElement.style.backgroundColor = this.highlightColor;
  }
  @HostListener('mouseleave') onMouseLeave() {
    this.el.nativeElement.style.backgroundColor = '';
  }
}
```
```html
<p appHighlight highlightColor="lightblue">Hover over me</p>
```

---

### Q3. Write a custom structural directive (like a simplified version of `*ngIf`).

**Answer:**
"A structural directive works by manipulating a `TemplateRef` via a `ViewContainerRef` — creating or clearing the embedded view based on a condition."

```typescript
@Directive({ selector: '[appUnless]', standalone: true })
export class UnlessDirective {
  private hasView = false;

  constructor(private templateRef: TemplateRef<unknown>, private viewContainer: ViewContainerRef) {}

  @Input() set appUnless(condition: boolean) {
    if (!condition && !this.hasView) {
      this.viewContainer.createEmbeddedView(this.templateRef);
      this.hasView = true;
    } else if (condition && this.hasView) {
      this.viewContainer.clear();
      this.hasView = false;
    }
  }
}
```
```html
<p *appUnless="isLoggedIn">Please log in to continue.</p>
```

---

### Q4. Implement a debounced type-ahead search using RxJS operators.

**Answer:**
"The classic combo: `debounceTime` waits for the user to pause typing, `distinctUntilChanged` skips re-searching the same term twice in a row, and `switchMap` cancels any still-in-flight request for a stale search term."

```typescript
searchControl = new FormControl('');

results$ = this.searchControl.valueChanges.pipe(
  debounceTime(300),
  distinctUntilChanged(),
  filter(term => (term ?? '').length >= 2), // don't search on 0-1 characters
  switchMap(term => this.api.search(term ?? ''))
);
```
```html
<input [formControl]="searchControl" />
<div *ngFor="let result of results$ | async">{{ result.name }}</div>
```

---

### Q5. Implement `ControlValueAccessor` for a custom form control (a star-rating component).

**Answer:**
```typescript
@Component({
  selector: 'app-star-rating',
  standalone: true,
  template: `<span *ngFor="let s of [1,2,3,4,5]" (click)="select(s)">{{ s <= value ? '★' : '☆' }}</span>`,
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: StarRatingComponent, multi: true }]
})
export class StarRatingComponent implements ControlValueAccessor {
  value = 0;
  disabled = false;
  private onChange: (value: number) => void = () => {};
  private onTouched: () => void = () => {};

  writeValue(value: number): void { this.value = value ?? 0; }
  registerOnChange(fn: (value: number) => void): void { this.onChange = fn; }
  registerOnTouched(fn: () => void): void { this.onTouched = fn; }
  setDisabledState(isDisabled: boolean): void { this.disabled = isDisabled; }

  select(star: number): void {
    if (this.disabled) return;
    this.value = star;
    this.onChange(star);
    this.onTouched();
  }
}
```
```html
<app-star-rating formControlName="rating"></app-star-rating> <!-- works just like a native input -->
```

---

### Q6. Write a custom Reactive Forms validator (sync) and a custom async validator.

**Answer:**
```typescript
// Sync
export function noWhitespaceValidator(control: AbstractControl): ValidationErrors | null {
  return (control.value ?? '').trim().length === 0 ? { whitespace: true } : null;
}

// Async - checking username availability against an API
export function usernameAvailableValidator(api: UserService): AsyncValidatorFn {
  return (control: AbstractControl) =>
    control.value
      ? api.isUsernameTaken(control.value).pipe(
          map(taken => (taken ? { usernameTaken: true } : null)),
          catchError(() => of(null)) // don't block the form if the availability check itself fails
        )
      : of(null);
}
```
```typescript
this.form = this.fb.group({
  username: ['', [Validators.required, noWhitespaceValidator], [usernameAvailableValidator(this.userService)]]
});
```

---

### Q7. Write an `HttpInterceptor` (attaching an auth token, handling a 401 globally).

**Answer:**
```typescript
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService, private router: Router) {}

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.auth.getToken();
    const authReq = token
      ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
      : req;

    return next.handle(authReq).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.auth.logout();
          this.router.navigate(['/login']);
        }
        return throwError(() => error);
      })
    );
  }
}
```
```typescript
providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }]
```

---

### Q8. Write a custom RxJS operator using `pipe()` composition.

**Answer:**
"A custom operator is just a function that takes a source Observable and returns a transformed one — usually built by composing existing operators rather than writing a subscriber from scratch."

```typescript
function retryWithBackoff<T>(maxRetries = 3, delayMs = 1000) {
  return (source: Observable<T>): Observable<T> =>
    source.pipe(
      retryWhen(errors =>
        errors.pipe(
          scan((retryCount, error) => {
            if (retryCount >= maxRetries) throw error;
            return retryCount + 1;
          }, 0),
          delay(delayMs)
        )
      )
    );
}

// Usage - composes into a normal pipe() chain like any built-in operator
this.api.getData().pipe(retryWithBackoff(3, 1000)).subscribe(data => { /* ... */ });
```
