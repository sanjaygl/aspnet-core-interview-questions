namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given an array containing n distinct numbers taken from the range 0 to n,
    /// exactly one number is missing. Find and return the missing number.
    ///
    /// Example:
    /// Input:  [3, 0, 1]
    /// Output: 2
    /// </summary>
    internal class FindMissingNumberProblem
    {
        public static int FindMissingNumber(int[] nums)
        {
            if (nums == null)
                throw new ArgumentNullException(nameof(nums));

            int n = nums.Length;

            // Expected sum of numbers from 0 to n
            int expectedSum = 0;

            for (int i = 0; i <= n; i++)
            {
                expectedSum += i;
            }

            int actualSum = 0;
            foreach (var num in nums)
            {
                actualSum += num;
            }

            return expectedSum - actualSum;
        }
    }
}