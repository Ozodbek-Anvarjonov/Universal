using Project.Domain.Common.Entities;
using Project.Domain.Enums;

namespace Project.Domain.Entities;

public class Notification : SoftDeletedEntity
{
    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public NotificationType Type { get; set; }
    public NotificationChannelType ChannelType { get; set; }

    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;
    
    public bool IsDelivered { get; set; }
    public DateTimeOffset? DeliveredAt { get; set; }

    public bool IsRead { get; set; } = false;
}