using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.BusinessModels
{
    class CatalogService : Service
    {
        public CatalogService(IUnitOfWork db, IMapper mapper) : base(db, mapper)
        {
        }
        public IEnumerable<LotDTO> GetLots()
        {
            var lots = Db.Lot.GetAll();
            var lotsDTO = Mapper.Map<IEnumerable<LotDTO>> (lots);
            return lotsDTO;
        }
    }
}
