using Microsoft.Extensions.Options;
using Project.Domain.Enums;
using Project.Infrastructure.Common.Notifications.Credentials.Emails.Options;

namespace Project.Infrastructure.Common.Notifications.Credentials.Emails;

public class LoginEmailNotificationSenderCredential : EmailNotificationSenderCredential
{
    public override NotificationType Type { get; } = NotificationType.Login;

    public LoginEmailNotificationSenderCredential(IOptions<LoginEmailNotificationSenderCredentialOptions> options)
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