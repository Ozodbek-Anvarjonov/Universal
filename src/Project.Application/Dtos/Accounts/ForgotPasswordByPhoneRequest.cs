namespace Project.Application.Dtos.Accounts;

public class ForgotPasswordByPhoneRequest
{
    public string PhoneNumber { get; set; } = default!;
}