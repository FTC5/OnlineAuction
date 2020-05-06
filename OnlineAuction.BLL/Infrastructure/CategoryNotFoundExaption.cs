using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Infrastructure
{
    class CategoryNotFoundExaption : Exception
    {
        public string Property { get; protected set; }
        public CategoryNotFoundExaption(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
