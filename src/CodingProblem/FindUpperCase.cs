namespace CodingProblem
{
    internal class FindUpperCase
    {
        public static void FindUpperCaseIterative(string input)
        {
            var result = FindUpperCaseRecursive(input);
            Console.WriteLine(result);
        }

        // Write a recursive C# program to find the first uppercase character in a string.
        public static char FindUpperCaseRecursive(string input, int index = 0)
        {
            if (string.IsNullOrEmpty(input) || index >= input.Length)
            {
                return '\0';
            }

            if (char.IsUpper(input[index]))
            {
                return input[index];
            }

            return FindUpperCaseRecursive(input, index + 1);
        }
    }
}
