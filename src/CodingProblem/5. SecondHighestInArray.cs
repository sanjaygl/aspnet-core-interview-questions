using System;

namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given a non-empty array of integers, find the second largest distinct element.
    /// If no second distinct element exists, return null.
    ///
    /// Example:
    /// Input:  [5, 1, 9, 3, 9]
    /// Output: 5
    /// </summary>
    internal class SecondHighestInArray
    {
        public static int? FindSecondHighest(int[] nums)
        {
            if (nums == null || nums.Length < 2)
                return null;

            int first = int.MinValue;
            int second = int.MinValue;

            foreach (int num in nums)
            {
                if (num > first)
                {
                    second = first;
                    first = num;
                }
                else if (num > second && num != first)
                {
                    second = num;
                }
            }

            // If second was never updated, no second distinct element exists
            return second == int.MinValue ? (int?)null : second;
        }
    }
}