using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates.Emails;

public class LoginEmailNotificationTemplate : INotificationTemplate
{
    public NotificationType Type { get; } = NotificationType.Login;
    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Email;

    public string GetTitle(NotificationTemplateContext? context = null) =>
        "Login successful";

    public string GetMessage(NotificationTemplateContext? context = null)
    {
        if (context is not LoginNotificationTemplateContext ctx)
            return "You have successfully registered in the system.";

        return $"Dear {ctx.FirstName}, you have successfully login with the system at {ctx.LoginAt}.";
    }
}