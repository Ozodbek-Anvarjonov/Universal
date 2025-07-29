using Project.Domain.Common.Entities;

namespace Project.Domain.Entities;

public class RefreshToken : Entity
{
    public string Token { get; set; } = default!;

    public DateTimeOffset ExpiresAt { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
