using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class LotDTO
    {
        private ICollection<BetDTO> bets;
        public virtual ICollection<BetDTO> Bets
        {
            get { return bets;  }
            set { bets = value; }
        }
        public int Id { get; set; }
        public int MinimumStroke { get; set; } = 1;
        public int BetsCount { get; set; } = 0;
        public int UserId { get; set; }
        public bool ModerationResult { get; set; } = false;
        public int TermDay { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual ProductDTO Product { get; set; }
        public virtual ModerationDTO Moderation { get; set; }
    }
}
