using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    public class SalesLotController : ApiController
    {
        private IMapper mapper;
        private IBoughLotService boughtLotService;
        public SalesLotController(IBoughLotService boughtLotServicee, IMapper mapper)
        {
            this.mapper = mapper;
            this.boughtLotService = boughtLotServicee;
        }
        public void DeleteSales(int userId, int lotId)
        {
            boughtLotService.DeleteSales(userId, lotId);
        }
        public void GetSales(int userId)
        {
            boughtLotService.GetSales(userId);
        }
    }
}