namespace Project.Application.Dtos.Accounts;

public class ResetPasswordByPhoneRequest
{
    public string PhoneNumber { get; set; } = default!;

    public string Code { get; set; } = default!;
    
    public string NewPassword { get; set; } = default!;
    
    public string ConfirmNewPassword { get; set; } = default!;
}