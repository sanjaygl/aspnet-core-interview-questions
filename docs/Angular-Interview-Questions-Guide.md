# 🚀 Angular Interview Questions with Answers & Code Examples

> **Angular Version Focus**: Angular 16–21 (latest features included)
>
> **Audience**: Beginner → Senior → Lead Developers
>
> **Structure**: Questions are split into **Beginner**, **Advanced**, and **Expert** levels

---

## 📑 Table of Contents

### 🟢 Beginner Level (Foundations)

001. [What is Angular?](#1-what-is-angular)
002. [What are the main building blocks of Angular?](#2-what-are-the-main-building-blocks-of-angular)
003. [Difference between constructor and ngOnInit?](#3-difference-between-constructor-and-ngoninit)
004. [What is data binding?](#4-what-is-data-binding)
005. [What is a directive?](#5-what-is-a-directive)

### 🟡 Advanced Level (Real Interview Focus)

006. [What is Change Detection?](#6-what-is-change-detection)
007. [What is RxJS and why is it used?](#7-what-is-rxjs-and-why-is-it-used)
008. [Difference between Subject and BehaviorSubject?](#8-difference-between-subject-and-behaviorsubject)
009. [What is an HTTP Interceptor?](#9-what-is-an-http-interceptor)
010. [What are Reactive Forms?](#10-what-are-reactive-forms)

### 🔴 Expert Level (Senior / Lead)

011. [What are Angular Signals? (Angular 16+)](#11-what-are-angular-signals-angular-16)
012. [Signals vs RxJS – when to use which?](#12-signals-vs-rxjs--when-to-use-which)
013. [What is OnPush change detection internally?](#13-what-is-onpush-change-detection-internally)
014. [What is Lazy Loading?](#14-what-is-lazy-loading)
015. [How does Angular prevent XSS?](#15-how-does-angular-prevent-xss)
016. [What is Server-Side Rendering (SSR)?](#16-what-is-server-side-rendering-ssr)
017. [What is Module Federation?](#17-what-is-module-federation)
018. [How do you optimize Angular performance?](#18-how-do-you-optimize-angular-performance)
019. [How do you handle global error handling?](#19-how-do-you-handle-global-error-handling)
020. [How do you structure a large Angular application?](#20-how-do-you-structure-a-large-angular-application)
021. [What are Standalone Components? (Angular 15+)](#21-what-are-standalone-components-angular-15)
022. [What is Control Flow syntax in Angular? (Angular 17+)](#22-what-is-control-flow-syntax-in-angular-angular-17)
023. [What is hydration in Angular SSR? (Angular 17+)](#23-what-is-hydration-in-angular-ssr-angular-17)
024. [What are some new features in Angular 18–21?](#24-what-are-some-new-features-in-angular-1821)
025. [How do you migrate a legacy Angular app to the latest version?](#25-how-do-you-migrate-a-legacy-angular-app-to-the-latest-version)
026. [How do you test Signals and Standalone Components?](#26-how-do-you-test-signals-and-standalone-components)
027. [How do you secure Angular apps (Angular 16+)?](#27-how-do-you-secure-angular-apps-angular-16)
028. [How do you use typed forms in Angular 17+?](#28-how-do-you-use-typed-forms-in-angular-17)
029. [How do you implement micro frontends in Angular 17+?](#29-how-do-you-implement-micro-frontends-in-angular-17)
030. [What is the future direction of Angular (v21+)?](#30-what-is-the-future-direction-of-angular-v21)
031. [How do you use Angular's new built-in control flow?](#31-how-do-you-use-angulars-new-built-in-control-flow-if-for-switch-in-templates-angular-17)
032. [How do you create a custom pipe in Angular?](#32-how-do-you-create-a-custom-pipe-in-angular)
033. [How do you use Angular signals in a service for shared state?](#33-how-do-you-use-angular-signals-in-a-service-for-shared-state-angular-16)
034. [How do you implement route guards using functional APIs?](#34-how-do-you-implement-route-guards-using-functional-apis-angular-15)
035. [How do you use dependency injection in standalone components?](#35-how-do-you-use-dependency-injection-in-standalone-components-angular-15)
036. [How do you use the new provideHttpClient API?](#36-how-do-you-use-the-new-providehttpclient-api-angular-15)
037. [How do you implement optimistic UI updates with signals?](#37-how-do-you-implement-optimistic-ui-updates-with-signals)
038. [How do you use Angular's built-in form validation improvements?](#38-how-do-you-use-angulars-built-in-form-validation-improvements-angular-18)
039. [How do you use the new @defer block for lazy rendering?](#39-how-do-you-use-the-new-defer-block-for-lazy-rendering-angular-17)
040. [How do you use the Angular CLI to generate a standalone component?](#40-how-do-you-use-the-angular-cli-to-generate-a-standalone-component)
041. [How do you use the new inject function in Angular?](#41-how-do-you-use-the-new-inject-function-in-angular-angular-14)
042. [How do you use the new destroyRef for cleanup in Angular?](#42-how-do-you-use-the-new-destroyref-for-cleanup-in-angular-angular-16)
043. [How do you use the new @input transform for input coercion?](#43-how-do-you-use-the-new-input-transform-for-input-coercion-angular-16)
044. [How do you use the new @output transform for output events?](#44-how-do-you-use-the-new-output-transform-for-output-events-angular-16)
045. [How do you use the new @model decorator for two-way binding?](#45-how-do-you-use-the-new-model-decorator-for-two-way-binding-angular-17)
046. [How do you use the new @signalEffect for side effects?](#46-how-do-you-use-the-new-signaleffect-for-side-effects-angular-18)
047. [How do you use the new @computed decorator for derived state?](#47-how-do-you-use-the-new-computed-decorator-for-derived-state-angular-17)
048. [How do you use the new @injectable decorator for tree-shakable services?](#48-how-do-you-use-the-new-injectable-decorator-for-tree-shakable-services-angular-16)
049. [How do you use the new @environmentInjector for environment-specific providers?](#49-how-do-you-use-the-new-environmentinjector-for-environment-specific-providers-angular-17)
050. [How do you use the new @hydration API for SSR hydration?](#50-how-do-you-use-the-new-hydration-api-for-ssr-hydration-angular-17)
051. [How do you use the new @input required flag for required inputs?](#51-how-do-you-use-the-new-input-required-flag-for-required-inputs-angular-16)
052. [How do you use the new @output required flag for required outputs?](#52-how-do-you-use-the-new-output-required-flag-for-required-outputs-angular-16)
053. [How do you use the new @HostDirectives decorator?](#53-how-do-you-use-the-new-hostdirectives-decorator-angular-16)
054. [How do you use the new @Self, @SkipSelf, @Optional, and @Host DI flags?](#54-how-do-you-use-the-new-self-skipself-optional-and-host-di-flags)
055. [How do you use the new @ViewChild and @ContentChild static option?](#55-how-do-you-use-the-new-viewchild-and-contentchild-static-option)
056. [How do you use the new @NgOptimizedImage directive?](#56-how-do-you-use-the-new-ngoptimizedimage-directive-angular-16)
057. [How do you use the new @NgIf else block with @if?](#57-how-do-you-use-the-new-ngif-else-block-with-if-angular-17)
058. [How do you use the new @NgFor with track and empty blocks?](#58-how-do-you-use-the-new-ngfor-with-track-and-empty-blocks-angular-17)
059. [How do you use the new @NgSwitch for advanced template branching?](#59-how-do-you-use-the-new-ngswitch-for-advanced-template-branching-angular-17)
060. [How do you use the new @NgTemplateOutlet for dynamic templates?](#60-how-do-you-use-the-new-ngtemplateoutlet-for-dynamic-templates)
061. [How do you use the new @NgZone for manual change detection?](#61-how-do-you-use-the-new-ngzone-for-manual-change-detection)
062. [How do you use the new @Renderer2 for safe DOM manipulation?](#62-how-do-you-use-the-new-renderer2-for-safe-dom-manipulation)
063. [How do you use the new @NgPlural for pluralization?](#63-how-do-you-use-the-new-ngplural-for-pluralization)
064. [How do you use the new @NgClass and @NgStyle for dynamic styling?](#64-how-do-you-use-the-new-ngclass-and-ngstyle-for-dynamic-styling)
065. [How do you use the new @NgContent for content projection?](#65-how-do-you-use-the-new-ngcontent-for-content-projection)
066. [How do you use the new @NgModule for legacy support?](#66-how-do-you-use-the-new-ngmodule-for-legacy-support)
067. [How do you use the new @NgUpgrade for hybrid AngularJS/Angular apps?](#67-how-do-you-use-the-new-ngupgrade-for-hybrid-angularjsangular-apps)
068. [How do you use the new @NgRx for state management?](#68-how-do-you-use-the-new-ngrx-for-state-management)
069. [How do you use the new @Ngxs for state management?](#69-how-do-you-use-the-new-ngxs-for-state-management)
070. [How do you use the new @ApolloAngular for GraphQL integration?](#70-how-do-you-use-the-new-apolloangular-for-graphql-integration)
071. [How do you use the new @ngx-translate for internationalization (i18n)?](#71-how-do-you-use-the-new-ngx-translate-for-internationalization-i18n)
072. [How do you use the new @angular/localize for built-in i18n?](#72-how-do-you-use-the-new-angularlocalize-for-built-in-i18n)
073. [How do you use the new @angular/service-worker for PWA support?](#73-how-do-you-use-the-new-angularservice-worker-for-pwa-support)
074. [How do you use the new @angular/animations for advanced animations?](#74-how-do-you-use-the-new-angularanimations-for-advanced-animations)
075. [How do you use the new @angular/cdk for accessibility and utilities?](#75-how-do-you-use-the-new-angularcdk-for-accessibility-and-utilities)
076. [How do you use the new @angular/material for UI components?](#76-how-do-you-use-the-new-angularmaterial-for-ui-components)
077. [How do you use the new @angular/forms for custom validators?](#77-how-do-you-use-the-new-angularforms-for-custom-validators)
078. [How do you use the new @angular/router for lazy loading with standalone components?](#78-how-do-you-use-the-new-angularrouter-for-lazy-loading-with-standalone-components)
079. [How do you use the new @angular/router for route resolvers?](#79-how-do-you-use-the-new-angularrouter-for-route-resolvers)
080. [How do you use the new @angular/router for guards with signals?](#80-how-do-you-use-the-new-angularrouter-for-guards-with-signals)
081. [How do you use the new @angular/router for canMatch and canLoad guards?](#81-how-do-you-use-the-new-angularrouter-for-canmatch-and-canload-guards)
082. [How do you use the new @angular/router for router events?](#82-how-do-you-use-the-new-angularrouter-for-router-events)
083. [How do you use the new @angular/router for router outlets and named outlets?](#83-how-do-you-use-the-new-angularrouter-for-router-outlets-and-named-outlets)
084. [How do you use the new @angular/router for routerLinkActive and routerLinkActiveOptions?](#84-how-do-you-use-the-new-angularrouter-for-routerlinkactive-and-routerlinkactiveoptions)
085. [How do you use the new @angular/router for query params and fragments?](#85-how-do-you-use-the-new-angularrouter-for-query-params-and-fragments)
086. [How do you use the new @angular/router for navigation extras?](#86-how-do-you-use-the-new-angularrouter-for-navigation-extras)
087. [How do you use the new @angular/router for route reuse strategy?](#87-how-do-you-use-the-new-angularrouter-for-route-reuse-strategy)
088. [How do you use the new @angular/router for preloading strategies?](#88-how-do-you-use-the-new-angularrouter-for-preloading-strategies)
089. [How do you use the new @angular/router for custom URL serializers?](#89-how-do-you-use-the-new-angularrouter-for-custom-url-serializers)
090. [How do you use the new @angular/router for router state snapshots?](#90-how-do-you-use-the-new-angularrouter-for-router-state-snapshots)
091. [How do you use the new @angular/router for dynamic route configuration?](#91-how-do-you-use-the-new-angularrouter-for-dynamic-route-configuration)
092. [How do you use the new @angular/router for guards with async/await?](#92-how-do-you-use-the-new-angularrouter-for-guards-with-asyncawait)
093. [How do you use the new @angular/router for router events with signals?](#93-how-do-you-use-the-new-angularrouter-for-router-events-with-signals)
094. [How do you use the new @angular/router for scroll position restoration?](#94-how-do-you-use-the-new-angularrouter-for-scroll-position-restoration)
095. [How do you use the new @angular/router for custom preloading strategies?](#95-how-do-you-use-the-new-angularrouter-for-custom-preloading-strategies)
096. [How do you use the new @angular/router for router guards with dependency injection?](#96-how-do-you-use-the-new-angularrouter-for-router-guards-with-dependency-injection)
097. [How do you use the new @angular/router for router navigation extras with replaceUrl?](#97-how-do-you-use-the-new-angularrouter-for-router-navigation-extras-with-replaceurl)
098. [How do you use the new @angular/router for router navigation with relativeTo?](#98-how-do-you-use-the-new-angularrouter-for-router-navigation-with-relativeto)
099. [How do you use the new @angular/router for router navigation with skipLocationChange?](#99-how-do-you-use-the-new-angularrouter-for-router-navigation-with-skiplocationchange)
100. [How do you use the new @angular/router for router navigation with state?](#100-how-do-you-use-the-new-angularrouter-for-router-navigation-with-state)

### 💼 Practical Coding Challenges

101. [Build a parent component that passes data to a child component](#101-build-a-parent-component-that-passes-data-to-a-child-component-and-receives-an-event-back-when-an-item-is-selected)
102. [Implement a search input that triggers an API call after debounce](#102-implement-a-search-input-that-triggers-an-api-call-only-after-the-user-stops-typing-for-500ms)
103. [Create a custom directive that changes background color on hover](#103-create-a-custom-directive-that-changes-the-background-color-of-an-element-on-hover)
104. [Build a reactive form with dynamic add/remove fields](#104-build-a-reactive-form-that-allows-users-to-dynamically-add-and-remove-form-fields)
105. [Optimize a component with large list performance issues](#105-optimize-a-component-that-renders-a-large-list-and-is-causing-performance-issues)
106. [Implement role-based route protection using guards](#106-implement-role-based-route-protection-using-angular-route-guards)
107. [Create a reusable HTTP interceptor for API errors](#107-create-a-reusable-http-interceptor-to-handle-api-errors-globally)
108. [Convert an RxJS-based service to use Angular Signals](#108-convert-an-existing-rxjs-based-service-to-use-angular-signals)
109. [Implement lazy loading for feature module or standalone route](#109-implement-lazy-loading-for-a-feature-module-or-standalone-route)
110. [Build a custom pipe to format dates based on user locale](#110-build-a-custom-pipe-to-format-dates-based-on-user-locale)
111. [Create a component using OnPush change detection](#111-create-a-component-using-onpush-change-detection-and-explain-when-it-updates)
112. [Implement pagination and sorting in an Angular data table](#112-implement-pagination-and-sorting-in-an-angular-data-table)
113. [Handle form validation errors from backend API](#113-handle-form-validation-errors-returned-from-a-backend-api)
114. [Implement a shared service for component communication](#114-implement-a-shared-service-to-communicate-between-two-unrelated-components)
115. [Build a debounce-enabled autocomplete dropdown](#115-build-a-debounce-enabled-autocomplete-dropdown)
116. [Implement search + filter + pagination solution end to end](#116-implement-a-search--filter--pagination-solution-end-to-end)
117. [Implement debounce + cancelable HTTP request pattern](#117-implement-a-debounce--cancelable-http-request-pattern)
118. [Build a dynamic menu based on user permissions](#118-build-a-dynamic-menu-based-on-user-permissions)
119. [Create a reusable date picker wrapper component](#119-create-a-reusable-date-picker-wrapper-component)
120. [Implement keyboard accessibility for custom component](#120-implement-keyboard-accessibility-for-a-custom-component)
121. [Handle memory leaks caused by subscriptions and fix them](#121-handle-memory-leaks-caused-by-subscriptions-and-fix-them)
122. [Optimize change detection in dashboard with nested components](#122-optimize-change-detection-in-a-dashboard-with-nested-components)
123. [Implement server-side pagination integration](#123-implement-server-side-pagination-integration-with-angular)
124. [Create a reusable dialog service using Angular Material or CDK](#124-create-a-reusable-dialog-service-using-angular-material-or-cdk)
125. [Implement lazy loading with preloading strategy](#125-implement-lazy-loading-with-preloading-strategy)
126. [Build a shared state service without using NgRx](#126-build-a-shared-state-service-without-using-ngrx)
127. [Refactor component to remove business logic into services](#127-refactor-a-component-to-remove-business-logic-into-services)
128. [Implement internationalization with dynamic language switching](#128-implement-internationalization-i18n-with-dynamic-language-switching)
129. [Build a breadcrumb navigation component based on router state](#129-build-a-breadcrumb-navigation-component-based-on-router-state)
130. [Handle WebSocket or real-time data updates](#130-handle-websocket-or-real-time-data-updates-in-angular)
131. [Implement optimistic UI updates for CRUD operations](#131-implement-optimistic-ui-updates-for-crud-operations)
132. [Create a reusable error boundary–like pattern](#132-create-a-reusable-error-boundarylike-pattern-in-angular)
133. [Build a micro-frontend–ready app using Module Federation](#133-build-a-micro-frontendready-angular-application-using-module-federation)
134. [Implement SSR-friendly code and fix hydration issues](#134-implement-ssr-friendly-code-and-fix-hydration-issues)
135. [Optimize bundle size and analyze build output](#135-optimize-bundle-size-and-analyze-build-output)
136. [Implement a custom preloading strategy for routes](#136-implement-a-custom-preloading-strategy-for-routes)
137. [Handle form-level and field-level validation messages cleanly](#137-handle-form-level-and-field-level-validation-messages-cleanly)
138. [Build a reusable search component with configurable debounce](#138-build-a-reusable-search-component-with-configurable-debounce-time)
139. [Implement polling with proper cleanup](#139-implement-polling-with-proper-cleanup)
140. [Create a directive to auto-focus input fields conditionally](#140-create-a-directive-to-auto-focus-input-fields-conditionally)
141. [Implement drag-and-drop functionality using Angular CDK](#141-implement-drag-and-drop-functionality-using-angular-cdk)
142. [Refactor RxJS-heavy code to Signals-first approach](#142-refactor-rxjs-heavy-code-to-a-signals-first-approach)
143. [Build a role-aware navigation guard with backend validation](#143-build-a-role-aware-navigation-guard-combined-with-backend-validation)

---

# 🟢 Beginner Level (Foundations)

## 1. What is Angular?

**Answer:**  
Angular is a TypeScript-based frontend framework developed by Google for building scalable, single-page applications using components, dependency injection, and reactive programming.

**Example:**

```ts
import { Component } from '@angular/core';
@Component({
  selector: 'app-root',
  template: `<h1>Hello Angular</h1>`
})
export class AppComponent {}
```

---

## 2. What are the main building blocks of Angular?

**Answer:**
* Components
* Templates
* Directives
* Services
* Modules (optional in modern Angular)

**Example:**

```ts
// Component
@Component({ selector: 'app-demo', template: '<div>Demo</div>' })
export class DemoComponent {}

// Directive
@Directive({ selector: '[appHighlight]' })
export class HighlightDirective {}

// Service
@Injectable({ providedIn: 'root' })
export class DataService {}
```

---

## 3. Difference between constructor and ngOnInit?

**Answer:**
* `constructor`: Used for dependency injection
* `ngOnInit`: Used for initialization logic

**Example:**

```ts
constructor(private service: DataService) {}

ngOnInit() {
  this.loadData();
}
```

---

## 4. What is data binding?

**Answer:**
Data binding connects the component class with the template.

Types:
* Interpolation `{{ }}`
* Property binding `[value]`
* Event binding `(click)`
* Two-way binding `[(ngModel)]`

**Example:**

```html
<!-- Interpolation -->
<p>{{ title }}</p>
<!-- Property binding -->
<input [value]="name">
<!-- Event binding -->
<button (click)="onClick()">Click</button>
<!-- Two-way binding -->
<input [(ngModel)]="userInput">
```

---

## 5. What is a directive?

**Answer:**
Directives are classes that modify the behavior or appearance of DOM elements.

**Example:**

```ts
@Directive({ selector: '[appHighlight]' })
export class HighlightDirective {
  @HostListener('mouseenter') onMouseEnter() {
    // highlight logic
  }
}
```

---

# 🟡 Advanced Level (Real Interview Focus)

## 6. What is Change Detection?

**Answer:**  
Change detection is the mechanism Angular uses to synchronize the DOM with application state.

* Default strategy checks entire component tree
* `OnPush` checks only on input changes

**Example:**

```ts
@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `{{ value }}`
})
export class FastComponent {
  @Input() value: string;
}
```

---

## 7. What is RxJS and why is it used?

**Answer:**  
RxJS provides reactive programming using Observables for async operations like HTTP calls and events.

**Example:**

```ts
import { HttpClient } from '@angular/common/http';
import { switchMap } from 'rxjs';

this.http.get('/api/users')
  .pipe(switchMap(res => this.process(res)))
  .subscribe();
```

---

## 8. Difference between Subject and BehaviorSubject?

**Answer:**
* Subject: No initial value
* BehaviorSubject: Requires initial value and emits last value

**Example:**

```ts
import { Subject, BehaviorSubject } from 'rxjs';
const subject = new Subject<number>();
const behavior = new BehaviorSubject<number>(0);

subject.next(1); // Subscribers get 1 only if subscribed before
behavior.next(2); // Subscribers always get latest value (2)
```

---

## 9. What is an HTTP Interceptor?

**Answer:**  
Interceptors allow you to modify HTTP requests and responses globally.

**Example:**

```ts
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const authReq = req.clone({ setHeaders: { Authorization: 'Bearer token' }});
    return next.handle(authReq);
  }
}
```

---

## 10. What are Reactive Forms?

**Answer:**  
Reactive forms provide a model-driven approach for handling form inputs.

**Example:**

```ts
import { FormBuilder, Validators } from '@angular/forms';
constructor(private fb: FormBuilder) {}

this.form = this.fb.group({
  name: ['', Validators.required]
});
```

---

# 🔴 Expert Level (Senior / Lead)

## 11. What are Angular Signals? (Angular 16+)

**Answer:**  
Signals are a new reactive primitive for state management with fine-grained reactivity, introduced in Angular 16 and improved in 17+.

**Example:**

```ts
import { signal } from '@angular/core';
count = signal(0);

increment() {
  this.count.update(v => v + 1);
}
```

---

## 12. Signals vs RxJS – when to use which?

**Answer:**
* Signals: Local UI state, synchronous reactivity
* RxJS: Async streams, HTTP, events, complex flows

**Example:**

```ts
// Signals for local state
count = signal(0);

// RxJS for async
users$ = this.http.get('/api/users');
```

---

## 13. What is OnPush change detection internally?

**Answer:**
`OnPush` triggers change detection only when:
* Input reference changes
* Event occurs
* Observable emits

**Example:**

```ts
@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `{{ data }}`
})
export class OnPushComponent {
  @Input() data: any;
}
```

---

## 14. What is Lazy Loading?

**Answer:**  
Lazy loading loads feature modules or routes only when needed.

**Example:**

```ts
// Standalone route lazy loading (Angular 15+)
{
  path: 'admin',
  loadComponent: () => import('./admin.component').then(m => m.AdminComponent)
}
```

---

## 15. How does Angular prevent XSS?

**Answer:**  
Angular sanitizes untrusted HTML and escapes values automatically.

**Example:**

```ts
@Component({ template: `<div [innerHTML]="userHtml"></div>` })
export class SafeHtmlComponent {
  userHtml = '<img src=x onerror=alert(1) />'; // Will be sanitized
}
```

---

## 16. What is Server-Side Rendering (SSR)?

**Answer:**  
SSR renders Angular apps on the server to improve performance and SEO. Angular 17+ supports hydration for seamless SSR/CSR transitions.

**Example:**

```ts
// main.server.ts
import { provideServerRendering } from '@angular/platform-server';
```

---

## 17. What is Module Federation?

**Answer:**  
Module Federation enables micro-frontends by sharing Angular modules at runtime.

**Example:**

```js
// webpack.config.js
new ModuleFederationPlugin({
    name: 'app1',
    remotes: {
        app2: 'app2@http://localhost:3002/remoteEntry.js'
    },
    shared: ['@angular/core']
})
```

---

## 18. How do you optimize Angular performance?

**Answer:**
* OnPush
* trackBy
* Lazy loading
* Signals
* Avoid heavy pipes
* Use Standalone APIs (Angular 15+)
* Use Control Flow syntax (Angular 17+)

**Example:**

```ts
// OnPush
@Component({ changeDetection: ChangeDetectionStrategy.OnPush })
// trackBy
*ngFor="let item of items; trackBy: trackById"
// Signals
count = signal(0);
```

---

## 19. How do you handle global error handling?

**Answer:**
Using HTTP interceptors and global error handlers.

**Example:**

```ts
@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  handleError(error: any) {
    // Log error
  }
}
```

---

## 20. How do you structure a large Angular application?

**Answer:**
* Feature-based folder structure
* Shared modules
* Smart vs dumb components
* Clear state boundaries
* Use Standalone Components (Angular 15+)

**Example:**

```
/src
  /features
    /users
    /admin
  /shared
  /core
```

---

## 21. What are Standalone Components? (Angular 15+)

**Answer:**
Standalone components can be used without NgModules, simplifying app structure and improving tree-shaking and lazy loading.

**Example:**

```ts
@Component({
  selector: 'app-standalone',
  standalone: true,
  template: `<h1>Standalone!</h1>`
})
export class StandaloneComponent {}
```

---

## 22. What is Control Flow syntax in Angular? (Angular 17+)

**Answer:**
Control Flow syntax ( `@if` , `@for` , `@switch` ) replaces structural directives for more readable and performant templates.

**Example:**

```html
@for (item of items; track item.id) {
<div>{{ item.name }}</div>
}
```

---

## 23. What is hydration in Angular SSR? (Angular 17+)

**Answer:**
Hydration allows Angular to reuse server-rendered HTML on the client, improving SSR performance and SEO.

**Example:**

```ts
// SSR hydration is enabled by default in Angular 17+ when using provideServerRendering
```

---

## 24. What are some new features in Angular 18–21?

**Answer:**
* Enhanced Signals API (Angular 18+)
* Built-in form validation improvements
* Improved SSR and hydration
* More granular lazy loading
* Advanced Control Flow and template type checking
* Deprecation of legacy APIs (NgModules, ViewEngine)

**Example:**

```ts
// Angular 18+ signals
const user = signal({ name: 'Alice', age: 30 });
user.mutate(u => { u.age = 31; });
```

---

## 25. How do you migrate a legacy Angular app to the latest version?

**Answer:**
* Upgrade dependencies step by step
* Refactor to standalone components
* Replace legacy APIs with Signals and new Control Flow
* Use Angular Update Guide for breaking changes

**Example:**

```ts
// Migrate NgModule-based component to standalone
@Component({
  selector: 'app-legacy',
  standalone: true,
  template: `<p>Upgraded!</p>`
})
export class LegacyComponent {}
```

---

## 26. How do you test Signals and Standalone Components?

**Answer:**
* Use TestBed for standalone components
* Use Angular's testing utilities for Signals
* Prefer Jest for fast, isolated tests

**Example:**

```ts
import { TestBed } from '@angular/core/testing';
import { StandaloneComponent } from './standalone.component';

describe('StandaloneComponent', () => {
  it('should create', () => {
    const fixture = TestBed.createComponent(StandaloneComponent);
    expect(fixture.componentInstance).toBeTruthy();
  });
});
```

---

## 27. How do you secure Angular apps (Angular 16+)?

**Answer:**
* Use built-in sanitization
* Avoid direct DOM manipulation
* Use HttpClient with XSRF protection
* Implement route guards and role-based access

**Example:**

```ts
// Route guard
import { CanActivateFn } from '@angular/router';
export const adminGuard: CanActivateFn = () => isAdmin();
```

---

## 28. How do you use typed forms in Angular 17+?

**Answer:**
Typed forms provide type safety for form controls and groups.

**Example:**

```ts
import { FormGroup, FormControl } from '@angular/forms';
interface UserForm {
  name: FormControl<string>;
  age: FormControl<number>;
}
const form = new FormGroup<UserForm>({
  name: new FormControl('', { nonNullable: true }),
  age: new FormControl(0, { nonNullable: true })
});
```

---

## 29. How do you implement micro frontends in Angular 17+?

**Answer:**
* Use Module Federation for runtime sharing
* Prefer standalone components for isolation
* Share common dependencies via Webpack config

**Example:**

```js
// webpack.config.js
new ModuleFederationPlugin({
    name: 'shell',
    remotes: {
        mfe1: 'mfe1@http://localhost:3001/remoteEntry.js'
    },
    shared: ['@angular/core']
})
```

---

## 30. What is the future direction of Angular (v21+)?

**Answer:**
* Signals-first reactivity
* Standalone-first architecture
* Enhanced SSR and hydration
* Improved DX (developer experience) and CLI
* More deprecations of legacy APIs

**Example:**

```ts
// Future: signals-first, standalone-first
@Component({ standalone: true })
export class FutureComponent {
  state = signal({});
}
```

---

## 31. How do you use Angular's new built-in control flow ( `@if` , `@for` , `@switch` ) in templates? (Angular 17+)

**Answer:**
The new control flow syntax provides more readable and performant alternatives to structural directives like `*ngIf` and `*ngFor` .

**Example:**

```html
@for (user of users; track user.id) {
<div>{{ user.name }}</div>
}
@else {
<div>No users found.</div>
}
```

---

## 32. How do you create a custom pipe in Angular?

**Answer:**
A custom pipe transforms data in templates.

**Example:**

```ts
import { Pipe, PipeTransform } from '@angular/core';
@Pipe({ name: 'capitalize' })
export class CapitalizePipe implements PipeTransform {
  transform(value: string) {
    return value.charAt(0).toUpperCase() + value.slice(1);
  }
}
```

---

## 33. How do you use Angular signals in a service for shared state? (Angular 16+)

**Answer:**
Signals can be used in services to share reactive state across components.

**Example:**

```ts
import { Injectable, signal } from '@angular/core';
@Injectable({ providedIn: 'root' })
export class CounterService {
  count = signal(0);
  increment() { this.count.update(c => c + 1); }
}
```

---

## 34. How do you implement route guards using functional APIs? (Angular 15+)

**Answer:**
Functional route guards are simple functions that return a boolean or an Observable/Promise.

**Example:**

```ts
import { CanActivateFn } from '@angular/router';
export const authGuard: CanActivateFn = () => isLoggedIn();
```

---

## 35. How do you use dependency injection in standalone components? (Angular 15+)

**Answer:**
Standalone components support DI just like NgModule-based components.

**Example:**

```ts
@Component({
  selector: 'app-profile',
  standalone: true,
  template: `{{ userService.user() }}`
})
export class ProfileComponent {
  constructor(public userService: UserService) {}
}
```

---

## 36. How do you use the new `provideHttpClient` API? (Angular 15+)

**Answer:**
`provideHttpClient` is used to provide HttpClient in standalone apps.

**Example:**

```ts
import { bootstrapApplication, provideHttpClient } from '@angular/platform-browser';
bootstrapApplication(AppComponent, {
  providers: [provideHttpClient()]
});
```

---

## 37. How do you implement optimistic UI updates with signals?

**Answer:**
Update the UI state immediately, then revert if the server call fails.

**Example:**

```ts
likeCount = signal(0);
likePost() {
  this.likeCount.update(c => c + 1);
  this.api.like().catch(() => this.likeCount.update(c => c - 1));
}
```

---

## 38. How do you use Angular's built-in form validation improvements (Angular 18+)?

**Answer:**
Angular 18+ provides stricter type checking and better error messages for forms.

**Example:**

```ts
form = new FormGroup({
  email: new FormControl('', { validators: [Validators.email], nonNullable: true })
});
```

---

## 39. How do you use the new `@defer` block for lazy rendering? (Angular 17+)

**Answer:**
`@defer` allows you to lazily render parts of the template.

**Example:**

```html
@defer (userLoaded) {
<app-user-profile [user]="user"></app-user-profile>
}
```

---

## 40. How do you use the Angular CLI to generate a standalone component?

**Answer:**
Use the `--standalone` flag with the `ng generate component` command.

**Example:**

```sh
ng generate component my-feature --standalone
```

---

## 41. How do you use the new `inject` function in Angular? (Angular 14+)

**Answer:**
`inject` allows you to inject dependencies outside of constructors.

**Example:**

```ts
import { inject } from '@angular/core';
const myService = inject(MyService);
```

---

## 42. How do you use the new `destroyRef` for cleanup in Angular? (Angular 16+)

**Answer:**
`DestroyRef` allows you to register cleanup logic when a component or directive is destroyed.

**Example:**

```ts
import { DestroyRef, inject } from '@angular/core';
const destroyRef = inject(DestroyRef);
destroyRef.onDestroy(() => console.log('Destroyed!'));
```

---

## 43. How do you use the new `@input` transform for input coercion? (Angular 16+)

**Answer:**
`@Input` transform allows you to coerce or transform input values.

**Example:**

```ts
@Input({ transform: (value: string) => value.trim() })
set name(val: string) { this._name = val; }
```

---

## 44. How do you use the new `@output` transform for output events? (Angular 16+)

**Answer:**
`@Output` transform allows you to transform output event values before emitting.

**Example:**

```ts
@Output({ transform: (val: number) => val * 2 })
valueChange = new EventEmitter<number>();
```

---

## 45. How do you use the new `@model` decorator for two-way binding? (Angular 17+)

**Answer:**
`@model` simplifies two-way binding in standalone components.

**Example:**

```ts
@model() value: string;
```

---

## 46. How do you use the new `@signalEffect` for side effects? (Angular 18+)

**Answer:**
`@signalEffect` runs a function whenever a signal changes.

**Example:**

```ts
import { signal, signalEffect } from '@angular/core';
count = signal(0);
signalEffect(() => console.log(this.count()));
```

---

## 47. How do you use the new `@computed` decorator for derived state? (Angular 17+)

**Answer:**
`@computed` creates a derived signal from other signals.

**Example:**

```ts
import { computed, signal } from '@angular/core';
count = signal(2);
double = computed(() => this.count() * 2);
```

---

## 48. How do you use the new `@injectable` decorator for tree-shakable services? (Angular 16+)

**Answer:**
`@Injectable({ providedIn: 'root' })` makes a service tree-shakable and available app-wide.

**Example:**

```ts
@Injectable({ providedIn: 'root' })
export class ApiService {}
```

---

## 49. How do you use the new `@environmentInjector` for environment-specific providers? (Angular 17+)

**Answer:**
`EnvironmentInjector` allows you to provide dependencies based on the environment.

**Example:**

```ts
import { EnvironmentInjector } from '@angular/core';
const injector = EnvironmentInjector.create([{ provide: API_URL, useValue: 'https://api.example.com' }]);
```

---

## 50. How do you use the new `@hydration` API for SSR hydration? (Angular 17+)

**Answer:**
`@hydration` enables advanced SSR hydration features.

**Example:**

```ts
import { provideHydration } from '@angular/platform-browser';
bootstrapApplication(AppComponent, { providers: [provideHydration()] });
```

---

## 51. How do you use the new `@input` required flag for required inputs? (Angular 16+)

**Answer:**
You can mark an input as required to enforce that a value is always provided.

**Example:**

```ts
@Component({ selector: 'app-user', template: '{{ name }}' })
export class UserComponent {
  @Input({ required: true }) name!: string;
}
```

---

## 52. How do you use the new `@output` required flag for required outputs? (Angular 16+)

**Answer:**
You can mark an output as required to ensure the parent listens to the event.

**Example:**

```ts
@Component({ selector: 'app-btn', template: '<button (click)="clicked.emit()">Click</button>' })
export class BtnComponent {
  @Output({ required: true }) clicked = new EventEmitter<void>();
}
```

---

## 53. How do you use the new `@HostDirectives` decorator? (Angular 16+)

**Answer:**
`@HostDirectives` allows you to attach directives to a host element from a component.

**Example:**

```ts
@Component({
  selector: 'app-card',
  template: '<div>Card</div>',
  hostDirectives: [HighlightDirective]
})
export class CardComponent {}
```

---

## 54. How do you use the new `@Self` , `@SkipSelf` , `@Optional` , and `@Host` DI flags?

**Answer:**
These flags control how Angular resolves dependencies in the injector hierarchy.

**Example:**

```ts
constructor(@Optional() private logger?: LoggerService) {}
```

---

## 55. How do you use the new `@ViewChild` and `@ContentChild` static option?

**Answer:**
The `static` option determines when the query is resolved (before or after change detection).

**Example:**

```ts
@ViewChild('input', { static: true }) input!: ElementRef;
```

---

## 56. How do you use the new `@NgOptimizedImage` directive? (Angular 16+)

**Answer:**
`NgOptimizedImage` optimizes image loading and performance.

**Example:**

```html
<img ngSrc="/assets/photo.jpg" width="400" height="300" />
```

---

## 57. How do you use the new `@NgIf` else block with `@if` ? (Angular 17+)

**Answer:**
The new control flow allows for cleaner else blocks.

**Example:**

```html
@if (isLoggedIn) {
<p>Welcome!</p>
} @else {
<p>Please log in.</p>
}
```

---

## 58. How do you use the new `@NgFor` with track and empty blocks? (Angular 17+)

**Answer:**
`@for` supports `track` and `empty` for better performance and fallback UI.

**Example:**

```html
@for (item of items; track item.id; empty) {
<div>No items found.</div>
}
```

---

## 59. How do you use the new `@NgSwitch` for advanced template branching? (Angular 17+)

**Answer:**
`@switch` provides a more readable alternative to `*ngSwitch` .

**Example:**

```html
@switch (status) {
@case ('loading') { <p>Loading...</p> }
@case ('error') { <p>Error!</p> }
@default { <p>Done!</p> }
}
```

---

## 60. How do you use the new `@NgTemplateOutlet` for dynamic templates?

**Answer:**
`ngTemplateOutlet` allows you to render templates dynamically.

**Example:**

```html
<ng-template #tpl let-name>
    <div>Hello {{ name }}</div>
</ng-template>
<div [ngTemplateOutlet]="tpl" [ngTemplateOutletContext]="{ $implicit: 'Angular' }"></div>
```

---

## 61. How do you use the new `@NgZone` for manual change detection?

**Answer:**
`NgZone` allows you to run code inside or outside Angular's zone for performance tuning.

**Example:**

```ts
constructor(private zone: NgZone) {}
runOutside() {
  this.zone.runOutsideAngular(() => {
    // code here will not trigger change detection
  });
}
```

---

## 62. How do you use the new `@Renderer2` for safe DOM manipulation?

**Answer:**
`Renderer2` provides a safe way to manipulate the DOM.

**Example:**

```ts
constructor(private renderer: Renderer2, private el: ElementRef) {}
ngOnInit() {
  this.renderer.setStyle(this.el.nativeElement, 'color', 'red');
}
```

---

## 63. How do you use the new `@NgPlural` for pluralization?

**Answer:**
`NgPlural` helps display different text based on a numeric value.

**Example:**

```html
<div [ngPlural]="messages.length">
    <ng-template ngPluralCase="=0">No messages</ng-template>
    <ng-template ngPluralCase="=1">One message</ng-template>
    <ng-template ngPluralCase="other">{{ messages.length }} messages</ng-template>
</div>
```

---

## 64. How do you use the new `@NgClass` and `@NgStyle` for dynamic styling?

**Answer:**
`ngClass` and `ngStyle` allow you to set classes and styles dynamically.

**Example:**

```html
<div [ngClass]="{ active: isActive }" [ngStyle]="{ color: color }">Styled Text</div>
```

---

## 65. How do you use the new `@NgContent` for content projection?

**Answer:**
`ng-content` projects content from parent to child components.

**Example:**

```ts
@Component({ selector: 'app-card', template: '<ng-content></ng-content>' })
export class CardComponent {}
```

---

## 66. How do you use the new `@NgModule` for legacy support?

**Answer:**
`NgModule` is still supported for legacy and large-scale apps.

**Example:**

```ts
@NgModule({ declarations: [AppComponent], imports: [BrowserModule], bootstrap: [AppComponent] })
export class AppModule {}
```

---

## 67. How do you use the new `@NgUpgrade` for hybrid AngularJS/Angular apps?

**Answer:**
`NgUpgrade` helps run AngularJS and Angular code together during migration.

**Example:**

```ts
import { UpgradeModule } from '@angular/upgrade/static';
@NgModule({ imports: [UpgradeModule] })
export class HybridAppModule {}
```

---

## 68. How do you use the new `@NgRx` for state management?

**Answer:**
NgRx provides Redux-style state management for Angular apps.

**Example:**

```ts
import { createAction, createReducer, on } from '@ngrx/store';
export const increment = createAction('[Counter] Increment');
export const counterReducer = createReducer(0, on(increment, state => state + 1));
```

---

## 69. How do you use the new `@Ngxs` for state management?

**Answer:**
Ngxs is an alternative state management library for Angular.

**Example:**

```ts
@State<number>({ name: 'count', defaults: 0 })
@Injectable()
export class CountState {
  @Action(Increment)
  increment(ctx: StateContext<number>) {
    ctx.setState(state => state + 1);
  }
}
```

---

## 70. How do you use the new `@ApolloAngular` for GraphQL integration?

**Answer:**
Apollo Angular provides GraphQL client support for Angular apps.

**Example:**

```ts
import { Apollo } from 'apollo-angular';
this.apollo.watchQuery({ query: GET_USERS }).valueChanges.subscribe(result => {
  this.users = result.data;
});
```

---

## 71. How do you use the new `@ngx-translate` for internationalization (i18n)?

**Answer:**
`@ngx-translate` is a popular library for runtime translation in Angular apps.

**Example:**

```ts
import { TranslateService } from '@ngx-translate/core';
constructor(private translate: TranslateService) {
  this.translate.use('en');
}
```

---

## 72. How do you use the new `@angular/localize` for built-in i18n?

**Answer:**
`@angular/localize` provides compile-time translation and localization.

**Example:**

```html
<p i18n="@@welcome">Welcome!</p>
```

---

## 73. How do you use the new `@angular/service-worker` for PWA support?

**Answer:**
Angular's service worker enables Progressive Web App features like offline support.

**Example:**

```sh
ng add @angular/pwa
```

---

## 74. How do you use the new `@angular/animations` for advanced animations?

**Answer:**
Angular animations provide a powerful API for UI transitions.

**Example:**

```ts
import { trigger, state, style, animate, transition } from '@angular/animations';
@Component({
  animations: [
    trigger('openClose', [
      state('open', style({ height: '200px' })),
      state('closed', style({ height: '100px' })),
      transition('open <=> closed', [animate('0.5s')])
    ])
  ]
})
```

---

## 75. How do you use the new `@angular/cdk` for accessibility and utilities?

**Answer:**
The CDK provides utilities for accessibility, overlays, drag-and-drop, and more.

**Example:**

```ts
import { CdkDrag } from '@angular/cdk/drag-drop';
<div cdkDrag>Drag me!</div>
```

---

## 76. How do you use the new `@angular/material` for UI components?

**Answer:**
Angular Material provides a set of pre-built UI components.

**Example:**

```html
<mat-form-field>
    <mat-label>Name</mat-label>
    <input matInput>
</mat-form-field>
```

---

## 77. How do you use the new `@angular/forms` for custom validators?

**Answer:**
Custom validators allow you to add custom validation logic to forms.

**Example:**

```ts
function forbiddenNameValidator(control: AbstractControl) {
  return /admin/.test(control.value) ? { forbidden: true } : null;
}
this.form = this.fb.group({ name: ['', forbiddenNameValidator] });
```

---

## 78. How do you use the new `@angular/router` for lazy loading with standalone components?

**Answer:**
You can lazy load standalone components directly in route configs.

**Example:**

```ts
{
  path: 'profile',
  loadComponent: () => import('./profile.component').then(m => m.ProfileComponent)
}
```

---

## 79. How do you use the new `@angular/router` for route resolvers?

**Answer:**
Resolvers fetch data before activating a route.

**Example:**

```ts
@Injectable({ providedIn: 'root' })
export class UserResolver implements Resolve<User> {
  resolve() { return this.api.getUser(); }
}
{
  path: 'user',
  component: UserComponent,
  resolve: { user: UserResolver }
}
```

---

## 80. How do you use the new `@angular/router` for guards with signals?

**Answer:**
You can use signals in functional guards for reactive route protection.

**Example:**

```ts
import { signal } from '@angular/core';
const isLoggedIn = signal(false);
export const authGuard: CanActivateFn = () => isLoggedIn();
```

---

## 81. How do you use the new `@angular/router` for canMatch and canLoad guards?

**Answer:**
`canMatch` and `canLoad` control access to lazy-loaded routes.

**Example:**

```ts
{
  path: 'admin',
  loadChildren: () => import('./admin.module').then(m => m.AdminModule),
  canLoad: [adminGuard],
  canMatch: [roleGuard]
}
```

---

## 82. How do you use the new `@angular/router` for router events?

**Answer:**
Router events allow you to react to navigation changes.

**Example:**

```ts
this.router.events.subscribe(event => {
  if (event instanceof NavigationEnd) {
    // Do something
  }
});
```

---

## 83. How do you use the new `@angular/router` for router outlets and named outlets?

**Answer:**
Named outlets allow multiple views in a single route.

**Example:**

```html
<router-outlet></router-outlet>
<router-outlet name="sidebar"></router-outlet>
```

---

## 84. How do you use the new `@angular/router` for routerLinkActive and routerLinkActiveOptions?

**Answer:**
These directives help style active links.

**Example:**

```html
<a routerLink="/home" routerLinkActive="active" [routerLinkActiveOptions]="{ exact: true }">Home</a>
```

---

## 85. How do you use the new `@angular/router` for query params and fragments?

**Answer:**
Query params and fragments allow you to pass data in the URL.

**Example:**

```ts
this.router.navigate(['/search'], { queryParams: { q: 'angular' }, fragment: 'top' });
```

---

## 86. How do you use the new `@angular/router` for navigation extras?

**Answer:**
Navigation extras provide additional options for navigation.

**Example:**

```ts
this.router.navigate(['/profile'], { state: { from: 'dashboard' } });
```

---

## 87. How do you use the new `@angular/router` for route reuse strategy?

**Answer:**
Route reuse strategy allows you to control when routes are reused or destroyed.

**Example:**

```ts
export class CustomReuseStrategy implements RouteReuseStrategy {
  shouldReuseRoute(future, curr) { return future.routeConfig === curr.routeConfig; }
}
```

---

## 88. How do you use the new `@angular/router` for preloading strategies?

**Answer:**
Preloading strategies load modules in the background for faster navigation.

**Example:**

```ts
@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })]
})
```

---

## 89. How do you use the new `@angular/router` for custom URL serializers?

**Answer:**
Custom URL serializers allow you to control how URLs are parsed and generated.

**Example:**

```ts
@Injectable()
export class CustomUrlSerializer implements UrlSerializer {
  parse(url: string) { /* custom logic */ }
  serialize(tree: UrlTree) { /* custom logic */ }
}
```

---

## 90. How do you use the new `@angular/router` for router state snapshots?

**Answer:**
Router state snapshots provide a static snapshot of the router state at a moment in time.

**Example:**

```ts
const snapshot = this.router.routerState.snapshot;
```

---

## 91. How do you use the new `@angular/router` for dynamic route configuration?

**Answer:**
You can add or modify routes at runtime for advanced use cases.

**Example:**

```ts
this.router.resetConfig([...this.router.config, { path: 'dynamic', component: DynamicComponent }]);
```

---

## 92. How do you use the new `@angular/router` for guards with async/await?

**Answer:**
Functional guards can return a Promise for async logic.

**Example:**

```ts
export const asyncGuard: CanActivateFn = async () => {
  const allowed = await checkPermission();
  return allowed;
};
```

---

## 93. How do you use the new `@angular/router` for router events with signals?

**Answer:**
You can update signals in response to router events for reactive navigation state.

**Example:**

```ts
import { signal } from '@angular/core';
const currentUrl = signal('');
this.router.events.subscribe(e => {
  if (e instanceof NavigationEnd) currentUrl.set(e.urlAfterRedirects);
});
```

---

## 94. How do you use the new `@angular/router` for scroll position restoration?

**Answer:**
Scroll position restoration helps maintain scroll state between navigations.

**Example:**

```ts
RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' });
```

---

## 95. How do you use the new `@angular/router` for custom preloading strategies?

**Answer:**
Custom preloading strategies allow you to control how and when modules are preloaded.

**Example:**

```ts
@Injectable({ providedIn: 'root' })
export class CustomPreload implements PreloadingStrategy {
  preload(route, fn) { return route.data?.preload ? fn() : of(null); }
}
```

---

## 96. How do you use the new `@angular/router` for router guards with dependency injection?

**Answer:**
You can inject services directly into functional guards.

**Example:**

```ts
export const adminGuard: CanActivateFn = (route, state, injector) => {
  const auth = injector.get(AuthService);
  return auth.isAdmin();
};
```

---

## 97. How do you use the new `@angular/router` for router navigation extras with replaceUrl?

**Answer:**
`replaceUrl` replaces the current history entry instead of adding a new one.

**Example:**

```ts
this.router.navigate(['/home'], { replaceUrl: true });
```

---

## 98. How do you use the new `@angular/router` for router navigation with relativeTo?

**Answer:**
`relativeTo` allows navigation relative to a specific route.

**Example:**

```ts
this.router.navigate(['details'], { relativeTo: this.route });
```

---

## 99. How do you use the new `@angular/router` for router navigation with skipLocationChange?

**Answer:**
`skipLocationChange` navigates without changing the browser URL.

**Example:**

```ts
this.router.navigate(['/hidden'], { skipLocationChange: true });
```

---

## 100. How do you use the new `@angular/router` for router navigation with state?

**Answer:**
You can pass custom state data during navigation.

**Example:**

```ts
this.router.navigate(['/profile'], { state: { referrer: 'dashboard' } });
```

---

## 101. Build a parent component that passes data to a child component and receives an event back when an item is selected.

**Answer:**  
Create a parent component that holds the data and a child component that displays the data and emits an event on selection.

**Example:**

```ts
// parent.component.ts
@Component({
  selector: 'app-parent',
  template: `<app-child [items]="items" (itemSelected)="onItemSelected($event)"></app-child>`
})
export class ParentComponent {
  items = [1, 2, 3];
  onItemSelected(item: number) {
    console.log('Selected item:', item);
  }
}

// child.component.ts
@Component({
  selector: 'app-child',
  template: `<div *ngFor="let item of items" (click)="selectItem(item)">{{ item }}</div>`
})
export class ChildComponent {
  @Input() items: number[];
  @Output() itemSelected = new EventEmitter<number>();
  selectItem(item: number) {
    this.itemSelected.emit(item);
  }
}
```

---

## 102. Implement a search input that triggers an API call only after the user stops typing for 500ms.

**Answer:**  
Use a reactive form with a value change listener, debounce time, and switchMap to handle the API call.

**Example:**

```ts
import { FormControl } from '@angular/forms';
import { debounceTime, switchMap } from 'rxjs';

searchControl = new FormControl();
ngOnInit() {
  this.searchControl.valueChanges.pipe(
    debounceTime(500),
    switchMap(value => this.api.search(value))
  ).subscribe(results => {
    this.results = results;
  });
}
```

---

## 103. Create a custom directive that changes the background color of an element on hover.

**Answer:**  
A directive that listens to mouse enter and leave events to change the background color.

**Example:**

```ts
@Directive({
  selector: '[appHoverColor]'
})
export class HoverColorDirective {
  @HostBinding('style.backgroundColor') backgroundColor: string;

  @HostListener('mouseenter') onMouseEnter() {
    this.backgroundColor = 'yellow';
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.backgroundColor = null;
  }
}
```

---

## 104. Build a reactive form that allows users to dynamically add and remove form fields.

**Answer:**  
A form that uses FormArray to manage a dynamic list of form controls.

**Example:**

```ts
import { FormBuilder, FormArray } from '@angular/forms';
constructor(private fb: FormBuilder) {}

form = this.fb.group({
  items: this.fb.array([])
});

get items() {
  return this.form.get('items') as FormArray;
}

addItem() {
  this.items.push(this.fb.control(''));
}

removeItem(index: number) {
  this.items.removeAt(index);
}
```

---

## 105. Optimize a component that renders a large list and is causing performance issues.

**Answer:**  
Use trackBy with ngFor to optimize rendering of large lists.

**Example:**

```ts
@Component({
  template: `<div *ngFor="let item of items; trackBy: trackById">{{ item.name }}</div>`
})
export class LargeListComponent {
  items = this.getLargeList();
  trackById(index: number, item: Item) {
    return item.id;
  }
}
```

---

## 106. Implement role-based route protection using Angular route guards.

**Answer:**  
A guard that checks the user's role before activating a route.

**Example:**

```ts
@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const expectedRole = route.data.expectedRole;
    const userRole = this.authService.getUserRole();
    return userRole === expectedRole;
  }
}
```

---

## 107. Create a reusable HTTP interceptor to handle API errors globally.

**Answer:**  
An interceptor that catches HTTP errors and handles them globally.

**Example:**

```ts
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    return next.handle(req).pipe(
      catchError(err => {
        // Handle error
        return throwError(err);
      })
    );
  }
}
```

---

## 108. Convert an existing RxJS-based service to use Angular Signals.

**Answer:**  
Refactor the service to use signals for state management instead of RxJS subjects/observables.

**Example:**

```ts
import { Injectable, signal } from '@angular/core';
@Injectable({ providedIn: 'root' })
export class UserService {
  private users = signal<User[]>([]);
  getUsers() {
    return this.users.asReadonly();
  }
  setUsers(users: User[]) {
    this.users.set(users);
  }
}
```

---

## 109. Implement lazy loading for a feature module or standalone route.

**Answer:**  
Configure the router to load a module or component lazily.

**Example:**

```ts
// For a feature module
{
  path: 'admin',
  loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
}

// For a standalone component (Angular 15+)
{
  path: 'profile',
  loadComponent: () => import('./profile.component').then(m => m.ProfileComponent)
}
```

---

## 110. Build a custom pipe to format dates based on user locale.

**Answer:**  
A pipe that formats a date value according to the user's locale.

**Example:**

```ts
import { Pipe, PipeTransform } from '@angular/core';
@Pipe({ name: 'localDate' })
export class LocalDatePipe implements PipeTransform {
  transform(value: Date | string, locale: string = 'en-US'): string {
    return new Intl.DateTimeFormat(locale).format(new Date(value));
  }
}
```

---

## 111. Create a component using OnPush change detection and explain when it updates.

**Answer:**  
A component that uses OnPush strategy and only updates when its inputs change by reference.

**Example:**

```ts
@Component({
  selector: 'app-on-push',
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `{{ data.name }}`
})
export class OnPushComponent {
  @Input() data!: { name: string };
}
```

---

## 112. Implement pagination and sorting in an Angular data table.

**Answer:**  
A data table that supports pagination and sorting of data.

**Example:**

```ts
@Component({
  template: `
    <table>
      <tr>
        <th (click)="sort('name')">Name</th>
        <th (click)="sort('age')">Age</th>
      </tr>
      <tr *ngFor="let user of paginatedData">
        <td>{{ user.name }}</td>
        <td>{{ user.age }}</td>
      </tr>
    </table>
    <button (click)="prevPage()">Previous</button>
    <button (click)="nextPage()">Next</button>
  `
})
export class DataTableComponent {
  data = this.getUsers();
  page = 1;
  pageSize = 10;
  sortedBy: string | null = null;
  sortOrder: 'asc' | 'desc' | null = null;

  get paginatedData() {
    // Return sorted and paginated data
  }

  sort(column: string) {
    // Sort data and toggle sort order
  }

  nextPage() {
    this.page++;
  }

  prevPage() {
    this.page--;
  }
}
```

---

## 113. Handle form validation errors returned from a backend API.

**Answer:**  
A form that displays backend validation errors to the user.

**Example:**

```ts
this.form = this.fb.group({
  username: ['', Validators.required],
  password: ['', Validators.required]
});

this.form.setErrors({ backend: 'Invalid credentials' });
```

---

## 114. Implement a shared service to communicate between two unrelated components.

**Answer:**  
A service that uses a subject to allow communication between components.

**Example:**

```ts
@Injectable({ providedIn: 'root' })
export class SharedService {
  private messageSource = new Subject<string>();
  message$ = this.messageSource.asObservable();

  sendMessage(message: string) {
    this.messageSource.next(message);
  }
}
```

---

## 115. Build a debounce-enabled autocomplete dropdown.

**Answer:**  
An autocomplete input that waits for the user to stop typing for a specified time before sending the request.

**Example:**

```ts
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime, switchMap } from 'rxjs';

@Component({
  selector: 'app-autocomplete',
  template: `<input [formControl]="searchControl">`
})
export class AutocompleteComponent implements OnInit {
  searchControl = new FormControl();

  ngOnInit() {
    this.searchControl.valueChanges.pipe(
      debounceTime(300),
      switchMap(value => this.search(value))
    ).subscribe(results => {
      // Handle results
    });
  }

  search(term: string) {
    // Return an observable of search results
  }
}
```

---

## 116. Implement a search + filter + pagination solution end to end.

**Answer:**  
A complete solution that includes searching, filtering, and paginating data.

**Example:**

```ts
@Component({
  template: `
    <input [(ngModel)]="searchTerm" placeholder="Search">
    <select [(ngModel)]="selectedCategory">
      <option *ngFor="let category of categories" [value]="category">{{ category }}</option>
    </select>
    <table>
      <tr *ngFor="let item of filteredAndSortedData">
        <td>{{ item.name }}</td>
        <td>{{ item.category }}</td>
      </tr>
    </table>
    <pagination [totalItems]="totalItems" (pageChange)="onPageChange($event)"></pagination>
  `
})
export class SearchFilterPaginationComponent {
  searchTerm = '';
  selectedCategory = '';
  data = this.getData();
  categories = this.getCategories();
  page = 1;
  pageSize = 10;

  get filteredAndSortedData() {
    // Return filtered, sorted, and paginated data
  }

  onPageChange(page: number) {
    this.page = page;
  }
}
```

---

## 117. Implement a debounce + cancelable HTTP request pattern.

**Answer:**  
A pattern that cancels the previous HTTP request if a new one is made within the debounce time.

**Example:**

```ts
import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { debounceTime, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-debounce-http',
  template: `<input (input)="onSearch($event.target.value)">`
})
export class DebounceHttpComponent implements OnDestroy {
  private searchTerms = new Subject<string>();
  private subscription: Subscription;

  constructor(private http: HttpClient) {
    this.subscription = this.searchTerms.pipe(
      debounceTime(300),
      switchMap(term => this.http.get(`/api/search?q=${term}`))
    ).subscribe();
  }

  onSearch(term: string) {
    this.searchTerms.next(term);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
```

---

## 118. Build a dynamic menu based on user permissions.

**Answer:**  
A menu that shows or hides items based on the user's permissions.

**Example:**

```ts
@Component({
  template: `
    <ul>
      <li *ngFor="let item of menuItems" *ngIf="hasPermission(item.permission)">
        {{ item.label }}
      </li>
    </ul>
  `
})
export class MenuComponent {
  menuItems = [
    { label: 'Admin', permission: 'admin' },
    { label: 'User', permission: 'user' }
  ];

  hasPermission(permission: string) {
    const userPermissions = this.authService.getUserPermissions();
    return userPermissions.includes(permission);
  }
}
```

---

## 119. Create a reusable date picker wrapper component.

**Answer:**  
A component that wraps a date picker library and provides a consistent API and style.

**Example:**

```ts
@Component({
  selector: 'app-date-picker',
  template: `<input [ngxDatePicker]="options">`
})
export class DatePickerComponent {
  options = {
    // Date picker options
  };
}
```

---

## 120. Implement keyboard accessibility for a custom component.

**Answer:**  
Add keyboard navigation and accessibility attributes to a component.

**Example:**

```ts
@Component({
  selector: 'app-accessible',
  template: `<div tabindex="0" (keydown)="onKeydown($event)">...</div>`
})
export class AccessibleComponent {
  onKeydown(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this.doAction();
    }
  }
}
```

---

## 121. Handle memory leaks caused by subscriptions and fix them.

**Answer:**  
Identify and unsubscribe from subscriptions to prevent memory leaks.

**Example:**

```ts
@Component({
  template: `...`
})
export class SomeComponent implements OnDestroy {
  private subscription: Subscription;

  ngOnInit() {
    this.subscription = this.service.getData().subscribe();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
```

---

## 122. Optimize change detection in a dashboard with nested components.

**Answer:**  
Use OnPush change detection strategy and immutable data structures to optimize performance.

**Example:**

```ts
@Component({
  selector: 'app-dashboard',
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `...`
})
export class DashboardComponent {
  @Input() data!: DashboardData;
}
```

---

## 123. Implement server-side pagination integration with Angular.

**Answer:**  
A solution that fetches only the required page of data from the server.

**Example:**

```ts
@Component({
  template: `
    <table>
      <tr *ngFor="let item of data">
        <td>{{ item.name }}</td>
      </tr>
    </table>
    <pagination [totalItems]="totalItems" (pageChange)="loadPage($event)"></pagination>
  `
})
export class ServerSidePaginationComponent {
  data: Item[] = [];
  totalItems = 0;

  loadPage(page: number) {
    this.http.get(`/api/items?page=${page}`).subscribe(response => {
      this.data = response.items;
      this.totalItems = response.total;
    });
  }
}
```

---

## 124. Create a reusable dialog service using Angular Material or CDK.

**Answer:**  
A service that opens a dialog and returns an observable for the result.

**Example:**

```ts
@Injectable({ providedIn: 'root' })
export class DialogService {
  constructor(private dialog: MatDialog) {}

  openConfirmDialog(data: ConfirmDialogData): Observable<boolean> {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data
    });
    return dialogRef.afterClosed();
  }
}
```

---

## 125. Implement lazy loading with preloading strategy.

**Answer:**  
Configure the router to lazy load a module and use a preloading strategy to load it in the background.

**Example:**

```ts
@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: CustomPreloadStrategy })]
})
export class AppRoutingModule {}
```

---

## 126. Build a shared state service without using NgRx.

**Answer:**  
A service that holds the application state and provides methods to update and select state.

**Example:**

```ts
@Injectable({ providedIn: 'root' })
export class StateService {
  private state = {
    user: null,
    settings: {}
  };

  setUser(user: User) {
    this.state.user = user;
  }

  getUser() {
    return this.state.user;
  }
}
```

---

## 127. Refactor a component to remove business logic into services.

**Answer:**  
Move the business logic from the component to a dedicated service.

**Example:**

```ts
@Component({
  template: `...`
})
export class SomeComponent {
  constructor(private dataService: DataService) {}

  saveData(data: Data) {
    this.dataService.save(data).subscribe();
  }
}
```

---

## 128. Implement internationalization (i18n) with dynamic language switching.

**Answer:**  
A solution that loads translations and updates the application language at runtime.

**Example:**

```ts
import { TranslateService } from '@ngx-translate/core';
constructor(private translate: TranslateService) {
  this.translate.setDefaultLang('en');
}

switchLanguage(lang: string) {
  this.translate.use(lang);
}
```

---

## 129. Build a breadcrumb navigation component based on router state.

**Answer:**  
A component that generates breadcrumbs from the activated route snapshot.

**Example:**

```ts
@Component({
  template: `<nav><ng-container *ngFor="let crumb of breadcrumbs">{{ crumb.label }}</ng-container></nav>`
})
export class BreadcrumbsComponent {
  breadcrumbs: Breadcrumb[] = [];

  constructor(private router: Router) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      map(() => this.createBreadcrumbs(this.router.routerState.snapshot.root))
    ).subscribe(crumbs => {
      this.breadcrumbs = crumbs;
    });
  }

  private createBreadcrumbs(route: ActivatedRouteSnapshot, url: string = '', breadcrumbs: Breadcrumb[] = []): Breadcrumb[] {
    const children: ActivatedRouteSnapshot[] = route.children;
    if (children.length === 0) {
      return breadcrumbs;
    }

    for (const child of children) {
      const routeURL: string = child.url.map(segment => segment.path).join('/');
      if (routeURL !== '') {
        url += `/${routeURL}`;
      }

      breadcrumbs.push({ label: routeURL, url });

      return this.createBreadcrumbs(child, url, breadcrumbs);
    }

    return breadcrumbs;
  }
}
```

---

## 130. Handle WebSocket or real-time data updates in Angular.

**Answer:**  
A service that manages a WebSocket connection and provides real-time data to components.

**Example:**

```ts
@Injectable({ providedIn: 'root' })
export class WebSocketService {
  private subject: Subject<MessageEvent>;

  connect(url: string): Subject<MessageEvent> {
    if (!this.subject) {
      this.subject = this.create(url);
    }
    return this.subject;
  }

  private create(url: string): Subject<MessageEvent> {
    const ws = new WebSocket(url);
    const observable = new Observable<MessageEvent>(obs => {
      ws.onmessage = obs.next.bind(obs);
      ws.onerror = obs.error.bind(obs);
      ws.onclose = obs.complete.bind(obs);
      return () => ws.close();
    });
    const observer = {
      next: (data: object) => {
        if (ws.readyState === WebSocket.OPEN) {
          ws.send(JSON.stringify(data));
        }
      }
    };
    return Subject.create(observer, observable);
  }
}
```

---

## 131. Implement optimistic UI updates for CRUD operations.

**Answer:**  
Update the UI immediately and revert if the server operation fails.

**Example:**

```ts
updateItem(item: Item) {
  this.items = this.items.map(i => i.id === item.id ? item : i);
  this.http.updateItem(item).subscribe({
    next: () => {},
    error: () => {
      // Revert change on error
      this.items = this.items.map(i => i.id === item.id ? { ...i, ...item.original } : i);
    }
  });
}
```

---

## 132. Create a reusable error boundary–like pattern in Angular.

**Answer:**  
A component that catches errors in its subtree and displays a fallback UI.

**Example:**

```ts
@Component({
  template: `
    <ng-container *ngIf="!hasError; else errorTemplate">
      <ng-content></ng-content>
    </ng-container>
    <ng-template #errorTemplate>
      <div class="error">Something went wrong.</div>
    </ng-template>
  `
})
export class ErrorBoundaryComponent {
  hasError = false;

  ngOnInit() {
    // Reset error state
    this.hasError = false;
  }

  ngOnDestroy() {
    // Cleanup
  }

  handleError() {
    this.hasError = true;
  }
}
```

---

## 133. Build a micro-frontend–ready Angular application using Module Federation.

**Answer:**  
Configure Module Federation to share Angular modules between micro-frontends.

**Example:**

```js
// webpack.config.js
new ModuleFederationPlugin({
    name: 'app1',
    remotes: {
        app2: 'app2@http://localhost:3002/remoteEntry.js',
    },
    shared: {
        '@angular/core': {
            singleton: true,
            strictVersion: true
        },
        '@angular/common': {
            singleton: true,
            strictVersion: true
        },
    }
})
```

---

## 134. Implement SSR-friendly code and fix hydration issues.

**Answer:**  
Ensure the app can render on the server and client without differences.

**Example:**

```ts
// Avoid browser-only APIs during module initialization
if (isPlatformBrowser(this.platformId)) {
  // Browser-specific code
}
```

---

## 135. Optimize bundle size and analyze build output.

**Answer:**  
Use Angular CLI built-in tools to analyze and optimize the bundle size.

**Example:**

```sh
ng build --prod --source-map
ng run app-name:stats-json
```

---

## 136. Implement a custom preloading strategy for routes.

**Answer:**  
A preloading strategy that loads specific modules based on custom logic.

**Example:**

```ts
@Injectable({ providedIn: 'root' })
export class CustomPreloadStrategy implements PreloadingStrategy {
  preload(route: Route, fn: () => Observable<any>): Observable<any> {
    return route.data && route.data.preload ? fn() : of(null);
  }
}
```

---

## 137. Handle form-level and field-level validation messages cleanly.

**Answer:**  
A solution that displays validation messages for each form field and the form as a whole.

**Example:**

```ts
@Component({
  template: `
    <form [formGroup]="form">
      <input formControlName="username">
      <div *ngIf="form.get('username').invalid && (form.get('username').dirty || form.get('username').touched)">
        Username is required.
      </div>
    </form>
  `
})
export class UserFormComponent {
  form = this.fb.group({
    username: ['', Validators.required]
  });
}
```

---

## 138. Build a reusable search component with configurable debounce time.

**Answer:**  
A search component that emits the search term after a configurable debounce time.

**Example:**

```ts
@Component({
  selector: 'app-search',
  template: `<input (input)="onInput($event.target.value)">`
})
export class SearchComponent {
  @Output() search = new EventEmitter<string>();
  private debounceTimer!: ReturnType<typeof setTimeout>;

  onInput(value: string) {
    clearTimeout(this.debounceTimer);
    this.debounceTimer = setTimeout(() => {
      this.search.emit(value);
    }, 300);
  }
}
```

---

## 139. Implement polling with proper cleanup.

**Answer:**  
A service that polls a URL at a specified interval and cleans up on unsubscribe.

**Example:**

```ts
@Injectable({ providedIn: 'root' })
export class PollingService {
  private intervalId!: ReturnType<typeof setInterval>;

  startPolling(url: string, interval: number) {
    this.intervalId = setInterval(() => {
      this.http.get(url).subscribe();
    }, interval);
  }

  stopPolling() {
    clearInterval(this.intervalId);
  }
}
```

---

## 140. Create a directive to auto-focus input fields conditionally.

**Answer:**  
A directive that focuses the host element based on a condition.

**Example:**

```ts
@Directive({
  selector: '[appAutoFocus]'
})
export class AutoFocusDirective {
  @Input() set appAutoFocus(condition: boolean) {
    if (condition) {
      this.el.nativeElement.focus();
    }
  }

  constructor(private el: ElementRef) {}
}
```

---

## 141. Implement drag-and-drop functionality using Angular CDK.

**Answer:**  
Use Angular CDK's drag-and-drop module to enable dragging and dropping of elements.

**Example:**

```ts
import { DragDropModule } from '@angular/cdk/drag-drop';
@NgModule({
  imports: [DragDropModule]
})
export class AppModule {}
```

---

## 142. Refactor RxJS-heavy code to a Signals-first approach.

**Answer:**  
Rewrite the code to use Angular Signals for state management and reactivity.

**Example:**

```ts
import { signal } from '@angular/core';
count = signal(0);
increment() {
  this.count.update(c => c + 1);
}
```

---

## 143. Build a role-aware navigation guard combined with backend validation.

**Answer:**  
A guard that checks the user's role and validates it with the backend before activating a route.

**Example:**

```ts
@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    const expectedRole = route.data.expectedRole;
    return this.authService.getUserRole().pipe(
      take(1),
      map(role => {
        const isValid = role === expectedRole;
        if (!isValid) {
          this.router.navigate(['/forbidden']);
        }
        return isValid;
      })
    );
  }
}
```

---

# 🎉 Congratulations!

You now have 143 Angular interview questions and answers (with code examples) covering Angular 16–21+ and all major enterprise, architecture, and real-world topics.

Use this as a daily revision and interview prep resource. Good luck!
