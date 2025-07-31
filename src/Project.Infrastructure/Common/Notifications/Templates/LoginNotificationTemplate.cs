using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates;

public class LoginNotificationTemplate : INotificationTemplate
{
    public NotificationType Type { get; } = NotificationType.Login;

    public string GetMessage(NotificationTemplateContext? context = null)
    {
        throw new NotImplementedException();
    }

    public string GetTitle(NotificationTemplateContext? context = null)
    {
        throw new NotImplementedException();
    }
}