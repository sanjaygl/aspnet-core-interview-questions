## Coding Problems - Implemented in CodingProblem Library (1 to 23)

This list is aligned with the actual implemented problem classes in `src/CodingProblem`.

---

### 1. Reverse a String
**Problem Statement**  
Given a non-empty string, return a new string that is the reverse of the input without using built-in reverse methods.

**Example**  
Input: `"interview"`  
Output: `"weivretni"`

---

### 2. Check for Palindrome String
**Problem Statement**  
Check whether a given string is a palindrome. Ignore case and non-alphanumeric characters.

**Example**  
Input: `"A man, a plan, a canal: Panama"`  
Output: `true`

---

### 3. Count Character Frequencies in a String
**Problem Statement**  
Given a string, return the frequency of each character.

**Example**  
Input: `"hello"`  
Output: `{ h: 1, e: 1, l: 2, o: 1 }`

---

### 4. Remove Duplicates from an Array
**Problem Statement**  
Given an integer array, return a new array containing only distinct values.

**Example**  
Input: `[1, 2, 2, 3, 1]`  
Output: `[1, 2, 3]`

---

### 5. Find Second Largest Distinct Element
**Problem Statement**  
Given an integer array, find the second largest distinct element. If it does not exist, return `null`.

**Example**  
Input: `[5, 1, 9, 3, 9]`  
Output: `5`

---

### 6. Merge Two Sorted Arrays
**Problem Statement**  
Merge two sorted integer arrays into one sorted array without sorting the final array again.

**Example**  
Input: `[1, 3, 5]`, `[2, 4, 6]`  
Output: `[1, 2, 3, 4, 5, 6]`

---

### 7. Group Anagrams
**Problem Statement**  
Given an array of strings, group words that are anagrams of each other.

**Example**  
Input: `["eat", "tea", "tan", "ate", "nat", "bat"]`  
Output: `[["eat", "tea", "ate"], ["tan", "nat"], ["bat"]]`

---

### 8. Validate Balanced Parentheses
**Problem Statement**  
Given a string containing only bracket characters `()[]{}`, determine whether it is valid.

**Example**  
Input: `"({[]})"`  
Output: `true`

---

### 9. Remove All Occurrences of a Value from an Array
**Problem Statement**  
Given an integer array and a target value, return a new array with all target values removed.

**Example**  
Input: `[1, 2, 3, 2, 4]`, target = `2`  
Output: `[1, 3, 4]`

---

### 10. Find First Non-Repeating Character
**Problem Statement**  
Given a string, return the first character that appears exactly once. If none exists, return `null`.

**Example**  
Input: `"swiss"`  
Output: `'w'`

---

### 11. Sum of Digits of an Integer
**Problem Statement**  
Given a non-negative integer, return the sum of its digits.

**Example**  
Input: `1234`  
Output: `10`

---

### 12. Find Intersection of Two Arrays
**Problem Statement**  
Given two integer arrays, return distinct elements present in both.

**Example**  
Input: `[1, 2, 2, 3]`, `[2, 3, 4]`  
Output: `[2, 3]`

---

### 13. Find Missing Number in Range 0..n
**Problem Statement**  
Given an array with `n` distinct numbers taken from `0` to `n`, find the missing number.

**Example**  
Input: `[3, 0, 1]`  
Output: `2`

---

### 14. Two Sum
**Problem Statement**  
Given an integer array and a target value, return indices (or values) of two numbers whose sum equals the target.

**Example**  
Input: `[2, 7, 11, 15]`, target = `9`  
Output: indices `[0, 1]` or values `[2, 7]`

---

### 15. Swap Two Numbers Without Temporary Variable
**Problem Statement**  
Swap two integer values without using a temporary variable.

**Example**  
Input: `a = 5, b = 10`  
Output: `a = 10, b = 5`

---

### 16. Factorial of a Number
**Problem Statement**  
Compute factorial of `n` (`n!`) where `n! = n * (n-1) * ... * 1` and `0! = 1`.

**Example**  
Input: `5`  
Output: `120`

---

### 17. Fibonacci Number and Series
**Problem Statement**  
Find the `n`th Fibonacci number and generate Fibonacci series up to index `n`.

**Example**  
Input: `n = 7`  
Output: number `13`, series `0, 1, 1, 2, 3, 5, 8, 13`

---

### 18. Find Duplicates in an Array
**Problem Statement**  
Given an integer array, find all values that appear more than once.

**Example**  
Input: `[1, 2, 3, 2, 4, 5, 5]`  
Output: `[2, 5]`

---

### 19. Find First Uppercase Character Recursively
**Problem Statement**  
Given a string, use recursion to find and return the first uppercase character.

**Example**  
Input: `"abcdeGTXYZqghABCD"`  
Output: `'G'`

---

### 20. Flatten Nested Collections
**Problem Statement**  
Given a nested list of integers (`List<List<int>>`), flatten it into a single list.

**Example**  
Input: `[[1, 2, 2], [3, 4], [5]]`  
Output: `[1, 2, 2, 3, 4, 5]`

---

### 21. FizzBuzz
**Problem Statement**  
Print numbers from `1` to `n`:
- Multiples of `3` -> `"Fizz"`
- Multiples of `5` -> `"Buzz"`
- Multiples of both -> `"FizzBuzz"`

**Example**  
Input: `n = 15`  
Output: `1, 2, Fizz, 4, Buzz, Fizz, 7, 8, Fizz, Buzz, 11, Fizz, 13, 14, FizzBuzz`

---

### 22. Find Majority Element
**Problem Statement**  
Given an integer array, find the element that appears more than `n/2` times. Return `null` if none exists.

**Example**  
Input: `[2, 2, 1, 2, 3, 2, 2]`  
Output: `2`

---

### 23. Check Prime Number
**Problem Statement**  
Given a positive integer `n`, determine whether it is a prime number.
A prime number is greater than `1` and has exactly two divisors: `1` and itself.

**Example**  
Input: `29`  
Output: `true`
