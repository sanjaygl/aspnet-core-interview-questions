namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given an integer array, find all elements that appear more than once.
    ///
    /// Example:
    /// Input:  [1, 2, 3, 2, 4, 5, 5]
    /// Output: [2, 5]
    /// </summary>
    internal class FindDuplicates
    {
        // Write a function that finds all duplicate numbers in an array.
        public static string FindDuplicatesInArray(int[] nums)
        {
            List<int> seen = new List<int>();
            List<int> duplicates = new List<int>();

            foreach (int num in nums)
            {
                if (seen.Contains(num))
                {
                    if (!duplicates.Contains(num))
                    {
                        duplicates.Add(num);
                    }
                }
                else
                {
                    seen.Add(num);
                }
            }
            Console.WriteLine("Duplicate numbers found: " + string.Join(", ", duplicates));
            return string.Join(", ", duplicates);
        }

        public static string FindDuplicateItem(int[] arr)
        {
            var duplicates = arr.GroupBy(x => x)
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Key)
                             .ToList();

            Console.WriteLine("Duplicate numbers found: " + string.Join(", ", duplicates));
            return string.Join(", ", duplicates);
        }
    }
}
