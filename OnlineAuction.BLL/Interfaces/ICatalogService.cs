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
    public interface ICatalogService
    {
        IEnumerable<LotViewDTO> GetLots();
        IEnumerable<LotViewDTO> FindByNameLot(String text);
        IEnumerable<LotViewDTO> FindByAutor(String nick);
        LotDTO GetLot(int id);
        IEnumerable<LotViewDTO> FindByCategory(int CategoryId);
    }
}
