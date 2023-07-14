﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Payroll
{

    internal class Operation
    {
        public static string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=payroll_service";
        SqlConnection connection = new SqlConnection(connectionstring);
        List<Employee> list = new List<Employee>();
        List<long> employeeSalary = new List<long>();
        public void GetAllRecords()
        {
            Employee employee = new Employee();
            try
            {
                using (this.connection)
                {
                    string query = @"select * from  employee_payroll";
                    SqlCommand command = new SqlCommand(query, this.connection);
                    command.CommandType = System.Data.CommandType.Text;
                    this.connection.Open();
                    SqlDataReader read = command.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            employee.ID = read.GetInt32(0);
                            employee.Name = read.GetString(1);
                            employee.Salary = read.GetInt64(2);
                            employee.StartDate = read.GetDateTime(3);
                            employee.Gender = read.GetString(4);
                            employee.Phone = read.GetString(5);
                            employee.Address = read.GetString(6);
                            employee.BasicPay = read.GetInt64(7);
                            employee.Deductions = read.GetInt64(8);
                            employee.TaxablePay = read.GetInt64(9);
                            employee.Incometax = read.GetInt64(10);
                            employee.NetPay = read.GetInt64(11);
                            list.Add(employee);
                            employeeSalary.Add(employee.Salary);
                            Console.WriteLine(employee.ID + "\n" + employee.Name + "\n" + employee.Salary + "\n" + employee.StartDate + "\n" + employee.Gender + "\n" + employee.Phone + "\n" + employee.Address + "\n" + employee.BasicPay + "\n" + employee.Deductions + "\n" + employee.TaxablePay + "\n" + employee.Incometax + "\n" + employee.NetPay);
                        }

                    }
                    else
                    {
                        Console.WriteLine("No Records avaible");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void CalulateRecords()
        {
            long sum = 0;
            GetAllRecords();
            var result = list.Where(x => x.Gender.Equals("F")).ToList();
            List<long> salary = new List<long>();
            foreach (var record in result)
            {

                sum = sum + record.Salary;
                salary.Add(record.Salary);
            }
            Console.WriteLine("Sum of salary of female is " + sum);
            Console.WriteLine("AVG of salary of female is " + sum / list.Count);

            Console.WriteLine("Max of salary of female is " + salary.Max());
            Console.WriteLine("Min of salary of female is " + salary.Min());
            Console.WriteLine("count of female is " + list.Count);
        }
        public void AddEmployee(Employee employee)
        {

            using (this.connection)
            {

                SqlCommand command = new SqlCommand("AddEmployee", this.connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@StartDate", employee.StartDate);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Phone", employee.Phone);
                command.Parameters.AddWithValue("@Address", employee.Address);
                command.Parameters.AddWithValue("@BasicPay", employee.BasicPay);
                command.Parameters.AddWithValue("@Deduction", employee.Deductions);
                command.Parameters.AddWithValue("@TaxeblePay", employee.TaxablePay);
                command.Parameters.AddWithValue("@IncomeTax", employee.Incometax);
                command.Parameters.AddWithValue("@NetPay", employee.NetPay);
                this.connection.Open();
                var result = command.ExecuteNonQuery();
                this.connection.Close();
                if (result != 0)
                {
                    Console.WriteLine("Employee Added Sucessfully");
                }
                else
                {
                    Console.WriteLine("Employee Added UnSucessfully");
                }

            }
        }
        public void AddPayRoll()
        {
            Employee employee = new Employee();
            int a = 1;
            GetAllRecords();
            using (this.connection)
            {

                SqlCommand command = new SqlCommand("AddPayRoll", this.connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (var item in employeeSalary)
                {

                    employee.Deductions = (item * 20 / 100);
                    employee.TaxablePay = item - employee.Deductions;
                    employee.Incometax = employee.TaxablePay * 10 / 100;
                    employee.NetPay = item - employee.Incometax;
                    command.Parameters.AddWithValue("@Deduction", employee.Deductions);
                    command.Parameters.AddWithValue("@TaxeblePay", employee.TaxablePay);
                    command.Parameters.AddWithValue("@IncomeTax", employee.Incometax);
                    command.Parameters.AddWithValue("@NetPay", employee.NetPay);
                    command.Parameters.AddWithValue("@EmpID", a);
                    a++;

                }

                this.connection.Open();
                var result1 = command.ExecuteNonQuery();
                this.connection.Close();
                if (result1 != 0)
                {
                    Console.WriteLine("Payroll Added Sucessfully");
                }
                else
                {
                    Console.WriteLine("Payroll Added UnSucessfully");
                }

            }
        }
        public void DeleteEmployee(int id)
        {
            using (this.connection)
            {

                SqlCommand command = new SqlCommand("DeleteEmployee", this.connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                this.connection.Open();
                var result = command.ExecuteNonQuery();
                this.connection.Close();
                if (result != 0)
                {
                    Console.WriteLine("Employee deleted Sucessfully");
                }
                else
                {
                    Console.WriteLine("Employee deleted UnSucessfully");
                }


            }
        }
        public void UpdateEmployee(string name, long BasicPay)
        {
            using (this.connection)
            {

                SqlCommand command = new SqlCommand("UpdateEmpBasicPay", this.connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", name);
                //command.Parameters.AddWithValue("@Salary", Salary);
                command.Parameters.AddWithValue("@BasicPay", BasicPay);
                this.connection.Open();
                var result = command.ExecuteNonQuery();
                this.connection.Close();
                if (result != 0)
                {
                    Console.WriteLine("Employee Updated Sucessfully");
                }
                else
                {
                    Console.WriteLine("Employee Updated  UnSucessfully");
                }


            }

        }
        public void GetEmployeeInParticularRange(DateTime FromDate, DateTime ToDate)
        {
            Employee employee = new Employee();
            try
            {
                using (this.connection)
                {
                    string query = @"select * from  employee_payroll";
                    SqlCommand command = new SqlCommand(query, this.connection);
                    command.CommandType = System.Data.CommandType.Text;
                    this.connection.Open();
                    SqlDataReader read = command.ExecuteReader();

                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            employee.ID = read.GetInt32(0);
                            employee.Name = read.GetString(1);
                            employee.Salary = read.GetInt64(2);
                            employee.StartDate = read.GetDateTime(3);
                            employee.Gender = read.GetString(4);
                            employee.Phone = read.GetString(5);
                            employee.Address = read.GetString(6);
                            employee.BasicPay = read.GetInt64(7);
                            employee.Deductions = read.GetInt64(8);
                            employee.TaxablePay = read.GetInt64(9);
                            employee.Incometax = read.GetInt64(10);
                            employee.NetPay = read.GetInt64(11);
                            if (employee.StartDate >= FromDate && employee.StartDate <= ToDate)
                                Console.WriteLine(employee.ID + "\n" + employee.Name + "\n" + employee.Salary + "\n" + employee.StartDate + "\n" + employee.Gender + "\n" + employee.Phone + "\n" + employee.Address + "\n" + employee.BasicPay + "\n" + employee.Deductions + "\n" + employee.TaxablePay + "\n" + employee.Incometax + "\n" + employee.NetPay);
                        }

                    }
                    else
                    {
                        Console.WriteLine("No Records avaible");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }


}