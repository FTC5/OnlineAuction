using AutoMapper;
using OnlineAuction.BLL.DTO;
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
    public class RegistrationController : ApiController
    {
        private IMapper mapper;
        private IRegistrationService registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
        }
        [Route("api/registration/authorization/{login}/{password}")]
        public IHttpActionResult PostAuthorization(string login, string password)
        {
            int id = registrationService.AuthorizationRegistration(login, password);
            return Ok(id);
        }
        [Route("api/registration/person/{authorizationId}/{perosn}")]
        public IHttpActionResult PostUserInfo(int authorizationId, PersonModel person)
        {
            registrationService.UserRegistration(authorizationId, mapper.Map<PersonDTO>(person));
            return Ok();
        }
    }
}