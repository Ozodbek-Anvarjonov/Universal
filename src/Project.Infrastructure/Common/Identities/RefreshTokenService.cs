using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Application.Common.Exceptions;
using Project.Application.Common.Identities;
using Project.Application.Settings;
using Project.Domain.Entities;
using Project.Persistence.Repositories;
using System.Net;

namespace Project.Infrastructure.Common.Identities;

public class RefreshTokenService(
    IOptions<JwtSettings> options,
    IRefreshTokenGeneratorService tokenGeneratorService,
    IRepository<RefreshToken> repository
    ) : IRefreshTokenService
{
    public async Task<RefreshToken> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var token = await tokenGeneratorService.GenerateAsync(user, cancellationToken);

        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = token,
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(options.Value.RefreshTokenLifeTimeInDays),
        };

        await repository.CreateAsync(refreshToken, saveChanges, cancellationToken);

        return refreshToken;
    }

    public async Task<RefreshToken> GetValidTokenAsync(string token, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = repository.Get(entity => entity.Token == token);

        if (asNoTracking)
            query = query.AsNoTracking();

        var refreshToken = await query
            .Include(entity => entity.User)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new CustomException("Refresh token is invalid or does not exist.", HttpStatusCode.Unauthorized);

        if (refreshToken.ExpiresAt < DateTimeOffset.UtcNow)
            throw new CustomException("Refresh token has expired.", HttpStatusCode.Unauthorized);

        return refreshToken;
    }

    public async Task<bool> ValidateAsync(string token, CancellationToken cancellationToken = default)
    {
        var existToken = await repository
            .Get(entity => entity.Token == token)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return existToken != null;
    }

    public async Task InvalidateAllAsync(long userId, CancellationToken cancellationToken = default)
    {
        await repository
            .Get(entity => entity.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task InvalidateAsync(string token, CancellationToken cancellationToken = default)
    {
        var existToken = await repository
            .Get(entity => entity.Token == token)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(RefreshToken), nameof(RefreshToken.Token), token);

        await repository.DeleteAsync(existToken, cancellationToken: cancellationToken);
    }
}