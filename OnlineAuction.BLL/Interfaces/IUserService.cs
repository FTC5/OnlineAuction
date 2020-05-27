using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IUserService :ISubscriptionService
    {
        UserDTO GetLotAutorInfo(int lotId);
        void AddBalance(int userd, int count);
        void AddBet(int lotId, int userd);
    }
}
