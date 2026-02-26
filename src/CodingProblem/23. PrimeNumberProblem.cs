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
        public static bool IsPrimeNumber(int number)
        {
            if (number <= 1)
            {
                return false;
            }

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
