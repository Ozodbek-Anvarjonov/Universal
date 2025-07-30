using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Formatters;

public interface INotificationFormatter
{
    NotificationChannelType ChannelType { get; }

    string FormatTitle(string title);
    string FormatMessage(string message);
}