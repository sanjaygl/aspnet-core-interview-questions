namespace CodingProblem
{
    internal class FlattenNestedCollections
    {
        public static void FlattenAndPrint()
        {
            var numbers = new List<List<int>>
              {
                  new() { 1, 2, 2 },
                  new() { 3, 4 },
                  new() { 5 }
              };

            var flat = numbers.SelectMany(x => x).ToList();

            Console.WriteLine("Flattened Collection: " + string.Join(", ", flat));
        }
    }
}
