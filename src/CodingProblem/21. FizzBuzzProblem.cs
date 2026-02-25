using System;
using System.Collections.Generic;

namespace CodingProblem
{
	/// <summary>
	/// Problem Statement:
	/// Print numbers from 1 to n.
	/// - Multiples of 3 → "Fizz"
	/// - Multiples of 5 → "Buzz"
	/// - Multiples of both 3 and 5 → "FizzBuzz"
	///
	/// Example:
	/// Input:  n = 15
	/// Output: 1, 2, Fizz, 4, Buzz, Fizz, 7, 8, Fizz, Buzz, 11, Fizz, 13, 14, FizzBuzz
	/// </summary>
	internal class FizzBuzzProblem
	{
		public static IList<string> FizzBuzz(int n)
		{
			var result = new List<string>();

			if (n <= 0)
				return result;

			for (int i = 1; i <= n; i++)
			{
				if (i % 15 == 0)
					result.Add("FizzBuzz");
				else if (i % 3 == 0)
					result.Add("Fizz");
				else if (i % 5 == 0)
					result.Add("Buzz");
				else
					result.Add(i.ToString());
			}

			return result;
		}
	}
}