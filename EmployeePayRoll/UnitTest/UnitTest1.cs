using EmployeePay;
using System;
using NUnit.Framework;


namespace UnitTest
{
    public class Tests
    {
        Employee employee;
        EmployeePayRoll employeePayRoll;
        [SetUp]
        public void Setup()
        {
            employeePayRoll = new EmployeePayRoll();
            employee = new Employee();
        }
        /// <summary>
        /// UC1: Inserting Employee Data
        /// </summary>
        [Test]
        public void AddingEmployeeDetails()
        {
            int expected = 1;
            employee.Name = "Captain";
            employee.StartDate = Convert.ToDateTime("2022-11-07");
            employee.Gender = "F";
            employee.PhoneNumber = 9652314523;
            employee.Address = "Texas";
            employee.Department = "Claims";
            employee.BasicPay = 14000;
            employee.Deduction = 5000;
            employee.TaxablePay = 2000;
            employee.IncomeTax = 1100;
            employee.NetPay = 2000;
            var actual = employeePayRoll.InsertEmployeeData(employee);
            Assert.AreEqual(expected, actual);
        }
    }
}