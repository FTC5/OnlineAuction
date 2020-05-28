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
        public async Task<IHttpActionResult> DeleteSales(int userId, int lotId)
        {
            await Task.Run(() => salestLotService.DeleteSales(userId, lotId));
            return Ok();
        }
        [HttpGet, Route("get")]
        public async Task<IHttpActionResult> GetSales(int userId)
        {
            var lots= await Task.Run(() => mapper.Map<IEnumerable<LotViewModel>>(salestLotService.GetSales(userId)));
            return Ok(lots);
        }
    }
}