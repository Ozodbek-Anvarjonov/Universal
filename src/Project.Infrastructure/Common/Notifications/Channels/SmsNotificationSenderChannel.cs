using Project.Application.Common.Notifications.Channels;
using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Services;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Channels;

public class SmsNotificationSenderChannel(ISmsService smsService) : INotificationSenderChannel
{
    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Sms;

    public async Task<SendResult> SendAsync(ChannelContext channel, CancellationToken cancellationToken = default)
    {
        var result = await smsService.SendAsync(channel, cancellationToken);

        return result;
    }
}