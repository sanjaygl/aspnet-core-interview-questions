namespace OOP_CSharp_Concepts
{
    // =========================================================================
    // PART A: ABSTRACT CLASS
    // =========================================================================
    // An abstract class is a partially implemented blueprint.
    //
    // It can contain:
    // - Instance fields
    // - Instance constructors
    // - Abstract members
    // - Concrete members
    // - Virtual members
    // - Static members
    // - Constants
    //
    // An abstract class cannot be instantiated directly.
    // =========================================================================

    internal abstract class AbstractionBase
    {
        // ---------------------------------------------------------------------
        // 1. INSTANCE FIELD
        // ---------------------------------------------------------------------
        // Abstract classes can contain instance state.
        // ---------------------------------------------------------------------

        private string internalState = "Sanjay";

        // ---------------------------------------------------------------------
        // 2. INSTANCE READONLY FIELD
        // ---------------------------------------------------------------------
        // A readonly instance field can be initialized when the object is
        // created and cannot be reassigned afterward.
        // ---------------------------------------------------------------------

        protected readonly string instanceId =
            Guid.NewGuid().ToString();

        // ---------------------------------------------------------------------
        // 3. INSTANCE CONSTRUCTOR
        // ---------------------------------------------------------------------
        // An abstract class can have an instance constructor.
        // It is executed when a derived class object is created.
        // ---------------------------------------------------------------------

        public AbstractionBase()
        {
            // Constructor logic goes here.
        }

        // ---------------------------------------------------------------------
        // PART B: ABSTRACT MEMBERS
        // ---------------------------------------------------------------------

        // 4. ABSTRACT PROPERTY
        // ---------------------------------------------------------------------
        // No implementation is provided here.
        // A concrete derived class must override it.
        // ---------------------------------------------------------------------

        public abstract string Name { get; set; }

        // ---------------------------------------------------------------------
        // 5. ABSTRACT GET-ONLY PROPERTY
        // ---------------------------------------------------------------------

        public abstract string RuntimeId { get; }

        // ---------------------------------------------------------------------
        // 6. ABSTRACT METHODS
        // ---------------------------------------------------------------------
        // Abstract methods have no body and must be implemented by a concrete
        // derived class.
        // ---------------------------------------------------------------------

        public abstract string PrintMessage(string message);

        public abstract string Print(string message);

        // ---------------------------------------------------------------------
        // 7. ABSTRACT EVENT
        // ---------------------------------------------------------------------

        public abstract event EventHandler OnDataChanged;

        // ---------------------------------------------------------------------
        // 8. ABSTRACT INDEXER
        // ---------------------------------------------------------------------

        public abstract string this[int index] { get; set; }


        // ---------------------------------------------------------------------
        // PART C: CONCRETE / REGULAR MEMBERS
        // ---------------------------------------------------------------------

        // 9. PRIVATE HELPER METHOD
        // ---------------------------------------------------------------------
        // An abstract class can contain normal implemented methods.
        // ---------------------------------------------------------------------

        private int InternalHelper(int a, int b)
        {
            return a + b;
        }

        // ---------------------------------------------------------------------
        // 10. VIRTUAL / CONCRETE METHOD
        // ---------------------------------------------------------------------
        // A virtual method already has an implementation.
        // A derived class may optionally override it.
        // ---------------------------------------------------------------------

        public virtual decimal Discount(decimal discount)
        {
            int temporaryCalculation =
                InternalHelper(5, 5);

            return discount;
        }

        // ---------------------------------------------------------------------
        // 11. STATIC FIELD AND METHOD
        // ---------------------------------------------------------------------
        // Static members belong to the type rather than an object.
        // ---------------------------------------------------------------------

        public static string ClassVersion = "v2.0";

        public static void ShowVersion()
        {
            Console.WriteLine($"Current version: {ClassVersion}");
        }

        // ---------------------------------------------------------------------
        // 12. CONSTANT
        // ---------------------------------------------------------------------
        // A const value is evaluated at compile time.
        // ---------------------------------------------------------------------

        public const int MaxRetries = 3;

        // ---------------------------------------------------------------------
        // 13. STATIC READONLY FIELD
        // ---------------------------------------------------------------------
        // Initialized at runtime and cannot be reassigned afterward.
        // ---------------------------------------------------------------------

        public static readonly string BuildDate =
            DateTime.Now.ToString("yyyy-MM-dd");
    }


    // =========================================================================
    // PART D: DERIVED CLASS
    // =========================================================================
    // A concrete derived class must implement all abstract members.
    // =========================================================================

    internal class DerivedImplementationRunner : AbstractionBase, IDisposable
    {
        // ---------------------------------------------------------------------
        // 14. IMPLEMENTING ABSTRACT PROPERTY
        // ---------------------------------------------------------------------

        public override string Name { get; set; }

        // ---------------------------------------------------------------------
        // 15. IMPLEMENTING ABSTRACT GET-ONLY PROPERTY
        // ---------------------------------------------------------------------

        public override string RuntimeId => instanceId;

        // ---------------------------------------------------------------------
        // 16. IMPLEMENTING ABSTRACT METHODS
        // ---------------------------------------------------------------------

        public override string Print(string message)
        {
            return message + " " +
                   PrintMessage(ClassVersion);
        }

        public override string PrintMessage(string message) =>
            $"ClassVersion: {message}, " +
            $"MaxRetries: {MaxRetries}, " +
            $"BuildDate: {BuildDate}";

        // ---------------------------------------------------------------------
        // 17. IMPLEMENTING ABSTRACT EVENT
        // ---------------------------------------------------------------------

        public override event EventHandler OnDataChanged;

        // ---------------------------------------------------------------------
        // 18. IMPLEMENTING ABSTRACT INDEXER
        // ---------------------------------------------------------------------

        public override string this[int index]
        {
            get => "Value";
            set { }
        }

        // ---------------------------------------------------------------------
        // 19. IMPLEMENTING INTERFACE
        // ---------------------------------------------------------------------
        // A class can inherit from one class and implement multiple interfaces.
        // ---------------------------------------------------------------------

        public void Dispose()
        {
            // Cleanup code.
        }
    }


    // =========================================================================
    // PART E: ABSTRACT CLASS INHERITANCE RULES
    // =========================================================================

    // A class can inherit from only ONE class.
    //
    // ❌ A class cannot inherit from multiple abstract classes.
    //
    // A struct cannot inherit from an abstract class.
    // Only classes can inherit from classes.


    // =========================================================================
    // PART F: EXECUTION PLATFORM
    // =========================================================================

    internal class AbstractionRunner
    {
        public static void Run()
        {
            // =================================================================
            // 1. ABSTRACT CLASS CANNOT BE INITIALIZED DIRECTLY
            // =================================================================

            // AbstractionBase baseObject = new AbstractionBase();
            // ❌ Compile error:
            // Cannot create an instance of the abstract class.

            // =================================================================
            // 2. ABSTRACT CLASS REFERENCE CAN POINT TO DERIVED OBJECT
            // =================================================================

            AbstractionBase concept = new DerivedImplementationRunner();

            concept.Name = "Sanjay";

            Console.WriteLine(concept.Name);
            Console.WriteLine(concept.RuntimeId);

            // =================================================================
            // 3. ABSTRACT METHOD
            // =================================================================

            Console.WriteLine(
                concept.PrintMessage("Hello"));

            // =================================================================
            // 4. CONCRETE / VIRTUAL METHOD
            // =================================================================

            Console.WriteLine(
                concept.Discount(100));

            // =================================================================
            // 5. STATIC MEMBERS
            // =================================================================

            Console.WriteLine(AbstractionBase.ClassVersion);
            Console.WriteLine(AbstractionBase.MaxRetries);
            Console.WriteLine(AbstractionBase.BuildDate);
        }
    }
}


// =========================================================================
// PART G: QUICK INTERVIEW QUESTIONS
// =========================================================================
//
// 1. What is abstraction, and why do we use it?
// 2. What is an abstract class?
// 3. Can an abstract class be initialized?
// 4. How is an abstract class initialized?
// 5. Can an abstract class have a constructor?
// 6. Can an abstract class have a private constructor?
// 7. Can an abstract class have a static constructor?
// 8. Can an abstract class have concrete methods?
// 9. Can an abstract class have virtual methods?
// 10. Can an abstract class have static members?
// 11. Can an abstract class have fields?
// 12. Can an abstract class have abstract properties?
// 13. Can an abstract class have abstract events and indexers?
// 14. Can a non-abstract class have abstract members?
// 15. Can an abstract class be sealed?
// 16. Can an abstract method have a body?
// 17. Can an abstract class inherit from another abstract class?
// 18. Can a class inherit from multiple abstract classes?
// 19. Can a struct inherit from an abstract class?
// 20. What is the difference between an abstract class and an interface?
//
// =========================================================================