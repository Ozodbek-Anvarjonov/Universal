using Project.Application.Common.Notifications.Models;

namespace Project.Application.Common.Notifications.Services;

public interface ISmsSenderService
{
    ValueTask<SendResult> SendAsync(ChannelContext context, CancellationToken cancellationToken = default);

}