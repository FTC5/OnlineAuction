using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.DAL.UnitOfWork;
using OnlineAuction.DAL.Interfaces;
using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.BLL.Services;
using OnlineAuction.BLL.BusinessModels;

namespace OnlineAuction.BLL.Infrastructure
{
    public class UoWModule : NinjectModule
    {
        private string connectionString;
        public UoWModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
            Bind<ICleanService>().To<CleanService>();
            Bind<IValidationCheckService>().To<ValidationCheckService>();
        }
    }
}
