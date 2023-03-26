using Eparafia.Application;
using Eparafia.Application.DataAccess;
using Eparafia.Application.EventConsumers;
using Eparafia.Application.Services;
using Eparafia.Infrastructure.DataAccess;
using Eparafia.Infrastructure.Services;
using MassTransit;
using Shared.BaseModels.Jwt;
using Shared.Extensions;

namespace Eparafia.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Configuration["ConnectionString"];
        var serviceName = Configuration["ServiceName"];
        var rabbitMQLogin = RabbitMQLogin.FromConfiguration(Configuration);

        //Configure Service
        services.Configure<string>(Configuration);
        services.AddSharedServices<AssemblyEntryPoint, DataContext, IUnitOfWork>(
            JwtLogin.FromConfiguration(Configuration), connectionString, serviceName);


        //Configure RabbitMQ
        services.AddMassTransit(c =>
        {
            //Add All Consumers
            c.AddConsumer<PriestCreatedConsumer>();
            c.AddConsumer<PriestUpdatedConsumer>();
            c.AddConsumer<PriestRemovedConsumer>();
            c.AddConsumer<UserCreatedConsumer>();
            c.AddConsumer<UserUpdatedConsumer>();
            c.AddConsumer<UserRemovedConsumer>();

            c.BuildRabbitMQ(rabbitMQLogin);
        });

        services.AddScoped<IIntentionService, IntentionService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.ConfigureApplication(Configuration);
    }
}