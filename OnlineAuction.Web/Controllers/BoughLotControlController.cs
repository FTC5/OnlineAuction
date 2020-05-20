using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    public class BoughLotControlController : ApiController
    {
        private IMapper mapper;
        private IBoughLotService boughtLotService;

        public BoughLotControlController(IBoughLotService boughtLotServicee, IMapper mapper)
        {
            this.mapper = mapper;
            this.boughtLotService = boughtLotServicee;
        }
        public void DeleteBought(int userId,int lotId)
        {
            boughtLotService.DeleteBought(userId, lotId);
        }
        public void GetBought(int userId)
        {
            boughtLotService.GetBought(userId);
        }
    }
}