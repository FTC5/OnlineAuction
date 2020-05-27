using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Services
{
    public class UserInfoService : Service, IUserInfoService
    {
        public UserInfoService(IUnitOfWork db) : base(db)
        {
        }
        public void ChangeLogin(int userId, string newLogin)
        {
            var cheack = db.Authentication.Find(i => i.Login == newLogin);
            if (cheack.Count() > 0)
            {
                return;
            }
            var authentication = db.Authentication.Get(userId);
            if (authentication == null)
                throw new UserNotFoundExaption("User does not exist");

            authentication.Login = newLogin;
            db.Authentication.Update(authentication);
            db.Save();
        }
        public void ChangePassword(int userId, string newPassword)
        {
            if (newPassword.Length < 8)
            {
                return;
            }
            var authentication = db.Authentication.Get(userId);
            if (authentication == null)
                throw new UserNotFoundExaption("User does not exist");

            authentication.Password = newPassword;
            db.Authentication.Update(authentication);
            db.Save();
        }
        public UserDTO GetUserInfo(int userId)
        {
            var user = db.User.Get(userId);
            return mapper.Map<UserDTO>(user);
        }
    }
}
