using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Entities;

namespace Project.Application.Common.Notifications.Services;

public interface INotificationSenderService
{
    Task SendAsync(Notification notification, NotificationTemplateContext context, CancellationToken cancellationToken = default);
}