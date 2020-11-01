using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motel.Core;
using Microsoft.AspNetCore.DataProtection;
using Motel.Core.Configuration;
using Motel.Core.Infrastructure;
using System;
using System.Net;
using Motel.Core.Redis;
using Motel.Core.Security;

namespace Motel.Api.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static (IEngine, MotelConfig) ConfigureApplicationServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
             ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var motelConfig = services.ConfigureStartupConfig<MotelConfig>(configuration.GetSection("Motel"));

             //add accessor to HttpContext
            services.AddHttpContextAccessor();
            CommonHelper.DefaultFileProvider = new MotelFileProvider(webHostEnvironment);

             services.AddControllers();

            var engine = EngineContext.Create();

            engine.ConfigureServices(services, configuration, motelConfig);

            return (engine, motelConfig);

        }

         /// <summary>
        /// Create, bind and register as service the specified configuration parameters 
        /// </summary>
        /// <typeparam name="TConfig">Configuration parameters</typeparam>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        /// <returns>Instance of configuration parameters</returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }

        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void AddMotelDataProtection(this IServiceCollection services)
        {
            var motelConfig = services.BuildServiceProvider().GetRequiredService<MotelConfig>();
            if (motelConfig.RedisEnabled && motelConfig.UseRedisToStoreDataProtectionKeys)
            {
                //store keys in Redis
                services.AddDataProtection().PersistKeysToStackExchangeRedis(() =>
                {
                    var redisConnectionWrapper = EngineContext.Current.Resolve<IRedisConnectionWrapper>();
                    return redisConnectionWrapper.GetDatabase(motelConfig.RedisDatabaseId ?? (int)RedisDatabaseNumber.DataProtectionKeys);
                }, MotelDataProtectionDefaults.RedisDataProtectionKey);
            }
            else
            {
                var dataProtectionKeysPath = CommonHelper.DefaultFileProvider.MapPath(MotelDataProtectionDefaults.DataProtectionKeysPath);;
                var dataProtectionKeysFolder = new System.IO.DirectoryInfo(dataProtectionKeysPath);
                
                services.AddDataProtection().PersistKeysToFileSystem(dataProtectionKeysFolder);
            }

        }

    }
}
