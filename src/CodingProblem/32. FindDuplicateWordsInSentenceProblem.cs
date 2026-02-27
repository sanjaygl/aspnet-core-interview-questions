namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Find duplicate words in a sentence and return their counts.
    /// Ignore punctuation and compare words case-insensitively.
    ///
    /// Example:
    /// Input:  "This is is a test Test"
    /// Output: { is:2, test:2 }
    /// </summary>
    internal class FindDuplicateWordsInSentenceProblem
    {
        public static Dictionary<string, int> FindDuplicateWords(string input)
        {
            var counts = new Dictionary<string, int>();

            if (string.IsNullOrWhiteSpace(input))
                return counts;

            foreach (string word in ExtractWords(input))
            {
                if (counts.TryGetValue(word, out int value))
                {
                    counts[word] = value + 1;
                }
                else
                {
                    counts[word] = 1;
                }
            }

            return counts
                .Where(kvp => kvp.Value > 1)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private static IEnumerable<string> ExtractWords(string input)
        {
            var chars = new List<char>();

            foreach (char c in input)
            {
                if (char.IsLetterOrDigit(c))
                {
                    chars.Add(char.ToLowerInvariant(c));
                    continue;
                }

                if (chars.Count > 0)
                {
                    yield return new string(chars.ToArray());
                    chars.Clear();
                }
            }

            if (chars.Count > 0)
            {
                yield return new string(chars.ToArray());
            }
        }
    }
}
