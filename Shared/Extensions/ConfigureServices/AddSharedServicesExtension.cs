using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Shared.BaseModels;
using Shared.BaseModels.Jwt;
using Shared.Behaviours;
using Shared.Service.Implementations;
using Shared.Service.Implementations.MessageBus;
using Shared.Service.Interfaces;
using Shared.Service.Interfaces.MessageBus;

namespace Shared.Extensions;

public static class AddSharedServicesExtension
{
    public static IServiceCollection AddSharedServices<AssemblyEntryPoint, DataContext, UnitOfWork>(this IServiceCollection services, IConfiguration appCnf)
        where UnitOfWork : class
        where DataContext : DbContext, UnitOfWork
    {
        var jwtLogin = JwtLogin.FromConfiguration(appCnf);
        var connectionString = appCnf["ConnectionString"];
        var serviceName = appCnf["ServiceName"];
        
        services.AddMediatR(typeof(AssemblyEntryPoint).GetTypeInfo().Assembly);
        services.AddFluentValidators(typeof(AssemblyEntryPoint).Assembly);

        services.AddDatabase<DataContext, UnitOfWork>(connectionString);
        services.AddFluentValidators(typeof(AssemblyEntryPoint).Assembly);
        services.AddJwtAuthorization(jwtLogin);
        services.AddOptions();

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policyBuilder => policyBuilder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
        });
        
        services.AddControllers();
        services.AddSwagger(serviceName);

        services.AddStackExchangeRedisCache(options =>
        {
            var connection = appCnf["RedisConnectionString"];
            options.Configuration = connection;
        });
        
        //Add Services
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

        services.AddTransient<IMessageBusConnectionBuilder<IBusClient>, MessageBusConnectionBuilder>();
        
        services.AddDistributedMemoryCache();
        services.AddSingleton<IAuthorizationCache, AuthorizationCache>();

        services.AddScoped<IUserProvider, UserProvider>();
        services.AddScoped<IFileManager, FileManager>();
        return services;
    }
}