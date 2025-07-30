using Project.Application.Common.Notifications.Templates;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates;

public class NotificationTemplateProvider : INotificationTemplateProvider
{
    public INotificationTemplate GetTemplate(NotificationType type)
    {
        throw new NotImplementedException();
    }
}