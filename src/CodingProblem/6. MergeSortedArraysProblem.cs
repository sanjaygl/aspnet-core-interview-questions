using System;

namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// You are given two sorted integer arrays. Merge them into a single sorted array 
    /// without using any built-in sort methods on the final result.
    ///
    /// Example:
    /// Input:  [1, 3, 5], [2, 4, 6]
    /// Output: [1, 2, 3, 4, 5, 6]
    /// </summary>
    internal class MergeSortedArraysProblem
    {
        public static int[] Merge(int[] arr1, int[] arr2)
        {
            if (arr1 == null || arr1.Length == 0)
                return arr2 ?? Array.Empty<int>();

            if (arr2 == null || arr2.Length == 0)
                return arr1;

            int i = 0, j = 0, k = 0;
            int[] result = new int[arr1.Length + arr2.Length];

            // Compare elements and merge in sorted order
            while (i < arr1.Length && j < arr2.Length)
            {
                if (arr1[i] <= arr2[j])
                {
                    result[k++] = arr1[i++];
                }
                else
                {
                    result[k++] = arr2[j++];
                }
            }

            // Copy remaining elements from arr1 (if any)
            while (i < arr1.Length)
            {
                result[k++] = arr1[i++];
            }

            // Copy remaining elements from arr2 (if any)
            while (j < arr2.Length)
            {
                result[k++] = arr2[j++];
            }

            return result;
        }
    }
}