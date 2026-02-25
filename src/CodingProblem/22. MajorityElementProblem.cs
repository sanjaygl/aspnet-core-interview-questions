namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given an array of integers, find the element that appears more than n/2 times.
    /// If no such element exists, return null.
    ///
    /// Example:
    /// Input:  [2, 2, 1, 2, 3, 2, 2]
    /// Output: 2
    /// </summary>
    internal class MajorityElementProblem
    {
        public static int? FindMajorityElement(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return null;

            // Phase 1: Find candidate
            int candidate = 0;
            int count = 0;

            foreach (var num in nums)
            {
                if (count == 0)
                {
                    candidate = num;
                    count = 1;
                }
                else if (num == candidate)
                {
                    count++;
                }
                else
                {
                    count--;
                }
            }

            // Phase 2: Verify candidate
            count = 0;
            foreach (var num in nums)
            {
                if (num == candidate)
                    count++;
            }

            return count > nums.Length / 2 ? candidate : (int?)null;
        }
    }
}