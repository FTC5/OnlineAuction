using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL
{
    [Table("User")]
    public class User : Person
    {
        private ICollection<Lot> userLots;
        public virtual ICollection<Lot> UserLots
        {
            get { return userLots ?? (userLots = new List<Lot>()); }
            set { userLots = value; }
        }
        private ICollection<Lot> subscriptions;
        public virtual ICollection<Lot> Subscriptions
        {
            get { return subscriptions ?? (subscriptions = new List<Lot>()); }
            set { subscriptions = value; }
        }
        private ICollection<Bet> bets;
        public virtual ICollection<Bet> Bets
        {
            get { return bets ?? (bets = new List<Bet>()); }
            set { bets = value; }
        }
    }
}
