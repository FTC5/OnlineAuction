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
    public interface IAdminService
    {
        void DeleteCategory(int categoryId);
        void AddCategory(CategoryDTO category);
        IEnumerable<AdvancedUserDTO> GetManegers();
        void DeleteManeger(int id);
        void AddManeger(PersonDTO person, AuthenticationDTO authent);
        void UpdateCategory(int CategoryId, string name);
    }
}
