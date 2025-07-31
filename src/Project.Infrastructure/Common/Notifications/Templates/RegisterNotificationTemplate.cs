using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates;

public class RegisterNotificationTemplate : INotificationTemplate
{
    public NotificationType Type { get; } = NotificationType.Register;

    public string GetMessage(NotificationTemplateContext? context = null) =>
        "Registration successful";

    public string GetTitle(NotificationTemplateContext? context = null)
    {
        if (context is not RegisterNotificationTemplateContext ctx)
            return "You have successfully registered in the system.";

        return $"Dear {ctx.FirstName}, you have successfully registered with the system at {ctx.RegisteredAt}.";
    }
}