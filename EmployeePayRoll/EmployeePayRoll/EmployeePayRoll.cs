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
    }
}
