using Project.Application.Common.Notifications.Channels;
using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Services;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Channels;

public class EmailNotificationChannelSender(IEmailService emailService) : INotificationSenderChannel
{
    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Email;

    public async Task<SendResult> SendAsync(ChannelContext channel, CancellationToken cancellationToken = default)
    {
        var result = await emailService.SendAsync(channel, cancellationToken);

        return  result;
    }
}