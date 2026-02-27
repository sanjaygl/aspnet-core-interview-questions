namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Count the total number of numeric digits (0-9) in a string.
    ///
    /// Example:
    /// Input:  "Order123Number45"
    /// Output: 5
    /// </summary>
    internal class CountDigitsInStringProblem
    {
        public static int CountDigits(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            int count = 0;

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
