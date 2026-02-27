namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Count total words in a string.
    /// Words are separated by one or more spaces.
    ///
    /// Example:
    /// Input:  " The quick brown fox "
    /// Output: 4
    /// </summary>
    internal class CountWordsInStringProblem
    {
        public static int CountWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            int wordCount = 0;
            bool inWord = false;

            foreach (char c in input)
            {
                if (c == ' ')
                {
                    inWord = false;
                    continue;
                }

                if (!inWord)
                {
                    wordCount++;
                    inWord = true;
                }
            }

            return wordCount;
        }
    }
}
