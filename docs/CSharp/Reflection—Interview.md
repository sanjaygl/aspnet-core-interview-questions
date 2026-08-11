# Reflection — Interview Q&A

---

### Q1. What is reflection in C#?

**Answer:**
"Reflection is the ability to inspect and interact with types, methods, properties, and assemblies at runtime — even ones you didn't know about at compile time. It lives in the `System.Reflection` namespace. Instead of hardcoding `new Customer()` and calling `customer.GetName()`, I can ask an object what type it is, what properties/methods it has, and invoke them dynamically."

```csharp
Customer c = new Customer { Name = "John" };

Type type = c.GetType();
Console.WriteLine(type.Name); // "Customer"

foreach (var prop in type.GetProperties())
    Console.WriteLine(prop.Name); // lists all property names
```

**Where to use:** anytime you need to work with a type without knowing it ahead of time — serializers, DI containers, ORMs, plugin systems, attribute-driven validation.

---

### Q2. What's the difference between `GetType()` and `typeof()`?

**Answer:**
"`typeof()` is resolved at compile time and needs the type name written in code — `typeof(Customer)`. `GetType()` is called on an instance and resolves at runtime — it returns the *actual* runtime type, which matters with inheritance/polymorphism."

```csharp
Type t1 = typeof(Customer);       // compile-time, needs the type name

Animal a = new Dog();
Type t2 = a.GetType();            // runtime — returns "Dog", not "Animal"
Type t3 = typeof(Animal);         // always "Animal", regardless of what's assigned
```

**Where to use:** `typeof()` when you know the exact type at compile time (e.g., attribute checks, generic constraints). `GetType()` when you need the actual runtime type of an object reference that could be a subclass.

---

### Q3. How do you create an instance of a type dynamically, without using `new`?

**Answer:**
"With `Activator.CreateInstance`. This is how DI containers and ORMs create objects when they only know the type as a `Type` object or a string, not at compile time."

```csharp
Type type = Type.GetType("MyApp.Customer");
object instance = Activator.CreateInstance(type);

// or from an already-known Type
var customer = (Customer)Activator.CreateInstance(typeof(Customer));
```

**Where to use:** dependency injection containers resolving a service type, ORMs materializing an entity from a database row, plugin loaders instantiating a class discovered from an assembly.

---

### Q4. How do you call a method dynamically using reflection?

**Answer:**
"I get the `MethodInfo` from the type, then call `Invoke`, passing the target instance and arguments."

```csharp
Type type = customer.GetType();
MethodInfo method = type.GetMethod("SayHello");
method.Invoke(customer, null); // no arguments

MethodInfo greet = type.GetMethod("Greet");
greet.Invoke(customer, new object[] { "Hi there" }); // with 1 argument
```

**Where to use:** frameworks that need to call a method they only know the name of at runtime — e.g., an MVC framework calling `HomeController.Index()` based on a route string.

---

### Q5. How do you read or set a property value dynamically?

**Answer:**
"Through `PropertyInfo.GetValue` and `SetValue`. This is exactly how JSON serializers like `System.Text.Json` or `Newtonsoft.Json` work under the hood — they don't know your class ahead of time, so they use reflection to read/write each property."

```csharp
PropertyInfo prop = typeof(Customer).GetProperty("Name");

string name = (string)prop.GetValue(customer); // read
prop.SetValue(customer, "Jane");                // write
```

**Where to use:** generic serialization/deserialization, object-to-object mapping (AutoMapper-style), building generic "dump all properties" debug/logging helpers.

---

### Q6. How does reflection relate to custom attributes?

**Answer:**
"Attributes by themselves do nothing — they're just metadata sitting on a class/method/property. Reflection is what actually reads that metadata at runtime and acts on it. That's how things like `[Required]` validation attributes or `[HttpGet]` routing attributes work — some framework code uses reflection to find the attribute and decide what to do."

```csharp
public class Customer
{
    [Required]
    public string Name { get; set; }
}

PropertyInfo prop = typeof(Customer).GetProperty("Name");
bool isRequired = prop.GetCustomAttributes(typeof(RequiredAttribute), false).Any();
```

**Where to use:** validation frameworks, ORMs reading `[Column]`/`[Table]` attributes, routing in ASP.NET Core reading `[HttpGet]`/`[Route]`.

---

### Q7. What's the downside of using reflection?

**Answer:**
"Performance — reflection is much slower than direct code because it involves runtime type lookups instead of a compiled, direct call. It also bypasses compile-time type safety, so mistakes (wrong property name, wrong argument type) only show up as runtime exceptions, not compiler errors. I'd avoid it in hot paths and only use it where the flexibility is actually needed — like framework/library code, not everyday business logic."

**Where to use / avoid:**
- Use it: cross-cutting framework code (serializers, DI, ORMs, plugin systems) where you genuinely don't know the type ahead of time.
- Avoid it: performance-critical loops, or anywhere a normal interface/generic could do the same job at compile time.

---

### Q8. How would you list all classes in an assembly, or find all classes implementing an interface?

**Answer:**
"I'd load the `Assembly` and call `GetTypes()`, then filter using LINQ. This is how plugin systems discover implementations at startup — they scan an assembly for any type that implements a known interface and register it."

```csharp
Assembly assembly = Assembly.GetExecutingAssembly();

var pluginTypes = assembly.GetTypes()
    .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface);

foreach (var type in pluginTypes)
{
    var plugin = (IPlugin)Activator.CreateInstance(type);
    plugin.Run();
}
```

**Where to use:** plugin/extension architectures, auto-registering services with a DI container by convention, unit test runners discovering test classes/methods.

---

### Q9. What's the difference between reflection and the `dynamic` keyword?

**Answer:**
"`dynamic` skips compile-time type checking and resolves member access at runtime using the DLR (Dynamic Language Runtime) — but it still needs the actual method/property to exist on the object at runtime, and under the hood it often uses reflection-like binding. Reflection is more explicit and lower-level — I directly ask for a `Type`, `MethodInfo`, `PropertyInfo` and invoke them myself, with full control over error handling. `dynamic` is more convenient syntax; reflection is more powerful/explicit but more verbose."

```csharp
dynamic d = customer;
d.SayHello(); // resolved at runtime, looks like normal method call syntax

// vs
typeof(Customer).GetMethod("SayHello").Invoke(customer, null); // explicit reflection
```

---

### Quick one-liner if asked to summarize

> "Reflection lets you inspect and use types, methods, properties, and attributes at runtime instead of at compile time. It's what makes serializers, DI containers, ORMs, and attribute-based validation possible — but it's slower and less type-safe than direct code, so it's best kept to framework-level code, not everyday business logic."
 