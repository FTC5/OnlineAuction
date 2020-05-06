using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Infrastructure
{
    class AuthenticationException : Exception
    {
        public string Property { get; protected set; }
        public AuthenticationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
