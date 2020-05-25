using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OnlineAuction.Web.Filters
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private string role;

        public CustomAuthorizationAttribute()
        {
        }

        public string Role { get => role; set => role = value; }
        
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,
                        CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            IPrincipal principal = actionContext.RequestContext.Principal;
            if (principal == null || !principal.IsInRole(role))
            {
                return Task.FromResult<HttpResponseMessage>(
                       actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            else
            {
                return continuation();
            }
        }
        public bool AllowMultiple
        {
            get { return false; }
        }

        
    }
}