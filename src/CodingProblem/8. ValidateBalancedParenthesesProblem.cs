namespace CodingProblem
{
    /// <summary>
    /// Problem Statement:
    /// Given a string containing only '(', ')', '{', '}', '[' and ']',
    /// determine if the input string is valid.
    /// 
    /// A string is valid if:
    /// - Open brackets are closed by the same type.
    /// - Open brackets are closed in the correct order.
    ///
    /// Example:
    /// Input:  "({[]})"
    /// Output: true
    /// </summary>
    internal class ValidateBalancedParenthesesProblem
    {
        public static bool IsValid(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            // Use a stack to keep track of opening brackets
            var stack = new Stack<char>();

            var pairs = new Dictionary<char, char>
            {
                { ')', '(' },
                { '}', '{' },
                { ']', '[' }
            };

            foreach (char c in input)
            {
                // If opening bracket, push to stack
                if (c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                }
                // If closing bracket, validate
                else if (pairs.ContainsKey(c))
                {
                    if (stack.Count == 0 || stack.Pop() != pairs[c])
                        return false;
                }
                else
                {
                    return false; // invalid character
                }
            }

            // Valid only if no unmatched opening brackets remain
            return stack.Count == 0;
        }
    }
}