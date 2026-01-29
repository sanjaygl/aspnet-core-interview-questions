namespace CodingProblem
{
    internal class Yield
    {
        internal static void Test()
        {
            var result1 = GetEvenNumbers(10);
            foreach (var item in result1)
            {
                Console.WriteLine($"Without Yield {item}");
            }

            var result2 = GetEvenNumbersUpTo(10);
            foreach (var item in result2)
            {
                Console.WriteLine($"WIth Yield {item}");
            }
        }

        public static IEnumerable<int> GetEvenNumbers(int max)
        {
            var result = new List<int>();

            for (int i = 0; i <= max; i++)
            {
                if (i % 2 == 0)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public static IEnumerable<int> GetEvenNumbersUpTo(int max)
        {
            for (int i = 0; i <= max; i++)
            {
                if (i % 2 == 0)
                {
                    yield return i;
                }
            }
        }
    }
}
