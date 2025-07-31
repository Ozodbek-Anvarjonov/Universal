using Project.Application.Common.Exceptions;
using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Services;
using Project.Infrastructure.Common.Notifications.Credentials.Sms;

namespace Project.Infrastructure.Common.Notifications.Services;

public class EskizSmsSenderService : ISmsSenderService
{
    public async ValueTask<SendResult> SendAsync(ChannelContext context, CancellationToken cancellationToken = default)
    {
        if (context.Credential is not SmsNotificationSenderCredential credential)
            throw new NotFoundException($"Credential is not found for type {context.Credential.Type} and channel {context.Credential.ChannelType}.");

        return new SendResult
        {
            IsSent = true,
            SenderName = credential.SenderName,
            SenderContact = context.ReceiverUser.PhoneNumber,
        };
    }
}