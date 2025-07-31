using Project.Application.Common.Notifications.Formatters;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Formatters;

public class SmsNotificationFormatter : INotificationFormatter
{
    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Sms;

    public string FormatTitle(string title) => string.Empty;

    public string FormatMessage(string message) =>
        message.Length > 160 ? message.Substring(0, 157) + "..." : message;
}