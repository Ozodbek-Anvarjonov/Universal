using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Exceptions;
using Project.Application.Common.Identities;
using Project.Application.Dtos.Login;
using Project.Domain.Entities;
using Project.Persistence.UnitOfWork.Interfaces;
using System.Net;

namespace Project.Infrastructure.Common.Identities;

public class AuthService(
    IPasswordHasherService passwordHasherService,
    IAccessTokenGeneratorService accessTokenGeneratorService,
    IRefreshTokenService refreshTokenService,
    IUnitOfWork unitOfWork
    ) : IAuthService
{
    public async Task<LoginResponse> LoginAsync(LoginWithEmailRequest loginRequest, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users
            .Get(entity => entity.EmailAddress == loginRequest.EmailAddress)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new BadRequestException("Invalid email or password.");

        return await LoginAsync(user, loginRequest.Password, cancellationToken);
    }

    public async Task<LoginResponse> LoginAsync(LoginWithPhoneNumberRequest loginRequest, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users
            .Get(entity => entity.PhoneNumber == loginRequest.PhoneNumber)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new BadRequestException("Invalid email or password.");

        return await LoginAsync(user, loginRequest.Password, cancellationToken);
    }

    public async Task<bool> RegisterAsync(User user, CancellationToken cancellationToken = default)
    {
        var createdUser = await unitOfWork.Users.CreateAsync(user, cancellationToken: cancellationToken);

        return createdUser != null;
    }

    public async Task<LoginResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var existToken = await refreshTokenService.GetValidTokenAsync(refreshToken, asNoTracking: false, cancellationToken);

        await unitOfWork.RefreshTokens.DeleteAsync(existToken, cancellationToken: cancellationToken);

        var newRefreshToken = await refreshTokenService.CreateAsync(existToken.User, cancellationToken: cancellationToken);
        var newAccessToken = await accessTokenGeneratorService.GenerateAsync(existToken.User, cancellationToken: cancellationToken);

        return new LoginResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
        };
    }

    public async Task<bool> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var existToken = await refreshTokenService.GetValidTokenAsync(refreshToken, asNoTracking: false, cancellationToken);

        await unitOfWork.RefreshTokens.DeleteAsync(existToken, cancellationToken: cancellationToken);

        return true;
    }

    private async Task<LoginResponse> LoginAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        var verify = await passwordHasherService.VerifyAsync(password, user.Password, cancellationToken);
        if (!verify)
            throw new BadRequestException("Invalid email or password");

        if (!user.IsActive)
            throw new CustomException("Your account has been blocked. Please contact support.", HttpStatusCode.Forbidden);

        var refreshToken = await refreshTokenService.CreateAsync(user, cancellationToken: cancellationToken);
        var accessToken = await accessTokenGeneratorService.GenerateAsync(user, cancellationToken: cancellationToken);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
        };
    }
}