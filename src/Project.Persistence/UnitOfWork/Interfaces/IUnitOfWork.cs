using Project.Persistence.DataContexts;

namespace Project.Persistence.UnitOfWork.Interfaces;

public interface IUnitOfWork<TContext> : ITransactionManager, IDisposable, IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IUnitOfWork: IUnitOfWork<AppDbContext>
{
}