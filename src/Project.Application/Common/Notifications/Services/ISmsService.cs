using Project.Application.Common.Notifications.Models;

namespace Project.Application.Common.Notifications.Services;

public interface ISmsService
{
    Task<SendResult> SendAsync(ChannelContext context, CancellationToken cancellationToken = default);
}