using AutoMapper;
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
        public IHttpActionResult GetLots()
        {
            var lots = mapper.Map<IEnumerable<LotViewModel>>(catalogService.GetLots());
            return Ok(lots);
        }
        [HttpGet, Route("categories")]
        public IHttpActionResult GetChieldCategory(int? parentId)
        {
            var categories = mapper.Map<IEnumerable<CategoryModel>>(categoryService.GetChildCategories(parentId)); 
            return Ok(categories);
        }
        [HttpGet, Route("category")]
        public IHttpActionResult GetCategory(int Id)
        {
            var category = mapper.Map<CategoryModel>(categoryService.GetCategory(Id));
            return Ok(category);
        }
        [HttpGet, Route("lots/include")]
        public IHttpActionResult FindLotsByName(string text)
        {
            var searchresult = catalogService.FindByNameLot(text);
            var lots = mapper.Map<IEnumerable<LotViewModel>>(searchresult);
            return Ok(lots);
        }
        [HttpGet, Route("autor/include")]
        public IHttpActionResult FindLotsByAutor(string text)
        {
            var searchresult = catalogService.FindByAutor(text);
            var lots = mapper.Map<IEnumerable<LotViewModel>>(searchresult);
            return Ok(lots);
        }
        [HttpGet, Route("lots/include/category")]
        public IHttpActionResult FindLotsWithCategory(int categoryId)
        {
            var categories = mapper.Map<IEnumerable<LotViewModel>>(catalogService.FindByCategory(categoryId));
            return Ok(categories);
        }
        [Route("lot/")]
        public IHttpActionResult GetLot(int id)
        {
            var lots = mapper.Map<LotModel>(catalogService.GetLot(id));
            return Ok(lots);
        }
    }
}