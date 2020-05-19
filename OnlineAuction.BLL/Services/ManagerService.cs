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
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.BLL.Services
{
    public class ManagerService :Service, IManagerService
    {
        public ManagerService(IUnitOfWork db) : base(db)
        {
        }

        public IEnumerable<LotViewDTO> GetUnCheackLot()
        {
            var lots = db.Lot.Find(i =>
              {
                  if (i.ModerationResult == false && i.Change == true)
                  {
                      return true;
                  }
                  return false;
              });
            return mapper.Map<IEnumerable<LotViewDTO>>(lots);
        }
        public void AllowLot(int lotId)
        {
            var lot = db.Lot.Get(lotId);
            if (lot == null)
                throw new LotNotFoundExaption("Lot with id= " + lotId + " did not Found");
            lot.ModerationResult = true;
            lot.Change = true;
            ConfirmModeration(lot, true, "");
        }
        public void PreventLot(int lotId,string comment)
        {
            var lot = db.Lot.Get(lotId);
            if (lot == null)
                throw new LotNotFoundExaption("Lot with id= " + lotId + " did not Found");
            if (String.IsNullOrWhiteSpace(comment))
                throw new Infrastructure.ValidationException("Write a comment, namely for what reason the lot was not accepted");
            lot.ModerationResult = false;
            lot.Change = false;
            ConfirmModeration(lot, true, comment);
        }
        private void ConfirmModeration(Lot lot,bool result,string comment)
        {
            if (lot.Moderation == null)
            {
                ModerationDTO moderation = new ModerationDTO();
                moderation.ModerationResult = result;
                moderation.Comment = comment;
                var moder = mapper.Map<Moderation>(moderation);
                lot.Moderation = moder;
                db.Moderation.Create(moder);
                db.Lot.Update(lot);
            }
            else
            {
                lot.Moderation.ModerationResult = result;
                lot.Moderation.Comment = comment;
                db.Moderation.Update(lot.Moderation);
                db.Lot.Update(lot);
            }
            db.Save();
        }
        public IEnumerable<LotViewDTO> GetOldLot()
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
            return mapper.Map<IEnumerable<LotViewDTO>>(lots);
        }
        public void StopLot(int lotId)////
        {
            var lot = db.Lot.Get(lotId);
            if (lot == null)
            {
                throw new LotNotFoundExaption("Lot not Found");

            }
            else if(lot.StartDate.AddDays(lot.TermDay).Date<= DateTime.Now.Date)
            {
                throw new OperationFaildException("Operation Failed : Cant delete not old lot");

            }
            if (lot.BetsCount == 0)
            {
                db.Lot.Delete(lotId);
                db.Save();
            }
            else
            {
                lot.Sels = true;
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
                throw new LotNotFoundExaption("Lot not Found");
            lot.TermDay += 1;
            db.Lot.Update(lot);
            db.Save();
        }

    }
}
