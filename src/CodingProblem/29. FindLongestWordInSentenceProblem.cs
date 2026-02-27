using System.Text;

namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Find the longest word in a sentence.
    /// Ignore punctuation, and if multiple words tie, return the first one.
    ///
    /// Example:
    /// Input:  "The quick brown fox jumps over the lazy dog"
    /// Output: "quick"
    /// </summary>
    internal class FindLongestWordInSentenceProblem
    {
        public static string FindLongestWord(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string longest = string.Empty;
            var current = new StringBuilder();

            void FinalizeCurrentWord()
            {
                if (current.Length == 0)
                    return;

                if (current.Length > longest.Length)
                {
                    longest = current.ToString();
                }

                current.Clear();
            }

            foreach (char c in input)
            {
                if (char.IsLetterOrDigit(c))
                {
                    current.Append(c);
                }
                else
                {
                    FinalizeCurrentWord();
                }
            }

            FinalizeCurrentWord();
            return longest;
        }
    }
}
