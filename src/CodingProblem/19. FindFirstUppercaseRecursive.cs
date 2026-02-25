namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Using recursion, find the first uppercase character in a string.
    /// Return '\0' if no uppercase character exists.
    ///
    /// Example:
    /// Input:  "abcdeGTXYZqghABCD"
    /// Output: 'G'
    /// </summary>
    internal class FindFirstUppercaseRecursive
    {
        public static void FirstUppercaseProblem(string input)
        {
            var result = RecursiveFirstUppercase(input);
            Console.WriteLine(result);
        }

        // Write a recursive C# program to find the first uppercase character in a string.
        public static char RecursiveFirstUppercase(string input, int index = 0)
        {
            if (string.IsNullOrEmpty(input) || index >= input.Length)
            {
                return '\0';
            }

            if (char.IsUpper(input[index]))
            {
                return input[index];
            }

            return RecursiveFirstUppercase(input, index + 1);
        }
    }
}
