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
   // [Authorize(Roles = "User")]
    public class SubscribeControler : ApiController
    {
        private IMapper mapper;
        private IUserService userService;

        public SubscribeControler(IUserService userService,IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }
        public void Delete(int userId,int lotId)
        {
            userService.DeleteSubscription(userId, lotId);
        }
    }
}