using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates;

public class LoginNotificationTemplate : INotificationTemplate
{
    public NotificationType Type { get; } = NotificationType.Login;

    public string GetTitle(NotificationTemplateContext? context = null) =>
        "Login successful";

    public string GetMessage(NotificationTemplateContext? context = null)
    {
        if (context is not LoginNotificationTemplateContext ctx)
            return "You have successfully registered in the system.";

        return $"Dear {ctx.FirstName}, you have successfully login with the system at {ctx.LoginAt}.";
    }
}