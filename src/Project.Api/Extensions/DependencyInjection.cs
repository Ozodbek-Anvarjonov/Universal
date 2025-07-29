using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using Project.Api.ExceptionHandlers;
using Project.Api.Routing;
using Project.Api.Services;
using Project.Application.Common.Response;
using Project.Application.Settings;
using Project.Persistence.UnitOfWork.Interfaces;
using System.Reflection;
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

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler();
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
        services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
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

        return services;
    }
}