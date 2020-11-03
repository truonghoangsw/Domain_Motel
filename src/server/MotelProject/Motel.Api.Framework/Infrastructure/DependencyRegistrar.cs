using Autofac;
using Motel.Core.Configuration;
using Motel.Core.Infrastructure;
using Motel.Core.Infrastructure.DependencyManagement;
using Motel.Domain.ContextDataBase;
using Motel.Services.Authentication;
using Motel.Services.RentalPosting;
using Motel.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Api.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 2;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MotelConfig config)
        {
            builder.RegisterType<IUserService>().As<UserService>().InstancePerLifetimeScope();
            builder.RegisterType<IPermissionService>().As<PermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<IAuthenticationService>().As<CookieAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<IDataProviderManager>().As<DataProviderManager>().InstancePerDependency();
            builder.RegisterType<ICategoryService>().As<ICategoryService>().InstancePerDependency();
            builder.RegisterType<IRentalPostService>().As<RentalPostService>().InstancePerDependency();
            builder.RegisterType<IRentalPostService>().As<RentalPostService>().InstancePerDependency();
        }
    }
}
