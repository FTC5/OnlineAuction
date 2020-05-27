using OnlineAuction.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IModerationService
    {
        IEnumerable<LotViewDTO> GetUncheckedLots();
        void AllowLot(int lotId);
        void PreventLot(int lotId, string comment);
    }
}
