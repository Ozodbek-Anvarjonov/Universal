using Project.Domain.Enums;

namespace Project.Application.Dtos.Notifications;

public class NotificationDto
{
    public long ReceiverUserId { get; set; }

    public NotificationType Type { get; set; }
    public NotificationChannelType ChannelType { get; set; }
}