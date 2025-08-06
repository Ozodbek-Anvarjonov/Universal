using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Templates;
using Project.Application.Common.Notifications.Templates.Contexts;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates.Base;

public abstract class ChangePasswordNotificationTemplateBase : INotificationTemplate
{
    public NotificationType Type { get; } = NotificationType.ChangePassword;
    public abstract NotificationChannelType ChannelType { get; }

    public abstract TemplateContext GetContext(NotificationTemplateContext? context = null);

    protected virtual (string Title, string Message) BuildText(NotificationTemplateContext? context)
    {
        var title = "Password changed";
        var message = context is ChangePasswordNotificationTemplateContext ctx
            ? $"Hi {ctx.FirstName}, your password was changed successfully at {ctx.ChangedAt}."
            : "Your password was changed successfully.";

        return (title, message);
    }
}