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
        public static IList<IList<string>> GroupAnagrams(string[] input)
        {
            var anagramMap = new Dictionary<string, List<string>>();

            // Loop through each string in the input array
            foreach (var group in input)
            {
                // Convert the string to a character array and sort it to create a key for the anagram group
                char[] chars = group.ToCharArray();

                // Sort the characters to ensure that anagrams will have the same sorted representation, Example: "eat", "tea", and "ate" will all become "aet" after sorting
                Array.Sort(chars);

                // Convert the sorted character array back to a string to use as a key in the dictionary, Example: For "eat", key will be "aet"
                string key = new string(chars);

                // If the key does not exist in the dictionary, create a new list for that key
                if (!anagramMap.ContainsKey(key))
                {
                    anagramMap[key] = new List<string>();
                }

                // Add the original string to the list corresponding to the sorted key, Example: "eat" will be added to the list for key "aet"
                anagramMap[key].Add(group);
            }

            // Return the values of the dictionary as a list of lists, where each inner list contains anagrams, Example: [["eat", "tea", "ate"], ["tan", "nat"], ["bat"]]
            return anagramMap.Values.ToList<IList<string>>();
        }
    }
}
