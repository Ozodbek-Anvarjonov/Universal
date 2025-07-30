using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Extensions;
using Project.Api.Filters;
using Project.Application.Common.Identities;
using Project.Application.Dtos.Accounts;
using Project.Application.Dtos.Users;
using Project.Domain.Enums;

namespace Project.Api.Controllers;

[CustomAuthorize(nameof(UserRole.Client), nameof(UserRole.Admin), nameof(UserRole.Owner))]
public class AccountController(
    IAccountService accountService,
    IMapper mapper,
    IValidator<ChangePasswordRequest> changePasswordValidator,
    IValidator<ForgotPasswordByEmailRequest> forgotEmailValidator,
    IValidator<ForgotPasswordByPhoneRequest> forgotPhoneValidator,
    IValidator<ResetPasswordByEmailRequest> resetEmailValidator,
    IValidator<ResetPasswordByPhoneRequest> resetPhoneValidator
    ) : BaseController
{
    [HttpGet("profile")]
    public async ValueTask<IActionResult> Get(CancellationToken cancellationToken)
    {
        var data = await accountService.GetAsync(cancellationToken: cancellationToken);

        return Ok(mapper.Map<UserGetDto>(data));
    }


    [HttpPatch("password")]
    public async ValueTask<IActionResult> Patch([FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        await changePasswordValidator.EnsureValidationAsync(request);
        var data = await accountService.ChangePasswordAsync(request, cancellationToken: cancellationToken);
        return Ok(data);
    }

    [AllowAnonymous]
    [HttpPost("forgot-password/email")]
    public async ValueTask<IActionResult> PostByEmail([FromBody] ForgotPasswordByEmailRequest request, CancellationToken cancellationToken)
    {
        await forgotEmailValidator.EnsureValidationAsync(request);
        var data = await accountService.ForgotPasswordByEmailAsync(request, cancellationToken: cancellationToken);
        return Ok(data);
    }

    [AllowAnonymous]
    [HttpPost("forgot-password/phone")]
    public async ValueTask<IActionResult> PostByPhone([FromBody] ForgotPasswordByPhoneRequest request, CancellationToken cancellationToken)
    {
        await forgotPhoneValidator.EnsureValidationAsync(request);
        var data = await accountService.ForgotPasswordByPhoneAsync(request, cancellationToken: cancellationToken);
        return Ok(data);
    }

    [AllowAnonymous]
    [HttpPost("reset-password/email")]
    public async ValueTask<IActionResult> ResetByEmail([FromBody] ResetPasswordByEmailRequest request, CancellationToken cancellationToken)
    {
        await resetEmailValidator.EnsureValidationAsync(request);
        var data = await accountService.ResetPasswordByEmailAsync(request, cancellationToken: cancellationToken);
        return Ok(data);
    }

    [AllowAnonymous]
    [HttpPost("reset-password/phone")]
    public async ValueTask<IActionResult> ResetByPhone([FromBody] ResetPasswordByPhoneRequest request, CancellationToken cancellationToken)
    {
        await resetPhoneValidator.EnsureValidationAsync(request);
        var data = await accountService.ResetPasswordByPhoneAsync(request, cancellationToken: cancellationToken);
        return Ok(data);
    }
}