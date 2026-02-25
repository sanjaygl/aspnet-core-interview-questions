namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Compute Fibonacci values where F(0) = 0, F(1) = 1, and
    /// F(n) = F(n - 1) + F(n - 2). Support nth number and series generation.
    ///
    /// Example:
    /// Input:  n = 7
    /// Output: 13
    /// </summary>
    internal class Fibonacci
    {
        public static int FibonacciNumberRecursive(int n)
        {
            if (n <= 1)
            {
                return n;
            }

            return FibonacciNumberRecursive(n - 1) + FibonacciNumberRecursive(n - 2);
        }

        public static int GetFibonacci(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            int first = 0;
            int second = 1;
            int nfibNumber = 0;

            for (int i = 2; i <= n; i++)
            {
                nfibNumber = first + second;
                first = second;
                second = nfibNumber;
            }

            Console.WriteLine($"F({n}) = {nfibNumber}");
            return nfibNumber;
        }

        public static string GetFibonacciSeries(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Input must be a non-negative integer.");
            }

            List<int> series = new List<int>();

            if (n >= 0)
            {
                series.Add(0);
            }

            if (n >= 1)
            {
                series.Add(1);
            }

            if (n <= 1)
            {
                Console.WriteLine($"Fibonacci series from F(0) to F({n}): {string.Join(", ", series)}");
                return string.Join(", ", series);
            }

            int first = 0;
            int second = 1;

            for (int i = 2; i <= n; i++)
            {
                int nfibNumber = first + second;

                series.Add(nfibNumber);

                first = second;

                second = nfibNumber;
            }

            Console.WriteLine($"Fibonacci series from F(0) to F({n}): {string.Join(", ", series)}");
            return string.Join(", ", series);
        }
    }
}
