using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class UserDTO:PersonDTO
    {
        public int Balance { get; set; } = 0;
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
        private ICollection<LotDTO> sels;
        public virtual ICollection<LotDTO> Sels
        {
            get { return sels ?? (sels = new List<LotDTO>()); }
            set { sels = value; }
        }
        private ICollection<LotDTO> bought;
        public virtual ICollection<LotDTO> Bought
        {
            get { return bought ?? (bought = new List<LotDTO>()); }
            set { bought = value; }
        }
    }
}
