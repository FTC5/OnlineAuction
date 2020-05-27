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
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    [CookieAuthorization(Role = "Admin")]
    public class ManagersControlController : ApiController
    {
        private IMapper mapper;
        private IAdminService adminService;

        public ManagersControlController(IAdminService adminService,IMapper mapper)
        {
            this.adminService = adminService;
            this.mapper = mapper;
        }
        [HttpPost, OperationFaildException, ValidationException]
        public async Task<IHttpActionResult> PostManager(string login, string password,[FromBody]PersonModel person)
        {
            AuthenticationModel model = new AuthenticationModel();
            model.Login = login;
            model.Password = password;
            await Task.Run(() => adminService.AddManager(mapper.Map<PersonDTO>(person), mapper.Map<AuthenticationDTO>(model)));
            return Ok();
        }
        public async Task<IHttpActionResult> GetManagers()
        {
            var managers = await Task.Run(() => mapper.Map<IEnumerable<AdvancedUserModel>>(adminService.GetManegers()));
            return Ok(managers);
        }
        [HttpDelete, OperationFaildException]
        public async Task<IHttpActionResult> DeleteManager(int manegerId)
        {
            await Task.Run(() => adminService.DeleteManeger(manegerId));
            return Ok();
        }
    }
}
