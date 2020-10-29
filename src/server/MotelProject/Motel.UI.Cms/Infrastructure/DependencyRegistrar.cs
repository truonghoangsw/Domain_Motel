using Autofac;
using Motel.Core.Configuration;
using Motel.Core.Infrastructure;
using Motel.Core.Infrastructure.DependencyManagement;
using Motel.Domain.ContextDataBase;
using Motel.Services.Authentication;
using Motel.Services.Security;


namespace Motel.UI.Cms.Infrastructure
{
    public class DependencyRegistrar:IDependencyRegistrar
    {
         public int Order => 2;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MotelConfig config)
        {
            builder.RegisterType<IUserService>().As<UserService>().InstancePerLifetimeScope();
            builder.RegisterType<IPermissionService>().As<PermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<IAuthenticationService>().As<CookieAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<DataProviderManager>().As<IDataProviderManager>().InstancePerDependency();

        }
    }
}
