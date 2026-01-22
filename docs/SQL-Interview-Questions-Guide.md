# SQL Interview Questions - Complete Guide

## Table of Contents

1. [What is SQL?](#1-what-is-sql)
2. [DDL, DML, DCL, and TCL Commands](#2-ddl-dml-dcl-and-tcl-commands)
3. [Primary Key vs Foreign Key](#3-primary-key-vs-foreign-key)
4. [DELETE vs TRUNCATE vs DROP](#4-delete-vs-truncate-vs-drop)
5. [WHERE vs HAVING](#5-where-vs-having)
6. [INNER JOIN vs LEFT JOIN vs RIGHT JOIN vs FULL JOIN](#6-inner-join-vs-left-join-vs-right-join-vs-full-join)
7. [UNION vs UNION ALL](#7-union-vs-union-all)
8. [What are Indexes and Why Use Them?](#8-what-are-indexes-and-why-use-them)
9. [Clustered vs Non-Clustered Index](#9-clustered-vs-non-clustered-index)
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
- ? Columns frequently used in WHERE clauses
- ? Columns used in JOIN conditions
- ? Columns used in ORDER BY or GROUP BY
- ? Foreign key columns
- ? Avoid on tables with frequent INSERT/UPDATE/DELETE
- ? Avoid on columns with low selectivity (few distinct values)

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
- **Clustered Index:** Primary key, unique identifier, frequently used for range queries
- **Non-Clustered Index:** Foreign keys, frequently searched columns, columns in WHERE/JOIN clauses

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
- **Normalization:** OLTP systems, frequent updates, data integrity critical
- **Denormalization:** Data warehouses, reporting systems, read-heavy workloads

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
- **Stored Procedure:** Complex business logic, data modifications, transaction management
- **Function:** Calculations, data transformations, reusable computations in queries

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
1. **READ UNCOMMITTED**: Lowest isolation, allows dirty reads
2. **READ COMMITTED**: Default, prevents dirty reads
3. **REPEATABLE READ**: Prevents non-repeatable reads
4. **SERIALIZABLE**: Highest isolation, prevents phantom reads

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
- ? Keep transactions short
- ? Access tables in same order across transactions
- ? Use appropriate isolation levels
- ? Create proper indexes to reduce lock duration
- ? Avoid user interaction during transactions

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
1. Find duplicates in a specific column
2. Find duplicates based on multiple columns
3. Delete duplicates keeping first/last occurrence
4. Count total number of duplicate records
5. Find records that appear exactly N times

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
- ? Always include ORDER BY with OFFSET
- ? Use indexed columns in ORDER BY for performance
- ? Consider keyset pagination for large datasets
- ? Return total count for UI (calculate total pages)
- ? Avoid OFFSET for very large offsets (slow)

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
1. ? Check for missing indexes using execution plans
2. ? Avoid SELECT * in production code
3. ? Use WHERE clause to limit rows early
4. ? Keep transactions short
5. ? Avoid cursors (use set-based operations)
6. ? Monitor slow query logs
7. ? Regular index maintenance (rebuild/reorganize)

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
- ?? Employee-Manager relationships
- ?? Organizational hierarchies
- ?? Route/network connections
- ?? Comparing rows within same table
- ?? Finding related items (recommendations)

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
- **ROW_NUMBER()**: Use for unique identifiers, pagination
- **RANK()**: Use for Olympic-style rankings (1st, 2nd, 2nd, 4th)
- **DENSE_RANK()**: Use for consecutive rankings (1st, 2nd, 2nd, 3rd)

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
- **AFTER/FOR**: Executes after the triggering operation completes
- **INSTEAD OF**: Replaces the triggering operation
- **DDL**: Responds to schema changes (CREATE, ALTER, DROP)

**Best Practices:**
- ? Keep trigger logic simple and fast
- ? Use for audit trails and critical validations
- ? Avoid complex business logic in triggers
- ? Avoid recursive triggers
- ? Document triggers clearly (they're hidden logic)

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
- ?? Security: Hide sensitive columns
- ?? Simplification: Hide complex JOINs
- ?? Abstraction: Insulate applications from schema changes
- ?? Reusability: Centralize common queries

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
- ? Use JOINs or window functions instead of correlated subqueries when possible
- ? Use EXISTS instead of COUNT(*) in correlated subqueries
- ? Non-correlated subqueries with IN are efficient for small result sets
- ? Avoid correlated subqueries in SELECT list (runs for every row)

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
1. ?? High number of database queries for simple operations
2. ?? Repeated similar queries with only ID parameter changing
3. ?? Slow page load despite simple data display
4. ?? Database connection pool exhaustion

**Prevention:**
- ? Use eager loading (JOINs) instead of lazy loading
- ? Use batch/bulk loading for related data
- ? Monitor and profile database queries
- ? Use ORM query analysis tools
- ? Implement caching for frequently accessed data

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
- ? Use IS NULL / IS NOT NULL for checks (not = NULL)
- ? Use COALESCE for default values
- ? Be aware of NULL behavior in aggregations
- ? Consider NOT NULL constraints where appropriate
- ? Document columns that allow NULL
- ? Avoid nullable foreign keys when possible
- ? Don't use NULL for empty strings ('')

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
1. ? Look for Table Scans ? Add indexes
2. ? Check for Missing Index warnings ? Consider creating them
3. ? Identify Key Lookups ? Create covering indexes
4. ? Review Sort operations ? Add index on ORDER BY columns
5. ? Compare Estimated vs Actual rows ? Update statistics
6. ? Avoid functions on indexed columns
7. ? Ensure correct data types (avoid implicit conversion)
8. ? Use specific columns instead of SELECT *

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

## Summary

This SQL interview preparation guide covers 25 essential topics that every backend developer should master:

**Core Concepts:**
- SQL fundamentals and command types (DDL, DML, DCL, TCL)
- Keys, constraints, and relationships
- Data manipulation operations

**Comparison Topics:**
- DELETE vs TRUNCATE vs DROP
- WHERE vs HAVING
- JOINs (INNER, LEFT, RIGHT, FULL)
- UNION vs UNION ALL
- Clustered vs Non-Clustered Index
- Normalization vs Denormalization
- Stored Procedures vs Functions
- Subquery vs CTE
- Correlated vs Non-Correlated Subqueries

**Advanced Topics:**
- Indexes and performance optimization
- Transactions and ACID properties
- Locks and deadlocks
- Window functions (RANK, DENSE_RANK, ROW_NUMBER)
- Triggers and Views
- Execution plans and performance tuning

**Practical Skills:**
- Finding duplicate records
- Pagination strategies
- NULL handling
- Self joins
- Avoiding N+1 query problem

**Key Takeaways for Interviews:**
1. ? Understand not just "what" but "why" and "when to use"
2. ? Be ready with real-world examples
3. ? Know performance implications of your choices
4. ? Demonstrate optimization mindset
5. ? Practice writing queries on paper/whiteboard

This guide prepares you for SQL-related questions in backend, full-stack, and data engineering interviews. Practice these concepts with real databases to build confidence and muscle memory.

**Good luck with your interviews! ??**
