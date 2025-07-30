using Project.Application.Dtos.Accounts;
using Project.Domain.Entities;

namespace Project.Application.Common.Identities;

public interface IAccountService
{
    Task<User> GetAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<bool> ChangePasswordAsync(ChangePasswordRequest request, bool saveChanges = true, CancellationToken cancellationToken = default);
}