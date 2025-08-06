using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;
using Project.Infrastructure.Common.Notifications.Templates.Base;

namespace Project.Infrastructure.Common.Notifications.Templates.Sms;

public class ChangePasswordSmsNotificationTemplate : ChangePasswordNotificationTemplateBase
{
    public override NotificationChannelType ChannelType => NotificationChannelType.Sms;

    public override TemplateContext GetContext(NotificationTemplateContext? context = null)
    {
        var (title, message) = BuildText(context);

        return new TemplateContext
        {
            Title = title,
            Message = message,
            FormattedTitle = title,
            FormattedMessage = message
        };
    }
}