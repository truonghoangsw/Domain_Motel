using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Motel.Api.Framework;
using Motel.Core;
using Motel.Core.Caching;
using Motel.Core.Configuration;
using Motel.Core.Infrastructure;
using Motel.Core.Infrastructure.DependencyManagement;
using Motel.Core.Redis;
using Motel.Domain;
using Motel.Domain.ContextDataBase;
using Motel.Services.Authentication;
using Motel.Services.Caching;
using Motel.Services.Configuration;
using Motel.Services.Events;
using Motel.Services.Logging;
using Motel.Services.Media;
using Motel.Services.RentalPosting;
using Motel.Services.Security;
using Nop.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Motel.Api.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 2;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, MotelConfig config)
        {
            builder.RegisterType<MotelFileProvider>().As<IMotelFileProvider>().InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

          
            //data layer
           //data layer
            builder.RegisterType<DataProviderManager>().As<IDataProviderManager>().InstancePerDependency();
            builder.Register(context => context.Resolve<IDataProviderManager>().DataProvider).As<IMotelDataProvider>().InstancePerDependency();

            
            //repositories
            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<DownloadService>().As<IDownloadService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<CookieAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<DataProviderManager>().As<IDataProviderManager>().InstancePerDependency();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerDependency();
            builder.RegisterType<EventPublisher>().As<IEventPublisher>().InstancePerDependency();
            builder.RegisterType<CacheKeyService>().As<ICacheKeyService>().InstancePerLifetimeScope();
            builder.RegisterType<PictureService>().As<IPictureService>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<IDataProviderManager>().DataProvider).As<IMotelDataProvider>().InstancePerDependency();
            if (config.RedisEnabled)
            {
                builder.RegisterType<RedisConnectionWrapper>()
                    .As<ILocker>()
                    .As<IRedisConnectionWrapper>()
                    .SingleInstance();
            }
            var consumers = typeFinder.FindClassesOfType(typeof(IConsumer<>)).ToList();
            foreach (var consumer in consumers)
            {
                builder.RegisterType(consumer)
                    .As(consumer.FindInterfaces((type, criteria) =>
                    {
                        var isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
                        return isMatch;
                    }, typeof(IConsumer<>)))
                    .InstancePerLifetimeScope();
            }

        }
    }

    public class SettingsSource : IRegistrationSource
    {
        private static readonly MethodInfo _buildMethod =
            typeof(SettingsSource).GetMethod("BuildRegistration", BindingFlags.Static | BindingFlags.NonPublic);

        /// <summary>
        /// Registrations for
        /// </summary>
        /// <param name="service">Service</param>
        /// <param name="registrations">Registrations</param>
        /// <returns>Registrations</returns>
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = _buildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = _buildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        /// <summary>
        /// Is adapter for individual components
        /// </summary>
        public bool IsAdapterForIndividualComponents => false;
    }
}
