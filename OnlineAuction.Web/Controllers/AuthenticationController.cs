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
    public class AuthenticationController
    {
        private IMapper mapper;
        private IAuthenticationService authentication;
        private ICategoryService categoryService;

        public AuthenticationController(IAuthenticationService authentication,IMapper mapper)
        {
            HttpContext.Current = null;
            this.mapper = mapper;
            this.authentication = authentication;
        }
        public void GetAuthentication(string login,string password)
        {
            var aut = new AuthenticationModel();
            aut.Login = login;
            aut.Password = password;
            aut.Id = authentication.GetAuthenticationId(mapper.Map<AuthenticationDTO>(aut));
            if(aut.Id == null)
            {
                return;
            }
            SetRole(aut);
            return;
        }
        private void SetRole(AuthenticationModel model)
        {
            var identity = new GenericIdentity(model.Login);
            int i = authentication.IsAdvancedUserDTO((int)model.Id);
            if (i == -1)
            {
                SetPrincipal(new GenericPrincipal(identity, new string[] { "User" }));
            }
            else if (i == 0)
            {
                SetPrincipal(new GenericPrincipal(identity, new string[] { "Manager" }));
            }
            else
            {
                SetPrincipal(new GenericPrincipal(identity, new string[] { "Admin" }));
            }
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