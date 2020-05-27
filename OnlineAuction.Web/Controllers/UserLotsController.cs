using AutoMapper;
using OnlineAuction.BLL.DTO;
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
    [RoutePrefix("api/user/lots"), CookieAuthorization(Role = "User")]
    public class UserLotsController : ApiController
    {
        private IMapper mapper;
        private IUserLotService userLotService;

        public UserLotsController(IUserLotService userLotService, IMapper mapper)
        {
            this.userLotService = userLotService;
            this.mapper = mapper;
        }
        [HttpGet,Route("get")]
        public async Task <IHttpActionResult> GetLots(int userId)
        {
            var lots = await Task.Run(() => mapper.Map<IEnumerable<LotViewModel>>(userLotService.GetUserLot(userId)));
            return Ok(lots);
        }
        [HttpPost, UserNotFoundExaption, Route("add")]
        public async void PostLot(int userId,[FromBody] LotModel lot)
        {
            await Task.Run(() => userLotService.AddLot(userId, mapper.Map<LotDTO>(lot)));
        }
        [HttpPut,LotNotFoundExaption, UserNotFoundExaption, Route("edit")]
        public async Task<IHttpActionResult> PutLot(int userId, LotModel lot)
        {
            await Task.Run(() => userLotService.UpdateLot(userId, mapper.Map<LotDTO>(lot)));
            return Ok();
        }
        [HttpDelete,OperationFaildException, Route("delete")]
        public async Task<IHttpActionResult> DeleteLot(int userId, int lotId)
        {
            await Task.Run(() => userLotService.DeleteLot(userId, lotId));
            return Ok();
        }
    }
}