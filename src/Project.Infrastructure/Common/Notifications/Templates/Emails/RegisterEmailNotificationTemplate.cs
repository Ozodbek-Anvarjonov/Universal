using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;
using Project.Infrastructure.Common.Notifications.Templates.Base;

namespace Project.Infrastructure.Common.Notifications.Templates.Emails;

public class RegisterEmailNotificationTemplate : RegisterNotificationTemplateBase
{
    public override NotificationChannelType ChannelType => NotificationChannelType.Email;

    public override TemplateContext GetContext(NotificationTemplateContext? context = null)
    {
        var (title, message) = base.BuildText(context);
        var formattedTitle = $"<h2 style=\"color: #2c3e50; font-family: Arial, sans-serif;\">{title}</h2>";
        var formattedMessage = $@"
            <div style=""font-family: Arial, sans-serif; font-size: 16px; color: #333;"">
                <p>{message}</p>
                <p style=""margin-top: 20px; color: #888;"">
                    If this wasn't you, please change your password immediately.
                </p>
            </div>";

        return new TemplateContext
        {
            Title = title,
            Message = message,
            FormattedTitle = formattedTitle,
            FormattedMessage = formattedMessage
        };
    }
}