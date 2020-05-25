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
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    [RoutePrefix("api/user/lots"), CookieAuthorization(Role = "User")]
    public class UserLotsController : ApiController
    {
        private IMapper mapper;
        private IUserService userService;

        public UserLotsController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }
        [HttpGet,Route("get")]
        public IHttpActionResult GetLots(int userId)
        {
            var lots = mapper.Map<IEnumerable<LotViewModel>>(userService.GetUserLot(userId));
            return Ok(lots);
        }
        [HttpPost, UserNotFoundExaption, Route("add")]
        public void PostLot(int userId,[FromBody] LotModel lot)
        {
            userService.AddLot(userId, mapper.Map<LotDTO>(lot));
        }
        [HttpPut,LotNotFoundExaption, UserNotFoundExaption, Route("edit")]
        public IHttpActionResult PutLot(int userId, LotModel lot)
        {
            userService.UpdateLot(userId, mapper.Map<LotDTO>(lot));
            return Ok();
        }
        [HttpDelete,OperationFaildException, Route("delete")]
        public IHttpActionResult DeleteLot(int userId, int lotId)
        {
            userService.DeleteLot(userId, lotId);
            return Ok();
        }
    }
}