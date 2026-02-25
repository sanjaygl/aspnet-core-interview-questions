namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given an integer array and a target value, return the indices of two numbers
    /// such that they add up to the target.
    ///
    /// Example:
    /// Input:  [2, 7, 11, 15], target = 9
    /// Output: [0, 1]
    /// </summary>
    internal class TwoSumProblem
    {
        // This method returns the indices of the two numbers that add up to the target
        // Time Complexity: O(n^2) - uses nested loops to check all pairs
        // Space Complexity: O(1) - no additional data structures used
        public static int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new int[] { i, j };
                    }
                }
            }

            return new int[] { -1, -1 };
        }

        // This method returns the indices of the two numbers that add up to the target using a hash map
        // Time Complexity: O(n) - single pass through the array
        // Space Complexity: O(n) - hash map stores up to n elements
        public static int[] TwoSumArrayIndex(int[] nums, int target)
        {
            var map = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];

                if (map.ContainsKey(complement))
                {
                    return new int[] { map[complement], i };
                }

                map[nums[i]] = i;
            }

            return new int[] { -1, -1 };
        }

        // This method returns the actual values instead of their indices
        // Time Complexity: O(n) - single pass through the array
        // Space Complexity: O(n) - hash map stores up to n elements
        public static int[] TwoSumValues(int[] nums, int target)
        {
            var map = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];

                if (map.ContainsKey(complement))
                {
                    return new int[] { complement, nums[i] };
                }

                map[nums[i]] = i;
            }

            return new int[] { -1, -1 };
        }
    }
}
