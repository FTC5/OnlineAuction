using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.BusinessModels
{
    class AdminService:Service
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
            if (category.ParentCategory == null)
            {
                return;
            }
            var lots = db.Lot.Find(i =>
              {
                  if (i.Product.CategoryId == CategoryId)
                  {
                      return true;
                  }
                  return false;
              });
            if (lots != null)
            {
                return;
            }
            var categorys = db.Category.Find(c =>
              {
                  if (c.ParentCategory.Id == CategoryId)
                  {
                      return true;
                  }
                  return false;
              });
            if (category != null)
            {
                return;
            }
            db.Category.Delete(CategoryId);
            db.Save();

        }
        public void AddCategory(CategoryDTO category)
        {
            if (String.IsNullOrEmpty(category.Name))
            {
                return;
            }
            var categories = db.Category.Find(c =>
              {
                  if(c.Name.Equals(category.Name) && c.ParentCategory.Id == category.ParentCategoryId)
                  {
                      return true;
                  }
                  return false;
              });
            if (categories != null)
            {
                return;
            }
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
            if (maneger == null || maneger.Admin==true)
            {
                return;
            }
            else
            {
                db.AdvancedUser.Delete(id);
            }
            db.Save();
        }
        public void AddManeger(AdvancedUserDTO advuser)
        {
            advuser.Admin = false;
            
            if (advuser.Authentication == null)
            {
                return;
            }else if(String.IsNullOrWhiteSpace(advuser.Authentication.Login) || 
                String.IsNullOrWhiteSpace(advuser.Authentication.Password))
            {
                return;
            }
            else if(advuser.Authentication.Password.Length<8)
            {
                return;
            }
            string login = advuser.Authentication.Login;
            var aut = db.Authentication.Find(a => a.Login == login);
            if (aut != null)
            {
                return;
            }
            var authentication = mapper.Map<Authentication>(advuser.Authentication);
            db.Authentication.Create(authentication);
            db.AdvancedUser.Create(mapper.Map<AdvancedUser>(advuser));
            db.Save();
        }
    }
}
