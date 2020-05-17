using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Infrastructure
{
    public class LotNotFoundExaption : Exception
    {
        public LotNotFoundExaption()
        {
        }

        public LotNotFoundExaption(string message) : base(message)
        {
        }

        public LotNotFoundExaption(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
