using Project.Application.Common.Exceptions;
using Project.Application.Common.Notifications.Templates;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates;

public class NotificationTemplateProvider : INotificationTemplateProvider
{
    private readonly Dictionary<NotificationType, INotificationTemplate> templateMap;

    public NotificationTemplateProvider(IEnumerable<INotificationTemplate> templates)
    {
        templateMap = new();

        foreach (var template in templates)
        {
            templateMap[template.Type] = template;
        }
    }

    public INotificationTemplate GetTemplate(NotificationType type)
    {
        if (templateMap.TryGetValue(type, out var template)) return template;

        throw new NotFoundException($"Template not found for type{type}.");
    }
}