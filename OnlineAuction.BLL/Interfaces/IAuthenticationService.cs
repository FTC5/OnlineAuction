using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.BusinessModels.Interfaces
{
    public interface IAuthenticationService
    {
        int AuthenticationCheack(AuthenticationDTO authentication);
        int isAdvancedUserDTO(int userId);
    }
}
