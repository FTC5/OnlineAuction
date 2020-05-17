using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Infrastructure
{
    public class OperationFaildException : Exception
    {
        public OperationFaildException()
        {
        }

        public OperationFaildException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OperationFaildException(string message) : base(message)
        {
        }
    }
}
