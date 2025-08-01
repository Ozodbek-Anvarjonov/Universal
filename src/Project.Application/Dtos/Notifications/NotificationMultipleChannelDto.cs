using Project.Domain.Enums;

namespace Project.Application.Dtos.Notifications;

public class NotificationMultipleChannelDto
{
    public long ReceiverUserId { get; set; }

    public NotificationType Type { get; set; }
    public IEnumerable<NotificationChannelType> ChannelTypes { get; set; } = [];
}