using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.BLL.BusinessModels.Interfaces;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.BusinessModels
{
    class CleanService:Service, ICleanService
    {
        public CleanService(IUnitOfWork db) : base(db)
        {
        }

        public void CleanOld()
        {
            int twoWeek = 14;
            DateTime date = DateTime.Now.Date;
            date.AddDays(twoWeek);
            DateTime datebuff;
            var lots = db.Lot.Find(l =>
            {
                datebuff = l.StartDate.AddDays(l.TermDay).Date;
                if (datebuff > date)
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
