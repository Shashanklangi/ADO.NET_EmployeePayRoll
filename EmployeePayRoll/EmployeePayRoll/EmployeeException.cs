using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRoll
{
    public class EmployeeException : Exception
    {
        public enum ExceptionType
        {
            NO_DATA_FOUND,
            INSERTION_ERROR,
            NO_SUCH_SQL_PROCEDURE,
            CONNECTION_FAILED,
        }
        ExceptionType type;
        /// <summary>
        /// Initialize a new insatnce of the <see cref="EmployeeException"/> class.
        /// </summary>
        /// <param name="type">Type of the exception.</param>param>
        /// <param name="message">The message.</param>
        public EmployeeException(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
