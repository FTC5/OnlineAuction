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
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> PostCategory([FromBody] CategoryModel category)
        {
            await Task.Run(() => adminService.AddCategory(mapper.Map<CategoryDTO>(category)));
            return Ok();
        }
        [HttpDelete, OperationFaildException]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {
            await Task.Run(() => adminService.DeleteCategory(id));
            return Ok();
        }
        public async Task<IHttpActionResult> GetCategory()
        {
            var categories = await Task.Run(() => mapper.Map<IEnumerable<CategoryModel>>(categoryService.GetCategories()));
            return Ok(categories);

        }
        [HttpPut, OperationFaildException]
        public async Task<IHttpActionResult> PutCategory(int id,string name)
        {
                await Task.Run(() => adminService.UpdateCategory(id, name));
                return Ok();
        }
    }
}