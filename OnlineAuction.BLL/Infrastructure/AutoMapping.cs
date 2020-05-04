using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Exceptions
{
    class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AdvancedUser, AdvancedUserDTO>();
            CreateMap<Authentication, AuthenticationDTO>();
            CreateMap<Bet, BetDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<DeliveryAndPayment, DeliveryAndPaymentDTO>();
            CreateMap<Image, ImageDTO>();
            CreateMap<Lot, LotDTO>();
            CreateMap<Moderation, ModerationDTO>();
            CreateMap<Person, PersonDTO>();
            CreateMap<Product,ProductDTO>();
            CreateMap<User,UserDTO>();
        }
    }
}
