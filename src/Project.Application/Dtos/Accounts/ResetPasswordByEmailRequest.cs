namespace Project.Application.Dtos.Accounts;

public class ResetPasswordByEmailRequest
{
    public string EmailAddress { get; set; } = default!;

    public string Token { get; set; } = default!;

    public string NewPassword { get; set; } = default!;

    public string ConfirmedPassword { get; set; } = default!;
}