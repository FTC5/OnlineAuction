using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.BusinessModels.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<CategoryDTO> GetCategory();
        void DeleteCategory(int CategoryId);
        void AddCategory(CategoryDTO category);
        IEnumerable<AdvancedUserDTO> GetManegers();
        void DeleteManeger(int id);
        void AddManeger(AdvancedUserDTO advuser);
    }
}
