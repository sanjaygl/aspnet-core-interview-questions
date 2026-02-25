using System.Text.RegularExpressions;

namespace CodingProblem
{
    //A phrase is a palindrome if, after converting all uppercase letters into lowercase letters and removing all non-alphanumeric characters, it reads the same forward and backward. Alphanumeric characters include letters and numbers.
    internal class Palindrome
    {
        public static bool CheckPalindrome(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            input = new string(input
                .Where(char.IsLetterOrDigit)
                .Select(char.ToLower)
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