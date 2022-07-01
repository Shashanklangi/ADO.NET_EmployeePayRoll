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
        /// <summary>
        /// UC2: Updating Employee Data
        /// </summary>
        [Test]
        public void UpdateDetails()
        {
            employee.ID = 3;
            employee.Name = "Smith";
            employee.BasicPay = 58000;
            var result = employeePayRoll.UpdateEmployeeData(employee);
            var expected = result.BasicPay;
            var actual = employee.BasicPay;
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// UC3: Retrieves Employee Data
        /// </summary>
        [Test]
        public void RetrievingData_In_DateRange()
        {
            var fromDate = Convert.ToDateTime("2013-01-01");
            var toDate = Convert.ToDateTime("2022-10-01");
            var result = employeePayRoll.RetrieveData_FromDate_ToDate(fromDate, toDate);
            var expected = result.Count;
            Assert.AreEqual(expected, result.Count);
        }
        //<summary>
        //TC 5 : Remove Details
        //</summary>
        [Test]
        public void RemovingEmployeeDetails()
        {
            bool expected = true;
            employee.ID = 5;
            bool result = employeePayRoll.RemoveDetails(employee);
            Assert.AreEqual(expected, result);
        }
    }
}