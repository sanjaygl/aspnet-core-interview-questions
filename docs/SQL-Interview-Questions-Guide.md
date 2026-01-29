# SQL Interview Questions - Complete Guide

## Table of Contents

### Core Concepts (1-25)

01. [What is SQL?](#1-what-is-sql)
02. [DDL, DML, DCL, and TCL Commands](#2-ddl-dml-dcl-and-tcl-commands)
03. [Primary Key vs Foreign Key](#3-primary-key-vs-foreign-key)
04. [DELETE vs TRUNCATE vs DROP](#4-delete-vs-truncate-vs-drop)
05. [WHERE vs HAVING](#5-where-vs-having)
06. [INNER JOIN vs LEFT JOIN vs RIGHT JOIN vs FULL JOIN](#6-inner-join-vs-left-join-vs-right-join-vs-full-join)
07. [UNION vs UNION ALL](#7-union-vs-union-all)
08. [What are Indexes and Why Use Them?](#8-what-are-indexes-and-why-use-them)
09. [Clustered vs Non-Clustered Index](#9-clustered-vs-non-clustered-index)
10. [Subquery vs CTE (Common Table Expression)](#10-subquery-vs-cte-common-table-expression)
11. [Normalization vs Denormalization](#11-normalization-vs-denormalization)
12. [Stored Procedures vs Functions](#12-stored-procedures-vs-functions)
13. [What are Transactions and ACID Properties?](#13-what-are-transactions-and-acid-properties)
14. [Locks and Deadlocks](#14-locks-and-deadlocks)
15. [How to Find Duplicate Records?](#15-how-to-find-duplicate-records)
16. [Pagination using OFFSET and LIMIT](#16-pagination-using-offset-and-limit)
17. [SQL Performance Optimization Techniques](#17-sql-performance-optimization-techniques)
18. [What is a Self Join?](#18-what-is-a-self-join)
19. [RANK() vs DENSE_RANK() vs ROW_NUMBER()](#19-rank-vs-dense_rank-vs-row_number)
20. [What are Triggers?](#20-what-are-triggers)
21. [What is a View and Materialized View?](#21-what-is-a-view-and-materialized-view)
22. [Correlated vs Non-Correlated Subquery](#22-correlated-vs-non-correlated-subquery)
23. [What is the N+1 Query Problem?](#23-what-is-the-n1-query-problem)
24. [How to Handle NULL Values in SQL?](#24-how-to-handle-null-values-in-sql)
25. [What are Execution Plans?](#25-what-are-execution-plans)

### Scenario-Based Questions (26-50)

26. [Scenario 1: Nested Stored Procedures with Temp Tables](#scenario-1-nested-stored-procedures-with-temp-tables)
27. [Scenario 2: Deadlock Between Two Concurrent Transactions](#scenario-2-deadlock-between-two-concurrent-transactions)
28. [Scenario 3: Query Performance Degradation Over Time](#scenario-3-query-performance-degradation-over-time)
29. [Scenario 4: Duplicate Records After Data Migration](#scenario-4-duplicate-records-after-data-migration)
30. [Scenario 5: Missing Data After Transaction Rollback](#scenario-5-missing-data-after-transaction-rollback)
31. [Scenario 6: Performance Issue with LIKE Query](#scenario-6-performance-issue-with-like-query)
32. [Scenario 7: Incorrect Results from JOIN Query](#scenario-7-incorrect-results-from-join-query)
33. [Scenario 8: Race Condition in Inventory Update](#scenario-8-race-condition-in-inventory-update)
34. [Scenario 9: Slow Query with Multiple OR Conditions](#scenario-9-slow-query-with-multiple-or-conditions)
35. [Scenario 10: Data Inconsistency Between Related Tables](#scenario-10-data-inconsistency-between-related-tables)
36. [Scenario 11: Memory Error with Large Result Set](#scenario-11-memory-error-with-large-result-set)
37. [Scenario 12: Trigger Causing Performance Issues](#scenario-12-trigger-causing-performance-issues)
38. [Scenario 13: Incorrect Aggregation Results with NULLs](#scenario-13-incorrect-aggregation-results-with-nulls)
39. [Scenario 14: Query Timeout on Production](#scenario-14-query-timeout-on-production)
40. [Scenario 15: Cascading Delete Causing Unexpected Deletions](#scenario-15-cascading-delete-causing-unexpected-deletions)
41. [Scenario 16: Stored Procedure Returns Different Results on Each Execution](#scenario-16-stored-procedure-returns-different-results-on-each-execution)
42. [Scenario 17: Foreign Key Constraint Preventing Legitimate Updates](#scenario-17-foreign-key-constraint-preventing-legitimate-updates)
43. [Scenario 18: Query with Date Range Returns No Results](#scenario-18-query-with-date-range-returns-no-results)
44. [Scenario 19: Duplicate Key Error on INSERT](#scenario-19-duplicate-key-error-on-insert)
45. [Scenario 20: CTE Referenced Multiple Times Performance](#scenario-20-cte-referenced-multiple-times-performance)
46. [Scenario 21: UNION Removing Valid Duplicates](#scenario-21-union-removing-valid-duplicates)
47. [Scenario 22: Index Not Being Used Despite Existing](#scenario-22-index-not-being-used-despite-existing)
48. [Scenario 23: Recursive Query Hitting Maximum Recursion](#scenario-23-recursive-query-hitting-maximum-recursion)
49. [Scenario 24: Bulk Insert Performance Issues](#scenario-24-bulk-insert-performance-issues)
50. [Scenario 25: Dynamic SQL Injection Risk](#scenario-25-dynamic-sql-injection-risk)

---

## 1. What is SQL?

### Description

SQL (Structured Query Language) is a standard programming language specifically designed for managing and manipulating relational databases. It allows developers to create, read, update, and delete (CRUD) data stored in database tables. SQL is declarative, meaning you specify *what* you want, not *how* to get it. In interviews, understanding SQL demonstrates your ability to work with data persistence, which is fundamental to backend development. Modern applications rely heavily on SQL databases like SQL Server, PostgreSQL, MySQL, and Oracle.

### Real-Time Example

```sql
-- Creating a table for storing employee information
CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Department VARCHAR(50),
    Salary DECIMAL(10, 2),
    HireDate DATE
);

-- Inserting employee records
INSERT INTO Employee (EmployeeId, FirstName, LastName, Department, Salary, HireDate)
VALUES (1, 'John', 'Doe', 'Engineering', 75000, '2022-01-15');

-- Querying employee data
SELECT FirstName, LastName, Salary 
FROM Employee 
WHERE Department = 'Engineering';
```

---

## 2. DDL, DML, DCL, and TCL Commands

### Description

SQL commands are categorized into four main types based on their functionality. DDL (Data Definition Language) deals with database structure, DML (Data Manipulation Language) handles data operations, DCL (Data Control Language) manages permissions, and TCL (Transaction Control Language) controls transactions. Understanding these categories helps in interviews when discussing database design, security, and transaction management. Each category serves a specific purpose in database management and is essential for building robust backend systems.

### Command Categories

| Category | Purpose | Common Commands | Use Case |
|----------|---------|-----------------|----------|
| **DDL** | Define database structure | CREATE, ALTER, DROP, TRUNCATE | Creating tables, modifying schema |
| **DML** | Manipulate data | SELECT, INSERT, UPDATE, DELETE | CRUD operations on data |
| **DCL** | Control access permissions | GRANT, REVOKE | Managing user access rights |
| **TCL** | Manage transactions | COMMIT, ROLLBACK, SAVEPOINT | Ensuring data consistency |

### Real-Time Example

```sql
-- DDL: Create a table structure
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY,
    CustomerId INT,
    OrderDate DATE,
    TotalAmount DECIMAL(10, 2)
);

-- DML: Insert and query data
INSERT INTO Orders (OrderId, CustomerId, OrderDate, TotalAmount)
VALUES (101, 5, '2024-01-20', 250.00);

SELECT * FROM Orders WHERE CustomerId = 5;

-- DCL: Grant access to a user
GRANT SELECT, INSERT ON Orders TO BackendUser;

-- TCL: Transaction management
BEGIN TRANSACTION;
UPDATE Orders SET TotalAmount = 300.00 WHERE OrderId = 101;
COMMIT;
```

---

## 3. Primary Key vs Foreign Key

### Description

Primary Key and Foreign Key are fundamental constraints that establish data integrity and relationships in relational databases. A Primary Key uniquely identifies each record in a table and cannot contain NULL values. A Foreign Key creates a link between two tables by referencing the Primary Key of another table. In interviews, this topic tests your understanding of database normalization and relational design. Proper use of these keys ensures data consistency and enables efficient joins across tables.

### Comparison

| Aspect | Primary Key | Foreign Key |
|--------|-------------|-------------|
| **Purpose** | Uniquely identifies each row in a table | Establishes relationship between tables |
| **Uniqueness** | Must be unique, no duplicates allowed | Can have duplicate values |
| **NULL Values** | Cannot contain NULL | Can contain NULL (unless specified NOT NULL) |
| **Number per Table** | Only one primary key per table | Can have multiple foreign keys |
| **Index** | Automatically creates a clustered index | Creates a non-clustered index |
| **References** | Referenced by foreign keys in other tables | References primary key in another table |

### Real-Time Example

```sql
-- Primary Key: Customer table
CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY,
    CustomerName VARCHAR(100),
    Email VARCHAR(100),
    Phone VARCHAR(15)
);

-- Foreign Key: Order table referencing Customer
CREATE TABLE Order (
    OrderId INT PRIMARY KEY,
    OrderDate DATE,
    CustomerId INT,
    TotalAmount DECIMAL(10, 2),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);

-- Insert data
INSERT INTO Customer (CustomerId, CustomerName, Email, Phone)
VALUES (1, 'Alice Johnson', 'alice@email.com', '555-1234');

INSERT INTO Order (OrderId, OrderDate, CustomerId, TotalAmount)
VALUES (101, '2024-01-15', 1, 599.99);

-- Query with relationship
SELECT c.CustomerName, o.OrderId, o.TotalAmount
FROM Customer c
INNER JOIN Order o ON c.CustomerId = o.CustomerId;
```

---

## 4. DELETE vs TRUNCATE vs DROP

### Description

DELETE, TRUNCATE, and DROP are three different commands for removing data, each with distinct behaviors and use cases. DELETE removes specific rows based on a condition and can be rolled back. TRUNCATE removes all rows quickly without logging individual deletions. DROP removes the entire table structure along with data. In interviews, understanding these differences demonstrates knowledge of data management strategies and transaction safety. Choosing the right command impacts performance, recoverability, and data integrity.

### Comparison

| Aspect | DELETE | TRUNCATE | DROP |
|--------|--------|----------|------|
| **Type** | DML command | DDL command | DDL command |
| **Removes** | Specific rows based on WHERE clause | All rows from table | Entire table structure and data |
| **WHERE Clause** | Supported (can delete selective rows) | Not supported (removes all rows) | Not applicable |
| **Rollback** | Can be rolled back (part of transaction) | Cannot be rolled back in most databases | Cannot be rolled back |
| **Performance** | Slower (logs each row deletion) | Faster (deallocates data pages) | Fastest (drops entire object) |
| **Identity Reset** | Does not reset identity column | Resets identity column to seed value | Not applicable (table removed) |
| **Triggers** | Fires DELETE triggers | Does not fire triggers | Does not fire triggers |
| **Storage** | Releases row space gradually | Releases all space immediately | Releases all space and removes table |

### Real-Time Example

```sql
-- Sample Employee table
CREATE TABLE Employee (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50),
    Department VARCHAR(50),
    Salary DECIMAL(10, 2)
);

INSERT INTO Employee (FirstName, Department, Salary)
VALUES ('John', 'IT', 60000), ('Sarah', 'HR', 55000), ('Mike', 'IT', 65000);

-- DELETE: Remove specific rows (can be rolled back)
DELETE FROM Employee WHERE Department = 'HR';
-- Result: Only HR employee removed, EmployeeId sequence continues

-- TRUNCATE: Remove all rows (fast, resets identity)
TRUNCATE TABLE Employee;
-- Result: All rows removed, next EmployeeId starts from 1

-- DROP: Remove entire table
DROP TABLE Employee;
-- Result: Table no longer exists in database
```

---

## 5. WHERE vs HAVING

### Description

WHERE and HAVING are both filtering clauses, but they operate at different stages of query execution. WHERE filters individual rows before grouping occurs, while HAVING filters grouped results after aggregation. In interviews, this distinction is crucial for demonstrating understanding of query processing order and aggregate functions. WHERE is used with regular columns, whereas HAVING is specifically designed for aggregate functions like COUNT, SUM, AVG. Using the wrong clause can lead to errors or incorrect results.

### Comparison

| Aspect | WHERE | HAVING |
|--------|-------|--------|
| **Purpose** | Filters rows before grouping | Filters groups after aggregation |
| **Usage** | Works with individual row data | Works with aggregate functions |
| **Execution Order** | Executes before GROUP BY | Executes after GROUP BY |
| **Aggregate Functions** | Cannot use aggregate functions directly | Can use aggregate functions (COUNT, SUM, AVG, etc.) |
| **Performance** | Generally faster (reduces rows early) | Slower (processes groups first) |
| **Common Use** | Filter individual records | Filter summarized/grouped data |

### Real-Time Example

```sql
-- Sample Orders table
CREATE TABLE Orders (
    OrderId INT,
    CustomerId INT,
    OrderDate DATE,
    Amount DECIMAL(10, 2)
);

INSERT INTO Orders VALUES 
(1, 101, '2024-01-10', 500),
(2, 102, '2024-01-11', 300),
(3, 101, '2024-01-12', 700),
(4, 103, '2024-01-13', 200),
(5, 101, '2024-01-14', 400);

-- WHERE: Filter individual orders before grouping
SELECT CustomerId, SUM(Amount) AS TotalAmount
FROM Orders
WHERE Amount > 300  -- Filter rows first
GROUP BY CustomerId;
-- Result: Only considers orders with Amount > 300

-- HAVING: Filter groups after aggregation
SELECT CustomerId, SUM(Amount) AS TotalAmount
FROM Orders
GROUP BY CustomerId
HAVING SUM(Amount) > 1000;  -- Filter grouped results
-- Result: Only shows customers whose total orders exceed 1000

-- Combined: WHERE + HAVING
SELECT CustomerId, COUNT(*) AS OrderCount, SUM(Amount) AS TotalAmount
FROM Orders
WHERE OrderDate >= '2024-01-11'  -- Filter rows first
GROUP BY CustomerId
HAVING COUNT(*) >= 2;  -- Filter groups
-- Result: Customers with 2+ orders placed after Jan 11
```

---

## 6. INNER JOIN vs LEFT JOIN vs RIGHT JOIN vs FULL JOIN

### Description

JOINs combine rows from two or more tables based on related columns, and understanding the different types is essential for backend developers. INNER JOIN returns only matching rows from both tables. LEFT JOIN returns all rows from the left table plus matching rows from right. RIGHT JOIN is the opposite of LEFT JOIN. FULL JOIN returns all rows from both tables. In interviews, JOIN questions test your ability to retrieve related data and handle missing relationships. Choosing the correct JOIN type impacts query results and application logic.

### Comparison

| Join Type | Returns | Use Case | NULL Handling |
|-----------|---------|----------|---------------|
| **INNER JOIN** | Only matching rows from both tables | When you need records that exist in both tables | No NULLs (unmatched rows excluded) |
| **LEFT JOIN** | All rows from left table + matching from right | When you need all records from main table, even without matches | NULLs for non-matching right table columns |
| **RIGHT JOIN** | All rows from right table + matching from left | When you need all records from second table | NULLs for non-matching left table columns |
| **FULL JOIN** | All rows from both tables | When you need complete data from both tables | NULLs for non-matching columns on both sides |
| **CROSS JOIN** | Cartesian product (all combinations) | Rarely used; generates all possible combinations | No NULL handling (all combinations returned) |

### Real-Time Example

```sql
-- Sample tables
CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY,
    CustomerName VARCHAR(100)
);

CREATE TABLE Order (
    OrderId INT PRIMARY KEY,
    CustomerId INT,
    OrderDate DATE,
    Amount DECIMAL(10, 2)
);

INSERT INTO Customer VALUES (1, 'Alice'), (2, 'Bob'), (3, 'Charlie');
INSERT INTO Order VALUES (101, 1, '2024-01-15', 500), (102, 1, '2024-01-16', 300), (103, 2, '2024-01-17', 700);

-- INNER JOIN: Only customers who have orders
SELECT c.CustomerName, o.OrderId, o.Amount
FROM Customer c
INNER JOIN Order o ON c.CustomerId = o.CustomerId;
-- Result: Alice (2 orders), Bob (1 order) - Charlie excluded

-- LEFT JOIN: All customers, including those without orders
SELECT c.CustomerName, o.OrderId, o.Amount
FROM Customer c
LEFT JOIN Order o ON c.CustomerId = o.CustomerId;
-- Result: Alice, Bob, Charlie (Charlie has NULL for OrderId and Amount)

-- RIGHT JOIN: All orders, including if customer deleted
SELECT c.CustomerName, o.OrderId, o.Amount
FROM Customer c
RIGHT JOIN Order o ON c.CustomerId = o.CustomerId;
-- Result: All orders shown, customer name NULL if customer doesn't exist

-- FULL JOIN: All customers and all orders
SELECT c.CustomerName, o.OrderId, o.Amount
FROM Customer c
FULL OUTER JOIN Order o ON c.CustomerId = o.CustomerId;
-- Result: All customers (even without orders) and all orders (even without customers)
```

---

## 7. UNION vs UNION ALL

### Description

UNION and UNION ALL combine result sets from multiple SELECT statements into a single result. UNION removes duplicate rows and sorts the result, while UNION ALL includes all rows including duplicates. In interviews, this topic tests understanding of set operations and performance considerations. UNION is useful when you need distinct combined results, but UNION ALL is faster when duplicates are acceptable or you know there won't be duplicates. Both require the same number and compatible data types of columns in each SELECT statement.

### Comparison

| Aspect | UNION | UNION ALL |
|--------|-------|-----------|
| **Duplicates** | Removes duplicate rows | Keeps all rows including duplicates |
| **Performance** | Slower (requires sorting and deduplication) | Faster (no duplicate check or sorting) |
| **Sorting** | Performs implicit sorting to remove duplicates | No sorting performed |
| **Use Case** | When you need distinct combined results | When duplicates are acceptable or impossible |
| **Memory Usage** | Higher (needs to compare all rows) | Lower (simple concatenation) |
| **Recommendation** | Use only when duplicates must be removed | Preferred when duplicates are not an issue |

### Real-Time Example

```sql
-- Sample tables
CREATE TABLE CurrentEmployees (
    EmployeeId INT,
    EmployeeName VARCHAR(100),
    Department VARCHAR(50)
);

CREATE TABLE FormerEmployees (
    EmployeeId INT,
    EmployeeName VARCHAR(100),
    Department VARCHAR(50)
);

INSERT INTO CurrentEmployees VALUES 
(1, 'John Doe', 'IT'), 
(2, 'Jane Smith', 'HR'), 
(3, 'Mike Johnson', 'IT');

INSERT INTO FormerEmployees VALUES 
(4, 'Sarah Williams', 'Finance'), 
(2, 'Jane Smith', 'HR'),  -- Duplicate (rehired employee)
(5, 'Tom Brown', 'IT');

-- UNION: Removes duplicates (Jane Smith appears once)
SELECT EmployeeName, Department FROM CurrentEmployees
UNION
SELECT EmployeeName, Department FROM FormerEmployees;
-- Result: 5 rows (Jane Smith counted once)

-- UNION ALL: Keeps all rows including duplicates
SELECT EmployeeName, Department FROM CurrentEmployees
UNION ALL
SELECT EmployeeName, Department FROM FormerEmployees;
-- Result: 6 rows (Jane Smith appears twice)

-- Practical use: Get all IT department employees (current and former)
SELECT EmployeeName, 'Current' AS Status FROM CurrentEmployees WHERE Department = 'IT'
UNION ALL
SELECT EmployeeName, 'Former' AS Status FROM FormerEmployees WHERE Department = 'IT';
-- Result: All IT employees with their employment status
```

---

## 8. What are Indexes and Why Use Them?

### Description

An index is a database object that improves the speed of data retrieval operations on a table at the cost of additional storage and slower write operations. Indexes work similarly to a book's index, allowing the database to find rows quickly without scanning the entire table. In interviews, understanding indexes demonstrates knowledge of performance optimization. While indexes dramatically improve SELECT query performance, they add overhead to INSERT, UPDATE, and DELETE operations. Proper index strategy is crucial for building scalable applications that handle large datasets efficiently.

### Real-Time Example

```sql
-- Create Employee table without index
CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY,  -- Primary key automatically creates clustered index
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Department VARCHAR(50),
    Email VARCHAR(100),
    Salary DECIMAL(10, 2)
);

-- Insert sample data (imagine 1 million records)
INSERT INTO Employee VALUES (1, 'John', 'Doe', 'IT', 'john@company.com', 75000);

-- Without index: Full table scan (slow on large tables)
SELECT * FROM Employee WHERE Email = 'john@company.com';
-- Execution: Scans all 1 million rows

-- Create index on Email column
CREATE INDEX IX_Employee_Email ON Employee(Email);

-- With index: Fast lookup (uses index seek)
SELECT * FROM Employee WHERE Email = 'john@company.com';
-- Execution: Uses index to find row directly

-- Composite index for frequently used filter combinations
CREATE INDEX IX_Employee_Department_Salary ON Employee(Department, Salary);

-- Benefits from composite index
SELECT EmployeeId, FirstName, LastName 
FROM Employee 
WHERE Department = 'IT' AND Salary > 70000;
-- Execution: Uses composite index for efficient filtering

-- View all indexes on a table
EXEC sp_helpindex 'Employee';
```

**When to Use Indexes:**
* ? Columns frequently used in WHERE clauses
* ? Columns used in JOIN conditions
* ? Columns used in ORDER BY or GROUP BY
* ? Foreign key columns
* ? Avoid on tables with frequent INSERT/UPDATE/DELETE
* ? Avoid on columns with low selectivity (few distinct values)

---

## 9. Clustered vs Non-Clustered Index

### Description

Clustered and Non-Clustered indexes are the two fundamental types of indexes in SQL databases. A Clustered Index determines the physical order of data in a table, and each table can have only one clustered index (usually the primary key). A Non-Clustered Index creates a separate structure that points to the actual data rows. In interviews, this topic tests deep understanding of database internals and performance tuning. The choice between clustered and non-clustered indexes significantly impacts query performance, storage, and data modification speed.

### Comparison

| Aspect | Clustered Index | Non-Clustered Index |
|--------|-----------------|---------------------|
| **Storage** | Data rows stored in index order (physical ordering) | Separate structure with pointers to data rows |
| **Number per Table** | Only one per table | Multiple allowed (up to 999 in SQL Server) |
| **Leaf Nodes** | Contains actual data rows | Contains pointers to data rows |
| **Performance** | Faster for range queries and sorting | Faster for covering queries (all columns in index) |
| **Default Creation** | Automatically created with PRIMARY KEY | Created explicitly on columns |
| **Memory Usage** | No extra storage (data itself is organized) | Additional storage for index structure |
| **Insert/Update Impact** | Slower (may require data reorganization) | Faster (only index structure updated) |

### Real-Time Example

```sql
-- Create table with clustered index (Primary Key)
CREATE TABLE Vehicle (
    VehicleId INT PRIMARY KEY,  -- Creates clustered index automatically
    LicensePlate VARCHAR(20),
    Make VARCHAR(50),
    Model VARCHAR(50),
    Year INT,
    OwnerId INT
);

-- Data is physically stored sorted by VehicleId
-- VehicleId: [1, 2, 3, 4, 5...] physically on disk

-- Create non-clustered index on LicensePlate
CREATE NONCLUSTERED INDEX IX_Vehicle_LicensePlate 
ON Vehicle(LicensePlate);

-- Non-clustered index structure:
-- LicensePlate -> Pointer to VehicleId
-- "ABC123" -> VehicleId 5
-- "XYZ789" -> VehicleId 2

-- Clustered index scan (efficient for range queries)
SELECT * FROM Vehicle 
WHERE VehicleId BETWEEN 100 AND 200;
-- Fast: Data already sorted by VehicleId

-- Non-clustered index seek
SELECT VehicleId, Make, Model 
FROM Vehicle 
WHERE LicensePlate = 'ABC123';
-- Step 1: Find 'ABC123' in non-clustered index
-- Step 2: Use pointer to retrieve row from clustered index

-- Covering index (non-clustered index with included columns)
CREATE NONCLUSTERED INDEX IX_Vehicle_OwnerId_Include 
ON Vehicle(OwnerId) 
INCLUDE (Make, Model, Year);

-- Query fully satisfied by covering index (no need to access table)
SELECT Make, Model, Year 
FROM Vehicle 
WHERE OwnerId = 42;
-- Fastest: All data in index, no table lookup needed
```

**Decision Guide:**
* **Clustered Index:** Primary key, unique identifier, frequently used for range queries
* **Non-Clustered Index:** Foreign keys, frequently searched columns, columns in WHERE/JOIN clauses

---

## 10. Subquery vs CTE (Common Table Expression)

### Description

Subqueries and CTEs (Common Table Expressions) are both methods to write complex queries by breaking them into manageable parts. A subquery is a query nested inside another query, while a CTE is a temporary named result set defined using WITH clause. In interviews, this topic demonstrates ability to write maintainable and readable SQL code. CTEs are generally preferred for complex queries due to better readability and ability to reference the same result set multiple times. CTEs can also be recursive, which subqueries cannot.

### Comparison

| Aspect | Subquery | CTE (Common Table Expression) |
|--------|----------|-------------------------------|
| **Syntax** | Nested inside SELECT, WHERE, FROM | Defined with WITH clause before main query |
| **Readability** | Can become complex and hard to read | More readable and maintainable |
| **Reusability** | Cannot reference multiple times in same query | Can be referenced multiple times |
| **Recursion** | Not possible | Supports recursive queries |
| **Performance** | May execute multiple times if not optimized | Executed once, can be reused |
| **Debugging** | Harder to debug nested logic | Easier to debug step-by-step |
| **Scope** | Limited to the statement where it's defined | Available throughout the query |

### Real-Time Example

```sql
-- Sample Orders and Customers tables
CREATE TABLE Customer (CustomerId INT, CustomerName VARCHAR(100), City VARCHAR(50));
CREATE TABLE Order (OrderId INT, CustomerId INT, OrderDate DATE, Amount DECIMAL(10,2));

INSERT INTO Customer VALUES (1, 'Alice', 'New York'), (2, 'Bob', 'Los Angeles'), (3, 'Charlie', 'Chicago');
INSERT INTO Order VALUES (101, 1, '2024-01-15', 500), (102, 1, '2024-01-20', 700), (103, 2, '2024-01-18', 300);

-- SUBQUERY: Find customers with total orders > 1000
SELECT CustomerName, City
FROM Customer
WHERE CustomerId IN (
    SELECT CustomerId 
    FROM Order 
    GROUP BY CustomerId 
    HAVING SUM(Amount) > 1000
);

-- CTE: Same logic, more readable
WITH HighValueCustomers AS (
    SELECT CustomerId, SUM(Amount) AS TotalAmount
    FROM Order
    GROUP BY CustomerId
    HAVING SUM(Amount) > 1000
)
SELECT c.CustomerName, c.City, h.TotalAmount
FROM Customer c
INNER JOIN HighValueCustomers h ON c.CustomerId = h.CustomerId;

-- CTE Reusability: Use same CTE multiple times
WITH OrderSummary AS (
    SELECT CustomerId, COUNT(*) AS OrderCount, SUM(Amount) AS TotalAmount
    FROM Order
    GROUP BY CustomerId
)
SELECT 
    (SELECT AVG(OrderCount) FROM OrderSummary) AS AvgOrderCount,
    (SELECT AVG(TotalAmount) FROM OrderSummary) AS AvgOrderValue,
    (SELECT MAX(TotalAmount) FROM OrderSummary) AS MaxOrderValue;

-- Recursive CTE: Employee hierarchy
CREATE TABLE Employee (EmployeeId INT, EmployeeName VARCHAR(100), ManagerId INT);
INSERT INTO Employee VALUES (1, 'CEO', NULL), (2, 'VP Sales', 1), (3, 'Sales Manager', 2), (4, 'Sales Rep', 3);

WITH EmployeeHierarchy AS (
    -- Anchor: Top-level employees
    SELECT EmployeeId, EmployeeName, ManagerId, 0 AS Level
    FROM Employee
    WHERE ManagerId IS NULL
    
    UNION ALL
    
    -- Recursive: Employees under each manager
    SELECT e.EmployeeId, e.EmployeeName, e.ManagerId, h.Level + 1
    FROM Employee e
    INNER JOIN EmployeeHierarchy h ON e.ManagerId = h.EmployeeId
)
SELECT EmployeeId, REPLICATE('  ', Level) + EmployeeName AS EmployeeName, Level
FROM EmployeeHierarchy
ORDER BY Level, EmployeeId;
-- Result shows organizational hierarchy with indentation
```

---

## 11. Normalization vs Denormalization

### Description

Normalization is the process of organizing database tables to reduce redundancy and improve data integrity, while denormalization intentionally introduces redundancy to improve read performance. Normalization follows forms (1NF, 2NF, 3NF, BCNF) to eliminate data anomalies. Denormalization is used in data warehousing and reporting systems where read performance is critical. In interviews, this demonstrates understanding of database design trade-offs. The choice depends on whether the system is OLTP (transaction-heavy, prefer normalization) or OLAP (analytics-heavy, prefer denormalization).

### Comparison

| Aspect | Normalization | Denormalization |
|--------|---------------|-----------------|
| **Purpose** | Reduce redundancy, improve data integrity | Improve read performance |
| **Redundancy** | Minimized (data stored once) | Intentionally added (data duplicated) |
| **Performance** | Slower reads (requires JOINs), faster writes | Faster reads (fewer JOINs), slower writes |
| **Storage** | Less storage space | More storage space |
| **Data Integrity** | High (update in one place) | Lower (risk of inconsistency) |
| **Use Case** | OLTP systems (banking, e-commerce) | OLAP systems (data warehouses, reporting) |
| **Complexity** | More tables, more JOINs | Fewer tables, simpler queries |
| **Updates** | Easier and safer | Riskier (must update multiple places) |

### Real-Time Example

```sql
-- NORMALIZED DESIGN (3NF): E-commerce system
CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY,
    CustomerName VARCHAR(100),
    Email VARCHAR(100)
);

CREATE TABLE Product (
    ProductId INT PRIMARY KEY,
    ProductName VARCHAR(100),
    CategoryId INT,
    Price DECIMAL(10,2)
);

CREATE TABLE Category (
    CategoryId INT PRIMARY KEY,
    CategoryName VARCHAR(50)
);

CREATE TABLE Order (
    OrderId INT PRIMARY KEY,
    CustomerId INT,
    OrderDate DATE,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);

CREATE TABLE OrderDetail (
    OrderDetailId INT PRIMARY KEY,
    OrderId INT,
    ProductId INT,
    Quantity INT,
    FOREIGN KEY (OrderId) REFERENCES Order(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);

-- Query requires multiple JOINs (slower for reporting)
SELECT 
    c.CustomerName,
    o.OrderDate,
    p.ProductName,
    cat.CategoryName,
    od.Quantity,
    p.Price * od.Quantity AS LineTotal
FROM Order o
INNER JOIN Customer c ON o.CustomerId = c.CustomerId
INNER JOIN OrderDetail od ON o.OrderId = od.OrderId
INNER JOIN Product p ON od.ProductId = p.ProductId
INNER JOIN Category cat ON p.CategoryId = cat.CategoryId;

-- DENORMALIZED DESIGN: Reporting/Analytics table
CREATE TABLE OrderReportFlat (
    OrderId INT,
    OrderDate DATE,
    CustomerId INT,
    CustomerName VARCHAR(100),
    CustomerEmail VARCHAR(100),
    ProductId INT,
    ProductName VARCHAR(100),
    CategoryName VARCHAR(50),  -- Denormalized: Category duplicated
    Quantity INT,
    Price DECIMAL(10,2),
    LineTotal DECIMAL(10,2),
    PRIMARY KEY (OrderId, ProductId)
);

-- Simple, fast query for reporting (no JOINs)
SELECT 
    CustomerName,
    OrderDate,
    ProductName,
    CategoryName,
    Quantity,
    LineTotal
FROM OrderReportFlat
WHERE OrderDate BETWEEN '2024-01-01' AND '2024-01-31'
ORDER BY OrderDate;
-- Much faster for analytics, but data is duplicated
```

**Decision Guide:**
* **Normalization:** OLTP systems, frequent updates, data integrity critical
* **Denormalization:** Data warehouses, reporting systems, read-heavy workloads

---

## 12. Stored Procedures vs Functions

### Description

Stored Procedures and Functions are both precompiled SQL code blocks stored in the database, but they serve different purposes. Stored Procedures are used for performing actions and can modify database state, while Functions are designed to compute and return values without side effects. In interviews, this topic tests understanding of database programming and when to use each construct. Stored Procedures can return multiple values via output parameters, execute DML statements, and use transactions. Functions must return a single value or table and cannot modify database state.

### Comparison

| Aspect | Stored Procedure | Function |
|--------|------------------|----------|
| **Purpose** | Perform actions, business logic | Compute and return values |
| **Return Value** | Optional (can use output parameters) | Mandatory (must return value or table) |
| **DML Statements** | Can execute INSERT, UPDATE, DELETE | Cannot execute DML statements |
| **Transactions** | Can use transactions (BEGIN, COMMIT, ROLLBACK) | Cannot use transactions |
| **Calling** | EXEC or EXECUTE statement | Can be used in SELECT, WHERE clauses |
| **Error Handling** | TRY-CATCH blocks allowed | Limited error handling |
| **Side Effects** | Can modify database state | Should not have side effects |
| **Performance** | Generally faster for complex operations | Faster for inline computations |

### Real-Time Example

```sql
-- STORED PROCEDURE: Process customer order
CREATE PROCEDURE ProcessOrder
    @CustomerId INT,
    @ProductId INT,
    @Quantity INT,
    @OrderId INT OUTPUT  -- Output parameter
AS
BEGIN
    BEGIN TRANSACTION;
    
    DECLARE @Price DECIMAL(10,2);
    DECLARE @StockAvailable INT;
    
    -- Check stock availability
    SELECT @Price = Price, @StockAvailable = StockQuantity
    FROM Product
    WHERE ProductId = @ProductId;
    
    IF @StockAvailable < @Quantity
    BEGIN
        ROLLBACK TRANSACTION;
        RAISERROR('Insufficient stock', 16, 1);
        RETURN;
    END
    
    -- Create order
    INSERT INTO Order (CustomerId, OrderDate, TotalAmount)
    VALUES (@CustomerId, GETDATE(), @Price * @Quantity);
    
    SET @OrderId = SCOPE_IDENTITY();
    
    -- Update stock
    UPDATE Product
    SET StockQuantity = StockQuantity - @Quantity
    WHERE ProductId = @ProductId;
    
    COMMIT TRANSACTION;
END;

-- Execute stored procedure
DECLARE @NewOrderId INT;
EXEC ProcessOrder @CustomerId = 1, @ProductId = 101, @Quantity = 5, @OrderId = @NewOrderId OUTPUT;
SELECT @NewOrderId AS CreatedOrderId;

-- SCALAR FUNCTION: Calculate order total with tax
CREATE FUNCTION CalculateOrderTotalWithTax
(
    @OrderAmount DECIMAL(10,2),
    @TaxRate DECIMAL(5,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Total DECIMAL(10,2);
    SET @Total = @OrderAmount + (@OrderAmount * @TaxRate / 100);
    RETURN @Total;
END;

-- Use function in SELECT query
SELECT 
    OrderId, 
    OrderAmount,
    dbo.CalculateOrderTotalWithTax(OrderAmount, 8.5) AS TotalWithTax
FROM Order;

-- TABLE-VALUED FUNCTION: Get customer orders
CREATE FUNCTION GetCustomerOrders(@CustomerId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT OrderId, OrderDate, TotalAmount
    FROM Order
    WHERE CustomerId = @CustomerId
);

-- Use table-valued function
SELECT * FROM dbo.GetCustomerOrders(1);

-- Use in JOIN
SELECT c.CustomerName, o.OrderId, o.TotalAmount
FROM Customer c
CROSS APPLY dbo.GetCustomerOrders(c.CustomerId) o;
```

**When to Use:**
* **Stored Procedure:** Complex business logic, data modifications, transaction management
* **Function:** Calculations, data transformations, reusable computations in queries

---

## 13. What are Transactions and ACID Properties?

### Description

A transaction is a logical unit of work that contains one or more SQL statements, executed as a single unit that either completely succeeds or completely fails. ACID (Atomicity, Consistency, Isolation, Durability) properties ensure data integrity in concurrent database operations. In interviews, understanding transactions demonstrates knowledge of data consistency and reliability. Proper transaction management is critical for financial systems, inventory management, and any scenario where data integrity cannot be compromised. Transactions prevent partial updates that could leave data in an inconsistent state.

### ACID Properties

| Property | Description | Example |
|----------|-------------|---------|
| **Atomicity** | All operations succeed or all fail (all-or-nothing) | Bank transfer: debit and credit must both happen or neither |
| **Consistency** | Database moves from one valid state to another | Account balance cannot be negative after transaction |
| **Isolation** | Concurrent transactions don't interfere with each other | Two users booking the same seat don't both succeed |
| **Durability** | Committed changes are permanent (survive system failure) | Once payment confirmed, order persists even if server crashes |

### Real-Time Example

```sql
-- Bank Transfer: Demonstrates all ACID properties
CREATE TABLE Account (
    AccountId INT PRIMARY KEY,
    AccountHolder VARCHAR(100),
    Balance DECIMAL(10,2)
);

INSERT INTO Account VALUES (1, 'Alice', 1000), (2, 'Bob', 500);

-- TRANSACTION: Transfer $200 from Alice to Bob
BEGIN TRANSACTION;

DECLARE @TransferAmount DECIMAL(10,2) = 200;
DECLARE @SourceAccount INT = 1;
DECLARE @DestAccount INT = 2;
DECLARE @SourceBalance DECIMAL(10,2);

-- Check source account balance
SELECT @SourceBalance = Balance FROM Account WHERE AccountId = @SourceAccount;

IF @SourceBalance < @TransferAmount
BEGIN
    PRINT 'Insufficient funds';
    ROLLBACK TRANSACTION;  -- ATOMICITY: Nothing changes
    RETURN;
END

-- Debit source account
UPDATE Account 
SET Balance = Balance - @TransferAmount 
WHERE AccountId = @SourceAccount;

-- Simulate error (commented out for demo)
-- RAISERROR('System error', 16, 1);

-- Credit destination account
UPDATE Account 
SET Balance = Balance + @TransferAmount 
WHERE AccountId = @DestAccount;

-- CONSISTENCY: Check balances are still valid
IF EXISTS (SELECT 1 FROM Account WHERE Balance < 0)
BEGIN
    ROLLBACK TRANSACTION;
    RAISERROR('Invalid balance detected', 16, 1);
    RETURN;
END

COMMIT TRANSACTION;  -- DURABILITY: Changes permanent

-- Verify results
SELECT * FROM Account;
-- Alice: $800, Bob: $700

-- ISOLATION: Demonstrate isolation levels
-- Session 1: Start transaction
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
BEGIN TRANSACTION;
UPDATE Account SET Balance = Balance - 100 WHERE AccountId = 1;
-- Not yet committed

-- Session 2: Try to read (blocked or sees old value depending on isolation level)
SELECT Balance FROM Account WHERE AccountId = 1;

-- Session 1: Commit
COMMIT TRANSACTION;

-- Session 2: Now sees updated value
SELECT Balance FROM Account WHERE AccountId = 1;

-- SAVEPOINT: Partial rollback
BEGIN TRANSACTION;

UPDATE Account SET Balance = Balance - 50 WHERE AccountId = 1;
SAVE TRANSACTION SavePoint1;

UPDATE Account SET Balance = Balance + 50 WHERE AccountId = 2;

-- Rollback only the second update
ROLLBACK TRANSACTION SavePoint1;

COMMIT TRANSACTION;
-- Only first update applied
```

**Transaction Isolation Levels:**
01. **READ UNCOMMITTED**: Lowest isolation, allows dirty reads
02. **READ COMMITTED**: Default, prevents dirty reads
03. **REPEATABLE READ**: Prevents non-repeatable reads
04. **SERIALIZABLE**: Highest isolation, prevents phantom reads

---

## 14. Locks and Deadlocks

### Description

Locks are mechanisms that prevent concurrent transactions from conflicting with each other, ensuring data consistency. A deadlock occurs when two or more transactions are waiting for each other to release locks, creating a circular dependency. In interviews, this topic demonstrates understanding of concurrency control and database performance under load. SQL Server automatically detects deadlocks and kills one transaction (deadlock victim) to resolve the issue. Proper indexing, short transactions, and accessing resources in consistent order help prevent deadlocks.

### Lock Types

| Lock Type | Description | Example Scenario |
|-----------|-------------|------------------|
| **Shared (S)** | Allows concurrent reads, prevents writes | Multiple users reading same customer record |
| **Exclusive (X)** | Prevents both reads and writes | Updating account balance |
| **Update (U)** | Prevents deadlocks during updates | Finding record then updating it |
| **Intent** | Indicates intention to acquire locks at lower level | Table-level lock before row-level lock |
| **Schema** | Prevents schema changes during query | ALTER TABLE blocked during SELECT |

### Real-Time Example

```sql
-- Setup: Inventory and Order tables
CREATE TABLE Inventory (
    ProductId INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Quantity INT
);

CREATE TABLE OrderQueue (
    OrderId INT PRIMARY KEY,
    ProductId INT,
    QuantityOrdered INT,
    Status VARCHAR(20)
);

INSERT INTO Inventory VALUES (1, 'Laptop', 10), (2, 'Mouse', 50);
INSERT INTO OrderQueue VALUES (101, 1, 2, 'Pending'), (102, 2, 5, 'Pending');

-- DEADLOCK SCENARIO
-- Session 1: Process Order 101
BEGIN TRANSACTION;
-- Lock Inventory for Product 1
UPDATE Inventory SET Quantity = Quantity - 2 WHERE ProductId = 1;
WAITFOR DELAY '00:00:05';  -- Simulate processing time
-- Try to lock OrderQueue
UPDATE OrderQueue SET Status = 'Completed' WHERE OrderId = 101;
COMMIT TRANSACTION;

-- Session 2: Process Order 102 (runs simultaneously)
BEGIN TRANSACTION;
-- Lock OrderQueue for Order 102
UPDATE OrderQueue SET Status = 'Processing' WHERE OrderId = 102;
WAITFOR DELAY '00:00:05';  -- Simulate processing time
-- Try to lock Inventory (DEADLOCK occurs here)
UPDATE Inventory SET Quantity = Quantity - 5 WHERE ProductId = 2;
COMMIT TRANSACTION;
-- One session will be deadlock victim

-- PREVENT DEADLOCK: Access resources in consistent order
-- Session 1: 
BEGIN TRANSACTION;
UPDATE Inventory SET Quantity = Quantity - 2 WHERE ProductId = 1;
UPDATE OrderQueue SET Status = 'Completed' WHERE OrderId = 101;
COMMIT TRANSACTION;

-- Session 2:
BEGIN TRANSACTION;
UPDATE Inventory SET Quantity = Quantity - 5 WHERE ProductId = 2;  -- Access Inventory first
UPDATE OrderQueue SET Status = 'Completed' WHERE OrderId = 102;
COMMIT TRANSACTION;

-- LOCK TIMEOUT: Handle locked resources gracefully
SET LOCK_TIMEOUT 5000;  -- Wait max 5 seconds for lock

BEGIN TRANSACTION;
BEGIN TRY
    UPDATE Inventory SET Quantity = Quantity - 1 WHERE ProductId = 1;
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'Lock timeout - resource is busy';
END CATCH;

-- NOLOCK HINT: Read without waiting (dirty read risk)
SELECT ProductName, Quantity 
FROM Inventory WITH (NOLOCK)
WHERE ProductId = 1;
-- Reads uncommitted data, no blocking

-- VIEW CURRENT LOCKS
SELECT 
    resource_type,
    resource_description,
    request_mode,
    request_status
FROM sys.dm_tran_locks
WHERE resource_database_id = DB_ID();

-- DEADLOCK RETRY PATTERN (application code)
-- Pseudo-code:
-- int retries = 3;
-- while (retries > 0) {
--     try {
--         ExecuteTransaction();
--         break;
--     } catch (DeadlockException) {
--         retries--;
--         Thread.Sleep(random);
--     }
-- }
```

**Best Practices to Avoid Deadlocks:**
* ? Keep transactions short
* ? Access tables in same order across transactions
* ? Use appropriate isolation levels
* ? Create proper indexes to reduce lock duration
* ? Avoid user interaction during transactions

---

## 15. How to Find Duplicate Records?

### Description

Finding duplicate records is a common interview question that tests knowledge of GROUP BY, HAVING, and aggregate functions. Duplicates can occur due to data entry errors, missing unique constraints, or data integration issues. In real applications, identifying duplicates is crucial for data quality, reporting accuracy, and maintaining database integrity. The solution typically involves grouping by the columns that should be unique and using HAVING COUNT(*) > 1 to filter groups with duplicates.

### Real-Time Example

```sql
-- Sample Employee table with duplicates
CREATE TABLE Employee (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100),
    Department VARCHAR(50)
);

INSERT INTO Employee (FirstName, LastName, Email, Department) VALUES
('John', 'Doe', 'john.doe@company.com', 'IT'),
('Jane', 'Smith', 'jane.smith@company.com', 'HR'),
('John', 'Doe', 'john.doe@company.com', 'IT'),  -- Duplicate
('Mike', 'Johnson', 'mike.j@company.com', 'Finance'),
('Jane', 'Smith', 'jane.smith@company.com', 'HR'),  -- Duplicate
('Sarah', 'Williams', 'sarah.w@company.com', 'IT');

-- METHOD 1: Find duplicate emails
SELECT Email, COUNT(*) AS DuplicateCount
FROM Employee
GROUP BY Email
HAVING COUNT(*) > 1;
-- Result: john.doe@company.com (2), jane.smith@company.com (2)

-- METHOD 2: Show all details of duplicate records
SELECT e.*
FROM Employee e
INNER JOIN (
    SELECT Email
    FROM Employee
    GROUP BY Email
    HAVING COUNT(*) > 1
) AS Duplicates ON e.Email = Duplicates.Email
ORDER BY e.Email;

-- METHOD 3: Using CTE (more readable)
WITH DuplicateEmails AS (
    SELECT Email, COUNT(*) AS Count
    FROM Employee
    GROUP BY Email
    HAVING COUNT(*) > 1
)
SELECT e.*
FROM Employee e
INNER JOIN DuplicateEmails d ON e.Email = d.Email
ORDER BY e.Email, e.EmployeeId;

-- METHOD 4: Find duplicates based on multiple columns
SELECT FirstName, LastName, Email, COUNT(*) AS DuplicateCount
FROM Employee
GROUP BY FirstName, LastName, Email
HAVING COUNT(*) > 1;

-- METHOD 5: Using ROW_NUMBER() to identify duplicates
WITH RankedEmployees AS (
    SELECT *,
        ROW_NUMBER() OVER (PARTITION BY Email ORDER BY EmployeeId) AS RowNum
    FROM Employee
)
SELECT *
FROM RankedEmployees
WHERE RowNum > 1;
-- Shows only duplicate rows (keeping first occurrence)

-- DELETE DUPLICATES: Keep only first occurrence
WITH RankedEmployees AS (
    SELECT *,
        ROW_NUMBER() OVER (PARTITION BY Email ORDER BY EmployeeId) AS RowNum
    FROM Employee
)
DELETE FROM RankedEmployees
WHERE RowNum > 1;

-- PREVENT DUPLICATES: Add unique constraint
ALTER TABLE Employee
ADD CONSTRAINT UK_Employee_Email UNIQUE (Email);
-- Future duplicate inserts will fail

-- Find duplicate combinations (FirstName + LastName)
SELECT FirstName, LastName, COUNT(*) AS Count
FROM Employee
GROUP BY FirstName, LastName
HAVING COUNT(*) > 1;
```

**Common Interview Variations:**
01. Find duplicates in a specific column
02. Find duplicates based on multiple columns
03. Delete duplicates keeping first/last occurrence
04. Count total number of duplicate records
05. Find records that appear exactly N times

---

## 16. Pagination using OFFSET and LIMIT

### Description

Pagination is essential for handling large datasets in web applications by retrieving data in smaller chunks or pages. The OFFSET and FETCH NEXT (SQL Server) or LIMIT (MySQL, PostgreSQL) clauses allow efficient pagination without loading all records into memory. In interviews, this demonstrates understanding of performance optimization and user experience. Proper pagination improves application responsiveness and reduces server load. For very large datasets, keyset pagination (using WHERE clause with last seen ID) is more efficient than OFFSET.

### Real-Time Example

```sql
-- Sample Product table
CREATE TABLE Product (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10,2),
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Insert sample data
INSERT INTO Product (ProductName, Category, Price)
VALUES 
('Laptop', 'Electronics', 999.99),
('Mouse', 'Electronics', 29.99),
('Keyboard', 'Electronics', 79.99),
('Desk Chair', 'Furniture', 199.99),
('Monitor', 'Electronics', 299.99),
('Desk Lamp', 'Furniture', 49.99),
('Headphones', 'Electronics', 149.99),
('Notebook', 'Stationery', 5.99),
('Pen Set', 'Stationery', 12.99),
('Webcam', 'Electronics', 89.99);

-- SQL SERVER: OFFSET and FETCH NEXT
DECLARE @PageNumber INT = 2;
DECLARE @PageSize INT = 3;

SELECT ProductId, ProductName, Category, Price
FROM Product
ORDER BY ProductId  -- ORDER BY is required for OFFSET
OFFSET (@PageNumber - 1) * @PageSize ROWS  -- Skip first N rows
FETCH NEXT @PageSize ROWS ONLY;  -- Take next N rows
-- Page 1: Products 1-3, Page 2: Products 4-6, Page 3: Products 7-9

-- MySQL/PostgreSQL: LIMIT and OFFSET
-- SELECT ProductId, ProductName, Category, Price
-- FROM Product
-- ORDER BY ProductId
-- LIMIT @PageSize OFFSET (@PageNumber - 1) * @PageSize;

-- Complete pagination with total count
DECLARE @PageNumber INT = 1;
DECLARE @PageSize INT = 5;
DECLARE @TotalRecords INT;
DECLARE @TotalPages INT;

-- Get total records
SELECT @TotalRecords = COUNT(*) FROM Product;
SET @TotalPages = CEILING(@TotalRecords * 1.0 / @PageSize);

-- Get page data
SELECT 
    ProductId, 
    ProductName, 
    Category, 
    Price,
    @TotalRecords AS TotalRecords,
    @TotalPages AS TotalPages,
    @PageNumber AS CurrentPage
FROM Product
ORDER BY ProductId
OFFSET (@PageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;

-- KEYSET PAGINATION (more efficient for large datasets)
-- Page 1: Initial load
SELECT TOP 10 ProductId, ProductName, Price
FROM Product
ORDER BY ProductId;
-- Remember last ProductId from result (e.g., 10)

-- Page 2: Using last seen ID
DECLARE @LastSeenId INT = 10;
SELECT TOP 10 ProductId, ProductName, Price
FROM Product
WHERE ProductId > @LastSeenId  -- More efficient than OFFSET
ORDER BY ProductId;

-- Pagination with filtering and sorting
DECLARE @PageNumber INT = 1;
DECLARE @PageSize INT = 5;
DECLARE @Category VARCHAR(50) = 'Electronics';
DECLARE @SortBy VARCHAR(20) = 'Price';  -- Price or ProductName

SELECT ProductId, ProductName, Category, Price
FROM Product
WHERE Category = @Category
ORDER BY 
    CASE WHEN @SortBy = 'Price' THEN Price END ASC,
    CASE WHEN @SortBy = 'ProductName' THEN ProductName END ASC
OFFSET (@PageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;

-- Performance comparison
-- ? BAD: Loading all records then taking subset in application
-- SELECT * FROM Product ORDER BY ProductId;
-- (Application takes records 11-20)

-- ? GOOD: Database handles pagination
-- SELECT * FROM Product ORDER BY ProductId OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY;
```

**Pagination Best Practices:**
* ? Always include ORDER BY with OFFSET
* ? Use indexed columns in ORDER BY for performance
* ? Consider keyset pagination for large datasets
* ? Return total count for UI (calculate total pages)
* ? Avoid OFFSET for very large offsets (slow)

---

## 17. SQL Performance Optimization Techniques

### Description

SQL performance optimization involves identifying and resolving bottlenecks to improve query execution speed and reduce resource consumption. Common techniques include proper indexing, query refactoring, avoiding SELECT *, and analyzing execution plans. In interviews, this topic demonstrates practical experience with production databases and ability to troubleshoot performance issues. Performance problems often manifest as slow page loads, timeouts, and high CPU usage. Understanding optimization is crucial for building scalable applications that handle growing data volumes.

### Optimization Techniques

| Technique | Problem It Solves | Example |
|-----------|-------------------|---------|
| **Create Indexes** | Full table scans | Index on WHERE clause columns |
| **Avoid SELECT *** | Unnecessary data transfer | Select only needed columns |
| **Use EXISTS instead of IN** | Subquery performance | EXISTS stops at first match |
| **Avoid Functions on Columns** | Prevents index usage | WHERE Year = 2024 not YEAR(Date) = 2024 |
| **Use JOINs instead of Subqueries** | Better optimization | JOIN often faster than IN/EXISTS |
| **Limit Result Sets** | Reduces data transfer | Use TOP, LIMIT, or pagination |
| **Update Statistics** | Outdated query plans | Keeps optimizer informed |
| **Partition Large Tables** | Reduces scan size | Partition by date or region |

### Real-Time Example

```sql
-- OPTIMIZATION 1: Avoid SELECT *
-- ? BAD: Retrieves all columns (unnecessary data)
SELECT * FROM Employee WHERE Department = 'IT';

-- ? GOOD: Select only needed columns
SELECT EmployeeId, FirstName, LastName, Salary 
FROM Employee 
WHERE Department = 'IT';

-- OPTIMIZATION 2: Use appropriate indexes
-- ? BAD: No index on Department (full table scan)
SELECT EmployeeId, FirstName 
FROM Employee 
WHERE Department = 'IT';
-- Execution: Scans all 1 million rows

-- ? GOOD: Create index on Department
CREATE INDEX IX_Employee_Department ON Employee(Department);
-- Execution: Index seek, scans only IT department rows

-- OPTIMIZATION 3: Avoid functions on indexed columns
-- ? BAD: Function on column prevents index usage
SELECT * FROM Orders 
WHERE YEAR(OrderDate) = 2024;
-- Execution: Full table scan (index not used)

-- ? GOOD: Use range instead
SELECT * FROM Orders 
WHERE OrderDate >= '2024-01-01' AND OrderDate < '2025-01-01';
-- Execution: Index seek on OrderDate

-- OPTIMIZATION 4: EXISTS vs IN for large datasets
-- ? SLOWER: IN with subquery
SELECT CustomerName FROM Customer
WHERE CustomerId IN (
    SELECT CustomerId FROM Order WHERE OrderDate > '2024-01-01'
);

-- ? FASTER: EXISTS (stops at first match)
SELECT CustomerName FROM Customer c
WHERE EXISTS (
    SELECT 1 FROM Order o 
    WHERE o.CustomerId = c.CustomerId AND o.OrderDate > '2024-01-01'
);

-- OPTIMIZATION 5: Use JOINs instead of multiple queries
-- ? BAD: Multiple round trips (N+1 problem)
-- Query 1: SELECT * FROM Customer;
-- Query 2: SELECT * FROM Order WHERE CustomerId = 1;
-- Query 3: SELECT * FROM Order WHERE CustomerId = 2;
-- ... (N queries for N customers)

-- ? GOOD: Single JOIN query
SELECT c.CustomerName, o.OrderId, o.OrderDate, o.Amount
FROM Customer c
LEFT JOIN Order o ON c.CustomerId = o.CustomerId;

-- OPTIMIZATION 6: Use covering indexes
-- Create index that includes all columns needed
CREATE INDEX IX_Employee_Department_Include 
ON Employee(Department) 
INCLUDE (FirstName, LastName, Salary);

SELECT FirstName, LastName, Salary 
FROM Employee 
WHERE Department = 'IT';
-- Execution: All data from index, no table lookup

-- OPTIMIZATION 7: Analyze execution plan
-- Press Ctrl+L or use:
SET SHOWPLAN_ALL ON;
SELECT * FROM Employee WHERE Department = 'IT';
SET SHOWPLAN_ALL OFF;
-- Look for: Table Scan (bad), Index Seek (good), Key Lookup (consider covering index)

-- OPTIMIZATION 8: Update statistics
UPDATE STATISTICS Employee;
-- Helps query optimizer choose better execution plans

-- OPTIMIZATION 9: Use appropriate data types
-- ? BAD: VARCHAR(MAX) for short strings
CREATE TABLE Product (ProductCode VARCHAR(MAX));

-- ? GOOD: Use appropriate size
CREATE TABLE Product (ProductCode VARCHAR(20));

-- OPTIMIZATION 10: Batch operations
-- ? BAD: Multiple single inserts
INSERT INTO Log VALUES (1, 'Event1');
INSERT INTO Log VALUES (2, 'Event2');
INSERT INTO Log VALUES (3, 'Event3');

-- ? GOOD: Batch insert
INSERT INTO Log VALUES 
(1, 'Event1'),
(2, 'Event2'),
(3, 'Event3');
```

**Quick Performance Checklist:**
01. ? Check for missing indexes using execution plans
02. ? Avoid SELECT * in production code
03. ? Use WHERE clause to limit rows early
04. ? Keep transactions short
05. ? Avoid cursors (use set-based operations)
06. ? Monitor slow query logs
07. ? Regular index maintenance (rebuild/reorganize)

---

## 18. What is a Self Join?

### Description

A self join is when a table is joined with itself, treating it as two separate tables using aliases. This is useful for hierarchical data or when comparing rows within the same table. Common use cases include employee-manager relationships, finding pairs or duplicates, and comparing time-series data. In interviews, self joins demonstrate understanding of advanced JOIN concepts and ability to work with hierarchical data structures. Self joins are particularly valuable in organizational charts, bill of materials, and any parent-child relationship within a single table.

### Real-Time Example

```sql
-- Sample Employee table with manager hierarchy
CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY,
    EmployeeName VARCHAR(100),
    JobTitle VARCHAR(50),
    ManagerId INT,
    Salary DECIMAL(10,2)
);

INSERT INTO Employee VALUES
(1, 'Sarah Johnson', 'CEO', NULL, 200000),
(2, 'Michael Brown', 'VP Sales', 1, 150000),
(3, 'Jennifer Davis', 'VP Engineering', 1, 160000),
(4, 'Robert Wilson', 'Sales Manager', 2, 90000),
(5, 'Lisa Anderson', 'Software Engineer', 3, 85000),
(6, 'David Martinez', 'Software Engineer', 3, 87000),
(7, 'Emily Taylor', 'Sales Rep', 4, 60000);

-- SELF JOIN: Find each employee with their manager
SELECT 
    e.EmployeeName AS Employee,
    e.JobTitle,
    m.EmployeeName AS Manager,
    m.JobTitle AS ManagerTitle
FROM Employee e
LEFT JOIN Employee m ON e.ManagerId = m.EmployeeId
ORDER BY e.EmployeeId;

-- Result:
-- Sarah Johnson (CEO) - Manager: NULL
-- Michael Brown (VP Sales) - Manager: Sarah Johnson (CEO)
-- Jennifer Davis (VP Engineering) - Manager: Sarah Johnson (CEO)
-- ...

-- Find employees who earn more than their manager
SELECT 
    e.EmployeeName AS Employee,
    e.Salary AS EmployeeSalary,
    m.EmployeeName AS Manager,
    m.Salary AS ManagerSalary
FROM Employee e
INNER JOIN Employee m ON e.ManagerId = m.EmployeeId
WHERE e.Salary > m.Salary;

-- Find employee hierarchy (employee and their manager's manager)
SELECT 
    e.EmployeeName AS Employee,
    m1.EmployeeName AS DirectManager,
    m2.EmployeeName AS ManagersManager
FROM Employee e
LEFT JOIN Employee m1 ON e.ManagerId = m1.EmployeeId
LEFT JOIN Employee m2 ON m1.ManagerId = m2.EmployeeId
WHERE e.ManagerId IS NOT NULL;

-- PRACTICAL EXAMPLE: Route connections
CREATE TABLE Flight (
    FlightId INT PRIMARY KEY,
    FromCity VARCHAR(50),
    ToCity VARCHAR(50),
    Price DECIMAL(10,2)
);

INSERT INTO Flight VALUES
(1, 'New York', 'London', 500),
(2, 'London', 'Paris', 150),
(3, 'New York', 'Paris', 700),
(4, 'Paris', 'Rome', 200);

-- Find connecting flights (2-leg journeys)
SELECT 
    f1.FromCity AS Origin,
    f1.ToCity AS Stopover,
    f2.ToCity AS Destination,
    f1.Price + f2.Price AS TotalPrice
FROM Flight f1
INNER JOIN Flight f2 ON f1.ToCity = f2.FromCity
WHERE f1.FromCity = 'New York' AND f2.ToCity = 'Rome';

-- Result: New York -> London -> Paris, New York -> Paris -> Rome

-- Find employees in same department
CREATE TABLE Department (
    EmployeeId INT,
    EmployeeName VARCHAR(100),
    DepartmentName VARCHAR(50)
);

SELECT 
    e1.EmployeeName AS Employee1,
    e2.EmployeeName AS Employee2,
    e1.DepartmentName
FROM Department e1
INNER JOIN Department e2 ON e1.DepartmentName = e2.DepartmentName
WHERE e1.EmployeeId < e2.EmployeeId  -- Avoid duplicate pairs
ORDER BY e1.DepartmentName;
```

**Common Self Join Use Cases:**
* ?? Employee-Manager relationships
* ?? Organizational hierarchies
* ?? Route/network connections
* ?? Comparing rows within same table
* ?? Finding related items (recommendations)

---

## 19. RANK() vs DENSE_RANK() vs ROW_NUMBER()

### Description

RANK(), DENSE_RANK(), and ROW_NUMBER() are window functions that assign ranking values to rows based on specified ordering. ROW_NUMBER() assigns unique sequential numbers, RANK() assigns ranks with gaps after ties, and DENSE_RANK() assigns ranks without gaps. In interviews, this tests knowledge of analytical functions and handling duplicate values. These functions are commonly used in reports, leaderboards, and finding top-N records per group. Understanding the differences is crucial for correct ranking logic in business scenarios like sales rankings or exam results.

### Comparison

| Function | Behavior with Ties | Sequence | Use Case |
|----------|-------------------|----------|----------|
| **ROW_NUMBER()** | Always unique (1, 2, 3, 4...) | No gaps | Pagination, unique row identifiers |
| **RANK()** | Same rank for ties, then skips (1, 2, 2, 4...) | Gaps after ties | Olympic-style ranking |
| **DENSE_RANK()** | Same rank for ties, no gaps (1, 2, 2, 3...) | No gaps | Consecutive ranking |

### Real-Time Example

```sql
-- Sample Sales data
CREATE TABLE SalesRep (
    SalesRepId INT,
    SalesRepName VARCHAR(100),
    Region VARCHAR(50),
    TotalSales DECIMAL(10,2)
);

INSERT INTO SalesRep VALUES
(1, 'John', 'North', 50000),
(2, 'Sarah', 'North', 75000),
(3, 'Mike', 'North', 75000),   -- Tie with Sarah
(4, 'Lisa', 'North', 60000),
(5, 'David', 'South', 90000),
(6, 'Emily', 'South', 80000),
(7, 'Robert', 'South', 80000);  -- Tie with Emily

-- Compare all three ranking functions
SELECT 
    SalesRepName,
    Region,
    TotalSales,
    ROW_NUMBER() OVER (PARTITION BY Region ORDER BY TotalSales DESC) AS RowNum,
    RANK() OVER (PARTITION BY Region ORDER BY TotalSales DESC) AS Rank,
    DENSE_RANK() OVER (PARTITION BY Region ORDER BY TotalSales DESC) AS DenseRank
FROM SalesRep
ORDER BY Region, TotalSales DESC;

-- Result for North region:
-- Sarah    - TotalSales: 75000 - RowNum: 1, Rank: 1, DenseRank: 1
-- Mike     - TotalSales: 75000 - RowNum: 2, Rank: 1, DenseRank: 1 (tie)
-- Lisa     - TotalSales: 60000 - RowNum: 3, Rank: 3, DenseRank: 2 (note difference)
-- John     - TotalSales: 50000 - RowNum: 4, Rank: 4, DenseRank: 3

-- PRACTICAL USE CASE 1: Top 3 sales reps per region (handle ties)
WITH RankedSales AS (
    SELECT 
        SalesRepName,
        Region,
        TotalSales,
        DENSE_RANK() OVER (PARTITION BY Region ORDER BY TotalSales DESC) AS Rank
    FROM SalesRep
)
SELECT * FROM RankedSales
WHERE Rank <= 3;

-- PRACTICAL USE CASE 2: Pagination with ROW_NUMBER
WITH PaginatedResults AS (
    SELECT 
        SalesRepName,
        TotalSales,
        ROW_NUMBER() OVER (ORDER BY TotalSales DESC) AS RowNum
    FROM SalesRep
)
SELECT * FROM PaginatedResults
WHERE RowNum BETWEEN 3 AND 5;  -- Page 2 (3 records per page)

-- PRACTICAL USE CASE 3: Find duplicate records with RANK
CREATE TABLE Student (
    StudentId INT,
    StudentName VARCHAR(100),
    Score DECIMAL(5,2)
);

INSERT INTO Student VALUES
(1, 'Alice', 95.5),
(2, 'Bob', 87.0),
(3, 'Charlie', 95.5),  -- Same score as Alice
(4, 'David', 92.0);

-- Find students with same scores
WITH RankedStudents AS (
    SELECT 
        StudentName,
        Score,
        RANK() OVER (ORDER BY Score DESC) AS Rank,
        DENSE_RANK() OVER (ORDER BY Score DESC) AS DenseRank
    FROM Student
)
SELECT * FROM RankedStudents;

-- PRACTICAL USE CASE 4: Salary ranking within department
CREATE TABLE EmployeeSalary (
    EmployeeId INT,
    EmployeeName VARCHAR(100),
    Department VARCHAR(50),
    Salary DECIMAL(10,2)
);

INSERT INTO EmployeeSalary VALUES
(1, 'John', 'IT', 80000),
(2, 'Sarah', 'IT', 95000),
(3, 'Mike', 'IT', 95000),
(4, 'Lisa', 'HR', 70000),
(5, 'David', 'HR', 75000);

-- Find 2nd highest salary in each department
WITH SalaryRank AS (
    SELECT 
        EmployeeName,
        Department,
        Salary,
        DENSE_RANK() OVER (PARTITION BY Department ORDER BY Salary DESC) AS Rank
    FROM EmployeeSalary
)
SELECT * FROM SalaryRank
WHERE Rank = 2;
```

**Decision Guide:**
* **ROW_NUMBER()**: Use for unique identifiers, pagination
* **RANK()**: Use for Olympic-style rankings (1st, 2nd, 2nd, 4th)
* **DENSE_RANK()**: Use for consecutive rankings (1st, 2nd, 2nd, 3rd)

---

## 20. What are Triggers?

### Description

A trigger is a special type of stored procedure that automatically executes when a specific event occurs on a table or view (INSERT, UPDATE, DELETE). Triggers are used for enforcing business rules, auditing changes, maintaining data integrity, and cascading operations. In interviews, triggers demonstrate understanding of database automation and event-driven logic. While powerful, triggers should be used judiciously as they can impact performance and make debugging complex. Modern applications often prefer application-level logic over triggers for better maintainability and testability.

### Real-Time Example

```sql
-- Sample tables for audit trail
CREATE TABLE Product (
    ProductId INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Price DECIMAL(10,2),
    StockQuantity INT,
    LastModified DATETIME
);

CREATE TABLE ProductAudit (
    AuditId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT,
    OldPrice DECIMAL(10,2),
    NewPrice DECIMAL(10,2),
    OldStock INT,
    NewStock INT,
    ModifiedBy VARCHAR(100),
    ModifiedDate DATETIME
);

-- AFTER UPDATE TRIGGER: Audit price changes
CREATE TRIGGER trg_Product_AuditPriceChange
ON Product
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO ProductAudit (ProductId, OldPrice, NewPrice, OldStock, NewStock, ModifiedBy, ModifiedDate)
    SELECT 
        i.ProductId,
        d.Price AS OldPrice,
        i.Price AS NewPrice,
        d.StockQuantity AS OldStock,
        i.StockQuantity AS NewStock,
        SYSTEM_USER AS ModifiedBy,
        GETDATE() AS ModifiedDate
    FROM inserted i
    INNER JOIN deleted d ON i.ProductId = d.ProductId
    WHERE i.Price <> d.Price OR i.StockQuantity <> d.StockQuantity;
END;

-- Test the trigger
INSERT INTO Product VALUES (1, 'Laptop', 999.99, 10, GETDATE());
UPDATE Product SET Price = 899.99, StockQuantity = 8 WHERE ProductId = 1;
SELECT * FROM ProductAudit;  -- Shows audit record

-- INSTEAD OF INSERT TRIGGER: Validation before insert
CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY,
    EmployeeName VARCHAR(100),
    Email VARCHAR(100),
    Salary DECIMAL(10,2)
);

CREATE TRIGGER trg_Employee_ValidateInsert
ON Employee
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate email format
    IF EXISTS (SELECT 1 FROM inserted WHERE Email NOT LIKE '%_@_%._%')
    BEGIN
        RAISERROR('Invalid email format', 16, 1);
        RETURN;
    END
    
    -- Validate salary range
    IF EXISTS (SELECT 1 FROM inserted WHERE Salary < 30000 OR Salary > 500000)
    BEGIN
        RAISERROR('Salary must be between 30,000 and 500,000', 16, 1);
        RETURN;
    END
    
    -- If validations pass, perform insert
    INSERT INTO Employee (EmployeeId, EmployeeName, Email, Salary)
    SELECT EmployeeId, EmployeeName, Email, Salary
    FROM inserted;
END;

-- Test validation
INSERT INTO Employee VALUES (1, 'John Doe', 'invalid-email', 50000);  -- Error: Invalid email
INSERT INTO Employee VALUES (2, 'Jane Smith', 'jane@company.com', 25000);  -- Error: Salary too low
INSERT INTO Employee VALUES (3, 'Mike Wilson', 'mike@company.com', 60000);  -- Success

-- AFTER DELETE TRIGGER: Prevent deletion of active products
CREATE TABLE Order (
    OrderId INT,
    ProductId INT,
    Quantity INT
);

CREATE TRIGGER trg_Product_PreventDelete
ON Product
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1 FROM deleted d
        INNER JOIN Order o ON d.ProductId = o.ProductId
    )
    BEGIN
        RAISERROR('Cannot delete product with existing orders', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;

-- DDL TRIGGER: Track schema changes
CREATE TABLE SchemaChangeLog (
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    EventType VARCHAR(100),
    ObjectName VARCHAR(100),
    ChangedBy VARCHAR(100),
    ChangeDate DATETIME
);

CREATE TRIGGER trg_Database_SchemaChanges
ON DATABASE
FOR CREATE_TABLE, ALTER_TABLE, DROP_TABLE
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @EventData XML = EVENTDATA();
    
    INSERT INTO SchemaChangeLog (EventType, ObjectName, ChangedBy, ChangeDate)
    VALUES (
        @EventData.value('(/EVENT_INSTANCE/EventType)[1]', 'VARCHAR(100)'),
        @EventData.value('(/EVENT_INSTANCE/ObjectName)[1]', 'VARCHAR(100)'),
        @EventData.value('(/EVENT_INSTANCE/LoginName)[1]', 'VARCHAR(100)'),
        GETDATE()
    );
END;

-- View all triggers on a table
SELECT * FROM sys.triggers WHERE parent_id = OBJECT_ID('Product');

-- Disable/Enable trigger
DISABLE TRIGGER trg_Product_AuditPriceChange ON Product;
ENABLE TRIGGER trg_Product_AuditPriceChange ON Product;

-- Drop trigger
DROP TRIGGER trg_Product_AuditPriceChange;
```

**Trigger Types:**
* **AFTER/FOR**: Executes after the triggering operation completes
* **INSTEAD OF**: Replaces the triggering operation
* **DDL**: Responds to schema changes (CREATE, ALTER, DROP)

**Best Practices:**
* ? Keep trigger logic simple and fast
* ? Use for audit trails and critical validations
* ? Avoid complex business logic in triggers
* ? Avoid recursive triggers
* ? Document triggers clearly (they're hidden logic)

---

## 21. What is a View and Materialized View?

### Description

A View is a virtual table based on a SELECT query that doesn't store data itself but presents data from underlying tables. A Materialized View stores the query result physically, improving performance but requiring periodic refresh. In interviews, views demonstrate understanding of data abstraction and security. Views simplify complex queries, hide sensitive columns, and provide a consistent interface even if underlying schema changes. Materialized views are used in reporting and analytics where real-time data isn't required and query performance is critical.

### Real-Time Example

```sql
-- Sample tables
CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Department VARCHAR(50),
    Salary DECIMAL(10,2),
    SSN VARCHAR(11),  -- Sensitive data
    HireDate DATE
);

CREATE TABLE Department (
    DepartmentId INT PRIMARY KEY,
    DepartmentName VARCHAR(50),
    Location VARCHAR(100)
);

INSERT INTO Employee VALUES 
(1, 'John', 'Doe', 'IT', 75000, '123-45-6789', '2020-01-15'),
(2, 'Jane', 'Smith', 'HR', 65000, '987-65-4321', '2019-06-20'),
(3, 'Mike', 'Johnson', 'IT', 80000, '555-55-5555', '2021-03-10');

-- CREATE VIEW: Simple abstraction
CREATE VIEW vw_EmployeeBasicInfo AS
SELECT 
    EmployeeId,
    FirstName + ' ' + LastName AS FullName,
    Department,
    HireDate
FROM Employee;
-- Note: SSN and Salary hidden for security

-- Query the view
SELECT * FROM vw_EmployeeBasicInfo;

-- CREATE VIEW: Complex join logic
CREATE VIEW vw_EmployeeDepartmentDetails AS
SELECT 
    e.EmployeeId,
    e.FirstName + ' ' + e.LastName AS FullName,
    e.Department,
    d.Location,
    DATEDIFF(YEAR, e.HireDate, GETDATE()) AS YearsOfService
FROM Employee e
INNER JOIN Department d ON e.Department = d.DepartmentName;

-- Users query simple view instead of complex JOIN
SELECT * FROM vw_EmployeeDepartmentDetails WHERE YearsOfService > 3;

-- CREATE VIEW: Aggregate data
CREATE VIEW vw_DepartmentSummary AS
SELECT 
    Department,
    COUNT(*) AS EmployeeCount,
    AVG(Salary) AS AverageSalary,
    MAX(Salary) AS MaxSalary,
    MIN(Salary) AS MinSalary
FROM Employee
GROUP BY Department;

SELECT * FROM vw_DepartmentSummary;

-- UPDATABLE VIEW: Can INSERT/UPDATE through view
CREATE VIEW vw_ITEmployees AS
SELECT EmployeeId, FirstName, LastName, Salary
FROM Employee
WHERE Department = 'IT'
WITH CHECK OPTION;  -- Prevents updates that violate WHERE clause

-- Update through view
UPDATE vw_ITEmployees SET Salary = 82000 WHERE EmployeeId = 3;

-- This will fail due to CHECK OPTION
UPDATE vw_ITEmployees SET Department = 'HR' WHERE EmployeeId = 3;  -- Error

-- INDEXED VIEW (Materialized View in SQL Server)
-- Note: Requires schema binding and other restrictions
CREATE VIEW vw_OrderSummary
WITH SCHEMABINDING  -- Required for indexed view
AS
SELECT 
    CustomerId,
    COUNT_BIG(*) AS OrderCount,  -- COUNT_BIG required for indexed view
    SUM(Amount) AS TotalAmount
FROM dbo.Order
GROUP BY CustomerId;

-- Create unique clustered index (materializes the view)
CREATE UNIQUE CLUSTERED INDEX IX_vw_OrderSummary 
ON vw_OrderSummary(CustomerId);

-- Query is now fast (data pre-aggregated and stored)
SELECT * FROM vw_OrderSummary WHERE CustomerId = 5;

-- ALTER VIEW: Modify existing view
ALTER VIEW vw_EmployeeBasicInfo AS
SELECT 
    EmployeeId,
    FirstName + ' ' + LastName AS FullName,
    Department,
    HireDate,
    DATEDIFF(YEAR, HireDate, GETDATE()) AS YearsOfService  -- Added column
FROM Employee;

-- View metadata
SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'vw_EmployeeBasicInfo';

-- View definition
EXEC sp_helptext 'vw_EmployeeBasicInfo';

-- DROP VIEW
DROP VIEW vw_EmployeeBasicInfo;
```

**View Benefits:**
* ?? Security: Hide sensitive columns
* ?? Simplification: Hide complex JOINs
* ?? Abstraction: Insulate applications from schema changes
* ?? Reusability: Centralize common queries

**View vs Materialized View:**

| Aspect | View | Materialized View (Indexed View) |
|--------|------|----------------------------------|
| **Storage** | No data stored (virtual) | Data physically stored |
| **Performance** | Queries underlying tables each time | Fast (pre-computed results) |
| **Freshness** | Always current | Needs refresh/rebuild |
| **Use Case** | Security, simplification | Reporting, analytics |

---

## 22. Correlated vs Non-Correlated Subquery

### Description

A subquery is a query nested inside another query. Non-Correlated Subqueries execute independently and once, while Correlated Subqueries reference the outer query and execute repeatedly for each row. In interviews, this tests understanding of query execution and performance implications. Non-correlated subqueries are generally faster as they execute only once. Correlated subqueries are useful for row-by-row comparisons but can be slow on large datasets. Converting correlated subqueries to JOINs or CTEs often improves performance.

### Comparison

| Aspect | Non-Correlated Subquery | Correlated Subquery |
|--------|------------------------|---------------------|
| **Independence** | Independent of outer query | References outer query |
| **Execution** | Executes once | Executes once per outer row |
| **Performance** | Generally faster | Can be slower on large datasets |
| **Readability** | Simpler to understand | More complex logic |
| **Use Case** | Fixed criteria, simple filtering | Row-by-row comparisons |

### Real-Time Example

```sql
-- Sample tables
CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY,
    EmployeeName VARCHAR(100),
    DepartmentId INT,
    Salary DECIMAL(10,2)
);

CREATE TABLE Department (
    DepartmentId INT PRIMARY KEY,
    DepartmentName VARCHAR(50),
    Budget DECIMAL(12,2)
);

INSERT INTO Department VALUES (1, 'IT', 500000), (2, 'HR', 300000), (3, 'Sales', 450000);
INSERT INTO Employee VALUES 
(1, 'John', 1, 75000),
(2, 'Sarah', 1, 95000),
(3, 'Mike', 2, 65000),
(4, 'Lisa', 2, 70000),
(5, 'David', 1, 85000),
(6, 'Emily', 3, 90000);

-- NON-CORRELATED SUBQUERY: Find employees in IT department
SELECT EmployeeName, Salary
FROM Employee
WHERE DepartmentId = (
    SELECT DepartmentId 
    FROM Department 
    WHERE DepartmentName = 'IT'
);
-- Subquery executes once, returns DepartmentId = 1

-- NON-CORRELATED SUBQUERY: Find employees earning above average
SELECT EmployeeName, Salary
FROM Employee
WHERE Salary > (SELECT AVG(Salary) FROM Employee);
-- Subquery executes once, calculates average

-- CORRELATED SUBQUERY: Find employees earning above department average
SELECT e1.EmployeeName, e1.Salary, e1.DepartmentId
FROM Employee e1
WHERE e1.Salary > (
    SELECT AVG(e2.Salary)
    FROM Employee e2
    WHERE e2.DepartmentId = e1.DepartmentId  -- References outer query
);
-- Subquery executes for each row in e1

-- CORRELATED SUBQUERY: Find departments with more than 2 employees
SELECT d.DepartmentName
FROM Department d
WHERE (
    SELECT COUNT(*)
    FROM Employee e
    WHERE e.DepartmentId = d.DepartmentId  -- References outer query
) > 2;

-- ALTERNATIVE: Same query using JOIN (better performance)
SELECT d.DepartmentName
FROM Department d
INNER JOIN (
    SELECT DepartmentId, COUNT(*) AS EmpCount
    FROM Employee
    GROUP BY DepartmentId
) AS emp_count ON d.DepartmentId = emp_count.DepartmentId
WHERE emp_count.EmpCount > 2;

-- CORRELATED EXISTS: Check if employee has orders
CREATE TABLE EmployeeSales (
    SalesId INT,
    EmployeeId INT,
    Amount DECIMAL(10,2)
);

INSERT INTO EmployeeSales VALUES (1, 1, 5000), (2, 1, 7000), (3, 3, 3000);

SELECT e.EmployeeName
FROM Employee e
WHERE EXISTS (
    SELECT 1
    FROM EmployeeSales s
    WHERE s.EmployeeId = e.EmployeeId  -- Correlated
);
-- Returns employees who have made sales

-- NON-CORRELATED IN: Find employees in specific departments
SELECT EmployeeName
FROM Employee
WHERE DepartmentId IN (
    SELECT DepartmentId
    FROM Department
    WHERE Budget > 400000
);
-- Subquery executes once, returns list of department IDs

-- CORRELATED UPDATE: Give 10% raise to employees earning below department average
UPDATE e1
SET Salary = Salary * 1.10
FROM Employee e1
WHERE e1.Salary < (
    SELECT AVG(e2.Salary)
    FROM Employee e2
    WHERE e2.DepartmentId = e1.DepartmentId
);

-- PRACTICAL: Find employees with highest salary in their department
SELECT e1.EmployeeName, e1.Salary, e1.DepartmentId
FROM Employee e1
WHERE e1.Salary = (
    SELECT MAX(e2.Salary)
    FROM Employee e2
    WHERE e2.DepartmentId = e1.DepartmentId  -- Correlated
);

-- BETTER ALTERNATIVE: Using window function
SELECT EmployeeName, Salary, DepartmentId
FROM (
    SELECT 
        EmployeeName, 
        Salary, 
        DepartmentId,
        RANK() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS SalaryRank
    FROM Employee
) AS Ranked
WHERE SalaryRank = 1;
```

**Performance Tips:**
* ? Use JOINs or window functions instead of correlated subqueries when possible
* ? Use EXISTS instead of COUNT(*) in correlated subqueries
* ? Non-correlated subqueries with IN are efficient for small result sets
* ? Avoid correlated subqueries in SELECT list (runs for every row)

---

## 23. What is the N+1 Query Problem?

### Description

The N+1 query problem occurs when an application makes one query to fetch N records, then makes N additional queries to fetch related data for each record. This results in N+1 total queries instead of efficiently fetching all data with JOINs. In interviews, this demonstrates understanding of ORM behavior and database performance. The problem commonly occurs with ORMs like Entity Framework or Hibernate when lazy loading is enabled. The solution is using eager loading (JOINs) or batch loading to reduce round trips to the database.

### Real-Time Example

```sql
-- Sample schema
CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY,
    CustomerName VARCHAR(100)
);

CREATE TABLE Order (
    OrderId INT PRIMARY KEY,
    CustomerId INT,
    OrderDate DATE,
    TotalAmount DECIMAL(10,2)
);

INSERT INTO Customer VALUES (1, 'Alice'), (2, 'Bob'), (3, 'Charlie');
INSERT INTO Order VALUES 
(101, 1, '2024-01-15', 500),
(102, 1, '2024-01-20', 300),
(103, 2, '2024-01-18', 700),
(104, 3, '2024-01-22', 450);

-- ? N+1 PROBLEM: Application code (pseudo-code)
-- Query 1: Get all customers
-- SELECT * FROM Customer;  -- Returns 3 customers

-- Query 2-4: Get orders for each customer (lazy loading)
-- SELECT * FROM Order WHERE CustomerId = 1;  -- For Alice
-- SELECT * FROM Order WHERE CustomerId = 2;  -- For Bob
-- SELECT * FROM Order WHERE CustomerId = 3;  -- For Charlie

-- Result: 1 + 3 = 4 queries total (N+1 problem)

-- ? SOLUTION 1: Single JOIN query (eager loading)
SELECT 
    c.CustomerId,
    c.CustomerName,
    o.OrderId,
    o.OrderDate,
    o.TotalAmount
FROM Customer c
LEFT JOIN Order o ON c.CustomerId = o.CustomerId
ORDER BY c.CustomerId;
-- Result: 1 query total, all data fetched at once

-- ? SOLUTION 2: Batch loading (if JOINs are complex)
-- Query 1: Get all customers
SELECT * FROM Customer;

-- Query 2: Get all orders for these customers (single query)
SELECT * FROM Order WHERE CustomerId IN (1, 2, 3);

-- Result: Only 2 queries total instead of N+1

-- PRACTICAL EXAMPLE: Blog posts and comments
CREATE TABLE BlogPost (
    PostId INT PRIMARY KEY,
    Title VARCHAR(200),
    Content TEXT
);

CREATE TABLE Comment (
    CommentId INT PRIMARY KEY,
    PostId INT,
    CommentText VARCHAR(500),
    CommentDate DATETIME
);

INSERT INTO BlogPost VALUES (1, 'SQL Tips', 'Content 1'), (2, 'Database Design', 'Content 2');
INSERT INTO Comment VALUES 
(1, 1, 'Great post!', '2024-01-15'),
(2, 1, 'Very helpful', '2024-01-16'),
(3, 2, 'Thanks for sharing', '2024-01-17');

-- ? N+1 PROBLEM
-- Query 1: SELECT * FROM BlogPost; (2 posts)
-- Query 2: SELECT * FROM Comment WHERE PostId = 1; (for first post)
-- Query 3: SELECT * FROM Comment WHERE PostId = 2; (for second post)
-- Total: 3 queries

-- ? SOLUTION: Single query with JOIN
SELECT 
    p.PostId,
    p.Title,
    c.CommentId,
    c.CommentText,
    c.CommentDate
FROM BlogPost p
LEFT JOIN Comment c ON p.PostId = c.PostId
ORDER BY p.PostId, c.CommentDate;
-- Total: 1 query

-- ENTITY FRAMEWORK EXAMPLE (C# pseudo-code)
-- ? N+1 Problem (Lazy Loading enabled)
-- var customers = context.Customers.ToList();  // Query 1
-- foreach (var customer in customers)
-- {
--     var orders = customer.Orders.ToList();  // Query per customer (N queries)
-- }

-- ? Solution: Eager Loading
-- var customers = context.Customers
--     .Include(c => c.Orders)  // JOIN in single query
--     .ToList();

-- Monitoring N+1 problems
-- Enable query logging in application
-- Use database profiler (SQL Server Profiler, pg_stat_statements)
-- Look for repeated similar queries with different parameters
```

**How to Identify N+1:**
01. ?? High number of database queries for simple operations
02. ?? Repeated similar queries with only ID parameter changing
03. ?? Slow page load despite simple data display
04. ?? Database connection pool exhaustion

**Prevention:**
* ? Use eager loading (JOINs) instead of lazy loading
* ? Use batch/bulk loading for related data
* ? Monitor and profile database queries
* ? Use ORM query analysis tools
* ? Implement caching for frequently accessed data

---

## 24. How to Handle NULL Values in SQL?

### Description

NULL represents missing or unknown data in SQL. Handling NULLs correctly is crucial as they behave differently from regular values (NULL != NULL, arithmetic with NULL returns NULL). In interviews, NULL handling tests understanding of three-valued logic and data quality. Common scenarios include default values, NULL checks in WHERE clauses, and NULL-safe aggregations. Functions like COALESCE, ISNULL, and NULLIF help manage NULL values. Proper NULL handling prevents bugs and ensures accurate query results.

### Real-Time Example

```sql
-- Sample Employee table with NULL values
CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY,
    EmployeeName VARCHAR(100),
    Department VARCHAR(50),
    Salary DECIMAL(10,2),
    ManagerId INT,
    Email VARCHAR(100),
    BonusPercentage DECIMAL(5,2)
);

INSERT INTO Employee VALUES 
(1, 'John Doe', 'IT', 75000, NULL, 'john@company.com', 10),
(2, 'Jane Smith', NULL, 65000, 1, NULL, NULL),  -- Department and Email NULL
(3, 'Mike Johnson', 'IT', NULL, 1, 'mike@company.com', 5),  -- Salary NULL
(4, 'Lisa Anderson', 'HR', 70000, NULL, 'lisa@company.com', NULL);

-- CHECKING FOR NULL
SELECT * FROM Employee WHERE Department IS NULL;  -- Returns Jane Smith
SELECT * FROM Employee WHERE Email IS NOT NULL;   -- Excludes Jane Smith

-- ? WRONG: Using = or != with NULL
SELECT * FROM Employee WHERE Department = NULL;   -- Returns 0 rows (wrong)
SELECT * FROM Employee WHERE Department != NULL;  -- Returns 0 rows (wrong)

-- ? CORRECT: Use IS NULL or IS NOT NULL
SELECT * FROM Employee WHERE Department IS NULL;

-- COALESCE: Return first non-NULL value
SELECT 
    EmployeeName,
    COALESCE(Department, 'Unassigned') AS Department,
    COALESCE(Email, 'No Email') AS Email
FROM Employee;
-- Jane Smith will show "Unassigned" for Department

-- ISNULL (SQL Server specific): Replace NULL with default
SELECT 
    EmployeeName,
    ISNULL(Salary, 50000) AS Salary,  -- Default salary if NULL
    ISNULL(BonusPercentage, 0) AS BonusPercentage
FROM Employee;

-- NULLIF: Return NULL if two values are equal
SELECT 
    EmployeeName,
    NULLIF(Department, 'IT') AS Department  -- Returns NULL for IT department
FROM Employee;
-- Useful for converting specific values to NULL

-- NULL in arithmetic operations (returns NULL)
SELECT 
    EmployeeName,
    Salary,
    BonusPercentage,
    Salary * (BonusPercentage / 100) AS BonusAmount  -- NULL if Salary or Bonus is NULL
FROM Employee;

-- ? Safe arithmetic with NULL handling
SELECT 
    EmployeeName,
    COALESCE(Salary, 0) AS Salary,
    COALESCE(BonusPercentage, 0) AS BonusPercentage,
    COALESCE(Salary, 0) * (COALESCE(BonusPercentage, 0) / 100) AS BonusAmount
FROM Employee;

-- NULL in comparisons (returns UNKNOWN, treated as FALSE)
SELECT * FROM Employee WHERE Salary > 60000;  
-- Mike Johnson (NULL salary) NOT included

SELECT * FROM Employee WHERE Salary > 60000 OR Salary IS NULL;
-- Now includes Mike Johnson

-- NULL in aggregations (ignored by COUNT, SUM, AVG)
SELECT 
    COUNT(*) AS TotalEmployees,           -- 4 (counts all rows)
    COUNT(Email) AS EmployeesWithEmail,   -- 3 (ignores NULLs)
    AVG(Salary) AS AverageSalary,         -- Calculates average of non-NULL salaries
    SUM(BonusPercentage) AS TotalBonus    -- Sums only non-NULL values
FROM Employee;

-- NULL in GROUP BY (NULL values grouped together)
SELECT 
    COALESCE(Department, 'Unknown') AS Department,
    COUNT(*) AS EmployeeCount
FROM Employee
GROUP BY Department;

-- NULL in ORDER BY (NULL values first or last depending on database)
SELECT EmployeeName, Salary
FROM Employee
ORDER BY Salary ASC;  -- In SQL Server, NULLs appear first

-- Force NULLs last
SELECT EmployeeName, Salary
FROM Employee
ORDER BY CASE WHEN Salary IS NULL THEN 1 ELSE 0 END, Salary;

-- CASE statement with NULL handling
SELECT 
    EmployeeName,
    CASE 
        WHEN Salary IS NULL THEN 'Salary Not Set'
        WHEN Salary < 60000 THEN 'Entry Level'
        WHEN Salary BETWEEN 60000 AND 80000 THEN 'Mid Level'
        ELSE 'Senior Level'
    END AS SalaryBand
FROM Employee;

-- NULL in JOINs (unmatched rows result in NULL)
CREATE TABLE Project (
    ProjectId INT,
    ProjectName VARCHAR(100),
    ManagerId INT
);

INSERT INTO Project VALUES (1, 'Project A', 1), (2, 'Project B', 99);  -- ManagerId 99 doesn't exist

SELECT 
    p.ProjectName,
    COALESCE(e.EmployeeName, 'No Manager Assigned') AS ManagerName
FROM Project p
LEFT JOIN Employee e ON p.ManagerId = e.EmployeeId;

-- Update NULL values
UPDATE Employee 
SET Department = 'General' 
WHERE Department IS NULL;

-- Insert with NULL handling
INSERT INTO Employee (EmployeeId, EmployeeName, Department, Salary)
VALUES (5, 'Tom Brown', NULLIF('', ''), 55000);  -- Empty string becomes NULL
```

**NULL Best Practices:**
* ? Use IS NULL / IS NOT NULL for checks (not = NULL)
* ? Use COALESCE for default values
* ? Be aware of NULL behavior in aggregations
* ? Consider NOT NULL constraints where appropriate
* ? Document columns that allow NULL
* ? Avoid nullable foreign keys when possible
* ? Don't use NULL for empty strings ('')

**Common NULL Functions:**

| Function | Purpose | Example |
|----------|---------|---------|
| **IS NULL** | Check if value is NULL | WHERE column IS NULL |
| **COALESCE** | First non-NULL value | COALESCE(col1, col2, 'default') |
| **ISNULL** | Replace NULL with value | ISNULL(column, 0) |
| **NULLIF** | Return NULL if equal | NULLIF(column, 0) |

---

## 25. What are Execution Plans?

### Description

An execution plan is a roadmap showing how SQL Server will execute a query, including which indexes to use, join methods, and estimated costs. Analyzing execution plans is essential for performance tuning and identifying bottlenecks. In interviews, this demonstrates advanced database optimization skills. Execution plans reveal table scans (slow), index seeks (fast), missing indexes, and expensive operations. Both estimated (before execution) and actual (after execution) plans provide insights. Understanding execution plans separates junior developers from senior performance-focused engineers.

### Real-Time Example

```sql
-- Sample tables for demonstration
CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY,
    CustomerName VARCHAR(100),
    City VARCHAR(50),
    Country VARCHAR(50)
);

CREATE TABLE Order (
    OrderId INT PRIMARY KEY,
    CustomerId INT,
    OrderDate DATE,
    TotalAmount DECIMAL(10,2)
);

-- Insert sample data
INSERT INTO Customer VALUES (1, 'Alice', 'New York', 'USA'), (2, 'Bob', 'London', 'UK');
INSERT INTO Order VALUES (101, 1, '2024-01-15', 500), (102, 1, '2024-01-20', 300);

-- VIEWING EXECUTION PLAN
-- Method 1: Graphical (SQL Server Management Studio)
-- Press Ctrl+L or click "Display Estimated Execution Plan"
-- Press Ctrl+M to include actual execution plan with results

-- Method 2: Text-based execution plan
SET SHOWPLAN_TEXT ON;
SELECT * FROM Customer WHERE CustomerId = 1;
SET SHOWPLAN_TEXT OFF;

-- Method 3: XML execution plan
SET SHOWPLAN_XML ON;
SELECT * FROM Customer WHERE CustomerId = 1;
SET SHOWPLAN_XML OFF;

-- READING EXECUTION PLAN ICONS
-- ? Index Seek: Fast, uses index to find specific rows (good)
-- ?? Index Scan: Reads entire index (acceptable for small tables)
-- ? Table Scan: Reads entire table (slow for large tables, needs index)
-- ?? Nested Loop Join: Good for small datasets
-- ?? Hash Match Join: Good for large datasets
-- ?? Merge Join: Best for sorted data

-- EXAMPLE 1: Table Scan (BAD - no index)
SELECT * FROM Customer WHERE City = 'New York';
-- Execution Plan: Table Scan (reads all rows)
-- Cost: High for large tables

-- Add index to improve performance
CREATE INDEX IX_Customer_City ON Customer(City);

-- Now query uses Index Seek
SELECT * FROM Customer WHERE City = 'New York';
-- Execution Plan: Index Seek (fast)
-- Cost: Low

-- EXAMPLE 2: Implicit conversion (BAD - prevents index usage)
-- CustomerId is INT, but we pass VARCHAR
SELECT * FROM Customer WHERE CustomerId = '1';  -- String instead of int
-- Execution Plan: Table Scan (index not used due to conversion)

-- ? Fix: Use correct data type
SELECT * FROM Customer WHERE CustomerId = 1;
-- Execution Plan: Index Seek (uses primary key index)

-- EXAMPLE 3: Function on column (BAD - prevents index usage)
SELECT * FROM Order WHERE YEAR(OrderDate) = 2024;
-- Execution Plan: Table Scan (function prevents index usage)

-- ? Fix: Use range instead of function
SELECT * FROM Order WHERE OrderDate >= '2024-01-01' AND OrderDate < '2025-01-01';
-- Execution Plan: Index Seek (if index exists on OrderDate)

-- EXAMPLE 4: SELECT * vs specific columns
-- ? BAD: SELECT * retrieves unnecessary data
SELECT * FROM Order WHERE OrderId = 101;

-- ? GOOD: Select only needed columns
SELECT OrderId, TotalAmount FROM Order WHERE OrderId = 101;
-- May use covering index if available

-- EXAMPLE 5: Missing index warning
SELECT c.CustomerName, o.OrderId, o.TotalAmount
FROM Customer c
INNER JOIN Order o ON c.CustomerId = o.CustomerId
WHERE c.Country = 'USA';
-- Execution plan may show "Missing Index" suggestion

-- ANALYZE EXECUTION PLAN METRICS
-- Look for:
-- 1. High Cost Operations (>50% of total cost)
-- 2. Table Scans on large tables
-- 3. Key Lookups (consider covering index)
-- 4. Sort operations (expensive, consider index)
-- 5. Estimated vs Actual rows (outdated statistics if different)

-- STATISTICS: Help optimizer create better plans
UPDATE STATISTICS Customer;
UPDATE STATISTICS Order;

-- View statistics
DBCC SHOW_STATISTICS('Customer', 'IX_Customer_City');

-- SET STATISTICS IO: Show page reads
SET STATISTICS IO ON;
SELECT * FROM Customer WHERE City = 'New York';
SET STATISTICS IO OFF;
-- Output: Logical reads: 2 (lower is better)

-- SET STATISTICS TIME: Show execution time
SET STATISTICS TIME ON;
SELECT * FROM Customer WHERE City = 'New York';
SET STATISTICS TIME OFF;
-- Output: CPU time and elapsed time

-- QUERY HINTS: Force specific behavior (use cautiously)
-- Force index usage
SELECT * FROM Customer WITH (INDEX(IX_Customer_City))
WHERE City = 'New York';

-- Force table scan (testing)
SELECT * FROM Customer WITH (INDEX(0))
WHERE CustomerId = 1;

-- PRACTICAL: Find expensive queries
SELECT TOP 10
    qs.execution_count,
    qs.total_worker_time / qs.execution_count AS avg_cpu_time,
    qs.total_elapsed_time / qs.execution_count AS avg_elapsed_time,
    SUBSTRING(qt.text, (qs.statement_start_offset/2)+1,
        ((CASE qs.statement_end_offset
            WHEN -1 THEN DATALENGTH(qt.text)
            ELSE qs.statement_end_offset
        END - qs.statement_start_offset)/2) + 1) AS query_text
FROM sys.dm_exec_query_stats qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) qt
ORDER BY avg_cpu_time DESC;
```

**Execution Plan Optimization Checklist:**
01. ? Look for Table Scans ? Add indexes
02. ? Check for Missing Index warnings ? Consider creating them
03. ? Identify Key Lookups ? Create covering indexes
04. ? Review Sort operations ? Add index on ORDER BY columns
05. ? Compare Estimated vs Actual rows ? Update statistics
06. ? Avoid functions on indexed columns
07. ? Ensure correct data types (avoid implicit conversion)
08. ? Use specific columns instead of SELECT *

**Common Execution Plan Operators:**

| Operator | Meaning | Performance |
|----------|---------|-------------|
| **Index Seek** | Uses index to find specific rows | ? Excellent |
| **Index Scan** | Reads entire index | ?? OK for small tables |
| **Table Scan** | Reads entire table | ? Slow for large tables |
| **Key Lookup** | Additional lookup after index seek | ?? Consider covering index |
| **Nested Loop Join** | Inner loop for each outer row | ? Good for small datasets |
| **Hash Match Join** | Builds hash table | ? Good for large datasets |
| **Sort** | Sorts data | ?? Expensive, consider index |

---

## 26. Scenario-Based Interview Questions

This section contains 25 real-world scenario-based questions that test your practical SQL knowledge and problem-solving skills. These scenarios simulate actual challenges you might face in production environments.

### Scenario 1: Nested Stored Procedures with Temp Tables

**Description:**
This scenario tests your understanding of variable and temp table scope in nested stored procedures. Temp tables are session-scoped and accessible across nested procedures, while temp variables are local-scoped and only accessible within the procedure where they're declared. The solution involves either converting temp variables to temp tables or passing values as parameters.

**Question:** You have three stored procedures: `sp_ProcessOrders` , `sp_CalculateTax` , and `sp_GenerateInvoice` . They are called in sequence (nested). `sp_ProcessOrders` creates a temp table `#OrderDetails` and a temp variable `@OrderTotal` . `sp_CalculateTax` is called from `sp_ProcessOrders` and needs to access `#OrderDetails` . `sp_GenerateInvoice` is called from `sp_CalculateTax` and needs both `#OrderDetails` and `@OrderTotal` . Will this work? If not, how would you fix it?

**Answer:**
Temp tables are session-scoped and accessible across nested procedures, while temp variables are local-scoped and only accessible within the procedure where they're declared. The solution involves either converting temp variables to temp tables or passing values as OUTPUT parameters.

```sql
-- Temp tables are accessible in nested procedures (session-scoped)
-- Temp variables are NOT accessible in nested procedures (local scope only)

-- SOLUTION: Use temp tables for both, or pass as parameters

-- Option 1: Convert temp variable to temp table
CREATE PROCEDURE sp_ProcessOrders
AS
BEGIN
    CREATE TABLE #OrderDetails (OrderId INT, ProductId INT, Quantity INT);
    CREATE TABLE #OrderTotal (TotalAmount DECIMAL(10,2));  -- Instead of @OrderTotal variable
    INSERT INTO #OrderTotal VALUES (0);
    
    -- Populate #OrderDetails...
    
    EXEC sp_CalculateTax;  -- Can access #OrderDetails and #OrderTotal
END;

CREATE PROCEDURE sp_CalculateTax
AS
BEGIN
    -- Can access #OrderDetails (temp table)
    -- Can access #OrderTotal (temp table)
    
    UPDATE #OrderTotal SET TotalAmount = TotalAmount * 1.08;  -- Add tax
    
    EXEC sp_GenerateInvoice;  -- Can access both temp tables
END;

CREATE PROCEDURE sp_GenerateInvoice
AS
BEGIN
    -- Can access #OrderDetails and #OrderTotal (both are temp tables)
    SELECT * FROM #OrderDetails;
    SELECT * FROM #OrderTotal;
END;

-- Option 2: Pass values as OUTPUT parameters
CREATE PROCEDURE sp_ProcessOrders
AS
BEGIN
    DECLARE @OrderTotal DECIMAL(10,2) = 1000;
    CREATE TABLE #OrderDetails (OrderId INT, ProductId INT, Quantity INT);
    
    EXEC sp_CalculateTax @OrderTotal = @OrderTotal OUTPUT;
    EXEC sp_GenerateInvoice @OrderTotal = @OrderTotal;
END;
```

---

### Scenario 2: Deadlock Between Two Concurrent Transactions

**Question:** Your application has two concurrent transactions. Transaction A updates the `Inventory` table first, then the `Order` table. Transaction B updates the `Order` table first, then the `Inventory` table. Users are experiencing deadlock errors. How would you resolve this?

**Answer:**
Deadlocks occur when two transactions wait for each other's locks by accessing resources in different orders. The solution is to ensure all transactions access tables in the same order (Inventory → Order), implement retry logic, or use appropriate isolation levels.

```sql
-- PROBLEM: Different lock order causes deadlock
-- Transaction A: Inventory -> Order
-- Transaction B: Order -> Inventory

-- SOLUTION: Always access tables in same order
-- Both transactions should access: Inventory -> Order

-- Transaction A (already correct)
BEGIN TRANSACTION;
UPDATE Inventory SET Quantity = Quantity - 1 WHERE ProductId = 1;
UPDATE Order SET Status = 'Completed' WHERE OrderId = 101;
COMMIT;

-- Transaction B (FIXED: Access Inventory first)
BEGIN TRANSACTION;
UPDATE Inventory SET Quantity = Quantity - 1 WHERE ProductId = 2;  -- Changed order
UPDATE Order SET Status = 'Completed' WHERE OrderId = 102;
COMMIT;

-- ALTERNATIVE: Use application-level retry logic
-- Catch deadlock exception and retry transaction
```

---

### Scenario 3: Query Performance Degradation Over Time

**Question:** A query that used to run in 2 seconds now takes 30 seconds. The table has grown from 10, 000 to 10 million rows. The query uses `WHERE CreatedDate >= DATEADD(DAY, -30, GETDATE())` . What could be the issue and how would you fix it?

**Answer:**
Performance degradation with table growth typically results from missing indexes, outdated statistics, or table scans. Solutions include creating indexes on the filtered column, updating statistics, implementing table partitioning for very large tables, or using filtered indexes for frequently queried date ranges.

```sql
-- PROBLEM: Missing index on CreatedDate, or outdated statistics

-- Check current execution plan
SELECT * FROM Orders 
WHERE CreatedDate >= DATEADD(DAY, -30, GETDATE());

-- SOLUTION 1: Create index on CreatedDate
CREATE INDEX IX_Orders_CreatedDate ON Orders(CreatedDate);

-- SOLUTION 2: Update statistics
UPDATE STATISTICS Orders;

-- SOLUTION 3: Partition table by date (for very large tables)
-- Create partition function
CREATE PARTITION FUNCTION pf_Orders_Date (DATE)
AS RANGE RIGHT FOR VALUES ('2024-01-01', '2024-07-01', '2025-01-01');

-- SOLUTION 4: Use filtered index for recent data
CREATE INDEX IX_Orders_RecentCreatedDate 
ON Orders(CreatedDate) 
WHERE CreatedDate >= DATEADD(DAY, -90, GETDATE());
```

---

### Scenario 4: Duplicate Records After Data Migration

**Question:** After migrating data from an old system, you notice duplicate customer records. Some customers have 2-3 duplicate entries with slightly different data (e.g., "John Doe" vs "John  Doe" with extra space, or different email formats). How would you identify and clean these duplicates?

**Answer:**
Identify duplicates using fuzzy matching with string functions (LTRIM, RTRIM, LOWER), rank records by data completeness, merge duplicates keeping the most complete record, and update foreign key references in related tables before deleting duplicates.

```sql
-- Step 1: Identify duplicates using fuzzy matching
WITH DuplicateCandidates AS (
    SELECT 
        CustomerId,
        CustomerName,
        Email,
        LTRIM(RTRIM(CustomerName)) AS CleanName,
        LOWER(LTRIM(RTRIM(Email))) AS CleanEmail,
        ROW_NUMBER() OVER (
            PARTITION BY LTRIM(RTRIM(CustomerName)), LOWER(LTRIM(RTRIM(Email))) 
            ORDER BY CustomerId
        ) AS RowNum
    FROM Customer
)
SELECT * FROM DuplicateCandidates WHERE RowNum > 1;

-- Step 2: Merge duplicates (keep most complete record)
WITH RankedCustomers AS (
    SELECT *,
        ROW_NUMBER() OVER (
            PARTITION BY LTRIM(RTRIM(CustomerName)), LOWER(LTRIM(RTRIM(Email)))
            ORDER BY 
                CASE WHEN Email IS NOT NULL THEN 0 ELSE 1 END,  -- Prefer records with email
                CASE WHEN Phone IS NOT NULL THEN 0 ELSE 1 END,   -- Prefer records with phone
                CustomerId  -- Keep oldest record
        ) AS RowNum
    FROM Customer
)
DELETE FROM RankedCustomers WHERE RowNum > 1;

-- Step 3: Update foreign keys in related tables
-- Before deleting, update Order.CustomerId to point to kept customer
UPDATE o
SET o.CustomerId = dc.KeptCustomerId
FROM Order o
INNER JOIN (
    SELECT 
        CustomerId AS DuplicateId,
        MIN(CustomerId) OVER (
            PARTITION BY LTRIM(RTRIM(CustomerName)), LOWER(LTRIM(RTRIM(Email)))
        ) AS KeptCustomerId
    FROM Customer
) dc ON o.CustomerId = dc.DuplicateId
WHERE dc.DuplicateId <> dc.KeptCustomerId;
```

---

### Scenario 5: Missing Data After Transaction Rollback

**Question:** A stored procedure inserts data into three tables ( `Orders` , `OrderItems` , `Payment` ) within a transaction. If the payment fails, the transaction rolls back. However, you need to log the failed attempt in an `AuditLog` table. How would you handle this?

**Answer:**
When transactions rollback, all changes including audit logs are lost. The solution requires logging failed attempts outside the transaction scope using autonomous transactions (separate BEGIN TRANSACTION for logging), TRY-CATCH blocks, or table variables that persist beyond rollback.

```sql
-- SOLUTION: Use separate transaction for audit, or TRY-CATCH with nested transactions

CREATE PROCEDURE sp_ProcessOrder
    @OrderId INT,
    @CustomerId INT,
    @Amount DECIMAL(10,2)
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Insert order
        INSERT INTO Orders (OrderId, CustomerId, OrderDate, TotalAmount)
        VALUES (@OrderId, @CustomerId, GETDATE(), @Amount);
        
        -- Insert order items
        INSERT INTO OrderItems (OrderId, ProductId, Quantity)
        SELECT @OrderId, ProductId, Quantity FROM #TempOrderItems;
        
        -- Process payment (might fail)
        EXEC sp_ProcessPayment @OrderId, @Amount;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Log error before rollback
        INSERT INTO AuditLog (EventType, EventData, ErrorMessage, LogDate)
        VALUES (
            'OrderProcessingFailed',
            CONCAT('OrderId: ', @OrderId, ', CustomerId: ', @CustomerId),
            ERROR_MESSAGE(),
            GETDATE()
        );
        
        ROLLBACK TRANSACTION;
        THROW;  -- Re-throw error
    END CATCH
END;

-- ALTERNATIVE: Use table variable for audit (survives rollback in some cases)
-- Or use separate connection/transaction for audit logging
```

---

### Scenario 6: Performance Issue with LIKE Query

**Question:** A search feature allows users to search products by name. The query `SELECT * FROM Products WHERE ProductName LIKE '%laptop%'` is very slow on a table with 5 million products. How would you optimize this?

**Answer:**
Leading wildcards ('%laptop%') prevent index usage causing table scans. Solutions include implementing full-text search for text queries, using computed columns with reverse strings for suffix searches, considering search engines like Elasticsearch, or restructuring queries to use prefix searches that can leverage indexes.

```sql
-- PROBLEM: Leading wildcard prevents index usage

-- SOLUTION 1: Full-Text Search (best for text search)
CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;
CREATE FULLTEXT INDEX ON Products(ProductName) KEY INDEX PK_Products;

-- Query using full-text search
SELECT * FROM Products
WHERE CONTAINS(ProductName, 'laptop');

-- SOLUTION 2: Use computed column with reverse string for suffix search
ALTER TABLE Products
ADD ProductNameReversed AS REVERSE(ProductName);

CREATE INDEX IX_Products_NameReversed ON Products(ProductNameReversed);

-- For suffix search: WHERE ProductName LIKE '%laptop'
-- Use: WHERE ProductNameReversed LIKE 'potpal%'

-- SOLUTION 3: Use n-gram approach (store trigrams)
-- Create table ProductNGrams (ProductId, NGram)
-- Index on NGram for fast lookup

-- SOLUTION 4: If exact prefix match is acceptable
-- WHERE ProductName LIKE 'laptop%'  -- This can use index
CREATE INDEX IX_Products_Name ON Products(ProductName);

-- SOLUTION 5: Use search engine (Elasticsearch, Azure Cognitive Search)
-- For production applications with complex search requirements
```

---

### Scenario 7: Incorrect Results from JOIN Query

**Question:** You're joining `Orders` and `OrderItems` tables. The query returns fewer rows than expected. Some orders appear to be missing. What could be wrong and how would you debug it?

**Answer:**
Using INNER JOIN excludes orders without items, causing missing results. Debug by checking for NULL foreign keys, using LEFT JOIN to include all parent records regardless of child records, verifying data integrity, checking JOIN conditions for typos, and confirming data type matches between joined columns.

```sql
-- PROBLEM: Using INNER JOIN excludes orders without items

-- Debug: Check what's being excluded
SELECT 
    o.OrderId,
    CASE WHEN oi.OrderId IS NULL THEN 'No Items' ELSE 'Has Items' END AS Status
FROM Orders o
LEFT JOIN OrderItems oi ON o.OrderId = oi.OrderId
WHERE oi.OrderId IS NULL;  -- Find orders without items

-- SOLUTION 1: Use LEFT JOIN if you need all orders
SELECT o.OrderId, o.OrderDate, oi.ProductId, oi.Quantity
FROM Orders o
LEFT JOIN OrderItems oi ON o.OrderId = oi.OrderId;

-- SOLUTION 2: Check for NULL foreign keys
SELECT * FROM Orders WHERE OrderId NOT IN (SELECT DISTINCT OrderId FROM OrderItems WHERE OrderId IS NOT NULL);

-- SOLUTION 3: Verify data integrity
-- Check for orphaned records
SELECT oi.* FROM OrderItems oi
LEFT JOIN Orders o ON oi.OrderId = o.OrderId
WHERE o.OrderId IS NULL;  -- OrderItems without parent Order

-- SOLUTION 4: Check JOIN condition (typo in column name?)
-- Verify: SELECT * FROM Orders o INNER JOIN OrderItems oi ON o.OrderId = oi.OrderId;
-- Common mistake: o.OrderId = o.OrderId (should be oi.OrderId)

-- SOLUTION 5: Check for data type mismatch
SELECT 
    o.OrderId,
    oi.OrderId,
    SQL_VARIANT_PROPERTY(o.OrderId, 'BaseType') AS OrdersType,
    SQL_VARIANT_PROPERTY(oi.OrderId, 'BaseType') AS OrderItemsType
FROM Orders o
LEFT JOIN OrderItems oi ON o.OrderId = oi.OrderId
WHERE o.OrderId IS NULL OR oi.OrderId IS NULL;
```

---

### Scenario 8: Race Condition in Inventory Update

**Question:** Multiple users can purchase the same product simultaneously. The application checks if quantity is available, then decrements it. Sometimes, the quantity goes negative. How would you prevent this?

**Answer:**
The solution uses atomic UPDATE operations with WHERE conditions to check and update in a single step, preventing race conditions. Alternative approaches include optimistic locking with version columns or using SERIALIZABLE isolation level to lock rows during read-update operations.

```sql
-- PROBLEM: Race condition - check and update are separate operations

-- SOLUTION 1: Use atomic UPDATE with WHERE condition
CREATE PROCEDURE sp_ReserveInventory
    @ProductId INT,
    @QuantityRequested INT
AS
BEGIN
    BEGIN TRANSACTION;
    
    -- Atomic update: only succeeds if enough stock
    UPDATE Inventory
    SET Quantity = Quantity - @QuantityRequested,
        LastUpdated = GETDATE()
    WHERE ProductId = @ProductId
      AND Quantity >= @QuantityRequested;  -- Critical: check in WHERE
    
    IF @@ROWCOUNT = 0
    BEGIN
        ROLLBACK TRANSACTION;
        RAISERROR('Insufficient inventory', 16, 1);
        RETURN;
    END
    
    COMMIT TRANSACTION;
END;

-- SOLUTION 2: Use optimistic locking with version column
ALTER TABLE Inventory ADD Version INT DEFAULT 0;

CREATE PROCEDURE sp_ReserveInventoryOptimistic
    @ProductId INT,
    @QuantityRequested INT,
    @CurrentVersion INT
AS
BEGIN
    BEGIN TRANSACTION;
    
    UPDATE Inventory
    SET Quantity = Quantity - @QuantityRequested,
        Version = Version + 1
    WHERE ProductId = @ProductId
      AND Version = @CurrentVersion  -- Ensures no concurrent modification
      AND Quantity >= @QuantityRequested;
    
    IF @@ROWCOUNT = 0
    BEGIN
        ROLLBACK TRANSACTION;
        RAISERROR('Concurrent modification detected or insufficient stock', 16, 1);
        RETURN;
    END
    
    COMMIT TRANSACTION;
END;

-- SOLUTION 3: Use application-level locking (pessimistic)
-- Lock row at application level before checking/updating

-- SOLUTION 4: Use SERIALIZABLE isolation level
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRANSACTION;
    SELECT Quantity FROM Inventory WHERE ProductId = @ProductId;
    -- Process...
    UPDATE Inventory SET Quantity = Quantity - @QuantityRequested WHERE ProductId = @ProductId;
COMMIT TRANSACTION;
```

---

### Scenario 9: Slow Query with Multiple OR Conditions

**Question:** A query with multiple OR conditions in the WHERE clause is very slow: `WHERE Status = 'Pending' OR Status = 'Processing' OR Status = 'Completed' OR CustomerId = 123 OR OrderDate > '2024-01-01'` . How would you optimize it?

**Answer:**
Multiple OR conditions prevent efficient index usage. The solution involves rewriting queries using UNION ALL for different conditions, using IN clause for same-column comparisons, or creating filtered indexes for specific status values to improve query performance.

```sql
-- PROBLEM: OR conditions prevent efficient index usage

-- SOLUTION 1: Use UNION ALL (often faster)
SELECT * FROM Orders WHERE Status = 'Pending'
UNION ALL
SELECT * FROM Orders WHERE Status = 'Processing'
UNION ALL
SELECT * FROM Orders WHERE Status = 'Completed'
UNION ALL
SELECT * FROM Orders WHERE CustomerId = 123
UNION ALL
SELECT * FROM Orders WHERE OrderDate > '2024-01-01';

-- SOLUTION 2: Use IN for same column
SELECT * FROM Orders 
WHERE Status IN ('Pending', 'Processing', 'Completed')
   OR CustomerId = 123
   OR OrderDate > '2024-01-01';

-- SOLUTION 3: Create filtered indexes
CREATE INDEX IX_Orders_Pending ON Orders(OrderId) WHERE Status = 'Pending';
CREATE INDEX IX_Orders_Processing ON Orders(OrderId) WHERE Status = 'Processing';
CREATE INDEX IX_Orders_Completed ON Orders(OrderId) WHERE Status = 'Completed';

-- SOLUTION 4: Rewrite with UNION and DISTINCT if duplicates are concern
SELECT DISTINCT * FROM (
    SELECT * FROM Orders WHERE Status IN ('Pending', 'Processing', 'Completed')
    UNION
    SELECT * FROM Orders WHERE CustomerId = 123
    UNION
    SELECT * FROM Orders WHERE OrderDate > '2024-01-01'
) AS Combined;

-- SOLUTION 5: If possible, separate into different queries based on business logic
```

---

### Scenario 10: Data Inconsistency Between Related Tables

**Question:** The `Orders` table shows `TotalAmount = 1000` , but the sum of `OrderItems.Amount` for that order is 950. There's a discrepancy. How would you find all such inconsistencies?

**Answer:**
Use aggregate queries with GROUP BY and HAVING to compare parent totals with calculated child sums. Fix inconsistencies by creating triggers to auto-update totals on child table changes, or use computed columns to ensure data consistency across related tables.

```sql
-- Find orders where TotalAmount doesn't match sum of items
SELECT 
    o.OrderId,
    o.TotalAmount AS OrderTotal,
    ISNULL(SUM(oi.Amount), 0) AS CalculatedTotal,
    o.TotalAmount - ISNULL(SUM(oi.Amount), 0) AS Difference
FROM Orders o
LEFT JOIN OrderItems oi ON o.OrderId = oi.OrderId
GROUP BY o.OrderId, o.TotalAmount
HAVING ABS(o.TotalAmount - ISNULL(SUM(oi.Amount), 0)) > 0.01;  -- Allow small rounding differences

-- Find orders with missing items
SELECT o.OrderId, o.TotalAmount
FROM Orders o
LEFT JOIN OrderItems oi ON o.OrderId = oi.OrderId
WHERE oi.OrderId IS NULL AND o.TotalAmount > 0;

-- Find items without parent order (orphaned)
SELECT oi.* FROM OrderItems oi
LEFT JOIN Orders o ON oi.OrderId = o.OrderId
WHERE o.OrderId IS NULL;

-- SOLUTION: Create trigger to maintain consistency
CREATE TRIGGER trg_OrderItems_UpdateTotal
ON OrderItems
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE o
    SET TotalAmount = (
        SELECT ISNULL(SUM(Amount), 0)
        FROM OrderItems oi
        WHERE oi.OrderId = o.OrderId
    )
    FROM Orders o
    INNER JOIN (
        SELECT DISTINCT OrderId FROM inserted
        UNION
        SELECT DISTINCT OrderId FROM deleted
    ) AS ChangedOrders ON o.OrderId = ChangedOrders.OrderId;
END;

-- Or use computed column (if TotalAmount can be calculated)
ALTER TABLE Orders
ADD CalculatedTotal AS (
    SELECT ISNULL(SUM(Amount), 0)
    FROM OrderItems oi
    WHERE oi.OrderId = Orders.OrderId
);
```

---

### Scenario 11: Memory Error with Large Result Set

**Question:** A report query returns 2 million rows, causing memory issues in the application. The query cannot be filtered further as all data is needed. How would you handle this?

**Answer:**
Implement server-side pagination using batching with TOP and WHERE conditions to process data in chunks. Alternative solutions include exporting to files using BCP/SSIS, using streaming approaches with table-valued functions, or implementing cursor-based pagination in the application layer.

```sql
-- SOLUTION 1: Implement server-side pagination/cursor
DECLARE @BatchSize INT = 10000;
DECLARE @LastId INT = 0;

WHILE 1 = 1
BEGIN
    SELECT TOP (@BatchSize)
        OrderId, CustomerId, OrderDate, TotalAmount
    FROM Orders
    WHERE OrderId > @LastId
    ORDER BY OrderId;
    
    IF @@ROWCOUNT = 0 BREAK;
    
    SET @LastId = (SELECT MAX(OrderId) FROM (
        SELECT TOP (@BatchSize) OrderId FROM Orders WHERE OrderId > @LastId ORDER BY OrderId
    ) AS Batch);
    
    -- Process batch in application
    -- Continue loop
END;

-- SOLUTION 2: Export to file instead of returning to application
-- Use BCP or SQL Server Integration Services (SSIS)
EXEC xp_cmdshell 'bcp "SELECT * FROM Orders" queryout "C:\Export\Orders.csv" -c -t, -S ServerName -d DatabaseName -T';

-- SOLUTION 3: Use table-valued function with streaming
CREATE FUNCTION fn_GetOrdersStream(@BatchSize INT)
RETURNS TABLE
AS
RETURN
(
    SELECT TOP (@BatchSize) * FROM Orders ORDER BY OrderId
);

-- SOLUTION 4: Create indexed view for pre-aggregated data
-- If report shows summaries, pre-aggregate in view

-- SOLUTION 5: Use SQL Server Reporting Services (SSRS) or similar
-- Let reporting tool handle large datasets

-- SOLUTION 6: Implement change data capture (CDC)
-- Only export changed data since last export
```

---

### Scenario 12: Trigger Causing Performance Issues

**Question:** An AFTER INSERT trigger on the `Orders` table updates an `OrderSummary` table. The trigger was fast initially, but now inserts are taking 10 seconds. The `Orders` table has grown to 10 million rows. What's wrong and how would you fix it?

**Answer:**
The trigger likely performs full table scans or recalculates entire summaries on each insert. Solutions include modifying triggers to only update affected rows using inserted/deleted tables, implementing incremental updates instead of recalculations, or moving summary updates to async background jobs.

```sql
-- PROBLEM: Trigger might be doing full table scans or inefficient operations

-- Current trigger (example of bad practice)
CREATE TRIGGER trg_Orders_UpdateSummary
ON Orders
AFTER INSERT
AS
BEGIN
    -- BAD: Full table scan
    UPDATE OrderSummary
    SET TotalOrders = (SELECT COUNT(*) FROM Orders),
        TotalAmount = (SELECT SUM(TotalAmount) FROM Orders);
END;

-- SOLUTION 1: Only update affected rows
CREATE TRIGGER trg_Orders_UpdateSummary_Fixed
ON Orders
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Only recalculate for affected customers
    UPDATE os
    SET 
        TotalOrders = (
            SELECT COUNT(*)
            FROM Orders o
            WHERE o.CustomerId = os.CustomerId
        ),
        TotalAmount = (
            SELECT ISNULL(SUM(TotalAmount), 0)
            FROM Orders o
            WHERE o.CustomerId = os.CustomerId
        )
    FROM OrderSummary os
    WHERE os.CustomerId IN (
        SELECT CustomerId FROM inserted
        UNION
        SELECT CustomerId FROM deleted
    );
END;

-- SOLUTION 2: Use incremental updates
CREATE TRIGGER trg_Orders_UpdateSummary_Incremental
ON Orders
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Incrementally update summary
    UPDATE os
    SET 
        TotalOrders = os.TotalOrders + 
            (SELECT COUNT(*) FROM inserted i WHERE i.CustomerId = os.CustomerId) -
            (SELECT COUNT(*) FROM deleted d WHERE d.CustomerId = os.CustomerId),
        TotalAmount = os.TotalAmount +
            (SELECT ISNULL(SUM(TotalAmount), 0) FROM inserted i WHERE i.CustomerId = os.CustomerId) -
            (SELECT ISNULL(SUM(TotalAmount), 0) FROM deleted d WHERE d.CustomerId = os.CustomerId)
    FROM OrderSummary os
    WHERE os.CustomerId IN (
        SELECT CustomerId FROM inserted
        UNION
        SELECT CustomerId FROM deleted
    );
END;

-- SOLUTION 3: Move to async processing
-- Instead of trigger, use queue table and background job

-- SOLUTION 4: Disable trigger and use scheduled job
-- Update summary table every 5 minutes instead of real-time
```

---

### Scenario 13: Incorrect Aggregation Results with NULLs

**Question:** A report shows `AVG(Salary)` as NULL for a department that has employees. Some employees have NULL salary. How would you fix the calculation?

**Answer:**
AVG() returns NULL when all values are NULL, and it ignores NULL values in calculation. Solutions include using COALESCE to convert NULLs to zero, filtering out NULL values explicitly, or displaying COUNT(Salary) alongside AVG(Salary) to show data completeness in the report.

```sql
-- PROBLEM: AVG ignores NULLs, but if ALL values are NULL, returns NULL

-- Check the data
SELECT 
    Department,
    COUNT(*) AS TotalEmployees,
    COUNT(Salary) AS EmployeesWithSalary,
    AVG(Salary) AS AverageSalary
FROM Employee
GROUP BY Department;

-- SOLUTION 1: Use COALESCE to treat NULL as 0 (if business rule allows)
SELECT 
    Department,
    AVG(COALESCE(Salary, 0)) AS AverageSalary
FROM Employee
GROUP BY Department;

-- SOLUTION 2: Only calculate for employees with salary
SELECT 
    Department,
    AVG(Salary) AS AverageSalary,
    COUNT(*) AS TotalEmployees,
    COUNT(Salary) AS EmployeesWithSalary,
    CASE 
        WHEN COUNT(Salary) = 0 THEN 'No salary data'
        ELSE CAST(AVG(Salary) AS VARCHAR)
    END AS AverageSalaryDisplay
FROM Employee
GROUP BY Department;

-- SOLUTION 3: Use different aggregation logic
SELECT 
    Department,
    AVG(CASE WHEN Salary IS NOT NULL THEN Salary END) AS AverageSalary,
    SUM(Salary) / NULLIF(COUNT(Salary), 0) AS AverageSalaryAlt  -- Same result
FROM Employee
GROUP BY Department;

-- SOLUTION 4: Handle in application layer
-- Check COUNT(Salary) before displaying AVG(Salary)
```

---

### Scenario 14: Query Timeout on Production

**Question:** A critical report query times out after 30 seconds in production but runs fine in development (completes in 2 seconds). Both environments have similar data volumes. What could cause this and how would you investigate?

**Answer:**
Production timeouts despite similar data usually indicate blocking/locks from concurrent users, outdated statistics causing poor execution plans, or missing indexes. Investigate using sys.dm_exec_requests for blocking, compare execution plans between environments, and verify index and statistics consistency.

```sql
-- POSSIBLE CAUSES: Locks, different execution plans, statistics, indexes

-- INVESTIGATION STEPS:

-- Step 1: Check for blocking/locks
SELECT 
    blocking_session_id,
    wait_type,
    wait_time,
    session_id,
    command,
    text
FROM sys.dm_exec_requests r
CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) t
WHERE r.session_id <> @@SPID;

-- Step 2: Compare execution plans
-- Run in both environments and compare
SET STATISTICS IO ON;
SET STATISTICS TIME ON;
-- Your query here
SET STATISTICS IO OFF;
SET STATISTICS TIME OFF;

-- Step 3: Check index differences
SELECT 
    OBJECT_NAME(object_id) AS TableName,
    name AS IndexName,
    type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('YourTable')
ORDER BY name;

-- Step 4: Check statistics freshness
SELECT 
    OBJECT_NAME(object_id) AS TableName,
    name AS StatName,
    STATS_DATE(object_id, stats_id) AS LastUpdated
FROM sys.stats
WHERE object_id = OBJECT_ID('YourTable');

-- Step 5: Check parameter sniffing
-- If using stored procedure, check if parameters differ
-- SOLUTION: Use OPTION (RECOMPILE) or OPTION (OPTIMIZE FOR UNKNOWN)

-- Step 6: Check for missing indexes
-- Review execution plan for "Missing Index" warnings

-- Step 7: Check table/index fragmentation
SELECT 
    OBJECT_NAME(object_id) AS TableName,
    index_id,
    avg_fragmentation_in_percent
FROM sys.dm_db_index_physical_stats(
    DB_ID(), 
    OBJECT_ID('YourTable'), 
    NULL, 
    NULL, 
    'DETAILED'
)
WHERE avg_fragmentation_in_percent > 30;

-- QUICK FIXES:
-- 1. Update statistics
UPDATE STATISTICS YourTable WITH FULLSCAN;

-- 2. Rebuild indexes
ALTER INDEX ALL ON YourTable REBUILD;

-- 3. Add query hint for recompile
SELECT * FROM YourTable
OPTION (RECOMPILE);

-- 4. Check for different SQL Server versions/service packs
SELECT @@VERSION;
```

---

### Scenario 15: Cascading Delete Causing Unexpected Deletions

**Question:** Deleting a customer record also deletes all their orders due to CASCADE DELETE on the foreign key. However, business rules require that orders should be archived, not deleted. How would you handle this?

**Answer:**
Replace CASCADE DELETE with soft delete pattern by adding IsDeleted flags, or use INSTEAD OF DELETE triggers to archive data before deletion. Alternative solutions include moving records to archive tables while maintaining referential integrity, or removing CASCADE and handling deletes through stored procedures.

```sql
-- PROBLEM: CASCADE DELETE removes child records

-- SOLUTION 1: Remove CASCADE, use soft delete
-- Step 1: Remove CASCADE DELETE
ALTER TABLE Orders
DROP CONSTRAINT FK_Orders_Customer;

-- Step 2: Add IsDeleted flag
ALTER TABLE Customer ADD IsDeleted BIT DEFAULT 0;
ALTER TABLE Orders ADD IsDeleted BIT DEFAULT 0;

-- Step 3: Create new foreign key without CASCADE
ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Customer 
FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId);

-- Step 4: Use soft delete procedure
CREATE PROCEDURE sp_DeleteCustomer
    @CustomerId INT
AS
BEGIN
    BEGIN TRANSACTION;
    
    -- Soft delete customer
    UPDATE Customer SET IsDeleted = 1 WHERE CustomerId = @CustomerId;
    
    -- Soft delete related orders
    UPDATE Orders SET IsDeleted = 1 WHERE CustomerId = @CustomerId;
    
    -- Archive orders to archive table
    INSERT INTO OrdersArchive
    SELECT * FROM Orders WHERE CustomerId = @CustomerId;
    
    COMMIT TRANSACTION;
END;

-- SOLUTION 2: Use INSTEAD OF DELETE trigger
CREATE TRIGGER trg_Customer_InsteadOfDelete
ON Customer
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Archive orders before soft delete
    INSERT INTO OrdersArchive
    SELECT o.* FROM Orders o
    INNER JOIN deleted d ON o.CustomerId = d.CustomerId;
    
    -- Soft delete customer and orders
    UPDATE Customer SET IsDeleted = 1 
    WHERE CustomerId IN (SELECT CustomerId FROM deleted);
    
    UPDATE Orders SET IsDeleted = 1 
    WHERE CustomerId IN (SELECT CustomerId FROM deleted);
END;

-- SOLUTION 3: Move to archive table instead of delete
CREATE TABLE CustomerArchive (
    -- Same structure as Customer
    ArchiveDate DATETIME DEFAULT GETDATE()
);

CREATE PROCEDURE sp_ArchiveCustomer
    @CustomerId INT
AS
BEGIN
    BEGIN TRANSACTION;
    
    -- Archive customer
    INSERT INTO CustomerArchive
    SELECT *, GETDATE() FROM Customer WHERE CustomerId = @CustomerId;
    
    -- Archive orders
    INSERT INTO OrdersArchive
    SELECT *, GETDATE() FROM Orders WHERE CustomerId = @CustomerId;
    
    -- Now safe to delete (or keep with IsDeleted flag)
    DELETE FROM Orders WHERE CustomerId = @CustomerId;
    DELETE FROM Customer WHERE CustomerId = @CustomerId;
    
    COMMIT TRANSACTION;
END;
```

---

### Scenario 16: Stored Procedure Returns Different Results on Each Execution

**Question:** A stored procedure that calculates monthly sales totals returns different results each time it's executed, even with the same input parameters. The procedure uses temp tables and doesn't have ORDER BY in some queries. What could be wrong?

**Answer:**
Missing ORDER BY clauses cause non-deterministic result ordering, especially with TOP queries. Additionally, using GETDATE(), RAND(), or NEWID() inside procedures makes results non-deterministic. Solutions include adding explicit ORDER BY, passing datetime values as parameters instead of using GETDATE(), and ensuring consistent ordering in all queries.

```sql
-- PROBLEM: Missing ORDER BY, or using non-deterministic functions incorrectly

-- EXAMPLE OF PROBLEMATIC PROCEDURE
CREATE PROCEDURE sp_GetMonthlySales
    @Month INT,
    @Year INT
AS
BEGIN
    CREATE TABLE #TempSales (ProductId INT, TotalSales DECIMAL(10,2));
    
    -- Missing ORDER BY - results in random order
    INSERT INTO #TempSales
    SELECT ProductId, SUM(Amount) AS TotalSales
    FROM Sales
    WHERE MONTH(SaleDate) = @Month AND YEAR(SaleDate) = @Year
    GROUP BY ProductId;  -- No ORDER BY
    
    -- Using GETDATE() or RAND() makes results non-deterministic
    SELECT *, GETDATE() AS ReportDate FROM #TempSales;
    
    -- Using TOP without ORDER BY
    SELECT TOP 10 * FROM #TempSales;  -- Returns random 10 rows
END;

-- SOLUTION: Add proper ORDER BY and avoid non-deterministic functions
CREATE PROCEDURE sp_GetMonthlySales_Fixed
    @Month INT,
    @Year INT,
    @ReportDate DATETIME  -- Pass as parameter instead of GETDATE()
AS
BEGIN
    CREATE TABLE #TempSales (
        ProductId INT, 
        TotalSales DECIMAL(10,2),
        RowNum INT IDENTITY(1,1)  -- For consistent ordering
    );
    
    -- Add ORDER BY for consistent results
    INSERT INTO #TempSales (ProductId, TotalSales)
    SELECT ProductId, SUM(Amount) AS TotalSales
    FROM Sales
    WHERE MONTH(SaleDate) = @Month AND YEAR(SaleDate) = @Year
    GROUP BY ProductId
    ORDER BY ProductId;  -- Deterministic ordering
    
    -- Use parameter instead of GETDATE()
    SELECT *, @ReportDate AS ReportDate 
    FROM #TempSales
    ORDER BY TotalSales DESC;  -- Consistent ordering
    
    -- TOP with ORDER BY
    SELECT TOP 10 * 
    FROM #TempSales
    ORDER BY TotalSales DESC;  -- Always same top 10
END;

-- COMMON ISSUES:
-- 1. Missing ORDER BY in SELECT statements
-- 2. Using GETDATE(), NEWID(), RAND() without seed
-- 3. TOP without ORDER BY
-- 4. Window functions without ORDER BY in OVER clause
-- 5. Non-deterministic functions in computed columns
```

---

### Scenario 17: Foreign Key Constraint Preventing Legitimate Updates

**Question:** You need to update `CustomerId` in the `Orders` table, but a foreign key constraint prevents it. The new `CustomerId` exists in the `Customer` table. However, you're getting an error. What could be wrong?

**Answer:**
Common issues include updating to non-existent CustomerIds in batch updates, data type mismatches between foreign and primary keys, or check constraints on the Customer table. Solutions involve validating all target values exist before update, ensuring proper data type casting, and checking for circular references or additional constraints.

```sql
-- POSSIBLE ISSUES:

-- Issue 1: Self-referencing update (updating to same value)
UPDATE Orders SET CustomerId = CustomerId WHERE OrderId = 101;
-- SQL Server might still check constraint

-- Issue 2: Updating multiple rows where some violate constraint
UPDATE Orders 
SET CustomerId = CASE 
    WHEN OrderId = 101 THEN 999  -- Valid
    WHEN OrderId = 102 THEN 888  -- Invalid (doesn't exist)
END
WHERE OrderId IN (101, 102);

-- SOLUTION: Update in transaction with validation
BEGIN TRANSACTION;

-- Validate all new CustomerIds exist
IF EXISTS (
    SELECT 1 FROM Orders o
    WHERE o.OrderId IN (101, 102)
    AND NOT EXISTS (
        SELECT 1 FROM Customer c 
        WHERE c.CustomerId = CASE 
            WHEN o.OrderId = 101 THEN 999
            WHEN o.OrderId = 102 THEN 888
        END
    )
)
BEGIN
    ROLLBACK TRANSACTION;
    RAISERROR('Invalid CustomerId', 16, 1);
    RETURN;
END

-- Now safe to update
UPDATE Orders 
SET CustomerId = CASE 
    WHEN OrderId = 101 THEN 999
    WHEN OrderId = 102 THEN 888
END
WHERE OrderId IN (101, 102);

COMMIT TRANSACTION;

-- Issue 3: Check constraint on Customer table
-- Customer table might have CHECK constraint preventing certain IDs
SELECT * FROM sys.check_constraints 
WHERE parent_object_id = OBJECT_ID('Customer');

-- Issue 4: Data type mismatch
-- CustomerId in Orders might be INT, but trying to update with VARCHAR
UPDATE Orders SET CustomerId = '999' WHERE OrderId = 101;  -- Might fail

-- SOLUTION: Cast explicitly
UPDATE Orders SET CustomerId = CAST('999' AS INT) WHERE OrderId = 101;

-- Issue 5: NULL values if column is NOT NULL
UPDATE Orders SET CustomerId = NULL WHERE OrderId = 101;
-- Fails if CustomerId is NOT NULL

-- Issue 6: Circular reference
-- If Customer also references Orders, might cause issues
-- Check all foreign key relationships
SELECT 
    fk.name AS FKName,
    OBJECT_NAME(fk.parent_object_id) AS ParentTable,
    OBJECT_NAME(fk.referenced_object_id) AS ReferencedTable
FROM sys.foreign_keys fk
WHERE fk.parent_object_id = OBJECT_ID('Orders')
   OR fk.referenced_object_id = OBJECT_ID('Orders');
```

---

### Scenario 18: Query with Date Range Returns No Results

**Question:** A query filtering by date range returns no results: `WHERE OrderDate BETWEEN '2024-01-01' AND '2024-01-31'` . You know there are orders in January 2024. The `OrderDate` column is `DATETIME` . What's wrong?

**Answer:**
DATETIME columns include time components, so BETWEEN '2024-01-31' means '2024-01-31 00:00:00', excluding all records after midnight on that day. Solutions include using >= and < with next day ('2024-02-01'), adding time to end date (23:59:59), or converting to DATE for comparison, though this prevents index usage.

```sql
-- PROBLEM: DATETIME includes time component

-- If OrderDate = '2024-01-31 14:30:00'
-- BETWEEN '2024-01-01' AND '2024-01-31' 
-- Becomes: BETWEEN '2024-01-01 00:00:00' AND '2024-01-31 00:00:00'
-- This excludes '2024-01-31 14:30:00' (it's after midnight)

-- SOLUTION 1: Include time in end date
SELECT * FROM Orders
WHERE OrderDate BETWEEN '2024-01-01' AND '2024-01-31 23:59:59.997';

-- SOLUTION 2: Use >= and < (better for DATETIME2)
SELECT * FROM Orders
WHERE OrderDate >= '2024-01-01' 
  AND OrderDate < '2024-02-01';  -- Exclusive upper bound

-- SOLUTION 3: Use DATE functions
SELECT * FROM Orders
WHERE CAST(OrderDate AS DATE) BETWEEN '2024-01-01' AND '2024-01-31';
-- Note: This prevents index usage on OrderDate

-- SOLUTION 4: Use YEAR and MONTH (if index usage is not critical)
SELECT * FROM Orders
WHERE YEAR(OrderDate) = 2024 AND MONTH(OrderDate) = 1;

-- SOLUTION 5: Use DATE data type (if possible to change schema)
ALTER TABLE Orders
ALTER COLUMN OrderDate DATE;  -- Then BETWEEN works as expected

-- SOLUTION 6: Use EOMONTH function
SELECT * FROM Orders
WHERE OrderDate >= '2024-01-01' 
  AND OrderDate <= EOMONTH('2024-01-31');

-- DEBUG: Check actual data
SELECT 
    OrderId,
    OrderDate,
    CAST(OrderDate AS DATE) AS OrderDateOnly,
    DATEPART(HOUR, OrderDate) AS Hour,
    DATEPART(MINUTE, OrderDate) AS Minute
FROM Orders
WHERE OrderDate >= '2024-01-01' AND OrderDate < '2024-02-01'
ORDER BY OrderDate;
```

---

### Scenario 19: Duplicate Key Error on INSERT

**Question:** An INSERT statement fails with "Violation of PRIMARY KEY constraint" even though you're using `IDENTITY` column and not specifying the primary key value. The table has an `IDENTITY` seed that should auto-increment. What could cause this?

**Answer:**
Identity values can get out of sync due to failed transactions, manual inserts with IDENTITY_INSERT ON, or data imports that didn't reseed properly. Solutions include using DBCC CHECKIDENT to reseed the identity column, checking for gaps in sequence, and ensuring previous manual inserts properly incremented the identity counter.

```sql
-- POSSIBLE CAUSES:

-- Cause 1: Identity seed/reset issue
-- Check current identity value
SELECT IDENT_CURRENT('Orders') AS CurrentIdentity;
SELECT IDENT_INCR('Orders') AS Increment;
SELECT IDENT_SEED('Orders') AS Seed;

-- If identity is behind, reset it
DBCC CHECKIDENT('Orders', RESEED, 1000);

-- Cause 2: Manual insert with explicit ID
INSERT INTO Orders (OrderId, CustomerId, OrderDate)  -- Explicitly setting OrderId
VALUES (100, 1, '2024-01-15');
-- This doesn't increment identity, next auto-insert might conflict

-- SOLUTION: Don't specify identity column, or use SET IDENTITY_INSERT
SET IDENTITY_INSERT Orders ON;
INSERT INTO Orders (OrderId, CustomerId, OrderDate)
VALUES (100, 1, '2024-01-15');
SET IDENTITY_INSERT Orders OFF;

-- Cause 3: Data imported with identity insert enabled
-- Previous import might have inserted IDs manually

-- Cause 4: Multiple inserts in transaction, one fails
BEGIN TRANSACTION;
    INSERT INTO Orders (CustomerId, OrderDate) VALUES (1, '2024-01-15');  -- Gets ID 100
    INSERT INTO Orders (CustomerId, OrderDate) VALUES (2, '2024-01-16');  -- Gets ID 101
    -- Error occurs, transaction rolls back, but identity already incremented
ROLLBACK;
-- Next insert will try ID 102, but if 100-101 exist, might conflict

-- SOLUTION: Check for gaps
SELECT 
    OrderId,
    LAG(OrderId) OVER (ORDER BY OrderId) AS PreviousId,
    OrderId - LAG(OrderId) OVER (ORDER BY OrderId) AS Gap
FROM Orders
ORDER BY OrderId;

-- Cause 5: Replication or merge causing conflicts
-- Check if replication is enabled
SELECT name, is_published, is_subscribed 
FROM sys.databases 
WHERE name = DB_NAME();

-- Cause 6: Composite primary key with non-identity column
CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1),
    OrderNumber VARCHAR(20),
    PRIMARY KEY (OrderId, OrderNumber)  -- Composite key
);
-- If OrderNumber is duplicated, violates constraint

-- SOLUTION: Check for duplicates in composite key
SELECT OrderId, OrderNumber, COUNT(*) AS DuplicateCount
FROM Orders
GROUP BY OrderId, OrderNumber
HAVING COUNT(*) > 1;
```

---

### Scenario 20: CTE Referenced Multiple Times Performance

**Question:** A CTE is referenced 5 times in the same query. The query is slow. You notice the execution plan shows the CTE logic executed 5 times. How would you optimize this?

**Answer:**
CTEs are re-evaluated each time they're referenced, causing performance issues when used multiple times. Solutions include materializing results into temp tables or table variables for single execution, using window functions for aggregates, or restructuring the query to reference CTE once with CROSS JOIN for aggregate values.

```sql
-- PROBLEM: CTE is re-evaluated each time it's referenced

-- Example of inefficient CTE usage
WITH SalesSummary AS (
    SELECT 
        CustomerId,
        SUM(Amount) AS TotalSales,
        COUNT(*) AS OrderCount
    FROM Orders
    WHERE OrderDate >= '2024-01-01'
    GROUP BY CustomerId
)
SELECT 
    (SELECT AVG(TotalSales) FROM SalesSummary) AS AvgSales,  -- Execution 1
    (SELECT MAX(TotalSales) FROM SalesSummary) AS MaxSales,   -- Execution 2
    (SELECT MIN(TotalSales) FROM SalesSummary) AS MinSales,  -- Execution 3
    (SELECT COUNT(*) FROM SalesSummary) AS CustomerCount,     -- Execution 4
    ss.TotalSales                                             -- Execution 5
FROM SalesSummary ss;

-- SOLUTION 1: Use temp table instead of CTE
CREATE TABLE #SalesSummary (
    CustomerId INT,
    TotalSales DECIMAL(10,2),
    OrderCount INT
);

INSERT INTO #SalesSummary
SELECT 
    CustomerId,
    SUM(Amount) AS TotalSales,
    COUNT(*) AS OrderCount
FROM Orders
WHERE OrderDate >= '2024-01-01'
GROUP BY CustomerId;

-- Now reference temp table (executed once)
SELECT 
    (SELECT AVG(TotalSales) FROM #SalesSummary) AS AvgSales,
    (SELECT MAX(TotalSales) FROM #SalesSummary) AS MaxSales,
    (SELECT MIN(TotalSales) FROM #SalesSummary) AS MinSales,
    (SELECT COUNT(*) FROM #SalesSummary) AS CustomerCount,
    ss.TotalSales
FROM #SalesSummary ss;

DROP TABLE #SalesSummary;

-- SOLUTION 2: Use table variable (for smaller datasets)
DECLARE @SalesSummary TABLE (
    CustomerId INT,
    TotalSales DECIMAL(10,2),
    OrderCount INT
);

INSERT INTO @SalesSummary
SELECT CustomerId, SUM(Amount), COUNT(*)
FROM Orders
WHERE OrderDate >= '2024-01-01'
GROUP BY CustomerId;

SELECT * FROM @SalesSummary;

-- SOLUTION 3: Calculate aggregates in single pass
WITH SalesSummary AS (
    SELECT 
        CustomerId,
        SUM(Amount) AS TotalSales,
        COUNT(*) AS OrderCount
    FROM Orders
    WHERE OrderDate >= '2024-01-01'
    GROUP BY CustomerId
)
SELECT 
    AVG(TotalSales) OVER () AS AvgSales,
    MAX(TotalSales) OVER () AS MaxSales,
    MIN(TotalSales) OVER () AS MinSales,
    COUNT(*) OVER () AS CustomerCount,
    TotalSales
FROM SalesSummary;

-- SOLUTION 4: Use CROSS JOIN with aggregated CTE once
WITH SalesSummary AS (
    SELECT 
        CustomerId,
        SUM(Amount) AS TotalSales,
        COUNT(*) AS OrderCount
    FROM Orders
    WHERE OrderDate >= '2024-01-01'
    GROUP BY CustomerId
),
Aggregates AS (
    SELECT 
        AVG(TotalSales) AS AvgSales,
        MAX(TotalSales) AS MaxSales,
        MIN(TotalSales) AS MinSales,
        COUNT(*) AS CustomerCount
    FROM SalesSummary
)
SELECT 
    a.AvgSales,
    a.MaxSales,
    a.MinSales,
    a.CustomerCount,
    ss.TotalSales
FROM SalesSummary ss
CROSS JOIN Aggregates a;
```

---

### Scenario 21: UNION Removing Valid Duplicates

**Question:** You're using UNION to combine customer lists from two tables. Some customers legitimately appear in both tables and should be shown twice, but UNION is removing them. How would you handle this?

**Answer:**
UNION automatically removes duplicates by performing a DISTINCT operation. Use UNION ALL to preserve all records including duplicates, or add source identifiers to differentiate between tables. Alternatively, use window functions to count occurrences or FULL OUTER JOIN to identify records existing in both sources.

```sql
-- PROBLEM: UNION removes duplicates

-- Current query (removes duplicates)
SELECT CustomerName, Email FROM CurrentCustomers
UNION
SELECT CustomerName, Email FROM ArchivedCustomers;
-- If same customer in both, appears only once

-- SOLUTION 1: Use UNION ALL to keep duplicates
SELECT CustomerName, Email FROM CurrentCustomers
UNION ALL
SELECT CustomerName, Email FROM ArchivedCustomers;
-- Shows all customers, including duplicates

-- SOLUTION 2: Add source identifier if you need to track origin
SELECT CustomerName, Email, 'Current' AS Source FROM CurrentCustomers
UNION ALL
SELECT CustomerName, Email, 'Archived' AS Source FROM ArchivedCustomers;

-- SOLUTION 3: If you need duplicates but want to mark them
WITH AllCustomers AS (
    SELECT CustomerName, Email, 'Current' AS Source FROM CurrentCustomers
    UNION ALL
    SELECT CustomerName, Email, 'Archived' AS Source FROM ArchivedCustomers
)
SELECT 
    CustomerName,
    Email,
    Source,
    COUNT(*) OVER (PARTITION BY CustomerName, Email) AS OccurrenceCount
FROM AllCustomers
ORDER BY CustomerName, Email;

-- SOLUTION 4: If business rule: show current customers, then archived (no duplicates)
SELECT CustomerName, Email, 'Current' AS Source FROM CurrentCustomers
UNION
SELECT CustomerName, Email, 'Archived' AS Source FROM ArchivedCustomers
WHERE NOT EXISTS (
    SELECT 1 FROM CurrentCustomers c 
    WHERE c.CustomerName = ArchivedCustomers.CustomerName 
    AND c.Email = ArchivedCustomers.Email
);

-- SOLUTION 5: Use FULL OUTER JOIN if you need to see both sources
SELECT 
    COALESCE(c.CustomerName, a.CustomerName) AS CustomerName,
    COALESCE(c.Email, a.Email) AS Email,
    CASE 
        WHEN c.CustomerName IS NOT NULL AND a.CustomerName IS NOT NULL THEN 'Both'
        WHEN c.CustomerName IS NOT NULL THEN 'Current Only'
        ELSE 'Archived Only'
    END AS Source
FROM CurrentCustomers c
FULL OUTER JOIN ArchivedCustomers a 
    ON c.CustomerName = a.CustomerName AND c.Email = a.Email;
```

---

### Scenario 22: Index Not Being Used Despite Existing

**Question:** You created an index on `CustomerEmail` column, but queries with `WHERE CustomerEmail = @Email` still show Table Scan in execution plan. The table has 1 million rows. Why isn't the index being used?

**Answer:**
Indexes aren't used when there's implicit data type conversion (VARCHAR vs NVARCHAR), functions on indexed columns (UPPER, LOWER), outdated statistics, disabled indexes, or when optimizer determines table scans are cheaper. Solutions include matching data types, avoiding functions on indexed columns, updating statistics, rebuilding indexes, or using computed columns for function-based searches.

```sql
-- POSSIBLE REASONS:

-- Reason 1: Implicit data type conversion
-- CustomerEmail is VARCHAR, but comparing with NVARCHAR
SELECT * FROM Customer WHERE CustomerEmail = N'test@email.com';  -- NVARCHAR prefix
-- Index not used due to conversion

-- SOLUTION: Match data types
SELECT * FROM Customer WHERE CustomerEmail = 'test@email.com';  -- VARCHAR

-- Reason 2: Function on indexed column
SELECT * FROM Customer WHERE UPPER(CustomerEmail) = 'TEST@EMAIL.COM';
-- Function prevents index usage

-- SOLUTION: Use case-insensitive collation or computed column
-- Option A: Case-insensitive collation
CREATE INDEX IX_Customer_Email ON Customer(CustomerEmail) 
WITH (IGNORE_DUP_KEY = OFF);

-- Query (if collation is case-insensitive)
SELECT * FROM Customer WHERE CustomerEmail = 'test@email.com';

-- Option B: Computed column
ALTER TABLE Customer
ADD CustomerEmailUpper AS UPPER(CustomerEmail);

CREATE INDEX IX_Customer_EmailUpper ON Customer(CustomerEmailUpper);

SELECT * FROM Customer WHERE CustomerEmailUpper = 'TEST@EMAIL.COM';

-- Reason 3: Statistics are outdated
-- Check statistics
DBCC SHOW_STATISTICS('Customer', 'IX_Customer_Email');

-- SOLUTION: Update statistics
UPDATE STATISTICS Customer WITH FULLSCAN;

-- Reason 4: Index is disabled
-- Check index status
SELECT 
    name,
    is_disabled,
    type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('Customer') AND name = 'IX_Customer_Email';

-- SOLUTION: Rebuild index
ALTER INDEX IX_Customer_Email ON Customer REBUILD;

-- Reason 5: Query returns too many rows (optimizer chooses scan)
-- If query returns > 5-10% of table, scan might be faster

-- SOLUTION: Add more selective WHERE conditions
SELECT * FROM Customer 
WHERE CustomerEmail = 'test@email.com' 
  AND IsActive = 1;  -- More selective

-- Create composite index
CREATE INDEX IX_Customer_Email_IsActive 
ON Customer(CustomerEmail, IsActive);

-- Reason 6: Parameter sniffing issue
CREATE PROCEDURE sp_GetCustomer
    @Email VARCHAR(100)
AS
BEGIN
    -- If @Email is NULL or very common value, might not use index
    SELECT * FROM Customer WHERE CustomerEmail = @Email;
END;

-- SOLUTION: Use query hint
CREATE PROCEDURE sp_GetCustomer_Fixed
    @Email VARCHAR(100)
AS
BEGIN
    SELECT * FROM Customer 
    WHERE CustomerEmail = @Email
    OPTION (RECOMPILE);  -- Recompile with actual parameter value
END;

-- Reason 7: Index fragmentation
SELECT 
    avg_fragmentation_in_percent,
    page_count
FROM sys.dm_db_index_physical_stats(
    DB_ID(), 
    OBJECT_ID('Customer'), 
    (SELECT index_id FROM sys.indexes WHERE name = 'IX_Customer_Email'),
    NULL, 
    'DETAILED'
);

-- SOLUTION: Rebuild or reorganize
ALTER INDEX IX_Customer_Email ON Customer REBUILD;
-- Or
ALTER INDEX IX_Customer_Email ON Customer REORGANIZE;
```

---

### Scenario 23: Recursive Query Hitting Maximum Recursion

**Question:** A recursive CTE for employee hierarchy throws "Maximum recursion limit of 100 has been exhausted" error. The hierarchy has more than 100 levels. How would you fix this?

**Answer:**
SQL Server limits recursive CTEs to 100 iterations by default to prevent infinite loops. Solutions include using OPTION (MAXRECURSION n) to increase the limit, MAXRECURSION 0 for unlimited recursion (with cycle detection), or implementing iterative WHILE loops with temp tables for very deep hierarchies to avoid CTE limitations.

```sql
-- PROBLEM: Default recursion limit is 100

-- Current query (fails for deep hierarchies)
WITH EmployeeHierarchy AS (
    -- Anchor
    SELECT EmployeeId, EmployeeName, ManagerId, 0 AS Level
    FROM Employee
    WHERE ManagerId IS NULL
    
    UNION ALL
    
    -- Recursive
    SELECT e.EmployeeId, e.EmployeeName, e.ManagerId, h.Level + 1
    FROM Employee e
    INNER JOIN EmployeeHierarchy h ON e.ManagerId = h.EmployeeId
)
SELECT * FROM EmployeeHierarchy;

-- SOLUTION 1: Increase MAXRECURSION option
WITH EmployeeHierarchy AS (
    SELECT EmployeeId, EmployeeName, ManagerId, 0 AS Level
    FROM Employee
    WHERE ManagerId IS NULL
    
    UNION ALL
    
    SELECT e.EmployeeId, e.EmployeeName, e.ManagerId, h.Level + 1
    FROM Employee e
    INNER JOIN EmployeeHierarchy h ON e.ManagerId = h.EmployeeId
)
SELECT * FROM EmployeeHierarchy
OPTION (MAXRECURSION 1000);  -- Increase limit

-- SOLUTION 2: Remove limit (use 0 for unlimited - use with caution)
SELECT * FROM EmployeeHierarchy
OPTION (MAXRECURSION 0);  -- Unlimited (risky for infinite loops)

-- SOLUTION 3: Detect circular references before recursion
-- Add check for cycles
WITH EmployeeHierarchy AS (
    SELECT 
        EmployeeId, 
        EmployeeName, 
        ManagerId, 
        0 AS Level,
        CAST(EmployeeId AS VARCHAR(MAX)) AS Path  -- Track path
    FROM Employee
    WHERE ManagerId IS NULL
    
    UNION ALL
    
    SELECT 
        e.EmployeeId, 
        e.EmployeeName, 
        e.ManagerId, 
        h.Level + 1,
        h.Path + '>' + CAST(e.EmployeeId AS VARCHAR(MAX))
    FROM Employee e
    INNER JOIN EmployeeHierarchy h ON e.ManagerId = h.EmployeeId
    WHERE h.Path NOT LIKE '%>' + CAST(e.EmployeeId AS VARCHAR(MAX)) + '>%'  -- Prevent cycles
      AND h.Level < 1000  -- Safety limit
)
SELECT * FROM EmployeeHierarchy
OPTION (MAXRECURSION 0);

-- SOLUTION 4: Use iterative approach with WHILE loop (for very deep hierarchies)
DECLARE @Level INT = 0;
CREATE TABLE #Hierarchy (
    EmployeeId INT,
    EmployeeName VARCHAR(100),
    ManagerId INT,
    Level INT
);

-- Insert root level
INSERT INTO #Hierarchy
SELECT EmployeeId, EmployeeName, ManagerId, 0
FROM Employee
WHERE ManagerId IS NULL;

-- Iteratively add levels
WHILE @@ROWCOUNT > 0 AND @Level < 1000
BEGIN
    SET @Level = @Level + 1;
    
    INSERT INTO #Hierarchy
    SELECT e.EmployeeId, e.EmployeeName, e.ManagerId, @Level
    FROM Employee e
    INNER JOIN #Hierarchy h ON e.ManagerId = h.EmployeeId
    WHERE h.Level = @Level - 1
      AND e.EmployeeId NOT IN (SELECT EmployeeId FROM #Hierarchy);  -- Avoid duplicates
END;

SELECT * FROM #Hierarchy ORDER BY Level, EmployeeId;
DROP TABLE #Hierarchy;
```

---

### Scenario 24: Bulk Insert Performance Issues

**Question:** Inserting 100, 000 rows using individual INSERT statements takes 30 minutes. You need to improve this to under 1 minute. How would you optimize it?

**Answer:**
Individual row inserts are extremely slow due to transaction overhead per row. Solutions include using set-based INSERT with SELECT, table-valued parameters, BULK INSERT from files, temporarily disabling indexes/triggers during insert, using TABLOCK for minimal logging, or batching inserts in chunks to significantly improve performance from minutes to seconds.

```sql
-- PROBLEM: Row-by-row inserts are slow

-- Current slow approach
DECLARE @i INT = 1;
WHILE @i <= 100000
BEGIN
    INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
    VALUES (@i, GETDATE(), 100.00);
    SET @i = @i + 1;
END;

-- SOLUTION 1: Batch INSERT (much faster)
INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
SELECT 
    number AS CustomerId,
    GETDATE() AS OrderDate,
    100.00 AS TotalAmount
FROM master.dbo.spt_values
WHERE type = 'P' AND number BETWEEN 1 AND 100000;

-- SOLUTION 2: Use table-valued parameter (for application code)
CREATE TYPE OrderList AS TABLE (
    CustomerId INT,
    OrderDate DATETIME,
    TotalAmount DECIMAL(10,2)
);

CREATE PROCEDURE sp_BulkInsertOrders
    @Orders OrderList READONLY
AS
BEGIN
    INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
    SELECT CustomerId, OrderDate, TotalAmount
    FROM @Orders;
END;

-- SOLUTION 3: Use BULK INSERT from file
BULK INSERT Orders
FROM 'C:\Data\Orders.csv'
WITH (
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '\n',
    BATCHSIZE = 10000,
    TABLOCK  -- Table lock for better performance
);

-- SOLUTION 4: Disable indexes during insert, rebuild after
-- Disable non-clustered indexes
ALTER INDEX ALL ON Orders DISABLE;

-- Perform bulk insert
INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
SELECT ... -- Your bulk insert

-- Rebuild indexes
ALTER INDEX ALL ON Orders REBUILD;

-- SOLUTION 5: Use minimal logging (if recovery model allows)
ALTER DATABASE YourDatabase SET RECOVERY MODEL BULK_LOGGED;

-- Bulk insert with TABLOCK and minimal logging
INSERT INTO Orders WITH (TABLOCK)
SELECT ... -- Your data

ALTER DATABASE YourDatabase SET RECOVERY MODEL FULL;

-- SOLUTION 6: Remove triggers temporarily
DISABLE TRIGGER ALL ON Orders;
-- Perform insert
ENABLE TRIGGER ALL ON Orders;

-- SOLUTION 7: Use bcp utility
-- bcp DatabaseName.dbo.Orders in Orders.csv -c -t, -S ServerName -T

-- SOLUTION 8: Batch in chunks (if single large insert not possible)
DECLARE @BatchSize INT = 5000;
DECLARE @Processed INT = 0;

WHILE @Processed < 100000
BEGIN
    INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
    SELECT 
        @Processed + number,
        GETDATE(),
        100.00
    FROM master.dbo.spt_values
    WHERE type = 'P' 
      AND number BETWEEN 1 AND @BatchSize
      AND @Processed + number <= 100000;
    
    SET @Processed = @Processed + @BatchSize;
END;
```

---

### Scenario 25: Dynamic SQL Injection Risk

**Question:** A stored procedure builds a dynamic SQL query by concatenating user input: `'SELECT * FROM Orders WHERE CustomerName = ''' + @CustomerName + '''` . Security team flagged this as SQL injection risk. How would you fix it while maintaining functionality?

**Answer:**
String concatenation in dynamic SQL allows SQL injection attacks where malicious input can execute arbitrary commands. Solutions include using sp_executesql with parameterized queries (recommended), QUOTENAME for object names, input validation/sanitization, whitelisting valid values, or avoiding dynamic SQL entirely by using conditional logic in static queries for better security.

```sql
-- PROBLEM: SQL injection vulnerability

-- VULNERABLE CODE
CREATE PROCEDURE sp_GetOrders_Vulnerable
    @CustomerName VARCHAR(100)
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = 'SELECT * FROM Orders WHERE CustomerName = ''' + @CustomerName + '''';
    EXEC sp_executesql @SQL;
    -- If @CustomerName = "'; DROP TABLE Orders; --"
    -- Query becomes: SELECT * FROM Orders WHERE CustomerName = ''; DROP TABLE Orders; --'
END;

-- SOLUTION 1: Use parameterized query with sp_executesql (RECOMMENDED)
CREATE PROCEDURE sp_GetOrders_Safe
    @CustomerName VARCHAR(100)
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = N'SELECT * FROM Orders WHERE CustomerName = @CustName';
    
    EXEC sp_executesql 
        @SQL,
        N'@CustName VARCHAR(100)',  -- Parameter definition
        @CustName = @CustomerName;   -- Parameter value (safe)
END;

-- SOLUTION 2: Use QUOTENAME for object names (if building object names dynamically)
CREATE PROCEDURE sp_GetOrdersByTable
    @TableName SYSNAME,  -- SYSNAME is safer for object names
    @CustomerName VARCHAR(100)
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = N'SELECT * FROM ' + QUOTENAME(@TableName) + 
               N' WHERE CustomerName = @CustName';
    
    EXEC sp_executesql 
        @SQL,
        N'@CustName VARCHAR(100)',
        @CustName = @CustomerName;
END;

-- SOLUTION 3: Validate and sanitize input
CREATE PROCEDURE sp_GetOrders_Validated
    @CustomerName VARCHAR(100)
AS
BEGIN
    -- Remove dangerous characters
    SET @CustomerName = REPLACE(@CustomerName, '''', '''''');  -- Escape single quotes
    SET @CustomerName = REPLACE(@CustomerName, ';', '');       -- Remove semicolons
    SET @CustomerName = REPLACE(@CustomerName, '--', '');     -- Remove comments
    SET @CustomerName = REPLACE(@CustomerName, '/*', '');     -- Remove block comments
    SET @CustomerName = REPLACE(@CustomerName, '*/', '');
    
    -- Use parameterized query
    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = N'SELECT * FROM Orders WHERE CustomerName = @CustName';
    
    EXEC sp_executesql 
        @SQL,
        N'@CustName VARCHAR(100)',
        @CustName = @CustomerName;
END;

-- SOLUTION 4: Whitelist validation (if possible)
CREATE PROCEDURE sp_GetOrders_Whitelist
    @CustomerName VARCHAR(100)
AS
BEGIN
    -- Validate against known good values
    IF NOT EXISTS (
        SELECT 1 FROM Customer 
        WHERE CustomerName = @CustomerName
    )
    BEGIN
        RAISERROR('Invalid customer name', 16, 1);
        RETURN;
    END
    
    -- Safe to use directly (no dynamic SQL needed)
    SELECT * FROM Orders 
    WHERE CustomerName = @CustomerName;
END;

-- SOLUTION 5: Use regular stored procedure (avoid dynamic SQL if possible)
CREATE PROCEDURE sp_GetOrders_Simple
    @CustomerName VARCHAR(100)
AS
BEGIN
    SELECT * FROM Orders 
    WHERE CustomerName = @CustomerName;  -- No dynamic SQL needed
END;

-- SOLUTION 6: For complex dynamic WHERE clauses, use conditional logic
CREATE PROCEDURE sp_SearchOrders
    @CustomerName VARCHAR(100) = NULL,
    @OrderDate DATE = NULL,
    @MinAmount DECIMAL(10,2) = NULL
AS
BEGIN
    SELECT * FROM Orders
    WHERE (@CustomerName IS NULL OR CustomerName = @CustomerName)
      AND (@OrderDate IS NULL OR OrderDate = @OrderDate)
      AND (@MinAmount IS NULL OR TotalAmount >= @MinAmount);
    -- No dynamic SQL needed
END;

-- BEST PRACTICES:
-- 1. Always use parameterized queries with sp_executesql
-- 2. Use QUOTENAME for object names
-- 3. Validate and sanitize user input
-- 4. Use least privilege (don't grant unnecessary permissions)
-- 5. Avoid dynamic SQL when possible
-- 6. Use SYSNAME for object name parameters
-- 7. Log suspicious input patterns
```

---

## Summary

This SQL interview preparation guide covers 25 essential topics that every backend developer should master:

**Core Concepts:**
* SQL fundamentals and command types (DDL, DML, DCL, TCL)
* Keys, constraints, and relationships
* Data manipulation operations

**Comparison Topics:**
* DELETE vs TRUNCATE vs DROP
* WHERE vs HAVING
* JOINs (INNER, LEFT, RIGHT, FULL)
* UNION vs UNION ALL
* Clustered vs Non-Clustered Index
* Normalization vs Denormalization
* Stored Procedures vs Functions
* Subquery vs CTE
* Correlated vs Non-Correlated Subqueries

**Advanced Topics:**
* Indexes and performance optimization
* Transactions and ACID properties
* Locks and deadlocks
* Window functions (RANK, DENSE_RANK, ROW_NUMBER)
* Triggers and Views
* Execution plans and performance tuning

**Practical Skills:**
* Finding duplicate records
* Pagination strategies
* NULL handling
* Self joins
* Avoiding N+1 query problem

**Key Takeaways for Interviews:**
01. ? Understand not just "what" but "why" and "when to use"
02. ? Be ready with real-world examples
03. ? Know performance implications of your choices
04. ? Demonstrate optimization mindset
05. ? Practice writing queries on paper/whiteboard

This guide prepares you for SQL-related questions in backend, full-stack, and data engineering interviews. Practice these concepts with real databases to build confidence and muscle memory.

**Good luck with your interviews! ??**
