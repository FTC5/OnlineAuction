﻿using AutoMapper;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Web.Models;
using OnlineAuction.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineAuction.Web.Controllers
{
    public class ManagersControlController : ApiController
    {
        private IMapper mapper;
        private IAdminService adminService;
        private IUserService userService;

        public ManagersControlController(IAdminService adminService, IUserService userService)
        {
            this.adminService = adminService;
            this.userService = userService;
            mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
        }

        public void PostManager(PersonModel person,string login,string password)
        {
            AuthenticationModel model = new AuthenticationModel();
            model.Login = login;
            model.Password = password;
            adminService.AddManeger(mapper.Map<PersonDTO>(person), mapper.Map<AuthenticationDTO>(model));
        }
        public IHttpActionResult GetManagers()
        {
            var managers = mapper.Map<IEnumerable<AdvancedUserModel>>(adminService.GetManegers());
            return Ok(managers);
        }
        public void DeleteManager(int manegerId)
        {
            adminService.DeleteManeger(manegerId);
        }
    }
}