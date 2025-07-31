using Project.Application.Common.Notifications.Credentials;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Credentials.Sms;

public abstract class SmsNotificationSenderCredential : INotificationSenderCredential
{
    public abstract NotificationType Type { get; }

    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Sms;

    public string Provider { get; set; } = default!;

    public string ApiUrl { get; set; } = default!;

    public string ApiToken { get; set; } = default!;

    public string SenderName { get; set; } = default!;
}