using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project.Api.ExceptionHandlers;
using Project.Api.Routing;
using Project.Api.Services;
using Project.Application.Common.Response;
using Project.Application.Settings;
using Project.Infrastructure.Common.Notifications.Credentials.Emails.Options;
using Project.Infrastructure.Common.Notifications.Credentials.Sms.Options;
using Project.Persistence.Caching.Settings;
using Project.Persistence.UnitOfWork.Interfaces;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Project.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddSwagger();
        services.AddControllers();
        services.AddExceptionHandlers();
        services.AddValidators();
        services.AddHttpContextAccessor();
        services.AddMappers();
        services.AddJsonConverter();
        services.AddSettings(configuration);
        services.AddSecurity(configuration);
        services.AddCredentials(configuration);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IHeaderWriter, HeaderWriter>();

        return services;
    }

    private static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<AppExceptionHandler>();
        services.AddExceptionHandler<ArgumentExceptionHandler>();
        services.AddExceptionHandler<InternalServerExceptionHandler>();

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    private static IServiceCollection AddControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new RouteTransformer()));
        });

        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on the textbox below!",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme,
                }
            };

            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() },
            });
            options.DescribeAllParametersInCamelCase();
        });

        return services;
    }

    private static IServiceCollection AddJsonConverter(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }

    private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.Configure<SystemSettings>(configuration.GetSection(nameof(SystemSettings)));
        services.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));

        return services;
    }

    private static IServiceCollection AddCredentials(this IServiceCollection services, IConfiguration configuration)
    {

        // credentials configurations
        // email
        services.Configure<RegisterEmailNotificationSenderCredentialOptions>(configuration.GetSection("NotificationCredentials:Email:Register"));
        services.Configure<LoginEmailNotificationSenderCredentialOptions>(configuration.GetSection("NotificationCredentials:Email:Login"));
        services.Configure<ChangePasswordEmailNotificationSenderCredentialOptions>(configuration.GetSection("NotificationCredentials:Email:ChangePassword"));
        // sms
        services.Configure<RegisterSmsNotificationSenderCredentialOptions>(configuration.GetSection("NotificationCredentials:Sms:Register"));
        services.Configure<LoginSmsNotificationSenderCredentialOptions>(configuration.GetSection("NotificationCredentials:Sms:Login"));
        services.Configure<ChangePasswordSmsNotificationSenderCredentialOptions>(configuration.GetSection("NotificationCredentials:Sms:ChangePassword"));

        return services;
    }

    private static void AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()
            ?? throw new InvalidOperationException($"{nameof(JwtSettings)} is not configurated.");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifeTime,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });
    }
}