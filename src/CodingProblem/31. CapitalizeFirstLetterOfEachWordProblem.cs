namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Capitalize the first letter of each word in a sentence.
    /// Keep all other characters unchanged.
    ///
    /// Example:
    /// Input:  "hello world"
    /// Output: "Hello World"
    /// </summary>
    internal class CapitalizeFirstLetterOfEachWordProblem
    {
        public static string CapitalizeWords(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            char[] chars = input.ToCharArray();
            bool startOfWord = true;

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == ' ')
                {
                    startOfWord = true;
                    continue;
                }

                if (startOfWord)
                {
                    chars[i] = char.ToUpperInvariant(chars[i]);
                    startOfWord = false;
                }
            }

            return new string(chars);
        }
    }
}
