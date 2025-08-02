using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Templates;

public interface INotificationTemplateProvider
{
    INotificationTemplate GetTemplate(NotificationType type, NotificationChannelType channelType);
}