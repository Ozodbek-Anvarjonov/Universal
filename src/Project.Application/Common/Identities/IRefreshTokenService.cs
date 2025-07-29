using Project.Domain.Entities;

namespace Project.Application.Common.Identities;

public interface IRefreshTokenService
{
    Task<RefreshToken> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    Task<RefreshToken> GetValidTokenAsync(string token, bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<bool> ValidateAsync(string token, CancellationToken cancellationToken = default);

    Task InvalidateAsync(string token, CancellationToken cancellationToken = default);
    
    Task InvalidateAllAsync(long userId, CancellationToken cancellationToken = default);
}