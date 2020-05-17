using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.Web.Models
{
    public class BetModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public string UserName { get; set; }
        public int LotId { get; set; }
        public virtual DateTime DateTime { get; set; }
    }
}
