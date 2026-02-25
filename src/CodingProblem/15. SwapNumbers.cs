namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Swap two integer values without using a temporary variable.
    ///
    /// Example:
    /// Input:  a = 5, b = 10
    /// Output: a = 10, b = 5
    /// </summary>
    internal class SwapNumbers
    {
        // Write a function that swaps two numbers without using a temporary variable.
        public static void SwapNumbersDemo()
        {
            int a = 5;
            int b = 10;
            Console.WriteLine($"Before Swap: a = {a}, b = {b}");
            Swap(ref a, ref b);
            Console.WriteLine($"After Swap: a = {a}, b = {b}");
        }

        public static void Swap(ref int a, ref int b)
        {
            a = a + b;  // a = 15, b = 10
            b = a - b;  // a = 15, b = 5
            a = a - b;  // a = 10, b = 5
        }
    }
}
