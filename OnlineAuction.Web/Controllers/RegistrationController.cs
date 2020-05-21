using AutoMapper;
using OnlineAuction.BLL.DTO;
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
    [RoutePrefix("api/registration")]
    public class RegistrationController : ApiController
    {
        private IMapper mapper;
        private IRegistrationService registrationService;
        IAuthenticationService authentication;

        public RegistrationController(IRegistrationService registrationService,IAuthenticationService authentication,IMapper mapper)
        {
            this.registrationService = registrationService;
            this.authentication = authentication;
            this.mapper = mapper;
        }
        [Route("authorization/"), OperationFaildException, ValidationException]
        public IHttpActionResult PostAuthorization(string login, string password)
        {
            AuthenticationModel model = new AuthenticationModel();
            model.Login = login;
            model.Password = password;
            registrationService.AuthorizationRegistration(login, password);
            int id = authentication.GetAuthenticationId(mapper.Map<AuthenticationDTO>(model));
            return Ok(id);
        }
        [Route("person/"), OperationFaildException, ValidationException]
        public IHttpActionResult PostUserInfo(int authorizationId, PersonModel person)
        {
            registrationService.UserRegistration(authorizationId, mapper.Map<PersonDTO>(person));
            return Ok();
        }
    }
}