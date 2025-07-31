using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Channels;

public interface INotificationSenderChannel
{
    NotificationChannelType ChannelType { get; }

    Task<SendResult> SendAsync(ChannelContext channel, CancellationToken cancellationToken = default);
}