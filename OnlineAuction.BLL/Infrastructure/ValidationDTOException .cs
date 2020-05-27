using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Infrastructure
{
    public class ValidationDTOException : Exception
    {
        List<ValidationResult> errorList;

        public ValidationDTOException()
        {
        }

        public ValidationDTOException(string message,List<ValidationResult> errorList) : base(message)
        {
            this.errorList = errorList;
        }

        public ValidationDTOException(string message) : base(message)
        {
        }

        public ValidationDTOException(string message, Exception innerException) : base(message, innerException)
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
