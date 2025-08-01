using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Common.Identities;
using Project.Application.Common.Notifications.Channels;
using Project.Application.Common.Notifications.Credentials;
using Project.Application.Common.Notifications.Formatters;
using Project.Application.Common.Notifications.Services;
using Project.Application.Common.Notifications.Templates;
using Project.Application.Services;
using Project.Infrastructure.Common.Caching;
using Project.Infrastructure.Common.Identities;
using Project.Infrastructure.Common.Notifications;
using Project.Infrastructure.Common.Notifications.Channels;
using Project.Infrastructure.Common.Notifications.Credentials;
using Project.Infrastructure.Common.Notifications.Credentials.Emails;
using Project.Infrastructure.Common.Notifications.Credentials.Emails.Options;
using Project.Infrastructure.Common.Notifications.Credentials.Sms;
using Project.Infrastructure.Common.Notifications.Credentials.Sms.Options;
using Project.Infrastructure.Common.Notifications.Formatters;
using Project.Infrastructure.Common.Notifications.Services;
using Project.Infrastructure.Common.Notifications.Templates;
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
        services.AddNotifications(configuration);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<INotificationService, NotificationService>();

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

    private static IServiceCollection AddNotifications(this IServiceCollection services, IConfiguration configuration)
    {
        // services
        services.AddScoped<IEmailService, SmtpEmailService>();
        services.AddScoped<ISmsService, EskizSmsService>();

        // base
        services.AddScoped<INotificationSenderService, NotificationSenderService>();
        services.AddScoped<INotificationSenderChannelProvider, NotificationSenderChannelProvider>();
        services.AddScoped<INotificationFormatterProvider, NotificationFormatterProvider>();
        services.AddScoped<INotificationTemplateProvider, NotificationTemplateProvider>();
        services.AddScoped<INotificationSenderCredentialProvider, NotificationSenderCredentialProvider>();

        // templates
        services.AddScoped<INotificationTemplate, RegisterNotificationTemplate>();
        services.AddScoped<INotificationTemplate, LoginNotificationTemplate>();
        services.AddScoped<INotificationTemplate, ChangePasswordNotificationTemplate>();

        // formatters
        services.AddScoped<INotificationFormatter, EmailNotificationFormatter>();
        services.AddScoped<INotificationFormatter, SmsNotificationFormatter>();

        // channels
        services.AddScoped<INotificationSenderChannel, EmailNotificationSenderChannel>();
        services.AddScoped<INotificationSenderChannel, SmsNotificationSenderChannel>();

        // credentials
        services.AddScoped<INotificationSenderCredential, RegisterSmsNotificationSenderCredential>();
        services.AddScoped<INotificationSenderCredential, RegisterEmailNotificationSenderCredential>();
        services.AddScoped<INotificationSenderCredential, LoginEmailNotificationSenderCredential>();

        return services;
    }
}