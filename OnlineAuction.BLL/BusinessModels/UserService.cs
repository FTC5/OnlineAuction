using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineAuction.BLL.BusinessModels.Interfaces;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.BusinessModels
{
    class UserService : Service, IUserService
    {
        public UserService(IUnitOfWork db) : base(db)
        {
        }
        public void AddLotTOSubscription(int UserID,int LotID)
        {
            var user = db.User.Get(UserID);
            var lot = db.Lot.Get(LotID);
            user.Subscriptions.Add(lot);
            db.User.Update(user);
            db.Save();
        }
        public IEnumerable<LotDTO> GetSubscription(int UserId)
        {
            var user = db.User.Get(UserId);
            return mapper.Map<IEnumerable<LotDTO>>(user.Subscriptions);
        }
        public void DeleteSubscription(int UserId,int LotId)
        {
            var user = db.User.Get(UserId);
            List<LotDTO> lotDTOs= mapper.Map<List<LotDTO>>(user.Subscriptions);
            LotDTO lot=lotDTOs.Find(i => i.Id == LotId);
            user.Subscriptions.Remove(mapper.Map<Lot>(lot));
            db.User.Update(user);
            db.Save();
        }
        public IEnumerable<LotDTO> GetUserLot(int UserId)
        {
            var user = db.User.Get(UserId);
            return mapper.Map<IEnumerable<LotDTO>>(user.UserLots);
        }
        public void EditLot(int UserId,LotDTO changed)
        {
            var user = db.User.Get(UserId);
            List<LotDTO> lotDTOs = mapper.Map<List<LotDTO>>(user.Subscriptions);
            LotDTO lot = lotDTOs.Find(i => (i.Id == changed.Id && i.UserId==changed.Id));
            if (lot == null)
            {
                return;
            }else if (lot.ModerationResult == true)
            {
                return;
            }
            lot.Change = true;
            db.Lot.Update(mapper.Map<Lot>(lot));
            db.Save();
        }
        public void AddLot(int UserId, LotDTO lot)
        {
            if (lot.CurrentPrice < 1)
            {

            } else if (lot.MinimumStroke < 1)
            {

            } else if (lot.Product == null || lot.Product.Images == null)
            {

            }else if(lot.Product.Category == null)
            {

            }
            ModerationDTO dTO = new ModerationDTO();
            dTO.Lot = lot;
            lot.Moderation = dTO;
            var eLot = mapper.Map<Lot>(lot);
            eLot.User = db.User.Get(UserId);
            eLot.TermDay = 6;
            eLot.StartDate = DateTime.Now.Date;
            var moder= mapper.Map<Moderation>(dTO);
            db.Lot.Create(eLot);
            db.Moderation.Create(moder);
            db.Save();
        }
        public UserDTO GetLotAutorInfo(int LotID)
        {
            var lot = db.Lot.Get(LotID);
            var user = lot.User;
            return mapper.Map<UserDTO>(user);
        }
        public void AddBalance(int UserId, int count)
        {
            var user = db.User.Get(UserId);
            user.Balance += count;
            db.User.Update(user);
            db.Save();
        }
        public void AddBet(int LotId,int UserId)
        {
            var lot = db.Lot.Get(LotId);
            int newPrice = lot.CurrentPrice + lot.MinimumStroke;
            var user = db.User.Get(UserId);
            if (user.Balance < newPrice)
            {
                return;
            }
            BetDTO betDTO = new BetDTO();
            betDTO.DateTime = DateTime.Now;
            betDTO.Price = newPrice;
            lot.CurrentPrice = newPrice;
            lot.BetsCount += 1;
            var bet = mapper.Map<Bet>(betDTO);
            bet.Lot = lot;
            bet.User = user;
            lot.Bets.Add(bet);
            db.Bet.Create(bet);
            db.Lot.Update(lot);
            db.Save();
        }
        public void ChangeLogin(int UserId,string newLogin)
        {
            var cheack = db.Authentication.Find(i => i.Login == newLogin);
            if (cheack!=null)
            {
                return;
            }
            var authentication = db.Authentication.Get(UserId);
            authentication.Login = newLogin;
            db.Authentication.Update(authentication);
            db.Save();
        }
        public void ChangePassword(int UserId, string newPassword)
        {
            if (newPassword.Length<8)
            {
                return;
            }
            var authentication = db.Authentication.Get(UserId);
            authentication.Password = newPassword;
            db.Authentication.Update(authentication);
            db.Save();
        }
    }
}
