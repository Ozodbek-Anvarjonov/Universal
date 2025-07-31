using Project.Application.Common.Notifications.Channels;
using Project.Application.Common.Notifications.Models;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Channels;

public class EmailNotificationSender : INotificationSenderChannel
{
    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Email;

    public Task<SendResult> SendAsync(ChannelContext channel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}