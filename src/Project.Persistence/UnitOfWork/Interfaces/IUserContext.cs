namespace Project.Persistence.UnitOfWork.Interfaces;

public interface IUserContext
{
    long SystemId { get; }

    long? UserId { get; }

    long GetCurrentUserId();
}