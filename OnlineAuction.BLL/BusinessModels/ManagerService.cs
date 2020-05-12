using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.BusinessModels
{
    public class ManagerService :Service, IManagerService
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
                throw new LotNotFoundExaption("Lot not Found", "");

            if (moderation.ModerationResult == false && String.IsNullOrEmpty(moderation.Comment) == true)
                throw new ValidationException("not all fields are filled", "");

            else if (moderation.ModerationResult == false)
            {
                lot.ModerationResult = false;
                lot.Change = false;
            }
            else
            {
                lot.ModerationResult = true;
                lot.Change = true;
            }
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
                if (datebuff > date && l.Sels==false)
                {
                    return true;
                }
                return false;
            });
            return mapper.Map<IEnumerable<LotDTO>>(lots);
        }
        public void StopLot(int lotId)
        {
            var lot = db.Lot.Get(lotId);
            if (lot == null)
            {

            }
            else if(lot.StartDate.AddDays(lot.TermDay).Date<= DateTime.Now.Date)
            {

            }
            if (lot.BetsCount == 0)
            {
                db.Lot.Delete(lotId);
                db.Save();
            }
            else
            {
                lot.Sels = true;
                lot.Change = true;
                lot.ModerationResult = true;
                foreach (var item in lot.Bets)
                {
                    var user = db.User.Get(item.Id);
                    if (user == null)
                    {
                        db.Bet.Delete(item.Id);
                    }
                    user.Subscriptions.Remove(lot);
                    db.User.Update(user);
                    if (item.Price == lot.CurrentPrice)
                    {
                        continue;
                    }
                    db.Bet.Delete(item.Id);
                }
                var sellUser = db.User.Get(lot.UserId);
                sellUser.UserLots.Remove(lot);
                sellUser.Sels.Add(lot);
                db.User.Update(sellUser);
                var boughtUser = db.User.Get(lot.Bets.First().UserId);
                boughtUser.Bought.Add(lot);
                db.User.Update(boughtUser);
                db.Save();
            }

            
        }
        public void ContinueLot(int lotId)
        {
            var lot = db.Lot.Get(lotId);

            if (lot == null)
                throw new LotNotFoundExaption("Lot not Found", "");

            if (lot == null)
            {

            }
            lot.TermDay += 1;
            db.Lot.Update(lot);
            db.Save();
        }

    }
}
