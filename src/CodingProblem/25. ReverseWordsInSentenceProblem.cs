namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Reverse each word in a sentence while preserving word order and spaces.
    ///
    /// Example:
    /// Input:  "Hello World"
    /// Output: "olleH dlroW"
    /// </summary>
    internal class ReverseWordsInSentenceProblem
    {
        public static string ReverseWords(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            char[] chars = input.ToCharArray();
            int index = 0;

            while (index < chars.Length)
            {
                if (chars[index] == ' ')
                {
                    index++;
                    continue;
                }

                int start = index;
                while (index < chars.Length && chars[index] != ' ')
                {
                    index++;
                }

                int end = index - 1;
                while (start < end)
                {
                    (chars[start], chars[end]) = (chars[end], chars[start]);
                    start++;
                    end--;
                }
            }

            return new string(chars);
        }
    }
}
