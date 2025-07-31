using Project.Application.Common.Notifications.Formatters;
using Project.Domain.Enums;

namespace Project.Infrastructure.Common.Notifications.Formatters;

public class EmailNotificationFormatter : INotificationFormatter
{
    public NotificationChannelType ChannelType { get; } = NotificationChannelType.Email;

    public string FormatTitle(string title) => $"{title}";

    public string FormatMessage(string message)
    {
        return $@"
        <html>
            <head>
                <style>
                    body {{
                        font-family: 'Segoe UI', sans-serif;
                        background-color: #f9f9f9;
                        padding: 20px;
                        color: #333;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: auto;
                        background-color: #fff;
                        border-radius: 8px;
                        box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
                        padding: 24px;
                    }}
                    .footer {{
                        margin-top: 24px;
                        font-size: 12px;
                        color: #888;
                        border-top: 1px solid #eee;
                        padding-top: 16px;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <h2>Azgara CRM</h2>
                    <p>{message}</p>
        
                    <div class=""footer"">
                        Ushbu xabar avtomatik tarzda yuborilgan. Iltimos, javob yozmang.<br />
                        &copy; {DateTime.UtcNow.Year} Azgara CRM
                    </div>
                </div>
            </body>
        </html>";

    }
}