using Project.Application.Common.Notifications.Formatters;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Formatters;

public class SmsNotificationFormatter : INotificationFormatter
{
    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Sms;

    public string FormatMessage(string message)
    {
        throw new NotImplementedException();
    }

    public string FormatTitle(string title)
    {
        throw new NotImplementedException();
    }
}