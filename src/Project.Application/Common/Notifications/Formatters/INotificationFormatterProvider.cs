using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Formatters;

public interface INotificationFormatterProvider
{
    INotificationFormatter GetFormatter(NotificationChannelType channelType);
}