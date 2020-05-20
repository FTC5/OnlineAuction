using AutoMapper;
using OnlineAuction.BLL.DTO;
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
    //[Authorize(Roles = "Manager")]
    public class LotModerationController : ApiController //Error
    {
        private IMapper mapper;
        private IManagerService managerService;

        public LotModerationController(IManagerService managerService,IMapper mapper)
        {
            this.mapper = mapper;
            this.managerService = managerService;
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
        }
        [HttpGet]
        public IHttpActionResult GetUncheckedLots()
        {
            var lots = mapper.Map<IEnumerable<LotViewModel>>(managerService.GetUncheckedLots());
            return Ok(lots);
        }
        [HttpPut,LotNotFoundExaption]
        public IHttpActionResult AllowLot(int lotId)
        {
            managerService.AllowLot(lotId);
            return Ok();
        }
        [HttpPost,LotNotFoundExaption, ValidationException]
        public IHttpActionResult PreventLot(int lotId,string cause) 
        {
            managerService.PreventLot(lotId,cause);
            return Ok();
        }
    }
}