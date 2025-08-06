using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates.Base;

public abstract class LoginNotificationTemplateBase : INotificationTemplate
{
    public NotificationType Type { get; } = NotificationType.Login;
    public abstract NotificationChannelType ChannelType { get; }

    public abstract TemplateContext GetContext(NotificationTemplateContext? context = null);

    protected virtual (string Title, string Message) BuildText(NotificationTemplateContext? context)
    {
        var title = "Login successful";
        var message = context is LoginNotificationTemplateContext ctx
            ? $"Hi {ctx.FirstName}, your login was successful. Welcome aboard!"
            : "Your login was successful. Welcome!";

        return (title, message);
    }
}