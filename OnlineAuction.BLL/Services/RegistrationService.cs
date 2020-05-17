using System;
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
        public RegistrationService(IUnitOfWork db) : base(db)
        {
        }

        public int AuthorizationRegistration(string login,string password)//Validation
        {
            var authentication = new AuthenticationDTO();
            authentication.Login = login;
            authentication.Password = password;

            var results = new List<ValidationResult>();
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(authentication);
            if (!Validator.TryValidateObject(authentication, context, results, true))
                throw new Infrastructure.ValidationException("Authorization have error", results);

            var aut = db.Authentication.Find(a => a.Login == login);
            if (aut.Count() != 0)
                throw new OperationFaildException("Operation Failed : Login already exists");

            db.Authentication.Create(mapper.Map<Authentication>(authentication));
            db.Save();
            return db.Authentication.Find(a => a.Login == login).First().Id;
        }
        public void UserRegistration(int authenticationId, PersonDTO person)
        {
            person.Id = authenticationId;
            UserDTO user = (UserDTO)person;
            db.User.Create(mapper.Map<User>(user));
        }
    }
}
