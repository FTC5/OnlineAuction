using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;

namespace OnlineAuction.Web.Controllers
{
    public class LotController : ApiController
    {
        private IMapper mapper;
        private ICatalogService catalogService;
        private IUserService userService;
        public LotController(ICatalogService catalogService, IUserService userService)
        {
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper(); 
            this.catalogService = catalogService;
            this.userService = userService;
        }

        public IHttpActionResult GetLot(int lotId)
        {
            var lot= mapper.Map<LotModel>(catalogService.GetLot(lotId));
            return Ok(lot);
        }
        [HttpPut]
        public IHttpActionResult SubscribeLot(int lotId,int userId)
        {
            userService.AddLotTOSubscription(userId, lotId);
            return Ok();
        }
        [HttpPost]
        public IHttpActionResult AddBet(int lotId, int userId)
        {
            userService.AddBet(lotId,userId);
            return Ok();
        }
    }
}