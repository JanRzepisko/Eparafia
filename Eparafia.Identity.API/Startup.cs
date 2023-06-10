using Eparafia.Application.Services.Jwt;
using Eparafia.Identity.Application;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Application.Services;
using Eparafia.Identity.Infrastructure.DataAccess;
using Shared.Extensions;
using Shared.Extensions.ConfigureApp;
using Shared.Messages;

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
        //Configure Service
        services.Configure<string>(Configuration);
        services.AddSharedServices<AssemblyEntryPoint, DataContext, IUnitOfWork>(Configuration);
        services.AddScoped<IJwtAuth, JwtAuth>();

        var cnf = Configuration.GetSection("RabbitMQ");
        services.AddMessageBusConnection(c => c.ApplyConfiguration(cnf)
            .RegisterConsumersFromAssembly(typeof(AssemblyEntryPoint).Assembly)
            .SubscribeToEvent<ChangedParishPriestBusEvent>()
        );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.ConfigureApplication(Configuration);
    }
}