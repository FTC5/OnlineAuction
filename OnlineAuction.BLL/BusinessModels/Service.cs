using AutoMapper;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.BusinessModels
{
    class Service
    {
        private IUnitOfWork db;
        private IMapper mapper;

        public Service(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IUnitOfWork Db { get => db;}
        public IMapper Mapper { get => mapper;}
    }
}
