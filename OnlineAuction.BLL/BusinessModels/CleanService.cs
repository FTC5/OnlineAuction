using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.BLL.Services;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.BusinessModels
{
    public class CleanService :Service, ICleanService
    {
        public CleanService(IUnitOfWork db) : base(db)
        {
        }
        public async void DeleteOldLots()
        {
            int extraDay = 6;
            var lots= await Task.Run(() => GetOldLots(extraDay));
            await Task.Run(() => DeleteBets(lots));
        }
        public IEnumerable<LotDTO> GetOldLots(int extraDay=0)
        {
            DateTime date = DateTime.Now.Date;
            DateTime buff;
            var lots = db.Lot.Find(l =>
            {
                buff = l.StartDate;
                if ((l.TermDay+ extraDay) < (date - buff).Days && l.Sels == false)
                {
                    return true;
                }
                return false;
            });
            return mapper.Map<IEnumerable<LotDTO>>(lots);
        }
        private async void DeleteBets(IEnumerable<LotDTO> lots)
        {
            foreach (var lot in lots)
            {
                foreach (var item in lot.Bets)
                {
                    db.Bet.Delete(item.Id);
                }
                await Task.Run(() => DeleteLot(lot.Id));
            }
        }
        public void DeleteLot(int lotId)
        {
            db.Moderation.Delete(lotId);
            db.DeliveryAndPayment.Delete(lotId);
            db.Product.Delete(lotId);
            db.Lot.Delete(lotId);
            db.Save();
        }
    }
}
