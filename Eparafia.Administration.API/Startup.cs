using Eparafia.Administration.Application;
using Eparafia.Administration.Application.DataAccess;
using Eparafia.Administration.Infrastructure;
using Eparafia.Administration.Infrastructure.DataAccess;
using MassTransit;
using Shared.BaseModels.Jwt;
using Shared.Extensions;
//TODO remove package get form shared
namespace Eparafia.Administration.API;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

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
            c.BuildRabbitMQ(rabbitMQLogin);
        });
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => app.ConfigureApplication(Configuration);
}