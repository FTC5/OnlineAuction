using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Services
{
    public class ModerationService : Service,IModerationService
    {
        public ModerationService(IUnitOfWork db) : base(db)
        {
        }

        public IEnumerable<LotViewDTO> GetUncheckedLots()
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
        public void PreventLot(int lotId, string comment)
        {
            var lot = db.Lot.Get(lotId);
            if (lot == null)
                throw new LotNotFoundExaption("Lot with id= " + lotId + " did not Found");
            if (String.IsNullOrWhiteSpace(comment))
                throw new Infrastructure.ValidationDTOException("Write a comment, namely for what reason the lot was not accepted");
            lot.ModerationResult = false;
            lot.Change = false;
            ConfirmModeration(lot, true, comment);
        }
        private void ConfirmModeration(Lot lot, bool result, string comment)
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
            lot.StartDate = DateTime.Now;
            db.Save();
        }
    }
}
