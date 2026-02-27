namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Determine whether two strings are anagrams.
    /// Ignore spaces and punctuation, and compare case-insensitively.
    ///
    /// Example:
    /// Input:  "Listen", "Silent"
    /// Output: true
    /// </summary>
    internal class CheckAnagramStringsProblem
    {
        public static bool AreAnagrams(string first, string second)
        {
            if (first == null || second == null)
                return false;

            string normalizedFirst = Normalize(first);
            string normalizedSecond = Normalize(second);

            if (normalizedFirst.Length != normalizedSecond.Length)
                return false;

            var counts = new Dictionary<char, int>();

            foreach (char c in normalizedFirst)
            {
                if (counts.TryGetValue(c, out int value))
                {
                    counts[c] = value + 1;
                }
                else
                {
                    counts[c] = 1;
                }
            }

            foreach (char c in normalizedSecond)
            {
                if (!counts.TryGetValue(c, out int value))
                    return false;

                if (value == 1)
                {
                    counts.Remove(c);
                }
                else
                {
                    counts[c] = value - 1;
                }
            }

            return counts.Count == 0;
        }

        private static string Normalize(string input)
        {
            return new string(input
                .Where(char.IsLetterOrDigit)
                .Select(char.ToLowerInvariant)
                .ToArray());
        }
    }
}
