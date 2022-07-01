using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePay
{
    public class EmployeePayRoll
    {
        /// <summary>
        /// UC1:Check connection is established or not
        /// </summary>
        static string connectionstring = @"Data source=(localdb)\MSSQLLocalDB;Initial Catalog=EmployeeServices;Integrated Security=SSPI";
        static SqlConnection connection = new SqlConnection(connectionstring);

        public void EstablishConnection()
        {
            if (connection != null && connection.State.Equals(ConnectionState.Closed))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    throw new EmployeeException(EmployeeException.ExceptionType.CONNECTION_FAILED, "Connection Not Found");
                }
            }
        }
        public void CloseConnection()
        {
            if (connection != null && connection.State.Equals(ConnectionState.Open))
            {
                try
                {
                    connection.Close();
                }
                catch (Exception)
                {
                    throw new EmployeeException(EmployeeException.ExceptionType.CONNECTION_FAILED, "Connection Not Found");
                }
            }
        }
        /// <summary>
        /// UC2:Inserting Employee Details 
        /// </summary>
        public int InsertEmployeeData(Employee employee)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertEmployeeDetails", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ID", employee.ID);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@BasicPay", employee.BasicPay);
                    cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", employee.Address);
                    cmd.Parameters.AddWithValue("@Deduction", employee.Deduction);
                    cmd.Parameters.AddWithValue("@TaxablePay", employee.TaxablePay);
                    cmd.Parameters.AddWithValue("@IncomeTax", employee.IncomeTax);
                    cmd.Parameters.AddWithValue("@NetPay", employee.NetPay);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    var returnParameter = cmd.Parameters.Add("@new_identity", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    var result = returnParameter.Value;
                    return (int)result;
                }
            }
            catch (Exception)
            {
                throw new EmployeeException(EmployeeException.ExceptionType.INSERTION_ERROR, "Insertion error");
            }
        }
        /// <summary>
        /// UC3:Updating Employee Details 
        /// </summary>
        public Employee UpdateEmployeeData(Employee employee)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("UpdateEmployeeDetails", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", employee.ID);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@BasicPay", employee.BasicPay);
                    employee = new Employee();
                    connection.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            employee.ID = (int)rd["ID"];
                            employee.Name = (string)rd["Name"];
                            employee.Gender = (string)rd["Gender"];
                            employee.PhoneNumber = (Int64)rd["PhoneNumber"];
                            employee.Address = (string)rd["Address"];
                            employee.StartDate = (DateTime)rd["StartDate"];
                            employee.Department = (string)rd["Department"];
                            employee.BasicPay = (Int32)rd["BasicPay"];
                            employee.Deduction = (Int32)rd["Deduction"];
                            employee.TaxablePay = (Int32)rd["TaxablePay"];
                            employee.IncomeTax = (Int32)rd["IncomeTax"];
                            employee.NetPay = (Int32)rd["NetPay"];

                        }
                        if (employee == null)
                        {
                            throw new EmployeeException(EmployeeException.ExceptionType.NO_DATA_FOUND, "Data Not Found");
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new EmployeeException(EmployeeException.ExceptionType.NO_DATA_FOUND, "Data not found");
            }
            return employee;
        }
        /// <summary>
        /// UC4:Retrieve Employee Details 
        /// </summary>
        public List<Employee> RetrieveData_FromDate_ToDate(DateTime fromDate, DateTime toDate)
        {
            Employee employee;
            List<Employee> employeeList = new List<Employee>();

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand("DateAndTime", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                connection.Open();
                using (connection)
                {
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@toDate", toDate);
                    SqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        employee = new Employee
                        {
                            ID = rd.IsDBNull(0) ? default : rd.GetInt32(0),
                            Name = rd.IsDBNull(1) ? default : rd.GetString(1),
                            PhoneNumber = rd.IsDBNull(2) ? default : rd.GetInt64(2),
                            Address = rd.IsDBNull(3) ? default : rd.GetString(3),
                            Department = rd.IsDBNull(4) ? default : rd.GetString(4),
                            Gender = rd.IsDBNull(5) ? default : rd.GetString(5),
                            BasicPay = rd.IsDBNull(6) ? default : rd.GetInt32(6),
                            Deduction = rd.IsDBNull(7) ? default : rd.GetInt32(7),
                            TaxablePay = rd.IsDBNull(8) ? default : rd.GetInt32(8),
                            IncomeTax = rd.IsDBNull(9) ? default : rd.GetInt32(9),
                            NetPay = rd.IsDBNull(10) ? default : rd.GetInt32(10),
                            StartDate = rd.IsDBNull(11) ? default : rd.GetDateTime(11)

                        };
                        Console.WriteLine(employee.ID + "," + employee.Name + "," + employee.PhoneNumber + "," + employee.Address + "," + employee.Department + "," + employee.Gender + "," + employee.BasicPay + "," + employee.Deduction + "," + employee.TaxablePay + "," + employee.IncomeTax + "," + employee.NetPay + "," + employee.StartDate + ",");
                        employeeList.Add(employee);
                    }
                    return employeeList;
                }
            }
            catch (Exception)
            {
                throw new EmployeeException(EmployeeException.ExceptionType.NO_DATA_FOUND, "Data not found");
            }
            return employeeList;

        }
        public bool RemoveDetails(Employee employee)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("RemoveEmpDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", employee.ID);
                    connection.Open();
                    var result = command.ExecuteNonQuery();

                    if (result != 0)
                    {
                        Console.WriteLine("Contact is deleted");
                        return true;
                    }
                    return false;
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new EmployeeException(EmployeeException.ExceptionType.NO_DATA_FOUND, "Data not found");
            }
        }
    }
}
