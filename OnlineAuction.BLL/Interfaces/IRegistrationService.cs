using OnlineAuction.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IRegistrationService
    {
        int AuthorizationRegistration(string login, string password);
        void UserRegistration(int authenticationId, PersonDTO person);
    }
}
