using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Infrastructure
{
    class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AdvancedUser, AdvancedUserDTO>();
            CreateMap<AdvancedUser, AdvancedUserDTO>().ReverseMap();
            CreateMap<Authentication, AuthenticationDTO>();
            CreateMap<Authentication, AuthenticationDTO>().ReverseMap();
            CreateMap<Bet, BetDTO>();
            CreateMap<Bet, BetDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<DeliveryAndPayment, DeliveryAndPaymentDTO>();
            CreateMap<DeliveryAndPayment, DeliveryAndPaymentDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<Lot, LotDTO>();
            CreateMap<Lot, LotDTO>().ReverseMap();
            CreateMap<Moderation, ModerationDTO>();
            CreateMap<Moderation, ModerationDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>()
                .Include<User, UserDTO>()
                .Include<AdvancedUser,AdvancedUserDTO>(); 
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Product,ProductDTO>();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
