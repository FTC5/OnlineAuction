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

        public ValidationException(string message,List<ValidationResult> errorList) : base(message)
        {
            this.errorList = errorList;
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override string ToString()
        {
            string text = "\n";
            for (int i = 0; i <errorList.Count; i++)
            {
                text += errorList[i].ErrorMessage;
            }
            return text;
        }
    }
}
