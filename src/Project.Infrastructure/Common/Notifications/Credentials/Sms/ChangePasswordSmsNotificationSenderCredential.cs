using Microsoft.Extensions.Options;
using Project.Domain.Enums;
using Project.Infrastructure.Common.Notifications.Credentials.Sms.Options;

namespace Project.Infrastructure.Common.Notifications.Credentials.Sms;

public class ChangePasswordSmsNotificationSenderCredential : SmsNotificationSenderCredential
{
    public override NotificationType Type { get; } = NotificationType.ChangePassword;

    public ChangePasswordSmsNotificationSenderCredential(IOptions<ChangePasswordSmsNotificationSenderCredentialOptions> options)
    {
        Provider = options.Value.Provider;
        ApiUrl = options.Value.ApiUrl;
        ApiToken = options.Value.ApiToken;
        SenderName = options.Value.SenderName;
    }
}