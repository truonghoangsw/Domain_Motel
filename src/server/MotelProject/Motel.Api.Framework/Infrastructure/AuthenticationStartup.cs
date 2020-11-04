
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motel.Api.Infrastructure;
using Motel.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Api.Framework.Infrastructure
{
    public class AuthenticationStartup : IMotelStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //add data protection
            services.AddMotelDataProtection();

            //add authentication
           // services.AddMotelAuthentication();
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            //configure authentication
            //application.UseNopAuthentication();
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 500; //authentication should be loaded before MVC
    }
}
