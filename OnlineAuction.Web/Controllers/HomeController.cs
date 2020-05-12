using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OnlineAuction.Web.Controllers
{
    public class HomeController : ApiController
    {
        private IMapper mapper;
        private ICatalogService catalogService;

        public HomeController(ICatalogService catalogService)
        {
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
            this.catalogService = catalogService;
        }
        public IHttpActionResult GetLot(int lotId)
        {
            var lot = mapper.Map<LotModel>(catalogService.GetLot(lotId));
            return Ok(lot);
        }
    }
}
