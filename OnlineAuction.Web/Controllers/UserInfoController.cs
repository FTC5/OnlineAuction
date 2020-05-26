using AutoMapper;
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
    [RoutePrefix("api/user"), CookieAuthorization]
    public class UserInfoController : ApiController
    {
        private IMapper mapper;
        private IUserService userService;

        public UserInfoController(IUserService userService,IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }
        [HttpPut,Route("put/password/"), UserNotFoundExaption]
        public async Task<IHttpActionResult> PutPassword(int userId,string password)
        {
            await Task.Run(() => userService.ChangePassword(userId,password));
            return Ok();
        }
        [HttpPut, UserNotFoundExaption,Route("put/login/")]
        public async Task<IHttpActionResult> PutLogin(int userId, string login)
        {
            await Task.Run(() => userService.ChangeLogin(userId,login));
            return Ok();
        }
        [HttpPost, UserNotFoundExaption, Route("add/balance/")]
        public async void AddToBalance(int userId, int count)
        {
            await Task.Run(() => userService.AddBalance(userId, count));
        }
        [HttpGet, Route("get")]
        public async Task<IHttpActionResult> GetUserInfo(int userId)
        {
            var  user = await Task.Run(() => mapper.Map<UserModel>(userService.GetUserInfo(userId)));
            return Ok(user);
        }
    }
}