# Indexers — Interview Q&A

---

### Q1. What is an indexer in C#?

**Answer:**
"An indexer lets an object be accessed using array-like square-bracket syntax — `obj[0]` or `obj["key"]` — instead of a method call. It's defined with the `this` keyword and a parameter in square brackets, and internally it's just a specially-named property with `get`/`set` accessors. It's how types like `List<T>`, `Dictionary<TKey,TValue>`, and `string` let you write `list[0]` or `dict["key"]` instead of `list.GetItem(0)`."

```csharp
public class OrderCollection
{
    private List<Order> _orders = new List<Order>();

    public Order this[int index]
    {
        get => _orders[index];
        set => _orders[index] = value;
    }
}

var orders = new OrderCollection();
orders[0] = new Order();      // calls the set accessor
Order first = orders[0];      // calls the get accessor
```

**Where to use:** any custom collection-like or wrapper class where accessing an item "by position" or "by key" reads more naturally than a `GetX()`/`SetX()` method pair.

---

### Q2. How is an indexer different from a normal property?

**Answer:**
"A property has a fixed name and no parameters — `Name { get; set; }`. An indexer has no name of its own (it's always `this[...]`), but it takes one or more parameters inside the brackets, which is what makes it usable like an array. You can also overload indexers with different parameter types, which you can't do with a regular property."

```csharp
public class Matrix
{
    private double[,] _data = new double[10, 10];

    // indexer with two parameters — like a 2D array
    public double this[int row, int col]
    {
        get => _data[row, col];
        set => _data[row, col] = value;
    }
}

var m = new Matrix();
m[1, 2] = 5.0;
double val = m[1, 2];
```

---

### Q3. Can an indexer use a key type other than `int`, like a string?

**Answer:**
"Yes — the parameter type can be anything, not just `int`. That's exactly how `Dictionary<TKey,TValue>` works — its indexer takes a `TKey`, not an integer."

```csharp
public class ConfigSettings
{
    private Dictionary<string, string> _settings = new Dictionary<string, string>();

    public string this[string key]
    {
        get => _settings.TryGetValue(key, out var value) ? value : null;
        set => _settings[key] = value;
    }
}

var config = new ConfigSettings();
config["Environment"] = "Production";
string env = config["Environment"];
```

**Where to use:** wrapper/facade classes around a dictionary or config source, where callers want `config["key"]` syntax instead of `config.Get("key")`.

---

### Q4. Can a class have multiple indexers?

**Answer:**
"Yes, as long as their parameter lists are different — it's overloading, the same rule as with methods. A class could have one indexer that takes an `int` for positional access and another that takes a `string` for lookup by name."

```csharp
public class Row
{
    private object[] _values;
    private Dictionary<string, int> _columnNames;

    public object this[int index] => _values[index];
    public object this[string columnName] => _values[_columnNames[columnName]];
}

var row = GetRow();
var byPosition = row[0];
var byName = row["CustomerName"];
```

**Where this comes up:** this is literally how `DataRow` in `System.Data` works — you can access a column by ordinal position or by column name.

---

### Q5. Can an indexer be read-only, or without a setter?

**Answer:**
"Yes — just define the `get` accessor and leave out `set`, same as a read-only property. Useful when the underlying data shouldn't be modified through the indexer, only read."

```csharp
public class ReadOnlyBuffer
{
    private readonly int[] _data;
    public ReadOnlyBuffer(int[] data) => _data = data;

    public int this[int index] => _data[index]; // get-only, expression-bodied
}
```

---

### Q6. Where would you actually use a custom indexer in a real project?

**Answer:**
"Anywhere I'm wrapping a collection or building a lightweight, in-memory lookup/cache type and want callers to use natural `[]` syntax instead of a `Get`/`Set` method pair — for example, a request-scoped cache, a strongly-typed wrapper over a `NameValueCollection` of headers or query parameters, or a custom collection type that also needs extra logic (like validation or lazy-loading) on access, which a plain array or `Dictionary` can't do by itself."

```csharp
public class HeaderBag
{
    private readonly Dictionary<string, string> _headers;
    public HeaderBag(Dictionary<string, string> headers) => _headers = headers;

    public string this[string name]
    {
        get => _headers.TryGetValue(name, out var value) ? value : throw new KeyNotFoundException($"Header '{name}' not found");
        set => _headers[name] = value;
    }
}
```

**Where to use:** custom collection wrappers, config/settings objects, anywhere you want the ergonomics of `obj[key]` but need extra behavior (validation, defaulting, logging) that a plain `Dictionary` indexer wouldn't give you.

---

### Q7. Does an indexer support `params` or optional parameters?

**Answer:**
"Optional parameters — yes, same as any method. Multiple indexer parameters are also allowed, like the 2D matrix example. It's essentially a property with a parameter list, so most of the normal parameter rules apply."

---

### Quick one-liner if asked to summarize

> "An indexer lets an object be accessed with `obj[key]` syntax instead of a `Get`/`Set` method — declared with `this[...]` and `get`/`set` accessors. It's how built-in types like `List<T>`, `Dictionary<TKey,TValue>`, and `string` support bracket access, and it's useful in your own code anytime you're wrapping a collection or lookup and want that same natural syntax."
 