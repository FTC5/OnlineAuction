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
    [CookieAuthorization(Role = "User")]
    public class BoughLotControlController : ApiController
    {
        private IMapper mapper;
        private IBoughLotService boughtLotService;

        public BoughLotControlController(IBoughLotService boughtLotServicee, IMapper mapper)
        {
            this.mapper = mapper;
            this.boughtLotService = boughtLotServicee;
        }
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteBought(int userId,int lotId)
        {
            await Task.Run(()=>boughtLotService.DeleteBought(userId, lotId));
            return Ok();
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetBought(int userId)
        {
            var lots= await Task.Run(() => mapper.Map<IEnumerable<LotViewModel>>(boughtLotService.GetBought(userId)));
            return Ok(lots);
        }
    }
}