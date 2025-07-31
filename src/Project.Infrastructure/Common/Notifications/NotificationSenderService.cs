using Project.Application.Common.Notifications.Channels;
using Project.Application.Common.Notifications.Credentials;
using Project.Application.Common.Notifications.Formatters;
using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Services;
using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Entities;

namespace Project.Infrastructure.Common.Notifications;

public class NotificationSenderService(
    INotificationSenderChannelProvider channelProvider,
    INotificationSenderCredentialProvider credentialProvider,
    INotificationTemplateProvider templateProvider,
    INotificationFormatterProvider formatterProvider,
    INotificationService notificationService
    ) : INotificationSenderService
{
    public async Task SendAsync(Notification notification, NotificationTemplateContext context, CancellationToken cancellationToken = default)
    {
        var template = templateProvider.GetTemplate(notification.Type);
        var formatter = formatterProvider.GetFormatter(notification.ChannelType);
        var credential = credentialProvider.GetCredential(notification.Type, notification.ChannelType);
        var channel = channelProvider.GetChannel(notification.ChannelType);

        var title = template.GetTitle(context);
        var message = template.GetMessage(context);
        var formatTitle = formatter.FormatTitle(title);
        var formatMessage = formatter.FormatMessage(message);
        var sendResult = await channel.SendAsync(new ChannelContext
        {
            Title = title,
            Message = formatMessage,
            FormattedTitle = formatTitle,
            FormattedMessage = formatMessage,
            Credential = credential,
            ReceiverUserId = notification.ReceiverUserId,
            ReceiverUser = notification.ReceiverUser,
        });

        notification.Title = title;
        notification.Message = formatMessage;
        notification.SenderName = sendResult.SenderName;
        notification.SenderContact = sendResult.SenderContact;
        notification.IsDelivered = sendResult.IsSent;
        notification.ErrorMessage = sendResult.ErrorMessage;

        await notificationService.CreateAsync(notification, cancellationToken: cancellationToken);
    }
}