namespace OOP_Concepts.Abstraction
{
    /// <summary>
    /// An abstract class is a partially built blueprint. It can contain abstract members (rules with no code) 
    /// and concrete members (fully functional methods/fields with code). Derived classes must implement the abstract parts.
    /// </summary>
    internal abstract class ConceptBase
    {
        // ---------------------------------------------------------------------
        // PART A: WHAT IS FULLY ALLOWED HERE (UNLIKE INTERFACES)
        // ---------------------------------------------------------------------

        // 1. ALLOWED INSTANCE FIELDS: Abstract classes can directly hold state variables.
        private string internalState = "Sanjay";

        // 2. ALLOWED INSTANCE READONLY FIELDS: Non-static 'readonly' instance variables are fully supported.
        protected readonly string instanceId = Guid.NewGuid().ToString();

        // 3. ALLOWED INSTANCE CONSTRUCTORS: Used to initialize instance fields when a child class is created.
        public ConceptBase()
        {
            // Constructor logic goes here..
        }

        // ---------------------------------------------------------------------
        // PART B: EXPLICIT ABSTRACT MEMBERS (MUST BE OVERRIDDEN BY CHILD CLASS)
        // ---------------------------------------------------------------------

        // 4. PROPERTIES: Declares an unwritten data rule. Must use the 'abstract' and 'public/protected' keywords.
        public abstract string Name { get; set; }

        // 5. GET-ONLY PROPERTIES: Enforces a read-only instance data rule that a child class must resolve.
        public abstract string RuneTimeId { get; }

        // 6. METHODS: Declares a behavior rule with no body. Must be marked 'abstract'.
        public abstract string PrintMessage(string message);
        public abstract string Print(string message);

        // 7. EVENTS: Declares a notification contract that a child class must implement.
        public abstract event EventHandler OnDataChanged;

        // 8. INDEXERS: Declares array-like index tracking rules for the derived class to code.
        public abstract string this[int index] { get; set; }


        // ---------------------------------------------------------------------
        // PART C: CONCRETE / REGULAR FEATURES (REUSABLE CODE FOR CHILDREN)
        // ---------------------------------------------------------------------

        // 9. PRIVATE HELPER METHODS: Fully allowed for structural background work within this class.
        private int InternalHelper(int a, int b)
        {
            return a + b;
        }

        // 10. VIRTUAL / CONCRETE METHODS: Provides working fallback code. Child classes inherit this automatically, 
        // but can optionally use the 'override' keyword to completely rewrite it.
        public virtual decimal Discount(decimal discount)
        {
            int temporaryCalculation = InternalHelper(5, 5);
            return discount;
        }

        // 11. STATIC FIELDS & METHODS: Belongs strictly to the class type. Can be accessed via 'ConceptBase.ClassVersion'.
        public static string ClassVersion = "v2.0";
        public static void ShowVersion()
        {
            Console.WriteLine($"Current version: {ClassVersion}");
        }

        // 12. CONST FIELDS: Evaluated at compile-time and shared globally.
        public const int MaxRetries = 3;

        // 13. STATIC READONLY FIELDS: Evaluated once at runtime and locked from changes.
        public static readonly string BuildDate = DateTime.Now.ToString("yyyy-MM-dd");
    }

    // ---------------------------------------------------------------------
    // PART D: USAGE & INHERITANCE RULES
    // ---------------------------------------------------------------------

    // Note 1: A class can inherit from ONLY ONE class (Single Inheritance). It cannot inherit multiple abstract classes.
    // Note 2: Structs CANNOT inherit from abstract classes. Only classes can.

    internal class DerivedImplementationExample : ConceptBase, IDisposable
    {
        // Fulfilling Rule 4 (Properties) using the 'override' keyword
        public override string Name { get; set; }

        // Fulfilling Rule 5 (Get-Only Properties) using the 'override' keyword
        public override string RuneTimeId => instanceId;

        // Fulfilling Rule 6 (Methods)
        public override string Print(string message)
        {
            return message + " " + PrintMessage(ConceptBase.ClassVersion);
        }

        public override string PrintMessage(string message) =>
            $"ClassVersion: {message}, MaxRetries: {ConceptBase.MaxRetries}, BuildDate: {ConceptBase.BuildDate} ";

        // Fulfilling Rule 7 (Events)
        public override event EventHandler OnDataChanged;

        // Fulfilling Rule 8 (Indexers)
        public override string this[int index] { get => "Value"; set { } }

        // Fulfilling standard interface contract (Multiple interfaces are allowed alongside one abstract class parent)
        public void Dispose() { /* Cleanup code */ }

        // Note regarding Rules 11, 12, 13: 
        // Static and Const members are inherited by the child type definition here! 
        // You can safely use 'DerivedImplementationExample.MaxRetries' or 'ConceptBase.MaxRetries'.
    }
}

//---------------------------------------------------------------------
// Questions
//---------------------------------------------------------------------

// 1. Can a non-abstract class have abstract members?
// 2. Can an abstract class be a sealed class?
// 3. Can an abstract class have non-abstract methods without a body?
// 4. Can an abstract class be initialized?
// 5. How is an abstract class initialized?
// 6. Can an abstract class have a private constructor?
// 7. Can an abstract class have a static constructor?
