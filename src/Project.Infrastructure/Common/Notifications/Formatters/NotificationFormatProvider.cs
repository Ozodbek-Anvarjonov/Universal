using Project.Application.Common.Exceptions;
using Project.Application.Common.Notifications.Formatters;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Formatters;

public class NotificationFormatProvider : INotificationFormatterProvider
{
    private readonly Dictionary<NotificationChannelType, INotificationFormatter> formatterMap;

    public NotificationFormatProvider(IEnumerable<INotificationFormatter> formatters)
    {
        formatterMap = new ();

        foreach (var formatter in formatters)
        {
            formatterMap[formatter.ChannelType] = formatter;
        }
    }

    public INotificationFormatter GetFormatter(NotificationChannelType channelType)
    {
        if (formatterMap.TryGetValue(channelType, out var formatter)) return formatter;

        throw new NotFoundException($"Formatter not found for channel {channelType}");
    }
}