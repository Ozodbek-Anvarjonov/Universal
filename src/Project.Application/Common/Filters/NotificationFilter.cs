using Project.Domain.Enums;

namespace Project.Application.Common.Filters;

public class NotificationFilter : PaginationFilter
{
    public string? SenderName { get; set; }
    public string? SenderContact { get; set; }

    public long? ReceiverUserId { get; set; }
    public string? ReceiverUserName { get; set; }

    public NotificationType? Type { get; set; }
    public NotificationChannelType? ChannelType { get; set; }

    public string? Title { get; set; } = default!;
    public string? Message { get; set; } = default!;

    public bool? IsDelivered { get; set; }
    public DateTimeOffset? DeliveredAt { get; set; }

    public bool? IsRead { get; set; } = false;
}