using AutoMapper;
using OnlineAuction.BLL.Interfaces;
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
    [RoutePrefix("api/catalog")]
    public class CatalogController : ApiController
    {
        private IMapper mapper;
        private ICatalogService catalogService;
        private ICategoryService categoryService;

        public CatalogController(ICatalogService catalogService, ICategoryService categoryService, IMapper mapper)
        {
            this.mapper =mapper;
            this.catalogService = catalogService;
            this.categoryService = categoryService;
        }
        public async Task<IHttpActionResult> GetLots()
        {
            var lots = await Task.Run(() => mapper.Map<IEnumerable<LotViewModel>>(catalogService.GetLots()));
            return Ok(lots);
        }
        [HttpGet, Route("categories")]
        public async Task<IHttpActionResult> GetChieldCategory(int? parentId)
        {
            var categories = await Task.Run(() => mapper.Map<IEnumerable<CategoryModel>>(categoryService.GetChildCategories(parentId))); 
            return Ok(categories);
        }
        [HttpGet, Route("category")]
        public async Task<IHttpActionResult> GetCategory(int Id)
        {
            var category = await Task.Run(() => mapper.Map<CategoryModel>(categoryService.GetCategory(Id)));
            return Ok(category);
        }
        [HttpGet, Route("lots/include")]
        public async Task<IHttpActionResult> FindLotsByName(string text)
        {
            var searchresult = await Task.Run(() => catalogService.FindByNameLot(text));
            var lots = mapper.Map<IEnumerable<LotViewModel>>(searchresult);
            return Ok(lots);
        }
        [HttpGet, Route("autor/include")]
        public async Task<IHttpActionResult> FindLotsByAutor(string text)
        {
            var searchresult = await Task.Run(() => catalogService.FindByAutor(text));
            var lots = mapper.Map<IEnumerable<LotViewModel>>(searchresult);
            return Ok(lots);
        }
        [HttpGet, Route("lots/include/category")]
        public async Task<IHttpActionResult> FindLotsWithCategory(int categoryId)
        {
            var categories = await Task.Run(() => mapper.Map<IEnumerable<LotViewModel>>(catalogService.FindByCategory(categoryId)));
            return Ok(categories);
        }
        [Route("lot/")]
        public async Task<IHttpActionResult> GetLot(int id)
        {
            var lots = await Task.Run(() => mapper.Map<LotModel>(catalogService.GetLot(id)));
            return Ok(lots);
        }
    }
}