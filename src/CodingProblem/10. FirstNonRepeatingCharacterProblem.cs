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

            var unique = new List<char>();
            var repeated = new HashSet<char>();

            foreach (char c in input)
            {
                if (repeated.Contains(c))
                    continue;

                if (unique.Contains(c))
                {
                    unique.Remove(c);
                    repeated.Add(c);
                }
                else
                {
                    unique.Add(c);
                }
            }

            return unique.Count > 0 ? unique[0] : (char?)null;
        }
    }
}