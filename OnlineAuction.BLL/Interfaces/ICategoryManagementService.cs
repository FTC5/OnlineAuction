using OnlineAuction.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Interfaces
{
    public interface ICategoryManagementService
    {
        void DeleteCategory(int categoryId);
        void AddCategory(CategoryDTO category);
        void UpdateCategory(int CategoryId, string name);
    }
}
