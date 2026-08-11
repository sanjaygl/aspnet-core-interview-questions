namespace OOP_CSharp_Concepts
{
    /// <summary>
    /// Defines a type-safe function pointer signature for mathematical operations.
    /// It accepts two integers and returns void.
    /// </summary>
    public delegate void MathOperation(int a, int b);

    /// <summary>
    /// Contains target mathematical methods that match the MathOperation delegate signature.
    /// </summary>
    internal class DelegateExample
    {
        public void Add(int a, int b)
        {
            Console.WriteLine($"Addition       : {a} + {b} = {a + b}");
        }

        public void Substract(int a, int b)
        {
            Console.WriteLine($"Subtraction    : {a} - {b} = {a - b}");
        }

        public void Multiply(int a, int b)
        {
            Console.WriteLine($"Multiplication : {a} * {b} = {a * b}");
        }

        /// <summary>
        /// Performs integer division with a built-in safety check to avoid DivideByZeroException.
        /// </summary>
        public void Division(int a, int b)
        {
            // Defensive programming check for zero denominator
            if (b == 0)
            {
                Console.WriteLine("Division       : Error (Cannot divide by zero)");
                return;
            }
            Console.WriteLine($"Division       : {a} / {b} = {a / b}");
        }
    }

    /// <summary>
    /// Executes the delegate examples to demonstrate single-cast and multicast workflows.
    /// </summary>
    internal class DelegateRunner
    {
        public static void Run()
        {
            // Create an instance of the class containing our target methods
            DelegateExample example = new DelegateExample();

            // 1. Single-Cast Delegate: Pointing to a single method
            Console.WriteLine("--- Single-Cast Delegate Execution ---");
            MathOperation del = example.Add;
            del(2, 4); // Executes only example.Add
            Console.WriteLine();

            // 2. Multicast Delegate: Appending multiple functions to the invocation chain
            // The compiler updates the internal list of methods to execute sequentially
            del += example.Substract;
            del += example.Multiply;
            del += example.Division;

            Console.WriteLine("--- Multicast Delegate Execution (Inputs: 10, 5) ---");
            del(10, 5); // Executes Add, Subtract, Multiply, and Division in order
            Console.WriteLine();

            // 3. Testing safety check (Demonstrating error handling inside multicast chain)
            Console.WriteLine("--- Multicast Delegate Execution (Inputs: 10, 0) ---");
            del(10, 0); // Executes all operations; division safely handles zero without crashing
        }
    }
}
