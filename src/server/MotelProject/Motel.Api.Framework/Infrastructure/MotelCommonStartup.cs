using Motel.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Motel.Api.Framework.Infrastructure
{
    public class MotelCommonStartup : IMotelStartup
    {
        public int Order => 100;

        public void Configure(IApplicationBuilder application)
        {
            application.UseRouting();
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
        }
    }
}
