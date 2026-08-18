CREATE DATABASE sql_interview_practice;
-- ============================================================
-- SQL INTERVIEW PRACTICE DATABASE
-- PostgreSQL
-- ============================================================

-- ------------------------------------------------------------
-- 1. CLEANUP
-- ------------------------------------------------------------

DROP TABLE IF EXISTS employee_projects;
DROP TABLE IF EXISTS projects;
DROP TABLE IF EXISTS employees;
DROP TABLE IF EXISTS departments;


-- ------------------------------------------------------------
-- 2. DEPARTMENTS
-- ------------------------------------------------------------

CREATE TABLE departments
(
    department_id   SERIAL PRIMARY KEY,
    department_name VARCHAR(100) NOT NULL,
    location        VARCHAR(100)
);


-- ------------------------------------------------------------
-- 3. EMPLOYEES
-- ------------------------------------------------------------

CREATE TABLE employees
(
    employee_id   SERIAL PRIMARY KEY,
    employee_name VARCHAR(100) NOT NULL,
    email         VARCHAR(150) UNIQUE,
    salary        NUMERIC(12,2),
    department_id INT,
    manager_id    INT,
    job_title     VARCHAR(100),
    hire_date     DATE,
    city          VARCHAR(100),
    bonus         NUMERIC(12,2),
    status        VARCHAR(20),

    CONSTRAINT fk_employee_department
        FOREIGN KEY (department_id)
        REFERENCES departments(department_id),

    CONSTRAINT fk_employee_manager
        FOREIGN KEY (manager_id)
        REFERENCES employees(employee_id)
);


-- ------------------------------------------------------------
-- 4. PROJECTS
-- ------------------------------------------------------------

CREATE TABLE projects
(
    project_id   SERIAL PRIMARY KEY,
    project_name VARCHAR(100) NOT NULL,
    budget       NUMERIC(15,2),
    start_date   DATE,
    end_date     DATE
);


-- ------------------------------------------------------------
-- 5. EMPLOYEE_PROJECTS
-- Many-to-Many relationship
-- ------------------------------------------------------------

CREATE TABLE employee_projects
(
    employee_id INT NOT NULL,
    project_id  INT NOT NULL,
    role        VARCHAR(100),
    assigned_date DATE,

    PRIMARY KEY (employee_id, project_id),

    CONSTRAINT fk_ep_employee
        FOREIGN KEY (employee_id)
        REFERENCES employees(employee_id),

    CONSTRAINT fk_ep_project
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
);


-- ============================================================
-- INSERT DEPARTMENTS
-- ============================================================

INSERT INTO departments
    (department_name, location)
VALUES
    ('IT', 'Pune'),
    ('HR', 'Mumbai'),
    ('Finance', 'Bangalore'),
    ('Sales', 'Delhi'),
    ('Marketing', 'Pune'),
    ('Operations', 'Hyderabad'),
    ('Legal', 'Mumbai');


-- ============================================================
-- INSERT EMPLOYEES
-- ============================================================

INSERT INTO employees
(
    employee_name,
    email,
    salary,
    department_id,
    manager_id,
    job_title,
    hire_date,
    city,
    bonus,
    status
)
VALUES
-- Managers
('Raj Sharma', 'raj@company.com', 120000, 1, NULL,
 'IT Manager', '2018-01-10', 'Pune', 20000, 'Active'),

('Priya Mehta', 'priya@company.com', 110000, 2, NULL,
 'HR Manager', '2019-03-15', 'Mumbai', 15000, 'Active'),

('Amit Verma', 'amit@company.com', 120000, 3, NULL,
 'Finance Manager', '2017-06-20', 'Bangalore', 18000, 'Active'),

('Neha Singh', 'neha@company.com', 100000, 4, NULL,
 'Sales Manager', '2020-01-05', 'Delhi', NULL, 'Active'),

-- IT employees
('Sanjay Kumar', 'sanjay@company.com', 90000, 1, 1,
 'Senior Developer', '2021-02-10', 'Pune', 10000, 'Active'),

('Rahul Patil', 'rahul@company.com', 80000, 1, 1,
 'Developer', '2022-04-12', 'Pune', 8000, 'Active'),

('Sneha Joshi', 'sneha@company.com', 80000, 1, 1,
 'Developer', '2022-07-18', 'Mumbai', NULL, 'Active'),

('Vikram Rao', 'vikram@company.com', 70000, 1, 5,
 'Developer', '2023-01-20', 'Pune', 5000, 'Active'),

('Anjali Deshmukh', 'anjali@company.com', 70000, 1, 5,
 'QA Engineer', '2023-05-25', 'Pune', NULL, 'Active'),

-- HR
('Pooja Shah', 'pooja@company.com', 75000, 2, 2,
 'HR Executive', '2021-09-01', 'Mumbai', 5000, 'Active'),

('Ravi Gupta', 'ravi@company.com', 65000, 2, 2,
 'Recruiter', '2022-11-10', 'Mumbai', NULL, 'Active'),

-- Finance
('Kiran Joshi', 'kiran@company.com', 85000, 3, 3,
 'Senior Accountant', '2020-08-12', 'Bangalore', 7000, 'Active'),

('Meena Rao', 'meena@company.com', 75000, 3, 3,
 'Accountant', '2022-02-15', 'Bangalore', NULL, 'Active'),

('Arjun Das', 'arjun@company.com', 65000, 3, 13,
 'Accountant', '2023-06-10', 'Hyderabad', 3000, 'Active'),

-- Sales
('Rohit Jain', 'rohit@company.com', 90000, 4, 4,
 'Sales Executive', '2021-01-10', 'Delhi', 12000, 'Active'),

('Divya Kapoor', 'divya@company.com', 85000, 4, 4,
 'Sales Executive', '2021-05-15', 'Delhi', 10000, 'Active'),

('Manish Yadav', 'manish@company.com', 75000, 4, 4,
 'Sales Executive', '2022-09-20', 'Jaipur', NULL, 'Active'),

-- Marketing
('Nisha Kulkarni', 'nisha@company.com', 70000, 5, NULL,
 'Marketing Executive', '2022-03-15', 'Pune', 5000, 'Active'),

-- Employee with NULL values for practice
('Vijay More', 'vijay@company.com', NULL, NULL, NULL,
 NULL, NULL, NULL, NULL, 'Inactive');


-- ============================================================
-- INSERT PROJECTS
-- ============================================================

INSERT INTO projects
(
    project_name,
    budget,
    start_date,
    end_date
)
VALUES
('Vehicle Tracking', 5000000, '2024-01-01', NULL),
('Insurance Platform', 3000000, '2024-03-01', NULL),
('HR Portal', 1000000, '2023-06-01', '2024-05-31'),
('Payment Gateway', 4000000, '2024-05-01', NULL),
('Analytics Platform', 2500000, '2024-07-01', NULL);


-- ============================================================
-- EMPLOYEE PROJECT ASSIGNMENTS
-- ============================================================

INSERT INTO employee_projects
(
    employee_id,
    project_id,
    role,
    assigned_date
)
VALUES
(1, 1, 'Project Manager', '2024-01-01'),
(5, 1, 'Tech Lead', '2024-01-01'),
(6, 1, 'Developer', '2024-02-01'),
(7, 1, 'Developer', '2024-02-01'),
(8, 1, 'Developer', '2024-03-01'),

(1, 2, 'Architect', '2024-03-01'),
(5, 2, 'Backend Developer', '2024-03-01'),
(9, 2, 'QA Engineer', '2024-04-01'),

(10, 3, 'HR Lead', '2023-06-01'),
(11, 3, 'Recruiter', '2023-06-01'),

(13, 4, 'Finance Lead', '2024-05-01'),
(15, 4, 'Accountant', '2024-05-01'),

(5, 5, 'Tech Lead', '2024-07-01'),
(6, 5, 'Developer', '2024-07-01');