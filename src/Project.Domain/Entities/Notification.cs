using Project.Domain.Common.Entities;
using Project.Domain.Enums;

namespace Project.Domain.Entities;

public class Notification : SoftDeletedEntity
{
    public string SenderName { get; set; } = default!;
    public string SenderContact { get; set; } = default!;

    public long ReceiverUserId { get; set; }
    public User ReceiverUser { get; set; } = default!;

    public NotificationType Type { get; set; }
    public NotificationChannelType ChannelType { get; set; }

    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;

    public bool IsDelivered { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTimeOffset? DeliveredAt { get; set; }

    public bool IsRead { get; set; } = false;
}