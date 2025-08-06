using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates.Base;

public abstract class RegisterNotificationTemplateBase : INotificationTemplate
{
    public NotificationType Type => NotificationType.Register;
    public abstract NotificationChannelType ChannelType { get; }

    protected virtual (string Title, string Message) BuildText(NotificationTemplateContext? context)
    {
        var title = "Registration successful";
        var message = context is RegisterNotificationTemplateContext ctx
            ? $"Hi {ctx.FirstName}, your registration was successful. Welcome aboard!"
            : "Your registration was successful. Welcome!";

        return (title, message);
    }

    public abstract TemplateContext GetContext(NotificationTemplateContext? context = null);
}