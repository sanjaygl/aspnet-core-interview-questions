namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given a nested list of integers, flatten it into a single list.
    ///
    /// Example:
    /// Input:  [[1, 2, 2], [3, 4], [5]]
    /// Output: [1, 2, 2, 3, 4, 5]
    /// </summary>
    internal class FlattenNestedCollections
    {
        public static void FlattenAndPrint()
        {
            var input = new List<List<int>>
              {
                  new List<int> { 1, 2, 2 },
                  new List<int> { 3, 4 },
                  new List<int> { 5 }
              };

            var result = Flatten(input);

            Console.WriteLine("Flattened Collection: " + string.Join(", ", result));
        }

        public static List<int> Flatten(List<List<int>> nestedList)
        {
            return nestedList.SelectMany(x => x).ToList();
        }

        public static List<int> FlattenList(List<List<int>> nestedList)
        {
            var result = new List<int>();

            foreach (var list in nestedList)
            {
                foreach (var item in list)
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
