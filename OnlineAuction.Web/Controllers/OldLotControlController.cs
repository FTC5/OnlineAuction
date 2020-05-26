using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.ExceptionFilters;
using OnlineAuction.Web.Filters;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    [RoutePrefix("api/old/lot"), CookieAuthorization(Role = "Manager")]
    public class OldLotControlController : ApiController
    {
        private IMapper mapper;
        private IManagerService managerService;

        public OldLotControlController(IManagerService managerService,IMapper mapper)
        {
            this.managerService = managerService;
            this.mapper = mapper;
        }
        [HttpGet, Route("get")]
        public async Task<IHttpActionResult> GetOldLot()
        {
            var lots = await Task.Run(() => mapper.Map<IEnumerable<LotViewModel>>(managerService.GetOldLot()));
            return Ok(lots);
        }
        [HttpPut, Route("stop"), OperationFaildException, LotNotFoundExaption]
        public async Task<IHttpActionResult> StopLot(int lotId)//Error
        {
            await Task.Run(() => managerService.StopLot(lotId));
            return Ok();
        }
        [HttpPut, Route("continue"), LotNotFoundExaption]
        public async Task<IHttpActionResult> ContinueLot(int lotId)
        {
            await Task.Run(() => managerService.ContinueLot(lotId));
            return Ok();
        }
    }
}