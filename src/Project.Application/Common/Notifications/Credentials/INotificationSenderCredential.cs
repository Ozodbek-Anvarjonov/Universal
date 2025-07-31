using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Credentials;

public interface INotificationSenderCredential
{
    NotificationType Type { get; }

    NotificationChannelType ChannelType { get; }
}