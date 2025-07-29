using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Extensions;
using Project.Api.Filters;
using Project.Application.Common.Filters;
using Project.Application.Dtos.Users;
using Project.Application.Services;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Api.Controllers;

[CustomAuthorize(nameof(UserRole.Client))]
public class UsersController(
    IUserService service,
    IValidator<UserDto> validator,
    IMapper mapper
    ) : BaseController
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] UserFilter filter, CancellationToken cancellationToken)
    {
        var data = await service.GetAsync(filter, cancellationToken: cancellationToken);

        return Ok(mapper.Map<IEnumerable<UserGetDto>>(data));
    }

    [HttpPost]
    public async ValueTask<IActionResult> Post([FromBody] UserDto dto, CancellationToken cancellationToken)
    {
        await validator.EnsureValidationAsync(dto, cancellationToken);

        var data = await service.CreateAsync(mapper.Map<User>(dto), cancellationToken: cancellationToken);

        return Ok(mapper.Map<UserGetDto>(data));
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> Put([FromRoute] long id, [FromBody] UserDto dto, CancellationToken cancellationToken)
    {
        await validator.EnsureValidationAsync(dto);

        var data = await service.UpdateAsync(id, mapper.Map<User>(dto), cancellationToken: cancellationToken);

        return Ok(mapper.Map<UserGetDto>(data));
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        var data = await service.DeleteByIdAsync(id, cancellationToken: cancellationToken);

        return Ok(data);
    }
}