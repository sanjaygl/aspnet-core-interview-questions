namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Find the most frequent character in a string.
    /// Ignore spaces and return the first character encountered in case of ties.
    ///
    /// Example:
    /// Input:  "mississippi"
    /// Output: 'i'
    /// </summary>
    internal class FindMostFrequentCharacterProblem
    {
        public static char? FindMostFrequentCharacter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var counts = new Dictionary<char, int>();
            var order = new List<char>();

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
                    order.Add(c);
                }
            }

            if (counts.Count == 0)
                return null;

            int maxCount = counts.Values.Max();

            foreach (char c in order)
            {
                if (counts[c] == maxCount)
                    return c;
            }

            return null;
        }
    }
}
