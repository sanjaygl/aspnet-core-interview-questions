# SQL Interview Questions Practice

## 1. What is the output of `SELECT 5` without a `FROM` and `WHERE` clause?

**Answer:**

```sql
SELECT 5;
```

**Output:**

| (No column name) |
|---:|
| 5 |

SQL Server allows `SELECT` statements without a `FROM` clause when selecting a constant or expression.

---

## 2. What is the output of `SELECT 'R'` without a `FROM` and `WHERE` clause?

**Answer:**

```sql
SELECT 'R';
```

**Output:**

| (No column name) |
|---|
| R |

SQL Server can return a literal value without requiring a table.

---

## 3. What is the output of `SELECT 1/0` without a `FROM` and `WHERE` clause?

**Answer:**

```sql
SELECT 1/0;
```

SQL Server returns a **divide-by-zero error**:

```text
Msg 8134, Level 16
Divide by zero error encountered.
```

---

## 4. What is the difference between `UNION` and `UNION ALL`?

Suppose the `Orders` table contains:

```text
5
5
3
4
2
2
1
```

### `UNION ALL`

```sql
SELECT * FROM Orders
UNION ALL
SELECT * FROM Orders;
```

`UNION ALL` keeps duplicates. Each query returns 7 rows, so the total is **14 rows**.

### `UNION`

```sql
SELECT * FROM Orders
UNION
SELECT * FROM Orders;
```

`UNION` removes duplicate rows. The distinct values are:

```text
1
2
3
4
5
```

So the total is **5 rows**.

| Operator | Duplicate Rows | Output Count |
|---|---|---:|
| `UNION ALL` | Keeps duplicates | 14 |
| `UNION` | Removes duplicates | 5 |

---

## 5. How do you check an expensive query in a Stored Procedure?

**Answer:**

You can use:

- **Actual Execution Plan** to identify expensive operators such as scans, sorts, joins, and key lookups.
- `SET STATISTICS IO ON` to check logical and physical reads.
- `SET STATISTICS TIME ON` to check CPU time and elapsed time.
- **Query Store** to identify high-cost or frequently executed queries.
- SQL Server DMVs to investigate query execution statistics.

Example:

```sql
SET STATISTICS IO ON;
SET STATISTICS TIME ON;

EXEC dbo.YourStoredProcedure @Id = 10;

SET STATISTICS IO OFF;
SET STATISTICS TIME OFF;
```

---

## 6. What steps would you take to optimize a Stored Procedure to return results within milliseconds?

**Answer:**

1. Identify the slow query inside the stored procedure.
2. Check the **Actual Execution Plan**.
3. Check `STATISTICS IO` and `STATISTICS TIME`.
4. Identify expensive scans, joins, sorts, key lookups, and implicit conversions.
5. Verify that appropriate indexes exist.
6. Optimize `JOIN`, `WHERE`, `ORDER BY`, and `GROUP BY` conditions.
7. Return only required columns instead of using `SELECT *`.
8. Reduce unnecessary data processing and result sets.
9. Check for parameter sniffing when execution plans behave differently for different parameters.
10. Test the optimized procedure with realistic data and compare execution time, CPU usage, and logical reads.

**Important:** The performance target should depend on the query, data size, workload, and application requirements.

---

## 7. How can we pass a list of parameters to a Stored Procedure?

**Answer:**

A common approach in SQL Server is to use a **Table-Valued Parameter (TVP)**.

### Step 1: Create a table type

```sql
CREATE TYPE dbo.IdList AS TABLE
(
    Id INT
);
```

### Step 2: Use it in the Stored Procedure

```sql
CREATE PROCEDURE dbo.GetOrders
    @Ids dbo.IdList READONLY
AS
BEGIN
    SELECT *
    FROM Orders
    WHERE OrderId IN (SELECT Id FROM @Ids);
END;
```

### Step 3: Pass multiple values

```sql
DECLARE @Ids dbo.IdList;

INSERT INTO @Ids (Id)
VALUES (1), (2), (3), (4);

EXEC dbo.GetOrders @Ids = @Ids;
```

A **Table-Valued Parameter** allows multiple values to be passed to a stored procedure as a structured parameter.

Other approaches include JSON, XML, or comma-separated strings, but TVPs are generally cleaner for structured lists in SQL Server.

---

## 8. What happens with `WHERE salary = NULL`?

**Question:**

What will the following query return?

```sql
SELECT *
FROM public.employees
WHERE salary = NULL;
```

**Answer:**

It returns **no rows**, even if an employee has `salary = NULL`.

In SQL, `NULL` represents an unknown or missing value. Comparisons involving `NULL` using `=` or `<>` do not evaluate to `TRUE`.

```text
NULL = NULL  → UNKNOWN
NULL = 100   → UNKNOWN
NULL <> 100  → UNKNOWN
```

The `WHERE` clause returns only rows where the condition evaluates to `TRUE`.

**Correct query:**

To find rows where `salary` is `NULL`, use:

```sql
SELECT *
FROM public.employees
WHERE salary IS NULL;
```

To find rows where `salary` is not `NULL`, use:

```sql
SELECT *
FROM public.employees
WHERE salary IS NOT NULL;
```

**Interview Point:**

> We cannot compare `NULL` using `=` or `<>`. We must use `IS NULL` or `IS NOT NULL` because `NULL` represents an unknown value, and comparisons involving `NULL` evaluate to `UNKNOWN`.

---

## 10. What will `COUNT(*)`, `COUNT(1)`, `COUNT(salary)`, and `COUNT(DISTINCT salary)` return?

Given that the `employees` table contains **19 employees**, **1 employee has `salary = NULL`**, and there are **9 distinct non-NULL salary values**:

```sql
SELECT
    COUNT(*) AS total_rows,
    COUNT(1) AS count_one,
    COUNT(salary) AS count_salary,
    COUNT(DISTINCT salary) AS count_distinct_salary
FROM public.employees;
```

**Answer:**

| Expression | Result | Reason |
|---|---:|---|
| `COUNT(*)` | 19 | Counts every row |
| `COUNT(1)` | 19 | Counts every row because `1` is non-NULL |
| `COUNT(salary)` | 18 | Ignores the one `NULL` salary |
| `COUNT(DISTINCT salary)` | 9 | Ignores `NULL` and removes duplicate salary values |

### Interview Point

> `COUNT(*)` and `COUNT(1)` count rows, `COUNT(column)` counts non-NULL values, and `COUNT(DISTINCT column)` counts unique non-NULL values.

---

## 17. What happens when a `WHERE` condition is applied to the right table of a `LEFT JOIN`?

### Scenario

We have two tables:

**`departments`**

| department_id | department_name |
|---:|---|
| 1 | IT |
| 2 | HR |
| 3 | Finance |
| 4 | Sales |
| 5 | Marketing |
| 6 | Operations |
| 7 | Legal |

**`employees`**

The `employees` table contains employees for IT, HR, Finance, Sales, and Marketing, but **Operations and Legal do not have any employees**.

We want to display:

- Every department
- The names of its employees
- Only employees whose `status = 'Active'`
- Departments with no employees should still appear

Consider the following query:

```sql
SELECT
    d.department_name,
    e.employee_name
FROM public.departments AS d
LEFT JOIN public.employees AS e
    ON e.department_id = d.department_id
WHERE e.status = 'Active';
```

## 20. What is the difference between filtering a `LEFT JOIN` condition in `WHERE` versus `ON`?

### Scenario

We have two tables:

**`departments`**

| department_id | department_name |
|---:|---|
| 1 | IT |
| 2 | HR |
| 3 | Finance |
| 4 | Sales |
| 5 | Marketing |
| 6 | Operations |
| 7 | Legal |

**`employees`**

Employees exist in departments 1–5, while **Operations** and **Legal** have no employees.

We want to:

- Display **every department**
- Display only employees whose salary is **greater than 80,000**
- Keep departments even when they have **no employee earning more than 80,000**

### Query A

```sql
SELECT
    d.department_name,
    e.employee_name,
    e.salary
FROM public.departments AS d
LEFT JOIN public.employees AS e
    ON e.department_id = d.department_id
WHERE e.salary > 80000;
```

### Query B

```sql
SELECT
    d.department_name,
    e.employee_name,
    e.salary
FROM public.departments AS d
LEFT JOIN public.employees AS e
    ON e.department_id = d.department_id
   AND e.salary > 80000;
```

### Question

Which query correctly satisfies the requirement?

**Answer: Query B**

Query A removes departments that do not have an employee with a salary greater than 80,000 because the `WHERE` condition is applied after the `LEFT JOIN`.

For an unmatched department:

```text
Operations | NULL | NULL
Legal      | NULL | NULL
```

Then:

```text
NULL > 80000 → UNKNOWN
```

The `WHERE` clause removes those rows.

Query B puts the salary condition in the `ON` clause. The condition filters which employees are matched, while the `LEFT JOIN` still preserves every department.

Therefore, departments without an employee earning more than 80,000 remain in the result with `NULL` employee values.

### Interview Point

> **When using a `LEFT JOIN`, put a condition in the `ON` clause when you want to filter the matching rows while still preserving unmatched rows from the left table. Putting the condition in `WHERE` can remove those unmatched rows.**

## 21. What happens when `NULL` is present in a `NOT IN` list?

### Scenario

We have an `employees` table with the following relevant data:

```text
employee_id | employee_name | department_id
------------+---------------+--------------
1           | Raj Sharma    | 1
2           | Priya Mehta   | 2
3           | Amit Verma    | 3
...
19          | Vijay More    | NULL
```

Suppose we want to find employees whose department is **not IT or HR**:

```sql
SELECT
    employee_name,
    department_id
FROM public.employees
WHERE department_id NOT IN (1, 2);
```

An employee with `department_id = NULL` is **not returned** because comparisons involving `NULL` evaluate to `UNKNOWN`.

### Interview Cross-Question

Now consider:

```sql
SELECT *
FROM public.employees
WHERE department_id NOT IN (1, 2, NULL);
```

Suppose an employee has:

```text
department_id = 3
```

Will that employee be returned?

**Answer: No.**

The condition is conceptually evaluated as:

```text
3 <> 1     → TRUE
3 <> 2     → TRUE
3 <> NULL  → UNKNOWN
```

Because the comparison with `NULL` produces `UNKNOWN`, the overall `NOT IN` condition does not evaluate to `TRUE`.

Therefore, **no rows are returned** when the `NOT IN` list contains `NULL`.

### Why is this dangerous?

This can cause unexpected results when `NOT IN` uses a subquery:

```sql
SELECT *
FROM public.employees
WHERE department_id NOT IN
(
    SELECT department_id
    FROM public.departments
);
```

If the subquery returns even one `NULL`, the `NOT IN` condition can produce unexpected results.

### Safer Alternative

When `NULL` values are possible, `NOT EXISTS` is generally safer:

```sql
SELECT e.*
FROM public.employees AS e
WHERE NOT EXISTS
(
    SELECT 1
    FROM public.departments AS d
    WHERE d.department_id = e.department_id
);
```

### Interview Point

> **If a `NOT IN` list or subquery contains `NULL`, comparisons can evaluate to `UNKNOWN`, causing `NOT IN` to return no expected matches. `NOT EXISTS` is generally safer when `NULL` values are possible.**

## 22. How do you find the second-highest distinct salary?

### Scenario

We have an `employees` table with an employee salary column:

```text
salary
------
120000
120000
110000
100000
90000
90000
85000
```

There are duplicate salary values.

We need to find the **second-highest distinct salary**.

### Question

Write a SQL query to return the second-highest salary.

The expected result is:

```text
second_highest_salary
---------------------
110000
```

### Solution Using `DENSE_RANK()`

```sql
WITH employeecte AS (
    SELECT
        salary,
        DENSE_RANK() OVER (ORDER BY salary DESC) AS salary_order
    FROM public.employees
)
SELECT *
FROM employeecte
WHERE salary_order = 2;
```

### Why use `DENSE_RANK()`?

`DENSE_RANK()` gives the same rank to duplicate salary values and does not skip the next rank.

For example:

```text
salary | salary_order
-------+-------------
120000 | 1
120000 | 1
110000 | 2
100000 | 3
90000  | 4
90000  | 4
85000  | 5
```

Therefore, filtering with:

```sql
WHERE salary_order = 2
```

returns:

```text
110000
```

### Interview Point

> **`DENSE_RANK()` is useful for nth-highest salary questions because duplicate salaries receive the same rank and the next distinct salary gets the next consecutive rank.**

### Important Cross-Question

What is the difference between `ROW_NUMBER()`, `RANK()`, and `DENSE_RANK()`?

```text
ROW_NUMBER()
→ Gives every row a unique number.

RANK()
→ Gives duplicate values the same rank but skips ranks after duplicates.

DENSE_RANK()
→ Gives duplicate values the same rank without skipping ranks.
```

### NULL Consideration

If `salary` can contain `NULL`, explicitly exclude it when finding the highest or nth-highest salary:

```sql
WITH employeecte AS (
    SELECT
        salary,
        DENSE_RANK() OVER (ORDER BY salary DESC) AS salary_order
    FROM public.employees
    WHERE salary IS NOT NULL
)
SELECT *
FROM employeecte
WHERE salary_order = 2;
```

This ensures that only employees with a salary value participate in the ranking.

## 23. How do you find employees earning more than their department average?

### Scenario

We have an `employees` table with the following relevant columns:

| employee_id | employee_name | salary | department_id |
|---:|---|---:|---:|
| 1 | Raj Sharma | 120000 | 1 |
| 5 | Sanjay Kumar | 90000 | 1 |
| 6 | Rahul Patil | 80000 | 1 |
| 7 | Sneha Joshi | 80000 | 1 |
| 8 | Vikram Rao | 70000 | 1 |
| 9 | Anjali | 70000 | 1 |
| 2 | Priya Mehta | 110000 | 2 |
| 10 | Pooja Shah | 75000 | 2 |
| 11 | Ravi Gupta | 65000 | 2 |
| 3 | Amit Verma | 120000 | 3 |
| 12 | Kiran Joshi | 85000 | 3 |
| 13 | Meena Rao | 75000 | 3 |
| 14 | Arjun Das | 65000 | 3 |
| 4 | Neha Singh | 100000 | 4 |
| 15 | Rohit Jain | 90000 | 4 |
| 16 | Divya Kapoor | 85000 | 4 |
| 17 | Manish Yadav | 75000 | 4 |

### Question

Find all employees whose salary is **greater than the average salary of their own department**.

Expected columns:

```text
employee_name
salary
department_id
department_average
```

For example, if the IT department's average salary is `85,000`, employees earning more than `85,000` should be returned, while employees earning `85,000` or less should not be returned.

### Solution Using a Window Function

```sql
WITH empCTE AS (
    SELECT
        employee_name,
        salary,
        department_id,
        AVG(salary) OVER (PARTITION BY department_id) AS department_average
    FROM public.employees
)
SELECT *
FROM empCTE
WHERE salary > department_average;
```

### Why does this work?

The important part is:

```sql
AVG(salary) OVER (PARTITION BY department_id)
```

It calculates the average salary separately for each department while keeping every employee row in the result.

For example, the IT department can conceptually look like:

```text
employee | salary | department_average
---------+--------+-------------------
Raj      | 120000 | 85000
Sanjay   | 90000  | 85000
Rahul    | 80000  | 85000
Sneha    | 80000  | 85000
Vikram   | 70000  | 85000
Anjali   | 70000  | 85000
```

The outer query then filters the rows:

```sql
WHERE salary > department_average
```

Therefore, only employees earning more than their department's average salary are returned.

### Interview Point

> **A window function can calculate information across related rows while preserving the individual rows. Unlike `GROUP BY`, it does not collapse the rows into one row per group.**

### `GROUP BY` vs Window Function

```text
GROUP BY
→ Combines rows into groups
→ Usually returns one row per group

Window Function
→ Keeps individual rows
→ Calculates information across related rows
```


## 24. How do you find employees who have the same salary?

### Scenario

We have an `employees` table containing employee details and salary information.

Some employees may have the same salary.

For example:

```text
employee_name | salary
--------------+--------
Raj           | 120000
Amit          | 120000
Priya         | 110000
Sanjay        | 90000
Rohit         | 90000
```

We need to find the **actual employees** whose salary is shared with at least one other employee.

The result should include:

```text
employee_id
employee_name
salary
```

For example, the result should include:

```text
Raj
Amit
Sanjay
Rohit
```

but should not include `Priya` because `110000` occurs only once.

### Question

Write a SQL query to return every employee whose salary occurs **more than once** in the `employees` table.

### Solution Using a Subquery

```sql
SELECT
    employee_id,
    employee_name,
    salary
FROM public.employees
WHERE salary IN (
    SELECT salary
    FROM public.employees
    GROUP BY salary
    HAVING COUNT(employee_name) > 1
)
ORDER BY salary ASC;
```

### How It Works

The inner query first finds the salary values that occur more than once:

```sql
SELECT salary
FROM public.employees
GROUP BY salary
HAVING COUNT(employee_name) > 1;
```

This returns the duplicate salary values.

The outer query then finds the actual employees whose salary is present in that duplicate-salary list:

```sql
WHERE salary IN (...)
```

### Alternative Solution Using a Window Function

The same problem can also be solved using `COUNT()` as a window function:

```sql
SELECT
    employee_id,
    employee_name,
    salary
FROM (
    SELECT
        employee_id,
        employee_name,
        salary,
        COUNT(*) OVER (PARTITION BY salary) AS salary_count
    FROM public.employees
) AS e
WHERE salary_count > 1;
```

Here:

```sql
COUNT(*) OVER (PARTITION BY salary)
```

counts how many employees have each salary while keeping every employee row.

### Interview Point

> **A `GROUP BY` query can identify duplicate salary values, while a subquery or window function can be used to return the actual employee records having those duplicate values.**

## 25. How do you use `CASE WHEN` to categorize data and handle `NULL` values?

### Scenario

We have an `employees` table with these relevant columns:

```text
employee_id
employee_name
salary
department_id
status
```

We want to categorize each employee based on salary.

### Question

Write a query that returns:

```text
employee_name
salary
salary_category
```

The `salary_category` should follow these rules:

| Condition | `salary_category` |
|---|---|
| `salary >= 100000` | `High` |
| `salary >= 80000` | `Medium` |
| `salary < 80000` | `Low` |
| `salary IS NULL` | `Not Available` |

### Solution

```sql
SELECT
    employee_name,
    salary,
    CASE
        WHEN salary IS NULL THEN 'Not Available'
        WHEN salary >= 100000 THEN 'High'
        WHEN salary >= 80000 THEN 'Medium'
        ELSE 'Low'
    END AS salary_category
FROM public.employees;
```

### Why is the order important?

The `CASE` expression is evaluated from top to bottom.

The `NULL` condition is checked explicitly first:

```sql
WHEN salary IS NULL THEN 'Not Available'
```

Then the numeric conditions are evaluated:

```sql
WHEN salary >= 100000 THEN 'High'
WHEN salary >= 80000 THEN 'Medium'
```

Anything remaining falls into:

```sql
ELSE 'Low'
```

### Interview Point

> **`CASE` evaluates conditions from top to bottom and returns the result for the first condition that is true. When handling `NULL`, use `IS NULL` rather than comparisons such as `salary = NULL`.**
