using Project.Application.Common.Notifications.Credentials;
using Project.Domain.Entities;

namespace Project.Application.Common.Notifications.Models;

public class ChannelContext
{
    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;

    public string FormattedTitle { get; set; } = default!;
    public string FormattedMessage { get; set; } = default!;

    public long ReceiverUserId { get; set; }
    public User ReceiverUser { get; set; } = default!;

    public INotificationSenderCredential Credential { get; set; } = default!;
}