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

            var set1 = new HashSet<int>(nums1);
            var result = new HashSet<int>();

            foreach (var num in nums2)
            {
                if (set1.Contains(num))
                {
                    result.Add(num);
                }
            }

            return result.ToArray();
        }
    }
}