namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given a string, return the first character that does not repeat.
    /// If all characters repeat, return null.
    ///
    /// Example:
    /// Input:  "swiss"
    /// Output: 'w'
    /// </summary>
    internal class FirstNonRepeatingCharacterProblem
    {
        public static char? FindFirstNonRepeating(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            // Step 1: Count frequencies
            var charCounts = new Dictionary<char, int>();
            foreach (char c in input)
            {
                charCounts[c] = charCounts.GetValueOrDefault(c, 0) + 1;
            }

            // Step 2: Find the first unique character
            foreach (char c in input)
            {
                if (charCounts[c] == 1)
                    return c;
            }

            return null;
        }
    }
}