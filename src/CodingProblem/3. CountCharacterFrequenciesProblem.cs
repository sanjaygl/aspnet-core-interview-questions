using System;
using System.Collections.Generic;

namespace CodingProblem
{
	/// <summary>
	/// Problem Statement:
	/// Given a string, return the frequency of each character in the string.
	/// The result can be returned in any reasonable structure (e.g., Dictionary<char, int>).
	///
	/// Example:
	/// Input:  "hello"
	/// Output: { h:1, e:1, l:2, o:1 }
	/// </summary>
	internal class CountCharacterFrequenciesProblem
	{
		public static Dictionary<char, int> CountCharacterFrequencies(string input)
		{
			var frequencyDict = new Dictionary<char, int>();

			if (string.IsNullOrEmpty(input))
				return frequencyDict;

			foreach (char c in input)
			{
				if (frequencyDict.TryGetValue(c, out int count))
				{
					frequencyDict[c] = count + 1;
				}
				else
				{
					frequencyDict[c] = 1;
				}
			}

			return frequencyDict;
		}
	}
}