using System.Web.Http.Dependencies;
using Ninject;
using System.Collections.Generic;
using System;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.BLL.Services;
using AutoMapper;

namespace OnlineAuction.Web.Utility
{
    public class CustomeNinjectDependencyResolver : IDependencyResolver
    {
        IKernel kernel;
        public CustomeNinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
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
