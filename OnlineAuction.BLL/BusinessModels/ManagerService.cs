using AutoMapper;
using OnlineAuction.BLL.BusinessModels.Interfaces;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.BusinessModels
{
    class ManagerService:Service, IManagerService
    {
        public ManagerService(IUnitOfWork db) : base(db)
        {
        }

        public IEnumerable<LotDTO> GetUnCheackLot()
        {
            var lots = db.Lot.Find(i =>
              {
                  if (i.ModerationResult == false && i.Change == true)
                  {
                      return true;
                  }
                  return false;
              });
            return mapper.Map<IEnumerable<LotDTO>>(lots);
        }
        public void SetModeration(ModerationDTO moderation)
        {
            var lot = db.Lot.Get(moderation.Id);
            if (lot == null)
            {

            }
            else if (moderation.ModerationResult == false && String.IsNullOrEmpty(moderation.Comment) == true)
            {

            }
            else if (moderation.ModerationResult == false)
            {
                lot.ModerationResult = false;
                lot.Change = false;
            }
            else
            {
                lot.ModerationResult = true;
            }
            lot.Change = false;
            var moder= mapper.Map<Moderation>(moderation);
            if (lot.Moderation == null)
            {
                lot.Moderation = moder;
                db.Moderation.Create(moder);
                db.Lot.Update(lot);
            }
            else
            {
                db.Moderation.Update(moder);
                db.Lot.Update(lot);
            }
            db.Save();
        }
        public IEnumerable<LotDTO> GetOldLot()
        {
            DateTime date = DateTime.Now.Date;
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
            return mapper.Map<IEnumerable<LotDTO>>(lots);
        }
        public void StopLot(int LotId)
        {
            var lot = db.Lot.Get(LotId);
            if (lot == null)
            {

            }
            else if(lot.StartDate.AddDays(lot.TermDay).Date<= DateTime.Now.Date)
            {

            }
            db.Lot.Delete(LotId);
            db.Save();
        }
        public void ContinueLot(int LotId)
        {
            var lot = db.Lot.Get(LotId);
            if (lot == null)
            {

            }
            lot.TermDay += 1;
            db.Lot.Update(lot);
            db.Save();
        }

    }
}
