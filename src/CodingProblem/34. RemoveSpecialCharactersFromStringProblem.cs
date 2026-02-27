using System.Text;

namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Remove special characters from a string.
    /// Keep only letters, digits, and spaces.
    ///
    /// Example:
    /// Input:  "Hello@ World#2024!"
    /// Output: "Hello World2024"
    /// </summary>
    internal class RemoveSpecialCharactersFromStringProblem
    {
        public static string RemoveSpecialCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var result = new StringBuilder(input.Length);

            foreach (char c in input)
            {
                if (char.IsLetterOrDigit(c) || c == ' ')
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}
