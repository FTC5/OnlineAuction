using AutoMapper;
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
    public class UserLotService : Service, IUserLotService
    {
        IMapper updateMap;
        ICleanService cleanService;
        IValidationCheckService validation;
        public UserLotService(IUnitOfWork db, ICleanService cleanService, IValidationCheckService validation) : base(db)
        {
            this.cleanService = cleanService;
            this.validation = validation;
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
                    l => l.Bets, us => us.Ignore()
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

        public IEnumerable<LotViewDTO> GetUserLot(int userId)
        {
            var user = db.User.Get(userId);
            if (user == null)
                return null;
            return mapper.Map<IEnumerable<LotViewDTO>>(user.UserLots);
        }
        public void UpdateLot(int userId, LotDTO changed)
        {
            if (changed == null)
            {
                return;
            }
            var user = db.User.Get(userId);
            if (user == null)
                throw new UserNotFoundExaption("User not found");
            var lots = user.UserLots.ToList();
            var lot = lots.Find(i => (i.Id == changed.Id));
            if (lot == null)
            {
                AddLot(userId, changed);
                return;
            }
            else if (lot.ModerationResult == true)
            {
                return;
            }
            lot.Change = true;
            //var lot1 = db.Lot.Get(changed.Id);
            var product = lot.Product;
            lot.StartDate = DateTime.Now;//
            UpdateImage(changed.Product.Images.ToList(), product.Images.ToList());
            var dap = product.DeliveryAndPayment;
            db.Lot.Update(updateMap.Map(changed, lot));
            db.Product.Update(updateMap.Map(changed.Product, product));
            db.DeliveryAndPayment.Update(mapper.Map(changed.Product.DeliveryAndPayment, dap));
            db.Save();
        }
        private void UpdateImage(List<ImageDTO> imagesDTO, List<Image> images)
        {
            int size = 0;
            if (imagesDTO.Count < images.Count)
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
            var results = validation.Check<LotDTO>(lot); ;
            if (results.Count > 0)
                throw new Infrastructure.ValidationDTOException("Lot have error", results);

            var user = db.User.Get(userId);
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
            {
                return;
            }
            else if (lot.UserId != userId)
            {
                return;
            }
            if (lot.StartDate != DateTime.Now.Date && lot.CurrentPrice != lot.Product.Price)
                throw new OperationFaildException("Lot cannot be deleted. Bidding has already begun");
            cleanService.DeleteLot(lotId);
        }
    }
}
