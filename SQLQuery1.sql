--UC1
Create Database payroll_service;
Use payroll_service;
--UC2
Create table employee_payroll
(
ID int Primary Key identity(1,1),
Name VarChar(30),
Salary Bigint,
StartDate Date
)
