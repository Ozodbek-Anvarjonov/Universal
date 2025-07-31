namespace Project.Infrastructure.Common.Notifications.Credentials.Sms.Options;

public abstract class SmsNotificationSenderCredentialOptions
{
    public string Provider { get; set; } = default!;

    public string ApiUrl { get; set; } = default!;

    public string ApiToken { get; set; } = default!;

    public string SenderName { get; set; } = default!;
}