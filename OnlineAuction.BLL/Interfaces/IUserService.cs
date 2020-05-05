using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.BusinessModels.Interfaces
{
    interface IUserService
    {
        void AddLotTOSubscription(int UserID, int LotID);
        IEnumerable<LotDTO> GetSubscription(int UserId);
        void DeleteSubscription(int UserId, int LotId);
        IEnumerable<LotDTO> GetUserLot(int UserId);
        void EditLot(int UserId, LotDTO changed);
        void AddLot(int UserId, LotDTO lot);
        UserDTO GetLotAutorInfo(int LotID);
        void AddBalance(int UserId, int count);
        void AddBet(int LotId, int UserId);
        void ChangeLogin(int UserId, string newLogin);
        void ChangePassword(int UserId, string newPassword);
    }
}
