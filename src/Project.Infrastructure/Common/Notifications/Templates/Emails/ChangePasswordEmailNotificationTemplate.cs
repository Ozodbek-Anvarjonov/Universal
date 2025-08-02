using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates.Emails;

public class ChangePasswordEmailNotificationTemplate : INotificationTemplate
{
    public NotificationType Type { get; } = NotificationType.ChangePassword;
    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Email;

    public string GetMessage(NotificationTemplateContext? context = null)
    {
        throw new NotImplementedException();
    }

    public string GetTitle(NotificationTemplateContext? context = null)
    {
        throw new NotImplementedException();
    }
}