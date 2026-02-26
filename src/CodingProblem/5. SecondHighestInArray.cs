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
            // Use case: null input or array with less than 2 elements cannot have a second largest
            if (nums == null || nums.Length < 2)
                return null;

            // Initialize with MinValue to support negative numbers
            int first = int.MinValue;
            int second = int.MinValue;

            foreach (int num in nums)
            {
                // Case 1: Found a new largest number
                // Example: [5, 9] → first = 9, second = 5
                if (num > first)
                {
                    second = first;
                    first = num;
                }
                // Case 2: Update second largest
                // Conditions:
                // - Greater than current second
                // - Not equal to first (ensures distinct)
                // Example: [9, 8] → second = 8
                else if (num > second && num != first)
                {
                    second = num;
                }
            }

            // Use case: all elements are equal (e.g., [5,5,5])
            // second will remain MinValue → return null
            return second == int.MinValue ? (int?)null : second;
        }
    }
}