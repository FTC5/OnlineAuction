using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.Services
{
    public class UserService : Service, IUserService
    {
        public UserService(IUnitOfWork db) : base(db)
        {
        }
        public void AddLotTOSubscription(int userId,int lotId)
        {
            var user = db.User.Get(userId);
            var lot = db.Lot.Get(lotId);

            if (lot == null)
                throw new LotNotFoundExaption("Lot not found", ""); 
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");

            if (user.Subscriptions.Contains(lot))
            {
                return;
            }
            user.Subscriptions.Add(lot);
            db.User.Update(user);
            db.Save();
        }
        public IEnumerable<LotViewDTO> GetSubscription(int userId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");
            return mapper.Map<IEnumerable<LotViewDTO>>(user.Subscriptions);
        }
        public void DeleteSubscription(int userId,int lotId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");
            List<LotDTO> lotDTOs= mapper.Map<List<LotDTO>>(user.Subscriptions);
            LotDTO lot=lotDTOs.Find(i => i.Id == lotId);
            user.Subscriptions.Remove(mapper.Map<Lot>(lot));
            db.User.Update(user);
            db.Save();
        }
        public IEnumerable<LotViewDTO> GetUserLot(int userId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");
            return mapper.Map<IEnumerable<LotViewDTO>>(user.UserLots);
        }
        public void UpdateLot(int userId,LotDTO changed)
        {
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");
            List<LotDTO> lotDTOs = mapper.Map<List<LotDTO>>(user.Subscriptions);
            LotDTO lot = lotDTOs.Find(i => (i.Id == changed.Id && i.UserId==changed.Id));
            if (lot == null)
                throw new LotNotFoundExaption("Lot not found", "");
            if (lot.ModerationResult == true)
            {
                return;
            }
            lot.Change = true;
            db.Lot.Update(mapper.Map<Lot>(lot));
            db.Save();
        }
        public void AddLot(int userId, LotDTO lot)
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
            var user= db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");
            var eLot = mapper.Map<Lot>(lot);
            eLot.User = user;
            user.UserLots.Add(eLot);
            eLot.TermDay = 6;
            eLot.StartDate = DateTime.Now.Date;

            //ModerationDTO dTO = new ModerationDTO();
            //dTO.Lot = lot;
            //lot.Moderation = dTO;
            //var moder = mapper.Map<Moderation>(dTO);

            db.Lot.Create(eLot);
            db.User.Update(user);
            //db.Moderation.Create(moder);
            db.Save();
        }
        public void DeleteLot(int userId, int lotId)
        {
            var lot = db.Lot.Get(lotId);
            if(lot==null)
                throw new UserNotFoundExaption("User not found", "");
            if (lot.UserId != userId)
                throw new OperationException("", "");
            if(lot.StartDate!=DateTime.Now.Date && lot.CurrentPrice!=lot.Product.Price)
                throw new OperationException("", "");
            db.Lot.Delete(lot.Id);
            db.Save();
        }
        public UserDTO GetLotAutorInfo(int lotId)
        {
            var lot = db.Lot.Get(lotId);
            var user = lot.User;
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");
            return mapper.Map<UserDTO>(user);
        }
        public void AddBalance(int userId, int count)
        {
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");
            user.Balance += count;
            db.User.Update(user);
            db.Save();
        }
        public void AddBet(int lotId,int userId)
        {
            var lot = db.Lot.Get(lotId);
            if (lot == null)
                throw new LotNotFoundExaption("Lots by Category not Found", "");

            int newPrice = lot.CurrentPrice + lot.MinimumStroke;
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found", "");
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
            //user.Bets.Add(bet);
            lot.Bets.Add(bet);
            db.Bet.Create(bet);
            db.Lot.Update(lot);
            db.User.Update(user);
            db.Save();
            AddLotTOSubscription(lotId, userId);
        }
        public void ChangeLogin(int userId,string newLogin)
        {
            var cheack = db.Authentication.Find(i => i.Login == newLogin);
            if (cheack!=null)
            {
                return;
            }
            var authentication = db.Authentication.Get(userId);
            if (authentication == null)
                throw new UserNotFoundExaption("User does not exist", "");

            authentication.Login = newLogin;
            db.Authentication.Update(authentication);
            db.Save();
        }
        public void ChangePassword(int userId, string newPassword)
        {
            if (newPassword.Length<8)
            {
                return;
            }
            var authentication = db.Authentication.Get(userId);
            if (authentication == null)
                throw new UserNotFoundExaption("User does not exist", "");

            authentication.Password = newPassword;
            db.Authentication.Update(authentication);
            db.Save();
        }
    }
}
