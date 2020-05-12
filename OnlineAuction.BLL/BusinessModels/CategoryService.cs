using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.BusinessModels
{
    public class CategoryService:Service,ICategoryService
    {
        protected static readonly int mainCategoryId = 1;
        public CategoryService(IUnitOfWork db) : base(db)
        {
        }

        public CategoryDTO GetCategory(int id)
        {
            var category = db.Category.Get(id);
            var categoryDTO = mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }
        public IEnumerable<CategoryDTO> GetCategories()
        {
            var categories = db.Category.GetAll();
            var categoriesDTO = mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return categoriesDTO;
        }

        public IEnumerable<CategoryDTO> GetChildCategories(int? categoryId)
        {
            if (categoryId == null)
            {
                categoryId = mainCategoryId;
            }
            var categories = db.Category.Find(c => c.ParentCategory.Id == categoryId);
            var categoriesDTO = mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return categoriesDTO;
        }
    }
}
