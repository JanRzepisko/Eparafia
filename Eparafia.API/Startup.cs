using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Eparafia.Application.DataAccess;
using Eparafia.Application.Services.FileManager;
using Eparafia.Application.Services.Jwt;
using Eparafia.Application.Services.UserProvider;
using Eparafia.Infrastructure.DataAccess;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
        services.AddCors();
        services.AddMediatR(typeof(Program).Assembly);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Eparafia", Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Jwt: Bearer {jwt token}"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        services.AddMvc().AddJsonOptions(c =>
        {
            c.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            c.JsonSerializerOptions.MaxDepth = 32;
            c.JsonSerializerOptions.PropertyNamingPolicy = null;
            c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            c.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            c.JsonSerializerOptions.WriteIndented = true;
        }).AddFluentValidation(c => { c.RegisterValidatorsFromAssemblies(new[] {typeof(Program).Assembly}); });
        
        services.Configure<string>(Configuration);

        services.AddDbContext<DataContext>(options => options.UseNpgsql(Configuration["ConnectionString"]));
        services.AddScoped<IUnitOfWork>(c => c.GetRequiredService<DataContext>());
        services.AddScoped<IJwtAuth, JwtAuth>();
        services.AddScoped<IUserProvider, UserProvider>();
        services.AddScoped<IFileManager, FileManager>();

        services.AddEndpointsApiExplorer();

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
            options.AddDefaultPolicy(policyBuilder =>
                policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        services.AddControllers();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseCors(c => c
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

