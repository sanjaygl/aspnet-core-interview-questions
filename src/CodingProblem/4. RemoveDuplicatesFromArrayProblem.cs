namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given an integer array, remove duplicate values and return a new array 
    /// that contains each distinct value only once.
    ///
    /// Example:
    /// Input:  [1, 2, 2, 3, 1]
    /// Output: [1, 2, 3]
    /// </summary>
    internal class RemoveDuplicatesFromArrayProblem
    {
        public static int[] RemoveDuplicates(int[] input)
        {
            if (input == null || input.Length == 0)
                return Array.Empty<int>();

            var seen = new HashSet<int>();
            var resultList = new List<int>();

            foreach (var number in input)
            {
                // HashSet.Add returns false if element already exists
                if (seen.Add(number))
                {
                    resultList.Add(number);
                }
            }

            return resultList.ToArray();
        }
    }
}