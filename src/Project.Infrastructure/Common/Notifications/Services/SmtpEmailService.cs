using Project.Application.Common.Exceptions;
using Project.Application.Common.Notifications.Models;
using Project.Application.Common.Notifications.Services;
using Project.Infrastructure.Common.Notifications.Credentials.Emails;
using System.Net;
using System.Net.Mail;

namespace Project.Infrastructure.Common.Notifications.Services;

public class SmtpEmailService : IEmailService
{
    public async ValueTask<SendResult> SendAsync(ChannelContext context, CancellationToken cancellationToken = default)
    {
        if (context.Credential is not EmailNotificationSenderCredential credential)
            throw new NotFoundException($"Credential is not found for type {context.Credential.Type} and channel {context.Credential.ChannelType}.");

        var smtpClient = new SmtpClient(credential.Host, credential.Port)
        {
            EnableSsl = credential.EnableSsl,
            Credentials = new NetworkCredential { UserName = credential.SenderEmail, Password = credential.Password },
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(credential.SenderEmail),
            Subject = context.Title,
            Body = context.Message,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(context.ReceiverUser.EmailAddress);

        var sendResult = new SendResult
        {
            SenderName = credential.SenderName,
            SenderContact = credential.SenderEmail,
        };

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
            sendResult.IsSent = true;
            sendResult.DeliveredAt = DateTimeOffset.UtcNow;
        }
        catch (SmtpFailedRecipientException ex)
        {
            sendResult.ErrorMessage = $"SMTP recipient error: {ex.FailedRecipient} - {ex.Message}";
        }
        catch (SmtpException ex)
        {
            sendResult.ErrorMessage = $"SMTP error: {ex.StatusCode} - {ex.Message}";
        }
        catch (FormatException ex)
        {
            sendResult.ErrorMessage = $"Email format xatosi: {ex.Message}";
        }
        catch (Exception ex)
        {
            sendResult.ErrorMessage = $"Xatolik yuz berdi: {ex.Message}";
        }

        return sendResult;
    }
}