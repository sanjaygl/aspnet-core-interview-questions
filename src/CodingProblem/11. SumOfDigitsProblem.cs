using System;

namespace CodingProblem
{
	/// <summary>
	/// Problem Statement:
	/// Write a function that takes a non-negative integer and returns the sum of its digits.
	///
	/// Example:
	/// Input:  1234
	/// Output: 10
	/// </summary>
	internal class SumOfDigitsProblem
	{
		public static int SumOfDigits(int number)
		{
			if (number < 0)
				throw new ArgumentException("Input must be a non-negative integer.");

			int sum = 0;

			while (number > 0)
			{
				sum += number % 10;
				number /= 10;
			}

			return sum;
		}
	}
}