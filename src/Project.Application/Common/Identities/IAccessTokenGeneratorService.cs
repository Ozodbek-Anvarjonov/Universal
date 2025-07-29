using Project.Domain.Entities;

namespace Project.Application.Common.Identities;

public interface IAccessTokenGeneratorService
{
    Task<string> GenerateAsync(User user, CancellationToken cancellationToken);
}