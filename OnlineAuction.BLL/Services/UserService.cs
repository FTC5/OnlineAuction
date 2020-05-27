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
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.BLL.Services
{
    public class UserService : Service, IUserService, ISubscriptionService
    {
        public UserService(IUnitOfWork db) : base(db)
        {
            
        }
        public void AddLotToSubscription(int userId,int lotId)
        {
            var user = db.User.Get(userId);
            var lot = db.Lot.Get(lotId);

            if (lot == null)
                throw new LotNotFoundExaption("Lot not found"); 
            if (user == null)
                throw new UserNotFoundExaption("User not found");

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
                return null;
            return mapper.Map<IEnumerable<LotViewDTO>>(user.Subscriptions);
        }
        public void DeleteSubscription(int userId,int lotId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                return;
            var lot = user.Subscriptions.ToList().Find(i => i.Id == lotId);
            user.Subscriptions.Remove(lot);
            db.User.Update(user);
            db.Save();
        }
        public UserDTO GetLotAutorInfo(int lotId)
        {
            var lot = db.Lot.Get(lotId);
            var user = lot.User;
            return mapper.Map<UserDTO>(user);
        }
        public void AddBalance(int userId, int count)
        {
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("Cannot find user");
            user.Balance += count;
            db.User.Update(user);
            db.Save();
        }
        public void AddBet(int lotId,int userId)
        {
            var lot = db.Lot.Get(lotId);
            if (lot == null)
                throw new LotNotFoundExaption("Lots not Found");

            int newPrice = lot.CurrentPrice + lot.MinimumStroke;
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found");
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
            AddLotToSubscription(userId, lotId);
        }
    }
}
