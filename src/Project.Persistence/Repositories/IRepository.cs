using Microsoft.EntityFrameworkCore;
using Project.Domain.Common.Entities;
using Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Project.Persistence.Repositories;

public interface IRepository<TEntity, TContext>
    where TEntity : IEntity
    where TContext : DbContext
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default);

    Task<TEntity?> GetByIdAsync(long id, bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<TEntity> CreateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IRepository<TEntity> : IRepository<TEntity, AppDbContext>
    where TEntity : IEntity
{
}