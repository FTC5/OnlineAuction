using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.BusinessModels
{
    public class CatalogService : Service, ICatalogService
    {
        public CatalogService(IUnitOfWork db) : base(db)
        {
        }
        public IEnumerable<LotDTO> GetLots() 
        {
            var lots = db.Lot.Find(l=>(l.ModerationResult==false && l.Sels==false));
            var lotsDTO = mapper.Map<IEnumerable<LotDTO>> (lots);
            return lotsDTO;
        }
        public IEnumerable<LotDTO> FindByNameLot(String text)
        {
            var lots = db.Lot.Find(i=>(i.Product.Name.Contains(text) && i.ModerationResult==true && i.Sels == false));
            var lotsDTO = mapper.Map<IEnumerable<LotDTO>>(lots);
            return lotsDTO;
        }
        public IEnumerable<LotDTO> FindByAutor(string nick)
        {
            var lots = db.Lot.Find(i => (i.User.LastName.Contains(nick) && i.ModerationResult == true && i.Sels == false));
            var lotsDTO = mapper.Map<IEnumerable<LotDTO>>(lots);
            return lotsDTO;
        }
        public LotDTO GetLot(int id)
        {
            var lot = db.Lot.Get(id);
            var lotDTO = mapper.Map<LotDTO>(lot);
            return lotDTO;
        }
        public IEnumerable<LotDTO> FindByCategory(int categoryId)
        {
            var lots = db.Lot.Find(i => 
                {
                    if (i.Product.CategoryId == categoryId)
                    {
                        return true;
                    }
                    else
                    {
                        int id = i.Product.Category.ParentCategory.Id;
                        while (i.Product.Category.ParentCategory != null)
                        {
                            if (id == categoryId && i.ModerationResult == true && i.Sels == false)
                            {
                                return true;
                            }
                            id = i.Product.Category.ParentCategory.Id;
                        }
                    }
                    return false;

                });
            var lotsDTO = mapper.Map<IEnumerable<LotDTO>>(lots);
            return lotsDTO;
        }
    }
}
