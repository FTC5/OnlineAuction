using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.Services
{
    public class CleanService :Service, ICleanService
    {
        public CleanService(IUnitOfWork db) : base(db)
        {
        }

        public void CleanOldLots()
        {
            int twoWeek = 14;
            TimeSpan time = new TimeSpan(twoWeek, 0, 0, 0);  
            DateTime date = DateTime.Now.Date;     
            DateTime buff;
            var lots = db.Lot.Find(l =>
            {
                buff = l.StartDate.AddDays(l.TermDay).Date;
                if (time<(buff - date))
                {
                    return true;
                }
                return false;
            });
            foreach (var lot in lots)
            {
                foreach (var item in lot.Bets)
                {
                    db.Bet.Delete(item.Id);
                }
                DeleteLot(lot.Id);
            }
        }
        public void DeleteLot(int lotId)
        {
            db.Moderation.Delete(lotId);
            db.DeliveryAndPayment.Delete(lotId);
            db.Product.Delete(lotId);
            db.Lot.Delete(lotId);
        }
    }
}
