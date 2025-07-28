using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Persistence.DataContexts;
using Project.Persistence.UnitOfWork.Interfaces;

namespace Project.Persistence.UnitOfWork;

public class UnitOfWork<TContext>(TContext context) : IUnitOfWork<TContext>
    where TContext : DbContext
{
    private IDbContextTransaction? currentTransaction;
    private bool disposed;

    public bool HasActiveTransaction => currentTransaction != null;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken);

    #region Transaction Management
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (HasActiveTransaction)
            return;

        currentTransaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction is null)
            throw new InvalidOperationException("There is no active transaction to commit.");

        try
        {
            await context.SaveChangesAsync(cancellationToken);
            await currentTransaction.CommitAsync(cancellationToken);
            await DisposeCurrentTransactionAsync();
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction is null)
            return;

        await currentTransaction.RollbackAsync(cancellationToken);
        await DisposeCurrentTransactionAsync();
    }

    private async Task DisposeCurrentTransactionAsync()
    {
        await currentTransaction!.DisposeAsync();
        currentTransaction = null;
    }
    #endregion

    #region Disposal
    public void Dispose()
    {
        if (disposed) return;

        context.Dispose();
        currentTransaction?.Dispose();
        disposed = true;
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (disposed) return;

        await context.DisposeAsync();
        if (HasActiveTransaction)
            await currentTransaction!.DisposeAsync();
        disposed = true;
        GC.SuppressFinalize(this);
    }
    #endregion
}

public class UnitOfWork(AppDbContext context) : UnitOfWork<AppDbContext>(context)
{
}