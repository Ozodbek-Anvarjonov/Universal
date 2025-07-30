using Project.Application.Common.Notifications;
using Project.Domain.Entities;

namespace Project.Infrastructure.Common.Notifications;

public class NotificationSenderService : INotificationSenderService
{
    public Task SendAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}