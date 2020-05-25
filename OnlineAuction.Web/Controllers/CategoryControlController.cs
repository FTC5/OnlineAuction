using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.ExceptionFilters;
using OnlineAuction.Web.Filters;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    [CookieAuthorization(Role = "Admin")]
    public class CategoryControlController : ApiController
    {
        private IMapper mapper;
        private ICategoryService categoryService;
        private IAdminService adminService;
        public CategoryControlController(IMapper mapper,ICategoryService categoryService, IAdminService adminService)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.adminService = adminService;
        }
        [HttpPost, ValidationException]
        public IHttpActionResult PostCategory([FromBody] CategoryModel category)
        {
            adminService.AddCategory(mapper.Map<CategoryDTO>(category));
            return Ok();
        }
        [HttpDelete, OperationFaildException]
        public IHttpActionResult DeleteCategory(int id)
        {
            adminService.DeleteCategory(id);
            return Ok();
        }
        public IHttpActionResult GetCategory()
        {
            var categories = mapper.Map<IEnumerable<CategoryModel>>(categoryService.GetCategories());
            return Ok(categories);

        }
        [HttpPut, OperationFaildException]
        public IHttpActionResult PutCategory(int id,string name)
        {
                adminService.UpdateCategory(id, name);
                return Ok();
        }
    }
}