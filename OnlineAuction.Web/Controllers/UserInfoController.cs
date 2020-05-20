using AutoMapper;
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
    //[Authorize]
    public class UserInfoController : ApiController
    {
        private IMapper mapper;
        private IUserService userService;

        public UserInfoController(IUserService userService,IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }
        [HttpPut,Route("api/UserInfo/PutPassword/{userId:decimal}/{password}"), UserNotFoundExaption]
        public IHttpActionResult PutPassword(int userId,string password)
        {
            userService.ChangePassword(userId,password);
            return Ok();
        }
        [HttpPut, UserNotFoundExaption]
        public IHttpActionResult PutLogin(int userId, string login)
        {
            userService.ChangeLogin(userId,login);
            return Ok();
        }
        [HttpPost, UserNotFoundExaption]
        public void AddToBalance(int userId, int count)
        {
            userService.AddBalance(userId, count);
        }
        [HttpGet]
        public IHttpActionResult GetUserInfo(int userId)
        {
            var user=mapper.Map<UserModel>(userService.GetUserInfo(userId));
            return Ok(user);
        }
    }
}