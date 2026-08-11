# Extension Methods — Interview Q&A

---

### Q1. What is an extension method?

**Answer:**
"An extension method lets you add a new method to an existing type without modifying its source code, without subclassing it, and without recompiling it. It's a `static` method in a `static` class, where the first parameter is prefixed with `this` — that first parameter is the type you're 'extending'. You then call it like it was an instance method on that type."

```csharp
public static class StringExtensions
{
    public static bool IsNullOrEmptyTrimmed(this string value)
    {
        return string.IsNullOrEmpty(value?.Trim());
    }
}

// called like a normal instance method:
string name = "  ";
bool empty = name.IsNullOrEmptyTrimmed(); // true
```

**Where to use:** adding convenience methods to types you don't own (BCL types like `string`, `IEnumerable<T>`, or types from a third-party/legacy library), or organizing cross-cutting helper logic without a giant static "Utils" class full of `Utils.DoSomething(x)` calls.

---

### Q2. What are the rules for writing one?

**Answer:**
"It has to be a `static` method, inside a `static` class, and the first parameter needs the `this` keyword in front of the type it's extending. Everything else about it is a normal static method — it can take more parameters after the `this` one, be generic, return anything."

```csharp
public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    {
        return source == null || !source.Any();
    }
}

List<int> numbers = null;
numbers.IsNullOrEmpty(); // true — even works on a null reference, since it's really just a static call
```

**Where this comes up as a trick question:** calling an extension method on a `null` reference doesn't throw a `NullReferenceException` — because under the hood it's just `EnumerableExtensions.IsNullOrEmpty(numbers)`, a normal static method call with `numbers` passed as an argument.

---

### Q3. How does the compiler actually resolve `myString.IsNullOrEmptyTrimmed()`?

**Answer:**
"It's syntactic sugar. The compiler rewrites `name.IsNullOrEmptyTrimmed()` into `StringExtensions.IsNullOrEmptyTrimmed(name)`. That's also why extension methods never override instance methods — if the type already has a method with a matching signature, the real instance method always wins; the extension method is only used as a fallback when no matching instance method exists."

---

### Q4. Where have you actually implemented extension methods in a real project?

**Answer:**
"In the Party service, there's `ApiControllerExtensions` in `WebAPI/Common` — it adds a `ReturnResponse<T>()` method onto `ApiController` (a Web API framework type we don't own) so every controller can build a properly content-negotiated `HttpResponseMessage` with one call instead of repeating that negotiation/formatting logic in every action method."

```csharp
namespace System.Web.Http
{
    public static class ApiControllerExtensions
    {
        public static HttpResponseMessage ReturnResponse<T>(this ApiController apiController, object responseObject)
        {
            if (responseObject == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                throw new HttpResponseException(response);
            }

            var negotiator = apiController.Configuration.Services.GetContentNegotiator();
            var result = negotiator.Negotiate(typeof(T), apiController.Request, apiController.Configuration.Formatters);

            return new HttpResponseMessage
            {
                Content = new ObjectContent<T>((T)Convert.ChangeType(responseObject, typeof(T)),
                    result.Formatter, result.MediaType.MediaType)
            };
        }
    }
}

// used in a controller action like this:
public HttpResponseMessage Get(int id)
{
    var party = _partyService.GetById(id);
    return this.ReturnResponse<PartyDto>(party); // extension method on ApiController
}
```

"Same codebase also has `SwaggerExtensions`, `TelemetryExtensions`, `AuthorizationExtensions` in `WebAPI/Middlewares` — they all follow the same pattern: extending `IAppBuilder`/`HttpConfiguration` (framework types) with a single `app.UseSwagger()`-style call that hides multi-step setup logic, which is the standard ASP.NET convention for middleware registration."

**Where to use this pattern:** whenever you're configuring or extending a framework type you don't own (`ApiController`, `IAppBuilder`, `HttpConfiguration`) and want a clean, chainable, one-line call at the composition root instead of repeating setup logic everywhere.

---

### Q5. Why not just write a regular static helper method instead?

**Answer:**
"You could — functionally `StringExtensions.IsNullOrEmptyTrimmed(name)` and `name.IsNullOrEmptyTrimmed()` do the exact same thing. The extension method version reads more naturally at the call site, especially when chaining several operations together, like LINQ does — `list.Where(...).Select(...).OrderBy(...)` is only readable because each of those is an extension method called fluently on the previous result. A static helper would force you to write nested calls instead: `OrderBy(Select(Where(list, ...), ...), ...)`."

```csharp
// Fluent, extension-method style — reads left to right
var result = orders.Where(o => o.Total > 100).OrderBy(o => o.Date).ToList();

// Equivalent with static methods — much harder to read
var result2 = Enumerable.ToList(Enumerable.OrderBy(Enumerable.Where(orders, o => o.Total > 100), o => o.Date));
```

---

### Q6. What are the downsides / things to watch out for?

**Answer:**
"They can hurt discoverability — a method 'added' to a type isn't visible just by looking at that type's own class definition, only by knowing the extension class exists and its namespace is imported (`using`). They also can't access private members of the type they extend, since they're not really part of that class — just static methods dressed up to look like instance methods. And if two extension methods with the same signature exist in different imported namespaces, you get an ambiguity error."

**Where to be careful:** keep extension methods in a clearly named, discoverable static class (e.g., `StringExtensions`, `ApiControllerExtensions`) and don't overuse them for logic that really belongs as a proper method on a type you actually own.

---

### Quick one-liner if asked to summarize

> "An extension method is a static method in a static class with `this` on the first parameter, letting you call it like an instance method on a type you don't own — used heavily by LINQ (`Where`, `Select`, etc.) and, in our own codebase, things like `ApiControllerExtensions.ReturnResponse<T>()` in Party to add reusable behavior onto framework types without subclassing them."
 