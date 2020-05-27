using OnlineAuction.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IUserLotService
    {
        IEnumerable<LotViewDTO> GetUserLot(int userd);
        void UpdateLot(int userd, LotDTO changed);
        void AddLot(int userd, LotDTO lot);
        void DeleteLot(int userId, int lotId);
    }
}
