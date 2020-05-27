using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IManagerService
    {      
        IEnumerable<LotViewDTO> GetOldLot();
        void StopLot(int lotId);
        void ContinueLot(int lotId);
    }
}
