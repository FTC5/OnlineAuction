using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class CategoryDTO
    {
        string name;
        string parentCategoryId;
        CategoryDTO parentCategory;

        public string Name { get; set; }
        public string ParentCategoryId { get; set; }
        public CategoryDTO ParentCategory { get; set; }
    }
}
