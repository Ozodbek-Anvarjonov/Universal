using Project.Application.Common.Exceptions;
using Project.Application.Common.Notifications.Credentials;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Credentials;

public class NotificationSenderCredentialProvider : INotificationSenderCredentialProvider
{
    private readonly Dictionary<(NotificationType, NotificationChannelType), INotificationSenderCredential> credentialMap;

    public NotificationSenderCredentialProvider(IEnumerable<INotificationSenderCredential> credentials)
    {
        credentialMap = new();

        foreach (var credential in credentials)
        {
            credentialMap[(credential.Type, credential.ChannelType)] = credential;
        }
    }
    
    public INotificationSenderCredential GetCredential(NotificationType type, NotificationChannelType channelType)
    {
        if (credentialMap.TryGetValue((type, channelType), out var credential))
            return credential;

        throw new NotFoundException($"$Credential not found for type{type} and channel {channelType}");
    }
}