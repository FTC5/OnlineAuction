using OnlineAuction.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Interfaces
{
    public interface ISubscriptionService
    {
        void AddLotToSubscription(int userId, int lotId);
        IEnumerable<LotViewDTO> GetSubscription(int userId);
        void DeleteSubscription(int userId, int lotId);
    }
}
