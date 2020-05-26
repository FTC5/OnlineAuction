using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Filters;
using OnlineAuction.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    [RoutePrefix("api/subscribe"), CookieAuthorization(Role = "User")]
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
        public async Task<IHttpActionResult> GetSubscription(int userId)
        {
            var lots = await Task.Run(() => mapper.Map<IEnumerable<LotViewModel>>(userService.GetSubscription(userId)));
            return Ok(lots);
        }
        [HttpDelete, Route("delete")]
        public async void DeleteSubscription(int userId,int lotId)
        {
            await Task.Run(() => userService.DeleteSubscription(userId,lotId));
        }
    }
}