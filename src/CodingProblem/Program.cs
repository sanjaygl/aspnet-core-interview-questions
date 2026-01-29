namespace CodingProblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Fibonacci.GetFibonacci(1)); // Example call to avoid unused method warning
            Console.WriteLine(Fibonacci.GetFibonacciSeries(1)); // Example call to avoid unused method warning

            SwapNumbers.SwapNumbersDemo();

            FindDuplicates.FindDuplicatesInArray([1, 2, 3, 2, 4, 5, 5]);
            FindDuplicates.FindDuplicateItem([1, 2, 3, 4, 5, 3]);

            Factorial.GetFactorial(5); // Example call to avoid unused method warning

            int anum = 123;
            Object obj = anum;
            Console.WriteLine(anum);
            Console.WriteLine(obj);

            Singleton singleton1 = Singleton.GetInstance;
            singleton1.DoWork();
            Logger logger = Logger.GetInstance;
            logger.Log("Hello");

            FindUpperCase.FindUpperCaseIterative("abcdeGTXYZqghABCD");

            SecondHighestInArray.FindSecondHighest(new int[] { 10, 5, 8, 12, 7 });

            FlattenNestedCollections.FlattenAndPrint();

            Yield.Test();

            Console.ReadLine();
        }
    }
}
