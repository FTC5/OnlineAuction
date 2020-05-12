using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.Web.Models
{
    public class LotModel
    {
        private ICollection<BetModel> bets;
        public virtual ICollection<BetModel> Bets
        {
            get { return bets;  }
            set { bets = value; }
        }
        public int? Id { get; set; }
        public int MinimumStroke { get; set; } = 1;
        public int BetsCount { get; set; } = 0;
        public int CurrentPrice { get; set; }
        public int UserId { get; set; }
        public bool Change { get; set; } = false;
        public bool ModerationResult { get; set; } = false;
        public int TermDay { get; set; }
        public virtual UserModel User { get; set; }
        public virtual ProductModel Product { get; set; }
        public virtual ModerationModel Moderation { get; set; }
    }
}
