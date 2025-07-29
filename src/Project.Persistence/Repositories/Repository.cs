using Microsoft.EntityFrameworkCore;
using Project.Domain.Common.Entities;
using Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Project.Persistence.Repositories;

public class Repository<TEntity, TContext>(TContext context) : IRepository<TEntity, TContext>
    where TEntity : Entity
    where TContext : DbContext
{
    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default)
    {
        var query = context.Set<TEntity>().AsQueryable();

        return predicate is not null ? query.Where(predicate) : query;
    }

    public async Task<TEntity?> GetByIdAsync(long id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = context.Set<TEntity>().AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<TEntity> CreateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(entity, cancellationToken);

        if (saveChanges)
            await context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        context.Update(entity);

        if (saveChanges)
            await context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<bool> DeleteAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        context.Remove(entity);

        if (saveChanges)
            await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken);
}

public class Repository<TEntity>(AppDbContext dbContext) : Repository<TEntity, AppDbContext>(dbContext), IRepository<TEntity>
    where TEntity : Entity
{
}