using Eparafia.Application.Services.Jwt;
using Eparafia.Identity.Application;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Application.Services;
using Eparafia.Identity.Infrastructure.DataAccess;
using Shared.BaseModels.Jwt;
using Shared.Extensions;
using Shared.Extensions.ConfigureApp;

namespace Eparafia.Identity.API;

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

        //Configure Service
        services.Configure<string>(Configuration);
        services.AddSharedServices<AssemblyEntryPoint, DataContext, IUnitOfWork>(JwtLogin.FromConfiguration(Configuration), connectionString, serviceName);
        services.AddScoped<IJwtAuth, JwtAuth>();
        
        services.AddMessageBusConnection(c => c.ApplyConfiguration(Configuration.GetSection("RabbitMQ"))
            .RegisterConsumersFromAssembly(typeof(AssemblyEntryPoint).Assembly));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.ConfigureApplication(Configuration);
    }
}