using Project.Api.ExceptionHandlers;
using Project.Api.Services;
using Project.Application.Common.Response;
using Project.Persistence.UnitOfWork.Interfaces;

namespace Project.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddSwaggerGen();
        services.AddControllers();
        services.AddExceptionHandlers();

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
}