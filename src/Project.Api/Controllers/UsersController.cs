using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Common.Exceptions;
using Project.Application.Common.Filters;
using Project.Application.Dtos.Users;

namespace Project.Api.Controllers;

public class UsersController : BaseController
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] UserFilter filter, CancellationToken cancellationToken)
    {
        throw new NotFoundException();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Post([FromBody] UserDto dto, CancellationToken cancellationToken)
    {
        throw new ConflictException();
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> Put([FromRoute] long id, [FromBody] UserDto dto, CancellationToken cancellationToken)
    {
        throw new ValidationException();
    }

    [HttpPatch("{id:long}")]
    public async ValueTask<IActionResult> Patch([FromRoute] long id, [FromBody] UserDto dto, CancellationToken cancellationToken)
    {
        throw new ArgumentOutOfRangeException();
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        throw new BadRequestException();
    }
}