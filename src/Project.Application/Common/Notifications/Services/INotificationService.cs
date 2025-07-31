using Project.Application.Common.Filters;
using Project.Domain.Entities;

namespace Project.Application.Common.Notifications.Services;

public interface INotificationService
{
    Task<IEnumerable<Notification>> GetAsync(NotificationFilter filter, bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<Notification> GetByIdAsync(long id, bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<Notification> CreateAsync(Notification notification, bool saveChanges = true, CancellationToken cancellationToken = default);

    Task<Notification> UpdateAsync(long id, Notification notification, bool saveChanges = true, CancellationToken cancellationToken = default);

    Task<bool> DeleteByIdAsync(long id, bool saveChanges = true, CancellationToken cancellationToken = default);
}