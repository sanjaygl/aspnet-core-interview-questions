# SQL Interview Questions — 10 High-Value Problems

## 1. How do you use `ROW_NUMBER()` to find and remove duplicate records?

### Question

Suppose an `employees` table can contain duplicate employee records based on `employee_name` and `email`.

How would you identify duplicate records while keeping the first record?

### Answer

Use `ROW_NUMBER()` with `PARTITION BY`:

```sql
WITH EmployeeCTE AS (
    SELECT
        employee_id,
        employee_name,
        email,
        ROW_NUMBER() OVER (
            PARTITION BY employee_name, email
            ORDER BY employee_id
        ) AS row_num
    FROM public.employees
)
SELECT *
FROM EmployeeCTE
WHERE row_num > 1;
```

`PARTITION BY` creates a separate group for each combination of `employee_name` and `email`. `ROW_NUMBER()` assigns a unique number within each group, so rows with `row_num > 1` are duplicates.

### Interview Point

> `ROW_NUMBER()` is commonly used to identify duplicate rows because it assigns a unique number to each row within a partition.

---

## 2. How do you delete duplicate records while keeping one record?

### Question

Suppose duplicate employee records exist based on `employee_name` and `email`.

Delete the duplicate records while keeping the record with the smallest `employee_id`.

### Answer

```sql
WITH EmployeeCTE AS (
    SELECT
        employee_id,
        ROW_NUMBER() OVER (
            PARTITION BY employee_name, email
            ORDER BY employee_id
        ) AS row_num
    FROM public.employees
)
DELETE FROM public.employees
WHERE employee_id IN (
    SELECT employee_id
    FROM EmployeeCTE
    WHERE row_num > 1
);
```

The first record in each duplicate group receives `row_num = 1` and is kept. All remaining records receive `row_num > 1` and are deleted.

### Interview Point

> Always identify the rows to delete first and verify the result with a `SELECT` before executing the `DELETE` in a production database.

---

## 3. How do you find employees earning more than their manager?

### Scenario

Assume the `employees` table contains a `manager_id` column:

```text
employee_id
employee_name
salary
manager_id
```

`manager_id` refers to another employee's `employee_id`.

### Question

Find employees whose salary is greater than their manager's salary.

### Answer

Use a self join:

```sql
SELECT
    e.employee_name AS employee_name,
    e.salary AS employee_salary,
    m.employee_name AS manager_name,
    m.salary AS manager_salary
FROM public.employees AS e
JOIN public.employees AS m
    ON e.manager_id = m.employee_id
WHERE e.salary > m.salary;
```

The `employees` table is joined to itself. One alias represents the employee (`e`) and the other represents the manager (`m`).

### Interview Point

> A self join is used when rows in the same table have a relationship with other rows in that same table.

---

## 4. What is a Self Join and when would you use it?

### Question

Explain a self join and give a practical example.

### Answer

A self join is a join where a table is joined with itself.

For example, an employee-manager relationship:

```sql
SELECT
    e.employee_name AS employee,
    m.employee_name AS manager
FROM public.employees AS e
LEFT JOIN public.employees AS m
    ON e.manager_id = m.employee_id;
```

Here:

- `e` represents the employee.
- `m` represents the manager.
- Both come from the same `employees` table.

### Common Use Cases

- Employee → Manager relationships
- Parent → Child relationships
- Organizational hierarchies
- Comparing rows within the same table

### Interview Point

> A self join allows rows in the same table to be compared or related to each other.

---

## 5. What are `LAG()` and `LEAD()`?

### Question

How would you compare an employee's salary with the salary of the previous or next employee when employees are ordered by `employee_id`?

### Answer

Use `LAG()` and `LEAD()`:

```sql
SELECT
    employee_id,
    employee_name,
    salary,
    LAG(salary) OVER (ORDER BY employee_id) AS previous_salary,
    LEAD(salary) OVER (ORDER BY employee_id) AS next_salary
FROM public.employees;
```

### `LAG()`

Returns a value from a previous row.

```text
Current row
    ↓
LAG() → previous row
```

### `LEAD()`

Returns a value from a following row.

```text
Current row
    ↓
LEAD() → next row
```

### Interview Point

> `LAG()` accesses a previous row without requiring a self join, while `LEAD()` accesses a following row.

---

## 6. How do you calculate a running total?

### Question

Suppose we want to calculate the cumulative salary amount as employees are ordered by `employee_id`.

### Answer

Use `SUM()` as a window function:

```sql
SELECT
    employee_id,
    employee_name,
    salary,
    SUM(salary) OVER (
        ORDER BY employee_id
        ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW
    ) AS running_total
FROM public.employees;
```

Conceptually:

```text
Salary   Running Total
-------  -------------
120000   120000
90000    210000
80000    290000
70000    360000
...
```

Each row contains the total of all salaries from the first row through the current row.

### Interview Point

> A running total can be calculated using an aggregate function such as `SUM()` as a window function, allowing the original rows to remain in the result.

---

## 7. How do you find the top 3 salaries in each department?

### Question

Find the top 3 distinct salaries from every department.

If two employees have the same salary, they should receive the same rank.

### Answer

Use `DENSE_RANK()` with `PARTITION BY`:

```sql
WITH EmployeeCTE AS (
    SELECT
        employee_id,
        employee_name,
        salary,
        department_id,
        DENSE_RANK() OVER (
            PARTITION BY department_id
            ORDER BY salary DESC
        ) AS salary_rank
    FROM public.employees
    WHERE salary IS NOT NULL
)
SELECT
    employee_id,
    employee_name,
    salary,
    department_id
FROM EmployeeCTE
WHERE salary_rank <= 3
ORDER BY department_id, salary DESC;
```

### Why `PARTITION BY`?

Without `PARTITION BY`, all employees are ranked together.

With:

```sql
PARTITION BY department_id
```

a separate ranking is created for every department.

### Why `DENSE_RANK()`?

Duplicate salaries receive the same rank, and the next distinct salary receives the next consecutive rank.

### Interview Point

> `PARTITION BY` allows a window function to restart its calculation for each group, making it useful for problems such as top N records per department.

---

## 8. What is a Correlated Subquery?

### Question

Find employees whose salary is greater than the average salary of their own department using a correlated subquery.

### Answer

```sql
SELECT
    e.employee_id,
    e.employee_name,
    e.salary,
    e.department_id
FROM public.employees AS e
WHERE e.salary > (
    SELECT AVG(e2.salary)
    FROM public.employees AS e2
    WHERE e2.department_id = e.department_id
);
```

The inner query depends on the current row of the outer query:

```sql
WHERE e2.department_id = e.department_id
```

For each employee, the subquery calculates the average salary of that employee's department.

### Correlated vs Non-Correlated Subquery

A non-correlated subquery can execute independently of the outer query.

A correlated subquery references a column from the outer query and therefore depends on the current outer row.

### Interview Point

> A correlated subquery is a subquery that references a value from the outer query and is logically evaluated in relation to each outer row.

---

## 9. What is the difference between a CTE and a Subquery?

### Question

Explain the difference between a Common Table Expression (`CTE`) and a subquery. When would you use each?

### Answer

A subquery is written directly inside another query:

```sql
SELECT *
FROM public.employees
WHERE salary > (
    SELECT AVG(salary)
    FROM public.employees
);
```

A CTE is defined using `WITH` and can make a complex query easier to read:

```sql
WITH EmployeeAverage AS (
    SELECT AVG(salary) AS average_salary
    FROM public.employees
)
SELECT e.*
FROM public.employees AS e
CROSS JOIN EmployeeAverage AS a
WHERE e.salary > a.average_salary;
```

### Key Difference

```text
Subquery
→ Embedded directly inside the query
→ Useful for smaller/simple operations

CTE
→ Defined separately using WITH
→ Makes complex queries easier to read and organize
→ Can be referenced by the main query and, depending on the query, multiple times
```

### Important Interview Nuance

> A CTE is primarily a query-organization/readability feature; it should not automatically be assumed to be faster than a subquery. The optimizer and database engine determine the actual execution behavior.

---

## 10. How do you use `EXPLAIN ANALYZE` to troubleshoot a slow query?

### Question

Suppose a PostgreSQL query is taking several seconds to execute.

How would you investigate the query and determine where the performance problem is?

### Answer

Use:

```sql
EXPLAIN ANALYZE
SELECT
    *
FROM public.employees
WHERE salary > 100000;
```

`EXPLAIN` shows the query execution plan.

`EXPLAIN ANALYZE` actually executes the query and reports runtime information about the plan.

You can also use:

```sql
EXPLAIN (ANALYZE, BUFFERS)
SELECT
    *
FROM public.employees
WHERE salary > 100000;
```

### Things to investigate

Look for:

- Sequential scans on large tables
- Index scans or index usage
- Actual rows vs estimated rows
- Expensive joins
- Sort operations
- Large amounts of data being scanned
- High execution time
- Buffer reads

### Example Index

If the query frequently filters by salary and the table is large, an index may help:

```sql
CREATE INDEX idx_employees_salary
ON public.employees (salary);
```

Then run `EXPLAIN ANALYZE` again and compare the execution plan and timing.

### Interview Point

> `EXPLAIN` shows how PostgreSQL plans to execute a query, while `EXPLAIN ANALYZE` executes the query and provides actual runtime and row information. It is an important tool for diagnosing slow queries.
