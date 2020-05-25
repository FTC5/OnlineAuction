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
    public class UserService : Service, IUserService
    {
        IMapper updateMap;
        public UserService(IUnitOfWork db) : base(db)//SubscribeController
        {
            updateMap = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LotDTO, Lot>()
                .ForMember(
                    l => l.Product, p => p.Ignore()
                 )
                 .ForMember(
                    l => l.User, us => us.Ignore()
                 )
                 .ForMember(
                    l => l.Moderation, m => m.Ignore()
                 );

                cfg.CreateMap<ProductDTO, Product>()
                .ForMember(
                    d => d.DeliveryAndPayment, d => d.Ignore()
                 )
                 .ForMember(
                    p => p.Category, c => c.Ignore()
                 )
                 .ForMember(
                    p => p.Images, im => im.Ignore()
                 );
            }).CreateMapper();
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
        public IEnumerable<LotViewDTO> GetSubscription(int userId)//SubscribeController
        {
            var user = db.User.Get(userId);
            if (user == null)
                return null;
            return mapper.Map<IEnumerable<LotViewDTO>>(user.Subscriptions);
        }
        public void DeleteSubscription(int userId,int lotId)//SubscribeController
        {
            var user = db.User.Get(userId);
            if (user == null)
                return;
            var lot = user.Subscriptions.ToList().Find(i => i.Id == lotId);
            user.Subscriptions.Remove(lot);
            db.User.Update(user);
            db.Save();
        }
        public IEnumerable<LotViewDTO> GetUserLot(int userId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                return null;
            return mapper.Map<IEnumerable<LotViewDTO>>(user.UserLots);
        }
        public void UpdateLot(int userId,LotDTO changed)
        {
            if (changed == null)
            {
                return;
            }
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found");
            List<LotDTO> lotDTOs = mapper.Map<List<LotDTO>>(user.UserLots);
            LotDTO lot = lotDTOs.Find(i => (i.Id == changed.Id));
            if (lot == null)
            {
                AddLot(userId, changed);
                return;
            }else if (lot.ModerationResult == true)
            {
                return;
            }
            lot.Change = true;
            var lot1 = db.Lot.Get(changed.Id);
            var product = lot1.Product;
            UpdateImage(changed.Product.Images.ToList(), product.Images.ToList());
            var dap = product.DeliveryAndPayment;
            db.Lot.Update(updateMap.Map(lot, lot1));
            db.Product.Update(updateMap.Map(lot.Product, product));
            db.DeliveryAndPayment.Update(mapper.Map(changed.Product.DeliveryAndPayment, dap));
            db.Save();
        }
        private void UpdateImage(List<ImageDTO> imagesDTO,List<Image> images)
        {
            int size = 0;
            if(imagesDTO.Count< images.Count)
            {
                size = images.Count();
                for (int i = 0; i < size; i++)
                {
                    if (imagesDTO.Count <= i)
                    {
                        db.Image.Delete(images[i].Id);
                    }
                    else
                    {
                        images[i].Link = imagesDTO[i].Link;
                        db.Image.Update(images[i]);
                    }
                }
            }
            else
            {
                size = imagesDTO.Count();
                for (int i = 0; i < size; i++)
                {
                    if (images.Count <= i)
                    {
                        db.Image.Create(mapper.Map<Image>(imagesDTO[i]));
                    }
                    else
                    {
                        images[i].Link = imagesDTO[i].Link;
                        db.Image.Update(images[i]);
                    }
                }
            }
        }
        public void AddLot(int userId, LotDTO lot)
        {
            var results = new List<ValidationResult>();
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(lot);
            if (!Validator.TryValidateObject(lot, context, results, true))
                throw new Infrastructure.ValidationException("Authorization have error", results);

            var user= db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found");
            lot.Moderation = new ModerationDTO();
            var eLot = mapper.Map<Lot>(lot);
            eLot.User = user;
            user.UserLots.Add(eLot);
            eLot.TermDay = 6;
            eLot.StartDate = DateTime.Now.Date;
            db.Lot.Create(eLot);
            db.User.Update(user);
            db.Save();
        }
        public void DeleteLot(int userId, int lotId)
        {
            var lot = db.Lot.Get(lotId);
            if (lot == null)
                return;
            if (lot.UserId != userId)
                return;
            if (lot.StartDate!=DateTime.Now.Date && lot.CurrentPrice!=lot.Product.Price)
                throw new OperationFaildException("Lot cannot be deleted. Bidding has already begun");
            db.Moderation.Delete(lotId);
            db.DeliveryAndPayment.Delete(lotId);
            db.Product.Delete(lotId);
            db.Lot.Delete(lot.Id);
            db.Save();
        }
        public UserDTO GetLotAutorInfo(int lotId)
        {
            var lot = db.Lot.Get(lotId);
            var user = lot.User;
            return mapper.Map<UserDTO>(user);
        }
        public UserDTO GetUserInfo(int userId)
        {
            var user = db.User.Get(userId);
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
                throw new LotNotFoundExaption("Lots by Category not Found");

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
            db.User.Update(user);
            db.Save();
            AddLotToSubscription(userId, lotId);
        }
        public void ChangeLogin(int userId,string newLogin)
        {
            var cheack = db.Authentication.Find(i => i.Login == newLogin);
            if (cheack.Count()>0)
            {
                return;
            }
            var authentication = db.Authentication.Get(userId);
            if (authentication == null)
                throw new UserNotFoundExaption("User does not exist");

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
                throw new UserNotFoundExaption("User does not exist");

            authentication.Password = newPassword;
            db.Authentication.Update(authentication);
            db.Save();
        }
    }
}
