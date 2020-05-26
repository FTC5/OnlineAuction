using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.Interfaces
{
    public interface ICleanService
    {
        IEnumerable<LotDTO> GetOldLots(int extraDay=0);
        void DeleteOldLots();
        void DeleteLot(int lotId);
    }
}
