namespace Project.Application.Common.Notifications.Templates.Contexts;

public class RegisterNotificationTemplateContext : NotificationTemplateContext
{
    public string FirstName { get; set; } = default!;

    public string RegisteredAt { get; set; } = default!;
}