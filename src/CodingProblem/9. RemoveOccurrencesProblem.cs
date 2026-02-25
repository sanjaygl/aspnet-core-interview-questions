namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given an array of integers and a target value, remove all occurrences 
    /// of the target and return the new array.
    ///
    /// Example:
    /// Input:  [1, 2, 3, 2, 4], target = 2
    /// Output: [1, 3, 4]
    /// </summary>
    internal class RemoveOccurrencesProblem
    {
        public static int[] RemoveOccurrences(int[] input, int target)
        {
            if (input == null || input.Length == 0)
                return Array.Empty<int>();

            var result = new List<int>();

            foreach (var number in input)
            {
                if (number != target)
                {
                    result.Add(number);
                }
            }

            return result.ToArray();
        }
    }
}