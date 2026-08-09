namespace OOP_Concepts.Abstraction
{
    /// <summary>
    /// An interface is a type of contract where the implementing class must implement its members. 
    /// In other words, we can say an interface is a set of rules that the implementer has to follow.
    /// </summary>
    internal interface IConcept
    {
        // ---------------------------------------------------------------------
        // PART A: WHAT IS NOT ALLOWED (RESTRICTIONS)
        // ---------------------------------------------------------------------

        // 1. NO INSTANCE FIELDS: Interfaces cannot hold normal instance state variables.
        // string name = "sanjay"; 

        // 2. NO INSTANCE READONLY FIELDS: Non-static 'readonly' instance variables are strictly banned.
        // readonly string instanceId;

        // 3. NO INSTANCE CONSTRUCTORS: Interfaces cannot have instance constructors because they cannot be initialized directly.
        // public IConcept() { } 


        // ---------------------------------------------------------------------
        // PART B: STANDARD ABSTRACT MEMBERS (MUST BE IMPLEMENTED BY CLASS/STRUCT)
        // ---------------------------------------------------------------------

        // 4. PROPERTIES: Declares a read-write data property rule (implicitly public).
        string Name { get; set; }

        // 5. GET-ONLY PROPERTIES: The correct alternative to 'readonly' variables for enforcing read-only instance data.
        string RuntimeId { get; }

        // 6. METHODS: Declares a behavior rule (implicitly public).
        string PrintMessage(string message);
        string Print(string message);

        // 7. EVENTS: Declares a notification rule that the class must trigger.
        event EventHandler OnDataChanged;

        // 8. INDEXERS: Declares array-like index tracking syntax (e.g., object[0]).
        string this[int index] { get; set; }

        // ---------------------------------------------------------------------
        // PART C: MODERN ADVANCED FEATURES (C# 8.0 AND NEWER)
        // ---------------------------------------------------------------------

        // 9. PRIVATE METHODS: Allowed ONLY if they contain a working code body, used to organize default methods.
        private int InternalHelper(int a, int b)
        {
            return a + b;
        }

        // 10. DEFAULT IMPLEMENTATIONS: Provides optional fallback code so implementing classes do not break when updated.
        public decimal Discount(decimal discount)
        {
            int temporaryCalculation = InternalHelper(5, 5);
            return discount;
        }

        // 11. STATIC FIELDS & METHODS: Allowed in C# 8.0+. Belongs strictly to the interface itself, not to the implementer.
        public static string InterfaceVersion = "v2.0";

        public static void ShowVersion()
        {
            Console.WriteLine($"Current version: {InterfaceVersion}");
        }

        // 12. CONST FIELDS: Fully allowed (C# 8.0+). They are implicitly static and evaluated at compile-time.
        public const int MaxRetries = 3;

        // 13. STATIC READONLY FIELDS: Fully allowed (C# 8.0+). Perfect for values evaluated at runtime that cannot change.
        public static readonly string BuildDate = DateTime.Now.ToString("yyyy-MM-dd");
    }

    // ---------------------------------------------------------------------
    // PART D: USAGE & INHERITANCE RULES
    // ---------------------------------------------------------------------

    // Note: A class or struct can implement UNLIMITED interfaces.
    internal class ImplementationExample : IConcept, IDisposable
    {
        // Fulfilling Rule 4 (Properties)
        public string Name { get; set; }

        // Fulfilling Rule 5 (Get-Only Properties)
        public string RuntimeId { get; } = Guid.NewGuid().ToString();

        // Fulfilling Rule 6 (Methods)
        // public string PrintMessage(string message) => $"Message: {message}";
        public string Print(string message)
        {
            return message + " " + PrintMessage(IConcept.InterfaceVersion);
        }

        // Fulfilling Rule 7 (Events)
        public event EventHandler OnDataChanged;

        // Fulfilling Rule 8 (Indexers)
        public string this[int index] { get => "Value"; set { } }

        // Fulfilling IDisposable Interface Contract
        public void Dispose() { /* Cleanup code */ }

        // Note regarding Rules 11, 12, 13: 
        // This class does NOT inherit MaxRetries, InterfaceVersion, or BuildDate.
        // To use them anywhere, you must call: IConcept.MaxRetries or IConcept.BuildDate

        public string PrintMessage(string message) => $"InterfaceVersion: {message}, MaxRetries: {IConcept.MaxRetries}, BuildDate: {IConcept.BuildDate} ";
    }
}

//---------------------------------------------------------------------
// Questions
//---------------------------------------------------------------------

// 1. Can an interface be initialized?
// 2. Can an interface have methods with implementations?
// 3. What happens if two interfaces have the same method? How can we implement them?