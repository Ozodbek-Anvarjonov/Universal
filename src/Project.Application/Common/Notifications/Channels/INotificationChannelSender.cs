using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Channels;

public interface INotificationChannelSender
{
    NotificationChannelType ChannelType { get; }

    Task<bool> SendAsync(Notification notification, CancellationToken cancellationToken = default);
}