﻿using System;

namespace EmployeePay
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Employee Pay Roll Service");
            EmployeePayRoll employeepayroll = new EmployeePayRoll();
            Employee emp = new Employee();
            int option = 0;
            do
            {
                Console.WriteLine("1: Establish Connection");
                Console.WriteLine("2: Close Connection");
                Console.WriteLine("3: Add Employee Data");
                Console.WriteLine("4: Update Employee Data");
                Console.WriteLine("5: Retrieve Employee Data");
                Console.WriteLine("6: Remove Employee Data");
                Console.WriteLine("0: Exit");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        employeepayroll.EstablishConnection();
                        break;
                    case 2:
                        employeepayroll.CloseConnection();
                        break;
                    case 3:
                        Console.WriteLine("Enter The Name");
                        string name = Console.ReadLine();
                        emp.Name = name;
                        Console.WriteLine("Enter a Gender");
                        string gender = Console.ReadLine();
                        emp.Gender = gender;
                        Console.WriteLine("Enter Phone number");
                        double Phone = Convert.ToInt64(Console.ReadLine());
                        emp.PhoneNumber = Phone;
                        Console.WriteLine("Enter a Address");
                        string address = Console.ReadLine();
                        emp.Address = address;
                        Console.WriteLine(" Emplyoee Join Date");
                        string date = Console.ReadLine();
                        emp.StartDate = Convert.ToDateTime(date);
                        Console.WriteLine("Enter a Department");
                        string department = Console.ReadLine();
                        emp.Department = department;
                        Console.WriteLine("Enter a Basic Pay");
                        double basicpay = Convert.ToInt64(Console.ReadLine());
                        emp.BasicPay = basicpay;
                        Console.WriteLine("Enter a Deduction");
                        int Deduction = int.Parse(Console.ReadLine());
                        emp.Deduction = Deduction;
                        Console.WriteLine("Enter a Taxable Pay");
                        int taxablepay = int.Parse(Console.ReadLine());
                        emp.TaxablePay = taxablepay;
                        Console.WriteLine("Enter a Income Tax");
                        int incometax = int.Parse(Console.ReadLine());
                        emp.IncomeTax = incometax;
                        Console.WriteLine("Enter a NetPay");
                        int netpay = int.Parse(Console.ReadLine());
                        emp.NetPay = netpay;
                        employeepayroll.InsertEmployeeData(emp);
                        break;
                    case 4:
                        Console.WriteLine("Enter Id");
                        int id = int.Parse(Console.ReadLine());
                        emp.ID = id;
                        Console.WriteLine("Enter Name");
                        string Name = Console.ReadLine();
                        emp.Name = Name;
                        Console.WriteLine("Enter a Basic Pay");
                        double basic = Convert.ToInt64(Console.ReadLine());
                        emp.BasicPay = basic;
                        employeepayroll.UpdateEmployeeData(emp);
                        break;
                    case 5:
                        var fromDate = Convert.ToDateTime("2013-01-01");
                        var ToDate = Convert.ToDateTime("2022-04-04");
                        employeepayroll.RetrieveData_FromDate_ToDate(fromDate, ToDate);
                        break;
                    case 6:
                        Console.WriteLine("Enter the ID");
                        int Ids = Convert.ToInt32(Console.ReadLine());
                        emp.ID = Ids;
                        employeepayroll.RemoveDetails(emp);
                        break;
                    case 0:
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Invalid Option:---Please Enter Correct Option");
                        break;
                }
            }
            while (option != 0);
        }
    }
}