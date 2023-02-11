using Microsoft.Extensions.Configuration;

namespace Shared.BaseModels.Jwt;

public class RabbitMQLogin
{
    public string Host { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    
    public static RabbitMQLogin FromConfiguration(IConfiguration configuration)
    {
        return new RabbitMQLogin
        {
            Host =configuration["RabbitMQ:Host"],
            Username = configuration["RabbitMQ:Username"],
            Password = configuration["RabbitMQ:Password"]
        };
    }
}