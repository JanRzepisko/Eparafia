using Eparafia.Bible.Application.DataAccess;
using Eparafia.Bible.Infrastructure.DataAccess;
using Shared.BaseModels.Jwt;
using Shared.Extensions;
using Shared.Extensions.ConfigureApp;

namespace Eparafia.Bible.API;

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
        services.AddSharedServices<Application.AssemblyEntryPoint, DataContext, IUnitOfWork>(JwtLogin.FromConfiguration(Configuration), connectionString, serviceName, RabbitMQLogin.FromConfiguration(Configuration));
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => app.ConfigureApplication(Configuration);
}

