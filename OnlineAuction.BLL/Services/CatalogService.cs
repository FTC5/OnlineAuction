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

namespace OnlineAuction.BLL.Services
{
    public class CatalogService : Service, ICatalogService
    {
        public CatalogService(IUnitOfWork db) : base(db)
        {
        }
        public IEnumerable<LotViewDTO> GetLots() 
        {
            var lots = db.Lot.Find(l=>(l.ModerationResult==false && l.Sels==false));
            var lotsDTO = mapper.Map<IEnumerable<LotViewDTO>> (lots);
            return lotsDTO;
        }
        public IEnumerable<LotViewDTO> FindByNameLot(String text)
        {
            var lots = db.Lot.Find(i=>(i.Product.Name.Contains(text) && i.ModerationResult==true && i.Sels == false));
            var lotsDTO = mapper.Map<IEnumerable<LotViewDTO>>(lots);
            return lotsDTO;
        }
        public IEnumerable<LotViewDTO> FindByAutor(string nick)
        {
            var lots = db.Lot.Find(i => (i.User.LastName.Contains(nick) && i.ModerationResult == true && i.Sels == false));
            var lotsDTO = mapper.Map<IEnumerable<LotViewDTO>>(lots);
            return lotsDTO;
        }
        public LotDTO GetLot(int id)
        {
            var lot = db.Lot.Get(id);
            var lotDTO = mapper.Map<LotDTO>(lot);
            return lotDTO;
        }
        public IEnumerable<LotViewDTO> FindByCategory(int categoryId)
        {
            Category category;
            var lots = db.Lot.Find(i => 
                {
                    if (i.Product.CategoryId == categoryId && i.ModerationResult == true && i.Sels == false)
                    {
                        return true;
                    }
                    else
                    {
                        category = i.Product.Category;
                        while (category.ParentCategory != null)
                        {
                            if (category.ParentCategory.Id == categoryId && i.ModerationResult == true && i.Sels == false)
                            {
                                return true;
                            }
                            category = category.ParentCategory;
                        }
                    }
                    return false;

                });
            var lotsDTO = mapper.Map<IEnumerable<LotViewDTO>>(lots);
            return lotsDTO;
        }
    }
}
