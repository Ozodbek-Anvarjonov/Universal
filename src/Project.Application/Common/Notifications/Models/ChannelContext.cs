using Project.Application.Common.Notifications.Credentials;
using System.Net;

namespace Project.Application.Common.Notifications.Models;

public class ChannelContext
{
    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;

    public string FormattedTitle { get; set; } = default!;
    public string FormattedMessage { get; set; } = default!;

    public INotificationSenderCredential Credential { get; set; } = default!;
}