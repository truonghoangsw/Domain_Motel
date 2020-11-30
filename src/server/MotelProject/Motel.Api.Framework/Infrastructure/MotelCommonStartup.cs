using Motel.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motel.Api.Framework.Jwt;
using Motel.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Motel.Api.Framework.Middleware;

namespace Motel.Api.Framework.Infrastructure
{
    public class MotelCommonStartup : IMotelStartup
    {
        public int Order => 100;

        public void Configure(IApplicationBuilder application)
        {
            
            application.UseRouting();
            application.UseAuthentication();
            application.UseAuthorization();
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");
            services.AddCustomJwtBearer(configuration);
           
            services.AddSwaggerGen();
    
            services.AddOptions<BearerTokensOptions>()
                .Bind(configuration.GetSection("BearerTokens"))
                .Validate(bearerTokens =>
                {
                    return bearerTokens.AccessTokenExpirationMinutes < bearerTokens.RefreshTokenExpirationMinutes;
                }, "RefreshTokenExpirationMinutes is less than AccessTokenExpirationMinutes. Obtaining new tokens using the refresh token should happen only if the access token has expired.");
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Latest);
            
        }
    }
}
