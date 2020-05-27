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
        private ISalesLotService salestLotService;
        public SalesLotController(ISalesLotService salesLotServicee, IMapper mapper)
        {
            this.mapper = mapper;
            this.salestLotService = salesLotServicee;
        }
        [HttpDelete,Route("delete")]
        public void DeleteSales(int userId, int lotId)
        {
            salestLotService.DeleteSales(userId, lotId);
        }
        [HttpGet, Route("get")]
        public IHttpActionResult GetSales(int userId)
        {
            var lots=mapper.Map<IEnumerable<LotViewModel>>(salestLotService.GetSales(userId));
            return Ok(lots);
        }
    }
}