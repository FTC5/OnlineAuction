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
    //[Authorize(Roles = "Manager")]
    public class LotModerationController : ApiController //Error
    {
        private IMapper mapper;
        private IManagerService managerService;

        public LotModerationController(IManagerService managerService)
        {
            this.managerService = managerService;
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
        }
        [HttpPut]
        public void AllowLot(int lotId)
        {
            ModerationModel moderation = new ModerationModel();
            moderation.ModerationResult = true;
            moderation.Id = lotId;
            managerService.SetModeration(mapper.Map<ModerationDTO>(moderation));
        }
        [HttpPost]
        public void PreventLot(int lotId,string cause) 
        {
            ModerationModel moderation = new ModerationModel();
            moderation.ModerationResult = false;
            moderation.Comment = cause;
            moderation.Id = lotId;
            managerService.SetModeration(mapper.Map<ModerationDTO>(moderation));
        }
    }
}