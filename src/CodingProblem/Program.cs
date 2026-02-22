using ThreadingPractice;

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

            FindFirstUppercaseRecursive.FirstUppercaseProblem("abcdeGTXYZqghABCD");
            SecondHighestInArray.FindSecondHighest(new int[] { 10, 5, 8, 12, 7 });
            FlattenNestedCollections.FlattenAndPrint();
            Yield.Test();
            JaggedArray.JaggedArrayDemo();
            Console.WriteLine(string.Join(", ", TwoSumProblem.TwoSumValues(new int[] { 2, 7, 11, 15 }, 9)));
            GroupAnagramsProblem.GroupAnagrams();

            ThreadExample threadExample = new ThreadExample();
            //threadExample.ProcessData();
            //threadExample.ProcessDataInBackground();
            threadExample.CuncurrancyExample();

            BankAccount bankAccount = new BankAccount(1000);
            bankAccount.Run();

            DeadlockExample deadlockExample = new DeadlockExample();
            //deadlockExample.Run();

            var customDictionary = new CustomDictionary<int, string>();
            customDictionary.Add(1, "One");
            customDictionary.Add(2, "Two");
            customDictionary.Add(3, "Three");
            customDictionary.Add(4, "Four");
            customDictionary.Add(5, "Five");
            customDictionary.Add(6, "Six");

            Console.WriteLine(customDictionary.Get(1));
            customDictionary.Remove(1);
            Console.WriteLine(customDictionary.ContainsKey(1));

            for (int i = 1; i <= 6; i++)
            {
                try
                {
                    Console.WriteLine($"Key: {i}, Value: {customDictionary.Get(i)}");
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadLine();
        }
    }
}
