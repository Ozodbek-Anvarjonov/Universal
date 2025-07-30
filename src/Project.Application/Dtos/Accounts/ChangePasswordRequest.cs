namespace Project.Application.Dtos.Accounts;

public class ChangePasswordRequest
{
    public string CurrentPassword { get; set; } = default!;

    public string NewPassword { get; set; } = default!;

    public string ConfirmNewPassword { get; set; } = default!;
}