using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.ExceptionFilters;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;

namespace OnlineAuction.Web.Controllers
{
    [RoutePrefix("api/lot")]
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
        public IHttpActionResult GetLot(int lotId)
        {
            var lot= mapper.Map<LotModel>(catalogService.GetLot(lotId));
            return Ok(lot);
        }
        [HttpPut, LotNotFoundExaption, UserNotFoundExaption]
        //[HttpPut, Authorize(Roles = "User")]
        public IHttpActionResult SubscribeLot(int lotId,int userId)
        {
            userService.AddLotToSubscription(userId, lotId);
            return Ok();
        }
        [HttpPost, LotNotFoundExaption,UserNotFoundExaption]
        //[HttpPost, Authorize(Roles = "User")]
        public IHttpActionResult AddBet(int lotId, int userId)
        {
            userService.AddBet(lotId,userId);
            return Ok();
        }
        [Route("categories/{id:int}"), HttpGet]
        public IHttpActionResult GetLotAutorInfo(int id)
        {
            var autor = mapper.Map<UserModel>(userService.GetLotAutorInfo(id));
            return Ok(autor);
        }
    }
}