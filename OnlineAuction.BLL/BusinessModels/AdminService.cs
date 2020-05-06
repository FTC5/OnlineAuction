using AutoMapper;
using OnlineAuction.BLL.BusinessModels.Interfaces;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.BusinessModels
{
    class AdminService:Service,IAdminService
    {
        public AdminService(IUnitOfWork db) : base(db)
        {
        }

        public IEnumerable<CategoryDTO> GetCategory()
        {
            var category = db.Category.GetAll();
            var categoryDTO = mapper.Map<IEnumerable<CategoryDTO>>(category);
            return categoryDTO;
        }
        public void DeleteCategory(int CategoryId)
        {
            var category = db.Category.Get(CategoryId);
            if (category == null)
                throw new CategoryNotFoundExaption("Category not found", "");

            if (category.ParentCategory == null)
                throw new OperationException("Operation Failed : Cant delete Main category", "");

            var lots = db.Lot.Find(i =>
              {
                  if (i.Product.CategoryId == CategoryId)
                  {
                      return true;
                  }
                  return false;
              });
            if (lots.Count() > 0)
                throw new OperationException("Operation Failed : Lots with this category exists", "");
            var categorys = db.Category.Find(c =>
              {
                  if (c.ParentCategory.Id == CategoryId)
                  {
                      return true;
                  }
                  return false;
              });
            if (categorys.Count()>0)
                throw new OperationException("Operation Failed : Category is parent for other", "");

            db.Category.Delete(CategoryId);
            db.Save();

        }
        public void AddCategory(CategoryDTO category)
        {
            if (String.IsNullOrEmpty(category.Name))
                throw new ValidationException("Empty name","");

            var categories = db.Category.Find(c =>
              {
                  if(c.Name.Equals(category.Name) && c.ParentCategory.Id == category.ParentCategoryId)
                  {
                      return true;
                  }
                  return false;
              });
            if (categories.Count()>0)
                throw new OperationException("Operation Failed : Category already exists", "");

            var cat = mapper.Map<Category>(category);
            cat.ParentCategory = db.Category.Get(category.ParentCategoryId);
            db.Category.Create(cat);
            db.Save();
        }
        public IEnumerable<AdvancedUserDTO> GetManegers()
        {
            var manegers = db.AdvancedUser.Find(m => !m.Admin);
            return mapper.Map<IEnumerable<AdvancedUserDTO>>(manegers);
        }
        public void DeleteManeger(int id)
        {
            var maneger = db.AdvancedUser.Get(id);
            if(maneger==null)
                throw new UserNotFoundExaption("User does not exist", "");
            if (maneger.Admin==true)
                throw new OperationException("Operation Failed : Is not manager Id", "");

            db.AdvancedUser.Delete(id);
            db.Save();
        }
        public void AddManeger(AdvancedUserDTO advuser)
        {
            advuser.Admin = false;
            
            if (advuser.Authentication == null)
                throw new OperationException("Operation Failed : Empty authentication", "");
            if(String.IsNullOrWhiteSpace(advuser.Authentication.Login) || 
                String.IsNullOrWhiteSpace(advuser.Authentication.Password))
                throw new ValidationException("Empty login or password", "");
            if(advuser.Authentication.Password.Length<8)
                throw new ValidationException("Small password", "");

            string login = advuser.Authentication.Login;
            var aut = db.Authentication.Find(a => a.Login == login);
            if (aut.Count()==0)
                throw new OperationException("Operation Failed : Login already exists", "");

            var authentication = mapper.Map<Authentication>(advuser.Authentication);
            db.Authentication.Create(authentication);
            db.AdvancedUser.Create(mapper.Map<AdvancedUser>(advuser));
            db.Save();
        }
    }
}
