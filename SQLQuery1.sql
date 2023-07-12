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
--UC5
Select * from employee_payroll where StartDate between cast('2021-07-16'as Date)and CURRENT_TIMESTAMP;
Alter table employee_payroll add Gender varchar (1);
--UC6
Update employee_payroll set Gender='F' where Name='Sanjana';
Update employee_payroll set Gender='F' where Name='Radha';
Update employee_payroll set Gender='F' where Name='Rutuja';
Update employee_payroll set Gender='F' where Name='Smita';
Update employee_payroll set Gender='F' where Name='Pranali';
--UC7
select SUM(Salary) from employee_payroll where Gender ='F' Group by Gender;
select Gender,AVG(salary) from employee_payroll Group by Gender;
select MIN(Salary) from employee_payroll where Gender='F';
select Max(Salary) from employee_payroll where Gender='F';
Select COUNT(*) from employee_payroll Group by Gender;