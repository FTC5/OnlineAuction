using System.Web.Mvc;
using Ninject;
using System.Collections.Generic;
using System;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.BLL.BusinessModels;
using AutoMapper;

namespace OnlineAuction.Web.Utility
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IAdminService>().To<AdminService>();
            kernel.Bind<ICatalogService>().To<CatalogService>();
            kernel.Bind<ICleanService>().To<CleanService>();
            kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
            kernel.Bind<IManagerService>().To<ManagerService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IRegistrationService>().To<RegistrationService>();
        }
    }
}