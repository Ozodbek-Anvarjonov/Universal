using Project.Application.Dtos.Login;
using Project.Domain.Entities;

namespace Project.Application.Common.Identities;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginWithEmailRequest loginRequest, CancellationToken cancellationToken = default);

    Task<LoginResponse> LoginAsync(LoginWithPhoneNumberRequest loginRequest, CancellationToken cancellationToken = default);

    Task<bool> RegisterAsync(User user, CancellationToken cancellationToken = default);

    Task<LoginResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<bool> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default);
}