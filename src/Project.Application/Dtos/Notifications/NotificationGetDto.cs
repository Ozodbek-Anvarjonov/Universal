using Project.Application.Dtos.Users;
using Project.Domain.Enums;
using System.Text.Json.Serialization;

namespace Project.Application.Dtos.Notifications;

public class NotificationGetDto
{
    public long Id { get; set; }

    public string SenderName { get; set; } = default!;
    public string SenderContact { get; set; } = default!;

    public long ReceiverUserId { get; set; }
    public UserGetDto ReceiverUser { get; set; } = default!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NotificationType Type { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NotificationChannelType ChannelType { get; set; }

    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;

    public bool IsDelivered { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTimeOffset? DeliveredAt { get; set; }

    public bool IsRead { get; set; } = false;

    public long CreatedById { get; set; }
    public UserGetDto CreatedBy { get; set; } = default!;
}