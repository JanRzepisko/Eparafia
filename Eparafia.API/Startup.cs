using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Eparafia.API.Extensions;
using Eparafia.API.Middlewares;
using Eparafia.Application.DataAccess;
using Eparafia.Application.Services.FileManager;
using Eparafia.Application.Services.Jwt;
using Eparafia.Application.Services.UserProvider;
using Eparafia.Infrastructure.DataAccess;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

        
        services.AddMediatR(typeof(Application.AssemblyEntryPoint).GetTypeInfo().Assembly);
        services.AddFluentValidators(typeof(Application.AssemblyEntryPoint).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviour<,>));
        services.Configure<string>(Configuration);
        services.AddDbContext<DataContext>(options => { options.UseNpgsql(Configuration["ConnectionString"]!); });

        services.AddScoped<DbContext, DataContext>();
        services.AddScoped<IUnitOfWork>(provider => provider.GetService<DataContext>()!);
        services.AddScoped<IJwtAuth, JwtAuth>();
        services.AddScoped<IUserProvider, UserProvider>();
        services.AddScoped<IFileManager, FileManager>();
        
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
        });

        
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        services.AddControllers();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseMiddleware<SetUserMiddleware>();
        
        app.UseCors(c => c
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        // app.UseDeveloperExceptionPage();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapSwagger();
        });
    }
}

