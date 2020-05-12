using AutoMapper;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.BusinessModels
{
    public class Service
    {

        protected IUnitOfWork db;
        protected readonly IMapper mapper;

        public Service(IUnitOfWork db)
        {
            this.db = db;
            this.mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();
        }
    }
}
