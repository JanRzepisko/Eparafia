using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.BaseModels.Exceptions;

namespace Shared.Extensions;

public static class AddFluentValidation
{
    public static IServiceCollection AddFluentValidators(this IServiceCollection services,
        params Assembly[] validatorsAssemblies)
    {
        services.AddMvc()
            .AddJsonOptions(c =>
            {
                c.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                c.JsonSerializerOptions.MaxDepth = 32;
                c.JsonSerializerOptions.PropertyNamingPolicy = null;
                c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                c.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                c.JsonSerializerOptions.WriteIndented = true;
            })
            .ConfigureApiBehaviorOptions(c =>
            {
                c.InvalidModelStateResponseFactory = c =>
                {
                    throw new InvalidRequestException(c.ModelState.Keys.Select(a => a)
                        .ToDictionary(a => a, a => c.ModelState[a].Errors.Select(a => a.ErrorMessage).ToArray()));
                };
            })
            .AddFluentValidation(c =>
            {
                c.ImplicitlyValidateChildProperties = true;
                c.ImplicitlyValidateRootCollectionElements = true;
                c.RegisterValidatorsFromAssemblies(validatorsAssemblies);
            });
        return services;
    }
}