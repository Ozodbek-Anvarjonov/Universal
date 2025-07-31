using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Credentials.Emails;

public class RegisterEmailNotificationSenderCredential : EmailNotificationSenderCredential
{
    public override NotificationType Type { get; } = NotificationType.Register;
}