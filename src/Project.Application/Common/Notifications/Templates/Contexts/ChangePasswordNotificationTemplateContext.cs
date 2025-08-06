namespace Project.Application.Common.Notifications.Templates.Contexts;

public class ChangePasswordNotificationTemplateContext : NotificationTemplateContext
{
    public string FirstName { get; set; } = default!;

    public string ChangedAt { get; set; } = default!;
}