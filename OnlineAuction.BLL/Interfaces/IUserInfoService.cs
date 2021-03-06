﻿using OnlineAuction.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IUserInfoService
    {
        void ChangeLogin(int userd, string newLogin);
        void ChangePassword(int userd, string newPassword);
        UserDTO GetUserInfo(int userId);
    }
}
