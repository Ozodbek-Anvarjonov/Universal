using Project.Domain.Entities;

namespace Project.Application.Common.Notifications;

public interface INotificationSenderService
{
    Task SendAsync(Notification notification, CancellationToken cancellationToken = default);
}