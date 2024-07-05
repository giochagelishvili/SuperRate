using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SuperRate.Application.Accounts;
using SuperRate.Application.Accounts.Interfaces;
using SuperRate.Application.IBans;
using SuperRate.Application.IBans.Interfaces;
using SuperRate.Application.MatchingOrders;
using SuperRate.Application.MatchingOrders.Interfaces;
using SuperRate.Application.Orders;
using SuperRate.Application.Orders.Interfaces;
using SuperRate.Application.Users;
using SuperRate.Application.Users.Interfaces;
using SuperRate.Domain.Users;
using SuperRate.Infrastructure.IBans;
using SuperRate.Infrastructure.MatchingOrders;
using SuperRate.Infrastructure.Orders;
using SuperRate.Persistence.Context;

namespace SuperRate.API.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IIBanService, IBanService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IMatchingOrderService, MatchingOrderService>();

        services.AddScoped<IIBanRepository, IBanRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IMatchingOrderRepository, MatchingOrderRepository>();
    }

    public static void UseConfiguredCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                corsPolicyBuilder =>
                    corsPolicyBuilder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
        });
    }

    public static void AddDbContextAndIdentity(this IServiceCollection services, IConfiguration configuration,
        ServiceLifetime contextLifeTime = ServiceLifetime.Scoped)
    {
        services.AddDbContext<SuperRateContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
            contextLifetime: contextLifeTime);

        services.AddIdentity<User, IdentityRole<int>>().AddEntityFrameworkStores<SuperRateContext>();
    }

    public static void AddCustomValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true)
            .AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public static void UseSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opts =>
        {
            opts.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Description = "Authorization"
            });

            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void AddTokenAuthorization(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,

                ValidIssuer = config["AuthConfiguration:Issuer"],
                ValidAudience = config["AuthConfiguration:Audience"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["AuthConfiguration:SecretKey"]!))
            };
        });
    }
}