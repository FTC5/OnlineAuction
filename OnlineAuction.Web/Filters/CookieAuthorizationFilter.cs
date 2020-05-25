using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OnlineAuction.Web.Filters
{
    public class CookieAuthorization : Attribute, ICookieAuthorizationFilter
    {
        public bool AllowMultiple
        {
            get { return false; }
        }

        public string Role { get => role; set => role = value; }

        private string role;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            CookieHeaderValue cookie = actionContext.Request.Headers.GetCookies("person").FirstOrDefault();
            if (cookie == null)
            {
                return Task.FromResult<HttpResponseMessage>(
                       actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Oшибка 403 – Forbidden, доступ запрещен"));
            }
            else if (Role == null)
            {
                return continuation();
            }
            CookieState cookieState = cookie["person"];
            if (Role.Equals(cookieState["Role"]))
            {
                return continuation();
            }
            return Task.FromResult<HttpResponseMessage>(
                       actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Oшибка 403 – Forbidden, доступ запрещен"));
        }
    }
}