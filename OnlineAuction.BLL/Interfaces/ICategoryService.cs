using OnlineAuction.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Interfaces
{
    public interface ICategoryService
    {
        CategoryDTO GetCategory(int id);
        IEnumerable<CategoryDTO> GetCategories();
        IEnumerable<CategoryDTO> GetChildCategories(int? categoryId);
    }
}
