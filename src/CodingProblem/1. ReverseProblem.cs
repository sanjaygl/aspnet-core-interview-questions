using System.Text;

namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given a non-empty string, return a new string that is the reverse of the input.
    /// Do not use any built-in reverse methods.
    ///
    /// Example:
    /// Input:  "interview"
    /// Output: "weivretni"
    /// </summary>
    internal class ReverseProblem
    {
        public static string ReverseString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder result = new StringBuilder(input.Length);

            for (int i = input.Length - 1; i >= 0; i--)
            {
                result.Append(input[i]);
            }

            return result.ToString();
        }
    }
}