namespace Project.Application.Common.Notifications.Services;

public interface IEmailService
{
    ValueTask<bool> SendAsync();
}