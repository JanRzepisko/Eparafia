using Eparafia.Administration.Application;
using Eparafia.Administration.Application.DataAccess;
using Eparafia.Administration.Infrastructure.DataAccess;
using Shared.Extensions;
using Shared.Extensions.ConfigureApp;
using Shared.Messages;

namespace Eparafia.Administration.API;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        //Configure Service
        services.Configure<string>(Configuration);
        services.AddSharedServices<AssemblyEntryPoint, DataContext, IUnitOfWork>(Configuration);

        
        services.AddMessageBusConnection(c => c.ApplyConfiguration(Configuration.GetSection("RabbitMQ"))
            .RegisterConsumersFromAssembly(typeof(AssemblyEntryPoint).Assembly)
            .SubscribeToEvent<PriestCreatedBusEvent>()
            .SubscribeToEvent<PriestUpdatedBusEvent>()
            .SubscribeToEvent<PriestRemovedBusEvent>()
            .SubscribeToEvent<ChangedParishPriestBusEvent>()
        );
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => app.ConfigureApplication(Configuration);
}