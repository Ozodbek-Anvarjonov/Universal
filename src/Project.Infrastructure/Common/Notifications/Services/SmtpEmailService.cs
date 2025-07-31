using Project.Application.Common.Notifications.Services;

namespace Project.Infrastructure.Common.Notifications.Services;

public class SmtpEmailService : IEmailService
{
    public ValueTask<bool> SendAsync()
    {
        throw new NotImplementedException();
    }
}