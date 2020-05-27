using OnlineAuction.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IManagerManagementService
    {
        IEnumerable<AdvancedUserDTO> GetManegers();
        void DeleteManeger(int id);
        void AddManager(PersonDTO person, AuthenticationDTO authent);
    }
}
