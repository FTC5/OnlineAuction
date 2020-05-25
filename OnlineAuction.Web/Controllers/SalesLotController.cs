using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Filters;
using OnlineAuction.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    [RoutePrefix("api/user/sales"), CookieAuthorization(Role = "User")]
    public class SalesLotController : ApiController
    {
        private IMapper mapper;
        private IBoughLotService boughtLotService;
        public SalesLotController(IBoughLotService boughtLotServicee, IMapper mapper)
        {
            this.mapper = mapper;
            this.boughtLotService = boughtLotServicee;
        }
        [HttpDelete,Route("delete")]
        public void DeleteSales(int userId, int lotId)
        {
            boughtLotService.DeleteSales(userId, lotId);
        }
        [HttpGet, Route("get")]
        public IHttpActionResult GetSales(int userId)
        {
            var lots=mapper.Map<IEnumerable<LotViewModel>>(boughtLotService.GetSales(userId));
            return Ok(lots);
        }
    }
}