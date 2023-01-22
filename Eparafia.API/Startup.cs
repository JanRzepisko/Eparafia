using System.Reflection;
using System.Text;
using Eparafia.API.Extensions;
using Eparafia.API.Middlewares;
using Eparafia.Application.DataAccess;
using Eparafia.Application.Services;
using Eparafia.Application.Services.FileManager;
using Eparafia.Application.Services.Jwt;
using Eparafia.Application.Services.UserProvider;
using Eparafia.Infrastructure.DataAccess;
using Eparafia.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        services.AddSwagger();
        services.AddOptions();

        services.AddEndpointsApiExplorer();
        services.AddMediatR(typeof(Application.AssemblyEntryPoint).GetTypeInfo().Assembly);
        services.AddFluentValidators(typeof(Application.AssemblyEntryPoint).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviour<,>));
        services.Configure<string>(Configuration);

        services.AddDbContext<DataContext>(options =>
        {
            options.UseMySql(Configuration["ConnectionString"], ServerVersion.AutoDetect(Configuration["ConnectionString"]));
        });
        
        services.AddScoped<DbContext, DataContext>();
        services.AddScoped<IUnitOfWork>(provider => provider.GetService<DataContext>()!);
        services.AddScoped<IJwtAuth, JwtAuth>();
        services.AddScoped<IUserProvider, UserProvider>();
        services.AddScoped<IFileManager, FileManager>();
        services.AddScoped<IIntentionService, IntentionService>();

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });
        
        Console.WriteLine("Token: " + Configuration["Jwt:Key"]);
        Console.WriteLine("ConnectionString: " + Configuration["ConnectionString"]);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"] ?? string.Empty)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });
        services.AddAuthorization(config =>
        {
            config.AddPolicy(JwtPolicies.Admin, JwtPolicies.AdminPolicy());
            config.AddPolicy(JwtPolicies.User, JwtPolicies.UserPolicy());
            config.AddPolicy(JwtPolicies.Priest, JwtPolicies.PriestPolicy());
        });
        
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        services.AddControllers();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        
        app.UseHttpsRedirection();
        app.UseCors(c => c
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<SetUserMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapSwagger();
            endpoints.MapGet("/" , async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        });

        
    }
}

