using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.BusinessModels.Interfaces
{
    public interface ICleanService
    {
        void CleanOld();
    }
}
