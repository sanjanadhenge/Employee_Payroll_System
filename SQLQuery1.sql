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
--UC3
Insert into employee_payroll(Name,Salary, StartDate) values('Sanjana','10000','2021-07-15');
Insert into employee_payroll(Name,Salary, StartDate) values('Radha','20000','2021-07-16');
Insert into employee_payroll(Name,Salary, StartDate) values('Rutuja','30000','2021-07-17');
Insert into employee_payroll(Name,Salary, StartDate) values('Smita','40000','2021-07-18');
Insert into employee_payroll(Name,Salary, StartDate) values('Pranali','50000','2021-07-19');
--UC4
Select * from employee_payroll;
