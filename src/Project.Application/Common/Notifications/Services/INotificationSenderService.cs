using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Entities;
using Project.Domain.Enums;
using System.Collections.ObjectModel;

namespace Project.Application.Common.Notifications.Services;

public interface INotificationSenderService
{
    Task SendAsync(Notification notification, NotificationTemplateContext context, CancellationToken cancellationToken = default);

    Task SendAsync(
        Notification notification,
        NotificationTemplateContext context,
        IEnumerable<NotificationChannelType> channelTypes,
        CancellationToken cancellationToken = default
        );
}