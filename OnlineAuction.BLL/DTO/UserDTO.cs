using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class UserDTO:PersonDTO
    {
        private ICollection<LotDTO> userLots;
        public virtual ICollection<LotDTO> UserLots
        {
            get { return userLots; }
            set { userLots = value; }
        }
        private ICollection<LotDTO> subscriptions;
        public virtual ICollection<LotDTO> Subscriptions
        {
            get { return subscriptions; }
            set { subscriptions = value; }
        }
        private ICollection<BetDTO> bets;
        public virtual ICollection<BetDTO> Bets
        {
            get { return bets ; }
            set { bets = value; }
        }
        public int Balance { get; set; } = 0;
    }
}
