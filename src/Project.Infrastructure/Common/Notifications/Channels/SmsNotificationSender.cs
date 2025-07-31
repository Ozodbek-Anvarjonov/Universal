using Project.Application.Common.Notifications.Channels;
using Project.Application.Common.Notifications.Models;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Channels;

public class SmsNotificationSender : INotificationSenderChannel
{
    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Sms;

    public Task<SendResult> SendAsync(ChannelContext channel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}