using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Extensions;
using Project.Application.Common.Identities;
using Project.Application.Dtos.Accounts;
using Project.Application.Dtos.Users;

namespace Project.Api.Controllers;

public class AccountController(
    IAccountService accountService,
    IMapper mapper,
    IValidator<ChangePasswordRequest> validator
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
        await validator.EnsureValidationAsync(request);
        var data = await accountService.ChangePasswordAsync(request, cancellationToken: cancellationToken);
        return Ok(data);
    }
}