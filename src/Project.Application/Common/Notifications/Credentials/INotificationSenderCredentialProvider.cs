using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Credentials;

public interface INotificationSenderCredentialProvider
{
    INotificationSenderCredential GetCredential(NotificationType type, NotificationChannelType channelType);
}