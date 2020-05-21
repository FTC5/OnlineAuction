using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace OnlineAuction.Web.Filters
{
    public class CustomAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        private IAuthenticationService authenticationService;
        private IMapper mapper;
        public CustomAuthenticationAttribute(IAuthenticationService authenticationServic,IMapper mapper)
        {
            this.authenticationService = authenticationServic;
            this.mapper = mapper;
        }

        public bool AllowMultiple
        {
            get { return false; }
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            context.Principal = null;
            AuthenticationHeaderValue authentication = context.Request.Headers.Authorization;
            if (authentication != null && authentication.Scheme == "Basic")
            {

                string[] authData = Encoding.ASCII.GetString(Convert.FromBase64String(authentication.Parameter)).Split(':');
                string login = authData[0];
                var aut = new AuthenticationModel() { Login = login, Password = authData[1] };
                int id = authenticationService.GetAuthenticationId(mapper.Map<AuthenticationDTO>(aut));
                if (id != 0)
                {
                    string[] roles = new string[] { "User", "Manager","Admin" };
                    int index = authenticationService.IsAdvancedUserDTO(id) + 1;
                    context.Principal = new GenericPrincipal(new GenericIdentity(login), new string[] { roles[index] });
                }        
            }
            if (context.Principal == null)
            {
                context.ErrorResult= new UnauthorizedResult(new AuthenticationHeaderValue[] {new AuthenticationHeaderValue("Basic") }, context.Request);
            }
            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }
    }
}