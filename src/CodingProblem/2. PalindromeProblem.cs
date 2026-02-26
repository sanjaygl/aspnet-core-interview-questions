namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Check whether a given string is a palindrome.
    /// A string is considered a palindrome if, after converting all letters to lowercase
    /// and removing all non-alphanumeric characters, it reads the same forward and backward.
    ///
    /// Example:
    /// Input:  "A man, a plan, a canal: Panama"
    /// Output: true
    /// </summary>
    internal class Palindrome
    {
        public static bool CheckPalindrome(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            input = new string(input
                .Where(c => char.IsLetterOrDigit(c))
                .Select(c => char.ToLower(c))
                .ToArray());

            int left = 0;
            int right = input.Length - 1;

            while (left < right)
            {
                if (input[left] != input[right])
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }

        // Using LINQ
        public static bool IsPalindromeLinq(string s)
        {
            string reversed = new string(s.Reverse().ToArray());
            return s.Equals(reversed, StringComparison.OrdinalIgnoreCase);
        }
    }
}