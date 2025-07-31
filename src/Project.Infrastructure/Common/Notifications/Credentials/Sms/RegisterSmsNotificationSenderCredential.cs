using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Credentials.Sms;

public class RegisterSmsNotificationSenderCredential : SmsNotificationSenderCredential
{
    public override NotificationType Type { get; } = NotificationType.Register;
}