﻿using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motel.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Core.Infrastructure
{
    public interface IEngine
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration, MotelConfig MotelConfig);
        void ConfigureRequestPipeline(IApplicationBuilder application);
        T Resolve<T>() where T : class;
        object Resolve(Type type);
        IEnumerable<T> ResolveAll<T>();
        object ResolveUnregistered(Type type);
        void RegisterDependencies(ContainerBuilder containerBuilder, MotelConfig nopConfig);

    }
}
