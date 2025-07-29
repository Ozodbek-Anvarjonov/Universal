using Project.Application.Common.Identities;
using Project.Domain.Entities;
using System.Security.Cryptography;

namespace Project.Infrastructure.Common.Identities;

public class RefreshTokenGeneratorService : IRefreshTokenGeneratorService
{
    public Task<string> GenerateAsync(User user, CancellationToken cancellationToken = default)
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        var token = Convert.ToBase64String(bytes);

        return Task.FromResult(token);
    }
}