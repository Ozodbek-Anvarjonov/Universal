using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Persistence.DataContexts;
using Project.Persistence.Repositories;

namespace Project.Persistence.UnitOfWork.Interfaces;

public interface IUnitOfWork<TContext> : ITransactionManager, IDisposable, IAsyncDisposable
    where TContext : DbContext
{
    IRepository<User, TContext> Users { get; }

    IRepository<RefreshToken, TContext> RefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IUnitOfWork : IUnitOfWork<AppDbContext>
{
}