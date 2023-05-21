using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Shared.Service.Implementations.MessageBus;
using Shared.Service.Interfaces.MessageBus;

namespace Shared.Extensions;


public static partial class IServiceCollectionExtensions
{
    
    public static IServiceCollection AddMessageBusConnection(this IServiceCollection services, Action<IMessageBusConnectionBuilder<IBusClient>> connectionBuild)
    {
        services.AddTransient<IEventConsumerWrapper, EventConsumerWrapper>();
        var builderInstance = new MessageBusConnectionBuilder(services);
        connectionBuild.Invoke(builderInstance);
        services.AddSingleton(builderInstance.Build());
        services.AddScoped<IMessageBusClient, MessageBusClient>();
        return services;
    }
}