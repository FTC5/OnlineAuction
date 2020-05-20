using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.ExceptionFilters;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
   // [Authorize(Roles = "Manager")]
    public class OldLotControlController : ApiController
    {
        private IMapper mapper;
        private IManagerService managerService;

        public OldLotControlController(IManagerService managerService,IMapper mapper)
        {
            this.managerService = managerService;
            this.mapper = mapper;
        }
        public IHttpActionResult GetOldLot()
        {
            var lots = mapper.Map<IEnumerable<LotViewModel>>(managerService.GetOldLot());
            return Ok(lots);
        }
        [HttpPut, Route("api/stop/{lotId:decimal}"), OperationFaildException, LotNotFoundExaption]
        public IHttpActionResult StopLot(int lotId)
        {
            managerService.StopLot(lotId);
            return Ok();
        }
        [HttpPut, Route("api/continue/{lotId:decimal}"), LotNotFoundExaption]
        public IHttpActionResult ContinueLot(int lotId)
        {
            managerService.ContinueLot(lotId);
            return Ok();
        }
    }
}