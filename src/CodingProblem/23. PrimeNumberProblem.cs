namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given a positive integer n, determine whether n is a prime number.
    /// A prime number is greater than 1 and divisible only by 1 and itself.
    ///
    /// Example:
    /// Input:  29
    /// Output: true
    /// </summary>
    internal class PrimeNumberProblem
    {
        public static bool IsPrime(int n)
        {
            if (n <= 1)
            {
                return false;
            }

            if (n == 2)
            {
                return true;
            }

            if (n % 2 == 0)
            {
                return false;
            }

            int limit = (int)Math.Sqrt(n);
            for (int i = 3; i <= limit; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
