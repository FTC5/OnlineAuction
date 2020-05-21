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
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.BLL.Services
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
                  if (c.ParentCategory!=null && c.ParentCategory.Id == categoryId)
                  {
                      return true;
                  }
                  return false;
              });
            if (categorys.Count()>0)
                throw new OperationFaildException("Operation Failed : Category is parent for other");

            db.Category.Delete(categoryId);
            db.Save();

        }
        public void UpdateCategory(int categoryId,string name)
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
        public void AddCategory(CategoryDTO category)//Parent=null//Perent not found
        {
            if (category == null)
                return;
            var results = new List<ValidationResult>();
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(category);
            if (!Validator.TryValidateObject(category, context, results, true))
                throw new Infrastructure.ValidationException("Unable to add category", results);
        
            var categories = db.Category.Find(c =>
            {
                if(c.Name.Equals(category.Name) && c.ParentCategory.Id == category.ParentCategoryId)
                {
                    return true;
                }
                return false;
            });
            if (categories.Count() > 0)
                return;

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
            if (maneger == null)
                return;
            if (maneger.Admin==true)
                throw new OperationFaildException("Operation Failed : Is not manager Id");

            db.AdvancedUser.Delete(id);
            db.Save();
        }
        public void AddManager(PersonDTO person,AuthenticationDTO authent)//validation
        {
            bool first = false;
            var results1 = new List<ValidationResult>();
            var results2 = new List<ValidationResult>();
            var context1 = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var context2 = new System.ComponentModel.DataAnnotations.ValidationContext(authent);
            first = !Validator.TryValidateObject(person, context1, results1, true);
            if (!Validator.TryValidateObject(authent, context2, results2, true) || first == true)
            {
                results1.AddRange(results2);
                throw new Infrastructure.ValidationException("Can not add new manager", results1);
            }
                
            AdvancedUserDTO advUser = mapper.Map<AdvancedUserDTO>(person);
            advUser.Admin = false;
            advUser.Authentication = authent;
            string login = authent.Login;
            var aut = db.Authentication.Find(a => a.Login == login);
            if (aut.Count()!=0)
                throw new OperationFaildException("Operation Failed : Login already exists");

            var authentication = mapper.Map<Authentication>(advUser.Authentication);
            db.Authentication.Create(authentication);
            db.AdvancedUser.Create(mapper.Map<AdvancedUser>(advUser));
            db.Save();
        }
    }
}
