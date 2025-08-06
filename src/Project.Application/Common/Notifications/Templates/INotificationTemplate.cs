using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Templates;

public interface INotificationTemplate
{
    NotificationType Type { get; }
    NotificationChannelType ChannelType { get; }

    TemplateContext GetContext(NotificationTemplateContext? context = null);
}