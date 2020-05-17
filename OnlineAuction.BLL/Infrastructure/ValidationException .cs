using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Infrastructure
{
    public class ValidationException : Exception
    {
        List<ValidationResult> errorList;

        public ValidationException()
        {
        }

        public ValidationException(string message,List<ValidationResult> errorList)
        {
            this.errorList = errorList;
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
