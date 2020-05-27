using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.ExceptionFilters;
using OnlineAuction.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    [RoutePrefix("api/user/balance"), CookieAuthorization]
    public class UserBalanceController : ApiController
    {
        private IMapper mapper;
        private IUserService userInfoService;

        public UserBalanceController(IUserService userInfoService, IMapper mapper)
        {
            this.userInfoService = userInfoService;
            this.mapper = mapper;
        }
        [HttpPost, UserNotFoundExaption, Route("add")]
        public async void AddToBalance(int userId, int count)
        {
            await Task.Run(() => userInfoService.AddBalance(userId, count));
        }
    }
}