﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL.Interfaces;
using OnlineAuction.DAL;
using OnlineAuction.BLL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.BLL.Services
{
    public class RegistrationService:Service, IRegistrationService
    {
        private IValidationCheckService validation;
        public RegistrationService(IUnitOfWork db,IValidationCheckService validation) : base(db)
        {
            this.validation = validation;
        }

        public void AuthorizationRegistration(string login,string password)
        {
            var authentication = new AuthenticationDTO();
            authentication.Login = login;
            authentication.Password = password;

            var results = validation.Check<AuthenticationDTO>(authentication);
            if (results.Count>0)
                throw new ValidationDTOException("Authorization have error", results);

            var aut = db.Authentication.Find(a => a.Login == login);
            if (aut.Count() != 0)
                throw new OperationFaildException("Operation Failed : Login already exists");

            db.Authentication.Create(mapper.Map<Authentication>(authentication));
            db.Save();
        }
        public void UserRegistration(int authenticationId, PersonDTO person)
        {
            var results = validation.Check<PersonDTO>(person);
            if (results.Count > 0)
                throw new ValidationDTOException("Data have error", results);
            UserDTO user = mapper.Map<UserDTO>(person);
            var aut = db.Authentication.Get(authenticationId);
            if(aut==null)
                throw new OperationFaildException("Authorization not found");
            user.Id = aut.Id;
            db.User.Create(mapper.Map<User>(user));
            db.Save();
        }
    }
}
