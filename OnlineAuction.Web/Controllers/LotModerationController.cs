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
    [CookieAuthorization(Role = "Manager"),RoutePrefix("api/moderation")]
    public class LotModerationController : ApiController
    {
        private IMapper mapper;
        private IModerationService moderationService;

        public LotModerationController(IModerationService moderationService,IMapper mapper)
        {
            this.mapper = mapper;
            this.moderationService = moderationService;
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
        }
        [HttpGet,Route("get")]
        public async Task <IHttpActionResult> GetUncheckedLots()
        {
            var lots = await Task.Run(() => mapper.Map<IEnumerable<LotViewModel>>(moderationService.GetUncheckedLots()));
            return Ok(lots);
        }
        [HttpPost, LotNotFoundExaption,Route("allow")]
        public async Task<IHttpActionResult> AllowLot(int lotId)
        {
            await Task.Run(() => moderationService.AllowLot(lotId));
            return Ok();
        }
        [HttpPost,LotNotFoundExaption, ValidationException, Route("prevent")]
        public async Task<IHttpActionResult> PreventLot(int lotId,string cause) 
        {
            await Task.Run(() => moderationService.PreventLot(lotId,cause));
            return Ok();
        }
    }
}