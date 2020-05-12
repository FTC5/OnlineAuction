using AutoMapper;
using OnlineAuction.BLL.Interfaces;
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
    public class AdminService:Service,IAdminService
    {
        public AdminService(IUnitOfWork db) : base(db)
        {
        }
        public void DeleteCategory(int categoryId)
        {
            var category = db.Category.Get(categoryId);
            if (category == null)
                throw new CategoryNotFoundExaption("Category not found", "");

            if (category.ParentCategory == null)
                throw new OperationException("Operation Failed : Cant delete Main category", "");

            var lots = db.Lot.Find(i =>
              {
                  if (i.Product.CategoryId == categoryId)
                  {
                      return true;
                  }
                  return false;
              });
            if (lots.Count() > 0)
                throw new OperationException("Operation Failed : Lots with this category exists", "");
            var categorys = db.Category.Find(c =>
              {
                  if (c.ParentCategory.Id == categoryId)
                  {
                      return true;
                  }
                  return false;
              });
            if (categorys.Count()>0)
                throw new OperationException("Operation Failed : Category is parent for other", "");

            db.Category.Delete(categoryId);
            db.Save();

        }
        public void UpdateCategory(int categoryId,string name)
        {
            var category = db.Category.Get(categoryId);
            if (category == null)
                throw new CategoryNotFoundExaption("Category not found", "");

            if (category.ParentCategory == null)
                throw new OperationException("Operation Failed : Cant update Main category", "");

            category.Name = name;

            var categories = db.Category.Find(c =>
            {
                if (c.Name.Equals(category.Name) && c.ParentCategory.Id == category.ParentCategory.Id)
                {
                    return true;
                }
                return false;
            });
            if (categories.Count() > 0)
                throw new OperationException("Operation Failed : Category already exists", "");

            db.Category.Update(category);
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
        public void AddManeger(PersonDTO person,AuthenticationDTO authent)
        {
            AdvancedUserDTO advUser = (AdvancedUserDTO)person;
            advUser.Admin = false;
            advUser.Authentication = authent;
            if (authent == null)
                throw new OperationException("Operation Failed : Empty authentication", "");
            if(String.IsNullOrWhiteSpace(authent.Login) || 
                String.IsNullOrWhiteSpace(authent.Password))
                throw new ValidationException("Empty login or password", "");
            if(authent.Password.Length<8)
                throw new ValidationException("Small password > 8", "");

            string login = authent.Login;
            var aut = db.Authentication.Find(a => a.Login == login);
            if (aut.Count()!=0)
                throw new OperationException("Operation Failed : Login already exists", "");

            var authentication = mapper.Map<Authentication>(advUser.Authentication);
            db.Authentication.Create(authentication);
            db.AdvancedUser.Create(mapper.Map<AdvancedUser>(advUser));
            db.Save();
        }
    }
}
