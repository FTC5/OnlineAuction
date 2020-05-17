using AutoMapper;
using OnlineAuction.BLL.Interfaces;
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

        public UserInfoController(IUserService userService)
        {
            this.userService = userService;
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
        }
        [HttpPut,Route("api/UserInfo/PutPassword/{userId:decimal}/{password}")]
        public IHttpActionResult PutPassword(int userId,string password)
        {
            userService.ChangePassword(userId,password);
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult PutLogin(int userId, string login)
        {
            userService.ChangeLogin(userId,login);
            return Ok();
        }
    }
}