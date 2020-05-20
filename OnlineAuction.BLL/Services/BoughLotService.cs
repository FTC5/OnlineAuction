using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.Services
{
    public class BoughLotService :Service, IBoughLotService///
    {
        public BoughLotService(IUnitOfWork db) : base(db)
        {
        }

        public IEnumerable<LotViewDTO> GetSales(int userId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                return null;
            return mapper.Map<IEnumerable<LotViewDTO>>(user.Sels);
        }
        public void DeleteSales(int userId, int lotId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                return;

            List<LotDTO> lotDTOs = mapper.Map<List<LotDTO>>(user.Sels);
            LotDTO lot = lotDTOs.Find(i => i.Id == lotId);

            if (lot == null)
                return;

            user.Sels.Remove(mapper.Map<Lot>(lot));
            db.User.Update(user);
            db.Save();
        }
        public IEnumerable<LotViewDTO> GetBought(int userId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                return null;
            return mapper.Map<IEnumerable<LotViewDTO>>(user.Bought);
        }
        public void DeleteBought(int userId, int lotId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                return;
            List<LotDTO> lotDTOs = mapper.Map<List<LotDTO>>(user.Bought);
            LotDTO lot = lotDTOs.Find(i => i.Id == lotId);

            if (lot == null)
                return;

            user.Bought.Remove(mapper.Map<Lot>(lot));
            db.User.Update(user);
            db.Save();
        }
    }
}
