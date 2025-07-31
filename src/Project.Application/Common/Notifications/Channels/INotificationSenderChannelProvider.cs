using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Channels;

public interface INotificationSenderChannelProvider
{
    INotificationSenderChannel GetChannel(NotificationChannelType channelType);
}