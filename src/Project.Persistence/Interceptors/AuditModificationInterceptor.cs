using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Project.Domain.Common.Entities;
using Project.Persistence.UnitOfWork.Interfaces;

namespace Project.Persistence.Interceptors;

public class AuditModificationInterceptor(IUserContext userContext) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var entries = eventData.Context?.ChangeTracker
            .Entries<IAuditableEntity>()
            .Where(entry => entry.State == EntityState.Modified)
            .ToList();

        entries?.ForEach(entry =>
        {
            entry.Entity.ModifiedById = userContext.GetCurrentUserId();
            entry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
        });

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var entries = eventData.Context?.ChangeTracker
            .Entries<IAuditableEntity>()
            .Where(entry => entry.State == EntityState.Modified)
            .ToList();

        entries?.ForEach(entry =>
        {
            entry.Entity.ModifiedById = userContext.GetCurrentUserId();
            entry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
        });

        return base.SavingChanges(eventData, result);
    }
}