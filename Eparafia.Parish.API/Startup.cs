using Eparafia.Application;
using Eparafia.Application.DataAccess;
using Eparafia.Application.Services;
using Eparafia.Infrastructure.DataAccess;
using Eparafia.Infrastructure.Services;
using Shared.Extensions;
using Shared.Extensions.ConfigureApp;
using Shared.Messages;

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
        //Configure Service
        services.Configure<string>(Configuration);
        services.AddSharedServices<AssemblyEntryPoint, DataContext, IUnitOfWork>(Configuration);
        services.AddScoped<IIntentionService, IntentionService>();
        
        services.AddMessageBusConnection(c => c.ApplyConfiguration(Configuration.GetSection("RabbitMQ"))
            .RegisterConsumersFromAssembly(typeof(AssemblyEntryPoint).Assembly)
            .SubscribeToEvent<PriestCreatedBusEvent>()
            .SubscribeToEvent<PriestUpdatedBusEvent>()
            .SubscribeToEvent<PriestRemovedBusEvent>());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.ConfigureApplication(Configuration);
    }
}