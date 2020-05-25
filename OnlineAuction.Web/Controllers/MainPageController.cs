using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    public class MainPageController:ApiController
    {
        private IMapper mapper;
        private ICatalogService catalogService;

        public MainPageController(ICatalogService catalogService,IMapper mapper)
        {
            this.mapper = mapper;
            this.catalogService = catalogService;
        }
        public IHttpActionResult GetLot(int lotId)
        {
            var lot = mapper.Map<LotModel>(catalogService.GetLot(lotId));
            return Ok(lot);
        }

    }
}