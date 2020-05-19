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
            CreateMap<Bet, BetDTO>()
                .ForMember(bd => bd.UserName, opt => opt.MapFrom(b => b.User.FirstName + " " + b.User.LastName));
            CreateMap<BetDTO, Bet>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<DeliveryAndPayment, DeliveryAndPaymentDTO>();
            CreateMap<DeliveryAndPayment, DeliveryAndPaymentDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<LotDTO, Lot>();
            CreateMap<Lot, LotDTO>()
                .ForMember(bd => bd.UserName, opt => opt.MapFrom(b => b.User.FirstName + " " + b.User.LastName));
            CreateMap<Moderation, ModerationDTO>();
            CreateMap<Moderation, ModerationDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>()
                .Include<User, UserDTO>()
                .Include<AdvancedUser,AdvancedUserDTO>(); 
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Product,ProductDTO>();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<UserDTO, PersonDTO>();
            CreateMap<UserDTO, PersonDTO>().ReverseMap();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<AdvancedUserDTO, PersonDTO>();
            CreateMap<AdvancedUserDTO, PersonDTO>().ReverseMap();
            CreateMap<Lot, LotViewDTO>()
                .ForMember(ld => ld.UserName, opt => opt.MapFrom(l => l.User.FirstName + " " + l.User.LastName))
                .ForMember(ld => ld.Name, opt => opt.MapFrom(l => l.Product.Name))
                .ForMember(ld => ld.Image, opt => opt.MapFrom(l => l.Product.Images.First()))
                .ForMember(ld => ld.Category, opt => opt.MapFrom(l => l.Product.Category));

        }
    }
}
