using Project.Application.Common.Notifications.Channels;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Channels;

public class SmsNotificationSender : INotificationChannelSender
{
    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Sms;

    public Task<bool> SendAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}