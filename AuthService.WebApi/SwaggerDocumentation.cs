using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AuthService.WebApi
{
    internal static class SwaggerDocumentation
    {
        internal static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "AuthService.WebApi",
                    Version = "v1.0",
                    Description = "AuthService.WebApi"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                options.CustomSchemaIds(SchemaIdStrategy);
            });
        }

        internal static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "AuthService.WebApi V1.0"); });
        }

        private static string SchemaIdStrategy(Type currentClass)
        {
            string className = currentClass.Name;
            if (className.EndsWith("Dto"))
            {
                className = className.Remove(className.Length - 3, 3);
            }

            return className;
        }
    }
}
