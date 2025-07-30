using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Channels;

public interface INotificationChannelSenderProvider
{
    INotificationChannelSender GetChannel(NotificationChannelType channelType);
}