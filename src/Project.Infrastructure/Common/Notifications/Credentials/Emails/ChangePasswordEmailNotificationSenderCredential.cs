using Microsoft.Extensions.Options;
using Project.Domain.Enums;
using Project.Infrastructure.Common.Notifications.Credentials.Emails.Options;

namespace Project.Infrastructure.Common.Notifications.Credentials.Emails;

public class ChangePasswordEmailNotificationSenderCredential : EmailNotificationSenderCredential
{
    public override NotificationType Type { get; } = NotificationType.ChangePassword;

    public ChangePasswordEmailNotificationSenderCredential(IOptions<ChangePasswordEmailNotificationSenderCredentialOptions> options)
    {
        Host = options.Value.Host;
        Port = options.Value.Port;
        EnableSsl = options.Value.EnableSsl;
        SenderName = options.Value.SenderName;
        SenderEmail = options.Value.SenderEmail;
        Username = options.Value.Username;
        Password = options.Value.Password;
    }
}