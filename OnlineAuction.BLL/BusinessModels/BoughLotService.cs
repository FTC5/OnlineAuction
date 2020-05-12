using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.BusinessModels
{
    public class BoughLotService :Service
    {
        public BoughLotService(IUnitOfWork db) : base(db)
        {
        }

        public IEnumerable<LotDTO> GetSels(int userId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");
            return mapper.Map<IEnumerable<LotDTO>>(user.Sels);
        }
        public void DeleteSels(int userId, int lotId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");

            List<LotDTO> lotDTOs = mapper.Map<List<LotDTO>>(user.Sels);
            LotDTO lot = lotDTOs.Find(i => i.Id == lotId);

            if (lot == null)
                throw new LotNotFoundExaption("Lot not Found", "");

            user.Sels.Remove(mapper.Map<Lot>(lot));
            lot.Change = false;

            db.User.Update(user);
            db.Save();
        }
        public IEnumerable<LotDTO> GetBought(int userId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");
            return mapper.Map<IEnumerable<LotDTO>>(user.Bought);
        }
        public void DeleteBought(int userId, int lotId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");
            List<LotDTO> lotDTOs = mapper.Map<List<LotDTO>>(user.Bought);
            LotDTO lot = lotDTOs.Find(i => i.Id == lotId);

            if (lot == null)
                throw new LotNotFoundExaption("Lot not Found", "");

            user.Bought.Remove(mapper.Map<Lot>(lot));
            lot.Change = false;
            
            db.User.Update(user);
            db.Save();
        }
        public void CheakForDel(LotDTO lot)
        {
            if (lot == null)
            {
                return;
            }
            if(lot.BetsCount==0 && lot.Change == false)
            {
                db.Lot.Delete(lot.Id);
            }
            else
            {
                db.Lot.Update(mapper.Map<Lot>(lot));

            }

        }
    }
}
