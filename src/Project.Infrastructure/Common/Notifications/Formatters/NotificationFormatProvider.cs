using Project.Application.Common.Notifications.Formatters;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Formatters;

public class NotificationFormatProvider : INotificationFormatterProvider
{
    public INotificationFormatter GetFormatter(NotificationChannelType channelType)
    {
        throw new NotImplementedException();
    }
}