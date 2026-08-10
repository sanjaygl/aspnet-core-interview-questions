# Polymorphism – Compact Interview Notes

## Original Code

```csharp
namespace OOP_Concepts.PolymorphismMastery
{
    // =========================================================================
    // PART A: BASE CLASS & CONSTRUCTOR CONCEPTS
    // =========================================================================
    // Demonstrates: Abstract class, Constructor initialization, Virtual method, Non-virtual method
    // An abstract class cannot be instantiated directly. It provides common state and behavior for derived classes.
    // =========================================================================
    public abstract class CreatureBaseClass
    {
        public string SpeciesName { get; set; }
        public int Health { get; set; }

        // ---------------------------------------------------------------------  
        // Base constructor executes before the derived constructor body.
        // Execution order:
        // Derived constructor requested
        //        ↓
        // Base constructor
        //        ↓
        // Base constructor body
        //        ↓
        // Derived constructor body
        // 'protected' allows the base constructor to be called by derived classes without allowing direct access from outside.
        // ---------------------------------------------------------------------
        protected CreatureBaseClass(string speciesName, int health)
        {
            SpeciesName = speciesName;
            Health = health;
            Console.WriteLine("--> [STEP 1]: [CREATURE BASE CLASS] constructor executed.");
        }

        // ---------------------------------------------------------------------
        // 'virtual' allows a derived class to provide its own implementation using 'override'.
        // If called through a base reference, runtime dispatch selects the overridden method of the actual object.
        // ---------------------------------------------------------------------
        public virtual void Speak()
        {
            Console.WriteLine($"[BASE CLASS] {SpeciesName} makes a generic creature sound.");
        }

        // ---------------------------------------------------------------------
        // This method is NOT virtual.
        // A derived class can hide it using the 'new' keyword, but hiding is different from overriding.
        // ---------------------------------------------------------------------
        public void DisplayId()
        {
            Console.WriteLine($"[BASE ID]: [BASE CLASS]-{SpeciesName.GetHashCode()}");
        }
    }

    // =========================================================================
    // PART B: DERIVED CLASS
    // =========================================================================
    // Demonstrates: Inheritance, Constructor chaining, Method overloading, Method overriding, Method hiding
    // =========================================================================
    public class DragonChildClass : CreatureBaseClass
    {
        public string ElementType { get; set; }
        public int HoardedGold { get; set; }

        // ---------------------------------------------------------------------
        // CONSTRUCTOR CHAINING USING 'this'
        // 'this(...)' calls another constructor in the SAME class.
        // This constructor redirects to the four-parameter constructor first.
        // ---------------------------------------------------------------------
        public DragonChildClass(string name, int health) : this(name, health, "Fire", 500)
        {
            Console.WriteLine("--> [STEP 4]: [CHILD CLASS] 2-parameter constructor executed.");
        }

        // ---------------------------------------------------------------------
        // CONSTRUCTOR CHAINING USING 'base'
        // 'base(...)' calls the constructor of the PARENT class.
        // Complete execution order:
        // Dragon 2-param constructor
        //        ↓
        // this(...)
        //        ↓
        // Dragon 4-param constructor
        //        ↓
        // base(...)
        //        ↓
        // Creature constructor
        //        ↓
        // Creature constructor body
        //        ↓
        // Dragon 4-param constructor body
        //        ↓
        // Dragon 2-param constructor body
        // ---------------------------------------------------------------------
        public DragonChildClass(string name, int health, string elementType, int gold) : base(name, health)
        {
            ElementType = elementType;
            HoardedGold = gold;
            Console.WriteLine("--> [STEP 2]: [CHILD CLASS] 4-parameter constructor executed.");
        }

        // =========================================================================
        // PART B1: METHOD OVERLOADING
        // =========================================================================
        // Compile-time polymorphism.
        // Multiple methods can have the same name when their parameter signatures are different.
        // The compiler determines the correct overload using:
        // - Number of parameters
        // - Parameter types
        // - Parameter order
        // IMPORTANT:
        // Return type alone CANNOT be used for method overloading.
        // =========================================================================
        public void BreatheElement()
        {
            Console.WriteLine($"[CHILD CLASS] {SpeciesName} breathes a massive wave of {ElementType}!");
        }

        // Overload based on parameter count.
        public void BreatheElement(int intensity)
        {
            Console.WriteLine($"[CHILD CLASS] {SpeciesName} breathes {ElementType} " + $"at intensity level {intensity}!");
        }

        // Overload based on parameter type.
        public void BreatheElement(string targetEnemy)
        {
            Console.WriteLine($"[CHILD CLASS] {SpeciesName} focuses its {ElementType} breath " + $"directly at {targetEnemy}!");
        }

        // =========================================================================
        // PART B2: METHOD OVERRIDING
        // =========================================================================
        // Runtime polymorphism.
        // Requirements:
        // 1. Base method must be virtual, abstract, or already overridden.
        // 2. Derived method must use 'override'.
        // 3. Method signature must match the inherited method.
        // Example:
        // Creature reference -> Dragon object
        // polymorphicCreature.Speak()
        //             ↓
        //        Dragon.Speak()
        // Runtime object type determines which overridden implementation runs.
        // =========================================================================

        public override void Speak()
        {
            Console.WriteLine($"[CHILD CLASS] {SpeciesName} lets out an earth-shaking roar!");
        }

        // =========================================================================
        // PART B3: METHOD HIDING
        // =========================================================================
        // 'new' hides the inherited method.
        // IMPORTANT DIFFERENCE:
        // Override:
        //     Actual OBJECT type determines behavior.
        // Hide:
        //     REFERENCE type determines behavior.
        // Therefore:
        // Dragon reference  -> Dragon.DisplayId()
        // Creature reference -> Creature.DisplayId()
        // Method hiding does not provide runtime polymorphism like override.
        // =========================================================================

        public new void DisplayId()
        {
            Console.WriteLine($"[CHILD CLASS] [CHILD HIDDEN ID]: " + $"DRAGON-{ElementType}-{SpeciesName.ToUpper()}");
        }
    }

    // =========================================================================
    // PART C: UPCASTING & DOWNCASTING
    // =========================================================================
    // UPCASTING:
    // Base-class reference points to a derived-class object.
    //     Creature creature = new Dragon(...);
    // Reference Type -> Creature
    // Actual Object  -> Dragon
    // Upcasting is implicit and safe.
    // DOWNCASTING:
    // Derived-class reference is obtained from a base-class reference.
    //     Dragon dragon = (Dragon)creature;
    // Downcasting requires an explicit cast and is safe ONLY when the actual runtime object is really a Dragon.
    // If the object is not a Dragon, InvalidCastException is thrown.
    // SAFE DOWNCASTING:
    //     if (creature is Dragon dragon)
    //     {
    //         dragon.BreatheElement();
    //     }
    // Or:
    //     Dragon? dragon = creature as Dragon;
    //     if (dragon != null)
    //         dragon.BreatheElement();
    // IMPORTANT:
    // Creature creature = new Dragon(...);  // UPCASTING   ✅
    // Dragon dragon = (Dragon)creature;     // DOWNCASTING ✅
    // Creature creature = new Creature(...);
    // Dragon dragon = (Dragon)creature;     // ❌ InvalidCastException
    // =========================================================================


    // =========================================================================
    // PART D: EXECUTION PLATFORM
    // =========================================================================

    public class PolymorphismRunner
    {
        public static void RunFullDemonstration()
        {
            // =================================================================
            // 1. CONSTRUCTOR CHAINING
            // =================================================================
            Console.WriteLine("=== 1. CONSTRUCTOR CHAINING DEMONSTRATION ===");

            // Triggers:
            // [CHILD CLASS] 2-param->[CHILD CLASS] 4-param->[BASE CLASS] constructor->[CHILD CLASS] 4-param body->[CHILD CLASS] 2-param body
            DragonChildClass childClass = new DragonChildClass("Smaug", 1500);

            // =================================================================
            // 2. METHOD OVERLOADING
            // =================================================================
            Console.WriteLine("\n=== 2. METHOD OVERLOADING DEMONSTRATION ===");
            Console.WriteLine("--> Compiler selects the appropriate Child Class method at compile time based on the number and type of parameters.");
            childClass.BreatheElement();
            childClass.BreatheElement(9000);
            childClass.BreatheElement("The Knight");


            // =================================================================
            // 3. UPCASTING
            // =================================================================

            Console.WriteLine("\n=== 3. UPCASTING DEMONSTRATION ===");
            Console.WriteLine("--> Base Class reference is pointing to the same Child Class object.");
            CreatureBaseClass baseClass = childClass;

            // =================================================================
            // 4. OVERRIDING / RUNTIME POLYMORPHISM
            // =================================================================
            Console.WriteLine("\n=== 4. OVERRIDING DEMONSTRATION ===");
            Console.WriteLine("--> Child Class reference is pointing to the Child Class object.");
            DragonChildClass pureDragonChildClass = childClass;

            Console.WriteLine("--> Child Class reference is pointing to the Child Class object. Therefore, the Child Class overridden method will be executed.");
            pureDragonChildClass.Speak();
            Console.WriteLine("--> Base Class reference is pointing to the Child Class object. Because Speak() is virtual and overridden, the Child Class method will be executed.");
            baseClass.Speak();

            // =================================================================
            // 5. METHOD HIDING
            // =================================================================
            Console.WriteLine("\n=== 5. METHOD HIDING DEMONSTRATION ===");
            Console.WriteLine("--> Child Class reference is pointing to the Child Class object. Therefore, the Child Class hidden method will be executed.");
            pureDragonChildClass.DisplayId();
            Console.WriteLine("--> Base Class reference is pointing to the Child Class object. DisplayId() is non-virtual, so the Base Class method will be executed.");
            baseClass.DisplayId();

            // =================================================================
            // 6. DOWNCASTING
            // =================================================================

            Console.WriteLine("\n=== 6. DOWNCASTING DEMONSTRATION ===");
            Console.WriteLine("--> Base Class reference is currently pointing to a Child Class object.");
            Console.WriteLine("--> Downcasting converts the Base Class reference back to a Child Class reference using an explicit cast.");
            Console.WriteLine("--> Actual object is DragonChildClass, so the explicit cast is valid.");
            DragonChildClass downcastedDragon = (DragonChildClass)baseClass;

            Console.WriteLine("--> Downcasting successful. Now the Child Class reference can access Child Class members.");
            Console.WriteLine("--> Calling DisplayId() using the downcasted Child Class reference. So child class method will be executed");
            downcastedDragon.DisplayId();
            Console.WriteLine("--> Calling BreatheElement() using the downcasted Child Class reference. So child class method will be executed");
            downcastedDragon.BreatheElement();

            // =================================================================
            // 7. 'is' TYPE CHECK
            // =================================================================
            Console.WriteLine("\n=== 7. 'is' TYPE CHECK ===");
            // Checks the actual runtime type before casting.
            if (baseClass is DragonChildClass dragon)
            {
                dragon.BreatheElement();
            }

            // =================================================================
            // 8. 'as' SAFE CAST
            // =================================================================
            Console.WriteLine("\n=== 8. 'as' SAFE CAST ===");
            // Returns null instead of throwing when conversion fails.
            DragonChildClass? safeDragon = baseClass as DragonChildClass;
            if (safeDragon != null)
            {
                safeDragon.BreatheElement();
            }
        }
    }
}

// =========================================================================
// PART E: QUICK INTERVIEW QUESTIONS
// =========================================================================
//
// 1. What is inheritance, and how does DragonChildClass inherit from CreatureBaseClass?
// 2. What is upcasting, and why can a CreatureBaseClass reference point to a DragonChildClass object?
// 3. What is downcasting, when is it required, and why is an explicit cast needed?
// 4. What is method overloading, and why is it considered compile-time polymorphism?
// 5. What is method overriding, and why is it considered runtime polymorphism?
// 6. What is method hiding, how does the 'new' keyword work, and how is it different from overriding?
// 7. What is the purpose of the 'is' operator in C#?
// 8. What is the purpose of the 'as' operator, and what happens when the conversion fails?
// 9. What is an abstract class, and why can it not be instantiated directly?
// 10. What is an abstract method, and why must a concrete child class implement it?
// 11. What is the purpose of the 'base' keyword in inheritance and constructor chaining?
// 12. What is the purpose of the 'this' keyword in constructor chaining?
// 13. What is the difference between reference type and actual object type in polymorphism?
// 14. Why can the return type alone not be used to overload a method?
// 15. Why can a base-class reference point to a child-class object, but a child-class reference cannot directly point to a base-class object?
// 16. What is an abstract method, and how does it force a concrete child class to provide an implementation?
// 17. What is interface polymorphism, and how can dispatch work through an interface reference without a shared base class?
// 18. What is operator overloading, and why is it considered compile-time polymorphism?
// 19. What is a sealed override, and why can a further derived class not override it again?
// 20. What is the difference between overridden, abstract, and interface members versus hidden ('new') and static members when determining which implementation is executed?
//
// =========================================================================
```

---

# Explanation

### Part A: Base Class & Constructor Concepts

`CreatureBaseClass` is an abstract base class. It provides common state and behavior for derived classes.

```csharp
public abstract class CreatureBaseClass
```

An abstract class cannot be instantiated directly.

The base class contains:

- `SpeciesName`
- `Health`
- A protected constructor
- A virtual method: `Speak()`
- A non-virtual method: `DisplayId()`

### Base Constructor

```csharp
protected CreatureBaseClass(string speciesName, int health)
```

The base constructor initializes the common properties.

When a derived object is created, the base constructor executes before the derived constructor body.

```text
Derived constructor requested
        ↓
Base constructor
        ↓
Base constructor body
        ↓
Derived constructor body
```

---

### Part B: Derived Class

```csharp
public class DragonChildClass : CreatureBaseClass
```

`DragonChildClass` inherits from `CreatureBaseClass`.

It adds:

```csharp
public string ElementType { get; set; }
public int HoardedGold { get; set; }
```

It also demonstrates:

- Constructor chaining
- Method overloading
- Method overriding
- Method hiding

### Constructor Chaining Using `this`

```csharp
public DragonChildClass(string name, int health)
    : this(name, health, "Fire", 500)
```

`this(...)` calls another constructor in the same class.

### Constructor Chaining Using `base`

```csharp
public DragonChildClass(
    string name,
    int health,
    string elementType,
    int gold) : base(name, health)
```

`base(...)` calls the constructor of the parent class.

Execution order:

```text
Dragon 2-parameter constructor
        ↓
this(...)
        ↓
Dragon 4-parameter constructor
        ↓
base(...)
        ↓
Creature constructor
        ↓
Creature constructor body
        ↓
Dragon 4-parameter constructor body
        ↓
Dragon 2-parameter constructor body
```

---

### Part B1: Method Overloading

Method overloading is **compile-time polymorphism**.

Multiple methods can have the same name when their parameter signatures are different.

```csharp
public void BreatheElement()
{
}

public void BreatheElement(int intensity)
{
}

public void BreatheElement(string targetEnemy)
{
}
```

The compiler determines the correct overload using:

- Number of parameters
- Parameter types
- Parameter order

Return type alone cannot be used for method overloading.

---

### Part B2: Method Overriding

Method overriding is **runtime polymorphism**.

Requirements:

1. Base method must be `virtual`, `abstract`, or already overridden.
2. Derived method must use `override`.
3. The method signature must match the inherited method.

```csharp
public virtual void Speak()
{
}

public override void Speak()
{
}
```

If:

```csharp
CreatureBaseClass creature = new DragonChildClass(...);
creature.Speak();
```

the runtime object is `DragonChildClass`, so the overridden `DragonChildClass.Speak()` implementation executes.

---

### Part B3: Method Hiding

Method hiding uses the `new` keyword.

```csharp
public new void DisplayId()
{
}
```

The important difference is:

```text
Override
    ↓
Actual OBJECT type determines behavior

Hide
    ↓
REFERENCE type determines behavior
```

Therefore:

```text
Dragon reference    → Dragon.DisplayId()
Creature reference  → Creature.DisplayId()
```

Method hiding does not provide runtime polymorphism like `override`.

---

### Part C: Upcasting

Upcasting means a base-class reference points to a derived-class object.

```csharp
CreatureBaseClass baseClass = childClass;
```

Conceptually:

```text
Reference Type → CreatureBaseClass
Actual Object  → DragonChildClass
```

Upcasting is implicit and safe.

---

### Downcasting

Downcasting means obtaining a derived-class reference from a base-class reference.

```csharp
DragonChildClass downcastedDragon =
    (DragonChildClass)baseClass;
```

Downcasting requires an explicit cast.

It is safe only when the actual runtime object is really a `DragonChildClass`.

If the object is not a `DragonChildClass`, `InvalidCastException` is thrown.

---

### Safe Downcasting Using `is`

```csharp
if (baseClass is DragonChildClass dragon)
{
    dragon.BreatheElement();
}
```

The `is` operator checks the actual runtime type before using the derived reference.

---

### Safe Downcasting Using `as`

```csharp
DragonChildClass? safeDragon =
    baseClass as DragonChildClass;

if (safeDragon != null)
{
    safeDragon.BreatheElement();
}
```

The `as` operator returns `null` instead of throwing when the reference conversion fails.

---

# Execution Demonstration

`PolymorphismRunner.RunFullDemonstration()` demonstrates:

1. Constructor chaining
2. Method overloading
3. Upcasting
4. Overriding / runtime polymorphism
5. Method hiding
6. Downcasting
7. `is` type check
8. `as` safe cast

---

# Important Rules

- Method overloading is **compile-time polymorphism**.
- Method overriding is **runtime polymorphism**.
- `virtual` allows a derived class to override a method.
- `override` provides the derived implementation.
- `new` hides an inherited member; it does not override it.
- Upcasting is converting a derived reference to a base reference.
- Upcasting is implicit and safe.
- Downcasting converts a base reference to a derived reference.
- Downcasting requires an explicit cast.
- Invalid downcasting can throw `InvalidCastException`.
- `is` can check the runtime type before casting.
- `as` returns `null` when the reference conversion fails.
- `base(...)` calls a parent-class constructor.
- `this(...)` calls another constructor in the same class.
- The reference type and actual object type can be different in polymorphism.

---

# Questions

### 1. What is inheritance, and how does `DragonChildClass` inherit from `CreatureBaseClass`?

`DragonChildClass` inherits from `CreatureBaseClass` using:

```csharp
public class DragonChildClass : CreatureBaseClass
```

This creates an inheritance relationship where the derived class can use accessible members of the base class.

### 2. What is upcasting, and why can a `CreatureBaseClass` reference point to a `DragonChildClass` object?

Upcasting means assigning a derived object to a base-class reference:

```csharp
CreatureBaseClass creature =
    new DragonChildClass(...);
```

A `DragonChildClass` is a `CreatureBaseClass`, so the assignment is valid.

### 3. What is downcasting, when is it required, and why is an explicit cast needed?

Downcasting converts a base-class reference back to a derived-class reference:

```csharp
DragonChildClass dragon =
    (DragonChildClass)creature;
```

An explicit cast is required because the compiler cannot guarantee that the actual runtime object is the derived type.

### 4. What is method overloading, and why is it considered compile-time polymorphism?

Method overloading means multiple methods have the same name but different parameter signatures.

The compiler selects the correct overload based on the arguments, so it is compile-time polymorphism.

### 5. What is method overriding, and why is it considered runtime polymorphism?

Method overriding allows a derived class to provide a different implementation of an inherited virtual or abstract method.

The runtime object determines which implementation executes.

### 6. What is method hiding, how does the `new` keyword work, and how is it different from overriding?

Method hiding uses `new` to define a member with the same name as an inherited member.

Unlike overriding, method hiding is selected based on the reference type.

### 7. What is the purpose of the `is` operator in C#?

It checks whether an object is compatible with a specified type and can perform pattern matching.

```csharp
if (baseClass is DragonChildClass dragon)
{
    dragon.BreatheElement();
}
```

### 8. What is the purpose of the `as` operator, and what happens when the conversion fails?

`as` performs a safe reference conversion. If the conversion fails, it returns `null`.

### 9. What is an abstract class, and why can it not be instantiated directly?

An abstract class is intended to be used as a base class. It cannot be instantiated directly.

### 10. What is an abstract method, and why must a concrete child class implement it?

An abstract method defines required behavior without providing an implementation. A concrete derived class must provide its implementation.

### 11. What is the purpose of the `base` keyword in inheritance and constructor chaining?

`base` is used to access base-class members and call a base-class constructor.

```csharp
: base(name, health)
```

### 12. What is the purpose of the `this` keyword in constructor chaining?

`this(...)` calls another constructor in the same class.

```csharp
: this(name, health, "Fire", 500)
```

### 13. What is the difference between reference type and actual object type in polymorphism?

Example:

```csharp
CreatureBaseClass baseClass =
    new DragonChildClass(...);
```

Here:

```text
Reference Type → CreatureBaseClass
Actual Object  → DragonChildClass
```

The reference type controls accessible members, while runtime dispatch uses the actual object for overridden virtual members.

### 14. Why can the return type alone not be used to overload a method?

Overload resolution is based on the parameter signature, not only the return type.

### 15. Why can a base-class reference point to a child-class object, but a child-class reference cannot directly point to a base-class object?

Every derived object is also an instance of its base type, so upcasting is valid.

Not every base object is an instance of the derived type, so downcasting is not always safe.

### 16. What is an abstract method, and how does it force a concrete child class to provide an implementation?

An abstract method has no implementation in the base class. A concrete derived class must override it.

### 17. What is interface polymorphism, and how can dispatch work through an interface reference without a shared base class?

An interface reference can point to different implementing objects. Each implementation can provide its own behavior for the same interface member.

### 18. What is operator overloading, and why is it considered compile-time polymorphism?

Operator overloading allows operators such as `+`, `-`, or `==` to have custom behavior for user-defined types. The compiler selects the appropriate operator based on operand types.

### 19. What is a sealed override, and why can a further derived class not override it again?

A method can be declared:

```csharp
public sealed override void Speak()
{
}
```

A further derived class can inherit the method but cannot override it again.

### 20. What is the difference between overridden, abstract, and interface members versus hidden (`new`) and static members when determining which implementation is executed?

- `override` participates in runtime polymorphism.
- `abstract` requires a concrete derived class to provide an implementation.
- Interface members provide a contract for implementing types.
- `new` hides a member and is selected based on the reference type.
- Static members belong to the type and are not overridden through normal instance polymorphism.

---

# Important Missing Points

- The source includes interface polymorphism, operator overloading, and sealed overrides as interview topics, but does not provide code demonstrations for them.
- The source directly demonstrates constructor chaining, overloading, overriding, method hiding, upcasting, downcasting, `is`, and `as`.
