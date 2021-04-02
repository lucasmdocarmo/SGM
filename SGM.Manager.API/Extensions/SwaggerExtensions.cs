﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.Manager.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                //options.AddSecurityDefinition("apikey", new OpenApiSecurityScheme
                //{
                //    Description = "API KEY",
                //    In = ParameterLocation.Header,
                //    Name = "Apikey",
                //    Type = SecuritySchemeType.ApiKey,
                //});

                //options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme{ Reference = new OpenApiReference
                        {
                           Type = ReferenceType.SecurityScheme,
                           Id = "Bearer"
                        },
                            BearerFormat = "Bearer <token>"

                        }, new string[] {}
                    }
                });

               

                var apiVersionDescriptionProvider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();


                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Where(a => !a.IsDeprecated)
                                            .OrderByDescending(a => a.ApiVersion.MajorVersion).ThenBy(a => a.ApiVersion.MinorVersion))
                {
                    options.SwaggerDoc(description.GroupName, CreateSwaggerInfoForApiVersion(description));
                }
            });

            return services;
        }
        private static OpenApiInfo CreateSwaggerInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = APIProfile.Name,
                Description = APIProfile.Description,
                Version = description.ApiVersion.ToString()
            };

            if (info.Version.Equals("1.0"))
            {
                info.Description += "<br>Versão inicial.";
            }
            else
            {
                if (APIProfile.VersioningDescriptions.ContainsKey(info.Version))
                {
                    var versionDescription = APIProfile.VersioningDescriptions?[info.Version];
                    if (!string.IsNullOrWhiteSpace(versionDescription))
                    {
                        info.Description += $"<br>{versionDescription}";
                    }
                }
            }

            return info;
        }

    }
    public class APIProfile
    {
        public static string Name => "SGM.Manager";
        public static string Description => "SGM Manager - API de Gerenciamento de Usuarios e Funcionarios no portal.";
        public static IDictionary<string, string> VersioningDescriptions => new Dictionary<string, string>();
        public static string DefaultCultureInfo => "pt-BR";
    }
}
