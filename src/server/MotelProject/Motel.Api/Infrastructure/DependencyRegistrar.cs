using Autofac;
using Microsoft.AspNetCore.Authentication;
using Motel.Core.Configuration;
using Motel.Core.Infrastructure;
using Motel.Core.Infrastructure.DependencyManagement;
using Motel.Services.Authentication;
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
           // builder.RegisterType<IAuthenticationService>().As<CookieAuthenticationService>().InstancePerLifetimeScope();

        }
    }
}
