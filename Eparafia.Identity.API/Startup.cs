using Eparafia.Application.Services.Jwt;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Application.Services;
using Eparafia.Identity.Infrastructure.DataAccess;
using MassTransit;
using Shared.BaseModels.Jwt;
using Shared.Extensions;

namespace Eparafia.Identity.API;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString = Configuration["ConnectionString"];
        string serviceName = Configuration["ServiceName"];
        RabbitMQLogin rabbitMQLogin = RabbitMQLogin.FromConfiguration(Configuration);
        
        //Configure Service
        services.Configure<string>(Configuration);
        services.AddSharedServices<Application.AssemblyEntryPoint, DataContext, IUnitOfWork>(JwtLogin.FromConfiguration(Configuration), connectionString, serviceName);
        services.AddScoped<IJwtAuth, JwtAuth>();

        //Configure RabbitMQ
        services.AddMassTransit(c =>
        { 
            //Add All Consumers
            c.BuildRabbitMQ(rabbitMQLogin);
        });
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => app.ConfigureApplication(Configuration);
}

