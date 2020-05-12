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

namespace OnlineAuction.BLL.BusinessModels
{
    public class RegistrationService:Service, IRegistrationService
    {
        public RegistrationService(IUnitOfWork db) : base(db)
        {
        }

        public int AuthorizationRegistration(string login,string password)
        {
            if (String.IsNullOrWhiteSpace(login) == true || String.IsNullOrWhiteSpace(password))
                throw new ValidationException("Empty string", "");
            if (password.Length < 8)
                throw new ValidationException("Small password>8", "");

            var authentication = new AuthenticationDTO();
            authentication.Login = login;
            authentication.Password = password;
            var aut = db.Authentication.Find(a => a.Login == login);
            if (aut.Count() != 0)
                throw new OperationException("Operation Failed : Login already exists", "");

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
