namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given two integer arrays, return an array containing the distinct elements 
    /// that are present in both arrays.
    ///
    /// Example:
    /// Input:  [1, 2, 2, 3], [2, 3, 4]
    /// Output: [2, 3]
    /// </summary>
    internal class IntersectionOfArraysProblem
    {
        public static int[] FindIntersection(int[] nums1, int[] nums2)
        {
            if (nums1 == null || nums2 == null || nums1.Length == 0 || nums2.Length == 0)
                return Array.Empty<int>();

            // Use a HashSet to store unique elements of the first array for O(1) lookups
            var set1 = new HashSet<int>(nums1);
            var result = new HashSet<int>();

            foreach (var num in nums2)
            {
                // If the number from the second array is in the first set, add it to the result set
                if (set1.Contains(num))
                {
                    result.Add(num);
                }
            }

            return result.ToArray();
        }
    }
}