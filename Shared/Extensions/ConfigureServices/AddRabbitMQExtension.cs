using MassTransit;
using RabbitMQ.Client;
using Shared.BaseModels.Jwt;

namespace Shared.Extensions;

public static class RabbitMQExtension
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

            cfg.ExchangeType = "topic";
            cfg.ConfigureEndpoints(ctx);
        });

        return conf;
    }
    

}