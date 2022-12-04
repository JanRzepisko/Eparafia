using System.Reflection;
using Eparafia.Application.Exceptions;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation.AspNetCore;

namespace Eparafia.API.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddFluentValidators(this IServiceCollection services, params Assembly[] validatorsAssemblies)
    {
        services.AddMvc()
            .AddJsonOptions(c => {
                c.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                c.JsonSerializerOptions.MaxDepth = 32;
                c.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            .ConfigureApiBehaviorOptions(c => {
                c.InvalidModelStateResponseFactory = c => {
                    throw new InvalidRequestException(c.ModelState.Keys.Select(a => a)
                        .ToDictionary(a => a, a => c.ModelState[a].Errors.Select(a => a.ErrorMessage).ToArray()));
                };
            })
            .AddFluentValidation(c=> {
                c.ImplicitlyValidateChildProperties = true;
                c.ImplicitlyValidateRootCollectionElements = true;
                c.RegisterValidatorsFromAssemblies(validatorsAssemblies);
            });
            
        return services;
    }
}