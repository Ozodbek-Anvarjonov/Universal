using Project.Application.Common.Exceptions;
using Project.Application.Common.Notifications.Channels;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Channels;

public class NotificationChannelSenderProvider : INotificationSenderChannelProvider
{
    private readonly Dictionary<NotificationChannelType, INotificationSenderChannel> channelMap;

    public NotificationChannelSenderProvider(IEnumerable<INotificationSenderChannel> channels)
    {
        channelMap = new();

        foreach (var channel in channels)
        {
            channelMap[channel.ChannelType] = channel;
        }
    }

    public INotificationSenderChannel GetChannel(NotificationChannelType channelType)
    {
        if (channelMap.TryGetValue(channelType, out var channel)) return channel;

        throw new NotFoundException($"Channel not found for channel {channelType}.");
    }
}