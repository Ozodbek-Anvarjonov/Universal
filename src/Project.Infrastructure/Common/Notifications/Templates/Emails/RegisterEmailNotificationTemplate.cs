using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates.Emails;

public class RegisterEmailNotificationTemplate : INotificationTemplate
{
    public NotificationType Type { get; } = NotificationType.Register;
    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Email;

    public string GetTitle(NotificationTemplateContext? context = null) =>
        "Registration successful";

    public string GetMessage(NotificationTemplateContext? context = null)
    {
        if (context is not RegisterNotificationTemplateContext ctx)
            return "You have successfully registered in the system.";

        return $"Dear {ctx.FirstName}, you have successfully registered with the system at {ctx.RegisteredAt}.";
    }
}