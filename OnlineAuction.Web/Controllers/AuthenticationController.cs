using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    [RoutePrefix("api/authentication")]
    public class AuthenticationController: ApiController
    {
        private IMapper mapper;
        private IAuthenticationService authentication;
        public AuthenticationController(IAuthenticationService authentication,IMapper mapper)
        {
            HttpContext.Current = null;
            this.mapper = mapper;
            this.authentication = authentication;
        }
        [Route("start")]
        public IHttpActionResult GetAuthentication(string login,string password)
        {
            var aut = new AuthenticationModel();
            aut.Login = login;
            aut.Password = password;
            aut.Id = authentication.GetAuthenticationId(mapper.Map<AuthenticationDTO>(aut));
            if (aut.Id >0 && aut.Id!=null)
            {
                return Ok(SetRole(aut));
            }
            return Unauthorized();
        }
        private string SetRole(AuthenticationModel model)
        {
            string[] roles = new string[] { "User", "Manager", "Admin" };
            int index = authentication.IsAdvancedUserDTO((int)model.Id) + 1;
            var identity = new GenericIdentity(model.Login);
            var principal = new GenericPrincipal(identity, new string[] { roles[index] });
            SetPrincipal(principal);
            return roles[index];
        }
        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
    }
}