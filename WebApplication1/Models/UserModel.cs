using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserModel: PersonModel
    {
        private ICollection<LotModel> userLots;
        public virtual ICollection<LotModel> UserLots
        {
            get { return userLots; }
            set { userLots = value; }
        }
        private ICollection<LotModel> subscriptions;
        public virtual ICollection<LotModel> Subscriptions
        {
            get { return subscriptions; }
            set { subscriptions = value; }
        }
        private ICollection<BetModel> bets;
        public virtual ICollection<BetModel> Bets
        {
            get { return bets ; }
            set { bets = value; }
        }
        public int Balance { get; set; } = 0;
    }
}
