using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    [RoutePrefix("api/subscribe")]
    public class SubscribeControlController : ApiController
    {
        private IMapper mapper;
        private IUserService userService;
        public SubscribeControlController(IUserService userService, IMapper mapper)
        {
            this.mapper = mapper;
            this.userService = userService;
        }
        [HttpGet,Route("get")]
        public IHttpActionResult GetSubscription(int userId)
        {
            var lots = mapper.Map<IEnumerable<LotViewModel>>(userService.GetSubscription(userId));
            return Ok(lots);
        }
        [HttpDelete, Route("delete")]
        public void DeleteSubscription(int userId,int lotId)
        {
            userService.DeleteSubscription(userId,lotId);
        }
    }
}