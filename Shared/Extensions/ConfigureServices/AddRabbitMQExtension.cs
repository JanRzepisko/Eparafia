using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Extensions;

public static partial class RabbitMQExtension
{
    public static void CreateQueue<T>(this IRabbitMqBusFactoryConfigurator cfg, string serviceName, IRegistrationContext ctx) where T : class, IConsumer
    {
        cfg.ReceiveEndpoint("hello", e =>
        {
            e.ConfigureConsumer<T>(ctx);
        });
    }
    
    public static void Subscribe<T>(this IBusRegistrationConfigurator bus) where T : class, IConsumer
    {
        bus.AddConsumer<T>();
    }
}