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
        public DateTime DateTime { get; set; }
        public UserDTO User { get; set; }
        public LotDTO Lot { get; set; }
    }
}
