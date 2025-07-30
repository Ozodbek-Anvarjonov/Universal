using Project.Application.Common.Notifications.Templates;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Templates;

public class RegisterNotificationTemplate : INotificationTemplate
{
    public NotificationType Type { get; } = NotificationType.Register;

    public string GetMessage(object? context = null)
    {
        throw new NotImplementedException();
    }

    public string GetTitle(object? context = null)
    {
        throw new NotImplementedException();
    }
}