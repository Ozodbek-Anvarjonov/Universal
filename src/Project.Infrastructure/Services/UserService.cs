using Project.Application.Common.Exceptions;
using Project.Application.Common.Filters;
using Project.Application.Common.Response;
using Project.Application.Services;
using Project.Domain.Entities;
using Project.Persistence.Repositories;

namespace Project.Infrastructure.Services;

public class UserService(IRepository<User> repository, IHeaderWriter writer) : IUserService
{
    public Task<IEnumerable<User>> GetAsync(UserFilter filter, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByIdAsync(long id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var exist = await repository.GetByIdAsync(id, asNoTracking, cancellationToken)
            ?? throw new NotFoundException(nameof(User), nameof(User.Id), id.ToString());

        return exist;
    }

    public async Task<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var created = await repository.CreateAsync(user, saveChanges, cancellationToken);

        return created;
    }

    public async Task<User> UpdateAsync(long id, User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var exist = await GetByIdAsync(id, asNoTracking: false, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);

        return exist;
    }

    public async Task<bool> DeleteByIdAsync(long id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var delete = await GetByIdAsync(id, false, cancellationToken);

        await repository.DeleteAsync(delete);

        return true;
    }
}