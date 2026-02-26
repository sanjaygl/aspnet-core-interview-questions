using ThreadingPractice;

namespace CodingProblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Problem 1: Reverse a string
            Console.WriteLine("\n Problem 1: Reverse a string \n");
            Console.WriteLine(ReverseProblem.ReverseString("interview"));

            // Problem 2: Check if a string is a palindrome
            Console.WriteLine("\n Problem 2: Check if a string is a palindrome \n");
            Console.WriteLine(Palindrome.CheckPalindrome("A man, a plan, a canal: Panama"));

            // Problem 3: Count character frequencies in a string
            Console.WriteLine("\n Problem 3: Count character frequencies in a string \n");
            var frequencies = CountCharacterFrequenciesProblem.CountCharacterFrequencies("Hello World");
            foreach (var kvp in frequencies)
            {
                Console.WriteLine($" Key: {kvp.Key} : Value: {kvp.Value}");
            }

            // Problem 4: Remove duplicates from an array
            Console.WriteLine("\n Problem 4: Remove duplicates from an array \n");
            var removeDuplicates = RemoveDuplicatesFromArrayProblem.RemoveDuplicates(new int[] { 1, 2, 3, 2, 4, 5, 5 });
            foreach (var num in removeDuplicates)
            {
                Console.Write($" {num}");
            }

            // Problem 5: Find the second highest element in an array
            Console.WriteLine("\n Problem 5: Find the second highest element in an array \n");
            Console.Write($"Second highest element: {SecondHighestInArray.FindSecondHighest(new int[] { 10, 5, 8, 12, 7 })}");

            // Problem 6: Merge Two Sorted Arrays
            Console.WriteLine("\n Problem 6: Merge Two Sorted Arrays \n");
            Console.WriteLine(string.Join(", ", MergeSortedArraysProblem.Merge(new int[] { 1, 3, 5 }, new int[] { 2, 4, 6 })));

            // Problem 7: Group Anagrams
            Console.WriteLine("\n Problem 7: Group Anagrams \n");
            var groupedAnagrams = GroupAnagramsProblem.GroupAnagrams(new string[] { "eat", "tea", "tan", "ate", "nat", "bat" });
            foreach (var group in groupedAnagrams)
            {
                Console.WriteLine($"[{string.Join(", ", group)}]");
            }

            // Problem 9: Remove Occurrences of a Substring
            Console.WriteLine("\n Problem 9: Remove Occurrences of a Substring \n");
            Console.WriteLine(string.Join(", ", RemoveOccurrencesProblem.RemoveOccurrences(new int[] { 1, 2, 3, 2, 4 }, 2)));

            // Problem 10: Find First Non-Repeating Character
            Console.WriteLine("\n Problem 10: Find First Non-Repeating Character \n");
            Console.WriteLine(FirstNonRepeatingCharacterProblem.FindFirstNonRepeating("leetcode"));

            // Problem 11: Sum of Digits
            Console.WriteLine("\n Problem 11: Sum of Digits \n");
            Console.WriteLine(SumOfDigitsProblem.SumOfDigits(12345));

            // Problem 12: Intersection of Arrays
            Console.WriteLine("\n Problem 12: Intersection of Arrays \n");
            Console.WriteLine(string.Join(", ", IntersectionOfArraysProblem.FindIntersection(new int[] { 1, 2, 2, 3 }, new int[] { 2, 3, 4 })));

            // Problem 13: Find Missing Number
            Console.WriteLine("\n Problem 13: Find Missing Number \n");
            Console.WriteLine(FindMissingNumberProblem.FindMissingNumber(new int[] { 1, 2, 4, 5, 6 }));

            // Problem 14: Two Sum
            Console.WriteLine("\n Problem 14: Two Sum \n");
            Console.WriteLine(string.Join(", ", TwoSumProblem.TwoSumArrayIndex(new int[] { 2, 7, 11, 15 }, 9)));

            // Problem 15: Swap Numbers
            Console.WriteLine("\n Problem 15: Swap Numbers \n");
            SwapNumbers.SwapNumbersDemo();

            // Problem 16: Factorial
            Console.WriteLine("\n Problem 16: Factorial \n");
            Console.WriteLine(Factorial.GetFactorial(5));

            // Problem 17: Fibonacci
            Console.WriteLine("\n Problem 17: Fibonacci \n");
            Console.WriteLine(Fibonacci.GetFibonacci(7));

            // Problem 23: Check if a number is prime
            Console.WriteLine("\n Problem 23: Check if a number is prime \n");
            Console.WriteLine(PrimeNumberProblem.IsPrimeNumber(17));

            Console.ReadLine();
        }
    }
}
