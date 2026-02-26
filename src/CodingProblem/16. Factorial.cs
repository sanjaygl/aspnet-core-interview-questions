namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given a non-negative integer n, return n! where
    /// n! = n * (n - 1) * ... * 1 and 0! = 1.
    ///
    /// Example:
    /// Input:  5
    /// Output: 120
    /// </summary>
    internal class Factorial
    {
        public static int GetFactorialRecursive(int n)
        {
            if (n == 0 || n == 1)
            {
                return 1;
            }

            return n * GetFactorialRecursive(n - 1);
        }

        public static int GetFactorial(int n)
        {
            if (n == 0 || n == 1)
            {
                return 1;
            }

            int result = 1;

            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}
