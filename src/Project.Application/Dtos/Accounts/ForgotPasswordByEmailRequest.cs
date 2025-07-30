namespace Project.Application.Dtos.Accounts;

public class ForgotPasswordByEmailRequest
{
    public string EmailAddress { get; set; } = default!;
}