using Project.Application.Common.Filters;
using Project.Domain.Entities;

namespace Project.Application.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAsync(UserFilter filter, bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<User> GetByIdAsync(long id, bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    Task<User> UpdateAsync(long id, User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    Task<bool> DeleteByIdAsync(long id, bool saveChanges = true, CancellationToken cancellationToken = default);
}