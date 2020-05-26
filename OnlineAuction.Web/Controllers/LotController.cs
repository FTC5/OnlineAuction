using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.ExceptionFilters;
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
    public class LotController : ApiController
    {
        private IMapper mapper;
        private ICatalogService catalogService;
        private IUserService userService;
        public LotController(ICatalogService catalogService, IUserService userService,IMapper mapper)
        {
            this.mapper = mapper;
            this.catalogService = catalogService;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetLot(int lotId)
        {
            var lot= await Task.Run(() => mapper.Map<LotModel>(catalogService.GetLot(lotId)));
            return Ok(lot);
        }
        [HttpPut, CookieAuthorization(Role = "User"), LotNotFoundExaption, UserNotFoundExaption]
        public async Task<IHttpActionResult> SubscribeLot(int lotId,int userId)
        {
            await Task.Run(() => userService.AddLotToSubscription(userId, lotId));
            return Ok();
        }
        [HttpPost,CookieAuthorization(Role = "User"), LotNotFoundExaption,UserNotFoundExaption]
        public async Task<IHttpActionResult> AddBet(int lotId, int userId)
        {
            await Task.Run(() => userService.AddBet(lotId,userId));
            return Ok();
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAutorInfo(int id)
        {
            var autor = await Task.Run(() => mapper.Map<UserModel>(userService.GetLotAutorInfo(id)));
            return Ok(autor);
        }
    }
}