using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates;

public class ChangePasswordNotificationTemplate : INotificationTemplate
{
    public NotificationType Type { get; } = NotificationType.ChangePassword;

    public string GetMessage(NotificationTemplateContext? context = null)
    {
        throw new NotImplementedException();
    }

    public string GetTitle(NotificationTemplateContext? context = null)
    {
        throw new NotImplementedException();
    }
}