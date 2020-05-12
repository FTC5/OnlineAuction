using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    public class CategoryControlController : ApiController
    {
        private IMapper mapper;
        private ICategoryService categoryService;
        private IAdminService adminService;
        public CategoryControlController(ICategoryService categoryService, IAdminService adminService)
        {
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
            this.categoryService = categoryService;
            this.adminService = adminService;
        }
        public void PostCategory(int parentId,string name)
        {
            CategoryModel category = new CategoryModel();
            category.Name = name;
            category.ParentCategoryId = parentId;
            adminService.AddCategory(mapper.Map<CategoryDTO>(category));
        }
        public void DeleteCategory(int id)
        {
            adminService.DeleteCategory(id);
        }
        public IHttpActionResult GetCategory()
        {
            var categories = mapper.Map<IEnumerable<CategoryModel>>(categoryService.GetCategories());
            return Ok(categories);

        }
        public IHttpActionResult PutCategory(int id,string name)
        {
            adminService.UpdateCategory(id, name);
            return Ok();

        }
    }
}