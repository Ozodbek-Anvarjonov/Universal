using Microsoft.Extensions.Options;
using Project.Application.Settings;
using Project.Persistence.UnitOfWork.Interfaces;
using System.Security.Claims;

namespace Project.Api.Services;

public class UserContext : IUserContext
{
    public UserContext(IHttpContextAccessor contextAccessor, IOptions<SystemSettings> options)
    {
        SystemId = options.Value.SystemId;

        var userIdClaim = contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (long.TryParse(userIdClaim, out var userId))
            UserId = userId;
        else
            UserId = null;
    }

    public long SystemId { get; }
    public long? UserId { get; }

    public long GetCurrentUserId() =>
        UserId is not null ? UserId.Value : SystemId;
}