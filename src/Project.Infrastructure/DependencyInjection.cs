using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Common.Identities;
using Project.Application.Services;
using Project.Infrastructure.Common.Caching;
using Project.Infrastructure.Common.Identities;
using Project.Infrastructure.Services;
using Project.Persistence.Caching.Brokers;

namespace Project.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddIdentities();
        services.AddCaching();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    private static IServiceCollection AddIdentities(this IServiceCollection services)
    {
        services.AddScoped<IAccessTokenGeneratorService, AccessTokenGeneratorService>();
        services.AddScoped<IRefreshTokenGeneratorService, RefreshTokenGeneratorService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }

    private static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheBroker, MemoryCacheBroker>();
        //services.AddSingleton<ICacheBroker, RedisDistributedCacheBroker>();
        //services.AddDistributedMemoryCache();

        return services;
    }
}