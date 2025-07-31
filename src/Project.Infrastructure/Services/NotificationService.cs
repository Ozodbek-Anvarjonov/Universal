using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Exceptions;
using Project.Application.Common.Filters;
using Project.Application.Common.Notifications.Services;
using Project.Application.Common.Response;
using Project.Domain.Entities;
using Project.Infrastructure.Common.Extensions;
using Project.Persistence.Repositories;

namespace Project.Infrastructure.Services;

public class NotificationService(
    IRepository<Notification> repository,
    IHeaderWriter writer
    ) : INotificationService
{
    public Task<IEnumerable<Notification>> GetAsync(NotificationFilter filter, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = repository.Get();

        if (asNoTracking)
            query = query.AsNoTracking();

        if (filter.SenderName is not null)
            query = query.Where(entity => entity.SenderName.ToLower().Contains(filter.SenderName.ToLower()));

        if (filter.SenderContact is not null)
            query = query.Where(entity => entity.SenderContact.ToLower().Contains(filter.SenderContact.ToLower()));

        if (filter.ReceiverUserId is not null)
            query = query.Where(entity => entity.ReceiverUserId == filter.ReceiverUserId);

        if (filter.ReceiverUserName is not null)
            query = query.Where(entity => entity.ReceiverUser.FirstName.ToLower().Contains(filter.ReceiverUserName.ToLower())
                || entity.ReceiverUser.LastName.ToLower().Contains(filter.ReceiverUserName.ToLower())
                || (entity.ReceiverUser.MiddleName != null && entity.ReceiverUser.MiddleName.ToLower().Contains(filter.ReceiverUserName.ToLower())));

        if (filter.Type is not null)
            query = query.Where(entity => entity.Type == filter.Type);

        if (filter.ChannelType is not null)
            query = query.Where(entity => entity.ChannelType == filter.ChannelType);

        if (filter.Title is not null)
            query = query.Where(entity => entity.Title.ToLower().Contains(filter.Title.ToLower()));

        if (filter.Message is not null)
            query = query.Where(entity => entity.SenderContact.ToLower().Contains(filter.Message.ToLower()));

        if (filter.IsDelivered is not null)
            query = query.Where(entity => entity.IsDelivered == filter.IsDelivered);

        if (filter.IsRead is not null)
            query = query.Where(entity => entity.IsRead == filter.IsRead);

        if (filter.DeliveredAt is not null)
            query = query.Where(entity => entity.DeliveredAt == filter.DeliveredAt);


        return query.ToPaginateAsync(filter, writer, cancellationToken);
    }

    public async Task<Notification> GetByIdAsync(long id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var exist = await repository.GetByIdAsync(id, asNoTracking, cancellationToken)
            ?? throw new NotFoundException(nameof(Notification), nameof(Notification.Id), id.ToString());

        return exist;
    }

    public async Task<Notification> CreateAsync(Notification notification, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var created = await repository.CreateAsync(notification, saveChanges, cancellationToken);

        return created;
    }

    public async Task<Notification> UpdateAsync(long id, Notification notification, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var exist = await GetByIdAsync(id, asNoTracking: false, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);

        return exist;
    }

    public async Task<bool> DeleteByIdAsync(long id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var delete = await GetByIdAsync(id, false, cancellationToken);

        await repository.DeleteAsync(delete, cancellationToken: cancellationToken);

        return true;
    }
}