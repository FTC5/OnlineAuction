using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    //[Authorize(Roles = "User")]
    public class UserLotsController : ApiController
    {
        private IMapper mapper;
        private IUserService userService;

        public UserLotsController(IUserService userService)
        {
            this.userService = userService;
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
        }
        public void GetLots(int userId)
        {
            var lots = mapper.Map<IEnumerable<LotViewModel>>(userService.GetUserLot(userId));
        }
        public void PostLot(int userId, LotModel lot)
        {
            userService.AddLot(userId, mapper.Map<LotDTO>(lot));
        }
        public void PutLot(int userId, LotModel lot)
        {
            userService.UpdateLot(userId, mapper.Map<LotDTO>(lot));
        }
        public void DeleteLot(int userId, int lotId)
        {
            userService.DeleteLot(userId, lotId);
        }
    }
}