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
    }
}
