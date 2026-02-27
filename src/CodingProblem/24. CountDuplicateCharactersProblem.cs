namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given a string, return duplicate characters with their total occurrences.
    /// Ignore spaces and treat uppercase/lowercase as different characters.
    ///
    /// Example:
    /// Input:  "the quick brown fox jumps over the lazy dog"
    /// Output: { t:2, h:2, e:3, u:2, r:2, o:4 }
    /// </summary>
    internal class CountDuplicateCharactersProblem
    {
        public static Dictionary<char, int> CountDuplicateCharacters(string input)
        {
            var counts = new Dictionary<char, int>();

            if (string.IsNullOrEmpty(input))
                return counts;

            foreach (char c in input)
            {
                if (c == ' ')
                    continue;

                if (counts.TryGetValue(c, out int value))
                {
                    counts[c] = value + 1;
                }
                else
                {
                    counts[c] = 1;
                }
            }

            return counts
                .Where(kvp => kvp.Value > 1)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
