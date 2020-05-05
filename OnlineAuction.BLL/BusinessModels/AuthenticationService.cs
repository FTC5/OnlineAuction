using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.BusinessModels
{
    class AuthenticationService : Service
    {
        public AuthenticationService(IUnitOfWork db) : base(db)
        {
        }
        public int AuthenticationCheack(AuthenticationDTO authentication)
        {
            string login = authentication.Login;
            string pasword= authentication.Password;
            var aut = db.Authentication.Find(a => (a.Login == login && a.Password == pasword)).ToList();
            if (aut == null || aut.Count==0)
            {

            }
            return aut.First().Id;
        }
        public int isAdvancedUserDTO(int userId)
        {
            var user = db.AdvancedUser.Get(userId);
            if (user == null)
            {
                return -1;
            }else if (user.Admin)
            {
                return 1;
            }
            return 0;
        }
    }
}
