using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Api.Framework
{
    public class SwaggerConfig : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IConfiguration _config;

        public SwaggerConfig(IConfiguration config)
        {
            _config = config;
        }
        public void Configure(SwaggerGenOptions options)
        {
            var oauthScopeDic = new Dictionary<string, string> { { "api", "Access to the motel" } };

            //options.OperationFilter<AuthorizeOperationFilter>();
            options.DescribeAllParametersInCamelCase();
            options.CustomSchemaIds(x => x.FullName);
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Club API", Version = "v1" });

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                   
                }
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "oauth2"}
                    },
                    oauthScopeDic.Keys.ToArray()
                }
            });
        }

    }
}
