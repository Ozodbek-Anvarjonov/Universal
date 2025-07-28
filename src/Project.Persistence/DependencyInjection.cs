using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Persistence.DataContexts;
using Project.Persistence.Interceptors;
using Project.Persistence.Repositories;
using Project.Persistence.UnitOfWork;
using Project.Persistence.UnitOfWork.Interfaces;

namespace Project.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddServices();

        return services;
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditCreationInterceptor>();
        services.AddScoped<AuditModificationInterceptor>();
        services.AddScoped<AuditDeletionInterceptor>();

        //services.AddScoped<DbContext>();

        services.AddDbContext<DbContext, AppDbContext>((provider, options) =>
        {
            var auditCreationInterceptor = provider.GetRequiredService<AuditCreationInterceptor>();
            var auditModificationInterceptor = provider.GetRequiredService<AuditModificationInterceptor>();
            var auditDeletionInterceptor = provider.GetRequiredService<AuditDeletionInterceptor>();

            options
                .UseNpgsql(configuration.GetConnectionString("DefaultDbConnection"))
                .AddInterceptors(auditCreationInterceptor)
                .AddInterceptors(auditModificationInterceptor)
                .AddInterceptors(auditDeletionInterceptor);
        });
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
    }
}