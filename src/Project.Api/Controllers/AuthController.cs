using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Extensions;
using Project.Application.Common.Identities;
using Project.Application.Dtos.Login;
using Project.Application.Dtos.Users;
using Project.Domain.Entities;

namespace Project.Api.Controllers;

public class AuthController(
    IAuthService service,
    IMapper mapper,
    IValidator<UserDto> validator
    ) : BaseController
{
    [HttpPost("login/email")]
    public async ValueTask<IActionResult> LoginWithEmail(
        [FromBody] LoginWithEmailRequest request,
        CancellationToken cancellationToken = default
        )
    {
        var data = await service.LoginAsync(request, cancellationToken);

        return Ok(data);
    }

    [HttpPost("login/phone")]
    public async ValueTask<IActionResult> LoginWithPhoneNumber(
        [FromBody] LoginWithPhoneNumberRequest request,
        CancellationToken cancellationToken = default
        )
    {
        var data = await service.LoginAsync(request, cancellationToken);

        return Ok(data);
    }

    [HttpPost("register")]
    public async ValueTask<IActionResult> Register(
        [FromBody] UserDto dto,
        CancellationToken cancellationToken = default
        )
    {
        await validator.EnsureValidationAsync(dto);

        var data = await service.RegisterAsync(mapper.Map<User>(dto), cancellationToken);

        return Ok(data);
    }

    [HttpPost("refresh-token")]
    public async ValueTask<IActionResult> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken = default
        )
    {
        var data = await service.RefreshTokenAsync(request.RefreshToken, cancellationToken);

        return Ok(data);
    }

    [HttpPost("logout")]
    public async ValueTask<IActionResult> Logout([FromBody] LogoutRequest request, CancellationToken cancellationToken = default)
    {
        var data = await service.LogoutAsync(request.RefreshToken, cancellationToken);

        return Ok(data);
    }
}