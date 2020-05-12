using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    public class SubscribeControler : ApiController
    {
        private IMapper mapper;
        private IUserService userService;

        public SubscribeControler(IUserService userService)
        {
            this.userService = userService;
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
        }
        public void DeleteSubsribe(int userId,int lotId)
        {
            userService.DeleteSubscription(userId, lotId);
        }
    }
}