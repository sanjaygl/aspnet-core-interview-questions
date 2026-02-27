using System.Text;

namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Remove duplicate characters from a string while preserving original order.
    /// Keep only the first occurrence of each character.
    ///
    /// Example:
    /// Input:  "programming"
    /// Output: "progamin"
    /// </summary>
    internal class RemoveDuplicateCharactersFromStringProblem
    {
        public static string RemoveDuplicateCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var seen = new HashSet<char>();
            var result = new StringBuilder(input.Length);

            foreach (char c in input)
            {
                if (seen.Add(c))
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}
