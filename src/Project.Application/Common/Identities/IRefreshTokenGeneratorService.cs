using Project.Domain.Entities;

namespace Project.Application.Common.Identities;

public interface IRefreshTokenGeneratorService
{
    Task<string> GenerateAsync(User user,CancellationToken cancellationToken = default);
}