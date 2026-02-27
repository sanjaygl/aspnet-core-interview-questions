namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Count vowels (a, e, i, o, u) in a string.
    /// Ignore non-alphabetic characters and treat case-insensitively.
    ///
    /// Example:
    /// Input:  "Hello World!"
    /// Output: 3
    /// </summary>
    internal class CountVowelsInStringProblem
    {
        public static int CountVowels(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            int count = 0;

            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                    continue;

                char lower = char.ToLowerInvariant(c);
                if (lower == 'a' || lower == 'e' || lower == 'i' || lower == 'o' || lower == 'u')
                {
                    count++;
                }
            }

            return count;
        }
    }
}
