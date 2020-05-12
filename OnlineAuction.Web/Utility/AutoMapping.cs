using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.Web.Utility
{
    class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AdvancedUserModel, AdvancedUserDTO>();
            CreateMap<AdvancedUserModel, AdvancedUserDTO>().ReverseMap();
            CreateMap<AuthenticationModel, AuthenticationDTO>();
            CreateMap<AuthenticationModel, AuthenticationDTO>().ReverseMap();
            CreateMap<BetModel, BetDTO>();
            CreateMap<BetModel, BetDTO>().ReverseMap();
            CreateMap<CategoryModel, CategoryDTO>();
            CreateMap<CategoryModel, CategoryDTO>().ReverseMap();
            CreateMap<DeliveryAndPaymentModel, DeliveryAndPaymentDTO>();
            CreateMap<DeliveryAndPaymentModel, DeliveryAndPaymentDTO>().ReverseMap();
            CreateMap<ImageModel, ImageDTO>();
            CreateMap<ImageModel, ImageDTO>().ReverseMap();
            CreateMap<LotModel, LotDTO>();
            CreateMap<LotModel, LotDTO>().ReverseMap();
            CreateMap<ModerationModel, ModerationDTO>();
            CreateMap<ModerationModel, ModerationDTO>().ReverseMap();
            CreateMap<PersonModel, PersonDTO>()
                .Include<UserModel, UserDTO>()
                .Include<AdvancedUserModel,AdvancedUserDTO>(); 
            CreateMap<PersonModel, PersonDTO>().ReverseMap();
            CreateMap<ProductModel,ProductDTO>();
            CreateMap<ProductModel, ProductDTO>().ReverseMap();
            CreateMap<UserModel, UserDTO>();
            CreateMap<UserModel, UserDTO>().ReverseMap();
        }
    }
}
