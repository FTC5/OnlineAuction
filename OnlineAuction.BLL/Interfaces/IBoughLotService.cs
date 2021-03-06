﻿using OnlineAuction.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IBoughLotService
    {
        IEnumerable<LotViewDTO> GetBought(int userId);
        void DeleteBought(int userId, int lotId);      
    }
}
