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
    public class CatalogController : ApiController
    {
        private IMapper mapper;
        private ICatalogService catalogService;
        private ICategoryService categoryService;

        public CatalogController(ICatalogService catalogService, ICategoryService categoryService)
        {
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
            this.catalogService = catalogService;
            this.categoryService = categoryService;
        }

        public IHttpActionResult GetLots()
        {
            var lots = mapper.Map<IEnumerable<LotModel>>(catalogService.GetLots());
            return Ok(lots);
        }
        [HttpGet, Route("api/catalog/categories/{parentId=1:decimal}")]
        public IHttpActionResult GetChieldCategory(int? parentId)
        {
            var categories = mapper.Map<IEnumerable<CategoryModel>>(categoryService.GetChildCategories(parentId)); 
            return Ok(categories);
        }
        [HttpGet, Route("api/catalog/category/{id:decimal}")]
        public IHttpActionResult GetCategory(int Id)
        {
            var category = mapper.Map<CategoryModel>(categoryService.GetCategory(Id));
            return Ok(category);
        }
        [HttpGet, Route("api/catalog/lots/include/{text}")]
        public IHttpActionResult FindLotsByName(string text)
        {
            var categories = mapper.Map<LotModel>(catalogService.FindByNameLot(text));
            return Ok(categories);
        }
        [HttpGet, Route("api/catalog/autorLots/{lastname:alpha}")]
        public IHttpActionResult FindLotsByAutor(string text)
        {
            var categories = mapper.Map<LotModel>(catalogService.FindByAutor(text)); 
            return Ok(categories);
        }
        public IHttpActionResult PutCategory(int categoryId)
        {
            var categories = mapper.Map<LotModel>(catalogService.FindByCategory(categoryId));
            return Ok(categories);
        }
        [Route("api/catalog/lot/{id:decimal}")]
        public IHttpActionResult GetLot(int id)
        {
            var lots = mapper.Map<LotModel>(catalogService.GetLot(id));
            return Ok(lots);
        }
    }
}