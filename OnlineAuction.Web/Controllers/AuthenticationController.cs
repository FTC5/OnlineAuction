using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public HttpResponseMessage GetCookieAuthentication(string login, string password)
        {
            var aut = new AuthenticationModel();
            aut.Login = login;
            aut.Password = password;
            aut.Id = authentication.GetAuthenticationId(mapper.Map<AuthenticationDTO>(aut));
            if (aut.Id > 0 && aut.Id != null)
            {
                return SetCookie(aut);
            }
            return Request.CreateResponse<string>(HttpStatusCode.Unauthorized, ""); 
        }
        private HttpResponseMessage SetCookie(AuthenticationModel model)
        {
            string[] roles = new string[] { "User", "Manager", "Admin" };
            int index = authentication.IsAdvancedUserDTO((int)model.Id) + 1;
            var value = new NameValueCollection();
            value["Id"] = model.Id.ToString();
            value["Login"] = model.Login;
            value["Role"] = roles[index];
            var cookie = new CookieHeaderValue("person", value);
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Path = "/";
            var response = Request.CreateResponse<string>(HttpStatusCode.OK, roles[index]);
            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return response;
        }
        [HttpPost,Route("end")]
        public HttpResponseMessage Exit()
        {
            return ClearCookie();
        }
        private HttpResponseMessage ClearCookie()
        {
            var currentCookie = Request.Headers.GetCookies("person").FirstOrDefault();
            if(currentCookie != null)
            {
                var value = new NameValueCollection();
                value["Id"] = "";
                value["Login"] = "";
                value["Role"] = "Visitor";
                var cookie = new CookieHeaderValue("person", value)
                {
                    Expires = DateTimeOffset.Now.AddDays(-1),
                    Domain = currentCookie.Domain,
                    Path = currentCookie.Path
                };
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }
    }
}