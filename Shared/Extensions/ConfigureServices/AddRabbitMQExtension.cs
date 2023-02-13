using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.BaseModels.Jwt;

namespace Shared.Extensions;

public static partial class RabbitMQExtension
{
    public static IBusRegistrationConfigurator BuildRabbitMQ(this IBusRegistrationConfigurator conf, RabbitMQLogin login)
    {
        conf.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host(login.Host, h =>
            {
                h.Username(login.Username);
                h.Password(login.Password);
            });
                
            //Add All Consumers
            cfg.ConfigureEndpoints(ctx);
        });
        
        return conf;
    }
}