namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given an array of strings, group the anagrams together.
    /// Strings that contain the same characters with the same frequencies
    /// should be placed in the same group.
    ///
    /// Example:
    /// Input:  ["eat", "tea", "tan", "ate", "nat", "bat"]
    /// Output: [["eat", "tea", "ate"], ["tan", "nat"], ["bat"]]
    /// </summary>
    internal class GroupAnagramsProblem
    {
        // Example: Given an array of strings, group the anagrams together.
        public static void GroupAnagrams()
        {
            string[] input = { "eat", "tea", "tan", "ate", "nat", "bat" };

            var groupedAnagrams = Groups(input);

            foreach (var group in groupedAnagrams)
            {
                Console.WriteLine($"[{string.Join(", ", group)}]");
            }
        }

        // Write a function that groups an array of strings into anagrams.
        private static IList<IList<string>> Groups(string[] input)
        {
            var anagramMap = new Dictionary<string, List<string>>();
            // Loop through each string in the input array
            foreach (var group in input)
            {
                // sort characters of the string to create a key for anagrams
                char[] chars = group.ToCharArray();

                // Example: For "eat", chars will be ['e', 'a', 't']
                Array.Sort(chars);

                // Example: For "eat", sorted chars will be ['a', 'e', 't'], key = "aet"
                string key = new string(chars);

                // If the key doesn't exist in the map, create a new list for this anagram group
                if (!anagramMap.ContainsKey(key))
                {
                    anagramMap[key] = new List<string>();
                }

                // Add the original string to the corresponding anagram group
                anagramMap[key].Add(group);
            }

            // Return the grouped anagrams as a list of lists
            return anagramMap.Values.ToList<IList<string>>();
        }
    }
}
