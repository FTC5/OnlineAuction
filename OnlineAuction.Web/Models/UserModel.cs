using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.Web.Models
{
    public class UserModel: PersonModel
    {
        public int Balance { get; set; } = 0;
        //private ICollection<LotModel> userLots;
        //public virtual ICollection<LotModel> UserLots
        //{
        //    get { return userLots; }
        //    set { userLots = value; }
        //}
        //private ICollection<LotModel> subscriptions;
        //public virtual ICollection<LotModel> Subscriptions
        //{
        //    get { return subscriptions; }
        //    set { subscriptions = value; }
        //}
        //private ICollection<LotModel> sels;
        //public virtual ICollection<LotModel> Sels
        //{
        //    get { return sels ?? (sels = new List<LotModel>()); }
        //    set { sels = value; }
        //}
        //private ICollection<LotModel> bought;
        //public virtual ICollection<LotModel> Bought
        //{
        //    get { return bought ?? (bought = new List<LotModel>()); }
        //    set { bought = value; }
        //}
    }
}
