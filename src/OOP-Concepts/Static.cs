namespace OOP_Concepts
{
    // ---------------------------------------------------------------------
    // PART A: ARCHITECTURAL RESTRICTIONS OF A STATIC CLASS
    // ---------------------------------------------------------------------

    /// <summary>
    /// A static class acts purely as a logical global "container". 
    /// It is implicitly 'abstract' and 'sealed' by the compiler.
    /// </summary>
    internal static class StaticClass // : SomeBaseClass ❌ COMPILE ERROR: Static classes cannot inherit from any class except System.Object.
    {
        // 1. STATIC CONSTRUCTOR: The only constructor allowed. 
        // It has no access modifiers (public/private) and no parameters.
        // The CLR runs it automatically exactly once before the class is first accessed.
        static StaticClass()
        {
            // Put global configuration loading here
        }

        // ❌ COMPILE ERROR: "Static classes cannot contain instance constructors"
        // public StaticClass() { }

        // ❌ COMPILE ERROR: "Cannot declare instance members in a static class"
        // public int score = 0;

        // 2. STATIC FIELD: Fully valid. 
        // It occupies one fixed, permanent spot in memory for the life of the application.
        public static int highScore = 100;

        // 3. CONSTANT FIELDS: Fully allowed.
        // They are implicitly static and evaluated at compile-time directly into the calling code.
        public const string GameMode = "Ranked";

        // 4. STATIC READONLY FIELDS: Fully allowed.
        // Value is evaluated once at runtime (useful if fetched from a config file) and cannot change.
        public static readonly string BuildId = Guid.NewGuid().ToString();

        // 5. STATIC METHOD: Fully valid. 
        // Can be called directly using the class name anywhere.
        public static void DisplayScore()
        {
            Console.WriteLine($"Current High Score: {highScore}");
        }

        // 6. EXTENSION METHODS: Must reside inside a non-generic, top-level static class.
        // This attaches a new method to the 'string' type without altering the string class source code.
        public static void PrintToConsole(this string text)
        {
            Console.WriteLine($"[Extended Output]: {text}");
        }
    }

    // ---------------------------------------------------------------------
    // PART B: INTERACTION WITH NORMAL CLASSES & MEMORY USAGE
    // ---------------------------------------------------------------------

    // class DerivedStaticClass : StaticClass { } ❌ COMPILE ERROR: Cannot derive from static class.

    class StaticClassImplementationExample
    {
        // ❌ COMPILE ERROR: "Cannot create an instance of the static class 'StaticClass'"
        // StaticClass staticClass = new StaticClass();

        // 7. INSTANCE FIELD: Every object created from this class gets its own unique 'username' copy in memory.
        public string username;

        // 8. STATIC FIELD IN NORMAL CLASS: This variable belongs to the class metadata. 
        // No matter how many objects you create, only ONE 'totalUserCount' exists in memory.
        public static int totalUserCount = 0;

        public void Print()
        {
            // Accessing static members directly via ClassName.MemberName
            Console.WriteLine(StaticClass.highScore);
            StaticClass.DisplayScore();

            // Utilizing the extension method defined inside the static class
            string greeting = "Hello OOP Study Guide";
            greeting.PrintToConsole();
        }
    }
}
