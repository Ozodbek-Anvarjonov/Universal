using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Services;

public interface INotificationSenderService
{
    Task<Notification> SendAsync(Notification notification, NotificationTemplateContext context, CancellationToken cancellationToken = default);

    Task<IEnumerable<Notification>> SendAsync(
        Notification notification,
        NotificationTemplateContext context,
        IEnumerable<NotificationChannelType> channelTypes,
        CancellationToken cancellationToken = default
        );
}