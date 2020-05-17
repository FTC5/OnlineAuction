using AutoMapper;
using OnlineAuction.BLL.Interfaces;
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

        public OldLotControlController(IManagerService managerService)
        {
            this.managerService = managerService;
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetOldLot()
        {
            var lots = mapper.Map<IEnumerable<LotViewModel>>(managerService.GetOldLot());
            return Ok(lots);
        }
        [HttpPut, Route("api/stop/{lotId:decimal}")]
        public IHttpActionResult StopLot(int lotId)
        {
            managerService.StopLot(lotId);
            return Ok();
        }
        [HttpPut, Route("api/continue/{lotId:decimal}")]
        public IHttpActionResult ContinueLot(int lotId)
        {
            managerService.ContinueLot(lotId);
            return Ok();
        }
    }
}