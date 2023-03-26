using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shared.Extensions;

public static class AddSwaggerExtension
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, string serviceName)
    {
        services.AddSwaggerGen(c =>
        {
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"Eparafia - {serviceName}", Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Jwt: Bearer {jwt token}"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            c.CustomSchemaIds(s => s.FullName!.Replace("+", "."));
            c.CustomOperationIds(apiDesc =>
            {
                var controllerName = apiDesc.TryGetMethodInfo(out var methodInfo)
                    ? methodInfo.DeclaringType!.Name
                    : null;
                return controllerName + "_" + apiDesc.HttpMethod;
            });
            //!!!
        });
        
        return services;
    }
}