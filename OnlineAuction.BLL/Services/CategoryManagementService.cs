using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.Services
{
    public class CategoryManagementService : Service, ICategoryManagementService
    {
        IValidationCheckService validation;
        public CategoryManagementService(IUnitOfWork db,IValidationCheckService validation) : base(db)
        {
            this.validation = validation;
        }
        public void DeleteCategory(int categoryId)
        {
            var category = db.Category.Get(categoryId);
            if (category == null)
                return;

            if (category.ParentCategory == null)
                throw new OperationFaildException("Operation Failed : Cant delete Main category");

            var lots = db.Lot.Find(i =>
            {
                if (i.Product.CategoryId == categoryId)
                {
                    return true;
                }
                return false;
            });
            if (lots.Count() > 0)
                throw new OperationFaildException("Operation Failed : Lots with this category exists");
            var categorys = db.Category.Find(c =>
            {
                if (c.ParentCategory != null && c.ParentCategory.Id == categoryId)
                {
                    return true;
                }
                return false;
            });
            if (categorys.Count() > 0)
                throw new OperationFaildException("Operation Failed : Category is parent for other");

            db.Category.Delete(categoryId);
            db.Save();

        }
        public void UpdateCategory(int categoryId, string name)
        {
            var category = db.Category.Get(categoryId);
            if (String.IsNullOrWhiteSpace(name))
                return;

            if (category == null)
                throw new OperationFaildException("Operation Failed : Category not found");

            if (category.ParentCategory == null)
                throw new OperationFaildException("Operation Failed : Cant update Main category");

            var categories = db.Category.Find(c =>
            {
                if (c.Name.Equals(name) && c.ParentCategory.Id == category.ParentCategory.Id)
                {
                    return true;
                }
                return false;
            });
            if (categories.Count() > 0)
                throw new OperationFaildException("Operation Failed : Category already exists");

            category.Name = name;
            db.Category.Update(category);
            db.Save();

        }
        public void AddCategory(CategoryDTO category)
        {
            if (category == null)
                return;
            var results = validation.Check<CategoryDTO>(category);
            if (results.Count > 0)
                throw new Infrastructure.ValidationDTOException("Unable to add category", results);

            var parent = db.Category.Get(category.ParentCategoryId);
            if (parent == null)
            {
                parent = db.Category.Get(1);//magic
            }
            var categories = db.Category.Find(c =>
            {
                if (c.Name.Equals(category.Name) && parent.Id == category.ParentCategoryId)
                {
                    return true;
                }
                return false;
            });
            if (categories.Count() > 0)
                return;

            var cat = mapper.Map<Category>(category);
            cat.ParentCategory = parent;
            db.Category.Create(cat);
            db.Save();
        }
    }
}
