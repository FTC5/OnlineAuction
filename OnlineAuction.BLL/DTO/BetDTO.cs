using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class BetDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public string UserName { get; set; }
        public int LotId { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual LotDTO Lot { get; set; }
    }
}
