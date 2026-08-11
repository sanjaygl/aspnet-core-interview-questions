# Array vs ArrayList vs Dictionary — Interview Q&A

---

### Q1. What is an Array?

**Answer:**
"An array is a fixed-size collection of elements, all of the same type, stored in contiguous memory. Once created, its size can't change. Because it's strongly typed, there's no boxing/unboxing and no casting needed."

```csharp
int[] numbers = new int[3];
numbers[0] = 1;
numbers[1] = 2;
numbers[2] = 3;

// numbers[3] = 4; // throws IndexOutOfRangeException — fixed size
```

**Where to use:** when you know the exact number of elements up front and want the best raw performance — e.g., a fixed set of days of the week, a buffer of a known size.

---

### Q2. What is an ArrayList?

**Answer:**
"`ArrayList` is an old, non-generic, resizable list from `System.Collections`. It stores everything as `object`, so it can hold mixed types, but that means value types get boxed, and you have to cast when reading items back out. It's basically obsolete now — `List<T>` replaced it once generics arrived in C# 2.0."

```csharp
ArrayList list = new ArrayList();
list.Add(1);        // boxed - int wrapped in an object
list.Add("hello");  // mixed types allowed, no compile-time safety
list.Add(3.5);

int first = (int)list[0]; // must cast back explicitly
```

**Where to use:** basically nowhere in new code — mentioned mainly because it's asked about historically, or if you're touching very old codebases. Always prefer `List<T>` today.

---

### Q3. What is a Dictionary?

**Answer:**
"`Dictionary<TKey, TValue>` stores data as key-value pairs, backed by a hash table. Lookups, inserts, and deletes by key are close to O(1) on average, instead of scanning through every element like you would with an array or list."

```csharp
Dictionary<string, int> ages = new Dictionary<string, int>();
ages["John"] = 30;
ages["Jane"] = 25;

int johnsAge = ages["John"]; // O(1) average lookup by key
bool exists = ages.ContainsKey("Mike"); // false
```

**Where to use:** whenever you need to look things up by a unique key instead of by position — caching, counting occurrences, mapping IDs to objects, fast membership checks.

---

### Q4. Array vs ArrayList — what's the real difference?

**Answer:**
"Array is fixed-size and strongly typed — no boxing, no casting, best performance. ArrayList is resizable but stores everything as `object`, so value types get boxed/unboxed and you need casts, which is slower and loses compile-time type safety."

| | Array | ArrayList |
|---|---|---|
| Size | Fixed at creation | Resizable |
| Type safety | Strongly typed (`int[]`) | Stores `object` — no compile-time safety |
| Boxing/unboxing | None | Yes, for value types |
| Performance | Fastest | Slower (boxing + casting overhead) |
| Modern usage | Still common for fixed-size data | Obsolete — use `List<T>` instead |

---

### Q5. ArrayList vs Dictionary — when would you use which?

**Answer:**
"They solve different problems. `ArrayList`/`List<T>` is about *order* — items accessed by index, 0, 1, 2... `Dictionary` is about *lookup by key* — items accessed by something meaningful, like a name or an ID, not their position. If I need 'give me the customer with ID 42' fast, that's a Dictionary. If I need 'give me the 3rd item added', that's a List."

```csharp
List<string> names = new List<string> { "John", "Jane" };
string first = names[0]; // access by position

Dictionary<int, string> customers = new Dictionary<int, string>();
customers[42] = "John";
string customer = customers[42]; // access by key, not position
```

---

### Q6. What's the performance difference for lookups: Array/List vs Dictionary?

**Answer:**
"Searching for a value inside an array or list means scanning through it — O(n) in the worst case. A Dictionary looks up by key using a hash table, so it's O(1) on average, regardless of how many items are in it. If I'm doing a lot of 'does this exist' or 'get me this by ID' operations, Dictionary is far faster at scale."

```csharp
// O(n) - has to scan every element
List<Customer> customers = GetCustomers();
var found = customers.FirstOrDefault(c => c.Id == 42);

// O(1) average - direct hash lookup
Dictionary<int, Customer> customerById = GetCustomersById();
var found2 = customerById[42];
```

**Where to use:** if you're going to look items up by some identifier repeatedly, build a `Dictionary` keyed by that identifier instead of scanning a `List` every time.

---

### Q7. Can a Dictionary have duplicate keys? What about duplicate values?

**Answer:**
"No duplicate keys — each key must be unique, and adding a duplicate key with `.Add()` throws an exception (use indexer assignment `dict[key] = value` instead, which overwrites). Values can absolutely repeat — many keys can map to the same value."

```csharp
var dict = new Dictionary<string, int>();
dict.Add("a", 1);
dict.Add("a", 2); // throws ArgumentException - key already exists

dict["a"] = 2; // fine - overwrites the existing value
dict["b"] = 2; // fine - duplicate VALUE is allowed
```

---

### Q8. What would you actually use in modern C# instead of Array/ArrayList/Dictionary directly?

**Answer:**
- Fixed-size, known-length data → **`Array`** (`T[]`) is still fine and fast.
- Resizable, ordered, indexed data → **`List<T>`**, the generic, type-safe replacement for `ArrayList`.
- Key-based lookup → **`Dictionary<TKey, TValue>`**.
- Thread-safe key-based lookup → **`ConcurrentDictionary<TKey, TValue>`**.

"In practice I never reach for `ArrayList` in new code — it's only relevant if I'm reading legacy code or answering an interview question about the pre-generics era."

---

### Quick one-liner if asked to summarize

> "Array is a fixed-size, strongly-typed collection accessed by index. ArrayList is its old, resizable but non-generic ancestor — obsolete, replaced by `List<T>`. Dictionary stores key-value pairs and gives near-O(1) lookup by key instead of by position — use it whenever you need to find something by an identifier rather than scan through a sequence."
