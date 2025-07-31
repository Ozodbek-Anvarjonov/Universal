using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Templates;

public interface INotificationTemplate
{
    NotificationType Type { get; }

    string GetTitle(NotificationTemplateContext? context = null);
    string GetMessage(NotificationTemplateContext? context = null);
}