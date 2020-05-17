using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Infrastructure
{
    public class UserNotFoundExaption : Exception
    {
        public UserNotFoundExaption()
        {
        }

        public UserNotFoundExaption(string message) : base(message)
        {
            
        }

        public UserNotFoundExaption(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
