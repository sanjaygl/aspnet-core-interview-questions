namespace OOP_CSharp_Concepts
{
    // reference : C:\Personal\aspnet-core-interview-questions\docs\CSharp\CSharp-Yield-Keyword-Complete-Guide.md
    public class YieldBasics
    {
        // Traditional approach - eager evaluation
        public List<int> GetNumbersTraditional()
        {
            var numbers = new List<int>();
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);
            return numbers; // All numbers created in memory
        }

        // yield approach - lazy evaluation
        public IEnumerable<int> GetNumbersWithYield()
        {
            Console.WriteLine("Generating 1");
            yield return 1;
            Console.WriteLine("Generating 2");
            yield return 2;
            Console.WriteLine("Generating 3");
            yield return 3;
        }
    }

    public class YieldRunner
    {
        public static void Run()
        {
            var demo = new YieldBasics();

            Console.WriteLine("=== Traditional Approach ===");
            var traditional = demo.GetNumbersTraditional(); // All executed immediately

            Console.WriteLine("\n=== yield Approach ===");
            var lazy = demo.GetNumbersWithYield(); // Nothing executed yet!
            Console.WriteLine("Created enumerator");

            Console.WriteLine("\nNow iterating:");
            foreach (var num in lazy) // Now it executes
            {
                Console.WriteLine($"Got: {num}");
            }
        }
    }
}