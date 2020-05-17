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
    public interface IUserService
    {
        void AddLotTOSubscription(int userD, int lotId);
        IEnumerable<LotViewDTO> GetSubscription(int userd);
        void DeleteSubscription(int userd, int lotId);
        IEnumerable<LotViewDTO> GetUserLot(int userd);
        void UpdateLot(int userd, LotDTO changed);
        void AddLot(int userd, LotDTO lot);
        void DeleteLot(int userId, int lotId);
        UserDTO GetLotAutorInfo(int lotId);
        void AddBalance(int userd, int count);
        void AddBet(int lotId, int userd);
        void ChangeLogin(int userd, string newLogin);
        void ChangePassword(int userd, string newPassword);
    }
}
