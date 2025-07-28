using Project.Persistence.UnitOfWork.Interfaces;

namespace Project.Api.Services;

public class UserContext : IUserContext
{
    public long SystemId { get; }
    public long? UserId { get; }

    public long GetCurrentUserId() =>
        UserId is not null ? UserId.Value : SystemId;
}