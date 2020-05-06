using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.BusinessModels
{
    class BoughLotService:Service
    {
        public BoughLotService(IUnitOfWork db) : base(db)
        {
        }

        public IEnumerable<LotDTO> GetSels(int UserId)
        {
            var user = db.User.Get(UserId);
            return mapper.Map<IEnumerable<LotDTO>>(user.Sels);
        }
        public void DeleteSels(int UserId, int LotId)
        {
            var user = db.User.Get(UserId);
            List<LotDTO> lotDTOs = mapper.Map<List<LotDTO>>(user.Sels);
            LotDTO lot = lotDTOs.Find(i => i.Id == LotId);
            user.Sels.Remove(mapper.Map<Lot>(lot));
            lot.Change = false;

            db.User.Update(user);
            db.Save();
        }
        public IEnumerable<LotDTO> GetBought(int UserId)
        {
            var user = db.User.Get(UserId);
            return mapper.Map<IEnumerable<LotDTO>>(user.Bought);
        }
        public void DeleteBought(int UserId, int LotId)
        {
            var user = db.User.Get(UserId);
            List<LotDTO> lotDTOs = mapper.Map<List<LotDTO>>(user.Bought);
            LotDTO lot = lotDTOs.Find(i => i.Id == LotId);
            user.Bought.Remove(mapper.Map<Lot>(lot));
            lot.Change = false;
            
            db.User.Update(user);
            db.Save();
        }
        public void CheakForDel(LotDTO lot)
        {
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
