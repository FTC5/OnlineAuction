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
        ICleanService cleanService;
        public ManagerService(IUnitOfWork db,ICleanService cleanService) : base(db)
        {
            this.cleanService = cleanService;
        }
        public IEnumerable<LotViewDTO> GetOldLot()
        {
            return mapper.Map<IEnumerable<LotViewDTO>>(cleanService.GetOldLots());
        }
        public void StopLot(int lotId)
        {
            var lot = db.Lot.Get(lotId);
            if (lot == null)
                throw new LotNotFoundExaption("Lot not Found");
            if(lot.TermDay > (DateTime.Now.Date-lot.StartDate).Days )
                throw new OperationFaildException("Operation Failed : Cant delete not old lot");

            if (lot.BetsCount == 0)
            {
                cleanService.DeleteLot(lotId);
            }
            else
            {
                lot.Sels = true;
                db.Lot.Update(lot);
                db.Save();
                var Bets = lot.Bets.ToList();
                int lastId=0;
                foreach (var item in Bets)
                {
                    var user = db.User.Get(item.Id);
                    if (user == null)
                    {
                        db.Bet.Delete(item.Id);
                        continue;
                    }
                    else
                    {
                        db.Bet.Delete(lastId);
                        lastId = item.Id;
                    }
                    user.Subscriptions.Remove(lot);
                    db.User.Update(user);
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
