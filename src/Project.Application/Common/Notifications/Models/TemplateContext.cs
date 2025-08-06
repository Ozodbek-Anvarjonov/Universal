namespace Project.Application.Common.Notifications.Models;

public class TemplateContext
{
    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;

    public string FormattedTitle { get; set; } = default!;
    public string FormattedMessage { get; set;} = default!;
}