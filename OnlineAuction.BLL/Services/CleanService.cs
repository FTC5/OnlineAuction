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
                if (time<(date-buff))
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
                db.Lot.Delete(lot.Id);
            }
        }
    }
}
