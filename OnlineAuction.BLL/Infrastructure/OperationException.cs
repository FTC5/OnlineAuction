using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Infrastructure
{
    class OperationException : Exception
    {
        public string Property { get; protected set; }
        public OperationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
