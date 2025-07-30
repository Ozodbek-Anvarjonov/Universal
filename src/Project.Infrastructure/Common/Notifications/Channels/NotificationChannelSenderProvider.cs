using Project.Application.Common.Notifications.Channels;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Channels;

public class NotificationChannelSenderProvider : INotificationChannelSenderProvider
{
    public INotificationChannelSender GetChannel(NotificationChannelType channelType)
    {
        throw new NotImplementedException();
    }
}