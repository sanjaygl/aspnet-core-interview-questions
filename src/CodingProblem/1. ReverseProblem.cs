using System.Text;

namespace CodingProblem
{
    // Given a non-empty string, write a function to return a new string that is the reverse of the input. You must not use any built-in reverse methods.
    internal class ReverseProblem
    {
        public static string ReverseString(string input)
        {
            StringBuilder result = new StringBuilder();

            for (int i = input.Length - 1; i >= 0; i--)
            {
                result.Append(input[i]);
            }

            return result.ToString();
        }
    }
}
