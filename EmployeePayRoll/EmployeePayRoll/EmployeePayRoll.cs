using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRoll
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
    }
}
