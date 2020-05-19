using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineAuction.Web.Utility
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToMethod(AutoMapper).InSingletonScope();
        }

        private IMapper AutoMapper(Ninject.Activation.IContext context)
        {
            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapping>();
                //cfg.AddProfile<OnlineAuction.BLL.Infrastructure.AutoMapping>();
                cfg.ConstructServicesUsing(type => context.Kernel.GetType());
            }).CreateMapper();
            return mapper;
        }
    }
}