# TDD, Design Thinking, and Mocking - Interview Scenarios

This document contains scenario-based interview prompts focused on TDD, design thinking, scalability, and testing quality mindset.

---

## Scenario 1 - Real-Time Business Requirement (TDD + Design Thinking)

### Client Scenario
You are working on an e-commerce platform.
We want to implement a feature that identifies duplicate products in the catalog based on Product Name.
If duplicates exist, the system should return the duplicate product names along with how many times they appear.
This feature needs to be reliable, maintainable, and well tested.

### Client Questions

#### TDD and Approach
1. How will you start solving this problem using TDD?
2. What will be your very first test case and why?
3. What edge cases will you consider?
4. When will you stop writing tests?

#### Technical and Design
1. What data structure or approach would you use? Why?
2. How will you ensure the algorithm is efficient?
3. If the product list becomes extremely large, how would your solution scale?

#### Mocking and Testing Deep Dive
1. Would you use mocking in this scenario? Why or why not?
2. What types of tests would you write?
3. Positive tests
4. Negative tests
5. Boundary tests

#### Maintainability and Business Thinking
1. If tomorrow requirement changes to: System should also consider case-insensitive duplicates, how will TDD help you handle this?

---

## Scenario 2 - API-Based System (Mocking + Quality Mindset)

### Client Scenario
Your application fetches customer details from an external Customer Service API.
Your team wants to write unit tests for the functionality, but calling real API every time is slow and unreliable.

### Client Questions
1. Will you call real API in unit tests? Why or why not?
2. Where will you apply mocking here?
3. What will you mock and what will you not mock?
4. What failures should you test?
5. API down
6. Timeout
7. Invalid response
8. Success response
9. How will you ensure your tests remain fast and stable?
10. How will TDD help in this scenario?

---

## Scenario 3 - Changing Requirement (Adaptability + TDD Maturity)

### Client Scenario
Initially, the requirement was to just return whether duplicates exist or not.
Midway, client changed requirement: now they want duplicate values plus their frequency count.

### Client Questions
1. What will you do first when requirements change?
2. Will you rewrite all tests or modify them?
3. How will TDD protect existing functionality?
4. How will you ensure no regression is introduced?
5. How do you communicate this change to team and client?

---

## Interview Question Bank

### Section 1 - TDD and Fail-First Thinking
1. What is Test-Driven Development? Explain Red-Green-Refactor.
2. Why do many companies prefer TDD?
3. What does Fail First mean in practice?
4. When should you stop writing tests?
5. How do you decide what the first test should be?
6. Can TDD slow development? When and why?
7. How do you handle changing requirements in TDD?
8. What is the difference between TDD and Test-After approach?
9. What does good test coverage really mean?
10. What makes a bad test?

### Section 2 - Writing Good Unit Tests
1. What makes a good unit test?
2. Explain Arrange-Act-Assert pattern.
3. What are positive test cases?
4. What are negative test cases?
5. What are edge cases?
6. What do fast tests mean and why are they important?
7. How do you make tests readable?
8. What is flakiness in tests?
9. How do you test performance boundaries?
10. How do you name your test cases?
11. How do you prioritize test scenarios in limited time?
12. What are anti-patterns in unit testing?

### Section 3 - Assertions
1. What is an assertion in unit testing?
2. Why do we need assertions?
3. What happens if an assertion fails?
4. What are examples of common assertions you have used?
5. Should one test contain multiple assertions?
6. How do you decide the right assertion?
7. How do assertions improve code quality?
8. What is the difference between Assert.AreEqual and Assert.True (or similar in your framework)?
9. What is a clear assertion failure message?
10. Explain how assertions help in debugging.

### Section 4 - Mocking and Test Doubles
1. What is mocking?
2. Why do we mock?
3. When should mocking not be used?
4. What is a mock?
5. What is a stub?
6. What is a fake?
7. What is a spy?
8. Why do we not mock pure logic methods?
9. When would you mock a repository or DB call?
10. When interviewers ask why did not you mock, what are they testing?
11. Does mocking slow tests?
12. Can mocking hide bad design?
13. Explain a real scenario where mocking saved you.

### Section 5 - Algorithm and Efficiency (Duplicate Characters Example)
1. How would you find duplicate characters in a string?
2. What assumptions would you clarify before coding?
3. What edge cases do you consider?
4. What is the time complexity of your approach?
5. Can you solve it without extra memory?
6. How would you do it using collections and dictionaries?
7. Can you solve it using LINQ?
8. What if the input string is extremely large?
9. How will you test this function?
10. How would you refactor your code for readability?
---

## WPP Client Rounds

### WPP - Client L1 Round

#### Self Introduction Guidance
- Be confident when discussing your work.
- Clearly explain the tasks you performed related to the required skills.
- Elaborate on your strongest areas of expertise.

#### Technical Test

##### Scenario 1 - Utility to Count Duplicate Characters in a String

As a user, I want a utility that can analyze a string and identify duplicate characters so that I can determine the frequency of each duplicate character within the string.

###### Acceptance Criteria

**Input**
- The utility should accept a single string input.
- The input string may contain up to 50 characters, including spaces.

**Processing**
- Ignore spaces in the input string.
- Treat uppercase and lowercase characters as different characters (for example, `A` and `a`).
- Include all characters (letters, numbers, punctuation, special characters) in the analysis.

**Output**
- Return duplicate characters and total occurrences for each duplicate.
- Format output as key-value pairs where key is the character and value is the count.

**Example**
- Input: `"the quick brown fox jumps over the lazy dog"`
- Output:
  - `'t': 2`
  - `'h': 2`
  - `'e': 3`
  - `'u': 2`
  - `'r': 2`
  - `'o': 4`

###### Tips
- Do not worry about host setup. A class and method implementation is enough.
- Treat it like production code.
- Think about test coverage and edge cases.
- Ask for help if you get stuck (team, documentation, search, AI tools).

###### Follow-Up Questions from Interviewer
1. When you get a functionality, what is your thought process while creating test cases?
2. How many and what test cases will you write for the above scenario?

### WPP - Client L2 Round

Create an API that accepts two parameters: `ProductId` (`Guid`) and product quantity.
- If item is not available, raise a domain event.
- Otherwise implement business logic for order placement.
- After placing the order, raise a domain event.

