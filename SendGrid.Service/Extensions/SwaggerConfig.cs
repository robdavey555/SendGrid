using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SendGrid.Service.Extensions
{
    public static class SwaggerConfig
    {
        private const string ApiName = "SendGrid Service";
        private const string ApiVersion = "v1";

        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = ApiName,
                    Version = ApiVersion,
                    Description = "Service that hosts Send Grid",
                    Contact = new OpenApiContact
                    {
                        Name = "Joe Bloggs",
                        Email = "bloggsjoe@hotmail.com"
                    },
                });
            });
        }

        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ApiName} {ApiVersion}");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
