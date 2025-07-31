namespace Project.Application.Common.Notifications.Models;

public class SendResult
{
    public string SenderName { get; set; } = default!;

    public string SenderContact { get; set; } = default!;
    
    public bool IsSent { get; set; }
}