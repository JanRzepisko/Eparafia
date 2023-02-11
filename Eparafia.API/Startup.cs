using Eparafia.Application.DataAccess;
using Eparafia.Application.Services;
using Eparafia.Application.Services.Jwt;
using Eparafia.Infrastructure.DataAccess;
using Eparafia.Infrastructure.Services;
using Shared.BaseModels.Jwt;
using Shared.Extensions;

namespace Eparafia.API;

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

        //Configure Service
        services.Configure<string>(Configuration);
        services.AddSharedServices<Application.AssemblyEntryPoint, DataContext, IUnitOfWork>(JwtLogin.FromConfiguration(Configuration), connectionString, serviceName);
        //Configure RabbitMQ
        //services.AddMassTransit(c =>
        //{
        //    //Add All Consumers
        //    c.UsingRabbitMq((ctx, cfg) =>
        //    {
        //        cfg.Host("host.docker.internal", h =>
        //        {
        //            h.Username("libman");
        //            h.Password("!Malinka@pass");
        //        });
        //        
        //        //Add All Consumers
        //        cfg.ConfigureEndpoints(ctx);
        //    });
        //});
        
        services.AddScoped<IIntentionService, IntentionService>();
        services.AddScoped<IJwtAuth, JwtAuth>();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => app.ConfigureApplication(Configuration);
}

