using Project.Application.Common.Notifications.Credentials;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Credentials;

public class RegisterEmailNotificationCredential : INotificationSenderCredential
{
    public NotificationType Type { get; } = NotificationType.Register;

    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Email;
}