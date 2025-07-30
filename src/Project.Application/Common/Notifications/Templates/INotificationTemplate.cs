using Project.Domain.Enums;

namespace Project.Application.Common.Notifications.Templates;

public interface INotificationTemplate
{
    NotificationType Type { get; }

    string GetTitle(object? context = null);
    string GetMessage(object? context = null);
}